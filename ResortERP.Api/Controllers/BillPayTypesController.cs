using ResortERP.Core.VM;
using ResortERP.Service.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace ResortERP.Api.Controllers
{
    public class BillPayTypesController : ApiController
    {
        IBillPayTypesService billPaytypeService;

        public BillPayTypesController(IBillPayTypesService _billPaytypeService)
        {
            this.billPaytypeService = _billPaytypeService;
        }

        [Route("billPaytypes/get")]
        [HttpGet]
        public async Task<IHttpActionResult> getAllBillPaytypes(int pageNum, int pageSize)
        {
            return Ok(await billPaytypeService.GetAllAsync(pageNum, pageSize));
        }

        [Route("billPaytypes/count")]
        [HttpGet]
        public async Task<IHttpActionResult> countBillPayTypes()
        {
            return Ok(await billPaytypeService.CountAsync());
        }


        [Route("billPaytypes/insert")]
        [HttpPost]
        public async Task<IHttpActionResult> insertBillPayTypes(BILL_PAY_TYPESVM billPay)
        {
            return Ok(await billPaytypeService.InsertAsync(billPay));
        }

        [Route("billPaytypes/update")]
        [HttpPost]
        public async Task<IHttpActionResult> updateBillPayTypes(BILL_PAY_TYPESVM billPay)
        {
            return Ok(await billPaytypeService.UpdateAsync(billPay));
        }

        [Route("billPaytypes/delete")]
        [HttpPost]
        public async Task<IHttpActionResult> deleteBillPayTypes(BILL_PAY_TYPESVM billPay)
        {
            return Ok(await billPaytypeService.DeleteAsync(billPay));
        }


        [Route("billPaytypes/getByMasterID")]
        [HttpGet]
        public async Task<IHttpActionResult> getByMasterID(int masterID)
        {
            return Ok(await billPaytypeService.getByMasterID(masterID));
        }




        [Route("billPaytypes/insertWithMasterID")]
        [HttpPost]
        public async Task<IHttpActionResult> InsertWithMasterID(List<BILL_PAY_TYPESVM> PayList)
        {
            return Ok(await billPaytypeService.InsertWithMasterID(PayList));
        }

    }
}
