using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.ViewModels
{
    public class DailyHotViewModel
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string Photo { get; set; }

        public int No { get; set; }
        public string Message { get; set; }
        public int MessageType { get; set; }
    }
}
