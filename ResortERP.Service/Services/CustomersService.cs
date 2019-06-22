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
    public class CustomersService : ICustomersService, IDisposable
    {
        IGenericRepository<CUSTOMERS> customerRepo;
        IGenericRepository<ACCOUNTS> accountRepo;
        IGenericRepository<ACCOUNTS_TYPES> accountTypeRepo;
        IGenericRepository<Currency> currencyRepo;
        IGenericRepository<Telephones> telephoneRepo;
        IGenericRepository<CustomerBranches> customerBranchesRepo;
        IGenericRepository<ENTRY_MASTER> entryMasterRepo;
        IGenericRepository<ENTRY_DETAILS> entryDetailsRepo;



        public CustomersService(IGenericRepository<CUSTOMERS> customerRepo, IGenericRepository<ACCOUNTS> accountRepo, IGenericRepository<Currency> currencyRepo, IGenericRepository<Telephones> telephoneRepo
            , IGenericRepository<CustomerBranches> customerBranchesRepo, IGenericRepository<ACCOUNTS_TYPES> accountTypeRepo, IGenericRepository<ENTRY_MASTER> entryMasterRepo, IGenericRepository<ENTRY_DETAILS> entryDetailsRepo)
        {
            this.customerRepo = customerRepo;
            this.accountRepo = accountRepo;
            this.accountTypeRepo = accountTypeRepo;
            this.currencyRepo = currencyRepo;
            this.telephoneRepo = telephoneRepo;
            this.customerBranchesRepo = customerBranchesRepo;
            this.entryMasterRepo = entryMasterRepo;
            this.entryDetailsRepo = entryDetailsRepo;
        }

        public Task<int> CountAsync(char type)
        {
            return Task.Run<int>(() =>
            {
                SqlParameter TypeParam = new SqlParameter("@type", (object)type);
                var count = customerRepo.SQLQuery<int>("exec GetCustomerSupplierCount  @type", TypeParam).ToList<int>().FirstOrDefault();
                return count;
            });
        }

        public bool Delete(CustomersVM entity)
        {
            CUSTOMERS cus = new CUSTOMERS
            {
                ACC_ID = entity.ACC_ID,
                AddedBy = entity.AddedBy,
                AddedOn = entity.AddedOn,
                ANYCOMMENTS = entity.ANYCOMMENTS,
                ANYCOMPLAINT = entity.ANYCOMPLAINT,
                CARMETERVALUE = entity.CARMETERVALUE,
                CARNUMBER = entity.CARNUMBER,
                CUST_ADDR_REMARKS = entity.CUST_ADDR_REMARKS,
                CUST_EMAIL = entity.CUST_EMAIL,
                CUST_PHOTO = entity.CUST_PHOTO,
                CUST_POST_BOX = entity.CUST_POST_BOX,
                CUST_WEB_SITE = entity.CUST_WEB_SITE,
                CUST_ZIP_CODE = entity.CUST_ZIP_CODE,
                Disable = entity.Disable,
                ENGINENUMBER = entity.ENGINENUMBER,
                GOV_ID = entity.GOV_ID,
                HEARABOUTUS = entity.HEARABOUTUS,
                ISTHISTHEFIRSTTIME = entity.ISTHISTHEFIRSTTIME,
                NATIONALITY_ID = entity.NATIONALITY_ID,
                NATION_ID = entity.NATION_ID,
                SELL_TYPE_ID = entity.SELL_TYPE_ID,
                SHASIHNUMBER = entity.SHASIHNUMBER,
                THINKABOUTSTORE = entity.THINKABOUTSTORE,
                TOWN_ID = entity.TOWN_ID,
                UpdatedBy = entity.UpdatedBy,
                updatedOn = entity.updatedOn,
                VILLAGE_ID = entity.VILLAGE_ID,
                CUST_AR_NAME = entity.CUST_AR_NAME,
                CUST_CODE = entity.CUST_CODE,
                CUST_EN_NAME = entity.CUST_EN_NAME,
                CUST_TITLE = entity.CUST_TITLE,
                Comp_Bran_ID = entity.Comp_Bran_ID,
                Dept_ID = entity.Dept_ID,
                VATRate = entity.VATRate,
                SubjectToVAT = entity.SubjectToVAT,
                TaxNumber = entity.TaxNumber,
                CommercialRegister = entity.CommercialRegister,

                CustomerAdminARName=entity.CustomerAdminARName,
                CustomerAdminENName=entity.CustomerAdminENName,
                CustomerAdminTelephone=entity.CustomerAdminTelephone
            };
            customerRepo.Delete(cus, entity.ACC_ID);
            return true;
        }

        public Task<bool> DeleteAsync(CustomersVM customer)
        {
            return Task.Run<bool>(() =>
            {
                List<Telephones> telephones = telephoneRepo.GetAll().Where(x => x.TELE_ID == customer.ACC_ID && x.TELE_TYPE_ID == 1).ToList();
                if (telephones != null)
                {
                    foreach (var te in telephones)
                    {
                        Telephones tel = new Telephones()
                        {
                            TELE_ID = te.TELE_ID,
                            TELE_CAT_ID = te.TELE_CAT_ID,
                            TELE_NUMBER = te.TELE_NUMBER,
                            TELE_TYPE_ID = te.TELE_TYPE_ID,
                            AddedBy = te.AddedBy,
                            AddedOn = te.AddedOn,
                            UpdatedBy = te.UpdatedBy,
                            updatedOn = te.updatedOn
                        };
                        object[] keys = new object[4] { te.TELE_ID, te.TELE_TYPE_ID, te.TELE_NUMBER, te.TELE_CAT_ID };
                        telephoneRepo.DeleteComposite(tel, keys);
                    }
                }
                List<CustomerBranches> custBran = customerBranchesRepo.GetAll().Where(x => x.ACC_ID == customer.ACC_ID).ToList();
                if (custBran != null)
                {
                    foreach (var cuBr in custBran)
                    {
                        CustomerBranches customBran = new CustomerBranches()
                        {
                            ACC_BRN_AR_NAME = cuBr.ACC_BRN_AR_NAME,
                            ACC_BRN_EN_NAME = cuBr.ACC_BRN_EN_NAME,
                            BRN_ADDR_REMARKS = cuBr.BRN_ADDR_REMARKS,
                            BRN_REMARKS = cuBr.BRN_REMARKS,
                            CUST_BRN_ID = cuBr.CUST_BRN_ID,
                            ACC_ID = cuBr.ACC_ID,
                            GOV_ID = cuBr.GOV_ID,
                            NATION_ID = cuBr.NATION_ID,
                            TOWN_ID = cuBr.TOWN_ID,
                            VILLAGE_ID = cuBr.VILLAGE_ID,
                            AddedBy = cuBr.AddedBy,
                            AddedOn = cuBr.AddedOn,
                            UpdatedBy = cuBr.UpdatedBy,
                            updatedOn = cuBr.updatedOn
                        };
                        customerBranchesRepo.Delete(customBran, cuBr.CUST_BRN_ID);
                    }
                }

                CUSTOMERS cus = new CUSTOMERS
                {
                    ACC_ID = customer.ACC_ID,
                    AddedBy = customer.AddedBy,
                    AddedOn = customer.AddedOn,
                    ANYCOMMENTS = customer.ANYCOMMENTS,
                    ANYCOMPLAINT = customer.ANYCOMPLAINT,
                    CARMETERVALUE = customer.CARMETERVALUE,
                    CARNUMBER = customer.CARNUMBER,
                    CUST_ADDR_REMARKS = customer.CUST_ADDR_REMARKS,
                    CUST_EMAIL = customer.CUST_EMAIL,
                    CUST_PHOTO = customer.CUST_PHOTO,
                    CUST_POST_BOX = customer.CUST_POST_BOX,
                    CUST_WEB_SITE = customer.CUST_WEB_SITE,
                    CUST_ZIP_CODE = customer.CUST_ZIP_CODE,
                    Disable = customer.Disable,
                    ENGINENUMBER = customer.ENGINENUMBER,
                    GOV_ID = customer.GOV_ID,
                    HEARABOUTUS = customer.HEARABOUTUS,
                    ISTHISTHEFIRSTTIME = customer.ISTHISTHEFIRSTTIME,
                    NATIONALITY_ID = customer.NATIONALITY_ID,
                    NATION_ID = customer.NATION_ID,
                    SELL_TYPE_ID = customer.SELL_TYPE_ID,
                    SHASIHNUMBER = customer.SHASIHNUMBER,
                    THINKABOUTSTORE = customer.THINKABOUTSTORE,
                    TOWN_ID = customer.TOWN_ID,
                    UpdatedBy = customer.UpdatedBy,
                    updatedOn = customer.updatedOn,
                    VILLAGE_ID = customer.VILLAGE_ID,
                    CUST_AR_NAME = customer.CUST_AR_NAME,
                    CUST_CODE = customer.CUST_CODE,
                    CUST_EN_NAME = customer.CUST_EN_NAME,
                    CUST_TITLE = customer.CUST_TITLE,
                    Comp_Bran_ID = customer.Comp_Bran_ID,
                    Dept_ID = customer.Dept_ID,
                    VATRate = customer.VATRate,
                    SubjectToVAT = customer.SubjectToVAT,
                    TaxNumber = customer.TaxNumber,
                    CommercialRegister = customer.CommercialRegister,

                    CreditOpeningAccount=customer.CreditOpeningAccount,
                    DepitOpeningAccount=customer.DepitOpeningAccount,

                    CustomerAdminARName = customer.CustomerAdminARName,
                    CustomerAdminENName = customer.CustomerAdminENName,
                    CustomerAdminTelephone = customer.CustomerAdminTelephone
                };
                customerRepo.Delete(cus, customer.ACC_ID);

                if (customer.Account.PARENT_ACC_ID != null)
                {
                    int? level = UpdateParent(customer.Account.PARENT_ACC_ID, 'm');
                }


                if(customer.CreditOpeningAccount != null || customer.DepitOpeningAccount != null)
                {
                    ENTRY_MASTER entryMaster = entryMasterRepo.Filter(e => e.CustID == customer.ACC_ID).FirstOrDefault();
                    if(entryMaster != null)
                    {
                        //delete
                        deleteEntryOfCustomer(entryMaster);
                    }
                }
                return true;
            });
        }

        public void Dispose()
        {
            customerRepo.Dispose();
            accountRepo.Dispose();
            currencyRepo.Dispose();
            telephoneRepo.Dispose();
            customerBranchesRepo.Dispose();
        }

        public List<CustomersVM> GetAll()
        {
            var q = from entity in customerRepo.GetAll()
                    select new CustomersVM
                    {
                        ACC_ID = entity.ACC_ID,
                        AddedBy = entity.AddedBy,
                        AddedOn = entity.AddedOn,
                        ANYCOMMENTS = entity.ANYCOMMENTS,
                        ANYCOMPLAINT = entity.ANYCOMPLAINT,
                        CARMETERVALUE = entity.CARMETERVALUE,
                        CARNUMBER = entity.CARNUMBER,
                        CUST_ADDR_REMARKS = entity.CUST_ADDR_REMARKS,
                        CUST_EMAIL = entity.CUST_EMAIL,
                        CUST_PHOTO = entity.CUST_PHOTO,
                        CUST_POST_BOX = entity.CUST_POST_BOX,
                        CUST_WEB_SITE = entity.CUST_WEB_SITE,
                        CUST_ZIP_CODE = entity.CUST_ZIP_CODE,
                        Disable = entity.Disable,
                        ENGINENUMBER = entity.ENGINENUMBER,
                        GOV_ID = entity.GOV_ID,
                        HEARABOUTUS = entity.HEARABOUTUS,
                        ISTHISTHEFIRSTTIME = entity.ISTHISTHEFIRSTTIME,
                        NATIONALITY_ID = entity.NATIONALITY_ID,
                        NATION_ID = entity.NATION_ID,
                        SELL_TYPE_ID = entity.SELL_TYPE_ID,
                        SHASIHNUMBER = entity.SHASIHNUMBER,
                        THINKABOUTSTORE = entity.THINKABOUTSTORE,
                        TOWN_ID = entity.TOWN_ID,
                        UpdatedBy = entity.UpdatedBy,
                        updatedOn = entity.updatedOn,
                        VILLAGE_ID = entity.VILLAGE_ID,
                        CUST_AR_NAME = entity.CUST_AR_NAME,
                        CUST_CODE = entity.CUST_CODE,
                        CUST_EN_NAME = entity.CUST_EN_NAME,
                        CUST_TITLE = entity.CUST_TITLE,
                        Comp_Bran_ID = entity.Comp_Bran_ID,
                        Dept_ID = entity.Dept_ID,
                        VATRate = entity.VATRate,
                        SubjectToVAT = entity.SubjectToVAT,
                        TaxNumber = entity.TaxNumber,
                        CommercialRegister = entity.CommercialRegister,
                        CreditOpeningAccount = entity.CreditOpeningAccount,
                        DepitOpeningAccount = entity.DepitOpeningAccount,

                        CustomerAdminARName = entity.CustomerAdminARName,
                        CustomerAdminENName = entity.CustomerAdminENName,
                        CustomerAdminTelephone = entity.CustomerAdminTelephone
                    };
            return q.ToList();
        }
        public static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }

        public Task<List<CustomersVM>> GetAllAsync(int pageNum, int pageSize, char type)
        {
            return Task.Run<List<CustomersVM>>(() =>
            {
                int cu = type == 'c' ? 5 : 7;
                int rowCount;
                var q = from entity in customerRepo.GetPaged<long>(pageNum, pageSize, p => p.ACC_ID, false, out rowCount)
                        join Acc in accountRepo.GetPaged<long>(pageNum, pageSize, p => p.ACC_ID, false, out rowCount) on entity.ACC_ID equals Acc.ACC_ID
                        join Cur in currencyRepo.GetPaged<long>(pageNum, pageSize, p => p.CURRENCY_ID, false, out rowCount) on Acc.CURRENCY_ID equals Cur.CURRENCY_ID into g
                        from currency in g.DefaultIfEmpty()
                        select new CustomersVM
                        {
                            ACC_ID = entity.ACC_ID,
                            AddedBy = entity.AddedBy,
                            AddedOn = entity.AddedOn,
                            ANYCOMMENTS = entity.ANYCOMMENTS,
                            ANYCOMPLAINT = entity.ANYCOMPLAINT,
                            CARMETERVALUE = entity.CARMETERVALUE,
                            CARNUMBER = entity.CARNUMBER,
                            CUST_ADDR_REMARKS = entity.CUST_ADDR_REMARKS,
                            CUST_EMAIL = entity.CUST_EMAIL,
                            CUST_PHOTO = entity.CUST_PHOTO,
                            CUST_POST_BOX = entity.CUST_POST_BOX,
                            CUST_WEB_SITE = entity.CUST_WEB_SITE,
                            CUST_ZIP_CODE = entity.CUST_ZIP_CODE,
                            Disable = entity.Disable,
                            ENGINENUMBER = entity.ENGINENUMBER,
                            GOV_ID = entity.GOV_ID,
                            HEARABOUTUS = entity.HEARABOUTUS,
                            ISTHISTHEFIRSTTIME = entity.ISTHISTHEFIRSTTIME,
                            NATIONALITY_ID = entity.NATIONALITY_ID,
                            NATION_ID = entity.NATION_ID,
                            SELL_TYPE_ID = entity.SELL_TYPE_ID,
                            SHASIHNUMBER = entity.SHASIHNUMBER,
                            THINKABOUTSTORE = entity.THINKABOUTSTORE,
                            TOWN_ID = entity.TOWN_ID,
                            UpdatedBy = entity.UpdatedBy,
                            updatedOn = entity.updatedOn,
                            VILLAGE_ID = entity.VILLAGE_ID,
                            CUST_AR_NAME = entity.CUST_AR_NAME,
                            CUST_CODE = entity.CUST_CODE,
                            CUST_EN_NAME = entity.CUST_EN_NAME,
                            CUST_TITLE = entity.CUST_TITLE,
                            Comp_Bran_ID = entity.Comp_Bran_ID,
                            Dept_ID = entity.Dept_ID,
                            VATRate = entity.VATRate,
                            SubjectToVAT = entity.SubjectToVAT,
                            TaxNumber = entity.TaxNumber,
                            CreditOpeningAccount = entity.CreditOpeningAccount,
                            DepitOpeningAccount = entity.DepitOpeningAccount,
                            CommercialRegister = entity.CommercialRegister,
                            CustomerAdminARName = entity.CustomerAdminARName,
                            CustomerAdminENName = entity.CustomerAdminENName,
                            CustomerAdminTelephone = entity.CustomerAdminTelephone,


                            Account = new AccountVM()
                            {
                                ACC_ID = Acc.ACC_ID,
                                ACC_AR_NAME = Acc.ACC_AR_NAME,
                                ACC_CHECK_DATE = Acc.ACC_CHECK_DATE,
                                ACC_CODE = Acc.ACC_CODE,
                                ACC_CREDIT = Acc.ACC_CREDIT,
                                ACC_CUSTOMER_CODE = Acc.ACC_CUSTOMER_CODE,
                                ACC_DATE = Acc.ACC_DATE,
                                ACC_DEBIT = Acc.ACC_DEBIT,
                                ACC_DEBIT_OR_CREDIT_OR_WITHOUT = Acc.ACC_DEBIT_OR_CREDIT_OR_WITHOUT,
                                ACC_EN_NAME = Acc.ACC_EN_NAME,
                                ACC_LEVEL = Acc.ACC_LEVEL,
                                ACC_MAX_DEBIT = Acc.ACC_MAX_DEBIT,
                                ACC_MAX_CREDIT = Acc.ACC_MAX_CREDIT,
                                ACC_NSONS = Acc.ACC_NSONS,
                                ACC_REMARKS = Acc.ACC_REMARKS,
                                ACC_STATE = Acc.ACC_STATE,
                                ACC_TURNNING_YES_OR_NO = Acc.ACC_TURNNING_YES_OR_NO,
                                ACC_TYPE_ID = Acc.ACC_TYPE_ID,
                                CURRENCY_ID = Acc.CURRENCY_ID,
                                Deactivate = Acc.Deactivate,
                                FINAL_ACC_ID = Acc.FINAL_ACC_ID,
                                AddedBy = Acc.AddedBy,
                                AddedOn = Acc.AddedOn,
                                UpdatedBy = Acc.UpdatedBy,
                                UpdatedOn = Acc.UpdatedOn,
                                PARENT_ACC_ID = Acc.PARENT_ACC_ID
                            },
                            currency = new CurrencyVM
                            {
                                CURRENCY_ID = currency.CURRENCY_ID,
                                CURRENCY_AR_NAME = currency.CURRENCY_AR_NAME,
                                CURRENCY_CODE = currency.CURRENCY_CODE,
                                CURRENCY_EN_NAME = currency.CURRENCY_EN_NAME,
                                CURRENCY_FIXED_RATE = currency.CURRENCY_FIXED_RATE,
                                CURRENCY_RATE = currency.CURRENCY_RATE,
                                CURRENCY_SUB_AR_NAME = currency.CURRENCY_SUB_AR_NAME,
                                CURRENCY_SUB_EN_NAME = currency.CURRENCY_SUB_EN_NAME,
                                CURRENCY_SYMBOL_AR_NAME = currency.CURRENCY_SYMBOL_AR_NAME,
                                CURRENCY_SYMBOL_EN_NAME = currency.CURRENCY_SYMBOL_EN_NAME,
                                SUB_TO_CURRENCY_TRANS = currency.SUB_TO_CURRENCY_TRANS,
                                AddedBy = currency.AddedBy,
                                AddedOn = currency.AddedOn,
                                Disable = currency.Disable,
                                UpdatedBy = currency.UpdatedBy,
                                updatedOn = currency.updatedOn
                            },
                        };
                return q.Where(x => (x.Account.ACC_TYPE_ID == 5 || x.Account.ACC_TYPE_ID == 7)).ToList();
            });
        }

        public Task<List<CustomersVM>> getChartsData()
        {
            return Task.Run<List<CustomersVM>>(() =>
            {
                var q = from entity in customerRepo.GetAll()
                        join Acc in accountRepo.GetAll() on entity.ACC_ID equals Acc.ACC_ID
                        join acType in accountTypeRepo.GetAll() on Acc.ACC_TYPE_ID equals acType.ACC_TYPE_ID
                        join Cur in currencyRepo.GetAll() on Acc.CURRENCY_ID equals Cur.CURRENCY_ID into g
                        from currency in g.DefaultIfEmpty()
                        select new CustomersVM
                        {
                            ACC_ID = entity.ACC_ID,
                            CUST_EMAIL = entity.CUST_EMAIL,
                            CUST_PHOTO = entity.CUST_PHOTO,
                            CUST_POST_BOX = entity.CUST_POST_BOX,
                            CUST_WEB_SITE = entity.CUST_WEB_SITE,
                            CUST_ZIP_CODE = entity.CUST_ZIP_CODE,
                            GOV_ID = entity.GOV_ID,
                            NATIONALITY_ID = entity.NATIONALITY_ID,
                            NATION_ID = entity.NATION_ID,
                            SELL_TYPE_ID = entity.SELL_TYPE_ID,
                            TOWN_ID = entity.TOWN_ID,
                            VILLAGE_ID = entity.VILLAGE_ID,
                            CUST_AR_NAME = entity.CUST_AR_NAME,
                            CUST_CODE = entity.CUST_CODE,
                            CUST_EN_NAME = entity.CUST_EN_NAME,
                            CUST_TITLE = entity.CUST_TITLE,
                            Comp_Bran_ID = entity.Comp_Bran_ID,
                            Dept_ID = entity.Dept_ID,
                            VATRate = entity.VATRate,
                            SubjectToVAT = entity.SubjectToVAT,
                            TaxNumber = entity.TaxNumber,
                            CommercialRegister = entity.CommercialRegister,
                            CreditOpeningAccount = entity.CreditOpeningAccount,
                            DepitOpeningAccount = entity.DepitOpeningAccount,
                            CustomerAdminARName = entity.CustomerAdminARName,
                            CustomerAdminENName = entity.CustomerAdminENName,
                            CustomerAdminTelephone = entity.CustomerAdminTelephone,


                            Account = new AccountVM()
                            {
                                ACC_ID = Acc.ACC_ID,
                                ACC_AR_NAME = Acc.ACC_AR_NAME,
                                ACC_CHECK_DATE = Acc.ACC_CHECK_DATE,
                                ACC_CODE = Acc.ACC_CODE,
                                ACC_CREDIT = Acc.ACC_CREDIT,
                                ACC_CUSTOMER_CODE = Acc.ACC_CUSTOMER_CODE,
                                ACC_DATE = Acc.ACC_DATE,
                                ACC_DEBIT = Acc.ACC_DEBIT,
                                ACC_DEBIT_OR_CREDIT_OR_WITHOUT = Acc.ACC_DEBIT_OR_CREDIT_OR_WITHOUT,
                                ACC_EN_NAME = Acc.ACC_EN_NAME,
                                ACC_LEVEL = Acc.ACC_LEVEL,
                                ACC_MAX_DEBIT = Acc.ACC_MAX_DEBIT,
                                ACC_MAX_CREDIT = Acc.ACC_MAX_CREDIT,
                                ACC_NSONS = Acc.ACC_NSONS,
                                ACC_REMARKS = Acc.ACC_REMARKS,
                                ACC_STATE = Acc.ACC_STATE,
                                ACC_TURNNING_YES_OR_NO = Acc.ACC_TURNNING_YES_OR_NO,
                                ACC_TYPE_ID = Acc.ACC_TYPE_ID,
                                CURRENCY_ID = Acc.CURRENCY_ID,
                                Deactivate = Acc.Deactivate,
                                FINAL_ACC_ID = Acc.FINAL_ACC_ID,
                                AddedBy = Acc.AddedBy,
                                AddedOn = Acc.AddedOn,
                                UpdatedBy = Acc.UpdatedBy,
                                UpdatedOn = Acc.UpdatedOn,
                                PARENT_ACC_ID = Acc.PARENT_ACC_ID
                            },
                            currency = new CurrencyVM
                            {
                                CURRENCY_ID = currency.CURRENCY_ID,
                                CURRENCY_AR_NAME = currency.CURRENCY_AR_NAME,
                                CURRENCY_CODE = currency.CURRENCY_CODE,
                                CURRENCY_EN_NAME = currency.CURRENCY_EN_NAME,
                                CURRENCY_FIXED_RATE = currency.CURRENCY_FIXED_RATE,
                                CURRENCY_RATE = currency.CURRENCY_RATE,
                                CURRENCY_SUB_AR_NAME = currency.CURRENCY_SUB_AR_NAME,
                                CURRENCY_SUB_EN_NAME = currency.CURRENCY_SUB_EN_NAME,
                                CURRENCY_SYMBOL_AR_NAME = currency.CURRENCY_SYMBOL_AR_NAME,
                                CURRENCY_SYMBOL_EN_NAME = currency.CURRENCY_SYMBOL_EN_NAME,
                                SUB_TO_CURRENCY_TRANS = currency.SUB_TO_CURRENCY_TRANS,
                                AddedBy = currency.AddedBy,
                                AddedOn = currency.AddedOn,
                                Disable = currency.Disable,
                                UpdatedBy = currency.UpdatedBy,
                                updatedOn = currency.updatedOn
                            },
                            AccountTypeARName = acType.ACC_TYPE_AR_NAME
                        };
     
                    return q.Where(x => (x.Account.ACC_TYPE_ID == 5 || x.Account.ACC_TYPE_ID == 6 || x.Account.ACC_TYPE_ID == 7)).ToList();
              
            });
        }
        public Task<List<CustomersVM>> GetSearchForCustomer(string Search)
        {
            return Task.Run<List<CustomersVM>>(() =>
            {
                var q = from entity in customerRepo.GetAll()
                        join Acc in accountRepo.GetAll() on entity.ACC_ID equals Acc.ACC_ID
                        join acType in accountTypeRepo.GetAll() on Acc.ACC_TYPE_ID equals acType.ACC_TYPE_ID
                        join Cur in currencyRepo.GetAll() on Acc.CURRENCY_ID equals Cur.CURRENCY_ID into g
                        from currency in g.DefaultIfEmpty()
                        select new CustomersVM
                        {
                            ACC_ID = entity.ACC_ID,
                            CUST_EMAIL = entity.CUST_EMAIL,
                            CUST_PHOTO = entity.CUST_PHOTO,
                            CUST_POST_BOX = entity.CUST_POST_BOX,
                            CUST_WEB_SITE = entity.CUST_WEB_SITE,
                            CUST_ZIP_CODE = entity.CUST_ZIP_CODE,
                            GOV_ID = entity.GOV_ID,
                            NATIONALITY_ID = entity.NATIONALITY_ID,
                            NATION_ID = entity.NATION_ID,
                            SELL_TYPE_ID = entity.SELL_TYPE_ID,
                            TOWN_ID = entity.TOWN_ID,
                            VILLAGE_ID = entity.VILLAGE_ID,
                            CUST_AR_NAME = entity.CUST_AR_NAME,
                            CUST_CODE = entity.CUST_CODE,
                            CUST_EN_NAME = entity.CUST_EN_NAME,
                            CUST_TITLE = entity.CUST_TITLE,
                            Comp_Bran_ID = entity.Comp_Bran_ID,
                            Dept_ID = entity.Dept_ID,
                            VATRate = entity.VATRate,
                            SubjectToVAT = entity.SubjectToVAT,
                            TaxNumber = entity.TaxNumber,
                            CommercialRegister = entity.CommercialRegister,
                            CreditOpeningAccount = entity.CreditOpeningAccount,
                            DepitOpeningAccount = entity.DepitOpeningAccount,
                            CustomerAdminARName = entity.CustomerAdminARName,
                            CustomerAdminENName = entity.CustomerAdminENName,
                            CustomerAdminTelephone = entity.CustomerAdminTelephone,


                            Account = new AccountVM()
                            {
                                ACC_ID = Acc.ACC_ID,
                                ACC_AR_NAME = Acc.ACC_AR_NAME,
                                ACC_CHECK_DATE = Acc.ACC_CHECK_DATE,
                                ACC_CODE = Acc.ACC_CODE,
                                ACC_CREDIT = Acc.ACC_CREDIT,
                                ACC_CUSTOMER_CODE = Acc.ACC_CUSTOMER_CODE,
                                ACC_DATE = Acc.ACC_DATE,
                                ACC_DEBIT = Acc.ACC_DEBIT,
                                ACC_DEBIT_OR_CREDIT_OR_WITHOUT = Acc.ACC_DEBIT_OR_CREDIT_OR_WITHOUT,
                                ACC_EN_NAME = Acc.ACC_EN_NAME,
                                ACC_LEVEL = Acc.ACC_LEVEL,
                                ACC_MAX_DEBIT = Acc.ACC_MAX_DEBIT,
                                ACC_MAX_CREDIT = Acc.ACC_MAX_CREDIT,
                                ACC_NSONS = Acc.ACC_NSONS,
                                ACC_REMARKS = Acc.ACC_REMARKS,
                                ACC_STATE = Acc.ACC_STATE,
                                ACC_TURNNING_YES_OR_NO = Acc.ACC_TURNNING_YES_OR_NO,
                                ACC_TYPE_ID = Acc.ACC_TYPE_ID,
                                CURRENCY_ID = Acc.CURRENCY_ID,
                                Deactivate = Acc.Deactivate,
                                FINAL_ACC_ID = Acc.FINAL_ACC_ID,
                                AddedBy = Acc.AddedBy,
                                AddedOn = Acc.AddedOn,
                                UpdatedBy = Acc.UpdatedBy,
                                UpdatedOn = Acc.UpdatedOn,
                                PARENT_ACC_ID = Acc.PARENT_ACC_ID
                            },
                            currency = new CurrencyVM
                            {
                                CURRENCY_ID = currency.CURRENCY_ID,
                                CURRENCY_AR_NAME = currency.CURRENCY_AR_NAME,
                                CURRENCY_CODE = currency.CURRENCY_CODE,
                                CURRENCY_EN_NAME = currency.CURRENCY_EN_NAME,
                                CURRENCY_FIXED_RATE = currency.CURRENCY_FIXED_RATE,
                                CURRENCY_RATE = currency.CURRENCY_RATE,
                                CURRENCY_SUB_AR_NAME = currency.CURRENCY_SUB_AR_NAME,
                                CURRENCY_SUB_EN_NAME = currency.CURRENCY_SUB_EN_NAME,
                                CURRENCY_SYMBOL_AR_NAME = currency.CURRENCY_SYMBOL_AR_NAME,
                                CURRENCY_SYMBOL_EN_NAME = currency.CURRENCY_SYMBOL_EN_NAME,
                                SUB_TO_CURRENCY_TRANS = currency.SUB_TO_CURRENCY_TRANS,
                                AddedBy = currency.AddedBy,
                                AddedOn = currency.AddedOn,
                                Disable = currency.Disable,
                                UpdatedBy = currency.UpdatedBy,
                                updatedOn = currency.updatedOn
                            },
                            AccountTypeARName = acType.ACC_TYPE_AR_NAME
                        };
                if (!string.IsNullOrEmpty(Search))
                {
                    return q.Where(x => (x.Account.ACC_TYPE_ID == 5 || x.Account.ACC_TYPE_ID == 6 || x.Account.ACC_TYPE_ID == 7) && (x.CUST_CODE.ToLower().Contains(Search.ToLower()) || x.CUST_AR_NAME.ToLower().Contains(Search.ToLower()) || x.CUST_EN_NAME.ToLower().Contains(Search.ToLower()))).ToList();
                }
                else
                {
                    return q.Where(x => (x.Account.ACC_TYPE_ID == 5 || x.Account.ACC_TYPE_ID == 6 || x.Account.ACC_TYPE_ID == 7)).ToList();
                }
            });
        }

        public bool Insert(CustomersVM entity)
        {
            CUSTOMERS cus = new CUSTOMERS
            {
                ACC_ID = entity.ACC_ID,
                AddedBy = entity.AddedBy,
                AddedOn = entity.AddedOn,
                ANYCOMMENTS = entity.ANYCOMMENTS,
                ANYCOMPLAINT = entity.ANYCOMPLAINT,
                CARMETERVALUE = entity.CARMETERVALUE,
                CARNUMBER = entity.CARNUMBER,
                CUST_ADDR_REMARKS = entity.CUST_ADDR_REMARKS,
                CUST_EMAIL = entity.CUST_EMAIL,
                CUST_PHOTO = entity.CUST_PHOTO,
                CUST_POST_BOX = entity.CUST_POST_BOX,
                CUST_WEB_SITE = entity.CUST_WEB_SITE,
                CUST_ZIP_CODE = entity.CUST_ZIP_CODE,
                Disable = entity.Disable,
                ENGINENUMBER = entity.ENGINENUMBER,
                GOV_ID = entity.GOV_ID,
                HEARABOUTUS = entity.HEARABOUTUS,
                ISTHISTHEFIRSTTIME = entity.ISTHISTHEFIRSTTIME,
                NATIONALITY_ID = entity.NATIONALITY_ID,
                NATION_ID = entity.NATION_ID,
                SELL_TYPE_ID = entity.SELL_TYPE_ID,
                SHASIHNUMBER = entity.SHASIHNUMBER,
                THINKABOUTSTORE = entity.THINKABOUTSTORE,
                TOWN_ID = entity.TOWN_ID,
                UpdatedBy = entity.UpdatedBy,
                updatedOn = entity.updatedOn,
                VILLAGE_ID = entity.VILLAGE_ID,
                CUST_AR_NAME = entity.CUST_AR_NAME,
                CUST_CODE = entity.CUST_CODE,
                CUST_EN_NAME = entity.CUST_EN_NAME,
                CUST_TITLE = entity.CUST_TITLE,
                Comp_Bran_ID = entity.Comp_Bran_ID,
                Dept_ID = entity.Dept_ID,
                VATRate = entity.VATRate,
                SubjectToVAT = entity.SubjectToVAT,
                TaxNumber = entity.TaxNumber,
                CommercialRegister = entity.CommercialRegister,
                CreditOpeningAccount = entity.CreditOpeningAccount,
                DepitOpeningAccount = entity.DepitOpeningAccount,
                CustomerAdminARName = entity.CustomerAdminARName,
                CustomerAdminENName = entity.CustomerAdminENName,
                CustomerAdminTelephone = entity.CustomerAdminTelephone
            };
            customerRepo.Add(cus);
            return true;
        }


        public Task<int> InsertAsync(CustomersVM entity)
        {
            return Task.Run<int>(() =>
            {
                CUSTOMERS cus = new CUSTOMERS
                {
                    ACC_ID = entity.ACC_ID,
                    AddedBy = entity.AddedBy,
                    AddedOn = entity.AddedOn,
                    ANYCOMMENTS = entity.ANYCOMMENTS,
                    ANYCOMPLAINT = entity.ANYCOMPLAINT,
                    CARMETERVALUE = entity.CARMETERVALUE,
                    CARNUMBER = entity.CARNUMBER,
                    CUST_ADDR_REMARKS = entity.CUST_ADDR_REMARKS,
                    CUST_EMAIL = entity.CUST_EMAIL,
                    CUST_PHOTO = entity.CUST_PHOTO,
                    CUST_POST_BOX = entity.CUST_POST_BOX,
                    CUST_WEB_SITE = entity.CUST_WEB_SITE,
                    CUST_ZIP_CODE = entity.CUST_ZIP_CODE,
                    Disable = entity.Disable,
                    ENGINENUMBER = entity.ENGINENUMBER,
                    GOV_ID = entity.GOV_ID,
                    HEARABOUTUS = entity.HEARABOUTUS,
                    ISTHISTHEFIRSTTIME = entity.ISTHISTHEFIRSTTIME,
                    NATIONALITY_ID = entity.NATIONALITY_ID,
                    NATION_ID = entity.NATION_ID,
                    SELL_TYPE_ID = entity.SELL_TYPE_ID,
                    SHASIHNUMBER = entity.SHASIHNUMBER,
                    THINKABOUTSTORE = entity.THINKABOUTSTORE,
                    TOWN_ID = entity.TOWN_ID,
                    UpdatedBy = entity.UpdatedBy,
                    updatedOn = entity.updatedOn,
                    VILLAGE_ID = entity.VILLAGE_ID,
                    CUST_AR_NAME = entity.CUST_AR_NAME,
                    CUST_CODE = entity.CUST_CODE,
                    CUST_EN_NAME = entity.CUST_EN_NAME,
                    CUST_TITLE = entity.CUST_TITLE,
                    Comp_Bran_ID = entity.Comp_Bran_ID,
                    Dept_ID = entity.Dept_ID,
                    VATRate = entity.VATRate,
                    SubjectToVAT = entity.SubjectToVAT,
                    TaxNumber = entity.TaxNumber,
                    CommercialRegister = entity.CommercialRegister,
                    CreditOpeningAccount = entity.CreditOpeningAccount,
                    DepitOpeningAccount = entity.DepitOpeningAccount,
                    CustomerAdminARName = entity.CustomerAdminARName,
                    CustomerAdminENName = entity.CustomerAdminENName,
                    CustomerAdminTelephone = entity.CustomerAdminTelephone
                };
                customerRepo.Add(cus);
                return cus.ACC_ID;
            });
        }

        public int? InsertUpdateCustomerDependence(CustomersVM customer, List<TelephoneVM> telephones, List<CustomerBranchesVM> customerBran, char transaction)
        {

            //try
            //{
            #region Accounts

            ACCOUNTS Acc = new ACCOUNTS
            {
                ACC_ID = customer.Account.ACC_ID,
                AddedBy = customer.Account.AddedBy,
                AddedOn = customer.Account.AddedOn,
                ACC_AR_NAME = customer.Account.ACC_AR_NAME,
                ACC_CHECK_DATE = DateTime.Now,
                ACC_CODE = customer.Account.ACC_CODE,
                ACC_CREDIT = 0,
                ACC_CUSTOMER_CODE = customer.Account.ACC_CUSTOMER_CODE,
                ACC_DATE = customer.Account.ACC_DATE == DateTime.MinValue ? DateTime.Now : customer.Account.ACC_DATE,
                ACC_DEBIT = 0,
                ACC_DEBIT_OR_CREDIT_OR_WITHOUT = customer.Account.ACC_DEBIT_OR_CREDIT_OR_WITHOUT,
                ACC_EN_NAME = customer.Account.ACC_EN_NAME,
                ACC_LEVEL = customer.Account.ACC_LEVEL,
                ACC_MAX_DEBIT = customer.Account.ACC_MAX_DEBIT,
                ACC_MAX_CREDIT = customer.Account.ACC_MAX_CREDIT,
                ACC_NSONS = 0,
                ACC_REMARKS = customer.Account.ACC_REMARKS,
                ACC_STATE = customer.Account.ACC_STATE,
                ACC_TURNNING_YES_OR_NO = "0",
                ACC_TYPE_ID = customer.Account.ACC_TYPE_ID,
                CURRENCY_ID = customer.Account.CURRENCY_ID,
                Deactivate = customer.Account.Deactivate,
                FINAL_ACC_ID = customer.Account.FINAL_ACC_ID,
                PARENT_ACC_ID = customer.Account.PARENT_ACC_ID,
                UpdatedOn = customer.Account.UpdatedOn
            };
            int? accID = 0;
            if (transaction == 'I')
            {
                accID = accountRepo.Add(Acc).ACC_ID;
            }
            else if (transaction == 'U')
            {
                accID = accountRepo.Update(Acc, Acc.ACC_ID).ACC_ID;
            }


            if (Acc.PARENT_ACC_ID != null)
            {
                int? level = UpdateParent(Acc.PARENT_ACC_ID, 'a');
                if (level != null)
                {
                    Acc.ACC_LEVEL = level + 1;
                }
                else
                {
                    Acc.ACC_LEVEL = level;
                }
                accountRepo.Update(Acc, Acc.ACC_ID);
            }




            #endregion
            if (accID != null || accID != 0)
            {
                #region Customer
                CUSTOMERS CUS = new CUSTOMERS
                {
                    ACC_ID = Convert.ToInt32(accID),
                    AddedBy = customer.AddedBy,
                    AddedOn = customer.AddedOn,
                    ANYCOMMENTS = customer.ANYCOMMENTS,
                    ANYCOMPLAINT = customer.ANYCOMPLAINT,
                    CARMETERVALUE = customer.CARMETERVALUE,
                    CARNUMBER = customer.CARNUMBER,
                    CUST_ADDR_REMARKS = customer.CUST_ADDR_REMARKS,
                    CUST_EMAIL = customer.CUST_EMAIL,
                    CUST_PHOTO = customer.CUST_PHOTO,
                    CUST_POST_BOX = customer.CUST_POST_BOX,
                    CUST_WEB_SITE = customer.CUST_WEB_SITE,
                    CUST_ZIP_CODE = customer.CUST_ZIP_CODE,
                    Disable = customer.Disable,
                    ENGINENUMBER = customer.ENGINENUMBER,
                    GOV_ID = customer.GOV_ID,
                    HEARABOUTUS = customer.HEARABOUTUS,
                    ISTHISTHEFIRSTTIME = customer.ISTHISTHEFIRSTTIME,
                    NATIONALITY_ID = customer.NATION_ID,
                    NATION_ID = customer.NATION_ID,
                    SELL_TYPE_ID = customer.SELL_TYPE_ID,
                    SHASIHNUMBER = customer.SHASIHNUMBER,
                    THINKABOUTSTORE = customer.THINKABOUTSTORE,
                    TOWN_ID = customer.TOWN_ID,
                    UpdatedBy = customer.UpdatedBy,
                    updatedOn = customer.updatedOn,
                    VILLAGE_ID = customer.VILLAGE_ID,
                    CUST_AR_NAME = customer.CUST_AR_NAME,
                    CUST_CODE = customer.CUST_CODE,
                    CUST_EN_NAME = customer.CUST_EN_NAME,
                    CUST_TITLE = customer.CUST_TITLE,
                    Comp_Bran_ID = customer.Comp_Bran_ID,
                    Dept_ID = customer.Dept_ID,
                    VATRate = customer.VATRate,
                    SubjectToVAT = customer.SubjectToVAT,
                    TaxNumber = customer.TaxNumber,
                    CommercialRegister = customer.CommercialRegister,
                    CreditOpeningAccount = customer.CreditOpeningAccount,
                    DepitOpeningAccount = customer.DepitOpeningAccount,
                    CustomerAdminARName = customer.CustomerAdminARName,
                    CustomerAdminENName = customer.CustomerAdminENName,
                    CustomerAdminTelephone = customer.CustomerAdminTelephone
                };
                int? accid = 0;
                if (transaction == 'I')
                {
                    accid = customerRepo.Add(CUS).ACC_ID;
                }
                else if (transaction == 'U')
                {
                    accid = customerRepo.Update(CUS, CUS.ACC_ID).ACC_ID;
                }
                #endregion
                if (accid != null || accid != 0)
                {
                    #region Telephones
                    if (telephones != null)
                    {
                        if (transaction == 'U')
                        {
                            List<Telephones> telephons = telephoneRepo.GetAll().Where(x => x.TELE_ID == accID && x.TELE_TYPE_ID == 1).ToList();
                            foreach (var te in telephons)
                            {
                                Telephones tel = new Telephones()
                                {
                                    TELE_ID = te.TELE_ID,
                                    TELE_CAT_ID = te.TELE_CAT_ID,
                                    TELE_NUMBER = te.TELE_NUMBER,
                                    TELE_TYPE_ID = te.TELE_TYPE_ID,
                                    AddedBy = te.AddedBy,
                                    AddedOn = te.AddedOn,
                                    UpdatedBy = te.UpdatedBy,
                                    updatedOn = te.updatedOn
                                };
                                object[] keys = new object[4] { te.TELE_ID, te.TELE_TYPE_ID, te.TELE_NUMBER, te.TELE_CAT_ID };
                                telephoneRepo.DeleteComposite(tel, keys);
                            }
                        }
                        foreach (var tele in telephones)
                        {
                            Telephones telep = new Telephones()
                            {
                                TELE_ID = Convert.ToInt32(accid),
                                TELE_CAT_ID = tele.TELE_CAT_ID,
                                TELE_NUMBER = tele.TELE_NUMBER,
                                TELE_TYPE_ID = tele.TELE_TYPE_ID,
                                AddedBy = tele.AddedBy,
                                AddedOn = tele.AddedOn,
                                UpdatedBy = tele.UpdatedBy,
                                updatedOn = tele.updatedOn
                            };
                            telephoneRepo.Add(telep);
                        }
                    }
                    #endregion
                    #region Customer Branches
                    if (customerBran != null)
                    {
                        if (transaction == 'U')
                        {
                            List<CustomerBranches> custBran = customerBranchesRepo.GetAll().Where(x => x.ACC_ID == accID).ToList();
                            foreach (var cuBr in custBran)
                            {
                                CustomerBranches customBran = new CustomerBranches()
                                {
                                    ACC_BRN_AR_NAME = cuBr.ACC_BRN_AR_NAME,
                                    ACC_BRN_EN_NAME = cuBr.ACC_BRN_EN_NAME,
                                    BRN_ADDR_REMARKS = cuBr.BRN_ADDR_REMARKS,
                                    BRN_REMARKS = cuBr.BRN_REMARKS,
                                    CUST_BRN_ID = cuBr.CUST_BRN_ID,
                                    ACC_ID = cuBr.ACC_ID,
                                    GOV_ID = cuBr.GOV_ID,
                                    NATION_ID = cuBr.NATION_ID,
                                    TOWN_ID = cuBr.TOWN_ID,
                                    VILLAGE_ID = cuBr.VILLAGE_ID,
                                    AddedBy = cuBr.AddedBy,
                                    AddedOn = cuBr.AddedOn,
                                    UpdatedBy = cuBr.UpdatedBy,
                                    updatedOn = cuBr.updatedOn
                                };
                                customerBranchesRepo.Delete(customBran, cuBr.CUST_BRN_ID);
                            }
                        }
                        foreach (var cuBr in customerBran)
                        {
                            CustomerBranches customBran = new CustomerBranches()
                            {
                                ACC_BRN_AR_NAME = cuBr.ACC_BRN_AR_NAME,
                                ACC_BRN_EN_NAME = cuBr.ACC_BRN_EN_NAME,
                                BRN_ADDR_REMARKS = cuBr.BRN_ADDR_REMARKS,
                                BRN_REMARKS = cuBr.BRN_REMARKS,
                                CUST_BRN_ID = cuBr.CUST_BRN_ID,
                                ACC_ID = Convert.ToInt32(accid),
                                GOV_ID = cuBr.GOV_ID,
                                NATION_ID = cuBr.NATION_ID,
                                TOWN_ID = cuBr.TOWN_ID,
                                VILLAGE_ID = cuBr.VILLAGE_ID,
                                AddedBy = cuBr.AddedBy,
                                AddedOn = cuBr.AddedOn,
                                UpdatedBy = cuBr.UpdatedBy,
                                updatedOn = cuBr.updatedOn
                            };
                            customerBranchesRepo.Add(customBran);
                        }
                    }
                    #endregion
                }
            }
            return accID;
            //  }
            // catch (Exception ex) { string str = ex.Message; return false; }

        }

        public bool Update(CustomersVM entity)
        {
            CUSTOMERS cus = new CUSTOMERS
            {
                ACC_ID = entity.ACC_ID,
                AddedBy = entity.AddedBy,
                AddedOn = entity.AddedOn,
                ANYCOMMENTS = entity.ANYCOMMENTS,
                ANYCOMPLAINT = entity.ANYCOMPLAINT,
                CARMETERVALUE = entity.CARMETERVALUE,
                CARNUMBER = entity.CARNUMBER,
                CUST_ADDR_REMARKS = entity.CUST_ADDR_REMARKS,
                CUST_EMAIL = entity.CUST_EMAIL,
                CUST_PHOTO = entity.CUST_PHOTO,
                CUST_POST_BOX = entity.CUST_POST_BOX,
                CUST_WEB_SITE = entity.CUST_WEB_SITE,
                CUST_ZIP_CODE = entity.CUST_ZIP_CODE,
                Disable = entity.Disable,
                ENGINENUMBER = entity.ENGINENUMBER,
                GOV_ID = entity.GOV_ID,
                HEARABOUTUS = entity.HEARABOUTUS,
                ISTHISTHEFIRSTTIME = entity.ISTHISTHEFIRSTTIME,
                NATIONALITY_ID = entity.NATIONALITY_ID,
                NATION_ID = entity.NATION_ID,
                SELL_TYPE_ID = entity.SELL_TYPE_ID,
                SHASIHNUMBER = entity.SHASIHNUMBER,
                THINKABOUTSTORE = entity.THINKABOUTSTORE,
                TOWN_ID = entity.TOWN_ID,
                UpdatedBy = entity.UpdatedBy,
                updatedOn = entity.updatedOn,
                VILLAGE_ID = entity.VILLAGE_ID,
                CUST_AR_NAME = entity.CUST_AR_NAME,
                CUST_CODE = entity.CUST_CODE,
                CUST_EN_NAME = entity.CUST_EN_NAME,
                CUST_TITLE = entity.CUST_TITLE,
                Comp_Bran_ID = entity.Comp_Bran_ID,
                Dept_ID = entity.Dept_ID,
                VATRate = entity.VATRate,
                SubjectToVAT = entity.SubjectToVAT,
                TaxNumber = entity.TaxNumber,
                CommercialRegister = entity.CommercialRegister,
                CreditOpeningAccount = entity.CreditOpeningAccount,
                DepitOpeningAccount = entity.DepitOpeningAccount,
                CustomerAdminARName = entity.CustomerAdminARName,
                CustomerAdminENName = entity.CustomerAdminENName,
                CustomerAdminTelephone = entity.CustomerAdminTelephone
            };
            customerRepo.Update(cus, cus.ACC_ID);
            return true;
        }

        public Task<bool> UpdateAsync(CustomersVM entity)
        {
            return Task.Run<bool>(() =>
            {
                CUSTOMERS cus = new CUSTOMERS
                {
                    ACC_ID = entity.ACC_ID,
                    AddedBy = entity.AddedBy,
                    AddedOn = entity.AddedOn,
                    ANYCOMMENTS = entity.ANYCOMMENTS,
                    ANYCOMPLAINT = entity.ANYCOMPLAINT,
                    CARMETERVALUE = entity.CARMETERVALUE,
                    CARNUMBER = entity.CARNUMBER,
                    CUST_ADDR_REMARKS = entity.CUST_ADDR_REMARKS,
                    CUST_EMAIL = entity.CUST_EMAIL,
                    CUST_PHOTO = entity.CUST_PHOTO,
                    CUST_POST_BOX = entity.CUST_POST_BOX,
                    CUST_WEB_SITE = entity.CUST_WEB_SITE,
                    CUST_ZIP_CODE = entity.CUST_ZIP_CODE,
                    Disable = entity.Disable,
                    ENGINENUMBER = entity.ENGINENUMBER,
                    GOV_ID = entity.GOV_ID,
                    HEARABOUTUS = entity.HEARABOUTUS,
                    ISTHISTHEFIRSTTIME = entity.ISTHISTHEFIRSTTIME,
                    NATIONALITY_ID = entity.NATIONALITY_ID,
                    NATION_ID = entity.NATION_ID,
                    SELL_TYPE_ID = entity.SELL_TYPE_ID,
                    SHASIHNUMBER = entity.SHASIHNUMBER,
                    THINKABOUTSTORE = entity.THINKABOUTSTORE,
                    TOWN_ID = entity.TOWN_ID,
                    UpdatedBy = entity.UpdatedBy,
                    updatedOn = entity.updatedOn,
                    VILLAGE_ID = entity.VILLAGE_ID,
                    CUST_AR_NAME = entity.CUST_AR_NAME,
                    CUST_CODE = entity.CUST_CODE,
                    CUST_EN_NAME = entity.CUST_EN_NAME,
                    CUST_TITLE = entity.CUST_TITLE,
                    Comp_Bran_ID = entity.Comp_Bran_ID,
                    Dept_ID = entity.Dept_ID,
                    VATRate = entity.VATRate,
                    SubjectToVAT = entity.SubjectToVAT,
                    TaxNumber = entity.TaxNumber,
                    CommercialRegister = entity.CommercialRegister,
                    CreditOpeningAccount = entity.CreditOpeningAccount,
                    DepitOpeningAccount = entity.DepitOpeningAccount,
                    CustomerAdminARName = entity.CustomerAdminARName,
                    CustomerAdminENName = entity.CustomerAdminENName,
                    CustomerAdminTelephone = entity.CustomerAdminTelephone
                };
                customerRepo.Update(cus, cus.ACC_ID);
                return true;
            });
        }



        public ACCOUNTS GetParentData(int? ParentID)
        {
            var parent = from a in accountRepo.GetAll().Where(a => a.ACC_ID == ParentID)
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
                             VATRate = a.VATRate

                         };
            return parent.FirstOrDefault();
        }

        public int? UpdateParent(int? ParentID, char Operation)
        {
            
            ACCOUNTS parent = GetParentData(ParentID);
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
                VATRate = parent.VATRate
            };

            accountRepo.Update(acc, acc.ACC_ID);
            return parent.ACC_LEVEL;
        }


        public string GetLastCode()
        {
            var lastCode = customerRepo.GetAll().OrderByDescending(c => c.ACC_ID).FirstOrDefault();
            return lastCode.CUST_CODE;
        }
        //delete entry//////////////////////////////////////////////////////////////////////////////
        public List<ENTRY_DETAILS> GetAllDetails(long EntryMasterID)
        {
            List<ENTRY_DETAILS> entryDetails = new List<ENTRY_DETAILS>();
            entryDetails = entryDetailsRepo.GetAll().Where(x => x.ENTRY_ID == EntryMasterID).ToList();

            return entryDetails;
        }

        public void deleteEntryOfCustomer(ENTRY_MASTER entity)
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

        public List<CUSTOMERS> getByNationID(int nationId)
        {
            var q = customerRepo.GetAll().Where(a => a.NATION_ID == nationId).ToList();
            return q;
        }
        public List<CUSTOMERS> getByGOVID(int govId)
        {
            var q = customerRepo.GetAll().Where(a => a.GOV_ID == govId).ToList();
            return q;
        }
        public List<CUSTOMERS> getByTownID(int townId)
        {
            var q = customerRepo.GetAll().Where(a => a.TOWN_ID == townId).ToList();
            return q;
        }
        public List<CUSTOMERS> getByVilID(long villageId)
        {
            var q = customerRepo.GetAll().Where(a => a.VILLAGE_ID == villageId).ToList();
            return q;
        }
        public List<CUSTOMERS> getByCompBranID(int compBran)
        {
            var q = customerRepo.GetAll().Where(a => a.Comp_Bran_ID == compBran).ToList();
            return q;
        }
        public List<CUSTOMERS> getByDept(int deptId)
        {
            var q = customerRepo.GetAll().Where(a => a.Dept_ID == deptId).ToList();
            return q;
        }
        
        public Task<List<CustomersVM>> getSearchForCustomer(string search)
        {
            return Task.Run<List<CustomersVM>>(() =>
            {
                if (!string.IsNullOrEmpty(search))
                {
                    return GetAll().Where(p => (p.CUST_CODE.ToLower()).Contains(search.ToLower()) ||
                    (p.CUST_AR_NAME.ToLower()).Contains(search.ToLower()) || (p.CUST_EN_NAME.ToLower()).Contains(search.ToLower())).ToList();
                }
                else
                {
                    return GetAll();
                }
            });
        }

        public CustomersVM GetById(int id)
        {
            var Customer = from entity in customerRepo.GetAll().Where(a => a.ACC_ID == id)
                           select new CustomersVM
                           {
                               ACC_ID = entity.ACC_ID,
                               AddedBy = entity.AddedBy,
                               AddedOn = entity.AddedOn,
                               ANYCOMMENTS = entity.ANYCOMMENTS,
                               ANYCOMPLAINT = entity.ANYCOMPLAINT,
                               CARMETERVALUE = entity.CARMETERVALUE,
                               CARNUMBER = entity.CARNUMBER,
                               CUST_ADDR_REMARKS = entity.CUST_ADDR_REMARKS,
                               CUST_EMAIL = entity.CUST_EMAIL,
                               CUST_PHOTO = entity.CUST_PHOTO,
                               CUST_POST_BOX = entity.CUST_POST_BOX,
                               CUST_WEB_SITE = entity.CUST_WEB_SITE,
                               CUST_ZIP_CODE = entity.CUST_ZIP_CODE,
                               Disable = entity.Disable,
                               ENGINENUMBER = entity.ENGINENUMBER,
                               GOV_ID = entity.GOV_ID,
                               HEARABOUTUS = entity.HEARABOUTUS,
                               ISTHISTHEFIRSTTIME = entity.ISTHISTHEFIRSTTIME,
                               NATIONALITY_ID = entity.NATIONALITY_ID,
                               NATION_ID = entity.NATION_ID,
                               SELL_TYPE_ID = entity.SELL_TYPE_ID,
                               SHASIHNUMBER = entity.SHASIHNUMBER,
                               THINKABOUTSTORE = entity.THINKABOUTSTORE,
                               TOWN_ID = entity.TOWN_ID,
                               UpdatedBy = entity.UpdatedBy,
                               updatedOn = entity.updatedOn,
                               VILLAGE_ID = entity.VILLAGE_ID,
                               CUST_AR_NAME = entity.CUST_AR_NAME,
                               CUST_CODE = entity.CUST_CODE,
                               CUST_EN_NAME = entity.CUST_EN_NAME,
                               CUST_TITLE = entity.CUST_TITLE,
                               Comp_Bran_ID = entity.Comp_Bran_ID,
                               Dept_ID = entity.Dept_ID,
                               VATRate = entity.VATRate,
                               SubjectToVAT = entity.SubjectToVAT,
                               TaxNumber = entity.TaxNumber,
                               CommercialRegister = entity.CommercialRegister,
                               CreditOpeningAccount = entity.CreditOpeningAccount,
                               DepitOpeningAccount = entity.DepitOpeningAccount,

                               CustomerAdminARName = entity.CustomerAdminARName,
                               CustomerAdminENName = entity.CustomerAdminENName,
                               CustomerAdminTelephone = entity.CustomerAdminTelephone
                           };
            return Customer.FirstOrDefault();
        }
    }
}
