using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardProject.Models
{
    public class PagerBase
    {
        public string Url { get; set; }
        //페이지 개수
        public int PageCount { get; set; }
        //게시글 개수 
        public int RecordCount { get; set; }
        //페이지당 게시글 개수
        public int PageSize { get; set; }
        //현재 페이지 위치
        public int PageIndex { get; set; }
        //현재 페이지 위치 (PageIndex + 1)
        public int PageNumber { get; set; }
        //한 화면에 보이는 최대 버튼 개수
        public int PagerButtonCount { get; set; }
        public bool SearchMode { get; set; }
        public string SearchField { get; set; }
        public string SearchQuery { get; set; }
    }
}
