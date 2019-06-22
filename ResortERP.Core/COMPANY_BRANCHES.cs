using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortERP.Core
{
    public class COMPANY_BRANCHES
    {
        [Key]
        public int COM_BRN_ID { get; set; }
        public string COM_BRN_CODE { get; set; }
        public string COM_BRN_AR_NAME { get; set; }
        public string COM_BRN_EN_NAME { get; set; }
        public string COM_BRN_AR_ABRV { get; set; }
        public string COM_BRN_EN_ABRV { get; set; }
        public short NATION_ID { get; set; }
        public short GOV_ID { get; set; }
        public Nullable<int> TOWN_ID { get; set; }
        public Nullable<long> VILLAGE_ID { get; set; }
        public string COM_BRN_ADDR_REMARKS { get; set; }
        public Nullable<int> COM_MASTER_BRN_ID { get; set; }
        public string COM_BRN_REMARKS { get; set; }
        public string AddedBy { get; set; }
        public Nullable<System.DateTime> AddedOn { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> updatedOn { get; set; }
        public Nullable<bool> Disable { get; set; }
        public int? CurrencyID { get; set; }
        public string ManagerName { get; set; }
        public bool? IsDefault { get; set; }
    }
}
