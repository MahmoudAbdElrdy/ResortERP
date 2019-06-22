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
    public class BillSettingsService : IBillSettingsService, IDisposable
    {
        IGenericRepository<BILL_SETTINGS> billSettingsMRepo;
        IGenericRepository<BILL_GRID_COLUMNS> billGrdMRepo;
        IGenericRepository<UserMenu> userMenuRepo;
        IGenericRepository<BILL_TYPES> billTypeRepo;
        public BillSettingsService(IGenericRepository<BILL_SETTINGS> billSettingsMRepo, IGenericRepository<BILL_GRID_COLUMNS> billGrdMRepo, IGenericRepository<UserMenu> userMenuRepo, IGenericRepository<BILL_TYPES> billTypeRepo)
        {
            this.billSettingsMRepo = billSettingsMRepo;
            this.billGrdMRepo = billGrdMRepo;
            this.userMenuRepo = userMenuRepo;
            this.billTypeRepo = billTypeRepo;
        }

        public Task<int> CountAsync()
        {
            return Task.Run<int>(() =>
            {
                return billSettingsMRepo.CountAsync();
            });
        }

        public bool Delete(BILL_SETTINGSVM entity)
        {
            BILL_SETTINGS bs = new BILL_SETTINGS
            {
                ABRV_BILL = entity.ABRV_BILL,
                ACC_COST_ID = entity.ACC_COST_ID,
                ACC_DISC_ID = entity.ACC_DISC_ID,
                ACC_EXTRA_ID = entity.ACC_EXTRA_ID,
                ACC_GIFT_ID = entity.ACC_GIFT_ID,
                ACC_ITEM_ID = entity.ACC_ITEM_ID,
                ACC_PAY_ID = entity.ACC_PAY_ID,
                ACC_STORE_ID = entity.ACC_STORE_ID,
                AddedBy = entity.AddedBy,
                AddedOn = entity.AddedOn,
                ADD_BILL_ENTRY_WITH_THE_MAIN_CURRENCY_RATE = entity.ADD_BILL_ENTRY_WITH_THE_MAIN_CURRENCY_RATE,
                ALTERNATE_COLOR = entity.ALTERNATE_COLOR,
                AUTOMATIC_DISCOUNT = entity.AUTOMATIC_DISCOUNT,
                BILL_ABRV_AR = entity.BILL_ABRV_AR,
                BILL_ABRV_EN = entity.BILL_ABRV_EN,
                BILL_AR_NAME = entity.BILL_AR_NAME,
                BILL_EN_NAME = entity.BILL_EN_NAME,
                BILL_PAY_TYPE = entity.BILL_PAY_TYPE,
                BILL_SELL_TYPE_ID = entity.BILL_SELL_TYPE_ID,
                BILL_SETTING_ID = entity.BILL_SETTING_ID,
                BILL_SHORTCUT = entity.BILL_SHORTCUT,
                BILL_SHOW_NAME = entity.BILL_SHOW_NAME,
                BILL_TYPE_ID = entity.BILL_TYPE_ID,
                COM_STORE_ID = entity.COM_STORE_ID,
                CONTINEOUS_INVENTORY = entity.CONTINEOUS_INVENTORY,
                COST_CENTER_ID = entity.COST_CENTER_ID,
                CURRENCY_ID = entity.CURRENCY_ID,
                CURRENCY_RATE = entity.CURRENCY_RATE,
                DEFAULT_COLOR = entity.DEFAULT_COLOR,
                Disable = entity.Disable,
                DISABLE_BILL_PAY_TYPE = entity.DISABLE_BILL_PAY_TYPE,
                FIRSTBILLMESSAGE = entity.FIRSTBILLMESSAGE,
                FIRST_EXPIRE = entity.FIRST_EXPIRE,
                GENERATE_COST_ENTRY = entity.GENERATE_COST_ENTRY,
                GENERATE_ENTRY_STATE = entity.GENERATE_ENTRY_STATE,
                GST = entity.GST,
                GSTNAME = entity.GSTNAME,
                HEADERBILLMESSAGE = entity.HEADERBILLMESSAGE,
                IS_AUTO_POSTING = entity.IS_AUTO_POSTING,
                IS_IT_CASHER_BILL = entity.IS_IT_CASHER_BILL,
                IS_IT_SALES_POINT = entity.IS_IT_SALES_POINT,
                IS_IT_SERVICE_BILL = entity.IS_IT_SERVICE_BILL,
                IS_IT_TAX_SALE_BILL = entity.IS_IT_TAX_SALE_BILL,
                IS_IT_THREADING = entity.IS_IT_THREADING,
                LAST_PAY_PRICE = entity.LAST_PAY_PRICE,
                MIN_QTY = entity.MIN_QTY,
                MODULE_CARS = entity.MODULE_CARS,
                NUMBEROFCOPIES = entity.NUMBEROFCOPIES,
                OUT_MINUS = entity.OUT_MINUS,
                PRICE_COST_EFFECT = entity.PRICE_COST_EFFECT,
                PRINTHALFPAGE = entity.PRINTHALFPAGE,
                PRINTHALFPAGEBYLONG = entity.PRINTHALFPAGEBYLONG,
                PRINTLANDSCIP = entity.PRINTLANDSCIP,
                PRINT_BILL_AS_RESET_OR_AS_BILL = entity.PRINT_BILL_AS_RESET_OR_AS_BILL,
                PRINT_BILL_AUTOMATIC = entity.PRINT_BILL_AUTOMATIC,
                PST = entity.PST,
                PSTNAME = entity.PSTNAME,
                REPEATETHE_ITEM_AT_THE_BILL = entity.REPEATETHE_ITEM_AT_THE_BILL,
                SATTLEMENT_INCOME_LIST = entity.SATTLEMENT_INCOME_LIST,
                SEARCH_ONLY_BY_DEAULT_UNIT = entity.SEARCH_ONLY_BY_DEAULT_UNIT,
                SECONDBILLMESSAGE = entity.SECONDBILLMESSAGE,
                SERIAL_NUMBER = entity.SERIAL_NUMBER,
                SHOW_BILL_PAY_TYPE = entity.SHOW_BILL_PAY_TYPE,
                SHOW_COST_CENTER = entity.SHOW_COST_CENTER,
                SHOW_EMPLOYEE = entity.SHOW_EMPLOYEE,
                SHOW_THE_COMPANY_DATA_ON_BILL = entity.SHOW_THE_COMPANY_DATA_ON_BILL,
                SHOW_THE_CURRENCY = entity.ABRV_BILL,
                SHOW_THE_CURRENT_QTY_ON_BILL = entity.SHOW_THE_CURRENT_QTY_ON_BILL,
                SHOW_THE_ITEM_CODE_ON_BILL = entity.SHOW_THE_ITEM_CODE_ON_BILL,
                SHOW_THE_LAST_BALANCE_ON_THE_BILL = entity.SHOW_THE_LAST_BALANCE_ON_THE_BILL,
                SHOW_THE_LAST_DATE_FOR_RETURN = entity.SHOW_THE_LAST_DATE_FOR_RETURN,
                SHOW_THE_RETURN_PERCENTAGE = entity.SHOW_THE_RETURN_PERCENTAGE,
                STORE_EFFECT_STATE = entity.STORE_EFFECT_STATE,
                THESECONDCELLISTHEITEMCELLNOTTHEQUANTITY = entity.THESECONDCELLISTHEITEMCELLNOTTHEQUANTITY,
                TO_COM_STORE_ID = entity.TO_COM_STORE_ID,
                UpdatedBy = entity.UpdatedBy,
                UpdatedOn = entity.UpdatedOn,
                ALTERNATE_COLOR_HEXA = entity.ALTERNATE_COLOR_HEXA,
                DEFAULT_COLOR_HEXA = entity.DEFAULT_COLOR_HEXA,
                Tax = entity.Tax,
                CommissionTax = entity.CommissionTax,

                IsAccount = entity.IsAccount,
                IsBillDate = entity.IsBillDate,
                IsBillRemarks = entity.IsBillRemarks,
                IsCompStoreID = entity.IsCompStoreID,
                IsCurrency = entity.IsCurrency,
                IsCurrencyTrans = entity.IsCurrencyTrans,
                IsCustAccID = entity.IsCustAccID,
                IsEmpID = entity.IsEmpID,
                IsItemAccID = entity.IsItemAccID,
                IsPayTypes = entity.IsPayTypes,
                IsPayWay = entity.IsPayWay,
                IsSellType = entity.IsSellType,
                IsShiftNumber = entity.IsShiftNumber,
                BillAccountName = entity.BillAccountName,
                BillEmpName = entity.BillEmpName,

                IsToCompStore = entity.IsToCompStore,
                IsTotalExtra = entity.IsTotalExtra,
                IsTotalMustPaid = entity.IsTotalMustPaid,
                IsTotalPaid = entity.IsTotalPaid,
                //IsTotalPrice=entity.IsTotalPrice,
                IsTotalRemaining = entity.IsTotalRemaining,

                AccWagesID = entity.AccWagesID,
                IsItems = entity.IsItems,
                IsItemsGroups = entity.IsItemsGroups,

                AccSalesID = entity.AccSalesID,
                AccVatRateID = entity.AccVatRateID,
                MenuID = entity.MenuID,

                CashAccountID = entity.CashAccountID,
                VisaAccountID = entity.VisaAccountID,
                NoPaidAccountID = entity.NoPaidAccountID,
                PaymentAccountID = entity.PaymentAccountID,

                AccWagesTaxID = entity.AccWagesTaxID,
                WagesTaxValue = entity.WagesTaxValue,
                ShowPriceTypeID = entity.ShowPriceTypeID,

                PurchasAccID = entity.PurchasAccID,
                PurchasTaxAccID = entity.PurchasTaxAccID,

                AccCommissionID = entity.AccCommissionID,
                AccCommissionTaxID = entity.AccCommissionTaxID,

                AccSalesGoldID = entity.AccSalesGoldID,
                AccPurchaseGoldID = entity.AccPurchaseGoldID,
                IsQuantityCalculated = entity.IsQuantityCalculated,
                AccGoldID = entity.AccGoldID,
                IsShowDeliveryPerson = entity.IsShowDeliveryPerson,
                IsShowEditReason = entity.IsShowEditReason,

                IsEnableTaxEdit = entity.IsEnableTaxEdit,
                IsShowCustomerBalance = entity.IsShowCustomerBalance,
                IsShowExternalNumber = entity.IsShowExternalNumber,
                IsShowGoldBoxBalance = entity.IsShowGoldBoxBalance,
                BillRowsNumber = entity.BillRowsNumber,

                IsCalcClearnessRate = entity.IsCalcClearnessRate,

                IsBillDiscRate = entity.IsBillDiscRate,
                IsBillDiscValue = entity.IsBillDiscValue,
                IsTotalDiscRates = entity.IsTotalDiscRates,
                IsTotalDiscValues = entity.IsTotalDiscValues,

                BillVatRate = entity.BillVatRate,
                IsBillVatRate = entity.IsBillVatRate,
                IsCalcBillDiscOfTotal = entity.IsCalcBillDiscOfTotal,

                TimesNumberOfPrinting = entity.TimesNumberOfPrinting,
                IsRepeatItem = entity.IsRepeatItem,
                IsQuickAccount = entity.IsQuickAccount,
                IsEntryGoldItemsAccounts=entity.IsEntryGoldItemsAccounts,

                IsTotalsSummary=entity.IsTotalsSummary,

                ExemptVatRate=entity.ExemptVatRate,
                MainVatRate=entity.MainVatRate,

                IsShowTaxesBox=entity.IsShowTaxesBox,
                IsEnableWeight = entity.IsEnableWeight,

                IsEnableGmWages = entity.IsEnableGmWages,
                IsLockPrice = entity.IsLockPrice


            };
            billSettingsMRepo.Delete(bs, entity.BILL_SETTING_ID);
            return true;
        }

        public Task<bool> DeleteAsync(BILL_SETTINGSVM entity)
        {
            return Task.Run<bool>(() =>
            {

                UserMenu menu = userMenuRepo.Filter(u => u.BillSetiingID == entity.BILL_SETTING_ID).FirstOrDefault();
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
                    };
                    userMenuRepo.Delete(userMenu, menu.ID);
                }


                BILL_GRID_COLUMNS billgrdCol = billGrdMRepo.Filter(x => x.BILL_SETTING_ID == entity.BILL_SETTING_ID).FirstOrDefault();
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
                    billGrdMRepo.Delete(bgc, billgrdCol.BILL_GRID_ID);
                }

                BILL_SETTINGS bs = new BILL_SETTINGS
                {
                    ABRV_BILL = entity.ABRV_BILL,
                    ACC_COST_ID = entity.ACC_COST_ID,
                    ACC_DISC_ID = entity.ACC_DISC_ID,
                    ACC_EXTRA_ID = entity.ACC_EXTRA_ID,
                    ACC_GIFT_ID = entity.ACC_GIFT_ID,
                    ACC_ITEM_ID = entity.ACC_ITEM_ID,
                    ACC_PAY_ID = entity.ACC_PAY_ID,
                    ACC_STORE_ID = entity.ACC_STORE_ID,
                    AddedBy = entity.AddedBy,
                    AddedOn = entity.AddedOn,
                    ADD_BILL_ENTRY_WITH_THE_MAIN_CURRENCY_RATE = entity.ADD_BILL_ENTRY_WITH_THE_MAIN_CURRENCY_RATE,
                    ALTERNATE_COLOR = entity.ALTERNATE_COLOR,
                    AUTOMATIC_DISCOUNT = entity.AUTOMATIC_DISCOUNT,
                    BILL_ABRV_AR = entity.BILL_ABRV_AR,
                    BILL_ABRV_EN = entity.BILL_ABRV_EN,
                    BILL_AR_NAME = entity.BILL_AR_NAME,
                    BILL_EN_NAME = entity.BILL_EN_NAME,
                    BILL_PAY_TYPE = entity.BILL_PAY_TYPE,
                    BILL_SELL_TYPE_ID = entity.BILL_SELL_TYPE_ID,
                    BILL_SETTING_ID = entity.BILL_SETTING_ID,
                    BILL_SHORTCUT = entity.BILL_SHORTCUT,
                    BILL_SHOW_NAME = entity.BILL_SHOW_NAME,
                    BILL_TYPE_ID = entity.BILL_TYPE_ID,
                    COM_STORE_ID = entity.COM_STORE_ID,
                    CONTINEOUS_INVENTORY = entity.CONTINEOUS_INVENTORY,
                    COST_CENTER_ID = entity.COST_CENTER_ID,
                    CURRENCY_ID = entity.CURRENCY_ID,
                    CURRENCY_RATE = entity.CURRENCY_RATE,
                    DEFAULT_COLOR = entity.DEFAULT_COLOR,
                    Disable = entity.Disable,
                    DISABLE_BILL_PAY_TYPE = entity.DISABLE_BILL_PAY_TYPE,
                    FIRSTBILLMESSAGE = entity.FIRSTBILLMESSAGE,
                    FIRST_EXPIRE = entity.FIRST_EXPIRE,
                    GENERATE_COST_ENTRY = entity.GENERATE_COST_ENTRY,
                    GENERATE_ENTRY_STATE = entity.GENERATE_ENTRY_STATE,
                    GST = entity.GST,
                    GSTNAME = entity.GSTNAME,
                    HEADERBILLMESSAGE = entity.HEADERBILLMESSAGE,
                    IS_AUTO_POSTING = entity.IS_AUTO_POSTING,
                    IS_IT_CASHER_BILL = entity.IS_IT_CASHER_BILL,
                    IS_IT_SALES_POINT = entity.IS_IT_SALES_POINT,
                    IS_IT_SERVICE_BILL = entity.IS_IT_SERVICE_BILL,
                    IS_IT_TAX_SALE_BILL = entity.IS_IT_TAX_SALE_BILL,
                    IS_IT_THREADING = entity.IS_IT_THREADING,
                    LAST_PAY_PRICE = entity.LAST_PAY_PRICE,
                    MIN_QTY = entity.MIN_QTY,
                    MODULE_CARS = entity.MODULE_CARS,
                    NUMBEROFCOPIES = entity.NUMBEROFCOPIES,
                    OUT_MINUS = entity.OUT_MINUS,
                    PRICE_COST_EFFECT = entity.PRICE_COST_EFFECT,
                    PRINTHALFPAGE = entity.PRINTHALFPAGE,
                    PRINTHALFPAGEBYLONG = entity.PRINTHALFPAGEBYLONG,
                    PRINTLANDSCIP = entity.PRINTLANDSCIP,
                    PRINT_BILL_AS_RESET_OR_AS_BILL = entity.PRINT_BILL_AS_RESET_OR_AS_BILL,
                    PRINT_BILL_AUTOMATIC = entity.PRINT_BILL_AUTOMATIC,
                    PST = entity.PST,
                    PSTNAME = entity.PSTNAME,
                    REPEATETHE_ITEM_AT_THE_BILL = entity.REPEATETHE_ITEM_AT_THE_BILL,
                    SATTLEMENT_INCOME_LIST = entity.SATTLEMENT_INCOME_LIST,
                    SEARCH_ONLY_BY_DEAULT_UNIT = entity.SEARCH_ONLY_BY_DEAULT_UNIT,
                    SECONDBILLMESSAGE = entity.SECONDBILLMESSAGE,
                    SERIAL_NUMBER = entity.SERIAL_NUMBER,
                    SHOW_BILL_PAY_TYPE = entity.SHOW_BILL_PAY_TYPE,
                    SHOW_COST_CENTER = entity.SHOW_COST_CENTER,
                    SHOW_EMPLOYEE = entity.SHOW_EMPLOYEE,
                    SHOW_THE_COMPANY_DATA_ON_BILL = entity.SHOW_THE_COMPANY_DATA_ON_BILL,
                    SHOW_THE_CURRENCY = entity.ABRV_BILL,
                    SHOW_THE_CURRENT_QTY_ON_BILL = entity.SHOW_THE_CURRENT_QTY_ON_BILL,
                    SHOW_THE_ITEM_CODE_ON_BILL = entity.SHOW_THE_ITEM_CODE_ON_BILL,
                    SHOW_THE_LAST_BALANCE_ON_THE_BILL = entity.SHOW_THE_LAST_BALANCE_ON_THE_BILL,
                    SHOW_THE_LAST_DATE_FOR_RETURN = entity.SHOW_THE_LAST_DATE_FOR_RETURN,
                    SHOW_THE_RETURN_PERCENTAGE = entity.SHOW_THE_RETURN_PERCENTAGE,
                    STORE_EFFECT_STATE = entity.STORE_EFFECT_STATE,
                    THESECONDCELLISTHEITEMCELLNOTTHEQUANTITY = entity.THESECONDCELLISTHEITEMCELLNOTTHEQUANTITY,
                    TO_COM_STORE_ID = entity.TO_COM_STORE_ID,
                    UpdatedBy = entity.UpdatedBy,
                    UpdatedOn = entity.UpdatedOn,
                    ALTERNATE_COLOR_HEXA = entity.ALTERNATE_COLOR_HEXA,
                    DEFAULT_COLOR_HEXA = entity.DEFAULT_COLOR_HEXA,
                    Tax = entity.Tax,
                    CommissionTax = entity.CommissionTax,

                    IsAccount = entity.IsAccount,
                    IsBillDate = entity.IsBillDate,
                    IsBillRemarks = entity.IsBillRemarks,
                    IsCompStoreID = entity.IsCompStoreID,
                    IsCurrency = entity.IsCurrency,
                    IsCurrencyTrans = entity.IsCurrencyTrans,
                    IsCustAccID = entity.IsCustAccID,
                    IsEmpID = entity.IsEmpID,
                    IsItemAccID = entity.IsItemAccID,
                    IsPayTypes = entity.IsPayTypes,
                    IsPayWay = entity.IsPayWay,
                    IsSellType = entity.IsSellType,
                    IsShiftNumber = entity.IsShiftNumber,
                    BillAccountName = entity.BillAccountName,
                    BillEmpName = entity.BillEmpName,

                    IsToCompStore = entity.IsToCompStore,
                    IsTotalExtra = entity.IsTotalExtra,
                    IsTotalMustPaid = entity.IsTotalMustPaid,
                    IsTotalPaid = entity.IsTotalPaid,
                    // IsTotalPrice = entity.IsTotalPrice,
                    IsTotalRemaining = entity.IsTotalRemaining,

                    AccWagesID = entity.AccWagesID,
                    IsItems = entity.IsItems,
                    IsItemsGroups = entity.IsItemsGroups,

                    AccSalesID = entity.AccSalesID,
                    AccVatRateID = entity.AccVatRateID,
                    MenuID = entity.MenuID,


                    CashAccountID = entity.CashAccountID,
                    VisaAccountID = entity.VisaAccountID,
                    NoPaidAccountID = entity.NoPaidAccountID,
                    PaymentAccountID = entity.PaymentAccountID,

                    AccWagesTaxID = entity.AccWagesTaxID,
                    WagesTaxValue = entity.WagesTaxValue,
                    ShowPriceTypeID = entity.ShowPriceTypeID,

                    PurchasAccID = entity.PurchasAccID,
                    PurchasTaxAccID = entity.PurchasTaxAccID,

                    AccCommissionID = entity.AccCommissionID,
                    AccCommissionTaxID = entity.AccCommissionTaxID,

                    AccSalesGoldID = entity.AccSalesGoldID,
                    AccPurchaseGoldID = entity.AccPurchaseGoldID,
                    IsQuantityCalculated = entity.IsQuantityCalculated,
                    AccGoldID = entity.AccGoldID,
                    IsShowDeliveryPerson = entity.IsShowDeliveryPerson,
                    IsShowEditReason = entity.IsShowEditReason,

                    IsEnableTaxEdit = entity.IsEnableTaxEdit,
                    IsShowCustomerBalance = entity.IsShowCustomerBalance,
                    IsShowExternalNumber = entity.IsShowExternalNumber,
                    IsShowGoldBoxBalance = entity.IsShowGoldBoxBalance,
                    BillRowsNumber = entity.BillRowsNumber,

                    IsCalcClearnessRate = entity.IsCalcClearnessRate,
                    IsBillDiscRate = entity.IsBillDiscRate,
                    IsBillDiscValue = entity.IsBillDiscValue,
                    IsTotalDiscRates = entity.IsTotalDiscRates,
                    IsTotalDiscValues = entity.IsTotalDiscValues,

                    BillVatRate = entity.BillVatRate,
                    IsBillVatRate = entity.IsBillVatRate,
                    IsCalcBillDiscOfTotal = entity.IsCalcBillDiscOfTotal,

                    TimesNumberOfPrinting = entity.TimesNumberOfPrinting,
                    IsRepeatItem = entity.IsRepeatItem,
                    IsQuickAccount = entity.IsQuickAccount,
                    IsEntryGoldItemsAccounts = entity.IsEntryGoldItemsAccounts,

                    IsTotalsSummary = entity.IsTotalsSummary,
                    ExemptVatRate = entity.ExemptVatRate,
                    MainVatRate = entity.MainVatRate,

                    IsShowTaxesBox = entity.IsShowTaxesBox,
                    IsEnableWeight = entity.IsEnableWeight,
                    IsEnableGmWages = entity.IsEnableGmWages,
                    IsLockPrice = entity.IsLockPrice


                };
                billSettingsMRepo.Delete(bs, entity.BILL_SETTING_ID);
                return true;
            });
        }

        public void Dispose()
        {
            billSettingsMRepo.Dispose();
            billGrdMRepo.Dispose();
            userMenuRepo.Dispose();
        }

        public Task<List<BILL_SETTINGSVM>> GetAllAsync(int pageNum, int pageSize)
        {
            return Task.Run<List<BILL_SETTINGSVM>>(() =>
            {
                int rowCount;
                var q = from entity in billSettingsMRepo.GetPaged<long>(pageNum, pageSize, p => p.BILL_SETTING_ID, false, out rowCount)
                        join billGrd in billGrdMRepo.GetPaged<int>(pageNum, pageSize, z => z.BILL_SETTING_ID, false, out rowCount) on entity.BILL_SETTING_ID equals billGrd.BILL_SETTING_ID into g
                        from bilGrid in g.DefaultIfEmpty()
                        select new BILL_SETTINGSVM
                        {
                            ABRV_BILL = entity.ABRV_BILL,
                            ACC_COST_ID = entity.ACC_COST_ID,
                            ACC_DISC_ID = entity.ACC_DISC_ID,
                            ACC_EXTRA_ID = entity.ACC_EXTRA_ID,
                            ACC_GIFT_ID = entity.ACC_GIFT_ID,
                            ACC_ITEM_ID = entity.ACC_ITEM_ID,
                            ACC_PAY_ID = entity.ACC_PAY_ID,
                            ACC_STORE_ID = entity.ACC_STORE_ID,
                            AddedBy = entity.AddedBy,
                            AddedOn = entity.AddedOn,
                            ADD_BILL_ENTRY_WITH_THE_MAIN_CURRENCY_RATE = entity.ADD_BILL_ENTRY_WITH_THE_MAIN_CURRENCY_RATE,
                            ALTERNATE_COLOR = entity.ALTERNATE_COLOR,
                            AUTOMATIC_DISCOUNT = entity.AUTOMATIC_DISCOUNT,
                            BILL_ABRV_AR = entity.BILL_ABRV_AR,
                            BILL_ABRV_EN = entity.BILL_ABRV_EN,
                            BILL_AR_NAME = entity.BILL_AR_NAME,
                            BILL_EN_NAME = entity.BILL_EN_NAME,
                            BILL_PAY_TYPE = entity.BILL_PAY_TYPE,
                            BILL_SELL_TYPE_ID = entity.BILL_SELL_TYPE_ID,
                            BILL_SETTING_ID = entity.BILL_SETTING_ID,
                            BILL_SHORTCUT = entity.BILL_SHORTCUT,
                            BILL_SHOW_NAME = entity.BILL_SHOW_NAME,
                            BILL_TYPE_ID = entity.BILL_TYPE_ID,
                            COM_STORE_ID = entity.COM_STORE_ID,
                            CONTINEOUS_INVENTORY = entity.CONTINEOUS_INVENTORY,
                            COST_CENTER_ID = entity.COST_CENTER_ID,
                            CURRENCY_ID = entity.CURRENCY_ID,
                            CURRENCY_RATE = entity.CURRENCY_RATE,
                            DEFAULT_COLOR = entity.DEFAULT_COLOR,
                            Disable = entity.Disable,
                            DISABLE_BILL_PAY_TYPE = entity.DISABLE_BILL_PAY_TYPE,
                            FIRSTBILLMESSAGE = entity.FIRSTBILLMESSAGE,
                            FIRST_EXPIRE = entity.FIRST_EXPIRE,
                            GENERATE_COST_ENTRY = entity.GENERATE_COST_ENTRY,
                            GENERATE_ENTRY_STATE = entity.GENERATE_ENTRY_STATE,
                            GST = entity.GST,
                            GSTNAME = entity.GSTNAME,
                            HEADERBILLMESSAGE = entity.HEADERBILLMESSAGE,
                            IS_AUTO_POSTING = entity.IS_AUTO_POSTING,
                            IS_IT_CASHER_BILL = entity.IS_IT_CASHER_BILL,
                            IS_IT_SALES_POINT = entity.IS_IT_SALES_POINT,
                            IS_IT_SERVICE_BILL = entity.IS_IT_SERVICE_BILL,
                            IS_IT_TAX_SALE_BILL = entity.IS_IT_TAX_SALE_BILL,
                            IS_IT_THREADING = entity.IS_IT_THREADING,
                            LAST_PAY_PRICE = entity.LAST_PAY_PRICE,
                            MIN_QTY = entity.MIN_QTY,
                            MODULE_CARS = entity.MODULE_CARS,
                            NUMBEROFCOPIES = entity.NUMBEROFCOPIES,
                            OUT_MINUS = entity.OUT_MINUS,
                            PRICE_COST_EFFECT = entity.PRICE_COST_EFFECT,
                            PRINTHALFPAGE = entity.PRINTHALFPAGE,
                            PRINTHALFPAGEBYLONG = entity.PRINTHALFPAGEBYLONG,
                            PRINTLANDSCIP = entity.PRINTLANDSCIP,
                            PRINT_BILL_AS_RESET_OR_AS_BILL = entity.PRINT_BILL_AS_RESET_OR_AS_BILL,
                            PRINT_BILL_AUTOMATIC = entity.PRINT_BILL_AUTOMATIC,
                            PST = entity.PST,
                            PSTNAME = entity.PSTNAME,
                            REPEATETHE_ITEM_AT_THE_BILL = entity.REPEATETHE_ITEM_AT_THE_BILL,
                            SATTLEMENT_INCOME_LIST = entity.SATTLEMENT_INCOME_LIST,
                            SEARCH_ONLY_BY_DEAULT_UNIT = entity.SEARCH_ONLY_BY_DEAULT_UNIT,
                            SECONDBILLMESSAGE = entity.SECONDBILLMESSAGE,
                            SERIAL_NUMBER = entity.SERIAL_NUMBER,
                            SHOW_BILL_PAY_TYPE = entity.SHOW_BILL_PAY_TYPE,
                            SHOW_COST_CENTER = entity.SHOW_COST_CENTER,
                            SHOW_EMPLOYEE = entity.SHOW_EMPLOYEE,
                            SHOW_THE_COMPANY_DATA_ON_BILL = entity.SHOW_THE_COMPANY_DATA_ON_BILL,
                            SHOW_THE_CURRENCY = entity.ABRV_BILL,
                            SHOW_THE_CURRENT_QTY_ON_BILL = entity.SHOW_THE_CURRENT_QTY_ON_BILL,
                            SHOW_THE_ITEM_CODE_ON_BILL = entity.SHOW_THE_ITEM_CODE_ON_BILL,
                            SHOW_THE_LAST_BALANCE_ON_THE_BILL = entity.SHOW_THE_LAST_BALANCE_ON_THE_BILL,
                            SHOW_THE_LAST_DATE_FOR_RETURN = entity.SHOW_THE_LAST_DATE_FOR_RETURN,
                            SHOW_THE_RETURN_PERCENTAGE = entity.SHOW_THE_RETURN_PERCENTAGE,
                            STORE_EFFECT_STATE = entity.STORE_EFFECT_STATE,
                            THESECONDCELLISTHEITEMCELLNOTTHEQUANTITY = entity.THESECONDCELLISTHEITEMCELLNOTTHEQUANTITY,
                            TO_COM_STORE_ID = entity.TO_COM_STORE_ID,
                            UpdatedBy = entity.UpdatedBy,
                            UpdatedOn = entity.UpdatedOn,
                            BillGrdCol = bilGrid,
                            ALTERNATE_COLOR_HEXA = entity.ALTERNATE_COLOR_HEXA,
                            DEFAULT_COLOR_HEXA = entity.DEFAULT_COLOR_HEXA,
                            Tax = entity.Tax,
                            CommissionTax = entity.CommissionTax,

                            IsAccount = entity.IsAccount,
                            IsBillDate = entity.IsBillDate,
                            IsBillRemarks = entity.IsBillRemarks,
                            IsCompStoreID = entity.IsCompStoreID,
                            IsCurrency = entity.IsCurrency,
                            IsCurrencyTrans = entity.IsCurrencyTrans,
                            IsCustAccID = entity.IsCustAccID,
                            IsEmpID = entity.IsEmpID,
                            IsItemAccID = entity.IsItemAccID,
                            IsPayTypes = entity.IsPayTypes,
                            IsPayWay = entity.IsPayWay,
                            IsSellType = entity.IsSellType,
                            IsShiftNumber = entity.IsShiftNumber,
                            BillAccountName = entity.BillAccountName,
                            BillEmpName = entity.BillEmpName,

                            IsToCompStore = entity.IsToCompStore,
                            IsTotalExtra = entity.IsTotalExtra,
                            IsTotalMustPaid = entity.IsTotalMustPaid,
                            IsTotalPaid = entity.IsTotalPaid,
                            //  IsTotalPrice = entity.IsTotalPrice,
                            IsTotalRemaining = entity.IsTotalRemaining,

                            AccWagesID = entity.AccWagesID,
                            IsItems = entity.IsItems,
                            IsItemsGroups = entity.IsItemsGroups,

                            AccSalesID = entity.AccSalesID,
                            AccVatRateID = entity.AccVatRateID,
                            MenuID = entity.MenuID,


                            CashAccountID = entity.CashAccountID,
                            VisaAccountID = entity.VisaAccountID,
                            NoPaidAccountID = entity.NoPaidAccountID,
                            PaymentAccountID = entity.PaymentAccountID,

                            AccWagesTaxID = entity.AccWagesTaxID,
                            WagesTaxValue = entity.WagesTaxValue,
                            ShowPriceTypeID = entity.ShowPriceTypeID,

                            Show_Wages_Discount=entity.Show_Wages_Discount,

                            PurchasAccID = entity.PurchasAccID,
                            PurchasTaxAccID = entity.PurchasTaxAccID,

                            AccCommissionID = entity.AccCommissionID,
                            AccCommissionTaxID = entity.AccCommissionTaxID,

                            AccSalesGoldID = entity.AccSalesGoldID,
                            AccPurchaseGoldID = entity.AccPurchaseGoldID,
                            IsQuantityCalculated = entity.IsQuantityCalculated,
                            AccGoldID = entity.AccGoldID,
                            IsShowDeliveryPerson = entity.IsShowDeliveryPerson,
                            IsShowEditReason = entity.IsShowEditReason,

                            IsEnableTaxEdit = entity.IsEnableTaxEdit,
                            IsShowCustomerBalance = entity.IsShowCustomerBalance,
                            IsShowExternalNumber = entity.IsShowExternalNumber,
                            IsShowGoldBoxBalance = entity.IsShowGoldBoxBalance,
                            BillRowsNumber = entity.BillRowsNumber,

                            IsCalcClearnessRate = entity.IsCalcClearnessRate,
                            IsBillDiscRate = entity.IsBillDiscRate,
                            IsBillDiscValue = entity.IsBillDiscValue,
                            IsTotalDiscRates = entity.IsTotalDiscRates,
                            IsTotalDiscValues = entity.IsTotalDiscValues,

                            BillVatRate = entity.BillVatRate,
                            IsBillVatRate = entity.IsBillVatRate,
                            IsCalcBillDiscOfTotal = entity.IsCalcBillDiscOfTotal,

                            TimesNumberOfPrinting = entity.TimesNumberOfPrinting,
                            IsRepeatItem = entity.IsRepeatItem,
                            IsQuickAccount = entity.IsQuickAccount,
                            IsEntryGoldItemsAccounts = entity.IsEntryGoldItemsAccounts,

                            IsTotalsSummary = entity.IsTotalsSummary,
                            ExemptVatRate = entity.ExemptVatRate,
                            MainVatRate = entity.MainVatRate,

                            IsShowTaxesBox = entity.IsShowTaxesBox,
                            IsEnableWeight = entity.IsEnableWeight,
                            IsEnableGmWages = entity.IsEnableGmWages,
                            IsLockPrice = entity.IsLockPrice

                        };
                return q.ToList();
            });
        }
        
        public BILL_SETTINGSVM GetBillSettingByBillID(int id)
        {
            var q = from entity in billSettingsMRepo.GetAll().Where(a=>a.BILL_SETTING_ID==id)
                    select new BILL_SETTINGSVM
                    {
                        ABRV_BILL = entity.ABRV_BILL,
                        ACC_COST_ID = entity.ACC_COST_ID,
                        ACC_DISC_ID = entity.ACC_DISC_ID,
                        ACC_EXTRA_ID = entity.ACC_EXTRA_ID,
                        ACC_GIFT_ID = entity.ACC_GIFT_ID,
                        ACC_ITEM_ID = entity.ACC_ITEM_ID,
                        ACC_PAY_ID = entity.ACC_PAY_ID,
                        ACC_STORE_ID = entity.ACC_STORE_ID,
                        AddedBy = entity.AddedBy,
                        AddedOn = entity.AddedOn,
                        ADD_BILL_ENTRY_WITH_THE_MAIN_CURRENCY_RATE = entity.ADD_BILL_ENTRY_WITH_THE_MAIN_CURRENCY_RATE,
                        ALTERNATE_COLOR = entity.ALTERNATE_COLOR,
                        AUTOMATIC_DISCOUNT = entity.AUTOMATIC_DISCOUNT,
                        BILL_ABRV_AR = entity.BILL_ABRV_AR,
                        BILL_ABRV_EN = entity.BILL_ABRV_EN,
                        BILL_AR_NAME = entity.BILL_AR_NAME,
                        BILL_EN_NAME = entity.BILL_EN_NAME,
                        BILL_PAY_TYPE = entity.BILL_PAY_TYPE,
                        BILL_SELL_TYPE_ID = entity.BILL_SELL_TYPE_ID,
                        BILL_SETTING_ID = entity.BILL_SETTING_ID,
                        BILL_SHORTCUT = entity.BILL_SHORTCUT,
                        BILL_SHOW_NAME = entity.BILL_SHOW_NAME,
                        BILL_TYPE_ID = entity.BILL_TYPE_ID,
                        COM_STORE_ID = entity.COM_STORE_ID,
                        CONTINEOUS_INVENTORY = entity.CONTINEOUS_INVENTORY,
                        COST_CENTER_ID = entity.COST_CENTER_ID,
                        CURRENCY_ID = entity.CURRENCY_ID,
                        CURRENCY_RATE = entity.CURRENCY_RATE,
                        DEFAULT_COLOR = entity.DEFAULT_COLOR,
                        Disable = entity.Disable,
                        DISABLE_BILL_PAY_TYPE = entity.DISABLE_BILL_PAY_TYPE,
                        FIRSTBILLMESSAGE = entity.FIRSTBILLMESSAGE,
                        FIRST_EXPIRE = entity.FIRST_EXPIRE,
                        GENERATE_COST_ENTRY = entity.GENERATE_COST_ENTRY,
                        GENERATE_ENTRY_STATE = entity.GENERATE_ENTRY_STATE,
                        GST = entity.GST,
                        GSTNAME = entity.GSTNAME,
                        HEADERBILLMESSAGE = entity.HEADERBILLMESSAGE,
                        IS_AUTO_POSTING = entity.IS_AUTO_POSTING,
                        IS_IT_CASHER_BILL = entity.IS_IT_CASHER_BILL,
                        IS_IT_SALES_POINT = entity.IS_IT_SALES_POINT,
                        IS_IT_SERVICE_BILL = entity.IS_IT_SERVICE_BILL,
                        IS_IT_TAX_SALE_BILL = entity.IS_IT_TAX_SALE_BILL,
                        IS_IT_THREADING = entity.IS_IT_THREADING,
                        LAST_PAY_PRICE = entity.LAST_PAY_PRICE,
                        MIN_QTY = entity.MIN_QTY,
                        MODULE_CARS = entity.MODULE_CARS,
                        NUMBEROFCOPIES = entity.NUMBEROFCOPIES,
                        OUT_MINUS = entity.OUT_MINUS,
                        PRICE_COST_EFFECT = entity.PRICE_COST_EFFECT,
                        PRINTHALFPAGE = entity.PRINTHALFPAGE,
                        PRINTHALFPAGEBYLONG = entity.PRINTHALFPAGEBYLONG,
                        PRINTLANDSCIP = entity.PRINTLANDSCIP,
                        PRINT_BILL_AS_RESET_OR_AS_BILL = entity.PRINT_BILL_AS_RESET_OR_AS_BILL,
                        PRINT_BILL_AUTOMATIC = entity.PRINT_BILL_AUTOMATIC,
                        PST = entity.PST,
                        PSTNAME = entity.PSTNAME,
                        REPEATETHE_ITEM_AT_THE_BILL = entity.REPEATETHE_ITEM_AT_THE_BILL,
                        SATTLEMENT_INCOME_LIST = entity.SATTLEMENT_INCOME_LIST,
                        SEARCH_ONLY_BY_DEAULT_UNIT = entity.SEARCH_ONLY_BY_DEAULT_UNIT,
                        SECONDBILLMESSAGE = entity.SECONDBILLMESSAGE,
                        SERIAL_NUMBER = entity.SERIAL_NUMBER,
                        SHOW_BILL_PAY_TYPE = entity.SHOW_BILL_PAY_TYPE,
                        SHOW_COST_CENTER = entity.SHOW_COST_CENTER,
                        SHOW_EMPLOYEE = entity.SHOW_EMPLOYEE,
                        SHOW_THE_COMPANY_DATA_ON_BILL = entity.SHOW_THE_COMPANY_DATA_ON_BILL,
                        SHOW_THE_CURRENCY = entity.ABRV_BILL,
                        SHOW_THE_CURRENT_QTY_ON_BILL = entity.SHOW_THE_CURRENT_QTY_ON_BILL,
                        SHOW_THE_ITEM_CODE_ON_BILL = entity.SHOW_THE_ITEM_CODE_ON_BILL,
                        SHOW_THE_LAST_BALANCE_ON_THE_BILL = entity.SHOW_THE_LAST_BALANCE_ON_THE_BILL,
                        SHOW_THE_LAST_DATE_FOR_RETURN = entity.SHOW_THE_LAST_DATE_FOR_RETURN,
                        SHOW_THE_RETURN_PERCENTAGE = entity.SHOW_THE_RETURN_PERCENTAGE,
                        STORE_EFFECT_STATE = entity.STORE_EFFECT_STATE,
                        THESECONDCELLISTHEITEMCELLNOTTHEQUANTITY = entity.THESECONDCELLISTHEITEMCELLNOTTHEQUANTITY,
                        TO_COM_STORE_ID = entity.TO_COM_STORE_ID,
                        UpdatedBy = entity.UpdatedBy,
                        UpdatedOn = entity.UpdatedOn,
                        ALTERNATE_COLOR_HEXA = entity.ALTERNATE_COLOR_HEXA,
                        DEFAULT_COLOR_HEXA = entity.DEFAULT_COLOR_HEXA,
                        Tax = entity.Tax,
                        CommissionTax = entity.CommissionTax,

                        IsAccount = entity.IsAccount,
                        IsBillDate = entity.IsBillDate,
                        IsBillRemarks = entity.IsBillRemarks,
                        IsCompStoreID = entity.IsCompStoreID,
                        IsCurrency = entity.IsCurrency,
                        IsCurrencyTrans = entity.IsCurrencyTrans,
                        IsCustAccID = entity.IsCustAccID,
                        IsEmpID = entity.IsEmpID,
                        IsItemAccID = entity.IsItemAccID,
                        IsPayTypes = entity.IsPayTypes,
                        IsPayWay = entity.IsPayWay,
                        IsSellType = entity.IsSellType,
                        IsShiftNumber = entity.IsShiftNumber,
                        BillAccountName = entity.BillAccountName,
                        BillEmpName = entity.BillEmpName,

                        IsToCompStore = entity.IsToCompStore,
                        IsTotalExtra = entity.IsTotalExtra,
                        IsTotalMustPaid = entity.IsTotalMustPaid,
                        IsTotalPaid = entity.IsTotalPaid,
                        //  IsTotalPrice = entity.IsTotalPrice,
                        IsTotalRemaining = entity.IsTotalRemaining,

                        AccWagesID = entity.AccWagesID,
                        IsItems = entity.IsItems,
                        IsItemsGroups = entity.IsItemsGroups,

                        AccSalesID = entity.AccSalesID,
                        AccVatRateID = entity.AccVatRateID,
                        MenuID = entity.MenuID,


                        CashAccountID = entity.CashAccountID,
                        VisaAccountID = entity.VisaAccountID,
                        NoPaidAccountID = entity.NoPaidAccountID,
                        PaymentAccountID = entity.PaymentAccountID,

                        AccWagesTaxID = entity.AccWagesTaxID,
                        WagesTaxValue = entity.WagesTaxValue,
                        ShowPriceTypeID = entity.ShowPriceTypeID,

                        PurchasAccID = entity.PurchasAccID,
                        PurchasTaxAccID = entity.PurchasTaxAccID,

                        AccCommissionID = entity.AccCommissionID,
                        AccCommissionTaxID = entity.AccCommissionTaxID,

                        AccSalesGoldID = entity.AccSalesGoldID,
                        AccPurchaseGoldID = entity.AccPurchaseGoldID,
                        IsQuantityCalculated = entity.IsQuantityCalculated,
                        AccGoldID = entity.AccGoldID,
                        IsShowDeliveryPerson = entity.IsShowDeliveryPerson,
                        IsShowEditReason = entity.IsShowEditReason,

                        IsEnableTaxEdit = entity.IsEnableTaxEdit,
                        IsShowCustomerBalance = entity.IsShowCustomerBalance,
                        IsShowExternalNumber = entity.IsShowExternalNumber,
                        IsShowGoldBoxBalance = entity.IsShowGoldBoxBalance,
                        BillRowsNumber = entity.BillRowsNumber,

                        IsCalcClearnessRate = entity.IsCalcClearnessRate,
                        IsBillDiscRate = entity.IsBillDiscRate,
                        IsBillDiscValue = entity.IsBillDiscValue,
                        IsTotalDiscRates = entity.IsTotalDiscRates,
                        IsTotalDiscValues = entity.IsTotalDiscValues,

                        BillVatRate = entity.BillVatRate,
                        IsBillVatRate = entity.IsBillVatRate,
                        IsCalcBillDiscOfTotal = entity.IsCalcBillDiscOfTotal,

                        TimesNumberOfPrinting = entity.TimesNumberOfPrinting,
                        IsRepeatItem = entity.IsRepeatItem,
                        IsQuickAccount = entity.IsQuickAccount,
                        IsEntryGoldItemsAccounts = entity.IsEntryGoldItemsAccounts,

                        IsTotalsSummary = entity.IsTotalsSummary,
                        ExemptVatRate = entity.ExemptVatRate,
                        MainVatRate = entity.MainVatRate,

                        IsShowTaxesBox = entity.IsShowTaxesBox,
                        IsEnableWeight = entity.IsEnableWeight,
                        IsEnableGmWages = entity.IsEnableGmWages,
                        IsLockPrice = entity.IsLockPrice

                    };
            return q.FirstOrDefault();
        }
        public List<BILL_SETTINGSVM> GetAll()
        {
            var q = from entity in billSettingsMRepo.GetAll()
                    select new BILL_SETTINGSVM
                    {
                        ABRV_BILL = entity.ABRV_BILL,
                        ACC_COST_ID = entity.ACC_COST_ID,
                        ACC_DISC_ID = entity.ACC_DISC_ID,
                        ACC_EXTRA_ID = entity.ACC_EXTRA_ID,
                        ACC_GIFT_ID = entity.ACC_GIFT_ID,
                        ACC_ITEM_ID = entity.ACC_ITEM_ID,
                        ACC_PAY_ID = entity.ACC_PAY_ID,
                        ACC_STORE_ID = entity.ACC_STORE_ID,
                        AddedBy = entity.AddedBy,
                        AddedOn = entity.AddedOn,
                        ADD_BILL_ENTRY_WITH_THE_MAIN_CURRENCY_RATE = entity.ADD_BILL_ENTRY_WITH_THE_MAIN_CURRENCY_RATE,
                        ALTERNATE_COLOR = entity.ALTERNATE_COLOR,
                        AUTOMATIC_DISCOUNT = entity.AUTOMATIC_DISCOUNT,
                        BILL_ABRV_AR = entity.BILL_ABRV_AR,
                        BILL_ABRV_EN = entity.BILL_ABRV_EN,
                        BILL_AR_NAME = entity.BILL_AR_NAME,
                        BILL_EN_NAME = entity.BILL_EN_NAME,
                        BILL_PAY_TYPE = entity.BILL_PAY_TYPE,
                        BILL_SELL_TYPE_ID = entity.BILL_SELL_TYPE_ID,
                        BILL_SETTING_ID = entity.BILL_SETTING_ID,
                        BILL_SHORTCUT = entity.BILL_SHORTCUT,
                        BILL_SHOW_NAME = entity.BILL_SHOW_NAME,
                        BILL_TYPE_ID = entity.BILL_TYPE_ID,
                        COM_STORE_ID = entity.COM_STORE_ID,
                        CONTINEOUS_INVENTORY = entity.CONTINEOUS_INVENTORY,
                        COST_CENTER_ID = entity.COST_CENTER_ID,
                        CURRENCY_ID = entity.CURRENCY_ID,
                        CURRENCY_RATE = entity.CURRENCY_RATE,
                        DEFAULT_COLOR = entity.DEFAULT_COLOR,
                        Disable = entity.Disable,
                        DISABLE_BILL_PAY_TYPE = entity.DISABLE_BILL_PAY_TYPE,
                        FIRSTBILLMESSAGE = entity.FIRSTBILLMESSAGE,
                        FIRST_EXPIRE = entity.FIRST_EXPIRE,
                        GENERATE_COST_ENTRY = entity.GENERATE_COST_ENTRY,
                        GENERATE_ENTRY_STATE = entity.GENERATE_ENTRY_STATE,
                        GST = entity.GST,
                        GSTNAME = entity.GSTNAME,
                        HEADERBILLMESSAGE = entity.HEADERBILLMESSAGE,
                        IS_AUTO_POSTING = entity.IS_AUTO_POSTING,
                        IS_IT_CASHER_BILL = entity.IS_IT_CASHER_BILL,
                        IS_IT_SALES_POINT = entity.IS_IT_SALES_POINT,
                        IS_IT_SERVICE_BILL = entity.IS_IT_SERVICE_BILL,
                        IS_IT_TAX_SALE_BILL = entity.IS_IT_TAX_SALE_BILL,
                        IS_IT_THREADING = entity.IS_IT_THREADING,
                        LAST_PAY_PRICE = entity.LAST_PAY_PRICE,
                        MIN_QTY = entity.MIN_QTY,
                        MODULE_CARS = entity.MODULE_CARS,
                        NUMBEROFCOPIES = entity.NUMBEROFCOPIES,
                        OUT_MINUS = entity.OUT_MINUS,
                        PRICE_COST_EFFECT = entity.PRICE_COST_EFFECT,
                        PRINTHALFPAGE = entity.PRINTHALFPAGE,
                        PRINTHALFPAGEBYLONG = entity.PRINTHALFPAGEBYLONG,
                        PRINTLANDSCIP = entity.PRINTLANDSCIP,
                        PRINT_BILL_AS_RESET_OR_AS_BILL = entity.PRINT_BILL_AS_RESET_OR_AS_BILL,
                        PRINT_BILL_AUTOMATIC = entity.PRINT_BILL_AUTOMATIC,
                        PST = entity.PST,
                        PSTNAME = entity.PSTNAME,
                        REPEATETHE_ITEM_AT_THE_BILL = entity.REPEATETHE_ITEM_AT_THE_BILL,
                        SATTLEMENT_INCOME_LIST = entity.SATTLEMENT_INCOME_LIST,
                        SEARCH_ONLY_BY_DEAULT_UNIT = entity.SEARCH_ONLY_BY_DEAULT_UNIT,
                        SECONDBILLMESSAGE = entity.SECONDBILLMESSAGE,
                        SERIAL_NUMBER = entity.SERIAL_NUMBER,
                        SHOW_BILL_PAY_TYPE = entity.SHOW_BILL_PAY_TYPE,
                        SHOW_COST_CENTER = entity.SHOW_COST_CENTER,
                        SHOW_EMPLOYEE = entity.SHOW_EMPLOYEE,
                        SHOW_THE_COMPANY_DATA_ON_BILL = entity.SHOW_THE_COMPANY_DATA_ON_BILL,
                        SHOW_THE_CURRENCY = entity.ABRV_BILL,
                        SHOW_THE_CURRENT_QTY_ON_BILL = entity.SHOW_THE_CURRENT_QTY_ON_BILL,
                        SHOW_THE_ITEM_CODE_ON_BILL = entity.SHOW_THE_ITEM_CODE_ON_BILL,
                        SHOW_THE_LAST_BALANCE_ON_THE_BILL = entity.SHOW_THE_LAST_BALANCE_ON_THE_BILL,
                        SHOW_THE_LAST_DATE_FOR_RETURN = entity.SHOW_THE_LAST_DATE_FOR_RETURN,
                        SHOW_THE_RETURN_PERCENTAGE = entity.SHOW_THE_RETURN_PERCENTAGE,
                        STORE_EFFECT_STATE = entity.STORE_EFFECT_STATE,
                        THESECONDCELLISTHEITEMCELLNOTTHEQUANTITY = entity.THESECONDCELLISTHEITEMCELLNOTTHEQUANTITY,
                        TO_COM_STORE_ID = entity.TO_COM_STORE_ID,
                        UpdatedBy = entity.UpdatedBy,
                        UpdatedOn = entity.UpdatedOn,
                        ALTERNATE_COLOR_HEXA = entity.ALTERNATE_COLOR_HEXA,
                        DEFAULT_COLOR_HEXA = entity.DEFAULT_COLOR_HEXA,
                        Tax = entity.Tax,
                        CommissionTax = entity.CommissionTax,

                        IsAccount = entity.IsAccount,
                        IsBillDate = entity.IsBillDate,
                        IsBillRemarks = entity.IsBillRemarks,
                        IsCompStoreID = entity.IsCompStoreID,
                        IsCurrency = entity.IsCurrency,
                        IsCurrencyTrans = entity.IsCurrencyTrans,
                        IsCustAccID = entity.IsCustAccID,
                        IsEmpID = entity.IsEmpID,
                        IsItemAccID = entity.IsItemAccID,
                        IsPayTypes = entity.IsPayTypes,
                        IsPayWay = entity.IsPayWay,
                        IsSellType = entity.IsSellType,
                        IsShiftNumber = entity.IsShiftNumber,
                        BillAccountName = entity.BillAccountName,
                        BillEmpName = entity.BillEmpName,

                        IsToCompStore = entity.IsToCompStore,
                        IsTotalExtra = entity.IsTotalExtra,
                        IsTotalMustPaid = entity.IsTotalMustPaid,
                        IsTotalPaid = entity.IsTotalPaid,
                        //  IsTotalPrice = entity.IsTotalPrice,
                        IsTotalRemaining = entity.IsTotalRemaining,

                        AccWagesID = entity.AccWagesID,
                        IsItems = entity.IsItems,
                        IsItemsGroups = entity.IsItemsGroups,

                        AccSalesID = entity.AccSalesID,
                        AccVatRateID = entity.AccVatRateID,
                        MenuID = entity.MenuID,


                        CashAccountID = entity.CashAccountID,
                        VisaAccountID = entity.VisaAccountID,
                        NoPaidAccountID = entity.NoPaidAccountID,
                        PaymentAccountID = entity.PaymentAccountID,

                        AccWagesTaxID = entity.AccWagesTaxID,
                        WagesTaxValue = entity.WagesTaxValue,
                        ShowPriceTypeID = entity.ShowPriceTypeID,

                        PurchasAccID = entity.PurchasAccID,
                        PurchasTaxAccID = entity.PurchasTaxAccID,

                        AccCommissionID = entity.AccCommissionID,
                        AccCommissionTaxID = entity.AccCommissionTaxID,

                        AccSalesGoldID = entity.AccSalesGoldID,
                        AccPurchaseGoldID = entity.AccPurchaseGoldID,
                        IsQuantityCalculated = entity.IsQuantityCalculated,
                        AccGoldID = entity.AccGoldID,
                        IsShowDeliveryPerson = entity.IsShowDeliveryPerson,
                        IsShowEditReason = entity.IsShowEditReason,

                        IsEnableTaxEdit = entity.IsEnableTaxEdit,
                        IsShowCustomerBalance = entity.IsShowCustomerBalance,
                        IsShowExternalNumber = entity.IsShowExternalNumber,
                        IsShowGoldBoxBalance = entity.IsShowGoldBoxBalance,
                        BillRowsNumber = entity.BillRowsNumber,

                        IsCalcClearnessRate = entity.IsCalcClearnessRate,
                        IsBillDiscRate = entity.IsBillDiscRate,
                        IsBillDiscValue = entity.IsBillDiscValue,
                        IsTotalDiscRates = entity.IsTotalDiscRates,
                        IsTotalDiscValues = entity.IsTotalDiscValues,

                        BillVatRate = entity.BillVatRate,
                        IsBillVatRate = entity.IsBillVatRate,
                        IsCalcBillDiscOfTotal = entity.IsCalcBillDiscOfTotal,

                        TimesNumberOfPrinting = entity.TimesNumberOfPrinting,
                        IsRepeatItem = entity.IsRepeatItem,
                        IsQuickAccount = entity.IsQuickAccount,
                        IsEntryGoldItemsAccounts = entity.IsEntryGoldItemsAccounts,

                        IsTotalsSummary = entity.IsTotalsSummary,
                        ExemptVatRate = entity.ExemptVatRate,
                        MainVatRate = entity.MainVatRate,

                        IsShowTaxesBox = entity.IsShowTaxesBox,
                        IsEnableWeight = entity.IsEnableWeight,
                        IsEnableGmWages = entity.IsEnableGmWages,
                        IsLockPrice = entity.IsLockPrice

                    };
            return q.ToList();
        }

        public Task<Dictionary<string, int>> CheckExist(BILL_SETTINGSVM entity)
        {
            return Task.Run<Dictionary<string, int>>(() =>
            {
                Dictionary<string, int> chkCount = new Dictionary<string, int>();
                chkCount.Add("nameAr", GetCountBILL_AR_NAME(entity.BILL_AR_NAME));
                chkCount.Add("nameEn", GetCountBILL_EN_NAME(entity.BILL_EN_NAME));
                chkCount.Add("aprvAr", GetCountBILL_ABRV_AR(entity.BILL_ABRV_AR));
                chkCount.Add("aprvEn", GetCountBILL_ABRV_EN(entity.BILL_ABRV_EN));

                return chkCount;
            });
        }

        int GetCountBILL_AR_NAME(string BILL_AR_NAME)
        {
            return billSettingsMRepo.GetAll().Where(x => x.BILL_AR_NAME.ToLower() == BILL_AR_NAME.ToLower()).Count();
        }

        int GetCountBILL_EN_NAME(string BILL_EN_NAME)
        {
            return billSettingsMRepo.GetAll().Where(x => x.BILL_EN_NAME.ToLower() == BILL_EN_NAME.ToLower()).Count();
        }

        int GetCountBILL_ABRV_AR(string BILL_ABRV_AR)
        {
            return billSettingsMRepo.GetAll().Where(x => x.BILL_ABRV_AR.ToLower() == BILL_ABRV_AR.ToLower()).Count();
        }

        int GetCountBILL_ABRV_EN(string BILL_ABRV_EN)
        {
            return billSettingsMRepo.GetAll().Where(x => x.BILL_ABRV_EN.ToLower() == BILL_ABRV_EN.ToLower()).Count();
        }

        public bool Insert(BILL_SETTINGSVM entity)
        {
            BILL_SETTINGS bs = new BILL_SETTINGS
            {
                ABRV_BILL = entity.ABRV_BILL,
                ACC_COST_ID = entity.ACC_COST_ID,
                ACC_DISC_ID = entity.ACC_DISC_ID,
                ACC_EXTRA_ID = entity.ACC_EXTRA_ID,
                ACC_GIFT_ID = entity.ACC_GIFT_ID,
                ACC_ITEM_ID = entity.ACC_ITEM_ID,
                ACC_PAY_ID = entity.ACC_PAY_ID,
                ACC_STORE_ID = entity.ACC_STORE_ID,
                AddedBy = entity.AddedBy,
                AddedOn = entity.AddedOn,
                ADD_BILL_ENTRY_WITH_THE_MAIN_CURRENCY_RATE = entity.ADD_BILL_ENTRY_WITH_THE_MAIN_CURRENCY_RATE,
                ALTERNATE_COLOR = entity.ALTERNATE_COLOR,
                AUTOMATIC_DISCOUNT = entity.AUTOMATIC_DISCOUNT,
                BILL_ABRV_AR = entity.BILL_ABRV_AR,
                BILL_ABRV_EN = entity.BILL_ABRV_EN,
                BILL_AR_NAME = entity.BILL_AR_NAME,
                BILL_EN_NAME = entity.BILL_EN_NAME,
                BILL_PAY_TYPE = entity.BILL_PAY_TYPE,
                BILL_SELL_TYPE_ID = entity.BILL_SELL_TYPE_ID,
                BILL_SETTING_ID = entity.BILL_SETTING_ID,
                BILL_SHORTCUT = entity.BILL_SHORTCUT,
                BILL_SHOW_NAME = entity.BILL_SHOW_NAME,
                BILL_TYPE_ID = entity.BILL_TYPE_ID,
                COM_STORE_ID = entity.COM_STORE_ID,
                CONTINEOUS_INVENTORY = entity.CONTINEOUS_INVENTORY,
                COST_CENTER_ID = entity.COST_CENTER_ID,
                CURRENCY_ID = entity.CURRENCY_ID,
                CURRENCY_RATE = entity.CURRENCY_RATE,
                DEFAULT_COLOR = entity.DEFAULT_COLOR,
                Disable = entity.Disable,
                DISABLE_BILL_PAY_TYPE = entity.DISABLE_BILL_PAY_TYPE,
                FIRSTBILLMESSAGE = entity.FIRSTBILLMESSAGE,
                FIRST_EXPIRE = entity.FIRST_EXPIRE,
                GENERATE_COST_ENTRY = entity.GENERATE_COST_ENTRY,
                GENERATE_ENTRY_STATE = entity.GENERATE_ENTRY_STATE,
                GST = entity.GST,
                GSTNAME = entity.GSTNAME,
                HEADERBILLMESSAGE = entity.HEADERBILLMESSAGE,
                IS_AUTO_POSTING = entity.IS_AUTO_POSTING,
                IS_IT_CASHER_BILL = entity.IS_IT_CASHER_BILL,
                IS_IT_SALES_POINT = entity.IS_IT_SALES_POINT,
                IS_IT_SERVICE_BILL = entity.IS_IT_SERVICE_BILL,
                IS_IT_TAX_SALE_BILL = entity.IS_IT_TAX_SALE_BILL,
                IS_IT_THREADING = entity.IS_IT_THREADING,
                LAST_PAY_PRICE = entity.LAST_PAY_PRICE,
                MIN_QTY = entity.MIN_QTY,
                MODULE_CARS = entity.MODULE_CARS,
                NUMBEROFCOPIES = entity.NUMBEROFCOPIES,
                OUT_MINUS = entity.OUT_MINUS,
                PRICE_COST_EFFECT = entity.PRICE_COST_EFFECT,
                PRINTHALFPAGE = entity.PRINTHALFPAGE,
                PRINTHALFPAGEBYLONG = entity.PRINTHALFPAGEBYLONG,
                PRINTLANDSCIP = entity.PRINTLANDSCIP,
                PRINT_BILL_AS_RESET_OR_AS_BILL = entity.PRINT_BILL_AS_RESET_OR_AS_BILL,
                PRINT_BILL_AUTOMATIC = entity.PRINT_BILL_AUTOMATIC,
                PST = entity.PST,
                PSTNAME = entity.PSTNAME,
                REPEATETHE_ITEM_AT_THE_BILL = entity.REPEATETHE_ITEM_AT_THE_BILL,
                SATTLEMENT_INCOME_LIST = entity.SATTLEMENT_INCOME_LIST,
                SEARCH_ONLY_BY_DEAULT_UNIT = entity.SEARCH_ONLY_BY_DEAULT_UNIT,
                SECONDBILLMESSAGE = entity.SECONDBILLMESSAGE,
                SERIAL_NUMBER = entity.SERIAL_NUMBER,
                SHOW_BILL_PAY_TYPE = entity.SHOW_BILL_PAY_TYPE,
                SHOW_COST_CENTER = entity.SHOW_COST_CENTER,
                SHOW_EMPLOYEE = entity.SHOW_EMPLOYEE,
                SHOW_THE_COMPANY_DATA_ON_BILL = entity.SHOW_THE_COMPANY_DATA_ON_BILL,
                SHOW_THE_CURRENCY = entity.ABRV_BILL,
                SHOW_THE_CURRENT_QTY_ON_BILL = entity.SHOW_THE_CURRENT_QTY_ON_BILL,
                SHOW_THE_ITEM_CODE_ON_BILL = entity.SHOW_THE_ITEM_CODE_ON_BILL,
                SHOW_THE_LAST_BALANCE_ON_THE_BILL = entity.SHOW_THE_LAST_BALANCE_ON_THE_BILL,
                SHOW_THE_LAST_DATE_FOR_RETURN = entity.SHOW_THE_LAST_DATE_FOR_RETURN,
                SHOW_THE_RETURN_PERCENTAGE = entity.SHOW_THE_RETURN_PERCENTAGE,
                STORE_EFFECT_STATE = entity.STORE_EFFECT_STATE,
                THESECONDCELLISTHEITEMCELLNOTTHEQUANTITY = entity.THESECONDCELLISTHEITEMCELLNOTTHEQUANTITY,
                TO_COM_STORE_ID = entity.TO_COM_STORE_ID,
                UpdatedBy = entity.UpdatedBy,
                UpdatedOn = entity.UpdatedOn,
                ALTERNATE_COLOR_HEXA = entity.ALTERNATE_COLOR_HEXA,
                DEFAULT_COLOR_HEXA = entity.DEFAULT_COLOR_HEXA,
                Tax = entity.Tax,
                CommissionTax = entity.CommissionTax,

                IsAccount = entity.IsAccount,
                IsBillDate = entity.IsBillDate,
                IsBillRemarks = entity.IsBillRemarks,
                IsCompStoreID = entity.IsCompStoreID,
                IsCurrency = entity.IsCurrency,
                IsCurrencyTrans = entity.IsCurrencyTrans,
                IsCustAccID = entity.IsCustAccID,
                IsEmpID = entity.IsEmpID,
                IsItemAccID = entity.IsItemAccID,
                IsPayTypes = entity.IsPayTypes,
                IsPayWay = entity.IsPayWay,
                IsSellType = entity.IsSellType,
                IsShiftNumber = entity.IsShiftNumber,
                BillAccountName = entity.BillAccountName,
                BillEmpName = entity.BillEmpName,

                IsToCompStore = entity.IsToCompStore,
                IsTotalExtra = entity.IsTotalExtra,
                IsTotalMustPaid = entity.IsTotalMustPaid,
                IsTotalPaid = entity.IsTotalPaid,
                //IsTotalPrice = entity.IsTotalPrice,
                IsTotalRemaining = entity.IsTotalRemaining,

                AccWagesID = entity.AccWagesID,
                IsItems = entity.IsItems,
                IsItemsGroups = entity.IsItemsGroups,

                AccSalesID = entity.AccSalesID,
                AccVatRateID = entity.AccVatRateID,
                MenuID = entity.MenuID,


                CashAccountID = entity.CashAccountID,
                VisaAccountID = entity.VisaAccountID,
                NoPaidAccountID = entity.NoPaidAccountID,
                PaymentAccountID = entity.PaymentAccountID,

                AccWagesTaxID = entity.AccWagesTaxID,
                WagesTaxValue = entity.WagesTaxValue,
                ShowPriceTypeID = entity.ShowPriceTypeID,

                PurchasAccID = entity.PurchasAccID,
                PurchasTaxAccID = entity.PurchasTaxAccID,

                AccCommissionID = entity.AccCommissionID,
                AccCommissionTaxID = entity.AccCommissionTaxID,

                AccSalesGoldID = entity.AccSalesGoldID,
                AccPurchaseGoldID = entity.AccPurchaseGoldID,

                Show_Wages_Discount = entity.Show_Wages_Discount,
                IsQuantityCalculated = entity.IsQuantityCalculated,
                AccGoldID = entity.AccGoldID,
                IsShowDeliveryPerson = entity.IsShowDeliveryPerson,
                IsShowEditReason = entity.IsShowEditReason,

                IsEnableTaxEdit = entity.IsEnableTaxEdit,
                IsShowCustomerBalance = entity.IsShowCustomerBalance,
                IsShowExternalNumber = entity.IsShowExternalNumber,
                IsShowGoldBoxBalance = entity.IsShowGoldBoxBalance,
                BillRowsNumber = entity.BillRowsNumber,

                IsCalcClearnessRate = entity.IsCalcClearnessRate,
                IsBillDiscRate = entity.IsBillDiscRate,
                IsBillDiscValue = entity.IsBillDiscValue,
                IsTotalDiscRates = entity.IsTotalDiscRates,
                IsTotalDiscValues = entity.IsTotalDiscValues,

                BillVatRate = entity.BillVatRate,
                IsBillVatRate = entity.IsBillVatRate,
                IsCalcBillDiscOfTotal = entity.IsCalcBillDiscOfTotal,

                TimesNumberOfPrinting = entity.TimesNumberOfPrinting,
                IsRepeatItem = entity.IsRepeatItem,
                IsQuickAccount = entity.IsQuickAccount,
                IsEntryGoldItemsAccounts = entity.IsEntryGoldItemsAccounts,

                IsTotalsSummary = entity.IsTotalsSummary,
                ExemptVatRate = entity.ExemptVatRate,
                MainVatRate = entity.MainVatRate,

                IsShowTaxesBox = entity.IsShowTaxesBox,
                IsEnableWeight = entity.IsEnableWeight,
                IsEnableGmWages = entity.IsEnableGmWages,
                IsLockPrice = entity.IsLockPrice

            };
            billSettingsMRepo.Add(bs);
            return true;
        }

        public Task<int> InsertAsync(BILL_SETTINGSVM entity)
        {
            return Task.Run<int>(() =>
            {
                BILL_SETTINGS bs = new BILL_SETTINGS
                {
                    ABRV_BILL = entity.ABRV_BILL,
                    ACC_COST_ID = entity.ACC_COST_ID,
                    ACC_DISC_ID = entity.ACC_DISC_ID,
                    ACC_EXTRA_ID = entity.ACC_EXTRA_ID,
                    ACC_GIFT_ID = entity.ACC_GIFT_ID,
                    ACC_ITEM_ID = entity.ACC_ITEM_ID,
                    ACC_PAY_ID = entity.ACC_PAY_ID,
                    ACC_STORE_ID = entity.ACC_STORE_ID,
                    AddedBy = entity.AddedBy,
                    AddedOn = entity.AddedOn,
                    ADD_BILL_ENTRY_WITH_THE_MAIN_CURRENCY_RATE = entity.ADD_BILL_ENTRY_WITH_THE_MAIN_CURRENCY_RATE,
                    ALTERNATE_COLOR = entity.ALTERNATE_COLOR,
                    AUTOMATIC_DISCOUNT = entity.AUTOMATIC_DISCOUNT,
                    BILL_ABRV_AR = entity.BILL_ABRV_AR,
                    BILL_ABRV_EN = entity.BILL_ABRV_EN,
                    BILL_AR_NAME = entity.BILL_AR_NAME,
                    BILL_EN_NAME = entity.BILL_EN_NAME,
                    BILL_PAY_TYPE = entity.BILL_PAY_TYPE,
                    BILL_SELL_TYPE_ID = entity.BILL_SELL_TYPE_ID,
                    BILL_SETTING_ID = entity.BILL_SETTING_ID,
                    BILL_SHORTCUT = entity.BILL_SHORTCUT,
                    BILL_SHOW_NAME = entity.BILL_SHOW_NAME,
                    BILL_TYPE_ID = entity.BILL_TYPE_ID,
                    COM_STORE_ID = entity.COM_STORE_ID,
                    CONTINEOUS_INVENTORY = entity.CONTINEOUS_INVENTORY,
                    COST_CENTER_ID = entity.COST_CENTER_ID,
                    CURRENCY_ID = entity.CURRENCY_ID,
                    CURRENCY_RATE = entity.CURRENCY_RATE,
                    DEFAULT_COLOR = entity.DEFAULT_COLOR,
                    Disable = entity.Disable,
                    DISABLE_BILL_PAY_TYPE = entity.DISABLE_BILL_PAY_TYPE,
                    FIRSTBILLMESSAGE = entity.FIRSTBILLMESSAGE,
                    FIRST_EXPIRE = entity.FIRST_EXPIRE,
                    GENERATE_COST_ENTRY = entity.GENERATE_COST_ENTRY,
                    GENERATE_ENTRY_STATE = entity.GENERATE_ENTRY_STATE,
                    GST = entity.GST,
                    GSTNAME = entity.GSTNAME,
                    HEADERBILLMESSAGE = entity.HEADERBILLMESSAGE,
                    IS_AUTO_POSTING = entity.IS_AUTO_POSTING,
                    IS_IT_CASHER_BILL = entity.IS_IT_CASHER_BILL,
                    IS_IT_SALES_POINT = entity.IS_IT_SALES_POINT,
                    IS_IT_SERVICE_BILL = entity.IS_IT_SERVICE_BILL,
                    IS_IT_TAX_SALE_BILL = entity.IS_IT_TAX_SALE_BILL,
                    IS_IT_THREADING = entity.IS_IT_THREADING,
                    LAST_PAY_PRICE = entity.LAST_PAY_PRICE,
                    MIN_QTY = entity.MIN_QTY,
                    MODULE_CARS = entity.MODULE_CARS,
                    NUMBEROFCOPIES = entity.NUMBEROFCOPIES,
                    OUT_MINUS = entity.OUT_MINUS,
                    PRICE_COST_EFFECT = entity.PRICE_COST_EFFECT,
                    PRINTHALFPAGE = entity.PRINTHALFPAGE,
                    PRINTHALFPAGEBYLONG = entity.PRINTHALFPAGEBYLONG,
                    PRINTLANDSCIP = entity.PRINTLANDSCIP,
                    PRINT_BILL_AS_RESET_OR_AS_BILL = entity.PRINT_BILL_AS_RESET_OR_AS_BILL,
                    PRINT_BILL_AUTOMATIC = entity.PRINT_BILL_AUTOMATIC,
                    PST = entity.PST,
                    PSTNAME = entity.PSTNAME,
                    REPEATETHE_ITEM_AT_THE_BILL = entity.REPEATETHE_ITEM_AT_THE_BILL,
                    SATTLEMENT_INCOME_LIST = entity.SATTLEMENT_INCOME_LIST,
                    SEARCH_ONLY_BY_DEAULT_UNIT = entity.SEARCH_ONLY_BY_DEAULT_UNIT,
                    SECONDBILLMESSAGE = entity.SECONDBILLMESSAGE,
                    SERIAL_NUMBER = entity.SERIAL_NUMBER,
                    SHOW_BILL_PAY_TYPE = entity.SHOW_BILL_PAY_TYPE,
                    SHOW_COST_CENTER = entity.SHOW_COST_CENTER,
                    SHOW_EMPLOYEE = entity.SHOW_EMPLOYEE,
                    SHOW_THE_COMPANY_DATA_ON_BILL = entity.SHOW_THE_COMPANY_DATA_ON_BILL,
                    SHOW_THE_CURRENCY = entity.ABRV_BILL,
                    SHOW_THE_CURRENT_QTY_ON_BILL = entity.SHOW_THE_CURRENT_QTY_ON_BILL,
                    SHOW_THE_ITEM_CODE_ON_BILL = entity.SHOW_THE_ITEM_CODE_ON_BILL,
                    SHOW_THE_LAST_BALANCE_ON_THE_BILL = entity.SHOW_THE_LAST_BALANCE_ON_THE_BILL,
                    SHOW_THE_LAST_DATE_FOR_RETURN = entity.SHOW_THE_LAST_DATE_FOR_RETURN,
                    SHOW_THE_RETURN_PERCENTAGE = entity.SHOW_THE_RETURN_PERCENTAGE,
                    STORE_EFFECT_STATE = entity.STORE_EFFECT_STATE,
                    THESECONDCELLISTHEITEMCELLNOTTHEQUANTITY = entity.THESECONDCELLISTHEITEMCELLNOTTHEQUANTITY,
                    TO_COM_STORE_ID = entity.TO_COM_STORE_ID,
                    UpdatedBy = entity.UpdatedBy,
                    UpdatedOn = entity.UpdatedOn,
                    ALTERNATE_COLOR_HEXA = entity.ALTERNATE_COLOR_HEXA,
                    DEFAULT_COLOR_HEXA = entity.DEFAULT_COLOR_HEXA,
                    Tax = entity.Tax,
                    CommissionTax = entity.CommissionTax,

                    IsAccount = entity.IsAccount,
                    IsBillDate = entity.IsBillDate,
                    IsBillRemarks = entity.IsBillRemarks,
                    IsCompStoreID = entity.IsCompStoreID,
                    IsCurrency = entity.IsCurrency,
                    IsCurrencyTrans = entity.IsCurrencyTrans,
                    IsCustAccID = entity.IsCustAccID,
                    IsEmpID = entity.IsEmpID,
                    IsItemAccID = entity.IsItemAccID,
                    IsPayTypes = entity.IsPayTypes,
                    IsPayWay = entity.IsPayWay,
                    IsSellType = entity.IsSellType,
                    IsShiftNumber = entity.IsShiftNumber,
                    BillAccountName = entity.BillAccountName,
                    BillEmpName = entity.BillEmpName,

                    AccWagesID = entity.AccWagesID,
                    IsItems = entity.IsItems,
                    IsItemsGroups = entity.IsItemsGroups,

                    AccSalesID = entity.AccSalesID,
                    AccVatRateID = entity.AccVatRateID,
                    MenuID = entity.MenuID,


                    CashAccountID = entity.CashAccountID,
                    VisaAccountID = entity.VisaAccountID,
                    NoPaidAccountID = entity.NoPaidAccountID,
                    PaymentAccountID = entity.PaymentAccountID,

                    AccWagesTaxID = entity.AccWagesTaxID,
                    WagesTaxValue = entity.WagesTaxValue,
                    ShowPriceTypeID = entity.ShowPriceTypeID,

                    PurchasAccID = entity.PurchasAccID,
                    PurchasTaxAccID = entity.PurchasTaxAccID,

                    AccCommissionID = entity.AccCommissionID,
                    AccCommissionTaxID = entity.AccCommissionTaxID,

                    AccSalesGoldID = entity.AccSalesGoldID,
                    AccPurchaseGoldID = entity.AccPurchaseGoldID,

                    Show_Wages_Discount = entity.Show_Wages_Discount,
                    IsQuantityCalculated = entity.IsQuantityCalculated,
                    AccGoldID = entity.AccGoldID,
                    IsShowDeliveryPerson = entity.IsShowDeliveryPerson,
                    IsShowEditReason = entity.IsShowEditReason,

                    IsEnableTaxEdit = entity.IsEnableTaxEdit,
                    IsShowCustomerBalance = entity.IsShowCustomerBalance,
                    IsShowExternalNumber = entity.IsShowExternalNumber,
                    IsShowGoldBoxBalance = entity.IsShowGoldBoxBalance,
                    BillRowsNumber = entity.BillRowsNumber,

                    IsCalcClearnessRate = entity.IsCalcClearnessRate,
                    IsBillDiscRate = entity.IsBillDiscRate,
                    IsBillDiscValue = entity.IsBillDiscValue,
                    IsTotalDiscRates = entity.IsTotalDiscRates,
                    IsTotalDiscValues = entity.IsTotalDiscValues,

                    BillVatRate = entity.BillVatRate,
                    IsBillVatRate = entity.IsBillVatRate,
                    IsCalcBillDiscOfTotal = entity.IsCalcBillDiscOfTotal,

                    TimesNumberOfPrinting = entity.TimesNumberOfPrinting,
                    IsRepeatItem = entity.IsRepeatItem,
                    IsQuickAccount = entity.IsQuickAccount,
                    IsEntryGoldItemsAccounts = entity.IsEntryGoldItemsAccounts,

                    IsTotalsSummary = entity.IsTotalsSummary,
                    ExemptVatRate = entity.ExemptVatRate,
                    MainVatRate = entity.MainVatRate,

                    IsShowTaxesBox = entity.IsShowTaxesBox,
                    IsEnableWeight = entity.IsEnableWeight,
                    IsEnableGmWages = entity.IsEnableGmWages,
                    IsLockPrice = entity.IsLockPrice

                };
                billSettingsMRepo.Add(bs);
                return bs.BILL_SETTING_ID;
            });
        }

        public bool Update(BILL_SETTINGSVM entity)
        {
            BILL_SETTINGS bs = new BILL_SETTINGS
            {
                ABRV_BILL = entity.ABRV_BILL,
                ACC_COST_ID = entity.ACC_COST_ID,
                ACC_DISC_ID = entity.ACC_DISC_ID,
                ACC_EXTRA_ID = entity.ACC_EXTRA_ID,
                ACC_GIFT_ID = entity.ACC_GIFT_ID,
                ACC_ITEM_ID = entity.ACC_ITEM_ID,
                ACC_PAY_ID = entity.ACC_PAY_ID,
                ACC_STORE_ID = entity.ACC_STORE_ID,
                AddedBy = entity.AddedBy,
                AddedOn = entity.AddedOn,
                ADD_BILL_ENTRY_WITH_THE_MAIN_CURRENCY_RATE = entity.ADD_BILL_ENTRY_WITH_THE_MAIN_CURRENCY_RATE,
                ALTERNATE_COLOR = entity.ALTERNATE_COLOR,
                AUTOMATIC_DISCOUNT = entity.AUTOMATIC_DISCOUNT,
                BILL_ABRV_AR = entity.BILL_ABRV_AR,
                BILL_ABRV_EN = entity.BILL_ABRV_EN,
                BILL_AR_NAME = entity.BILL_AR_NAME,
                BILL_EN_NAME = entity.BILL_EN_NAME,
                BILL_PAY_TYPE = entity.BILL_PAY_TYPE,
                BILL_SELL_TYPE_ID = entity.BILL_SELL_TYPE_ID,
                BILL_SETTING_ID = entity.BILL_SETTING_ID,
                BILL_SHORTCUT = entity.BILL_SHORTCUT,
                BILL_SHOW_NAME = entity.BILL_SHOW_NAME,
                BILL_TYPE_ID = entity.BILL_TYPE_ID,
                COM_STORE_ID = entity.COM_STORE_ID,
                CONTINEOUS_INVENTORY = entity.CONTINEOUS_INVENTORY,
                COST_CENTER_ID = entity.COST_CENTER_ID,
                CURRENCY_ID = entity.CURRENCY_ID,
                CURRENCY_RATE = entity.CURRENCY_RATE,
                DEFAULT_COLOR = entity.DEFAULT_COLOR,
                Disable = entity.Disable,
                DISABLE_BILL_PAY_TYPE = entity.DISABLE_BILL_PAY_TYPE,
                FIRSTBILLMESSAGE = entity.FIRSTBILLMESSAGE,
                FIRST_EXPIRE = entity.FIRST_EXPIRE,
                GENERATE_COST_ENTRY = entity.GENERATE_COST_ENTRY,
                GENERATE_ENTRY_STATE = entity.GENERATE_ENTRY_STATE,
                GST = entity.GST,
                GSTNAME = entity.GSTNAME,
                HEADERBILLMESSAGE = entity.HEADERBILLMESSAGE,
                IS_AUTO_POSTING = entity.IS_AUTO_POSTING,
                IS_IT_CASHER_BILL = entity.IS_IT_CASHER_BILL,
                IS_IT_SALES_POINT = entity.IS_IT_SALES_POINT,
                IS_IT_SERVICE_BILL = entity.IS_IT_SERVICE_BILL,
                IS_IT_TAX_SALE_BILL = entity.IS_IT_TAX_SALE_BILL,
                IS_IT_THREADING = entity.IS_IT_THREADING,
                LAST_PAY_PRICE = entity.LAST_PAY_PRICE,
                MIN_QTY = entity.MIN_QTY,
                MODULE_CARS = entity.MODULE_CARS,
                NUMBEROFCOPIES = entity.NUMBEROFCOPIES,
                OUT_MINUS = entity.OUT_MINUS,
                PRICE_COST_EFFECT = entity.PRICE_COST_EFFECT,
                PRINTHALFPAGE = entity.PRINTHALFPAGE,
                PRINTHALFPAGEBYLONG = entity.PRINTHALFPAGEBYLONG,
                PRINTLANDSCIP = entity.PRINTLANDSCIP,
                PRINT_BILL_AS_RESET_OR_AS_BILL = entity.PRINT_BILL_AS_RESET_OR_AS_BILL,
                PRINT_BILL_AUTOMATIC = entity.PRINT_BILL_AUTOMATIC,
                PST = entity.PST,
                PSTNAME = entity.PSTNAME,
                REPEATETHE_ITEM_AT_THE_BILL = entity.REPEATETHE_ITEM_AT_THE_BILL,
                SATTLEMENT_INCOME_LIST = entity.SATTLEMENT_INCOME_LIST,
                SEARCH_ONLY_BY_DEAULT_UNIT = entity.SEARCH_ONLY_BY_DEAULT_UNIT,
                SECONDBILLMESSAGE = entity.SECONDBILLMESSAGE,
                SERIAL_NUMBER = entity.SERIAL_NUMBER,
                SHOW_BILL_PAY_TYPE = entity.SHOW_BILL_PAY_TYPE,
                SHOW_COST_CENTER = entity.SHOW_COST_CENTER,
                SHOW_EMPLOYEE = entity.SHOW_EMPLOYEE,
                SHOW_THE_COMPANY_DATA_ON_BILL = entity.SHOW_THE_COMPANY_DATA_ON_BILL,
                SHOW_THE_CURRENCY = entity.ABRV_BILL,
                SHOW_THE_CURRENT_QTY_ON_BILL = entity.SHOW_THE_CURRENT_QTY_ON_BILL,
                SHOW_THE_ITEM_CODE_ON_BILL = entity.SHOW_THE_ITEM_CODE_ON_BILL,
                SHOW_THE_LAST_BALANCE_ON_THE_BILL = entity.SHOW_THE_LAST_BALANCE_ON_THE_BILL,
                SHOW_THE_LAST_DATE_FOR_RETURN = entity.SHOW_THE_LAST_DATE_FOR_RETURN,
                SHOW_THE_RETURN_PERCENTAGE = entity.SHOW_THE_RETURN_PERCENTAGE,
                STORE_EFFECT_STATE = entity.STORE_EFFECT_STATE,
                THESECONDCELLISTHEITEMCELLNOTTHEQUANTITY = entity.THESECONDCELLISTHEITEMCELLNOTTHEQUANTITY,
                TO_COM_STORE_ID = entity.TO_COM_STORE_ID,
                UpdatedBy = entity.UpdatedBy,
                UpdatedOn = entity.UpdatedOn,
                ALTERNATE_COLOR_HEXA = entity.ALTERNATE_COLOR_HEXA,
                DEFAULT_COLOR_HEXA = entity.DEFAULT_COLOR_HEXA,
                Tax = entity.Tax,
                CommissionTax = entity.CommissionTax,

                IsAccount = entity.IsAccount,
                IsBillDate = entity.IsBillDate,
                IsBillRemarks = entity.IsBillRemarks,
                IsCompStoreID = entity.IsCompStoreID,
                IsCurrency = entity.IsCurrency,
                IsCurrencyTrans = entity.IsCurrencyTrans,
                IsCustAccID = entity.IsCustAccID,
                IsEmpID = entity.IsEmpID,
                IsItemAccID = entity.IsItemAccID,
                IsPayTypes = entity.IsPayTypes,
                IsPayWay = entity.IsPayWay,
                IsSellType = entity.IsSellType,
                IsShiftNumber = entity.IsShiftNumber,
                BillAccountName = entity.BillAccountName,
                BillEmpName = entity.BillEmpName,

                IsToCompStore = entity.IsToCompStore,
                IsTotalExtra = entity.IsTotalExtra,
                IsTotalMustPaid = entity.IsTotalMustPaid,
                IsTotalPaid = entity.IsTotalPaid,
                //IsTotalPrice = entity.IsTotalPrice,
                IsTotalRemaining = entity.IsTotalRemaining,

                AccWagesID = entity.AccWagesID,
                IsItems = entity.IsItems,
                IsItemsGroups = entity.IsItemsGroups,

                AccSalesID = entity.AccSalesID,
                AccVatRateID = entity.AccVatRateID,
                MenuID = entity.MenuID,


                CashAccountID = entity.CashAccountID,
                VisaAccountID = entity.VisaAccountID,
                NoPaidAccountID = entity.NoPaidAccountID,
                PaymentAccountID = entity.PaymentAccountID,

                AccWagesTaxID = entity.AccWagesTaxID,
                WagesTaxValue = entity.WagesTaxValue,
                ShowPriceTypeID = entity.ShowPriceTypeID,

                PurchasAccID = entity.PurchasAccID,
                PurchasTaxAccID = entity.PurchasTaxAccID,

                AccCommissionID = entity.AccCommissionID,
                AccCommissionTaxID = entity.AccCommissionTaxID,

                AccSalesGoldID = entity.AccSalesGoldID,
                AccPurchaseGoldID = entity.AccPurchaseGoldID,

                Show_Wages_Discount = entity.Show_Wages_Discount,
                IsQuantityCalculated = entity.IsQuantityCalculated,
                AccGoldID = entity.AccGoldID,
                IsShowDeliveryPerson = entity.IsShowDeliveryPerson,
                IsShowEditReason = entity.IsShowEditReason,

                IsEnableTaxEdit = entity.IsEnableTaxEdit,
                IsShowCustomerBalance = entity.IsShowCustomerBalance,
                IsShowExternalNumber = entity.IsShowExternalNumber,
                IsShowGoldBoxBalance = entity.IsShowGoldBoxBalance,
                BillRowsNumber = entity.BillRowsNumber,

                IsCalcClearnessRate = entity.IsCalcClearnessRate,
                IsBillDiscRate = entity.IsBillDiscRate,
                IsBillDiscValue = entity.IsBillDiscValue,
                IsTotalDiscRates = entity.IsTotalDiscRates,
                IsTotalDiscValues = entity.IsTotalDiscValues,

                BillVatRate = entity.BillVatRate,
                IsBillVatRate = entity.IsBillVatRate,
                IsCalcBillDiscOfTotal = entity.IsCalcBillDiscOfTotal,

                TimesNumberOfPrinting = entity.TimesNumberOfPrinting,
                IsRepeatItem = entity.IsRepeatItem,
                IsQuickAccount = entity.IsQuickAccount,
                IsEntryGoldItemsAccounts = entity.IsEntryGoldItemsAccounts,

                IsTotalsSummary = entity.IsTotalsSummary,
                ExemptVatRate = entity.ExemptVatRate,
                MainVatRate = entity.MainVatRate,

                IsShowTaxesBox = entity.IsShowTaxesBox,
                IsEnableWeight = entity.IsEnableWeight,
                IsEnableGmWages = entity.IsEnableGmWages,
                IsLockPrice = entity.IsLockPrice

            };
            billSettingsMRepo.Update(bs, bs.BILL_SETTING_ID);
            return true;
        }

        public Task<bool> UpdateAsync(BILL_SETTINGSVM entity)
        {
            return Task.Run<bool>(() =>
            {
                BILL_SETTINGS bs = new BILL_SETTINGS
                {
                    ABRV_BILL = entity.ABRV_BILL,
                    ACC_COST_ID = entity.ACC_COST_ID,
                    ACC_DISC_ID = entity.ACC_DISC_ID,
                    ACC_EXTRA_ID = entity.ACC_EXTRA_ID,
                    ACC_GIFT_ID = entity.ACC_GIFT_ID,
                    ACC_ITEM_ID = entity.ACC_ITEM_ID,

                    ACC_PAY_ID = entity.ACC_PAY_ID,
                    ACC_STORE_ID = entity.ACC_STORE_ID,
                    AddedBy = entity.AddedBy,
                    AddedOn = entity.AddedOn,
                    ADD_BILL_ENTRY_WITH_THE_MAIN_CURRENCY_RATE = entity.ADD_BILL_ENTRY_WITH_THE_MAIN_CURRENCY_RATE,
                    ALTERNATE_COLOR = entity.ALTERNATE_COLOR,
                    AUTOMATIC_DISCOUNT = entity.AUTOMATIC_DISCOUNT,
                    BILL_ABRV_AR = entity.BILL_ABRV_AR,
                    BILL_ABRV_EN = entity.BILL_ABRV_EN,
                    BILL_AR_NAME = entity.BILL_AR_NAME,
                    BILL_EN_NAME = entity.BILL_EN_NAME,
                    BILL_PAY_TYPE = entity.BILL_PAY_TYPE,
                    BILL_SELL_TYPE_ID = entity.BILL_SELL_TYPE_ID,
                    BILL_SETTING_ID = entity.BILL_SETTING_ID,
                    BILL_SHORTCUT = entity.BILL_SHORTCUT,
                    BILL_SHOW_NAME = entity.BILL_SHOW_NAME,
                    BILL_TYPE_ID = entity.BILL_TYPE_ID,
                    COM_STORE_ID = entity.COM_STORE_ID,
                    CONTINEOUS_INVENTORY = entity.CONTINEOUS_INVENTORY,
                    COST_CENTER_ID = entity.COST_CENTER_ID,
                    CURRENCY_ID = entity.CURRENCY_ID,
                    CURRENCY_RATE = entity.CURRENCY_RATE,
                    DEFAULT_COLOR = entity.DEFAULT_COLOR,
                    Disable = entity.Disable,
                    DISABLE_BILL_PAY_TYPE = entity.DISABLE_BILL_PAY_TYPE,
                    FIRSTBILLMESSAGE = entity.FIRSTBILLMESSAGE,
                    FIRST_EXPIRE = entity.FIRST_EXPIRE,
                    GENERATE_COST_ENTRY = entity.GENERATE_COST_ENTRY,
                    GENERATE_ENTRY_STATE = entity.GENERATE_ENTRY_STATE,
                    GST = entity.GST,
                    GSTNAME = entity.GSTNAME,
                    HEADERBILLMESSAGE = entity.HEADERBILLMESSAGE,
                    IS_AUTO_POSTING = entity.IS_AUTO_POSTING,
                    IS_IT_CASHER_BILL = entity.IS_IT_CASHER_BILL,
                    IS_IT_SALES_POINT = entity.IS_IT_SALES_POINT,
                    IS_IT_SERVICE_BILL = entity.IS_IT_SERVICE_BILL,
                    IS_IT_TAX_SALE_BILL = entity.IS_IT_TAX_SALE_BILL,
                    IS_IT_THREADING = entity.IS_IT_THREADING,
                    LAST_PAY_PRICE = entity.LAST_PAY_PRICE,
                    MIN_QTY = entity.MIN_QTY,
                    MODULE_CARS = entity.MODULE_CARS,
                    NUMBEROFCOPIES = entity.NUMBEROFCOPIES,
                    OUT_MINUS = entity.OUT_MINUS,
                    PRICE_COST_EFFECT = entity.PRICE_COST_EFFECT,
                    PRINTHALFPAGE = entity.PRINTHALFPAGE,
                    PRINTHALFPAGEBYLONG = entity.PRINTHALFPAGEBYLONG,
                    PRINTLANDSCIP = entity.PRINTLANDSCIP,
                    PRINT_BILL_AS_RESET_OR_AS_BILL = entity.PRINT_BILL_AS_RESET_OR_AS_BILL,
                    PRINT_BILL_AUTOMATIC = entity.PRINT_BILL_AUTOMATIC,
                    PST = entity.PST,
                    PSTNAME = entity.PSTNAME,
                    REPEATETHE_ITEM_AT_THE_BILL = entity.REPEATETHE_ITEM_AT_THE_BILL,
                    SATTLEMENT_INCOME_LIST = entity.SATTLEMENT_INCOME_LIST,
                    SEARCH_ONLY_BY_DEAULT_UNIT = entity.SEARCH_ONLY_BY_DEAULT_UNIT,
                    SECONDBILLMESSAGE = entity.SECONDBILLMESSAGE,
                    SERIAL_NUMBER = entity.SERIAL_NUMBER,
                    SHOW_BILL_PAY_TYPE = entity.SHOW_BILL_PAY_TYPE,
                    SHOW_COST_CENTER = entity.SHOW_COST_CENTER,
                    SHOW_EMPLOYEE = entity.SHOW_EMPLOYEE,
                    SHOW_THE_COMPANY_DATA_ON_BILL = entity.SHOW_THE_COMPANY_DATA_ON_BILL,
                    SHOW_THE_CURRENCY = entity.ABRV_BILL,
                    SHOW_THE_CURRENT_QTY_ON_BILL = entity.SHOW_THE_CURRENT_QTY_ON_BILL,
                    SHOW_THE_ITEM_CODE_ON_BILL = entity.SHOW_THE_ITEM_CODE_ON_BILL,
                    SHOW_THE_LAST_BALANCE_ON_THE_BILL = entity.SHOW_THE_LAST_BALANCE_ON_THE_BILL,
                    SHOW_THE_LAST_DATE_FOR_RETURN = entity.SHOW_THE_LAST_DATE_FOR_RETURN,
                    SHOW_THE_RETURN_PERCENTAGE = entity.SHOW_THE_RETURN_PERCENTAGE,
                    STORE_EFFECT_STATE = entity.STORE_EFFECT_STATE,
                    THESECONDCELLISTHEITEMCELLNOTTHEQUANTITY = entity.THESECONDCELLISTHEITEMCELLNOTTHEQUANTITY,
                    TO_COM_STORE_ID = entity.TO_COM_STORE_ID,
                    UpdatedBy = entity.UpdatedBy,
                    UpdatedOn = entity.UpdatedOn,
                    ALTERNATE_COLOR_HEXA = entity.ALTERNATE_COLOR_HEXA,
                    DEFAULT_COLOR_HEXA = entity.DEFAULT_COLOR_HEXA,
                    Tax = entity.Tax,
                    CommissionTax = entity.CommissionTax,

                    IsAccount = entity.IsAccount,
                    IsBillDate = entity.IsBillDate,
                    IsBillRemarks = entity.IsBillRemarks,
                    IsCompStoreID = entity.IsCompStoreID,
                    IsCurrency = entity.IsCurrency,
                    IsCurrencyTrans = entity.IsCurrencyTrans,
                    IsCustAccID = entity.IsCustAccID,
                    IsEmpID = entity.IsEmpID,
                    IsItemAccID = entity.IsItemAccID,
                    IsPayTypes = entity.IsPayTypes,
                    IsPayWay = entity.IsPayWay,
                    IsSellType = entity.IsSellType,
                    IsShiftNumber = entity.IsShiftNumber,
                    BillAccountName = entity.BillAccountName,
                    BillEmpName = entity.BillEmpName,

                    IsToCompStore = entity.IsToCompStore,
                    IsTotalExtra = entity.IsTotalExtra,
                    IsTotalMustPaid = entity.IsTotalMustPaid,
                    IsTotalPaid = entity.IsTotalPaid,
                    // IsTotalPrice = entity.IsTotalPrice,
                    IsTotalRemaining = entity.IsTotalRemaining,

                    AccWagesID = entity.AccWagesID,
                    IsItems = entity.IsItems,
                    IsItemsGroups = entity.IsItemsGroups,

                    AccSalesID = entity.AccSalesID,
                    AccVatRateID = entity.AccVatRateID,
                    MenuID = entity.MenuID,


                    CashAccountID = entity.CashAccountID,
                    VisaAccountID = entity.VisaAccountID,
                    NoPaidAccountID = entity.NoPaidAccountID,
                    PaymentAccountID = entity.PaymentAccountID,

                    AccWagesTaxID = entity.AccWagesTaxID,
                    WagesTaxValue = entity.WagesTaxValue,
                    ShowPriceTypeID = entity.ShowPriceTypeID,

                    PurchasAccID = entity.PurchasAccID,
                    PurchasTaxAccID = entity.PurchasTaxAccID,

                    AccCommissionID = entity.AccCommissionID,
                    AccCommissionTaxID = entity.AccCommissionTaxID,

                    AccSalesGoldID = entity.AccSalesGoldID,
                    AccPurchaseGoldID = entity.AccPurchaseGoldID,

                    Show_Wages_Discount = entity.Show_Wages_Discount,
                    IsQuantityCalculated = entity.IsQuantityCalculated,
                    AccGoldID = entity.AccGoldID,
                    IsShowDeliveryPerson = entity.IsShowDeliveryPerson,
                    IsShowEditReason = entity.IsShowEditReason,

                    IsEnableTaxEdit = entity.IsEnableTaxEdit,
                    IsShowCustomerBalance = entity.IsShowCustomerBalance,
                    IsShowExternalNumber = entity.IsShowExternalNumber,
                    IsShowGoldBoxBalance = entity.IsShowGoldBoxBalance,
                    BillRowsNumber = entity.BillRowsNumber,

                    IsCalcClearnessRate = entity.IsCalcClearnessRate,
                    IsBillDiscRate = entity.IsBillDiscRate,
                    IsBillDiscValue = entity.IsBillDiscValue,
                    IsTotalDiscRates = entity.IsTotalDiscRates,
                    IsTotalDiscValues = entity.IsTotalDiscValues,

                    BillVatRate = entity.BillVatRate,
                    IsBillVatRate = entity.IsBillVatRate,
                    IsCalcBillDiscOfTotal = entity.IsCalcBillDiscOfTotal,

                    TimesNumberOfPrinting = entity.TimesNumberOfPrinting,
                    IsRepeatItem = entity.IsRepeatItem,
                    IsQuickAccount = entity.IsQuickAccount,
                    IsEntryGoldItemsAccounts = entity.IsEntryGoldItemsAccounts,

                    IsTotalsSummary = entity.IsTotalsSummary,
                    ExemptVatRate = entity.ExemptVatRate,
                    MainVatRate = entity.MainVatRate,

                    IsShowTaxesBox = entity.IsShowTaxesBox,
                    IsEnableWeight = entity.IsEnableWeight,
                    IsEnableGmWages = entity.IsEnableGmWages,
                    IsLockPrice = entity.IsLockPrice
                };
                billSettingsMRepo.Update(bs, bs.BILL_SETTING_ID);
                return true;
            });
        }


        public Task<BILL_SETTINGSVM> GetBillSettingByID(byte typeID)
        {




            return Task.Run<BILL_SETTINGSVM>(() =>
            {
                var billsetting = from entity in billSettingsMRepo.Filter(X => (X.BILL_SETTING_ID == typeID))
                                  join billType in billTypeRepo.GetAll() on entity.BILL_TYPE_ID equals billType.BILL_TYPE_ID into billTypesetting
                                  from billsettingType in billTypesetting
                                  select new BILL_SETTINGSVM
                                  {
                                      BILL_EFF_ID = billsettingType.BILL_EFF_ID,
                                      ABRV_BILL = entity.ABRV_BILL,
                                      ACC_COST_ID = entity.ACC_COST_ID,
                                      ACC_DISC_ID = entity.ACC_DISC_ID,
                                      ACC_EXTRA_ID = entity.ACC_EXTRA_ID,
                                      ACC_GIFT_ID = entity.ACC_GIFT_ID,
                                      ACC_ITEM_ID = entity.ACC_ITEM_ID,
                                      ACC_PAY_ID = entity.ACC_PAY_ID,
                                      ACC_STORE_ID = entity.ACC_STORE_ID,
                                      AddedBy = entity.AddedBy,
                                      AddedOn = entity.AddedOn,
                                      ADD_BILL_ENTRY_WITH_THE_MAIN_CURRENCY_RATE = entity.ADD_BILL_ENTRY_WITH_THE_MAIN_CURRENCY_RATE,
                                      ALTERNATE_COLOR = entity.ALTERNATE_COLOR,
                                      AUTOMATIC_DISCOUNT = entity.AUTOMATIC_DISCOUNT,
                                      BILL_ABRV_AR = entity.BILL_ABRV_AR,
                                      BILL_ABRV_EN = entity.BILL_ABRV_EN,
                                      BILL_AR_NAME = entity.BILL_AR_NAME,
                                      BILL_EN_NAME = entity.BILL_EN_NAME,
                                      BILL_PAY_TYPE = entity.BILL_PAY_TYPE,
                                      BILL_SELL_TYPE_ID = entity.BILL_SELL_TYPE_ID,
                                      BILL_SETTING_ID = entity.BILL_SETTING_ID,
                                      BILL_SHORTCUT = entity.BILL_SHORTCUT,
                                      BILL_SHOW_NAME = entity.BILL_SHOW_NAME,
                                      BILL_TYPE_ID = entity.BILL_TYPE_ID,
                                      COM_STORE_ID = entity.COM_STORE_ID,
                                      CONTINEOUS_INVENTORY = entity.CONTINEOUS_INVENTORY,
                                      COST_CENTER_ID = entity.COST_CENTER_ID,
                                      CURRENCY_ID = entity.CURRENCY_ID,
                                      CURRENCY_RATE = entity.CURRENCY_RATE,
                                      DEFAULT_COLOR = entity.DEFAULT_COLOR,
                                      Disable = entity.Disable,
                                      DISABLE_BILL_PAY_TYPE = entity.DISABLE_BILL_PAY_TYPE,
                                      FIRSTBILLMESSAGE = entity.FIRSTBILLMESSAGE,
                                      FIRST_EXPIRE = entity.FIRST_EXPIRE,
                                      GENERATE_COST_ENTRY = entity.GENERATE_COST_ENTRY,
                                      GENERATE_ENTRY_STATE = entity.GENERATE_ENTRY_STATE,
                                      GST = entity.GST,
                                      GSTNAME = entity.GSTNAME,
                                      HEADERBILLMESSAGE = entity.HEADERBILLMESSAGE,
                                      IS_AUTO_POSTING = entity.IS_AUTO_POSTING,
                                      IS_IT_CASHER_BILL = entity.IS_IT_CASHER_BILL,
                                      IS_IT_SALES_POINT = entity.IS_IT_SALES_POINT,
                                      IS_IT_SERVICE_BILL = entity.IS_IT_SERVICE_BILL,
                                      IS_IT_TAX_SALE_BILL = entity.IS_IT_TAX_SALE_BILL,
                                      IS_IT_THREADING = entity.IS_IT_THREADING,
                                      LAST_PAY_PRICE = entity.LAST_PAY_PRICE,
                                      MIN_QTY = entity.MIN_QTY,
                                      MODULE_CARS = entity.MODULE_CARS,
                                      NUMBEROFCOPIES = entity.NUMBEROFCOPIES,
                                      OUT_MINUS = entity.OUT_MINUS,
                                      PRICE_COST_EFFECT = entity.PRICE_COST_EFFECT,
                                      PRINTHALFPAGE = entity.PRINTHALFPAGE,
                                      PRINTHALFPAGEBYLONG = entity.PRINTHALFPAGEBYLONG,
                                      PRINTLANDSCIP = entity.PRINTLANDSCIP,
                                      PRINT_BILL_AS_RESET_OR_AS_BILL = entity.PRINT_BILL_AS_RESET_OR_AS_BILL,
                                      PRINT_BILL_AUTOMATIC = entity.PRINT_BILL_AUTOMATIC,
                                      PST = entity.PST,
                                      PSTNAME = entity.PSTNAME,
                                      REPEATETHE_ITEM_AT_THE_BILL = entity.REPEATETHE_ITEM_AT_THE_BILL,
                                      SATTLEMENT_INCOME_LIST = entity.SATTLEMENT_INCOME_LIST,
                                      SEARCH_ONLY_BY_DEAULT_UNIT = entity.SEARCH_ONLY_BY_DEAULT_UNIT,
                                      SECONDBILLMESSAGE = entity.SECONDBILLMESSAGE,
                                      SERIAL_NUMBER = entity.SERIAL_NUMBER,
                                      SHOW_BILL_PAY_TYPE = entity.SHOW_BILL_PAY_TYPE,
                                      SHOW_COST_CENTER = entity.SHOW_COST_CENTER,
                                      SHOW_EMPLOYEE = entity.SHOW_EMPLOYEE,
                                      SHOW_THE_COMPANY_DATA_ON_BILL = entity.SHOW_THE_COMPANY_DATA_ON_BILL,
                                      SHOW_THE_CURRENCY = entity.ABRV_BILL,
                                      SHOW_THE_CURRENT_QTY_ON_BILL = entity.SHOW_THE_CURRENT_QTY_ON_BILL,
                                      SHOW_THE_ITEM_CODE_ON_BILL = entity.SHOW_THE_ITEM_CODE_ON_BILL,
                                      SHOW_THE_LAST_BALANCE_ON_THE_BILL = entity.SHOW_THE_LAST_BALANCE_ON_THE_BILL,
                                      SHOW_THE_LAST_DATE_FOR_RETURN = entity.SHOW_THE_LAST_DATE_FOR_RETURN,
                                      SHOW_THE_RETURN_PERCENTAGE = entity.SHOW_THE_RETURN_PERCENTAGE,
                                      STORE_EFFECT_STATE = entity.STORE_EFFECT_STATE,
                                      THESECONDCELLISTHEITEMCELLNOTTHEQUANTITY = entity.THESECONDCELLISTHEITEMCELLNOTTHEQUANTITY,
                                      TO_COM_STORE_ID = entity.TO_COM_STORE_ID,
                                      UpdatedBy = entity.UpdatedBy,
                                      UpdatedOn = entity.UpdatedOn,
                                      ALTERNATE_COLOR_HEXA = entity.ALTERNATE_COLOR_HEXA,
                                      DEFAULT_COLOR_HEXA = entity.DEFAULT_COLOR_HEXA,
                                      Tax = entity.Tax,
                                      CommissionTax = entity.CommissionTax,

                                      IsAccount = entity.IsAccount,
                                      IsBillDate = entity.IsBillDate,
                                      IsBillRemarks = entity.IsBillRemarks,
                                      IsCompStoreID = entity.IsCompStoreID,
                                      IsCurrency = entity.IsCurrency,
                                      IsCurrencyTrans = entity.IsCurrencyTrans,
                                      IsCustAccID = entity.IsCustAccID,
                                      IsEmpID = entity.IsEmpID,
                                      IsItemAccID = entity.IsItemAccID,
                                      IsPayTypes = entity.IsPayTypes,
                                      IsPayWay = entity.IsPayWay,
                                      IsSellType = entity.IsSellType,
                                      IsShiftNumber = entity.IsShiftNumber,
                                      BillAccountName = entity.BillAccountName,
                                      BillEmpName = entity.BillEmpName,

                                      IsToCompStore = entity.IsToCompStore,
                                      IsTotalExtra = entity.IsTotalExtra,
                                      IsTotalMustPaid = entity.IsTotalMustPaid,
                                      IsTotalPaid = entity.IsTotalPaid,
                                      // IsTotalPrice = entity.IsTotalPrice,
                                      IsTotalRemaining = entity.IsTotalRemaining,

                                      AccWagesID = entity.AccWagesID,
                                      IsItems = entity.IsItems,
                                      IsItemsGroups = entity.IsItemsGroups,

                                      AccSalesID = entity.AccSalesID,
                                      AccVatRateID = entity.AccVatRateID,
                                      MenuID = entity.MenuID,


                                      CashAccountID = entity.CashAccountID,
                                      VisaAccountID = entity.VisaAccountID,
                                      NoPaidAccountID = entity.NoPaidAccountID,
                                      PaymentAccountID = entity.PaymentAccountID,

                                      AccWagesTaxID = entity.AccWagesTaxID,
                                      WagesTaxValue = entity.WagesTaxValue,
                                      ShowPriceTypeID = entity.ShowPriceTypeID,

                                      PurchasAccID = entity.PurchasAccID,
                                      PurchasTaxAccID = entity.PurchasTaxAccID,

                                      AccCommissionID = entity.AccCommissionID,
                                      AccCommissionTaxID = entity.AccCommissionTaxID,

                                      AccSalesGoldID = entity.AccSalesGoldID,
                                      AccPurchaseGoldID = entity.AccPurchaseGoldID,

                                      Show_Wages_Discount = entity.Show_Wages_Discount,
                                      IsQuantityCalculated = entity.IsQuantityCalculated,
                                      AccGoldID = entity.AccGoldID,
                                      IsShowDeliveryPerson = entity.IsShowDeliveryPerson,
                                      IsShowEditReason = entity.IsShowEditReason,

                                      IsEnableTaxEdit = entity.IsEnableTaxEdit,
                                      IsShowCustomerBalance = entity.IsShowCustomerBalance,
                                      IsShowExternalNumber = entity.IsShowExternalNumber,
                                      IsShowGoldBoxBalance = entity.IsShowGoldBoxBalance,
                                      BillRowsNumber = entity.BillRowsNumber,

                                      IsCalcClearnessRate = entity.IsCalcClearnessRate,
                                      IsBillDiscRate = entity.IsBillDiscRate,
                                      IsBillDiscValue = entity.IsBillDiscValue,
                                      IsTotalDiscRates = entity.IsTotalDiscRates,
                                      IsTotalDiscValues = entity.IsTotalDiscValues,

                                      BillVatRate = entity.BillVatRate,
                                      IsBillVatRate = entity.IsBillVatRate,
                                      IsCalcBillDiscOfTotal = entity.IsCalcBillDiscOfTotal,

                                      TimesNumberOfPrinting = entity.TimesNumberOfPrinting,
                                      IsRepeatItem = entity.IsRepeatItem,
                                      IsQuickAccount = entity.IsQuickAccount,
                                      IsEntryGoldItemsAccounts = entity.IsEntryGoldItemsAccounts,

                                      IsTotalsSummary = entity.IsTotalsSummary,
                                      ExemptVatRate = entity.ExemptVatRate,
                                      MainVatRate = entity.MainVatRate,

                                      IsShowTaxesBox = entity.IsShowTaxesBox,
                                      IsEnableWeight = entity.IsEnableWeight,
                                      IsEnableGmWages = entity.IsEnableGmWages,
                                      IsLockPrice = entity.IsLockPrice

                                  };
                return billsetting.FirstOrDefault();

                //return billSettingsMRepo.Filter(X => (X.BILL_SETTING_ID == typeID)).Select(entity => new BILL_SETTINGSVM()
                //{
                //    ABRV_BILL = entity.ABRV_BILL,
                //    ACC_COST_ID = entity.ACC_COST_ID,
                //    ACC_DISC_ID = entity.ACC_DISC_ID,
                //    ACC_EXTRA_ID = entity.ACC_EXTRA_ID,
                //    ACC_GIFT_ID = entity.ACC_GIFT_ID,
                //    ACC_ITEM_ID = entity.ACC_ITEM_ID,
                //    ACC_PAY_ID = entity.ACC_PAY_ID,
                //    ACC_STORE_ID = entity.ACC_STORE_ID,
                //    AddedBy = entity.AddedBy,
                //    AddedOn = entity.AddedOn,
                //    ADD_BILL_ENTRY_WITH_THE_MAIN_CURRENCY_RATE = entity.ADD_BILL_ENTRY_WITH_THE_MAIN_CURRENCY_RATE,
                //    ALTERNATE_COLOR = entity.ALTERNATE_COLOR,
                //    AUTOMATIC_DISCOUNT = entity.AUTOMATIC_DISCOUNT,
                //    BILL_ABRV_AR = entity.BILL_ABRV_AR,
                //    BILL_ABRV_EN = entity.BILL_ABRV_EN,
                //    BILL_AR_NAME = entity.BILL_AR_NAME,
                //    BILL_EN_NAME = entity.BILL_EN_NAME,
                //    BILL_PAY_TYPE = entity.BILL_PAY_TYPE,
                //    BILL_SELL_TYPE_ID = entity.BILL_SELL_TYPE_ID,
                //    BILL_SETTING_ID = entity.BILL_SETTING_ID,
                //    BILL_SHORTCUT = entity.BILL_SHORTCUT,
                //    BILL_SHOW_NAME = entity.BILL_SHOW_NAME,
                //    BILL_TYPE_ID = entity.BILL_TYPE_ID,
                //    COM_STORE_ID = entity.COM_STORE_ID,
                //    CONTINEOUS_INVENTORY = entity.CONTINEOUS_INVENTORY,
                //    COST_CENTER_ID = entity.COST_CENTER_ID,
                //    CURRENCY_ID = entity.CURRENCY_ID,
                //    CURRENCY_RATE = entity.CURRENCY_RATE,
                //    DEFAULT_COLOR = entity.DEFAULT_COLOR,
                //    Disable = entity.Disable,
                //    DISABLE_BILL_PAY_TYPE = entity.DISABLE_BILL_PAY_TYPE,
                //    FIRSTBILLMESSAGE = entity.FIRSTBILLMESSAGE,
                //    FIRST_EXPIRE = entity.FIRST_EXPIRE,
                //    GENERATE_COST_ENTRY = entity.GENERATE_COST_ENTRY,
                //    GENERATE_ENTRY_STATE = entity.GENERATE_ENTRY_STATE,
                //    GST = entity.GST,
                //    GSTNAME = entity.GSTNAME,
                //    HEADERBILLMESSAGE = entity.HEADERBILLMESSAGE,
                //    IS_AUTO_POSTING = entity.IS_AUTO_POSTING,
                //    IS_IT_CASHER_BILL = entity.IS_IT_CASHER_BILL,
                //    IS_IT_SALES_POINT = entity.IS_IT_SALES_POINT,
                //    IS_IT_SERVICE_BILL = entity.IS_IT_SERVICE_BILL,
                //    IS_IT_TAX_SALE_BILL = entity.IS_IT_TAX_SALE_BILL,
                //    IS_IT_THREADING = entity.IS_IT_THREADING,
                //    LAST_PAY_PRICE = entity.LAST_PAY_PRICE,
                //    MIN_QTY = entity.MIN_QTY,
                //    MODULE_CARS = entity.MODULE_CARS,
                //    NUMBEROFCOPIES = entity.NUMBEROFCOPIES,
                //    OUT_MINUS = entity.OUT_MINUS,
                //    PRICE_COST_EFFECT = entity.PRICE_COST_EFFECT,
                //    PRINTHALFPAGE = entity.PRINTHALFPAGE,
                //    PRINTHALFPAGEBYLONG = entity.PRINTHALFPAGEBYLONG,
                //    PRINTLANDSCIP = entity.PRINTLANDSCIP,
                //    PRINT_BILL_AS_RESET_OR_AS_BILL = entity.PRINT_BILL_AS_RESET_OR_AS_BILL,
                //    PRINT_BILL_AUTOMATIC = entity.PRINT_BILL_AUTOMATIC,
                //    PST = entity.PST,
                //    PSTNAME = entity.PSTNAME,
                //    REPEATETHE_ITEM_AT_THE_BILL = entity.REPEATETHE_ITEM_AT_THE_BILL,
                //    SATTLEMENT_INCOME_LIST = entity.SATTLEMENT_INCOME_LIST,
                //    SEARCH_ONLY_BY_DEAULT_UNIT = entity.SEARCH_ONLY_BY_DEAULT_UNIT,
                //    SECONDBILLMESSAGE = entity.SECONDBILLMESSAGE,
                //    SERIAL_NUMBER = entity.SERIAL_NUMBER,
                //    SHOW_BILL_PAY_TYPE = entity.SHOW_BILL_PAY_TYPE,
                //    SHOW_COST_CENTER = entity.SHOW_COST_CENTER,
                //    SHOW_EMPLOYEE = entity.SHOW_EMPLOYEE,
                //    SHOW_THE_COMPANY_DATA_ON_BILL = entity.SHOW_THE_COMPANY_DATA_ON_BILL,
                //    SHOW_THE_CURRENCY = entity.ABRV_BILL,
                //    SHOW_THE_CURRENT_QTY_ON_BILL = entity.SHOW_THE_CURRENT_QTY_ON_BILL,
                //    SHOW_THE_ITEM_CODE_ON_BILL = entity.SHOW_THE_ITEM_CODE_ON_BILL,
                //    SHOW_THE_LAST_BALANCE_ON_THE_BILL = entity.SHOW_THE_LAST_BALANCE_ON_THE_BILL,
                //    SHOW_THE_LAST_DATE_FOR_RETURN = entity.SHOW_THE_LAST_DATE_FOR_RETURN,
                //    SHOW_THE_RETURN_PERCENTAGE = entity.SHOW_THE_RETURN_PERCENTAGE,
                //    STORE_EFFECT_STATE = entity.STORE_EFFECT_STATE,
                //    THESECONDCELLISTHEITEMCELLNOTTHEQUANTITY = entity.THESECONDCELLISTHEITEMCELLNOTTHEQUANTITY,
                //    TO_COM_STORE_ID = entity.TO_COM_STORE_ID,
                //    UpdatedBy = entity.UpdatedBy,
                //    UpdatedOn = entity.UpdatedOn,
                //    ALTERNATE_COLOR_HEXA = entity.ALTERNATE_COLOR_HEXA,
                //    DEFAULT_COLOR_HEXA = entity.DEFAULT_COLOR_HEXA,
                //    Tax = entity.Tax,
                //    CommissionTax = entity.CommissionTax,

                //    IsAccount = entity.IsAccount,
                //    IsBillDate = entity.IsBillDate,
                //    IsBillRemarks = entity.IsBillRemarks,
                //    IsCompStoreID = entity.IsCompStoreID,
                //    IsCurrency = entity.IsCurrency,
                //    IsCurrencyTrans = entity.IsCurrencyTrans,
                //    IsCustAccID = entity.IsCustAccID,
                //    IsEmpID = entity.IsEmpID,
                //    IsItemAccID = entity.IsItemAccID,
                //    IsPayTypes = entity.IsPayTypes,
                //    IsPayWay = entity.IsPayWay,
                //    IsSellType = entity.IsSellType,
                //    IsShiftNumber = entity.IsShiftNumber,
                //    BillAccountName = entity.BillAccountName,
                //    BillEmpName = entity.BillEmpName,

                //    IsToCompStore = entity.IsToCompStore,
                //    IsTotalExtra = entity.IsTotalExtra,
                //    IsTotalMustPaid = entity.IsTotalMustPaid,
                //    IsTotalPaid = entity.IsTotalPaid,
                //    // IsTotalPrice = entity.IsTotalPrice,
                //    IsTotalRemaining = entity.IsTotalRemaining,

                //    AccWagesID = entity.AccWagesID,
                //    IsItems = entity.IsItems,
                //    IsItemsGroups = entity.IsItemsGroups,

                //    AccSalesID = entity.AccSalesID,
                //    AccVatRateID = entity.AccVatRateID,
                //    MenuID = entity.MenuID,


                //    CashAccountID = entity.CashAccountID,
                //    VisaAccountID = entity.VisaAccountID,
                //    NoPaidAccountID = entity.NoPaidAccountID,
                //    PaymentAccountID = entity.PaymentAccountID,

                //    AccWagesTaxID = entity.AccWagesTaxID,
                //    WagesTaxValue = entity.WagesTaxValue,
                //    ShowPriceTypeID = entity.ShowPriceTypeID,

                //    PurchasAccID = entity.PurchasAccID,
                //    PurchasTaxAccID = entity.PurchasTaxAccID,

                //    AccCommissionID = entity.AccCommissionID,
                //    AccCommissionTaxID = entity.AccCommissionTaxID,

                //    AccSalesGoldID = entity.AccSalesGoldID,
                //    AccPurchaseGoldID = entity.AccPurchaseGoldID,

                //    Show_Wages_Discount = entity.Show_Wages_Discount,
                //    IsQuantityCalculated = entity.IsQuantityCalculated,
                //    AccGoldID = entity.AccGoldID,
                //    IsShowDeliveryPerson = entity.IsShowDeliveryPerson,
                //    IsShowEditReason = entity.IsShowEditReason,

                //    IsEnableTaxEdit = entity.IsEnableTaxEdit,
                //    IsShowCustomerBalance = entity.IsShowCustomerBalance,
                //    IsShowExternalNumber = entity.IsShowExternalNumber,
                //    IsShowGoldBoxBalance = entity.IsShowGoldBoxBalance,
                //    BillRowsNumber = entity.BillRowsNumber,

                //    IsCalcClearnessRate = entity.IsCalcClearnessRate,
                //    IsBillDiscRate = entity.IsBillDiscRate,
                //    IsBillDiscValue = entity.IsBillDiscValue,
                //    IsTotalDiscRates = entity.IsTotalDiscRates,
                //    IsTotalDiscValues = entity.IsTotalDiscValues,

                //    BillVatRate = entity.BillVatRate,
                //    IsBillVatRate = entity.IsBillVatRate,
                //    IsCalcBillDiscOfTotal = entity.IsCalcBillDiscOfTotal,

                //    TimesNumberOfPrinting = entity.TimesNumberOfPrinting

                //}).FirstOrDefault();
            });
        }
    }
}
