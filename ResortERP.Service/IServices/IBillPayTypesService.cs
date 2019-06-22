using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ResortERP.Core;
using ResortERP.Core.VM;

namespace ResortERP.Service.IServices
{
    public interface IBillPayTypesService
    {
        Task<int> CountAsync();
        Task<bool> DeleteAsync(BILL_PAY_TYPESVM entity);
        Task<List<BILL_PAY_TYPESVM>> GetAllAsync(int pageNum, int pageSize);
        Task<bool> InsertAsync(BILL_PAY_TYPESVM entity);
        Task<bool> UpdateAsync(BILL_PAY_TYPESVM entity);
        Task<List<BILL_PAY_TYPESVM>> getByMasterID(int masterID);

        Task<bool> InsertWithMasterID(List<BILL_PAY_TYPESVM> payTypeList);
        bool DeleteByMasterID(int? MasterID);
    }
}
