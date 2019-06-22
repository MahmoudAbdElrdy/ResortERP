using ResortERP.Core;
using ResortERP.Core.VM;
using SolversTeamERP.Core.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortERP.Service.IServices
{
   public interface IAssetOperationMasterService
    {
        Task<List<AssetOperationMasterVM>> GetAllAsync(int pageNum, int pageSize, int operationType);
        Task<int> CountAsync(int operationType);
        Task<int> InsertAsync(AssetOperationMasterVM entity);
        Task<bool> UpdateAsync(AssetOperationMasterVM entity);
        Task<bool> DeleteAsync(AssetOperationMasterVM entity);
        string GetLastCode();
        Task<List<AssetMasterDropDownMenu>> getAssetMasterList();
        Task<List<DropDownMenuOptionsVM>> getCostCenterList();
        Task<List<DropDownMenuOptionsVM>> getDepartmentList();
        Task<List<DropDownMenuOptionsVM>> getCompanyList();
        Task<List<DropDownMenuOptionsVM>> getCurrencyList();
        Task<List<DropDownMenuOptionsVM>> getAccountList();
        Task<List<DropDownMenuOptionsVM>> getEmployeeList();
        bool CheckNameAndCode(int Id, string number, string code);
        Task<bool> CheckOperationForDeletion(int Id);
        Task<List<EmployeeAssetsVM>> getEmployeeAssets(int employeeId);
    }
}
