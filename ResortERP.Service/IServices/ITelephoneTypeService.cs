using ResortERP.Core.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortERP.Service.IServices
{
    public interface ITelephoneTypeService
    {
        bool Insert(TelephoneTypesVM entity);
        Task<int> InsertAsync(TelephoneTypesVM entity);
        bool Update(TelephoneTypesVM entity);
        Task<bool> UpdateAsync(TelephoneTypesVM entity);
        bool Delete(TelephoneTypesVM entity);
        Task<bool> DeleteAsync(TelephoneTypesVM entity);
        Task<List<TelephoneTypesVM>> GetAllAsync(int pageNum, int pageSize);
        Task<int> CountAsync();
        List<TelephoneTypesVM> GetAll();
       // TelephoneTypesVM GetByID(int TypeID);
    }
}
