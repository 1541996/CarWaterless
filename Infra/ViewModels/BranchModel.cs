using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Infra.ViewModels
{
    public class BranchModel
    {
        public int Id { get; set; }
        public int? TownshipId { get; set; }
        public string LocationName { get; set; }
        public string LocationPhoneNo { get; set; }
        public string LocationAddress { get; set; }
        public string Photo { get; set; }
        public string MapHtml { get; set; }
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

        public int No { get; set; }
        public string Message { get; set; }
        public int MessageType { get; set; }
        public SelectList TownshipList { get; set; }
        public SelectList AdminAgentList { get; set; }
        public string TownshipName { get; set; }
        public string AdminAgentName { get; set; }
    }
}
