using Infra.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Infra.ViewModels
{
    public class VehicleCategoryViewModel
    {
        public tbCustomerVehicle vehicle { get; set; }
        public tbCarCategory category { get; set; }
        public tbPhoto photos { get; set; }
    }


    public class VehicleCategoryViewModelV2
    {
        public int VehicleId { get; set; }
        public string CustomerId { get; set; }
        public string VehicleBrand { get; set; }
        public string VehicleName { get; set; }
        public string CategoryName { get; set; }
        public string VehicleColor { get; set; }
        public string VehicleNo { get; set; }
        public string Photo { get; set; }
        public string PhotoUrl
        {
            get
            {
                if (this.Photo != null)
                {
                    return string.Format("http://filestorage.centurylinks-stock.com/ImageStorage/CarWaterlessProject/CustomerVehicle/{0}", Photo);
                }
                return "https://kktstroage.azureedge.net/yammo/careme/knowledge.png";

            }
        }


    }


    public class CarDDViewModel
    {
        public int carid { get; set; }
        public string carname { get; set; }
        public string carbrand { get; set; }
        public int carcategoryid { get; set; }
        public string carcategoryname { get; set; }
        public string carcategorytype { get; set; }

    }



    public class BookingViewModel
    {
        public string FullName { get; set; }
        public string VehicleBrand { get; set; }
        public string VehicleName { get; set; }
        public string VehicleColor { get; set; }
        public string CategoryName { get; set; }
        public string VehicleNo { get; set; }
        public string PhoneNo { get; set; }
        public int OperationId { get; set; }
        public string BookingStatus { get; set; }
        public string WashOption { get; set; }
        public string CustomerAddress { get; set; }
        public string CategoryType { get; set; }
        public decimal? CategoryBasicPrice { get; set; }
        public string AdditionalNames { get; set; }
        public string AdditionalPrices { get; set; }
        public int CustomerId { get; set; }
        public DateTime? OperationDate { get; set; }
        public DateTime? ConfirmedDate { get; set; }
        public decimal? TotalAmount { get; set; }
        public DateTime? CancelDate { get; set; }
        public DateTime? FinishedDate { get; set; }
        public string Email { get; set; }
        public string BookingPackage { get; set; }
        public string MemberPackage { get; set; }
        public string PaymentType { get; set; }
        public string ComplaintMessage { get; set; }
        public string Township { get; set; }
        public string Branch { get; set; }
        public string customername { get; set; }


    }

    public class ChatDataViewModel
    {
        public string vehiclename { get; set; }
        public string vehiclebrand { get; set; }
        public string vehicleno { get; set; }
        public string userid { get; set; }
        public string username { get; set; }
        public string lastmessage { get; set; }
        public DateTime? senddate { get; set; }
        public bool? isread { get; set; }
        public string type { get; set; }
        public string customername { get; set; }
    }


    public class BranchViewModel
    {
        public tbBranch branch { get; set; }
        public tbTownship township { get; set; }
        public int Id { get; set; }
        public int? TownshipId { get; set; }
        public string LocationName { get; set; }
        public string LocationPhoneNo { get; set; }
        public string LocationAddress { get; set; }
        public int? AdminAgentId { get; set; }
        public string OpenTime { get; set; }
        public string CloseTime { get; set; }
        public int? CarLimit { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? CreateDate { get; set; }
        public int? CreateUserId { get; set; }
        public DateTime? UpdateDate { get; set; }
        public int? UpdateUserId { get; set; }
        public string Photo { get; set; }
        public string MapHtml { get; set; }
       

        public int No { get; set; }
        public string Message { get; set; }
        public int MessageType { get; set; }
        public SelectList TownshipList { get; set; }
        public SelectList AdminAgentList { get; set; }
        public string TownshipName { get; set; }
        public string AdminAgentName { get; set; }
    }
}
