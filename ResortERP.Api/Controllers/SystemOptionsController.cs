using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ResortERP.Core;
using ResortERP.Core.VM;
using ResortERP.Repository;
using ResortERP.Service.IServices;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace ResortERP.Api.Controllers
{
    [Logger]
    public class SystemOptionsController : ApiController
    {
        ISystemOptionsService sysOptService;
        IUserLogFileService userLogFileService;
        public SystemOptionsController(ISystemOptionsService sysOptService, IUserLogFileService userLogFileService)
        {
            this.sysOptService = sysOptService;
            this.userLogFileService = userLogFileService;
        }

        [Route("sysOpt/insert")]
        [HttpPost]
        public async Task<IHttpActionResult> add([FromBody]System_OptionsVM entity)
        {
            var result = await sysOptService.InsertAsync(entity);
           // LogData();
            return Ok(result);

        }

        [Route("sysOpt/SaveAll")]
        [HttpPost]
        public async Task<IHttpActionResult> SaveAll(SystemOptionsComplexVM ComplexObj)
        {
            var result = await sysOptService.SaveAll(ComplexObj);
            await LogData(null,ComplexObj.SystemOptions.UID);
            return Ok(result);
        }

        [Route("sysOpt/update")]
        [HttpPost]
        public async Task<IHttpActionResult> update([FromBody]System_OptionsVM entity)
        {
            var result = await sysOptService.UpdateAsync(entity);
            await LogData(null,entity.UID);
            return Ok(result);

        }
        [Route("sysOpt/delete")]
        [HttpPost]
        public async Task<IHttpActionResult> delete([FromBody]System_OptionsVM entity)
        {
            var result = await sysOptService.DeleteAsync(entity);
            await LogData(null,entity.UID);
            return Ok(result);

        }
        [Route("sysOpt/get")]
        [HttpGet]
        public async Task<IHttpActionResult> get(int pageNum, int pageSize)
        {
            return Ok(await sysOptService.GetAllAsync(pageNum, pageSize));
        }

        [Route("sysOpt/getByUserName")]
        [HttpGet]
        public async Task<IHttpActionResult> getByUserName(string userName)
        {
            return Ok(await sysOptService.GetByUserName(userName));
        }

        [Route("sysOpt/getAll")]
        [HttpGet]
        public List<System_OptionsVM> getAll()
        {
            return sysOptService.GetAll();
        }

        [Route("sysOpt/count")]
        [HttpGet]
        public async Task<IHttpActionResult> count()
        {
            return Ok(await sysOptService.CountAsync());
        }

        [Route("sysOpt/insertFilterForCompany")]
        [HttpPost]
        public IHttpActionResult insertFilterForCompany(string[] entity)
        {
            var result = sysOptService.insertFilterForCompany(entity);
            return Ok(result);

        }
        [Route("sysOpt/getFilterCompValue")]
        [HttpGet]
        public IHttpActionResult getFilterCompValue()
        {
            return Ok(sysOptService.getFilterCompValue());
        }
        public async Task<bool> LogData(string code = null,string id=null)
        {
            Logger logger = new Logger();
            await userLogFileService.Insert(logger.LogDataMethod(code,id));
            return true;
        }

    }
}