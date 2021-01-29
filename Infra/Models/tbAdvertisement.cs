using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Infra.Models
{
    [Table("tbAdvertisement")]
    public class tbAdvertisement
    {
        [Key]
        public int ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? Accesstime { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }     
        public string Type { get; set; }
        public string Photo { get; set; }
        public string DisplayText { get; set; }
        public string WebLink { get; set; }
        public DateTime? PostedDate { get; set; }
        public bool? IsDeleted { get; set; }
        public string Duration { get; set; }
        public bool? IsGif { get; set; }
        public string PhotoUrl
        {
            get
            {
                if (this.Photo != null)
                {
                    return string.Format("http://filestorage.centurylinks-stock.com/ImageStorage/CarWaterlessProject/Advertisement/{0}", Photo);
                }
                return "http://ecowash.centurylinks-stock.com/ArchitectThemes/image/imageplaceholder.png";
            }
        }
    }
}