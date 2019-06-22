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
    public class SELLS_TYPESController : ApiController
    {
        ISELLS_TYPESService SellsTypesService;
        public SELLS_TYPESController(ISELLS_TYPESService SellsTypesService)
        {
            this.SellsTypesService = SellsTypesService;
        }

        [Route("SELLS_TYPES/add")]
        [HttpPost]
        public async Task<IHttpActionResult> add([FromBody]SELLS_TYPESVM entity)
        {
            return Ok(await SellsTypesService.InsertAsync(entity));
        }
        [Route("SELLS_TYPES/update")]
        [HttpPost]
        public async Task<IHttpActionResult> update([FromBody]SELLS_TYPESVM entity)
        {
            return Ok(await SellsTypesService.UpdateAsync(entity));
        }
        [Route("SELLS_TYPES/delete")]
        [HttpPost]
        public async Task<IHttpActionResult> delete([FromBody]SELLS_TYPESVM entity)
        {
            return Ok(await SellsTypesService.DeleteAsync(entity));
        }
        [Route("SELLS_TYPES/get")]
        [HttpGet]
        public async Task<IHttpActionResult> get(int pageNum, int pageSize)
        {
            return Ok(await SellsTypesService.GetAllAsync(pageNum, pageSize));
        }

        [Route("SELLS_TYPES/count")]
        [HttpGet]
        public async Task<IHttpActionResult> count()
        {
            return Ok(await SellsTypesService.CountAsync());
        }

        [Route("SELLS_TYPES/getAll")]
        [HttpGet]
        public List<SELLS_TYPESVM> getAll()
        {
            return SellsTypesService.GetAll();
        }

    }
}
