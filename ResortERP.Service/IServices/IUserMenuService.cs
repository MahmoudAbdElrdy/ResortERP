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
    public interface IUserMenuService
    {

        List<UserMenuVM> GetAllUserMenu(int country = 1, int usershortcut = 1, int lang = 1);
        List<UserMenuVM> GetUserMenuByID(int UserMenuByID = 1, int lang = 1);

        List<UserMenuVM> GetAllUserMenuForWeb(int usershortcut = 1, int lang = 1);

        Task<int> InsertUserMenu(UserMenuVM entity);
        Task<bool> UpdateUserMenu(UserMenuVM entity);
        Task<bool> DeleteUserMenu(UserMenuVM entity);
        Task<List<UserMenu>> GetMainMenu();

        UserMenu GetUserMenuBySettingID(int settingID);
        UserMenu GetUserMenuByEntrySettingID(int settingID);
        Task<List<UserMenuVM>> getAllMenuAsync(int pageNum, int pageSize);
        Task<int> CountUserMenuAsync();
        string GetLastCode();

        // for get multi level menu 
        List<UserMenuVM> GetUserMenu(int lang = 1);
    }
}
