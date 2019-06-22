using ResortERP.Core.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortERP.Service.IServices
{
    public interface IGetCustomerSupplierService
    {
        Task<List<CustomersVM>> GetAllAsync(int pageNum, int pageSize, char type);
        Task<int> CountAsync();
        List<CustomersVM> GetAll(char type);
        Task<CustomersVM> getByIdLog(int ACC_ID, char type);
    }
}
