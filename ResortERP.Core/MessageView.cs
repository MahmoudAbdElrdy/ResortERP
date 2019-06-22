using System;
using System.ComponentModel.DataAnnotations;

namespace ResortERP.Core
{
    public class MessagesView
    {
        [Key]
        public long NID { get; set; }
        public int MessageID { get; set; }
        public int? FromUserID { get; set; }
        public string FromUserARFullName { get; set; }
        public string FromUserLatFullName { get; set; }
        public int? ToUserID { get; set; }
        public string ToUserARFullName { get; set; }
        public string ToUserLatFullName { get; set; }
        public Nullable<System.DateTime> CreationDate { get; set; }
        public string MessageARSubject { get; set; }
        public string MessageLatSubject { get; set; }
        public int? MessageTypeID { get; set; }
        public int? MessageCategoryID { get; set; }
        public string MessageTypeIconURL { get; set; }
        public bool? IsRead { get; set; }
        public bool? IsReceived { get; set; }
        public bool? IsSent { get; set; }
        public bool? IsStar { get; set; }
        public bool? IsTrash { get; set; }
        public bool? IsDraft { get; set; }
        public bool? IsTranslated { get; set; }
        public string MessageARBody { get; set; }
        public string MessageLatBody { get; set; }
        public string Notes { get; set; }
        public Nullable<Boolean> Active { get; set; }
        public Nullable<int> Position { get; set; }

    }
}
