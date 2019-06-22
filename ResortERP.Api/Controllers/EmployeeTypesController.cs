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
    public class EmployeeTypesController : ApiController
    {
        IEmployeeTypesService employeeTypesService;
        IEmployeeService employeeService;
        IUserLogFileService userLogFileService;
        public EmployeeTypesController(IEmployeeTypesService employeeTypesService, IEmployeeService employeeService, IUserLogFileService userLogFileService)
        {
            this.employeeTypesService = employeeTypesService;
            this.employeeService = employeeService;
            this.userLogFileService = userLogFileService;
        }

        [Route("employeeTypes/insert")]
        [HttpPost]
        public async Task<IHttpActionResult> add([FromBody]EmployeeTypesVM entity)
        {
            var result = await employeeTypesService.InsertAsync(entity);
            await LogData(entity.EMP_TYPE_CODE, result.ToString());
            if (result != 0)
            {
                return Ok(true);
            }
            return Ok(false);

        }
        [Route("employeeTypes/update")]
        [HttpPost]
        public async Task<IHttpActionResult> update([FromBody]EmployeeTypesVM entity)
        {
            var result = await employeeTypesService.UpdateAsync(entity);
            await LogData(entity.EMP_TYPE_CODE,entity.EMP_TYPE_ID.ToString());
            return Ok(result);

        }
        [Route("employeeTypes/delete")]
        [HttpPost]
        public async Task<IHttpActionResult> delete([FromBody]EmployeeTypesVM entity)
        {
            var q = employeeService.getByEmpType(entity.EMP_TYPE_ID);
            if (q.Count == 0)
            {
                var result = await employeeTypesService.DeleteAsync(entity);
                await LogData(entity.EMP_TYPE_CODE, entity.EMP_TYPE_ID.ToString());
                return Ok(result);
            }
            else
                return Ok(false);
            //return Ok(await employeeTypesService.DeleteAsync(entity));
        }
        [Route("employeeTypes/get")]
        [HttpGet]
        public async Task<IHttpActionResult> get(int pageNum, int pageSize)
        {
            return Ok(await employeeTypesService.GetAllAsync(pageNum, pageSize));
        }

        [Route("employeeTypes/count")]
        [HttpGet]
        public async Task<IHttpActionResult> count()
        {
            return Ok(await employeeTypesService.CountAsync());
        }
        [Route("employeeTypes/getAll")]
        [HttpGet]
        public List<EmployeeTypesVM> getAll()
        {
            return employeeTypesService.GetAll();
        }
        [Route("employeeTypes/getById")]
        [HttpGet]
        public EmployeeTypesVM getById(int EMP_TYPE_ID)
        {
            return employeeTypesService.getById(EMP_TYPE_ID);
        }

        [Route("employeeTypes/getLastCode")]
        [HttpGet]
        public string GetLastCode()
        {
            return employeeTypesService.GetLastCode();
        }

        public async Task<bool> LogData(string code = null,string id=null)
        {
            Logger logger = new Logger();
            await userLogFileService.Insert(logger.LogDataMethod(code,id));
            return true;
        }

    }
}
