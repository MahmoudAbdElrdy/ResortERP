using System;
using System.ComponentModel.DataAnnotations;

namespace ResortERP.Core
{
    public class NotificationsView
    {
        [Key]
        public long NID { get; set; }
        public int NotificationID { get; set; }
        public int? FromUserID { get; set; }
        public string FromUserARFullName { get; set; }
        public string FromUserLatFullName { get; set; }
        public int? ToUserID { get; set; }
        public string ToUserARFullName { get; set; }
        public string ToUserLatFullName { get; set; }
        public int? NotificationTypeID { get; set; }
        public int? NotifcationCategoryID { get; set; }

        public string NotificationTypeIconURL { get; set; }
        public bool? IsRead { get; set; }
        public bool? IsReceived { get; set; }
        public bool? IsTranslated { get; set; }
        public bool? IsConfirmed { get; set; }
        public bool? RequiredConfirmation { get; set; }
        public Nullable<System.DateTime> CreationDate { get; set; }
        public string ARBody { get; set; }
        public string LatBody { get; set; }
        public string Notes { get; set; }
        public Nullable<Boolean> Active { get; set; }
        public Nullable<int> Position { get; set; }

    }
}
