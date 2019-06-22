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
    public class BillTypesService : IBillTypesService, IDisposable
    {
        IGenericRepository<BILL_TYPES> billTypesRepo;
        public BillTypesService(IGenericRepository<BILL_TYPES> billTypesRepo)
        {
            this.billTypesRepo = billTypesRepo;
        }

        public Task<int> CountAsync()
        {
            return Task.Run<int>(() =>
            {
                return billTypesRepo.CountAsync();
            });
        }

        public bool Delete(BILL_TYPESVM entity)
        {
            BILL_TYPES bt = new BILL_TYPES
            {
                AddedBy = entity.AddedBy,
                AddedOn = entity.AddedOn,
                BILL_EFF_ID = entity.BILL_EFF_ID,
                BILL_TYPE_AR_NAME = entity.BILL_TYPE_AR_NAME,
                BILL_TYPE_EN_NAME = entity.BILL_TYPE_EN_NAME,
                BILL_TYPE_ID = entity.BILL_TYPE_ID,
                Disable = entity.Disable,
                UpdatedBy = entity.UpdatedBy,
                UpdatedOn = entity.UpdatedOn
            };
            billTypesRepo.Delete(bt, entity.BILL_TYPE_ID);
            return true;
        }

        public Task<bool> DeleteAsync(BILL_TYPESVM entity)
        {
            return Task.Run<bool>(() =>
            {
                BILL_TYPES bt = new BILL_TYPES
                {
                    AddedBy = entity.AddedBy,
                    AddedOn = entity.AddedOn,
                    BILL_EFF_ID = entity.BILL_EFF_ID,
                    BILL_TYPE_AR_NAME = entity.BILL_TYPE_AR_NAME,
                    BILL_TYPE_EN_NAME = entity.BILL_TYPE_EN_NAME,
                    BILL_TYPE_ID = entity.BILL_TYPE_ID,
                    Disable = entity.Disable,
                    UpdatedBy = entity.UpdatedBy,
                    UpdatedOn = entity.UpdatedOn
                };
                billTypesRepo.Delete(bt, entity.BILL_TYPE_ID);
                return true;
            });
        }

        public void Dispose()
        {
            billTypesRepo.Dispose();
        }

        public Task<List<BILL_TYPESVM>> GetAllAsync(int pageNum, int pageSize)
        {
            return Task.Run<List<BILL_TYPESVM>>(() =>
            {
                int rowCount;
                var q = from entity in billTypesRepo.GetPaged<long>(pageNum, pageSize, p => p.BILL_TYPE_ID, false, out rowCount)
                        select new BILL_TYPESVM
                        {
                            AddedBy = entity.AddedBy,
                            AddedOn = entity.AddedOn,
                            BILL_EFF_ID = entity.BILL_EFF_ID,
                            BILL_TYPE_AR_NAME = entity.BILL_TYPE_AR_NAME,
                            BILL_TYPE_EN_NAME = entity.BILL_TYPE_EN_NAME,
                            BILL_TYPE_ID = entity.BILL_TYPE_ID,
                            Disable = entity.Disable,
                            UpdatedBy = entity.UpdatedBy,
                            UpdatedOn = entity.UpdatedOn
                        };
                return q.ToList();
            });
        }

        public List<BILL_TYPESVM> GetAll()
        {
            var q = from entity in billTypesRepo.GetAll()
                    select new BILL_TYPESVM
                    {
                        AddedBy = entity.AddedBy,
                        AddedOn = entity.AddedOn,
                        BILL_EFF_ID = entity.BILL_EFF_ID,
                        BILL_TYPE_AR_NAME = entity.BILL_TYPE_AR_NAME,
                        BILL_TYPE_EN_NAME = entity.BILL_TYPE_EN_NAME,
                        BILL_TYPE_ID = entity.BILL_TYPE_ID,
                        Disable = entity.Disable,
                        UpdatedBy = entity.UpdatedBy,
                        UpdatedOn = entity.UpdatedOn
                    };
            return q.ToList();
        }

        public bool Insert(BILL_TYPESVM entity)
        {
            BILL_TYPES bt = new BILL_TYPES
            {
                AddedBy = entity.AddedBy,
                AddedOn = entity.AddedOn,
                BILL_EFF_ID = entity.BILL_EFF_ID,
                BILL_TYPE_AR_NAME = entity.BILL_TYPE_AR_NAME,
                BILL_TYPE_EN_NAME = entity.BILL_TYPE_EN_NAME,
                BILL_TYPE_ID = entity.BILL_TYPE_ID,
                Disable = entity.Disable,
                UpdatedBy = entity.UpdatedBy,
                UpdatedOn = entity.UpdatedOn
            };
            billTypesRepo.Add(bt);
            return true;
        }

        public Task<bool> InsertAsync(BILL_TYPESVM entity)
        {
            return Task.Run<bool>(() =>
            {
                BILL_TYPES bt = new BILL_TYPES
                {
                    AddedBy = entity.AddedBy,
                    AddedOn = entity.AddedOn,
                    BILL_EFF_ID = entity.BILL_EFF_ID,
                    BILL_TYPE_AR_NAME = entity.BILL_TYPE_AR_NAME,
                    BILL_TYPE_EN_NAME = entity.BILL_TYPE_EN_NAME,
                    BILL_TYPE_ID = entity.BILL_TYPE_ID,
                    Disable = entity.Disable,
                    UpdatedBy = entity.UpdatedBy,
                    UpdatedOn = entity.UpdatedOn
                };
                billTypesRepo.Add(bt);
                return true;
            });
        }

        public bool Update(BILL_TYPESVM entity)
        {
            BILL_TYPES bt = new BILL_TYPES
            {
                AddedBy = entity.AddedBy,
                AddedOn = entity.AddedOn,
                BILL_EFF_ID = entity.BILL_EFF_ID,
                BILL_TYPE_AR_NAME = entity.BILL_TYPE_AR_NAME,
                BILL_TYPE_EN_NAME = entity.BILL_TYPE_EN_NAME,
                BILL_TYPE_ID = entity.BILL_TYPE_ID,
                Disable = entity.Disable,
                UpdatedBy = entity.UpdatedBy,
                UpdatedOn = entity.UpdatedOn
            };
            billTypesRepo.Update(bt, bt.BILL_TYPE_ID);
            return true;
        }

        public Task<bool> UpdateAsync(BILL_TYPESVM entity)
        {
            return Task.Run<bool>(() =>
            {
                BILL_TYPES bt = new BILL_TYPES
                {
                    AddedBy = entity.AddedBy,
                    AddedOn = entity.AddedOn,
                    BILL_EFF_ID = entity.BILL_EFF_ID,
                    BILL_TYPE_AR_NAME = entity.BILL_TYPE_AR_NAME,
                    BILL_TYPE_EN_NAME = entity.BILL_TYPE_EN_NAME,
                    BILL_TYPE_ID = entity.BILL_TYPE_ID,
                    Disable = entity.Disable,
                    UpdatedBy = entity.UpdatedBy,
                    UpdatedOn = entity.UpdatedOn
                };
                billTypesRepo.Update(bt, bt.BILL_TYPE_ID);
                return true;
            });
        }
    }
}
