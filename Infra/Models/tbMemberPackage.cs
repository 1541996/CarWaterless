using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Models
{
    [Table("tbMemberPackage")]
    public class tbMemberPackage
    {
       [Key]
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
        public string PhotoUrl
        {
            get
            {
                if (this.Photo != null)
                {
                    return string.Format("http://filestorage.centurylinks-stock.com/ImageStorage/CarWaterlessProject/MemberPackage/{0}", Photo);
                }
                return "http://ecowash.centurylinks-stock.com/ArchitectThemes/image/imageplaceholder.png";

            }
        }
    }
}
