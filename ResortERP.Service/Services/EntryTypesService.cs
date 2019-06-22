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
    public class EntryTypesService : IEntryTypesService, IDisposable
    {
        IGenericRepository<ENTRY_TYPES> entryTypesRepo;
        public EntryTypesService(IGenericRepository<ENTRY_TYPES> entryTypesRepo)
        {
            this.entryTypesRepo = entryTypesRepo;
        }

        public Task<int> CountAsync()
        {
            return Task.Run<int>(() =>
            {
                return entryTypesRepo.CountAsync();
            });
        }

        public bool Delete(Entry_TypesVM entity)
        {
            ENTRY_TYPES et = new ENTRY_TYPES
            {
                ENTRY_TYPE_ID = entity.ENTRY_TYPE_ID,
                ENTRY_AR_NAME = entity.ENTRY_AR_NAME,
                ENTRY_EN_NAME = entity.ENTRY_EN_NAME,
                AddedBy = entity.AddedBy,
                AddedOn = entity.AddedOn,
                UpdatedBy = entity.UpdatedBy,
                updatedOn = entity.updatedOn
            };
            entryTypesRepo.Delete(et, entity.ENTRY_TYPE_ID);
            return true;
        }

        public Task<bool> DeleteAsync(Entry_TypesVM entity)
        {
            return Task.Run<bool>(() =>
            {
                ENTRY_TYPES et = new ENTRY_TYPES
                {
                    ENTRY_TYPE_ID = entity.ENTRY_TYPE_ID,
                    ENTRY_AR_NAME = entity.ENTRY_AR_NAME,
                    ENTRY_EN_NAME = entity.ENTRY_EN_NAME,
                    AddedBy = entity.AddedBy,
                    AddedOn = entity.AddedOn,
                    UpdatedBy = entity.UpdatedBy,
                    updatedOn = entity.updatedOn
                };
                entryTypesRepo.Delete(et, entity.ENTRY_TYPE_ID);
                return true;
            });
        }

        public void Dispose()
        {
            entryTypesRepo.Dispose();
        }

        public Task<List<Entry_TypesVM>> GetAllAsync(int pageNum, int pageSize)
        {
            return Task.Run<List<Entry_TypesVM>>(() =>
            {
                int rowCount;
                var q = from entity in entryTypesRepo.GetPaged<long>(pageNum, pageSize, p => p.ENTRY_TYPE_ID, false, out rowCount)
                        select new Entry_TypesVM
                        {
                            ENTRY_TYPE_ID = entity.ENTRY_TYPE_ID,
                            ENTRY_AR_NAME = entity.ENTRY_AR_NAME,
                            ENTRY_EN_NAME = entity.ENTRY_EN_NAME,
                            AddedBy = entity.AddedBy,
                            AddedOn = entity.AddedOn,
                            UpdatedBy = entity.UpdatedBy,
                            updatedOn = entity.updatedOn
                        };
                return q.ToList();
            });
        }

        public List<Entry_TypesVM> GetAll()
        {
            var q = from entity in entryTypesRepo.GetAll()
                    select new Entry_TypesVM
                    {
                        ENTRY_TYPE_ID = entity.ENTRY_TYPE_ID,
                        ENTRY_AR_NAME = entity.ENTRY_AR_NAME,
                        ENTRY_EN_NAME = entity.ENTRY_EN_NAME,
                        AddedBy = entity.AddedBy,
                        AddedOn = entity.AddedOn,
                        UpdatedBy = entity.UpdatedBy,
                        updatedOn = entity.updatedOn
                    };
            return q.ToList();
        }

        public bool Insert(Entry_TypesVM entity)
        {
            ENTRY_TYPES et = new ENTRY_TYPES
            {
                ENTRY_TYPE_ID = entity.ENTRY_TYPE_ID,
                ENTRY_AR_NAME = entity.ENTRY_AR_NAME,
                ENTRY_EN_NAME = entity.ENTRY_EN_NAME,
                AddedBy = entity.AddedBy,
                AddedOn = entity.AddedOn,
                UpdatedBy = entity.UpdatedBy,
                updatedOn = entity.updatedOn
            };
            entryTypesRepo.Add(et);
            return true;
        }

        public Task<bool> InsertAsync(Entry_TypesVM entity)
        {
            return Task.Run<bool>(() =>
            {
                ENTRY_TYPES et = new ENTRY_TYPES
                {
                    ENTRY_TYPE_ID = entity.ENTRY_TYPE_ID,
                    ENTRY_AR_NAME = entity.ENTRY_AR_NAME,
                    ENTRY_EN_NAME = entity.ENTRY_EN_NAME,
                    AddedBy = entity.AddedBy,
                    AddedOn = entity.AddedOn,
                    UpdatedBy = entity.UpdatedBy,
                    updatedOn = entity.updatedOn
                };
                entryTypesRepo.Add(et);
                return true;
            });
        }

        public bool Update(Entry_TypesVM entity)
        {
            ENTRY_TYPES et = new ENTRY_TYPES
            {
                ENTRY_TYPE_ID = entity.ENTRY_TYPE_ID,
                ENTRY_AR_NAME = entity.ENTRY_AR_NAME,
                ENTRY_EN_NAME = entity.ENTRY_EN_NAME,
                AddedBy = entity.AddedBy,
                AddedOn = entity.AddedOn,
                UpdatedBy = entity.UpdatedBy,
                updatedOn = entity.updatedOn
            };
            entryTypesRepo.Update(et, et.ENTRY_TYPE_ID);
            return true;
        }

        public Task<bool> UpdateAsync(Entry_TypesVM entity)
        {
            return Task.Run<bool>(() =>
            {
                ENTRY_TYPES et = new ENTRY_TYPES
                {
                    ENTRY_TYPE_ID = entity.ENTRY_TYPE_ID,
                    ENTRY_AR_NAME = entity.ENTRY_AR_NAME,
                    ENTRY_EN_NAME = entity.ENTRY_EN_NAME,
                    AddedBy = entity.AddedBy,
                    AddedOn = entity.AddedOn,
                    UpdatedBy = entity.UpdatedBy,
                    updatedOn = entity.updatedOn
                };
                entryTypesRepo.Update(et, et.ENTRY_TYPE_ID);
                return true;
            });
        }
    }
}
