using ResortERP.Core.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortERP.Service.IServices
{
    public interface IBillShowOptionService
    {
        bool Insert(BILL_SHOW_OPTIONSVM entity);
        Task<bool> InsertAsync(BILL_SHOW_OPTIONSVM entity);
        bool Update(BILL_SHOW_OPTIONSVM entity);
        Task<bool> UpdateAsync(BILL_SHOW_OPTIONSVM entity);
        bool Delete(BILL_SHOW_OPTIONSVM entity);
        Task<bool> DeleteAsync(BILL_SHOW_OPTIONSVM entity);
        Task<List<BILL_SHOW_OPTIONSVM>> GetAllAsync(int pageNum, int pageSize);
        Task<int> CountAsync();
        List<BILL_SHOW_OPTIONSVM> GetAll();
    }
}
