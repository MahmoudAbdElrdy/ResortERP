using ResortERP.Core.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortERP.Service.IServices
{
    public interface IBillOptionService
    {
        bool Insert(BILL_OPTIONSVM entity);
        Task<bool> InsertAsync(BILL_OPTIONSVM entity);
        bool Update(BILL_OPTIONSVM entity);
        Task<bool> UpdateAsync(BILL_OPTIONSVM entity);
        bool Delete(BILL_OPTIONSVM entity);
        Task<bool> DeleteAsync(BILL_OPTIONSVM entity);
        Task<List<BILL_OPTIONSVM>> GetAllAsync(int pageNum, int pageSize);
        Task<int> CountAsync();
        List<BILL_OPTIONSVM> GetAll();
    }
}
