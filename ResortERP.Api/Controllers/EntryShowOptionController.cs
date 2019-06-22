using ResortERP.Core.VM;
using ResortERP.Service.IServices;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace ResortERP.Api.Controllers
{
    public class EntryShowOptionController : ApiController
    {
        IEntryShowOptionService entryShowOptionService;
        public EntryShowOptionController(IEntryShowOptionService entryShowOptionService)
        {
            this.entryShowOptionService = entryShowOptionService;
        }

        [Route("entryShowOption/insert")]
        [HttpPost]
        public async Task<IHttpActionResult> add([FromBody]Entry_Show_OptionsVM entity)
        {
            return Ok(await entryShowOptionService.InsertAsync(entity));
        }
        [Route("entryShowOption/update")]
        [HttpPost]
        public async Task<IHttpActionResult> update([FromBody]Entry_Show_OptionsVM entity)
        {
            return Ok(await entryShowOptionService.UpdateAsync(entity));
        }
        [Route("entryShowOption/delete")]
        [HttpPost]
        public async Task<IHttpActionResult> delete([FromBody]Entry_Show_OptionsVM entity)
        {
            return Ok(await entryShowOptionService.DeleteAsync(entity));
        }
        [Route("entryShowOption/get")]
        [HttpGet]
        public async Task<IHttpActionResult> get(int pageNum, int pageSize)
        {
            return Ok(await entryShowOptionService.GetAllAsync(pageNum, pageSize));
        }

        [Route("entryShowOption/getAll")]
        [HttpGet]
        public List<Entry_Show_OptionsVM> getAll()
        {
            return entryShowOptionService.GetAll();
        }

        [Route("entryShowOption/count")]
        [HttpGet]
        public async Task<IHttpActionResult> count()
        {
            return Ok(await entryShowOptionService.CountAsync());
        }
    }
}