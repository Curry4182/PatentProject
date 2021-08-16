using ExcelInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelInterop
{
    public interface IPatentProcess
    {
        public PatentDateModel GetApplicationDate(int intervalYear = 5);
        public List<KeyValuePair<string, int>> GetApplicant();
    }
}
