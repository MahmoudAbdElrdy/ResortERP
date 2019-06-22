using ResortERP.Core.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortERP.Service.IServices
{
    public interface IAcountsTypesService
    {
        bool Insert(AccountTypeVM entity);
        Task<int> InsertAsync(AccountTypeVM entity);
        bool Update(AccountTypeVM entity);
        Task<bool> UpdateAsync(AccountTypeVM entity);
        bool Delete(AccountTypeVM entity);
        Task<bool> DeleteAsync(AccountTypeVM entity);
        Task<List<AccountTypeVM>> GetAllAsync(int pageNum, int pageSize);
        Task<int> CountAsync();
        List<AccountTypeVM> GetAll();
        List<AccountTypeVM> GetCustomerSupplier(char type);
        List<AccountTypeVM> GetByTypeQueryString(char type);
    }
}
