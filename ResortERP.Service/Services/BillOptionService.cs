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
    public class BillOptionService : IBillOptionService, IDisposable
    {
        IGenericRepository<BILL_OPTIONS> billOptRepo;
        public BillOptionService(IGenericRepository<BILL_OPTIONS> billOptRepo)
        {
            this.billOptRepo = billOptRepo;
        }

        public Task<int> CountAsync()
        {
            return Task.Run<int>(() =>
            {
                return billOptRepo.CountAsync();
            });
        }

        public bool Delete(BILL_OPTIONSVM entity)
        {
            BILL_OPTIONS bt = new BILL_OPTIONS
            {
                BILL_OPTION_ID = entity.BILL_OPTION_ID,
                BILL_OPTION_ABRV_AR = entity.BILL_OPTION_ABRV_AR,
                BILL_OPTION_ABRV_EN = entity.BILL_OPTION_ABRV_EN,
                BILL_OPTION_AR_NAME = entity.BILL_OPTION_AR_NAME,
                BILL_OPTION_EN_NAME = entity.BILL_OPTION_EN_NAME,
                AddedBy = entity.AddedBy,
                AddedOn = entity.AddedOn,
                UpdatedBy = entity.UpdatedBy,
                UpdatedOn = entity.UpdatedOn
            };
            billOptRepo.Delete(bt, entity.BILL_OPTION_ID);
            return true;
        }

        public Task<bool> DeleteAsync(BILL_OPTIONSVM entity)
        {
            return Task.Run<bool>(() =>
            {
                BILL_OPTIONS bt = new BILL_OPTIONS
                {
                    BILL_OPTION_ID = entity.BILL_OPTION_ID,
                    BILL_OPTION_ABRV_AR = entity.BILL_OPTION_ABRV_AR,
                    BILL_OPTION_ABRV_EN = entity.BILL_OPTION_ABRV_EN,
                    BILL_OPTION_AR_NAME = entity.BILL_OPTION_AR_NAME,
                    BILL_OPTION_EN_NAME = entity.BILL_OPTION_EN_NAME,
                    AddedBy = entity.AddedBy,
                    AddedOn = entity.AddedOn,
                    UpdatedBy = entity.UpdatedBy,
                    UpdatedOn = entity.UpdatedOn
                };
                billOptRepo.Delete(bt, entity.BILL_OPTION_ID);
                return true;
            });
        }

        public void Dispose()
        {
            billOptRepo.Dispose();
        }

        public Task<List<BILL_OPTIONSVM>> GetAllAsync(int pageNum, int pageSize)
        {
            return Task.Run<List<BILL_OPTIONSVM>>(() =>
            {
                int rowCount;
                var q = from entity in billOptRepo.GetPaged<long>(pageNum, pageSize, p => p.BILL_OPTION_ID, false, out rowCount)
                        select new BILL_OPTIONSVM
                        {
                            BILL_OPTION_ID = entity.BILL_OPTION_ID,
                            BILL_OPTION_ABRV_AR = entity.BILL_OPTION_ABRV_AR,
                            BILL_OPTION_ABRV_EN = entity.BILL_OPTION_ABRV_EN,
                            BILL_OPTION_AR_NAME = entity.BILL_OPTION_AR_NAME,
                            BILL_OPTION_EN_NAME = entity.BILL_OPTION_EN_NAME,
                            AddedBy = entity.AddedBy,
                            AddedOn = entity.AddedOn,
                            UpdatedBy = entity.UpdatedBy,
                            UpdatedOn = entity.UpdatedOn
                        };
                return q.ToList();
            });
        }

        public List<BILL_OPTIONSVM> GetAll()
        {
            var q = from entity in billOptRepo.GetAll()
                    select new BILL_OPTIONSVM
                    {
                        BILL_OPTION_ID = entity.BILL_OPTION_ID,
                        BILL_OPTION_ABRV_AR = entity.BILL_OPTION_ABRV_AR,
                        BILL_OPTION_ABRV_EN = entity.BILL_OPTION_ABRV_EN,
                        BILL_OPTION_AR_NAME = entity.BILL_OPTION_AR_NAME,
                        BILL_OPTION_EN_NAME = entity.BILL_OPTION_EN_NAME,
                        AddedBy = entity.AddedBy,
                        AddedOn = entity.AddedOn,
                        UpdatedBy = entity.UpdatedBy,
                        UpdatedOn = entity.UpdatedOn
                    };
            return q.ToList();
        }

        public bool Insert(BILL_OPTIONSVM entity)
        {
            BILL_OPTIONS bt = new BILL_OPTIONS
            {
                BILL_OPTION_ID = entity.BILL_OPTION_ID,
                BILL_OPTION_ABRV_AR = entity.BILL_OPTION_ABRV_AR,
                BILL_OPTION_ABRV_EN = entity.BILL_OPTION_ABRV_EN,
                BILL_OPTION_AR_NAME = entity.BILL_OPTION_AR_NAME,
                BILL_OPTION_EN_NAME = entity.BILL_OPTION_EN_NAME,
                AddedBy = entity.AddedBy,
                AddedOn = entity.AddedOn,
                UpdatedBy = entity.UpdatedBy,
                UpdatedOn = entity.UpdatedOn
            };
            billOptRepo.Add(bt);
            return true;
        }

        public Task<bool> InsertAsync(BILL_OPTIONSVM entity)
        {
            return Task.Run<bool>(() =>
            {
                BILL_OPTIONS bt = new BILL_OPTIONS
                {
                    BILL_OPTION_ID = entity.BILL_OPTION_ID,
                    BILL_OPTION_ABRV_AR = entity.BILL_OPTION_ABRV_AR,
                    BILL_OPTION_ABRV_EN = entity.BILL_OPTION_ABRV_EN,
                    BILL_OPTION_AR_NAME = entity.BILL_OPTION_AR_NAME,
                    BILL_OPTION_EN_NAME = entity.BILL_OPTION_EN_NAME,
                    AddedBy = entity.AddedBy,
                    AddedOn = entity.AddedOn,
                    UpdatedBy = entity.UpdatedBy,
                    UpdatedOn = entity.UpdatedOn
                };
                billOptRepo.Add(bt);
                return true;
            });
        }

        public bool Update(BILL_OPTIONSVM entity)
        {
            BILL_OPTIONS bt = new BILL_OPTIONS
            {
                BILL_OPTION_ID = entity.BILL_OPTION_ID,
                BILL_OPTION_ABRV_AR = entity.BILL_OPTION_ABRV_AR,
                BILL_OPTION_ABRV_EN = entity.BILL_OPTION_ABRV_EN,
                BILL_OPTION_AR_NAME = entity.BILL_OPTION_AR_NAME,
                BILL_OPTION_EN_NAME = entity.BILL_OPTION_EN_NAME,
                AddedBy = entity.AddedBy,
                AddedOn = entity.AddedOn,
                UpdatedBy = entity.UpdatedBy,
                UpdatedOn = entity.UpdatedOn
            };
            billOptRepo.Update(bt, bt.BILL_OPTION_ID);
            return true;
        }

        public Task<bool> UpdateAsync(BILL_OPTIONSVM entity)
        {
            return Task.Run<bool>(() =>
            {
                BILL_OPTIONS bt = new BILL_OPTIONS
                {
                    BILL_OPTION_ID = entity.BILL_OPTION_ID,
                    BILL_OPTION_ABRV_AR = entity.BILL_OPTION_ABRV_AR,
                    BILL_OPTION_ABRV_EN = entity.BILL_OPTION_ABRV_EN,
                    BILL_OPTION_AR_NAME = entity.BILL_OPTION_AR_NAME,
                    BILL_OPTION_EN_NAME = entity.BILL_OPTION_EN_NAME,
                    AddedBy = entity.AddedBy,
                    AddedOn = entity.AddedOn,
                    UpdatedBy = entity.UpdatedBy,
                    UpdatedOn = entity.UpdatedOn
                };
                billOptRepo.Update(bt, bt.BILL_OPTION_ID);
                return true;
            });
        }
    }
}
