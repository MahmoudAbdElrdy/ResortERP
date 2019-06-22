using ResortERP.Core.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortERP.Service.IServices
{
    public interface ICustomerSonService
    {
        bool Insert(CustomerSonVM entity);
        Task<bool> InsertAsync(CustomerSonVM entity);
        bool Update(CustomerSonVM entity);
        Task<bool> UpdateAsync(CustomerSonVM entity);

        bool Delete(CustomerSonVM entity);
        Task<bool> DeleteAsync(CustomerSonVM entity);
        Task<List<CustomerSonVM>> GetAllAsync(int pageNum, int pageSize);
        Task<int> CountAsync();
        List<CustomerSonVM> GetAll();
    }
}
