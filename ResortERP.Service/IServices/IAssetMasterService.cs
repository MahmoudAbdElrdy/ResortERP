using ResortERP.Core;
using ResortERP.Core.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortERP.Service.IServices
{
   public interface IAssetMasterService
    {
        Task<List<AssetMasterVM>> GetAllAsync(int pageNum, int pageSize);
        Task<int> CountAsync();
        Task<int> InsertAsync(AssetMasterVM entity);
        Task<bool> UpdateAsync(AssetMasterVM entity);
        Task<bool> DeleteAsync(AssetMasterVM entity);
        string GetLastCode();
        Task<List<DropDownMenuOptionsVM>> getAssetTypeList();
        Task<List<DropDownMenuOptionsVM>> getAssetGroupList();
        Task<List<DropDownMenuOptionsVM>> getAssetStatusList();
        Task<List<DropDownMenuOptionsVM>> getOriginNationList();
        Task<List<DropDownMenuOptionsVM>> getCompanyList();
        Task<List<DropDownMenuOptionsVM>> getAssetDepreciationTypeList();
        Task<List<DropDownMenuOptionsVM>> getAssetLifeSpanUnitList();
        Task<List<DropDownMenuOptionsVM>> getCurrencyList();
        Task<List<DropDownMenuOptionsVM>> getAccountList();
        Task<List<DropDownMenuOptionsVM>> getDepartmentList();
        Task<AssetDepreciationDetailsVM> getAssetDepreciationDetails(int assetMasterId);
        bool CheckNameAndCode(int Id, string aRName, string latName, string code);
        Task<bool> CheckAssetMasterForDeletion(int Id);
    }
}
