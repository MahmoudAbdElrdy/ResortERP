using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortERP.Core.VM
{
  public class EntryMasterDetails
    {
        public Entry_MasterVM EntryMaster { get; set; }
        public List<Entry_DetailsVM> EntryDetails { get; set; }

    }
}
