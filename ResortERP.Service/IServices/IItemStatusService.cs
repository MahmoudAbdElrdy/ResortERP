using ResortERP.Core.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortERP.Service.IServices
{
    public interface IItemStatusService
    {

        bool Insert(ItemStatusVM entity);
        Task<int> InsertAsync(ItemStatusVM entity);
        bool Update(ItemStatusVM entity);
        Task<bool> UpdateAsync(ItemStatusVM entity);
        bool Delete(ItemStatusVM entity);
        Task<bool> DeleteAsync(ItemStatusVM entity);
        Task<List<ItemStatusVM>> GetAllAsync(int pageNum, int pageSize);
        Task<int> CountAsync();
        CustomItemUnitsVM GetUnitDataUsingUnitCode(string UnitCode);
        Task<List<ItemStatusVM>> GetAllgetItemStatus();
        string GetLastCode();

        Task<ItemStatusVM> getById(int ID);


    }
}
