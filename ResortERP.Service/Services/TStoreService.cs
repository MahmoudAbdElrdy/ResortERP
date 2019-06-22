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
    public class TStoreService : ITStoreService, IDisposable
    {
        IGenericRepository<TSTORE> tStoreRepo;
        public TStoreService(IGenericRepository<TSTORE> tStoreRepo)
        {
            this.tStoreRepo = tStoreRepo;
        }

        public Task<int> CountAsync()
        {
            return Task.Run<int>(() =>
            {
                return tStoreRepo.CountAsync();
            });
        }

        public bool Delete(TStoreVM entity)
        {
            TSTORE tstore = new TSTORE
            {
                TSTORE_ID = entity.TSTORE_ID
            };
            tStoreRepo.Delete(tstore, entity.TSTORE_ID);
            return true;
        }

        public Task<bool> DeleteAsync(TStoreVM entity)
        {
            return Task.Run<bool>(() =>
            {
                TSTORE tstore = new TSTORE
                {
                    TSTORE_ID = entity.TSTORE_ID
                };

                tStoreRepo.Delete(tstore, entity.TSTORE_ID);
                return true;
            });
        }

        public void Dispose()
        {
            tStoreRepo.Dispose();
        }

        public List<TStoreVM> GetAll()
        {
            var q = (from entity in tStoreRepo.GetAll() select entity).Select(X => Mapper.Map<TSTORE, TStoreVM>(X));
            return q.ToList();
        }

        public Task<List<TStoreVM>> GetAllAsync(int pageNum, int pageSize)
        {
            return Task.Run<List<TStoreVM>>(() =>
            {
                int rowCount;
                var q = (from entity in tStoreRepo.GetPaged<long>(pageNum, pageSize, p => p.TSTORE_ID, false, out rowCount)
                         select entity).Select(X => Mapper.Map<TSTORE, TStoreVM>(X));
                return q.ToList();
            });
        }

        public Task<List<TStoreVM>> GetByUID(string userName)
        {
            return Task.Run<List<TStoreVM>>(() =>
            {
                return tStoreRepo.Filter(X => X.UID == userName).Select(Y => Mapper.Map<TSTORE, TStoreVM>(Y)).ToList();
            });
        }

        public bool Insert(TStoreVM entity)
        {
            TSTORE tstore = Mapper.Map<TStoreVM, TSTORE>(entity);
            TSTORE ts = tStoreRepo.Add(tstore);
            return ts != null ? true : false;
        }

        public Task<bool> InsertAsync(TStoreVM entity)
        {
            return Task.Run<bool>(() =>
            {
                TSTORE tstore = Mapper.Map<TStoreVM, TSTORE>(entity);
                TSTORE ts = tStoreRepo.Add(tstore);
                return ts != null ? true : false;
            });
        }

        public bool Update(TStoreVM entity)
        {
            TSTORE tstore = Mapper.Map<TStoreVM, TSTORE>(entity);
            TSTORE ts = tStoreRepo.Update(tstore, tstore.TSTORE_ID);
            return ts != null ? true : false;
        }

        public Task<bool> UpdateAsync(TStoreVM entity)
        {
            return Task.Run<bool>(() =>
            {
                TSTORE tstore = Mapper.Map<TStoreVM, TSTORE>(entity);
                TSTORE ts = tStoreRepo.Update(tstore, tstore.TSTORE_ID);
                return ts != null ? true : false;
            });
        }
    }
}
