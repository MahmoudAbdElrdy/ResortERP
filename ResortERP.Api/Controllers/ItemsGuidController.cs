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
    public class ItemsGuidController : ApiController
    {
        IItemsGroupsService itemsGroupsService;
        IItemsService itemsService;
        public ItemsGuidController(IItemsGroupsService itemsGroupsService, IItemsService itemsService)
        {
            this.itemsGroupsService = itemsGroupsService;
            this.itemsService = itemsService;
        }


        [Route("ItemsGuid/get")]
        [HttpGet]
        public IHttpActionResult get()
        {
            return Ok(itemsGroupsService.getForTree());
        }

        //[Route("ItemsGuid/get")]
        //[HttpGet]
        //public IHttpActionResult buildTree()
        //{
        //    return Ok(itemsGroupsService.buildItemsTree());
        //}

    }
}
