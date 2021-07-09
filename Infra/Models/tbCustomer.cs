using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infra.Models
{
    [Table("tbCustomer")]
    public partial class tbCustomer
    {
        [Key]
        public int Id { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string PhoneNo { get; set; }
        public string FacebookId { get; set; }
        public string UserAppId { get; set; }
        public string Photo { get; set; }
        public Nullable<System.DateTime> LastLoginTime { get; set; }
        public Nullable<bool> IsSpecial { get; set; }
        public string RegisterStatus { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public Nullable<bool> IsMember { get; set; }
        public string MemberCode { get; set; }
        public Nullable<System.DateTime> MemberStartDate { get; set; }
        public Nullable<System.DateTime> MemberExpireDate { get; set; }
        public string BookingStatus { get; set; }
        public bool? IsPrepaid { get; set; }
        public decimal? PrepaidLeftAmount { get; set; }
        public decimal? PrepaidAmount { get; set; }
        public string UserToken { get; set; }
        public string ReturnStatus { get; set; }
        public string ReturnMessage { get; set; }
        //public bool? IsLogin { get; set; }
        public string PhotoUrl
        {
            get
            {
                if (this.Photo != null)
                {
                    return string.Format("http://filestorage.centurylinks-stock.com/ImageStorage/CarWaterlessProject/Customer/{0}", Photo);
                }
                return "http://ecowash.centurylinks-stock.com/Plesirthemes/img/useravator.png";

            }
        }

    }
}
