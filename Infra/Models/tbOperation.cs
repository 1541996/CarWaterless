using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infra.Models
{
    [Table("tbOperation")]
    public partial class tbOperation
    {
        [Key]
        public int Id { get; set; }
        public string OperationCode { get; set; }
        public Nullable<int> CustomerId { get; set; }
        public Nullable<int> CustomerVehicleId { get; set; }
        public Nullable<int> BranchId { get; set; }
        public string BranchName { get; set; }
        public Nullable<int> TownshipId { get; set; }
        public string TownshipName { get; set; }
        public string CustomerAddress { get; set; }
        public DateTime? RequestDateTime { get; set; }
        public string AdditionalIds { get; set; }
        public string AdditionalNames { get; set; }
        public string AdditionalPrices { get; set; }
        public Nullable<System.DateTime> OperationDate { get; set; }
        public Nullable<System.DateTime> StartTime { get; set; }
        public Nullable<System.DateTime> EndTime { get; set; }
        public Nullable<decimal> TotalAmount { get; set; }
        public string Discount { get; set; }
        public Nullable<decimal> OtherCost { get; set; }
        public Nullable<decimal> NetAmount { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public Nullable<int> CreateUserId { get; set; }
        public string Status { get; set; }
        public string BookingSource { get; set; }
        public string StarRate { get; set; }
        public string Feedback { get; set; }
        public string ComplaintsMessage { get; set; }
        public int? CarCategoryId { get; set; }
        //public string CarCategoryName { get; set; }
        //public string CarCategoryType { get; set; }
        public string WashOption { get; set; }
        public DateTime? ConfirmedTime { get; set; }
        public DateTime? FinishedTime { get; set; }
        public DateTime? CancelTime { get; set; }
        public string BookingPackage { get; set; }
        public int? MemberPackageId { get; set; }
        public string MemberPackageName { get; set; }
        public string PaymentType { get; set; }
        public decimal? TransportationCharges { get; set; }


    }
}
