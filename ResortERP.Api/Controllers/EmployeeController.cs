using ResortERP.Core;
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
    public class EmployeeController : ApiController
    {
        IEmployeeService employeeService;
        IBillMasterService billMasterService;
        IEntryMasterService entryMasterService;
        IUserLogFileService userLogFileService;
        public EmployeeController(IEmployeeService employeeService, IBillMasterService billMasterService, IEntryMasterService entryMasterService, IUserLogFileService userLogFileService)
        {
            this.employeeService = employeeService;
            this.billMasterService = billMasterService;
            this.entryMasterService = entryMasterService;
            this.userLogFileService = userLogFileService;
        }

        [Route("employee/insert")]
        [HttpPost]
        public async Task<IHttpActionResult> add([FromBody]HrPslEmployeeVM entity)
        {
            var result = await employeeService.InsertAsync(entity);
            await LogData(entity.EmployeeCode,result.ToString());
            if (result != 0)
            {
                return Ok(true);
            }
            return Ok(false);

        }

        [Route("employee/insertGetID")]
        [HttpPost]
        public async Task<int> insertGetID([FromBody]EMPLOYEEVM entity)
        {
            var result = employeeService.InsertGetID(entity);
            await LogData(entity.EMP_CODE,result.ToString());
            return result;

        }

        [Route("employee/update")]
        [HttpPost]
        public async Task<IHttpActionResult> update([FromBody]HrPslEmployeeVM entity)
        {
            var result = await employeeService.UpdateAsync(entity);
            await LogData(entity.EmployeeCode,entity.HrPslEmployeeID.ToString());
            return Ok(result);

        }
        [Route("employee/delete")]
        [HttpPost]
        public async Task<IHttpActionResult> delete([FromBody]HrPslEmployeeVM entity)
        {
            var q1 = billMasterService.getByEmp(entity.HrPslEmployeeID);
            var q = entryMasterService.getByEmp(entity.HrPslEmployeeID);
            if (q.Count == 0 && q1.Count == 0)
            {
                var result = await employeeService.DeleteAsync(entity);
                await LogData(entity.EmployeeCode, entity.HrPslEmployeeID.ToString());
                return Ok(result);
            }

            else
                return Ok(false);
            // return Ok(await employeeService.DeleteAsync(entity));
        }
        [Route("employee/get")]
        [HttpGet]
        public async Task<IHttpActionResult> get(int pageNum, int pageSize)
        {
            return Ok(await employeeService.GetAllAsync(pageNum, pageSize));
        }
        [Route("employee/getAll")]
        [HttpGet]
        public List<EMPLOYEEVM> getAll()
        {
            return employeeService.GetAll();
        }

        [Route("employee/getById")]
        [HttpGet]
        public EMPLOYEEVM getById(int EMP_ID)
        {
            return employeeService.getById(EMP_ID);
        }

        [Route("employee/getByTypeID")]
        [HttpGet]
        public List<EMPLOYEEVM> getAllEmployeesByTypeID(int typeID)
        {
            return employeeService.GetByTypeID(typeID);
        }
        [Route("employee/count")]
        [HttpGet]
        public async Task<IHttpActionResult> count()
        {
            return Ok(await employeeService.CountAsync());
        }

        [Route("employee/GetAllSalesEmployees")]
        [HttpGet]
        public List<EMPLOYEEVM> GetAllSalesEmployees()
        {
            return employeeService.GetAllSalesEmployees();
        }


        [Route("employee/getLastCode")]
        [HttpGet]
        public string GetLastCode()
        {
            return employeeService.GetLastCode();
        }

        [Route("employee/getMartialStatusList")]
        [HttpGet]
        public List<DropDownMenuOptionsVM> getMartialStatusList()
        {
            return employeeService.getMartialStatusList();
        }

        [Route("employee/getRelegionList")]
        [HttpGet]
        public List<DropDownMenuOptionsVM> getRelegionList()
        {
            return employeeService.getRelegionList();
        }

        [Route("employee/getCityList")]
        [HttpGet]
        public List<DropDownMenuOptionsVM> getCityList()
        {
            return employeeService.getCityList();
        }

        [Route("employee/getNationalityList")]
        [HttpGet]
        public List<DropDownMenuOptionsVM> getNationalityList()
        {
            return employeeService.getNationalityList();
        }

        [Route("employee/getJobTitleList")]
        [HttpGet]
        public List<DropDownMenuOptionsVM> getJobTitleList()
        {
            return employeeService.getJobTitleList();
        }

        [Route("employee/getJobLevelList")]
        [HttpGet]
        public List<DropDownMenuOptionsVM> getJobLevelList()
        {
            return employeeService.getJobLevelList();
        }

        [Route("employee/getDeptartmentList")]
        [HttpGet]
        public List<DropDownMenuOptionsVM> getDeptartmentList()
        {
            return employeeService.getDeptartmentList();
        }

        [Route("employee/getEmployeeStatusList")]
        [HttpGet]
        public List<DropDownMenuOptionsVM> getEmployeeStatusList()
        {
            return employeeService.getEmployeeStatusList();
        }

        [Route("employee/getDirectManagerList")]
        [HttpGet]
        public List<DropDownMenuOptionsVM> getDirectManagerList()
        {
            return employeeService.getDirectManagerList();
        }

        [Route("employee/getContactTypeList")]
        [HttpGet]
        public List<DropDownMenuOptionsVM> getContactTypeList()
        {
            return employeeService.getContactTypeList();
        }

        [Route("employee/getAcademicDegreeList")]
        [HttpGet]
        public List<DropDownMenuOptionsVM> getAcademicDegreeList()
        {
            return employeeService.getAcademicDegreeList();
        }
        
        [Route("employee/getBankList")]
        [HttpGet]
        public List<DropDownMenuOptionsVM> getBankList()
        {
            return employeeService.getBankList();
        }

        [Route("employee/getEmployeeAcademicDegrees")]
        [HttpGet]
        public List<HrPslEmployeeAcademicDegree> getEmployeeAcademicDegrees(int employeeId)
        {
            return employeeService.getEmployeeAcademicDegrees(employeeId);
        }
        [Route("employee/getEmployeeExperiences")]
        [HttpGet]
        public List<HrPslEmployeeExperience> getEmployeeExperiences(int employeeId)
        {
            return employeeService.getEmployeeExperiences(employeeId);
        }
        [Route("employee/getEmployeeFamilyDetails")]
        [HttpGet]
        public List<HrPslEmployeeFamilyDetails> getEmployeeFamilyDetails(int employeeId)
        {
            return employeeService.getEmployeeFamilyDetails(employeeId);
        }
        [Route("employee/getEmployeeTrainingCourses")]
        [HttpGet]
        public List<HrPslEmployeeTrainingCources> getEmployeeTrainingCourses(int employeeId)
        {
            return employeeService.getEmployeeTrainingCourses(employeeId);
        }
        [Route("employee/getEmployeeContacts")]
        [HttpGet]
        public List<HrPslEmployeeContacts> getEmployeeContacts(int employeeId)
        {
            return employeeService.getEmployeeContacts(employeeId);
        }

        public async Task<bool> LogData(string code = null,string id=null)
        {
            Logger logger = new Logger();
            await userLogFileService.Insert(logger.LogDataMethod(code,id));
            return true;
        }
    }
}