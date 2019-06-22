using ResortERP.Core.VM;
using ResortERP.Repository;
using ResortERP.Service.IServices;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace ResortERP.Api.Controllers
{
    [Logger]
    public class UserGroupController : ApiController
    {
        IUserGroupService userGroupService;
        IUserLogFileService _userLogFileService;
        public UserGroupController(IUserGroupService userGroupService, IUserLogFileService _userLogFileService)
        {
            this.userGroupService = userGroupService;
            this._userLogFileService = _userLogFileService;
        }

        [Route("userGroup/insert")]
        [HttpPost]
        public async Task<IHttpActionResult> add([FromBody]UserGroupVM entity)
        {
            var result = await userGroupService.InsertAsync(entity);
            await LogData(null,result.ToString());
            if (result != 0)
            {
                return Ok(true);
            }
            return Ok(false);
        }
        [Route("userGroup/update")]
        [HttpPost]
        public async Task<IHttpActionResult> update([FromBody]UserGroupVM entity)
        {
            var result = await userGroupService.UpdateAsync(entity);
            await LogData(null,entity.ID.ToString());
            return Ok(result);
        }
        [Route("userGroup/delete")]
        [HttpPost]
        public async Task<IHttpActionResult> delete([FromBody]UserGroupVM entity)
        {
            var result = await userGroupService.DeleteAsync(entity);
            await LogData(null,entity.ID.ToString());
            return Ok(result);

        }
        [Route("userGroup/get")]
        [HttpGet]
        public async Task<IHttpActionResult> get(int pageNum, int pageSize)
        {
            return Ok(await userGroupService.GetAllAsync(pageNum, pageSize));
        }

        [Route("userGroup/count")]
        [HttpGet]
        public async Task<IHttpActionResult> count()
        {
            return Ok(await userGroupService.CountAsync());
        }
        [Route("userGroup/getAll")]
        [HttpGet]
        public List<UserGroupVM> getAll()
        {
            return userGroupService.GetAll();
        }

        public async Task<bool> LogData(string code = null,string id=null)
        {
            Logger logger = new Logger();
            await _userLogFileService.Insert(logger.LogDataMethod(code,id));
            return true;
        }
    }
}
