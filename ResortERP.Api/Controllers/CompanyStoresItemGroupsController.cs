using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ResortERP.Core.VM;
using ResortERP.Service;
using ResortERP.Service.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Script.Serialization;

namespace ResortERP.Api.Controllers
{
    public class CompanyStoresItemGroupsController : ApiController
    {
        ICompanyStoresItemGroupsService csigService;
        public CompanyStoresItemGroupsController(ICompanyStoresItemGroupsService csigService)
        {
            this.csigService = csigService;
        }

        [Route("csig/add")]
        [HttpPost]
        public async Task<IHttpActionResult> add([FromBody]CompStore_ItmGroupsVM entity)
        {
            return Ok(await csigService.InsertAsync(entity));
        }

        [Route("csig/update")]
        [HttpPost]
        public async Task<IHttpActionResult> update([FromBody]CompStore_ItmGroupsVM entity)
        {
            return Ok(await csigService.UpdateAsync(entity));
        }
        [Route("csig/delete")]
        [HttpPost]
        public async Task<IHttpActionResult> delete([FromBody]CompStore_ItmGroupsVM entity)
        {
            return Ok(await csigService.DeleteAsync(entity));
        }
        [Route("csig/get")]
        [HttpGet]
        public async Task<IHttpActionResult> get(int pageNum, int pageSize, int CompanyStoreID)
        {
            return Ok(await csigService.GetAllAsync(pageNum, pageSize, CompanyStoreID));
        }
        [Route("csig/getAll")]
        [HttpGet]
        public List<CompStore_ItmGroupsVM> getAll(int CompanyStoreID)
        {
            return csigService.GetAll(CompanyStoreID);
        }

        [Route("csig/count")]
        [HttpGet]
        public async Task<IHttpActionResult> count(int CompanyStoreID)
        {
            return Ok(await csigService.CountAsync(CompanyStoreID));
        }
    }
}
