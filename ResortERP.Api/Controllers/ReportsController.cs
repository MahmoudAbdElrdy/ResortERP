using ResortERP.Core.VM;
using ResortERP.Service;
using ResortERP.Service.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using ResortERP.Core;
using ResortERP.Repository;
using SolversTeamERP.Core.VM;

namespace ResortERP.Api.Controllers
{
    [Logger]
    public class ReportsController : ApiController
    {
        IReportsService _ReportsService;
        public ReportsController(IReportsService ReportsService)
        {
            this._ReportsService = ReportsService;
        }

        //[Route("Reports/add")]
        //[HttpPost]
        //public async Task<IHttpActionResult> add([FromBody]ReportsVM entity)
        //{
        //    if (!_ReportsService.CheckNameAndCode(entity.ID, entity.ARName, entity.LatName, entity.Code))
        //    {
        //        var result = await _ReportsService.InsertAsync(entity);
        //        await LogData(entity.Code, result.ToString());
        //        if (result != 0)
        //        {
        //            return Ok(true);
        //        }
        //    }
        //    return Ok(false);
        //}

        [Route("Reports/getBranchesList")]
        [HttpGet]
        public async Task<IHttpActionResult> getBranchesList()
        {
            return Ok(await _ReportsService.getBranchesList());
        }

        [Route("Reports/getCostCenterList")]
        [HttpGet]
        public async Task<IHttpActionResult> getCostCenterList()
        {
            return Ok(await _ReportsService.getCostCenterList());
        }

        [Route("Reports/getStoresList")]
        [HttpGet]
        public async Task<IHttpActionResult> getStoresList()
        {
            return Ok(await _ReportsService.getStoresList());
        }

        [Route("Reports/getItemGroupList")]
        [HttpGet]
        public async Task<IHttpActionResult> getItemGroupList()
        {
            return Ok(await _ReportsService.getItemGroupList());
        }
        
        [Route("Reports/getSellTypeList")]
        [HttpGet]
        public async Task<IHttpActionResult> getSellTypeList()
        {
            return Ok(await _ReportsService.getSellTypeList());
        }

        [Route("Reports/getAccountsByType")]
        [HttpGet]
        public async Task<IHttpActionResult> getAccountsByType(string Type)
        {
            return Ok(await _ReportsService.getAccountsByType(Type));
        }

        [Route("Reports/getItemsInventoryReportResult")]
        [HttpPost]
        public async Task<IHttpActionResult> getItemsInventoryReportResult(ItemsInventoryReportSearchVM searchObject)
        {
            return Ok(await _ReportsService.getItemsInventoryReportResult(searchObject));
        }

        [Route("Reports/getItemsInventoryBalanceResult")]
        [HttpPost]
        public async Task<IHttpActionResult> getItemsInventoryBalanceResult(ItemsInventoryReportSearchVM searchObject)
        {
            return Ok(await _ReportsService.getItemsInventoryBalanceResult(searchObject));
        }

        [Route("Reports/getAccountBalancesReportResult")]
        [HttpPost]
        public async Task<IHttpActionResult> getAccountBalancesReportResult(AccountBalancesReportSearchVM searchObject)
        {
            return Ok(await _ReportsService.getAccountBalancesReportResult(searchObject));
        }
    }
}
