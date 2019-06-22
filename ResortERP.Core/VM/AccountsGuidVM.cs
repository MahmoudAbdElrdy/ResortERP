using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortERP.Core.VM
{
    public class AccountsGuidVM
    {
        public int id { get; set; }
        public string text { get; set; }
        public Nullable<int> PARENT_ACC_ID { get; set; }
        public AccountsDataGuidVM data { get; set; }
        public List<AccountsGuidVM> children { get; set; }
 
    }

    public class AccountsDataGuidVM
    {
        public int data { get; set; }
    }

}
