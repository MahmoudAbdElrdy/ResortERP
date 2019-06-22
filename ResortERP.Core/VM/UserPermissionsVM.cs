using System;

namespace ResortERP.Core.VM
{
    public class UserPermissionsVM
    {
        public int ID { get; set; }
        public int MenuID { get; set; }
        public int FormID { get; set; }
        public int UserOperationID { get; set; }
        public string MenuUrl { get; set; }

        public Nullable<bool> MenuOpen { get; set; }
        public Nullable<bool> OpAdd { get; set; }
        public Nullable<bool> OpUpdate { get; set; }
        public Nullable<bool> OpDelete { get; set; }
        public Nullable<bool> OpPreview { get; set; }
        public Nullable<bool> OpPrint { get; set; }
        public Nullable<bool> OpSearch { get; set; }
    }
}
