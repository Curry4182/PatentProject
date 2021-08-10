using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Patent.Models
{
    public interface IFileUpload
    {
        public Task FileOnChange(InputFileChangeEventArgs eventArgs);
        public Task OnUploadFile();
        public string RecentUploadFilePath { get; set; }
        public string FileName { get; set; }
    }
}
