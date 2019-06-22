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
    public class EntrySettingController : ApiController
    {
        IEntrySettingService entrysettingService;
        IUserLogFileService userLogFileService;
        public EntrySettingController(IEntrySettingService entrysettingService, IUserLogFileService userLogFileService)
        {
            this.entrysettingService = entrysettingService;
            this.userLogFileService = userLogFileService;
        }

        [Route("entrySetting/insert")]
        [HttpPost]
        public async Task<IHttpActionResult> add([FromBody]Entry_SettingsVM entity)
        {
            var result = await entrysettingService.InsertAsync(entity);
            await LogData(null,result.ToString());
            //if (result != 0)
            //{
            //    return Ok(true);
            //}
            return Ok(result);
        }
        [Route("entrySetting/update")]
        [HttpPost]
        public async Task<IHttpActionResult> update([FromBody]Entry_SettingsVM entity)
        {
            var result = await entrysettingService.UpdateAsync(entity);
            await LogData(null,entity.ENTRY_SETTING_ID.ToString());
            return Ok(result);
        }
        [Route("entrySetting/delete")]
        [HttpPost]
        public async Task<IHttpActionResult> delete([FromBody]Entry_SettingsVM entity)
        {
            var result = await entrysettingService.DeleteAsync(entity);
            await LogData(null,entity.ENTRY_SETTING_ID.ToString());
            return Ok(result);
        }
        [Route("entrySetting/get")]
        [HttpGet]
        public async Task<IHttpActionResult> get(int pageNum, int pageSize)
        {
            return Ok(await entrysettingService.GetAllAsync(pageNum, pageSize));
        }

        [Route("entrySetting/getAll")]
        [HttpGet]
        public List<Entry_SettingsVM> getAll()
        {
            return entrysettingService.GetAll();
        }

        [Route("entrySetting/count")]
        [HttpGet]
        public async Task<IHttpActionResult> count()
        {
            return Ok(await entrysettingService.CountAsync());
        }



        [Route("entrySetting/getEntrySettingByID/{typeID}")]
        [HttpGet]
        public async Task<IHttpActionResult> GetEntrySettingByID(byte typeID)
        {
            return Ok(await entrysettingService.GetEntrySettingByID(typeID));
        }




        [Route("entrySetting/getSettingBySettingID")]
        [HttpGet]
        public Entry_SettingsVM GetSettingBySettingID(int SettingID)
        {
            return  entrysettingService.GetEntrySettingBySettingID(SettingID);
        }



        [Route("entrySetting/getEntryTypeBySettingID")]
        [HttpGet]
        public int GetEntryTypeBySettingID(int SettingID)
        {
            return entrysettingService.GetEntryTypeBySettingID(SettingID);
        }

        public async Task<bool> LogData(string code=null,string id=null)
        {
            Logger logger = new Logger();
            await userLogFileService.Insert(logger.LogDataMethod(code,id));
            return true;
        }

    }
}