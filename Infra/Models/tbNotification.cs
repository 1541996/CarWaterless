using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infra.Models
{
    [Table("tbNotification")]
    public partial class tbNotification
    {
        [Key]
        public int Id { get; set; }
        public Nullable<int> CustomerId { get; set; }
        public Nullable<int> OperationId { get; set; }
        public string NotiMessage { get; set; }
        public string MessageBody { get; set; }
        public Nullable<System.DateTime> MessageSendDateTime { get; set; }
        public Nullable<bool> IsSeen { get; set; }
        public string ResponseType { get; set; }
        public string NotiType { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
    }
}
