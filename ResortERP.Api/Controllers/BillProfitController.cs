using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using Newtonsoft.Json.Linq;
using ResortERP.Core.VM;
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
    public class BillProfitController : ApiController
    {
        ICustomersService customerService;
        ICompanyStoresService companyStoreService;
        IEmployeeService employeeService;
        ICostCentersService costCenterService;

        public BillProfitController(ICustomersService customerService, ICompanyStoresService companyStoreService, IEmployeeService employeeService, ICostCentersService costCenterService)
        {
            this.customerService = customerService;
            this.companyStoreService = companyStoreService;
            this.employeeService = employeeService;
            this.costCenterService = costCenterService;
        }


        [Route("billProfit/getSearchForCustomer")]
        [HttpGet]
        public async Task<IHttpActionResult> getSearchForCustomer(string Search)
        {
            return Ok(await customerService.GetSearchForCustomer(Search));
        }

        [Route("billProfit/getSearchForStore")]
        [HttpGet]
        public async Task<IHttpActionResult> getSearchForStore(string Search)
        {
            return Ok(await companyStoreService.GetSearchForStore(Search));
        }

        [Route("billProfit/getSearchForEmployee")]
        [HttpGet]
        public async Task<IHttpActionResult> getSearchForEmployee(string Search, int TypeID)
        {
            return Ok(await employeeService.GetSearchForStore(Search, TypeID));
        }

        [Route("billProfit/getSearchForCostCenter")]
        [HttpGet]
        public async Task<IHttpActionResult> getSearchForCostCenter(string Search)
        {
            return Ok(await costCenterService.GetSearchForCostCenter(Search));
        }

        [Route("billProfit/getChartsData")]
        [HttpGet]
        public async Task<IHttpActionResult> getChartsData()
        {
 
            var q = await customerService.getChartsData();

            var result = new JArray();
            foreach (var item in q)
            {
                //if (item["Good"].Value<bool>())
                //{
                var dataPair = new JObject();
                dataPair.Add("label","");
                dataPair.Add("value", (double)item.ACC_ID);
                result.Add(dataPair);
                //  }
            }

            return Ok(result);
        }

    }
}
