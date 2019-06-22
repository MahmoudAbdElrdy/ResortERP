using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolversTeamERP.Core.VM
{
    public class EmployeeAssetsVM
    {
        public int AssetMasterID { get; set; }
        public int FromEmployeeID { get; set; }
        public string AssetName { get; set; }
        public int assignedAssetOperationId { get; set; }
    }
}
