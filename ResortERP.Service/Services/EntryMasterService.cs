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
    public class EntryMasterService : IEntryMasterService, IDisposable
    {
        IGenericRepository<ENTRY_MASTER> entrymasterRepo;
        IGenericRepository<ENTRY_DETAILS> _entryDetailsRepo;
        IGenericRepository<ENTRY_SETTINGS> settingRepo;
        IGenericRepository<ACCOUNTS> accountRepo;
        IGenericRepository<Currency> currencyRepo;
        IGenericRepository<EMPLOYEES> empRepo;

        public EntryMasterService(IGenericRepository<ENTRY_MASTER> entrymasterRepo, IGenericRepository<ENTRY_DETAILS> EntryDetailsRepoParam, IGenericRepository<ENTRY_SETTINGS> settingRepo, IGenericRepository<ACCOUNTS> accountRepo
            , IGenericRepository<Currency> currencyRepo, IGenericRepository<EMPLOYEES> empRepo)
        {
            this.entrymasterRepo = entrymasterRepo;
            this._entryDetailsRepo = EntryDetailsRepoParam;
            this.settingRepo = settingRepo;
            this.accountRepo = accountRepo;
            this.currencyRepo = currencyRepo;
            this.empRepo = empRepo;

        }

        public Task<int> CountAsync(int entryType)
        {
            return Task.Run<int>(() =>
            {
                return entrymasterRepo.CountAsync(e => e.ENTRY_SETTING_ID == entryType);
            });
        }

        public bool Delete(Entry_MasterVM entity)
        {
            ENTRY_MASTER et = new ENTRY_MASTER
            {
                ACC_ID = entity.ACC_ID,
                CustAccID = entity.CustAccID,
                EntryVal = entity.EntryVal,

                BILL_ID = entity.BILL_ID,
                CHECK_DATE = entity.CHECK_DATE,
                CHECK_NUMBER = entity.CHECK_NUMBER,
                COLLECT_ENTRY_CODE = entity.COLLECT_ENTRY_CODE,
                CURRENCY_ID = entity.CURRENCY_ID,
                CURRENCY_RATE = entity.CURRENCY_RATE,
                EMP_ID = entity.EMP_ID,
                ENTRY_CREDIT = entity.ENTRY_CREDIT,
                ENTRY_CURRENCY_DIFFERENCE = entity.ENTRY_CURRENCY_DIFFERENCE,
                ENTRY_DATE = entity.ENTRY_DATE,
                ENTRY_DEBIT = entity.ENTRY_DEBIT,
                ENTRY_GOLD_CREDIT = entity.ENTRY_GOLD_CREDIT,
                ENTRY_GOLD_DEBIT = entity.ENTRY_GOLD_DEBIT,
                ENTRY_ID = entity.ENTRY_ID,
                ENTRY_NUMBER = entity.ENTRY_NUMBER,
                ENTRY_REMARKS = entity.ENTRY_REMARKS,
                ENTRY_SETTING_ID = entity.ENTRY_SETTING_ID,
                for_kest = entity.for_kest,
                IS_POSTED = entity.IS_POSTED,
                PAIED = entity.PAIED,
                AddedBy = entity.AddedBy,
                AddedOn = entity.AddedOn,
                UpdatedBy = entity.UpdatedBy,
                updatedOn = entity.updatedOn,
                RelatedEntryID = entity.RelatedEntryID,
                CustID = entity.CustID,
                OpeningAccID = entity.OpeningAccID,
                COM_BRN_ID = entity.COM_BRN_ID,
                TotalCredit = entity.TotalCredit,
                TotalDepit = entity.TotalDepit,
                TotalGoldCredit24 = entity.TotalGoldCredit24,
                TotalGoldDepit24 = entity.TotalGoldDepit24,
                TotalGoldCredit22 = entity.TotalGoldCredit22,
                TotalGoldDepit22 = entity.TotalGoldDepit22,
                TotalGoldCredit21 = entity.TotalGoldCredit21,
                TotalGoldDepit21 = entity.TotalGoldDepit21,
                TotalGoldCredit18 = entity.TotalGoldCredit18,
                TotalGoldDepit18 = entity.TotalGoldDepit18,
                TotalCurrencyCredit = entity.TotalCurrencyCredit,
                TotalCurrenctDepit = entity.TotalCurrenctDepit,
                TotalCurGoldCredit24 = entity.TotalCurGoldCredit24,
                TotalCurGoldDepit24 = entity.TotalCurGoldDepit24,
                TotalCurGoldCredit22 = entity.TotalCurGoldCredit22,
                TotalCurGoldDepit22 = entity.TotalCurGoldDepit22,
                TotalCurGoldCredit21 = entity.TotalCurGoldCredit21,
                TotalCurGoldDepit21 = entity.TotalCurGoldDepit21,
                TotalCurGoldCredit18 = entity.TotalCurGoldCredit18,
                TotalCurGoldDepit18 = entity.TotalCurGoldDepit18,
                TotalTaxValueCredit = entity.TotalTaxValueCredit,
                TotalTaxValueDebit = entity.TotalTaxValueDebit,
                TotalTaxRateCredit = entity.TotalTaxRateCredit,
                TotalTaxRateDebit = entity.TotalTaxRateDebit,
                TotalNotTaxCredit = entity.TotalNotTaxCredit,
                TotalNotTaxDebit = entity.TotalNotTaxDebit,

                Taxable = entity.Taxable,
                TaxRate = entity.TaxRate,
                TaxValue = entity.TaxValue,

                TotalCreditWithTax = entity.TotalCreditWithTax,
                TotalDepitWithTax = entity.TotalDepitWithTax,

                EditReason = entity.EditReason,
                DeliveryPersonName = entity.DeliveryPersonName,

                ExternalNumber = entity.ExternalNumber,

                TotalCreditExemptOfTax = entity.TotalCreditExemptOfTax,
                ExemptCreditOfTaxValue = entity.ExemptCreditOfTaxValue,
                TotalDepitExemptOfTax = entity.TotalDepitExemptOfTax,
                ExemptDepitOfTaxValue = entity.ExemptDepitOfTaxValue,
                TotalCreditTotalMainVat = entity.TotalCreditTotalMainVat,
                MainCreditVatValue = entity.MainCreditVatValue,
                TotalDepitTotalMainVat = entity.TotalDepitTotalMainVat,
                MainDepitVatValue = entity.MainDepitVatValue,
                TotalCreditTotalZeroVat = entity.TotalCreditTotalZeroVat,
                ZeroCreditVatValue = entity.ZeroCreditVatValue,
                TotalDepitTotalZeroVat = entity.TotalDepitTotalZeroVat,
                ZeroDepitVatValue = entity.ZeroDepitVatValue,
                ExemptVatRate = entity.ExemptVatRate,
                MainVatRate = entity.MainVatRate,
                ZeroVatRate = entity.ZeroVatRate,
                TaxNumber = entity.TaxNumber
            };
            entrymasterRepo.Delete(et, entity.ENTRY_ID);
            return true;
        }

        public Task<bool> DeleteAsync(Entry_MasterVM entity)
        {
            return Task.Run<bool>(() =>
            {
                checkDailyEntryForDelete(entity.ENTRY_ID);
                ENTRY_MASTER et = new ENTRY_MASTER
                {
                    ACC_ID = entity.ACC_ID,
                    CustAccID = entity.CustAccID,
                    EntryVal = entity.EntryVal,

                    BILL_ID = entity.BILL_ID,
                    CHECK_DATE = entity.CHECK_DATE,
                    CHECK_NUMBER = entity.CHECK_NUMBER,
                    COLLECT_ENTRY_CODE = entity.COLLECT_ENTRY_CODE,
                    CURRENCY_ID = entity.CURRENCY_ID,
                    CURRENCY_RATE = entity.CURRENCY_RATE,
                    EMP_ID = entity.EMP_ID,
                    ENTRY_CREDIT = entity.ENTRY_CREDIT,
                    ENTRY_CURRENCY_DIFFERENCE = entity.ENTRY_CURRENCY_DIFFERENCE,
                    ENTRY_DATE = entity.ENTRY_DATE,
                    ENTRY_DEBIT = entity.ENTRY_DEBIT,
                    ENTRY_GOLD_CREDIT = entity.ENTRY_GOLD_CREDIT,
                    ENTRY_GOLD_DEBIT = entity.ENTRY_GOLD_DEBIT,
                    ENTRY_ID = entity.ENTRY_ID,
                    ENTRY_NUMBER = entity.ENTRY_NUMBER,
                    ENTRY_REMARKS = entity.ENTRY_REMARKS,
                    ENTRY_SETTING_ID = entity.ENTRY_SETTING_ID,
                    for_kest = entity.for_kest,
                    IS_POSTED = entity.IS_POSTED,
                    PAIED = entity.PAIED,
                    AddedBy = entity.AddedBy,
                    AddedOn = entity.AddedOn,
                    UpdatedBy = entity.UpdatedBy,
                    updatedOn = entity.updatedOn,
                    RelatedEntryID = entity.RelatedEntryID,
                    OpeningAccID = entity.OpeningAccID,
                    CustID = entity.CustID,
                    COM_BRN_ID = entity.COM_BRN_ID,

                    TotalCredit = entity.TotalCredit,
                    TotalDepit = entity.TotalDepit,
                    TotalGoldCredit24 = entity.TotalGoldCredit24,
                    TotalGoldDepit24 = entity.TotalGoldDepit24,
                    TotalGoldCredit22 = entity.TotalGoldCredit22,
                    TotalGoldDepit22 = entity.TotalGoldDepit22,
                    TotalGoldCredit21 = entity.TotalGoldCredit21,
                    TotalGoldDepit21 = entity.TotalGoldDepit21,
                    TotalGoldCredit18 = entity.TotalGoldCredit18,
                    TotalGoldDepit18 = entity.TotalGoldDepit18,
                    TotalCurrencyCredit = entity.TotalCurrencyCredit,
                    TotalCurrenctDepit = entity.TotalCurrenctDepit,
                    TotalCurGoldCredit24 = entity.TotalCurGoldCredit24,
                    TotalCurGoldDepit24 = entity.TotalCurGoldDepit24,
                    TotalCurGoldCredit22 = entity.TotalCurGoldCredit22,
                    TotalCurGoldDepit22 = entity.TotalCurGoldDepit22,
                    TotalCurGoldCredit21 = entity.TotalCurGoldCredit21,
                    TotalCurGoldDepit21 = entity.TotalCurGoldDepit21,
                    TotalCurGoldCredit18 = entity.TotalCurGoldCredit18,
                    TotalCurGoldDepit18 = entity.TotalCurGoldDepit18,
                    TotalTaxValueCredit = entity.TotalTaxValueCredit,
                    TotalTaxValueDebit = entity.TotalTaxValueDebit,
                    TotalTaxRateCredit = entity.TotalTaxRateCredit,
                    TotalTaxRateDebit = entity.TotalTaxRateDebit,
                    TotalNotTaxCredit = entity.TotalNotTaxCredit,
                    TotalNotTaxDebit = entity.TotalNotTaxDebit,

                    Taxable = entity.Taxable,
                    TaxRate = entity.TaxRate,
                    TaxValue = entity.TaxValue,

                    TotalCreditWithTax = entity.TotalCreditWithTax,
                    TotalDepitWithTax = entity.TotalDepitWithTax,

                    EditReason = entity.EditReason,
                    DeliveryPersonName = entity.DeliveryPersonName,

                    ExternalNumber = entity.ExternalNumber,

                    TotalCreditExemptOfTax = entity.TotalCreditExemptOfTax,
                    ExemptCreditOfTaxValue = entity.ExemptCreditOfTaxValue,
                    TotalDepitExemptOfTax = entity.TotalDepitExemptOfTax,
                    ExemptDepitOfTaxValue = entity.ExemptDepitOfTaxValue,
                    TotalCreditTotalMainVat = entity.TotalCreditTotalMainVat,
                    MainCreditVatValue = entity.MainCreditVatValue,
                    TotalDepitTotalMainVat = entity.TotalDepitTotalMainVat,
                    MainDepitVatValue = entity.MainDepitVatValue,
                    TotalCreditTotalZeroVat = entity.TotalCreditTotalZeroVat,
                    ZeroCreditVatValue = entity.ZeroCreditVatValue,
                    TotalDepitTotalZeroVat = entity.TotalDepitTotalZeroVat,
                    ZeroDepitVatValue = entity.ZeroDepitVatValue,
                    ExemptVatRate = entity.ExemptVatRate,
                    MainVatRate = entity.MainVatRate,
                    ZeroVatRate = entity.ZeroVatRate,
                    TaxNumber = entity.TaxNumber
                };
                entrymasterRepo.Delete(et, entity.ENTRY_ID);

                List<ENTRY_DETAILS> ExistingDetails = GetAllDetails(entity.ENTRY_ID);
                if (ExistingDetails.Count > 0)
                {
                    //Delete ALl
                    foreach (var item in ExistingDetails)
                    {
                        //object [] keys = { item, item.ENTRY_ID, item, item.ENTRY_ROW_NUMBER };
                        object[] keys = new object[2] { item.ENTRY_ID, item.ENTRY_ROW_NUMBER };
                        try
                        {
                            _entryDetailsRepo.DeleteComposite(item, keys);
                        }
                        catch (Exception ex)
                        {

                        }
                    }
                }

                return true;
            });
        }

        public void Dispose()
        {
            entrymasterRepo.Dispose();
            settingRepo.Dispose();
            accountRepo.Dispose();
            currencyRepo.Dispose();
            empRepo.Dispose();

        }

        public Task<List<Entry_MasterVM>> GetAllAsync(int entryType, int pageNum, int pageSize)
        {
            return Task.Run<List<Entry_MasterVM>>(() =>
            {
                int rowCount;
                var q = from entity in entrymasterRepo.GetPaged<long>(pageNum, pageSize, p => p.ENTRY_SETTING_ID == entryType, p => p.ENTRY_ID, false, out rowCount)
                        join emp in empRepo.GetPaged<short>(pageNum, pageSize, p => p.EMP_ID, false, out rowCount) on entity.EMP_ID equals emp.EMP_ID into group1
                        from g1 in group1.DefaultIfEmpty()
                        join curr in currencyRepo.GetPaged<short>(pageNum, pageSize, p => p.CURRENCY_ID, false, out rowCount) on entity.CURRENCY_ID equals curr.CURRENCY_ID into group2
                        from g2 in group2.DefaultIfEmpty()
                        join acc in accountRepo.GetPaged<int>(pageNum, pageSize, p => p.ACC_ID, false, out rowCount) on entity.ACC_ID equals acc.ACC_ID into group3
                        from g3 in group3.DefaultIfEmpty()

                        select new Entry_MasterVM
                        {
                            ACC_ID = entity.ACC_ID,
                            CustAccID = entity.CustAccID,
                            EntryVal = entity.EntryVal,

                            BILL_ID = entity.BILL_ID,
                            CHECK_DATE = entity.CHECK_DATE,
                            CHECK_NUMBER = entity.CHECK_NUMBER,
                            COLLECT_ENTRY_CODE = entity.COLLECT_ENTRY_CODE,
                            CURRENCY_ID = entity.CURRENCY_ID,
                            CURRENCY_RATE = entity.CURRENCY_RATE,
                            EMP_ID = entity.EMP_ID,
                            ENTRY_CREDIT = entity.ENTRY_CREDIT,
                            ENTRY_CURRENCY_DIFFERENCE = entity.ENTRY_CURRENCY_DIFFERENCE,
                            ENTRY_DATE = entity.ENTRY_DATE,
                            ENTRY_DEBIT = entity.ENTRY_DEBIT,
                            ENTRY_GOLD_CREDIT = entity.ENTRY_GOLD_CREDIT,
                            ENTRY_GOLD_DEBIT = entity.ENTRY_GOLD_DEBIT,
                            ENTRY_ID = entity.ENTRY_ID,
                            ENTRY_NUMBER = entity.ENTRY_NUMBER,
                            ENTRY_REMARKS = entity.ENTRY_REMARKS,
                            ENTRY_SETTING_ID = entity.ENTRY_SETTING_ID,
                            for_kest = entity.for_kest,
                            IS_POSTED = entity.IS_POSTED,
                            PAIED = entity.PAIED,
                            AddedBy = entity.AddedBy,
                            AddedOn = entity.AddedOn,
                            UpdatedBy = entity.UpdatedBy,
                            updatedOn = entity.updatedOn,
                            RelatedEntryID = entity.RelatedEntryID,
                            EmpName = g1.EMP_AR_NAME,
                            CurrencyName = g2.CURRENCY_AR_NAME,
                            AccountName = g3.ACC_AR_NAME,
                            OpeningAccID = entity.OpeningAccID,
                            CustID = entity.CustID,
                            COM_BRN_ID = entity.COM_BRN_ID,

                            TotalCredit = entity.TotalCredit,
                            TotalDepit = entity.TotalDepit,
                            TotalGoldCredit24 = entity.TotalGoldCredit24,
                            TotalGoldDepit24 = entity.TotalGoldDepit24,
                            TotalGoldCredit22 = entity.TotalGoldCredit22,
                            TotalGoldDepit22 = entity.TotalGoldDepit22,
                            TotalGoldCredit21 = entity.TotalGoldCredit21,
                            TotalGoldDepit21 = entity.TotalGoldDepit21,
                            TotalGoldCredit18 = entity.TotalGoldCredit18,
                            TotalGoldDepit18 = entity.TotalGoldDepit18,
                            TotalCurrencyCredit = entity.TotalCurrencyCredit,
                            TotalCurrenctDepit = entity.TotalCurrenctDepit,
                            TotalCurGoldCredit24 = entity.TotalCurGoldCredit24,
                            TotalCurGoldDepit24 = entity.TotalCurGoldDepit24,
                            TotalCurGoldCredit22 = entity.TotalCurGoldCredit22,
                            TotalCurGoldDepit22 = entity.TotalCurGoldDepit22,
                            TotalCurGoldCredit21 = entity.TotalCurGoldCredit21,
                            TotalCurGoldDepit21 = entity.TotalCurGoldDepit21,
                            TotalCurGoldCredit18 = entity.TotalCurGoldCredit18,
                            TotalCurGoldDepit18 = entity.TotalCurGoldDepit18,
                            TotalTaxValueCredit = entity.TotalTaxValueCredit,
                            TotalTaxValueDebit = entity.TotalTaxValueDebit,
                            TotalTaxRateCredit = entity.TotalTaxRateCredit,
                            TotalTaxRateDebit = entity.TotalTaxRateDebit,
                            TotalNotTaxCredit = entity.TotalNotTaxCredit,
                            TotalNotTaxDebit = entity.TotalNotTaxDebit,

                            Taxable = entity.Taxable,
                            TaxRate = entity.TaxRate,
                            TaxValue = entity.TaxValue,

                            TotalCreditWithTax = entity.TotalCreditWithTax,
                            TotalDepitWithTax = entity.TotalDepitWithTax,

                            EditReason = entity.EditReason,
                            DeliveryPersonName = entity.DeliveryPersonName,

                            ExternalNumber = entity.ExternalNumber,

                            TotalCreditExemptOfTax = entity.TotalCreditExemptOfTax,
                            ExemptCreditOfTaxValue = entity.ExemptCreditOfTaxValue,
                            TotalDepitExemptOfTax = entity.TotalDepitExemptOfTax,
                            ExemptDepitOfTaxValue = entity.ExemptDepitOfTaxValue,
                            TotalCreditTotalMainVat = entity.TotalCreditTotalMainVat,
                            MainCreditVatValue = entity.MainCreditVatValue,
                            TotalDepitTotalMainVat = entity.TotalDepitTotalMainVat,
                            MainDepitVatValue = entity.MainDepitVatValue,
                            TotalCreditTotalZeroVat = entity.TotalCreditTotalZeroVat,
                            ZeroCreditVatValue = entity.ZeroCreditVatValue,
                            TotalDepitTotalZeroVat = entity.TotalDepitTotalZeroVat,
                            ZeroDepitVatValue = entity.ZeroDepitVatValue,
                            ExemptVatRate = entity.ExemptVatRate,
                            MainVatRate = entity.MainVatRate,
                            ZeroVatRate = entity.ZeroVatRate,
                            TaxNumber = entity.TaxNumber

                        };
                return q.ToList();
            });
        }
        public List<ENTRY_MASTER> getByEmp(int empId)
        {
            var q = entrymasterRepo.GetAll().Where(a => a.EMP_ID == empId).ToList();
            return q;
        }
        public List<ENTRY_MASTER> getByCust(int custId)
        {
            var q = entrymasterRepo.GetAll().Where(a => a.ACC_ID == custId).ToList();
            return q;
        }
        public List<ENTRY_MASTER> getByCurrency(int currencyId)
        {
            var q = entrymasterRepo.GetAll().Where(a => a.CURRENCY_ID == currencyId).ToList();
            return q;
        }
        public List<Entry_MasterVM> GetAll()
        {
            var q = from entity in entrymasterRepo.GetAll()
                    select new Entry_MasterVM
                    {
                        ACC_ID = entity.ACC_ID,
                        CustAccID = entity.CustAccID,
                        EntryVal = entity.EntryVal,

                        BILL_ID = entity.BILL_ID,
                        CHECK_DATE = entity.CHECK_DATE,
                        CHECK_NUMBER = entity.CHECK_NUMBER,
                        COLLECT_ENTRY_CODE = entity.COLLECT_ENTRY_CODE,
                        CURRENCY_ID = entity.CURRENCY_ID,
                        CURRENCY_RATE = entity.CURRENCY_RATE,
                        EMP_ID = entity.EMP_ID,
                        ENTRY_CREDIT = entity.ENTRY_CREDIT,
                        ENTRY_CURRENCY_DIFFERENCE = entity.ENTRY_CURRENCY_DIFFERENCE,
                        ENTRY_DATE = entity.ENTRY_DATE,
                        ENTRY_DEBIT = entity.ENTRY_DEBIT,
                        ENTRY_GOLD_CREDIT = entity.ENTRY_GOLD_CREDIT,
                        ENTRY_GOLD_DEBIT = entity.ENTRY_GOLD_DEBIT,
                        ENTRY_ID = entity.ENTRY_ID,
                        ENTRY_NUMBER = entity.ENTRY_NUMBER,
                        ENTRY_REMARKS = entity.ENTRY_REMARKS,
                        ENTRY_SETTING_ID = entity.ENTRY_SETTING_ID,
                        for_kest = entity.for_kest,
                        IS_POSTED = entity.IS_POSTED,
                        PAIED = entity.PAIED,
                        AddedBy = entity.AddedBy,
                        AddedOn = entity.AddedOn,
                        UpdatedBy = entity.UpdatedBy,
                        updatedOn = entity.updatedOn,
                        RelatedEntryID = entity.RelatedEntryID,
                        OpeningAccID = entity.OpeningAccID,
                        CustID = entity.CustID,
                        COM_BRN_ID = entity.COM_BRN_ID,

                        TotalCredit = entity.TotalCredit,
                        TotalDepit = entity.TotalDepit,
                        TotalGoldCredit24 = entity.TotalGoldCredit24,
                        TotalGoldDepit24 = entity.TotalGoldDepit24,
                        TotalGoldCredit22 = entity.TotalGoldCredit22,
                        TotalGoldDepit22 = entity.TotalGoldDepit22,
                        TotalGoldCredit21 = entity.TotalGoldCredit21,
                        TotalGoldDepit21 = entity.TotalGoldDepit21,
                        TotalGoldCredit18 = entity.TotalGoldCredit18,
                        TotalGoldDepit18 = entity.TotalGoldDepit18,
                        TotalCurrencyCredit = entity.TotalCurrencyCredit,
                        TotalCurrenctDepit = entity.TotalCurrenctDepit,
                        TotalCurGoldCredit24 = entity.TotalCurGoldCredit24,
                        TotalCurGoldDepit24 = entity.TotalCurGoldDepit24,
                        TotalCurGoldCredit22 = entity.TotalCurGoldCredit22,
                        TotalCurGoldDepit22 = entity.TotalCurGoldDepit22,
                        TotalCurGoldCredit21 = entity.TotalCurGoldCredit21,
                        TotalCurGoldDepit21 = entity.TotalCurGoldDepit21,
                        TotalCurGoldCredit18 = entity.TotalCurGoldCredit18,
                        TotalCurGoldDepit18 = entity.TotalCurGoldDepit18,
                        TotalTaxValueCredit = entity.TotalTaxValueCredit,
                        TotalTaxValueDebit = entity.TotalTaxValueDebit,
                        TotalTaxRateCredit = entity.TotalTaxRateCredit,
                        TotalTaxRateDebit = entity.TotalTaxRateDebit,
                        TotalNotTaxCredit = entity.TotalNotTaxCredit,
                        TotalNotTaxDebit = entity.TotalNotTaxDebit,

                        Taxable = entity.Taxable,
                        TaxRate = entity.TaxRate,
                        TaxValue = entity.TaxValue,

                        TotalCreditWithTax = entity.TotalCreditWithTax,
                        TotalDepitWithTax = entity.TotalDepitWithTax,

                        EditReason = entity.EditReason,
                        DeliveryPersonName = entity.DeliveryPersonName,

                        ExternalNumber = entity.ExternalNumber,

                        TotalCreditExemptOfTax = entity.TotalCreditExemptOfTax,
                        ExemptCreditOfTaxValue = entity.ExemptCreditOfTaxValue,
                        TotalDepitExemptOfTax = entity.TotalDepitExemptOfTax,
                        ExemptDepitOfTaxValue = entity.ExemptDepitOfTaxValue,
                        TotalCreditTotalMainVat = entity.TotalCreditTotalMainVat,
                        MainCreditVatValue = entity.MainCreditVatValue,
                        TotalDepitTotalMainVat = entity.TotalDepitTotalMainVat,
                        MainDepitVatValue = entity.MainDepitVatValue,
                        TotalCreditTotalZeroVat = entity.TotalCreditTotalZeroVat,
                        ZeroCreditVatValue = entity.ZeroCreditVatValue,
                        TotalDepitTotalZeroVat = entity.TotalDepitTotalZeroVat,
                        ZeroDepitVatValue = entity.ZeroDepitVatValue,
                        ExemptVatRate = entity.ExemptVatRate,
                        MainVatRate = entity.MainVatRate,
                        ZeroVatRate = entity.ZeroVatRate,
                        TaxNumber = entity.TaxNumber

                    };
            return q.ToList();
        }

        public long Insert(Entry_MasterVM entity)
        {
            ENTRY_MASTER et = new ENTRY_MASTER
            {
                ACC_ID = entity.ACC_ID,
                CustAccID = entity.CustAccID,
                EntryVal = entity.EntryVal,

                BILL_ID = entity.BILL_ID,
                CHECK_DATE = entity.CHECK_DATE,
                CHECK_NUMBER = entity.CHECK_NUMBER,
                COLLECT_ENTRY_CODE = entity.COLLECT_ENTRY_CODE,
                CURRENCY_ID = entity.CURRENCY_ID,
                CURRENCY_RATE = entity.CURRENCY_RATE,
                EMP_ID = entity.EMP_ID,
                ENTRY_CREDIT = entity.ENTRY_CREDIT,
                ENTRY_CURRENCY_DIFFERENCE = entity.ENTRY_CURRENCY_DIFFERENCE,
                ENTRY_DATE = entity.ENTRY_DATE,
                ENTRY_DEBIT = entity.ENTRY_DEBIT,
                ENTRY_GOLD_CREDIT = entity.ENTRY_GOLD_CREDIT,
                ENTRY_GOLD_DEBIT = entity.ENTRY_GOLD_DEBIT,
                ENTRY_ID = entity.ENTRY_ID,
                ENTRY_NUMBER = entity.ENTRY_NUMBER,
                ENTRY_REMARKS = entity.ENTRY_REMARKS,
                ENTRY_SETTING_ID = entity.ENTRY_SETTING_ID,
                for_kest = entity.for_kest,
                IS_POSTED = entity.IS_POSTED,
                PAIED = entity.PAIED,
                AddedBy = entity.AddedBy,
                AddedOn = entity.AddedOn,
                UpdatedBy = entity.UpdatedBy,
                updatedOn = entity.updatedOn,
                RelatedEntryID = entity.RelatedEntryID,
                OpeningAccID = entity.OpeningAccID,
                CustID = entity.CustID,
                COM_BRN_ID = entity.COM_BRN_ID,

                TotalCredit = entity.TotalCredit,
                TotalDepit = entity.TotalDepit,
                TotalGoldCredit24 = entity.TotalGoldCredit24,
                TotalGoldDepit24 = entity.TotalGoldDepit24,
                TotalGoldCredit22 = entity.TotalGoldCredit22,
                TotalGoldDepit22 = entity.TotalGoldDepit22,
                TotalGoldCredit21 = entity.TotalGoldCredit21,
                TotalGoldDepit21 = entity.TotalGoldDepit21,
                TotalGoldCredit18 = entity.TotalGoldCredit18,
                TotalGoldDepit18 = entity.TotalGoldDepit18,
                TotalCurrencyCredit = entity.TotalCurrencyCredit,
                TotalCurrenctDepit = entity.TotalCurrenctDepit,
                TotalCurGoldCredit24 = entity.TotalCurGoldCredit24,
                TotalCurGoldDepit24 = entity.TotalCurGoldDepit24,
                TotalCurGoldCredit22 = entity.TotalCurGoldCredit22,
                TotalCurGoldDepit22 = entity.TotalCurGoldDepit22,
                TotalCurGoldCredit21 = entity.TotalCurGoldCredit21,
                TotalCurGoldDepit21 = entity.TotalCurGoldDepit21,
                TotalCurGoldCredit18 = entity.TotalCurGoldCredit18,
                TotalCurGoldDepit18 = entity.TotalCurGoldDepit18,
                TotalTaxValueCredit = entity.TotalTaxValueCredit,
                TotalTaxValueDebit = entity.TotalTaxValueDebit,
                TotalTaxRateCredit = entity.TotalTaxRateCredit,
                TotalTaxRateDebit = entity.TotalTaxRateDebit,
                TotalNotTaxCredit = entity.TotalNotTaxCredit,
                TotalNotTaxDebit = entity.TotalNotTaxDebit,

                Taxable = entity.Taxable,
                TaxRate = entity.TaxRate,
                TaxValue = entity.TaxValue,

                TotalCreditWithTax = entity.TotalCreditWithTax,
                TotalDepitWithTax = entity.TotalDepitWithTax,

                EditReason = entity.EditReason,
                DeliveryPersonName = entity.DeliveryPersonName,

                ExternalNumber = entity.ExternalNumber,

                TotalCreditExemptOfTax = entity.TotalCreditExemptOfTax,
                ExemptCreditOfTaxValue = entity.ExemptCreditOfTaxValue,
                TotalDepitExemptOfTax = entity.TotalDepitExemptOfTax,
                ExemptDepitOfTaxValue = entity.ExemptDepitOfTaxValue,
                TotalCreditTotalMainVat = entity.TotalCreditTotalMainVat,
                MainCreditVatValue = entity.MainCreditVatValue,
                TotalDepitTotalMainVat = entity.TotalDepitTotalMainVat,
                MainDepitVatValue = entity.MainDepitVatValue,
                TotalCreditTotalZeroVat = entity.TotalCreditTotalZeroVat,
                ZeroCreditVatValue = entity.ZeroCreditVatValue,
                TotalDepitTotalZeroVat = entity.TotalDepitTotalZeroVat,
                ZeroDepitVatValue = entity.ZeroDepitVatValue,
                ExemptVatRate = entity.ExemptVatRate,
                MainVatRate = entity.MainVatRate,
                ZeroVatRate = entity.ZeroVatRate,
                TaxNumber = entity.TaxNumber

            };
            entrymasterRepo.Add(et);
            return et.ENTRY_ID;
        }
        public long InsertGetLastId(Entry_MasterVM entity)
        {
            ENTRY_MASTER et = new ENTRY_MASTER
            {
                ACC_ID = entity.ACC_ID,
                CustAccID = entity.CustAccID,
                EntryVal = entity.EntryVal,

                BILL_ID = entity.BILL_ID,
                CHECK_DATE = entity.CHECK_DATE,
                CHECK_NUMBER = entity.CHECK_NUMBER,
                COLLECT_ENTRY_CODE = entity.COLLECT_ENTRY_CODE,
                CURRENCY_ID = entity.CURRENCY_ID,
                CURRENCY_RATE = entity.CURRENCY_RATE,
                EMP_ID = entity.EMP_ID,
                ENTRY_CREDIT = entity.ENTRY_CREDIT,
                ENTRY_CURRENCY_DIFFERENCE = entity.ENTRY_CURRENCY_DIFFERENCE,
                ENTRY_DATE = entity.ENTRY_DATE,
                ENTRY_DEBIT = entity.ENTRY_DEBIT,
                ENTRY_GOLD_CREDIT = entity.ENTRY_GOLD_CREDIT,
                ENTRY_GOLD_DEBIT = entity.ENTRY_GOLD_DEBIT,
                ENTRY_ID = entity.ENTRY_ID,
                ENTRY_NUMBER = entity.ENTRY_NUMBER,
                ENTRY_REMARKS = entity.ENTRY_REMARKS,
                ENTRY_SETTING_ID = entity.ENTRY_SETTING_ID,
                for_kest = entity.for_kest,
                IS_POSTED = entity.IS_POSTED,
                PAIED = entity.PAIED,
                AddedBy = entity.AddedBy,
                AddedOn = entity.AddedOn,
                UpdatedBy = entity.UpdatedBy,
                updatedOn = entity.updatedOn,
                RelatedEntryID = entity.RelatedEntryID,
                OpeningAccID = entity.OpeningAccID,
                CustID = entity.CustID,
                COM_BRN_ID = entity.COM_BRN_ID,

                TotalCredit = entity.TotalCredit,
                TotalDepit = entity.TotalDepit,
                TotalGoldCredit24 = entity.TotalGoldCredit24,
                TotalGoldDepit24 = entity.TotalGoldDepit24,
                TotalGoldCredit22 = entity.TotalGoldCredit22,
                TotalGoldDepit22 = entity.TotalGoldDepit22,
                TotalGoldCredit21 = entity.TotalGoldCredit21,
                TotalGoldDepit21 = entity.TotalGoldDepit21,
                TotalGoldCredit18 = entity.TotalGoldCredit18,
                TotalGoldDepit18 = entity.TotalGoldDepit18,
                TotalCurrencyCredit = entity.TotalCurrencyCredit,
                TotalCurrenctDepit = entity.TotalCurrenctDepit,
                TotalCurGoldCredit24 = entity.TotalCurGoldCredit24,
                TotalCurGoldDepit24 = entity.TotalCurGoldDepit24,
                TotalCurGoldCredit22 = entity.TotalCurGoldCredit22,
                TotalCurGoldDepit22 = entity.TotalCurGoldDepit22,
                TotalCurGoldCredit21 = entity.TotalCurGoldCredit21,
                TotalCurGoldDepit21 = entity.TotalCurGoldDepit21,
                TotalCurGoldCredit18 = entity.TotalCurGoldCredit18,
                TotalCurGoldDepit18 = entity.TotalCurGoldDepit18,
                TotalTaxValueCredit = entity.TotalTaxValueCredit,
                TotalTaxValueDebit = entity.TotalTaxValueDebit,
                TotalTaxRateCredit = entity.TotalTaxRateCredit,
                TotalTaxRateDebit = entity.TotalTaxRateDebit,
                TotalNotTaxCredit = entity.TotalNotTaxCredit,
                TotalNotTaxDebit = entity.TotalNotTaxDebit,

                Taxable = entity.Taxable,
                TaxRate = entity.TaxRate,
                TaxValue = entity.TaxValue,

                TotalCreditWithTax = entity.TotalCreditWithTax,
                TotalDepitWithTax = entity.TotalDepitWithTax,

                EditReason = entity.EditReason,
                DeliveryPersonName = entity.DeliveryPersonName,

                ExternalNumber = entity.ExternalNumber,

                TotalCreditExemptOfTax = entity.TotalCreditExemptOfTax,
                ExemptCreditOfTaxValue = entity.ExemptCreditOfTaxValue,
                TotalDepitExemptOfTax = entity.TotalDepitExemptOfTax,
                ExemptDepitOfTaxValue = entity.ExemptDepitOfTaxValue,
                TotalCreditTotalMainVat = entity.TotalCreditTotalMainVat,
                MainCreditVatValue = entity.MainCreditVatValue,
                TotalDepitTotalMainVat = entity.TotalDepitTotalMainVat,
                MainDepitVatValue = entity.MainDepitVatValue,
                TotalCreditTotalZeroVat = entity.TotalCreditTotalZeroVat,
                ZeroCreditVatValue = entity.ZeroCreditVatValue,
                TotalDepitTotalZeroVat = entity.TotalDepitTotalZeroVat,
                ZeroDepitVatValue = entity.ZeroDepitVatValue,
                ExemptVatRate = entity.ExemptVatRate,
                MainVatRate = entity.MainVatRate,
                ZeroVatRate = entity.ZeroVatRate,
                AssetOperationMasterID = entity.AssetOperationMasterID,
                TaxNumber = entity.TaxNumber

            };
            entrymasterRepo.Add(et);
            return et.ENTRY_ID;
        }



        public Task<long> InsertAsync(Entry_MasterVM entity)
        {
            return Task.Run<long>(() =>
            {
                ENTRY_MASTER et = new ENTRY_MASTER
                {
                    ACC_ID = entity.ACC_ID,
                    CustAccID = entity.CustAccID,
                    EntryVal = entity.EntryVal,

                    BILL_ID = entity.BILL_ID,
                    CHECK_DATE = entity.CHECK_DATE,
                    CHECK_NUMBER = entity.CHECK_NUMBER,
                    COLLECT_ENTRY_CODE = entity.COLLECT_ENTRY_CODE,
                    CURRENCY_ID = entity.CURRENCY_ID,
                    CURRENCY_RATE = entity.CURRENCY_RATE,
                    EMP_ID = entity.EMP_ID,
                    ENTRY_CREDIT = entity.ENTRY_CREDIT,
                    ENTRY_CURRENCY_DIFFERENCE = entity.ENTRY_CURRENCY_DIFFERENCE,
                    ENTRY_DATE = entity.ENTRY_DATE,
                    ENTRY_DEBIT = entity.ENTRY_DEBIT,
                    ENTRY_GOLD_CREDIT = entity.ENTRY_GOLD_CREDIT,
                    ENTRY_GOLD_DEBIT = entity.ENTRY_GOLD_DEBIT,
                    ENTRY_ID = entity.ENTRY_ID,
                    ENTRY_NUMBER = entity.ENTRY_NUMBER,
                    ENTRY_REMARKS = entity.ENTRY_REMARKS,
                    ENTRY_SETTING_ID = entity.ENTRY_SETTING_ID,
                    for_kest = entity.for_kest,
                    IS_POSTED = entity.IS_POSTED,
                    PAIED = entity.PAIED,
                    AddedBy = entity.AddedBy,
                    AddedOn = entity.AddedOn,
                    UpdatedBy = entity.UpdatedBy,
                    updatedOn = entity.updatedOn,
                    RelatedEntryID = entity.RelatedEntryID,
                    OpeningAccID = entity.OpeningAccID,
                    CustID = entity.CustID,
                    COM_BRN_ID = entity.COM_BRN_ID,

                    TotalCredit = entity.TotalCredit,
                    TotalDepit = entity.TotalDepit,
                    TotalGoldCredit24 = entity.TotalGoldCredit24,
                    TotalGoldDepit24 = entity.TotalGoldDepit24,
                    TotalGoldCredit22 = entity.TotalGoldCredit22,
                    TotalGoldDepit22 = entity.TotalGoldDepit22,
                    TotalGoldCredit21 = entity.TotalGoldCredit21,
                    TotalGoldDepit21 = entity.TotalGoldDepit21,
                    TotalGoldCredit18 = entity.TotalGoldCredit18,
                    TotalGoldDepit18 = entity.TotalGoldDepit18,
                    TotalCurrencyCredit = entity.TotalCurrencyCredit,
                    TotalCurrenctDepit = entity.TotalCurrenctDepit,
                    TotalCurGoldCredit24 = entity.TotalCurGoldCredit24,
                    TotalCurGoldDepit24 = entity.TotalCurGoldDepit24,
                    TotalCurGoldCredit22 = entity.TotalCurGoldCredit22,
                    TotalCurGoldDepit22 = entity.TotalCurGoldDepit22,
                    TotalCurGoldCredit21 = entity.TotalCurGoldCredit21,
                    TotalCurGoldDepit21 = entity.TotalCurGoldDepit21,
                    TotalCurGoldCredit18 = entity.TotalCurGoldCredit18,
                    TotalCurGoldDepit18 = entity.TotalCurGoldDepit18,
                    TotalTaxValueCredit = entity.TotalTaxValueCredit,
                    TotalTaxValueDebit = entity.TotalTaxValueDebit,
                    TotalTaxRateCredit = entity.TotalTaxRateCredit,
                    TotalTaxRateDebit = entity.TotalTaxRateDebit,
                    TotalNotTaxCredit = entity.TotalNotTaxCredit,
                    TotalNotTaxDebit = entity.TotalNotTaxDebit,

                    Taxable = entity.Taxable,
                    TaxRate = entity.TaxRate,
                    TaxValue = entity.TaxValue,

                    TotalCreditWithTax = entity.TotalCreditWithTax,
                    TotalDepitWithTax = entity.TotalDepitWithTax,

                    EditReason = entity.EditReason,
                    DeliveryPersonName = entity.DeliveryPersonName,

                    ExternalNumber = entity.ExternalNumber,

                    TotalCreditExemptOfTax = entity.TotalCreditExemptOfTax,
                    ExemptCreditOfTaxValue = entity.ExemptCreditOfTaxValue,
                    TotalDepitExemptOfTax = entity.TotalDepitExemptOfTax,
                    ExemptDepitOfTaxValue = entity.ExemptDepitOfTaxValue,
                    TotalCreditTotalMainVat = entity.TotalCreditTotalMainVat,
                    MainCreditVatValue = entity.MainCreditVatValue,
                    TotalDepitTotalMainVat = entity.TotalDepitTotalMainVat,
                    MainDepitVatValue = entity.MainDepitVatValue,
                    TotalCreditTotalZeroVat = entity.TotalCreditTotalZeroVat,
                    ZeroCreditVatValue = entity.ZeroCreditVatValue,
                    TotalDepitTotalZeroVat = entity.TotalDepitTotalZeroVat,
                    ZeroDepitVatValue = entity.ZeroDepitVatValue,
                    ExemptVatRate = entity.ExemptVatRate,
                    MainVatRate = entity.MainVatRate,
                    ZeroVatRate = entity.ZeroVatRate,
                    TaxNumber = entity.TaxNumber

                };
                entrymasterRepo.Add(et);
                return et.ENTRY_ID;
            });
        }

        public long InsertEntryMasterDetails(EntryMasterDetails Obj)
        {
            long EntryID;
            //if entryMaster.ID is 0 then Insert else Update 
            if (Obj.EntryMaster.ENTRY_ID == 0)
            {
                EntryID = InsertGetLastId(Obj.EntryMaster);
                if (EntryID > 0)
                {
                    byte rowNo = 0;
                    foreach (var item in Obj.EntryDetails)
                    {
                        ENTRY_DETAILS Entry_DetailsMapped = new ENTRY_DETAILS();
                        Entry_DetailsMapped.ACC_ID = item.ACC_ID;
                        Entry_DetailsMapped.COST_CENTER_ID = item.COST_CENTER_ID;
                        Entry_DetailsMapped.Disable = item.Disable;
                        Entry_DetailsMapped.ENTRY_CREDIT = item.ENTRY_CREDIT;
                        Entry_DetailsMapped.ENTRY_DEBIT = item.ENTRY_DEBIT;

                        Entry_DetailsMapped.ENTRY_GOLD24_CREDIT = item.ENTRY_GOLD24_CREDIT;
                        Entry_DetailsMapped.ENTRY_GOLD24_DEBIT = item.ENTRY_GOLD24_DEBIT;

                        Entry_DetailsMapped.ENTRY_GOLD22_CREDIT = item.ENTRY_GOLD22_CREDIT;
                        Entry_DetailsMapped.ENTRY_GOLD22_DEBIT = item.ENTRY_GOLD22_DEBIT;

                        Entry_DetailsMapped.ENTRY_GOLD21_CREDIT = item.ENTRY_GOLD21_CREDIT;
                        Entry_DetailsMapped.ENTRY_GOLD21_DEBIT = item.ENTRY_GOLD21_DEBIT;

                        Entry_DetailsMapped.ENTRY_GOLD18_CREDIT = item.ENTRY_GOLD18_CREDIT;
                        Entry_DetailsMapped.ENTRY_GOLD18_DEBIT = item.ENTRY_GOLD18_DEBIT;

                        Entry_DetailsMapped.ENTRY_DETAILS_REMARKS = item.ENTRY_DETAILS_REMARKS;
                        Entry_DetailsMapped.ENTRY_ID = EntryID;
                        Entry_DetailsMapped.TaxValue = item.TaxValue;
                        Entry_DetailsMapped.Taxable = item.Taxable;
                        //  Entry_DetailsMapped.TaxAccountID = item.TaxAccountID;
                        //Entry_DetailsMapped.ENTRY_ROW_NUMBER = item.ENTRY_ROW_NUMBER;
                        Entry_DetailsMapped.ENTRY_ROW_NUMBER = rowNo;
                        Entry_DetailsMapped.TaxRate = item.TaxRate;

                        Entry_DetailsMapped.CheckNumber = item.CheckNumber;
                        Entry_DetailsMapped.CheckDate = item.CheckDate;
                        Entry_DetailsMapped.CheckIssueDate = item.CheckIssueDate;

                        Entry_DetailsMapped.IsExemptOfTax = item.IsExemptOfTax;


                        Entry_DetailsMapped.ExemptOfTaxValue = item.ExemptOfTaxValue;
                        Entry_DetailsMapped.IsMainVatValue = item.IsMainVatValue;
                        Entry_DetailsMapped.MainVatValue = item.MainVatValue;
                        Entry_DetailsMapped.IsZeroVatValue = item.IsZeroVatValue;
                        Entry_DetailsMapped.ZeroVatValue = item.ZeroVatValue;

                        Entry_DetailsMapped.MainVat = item.MainVat;

                        _entryDetailsRepo.Add(Entry_DetailsMapped);
                        rowNo++;
                    }
                }
            }
            else
            {
                EntryID = Obj.EntryMaster.ENTRY_ID;
                //Update
                bool UpdateStatus = Update(Obj.EntryMaster);
                if (UpdateStatus)
                {
                    //Getall
                    List<ENTRY_DETAILS> ExistingDetails = GetAllDetails(Obj.EntryMaster.ENTRY_ID);
                    if (ExistingDetails.Count > 0)
                    {
                        //Delete ALl
                        foreach (var item in ExistingDetails)
                        {
                            //object [] keys = { item, item.ENTRY_ID, item, item.ENTRY_ROW_NUMBER };
                            object[] keys = new object[2] { item.ENTRY_ID, item.ENTRY_ROW_NUMBER };
                            try
                            {
                                _entryDetailsRepo.DeleteComposite(item, keys);
                            }
                            catch (Exception ex)
                            {


                            }

                        }
                    }
                    //Insert Again
                    byte rowNo = 0;
                    foreach (var item in Obj.EntryDetails)
                    {
                        ENTRY_DETAILS Entry_DetailsMapped = new ENTRY_DETAILS();
                        Entry_DetailsMapped.ACC_ID = item.ACC_ID;
                        Entry_DetailsMapped.COST_CENTER_ID = item.COST_CENTER_ID;
                        Entry_DetailsMapped.Disable = item.Disable;
                        Entry_DetailsMapped.ENTRY_CREDIT = item.ENTRY_CREDIT;
                        Entry_DetailsMapped.ENTRY_DEBIT = item.ENTRY_DEBIT;
                        Entry_DetailsMapped.ENTRY_GOLD24_CREDIT = item.ENTRY_GOLD24_CREDIT;
                        Entry_DetailsMapped.ENTRY_GOLD24_DEBIT = item.ENTRY_GOLD24_DEBIT;

                        Entry_DetailsMapped.ENTRY_GOLD22_CREDIT = item.ENTRY_GOLD22_CREDIT;
                        Entry_DetailsMapped.ENTRY_GOLD22_DEBIT = item.ENTRY_GOLD22_DEBIT;

                        Entry_DetailsMapped.ENTRY_GOLD21_CREDIT = item.ENTRY_GOLD21_CREDIT;
                        Entry_DetailsMapped.ENTRY_GOLD21_DEBIT = item.ENTRY_GOLD21_DEBIT;

                        Entry_DetailsMapped.ENTRY_GOLD18_CREDIT = item.ENTRY_GOLD18_CREDIT;
                        Entry_DetailsMapped.ENTRY_GOLD18_DEBIT = item.ENTRY_GOLD18_DEBIT;
                        Entry_DetailsMapped.ENTRY_DETAILS_REMARKS = item.ENTRY_DETAILS_REMARKS;
                        Entry_DetailsMapped.ENTRY_ID = Obj.EntryMaster.ENTRY_ID;
                        Entry_DetailsMapped.TaxValue = item.TaxValue;
                        Entry_DetailsMapped.Taxable = item.Taxable;
                        Entry_DetailsMapped.TaxRate = item.TaxRate;

                        Entry_DetailsMapped.CheckNumber = item.CheckNumber;
                        Entry_DetailsMapped.CheckDate = item.CheckDate;
                        Entry_DetailsMapped.CheckIssueDate = item.CheckIssueDate;
                        Entry_DetailsMapped.IsExemptOfTax = item.IsExemptOfTax;


                        Entry_DetailsMapped.ExemptOfTaxValue = item.ExemptOfTaxValue;
                        Entry_DetailsMapped.IsMainVatValue = item.IsMainVatValue;
                        Entry_DetailsMapped.MainVatValue = item.MainVatValue;
                        Entry_DetailsMapped.IsZeroVatValue = item.IsZeroVatValue;
                        Entry_DetailsMapped.ZeroVatValue = item.ZeroVatValue;
                        Entry_DetailsMapped.MainVat = item.MainVat;

                        //Entry_DetailsMapped.ENTRY_ROW_NUMBER = item.ENTRY_ROW_NUMBER;
                        Entry_DetailsMapped.ENTRY_ROW_NUMBER = rowNo;
                        _entryDetailsRepo.Add(Entry_DetailsMapped);
                        rowNo++;
                    }



                    checkDailyEntryForDelete(Obj.EntryMaster.ENTRY_ID);


                }
            }
            return EntryID;
        }



        public long GetLastEntryNumber(int settingID)
        {
            var q = entrymasterRepo.Filter(m => m.ENTRY_SETTING_ID == settingID).OrderByDescending(m => m.ENTRY_ID).FirstOrDefault();
            if (q == null)
            {
                return 0;
            }
            else
                return Convert.ToInt64(q.ENTRY_NUMBER);
        }


        public List<ENTRY_DETAILS> GetAllDetails(long EntryMasterID)
        {
            List<ENTRY_DETAILS> entryDetails = new List<ENTRY_DETAILS>();
            entryDetails = _entryDetailsRepo.GetAll().Where(x => x.ENTRY_ID == EntryMasterID).ToList();

            return entryDetails;
        }



        public List<Entry_DetailsVM> GetDetailsByEntryId(long EntryMasterID)
        {
            //var detailsList = from det in _entryDetailsRepo.GetAll().Where(d => d.ENTRY_ID == EntryMasterID)
            //                  join acc in accountRepo.GetAll() on det.ACC_ID equals acc.ACC_ID into group1
            //                  from g in group1.DefaultIfEmpty()
            //                  join pAcc in accountRepo.GetAll() on g.PARENT_ACC_ID equals pAcc.ACC_ID into parentacc
            //                  from parentAccount in parentacc.DefaultIfEmpty()
            var detailsList = from det in _entryDetailsRepo.GetAll().Where(d => d.ENTRY_ID == EntryMasterID)
                join acc in accountRepo.GetAll() on det.ACC_ID equals acc.ACC_ID
                join acc2 in accountRepo.GetAll() on acc.PARENT_ACC_ID equals acc2.ACC_ID
                              select new Entry_DetailsVM
                              {
                                  ACC_ID = det.ACC_ID,
                                  COST_CENTER_ID = det.COST_CENTER_ID,
                                  Disable = det.Disable,
                                  ENTRY_CREDIT = det.ENTRY_CREDIT,
                                  ENTRY_DEBIT = det.ENTRY_DEBIT,

                                  ENTRY_GOLD24_CREDIT = det.ENTRY_GOLD24_CREDIT,
                                  ENTRY_GOLD24_DEBIT = det.ENTRY_GOLD24_DEBIT,

                                  ENTRY_GOLD22_CREDIT = det.ENTRY_GOLD22_CREDIT,
                                  ENTRY_GOLD22_DEBIT = det.ENTRY_GOLD22_DEBIT,

                                  ENTRY_GOLD21_CREDIT = det.ENTRY_GOLD21_CREDIT,
                                  ENTRY_GOLD21_DEBIT = det.ENTRY_GOLD21_DEBIT,

                                  ENTRY_GOLD18_CREDIT = det.ENTRY_GOLD18_CREDIT,
                                  ENTRY_GOLD18_DEBIT = det.ENTRY_GOLD18_DEBIT,

                                  ENTRY_DETAILS_REMARKS = det.ENTRY_DETAILS_REMARKS,
                                  ENTRY_ROW_NUMBER = det.ENTRY_ROW_NUMBER,
                                  ACC_AR_NAME = acc == null ? "" : acc.ACC_AR_NAME,
                                  Taxable = det.Taxable,
                                  TaxValue = det.TaxValue,
                                  TaxRate = det.TaxRate,
                                  CheckNumber = det.CheckNumber,
                                  CheckDate = det.CheckDate,
                                  CheckIssueDate = det.CheckIssueDate,
                                  IsExemptOfTax = det.IsExemptOfTax,

                                  ExemptOfTaxValue = det.ExemptOfTaxValue,
                                  IsMainVatValue = det.IsMainVatValue,
                                  MainVatValue = det.MainVatValue,
                                  IsZeroVatValue = det.IsZeroVatValue,
                                  ZeroVatValue = det.ZeroVatValue,
                                  MainVat = det.MainVat,

                                  ACC_CODE = acc == null ? "" : acc.ACC_CODE,

                                  Parent_ACC_AR_NAME = acc2 != null ? acc2.ACC_AR_NAME : null
                              };
            return detailsList.ToList();
        }

        public bool Update(Entry_MasterVM entity)
        {
            ENTRY_MASTER et = new ENTRY_MASTER
            {
                ACC_ID = entity.ACC_ID,
                CustAccID = entity.CustAccID,
                EntryVal = entity.EntryVal,

                BILL_ID = entity.BILL_ID,
                CHECK_DATE = entity.CHECK_DATE,
                CHECK_NUMBER = entity.CHECK_NUMBER,
                COLLECT_ENTRY_CODE = entity.COLLECT_ENTRY_CODE,
                CURRENCY_ID = entity.CURRENCY_ID,
                CURRENCY_RATE = entity.CURRENCY_RATE,
                EMP_ID = entity.EMP_ID,
                ENTRY_CREDIT = entity.ENTRY_CREDIT,
                ENTRY_CURRENCY_DIFFERENCE = entity.ENTRY_CURRENCY_DIFFERENCE,
                ENTRY_DATE = entity.ENTRY_DATE,
                ENTRY_DEBIT = entity.ENTRY_DEBIT,
                ENTRY_GOLD_CREDIT = entity.ENTRY_GOLD_CREDIT,
                ENTRY_GOLD_DEBIT = entity.ENTRY_GOLD_DEBIT,
                ENTRY_ID = entity.ENTRY_ID,
                ENTRY_NUMBER = entity.ENTRY_NUMBER,
                ENTRY_REMARKS = entity.ENTRY_REMARKS,
                ENTRY_SETTING_ID = entity.ENTRY_SETTING_ID,
                for_kest = entity.for_kest,
                IS_POSTED = entity.IS_POSTED,
                PAIED = entity.PAIED,
                AddedBy = entity.AddedBy,
                AddedOn = entity.AddedOn,
                UpdatedBy = entity.UpdatedBy,
                updatedOn = entity.updatedOn,
                RelatedEntryID = entity.RelatedEntryID,
                OpeningAccID = entity.OpeningAccID,
                CustID = entity.CustID,
                COM_BRN_ID = entity.COM_BRN_ID,

                TotalCredit = entity.TotalCredit,
                TotalDepit = entity.TotalDepit,
                TotalGoldCredit24 = entity.TotalGoldCredit24,
                TotalGoldDepit24 = entity.TotalGoldDepit24,
                TotalGoldCredit22 = entity.TotalGoldCredit22,
                TotalGoldDepit22 = entity.TotalGoldDepit22,
                TotalGoldCredit21 = entity.TotalGoldCredit21,
                TotalGoldDepit21 = entity.TotalGoldDepit21,
                TotalGoldCredit18 = entity.TotalGoldCredit18,
                TotalGoldDepit18 = entity.TotalGoldDepit18,
                TotalCurrencyCredit = entity.TotalCurrencyCredit,
                TotalCurrenctDepit = entity.TotalCurrenctDepit,
                TotalCurGoldCredit24 = entity.TotalCurGoldCredit24,
                TotalCurGoldDepit24 = entity.TotalCurGoldDepit24,
                TotalCurGoldCredit22 = entity.TotalCurGoldCredit22,
                TotalCurGoldDepit22 = entity.TotalCurGoldDepit22,
                TotalCurGoldCredit21 = entity.TotalCurGoldCredit21,
                TotalCurGoldDepit21 = entity.TotalCurGoldDepit21,
                TotalCurGoldCredit18 = entity.TotalCurGoldCredit18,
                TotalCurGoldDepit18 = entity.TotalCurGoldDepit18,
                TotalTaxValueCredit = entity.TotalTaxValueCredit,
                TotalTaxValueDebit = entity.TotalTaxValueDebit,
                TotalTaxRateCredit = entity.TotalTaxRateCredit,
                TotalTaxRateDebit = entity.TotalTaxRateDebit,
                TotalNotTaxCredit = entity.TotalNotTaxCredit,
                TotalNotTaxDebit = entity.TotalNotTaxDebit,

                Taxable = entity.Taxable,
                TaxRate = entity.TaxRate,
                TaxValue = entity.TaxValue,

                TotalCreditWithTax = entity.TotalCreditWithTax,
                TotalDepitWithTax = entity.TotalDepitWithTax,

                EditReason = entity.EditReason,
                DeliveryPersonName = entity.DeliveryPersonName,

                ExternalNumber = entity.ExternalNumber,

                TotalCreditExemptOfTax = entity.TotalCreditExemptOfTax,
                ExemptCreditOfTaxValue = entity.ExemptCreditOfTaxValue,
                TotalDepitExemptOfTax = entity.TotalDepitExemptOfTax,
                ExemptDepitOfTaxValue = entity.ExemptDepitOfTaxValue,
                TotalCreditTotalMainVat = entity.TotalCreditTotalMainVat,
                MainCreditVatValue = entity.MainCreditVatValue,
                TotalDepitTotalMainVat = entity.TotalDepitTotalMainVat,
                MainDepitVatValue = entity.MainDepitVatValue,
                TotalCreditTotalZeroVat = entity.TotalCreditTotalZeroVat,
                ZeroCreditVatValue = entity.ZeroCreditVatValue,
                TotalDepitTotalZeroVat = entity.TotalDepitTotalZeroVat,
                ZeroDepitVatValue = entity.ZeroDepitVatValue,
                ExemptVatRate = entity.ExemptVatRate,
                MainVatRate = entity.MainVatRate,
                ZeroVatRate = entity.ZeroVatRate,
                TaxNumber = entity.TaxNumber

            };
            entrymasterRepo.Update(et, et.ENTRY_ID);
            return true;
        }

        public Task<bool> UpdateAsync(Entry_MasterVM entity)
        {
            return Task.Run<bool>(() =>
            {
                ENTRY_MASTER et = new ENTRY_MASTER
                {
                    ACC_ID = entity.ACC_ID,
                    CustAccID = entity.CustAccID,
                    EntryVal = entity.EntryVal,

                    BILL_ID = entity.BILL_ID,
                    CHECK_DATE = entity.CHECK_DATE,
                    CHECK_NUMBER = entity.CHECK_NUMBER,
                    COLLECT_ENTRY_CODE = entity.COLLECT_ENTRY_CODE,
                    CURRENCY_ID = entity.CURRENCY_ID,
                    CURRENCY_RATE = entity.CURRENCY_RATE,
                    EMP_ID = entity.EMP_ID,
                    ENTRY_CREDIT = entity.ENTRY_CREDIT,
                    ENTRY_CURRENCY_DIFFERENCE = entity.ENTRY_CURRENCY_DIFFERENCE,
                    ENTRY_DATE = entity.ENTRY_DATE,
                    ENTRY_DEBIT = entity.ENTRY_DEBIT,
                    ENTRY_GOLD_CREDIT = entity.ENTRY_GOLD_CREDIT,
                    ENTRY_GOLD_DEBIT = entity.ENTRY_GOLD_DEBIT,
                    ENTRY_ID = entity.ENTRY_ID,
                    ENTRY_NUMBER = entity.ENTRY_NUMBER,
                    ENTRY_REMARKS = entity.ENTRY_REMARKS,
                    ENTRY_SETTING_ID = entity.ENTRY_SETTING_ID,
                    for_kest = entity.for_kest,
                    IS_POSTED = entity.IS_POSTED,
                    PAIED = entity.PAIED,
                    AddedBy = entity.AddedBy,
                    AddedOn = entity.AddedOn,
                    UpdatedBy = entity.UpdatedBy,
                    updatedOn = entity.updatedOn,
                    RelatedEntryID = entity.RelatedEntryID,
                    OpeningAccID = entity.OpeningAccID,
                    CustID = entity.CustID,
                    COM_BRN_ID = entity.COM_BRN_ID,

                    TotalCredit = entity.TotalCredit,
                    TotalDepit = entity.TotalDepit,
                    TotalGoldCredit24 = entity.TotalGoldCredit24,
                    TotalGoldDepit24 = entity.TotalGoldDepit24,
                    TotalGoldCredit22 = entity.TotalGoldCredit22,
                    TotalGoldDepit22 = entity.TotalGoldDepit22,
                    TotalGoldCredit21 = entity.TotalGoldCredit21,
                    TotalGoldDepit21 = entity.TotalGoldDepit21,
                    TotalGoldCredit18 = entity.TotalGoldCredit18,
                    TotalGoldDepit18 = entity.TotalGoldDepit18,
                    TotalCurrencyCredit = entity.TotalCurrencyCredit,
                    TotalCurrenctDepit = entity.TotalCurrenctDepit,
                    TotalCurGoldCredit24 = entity.TotalCurGoldCredit24,
                    TotalCurGoldDepit24 = entity.TotalCurGoldDepit24,
                    TotalCurGoldCredit22 = entity.TotalCurGoldCredit22,
                    TotalCurGoldDepit22 = entity.TotalCurGoldDepit22,
                    TotalCurGoldCredit21 = entity.TotalCurGoldCredit21,
                    TotalCurGoldDepit21 = entity.TotalCurGoldDepit21,
                    TotalCurGoldCredit18 = entity.TotalCurGoldCredit18,
                    TotalCurGoldDepit18 = entity.TotalCurGoldDepit18,
                    TotalTaxValueCredit = entity.TotalTaxValueCredit,
                    TotalTaxValueDebit = entity.TotalTaxValueDebit,
                    TotalTaxRateCredit = entity.TotalTaxRateCredit,
                    TotalTaxRateDebit = entity.TotalTaxRateDebit,
                    TotalNotTaxCredit = entity.TotalNotTaxCredit,
                    TotalNotTaxDebit = entity.TotalNotTaxDebit,

                    Taxable = entity.Taxable,
                    TaxRate = entity.TaxRate,
                    TaxValue = entity.TaxValue,

                    TotalCreditWithTax = entity.TotalCreditWithTax,
                    TotalDepitWithTax = entity.TotalDepitWithTax,

                    EditReason = entity.EditReason,
                    DeliveryPersonName = entity.DeliveryPersonName,

                    ExternalNumber = entity.ExternalNumber,

                    TotalCreditExemptOfTax = entity.TotalCreditExemptOfTax,
                    ExemptCreditOfTaxValue = entity.ExemptCreditOfTaxValue,
                    TotalDepitExemptOfTax = entity.TotalDepitExemptOfTax,
                    ExemptDepitOfTaxValue = entity.ExemptDepitOfTaxValue,
                    TotalCreditTotalMainVat = entity.TotalCreditTotalMainVat,
                    MainCreditVatValue = entity.MainCreditVatValue,
                    TotalDepitTotalMainVat = entity.TotalDepitTotalMainVat,
                    MainDepitVatValue = entity.MainDepitVatValue,
                    TotalCreditTotalZeroVat = entity.TotalCreditTotalZeroVat,
                    ZeroCreditVatValue = entity.ZeroCreditVatValue,
                    TotalDepitTotalZeroVat = entity.TotalDepitTotalZeroVat,
                    ZeroDepitVatValue = entity.ZeroDepitVatValue,
                    ExemptVatRate = entity.ExemptVatRate,
                    MainVatRate = entity.MainVatRate,
                    ZeroVatRate = entity.ZeroVatRate,
                    TaxNumber = entity.TaxNumber


                };
                entrymasterRepo.Update(et, et.ENTRY_ID);
                return true;
            });
        }



        public Task<long> InsertEntryBill(EntryMasterDetails Obj)
        {
            return Task.Run<long>(() =>
            {
                long masterEntryID;
                //if entryMaster.ID is 0 then Insert else Update 
                if (Obj.EntryMaster.ENTRY_ID == 0)
                {
                    Obj.EntryMaster.ENTRY_NUMBER = (GetLastEntryNumber(3) + 1).ToString();
                    long EntryID = InsertGetLastId(Obj.EntryMaster);
                    masterEntryID = EntryID;
                    if (EntryID > 0)
                    {
                        byte rowNo = 0;
                        foreach (var item in Obj.EntryDetails)
                        {
                            ENTRY_DETAILS Entry_DetailsMapped = new ENTRY_DETAILS();
                            Entry_DetailsMapped.ACC_ID = item.ACC_ID;
                            Entry_DetailsMapped.COST_CENTER_ID = item.COST_CENTER_ID;
                            Entry_DetailsMapped.Disable = item.Disable;
                            Entry_DetailsMapped.ENTRY_CREDIT = item.ENTRY_CREDIT;
                            Entry_DetailsMapped.ENTRY_DEBIT = item.ENTRY_DEBIT;

                            Entry_DetailsMapped.ENTRY_GOLD24_CREDIT = item.ENTRY_GOLD24_CREDIT;
                            Entry_DetailsMapped.ENTRY_GOLD24_DEBIT = item.ENTRY_GOLD24_DEBIT;

                            Entry_DetailsMapped.ENTRY_GOLD22_CREDIT = item.ENTRY_GOLD22_CREDIT;
                            Entry_DetailsMapped.ENTRY_GOLD22_DEBIT = item.ENTRY_GOLD22_DEBIT;

                            Entry_DetailsMapped.ENTRY_GOLD21_CREDIT = item.ENTRY_GOLD21_CREDIT;
                            Entry_DetailsMapped.ENTRY_GOLD21_DEBIT = item.ENTRY_GOLD21_DEBIT;

                            Entry_DetailsMapped.ENTRY_GOLD18_CREDIT = item.ENTRY_GOLD18_CREDIT;
                            Entry_DetailsMapped.ENTRY_GOLD18_DEBIT = item.ENTRY_GOLD18_DEBIT;

                            Entry_DetailsMapped.ENTRY_DETAILS_REMARKS = item.ENTRY_DETAILS_REMARKS;
                            Entry_DetailsMapped.ENTRY_ID = EntryID;
                            Entry_DetailsMapped.TaxValue = item.TaxValue;
                            Entry_DetailsMapped.Taxable = item.Taxable;
                            Entry_DetailsMapped.TaxRate = item.TaxRate;

                            Entry_DetailsMapped.CheckNumber = item.CheckNumber;
                            Entry_DetailsMapped.CheckDate = item.CheckDate;
                            Entry_DetailsMapped.CheckIssueDate = item.CheckIssueDate;
                            Entry_DetailsMapped.IsExemptOfTax = item.IsExemptOfTax;


                            Entry_DetailsMapped.ExemptOfTaxValue = item.ExemptOfTaxValue;
                            Entry_DetailsMapped.IsMainVatValue = item.IsMainVatValue;
                            Entry_DetailsMapped.MainVatValue = item.MainVatValue;
                            Entry_DetailsMapped.IsZeroVatValue = item.IsZeroVatValue;
                            Entry_DetailsMapped.ZeroVatValue = item.ZeroVatValue;
                            Entry_DetailsMapped.MainVat = item.MainVat;
                            //  Entry_DetailsMapped.TaxAccountID = item.TaxAccountID;
                            //Entry_DetailsMapped.ENTRY_ROW_NUMBER = item.ENTRY_ROW_NUMBER;
                            Entry_DetailsMapped.ENTRY_ROW_NUMBER = rowNo;
                            _entryDetailsRepo.Add(Entry_DetailsMapped);
                            rowNo++;
                        }
                    }
                }
                else
                {
                    masterEntryID = Obj.EntryMaster.ENTRY_ID;
                    //Update
                    bool UpdateStatus = Update(Obj.EntryMaster);
                    if (UpdateStatus)
                    {
                        //Getall
                        List<ENTRY_DETAILS> ExistingDetails = GetAllDetails(Obj.EntryMaster.ENTRY_ID);
                        if (ExistingDetails.Count > 0)
                        {
                            //Delete ALl
                            foreach (var item in ExistingDetails)
                            {
                                //object [] keys = { item, item.ENTRY_ID, item, item.ENTRY_ROW_NUMBER };
                                object[] keys = new object[2] { item.ENTRY_ID, item.ENTRY_ROW_NUMBER };
                                try
                                {
                                    _entryDetailsRepo.DeleteComposite(item, keys);
                                }
                                catch (Exception ex)
                                {


                                }

                            }
                        }
                        //Insert Again
                        byte rowNo = 0;
                        foreach (var item in Obj.EntryDetails)
                        {
                            ENTRY_DETAILS Entry_DetailsMapped = new ENTRY_DETAILS();
                            Entry_DetailsMapped.ACC_ID = item.ACC_ID;
                            Entry_DetailsMapped.COST_CENTER_ID = item.COST_CENTER_ID;
                            Entry_DetailsMapped.Disable = item.Disable;
                            Entry_DetailsMapped.ENTRY_CREDIT = item.ENTRY_CREDIT;
                            Entry_DetailsMapped.ENTRY_DEBIT = item.ENTRY_DEBIT;
                            Entry_DetailsMapped.ENTRY_GOLD24_CREDIT = item.ENTRY_GOLD24_CREDIT;
                            Entry_DetailsMapped.ENTRY_GOLD24_DEBIT = item.ENTRY_GOLD24_DEBIT;

                            Entry_DetailsMapped.ENTRY_GOLD22_CREDIT = item.ENTRY_GOLD22_CREDIT;
                            Entry_DetailsMapped.ENTRY_GOLD22_DEBIT = item.ENTRY_GOLD22_DEBIT;

                            Entry_DetailsMapped.ENTRY_GOLD21_CREDIT = item.ENTRY_GOLD21_CREDIT;
                            Entry_DetailsMapped.ENTRY_GOLD21_DEBIT = item.ENTRY_GOLD21_DEBIT;

                            Entry_DetailsMapped.ENTRY_GOLD18_CREDIT = item.ENTRY_GOLD18_CREDIT;
                            Entry_DetailsMapped.ENTRY_GOLD18_DEBIT = item.ENTRY_GOLD18_DEBIT;
                            Entry_DetailsMapped.ENTRY_DETAILS_REMARKS = item.ENTRY_DETAILS_REMARKS;
                            Entry_DetailsMapped.ENTRY_ID = Obj.EntryMaster.ENTRY_ID;
                            Entry_DetailsMapped.TaxValue = item.TaxValue;
                            Entry_DetailsMapped.Taxable = item.Taxable;
                            Entry_DetailsMapped.TaxRate = item.TaxRate;
                            //Entry_DetailsMapped.ENTRY_ROW_NUMBER = item.ENTRY_ROW_NUMBER;
                            Entry_DetailsMapped.ENTRY_ROW_NUMBER = rowNo;

                            Entry_DetailsMapped.CheckNumber = item.CheckNumber;
                            Entry_DetailsMapped.CheckDate = item.CheckDate;
                            Entry_DetailsMapped.CheckIssueDate = item.CheckIssueDate;
                            Entry_DetailsMapped.IsExemptOfTax = item.IsExemptOfTax;


                            Entry_DetailsMapped.ExemptOfTaxValue = item.ExemptOfTaxValue;
                            Entry_DetailsMapped.IsMainVatValue = item.IsMainVatValue;
                            Entry_DetailsMapped.MainVatValue = item.MainVatValue;
                            Entry_DetailsMapped.IsZeroVatValue = item.IsZeroVatValue;
                            Entry_DetailsMapped.ZeroVatValue = item.ZeroVatValue;
                            Entry_DetailsMapped.MainVat = item.MainVat;
                            _entryDetailsRepo.Add(Entry_DetailsMapped);
                            rowNo++;
                        }
                    }
                }
                return masterEntryID;
            });
        }

        public Task<long> InsertEntryAsset(EntryMasterDetails Obj)
        {
            return Task.Run<long>(() =>
            {
                long masterEntryID;
                //if entryMaster.ID is 0 then Insert else Update 
                if (Obj.EntryMaster.AssetOperationMasterID == 0)
                {
                    Obj.EntryMaster.ENTRY_NUMBER = (GetLastEntryNumber(3) + 1).ToString();
                    long EntryID = InsertGetLastId(Obj.EntryMaster);
                    masterEntryID = EntryID;
                    if (EntryID > 0)
                    {
                        byte rowNo = 0;
                        foreach (var item in Obj.EntryDetails)
                        {
                            ENTRY_DETAILS Entry_DetailsMapped = new ENTRY_DETAILS();
                            Entry_DetailsMapped.ACC_ID = item.ACC_ID;
                            Entry_DetailsMapped.COST_CENTER_ID = item.COST_CENTER_ID;
                            Entry_DetailsMapped.Disable = item.Disable;
                            Entry_DetailsMapped.ENTRY_CREDIT = item.ENTRY_CREDIT;
                            Entry_DetailsMapped.ENTRY_DEBIT = item.ENTRY_DEBIT;

                            Entry_DetailsMapped.ENTRY_GOLD24_CREDIT = item.ENTRY_GOLD24_CREDIT;
                            Entry_DetailsMapped.ENTRY_GOLD24_DEBIT = item.ENTRY_GOLD24_DEBIT;

                            Entry_DetailsMapped.ENTRY_GOLD22_CREDIT = item.ENTRY_GOLD22_CREDIT;
                            Entry_DetailsMapped.ENTRY_GOLD22_DEBIT = item.ENTRY_GOLD22_DEBIT;

                            Entry_DetailsMapped.ENTRY_GOLD21_CREDIT = item.ENTRY_GOLD21_CREDIT;
                            Entry_DetailsMapped.ENTRY_GOLD21_DEBIT = item.ENTRY_GOLD21_DEBIT;

                            Entry_DetailsMapped.ENTRY_GOLD18_CREDIT = item.ENTRY_GOLD18_CREDIT;
                            Entry_DetailsMapped.ENTRY_GOLD18_DEBIT = item.ENTRY_GOLD18_DEBIT;

                            Entry_DetailsMapped.ENTRY_DETAILS_REMARKS = item.ENTRY_DETAILS_REMARKS;
                            Entry_DetailsMapped.ENTRY_ID = EntryID;
                            Entry_DetailsMapped.TaxValue = item.TaxValue;
                            Entry_DetailsMapped.Taxable = item.Taxable;
                            Entry_DetailsMapped.TaxRate = item.TaxRate;

                            Entry_DetailsMapped.CheckNumber = item.CheckNumber;
                            Entry_DetailsMapped.CheckDate = item.CheckDate;
                            Entry_DetailsMapped.CheckIssueDate = item.CheckIssueDate;
                            Entry_DetailsMapped.IsExemptOfTax = item.IsExemptOfTax;


                            Entry_DetailsMapped.ExemptOfTaxValue = item.ExemptOfTaxValue;
                            Entry_DetailsMapped.IsMainVatValue = item.IsMainVatValue;
                            Entry_DetailsMapped.MainVatValue = item.MainVatValue;
                            Entry_DetailsMapped.IsZeroVatValue = item.IsZeroVatValue;
                            Entry_DetailsMapped.ZeroVatValue = item.ZeroVatValue;
                            Entry_DetailsMapped.MainVat = item.MainVat;
                            //  Entry_DetailsMapped.TaxAccountID = item.TaxAccountID;
                            //Entry_DetailsMapped.ENTRY_ROW_NUMBER = item.ENTRY_ROW_NUMBER;
                            Entry_DetailsMapped.ENTRY_ROW_NUMBER = rowNo;
                            _entryDetailsRepo.Add(Entry_DetailsMapped);
                            rowNo++;
                        }
                    }
                }
                else
                {
                    var entryMaster = entrymasterRepo.Filter(p => p.AssetOperationMasterID == Obj.EntryMaster.AssetOperationMasterID).FirstOrDefault();
                    masterEntryID = entryMaster == null ? 0 : entryMaster.ENTRY_ID;
                    //Update
                    if (entryMaster == null)
                    {
                        masterEntryID = InsertGetLastId(Obj.EntryMaster);
                    }
                    else
                    {
                        entryMaster.ENTRY_DEBIT = Obj.EntryMaster.ENTRY_DEBIT;
                        entryMaster.ENTRY_CREDIT = Obj.EntryMaster.ENTRY_CREDIT;
                        entryMaster.ACC_ID = Obj.EntryMaster.ACC_ID;
                        //AutoMapper.Mapper.Map(entryMaster,ENTRY_MASTER,EntryMasterDetails)
                        entrymasterRepo.Update(entryMaster, entryMaster.ENTRY_ID);
                        //if (UpdateStatus)
                        //{
                            //Getall
                            List<ENTRY_DETAILS> ExistingDetails = GetAllDetails(masterEntryID);
                            if (ExistingDetails.Count > 0)
                            {
                                //Delete ALl
                                foreach (var item in ExistingDetails)
                                {
                                    //object [] keys = { item, item.ENTRY_ID, item, item.ENTRY_ROW_NUMBER };
                                    object[] keys = new object[2] { item.ENTRY_ID, item.ENTRY_ROW_NUMBER };
                                    try
                                    {
                                        _entryDetailsRepo.DeleteComposite(item, keys);
                                    }
                                    catch (Exception ex)
                                    {


                                    }

                                }
                            }
                            //Insert Again
                            byte rowNo = 0;
                            foreach (var item in Obj.EntryDetails)
                            {
                                ENTRY_DETAILS Entry_DetailsMapped = new ENTRY_DETAILS();
                                Entry_DetailsMapped.ACC_ID = item.ACC_ID;
                                Entry_DetailsMapped.COST_CENTER_ID = item.COST_CENTER_ID;
                                Entry_DetailsMapped.Disable = item.Disable;
                                Entry_DetailsMapped.ENTRY_CREDIT = item.ENTRY_CREDIT;
                                Entry_DetailsMapped.ENTRY_DEBIT = item.ENTRY_DEBIT;
                                Entry_DetailsMapped.ENTRY_GOLD24_CREDIT = item.ENTRY_GOLD24_CREDIT;
                                Entry_DetailsMapped.ENTRY_GOLD24_DEBIT = item.ENTRY_GOLD24_DEBIT;

                                Entry_DetailsMapped.ENTRY_GOLD22_CREDIT = item.ENTRY_GOLD22_CREDIT;
                                Entry_DetailsMapped.ENTRY_GOLD22_DEBIT = item.ENTRY_GOLD22_DEBIT;

                                Entry_DetailsMapped.ENTRY_GOLD21_CREDIT = item.ENTRY_GOLD21_CREDIT;
                                Entry_DetailsMapped.ENTRY_GOLD21_DEBIT = item.ENTRY_GOLD21_DEBIT;

                                Entry_DetailsMapped.ENTRY_GOLD18_CREDIT = item.ENTRY_GOLD18_CREDIT;
                                Entry_DetailsMapped.ENTRY_GOLD18_DEBIT = item.ENTRY_GOLD18_DEBIT;
                                Entry_DetailsMapped.ENTRY_DETAILS_REMARKS = item.ENTRY_DETAILS_REMARKS;
                                Entry_DetailsMapped.ENTRY_ID = masterEntryID;
                                Entry_DetailsMapped.TaxValue = item.TaxValue;
                                Entry_DetailsMapped.Taxable = item.Taxable;
                                Entry_DetailsMapped.TaxRate = item.TaxRate;
                                //Entry_DetailsMapped.ENTRY_ROW_NUMBER = item.ENTRY_ROW_NUMBER;
                                Entry_DetailsMapped.ENTRY_ROW_NUMBER = rowNo;

                                Entry_DetailsMapped.CheckNumber = item.CheckNumber;
                                Entry_DetailsMapped.CheckDate = item.CheckDate;
                                Entry_DetailsMapped.CheckIssueDate = item.CheckIssueDate;
                                Entry_DetailsMapped.IsExemptOfTax = item.IsExemptOfTax;


                                Entry_DetailsMapped.ExemptOfTaxValue = item.ExemptOfTaxValue;
                                Entry_DetailsMapped.IsMainVatValue = item.IsMainVatValue;
                                Entry_DetailsMapped.MainVatValue = item.MainVatValue;
                                Entry_DetailsMapped.IsZeroVatValue = item.IsZeroVatValue;
                                Entry_DetailsMapped.ZeroVatValue = item.ZeroVatValue;
                                Entry_DetailsMapped.MainVat = item.MainVat;
                                _entryDetailsRepo.Add(Entry_DetailsMapped);
                                rowNo++;
                            }
                        //}
                    }
                }
                return masterEntryID;
            });
        }

        public List<Entry_DetailsVM> GetDetailsByMasterID(long EntryMasterID)
        {
            var q = from entity in _entryDetailsRepo.Filter(b => b.ENTRY_ID == EntryMasterID)
                    select new Entry_DetailsVM
                    {
                        ENTRY_ROW_NUMBER = entity.ENTRY_ROW_NUMBER,
                        ENTRY_ID = entity.ENTRY_ID,
                        ACC_ID = entity.ACC_ID,
                        COST_CENTER_ID = entity.COST_CENTER_ID,
                        ENTRY_CREDIT = entity.ENTRY_CREDIT,
                        ENTRY_DEBIT = entity.ENTRY_DEBIT,
                        ENTRY_GOLD24_CREDIT = entity.ENTRY_GOLD24_CREDIT,
                        ENTRY_GOLD24_DEBIT = entity.ENTRY_GOLD24_DEBIT,

                        ENTRY_GOLD22_CREDIT = entity.ENTRY_GOLD22_CREDIT,
                        ENTRY_GOLD22_DEBIT = entity.ENTRY_GOLD22_DEBIT,

                        ENTRY_GOLD21_CREDIT = entity.ENTRY_GOLD21_CREDIT,
                        ENTRY_GOLD21_DEBIT = entity.ENTRY_GOLD21_DEBIT,

                        ENTRY_GOLD18_CREDIT = entity.ENTRY_GOLD18_CREDIT,
                        ENTRY_GOLD18_DEBIT = entity.ENTRY_GOLD18_DEBIT,

                        ENTRY_DETAILS_REMARKS = entity.ENTRY_DETAILS_REMARKS,
                        AddedBy = entity.AddedBy,
                        AddedOn = entity.AddedOn,
                        UpdatedBy = entity.UpdatedBy,
                        UpdatedOn = entity.UpdatedOn,
                        Disable = entity.Disable,
                        Taxable = entity.Taxable,
                        TaxValue = entity.TaxValue,
                        TaxRate = entity.TaxRate,

                        CheckNumber = entity.CheckNumber,
                        CheckDate = entity.CheckDate,
                        CheckIssueDate = entity.CheckIssueDate,
                        IsExemptOfTax = entity.IsExemptOfTax,

                        ExemptOfTaxValue = entity.ExemptOfTaxValue,
                        IsMainVatValue = entity.IsMainVatValue,
                        MainVatValue = entity.MainVatValue,
                        IsZeroVatValue = entity.IsZeroVatValue,
                        ZeroVatValue = entity.ZeroVatValue,
                        MainVat = entity.MainVat,
        };
            return q.ToList();
        }



        public Task<EntryMasterDetails> getEntryByEntryNumber(int entryNumber, int entryType)
        {
            return Task.Run<EntryMasterDetails>(() =>
            {
                var entryMasterNumber = entryNumber.ToString();
                var q = from entity in entrymasterRepo.Filter(e => e.ENTRY_NUMBER == entryMasterNumber && e.ENTRY_SETTING_ID == entryType)
                        select new Entry_MasterVM
                        {
                            ACC_ID = entity.ACC_ID,
                            CustAccID = entity.CustAccID,
                            EntryVal = entity.EntryVal,

                            BILL_ID = entity.BILL_ID,
                            CHECK_DATE = entity.CHECK_DATE,
                            CHECK_NUMBER = entity.CHECK_NUMBER,
                            COLLECT_ENTRY_CODE = entity.COLLECT_ENTRY_CODE,
                            CURRENCY_ID = entity.CURRENCY_ID,
                            CURRENCY_RATE = entity.CURRENCY_RATE,
                            EMP_ID = entity.EMP_ID,
                            ENTRY_CREDIT = entity.ENTRY_CREDIT,
                            ENTRY_CURRENCY_DIFFERENCE = entity.ENTRY_CURRENCY_DIFFERENCE,
                            ENTRY_DATE = entity.ENTRY_DATE,
                            ENTRY_DEBIT = entity.ENTRY_DEBIT,
                            ENTRY_GOLD_CREDIT = entity.ENTRY_GOLD_CREDIT,
                            ENTRY_GOLD_DEBIT = entity.ENTRY_GOLD_DEBIT,
                            ENTRY_ID = entity.ENTRY_ID,
                            ENTRY_NUMBER = entity.ENTRY_NUMBER,
                            ENTRY_REMARKS = entity.ENTRY_REMARKS,
                            ENTRY_SETTING_ID = entity.ENTRY_SETTING_ID,
                            for_kest = entity.for_kest,
                            IS_POSTED = entity.IS_POSTED,
                            PAIED = entity.PAIED,
                            AddedBy = entity.AddedBy,
                            AddedOn = entity.AddedOn,
                            UpdatedBy = entity.UpdatedBy,
                            updatedOn = entity.updatedOn,
                            RelatedEntryID = entity.RelatedEntryID,
                            OpeningAccID = entity.OpeningAccID,
                            CustID = entity.CustID,
                            COM_BRN_ID = entity.COM_BRN_ID,

                            TotalCredit = entity.TotalCredit,
                            TotalDepit = entity.TotalDepit,
                            TotalGoldCredit24 = entity.TotalGoldCredit24,
                            TotalGoldDepit24 = entity.TotalGoldDepit24,
                            TotalGoldCredit22 = entity.TotalGoldCredit22,
                            TotalGoldDepit22 = entity.TotalGoldDepit22,
                            TotalGoldCredit21 = entity.TotalGoldCredit21,
                            TotalGoldDepit21 = entity.TotalGoldDepit21,
                            TotalGoldCredit18 = entity.TotalGoldCredit18,
                            TotalGoldDepit18 = entity.TotalGoldDepit18,
                            TotalCurrencyCredit = entity.TotalCurrencyCredit,
                            TotalCurrenctDepit = entity.TotalCurrenctDepit,
                            TotalCurGoldCredit24 = entity.TotalCurGoldCredit24,
                            TotalCurGoldDepit24 = entity.TotalCurGoldDepit24,
                            TotalCurGoldCredit22 = entity.TotalCurGoldCredit22,
                            TotalCurGoldDepit22 = entity.TotalCurGoldDepit22,
                            TotalCurGoldCredit21 = entity.TotalCurGoldCredit21,
                            TotalCurGoldDepit21 = entity.TotalCurGoldDepit21,
                            TotalCurGoldCredit18 = entity.TotalCurGoldCredit18,
                            TotalCurGoldDepit18 = entity.TotalCurGoldDepit18,
                            TotalTaxValueCredit = entity.TotalTaxValueCredit,
                            TotalTaxValueDebit = entity.TotalTaxValueDebit,
                            TotalTaxRateCredit = entity.TotalTaxRateCredit,
                            TotalTaxRateDebit = entity.TotalTaxRateDebit,
                            TotalNotTaxCredit = entity.TotalNotTaxCredit,
                            TotalNotTaxDebit = entity.TotalNotTaxDebit,

                            Taxable = entity.Taxable,
                            TaxRate = entity.TaxRate,
                            TaxValue = entity.TaxValue,

                            TotalCreditWithTax = entity.TotalCreditWithTax,
                            TotalDepitWithTax = entity.TotalDepitWithTax,

                            EditReason = entity.EditReason,
                            DeliveryPersonName = entity.DeliveryPersonName,

                            TotalCreditExemptOfTax = entity.TotalCreditExemptOfTax,
                            ExemptCreditOfTaxValue = entity.ExemptCreditOfTaxValue,
                            TotalDepitExemptOfTax = entity.TotalDepitExemptOfTax,
                            ExemptDepitOfTaxValue = entity.ExemptDepitOfTaxValue,
                            TotalCreditTotalMainVat = entity.TotalCreditTotalMainVat,
                            MainCreditVatValue = entity.MainCreditVatValue,
                            TotalDepitTotalMainVat = entity.TotalDepitTotalMainVat,
                            MainDepitVatValue = entity.MainDepitVatValue,
                            TotalCreditTotalZeroVat = entity.TotalCreditTotalZeroVat,
                            ZeroCreditVatValue = entity.ZeroCreditVatValue,
                            TotalDepitTotalZeroVat = entity.TotalDepitTotalZeroVat,
                            ZeroDepitVatValue = entity.ZeroDepitVatValue,
                            ExemptVatRate = entity.ExemptVatRate,
                            MainVatRate = entity.MainVatRate,
                            ZeroVatRate = entity.ZeroVatRate

                        };
                var entryMaster = q.FirstOrDefault();
                List<Entry_DetailsVM> entryDetails = GetDetailsByMasterID(entryMaster.ENTRY_ID);

                EntryMasterDetails masterDetails = new EntryMasterDetails();
                masterDetails.EntryMaster = entryMaster;
                masterDetails.EntryDetails = entryDetails;
                return masterDetails;
            });
        }


        ////////////////////////// bill entry////////////////////////////////////////////////////////////////
        public EntryMasterDetails GetEntryByBillID(long billID)
        {

            var q = from entity in entrymasterRepo.Filter(e => e.BILL_ID == billID)
                    select new Entry_MasterVM
                    {
                        ACC_ID = entity.ACC_ID,
                        CustAccID = entity.CustAccID,
                        EntryVal = entity.EntryVal,

                        BILL_ID = entity.BILL_ID,
                        CHECK_DATE = entity.CHECK_DATE,
                        CHECK_NUMBER = entity.CHECK_NUMBER,
                        COLLECT_ENTRY_CODE = entity.COLLECT_ENTRY_CODE,
                        CURRENCY_ID = entity.CURRENCY_ID,
                        CURRENCY_RATE = entity.CURRENCY_RATE,
                        EMP_ID = entity.EMP_ID,
                        ENTRY_CREDIT = entity.ENTRY_CREDIT,
                        ENTRY_CURRENCY_DIFFERENCE = entity.ENTRY_CURRENCY_DIFFERENCE,
                        ENTRY_DATE = entity.ENTRY_DATE,
                        ENTRY_DEBIT = entity.ENTRY_DEBIT,
                        ENTRY_GOLD_CREDIT = entity.ENTRY_GOLD_CREDIT,
                        ENTRY_GOLD_DEBIT = entity.ENTRY_GOLD_DEBIT,
                        ENTRY_ID = entity.ENTRY_ID,
                        ENTRY_NUMBER = entity.ENTRY_NUMBER,
                        ENTRY_REMARKS = entity.ENTRY_REMARKS,
                        ENTRY_SETTING_ID = entity.ENTRY_SETTING_ID,
                        for_kest = entity.for_kest,
                        IS_POSTED = entity.IS_POSTED,
                        PAIED = entity.PAIED,
                        AddedBy = entity.AddedBy,
                        AddedOn = entity.AddedOn,
                        UpdatedBy = entity.UpdatedBy,
                        updatedOn = entity.updatedOn,
                        RelatedEntryID = entity.RelatedEntryID,
                        OpeningAccID = entity.OpeningAccID,
                        CustID = entity.CustID,
                        COM_BRN_ID = entity.COM_BRN_ID,

                        TotalCredit = entity.TotalCredit,
                        TotalDepit = entity.TotalDepit,
                        TotalGoldCredit24 = entity.TotalGoldCredit24,
                        TotalGoldDepit24 = entity.TotalGoldDepit24,
                        TotalGoldCredit22 = entity.TotalGoldCredit22,
                        TotalGoldDepit22 = entity.TotalGoldDepit22,
                        TotalGoldCredit21 = entity.TotalGoldCredit21,
                        TotalGoldDepit21 = entity.TotalGoldDepit21,
                        TotalGoldCredit18 = entity.TotalGoldCredit18,
                        TotalGoldDepit18 = entity.TotalGoldDepit18,
                        TotalCurrencyCredit = entity.TotalCurrencyCredit,
                        TotalCurrenctDepit = entity.TotalCurrenctDepit,
                        TotalCurGoldCredit24 = entity.TotalCurGoldCredit24,
                        TotalCurGoldDepit24 = entity.TotalCurGoldDepit24,
                        TotalCurGoldCredit22 = entity.TotalCurGoldCredit22,
                        TotalCurGoldDepit22 = entity.TotalCurGoldDepit22,
                        TotalCurGoldCredit21 = entity.TotalCurGoldCredit21,
                        TotalCurGoldDepit21 = entity.TotalCurGoldDepit21,
                        TotalCurGoldCredit18 = entity.TotalCurGoldCredit18,
                        TotalCurGoldDepit18 = entity.TotalCurGoldDepit18,
                        TotalTaxValueCredit = entity.TotalTaxValueCredit,
                        TotalTaxValueDebit = entity.TotalTaxValueDebit,
                        TotalTaxRateCredit = entity.TotalTaxRateCredit,
                        TotalTaxRateDebit = entity.TotalTaxRateDebit,
                        TotalNotTaxCredit = entity.TotalNotTaxCredit,
                        TotalNotTaxDebit = entity.TotalNotTaxDebit,

                        Taxable = entity.Taxable,
                        TaxRate = entity.TaxRate,
                        TaxValue = entity.TaxValue,

                        TotalCreditWithTax = entity.TotalCreditWithTax,
                        TotalDepitWithTax = entity.TotalDepitWithTax,

                        EditReason = entity.EditReason,
                        DeliveryPersonName = entity.DeliveryPersonName,

                        ExternalNumber = entity.ExternalNumber,

                        TotalCreditExemptOfTax = entity.TotalCreditExemptOfTax,
                        ExemptCreditOfTaxValue = entity.ExemptCreditOfTaxValue,
                        TotalDepitExemptOfTax = entity.TotalDepitExemptOfTax,
                        ExemptDepitOfTaxValue = entity.ExemptDepitOfTaxValue,
                        TotalCreditTotalMainVat = entity.TotalCreditTotalMainVat,
                        MainCreditVatValue = entity.MainCreditVatValue,
                        TotalDepitTotalMainVat = entity.TotalDepitTotalMainVat,
                        MainDepitVatValue = entity.MainDepitVatValue,
                        TotalCreditTotalZeroVat = entity.TotalCreditTotalZeroVat,
                        ZeroCreditVatValue = entity.ZeroCreditVatValue,
                        TotalDepitTotalZeroVat = entity.TotalDepitTotalZeroVat,
                        ZeroDepitVatValue = entity.ZeroDepitVatValue,
                        ExemptVatRate = entity.ExemptVatRate,
                        MainVatRate = entity.MainVatRate,
                        ZeroVatRate = entity.ZeroVatRate

                    };
            var entryMaster = q.FirstOrDefault();
            List<Entry_DetailsVM> entryDetails = GetDetailsByMasterID(entryMaster.ENTRY_ID);

            EntryMasterDetails masterDetails = new EntryMasterDetails();
            masterDetails.EntryMaster = entryMaster;
            masterDetails.EntryDetails = entryDetails;
            return masterDetails;
        }



        public Entry_MasterVM GetEntryMasterByBill(long billID)
        {
            var q = from entity in entrymasterRepo.Filter(e => e.BILL_ID == billID)
                    select new Entry_MasterVM
                    {
                        ACC_ID = entity.ACC_ID,
                        CustAccID = entity.CustAccID,
                        EntryVal = entity.EntryVal,

                        BILL_ID = entity.BILL_ID,
                        CHECK_DATE = entity.CHECK_DATE,
                        CHECK_NUMBER = entity.CHECK_NUMBER,
                        COLLECT_ENTRY_CODE = entity.COLLECT_ENTRY_CODE,
                        CURRENCY_ID = entity.CURRENCY_ID,
                        CURRENCY_RATE = entity.CURRENCY_RATE,
                        EMP_ID = entity.EMP_ID,
                        ENTRY_CREDIT = entity.ENTRY_CREDIT,
                        ENTRY_CURRENCY_DIFFERENCE = entity.ENTRY_CURRENCY_DIFFERENCE,
                        ENTRY_DATE = entity.ENTRY_DATE,
                        ENTRY_DEBIT = entity.ENTRY_DEBIT,
                        ENTRY_GOLD_CREDIT = entity.ENTRY_GOLD_CREDIT,
                        ENTRY_GOLD_DEBIT = entity.ENTRY_GOLD_DEBIT,
                        ENTRY_ID = entity.ENTRY_ID,
                        ENTRY_NUMBER = entity.ENTRY_NUMBER,
                        ENTRY_REMARKS = entity.ENTRY_REMARKS,
                        ENTRY_SETTING_ID = entity.ENTRY_SETTING_ID,
                        for_kest = entity.for_kest,
                        IS_POSTED = entity.IS_POSTED,
                        PAIED = entity.PAIED,
                        AddedBy = entity.AddedBy,
                        AddedOn = entity.AddedOn,
                        UpdatedBy = entity.UpdatedBy,
                        updatedOn = entity.updatedOn,
                        RelatedEntryID = entity.RelatedEntryID,
                        OpeningAccID = entity.OpeningAccID,
                        CustID = entity.CustID,
                        COM_BRN_ID = entity.COM_BRN_ID,

                        TotalCredit = entity.TotalCredit,
                        TotalDepit = entity.TotalDepit,
                        TotalGoldCredit24 = entity.TotalGoldCredit24,
                        TotalGoldDepit24 = entity.TotalGoldDepit24,
                        TotalGoldCredit22 = entity.TotalGoldCredit22,
                        TotalGoldDepit22 = entity.TotalGoldDepit22,
                        TotalGoldCredit21 = entity.TotalGoldCredit21,
                        TotalGoldDepit21 = entity.TotalGoldDepit21,
                        TotalGoldCredit18 = entity.TotalGoldCredit18,
                        TotalGoldDepit18 = entity.TotalGoldDepit18,
                        TotalCurrencyCredit = entity.TotalCurrencyCredit,
                        TotalCurrenctDepit = entity.TotalCurrenctDepit,
                        TotalCurGoldCredit24 = entity.TotalCurGoldCredit24,
                        TotalCurGoldDepit24 = entity.TotalCurGoldDepit24,
                        TotalCurGoldCredit22 = entity.TotalCurGoldCredit22,
                        TotalCurGoldDepit22 = entity.TotalCurGoldDepit22,
                        TotalCurGoldCredit21 = entity.TotalCurGoldCredit21,
                        TotalCurGoldDepit21 = entity.TotalCurGoldDepit21,
                        TotalCurGoldCredit18 = entity.TotalCurGoldCredit18,
                        TotalCurGoldDepit18 = entity.TotalCurGoldDepit18,
                        TotalTaxValueCredit = entity.TotalTaxValueCredit,
                        TotalTaxValueDebit = entity.TotalTaxValueDebit,
                        TotalTaxRateCredit = entity.TotalTaxRateCredit,
                        TotalTaxRateDebit = entity.TotalTaxRateDebit,
                        TotalNotTaxCredit = entity.TotalNotTaxCredit,
                        TotalNotTaxDebit = entity.TotalNotTaxDebit,

                        Taxable = entity.Taxable,
                        TaxRate = entity.TaxRate,
                        TaxValue = entity.TaxValue,

                        TotalCreditWithTax = entity.TotalCreditWithTax,
                        TotalDepitWithTax = entity.TotalDepitWithTax,

                        EditReason = entity.EditReason,
                        DeliveryPersonName = entity.DeliveryPersonName,

                        ExternalNumber = entity.ExternalNumber,

                        TotalCreditExemptOfTax = entity.TotalCreditExemptOfTax,
                        ExemptCreditOfTaxValue = entity.ExemptCreditOfTaxValue,
                        TotalDepitExemptOfTax = entity.TotalDepitExemptOfTax,
                        ExemptDepitOfTaxValue = entity.ExemptDepitOfTaxValue,
                        TotalCreditTotalMainVat = entity.TotalCreditTotalMainVat,
                        MainCreditVatValue = entity.MainCreditVatValue,
                        TotalDepitTotalMainVat = entity.TotalDepitTotalMainVat,
                        MainDepitVatValue = entity.MainDepitVatValue,
                        TotalCreditTotalZeroVat = entity.TotalCreditTotalZeroVat,
                        ZeroCreditVatValue = entity.ZeroCreditVatValue,
                        TotalDepitTotalZeroVat = entity.TotalDepitTotalZeroVat,
                        ZeroDepitVatValue = entity.ZeroDepitVatValue,
                        ExemptVatRate = entity.ExemptVatRate,
                        MainVatRate = entity.MainVatRate,
                        ZeroVatRate = entity.ZeroVatRate,
                        TaxNumber = entity.TaxNumber
                    };
            return q.FirstOrDefault();
        }
        
        public Entry_MasterVM getEntryMasterByAssetOperation(long assetOperationId)
        {
            var q = from entity in entrymasterRepo.Filter(e => e.AssetOperationMasterID == assetOperationId)
                    select new Entry_MasterVM
                    {
                        ACC_ID = entity.ACC_ID,
                        CustAccID = entity.CustAccID,
                        EntryVal = entity.EntryVal,

                        BILL_ID = entity.BILL_ID,
                        CHECK_DATE = entity.CHECK_DATE,
                        CHECK_NUMBER = entity.CHECK_NUMBER,
                        COLLECT_ENTRY_CODE = entity.COLLECT_ENTRY_CODE,
                        CURRENCY_ID = entity.CURRENCY_ID,
                        CURRENCY_RATE = entity.CURRENCY_RATE,
                        EMP_ID = entity.EMP_ID,
                        ENTRY_CREDIT = entity.ENTRY_CREDIT,
                        ENTRY_CURRENCY_DIFFERENCE = entity.ENTRY_CURRENCY_DIFFERENCE,
                        ENTRY_DATE = entity.ENTRY_DATE,
                        ENTRY_DEBIT = entity.ENTRY_DEBIT,
                        ENTRY_GOLD_CREDIT = entity.ENTRY_GOLD_CREDIT,
                        ENTRY_GOLD_DEBIT = entity.ENTRY_GOLD_DEBIT,
                        ENTRY_ID = entity.ENTRY_ID,
                        ENTRY_NUMBER = entity.ENTRY_NUMBER,
                        ENTRY_REMARKS = entity.ENTRY_REMARKS,
                        ENTRY_SETTING_ID = entity.ENTRY_SETTING_ID,
                        for_kest = entity.for_kest,
                        IS_POSTED = entity.IS_POSTED,
                        PAIED = entity.PAIED,
                        AddedBy = entity.AddedBy,
                        AddedOn = entity.AddedOn,
                        UpdatedBy = entity.UpdatedBy,
                        updatedOn = entity.updatedOn,
                        RelatedEntryID = entity.RelatedEntryID,
                        OpeningAccID = entity.OpeningAccID,
                        CustID = entity.CustID,
                        COM_BRN_ID = entity.COM_BRN_ID,

                        TotalCredit = entity.TotalCredit,
                        TotalDepit = entity.TotalDepit,
                        TotalGoldCredit24 = entity.TotalGoldCredit24,
                        TotalGoldDepit24 = entity.TotalGoldDepit24,
                        TotalGoldCredit22 = entity.TotalGoldCredit22,
                        TotalGoldDepit22 = entity.TotalGoldDepit22,
                        TotalGoldCredit21 = entity.TotalGoldCredit21,
                        TotalGoldDepit21 = entity.TotalGoldDepit21,
                        TotalGoldCredit18 = entity.TotalGoldCredit18,
                        TotalGoldDepit18 = entity.TotalGoldDepit18,
                        TotalCurrencyCredit = entity.TotalCurrencyCredit,
                        TotalCurrenctDepit = entity.TotalCurrenctDepit,
                        TotalCurGoldCredit24 = entity.TotalCurGoldCredit24,
                        TotalCurGoldDepit24 = entity.TotalCurGoldDepit24,
                        TotalCurGoldCredit22 = entity.TotalCurGoldCredit22,
                        TotalCurGoldDepit22 = entity.TotalCurGoldDepit22,
                        TotalCurGoldCredit21 = entity.TotalCurGoldCredit21,
                        TotalCurGoldDepit21 = entity.TotalCurGoldDepit21,
                        TotalCurGoldCredit18 = entity.TotalCurGoldCredit18,
                        TotalCurGoldDepit18 = entity.TotalCurGoldDepit18,
                        TotalTaxValueCredit = entity.TotalTaxValueCredit,
                        TotalTaxValueDebit = entity.TotalTaxValueDebit,
                        TotalTaxRateCredit = entity.TotalTaxRateCredit,
                        TotalTaxRateDebit = entity.TotalTaxRateDebit,
                        TotalNotTaxCredit = entity.TotalNotTaxCredit,
                        TotalNotTaxDebit = entity.TotalNotTaxDebit,

                        Taxable = entity.Taxable,
                        TaxRate = entity.TaxRate,
                        TaxValue = entity.TaxValue,

                        TotalCreditWithTax = entity.TotalCreditWithTax,
                        TotalDepitWithTax = entity.TotalDepitWithTax,

                        EditReason = entity.EditReason,
                        DeliveryPersonName = entity.DeliveryPersonName,

                        ExternalNumber = entity.ExternalNumber,

                        TotalCreditExemptOfTax = entity.TotalCreditExemptOfTax,
                        ExemptCreditOfTaxValue = entity.ExemptCreditOfTaxValue,
                        TotalDepitExemptOfTax = entity.TotalDepitExemptOfTax,
                        ExemptDepitOfTaxValue = entity.ExemptDepitOfTaxValue,
                        TotalCreditTotalMainVat = entity.TotalCreditTotalMainVat,
                        MainCreditVatValue = entity.MainCreditVatValue,
                        TotalDepitTotalMainVat = entity.TotalDepitTotalMainVat,
                        MainDepitVatValue = entity.MainDepitVatValue,
                        TotalCreditTotalZeroVat = entity.TotalCreditTotalZeroVat,
                        ZeroCreditVatValue = entity.ZeroCreditVatValue,
                        TotalDepitTotalZeroVat = entity.TotalDepitTotalZeroVat,
                        ZeroDepitVatValue = entity.ZeroDepitVatValue,
                        ExemptVatRate = entity.ExemptVatRate,
                        MainVatRate = entity.MainVatRate,
                        ZeroVatRate = entity.ZeroVatRate,
                        TaxNumber = entity.TaxNumber
                    };
            return q.FirstOrDefault();
        }

        public Task<bool> updateEntryOfBill(EntryMasterDetails masterDetails)
        {
            return Task.Run<bool>(() =>
            {
                ENTRY_MASTER entryBill = entrymasterRepo.Filter(e => e.BILL_ID == masterDetails.EntryMaster.BILL_ID).FirstOrDefault();
                if (entryBill != null)
                {
                    ENTRY_MASTER et = new ENTRY_MASTER
                    {
                        ACC_ID = entryBill.ACC_ID,
                        CustAccID = entryBill.CustAccID,
                        EntryVal = entryBill.EntryVal,
                        BILL_ID = entryBill.BILL_ID,
                        CHECK_DATE = entryBill.CHECK_DATE,
                        CHECK_NUMBER = entryBill.CHECK_NUMBER,
                        COLLECT_ENTRY_CODE = entryBill.COLLECT_ENTRY_CODE,
                        CURRENCY_ID = entryBill.CURRENCY_ID,
                        CURRENCY_RATE = entryBill.CURRENCY_RATE,
                        EMP_ID = entryBill.EMP_ID,
                        ENTRY_CREDIT = entryBill.ENTRY_CREDIT,
                        ENTRY_CURRENCY_DIFFERENCE = entryBill.ENTRY_CURRENCY_DIFFERENCE,
                        ENTRY_DATE = entryBill.ENTRY_DATE,
                        ENTRY_DEBIT = entryBill.ENTRY_DEBIT,
                        ENTRY_GOLD_CREDIT = entryBill.ENTRY_GOLD_CREDIT,
                        ENTRY_GOLD_DEBIT = entryBill.ENTRY_GOLD_DEBIT,
                        ENTRY_ID = entryBill.ENTRY_ID,
                        ENTRY_NUMBER = entryBill.ENTRY_NUMBER,
                        ENTRY_REMARKS = entryBill.ENTRY_REMARKS,
                        ENTRY_SETTING_ID = entryBill.ENTRY_SETTING_ID,
                        for_kest = entryBill.for_kest,
                        IS_POSTED = entryBill.IS_POSTED,
                        PAIED = entryBill.PAIED,
                        AddedBy = entryBill.AddedBy,
                        AddedOn = entryBill.AddedOn,
                        UpdatedBy = entryBill.UpdatedBy,
                        updatedOn = entryBill.updatedOn,
                        RelatedEntryID = entryBill.RelatedEntryID,
                        OpeningAccID = entryBill.OpeningAccID,
                        CustID = entryBill.CustID,
                        COM_BRN_ID = entryBill.COM_BRN_ID,

                        TotalCredit = entryBill.TotalCredit,
                        TotalDepit = entryBill.TotalDepit,
                        TotalGoldCredit24 = entryBill.TotalGoldCredit24,
                        TotalGoldDepit24 = entryBill.TotalGoldDepit24,
                        TotalGoldCredit22 = entryBill.TotalGoldCredit22,
                        TotalGoldDepit22 = entryBill.TotalGoldDepit22,
                        TotalGoldCredit21 = entryBill.TotalGoldCredit21,
                        TotalGoldDepit21 = entryBill.TotalGoldDepit21,
                        TotalGoldCredit18 = entryBill.TotalGoldCredit18,
                        TotalGoldDepit18 = entryBill.TotalGoldDepit18,
                        TotalCurrencyCredit = entryBill.TotalCurrencyCredit,
                        TotalCurrenctDepit = entryBill.TotalCurrenctDepit,
                        TotalCurGoldCredit24 = entryBill.TotalCurGoldCredit24,
                        TotalCurGoldDepit24 = entryBill.TotalCurGoldDepit24,
                        TotalCurGoldCredit22 = entryBill.TotalCurGoldCredit22,
                        TotalCurGoldDepit22 = entryBill.TotalCurGoldDepit22,
                        TotalCurGoldCredit21 = entryBill.TotalCurGoldCredit21,
                        TotalCurGoldDepit21 = entryBill.TotalCurGoldDepit21,
                        TotalCurGoldCredit18 = entryBill.TotalCurGoldCredit18,
                        TotalCurGoldDepit18 = entryBill.TotalCurGoldDepit18,
                        TotalTaxValueCredit = entryBill.TotalTaxValueCredit,
                        TotalTaxValueDebit = entryBill.TotalTaxValueDebit,
                        TotalTaxRateCredit = entryBill.TotalTaxRateCredit,
                        TotalTaxRateDebit = entryBill.TotalTaxRateDebit,
                        TotalNotTaxCredit = entryBill.TotalNotTaxCredit,
                        TotalNotTaxDebit = entryBill.TotalNotTaxDebit,

                        Taxable = entryBill.Taxable,
                        TaxRate = entryBill.TaxRate,
                        TaxValue = entryBill.TaxValue,

                        TotalCreditWithTax = entryBill.TotalCreditWithTax,
                        TotalDepitWithTax = entryBill.TotalDepitWithTax,

                        EditReason = entryBill.EditReason,
                        DeliveryPersonName = entryBill.DeliveryPersonName,

                        ExternalNumber = entryBill.ExternalNumber,

                        TotalCreditExemptOfTax = entryBill.TotalCreditExemptOfTax,
                        ExemptCreditOfTaxValue = entryBill.ExemptCreditOfTaxValue,
                        TotalDepitExemptOfTax = entryBill.TotalDepitExemptOfTax,
                        ExemptDepitOfTaxValue = entryBill.ExemptDepitOfTaxValue,
                        TotalCreditTotalMainVat = entryBill.TotalCreditTotalMainVat,
                        MainCreditVatValue = entryBill.MainCreditVatValue,
                        TotalDepitTotalMainVat = entryBill.TotalDepitTotalMainVat,
                        MainDepitVatValue = entryBill.MainDepitVatValue,
                        TotalCreditTotalZeroVat = entryBill.TotalCreditTotalZeroVat,
                        ZeroCreditVatValue = entryBill.ZeroCreditVatValue,
                        TotalDepitTotalZeroVat = entryBill.TotalDepitTotalZeroVat,
                        ZeroDepitVatValue = entryBill.ZeroDepitVatValue,
                        ExemptVatRate = entryBill.ExemptVatRate,
                        MainVatRate = entryBill.MainVatRate,
                        ZeroVatRate = entryBill.ZeroVatRate

                    };
                    entrymasterRepo.Update(et, et.ENTRY_ID);


                    //Getall
                    List<ENTRY_DETAILS> ExistingDetails = GetAllDetails(et.ENTRY_ID);
                    if (ExistingDetails.Count > 0)
                    {
                        //Delete ALl
                        foreach (var item in ExistingDetails)
                        {
                            //object [] keys = { item, item.ENTRY_ID, item, item.ENTRY_ROW_NUMBER };
                            object[] keys = new object[2] { item.ENTRY_ID, item.ENTRY_ROW_NUMBER };
                            try
                            {
                                _entryDetailsRepo.DeleteComposite(item, keys);
                            }
                            catch (Exception ex)
                            {


                            }

                        }
                    }
                    //Insert Again
                    byte rowNo = 0;
                    foreach (var item in masterDetails.EntryDetails)
                    {
                        ENTRY_DETAILS Entry_DetailsMapped = new ENTRY_DETAILS();
                        Entry_DetailsMapped.ACC_ID = item.ACC_ID;
                        Entry_DetailsMapped.COST_CENTER_ID = item.COST_CENTER_ID;
                        Entry_DetailsMapped.Disable = item.Disable;
                        Entry_DetailsMapped.ENTRY_CREDIT = item.ENTRY_CREDIT;
                        Entry_DetailsMapped.ENTRY_DEBIT = item.ENTRY_DEBIT;
                        Entry_DetailsMapped.ENTRY_GOLD24_CREDIT = item.ENTRY_GOLD24_CREDIT;
                        Entry_DetailsMapped.ENTRY_GOLD24_DEBIT = item.ENTRY_GOLD24_DEBIT;

                        Entry_DetailsMapped.ENTRY_GOLD22_CREDIT = item.ENTRY_GOLD22_CREDIT;
                        Entry_DetailsMapped.ENTRY_GOLD22_DEBIT = item.ENTRY_GOLD22_DEBIT;

                        Entry_DetailsMapped.ENTRY_GOLD21_CREDIT = item.ENTRY_GOLD21_CREDIT;
                        Entry_DetailsMapped.ENTRY_GOLD21_DEBIT = item.ENTRY_GOLD21_DEBIT;

                        Entry_DetailsMapped.ENTRY_GOLD18_CREDIT = item.ENTRY_GOLD18_CREDIT;
                        Entry_DetailsMapped.ENTRY_GOLD18_DEBIT = item.ENTRY_GOLD18_DEBIT;
                        Entry_DetailsMapped.ENTRY_DETAILS_REMARKS = item.ENTRY_DETAILS_REMARKS;
                        Entry_DetailsMapped.ENTRY_ID = et.ENTRY_ID;
                        Entry_DetailsMapped.TaxValue = item.TaxValue;
                        Entry_DetailsMapped.Taxable = item.Taxable;
                        Entry_DetailsMapped.TaxRate = item.TaxRate;

                        Entry_DetailsMapped.CheckNumber = item.CheckNumber;
                        Entry_DetailsMapped.CheckDate = item.CheckDate;
                        Entry_DetailsMapped.CheckIssueDate = item.CheckIssueDate;
                        Entry_DetailsMapped.IsExemptOfTax = item.IsExemptOfTax;


                        Entry_DetailsMapped.ExemptOfTaxValue = item.ExemptOfTaxValue;
                        Entry_DetailsMapped.IsMainVatValue = item.IsMainVatValue;
                        Entry_DetailsMapped.MainVatValue = item.MainVatValue;
                        Entry_DetailsMapped.IsZeroVatValue = item.IsZeroVatValue;
                        Entry_DetailsMapped.ZeroVatValue = item.ZeroVatValue;
                        Entry_DetailsMapped.MainVat = item.MainVat;

                        //Entry_DetailsMapped.ENTRY_ROW_NUMBER = item.ENTRY_ROW_NUMBER;
                        Entry_DetailsMapped.ENTRY_ROW_NUMBER = rowNo;
                        _entryDetailsRepo.Add(Entry_DetailsMapped);
                        rowNo++;
                    }
                }

                return true;


            });
        }



        public Task<bool> deleteEntryOfBill(int billID)
        {
            return Task.Run<bool>(() =>
            {
                ENTRY_MASTER entity = entrymasterRepo.Filter(e => e.BILL_ID == billID).FirstOrDefault();
                if (entity != null)
                {
                    ENTRY_MASTER et = new ENTRY_MASTER
                    {
                        ACC_ID = entity.ACC_ID,
                        CustAccID = entity.CustAccID,
                        EntryVal = entity.EntryVal,

                        BILL_ID = entity.BILL_ID,
                        CHECK_DATE = entity.CHECK_DATE,
                        CHECK_NUMBER = entity.CHECK_NUMBER,
                        COLLECT_ENTRY_CODE = entity.COLLECT_ENTRY_CODE,
                        CURRENCY_ID = entity.CURRENCY_ID,
                        CURRENCY_RATE = entity.CURRENCY_RATE,
                        EMP_ID = entity.EMP_ID,
                        ENTRY_CREDIT = entity.ENTRY_CREDIT,
                        ENTRY_CURRENCY_DIFFERENCE = entity.ENTRY_CURRENCY_DIFFERENCE,
                        ENTRY_DATE = entity.ENTRY_DATE,
                        ENTRY_DEBIT = entity.ENTRY_DEBIT,
                        ENTRY_GOLD_CREDIT = entity.ENTRY_GOLD_CREDIT,
                        ENTRY_GOLD_DEBIT = entity.ENTRY_GOLD_DEBIT,
                        ENTRY_ID = entity.ENTRY_ID,
                        ENTRY_NUMBER = entity.ENTRY_NUMBER,
                        ENTRY_REMARKS = entity.ENTRY_REMARKS,
                        ENTRY_SETTING_ID = entity.ENTRY_SETTING_ID,
                        for_kest = entity.for_kest,
                        IS_POSTED = entity.IS_POSTED,
                        PAIED = entity.PAIED,
                        AddedBy = entity.AddedBy,
                        AddedOn = entity.AddedOn,
                        UpdatedBy = entity.UpdatedBy,
                        updatedOn = entity.updatedOn,
                        RelatedEntryID = entity.RelatedEntryID,
                        OpeningAccID = entity.OpeningAccID,
                        CustID = entity.CustID,
                        COM_BRN_ID = entity.COM_BRN_ID,

                        TotalCredit = entity.TotalCredit,
                        TotalDepit = entity.TotalDepit,
                        TotalGoldCredit24 = entity.TotalGoldCredit24,
                        TotalGoldDepit24 = entity.TotalGoldDepit24,
                        TotalGoldCredit22 = entity.TotalGoldCredit22,
                        TotalGoldDepit22 = entity.TotalGoldDepit22,
                        TotalGoldCredit21 = entity.TotalGoldCredit21,
                        TotalGoldDepit21 = entity.TotalGoldDepit21,
                        TotalGoldCredit18 = entity.TotalGoldCredit18,
                        TotalGoldDepit18 = entity.TotalGoldDepit18,
                        TotalCurrencyCredit = entity.TotalCurrencyCredit,
                        TotalCurrenctDepit = entity.TotalCurrenctDepit,
                        TotalCurGoldCredit24 = entity.TotalCurGoldCredit24,
                        TotalCurGoldDepit24 = entity.TotalCurGoldDepit24,
                        TotalCurGoldCredit22 = entity.TotalCurGoldCredit22,
                        TotalCurGoldDepit22 = entity.TotalCurGoldDepit22,
                        TotalCurGoldCredit21 = entity.TotalCurGoldCredit21,
                        TotalCurGoldDepit21 = entity.TotalCurGoldDepit21,
                        TotalCurGoldCredit18 = entity.TotalCurGoldCredit18,
                        TotalCurGoldDepit18 = entity.TotalCurGoldDepit18,
                        TotalTaxValueCredit = entity.TotalTaxValueCredit,
                        TotalTaxValueDebit = entity.TotalTaxValueDebit,
                        TotalTaxRateCredit = entity.TotalTaxRateCredit,
                        TotalTaxRateDebit = entity.TotalTaxRateDebit,
                        TotalNotTaxCredit = entity.TotalNotTaxCredit,
                        TotalNotTaxDebit = entity.TotalNotTaxDebit,

                        Taxable = entity.Taxable,
                        TaxRate = entity.TaxRate,
                        TaxValue = entity.TaxValue,

                        TotalCreditWithTax = entity.TotalCreditWithTax,
                        TotalDepitWithTax = entity.TotalDepitWithTax,

                        EditReason = entity.EditReason,
                        DeliveryPersonName = entity.DeliveryPersonName,

                        ExternalNumber = entity.ExternalNumber,

                        TotalCreditExemptOfTax = entity.TotalCreditExemptOfTax,
                        ExemptCreditOfTaxValue = entity.ExemptCreditOfTaxValue,
                        TotalDepitExemptOfTax = entity.TotalDepitExemptOfTax,
                        ExemptDepitOfTaxValue = entity.ExemptDepitOfTaxValue,
                        TotalCreditTotalMainVat = entity.TotalCreditTotalMainVat,
                        MainCreditVatValue = entity.MainCreditVatValue,
                        TotalDepitTotalMainVat = entity.TotalDepitTotalMainVat,
                        MainDepitVatValue = entity.MainDepitVatValue,
                        TotalCreditTotalZeroVat = entity.TotalCreditTotalZeroVat,
                        ZeroCreditVatValue = entity.ZeroCreditVatValue,
                        TotalDepitTotalZeroVat = entity.TotalDepitTotalZeroVat,
                        ZeroDepitVatValue = entity.ZeroDepitVatValue,
                        ExemptVatRate = entity.ExemptVatRate,
                        MainVatRate = entity.MainVatRate,
                        ZeroVatRate = entity.ZeroVatRate



                    };
                    entrymasterRepo.Delete(et, entity.ENTRY_ID);

                    List<ENTRY_DETAILS> ExistingDetails = GetAllDetails(entity.ENTRY_ID);
                    if (ExistingDetails.Count > 0)
                    {
                        //Delete ALl
                        foreach (var item in ExistingDetails)
                        {
                            //object [] keys = { item, item.ENTRY_ID, item, item.ENTRY_ROW_NUMBER };
                            object[] keys = new object[2] { item.ENTRY_ID, item.ENTRY_ROW_NUMBER };
                            try
                            {
                                _entryDetailsRepo.DeleteComposite(item, keys);
                            }
                            catch (Exception ex)
                            {

                            }
                        }
                    }
                }


                return true;
            });
        }

        //////////////////// daily entry for entry///////////////////////////////////////////////////////
        public Task<bool> CheckEntryByEntryNumber(int billNumber, int billType)
        {
            return Task.Run<bool>(() =>
            {

                var q = entrymasterRepo.Filter(x => x.ENTRY_SETTING_ID == billType && x.ENTRY_NUMBER == billNumber.ToString()).FirstOrDefault();
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




        public Task<EntryMasterDetails> getDailyEntryByMsterID(int masterID)
        {
            return Task.Run<EntryMasterDetails>(() =>
            {

                var q = from entity in entrymasterRepo.Filter(e => e.RelatedEntryID == masterID)
                        select new Entry_MasterVM
                        {
                            ACC_ID = entity.ACC_ID,
                            CustAccID = entity.CustAccID,
                            EntryVal = entity.EntryVal,

                            BILL_ID = entity.BILL_ID,
                            CHECK_DATE = entity.CHECK_DATE,
                            CHECK_NUMBER = entity.CHECK_NUMBER,
                            COLLECT_ENTRY_CODE = entity.COLLECT_ENTRY_CODE,
                            CURRENCY_ID = entity.CURRENCY_ID,
                            CURRENCY_RATE = entity.CURRENCY_RATE,
                            EMP_ID = entity.EMP_ID,
                            ENTRY_CREDIT = entity.ENTRY_CREDIT,
                            ENTRY_CURRENCY_DIFFERENCE = entity.ENTRY_CURRENCY_DIFFERENCE,
                            ENTRY_DATE = entity.ENTRY_DATE,
                            ENTRY_DEBIT = entity.ENTRY_DEBIT,
                            ENTRY_GOLD_CREDIT = entity.ENTRY_GOLD_CREDIT,
                            ENTRY_GOLD_DEBIT = entity.ENTRY_GOLD_DEBIT,
                            ENTRY_ID = entity.ENTRY_ID,
                            ENTRY_NUMBER = entity.ENTRY_NUMBER,
                            ENTRY_REMARKS = entity.ENTRY_REMARKS,
                            ENTRY_SETTING_ID = entity.ENTRY_SETTING_ID,
                            for_kest = entity.for_kest,
                            IS_POSTED = entity.IS_POSTED,
                            PAIED = entity.PAIED,
                            AddedBy = entity.AddedBy,
                            AddedOn = entity.AddedOn,
                            UpdatedBy = entity.UpdatedBy,
                            updatedOn = entity.updatedOn,
                            RelatedEntryID = entity.RelatedEntryID,
                            OpeningAccID = entity.OpeningAccID,
                            CustID = entity.CustID,
                            COM_BRN_ID = entity.COM_BRN_ID,

                            TotalCredit = entity.TotalCredit,
                            TotalDepit = entity.TotalDepit,
                            TotalGoldCredit24 = entity.TotalGoldCredit24,
                            TotalGoldDepit24 = entity.TotalGoldDepit24,
                            TotalGoldCredit22 = entity.TotalGoldCredit22,
                            TotalGoldDepit22 = entity.TotalGoldDepit22,
                            TotalGoldCredit21 = entity.TotalGoldCredit21,
                            TotalGoldDepit21 = entity.TotalGoldDepit21,
                            TotalGoldCredit18 = entity.TotalGoldCredit18,
                            TotalGoldDepit18 = entity.TotalGoldDepit18,
                            TotalCurrencyCredit = entity.TotalCurrencyCredit,
                            TotalCurrenctDepit = entity.TotalCurrenctDepit,
                            TotalCurGoldCredit24 = entity.TotalCurGoldCredit24,
                            TotalCurGoldDepit24 = entity.TotalCurGoldDepit24,
                            TotalCurGoldCredit22 = entity.TotalCurGoldCredit22,
                            TotalCurGoldDepit22 = entity.TotalCurGoldDepit22,
                            TotalCurGoldCredit21 = entity.TotalCurGoldCredit21,
                            TotalCurGoldDepit21 = entity.TotalCurGoldDepit21,
                            TotalCurGoldCredit18 = entity.TotalCurGoldCredit18,
                            TotalCurGoldDepit18 = entity.TotalCurGoldDepit18,
                            TotalTaxValueCredit = entity.TotalTaxValueCredit,
                            TotalTaxValueDebit = entity.TotalTaxValueDebit,
                            TotalTaxRateCredit = entity.TotalTaxRateCredit,
                            TotalTaxRateDebit = entity.TotalTaxRateDebit,
                            TotalNotTaxCredit = entity.TotalNotTaxCredit,
                            TotalNotTaxDebit = entity.TotalNotTaxDebit,

                            Taxable = entity.Taxable,
                            TaxRate = entity.TaxRate,
                            TaxValue = entity.TaxValue,

                            TotalCreditWithTax = entity.TotalCreditWithTax,
                            TotalDepitWithTax = entity.TotalDepitWithTax,

                            EditReason = entity.EditReason,
                            DeliveryPersonName = entity.DeliveryPersonName,

                            ExternalNumber = entity.ExternalNumber,

                            TotalCreditExemptOfTax = entity.TotalCreditExemptOfTax,
                            ExemptCreditOfTaxValue = entity.ExemptCreditOfTaxValue,
                            TotalDepitExemptOfTax = entity.TotalDepitExemptOfTax,
                            ExemptDepitOfTaxValue = entity.ExemptDepitOfTaxValue,
                            TotalCreditTotalMainVat = entity.TotalCreditTotalMainVat,
                            MainCreditVatValue = entity.MainCreditVatValue,
                            TotalDepitTotalMainVat = entity.TotalDepitTotalMainVat,
                            MainDepitVatValue = entity.MainDepitVatValue,
                            TotalCreditTotalZeroVat = entity.TotalCreditTotalZeroVat,
                            ZeroCreditVatValue = entity.ZeroCreditVatValue,
                            TotalDepitTotalZeroVat = entity.TotalDepitTotalZeroVat,
                            ZeroDepitVatValue = entity.ZeroDepitVatValue,
                            ExemptVatRate = entity.ExemptVatRate,
                            MainVatRate = entity.MainVatRate,
                            ZeroVatRate = entity.ZeroVatRate


                        };
                var entryMaster = q.FirstOrDefault();
                List<Entry_DetailsVM> entryDetails = GetDetailsByMasterID(entryMaster.ENTRY_ID);

                EntryMasterDetails masterDetails = new EntryMasterDetails();
                masterDetails.EntryMaster = entryMaster;
                masterDetails.EntryDetails = entryDetails;
                return masterDetails;
            });
        }



        public Task<EntryMasterDetails> getEntryByEntryID(long entryID)
        {
            return Task.Run<EntryMasterDetails>(() =>
            {

                var q = from entity in entrymasterRepo.Filter(e => e.ENTRY_ID == entryID)
                        select new Entry_MasterVM
                        {
                            ACC_ID = entity.ACC_ID,
                            CustAccID = entity.CustAccID,
                            EntryVal = entity.EntryVal,

                            BILL_ID = entity.BILL_ID,
                            CHECK_DATE = entity.CHECK_DATE,
                            CHECK_NUMBER = entity.CHECK_NUMBER,
                            COLLECT_ENTRY_CODE = entity.COLLECT_ENTRY_CODE,
                            CURRENCY_ID = entity.CURRENCY_ID,
                            CURRENCY_RATE = entity.CURRENCY_RATE,
                            EMP_ID = entity.EMP_ID,
                            ENTRY_CREDIT = entity.ENTRY_CREDIT,
                            ENTRY_CURRENCY_DIFFERENCE = entity.ENTRY_CURRENCY_DIFFERENCE,
                            ENTRY_DATE = entity.ENTRY_DATE,
                            ENTRY_DEBIT = entity.ENTRY_DEBIT,
                            ENTRY_GOLD_CREDIT = entity.ENTRY_GOLD_CREDIT,
                            ENTRY_GOLD_DEBIT = entity.ENTRY_GOLD_DEBIT,
                            ENTRY_ID = entity.ENTRY_ID,
                            ENTRY_NUMBER = entity.ENTRY_NUMBER,
                            ENTRY_REMARKS = entity.ENTRY_REMARKS,
                            ENTRY_SETTING_ID = entity.ENTRY_SETTING_ID,
                            for_kest = entity.for_kest,
                            IS_POSTED = entity.IS_POSTED,
                            PAIED = entity.PAIED,
                            AddedBy = entity.AddedBy,
                            AddedOn = entity.AddedOn,
                            UpdatedBy = entity.UpdatedBy,
                            updatedOn = entity.updatedOn,
                            RelatedEntryID = entity.RelatedEntryID,
                            OpeningAccID = entity.OpeningAccID,
                            CustID = entity.CustID,
                            COM_BRN_ID = entity.COM_BRN_ID,

                            TotalCredit = entity.TotalCredit,
                            TotalDepit = entity.TotalDepit,
                            TotalGoldCredit24 = entity.TotalGoldCredit24,
                            TotalGoldDepit24 = entity.TotalGoldDepit24,
                            TotalGoldCredit22 = entity.TotalGoldCredit22,
                            TotalGoldDepit22 = entity.TotalGoldDepit22,
                            TotalGoldCredit21 = entity.TotalGoldCredit21,
                            TotalGoldDepit21 = entity.TotalGoldDepit21,
                            TotalGoldCredit18 = entity.TotalGoldCredit18,
                            TotalGoldDepit18 = entity.TotalGoldDepit18,
                            TotalCurrencyCredit = entity.TotalCurrencyCredit,
                            TotalCurrenctDepit = entity.TotalCurrenctDepit,
                            TotalCurGoldCredit24 = entity.TotalCurGoldCredit24,
                            TotalCurGoldDepit24 = entity.TotalCurGoldDepit24,
                            TotalCurGoldCredit22 = entity.TotalCurGoldCredit22,
                            TotalCurGoldDepit22 = entity.TotalCurGoldDepit22,
                            TotalCurGoldCredit21 = entity.TotalCurGoldCredit21,
                            TotalCurGoldDepit21 = entity.TotalCurGoldDepit21,
                            TotalCurGoldCredit18 = entity.TotalCurGoldCredit18,
                            TotalCurGoldDepit18 = entity.TotalCurGoldDepit18,
                            TotalTaxValueCredit = entity.TotalTaxValueCredit,
                            TotalTaxValueDebit = entity.TotalTaxValueDebit,
                            TotalTaxRateCredit = entity.TotalTaxRateCredit,
                            TotalTaxRateDebit = entity.TotalTaxRateDebit,
                            TotalNotTaxCredit = entity.TotalNotTaxCredit,
                            TotalNotTaxDebit = entity.TotalNotTaxDebit,

                            Taxable = entity.Taxable,
                            TaxRate = entity.TaxRate,
                            TaxValue = entity.TaxValue,

                            TotalCreditWithTax = entity.TotalCreditWithTax,
                            TotalDepitWithTax = entity.TotalDepitWithTax,

                            EditReason = entity.EditReason,
                            DeliveryPersonName = entity.DeliveryPersonName,

                            ExternalNumber = entity.ExternalNumber,

                            TotalCreditExemptOfTax = entity.TotalCreditExemptOfTax,
                            ExemptCreditOfTaxValue = entity.ExemptCreditOfTaxValue,
                            TotalDepitExemptOfTax = entity.TotalDepitExemptOfTax,
                            ExemptDepitOfTaxValue = entity.ExemptDepitOfTaxValue,
                            TotalCreditTotalMainVat = entity.TotalCreditTotalMainVat,
                            MainCreditVatValue = entity.MainCreditVatValue,
                            TotalDepitTotalMainVat = entity.TotalDepitTotalMainVat,
                            MainDepitVatValue = entity.MainDepitVatValue,
                            TotalCreditTotalZeroVat = entity.TotalCreditTotalZeroVat,
                            ZeroCreditVatValue = entity.ZeroCreditVatValue,
                            TotalDepitTotalZeroVat = entity.TotalDepitTotalZeroVat,
                            ZeroDepitVatValue = entity.ZeroDepitVatValue,
                            ExemptVatRate = entity.ExemptVatRate,
                            MainVatRate = entity.MainVatRate,
                            ZeroVatRate = entity.ZeroVatRate


                        };
                var entryMaster = q.FirstOrDefault();
                List<Entry_DetailsVM> entryDetails = GetDetailsByMasterID(entryMaster.ENTRY_ID);

                EntryMasterDetails masterDetails = new EntryMasterDetails();
                masterDetails.EntryMaster = entryMaster;
                masterDetails.EntryDetails = entryDetails;
                return masterDetails;
            });
        }




        public void checkDailyEntryForDelete(long masterID)
        {
            var dailyMaster = entrymasterRepo.Filter(e => e.RelatedEntryID == masterID).FirstOrDefault();
            if (dailyMaster != null)
            {
                List<ENTRY_DETAILS> ExistingDetails = GetAllDetails(dailyMaster.ENTRY_ID);
                if (ExistingDetails.Count > 0)
                {
                    //Delete ALl
                    foreach (var item in ExistingDetails)
                    {
                        //object [] keys = { item, item.ENTRY_ID, item, item.ENTRY_ROW_NUMBER };
                        object[] keys = new object[2] { item.ENTRY_ID, item.ENTRY_ROW_NUMBER };
                        try
                        {
                            _entryDetailsRepo.DeleteComposite(item, keys);
                        }
                        catch (Exception ex)
                        {


                        }

                    }
                }
                entrymasterRepo.Delete(dailyMaster, dailyMaster.ENTRY_ID);
            }

        }
        ////////////////////////////////////////////////////////////////////account entry/////////////////////////


        public long InsertEntryAccount(EntryMasterDetails Obj)
        {
            long masterEntryID;
            //if entryMaster.ID is 0 then Insert else Update 
            if (Obj.EntryMaster.ENTRY_ID == 0)
            {
                long EntryID = InsertGetLastId(Obj.EntryMaster);
                masterEntryID = EntryID;
                if (EntryID > 0)
                {
                    byte rowNo = 0;
                    foreach (var item in Obj.EntryDetails)
                    {
                        ENTRY_DETAILS Entry_DetailsMapped = new ENTRY_DETAILS();
                        Entry_DetailsMapped.ACC_ID = item.ACC_ID;
                        Entry_DetailsMapped.COST_CENTER_ID = item.COST_CENTER_ID;
                        Entry_DetailsMapped.Disable = item.Disable;
                        Entry_DetailsMapped.ENTRY_CREDIT = item.ENTRY_CREDIT;
                        Entry_DetailsMapped.ENTRY_DEBIT = item.ENTRY_DEBIT;

                        Entry_DetailsMapped.ENTRY_GOLD24_CREDIT = item.ENTRY_GOLD24_CREDIT;
                        Entry_DetailsMapped.ENTRY_GOLD24_DEBIT = item.ENTRY_GOLD24_DEBIT;

                        Entry_DetailsMapped.ENTRY_GOLD22_CREDIT = item.ENTRY_GOLD22_CREDIT;
                        Entry_DetailsMapped.ENTRY_GOLD22_DEBIT = item.ENTRY_GOLD22_DEBIT;

                        Entry_DetailsMapped.ENTRY_GOLD21_CREDIT = item.ENTRY_GOLD21_CREDIT;
                        Entry_DetailsMapped.ENTRY_GOLD21_DEBIT = item.ENTRY_GOLD21_DEBIT;

                        Entry_DetailsMapped.ENTRY_GOLD18_CREDIT = item.ENTRY_GOLD18_CREDIT;
                        Entry_DetailsMapped.ENTRY_GOLD18_DEBIT = item.ENTRY_GOLD18_DEBIT;

                        Entry_DetailsMapped.ENTRY_DETAILS_REMARKS = item.ENTRY_DETAILS_REMARKS;
                        Entry_DetailsMapped.ENTRY_ID = EntryID;
                        Entry_DetailsMapped.TaxValue = item.TaxValue;
                        Entry_DetailsMapped.Taxable = item.Taxable;
                        Entry_DetailsMapped.TaxRate = item.TaxRate;

                        Entry_DetailsMapped.CheckNumber = item.CheckNumber;
                        Entry_DetailsMapped.CheckDate = item.CheckDate;
                        Entry_DetailsMapped.CheckIssueDate = item.CheckIssueDate;

                        Entry_DetailsMapped.IsExemptOfTax = item.IsExemptOfTax;


                        Entry_DetailsMapped.ExemptOfTaxValue = item.ExemptOfTaxValue;
                        Entry_DetailsMapped.IsMainVatValue = item.IsMainVatValue;
                        Entry_DetailsMapped.MainVatValue = item.MainVatValue;
                        Entry_DetailsMapped.IsZeroVatValue = item.IsZeroVatValue;
                        Entry_DetailsMapped.ZeroVatValue = item.ZeroVatValue;
                        Entry_DetailsMapped.MainVat = item.MainVat;
                        //  Entry_DetailsMapped.TaxAccountID = item.TaxAccountID;
                        //Entry_DetailsMapped.ENTRY_ROW_NUMBER = item.ENTRY_ROW_NUMBER;
                        Entry_DetailsMapped.ENTRY_ROW_NUMBER = rowNo;
                        _entryDetailsRepo.Add(Entry_DetailsMapped);
                        rowNo++;
                    }
                }
            }
            else
            {
                masterEntryID = Obj.EntryMaster.ENTRY_ID;
                //Update
                bool UpdateStatus = Update(Obj.EntryMaster);
                if (UpdateStatus)
                {
                    //Getall
                    List<ENTRY_DETAILS> ExistingDetails = GetAllDetails(Obj.EntryMaster.ENTRY_ID);
                    if (ExistingDetails.Count > 0)
                    {
                        //Delete ALl
                        foreach (var item in ExistingDetails)
                        {
                            //object [] keys = { item, item.ENTRY_ID, item, item.ENTRY_ROW_NUMBER };
                            object[] keys = new object[2] { item.ENTRY_ID, item.ENTRY_ROW_NUMBER };
                            try
                            {
                                _entryDetailsRepo.DeleteComposite(item, keys);
                            }
                            catch (Exception ex)
                            {


                            }

                        }
                    }
                    //Insert Again
                    byte rowNo = 0;
                    foreach (var item in Obj.EntryDetails)
                    {
                        ENTRY_DETAILS Entry_DetailsMapped = new ENTRY_DETAILS();
                        Entry_DetailsMapped.ACC_ID = item.ACC_ID;
                        Entry_DetailsMapped.COST_CENTER_ID = item.COST_CENTER_ID;
                        Entry_DetailsMapped.Disable = item.Disable;
                        Entry_DetailsMapped.ENTRY_CREDIT = item.ENTRY_CREDIT;
                        Entry_DetailsMapped.ENTRY_DEBIT = item.ENTRY_DEBIT;
                        Entry_DetailsMapped.ENTRY_GOLD24_CREDIT = item.ENTRY_GOLD24_CREDIT;
                        Entry_DetailsMapped.ENTRY_GOLD24_DEBIT = item.ENTRY_GOLD24_DEBIT;

                        Entry_DetailsMapped.ENTRY_GOLD22_CREDIT = item.ENTRY_GOLD22_CREDIT;
                        Entry_DetailsMapped.ENTRY_GOLD22_DEBIT = item.ENTRY_GOLD22_DEBIT;

                        Entry_DetailsMapped.ENTRY_GOLD21_CREDIT = item.ENTRY_GOLD21_CREDIT;
                        Entry_DetailsMapped.ENTRY_GOLD21_DEBIT = item.ENTRY_GOLD21_DEBIT;

                        Entry_DetailsMapped.ENTRY_GOLD18_CREDIT = item.ENTRY_GOLD18_CREDIT;
                        Entry_DetailsMapped.ENTRY_GOLD18_DEBIT = item.ENTRY_GOLD18_DEBIT;
                        Entry_DetailsMapped.ENTRY_DETAILS_REMARKS = item.ENTRY_DETAILS_REMARKS;
                        Entry_DetailsMapped.ENTRY_ID = Obj.EntryMaster.ENTRY_ID;
                        Entry_DetailsMapped.TaxValue = item.TaxValue;
                        Entry_DetailsMapped.Taxable = item.Taxable;
                        Entry_DetailsMapped.TaxRate = item.TaxRate;

                        Entry_DetailsMapped.CheckNumber = item.CheckNumber;
                        Entry_DetailsMapped.CheckDate = item.CheckDate;
                        Entry_DetailsMapped.CheckIssueDate = item.CheckIssueDate;

                        Entry_DetailsMapped.IsExemptOfTax = item.IsExemptOfTax;

                        Entry_DetailsMapped.ExemptOfTaxValue = item.ExemptOfTaxValue;
                        Entry_DetailsMapped.IsMainVatValue = item.IsMainVatValue;
                        Entry_DetailsMapped.MainVatValue = item.MainVatValue;
                        Entry_DetailsMapped.IsZeroVatValue = item.IsZeroVatValue;
                        Entry_DetailsMapped.ZeroVatValue = item.ZeroVatValue;
                        Entry_DetailsMapped.MainVat = item.MainVat;
                        //Entry_DetailsMapped.ENTRY_ROW_NUMBER = item.ENTRY_ROW_NUMBER;
                        Entry_DetailsMapped.ENTRY_ROW_NUMBER = rowNo;
                        _entryDetailsRepo.Add(Entry_DetailsMapped);
                        rowNo++;
                    }
                }
            }
            return masterEntryID;
        }


        public Entry_MasterVM getEntryByaccountID(int accountID)
        {
            var q = from entity in entrymasterRepo.Filter(e => e.OpeningAccID == accountID)
                    select new Entry_MasterVM
                    {
                        ACC_ID = entity.ACC_ID,
                        CustAccID = entity.CustAccID,
                        EntryVal = entity.EntryVal,

                        BILL_ID = entity.BILL_ID,
                        CHECK_DATE = entity.CHECK_DATE,
                        CHECK_NUMBER = entity.CHECK_NUMBER,
                        COLLECT_ENTRY_CODE = entity.COLLECT_ENTRY_CODE,
                        CURRENCY_ID = entity.CURRENCY_ID,
                        CURRENCY_RATE = entity.CURRENCY_RATE,
                        EMP_ID = entity.EMP_ID,
                        ENTRY_CREDIT = entity.ENTRY_CREDIT,
                        ENTRY_CURRENCY_DIFFERENCE = entity.ENTRY_CURRENCY_DIFFERENCE,
                        ENTRY_DATE = entity.ENTRY_DATE,
                        ENTRY_DEBIT = entity.ENTRY_DEBIT,
                        ENTRY_GOLD_CREDIT = entity.ENTRY_GOLD_CREDIT,
                        ENTRY_GOLD_DEBIT = entity.ENTRY_GOLD_DEBIT,
                        ENTRY_ID = entity.ENTRY_ID,
                        ENTRY_NUMBER = entity.ENTRY_NUMBER,
                        ENTRY_REMARKS = entity.ENTRY_REMARKS,
                        ENTRY_SETTING_ID = entity.ENTRY_SETTING_ID,
                        for_kest = entity.for_kest,
                        IS_POSTED = entity.IS_POSTED,
                        PAIED = entity.PAIED,
                        AddedBy = entity.AddedBy,
                        AddedOn = entity.AddedOn,
                        UpdatedBy = entity.UpdatedBy,
                        updatedOn = entity.updatedOn,
                        RelatedEntryID = entity.RelatedEntryID,
                        OpeningAccID = entity.OpeningAccID,
                        CustID = entity.CustID,
                        COM_BRN_ID = entity.COM_BRN_ID,

                        TotalCredit = entity.TotalCredit,
                        TotalDepit = entity.TotalDepit,
                        TotalGoldCredit24 = entity.TotalGoldCredit24,
                        TotalGoldDepit24 = entity.TotalGoldDepit24,
                        TotalGoldCredit22 = entity.TotalGoldCredit22,
                        TotalGoldDepit22 = entity.TotalGoldDepit22,
                        TotalGoldCredit21 = entity.TotalGoldCredit21,
                        TotalGoldDepit21 = entity.TotalGoldDepit21,
                        TotalGoldCredit18 = entity.TotalGoldCredit18,
                        TotalGoldDepit18 = entity.TotalGoldDepit18,
                        TotalCurrencyCredit = entity.TotalCurrencyCredit,
                        TotalCurrenctDepit = entity.TotalCurrenctDepit,
                        TotalCurGoldCredit24 = entity.TotalCurGoldCredit24,
                        TotalCurGoldDepit24 = entity.TotalCurGoldDepit24,
                        TotalCurGoldCredit22 = entity.TotalCurGoldCredit22,
                        TotalCurGoldDepit22 = entity.TotalCurGoldDepit22,
                        TotalCurGoldCredit21 = entity.TotalCurGoldCredit21,
                        TotalCurGoldDepit21 = entity.TotalCurGoldDepit21,
                        TotalCurGoldCredit18 = entity.TotalCurGoldCredit18,
                        TotalCurGoldDepit18 = entity.TotalCurGoldDepit18,
                        TotalTaxValueCredit = entity.TotalTaxValueCredit,
                        TotalTaxValueDebit = entity.TotalTaxValueDebit,
                        TotalTaxRateCredit = entity.TotalTaxRateCredit,
                        TotalTaxRateDebit = entity.TotalTaxRateDebit,
                        TotalNotTaxCredit = entity.TotalNotTaxCredit,
                        TotalNotTaxDebit = entity.TotalNotTaxDebit,

                        Taxable = entity.Taxable,
                        TaxRate = entity.TaxRate,
                        TaxValue = entity.TaxValue,

                        TotalCreditWithTax = entity.TotalCreditWithTax,
                        TotalDepitWithTax = entity.TotalDepitWithTax,

                        EditReason = entity.EditReason,
                        DeliveryPersonName = entity.DeliveryPersonName,

                        ExternalNumber = entity.ExternalNumber,

                        TotalCreditExemptOfTax = entity.TotalCreditExemptOfTax,
                        ExemptCreditOfTaxValue = entity.ExemptCreditOfTaxValue,
                        TotalDepitExemptOfTax = entity.TotalDepitExemptOfTax,
                        ExemptDepitOfTaxValue = entity.ExemptDepitOfTaxValue,
                        TotalCreditTotalMainVat = entity.TotalCreditTotalMainVat,
                        MainCreditVatValue = entity.MainCreditVatValue,
                        TotalDepitTotalMainVat = entity.TotalDepitTotalMainVat,
                        MainDepitVatValue = entity.MainDepitVatValue,
                        TotalCreditTotalZeroVat = entity.TotalCreditTotalZeroVat,
                        ZeroCreditVatValue = entity.ZeroCreditVatValue,
                        TotalDepitTotalZeroVat = entity.TotalDepitTotalZeroVat,
                        ZeroDepitVatValue = entity.ZeroDepitVatValue,
                        ExemptVatRate = entity.ExemptVatRate,
                        MainVatRate = entity.MainVatRate,
                        ZeroVatRate = entity.ZeroVatRate,
                        TaxNumber = entity.TaxNumber



                    };
            return q.FirstOrDefault();
        }



        public Entry_MasterVM getEntryByCustID(int customerID)
        {
            var q = from entity in entrymasterRepo.Filter(e => e.CustID == customerID)
                    select new Entry_MasterVM
                    {
                        ACC_ID = entity.ACC_ID,
                        CustAccID = entity.CustAccID,
                        EntryVal = entity.EntryVal,

                        BILL_ID = entity.BILL_ID,
                        CHECK_DATE = entity.CHECK_DATE,
                        CHECK_NUMBER = entity.CHECK_NUMBER,
                        COLLECT_ENTRY_CODE = entity.COLLECT_ENTRY_CODE,
                        CURRENCY_ID = entity.CURRENCY_ID,
                        CURRENCY_RATE = entity.CURRENCY_RATE,
                        EMP_ID = entity.EMP_ID,
                        ENTRY_CREDIT = entity.ENTRY_CREDIT,
                        ENTRY_CURRENCY_DIFFERENCE = entity.ENTRY_CURRENCY_DIFFERENCE,
                        ENTRY_DATE = entity.ENTRY_DATE,
                        ENTRY_DEBIT = entity.ENTRY_DEBIT,
                        ENTRY_GOLD_CREDIT = entity.ENTRY_GOLD_CREDIT,
                        ENTRY_GOLD_DEBIT = entity.ENTRY_GOLD_DEBIT,
                        ENTRY_ID = entity.ENTRY_ID,
                        ENTRY_NUMBER = entity.ENTRY_NUMBER,
                        ENTRY_REMARKS = entity.ENTRY_REMARKS,
                        ENTRY_SETTING_ID = entity.ENTRY_SETTING_ID,
                        for_kest = entity.for_kest,
                        IS_POSTED = entity.IS_POSTED,
                        PAIED = entity.PAIED,
                        AddedBy = entity.AddedBy,
                        AddedOn = entity.AddedOn,
                        UpdatedBy = entity.UpdatedBy,
                        updatedOn = entity.updatedOn,
                        RelatedEntryID = entity.RelatedEntryID,
                        OpeningAccID = entity.OpeningAccID,
                        CustID = entity.CustID,
                        COM_BRN_ID = entity.COM_BRN_ID,

                        TotalCredit = entity.TotalCredit,
                        TotalDepit = entity.TotalDepit,
                        TotalGoldCredit24 = entity.TotalGoldCredit24,
                        TotalGoldDepit24 = entity.TotalGoldDepit24,
                        TotalGoldCredit22 = entity.TotalGoldCredit22,
                        TotalGoldDepit22 = entity.TotalGoldDepit22,
                        TotalGoldCredit21 = entity.TotalGoldCredit21,
                        TotalGoldDepit21 = entity.TotalGoldDepit21,
                        TotalGoldCredit18 = entity.TotalGoldCredit18,
                        TotalGoldDepit18 = entity.TotalGoldDepit18,
                        TotalCurrencyCredit = entity.TotalCurrencyCredit,
                        TotalCurrenctDepit = entity.TotalCurrenctDepit,
                        TotalCurGoldCredit24 = entity.TotalCurGoldCredit24,
                        TotalCurGoldDepit24 = entity.TotalCurGoldDepit24,
                        TotalCurGoldCredit22 = entity.TotalCurGoldCredit22,
                        TotalCurGoldDepit22 = entity.TotalCurGoldDepit22,
                        TotalCurGoldCredit21 = entity.TotalCurGoldCredit21,
                        TotalCurGoldDepit21 = entity.TotalCurGoldDepit21,
                        TotalCurGoldCredit18 = entity.TotalCurGoldCredit18,
                        TotalCurGoldDepit18 = entity.TotalCurGoldDepit18,
                        TotalTaxValueCredit = entity.TotalTaxValueCredit,
                        TotalTaxValueDebit = entity.TotalTaxValueDebit,
                        TotalTaxRateCredit = entity.TotalTaxRateCredit,
                        TotalTaxRateDebit = entity.TotalTaxRateDebit,
                        TotalNotTaxCredit = entity.TotalNotTaxCredit,
                        TotalNotTaxDebit = entity.TotalNotTaxDebit,

                        Taxable = entity.Taxable,
                        TaxRate = entity.TaxRate,
                        TaxValue = entity.TaxValue,

                        TotalCreditWithTax = entity.TotalCreditWithTax,
                        TotalDepitWithTax = entity.TotalDepitWithTax,

                        EditReason = entity.EditReason,
                        DeliveryPersonName = entity.DeliveryPersonName,

                        ExternalNumber = entity.ExternalNumber,

                        TotalCreditExemptOfTax = entity.TotalCreditExemptOfTax,
                        ExemptCreditOfTaxValue = entity.ExemptCreditOfTaxValue,
                        TotalDepitExemptOfTax = entity.TotalDepitExemptOfTax,
                        ExemptDepitOfTaxValue = entity.ExemptDepitOfTaxValue,
                        TotalCreditTotalMainVat = entity.TotalCreditTotalMainVat,
                        MainCreditVatValue = entity.MainCreditVatValue,
                        TotalDepitTotalMainVat = entity.TotalDepitTotalMainVat,
                        MainDepitVatValue = entity.MainDepitVatValue,
                        TotalCreditTotalZeroVat = entity.TotalCreditTotalZeroVat,
                        ZeroCreditVatValue = entity.ZeroCreditVatValue,
                        TotalDepitTotalZeroVat = entity.TotalDepitTotalZeroVat,
                        ZeroDepitVatValue = entity.ZeroDepitVatValue,
                        ExemptVatRate = entity.ExemptVatRate,
                        MainVatRate = entity.MainVatRate,
                        ZeroVatRate = entity.ZeroVatRate,
                        TaxNumber = entity.TaxNumber



                    };
            return q.FirstOrDefault();
        }

        public bool SetEntriesPosted(List<int> entryIds)
        {
            try
            {
                var Entries = entrymasterRepo.Filter(p => entryIds.Contains((int)p.ENTRY_ID));
                foreach (var entry in Entries)
                {
                    try
                    {
                        entry.IS_POSTED = true;
                        entrymasterRepo.Update(entry, entry.ENTRY_ID);
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

        public List<Entry_MasterVM> getNotPostedEntries(EntryPostingSearchVM searchObject)
        {
            try
            {
                return entrymasterRepo.Filter(p =>
                        p.IS_POSTED == false && p.ENTRY_DATE >= searchObject.StartDate &&
                        p.ENTRY_DATE <= searchObject.EndDate &&
                        //(searchObject.CostCenterId != null ? p.COST_CENTER_ID == searchObject.CostCenterId : true) &&
                        (searchObject.EmployeeId != null ? p.EMP_ID == searchObject.EmployeeId : true) &&
                        (searchObject.BoxAccountId != null ? p.ACC_ID == searchObject.BoxAccountId : true) &&
                        (searchObject.EntryTypeId != null ? p.ENTRY_SETTING_ID == searchObject.EntryTypeId : true))
                    //(searchObject.PayTypeId != null ? p.BILL_PAY_WAY == searchObject.PayTypeId.ToString() : true))
                    .Where(p => (searchObject.FromEntryNumber != null
                                    ? int.Parse(p.ENTRY_NUMBER) >= searchObject.FromEntryNumber
                                    : true) &&
                                (searchObject.ToEntryNumber != null
                                    ? int.Parse(p.ENTRY_NUMBER) <= searchObject.ToEntryNumber
                                    : true))
                    .Select(p =>
                        new Entry_MasterVM
                        {
                            ENTRY_ID = p.ENTRY_ID,
                            ENTRY_DATE = p.ENTRY_DATE,
                            ENTRY_REMARKS = p.ENTRY_REMARKS,
                            ENTRY_NUMBER = p.ENTRY_NUMBER
                        }).ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

    }

}
