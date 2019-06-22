using ResortERP.Core;
using ResortERP.Core.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortERP.Service.IServices
{
    public interface IOrdersService
    {
        bool Insert(OrdersVM entity);
        Task<bool> InsertAsync(OrdersVM entity);
        bool Update(OrdersVM entity);
        Task<bool> UpdateAsync(OrdersVM entity);
        bool Delete(OrdersVM entity);
        Task<bool> DeleteAsync(OrdersVM entity);
        Task<List<OrdersVM>> GetAllAsync(int pageNum, int pageSize);
        Task<int> CountAsync();
        List<OrdersVM> GetAll();
    }
}
