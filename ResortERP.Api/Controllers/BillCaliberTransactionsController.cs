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
    public class BillCaliberTransactionsController : ApiController
    {
        IBillCaliberTransactionsService billCaliberTransService;

        public BillCaliberTransactionsController(IBillCaliberTransactionsService _billCaliberTransService)
        {
            this.billCaliberTransService = _billCaliberTransService;
        }

        [Route("billCaliberTrans/get")]
        [HttpGet]
        public async Task<IHttpActionResult> getAllBillPaytypes(int pageNum, int pageSize)
        {
            return Ok(await billCaliberTransService.GetAllAsync(pageNum, pageSize));
        }

        [Route("billCaliberTrans/count")]
        [HttpGet]
        public async Task<IHttpActionResult> countBillPayTypes()
        {
            return Ok(await billCaliberTransService.CountAsync());
        }


        [Route("billCaliberTrans/insert")]
        [HttpPost]
        public async Task<IHttpActionResult> insertBillPayTypes(BillCaliberTransactionsVM billCaliberTrans)
        {
            return Ok(await billCaliberTransService.InsertAsync(billCaliberTrans));
        }

        [Route("billCaliberTrans/update")]
        [HttpPost]
        public async Task<IHttpActionResult> updateBillPayTypes(BillCaliberTransactionsVM billCaliberTrans)
        {
            return Ok(await billCaliberTransService.UpdateAsync(billCaliberTrans));
        }

        [Route("billCaliberTrans/delete")]
        [HttpPost]
        public async Task<IHttpActionResult> deleteBillPayTypes(BillCaliberTransactionsVM billCaliberTrans)
        {
            return Ok(await billCaliberTransService.DeleteAsync(billCaliberTrans));
        }


        [Route("billCaliberTrans/getByMasterID")]
        [HttpGet]
        public async Task<IHttpActionResult> getByMasterID(int masterID)
        {
            return Ok(await billCaliberTransService.getByMasterID(masterID));
        }




        [Route("billCaliberTrans/UpdateWithMasterID")]
        [HttpPost]
        public async Task<IHttpActionResult> UpdateWithMasterID(List <BillCaliberTransactionsVM> billCaliberTransList, int masterID)
        {
            return Ok(await billCaliberTransService.UpdateWithMasterID(billCaliberTransList, masterID));
        }



        [Route("billCaliberTrans/insertWithMasterID")]
        [HttpPost]
        public async Task<IHttpActionResult> InsertWithMasterID(List<BillCaliberTransactionsVM> TransList)
        {
            return Ok(await billCaliberTransService.InsertWithMasterID(TransList));
        }

    }
}
