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
    public class EntryGridColumnsController : ApiController
    {
        IEntryGridColumnsService entryGrdColService;
        IUserLogFileService _userLogFileService;
        public EntryGridColumnsController(IEntryGridColumnsService entryGrdColService, IUserLogFileService _userLogFileService)
        {
            this.entryGrdColService = entryGrdColService;
            this._userLogFileService = _userLogFileService;
        }

        [Route("entryGrdCol/insert")]
        [HttpPost]
        public async Task<IHttpActionResult> add([FromBody]Entry_Grid_ColumnsVM entity)
        {
           // return Ok(await entryGrdColService.InsertAsync(entity));
            var result = await entryGrdColService.InsertAsync(entity);
            await LogData(null, result.ToString());
            if (result != 0)
            {
                return Ok(true);
            }
            return Ok(false);
        }
        [Route("entryGrdCol/update")]
        [HttpPost]
        public async Task<IHttpActionResult> update([FromBody]Entry_Grid_ColumnsVM entity)
        {
            var result = await entryGrdColService.UpdateAsync(entity);
            await LogData(null,entity.ENTRY_SETTING_ID.ToString());
            return Ok(result);
        }
        [Route("entryGrdCol/delete")]
        [HttpPost]
        public async Task<IHttpActionResult> delete([FromBody]Entry_Grid_ColumnsVM entity)
        {
            var result = await entryGrdColService.DeleteAsync(entity);
            await LogData(null,entity.ENTRY_SETTING_ID.ToString());
            return Ok(result);

        }
        [Route("entryGrdCol/get")]
        [HttpGet]
        public async Task<IHttpActionResult> get(int pageNum, int pageSize)
        {
            return Ok(await entryGrdColService.GetAllAsync(pageNum, pageSize));
        }

        [Route("entryGrdCol/getByID")]
        [HttpGet]
        public async Task<IHttpActionResult> getByID(int EntrySettingID)
        {
            return Ok(await entryGrdColService.GetByID(EntrySettingID));

        }
        [Route("entryGrdCol/getAll")]
        [HttpGet]
        public List<Entry_Grid_ColumnsVM> getAll()
        {
            return entryGrdColService.GetAll();
        }

        [Route("entryGrdCol/count")]
        [HttpGet]
        public async Task<IHttpActionResult> count()
        {
            return Ok(await entryGrdColService.CountAsync());
        }

        public async Task<bool> LogData(string code = null, string id = null)
        {
            Logger logger = new Logger();
            //await _userLogFileService.Insert(logger.LogDataMethod(code, id));
            return true;
        }
    }
}