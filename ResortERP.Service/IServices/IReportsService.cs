using ResortERP.Core;
using ResortERP.Core.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SolversTeamERP.Core.VM;

namespace ResortERP.Service.IServices
{
   public interface IReportsService
    {
        Task<List<DropDownMenuOptionsVM>> getBranchesList();
        Task<List<DropDownMenuOptionsVM>> getCostCenterList();
        Task<List<DropDownMenuOptionsVM>> getStoresList();
        Task<List<DropDownMenuOptionsVM>> getItemGroupList();
        Task<List<DropDownMenuOptionsVM>> getSellTypeList();
        Task<ItemsInventoryReportResultVM> getItemsInventoryReportResult(ItemsInventoryReportSearchVM searchObject);
        Task<ItemsInventoryReportResultVM> getItemsInventoryBalanceResult(ItemsInventoryReportSearchVM searchObject);
        Task<AccountBalancesReportResultVM> getAccountBalancesReportResult(AccountBalancesReportSearchVM searchObject);
        Task<List<DropDownMenuOptionsVM>> getAccountsByType(string type);
    }
}
