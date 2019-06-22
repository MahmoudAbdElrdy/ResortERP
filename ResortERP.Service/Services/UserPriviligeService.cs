using AutoMapper;
using ResortERP.Core;
using ResortERP.Core.VM;
using ResortERP.Repository;
using ResortERP.Service.IServices;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using System.Web.UI.WebControls;


namespace ResortERP.Service.Services
{
    public class UserPriviligeService : IUserPriviligeService
    {
        IGenericRepository<UserMenu> _UserMenuRepo;
        IGenericRepository<UserPrivilige> _userPriviliageRepo;
        public UserPriviligeService(IGenericRepository<UserMenu> UserMenuRepo, IGenericRepository<UserPrivilige> userPriviliageRepo)
        {
            _UserMenuRepo = UserMenuRepo;
            _userPriviliageRepo = userPriviliageRepo;
        }

        public List<UserMenuVM> GetMasterUserMenus(int usershortcut = 1, int lang = 1, int MenuID = 0)
        {

            var q = new List<UserMenuVM>();
            if (MenuID == 0)
            //Master
            {
                q = (from entity in _UserMenuRepo.GetAll()
                  .Where(x => x.Active == true && x.MenuID == null)
                  .OrderBy(x => x.Position)
                     select entity).ToList().Select(x => Mapper.Map<UserMenu, UserMenuVM>(x)).ToList();
            }
            else
            //Details
            {
                q = (from entity in _UserMenuRepo.GetAll()
                .Where(x => x.Active == true&& x.MenuID == MenuID)
                .OrderBy(x => x.Position)
                     select entity).ToList().Select(x => Mapper.Map<UserMenu, UserMenuVM>(x)).ToList();
            }

            if (lang == 1)
            {
                foreach (var item in q)
                {
                    item.NAME = item.ARName;
                }
            }
            else
            {
                foreach (var item in q)
                {
                    item.NAME = item.LatName;
                }
            }
            return q;
        }

        public List<UserMenuVM> GetAllUserMenu(int country = 1, int usershortcut = 1, int lang = 1)
        {
            var q = (from entity in _UserMenuRepo.GetAll()
                    .Where(x => x.Active == true && x.CountryID == country && x.UserShortcut == usershortcut && x.MenuID == null)
                    .OrderBy(x => x.Position)
                     select entity).ToList().Select(x => Mapper.Map<UserMenu, UserMenuVM>(x)).ToList();
            if (lang == 1)
            {
                foreach (var item in q)
                {
                    item.NAME = item.ARName;
                }
            }
            else
            {
                foreach (var item in q)
                {
                    item.NAME = item.LatName;
                }
            }
            List<UserMenuVM> UserMenuList = new List<UserMenuVM>();

            UserMenuList = ReturnCorrectUserMenus(q);
            return UserMenuList.OrderBy(x => x.Position).ToList();
        }



        public string GetUserId()
        {

            var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;
            // Get the claims values
            var UserId = identity.Claims.Where(c => c.Type == "userId").Select(c => c.Value)
                               .SingleOrDefault();
            return UserId;

        }

        public List<UserMenuVM> GetAllUserMenuForWeb(int country = 1, int usershortcut = 1, int lang = 1)
        {
            //Get User Id from Claim
            string UserId = GetUserId();
            //Get The Menu Ids from Table UserPrivilages
            var q = (from entity in _userPriviliageRepo.GetAll().Where(x => x.UserID == UserId)
                     select entity).ToList().Select(x => Mapper.Map<UserPrivilige, UserPriviligeVM>(x)).ToList();

            List<UserMenuVM> UserMenuList = new List<UserMenuVM>();
            foreach (var item in q)
            {
                UserMenuList = ReturnUserMenus(country, usershortcut, lang, UserMenuList, item.MenuID);
            }

            UserMenuList = ReturnCorrectUserMenus(UserMenuList);
            return UserMenuList.OrderBy(x => x.Position).ToList();
        }

        public List<UserPriviligeVM> GetAllUserMenusForUser(int UserID)
        {
            string UserId = Convert.ToString(UserID);
            var q = (from entity in _userPriviliageRepo.GetAll().Where(x => x.UserID == UserId)
                     select entity).ToList().Select(x => Mapper.Map<UserPrivilige, UserPriviligeVM>(x)).ToList();
            return q;
        }

        public List<UserPermissionsVM> getAllUserPermissions(int UserID)
        {
            string UserId = Convert.ToString(UserID);
            var q = (from Privilage in _userPriviliageRepo.GetAll().Where(x => x.UserID == UserId)
                join Menu in _UserMenuRepo.GetAll() on Privilage.MenuID equals Menu.ID
                select new UserPermissionsVM()
                {
                    ID = Privilage.ID,
                    MenuID = Privilage.MenuID,
                    FormID = Privilage.FormID,
                    MenuOpen = Privilage.MenuOpen,
                    MenuUrl = Menu.MenuLink,
                    OpAdd = Privilage.OpAdd,
                    OpDelete = Privilage.OpDelete,
                    OpPreview = Privilage.OpPreview,
                    OpPrint = Privilage.OpPrint,
                    OpSearch = Privilage.OpSearch,
                    OpUpdate = Privilage.OpUpdate,
                    UserOperationID = Privilage.UserOperationID
                }).ToList();
            return q;
        }

        public List<UserPriviligeVM> getByMenuId(int menuId)
        {
            var q = (from entity in _userPriviliageRepo.GetAll().Where(x => x.ID == menuId)
                     select entity).ToList().Select(x => Mapper.Map<UserPrivilige, UserPriviligeVM>(x)).ToList();
            if (q != null)
            {
                var result = (from entity in _userPriviliageRepo.GetAll().Where(x => x.UserID == q.FirstOrDefault().UserID)
                              select entity).ToList().Select(x => Mapper.Map<UserPrivilige, UserPriviligeVM>(x)).ToList();
                return result;
            }
           
            return q;
        }
        

        private List<UserMenuVM> ReturnUserMenus(int country, int usershortcut, int lang, List<UserMenuVM> UserMenuList, int MenuID)
        {
            UserMenu obj = _UserMenuRepo.GetByID(MenuID);
            UserMenuVM entityObj = new UserMenuVM();
            if (obj.Active == true && obj.CountryID == country && obj.UserShortcut == usershortcut)
            {

                if (lang == 1)
                {
                    entityObj.NAME = obj.ARName;
                }
                else
                {
                    entityObj.NAME = obj.LatName;
                }
                entityObj.Active = obj.Active;
                entityObj.AddedBy = obj.AddedBy;
                entityObj.AddedOn = obj.AddedOn;
                entityObj.ARName = obj.ARName;
                entityObj.Code = obj.Code;
                entityObj.CountryID = obj.CountryID;
                entityObj.IconClass = obj.IconClass;
                entityObj.IconImageURL = obj.IconImageURL;
                entityObj.ID = obj.ID;
                entityObj.LatName = obj.LatName;
                entityObj.MenuClass = obj.MenuClass;
                entityObj.MenuID = obj.MenuID;
                entityObj.MenuLink = obj.MenuLink;
                entityObj.Notes = obj.Notes;
                entityObj.Position = obj.Position;
                entityObj.UpdatedBy = obj.UpdatedBy;
                entityObj.UpdatedOn = obj.UpdatedOn;
                entityObj.UserRoleID = obj.UserRoleID;
                entityObj.UserShortcut = obj.UserShortcut;
                if (UserMenuList.Find(x => x.ID == entityObj.ID) == null)
                {
                    UserMenuList.Add(entityObj);
                }

                if (obj.MenuID != null)
                {

                    UserMenuList = ReturnUserMenus(country, usershortcut, lang, UserMenuList, Convert.ToInt32(obj.MenuID));
                }

            }
            return UserMenuList;
        }




        public List<UserMenuVM> ReturnCorrectUserMenus(List<UserMenuVM> UserMenus)
        {
            List<UserMenuVM> CorrectMenus = new List<UserMenuVM>();
            foreach (var UserMenuVMitem in UserMenus)
            {
                UserMenuVM obj = new UserMenuVM();
                obj.ARName = UserMenuVMitem.ARName;
                obj.LatName = UserMenuVMitem.LatName;
                obj.NAME = UserMenuVMitem.NAME;
                obj.UserShortcut = UserMenuVMitem.UserShortcut;
                obj.UserRoleID = UserMenuVMitem.UserRoleID;
                obj.MenuLink = UserMenuVMitem.MenuLink;
                obj.IconClass = UserMenuVMitem.IconClass;
                obj.IconImageURL = UserMenuVMitem.IconImageURL;
                obj.Position = UserMenuVMitem.Position;
                obj.ChildMenus = new List<UserMenuVM>();
                foreach (var item in UserMenus)
                {

                    if (UserMenuVMitem.ID == item.MenuID)
                    {
                        UserMenuVM child = new UserMenuVM();
                        child.ARName = item.ARName;
                        child.LatName = item.LatName;
                        child.NAME = item.NAME;
                        child.UserShortcut = item.UserShortcut;
                        child.UserRoleID = item.UserRoleID;
                        child.MenuLink = item.MenuLink;
                        child.IconClass = item.IconClass;
                        child.IconImageURL = item.IconImageURL;
                        child.Position = item.Position;
                        obj.ChildMenus.Add(child);
                    }
                }
                if (obj.ChildMenus != null && obj.ChildMenus.Count > 0)
                {
                    CorrectMenus.Add(obj);
                }
            }
            return CorrectMenus;
        }

        public async Task<List<UserPriviligeVM>> InsertUserPrivilages(List<UserPriviligeVM> UserPrivilages)
        {
            //Delete All Privilages for user
            List<UserPriviligeVM> ExistingPrivilages = GetAllUserMenusForUser(Convert.ToInt32(UserPrivilages[0].UserID));
            foreach (var item in ExistingPrivilages)
            {
                UserPrivilige deletedItem = new UserPrivilige
                {
                    ID = item.ID,
                    ARName = item.ARName

                };
               
                _userPriviliageRepo.Delete(deletedItem, item.ID);
            }

            //Add it again
            foreach (var item in UserPrivilages)
            {
                var dbEntity = Mapper.Map<UserPriviligeVM, UserPrivilige>(item);
                AsyncHelpers.RunSync<UserPrivilige>(() => {
                    return Task.Run<UserPrivilige>
                    (() => { _userPriviliageRepo.Add(dbEntity);item.ID=dbEntity.ID; return dbEntity; }); }
                );
               
                
            }

            return UserPrivilages;
        }
    }
}
