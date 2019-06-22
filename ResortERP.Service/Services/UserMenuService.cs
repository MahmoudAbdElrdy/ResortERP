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
    public class UserMenuService : IUserMenuService
    {
        IGenericRepository<UserMenu> _UserMenuRepo;
        IGenericRepository<UserPrivilige> _userPriviliageRepo;
        IGenericRepository<ENTRY_SETTINGS> _entrySettingsRepo;
        IGenericRepository<BILL_SETTINGS> _billSettingsRepo;
        IGenericRepository<ENTRY_MASTER> _entryMasterRepo;
        IGenericRepository<BILL_MASTER> _billMasterRepo;
        IGenericRepository<ENTRY_GRID_COLUMNS> _entryGrdColRepo;
        IGenericRepository<BILL_GRID_COLUMNS> _billGrdColRepo;

        public UserMenuService(IGenericRepository<UserMenu> UserMenuRepo, IGenericRepository<UserPrivilige> userPriviliageRepo, IGenericRepository<ENTRY_SETTINGS> entrySettingsRepo,
            IGenericRepository<BILL_SETTINGS> billSettingsRepo, IGenericRepository<ENTRY_MASTER> entryMasterRepo, IGenericRepository<BILL_MASTER> billMasterRepo,
             IGenericRepository<ENTRY_GRID_COLUMNS> entryGrdColRepo, IGenericRepository<BILL_GRID_COLUMNS> billGrdColRepo)
        {
            _UserMenuRepo = UserMenuRepo;
            _userPriviliageRepo = userPriviliageRepo;
            _entryMasterRepo = entryMasterRepo;
            _billMasterRepo = billMasterRepo;
            _entrySettingsRepo = entrySettingsRepo;
            _billSettingsRepo = billSettingsRepo;
            _entryGrdColRepo = entryGrdColRepo;
            _billGrdColRepo = billGrdColRepo;
        }

        public List<UserMenuVM> GetUserMenuByID(int UserMenuByID = 1, int lang = 1)
        {
            var q = (from entity in _UserMenuRepo.GetAll()
                    .Where(x => x.Active == true && x.ID == UserMenuByID)
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
            return q;
        }


        public List<UserMenuVM> GetAllUserMenu(int country = 1, int usershortcut = 1, int lang = 1)
        {
            var q = (from entity in _UserMenuRepo.GetAll()
                    .Where(x => x.Active == true && x.CountryID == country && x.UserShortcut == usershortcut && x.LanguageID == lang)
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
            return q;
        }



        public string GetUserId()
        {

            var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;
            // Get the claims values
            var UserId = identity.Claims.Where(c => c.Type == "userIdPK").Select(c => c.Value)
                               .SingleOrDefault();
            return UserId;

        }
        public string GetUserName()
        {

            var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;
            // Get the claims values
            var UserName = identity.Claims.Where(c => c.Type == "sub").Select(c => c.Value)
                               .SingleOrDefault();
            return UserName;

        }

        public List<UserMenuVM> GetAllUserMenuForWeb(int usershortcut = 1, int lang = 1)
        {

            try
            {


                //Get User Id from Claim
                List<UserMenuVM> UserMenuList = new List<UserMenuVM>();
                string UserId = GetUserId();
                string UserName = GetUserName();
                if (!UserName.Contains("@"))
                {
                    UserMenuList = (from q in _UserMenuRepo.GetAll().Where(x => x.Active == true) select q).ToList().Select(x => Mapper.Map<UserMenu, UserMenuVM>(x)).ToList();
                }
                else
                {
                    //Get The Menu Ids from Table UserPrivilages
                    var q = (from entity in _userPriviliageRepo.GetAll().Where(x => x.UserID == UserId)
                             select entity).ToList().Select(x => Mapper.Map<UserPrivilige, UserPriviligeVM>(x)).ToList();
                    foreach (var item in q)
                    {
                        UserMenuList = ReturnUserMenus(usershortcut, lang, UserMenuList, item.MenuID);
                    }
                }
                UserMenuList = ReturnCorrectUserMenus(UserMenuList);
                return UserMenuList.OrderBy(x => x.Position).ToList();

            }
            catch (Exception ex)
            {
                List<UserMenuVM> UserMenuList = new List<UserMenuVM>();
                return UserMenuList.OrderBy(x => x.Position).ToList();
            }
        }

        private List<UserMenuVM> ReturnUserMenus(int usershortcut, int lang, List<UserMenuVM> UserMenuList, int MenuID)
        {
            UserMenu obj = _UserMenuRepo.GetByID(MenuID);
            UserMenuVM entityObj = new UserMenuVM();
            if (obj.Active == true && obj.UserShortcut == usershortcut)
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
                    UserMenuList = ReturnUserMenus(usershortcut, lang, UserMenuList, Convert.ToInt32(obj.MenuID));
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
                obj.MenuClass = UserMenuVMitem.MenuClass;
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



        public Task<int> InsertUserMenu(UserMenuVM entity)
        {
            return Task.Run(() =>
            {
                UserMenu userMenu = new UserMenu();
                {
                    userMenu.ID = entity.ID;
                    userMenu.Code = entity.Code;
                    userMenu.ARName = entity.ARName;
                    userMenu.LatName = entity.LatName;
                    userMenu.MenuLink = entity.MenuLink;
                    userMenu.MenuClass = entity.MenuClass;
                    userMenu.IconClass = entity.IconClass;
                    userMenu.IconImageURL = entity.IconImageURL;
                    userMenu.MenuID = entity.MenuID;
                    userMenu.DisplayOrNot = entity.DisplayOrNot;
                    userMenu.UserShortcut = entity.UserShortcut;
                    userMenu.UserRoleID = entity.UserRoleID;
                    userMenu.MenuType = entity.MenuType;
                    userMenu.LanguageID = entity.LanguageID;
                    userMenu.ResourceURL = entity.ResourceURL;
                    userMenu.ResourceContent = entity.ResourceContent;
                    userMenu.Notes = entity.Notes;
                    userMenu.AddedBy = entity.AddedBy;
                    userMenu.AddedOn = entity.AddedOn;
                    userMenu.UpdatedBy = entity.UpdatedBy;
                    userMenu.UpdatedOn = entity.UpdatedOn;
                    userMenu.Active = entity.Active;
                    userMenu.Position = entity.Position;
                    userMenu.CountryID = entity.CountryID;
                    userMenu.FRName = entity.FRName;
                    userMenu.URName = entity.URName;
                    userMenu.TRName = entity.TRName;
                    userMenu.BillSetiingID = entity.BillSetiingID;
                    userMenu.EntrySettingID = entity.EntrySettingID;
                };

                _UserMenuRepo.Add(userMenu);

                return userMenu.ID;
            });

        }


        public Task<bool> UpdateUserMenu(UserMenuVM entity)
        {
            return Task.Run(() =>
            {
                UserMenu userMenu = new UserMenu();
                {
                    userMenu.ID = entity.ID;
                    userMenu.Code = entity.Code;
                    userMenu.ARName = entity.ARName;
                    userMenu.LatName = entity.LatName;
                    userMenu.MenuLink = entity.MenuLink;
                    userMenu.MenuClass = entity.MenuClass;
                    userMenu.IconClass = entity.IconClass;
                    userMenu.IconImageURL = entity.IconImageURL;
                    userMenu.MenuID = entity.MenuID;
                    userMenu.DisplayOrNot = entity.DisplayOrNot;
                    userMenu.UserShortcut = entity.UserShortcut;
                    userMenu.UserRoleID = entity.UserRoleID;
                    userMenu.MenuType = entity.MenuType;
                    userMenu.LanguageID = entity.LanguageID;
                    userMenu.ResourceURL = entity.ResourceURL;
                    userMenu.ResourceContent = entity.ResourceContent;
                    userMenu.Notes = entity.Notes;
                    userMenu.AddedBy = entity.AddedBy;
                    userMenu.AddedOn = entity.AddedOn;
                    userMenu.UpdatedBy = entity.UpdatedBy;
                    userMenu.UpdatedOn = entity.UpdatedOn;
                    userMenu.Active = entity.Active;
                    userMenu.Position = entity.Position;
                    userMenu.CountryID = entity.CountryID;
                    userMenu.FRName = entity.FRName;
                    userMenu.URName = entity.URName;
                    userMenu.TRName = entity.TRName;
                    userMenu.BillSetiingID = entity.BillSetiingID;
                    userMenu.EntrySettingID = entity.EntrySettingID;
                };

                _UserMenuRepo.Update(userMenu, userMenu.ID);
                return true;
            });

        }

        public Task<bool> DeleteUserMenu(UserMenuVM entity)
        {
            return Task.Run(() =>
            {

                bool used = CheckDeleteUserMenu(entity.ID, entity.AddedBy);

                if (!used)
                {
                    DeleteSubMenuOfMenu(entity.ID, entity.AddedBy);

                    UserMenu userMenu = new UserMenu();
                    {
                        userMenu.ID = entity.ID;
                        userMenu.Code = entity.Code;
                        userMenu.ARName = entity.ARName;
                        userMenu.LatName = entity.LatName;
                        userMenu.MenuLink = entity.MenuLink;
                        userMenu.MenuClass = entity.MenuClass;
                        userMenu.IconClass = entity.IconClass;
                        userMenu.IconImageURL = entity.IconImageURL;
                        userMenu.MenuID = entity.MenuID;
                        userMenu.DisplayOrNot = entity.DisplayOrNot;
                        userMenu.UserShortcut = entity.UserShortcut;
                        userMenu.UserRoleID = entity.UserRoleID;
                        userMenu.MenuType = entity.MenuType;
                        userMenu.LanguageID = entity.LanguageID;
                        userMenu.ResourceURL = entity.ResourceURL;
                        userMenu.ResourceContent = entity.ResourceContent;
                        userMenu.Notes = entity.Notes;
                        userMenu.AddedBy = entity.AddedBy;
                        userMenu.AddedOn = entity.AddedOn;
                        userMenu.UpdatedBy = entity.UpdatedBy;
                        userMenu.UpdatedOn = entity.UpdatedOn;
                        userMenu.Active = entity.Active;
                        userMenu.Position = entity.Position;
                        userMenu.CountryID = entity.CountryID;
                        userMenu.FRName = entity.FRName;
                        userMenu.URName = entity.URName;
                        userMenu.TRName = entity.TRName;
                        userMenu.BillSetiingID = entity.BillSetiingID;
                        userMenu.EntrySettingID = entity.EntrySettingID;
                    };

                    _UserMenuRepo.Delete(userMenu, userMenu.ID);


                    return true;
                }
                else
                {
                    return false;
                }
                //  return true;
            });

        }


        public bool CheckDeleteUserMenu(int menuID, string addedBy)
        {
            bool used = false;
            List<ENTRY_SETTINGS> entrySetting = _entrySettingsRepo.Filter(s => s.MenuID == menuID && s.AddedBy == addedBy).ToList();
            if (entrySetting.Count > 0)
            {
                foreach (var entrySet in entrySetting)
                {
                    List<ENTRY_MASTER> entry = _entryMasterRepo.Filter(e => e.ENTRY_SETTING_ID == entrySet.ENTRY_SETTING_ID).ToList();
                    if (entry.Count > 0)
                    {
                        used = true;
                    }
                }
            }

            List<BILL_SETTINGS> billSetting = _billSettingsRepo.Filter(b => b.MenuID == menuID && b.AddedBy == addedBy).ToList();
            if (billSetting.Count > 0)
            {
                foreach (var billSet in billSetting)
                {
                    List<BILL_MASTER> bill = _billMasterRepo.Filter(b => b.BILL_SETTING_ID == billSet.BILL_SETTING_ID).ToList();
                    if (bill.Count > 0)
                    {
                        used = true;
                    }
                }
            }
            return used;
        }

        public void DeleteSubMenuOfMenu(int menuID, string addedBy)
        {
            List<ENTRY_SETTINGS> entrySetting = _entrySettingsRepo.Filter(s => s.MenuID == menuID && s.AddedBy == addedBy && s.MenuID != null && s.AddedBy != null).ToList();
            if (entrySetting.Count > 0)
            {

                foreach (var set in entrySetting)
                {

                    ENTRY_GRID_COLUMNS egc = _entryGrdColRepo.Filter(X => X.ENTRY_SETTING_ID == set.ENTRY_SETTING_ID).FirstOrDefault();

                    if (egc != null)
                    {
                        ENTRY_GRID_COLUMNS egc_ = new ENTRY_GRID_COLUMNS
                        {
                            ENTRY_SETTING_ID = egc.ENTRY_SETTING_ID,
                            ACC_COLOR = egc.ACC_COLOR,
                            ACC_INDEX = egc.ACC_INDEX,
                            ACC_WIDTH = egc.ACC_WIDTH,
                            COST_CENTER_COLOR = egc.COST_CENTER_COLOR,
                            COST_CENTER_INDEX = egc.COST_CENTER_INDEX,
                            COST_CENTER_WIDTH = egc.COST_CENTER_WIDTH,
                            CREDIT_COLOR = egc.CREDIT_COLOR,
                            CREDIT_INDEX = egc.CREDIT_INDEX,
                            CREDIT_WIDTH = egc.CREDIT_WIDTH,
                            DEBIT_COLOR = egc.DEBIT_COLOR,
                            DEBIT_INDEX = egc.DEBIT_INDEX,
                            DEBIT_WIDTH = egc.DEBIT_WIDTH,
                            REMARKS_COLOR = egc.REMARKS_COLOR,
                            REMARKS_INDEX = egc.REMARKS_INDEX,
                            REMARKS_WIDTH = egc.REMARKS_WIDTH,
                            AddedBy = egc.AddedBy,
                            AddedOn = egc.AddedOn,
                            UpdatedBy = egc.UpdatedBy,
                            updatedOn = egc.updatedOn,
                            ACC_COLOR_HEXA = egc.ACC_COLOR_HEXA,
                            COST_CENTER_COLOR_HEXA = egc.COST_CENTER_COLOR_HEXA,
                            CREDIT_COLOR_HEXA = egc.CREDIT_COLOR_HEXA,
                            DEBIT_COLOR_HEXA = egc.DEBIT_COLOR_HEXA,
                            REMARKS_COLOR_HEXA = egc.REMARKS_COLOR_HEXA,
                            //  TaxAccountID = entity.TaxAccountID
                        };
                        _entryGrdColRepo.Delete(egc_, egc.ENTRY_SETTING_ID);


                        ENTRY_SETTINGS et = new ENTRY_SETTINGS
                        {
                            ENTRY_SETTING_ID = set.ENTRY_SETTING_ID,
                            ACCEPT_DIST_ACC = set.ACCEPT_DIST_ACC,
                            AUTO_POSTING = set.AUTO_POSTING,
                            COSTCENTER_BELONG = set.COSTCENTER_BELONG,
                            CURRENCY_ID = set.CURRENCY_ID,
                            ENTRY_ACC_ID = set.ENTRY_ACC_ID,
                            ENTRY_SETTING_AR_ABRV = set.ENTRY_SETTING_AR_ABRV,
                            ENTRY_SETTING_AR_NAME = set.ENTRY_SETTING_AR_NAME,
                            ENTRY_SETTING_EN_ABRV = set.ENTRY_SETTING_EN_ABRV,
                            ENTRY_SETTING_EN_NAME = set.ENTRY_SETTING_EN_NAME,
                            ENTRY_TYPE_ID = set.ENTRY_TYPE_ID,
                            AddedBy = set.AddedBy,
                            AddedOn = set.AddedOn,
                            UpdatedBy = set.UpdatedBy,
                            updatedOn = set.updatedOn,
                            TaxAccountID = set.TaxAccountID,
                            MenuID = set.MenuID

                        };
                        _entrySettingsRepo.Delete(et, set.ENTRY_SETTING_ID);

                    }
                }
            }


            List<BILL_SETTINGS> billSetting = _billSettingsRepo.Filter(b => b.MenuID == menuID && b.AddedBy == addedBy).ToList();
            if (billSetting.Count > 0)
            {
                foreach (var bill in billSetting)
                {


                    BILL_GRID_COLUMNS billgrdCol = _billGrdColRepo.Filter(x => x.BILL_SETTING_ID == bill.BILL_SETTING_ID).FirstOrDefault();
                    if (billgrdCol != null)
                    {
                        BILL_GRID_COLUMNS bgc = new BILL_GRID_COLUMNS
                        {
                            AddedBy = billgrdCol.AddedBy,
                            AddedOn = billgrdCol.AddedOn,
                            BILL_GRID_ID = billgrdCol.BILL_GRID_ID,
                            BILL_REMARKS_INDEX = billgrdCol.BILL_REMARKS_INDEX,
                            BILL_REMARKS_WIDTH = billgrdCol.BILL_REMARKS_WIDTH,
                            BILL_SETTING_ID = billgrdCol.BILL_SETTING_ID,
                            CEXTERNALLEXPENSES_INDEX = billgrdCol.CEXTERNALLEXPENSES_INDEX,
                            CEXTERNALLEXPENSES_WIDTH = billgrdCol.CEXTERNALLEXPENSES_WIDTH,
                            CINVENTORYVALUE_INDEX = billgrdCol.CINVENTORYVALUE_INDEX,
                            CINVENTORYVALUE_WIDTH = billgrdCol.CINVENTORYVALUE_WIDTH,
                            CTOTALCURR_INDEX = billgrdCol.CTOTALCURR_INDEX,
                            CTOTALCURR_WIDTH = billgrdCol.CTOTALCURR_WIDTH,
                            CTOTALWITHEXPENSES_INDEX = billgrdCol.CTOTALWITHEXPENSES_INDEX,
                            CTOTALWITHEXPENSES_WIDTH = billgrdCol.CTOTALWITHEXPENSES_WIDTH,
                            DISC_RATE_INDEX = billgrdCol.DISC_RATE_INDEX,
                            DISC_RATE_WIDTH = billgrdCol.DISC_RATE_WIDTH,
                            DISC_VALUE_INDEX = billgrdCol.DISC_VALUE_INDEX,
                            DISC_VALUE_WIDTH = billgrdCol.DISC_VALUE_WIDTH,
                            EMPLOYEE_INDEX = billgrdCol.EMPLOYEE_INDEX,
                            EMPLOYEE_WIDTH = billgrdCol.EMPLOYEE_WIDTH,
                            END_USERS_INDEX = billgrdCol.END_USERS_INDEX,
                            END_USERS_WIDTH = billgrdCol.END_USERS_WIDTH,
                            EXPIRED_DATE_INDEX = billgrdCol.EXPIRED_DATE_INDEX,
                            EXPIRED_DATE_WIDTH = billgrdCol.EXPIRED_DATE_WIDTH,
                            EXPORT_INDEX = billgrdCol.EXPORT_INDEX,
                            EXPORT_WIDTH = billgrdCol.EXPORT_WIDTH,
                            EXTRA_RATE_INDEX = billgrdCol.EXTRA_RATE_INDEX,
                            EXTRA_RATE_WIDTH = billgrdCol.EXTRA_RATE_WIDTH,
                            EXTRA_VALUE_INDEX = billgrdCol.EXTRA_VALUE_INDEX,
                            EXTRA_VALUE_WIDTH = billgrdCol.EXTRA_VALUE_WIDTH,
                            GIFTS_INDEX = billgrdCol.GIFTS_INDEX,
                            GIFTS_WIDTH = billgrdCol.GIFTS_WIDTH,
                            HALF_WHOLE_INDEX = billgrdCol.HALF_WHOLE_INDEX,
                            HALF_WHOLE_WIDTH = billgrdCol.HALF_WHOLE_WIDTH,
                            ITEM_INDEX = billgrdCol.ITEM_INDEX,
                            ITEM_REMAIN_QTY_INDEX = billgrdCol.ITEM_REMAIN_QTY_INDEX,
                            ITEM_WIDTH = billgrdCol.ITEM_WIDTH,
                            ITEM_REMAIN_QTY_WIDTH = billgrdCol.ITEM_REMAIN_QTY_WIDTH,
                            LAST_BUY_INDEX = billgrdCol.LAST_BUY_INDEX,
                            LAST_BUY_WIDTH = billgrdCol.LAST_BUY_WIDTH,
                            PRODUCTION_DATE_INDEX = billgrdCol.PRODUCTION_DATE_INDEX,
                            PRODUCTION_DATE_WIDTH = billgrdCol.PRODUCTION_DATE_WIDTH,
                            QTY_INDEX = billgrdCol.QTY_INDEX,
                            QTY_WIDTH = billgrdCol.QTY_WIDTH,
                            RETAIL_INDEX = billgrdCol.RETAIL_INDEX,
                            RETAIL_WIDTH = billgrdCol.RETAIL_WIDTH,
                            TOTAL_INDEX = billgrdCol.TOTAL_INDEX,
                            TOTAL_WIDTH = billgrdCol.TOTAL_WIDTH,
                            UNIT_COST_INDEX = billgrdCol.UNIT_COST_INDEX,
                            UNIT_COST_WIDTH = billgrdCol.UNIT_COST_WIDTH,
                            UNIT_INDEX = billgrdCol.UNIT_INDEX,
                            UNIT_WIDTH = billgrdCol.UNIT_WIDTH,
                            UpdatedBy = billgrdCol.UpdatedBy,
                            updatedOn = billgrdCol.updatedOn,
                            WHOLE_INDEX = billgrdCol.WHOLE_INDEX,
                            WHOLE_WIDTH = billgrdCol.WHOLE_WIDTH,



                        };
                        _billGrdColRepo.Delete(bgc, billgrdCol.BILL_GRID_ID);
                    }

                    BILL_SETTINGS bs = new BILL_SETTINGS
                    {
                        ABRV_BILL = bill.ABRV_BILL,
                        ACC_COST_ID = bill.ACC_COST_ID,
                        ACC_DISC_ID = bill.ACC_DISC_ID,
                        ACC_EXTRA_ID = bill.ACC_EXTRA_ID,
                        ACC_GIFT_ID = bill.ACC_GIFT_ID,
                        ACC_ITEM_ID = bill.ACC_ITEM_ID,
                        ACC_PAY_ID = bill.ACC_PAY_ID,
                        ACC_STORE_ID = bill.ACC_STORE_ID,
                        AddedBy = bill.AddedBy,
                        AddedOn = bill.AddedOn,
                        ADD_BILL_ENTRY_WITH_THE_MAIN_CURRENCY_RATE = bill.ADD_BILL_ENTRY_WITH_THE_MAIN_CURRENCY_RATE,
                        ALTERNATE_COLOR = bill.ALTERNATE_COLOR,
                        AUTOMATIC_DISCOUNT = bill.AUTOMATIC_DISCOUNT,
                        BILL_ABRV_AR = bill.BILL_ABRV_AR,
                        BILL_ABRV_EN = bill.BILL_ABRV_EN,
                        BILL_AR_NAME = bill.BILL_AR_NAME,
                        BILL_EN_NAME = bill.BILL_EN_NAME,
                        BILL_PAY_TYPE = bill.BILL_PAY_TYPE,
                        BILL_SELL_TYPE_ID = bill.BILL_SELL_TYPE_ID,
                        BILL_SETTING_ID = bill.BILL_SETTING_ID,
                        BILL_SHORTCUT = bill.BILL_SHORTCUT,
                        BILL_SHOW_NAME = bill.BILL_SHOW_NAME,
                        BILL_TYPE_ID = bill.BILL_TYPE_ID,
                        COM_STORE_ID = bill.COM_STORE_ID,
                        CONTINEOUS_INVENTORY = bill.CONTINEOUS_INVENTORY,
                        COST_CENTER_ID = bill.COST_CENTER_ID,
                        CURRENCY_ID = bill.CURRENCY_ID,
                        CURRENCY_RATE = bill.CURRENCY_RATE,
                        DEFAULT_COLOR = bill.DEFAULT_COLOR,
                        Disable = bill.Disable,
                        DISABLE_BILL_PAY_TYPE = bill.DISABLE_BILL_PAY_TYPE,
                        FIRSTBILLMESSAGE = bill.FIRSTBILLMESSAGE,
                        FIRST_EXPIRE = bill.FIRST_EXPIRE,
                        GENERATE_COST_ENTRY = bill.GENERATE_COST_ENTRY,
                        GENERATE_ENTRY_STATE = bill.GENERATE_ENTRY_STATE,
                        GST = bill.GST,
                        GSTNAME = bill.GSTNAME,
                        HEADERBILLMESSAGE = bill.HEADERBILLMESSAGE,
                        IS_AUTO_POSTING = bill.IS_AUTO_POSTING,
                        IS_IT_CASHER_BILL = bill.IS_IT_CASHER_BILL,
                        IS_IT_SALES_POINT = bill.IS_IT_SALES_POINT,
                        IS_IT_SERVICE_BILL = bill.IS_IT_SERVICE_BILL,
                        IS_IT_TAX_SALE_BILL = bill.IS_IT_TAX_SALE_BILL,
                        IS_IT_THREADING = bill.IS_IT_THREADING,
                        LAST_PAY_PRICE = bill.LAST_PAY_PRICE,
                        MIN_QTY = bill.MIN_QTY,
                        MODULE_CARS = bill.MODULE_CARS,
                        NUMBEROFCOPIES = bill.NUMBEROFCOPIES,
                        OUT_MINUS = bill.OUT_MINUS,
                        PRICE_COST_EFFECT = bill.PRICE_COST_EFFECT,
                        PRINTHALFPAGE = bill.PRINTHALFPAGE,
                        PRINTHALFPAGEBYLONG = bill.PRINTHALFPAGEBYLONG,
                        PRINTLANDSCIP = bill.PRINTLANDSCIP,
                        PRINT_BILL_AS_RESET_OR_AS_BILL = bill.PRINT_BILL_AS_RESET_OR_AS_BILL,
                        PRINT_BILL_AUTOMATIC = bill.PRINT_BILL_AUTOMATIC,
                        PST = bill.PST,
                        PSTNAME = bill.PSTNAME,
                        REPEATETHE_ITEM_AT_THE_BILL = bill.REPEATETHE_ITEM_AT_THE_BILL,
                        SATTLEMENT_INCOME_LIST = bill.SATTLEMENT_INCOME_LIST,
                        SEARCH_ONLY_BY_DEAULT_UNIT = bill.SEARCH_ONLY_BY_DEAULT_UNIT,
                        SECONDBILLMESSAGE = bill.SECONDBILLMESSAGE,
                        SERIAL_NUMBER = bill.SERIAL_NUMBER,
                        SHOW_BILL_PAY_TYPE = bill.SHOW_BILL_PAY_TYPE,
                        SHOW_COST_CENTER = bill.SHOW_COST_CENTER,
                        SHOW_EMPLOYEE = bill.SHOW_EMPLOYEE,
                        SHOW_THE_COMPANY_DATA_ON_BILL = bill.SHOW_THE_COMPANY_DATA_ON_BILL,
                        SHOW_THE_CURRENCY = bill.ABRV_BILL,
                        SHOW_THE_CURRENT_QTY_ON_BILL = bill.SHOW_THE_CURRENT_QTY_ON_BILL,
                        SHOW_THE_ITEM_CODE_ON_BILL = bill.SHOW_THE_ITEM_CODE_ON_BILL,
                        SHOW_THE_LAST_BALANCE_ON_THE_BILL = bill.SHOW_THE_LAST_BALANCE_ON_THE_BILL,
                        SHOW_THE_LAST_DATE_FOR_RETURN = bill.SHOW_THE_LAST_DATE_FOR_RETURN,
                        SHOW_THE_RETURN_PERCENTAGE = bill.SHOW_THE_RETURN_PERCENTAGE,
                        STORE_EFFECT_STATE = bill.STORE_EFFECT_STATE,
                        THESECONDCELLISTHEITEMCELLNOTTHEQUANTITY = bill.THESECONDCELLISTHEITEMCELLNOTTHEQUANTITY,
                        TO_COM_STORE_ID = bill.TO_COM_STORE_ID,
                        UpdatedBy = bill.UpdatedBy,
                        UpdatedOn = bill.UpdatedOn,
                        ALTERNATE_COLOR_HEXA = bill.ALTERNATE_COLOR_HEXA,
                        DEFAULT_COLOR_HEXA = bill.DEFAULT_COLOR_HEXA,
                        Tax = bill.Tax,
                        CommissionTax = bill.CommissionTax,

                        IsAccount = bill.IsAccount,
                        IsBillDate = bill.IsBillDate,
                        IsBillRemarks = bill.IsBillRemarks,
                        IsCompStoreID = bill.IsCompStoreID,
                        IsCurrency = bill.IsCurrency,
                        IsCurrencyTrans = bill.IsCurrencyTrans,
                        IsCustAccID = bill.IsCustAccID,
                        IsEmpID = bill.IsEmpID,
                        IsItemAccID = bill.IsItemAccID,
                        IsPayTypes = bill.IsPayTypes,
                        IsPayWay = bill.IsPayWay,
                        IsSellType = bill.IsSellType,
                        IsShiftNumber = bill.IsShiftNumber,
                        BillAccountName = bill.BillAccountName,
                        BillEmpName = bill.BillEmpName,

                        IsToCompStore = bill.IsToCompStore,
                        IsTotalExtra = bill.IsTotalExtra,
                        IsTotalMustPaid = bill.IsTotalMustPaid,
                        IsTotalPaid = bill.IsTotalPaid,
                        // IsTotalPrice = entity.IsTotalPrice,
                        IsTotalRemaining = bill.IsTotalRemaining,

                        AccWagesID = bill.AccWagesID,
                        IsItems = bill.IsItems,
                        IsItemsGroups = bill.IsItemsGroups,

                        AccSalesID = bill.AccSalesID,
                        AccVatRateID = bill.AccVatRateID,
                        MenuID = bill.MenuID

                    };
                    _billSettingsRepo.Delete(bs, bill.BILL_SETTING_ID);

                }


            }

            // return true;
        }


        public Task<List<UserMenuVM>> getAllMenuAsync(int pageNum, int pageSize)
        {
            return Task.Run(() =>
            {

                int rowCount;
                var menuList = from entity in _UserMenuRepo.GetPaged<int>(pageNum, pageSize, x => x.MenuID == null, p => p.ID, false, out rowCount)

                               select new UserMenuVM
                               {
                                   ID = entity.ID,
                                   Code = entity.Code,
                                   ARName = entity.ARName,
                                   LatName = entity.LatName,
                                   MenuLink = entity.MenuLink,
                                   MenuClass = entity.MenuClass,
                                   IconClass = entity.IconClass,
                                   IconImageURL = entity.IconImageURL,
                                   MenuID = entity.MenuID,
                                   DisplayOrNot = entity.DisplayOrNot,
                                   UserShortcut = entity.UserShortcut,
                                   UserRoleID = entity.UserRoleID,
                                   MenuType = entity.MenuType,
                                   LanguageID = entity.LanguageID,
                                   ResourceURL = entity.ResourceURL,
                                   ResourceContent = entity.ResourceContent,
                                   Notes = entity.Notes,
                                   AddedBy = entity.AddedBy,
                                   AddedOn = entity.AddedOn,
                                   UpdatedBy = entity.UpdatedBy,
                                   UpdatedOn = entity.UpdatedOn,
                                   Active = entity.Active,
                                   Position = entity.Position,
                                   CountryID = entity.CountryID,
                                   FRName = entity.FRName,
                                   URName = entity.URName,
                                   TRName = entity.TRName,
                                   BillSetiingID = entity.BillSetiingID,
                                   EntrySettingID = entity.EntrySettingID

                               };
                return menuList.ToList();
            });
        }



        public Task<List<UserMenu>> GetMainMenu()
        {
            return Task.Run(() =>
            {
                var menus = _UserMenuRepo.Filter(m => m.MenuID == null);
                return menus.ToList();
            });
        }


        public UserMenu GetUserMenuBySettingID(int settingID)
        {
            var menu = _UserMenuRepo.Filter(m => m.BillSetiingID == settingID).FirstOrDefault();
            return menu;
        }

        public UserMenu GetUserMenuByEntrySettingID(int settingID)
        {
            var menu = _UserMenuRepo.Filter(m => m.EntrySettingID == settingID).FirstOrDefault();
            return menu;
        }




        public Task<int> CountUserMenuAsync()
        {
            return Task.Run(() => {
                return _UserMenuRepo.Filter(u => u.MenuID == null).Count();
            });
        }

        public string GetLastCode()
        {
            string code = "0";
            var lastCode = _UserMenuRepo.GetAll().OrderByDescending(b => b.ID).FirstOrDefault();
            if (lastCode.Code != null)
            {
                return lastCode.Code.ToString();
            }
            else
            {
                return code;
            }
        }

        /*********** Multi level menu ************/
        /// <summary>
        /// Get user menu items with multi levels
        /// </summary>
        /// <param name="lang">language that use use to return menu items related to it </param>
        /// <returns></returns>
        public List<UserMenuVM> GetUserMenu(int lang = 1)
        {
            List<UserMenu> UserMenuList = new List<UserMenu>();
            string UserId = GetUserId();
            string UserName = GetUserName();
            if (UserName != "" && !UserName.Contains("@"))
            {
                UserMenuList = _UserMenuRepo.Filter(x => x.Active == true).ToList();
            }
            else
            {
                var UserPrivilages = _userPriviliageRepo.Filter(p => p.UserID == UserId).Select(p => p.MenuID).ToList();
                var UserMenuItems = _UserMenuRepo.Filter(p => UserPrivilages.Contains(p.ID)).ToList();
                foreach (var Item in UserMenuItems)
                {
                    if (Item.Active == true)
                    {
                        UserMenuList.Add(Item);
                        if (Item.MenuID != null)
                        {
                            UserMenuList = GetMenuItemParent(UserMenuList, Item.MenuID);
                        }
                    }
                }
            }
            return GetAllMenuItemsWithLevels(UserMenuList, lang);
        }

        /// <summary>
        /// Get parent for each menu item and insert it in the returned menu
        /// </summary>
        /// <param name="userMenuList">final menu list that related to user </param>
        /// <param name="MenuId">menu item id </param>
        /// <returns></returns>
        private List<UserMenu> GetMenuItemParent(List<UserMenu> userMenuList, int? MenuId)
        {
            var MenuItem = _UserMenuRepo.GetByID(MenuId);
            if (MenuItem != null && MenuItem.Active == true && userMenuList.FirstOrDefault(p => p.ID == MenuItem.ID) == null)
            {
                userMenuList.Add(MenuItem);
                if (MenuItem.MenuID != null)
                {
                    userMenuList = GetMenuItemParent(userMenuList, MenuItem.MenuID);
                }
            }
            return userMenuList;
        }

        /// <summary>
        /// return menu as list with childs for each item and sub item and so on
        /// </summary>
        /// <param name="userMenuList"> menu list without levels </param>
        /// <param name="Lang"> user language </param>
        /// <returns></returns>
        private List<UserMenuVM> GetAllMenuItemsWithLevels(List<UserMenu> userMenuList, int Lang)
        {
            List<UserMenuVM> MenuList = new List<UserMenuVM>();
            if (userMenuList.Count() > 0)
            {
                foreach (var Item in userMenuList.Where(p => p.MenuID == null).ToList())
                {
                    var MenuItem = new UserMenuVM();
                    if (Lang == 1)
                    {
                        MenuItem.NAME = Item.ARName;
                    }
                    else
                    {
                        MenuItem.NAME = Item.LatName;
                    }
                    MenuItem.Active = Item.Active;
                    MenuItem.AddedBy = Item.AddedBy;
                    MenuItem.AddedOn = Item.AddedOn;
                    MenuItem.ARName = Item.ARName;
                    MenuItem.Code = Item.Code;
                    MenuItem.CountryID = Item.CountryID;
                    MenuItem.IconClass = Item.IconClass;
                    MenuItem.IconImageURL = Item.IconImageURL;
                    MenuItem.ID = Item.ID;
                    MenuItem.LatName = Item.LatName;
                    MenuItem.MenuClass = Item.MenuClass;
                    MenuItem.MenuID = Item.MenuID;
                    MenuItem.MenuLink = Item.MenuLink;
                    MenuItem.Notes = Item.Notes;
                    MenuItem.Position = Item.Position;
                    MenuItem.UpdatedBy = Item.UpdatedBy;
                    MenuItem.UpdatedOn = Item.UpdatedOn;
                    MenuItem.UserRoleID = Item.UserRoleID;
                    MenuItem.UserShortcut = Item.UserShortcut;
                    MenuItem.ChildMenus = GetMenuItemChilds(Item.ID, userMenuList, Lang);
                    MenuList.Add(MenuItem);
                }
            }
            return MenuList;
        }

        /// <summary>
        /// Get childs for each item in menu
        /// </summary>
        /// <param name="MenuId"> menu item id </param>
        /// <param name="userMenuList"> list that get item'childs from it </param>
        /// <param name="Lang"> user language </param>
        /// <returns></returns>
        private List<UserMenuVM> GetMenuItemChilds(int MenuId, List<UserMenu> userMenuList, int Lang)
        {
            List<UserMenuVM> MenuList = new List<UserMenuVM>();
            foreach (var Item in userMenuList.Where(p => p.MenuID == MenuId).ToList())
            {
                var MenuItem = new UserMenuVM();
                if (Lang == 1)
                {
                    MenuItem.NAME = Item.ARName;
                }
                else
                {
                    MenuItem.NAME = Item.LatName;
                }
                MenuItem.Active = Item.Active;
                MenuItem.AddedBy = Item.AddedBy;
                MenuItem.AddedOn = Item.AddedOn;
                MenuItem.ARName = Item.ARName;
                MenuItem.Code = Item.Code;
                MenuItem.CountryID = Item.CountryID;
                MenuItem.IconClass = Item.IconClass;
                MenuItem.IconImageURL = Item.IconImageURL;
                MenuItem.ID = Item.ID;
                MenuItem.LatName = Item.LatName;
                MenuItem.MenuClass = Item.MenuClass;
                MenuItem.MenuID = Item.MenuID;
                MenuItem.MenuLink = Item.MenuLink;
                MenuItem.Notes = Item.Notes;
                MenuItem.Position = Item.Position;
                MenuItem.UpdatedBy = Item.UpdatedBy;
                MenuItem.UpdatedOn = Item.UpdatedOn;
                MenuItem.UserRoleID = Item.UserRoleID;
                MenuItem.UserShortcut = Item.UserShortcut;
                MenuItem.ChildMenus = GetMenuItemChilds(Item.ID, userMenuList, Lang);
                MenuList.Add(MenuItem);
            }
            return MenuList;
        }
        /********************************************/
    }
}
