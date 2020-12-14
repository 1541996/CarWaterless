using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infra.Models
{
    [Table("tbFinance")]
    public partial class tbFinance
    {
        [Key]
        public int Id { get; set; }
        public Nullable<int> Month { get; set; }
        public Nullable<int> Year { get; set; }
        public Nullable<int> TotalCars { get; set; }
        public Nullable<int> TotalCustomers { get; set; }
        public string TotalNetAmount { get; set; }
        public Nullable<System.DateTime> CalculateDate { get; set; }
        public Nullable<bool> IsLatest { get; set; }
        public Nullable<int> CreateUserId { get; set; }
    }
}
