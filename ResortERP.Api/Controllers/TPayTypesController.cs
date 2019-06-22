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
    public class TPayTypesController : ApiController
    {
        ITPAY_TYPESService tPayTypesService;
        IBillMasterService billMasterService;
        IUserLogFileService userLogFileService;
        public TPayTypesController(ITPAY_TYPESService tPayTypesService, IBillMasterService billMasterService, IUserLogFileService userLogFileService)
        {
            this.tPayTypesService = tPayTypesService;
            this.billMasterService = billMasterService;
            this.userLogFileService = userLogFileService;
        }

        [Route("tPayTypes/add")]
        [HttpPost]
        public async Task<IHttpActionResult> add([FromBody]TPAY_TYPESVM entity)
        {
            var result =await tPayTypesService.InsertAsync(entity);
            await LogData(entity.PAY_TYPE_CODE,result.ToString());
            if (result != 0)
            {
                return Ok(true);
            }
            return Ok(false);

        }
        [Route("tPayTypes/update")]
        [HttpPost]
        public async Task<IHttpActionResult> update([FromBody]TPAY_TYPESVM entity)
        {
            var result = await tPayTypesService.UpdateAsync(entity);
            await LogData(entity.PAY_TYPE_CODE,entity.PAY_TYPE_ID.ToString());
            return Ok(result);
 
        }
        [Route("tPayTypes/delete")]
        [HttpPost]
        public async Task<IHttpActionResult> delete([FromBody]TPAY_TYPESVM entity)
        {
            var q = billMasterService.getByPayTypeId(entity.PAY_TYPE_ID.ToString());
            if (q.Count == 0)
            {
                var result = await tPayTypesService.DeleteAsync(entity);
                await LogData(entity.PAY_TYPE_CODE, entity.PAY_TYPE_ID.ToString());
                return Ok(result);
            }
            else
                return Ok(false);
            //return Ok(await tPayTypesService.DeleteAsync(entity));
        }
        [Route("tPayTypes/get")]
        [HttpGet]
        public async Task<IHttpActionResult> get(int pageNum, int pageSize)
        {
            return Ok(await tPayTypesService.GetAllAsync(pageNum, pageSize));
        }


        [Route("tPayTypes/getAll")]
        [HttpGet]
        public async Task<IHttpActionResult> getAll()
        {
            return Ok(await tPayTypesService.GetAll());
        }



        [Route("tPayTypes/count")]
        [HttpGet]
        public async Task<IHttpActionResult> count()
        {
            return Ok(await tPayTypesService.CountAsync());
        }


        [Route("tPayTypes/getLastCode")]
        [HttpGet]
        public string GetLastCode()
        {
            return tPayTypesService.GetLastCode();
        }



        [Route("tPayTypes/getByID")]
        [HttpGet]
        public async Task<IHttpActionResult> getByID(int payTypeID)
        {
            return Ok(await tPayTypesService.GetByID(payTypeID));
        }


        [Route("tPayTypes/getAllActive")]
        [HttpGet]
        public async Task<IHttpActionResult> getAllActive()
        {
            return Ok(await tPayTypesService.GetAllActive());
        }
        public async Task<bool> LogData(string code = null,string id=null)
        {
            Logger logger = new Logger();
            await userLogFileService.Insert(logger.LogDataMethod(code,id));
            return true;
        }
    }
}
