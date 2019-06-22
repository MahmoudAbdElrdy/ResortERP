using System;

namespace ResortERP.Core.VM
{
    public class EmployeeTypesVM
    {
        public int EMP_TYPE_ID { get; set; }
        public string EMP_TYPE_CODE { get; set; }
        public string EMP_TYPE_AR_NAME { get; set; }
        public string EMP_TYPE_EN_NAME { get; set; }
        public string AddedBy { get; set; }
        public Nullable<System.DateTime> AddedOn { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> updatedOn { get; set; }
        public Nullable<bool> Disable { get; set; }
    }
}
