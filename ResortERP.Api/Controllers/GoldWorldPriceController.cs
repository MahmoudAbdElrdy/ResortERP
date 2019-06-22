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
    public class GoldWorldPriceController : ApiController
    {
        IGoldWorldPriceService goldWorldPriceService;
        IUserLogFileService userLogFileService;
        public GoldWorldPriceController(IGoldWorldPriceService goldWorldPriceService, IUserLogFileService userLogFileService)
        {
            this.goldWorldPriceService = goldWorldPriceService;
            this.userLogFileService = userLogFileService;
        }

        [Route("GoldWorldPrice/add")]
        [HttpPost]
        public async Task<IHttpActionResult> add([FromBody]GoldWorldPriceVM entity)
        {
            var result = await goldWorldPriceService.InsertAsync(entity);
            await LogData(entity.Code, result.ToString());
            if (result != 0)
            {
                return Ok(true);
            }
            return Ok(false);

        }
        [Route("GoldWorldPrice/update")]
        [HttpPost]
        public async Task<IHttpActionResult> update([FromBody]GoldWorldPriceVM entity)
        {
            var result = await goldWorldPriceService.UpdateAsync(entity);
            await LogData(entity.Code, entity.ID.ToString());
            return Ok(result);

        }
        [Route("GoldWorldPrice/delete")]
        [HttpPost]
        public async Task<IHttpActionResult> delete([FromBody]GoldWorldPriceVM entity)
        {
            var result = await goldWorldPriceService.DeleteAsync(entity);
            await LogData(entity.Code, entity.ID.ToString());
            return Ok(result);

        }
        [Route("GoldWorldPrice/get")]
        [HttpGet]
        public async Task<IHttpActionResult> get(int pageNum, int pageSize)
        {
            return Ok(await goldWorldPriceService.GetAllAsync(pageNum, pageSize));
        }

        [Route("GoldWorldPrice/getById")]
        [HttpGet]
        public async Task<IHttpActionResult> getById(int ID)
        {
            return Ok(await goldWorldPriceService.getById(ID));
        }

        [Route("GoldWorldPrice/getAllGoldWorldPrices")]
        [HttpGet]
        public List<GoldWorldPriceVM> getAllGoldWorldPrices()
        {
            return goldWorldPriceService.getAllGoldWorldPrices();
        }
        [Route("GoldWorldPrice/count")]
        [HttpGet]
        public async Task<IHttpActionResult> count()
        {
            return Ok(await goldWorldPriceService.CountAsync());
        }
        [Route("GoldWorldPrice/getMainItemGroupsPos")]
        [HttpGet]
        public List<GoldWorldPriceVM> getMainItemGroupsPos()
        {
            return goldWorldPriceService.GetAllItemGroupsPos();
        }
        [Route("GoldWorldPrice/GetLastGoldWorldPrice")]
        [HttpGet]
        public async Task<Double> GetLastBillNumber()
        {
            return await goldWorldPriceService.GetLastGoldWorldPrice();
        }


        [Route("GoldWorldPrice/getLastCode")]
        [HttpGet]
        public string GetLastCode()
        {
            return goldWorldPriceService.GetLastCode();
        }




        [Route("GoldWorldPrice/getLastGoldPrice")]
        [HttpGet]
        public double? GetLastGoldPrice()
        {
            return goldWorldPriceService.GetLastGoldPrice();
        }
        public async Task<bool> LogData(string code = null, string id = null)
        {
            Logger logger = new Logger();
            await userLogFileService.Insert(logger.LogDataMethod(code, id));
            return true;
        }




        [Route("GoldWorldPrice/GetLastGoldWorldPriceData")]
        [HttpGet]
        public async Task<GoldWorldPriceVM> GetLastGoldWorldPriceData()
        {
            return await goldWorldPriceService.GetLastGoldWorldPriceData();
        }
    }
}
