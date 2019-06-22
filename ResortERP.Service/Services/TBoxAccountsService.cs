using ResortERP.Core;
using ResortERP.Core.VM;
using ResortERP.Repository;
using ResortERP.Service.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace ResortERP.Service.Services
{
    public class TBoxAccountsService : ITBoxAccountsService, IDisposable
    {
        IGenericRepository<TBOXACCOUNTS> tBoxAccRepo;
        public TBoxAccountsService(IGenericRepository<TBOXACCOUNTS> tBoxAccRepo)
        {
            this.tBoxAccRepo = tBoxAccRepo;
        }

        public Task<int> CountAsync()
        {
            return Task.Run<int>(() =>
            {
                return tBoxAccRepo.CountAsync();
            });
        }

        public bool Delete(TBoxAccountsVM entity)
        {
            TBOXACCOUNTS tBoxAcc = new TBOXACCOUNTS
            {
                BOXACC_ID = entity.BOXACC_ID
            };
            tBoxAccRepo.Delete(tBoxAcc, entity.BOXACC_ID);
            return true;
        }

        public Task<bool> DeleteAsync(TBoxAccountsVM entity)
        {
            return Task.Run<bool>(() =>
            {
                TBOXACCOUNTS tBoxAcc = new TBOXACCOUNTS
                {
                    BOXACC_ID = entity.BOXACC_ID
                };

                tBoxAccRepo.Delete(tBoxAcc, entity.BOXACC_ID);
                return true;
            });
        }

        public void Dispose()
        {
            tBoxAccRepo.Dispose();
        }

        public List<TBoxAccountsVM> GetAll()
        {
            var q = (from entity in tBoxAccRepo.GetAll() select entity).Select(X => Mapper.Map<TBOXACCOUNTS, TBoxAccountsVM>(X));
            return q.ToList();
        }

        public Task<List<TBoxAccountsVM>> GetAllAsync(int pageNum, int pageSize)
        {
            return Task.Run<List<TBoxAccountsVM>>(() =>
            {
                int rowCount;
                var q = (from entity in tBoxAccRepo.GetPaged<long>(pageNum, pageSize, p => p.BOXACC_ID, false, out rowCount)
                         select entity).Select(X => Mapper.Map<TBOXACCOUNTS, TBoxAccountsVM>(X));
                return q.ToList();
            });
        }

        public Task<List<TBoxAccountsVM>> GetByUID(string userName)
        {
            return Task.Run<List<TBoxAccountsVM>>(() =>
            {
                return tBoxAccRepo.Filter(X => X.UID == userName).Select(Y => Mapper.Map<TBOXACCOUNTS, TBoxAccountsVM>(Y)).ToList();
            });
        }

        public bool Insert(TBoxAccountsVM entity)
        {
            TBOXACCOUNTS tBoxAcc = Mapper.Map<TBoxAccountsVM, TBOXACCOUNTS>(entity);
            TBOXACCOUNTS tBox = tBoxAccRepo.Add(tBoxAcc);
            return tBox != null ? true : false;
        }

        public Task<bool> InsertAsync(TBoxAccountsVM entity)
        {
            return Task.Run<bool>(() =>
            {
                TBOXACCOUNTS tBoxAcc = Mapper.Map<TBoxAccountsVM, TBOXACCOUNTS>(entity);
                TBOXACCOUNTS tBox = tBoxAccRepo.Add(tBoxAcc);
                return tBox != null ? true : false;
            });
        }

        public bool Update(TBoxAccountsVM entity)
        {
            TBOXACCOUNTS tBoxAcc = Mapper.Map<TBoxAccountsVM, TBOXACCOUNTS>(entity);
            TBOXACCOUNTS tBox = tBoxAccRepo.Update(tBoxAcc, tBoxAcc.BOXACC_ID);
            return tBox != null ? true : false;
        }

        public Task<bool> UpdateAsync(TBoxAccountsVM entity)
        {
            return Task.Run<bool>(() =>
            {
                TBOXACCOUNTS tBoxAcc = Mapper.Map<TBoxAccountsVM, TBOXACCOUNTS>(entity);
                TBOXACCOUNTS tBox = tBoxAccRepo.Update(tBoxAcc, tBoxAcc.BOXACC_ID);
                return tBox != null ? true : false;
            });
        }
    }
}
