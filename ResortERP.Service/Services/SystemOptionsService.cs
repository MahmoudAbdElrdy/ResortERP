using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ResortERP.Core.VM;
using ResortERP.Repository;
using ResortERP.Core;
using ResortERP.Service.IServices;
using System.Data.SqlClient;

namespace ResortERP.Service.Services
{
    public class SystemOptionsService : ISystemOptionsService, IDisposable
    {
        IGenericRepository<SYSTEM_OPTIONS> sysOptRepo;
        IGenericRepository<User> userRepo;
        ICommonRepository<User> commonRepo;
        IGenericRepository<Emails> emailRepo;
        IGenericRepository<SHORTCUTS> shortCutRepo;
        IGenericRepository<TBOXACCOUNTS> tBoxRepo;
        IGenericRepository<TSTORE> tStoreRepo;
        IGenericRepository<Income_Account_Types> incomeAccTypeRepo;
        IGenericRepository<KEST_OPTION> kestOptRepo;
        IGenericRepository<TBUDGETACCOUNTS> tBudRepo;
        IGenericRepository<SELLS_TYPES> sellTypeRepo;
        IGenericRepository<AddressPartsSettings> addPartsRepo;
         public SystemOptionsService(IGenericRepository<SYSTEM_OPTIONS> sysOptRepo, IGenericRepository<User> userRepo, ICommonRepository<User> commonRepo, IGenericRepository<Emails> emailRepo, IGenericRepository<SHORTCUTS> shortCutRepo, IGenericRepository<TBOXACCOUNTS> tBoxRepo, IGenericRepository<TSTORE> tStoreRepo, IGenericRepository<Income_Account_Types> incomeAccTypeRepo, IGenericRepository<KEST_OPTION> kestOptRepo, IGenericRepository<TBUDGETACCOUNTS> tBudRepo, IGenericRepository<SELLS_TYPES> sellTypeRepo, IGenericRepository<AddressPartsSettings> addPartsRepo)
        {
            this.sysOptRepo = sysOptRepo;
            this.userRepo = userRepo;
            this.commonRepo = commonRepo;
            this.emailRepo = emailRepo;
            this.shortCutRepo = shortCutRepo;
            this.tBoxRepo = tBoxRepo;
            this.tStoreRepo = tStoreRepo;
            this.incomeAccTypeRepo = incomeAccTypeRepo;
            this.kestOptRepo = kestOptRepo;
            this.tBudRepo = tBudRepo;
            this.sellTypeRepo = sellTypeRepo;
            this.addPartsRepo = addPartsRepo;
        }
        public Task<int> CountAsync()
        {
            return Task.Run<int>(() =>
            {
                return sysOptRepo.CountAsync();
            });
        }

        
        public bool Delete(System_OptionsVM entity)
        {
            SYSTEM_OPTIONS bt = new SYSTEM_OPTIONS
            {
                A4BOTTOMMARGIN = entity.A4BOTTOMMARGIN,
                A4LEFTMARGIN = entity.A4LEFTMARGIN,
                A4PRINTER_ID = entity.A4PRINTER_ID,
                A4RIGHTMARGIN = entity.A4RIGHTMARGIN,
                A4TOPMARGIN = entity.A4TOPMARGIN,
                BILL_CODE = entity.BILL_CODE,
                CAN_CHANGE_BILL_DATE = entity.CAN_CHANGE_BILL_DATE,
                CAN_CHANGE_THE_PRICE_ON_THE_BILL = entity.CAN_CHANGE_THE_PRICE_ON_THE_BILL,
                CAN_SEE_THE_BILL_PROFIT = entity.CAN_SEE_THE_BILL_PROFIT,
                CAN_SEE_THE_CHEQUES_NOTIFICATIONS = entity.CAN_SEE_THE_CHEQUES_NOTIFICATIONS,
                CAN_SEE_THE_CURRENT_QTY_ON_BILL = entity.CAN_SEE_THE_CURRENT_QTY_ON_BILL,
                CAN_SEE_THE_PRICE = entity.CAN_SEE_THE_PRICE,
                CURRENCYDIFFERENCEACCOUNTID = entity.CURRENCYDIFFERENCEACCOUNTID,
                DATE_AFTER = entity.DATE_AFTER,
                DEFAULT_CURRENCY = entity.DEFAULT_CURRENCY,
                DISABLE_BILL_PAY_TYPE = entity.DISABLE_BILL_PAY_TYPE,
                END_DATE = entity.END_DATE,
                END_SYSTEM = entity.END_SYSTEM,
                ENTRY_CODE = entity.ENTRY_CODE,
                FIRST_DATE = entity.FIRST_DATE,
                GENERATE_THE_CURRENCY_DIFFERENCE_ENTRY_FOR_BILLS = entity.GENERATE_THE_CURRENCY_DIFFERENCE_ENTRY_FOR_BILLS,
                GENERATE_THE_CURRENCY_DIFFERENCE_ENTRY_FOR_ENTRIES = entity.GENERATE_THE_CURRENCY_DIFFERENCE_ENTRY_FOR_ENTRIES,
                HOW_MANY_BILL_CAN_NAVIGATE = entity.HOW_MANY_BILL_CAN_NAVIGATE,
                ISITORDEREDBYITEMCODEORNOT = entity.ISITORDEREDBYITEMCODEORNOT,
                ITEM_CODE = entity.ITEM_CODE,
                ITEM_GROUP_CODE = entity.ITEM_GROUP_CODE,
                LOAD_THE_LAST_BILLS_AFTER_SAVE = entity.LOAD_THE_LAST_BILLS_AFTER_SAVE,
                LOSTACCOUNTID = entity.LOSTACCOUNTID,
                NUMBEROFDAYSBEFOREDUEDATE = entity.NUMBEROFDAYSBEFOREDUEDATE,
                ON_ASK_ABOUT_CHANGE = entity.ON_ASK_ABOUT_CHANGE,
                ON_DELETE = entity.ON_DELETE,
                ON_POST_ACC = entity.ON_POST_ACC,
                ON_POST_ENTRY = entity.ON_POST_ENTRY,
                ON_POST_STORE = entity.ON_POST_STORE,
                ON_SAVE = entity.ON_SAVE,
                ON_UPDATE = entity.ON_UPDATE,
                OPERATION_DATE = entity.OPERATION_DATE,
                PRICE = entity.PRICE,
                PRINTER_ID = entity.PRINTER_ID,
                QTY = entity.QTY,
                RATE = entity.RATE,
                RESETBOTTOMMARGIN = entity.RESETBOTTOMMARGIN,
                RESETLEFTMARGIN = entity.RESETLEFTMARGIN,
                RESETPRINTER_ID = entity.RESETPRINTER_ID,
                RESETRIGHTMARGIN = entity.RESETRIGHTMARGIN,
                RESETTOPMARGIN = entity.RESETTOPMARGIN,
                SAME_ACC_ON_ENTRY = entity.SAME_ACC_ON_ENTRY,
                SAME_ITEM_ON_BILL = entity.SAME_ITEM_ON_BILL,
                SERVICE_CODE = entity.SERVICE_CODE,
                SERVICE_GROUP_CODE = entity.SERVICE_GROUP_CODE,
                SHOW_AUTO_DISCOUNT = entity.SHOW_AUTO_DISCOUNT,
                SHOW_BILL_PAY_TYPE = entity.SHOW_BILL_PAY_TYPE,
                SHOW_IS_IT_RESERVATION = entity.SHOW_IS_IT_RESERVATION,
                SUPPLIER_ITEM_CODE = entity.SUPPLIER_ITEM_CODE,
                UID = entity.UID,
                UNPOSTED_BILL = entity.UNPOSTED_BILL,
                AddedBy = entity.AddedBy,
                AddedOn = entity.AddedOn,
                UpdatedBy = entity.UpdatedBy,
                updatedOn = entity.updatedOn,
                IsShowPrices = entity.IsShowPrices,
                ReconstructionOfAssets = entity.ReconstructionOfAssets,
                DisplayPaym = entity.DisplayPaym,
                ApplyDueValueDate = entity.ApplyDueValueDate,
                IncludePrevYear = entity.IncludePrevYear,
                HighlightGenActs = entity.HighlightGenActs,
            };
            sysOptRepo.Delete(bt, entity.UID);
            return true;
        }

        public Task<bool> DeleteAsync(System_OptionsVM entity)
        {
            return Task.Run<bool>(() =>
            {
                SYSTEM_OPTIONS bt = new SYSTEM_OPTIONS
                {
                    A4BOTTOMMARGIN = entity.A4BOTTOMMARGIN,
                    A4LEFTMARGIN = entity.A4LEFTMARGIN,
                    A4PRINTER_ID = entity.A4PRINTER_ID,
                    A4RIGHTMARGIN = entity.A4RIGHTMARGIN,
                    A4TOPMARGIN = entity.A4TOPMARGIN,
                    BILL_CODE = entity.BILL_CODE,
                    CAN_CHANGE_BILL_DATE = entity.CAN_CHANGE_BILL_DATE,
                    CAN_CHANGE_THE_PRICE_ON_THE_BILL = entity.CAN_CHANGE_THE_PRICE_ON_THE_BILL,
                    CAN_SEE_THE_BILL_PROFIT = entity.CAN_SEE_THE_BILL_PROFIT,
                    CAN_SEE_THE_CHEQUES_NOTIFICATIONS = entity.CAN_SEE_THE_CHEQUES_NOTIFICATIONS,
                    CAN_SEE_THE_CURRENT_QTY_ON_BILL = entity.CAN_SEE_THE_CURRENT_QTY_ON_BILL,
                    CAN_SEE_THE_PRICE = entity.CAN_SEE_THE_PRICE,
                    CURRENCYDIFFERENCEACCOUNTID = entity.CURRENCYDIFFERENCEACCOUNTID,
                    DATE_AFTER = entity.DATE_AFTER,
                    DEFAULT_CURRENCY = entity.DEFAULT_CURRENCY,
                    DISABLE_BILL_PAY_TYPE = entity.DISABLE_BILL_PAY_TYPE,
                    END_DATE = entity.END_DATE,
                    END_SYSTEM = entity.END_SYSTEM,
                    ENTRY_CODE = entity.ENTRY_CODE,
                    FIRST_DATE = entity.FIRST_DATE,
                    GENERATE_THE_CURRENCY_DIFFERENCE_ENTRY_FOR_BILLS = entity.GENERATE_THE_CURRENCY_DIFFERENCE_ENTRY_FOR_BILLS,
                    GENERATE_THE_CURRENCY_DIFFERENCE_ENTRY_FOR_ENTRIES = entity.GENERATE_THE_CURRENCY_DIFFERENCE_ENTRY_FOR_ENTRIES,
                    HOW_MANY_BILL_CAN_NAVIGATE = entity.HOW_MANY_BILL_CAN_NAVIGATE,
                    ISITORDEREDBYITEMCODEORNOT = entity.ISITORDEREDBYITEMCODEORNOT,
                    ITEM_CODE = entity.ITEM_CODE,
                    ITEM_GROUP_CODE = entity.ITEM_GROUP_CODE,
                    LOAD_THE_LAST_BILLS_AFTER_SAVE = entity.LOAD_THE_LAST_BILLS_AFTER_SAVE,
                    LOSTACCOUNTID = entity.LOSTACCOUNTID,
                    NUMBEROFDAYSBEFOREDUEDATE = entity.NUMBEROFDAYSBEFOREDUEDATE,
                    ON_ASK_ABOUT_CHANGE = entity.ON_ASK_ABOUT_CHANGE,
                    ON_DELETE = entity.ON_DELETE,
                    ON_POST_ACC = entity.ON_POST_ACC,
                    ON_POST_ENTRY = entity.ON_POST_ENTRY,
                    ON_POST_STORE = entity.ON_POST_STORE,
                    ON_SAVE = entity.ON_SAVE,
                    ON_UPDATE = entity.ON_UPDATE,
                    OPERATION_DATE = entity.OPERATION_DATE,
                    PRICE = entity.PRICE,
                    PRINTER_ID = entity.PRINTER_ID,
                    QTY = entity.QTY,
                    RATE = entity.RATE,
                    RESETBOTTOMMARGIN = entity.RESETBOTTOMMARGIN,
                    RESETLEFTMARGIN = entity.RESETLEFTMARGIN,
                    RESETPRINTER_ID = entity.RESETPRINTER_ID,
                    RESETRIGHTMARGIN = entity.RESETRIGHTMARGIN,
                    RESETTOPMARGIN = entity.RESETTOPMARGIN,
                    SAME_ACC_ON_ENTRY = entity.SAME_ACC_ON_ENTRY,
                    SAME_ITEM_ON_BILL = entity.SAME_ITEM_ON_BILL,
                    SERVICE_CODE = entity.SERVICE_CODE,
                    SERVICE_GROUP_CODE = entity.SERVICE_GROUP_CODE,
                    SHOW_AUTO_DISCOUNT = entity.SHOW_AUTO_DISCOUNT,
                    SHOW_BILL_PAY_TYPE = entity.SHOW_BILL_PAY_TYPE,
                    SHOW_IS_IT_RESERVATION = entity.SHOW_IS_IT_RESERVATION,
                    SUPPLIER_ITEM_CODE = entity.SUPPLIER_ITEM_CODE,
                    UID = entity.UID,
                    UNPOSTED_BILL = entity.UNPOSTED_BILL,
                    AddedBy = entity.AddedBy,
                    AddedOn = entity.AddedOn,
                    UpdatedBy = entity.UpdatedBy,
                    updatedOn = entity.updatedOn,
                    IsShowPrices = entity.IsShowPrices,
                    ReconstructionOfAssets = entity.ReconstructionOfAssets,
                    DisplayPaym = entity.DisplayPaym,
                    ApplyDueValueDate = entity.ApplyDueValueDate,
                    IncludePrevYear = entity.IncludePrevYear,
                    HighlightGenActs = entity.HighlightGenActs,
                };
                sysOptRepo.Delete(bt, entity.UID);
                return true;
            });
        }

        public void Dispose()
        {
            sysOptRepo.Dispose();
            userRepo.Dispose();
            commonRepo.Dispose();
            emailRepo.Dispose();
            shortCutRepo.Dispose();
            tBoxRepo.Dispose();
            tStoreRepo.Dispose();
            incomeAccTypeRepo.Dispose();
            kestOptRepo.Dispose();
            tBudRepo.Dispose();
            sellTypeRepo.Dispose();
            addPartsRepo.Dispose();
        }

        public Task<List<System_OptionsVM>> GetAllAsync(int pageNum, int pageSize)
        {
            return Task.Run<List<System_OptionsVM>>(() =>
            {
                int rowCount;
                var q = from entity in sysOptRepo.GetPaged<string>(pageNum, pageSize, p => p.UID, false, out rowCount)
                        select new System_OptionsVM
                        {
                            A4BOTTOMMARGIN = entity.A4BOTTOMMARGIN,
                            A4LEFTMARGIN = entity.A4LEFTMARGIN,
                            A4PRINTER_ID = entity.A4PRINTER_ID,
                            A4RIGHTMARGIN = entity.A4RIGHTMARGIN,
                            A4TOPMARGIN = entity.A4TOPMARGIN,
                            BILL_CODE = entity.BILL_CODE,
                            CAN_CHANGE_BILL_DATE = entity.CAN_CHANGE_BILL_DATE,
                            CAN_CHANGE_THE_PRICE_ON_THE_BILL = entity.CAN_CHANGE_THE_PRICE_ON_THE_BILL,
                            CAN_SEE_THE_BILL_PROFIT = entity.CAN_SEE_THE_BILL_PROFIT,
                            CAN_SEE_THE_CHEQUES_NOTIFICATIONS = entity.CAN_SEE_THE_CHEQUES_NOTIFICATIONS,
                            CAN_SEE_THE_CURRENT_QTY_ON_BILL = entity.CAN_SEE_THE_CURRENT_QTY_ON_BILL,
                            CAN_SEE_THE_PRICE = entity.CAN_SEE_THE_PRICE,
                            CURRENCYDIFFERENCEACCOUNTID = entity.CURRENCYDIFFERENCEACCOUNTID,
                            DATE_AFTER = entity.DATE_AFTER,
                            DEFAULT_CURRENCY = entity.DEFAULT_CURRENCY,
                            DISABLE_BILL_PAY_TYPE = entity.DISABLE_BILL_PAY_TYPE,
                            END_DATE = entity.END_DATE,
                            END_SYSTEM = entity.END_SYSTEM,
                            ENTRY_CODE = entity.ENTRY_CODE,
                            FIRST_DATE = entity.FIRST_DATE,
                            GENERATE_THE_CURRENCY_DIFFERENCE_ENTRY_FOR_BILLS = entity.GENERATE_THE_CURRENCY_DIFFERENCE_ENTRY_FOR_BILLS,
                            GENERATE_THE_CURRENCY_DIFFERENCE_ENTRY_FOR_ENTRIES = entity.GENERATE_THE_CURRENCY_DIFFERENCE_ENTRY_FOR_ENTRIES,
                            HOW_MANY_BILL_CAN_NAVIGATE = entity.HOW_MANY_BILL_CAN_NAVIGATE,
                            ISITORDEREDBYITEMCODEORNOT = entity.ISITORDEREDBYITEMCODEORNOT,
                            ITEM_CODE = entity.ITEM_CODE,
                            ITEM_GROUP_CODE = entity.ITEM_GROUP_CODE,
                            LOAD_THE_LAST_BILLS_AFTER_SAVE = entity.LOAD_THE_LAST_BILLS_AFTER_SAVE,
                            LOSTACCOUNTID = entity.LOSTACCOUNTID,
                            NUMBEROFDAYSBEFOREDUEDATE = entity.NUMBEROFDAYSBEFOREDUEDATE,
                            ON_ASK_ABOUT_CHANGE = entity.ON_ASK_ABOUT_CHANGE,
                            ON_DELETE = entity.ON_DELETE,
                            ON_POST_ACC = entity.ON_POST_ACC,
                            ON_POST_ENTRY = entity.ON_POST_ENTRY,
                            ON_POST_STORE = entity.ON_POST_STORE,
                            ON_SAVE = entity.ON_SAVE,
                            ON_UPDATE = entity.ON_UPDATE,
                            OPERATION_DATE = entity.OPERATION_DATE,
                            PRICE = entity.PRICE,
                            PRINTER_ID = entity.PRINTER_ID,
                            QTY = entity.QTY,
                            RATE = entity.RATE,
                            RESETBOTTOMMARGIN = entity.RESETBOTTOMMARGIN,
                            RESETLEFTMARGIN = entity.RESETLEFTMARGIN,
                            RESETPRINTER_ID = entity.RESETPRINTER_ID,
                            RESETRIGHTMARGIN = entity.RESETRIGHTMARGIN,
                            RESETTOPMARGIN = entity.RESETTOPMARGIN,
                            SAME_ACC_ON_ENTRY = entity.SAME_ACC_ON_ENTRY,
                            SAME_ITEM_ON_BILL = entity.SAME_ITEM_ON_BILL,
                            SERVICE_CODE = entity.SERVICE_CODE,
                            SERVICE_GROUP_CODE = entity.SERVICE_GROUP_CODE,
                            SHOW_AUTO_DISCOUNT = entity.SHOW_AUTO_DISCOUNT,
                            SHOW_BILL_PAY_TYPE = entity.SHOW_BILL_PAY_TYPE,
                            SHOW_IS_IT_RESERVATION = entity.SHOW_IS_IT_RESERVATION,
                            SUPPLIER_ITEM_CODE = entity.SUPPLIER_ITEM_CODE,
                            UID = entity.UID,
                            UNPOSTED_BILL = entity.UNPOSTED_BILL,
                            AddedBy = entity.AddedBy,
                            AddedOn = entity.AddedOn,
                            UpdatedBy = entity.UpdatedBy,
                            updatedOn = entity.updatedOn,
                            IsShowPrices = entity.IsShowPrices,
                            ReconstructionOfAssets = entity.ReconstructionOfAssets,
                            DisplayPaym = entity.DisplayPaym,
                            ApplyDueValueDate = entity.ApplyDueValueDate,
                            IncludePrevYear = entity.IncludePrevYear,
                            HighlightGenActs = entity.HighlightGenActs,
                        };
                return q.ToList();
            });
        }

        public List<System_OptionsVM> GetAll()
        {
            var q = from entity in sysOptRepo.GetAll()
                    select new System_OptionsVM
                    {
                        A4BOTTOMMARGIN = entity.A4BOTTOMMARGIN,
                        A4LEFTMARGIN = entity.A4LEFTMARGIN,
                        A4PRINTER_ID = entity.A4PRINTER_ID,
                        A4RIGHTMARGIN = entity.A4RIGHTMARGIN,
                        A4TOPMARGIN = entity.A4TOPMARGIN,
                        BILL_CODE = entity.BILL_CODE,
                        CAN_CHANGE_BILL_DATE = entity.CAN_CHANGE_BILL_DATE,
                        CAN_CHANGE_THE_PRICE_ON_THE_BILL = entity.CAN_CHANGE_THE_PRICE_ON_THE_BILL,
                        CAN_SEE_THE_BILL_PROFIT = entity.CAN_SEE_THE_BILL_PROFIT,
                        CAN_SEE_THE_CHEQUES_NOTIFICATIONS = entity.CAN_SEE_THE_CHEQUES_NOTIFICATIONS,
                        CAN_SEE_THE_CURRENT_QTY_ON_BILL = entity.CAN_SEE_THE_CURRENT_QTY_ON_BILL,
                        CAN_SEE_THE_PRICE = entity.CAN_SEE_THE_PRICE,
                        CURRENCYDIFFERENCEACCOUNTID = entity.CURRENCYDIFFERENCEACCOUNTID,
                        DATE_AFTER = entity.DATE_AFTER,
                        DEFAULT_CURRENCY = entity.DEFAULT_CURRENCY,
                        DISABLE_BILL_PAY_TYPE = entity.DISABLE_BILL_PAY_TYPE,
                        END_DATE = entity.END_DATE,
                        END_SYSTEM = entity.END_SYSTEM,
                        ENTRY_CODE = entity.ENTRY_CODE,
                        FIRST_DATE = entity.FIRST_DATE,
                        GENERATE_THE_CURRENCY_DIFFERENCE_ENTRY_FOR_BILLS = entity.GENERATE_THE_CURRENCY_DIFFERENCE_ENTRY_FOR_BILLS,
                        GENERATE_THE_CURRENCY_DIFFERENCE_ENTRY_FOR_ENTRIES = entity.GENERATE_THE_CURRENCY_DIFFERENCE_ENTRY_FOR_ENTRIES,
                        HOW_MANY_BILL_CAN_NAVIGATE = entity.HOW_MANY_BILL_CAN_NAVIGATE,
                        ISITORDEREDBYITEMCODEORNOT = entity.ISITORDEREDBYITEMCODEORNOT,
                        ITEM_CODE = entity.ITEM_CODE,
                        ITEM_GROUP_CODE = entity.ITEM_GROUP_CODE,
                        LOAD_THE_LAST_BILLS_AFTER_SAVE = entity.LOAD_THE_LAST_BILLS_AFTER_SAVE,
                        LOSTACCOUNTID = entity.LOSTACCOUNTID,
                        NUMBEROFDAYSBEFOREDUEDATE = entity.NUMBEROFDAYSBEFOREDUEDATE,
                        ON_ASK_ABOUT_CHANGE = entity.ON_ASK_ABOUT_CHANGE,
                        ON_DELETE = entity.ON_DELETE,
                        ON_POST_ACC = entity.ON_POST_ACC,
                        ON_POST_ENTRY = entity.ON_POST_ENTRY,
                        ON_POST_STORE = entity.ON_POST_STORE,
                        ON_SAVE = entity.ON_SAVE,
                        ON_UPDATE = entity.ON_UPDATE,
                        OPERATION_DATE = entity.OPERATION_DATE,
                        PRICE = entity.PRICE,
                        PRINTER_ID = entity.PRINTER_ID,
                        QTY = entity.QTY,
                        RATE = entity.RATE,
                        RESETBOTTOMMARGIN = entity.RESETBOTTOMMARGIN,
                        RESETLEFTMARGIN = entity.RESETLEFTMARGIN,
                        RESETPRINTER_ID = entity.RESETPRINTER_ID,
                        RESETRIGHTMARGIN = entity.RESETRIGHTMARGIN,
                        RESETTOPMARGIN = entity.RESETTOPMARGIN,
                        SAME_ACC_ON_ENTRY = entity.SAME_ACC_ON_ENTRY,
                        SAME_ITEM_ON_BILL = entity.SAME_ITEM_ON_BILL,
                        SERVICE_CODE = entity.SERVICE_CODE,
                        SERVICE_GROUP_CODE = entity.SERVICE_GROUP_CODE,
                        SHOW_AUTO_DISCOUNT = entity.SHOW_AUTO_DISCOUNT,
                        SHOW_BILL_PAY_TYPE = entity.SHOW_BILL_PAY_TYPE,
                        SHOW_IS_IT_RESERVATION = entity.SHOW_IS_IT_RESERVATION,
                        SUPPLIER_ITEM_CODE = entity.SUPPLIER_ITEM_CODE,
                        UID = entity.UID,
                        UNPOSTED_BILL = entity.UNPOSTED_BILL,
                        AddedBy = entity.AddedBy,
                        AddedOn = entity.AddedOn,
                        UpdatedBy = entity.UpdatedBy,
                        updatedOn = entity.updatedOn,
                        IsShowPrices = entity.IsShowPrices,
                        ReconstructionOfAssets = entity.ReconstructionOfAssets,
                        DisplayPaym = entity.DisplayPaym,
                        ApplyDueValueDate = entity.ApplyDueValueDate,
                        IncludePrevYear = entity.IncludePrevYear,
                        HighlightGenActs = entity.HighlightGenActs,
                    };
            return q.ToList();
        }

        public bool? getFilterCompValue()
        {
            var query1 = sysOptRepo.GetAll().FirstOrDefault();
            if (query1 != null)
            {
                return query1.filterWithCompanyBranch;
            }
            else
            {
                return null;
            }
        }

        public bool insertFilterForCompany(string[] entity)
        {
            bool x = bool.Parse(entity[0]);
            string userName = entity[1];

            var q=sysOptRepo.GetAll();
            if (q != null)
            {
                foreach(var item in q)
                {
                    item.filterWithCompanyBranch = x;
                    sysOptRepo.Update(item, item.UID);
                }

            }
            //sysOptRepo
            return true;
        }
        public async Task<bool> Insert(System_OptionsVM entity)
        {
            try
            {
                SYSTEM_OPTIONS bt = new SYSTEM_OPTIONS
                {
                    A4BOTTOMMARGIN = entity.A4BOTTOMMARGIN,
                    A4LEFTMARGIN = entity.A4LEFTMARGIN,
                    A4PRINTER_ID = entity.A4PRINTER_ID,
                    A4RIGHTMARGIN = entity.A4RIGHTMARGIN,
                    A4TOPMARGIN = entity.A4TOPMARGIN,
                    BILL_CODE = entity.BILL_CODE,
                    CAN_CHANGE_BILL_DATE = entity.CAN_CHANGE_BILL_DATE,
                    CAN_CHANGE_THE_PRICE_ON_THE_BILL = entity.CAN_CHANGE_THE_PRICE_ON_THE_BILL,
                    CAN_SEE_THE_BILL_PROFIT = entity.CAN_SEE_THE_BILL_PROFIT,
                    CAN_SEE_THE_CHEQUES_NOTIFICATIONS = entity.CAN_SEE_THE_CHEQUES_NOTIFICATIONS,
                    CAN_SEE_THE_CURRENT_QTY_ON_BILL = entity.CAN_SEE_THE_CURRENT_QTY_ON_BILL,
                    CAN_SEE_THE_PRICE = entity.CAN_SEE_THE_PRICE,
                    CURRENCYDIFFERENCEACCOUNTID = entity.CURRENCYDIFFERENCEACCOUNTID,
                    DATE_AFTER = entity.DATE_AFTER,
                    DEFAULT_CURRENCY = entity.DEFAULT_CURRENCY,
                    DISABLE_BILL_PAY_TYPE = entity.DISABLE_BILL_PAY_TYPE,
                    END_DATE = entity.END_DATE,
                    END_SYSTEM = entity.END_SYSTEM,
                    ENTRY_CODE = entity.ENTRY_CODE,
                    FIRST_DATE = entity.FIRST_DATE,
                    GENERATE_THE_CURRENCY_DIFFERENCE_ENTRY_FOR_BILLS = entity.GENERATE_THE_CURRENCY_DIFFERENCE_ENTRY_FOR_BILLS,
                    GENERATE_THE_CURRENCY_DIFFERENCE_ENTRY_FOR_ENTRIES = entity.GENERATE_THE_CURRENCY_DIFFERENCE_ENTRY_FOR_ENTRIES,
                    HOW_MANY_BILL_CAN_NAVIGATE = entity.HOW_MANY_BILL_CAN_NAVIGATE,
                    ISITORDEREDBYITEMCODEORNOT = entity.ISITORDEREDBYITEMCODEORNOT,
                    ITEM_CODE = entity.ITEM_CODE,
                    ITEM_GROUP_CODE = entity.ITEM_GROUP_CODE,
                    LOAD_THE_LAST_BILLS_AFTER_SAVE = entity.LOAD_THE_LAST_BILLS_AFTER_SAVE,
                    LOSTACCOUNTID = entity.LOSTACCOUNTID,
                    NUMBEROFDAYSBEFOREDUEDATE = entity.NUMBEROFDAYSBEFOREDUEDATE,
                    ON_ASK_ABOUT_CHANGE = entity.ON_ASK_ABOUT_CHANGE,
                    ON_DELETE = entity.ON_DELETE,
                    ON_POST_ACC = entity.ON_POST_ACC,
                    ON_POST_ENTRY = entity.ON_POST_ENTRY,
                    ON_POST_STORE = entity.ON_POST_STORE,
                    ON_SAVE = entity.ON_SAVE,
                    ON_UPDATE = entity.ON_UPDATE,
                    OPERATION_DATE = entity.OPERATION_DATE,
                    PRICE = entity.PRICE,
                    PRINTER_ID = entity.PRINTER_ID,
                    QTY = entity.QTY,
                    RATE = entity.RATE,
                    RESETBOTTOMMARGIN = entity.RESETBOTTOMMARGIN,
                    RESETLEFTMARGIN = entity.RESETLEFTMARGIN,
                    RESETPRINTER_ID = entity.RESETPRINTER_ID,
                    RESETRIGHTMARGIN = entity.RESETRIGHTMARGIN,
                    RESETTOPMARGIN = entity.RESETTOPMARGIN,
                    SAME_ACC_ON_ENTRY = entity.SAME_ACC_ON_ENTRY,
                    SAME_ITEM_ON_BILL = entity.SAME_ITEM_ON_BILL,
                    SERVICE_CODE = entity.SERVICE_CODE,
                    SERVICE_GROUP_CODE = entity.SERVICE_GROUP_CODE,
                    SHOW_AUTO_DISCOUNT = entity.SHOW_AUTO_DISCOUNT,
                    SHOW_BILL_PAY_TYPE = entity.SHOW_BILL_PAY_TYPE,
                    SHOW_IS_IT_RESERVATION = entity.SHOW_IS_IT_RESERVATION,
                    SUPPLIER_ITEM_CODE = entity.SUPPLIER_ITEM_CODE,
                    UID = entity.UID,
                    UNPOSTED_BILL = entity.UNPOSTED_BILL,
                    AddedBy = entity.AddedBy,
                    AddedOn = entity.AddedOn,
                    UpdatedBy = entity.UpdatedBy,
                    updatedOn = entity.updatedOn,
                    IsItemCodeRelatedByGroup=entity.IsItemCodeRelatedByGroup,
                    CodeSeparator=entity.CodeSeparator,
                    IsShowPrices = entity.IsShowPrices,
                    ReconstructionOfAssets = entity.ReconstructionOfAssets,
                    DisplayPaym = entity.DisplayPaym,
                    ApplyDueValueDate = entity.ApplyDueValueDate,
                    IncludePrevYear = entity.IncludePrevYear,
                    HighlightGenActs = entity.HighlightGenActs,
                };
                sysOptRepo.Add(bt);
            }
            catch(SqlException ex)
            {
                var x = ex.Message;
            }
        
            return true;
        }

        public Task<string> InsertAsync(System_OptionsVM entity)
        {
            return Task.Run<string>(() =>
            {
                SYSTEM_OPTIONS bt = new SYSTEM_OPTIONS
                {
                    A4BOTTOMMARGIN = entity.A4BOTTOMMARGIN,
                    A4LEFTMARGIN = entity.A4LEFTMARGIN,
                    A4PRINTER_ID = entity.A4PRINTER_ID,
                    A4RIGHTMARGIN = entity.A4RIGHTMARGIN,
                    A4TOPMARGIN = entity.A4TOPMARGIN,
                    BILL_CODE = entity.BILL_CODE,
                    CAN_CHANGE_BILL_DATE = entity.CAN_CHANGE_BILL_DATE,
                    CAN_CHANGE_THE_PRICE_ON_THE_BILL = entity.CAN_CHANGE_THE_PRICE_ON_THE_BILL,
                    CAN_SEE_THE_BILL_PROFIT = entity.CAN_SEE_THE_BILL_PROFIT,
                    CAN_SEE_THE_CHEQUES_NOTIFICATIONS = entity.CAN_SEE_THE_CHEQUES_NOTIFICATIONS,
                    CAN_SEE_THE_CURRENT_QTY_ON_BILL = entity.CAN_SEE_THE_CURRENT_QTY_ON_BILL,
                    CAN_SEE_THE_PRICE = entity.CAN_SEE_THE_PRICE,
                    CURRENCYDIFFERENCEACCOUNTID = entity.CURRENCYDIFFERENCEACCOUNTID,
                    DATE_AFTER = entity.DATE_AFTER,
                    DEFAULT_CURRENCY = entity.DEFAULT_CURRENCY,
                    DISABLE_BILL_PAY_TYPE = entity.DISABLE_BILL_PAY_TYPE,
                    END_DATE = entity.END_DATE,
                    END_SYSTEM = entity.END_SYSTEM,
                    ENTRY_CODE = entity.ENTRY_CODE,
                    FIRST_DATE = entity.FIRST_DATE,
                    GENERATE_THE_CURRENCY_DIFFERENCE_ENTRY_FOR_BILLS = entity.GENERATE_THE_CURRENCY_DIFFERENCE_ENTRY_FOR_BILLS,
                    GENERATE_THE_CURRENCY_DIFFERENCE_ENTRY_FOR_ENTRIES = entity.GENERATE_THE_CURRENCY_DIFFERENCE_ENTRY_FOR_ENTRIES,
                    HOW_MANY_BILL_CAN_NAVIGATE = entity.HOW_MANY_BILL_CAN_NAVIGATE,
                    ISITORDEREDBYITEMCODEORNOT = entity.ISITORDEREDBYITEMCODEORNOT,
                    ITEM_CODE = entity.ITEM_CODE,
                    ITEM_GROUP_CODE = entity.ITEM_GROUP_CODE,
                    LOAD_THE_LAST_BILLS_AFTER_SAVE = entity.LOAD_THE_LAST_BILLS_AFTER_SAVE,
                    LOSTACCOUNTID = entity.LOSTACCOUNTID,
                    NUMBEROFDAYSBEFOREDUEDATE = entity.NUMBEROFDAYSBEFOREDUEDATE,
                    ON_ASK_ABOUT_CHANGE = entity.ON_ASK_ABOUT_CHANGE,
                    ON_DELETE = entity.ON_DELETE,
                    ON_POST_ACC = entity.ON_POST_ACC,
                    ON_POST_ENTRY = entity.ON_POST_ENTRY,
                    ON_POST_STORE = entity.ON_POST_STORE,
                    ON_SAVE = entity.ON_SAVE,
                    ON_UPDATE = entity.ON_UPDATE,
                    OPERATION_DATE = entity.OPERATION_DATE,
                    PRICE = entity.PRICE,
                    PRINTER_ID = entity.PRINTER_ID,
                    QTY = entity.QTY,
                    RATE = entity.RATE,
                    RESETBOTTOMMARGIN = entity.RESETBOTTOMMARGIN,
                    RESETLEFTMARGIN = entity.RESETLEFTMARGIN,
                    RESETPRINTER_ID = entity.RESETPRINTER_ID,
                    RESETRIGHTMARGIN = entity.RESETRIGHTMARGIN,
                    RESETTOPMARGIN = entity.RESETTOPMARGIN,
                    SAME_ACC_ON_ENTRY = entity.SAME_ACC_ON_ENTRY,
                    SAME_ITEM_ON_BILL = entity.SAME_ITEM_ON_BILL,
                    SERVICE_CODE = entity.SERVICE_CODE,
                    SERVICE_GROUP_CODE = entity.SERVICE_GROUP_CODE,
                    SHOW_AUTO_DISCOUNT = entity.SHOW_AUTO_DISCOUNT,
                    SHOW_BILL_PAY_TYPE = entity.SHOW_BILL_PAY_TYPE,
                    SHOW_IS_IT_RESERVATION = entity.SHOW_IS_IT_RESERVATION,
                    SUPPLIER_ITEM_CODE = entity.SUPPLIER_ITEM_CODE,
                    UID = entity.UID,
                    UNPOSTED_BILL = entity.UNPOSTED_BILL,
                    AddedBy = entity.AddedBy,
                    AddedOn = entity.AddedOn,
                    UpdatedBy = entity.UpdatedBy,
                    updatedOn = entity.updatedOn,
                    IsItemCodeRelatedByGroup = entity.IsItemCodeRelatedByGroup,
                    CodeSeparator = entity.CodeSeparator,
                    IsShowPrices = entity.IsShowPrices,
                    ReconstructionOfAssets = entity.ReconstructionOfAssets,
                    DisplayPaym = entity.DisplayPaym,
                    ApplyDueValueDate = entity.ApplyDueValueDate,
                    IncludePrevYear = entity.IncludePrevYear,
                    HighlightGenActs = entity.HighlightGenActs,
                };
                sysOptRepo.Add(bt);
                return bt.UID;
            });
        }

        public bool Update(System_OptionsVM entity)
        {
            SYSTEM_OPTIONS bt = new SYSTEM_OPTIONS
            {
                A4BOTTOMMARGIN = entity.A4BOTTOMMARGIN,
                A4LEFTMARGIN = entity.A4LEFTMARGIN,
                A4PRINTER_ID = entity.A4PRINTER_ID,
                A4RIGHTMARGIN = entity.A4RIGHTMARGIN,
                A4TOPMARGIN = entity.A4TOPMARGIN,
                BILL_CODE = entity.BILL_CODE,
                CAN_CHANGE_BILL_DATE = entity.CAN_CHANGE_BILL_DATE,
                CAN_CHANGE_THE_PRICE_ON_THE_BILL = entity.CAN_CHANGE_THE_PRICE_ON_THE_BILL,
                CAN_SEE_THE_BILL_PROFIT = entity.CAN_SEE_THE_BILL_PROFIT,
                CAN_SEE_THE_CHEQUES_NOTIFICATIONS = entity.CAN_SEE_THE_CHEQUES_NOTIFICATIONS,
                CAN_SEE_THE_CURRENT_QTY_ON_BILL = entity.CAN_SEE_THE_CURRENT_QTY_ON_BILL,
                CAN_SEE_THE_PRICE = entity.CAN_SEE_THE_PRICE,
                CURRENCYDIFFERENCEACCOUNTID = entity.CURRENCYDIFFERENCEACCOUNTID,
                DATE_AFTER = entity.DATE_AFTER,
                DEFAULT_CURRENCY = entity.DEFAULT_CURRENCY,
                DISABLE_BILL_PAY_TYPE = entity.DISABLE_BILL_PAY_TYPE,
                END_DATE = entity.END_DATE,
                END_SYSTEM = entity.END_SYSTEM,
                ENTRY_CODE = entity.ENTRY_CODE,
                FIRST_DATE = entity.FIRST_DATE,
                GENERATE_THE_CURRENCY_DIFFERENCE_ENTRY_FOR_BILLS = entity.GENERATE_THE_CURRENCY_DIFFERENCE_ENTRY_FOR_BILLS,
                GENERATE_THE_CURRENCY_DIFFERENCE_ENTRY_FOR_ENTRIES = entity.GENERATE_THE_CURRENCY_DIFFERENCE_ENTRY_FOR_ENTRIES,
                HOW_MANY_BILL_CAN_NAVIGATE = entity.HOW_MANY_BILL_CAN_NAVIGATE,
                ISITORDEREDBYITEMCODEORNOT = entity.ISITORDEREDBYITEMCODEORNOT,
                ITEM_CODE = entity.ITEM_CODE,
                ITEM_GROUP_CODE = entity.ITEM_GROUP_CODE,
                LOAD_THE_LAST_BILLS_AFTER_SAVE = entity.LOAD_THE_LAST_BILLS_AFTER_SAVE,
                LOSTACCOUNTID = entity.LOSTACCOUNTID,
                NUMBEROFDAYSBEFOREDUEDATE = entity.NUMBEROFDAYSBEFOREDUEDATE,
                ON_ASK_ABOUT_CHANGE = entity.ON_ASK_ABOUT_CHANGE,
                ON_DELETE = entity.ON_DELETE,
                ON_POST_ACC = entity.ON_POST_ACC,
                ON_POST_ENTRY = entity.ON_POST_ENTRY,
                ON_POST_STORE = entity.ON_POST_STORE,
                ON_SAVE = entity.ON_SAVE,
                ON_UPDATE = entity.ON_UPDATE,
                OPERATION_DATE = entity.OPERATION_DATE,
                PRICE = entity.PRICE,
                PRINTER_ID = entity.PRINTER_ID,
                QTY = entity.QTY,
                RATE = entity.RATE,
                RESETBOTTOMMARGIN = entity.RESETBOTTOMMARGIN,
                RESETLEFTMARGIN = entity.RESETLEFTMARGIN,
                RESETPRINTER_ID = entity.RESETPRINTER_ID,
                RESETRIGHTMARGIN = entity.RESETRIGHTMARGIN,
                RESETTOPMARGIN = entity.RESETTOPMARGIN,
                SAME_ACC_ON_ENTRY = entity.SAME_ACC_ON_ENTRY,
                SAME_ITEM_ON_BILL = entity.SAME_ITEM_ON_BILL,
                SERVICE_CODE = entity.SERVICE_CODE,
                SERVICE_GROUP_CODE = entity.SERVICE_GROUP_CODE,
                SHOW_AUTO_DISCOUNT = entity.SHOW_AUTO_DISCOUNT,
                SHOW_BILL_PAY_TYPE = entity.SHOW_BILL_PAY_TYPE,
                SHOW_IS_IT_RESERVATION = entity.SHOW_IS_IT_RESERVATION,
                SUPPLIER_ITEM_CODE = entity.SUPPLIER_ITEM_CODE,
                UID = entity.UID,
                UNPOSTED_BILL = entity.UNPOSTED_BILL,
                AddedBy = entity.AddedBy,
                AddedOn = entity.AddedOn,
                UpdatedBy = entity.UpdatedBy,
                updatedOn = entity.updatedOn,
                IsItemCodeRelatedByGroup = entity.IsItemCodeRelatedByGroup,
                CodeSeparator = entity.CodeSeparator,
                IsShowPrices = entity.IsShowPrices,
                ReconstructionOfAssets = entity.ReconstructionOfAssets,
                DisplayPaym = entity.DisplayPaym,
                ApplyDueValueDate = entity.ApplyDueValueDate,
                IncludePrevYear = entity.IncludePrevYear,
                HighlightGenActs = entity.HighlightGenActs,
            };
            sysOptRepo.Update(bt, bt.UID);
            return true;
        }

        public Task<bool> UpdateAsync(System_OptionsVM entity)
        {
            return Task.Run<bool>(() =>
            {
                SYSTEM_OPTIONS bt = new SYSTEM_OPTIONS
                {
                    A4BOTTOMMARGIN = entity.A4BOTTOMMARGIN,
                    A4LEFTMARGIN = entity.A4LEFTMARGIN,
                    A4PRINTER_ID = entity.A4PRINTER_ID,
                    A4RIGHTMARGIN = entity.A4RIGHTMARGIN,
                    A4TOPMARGIN = entity.A4TOPMARGIN,
                    BILL_CODE = entity.BILL_CODE,
                    CAN_CHANGE_BILL_DATE = entity.CAN_CHANGE_BILL_DATE,
                    CAN_CHANGE_THE_PRICE_ON_THE_BILL = entity.CAN_CHANGE_THE_PRICE_ON_THE_BILL,
                    CAN_SEE_THE_BILL_PROFIT = entity.CAN_SEE_THE_BILL_PROFIT,
                    CAN_SEE_THE_CHEQUES_NOTIFICATIONS = entity.CAN_SEE_THE_CHEQUES_NOTIFICATIONS,
                    CAN_SEE_THE_CURRENT_QTY_ON_BILL = entity.CAN_SEE_THE_CURRENT_QTY_ON_BILL,
                    CAN_SEE_THE_PRICE = entity.CAN_SEE_THE_PRICE,
                    CURRENCYDIFFERENCEACCOUNTID = entity.CURRENCYDIFFERENCEACCOUNTID,
                    DATE_AFTER = entity.DATE_AFTER,
                    DEFAULT_CURRENCY = entity.DEFAULT_CURRENCY,
                    DISABLE_BILL_PAY_TYPE = entity.DISABLE_BILL_PAY_TYPE,
                    END_DATE = entity.END_DATE,
                    END_SYSTEM = entity.END_SYSTEM,
                    ENTRY_CODE = entity.ENTRY_CODE,
                    FIRST_DATE = entity.FIRST_DATE,
                    GENERATE_THE_CURRENCY_DIFFERENCE_ENTRY_FOR_BILLS = entity.GENERATE_THE_CURRENCY_DIFFERENCE_ENTRY_FOR_BILLS,
                    GENERATE_THE_CURRENCY_DIFFERENCE_ENTRY_FOR_ENTRIES = entity.GENERATE_THE_CURRENCY_DIFFERENCE_ENTRY_FOR_ENTRIES,
                    HOW_MANY_BILL_CAN_NAVIGATE = entity.HOW_MANY_BILL_CAN_NAVIGATE,
                    ISITORDEREDBYITEMCODEORNOT = entity.ISITORDEREDBYITEMCODEORNOT,
                    ITEM_CODE = entity.ITEM_CODE,
                    ITEM_GROUP_CODE = entity.ITEM_GROUP_CODE,
                    LOAD_THE_LAST_BILLS_AFTER_SAVE = entity.LOAD_THE_LAST_BILLS_AFTER_SAVE,
                    LOSTACCOUNTID = entity.LOSTACCOUNTID,
                    NUMBEROFDAYSBEFOREDUEDATE = entity.NUMBEROFDAYSBEFOREDUEDATE,
                    ON_ASK_ABOUT_CHANGE = entity.ON_ASK_ABOUT_CHANGE,
                    ON_DELETE = entity.ON_DELETE,
                    ON_POST_ACC = entity.ON_POST_ACC,
                    ON_POST_ENTRY = entity.ON_POST_ENTRY,
                    ON_POST_STORE = entity.ON_POST_STORE,
                    ON_SAVE = entity.ON_SAVE,
                    ON_UPDATE = entity.ON_UPDATE,
                    OPERATION_DATE = entity.OPERATION_DATE,
                    PRICE = entity.PRICE,
                    PRINTER_ID = entity.PRINTER_ID,
                    QTY = entity.QTY,
                    RATE = entity.RATE,
                    RESETBOTTOMMARGIN = entity.RESETBOTTOMMARGIN,
                    RESETLEFTMARGIN = entity.RESETLEFTMARGIN,
                    RESETPRINTER_ID = entity.RESETPRINTER_ID,
                    RESETRIGHTMARGIN = entity.RESETRIGHTMARGIN,
                    RESETTOPMARGIN = entity.RESETTOPMARGIN,
                    SAME_ACC_ON_ENTRY = entity.SAME_ACC_ON_ENTRY,
                    SAME_ITEM_ON_BILL = entity.SAME_ITEM_ON_BILL,
                    SERVICE_CODE = entity.SERVICE_CODE,
                    SERVICE_GROUP_CODE = entity.SERVICE_GROUP_CODE,
                    SHOW_AUTO_DISCOUNT = entity.SHOW_AUTO_DISCOUNT,
                    SHOW_BILL_PAY_TYPE = entity.SHOW_BILL_PAY_TYPE,
                    SHOW_IS_IT_RESERVATION = entity.SHOW_IS_IT_RESERVATION,
                    SUPPLIER_ITEM_CODE = entity.SUPPLIER_ITEM_CODE,
                    UID = entity.UID,
                    UNPOSTED_BILL = entity.UNPOSTED_BILL,
                    AddedBy = entity.AddedBy,
                    AddedOn = entity.AddedOn,
                    UpdatedBy = entity.UpdatedBy,
                    updatedOn = entity.updatedOn,
                    IsItemCodeRelatedByGroup = entity.IsItemCodeRelatedByGroup,
                    CodeSeparator = entity.CodeSeparator,
                    IsShowPrices = entity.IsShowPrices,
                    ReconstructionOfAssets = entity.ReconstructionOfAssets,
                    DisplayPaym = entity.DisplayPaym,
                    ApplyDueValueDate = entity.ApplyDueValueDate,
                    IncludePrevYear = entity.IncludePrevYear,
                    HighlightGenActs = entity.HighlightGenActs,
                };
                sysOptRepo.Update(bt, bt.UID);
                return true;
            });
        }

        public Task<System_OptionsVM> GetByUserName(string userName)
        {
            return Task.Run<System_OptionsVM>(() =>
            {
                var entity = sysOptRepo.Filter(X => X.UID == userName).Select(Y => AutoMapper.Mapper.Map<SYSTEM_OPTIONS, System_OptionsVM>(Y)).FirstOrDefault();
                return entity;
            });
        }


        public  Task<bool> SaveAll(SystemOptionsComplexVM ComplexObj)
        {

            return Task.Run<bool>(() =>
            {
                System_OptionsVM sysOpt = AsyncHelpers.RunSync<System_OptionsVM>(() => GetByUserName(ComplexObj.SystemOptions.UID));

               // try
                {
                    #region Services
                    UserService userService = new UserService(userRepo, commonRepo);
                    EmailsService emailService = new EmailsService(emailRepo);
                    ShortcutService shortCutService = new ShortcutService(shortCutRepo);
                    TBoxAccountsService tBoxService = new TBoxAccountsService(tBoxRepo);
                    TStoreService tStoreService = new TStoreService(tStoreRepo);
                    TBudgetAccountsService tBudService = new TBudgetAccountsService(tBudRepo);
                    IncomeAccountTypesService incomeAccTypeService = new IncomeAccountTypesService(incomeAccTypeRepo);
                    KestOptionService kestOptService = new KestOptionService(kestOptRepo);
                    SELLS_TYPESService sellTypeService = new SELLS_TYPESService(sellTypeRepo);
                    AddressPartsSettingsService addPartsService = new AddressPartsSettingsService(addPartsRepo);
                    #endregion
                    if (sysOpt != null)
                    {
                        bool sysRes = Update(ComplexObj.SystemOptions);
                        if (sysRes)
                        {
                           // bool userRes = userService.Update(ComplexObj.userDataEntity);
                            #region Emails
                            List<EmailsVM> emails = AsyncHelpers.RunSync<List<EmailsVM>>(() => emailService.GetByUID(ComplexObj.userDataEntity.ID));

                            for (int i = 0; i < emails.Count(); i++)
                            {
                                emailService.Delete(emails[i]);
                            }
                            for (int i = 0; i < ComplexObj.emailsList.Count(); i++)
                            {
                                emailService.Insert(ComplexObj.emailsList[i]);
                            }
                            #endregion

                            #region Shortcuts
                            List<ShortcutsVM> shortcuts = AsyncHelpers.RunSync<List<ShortcutsVM>>(() => shortCutService.getByUID(ComplexObj.userDataEntity.UID));
                            for (int i = 0; i < shortcuts.Count(); i++)
                            {
                                shortCutService.Delete(shortcuts[i]);
                            }
                            for (int i = 0; i < ComplexObj.shortcutList.Count(); i++)
                            {
                                shortCutService.Insert(ComplexObj.shortcutList[i]);
                            }
                            #endregion
                            #region TBoxAccounts
                            List<TBoxAccountsVM> tbox = AsyncHelpers.RunSync<List<TBoxAccountsVM>>(() => tBoxService.GetByUID(ComplexObj.userDataEntity.UID));
                            for (int i = 0; i < tbox.Count(); i++)
                            {
                                tBoxService.Delete(tbox[i]);
                            }
                            for (int i = 0; i < ComplexObj.TBoxAccountsList.Count(); i++)
                            {
                                tBoxService.Insert(ComplexObj.TBoxAccountsList[i]);
                            }
                            #endregion
                            #region TBudgetAccounts
                            List<TBudgetAccountsVM> tbud = AsyncHelpers.RunSync<List<TBudgetAccountsVM>>(() => tBudService.GetByUID(ComplexObj.userDataEntity.UID));
                            for (int i = 0; i < tbud.Count(); i++)
                            {
                                tBudService.Delete(tbud[i]);
                            }
                            for (int i = 0; i < ComplexObj.IncomeAccountTypesList.Count(); i++)
                            {
                                tBudService.Insert(ComplexObj.IncomeAccountTypesList[i]);
                            }
                            #endregion
                            #region TStore
                            List<TStoreVM> tStore = AsyncHelpers.RunSync<List<TStoreVM>>(() => tStoreService.GetByUID(ComplexObj.userDataEntity.UID));
                            for (int i = 0; i < tStore.Count(); i++)
                            {
                                tStoreService.Delete(tStore[i]);
                            }
                            for (int i = 0; i < ComplexObj.TStoresList.Count(); i++)
                            {
                                tStoreService.Insert(ComplexObj.TStoresList[i]);
                            }
                            #endregion
                            #region SELL_TYPES
                            SELLS_TYPESVM sellType1 = sellTypeService.GetByID(1);
                            sellType1.SELL_TYPE_AR_NAME = ComplexObj.priceTypeEntity.Price1;
                            sellType1.SHOW_OR_NOT = ComplexObj.priceTypeEntity.IsShowPrice1;
                            bool res1 = sellTypeService.Update(sellType1);
                            SELLS_TYPESVM sellType2 = sellTypeService.GetByID(2);
                            sellType2.SELL_TYPE_AR_NAME = ComplexObj.priceTypeEntity.Price2;
                            sellType2.SHOW_OR_NOT = ComplexObj.priceTypeEntity.IsShowPrice2;
                            bool res2 = sellTypeService.Update(sellType2);
                            SELLS_TYPESVM sellType3 = sellTypeService.GetByID(3);
                            sellType3.SELL_TYPE_AR_NAME = ComplexObj.priceTypeEntity.Price3;
                            sellType3.SHOW_OR_NOT = ComplexObj.priceTypeEntity.IsShowPrice3;
                            bool res3 = sellTypeService.Update(sellType3);
                            SELLS_TYPESVM sellType4 = sellTypeService.GetByID(4);
                            sellType4.SELL_TYPE_AR_NAME = ComplexObj.priceTypeEntity.Price4;
                            sellType4.SHOW_OR_NOT = ComplexObj.priceTypeEntity.IsShowPrice4;
                            bool res4 = sellTypeService.Update(sellType4);
                            SELLS_TYPESVM sellType5 = sellTypeService.GetByID(5);
                            sellType5.SELL_TYPE_AR_NAME = ComplexObj.priceTypeEntity.Price5;
                            sellType5.SHOW_OR_NOT = ComplexObj.priceTypeEntity.IsShowPrice5;
                            bool res5 = sellTypeService.Update(sellType5);
                            SELLS_TYPESVM sellType6 = sellTypeService.GetByID(6);
                            sellType6.SELL_TYPE_AR_NAME = ComplexObj.priceTypeEntity.Price6;
                            sellType6.SHOW_OR_NOT = ComplexObj.priceTypeEntity.IsShowPrice6;
                            bool res6 = sellTypeService.Update(sellType6);
                            SELLS_TYPESVM sellType7 = sellTypeService.GetByID(7);
                            sellType7.SELL_TYPE_AR_NAME = ComplexObj.priceTypeEntity.Price7;
                            sellType7.SHOW_OR_NOT = ComplexObj.priceTypeEntity.IsShowPrice7;
                            bool res7 = sellTypeService.Update(sellType7);
                            #endregion
                            #region Kest_Options
                            for (int i = 0; i < ComplexObj.KestOptList.Count(); i++)
                            {
                                kestOptService.Update(ComplexObj.KestOptList[i]);
                            }
                            #endregion




                            #region addressParts
                            AddressPartsSettingsVM ExistaddPart = AsyncHelpers.RunSync<AddressPartsSettingsVM>(() => addPartsService.getAddrPartsByUID(ComplexObj.userDataEntity.UID));
                            //for (int i = 0; i < addParts.Count(); i++)
                            // {
                            AsyncHelpers.RunSync<bool>(() => addPartsService.deleteAddressPartsSync(ExistaddPart));
                            AsyncHelpers.RunSync<bool>(() => addPartsService.insertAddressPartsSync(ComplexObj.addPartEntity));
                            // }
                            // for (int i = 0; i < ComplexObj.addPartsList.Count(); i++)
                            // {
                            
                            //}
                            #endregion

                            return true;
                        }
                        else { return false; }
                    }
                    else
                    {
                        bool sysRes = AsyncHelpers.RunSync<bool>(() => Insert(ComplexObj.SystemOptions));
                        if (sysRes)
                        {
                           // bool userRes = userService.Update(ComplexObj.userDataEntity);
                            #region Emails
                            if (ComplexObj.emailsList.Count() > 0)
                            {
                                for (int i = 0; i < ComplexObj.emailsList.Count(); i++)
                                {
                                    emailService.Insert(ComplexObj.emailsList[i]);
                                }
                            }
                            #endregion

                            #region Shortcuts
                            if (ComplexObj.shortcutList.Count() > 0)
                            {
                                for (int i = 0; i < ComplexObj.shortcutList.Count(); i++)
                                {
                                    shortCutService.Insert(ComplexObj.shortcutList[i]);
                                }
                            }
                            #endregion

                            #region TBoxAccounts
                            if (ComplexObj.TBoxAccountsList.Count() > 0)
                            {
                                for (int i = 0; i < ComplexObj.TBoxAccountsList.Count(); i++)
                                {
                                    tBoxService.Insert(ComplexObj.TBoxAccountsList[i]);
                                }
                            }
                            #endregion

                            #region TBudgetAccounts
                            if (ComplexObj.IncomeAccountTypesList.Count() > 0)
                            {
                                for (int i = 0; i < ComplexObj.IncomeAccountTypesList.Count(); i++)
                                {
                                    tBudService.Insert(ComplexObj.IncomeAccountTypesList[i]);
                                }
                            }
                            #endregion

                            #region TStore
                            if (ComplexObj.TStoresList.Count() > 0)
                            {
                                for (int i = 0; i < ComplexObj.TStoresList.Count(); i++)
                                {
                                    tStoreService.Insert(ComplexObj.TStoresList[i]);
                                }
                            }
                            #endregion

                            #region Kest_Options
                            if (ComplexObj.KestOptList.Count() > 0)
                            {
                                for (int i = 0; i < ComplexObj.KestOptList.Count(); i++)
                                {
                                    kestOptService.Update(ComplexObj.KestOptList[i]);
                                }
                            }
                            #endregion

                            #region addParts

                            //for (int i = 0; i < ComplexObj.addPartEntity.Count(); i++)
                            //{
                            if (ComplexObj.addPartEntity != null)
                            {
                                addPartsService.insertAddressPartsSync(ComplexObj.addPartEntity);
                            }
                            //}
                            #endregion

                            return true;

                        }
                        else { return false; }
                    }
                }
                //catch (Exception ex)
                //{
                //    string str = ex.Message;
                //    return false;
                //}
            });
        }
    }
}
