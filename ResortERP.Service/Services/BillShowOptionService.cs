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
    public class BillShowOptionService : IBillShowOptionService, IDisposable
    {
        IGenericRepository<BILL_SHOW_OPTIONS> billShowOptRepo;
        IGenericRepository<BILL_GRID_COLUMNS> bilGrdRepo;
        public BillShowOptionService(IGenericRepository<BILL_SHOW_OPTIONS> billShowOptRepo, IGenericRepository<BILL_GRID_COLUMNS> bilGrdRepo)
        {
            this.billShowOptRepo = billShowOptRepo;
            this.bilGrdRepo = bilGrdRepo;
        }

        public Task<int> CountAsync()
        {
            return Task.Run<int>(() =>
            {
                return billShowOptRepo.CountAsync();
            });
        }

        public bool Delete(BILL_SHOW_OPTIONSVM entity)
        {
            BILL_SHOW_OPTIONS bt = new BILL_SHOW_OPTIONS
            {
                BILL_SHOW_ID = entity.BILL_SHOW_ID,
                BILL_SHOW_ABRV_AR = entity.BILL_SHOW_ABRV_AR,
                BILL_SHOW_ABRV_EN = entity.BILL_SHOW_ABRV_EN,
                BILL_SHOW_AR_NAME = entity.BILL_SHOW_AR_NAME,
                BILL_SHOW_EN_NAME = entity.BILL_SHOW_EN_NAME,
                AddedBy = entity.AddedBy,
                AddedOn = entity.AddedOn,
                UpdatedBy = entity.UpdatedBy,
                UpdatedOn = entity.UpdatedOn
            };
            billShowOptRepo.Delete(bt, entity.BILL_SHOW_ID);
            return true;
        }

        public Task<bool> DeleteAsync(BILL_SHOW_OPTIONSVM entity)
        {
            return Task.Run<bool>(() =>
            {
                BILL_SHOW_OPTIONS bt = new BILL_SHOW_OPTIONS
                {
                    BILL_SHOW_ID = entity.BILL_SHOW_ID,
                    BILL_SHOW_ABRV_AR = entity.BILL_SHOW_ABRV_AR,
                    BILL_SHOW_ABRV_EN = entity.BILL_SHOW_ABRV_EN,
                    BILL_SHOW_AR_NAME = entity.BILL_SHOW_AR_NAME,
                    BILL_SHOW_EN_NAME = entity.BILL_SHOW_EN_NAME,
                    AddedBy = entity.AddedBy,
                    AddedOn = entity.AddedOn,
                    UpdatedBy = entity.UpdatedBy,
                    UpdatedOn = entity.UpdatedOn
                };
                billShowOptRepo.Delete(bt, entity.BILL_SHOW_ID);
                return true;
            });
        }

        public void Dispose()
        {
            billShowOptRepo.Dispose();
            bilGrdRepo.Dispose();
        }

        public Task<List<BILL_SHOW_OPTIONSVM>> GetAllAsync(int pageNum, int pageSize)
        {
            return Task.Run<List<BILL_SHOW_OPTIONSVM>>(() =>
            {
                int rowCount;
                var q = from entity in billShowOptRepo.GetPaged<long>(pageNum, pageSize, p => p.BILL_SHOW_ID, false, out rowCount)
                        select new BILL_SHOW_OPTIONSVM
                        {
                            BILL_SHOW_ID = entity.BILL_SHOW_ID,
                            BILL_SHOW_ABRV_AR = entity.BILL_SHOW_ABRV_AR,
                            BILL_SHOW_ABRV_EN = entity.BILL_SHOW_ABRV_EN,
                            BILL_SHOW_AR_NAME = entity.BILL_SHOW_AR_NAME,
                            BILL_SHOW_EN_NAME = entity.BILL_SHOW_EN_NAME,
                            AddedBy = entity.AddedBy,
                            AddedOn = entity.AddedOn,
                            UpdatedBy = entity.UpdatedBy,
                            UpdatedOn = entity.UpdatedOn
                        };
                return q.ToList();
            });
        }

        public List<BILL_SHOW_OPTIONSVM> GetAll()
        {
            var q = from entity in billShowOptRepo.GetAll()
                    select new BILL_SHOW_OPTIONSVM
                    {
                        BILL_SHOW_ID = entity.BILL_SHOW_ID,
                        BILL_SHOW_ABRV_AR = entity.BILL_SHOW_ABRV_AR,
                        BILL_SHOW_ABRV_EN = entity.BILL_SHOW_ABRV_EN,
                        BILL_SHOW_AR_NAME = entity.BILL_SHOW_AR_NAME,
                        BILL_SHOW_EN_NAME = entity.BILL_SHOW_EN_NAME,
                        AddedBy = entity.AddedBy,
                        AddedOn = entity.AddedOn,
                        UpdatedBy = entity.UpdatedBy,
                        UpdatedOn = entity.UpdatedOn
                    };
            return q.ToList();
        }

        public bool Insert(BILL_SHOW_OPTIONSVM entity)
        {
            BILL_SHOW_OPTIONS bt = new BILL_SHOW_OPTIONS
            {
                BILL_SHOW_ID = entity.BILL_SHOW_ID,
                BILL_SHOW_ABRV_AR = entity.BILL_SHOW_ABRV_AR,
                BILL_SHOW_ABRV_EN = entity.BILL_SHOW_ABRV_EN,
                BILL_SHOW_AR_NAME = entity.BILL_SHOW_AR_NAME,
                BILL_SHOW_EN_NAME = entity.BILL_SHOW_EN_NAME,
                AddedBy = entity.AddedBy,
                AddedOn = entity.AddedOn,
                UpdatedBy = entity.UpdatedBy,
                UpdatedOn = entity.UpdatedOn
            };
            billShowOptRepo.Add(bt);
            return true;
        }

        public Task<bool> InsertAsync(BILL_SHOW_OPTIONSVM entity)
        {
            return Task.Run<bool>(() =>
            {
                BILL_SHOW_OPTIONS bt = new BILL_SHOW_OPTIONS
                {
                    BILL_SHOW_ID = entity.BILL_SHOW_ID,
                    BILL_SHOW_ABRV_AR = entity.BILL_SHOW_ABRV_AR,
                    BILL_SHOW_ABRV_EN = entity.BILL_SHOW_ABRV_EN,
                    BILL_SHOW_AR_NAME = entity.BILL_SHOW_AR_NAME,
                    BILL_SHOW_EN_NAME = entity.BILL_SHOW_EN_NAME,
                    AddedBy = entity.AddedBy,
                    AddedOn = entity.AddedOn,
                    UpdatedBy = entity.UpdatedBy,
                    UpdatedOn = entity.UpdatedOn
                };
                billShowOptRepo.Add(bt);
                return true;
            });
        }

        public bool Update(BILL_SHOW_OPTIONSVM entity)
        {
            BILL_SHOW_OPTIONS bt = new BILL_SHOW_OPTIONS
            {
                BILL_SHOW_ID = entity.BILL_SHOW_ID,
                BILL_SHOW_ABRV_AR = entity.BILL_SHOW_ABRV_AR,
                BILL_SHOW_ABRV_EN = entity.BILL_SHOW_ABRV_EN,
                BILL_SHOW_AR_NAME = entity.BILL_SHOW_AR_NAME,
                BILL_SHOW_EN_NAME = entity.BILL_SHOW_EN_NAME,
                AddedBy = entity.AddedBy,
                AddedOn = entity.AddedOn,
                UpdatedBy = entity.UpdatedBy,
                UpdatedOn = entity.UpdatedOn
            };
            billShowOptRepo.Update(bt, bt.BILL_SHOW_ID);
            return true;
        }

        public Task<bool> UpdateAsync(BILL_SHOW_OPTIONSVM entity)
        {
            return Task.Run<bool>(() =>
            {
                BILL_SHOW_OPTIONS bt = new BILL_SHOW_OPTIONS
                {
                    BILL_SHOW_ID = entity.BILL_SHOW_ID,
                    BILL_SHOW_ABRV_AR = entity.BILL_SHOW_ABRV_AR,
                    BILL_SHOW_ABRV_EN = entity.BILL_SHOW_ABRV_EN,
                    BILL_SHOW_AR_NAME = entity.BILL_SHOW_AR_NAME,
                    BILL_SHOW_EN_NAME = entity.BILL_SHOW_EN_NAME,
                    AddedBy = entity.AddedBy,
                    AddedOn = entity.AddedOn,
                    UpdatedBy = entity.UpdatedBy,
                    UpdatedOn = entity.UpdatedOn
                };
                billShowOptRepo.Update(bt, bt.BILL_SHOW_ID);
                return true;
            });
        }
    }
}
