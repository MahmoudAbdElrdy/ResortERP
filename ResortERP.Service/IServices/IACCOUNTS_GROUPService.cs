using ResortERP.Core;
using ResortERP.Core.VM;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ResortERP.Service.IServices
{
    public interface IACCOUNTS_GROUPService
    {
        bool Insert(ACCOUNTS_GROUPVM entity);
        Task<int> InsertAsync(ACCOUNTS_GROUPVM entity);
        bool Update(ACCOUNTS_GROUPVM entity);
        Task<bool> UpdateAsync(ACCOUNTS_GROUPVM entity);

        bool Delete(ACCOUNTS_GROUPVM entity);
        Task<bool> DeleteAsync(ACCOUNTS_GROUPVM entity);
        Task<List<ACCOUNTS_GROUPVM>> GetAllAsync(int pageNum, int pageSize);
        Task<int> CountAsync();
        List<ACCOUNTS_GROUPVM> GetAllACCOUNTS_GROUP();
        Task<ACCOUNTS_GROUPVM> getACCOUNTS_GROUPByID(int GroupID);
        string GetLastCode();
        ACCOUNTS_GROUPVM GetByID(int itemID);

        //List<ItemsGuidVM> buildItemsTree();
    }
}