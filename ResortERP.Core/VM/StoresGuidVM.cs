using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortERP.Core.VM
{
    public class StoresGuidVM
    {
        public int id { get; set; }
        public string text { get; set; }
        public Nullable<int> PARENT_ACC_ID { get; set; }
        public StoresDataGuidVM data { get; set; }
        public List<StoresGuidVM> children { get; set; }

    }

    public class StoresDataGuidVM
    {
        public int data { get; set; }
    }
  
}
