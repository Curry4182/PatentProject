using Ganss.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExcelInterop
{
    public class PatentProcess
    {
        IList<PatentModel> patents;
        public PatentProcess(string filePath)
        {
            var mapper = new ExcelMapper(filePath) { HeaderRow = true, HeaderRowNumber = 7};
            patents = mapper.Fetch<PatentModel>().ToList();
        }
        //엑셀 파일위치를 입력으로하면
        //출원일자를 size 간격만큼 저장한 후 반환합니다.
        public PatentDateModel GetYears(int intervalYear = 5)
        {
            var patentDate = new PatentDateModel();
            var dates = new List<DateTime>();

            DateTime min = DateTime.MaxValue, max = DateTime.MinValue;
            foreach (var patent in patents)
            {
                if(patent.ApplicationDate == null) continue;
                var date = Convert.ToDateTime(patent.ApplicationDate.Replace(".", "-"));
                dates.Add(Convert.ToDateTime(date));

                min = date < min ? date : min;
                max = date > max ? date : max;
            }

            patentDate.Size = Convert.ToInt32((max.Year - min.Year) / intervalYear) + 1;
            patentDate.YearsCount = new int[patentDate.Size];
            patentDate.IntervalYear = intervalYear;

            foreach (var date in dates)
            {
                int idx = (date.Year - min.Year) / intervalYear;
                patentDate.YearsCount[idx] = patentDate.YearsCount[idx] + 1;
            }

            patentDate.Dates = dates;
            patentDate.MinTime = min;
            patentDate.MaxTime = max;

            return patentDate;
        }

        public List<KeyValuePair<string, int>> GetApplicant()
        {
            var applicantDic = new Dictionary<string, int>();
            foreach (var patent in patents)
            {
                if (patent.Applicant == null) continue;
                if (!applicantDic.ContainsKey(patent.Applicant))
                {
                    applicantDic.Add(patent.Applicant, 1);
                }
                else
                {
                    applicantDic[patent.Applicant] = applicantDic[patent.Applicant] + 1;
                }
            }

            var applicationDicSorted = from pair in applicantDic
                                       orderby pair.Value descending
                                       select pair;

            return applicationDicSorted.ToList();
        }
    }
}
