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
    public class BillGridColumnsService : IBillGridColumnsService, IDisposable
    {
        IGenericRepository<BILL_GRID_COLUMNS> billGridColumnsMRepo;
        public BillGridColumnsService(IGenericRepository<BILL_GRID_COLUMNS> billGridColumnsMRepo)
        {
            this.billGridColumnsMRepo = billGridColumnsMRepo;
        }

        public Task<int> CountAsync()
        {
            return Task.Run<int>(() =>
            {
                return billGridColumnsMRepo.CountAsync();
            });
        }

        public bool Delete(BILL_GRID_COLUMNSVM entity)
        {
            BILL_GRID_COLUMNS bgc = new BILL_GRID_COLUMNS
            {
                AddedBy = entity.AddedBy,
                AddedOn = entity.AddedOn,
                BILL_GRID_ID = entity.BILL_GRID_ID,
                BILL_REMARKS_INDEX = entity.BILL_REMARKS_INDEX,
                BILL_REMARKS_WIDTH = entity.BILL_REMARKS_WIDTH,
                BILL_SETTING_ID = entity.BILL_SETTING_ID,
                CEXTERNALLEXPENSES_INDEX = entity.CEXTERNALLEXPENSES_INDEX,
                CEXTERNALLEXPENSES_WIDTH = entity.CEXTERNALLEXPENSES_WIDTH,
                CINVENTORYVALUE_INDEX = entity.CINVENTORYVALUE_INDEX,
                CINVENTORYVALUE_WIDTH = entity.CINVENTORYVALUE_WIDTH,
                CTOTALCURR_INDEX = entity.CTOTALCURR_INDEX,
                CTOTALCURR_WIDTH = entity.CTOTALCURR_WIDTH,
                CTOTALWITHEXPENSES_INDEX = entity.CTOTALWITHEXPENSES_INDEX,
                CTOTALWITHEXPENSES_WIDTH = entity.CTOTALWITHEXPENSES_WIDTH,
                DISC_RATE_INDEX = entity.DISC_RATE_INDEX,
                DISC_RATE_WIDTH = entity.DISC_RATE_WIDTH,
                DISC_VALUE_INDEX = entity.DISC_VALUE_INDEX,
                DISC_VALUE_WIDTH = entity.DISC_VALUE_WIDTH,
                EMPLOYEE_INDEX = entity.EMPLOYEE_INDEX,
                EMPLOYEE_WIDTH = entity.EMPLOYEE_WIDTH,
                END_USERS_INDEX = entity.END_USERS_INDEX,
                END_USERS_WIDTH = entity.END_USERS_WIDTH,
                EXPIRED_DATE_INDEX = entity.EXPIRED_DATE_INDEX,
                EXPIRED_DATE_WIDTH = entity.EXPIRED_DATE_WIDTH,
                EXPORT_INDEX = entity.EXPORT_INDEX,
                EXPORT_WIDTH = entity.EXPORT_WIDTH,
                EXTRA_RATE_INDEX = entity.EXTRA_RATE_INDEX,
                EXTRA_RATE_WIDTH = entity.EXTRA_RATE_WIDTH,
                EXTRA_VALUE_INDEX = entity.EXTRA_VALUE_INDEX,
                EXTRA_VALUE_WIDTH = entity.EXTRA_VALUE_WIDTH,
                GIFTS_INDEX = entity.GIFTS_INDEX,
                GIFTS_WIDTH = entity.GIFTS_WIDTH,
                HALF_WHOLE_INDEX = entity.HALF_WHOLE_INDEX,
                HALF_WHOLE_WIDTH = entity.HALF_WHOLE_WIDTH,
                ITEM_INDEX = entity.ITEM_INDEX,
                ITEM_REMAIN_QTY_INDEX = entity.ITEM_REMAIN_QTY_INDEX,
                ITEM_WIDTH = entity.ITEM_WIDTH,
                ITEM_REMAIN_QTY_WIDTH = entity.ITEM_REMAIN_QTY_WIDTH,
                LAST_BUY_INDEX = entity.LAST_BUY_INDEX,
                LAST_BUY_WIDTH = entity.LAST_BUY_WIDTH,
                PRODUCTION_DATE_INDEX = entity.PRODUCTION_DATE_INDEX,
                PRODUCTION_DATE_WIDTH = entity.PRODUCTION_DATE_WIDTH,
                QTY_INDEX = entity.QTY_INDEX,
                QTY_WIDTH = entity.QTY_WIDTH,
                RETAIL_INDEX = entity.RETAIL_INDEX,
                RETAIL_WIDTH = entity.RETAIL_WIDTH,
                TOTAL_INDEX = entity.TOTAL_INDEX,
                TOTAL_WIDTH = entity.TOTAL_WIDTH,
                UNIT_COST_INDEX = entity.UNIT_COST_INDEX,
                UNIT_COST_WIDTH = entity.UNIT_COST_WIDTH,
                UNIT_INDEX = entity.UNIT_INDEX,
                UNIT_WIDTH = entity.UNIT_WIDTH,
                UpdatedBy = entity.UpdatedBy,
                updatedOn = entity.updatedOn,
                WHOLE_INDEX = entity.WHOLE_INDEX,
                WHOLE_WIDTH = entity.WHOLE_WIDTH,

                Cost_Center_INDEX = entity.Cost_Center_INDEX,
                Cost_Center_WIDTH = entity.Cost_Center_WIDTH,

                Item_Weight_INDEX = entity.Item_Weight_INDEX,
                Item_Weight_WIDTH = entity.Item_Weight_WIDTH,
                Item_Wages_INDEX = entity.Item_Wages_INDEX,
                Item_Wages_WIDTH = entity.Item_Wages_WIDTH,
                Manufact_Wages_INDEX = entity.Manufact_Wages_INDEX,
                Manufact_Wages_WIDTH = entity.Manufact_Wages_WIDTH,
                ARName_INDEX = entity.ARName_INDEX,
                ARName_WIDTH = entity.ARName_WIDTH,
                Clearness_Rate_INDEX = entity.Clearness_Rate_INDEX,
                Clearness_Rate_WIDTH = entity.Clearness_Rate_WIDTH,
                Caliber24_INDEX = entity.Caliber24_INDEX,
                Caliber24_WIDTH = entity.Caliber24_WIDTH,
                Caliber22_INDEX = entity.Caliber22_INDEX,
                Caliber22_WIDTH = entity.Caliber22_WIDTH,
                Caliber21_INDEX = entity.Caliber21_INDEX,
                Caliber21_WIDTH = entity.Caliber21_WIDTH,
                Caliber18_INDEX = entity.Caliber18_INDEX,
                Caliber18_WIDTH = entity.Caliber18_WIDTH,
                Subject_To_VAT_INDEX = entity.Subject_To_VAT_INDEX,
                Subject_To_VAT_WIDTH = entity.Subject_To_VAT_WIDTH,
                VAT_Rate_INDEX = entity.VAT_Rate_INDEX,
                VAT_Rate_WIDTH = entity.VAT_Rate_WIDTH,

                WagesDiscValue_INDEX = entity.WagesDiscValue_INDEX,
                WagesDiscValue_WIDTH = entity.WagesDiscValue_WIDTH,
                WagesDiscRate_INDEX = entity.WagesDiscRate_INDEX,
                WagesDiscRate_WIDTH = entity.WagesDiscRate_WIDTH,

                ActualWeight_INDEX = entity.ActualWeight_INDEX,
                ActualWeight_WIDTH = entity.ActualWeight_WIDTH,

                TaxTotal_INDEX = entity.TaxTotal_INDEX,
                TaxTotal_WIDTH = entity.TaxTotal_WIDTH,

                TotalItemGmWages_INDEX = entity.TotalItemGmWages_INDEX,
                TotalItemGmWages_WIDTH = entity.TotalItemGmWages_WIDTH,

                TotalGoldManufact_INDEX = entity.TotalGoldManufact_INDEX,
                TotalGoldManufact_WIDTH = entity.TotalGoldManufact_WIDTH,

                ExemptOfTax_INDEX = entity.ExemptOfTax_INDEX,
                ExemptOfTax_WIDTH = entity.ExemptOfTax_WIDTH,

                ItemPrice_INDEX = entity.ItemPrice_INDEX,
                ItemPrice_WIDTH = entity.ItemPrice_WIDTH,

                AfterDiscount_INDEX= entity.AfterDiscount_INDEX,
                AfterDiscount_WIDTH= entity.AfterDiscount_WIDTH,

                ARAfterDiscount= entity.ARAfterDiscount,
                ENAfterDiscount= entity.ENAfterDiscount,


                ARItemPrice = entity.ARItemPrice,
                ENItemPrice = entity.ENItemPrice,


                ARExemptOfTax = entity.ARExemptOfTax,
                ENExemptOfTax = entity.ENExemptOfTax,

                ARTotalGoldManufact = entity.ARTotalGoldManufact,
                ENTotalGoldManufact = entity.ENTotalGoldManufact,

                ARTotalItemGmWages = entity.ARTotalItemGmWages,
                ENTotalItemGmWages = entity.ENTotalItemGmWages,


                ARTaxTotal = entity.ARTaxTotal,
                ENTaxTotal = entity.ENTaxTotal,

                ARCode = entity.ARCode,
                ENCode = entity.ENCode,
                ARItem = entity.ARItem,
                ENItem = entity.ENItem,
                ARQuantity = entity.ARQuantity,
                ENQuantity = entity.ENQuantity,
                ARItemWeight = entity.ARItemWeight,
                ENItemWeight = entity.ENItemWeight,
                ARItemGmWages = entity.ARItemGmWages,
                ENItemGmWages = entity.ENItemGmWages,
                ARManufacturingWages = entity.ARManufacturingWages,
                ENManufacturingWages = entity.ENManufacturingWages,
                ARUnit = entity.ARUnit,
                ENUnit = entity.ENUnit,
                ARCaliberName = entity.ARCaliberName,
                ENCaliberName = entity.ENCaliberName,
                ARPrice = entity.ARPrice,
                ENPrice = entity.ENPrice,
                ARDiscount = entity.ARDiscount,
                ENDiscount = entity.ENDiscount,
                ARDiscRate = entity.ARDiscRate,
                ENDiscRate = entity.ENDiscRate,
                ARTotal = entity.ARTotal,
                ENTotal = entity.ENTotal,
                ARClearnessRate = entity.ARClearnessRate,
                ENClearnessRate = entity.ENClearnessRate,
                ARCaliber24 = entity.ARCaliber24,
                ENCaliber24 = entity.ENCaliber24,
                ARCaliber22 = entity.ARCaliber22,
                ENCaliber22 = entity.ENCaliber22,
                ARCaliber21 = entity.ARCaliber21,
                ENCaliber21 = entity.ENCaliber21,
                ARCaliber18 = entity.ARCaliber18,
                ENCaliber18 = entity.ENCaliber18,
                ARCostCenter = entity.ARCostCenter,
                ENCostCenter = entity.ENCostCenter,
                ARSubjectToVat = entity.ARSubjectToVat,
                ENSubjectToVat = entity.ENSubjectToVat,
                ARVatRate = entity.ARVatRate,
                ENVatRate = entity.ENVatRate,

                VatValue_INDEX = entity.VatValue_INDEX,
                VatValue_WIDTH = entity.VatValue_WIDTH,
                ARVatValue = entity.ARVatValue,
                ENVatValue = entity.ENVatValue,

                ARWagesDiscValue = entity.ARWagesDiscValue,
                ENWagesDiscValue = entity.ENWagesDiscValue,
                ARWagesDiscRate = entity.ARWagesDiscRate,
                ENWagesDiscRate = entity.ENWagesDiscRate,

                ARActualWeight = entity.ARActualWeight,
                ENActualWeight = entity.ENActualWeight,
                ARLockPrice = entity.ARLockPrice,
                ENLockPrice = entity.ENLockPrice,
                LockPrice_INDEX=entity.LockPrice_INDEX,
                LockPrice_WIDTH=entity.LockPrice_WIDTH



            };
            billGridColumnsMRepo.Delete(bgc, entity.BILL_GRID_ID);
            return true;
        }

        public Task<bool> DeleteAsync(BILL_GRID_COLUMNSVM entity)
        {
            return Task.Run<bool>(() =>
            {
                BILL_GRID_COLUMNS bgc = new BILL_GRID_COLUMNS
                {
                    AddedBy = entity.AddedBy,
                    AddedOn = entity.AddedOn,
                    BILL_GRID_ID = entity.BILL_GRID_ID,
                    BILL_REMARKS_INDEX = entity.BILL_REMARKS_INDEX,
                    BILL_REMARKS_WIDTH = entity.BILL_REMARKS_WIDTH,
                    BILL_SETTING_ID = entity.BILL_SETTING_ID,
                    CEXTERNALLEXPENSES_INDEX = entity.CEXTERNALLEXPENSES_INDEX,
                    CEXTERNALLEXPENSES_WIDTH = entity.CEXTERNALLEXPENSES_WIDTH,
                    CINVENTORYVALUE_INDEX = entity.CINVENTORYVALUE_INDEX,
                    CINVENTORYVALUE_WIDTH = entity.CINVENTORYVALUE_WIDTH,
                    CTOTALCURR_INDEX = entity.CTOTALCURR_INDEX,
                    CTOTALCURR_WIDTH = entity.CTOTALCURR_WIDTH,
                    CTOTALWITHEXPENSES_INDEX = entity.CTOTALWITHEXPENSES_INDEX,
                    CTOTALWITHEXPENSES_WIDTH = entity.CTOTALWITHEXPENSES_WIDTH,
                    DISC_RATE_INDEX = entity.DISC_RATE_INDEX,
                    DISC_RATE_WIDTH = entity.DISC_RATE_WIDTH,
                    DISC_VALUE_INDEX = entity.DISC_VALUE_INDEX,
                    DISC_VALUE_WIDTH = entity.DISC_VALUE_WIDTH,
                    EMPLOYEE_INDEX = entity.EMPLOYEE_INDEX,
                    EMPLOYEE_WIDTH = entity.EMPLOYEE_WIDTH,
                    END_USERS_INDEX = entity.END_USERS_INDEX,
                    END_USERS_WIDTH = entity.END_USERS_WIDTH,
                    EXPIRED_DATE_INDEX = entity.EXPIRED_DATE_INDEX,
                    EXPIRED_DATE_WIDTH = entity.EXPIRED_DATE_WIDTH,
                    EXPORT_INDEX = entity.EXPORT_INDEX,
                    EXPORT_WIDTH = entity.EXPORT_WIDTH,
                    EXTRA_RATE_INDEX = entity.EXTRA_RATE_INDEX,
                    EXTRA_RATE_WIDTH = entity.EXTRA_RATE_WIDTH,
                    EXTRA_VALUE_INDEX = entity.EXTRA_VALUE_INDEX,
                    EXTRA_VALUE_WIDTH = entity.EXTRA_VALUE_WIDTH,
                    GIFTS_INDEX = entity.GIFTS_INDEX,
                    GIFTS_WIDTH = entity.GIFTS_WIDTH,
                    HALF_WHOLE_INDEX = entity.HALF_WHOLE_INDEX,
                    HALF_WHOLE_WIDTH = entity.HALF_WHOLE_WIDTH,
                    ITEM_INDEX = entity.ITEM_INDEX,
                    ITEM_REMAIN_QTY_INDEX = entity.ITEM_REMAIN_QTY_INDEX,
                    ITEM_WIDTH = entity.ITEM_WIDTH,
                    ITEM_REMAIN_QTY_WIDTH = entity.ITEM_REMAIN_QTY_WIDTH,
                    LAST_BUY_INDEX = entity.LAST_BUY_INDEX,
                    LAST_BUY_WIDTH = entity.LAST_BUY_WIDTH,
                    PRODUCTION_DATE_INDEX = entity.PRODUCTION_DATE_INDEX,
                    PRODUCTION_DATE_WIDTH = entity.PRODUCTION_DATE_WIDTH,
                    QTY_INDEX = entity.QTY_INDEX,
                    QTY_WIDTH = entity.QTY_WIDTH,
                    RETAIL_INDEX = entity.RETAIL_INDEX,
                    RETAIL_WIDTH = entity.RETAIL_WIDTH,
                    TOTAL_INDEX = entity.TOTAL_INDEX,
                    TOTAL_WIDTH = entity.TOTAL_WIDTH,
                    UNIT_COST_INDEX = entity.UNIT_COST_INDEX,
                    UNIT_COST_WIDTH = entity.UNIT_COST_WIDTH,
                    UNIT_INDEX = entity.UNIT_INDEX,
                    UNIT_WIDTH = entity.UNIT_WIDTH,
                    UpdatedBy = entity.UpdatedBy,
                    updatedOn = entity.updatedOn,
                    WHOLE_INDEX = entity.WHOLE_INDEX,
                    WHOLE_WIDTH = entity.WHOLE_WIDTH,
                    Cost_Center_INDEX = entity.Cost_Center_INDEX,
                    Cost_Center_WIDTH = entity.Cost_Center_WIDTH,

                    Item_Weight_INDEX = entity.Item_Weight_INDEX,
                    Item_Weight_WIDTH = entity.Item_Weight_WIDTH,
                    Item_Wages_INDEX = entity.Item_Wages_INDEX,
                    Item_Wages_WIDTH = entity.Item_Wages_WIDTH,
                    Manufact_Wages_INDEX = entity.Manufact_Wages_INDEX,
                    Manufact_Wages_WIDTH = entity.Manufact_Wages_WIDTH,
                    ARName_INDEX = entity.ARName_INDEX,
                    ARName_WIDTH = entity.ARName_WIDTH,
                    Clearness_Rate_INDEX = entity.Clearness_Rate_INDEX,
                    Clearness_Rate_WIDTH = entity.Clearness_Rate_WIDTH,
                    Caliber24_INDEX = entity.Caliber24_INDEX,
                    Caliber24_WIDTH = entity.Caliber24_WIDTH,
                    Caliber22_INDEX = entity.Caliber22_INDEX,
                    Caliber22_WIDTH = entity.Caliber22_WIDTH,
                    Caliber21_INDEX = entity.Caliber21_INDEX,
                    Caliber21_WIDTH = entity.Caliber21_WIDTH,
                    Caliber18_INDEX = entity.Caliber18_INDEX,
                    Caliber18_WIDTH = entity.Caliber18_WIDTH,
                    Subject_To_VAT_INDEX = entity.Subject_To_VAT_INDEX,
                    Subject_To_VAT_WIDTH = entity.Subject_To_VAT_WIDTH,
                    VAT_Rate_INDEX = entity.VAT_Rate_INDEX,
                    VAT_Rate_WIDTH = entity.VAT_Rate_WIDTH,

                    ActualWeight_INDEX = entity.ActualWeight_INDEX,
                    ActualWeight_WIDTH = entity.ActualWeight_WIDTH,

                    TotalItemGmWages_INDEX = entity.TotalItemGmWages_INDEX,
                    TotalItemGmWages_WIDTH = entity.TotalItemGmWages_WIDTH,


                    TotalGoldManufact_INDEX = entity.TotalGoldManufact_INDEX,
                    TotalGoldManufact_WIDTH = entity.TotalGoldManufact_WIDTH,

                    ExemptOfTax_INDEX = entity.ExemptOfTax_INDEX,
                    ExemptOfTax_WIDTH = entity.ExemptOfTax_WIDTH,

                    ItemPrice_INDEX = entity.ItemPrice_INDEX,
                    ItemPrice_WIDTH = entity.ItemPrice_WIDTH,


                    AfterDiscount_INDEX = entity.AfterDiscount_INDEX,
                    AfterDiscount_WIDTH = entity.AfterDiscount_WIDTH,

                    ARAfterDiscount = entity.ARAfterDiscount,
                    ENAfterDiscount = entity.ENAfterDiscount,


                    ARItemPrice = entity.ARItemPrice,
                    ENItemPrice = entity.ENItemPrice,

                    ARExemptOfTax = entity.ARExemptOfTax,
                    ENExemptOfTax = entity.ENExemptOfTax,

                    ARTotalGoldManufact = entity.ARTotalGoldManufact,
                    ENTotalGoldManufact = entity.ENTotalGoldManufact,

                    ARTotalItemGmWages = entity.ARTotalItemGmWages,
                    ENTotalItemGmWages = entity.ENTotalItemGmWages,


                    ARActualWeight = entity.ARActualWeight,
                    ENActualWeight = entity.ENActualWeight,

                    TaxTotal_INDEX = entity.TaxTotal_INDEX,
                    TaxTotal_WIDTH = entity.TaxTotal_WIDTH,
                    ARTaxTotal = entity.ARTaxTotal,
                    ENTaxTotal = entity.ENTaxTotal,

                    ARCode = entity.ARCode,
                    ENCode = entity.ENCode,
                    ARItem = entity.ARItem,
                    ENItem = entity.ENItem,
                    ARQuantity = entity.ARQuantity,
                    ENQuantity = entity.ENQuantity,
                    ARItemWeight = entity.ARItemWeight,
                    ENItemWeight = entity.ENItemWeight,
                    ARItemGmWages = entity.ARItemGmWages,
                    ENItemGmWages = entity.ENItemGmWages,
                    ARManufacturingWages = entity.ARManufacturingWages,
                    ENManufacturingWages = entity.ENManufacturingWages,
                    ARUnit = entity.ARUnit,
                    ENUnit = entity.ENUnit,
                    ARCaliberName = entity.ARCaliberName,
                    ENCaliberName = entity.ENCaliberName,
                    ARPrice = entity.ARPrice,
                    ENPrice = entity.ENPrice,
                    ARDiscount = entity.ARDiscount,
                    ENDiscount = entity.ENDiscount,
                    ARDiscRate = entity.ARDiscRate,
                    ENDiscRate = entity.ENDiscRate,
                    ARTotal = entity.ARTotal,
                    ENTotal = entity.ENTotal,
                    ARClearnessRate = entity.ARClearnessRate,
                    ENClearnessRate = entity.ENClearnessRate,
                    ARCaliber24 = entity.ARCaliber24,
                    ENCaliber24 = entity.ENCaliber24,
                    ARCaliber22 = entity.ARCaliber22,
                    ENCaliber22 = entity.ENCaliber22,
                    ARCaliber21 = entity.ARCaliber21,
                    ENCaliber21 = entity.ENCaliber21,
                    ARCaliber18 = entity.ARCaliber18,
                    ENCaliber18 = entity.ENCaliber18,
                    ARCostCenter = entity.ARCostCenter,
                    ENCostCenter = entity.ENCostCenter,
                    ARSubjectToVat = entity.ARSubjectToVat,
                    ENSubjectToVat = entity.ENSubjectToVat,
                    ARVatRate = entity.ARVatRate,
                    ENVatRate = entity.ENVatRate,

                    VatValue_INDEX = entity.VatValue_INDEX,
                    VatValue_WIDTH = entity.VatValue_WIDTH,
                    ARVatValue = entity.ARVatValue,
                    ENVatValue = entity.ENVatValue,

                    WagesDiscValue_INDEX = entity.WagesDiscValue_INDEX,
                    WagesDiscValue_WIDTH = entity.WagesDiscValue_WIDTH,
                    WagesDiscRate_INDEX = entity.WagesDiscRate_INDEX,
                    WagesDiscRate_WIDTH = entity.WagesDiscRate_WIDTH,

                    ARWagesDiscValue = entity.ARWagesDiscValue,
                    ENWagesDiscValue = entity.ENWagesDiscValue,
                    ARWagesDiscRate = entity.ARWagesDiscRate,
                    ENWagesDiscRate = entity.ENWagesDiscRate,
                    ARLockPrice = entity.ARLockPrice,
                    ENLockPrice = entity.ENLockPrice,
                    LockPrice_INDEX = entity.LockPrice_INDEX,
                    LockPrice_WIDTH = entity.LockPrice_WIDTH

                };
                billGridColumnsMRepo.Delete(bgc, entity.BILL_GRID_ID);
                return true;
            });
        }

        public void Dispose()
        {
            billGridColumnsMRepo.Dispose();
        }

        public Task<List<BILL_GRID_COLUMNSVM>> GetAllAsync(int pageNum, int pageSize)
        {
            return Task.Run<List<BILL_GRID_COLUMNSVM>>(() =>
            {
                int rowCount;
                var q = from entity in billGridColumnsMRepo.GetPaged<long>(pageNum, pageSize, p => p.BILL_GRID_ID, false, out rowCount)
                        select new BILL_GRID_COLUMNSVM
                        {
                            AddedBy = entity.AddedBy,
                            AddedOn = entity.AddedOn,
                            BILL_GRID_ID = entity.BILL_GRID_ID,
                            BILL_REMARKS_INDEX = entity.BILL_REMARKS_INDEX,
                            BILL_REMARKS_WIDTH = entity.BILL_REMARKS_WIDTH,
                            BILL_SETTING_ID = entity.BILL_SETTING_ID,
                            CEXTERNALLEXPENSES_INDEX = entity.CEXTERNALLEXPENSES_INDEX,
                            CEXTERNALLEXPENSES_WIDTH = entity.CEXTERNALLEXPENSES_WIDTH,
                            CINVENTORYVALUE_INDEX = entity.CINVENTORYVALUE_INDEX,
                            CINVENTORYVALUE_WIDTH = entity.CINVENTORYVALUE_WIDTH,
                            CTOTALCURR_INDEX = entity.CTOTALCURR_INDEX,
                            CTOTALCURR_WIDTH = entity.CTOTALCURR_WIDTH,
                            CTOTALWITHEXPENSES_INDEX = entity.CTOTALWITHEXPENSES_INDEX,
                            CTOTALWITHEXPENSES_WIDTH = entity.CTOTALWITHEXPENSES_WIDTH,
                            DISC_RATE_INDEX = entity.DISC_RATE_INDEX,
                            DISC_RATE_WIDTH = entity.DISC_RATE_WIDTH,
                            DISC_VALUE_INDEX = entity.DISC_VALUE_INDEX,
                            DISC_VALUE_WIDTH = entity.DISC_VALUE_WIDTH,
                            EMPLOYEE_INDEX = entity.EMPLOYEE_INDEX,
                            EMPLOYEE_WIDTH = entity.EMPLOYEE_WIDTH,
                            END_USERS_INDEX = entity.END_USERS_INDEX,
                            END_USERS_WIDTH = entity.END_USERS_WIDTH,
                            EXPIRED_DATE_INDEX = entity.EXPIRED_DATE_INDEX,
                            EXPIRED_DATE_WIDTH = entity.EXPIRED_DATE_WIDTH,
                            EXPORT_INDEX = entity.EXPORT_INDEX,
                            EXPORT_WIDTH = entity.EXPORT_WIDTH,
                            EXTRA_RATE_INDEX = entity.EXTRA_RATE_INDEX,
                            EXTRA_RATE_WIDTH = entity.EXTRA_RATE_WIDTH,
                            EXTRA_VALUE_INDEX = entity.EXTRA_VALUE_INDEX,
                            EXTRA_VALUE_WIDTH = entity.EXTRA_VALUE_WIDTH,
                            GIFTS_INDEX = entity.GIFTS_INDEX,
                            GIFTS_WIDTH = entity.GIFTS_WIDTH,
                            HALF_WHOLE_INDEX = entity.HALF_WHOLE_INDEX,
                            HALF_WHOLE_WIDTH = entity.HALF_WHOLE_WIDTH,
                            ITEM_INDEX = entity.ITEM_INDEX,
                            ITEM_REMAIN_QTY_INDEX = entity.ITEM_REMAIN_QTY_INDEX,
                            ITEM_WIDTH = entity.ITEM_WIDTH,
                            ITEM_REMAIN_QTY_WIDTH = entity.ITEM_REMAIN_QTY_WIDTH,
                            LAST_BUY_INDEX = entity.LAST_BUY_INDEX,
                            LAST_BUY_WIDTH = entity.LAST_BUY_WIDTH,
                            PRODUCTION_DATE_INDEX = entity.PRODUCTION_DATE_INDEX,
                            PRODUCTION_DATE_WIDTH = entity.PRODUCTION_DATE_WIDTH,
                            QTY_INDEX = entity.QTY_INDEX,
                            QTY_WIDTH = entity.QTY_WIDTH,
                            RETAIL_INDEX = entity.RETAIL_INDEX,
                            RETAIL_WIDTH = entity.RETAIL_WIDTH,
                            TOTAL_INDEX = entity.TOTAL_INDEX,
                            TOTAL_WIDTH = entity.TOTAL_WIDTH,
                            UNIT_COST_INDEX = entity.UNIT_COST_INDEX,
                            UNIT_COST_WIDTH = entity.UNIT_COST_WIDTH,
                            UNIT_INDEX = entity.UNIT_INDEX,
                            UNIT_WIDTH = entity.UNIT_WIDTH,
                            UpdatedBy = entity.UpdatedBy,
                            updatedOn = entity.updatedOn,
                            WHOLE_INDEX = entity.WHOLE_INDEX,
                            WHOLE_WIDTH = entity.WHOLE_WIDTH,

                            Cost_Center_INDEX = entity.Cost_Center_INDEX,
                            Cost_Center_WIDTH = entity.Cost_Center_WIDTH,

                            Item_Weight_INDEX = entity.Item_Weight_INDEX,
                            Item_Weight_WIDTH = entity.Item_Weight_WIDTH,
                            Item_Wages_INDEX = entity.Item_Wages_INDEX,
                            Item_Wages_WIDTH = entity.Item_Wages_WIDTH,
                            Manufact_Wages_INDEX = entity.Manufact_Wages_INDEX,
                            Manufact_Wages_WIDTH = entity.Manufact_Wages_WIDTH,
                            ARName_INDEX = entity.ARName_INDEX,
                            ARName_WIDTH = entity.ARName_WIDTH,
                            Clearness_Rate_INDEX = entity.Clearness_Rate_INDEX,
                            Clearness_Rate_WIDTH = entity.Clearness_Rate_WIDTH,
                            Caliber24_INDEX = entity.Caliber24_INDEX,
                            Caliber24_WIDTH = entity.Caliber24_WIDTH,
                            Caliber22_INDEX = entity.Caliber22_INDEX,
                            Caliber22_WIDTH = entity.Caliber22_WIDTH,
                            Caliber21_INDEX = entity.Caliber21_INDEX,
                            Caliber21_WIDTH = entity.Caliber21_WIDTH,
                            Caliber18_INDEX = entity.Caliber18_INDEX,
                            Caliber18_WIDTH = entity.Caliber18_WIDTH,
                            Subject_To_VAT_INDEX = entity.Subject_To_VAT_INDEX,
                            Subject_To_VAT_WIDTH = entity.Subject_To_VAT_WIDTH,
                            VAT_Rate_INDEX = entity.VAT_Rate_INDEX,
                            VAT_Rate_WIDTH = entity.VAT_Rate_WIDTH,

                            ActualWeight_INDEX = entity.ActualWeight_INDEX,
                            ActualWeight_WIDTH = entity.ActualWeight_WIDTH,


                            TotalGoldManufact_INDEX = entity.TotalGoldManufact_INDEX,
                            TotalGoldManufact_WIDTH = entity.TotalGoldManufact_WIDTH,

                            ExemptOfTax_INDEX = entity.ExemptOfTax_INDEX,
                            ExemptOfTax_WIDTH = entity.ExemptOfTax_WIDTH,

                            ItemPrice_INDEX = entity.ItemPrice_INDEX,
                            ItemPrice_WIDTH = entity.ItemPrice_WIDTH,


                            AfterDiscount_INDEX = entity.AfterDiscount_INDEX,
                            AfterDiscount_WIDTH = entity.AfterDiscount_WIDTH,

                            ARAfterDiscount = entity.ARAfterDiscount,
                            ENAfterDiscount = entity.ENAfterDiscount,


                            ARItemPrice = entity.ARItemPrice,
                            ENItemPrice = entity.ENItemPrice,

                            ARExemptOfTax = entity.ARExemptOfTax,
                            ENExemptOfTax = entity.ENExemptOfTax,

                            ARTotalGoldManufact = entity.ARTotalGoldManufact,
                            ENTotalGoldManufact = entity.ENTotalGoldManufact,

                            ARActualWeight = entity.ARActualWeight,
                            ENActualWeight = entity.ENActualWeight,

                            TaxTotal_INDEX = entity.TaxTotal_INDEX,
                            TaxTotal_WIDTH = entity.TaxTotal_WIDTH,

                            TotalItemGmWages_INDEX = entity.TotalItemGmWages_INDEX,
                            TotalItemGmWages_WIDTH = entity.TotalItemGmWages_WIDTH,

                            ARTotalItemGmWages = entity.ARTotalItemGmWages,
                            ENTotalItemGmWages = entity.ENTotalItemGmWages,


                            ARTaxTotal = entity.ARTaxTotal,
                            ENTaxTotal = entity.ENTaxTotal,

                            ARCode = entity.ARCode,
                            ENCode = entity.ENCode,
                            ARItem = entity.ARItem,
                            ENItem = entity.ENItem,
                            ARQuantity = entity.ARQuantity,
                            ENQuantity = entity.ENQuantity,
                            ARItemWeight = entity.ARItemWeight,
                            ENItemWeight = entity.ENItemWeight,
                            ARItemGmWages = entity.ARItemGmWages,
                            ENItemGmWages = entity.ENItemGmWages,
                            ARManufacturingWages = entity.ARManufacturingWages,
                            ENManufacturingWages = entity.ENManufacturingWages,
                            ARUnit = entity.ARUnit,
                            ENUnit = entity.ENUnit,
                            ARCaliberName = entity.ARCaliberName,
                            ENCaliberName = entity.ENCaliberName,
                            ARPrice = entity.ARPrice,
                            ENPrice = entity.ENPrice,
                            ARDiscount = entity.ARDiscount,
                            ENDiscount = entity.ENDiscount,
                            ARDiscRate = entity.ARDiscRate,
                            ENDiscRate = entity.ENDiscRate,
                            ARTotal = entity.ARTotal,
                            ENTotal = entity.ENTotal,
                            ARClearnessRate = entity.ARClearnessRate,
                            ENClearnessRate = entity.ENClearnessRate,
                            ARCaliber24 = entity.ARCaliber24,
                            ENCaliber24 = entity.ENCaliber24,
                            ARCaliber22 = entity.ARCaliber22,
                            ENCaliber22 = entity.ENCaliber22,
                            ARCaliber21 = entity.ARCaliber21,
                            ENCaliber21 = entity.ENCaliber21,
                            ARCaliber18 = entity.ARCaliber18,
                            ENCaliber18 = entity.ENCaliber18,
                            ARCostCenter = entity.ARCostCenter,
                            ENCostCenter = entity.ENCostCenter,
                            ARSubjectToVat = entity.ARSubjectToVat,
                            ENSubjectToVat = entity.ENSubjectToVat,
                            ARVatRate = entity.ARVatRate,
                            ENVatRate = entity.ENVatRate,

                            VatValue_INDEX = entity.VatValue_INDEX,
                            VatValue_WIDTH = entity.VatValue_WIDTH,
                            ARVatValue = entity.ARVatValue,
                            ENVatValue = entity.ENVatValue,

                            WagesDiscValue_INDEX = entity.WagesDiscValue_INDEX,
                            WagesDiscValue_WIDTH = entity.WagesDiscValue_WIDTH,
                            WagesDiscRate_INDEX = entity.WagesDiscRate_INDEX,
                            WagesDiscRate_WIDTH = entity.WagesDiscRate_WIDTH,

                            ARWagesDiscValue = entity.ARWagesDiscValue,
                            ENWagesDiscValue = entity.ENWagesDiscValue,
                            ARWagesDiscRate = entity.ARWagesDiscRate,
                            ENWagesDiscRate = entity.ENWagesDiscRate,
                            ARLockPrice = entity.ARLockPrice,
                            ENLockPrice = entity.ENLockPrice,
                            LockPrice_INDEX = entity.LockPrice_INDEX,
                            LockPrice_WIDTH = entity.LockPrice_WIDTH
                        };
                return q.ToList();
            });
        }

        public bool Insert(BILL_GRID_COLUMNSVM entity)
        {
            BILL_GRID_COLUMNS bgc = new BILL_GRID_COLUMNS
            {
                AddedBy = entity.AddedBy,
                AddedOn = entity.AddedOn,
                BILL_GRID_ID = entity.BILL_GRID_ID,
                BILL_REMARKS_INDEX = entity.BILL_REMARKS_INDEX,
                BILL_REMARKS_WIDTH = entity.BILL_REMARKS_WIDTH,
                BILL_SETTING_ID = entity.BILL_SETTING_ID,
                CEXTERNALLEXPENSES_INDEX = entity.CEXTERNALLEXPENSES_INDEX,
                CEXTERNALLEXPENSES_WIDTH = entity.CEXTERNALLEXPENSES_WIDTH,
                CINVENTORYVALUE_INDEX = entity.CINVENTORYVALUE_INDEX,
                CINVENTORYVALUE_WIDTH = entity.CINVENTORYVALUE_WIDTH,
                CTOTALCURR_INDEX = entity.CTOTALCURR_INDEX,
                CTOTALCURR_WIDTH = entity.CTOTALCURR_WIDTH,
                CTOTALWITHEXPENSES_INDEX = entity.CTOTALWITHEXPENSES_INDEX,
                CTOTALWITHEXPENSES_WIDTH = entity.CTOTALWITHEXPENSES_WIDTH,
                DISC_RATE_INDEX = entity.DISC_RATE_INDEX,
                DISC_RATE_WIDTH = entity.DISC_RATE_WIDTH,
                DISC_VALUE_INDEX = entity.DISC_VALUE_INDEX,
                DISC_VALUE_WIDTH = entity.DISC_VALUE_WIDTH,
                EMPLOYEE_INDEX = entity.EMPLOYEE_INDEX,
                EMPLOYEE_WIDTH = entity.EMPLOYEE_WIDTH,
                END_USERS_INDEX = entity.END_USERS_INDEX,
                END_USERS_WIDTH = entity.END_USERS_WIDTH,
                EXPIRED_DATE_INDEX = entity.EXPIRED_DATE_INDEX,
                EXPIRED_DATE_WIDTH = entity.EXPIRED_DATE_WIDTH,
                EXPORT_INDEX = entity.EXPORT_INDEX,
                EXPORT_WIDTH = entity.EXPORT_WIDTH,
                EXTRA_RATE_INDEX = entity.EXTRA_RATE_INDEX,
                EXTRA_RATE_WIDTH = entity.EXTRA_RATE_WIDTH,
                EXTRA_VALUE_INDEX = entity.EXTRA_VALUE_INDEX,
                EXTRA_VALUE_WIDTH = entity.EXTRA_VALUE_WIDTH,
                GIFTS_INDEX = entity.GIFTS_INDEX,
                GIFTS_WIDTH = entity.GIFTS_WIDTH,
                HALF_WHOLE_INDEX = entity.HALF_WHOLE_INDEX,
                HALF_WHOLE_WIDTH = entity.HALF_WHOLE_WIDTH,
                ITEM_INDEX = entity.ITEM_INDEX,
                ITEM_REMAIN_QTY_INDEX = entity.ITEM_REMAIN_QTY_INDEX,
                ITEM_WIDTH = entity.ITEM_WIDTH,
                ITEM_REMAIN_QTY_WIDTH = entity.ITEM_REMAIN_QTY_WIDTH,
                LAST_BUY_INDEX = entity.LAST_BUY_INDEX,
                LAST_BUY_WIDTH = entity.LAST_BUY_WIDTH,
                PRODUCTION_DATE_INDEX = entity.PRODUCTION_DATE_INDEX,
                PRODUCTION_DATE_WIDTH = entity.PRODUCTION_DATE_WIDTH,
                QTY_INDEX = entity.QTY_INDEX,
                QTY_WIDTH = entity.QTY_WIDTH,
                RETAIL_INDEX = entity.RETAIL_INDEX,
                RETAIL_WIDTH = entity.RETAIL_WIDTH,
                TOTAL_INDEX = entity.TOTAL_INDEX,
                TOTAL_WIDTH = entity.TOTAL_WIDTH,
                UNIT_COST_INDEX = entity.UNIT_COST_INDEX,
                UNIT_COST_WIDTH = entity.UNIT_COST_WIDTH,
                UNIT_INDEX = entity.UNIT_INDEX,
                UNIT_WIDTH = entity.UNIT_WIDTH,
                UpdatedBy = entity.UpdatedBy,
                updatedOn = entity.updatedOn,
                WHOLE_INDEX = entity.WHOLE_INDEX,
                WHOLE_WIDTH = entity.WHOLE_WIDTH,
                Cost_Center_INDEX = entity.Cost_Center_INDEX,
                Cost_Center_WIDTH = entity.Cost_Center_WIDTH,

                Item_Weight_INDEX = entity.Item_Weight_INDEX,
                Item_Weight_WIDTH = entity.Item_Weight_WIDTH,
                Item_Wages_INDEX = entity.Item_Wages_INDEX,
                Item_Wages_WIDTH = entity.Item_Wages_WIDTH,
                Manufact_Wages_INDEX = entity.Manufact_Wages_INDEX,
                Manufact_Wages_WIDTH = entity.Manufact_Wages_WIDTH,
                ARName_INDEX = entity.ARName_INDEX,
                ARName_WIDTH = entity.ARName_WIDTH,
                Clearness_Rate_INDEX = entity.Clearness_Rate_INDEX,
                Clearness_Rate_WIDTH = entity.Clearness_Rate_WIDTH,
                Caliber24_INDEX = entity.Caliber24_INDEX,
                Caliber24_WIDTH = entity.Caliber24_WIDTH,
                Caliber22_INDEX = entity.Caliber22_INDEX,
                Caliber22_WIDTH = entity.Caliber22_WIDTH,
                Caliber21_INDEX = entity.Caliber21_INDEX,
                Caliber21_WIDTH = entity.Caliber21_WIDTH,
                Caliber18_INDEX = entity.Caliber18_INDEX,
                Caliber18_WIDTH = entity.Caliber18_WIDTH,
                Subject_To_VAT_INDEX = entity.Subject_To_VAT_INDEX,
                Subject_To_VAT_WIDTH = entity.Subject_To_VAT_WIDTH,
                VAT_Rate_INDEX = entity.VAT_Rate_INDEX,
                VAT_Rate_WIDTH = entity.VAT_Rate_WIDTH,

                ActualWeight_INDEX = entity.ActualWeight_INDEX,
                ActualWeight_WIDTH = entity.ActualWeight_WIDTH,
                ARActualWeight = entity.ARActualWeight,
                ENActualWeight = entity.ENActualWeight,

                TaxTotal_INDEX = entity.TaxTotal_INDEX,
                TaxTotal_WIDTH = entity.TaxTotal_WIDTH,

                TotalItemGmWages_INDEX = entity.TotalItemGmWages_INDEX,
                TotalItemGmWages_WIDTH = entity.TotalItemGmWages_WIDTH,

                TotalGoldManufact_INDEX = entity.TotalGoldManufact_INDEX,
                TotalGoldManufact_WIDTH = entity.TotalGoldManufact_WIDTH,

                ExemptOfTax_INDEX = entity.ExemptOfTax_INDEX,
                ExemptOfTax_WIDTH = entity.ExemptOfTax_WIDTH,

                ItemPrice_INDEX = entity.ItemPrice_INDEX,
                ItemPrice_WIDTH = entity.ItemPrice_WIDTH,

                AfterDiscount_INDEX = entity.AfterDiscount_INDEX,
                AfterDiscount_WIDTH = entity.AfterDiscount_WIDTH,

                ARAfterDiscount = entity.ARAfterDiscount,
                ENAfterDiscount = entity.ENAfterDiscount,


                ARItemPrice = entity.ARItemPrice,
                ENItemPrice = entity.ENItemPrice,

                ARExemptOfTax = entity.ARExemptOfTax,
                ENExemptOfTax = entity.ENExemptOfTax,

                ARTotalGoldManufact = entity.ARTotalGoldManufact,
                ENTotalGoldManufact = entity.ENTotalGoldManufact,

                ARTotalItemGmWages = entity.ARTotalItemGmWages,
                ENTotalItemGmWages = entity.ENTotalItemGmWages,

                ARTaxTotal = entity.ARTaxTotal,
                ENTaxTotal = entity.ENTaxTotal,

                ARCode = entity.ARCode,
                ENCode = entity.ENCode,
                ARItem = entity.ARItem,
                ENItem = entity.ENItem,
                ARQuantity = entity.ARQuantity,
                ENQuantity = entity.ENQuantity,
                ARItemWeight = entity.ARItemWeight,
                ENItemWeight = entity.ENItemWeight,
                ARItemGmWages = entity.ARItemGmWages,
                ENItemGmWages = entity.ENItemGmWages,
                ARManufacturingWages = entity.ARManufacturingWages,
                ENManufacturingWages = entity.ENManufacturingWages,
                ARUnit = entity.ARUnit,
                ENUnit = entity.ENUnit,
                ARCaliberName = entity.ARCaliberName,
                ENCaliberName = entity.ENCaliberName,
                ARPrice = entity.ARPrice,
                ENPrice = entity.ENPrice,
                ARDiscount = entity.ARDiscount,
                ENDiscount = entity.ENDiscount,
                ARDiscRate = entity.ARDiscRate,
                ENDiscRate = entity.ENDiscRate,
                ARTotal = entity.ARTotal,
                ENTotal = entity.ENTotal,
                ARClearnessRate = entity.ARClearnessRate,
                ENClearnessRate = entity.ENClearnessRate,
                ARCaliber24 = entity.ARCaliber24,
                ENCaliber24 = entity.ENCaliber24,
                ARCaliber22 = entity.ARCaliber22,
                ENCaliber22 = entity.ENCaliber22,
                ARCaliber21 = entity.ARCaliber21,
                ENCaliber21 = entity.ENCaliber21,
                ARCaliber18 = entity.ARCaliber18,
                ENCaliber18 = entity.ENCaliber18,
                ARCostCenter = entity.ARCostCenter,
                ENCostCenter = entity.ENCostCenter,
                ARSubjectToVat = entity.ARSubjectToVat,
                ENSubjectToVat = entity.ENSubjectToVat,
                ARVatRate = entity.ARVatRate,
                ENVatRate = entity.ENVatRate,

                VatValue_INDEX = entity.VatValue_INDEX,
                VatValue_WIDTH = entity.VatValue_WIDTH,
                ARVatValue = entity.ARVatValue,
                ENVatValue = entity.ENVatValue,

                WagesDiscValue_INDEX = entity.WagesDiscValue_INDEX,
                WagesDiscValue_WIDTH = entity.WagesDiscValue_WIDTH,
                WagesDiscRate_INDEX = entity.WagesDiscRate_INDEX,
                WagesDiscRate_WIDTH = entity.WagesDiscRate_WIDTH,

                ARWagesDiscValue = entity.ARWagesDiscValue,
                ENWagesDiscValue = entity.ENWagesDiscValue,
                ARWagesDiscRate = entity.ARWagesDiscRate,
                ENWagesDiscRate = entity.ENWagesDiscRate,
                ARLockPrice = entity.ARLockPrice,
                ENLockPrice = entity.ENLockPrice,
                LockPrice_INDEX = entity.LockPrice_INDEX,
                LockPrice_WIDTH = entity.LockPrice_WIDTH

            };
            billGridColumnsMRepo.Add(bgc);
            return true;
        }

        public Task<int> InsertAsync(BILL_GRID_COLUMNSVM entity)
        {
            return Task.Run<int>(() =>
            {
                BILL_GRID_COLUMNS bgc = new BILL_GRID_COLUMNS
                {
                    AddedBy = entity.AddedBy,
                    AddedOn = entity.AddedOn,
                    BILL_GRID_ID = entity.BILL_GRID_ID,
                    BILL_REMARKS_INDEX = entity.BILL_REMARKS_INDEX,
                    BILL_REMARKS_WIDTH = entity.BILL_REMARKS_WIDTH,
                    BILL_SETTING_ID = entity.BILL_SETTING_ID,
                    CEXTERNALLEXPENSES_INDEX = entity.CEXTERNALLEXPENSES_INDEX,
                    CEXTERNALLEXPENSES_WIDTH = entity.CEXTERNALLEXPENSES_WIDTH,
                    CINVENTORYVALUE_INDEX = entity.CINVENTORYVALUE_INDEX,
                    CINVENTORYVALUE_WIDTH = entity.CINVENTORYVALUE_WIDTH,
                    CTOTALCURR_INDEX = entity.CTOTALCURR_INDEX,
                    CTOTALCURR_WIDTH = entity.CTOTALCURR_WIDTH,
                    CTOTALWITHEXPENSES_INDEX = entity.CTOTALWITHEXPENSES_INDEX,
                    CTOTALWITHEXPENSES_WIDTH = entity.CTOTALWITHEXPENSES_WIDTH,
                    DISC_RATE_INDEX = entity.DISC_RATE_INDEX,
                    DISC_RATE_WIDTH = entity.DISC_RATE_WIDTH,
                    DISC_VALUE_INDEX = entity.DISC_VALUE_INDEX,
                    DISC_VALUE_WIDTH = entity.DISC_VALUE_WIDTH,
                    EMPLOYEE_INDEX = entity.EMPLOYEE_INDEX,
                    EMPLOYEE_WIDTH = entity.EMPLOYEE_WIDTH,
                    END_USERS_INDEX = entity.END_USERS_INDEX,
                    END_USERS_WIDTH = entity.END_USERS_WIDTH,
                    EXPIRED_DATE_INDEX = entity.EXPIRED_DATE_INDEX,
                    EXPIRED_DATE_WIDTH = entity.EXPIRED_DATE_WIDTH,
                    EXPORT_INDEX = entity.EXPORT_INDEX,
                    EXPORT_WIDTH = entity.EXPORT_WIDTH,
                    EXTRA_RATE_INDEX = entity.EXTRA_RATE_INDEX,
                    EXTRA_RATE_WIDTH = entity.EXTRA_RATE_WIDTH,
                    EXTRA_VALUE_INDEX = entity.EXTRA_VALUE_INDEX,
                    EXTRA_VALUE_WIDTH = entity.EXTRA_VALUE_WIDTH,
                    GIFTS_INDEX = entity.GIFTS_INDEX,
                    GIFTS_WIDTH = entity.GIFTS_WIDTH,
                    HALF_WHOLE_INDEX = entity.HALF_WHOLE_INDEX,
                    HALF_WHOLE_WIDTH = entity.HALF_WHOLE_WIDTH,
                    ITEM_INDEX = entity.ITEM_INDEX,
                    ITEM_REMAIN_QTY_INDEX = entity.ITEM_REMAIN_QTY_INDEX,
                    ITEM_WIDTH = entity.ITEM_WIDTH,
                    ITEM_REMAIN_QTY_WIDTH = entity.ITEM_REMAIN_QTY_WIDTH,
                    LAST_BUY_INDEX = entity.LAST_BUY_INDEX,
                    LAST_BUY_WIDTH = entity.LAST_BUY_WIDTH,
                    PRODUCTION_DATE_INDEX = entity.PRODUCTION_DATE_INDEX,
                    PRODUCTION_DATE_WIDTH = entity.PRODUCTION_DATE_WIDTH,
                    QTY_INDEX = entity.QTY_INDEX,
                    QTY_WIDTH = entity.QTY_WIDTH,
                    RETAIL_INDEX = entity.RETAIL_INDEX,
                    RETAIL_WIDTH = entity.RETAIL_WIDTH,
                    TOTAL_INDEX = entity.TOTAL_INDEX,
                    TOTAL_WIDTH = entity.TOTAL_WIDTH,
                    UNIT_COST_INDEX = entity.UNIT_COST_INDEX,
                    UNIT_COST_WIDTH = entity.UNIT_COST_WIDTH,
                    UNIT_INDEX = entity.UNIT_INDEX,
                    UNIT_WIDTH = entity.UNIT_WIDTH,
                    UpdatedBy = entity.UpdatedBy,
                    updatedOn = entity.updatedOn,
                    WHOLE_INDEX = entity.WHOLE_INDEX,
                    WHOLE_WIDTH = entity.WHOLE_WIDTH,

                    Cost_Center_INDEX = entity.Cost_Center_INDEX,
                    Cost_Center_WIDTH = entity.Cost_Center_WIDTH,

                    Item_Weight_INDEX = entity.Item_Weight_INDEX,
                    Item_Weight_WIDTH = entity.Item_Weight_WIDTH,
                    Item_Wages_INDEX = entity.Item_Wages_INDEX,
                    Item_Wages_WIDTH = entity.Item_Wages_WIDTH,
                    Manufact_Wages_INDEX = entity.Manufact_Wages_INDEX,
                    Manufact_Wages_WIDTH = entity.Manufact_Wages_WIDTH,
                    ARName_INDEX = entity.ARName_INDEX,
                    ARName_WIDTH = entity.ARName_WIDTH,
                    Clearness_Rate_INDEX = entity.Clearness_Rate_INDEX,
                    Clearness_Rate_WIDTH = entity.Clearness_Rate_WIDTH,
                    Caliber24_INDEX = entity.Caliber24_INDEX,
                    Caliber24_WIDTH = entity.Caliber24_WIDTH,
                    Caliber22_INDEX = entity.Caliber22_INDEX,
                    Caliber22_WIDTH = entity.Caliber22_WIDTH,
                    Caliber21_INDEX = entity.Caliber21_INDEX,
                    Caliber21_WIDTH = entity.Caliber21_WIDTH,
                    Caliber18_INDEX = entity.Caliber18_INDEX,
                    Caliber18_WIDTH = entity.Caliber18_WIDTH,
                    Subject_To_VAT_INDEX = entity.Subject_To_VAT_INDEX,
                    Subject_To_VAT_WIDTH = entity.Subject_To_VAT_WIDTH,
                    VAT_Rate_INDEX = entity.VAT_Rate_INDEX,
                    VAT_Rate_WIDTH = entity.VAT_Rate_WIDTH,

                    ActualWeight_INDEX = entity.ActualWeight_INDEX,
                    ActualWeight_WIDTH = entity.ActualWeight_WIDTH,

                    TotalItemGmWages_INDEX = entity.TotalItemGmWages_INDEX,
                    TotalItemGmWages_WIDTH = entity.TotalItemGmWages_WIDTH,

                    ItemPrice_INDEX = entity.ItemPrice_INDEX,
                    ItemPrice_WIDTH = entity.ItemPrice_WIDTH,


                    AfterDiscount_INDEX = entity.AfterDiscount_INDEX,
                    AfterDiscount_WIDTH = entity.AfterDiscount_WIDTH,

                    ARAfterDiscount = entity.ARAfterDiscount,
                    ENAfterDiscount = entity.ENAfterDiscount,


                    ARItemPrice = entity.ARItemPrice,
                    ENItemPrice = entity.ENItemPrice,

                    ARTotalItemGmWages = entity.ARTotalItemGmWages,
                    ENTotalItemGmWages = entity.ENTotalItemGmWages,


                    ARActualWeight = entity.ARActualWeight,
                    ENActualWeight = entity.ENActualWeight,


                    TaxTotal_INDEX = entity.TaxTotal_INDEX,
                    TaxTotal_WIDTH = entity.TaxTotal_WIDTH,

                    TotalGoldManufact_INDEX = entity.TotalGoldManufact_INDEX,
                    TotalGoldManufact_WIDTH = entity.TotalGoldManufact_WIDTH,

                    ExemptOfTax_INDEX = entity.ExemptOfTax_INDEX,
                    ExemptOfTax_WIDTH = entity.ExemptOfTax_WIDTH,


                    ARExemptOfTax = entity.ARExemptOfTax,
                    ENExemptOfTax = entity.ENExemptOfTax,

                    ARTotalGoldManufact = entity.ARTotalGoldManufact,
                    ENTotalGoldManufact = entity.ENTotalGoldManufact,

                    ARTaxTotal = entity.ARTaxTotal,
                    ENTaxTotal = entity.ENTaxTotal,

                    ARCode = entity.ARCode,
                    ENCode = entity.ENCode,
                    ARItem = entity.ARItem,
                    ENItem = entity.ENItem,
                    ARQuantity = entity.ARQuantity,
                    ENQuantity = entity.ENQuantity,
                    ARItemWeight = entity.ARItemWeight,
                    ENItemWeight = entity.ENItemWeight,
                    ARItemGmWages = entity.ARItemGmWages,
                    ENItemGmWages = entity.ENItemGmWages,
                    ARManufacturingWages = entity.ARManufacturingWages,
                    ENManufacturingWages = entity.ENManufacturingWages,
                    ARUnit = entity.ARUnit,
                    ENUnit = entity.ENUnit,
                    ARCaliberName = entity.ARCaliberName,
                    ENCaliberName = entity.ENCaliberName,
                    ARPrice = entity.ARPrice,
                    ENPrice = entity.ENPrice,
                    ARDiscount = entity.ARDiscount,
                    ENDiscount = entity.ENDiscount,
                    ARDiscRate = entity.ARDiscRate,
                    ENDiscRate = entity.ENDiscRate,
                    ARTotal = entity.ARTotal,
                    ENTotal = entity.ENTotal,
                    ARClearnessRate = entity.ARClearnessRate,
                    ENClearnessRate = entity.ENClearnessRate,
                    ARCaliber24 = entity.ARCaliber24,
                    ENCaliber24 = entity.ENCaliber24,
                    ARCaliber22 = entity.ARCaliber22,
                    ENCaliber22 = entity.ENCaliber22,
                    ARCaliber21 = entity.ARCaliber21,
                    ENCaliber21 = entity.ENCaliber21,
                    ARCaliber18 = entity.ARCaliber18,
                    ENCaliber18 = entity.ENCaliber18,
                    ARCostCenter = entity.ARCostCenter,
                    ENCostCenter = entity.ENCostCenter,
                    ARSubjectToVat = entity.ARSubjectToVat,
                    ENSubjectToVat = entity.ENSubjectToVat,
                    ARVatRate = entity.ARVatRate,
                    ENVatRate = entity.ENVatRate,

                    VatValue_INDEX = entity.VatValue_INDEX,
                    VatValue_WIDTH = entity.VatValue_WIDTH,
                    ARVatValue = entity.ARVatValue,
                    ENVatValue = entity.ENVatValue,

                    WagesDiscValue_INDEX = entity.WagesDiscValue_INDEX,
                    WagesDiscValue_WIDTH = entity.WagesDiscValue_WIDTH,
                    WagesDiscRate_INDEX = entity.WagesDiscRate_INDEX,
                    WagesDiscRate_WIDTH = entity.WagesDiscRate_WIDTH,

                    ARWagesDiscValue = entity.ARWagesDiscValue,
                    ENWagesDiscValue = entity.ENWagesDiscValue,
                    ARWagesDiscRate = entity.ARWagesDiscRate,
                    ENWagesDiscRate = entity.ENWagesDiscRate,
                    ARLockPrice = entity.ARLockPrice,
                    ENLockPrice = entity.ENLockPrice,
                    LockPrice_INDEX = entity.LockPrice_INDEX,
                    LockPrice_WIDTH = entity.LockPrice_WIDTH

                };
                billGridColumnsMRepo.Add(bgc);
                return bgc.BILL_GRID_ID;
            });
        }

        public bool Update(BILL_GRID_COLUMNSVM entity)
        {
            BILL_GRID_COLUMNS bgc = new BILL_GRID_COLUMNS
            {
                AddedBy = entity.AddedBy,
                AddedOn = entity.AddedOn,
                BILL_GRID_ID = entity.BILL_GRID_ID,
                BILL_REMARKS_INDEX = entity.BILL_REMARKS_INDEX,
                BILL_REMARKS_WIDTH = entity.BILL_REMARKS_WIDTH,
                BILL_SETTING_ID = entity.BILL_SETTING_ID,
                CEXTERNALLEXPENSES_INDEX = entity.CEXTERNALLEXPENSES_INDEX,
                CEXTERNALLEXPENSES_WIDTH = entity.CEXTERNALLEXPENSES_WIDTH,
                CINVENTORYVALUE_INDEX = entity.CINVENTORYVALUE_INDEX,
                CINVENTORYVALUE_WIDTH = entity.CINVENTORYVALUE_WIDTH,
                CTOTALCURR_INDEX = entity.CTOTALCURR_INDEX,
                CTOTALCURR_WIDTH = entity.CTOTALCURR_WIDTH,
                CTOTALWITHEXPENSES_INDEX = entity.CTOTALWITHEXPENSES_INDEX,
                CTOTALWITHEXPENSES_WIDTH = entity.CTOTALWITHEXPENSES_WIDTH,
                DISC_RATE_INDEX = entity.DISC_RATE_INDEX,
                DISC_RATE_WIDTH = entity.DISC_RATE_WIDTH,
                DISC_VALUE_INDEX = entity.DISC_VALUE_INDEX,
                DISC_VALUE_WIDTH = entity.DISC_VALUE_WIDTH,
                EMPLOYEE_INDEX = entity.EMPLOYEE_INDEX,
                EMPLOYEE_WIDTH = entity.EMPLOYEE_WIDTH,
                END_USERS_INDEX = entity.END_USERS_INDEX,
                END_USERS_WIDTH = entity.END_USERS_WIDTH,
                EXPIRED_DATE_INDEX = entity.EXPIRED_DATE_INDEX,
                EXPIRED_DATE_WIDTH = entity.EXPIRED_DATE_WIDTH,
                EXPORT_INDEX = entity.EXPORT_INDEX,
                EXPORT_WIDTH = entity.EXPORT_WIDTH,
                EXTRA_RATE_INDEX = entity.EXTRA_RATE_INDEX,
                EXTRA_RATE_WIDTH = entity.EXTRA_RATE_WIDTH,
                EXTRA_VALUE_INDEX = entity.EXTRA_VALUE_INDEX,
                EXTRA_VALUE_WIDTH = entity.EXTRA_VALUE_WIDTH,
                GIFTS_INDEX = entity.GIFTS_INDEX,
                GIFTS_WIDTH = entity.GIFTS_WIDTH,
                HALF_WHOLE_INDEX = entity.HALF_WHOLE_INDEX,
                HALF_WHOLE_WIDTH = entity.HALF_WHOLE_WIDTH,
                ITEM_INDEX = entity.ITEM_INDEX,
                ITEM_REMAIN_QTY_INDEX = entity.ITEM_REMAIN_QTY_INDEX,
                ITEM_WIDTH = entity.ITEM_WIDTH,
                ITEM_REMAIN_QTY_WIDTH = entity.ITEM_REMAIN_QTY_WIDTH,
                LAST_BUY_INDEX = entity.LAST_BUY_INDEX,
                LAST_BUY_WIDTH = entity.LAST_BUY_WIDTH,
                PRODUCTION_DATE_INDEX = entity.PRODUCTION_DATE_INDEX,
                PRODUCTION_DATE_WIDTH = entity.PRODUCTION_DATE_WIDTH,
                QTY_INDEX = entity.QTY_INDEX,
                QTY_WIDTH = entity.QTY_WIDTH,
                RETAIL_INDEX = entity.RETAIL_INDEX,
                RETAIL_WIDTH = entity.RETAIL_WIDTH,
                TOTAL_INDEX = entity.TOTAL_INDEX,
                TOTAL_WIDTH = entity.TOTAL_WIDTH,
                UNIT_COST_INDEX = entity.UNIT_COST_INDEX,
                UNIT_COST_WIDTH = entity.UNIT_COST_WIDTH,
                UNIT_INDEX = entity.UNIT_INDEX,
                UNIT_WIDTH = entity.UNIT_WIDTH,
                UpdatedBy = entity.UpdatedBy,
                updatedOn = entity.updatedOn,
                WHOLE_INDEX = entity.WHOLE_INDEX,
                WHOLE_WIDTH = entity.WHOLE_WIDTH,
                Cost_Center_INDEX = entity.Cost_Center_INDEX,
                Cost_Center_WIDTH = entity.Cost_Center_WIDTH,

                Item_Weight_INDEX = entity.Item_Weight_INDEX,
                Item_Weight_WIDTH = entity.Item_Weight_WIDTH,
                Item_Wages_INDEX = entity.Item_Wages_INDEX,
                Item_Wages_WIDTH = entity.Item_Wages_WIDTH,
                Manufact_Wages_INDEX = entity.Manufact_Wages_INDEX,
                Manufact_Wages_WIDTH = entity.Manufact_Wages_WIDTH,
                ARName_INDEX = entity.ARName_INDEX,
                ARName_WIDTH = entity.ARName_WIDTH,
                Clearness_Rate_INDEX = entity.Clearness_Rate_INDEX,
                Clearness_Rate_WIDTH = entity.Clearness_Rate_WIDTH,
                Caliber24_INDEX = entity.Caliber24_INDEX,
                Caliber24_WIDTH = entity.Caliber24_WIDTH,
                Caliber22_INDEX = entity.Caliber22_INDEX,
                Caliber22_WIDTH = entity.Caliber22_WIDTH,
                Caliber21_INDEX = entity.Caliber21_INDEX,
                Caliber21_WIDTH = entity.Caliber21_WIDTH,
                Caliber18_INDEX = entity.Caliber18_INDEX,
                Caliber18_WIDTH = entity.Caliber18_WIDTH,
                Subject_To_VAT_INDEX = entity.Subject_To_VAT_INDEX,
                Subject_To_VAT_WIDTH = entity.Subject_To_VAT_WIDTH,
                VAT_Rate_INDEX = entity.VAT_Rate_INDEX,
                VAT_Rate_WIDTH = entity.VAT_Rate_WIDTH,

                ActualWeight_INDEX = entity.ActualWeight_INDEX,
                ActualWeight_WIDTH = entity.ActualWeight_WIDTH,
                ARActualWeight = entity.ARActualWeight,
                ENActualWeight = entity.ENActualWeight,

                TaxTotal_INDEX = entity.TaxTotal_INDEX,
                TaxTotal_WIDTH = entity.TaxTotal_WIDTH,

                TotalItemGmWages_INDEX = entity.TotalItemGmWages_INDEX,
                TotalItemGmWages_WIDTH = entity.TotalItemGmWages_WIDTH,


                TotalGoldManufact_INDEX = entity.TotalGoldManufact_INDEX,
                TotalGoldManufact_WIDTH = entity.TotalGoldManufact_WIDTH,

                ExemptOfTax_INDEX = entity.ExemptOfTax_INDEX,
                ExemptOfTax_WIDTH = entity.ExemptOfTax_WIDTH,

                ItemPrice_INDEX = entity.ItemPrice_INDEX,
                ItemPrice_WIDTH = entity.ItemPrice_WIDTH,


                AfterDiscount_INDEX = entity.AfterDiscount_INDEX,
                AfterDiscount_WIDTH = entity.AfterDiscount_WIDTH,

                ARAfterDiscount = entity.ARAfterDiscount,
                ENAfterDiscount = entity.ENAfterDiscount,


                ARItemPrice = entity.ARItemPrice,
                ENItemPrice = entity.ENItemPrice,

                ARExemptOfTax = entity.ARExemptOfTax,
                ENExemptOfTax = entity.ENExemptOfTax,

                ARTotalGoldManufact = entity.ARTotalGoldManufact,
                ENTotalGoldManufact = entity.ENTotalGoldManufact,

                ARTotalItemGmWages = entity.ARTotalItemGmWages,
                ENTotalItemGmWages = entity.ENTotalItemGmWages,


                ARTaxTotal = entity.ARTaxTotal,
                ENTaxTotal = entity.ENTaxTotal,


                ARCode = entity.ARCode,
                ENCode = entity.ENCode,
                ARItem = entity.ARItem,
                ENItem = entity.ENItem,
                ARQuantity = entity.ARQuantity,
                ENQuantity = entity.ENQuantity,
                ARItemWeight = entity.ARItemWeight,
                ENItemWeight = entity.ENItemWeight,
                ARItemGmWages = entity.ARItemGmWages,
                ENItemGmWages = entity.ENItemGmWages,
                ARManufacturingWages = entity.ARManufacturingWages,
                ENManufacturingWages = entity.ENManufacturingWages,
                ARUnit = entity.ARUnit,
                ENUnit = entity.ENUnit,
                ARCaliberName = entity.ARCaliberName,
                ENCaliberName = entity.ENCaliberName,
                ARPrice = entity.ARPrice,
                ENPrice = entity.ENPrice,
                ARDiscount = entity.ARDiscount,
                ENDiscount = entity.ENDiscount,
                ARDiscRate = entity.ARDiscRate,
                ENDiscRate = entity.ENDiscRate,
                ARTotal = entity.ARTotal,
                ENTotal = entity.ENTotal,
                ARClearnessRate = entity.ARClearnessRate,
                ENClearnessRate = entity.ENClearnessRate,
                ARCaliber24 = entity.ARCaliber24,
                ENCaliber24 = entity.ENCaliber24,
                ARCaliber22 = entity.ARCaliber22,
                ENCaliber22 = entity.ENCaliber22,
                ARCaliber21 = entity.ARCaliber21,
                ENCaliber21 = entity.ENCaliber21,
                ARCaliber18 = entity.ARCaliber18,
                ENCaliber18 = entity.ENCaliber18,
                ARCostCenter = entity.ARCostCenter,
                ENCostCenter = entity.ENCostCenter,
                ARSubjectToVat = entity.ARSubjectToVat,
                ENSubjectToVat = entity.ENSubjectToVat,
                ARVatRate = entity.ARVatRate,
                ENVatRate = entity.ENVatRate,

                VatValue_INDEX = entity.VatValue_INDEX,
                VatValue_WIDTH = entity.VatValue_WIDTH,
                ARVatValue = entity.ARVatValue,
                ENVatValue = entity.ENVatValue,

                WagesDiscValue_INDEX = entity.WagesDiscValue_INDEX,
                WagesDiscValue_WIDTH = entity.WagesDiscValue_WIDTH,
                WagesDiscRate_INDEX = entity.WagesDiscRate_INDEX,
                WagesDiscRate_WIDTH = entity.WagesDiscRate_WIDTH,

                ARWagesDiscValue = entity.ARWagesDiscValue,
                ENWagesDiscValue = entity.ENWagesDiscValue,
                ARWagesDiscRate = entity.ARWagesDiscRate,
                ENWagesDiscRate = entity.ENWagesDiscRate,
                ARLockPrice = entity.ARLockPrice,
                ENLockPrice = entity.ENLockPrice,
                LockPrice_INDEX = entity.LockPrice_INDEX,
                LockPrice_WIDTH = entity.LockPrice_WIDTH

            };
            billGridColumnsMRepo.Update(bgc, bgc.BILL_GRID_ID);
            return true;
        }

        public Task<bool> UpdateAsync(BILL_GRID_COLUMNSVM entity)
        {
            return Task.Run<bool>(() =>
            {
                BILL_GRID_COLUMNS bgc = new BILL_GRID_COLUMNS
                {
                    AddedBy = entity.AddedBy,
                    AddedOn = entity.AddedOn,
                    BILL_GRID_ID = entity.BILL_GRID_ID,
                    BILL_REMARKS_INDEX = entity.BILL_REMARKS_INDEX,
                    BILL_REMARKS_WIDTH = entity.BILL_REMARKS_WIDTH,
                    BILL_SETTING_ID = entity.BILL_SETTING_ID,
                    CEXTERNALLEXPENSES_INDEX = entity.CEXTERNALLEXPENSES_INDEX,
                    CEXTERNALLEXPENSES_WIDTH = entity.CEXTERNALLEXPENSES_WIDTH,
                    CINVENTORYVALUE_INDEX = entity.CINVENTORYVALUE_INDEX,
                    CINVENTORYVALUE_WIDTH = entity.CINVENTORYVALUE_WIDTH,
                    CTOTALCURR_INDEX = entity.CTOTALCURR_INDEX,
                    CTOTALCURR_WIDTH = entity.CTOTALCURR_WIDTH,
                    CTOTALWITHEXPENSES_INDEX = entity.CTOTALWITHEXPENSES_INDEX,
                    CTOTALWITHEXPENSES_WIDTH = entity.CTOTALWITHEXPENSES_WIDTH,
                    DISC_RATE_INDEX = entity.DISC_RATE_INDEX,
                    DISC_RATE_WIDTH = entity.DISC_RATE_WIDTH,
                    DISC_VALUE_INDEX = entity.DISC_VALUE_INDEX,
                    DISC_VALUE_WIDTH = entity.DISC_VALUE_WIDTH,
                    EMPLOYEE_INDEX = entity.EMPLOYEE_INDEX,
                    EMPLOYEE_WIDTH = entity.EMPLOYEE_WIDTH,
                    END_USERS_INDEX = entity.END_USERS_INDEX,
                    END_USERS_WIDTH = entity.END_USERS_WIDTH,
                    EXPIRED_DATE_INDEX = entity.EXPIRED_DATE_INDEX,
                    EXPIRED_DATE_WIDTH = entity.EXPIRED_DATE_WIDTH,
                    EXPORT_INDEX = entity.EXPORT_INDEX,
                    EXPORT_WIDTH = entity.EXPORT_WIDTH,
                    EXTRA_RATE_INDEX = entity.EXTRA_RATE_INDEX,
                    EXTRA_RATE_WIDTH = entity.EXTRA_RATE_WIDTH,
                    EXTRA_VALUE_INDEX = entity.EXTRA_VALUE_INDEX,
                    EXTRA_VALUE_WIDTH = entity.EXTRA_VALUE_WIDTH,
                    GIFTS_INDEX = entity.GIFTS_INDEX,
                    GIFTS_WIDTH = entity.GIFTS_WIDTH,
                    HALF_WHOLE_INDEX = entity.HALF_WHOLE_INDEX,
                    HALF_WHOLE_WIDTH = entity.HALF_WHOLE_WIDTH,
                    ITEM_INDEX = entity.ITEM_INDEX,
                    ITEM_REMAIN_QTY_INDEX = entity.ITEM_REMAIN_QTY_INDEX,
                    ITEM_WIDTH = entity.ITEM_WIDTH,
                    ITEM_REMAIN_QTY_WIDTH = entity.ITEM_REMAIN_QTY_WIDTH,
                    LAST_BUY_INDEX = entity.LAST_BUY_INDEX,
                    LAST_BUY_WIDTH = entity.LAST_BUY_WIDTH,
                    PRODUCTION_DATE_INDEX = entity.PRODUCTION_DATE_INDEX,
                    PRODUCTION_DATE_WIDTH = entity.PRODUCTION_DATE_WIDTH,
                    QTY_INDEX = entity.QTY_INDEX,
                    QTY_WIDTH = entity.QTY_WIDTH,
                    RETAIL_INDEX = entity.RETAIL_INDEX,
                    RETAIL_WIDTH = entity.RETAIL_WIDTH,
                    TOTAL_INDEX = entity.TOTAL_INDEX,
                    TOTAL_WIDTH = entity.TOTAL_WIDTH,
                    UNIT_COST_INDEX = entity.UNIT_COST_INDEX,
                    UNIT_COST_WIDTH = entity.UNIT_COST_WIDTH,
                    UNIT_INDEX = entity.UNIT_INDEX,
                    UNIT_WIDTH = entity.UNIT_WIDTH,
                    UpdatedBy = entity.UpdatedBy,
                    updatedOn = entity.updatedOn,
                    WHOLE_INDEX = entity.WHOLE_INDEX,
                    WHOLE_WIDTH = entity.WHOLE_WIDTH,
                    Cost_Center_INDEX = entity.Cost_Center_INDEX,
                    Cost_Center_WIDTH = entity.Cost_Center_WIDTH,

                    Item_Weight_INDEX = entity.Item_Weight_INDEX,
                    Item_Weight_WIDTH = entity.Item_Weight_WIDTH,
                    Item_Wages_INDEX = entity.Item_Wages_INDEX,
                    Item_Wages_WIDTH = entity.Item_Wages_WIDTH,
                    Manufact_Wages_INDEX = entity.Manufact_Wages_INDEX,
                    Manufact_Wages_WIDTH = entity.Manufact_Wages_WIDTH,
                    ARName_INDEX = entity.ARName_INDEX,
                    ARName_WIDTH = entity.ARName_WIDTH,
                    Clearness_Rate_INDEX = entity.Clearness_Rate_INDEX,
                    Clearness_Rate_WIDTH = entity.Clearness_Rate_WIDTH,
                    Caliber24_INDEX = entity.Caliber24_INDEX,
                    Caliber24_WIDTH = entity.Caliber24_WIDTH,
                    Caliber22_INDEX = entity.Caliber22_INDEX,
                    Caliber22_WIDTH = entity.Caliber22_WIDTH,
                    Caliber21_INDEX = entity.Caliber21_INDEX,
                    Caliber21_WIDTH = entity.Caliber21_WIDTH,
                    Caliber18_INDEX = entity.Caliber18_INDEX,
                    Caliber18_WIDTH = entity.Caliber18_WIDTH,
                    Subject_To_VAT_INDEX = entity.Subject_To_VAT_INDEX,
                    Subject_To_VAT_WIDTH = entity.Subject_To_VAT_WIDTH,
                    VAT_Rate_INDEX = entity.VAT_Rate_INDEX,
                    VAT_Rate_WIDTH = entity.VAT_Rate_WIDTH,

                    ActualWeight_INDEX = entity.ActualWeight_INDEX,
                    ActualWeight_WIDTH = entity.ActualWeight_WIDTH,
                    ARActualWeight = entity.ARActualWeight,
                    ENActualWeight = entity.ENActualWeight,

                    TaxTotal_INDEX = entity.TaxTotal_INDEX,
                    TaxTotal_WIDTH = entity.TaxTotal_WIDTH,

                    TotalItemGmWages_INDEX = entity.TotalItemGmWages_INDEX,
                    TotalItemGmWages_WIDTH = entity.TotalItemGmWages_WIDTH,

                    ARTotalItemGmWages = entity.ARTotalItemGmWages,
                    ENTotalItemGmWages = entity.ENTotalItemGmWages,

                    TotalGoldManufact_INDEX = entity.TotalGoldManufact_INDEX,
                    TotalGoldManufact_WIDTH = entity.TotalGoldManufact_WIDTH,

                    ExemptOfTax_INDEX = entity.ExemptOfTax_INDEX,
                    ExemptOfTax_WIDTH = entity.ExemptOfTax_WIDTH,

                    ItemPrice_INDEX = entity.ItemPrice_INDEX,
                    ItemPrice_WIDTH = entity.ItemPrice_WIDTH,


                    AfterDiscount_INDEX = entity.AfterDiscount_INDEX,
                    AfterDiscount_WIDTH = entity.AfterDiscount_WIDTH,

                    ARAfterDiscount = entity.ARAfterDiscount,
                    ENAfterDiscount = entity.ENAfterDiscount,


                    ARItemPrice = entity.ARItemPrice,
                    ENItemPrice = entity.ENItemPrice,

                    ARExemptOfTax = entity.ARExemptOfTax,
                    ENExemptOfTax = entity.ENExemptOfTax,

                    ARTotalGoldManufact = entity.ARTotalGoldManufact,
                    ENTotalGoldManufact = entity.ENTotalGoldManufact,


                    ARTaxTotal = entity.ARTaxTotal,
                    ENTaxTotal = entity.ENTaxTotal,

                    ARCode = entity.ARCode,
                    ENCode = entity.ENCode,
                    ARItem = entity.ARItem,
                    ENItem = entity.ENItem,
                    ARQuantity = entity.ARQuantity,
                    ENQuantity = entity.ENQuantity,
                    ARItemWeight = entity.ARItemWeight,
                    ENItemWeight = entity.ENItemWeight,
                    ARItemGmWages = entity.ARItemGmWages,
                    ENItemGmWages = entity.ENItemGmWages,
                    ARManufacturingWages = entity.ARManufacturingWages,
                    ENManufacturingWages = entity.ENManufacturingWages,
                    ARUnit = entity.ARUnit,
                    ENUnit = entity.ENUnit,
                    ARCaliberName = entity.ARCaliberName,
                    ENCaliberName = entity.ENCaliberName,
                    ARPrice = entity.ARPrice,
                    ENPrice = entity.ENPrice,
                    ARDiscount = entity.ARDiscount,
                    ENDiscount = entity.ENDiscount,
                    ARDiscRate = entity.ARDiscRate,
                    ENDiscRate = entity.ENDiscRate,
                    ARTotal = entity.ARTotal,
                    ENTotal = entity.ENTotal,
                    ARClearnessRate = entity.ARClearnessRate,
                    ENClearnessRate = entity.ENClearnessRate,
                    ARCaliber24 = entity.ARCaliber24,
                    ENCaliber24 = entity.ENCaliber24,
                    ARCaliber22 = entity.ARCaliber22,
                    ENCaliber22 = entity.ENCaliber22,
                    ARCaliber21 = entity.ARCaliber21,
                    ENCaliber21 = entity.ENCaliber21,
                    ARCaliber18 = entity.ARCaliber18,
                    ENCaliber18 = entity.ENCaliber18,
                    ARCostCenter = entity.ARCostCenter,
                    ENCostCenter = entity.ENCostCenter,
                    ARSubjectToVat = entity.ARSubjectToVat,
                    ENSubjectToVat = entity.ENSubjectToVat,
                    ARVatRate = entity.ARVatRate,
                    ENVatRate = entity.ENVatRate,

                    VatValue_INDEX = entity.VatValue_INDEX,
                    VatValue_WIDTH = entity.VatValue_WIDTH,
                    ARVatValue = entity.ARVatValue,
                    ENVatValue = entity.ENVatValue,

                    WagesDiscValue_INDEX = entity.WagesDiscValue_INDEX,
                    WagesDiscValue_WIDTH = entity.WagesDiscValue_WIDTH,
                    WagesDiscRate_INDEX = entity.WagesDiscRate_INDEX,
                    WagesDiscRate_WIDTH = entity.WagesDiscRate_WIDTH,

                    ARWagesDiscValue = entity.ARWagesDiscValue,
                    ENWagesDiscValue = entity.ENWagesDiscValue,
                    ARWagesDiscRate = entity.ARWagesDiscRate,
                    ENWagesDiscRate = entity.ENWagesDiscRate,
                    
                    ARLockPrice = entity.ARLockPrice,
                    ENLockPrice = entity.ENLockPrice,
                    LockPrice_INDEX = entity.LockPrice_INDEX,
                    LockPrice_WIDTH = entity.LockPrice_WIDTH


                };
                billGridColumnsMRepo.Update(bgc, bgc.BILL_GRID_ID);
                return true;
            });
        }


        public Task<BILL_GRID_COLUMNSVM> GetBySettingID(int settingID)
        {
            return Task.Run<BILL_GRID_COLUMNSVM>(() =>
            {

                var q = from entity in billGridColumnsMRepo.Filter(b => b.BILL_SETTING_ID == settingID)
                        select new BILL_GRID_COLUMNSVM
                        {
                            AddedBy = entity.AddedBy,
                            AddedOn = entity.AddedOn,
                            BILL_GRID_ID = entity.BILL_GRID_ID,
                            BILL_REMARKS_INDEX = entity.BILL_REMARKS_INDEX,
                            BILL_REMARKS_WIDTH = entity.BILL_REMARKS_WIDTH,
                            BILL_SETTING_ID = entity.BILL_SETTING_ID,
                            CEXTERNALLEXPENSES_INDEX = entity.CEXTERNALLEXPENSES_INDEX,
                            CEXTERNALLEXPENSES_WIDTH = entity.CEXTERNALLEXPENSES_WIDTH,
                            CINVENTORYVALUE_INDEX = entity.CINVENTORYVALUE_INDEX,
                            CINVENTORYVALUE_WIDTH = entity.CINVENTORYVALUE_WIDTH,
                            CTOTALCURR_INDEX = entity.CTOTALCURR_INDEX,
                            CTOTALCURR_WIDTH = entity.CTOTALCURR_WIDTH,
                            CTOTALWITHEXPENSES_INDEX = entity.CTOTALWITHEXPENSES_INDEX,
                            CTOTALWITHEXPENSES_WIDTH = entity.CTOTALWITHEXPENSES_WIDTH,
                            DISC_RATE_INDEX = entity.DISC_RATE_INDEX,
                            DISC_RATE_WIDTH = entity.DISC_RATE_WIDTH,
                            DISC_VALUE_INDEX = entity.DISC_VALUE_INDEX,
                            DISC_VALUE_WIDTH = entity.DISC_VALUE_WIDTH,
                            EMPLOYEE_INDEX = entity.EMPLOYEE_INDEX,
                            EMPLOYEE_WIDTH = entity.EMPLOYEE_WIDTH,
                            END_USERS_INDEX = entity.END_USERS_INDEX,
                            END_USERS_WIDTH = entity.END_USERS_WIDTH,
                            EXPIRED_DATE_INDEX = entity.EXPIRED_DATE_INDEX,
                            EXPIRED_DATE_WIDTH = entity.EXPIRED_DATE_WIDTH,
                            EXPORT_INDEX = entity.EXPORT_INDEX,
                            EXPORT_WIDTH = entity.EXPORT_WIDTH,
                            EXTRA_RATE_INDEX = entity.EXTRA_RATE_INDEX,
                            EXTRA_RATE_WIDTH = entity.EXTRA_RATE_WIDTH,
                            EXTRA_VALUE_INDEX = entity.EXTRA_VALUE_INDEX,
                            EXTRA_VALUE_WIDTH = entity.EXTRA_VALUE_WIDTH,
                            GIFTS_INDEX = entity.GIFTS_INDEX,
                            GIFTS_WIDTH = entity.GIFTS_WIDTH,
                            HALF_WHOLE_INDEX = entity.HALF_WHOLE_INDEX,
                            HALF_WHOLE_WIDTH = entity.HALF_WHOLE_WIDTH,
                            ITEM_INDEX = entity.ITEM_INDEX,
                            ITEM_REMAIN_QTY_INDEX = entity.ITEM_REMAIN_QTY_INDEX,
                            ITEM_WIDTH = entity.ITEM_WIDTH,
                            ITEM_REMAIN_QTY_WIDTH = entity.ITEM_REMAIN_QTY_WIDTH,
                            LAST_BUY_INDEX = entity.LAST_BUY_INDEX,
                            LAST_BUY_WIDTH = entity.LAST_BUY_WIDTH,
                            PRODUCTION_DATE_INDEX = entity.PRODUCTION_DATE_INDEX,
                            PRODUCTION_DATE_WIDTH = entity.PRODUCTION_DATE_WIDTH,
                            QTY_INDEX = entity.QTY_INDEX,
                            QTY_WIDTH = entity.QTY_WIDTH,
                            RETAIL_INDEX = entity.RETAIL_INDEX,
                            RETAIL_WIDTH = entity.RETAIL_WIDTH,
                            TOTAL_INDEX = entity.TOTAL_INDEX,
                            TOTAL_WIDTH = entity.TOTAL_WIDTH,
                            UNIT_COST_INDEX = entity.UNIT_COST_INDEX,
                            UNIT_COST_WIDTH = entity.UNIT_COST_WIDTH,
                            UNIT_INDEX = entity.UNIT_INDEX,
                            UNIT_WIDTH = entity.UNIT_WIDTH,
                            UpdatedBy = entity.UpdatedBy,
                            updatedOn = entity.updatedOn,
                            WHOLE_INDEX = entity.WHOLE_INDEX,
                            WHOLE_WIDTH = entity.WHOLE_WIDTH,

                            Cost_Center_INDEX = entity.Cost_Center_INDEX,
                            Cost_Center_WIDTH = entity.Cost_Center_WIDTH,

                            Item_Weight_INDEX = entity.Item_Weight_INDEX,
                            Item_Weight_WIDTH = entity.Item_Weight_WIDTH,
                            Item_Wages_INDEX = entity.Item_Wages_INDEX,
                            Item_Wages_WIDTH = entity.Item_Wages_WIDTH,
                            Manufact_Wages_INDEX = entity.Manufact_Wages_INDEX,
                            Manufact_Wages_WIDTH = entity.Manufact_Wages_WIDTH,
                            ARName_INDEX = entity.ARName_INDEX,
                            ARName_WIDTH = entity.ARName_WIDTH,
                            Clearness_Rate_INDEX = entity.Clearness_Rate_INDEX,
                            Clearness_Rate_WIDTH = entity.Clearness_Rate_WIDTH,
                            Caliber24_INDEX = entity.Caliber24_INDEX,
                            Caliber24_WIDTH = entity.Caliber24_WIDTH,
                            Caliber22_INDEX = entity.Caliber22_INDEX,
                            Caliber22_WIDTH = entity.Caliber22_WIDTH,
                            Caliber21_INDEX = entity.Caliber21_INDEX,
                            Caliber21_WIDTH = entity.Caliber21_WIDTH,
                            Caliber18_INDEX = entity.Caliber18_INDEX,
                            Caliber18_WIDTH = entity.Caliber18_WIDTH,
                            Subject_To_VAT_INDEX = entity.Subject_To_VAT_INDEX,
                            Subject_To_VAT_WIDTH = entity.Subject_To_VAT_WIDTH,
                            VAT_Rate_INDEX = entity.VAT_Rate_INDEX,
                            VAT_Rate_WIDTH = entity.VAT_Rate_WIDTH,

                            ActualWeight_INDEX = entity.ActualWeight_INDEX,
                            ActualWeight_WIDTH = entity.ActualWeight_WIDTH,
                            ARActualWeight = entity.ARActualWeight,
                            ENActualWeight = entity.ENActualWeight,

                            TaxTotal_INDEX = entity.TaxTotal_INDEX,
                            TaxTotal_WIDTH = entity.TaxTotal_WIDTH,

                            TotalItemGmWages_INDEX = entity.TotalItemGmWages_INDEX,
                            TotalItemGmWages_WIDTH = entity.TotalItemGmWages_WIDTH,


                            TotalGoldManufact_INDEX = entity.TotalGoldManufact_INDEX,
                            TotalGoldManufact_WIDTH = entity.TotalGoldManufact_WIDTH,

                            ExemptOfTax_INDEX = entity.ExemptOfTax_INDEX,
                            ExemptOfTax_WIDTH = entity.ExemptOfTax_WIDTH,

                            ItemPrice_INDEX = entity.ItemPrice_INDEX,
                            ItemPrice_WIDTH = entity.ItemPrice_WIDTH,


                            AfterDiscount_INDEX = entity.AfterDiscount_INDEX,
                            AfterDiscount_WIDTH = entity.AfterDiscount_WIDTH,

                            ARAfterDiscount = entity.ARAfterDiscount,
                            ENAfterDiscount = entity.ENAfterDiscount,


                            ARItemPrice = entity.ARItemPrice,
                            ENItemPrice = entity.ENItemPrice,


                            ARExemptOfTax = entity.ARExemptOfTax,
                            ENExemptOfTax = entity.ENExemptOfTax,

                            ARTotalGoldManufact = entity.ARTotalGoldManufact,
                            ENTotalGoldManufact = entity.ENTotalGoldManufact,

                            ARTotalItemGmWages = entity.ARTotalItemGmWages,
                            ENTotalItemGmWages = entity.ENTotalItemGmWages,


                            ARTaxTotal = entity.ARTaxTotal,
                            ENTaxTotal = entity.ENTaxTotal,


                            ARCode = entity.ARCode,
                            ENCode = entity.ENCode,
                            ARItem = entity.ARItem,
                            ENItem = entity.ENItem,
                            ARQuantity = entity.ARQuantity,
                            ENQuantity = entity.ENQuantity,
                            ARItemWeight = entity.ARItemWeight,
                            ENItemWeight = entity.ENItemWeight,
                            ARItemGmWages = entity.ARItemGmWages,
                            ENItemGmWages = entity.ENItemGmWages,
                            ARManufacturingWages = entity.ARManufacturingWages,
                            ENManufacturingWages = entity.ENManufacturingWages,
                            ARUnit = entity.ARUnit,
                            ENUnit = entity.ENUnit,
                            ARCaliberName = entity.ARCaliberName,
                            ENCaliberName = entity.ENCaliberName,
                            ARPrice = entity.ARPrice,
                            ENPrice = entity.ENPrice,
                            ARDiscount = entity.ARDiscount,
                            ENDiscount = entity.ENDiscount,
                            ARDiscRate = entity.ARDiscRate,
                            ENDiscRate = entity.ENDiscRate,
                            ARTotal = entity.ARTotal,
                            ENTotal = entity.ENTotal,
                            ARClearnessRate = entity.ARClearnessRate,
                            ENClearnessRate = entity.ENClearnessRate,
                            ARCaliber24 = entity.ARCaliber24,
                            ENCaliber24 = entity.ENCaliber24,
                            ARCaliber22 = entity.ARCaliber22,
                            ENCaliber22 = entity.ENCaliber22,
                            ARCaliber21 = entity.ARCaliber21,
                            ENCaliber21 = entity.ENCaliber21,
                            ARCaliber18 = entity.ARCaliber18,
                            ENCaliber18 = entity.ENCaliber18,
                            ARCostCenter = entity.ARCostCenter,
                            ENCostCenter = entity.ENCostCenter,
                            ARSubjectToVat = entity.ARSubjectToVat,
                            ENSubjectToVat = entity.ENSubjectToVat,
                            ARVatRate = entity.ARVatRate,
                            ENVatRate = entity.ENVatRate,

                            VatValue_INDEX = entity.VatValue_INDEX,
                            VatValue_WIDTH = entity.VatValue_WIDTH,
                            ARVatValue = entity.ARVatValue,
                            ENVatValue = entity.ENVatValue,

                            WagesDiscValue_INDEX = entity.WagesDiscValue_INDEX,
                            WagesDiscValue_WIDTH = entity.WagesDiscValue_WIDTH,
                            WagesDiscRate_INDEX = entity.WagesDiscRate_INDEX,
                            WagesDiscRate_WIDTH = entity.WagesDiscRate_WIDTH,

                            ARWagesDiscValue = entity.ARWagesDiscValue,
                            ENWagesDiscValue = entity.ENWagesDiscValue,
                            ARWagesDiscRate = entity.ARWagesDiscRate,
                            ENWagesDiscRate = entity.ENWagesDiscRate,
                            ARLockPrice = entity.ARLockPrice,
                            ENLockPrice = entity.ENLockPrice,
                            LockPrice_INDEX = entity.LockPrice_INDEX,
                            LockPrice_WIDTH = entity.LockPrice_WIDTH

                        };
                return q.FirstOrDefault();
            });
        }
    }
}
