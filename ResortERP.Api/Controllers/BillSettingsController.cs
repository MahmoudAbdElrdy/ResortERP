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
    public class BillSettingsController : ApiController
    {
        IBillSettingsService billSettingsService;
        IUserLogFileService userLogFileService;
        public BillSettingsController(IBillSettingsService billSettingsService, IUserLogFileService userLogFileService)
        {
            this.billSettingsService = billSettingsService;
            this.userLogFileService = userLogFileService;
        }

        [Route("billSettings/insert")]
        [HttpPost]
        public async Task<IHttpActionResult> add([FromBody]BILL_SETTINGSVM entity)
        {
            var result = await billSettingsService.InsertAsync(entity);
            await LogData(null,result.ToString());
            return Ok(result);

        }
        [Route("billSettings/update")]
        [HttpPost]
        public async Task<IHttpActionResult> update([FromBody]BILL_SETTINGSVM entity)
        {
            var result = await billSettingsService.UpdateAsync(entity);
           await LogData(null,entity.BILL_SETTING_ID.ToString());
            return Ok(result);

        }

        [Route("billSettings/chkExist")]
        [HttpPost]
        public async Task<IHttpActionResult> chkExist([FromBody]BILL_SETTINGSVM entity)
        {
            return Ok(await billSettingsService.CheckExist(entity));
        }

        [Route("billSettings/delete")]
        [HttpPost]
        public async Task<IHttpActionResult> delete([FromBody]BILL_SETTINGSVM entity)
        {
            var result = await billSettingsService.DeleteAsync(entity);
            await LogData(null,entity.BILL_SETTING_ID.ToString());
            return Ok(result);

        }
        [Route("billSettings/get")]
        [HttpGet]
        public async Task<IHttpActionResult> get(int pageNum, int pageSize)
        {
            return Ok(await billSettingsService.GetAllAsync(pageNum, pageSize));
        }

        [Route("billSettings/getAll")]
        [HttpGet]
        public List<BILL_SETTINGSVM> getAll()
        {
            return billSettingsService.GetAll();
        }

        [Route("billSettings/getBillSettingByID")]
        [HttpGet]
        public async Task<IHttpActionResult> GetBillSettingByID(byte typeID)
        {
            return Ok(await billSettingsService.GetBillSettingByID(typeID));
        }

        [Route("billSettings/getBillSettingByBillID")]
        [HttpGet]
        public IHttpActionResult GetBillSettingByBillID(int BILL_SETTING_ID)
        {
            return Ok(billSettingsService.GetBillSettingByBillID(BILL_SETTING_ID));
        }

        [Route("billSettings/count")]
        [HttpGet]
        public async Task<IHttpActionResult> count()
        {
            return Ok(await billSettingsService.CountAsync());
        }
        public async Task<bool> LogData(string code=null,string id=null)
        {
            Logger logger = new Logger();
            await userLogFileService.Insert(logger.LogDataMethod(code,id));
            return true;
        }
    }
}