using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infra.Models
{
    [Table("tbCustomerVehicle")]
    public partial class tbCustomerVehicle
    {
        [Key]
        public int Id { get; set; }
        public string CustomerId { get; set; }
        public string CarCategoryId { get; set; }
        public string VehicleNo { get; set; }
        public string VehicleName { get; set; }
        public string VehicleBrand { get; set; }
        public string CarPhoto { get; set; }
        public string Status { get; set; }
        public string BookingStatus { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<int> CreateUserId { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public Nullable<System.DateTime> UpdateDate { get; set; }
    }
}
