using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortERP.Core.VM
{
    public class CostCentersGuidVM
    {
        public int id { get; set; }
        public string text { get; set; }
        public Nullable<int> PARENT_ACC_ID { get; set; }
        public CostCentersDataGuidVM data { get; set; }
        public List<CostCentersGuidVM> children { get; set; }

    }

    public class CostCentersDataGuidVM
    {
        public int data { get; set; }
    }
  
}
