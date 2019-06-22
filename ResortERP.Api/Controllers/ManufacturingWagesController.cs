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
    public class ManufacturingWagesController : ApiController
    {
        IManufacturingWagesService manufacturingWagesService;
        IUserLogFileService userLogFileService;
        public ManufacturingWagesController(IManufacturingWagesService manufacturingWagesService, IUserLogFileService userLogFileService)
        {
            this.manufacturingWagesService = manufacturingWagesService;
            this.userLogFileService = userLogFileService;
        }

        [Route("manufacturingWages/insert")]
        [HttpPost]
        public async Task<IHttpActionResult> add([FromBody]ManufacturingWagesVM entity)
        {
            var result = await manufacturingWagesService.InsertAsync(entity);
            await LogData(entity.ManufacturingWage_CODE, result.ToString());
            if (result != 0)
            {
                return Ok(true);
            }
            return Ok(false);

        }
        [Route("manufacturingWages/update")]
        [HttpPost]
        public async Task<IHttpActionResult> update([FromBody]ManufacturingWagesVM entity)
        {
            var result = await manufacturingWagesService.UpdateAsync(entity);
            await LogData(entity.ManufacturingWage_CODE,entity.ManufacturingWage_ID.ToString());
            return Ok(result);

        }
        [Route("manufacturingWages/delete")]
        [HttpPost]
        public async Task<IHttpActionResult> delete([FromBody]ManufacturingWagesVM entity)
        {
            var result = await manufacturingWagesService.DeleteAsync(entity);
            await LogData(entity.ManufacturingWage_CODE, entity.ManufacturingWage_ID.ToString());
            return Ok(result);

        }
        [Route("manufacturingWages/get")]
        [HttpGet]
        public async Task<IHttpActionResult> get(int pageNum, int pageSize)
        {
            return Ok(await manufacturingWagesService.GetAllAsync(pageNum, pageSize));
        }

        [Route("manufacturingWages/getById")]
        [HttpGet]
        public async Task<IHttpActionResult> getById(int ID)
        {
            return Ok(await manufacturingWagesService.getById(ID));
        }

        [Route("manufacturingWages/count")]
        [HttpGet]
        public async Task<IHttpActionResult> count()
        {
            return Ok(await manufacturingWagesService.CountAsync());
        }
        //Get Unit Data Using UnitCode
        //[Route("manufacturingWages/GetUnitDataUsingUnitCode")]
        //[HttpGet]
        //public CustomItemManufacturingWagesVM GetUnitDataUsingUnitCode(string UnitCode)
        //{
        //    return manufacturingWagesService.GetUnitDataUsingUnitCode(UnitCode);
        //}

        [Route("manufacturingWages/getLastCode")]
        [HttpGet]
        public string GetLastCode()
        {
            return manufacturingWagesService.GetLastCode();
        }
        public async Task<bool> LogData(string code = null,string id=null)
        {
            Logger logger = new Logger();
            await userLogFileService.Insert(logger.LogDataMethod(code,id));
            return true;
        }
    }
}