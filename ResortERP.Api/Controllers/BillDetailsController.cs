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

namespace ResortERP.Api.Controllers
{
    public class BillDetailsController : ApiController
    {
        IBillDetailsService billDetailsService;
        public BillDetailsController(IBillDetailsService billDetailsService)
        {
            this.billDetailsService = billDetailsService;
        }

        [Route("billDetails/add")]
        [HttpPost]
        public async Task<IHttpActionResult> add([FromBody]BILL_DETAILSVM entity)
        {
            return Ok(await billDetailsService.InsertAsync(entity));
        }
        [Route("billDetails/addBillDetails")]
        [HttpPost]
        public bool addBillDetails(List<BILL_DETAILSVM> BillDetails)
        {
            return billDetailsService.InsertBillDetails(BillDetails);
        }

        [Route("billDetails/update")]
        [HttpPost]
        public async Task<IHttpActionResult> update([FromBody]BILL_DETAILSVM entity)
        {
            return Ok(await billDetailsService.UpdateAsync(entity));
        }
        [Route("billDetails/delete")]
        [HttpPost]
        public async Task<IHttpActionResult> delete([FromBody]BILL_DETAILSVM entity)
        {
            return Ok(await billDetailsService.DeleteAsync(entity));
        }
        [Route("billDetails/get")]
        [HttpGet]
        public async Task<IHttpActionResult> get(int pageNum, int pageSize)
        {
            return Ok(await billDetailsService.GetAllAsync(pageNum, pageSize));
        }

        [Route("billDetails/count")]
        [HttpGet]
        public async Task<IHttpActionResult> count()
        {
            return Ok(await billDetailsService.CountAsync());
        }

        [Route("billDetails/GetAllBillDetailsByBill_ID")]
        [HttpGet]
        public  List<BILL_DETAILSVM> GetAllBillDetailsByBill_ID(long Bill_ID)
        {
            return billDetailsService.GetAllBillDetailsByBill_ID(Bill_ID);
        }

    }
}