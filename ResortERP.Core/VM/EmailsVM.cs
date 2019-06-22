using System;

namespace ResortERP.Core.VM
{
    public class EmailsVM
    {
        public int id { get; set; }
        public int UID_ID { get; set; }
        public string Email { get; set; }
        public string AddedBy { get; set; }
        public Nullable<System.DateTime> AddedOn { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> updatedOn { get; set; }
    }
}
