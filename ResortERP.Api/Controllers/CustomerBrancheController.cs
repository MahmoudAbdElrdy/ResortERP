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
    public class CustomerBrancheController : ApiController
    {
        ICustomerBranchesService customerBrancheService;
        public CustomerBrancheController(ICustomerBranchesService customerBrancheService)
        {
            this.customerBrancheService = customerBrancheService;
        }

        [Route("customerBranche/add")]
        [HttpPost]
        public async Task<IHttpActionResult> add([FromBody]CustomerBranchesVM entity)
        {
            return Ok(await customerBrancheService.InsertAsync(entity));
        }
        [Route("customerBranche/update")]
        [HttpPost]
        public async Task<IHttpActionResult> update([FromBody]CustomerBranchesVM entity)
        {
            return Ok(await customerBrancheService.UpdateAsync(entity));
        }
        [Route("customerBranche/delete")]
        [HttpPost]
        public async Task<IHttpActionResult> delete([FromBody]CustomerBranchesVM entity)
        {
            return Ok(await customerBrancheService.DeleteAsync(entity));
        }
        [Route("customerBranche/get")]
        [HttpGet]
        public async Task<IHttpActionResult> get(int pageNum, int pageSize)
        {
            return Ok(await customerBrancheService.GetAllAsync(pageNum, pageSize));
        }
        [Route("customerBranche/getAll")]
        [HttpGet]
        public List<CustomerBranchesVM> getAll()
        {
            return customerBrancheService.GetAll();
        }
        [Route("customerBranche/getAllByAccID")]
        [HttpGet]
        public List<CustomerBranchesVM> GetAllByAccID(int AccID)
        {
            return customerBrancheService.GetAllByAccID(AccID);
        }
        [Route("customerBranche/count")]
        [HttpGet]
        public async Task<IHttpActionResult> count()
        {
            return Ok(await customerBrancheService.CountAsync());
        }
    }
}
