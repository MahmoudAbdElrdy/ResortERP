using ResortERP.Core.VM;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ResortERP.Service.IServices
{
    public interface IBillTypesService
    {
        bool Insert(BILL_TYPESVM entity);
        Task<bool> InsertAsync(BILL_TYPESVM entity);
        bool Update(BILL_TYPESVM entity);
        Task<bool> UpdateAsync(BILL_TYPESVM entity);
        bool Delete(BILL_TYPESVM entity);
        Task<bool> DeleteAsync(BILL_TYPESVM entity);
        List<BILL_TYPESVM> GetAll();
        Task<List<BILL_TYPESVM>> GetAllAsync(int pageNum, int pageSize);
        Task<int> CountAsync();
    }
}
