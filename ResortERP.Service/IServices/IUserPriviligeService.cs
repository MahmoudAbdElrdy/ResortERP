using ResortERP.Core;
using ResortERP.Core.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace ResortERP.Service.IServices
{
    public interface IUserPriviligeService
    {
        List<UserMenuVM> GetAllUserMenu(int country = 1, int usershortcut = 1, int lang = 1);
        List<UserMenuVM> GetAllUserMenuForWeb(int country = 1, int usershortcut = 1, int lang = 1);
        List<UserPriviligeVM> GetAllUserMenusForUser(int UserID);
        List<UserPermissionsVM> getAllUserPermissions(int UserID);
        List<UserMenuVM> GetMasterUserMenus( int usershortcut = 1, int lang = 1, int MenuID = 0);
        Task<List<UserPriviligeVM>> InsertUserPrivilages (List<UserPriviligeVM> UserPrivilages);
        List<UserPriviligeVM> getByMenuId(int menuId);
    }
}
