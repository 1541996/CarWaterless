using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.ViewModels
{
    public class ExpoNotiViewModel
    {
        public string to { get; set; }
        public string sound { get; set; } = "default";
        public string title { get; set; }
        public string body { get; set; }
        public data data { get; set; }
    }


   public class data
   {
        public string someData { get; set; }
    }
}
