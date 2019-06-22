using ResortERP.Core;
using ResortERP.Core.VM;
using ResortERP.Repository;
using ResortERP.Service;
using ResortERP.Service.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace ResortERP.Api.Controllers
{
    [Logger]
    public class itemsController : ApiController
    {
        IItemsService itemsService;
        IUserService userService;
        IBillMasterService billMasterService;
        IUserLogFileService userLogFileService;
        public itemsController(IItemsService itemsService, IUserService userService, IBillMasterService billMasterService, IUserLogFileService userLogFileService)
        {
            this.itemsService = itemsService;
            this.userService = userService;
            this.billMasterService = billMasterService;
            this.userLogFileService = userLogFileService;
        }

        [Route("Items/add")]
        [HttpPost]
        public async Task<int> add([FromBody]ITEMSVM entity)
        {
            var result = itemsService.Insert(entity);
            await LogData(entity.ITEM_CODE, result.ToString());
            return result;
        }
        [Route("Items/update")]
        [HttpPost]
        public async Task<IHttpActionResult> update([FromBody]ITEMSVM entity)
        {
            var result = await itemsService.UpdateAsync(entity);
            await LogData(entity.ITEM_CODE,entity.ITEM_ID.ToString());
            return Ok(result);
        }

        [Route("Items/validat")]
        [HttpGet]
        public async Task<IHttpActionResult> chkValidat(string Code, string ArName, string EnName)
        {
            return Ok(await itemsService.checkValidation(Code, ArName, EnName));
        }

        [Route("Items/delete")]
        [HttpPost]
        public async Task<IHttpActionResult> delete([FromBody]ITEMSVM entity)
        {
            var q = billMasterService.getByItemId(entity.ITEM_ID);
            if (q.Count == 0)
            {
                var result = await itemsService.DeleteAsync(entity);
                await LogData(entity.ITEM_CODE, entity.ITEM_ID.ToString());
                return Ok(result);
            }
                
            else
                return Ok(false);
            //return Ok(await itemsService.DeleteAsync(entity));
        }
        [Route("Items/get")]
        [HttpGet]
        public async Task<IHttpActionResult> get(int pageNum, int pageSize)
        {
            return Ok(await itemsService.GetAllAsync(pageNum, pageSize));
        }

        [Route("Items/getByID")]
        [HttpGet]
        public async Task<IHttpActionResult> getByID(int itemID)
        {
            return Ok(await itemsService.GetByID(itemID));
        }

        [Route("Items/getAll")]
        [HttpGet]
        public List<ITEMSVM> getAll()
        {
            return itemsService.GetAllItems();
        }

        [Route("Items/count")]
        [HttpGet]
        public async Task<IHttpActionResult> count()
        {
            return Ok(await itemsService.CountAsync());
        }

        [Route("Items/GetItemsUnits")]
        [HttpGet]
        public List<CustomItemUnitsVM> GetItemsUnits(int ItemID)
        {
            return itemsService.GetAllItemUnits(ItemID);
        }

        [Route("Items/SaveItemUnits")]
        [HttpPost]
        public bool SaveItemUnits(ItemUnitsInsertedDeleted obj)
        {
            var result = itemsService.InsertItemUnits(obj.ItemUnitsList, obj.DeltedItemUnits);
            //LogData("+" + obj.ItemUnitsList.co);
            return result;
        }

        [Route("Items/GetItemsByItemGroup")]
        [HttpGet]
        public List<ITEMSVM> GetItemsByItemGroup(int GroupID)
        {
            return itemsService.GetItemsByItemGroup(GroupID);
        }

        [Route("Items/GetItemNameUsingItemID")]
        [HttpGet]
        public string GetItemNameUsingItemID(int ItemID)
        {
            return itemsService.GetItemNameUsingItemID(ItemID);
        }

        [Route("Items/GetItemDataUsingItemCode")]
        [HttpGet]
        public ITEMSVM GetItemDataUsingItemCode(string ItemCode, int sellTypeID)
        {
            return itemsService.GetItemDataUsingItemCode(ItemCode, sellTypeID);
        }


        


        [Route("Items/getLastItemCodeByGroup")]
        [HttpGet]
        public string getLastItemCodeByGroup(int groupID)
        {
            return itemsService.getLastItemCodeByGroup(groupID);
        }

        [Route("Items/SearchItems")]
        [HttpGet]
        public List<ITEMSVM> SearchItems(string SearchCriteria)
        {
            return itemsService.SearchItems(SearchCriteria);
        }

        [Route("itemMotion/getSearchForItem")]
        [HttpGet]
        public async Task<IHttpActionResult> getSearchForItem(string Search)
        {
            return Ok(await itemsService.GetSearchForItem(Search));
        }




        [Route("Items/getLastItemCode")]
        [HttpGet]
        public string GetLastItemCode()
        {
            return itemsService.getLastItemCode();
        }


        [Route("Items/GetItemCurrentQuantity")]
        [HttpGet]
        public async Task<ItemCurrentQuantityVM> GetItemCurrentQuantity(string ItemID, int StoreID)
        {
                return await itemsService.GetItemCurrentQuantity(ItemID, StoreID);
        }


        public async Task<bool> LogData(string code = null,string id=null)
        {
            Logger logger = new Logger();
            await userLogFileService.Insert(logger.LogDataMethod(code,id));
            return true;
        }


    }
}
