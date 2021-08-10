using System;
using System.Collections.Generic;
using System.Text;

namespace ExcelInterop
{
    public class PatentDateModel
    {
        public DateTime MaxTime { get; set; }
        public DateTime MinTime { get; set; }
        public int IntervalYear { get; set; }
        public IEnumerable<DateTime> Dates { get; set; }
        public int[] YearsCount { get; set; }
        public int Size { get; set; }
    }
}
