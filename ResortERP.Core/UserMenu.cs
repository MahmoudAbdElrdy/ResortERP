using System;
using System.ComponentModel.DataAnnotations;

namespace ResortERP.Core
{
    public class UserMenu
    {
        [Key]
        public int ID { get; set; }
        public string Code { get; set; }
        public string ARName { get; set; }
        public string LatName { get; set; }
        public string MenuLink { get; set; }
        public string MenuClass { get; set; }
        public string IconClass { get; set; }
        public string IconImageURL { get; set; }
        public int? MenuID { get; set; }
        public int? UserShortcut { get; set; }
        public int? UserRoleID { get; set; }
        public int? LanguageID { get; set; }
        public int? MenuType { get; set; }
        public string ResourceURL { get; set; }
        public string ResourceContent { get; set; }
        public string Notes { get; set; }
        public string AddedBy { get; set; }
        public Nullable<System.DateTime> AddedOn { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public Nullable<Boolean> Active { get; set; }
        public Nullable<int> Position { get; set; }
        public Nullable<int> CountryID { get; set; }

        public int? DisplayOrNot { get; set; }
        public string FRName { get; set; }
        public string TRName { get; set; }
        public string URName { get; set; }

        public int? BillSetiingID { get; set; }
        public int? EntrySettingID { get; set; }
    }
}
