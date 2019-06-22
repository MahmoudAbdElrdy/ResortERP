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
    public class SmartSysDatabaseViewController : ApiController
    {
        ISmartSysDatabaseViewService ssdService;
        ICommonService commonService;
        public SmartSysDatabaseViewController(ISmartSysDatabaseViewService ssdService, ICommonService commonService)
        {
            this.ssdService = ssdService;
            this.commonService = commonService;
        }


        [Route("ssdView/get")]
        [HttpGet]
        public async Task<IHttpActionResult> get(int pageNum, int pageSize)
        {
            return Ok(await ssdService.GetAllAsync(pageNum, pageSize));
        }

        [Route("ssdView/count")]
        [HttpGet]
        public async Task<IHttpActionResult> count()
        {
            return Ok(await ssdService.CountAsync());
        }
        [Route("ssdView/getAll")]
        [HttpGet]
        public List<SmartSysDataBases_View> getAll()
        {
            return ssdService.GetAll();
        }
        //[Route("ssdView/getLogo")]
        //[HttpGet]
        //public byte[] getLogo()
        //{
        //    return ssdService.GetBy((await commonService.GetLoggedDatabase()).Intial_Catalog).FirstOrDefault().COMPANY_LOGO;
        //}
    }
}
