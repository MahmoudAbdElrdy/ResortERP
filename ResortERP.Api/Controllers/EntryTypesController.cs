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
    public class EntryTypesController : ApiController
    {
        IEntryTypesService entryTypesService;
        public EntryTypesController(IEntryTypesService entryTypesService)
        {
            this.entryTypesService = entryTypesService;
        }

        [Route("entryTypes/insert")]
        [HttpPost]
        public async Task<IHttpActionResult> add([FromBody]Entry_TypesVM entity)
        {
            return Ok(await entryTypesService.InsertAsync(entity));
        }
        [Route("entryTypes/update")]
        [HttpPost]
        public async Task<IHttpActionResult> update([FromBody]Entry_TypesVM entity)
        {
            return Ok(await entryTypesService.UpdateAsync(entity));
        }
        [Route("entryTypes/delete")]
        [HttpPost]
        public async Task<IHttpActionResult> delete([FromBody]Entry_TypesVM entity)
        {
            return Ok(await entryTypesService.DeleteAsync(entity));
        }
        [Route("entryTypes/get")]
        [HttpGet]
        public async Task<IHttpActionResult> get(int pageNum, int pageSize)
        {
            return Ok(await entryTypesService.GetAllAsync(pageNum, pageSize));
        }

        [Route("entryTypes/getAll")]
        [HttpGet]
        public List<Entry_TypesVM> getAll()
        {
            return entryTypesService.GetAll();
        }

        [Route("entryTypes/count")]
        [HttpGet]
        public async Task<IHttpActionResult> count()
        {
            return Ok(await entryTypesService.CountAsync());
        }
    }
}