using AutoMapper;
using ResortERP.Core;
using ResortERP.Core.VM;
using ResortERP.Repository;
using ResortERP.Service.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResortERP.Service.Services
{
    public class TBudgetAccountsService : ITBudgetAccountsService, IDisposable
    {
        IGenericRepository<TBUDGETACCOUNTS> tBudAccRepo;
        public TBudgetAccountsService(IGenericRepository<TBUDGETACCOUNTS> tBudAccRepo)
        {
            this.tBudAccRepo = tBudAccRepo;
        }

        public Task<int> CountAsync()
        {
            return Task.Run<int>(() =>
            {
                return tBudAccRepo.CountAsync();
            });
        }

        public bool Delete(TBudgetAccountsVM entity)
        {
            TBUDGETACCOUNTS tBudAcc = new TBUDGETACCOUNTS
            {
                BOXACC_ID = entity.BOXACC_ID
            };
            tBudAccRepo.Delete(tBudAcc, entity.BOXACC_ID);
            return true;
        }

        public Task<bool> DeleteAsync(TBudgetAccountsVM entity)
        {
            return Task.Run<bool>(() =>
            {
                TBUDGETACCOUNTS tBudAcc = new TBUDGETACCOUNTS
                {
                    BOXACC_ID = entity.BOXACC_ID
                };

                tBudAccRepo.Delete(tBudAcc, entity.BOXACC_ID);
                return true;
            });
        }

        public void Dispose()
        {
            tBudAccRepo.Dispose();
        }

        public List<TBudgetAccountsVM> GetAll()
        {
            var q = (from entity in tBudAccRepo.GetAll() select entity).Select(X => Mapper.Map<TBUDGETACCOUNTS, TBudgetAccountsVM>(X));
            return q.ToList();
        }

        public Task<List<TBudgetAccountsVM>> GetAllAsync(int pageNum, int pageSize)
        {
            return Task.Run<List<TBudgetAccountsVM>>(() =>
            {
                int rowCount;
                var q = (from entity in tBudAccRepo.GetPaged<long>(pageNum, pageSize, p => p.BOXACC_ID, false, out rowCount)
                         select entity).Select(X => Mapper.Map<TBUDGETACCOUNTS, TBudgetAccountsVM>(X));
                return q.ToList();
            });
        }

        public Task<List<TBudgetAccountsVM>> GetByUID(string userName)
        {
            return Task.Run<List<TBudgetAccountsVM>>(() =>
            {
                var q = tBudAccRepo.Filter(Y => Y.UID == userName).Select(X => Mapper.Map<TBUDGETACCOUNTS, TBudgetAccountsVM>(X));
                return q.ToList();
            });
        }

        public bool Insert(TBudgetAccountsVM entity)
        {
            TBUDGETACCOUNTS tBudAcc = Mapper.Map<TBudgetAccountsVM, TBUDGETACCOUNTS>(entity);
            TBUDGETACCOUNTS em = tBudAccRepo.Add(tBudAcc);
            return em != null ? true : false;
        }

        public Task<bool> InsertAsync(TBudgetAccountsVM entity)
        {
            return Task.Run<bool>(() =>
            {
                TBUDGETACCOUNTS tBudAcc = Mapper.Map<TBudgetAccountsVM, TBUDGETACCOUNTS>(entity);
                TBUDGETACCOUNTS em = tBudAccRepo.Add(tBudAcc);
                return em != null ? true : false;
            });
        }

        public bool Update(TBudgetAccountsVM entity)
        {
            TBUDGETACCOUNTS tBudAcc = Mapper.Map<TBudgetAccountsVM, TBUDGETACCOUNTS>(entity);
            TBUDGETACCOUNTS em = tBudAccRepo.Update(tBudAcc, tBudAcc.BOXACC_ID);
            return em != null ? true : false;
        }

        public Task<bool> UpdateAsync(TBudgetAccountsVM entity)
        {
            return Task.Run<bool>(() =>
            {
                TBUDGETACCOUNTS tBudAcc = Mapper.Map<TBudgetAccountsVM, TBUDGETACCOUNTS>(entity);
                TBUDGETACCOUNTS em = tBudAccRepo.Update(tBudAcc, tBudAcc.BOXACC_ID);
                return em != null ? true : false;
            });
        }
    }
}
