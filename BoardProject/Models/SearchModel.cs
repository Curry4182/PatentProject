using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardProject.Models
{
    /// <summary>
    /// SearchBox 컴포넌트의 ui 쪽에서 값을 입력받으면 함수를 콜백하는데 그 콜백 함수에 입력값의 타입이다. 
    /// </summary>
    public class SearchModel
    {
        [Required]
        public string SearchStr { get; set; }

        [Required]
        public SearchOption Option{ get; set; }
    }

    [Flags]
    public enum SearchOption
    {
        TitleSearch = 0,
        ContentSearch = 1,
        UserSearch = 2
    }
}
