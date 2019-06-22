using ResortERP.Core;
using ResortERP.Core.VM;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ResortERP.Service.IServices
{
    public interface IItemsGroupsService
    {
        bool Insert(ItemsGroupsVM entity);
        Task<int> InsertAsync(ItemsGroupsVM entity); 
        bool Update(ItemsGroupsVM entity);
        Task<bool> UpdateAsync(ItemsGroupsVM entity);

        bool Delete(ItemsGroupsVM entity);
        Task<bool> DeleteAsync(ItemsGroupsVM entity);
        Task<List<ItemsGroupsVM>> GetAllAsync(int pageNum, int pageSize);
        Task<int> CountAsync();
        List<ItemsGroupsVM> GetAllItemGroups();
        List<ItemsGroupsVM> GetAllItemGroupsPos();
        Task<ItemsGroupsVM> getItemGroupByID(int GroupID);
        string GetLastCode();
        List<ItemsGuidVM> getForTree();
        ItemsGroupsVM GetByID(int itemID);

        //List<ItemsGuidVM> buildItemsTree();
    }
}