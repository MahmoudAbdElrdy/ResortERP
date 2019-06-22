using ResortERP.Core.VM;
using ResortERP.Service.IServices;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace ResortERP.Api.Controllers
{
    public class BillOptionController : ApiController
    {
        IBillOptionService billOptionService;
        public BillOptionController(IBillOptionService billOptionService)
        {
            this.billOptionService = billOptionService;
        }

        [Route("billOption/insert")]
        [HttpPost]
        public async Task<IHttpActionResult> add([FromBody]BILL_OPTIONSVM entity)
        {
            return Ok(await billOptionService.InsertAsync(entity));
        }
        [Route("billOption/update")]
        [HttpPost]
        public async Task<IHttpActionResult> update([FromBody]BILL_OPTIONSVM entity)
        {
            return Ok(await billOptionService.UpdateAsync(entity));
        }
        [Route("billOption/delete")]
        [HttpPost]
        public async Task<IHttpActionResult> delete([FromBody]BILL_OPTIONSVM entity)
        {
            return Ok(await billOptionService.DeleteAsync(entity));
        }
        [Route("billOption/get")]
        [HttpGet]
        public async Task<IHttpActionResult> get(int pageNum, int pageSize)
        {
            return Ok(await billOptionService.GetAllAsync(pageNum, pageSize));
        }

        [Route("billOption/getAll")]
        [HttpGet]
        public List<BILL_OPTIONSVM> getAll()
        {
            return billOptionService.GetAll();
        }

        [Route("billOption/count")]
        [HttpGet]
        public async Task<IHttpActionResult> count()
        {
            return Ok(await billOptionService.CountAsync());
        }
    }
}