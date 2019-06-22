using ResortERP.Core.VM;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ResortERP.Service.IServices
{
    public interface ITStoreService
    {
        bool Insert(TStoreVM entity);
        Task<bool> InsertAsync(TStoreVM entity);
        bool Update(TStoreVM entity);
        Task<bool> UpdateAsync(TStoreVM entity);
        bool Delete(TStoreVM entity);
        Task<bool> DeleteAsync(TStoreVM entity);
        List<TStoreVM> GetAll();
        Task<List<TStoreVM>> GetAllAsync(int pageNum, int pageSize);
        Task<List<TStoreVM>> GetByUID(string userName);
        Task<int> CountAsync();
    }
}
