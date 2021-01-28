using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.ViewModels
{
    public class FileUploadViewModel
    {
        public string filepath { get; set; }
        public string photo { get; set; }
    }


    public class ChatViewModel
    {
        public string userid { get; set; }
        public string type { get; set; }
        public int? operationid { get; set; }
        public string message { get; set; }
        public string file { get; set; }
        public string username { get; set; }

    }

}
