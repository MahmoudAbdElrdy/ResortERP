using ResortERP.Core.VM;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ResortERP.Service.IServices
{
    public interface ITBoxAccountsService
    {
        bool Insert(TBoxAccountsVM entity);
        Task<bool> InsertAsync(TBoxAccountsVM entity);
        bool Update(TBoxAccountsVM entity);
        Task<bool> UpdateAsync(TBoxAccountsVM entity);
        bool Delete(TBoxAccountsVM entity);
        Task<bool> DeleteAsync(TBoxAccountsVM entity);
        List<TBoxAccountsVM> GetAll();
        Task<List<TBoxAccountsVM>> GetAllAsync(int pageNum, int pageSize);
        Task<List<TBoxAccountsVM>> GetByUID(string userName);
        Task<int> CountAsync();
    }
}
