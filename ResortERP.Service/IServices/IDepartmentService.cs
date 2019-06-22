using ResortERP.Core;
using ResortERP.Core.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortERP.Service.IServices
{
    public interface IDepartmentService
    {
        bool Insert(DepartmentVM entity);
        Task<int> InsertAsync(DepartmentVM entity);
        bool Update(DepartmentVM entity);
        Task<bool> UpdateAsync(DepartmentVM entity);
        bool Delete(DepartmentVM entity);
        Task<bool> DeleteAsync(DepartmentVM entity);
        Task<List<DepartmentVM>> GetAllAsync(int pageNum, int pageSize);
        Task<int> CountAsync();
        List<DepartmentVM> GetAll();
        string GetLastCode();
        List<Department> getDeptByBran(int compBranId);
        DepartmentVM getById(int DEPT_ID);
    }
}
