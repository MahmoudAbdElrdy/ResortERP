using ResortERP.Core.VM;
using ResortERP.Service.IServices;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace ResortERP.Api.Controllers
{
    public class TBudgetAccountsController : ApiController
    {
        ITBudgetAccountsService tBudAccService;
        public TBudgetAccountsController(ITBudgetAccountsService tBudAccService)
        {
            this.tBudAccService = tBudAccService;
        }

        [Route("tBudAcc/insert")]
        [HttpPost]
        public async Task<IHttpActionResult> add([FromBody]TBudgetAccountsVM entity)
        {
            return Ok(await tBudAccService.InsertAsync(entity));
        }
        [Route("tBudAcc/update")]
        [HttpPost]
        public async Task<IHttpActionResult> update([FromBody]TBudgetAccountsVM entity)
        {
            return Ok(await tBudAccService.UpdateAsync(entity));
        }
        [Route("tBudAcc/delete")]
        [HttpPost]
        public async Task<IHttpActionResult> delete([FromBody]TBudgetAccountsVM entity)
        {
            return Ok(await tBudAccService.DeleteAsync(entity));
        }
        [Route("tBudAcc/get")]
        [HttpGet]
        public async Task<IHttpActionResult> get(int pageNum, int pageSize)
        {
            return Ok(await tBudAccService.GetAllAsync(pageNum, pageSize));
        }

        [Route("tBudAcc/getByUID/{userName}")]
        [HttpGet]
        public async Task<IHttpActionResult> getByUID(string userName)
        {
            return Ok(await tBudAccService.GetByUID(userName));
        }

        [Route("tBudAcc/getAll")]
        [HttpGet]
        public List<TBudgetAccountsVM> getAll()
        {
            return tBudAccService.GetAll();
        }

        [Route("tBudAcc/count")]
        [HttpGet]
        public async Task<IHttpActionResult> count()
        {
            return Ok(await tBudAccService.CountAsync());
        }
    }
}