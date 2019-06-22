using ResortERP.Core;
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
    public class RptInvoiceMotionController : ApiController
    {
        IBillMasterService BillMasterService;
        public RptInvoiceMotionController(IBillMasterService billMasterService)
        {
            BillMasterService = billMasterService;
        }

        [Route("RptInvoiceMotion/getSearch")]
        [HttpPost]
        public List<InvoiceMotionViewVM> getSearch([FromBody]InvoiceMotionSearchModel searchObject)
        {
            return BillMasterService.GetInvoiceMotionReportSearch(searchObject);
        }
    }
}
