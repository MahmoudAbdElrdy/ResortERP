using ResortERP.Core;
using ResortERP.Service.IServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace ResortERP.Api.Controllers
{
    public class UIDViewController : ApiController
    {
        IUIDViewService uidService;
        ICommonService commonService;
        public UIDViewController(IUIDViewService uidService, ICommonService commonService)
        {
            this.uidService = uidService;
            this.commonService = commonService;
        }


        [Route("uidView/get")]
        [HttpGet]
        public async Task<IHttpActionResult> get(int pageNum, int pageSize)
        {
            return Ok(await uidService.GetAllAsync(pageNum, pageSize));
        }

        [Route("uidView/count")]
        [HttpGet]
        public async Task<IHttpActionResult> count()
        {
            return Ok(await uidService.CountAsync());
        }
        [Route("uidView/getAll")]
        [HttpGet]
        public List<UID_View> getAll()
        {
            return uidService.GetAll();
        }

        [Route("uidView/getLogo")]
        [HttpGet]
        public async Task<IHttpActionResult> getLogo()
        {
            try
            {
                return Ok(await uidService.GetByLOGO((await commonService.GetLoggedDatabase()).Intial_Catalog));
            }
            catch (Exception ex)
            {
                string str = ex.Message;
                return null;
            }
        }

        [Route("uidView/getCompany")]
        [HttpGet]
        public async Task<IHttpActionResult> getCompany()
        {
            try
            {
                return Ok( uidService.GetCompany((await commonService.GetLoggedDatabase()).Intial_Catalog));
            }
            catch (Exception ex)
            {
                string str = ex.Message;
                return null;
            }
        }
    }
}
