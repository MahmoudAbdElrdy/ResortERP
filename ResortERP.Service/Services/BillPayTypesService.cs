using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ResortERP.Repository;
using ResortERP.Core;
using ResortERP.Service.IServices;
using ResortERP.Core.VM;

namespace ResortERP.Service.Services
{
    public class BillPayTypesService : IBillPayTypesService, IDisposable
    {
        IGenericRepository<BILL_PAY_TYPES> billPayTypeRepo;
        IGenericRepository<TPAY_TYPES> payTypesRepo;

        public BillPayTypesService(IGenericRepository<BILL_PAY_TYPES> _billPayTypeRepo, IGenericRepository<TPAY_TYPES> _payTypesRepo)
        {
            this.billPayTypeRepo = _billPayTypeRepo;
            this.payTypesRepo = _payTypesRepo;
        }

        public void Dispose()
        {
            billPayTypeRepo.Dispose();
            payTypesRepo.Dispose();
        }

        public Task<int> CountAsync()
        {
            return Task.Run<int>(() =>
            {
                return billPayTypeRepo.CountAsync();
            });
        }

        public Task<bool> DeleteAsync(BILL_PAY_TYPESVM entity)
        {
            return Task.Run<bool>(() =>
            {
                BILL_PAY_TYPES bp = new BILL_PAY_TYPES
                {
                    BillPayTypeID = entity.BillPayTypeID,
                    BillMasterID = entity.BillMasterID,
                    AccountID = entity.AccountID,
                    PayTypeID = entity.PayTypeID,
                    PayTypeValue = entity.PayTypeValue,
                    BankCommissionRate = entity.BankCommissionRate,
                    BankCommissionValue = entity.BankCommissionValue,
                    MaxCommission = entity.MaxCommission,
                    CommissionTaxRate = entity.CommissionTaxRate,
                    CommissionTaxValue = entity.CommissionTaxValue
                };
                billPayTypeRepo.Delete(bp, entity.BillPayTypeID);
                return true;
            });
        }

        public Task<List<BILL_PAY_TYPESVM>> GetAllAsync(int pageNum, int pageSize)
        {
            return Task.Run<List<BILL_PAY_TYPESVM>>(() =>
            {
                int rowCount;
                var q = from entity in billPayTypeRepo.GetPaged<long>(pageNum, pageSize, p => p.BillPayTypeID, false, out rowCount)
                        select new BILL_PAY_TYPESVM
                        {
                            BillPayTypeID = entity.BillPayTypeID,
                            BillMasterID = entity.BillMasterID,
                            AccountID = entity.AccountID,
                            PayTypeID = entity.PayTypeID,
                            PayTypeValue = entity.PayTypeValue,
                            BankCommissionRate = entity.BankCommissionRate,
                            BankCommissionValue = entity.BankCommissionValue,
                            MaxCommission = entity.MaxCommission,
                            CommissionTaxRate = entity.CommissionTaxRate,
                            CommissionTaxValue = entity.CommissionTaxValue

                        };
                return q.ToList();
            });
        }

        public Task<bool> InsertAsync(BILL_PAY_TYPESVM entity)
        {
            return Task.Run<bool>(() =>
            {
                BILL_PAY_TYPES bpt = new BILL_PAY_TYPES
                {
                    BillPayTypeID = entity.BillPayTypeID,
                    BillMasterID = entity.BillMasterID,
                    AccountID = entity.AccountID,
                    PayTypeID = entity.PayTypeID,
                    PayTypeValue = entity.PayTypeValue,
                    BankCommissionRate = entity.BankCommissionRate,
                    BankCommissionValue = entity.BankCommissionValue,
                    MaxCommission = entity.MaxCommission,
                    CommissionTaxRate = entity.CommissionTaxRate,
                    CommissionTaxValue = entity.CommissionTaxValue
                };
                billPayTypeRepo.Add(bpt);
                return true;
            });
        }

        public Task<bool> UpdateAsync(BILL_PAY_TYPESVM entity)
        {
            return Task.Run<bool>(() =>
            {
                BILL_PAY_TYPES bp = new BILL_PAY_TYPES
                {
                    BillPayTypeID = entity.BillPayTypeID,
                    BillMasterID = entity.BillMasterID,
                    AccountID = entity.AccountID,
                    PayTypeID = entity.PayTypeID,
                    PayTypeValue = entity.PayTypeValue,
                    BankCommissionRate = entity.BankCommissionRate,
                    BankCommissionValue = entity.BankCommissionValue,
                    MaxCommission = entity.MaxCommission,
                    CommissionTaxRate = entity.CommissionTaxRate,
                    CommissionTaxValue = entity.CommissionTaxValue

                };
                billPayTypeRepo.Update(bp, bp.BillPayTypeID);
                return true;
            });


        }



        public Task<List<BILL_PAY_TYPESVM>> getByMasterID(int masterID)
        {
            return Task.Run<List<BILL_PAY_TYPESVM>>(() =>
            {

                var q = from entity in billPayTypeRepo.Filter(b => b.BillMasterID == masterID)
                    join pay in payTypesRepo.GetAll() on entity.PayTypeID equals pay.PAY_TYPE_ID into g1
                    from g in g1.DefaultIfEmpty()
                    orderby entity.PayTypeID ascending
                    select new BILL_PAY_TYPESVM
                    {
                        BillPayTypeID = entity.BillPayTypeID,
                        BillMasterID = entity.BillMasterID,
                        AccountID = entity.AccountID,
                        PayTypeID = entity.PayTypeID,
                        PayTypeValue = entity.PayTypeValue,
                        BankCommissionRate = entity.BankCommissionRate,
                        BankCommissionValue = entity.BankCommissionValue,
                        MaxCommission = entity.MaxCommission,
                        CommissionTaxRate = entity.CommissionTaxRate,
                        CommissionTaxValue = entity.CommissionTaxValue,
                        PAY_TYPE_AR_NAME = g.PAY_TYPE_AR_NAME
                    };
            return q.ToList();
            });
        }



        public bool DeleteByMasterID (int? MasterID)
        {
            var existPayTypes = billPayTypeRepo.Filter(p => p.BillMasterID == MasterID).ToList();
            if(existPayTypes.Count >0)
            {
                foreach (var entity in existPayTypes)
                {
                    BILL_PAY_TYPES bp = new BILL_PAY_TYPES
                    {
                        BillPayTypeID = entity.BillPayTypeID,
                        BillMasterID = entity.BillMasterID,
                        AccountID = entity.AccountID,
                        PayTypeID = entity.PayTypeID,
                        PayTypeValue = entity.PayTypeValue,
                        BankCommissionRate = entity.BankCommissionRate,
                        BankCommissionValue = entity.BankCommissionValue,
                        MaxCommission = entity.MaxCommission,
                        CommissionTaxRate = entity.CommissionTaxRate,
                        CommissionTaxValue = entity.CommissionTaxValue
                    };
                    billPayTypeRepo.Delete(bp, entity.BillPayTypeID);
                }
            }
            return true;
        }


        public Task<bool> InsertWithMasterID(List<BILL_PAY_TYPESVM> payTypeList)
        {
            return Task.Run<bool>(() =>
            {
                bool delete = DeleteByMasterID(payTypeList[0].BillMasterID);
                if (delete == true)
                {

                    foreach (var entity in payTypeList)
                    {
                        BILL_PAY_TYPES bp = new BILL_PAY_TYPES
                        {
                            BillPayTypeID = entity.BillPayTypeID,
                            BillMasterID = entity.BillMasterID,
                            AccountID = entity.AccountID,
                            PayTypeID = entity.PayTypeID,
                            PayTypeValue = entity.PayTypeValue,
                            BankCommissionRate = entity.BankCommissionRate,
                            BankCommissionValue = entity.BankCommissionValue,
                            MaxCommission = entity.MaxCommission,
                            CommissionTaxRate = entity.CommissionTaxRate,
                            CommissionTaxValue = entity.CommissionTaxValue

                        };
                        billPayTypeRepo.Add(bp);
                    }
                }
                return true;
            });


        }


    }
}
