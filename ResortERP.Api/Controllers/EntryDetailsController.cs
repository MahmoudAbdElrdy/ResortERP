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
   // [Logger]
    public class EntryDetailsController : ApiController
    {
        IEntryDetailsService entryDetailsService;
        IUserLogFileService userLogFileService;
        public EntryDetailsController(IEntryDetailsService entryDetailsService, IUserLogFileService userLogFileService)
        {
            this.entryDetailsService = entryDetailsService;
            this.userLogFileService = userLogFileService;
        }

        [Route("entryDetails/insert")]
        [HttpPost]
        public async Task<IHttpActionResult> add([FromBody]Entry_DetailsVM entity)
        {
            var result = await entryDetailsService.InsertAsync(entity);
           // await LogData(null, result.ToString());
            if (result != 0)
            {
                return Ok(true);
            }
            return Ok(false);
        }
        [Route("entryDetails/update")]
        [HttpPost]
        public async Task<IHttpActionResult> update([FromBody]Entry_DetailsVM entity)
        {
            var result = await entryDetailsService.UpdateAsync(entity);
            //await LogData(entity.ACC_CODE, entity.ENTRY_ID.ToString());
            return Ok(result);
        }
        [Route("entryDetails/delete")]
        [HttpPost]
        public async Task<IHttpActionResult> delete([FromBody]Entry_DetailsVM entity)
        {
            var result = await entryDetailsService.DeleteAsync(entity);
            //await LogData(entity.ACC_CODE, entity.ENTRY_ID.ToString());
            return Ok(result);
        }
        [Route("entryDetails/get")]
        [HttpGet]
        public async Task<IHttpActionResult> get(int pageNum, int pageSize)
        {
            return Ok(await entryDetailsService.GetAllAsync(pageNum, pageSize));
        }

        [Route("entryDetails/getAll")]
        [HttpGet]
        public List<Entry_DetailsVM> getAll()
        {
            return entryDetailsService.GetAll();
        }

        [Route("entryDetails/count")]
        [HttpGet]
        public async Task<IHttpActionResult> count()
        {
            return Ok(await entryDetailsService.CountAsync());
        }
        [Route("entryDetails/SearchItems")]
        [HttpGet]
        public List<AccountVM> SearchItems(string SearchCriteria)
        {
            return  entryDetailsService.SearchItems(SearchCriteria);
        }

        [Route("entryDetails/getAccountValByaccID")]
        [HttpGet]
        public async Task<IHttpActionResult> getAccountValByaccID(int accID)
        {
            return Ok(await entryDetailsService.getAccountValByaccID(accID));
        }


        [Route("entryDetails/getAccountValByaccIDandBranchID")]
        [HttpGet]
        public async Task<IHttpActionResult> getAccountValByaccIDandBranchID(int accID,string BranchID)
        {
            return Ok(await entryDetailsService.getAccountValByaccIDandBranchID(accID, BranchID));
        }

        //public async Task<bool> LogData(string code = null,string id=null)
        //{
        //    Logger logger = new Logger();
        //   // await userLogFileService.Insert(logger.LogDataMethod(code,id));
        //    return true;
        //}
    }
}