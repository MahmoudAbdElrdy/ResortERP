using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortERP.Core.VM
{
   public class BankVM
    {
        public int ID { get; set; }
        public int Code { get; set; }
        public string ArName { get; set; }
        public string LatName { get; set; }
        public int? CurrencyID { get; set; }
        public string Notes { get; set; }
        public string AccountNo { get; set; }
        public string AddedBy { get; set; }
        public DateTime? AddedOn { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public bool Disable { get; set; }
        public short? NationID { get; set; }
        public short? GovID { get; set; }
        public short? TownID { get; set; }
        public short? VillageID { get; set; }
        public string CurrencyName { get; set; }
        public string AddressNotes { get; set; }

    }
}
