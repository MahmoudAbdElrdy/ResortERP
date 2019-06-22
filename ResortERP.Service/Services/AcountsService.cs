using Microsoft.CSharp;
using ResortERP.Core;
using ResortERP.Core.VM;
using ResortERP.Repository;
using ResortERP.Service.IServices;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ResortERP.Service.Services
{
    public class AcountsService : IAcountsService, IDisposable
    {
        IGenericRepository<ACCOUNTS> _accountsRepo;
        IGenericRepository<BILL_MASTER> billMasterRepo;
        IGenericRepository<CustomerBranches> customerBranchRepo;
        IGenericRepository<ENTRY_MASTER> entryMasterRepo;
        IGenericRepository<ENTRY_DETAILS> entryDetailsRepo;
        IGenericRepository<ENTRY_SETTINGS> entrySettingRepo;
        IGenericRepository<SYSTEM_OPTIONS> sysOptRepo;
        public AcountsService(IGenericRepository<ACCOUNTS> accountsRepo, IGenericRepository<BILL_MASTER> billMasterRepo, IGenericRepository<CustomerBranches> customerBranchRepo
            , IGenericRepository<ENTRY_MASTER> entryMasterRepo, IGenericRepository<ENTRY_DETAILS> entryDetailsRepo, IGenericRepository<ENTRY_SETTINGS> entrySettingRepo, IGenericRepository<SYSTEM_OPTIONS> _sysOptRepo)
        {
            this._accountsRepo = accountsRepo;
            this.billMasterRepo = billMasterRepo;
            this.customerBranchRepo = customerBranchRepo;
            this.entryMasterRepo = entryMasterRepo;
            this.entryDetailsRepo = entryDetailsRepo;
            this.entrySettingRepo = entrySettingRepo;
            this.sysOptRepo = _sysOptRepo;
        }

        public Task<int> CountAsync()
        {
            return Task.Run<int>(() =>
            {
                return _accountsRepo.CountAsync();
            });
        }

        public Task<int> CountofSearchAsync(string accountName)
        {
            return Task.Run<int>(() =>
            {
                if (accountName != "" && accountName != null)
                {
                    return _accountsRepo.GetAll().Where(X => (X.ACC_AR_NAME.ToLower().Contains(accountName) || X.ACC_EN_NAME.ToLower().Contains(accountName) || X.ACC_CODE.ToLower().Contains(accountName))).Count();
                }
                else
                {
                    return _accountsRepo.GetAll().Count();
                }
            });
        }


        public bool Delete(AccountVM entity)
        {
            ACCOUNTS emp = new ACCOUNTS
            {
                ACC_ID = entity.ACC_ID
            };
            _accountsRepo.Delete(emp, entity.ACC_ID);
            return true;
        }

        public Task<bool> DeleteAsync(AccountVM entity)
        {
            return Task.Run<bool>(() =>
            {
                ACCOUNTS emp = new ACCOUNTS
                {
                    ACC_ID = entity.ACC_ID
                };
                _accountsRepo.Delete(emp, entity.ACC_ID);
                int? level = UpdateParent(entity.PARENT_ACC_ID, 'm');


                if (entity.CreditOpeningAccount != null || entity.DepitOpeningAccount != null)
                {
                    ENTRY_MASTER entryMaster = entryMasterRepo.Filter(e => e.OpeningAccID == entity.ACC_ID).FirstOrDefault();
                    if (entryMaster != null)
                    {
                        //delete
                        deleteEntryOfAccount(entryMaster);
                    }
                }



                return true;
            });
        }

        public void Dispose()
        {
            _accountsRepo.Dispose();
        }

        public List<AccountVM> GetAll()
        {
            var q = from entity in _accountsRepo.GetAll()
                    select new AccountVM
                    {
                        ACC_ID = entity.ACC_ID,
                        AddedBy = entity.AddedBy,
                        AddedOn = entity.AddedOn,
                        ACC_AR_NAME = entity.ACC_AR_NAME,
                        ACC_CHECK_DATE = entity.ACC_CHECK_DATE,
                        ACC_CODE = entity.ACC_CODE,
                        ACC_CREDIT = entity.ACC_CREDIT,
                        ACC_CUSTOMER_CODE = entity.ACC_CUSTOMER_CODE,
                        ACC_DATE = entity.ACC_DATE,
                        ACC_DEBIT = entity.ACC_DEBIT,
                        ACC_DEBIT_OR_CREDIT_OR_WITHOUT = entity.ACC_DEBIT_OR_CREDIT_OR_WITHOUT,
                        ACC_EN_NAME = entity.ACC_EN_NAME,
                        ACC_LEVEL = entity.ACC_LEVEL,
                        ACC_MAX_DEBIT = entity.ACC_MAX_DEBIT,
                        ACC_MAX_CREDIT = entity.ACC_MAX_CREDIT,
                        ACC_NSONS = entity.ACC_NSONS,
                        ACC_REMARKS = entity.ACC_REMARKS,
                        ACC_STATE = entity.ACC_STATE,
                        ACC_TURNNING_YES_OR_NO = entity.ACC_TURNNING_YES_OR_NO,
                        ACC_TYPE_ID = entity.ACC_TYPE_ID,
                        CURRENCY_ID = entity.CURRENCY_ID,
                        Deactivate = entity.Deactivate,
                        FINAL_ACC_ID = entity.FINAL_ACC_ID,
                        PARENT_ACC_ID = entity.PARENT_ACC_ID,
                        UpdatedOn = entity.UpdatedOn,
                        SubjectToVAT = entity.SubjectToVAT,
                        CreditOpeningAccount = entity.CreditOpeningAccount,
                        DepitOpeningAccount = entity.DepitOpeningAccount,
                        Company_Branch_ID = entity.Company_Branch_ID,
                        TaxAccountID = entity.TaxAccountID,
                        GROUP_ID = entity.GROUP_ID
                    };
            return q.ToList();
        }


        public Task<ACCOUNTS> GetByAccountID(int AccountID)
        {
            return Task.Run<ACCOUNTS>(() =>
            {
                var acc = _accountsRepo.Filter(X => X.ACC_ID == AccountID).FirstOrDefault();
                return acc;
            });
        }
        public Task<AccountVM> GetByAccountIDTree(int AccountID)
        {
            return Task.Run<AccountVM>(() =>
            {
                //int rowCount;
                var q = from entity in _accountsRepo.GetAll()
                        select new AccountVM
                        {
                            ACC_ID = entity.ACC_ID,
                            AddedBy = entity.AddedBy,
                            AddedOn = entity.AddedOn,
                            ACC_AR_NAME = entity.ACC_AR_NAME,
                            ACC_CHECK_DATE = entity.ACC_CHECK_DATE,
                            ACC_CODE = entity.ACC_CODE,
                            ACC_CREDIT = entity.ACC_CREDIT,
                            ACC_CUSTOMER_CODE = entity.ACC_CUSTOMER_CODE,
                            ACC_DATE = entity.ACC_DATE,
                            ACC_DEBIT = entity.ACC_DEBIT,
                            ACC_DEBIT_OR_CREDIT_OR_WITHOUT = entity.ACC_DEBIT_OR_CREDIT_OR_WITHOUT,
                            ACC_EN_NAME = entity.ACC_EN_NAME,
                            ACC_LEVEL = entity.ACC_LEVEL,
                            ACC_MAX_DEBIT = entity.ACC_MAX_DEBIT,
                            ACC_MAX_CREDIT = entity.ACC_MAX_CREDIT,
                            ACC_NSONS = entity.ACC_NSONS,
                            ACC_REMARKS = entity.ACC_REMARKS,
                            ACC_STATE = entity.ACC_STATE,
                            ACC_TURNNING_YES_OR_NO = entity.ACC_TURNNING_YES_OR_NO,
                            ACC_TYPE_ID = entity.ACC_TYPE_ID,
                            CURRENCY_ID = entity.CURRENCY_ID,
                            Deactivate = entity.Deactivate,
                            FINAL_ACC_ID = entity.FINAL_ACC_ID,
                            PARENT_ACC_ID = entity.PARENT_ACC_ID,
                            UpdatedOn = entity.UpdatedOn,
                            VATRate = entity.VATRate,
                            SubjectToVAT = entity.SubjectToVAT,
                            CreditOpeningAccount = entity.CreditOpeningAccount,
                            DepitOpeningAccount = entity.DepitOpeningAccount,
                            Company_Branch_ID = entity.Company_Branch_ID,
                            TaxAccountID = entity.TaxAccountID,
                            GROUP_ID = entity.GROUP_ID
                        };

                return q.Where(a => a.ACC_ID == AccountID).FirstOrDefault();
            });
        }

        public Task<List<AccountVM>> GetSearch(string search)
        {
            return Task.Run<List<AccountVM>>(() =>
            {
                //int rowCount;
                var q = from entity in _accountsRepo.GetAll()
                        select new AccountVM
                        {
                            ACC_ID = entity.ACC_ID,
                            AddedBy = entity.AddedBy,
                            AddedOn = entity.AddedOn,
                            ACC_AR_NAME = entity.ACC_AR_NAME,
                            ACC_CHECK_DATE = entity.ACC_CHECK_DATE,
                            ACC_CODE = entity.ACC_CODE,
                            ACC_CREDIT = entity.ACC_CREDIT,
                            ACC_CUSTOMER_CODE = entity.ACC_CUSTOMER_CODE,
                            ACC_DATE = entity.ACC_DATE,
                            ACC_DEBIT = entity.ACC_DEBIT,
                            ACC_DEBIT_OR_CREDIT_OR_WITHOUT = entity.ACC_DEBIT_OR_CREDIT_OR_WITHOUT,
                            ACC_EN_NAME = entity.ACC_EN_NAME,
                            ACC_LEVEL = entity.ACC_LEVEL,
                            ACC_MAX_DEBIT = entity.ACC_MAX_DEBIT,
                            ACC_MAX_CREDIT = entity.ACC_MAX_CREDIT,
                            ACC_NSONS = entity.ACC_NSONS,
                            ACC_REMARKS = entity.ACC_REMARKS,
                            ACC_STATE = entity.ACC_STATE,
                            ACC_TURNNING_YES_OR_NO = entity.ACC_TURNNING_YES_OR_NO,
                            ACC_TYPE_ID = entity.ACC_TYPE_ID,
                            CURRENCY_ID = entity.CURRENCY_ID,
                            Deactivate = entity.Deactivate,
                            FINAL_ACC_ID = entity.FINAL_ACC_ID,
                            PARENT_ACC_ID = entity.PARENT_ACC_ID,
                            UpdatedOn = entity.UpdatedOn,
                            VATRate = entity.VATRate,
                            SubjectToVAT = entity.SubjectToVAT,
                            CreditOpeningAccount = entity.CreditOpeningAccount,
                            DepitOpeningAccount = entity.DepitOpeningAccount,
                            Company_Branch_ID = entity.Company_Branch_ID,
                            TaxAccountID = entity.TaxAccountID,
                            GROUP_ID = entity.GROUP_ID
                        };
                if (search != "" && search != null)
                {
                    var entit = q.Where(X => (X.ACC_AR_NAME.ToLower().Contains(search) || X.ACC_EN_NAME.ToLower().Contains(search) || X.ACC_CODE.ToLower().Contains(search))).ToList();
                    return entit;
                }
                else { return q.ToList(); }
            });
        }

        public Task<List<AccountVM>> getSearchForAccount(string accountName, int pageNumA, int pageSizeA)
        {
            return Task.Run<List<AccountVM>>(() =>
            {
                //int rowCount;
                var q = from entity in _accountsRepo.GetAll()
                        select new AccountVM
                        {
                            ACC_ID = entity.ACC_ID,
                            AddedBy = entity.AddedBy,
                            AddedOn = entity.AddedOn,
                            ACC_AR_NAME = entity.ACC_AR_NAME,
                            ACC_CHECK_DATE = entity.ACC_CHECK_DATE,
                            ACC_CODE = entity.ACC_CODE,
                            ACC_CREDIT = entity.ACC_CREDIT,
                            ACC_CUSTOMER_CODE = entity.ACC_CUSTOMER_CODE,
                            ACC_DATE = entity.ACC_DATE,
                            ACC_DEBIT = entity.ACC_DEBIT,
                            ACC_DEBIT_OR_CREDIT_OR_WITHOUT = entity.ACC_DEBIT_OR_CREDIT_OR_WITHOUT,
                            ACC_EN_NAME = entity.ACC_EN_NAME,
                            ACC_LEVEL = entity.ACC_LEVEL,
                            ACC_MAX_DEBIT = entity.ACC_MAX_DEBIT,
                            ACC_MAX_CREDIT = entity.ACC_MAX_CREDIT,
                            ACC_NSONS = entity.ACC_NSONS,
                            ACC_REMARKS = entity.ACC_REMARKS,
                            ACC_STATE = entity.ACC_STATE,
                            ACC_TURNNING_YES_OR_NO = entity.ACC_TURNNING_YES_OR_NO,
                            ACC_TYPE_ID = entity.ACC_TYPE_ID,
                            CURRENCY_ID = entity.CURRENCY_ID,
                            Deactivate = entity.Deactivate,
                            FINAL_ACC_ID = entity.FINAL_ACC_ID,
                            PARENT_ACC_ID = entity.PARENT_ACC_ID,
                            UpdatedOn = entity.UpdatedOn,
                            VATRate = entity.VATRate,
                            SubjectToVAT = entity.SubjectToVAT,
                            CreditOpeningAccount = entity.CreditOpeningAccount,
                            DepitOpeningAccount = entity.DepitOpeningAccount,
                            Company_Branch_ID = entity.Company_Branch_ID,
                            TaxAccountID = entity.TaxAccountID,
                            GROUP_ID = entity.GROUP_ID
                        };
                if (accountName != "" && accountName != null)
                {
                    var entit = q.Where(X => (X.ACC_AR_NAME.ToLower().Contains(accountName) || X.ACC_EN_NAME.ToLower().Contains(accountName) || X.ACC_CODE.ToLower().Contains(accountName))).Skip((pageNumA - 1) * pageSizeA).Take(pageSizeA).ToList();
                    return entit;
                }
                else { return q.Skip((pageNumA - 1) * pageSizeA).Take(pageSizeA).ToList(); }
            });
        }


        public Task<List<AccountVM>> GetAllAsync(int pageNum, int pageSize)
        {
            return Task.Run<List<AccountVM>>(() =>
            {
                int rowCount;
                var q = from entity in _accountsRepo.GetPaged<long>(pageNum, pageSize, p => p.ACC_ID, false, out rowCount)
                        select new AccountVM
                        {
                            ACC_ID = entity.ACC_ID,
                            AddedBy = entity.AddedBy,
                            AddedOn = entity.AddedOn,
                            ACC_AR_NAME = entity.ACC_AR_NAME,
                            ACC_CHECK_DATE = entity.ACC_CHECK_DATE,
                            ACC_CODE = entity.ACC_CODE,
                            ACC_CREDIT = entity.ACC_CREDIT,
                            ACC_CUSTOMER_CODE = entity.ACC_CUSTOMER_CODE,
                            ACC_DATE = entity.ACC_DATE,
                            ACC_DEBIT = entity.ACC_DEBIT,
                            ACC_DEBIT_OR_CREDIT_OR_WITHOUT = entity.ACC_DEBIT_OR_CREDIT_OR_WITHOUT,
                            ACC_EN_NAME = entity.ACC_EN_NAME,
                            ACC_LEVEL = entity.ACC_LEVEL,
                            ACC_MAX_DEBIT = entity.ACC_MAX_DEBIT,
                            ACC_MAX_CREDIT = entity.ACC_MAX_CREDIT,
                            ACC_NSONS = entity.ACC_NSONS,
                            ACC_REMARKS = entity.ACC_REMARKS,
                            ACC_STATE = entity.ACC_STATE,
                            ACC_TURNNING_YES_OR_NO = entity.ACC_TURNNING_YES_OR_NO,
                            ACC_TYPE_ID = entity.ACC_TYPE_ID,
                            CURRENCY_ID = entity.CURRENCY_ID,
                            Deactivate = entity.Deactivate,
                            FINAL_ACC_ID = entity.FINAL_ACC_ID,
                            PARENT_ACC_ID = entity.PARENT_ACC_ID,
                            UpdatedOn = entity.UpdatedOn,
                            VATRate = entity.VATRate,
                            SubjectToVAT = entity.SubjectToVAT,
                            CreditOpeningAccount = entity.CreditOpeningAccount,
                            DepitOpeningAccount = entity.DepitOpeningAccount,
                            Company_Branch_ID = entity.Company_Branch_ID,
                            TaxAccountID = entity.TaxAccountID,
                            GROUP_ID = entity.GROUP_ID
                        };
                return q.ToList();
            });
        }

        public bool Insert(AccountVM entity)
        {
            ACCOUNTS emp = new ACCOUNTS
            {
                ACC_ID = entity.ACC_ID,
                AddedBy = entity.AddedBy,
                AddedOn = entity.AddedOn,
                ACC_AR_NAME = entity.ACC_AR_NAME,
                ACC_CHECK_DATE = entity.ACC_CHECK_DATE,
                ACC_CODE = entity.ACC_CODE,
                ACC_CREDIT = entity.ACC_CREDIT,
                ACC_CUSTOMER_CODE = entity.ACC_CUSTOMER_CODE,
                ACC_DATE = entity.ACC_DATE,
                ACC_DEBIT = entity.ACC_DEBIT,
                ACC_DEBIT_OR_CREDIT_OR_WITHOUT = entity.ACC_DEBIT_OR_CREDIT_OR_WITHOUT,
                ACC_EN_NAME = entity.ACC_EN_NAME,
                ACC_LEVEL = entity.ACC_LEVEL,
                ACC_MAX_DEBIT = entity.ACC_MAX_DEBIT,
                ACC_MAX_CREDIT = entity.ACC_MAX_CREDIT,
                ACC_NSONS = entity.ACC_NSONS,
                ACC_REMARKS = entity.ACC_REMARKS,
                ACC_STATE = entity.ACC_STATE,
                ACC_TURNNING_YES_OR_NO = entity.ACC_TURNNING_YES_OR_NO,
                ACC_TYPE_ID = entity.ACC_TYPE_ID,
                CURRENCY_ID = entity.CURRENCY_ID,
                Deactivate = entity.Deactivate,
                FINAL_ACC_ID = entity.FINAL_ACC_ID,
                PARENT_ACC_ID = entity.PARENT_ACC_ID,
                UpdatedOn = entity.UpdatedOn,
                VATRate = entity.VATRate,
                SubjectToVAT = entity.SubjectToVAT,
                CreditOpeningAccount = entity.CreditOpeningAccount,
                DepitOpeningAccount = entity.DepitOpeningAccount,
                Company_Branch_ID = entity.Company_Branch_ID,
                TaxAccountID = entity.TaxAccountID,
                GROUP_ID = entity.GROUP_ID
            };
            _accountsRepo.Add(emp);
            return true;
        }

        public int InsertGetID(AccountVM entity)
        {
            ACCOUNTS Acc = new ACCOUNTS
            {
                ACC_ID = entity.ACC_ID,
                AddedBy = entity.AddedBy,
                AddedOn = entity.AddedOn,
                ACC_AR_NAME = entity.ACC_AR_NAME,
                ACC_CHECK_DATE = entity.ACC_CHECK_DATE,
                ACC_CODE = entity.ACC_CODE,
                ACC_CREDIT = entity.ACC_CREDIT,
                ACC_CUSTOMER_CODE = entity.ACC_CUSTOMER_CODE,
                ACC_DATE = entity.ACC_DATE,
                ACC_DEBIT = entity.ACC_DEBIT,
                ACC_DEBIT_OR_CREDIT_OR_WITHOUT = entity.ACC_DEBIT_OR_CREDIT_OR_WITHOUT,
                ACC_EN_NAME = entity.ACC_EN_NAME,
                ACC_LEVEL = entity.ACC_LEVEL,
                ACC_MAX_DEBIT = entity.ACC_MAX_DEBIT,
                ACC_MAX_CREDIT = entity.ACC_MAX_CREDIT,
                ACC_NSONS = entity.ACC_NSONS,
                ACC_REMARKS = entity.ACC_REMARKS,
                ACC_STATE = entity.ACC_STATE,
                ACC_TURNNING_YES_OR_NO = entity.ACC_TURNNING_YES_OR_NO,
                ACC_TYPE_ID = entity.ACC_TYPE_ID,
                CURRENCY_ID = entity.CURRENCY_ID,
                Deactivate = entity.Deactivate,
                FINAL_ACC_ID = entity.FINAL_ACC_ID,
                PARENT_ACC_ID = entity.PARENT_ACC_ID,
                UpdatedOn = entity.UpdatedOn,
                VATRate = entity.VATRate,
                SubjectToVAT = entity.SubjectToVAT,
                CreditOpeningAccount = entity.CreditOpeningAccount,
                DepitOpeningAccount = entity.DepitOpeningAccount,
                Company_Branch_ID = entity.Company_Branch_ID,
                TaxAccountID = entity.TaxAccountID,
                GROUP_ID = entity.GROUP_ID
            };
            _accountsRepo.Add(Acc);
            return Acc.ACC_ID;
        }

        public Task<int> InsertAsync(AccountVM entity)
        {
            return Task.Run<int>(() =>
            {
                ACCOUNTS emp = new ACCOUNTS
                {
                    ACC_ID = entity.ACC_ID,
                    AddedBy = entity.AddedBy,
                    AddedOn = entity.AddedOn,
                    ACC_AR_NAME = entity.ACC_AR_NAME,
                    ACC_CHECK_DATE = entity.ACC_CHECK_DATE,
                    ACC_CODE = entity.ACC_CODE,
                    ACC_CREDIT = entity.ACC_CREDIT,
                    ACC_CUSTOMER_CODE = entity.ACC_CUSTOMER_CODE,
                    ACC_DATE = entity.ACC_DATE,
                    ACC_DEBIT = entity.ACC_DEBIT,
                    ACC_DEBIT_OR_CREDIT_OR_WITHOUT = entity.ACC_DEBIT_OR_CREDIT_OR_WITHOUT,
                    ACC_EN_NAME = entity.ACC_EN_NAME,
                    ACC_LEVEL = entity.ACC_LEVEL,
                    ACC_MAX_DEBIT = entity.ACC_MAX_DEBIT,
                    ACC_MAX_CREDIT = entity.ACC_MAX_CREDIT,
                    ACC_NSONS = entity.ACC_NSONS,
                    ACC_REMARKS = entity.ACC_REMARKS,
                    ACC_STATE = entity.ACC_STATE,
                    ACC_TURNNING_YES_OR_NO = "y",
                    ACC_TYPE_ID = entity.ACC_TYPE_ID,
                    CURRENCY_ID = entity.CURRENCY_ID,
                    Deactivate = entity.Deactivate,
                    FINAL_ACC_ID = entity.FINAL_ACC_ID,
                    PARENT_ACC_ID = entity.PARENT_ACC_ID,
                    UpdatedOn = entity.UpdatedOn,
                    VATRate = entity.VATRate,
                    SubjectToVAT = entity.SubjectToVAT,
                    CreditOpeningAccount = entity.CreditOpeningAccount,
                    DepitOpeningAccount = entity.DepitOpeningAccount,
                    Company_Branch_ID = entity.Company_Branch_ID,
                    TaxAccountID = entity.TaxAccountID,
                    GROUP_ID = entity.GROUP_ID

                };
                _accountsRepo.Add(emp);

                int? level = UpdateParent(entity.PARENT_ACC_ID, 'a');
                if (level != null)
                {
                    emp.ACC_LEVEL = level + 1;
                }
                else
                {
                    emp.ACC_LEVEL = level;
                }
                _accountsRepo.Update(emp, emp.ACC_ID);
                return emp.ACC_ID;
            });
        }

        public bool Update(AccountVM entity)
        {
            ACCOUNTS emp = new ACCOUNTS
            {
                ACC_ID = entity.ACC_ID,
                AddedBy = entity.AddedBy,
                AddedOn = entity.AddedOn,
                ACC_AR_NAME = entity.ACC_AR_NAME,
                ACC_CHECK_DATE = entity.ACC_CHECK_DATE,
                ACC_CODE = entity.ACC_CODE,
                ACC_CREDIT = entity.ACC_CREDIT,
                ACC_CUSTOMER_CODE = entity.ACC_CUSTOMER_CODE,
                ACC_DATE = entity.ACC_DATE,
                ACC_DEBIT = entity.ACC_DEBIT,
                ACC_DEBIT_OR_CREDIT_OR_WITHOUT = entity.ACC_DEBIT_OR_CREDIT_OR_WITHOUT,
                ACC_EN_NAME = entity.ACC_EN_NAME,
                ACC_LEVEL = entity.ACC_LEVEL,
                ACC_MAX_DEBIT = entity.ACC_MAX_DEBIT,
                ACC_MAX_CREDIT = entity.ACC_MAX_CREDIT,
                ACC_NSONS = entity.ACC_NSONS,
                ACC_REMARKS = entity.ACC_REMARKS,
                ACC_STATE = entity.ACC_STATE,
                ACC_TURNNING_YES_OR_NO = entity.ACC_TURNNING_YES_OR_NO,
                ACC_TYPE_ID = entity.ACC_TYPE_ID,
                CURRENCY_ID = entity.CURRENCY_ID,
                Deactivate = entity.Deactivate,
                FINAL_ACC_ID = entity.FINAL_ACC_ID,
                PARENT_ACC_ID = entity.PARENT_ACC_ID,
                UpdatedOn = entity.UpdatedOn,
                VATRate = entity.VATRate,
                SubjectToVAT = entity.SubjectToVAT,
                CreditOpeningAccount = entity.CreditOpeningAccount,
                DepitOpeningAccount = entity.DepitOpeningAccount,
                Company_Branch_ID = entity.Company_Branch_ID,
                TaxAccountID = entity.TaxAccountID,
                GROUP_ID = entity.GROUP_ID
            };
            _accountsRepo.Update(emp, emp.ACC_ID);
            return true;
        }

        public Task<bool> UpdateAsync(AccountVM entity)
        {
            return Task.Run<bool>(() =>
            {
                ACCOUNTS emp = new ACCOUNTS
                {
                    ACC_ID = entity.ACC_ID,
                    AddedBy = entity.AddedBy,
                    AddedOn = entity.AddedOn,
                    ACC_AR_NAME = entity.ACC_AR_NAME,
                    ACC_CHECK_DATE = entity.ACC_CHECK_DATE,
                    ACC_CODE = entity.ACC_CODE,
                    ACC_CREDIT = entity.ACC_CREDIT,
                    ACC_CUSTOMER_CODE = entity.ACC_CUSTOMER_CODE,
                    ACC_DATE = entity.ACC_DATE,
                    ACC_DEBIT = entity.ACC_DEBIT,
                    ACC_DEBIT_OR_CREDIT_OR_WITHOUT = entity.ACC_DEBIT_OR_CREDIT_OR_WITHOUT,
                    ACC_EN_NAME = entity.ACC_EN_NAME,
                    ACC_LEVEL = entity.ACC_LEVEL,
                    ACC_MAX_DEBIT = entity.ACC_MAX_DEBIT,
                    ACC_MAX_CREDIT = entity.ACC_MAX_CREDIT,
                    ACC_NSONS = entity.ACC_NSONS,
                    ACC_REMARKS = entity.ACC_REMARKS,
                    ACC_STATE = entity.ACC_STATE,
                    ACC_TURNNING_YES_OR_NO = entity.ACC_TURNNING_YES_OR_NO,
                    ACC_TYPE_ID = entity.ACC_TYPE_ID,
                    CURRENCY_ID = entity.CURRENCY_ID,
                    Deactivate = entity.Deactivate,
                    FINAL_ACC_ID = entity.FINAL_ACC_ID,
                    PARENT_ACC_ID = entity.PARENT_ACC_ID,
                    UpdatedOn = entity.UpdatedOn,
                    VATRate = entity.VATRate,
                    SubjectToVAT = entity.SubjectToVAT,
                    CreditOpeningAccount = entity.CreditOpeningAccount,
                    DepitOpeningAccount = entity.DepitOpeningAccount,
                    Company_Branch_ID = entity.Company_Branch_ID,
                    TaxAccountID = entity.TaxAccountID,
                    GROUP_ID = entity.GROUP_ID
                };
                _accountsRepo.Update(emp, emp.ACC_ID);



                //int? level = UpdateParent(entity.PARENT_ACC_ID, 'a');
                //if (level != null)
                //{
                //    emp.ACC_LEVEL = level + 1;
                //}
                //else
                //{
                //    emp.ACC_LEVEL = level;
                //}
                //_accountsRepo.Update(emp, emp.ACC_ID);

                return true;
            });
        }

        public List<AccountVM> GetAllCustomerAccounts(int Type)
        {

            if (Type == 13)
            {
                var q = from entity in _accountsRepo.GetAll().Where(x => x.ACC_TYPE_ID == (int)AccountWorkshopsEnum.Workshop0 || x.ACC_TYPE_ID == (int)AccountCustomersEnum.Cust_Acc0 || x.ACC_TYPE_ID == (int)AccountCustomersEnum.Cust_Acc2)
                        select new AccountVM
                        {
                            ACC_ID = entity.ACC_ID,
                            AddedBy = entity.AddedBy,
                            AddedOn = entity.AddedOn,
                            ACC_AR_NAME = entity.ACC_AR_NAME,
                            ACC_CHECK_DATE = entity.ACC_CHECK_DATE,
                            ACC_CODE = entity.ACC_CODE,
                            ACC_CREDIT = entity.ACC_CREDIT,
                            ACC_CUSTOMER_CODE = entity.ACC_CUSTOMER_CODE,
                            ACC_DATE = entity.ACC_DATE,
                            ACC_DEBIT = entity.ACC_DEBIT,
                            ACC_DEBIT_OR_CREDIT_OR_WITHOUT = entity.ACC_DEBIT_OR_CREDIT_OR_WITHOUT,
                            ACC_EN_NAME = entity.ACC_EN_NAME,
                            ACC_LEVEL = entity.ACC_LEVEL,
                            ACC_MAX_DEBIT = entity.ACC_MAX_DEBIT,
                            ACC_MAX_CREDIT = entity.ACC_MAX_CREDIT,
                            ACC_NSONS = entity.ACC_NSONS,
                            ACC_REMARKS = entity.ACC_REMARKS,
                            ACC_STATE = entity.ACC_STATE,
                            ACC_TURNNING_YES_OR_NO = entity.ACC_TURNNING_YES_OR_NO,
                            ACC_TYPE_ID = entity.ACC_TYPE_ID,
                            CURRENCY_ID = entity.CURRENCY_ID,
                            Deactivate = entity.Deactivate,
                            FINAL_ACC_ID = entity.FINAL_ACC_ID,
                            PARENT_ACC_ID = entity.PARENT_ACC_ID,
                            UpdatedOn = entity.UpdatedOn,
                            VATRate = entity.VATRate,
                            SubjectToVAT = entity.SubjectToVAT,
                            CreditOpeningAccount = entity.CreditOpeningAccount,
                            DepitOpeningAccount = entity.DepitOpeningAccount,
                            Company_Branch_ID = entity.Company_Branch_ID,
                            TaxAccountID = entity.TaxAccountID,
                            GROUP_ID = entity.GROUP_ID
                        };
                return q.ToList();
            }
            else if (Type == 14)
            {
                var q = from entity in _accountsRepo.GetAll().Where(x => x.ACC_TYPE_ID == (int)AccountWorkshopsEnum.Workshop0 || x.ACC_TYPE_ID == (int)AccountCustomersEnum.Cust_Acc1 || x.ACC_TYPE_ID == (int)AccountCustomersEnum.Cust_Acc2)
                        select new AccountVM
                        {
                            ACC_ID = entity.ACC_ID,
                            AddedBy = entity.AddedBy,
                            AddedOn = entity.AddedOn,
                            ACC_AR_NAME = entity.ACC_AR_NAME,
                            ACC_CHECK_DATE = entity.ACC_CHECK_DATE,
                            ACC_CODE = entity.ACC_CODE,
                            ACC_CREDIT = entity.ACC_CREDIT,
                            ACC_CUSTOMER_CODE = entity.ACC_CUSTOMER_CODE,
                            ACC_DATE = entity.ACC_DATE,
                            ACC_DEBIT = entity.ACC_DEBIT,
                            ACC_DEBIT_OR_CREDIT_OR_WITHOUT = entity.ACC_DEBIT_OR_CREDIT_OR_WITHOUT,
                            ACC_EN_NAME = entity.ACC_EN_NAME,
                            ACC_LEVEL = entity.ACC_LEVEL,
                            ACC_MAX_DEBIT = entity.ACC_MAX_DEBIT,
                            ACC_MAX_CREDIT = entity.ACC_MAX_CREDIT,
                            ACC_NSONS = entity.ACC_NSONS,
                            ACC_REMARKS = entity.ACC_REMARKS,
                            ACC_STATE = entity.ACC_STATE,
                            ACC_TURNNING_YES_OR_NO = entity.ACC_TURNNING_YES_OR_NO,
                            ACC_TYPE_ID = entity.ACC_TYPE_ID,
                            CURRENCY_ID = entity.CURRENCY_ID,
                            Deactivate = entity.Deactivate,
                            FINAL_ACC_ID = entity.FINAL_ACC_ID,
                            PARENT_ACC_ID = entity.PARENT_ACC_ID,
                            UpdatedOn = entity.UpdatedOn,
                            VATRate = entity.VATRate,
                            SubjectToVAT = entity.SubjectToVAT,
                            CreditOpeningAccount = entity.CreditOpeningAccount,
                            DepitOpeningAccount = entity.DepitOpeningAccount,
                            Company_Branch_ID = entity.Company_Branch_ID,
                            TaxAccountID = entity.TaxAccountID,
                            GROUP_ID = entity.GROUP_ID
                        };
                return q.ToList();
            }
            else if (Type == 17 || Type == 18)
            {
                var q = from entity in _accountsRepo.GetAll().Where(x => x.ACC_TYPE_ID == (int)AccountWorkshopsEnum.Workshop0 || x.ACC_TYPE_ID == (int)AccountCustomersEnum.Cust_Acc0 || x.ACC_TYPE_ID == (int)AccountCustomersEnum.Cust_Acc1 || x.ACC_TYPE_ID == (int)AccountCustomersEnum.Cust_Acc2)
                        select new AccountVM
                        {
                            ACC_ID = entity.ACC_ID,
                            AddedBy = entity.AddedBy,
                            AddedOn = entity.AddedOn,
                            ACC_AR_NAME = entity.ACC_AR_NAME,
                            ACC_CHECK_DATE = entity.ACC_CHECK_DATE,
                            ACC_CODE = entity.ACC_CODE,
                            ACC_CREDIT = entity.ACC_CREDIT,
                            ACC_CUSTOMER_CODE = entity.ACC_CUSTOMER_CODE,
                            ACC_DATE = entity.ACC_DATE,
                            ACC_DEBIT = entity.ACC_DEBIT,
                            ACC_DEBIT_OR_CREDIT_OR_WITHOUT = entity.ACC_DEBIT_OR_CREDIT_OR_WITHOUT,
                            ACC_EN_NAME = entity.ACC_EN_NAME,
                            ACC_LEVEL = entity.ACC_LEVEL,
                            ACC_MAX_DEBIT = entity.ACC_MAX_DEBIT,
                            ACC_MAX_CREDIT = entity.ACC_MAX_CREDIT,
                            ACC_NSONS = entity.ACC_NSONS,
                            ACC_REMARKS = entity.ACC_REMARKS,
                            ACC_STATE = entity.ACC_STATE,
                            ACC_TURNNING_YES_OR_NO = entity.ACC_TURNNING_YES_OR_NO,
                            ACC_TYPE_ID = entity.ACC_TYPE_ID,
                            CURRENCY_ID = entity.CURRENCY_ID,
                            Deactivate = entity.Deactivate,
                            FINAL_ACC_ID = entity.FINAL_ACC_ID,
                            PARENT_ACC_ID = entity.PARENT_ACC_ID,
                            UpdatedOn = entity.UpdatedOn,
                            VATRate = entity.VATRate,
                            SubjectToVAT = entity.SubjectToVAT,
                            CreditOpeningAccount = entity.CreditOpeningAccount,
                            DepitOpeningAccount = entity.DepitOpeningAccount,
                            Company_Branch_ID = entity.Company_Branch_ID,
                            TaxAccountID = entity.TaxAccountID,
                            GROUP_ID = entity.GROUP_ID
                        };
                return q.ToList();
            }
            else if (Type == 19 || Type == 20 || Type == 23)
            {
                var q = from entity in _accountsRepo.GetAll().Where(x => x.ACC_TYPE_ID == (int)AccountWorkshopsEnum.Workshop0 || x.ACC_TYPE_ID == (int)AccountCustomersEnum.Cust_Acc1 || x.ACC_TYPE_ID == (int)AccountCustomersEnum.Cust_Acc2)
                        select new AccountVM
                        {
                            ACC_ID = entity.ACC_ID,
                            AddedBy = entity.AddedBy,
                            AddedOn = entity.AddedOn,
                            ACC_AR_NAME = entity.ACC_AR_NAME,
                            ACC_CHECK_DATE = entity.ACC_CHECK_DATE,
                            ACC_CODE = entity.ACC_CODE,
                            ACC_CREDIT = entity.ACC_CREDIT,
                            ACC_CUSTOMER_CODE = entity.ACC_CUSTOMER_CODE,
                            ACC_DATE = entity.ACC_DATE,
                            ACC_DEBIT = entity.ACC_DEBIT,
                            ACC_DEBIT_OR_CREDIT_OR_WITHOUT = entity.ACC_DEBIT_OR_CREDIT_OR_WITHOUT,
                            ACC_EN_NAME = entity.ACC_EN_NAME,
                            ACC_LEVEL = entity.ACC_LEVEL,
                            ACC_MAX_DEBIT = entity.ACC_MAX_DEBIT,
                            ACC_MAX_CREDIT = entity.ACC_MAX_CREDIT,
                            ACC_NSONS = entity.ACC_NSONS,
                            ACC_REMARKS = entity.ACC_REMARKS,
                            ACC_STATE = entity.ACC_STATE,
                            ACC_TURNNING_YES_OR_NO = entity.ACC_TURNNING_YES_OR_NO,
                            ACC_TYPE_ID = entity.ACC_TYPE_ID,
                            CURRENCY_ID = entity.CURRENCY_ID,
                            Deactivate = entity.Deactivate,
                            FINAL_ACC_ID = entity.FINAL_ACC_ID,
                            PARENT_ACC_ID = entity.PARENT_ACC_ID,
                            UpdatedOn = entity.UpdatedOn,
                            VATRate = entity.VATRate,
                            SubjectToVAT = entity.SubjectToVAT,
                            CreditOpeningAccount = entity.CreditOpeningAccount,
                            DepitOpeningAccount = entity.DepitOpeningAccount,
                            Company_Branch_ID = entity.Company_Branch_ID,
                            TaxAccountID = entity.TaxAccountID,
                            GROUP_ID = entity.GROUP_ID
                        };
                return q.ToList();
            }
            else
            {
                var q = from entity in _accountsRepo.GetAll().Where(x => x.ACC_TYPE_ID == (int)AccountCustomersEnum.Cust_Acc0 || x.ACC_TYPE_ID == (int)AccountCustomersEnum.Cust_Acc1 || x.ACC_TYPE_ID == (int)AccountCustomersEnum.Cust_Acc2)
                        select new AccountVM
                        {
                            ACC_ID = entity.ACC_ID,
                            AddedBy = entity.AddedBy,
                            AddedOn = entity.AddedOn,
                            ACC_AR_NAME = entity.ACC_AR_NAME,
                            ACC_CHECK_DATE = entity.ACC_CHECK_DATE,
                            ACC_CODE = entity.ACC_CODE,
                            ACC_CREDIT = entity.ACC_CREDIT,
                            ACC_CUSTOMER_CODE = entity.ACC_CUSTOMER_CODE,
                            ACC_DATE = entity.ACC_DATE,
                            ACC_DEBIT = entity.ACC_DEBIT,
                            ACC_DEBIT_OR_CREDIT_OR_WITHOUT = entity.ACC_DEBIT_OR_CREDIT_OR_WITHOUT,
                            ACC_EN_NAME = entity.ACC_EN_NAME,
                            ACC_LEVEL = entity.ACC_LEVEL,
                            ACC_MAX_DEBIT = entity.ACC_MAX_DEBIT,
                            ACC_MAX_CREDIT = entity.ACC_MAX_CREDIT,
                            ACC_NSONS = entity.ACC_NSONS,
                            ACC_REMARKS = entity.ACC_REMARKS,
                            ACC_STATE = entity.ACC_STATE,
                            ACC_TURNNING_YES_OR_NO = entity.ACC_TURNNING_YES_OR_NO,
                            ACC_TYPE_ID = entity.ACC_TYPE_ID,
                            CURRENCY_ID = entity.CURRENCY_ID,
                            Deactivate = entity.Deactivate,
                            FINAL_ACC_ID = entity.FINAL_ACC_ID,
                            PARENT_ACC_ID = entity.PARENT_ACC_ID,
                            UpdatedOn = entity.UpdatedOn,
                            VATRate = entity.VATRate,
                            SubjectToVAT = entity.SubjectToVAT,
                            CreditOpeningAccount = entity.CreditOpeningAccount,
                            DepitOpeningAccount = entity.DepitOpeningAccount,
                            Company_Branch_ID = entity.Company_Branch_ID,
                            TaxAccountID = entity.TaxAccountID,
                            GROUP_ID = entity.GROUP_ID
                        };
                return q.ToList();
            }

        }


        public List<AccountVM> GetAllBoxAccounts()
        {

            var q = from entity in _accountsRepo.GetAll().Where(x => x.ACC_TYPE_ID == (int)AccountBoxEnum.Box_Acc0 || x.ACC_TYPE_ID == (int)AccountBoxEnum.Box_Acc1)
                    select new AccountVM
                    {
                        ACC_ID = entity.ACC_ID,
                        AddedBy = entity.AddedBy,
                        AddedOn = entity.AddedOn,
                        ACC_AR_NAME = entity.ACC_AR_NAME,
                        ACC_CHECK_DATE = entity.ACC_CHECK_DATE,
                        ACC_CODE = entity.ACC_CODE,
                        ACC_CREDIT = entity.ACC_CREDIT,
                        ACC_CUSTOMER_CODE = entity.ACC_CUSTOMER_CODE,
                        ACC_DATE = entity.ACC_DATE,
                        ACC_DEBIT = entity.ACC_DEBIT,
                        ACC_DEBIT_OR_CREDIT_OR_WITHOUT = entity.ACC_DEBIT_OR_CREDIT_OR_WITHOUT,
                        ACC_EN_NAME = entity.ACC_EN_NAME,
                        ACC_LEVEL = entity.ACC_LEVEL,
                        ACC_MAX_DEBIT = entity.ACC_MAX_DEBIT,
                        ACC_MAX_CREDIT = entity.ACC_MAX_CREDIT,
                        ACC_NSONS = entity.ACC_NSONS,
                        ACC_REMARKS = entity.ACC_REMARKS,
                        ACC_STATE = entity.ACC_STATE,
                        ACC_TURNNING_YES_OR_NO = entity.ACC_TURNNING_YES_OR_NO,
                        ACC_TYPE_ID = entity.ACC_TYPE_ID,
                        CURRENCY_ID = entity.CURRENCY_ID,
                        Deactivate = entity.Deactivate,
                        FINAL_ACC_ID = entity.FINAL_ACC_ID,
                        PARENT_ACC_ID = entity.PARENT_ACC_ID,
                        UpdatedOn = entity.UpdatedOn,
                        VATRate = entity.VATRate,
                        SubjectToVAT = entity.SubjectToVAT,
                        CreditOpeningAccount = entity.CreditOpeningAccount,
                        DepitOpeningAccount = entity.DepitOpeningAccount,
                        Company_Branch_ID = entity.Company_Branch_ID,
                        TaxAccountID = entity.TaxAccountID,
                        GROUP_ID = entity.GROUP_ID
                    };
            return q.ToList();
        }


        //var q = from entity in _accountsRepo.GetAll().Where(x => x.ACC_TYPE_ID == (int)AccountEnum.Acc8 || x.ACC_TYPE_ID == (int)AccountEnum.Acc9)
        //        select new AccountVM
        //        {
        //            ACC_ID = entity.ACC_ID,
        //            AddedBy = entity.AddedBy,
        //            AddedOn = entity.AddedOn,
        //            ACC_AR_NAME = entity.ACC_AR_NAME,
        //            ACC_CHECK_DATE = entity.ACC_CHECK_DATE,
        //            ACC_CODE = entity.ACC_CODE,
        //            ACC_CREDIT = entity.ACC_CREDIT,
        //            ACC_CUSTOMER_CODE = entity.ACC_CUSTOMER_CODE,
        //            ACC_DATE = entity.ACC_DATE,
        //            ACC_DEBIT = entity.ACC_DEBIT,
        //            ACC_DEBIT_OR_CREDIT_OR_WITHOUT = entity.ACC_DEBIT_OR_CREDIT_OR_WITHOUT,
        //            ACC_EN_NAME = entity.ACC_EN_NAME,
        //            ACC_LEVEL = entity.ACC_LEVEL,
        //            ACC_MAX_DEBIT = entity.ACC_MAX_DEBIT,
        //            ACC_MAX_CREDIT = entity.ACC_MAX_CREDIT,
        //            ACC_NSONS = entity.ACC_NSONS,
        //            ACC_REMARKS = entity.ACC_REMARKS,
        //            ACC_STATE = entity.ACC_STATE,
        //            ACC_TURNNING_YES_OR_NO = entity.ACC_TURNNING_YES_OR_NO,
        //            ACC_TYPE_ID = entity.ACC_TYPE_ID,
        //            CURRENCY_ID = entity.CURRENCY_ID,
        //            Deactivate = entity.Deactivate,
        //            FINAL_ACC_ID = entity.FINAL_ACC_ID,
        //            PARENT_ACC_ID = entity.PARENT_ACC_ID,
        //            UpdatedOn = entity.UpdatedOn,
        //            VATRate = entity.VATRate,
        //            SubjectToVAT = entity.SubjectToVAT,
        //            CreditOpeningAccount = entity.CreditOpeningAccount,
        //            DepitOpeningAccount = entity.DepitOpeningAccount
        //        };
        //return q.ToList();
        // }



        public List<AccountVM> GetAllGoldBoxAccounts()
        {

            var q = from entity in _accountsRepo.GetAll().Where(x => x.ACC_TYPE_ID == (int)AccountGoldBoxEnum.GoldBox_Acc0)
                    select new AccountVM
                    {
                        ACC_ID = entity.ACC_ID,
                        AddedBy = entity.AddedBy,
                        AddedOn = entity.AddedOn,
                        ACC_AR_NAME = entity.ACC_AR_NAME,
                        ACC_CHECK_DATE = entity.ACC_CHECK_DATE,
                        ACC_CODE = entity.ACC_CODE,
                        ACC_CREDIT = entity.ACC_CREDIT,
                        ACC_CUSTOMER_CODE = entity.ACC_CUSTOMER_CODE,
                        ACC_DATE = entity.ACC_DATE,
                        ACC_DEBIT = entity.ACC_DEBIT,
                        ACC_DEBIT_OR_CREDIT_OR_WITHOUT = entity.ACC_DEBIT_OR_CREDIT_OR_WITHOUT,
                        ACC_EN_NAME = entity.ACC_EN_NAME,
                        ACC_LEVEL = entity.ACC_LEVEL,
                        ACC_MAX_DEBIT = entity.ACC_MAX_DEBIT,
                        ACC_MAX_CREDIT = entity.ACC_MAX_CREDIT,
                        ACC_NSONS = entity.ACC_NSONS,
                        ACC_REMARKS = entity.ACC_REMARKS,
                        ACC_STATE = entity.ACC_STATE,
                        ACC_TURNNING_YES_OR_NO = entity.ACC_TURNNING_YES_OR_NO,
                        ACC_TYPE_ID = entity.ACC_TYPE_ID,
                        CURRENCY_ID = entity.CURRENCY_ID,
                        Deactivate = entity.Deactivate,
                        FINAL_ACC_ID = entity.FINAL_ACC_ID,
                        PARENT_ACC_ID = entity.PARENT_ACC_ID,
                        UpdatedOn = entity.UpdatedOn,
                        VATRate = entity.VATRate,
                        SubjectToVAT = entity.SubjectToVAT,
                        CreditOpeningAccount = entity.CreditOpeningAccount,
                        DepitOpeningAccount = entity.DepitOpeningAccount,
                        Company_Branch_ID = entity.Company_Branch_ID,
                        TaxAccountID = entity.TaxAccountID,
                        GROUP_ID = entity.GROUP_ID
                    };
            return q.ToList();
        }


        public List<AccountVM> SearchItems(string SearchCriteria)
        {
            int CharsCount = Regex.Matches(SearchCriteria, @"[a-zA-Z]").Count;
            bool IsArabicLetters = ContainsArabicLetters(SearchCriteria);
            if (CharsCount > 1 || IsArabicLetters)
            {
                SqlParameter SearchCodeParam = new SqlParameter("@SEARCH_CODE", (object)SearchCriteria);
                SqlParameter SearchTypeParam = new SqlParameter("@TYPE", (object)1);
                var AccountList = _accountsRepo.SQLQuery<AccountVM>("exec SEARCH_ACCOUNTS  @SEARCH_CODE,@TYPE", SearchCodeParam, SearchTypeParam).ToList<AccountVM>();
                return AccountList;
            }
            else
            {
                SqlParameter SEARCH_CODEParam = new SqlParameter("@SEARCH_CODE", (object)SearchCriteria);
                SqlParameter SearchTypeParam = new SqlParameter("@TYPE", (object)1);
                var AccountList = _accountsRepo.SQLQuery<AccountVM>("exec SEARCH_ACCOUNTS  @SEARCH_CODE,@TYPE", SEARCH_CODEParam, SearchTypeParam).ToList<AccountVM>();
                return AccountList;
            }
        }


        internal static bool ContainsArabicLetters(string text)
        {
            foreach (char character in text.ToCharArray())
            {
                if (character >= 0x600 && character <= 0x6ff)
                    return true;

                if (character >= 0x750 && character <= 0x77f)
                    return true;

                if (character >= 0xfb50 && character <= 0xfc3f)
                    return true;

                if (character >= 0xfe70 && character <= 0xfefc)
                    return true;
            }
            return false;
        }


        public AccountVM GetAccountDataByCode(string AccountCode, int Type)
        {
            var q = from p in _accountsRepo.Filter(x => x.ACC_CODE == AccountCode)
                        //join iu in itemUnitsRepo.GetAll() on p.ITEM_ID equals iu.ITEM_ID into g
                        //from x in g.DefaultIfEmpty()
                        //join Y in unitsRepo.GetAll() on x.UNIT_ID equals Y.UNIT_ID into z
                        //from YY in z.DefaultIfEmpty()
                        //join YYY in itemCaliberRepo.GetAll() on p.CaliberID equals YYY.ID into zz
                        //from YYYY in zz.DefaultIfEmpty()
                        //where x.IS_DEFAULT = true
                    select new AccountVM
                    {
                        ACC_ID = p.ACC_ID,
                        AddedBy = p.AddedBy,
                        AddedOn = p.AddedOn,
                        ACC_AR_NAME = p.ACC_AR_NAME,
                        ACC_CHECK_DATE = p.ACC_CHECK_DATE,
                        ACC_CODE = p.ACC_CODE,
                        ACC_CREDIT = p.ACC_CREDIT,
                        ACC_CUSTOMER_CODE = p.ACC_CUSTOMER_CODE,
                        ACC_DATE = p.ACC_DATE,
                        ACC_DEBIT = p.ACC_DEBIT,
                        ACC_DEBIT_OR_CREDIT_OR_WITHOUT = p.ACC_DEBIT_OR_CREDIT_OR_WITHOUT,
                        ACC_EN_NAME = p.ACC_EN_NAME,
                        ACC_LEVEL = p.ACC_LEVEL,
                        ACC_MAX_DEBIT = p.ACC_MAX_DEBIT,
                        ACC_MAX_CREDIT = p.ACC_MAX_CREDIT,
                        ACC_NSONS = p.ACC_NSONS,
                        ACC_REMARKS = p.ACC_REMARKS,
                        ACC_STATE = p.ACC_STATE,
                        ACC_TURNNING_YES_OR_NO = p.ACC_TURNNING_YES_OR_NO,
                        ACC_TYPE_ID = p.ACC_TYPE_ID,
                        CURRENCY_ID = p.CURRENCY_ID,
                        Deactivate = p.Deactivate,
                        FINAL_ACC_ID = p.FINAL_ACC_ID,
                        PARENT_ACC_ID = p.PARENT_ACC_ID,
                        UpdatedOn = p.UpdatedOn,
                        VATRate = p.VATRate,
                        SubjectToVAT = p.SubjectToVAT,
                        CreditOpeningAccount = p.CreditOpeningAccount,
                        DepitOpeningAccount = p.DepitOpeningAccount,
                        Company_Branch_ID = p.Company_Branch_ID,
                        TaxAccountID = p.TaxAccountID,
                        GROUP_ID = p.GROUP_ID
                    };
            return q.FirstOrDefault();
        }

        public ACCOUNTS GetParentData(int? ParentID)
        {
            var parent = from a in _accountsRepo.GetAll().Where(a => a.ACC_ID == ParentID)
                         select new ACCOUNTS
                         {
                             ACC_ID = a.ACC_ID,
                             AddedBy = a.AddedBy,
                             AddedOn = a.AddedOn,
                             ACC_AR_NAME = a.ACC_AR_NAME,
                             ACC_CHECK_DATE = a.ACC_CHECK_DATE,
                             ACC_CODE = a.ACC_CODE,
                             ACC_CREDIT = a.ACC_CREDIT,
                             ACC_CUSTOMER_CODE = a.ACC_CUSTOMER_CODE,
                             ACC_DATE = a.ACC_DATE,
                             ACC_DEBIT = a.ACC_DEBIT,
                             ACC_DEBIT_OR_CREDIT_OR_WITHOUT = a.ACC_DEBIT_OR_CREDIT_OR_WITHOUT,
                             ACC_EN_NAME = a.ACC_EN_NAME,
                             ACC_LEVEL = a.ACC_LEVEL,
                             ACC_MAX_DEBIT = a.ACC_MAX_DEBIT,
                             ACC_MAX_CREDIT = a.ACC_MAX_CREDIT,
                             ACC_NSONS = a.ACC_NSONS,
                             ACC_REMARKS = a.ACC_REMARKS,
                             ACC_STATE = a.ACC_STATE,
                             ACC_TURNNING_YES_OR_NO = a.ACC_TURNNING_YES_OR_NO,
                             ACC_TYPE_ID = a.ACC_TYPE_ID,
                             CURRENCY_ID = a.CURRENCY_ID,
                             Deactivate = a.Deactivate,
                             FINAL_ACC_ID = a.FINAL_ACC_ID,
                             PARENT_ACC_ID = a.PARENT_ACC_ID,
                             UpdatedOn = a.UpdatedOn,
                             VATRate = a.VATRate,
                             SubjectToVAT = a.SubjectToVAT,
                             CreditOpeningAccount = a.CreditOpeningAccount,
                             DepitOpeningAccount = a.DepitOpeningAccount,
                             Company_Branch_ID = a.Company_Branch_ID,
                             TaxAccountID = a.TaxAccountID,
                             GROUP_ID = a.GROUP_ID

                         };
            return parent.FirstOrDefault();
        }


        public int? UpdateParent(int? ParentID, char Operation)
        {
            int? level = 0;
            ACCOUNTS parent = GetParentData(ParentID);
            if (parent != null)
            {
                short sons = parent.ACC_NSONS;
                if (Operation == 'a')
                {
                    sons = Convert.ToInt16(parent.ACC_NSONS + 1);
                }
                else if (Operation == 'm')
                {
                    sons = Convert.ToInt16(parent.ACC_NSONS - 1);
                }

                ACCOUNTS acc = new ACCOUNTS
                {
                    ACC_ID = parent.ACC_ID,
                    AddedBy = parent.AddedBy,
                    AddedOn = parent.AddedOn,
                    ACC_AR_NAME = parent.ACC_AR_NAME,
                    ACC_CHECK_DATE = parent.ACC_CHECK_DATE,
                    ACC_CODE = parent.ACC_CODE,
                    ACC_CREDIT = parent.ACC_CREDIT,
                    ACC_CUSTOMER_CODE = parent.ACC_CUSTOMER_CODE,
                    ACC_DATE = parent.ACC_DATE,
                    ACC_DEBIT = parent.ACC_DEBIT,
                    ACC_DEBIT_OR_CREDIT_OR_WITHOUT = parent.ACC_DEBIT_OR_CREDIT_OR_WITHOUT,
                    ACC_EN_NAME = parent.ACC_EN_NAME,
                    ACC_LEVEL = parent.ACC_LEVEL,
                    ACC_MAX_DEBIT = parent.ACC_MAX_DEBIT,
                    ACC_MAX_CREDIT = parent.ACC_MAX_CREDIT,
                    ACC_NSONS = sons,
                    ACC_REMARKS = parent.ACC_REMARKS,
                    ACC_STATE = parent.ACC_STATE,
                    ACC_TURNNING_YES_OR_NO = parent.ACC_TURNNING_YES_OR_NO,
                    ACC_TYPE_ID = parent.ACC_TYPE_ID,
                    CURRENCY_ID = parent.CURRENCY_ID,
                    Deactivate = parent.Deactivate,
                    FINAL_ACC_ID = parent.FINAL_ACC_ID,
                    PARENT_ACC_ID = parent.PARENT_ACC_ID,
                    UpdatedOn = parent.UpdatedOn,
                    VATRate = parent.VATRate,
                    SubjectToVAT = parent.SubjectToVAT,
                    CreditOpeningAccount = parent.CreditOpeningAccount,
                    DepitOpeningAccount = parent.DepitOpeningAccount,
                    Company_Branch_ID = parent.Company_Branch_ID,
                    TaxAccountID = parent.TaxAccountID,
                    GROUP_ID = parent.GROUP_ID
                };

                _accountsRepo.Update(acc, acc.ACC_ID);
                level = parent.ACC_LEVEL;
            }
            return level;
        }

        public string GetLastCode()
        {
            var Code = _accountsRepo.GetAll().OrderByDescending(c => c.ACC_ID).FirstOrDefault();
            string lastCode = Code.ACC_CODE;
            return lastCode;
        }


        //////////////////////////////////////////////////////
        //get default code of account without parent 
        public string GetCodeWithoutParent()
        {
            var lastCode = _accountsRepo.Filter(a => a.PARENT_ACC_ID == null).OrderByDescending(a => a.ACC_ID).FirstOrDefault();
            string ACC_CODE = lastCode.ACC_CODE;
            return ACC_CODE;
        }

        public Task<bool> GetByAccountcode(string AccountCode)
        {
            return Task.Run<bool>(() =>
            {
                var acc = _accountsRepo.Filter(X => X.ACC_CODE == AccountCode).FirstOrDefault();
                if (acc != null)
                {
                    return true;
                }
                return false;
            });
        }




        public AccountVM GetParentDataByID(int ParentID)
        {
            var parent = from a in _accountsRepo.GetAll().Where(a => a.ACC_ID == ParentID)
                         select new AccountVM
                         {
                             ACC_ID = a.ACC_ID,
                             AddedBy = a.AddedBy,
                             AddedOn = a.AddedOn,
                             ACC_AR_NAME = a.ACC_AR_NAME,
                             ACC_CHECK_DATE = a.ACC_CHECK_DATE,
                             ACC_CODE = a.ACC_CODE,
                             ACC_CREDIT = a.ACC_CREDIT,
                             ACC_CUSTOMER_CODE = a.ACC_CUSTOMER_CODE,
                             ACC_DATE = a.ACC_DATE,
                             ACC_DEBIT = a.ACC_DEBIT,
                             ACC_DEBIT_OR_CREDIT_OR_WITHOUT = a.ACC_DEBIT_OR_CREDIT_OR_WITHOUT,
                             ACC_EN_NAME = a.ACC_EN_NAME,
                             ACC_LEVEL = a.ACC_LEVEL,
                             ACC_MAX_DEBIT = a.ACC_MAX_DEBIT,
                             ACC_MAX_CREDIT = a.ACC_MAX_CREDIT,
                             ACC_NSONS = a.ACC_NSONS,
                             ACC_REMARKS = a.ACC_REMARKS,
                             ACC_STATE = a.ACC_STATE,
                             ACC_TURNNING_YES_OR_NO = a.ACC_TURNNING_YES_OR_NO,
                             ACC_TYPE_ID = a.ACC_TYPE_ID,
                             CURRENCY_ID = a.CURRENCY_ID,
                             Deactivate = a.Deactivate,
                             FINAL_ACC_ID = a.FINAL_ACC_ID,
                             PARENT_ACC_ID = a.PARENT_ACC_ID,
                             UpdatedOn = a.UpdatedOn,
                             VATRate = a.VATRate,
                             SubjectToVAT = a.SubjectToVAT,
                             CreditOpeningAccount = a.CreditOpeningAccount,
                             DepitOpeningAccount = a.DepitOpeningAccount,
                             Company_Branch_ID = a.Company_Branch_ID,
                             TaxAccountID = a.TaxAccountID,
                             GROUP_ID = a.GROUP_ID

                         };

            var parentAccount = parent.FirstOrDefault();

            var sonCode = "";
            if (parentAccount.ACC_NSONS == 0)
            {
                sonCode = parentAccount.ACC_CODE + "1";
                parentAccount.SonCode = sonCode;
            }
            else if (parentAccount.ACC_NSONS > 0)
            {
                var lastSon = _accountsRepo.Filter(a => a.PARENT_ACC_ID == parentAccount.ACC_ID).OrderByDescending(a => a.ACC_ID).FirstOrDefault();
                if (lastSon != null)
                {
                    sonCode = (int.Parse(lastSon.ACC_CODE) + 1).ToString();
                    parentAccount.SonCode = sonCode;
                }
                else
                {
                    parentAccount.SonCode = GetCodeWithoutParent();
                }
            }



            return parentAccount;
        }


        public string checkAccountIfUsed(int accountID)
        {
            string errorMessage = "";
            var parentAccount = _accountsRepo.Filter(a => a.PARENT_ACC_ID == accountID).Count();
            if (parentAccount > 0)
            {
                errorMessage = "هذا الحساب مستخدم كحساب رئيسى لا يمكن حذفه";
            }

            var billMaster = billMasterRepo.Filter(b => b.CUST_ACC_ID == accountID).Count();
            if (billMaster > 0)
            {
                errorMessage = "هذا الحساب مستخدم فى الفواتير لا يمكن حذفة";
            }

            var custBranches = customerBranchRepo.Filter(c => c.ACC_ID == accountID).Count();
            if (custBranches > 0)
            {
                errorMessage = "هذا الحساب مستخدم فى فروع العملاء لا يمكن حذفة";
            }

            var entryDetails = entryDetailsRepo.Filter(e => e.ACC_ID == accountID).Count();
            bool check = CheckEntryAccount(accountID);
            if (entryDetails > 0 && !check)
            {
                errorMessage = "هذا الحساب مستخدم فى السندات لا يمكن حذفة";
            }

            var entryMaster = entryMasterRepo.Filter(e => e.ACC_ID == accountID).Count();
            if (entryMaster > 0)
            {
                errorMessage = "هذا الحساب مستخدم فى السندات لا يمكن حذفة";
            }

            var entrySetting = entrySettingRepo.Filter(e => e.ENTRY_ACC_ID == accountID).Count();
            if (entrySetting > 0)
            {
                errorMessage = "هذا الحساب مستخدم فى السندات لا يمكن حذفة";
            }
            return errorMessage;
        }


        //public string GetSonCode (int ParentID)
        //{
        //    var parentSons = _accountsRepo.
        //}



        //entry

        public bool CheckEntryAccount(int accountID)
        {
            var entryDetails = (from entry in entryMasterRepo.GetAll()
                                join det in entryDetailsRepo.GetAll() on entry.ENTRY_ID equals det.ENTRY_ID into group1
                                from g1 in group1.DefaultIfEmpty()
                                where entry.OpeningAccID == accountID && g1.ACC_ID == accountID
                                select new Entry_MasterVM
                                {
                                    ENTRY_ID = entry.ENTRY_ID,
                                }
            ).FirstOrDefault();

            if (entryDetails != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }



        public List<ENTRY_DETAILS> GetAllDetails(long EntryMasterID)
        {
            List<ENTRY_DETAILS> entryDetails = new List<ENTRY_DETAILS>();
            entryDetails = entryDetailsRepo.GetAll().Where(x => x.ENTRY_ID == EntryMasterID).ToList();

            return entryDetails;
        }



        public void deleteEntryOfAccount(ENTRY_MASTER entity)
        {

            ENTRY_MASTER et = new ENTRY_MASTER
            {
                ACC_ID = entity.ACC_ID,
                CustAccID = entity.CustAccID,
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
                CustID = entity.CustID



            };

            entryMasterRepo.Delete(et, entity.ENTRY_ID);

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
                        entryDetailsRepo.DeleteComposite(item, keys);
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }
        }

        public void GetTreeView(List<AccountsGuidVM> list, AccountsGuidVM current, ref List<AccountsGuidVM> result)
        {
            // get child of current items

            var childs = list.Where(a => a.PARENT_ACC_ID == current.id);
            current.children = new List<AccountsGuidVM>();
            current.children.AddRange(childs);
            foreach (var i in childs)
            {
                GetTreeView(list, i, ref result);
            }
        }

        public List<AccountsGuidVM> Buildtree(List<AccountsGuidVM> list)
        {
            List<AccountsGuidVM> result = new List<AccountsGuidVM>();
            // find top levels items
            var toplevels = list.Where(a => a.PARENT_ACC_ID == list.OrderBy(b => b.PARENT_ACC_ID).FirstOrDefault().PARENT_ACC_ID);
            result.AddRange(toplevels);
            foreach (var i in toplevels)
            {
                GetTreeView(list, i, ref result);
            }
            return result;
        }

        public List<AccountsGuidVM> buildAccTree()
        {
            List<AccountsGuidVM> treelist = new List<AccountsGuidVM>();
            var q = from a in _accountsRepo.GetAll()
                    select new AccountsGuidVM
                    {
                        id = a.ACC_ID,
                        text = a.ACC_CODE + " - " + a.ACC_AR_NAME,
                        PARENT_ACC_ID = a.PARENT_ACC_ID,
                        data = new AccountsDataGuidVM { data = a.ACC_STATE }
                    };


            if (q.ToList().Count > 0)
            {
                treelist = Buildtree(q.ToList());
            }
            return treelist;
        }





        ////////////////////////////////////////////////accounts by types 



        public List<AccountVM> getAllBankAccounts()
        {

            var q = from entity in _accountsRepo.GetAll().Where(x => (x.ACC_TYPE_ID == (int)AccountEnum.Acc8 || x.ACC_TYPE_ID == (int)AccountEnum.Acc9) && x.PARENT_ACC_ID != null)
                    select new AccountVM
                    {
                        ACC_ID = entity.ACC_ID,
                        AddedBy = entity.AddedBy,
                        AddedOn = entity.AddedOn,
                        ACC_AR_NAME = entity.ACC_AR_NAME,
                        ACC_CHECK_DATE = entity.ACC_CHECK_DATE,
                        ACC_CODE = entity.ACC_CODE,
                        ACC_CREDIT = entity.ACC_CREDIT,
                        ACC_CUSTOMER_CODE = entity.ACC_CUSTOMER_CODE,
                        ACC_DATE = entity.ACC_DATE,
                        ACC_DEBIT = entity.ACC_DEBIT,
                        ACC_DEBIT_OR_CREDIT_OR_WITHOUT = entity.ACC_DEBIT_OR_CREDIT_OR_WITHOUT,
                        ACC_EN_NAME = entity.ACC_EN_NAME,
                        ACC_LEVEL = entity.ACC_LEVEL,
                        ACC_MAX_DEBIT = entity.ACC_MAX_DEBIT,
                        ACC_MAX_CREDIT = entity.ACC_MAX_CREDIT,
                        ACC_NSONS = entity.ACC_NSONS,
                        ACC_REMARKS = entity.ACC_REMARKS,
                        ACC_STATE = entity.ACC_STATE,
                        ACC_TURNNING_YES_OR_NO = entity.ACC_TURNNING_YES_OR_NO,
                        ACC_TYPE_ID = entity.ACC_TYPE_ID,
                        CURRENCY_ID = entity.CURRENCY_ID,
                        Deactivate = entity.Deactivate,
                        FINAL_ACC_ID = entity.FINAL_ACC_ID,
                        PARENT_ACC_ID = entity.PARENT_ACC_ID,
                        UpdatedOn = entity.UpdatedOn,
                        VATRate = entity.VATRate,
                        SubjectToVAT = entity.SubjectToVAT,
                        CreditOpeningAccount = entity.CreditOpeningAccount,
                        DepitOpeningAccount = entity.DepitOpeningAccount,
                        TaxAccountID = entity.TaxAccountID,
                        GROUP_ID = entity.GROUP_ID
                    };
            return q.ToList();
        }

        ////////////////////////////////////////entry accounts//////////////////////////////////////

        public List<AccountVM> GetAccountBoxByTypesForEntry(int EntryType)
        {
            //cash type Cash Receipt Entry & Cash Pay Entry
            ////////if (EntryType == 1 || EntryType == 2)
            ////////{
            ////////    var q = from entity in _accountsRepo.GetAll().Where(x => (x.ACC_TYPE_ID == (int)AccountEnum.Acc7 || x.ACC_TYPE_ID == (int)AccountEnum.Acc9) && x.ACC_STATE == 2)
            ////////            select new AccountVM
            ////////            {
            ////////                ACC_ID = entity.ACC_ID,
            ////////                AddedBy = entity.AddedBy,
            ////////                AddedOn = entity.AddedOn,
            ////////                ACC_AR_NAME = entity.ACC_AR_NAME,
            ////////                ACC_CHECK_DATE = entity.ACC_CHECK_DATE,
            ////////                ACC_CODE = entity.ACC_CODE,
            ////////                ACC_CREDIT = entity.ACC_CREDIT,
            ////////                ACC_CUSTOMER_CODE = entity.ACC_CUSTOMER_CODE,
            ////////                ACC_DATE = entity.ACC_DATE,
            ////////                ACC_DEBIT = entity.ACC_DEBIT,
            ////////                ACC_DEBIT_OR_CREDIT_OR_WITHOUT = entity.ACC_DEBIT_OR_CREDIT_OR_WITHOUT,
            ////////                ACC_EN_NAME = entity.ACC_EN_NAME,
            ////////                ACC_LEVEL = entity.ACC_LEVEL,
            ////////                ACC_MAX_DEBIT = entity.ACC_MAX_DEBIT,
            ////////                ACC_MAX_CREDIT = entity.ACC_MAX_CREDIT,
            ////////                ACC_NSONS = entity.ACC_NSONS,
            ////////                ACC_REMARKS = entity.ACC_REMARKS,
            ////////                ACC_STATE = entity.ACC_STATE,
            ////////                ACC_TURNNING_YES_OR_NO = entity.ACC_TURNNING_YES_OR_NO,
            ////////                ACC_TYPE_ID = entity.ACC_TYPE_ID,
            ////////                CURRENCY_ID = entity.CURRENCY_ID,
            ////////                Deactivate = entity.Deactivate,
            ////////                FINAL_ACC_ID = entity.FINAL_ACC_ID,
            ////////                PARENT_ACC_ID = entity.PARENT_ACC_ID,
            ////////                UpdatedOn = entity.UpdatedOn,
            ////////                VATRate = entity.VATRate,
            ////////                SubjectToVAT = entity.SubjectToVAT,
            ////////                CreditOpeningAccount = entity.CreditOpeningAccount,
            ////////                DepitOpeningAccount = entity.DepitOpeningAccount
            ////////            };
            ////////    return q.ToList();
            ////////}

            //////////bank accounts Check Pay Entry & Check Receipt Entry
            ////////else if (EntryType == 4 || EntryType == 5)
            ////////{
            ////////    var q = from entity in _accountsRepo.GetAll().Where(x => (x.ACC_TYPE_ID == (int)AccountEnum.Acc8 || x.ACC_TYPE_ID == (int)AccountEnum.Acc9) && x.ACC_STATE == 2)
            ////////            select new AccountVM
            ////////            {
            ////////                ACC_ID = entity.ACC_ID,
            ////////                AddedBy = entity.AddedBy,
            ////////                AddedOn = entity.AddedOn,
            ////////                ACC_AR_NAME = entity.ACC_AR_NAME,
            ////////                ACC_CHECK_DATE = entity.ACC_CHECK_DATE,
            ////////                ACC_CODE = entity.ACC_CODE,
            ////////                ACC_CREDIT = entity.ACC_CREDIT,
            ////////                ACC_CUSTOMER_CODE = entity.ACC_CUSTOMER_CODE,
            ////////                ACC_DATE = entity.ACC_DATE,
            ////////                ACC_DEBIT = entity.ACC_DEBIT,
            ////////                ACC_DEBIT_OR_CREDIT_OR_WITHOUT = entity.ACC_DEBIT_OR_CREDIT_OR_WITHOUT,
            ////////                ACC_EN_NAME = entity.ACC_EN_NAME,
            ////////                ACC_LEVEL = entity.ACC_LEVEL,
            ////////                ACC_MAX_DEBIT = entity.ACC_MAX_DEBIT,
            ////////                ACC_MAX_CREDIT = entity.ACC_MAX_CREDIT,
            ////////                ACC_NSONS = entity.ACC_NSONS,
            ////////                ACC_REMARKS = entity.ACC_REMARKS,
            ////////                ACC_STATE = entity.ACC_STATE,
            ////////                ACC_TURNNING_YES_OR_NO = entity.ACC_TURNNING_YES_OR_NO,
            ////////                ACC_TYPE_ID = entity.ACC_TYPE_ID,
            ////////                CURRENCY_ID = entity.CURRENCY_ID,
            ////////                Deactivate = entity.Deactivate,
            ////////                FINAL_ACC_ID = entity.FINAL_ACC_ID,
            ////////                PARENT_ACC_ID = entity.PARENT_ACC_ID,
            ////////                UpdatedOn = entity.UpdatedOn,
            ////////                VATRate = entity.VATRate,
            ////////                SubjectToVAT = entity.SubjectToVAT,
            ////////                CreditOpeningAccount = entity.CreditOpeningAccount,
            ////////                DepitOpeningAccount = entity.DepitOpeningAccount
            ////////            };
            ////////    return q.ToList();
            ////////}



            //else
            //{
            var q = from entity in _accountsRepo.GetAll().Where(x => x.ACC_STATE == 2)
                    select new AccountVM
                    {
                        ACC_ID = entity.ACC_ID,
                        AddedBy = entity.AddedBy,
                        AddedOn = entity.AddedOn,
                        ACC_AR_NAME = entity.ACC_AR_NAME,
                        ACC_CHECK_DATE = entity.ACC_CHECK_DATE,
                        ACC_CODE = entity.ACC_CODE,
                        ACC_CREDIT = entity.ACC_CREDIT,
                        ACC_CUSTOMER_CODE = entity.ACC_CUSTOMER_CODE,
                        ACC_DATE = entity.ACC_DATE,
                        ACC_DEBIT = entity.ACC_DEBIT,
                        ACC_DEBIT_OR_CREDIT_OR_WITHOUT = entity.ACC_DEBIT_OR_CREDIT_OR_WITHOUT,
                        ACC_EN_NAME = entity.ACC_EN_NAME,
                        ACC_LEVEL = entity.ACC_LEVEL,
                        ACC_MAX_DEBIT = entity.ACC_MAX_DEBIT,
                        ACC_MAX_CREDIT = entity.ACC_MAX_CREDIT,
                        ACC_NSONS = entity.ACC_NSONS,
                        ACC_REMARKS = entity.ACC_REMARKS,
                        ACC_STATE = entity.ACC_STATE,
                        ACC_TURNNING_YES_OR_NO = entity.ACC_TURNNING_YES_OR_NO,
                        ACC_TYPE_ID = entity.ACC_TYPE_ID,
                        CURRENCY_ID = entity.CURRENCY_ID,
                        Deactivate = entity.Deactivate,
                        FINAL_ACC_ID = entity.FINAL_ACC_ID,
                        PARENT_ACC_ID = entity.PARENT_ACC_ID,
                        UpdatedOn = entity.UpdatedOn,
                        VATRate = entity.VATRate,
                        SubjectToVAT = entity.SubjectToVAT,
                        CreditOpeningAccount = entity.CreditOpeningAccount,
                        DepitOpeningAccount = entity.DepitOpeningAccount,
                        TaxAccountID = entity.TaxAccountID,
                        GROUP_ID = entity.GROUP_ID
                    };
            return q.ToList();
            //}

        }


        /////////////////////////////////////////////////bill accounts /////////////////////////////////////
        public Task<List<ACCOUNTS>> GetAccountBoxByTypesForBill(int BillType, string AccType, int CompBran)
        {
            return Task.Run<List<ACCOUNTS>>(() =>
            {

                var query1 = sysOptRepo.GetAll().FirstOrDefault();

                if (AccType == "gold")
                {
                    if (query1.filterWithCompanyBranch == false)
                    {
                        return _accountsRepo.Filter(x => x.ACC_STATE == 2 && x.ACC_TYPE_ID == (int)AccountEnum.Acc10).ToList();
                    }
                    else
                    {
                        if (CompBran != 0)
                        {
                            return _accountsRepo.Filter(x => x.ACC_STATE == 2 && x.ACC_TYPE_ID == (int)AccountEnum.Acc10 && x.Company_Branch_ID == CompBran).ToList();

                        }
                        else
                        {
                            return _accountsRepo.Filter(x => x.ACC_STATE == 2 && x.ACC_TYPE_ID == (int)AccountEnum.Acc10).ToList();
                        }

                    }
                }

                if (AccType == "pay")
                {
                    var q = _accountsRepo.Filter(x => x.ACC_STATE == 2 && (x.ACC_TYPE_ID == (int)AccountEnum.Acc7 || x.ACC_TYPE_ID == (int)AccountEnum.Acc8 || x.ACC_TYPE_ID == (int)AccountEnum.Acc9)).ToList();
                    return q;
                }



                //cash type Cash Receipt Entry & Cash Pay Entry
                if (BillType == 1 || BillType == 2)
                {
                    if (AccType == "cash")
                    {
                        var q = _accountsRepo.Filter(x => x.ACC_STATE == 2 && (x.ACC_TYPE_ID == (int)AccountEnum.Acc7 || x.ACC_TYPE_ID == (int)AccountEnum.Acc8 || x.ACC_TYPE_ID == (int)AccountEnum.Acc9)).ToList();
                        return q;
                    }

                    else if (AccType == "cust")
                    {
                        if (BillType == 1)
                        {
                            var q = _accountsRepo.Filter(x => x.ACC_STATE == 2 && (x.ACC_TYPE_ID == (int)AccountEnum.Acc5 || x.ACC_TYPE_ID == (int)AccountEnum.Acc6 || x.ACC_TYPE_ID == (int)AccountEnum.Acc7 || x.ACC_TYPE_ID == (int)AccountEnum.Acc8)).ToList();
                            return q;
                        }
                        else
                        {
                            var q = _accountsRepo.Filter(x => x.ACC_STATE == 2 && (x.ACC_TYPE_ID == (int)AccountEnum.Acc4 || x.ACC_TYPE_ID == (int)AccountEnum.Acc6)).ToList();
                            return q;
                        }
                    }
                    else
                    {
                        var q = _accountsRepo.Filter(x => x.ACC_STATE == 2).ToList();
                        return q;
                    }
                }

                else if (BillType == 19 || BillType == 20)
                {
                    if (AccType == "cash")
                    {
                        var q = _accountsRepo.Filter(x => x.ACC_STATE == 2 && (x.ACC_TYPE_ID == (int)AccountEnum.Acc7 || x.ACC_TYPE_ID == (int)AccountEnum.Acc9)).ToList();
                        return q;
                    }

                    else if (AccType == "cust")
                    {
                        var q = _accountsRepo.Filter(x => x.ACC_STATE == 2 && (x.ACC_TYPE_ID == (int)AccountEnum.Acc5 || x.ACC_TYPE_ID == (int)AccountEnum.Acc6 || x.ACC_TYPE_ID == (int)AccountEnum.Acc11)).ToList();
                        return q;
                    }
                    else
                    {
                        var q = _accountsRepo.Filter(x => x.ACC_STATE == 2).ToList();
                        return q;
                    }
                }


                else if (BillType == 13 || BillType == 14 || BillType == 23 || BillType == 24)
                {
                    if (AccType == "cash")
                    {
                        var q = _accountsRepo.Filter(x => x.ACC_STATE == 2 && (x.ACC_TYPE_ID == (int)AccountEnum.Acc7 || x.ACC_TYPE_ID == (int)AccountEnum.Acc8 || x.ACC_TYPE_ID == (int)AccountEnum.Acc9)).ToList();
                        return q;
                    }


                    else if (AccType == "cust")
                    {
                        var q = _accountsRepo.Filter(x => x.ACC_STATE == 2 && (x.ACC_TYPE_ID == (int)AccountEnum.Acc4 || x.ACC_TYPE_ID == (int)AccountEnum.Acc5 || x.ACC_TYPE_ID == (int)AccountEnum.Acc6 || x.ACC_TYPE_ID == (int)AccountEnum.Acc11)).ToList();
                        return q;
                    }
                    else
                    {
                        var q = _accountsRepo.Filter(x => x.ACC_STATE == 2).ToList();
                        return q;
                    }
                }


                else
                {
                    var q = _accountsRepo.Filter(x => x.ACC_STATE == 2).ToList();
                    return q;
                }
            });
        }



        public Task<List<ACCOUNTS>> GetGoldBoxByTypesForBill(string AccType, int CompBran)
        {
            return Task.Run<List<ACCOUNTS>>(() =>
            {

                var query1 = sysOptRepo.GetAll().FirstOrDefault();

                if (AccType == "gold")
                {
                    if (query1.filterWithCompanyBranch == false)
                    {
                        return _accountsRepo.Filter(x => x.ACC_STATE == 2 && x.ACC_TYPE_ID == (int)AccountEnum.Acc10).ToList();
                    }
                    else
                    {
                        if (CompBran != 0)
                        {
                            return _accountsRepo.Filter(x => x.ACC_STATE == 2 && x.ACC_TYPE_ID == (int)AccountEnum.Acc10 && x.Company_Branch_ID == CompBran).ToList();

                        }
                        else
                        {
                            return _accountsRepo.Filter(x => x.ACC_STATE == 2 && x.ACC_TYPE_ID == (int)AccountEnum.Acc10).ToList();
                        }

                    }
                }

                else
                {
                    var q = _accountsRepo.Filter(x => x.ACC_STATE == 2).ToList();
                    return q;
                }
            });
        }

        public List<ACCOUNTS> GetAccountsFilteredByType(string AccType)
        {
            if(AccType == "Fund")
            {
                return _accountsRepo.Filter(x => x.ACC_TYPE_ID == (int)AccountEnum.Acc7 ).ToList();
            }
            else if (AccType == "GoldBox")
            {
                return _accountsRepo.Filter(x => x.ACC_TYPE_ID == (int)AccountEnum.Acc10).ToList();
            }
            else
            {
                return _accountsRepo.GetAll().ToList();
            }
        }

        //////////////////////////////////////////////////////default accounts of bill settings//////////////////////

        public List<ACCOUNTS> GetDefaultAccountsOfBillSettings(string AccType)
        {
            //cash type Cash Receipt Entry & Cash Pay Entry

            if (AccType == "extra")
            {
                var q = _accountsRepo.Filter(x => x.ACC_STATE == 2).ToList();
                return q;
            }

            else if (AccType == "pay")
            {
                var q = _accountsRepo.Filter(x => x.ACC_STATE == 2 && (x.ACC_TYPE_ID == (int)AccountEnum.Acc7 || x.ACC_TYPE_ID == (int)AccountEnum.Acc8)).ToList();
                return q;
            }

            else if (AccType == "cost")
            {
                var q = _accountsRepo.Filter(x => x.ACC_STATE == 2 && x.PARENT_ACC_ID == 6).ToList();
                return q;
            }

            else if (AccType == "store")
            {
                var q = _accountsRepo.Filter(x => x.ACC_STATE == 2 && x.PARENT_ACC_ID == 11).ToList();
                return q;
            }

            else if (AccType == "gift")
            {
                var q = _accountsRepo.Filter(x => x.ACC_STATE == 2 && x.PARENT_ACC_ID == 8).ToList();
                return q;
            }

            else if (AccType == "wages")
            {
                var q = _accountsRepo.Filter(x => x.ACC_STATE == 2 && x.PARENT_ACC_ID == 8).ToList();
                return q;
            }

            else if (AccType == "sales")
            {
                var q = _accountsRepo.Filter(x => x.ACC_STATE == 2 && (x.PARENT_ACC_ID == 9 || x.PARENT_ACC_ID == 41)).ToList();
                return q;
            }

            else if (AccType == "vat")
            {
                var q = _accountsRepo.Filter(x => x.ACC_STATE == 2 && (x.PARENT_ACC_ID == 11 || x.PARENT_ACC_ID == 23)).ToList();
                return q;
            }

            else if (AccType == "purchas")
            {
                var q = _accountsRepo.Filter(x => x.ACC_STATE == 2 && (x.PARENT_ACC_ID == 8 || x.PARENT_ACC_ID == 36)).ToList();
                return q;
            }

            else if (AccType == "purchasTax")
            {
                var q = _accountsRepo.Filter(x => x.ACC_STATE == 2 && (x.PARENT_ACC_ID == 11 || x.PARENT_ACC_ID == 23)).ToList();
                return q;
            }

            else if (AccType == "comm")
            {
                var q = _accountsRepo.Filter(x => x.ACC_STATE == 2 && x.PARENT_ACC_ID == 8).ToList();
                return q;
            }

            else if (AccType == "commTax")
            {
                var q = _accountsRepo.Filter(x => x.ACC_STATE == 2 && (x.PARENT_ACC_ID == 11 || x.PARENT_ACC_ID == 23)).ToList();
                return q;
            }

            else if (AccType == "cust")
            {
                var q = _accountsRepo.Filter(x => x.ACC_STATE == 2 && (x.ACC_TYPE_ID == (int)AccountEnum.Acc4 || x.ACC_TYPE_ID == (int)AccountEnum.Acc5 || x.ACC_TYPE_ID == (int)AccountEnum.Acc6 || x.ACC_TYPE_ID == (int)AccountEnum.Acc11)).ToList();
                return q;
            }

            else if (AccType == "gold")
            {
                var q = _accountsRepo.Filter(x => x.ACC_STATE == 2 && x.ACC_TYPE_ID == (int)AccountEnum.Acc10).ToList();
                return q;
            }


            else
            {
                var q = _accountsRepo.Filter(x => x.ACC_STATE == 2).ToList();
                return q;
            }

        }


        /////////////////////////////get all main accounts
        public Task<List<AccountVM>> GetAllMainAccounts()
        {

            return Task.Run<List<AccountVM>>(() =>
            {
                var q = from entity in _accountsRepo.Filter(x => x.ACC_STATE == 1)
                        select new AccountVM
                        {
                            ACC_ID = entity.ACC_ID,
                            AddedBy = entity.AddedBy,
                            AddedOn = entity.AddedOn,
                            ACC_AR_NAME = entity.ACC_AR_NAME,
                            ACC_CHECK_DATE = entity.ACC_CHECK_DATE,
                            ACC_CODE = entity.ACC_CODE,
                            ACC_CREDIT = entity.ACC_CREDIT,
                            ACC_CUSTOMER_CODE = entity.ACC_CUSTOMER_CODE,
                            ACC_DATE = entity.ACC_DATE,
                            ACC_DEBIT = entity.ACC_DEBIT,
                            ACC_DEBIT_OR_CREDIT_OR_WITHOUT = entity.ACC_DEBIT_OR_CREDIT_OR_WITHOUT,
                            ACC_EN_NAME = entity.ACC_EN_NAME,
                            ACC_LEVEL = entity.ACC_LEVEL,
                            ACC_MAX_DEBIT = entity.ACC_MAX_DEBIT,
                            ACC_MAX_CREDIT = entity.ACC_MAX_CREDIT,
                            ACC_NSONS = entity.ACC_NSONS,
                            ACC_REMARKS = entity.ACC_REMARKS,
                            ACC_STATE = entity.ACC_STATE,
                            ACC_TURNNING_YES_OR_NO = entity.ACC_TURNNING_YES_OR_NO,
                            ACC_TYPE_ID = entity.ACC_TYPE_ID,
                            CURRENCY_ID = entity.CURRENCY_ID,
                            Deactivate = entity.Deactivate,
                            FINAL_ACC_ID = entity.FINAL_ACC_ID,
                            PARENT_ACC_ID = entity.PARENT_ACC_ID,
                            UpdatedOn = entity.UpdatedOn,
                            SubjectToVAT = entity.SubjectToVAT,
                            CreditOpeningAccount = entity.CreditOpeningAccount,
                            DepitOpeningAccount = entity.DepitOpeningAccount,
                            Company_Branch_ID = entity.Company_Branch_ID,
                            TaxAccountID = entity.TaxAccountID,
                            GROUP_ID = entity.GROUP_ID
                        };
                return q.ToList();
            });
        }


        //////////////////// get all customers for entry in complex entry

        public List<AccountVM> GetCustomerAccountsForComplexEntry()
        {//x => x.ACC_TYPE_ID == (int)AccountEnum.Acc4 || x.ACC_TYPE_ID == (int)AccountEnum.Acc5 || x.ACC_TYPE_ID == (int)AccountEnum.Acc6 || x.ACC_TYPE_ID == (int)AccountEnum.Acc11
            var q = from entity in _accountsRepo.GetAll().Where(x => x.ACC_STATE==2)
                    select new AccountVM
                    {
                        ACC_ID = entity.ACC_ID,
                        AddedBy = entity.AddedBy,
                        AddedOn = entity.AddedOn,
                        ACC_AR_NAME = entity.ACC_AR_NAME,
                        ACC_CHECK_DATE = entity.ACC_CHECK_DATE,
                        ACC_CODE = entity.ACC_CODE,
                        ACC_CREDIT = entity.ACC_CREDIT,
                        ACC_CUSTOMER_CODE = entity.ACC_CUSTOMER_CODE,
                        ACC_DATE = entity.ACC_DATE,
                        ACC_DEBIT = entity.ACC_DEBIT,
                        ACC_DEBIT_OR_CREDIT_OR_WITHOUT = entity.ACC_DEBIT_OR_CREDIT_OR_WITHOUT,
                        ACC_EN_NAME = entity.ACC_EN_NAME,
                        ACC_LEVEL = entity.ACC_LEVEL,
                        ACC_MAX_DEBIT = entity.ACC_MAX_DEBIT,
                        ACC_MAX_CREDIT = entity.ACC_MAX_CREDIT,
                        ACC_NSONS = entity.ACC_NSONS,
                        ACC_REMARKS = entity.ACC_REMARKS,
                        ACC_STATE = entity.ACC_STATE,
                        ACC_TURNNING_YES_OR_NO = entity.ACC_TURNNING_YES_OR_NO,
                        ACC_TYPE_ID = entity.ACC_TYPE_ID,
                        CURRENCY_ID = entity.CURRENCY_ID,
                        Deactivate = entity.Deactivate,
                        FINAL_ACC_ID = entity.FINAL_ACC_ID,
                        PARENT_ACC_ID = entity.PARENT_ACC_ID,
                        UpdatedOn = entity.UpdatedOn,
                        VATRate = entity.VATRate,
                        SubjectToVAT = entity.SubjectToVAT,
                        CreditOpeningAccount = entity.CreditOpeningAccount,
                        DepitOpeningAccount = entity.DepitOpeningAccount,
                        TaxAccountID = entity.TaxAccountID,
                        GROUP_ID = entity.GROUP_ID
                    };
            return q.ToList();

        }



        // get search accounts of depit grid 
        public List<AccountVM> SearchAccountsForDepitGrid(string SearchCriteria, int EntryTypeID)
        {
            List<AccountVM> accList = SearchItems(SearchCriteria);
            // var returenList = accList;

            var returenList = accList.Where(x => x.ACC_STATE == 2).ToList();

            //if (EntryTypeID == 1 || EntryTypeID == 5)
            //{
            //    returenList = accList.Where(x => x.ACC_TYPE_ID == (int)AccountEnum.Acc7 || x.ACC_TYPE_ID == (int)AccountEnum.Acc9).ToList();

            //}

            //else if (EntryTypeID == 2 || EntryTypeID == 4)
            //{
            //    returenList = accList.Where(x => x.ACC_TYPE_ID == (int)AccountEnum.Acc4 || x.ACC_TYPE_ID == (int)AccountEnum.Acc5 || x.ACC_TYPE_ID == (int)AccountEnum.Acc6 || x.ACC_TYPE_ID == (int)AccountEnum.Acc11).ToList();

            //}          
            return returenList;

        }


        public List<AccountVM> SearchAccountsForCreditGrid(string SearchCriteria, int EntryTypeID)
        {
            List<AccountVM> accList = SearchItems(SearchCriteria);
            var returenList = accList.Where(x => x.ACC_STATE == 2).ToList();
            //if (EntryTypeID == 2 || EntryTypeID == 4)
            //{
            //    returenList = accList.Where(x => x.ACC_TYPE_ID == (int)AccountEnum.Acc7 || x.ACC_TYPE_ID == (int)AccountEnum.Acc9).ToList();

            //}

            //else if (EntryTypeID == 1 || EntryTypeID == 5)
            //{
            //    returenList = accList.Where(x => x.ACC_TYPE_ID == (int)AccountEnum.Acc4 || x.ACC_TYPE_ID == (int)AccountEnum.Acc5 || x.ACC_TYPE_ID == (int)AccountEnum.Acc6 || x.ACC_TYPE_ID == (int)AccountEnum.Acc11).ToList();

            //}
            return returenList;

        }
    }
}
