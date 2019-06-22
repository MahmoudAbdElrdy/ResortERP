using ResortERP.Core;
using ResortERP.Service.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace ResortERP.Api.Controllers
{
    public class DashBoardController : ApiController
    {
        IDashBoardService dashBoardService;
        public DashBoardController(IDashBoardService dashBoardService)
        {
            this.dashBoardService = dashBoardService;
        }

        [Route("DashBoard/save")]
        [HttpPost]
        public DashBoard save(DashBoard entity)
        {
            
            return dashBoardService.InsertDashBoardPer(entity);
        }

        [Route("DashBoard/get")]
        [HttpGet]
        public DashBoard get(int id)
        {
            return dashBoardService.get(id);
        }
    }
}