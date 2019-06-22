using ResortERP.Core;
using ResortERP.Core.VM;
using ResortERP.Repository;
using ResortERP.Service.IServices;
using ResortERP.Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace ResortERP.Api.Controllers
{
    [Logger]
    public class LogFileReportController : ApiController
    {
        #region var
        IUserLogFileService userLogFileService;
        IUserFormService userFormService;
        #endregion

        #region Const
        public LogFileReportController(IUserFormService _userFormService, IUserLogFileService _userLogFileService)
        {
            userFormService = _userFormService;
            userLogFileService = _userLogFileService;
        }
        #endregion

        #region Api Methods

        [Route("LogFileReport/getAll")]
        [HttpGet]
        public List<menuFormSelVM> GetAll()
        {
            return userFormService.GetAll();
        }

        [Route("LogFileReport/getAllUsers")]
        [HttpGet]
        public List<User> GetAllUsers()
        {
            return userFormService.GetAllUsers();
        }

        [Route("LogFileReport/getAllBranshes")]
        [HttpGet]
        public List<COMPANY_BRANCHES> GetAllBranshes()
        {
            return userFormService.GetAllBranshes();
        }

        [Route("LogFileReportServices/count")]
        [HttpPost]
        public async Task<IHttpActionResult> count(string[] Forms, string StartDate, string EndDate, string UID, string branchId)
        {
            return Ok(await userFormService.CountAsync(Forms, StartDate, EndDate, UID, branchId));
        }

        [Route("LogFileReport/GetBySelForms")]
        [HttpGet]
        public List<UserLogFile> GetBySelForms(string ARName)
        {
            return userFormService.GetBySelForms(ARName);
        }

        [Route("LogFileReport/GetByRangDate")]
        [HttpPost]
        public async Task<List<UserLogFile>> GetByRangDate(string [] Forms, string StartDate, string EndDate, string UID,string branchId, int pageNum, int pageSize)
        {
            return await userFormService.getByRangDate(Forms, StartDate, EndDate,UID, branchId, pageNum, pageSize);
        }


        public async Task<bool> LogData(string code = null)
        {
            Logger logger = new Logger();
            await userLogFileService.Insert(logger.LogDataMethod(code));
            return true;
        }

        #endregion
    }
}
