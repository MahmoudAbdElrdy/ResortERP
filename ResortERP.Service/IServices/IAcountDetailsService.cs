using ResortERP.Core.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortERP.Service.IServices
{
    public interface IAcountDetailsService
    {
        bool Insert(AccountDetailVM entity);
        Task<int> InsertAsync(AccountDetailVM entity);
        bool Update(AccountDetailVM entity);
        Task<bool> UpdateAsync(AccountDetailVM entity);
        bool Delete(AccountDetailVM entity);
        Task<bool> DeleteAsync(AccountDetailVM entity);
        Task<List<AccountDetailVM>> GetAllAsync(int pageNum, int pageSize);
        Task<int> CountAsync();
        List<AccountDetailVM> GetAll();
    }
}
