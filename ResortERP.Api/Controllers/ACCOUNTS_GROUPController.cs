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
    public class ACCOUNTS_GROUPController : ApiController
    {
        IACCOUNTS_GROUPService ACCOUNTS_GROUPService;
        IUserLogFileService userLogFileService;
        public ACCOUNTS_GROUPController(IACCOUNTS_GROUPService ACCOUNTS_GROUPService, IUserLogFileService userLogFileService)
        {
            this.ACCOUNTS_GROUPService = ACCOUNTS_GROUPService;
            this.userLogFileService = userLogFileService;
        }

        [Route("ACCOUNTS_GROUP/add")]
        [HttpPost]
        public async Task<IHttpActionResult> add([FromBody]ACCOUNTS_GROUPVM entity)
        {
            var result = await ACCOUNTS_GROUPService.InsertAsync(entity);
            await LogData(entity.GROUP_CODE,result.ToString());
            if (result != 0)
            {
                return Ok(true);
            }
            return Ok(false);
        }
        [Route("ACCOUNTS_GROUP/update")]
        [HttpPost]
        public async Task<IHttpActionResult> update([FromBody]ACCOUNTS_GROUPVM entity)
        {
            var result = await ACCOUNTS_GROUPService.UpdateAsync(entity);
            await LogData(entity.GROUP_CODE,entity.GROUP_ID.ToString());
            return Ok(result);
        }
        [Route("ACCOUNTS_GROUP/delete")]
        [HttpPost]
        public async Task<IHttpActionResult> delete([FromBody]ACCOUNTS_GROUPVM entity)
        {
                var result = await ACCOUNTS_GROUPService.DeleteAsync(entity);
                await LogData(entity.GROUP_CODE, entity.GROUP_ID.ToString());
                return Ok(result);
        }
        [Route("ACCOUNTS_GROUP/get")]
        [HttpGet]
        public async Task<IHttpActionResult> get(int pageNum, int pageSize)
        {
            return Ok(await ACCOUNTS_GROUPService.GetAllAsync(pageNum, pageSize));
        }
        [Route("ACCOUNTS_GROUP/getMainACCOUNTS_GROUP")]
        [HttpGet]
        public List<ACCOUNTS_GROUPVM> getMainACCOUNTS_GROUPGroups()
        {
            return ACCOUNTS_GROUPService.GetAllACCOUNTS_GROUP();
        }
        [Route("ACCOUNTS_GROUP/count")]
        [HttpGet]
        public async Task<IHttpActionResult> count()
        {
            return Ok(await ACCOUNTS_GROUPService.CountAsync());
        }


        [Route("ACCOUNTS_GROUP/getACCOUNTS_GROUPByID")]
        [HttpGet]
        public async Task<IHttpActionResult> getACCOUNTS_GROUPGroupByID(int groupID)
        {
            return Ok(await ACCOUNTS_GROUPService.getACCOUNTS_GROUPByID(groupID));
        }


        [Route("ACCOUNTS_GROUP/getLastCode")]
        [HttpGet]
        public string GetLastCode()
        {
            return ACCOUNTS_GROUPService.GetLastCode();
        }

        [Route("ACCOUNTS_GROUP/getByID")]
        [HttpGet]
        public IHttpActionResult getByID(int ACCOUNTS_GROUPID)
        {
            return Ok( ACCOUNTS_GROUPService.GetByID(ACCOUNTS_GROUPID));
        }
        public async Task<bool> LogData(string code = null,string id=null)
        {
            Logger logger = new Logger();
            await userLogFileService.Insert(logger.LogDataMethod(code,id));
            return true;
        }
    }
}
