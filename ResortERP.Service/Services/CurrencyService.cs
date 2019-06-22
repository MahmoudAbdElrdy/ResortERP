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
    public class CurrencyService : ICurrencyService, IDisposable
    {
        IGenericRepository<Currency> currencyRepo;

        public CurrencyService(IGenericRepository<Currency> currencyRepo)
        {
            this.currencyRepo = currencyRepo;
        }

        public Task<int> CountAsync()
        {
            return Task.Run<int>(() =>
            {
                return currencyRepo.CountAsync();
            });
        }

        public bool Delete(CurrencyVM entity)
        {
            Currency cur = new Currency()
            {
                AddedBy = entity.AddedBy,
                AddedOn = entity.AddedOn,
                CURRENCY_AR_NAME = entity.CURRENCY_AR_NAME,
                CURRENCY_CODE = entity.CURRENCY_CODE,
                CURRENCY_EN_NAME = entity.CURRENCY_EN_NAME,
                CURRENCY_FIXED_RATE = entity.CURRENCY_FIXED_RATE,
                CURRENCY_ID = entity.CURRENCY_ID,
                CURRENCY_RATE = entity.CURRENCY_RATE,
                CURRENCY_SUB_AR_NAME = entity.CURRENCY_SUB_AR_NAME,
                CURRENCY_SUB_EN_NAME = entity.CURRENCY_SUB_EN_NAME,
                CURRENCY_SYMBOL_AR_NAME = entity.CURRENCY_SYMBOL_AR_NAME,
                CURRENCY_SYMBOL_EN_NAME = entity.CURRENCY_SYMBOL_EN_NAME,
                Disable = entity.Disable,
                SUB_TO_CURRENCY_TRANS = entity.SUB_TO_CURRENCY_TRANS,
                UpdatedBy = entity.UpdatedBy,
                updatedOn = entity.updatedOn
            };
            currencyRepo.Delete(cur, entity.CURRENCY_ID);
            return true;
        }

        public Task<bool> DeleteAsync(CurrencyVM entity)
        {
            return Task.Run<bool>(() =>
            {
                Currency cur = new Currency()
                {
                    AddedBy = entity.AddedBy,
                    AddedOn = entity.AddedOn,
                    CURRENCY_AR_NAME = entity.CURRENCY_AR_NAME,
                    CURRENCY_CODE = entity.CURRENCY_CODE,
                    CURRENCY_EN_NAME = entity.CURRENCY_EN_NAME,
                    CURRENCY_FIXED_RATE = entity.CURRENCY_FIXED_RATE,
                    CURRENCY_ID = entity.CURRENCY_ID,
                    CURRENCY_RATE = entity.CURRENCY_RATE,
                    CURRENCY_SUB_AR_NAME = entity.CURRENCY_SUB_AR_NAME,
                    CURRENCY_SUB_EN_NAME = entity.CURRENCY_SUB_EN_NAME,
                    CURRENCY_SYMBOL_AR_NAME = entity.CURRENCY_SYMBOL_AR_NAME,
                    CURRENCY_SYMBOL_EN_NAME = entity.CURRENCY_SYMBOL_EN_NAME,
                    Disable = entity.Disable,
                    SUB_TO_CURRENCY_TRANS = entity.SUB_TO_CURRENCY_TRANS,
                    UpdatedBy = entity.UpdatedBy,
                    updatedOn = entity.updatedOn,
                    DefaultCurrency= entity.DefaultCurrency
                };
                currencyRepo.Delete(cur, entity.CURRENCY_ID);
                return true;
            });
        }

        public void Dispose()
        {
            currencyRepo.Dispose();
        }

        public Task<string> GetCurrencyRate(int CurrencyID)
        {
            return Task.Run<string>(() =>
            {
                return currencyRepo.GetByID(CurrencyID).CURRENCY_RATE.ToString();
            });
        }

        public List<CurrencyVM> GetAll()
        {
            var q = from entity in currencyRepo.GetAll()
                    select new CurrencyVM
                    {
                        AddedBy = entity.AddedBy,
                        AddedOn = entity.AddedOn,
                        CURRENCY_AR_NAME = entity.CURRENCY_AR_NAME,
                        CURRENCY_CODE = entity.CURRENCY_CODE,
                        CURRENCY_EN_NAME = entity.CURRENCY_EN_NAME,
                        CURRENCY_FIXED_RATE = entity.CURRENCY_FIXED_RATE,
                        CURRENCY_ID = entity.CURRENCY_ID,
                        CURRENCY_RATE = entity.CURRENCY_RATE,
                        CURRENCY_SUB_AR_NAME = entity.CURRENCY_SUB_AR_NAME,
                        CURRENCY_SUB_EN_NAME = entity.CURRENCY_SUB_EN_NAME,
                        CURRENCY_SYMBOL_AR_NAME = entity.CURRENCY_SYMBOL_AR_NAME,
                        CURRENCY_SYMBOL_EN_NAME = entity.CURRENCY_SYMBOL_EN_NAME,
                        Disable = entity.Disable,
                        SUB_TO_CURRENCY_TRANS = entity.SUB_TO_CURRENCY_TRANS,
                        UpdatedBy = entity.UpdatedBy,
                        updatedOn = entity.updatedOn,
                        DefaultCurrency= entity.DefaultCurrency
                    };
            return q.ToList();
        }

        public string GetBy(int CurrencyID)
        {
            return currencyRepo.GetByID(CurrencyID).CURRENCY_RATE.ToString();
        }

        public Task<List<CurrencyVM>> GetAllAsync(int pageNum, int pageSize)
        {
            return Task.Run(() =>
            {
                int rowCount;
                var q = from entity in currencyRepo.GetPaged<int>(pageNum, pageSize, p => p.CURRENCY_ID, false, out rowCount)
                        select new CurrencyVM
                        {
                            AddedBy = entity.AddedBy,
                            AddedOn = entity.AddedOn,
                            CURRENCY_AR_NAME = entity.CURRENCY_AR_NAME,
                            CURRENCY_CODE = entity.CURRENCY_CODE,
                            CURRENCY_EN_NAME = entity.CURRENCY_EN_NAME,
                            CURRENCY_FIXED_RATE = entity.CURRENCY_FIXED_RATE,
                            CURRENCY_ID = entity.CURRENCY_ID,
                            CURRENCY_RATE = entity.CURRENCY_RATE,
                            CURRENCY_SUB_AR_NAME = entity.CURRENCY_SUB_AR_NAME,
                            CURRENCY_SUB_EN_NAME = entity.CURRENCY_SUB_EN_NAME,
                            CURRENCY_SYMBOL_AR_NAME = entity.CURRENCY_SYMBOL_AR_NAME,
                            CURRENCY_SYMBOL_EN_NAME = entity.CURRENCY_SYMBOL_EN_NAME,
                            Disable = entity.Disable,
                            SUB_TO_CURRENCY_TRANS = entity.SUB_TO_CURRENCY_TRANS,
                            UpdatedBy = entity.UpdatedBy,
                            updatedOn = entity.updatedOn,
                            DefaultCurrency = entity.DefaultCurrency
                        };
                return q.ToList();
            });
        }


        public Task<CurrencyVM> GetByCurrId(int CurrencyId)
        {
            return Task.Run(() =>
            {
               
                var q = from entity in currencyRepo.GetAll().Where(c => c.CURRENCY_ID == CurrencyId)
                        select new CurrencyVM
                        {
                            AddedBy = entity.AddedBy,
                            AddedOn = entity.AddedOn,
                            CURRENCY_AR_NAME = entity.CURRENCY_AR_NAME,
                            CURRENCY_CODE = entity.CURRENCY_CODE,
                            CURRENCY_EN_NAME = entity.CURRENCY_EN_NAME,
                            CURRENCY_FIXED_RATE = entity.CURRENCY_FIXED_RATE,
                            CURRENCY_ID = entity.CURRENCY_ID,
                            CURRENCY_RATE = entity.CURRENCY_RATE,
                            CURRENCY_SUB_AR_NAME = entity.CURRENCY_SUB_AR_NAME,
                            CURRENCY_SUB_EN_NAME = entity.CURRENCY_SUB_EN_NAME,
                            CURRENCY_SYMBOL_AR_NAME = entity.CURRENCY_SYMBOL_AR_NAME,
                            CURRENCY_SYMBOL_EN_NAME = entity.CURRENCY_SYMBOL_EN_NAME,
                            Disable = entity.Disable,
                            SUB_TO_CURRENCY_TRANS = entity.SUB_TO_CURRENCY_TRANS,
                            UpdatedBy = entity.UpdatedBy,
                            updatedOn = entity.updatedOn,
                            DefaultCurrency = entity.DefaultCurrency
                        };
                return q.FirstOrDefault();
            });
        }



        public bool Insert(CurrencyVM entity)
        {
            Currency cur = new Currency()
            {
                AddedBy = entity.AddedBy,
                AddedOn = entity.AddedOn,
                CURRENCY_AR_NAME = entity.CURRENCY_AR_NAME,
                CURRENCY_CODE = entity.CURRENCY_CODE,
                CURRENCY_EN_NAME = entity.CURRENCY_EN_NAME,
                CURRENCY_FIXED_RATE = entity.CURRENCY_FIXED_RATE,
                CURRENCY_ID = entity.CURRENCY_ID,
                CURRENCY_RATE = entity.CURRENCY_RATE,
                CURRENCY_SUB_AR_NAME = entity.CURRENCY_SUB_AR_NAME,
                CURRENCY_SUB_EN_NAME = entity.CURRENCY_SUB_EN_NAME,
                CURRENCY_SYMBOL_AR_NAME = entity.CURRENCY_SYMBOL_AR_NAME,
                CURRENCY_SYMBOL_EN_NAME = entity.CURRENCY_SYMBOL_EN_NAME,
                Disable = entity.Disable,
                SUB_TO_CURRENCY_TRANS = entity.SUB_TO_CURRENCY_TRANS,
                UpdatedBy = entity.UpdatedBy,
                updatedOn = entity.updatedOn,
                DefaultCurrency = entity.DefaultCurrency
            };
            currencyRepo.Add(cur);
            return true;
        }

        public Task<int> InsertAsync(CurrencyVM entity)
        {
            return Task.Run<int>(() =>
             {
                 Currency cur = new Currency()
                 {
                     AddedBy = entity.AddedBy,
                     AddedOn = entity.AddedOn,
                     CURRENCY_AR_NAME = entity.CURRENCY_AR_NAME,
                     CURRENCY_CODE = entity.CURRENCY_CODE,
                     CURRENCY_EN_NAME = entity.CURRENCY_EN_NAME,
                     CURRENCY_FIXED_RATE = entity.CURRENCY_FIXED_RATE,
                     CURRENCY_ID = entity.CURRENCY_ID,
                     CURRENCY_RATE = entity.CURRENCY_RATE,
                     CURRENCY_SUB_AR_NAME = entity.CURRENCY_SUB_AR_NAME,
                     CURRENCY_SUB_EN_NAME = entity.CURRENCY_SUB_EN_NAME,
                     CURRENCY_SYMBOL_AR_NAME = entity.CURRENCY_SYMBOL_AR_NAME,
                     CURRENCY_SYMBOL_EN_NAME = entity.CURRENCY_SYMBOL_EN_NAME,
                     Disable = entity.Disable,
                     SUB_TO_CURRENCY_TRANS = entity.SUB_TO_CURRENCY_TRANS,
                     UpdatedBy = entity.UpdatedBy,
                     updatedOn = entity.updatedOn,
                     DefaultCurrency = entity.DefaultCurrency
                 };
                 currencyRepo.Add(cur);
                 int id = cur.CURRENCY_ID;
                 return id;
             });
        }

        public bool Update(CurrencyVM entity)
        {
            Currency cur = new Currency()
            {
                AddedBy = entity.AddedBy,
                AddedOn = entity.AddedOn,
                CURRENCY_AR_NAME = entity.CURRENCY_AR_NAME,
                CURRENCY_CODE = entity.CURRENCY_CODE,
                CURRENCY_EN_NAME = entity.CURRENCY_EN_NAME,
                CURRENCY_FIXED_RATE = entity.CURRENCY_FIXED_RATE,
                CURRENCY_ID = entity.CURRENCY_ID,
                CURRENCY_RATE = entity.CURRENCY_RATE,
                CURRENCY_SUB_AR_NAME = entity.CURRENCY_SUB_AR_NAME,
                CURRENCY_SUB_EN_NAME = entity.CURRENCY_SUB_EN_NAME,
                CURRENCY_SYMBOL_AR_NAME = entity.CURRENCY_SYMBOL_AR_NAME,
                CURRENCY_SYMBOL_EN_NAME = entity.CURRENCY_SYMBOL_EN_NAME,
                Disable = entity.Disable,
                SUB_TO_CURRENCY_TRANS = entity.SUB_TO_CURRENCY_TRANS,
                UpdatedBy = entity.UpdatedBy,
                updatedOn = entity.updatedOn,
                DefaultCurrency = entity.DefaultCurrency
            };
            currencyRepo.Update(cur, cur.CURRENCY_ID);
            return true;
        }

        public Task<bool> UpdateAsync(CurrencyVM entity)
        {
            return Task.Run<bool>(() =>
            {
                Currency cur = new Currency()
                {
                    AddedBy = entity.AddedBy,
                    AddedOn = entity.AddedOn,
                    CURRENCY_AR_NAME = entity.CURRENCY_AR_NAME,
                    CURRENCY_CODE = entity.CURRENCY_CODE,
                    CURRENCY_EN_NAME = entity.CURRENCY_EN_NAME,
                    CURRENCY_FIXED_RATE = entity.CURRENCY_FIXED_RATE,
                    CURRENCY_ID = entity.CURRENCY_ID,
                    CURRENCY_RATE = entity.CURRENCY_RATE,
                    CURRENCY_SUB_AR_NAME = entity.CURRENCY_SUB_AR_NAME,
                    CURRENCY_SUB_EN_NAME = entity.CURRENCY_SUB_EN_NAME,
                    CURRENCY_SYMBOL_AR_NAME = entity.CURRENCY_SYMBOL_AR_NAME,
                    CURRENCY_SYMBOL_EN_NAME = entity.CURRENCY_SYMBOL_EN_NAME,
                    Disable = entity.Disable,
                    SUB_TO_CURRENCY_TRANS = entity.SUB_TO_CURRENCY_TRANS,
                    UpdatedBy = entity.UpdatedBy,
                    updatedOn = entity.updatedOn,
                    DefaultCurrency = entity.DefaultCurrency
                };
                currencyRepo.Update(cur, cur.CURRENCY_ID);
                return true;
            });
        }

        public Task<List<CurrencyVM>> GetSearchResultAsync(CurrencyVM entity, int pageNum, int pageSize)
        {
            return Task.Run(() =>
            {
                int rowCount;
                var q = from ent in currencyRepo.GetPaged<int>(pageNum, pageSize, p => p.CURRENCY_ID, false, out rowCount).Where(x => x.CURRENCY_CODE.Contains(entity.CURRENCY_CODE))
                        select new CurrencyVM
                        {
                            AddedBy = ent.AddedBy,
                            AddedOn = ent.AddedOn,
                            CURRENCY_AR_NAME = ent.CURRENCY_AR_NAME,
                            CURRENCY_CODE = ent.CURRENCY_CODE,
                            CURRENCY_EN_NAME = ent.CURRENCY_EN_NAME,
                            CURRENCY_FIXED_RATE = ent.CURRENCY_FIXED_RATE,
                            CURRENCY_ID = ent.CURRENCY_ID,
                            CURRENCY_RATE = ent.CURRENCY_RATE,
                            CURRENCY_SUB_AR_NAME = ent.CURRENCY_SUB_AR_NAME,
                            CURRENCY_SUB_EN_NAME = ent.CURRENCY_SUB_EN_NAME,
                            CURRENCY_SYMBOL_AR_NAME = ent.CURRENCY_SYMBOL_AR_NAME,
                            CURRENCY_SYMBOL_EN_NAME = ent.CURRENCY_SYMBOL_EN_NAME,
                            Disable = ent.Disable,
                            SUB_TO_CURRENCY_TRANS = ent.SUB_TO_CURRENCY_TRANS,
                            UpdatedBy = ent.UpdatedBy,
                            updatedOn = ent.updatedOn,
                            DefaultCurrency = entity.DefaultCurrency
                        };
                return q.ToList();
            });
        }



        public string GetLastCode()
        {
            var Code = currencyRepo.GetAll().OrderByDescending(c => c.CURRENCY_ID).FirstOrDefault();
            string lastCode = Code.CURRENCY_CODE;
            return lastCode;
        }
    }
}
