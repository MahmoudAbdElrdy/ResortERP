using System;

namespace ResortERP.Core.VM
{
    public class Entry_Show_OptionsVM
    {
        public int ENTRY_SHOW_ID { get; set; }
        public string ENTRY_SHOW_AR_NAME { get; set; }
        public string ENTRY_SHOW_EN_NAME { get; set; }
        public string ENTRY_SHOW_ABRV_AR { get; set; }
        public string ENTRY_SHOW_ABRV_EN { get; set; }
        public string AddedBy { get; set; }
        public Nullable<System.DateTime> AddedOn { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
    }
}
