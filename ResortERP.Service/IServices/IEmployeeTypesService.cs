using ResortERP.Core.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortERP.Service.IServices
{
    public interface IEmployeeTypesService
    {
        bool Insert(EmployeeTypesVM entity);
        Task<int> InsertAsync(EmployeeTypesVM entity);
        bool Update(EmployeeTypesVM entity);
        Task<bool> UpdateAsync(EmployeeTypesVM entity);
        bool Delete(EmployeeTypesVM entity);
        Task<bool> DeleteAsync(EmployeeTypesVM entity);
        Task<List<EmployeeTypesVM>> GetAllAsync(int pageNum, int pageSize);
        Task<int> CountAsync();
        List<EmployeeTypesVM> GetAll();
        EmployeeTypesVM getById(int EMP_TYPE_ID);
        string GetLastCode();
    }
}
