using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infra.Models
{
    [Table("tbServicePackage")]
    public partial class tbServicePackage
    {
        [Key]
        public int Id { get; set; }
        public Nullable<int> CarCategoryId { get; set; }
        public Nullable<int> ServiceCategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Nullable<decimal> RegularPrice { get; set; }
        public Nullable<decimal> SpecialPrice { get; set; }
        public string Duration { get; set; }
        public string Photo { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public Nullable<int> CreateUserId { get; set; }
        public Nullable<System.DateTime> UpdateDate { get; set; }
        public Nullable<int> UpdateUserId { get; set; }
    }
}
