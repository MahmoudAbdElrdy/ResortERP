using ResortERP.Core;
using ResortERP.Core.VM;
using ResortERP.Repository;
using ResortERP.Service.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortERP.Service.Services
{
    public class UnitsService : IUnitsService, IDisposable
    {
        IGenericRepository<UNITS> unitsRepo;
        public UnitsService(IGenericRepository<UNITS> unitsRepo)
        {
            this.unitsRepo = unitsRepo;
        }

        public void Dispose()
        {
            unitsRepo.Dispose();
        }


        public bool Insert(UNITSVM entity)
        {
            UNITS ig = new UNITS
            {
                UNIT_AR_NAME = entity.UNIT_AR_NAME,
                UNIT_CODE = entity.UNIT_CODE,
                UNIT_EN_NAME = entity.UNIT_EN_NAME,
                UNIT_ID = entity.UNIT_ID,
                UNIT_REMARKS = entity.UNIT_REMARKS,
                AddedBy = entity.AddedBy,
                AddedOn = entity.AddedOn,
                Disable = entity.Disable,
                UpdatedBy = entity.UpdatedBy,
                UpdatedOn = entity.UpdatedOn
            };
            unitsRepo.Add(ig);
            return true;
        }

        public Task<int> InsertAsync(UNITSVM entity)
        {
            return Task.Run<int>(() =>
            {
                UNITS ig = new UNITS
                {
                    UNIT_AR_NAME = entity.UNIT_AR_NAME,
                    UNIT_CODE = entity.UNIT_CODE,
                    UNIT_EN_NAME = entity.UNIT_EN_NAME,
                    UNIT_ID = entity.UNIT_ID,
                    UNIT_REMARKS = entity.UNIT_REMARKS,
                    AddedBy = entity.AddedBy,
                    AddedOn = entity.AddedOn,
                    Disable = entity.Disable,
                    UpdatedBy = entity.UpdatedBy,
                    UpdatedOn = entity.UpdatedOn
                };
                unitsRepo.Add(ig);
                return ig.UNIT_ID;
            });
        }

        public bool Update(UNITSVM entity)
        {
            UNITS ig = new UNITS
            {
                UNIT_AR_NAME = entity.UNIT_AR_NAME,
                UNIT_CODE = entity.UNIT_CODE,
                UNIT_EN_NAME = entity.UNIT_EN_NAME,
                UNIT_ID = entity.UNIT_ID,
                UNIT_REMARKS = entity.UNIT_REMARKS,
                AddedBy = entity.AddedBy,
                AddedOn = entity.AddedOn,
                Disable = entity.Disable,
                UpdatedBy = entity.UpdatedBy,
                UpdatedOn = entity.UpdatedOn
            };
            unitsRepo.Update(ig, ig.UNIT_ID);
            return true;
        }
        public Task<bool> UpdateAsync(UNITSVM entity)
        {
            return Task.Run<bool>(() =>
            {
                UNITS ig = new UNITS
                {
                    UNIT_AR_NAME = entity.UNIT_AR_NAME,
                    UNIT_CODE = entity.UNIT_CODE,
                    UNIT_EN_NAME = entity.UNIT_EN_NAME,
                    UNIT_ID = entity.UNIT_ID,
                    UNIT_REMARKS = entity.UNIT_REMARKS,
                    AddedBy = entity.AddedBy,
                    AddedOn = entity.AddedOn,
                    Disable = entity.Disable,
                    UpdatedBy = entity.UpdatedBy,
                    UpdatedOn = entity.UpdatedOn
                };
                unitsRepo.Update(ig, ig.UNIT_ID);
                return true;
            });
        }

        public bool Delete(UNITSVM entity)
        {
            UNITS ig = new UNITS
            {
                UNIT_AR_NAME = entity.UNIT_AR_NAME,
                UNIT_CODE = entity.UNIT_CODE,
                UNIT_EN_NAME = entity.UNIT_EN_NAME,
                UNIT_ID = entity.UNIT_ID,
                UNIT_REMARKS = entity.UNIT_REMARKS,
                AddedBy = entity.AddedBy,
                AddedOn = entity.AddedOn,
                Disable = entity.Disable,
                UpdatedBy = entity.UpdatedBy,
                UpdatedOn = entity.UpdatedOn
            };
            unitsRepo.Delete(ig, entity.UNIT_ID);
            return true;
        }

        public Task<bool> DeleteAsync(UNITSVM entity)
        {
            return Task.Run<bool>(() =>
            {
                UNITS ig = new UNITS
                {
                    UNIT_AR_NAME = entity.UNIT_AR_NAME,
                    UNIT_CODE = entity.UNIT_CODE,
                    UNIT_EN_NAME = entity.UNIT_EN_NAME,
                    UNIT_ID = entity.UNIT_ID,
                    UNIT_REMARKS = entity.UNIT_REMARKS,
                    AddedBy = entity.AddedBy,
                    AddedOn = entity.AddedOn,
                    Disable = entity.Disable,
                    UpdatedBy = entity.UpdatedBy,
                    UpdatedOn = entity.UpdatedOn
                };
                unitsRepo.Delete(ig, entity.UNIT_ID);
                return true;
            });
        }

        
        public Task<UNITSVM> getById(int id)
        {
            return Task.Run<UNITSVM>(() =>
            {
                var q = from p in unitsRepo.GetAll().Where(a=>a.UNIT_ID==id)
                        select new UNITSVM
                        {
                            UNIT_AR_NAME = p.UNIT_AR_NAME,
                            UNIT_CODE = p.UNIT_CODE,
                            UNIT_EN_NAME = p.UNIT_EN_NAME,
                            UNIT_ID = p.UNIT_ID,
                            UNIT_REMARKS = p.UNIT_REMARKS,
                            AddedBy = p.AddedBy,
                            AddedOn = p.AddedOn,
                            Disable = p.Disable,
                            UpdatedBy = p.UpdatedBy,
                            UpdatedOn = p.UpdatedOn
                        };
                return q.FirstOrDefault();
            });
        }
        public Task<List<UNITSVM>> GetAllAsync(int pageNum, int pageSize)
        {
            return Task.Run<List<UNITSVM>>(() =>
            {
                int rowCount;
                var q = from p in unitsRepo.GetPaged<int>(pageNum, pageSize, p => p.UNIT_ID, false, out rowCount)
                        select new UNITSVM
                        {
                            UNIT_AR_NAME = p.UNIT_AR_NAME,
                            UNIT_CODE = p.UNIT_CODE,
                            UNIT_EN_NAME = p.UNIT_EN_NAME,
                            UNIT_ID = p.UNIT_ID,
                            UNIT_REMARKS = p.UNIT_REMARKS,
                            AddedBy = p.AddedBy,
                            AddedOn = p.AddedOn,
                            Disable = p.Disable,
                            UpdatedBy = p.UpdatedBy,
                            UpdatedOn = p.UpdatedOn
                        };
                return q.ToList();
            });
        }

        public Task<int> CountAsync()
        {
            return Task.Run<int>(() =>
            {
                return unitsRepo.CountAsync();
            });
        }

        public CustomItemUnitsVM GetUnitDataUsingUnitCode(string UnitCode)
        {
            var q = from p in unitsRepo.GetAll().Where(X => (X.UNIT_AR_NAME.ToLower().Contains(UnitCode) || X.UNIT_EN_NAME.ToLower().Contains(UnitCode) || X.UNIT_CODE.ToLower().Contains(UnitCode)))
                    select new CustomItemUnitsVM
                    {
                        UNIT_CODE = p.UNIT_CODE,
                        UNIT_ID = p.UNIT_ID,
                        UNIT_AR_NAME = p.UNIT_AR_NAME,
                        UNIT_TRANS_RATE = 1,
                        Operation = "Insert"
                    };
            return q.FirstOrDefault();
        }


        public string GetLastCode()
        {
            var Code = unitsRepo.GetAll().OrderByDescending(u => u.UNIT_ID).FirstOrDefault();
            //var x = Code.OrderByDescending(u => u.UNIT_ID).ToList();
            //   var y =x.FirstOrDefault() ;
            string lastCode = Code.UNIT_CODE;
            return lastCode;
        }
    }
}
