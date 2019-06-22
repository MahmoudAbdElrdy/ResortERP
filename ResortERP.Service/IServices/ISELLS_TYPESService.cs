using ResortERP.Core.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortERP.Service.IServices
{
    public interface ISELLS_TYPESService
    {
        bool Insert(SELLS_TYPESVM entity);
        Task<bool> InsertAsync(SELLS_TYPESVM entity);
        bool Update(SELLS_TYPESVM entity);
        Task<bool> UpdateAsync(SELLS_TYPESVM entity);
        bool Delete(SELLS_TYPESVM entity);
        Task<bool> DeleteAsync(SELLS_TYPESVM entity);
        Task<List<SELLS_TYPESVM>> GetAllAsync(int pageNum, int pageSize);
        Task<int> CountAsync();
        List<SELLS_TYPESVM> GetAll();
        SELLS_TYPESVM GetByID(int id);

    }
}
