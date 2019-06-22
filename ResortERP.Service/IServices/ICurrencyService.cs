using ResortERP.Core.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortERP.Service.IServices
{
    public interface ICurrencyService
    {
        bool Insert(CurrencyVM entity);
        Task<int> InsertAsync(CurrencyVM entity);
        bool Update(CurrencyVM entity);
        Task<bool> UpdateAsync(CurrencyVM entity);
        bool Delete(CurrencyVM entity);
        Task<bool> DeleteAsync(CurrencyVM entity);
        Task<List<CurrencyVM>> GetAllAsync(int pageNum, int pageSize);
        Task<int> CountAsync();
        List<CurrencyVM> GetAll();
        string GetBy(int CurrencyID);
        Task<List<CurrencyVM>> GetSearchResultAsync(CurrencyVM entity, int pageNum, int pageSize);
        Task<string> GetCurrencyRate(int CurrencyID);
        string GetLastCode();
        Task<CurrencyVM> GetByCurrId(int CurrencyId);
    }
}
