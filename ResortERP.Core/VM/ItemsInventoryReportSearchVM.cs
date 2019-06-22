using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolversTeamERP.Core.VM
{
    public class ItemsInventoryReportSearchVM
    {
        public int? GroupId { get; set; }
        public int StoreId { get; set; }
        public int CostCenterId { get; set; }
        public DateTime dateFrom { get; set; }
        public DateTime dateTo { get; set; }
        public DateTime? ExpireDate { get; set; }
        public int SellTypeId { get; set; }
        public int SortBy { get; set; }
        public bool ShowExpired { get; set; }
        public bool ShowStoreDetails { get; set; }
        public bool ShowZeroBalances { get; set; }
        public bool ShowZeroBalancesOnly { get; set; }
        public bool ShowNegativeBalancesOnly { get; set; }
    }
}
