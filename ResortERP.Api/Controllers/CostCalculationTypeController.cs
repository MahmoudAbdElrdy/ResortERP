using ResortERP.Core.VM;
using ResortERP.Service.IServices;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace ResortERP.Api.Controllers
{
    public class CostCalculationTypeController : ApiController
    {
        ICostCalculationTypeService costCalculationTypeService;
        public CostCalculationTypeController(ICostCalculationTypeService costCalculationTypeService)
        {
            this.costCalculationTypeService = costCalculationTypeService;
        }

        [Route("costCalculationType/insert")]
        [HttpPost]
        public async Task<IHttpActionResult> add([FromBody]CostCalculationTypeVM entity)
        {
            return Ok(await costCalculationTypeService.InsertAsync(entity));
        }
        [Route("costCalculationType/update")]
        [HttpPost]
        public async Task<IHttpActionResult> update([FromBody]CostCalculationTypeVM entity)
        {
            return Ok(await costCalculationTypeService.UpdateAsync(entity));
        }
        [Route("costCalculationType/delete")]
        [HttpPost]
        public async Task<IHttpActionResult> delete([FromBody]CostCalculationTypeVM entity)
        {
            return Ok(await costCalculationTypeService.DeleteAsync(entity));
        }
        [Route("costCalculationType/get")]
        [HttpGet]
        public async Task<IHttpActionResult> get(int pageNum, int pageSize)
        {
            return Ok(await costCalculationTypeService.GetAllAsync(pageNum, pageSize));
        }

        [Route("costCalculationType/count")]
        [HttpGet]
        public async Task<IHttpActionResult> count()
        {
            return Ok(await costCalculationTypeService.CountAsync());
        }
        //Get Unit Data Using UnitCode
        [Route("costCalculationType/GetUnitDataUsingUnitCode")]
        [HttpGet]
        public CustomItemUnitsVM GetUnitDataUsingUnitCode(string UnitCode)
        {
            return costCalculationTypeService.GetUnitDataUsingUnitCode(UnitCode);
        }
        [Route("costCalculationType/getCostCalculationType")]
        [HttpGet]
        public Task<List<CostCalculationTypeVM>> getCostCalculationType()
        {
            return costCalculationTypeService.GetAllgetCostCalculationType();
        }

    }
}