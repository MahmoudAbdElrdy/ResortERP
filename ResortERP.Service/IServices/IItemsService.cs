using ResortERP.Core;
using ResortERP.Core.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortERP.Service.IServices
{
    public interface IItemsService
    {
        int Insert(ITEMSVM entity);
        Task<int> InsertAsync(ITEMSVM entity);
        bool Update(ITEMSVM entity);
        Task<bool> UpdateAsync(ITEMSVM entity);

        bool Delete(ITEMSVM entity);
        Task<bool> DeleteAsync(ITEMSVM entity);
        Task<List<ITEMSVM>> GetAllAsync(int pageNum, int pageSize);
        Task<int> CountAsync();
        List<ITEMSVM> GetAllItems();
        List<CustomItemUnitsVM> GetAllItemUnits(int ItemID);
        bool InsertItemUnits(List<CustomItemUnitsVM> ItemUnitsList, int[] DeletedItemUnits);
        List<ITEMSVM> GetItemsByItemGroup(int GroupID);
        string GetItemNameUsingItemID(int ItemID);
        ITEMSVM GetItemDataUsingItemCode(string ItemCode, int SellTypeID);

        string getLastItemCodeByGroup(int GroupID);
        List<ITEMSVM> SearchItems(string SearchCriteria);
        List<ITEMS> GetByIDGroup(int groupId);
        Task<List<ITEMSVM>> GetSearchForItem(string search);
        Task<Dictionary<string, bool>> checkValidation(string Code, string ArName, string EnName);
        Task<ITEMS> GetByID(int itemID);

        string getLastItemCode();
        List<ITEMS> getByItemCaliber(int itemCaliberID);
        List<ITEMS> getByItemStatus(int itemStatusID);
        Task<ItemCurrentQuantityVM> GetItemCurrentQuantity(string ItemID, int StoreID);
        


    }
}
