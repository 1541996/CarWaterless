using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infra.Models
{
    [Table("tbAdditionalService")]
    public partial class tbAdditionalService
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public Nullable<int> CreateUserId { get; set; }
        public Nullable<System.DateTime> UpdateDate { get; set; }
        public Nullable<int> UpdateUserId { get; set; }
        public Nullable<decimal> Price { get; set; }
        public Nullable<decimal> DiscountPrice { get; set; }


    }
}
