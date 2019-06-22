using System;

namespace ResortERP.Core.VM
{
    public class DepartmentVM
    {
        public int DEPT_ID { get; set; }
        public string DEPT_CODE { get; set; }
        public string DEPT_AR_NAME { get; set; }
        public string DEPT_EN_NAME { get; set; }
        public string DEPT_AR_ABRV { get; set; }
        public string DEPT_EN_ABRV { get; set; }
        public Nullable<int> COM_BRN_ID { get; set; }
        public string AddedBy { get; set; }
        public Nullable<System.DateTime> AddedOn { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> updatedOn { get; set; }
        public Nullable<bool> Disable { get; set; }
    }
}
