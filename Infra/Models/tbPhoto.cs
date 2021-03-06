﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Models
{
    [Table("tbPhoto")]
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
                    return string.Format("http://filestorage.centurylinks-stock.com/ImageStorage/CarWaterlessProject/CustomerVehicle/{0}", Photo);
                }
                return "http://ecowash.centurylinks-stock.com/ArchitectThemes/image/imageplaceholder.png";

            }
        }
        
        public Nullable<bool> IsDeleted { get; set; }
        public string Type { get; set; }
        public int CarID { get; set; }
        public DateTime? Accesstime { get; set; }

    }
}
