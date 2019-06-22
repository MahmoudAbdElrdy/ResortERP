using ResortERP.Core.VM;
using ResortERP.Service.IServices;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace ResortERP.Api.Controllers
{
    public class TStoreController : ApiController
    {
        ITStoreService tStoreService;
        public TStoreController(ITStoreService tStoreService)
        {
            this.tStoreService = tStoreService;
        }

        [Route("tStore/insert")]
        [HttpPost]
        public async Task<IHttpActionResult> add([FromBody]TStoreVM entity)
        {
            return Ok(await tStoreService.InsertAsync(entity));
        }
        [Route("tStore/update")]
        [HttpPost]
        public async Task<IHttpActionResult> update([FromBody]TStoreVM entity)
        {
            return Ok(await tStoreService.UpdateAsync(entity));
        }
        [Route("tStore/delete")]
        [HttpPost]
        public async Task<IHttpActionResult> delete([FromBody]TStoreVM entity)
        {
            return Ok(await tStoreService.DeleteAsync(entity));
        }
        [Route("tStore/get")]
        [HttpGet]
        public async Task<IHttpActionResult> get(int pageNum, int pageSize)
        {
            return Ok(await tStoreService.GetAllAsync(pageNum, pageSize));
        }

        [Route("tStore/getByUID/{userName}")]
        [HttpGet]
        public async Task<IHttpActionResult> getByUID(string userName)
        {
            return Ok(await tStoreService.GetByUID(userName));
        }

        [Route("tStore/getAll")]
        [HttpGet]
        public List<TStoreVM> getAll()
        {
            return tStoreService.GetAll();
        }

        [Route("tStore/count")]
        [HttpGet]
        public async Task<IHttpActionResult> count()
        {
            return Ok(await tStoreService.CountAsync());
        }
    }
}