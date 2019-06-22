using ResortERP.Api.Classes;
using ResortERP.Core;
using ResortERP.Core.VM;
using ResortERP.Repository;
using ResortERP.Service;
using ResortERP.Service.IServices;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.UI.WebControls;

namespace ResortERP.Api.Controllers
{
    [Logger]
    public class UserPriviligeController : ApiController
    {
        IUserPriviligeService _userPrivilagesService;
        IUserLogFileService _userLogFileService;
        public UserPriviligeController(IUserPriviligeService UserPrivilagesService, IUserLogFileService userLogFileService)
        {
            _userPrivilagesService = UserPrivilagesService;
            _userLogFileService = userLogFileService;
        }


        [Route("UserPrivilige/GetUserMenu/{country}/{usershortcut}/{lang}")]
        [HttpGet]
        public UserMenuAPICollection GetAllUserMenu(int country = 1, int usershortcut = 1, int lang = 2)
        {
            var json = new UserMenuAPICollection();
            json.UserMenu = _userPrivilagesService.GetAllUserMenu(country, usershortcut, lang);
            return json;
        }
        [Route("UserPrivilige/GetMasterUserMenus/{usershortcut}/{lang}/{MenuID}")]
        [HttpGet]
        public List<UserMenuVM> GetMasterUserMenus(int usershortcut = 1, int lang = 1, int MenuID = 0)
        {
            List<UserMenuVM> MasterUserMenus = _userPrivilagesService.GetMasterUserMenus( usershortcut, lang, MenuID);
            return MasterUserMenus;
        }

        [Route("UserPrivilige/GetAllUserMenuForWeb/{country}/{usershortcut}/{lang}")]
        [HttpGet]
        public UserMenuAPICollection GetAllUserMenuForWeb(int country = 1, int usershortcut = 1, int lang = 2)
        {
            var json = new UserMenuAPICollection();
            json.UserMenu = _userPrivilagesService.GetAllUserMenuForWeb(country, usershortcut, lang);
            return json;
        }
        [Route("UserPrivilige/GetAllUserMenusForUser/{UserID}")]
        [HttpGet]
        public List<UserPriviligeVM> GetAllUserMenusForUser(int UserID)
        {
            List<UserPriviligeVM> UserPrivilagesForUser = _userPrivilagesService.GetAllUserMenusForUser(UserID);
            return UserPrivilagesForUser;
        }
        
        [Route("UserPrivilige/getAllUserPermissions/{UserID}")]
        [HttpGet]
        public List<UserPermissionsVM> getAllUserPermissions(int UserID)
        {
            List<UserPermissionsVM> UserPrivilages = _userPrivilagesService.getAllUserPermissions(UserID);
            return UserPrivilages;
        }

        [Route("UserPrivilige/getByMenuId")]
        [HttpGet]
        public List<UserPriviligeVM> InsertUserPrivilages(int id)
        {
            return  _userPrivilagesService.getByMenuId(id);
        }

        [Route("UserPrivilige/InsertUserPrivilages")]
        [HttpPost]
        public async Task<bool> InsertUserPrivilages([FromBody]List<UserPriviligeVM> UserPrivilages)
        {
            var result= await _userPrivilagesService.InsertUserPrivilages(UserPrivilages);
            if (result.FirstOrDefault().ID != 0)
            {
                await LogData(null, result.FirstOrDefault().ID.ToString());
                return true;
            }
            return false;
        }
        public class UserMenuAPICollection
        {
            public List<UserMenuVM> UserMenu { get; set; }
        }

        public async Task<bool> LogData(string code=null,string id=null)
        {
            Logger logger = new Logger();
            await _userLogFileService.Insert(logger.LogDataMethod(code,id));
            return true;
        }
    }
}
