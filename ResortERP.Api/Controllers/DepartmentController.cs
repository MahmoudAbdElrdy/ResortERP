using ResortERP.Core.VM;
using ResortERP.Repository;
using ResortERP.Service.IServices;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace ResortERP.Api.Controllers
{
    [Logger]
    public class DepartmentController : ApiController
    {
        IDepartmentService departmentService;
        IEmployeeService employeeService;
        ICustomersService customersService;
        IUserLogFileService userLogFileService;
        public DepartmentController(IDepartmentService departmentService, IEmployeeService employeeService, ICustomersService customersService, IUserLogFileService userLogFileService)
        {
            this.departmentService = departmentService;
            this.employeeService = employeeService;
            this.customersService = customersService;
            this.userLogFileService = userLogFileService;
        }

        [Route("department/insert")]
        [HttpPost]
        public async Task<IHttpActionResult> add([FromBody]DepartmentVM entity)
        {
            var result = await departmentService.InsertAsync(entity);
            await LogData(entity.DEPT_CODE,result.ToString());
            if (result != 0)
            {
                return Ok(true);
            }
            return Ok(false);

        }
        [Route("department/update")]
        [HttpPost]
        public async Task<IHttpActionResult> update([FromBody]DepartmentVM entity)
        {
            var result = await departmentService.UpdateAsync(entity);
            await LogData(entity.DEPT_CODE,entity.DEPT_ID.ToString());
            return Ok(result);

        }
        [Route("department/delete")]
        [HttpPost]
        public async Task<IHttpActionResult> delete([FromBody]DepartmentVM entity)
        {
            var q = employeeService.getByDeptID(entity.DEPT_ID);
            var q1 = customersService.getByDept(entity.DEPT_ID);
            if (q.Count == 0 && q1.Count == 0)
            {
                var result = await departmentService.DeleteAsync(entity);
                await LogData(entity.DEPT_CODE, entity.DEPT_ID.ToString());
                return Ok(result);
            }

            else
                return Ok(false);
            //return Ok(await departmentService.DeleteAsync(entity));
        }
        [Route("department/get")]
        [HttpGet]
        public async Task<IHttpActionResult> get(int pageNum, int pageSize)
        {
            return Ok(await departmentService.GetAllAsync(pageNum, pageSize));
        }

        [Route("department/count")]
        [HttpGet]
        public async Task<IHttpActionResult> count()
        {
            return Ok(await departmentService.CountAsync());
        }
        [Route("department/getAll")]
        [HttpGet]
        public List<DepartmentVM> getAll()
        {
            return departmentService.GetAll();
        }

        [Route("department/getById")]
        [HttpGet]
        public DepartmentVM getById(int DEPT_ID)
        {
            return departmentService.getById(DEPT_ID);
        }

        [Route("department/getLastCode")]
        [HttpGet]
        public string GetLastCode()
        {
            return departmentService.GetLastCode();
        }

        public async Task<bool> LogData(string code = null,string id=null)
        {
            Logger logger = new Logger();
            await userLogFileService.Insert(logger.LogDataMethod(code,id));
            return true;
        }

    }
}
