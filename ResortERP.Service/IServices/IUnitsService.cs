using ResortERP.Core.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortERP.Service.IServices
{
    public interface IUnitsService
    {

        bool Insert(UNITSVM entity);
        Task<int> InsertAsync(UNITSVM entity);
        bool Update(UNITSVM entity);
        Task<bool> UpdateAsync(UNITSVM entity);

        bool Delete(UNITSVM entity);
        Task<bool> DeleteAsync(UNITSVM entity);
        Task<List<UNITSVM>> GetAllAsync(int pageNum, int pageSize);
        Task<int> CountAsync();
        CustomItemUnitsVM GetUnitDataUsingUnitCode(string UnitCode);
        string GetLastCode();
        Task<UNITSVM> getById(int id);
    }
}
