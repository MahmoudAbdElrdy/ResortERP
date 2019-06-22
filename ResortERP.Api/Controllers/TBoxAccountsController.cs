using ResortERP.Core.VM;
using ResortERP.Service.IServices;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace ResortERP.Api.Controllers
{
    public class TBoxAccountsController : ApiController
    {
        ITBoxAccountsService tBoxAccService;
        public TBoxAccountsController(ITBoxAccountsService tBoxAccService)
        {
            this.tBoxAccService = tBoxAccService;
        }

        [Route("tBoxAccounts/insert")]
        [HttpPost]
        public async Task<IHttpActionResult> add([FromBody]TBoxAccountsVM entity)
        {
            return Ok(await tBoxAccService.InsertAsync(entity));
        }
        [Route("tBoxAccounts/update")]
        [HttpPost]
        public async Task<IHttpActionResult> update([FromBody]TBoxAccountsVM entity)
        {
            return Ok(await tBoxAccService.UpdateAsync(entity));
        }
        [Route("tBoxAccounts/delete")]
        [HttpPost]
        public async Task<IHttpActionResult> delete([FromBody]TBoxAccountsVM entity)
        {
            return Ok(await tBoxAccService.DeleteAsync(entity));
        }
        [Route("tBoxAccounts/get")]
        [HttpGet]
        public async Task<IHttpActionResult> get(int pageNum, int pageSize)
        {
            return Ok(await tBoxAccService.GetAllAsync(pageNum, pageSize));
        }

        [Route("tBoxAccounts/getByUID/{userName}")]
        [HttpGet]
        public async Task<IHttpActionResult> getByUID(string userName)
        {
            return Ok(await tBoxAccService.GetByUID(userName));
        }

        [Route("tBoxAccounts/getAll")]
        [HttpGet]
        public List<TBoxAccountsVM> getAll()
        {
            return tBoxAccService.GetAll();
        }

        [Route("tBoxAccounts/count")]
        [HttpGet]
        public async Task<IHttpActionResult> count()
        {
            return Ok(await tBoxAccService.CountAsync());
        }
    }
}