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

namespace ResortERP.Api.Controllers
{
    [Logger]
    public class CompanyStoresController : ApiController
    {
        ICompanyStoresService companyStoresService;
        IBillMasterService billMasterService;
       IUserLogFileService userLogFileService;
        public CompanyStoresController(ICompanyStoresService companyStoresService, IBillMasterService billMasterService, IUserLogFileService userLogFileService)
        {
            this.companyStoresService = companyStoresService;
            this.billMasterService = billMasterService;
            this.userLogFileService= userLogFileService;
        }

        [Route("companyStores/insert")]
        [HttpPost]
        public async Task<IHttpActionResult> add([FromBody]CompanyStoresVM entity)
        {
            var result = await companyStoresService.InsertAsync(entity);
            await LogData(entity.COM_STORE_CODE,result.ToString());
            if (result != 0)
            {
                return Ok(true);
            }
            return Ok(false);
        }
        [Route("companyStores/update")]
        [HttpPost]
        public async Task<IHttpActionResult> update([FromBody]CompanyStoresVM entity)
        {
            var result = await companyStoresService.UpdateAsync(entity);
            await LogData(entity.COM_STORE_CODE,entity.COM_STORE_ID.ToString());
            return Ok(result);
        }
        [Route("companyStores/delete")]
        [HttpPost]
        public async Task<IHttpActionResult> delete([FromBody]CompanyStoresVM entity)
        {
            var q = billMasterService.getBillStores(entity.COM_STORE_ID);
            if (q.Count == 0)
            {
                var result = await companyStoresService.DeleteAsync(entity);
                await LogData(entity.COM_STORE_CODE, entity.COM_STORE_ID.ToString());
                return Ok(result);
            }
               
            else
                return Ok(false);
            
            //return Ok(await companyStoresService.DeleteAsync(entity));
        }
        [Route("companyStores/get")]
        [HttpGet]
        public async Task<IHttpActionResult> get(int pageNum, int pageSize)
        {
            return Ok(await companyStoresService.GetAllAsync(pageNum, pageSize));
        }

        [Route("companyStores/searchForStores")]
        [HttpGet]
        public async Task<IHttpActionResult> getSearch(string search)
        {
            return Ok(await companyStoresService.GetSearch(search));
        }

        [Route("companyStores/count")]
        [HttpGet]
        public async Task<IHttpActionResult> count()
        {
            return Ok(await companyStoresService.CountAsync());
        }
        [Route("companyStores/getAll")]
        [HttpGet]
        public List<CompanyStoresVM> getAll()
        {
            return companyStoresService.GetAll();
        }

        [Route("companyStores/getById")]
        [HttpGet]
        public CompanyStoresVM getById(int COM_STORE_ID)
        {
            return companyStoresService.GetById(COM_STORE_ID);
        }


        [Route("companyStores/getLastCode")]
        [HttpGet]
        public string getLastCode() {
            return companyStoresService.getLastCode();
        }

        public async Task<bool> LogData(string code = null,string id=null,string notes=null)
        {
            Logger logger = new Logger();
            await userLogFileService.Insert(logger.LogDataMethod(code,id,notes));
            return true;
        }

    }
}
