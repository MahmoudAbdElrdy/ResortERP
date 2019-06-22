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

namespace ResortERP.Api.Controllers
{
    public class BillTypesController : ApiController
    {
        IBillTypesService billTypesService;
        public BillTypesController(IBillTypesService billTypesService)
        {
            this.billTypesService = billTypesService;
        }

        [Route("billTypes/insert")]
        [HttpPost]
        public async Task<IHttpActionResult> add([FromBody]BILL_TYPESVM entity)
        {
            return Ok(await billTypesService.InsertAsync(entity));
        }
        [Route("billTypes/update")]
        [HttpPost]
        public async Task<IHttpActionResult> update([FromBody]BILL_TYPESVM entity)
        {
            return Ok(await billTypesService.UpdateAsync(entity));
        }
        [Route("billTypes/delete")]
        [HttpPost]
        public async Task<IHttpActionResult> delete([FromBody]BILL_TYPESVM entity)
        {
            return Ok(await billTypesService.DeleteAsync(entity));
        }
        [Route("billTypes/get")]
        [HttpGet]
        public async Task<IHttpActionResult> get(int pageNum, int pageSize)
        {
            return Ok(await billTypesService.GetAllAsync(pageNum, pageSize));
        }

        [Route("billTypes/getAll")]
        [HttpGet]
        public List<BILL_TYPESVM> getAll()
        {
            return billTypesService.GetAll();
        }

        [Route("billTypes/count")]
        [HttpGet]
        public async Task<IHttpActionResult> count()
        {
            return Ok(await billTypesService.CountAsync());
        }
    }
}