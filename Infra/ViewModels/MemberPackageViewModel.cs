using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Infra.ViewModels
{
    public class MemberPackageViewModel
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string CarType { get; set; }
        public int? SortOrder { get; set; }
        public string AdditionalServiceIds { get; set; }
        public string AdditionalServiceNames { get; set; }
        public decimal? PackagePrice { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? CreateDate { get; set; }
        public int? CreateUserId { get; set; }
        public DateTime? UpdateDate { get; set; }
        public int? UpdateUserId { get; set; }
        public string Photo { get; set; }

        public int No { get; set; }
        public string Message { get; set; }
        public int MessageType { get; set; }
        public SelectList AdditionalServiceList { get; set; }
    }
}
