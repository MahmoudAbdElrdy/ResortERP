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
    public class CustomerSonController : ApiController
    {
        ICustomerSonService customerSonService;
        public CustomerSonController(ICustomerSonService customerSonService)
        {
            this.customerSonService = customerSonService;
        }

        [Route("customerSon/add")]
        [HttpPost]
        public async Task<IHttpActionResult> add([FromBody]CustomerSonVM entity)
        {
            return Ok(await customerSonService.InsertAsync(entity));
        }
        [Route("customerSon/update")]
        [HttpPost]
        public async Task<IHttpActionResult> update([FromBody]CustomerSonVM entity)
        {
            return Ok(await customerSonService.UpdateAsync(entity));
        }
        [Route("customerSon/delete")]
        [HttpPost]
        public async Task<IHttpActionResult> delete([FromBody]CustomerSonVM entity)
        {
            return Ok(await customerSonService.DeleteAsync(entity));
        }
        [Route("customerSon/get")]
        [HttpGet]
        public async Task<IHttpActionResult> get(int pageNum, int pageSize)
        {
            return Ok(await customerSonService.GetAllAsync(pageNum, pageSize));
        }
        [Route("customerSon/getAll")]
        [HttpGet]
        public List<CustomerSonVM> getAll()
        {
            return customerSonService.GetAll();
        }
        [Route("customerSon/count")]
        [HttpGet]
        public async Task<IHttpActionResult> count()
        {
            return Ok(await customerSonService.CountAsync());
        }
    }
}
