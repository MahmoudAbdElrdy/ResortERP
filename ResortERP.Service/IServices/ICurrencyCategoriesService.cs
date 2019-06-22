using ResortERP.Core.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortERP.Service.IServices
{
    public interface ICurrencyCategoriesService
    {
        Task<List<CurrencyCategoriesVM>> getCatAsync(int pageNum, int pageSize);
        Task<bool> insertCatSync(CurrencyCategoriesVM entity);
        Task<bool> updateCatSync(CurrencyCategoriesVM entity);
        Task<bool> deleteCatSync(CurrencyCategoriesVM entity);
        Task<int> countCatAsync();
        List<CurrencyCategoriesVM> getbyCurrId(int currId);
        string getbyCurrIdCatId(int currId, int catId);
        string GetLastCode();
    }
}
