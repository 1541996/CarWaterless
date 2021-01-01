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
        public string FromUserID { get; set; }
        public string FromUserName { get; set; }
        public string ToUserID { get; set; }
        public string ToUserName { get; set; }
        public string UserToken { get; set; }
        public int OperationID { get; set; }
        public Nullable<System.DateTime> SendDateTime { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<bool> IsConversationEnd { get; set; }
        public string Message { get; set; }
    }
}
