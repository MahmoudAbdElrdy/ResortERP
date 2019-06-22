using System;
using System.ComponentModel.DataAnnotations;

namespace ResortERP.Core
{
    public class Message
    {
        [Key]
        public int ID { get; set; }
        public string Code { get; set; }
        public int? FromUserID { get; set; }
        public int? ToUserID { get; set; }
        public int? AssignedToUserID { get; set; }
        public Nullable<System.DateTime> CreationDate { get; set; }
        public string ARSubject { get; set; }
        public string LatSubject { get; set; }
        public string TranslatedSubject { get; set; }
        public string ARBody { get; set; }
        public string LatBody { get; set; }
        public string TranslatedBody { get; set; }
        public bool? IsRead { get; set; }
        public bool? IsReceived { get; set; }
        public bool? IsSent { get; set; }
        public bool? IsStar { get; set; }
        public bool? IsTrash { get; set; }
        public bool? IsDraft { get; set; }
        public bool? IsTranslated { get; set; }
        public int? MessageTypeID { get; set; }
        public Nullable<System.DateTime> ReadDate { get; set; }
        public string Notes { get; set; }
        public Nullable<int> AddedBy { get; set; }
        public Nullable<System.DateTime> AddedOn { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public Nullable<Boolean> Active { get; set; }
        public Nullable<int> Position { get; set; }
    }
}
