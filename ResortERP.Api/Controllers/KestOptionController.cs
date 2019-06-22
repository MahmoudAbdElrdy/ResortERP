using ResortERP.Core.VM;
using ResortERP.Service.IServices;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace ResortERP.Api.Controllers
{
    public class KestOptionController : ApiController
    {
        IKestOptionService KestOptService;
        public KestOptionController(IKestOptionService KestOptService)
        {
            this.KestOptService = KestOptService;
        }

        [Route("kestOption/add")]
        [HttpPost]
        public async Task<IHttpActionResult> add([FromBody]Kest_OptionVM entity)
        {
            return Ok(await KestOptService.InsertAsync(entity));
        }
        [Route("kestOption/update")]
        [HttpPost]
        public async Task<IHttpActionResult> update([FromBody]Kest_OptionVM entity)
        {
            return Ok(await KestOptService.UpdateAsync(entity));
        }
        [Route("kestOption/delete")]
        [HttpPost]
        public async Task<IHttpActionResult> delete([FromBody]Kest_OptionVM entity)
        {
            return Ok(await KestOptService.DeleteAsync(entity));
        }
        [Route("kestOption/get")]
        [HttpGet]
        public async Task<IHttpActionResult> get(int pageNum, int pageSize)
        {
            return Ok(await KestOptService.GetAllAsync(pageNum, pageSize));
        }
        [Route("kestOption/getAll")]
        [HttpGet]
        public List<Kest_OptionVM> getAll()
        {
            return KestOptService.GetAll();
        }

        [Route("kestOption/count")]
        [HttpGet]
        public async Task<IHttpActionResult> count()
        {
            return Ok(await KestOptService.CountAsync());
        }
    }
}
