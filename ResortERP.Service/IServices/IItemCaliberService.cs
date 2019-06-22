using ResortERP.Core.VM;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ResortERP.Service.IServices
{
    public interface IItemCaliberService
    {
        bool Insert(ItemCaliberVM entity);
        Task<int> InsertAsync(ItemCaliberVM entity);
        bool Update(ItemCaliberVM entity);
        Task<bool> UpdateAsync(ItemCaliberVM entity);

        bool Delete(ItemCaliberVM entity);
        Task<bool> DeleteAsync(ItemCaliberVM entity);
        Task<List<ItemCaliberVM>> GetAllAsync(int pageNum, int pageSize);
        Task<int> CountAsync();
        List<ItemCaliberVM> getAllItemCalibers();
        List<ItemCaliberVM> GetAllItemGroupsPos();
        string GetLastCode();
        ItemCaliberVM GetByID(int CaliberID);
        ItemCaliberVM GetByIDLog(int CaliberID);
        Task<List<ItemCaliberVM>> GetAll();
    }
}