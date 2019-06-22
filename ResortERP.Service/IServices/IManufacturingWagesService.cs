using ResortERP.Core.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortERP.Service.IServices
{
    public interface IManufacturingWagesService
    {

        bool Insert(ManufacturingWagesVM entity);
        Task<int> InsertAsync(ManufacturingWagesVM entity);
        bool Update(ManufacturingWagesVM entity);
        Task<bool> UpdateAsync(ManufacturingWagesVM entity);

        bool Delete(ManufacturingWagesVM entity);
        Task<bool> DeleteAsync(ManufacturingWagesVM entity);
        Task<List<ManufacturingWagesVM>> GetAllAsync(int pageNum, int pageSize);
        Task<int> CountAsync();
        string GetLastCode();

        Task<ManufacturingWagesVM> getById(int ID);
     //   CustomItemmanufacturingWagesVM GetUnitDataUsingUnitCode(string UnitCode);

    }
}
