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
    public class EntryShowOptionService : IEntryShowOptionService, IDisposable
    {
        IGenericRepository<ENTRY_SHOW_OPTIONS> EntryShowOptRepo;
        IGenericRepository<ENTRY_GRID_COLUMNS> entryGrdRepo;
        public EntryShowOptionService(IGenericRepository<ENTRY_SHOW_OPTIONS> EntryShowOptRepo, IGenericRepository<ENTRY_GRID_COLUMNS> entryGrdRepo)
        {
            this.EntryShowOptRepo = EntryShowOptRepo;
            this.entryGrdRepo = entryGrdRepo;
        }

        public Task<int> CountAsync()
        {
            return Task.Run<int>(() =>
            {
                return EntryShowOptRepo.CountAsync();
            });
        }

        public bool Delete(Entry_Show_OptionsVM entity)
        {
            ENTRY_SHOW_OPTIONS bt = new ENTRY_SHOW_OPTIONS
            {
                ENTRY_SHOW_ID = entity.ENTRY_SHOW_ID,
                ENTRY_SHOW_ABRV_AR = entity.ENTRY_SHOW_ABRV_AR,
                ENTRY_SHOW_ABRV_EN = entity.ENTRY_SHOW_ABRV_EN,
                ENTRY_SHOW_AR_NAME = entity.ENTRY_SHOW_AR_NAME,
                ENTRY_SHOW_EN_NAME = entity.ENTRY_SHOW_EN_NAME,
                AddedBy = entity.AddedBy,
                AddedOn = entity.AddedOn,
                UpdatedBy = entity.UpdatedBy,
                UpdatedOn = entity.UpdatedOn
            };
            EntryShowOptRepo.Delete(bt, entity.ENTRY_SHOW_ID);
            return true;
        }

        public Task<bool> DeleteAsync(Entry_Show_OptionsVM entity)
        {
            return Task.Run<bool>(() =>
            {
                ENTRY_SHOW_OPTIONS bt = new ENTRY_SHOW_OPTIONS
                {
                    ENTRY_SHOW_ID = entity.ENTRY_SHOW_ID,
                    ENTRY_SHOW_ABRV_AR = entity.ENTRY_SHOW_ABRV_AR,
                    ENTRY_SHOW_ABRV_EN = entity.ENTRY_SHOW_ABRV_EN,
                    ENTRY_SHOW_AR_NAME = entity.ENTRY_SHOW_AR_NAME,
                    ENTRY_SHOW_EN_NAME = entity.ENTRY_SHOW_EN_NAME,
                    AddedBy = entity.AddedBy,
                    AddedOn = entity.AddedOn,
                    UpdatedBy = entity.UpdatedBy,
                    UpdatedOn = entity.UpdatedOn
                };
                EntryShowOptRepo.Delete(bt, entity.ENTRY_SHOW_ID);
                return true;
            });
        }

        public void Dispose()
        {
            EntryShowOptRepo.Dispose();
            entryGrdRepo.Dispose();
        }

        public Task<List<Entry_Show_OptionsVM>> GetAllAsync(int pageNum, int pageSize)
        {
            return Task.Run<List<Entry_Show_OptionsVM>>(() =>
            {
                int rowCount;
                var q = from entity in EntryShowOptRepo.GetPaged<long>(pageNum, pageSize, p => p.ENTRY_SHOW_ID, false, out rowCount)
                        select new Entry_Show_OptionsVM
                        {
                            ENTRY_SHOW_ID = entity.ENTRY_SHOW_ID,
                            ENTRY_SHOW_ABRV_AR = entity.ENTRY_SHOW_ABRV_AR,
                            ENTRY_SHOW_ABRV_EN = entity.ENTRY_SHOW_ABRV_EN,
                            ENTRY_SHOW_AR_NAME = entity.ENTRY_SHOW_AR_NAME,
                            ENTRY_SHOW_EN_NAME = entity.ENTRY_SHOW_EN_NAME,
                            AddedBy = entity.AddedBy,
                            AddedOn = entity.AddedOn,
                            UpdatedBy = entity.UpdatedBy,
                            UpdatedOn = entity.UpdatedOn
                        };
                return q.ToList();
            });
        }

        public List<Entry_Show_OptionsVM> GetAll()
        {
            var q = from entity in EntryShowOptRepo.GetAll()
                    select new Entry_Show_OptionsVM
                    {
                        ENTRY_SHOW_ID = entity.ENTRY_SHOW_ID,
                        ENTRY_SHOW_ABRV_AR = entity.ENTRY_SHOW_ABRV_AR,
                        ENTRY_SHOW_ABRV_EN = entity.ENTRY_SHOW_ABRV_EN,
                        ENTRY_SHOW_AR_NAME = entity.ENTRY_SHOW_AR_NAME,
                        ENTRY_SHOW_EN_NAME = entity.ENTRY_SHOW_EN_NAME,
                        AddedBy = entity.AddedBy,
                        AddedOn = entity.AddedOn,
                        UpdatedBy = entity.UpdatedBy,
                        UpdatedOn = entity.UpdatedOn
                    };
            return q.ToList();
        }

        public bool Insert(Entry_Show_OptionsVM entity)
        {
            ENTRY_SHOW_OPTIONS bt = new ENTRY_SHOW_OPTIONS
            {
                ENTRY_SHOW_ID = entity.ENTRY_SHOW_ID,
                ENTRY_SHOW_ABRV_AR = entity.ENTRY_SHOW_ABRV_AR,
                ENTRY_SHOW_ABRV_EN = entity.ENTRY_SHOW_ABRV_EN,
                ENTRY_SHOW_AR_NAME = entity.ENTRY_SHOW_AR_NAME,
                ENTRY_SHOW_EN_NAME = entity.ENTRY_SHOW_EN_NAME,
                AddedBy = entity.AddedBy,
                AddedOn = entity.AddedOn,
                UpdatedBy = entity.UpdatedBy,
                UpdatedOn = entity.UpdatedOn
            };
            EntryShowOptRepo.Add(bt);
            return true;
        }

        public Task<bool> InsertAsync(Entry_Show_OptionsVM entity)
        {
            return Task.Run<bool>(() =>
            {
                ENTRY_SHOW_OPTIONS bt = new ENTRY_SHOW_OPTIONS
                {
                    ENTRY_SHOW_ID = entity.ENTRY_SHOW_ID,
                    ENTRY_SHOW_ABRV_AR = entity.ENTRY_SHOW_ABRV_AR,
                    ENTRY_SHOW_ABRV_EN = entity.ENTRY_SHOW_ABRV_EN,
                    ENTRY_SHOW_AR_NAME = entity.ENTRY_SHOW_AR_NAME,
                    ENTRY_SHOW_EN_NAME = entity.ENTRY_SHOW_EN_NAME,
                    AddedBy = entity.AddedBy,
                    AddedOn = entity.AddedOn,
                    UpdatedBy = entity.UpdatedBy,
                    UpdatedOn = entity.UpdatedOn
                };
                EntryShowOptRepo.Add(bt);
                return true;
            });
        }

        public bool Update(Entry_Show_OptionsVM entity)
        {
            ENTRY_SHOW_OPTIONS bt = new ENTRY_SHOW_OPTIONS
            {
                ENTRY_SHOW_ID = entity.ENTRY_SHOW_ID,
                ENTRY_SHOW_ABRV_AR = entity.ENTRY_SHOW_ABRV_AR,
                ENTRY_SHOW_ABRV_EN = entity.ENTRY_SHOW_ABRV_EN,
                ENTRY_SHOW_AR_NAME = entity.ENTRY_SHOW_AR_NAME,
                ENTRY_SHOW_EN_NAME = entity.ENTRY_SHOW_EN_NAME,
                AddedBy = entity.AddedBy,
                AddedOn = entity.AddedOn,
                UpdatedBy = entity.UpdatedBy,
                UpdatedOn = entity.UpdatedOn
            };
            EntryShowOptRepo.Update(bt, bt.ENTRY_SHOW_ID);
            return true;
        }

        public Task<bool> UpdateAsync(Entry_Show_OptionsVM entity)
        {
            return Task.Run<bool>(() =>
            {
                ENTRY_SHOW_OPTIONS bt = new ENTRY_SHOW_OPTIONS
                {
                    ENTRY_SHOW_ID = entity.ENTRY_SHOW_ID,
                    ENTRY_SHOW_ABRV_AR = entity.ENTRY_SHOW_ABRV_AR,
                    ENTRY_SHOW_ABRV_EN = entity.ENTRY_SHOW_ABRV_EN,
                    ENTRY_SHOW_AR_NAME = entity.ENTRY_SHOW_AR_NAME,
                    ENTRY_SHOW_EN_NAME = entity.ENTRY_SHOW_EN_NAME,
                    AddedBy = entity.AddedBy,
                    AddedOn = entity.AddedOn,
                    UpdatedBy = entity.UpdatedBy,
                    UpdatedOn = entity.UpdatedOn
                };
                EntryShowOptRepo.Update(bt, bt.ENTRY_SHOW_ID);
                return true;
            });
        }
    }
}
