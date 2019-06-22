using ResortERP.Core.VM;
using ResortERP.Service.IServices;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace ResortERP.Api.Controllers
{
    public class IncomeAccountTypesController : ApiController
    {
        IIncomeAccountTypesService incomeAccTypService;
        public IncomeAccountTypesController(IIncomeAccountTypesService incomeAccTypService)
        {
            this.incomeAccTypService = incomeAccTypService;
        }

        [Route("incomeAccountType/add")]
        [HttpPost]
        public async Task<IHttpActionResult> add([FromBody]Income_Account_TypesVM entity)
        {
            return Ok(await incomeAccTypService.InsertAsync(entity));
        }
        [Route("incomeAccountType/update")]
        [HttpPost]
        public async Task<IHttpActionResult> update([FromBody]Income_Account_TypesVM entity)
        {
            return Ok(await incomeAccTypService.UpdateAsync(entity));
        }
        [Route("incomeAccountType/delete")]
        [HttpPost]
        public async Task<IHttpActionResult> delete([FromBody]Income_Account_TypesVM entity)
        {
            return Ok(await incomeAccTypService.DeleteAsync(entity));
        }
        [Route("incomeAccountType/get")]
        [HttpGet]
        public async Task<IHttpActionResult> get(int pageNum, int pageSize)
        {
            return Ok(await incomeAccTypService.GetAllAsync(pageNum, pageSize));
        }
        [Route("incomeAccountType/getAll")]
        [HttpGet]
        public List<Income_Account_TypesVM> getAll()
        {
            return incomeAccTypService.GetAll();
        }
         
        [Route("incomeAccountType/count")]
        [HttpGet]
        public async Task<IHttpActionResult> count()
        {
            return Ok(await incomeAccTypService.CountAsync());
        }
    }
}
