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
using System.Data.Entity.Infrastructure;
using System.Globalization;
using System.Data;

namespace ResortERP.Service.Services
{
    public class BillMasterService : IBillMasterService, IDisposable
    {
        private IDbContext _context;

        IGenericRepository<BILL_MASTER> billMasterRepo;
        IGenericRepository<ACCOUNTS> accountsRepo;
        IGenericRepository<BILL_SETTINGS> billSettingRepo;
        public BillMasterService(IGenericRepository<BILL_MASTER> billMasterRepo, IGenericRepository<ACCOUNTS> accountsRepo, IGenericRepository<BILL_SETTINGS> billSettingRepo, IDbContext _context)
        {
            this.billMasterRepo = billMasterRepo;
            this.accountsRepo = accountsRepo;
            this.billSettingRepo = billSettingRepo;
            this._context = _context;
        }

        public Task<int> CountAsync(int billType)
        {
            return Task.Run<int>(() =>
            {
                return billMasterRepo.CountAsync(p => p.BILL_SETTING_ID == billType);
            });
        }

        public List<BILL_MASTER> getByEmp(int empId)
        {
            var q = billMasterRepo.GetAll().Where(a => a.EMP_ID == empId).ToList();
            return q;
        }
        public List<BILL_MASTER> getByCust(int custId)
        {
            var q = billMasterRepo.GetAll().Where(a => a.CUST_ACC_ID == custId).ToList();
            return q;
        }
        public List<BILL_MASTER> getByCompBranId(int compBranId)
        {
            var q = billMasterRepo.GetAll().Where(a => a.COM_BRN_ID == compBranId).ToList();
            return q;
        }
        public List<BILL_MASTER> getByPayTypeId(string payId)
        {
            var q = billMasterRepo.GetAll().Where(a => a.BILL_PAY_WAY == payId).ToList();
            return q;
        }

        public List<BILL_MASTER> getByCurrency(int currencyId)
        {
            var q = billMasterRepo.GetAll().Where(a => a.CURRENCY_ID == currencyId).ToList();
            return q;
        }

        public bool Delete(BILL_MASTERVM entity)
        {
            BILL_MASTER bm = new BILL_MASTER
            {
                BILLCURRENTPAIDEDVALUE = entity.BILLCURRENTPAIDEDVALUE,
                BILLCURRENTREMAINVALUE = entity.BILLCURRENTREMAINVALUE,
                BILL_ANOTHER_DISC = entity.BILL_ANOTHER_DISC,
                BILL_COST = entity.BILL_COST,
                BILL_DATE = entity.BILL_DATE,
                BILL_ID = entity.BILL_ID,
                BILL_IS_POST = entity.BILL_IS_POST,
                BILL_NUMBER = entity.BILL_NUMBER,
                BILL_PAIDED = entity.BILL_PAIDED,
                BILL_PAY_WAY = entity.BILL_PAY_WAY,
                BILL_PROFIT = entity.BILL_PROFIT,
                BILL_REMARKS = entity.BILL_REMARKS,
                BILL_SETTING_ID = entity.BILL_SETTING_ID,
                BILL_TOTAL = entity.BILL_TOTAL,
                BILL_TOTAL_DISC = entity.BILL_TOTAL_DISC,
                BILL_TOTAL_EXTRA = entity.BILL_TOTAL_EXTRA,
                BILL_TYPE = entity.BILL_TYPE,
                BILL_WIEGHT = entity.BILL_WIEGHT,
                COM_BRN_ID = entity.COM_BRN_ID,
                COM_STORE_ID = entity.COM_STORE_ID,
                COST_CENTER_ID = entity.COST_CENTER_ID,
                COUNTER_VALUE = entity.COUNTER_VALUE,
                CURRENCY_ID = entity.CURRENCY_ID,
                CURRENCY_RATE = entity.CURRENCY_RATE,
                CUST_ACC_ID = entity.CUST_ACC_ID,
                GOLD_ACC_ID = entity.GOLD_ACC_ID,
                CUST_BRN_ID = entity.CUST_BRN_ID,
                EMP_ID = entity.EMP_ID,
                ENTRY_ID = entity.ENTRY_ID,
                IS_IT_RESERVATION = entity.IS_IT_RESERVATION,
                ITEM_ACC_ID = entity.ITEM_ACC_ID,
                kest_value = entity.kest_value,
                PAY_ACC_ID = entity.PAY_ACC_ID,
                SELL_TYPE_ID = entity.SELL_TYPE_ID,
                SHIFT_NUMBER = entity.SHIFT_NUMBER,
                THE_LAST_DATE_FOR_RETURN = entity.THE_LAST_DATE_FOR_RETURN,
                THE_RETURN_PERCENTAGE = entity.THE_RETURN_PERCENTAGE,
                TO_COM_STORE_ID = entity.TO_COM_STORE_ID,
                WORK_ORDER_NUMBER = entity.WORK_ORDER_NUMBER,
                AddedBy = entity.AddedBy,
                AddedOn = entity.AddedOn,
                UpdatedBy = entity.UpdatedBy,
                updatedOn = entity.updatedOn,
                BILL_PERCENTAGE_DISC = entity.BILL_PERCENTAGE_DISC,

                CostCenterID = entity.CostCenterID,

                Total18Quantity = entity.Total18Quantity,
                Total21Quantity = entity.Total21Quantity,
                Total22Quantity = entity.Total22Quantity,
                Total24Quantity = entity.Total24Quantity,
                TotalItemWages = entity.TotalItemWages,
                TotalManufactWages = entity.TotalManufactWages,
                TotalMustPaid = entity.TotalMustPaid,
                TotalPaid = entity.TotalPaid,
                TotalPrice = entity.TotalPrice,
                TotalQuantity = entity.TotalQuantity,
                TotalRemaining = entity.TotalRemaining,
                TotalVatRate = entity.TotalVatRate,
                TotalWeight = entity.TotalWeight,

                TransTotalGold18 = entity.TransTotalGold18,
                TransTotalGold21 = entity.TransTotalGold21,
                TransTotalGold22 = entity.TransTotalGold22,
                TransTotalGold24 = entity.TransTotalGold24,
                TransTotalGoldQuantity = entity.TransTotalGoldQuantity,

                TotalDisc = entity.TotalDisc,
                TotalDiscPercentage = entity.TotalDiscPercentage,
                TotalAllDisc = entity.TotalAllDisc,
                TotalAllDiscPercentage = entity.TotalAllDiscPercentage,
                TotalVatValue = entity.TotalVatValue,

                TotalBeforeDiscount = entity.TotalBeforeDiscount,
                TotalAfterDiscount = entity.TotalAfterDiscount,

                TotalWagesDiscRate = entity.TotalWagesDiscRate,
                TotalWagesDiscValue = entity.TotalWagesDiscValue,
                EditReason = entity.EditReason,
                DeliveryPersonName = entity.DeliveryPersonName,
                ExternalNumber = entity.ExternalNumber,

                ExemptOfTaxRate = entity.ExemptOfTaxRate,
                TotalExemptOfTax = entity.TotalExemptOfTax,
                TotalMainVat = entity.TotalMainVat,
                MainVatRate = entity.MainVatRate,
                TotalZeroVat = entity.TotalZeroVat,
                ZeroVatRate = entity.ZeroVatRate,

                ExemptOfTaxValue = entity.ExemptOfTaxValue,
                MainVatValue = entity.MainVatValue,
                ZeroVatValue = entity.ZeroVatValue,
                TotalBeforeTax = entity.TotalBeforeTax,
                TotalAfterTax = entity.TotalAfterTax,
                TotalTaxableAmount = entity.TotalTaxableAmount,
                TotalNotTaxableAmount = entity.TotalNotTaxableAmount,
                DiscountAmount = entity.DiscountAmount

            };
            billMasterRepo.Delete(bm, entity.BILL_ID);
            return true;
        }

        public Task<bool> DeleteAsync(BILL_MASTERVM entity)
        {
            return Task.Run<bool>(() =>
            {
                BILL_MASTER be = new BILL_MASTER
                {
                    BILLCURRENTPAIDEDVALUE = entity.BILLCURRENTPAIDEDVALUE,
                    BILLCURRENTREMAINVALUE = entity.BILLCURRENTREMAINVALUE,
                    BILL_ANOTHER_DISC = entity.BILL_ANOTHER_DISC,
                    BILL_COST = entity.BILL_COST,
                    BILL_DATE = entity.BILL_DATE,
                    BILL_ID = entity.BILL_ID,
                    BILL_IS_POST = entity.BILL_IS_POST,
                    BILL_NUMBER = entity.BILL_NUMBER,
                    BILL_PAIDED = entity.BILL_PAIDED,
                    BILL_PAY_WAY = entity.BILL_PAY_WAY,
                    BILL_PROFIT = entity.BILL_PROFIT,
                    BILL_REMARKS = entity.BILL_REMARKS,
                    BILL_SETTING_ID = entity.BILL_SETTING_ID,
                    BILL_TOTAL = entity.BILL_TOTAL,
                    BILL_TOTAL_DISC = entity.BILL_TOTAL_DISC,
                    BILL_TOTAL_EXTRA = entity.BILL_TOTAL_EXTRA,
                    BILL_TYPE = entity.BILL_TYPE,
                    BILL_WIEGHT = entity.BILL_WIEGHT,
                    COM_BRN_ID = entity.COM_BRN_ID,
                    COM_STORE_ID = entity.COM_STORE_ID,
                    COST_CENTER_ID = entity.COST_CENTER_ID,
                    COUNTER_VALUE = entity.COUNTER_VALUE,
                    CURRENCY_ID = entity.CURRENCY_ID,
                    CURRENCY_RATE = entity.CURRENCY_RATE,
                    CUST_ACC_ID = entity.CUST_ACC_ID,
                    GOLD_ACC_ID = entity.GOLD_ACC_ID,
                    CUST_BRN_ID = entity.CUST_BRN_ID,
                    EMP_ID = entity.EMP_ID,
                    ENTRY_ID = entity.ENTRY_ID,
                    IS_IT_RESERVATION = entity.IS_IT_RESERVATION,
                    ITEM_ACC_ID = entity.ITEM_ACC_ID,
                    kest_value = entity.kest_value,
                    PAY_ACC_ID = entity.PAY_ACC_ID,
                    SELL_TYPE_ID = entity.SELL_TYPE_ID,
                    SHIFT_NUMBER = entity.SHIFT_NUMBER,
                    THE_LAST_DATE_FOR_RETURN = entity.THE_LAST_DATE_FOR_RETURN,
                    THE_RETURN_PERCENTAGE = entity.THE_RETURN_PERCENTAGE,
                    TO_COM_STORE_ID = entity.TO_COM_STORE_ID,
                    WORK_ORDER_NUMBER = entity.WORK_ORDER_NUMBER,
                    AddedBy = entity.AddedBy,
                    AddedOn = entity.AddedOn,
                    UpdatedBy = entity.UpdatedBy,
                    updatedOn = entity.updatedOn,
                    BILL_PERCENTAGE_DISC = entity.BILL_PERCENTAGE_DISC,

                    CostCenterID = entity.CostCenterID,

                    Total18Quantity = entity.Total18Quantity,
                    Total21Quantity = entity.Total21Quantity,
                    Total22Quantity = entity.Total22Quantity,
                    Total24Quantity = entity.Total24Quantity,
                    TotalItemWages = entity.TotalItemWages,
                    TotalManufactWages = entity.TotalManufactWages,
                    TotalMustPaid = entity.TotalMustPaid,
                    TotalPaid = entity.TotalPaid,
                    TotalPrice = entity.TotalPrice,
                    TotalQuantity = entity.TotalQuantity,
                    TotalRemaining = entity.TotalRemaining,
                    TotalVatRate = entity.TotalVatRate,
                    TotalWeight = entity.TotalWeight,

                    TransTotalGold18 = entity.TransTotalGold18,
                    TransTotalGold21 = entity.TransTotalGold21,
                    TransTotalGold22 = entity.TransTotalGold22,
                    TransTotalGold24 = entity.TransTotalGold24,
                    TransTotalGoldQuantity = entity.TransTotalGoldQuantity,

                    TotalDisc = entity.TotalDisc,
                    TotalDiscPercentage = entity.TotalDiscPercentage,
                    TotalAllDisc = entity.TotalAllDisc,
                    TotalAllDiscPercentage = entity.TotalAllDiscPercentage,
                    TotalVatValue = entity.TotalVatValue,

                    TotalBeforeDiscount = entity.TotalBeforeDiscount,
                    TotalAfterDiscount = entity.TotalAfterDiscount,

                    TotalWagesDiscRate = entity.TotalWagesDiscRate,
                    TotalWagesDiscValue = entity.TotalWagesDiscValue,
                    EditReason = entity.EditReason,
                    DeliveryPersonName = entity.DeliveryPersonName,

                    ExternalNumber = entity.ExternalNumber,
                    ExemptOfTaxRate = entity.ExemptOfTaxRate,
                    TotalExemptOfTax = entity.TotalExemptOfTax,
                    TotalMainVat = entity.TotalMainVat,
                    MainVatRate = entity.MainVatRate,
                    TotalZeroVat = entity.TotalZeroVat,
                    ZeroVatRate = entity.ZeroVatRate,

                    ExemptOfTaxValue = entity.ExemptOfTaxValue,
                    MainVatValue = entity.MainVatValue,
                    ZeroVatValue = entity.ZeroVatValue,
                    TotalBeforeTax = entity.TotalBeforeTax,
                    TotalAfterTax = entity.TotalAfterTax,
                    TotalTaxableAmount = entity.TotalTaxableAmount,
                    TotalNotTaxableAmount = entity.TotalNotTaxableAmount,
                    DiscountAmount = entity.DiscountAmount


                };
                billMasterRepo.Delete(be, entity.BILL_ID);
                return true;
            });
        }

        public void Dispose()
        {
            billMasterRepo.Dispose();
            accountsRepo.Dispose();
            billSettingRepo.Dispose();
        }

        public Task<List<BILL_MASTERVM>> GetAllAsync(int branchId, int billType, int pageNum, int pageSize)
        {
            try
            {
                return Task.Run<List<BILL_MASTERVM>>(() =>
                {
                    int rowCount;
                    //var q = from entity in billMasterRepo.GetPaged<long>(pageNum, pageSize, p => p.BILL_ID&(p.BILL_TYPE==billType), false, out rowCount)
                    var q = from entity in billMasterRepo.GetPaged<long>(pageNum, pageSize, x => x.BILL_SETTING_ID == billType && x.COM_BRN_ID == branchId, p => p.BILL_ID, false, out rowCount)
                                //join gold in accountsRepo.GetPaged<int>(pageNum, pageSize, x => x.ACC_ID, false, out rowCount) on entity.GOLD_ACC_ID equals gold.ACC_ID into group1 
                                //from g1 in group1.DefaultIfEmpty()
                                //join pay in accountsRepo.GetPaged<int>(pageNum, pageSize, x => x.ACC_ID, false, out rowCount) on entity.PAY_ACC_ID equals pay.ACC_ID into group2
                                //from g2 in group2.DefaultIfEmpty()
                            select new BILL_MASTERVM
                            {
                                BILLCURRENTPAIDEDVALUE = entity.BILLCURRENTPAIDEDVALUE,
                                BILLCURRENTREMAINVALUE = entity.BILLCURRENTREMAINVALUE,
                                BILL_ANOTHER_DISC = entity.BILL_ANOTHER_DISC,
                                BILL_COST = entity.BILL_COST,
                                BILL_DATE = entity.BILL_DATE,
                                BILL_ID = entity.BILL_ID,
                                BILL_IS_POST = entity.BILL_IS_POST,
                                BILL_NUMBER = entity.BILL_NUMBER,
                                BILL_PAIDED = entity.BILL_PAIDED,
                                BILL_PAY_WAY = entity.BILL_PAY_WAY,
                                BILL_PROFIT = entity.BILL_PROFIT,
                                BILL_REMARKS = entity.BILL_REMARKS,
                                BILL_SETTING_ID = entity.BILL_SETTING_ID,
                                BILL_TOTAL = entity.BILL_TOTAL,
                                BILL_TOTAL_DISC = entity.BILL_TOTAL_DISC,
                                BILL_TOTAL_EXTRA = entity.BILL_TOTAL_EXTRA,
                                BILL_TYPE = entity.BILL_TYPE,
                                BILL_WIEGHT = entity.BILL_WIEGHT,
                                COM_BRN_ID = entity.COM_BRN_ID,
                                COM_STORE_ID = entity.COM_STORE_ID,
                                COST_CENTER_ID = entity.COST_CENTER_ID,
                                COUNTER_VALUE = entity.COUNTER_VALUE,
                                CURRENCY_ID = entity.CURRENCY_ID,
                                CURRENCY_RATE = entity.CURRENCY_RATE,
                                CUST_ACC_ID = entity.CUST_ACC_ID,
                                GOLD_ACC_ID = entity.GOLD_ACC_ID,
                                CUST_BRN_ID = entity.CUST_BRN_ID,
                                EMP_ID = entity.EMP_ID,
                                ENTRY_ID = entity.ENTRY_ID,
                                IS_IT_RESERVATION = entity.IS_IT_RESERVATION,
                                ITEM_ACC_ID = entity.ITEM_ACC_ID,
                                kest_value = entity.kest_value,
                                PAY_ACC_ID = entity.PAY_ACC_ID,
                                SELL_TYPE_ID = entity.SELL_TYPE_ID,
                                SHIFT_NUMBER = entity.SHIFT_NUMBER,
                                THE_LAST_DATE_FOR_RETURN = entity.THE_LAST_DATE_FOR_RETURN,
                                THE_RETURN_PERCENTAGE = entity.THE_RETURN_PERCENTAGE,
                                TO_COM_STORE_ID = entity.TO_COM_STORE_ID,
                                WORK_ORDER_NUMBER = entity.WORK_ORDER_NUMBER,
                                AddedBy = entity.AddedBy,
                                AddedOn = entity.AddedOn,
                                UpdatedBy = entity.UpdatedBy,
                                updatedOn = entity.updatedOn,
                                BILL_PERCENTAGE_DISC = entity.BILL_PERCENTAGE_DISC,

                                CostCenterID = entity.CostCenterID,

                                Total18Quantity = entity.Total18Quantity,
                                Total21Quantity = entity.Total21Quantity,
                                Total22Quantity = entity.Total22Quantity,
                                Total24Quantity = entity.Total24Quantity,
                                TotalItemWages = entity.TotalItemWages,
                                TotalManufactWages = entity.TotalManufactWages,
                                TotalMustPaid = entity.TotalMustPaid,
                                TotalPaid = entity.TotalPaid,
                                TotalPrice = entity.TotalPrice,
                                TotalQuantity = entity.TotalQuantity,
                                TotalRemaining = entity.TotalRemaining,
                                TotalVatRate = entity.TotalVatRate,
                                TotalWeight = entity.TotalWeight,
                                TotalVatValue = entity.TotalVatValue,

                                TotalBeforeDiscount = entity.TotalBeforeDiscount,
                                TotalAfterDiscount = entity.TotalAfterDiscount,

                                TotalWagesDiscRate = entity.TotalWagesDiscRate,
                                TotalWagesDiscValue = entity.TotalWagesDiscValue,
                                EditReason = entity.EditReason,
                                DeliveryPersonName = entity.DeliveryPersonName,

                                ExternalNumber = entity.ExternalNumber,

                                ExemptOfTaxRate = entity.ExemptOfTaxRate,
                                TotalExemptOfTax = entity.TotalExemptOfTax,
                                TotalMainVat = entity.TotalMainVat,
                                MainVatRate = entity.MainVatRate,
                                TotalZeroVat = entity.TotalZeroVat,
                                ZeroVatRate = entity.ZeroVatRate,

                                ExemptOfTaxValue = entity.ExemptOfTaxValue,
                                MainVatValue = entity.MainVatValue,
                                ZeroVatValue = entity.ZeroVatValue,
                                TotalBeforeTax = entity.TotalBeforeTax,
                                TotalAfterTax = entity.TotalAfterTax,
                                TotalTaxableAmount = entity.TotalTaxableAmount,
                                TotalNotTaxableAmount = entity.TotalNotTaxableAmount,
                                DiscountAmount = entity.DiscountAmount
                                //GoldAccountName = g1.ACC_AR_NAME,
                                //PayAccountName = g2.ACC_AR_NAME
                            };
                    return q.ToList();
                });
            }
            catch (Exception ex)
            {

                var c = ex.Message;

                return Task.Run<List<BILL_MASTERVM>>(() =>
                {
                    int rowCount;
                    //var q = from entity in billMasterRepo.GetPaged<long>(pageNum, pageSize, p => p.BILL_ID&(p.BILL_TYPE==billType), false, out rowCount)
                    var q = from entity in billMasterRepo.GetPaged<long>(pageNum, pageSize, x => x.BILL_SETTING_ID == billType, p => p.BILL_ID, false, out rowCount)
                            select new BILL_MASTERVM
                            {
                                BILLCURRENTPAIDEDVALUE = entity.BILLCURRENTPAIDEDVALUE,
                                BILLCURRENTREMAINVALUE = entity.BILLCURRENTREMAINVALUE,
                                BILL_ANOTHER_DISC = entity.BILL_ANOTHER_DISC,
                                BILL_COST = entity.BILL_COST,
                                BILL_DATE = entity.BILL_DATE,
                                BILL_ID = entity.BILL_ID,
                                BILL_IS_POST = entity.BILL_IS_POST,
                                BILL_NUMBER = entity.BILL_NUMBER,
                                BILL_PAIDED = entity.BILL_PAIDED,
                                BILL_PAY_WAY = entity.BILL_PAY_WAY,
                                BILL_PROFIT = entity.BILL_PROFIT,
                                BILL_REMARKS = entity.BILL_REMARKS,
                                BILL_SETTING_ID = entity.BILL_SETTING_ID,
                                BILL_TOTAL = entity.BILL_TOTAL,
                                BILL_TOTAL_DISC = entity.BILL_TOTAL_DISC,
                                BILL_TOTAL_EXTRA = entity.BILL_TOTAL_EXTRA,
                                BILL_TYPE = entity.BILL_TYPE,
                                BILL_WIEGHT = entity.BILL_WIEGHT,
                                COM_BRN_ID = entity.COM_BRN_ID,
                                COM_STORE_ID = entity.COM_STORE_ID,
                                COST_CENTER_ID = entity.COST_CENTER_ID,
                                COUNTER_VALUE = entity.COUNTER_VALUE,
                                CURRENCY_ID = entity.CURRENCY_ID,
                                CURRENCY_RATE = entity.CURRENCY_RATE,
                                CUST_ACC_ID = entity.CUST_ACC_ID,
                                GOLD_ACC_ID = entity.GOLD_ACC_ID,
                                CUST_BRN_ID = entity.CUST_BRN_ID,
                                EMP_ID = entity.EMP_ID,
                                ENTRY_ID = entity.ENTRY_ID,
                                IS_IT_RESERVATION = entity.IS_IT_RESERVATION,
                                ITEM_ACC_ID = entity.ITEM_ACC_ID,
                                kest_value = entity.kest_value,
                                PAY_ACC_ID = entity.PAY_ACC_ID,
                                SELL_TYPE_ID = entity.SELL_TYPE_ID,
                                SHIFT_NUMBER = entity.SHIFT_NUMBER,
                                THE_LAST_DATE_FOR_RETURN = entity.THE_LAST_DATE_FOR_RETURN,
                                THE_RETURN_PERCENTAGE = entity.THE_RETURN_PERCENTAGE,
                                TO_COM_STORE_ID = entity.TO_COM_STORE_ID,
                                WORK_ORDER_NUMBER = entity.WORK_ORDER_NUMBER,
                                AddedBy = entity.AddedBy,
                                AddedOn = entity.AddedOn,
                                UpdatedBy = entity.UpdatedBy,
                                updatedOn = entity.updatedOn,
                                BILL_PERCENTAGE_DISC = entity.BILL_PERCENTAGE_DISC,

                                CostCenterID = entity.CostCenterID,

                                Total18Quantity = entity.Total18Quantity,
                                Total21Quantity = entity.Total21Quantity,
                                Total22Quantity = entity.Total22Quantity,
                                Total24Quantity = entity.Total24Quantity,
                                TotalItemWages = entity.TotalItemWages,
                                TotalManufactWages = entity.TotalManufactWages,
                                TotalMustPaid = entity.TotalMustPaid,
                                TotalPaid = entity.TotalPaid,
                                TotalPrice = entity.TotalPrice,
                                TotalQuantity = entity.TotalQuantity,
                                TotalRemaining = entity.TotalRemaining,
                                TotalVatRate = entity.TotalVatRate,
                                TotalWeight = entity.TotalWeight,

                                TransTotalGold18 = entity.TransTotalGold18,
                                TransTotalGold21 = entity.TransTotalGold21,
                                TransTotalGold22 = entity.TransTotalGold22,
                                TransTotalGold24 = entity.TransTotalGold24,
                                TransTotalGoldQuantity = entity.TransTotalGoldQuantity,
                                TotalVatValue = entity.TotalVatValue,

                                TotalBeforeDiscount = entity.TotalBeforeDiscount,
                                TotalAfterDiscount = entity.TotalAfterDiscount,

                                TotalWagesDiscRate = entity.TotalWagesDiscRate,
                                TotalWagesDiscValue = entity.TotalWagesDiscValue,
                                EditReason = entity.EditReason,
                                DeliveryPersonName = entity.DeliveryPersonName,

                                ExternalNumber = entity.ExternalNumber,

                                ExemptOfTaxRate = entity.ExemptOfTaxRate,
                                TotalExemptOfTax = entity.TotalExemptOfTax,
                                TotalMainVat = entity.TotalMainVat,
                                MainVatRate = entity.MainVatRate,
                                TotalZeroVat = entity.TotalZeroVat,
                                ZeroVatRate = entity.ZeroVatRate,

                                ExemptOfTaxValue = entity.ExemptOfTaxValue,
                                MainVatValue = entity.MainVatValue,
                                ZeroVatValue = entity.ZeroVatValue,
                                TotalBeforeTax = entity.TotalBeforeTax,
                                TotalAfterTax = entity.TotalAfterTax,
                                TotalTaxableAmount = entity.TotalTaxableAmount,
                                TotalNotTaxableAmount = entity.TotalNotTaxableAmount,
                                DiscountAmount = entity.DiscountAmount


                            };
                    return q.ToList();
                });
            }
        }




        public Task<BILL_MASTERVM> getBillByBillNumber(int billNumber, int billType)
        {
            return Task.Run<BILL_MASTERVM>(() =>
                 {
                     var q = from entity in billMasterRepo.Filter(x => x.BILL_SETTING_ID == billType && x.BILL_NUMBER == billNumber.ToString())
                             select new BILL_MASTERVM
                             {
                                 BILLCURRENTPAIDEDVALUE = entity.BILLCURRENTPAIDEDVALUE,
                                 BILLCURRENTREMAINVALUE = entity.BILLCURRENTREMAINVALUE,
                                 BILL_ANOTHER_DISC = entity.BILL_ANOTHER_DISC,
                                 BILL_COST = entity.BILL_COST,
                                 BILL_DATE = entity.BILL_DATE,
                                 BILL_ID = entity.BILL_ID,
                                 BILL_IS_POST = entity.BILL_IS_POST,
                                 BILL_NUMBER = entity.BILL_NUMBER,
                                 BILL_PAIDED = entity.BILL_PAIDED,
                                 BILL_PAY_WAY = entity.BILL_PAY_WAY,
                                 BILL_PROFIT = entity.BILL_PROFIT,
                                 BILL_REMARKS = entity.BILL_REMARKS,
                                 BILL_SETTING_ID = entity.BILL_SETTING_ID,
                                 BILL_TOTAL = entity.BILL_TOTAL,
                                 BILL_TOTAL_DISC = entity.BILL_TOTAL_DISC,
                                 BILL_TOTAL_EXTRA = entity.BILL_TOTAL_EXTRA,
                                 BILL_TYPE = entity.BILL_TYPE,
                                 BILL_WIEGHT = entity.BILL_WIEGHT,
                                 COM_BRN_ID = entity.COM_BRN_ID,
                                 COM_STORE_ID = entity.COM_STORE_ID,
                                 COST_CENTER_ID = entity.COST_CENTER_ID,
                                 COUNTER_VALUE = entity.COUNTER_VALUE,
                                 CURRENCY_ID = entity.CURRENCY_ID,
                                 CURRENCY_RATE = entity.CURRENCY_RATE,
                                 CUST_ACC_ID = entity.CUST_ACC_ID,
                                 GOLD_ACC_ID = entity.GOLD_ACC_ID,
                                 CUST_BRN_ID = entity.CUST_BRN_ID,
                                 EMP_ID = entity.EMP_ID,
                                 ENTRY_ID = entity.ENTRY_ID,
                                 IS_IT_RESERVATION = entity.IS_IT_RESERVATION,
                                 ITEM_ACC_ID = entity.ITEM_ACC_ID,
                                 kest_value = entity.kest_value,
                                 PAY_ACC_ID = entity.PAY_ACC_ID,
                                 SELL_TYPE_ID = entity.SELL_TYPE_ID,
                                 SHIFT_NUMBER = entity.SHIFT_NUMBER,
                                 THE_LAST_DATE_FOR_RETURN = entity.THE_LAST_DATE_FOR_RETURN,
                                 THE_RETURN_PERCENTAGE = entity.THE_RETURN_PERCENTAGE,
                                 TO_COM_STORE_ID = entity.TO_COM_STORE_ID,
                                 WORK_ORDER_NUMBER = entity.WORK_ORDER_NUMBER,
                                 AddedBy = entity.AddedBy,
                                 AddedOn = entity.AddedOn,
                                 UpdatedBy = entity.UpdatedBy,
                                 updatedOn = entity.updatedOn,
                                 BILL_PERCENTAGE_DISC = entity.BILL_PERCENTAGE_DISC,

                                 CostCenterID = entity.CostCenterID,

                                 Total18Quantity = entity.Total18Quantity,
                                 Total21Quantity = entity.Total21Quantity,
                                 Total22Quantity = entity.Total22Quantity,
                                 Total24Quantity = entity.Total24Quantity,
                                 TotalItemWages = entity.TotalItemWages,
                                 TotalManufactWages = entity.TotalManufactWages,
                                 TotalMustPaid = entity.TotalMustPaid,
                                 TotalPaid = entity.TotalPaid,
                                 TotalPrice = entity.TotalPrice,
                                 TotalQuantity = entity.TotalQuantity,
                                 TotalRemaining = entity.TotalRemaining,
                                 TotalVatRate = entity.TotalVatRate,
                                 TotalWeight = entity.TotalWeight,
                                 TotalVatValue = entity.TotalVatValue,

                                 TotalBeforeDiscount = entity.TotalBeforeDiscount,
                                 TotalAfterDiscount = entity.TotalAfterDiscount,

                                 TotalWagesDiscRate = entity.TotalWagesDiscRate,
                                 TotalWagesDiscValue = entity.TotalWagesDiscValue,
                                 EditReason = entity.EditReason,
                                 DeliveryPersonName = entity.DeliveryPersonName,

                                 ExternalNumber = entity.ExternalNumber,

                                 ExemptOfTaxRate = entity.ExemptOfTaxRate,
                                 TotalExemptOfTax = entity.TotalExemptOfTax,
                                 TotalMainVat = entity.TotalMainVat,
                                 MainVatRate = entity.MainVatRate,
                                 TotalZeroVat = entity.TotalZeroVat,
                                 ZeroVatRate = entity.ZeroVatRate,

                                 ExemptOfTaxValue = entity.ExemptOfTaxValue,
                                 MainVatValue = entity.MainVatValue,
                                 ZeroVatValue = entity.ZeroVatValue,
                                 TotalBeforeTax = entity.TotalBeforeTax,
                                 TotalAfterTax = entity.TotalAfterTax,
                                 TotalTaxableAmount = entity.TotalTaxableAmount,
                                 TotalNotTaxableAmount = entity.TotalNotTaxableAmount,
                                 DiscountAmount = entity.DiscountAmount

                             };
                     return q.FirstOrDefault();
                 });
        }

        public Task<long> GetLastBillNumber(int branchId, int settingID)
        {
            return Task.Run<long>(() =>
            {

                var chk = billSettingRepo.Filter(b => b.BILL_SETTING_ID == settingID).FirstOrDefault();
                if (chk.BILL_TYPE_ID == 19 || chk.BILL_TYPE_ID == 20)
                {
                    var q = billMasterRepo.Filter(m => (m.BILL_SETTING_ID == 67 || m.BILL_SETTING_ID == 72) && m.COM_BRN_ID == branchId).OrderByDescending(m => m.BILL_ID).FirstOrDefault();
                    if (q == null)
                    {
                        return 0;
                    }
                    return Convert.ToInt64(q.BILL_NUMBER);
                }
                else
                {
                    var q = billMasterRepo.Filter(m => m.BILL_SETTING_ID == settingID && m.COM_BRN_ID == branchId).OrderByDescending(m => m.BILL_ID).FirstOrDefault();
                    if (q == null)
                    {
                        return 0;
                    }
                    else
                        return Convert.ToInt64(q.BILL_NUMBER);
                }
            });
        }

        public bool Insert(BILL_MASTERVM entity)
        {
            BILL_MASTER bm = new BILL_MASTER
            {
                BILLCURRENTPAIDEDVALUE = entity.BILLCURRENTPAIDEDVALUE,
                BILLCURRENTREMAINVALUE = entity.BILLCURRENTREMAINVALUE,
                BILL_ANOTHER_DISC = entity.BILL_ANOTHER_DISC,
                BILL_COST = entity.BILL_COST,
                BILL_DATE = entity.BILL_DATE,
                BILL_ID = entity.BILL_ID,
                BILL_IS_POST = entity.BILL_IS_POST,
                BILL_NUMBER = entity.BILL_NUMBER,
                BILL_PAIDED = entity.BILL_PAIDED,
                BILL_PAY_WAY = entity.BILL_PAY_WAY,
                BILL_PROFIT = entity.BILL_PROFIT,
                BILL_REMARKS = entity.BILL_REMARKS,
                BILL_SETTING_ID = entity.BILL_SETTING_ID,
                BILL_TOTAL = entity.BILL_TOTAL,
                BILL_TOTAL_DISC = entity.BILL_TOTAL_DISC,
                BILL_TOTAL_EXTRA = entity.BILL_TOTAL_EXTRA,
                BILL_TYPE = entity.BILL_TYPE,
                BILL_WIEGHT = entity.BILL_WIEGHT,
                COM_BRN_ID = entity.COM_BRN_ID,
                COM_STORE_ID = entity.COM_STORE_ID,
                COST_CENTER_ID = entity.COST_CENTER_ID,
                COUNTER_VALUE = entity.COUNTER_VALUE,
                CURRENCY_ID = entity.CURRENCY_ID,
                CURRENCY_RATE = entity.CURRENCY_RATE,
                CUST_ACC_ID = entity.CUST_ACC_ID,
                GOLD_ACC_ID = entity.GOLD_ACC_ID,

                CUST_BRN_ID = entity.CUST_BRN_ID,
                EMP_ID = entity.EMP_ID,
                ENTRY_ID = entity.ENTRY_ID,
                IS_IT_RESERVATION = entity.IS_IT_RESERVATION,
                ITEM_ACC_ID = entity.ITEM_ACC_ID,
                kest_value = entity.kest_value,
                PAY_ACC_ID = entity.PAY_ACC_ID,
                SELL_TYPE_ID = entity.SELL_TYPE_ID,
                SHIFT_NUMBER = entity.SHIFT_NUMBER,
                THE_LAST_DATE_FOR_RETURN = entity.THE_LAST_DATE_FOR_RETURN,
                THE_RETURN_PERCENTAGE = entity.THE_RETURN_PERCENTAGE,
                TO_COM_STORE_ID = entity.TO_COM_STORE_ID,
                WORK_ORDER_NUMBER = entity.WORK_ORDER_NUMBER,
                AddedBy = entity.AddedBy,
                AddedOn = entity.AddedOn,
                UpdatedBy = entity.UpdatedBy,
                updatedOn = entity.updatedOn,
                BILL_PERCENTAGE_DISC = entity.BILL_PERCENTAGE_DISC,

                CostCenterID = entity.CostCenterID,

                Total18Quantity = entity.Total18Quantity,
                Total21Quantity = entity.Total21Quantity,
                Total22Quantity = entity.Total22Quantity,
                Total24Quantity = entity.Total24Quantity,
                TotalItemWages = entity.TotalItemWages,
                TotalManufactWages = entity.TotalManufactWages,
                TotalMustPaid = entity.TotalMustPaid,
                TotalPaid = entity.TotalPaid,
                TotalPrice = entity.TotalPrice,
                TotalQuantity = entity.TotalQuantity,
                TotalRemaining = entity.TotalRemaining,
                TotalVatRate = entity.TotalVatRate,
                TotalWeight = entity.TotalWeight,

                TransTotalGold18 = entity.TransTotalGold18,
                TransTotalGold21 = entity.TransTotalGold21,
                TransTotalGold22 = entity.TransTotalGold22,
                TransTotalGold24 = entity.TransTotalGold24,
                TransTotalGoldQuantity = entity.TransTotalGoldQuantity,

                TotalDisc = entity.TotalDisc,
                TotalDiscPercentage = entity.TotalDiscPercentage,
                TotalAllDisc = entity.TotalAllDisc,
                TotalAllDiscPercentage = entity.TotalAllDiscPercentage,
                TotalVatValue = entity.TotalVatValue,

                TotalBeforeDiscount = entity.TotalBeforeDiscount,
                TotalAfterDiscount = entity.TotalAfterDiscount,

                TotalWagesDiscRate = entity.TotalWagesDiscRate,
                TotalWagesDiscValue = entity.TotalWagesDiscValue,
                EditReason = entity.EditReason,
                DeliveryPersonName = entity.DeliveryPersonName,

                ExternalNumber = entity.ExternalNumber,

                ExemptOfTaxRate = entity.ExemptOfTaxRate,
                TotalExemptOfTax = entity.TotalExemptOfTax,
                TotalMainVat = entity.TotalMainVat,
                MainVatRate = entity.MainVatRate,
                TotalZeroVat = entity.TotalZeroVat,
                ZeroVatRate = entity.ZeroVatRate,

                ExemptOfTaxValue = entity.ExemptOfTaxValue,
                MainVatValue = entity.MainVatValue,
                ZeroVatValue = entity.ZeroVatValue,
                TotalBeforeTax = entity.TotalBeforeTax,
                TotalAfterTax = entity.TotalAfterTax,
                TotalTaxableAmount = entity.TotalTaxableAmount,
                TotalNotTaxableAmount = entity.TotalNotTaxableAmount,
                DiscountAmount = entity.DiscountAmount

            };
            billMasterRepo.Add(bm);
            return true;
        }

        public Task<long> InsertAsync(BILL_MASTERVM entity)
        {
            return Task.Run<long>(() =>
            {
                BILL_MASTER bm = new BILL_MASTER
                {
                    BILLCURRENTPAIDEDVALUE = entity.BILLCURRENTPAIDEDVALUE,
                    BILLCURRENTREMAINVALUE = entity.BILLCURRENTREMAINVALUE,
                    BILL_ANOTHER_DISC = entity.BILL_ANOTHER_DISC,
                    BILL_COST = entity.BILL_COST,
                    BILL_DATE = entity.BILL_DATE,
                    BILL_ID = entity.BILL_ID,
                    BILL_IS_POST = entity.BILL_IS_POST,
                    BILL_NUMBER = entity.BILL_NUMBER,
                    BILL_PAIDED = entity.BILL_PAIDED,
                    BILL_PAY_WAY = entity.BILL_PAY_WAY,
                    BILL_PROFIT = entity.BILL_PROFIT,
                    BILL_REMARKS = entity.BILL_REMARKS,
                    BILL_SETTING_ID = entity.BILL_SETTING_ID,
                    BILL_TOTAL = entity.BILL_TOTAL,
                    BILL_TOTAL_DISC = entity.BILL_TOTAL_DISC,
                    BILL_TOTAL_EXTRA = entity.BILL_TOTAL_EXTRA,
                    BILL_TYPE = entity.BILL_TYPE,
                    BILL_WIEGHT = entity.BILL_WIEGHT,
                    COM_BRN_ID = entity.COM_BRN_ID,
                    COM_STORE_ID = entity.COM_STORE_ID,
                    COST_CENTER_ID = entity.COST_CENTER_ID,
                    COUNTER_VALUE = entity.COUNTER_VALUE,
                    CURRENCY_ID = entity.CURRENCY_ID,
                    CURRENCY_RATE = entity.CURRENCY_RATE,
                    CUST_ACC_ID = entity.CUST_ACC_ID,
                    GOLD_ACC_ID = entity.GOLD_ACC_ID,

                    CUST_BRN_ID = entity.CUST_BRN_ID,
                    EMP_ID = entity.EMP_ID,
                    ENTRY_ID = entity.ENTRY_ID,
                    IS_IT_RESERVATION = entity.IS_IT_RESERVATION,
                    ITEM_ACC_ID = entity.ITEM_ACC_ID,
                    kest_value = entity.kest_value,
                    PAY_ACC_ID = entity.PAY_ACC_ID,
                    SELL_TYPE_ID = entity.SELL_TYPE_ID,
                    SHIFT_NUMBER = entity.SHIFT_NUMBER,
                    THE_LAST_DATE_FOR_RETURN = entity.THE_LAST_DATE_FOR_RETURN,
                    THE_RETURN_PERCENTAGE = entity.THE_RETURN_PERCENTAGE,
                    TO_COM_STORE_ID = entity.TO_COM_STORE_ID,
                    WORK_ORDER_NUMBER = entity.WORK_ORDER_NUMBER,
                    AddedBy = entity.AddedBy,
                    AddedOn = entity.AddedOn,
                    UpdatedBy = entity.UpdatedBy,
                    updatedOn = entity.updatedOn,
                    BILL_PERCENTAGE_DISC = entity.BILL_PERCENTAGE_DISC,

                    CostCenterID = entity.CostCenterID,

                    Total18Quantity = entity.Total18Quantity,
                    Total21Quantity = entity.Total21Quantity,
                    Total22Quantity = entity.Total22Quantity,
                    Total24Quantity = entity.Total24Quantity,
                    TotalItemWages = entity.TotalItemWages,
                    TotalManufactWages = entity.TotalManufactWages,
                    TotalMustPaid = entity.TotalMustPaid,
                    TotalPaid = entity.TotalPaid,
                    TotalPrice = entity.TotalPrice,
                    TotalQuantity = entity.TotalQuantity,
                    TotalRemaining = entity.TotalRemaining,
                    TotalVatRate = entity.TotalVatRate,
                    TotalWeight = entity.TotalWeight,

                    TransTotalGold18 = entity.TransTotalGold18,
                    TransTotalGold21 = entity.TransTotalGold21,
                    TransTotalGold22 = entity.TransTotalGold22,
                    TransTotalGold24 = entity.TransTotalGold24,
                    TransTotalGoldQuantity = entity.TransTotalGoldQuantity,

                    TotalDisc = entity.TotalDisc,
                    TotalDiscPercentage = entity.TotalDiscPercentage,
                    TotalAllDisc = entity.TotalAllDisc,
                    TotalAllDiscPercentage = entity.TotalAllDiscPercentage,
                    TotalVatValue = entity.TotalVatValue,

                    TotalBeforeDiscount = entity.TotalBeforeDiscount,
                    TotalAfterDiscount = entity.TotalAfterDiscount,

                    TotalWagesDiscRate = entity.TotalWagesDiscRate,
                    TotalWagesDiscValue = entity.TotalWagesDiscValue,
                    EditReason = entity.EditReason,
                    DeliveryPersonName = entity.DeliveryPersonName,
                    ExternalNumber = entity.ExternalNumber,

                    ExemptOfTaxRate = entity.ExemptOfTaxRate,
                    TotalExemptOfTax = entity.TotalExemptOfTax,
                    TotalMainVat = entity.TotalMainVat,
                    MainVatRate = entity.MainVatRate,
                    TotalZeroVat = entity.TotalZeroVat,
                    ZeroVatRate = entity.ZeroVatRate,

                    ExemptOfTaxValue = entity.ExemptOfTaxValue,
                    MainVatValue = entity.MainVatValue,
                    ZeroVatValue = entity.ZeroVatValue,
                    TotalBeforeTax = entity.TotalBeforeTax,
                    TotalAfterTax = entity.TotalAfterTax,
                    TotalTaxableAmount = entity.TotalTaxableAmount,
                    TotalNotTaxableAmount = entity.TotalNotTaxableAmount,
                    DiscountAmount = entity.DiscountAmount


                };
                try
                {
                    billMasterRepo.Add(bm);
                }
                catch (Exception ex)
                {


                }

                return bm.BILL_ID;
            });
        }

        public bool Update(BILL_MASTERVM entity)
        {
            BILL_MASTER bm = new BILL_MASTER
            {
                BILLCURRENTPAIDEDVALUE = entity.BILLCURRENTPAIDEDVALUE,
                BILLCURRENTREMAINVALUE = entity.BILLCURRENTREMAINVALUE,
                BILL_ANOTHER_DISC = entity.BILL_ANOTHER_DISC,
                BILL_COST = entity.BILL_COST,
                BILL_DATE = entity.BILL_DATE,
                BILL_ID = entity.BILL_ID,
                BILL_IS_POST = entity.BILL_IS_POST,
                BILL_NUMBER = entity.BILL_NUMBER,
                BILL_PAIDED = entity.BILL_PAIDED,
                BILL_PAY_WAY = entity.BILL_PAY_WAY,
                BILL_PROFIT = entity.BILL_PROFIT,
                BILL_REMARKS = entity.BILL_REMARKS,
                BILL_SETTING_ID = entity.BILL_SETTING_ID,
                BILL_TOTAL = entity.BILL_TOTAL,
                BILL_TOTAL_DISC = entity.BILL_TOTAL_DISC,
                BILL_TOTAL_EXTRA = entity.BILL_TOTAL_EXTRA,
                BILL_TYPE = entity.BILL_TYPE,
                BILL_WIEGHT = entity.BILL_WIEGHT,
                COM_BRN_ID = entity.COM_BRN_ID,
                COM_STORE_ID = entity.COM_STORE_ID,
                COST_CENTER_ID = entity.COST_CENTER_ID,
                COUNTER_VALUE = entity.COUNTER_VALUE,
                CURRENCY_ID = entity.CURRENCY_ID,
                CURRENCY_RATE = entity.CURRENCY_RATE,
                CUST_ACC_ID = entity.CUST_ACC_ID,
                GOLD_ACC_ID = entity.GOLD_ACC_ID,

                CUST_BRN_ID = entity.CUST_BRN_ID,
                EMP_ID = entity.EMP_ID,
                ENTRY_ID = entity.ENTRY_ID,
                IS_IT_RESERVATION = entity.IS_IT_RESERVATION,
                ITEM_ACC_ID = entity.ITEM_ACC_ID,
                kest_value = entity.kest_value,
                PAY_ACC_ID = entity.PAY_ACC_ID,
                SELL_TYPE_ID = entity.SELL_TYPE_ID,
                SHIFT_NUMBER = entity.SHIFT_NUMBER,
                THE_LAST_DATE_FOR_RETURN = entity.THE_LAST_DATE_FOR_RETURN,
                THE_RETURN_PERCENTAGE = entity.THE_RETURN_PERCENTAGE,
                TO_COM_STORE_ID = entity.TO_COM_STORE_ID,
                WORK_ORDER_NUMBER = entity.WORK_ORDER_NUMBER,
                AddedBy = entity.AddedBy,
                AddedOn = entity.AddedOn,
                UpdatedBy = entity.UpdatedBy,
                updatedOn = entity.updatedOn,
                BILL_PERCENTAGE_DISC = entity.BILL_PERCENTAGE_DISC,

                CostCenterID = entity.CostCenterID,

                Total18Quantity = entity.Total18Quantity,
                Total21Quantity = entity.Total21Quantity,
                Total22Quantity = entity.Total22Quantity,
                Total24Quantity = entity.Total24Quantity,
                TotalItemWages = entity.TotalItemWages,
                TotalManufactWages = entity.TotalManufactWages,
                TotalMustPaid = entity.TotalMustPaid,
                TotalPaid = entity.TotalPaid,
                TotalPrice = entity.TotalPrice,
                TotalQuantity = entity.TotalQuantity,
                TotalRemaining = entity.TotalRemaining,
                TotalVatRate = entity.TotalVatRate,
                TotalWeight = entity.TotalWeight,

                TransTotalGold18 = entity.TransTotalGold18,
                TransTotalGold21 = entity.TransTotalGold21,
                TransTotalGold22 = entity.TransTotalGold22,
                TransTotalGold24 = entity.TransTotalGold24,
                TransTotalGoldQuantity = entity.TransTotalGoldQuantity,

                TotalDisc = entity.TotalDisc,
                TotalDiscPercentage = entity.TotalDiscPercentage,
                TotalAllDisc = entity.TotalAllDisc,
                TotalAllDiscPercentage = entity.TotalAllDiscPercentage,
                TotalVatValue = entity.TotalVatValue,

                TotalBeforeDiscount = entity.TotalBeforeDiscount,
                TotalAfterDiscount = entity.TotalAfterDiscount,

                TotalWagesDiscRate = entity.TotalWagesDiscRate,
                TotalWagesDiscValue = entity.TotalWagesDiscValue,
                EditReason = entity.EditReason,
                DeliveryPersonName = entity.DeliveryPersonName,
                ExternalNumber = entity.ExternalNumber,

                ExemptOfTaxRate = entity.ExemptOfTaxRate,
                TotalExemptOfTax = entity.TotalExemptOfTax,
                TotalMainVat = entity.TotalMainVat,
                MainVatRate = entity.MainVatRate,
                TotalZeroVat = entity.TotalZeroVat,
                ZeroVatRate = entity.ZeroVatRate,

                ExemptOfTaxValue = entity.ExemptOfTaxValue,
                MainVatValue = entity.MainVatValue,
                ZeroVatValue = entity.ZeroVatValue,
                TotalBeforeTax = entity.TotalBeforeTax,
                TotalAfterTax = entity.TotalAfterTax,
                TotalTaxableAmount = entity.TotalTaxableAmount,
                TotalNotTaxableAmount = entity.TotalNotTaxableAmount,
                DiscountAmount = entity.DiscountAmount

            };
            billMasterRepo.Update(bm, bm.BILL_ID);
            return true;
        }

        public Task<bool> UpdateAsync(BILL_MASTERVM entity)
        {
            return Task.Run<bool>(() =>
            {
                BILL_MASTER bm = new BILL_MASTER
                {
                    BILLCURRENTPAIDEDVALUE = entity.BILLCURRENTPAIDEDVALUE,
                    BILLCURRENTREMAINVALUE = entity.BILLCURRENTREMAINVALUE,
                    BILL_ANOTHER_DISC = entity.BILL_ANOTHER_DISC,
                    BILL_COST = entity.BILL_COST,
                    BILL_DATE = entity.BILL_DATE,
                    BILL_ID = entity.BILL_ID,
                    BILL_IS_POST = entity.BILL_IS_POST,
                    BILL_NUMBER = entity.BILL_NUMBER,
                    BILL_PAIDED = entity.BILL_PAIDED,
                    BILL_PAY_WAY = entity.BILL_PAY_WAY,
                    BILL_PROFIT = entity.BILL_PROFIT,
                    BILL_REMARKS = entity.BILL_REMARKS,
                    BILL_SETTING_ID = entity.BILL_SETTING_ID,
                    BILL_TOTAL = entity.BILL_TOTAL,
                    BILL_TOTAL_DISC = entity.BILL_TOTAL_DISC,
                    BILL_TOTAL_EXTRA = entity.BILL_TOTAL_EXTRA,
                    BILL_TYPE = entity.BILL_TYPE,
                    BILL_WIEGHT = entity.BILL_WIEGHT,
                    COM_BRN_ID = entity.COM_BRN_ID,
                    COM_STORE_ID = entity.COM_STORE_ID,
                    COST_CENTER_ID = entity.COST_CENTER_ID,
                    COUNTER_VALUE = entity.COUNTER_VALUE,
                    CURRENCY_ID = entity.CURRENCY_ID,
                    CURRENCY_RATE = entity.CURRENCY_RATE,
                    CUST_ACC_ID = entity.CUST_ACC_ID,
                    GOLD_ACC_ID = entity.GOLD_ACC_ID,

                    CUST_BRN_ID = entity.CUST_BRN_ID,
                    EMP_ID = entity.EMP_ID,
                    ENTRY_ID = entity.ENTRY_ID,
                    IS_IT_RESERVATION = entity.IS_IT_RESERVATION,
                    ITEM_ACC_ID = entity.ITEM_ACC_ID,
                    kest_value = entity.kest_value,
                    PAY_ACC_ID = entity.PAY_ACC_ID,
                    SELL_TYPE_ID = entity.SELL_TYPE_ID,
                    SHIFT_NUMBER = entity.SHIFT_NUMBER,
                    THE_LAST_DATE_FOR_RETURN = entity.THE_LAST_DATE_FOR_RETURN,
                    THE_RETURN_PERCENTAGE = entity.THE_RETURN_PERCENTAGE,
                    TO_COM_STORE_ID = entity.TO_COM_STORE_ID,
                    WORK_ORDER_NUMBER = entity.WORK_ORDER_NUMBER,
                    AddedBy = entity.AddedBy,
                    AddedOn = entity.AddedOn,
                    UpdatedBy = entity.UpdatedBy,
                    updatedOn = entity.updatedOn,
                    BILL_PERCENTAGE_DISC = entity.BILL_PERCENTAGE_DISC,

                    CostCenterID = entity.CostCenterID,

                    Total18Quantity = entity.Total18Quantity,
                    Total21Quantity = entity.Total21Quantity,
                    Total22Quantity = entity.Total22Quantity,
                    Total24Quantity = entity.Total24Quantity,
                    TotalItemWages = entity.TotalItemWages,
                    TotalManufactWages = entity.TotalManufactWages,
                    TotalMustPaid = entity.TotalMustPaid,
                    TotalPaid = entity.TotalPaid,
                    TotalPrice = entity.TotalPrice,
                    TotalQuantity = entity.TotalQuantity,
                    TotalRemaining = entity.TotalRemaining,
                    TotalVatRate = entity.TotalVatRate,
                    TotalWeight = entity.TotalWeight,

                    TransTotalGold18 = entity.TransTotalGold18,
                    TransTotalGold21 = entity.TransTotalGold21,
                    TransTotalGold22 = entity.TransTotalGold22,
                    TransTotalGold24 = entity.TransTotalGold24,
                    TransTotalGoldQuantity = entity.TransTotalGoldQuantity,

                    TotalDisc = entity.TotalDisc,
                    TotalDiscPercentage = entity.TotalDiscPercentage,
                    TotalAllDisc = entity.TotalAllDisc,
                    TotalAllDiscPercentage = entity.TotalAllDiscPercentage,
                    TotalVatValue = entity.TotalVatValue,

                    TotalBeforeDiscount = entity.TotalBeforeDiscount,
                    TotalAfterDiscount = entity.TotalAfterDiscount,

                    TotalWagesDiscRate = entity.TotalWagesDiscRate,
                    TotalWagesDiscValue = entity.TotalWagesDiscValue,
                    EditReason = entity.EditReason,
                    DeliveryPersonName = entity.DeliveryPersonName,

                    ExternalNumber = entity.ExternalNumber,

                    ExemptOfTaxRate = entity.ExemptOfTaxRate,
                    TotalExemptOfTax = entity.TotalExemptOfTax,
                    TotalMainVat = entity.TotalMainVat,
                    MainVatRate = entity.MainVatRate,
                    TotalZeroVat = entity.TotalZeroVat,
                    ZeroVatRate = entity.ZeroVatRate,

                    ExemptOfTaxValue = entity.ExemptOfTaxValue,
                    MainVatValue = entity.MainVatValue,
                    ZeroVatValue = entity.ZeroVatValue,
                    TotalBeforeTax = entity.TotalBeforeTax,
                    TotalAfterTax = entity.TotalAfterTax,
                    TotalTaxableAmount = entity.TotalTaxableAmount,
                    TotalNotTaxableAmount = entity.TotalNotTaxableAmount,
                    DiscountAmount = entity.DiscountAmount


                };
                try
                {
                    billMasterRepo.Update(bm, bm.BILL_ID);
                }
                catch (DbUpdateException e)
                {
                    var sqlex = e.InnerException.InnerException as SqlException;
                    var x = sqlex.Number;
                }
                return true;
            });
        }

        ///////////////////////////////////////////
        public Task<BILL_MASTERVM> GetByBillID(int billID)
        {
            return Task.Run<BILL_MASTERVM>(() =>
            {
                var q = from entity in billMasterRepo.Filter(b => b.BILL_ID == billID)
                        select new BILL_MASTERVM
                        {
                            BILLCURRENTPAIDEDVALUE = entity.BILLCURRENTPAIDEDVALUE,
                            BILLCURRENTREMAINVALUE = entity.BILLCURRENTREMAINVALUE,
                            BILL_ANOTHER_DISC = entity.BILL_ANOTHER_DISC,
                            BILL_COST = entity.BILL_COST,
                            BILL_DATE = entity.BILL_DATE,
                            BILL_ID = entity.BILL_ID,
                            BILL_IS_POST = entity.BILL_IS_POST,
                            BILL_NUMBER = entity.BILL_NUMBER,
                            BILL_PAIDED = entity.BILL_PAIDED,
                            BILL_PAY_WAY = entity.BILL_PAY_WAY,
                            BILL_PROFIT = entity.BILL_PROFIT,
                            BILL_REMARKS = entity.BILL_REMARKS,
                            BILL_SETTING_ID = entity.BILL_SETTING_ID,
                            BILL_TOTAL = entity.BILL_TOTAL,
                            BILL_TOTAL_DISC = entity.BILL_TOTAL_DISC,
                            BILL_TOTAL_EXTRA = entity.BILL_TOTAL_EXTRA,
                            BILL_TYPE = entity.BILL_TYPE,
                            BILL_WIEGHT = entity.BILL_WIEGHT,
                            COM_BRN_ID = entity.COM_BRN_ID,
                            COM_STORE_ID = entity.COM_STORE_ID,
                            COST_CENTER_ID = entity.COST_CENTER_ID,
                            COUNTER_VALUE = entity.COUNTER_VALUE,
                            CURRENCY_ID = entity.CURRENCY_ID,
                            CURRENCY_RATE = entity.CURRENCY_RATE,
                            CUST_ACC_ID = entity.CUST_ACC_ID,
                            GOLD_ACC_ID = entity.GOLD_ACC_ID,
                            CUST_BRN_ID = entity.CUST_BRN_ID,
                            EMP_ID = entity.EMP_ID,
                            ENTRY_ID = entity.ENTRY_ID,
                            IS_IT_RESERVATION = entity.IS_IT_RESERVATION,
                            ITEM_ACC_ID = entity.ITEM_ACC_ID,
                            kest_value = entity.kest_value,
                            PAY_ACC_ID = entity.PAY_ACC_ID,
                            SELL_TYPE_ID = entity.SELL_TYPE_ID,
                            SHIFT_NUMBER = entity.SHIFT_NUMBER,
                            THE_LAST_DATE_FOR_RETURN = entity.THE_LAST_DATE_FOR_RETURN,
                            THE_RETURN_PERCENTAGE = entity.THE_RETURN_PERCENTAGE,
                            TO_COM_STORE_ID = entity.TO_COM_STORE_ID,
                            WORK_ORDER_NUMBER = entity.WORK_ORDER_NUMBER,
                            AddedBy = entity.AddedBy,
                            AddedOn = entity.AddedOn,
                            UpdatedBy = entity.UpdatedBy,
                            updatedOn = entity.updatedOn,
                            BILL_PERCENTAGE_DISC = entity.BILL_PERCENTAGE_DISC,

                            CostCenterID = entity.CostCenterID,

                            Total18Quantity = entity.Total18Quantity,
                            Total21Quantity = entity.Total21Quantity,
                            Total22Quantity = entity.Total22Quantity,
                            Total24Quantity = entity.Total24Quantity,
                            TotalItemWages = entity.TotalItemWages,
                            TotalManufactWages = entity.TotalManufactWages,
                            TotalMustPaid = entity.TotalMustPaid,
                            TotalPaid = entity.TotalPaid,
                            TotalPrice = entity.TotalPrice,
                            TotalQuantity = entity.TotalQuantity,
                            TotalRemaining = entity.TotalRemaining,
                            TotalVatRate = entity.TotalVatRate,
                            TotalWeight = entity.TotalWeight,


                            TransTotalGold18 = entity.TransTotalGold18,
                            TransTotalGold21 = entity.TransTotalGold21,
                            TransTotalGold22 = entity.TransTotalGold22,
                            TransTotalGold24 = entity.TransTotalGold24,
                            TransTotalGoldQuantity = entity.TransTotalGoldQuantity,
                            TotalVatValue = entity.TotalVatValue,

                            TotalBeforeDiscount = entity.TotalBeforeDiscount,
                            TotalAfterDiscount = entity.TotalAfterDiscount,

                            TotalWagesDiscRate = entity.TotalWagesDiscRate,
                            TotalWagesDiscValue = entity.TotalWagesDiscValue,
                            EditReason = entity.EditReason,
                            DeliveryPersonName = entity.DeliveryPersonName,
                            ExternalNumber = entity.ExternalNumber,

                            ExemptOfTaxRate = entity.ExemptOfTaxRate,
                            TotalExemptOfTax = entity.TotalExemptOfTax,
                            TotalMainVat = entity.TotalMainVat,
                            MainVatRate = entity.MainVatRate,
                            TotalZeroVat = entity.TotalZeroVat,
                            ZeroVatRate = entity.ZeroVatRate,

                            ExemptOfTaxValue = entity.ExemptOfTaxValue,
                            MainVatValue = entity.MainVatValue,
                            ZeroVatValue = entity.ZeroVatValue,
                            TotalBeforeTax = entity.TotalBeforeTax,
                            TotalAfterTax = entity.TotalAfterTax,
                            TotalTaxableAmount = entity.TotalTaxableAmount,
                            TotalNotTaxableAmount = entity.TotalNotTaxableAmount,
                            DiscountAmount = entity.DiscountAmount


                        };
                return q.FirstOrDefault();
            });
        }


        public Task<bool> UpdateBillEntryByID(BILL_MASTERVM entity, long entryID)
        {
            return Task.Run<bool>(() =>
            {
                BILL_MASTER bm = new BILL_MASTER
                {
                    BILLCURRENTPAIDEDVALUE = entity.BILLCURRENTPAIDEDVALUE,
                    BILLCURRENTREMAINVALUE = entity.BILLCURRENTREMAINVALUE,
                    BILL_ANOTHER_DISC = entity.BILL_ANOTHER_DISC,
                    BILL_COST = entity.BILL_COST,
                    BILL_DATE = entity.BILL_DATE,
                    BILL_ID = entity.BILL_ID,
                    BILL_IS_POST = entity.BILL_IS_POST,
                    BILL_NUMBER = entity.BILL_NUMBER,
                    BILL_PAIDED = entity.BILL_PAIDED,
                    BILL_PAY_WAY = entity.BILL_PAY_WAY,
                    BILL_PROFIT = entity.BILL_PROFIT,
                    BILL_REMARKS = entity.BILL_REMARKS,
                    BILL_SETTING_ID = entity.BILL_SETTING_ID,
                    BILL_TOTAL = entity.BILL_TOTAL,
                    BILL_TOTAL_DISC = entity.BILL_TOTAL_DISC,
                    BILL_TOTAL_EXTRA = entity.BILL_TOTAL_EXTRA,
                    BILL_TYPE = entity.BILL_TYPE,
                    BILL_WIEGHT = entity.BILL_WIEGHT,
                    COM_BRN_ID = entity.COM_BRN_ID,
                    COM_STORE_ID = entity.COM_STORE_ID,
                    COST_CENTER_ID = entity.COST_CENTER_ID,
                    COUNTER_VALUE = entity.COUNTER_VALUE,
                    CURRENCY_ID = entity.CURRENCY_ID,
                    CURRENCY_RATE = entity.CURRENCY_RATE,
                    CUST_ACC_ID = entity.CUST_ACC_ID,
                    GOLD_ACC_ID = entity.GOLD_ACC_ID,

                    CUST_BRN_ID = entity.CUST_BRN_ID,
                    EMP_ID = entity.EMP_ID,
                    ENTRY_ID = entryID,
                    IS_IT_RESERVATION = entity.IS_IT_RESERVATION,
                    ITEM_ACC_ID = entity.ITEM_ACC_ID,
                    kest_value = entity.kest_value,
                    PAY_ACC_ID = entity.PAY_ACC_ID,
                    SELL_TYPE_ID = entity.SELL_TYPE_ID,
                    SHIFT_NUMBER = entity.SHIFT_NUMBER,
                    THE_LAST_DATE_FOR_RETURN = entity.THE_LAST_DATE_FOR_RETURN,
                    THE_RETURN_PERCENTAGE = entity.THE_RETURN_PERCENTAGE,
                    TO_COM_STORE_ID = entity.TO_COM_STORE_ID,
                    WORK_ORDER_NUMBER = entity.WORK_ORDER_NUMBER,
                    AddedBy = entity.AddedBy,
                    AddedOn = entity.AddedOn,
                    UpdatedBy = entity.UpdatedBy,
                    updatedOn = entity.updatedOn,
                    BILL_PERCENTAGE_DISC = entity.BILL_PERCENTAGE_DISC,

                    CostCenterID = entity.CostCenterID,

                    Total18Quantity = entity.Total18Quantity,
                    Total21Quantity = entity.Total21Quantity,
                    Total22Quantity = entity.Total22Quantity,
                    Total24Quantity = entity.Total24Quantity,
                    TotalItemWages = entity.TotalItemWages,
                    TotalManufactWages = entity.TotalManufactWages,
                    TotalMustPaid = entity.TotalMustPaid,
                    TotalPaid = entity.TotalPaid,
                    TotalPrice = entity.TotalPrice,
                    TotalQuantity = entity.TotalQuantity,
                    TotalRemaining = entity.TotalRemaining,
                    TotalVatRate = entity.TotalVatRate,
                    TotalWeight = entity.TotalWeight,

                    TransTotalGold18 = entity.TransTotalGold18,
                    TransTotalGold21 = entity.TransTotalGold21,
                    TransTotalGold22 = entity.TransTotalGold22,
                    TransTotalGold24 = entity.TransTotalGold24,
                    TransTotalGoldQuantity = entity.TransTotalGoldQuantity,

                    TotalDisc = entity.TotalDisc,
                    TotalDiscPercentage = entity.TotalDiscPercentage,
                    TotalAllDisc = entity.TotalAllDisc,
                    TotalAllDiscPercentage = entity.TotalAllDiscPercentage,
                    TotalVatValue = entity.TotalVatValue,

                    TotalBeforeDiscount = entity.TotalBeforeDiscount,
                    TotalAfterDiscount = entity.TotalAfterDiscount,

                    TotalWagesDiscRate = entity.TotalWagesDiscRate,
                    TotalWagesDiscValue = entity.TotalWagesDiscValue,
                    EditReason = entity.EditReason,
                    DeliveryPersonName = entity.DeliveryPersonName,
                    ExternalNumber = entity.ExternalNumber,

                    ExemptOfTaxRate = entity.ExemptOfTaxRate,
                    TotalExemptOfTax = entity.TotalExemptOfTax,
                    TotalMainVat = entity.TotalMainVat,
                    MainVatRate = entity.MainVatRate,
                    TotalZeroVat = entity.TotalZeroVat,
                    ZeroVatRate = entity.ZeroVatRate,

                    ExemptOfTaxValue = entity.ExemptOfTaxValue,
                    MainVatValue = entity.MainVatValue,
                    ZeroVatValue = entity.ZeroVatValue,
                    TotalBeforeTax = entity.TotalBeforeTax,
                    TotalAfterTax = entity.TotalAfterTax,
                    TotalTaxableAmount = entity.TotalTaxableAmount,
                    TotalNotTaxableAmount = entity.TotalNotTaxableAmount,
                    DiscountAmount = entity.DiscountAmount

                };

                billMasterRepo.Update(bm, bm.BILL_ID);

                return true;
            });
        }

        public Task<bool> CheckBillByBillNumber(int billNumber, int billType)
        {
            return Task.Run<bool>(() =>
            {

                var q = billMasterRepo.Filter(x => x.BILL_SETTING_ID == billType && x.BILL_NUMBER == billNumber.ToString()).FirstOrDefault();
                if (q != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            });


        }
        ////////////////////////////////////////////////////////////////////////////
        public int getPaginationByType(int billSettingID)
        {
            int numOfPages = 1;
            int allBills = billMasterRepo.Filter(b => b.BILL_SETTING_ID == billSettingID).Count();
            if (allBills > 0 && allBills > 10)
            {
                numOfPages = allBills / 10;
            }
            return numOfPages;
        }

        ////////////////////////////////////////////////////////////////searching bills ////////////////////////////////////////
        public List<BILL_MASTERVM> GetAllBillsBySettingID(int settingID)
        {
            var x = from bill in billMasterRepo.Filter(b => b.BILL_SETTING_ID == settingID).OrderByDescending(b => b.BILL_DATE).Take(10)
                    join a in accountsRepo.GetAll() on bill.CUST_ACC_ID equals a.ACC_ID into jj
                    from v in jj
                    select new BILL_MASTERVM
                    {
                        CustName = v.ACC_AR_NAME,
                        BILL_ID = bill.BILL_ID,
                        BILL_DATE = bill.BILL_DATE,
                        BILL_NUMBER = bill.BILL_NUMBER
                    };

            List<BILL_MASTERVM> billList = x.ToList();
            return billList;
        }

        public List<BILL_MASTER> getBillStores(int storeId)
        {
            var q = billMasterRepo.GetAll().Where(a => a.COM_STORE_ID == storeId).ToList();
            return q;
        }
        public List<BILL_MASTER> getByCostCenter(int costCenterId)
        {
            var q = billMasterRepo.GetAll().Where(a => a.CostCenterID == costCenterId).ToList();
            return q;
        }
        public List<BILL_MASTER> getByItemId(int itemId)
        {
            var q = billMasterRepo.GetAll().Where(a => a.ITEM_ACC_ID == itemId).ToList();
            return q;
        }


        public List<BILL_MASTERVM> GetBillsForSearch(BillSearch BillSearchObj)
        {
            SqlParameter custNameParam;
            SqlParameter billNumberParam;

            SqlParameter dateFromParam;
            SqlParameter dateToParam;

            if (BillSearchObj.DateFrom != null)
            {
                dateFromParam = new SqlParameter("@dateFrom", Convert.ToDateTime(BillSearchObj.DateFrom.ToString()));
            }
            else
            {
                dateFromParam = new SqlParameter("@dateFrom", DBNull.Value);
            }


            if (BillSearchObj.DateTo != null)
            {
                dateToParam = new SqlParameter("@dateTo", Convert.ToDateTime(BillSearchObj.DateTo.ToString()));
            }
            else
            {
                dateToParam = new SqlParameter("@dateTo", DBNull.Value);
            }


            if (BillSearchObj.CustName != null)
            {
                custNameParam = new SqlParameter("@custName", BillSearchObj.CustName);
            }
            else
            {
                custNameParam = new SqlParameter("@custName", DBNull.Value);
            }



            if (BillSearchObj.BillNumber != null)
            {
                billNumberParam = new SqlParameter("@billNumber", BillSearchObj.BillNumber);
            }
            else
            {
                billNumberParam = new SqlParameter("@billNumber", DBNull.Value);
            }
            // SqlParameter dateToParam = new SqlParameter("@dateTo", to);

            SqlParameter settingIDParam = new SqlParameter("@settingID", BillSearchObj.SettingID);

            var billmaster = billMasterRepo.SQLQuery<BILL_MASTERVM>("exec getBillsForSearch  @custName,@billNumber, @dateFrom, @dateTo, @settingID", custNameParam, billNumberParam, dateFromParam, dateToParam, settingIDParam).ToList<BILL_MASTERVM>();
            return billmaster;

        }

        public List<AccountStatementVM> GetAccountStatement(int AccID)
        {
            SqlParameter cust_Acc_IDParam = new SqlParameter("@Cust_Acc_ID", AccID);

            var billmaster = billMasterRepo.SQLQuery<AccountStatementVM>("exec GetAccountStatement2 @Cust_Acc_ID", cust_Acc_IDParam).ToList<AccountStatementVM>();
            return billmaster;
        }

        public List<AccountStatementVM> Get_PRC_RPT_LEDGER(string companyBranches, string Accounts, string StartDate, string EndDate)
        {
            SqlParameter companyBranches_Param = new SqlParameter("@CompanyBranchID", companyBranches);

            SqlParameter ACCOUNTS_Param = new SqlParameter("@ACCOUNTS", Accounts);

            SqlParameter STARTDATE_Param = new SqlParameter("@STARTDATE", StartDate);

            SqlParameter ENDDATE_Param = new SqlParameter("@ENDDATE", EndDate);

            SqlParameter COSTCENTER_ID_Param = new SqlParameter("@COSTCENTER_ID", DBNull.Value);

            SqlParameter SOURCES_Param = new SqlParameter("@SOURCES", DBNull.Value);

            var billmaster = billMasterRepo.SQLQuery<AccountStatementVM>("exec PRC_RPT_LEDGER_Without_CURRENCY_RATE @ACCOUNTS, @COSTCENTER_ID, @STARTDATE, @ENDDATE, @SOURCES, @CompanyBranchID", ACCOUNTS_Param, COSTCENTER_ID_Param, STARTDATE_Param, ENDDATE_Param, SOURCES_Param, companyBranches_Param).ToList<AccountStatementVM>();

            return billmaster;
        }

        public List<AccountStatementVM> getAccountStatementDailyReport(string companyBranches, string Accounts, string Date)
        {
            SqlParameter companyBranches_Param = new SqlParameter("@CompanyBranchID", companyBranches);

            SqlParameter ACCOUNTS_Param = new SqlParameter("@ACCOUNTS", Accounts);
            
            SqlParameter ENDDATE_Param = new SqlParameter("@ENDDATE", Date);

            SqlParameter STARTDATE_Param = new SqlParameter("@STARTDATE", DBNull.Value);

            SqlParameter COSTCENTER_ID_Param = new SqlParameter("@COSTCENTER_ID", DBNull.Value);

            SqlParameter SOURCES_Param = new SqlParameter("@SOURCES", DBNull.Value);

            var billmaster = billMasterRepo.SQLQuery<AccountStatementVM>("exec PRC_RPT_LEDGER_Without_CURRENCY_RATE @ACCOUNTS, @COSTCENTER_ID, @STARTDATE, @ENDDATE, @SOURCES, @CompanyBranchID", ACCOUNTS_Param, COSTCENTER_ID_Param, STARTDATE_Param, ENDDATE_Param, SOURCES_Param, companyBranches_Param).ToList<AccountStatementVM>();

            return billmaster;
        }

        public List<AccountStatementGoldVM> Get_PRC_RPT_LEDGER_Gold(string companyBranches, string Accounts, string StartDate, string EndDate)
        {
            SqlParameter companyBranches_Param = new SqlParameter("@CompanyBranchID", companyBranches);

            SqlParameter ACCOUNTS_Param = new SqlParameter("@ACCOUNTS", Accounts);

            SqlParameter STARTDATE_Param = new SqlParameter("@STARTDATE", StartDate);

            SqlParameter ENDDATE_Param = new SqlParameter("@ENDDATE", EndDate);

            SqlParameter COSTCENTER_ID_Param = new SqlParameter("@COSTCENTER_ID", DBNull.Value);

            SqlParameter SOURCES_Param = new SqlParameter("@SOURCES", DBNull.Value);

            var billmaster = billMasterRepo.SQLQuery<AccountStatementGoldVM>("exec PRC_RPT_LEDGER_Gold @ACCOUNTS, @COSTCENTER_ID, @STARTDATE, @ENDDATE, @SOURCES, @CompanyBranchID", ACCOUNTS_Param, COSTCENTER_ID_Param, STARTDATE_Param, ENDDATE_Param, SOURCES_Param, companyBranches_Param).ToList<AccountStatementGoldVM>();

            return billmaster;
        }

        public List<AccountStatementGoldVM> getAccountStatementGoldDailyReport(string companyBranches, string Accounts, string Date)
        {
            SqlParameter companyBranches_Param = new SqlParameter("@CompanyBranchID", companyBranches);

            SqlParameter ACCOUNTS_Param = new SqlParameter("@ACCOUNTS", Accounts);

            SqlParameter STARTDATE_Param = new SqlParameter("@STARTDATE", DBNull.Value);

            SqlParameter ENDDATE_Param = new SqlParameter("@ENDDATE", Date);

            SqlParameter COSTCENTER_ID_Param = new SqlParameter("@COSTCENTER_ID", DBNull.Value);

            SqlParameter SOURCES_Param = new SqlParameter("@SOURCES", DBNull.Value);

            var billmaster = billMasterRepo.SQLQuery<AccountStatementGoldVM>("exec PRC_RPT_LEDGER_Gold @ACCOUNTS, @COSTCENTER_ID, @STARTDATE, @ENDDATE, @SOURCES, @CompanyBranchID", ACCOUNTS_Param, COSTCENTER_ID_Param, STARTDATE_Param, ENDDATE_Param, SOURCES_Param, companyBranches_Param).ToList<AccountStatementGoldVM>();

            return billmaster;
        }

        public List<AccountStatementGoldMonthlyVM> getAccountStatementGoldMonthlyReport(string companyBranches, string Accounts, string StartDate, string EndDate)
        {
            SqlParameter companyBranches_Param = new SqlParameter("@CompanyBranchID", companyBranches);

            SqlParameter ACCOUNTS_Param = new SqlParameter("@ACCOUNTS", Accounts);

            SqlParameter STARTDATE_Param = new SqlParameter("@STARTDATE", StartDate);

            SqlParameter ENDDATE_Param = new SqlParameter("@ENDDATE", EndDate);

            SqlParameter COSTCENTER_ID_Param = new SqlParameter("@COSTCENTER_ID", DBNull.Value);

            SqlParameter SOURCES_Param = new SqlParameter("@SOURCES", DBNull.Value);

            SqlParameter MONTHELY_Param = new SqlParameter("@MONTHELY", 1);

            var billmaster = billMasterRepo.SQLQuery<AccountStatementGoldMonthlyVM>("exec PRC_RPT_LEDGER_Gold @ACCOUNTS, @COSTCENTER_ID, @STARTDATE, @ENDDATE, @SOURCES, @CompanyBranchID, @MONTHELY", ACCOUNTS_Param, COSTCENTER_ID_Param, STARTDATE_Param, ENDDATE_Param, SOURCES_Param, companyBranches_Param, MONTHELY_Param).ToList<AccountStatementGoldMonthlyVM>();

            return billmaster;
        }

        public List<AccountStatementMonthlyVM> getAccountStatementMonthlyReport(string companyBranches, string Accounts, string StartDate, string EndDate)
        {
            SqlParameter companyBranches_Param = new SqlParameter("@CompanyBranchID", companyBranches);

            SqlParameter ACCOUNTS_Param = new SqlParameter("@ACCOUNTS", Accounts);

            SqlParameter STARTDATE_Param = new SqlParameter("@STARTDATE",StartDate);

            SqlParameter ENDDATE_Param = new SqlParameter("@ENDDATE", EndDate);

            SqlParameter COSTCENTER_ID_Param = new SqlParameter("@COSTCENTER_ID", DBNull.Value);

            SqlParameter SOURCES_Param = new SqlParameter("@SOURCES", DBNull.Value);

            SqlParameter MONTHELY_Param = new SqlParameter("@MONTHELY", '1');

            var billmaster = billMasterRepo.SQLQuery<AccountStatementMonthlyVM>("exec PRC_RPT_LEDGER_Without_CURRENCY_RATE @ACCOUNTS, @COSTCENTER_ID, @STARTDATE, @ENDDATE, @SOURCES, @CompanyBranchID, @MONTHELY", ACCOUNTS_Param, COSTCENTER_ID_Param, STARTDATE_Param, ENDDATE_Param, SOURCES_Param, companyBranches_Param, MONTHELY_Param).ToList<AccountStatementMonthlyVM>();

            return billmaster;
        }

        public List<TrialBalance> Get_PRC_RPT_TRIALBALANCE(string companyBranches, string StartDate, string EndDate, string Type)
        {
            SqlParameter companyBranches_Param = new SqlParameter("@CompanyBranchID", companyBranches);
            SqlParameter COSTCENTER_ID_Param = new SqlParameter("@COSTCENTER_ID", DBNull.Value);

            SqlParameter STARTDATE_Param = new SqlParameter("@STARTDATE", StartDate);

            SqlParameter ENDDATE_Param = new SqlParameter("@ENDDATE", EndDate);

            SqlParameter TYPE_Param = new SqlParameter("@TYPE", Type);

            var billmaster = billMasterRepo.SQLQuery<TrialBalance>("exec PRC_RPT_TRIALBALANCE @COSTCENTER_ID, @STARTDATE, @ENDDATE, @TYPE, @CompanyBranchID", COSTCENTER_ID_Param, STARTDATE_Param, ENDDATE_Param, TYPE_Param, companyBranches_Param).ToList<TrialBalance>();

            return billmaster;
        }

        /*********************** Expenses taxes *********************/
        public List<AccountStatementVM> getExpensesTaxReport(string companyBranches, string Accounts, string StartDate, string EndDate)
        {
            SqlParameter companyBranches_Param = new SqlParameter("@CompanyBranchID", companyBranches);

            SqlParameter ACCOUNTS_Param = new SqlParameter("@ACCOUNTS", Accounts);

            SqlParameter STARTDATE_Param = new SqlParameter("@STARTDATE", StartDate);

            SqlParameter ENDDATE_Param = new SqlParameter("@ENDDATE", EndDate);

            SqlParameter COSTCENTER_ID_Param = new SqlParameter("@COSTCENTER_ID", DBNull.Value);

            SqlParameter SOURCES_Param = new SqlParameter("@SOURCES", DBNull.Value);

            var billmaster = billMasterRepo.SQLQuery<AccountStatementVM>("exec PRC_RPT_TAX @ACCOUNTS, @COSTCENTER_ID, @STARTDATE, @ENDDATE, @SOURCES, @CompanyBranchID", ACCOUNTS_Param, COSTCENTER_ID_Param, STARTDATE_Param, ENDDATE_Param, SOURCES_Param, companyBranches_Param).ToList<AccountStatementVM>();

            return billmaster;
        }

        public List<AccountStatementVM> getExpensesTaxDailyReport(string companyBranches, string Accounts, string Date)
        {
            SqlParameter companyBranches_Param = new SqlParameter("@CompanyBranchID", companyBranches);

            SqlParameter ACCOUNTS_Param = new SqlParameter("@ACCOUNTS", Accounts);

            SqlParameter ENDDATE_Param = new SqlParameter("@ENDDATE", Date);

            SqlParameter STARTDATE_Param = new SqlParameter("@STARTDATE", DBNull.Value);

            SqlParameter COSTCENTER_ID_Param = new SqlParameter("@COSTCENTER_ID", DBNull.Value);

            SqlParameter SOURCES_Param = new SqlParameter("@SOURCES", DBNull.Value);

            var billmaster = billMasterRepo.SQLQuery<AccountStatementVM>("exec PRC_RPT_TAX @ACCOUNTS, @COSTCENTER_ID, @STARTDATE, @ENDDATE, @SOURCES, @CompanyBranchID", ACCOUNTS_Param, COSTCENTER_ID_Param, STARTDATE_Param, ENDDATE_Param, SOURCES_Param, companyBranches_Param).ToList<AccountStatementVM>();

            return billmaster;
        }

        public List<AccountStatementMonthlyVM> getExpensesTaxMonthlyReport(string companyBranches, string Accounts, string StartDate, string EndDate)
        {
            SqlParameter companyBranches_Param = new SqlParameter("@CompanyBranchID", companyBranches);

            SqlParameter ACCOUNTS_Param = new SqlParameter("@ACCOUNTS", Accounts);

            SqlParameter STARTDATE_Param = new SqlParameter("@STARTDATE", StartDate);

            SqlParameter ENDDATE_Param = new SqlParameter("@ENDDATE", EndDate);

            SqlParameter COSTCENTER_ID_Param = new SqlParameter("@COSTCENTER_ID", DBNull.Value);

            SqlParameter SOURCES_Param = new SqlParameter("@SOURCES", DBNull.Value);

            SqlParameter MONTHELY_Param = new SqlParameter("@MONTHELY", '1');

            var billmaster = billMasterRepo.SQLQuery<AccountStatementMonthlyVM>("exec PRC_RPT_TAX @ACCOUNTS, @COSTCENTER_ID, @STARTDATE, @ENDDATE, @SOURCES, @CompanyBranchID, @MONTHELY", ACCOUNTS_Param, COSTCENTER_ID_Param, STARTDATE_Param, ENDDATE_Param, SOURCES_Param, companyBranches_Param, MONTHELY_Param).ToList<AccountStatementMonthlyVM>();

            return billmaster;
        }
        /************************************************************/

        public List<List<AccountStatementVM>> Get_PRC_RPT_LEDGER(string companyBranches, string Accounts, string CostCenterId, string StartDate, string EndDate)
        {
            SqlParameter companyBranches_Param = new SqlParameter("@CompanyBranchID", companyBranches);

            SqlParameter ACCOUNTS_Param = new SqlParameter("@ACCOUNTS", Accounts);

            SqlParameter STARTDATE_Param = new SqlParameter("@STARTDATE", StartDate);

            SqlParameter ENDDATE_Param = new SqlParameter("@ENDDATE", EndDate);

            SqlParameter COSTCENTER_ID_Param = new SqlParameter("@COSTCENTER_ID", DBNull.Value);

            SqlParameter SOURCES_Param = new SqlParameter("@SOURCES", DBNull.Value);

            //var billmaster = billMasterRepo.SQLQuery<AccountStatementVM>("exec PRC_RPT_LEDGER @ACCOUNTS, @COSTCENTER_ID, @STARTDATE, @ENDDATE, @SOURCES", ACCOUNTS_Param, COSTCENTER_ID_Param, STARTDATE_Param, ENDDATE_Param, SOURCES_Param).ToList<AccountStatementVM>();

            List<List<AccountStatementVM>> AccList = new List<List<AccountStatementVM>>();

            using (var connection = _context.Database.Connection)
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "EXEC [dbo].[PRC_RPT_LEDGER] @ACCOUNTS, @COSTCENTER_ID, @STARTDATE, @ENDDATE, @SOURCES, @CompanyBranchID";
                //command.CommandType = System.Data.CommandType.StoredProcedure;

                command.Parameters.Add(ACCOUNTS_Param);
                command.Parameters.Add(STARTDATE_Param);
                command.Parameters.Add(ENDDATE_Param);
                command.Parameters.Add(COSTCENTER_ID_Param);
                command.Parameters.Add(SOURCES_Param);
                command.Parameters.Add(companyBranches_Param);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.HasRows)
                    {
                        var list1 =
                            ((IObjectContextAdapter)_context)
                                .ObjectContext
                                .Translate<AccountStatementVM>(reader)
                                .ToList();

                        reader.NextResult();

                        AccList.Add(list1);
                    }
                }
            }

            return AccList;
        }

        public void test(string val1, string val2, string val3)
        {

            using (var connection = _context.Database.Connection)
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "exec [dbo].[PRC_RPT_LEDGER]";
                command.CommandType = System.Data.CommandType.StoredProcedure;

                using (var reader = command.ExecuteReader())
                {
                    var list1 =
                        ((IObjectContextAdapter)_context)
                            .ObjectContext
                            .Translate<AccountStatementVM>(reader)
                            .ToList();

                    reader.NextResult();

                    var list2 =
                        ((IObjectContextAdapter)_context)
                            .ObjectContext
                            .Translate<AccountStatementVM>(reader)
                            .ToList();

                }
            }
        }

        public List<List<AccountStatementGoldVM>> Get_PRC_RPT_LEDGER_Gold(string companyBranches, string Accounts, string CostCenterId, string Source, string StartDate, string EndDate)
        {
            SqlParameter companyBranches_Param = new SqlParameter("@CompanyBranchID", companyBranches);

            SqlParameter ACCOUNTS_Param = new SqlParameter("@ACCOUNTS", Accounts);

            SqlParameter STARTDATE_Param = new SqlParameter("@STARTDATE", StartDate);

            SqlParameter ENDDATE_Param = new SqlParameter("@ENDDATE", EndDate);

            SqlParameter COSTCENTER_ID_Param = new SqlParameter();

            if (string.IsNullOrEmpty(CostCenterId) || CostCenterId == "null")
            {
                COSTCENTER_ID_Param = new SqlParameter("@COSTCENTER_ID", DBNull.Value);
            }
            else
            {
                COSTCENTER_ID_Param = new SqlParameter("@COSTCENTER_ID", CostCenterId);
            }

            SqlParameter SOURCES_Param = new SqlParameter("@SOURCES", DBNull.Value);

            List<List<AccountStatementGoldVM>> AccList = new List<List<AccountStatementGoldVM>>();

            using (var connection = _context.Database.Connection)
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "EXEC [dbo].[PRC_RPT_LEDGER_Gold] @ACCOUNTS, @COSTCENTER_ID, @STARTDATE, @ENDDATE, @SOURCES, @CompanyBranchID";

                command.Parameters.Add(ACCOUNTS_Param);
                command.Parameters.Add(STARTDATE_Param);
                command.Parameters.Add(ENDDATE_Param);
                command.Parameters.Add(COSTCENTER_ID_Param);
                command.Parameters.Add(SOURCES_Param);
                command.Parameters.Add(companyBranches_Param);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.HasRows)
                    {
                        var list1 =
                            ((IObjectContextAdapter)_context)
                                .ObjectContext
                                .Translate<AccountStatementGoldVM>(reader)
                                .ToList();

                        reader.NextResult();

                        AccList.Add(list1);
                    }
                }
            }

            return AccList;
        }

        public List<TrialBalanceGoldVM> Get_PRC_RPT_TRIALBALANCE_Gold(string companyBranches, string StartDate, string EndDate, string Type)
        {
            SqlParameter companyBranches_Param = new SqlParameter("@CompanyBranchID", companyBranches);



            SqlParameter COSTCENTER_ID_Param = new SqlParameter("@COSTCENTER_ID", DBNull.Value);

            SqlParameter STARTDATE_Param = new SqlParameter("@STARTDATE", StartDate);

            SqlParameter ENDDATE_Param = new SqlParameter("@ENDDATE", EndDate);

            SqlParameter TYPE_Param = new SqlParameter("@TYPE", Type);

            var billmaster = billMasterRepo.SQLQuery<TrialBalanceGoldVM>("exec PRC_RPT_TRIALBALANCE_Gold @COSTCENTER_ID,@STARTDATE, @ENDDATE, @TYPE, @CompanyBranchID", COSTCENTER_ID_Param, STARTDATE_Param, ENDDATE_Param, TYPE_Param, companyBranches_Param).ToList<TrialBalanceGoldVM>();

            return billmaster;
        }

        public List<InvoiceMotionViewVM> GetInvoiceMotionReportSearch(InvoiceMotionSearchModel searchObject)
        {
            string WhereCondition = "";

            WhereCondition += "COM_BRN_ID in(" + searchObject.companyBranches + ") AND ";
            WhereCondition += searchObject.CostCenterID == null ? "" : "COST_CENTER_ID = " + searchObject.CostCenterID + " AND ";
            WhereCondition += searchObject.CurrencyId == null ? "" :  "CURRENCY_ID = " + searchObject.CurrencyId + " AND ";
            WhereCondition += searchObject.EmployeeID == null ? "" : "EMP_ID = " + searchObject.EmployeeID + " AND ";
            WhereCondition += searchObject.EndDate == null ? "" : "cast(BILL_DATE as date) <= '" + searchObject.EndDate + "' AND ";
            WhereCondition += searchObject.StartDate == null ? "" : "cast(BILL_DATE as date) >= '" + searchObject.StartDate + "' AND ";
            WhereCondition += searchObject.CustID == null ? "" : "CUST_ACC_ID = " + searchObject.CustID + " AND ";
            WhereCondition += searchObject.storeID == null ? "" : "COM_STORE_ID = " + searchObject.storeID + " AND ";
            WhereCondition += searchObject.payTypeId == null ? "" : "BILL_PAY_WAY = " + searchObject.payTypeId + " AND ";

            if (searchObject.rptOptions.Count() > 0)
            {
                WhereCondition += "(";
                WhereCondition += searchObject.rptOptions.Any(x => x == 1) ? "BILL_IS_POST = 1 OR " : "";
                WhereCondition += searchObject.rptOptions.Any(x => x == 2) ? "BILL_IS_POST = 0 OR " : "";
                if (WhereCondition.Contains("BILL_IS_POST"))
                    WhereCondition = WhereCondition.Remove(WhereCondition.Length - 4, 3) + ") AND ";
                else
                    WhereCondition = WhereCondition.Remove(WhereCondition.Length - 1, 1);
            }

            if (searchObject.bilSettings.Count() > 0)
            {
                WhereCondition += "(";
                for (int i = 0; i < searchObject.bilSettings.Count(); i++)
                {
                    WhereCondition += "BILL_SETTING_ID = " + searchObject.bilSettings[i] + " OR ";
                }
                WhereCondition = WhereCondition.Remove(WhereCondition.Length - 4, 3) + ") AND ";
            }

            WhereCondition += searchObject.finalize == null ? "1=1" : "BILL_PAY_WAY = " + searchObject.finalize;        

            var ItemMotionList = billMasterRepo.SQLQuery<InvoiceMotionViewVM>("select * from INVOICE_MOTION_VIEW Where " + WhereCondition).ToList();
            return ItemMotionList;
        }

        public List<DetectionReportTotalVM> getAccountDetectingReportData(string companyBranches, string startDate, string endDate)
        {
            string WhereCondition = "";

            WhereCondition += "COM_BRN_ID in(" + companyBranches + ") AND ";
            WhereCondition += endDate == null ? "" : "cast(BILL_DATE as date) <= '" + endDate + "' AND ";
            WhereCondition += startDate == null ? "" : "cast(BILL_DATE as date) >= '" + startDate + "' ";
            var ItemMotionList = billMasterRepo.SQLQuery<DetectionReportTotalVM>("select * from DetectionReportTotalView Where " + WhereCondition + " order by BILL_DATE desc").ToList();
            return ItemMotionList;
        }

        public List<DetectionReportTotalVM> getAccountDetectingReportDataChart(string companyBranches, string startDate, string endDate)
        {
            string WhereCondition = "";

            //WhereCondition += "COM_BRN_ID in(" + companyBranches + ") AND ";
            WhereCondition += endDate == null ? "" : "BILL_DATE <= '" + endDate + "' AND ";
            WhereCondition += startDate == null ? "" : "BILL_DATE >= '" + startDate + "' ";
            var ItemMotionList = billMasterRepo.SQLQuery<DetectionReportTotalVM>("select * from DetectionReportTotalView Where " + WhereCondition + " order by BILL_DATE desc").ToList();
            return ItemMotionList;
        }

        public List<SalesTaxViewVM> getSalesTaxReportData(string companyBranches, string startDate, string endDate)
        {
            string WhereCondition = "";

            WhereCondition += "COM_BRN_ID in(" + companyBranches + ") AND ";
            WhereCondition += endDate == null ? "" : "cast(BILL_DATE as date) <= '" + endDate + "' AND ";
            WhereCondition += startDate == null ? "" : "cast(BILL_DATE as date) >= '" + startDate + "' ";
            var ItemMotionList = billMasterRepo.SQLQuery<SalesTaxViewVM>("select * from SalesTaxView Where " + WhereCondition + " order by BILL_DATE desc").ToList();
            return ItemMotionList;
        }

        public List<PurchasingTaxViewVM> getPurchasingTaxReportData(string companyBranches, string startDate, string endDate)
        {
            string WhereCondition = "";

            WhereCondition += "COM_BRN_ID in(" + companyBranches + ") AND ";
            WhereCondition += endDate == null ? "" : "cast(BILL_DATE as date) <= '" + endDate + "' AND ";
            WhereCondition += startDate == null ? "" : "cast(BILL_DATE as date) >= '" + startDate + "' ";
            var ItemMotionList = billMasterRepo.SQLQuery<PurchasingTaxViewVM>("select * from PurchasingTaxView Where " + WhereCondition + " order by BILL_DATE desc").ToList();
            return ItemMotionList;
        }

        public List<BILL_MASTERVM> getNotPotedBills(BillPostingSearchVM searchObject)
        {
            try
            {
                return billMasterRepo.Filter(p =>
                        p.BILL_IS_POST == false && p.BILL_DATE >= searchObject.startDate &&
                        p.BILL_DATE <= searchObject.endDate &&
                        (searchObject.CostCenterId != null ? p.COST_CENTER_ID == searchObject.CostCenterId : true) &&
                        (searchObject.EmployeeId != null ? p.EMP_ID == searchObject.EmployeeId : true) &&
                        (searchObject.StoreId != null ? p.COM_STORE_ID == searchObject.StoreId : true) &&
                        (searchObject.BillTypeId != null ? p.BILL_SETTING_ID == searchObject.BillTypeId : true) &&
                        (searchObject.PayTypeId != null ? p.BILL_PAY_WAY == searchObject.PayTypeId.ToString() : true))
                    .Where(p => (searchObject.FromBillNumber != null
                                    ? int.Parse(p.BILL_NUMBER) >= searchObject.FromBillNumber
                                    : true) &&
                                (searchObject.ToBillNumber != null
                                    ? int.Parse(p.BILL_NUMBER) <= searchObject.ToBillNumber
                                    : true))
                    .Select(p =>
                        new BILL_MASTERVM
                        {
                            BILLCURRENTPAIDEDVALUE = p.BILLCURRENTPAIDEDVALUE ?? 0,
                            BILLCURRENTREMAINVALUE = p.BILLCURRENTREMAINVALUE ?? 0,
                            BILL_ANOTHER_DISC = p.BILL_ANOTHER_DISC ?? 0,
                            BILL_COST = p.BILL_COST,
                            BILL_DATE = p.BILL_DATE,
                            BILL_ID = p.BILL_ID,
                            BILL_IS_POST = p.BILL_IS_POST,
                            BILL_NUMBER = p.BILL_NUMBER,
                            BILL_PAIDED = p.BILL_PAIDED,
                            BILL_PAY_WAY = p.BILL_PAY_WAY,
                            BILL_PROFIT = p.BILL_PROFIT,
                            BILL_REMARKS = p.BILL_REMARKS,
                            BILL_SETTING_ID = p.BILL_SETTING_ID,
                            BILL_TOTAL = p.BILL_TOTAL ?? 0,
                            BILL_TOTAL_DISC = p.BILL_TOTAL_DISC ?? 0,
                            BILL_TOTAL_EXTRA = p.BILL_TOTAL_EXTRA ?? 0,
                            BILL_TYPE = p.BILL_TYPE,
                            BILL_WIEGHT = p.BILL_WIEGHT,
                            COM_BRN_ID = p.COM_BRN_ID,
                            COM_STORE_ID = p.COM_STORE_ID,
                            COST_CENTER_ID = p.COST_CENTER_ID,
                            COUNTER_VALUE = p.COUNTER_VALUE,
                            CURRENCY_ID = p.CURRENCY_ID,
                            CURRENCY_RATE = p.CURRENCY_RATE,
                            CUST_ACC_ID = p.CUST_ACC_ID,
                            GOLD_ACC_ID = p.GOLD_ACC_ID,
                            CUST_BRN_ID = p.CUST_BRN_ID,
                            EMP_ID = p.EMP_ID,
                            ENTRY_ID = p.ENTRY_ID,
                            IS_IT_RESERVATION = p.IS_IT_RESERVATION,
                            ITEM_ACC_ID = p.ITEM_ACC_ID,
                            kest_value = p.kest_value,
                            PAY_ACC_ID = p.PAY_ACC_ID,
                            SELL_TYPE_ID = p.SELL_TYPE_ID,
                            SHIFT_NUMBER = p.SHIFT_NUMBER,
                            THE_LAST_DATE_FOR_RETURN = p.THE_LAST_DATE_FOR_RETURN,
                            THE_RETURN_PERCENTAGE = p.THE_RETURN_PERCENTAGE,
                            TO_COM_STORE_ID = p.TO_COM_STORE_ID,
                            WORK_ORDER_NUMBER = p.WORK_ORDER_NUMBER,
                            AddedBy = p.AddedBy,
                            AddedOn = p.AddedOn,
                            UpdatedBy = p.UpdatedBy,
                            updatedOn = p.updatedOn,
                            BILL_PERCENTAGE_DISC = p.BILL_PERCENTAGE_DISC ?? 0,
                            CostCenterID = p.CostCenterID,
                            Total18Quantity = p.Total18Quantity ?? 0,
                            Total21Quantity = p.Total21Quantity ?? 0,
                            Total22Quantity = p.Total22Quantity ?? 0,
                            Total24Quantity = p.Total24Quantity ?? 0,
                            TotalItemWages = p.TotalItemWages ?? 0,
                            TotalManufactWages = p.TotalManufactWages ?? 0,
                            TotalMustPaid = p.TotalMustPaid ?? 0,
                            TotalPaid = p.TotalPaid ?? 0,
                            TotalPrice = p.TotalPrice ?? 0,
                            TotalQuantity = p.TotalQuantity ?? 0,
                            TotalRemaining = p.TotalRemaining ?? 0,
                            TotalVatRate = p.TotalVatRate ?? 0,
                            TotalWeight = p.TotalWeight ?? 0,
                            TransTotalGold18 = p.TransTotalGold18 ?? 0,
                            TransTotalGold21 = p.TransTotalGold21 ?? 0,
                            TransTotalGold22 = p.TransTotalGold22 ?? 0,
                            TransTotalGold24 = p.TransTotalGold24 ?? 0,
                            TransTotalGoldQuantity = p.TransTotalGoldQuantity ?? 0,
                            TotalVatValue = p.TotalVatValue ?? 0,
                            TotalBeforeDiscount = p.TotalBeforeDiscount ?? 0,
                            TotalAfterDiscount = p.TotalAfterDiscount ?? 0,
                            TotalWagesDiscRate = p.TotalWagesDiscRate ?? 0,
                            TotalWagesDiscValue = p.TotalWagesDiscValue ?? 0,
                            EditReason = p.EditReason,
                            DeliveryPersonName = p.DeliveryPersonName,
                            ExternalNumber = p.ExternalNumber,
                            ExemptOfTaxRate = p.ExemptOfTaxRate ?? 0,
                            TotalExemptOfTax = p.TotalExemptOfTax ?? 0,
                            TotalMainVat = p.TotalMainVat ?? 0,
                            MainVatRate = p.MainVatRate ?? 0,
                            TotalZeroVat = p.TotalZeroVat ?? 0,
                            ZeroVatRate = p.ZeroVatRate ?? 0,
                            ExemptOfTaxValue = p.ExemptOfTaxValue ?? 0,
                            MainVatValue = p.MainVatValue ?? 0,
                            ZeroVatValue = p.ZeroVatValue ?? 0,
                            TotalBeforeTax = p.TotalBeforeTax ?? 0,
                            TotalAfterTax = p.TotalAfterTax ?? 0,
                            TotalTaxableAmount = p.TotalTaxableAmount ?? 0,
                            TotalNotTaxableAmount = p.TotalNotTaxableAmount ?? 0,
                            TotalAllDisc = p.TotalAllDisc ?? 0,
                            TotalDisc = p.TotalDisc ?? 0,
                            TotalAllDiscPercentage = p.TotalAllDiscPercentage ?? 0,
                            TotalDiscPercentage = p.TotalDiscPercentage ?? 0,
                            DiscountAmount = p.DiscountAmount ?? 0,
                        }).ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public bool SetBillsPosted(List<int> BillIds)
        {
            try
            {            
                var Bills = billMasterRepo.Filter(p => BillIds.Contains((int) p.BILL_ID));
                foreach (var bill in Bills)
                {
                    try
                    {
                        bill.BILL_IS_POST = true;
                        billMasterRepo.Update(bill, bill.BILL_ID);
                    }
                    catch (Exception e)
                    {
                        throw;
                    }
                }
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
