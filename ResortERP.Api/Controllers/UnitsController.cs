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
    public class UnitsController : ApiController
    {
        IUnitsService unitsService;
        IItemsService itemsService;
        IUnit_ItemsService unitsItems;
        IUserLogFileService userLogFileService;
        public UnitsController(IUnitsService unitsService, IItemsService itemsService, IUnit_ItemsService unitsItems, IUserLogFileService userLogFileService)
        {
            this.unitsService = unitsService;
            this.itemsService = itemsService;
            this.unitsItems = unitsItems;
            this.userLogFileService = userLogFileService;
        }

        [Route("units/insert")]
        [HttpPost]
        public async Task<IHttpActionResult> add([FromBody]UNITSVM entity)
        {
            var result = await unitsService.InsertAsync(entity);
            await LogData(entity.UNIT_CODE,result.ToString());
            if (result != 0)
            {
                return Ok(true);
            }
            return Ok(false);
        }
        [Route("units/update")]
        [HttpPost]
        public async Task<IHttpActionResult> update([FromBody]UNITSVM entity)
        {
            var result = await unitsService.UpdateAsync(entity);
            await LogData(entity.UNIT_CODE,entity.UNIT_ID.ToString());
            return Ok(result);
        }
        [Route("units/delete")]
        [HttpPost]
        public async Task<IHttpActionResult> delete([FromBody]UNITSVM entity)
        {
            var q = unitsItems.getByItemunits(entity.UNIT_ID);
            if (q.Count == 0)
            {
                var result = await unitsService.DeleteAsync(entity);
                await LogData(entity.UNIT_CODE, entity.UNIT_ID.ToString());
                return Ok(result);
            }
               
            else
                return Ok(false);
            //return Ok(await unitsService.DeleteAsync(entity));
        }
        [Route("units/get")]
        [HttpGet]
        public async Task<IHttpActionResult> get(int pageNum, int pageSize)
        {
            return Ok(await unitsService.GetAllAsync(pageNum, pageSize));
        }

        [Route("units/getById")]
        [HttpGet]
        public async Task<IHttpActionResult> getById(int UNIT_ID)
        {
            return Ok(await unitsService.getById(UNIT_ID));
        }

        [Route("units/count")]
        [HttpGet]
        public async Task<IHttpActionResult> count()
        {
            return Ok(await unitsService.CountAsync());
        }
        //Get Unit Data Using UnitCode
        [Route("units/GetUnitDataUsingUnitCode")]
        [HttpGet]
        public CustomItemUnitsVM GetUnitDataUsingUnitCode(string UnitCode)
        {
            return unitsService.GetUnitDataUsingUnitCode(UnitCode);
        }


        [Route("units/getLastCode")]
        [HttpGet]
        public string GetLastCode()
        {
            return unitsService.GetLastCode();
        }

        public async Task<bool> LogData(string code=null,string id=null)
        {
            Logger logger = new Logger();
            await userLogFileService.Insert(logger.LogDataMethod(code,id));
            return true;
        }
    }
}