using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Threading.Tasks;
using ResortERP.Service.IServices;
using ResortERP.Core.VM;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using ResortERP.Service;
using System.IO;
using System.Net.Mail;
using ResortERP.Repository;
using System.Web.Http.Controllers;

namespace ResortERP.Api.Controllers
{
    [Logger]
    public class CompanyBranchesController : ApiController
    {
        ICompanyBranchesService companyBranchesService;
        IUserPriviligeBranchesService userPriviligeBranService;
        IBillMasterService billMasterService;
        IDepartmentService departmentService;
        ICompanyStoresService companyStoresService;
        ICustomersService customersService;
        IUserLogFileService userLogFileService;
        public CompanyBranchesController(ICompanyBranchesService _companyBranchesService,IUserPriviligeBranchesService _userPriviligeBranService, IBillMasterService billMasterService, IDepartmentService departmentService, ICompanyStoresService companyStoresService, ICustomersService customersService, IUserLogFileService userLogFileService)
        {
            this.companyBranchesService = _companyBranchesService;
            this.userPriviligeBranService = _userPriviligeBranService;
            this.billMasterService = billMasterService;
            this.departmentService = departmentService;
            this.companyStoresService = companyStoresService;
            this.customersService = customersService;
            this.userLogFileService = userLogFileService;
        }


        [Route("companyBranches/get")]
        [HttpGet]
        public async Task<IHttpActionResult> get(int pageNum, int pageSize)
        {
            return Ok(await companyBranchesService.GetAllAsync(pageNum, pageSize));
        }
        [Route("companyBranches/getAll")]
        [HttpGet]
        public async Task<IHttpActionResult> getAll()
        {
            return Ok(await companyBranchesService.GetAll());
        }

        [Route("companyBranches/getByIdLog")]
        [HttpGet]
        public async Task<IHttpActionResult> getByIdLog(int COM_BRN_ID)
        {
            return Ok(await companyBranchesService.getByIdLog(COM_BRN_ID));
        }


        [Route("companyBranches/getUserBranches")]
        [HttpGet]
        public async Task<IHttpActionResult> getAllReport()
        {
            var ss = Request.Headers.Where(a => a.Key == "userNameLog").SingleOrDefault();
            string y=null;
            if (ss.Value != null)
            {
                string[] s = Request.Headers.Where(a => a.Key == "userNameLog").SingleOrDefault().Value.ToArray();
                y = s[0];
                if (y.Split('@').Count() > 0)
                {
                    y = y.Split('@')[0];
                }
            };
            return Ok(await companyBranchesService.GetAllReport(y));
        }

        [Route("companyBranches/add")]
        [HttpPost]
        public async Task<IHttpActionResult> add([FromBody] COMPANY_BRANCHESVM entity)
        {
            var result = await companyBranchesService.InsertAsync(entity);
            await LogData(entity.COM_BRN_CODE, result.ToString());
            if (result != 0)
            {
                return Ok(true);
            }
            return Ok(false);

        }


        [Route("companyBranches/update")]
        [HttpPost]
        public async Task<IHttpActionResult> update([FromBody] COMPANY_BRANCHESVM entity)
        {
            var result = await companyBranchesService.UpdateAsync(entity);
            await LogData(entity.COM_BRN_CODE,entity.COM_BRN_ID.ToString());
            return Ok(result);

        }


        [Route("companyBranches/delete")]
        [HttpPost]
        public async Task<IHttpActionResult> delete([FromBody] COMPANY_BRANCHESVM entity)
        {
           // return Ok(await companyBranchesService.DeleteAsync(entity));
            var q = userPriviligeBranService.GetByIdBransh(entity.COM_BRN_ID);
            var q1 = billMasterService.getByCompBranId(entity.COM_BRN_ID);
            var q2=departmentService.getDeptByBran(entity.COM_BRN_ID);
            var q3 = companyStoresService.getCompBranByStoresID(entity.COM_BRN_ID);
            var q4 = customersService.getByCompBranID(entity.COM_BRN_ID);
            if (q.Count == 0 && q1.Count == 0 && q2.Count == 0 && q3.Count == 0 && q4.Count == 0)
            {
                var result = await companyBranchesService.DeleteAsync(entity);
                await LogData(entity.COM_BRN_CODE,entity.COM_BRN_ID.ToString());
                return Ok(result);
            }

            else
                return Ok(false);
        }

        //[Route("companyBranches/deleteBranUsers")]
        //[HttpPost]
        //public async Task<IHttpActionResult> deleteBran([FromBody] COMPANY_BRANCHESVM entity)
        //{
        //    var q = userPriviligeBranService.GetByIdBransh(entity.COM_BRN_ID);
        //    var q1 = billMasterService.getByCompBranId(entity.COM_BRN_ID);
        //    if (q1.Count != 0|| q.Count != 0)
        //        return Ok(false);


        //    //foreach (var item in q)
        //    //{
        //    //    await userPriviligeBranService.DeleteAllByBranch(item.COM_BRN_ID);
        //    //}

        //    return Ok(await companyBranchesService.DeleteAsync(entity));
        //}

        [Route("companyBranches/getMainCompanyBranches")]
        [HttpGet]
        public async Task<IHttpActionResult> getMainCompanyBranches()
        {
            return Ok(await companyBranchesService.getMainComapnyBranches());
        }


        [Route("companyBranches/count")]
        [HttpGet]
        public async Task<IHttpActionResult> count()
        {
            return Ok(await companyBranchesService.countAsync());
        }



        [Route("companyBranches/getLastCode")]
        [HttpGet]
        public string GetLastCode()
        {
            return companyBranchesService.GetLastCode();
        }

        public async Task<bool> LogData(string code = null,string id=null,string notes=null)
        {
            Logger logger = new Logger();
            await userLogFileService.Insert(logger.LogDataMethod(code,id,notes));
            return true;
        }

    }
}
