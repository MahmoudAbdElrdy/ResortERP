using ResortERP.Core.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortERP.Service.IServices
{
    public interface IAssetCompanyDetailsService
    {

        bool Insert(AssetCompanyDetailsVM entity);
        Task<int> InsertAsync(AssetCompanyDetailsVM entity);
        bool Update(AssetCompanyDetailsVM entity);
        Task<bool> UpdateAsync(AssetCompanyDetailsVM entity);

        bool Delete(AssetCompanyDetailsVM entity);
        Task<bool> DeleteAsync(AssetCompanyDetailsVM entity);
        Task<List<AssetCompanyDetailsVM>> GetAllAsync(int pageNum, int pageSize);
        Task<int> CountAsync();
        string GetLastCode();
        Task<AssetCompanyDetailsVM> getById(int id);
    }
}
