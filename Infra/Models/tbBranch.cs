using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infra.Models
{
    [Table("tbBranch")]
    public partial class tbBranch
    {
        [Key]
        public int Id { get; set; }
        public Nullable<int> TownshipId { get; set; }
        public string LocationName { get; set; }
        public string LocationPhoneNo { get; set; }
        public string LocationAddress { get; set; }
        public Nullable<int> AdminAgentId { get; set; }
        public string OpenTime { get; set; }
        public string CloseTime { get; set; }
        public Nullable<int> CarLimit { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public Nullable<int> CreateUserId { get; set; }
        public Nullable<System.DateTime> UpdateDate { get; set; }
        public Nullable<int> UpdateUserId { get; set; }

    }
}
