using ResortERP.Core.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortERP.Service.IServices
{
    public interface ICompanyStoresItemGroupsService
    {
        bool Insert(CompStore_ItmGroupsVM entity);
        Task<bool> InsertAsync(CompStore_ItmGroupsVM entity);
        bool Update(CompStore_ItmGroupsVM entity);
        Task<bool> UpdateAsync(CompStore_ItmGroupsVM entity);
        bool Delete(CompStore_ItmGroupsVM entity);
        Task<bool> DeleteAsync(CompStore_ItmGroupsVM entity);
        Task<List<CompStore_ItmGroupsVM>> GetAllAsync(int pageNum, int pageSize, int CompnyStoreID);
        List<CompStore_ItmGroupsVM> GetAll(int CompnyStoreID);
        Task<int> CountAsync(int CompnyStoreID);

    }
}
