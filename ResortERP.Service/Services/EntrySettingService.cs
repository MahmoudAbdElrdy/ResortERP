using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ResortERP.Core.VM;
using ResortERP.Repository;
using ResortERP.Core;
using ResortERP.Service.IServices;

namespace ResortERP.Service.Services
{
    public class EntrySettingService : IEntrySettingService, IDisposable
    {
        IGenericRepository<ENTRY_SETTINGS> entrySettingRepo;
        IGenericRepository<ENTRY_GRID_COLUMNS> entryGrdColRepo;
        IGenericRepository<UserMenu> userMenuRepo;
        public EntrySettingService(IGenericRepository<ENTRY_SETTINGS> entrySettingRepo, IGenericRepository<ENTRY_GRID_COLUMNS> entryGrdColRepo, IGenericRepository<UserMenu> userMenuRepo)
        {
            this.entrySettingRepo = entrySettingRepo;
            this.entryGrdColRepo = entryGrdColRepo;
            this.userMenuRepo = userMenuRepo;
        }

        public Task<int> CountAsync()
        {
            return Task.Run<int>(() =>
            {
                return entrySettingRepo.CountAsync();
            });
        }

        public bool Delete(Entry_SettingsVM entity)
        {
            ENTRY_SETTINGS et = new ENTRY_SETTINGS
            {
                ENTRY_SETTING_ID = entity.ENTRY_SETTING_ID,
                ACCEPT_DIST_ACC = entity.ACCEPT_DIST_ACC,
                AUTO_POSTING = entity.AUTO_POSTING,
                COSTCENTER_BELONG = entity.COSTCENTER_BELONG,
                CURRENCY_ID = entity.CURRENCY_ID,
                ENTRY_ACC_ID = entity.ENTRY_ACC_ID,
                ENTRY_SETTING_AR_ABRV = entity.ENTRY_SETTING_AR_ABRV,
                ENTRY_SETTING_AR_NAME = entity.ENTRY_SETTING_AR_NAME,
                ENTRY_SETTING_EN_ABRV = entity.ENTRY_SETTING_EN_ABRV,
                ENTRY_SETTING_EN_NAME = entity.ENTRY_SETTING_EN_NAME,
                ENTRY_TYPE_ID = entity.ENTRY_TYPE_ID,
                AddedBy = entity.AddedBy,
                AddedOn = entity.AddedOn,
                UpdatedBy = entity.UpdatedBy,
                updatedOn = entity.updatedOn,
                TaxAccountID = entity.TaxAccountID,
                MenuID = entity.MenuID,
                IsTaxable = entity.IsTaxable,
                TaxRate = entity.TaxRate,
                IsEnableTaxEdit = entity.IsEnableTaxEdit,

                TimesNumberOfPrinting = entity.TimesNumberOfPrinting,
                EntryModeID = entity.EntryModeID,
                IsTaxesIncluded = entity.IsTaxesIncluded,

                IsRepeatItem = entity.IsRepeatItem,
                IsQuickAccount = entity.IsQuickAccount,

                ExemptVatRate=entity.ExemptVatRate,
                MainVatRate=entity.MainVatRate,
                IsShowTaxesBox=entity.IsShowTaxesBox,
                ShowEntryTotalsSammaryAsTable = entity.ShowEntryTotalsSammaryAsTable,
                IsTaxAccForAccount= entity.IsTaxAccForAccount
            };
            entrySettingRepo.Delete(et, entity.ENTRY_SETTING_ID);
            return true;
        }

        public Task<bool> DeleteAsync(Entry_SettingsVM entity)
        {
            return Task.Run<bool>(() =>
            {
                ENTRY_GRID_COLUMNS egc = entryGrdColRepo.Filter(X => X.ENTRY_SETTING_ID == entity.ENTRY_SETTING_ID).FirstOrDefault();
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
                    entryGrdColRepo.Delete(egc_, egc.ENTRY_SETTING_ID);
                }



                UserMenu menu = userMenuRepo.Filter(u => u.EntrySettingID == entity.ENTRY_SETTING_ID).FirstOrDefault();
                if (menu != null)
                {
                    UserMenu userMenu = new UserMenu();
                    {
                        userMenu.ID = menu.ID;
                        userMenu.Code = menu.Code;
                        userMenu.ARName = menu.ARName;
                        userMenu.LatName = menu.LatName;
                        userMenu.MenuLink = menu.MenuLink;
                        userMenu.MenuClass = menu.MenuClass;
                        userMenu.IconClass = menu.IconClass;
                        userMenu.IconImageURL = menu.IconImageURL;
                        userMenu.MenuID = menu.MenuID;
                        userMenu.DisplayOrNot = menu.DisplayOrNot;
                        userMenu.UserShortcut = menu.UserShortcut;
                        userMenu.UserRoleID = menu.UserRoleID;
                        userMenu.MenuType = menu.MenuType;
                        userMenu.LanguageID = menu.LanguageID;
                        userMenu.ResourceURL = menu.ResourceURL;
                        userMenu.ResourceContent = menu.ResourceContent;
                        userMenu.Notes = menu.Notes;
                        userMenu.AddedBy = menu.AddedBy;
                        userMenu.AddedOn = menu.AddedOn;
                        userMenu.UpdatedBy = menu.UpdatedBy;
                        userMenu.UpdatedOn = menu.UpdatedOn;
                        userMenu.Active = menu.Active;
                        userMenu.Position = menu.Position;
                        userMenu.CountryID = menu.CountryID;
                        userMenu.FRName = menu.FRName;
                        userMenu.URName = menu.URName;
                        userMenu.TRName = menu.TRName;
                        userMenu.BillSetiingID = menu.BillSetiingID;
                        userMenu.EntrySettingID = menu.EntrySettingID;
                    };
                    userMenuRepo.Delete(userMenu, menu.ID);
                }


                ENTRY_SETTINGS et = new ENTRY_SETTINGS
                {
                    ENTRY_SETTING_ID = entity.ENTRY_SETTING_ID,
                    ACCEPT_DIST_ACC = entity.ACCEPT_DIST_ACC,
                    AUTO_POSTING = entity.AUTO_POSTING,
                    COSTCENTER_BELONG = entity.COSTCENTER_BELONG,
                    CURRENCY_ID = entity.CURRENCY_ID,
                    ENTRY_ACC_ID = entity.ENTRY_ACC_ID,
                    ENTRY_SETTING_AR_ABRV = entity.ENTRY_SETTING_AR_ABRV,
                    ENTRY_SETTING_AR_NAME = entity.ENTRY_SETTING_AR_NAME,
                    ENTRY_SETTING_EN_ABRV = entity.ENTRY_SETTING_EN_ABRV,
                    ENTRY_SETTING_EN_NAME = entity.ENTRY_SETTING_EN_NAME,
                    ENTRY_TYPE_ID = entity.ENTRY_TYPE_ID,
                    AddedBy = entity.AddedBy,
                    AddedOn = entity.AddedOn,
                    UpdatedBy = entity.UpdatedBy,
                    updatedOn = entity.updatedOn,
                    TaxAccountID = entity.TaxAccountID,
                    MenuID = entity.MenuID,
                    IsTaxable = entity.IsTaxable,
                    TaxRate = entity.TaxRate,
                    IsEnableTaxEdit = entity.IsEnableTaxEdit,

                    TimesNumberOfPrinting = entity.TimesNumberOfPrinting,
                    EntryModeID = entity.EntryModeID,
                    IsTaxesIncluded = entity.IsTaxesIncluded,
                    IsRepeatItem = entity.IsRepeatItem,
                    IsQuickAccount = entity.IsQuickAccount,

                    ExemptVatRate = entity.ExemptVatRate,
                    MainVatRate = entity.MainVatRate,
                    IsShowTaxesBox = entity.IsShowTaxesBox,
                    IsTaxAccForAccount = entity.IsTaxAccForAccount

                };
                entrySettingRepo.Delete(et, entity.ENTRY_SETTING_ID);
                return true;
            });
        }

        public void Dispose()
        {
            entrySettingRepo.Dispose();
            entryGrdColRepo.Dispose();
        }


        public Task<Entry_SettingsVM> GetEntrySettingByID(int typeID)
        {
            return Task.Run<Entry_SettingsVM>(() =>
            {
                return entrySettingRepo.Filter(X => (X.ENTRY_TYPE_ID == typeID)).Select(entity => new Entry_SettingsVM()
                {
                    ENTRY_SETTING_ID = entity.ENTRY_SETTING_ID,
                    ACCEPT_DIST_ACC = entity.ACCEPT_DIST_ACC,
                    AUTO_POSTING = entity.AUTO_POSTING,
                    COSTCENTER_BELONG = entity.COSTCENTER_BELONG,
                    CURRENCY_ID = entity.CURRENCY_ID,
                    ENTRY_ACC_ID = entity.ENTRY_ACC_ID,
                    ENTRY_SETTING_AR_ABRV = entity.ENTRY_SETTING_AR_ABRV,
                    ENTRY_SETTING_AR_NAME = entity.ENTRY_SETTING_AR_NAME,
                    ENTRY_SETTING_EN_ABRV = entity.ENTRY_SETTING_EN_ABRV,
                    ENTRY_SETTING_EN_NAME = entity.ENTRY_SETTING_EN_NAME,
                    ENTRY_TYPE_ID = entity.ENTRY_TYPE_ID,
                    AddedBy = entity.AddedBy,
                    AddedOn = entity.AddedOn,
                    UpdatedBy = entity.UpdatedBy,
                    updatedOn = entity.updatedOn,
                    TaxAccountID = entity.TaxAccountID,
                    MenuID = entity.MenuID,
                    IsTaxable = entity.IsTaxable,
                    TaxRate = entity.TaxRate,
                    IsEnableTaxEdit = entity.IsEnableTaxEdit,

                    TimesNumberOfPrinting = entity.TimesNumberOfPrinting,
                    EntryModeID = entity.EntryModeID,
                    IsTaxesIncluded = entity.IsTaxesIncluded,
                    IsRepeatItem = entity.IsRepeatItem,
                    IsQuickAccount = entity.IsQuickAccount,
                    ShowEntryTotalsSammaryAsTable = entity.ShowEntryTotalsSammaryAsTable,

                    ExemptVatRate = entity.ExemptVatRate,
                    MainVatRate = entity.MainVatRate,
                    IsShowTaxesBox = entity.IsShowTaxesBox,
                    IsTaxAccForAccount = entity.IsTaxAccForAccount

                }).ToList().FirstOrDefault();
            });
        }


        public Task<List<Entry_SettingsVM>> GetAllAsync(int pageNum, int pageSize)
        {
            return Task.Run<List<Entry_SettingsVM>>(() =>
            {
                int rowCount;
                var q = from entity in entrySettingRepo.GetPaged<long>(pageNum, pageSize, p => p.ENTRY_SETTING_ID, false, out rowCount)
                        select new Entry_SettingsVM
                        {
                            ENTRY_SETTING_ID = entity.ENTRY_SETTING_ID,
                            ACCEPT_DIST_ACC = entity.ACCEPT_DIST_ACC,
                            AUTO_POSTING = entity.AUTO_POSTING,
                            COSTCENTER_BELONG = entity.COSTCENTER_BELONG,
                            CURRENCY_ID = entity.CURRENCY_ID,
                            ENTRY_ACC_ID = entity.ENTRY_ACC_ID,
                            ENTRY_SETTING_AR_ABRV = entity.ENTRY_SETTING_AR_ABRV,
                            ENTRY_SETTING_AR_NAME = entity.ENTRY_SETTING_AR_NAME,
                            ENTRY_SETTING_EN_ABRV = entity.ENTRY_SETTING_EN_ABRV,
                            ENTRY_SETTING_EN_NAME = entity.ENTRY_SETTING_EN_NAME,
                            ENTRY_TYPE_ID = entity.ENTRY_TYPE_ID,
                            AddedBy = entity.AddedBy,
                            AddedOn = entity.AddedOn,
                            UpdatedBy = entity.UpdatedBy,
                            updatedOn = entity.updatedOn,
                            TaxAccountID = entity.TaxAccountID,
                            MenuID = entity.MenuID,
                            IsTaxable = entity.IsTaxable,
                            TaxRate = entity.TaxRate,
                            IsEnableTaxEdit = entity.IsEnableTaxEdit,

                            TimesNumberOfPrinting = entity.TimesNumberOfPrinting,
                            EntryModeID = entity.EntryModeID,
                            IsTaxesIncluded = entity.IsTaxesIncluded,

                            IsRepeatItem = entity.IsRepeatItem,
                            IsQuickAccount = entity.IsQuickAccount,
                            ShowEntryTotalsSammaryAsTable = entity.ShowEntryTotalsSammaryAsTable,

                            ExemptVatRate = entity.ExemptVatRate,
                            MainVatRate = entity.MainVatRate,
                            IsShowTaxesBox = entity.IsShowTaxesBox,
                            IsTaxAccForAccount = entity.IsTaxAccForAccount
                        };
                return q.ToList();
            });
        }

        public List<Entry_SettingsVM> GetAll()
        {
            var q = from entity in entrySettingRepo.GetAll()
                    select new Entry_SettingsVM
                    {
                        ENTRY_SETTING_ID = entity.ENTRY_SETTING_ID,
                        ACCEPT_DIST_ACC = entity.ACCEPT_DIST_ACC,
                        AUTO_POSTING = entity.AUTO_POSTING,
                        COSTCENTER_BELONG = entity.COSTCENTER_BELONG,
                        CURRENCY_ID = entity.CURRENCY_ID,
                        ENTRY_ACC_ID = entity.ENTRY_ACC_ID,
                        ENTRY_SETTING_AR_ABRV = entity.ENTRY_SETTING_AR_ABRV,
                        ENTRY_SETTING_AR_NAME = entity.ENTRY_SETTING_AR_NAME,
                        ENTRY_SETTING_EN_ABRV = entity.ENTRY_SETTING_EN_ABRV,
                        ENTRY_SETTING_EN_NAME = entity.ENTRY_SETTING_EN_NAME,
                        ENTRY_TYPE_ID = entity.ENTRY_TYPE_ID,
                        AddedBy = entity.AddedBy,
                        AddedOn = entity.AddedOn,
                        UpdatedBy = entity.UpdatedBy,
                        updatedOn = entity.updatedOn,
                        TaxAccountID = entity.TaxAccountID,
                        MenuID = entity.MenuID,
                        IsTaxable = entity.IsTaxable,
                        TaxRate = entity.TaxRate,
                        IsEnableTaxEdit = entity.IsEnableTaxEdit,

                        TimesNumberOfPrinting = entity.TimesNumberOfPrinting,
                        EntryModeID = entity.EntryModeID,
                        IsTaxesIncluded = entity.IsTaxesIncluded,
                        IsRepeatItem = entity.IsRepeatItem,
                        IsQuickAccount = entity.IsQuickAccount,
                        ShowEntryTotalsSammaryAsTable = entity.ShowEntryTotalsSammaryAsTable,

                        ExemptVatRate = entity.ExemptVatRate,
                        MainVatRate = entity.MainVatRate,
                        IsShowTaxesBox = entity.IsShowTaxesBox,
                        IsTaxAccForAccount = entity.IsTaxAccForAccount
                    };
            return q.ToList();
        }

        public bool Insert(Entry_SettingsVM entity)
        {
            ENTRY_SETTINGS et = new ENTRY_SETTINGS
            {
                ENTRY_SETTING_ID = entity.ENTRY_SETTING_ID,
                ACCEPT_DIST_ACC = entity.ACCEPT_DIST_ACC,
                AUTO_POSTING = entity.AUTO_POSTING,
                COSTCENTER_BELONG = entity.COSTCENTER_BELONG,
                CURRENCY_ID = entity.CURRENCY_ID,
                ENTRY_ACC_ID = entity.ENTRY_ACC_ID,
                ENTRY_SETTING_AR_ABRV = entity.ENTRY_SETTING_AR_ABRV,
                ENTRY_SETTING_AR_NAME = entity.ENTRY_SETTING_AR_NAME,
                ENTRY_SETTING_EN_ABRV = entity.ENTRY_SETTING_EN_ABRV,
                ENTRY_SETTING_EN_NAME = entity.ENTRY_SETTING_EN_NAME,
                ENTRY_TYPE_ID = entity.ENTRY_TYPE_ID,
                AddedBy = entity.AddedBy,
                AddedOn = entity.AddedOn,
                UpdatedBy = entity.UpdatedBy,
                updatedOn = entity.updatedOn,
                TaxAccountID = entity.TaxAccountID,
                MenuID = entity.MenuID,
                IsTaxable = entity.IsTaxable,
                TaxRate = entity.TaxRate,
                IsEnableTaxEdit = entity.IsEnableTaxEdit,

                TimesNumberOfPrinting = entity.TimesNumberOfPrinting,
                EntryModeID = entity.EntryModeID,
                IsTaxesIncluded = entity.IsTaxesIncluded,
                IsRepeatItem = entity.IsRepeatItem,
                IsQuickAccount = entity.IsQuickAccount,
                ShowEntryTotalsSammaryAsTable = entity.ShowEntryTotalsSammaryAsTable,

                ExemptVatRate = entity.ExemptVatRate,
                MainVatRate = entity.MainVatRate,
                IsShowTaxesBox = entity.IsShowTaxesBox,
                IsTaxAccForAccount = entity.IsTaxAccForAccount
            };
            entrySettingRepo.Add(et);
            return true;
        }

        public Task<int> InsertAsync(Entry_SettingsVM entity)
        {
            return Task.Run<int>(() =>
            {
                ENTRY_SETTINGS et = new ENTRY_SETTINGS
                {
                    ENTRY_SETTING_ID = entity.ENTRY_SETTING_ID,
                    ACCEPT_DIST_ACC = entity.ACCEPT_DIST_ACC,
                    AUTO_POSTING = entity.AUTO_POSTING,
                    COSTCENTER_BELONG = entity.COSTCENTER_BELONG,
                    CURRENCY_ID = entity.CURRENCY_ID,
                    ENTRY_ACC_ID = entity.ENTRY_ACC_ID,
                    ENTRY_SETTING_AR_ABRV = entity.ENTRY_SETTING_AR_ABRV,
                    ENTRY_SETTING_AR_NAME = entity.ENTRY_SETTING_AR_NAME,
                    ENTRY_SETTING_EN_ABRV = entity.ENTRY_SETTING_EN_ABRV,
                    ENTRY_SETTING_EN_NAME = entity.ENTRY_SETTING_EN_NAME,
                    ENTRY_TYPE_ID = entity.ENTRY_TYPE_ID,
                    AddedBy = entity.AddedBy,
                    AddedOn = entity.AddedOn,
                    UpdatedBy = entity.UpdatedBy,
                    updatedOn = entity.updatedOn,
                    TaxAccountID = entity.TaxAccountID,
                    MenuID = entity.MenuID,
                    IsTaxable = entity.IsTaxable,
                    TaxRate = entity.TaxRate,
                    IsEnableTaxEdit = entity.IsEnableTaxEdit,

                    TimesNumberOfPrinting = entity.TimesNumberOfPrinting,
                    EntryModeID = entity.EntryModeID,
                    IsTaxesIncluded = entity.IsTaxesIncluded,
                    IsRepeatItem = entity.IsRepeatItem,
                    IsQuickAccount = entity.IsQuickAccount,
                    ShowEntryTotalsSammaryAsTable = entity.ShowEntryTotalsSammaryAsTable,

                    ExemptVatRate = entity.ExemptVatRate,
                    MainVatRate = entity.MainVatRate,
                    IsShowTaxesBox = entity.IsShowTaxesBox,
                    IsTaxAccForAccount = entity.IsTaxAccForAccount
                };
                entrySettingRepo.Add(et);
                return et.ENTRY_SETTING_ID;
            });
        }

        public bool Update(Entry_SettingsVM entity)
        {
            ENTRY_SETTINGS et = new ENTRY_SETTINGS
            {
                ENTRY_SETTING_ID = entity.ENTRY_SETTING_ID,
                ACCEPT_DIST_ACC = entity.ACCEPT_DIST_ACC,
                AUTO_POSTING = entity.AUTO_POSTING,
                COSTCENTER_BELONG = entity.COSTCENTER_BELONG,
                CURRENCY_ID = entity.CURRENCY_ID,
                ENTRY_ACC_ID = entity.ENTRY_ACC_ID,
                ENTRY_SETTING_AR_ABRV = entity.ENTRY_SETTING_AR_ABRV,
                ENTRY_SETTING_AR_NAME = entity.ENTRY_SETTING_AR_NAME,
                ENTRY_SETTING_EN_ABRV = entity.ENTRY_SETTING_EN_ABRV,
                ENTRY_SETTING_EN_NAME = entity.ENTRY_SETTING_EN_NAME,
                ENTRY_TYPE_ID = entity.ENTRY_TYPE_ID,
                AddedBy = entity.AddedBy,
                AddedOn = entity.AddedOn,
                UpdatedBy = entity.UpdatedBy,
                updatedOn = entity.updatedOn,
                TaxAccountID = entity.TaxAccountID,
                MenuID = entity.MenuID,
                IsTaxable = entity.IsTaxable,
                TaxRate = entity.TaxRate,
                IsEnableTaxEdit = entity.IsEnableTaxEdit,

                TimesNumberOfPrinting = entity.TimesNumberOfPrinting,
                EntryModeID = entity.EntryModeID,
                IsTaxesIncluded = entity.IsTaxesIncluded,
                IsRepeatItem = entity.IsRepeatItem,
                IsQuickAccount = entity.IsQuickAccount,
                ShowEntryTotalsSammaryAsTable = entity.ShowEntryTotalsSammaryAsTable,

                ExemptVatRate = entity.ExemptVatRate,
                MainVatRate = entity.MainVatRate,
                IsShowTaxesBox = entity.IsShowTaxesBox,
                IsTaxAccForAccount = entity.IsTaxAccForAccount
            };
            entrySettingRepo.Update(et, et.ENTRY_SETTING_ID);
            return true;
        }

        public Task<bool> UpdateAsync(Entry_SettingsVM entity)
        {
            return Task.Run<bool>(() =>
            {
                ENTRY_SETTINGS et = new ENTRY_SETTINGS
                {
                    ENTRY_SETTING_ID = entity.ENTRY_SETTING_ID,
                    ACCEPT_DIST_ACC = entity.ACCEPT_DIST_ACC,
                    AUTO_POSTING = entity.AUTO_POSTING,
                    COSTCENTER_BELONG = entity.COSTCENTER_BELONG,
                    CURRENCY_ID = entity.CURRENCY_ID,
                    ENTRY_ACC_ID = entity.ENTRY_ACC_ID,
                    ENTRY_SETTING_AR_ABRV = entity.ENTRY_SETTING_AR_ABRV,
                    ENTRY_SETTING_AR_NAME = entity.ENTRY_SETTING_AR_NAME,
                    ENTRY_SETTING_EN_ABRV = entity.ENTRY_SETTING_EN_ABRV,
                    ENTRY_SETTING_EN_NAME = entity.ENTRY_SETTING_EN_NAME,
                    ENTRY_TYPE_ID = entity.ENTRY_TYPE_ID,
                    AddedBy = entity.AddedBy,
                    AddedOn = entity.AddedOn,
                    UpdatedBy = entity.UpdatedBy,
                    updatedOn = entity.updatedOn,
                    TaxAccountID = entity.TaxAccountID,
                    MenuID = entity.MenuID,
                    IsTaxable = entity.IsTaxable,
                    TaxRate = entity.TaxRate,
                    IsEnableTaxEdit = entity.IsEnableTaxEdit,

                    TimesNumberOfPrinting = entity.TimesNumberOfPrinting,
                    EntryModeID = entity.EntryModeID,
                    IsTaxesIncluded = entity.IsTaxesIncluded,
                    IsRepeatItem = entity.IsRepeatItem,
                    IsQuickAccount = entity.IsQuickAccount,
                    ShowEntryTotalsSammaryAsTable = entity.ShowEntryTotalsSammaryAsTable,

                    ExemptVatRate = entity.ExemptVatRate,
                    MainVatRate = entity.MainVatRate,
                    IsShowTaxesBox = entity.IsShowTaxesBox,
                    IsTaxAccForAccount = entity.IsTaxAccForAccount
                };
                entrySettingRepo.Update(et, et.ENTRY_SETTING_ID);
                return true;
            });
        }


        public Entry_SettingsVM GetEntrySettingBySettingID(int settingID)
        {
            var q = from entity in entrySettingRepo.GetAll().Where(s => s.ENTRY_SETTING_ID == settingID)
                    select new Entry_SettingsVM
                    {
                        ENTRY_SETTING_ID = entity.ENTRY_SETTING_ID,
                        ACCEPT_DIST_ACC = entity.ACCEPT_DIST_ACC,
                        AUTO_POSTING = entity.AUTO_POSTING,
                        COSTCENTER_BELONG = entity.COSTCENTER_BELONG,
                        CURRENCY_ID = entity.CURRENCY_ID,
                        ENTRY_ACC_ID = entity.ENTRY_ACC_ID,
                        ENTRY_SETTING_AR_ABRV = entity.ENTRY_SETTING_AR_ABRV,
                        ENTRY_SETTING_AR_NAME = entity.ENTRY_SETTING_AR_NAME,
                        ENTRY_SETTING_EN_ABRV = entity.ENTRY_SETTING_EN_ABRV,
                        ENTRY_SETTING_EN_NAME = entity.ENTRY_SETTING_EN_NAME,
                        ENTRY_TYPE_ID = entity.ENTRY_TYPE_ID,
                        AddedBy = entity.AddedBy,
                        AddedOn = entity.AddedOn,
                        UpdatedBy = entity.UpdatedBy,
                        updatedOn = entity.updatedOn,
                        TaxAccountID = entity.TaxAccountID,
                        MenuID = entity.MenuID,
                        IsTaxable = entity.IsTaxable,
                        TaxRate = entity.TaxRate,
                        IsEnableTaxEdit = entity.IsEnableTaxEdit,

                        TimesNumberOfPrinting = entity.TimesNumberOfPrinting,
                        EntryModeID = entity.EntryModeID,
                        IsTaxesIncluded = entity.IsTaxesIncluded,

                        IsRepeatItem = entity.IsRepeatItem,
                        IsQuickAccount = entity.IsQuickAccount,
                        ShowEntryTotalsSammaryAsTable = entity.ShowEntryTotalsSammaryAsTable,

                        ExemptVatRate = entity.ExemptVatRate,
                        MainVatRate = entity.MainVatRate,
                        IsShowTaxesBox = entity.IsShowTaxesBox,
                        IsTaxAccForAccount = entity.IsTaxAccForAccount
                    };
            return q.FirstOrDefault();
        }



        public int GetEntryTypeBySettingID(int settingID)
        {
            int type = entrySettingRepo.GetAll().Where(e => e.ENTRY_SETTING_ID == settingID).Select(e => e.ENTRY_TYPE_ID).FirstOrDefault();
            return type;
        }
    }
}
