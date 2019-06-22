using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using ResortERP.Core.VM;
using ResortERP.Repository;
using ResortERP.Service;
using ResortERP.Service.IServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web.Http;

namespace ResortERP.Api.Controllers
{
    [Logger]
    public class CustomerSupplierController : ApiController
    {
        IGetCustomerSupplierService customerSupplierService;
        IUserLogFileService userLogFileService;
        public CustomerSupplierController(IGetCustomerSupplierService customerSupplierService, IUserLogFileService userLogFileService)
        {
            this.customerSupplierService = customerSupplierService;
            this.userLogFileService = userLogFileService;
        }


        [Route("CustomerSupplier/get")]
        [HttpGet]
        public async Task<IHttpActionResult> get(int pageNum, int pageSize, char type)
        {
            return Ok(await customerSupplierService.GetAllAsync(pageNum, pageSize, type));
        }

        [Route("CustomerSupplier/count")]
        [HttpGet]
        public async Task<IHttpActionResult> count()
        {
            return Ok(await customerSupplierService.CountAsync());
        }

        [Route("CustomerSupplier/getAll")]
        [HttpGet]
        public List<CustomersVM> getAll(char type)
        {
            return customerSupplierService.GetAll(type);
        }

        [Route("CustomerSupplier/getByIdLog")]
        [HttpGet]
        public IHttpActionResult getByIdLog(int custId, char type)
        {
            return Ok(customerSupplierService.getByIdLog(custId, type));
        }
        public async Task<bool> LogData(string code = null,string id=null)
        {
            Logger logger = new Logger();
            await userLogFileService.Insert(logger.LogDataMethod(code,id));
            return true;
        }

    }
}
