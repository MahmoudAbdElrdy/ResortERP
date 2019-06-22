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
    public class ItemCaliberController : ApiController
    {
        IItemCaliberService itemCaliberService;
        IItemsService itemsService;
        IUserLogFileService userLogFileService;
        public ItemCaliberController(IItemCaliberService itemCaliberService, IItemsService itemsService, IUserLogFileService userLogFileService)
        {
            this.itemCaliberService = itemCaliberService;
            this.itemsService = itemsService;
            this.userLogFileService = userLogFileService;
        }
       
        [Route("ItemCaliber/add")]
        [HttpPost]
        public async Task<IHttpActionResult> add([FromBody]ItemCaliberVM entity)
        {
            var result = await itemCaliberService.InsertAsync(entity);
            await LogData(entity.Code, result.ToString());
            if (result != 0)
            {
                return Ok(true);
            }
            return Ok(false);

        }
        [Route("ItemCaliber/update")]
        [HttpPost]
        public async Task<IHttpActionResult> update([FromBody]ItemCaliberVM entity)
        {
            var result = await itemCaliberService.UpdateAsync(entity);
            await LogData(entity.Code,entity.ID.ToString());
            return Ok(result);

        }
        [Route("ItemCaliber/delete")]
        [HttpPost]
        public async Task<IHttpActionResult> delete([FromBody]ItemCaliberVM entity)
        {
            var q = itemsService.getByItemCaliber(entity.ID);
            if (q.Count == 0)
            {
                var result = await itemCaliberService.DeleteAsync(entity);
                await LogData(entity.Code, entity.ID.ToString());
                return Ok(result);
            }
            else
                return Ok(false);
            // return Ok(await itemCaliberService.DeleteAsync(entity));
        }
        [Route("ItemCaliber/get")]
        [HttpGet]
        public async Task<IHttpActionResult> get(int pageNum, int pageSize)
        {
            return Ok(await itemCaliberService.GetAllAsync( pageNum,  pageSize));
        }
        [Route("ItemCaliber/getAllItemCalibers")]
        [HttpGet]
        public List<ItemCaliberVM> getAllItemCalibers()
        {
            return itemCaliberService.getAllItemCalibers();
        }
        [Route("ItemCaliber/count")]
        [HttpGet]
        public async Task<IHttpActionResult> count()
        {
            return Ok(await itemCaliberService.CountAsync());
        }
        [Route("ItemCaliber/getMainItemGroupsPos")]
        [HttpGet]
        public List<ItemCaliberVM> getMainItemGroupsPos()
        {
            return itemCaliberService.GetAllItemGroupsPos();
        }


        [Route("ItemCaliber/getLastCode")]
        [HttpGet]
        public string GetLastCode()
        {
            return itemCaliberService.GetLastCode();
        }


        [Route("ItemCaliber/getByID")]
        [HttpGet]
        public ItemCaliberVM getByID(int caliberId)
        {
            return itemCaliberService.GetByID(caliberId);
        }

        [Route("ItemCaliber/getByIDLog")]
        [HttpGet]
        public ItemCaliberVM getByIDLog(int caliberId)
        {
            return itemCaliberService.GetByIDLog(caliberId);
        }
        public async Task<bool> LogData(string code = null,string id=null)
        {
            Logger logger = new Logger();
            await userLogFileService.Insert(logger.LogDataMethod(code,id));
            return true;
        }



        [Route("ItemCaliber/getAll")]
        [HttpGet]
        public async Task<IHttpActionResult> getAll()
        {
            return Ok(await itemCaliberService.GetAll());
        }

    }
}
