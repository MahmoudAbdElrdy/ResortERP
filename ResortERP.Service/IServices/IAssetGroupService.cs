using ResortERP.Core;
using ResortERP.Core.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortERP.Service.IServices
{
   public interface IAssetGroupService
    {
        Task<List<AssetGroupVM>> getAssetGroupAsync(int pageNum, int pageSize);
        Task<int> insertAssetGroupSync(AssetGroupVM entity);
        Task<bool> updateAssetGroupSync(AssetGroupVM entity);
        Task<bool> deleteAssetGroupSync(AssetGroupVM entity);
        Task<int> countGroupAssetAsync();
        string GetLastCode();
       
        Task<AssetGroupVM> getById(int ID);
        Task<List<AssetGroupVM>> getAll();
    }
}
