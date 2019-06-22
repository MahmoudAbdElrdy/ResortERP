using System;
using System.ComponentModel.DataAnnotations;

namespace ResortERP.Core
{
    public class Notification
    {
        [Key]
        public int ID { get; set; }
        public int? FromUserID { get; set; }
        public int? ToUserID { get; set; }
        public Nullable<System.DateTime> CreationDate { get; set; }
        public string ARBody { get; set; }
        public string LatBody { get; set; }
        public string TranslatedBody { get; set; }
        public bool? IsRead { get; set; }
        public bool? IsReceived { get; set; }
        public bool? IsTranslated { get; set; }
        public bool? IsConfirmed { get; set; }
        public bool? RequiredConfirmation { get; set; }
        public int? NotificationTypeID { get; set; }
        public int? NotifcationCategoryID { get; set; }
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
