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
    public class TPAY_TYPESService : ITPAY_TYPESService, IDisposable
    {
        IGenericRepository<TPAY_TYPES> _TPAY_TYPESRepo;
        IGenericRepository<ACCOUNTS> accountRepo;
        public TPAY_TYPESService(IGenericRepository<TPAY_TYPES> TPAY_TYPESRepo, IGenericRepository<ACCOUNTS> _accountRepo)
        {
            this._TPAY_TYPESRepo = TPAY_TYPESRepo;
            this.accountRepo = _accountRepo;
        }

        public Task<int> CountAsync()
        {
            return Task.Run<int>(() =>
            {
                return _TPAY_TYPESRepo.CountAsync();
            });
        }

        public bool Delete(TPAY_TYPESVM entity)
        {
            TPAY_TYPES tPay = new TPAY_TYPES
            {
                PAY_TYPE_ID = entity.PAY_TYPE_ID
            };
            _TPAY_TYPESRepo.Delete(tPay, entity.PAY_TYPE_ID);
            return true;
        }

        public Task<bool> DeleteAsync(TPAY_TYPESVM entity)
        {
            return Task.Run<bool>(() =>
            {
                TPAY_TYPES tPay = new TPAY_TYPES
                {
                    PAY_TYPE_ID = entity.PAY_TYPE_ID
                };

                _TPAY_TYPESRepo.Delete(tPay, entity.PAY_TYPE_ID);
                return true;
            });
        }

        public void Dispose()
        {
            _TPAY_TYPESRepo.Dispose();
        }

        //public List<TPAY_TYPESVM> GetAll()
        //{
        //    var q = from entity in _TPAY_TYPESRepo.GetAll()
        //            join a in accountRepo.GetAll() on entity.AccountID equals a.ACC_ID into g
        //            from x in g.DefaultIfEmpty()
        //            select new TPAY_TYPESVM
        //            {
        //                PAY_TYPE_ID = entity.PAY_TYPE_ID,
        //                AddedBy = entity.AddedBy,
        //                AddedOn = entity.AddedOn,
        //                Disable = entity.Disable,
        //                PAY_TYPE_AR_NAME = entity.PAY_TYPE_AR_NAME,
        //                PAY_TYPE_CODE = entity.PAY_TYPE_CODE,
        //                PAY_TYPE_EN_NAME = entity.PAY_TYPE_EN_NAME,
        //                PAY_TYPE_NOTES = entity.PAY_TYPE_NOTES,
        //                UpdatedBy = entity.UpdatedBy,
        //                UpdatedOn = entity.UpdatedOn,
        //                AccountID=entity.AccountID,
        //                MaxCommission=entity.MaxCommission,
        //                BankCommissionRate=entity.BankCommissionRate,
        //                CommissionTaxRate= entity.CommissionTaxRate,
        //              //  AccountName = (string)(x.ACC_AR_NAME == null ? string.Empty : x.ACC_AR_NAME) 
        //            };
        //    return q.ToList();
        //}

        public Task<List<TPAY_TYPESVM>> GetAll()
        {
            return Task.Run<List<TPAY_TYPESVM>>(() =>
            {
                var q = from entity in _TPAY_TYPESRepo.GetAll()
                        join a in accountRepo.GetAll() on entity.AccountID equals a.ACC_ID into g
                        from x in g.DefaultIfEmpty()
                        select new TPAY_TYPESVM
                        {
                            PAY_TYPE_ID = entity.PAY_TYPE_ID,
                            AddedBy = entity.AddedBy,
                            AddedOn = entity.AddedOn,
                            Disable = entity.Disable,
                            PAY_TYPE_AR_NAME = entity.PAY_TYPE_AR_NAME,
                            PAY_TYPE_CODE = entity.PAY_TYPE_CODE,
                            PAY_TYPE_EN_NAME = entity.PAY_TYPE_EN_NAME,
                            PAY_TYPE_NOTES = entity.PAY_TYPE_NOTES,
                            UpdatedBy = entity.UpdatedBy,
                            UpdatedOn = entity.UpdatedOn,
                            AccountID = entity.AccountID,
                            MaxCommission = entity.MaxCommission,
                            BankCommissionRate = entity.BankCommissionRate,
                            CommissionTaxRate = entity.CommissionTaxRate,
                            //  AccountName = (string)(x.ACC_AR_NAME == null ? string.Empty : x.ACC_AR_NAME) 
                        };
                return q.ToList();
            });
        }




        public Task<List<TPAY_TYPESVM>> GetAllAsync(int pageNum, int pageSize)
        {
            return Task.Run<List<TPAY_TYPESVM>>(() =>
            {
                int rowCount;
                var q = from entity in _TPAY_TYPESRepo.GetPaged<long>(pageNum, pageSize, p => p.PAY_TYPE_ID, false, out rowCount)
                        join a in accountRepo.GetPaged<long>(pageNum, pageSize, a => a.ACC_ID, false, out rowCount) on entity.AccountID equals a.ACC_ID into g
                        from g1 in g.DefaultIfEmpty()
                        select new TPAY_TYPESVM
                        {
                            PAY_TYPE_ID = entity.PAY_TYPE_ID,
                            AddedBy = entity.AddedBy,
                            AddedOn = entity.AddedOn,
                            Disable = entity.Disable,
                            PAY_TYPE_AR_NAME = entity.PAY_TYPE_AR_NAME,
                            PAY_TYPE_CODE = entity.PAY_TYPE_CODE,
                            PAY_TYPE_EN_NAME = entity.PAY_TYPE_EN_NAME,
                            PAY_TYPE_NOTES = entity.PAY_TYPE_NOTES,
                            UpdatedBy = entity.UpdatedBy,
                            UpdatedOn = entity.UpdatedOn,
                            AccountID = entity.AccountID,
                            MaxCommission = entity.MaxCommission,
                            BankCommissionRate = entity.BankCommissionRate,
                            CommissionTaxRate = entity.CommissionTaxRate,
                            AccountName = g1.ACC_AR_NAME,
                        };
                return q.ToList();
            });
        }

        public bool Insert(TPAY_TYPESVM entity)
        {
            TPAY_TYPES tPay = new TPAY_TYPES
            {
                PAY_TYPE_ID = entity.PAY_TYPE_ID,
                AddedBy = entity.AddedBy,
                AddedOn = entity.AddedOn,
                Disable = entity.Disable,
                PAY_TYPE_AR_NAME = entity.PAY_TYPE_AR_NAME,
                PAY_TYPE_CODE = entity.PAY_TYPE_CODE,
                PAY_TYPE_EN_NAME = entity.PAY_TYPE_EN_NAME,
                PAY_TYPE_NOTES = entity.PAY_TYPE_NOTES,
                UpdatedBy = entity.UpdatedBy,
                UpdatedOn = entity.UpdatedOn,
                AccountID = entity.AccountID,
                MaxCommission = entity.MaxCommission,
                BankCommissionRate = entity.BankCommissionRate,
                CommissionTaxRate = entity.CommissionTaxRate,
            };
            _TPAY_TYPESRepo.Add(tPay);
            return true;
        }

        public Task<int> InsertAsync(TPAY_TYPESVM entity)
        {
            return Task.Run<int>(() =>
            {
                TPAY_TYPES tPay = new TPAY_TYPES
                {
                    PAY_TYPE_ID = entity.PAY_TYPE_ID,
                    AddedBy = entity.AddedBy,
                    AddedOn = entity.AddedOn,
                    Disable = entity.Disable,
                    PAY_TYPE_AR_NAME = entity.PAY_TYPE_AR_NAME,
                    PAY_TYPE_CODE = entity.PAY_TYPE_CODE,
                    PAY_TYPE_EN_NAME = entity.PAY_TYPE_EN_NAME,
                    PAY_TYPE_NOTES = entity.PAY_TYPE_NOTES,
                    UpdatedBy = entity.UpdatedBy,
                    UpdatedOn = entity.UpdatedOn,
                    AccountID = entity.AccountID,
                    MaxCommission = entity.MaxCommission,
                    BankCommissionRate = entity.BankCommissionRate,
                    CommissionTaxRate = entity.CommissionTaxRate,
                };
                _TPAY_TYPESRepo.Add(tPay);
                return tPay.PAY_TYPE_ID;
            });
        }

        public bool Update(TPAY_TYPESVM entity)
        {
            TPAY_TYPES tPay = new TPAY_TYPES
            {
                PAY_TYPE_ID = entity.PAY_TYPE_ID,
                AddedBy = entity.AddedBy,
                AddedOn = entity.AddedOn,
                Disable = entity.Disable,
                PAY_TYPE_AR_NAME = entity.PAY_TYPE_AR_NAME,
                PAY_TYPE_CODE = entity.PAY_TYPE_CODE,
                PAY_TYPE_EN_NAME = entity.PAY_TYPE_EN_NAME,
                PAY_TYPE_NOTES = entity.PAY_TYPE_NOTES,
                UpdatedBy = entity.UpdatedBy,
                UpdatedOn = entity.UpdatedOn,
                AccountID = entity.AccountID,
                MaxCommission = entity.MaxCommission,
                BankCommissionRate = entity.BankCommissionRate,
                CommissionTaxRate = entity.CommissionTaxRate,
            };
            _TPAY_TYPESRepo.Update(tPay, tPay.PAY_TYPE_ID);
            return true;
        }

        public Task<bool> UpdateAsync(TPAY_TYPESVM entity)
        {
            return Task.Run<bool>(() =>
            {
                TPAY_TYPES tPay = new TPAY_TYPES
                {
                    PAY_TYPE_ID = entity.PAY_TYPE_ID,
                    AddedBy = entity.AddedBy,
                    AddedOn = entity.AddedOn,
                    Disable = entity.Disable,
                    PAY_TYPE_AR_NAME = entity.PAY_TYPE_AR_NAME,
                    PAY_TYPE_CODE = entity.PAY_TYPE_CODE,
                    PAY_TYPE_EN_NAME = entity.PAY_TYPE_EN_NAME,
                    PAY_TYPE_NOTES = entity.PAY_TYPE_NOTES,
                    UpdatedBy = entity.UpdatedBy,
                    UpdatedOn = entity.UpdatedOn,
                    AccountID = entity.AccountID,
                    MaxCommission = entity.MaxCommission,
                    BankCommissionRate = entity.BankCommissionRate,
                    CommissionTaxRate = entity.CommissionTaxRate,
                };
                _TPAY_TYPESRepo.Update(tPay, tPay.PAY_TYPE_ID);
                return true;
            });
        }


        public string GetLastCode()
        {
            var lastCode = _TPAY_TYPESRepo.GetAll().OrderByDescending(t => t.PAY_TYPE_ID).FirstOrDefault();
            return lastCode.PAY_TYPE_CODE;
        }




        public Task<TPAY_TYPESVM> GetByID(int PayTypeID)
        {
            return Task.Run<TPAY_TYPESVM>(() =>
            {
                var q = from entity in _TPAY_TYPESRepo.Filter(p => p.PAY_TYPE_ID == PayTypeID)

                        select new TPAY_TYPESVM
                        {
                            PAY_TYPE_ID = entity.PAY_TYPE_ID,
                            AddedBy = entity.AddedBy,
                            AddedOn = entity.AddedOn,
                            Disable = entity.Disable,
                            PAY_TYPE_AR_NAME = entity.PAY_TYPE_AR_NAME,
                            PAY_TYPE_CODE = entity.PAY_TYPE_CODE,
                            PAY_TYPE_EN_NAME = entity.PAY_TYPE_EN_NAME,
                            PAY_TYPE_NOTES = entity.PAY_TYPE_NOTES,
                            UpdatedBy = entity.UpdatedBy,
                            UpdatedOn = entity.UpdatedOn,
                            AccountID = entity.AccountID,
                            MaxCommission = entity.MaxCommission,
                            BankCommissionRate = entity.BankCommissionRate,
                            CommissionTaxRate = entity.CommissionTaxRate,
                            //  AccountName = (string)(x.ACC_AR_NAME == null ? string.Empty : x.ACC_AR_NAME) 
                        };
                return q.FirstOrDefault();
            });
        }




        public Task<List<TPAY_TYPESVM>> GetAllActive()
        {
            return Task.Run<List<TPAY_TYPESVM>>(() =>
            {
                var q = from entity in _TPAY_TYPESRepo.Filter(p => p.Disable == false)
                        join a in accountRepo.GetAll() on entity.AccountID equals a.ACC_ID into g
                        from x in g.DefaultIfEmpty()
                        select new TPAY_TYPESVM
                        {
                            PAY_TYPE_ID = entity.PAY_TYPE_ID,
                            AddedBy = entity.AddedBy,
                            AddedOn = entity.AddedOn,
                            Disable = entity.Disable,
                            PAY_TYPE_AR_NAME = entity.PAY_TYPE_AR_NAME,
                            PAY_TYPE_CODE = entity.PAY_TYPE_CODE,
                            PAY_TYPE_EN_NAME = entity.PAY_TYPE_EN_NAME,
                            PAY_TYPE_NOTES = entity.PAY_TYPE_NOTES,
                            UpdatedBy = entity.UpdatedBy,
                            UpdatedOn = entity.UpdatedOn,
                            AccountID = entity.AccountID,
                            MaxCommission = entity.MaxCommission,
                            BankCommissionRate = entity.BankCommissionRate,
                            CommissionTaxRate = entity.CommissionTaxRate,
                            //  AccountName = (string)(x.ACC_AR_NAME == null ? string.Empty : x.ACC_AR_NAME) 
                        };
                return q.ToList();
            });
        }


    }
}
