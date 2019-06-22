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
    public class ItemsGroupsController : ApiController
    {
        IItemsGroupsService itemsGroupsService;
        IItemsService itemsService;
        IUserLogFileService userLogFileService;
        public ItemsGroupsController(IItemsGroupsService itemsGroupsService, IItemsService itemsService, IUserLogFileService userLogFileService)
        {
            this.itemsGroupsService = itemsGroupsService;
            this.itemsService = itemsService;
            this.userLogFileService = userLogFileService;
        }

        [Route("ItemsGroups/add")]
        [HttpPost]
        public async Task<IHttpActionResult> add([FromBody]ItemsGroupsVM entity)
        {
            var result = await itemsGroupsService.InsertAsync(entity);
            await LogData(entity.GroupCode,result.ToString());
            if (result != 0)
            {
                return Ok(true);
            }
            return Ok(false);
        }
        [Route("ItemsGroups/update")]
        [HttpPost]
        public async Task<IHttpActionResult> update([FromBody]ItemsGroupsVM entity)
        {
            var result = await itemsGroupsService.UpdateAsync(entity);
            await LogData(entity.GroupCode,entity.GroupID.ToString());
            return Ok(result);
        }
        [Route("ItemsGroups/delete")]
        [HttpPost]
        public async Task<IHttpActionResult> delete([FromBody]ItemsGroupsVM entity)
        {
            //return Ok(await itemsGroupsService.DeleteAsync(entity));
            var q = itemsService.GetByIDGroup(entity.GroupID);
            if (q.Count == 0)
            {
                var result = await itemsGroupsService.DeleteAsync(entity);
                await LogData(entity.GroupCode, entity.GroupID.ToString());
                return Ok(result);
            }
               
            else
                return Ok(false);
        }
        [Route("ItemsGroups/get")]
        [HttpGet]
        public async Task<IHttpActionResult> get(int pageNum, int pageSize)
        {
            return Ok(await itemsGroupsService.GetAllAsync(pageNum, pageSize));
        }
        [Route("ItemsGroups/getMainItemGroups")]
        [HttpGet]
        public List<ItemsGroupsVM> getMainItemGroups()
        {
            return itemsGroupsService.GetAllItemGroups();
        }
        [Route("ItemsGroups/count")]
        [HttpGet]
        public async Task<IHttpActionResult> count()
        {
            return Ok(await itemsGroupsService.CountAsync());
        }
        [Route("ItemsGroups/getMainItemGroupsPos")]
        [HttpGet]
        public List<ItemsGroupsVM> getMainItemGroupsPos()
        {
            return itemsGroupsService.GetAllItemGroupsPos();
        }



        [Route("ItemsGroups/getItemGroupByID")]
        [HttpGet]
        public async Task<IHttpActionResult> getItemGroupByID(int groupID)
        {
            return Ok(await itemsGroupsService.getItemGroupByID(groupID));
        }


        [Route("ItemsGroups/getLastCode")]
        [HttpGet]
        public string GetLastCode()
        {
            return itemsGroupsService.GetLastCode();
        }

        [Route("ItemsGroups/getByID")]
        [HttpGet]
        public IHttpActionResult getByID(int itemID)
        {
            return Ok( itemsGroupsService.GetByID(itemID));
        }
        public async Task<bool> LogData(string code = null,string id=null)
        {
            Logger logger = new Logger();
            await userLogFileService.Insert(logger.LogDataMethod(code,id));
            return true;
        }
    }
}
