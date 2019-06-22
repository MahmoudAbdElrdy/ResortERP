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
    public class EntryGridColumnsService : IEntryGridColumnsService, IDisposable
    {
        IGenericRepository<ENTRY_GRID_COLUMNS> entryGrdColRepo;
        public EntryGridColumnsService(IGenericRepository<ENTRY_GRID_COLUMNS> entryGrdColRepo)
        {
            this.entryGrdColRepo = entryGrdColRepo;
        }

        public Task<int> CountAsync()
        {
            return Task.Run<int>(() =>
            {
                return entryGrdColRepo.CountAsync();
            });
        }

        public bool Delete(Entry_Grid_ColumnsVM entity)
        {
            ENTRY_GRID_COLUMNS egc = new ENTRY_GRID_COLUMNS
            {
                ENTRY_SETTING_ID = entity.ENTRY_SETTING_ID,
                ACC_COLOR = entity.ACC_COLOR,
                ACC_INDEX = entity.ACC_INDEX,
                ACC_WIDTH = entity.ACC_WIDTH,
                COST_CENTER_COLOR = entity.COST_CENTER_COLOR,
                COST_CENTER_INDEX = entity.COST_CENTER_INDEX,
                COST_CENTER_WIDTH = entity.COST_CENTER_WIDTH,
                CREDIT_COLOR = entity.CREDIT_COLOR,
                CREDIT_INDEX = entity.CREDIT_INDEX,
                CREDIT_WIDTH = entity.CREDIT_WIDTH,
                DEBIT_COLOR = entity.DEBIT_COLOR,
                DEBIT_INDEX = entity.DEBIT_INDEX,
                DEBIT_WIDTH = entity.DEBIT_WIDTH,
                REMARKS_COLOR = entity.REMARKS_COLOR,
                REMARKS_INDEX = entity.REMARKS_INDEX,
                REMARKS_WIDTH = entity.REMARKS_WIDTH,
                AddedBy = entity.AddedBy,
                AddedOn = entity.AddedOn,
                UpdatedBy = entity.UpdatedBy,
                updatedOn = entity.updatedOn,
                ACC_COLOR_HEXA = entity.ACC_COLOR_HEXA,
                COST_CENTER_COLOR_HEXA = entity.COST_CENTER_COLOR_HEXA,
                CREDIT_COLOR_HEXA = entity.CREDIT_COLOR_HEXA,
                DEBIT_COLOR_HEXA = entity.DEBIT_COLOR_HEXA,
                REMARKS_COLOR_HEXA = entity.REMARKS_COLOR_HEXA,

                IS_CREDIT = entity.IS_CREDIT,
                IS_DEBIT = entity.IS_DEBIT,
                IS_ACC = entity.IS_ACC,
                IS_COST_CENTER = entity.IS_COST_CENTER,
                IS_REMARKS = entity.IS_REMARKS,

                IS_TaxValue = entity.IS_TaxValue,
                IS_Taxable = entity.IS_Taxable,

                Taxable_INDEX = entity.Taxable_INDEX,
                Taxable_WIDTH = entity.Taxable_WIDTH,
                Taxable_COLOR = entity.Taxable_COLOR,
                Taxable_COLOR_HEXA = entity.Taxable_COLOR_HEXA,

                TaxValue_INDEX = entity.TaxValue_INDEX,
                TaxValue_WIDTH = entity.TaxValue_WIDTH,
                TaxValue_COLOR = entity.TaxValue_COLOR,
                TaxValue_COLOR_HEXA = entity.TaxValue_COLOR_HEXA,


                GoldCredit24_INDEX = entity.GoldCredit24_INDEX,
                GoldCredit24_WIDTH = entity.GoldCredit24_WIDTH,
                GoldCredit24_COLOR = entity.GoldCredit24_COLOR,
                GoldCredit24_COLOR_HEXA = entity.GoldCredit24_COLOR_HEXA,
                IS_GoldCredit24 = entity.IS_GoldCredit24,

                GoldDepit24_INDEX = entity.GoldDepit24_INDEX,
                GoldDepit24_WIDTH = entity.GoldDepit24_WIDTH,
                GoldDepit24_COLOR = entity.GoldDepit24_COLOR,
                GoldDepit24_COLOR_HEXA = entity.GoldDepit24_COLOR_HEXA,
                IS_GoldDepit24 = entity.IS_GoldDepit24,


                GoldCredit22_INDEX = entity.GoldCredit22_INDEX,
                GoldCredit22_WIDTH = entity.GoldCredit22_WIDTH,
                GoldCredit22_COLOR = entity.GoldCredit22_COLOR,
                GoldCredit22_COLOR_HEXA = entity.GoldCredit22_COLOR_HEXA,
                IS_GoldCredit22 = entity.IS_GoldCredit22,

                GoldDepit22_INDEX = entity.GoldDepit22_INDEX,
                GoldDepit22_WIDTH = entity.GoldDepit22_WIDTH,
                GoldDepit22_COLOR = entity.GoldDepit22_COLOR,
                GoldDepit22_COLOR_HEXA = entity.GoldDepit22_COLOR_HEXA,
                IS_GoldDepit22 = entity.IS_GoldDepit22,


                GoldCredit21_INDEX = entity.GoldCredit21_INDEX,
                GoldCredit21_WIDTH = entity.GoldCredit21_WIDTH,
                GoldCredit21_COLOR = entity.GoldCredit21_COLOR,
                GoldCredit21_COLOR_HEXA = entity.GoldCredit21_COLOR_HEXA,
                IS_GoldCredit21 = entity.IS_GoldCredit21,

                GoldDepit21_INDEX = entity.GoldDepit21_INDEX,
                GoldDepit21_WIDTH = entity.GoldDepit21_WIDTH,
                GoldDepit21_COLOR = entity.GoldDepit21_COLOR,
                GoldDepit21_COLOR_HEXA = entity.GoldDepit21_COLOR_HEXA,
                IS_GoldDepit21 = entity.IS_GoldDepit21,

                GoldCredit18_INDEX = entity.GoldCredit18_INDEX,
                GoldCredit18_WIDTH = entity.GoldCredit18_WIDTH,
                GoldCredit18_COLOR = entity.GoldCredit18_COLOR,
                GoldCredit18_COLOR_HEXA = entity.GoldCredit18_COLOR_HEXA,
                IS_GoldCredit18 = entity.IS_GoldCredit18,

                GoldDepit18_INDEX = entity.GoldDepit18_INDEX,
                GoldDepit18_WIDTH = entity.GoldDepit18_WIDTH,
                GoldDepit18_COLOR = entity.GoldDepit18_COLOR,
                GoldDepit18_COLOR_HEXA = entity.GoldDepit18_COLOR_HEXA,
                IS_GoldDepit18 = entity.IS_GoldDepit18,

                TaxRate_INDEX = entity.TaxRate_INDEX,
                TaxRate_WIDTH = entity.TaxRate_WIDTH,
                TaxRate_COLOR = entity.TaxRate_COLOR,
                TaxRate_COLOR_HEXA = entity.TaxRate_COLOR_HEXA,
                IS_TaxRate = entity.IS_TaxRate,

                ARTaxValue = entity.ARTaxValue,
                ENTaxValue = entity.ENTaxValue,

                CheckDate_INDEX = entity.CheckDate_INDEX,
                CheckDate_WIDTH = entity.CheckDate_WIDTH,
                CheckDate_COLOR = entity.CheckDate_COLOR,
                CheckDate_COLOR_HEXA = entity.CheckDate_COLOR_HEXA,
                IsCheckDate = entity.IsCheckDate,

                CheckNumber_INDEX = entity.CheckNumber_INDEX,
                CheckNumber_WIDTH = entity.CheckNumber_WIDTH,
                CheckNumber_COLOR = entity.CheckNumber_COLOR,
                CheckNumber_COLOR_HEXA = entity.CheckNumber_COLOR_HEXA,
                IsCheckNumber = entity.IsCheckNumber,

                CheckIssueDate_INDEX = entity.CheckIssueDate_INDEX,
                CheckIssueDate_WIDTH = entity.CheckIssueDate_WIDTH,
                CheckIssueDate_COLOR = entity.CheckIssueDate_COLOR,
                CheckIssueDate_COLOR_HEXA = entity.CheckIssueDate_COLOR_HEXA,
                IsCheckIssueDate = entity.IsCheckIssueDate,


                ExemptOfTax_INDEX=entity.ExemptOfTax_INDEX,
                ExemptOfTax_WIDTH= entity.ExemptOfTax_WIDTH,
                ExemptOfTax_COLOR=entity.ExemptOfTax_COLOR,
                ExemptOfTax_COLOR_HEXA=entity.ExemptOfTax_COLOR_HEXA,
                IsExemptOfTax=entity.IsExemptOfTax,

                ARExemptOfTax=entity.ARExemptOfTax,
                ENExemptOfTax=entity.ENExemptOfTax,



                ARCheckDate = entity.ARCheckDate,
                ENCheckDate = entity.ENCheckDate,

                ARCheckNumber = entity.ARCheckNumber,
                ENCheckNumber = entity.ENCheckNumber,

                ARCheckIssueDate = entity.ARCheckIssueDate,
                ENCheckIssueDate = entity.ENCheckIssueDate

            };
            entryGrdColRepo.Delete(egc, entity.ENTRY_SETTING_ID);
            return true;
        }

        public Task<bool> DeleteAsync(Entry_Grid_ColumnsVM entity)
        {
            return Task.Run<bool>(() =>
            {
                ENTRY_GRID_COLUMNS egc = new ENTRY_GRID_COLUMNS
                {
                    ENTRY_SETTING_ID = entity.ENTRY_SETTING_ID,
                    ACC_COLOR = entity.ACC_COLOR,
                    ACC_INDEX = entity.ACC_INDEX,
                    ACC_WIDTH = entity.ACC_WIDTH,
                    COST_CENTER_COLOR = entity.COST_CENTER_COLOR,
                    COST_CENTER_INDEX = entity.COST_CENTER_INDEX,
                    COST_CENTER_WIDTH = entity.COST_CENTER_WIDTH,
                    CREDIT_COLOR = entity.CREDIT_COLOR,
                    CREDIT_INDEX = entity.CREDIT_INDEX,
                    CREDIT_WIDTH = entity.CREDIT_WIDTH,
                    DEBIT_COLOR = entity.DEBIT_COLOR,
                    DEBIT_INDEX = entity.DEBIT_INDEX,
                    DEBIT_WIDTH = entity.DEBIT_WIDTH,
                    REMARKS_COLOR = entity.REMARKS_COLOR,
                    REMARKS_INDEX = entity.REMARKS_INDEX,
                    REMARKS_WIDTH = entity.REMARKS_WIDTH,
                    AddedBy = entity.AddedBy,
                    AddedOn = entity.AddedOn,
                    UpdatedBy = entity.UpdatedBy,
                    updatedOn = entity.updatedOn,
                    ACC_COLOR_HEXA = entity.ACC_COLOR_HEXA,
                    COST_CENTER_COLOR_HEXA = entity.COST_CENTER_COLOR_HEXA,
                    CREDIT_COLOR_HEXA = entity.CREDIT_COLOR_HEXA,
                    DEBIT_COLOR_HEXA = entity.DEBIT_COLOR_HEXA,
                    REMARKS_COLOR_HEXA = entity.REMARKS_COLOR_HEXA,

                    IS_CREDIT = entity.IS_CREDIT,
                    IS_DEBIT = entity.IS_DEBIT,
                    IS_ACC = entity.IS_ACC,
                    IS_COST_CENTER = entity.IS_COST_CENTER,
                    IS_REMARKS = entity.IS_REMARKS,
                    IS_TaxValue = entity.IS_TaxValue,
                    IS_Taxable = entity.IS_Taxable,

                    Taxable_INDEX = entity.Taxable_INDEX,
                    Taxable_WIDTH = entity.Taxable_WIDTH,
                    Taxable_COLOR = entity.Taxable_COLOR,
                    Taxable_COLOR_HEXA = entity.Taxable_COLOR_HEXA,

                    TaxValue_INDEX = entity.TaxValue_INDEX,
                    TaxValue_WIDTH = entity.TaxValue_WIDTH,
                    TaxValue_COLOR = entity.TaxValue_COLOR,
                    TaxValue_COLOR_HEXA = entity.TaxValue_COLOR_HEXA,

                    GoldCredit24_INDEX = entity.GoldCredit24_INDEX,
                    GoldCredit24_WIDTH = entity.GoldCredit24_WIDTH,
                    GoldCredit24_COLOR = entity.GoldCredit24_COLOR,
                    GoldCredit24_COLOR_HEXA = entity.GoldCredit24_COLOR_HEXA,
                    IS_GoldCredit24 = entity.IS_GoldCredit24,

                    GoldDepit24_INDEX = entity.GoldDepit24_INDEX,
                    GoldDepit24_WIDTH = entity.GoldDepit24_WIDTH,
                    GoldDepit24_COLOR = entity.GoldDepit24_COLOR,
                    GoldDepit24_COLOR_HEXA = entity.GoldDepit24_COLOR_HEXA,
                    IS_GoldDepit24 = entity.IS_GoldDepit24,


                    GoldCredit22_INDEX = entity.GoldCredit22_INDEX,
                    GoldCredit22_WIDTH = entity.GoldCredit22_WIDTH,
                    GoldCredit22_COLOR = entity.GoldCredit22_COLOR,
                    GoldCredit22_COLOR_HEXA = entity.GoldCredit22_COLOR_HEXA,
                    IS_GoldCredit22 = entity.IS_GoldCredit22,

                    GoldDepit22_INDEX = entity.GoldDepit22_INDEX,
                    GoldDepit22_WIDTH = entity.GoldDepit22_WIDTH,
                    GoldDepit22_COLOR = entity.GoldDepit22_COLOR,
                    GoldDepit22_COLOR_HEXA = entity.GoldDepit22_COLOR_HEXA,
                    IS_GoldDepit22 = entity.IS_GoldDepit22,


                    GoldCredit21_INDEX = entity.GoldCredit21_INDEX,
                    GoldCredit21_WIDTH = entity.GoldCredit21_WIDTH,
                    GoldCredit21_COLOR = entity.GoldCredit21_COLOR,
                    GoldCredit21_COLOR_HEXA = entity.GoldCredit21_COLOR_HEXA,
                    IS_GoldCredit21 = entity.IS_GoldCredit21,

                    GoldDepit21_INDEX = entity.GoldDepit21_INDEX,
                    GoldDepit21_WIDTH = entity.GoldDepit21_WIDTH,
                    GoldDepit21_COLOR = entity.GoldDepit21_COLOR,
                    GoldDepit21_COLOR_HEXA = entity.GoldDepit21_COLOR_HEXA,
                    IS_GoldDepit21 = entity.IS_GoldDepit21,

                    GoldCredit18_INDEX = entity.GoldCredit18_INDEX,
                    GoldCredit18_WIDTH = entity.GoldCredit18_WIDTH,
                    GoldCredit18_COLOR = entity.GoldCredit18_COLOR,
                    GoldCredit18_COLOR_HEXA = entity.GoldCredit18_COLOR_HEXA,
                    IS_GoldCredit18 = entity.IS_GoldCredit18,

                    GoldDepit18_INDEX = entity.GoldDepit18_INDEX,
                    GoldDepit18_WIDTH = entity.GoldDepit18_WIDTH,
                    GoldDepit18_COLOR = entity.GoldDepit18_COLOR,
                    GoldDepit18_COLOR_HEXA = entity.GoldDepit18_COLOR_HEXA,
                    IS_GoldDepit18 = entity.IS_GoldDepit18,


                    TaxRate_INDEX = entity.TaxRate_INDEX,
                    TaxRate_WIDTH = entity.TaxRate_WIDTH,
                    TaxRate_COLOR = entity.TaxRate_COLOR,
                    TaxRate_COLOR_HEXA = entity.TaxRate_COLOR_HEXA,
                    IS_TaxRate = entity.IS_TaxRate,



                    CheckDate_INDEX = entity.CheckDate_INDEX,
                    CheckDate_WIDTH = entity.CheckDate_WIDTH,
                    CheckDate_COLOR = entity.CheckDate_COLOR,
                    CheckDate_COLOR_HEXA = entity.CheckDate_COLOR_HEXA,
                    IsCheckDate = entity.IsCheckDate,

                    CheckNumber_INDEX = entity.CheckNumber_INDEX,
                    CheckNumber_WIDTH = entity.CheckNumber_WIDTH,
                    CheckNumber_COLOR = entity.CheckNumber_COLOR,
                    CheckNumber_COLOR_HEXA = entity.CheckNumber_COLOR_HEXA,
                    IsCheckNumber = entity.IsCheckNumber,

                    CheckIssueDate_INDEX = entity.CheckIssueDate_INDEX,
                    CheckIssueDate_WIDTH = entity.CheckIssueDate_WIDTH,
                    CheckIssueDate_COLOR = entity.CheckIssueDate_COLOR,
                    CheckIssueDate_COLOR_HEXA = entity.CheckIssueDate_COLOR_HEXA,
                    IsCheckIssueDate = entity.IsCheckIssueDate,



                    ExemptOfTax_INDEX = entity.ExemptOfTax_INDEX,
                    ExemptOfTax_WIDTH = entity.ExemptOfTax_WIDTH,
                    ExemptOfTax_COLOR = entity.ExemptOfTax_COLOR,
                    ExemptOfTax_COLOR_HEXA = entity.ExemptOfTax_COLOR_HEXA,
                    IsExemptOfTax = entity.IsExemptOfTax,

                    ARExemptOfTax = entity.ARExemptOfTax,
                    ENExemptOfTax = entity.ENExemptOfTax,




                    ARCheckDate = entity.ARCheckDate,
                    ENCheckDate = entity.ENCheckDate,

                    ARCheckNumber = entity.ARCheckNumber,
                    ENCheckNumber = entity.ENCheckNumber,

                    ARCheckIssueDate = entity.ARCheckIssueDate,
                    ENCheckIssueDate = entity.ENCheckIssueDate,


                    ARTaxValue = entity.ARTaxValue,
                    ENTaxValue = entity.ENTaxValue,

                    ARCode = entity.ARCode,
                    ENCode = entity.ENCode,
                    ARAccountName = entity.ARAccountName,
                    ENAccountName = entity.ENAccountName,
                    ARCredit = entity.ARCredit,
                    ENCredit = entity.ENCredit,
                    ARDepit = entity.ARDepit,
                    ENDepit = entity.ENDepit,
                    ARCredit24 = entity.ARCredit24,
                    ENCredit24 = entity.ENCredit24,
                    ARDepit24 = entity.ARDepit24,
                    ENDepit24 = entity.ENDepit24,
                    ARCredit22 = entity.ARCredit22,
                    ENCredit22 = entity.ENCredit22,
                    ARDepit22 = entity.ARDepit22,
                    ENDepit22 = entity.ENDepit22,
                    ARCredit21 = entity.ARCredit21,
                    ENCredit21 = entity.ENCredit21,
                    ARDepit21 = entity.ARDepit21,
                    ENDepit21 = entity.ENDepit21,
                    ARCredit18 = entity.ARCredit18,
                    ENCredit18 = entity.ENCredit18,
                    ARDepit18 = entity.ARDepit18,
                    ENDepit18 = entity.ENDepit18,
                    ARCostCenter = entity.ARCostCenter,
                    ENCostCenter = entity.ENCostCenter,
                    ARRemarks = entity.ARRemarks,
                    ENRemarks = entity.ENRemarks,
                    ARTaxable = entity.ARTaxable,
                    EnTaxable = entity.EnTaxable,
                    ARTaxRate = entity.ARTaxRate,
                    ENTaxRate = entity.ENTaxRate


                };
                entryGrdColRepo.Delete(egc, entity.ENTRY_SETTING_ID);
                return true;
            });
        }

        public void Dispose()
        {
            entryGrdColRepo.Dispose();
        }

        public Task<List<Entry_Grid_ColumnsVM>> GetAllAsync(int pageNum, int pageSize)
        {
            return Task.Run<List<Entry_Grid_ColumnsVM>>(() =>
            {
                int rowCount;
                var q = from entity in entryGrdColRepo.GetPaged<long>(pageNum, pageSize, p => p.ENTRY_SETTING_ID, false, out rowCount)
                        select new Entry_Grid_ColumnsVM
                        {
                            ENTRY_SETTING_ID = entity.ENTRY_SETTING_ID,
                            ACC_COLOR = entity.ACC_COLOR,
                            ACC_INDEX = entity.ACC_INDEX,
                            ACC_WIDTH = entity.ACC_WIDTH,
                            COST_CENTER_COLOR = entity.COST_CENTER_COLOR,
                            COST_CENTER_INDEX = entity.COST_CENTER_INDEX,
                            COST_CENTER_WIDTH = entity.COST_CENTER_WIDTH,
                            CREDIT_COLOR = entity.CREDIT_COLOR,
                            CREDIT_INDEX = entity.CREDIT_INDEX,
                            CREDIT_WIDTH = entity.CREDIT_WIDTH,
                            DEBIT_COLOR = entity.DEBIT_COLOR,
                            DEBIT_INDEX = entity.DEBIT_INDEX,
                            DEBIT_WIDTH = entity.DEBIT_WIDTH,
                            REMARKS_COLOR = entity.REMARKS_COLOR,
                            REMARKS_INDEX = entity.REMARKS_INDEX,
                            REMARKS_WIDTH = entity.REMARKS_WIDTH,
                            AddedBy = entity.AddedBy,
                            AddedOn = entity.AddedOn,
                            UpdatedBy = entity.UpdatedBy,
                            updatedOn = entity.updatedOn,
                            ACC_COLOR_HEXA = entity.ACC_COLOR_HEXA,
                            COST_CENTER_COLOR_HEXA = entity.COST_CENTER_COLOR_HEXA,
                            CREDIT_COLOR_HEXA = entity.CREDIT_COLOR_HEXA,
                            DEBIT_COLOR_HEXA = entity.DEBIT_COLOR_HEXA,
                            REMARKS_COLOR_HEXA = entity.REMARKS_COLOR_HEXA,

                            IS_CREDIT = entity.IS_CREDIT,
                            IS_DEBIT = entity.IS_DEBIT,
                            IS_ACC = entity.IS_ACC,
                            IS_COST_CENTER = entity.IS_COST_CENTER,
                            IS_REMARKS = entity.IS_REMARKS,
                            IS_TaxValue = entity.IS_TaxValue,
                            IS_Taxable = entity.IS_Taxable,

                            Taxable_INDEX = entity.Taxable_INDEX,
                            Taxable_WIDTH = entity.Taxable_WIDTH,
                            Taxable_COLOR = entity.Taxable_COLOR,
                            Taxable_COLOR_HEXA = entity.Taxable_COLOR_HEXA,

                            TaxValue_INDEX = entity.TaxValue_INDEX,
                            TaxValue_WIDTH = entity.TaxValue_WIDTH,
                            TaxValue_COLOR = entity.TaxValue_COLOR,
                            TaxValue_COLOR_HEXA = entity.TaxValue_COLOR_HEXA,

                            GoldCredit24_INDEX = entity.GoldCredit24_INDEX,
                            GoldCredit24_WIDTH = entity.GoldCredit24_WIDTH,
                            GoldCredit24_COLOR = entity.GoldCredit24_COLOR,
                            GoldCredit24_COLOR_HEXA = entity.GoldCredit24_COLOR_HEXA,
                            IS_GoldCredit24 = entity.IS_GoldCredit24,

                            GoldDepit24_INDEX = entity.GoldDepit24_INDEX,
                            GoldDepit24_WIDTH = entity.GoldDepit24_WIDTH,
                            GoldDepit24_COLOR = entity.GoldDepit24_COLOR,
                            GoldDepit24_COLOR_HEXA = entity.GoldDepit24_COLOR_HEXA,
                            IS_GoldDepit24 = entity.IS_GoldDepit24,


                            GoldCredit22_INDEX = entity.GoldCredit22_INDEX,
                            GoldCredit22_WIDTH = entity.GoldCredit22_WIDTH,
                            GoldCredit22_COLOR = entity.GoldCredit22_COLOR,
                            GoldCredit22_COLOR_HEXA = entity.GoldCredit22_COLOR_HEXA,
                            IS_GoldCredit22 = entity.IS_GoldCredit22,

                            GoldDepit22_INDEX = entity.GoldDepit22_INDEX,
                            GoldDepit22_WIDTH = entity.GoldDepit22_WIDTH,
                            GoldDepit22_COLOR = entity.GoldDepit22_COLOR,
                            GoldDepit22_COLOR_HEXA = entity.GoldDepit22_COLOR_HEXA,
                            IS_GoldDepit22 = entity.IS_GoldDepit22,


                            GoldCredit21_INDEX = entity.GoldCredit21_INDEX,
                            GoldCredit21_WIDTH = entity.GoldCredit21_WIDTH,
                            GoldCredit21_COLOR = entity.GoldCredit21_COLOR,
                            GoldCredit21_COLOR_HEXA = entity.GoldCredit21_COLOR_HEXA,
                            IS_GoldCredit21 = entity.IS_GoldCredit21,

                            GoldDepit21_INDEX = entity.GoldDepit21_INDEX,
                            GoldDepit21_WIDTH = entity.GoldDepit21_WIDTH,
                            GoldDepit21_COLOR = entity.GoldDepit21_COLOR,
                            GoldDepit21_COLOR_HEXA = entity.GoldDepit21_COLOR_HEXA,
                            IS_GoldDepit21 = entity.IS_GoldDepit21,

                            GoldCredit18_INDEX = entity.GoldCredit18_INDEX,
                            GoldCredit18_WIDTH = entity.GoldCredit18_WIDTH,
                            GoldCredit18_COLOR = entity.GoldCredit18_COLOR,
                            GoldCredit18_COLOR_HEXA = entity.GoldCredit18_COLOR_HEXA,
                            IS_GoldCredit18 = entity.IS_GoldCredit18,

                            GoldDepit18_INDEX = entity.GoldDepit18_INDEX,
                            GoldDepit18_WIDTH = entity.GoldDepit18_WIDTH,
                            GoldDepit18_COLOR = entity.GoldDepit18_COLOR,
                            GoldDepit18_COLOR_HEXA = entity.GoldDepit18_COLOR_HEXA,
                            IS_GoldDepit18 = entity.IS_GoldDepit18,

                            TaxRate_INDEX = entity.TaxRate_INDEX,
                            TaxRate_WIDTH = entity.TaxRate_WIDTH,
                            TaxRate_COLOR = entity.TaxRate_COLOR,
                            TaxRate_COLOR_HEXA = entity.TaxRate_COLOR_HEXA,
                            IS_TaxRate = entity.IS_TaxRate,



                            ExemptOfTax_INDEX = entity.ExemptOfTax_INDEX,
                            ExemptOfTax_WIDTH = entity.ExemptOfTax_WIDTH,
                            ExemptOfTax_COLOR = entity.ExemptOfTax_COLOR,
                            ExemptOfTax_COLOR_HEXA = entity.ExemptOfTax_COLOR_HEXA,
                            IsExemptOfTax = entity.IsExemptOfTax,

                            ARExemptOfTax = entity.ARExemptOfTax,
                            ENExemptOfTax = entity.ENExemptOfTax,


                            ARTaxValue = entity.ARTaxValue,
                            ENTaxValue = entity.ENTaxValue,

                            ARCode = entity.ARCode,
                            ENCode = entity.ENCode,
                            ARAccountName = entity.ARAccountName,
                            ENAccountName = entity.ENAccountName,
                            ARCredit = entity.ARCredit,
                            ENCredit = entity.ENCredit,
                            ARDepit = entity.ARDepit,
                            ENDepit = entity.ENDepit,
                            ARCredit24 = entity.ARCredit24,
                            ENCredit24 = entity.ENCredit24,
                            ARDepit24 = entity.ARDepit24,
                            ENDepit24 = entity.ENDepit24,
                            ARCredit22 = entity.ARCredit22,
                            ENCredit22 = entity.ENCredit22,
                            ARDepit22 = entity.ARDepit22,
                            ENDepit22 = entity.ENDepit22,
                            ARCredit21 = entity.ARCredit21,
                            ENCredit21 = entity.ENCredit21,
                            ARDepit21 = entity.ARDepit21,
                            ENDepit21 = entity.ENDepit21,
                            ARCredit18 = entity.ARCredit18,
                            ENCredit18 = entity.ENCredit18,
                            ARDepit18 = entity.ARDepit18,
                            ENDepit18 = entity.ENDepit18,
                            ARCostCenter = entity.ARCostCenter,
                            ENCostCenter = entity.ENCostCenter,
                            ARRemarks = entity.ARRemarks,
                            ENRemarks = entity.ENRemarks,
                            ARTaxable = entity.ARTaxable,
                            EnTaxable = entity.EnTaxable,
                            ARTaxRate = entity.ARTaxRate,
                            ENTaxRate = entity.ENTaxRate,


                            CheckDate_INDEX = entity.CheckDate_INDEX,
                            CheckDate_WIDTH = entity.CheckDate_WIDTH,
                            CheckDate_COLOR = entity.CheckDate_COLOR,
                            CheckDate_COLOR_HEXA = entity.CheckDate_COLOR_HEXA,
                            IsCheckDate = entity.IsCheckDate,

                            CheckNumber_INDEX = entity.CheckNumber_INDEX,
                            CheckNumber_WIDTH = entity.CheckNumber_WIDTH,
                            CheckNumber_COLOR = entity.CheckNumber_COLOR,
                            CheckNumber_COLOR_HEXA = entity.CheckNumber_COLOR_HEXA,
                            IsCheckNumber = entity.IsCheckNumber,

                            CheckIssueDate_INDEX = entity.CheckIssueDate_INDEX,
                            CheckIssueDate_WIDTH = entity.CheckIssueDate_WIDTH,
                            CheckIssueDate_COLOR = entity.CheckIssueDate_COLOR,
                            CheckIssueDate_COLOR_HEXA = entity.CheckIssueDate_COLOR_HEXA,
                            IsCheckIssueDate = entity.IsCheckIssueDate,

                            ARCheckDate = entity.ARCheckDate,
                            ENCheckDate = entity.ENCheckDate,

                            ARCheckNumber = entity.ARCheckNumber,
                            ENCheckNumber = entity.ENCheckNumber,

                            ARCheckIssueDate = entity.ARCheckIssueDate,
                            ENCheckIssueDate = entity.ENCheckIssueDate


                        };
                return q.ToList();
            });
        }

        public Task<ENTRY_GRID_COLUMNS> GetByID(int EntrySettingID)
        {
            return Task.Run<ENTRY_GRID_COLUMNS>(() =>
            {
                var grdCol = entryGrdColRepo.Filter(X => X.ENTRY_SETTING_ID == EntrySettingID).FirstOrDefault();
                return grdCol;
            });
        }

        public List<Entry_Grid_ColumnsVM> GetAll()
        {
            var q = from entity in entryGrdColRepo.GetAll()
                    select new Entry_Grid_ColumnsVM
                    {
                        ENTRY_SETTING_ID = entity.ENTRY_SETTING_ID,
                        ACC_COLOR = entity.ACC_COLOR,
                        ACC_INDEX = entity.ACC_INDEX,
                        ACC_WIDTH = entity.ACC_WIDTH,
                        COST_CENTER_COLOR = entity.COST_CENTER_COLOR,
                        COST_CENTER_INDEX = entity.COST_CENTER_INDEX,
                        COST_CENTER_WIDTH = entity.COST_CENTER_WIDTH,
                        CREDIT_COLOR = entity.CREDIT_COLOR,
                        CREDIT_INDEX = entity.CREDIT_INDEX,
                        CREDIT_WIDTH = entity.CREDIT_WIDTH,
                        DEBIT_COLOR = entity.DEBIT_COLOR,
                        DEBIT_INDEX = entity.DEBIT_INDEX,
                        DEBIT_WIDTH = entity.DEBIT_WIDTH,
                        REMARKS_COLOR = entity.REMARKS_COLOR,
                        REMARKS_INDEX = entity.REMARKS_INDEX,
                        REMARKS_WIDTH = entity.REMARKS_WIDTH,
                        AddedBy = entity.AddedBy,
                        AddedOn = entity.AddedOn,
                        UpdatedBy = entity.UpdatedBy,
                        updatedOn = entity.updatedOn,
                        ACC_COLOR_HEXA = entity.ACC_COLOR_HEXA,
                        COST_CENTER_COLOR_HEXA = entity.COST_CENTER_COLOR_HEXA,
                        CREDIT_COLOR_HEXA = entity.CREDIT_COLOR_HEXA,
                        DEBIT_COLOR_HEXA = entity.DEBIT_COLOR_HEXA,
                        REMARKS_COLOR_HEXA = entity.REMARKS_COLOR_HEXA,

                        IS_CREDIT = entity.IS_CREDIT,
                        IS_DEBIT = entity.IS_DEBIT,
                        IS_ACC = entity.IS_ACC,
                        IS_COST_CENTER = entity.IS_COST_CENTER,
                        IS_REMARKS = entity.IS_REMARKS,
                        IS_TaxValue = entity.IS_TaxValue,
                        IS_Taxable = entity.IS_Taxable,

                        Taxable_INDEX = entity.Taxable_INDEX,
                        Taxable_WIDTH = entity.Taxable_WIDTH,
                        Taxable_COLOR = entity.Taxable_COLOR,
                        Taxable_COLOR_HEXA = entity.Taxable_COLOR_HEXA,

                        TaxValue_INDEX = entity.TaxValue_INDEX,
                        TaxValue_WIDTH = entity.TaxValue_WIDTH,
                        TaxValue_COLOR = entity.TaxValue_COLOR,
                        TaxValue_COLOR_HEXA = entity.TaxValue_COLOR_HEXA,

                        GoldCredit24_INDEX = entity.GoldCredit24_INDEX,
                        GoldCredit24_WIDTH = entity.GoldCredit24_WIDTH,
                        GoldCredit24_COLOR = entity.GoldCredit24_COLOR,
                        GoldCredit24_COLOR_HEXA = entity.GoldCredit24_COLOR_HEXA,
                        IS_GoldCredit24 = entity.IS_GoldCredit24,

                        GoldDepit24_INDEX = entity.GoldDepit24_INDEX,
                        GoldDepit24_WIDTH = entity.GoldDepit24_WIDTH,
                        GoldDepit24_COLOR = entity.GoldDepit24_COLOR,
                        GoldDepit24_COLOR_HEXA = entity.GoldDepit24_COLOR_HEXA,
                        IS_GoldDepit24 = entity.IS_GoldDepit24,


                        GoldCredit22_INDEX = entity.GoldCredit22_INDEX,
                        GoldCredit22_WIDTH = entity.GoldCredit22_WIDTH,
                        GoldCredit22_COLOR = entity.GoldCredit22_COLOR,
                        GoldCredit22_COLOR_HEXA = entity.GoldCredit22_COLOR_HEXA,
                        IS_GoldCredit22 = entity.IS_GoldCredit22,

                        GoldDepit22_INDEX = entity.GoldDepit22_INDEX,
                        GoldDepit22_WIDTH = entity.GoldDepit22_WIDTH,
                        GoldDepit22_COLOR = entity.GoldDepit22_COLOR,
                        GoldDepit22_COLOR_HEXA = entity.GoldDepit22_COLOR_HEXA,
                        IS_GoldDepit22 = entity.IS_GoldDepit22,


                        GoldCredit21_INDEX = entity.GoldCredit21_INDEX,
                        GoldCredit21_WIDTH = entity.GoldCredit21_WIDTH,
                        GoldCredit21_COLOR = entity.GoldCredit21_COLOR,
                        GoldCredit21_COLOR_HEXA = entity.GoldCredit21_COLOR_HEXA,
                        IS_GoldCredit21 = entity.IS_GoldCredit21,

                        GoldDepit21_INDEX = entity.GoldDepit21_INDEX,
                        GoldDepit21_WIDTH = entity.GoldDepit21_WIDTH,
                        GoldDepit21_COLOR = entity.GoldDepit21_COLOR,
                        GoldDepit21_COLOR_HEXA = entity.GoldDepit21_COLOR_HEXA,
                        IS_GoldDepit21 = entity.IS_GoldDepit21,

                        GoldCredit18_INDEX = entity.GoldCredit18_INDEX,
                        GoldCredit18_WIDTH = entity.GoldCredit18_WIDTH,
                        GoldCredit18_COLOR = entity.GoldCredit18_COLOR,
                        GoldCredit18_COLOR_HEXA = entity.GoldCredit18_COLOR_HEXA,
                        IS_GoldCredit18 = entity.IS_GoldCredit18,

                        GoldDepit18_INDEX = entity.GoldDepit18_INDEX,
                        GoldDepit18_WIDTH = entity.GoldDepit18_WIDTH,
                        GoldDepit18_COLOR = entity.GoldDepit18_COLOR,
                        GoldDepit18_COLOR_HEXA = entity.GoldDepit18_COLOR_HEXA,
                        IS_GoldDepit18 = entity.IS_GoldDepit18,

                        TaxRate_INDEX = entity.TaxRate_INDEX,
                        TaxRate_WIDTH = entity.TaxRate_WIDTH,
                        TaxRate_COLOR = entity.TaxRate_COLOR,
                        TaxRate_COLOR_HEXA = entity.TaxRate_COLOR_HEXA,
                        IS_TaxRate = entity.IS_TaxRate,



                        ExemptOfTax_INDEX = entity.ExemptOfTax_INDEX,
                        ExemptOfTax_WIDTH = entity.ExemptOfTax_WIDTH,
                        ExemptOfTax_COLOR = entity.ExemptOfTax_COLOR,
                        ExemptOfTax_COLOR_HEXA = entity.ExemptOfTax_COLOR_HEXA,
                        IsExemptOfTax = entity.IsExemptOfTax,

                        ARExemptOfTax = entity.ARExemptOfTax,
                        ENExemptOfTax = entity.ENExemptOfTax,



                        ARTaxValue = entity.ARTaxValue,
                        ENTaxValue = entity.ENTaxValue,


                        ARCode = entity.ARCode,
                        ENCode = entity.ENCode,
                        ARAccountName = entity.ARAccountName,
                        ENAccountName = entity.ENAccountName,
                        ARCredit = entity.ARCredit,
                        ENCredit = entity.ENCredit,
                        ARDepit = entity.ARDepit,
                        ENDepit = entity.ENDepit,
                        ARCredit24 = entity.ARCredit24,
                        ENCredit24 = entity.ENCredit24,
                        ARDepit24 = entity.ARDepit24,
                        ENDepit24 = entity.ENDepit24,
                        ARCredit22 = entity.ARCredit22,
                        ENCredit22 = entity.ENCredit22,
                        ARDepit22 = entity.ARDepit22,
                        ENDepit22 = entity.ENDepit22,
                        ARCredit21 = entity.ARCredit21,
                        ENCredit21 = entity.ENCredit21,
                        ARDepit21 = entity.ARDepit21,
                        ENDepit21 = entity.ENDepit21,
                        ARCredit18 = entity.ARCredit18,
                        ENCredit18 = entity.ENCredit18,
                        ARDepit18 = entity.ARDepit18,
                        ENDepit18 = entity.ENDepit18,
                        ARCostCenter = entity.ARCostCenter,
                        ENCostCenter = entity.ENCostCenter,
                        ARRemarks = entity.ARRemarks,
                        ENRemarks = entity.ENRemarks,
                        ARTaxable = entity.ARTaxable,
                        EnTaxable = entity.EnTaxable,
                        ARTaxRate = entity.ARTaxRate,
                        ENTaxRate = entity.ENTaxRate,


                        CheckDate_INDEX = entity.CheckDate_INDEX,
                        CheckDate_WIDTH = entity.CheckDate_WIDTH,
                        CheckDate_COLOR = entity.CheckDate_COLOR,
                        CheckDate_COLOR_HEXA = entity.CheckDate_COLOR_HEXA,
                        IsCheckDate = entity.IsCheckDate,

                        CheckNumber_INDEX = entity.CheckNumber_INDEX,
                        CheckNumber_WIDTH = entity.CheckNumber_WIDTH,
                        CheckNumber_COLOR = entity.CheckNumber_COLOR,
                        CheckNumber_COLOR_HEXA = entity.CheckNumber_COLOR_HEXA,
                        IsCheckNumber = entity.IsCheckNumber,

                        CheckIssueDate_INDEX = entity.CheckIssueDate_INDEX,
                        CheckIssueDate_WIDTH = entity.CheckIssueDate_WIDTH,
                        CheckIssueDate_COLOR = entity.CheckIssueDate_COLOR,
                        CheckIssueDate_COLOR_HEXA = entity.CheckIssueDate_COLOR_HEXA,
                        IsCheckIssueDate = entity.IsCheckIssueDate,

                        ARCheckDate = entity.ARCheckDate,
                        ENCheckDate = entity.ENCheckDate,

                        ARCheckNumber = entity.ARCheckNumber,
                        ENCheckNumber = entity.ENCheckNumber,

                        ARCheckIssueDate = entity.ARCheckIssueDate,
                        ENCheckIssueDate = entity.ENCheckIssueDate


                    };
            return q.ToList();
        }

        public bool Insert(Entry_Grid_ColumnsVM entity)
        {
            ENTRY_GRID_COLUMNS egc = new ENTRY_GRID_COLUMNS
            {
                ENTRY_SETTING_ID = entity.ENTRY_SETTING_ID,
                ACC_COLOR = entity.ACC_COLOR,
                ACC_INDEX = entity.ACC_INDEX,
                ACC_WIDTH = entity.ACC_WIDTH,
                COST_CENTER_COLOR = entity.COST_CENTER_COLOR,
                COST_CENTER_INDEX = entity.COST_CENTER_INDEX,
                COST_CENTER_WIDTH = entity.COST_CENTER_WIDTH,
                CREDIT_COLOR = entity.CREDIT_COLOR,
                CREDIT_INDEX = entity.CREDIT_INDEX,
                CREDIT_WIDTH = entity.CREDIT_WIDTH,
                DEBIT_COLOR = entity.DEBIT_COLOR,
                DEBIT_INDEX = entity.DEBIT_INDEX,
                DEBIT_WIDTH = entity.DEBIT_WIDTH,
                REMARKS_COLOR = entity.REMARKS_COLOR,
                REMARKS_INDEX = entity.REMARKS_INDEX,
                REMARKS_WIDTH = entity.REMARKS_WIDTH,
                AddedBy = entity.AddedBy,
                AddedOn = entity.AddedOn,
                UpdatedBy = entity.UpdatedBy,
                updatedOn = entity.updatedOn,
                ACC_COLOR_HEXA = entity.ACC_COLOR_HEXA,
                COST_CENTER_COLOR_HEXA = entity.COST_CENTER_COLOR_HEXA,
                CREDIT_COLOR_HEXA = entity.CREDIT_COLOR_HEXA,
                DEBIT_COLOR_HEXA = entity.DEBIT_COLOR_HEXA,
                REMARKS_COLOR_HEXA = entity.REMARKS_COLOR_HEXA,

                IS_CREDIT = entity.IS_CREDIT,
                IS_DEBIT = entity.IS_DEBIT,
                IS_ACC = entity.IS_ACC,
                IS_COST_CENTER = entity.IS_COST_CENTER,
                IS_REMARKS = entity.IS_REMARKS,
                IS_TaxValue = entity.IS_TaxValue,
                IS_Taxable = entity.IS_Taxable,

                Taxable_INDEX = entity.Taxable_INDEX,
                Taxable_WIDTH = entity.Taxable_WIDTH,
                Taxable_COLOR = entity.Taxable_COLOR,
                Taxable_COLOR_HEXA = entity.Taxable_COLOR_HEXA,

                TaxValue_INDEX = entity.TaxValue_INDEX,
                TaxValue_WIDTH = entity.TaxValue_WIDTH,
                TaxValue_COLOR = entity.TaxValue_COLOR,
                TaxValue_COLOR_HEXA = entity.TaxValue_COLOR_HEXA,

                GoldCredit24_INDEX = entity.GoldCredit24_INDEX,
                GoldCredit24_WIDTH = entity.GoldCredit24_WIDTH,
                GoldCredit24_COLOR = entity.GoldCredit24_COLOR,
                GoldCredit24_COLOR_HEXA = entity.GoldCredit24_COLOR_HEXA,
                IS_GoldCredit24 = entity.IS_GoldCredit24,

                GoldDepit24_INDEX = entity.GoldDepit24_INDEX,
                GoldDepit24_WIDTH = entity.GoldDepit24_WIDTH,
                GoldDepit24_COLOR = entity.GoldDepit24_COLOR,
                GoldDepit24_COLOR_HEXA = entity.GoldDepit24_COLOR_HEXA,
                IS_GoldDepit24 = entity.IS_GoldDepit24,


                GoldCredit22_INDEX = entity.GoldCredit22_INDEX,
                GoldCredit22_WIDTH = entity.GoldCredit22_WIDTH,
                GoldCredit22_COLOR = entity.GoldCredit22_COLOR,
                GoldCredit22_COLOR_HEXA = entity.GoldCredit22_COLOR_HEXA,
                IS_GoldCredit22 = entity.IS_GoldCredit22,

                GoldDepit22_INDEX = entity.GoldDepit22_INDEX,
                GoldDepit22_WIDTH = entity.GoldDepit22_WIDTH,
                GoldDepit22_COLOR = entity.GoldDepit22_COLOR,
                GoldDepit22_COLOR_HEXA = entity.GoldDepit22_COLOR_HEXA,
                IS_GoldDepit22 = entity.IS_GoldDepit22,


                GoldCredit21_INDEX = entity.GoldCredit21_INDEX,
                GoldCredit21_WIDTH = entity.GoldCredit21_WIDTH,
                GoldCredit21_COLOR = entity.GoldCredit21_COLOR,
                GoldCredit21_COLOR_HEXA = entity.GoldCredit21_COLOR_HEXA,
                IS_GoldCredit21 = entity.IS_GoldCredit21,

                GoldDepit21_INDEX = entity.GoldDepit21_INDEX,
                GoldDepit21_WIDTH = entity.GoldDepit21_WIDTH,
                GoldDepit21_COLOR = entity.GoldDepit21_COLOR,
                GoldDepit21_COLOR_HEXA = entity.GoldDepit21_COLOR_HEXA,
                IS_GoldDepit21 = entity.IS_GoldDepit21,

                GoldCredit18_INDEX = entity.GoldCredit18_INDEX,
                GoldCredit18_WIDTH = entity.GoldCredit18_WIDTH,
                GoldCredit18_COLOR = entity.GoldCredit18_COLOR,
                GoldCredit18_COLOR_HEXA = entity.GoldCredit18_COLOR_HEXA,
                IS_GoldCredit18 = entity.IS_GoldCredit18,

                GoldDepit18_INDEX = entity.GoldDepit18_INDEX,
                GoldDepit18_WIDTH = entity.GoldDepit18_WIDTH,
                GoldDepit18_COLOR = entity.GoldDepit18_COLOR,
                GoldDepit18_COLOR_HEXA = entity.GoldDepit18_COLOR_HEXA,
                IS_GoldDepit18 = entity.IS_GoldDepit18,

                TaxRate_INDEX = entity.TaxRate_INDEX,
                TaxRate_WIDTH = entity.TaxRate_WIDTH,
                TaxRate_COLOR = entity.TaxRate_COLOR,
                TaxRate_COLOR_HEXA = entity.TaxRate_COLOR_HEXA,
                IS_TaxRate = entity.IS_TaxRate,




                ExemptOfTax_INDEX = entity.ExemptOfTax_INDEX,
                ExemptOfTax_WIDTH = entity.ExemptOfTax_WIDTH,
                ExemptOfTax_COLOR = entity.ExemptOfTax_COLOR,
                ExemptOfTax_COLOR_HEXA = entity.ExemptOfTax_COLOR_HEXA,
                IsExemptOfTax = entity.IsExemptOfTax,

                ARExemptOfTax = entity.ARExemptOfTax,
                ENExemptOfTax = entity.ENExemptOfTax,



                ARTaxValue = entity.ARTaxValue,
                ENTaxValue = entity.ENTaxValue,

                ARCode = entity.ARCode,
                ENCode = entity.ENCode,
                ARAccountName = entity.ARAccountName,
                ENAccountName = entity.ENAccountName,
                ARCredit = entity.ARCredit,
                ENCredit = entity.ENCredit,
                ARDepit = entity.ARDepit,
                ENDepit = entity.ENDepit,
                ARCredit24 = entity.ARCredit24,
                ENCredit24 = entity.ENCredit24,
                ARDepit24 = entity.ARDepit24,
                ENDepit24 = entity.ENDepit24,
                ARCredit22 = entity.ARCredit22,
                ENCredit22 = entity.ENCredit22,
                ARDepit22 = entity.ARDepit22,
                ENDepit22 = entity.ENDepit22,
                ARCredit21 = entity.ARCredit21,
                ENCredit21 = entity.ENCredit21,
                ARDepit21 = entity.ARDepit21,
                ENDepit21 = entity.ENDepit21,
                ARCredit18 = entity.ARCredit18,
                ENCredit18 = entity.ENCredit18,
                ARDepit18 = entity.ARDepit18,
                ENDepit18 = entity.ENDepit18,
                ARCostCenter = entity.ARCostCenter,
                ENCostCenter = entity.ENCostCenter,
                ARRemarks = entity.ARRemarks,
                ENRemarks = entity.ENRemarks,
                ARTaxable = entity.ARTaxable,
                EnTaxable = entity.EnTaxable,
                ARTaxRate = entity.ARTaxRate,
                ENTaxRate = entity.ENTaxRate,

                CheckDate_INDEX = entity.CheckDate_INDEX,
                CheckDate_WIDTH = entity.CheckDate_WIDTH,
                CheckDate_COLOR = entity.CheckDate_COLOR,
                CheckDate_COLOR_HEXA = entity.CheckDate_COLOR_HEXA,
                IsCheckDate = entity.IsCheckDate,

                CheckNumber_INDEX = entity.CheckNumber_INDEX,
                CheckNumber_WIDTH = entity.CheckNumber_WIDTH,
                CheckNumber_COLOR = entity.CheckNumber_COLOR,
                CheckNumber_COLOR_HEXA = entity.CheckNumber_COLOR_HEXA,
                IsCheckNumber = entity.IsCheckNumber,

                CheckIssueDate_INDEX = entity.CheckIssueDate_INDEX,
                CheckIssueDate_WIDTH = entity.CheckIssueDate_WIDTH,
                CheckIssueDate_COLOR = entity.CheckIssueDate_COLOR,
                CheckIssueDate_COLOR_HEXA = entity.CheckIssueDate_COLOR_HEXA,
                IsCheckIssueDate = entity.IsCheckIssueDate,

                ARCheckDate = entity.ARCheckDate,
                ENCheckDate = entity.ENCheckDate,

                ARCheckNumber = entity.ARCheckNumber,
                ENCheckNumber = entity.ENCheckNumber,

                ARCheckIssueDate = entity.ARCheckIssueDate,
                ENCheckIssueDate = entity.ENCheckIssueDate


            };
            entryGrdColRepo.Add(egc);
            return true;
        }

        public Task<int> InsertAsync(Entry_Grid_ColumnsVM entity)
        {
            return Task.Run<int>(() =>
            {
                ENTRY_GRID_COLUMNS egc = new ENTRY_GRID_COLUMNS
                {
                    ENTRY_SETTING_ID = entity.ENTRY_SETTING_ID,
                    ACC_COLOR = entity.ACC_COLOR,
                    ACC_INDEX = entity.ACC_INDEX,
                    ACC_WIDTH = entity.ACC_WIDTH,
                    COST_CENTER_COLOR = entity.COST_CENTER_COLOR,
                    COST_CENTER_INDEX = entity.COST_CENTER_INDEX,
                    COST_CENTER_WIDTH = entity.COST_CENTER_WIDTH,
                    CREDIT_COLOR = entity.CREDIT_COLOR,
                    CREDIT_INDEX = entity.CREDIT_INDEX,
                    CREDIT_WIDTH = entity.CREDIT_WIDTH,
                    DEBIT_COLOR = entity.DEBIT_COLOR,
                    DEBIT_INDEX = entity.DEBIT_INDEX,
                    DEBIT_WIDTH = entity.DEBIT_WIDTH,
                    REMARKS_COLOR = entity.REMARKS_COLOR,
                    REMARKS_INDEX = entity.REMARKS_INDEX,
                    REMARKS_WIDTH = entity.REMARKS_WIDTH,
                    AddedBy = entity.AddedBy,
                    AddedOn = entity.AddedOn,
                    UpdatedBy = entity.UpdatedBy,
                    updatedOn = entity.updatedOn,
                    ACC_COLOR_HEXA = entity.ACC_COLOR_HEXA,
                    COST_CENTER_COLOR_HEXA = entity.COST_CENTER_COLOR_HEXA,
                    CREDIT_COLOR_HEXA = entity.CREDIT_COLOR_HEXA,
                    DEBIT_COLOR_HEXA = entity.DEBIT_COLOR_HEXA,
                    REMARKS_COLOR_HEXA = entity.REMARKS_COLOR_HEXA,

                    IS_CREDIT = entity.IS_CREDIT,
                    IS_DEBIT = entity.IS_DEBIT,
                    IS_ACC = entity.IS_ACC,
                    IS_COST_CENTER = entity.IS_COST_CENTER,
                    IS_REMARKS = entity.IS_REMARKS,
                    IS_TaxValue = entity.IS_TaxValue,
                    IS_Taxable = entity.IS_Taxable,

                    Taxable_INDEX = entity.Taxable_INDEX,
                    Taxable_WIDTH = entity.Taxable_WIDTH,
                    Taxable_COLOR = entity.Taxable_COLOR,
                    Taxable_COLOR_HEXA = entity.Taxable_COLOR_HEXA,

                    TaxValue_INDEX = entity.TaxValue_INDEX,
                    TaxValue_WIDTH = entity.TaxValue_WIDTH,
                    TaxValue_COLOR = entity.TaxValue_COLOR,
                    TaxValue_COLOR_HEXA = entity.TaxValue_COLOR_HEXA,

                    GoldCredit24_INDEX = entity.GoldCredit24_INDEX,
                    GoldCredit24_WIDTH = entity.GoldCredit24_WIDTH,
                    GoldCredit24_COLOR = entity.GoldCredit24_COLOR,
                    GoldCredit24_COLOR_HEXA = entity.GoldCredit24_COLOR_HEXA,
                    IS_GoldCredit24 = entity.IS_GoldCredit24,

                    GoldDepit24_INDEX = entity.GoldDepit24_INDEX,
                    GoldDepit24_WIDTH = entity.GoldDepit24_WIDTH,
                    GoldDepit24_COLOR = entity.GoldDepit24_COLOR,
                    GoldDepit24_COLOR_HEXA = entity.GoldDepit24_COLOR_HEXA,
                    IS_GoldDepit24 = entity.IS_GoldDepit24,


                    GoldCredit22_INDEX = entity.GoldCredit22_INDEX,
                    GoldCredit22_WIDTH = entity.GoldCredit22_WIDTH,
                    GoldCredit22_COLOR = entity.GoldCredit22_COLOR,
                    GoldCredit22_COLOR_HEXA = entity.GoldCredit22_COLOR_HEXA,
                    IS_GoldCredit22 = entity.IS_GoldCredit22,

                    GoldDepit22_INDEX = entity.GoldDepit22_INDEX,
                    GoldDepit22_WIDTH = entity.GoldDepit22_WIDTH,
                    GoldDepit22_COLOR = entity.GoldDepit22_COLOR,
                    GoldDepit22_COLOR_HEXA = entity.GoldDepit22_COLOR_HEXA,
                    IS_GoldDepit22 = entity.IS_GoldDepit22,


                    GoldCredit21_INDEX = entity.GoldCredit21_INDEX,
                    GoldCredit21_WIDTH = entity.GoldCredit21_WIDTH,
                    GoldCredit21_COLOR = entity.GoldCredit21_COLOR,
                    GoldCredit21_COLOR_HEXA = entity.GoldCredit21_COLOR_HEXA,
                    IS_GoldCredit21 = entity.IS_GoldCredit21,

                    GoldDepit21_INDEX = entity.GoldDepit21_INDEX,
                    GoldDepit21_WIDTH = entity.GoldDepit21_WIDTH,
                    GoldDepit21_COLOR = entity.GoldDepit21_COLOR,
                    GoldDepit21_COLOR_HEXA = entity.GoldDepit21_COLOR_HEXA,
                    IS_GoldDepit21 = entity.IS_GoldDepit21,

                    GoldCredit18_INDEX = entity.GoldCredit18_INDEX,
                    GoldCredit18_WIDTH = entity.GoldCredit18_WIDTH,
                    GoldCredit18_COLOR = entity.GoldCredit18_COLOR,
                    GoldCredit18_COLOR_HEXA = entity.GoldCredit18_COLOR_HEXA,
                    IS_GoldCredit18 = entity.IS_GoldCredit18,

                    GoldDepit18_INDEX = entity.GoldDepit18_INDEX,
                    GoldDepit18_WIDTH = entity.GoldDepit18_WIDTH,
                    GoldDepit18_COLOR = entity.GoldDepit18_COLOR,
                    GoldDepit18_COLOR_HEXA = entity.GoldDepit18_COLOR_HEXA,
                    IS_GoldDepit18 = entity.IS_GoldDepit18,

                    TaxRate_INDEX = entity.TaxRate_INDEX,
                    TaxRate_WIDTH = entity.TaxRate_WIDTH,
                    TaxRate_COLOR = entity.TaxRate_COLOR,
                    TaxRate_COLOR_HEXA = entity.TaxRate_COLOR_HEXA,
                    IS_TaxRate = entity.IS_TaxRate,




                    ExemptOfTax_INDEX = entity.ExemptOfTax_INDEX,
                    ExemptOfTax_WIDTH = entity.ExemptOfTax_WIDTH,
                    ExemptOfTax_COLOR = entity.ExemptOfTax_COLOR,
                    ExemptOfTax_COLOR_HEXA = entity.ExemptOfTax_COLOR_HEXA,
                    IsExemptOfTax = entity.IsExemptOfTax,

                    ARExemptOfTax = entity.ARExemptOfTax,
                    ENExemptOfTax = entity.ENExemptOfTax,



                    ARTaxValue = entity.ARTaxValue,
                    ENTaxValue = entity.ENTaxValue,

                    ARCode = entity.ARCode,
                    ENCode = entity.ENCode,
                    ARAccountName = entity.ARAccountName,
                    ENAccountName = entity.ENAccountName,
                    ARCredit = entity.ARCredit,
                    ENCredit = entity.ENCredit,
                    ARDepit = entity.ARDepit,
                    ENDepit = entity.ENDepit,
                    ARCredit24 = entity.ARCredit24,
                    ENCredit24 = entity.ENCredit24,
                    ARDepit24 = entity.ARDepit24,
                    ENDepit24 = entity.ENDepit24,
                    ARCredit22 = entity.ARCredit22,
                    ENCredit22 = entity.ENCredit22,
                    ARDepit22 = entity.ARDepit22,
                    ENDepit22 = entity.ENDepit22,
                    ARCredit21 = entity.ARCredit21,
                    ENCredit21 = entity.ENCredit21,
                    ARDepit21 = entity.ARDepit21,
                    ENDepit21 = entity.ENDepit21,
                    ARCredit18 = entity.ARCredit18,
                    ENCredit18 = entity.ENCredit18,
                    ARDepit18 = entity.ARDepit18,
                    ENDepit18 = entity.ENDepit18,
                    ARCostCenter = entity.ARCostCenter,
                    ENCostCenter = entity.ENCostCenter,
                    ARRemarks = entity.ARRemarks,
                    ENRemarks = entity.ENRemarks,
                    ARTaxable = entity.ARTaxable,
                    EnTaxable = entity.EnTaxable,
                    ARTaxRate = entity.ARTaxRate,
                    ENTaxRate = entity.ENTaxRate,

                    CheckDate_INDEX = entity.CheckDate_INDEX,
                    CheckDate_WIDTH = entity.CheckDate_WIDTH,
                    CheckDate_COLOR = entity.CheckDate_COLOR,
                    CheckDate_COLOR_HEXA = entity.CheckDate_COLOR_HEXA,
                    IsCheckDate = entity.IsCheckDate,

                    CheckNumber_INDEX = entity.CheckNumber_INDEX,
                    CheckNumber_WIDTH = entity.CheckNumber_WIDTH,
                    CheckNumber_COLOR = entity.CheckNumber_COLOR,
                    CheckNumber_COLOR_HEXA = entity.CheckNumber_COLOR_HEXA,
                    IsCheckNumber = entity.IsCheckNumber,

                    CheckIssueDate_INDEX = entity.CheckIssueDate_INDEX,
                    CheckIssueDate_WIDTH = entity.CheckIssueDate_WIDTH,
                    CheckIssueDate_COLOR = entity.CheckIssueDate_COLOR,
                    CheckIssueDate_COLOR_HEXA = entity.CheckIssueDate_COLOR_HEXA,
                    IsCheckIssueDate = entity.IsCheckIssueDate,

                    ARCheckDate = entity.ARCheckDate,
                    ENCheckDate = entity.ENCheckDate,

                    ARCheckNumber = entity.ARCheckNumber,
                    ENCheckNumber = entity.ENCheckNumber,

                    ARCheckIssueDate = entity.ARCheckIssueDate,
                    ENCheckIssueDate = entity.ENCheckIssueDate


                };
                entryGrdColRepo.Add(egc);
                // return true;
                return egc.ENTRY_SETTING_ID;
            });
        }

        public bool Update(Entry_Grid_ColumnsVM entity)
        {
            ENTRY_GRID_COLUMNS egc = new ENTRY_GRID_COLUMNS
            {
                ENTRY_SETTING_ID = entity.ENTRY_SETTING_ID,
                ACC_COLOR = entity.ACC_COLOR,
                ACC_INDEX = entity.ACC_INDEX,
                ACC_WIDTH = entity.ACC_WIDTH,
                COST_CENTER_COLOR = entity.COST_CENTER_COLOR,
                COST_CENTER_INDEX = entity.COST_CENTER_INDEX,
                COST_CENTER_WIDTH = entity.COST_CENTER_WIDTH,
                CREDIT_COLOR = entity.CREDIT_COLOR,
                CREDIT_INDEX = entity.CREDIT_INDEX,
                CREDIT_WIDTH = entity.CREDIT_WIDTH,
                DEBIT_COLOR = entity.DEBIT_COLOR,
                DEBIT_INDEX = entity.DEBIT_INDEX,
                DEBIT_WIDTH = entity.DEBIT_WIDTH,
                REMARKS_COLOR = entity.REMARKS_COLOR,
                REMARKS_INDEX = entity.REMARKS_INDEX,
                REMARKS_WIDTH = entity.REMARKS_WIDTH,
                AddedBy = entity.AddedBy,
                AddedOn = entity.AddedOn,
                UpdatedBy = entity.UpdatedBy,
                updatedOn = entity.updatedOn,
                ACC_COLOR_HEXA = entity.ACC_COLOR_HEXA,
                COST_CENTER_COLOR_HEXA = entity.COST_CENTER_COLOR_HEXA,
                CREDIT_COLOR_HEXA = entity.CREDIT_COLOR_HEXA,
                DEBIT_COLOR_HEXA = entity.DEBIT_COLOR_HEXA,
                REMARKS_COLOR_HEXA = entity.REMARKS_COLOR_HEXA,

                IS_CREDIT = entity.IS_CREDIT,
                IS_DEBIT = entity.IS_DEBIT,
                IS_ACC = entity.IS_ACC,
                IS_COST_CENTER = entity.IS_COST_CENTER,
                IS_REMARKS = entity.IS_REMARKS,
                IS_TaxValue = entity.IS_TaxValue,
                IS_Taxable = entity.IS_Taxable,

                Taxable_INDEX = entity.Taxable_INDEX,
                Taxable_WIDTH = entity.Taxable_WIDTH,
                Taxable_COLOR = entity.Taxable_COLOR,
                Taxable_COLOR_HEXA = entity.Taxable_COLOR_HEXA,

                TaxValue_INDEX = entity.TaxValue_INDEX,
                TaxValue_WIDTH = entity.TaxValue_WIDTH,
                TaxValue_COLOR = entity.TaxValue_COLOR,
                TaxValue_COLOR_HEXA = entity.TaxValue_COLOR_HEXA,

                GoldCredit24_INDEX = entity.GoldCredit24_INDEX,
                GoldCredit24_WIDTH = entity.GoldCredit24_WIDTH,
                GoldCredit24_COLOR = entity.GoldCredit24_COLOR,
                GoldCredit24_COLOR_HEXA = entity.GoldCredit24_COLOR_HEXA,
                IS_GoldCredit24 = entity.IS_GoldCredit24,

                GoldDepit24_INDEX = entity.GoldDepit24_INDEX,
                GoldDepit24_WIDTH = entity.GoldDepit24_WIDTH,
                GoldDepit24_COLOR = entity.GoldDepit24_COLOR,
                GoldDepit24_COLOR_HEXA = entity.GoldDepit24_COLOR_HEXA,
                IS_GoldDepit24 = entity.IS_GoldDepit24,


                GoldCredit22_INDEX = entity.GoldCredit22_INDEX,
                GoldCredit22_WIDTH = entity.GoldCredit22_WIDTH,
                GoldCredit22_COLOR = entity.GoldCredit22_COLOR,
                GoldCredit22_COLOR_HEXA = entity.GoldCredit22_COLOR_HEXA,
                IS_GoldCredit22 = entity.IS_GoldCredit22,

                GoldDepit22_INDEX = entity.GoldDepit22_INDEX,
                GoldDepit22_WIDTH = entity.GoldDepit22_WIDTH,
                GoldDepit22_COLOR = entity.GoldDepit22_COLOR,
                GoldDepit22_COLOR_HEXA = entity.GoldDepit22_COLOR_HEXA,
                IS_GoldDepit22 = entity.IS_GoldDepit22,


                GoldCredit21_INDEX = entity.GoldCredit21_INDEX,
                GoldCredit21_WIDTH = entity.GoldCredit21_WIDTH,
                GoldCredit21_COLOR = entity.GoldCredit21_COLOR,
                GoldCredit21_COLOR_HEXA = entity.GoldCredit21_COLOR_HEXA,
                IS_GoldCredit21 = entity.IS_GoldCredit21,

                GoldDepit21_INDEX = entity.GoldDepit21_INDEX,
                GoldDepit21_WIDTH = entity.GoldDepit21_WIDTH,
                GoldDepit21_COLOR = entity.GoldDepit21_COLOR,
                GoldDepit21_COLOR_HEXA = entity.GoldDepit21_COLOR_HEXA,
                IS_GoldDepit21 = entity.IS_GoldDepit21,

                GoldCredit18_INDEX = entity.GoldCredit18_INDEX,
                GoldCredit18_WIDTH = entity.GoldCredit18_WIDTH,
                GoldCredit18_COLOR = entity.GoldCredit18_COLOR,
                GoldCredit18_COLOR_HEXA = entity.GoldCredit18_COLOR_HEXA,
                IS_GoldCredit18 = entity.IS_GoldCredit18,

                GoldDepit18_INDEX = entity.GoldDepit18_INDEX,
                GoldDepit18_WIDTH = entity.GoldDepit18_WIDTH,
                GoldDepit18_COLOR = entity.GoldDepit18_COLOR,
                GoldDepit18_COLOR_HEXA = entity.GoldDepit18_COLOR_HEXA,
                IS_GoldDepit18 = entity.IS_GoldDepit18,

                TaxRate_INDEX = entity.TaxRate_INDEX,
                TaxRate_WIDTH = entity.TaxRate_WIDTH,
                TaxRate_COLOR = entity.TaxRate_COLOR,
                TaxRate_COLOR_HEXA = entity.TaxRate_COLOR_HEXA,
                IS_TaxRate = entity.IS_TaxRate,



                ExemptOfTax_INDEX = entity.ExemptOfTax_INDEX,
                ExemptOfTax_WIDTH = entity.ExemptOfTax_WIDTH,
                ExemptOfTax_COLOR = entity.ExemptOfTax_COLOR,
                ExemptOfTax_COLOR_HEXA = entity.ExemptOfTax_COLOR_HEXA,
                IsExemptOfTax = entity.IsExemptOfTax,

                ARExemptOfTax = entity.ARExemptOfTax,
                ENExemptOfTax = entity.ENExemptOfTax,



                ARTaxValue = entity.ARTaxValue,
                ENTaxValue = entity.ENTaxValue,

                ARCode = entity.ARCode,
                ENCode = entity.ENCode,
                ARAccountName = entity.ARAccountName,
                ENAccountName = entity.ENAccountName,
                ARCredit = entity.ARCredit,
                ENCredit = entity.ENCredit,
                ARDepit = entity.ARDepit,
                ENDepit = entity.ENDepit,
                ARCredit24 = entity.ARCredit24,
                ENCredit24 = entity.ENCredit24,
                ARDepit24 = entity.ARDepit24,
                ENDepit24 = entity.ENDepit24,
                ARCredit22 = entity.ARCredit22,
                ENCredit22 = entity.ENCredit22,
                ARDepit22 = entity.ARDepit22,
                ENDepit22 = entity.ENDepit22,
                ARCredit21 = entity.ARCredit21,
                ENCredit21 = entity.ENCredit21,
                ARDepit21 = entity.ARDepit21,
                ENDepit21 = entity.ENDepit21,
                ARCredit18 = entity.ARCredit18,
                ENCredit18 = entity.ENCredit18,
                ARDepit18 = entity.ARDepit18,
                ENDepit18 = entity.ENDepit18,
                ARCostCenter = entity.ARCostCenter,
                ENCostCenter = entity.ENCostCenter,
                ARRemarks = entity.ARRemarks,
                ENRemarks = entity.ENRemarks,
                ARTaxable = entity.ARTaxable,
                EnTaxable = entity.EnTaxable,
                ARTaxRate = entity.ARTaxRate,
                ENTaxRate = entity.ENTaxRate,

                CheckDate_INDEX = entity.CheckDate_INDEX,
                CheckDate_WIDTH = entity.CheckDate_WIDTH,
                CheckDate_COLOR = entity.CheckDate_COLOR,
                CheckDate_COLOR_HEXA = entity.CheckDate_COLOR_HEXA,
                IsCheckDate = entity.IsCheckDate,

                CheckNumber_INDEX = entity.CheckNumber_INDEX,
                CheckNumber_WIDTH = entity.CheckNumber_WIDTH,
                CheckNumber_COLOR = entity.CheckNumber_COLOR,
                CheckNumber_COLOR_HEXA = entity.CheckNumber_COLOR_HEXA,
                IsCheckNumber = entity.IsCheckNumber,

                CheckIssueDate_INDEX = entity.CheckIssueDate_INDEX,
                CheckIssueDate_WIDTH = entity.CheckIssueDate_WIDTH,
                CheckIssueDate_COLOR = entity.CheckIssueDate_COLOR,
                CheckIssueDate_COLOR_HEXA = entity.CheckIssueDate_COLOR_HEXA,
                IsCheckIssueDate = entity.IsCheckIssueDate,

                ARCheckDate = entity.ARCheckDate,
                ENCheckDate = entity.ENCheckDate,

                ARCheckNumber = entity.ARCheckNumber,
                ENCheckNumber = entity.ENCheckNumber,

                ARCheckIssueDate = entity.ARCheckIssueDate,
                ENCheckIssueDate = entity.ENCheckIssueDate


            };
            entryGrdColRepo.Update(egc, egc.ENTRY_SETTING_ID);
            return true;
        }

        public Task<bool> UpdateAsync(Entry_Grid_ColumnsVM entity)
        {
            return Task.Run<bool>(() =>
            {
                var entryGrid = entryGrdColRepo.Filter(e => e.ENTRY_SETTING_ID == entity.ENTRY_SETTING_ID).FirstOrDefault();
                if (entryGrid != null)
                {
                    ENTRY_GRID_COLUMNS egc = new ENTRY_GRID_COLUMNS
                    {
                        ENTRY_SETTING_ID = entity.ENTRY_SETTING_ID,
                        ACC_COLOR = entity.ACC_COLOR,
                        ACC_INDEX = entity.ACC_INDEX,
                        ACC_WIDTH = entity.ACC_WIDTH,
                        COST_CENTER_COLOR = entity.COST_CENTER_COLOR,
                        COST_CENTER_INDEX = entity.COST_CENTER_INDEX,
                        COST_CENTER_WIDTH = entity.COST_CENTER_WIDTH,
                        CREDIT_COLOR = entity.CREDIT_COLOR,
                        CREDIT_INDEX = entity.CREDIT_INDEX,
                        CREDIT_WIDTH = entity.CREDIT_WIDTH,
                        DEBIT_COLOR = entity.DEBIT_COLOR,
                        DEBIT_INDEX = entity.DEBIT_INDEX,
                        DEBIT_WIDTH = entity.DEBIT_WIDTH,
                        REMARKS_COLOR = entity.REMARKS_COLOR,
                        REMARKS_INDEX = entity.REMARKS_INDEX,
                        REMARKS_WIDTH = entity.REMARKS_WIDTH,
                        AddedBy = entity.AddedBy,
                        AddedOn = entity.AddedOn,
                        UpdatedBy = entity.UpdatedBy,
                        updatedOn = entity.updatedOn,
                        ACC_COLOR_HEXA = entity.ACC_COLOR_HEXA,
                        COST_CENTER_COLOR_HEXA = entity.COST_CENTER_COLOR_HEXA,
                        CREDIT_COLOR_HEXA = entity.CREDIT_COLOR_HEXA,
                        DEBIT_COLOR_HEXA = entity.DEBIT_COLOR_HEXA,
                        REMARKS_COLOR_HEXA = entity.REMARKS_COLOR_HEXA,

                        IS_CREDIT = entity.IS_CREDIT,
                        IS_DEBIT = entity.IS_DEBIT,
                        IS_ACC = entity.IS_ACC,
                        IS_COST_CENTER = entity.IS_COST_CENTER,
                        IS_REMARKS = entity.IS_REMARKS,
                        IS_TaxValue = entity.IS_TaxValue,
                        IS_Taxable = entity.IS_Taxable,

                        Taxable_INDEX = entity.Taxable_INDEX,
                        Taxable_WIDTH = entity.Taxable_WIDTH,
                        Taxable_COLOR = entity.Taxable_COLOR,
                        Taxable_COLOR_HEXA = entity.Taxable_COLOR_HEXA,

                        TaxValue_INDEX = entity.TaxValue_INDEX,
                        TaxValue_WIDTH = entity.TaxValue_WIDTH,
                        TaxValue_COLOR = entity.TaxValue_COLOR,
                        TaxValue_COLOR_HEXA = entity.TaxValue_COLOR_HEXA,

                        GoldCredit24_INDEX = entity.GoldCredit24_INDEX,
                        GoldCredit24_WIDTH = entity.GoldCredit24_WIDTH,
                        GoldCredit24_COLOR = entity.GoldCredit24_COLOR,
                        GoldCredit24_COLOR_HEXA = entity.GoldCredit24_COLOR_HEXA,
                        IS_GoldCredit24 = entity.IS_GoldCredit24,

                        GoldDepit24_INDEX = entity.GoldDepit24_INDEX,
                        GoldDepit24_WIDTH = entity.GoldDepit24_WIDTH,
                        GoldDepit24_COLOR = entity.GoldDepit24_COLOR,
                        GoldDepit24_COLOR_HEXA = entity.GoldDepit24_COLOR_HEXA,
                        IS_GoldDepit24 = entity.IS_GoldDepit24,


                        GoldCredit22_INDEX = entity.GoldCredit22_INDEX,
                        GoldCredit22_WIDTH = entity.GoldCredit22_WIDTH,
                        GoldCredit22_COLOR = entity.GoldCredit22_COLOR,
                        GoldCredit22_COLOR_HEXA = entity.GoldCredit22_COLOR_HEXA,
                        IS_GoldCredit22 = entity.IS_GoldCredit22,

                        GoldDepit22_INDEX = entity.GoldDepit22_INDEX,
                        GoldDepit22_WIDTH = entity.GoldDepit22_WIDTH,
                        GoldDepit22_COLOR = entity.GoldDepit22_COLOR,
                        GoldDepit22_COLOR_HEXA = entity.GoldDepit22_COLOR_HEXA,
                        IS_GoldDepit22 = entity.IS_GoldDepit22,


                        GoldCredit21_INDEX = entity.GoldCredit21_INDEX,
                        GoldCredit21_WIDTH = entity.GoldCredit21_WIDTH,
                        GoldCredit21_COLOR = entity.GoldCredit21_COLOR,
                        GoldCredit21_COLOR_HEXA = entity.GoldCredit21_COLOR_HEXA,
                        IS_GoldCredit21 = entity.IS_GoldCredit21,

                        GoldDepit21_INDEX = entity.GoldDepit21_INDEX,
                        GoldDepit21_WIDTH = entity.GoldDepit21_WIDTH,
                        GoldDepit21_COLOR = entity.GoldDepit21_COLOR,
                        GoldDepit21_COLOR_HEXA = entity.GoldDepit21_COLOR_HEXA,
                        IS_GoldDepit21 = entity.IS_GoldDepit21,

                        GoldCredit18_INDEX = entity.GoldCredit18_INDEX,
                        GoldCredit18_WIDTH = entity.GoldCredit18_WIDTH,
                        GoldCredit18_COLOR = entity.GoldCredit18_COLOR,
                        GoldCredit18_COLOR_HEXA = entity.GoldCredit18_COLOR_HEXA,
                        IS_GoldCredit18 = entity.IS_GoldCredit18,

                        GoldDepit18_INDEX = entity.GoldDepit18_INDEX,
                        GoldDepit18_WIDTH = entity.GoldDepit18_WIDTH,
                        GoldDepit18_COLOR = entity.GoldDepit18_COLOR,
                        GoldDepit18_COLOR_HEXA = entity.GoldDepit18_COLOR_HEXA,
                        IS_GoldDepit18 = entity.IS_GoldDepit18,

                        TaxRate_INDEX = entity.TaxRate_INDEX,
                        TaxRate_WIDTH = entity.TaxRate_WIDTH,
                        TaxRate_COLOR = entity.TaxRate_COLOR,
                        TaxRate_COLOR_HEXA = entity.TaxRate_COLOR_HEXA,
                        IS_TaxRate = entity.IS_TaxRate,


                        ExemptOfTax_INDEX = entity.ExemptOfTax_INDEX,
                        ExemptOfTax_WIDTH = entity.ExemptOfTax_WIDTH,
                        ExemptOfTax_COLOR = entity.ExemptOfTax_COLOR,
                        ExemptOfTax_COLOR_HEXA = entity.ExemptOfTax_COLOR_HEXA,
                        IsExemptOfTax = entity.IsExemptOfTax,

                        ARExemptOfTax = entity.ARExemptOfTax,
                        ENExemptOfTax = entity.ENExemptOfTax,



                        ARTaxValue = entity.ARTaxValue,
                        ENTaxValue = entity.ENTaxValue,

                        ARCode = entity.ARCode,
                        ENCode = entity.ENCode,
                        ARAccountName = entity.ARAccountName,
                        ENAccountName = entity.ENAccountName,
                        ARCredit = entity.ARCredit,
                        ENCredit = entity.ENCredit,
                        ARDepit = entity.ARDepit,
                        ENDepit = entity.ENDepit,
                        ARCredit24 = entity.ARCredit24,
                        ENCredit24 = entity.ENCredit24,
                        ARDepit24 = entity.ARDepit24,
                        ENDepit24 = entity.ENDepit24,
                        ARCredit22 = entity.ARCredit22,
                        ENCredit22 = entity.ENCredit22,
                        ARDepit22 = entity.ARDepit22,
                        ENDepit22 = entity.ENDepit22,
                        ARCredit21 = entity.ARCredit21,
                        ENCredit21 = entity.ENCredit21,
                        ARDepit21 = entity.ARDepit21,
                        ENDepit21 = entity.ENDepit21,
                        ARCredit18 = entity.ARCredit18,
                        ENCredit18 = entity.ENCredit18,
                        ARDepit18 = entity.ARDepit18,
                        ENDepit18 = entity.ENDepit18,
                        ARCostCenter = entity.ARCostCenter,
                        ENCostCenter = entity.ENCostCenter,
                        ARRemarks = entity.ARRemarks,
                        ENRemarks = entity.ENRemarks,
                        ARTaxable = entity.ARTaxable,
                        EnTaxable = entity.EnTaxable,
                        ARTaxRate = entity.ARTaxRate,
                        ENTaxRate = entity.ENTaxRate,


                        CheckDate_INDEX = entity.CheckDate_INDEX,
                        CheckDate_WIDTH = entity.CheckDate_WIDTH,
                        CheckDate_COLOR = entity.CheckDate_COLOR,
                        CheckDate_COLOR_HEXA = entity.CheckDate_COLOR_HEXA,
                        IsCheckDate = entity.IsCheckDate,

                        CheckNumber_INDEX = entity.CheckNumber_INDEX,
                        CheckNumber_WIDTH = entity.CheckNumber_WIDTH,
                        CheckNumber_COLOR = entity.CheckNumber_COLOR,
                        CheckNumber_COLOR_HEXA = entity.CheckNumber_COLOR_HEXA,
                        IsCheckNumber = entity.IsCheckNumber,

                        CheckIssueDate_INDEX = entity.CheckIssueDate_INDEX,
                        CheckIssueDate_WIDTH = entity.CheckIssueDate_WIDTH,
                        CheckIssueDate_COLOR = entity.CheckIssueDate_COLOR,
                        CheckIssueDate_COLOR_HEXA = entity.CheckIssueDate_COLOR_HEXA,
                        IsCheckIssueDate = entity.IsCheckIssueDate,

                        ARCheckDate = entity.ARCheckDate,
                        ENCheckDate = entity.ENCheckDate,

                        ARCheckNumber = entity.ARCheckNumber,
                        ENCheckNumber = entity.ENCheckNumber,

                        ARCheckIssueDate = entity.ARCheckIssueDate,
                        ENCheckIssueDate = entity.ENCheckIssueDate
                    };
                    entryGrdColRepo.Update(egc, egc.ENTRY_SETTING_ID);
                }
                else
                {
                    Insert(entity);
                }
                return true;
            });
        }
    }
}
