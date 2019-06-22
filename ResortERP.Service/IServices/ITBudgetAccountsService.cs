using ResortERP.Core.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortERP.Service.IServices
{
    public interface ITBudgetAccountsService
    {
        bool Insert(TBudgetAccountsVM entity);
        Task<bool> InsertAsync(TBudgetAccountsVM entity);
        bool Update(TBudgetAccountsVM entity);
        Task<bool> UpdateAsync(TBudgetAccountsVM entity);
        bool Delete(TBudgetAccountsVM entity);
        Task<bool> DeleteAsync(TBudgetAccountsVM entity);
        List<TBudgetAccountsVM> GetAll();
        Task<List<TBudgetAccountsVM>> GetAllAsync(int pageNum, int pageSize);
        Task<int> CountAsync();
        Task<List<TBudgetAccountsVM>> GetByUID(string userName);
    }
}
