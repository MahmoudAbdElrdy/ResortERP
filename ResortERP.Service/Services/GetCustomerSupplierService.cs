using ResortERP.Core;
using ResortERP.Core.VM;
using ResortERP.Repository;
using ResortERP.Service.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortERP.Service.Services
{
    public class GetCustomerSupplierService : IGetCustomerSupplierService, IDisposable
    {
        IGenericRepository<GetCustomerSupplierData> customerSupplierRepo;

        public GetCustomerSupplierService(IGenericRepository<GetCustomerSupplierData> customerSupplierRepo)
        {
            this.customerSupplierRepo = customerSupplierRepo;
        }

        public Task<int> CountAsync()
        {
            return Task.Run<int>(() =>
            {
                return customerSupplierRepo.CountAsync();
            });
        }

        public void Dispose()
        {
            customerSupplierRepo.Dispose();
        }

        public List<CustomersVM> GetAll(char type)
        {
            try
            {

            }
            catch (Exception ex)
            {

                throw;
            }
            int cu = type == 'c' ? 5 : 6;
            var q = from entity in customerSupplierRepo.GetAll()
                    select new CustomersVM
                    {
                        ACC_ID = entity.ACC_ID,
                        AddedBy = entity.AddedBy,
                        AddedOn = entity.AddedOn,
                        ANYCOMMENTS = entity.ANYCOMMENTS,
                        ANYCOMPLAINT = entity.ANYCOMPLAINT,
                        CARMETERVALUE = entity.CARMETERVALUE,
                        CARNUMBER = entity.CARNUMBER,
                        Comp_Bran_ID = entity.Comp_Bran_ID,
                        CUST_ADDR_REMARKS = entity.CUST_ADDR_REMARKS,
                        CUST_AR_NAME = entity.CUST_AR_NAME,
                        CUST_CODE = entity.CUST_CODE,
                        CUST_EMAIL = entity.CUST_EMAIL,
                        CUST_EN_NAME = entity.CUST_EN_NAME,
                        CUST_PHOTO = entity.CUST_PHOTO,
                        CUST_POST_BOX = entity.CUST_POST_BOX,
                        CUST_TITLE = entity.CUST_TITLE,
                        CUST_WEB_SITE = entity.CUST_WEB_SITE,
                        CUST_ZIP_CODE = entity.CUST_ZIP_CODE,
                        Dept_ID = entity.Dept_ID,
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
                        VATRate = entity.VATRate,
                        SubjectToVAT = entity.SubjectToVAT,
                        TaxNumber = entity.TaxNumber,
                        CommercialRegister = entity.CommercialRegister,
                        CreditOpeningAccount = entity.CreditOpeningAccount,
                        DepitOpeningAccount = entity.DepitOpeningAccount,
                        CustomerAdminARName = entity.CustomerAdminARName,
                        CustomerAdminENName = entity.CustomerAdminENName,
                        CustomerAdminTelephone = entity.CustomerAdminTelephone,

                        Account = new AccountVM
                        {
                            ACC_ID = entity.ACC_ID,
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
                            AddedBy = entity.AccAddedBy,
                            AddedOn = entity.AccAddedOn,
                            UpdatedBy = entity.AccUpdatedBy,
                            UpdatedOn = entity.AccUpdatedOn
                        },
                        currency = new CurrencyVM
                        {
                            CURRENCY_ID = entity.CURRENCY_ID,
                            CURRENCY_AR_NAME = entity.CURRENCY_AR_NAME,
                            CURRENCY_CODE = entity.CURRENCY_CODE,
                            CURRENCY_EN_NAME = entity.CURRENCY_EN_NAME,
                            CURRENCY_FIXED_RATE = entity.CURRENCY_FIXED_RATE,
                            CURRENCY_RATE = entity.CURRENCY_RATE,
                            CURRENCY_SUB_AR_NAME = entity.CURRENCY_SUB_AR_NAME,
                            CURRENCY_SUB_EN_NAME = entity.CURRENCY_SUB_EN_NAME,
                            CURRENCY_SYMBOL_AR_NAME = entity.CURRENCY_SYMBOL_AR_NAME,
                            CURRENCY_SYMBOL_EN_NAME = entity.CURRENCY_SYMBOL_EN_NAME,
                            SUB_TO_CURRENCY_TRANS = entity.SUB_TO_CURRENCY_TRANS,
                            AddedBy = entity.CurAddedBy,
                            AddedOn = entity.CurAddedOn,
                            Disable = entity.CurDisable,
                            UpdatedBy = entity.CurUpdatedBy,
                            updatedOn = entity.CurupdatedOn
                        }
                    };
            var lst = q.Where(x => (x.Account.ACC_TYPE_ID == cu || x.Account.ACC_TYPE_ID == 7)).ToList();
            return lst;
        }

        public Task<List<CustomersVM>> GetAllAsync(int pageNum, int pageSize, char type)
        {
            return Task.Run<List<CustomersVM>>(() =>
            {

                //int cu = type == 'c' ? 5 : 6;
                // cu = type == 'w' ? 21 : 6;
                int type1 = 1, type2 = 1;
                if (type == 'c')
                {
                    type1 = 5;
                    type2 = 7;
                }
                else
                    if (type == 's')
                {
                    type1 = 6;
                    type2 = 7;
                }
                else if (type == 'w')
                {
                    type1 = 21;
                }



                int rowCount = 0;
                var q = from entity in customerSupplierRepo.GetPaged(pageNum, pageSize, p => p.ACC_ID, false, out rowCount)
                        select new CustomersVM()
                        {
                            ACC_ID = entity.ACC_ID,
                            AddedBy = entity.AddedBy,
                            AddedOn = entity.AddedOn,
                            ANYCOMMENTS = entity.ANYCOMMENTS,
                            ANYCOMPLAINT = entity.ANYCOMPLAINT,
                            CARMETERVALUE = entity.CARMETERVALUE,
                            CARNUMBER = entity.CARNUMBER,
                            Comp_Bran_ID = entity.Comp_Bran_ID,
                            CUST_ADDR_REMARKS = entity.CUST_ADDR_REMARKS,
                            CUST_AR_NAME = entity.CUST_AR_NAME,
                            CUST_CODE = entity.CUST_CODE,
                            CUST_EMAIL = entity.CUST_EMAIL,
                            CUST_EN_NAME = entity.CUST_EN_NAME,
                            CUST_PHOTO = entity.CUST_PHOTO,
                            CUST_POST_BOX = entity.CUST_POST_BOX,
                            CUST_TITLE = entity.CUST_TITLE,
                            CUST_WEB_SITE = entity.CUST_WEB_SITE,
                            CUST_ZIP_CODE = entity.CUST_ZIP_CODE,
                            Dept_ID = entity.Dept_ID,
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
                                ACC_ID = entity.ACC_ID,
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
                                AddedBy = entity.AccAddedBy,
                                AddedOn = entity.AccAddedOn,
                                UpdatedBy = entity.AccUpdatedBy,
                                UpdatedOn = entity.AccUpdatedOn
                            },
                            currency = new CurrencyVM()
                            {
                                CURRENCY_ID = entity.CURRENCY_ID,
                                CURRENCY_AR_NAME = entity.CURRENCY_AR_NAME,
                                CURRENCY_CODE = entity.CURRENCY_CODE,
                                CURRENCY_EN_NAME = entity.CURRENCY_EN_NAME,
                                CURRENCY_FIXED_RATE = entity.CURRENCY_FIXED_RATE,
                                CURRENCY_RATE = entity.CURRENCY_RATE,
                                CURRENCY_SUB_AR_NAME = entity.CURRENCY_SUB_AR_NAME,
                                CURRENCY_SUB_EN_NAME = entity.CURRENCY_SUB_EN_NAME,
                                CURRENCY_SYMBOL_AR_NAME = entity.CURRENCY_SYMBOL_AR_NAME,
                                CURRENCY_SYMBOL_EN_NAME = entity.CURRENCY_SYMBOL_EN_NAME,
                                SUB_TO_CURRENCY_TRANS = entity.SUB_TO_CURRENCY_TRANS,
                                AddedBy = entity.CurAddedBy,
                                AddedOn = entity.CurAddedOn,
                                Disable = entity.CurDisable,
                                UpdatedBy = entity.CurUpdatedBy,
                                updatedOn = entity.CurupdatedOn
                            }
                        };

                try
                {
                    //var lst = q.Where(x => (x.Account.ACC_TYPE_ID == cu || x.Account.ACC_TYPE_ID == 7)).ToList();
                    var lst = q.Where(x => (x.Account.ACC_TYPE_ID == type1 || x.Account.ACC_TYPE_ID == type2)).ToList();
                    return lst;
                }
                catch (Exception ex)
                {
                    var lst = q.Where(x => (x.Account.ACC_TYPE_ID == type1 || x.Account.ACC_TYPE_ID == type2)).ToList();
                    return lst;
                    //var lst = q.Where(x => (x.Account.ACC_TYPE_ID == cu || x.Account.ACC_TYPE_ID == 7)).ToList();
                    //return lst;
                }


            });
        }

        public Task<CustomersVM> getByIdLog(int ACC_ID, char type)
        {
            return Task.Run<CustomersVM>(() =>
            {

                //int cu = type == 'c' ? 5 : 6;
                // cu = type == 'w' ? 21 : 6;
                int type1 = 1, type2 = 1;
                if (type == 'c')
                {
                    type1 = 5;
                    type2 = 7;
                }
                else
                    if (type == 's')
                {
                    type1 = 6;
                    type2 = 7;
                }
                else if (type == 'w')
                {
                    type1 = 21;
                }


                var q = from entity in customerSupplierRepo.GetAll().Where(a => a.ACC_ID == ACC_ID)
                        select new CustomersVM()
                        {
                            ACC_ID = entity.ACC_ID,
                            AddedBy = entity.AddedBy,
                            AddedOn = entity.AddedOn,
                            ANYCOMMENTS = entity.ANYCOMMENTS,
                            ANYCOMPLAINT = entity.ANYCOMPLAINT,
                            CARMETERVALUE = entity.CARMETERVALUE,
                            CARNUMBER = entity.CARNUMBER,
                            Comp_Bran_ID = entity.Comp_Bran_ID,
                            CUST_ADDR_REMARKS = entity.CUST_ADDR_REMARKS,
                            CUST_AR_NAME = entity.CUST_AR_NAME,
                            CUST_CODE = entity.CUST_CODE,
                            CUST_EMAIL = entity.CUST_EMAIL,
                            CUST_EN_NAME = entity.CUST_EN_NAME,
                            CUST_PHOTO = entity.CUST_PHOTO,
                            CUST_POST_BOX = entity.CUST_POST_BOX,
                            CUST_TITLE = entity.CUST_TITLE,
                            CUST_WEB_SITE = entity.CUST_WEB_SITE,
                            CUST_ZIP_CODE = entity.CUST_ZIP_CODE,
                            Dept_ID = entity.Dept_ID,
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
                                ACC_ID = entity.ACC_ID,
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
                                AddedBy = entity.AccAddedBy,
                                AddedOn = entity.AccAddedOn,
                                UpdatedBy = entity.AccUpdatedBy,
                                UpdatedOn = entity.AccUpdatedOn
                            },
                            currency = new CurrencyVM()
                            {
                                CURRENCY_ID = entity.CURRENCY_ID,
                                CURRENCY_AR_NAME = entity.CURRENCY_AR_NAME,
                                CURRENCY_CODE = entity.CURRENCY_CODE,
                                CURRENCY_EN_NAME = entity.CURRENCY_EN_NAME,
                                CURRENCY_FIXED_RATE = entity.CURRENCY_FIXED_RATE,
                                CURRENCY_RATE = entity.CURRENCY_RATE,
                                CURRENCY_SUB_AR_NAME = entity.CURRENCY_SUB_AR_NAME,
                                CURRENCY_SUB_EN_NAME = entity.CURRENCY_SUB_EN_NAME,
                                CURRENCY_SYMBOL_AR_NAME = entity.CURRENCY_SYMBOL_AR_NAME,
                                CURRENCY_SYMBOL_EN_NAME = entity.CURRENCY_SYMBOL_EN_NAME,
                                SUB_TO_CURRENCY_TRANS = entity.SUB_TO_CURRENCY_TRANS,
                                AddedBy = entity.CurAddedBy,
                                AddedOn = entity.CurAddedOn,
                                Disable = entity.CurDisable,
                                UpdatedBy = entity.CurUpdatedBy,
                                updatedOn = entity.CurupdatedOn
                            }
                        };

                try
                {
                    //var lst = q.Where(x => (x.Account.ACC_TYPE_ID == cu || x.Account.ACC_TYPE_ID == 7)).ToList();
                    var lst = q.Where(x => (x.Account.ACC_TYPE_ID == type1 || x.Account.ACC_TYPE_ID == type2));
                    return lst.FirstOrDefault();
                }
                catch (Exception ex)
                {
                    var lst = q.Where(x => (x.Account.ACC_TYPE_ID == type1 || x.Account.ACC_TYPE_ID == type2));
                    return lst.FirstOrDefault();
                    //var lst = q.Where(x => (x.Account.ACC_TYPE_ID == cu || x.Account.ACC_TYPE_ID == 7)).ToList();
                    //return lst;
                }


            });
        }
    }
}
