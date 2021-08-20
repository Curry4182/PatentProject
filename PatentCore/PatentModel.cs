using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ganss.Excel;

namespace ExcelInterop
{
    public class PatentModel
    {  
        [Column("순서")]
        public string Order { get; set; }
        [Column("심사진행상태")]
        public string ExaminationProgress { get; set; }

        [Column("출원번호")]
        public string ApplicationNumber { get; set; }

        [Column("출원일자")]
        public string ApplicationDate { get; set; }

        [Column("발명의명칭")]
        public string TitleOfInvention{ get; set; }

        [Column("출원인")]
        public string Applicant { get; set; }

        [Column("IPC분류")]
        public string IPC { get; set; }

        [Column("CPC분류")]
        public string CPC { get; set; }

        [Column("공개일자")]
        public string OpenDate { get; set; }

        [Column("등록일자")]
        public string RegistrationDate { get; set; }

        [Column("심사청구항수")]
        public string NumberOfClaimsForExamination { get; set; }
    
        [Column("발명자")]
        public string Inventor { get; set; }

        [Column("요약")]
        public string Summary { get; set; }

        [Column("청구항")]
        public string Claim { get; set; }
    }
}
