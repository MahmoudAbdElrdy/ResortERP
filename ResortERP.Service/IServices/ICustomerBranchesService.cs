using ResortERP.Core.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortERP.Service.IServices
{
    public interface ICustomerBranchesService
    {
        bool Insert(CustomerBranchesVM entity);
        Task<bool> InsertAsync(CustomerBranchesVM entity);
        bool Update(CustomerBranchesVM entity);
        Task<bool> UpdateAsync(CustomerBranchesVM entity);

        bool Delete(CustomerBranchesVM entity);
        Task<bool> DeleteAsync(CustomerBranchesVM entity);
        Task<List<CustomerBranchesVM>> GetAllAsync(int pageNum, int pageSize);
        Task<int> CountAsync();
        List<CustomerBranchesVM> GetAll();
        List<CustomerBranchesVM> GetAllByAccID(int AccID);
    }
}
