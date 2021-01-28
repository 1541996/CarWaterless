using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infra.Models
{
    [Table("tbChatMessage")]
    public partial class tbChatMessage
    {
        [Key]
        public int Id { get; set; }
        public string UserID { get; set; }
        public string UserName { get; set; }
        public string UserToken { get; set; }
        public int OperationID { get; set; }
        public Nullable<System.DateTime> SendDateTime { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<bool> IsConversationEnd { get; set; }
        public string Message { get; set; }
        public string Photo { get; set; }
        public Nullable<System.DateTime> OperationDate { get; set; }
        public string Type { get; set; }
        //public string PhotoUrl
        //{
        //    get
        //    {
        //        if (this.Photo != null)
        //        {
        //            return string.Format("http://filestorage.centurylinks-stock.com/ImageStorage/CarWaterlessProject/Chat/{0}", Photo);
        //        }
        //        return "https://kktstroage.azureedge.net/yammo/careme/knowledge.png";

        //    }
        //}
    }
}
