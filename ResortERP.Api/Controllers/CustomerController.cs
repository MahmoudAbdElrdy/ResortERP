using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ResortERP.Core.VM;
using ResortERP.Repository;
using ResortERP.Service;
using ResortERP.Service.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Script.Serialization;

namespace ResortERP.Api.Controllers
{
    [Logger]
    public class CustomerController : ApiController
    {
        ICustomersService customerService;
        IBillMasterService billMasterService;
        IEntryMasterService entryMasterService;
        IUserLogFileService userLogFileService;
        public CustomerController(ICustomersService customerService, IBillMasterService billMasterService, IEntryMasterService entryMasterService, IUserLogFileService userLogFileService)
        {
            this.customerService = customerService;
            this.entryMasterService = entryMasterService;
            this.billMasterService = billMasterService;
            this.userLogFileService = userLogFileService;
        }

        [Route("customer/add")]
        [HttpPost]
        public async Task<IHttpActionResult> add([FromBody]CustomersVM entity)
        {
            var result = await customerService.InsertAsync(entity);
            await LogData("&" + entity.ACC_ID + "&" + entity.CUST_CODE,result.ToString());
            if (result != 0)
            {
                return Ok(true);
            }
            return Ok(false);

        }

        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        public static byte[] ConvertBytes(string plainText)
        {
            return System.Text.Encoding.UTF8.GetBytes(plainText);

        }

        [Route("customer/InsertUpdateCustomerDependence")]
        [System.Web.Http.HttpPost]
        public async Task<int?> InsertUpdateCustomerDependence(Object obj)
        {
            var jsonString = obj.ToString();

            var jsonObject = (JObject)JsonConvert.DeserializeObject(jsonString);
            if (jsonObject["obj"] == null) { }

            CustomerDependences result = JsonConvert.DeserializeObject<CustomerDependences>(jsonObject["obj"].ToString());

            CustomersVM customer = result.customer;

            string base64 = customer.Cust_Photo_Base64;
            if (base64 != null)
            {
                customer.Cust_Photo_Base64 = String.Format(base64);

                base64 = base64.Remove(0, base64.IndexOf("base64,") + 7);
                customer.CUST_PHOTO = Convert.FromBase64String(base64);
            }

            List<TelephoneVM> telephones = result.telephones;
            List<CustomerBranchesVM> customerBran = result.customerBran;
            char transaction = result.transaction;
            var FinalResult= customerService.InsertUpdateCustomerDependence(customer, telephones, customerBran, transaction);
            await LogData("&" + FinalResult+"&"+customer.CUST_CODE, FinalResult.ToString());
            return FinalResult;
        }

        [Route("customer/update")]
        [HttpPost]
        public async Task<IHttpActionResult> update([FromBody]CustomersVM entity)
        {
            var result = await customerService.UpdateAsync(entity);
            await LogData("&" + entity.ACC_ID + "&" + entity.CUST_CODE,entity.ACC_ID.ToString());
            return Ok(result);

        }
        [Route("customer/delete")]
        [HttpPost]
        public async Task<IHttpActionResult> delete([FromBody]CustomersVM entity)
        {
            var q1 = entryMasterService.getByCust(entity.ACC_ID);
            var q = billMasterService.getByCust(entity.ACC_ID);
            if (q.Count == 0 && q1.Count == 0)
            {
                var result = await customerService.DeleteAsync(entity);
                await LogData("&" + entity.ACC_ID + "&" + entity.CUST_CODE,entity.ACC_ID.ToString());
                return Ok(result);
            }

            else
                return Ok(false);
            //return Ok(await customerService.DeleteAsync(entity));
        }
        [Route("customer/get")]
        [HttpGet]
        public async Task<IHttpActionResult> get(int pageNum, int pageSize, char type)
        {
            return Ok(await customerService.GetAllAsync(pageNum, pageSize, type));
        }


        [Route("customer/getById")]
        [HttpGet]
        public IHttpActionResult getById(int id)
        {
            return Ok(customerService.GetById(id));
        }

        [Route("customer/getAll")]
        [HttpGet]
        public List<CustomersVM> getAll()
        {
            return customerService.GetAll();
        }
        [Route("customer/count")]
        [HttpGet]
        public async Task<IHttpActionResult> count(char type)
        {
            return Ok(await customerService.CountAsync(type));
        }

        [Route("customer/getLastCode")]
        [HttpGet]
        public string GetLastCode()
        {
            return customerService.GetLastCode();
        }
        public async Task<bool> LogData(string code = null,string id=null)
        {
            Logger logger = new Logger();
            await userLogFileService.Insert(logger.LogDataMethod(code,id));
            return true;
        }

        [Route("customer/getSearchForCustomer")]
        [HttpGet]
        public async Task<IHttpActionResult> getSearchForCustomer(string Search)
        {
            return Ok(await customerService.getSearchForCustomer(Search));
        }

    }

    public class CustomerDependences
    {
        public CustomersVM customer { get; set; }
        public List<TelephoneVM> telephones { get; set; }
        public List<CustomerBranchesVM> customerBran { get; set; }
        public char transaction { get; set; }
    }
}
