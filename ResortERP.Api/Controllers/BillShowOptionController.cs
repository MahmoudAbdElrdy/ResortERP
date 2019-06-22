using ResortERP.Core.VM;
using ResortERP.Service.IServices;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace ResortERP.Api.Controllers
{
    public class BillShowOptionController : ApiController
    {
        IBillShowOptionService billShowOptionService;
        public BillShowOptionController(IBillShowOptionService billShowOptionService)
        {
            this.billShowOptionService = billShowOptionService;
        }

        [Route("billShowOption/insert")]
        [HttpPost]
        public async Task<IHttpActionResult> add([FromBody]BILL_SHOW_OPTIONSVM entity)
        {
            return Ok(await billShowOptionService.InsertAsync(entity));
        }
        [Route("billShowOption/update")]
        [HttpPost]
        public async Task<IHttpActionResult> update([FromBody]BILL_SHOW_OPTIONSVM entity)
        {
            return Ok(await billShowOptionService.UpdateAsync(entity));
        }
        [Route("billShowOption/delete")]
        [HttpPost]
        public async Task<IHttpActionResult> delete([FromBody]BILL_SHOW_OPTIONSVM entity)
        {
            return Ok(await billShowOptionService.DeleteAsync(entity));
        }
        [Route("billShowOption/get")]
        [HttpGet]
        public async Task<IHttpActionResult> get(int pageNum, int pageSize)
        {
            return Ok(await billShowOptionService.GetAllAsync(pageNum, pageSize));
        }

        [Route("billShowOption/getAll")]
        [HttpGet]
        public List<BILL_SHOW_OPTIONSVM> getAll()
        {
            return billShowOptionService.GetAll();
        }

        [Route("billShowOption/count")]
        [HttpGet]
        public async Task<IHttpActionResult> count()
        {
            return Ok(await billShowOptionService.CountAsync());
        }
    }
}