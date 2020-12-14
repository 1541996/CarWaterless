using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infra.Models
{
    [Table("tbTopCustomer")]
    public partial class tbTopCustomer
    {
        [Key]
        public int Id { get; set; }
        public Nullable<int> Month { get; set; }
        public Nullable<int> Year { get; set; }
        public string VehicleNo { get; set; }
        public Nullable<int> CustomerId { get; set; }
        public Nullable<int> RankId { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public Nullable<int> CreateUserId { get; set; }
        public string UserAppId { get; set; }
    }
}
