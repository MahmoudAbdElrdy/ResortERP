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
    public class BillEffectsService : IBillEffectsService, IDisposable
    {
        IGenericRepository<BILL_EFFECTS> billEffectsRepo;
        public BillEffectsService(IGenericRepository<BILL_EFFECTS> billEffectsRepo)
        {
            this.billEffectsRepo = billEffectsRepo;
        }

        public void Dispose()
        {
            billEffectsRepo.Dispose();
        }


        public bool Insert(BILL_EFFECTSVM entity)
        {
            BILL_EFFECTS be = new BILL_EFFECTS
            {
                AddedBy = entity.AddedBy,
                AddedOn = entity.AddedOn,
                BILL_EFF_AR_NAME = entity.BILL_EFF_AR_NAME,
                BILL_EFF_EN_NAME = entity.BILL_EFF_EN_NAME,
                BILL_EFF_ID = entity.BILL_EFF_ID,
                Disable = entity.Disable,
                UpdatedBy = entity.UpdatedBy,
                updatedOn = entity.updatedOn
            };
            billEffectsRepo.Add(be);
            return true;
        }

        public Task<bool> InsertAsync(BILL_EFFECTSVM entity)
        {
            return Task.Run<bool>(() =>
            {
                BILL_EFFECTS be = new BILL_EFFECTS
                {
                    AddedBy = entity.AddedBy,
                    AddedOn = entity.AddedOn,
                    BILL_EFF_AR_NAME = entity.BILL_EFF_AR_NAME,
                    BILL_EFF_EN_NAME = entity.BILL_EFF_EN_NAME,
                    BILL_EFF_ID = entity.BILL_EFF_ID,
                    Disable = entity.Disable,
                    UpdatedBy = entity.UpdatedBy,
                    updatedOn = entity.updatedOn
                };
                billEffectsRepo.Add(be);
                return true;
            }
              );

        }

        public bool Update(BILL_EFFECTSVM entity)
        {

            BILL_EFFECTS be = new BILL_EFFECTS
            {
                AddedBy = entity.AddedBy,
                AddedOn = entity.AddedOn,
                BILL_EFF_AR_NAME = entity.BILL_EFF_AR_NAME,
                BILL_EFF_EN_NAME = entity.BILL_EFF_EN_NAME,
                BILL_EFF_ID = entity.BILL_EFF_ID,
                Disable = entity.Disable,
                UpdatedBy = entity.UpdatedBy,
                updatedOn = entity.updatedOn
            };
            billEffectsRepo.Update(be, be.BILL_EFF_ID);
            return true;


        }
        public Task<bool> UpdateAsync(BILL_EFFECTSVM entity)
        {
            return Task.Run<bool>(() =>
            {
                BILL_EFFECTS be = new BILL_EFFECTS
                {
                    AddedBy = entity.AddedBy,
                    AddedOn = entity.AddedOn,
                    BILL_EFF_AR_NAME = entity.BILL_EFF_AR_NAME,
                    BILL_EFF_EN_NAME = entity.BILL_EFF_EN_NAME,
                    BILL_EFF_ID = entity.BILL_EFF_ID,
                    Disable = entity.Disable,
                    UpdatedBy = entity.UpdatedBy,
                    updatedOn = entity.updatedOn
                };
                billEffectsRepo.Update(be, be.BILL_EFF_ID);
                return true;
            }
              );

        }

        public bool Delete(BILL_EFFECTSVM entity)
        {
            BILL_EFFECTS be = new BILL_EFFECTS
            {
                AddedBy = entity.AddedBy,
                AddedOn = entity.AddedOn,
                BILL_EFF_AR_NAME = entity.BILL_EFF_AR_NAME,
                BILL_EFF_EN_NAME = entity.BILL_EFF_EN_NAME,
                BILL_EFF_ID = entity.BILL_EFF_ID,
                Disable = entity.Disable,
                UpdatedBy = entity.UpdatedBy,
                updatedOn = entity.updatedOn
            };
            billEffectsRepo.Delete(be, entity.BILL_EFF_ID);
            return true;


        }
        public Task<bool> DeleteAsync(BILL_EFFECTSVM entity)
        {
            return Task.Run<bool>(() =>
            {
                BILL_EFFECTS be = new BILL_EFFECTS
                {
                    AddedBy = entity.AddedBy,
                    AddedOn = entity.AddedOn,
                    BILL_EFF_AR_NAME = entity.BILL_EFF_AR_NAME,
                    BILL_EFF_EN_NAME = entity.BILL_EFF_EN_NAME,
                    BILL_EFF_ID = entity.BILL_EFF_ID,
                    Disable = entity.Disable,
                    UpdatedBy = entity.UpdatedBy,
                    updatedOn = entity.updatedOn
                };
                billEffectsRepo.Delete(be, entity.BILL_EFF_ID);
                return true;
            });
        }

        public Task<List<BILL_EFFECTSVM>> GetAllAsync(int pageNum, int pageSize)
        {

            return Task.Run<List<BILL_EFFECTSVM>>(() =>
            {
                int rowCount;
                var q = from entity in billEffectsRepo.GetPaged<int>(pageNum, pageSize, p => p.BILL_EFF_ID, false, out rowCount)
                        select new BILL_EFFECTSVM
                        {
                            AddedBy = entity.AddedBy,
                            AddedOn = entity.AddedOn,
                            BILL_EFF_AR_NAME = entity.BILL_EFF_AR_NAME,
                            BILL_EFF_EN_NAME = entity.BILL_EFF_EN_NAME,
                            BILL_EFF_ID = entity.BILL_EFF_ID,
                            Disable = entity.Disable,
                            UpdatedBy = entity.UpdatedBy,
                            updatedOn = entity.updatedOn
                        };
                return q.ToList();
            });
        }

        public List<BILL_EFFECTSVM> GetAll()
        {
            var q = from entity in billEffectsRepo.GetAll()
                    select new BILL_EFFECTSVM
                    {
                        AddedBy = entity.AddedBy,
                        AddedOn = entity.AddedOn,
                        BILL_EFF_AR_NAME = entity.BILL_EFF_AR_NAME,
                        BILL_EFF_EN_NAME = entity.BILL_EFF_EN_NAME,
                        BILL_EFF_ID = entity.BILL_EFF_ID,
                        Disable = entity.Disable,
                        UpdatedBy = entity.UpdatedBy,
                        updatedOn = entity.updatedOn
                    };
            return q.ToList();
        }
        public Task<int> CountAsync()
        {
            return Task.Run<int>(() =>
            {
                return billEffectsRepo.CountAsync();
            });
        }
    }
}
