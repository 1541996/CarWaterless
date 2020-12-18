using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Models
{
    [Table("tbAdmin")]
    public partial class tbPhoto
    {
        [Key]
        public int ID { get; set; }
        public string Photo { get; set; }
        public string PhotoUrl
        {
            get
            {
                if (this.Photo != null)
                {
                    return string.Format("https://portalvhdslvb28rs1c3tmc.blob.core.windows.net/yammo/careme/{0}", Photo);
                }
                return "https://kktstroage.azureedge.net/yammo/careme/knowledge.png";

            }


        }
        
        public Nullable<bool> IsDeleted { get; set; }
        public string Type { get; set; }
        public int CarID { get; set; }
        public DateTime? Accesstime { get; set; }

    }
}
