using ResortERP.Core.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortERP.Service.IServices
{
    public interface ICostCalculationTypeService
    {

        bool Insert(CostCalculationTypeVM entity);
        Task<bool> InsertAsync(CostCalculationTypeVM entity);
        bool Update(CostCalculationTypeVM entity);
        Task<bool> UpdateAsync(CostCalculationTypeVM entity);
        bool Delete(CostCalculationTypeVM entity);
        Task<bool> DeleteAsync(CostCalculationTypeVM entity);
        Task<List<CostCalculationTypeVM>> GetAllAsync(int pageNum, int pageSize);
        Task<int> CountAsync();
        CustomItemUnitsVM GetUnitDataUsingUnitCode(string UnitCode);
        Task<List<CostCalculationTypeVM>> GetAllgetCostCalculationType();

        
    }
}
