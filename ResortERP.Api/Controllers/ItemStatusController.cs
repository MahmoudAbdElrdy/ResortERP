using ResortERP.Core.VM;
using ResortERP.Repository;
using ResortERP.Service.IServices;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace ResortERP.Api.Controllers
{
    [Logger]
    public class ItemStatusController : ApiController
    {
        IItemStatusService itemStatusService;
        IItemsService itemsService;
        IUserLogFileService userLogFileService;
        public ItemStatusController(IItemStatusService itemStatusService, IItemsService itemsService, IUserLogFileService userLogFileService)
        {
            this.itemStatusService = itemStatusService;
            this.itemsService= itemsService;
            this.userLogFileService = userLogFileService;
        }

        [Route("itemStatus/insert")]
        [HttpPost]
        public async Task<IHttpActionResult> add([FromBody]ItemStatusVM entity)
        {
            var result = await itemStatusService.InsertAsync(entity);
            await LogData(entity.Code,result.ToString());
            if (result != 0)
            {
                return Ok(true);
            }
            return Ok(false);

        }
        [Route("itemStatus/update")]
        [HttpPost]
        public async Task<IHttpActionResult> update([FromBody]ItemStatusVM entity)
        {
            var result = await itemStatusService.UpdateAsync(entity);
            await LogData(entity.Code,entity.ID.ToString());
            return Ok(result);

        }
        [Route("itemStatus/delete")]
        [HttpPost]
        public async Task<IHttpActionResult> delete([FromBody]ItemStatusVM entity)
        {
            var q = itemsService.getByItemStatus(entity.ID);
            if (q.Count == 0)
            {
                var result = await itemStatusService.DeleteAsync(entity);
                await LogData(entity.Code, entity.ID.ToString());
                return Ok(result);
            }

            else
                return Ok(false);
            //return Ok(await itemStatusService.DeleteAsync(entity));
        }
        [Route("itemStatus/get")]
        [HttpGet]
        public async Task<IHttpActionResult> get(int pageNum, int pageSize)
        {
            return Ok(await itemStatusService.GetAllAsync(pageNum, pageSize));
        }

        [Route("itemStatus/getById")]
        [HttpGet]
        public async Task<IHttpActionResult> getById(int ID)
        {
            return Ok(await itemStatusService.getById(ID));
        }

        [Route("itemStatus/count")]
        [HttpGet]
        public async Task<IHttpActionResult> count()
        {
            return Ok(await itemStatusService.CountAsync());
        }
        //Get Unit Data Using UnitCode
        [Route("itemStatus/GetUnitDataUsingUnitCode")]
        [HttpGet]
        public CustomItemUnitsVM GetUnitDataUsingUnitCode(string UnitCode)
        {
            return itemStatusService.GetUnitDataUsingUnitCode(UnitCode);
        }
        [Route("itemStatus/getItemStatus")]
        [HttpGet]
        public Task<List<ItemStatusVM>> getItemStatus()
        {
            return itemStatusService.GetAllgetItemStatus();
        }



        [Route("itemStatus/getLastCode")]
        [HttpGet]
        public string GetLastCode()
        {
            return itemStatusService.GetLastCode();
        }
        public async Task<bool> LogData(string code = null,string id=null)
        {
            Logger logger = new Logger();
            await userLogFileService.Insert(logger.LogDataMethod(code,id));
            return true;
        }
    }
}