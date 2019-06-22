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
    public class EntryDetailsService : IEntryDetailsService, IDisposable
    {
        IGenericRepository<ENTRY_DETAILS> entrydetailsRepo;
        IGenericRepository<ACCOUNTS> _accountsRepo;
        public EntryDetailsService(IGenericRepository<ENTRY_DETAILS> entrydetailsRepo, IGenericRepository<ACCOUNTS> AccountsRepoParam)
        {
            this.entrydetailsRepo = entrydetailsRepo;
            this._accountsRepo = AccountsRepoParam;

        }

        public Task<int> CountAsync()
        {
            return Task.Run<int>(() =>
            {
                return entrydetailsRepo.CountAsync();
            });
        }

        public bool Delete(Entry_DetailsVM entity)
        {
            ENTRY_DETAILS et = new ENTRY_DETAILS
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

                IsExemptOfTax=entity.IsExemptOfTax,

                ExemptOfTaxValue= entity.ExemptOfTaxValue,
                IsMainVatValue= entity.IsMainVatValue,
                MainVatValue=entity.MainVatValue,
                IsZeroVatValue=entity.IsZeroVatValue,
                ZeroVatValue= entity.ZeroVatValue,
                MainVat = entity.MainVat

            };
            object[] keys = new object[2] { entity.ENTRY_ID, entity.ENTRY_ROW_NUMBER };
            entrydetailsRepo.DeleteComposite(et, keys);
            return true;
        }

        public Task<bool> DeleteAsync(Entry_DetailsVM entity)
        {
            return Task.Run<bool>(() =>
            {
                ENTRY_DETAILS et = new ENTRY_DETAILS
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
                    MainVat = entity.MainVat
                };
                object[] keys = new object[2] { entity.ENTRY_ID, entity.ENTRY_ROW_NUMBER };
                entrydetailsRepo.DeleteComposite(et, keys);
                return true;
            });
        }

        public void Dispose()
        {
            entrydetailsRepo.Dispose();
        }

        public Task<List<Entry_DetailsVM>> GetAllAsync(int pageNum, int pageSize)
        {
            return Task.Run<List<Entry_DetailsVM>>(() =>
            {
                int rowCount;
                var q = from entity in entrydetailsRepo.GetPaged<long>(pageNum, pageSize, p => p.ENTRY_ID, false, out rowCount)
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
                            MainVat = entity.MainVat
                        };
                return q.ToList();
            });
        }

        public List<Entry_DetailsVM> GetAll()
        {
            var q = from entity in entrydetailsRepo.GetAll()
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
                        MainVat = entity.MainVat
                    };
            return q.ToList();
        }

        public long Insert(Entry_DetailsVM entity)
        {
            ENTRY_DETAILS et = new ENTRY_DETAILS
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
                MainVat = entity.MainVat
            };
            entrydetailsRepo.Add(et);
            return et.ENTRY_ID;
        }

        public Task<long> InsertAsync(Entry_DetailsVM entity)
        {
            return Task.Run<long>(() =>
            {
                ENTRY_DETAILS et = new ENTRY_DETAILS
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
                    MainVat = entity.MainVat
                };
                entrydetailsRepo.Add(et);
                return et.ENTRY_ID;
            });
        }

        public bool Update(Entry_DetailsVM entity)
        {
            ENTRY_DETAILS et = new ENTRY_DETAILS
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
                MainVat = entity.MainVat
            };
            object[] keys = new object[2] { entity.ENTRY_ID, entity.ENTRY_ROW_NUMBER };
            entrydetailsRepo.UpdateComposite(et, keys);
            return true;
        }

        public Task<bool> UpdateAsync(Entry_DetailsVM entity)
        {
            return Task.Run<bool>(() =>
            {
                ENTRY_DETAILS et = new ENTRY_DETAILS
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

                    CheckNumber = entity.CheckNumber,
                    CheckDate = entity.CheckDate,
                    CheckIssueDate = entity.CheckIssueDate,
                    IsExemptOfTax = entity.IsExemptOfTax,


                    ExemptOfTaxValue = entity.ExemptOfTaxValue,
                    IsMainVatValue = entity.IsMainVatValue,
                    MainVatValue = entity.MainVatValue,
                    IsZeroVatValue = entity.IsZeroVatValue,
                    ZeroVatValue = entity.ZeroVatValue,
                    MainVat= entity.MainVat
                };
                object[] keys = new object[2] { entity.ENTRY_ID, entity.ENTRY_ROW_NUMBER };
                entrydetailsRepo.UpdateComposite(et, keys);
                return true;
            });
        }

        public List<ENTRY_DETAILS> getByCostCenter(int costCenterId)
        {
            var q = entrydetailsRepo.GetAll().Where(a => a.COST_CENTER_ID == costCenterId).ToList();
            return q;
        }

        //Search Accounts
        public List<AccountVM> SearchItems(string SearchCriteria)
        {
            SqlParameter SEARCH_CODEParam = new SqlParameter("@SEARCH_CODE", (object)SearchCriteria);
            var ItemsList = _accountsRepo.SQLQuery<AccountVM>("exec SEARCH_ACCOUNTS  @SEARCH_CODE", SEARCH_CODEParam).ToList<AccountVM>();
            return ItemsList;
        }

        public Task<object> getAccountValByaccID(int accId)
        {

            return Task.Run<object>(() =>
            {
                double c24 = 0.0;
                double c22 = 0.0;
                double c21 = 0.0;
                double c18 = 0.0;
                double d24 = 0.0;
                double d22 = 0.0;
                double d21 = 0.0;
                double d18 = 0.0;
                double cMoney = 0.0;
                double dMoney = 0.0;
                var q = entrydetailsRepo.GetAll().Where(a => a.ACC_ID == accId).ToList();
                if (q.Count != 0)
                {
                    foreach (var item in q)
                    {
                        if (item.ENTRY_DEBIT != null)
                        {
                            dMoney += item.ENTRY_DEBIT;
                        }
                        if (item.ENTRY_CREDIT != null)
                        {
                            cMoney += item.ENTRY_CREDIT;
                        }
                        if (item.ENTRY_GOLD24_CREDIT != null)
                        {
                            c24 += item.ENTRY_GOLD24_CREDIT;
                        }
                        if (item.ENTRY_GOLD22_CREDIT != null)
                        {
                            c22 += (double)item.ENTRY_GOLD22_CREDIT;
                        }
                        if (item.ENTRY_GOLD21_CREDIT != null)
                        {
                            c21 += (double)item.ENTRY_GOLD21_CREDIT;
                        }
                        if (item.ENTRY_GOLD18_CREDIT != null)
                        {
                            c18 += (double)item.ENTRY_GOLD18_CREDIT;
                        }

                        if (item.ENTRY_GOLD24_DEBIT != null)
                        {
                            d24 += item.ENTRY_GOLD24_DEBIT;
                        }
                        if (item.ENTRY_GOLD22_DEBIT != null)
                        {
                            d22 += (double)item.ENTRY_GOLD22_DEBIT;
                        }
                        if (item.ENTRY_GOLD21_DEBIT != null)
                        {
                            d21 += (double)item.ENTRY_GOLD21_DEBIT;
                        }
                        if (item.ENTRY_GOLD18_DEBIT != null)
                        {
                            d18 += (double)item.ENTRY_GOLD18_DEBIT;
                        }
                    }
                }
                var name = "";
                var q_name = _accountsRepo.GetByID(accId);
                if (q_name != null)
                {
                    name = q_name.ACC_AR_NAME;
                }
                var str = new string[] {
                Convert.ToString(c24),
                Convert.ToString(c22),
                Convert.ToString(c21),
                Convert.ToString(c18),
                Convert.ToString(d24),
                Convert.ToString(d22),
                Convert.ToString(d21),
                Convert.ToString(d18),
                Convert.ToString(cMoney),
                Convert.ToString(dMoney),
                Convert.ToString(name)
            };


                return str;
            });
        }




        public Task<object> getAccountValByaccIDandBranchID(int accId,string BranchID)
        {

            return Task.Run<object>(() =>
            {
                double c24 = 0.0;
                double c22 = 0.0;
                double c21 = 0.0;
                double c18 = 0.0;
                double d24 = 0.0;
                double d22 = 0.0;
                double d21 = 0.0;
                double d18 = 0.0;
                double cMoney = 0.0;
                double dMoney = 0.0;
                var q = entrydetailsRepo.GetAll().Where(a => a.ACC_ID == accId).ToList();
                if (q.Count != 0)
                {
                    foreach (var item in q)
                    {
                        if (item.ENTRY_DEBIT != null)
                        {
                            dMoney += item.ENTRY_DEBIT;
                        }
                        if (item.ENTRY_CREDIT != null)
                        {
                            cMoney += item.ENTRY_CREDIT;
                        }
                        if (item.ENTRY_GOLD24_CREDIT != null)
                        {
                            c24 += item.ENTRY_GOLD24_CREDIT;
                        }
                        if (item.ENTRY_GOLD22_CREDIT != null)
                        {
                            c22 += (double)item.ENTRY_GOLD22_CREDIT;
                        }
                        if (item.ENTRY_GOLD21_CREDIT != null)
                        {
                            c21 += (double)item.ENTRY_GOLD21_CREDIT;
                        }
                        if (item.ENTRY_GOLD18_CREDIT != null)
                        {
                            c18 += (double)item.ENTRY_GOLD18_CREDIT;
                        }

                        if (item.ENTRY_GOLD24_DEBIT != null)
                        {
                            d24 += item.ENTRY_GOLD24_DEBIT;
                        }
                        if (item.ENTRY_GOLD22_DEBIT != null)
                        {
                            d22 += (double)item.ENTRY_GOLD22_DEBIT;
                        }
                        if (item.ENTRY_GOLD21_DEBIT != null)
                        {
                            d21 += (double)item.ENTRY_GOLD21_DEBIT;
                        }
                        if (item.ENTRY_GOLD18_DEBIT != null)
                        {
                            d18 += (double)item.ENTRY_GOLD18_DEBIT;
                        }
                    }
                }
                var name = "";
                var q_name = _accountsRepo.GetByID(accId);
                if (q_name != null)
                {
                    name = q_name.ACC_AR_NAME;
                }
                var str = new string[] {
                Convert.ToString(c24),
                Convert.ToString(c22),
                Convert.ToString(c21),
                Convert.ToString(c18),
                Convert.ToString(d24),
                Convert.ToString(d22),
                Convert.ToString(d21),
                Convert.ToString(d18),
                Convert.ToString(cMoney),
                Convert.ToString(dMoney),
                Convert.ToString(name)
            };


                return str;
            });
        }


        public Task<object> getAccountValByaccID1(int accId)
        {

            return Task.Run<object>(() =>
            {
                double c24 = 0.0;
                double c22 = 0.0;
                double c21 = 0.0;
                double c18 = 0.0;
                double d24 = 0.0;
                double d22 = 0.0;
                double d21 = 0.0;
                double d18 = 0.0;
                double cMoney = 0.0;
                double dMoney = 0.0;
                var q = entrydetailsRepo.GetAll().Where(a => a.ACC_ID == accId).ToList();
                if (q.Count != 0)
                {
                    foreach (var item in q)
                    {
                        if (item.ENTRY_DEBIT != null)
                        {
                            dMoney += item.ENTRY_DEBIT;
                        }
                        if (item.ENTRY_CREDIT != null)
                        {
                            cMoney += item.ENTRY_CREDIT;
                        }
                        if (item.ENTRY_GOLD24_CREDIT != null)
                        {
                            c24 += item.ENTRY_GOLD24_CREDIT;
                        }
                        if (item.ENTRY_GOLD22_CREDIT != null)
                        {
                            c22 += (double)item.ENTRY_GOLD22_CREDIT;
                        }
                        if (item.ENTRY_GOLD21_CREDIT != null)
                        {
                            c21 += (double)item.ENTRY_GOLD21_CREDIT;
                        }
                        if (item.ENTRY_GOLD18_CREDIT != null)
                        {
                            c18 += (double)item.ENTRY_GOLD18_CREDIT;
                        }

                        if (item.ENTRY_GOLD24_DEBIT != null)
                        {
                            d24 += item.ENTRY_GOLD24_DEBIT;
                        }
                        if (item.ENTRY_GOLD22_DEBIT != null)
                        {
                            d22 += (double)item.ENTRY_GOLD22_DEBIT;
                        }
                        if (item.ENTRY_GOLD21_DEBIT != null)
                        {
                            d21 += (double)item.ENTRY_GOLD21_DEBIT;
                        }
                        if (item.ENTRY_GOLD18_DEBIT != null)
                        {
                            d18 += (double)item.ENTRY_GOLD18_DEBIT;
                        }
                    }
                }
                var name = "";
                var q_name = _accountsRepo.GetByID(accId);
                if (q_name != null)
                {
                    name = q_name.ACC_AR_NAME;
                }
                var str = new string[] {
                Convert.ToString(c24),
                Convert.ToString(c22),
                Convert.ToString(c21),
                Convert.ToString(c18),
                Convert.ToString(d24),
                Convert.ToString(d22),
                Convert.ToString(d21),
                Convert.ToString(d18),
                Convert.ToString(cMoney),
                Convert.ToString(dMoney),
                Convert.ToString(name)
            };


                return str;
            });
        }
    }
}
