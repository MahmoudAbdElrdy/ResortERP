using ResortERP.Core;
using ResortERP.Core.VM;
using ResortERP.Repository;
using ResortERP.Service.IServices;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace ResortERP.Api.Controllers
{
    [Logger]
    public class UserMenuController : ApiController
    {
        IUserMenuService _statusService;
        IUserLogFileService _userLogFileService;
        IUserFormService _userFormService;
        public UserMenuController(IUserMenuService statusService, IUserLogFileService userLogFileService, IUserFormService userFormService)
        {
            _statusService = statusService;
            _userLogFileService = userLogFileService;
            _userFormService = userFormService;
        }


        [Route("UserMenu/GetUserMenu/{country}/{usershortcut}/{lang}")]
        [HttpGet]
        public UserMenuAPICollection GetAllUserMenu(int country = 1, int usershortcut = 1, int lang = 2)
        {
            var json = new UserMenuAPICollection();
            json.UserMenu = _statusService.GetAllUserMenu(country, usershortcut, lang);
            return json;
        }

        [Route("UserMenu/GetUserMenuByID/{usermenubyid}/{lang}")]
        [HttpGet]
        public UserMenuAPICollection GetUserMenuByID(int UserMenuByID = 1, int lang = 2)
        {
            var json = new UserMenuAPICollection();
            json.UserMenu = _statusService.GetUserMenuByID(UserMenuByID, lang);
            return json;
        }

        [Route("UserMenu/GetAllUserMenuForWeb/{usershortcut}/{lang}")]
        [HttpGet]
        public UserMenuAPICollection GetAllUserMenuForWeb(int usershortcut = 1, int lang = 2)
        {

            var json = new UserMenuAPICollection();
            json.UserMenu = _statusService.GetUserMenu(lang);
            //json.UserMenu = _statusService.GetAllUserMenuForWeb(usershortcut, lang);
            return json;
        }



        public class UserMenuAPICollection
        {
            public List<UserMenuVM> UserMenu { get; set; }
        }




        [Route("UserMenu/insert")]
        [HttpPost]
        public async Task<IHttpActionResult> InsertUserMenu(UserMenuVM userMenu)
        {
            var result = await _statusService.InsertUserMenu(userMenu);
            if (userMenu.MenuID != null && userMenu.MenuID != 0)
            {
                // await _userFormService.InsertUserForm(userMenu, result);
                await LogData(null, result.ToString());
            }

            if (result != 0)
            {
                return Ok(true);
            };
            return Ok(false);
        }



        [Route("UserMenu/update")]
        [HttpPost]
        public async Task<IHttpActionResult> UpdateUserMenu(UserMenuVM userMenu)
        {
            var result = await _statusService.UpdateUserMenu(userMenu);

            if (userMenu.MenuID != null && userMenu.MenuID != 0)
            {
                // await _userFormService.InsertUserForm(userMenu, result);
                await LogData(null, userMenu.ID.ToString());
            }

            return Ok(result);
        }


        [Route("UserMenu/delete")]
        [HttpPost]
        public async Task<IHttpActionResult> DeleteUserMenu(UserMenuVM userMenu)
        {
            var result = await _statusService.DeleteUserMenu(userMenu);
            if (userMenu.MenuID != null && userMenu.MenuID != 0)
            {
                // await _userFormService.InsertUserForm(userMenu, result);
                await LogData(null, userMenu.ID.ToString());
            }

            return Ok(result);
        }


        [Route("UserMenu/getMainMenu")]
        [HttpGet]
        public async Task<IHttpActionResult> GetMainMenues()
        {
            return Ok(await _statusService.GetMainMenu());
        }

        [Route("UserMenu/getMenuBySettingId")]
        [HttpGet]
        public UserMenu GetMenueBySetiingID(int settingID)
        {
            return _statusService.GetUserMenuBySettingID(settingID);
        }


        [Route("UserMenu/getMenuByEntrySettingId")]
        [HttpGet]
        public UserMenu GetMenueByEntrySetiingID(int settingID)
        {
            return _statusService.GetUserMenuByEntrySettingID(settingID);
        }

        [Route("UserMenu/getAllMenuAsync")]
        [HttpGet]
        public async Task<IHttpActionResult> getAllMenuAsync(int pageNum, int pageSize)
        {
            return Ok(await _statusService.getAllMenuAsync(pageNum, pageSize));
        }



        [Route("UserMenu/count")]
        [HttpGet]
        public async Task<IHttpActionResult> countBanks()
        {
            return Ok(await _statusService.CountUserMenuAsync());
        }


        [Route("UserMenu/getLastCode")]
        [HttpGet]
        public string GetLastCode()
        {
            return _statusService.GetLastCode();
        }

        public async Task<bool> LogData(string code = null, string id = null)
        {
            //Logger logger = new Logger();
            //await _userLogFileService.Insert(logger.LogDataMethod(code,id));
            return true;
        }
    }
}
