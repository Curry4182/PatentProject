using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoardProject
{
    public struct PagingResult<T>
    {
        public PagingResult(IEnumerable<T> items, int totalRecords)
        {
            Records = items;
            TotalRecords = totalRecords;
        }

        public IEnumerable<T> Records { get; set; }
        public int TotalRecords { get; set; }
    }
}
