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
    public class StoresGuidController : ApiController
    {
        ICompanyStoresService storesService;
        public StoresGuidController(ICompanyStoresService storesService)
        {
            this.storesService = storesService;
        }


        [Route("StoresGuid/get")]
        [HttpGet]
        public IHttpActionResult get()
        {
            return Ok(storesService.getForTree());
        }

    }
}
