using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infra.Models
{
    [Table("tbDailyHot")]
    public partial class tbDailyHot
    {
        [Key]
        public int ID { get; set; }      
        public string Title { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string Photo { get; set; }
        public string PhotoUrl
        {
            get
            {
                if (this.Photo != null)
                {
                    return string.Format("http://filestorage.centurylinks-stock.com/ImageStorage/CarWaterlessProject/DailyHot/{0}", Photo);
                }
                return "https://kktstroage.azureedge.net/yammo/careme/knowledge.png";

            }
        }


    }
}
