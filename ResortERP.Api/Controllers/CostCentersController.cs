using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using ResortERP.Core.VM;
using ResortERP.Repository;
using ResortERP.Service;
using ResortERP.Service.IServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web.Http;

namespace ResortERP.Api.Controllers
{
    [Logger]
    public class CostCentersController : ApiController
    {
        ICostCentersService costCentersService;
        IBillMasterService billMasterService;
        IEntryDetailsService entryDetailsService;
        IUserLogFileService userLogFileService;
        public CostCentersController(ICostCentersService costCentersService, IBillMasterService billMasterService,IEntryDetailsService entryDetailsService, IUserLogFileService _userLogFileService)
        {
            this.costCentersService = costCentersService;
            this.billMasterService = billMasterService;
            this.entryDetailsService = entryDetailsService;
            this.userLogFileService = _userLogFileService;
        }

        [Route("costCenters/insert")]
        [HttpPost]
        public async Task<IHttpActionResult> add([FromBody]CostCentersVM entity)
        {
            var result = await costCentersService.InsertAsync(entity);
            await LogData(entity.COST_CENTER_CODE, result.ToString());
            if (result != 0)
            {
                return Ok(true);
            }
            return Ok(false);
        }
        [Route("costCenters/update")]
        [HttpPost]
        public async Task<IHttpActionResult> update([FromBody]CostCentersVM entity)
        {
            var result = await costCentersService.UpdateAsync(entity);
            await LogData(entity.COST_CENTER_CODE, entity.COST_CENTER_ID.ToString());
            return Ok(result);

        }
        [Route("costCenters/delete")]
        [HttpPost]
        public async Task<IHttpActionResult> delete([FromBody]CostCentersVM entity)
        {
            var q1 = entryDetailsService.getByCostCenter(entity.COST_CENTER_ID);
            var q = billMasterService.getByCostCenter(entity.COST_CENTER_ID);
            if (q.Count == 0&& q1.Count == 0)
            {
                var result = await costCentersService.DeleteAsync(entity);
                await LogData(entity.COST_CENTER_CODE, entity.COST_CENTER_ID.ToString());
                return Ok(result);
            }

            else
                return Ok(false);
            //return Ok(await costCentersService.DeleteAsync(entity));
        }
        [Route("costCenters/get")]
        [HttpGet]
        public async Task<IHttpActionResult> get(int pageNum, int pageSize)
        {
            return Ok(await costCentersService.GetAllAsync(pageNum, pageSize));
        }

        [Route("costCenters/count")]
        [HttpGet]
        public async Task<IHttpActionResult> count()
        {
            return Ok(await costCentersService.CountAsync());
        }
        [Route("costCenters/getAll")]
        [HttpGet]
        public List<CostCentersVM> getAll()
        {
            return costCentersService.GetAll();
        }

        [Route("costCenters/getById")]
        [HttpGet]
        public CostCentersVM getById(int COST_CENTER_ID)
        {
            return costCentersService.getById(COST_CENTER_ID);
        }


        [Route("costCenters/getMainCostCenters")]
        [HttpGet]
        public List<CostCentersVM> getMainCostCenters()
        {
            return costCentersService.GetMainCostCenters();
        }



        [Route("costCenters/getLastCode")]
        [HttpGet]
        public string getLastCode()
        {
            return costCentersService.GetLastCode();
        }

        [Route("costCenters/getGuid")]
        [HttpGet]
        public IHttpActionResult get()
        {
            return Ok(costCentersService.getForTree());
        }

        public async Task<bool> LogData(string code = null,string id=null)
        {
            Logger logger = new Logger();
            await userLogFileService.Insert(logger.LogDataMethod(code,id));
            return true;
        }

    }
}
