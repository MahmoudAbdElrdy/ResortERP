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
    public class OrdersController : ApiController
    {
        IOrdersService ordersService;
        public OrdersController(IOrdersService ordersService)
        {
            this.ordersService = ordersService;
        }

        [Route("Orders/add")]
        [HttpPost]
        public async Task<IHttpActionResult> add([FromBody]OrdersVM entity)
        {
            return Ok(await ordersService.InsertAsync(entity));
        }
        [Route("Orders/update")]
        [HttpPost]
        public async Task<IHttpActionResult> update([FromBody]OrdersVM entity)
        {
            return Ok(await ordersService.UpdateAsync(entity));
        }
        [Route("Orders/delete")]
        [HttpPost]
        public async Task<IHttpActionResult> delete([FromBody]OrdersVM entity)
        {
            return Ok(await ordersService.DeleteAsync(entity));
        }
        [Route("Orders/get")]
        [HttpGet]
        public async Task<IHttpActionResult> get(int pageNum, int pageSize)
        {
            return Ok(await ordersService.GetAllAsync(pageNum, pageSize));
        }
        [Route("Orders/getAll")]
        [HttpGet]
        public List<OrdersVM> getAll()
        {
            return ordersService.GetAll();
        }
        [Route("Orders/count")]
        [HttpGet]
        public async Task<IHttpActionResult> count()
        {
            return Ok(await ordersService.CountAsync());
        }

    }
}
