using ResortERP.Core.VM;
using ResortERP.Service.IServices;
using System.Threading.Tasks;
using System.Web.Http;

namespace ResortERP.Api.Controllers
{
    public class BillEffectsController : ApiController
    {
        IBillEffectsService billEffectsService;
        public BillEffectsController(IBillEffectsService billEffectsService)
        {
            this.billEffectsService = billEffectsService;
        }

        [Route("billEffects/insert")]
        [HttpPost]
        public async Task<IHttpActionResult> add([FromBody]BILL_EFFECTSVM entity)
        {
            return Ok(await billEffectsService.InsertAsync(entity));
        }
        [Route("billEffects/update")]
        [HttpPost]
        public async Task<IHttpActionResult> update([FromBody]BILL_EFFECTSVM entity)
        {
            return Ok(await billEffectsService.UpdateAsync(entity));
        }
        [Route("billEffects/delete")]
        [HttpPost]
        public async Task<IHttpActionResult> delete([FromBody]BILL_EFFECTSVM entity)
        {
            return Ok(await billEffectsService.DeleteAsync(entity));
        }
        [Route("billEffects/get")]
        [HttpGet]
        public async Task<IHttpActionResult> get(int pageNum, int pageSize)
        {
            return Ok(await billEffectsService.GetAllAsync(pageNum, pageSize));
        }

        [Route("billEffects/count")]
        [HttpGet]
        public async Task<IHttpActionResult> count()
        {
            return Ok(await billEffectsService.CountAsync());
        }
    }
}