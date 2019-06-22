using ResortERP.Core;
using ResortERP.Core.VM;
using ResortERP.Repository;
using ResortERP.Service.IServices;

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
    public class UserPriviligeBranchesController : ApiController
    {
        #region var
        IUserPriviligeBranchesService uPrivBranServices;
        IUserLogFileService userLogFileService;
        #endregion

        #region Const
        public UserPriviligeBranchesController(IUserPriviligeBranchesService _uPrivBranServices, IUserLogFileService _userLogFileService)
        {
            uPrivBranServices = _uPrivBranServices;
            userLogFileService = _userLogFileService;
        }
        #endregion

        #region Api Methods
        //[Route("UserPriviligeBranchesServices/insert")]
        //[HttpPost]
        //public async Task<IHttpActionResult> add([FromBody]UserPriviligeBranchesVM entity)
        //{
        //   await uPrivBranServices.DeleteAll(entity.ID);
        //    foreach (var item in entity.COM_BRN_ID)
        //    {
        //        var entityAdd = new UserPriviligeBranches
        //        {
        //            ID = entity.ID,
        //            COM_BRN_ID = item
        //        };
        //        await uPrivBranServices.InsertAsync(entityAdd);
        //    };

        //    return Ok();
        //}

        [Route("UserPriviligeBranchesServices/insert")]
        [HttpPost]
        public async Task<IHttpActionResult> add([FromBody]UserPriviligeBranchesVM entity)
        {
            await uPrivBranServices.DeleteAll(entity.ID);
            foreach (var item in entity.COM_BRN_ID)
            {
                var entityAdd = new UserPriviligeBranches
                {
                    ID = entity.ID,
                    COM_BRN_ID = item
                };

                //string controllerName = ControllerContext.RouteData.Values["controller"].ToString();
                await uPrivBranServices.InsertAsync(entityAdd);
                //await LogData(entity.ID.ToString());

            };

            return Ok();
        }

        [Route("UserPriviligeBranchesServices/insertGetID")]
        [HttpPost]
        public async Task<int> insertGetID([FromBody]UserPriviligeBranchesVM entity)
        {
            var result = 0;
            foreach (var item in entity.COM_BRN_ID)
            {
                var entityAdd = new UserPriviligeBranches
                {
                    ID = entity.ID,
                    COM_BRN_ID = item
                };
                result = uPrivBranServices.InsertGetID(entityAdd);
                await LogData(null,entity.ID.ToString());
            };
            return result;
        }

        [Route("UserPriviligeBranchesServices/update")]
        [HttpPost]
        public async Task<IHttpActionResult> update([FromBody]UserPriviligeBranches entity)
        {
            await uPrivBranServices.UpdateAsync(entity);
            await LogData(null,entity.ID.ToString());
            return Ok() ;
        }
        [Route("UserPriviligeBranchesServices/delete")]
        [HttpPost]
        public async Task<IHttpActionResult> delete([FromBody]UserPriviligeBranches entity)
        {
            await uPrivBranServices.DeleteAsync(entity);
            await LogData(null,entity.ID.ToString());
            return Ok();
        }
        [Route("UserPriviligeBranchesServices/get")]
        [HttpGet]
        public async Task<IHttpActionResult> get(int pageNum, int pageSize)
        {
            return Ok(await uPrivBranServices.GetAllAsync(pageNum, pageSize));
        }
        [Route("UserPriviligeBranchesServices/getAll")]
        [HttpGet]
        public List<UserPriviligeBranches> getAll()
        {
            return uPrivBranServices.GetAll();
        }

        [Route("UserPriviligeBranchesServices/count")]
        [HttpGet]
        public async Task<IHttpActionResult> count()
        {
            return Ok(await uPrivBranServices.CountAsync());
        }

        [Route("UserPriviligeBranchesServices/getById")]
        [HttpGet]
        public List<UserPriviligeBranches> getById(int id)
        {
            return uPrivBranServices.GetById(id);
        }

        [Route("UserPriviligeBranchesServices/getByCN")]
        [HttpPost]
        public Task<List<UserPriviligeBranches>> getByCN([FromBody]string name)
        {
            if (name.Contains("@"))
            {
                name = name.Split('@')[0];
            }

            return uPrivBranServices.GetByCN(name);
        }

        public async Task<bool> LogData(string code = null,string id=null)
        {
            Logger logger = new Logger();
            await userLogFileService.Insert(logger.LogDataMethod(code,id));
            return true;
        }

        #endregion
    }
}
