using ResortERP.Core;
using ResortERP.Core.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortERP.Service.IServices
{
    public interface IEntryDetailsService
    {
        long Insert(Entry_DetailsVM entity);
        Task<long> InsertAsync(Entry_DetailsVM entity);
        bool Update(Entry_DetailsVM entity);
        Task<bool> UpdateAsync(Entry_DetailsVM entity);

        bool Delete(Entry_DetailsVM entity);
        Task<bool> DeleteAsync(Entry_DetailsVM entity);
        Task<List<Entry_DetailsVM>> GetAllAsync(int pageNum, int pageSize);
        Task<int> CountAsync();
        List<Entry_DetailsVM> GetAll();
        List<AccountVM> SearchItems(string SearchCriteria);
        List<ENTRY_DETAILS> getByCostCenter(int costCenterId);
        Task<object> getAccountValByaccID(int accId);
        Task<object> getAccountValByaccIDandBranchID(int accId,string BranchID);

        
    }
}
