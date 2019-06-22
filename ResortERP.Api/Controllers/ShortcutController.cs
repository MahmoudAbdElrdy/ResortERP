using ResortERP.Core.VM;
using ResortERP.Service.IServices;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace ResortERP.Api.Controllers
{
    public class ShortcutController : ApiController
    {
        IShortcutService shortCutService;
        public ShortcutController(IShortcutService shortCutService)
        {
            this.shortCutService = shortCutService;
        }

        [Route("shortcut/insert")]
        [HttpPost]
        public async Task<IHttpActionResult> add([FromBody]ShortcutsVM entity)
        {
            return Ok(await shortCutService.InsertAsync(entity));
        }
        [Route("shortcut/update")]
        [HttpPost]
        public async Task<IHttpActionResult> update([FromBody]ShortcutsVM entity)
        {
            return Ok(await shortCutService.UpdateAsync(entity));
        }
        [Route("shortcut/delete")]
        [HttpPost]
        public async Task<IHttpActionResult> delete([FromBody]ShortcutsVM entity)
        {
            return Ok(await shortCutService.DeleteAsync(entity));
        }
        [Route("shortcut/get")]
        [HttpGet]
        public async Task<IHttpActionResult> get(int pageNum, int pageSize)
        {
            return Ok(await shortCutService.GetAllAsync(pageNum, pageSize));
        }

        [Route("shortcut/getByUID/{userName}")]
        [HttpGet]
        public async Task<IHttpActionResult> getByUID(string userName)
        {
            return Ok(await shortCutService.getByUID(userName));
        }

        [Route("shortcut/getAll")]
        [HttpGet]
        public List<ShortcutsVM> getAll()
        {
            return shortCutService.GetAll();
        }

        [Route("shortcut/count")]
        [HttpGet]
        public async Task<IHttpActionResult> count()
        {
            return Ok(await shortCutService.CountAsync());
        }
    }
}