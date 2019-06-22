using ResortERP.Core.VM;
using ResortERP.Repository;
using ResortERP.Service.IServices;
using System.Threading.Tasks;
using System.Web.Http;

namespace ResortERP.Api.Controllers
{
    [Logger]
    public class AccountDetailController : ApiController
    {
        IAcountDetailsService _AcountsDetailsService;
        IUserLogFileService _UserOperationService;
        IUserLogFileService userLogFileService;
        public AccountDetailController(IAcountDetailsService countsDetailsService,IUserLogFileService userOperationService, IUserLogFileService userLogFileService)
        {
            this._AcountsDetailsService = countsDetailsService;
            this._UserOperationService = userOperationService;
            this.userLogFileService = userLogFileService;
        }

        [Route("ACCOUNT_DETAILS/insert")]
        [HttpPost]
        public async Task<IHttpActionResult> add([FromBody]AccountDetailVM entity)
        {
            var controllerName = ControllerContext.RouteData.Values["controller"];
            var result = await _AcountsDetailsService.InsertAsync(entity);
            await LogData(null,result.ToString());
            if (result != 0)
            {
                return Ok(true);
            }
            return Ok(false);
        }
        [Route("ACCOUNT_DETAILS/update")]
        [HttpPost]
        public async Task<IHttpActionResult> update([FromBody]AccountDetailVM entity)
        {
            var result = await _AcountsDetailsService.UpdateAsync(entity);
            await LogData(null,entity.PARENT_ACC_ID.ToString());
            return Ok(result);
        }
        [Route("ACCOUNT_DETAILS/delete")]
        [HttpPost]
        public async Task<IHttpActionResult> delete([FromBody]AccountDetailVM entity)
        {
            var result = await _AcountsDetailsService.DeleteAsync(entity);
            await LogData(null,entity.PARENT_ACC_ID.ToString());
            return Ok(result);

        }
        [Route("ACCOUNT_DETAILS/get")]
        [HttpGet]
        public async Task<IHttpActionResult> get(int pageNum, int pageSize)
        {
            return Ok(await _AcountsDetailsService.GetAllAsync(pageNum, pageSize));
        }

        [Route("ACCOUNT_DETAILS/count")]
        [HttpGet]
        public async Task<IHttpActionResult> count()
        {
            return Ok(await _AcountsDetailsService.CountAsync());
        }

        public async Task<bool> LogData(string code = null, string id = null)
        {
            Logger logger = new Logger();
            await userLogFileService.Insert(logger.LogDataMethod(code,id));
            return true;
        }
    }
}