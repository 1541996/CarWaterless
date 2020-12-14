using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infra.Models
{
    [Table("tbDiscountedCar")]
    public partial class tbDiscountedCar
    {
        [Key]
        public int Id { get; set; }
        public string Type { get; set; }
        public Nullable<int> CustomerVehicleId { get; set; }
        public Nullable<int> OperationId { get; set; }
        public Nullable<bool> IsExpired { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public Nullable<int> CreateUserId { get; set; }
    }
}
