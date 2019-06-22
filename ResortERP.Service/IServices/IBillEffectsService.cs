using ResortERP.Core.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortERP.Service.IServices
{
    public interface IBillEffectsService
    {
        bool Insert(BILL_EFFECTSVM entity);
        Task<bool> InsertAsync(BILL_EFFECTSVM entity);
        bool Update(BILL_EFFECTSVM entity);
        Task<bool> UpdateAsync(BILL_EFFECTSVM entity);

        bool Delete(BILL_EFFECTSVM entity);
        Task<bool> DeleteAsync(BILL_EFFECTSVM entity);
        Task<List<BILL_EFFECTSVM>> GetAllAsync(int pageNum, int pageSize);
        Task<int> CountAsync();
    }
}
