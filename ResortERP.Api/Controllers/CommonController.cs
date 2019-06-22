using ResortERP.Core.VM;
using ResortERP.Service;
using ResortERP.Service.IServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;

namespace ResortERP.Api.Controllers
{
    public class CommonController : ApiController
    {
        ICommonService commonService;
        public CommonController(ICommonService commonService)
        {
            this.commonService = commonService;
        }

        [Route("common/getPrinters")]
        [HttpGet]
        public List<string> getPrinters()
        {
            return commonService.GetInstalledPrinters();
        }
        [Route("common/getUserModule")]
        [HttpGet]
        public List<int> GetUserModule(string Id)
        {
            return commonService.GetUserModule(Id);
        }
        

        [Route("common/getAllCompanyBranches")]
        [HttpGet]
        public List<COMPANY_BRANCHESVM> getAllCompanyBranches()
        {
            return commonService.GetAllCompanyBranchesAsync();
        }
        [Route("common/getAllEmployees")]
        [HttpGet]
        public List<EMPLOYEEVM> getAllEmployees()
        {
            return commonService.GetAllEmployeesAsync();
        }
        [Route("common/getAllGovernate")]
        [HttpGet]
        public List<GOVERNORATEVM> getAllGovernate()
        {
            return commonService.GetAllGovernateAsync();
        }
        [Route("common/getAllNations")]
        [HttpGet]
        public List<NATIONVM> getAllNations()
        {
            return commonService.GetAllNationsAsync();
        }
        [Route("common/getAllTowns")]
        [HttpGet]
        public List<TOWNVM> getAllTowns()
        {
            return commonService.GetAllTownsAsync();
        }
        [Route("common/getAllVillages")]
        [HttpGet]
        public List<VILLAGEVM> getAllVillages()
        {
            return commonService.GetAllVillagesAsync();
        }

        [Route("common/GetGovernatesByNationID")]
        [HttpGet]
        public List<GOVERNORATEVM> GetGovernatesByNationID(int NationID)
        {
            return commonService.GetGovernatesByNationID(NationID);
        }
        [Route("common/GetTownsByGovernateID")]
        [HttpGet]
        public List<TOWNVM> GetTownsByGovernateID(int GovID)
        {
            return commonService.GetTownsByGovernateID(GovID);
        }
        [Route("common/GetVillageByTownID")]
        [HttpGet]
        public List<VILLAGEVM> GetVillageByTownID(int TownID)
        {
            return commonService.GetVillageByTownID(TownID);
        }
        [Route("common/GetDepartmentByCompanyBranchID")]
        [HttpGet]
        public List<DepartmentVM> GetDepartmentByCompanyBranchID(int COM_BRAN_ID)
        {
            return commonService.GetDepartmentByCompanyBranchID(COM_BRAN_ID);
        }

        [Route("common/GetCompanyBranchNameByID")]
        [HttpGet]
        public string GetCompanyBranchNameByID(int BranID)
        {
            return commonService.GetCompanyBranchNameByID(BranID);
        }

        [Route("common/GetEmployeeNameByID")]
        [HttpGet]
        public string GetEmployeeNameByID(int EmpID)
        {
            return commonService.GetEmployeeNameByID(EmpID);
        }

        //public HttpResponseMessage GetFile(string id)
        //{
        //    if (String.IsNullOrEmpty(id))
        //        return Request.CreateResponse(HttpStatusCode.BadRequest);

        //    string fileName;
        //    string localFilePath;
        //    int fileSize;

        //    localFilePath = getFileFromID(id, out fileName, out fileSize);

        //    HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
        //    response.Content = new StreamContent(new FileStream(localFilePath, FileMode.Open, FileAccess.Read));
        //    response.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment");
        //    response.Content.Headers.ContentDisposition.FileName = fileName;
        //    response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");

        //    return response;
        //}
    }
}