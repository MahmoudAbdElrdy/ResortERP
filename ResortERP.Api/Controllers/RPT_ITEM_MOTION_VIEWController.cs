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
    public class RPT_ITEM_MOTION_VIEWController : ApiController
    {
        IRPT_ITEM_MOTION_VIEWService rpt_ITEM_MOTION_VIEWService;
        public RPT_ITEM_MOTION_VIEWController(IRPT_ITEM_MOTION_VIEWService rpt_ITEM_MOTION_VIEWService)
        {
            this.rpt_ITEM_MOTION_VIEWService = rpt_ITEM_MOTION_VIEWService;
        }

        [Route("RPT_ITEM_MOTION_VIEW/get")]
        [HttpGet]
        public async Task<IHttpActionResult> get(int pageNum, int pageSize)
        {
            return Ok(await rpt_ITEM_MOTION_VIEWService.GetAllAsync(pageNum, pageSize));
        }

        [Route("RPT_ITEM_MOTION_VIEW/getAll")]
        [HttpGet]
        public List<RPT_ITEM_MOTION_VIEW_VM> getAll()
        {
            return rpt_ITEM_MOTION_VIEWService.GetAll();
        }


        [Route("RPT_ITEM_MOTION_VIEW/getSearch")]
        [HttpPost]
        public List<RPT_ITEM_MOTION_VIEW_VM> getSearch(SearchItem obj)
        {
            return rpt_ITEM_MOTION_VIEWService.GetingSearchItems(obj.searchItm, obj.bilSettings, obj.rptOptions);
        }
    }
}
