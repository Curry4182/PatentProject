using System;
using System.Collections.Generic;
using System.Text;

namespace ExcelInterop
{
    public class PatentDateModel
    {

        public DateTime MaxTime { get; set; }
        public DateTime MinTime { get; set; }
        /// <summary>
        /// 연도별로 묶을때의 간격
        /// </summary>
        public int IntervalYear { get; set; }
        /// <summary>
        /// 날짜를 입력받은 순서대로 저장
        /// </summary>
        public IEnumerable<DateTime> Dates { get; set; }
        /// <summary>
        /// IntervalYear간격으로 묶을때 각 간격당 몇개의 date가 있는지 저장.
        /// </summary>
        public int[] YearsCount { get; set; }
        /// <summary>
        /// 전체 date의 크기
        /// </summary>
        public int Size { get; set; }
    }
}
