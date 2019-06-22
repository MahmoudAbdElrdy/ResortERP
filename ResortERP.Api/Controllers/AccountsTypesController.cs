using ResortERP.Core.VM;
using ResortERP.Repository;
using ResortERP.Service.IServices;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace ResortERP.Api.Controllers
{
    [Logger]
    public class AccountsTypesController : ApiController
    {
        IAcountsTypesService accountTypesService;
        IUserLogFileService userLogFileService;
        public AccountsTypesController(IAcountsTypesService accountTypesService, IUserLogFileService userLogFileService)
        {
            this.accountTypesService = accountTypesService;
            this.userLogFileService = userLogFileService;
        }

        [Route("accountType/add")]
        [HttpPost]
        public async Task<IHttpActionResult> add([FromBody]AccountTypeVM entity)
        {
            var result = await accountTypesService.InsertAsync(entity);
            await LogData(null,result.ToString());
            if (result != 0)
            {
                return Ok(true);
            }
            return Ok(false);
        }
        [Route("accountType/update")]
        [HttpPost]
        public async Task<IHttpActionResult> update([FromBody]AccountTypeVM entity)
        {
            var result = await accountTypesService.UpdateAsync(entity);
            await LogData(null,entity.ACC_TYPE_ID.ToString());
            return Ok(result);
        }
        [Route("accountType/delete")]
        [HttpPost]
        public async Task<IHttpActionResult> delete([FromBody]AccountTypeVM entity)
        {
            var result = await accountTypesService.DeleteAsync(entity);
            await LogData(null,entity.ACC_TYPE_ID.ToString());
            return Ok(result);
        }
        [Route("accountType/get")]
        [HttpGet]
        public async Task<IHttpActionResult> get(int pageNum, int pageSize)
        {
            return Ok(await accountTypesService.GetAllAsync(pageNum, pageSize));
        }
        [Route("accountType/getAll")]
        [HttpGet]
        public List<AccountTypeVM> getAll()
        {
            return accountTypesService.GetAll();
        }

        [Route("accountType/getCustomerSupplier")]
        [HttpGet]
        public List<AccountTypeVM> getCustomerSupplier(char type)
        {
            return accountTypesService.GetCustomerSupplier(type);
        }

        [Route("accountType/count")]
        [HttpGet]
        public async Task<IHttpActionResult> count()
        {
            return Ok(await accountTypesService.CountAsync());
        }


        [Route("accountType/getByQueryString")]
        [HttpGet]
        public List<AccountTypeVM> getByQuerystring(char type)
        {
            return accountTypesService.GetByTypeQueryString(type);
        }

        public async Task<bool> LogData(string code = null, string id = null)
        {
            Logger logger = new Logger();
            await userLogFileService.Insert(logger.LogDataMethod(code,id));
            return true;
        }
    }
}
