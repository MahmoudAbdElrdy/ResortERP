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
    public class ManufacturingWagesService : IManufacturingWagesService, IDisposable
    {
        IGenericRepository<ManufacturingWages> manufactRepo;
        public ManufacturingWagesService(IGenericRepository<ManufacturingWages> manufactRepo)
        {
            this.manufactRepo = manufactRepo;
        }

        public void Dispose()
        {
            manufactRepo.Dispose();
        }


        public bool Insert(ManufacturingWagesVM entity)
        {
            ManufacturingWages ig = new ManufacturingWages
            {
                ManufacturingWage_AR_NAME = entity.ManufacturingWage_AR_NAME,
                ManufacturingWage_CODE = entity.ManufacturingWage_CODE,
                ManufacturingWage_EN_NAME = entity.ManufacturingWage_EN_NAME,
                ManufacturingWage_ID = entity.ManufacturingWage_ID,
                ManufacturingWage_REMARKS = entity.ManufacturingWage_REMARKS,
                AddedBy = entity.AddedBy,
                AddedOn = entity.AddedOn,
                Disable = entity.Disable,
                UpdatedBy = entity.UpdatedBy,
                UpdatedOn = entity.UpdatedOn
            };
            manufactRepo.Add(ig);
            return true;
        }

        public Task<int> InsertAsync(ManufacturingWagesVM entity)
        {
            return Task.Run<int>(() =>
            {
                ManufacturingWages ig = new ManufacturingWages
                {
                    ManufacturingWage_AR_NAME = entity.ManufacturingWage_AR_NAME,
                    ManufacturingWage_CODE = entity.ManufacturingWage_CODE,
                    ManufacturingWage_EN_NAME = entity.ManufacturingWage_EN_NAME,
                    ManufacturingWage_ID = entity.ManufacturingWage_ID,
                    ManufacturingWage_REMARKS = entity.ManufacturingWage_REMARKS,
                    AddedBy = entity.AddedBy,
                    AddedOn = entity.AddedOn,
                    Disable = entity.Disable,
                    UpdatedBy = entity.UpdatedBy,
                    UpdatedOn = entity.UpdatedOn
                };
                manufactRepo.Add(ig);
                return ig.ManufacturingWage_ID;
            });
        }

        public bool Update(ManufacturingWagesVM entity)
        {
            ManufacturingWages ig = new ManufacturingWages
            {
                ManufacturingWage_AR_NAME = entity.ManufacturingWage_AR_NAME,
                ManufacturingWage_CODE = entity.ManufacturingWage_CODE,
                ManufacturingWage_EN_NAME = entity.ManufacturingWage_EN_NAME,
                ManufacturingWage_ID = entity.ManufacturingWage_ID,
                ManufacturingWage_REMARKS = entity.ManufacturingWage_REMARKS,
                AddedBy = entity.AddedBy,
                AddedOn = entity.AddedOn,
                Disable = entity.Disable,
                UpdatedBy = entity.UpdatedBy,
                UpdatedOn = entity.UpdatedOn
            };
            manufactRepo.Update(ig, ig.ManufacturingWage_ID);
            return true;
        }
        public Task<bool> UpdateAsync(ManufacturingWagesVM entity)
        {
            return Task.Run<bool>(() =>
            {
                ManufacturingWages ig = new ManufacturingWages
                {
                    ManufacturingWage_AR_NAME = entity.ManufacturingWage_AR_NAME,
                    ManufacturingWage_CODE = entity.ManufacturingWage_CODE,
                    ManufacturingWage_EN_NAME = entity.ManufacturingWage_EN_NAME,
                    ManufacturingWage_ID = entity.ManufacturingWage_ID,
                    ManufacturingWage_REMARKS = entity.ManufacturingWage_REMARKS,
                    AddedBy = entity.AddedBy,
                    AddedOn = entity.AddedOn,
                    Disable = entity.Disable,
                    UpdatedBy = entity.UpdatedBy,
                    UpdatedOn = entity.UpdatedOn
                };
                manufactRepo.Update(ig, ig.ManufacturingWage_ID);
                return true;
            });
        }

        public bool Delete(ManufacturingWagesVM entity)
        {
            ManufacturingWages ig = new ManufacturingWages
            {
                ManufacturingWage_AR_NAME = entity.ManufacturingWage_AR_NAME,
                ManufacturingWage_CODE = entity.ManufacturingWage_CODE,
                ManufacturingWage_EN_NAME = entity.ManufacturingWage_EN_NAME,
                ManufacturingWage_ID = entity.ManufacturingWage_ID,
                ManufacturingWage_REMARKS = entity.ManufacturingWage_REMARKS,
                AddedBy = entity.AddedBy,
                AddedOn = entity.AddedOn,
                Disable = entity.Disable,
                UpdatedBy = entity.UpdatedBy,
                UpdatedOn = entity.UpdatedOn
            };
            manufactRepo.Delete(ig, entity.ManufacturingWage_ID);
            return true;
        }

        public Task<bool> DeleteAsync(ManufacturingWagesVM entity)
        {
            return Task.Run<bool>(() =>
            {
                ManufacturingWages ig = new ManufacturingWages
                {
                    ManufacturingWage_AR_NAME = entity.ManufacturingWage_AR_NAME,
                    ManufacturingWage_CODE = entity.ManufacturingWage_CODE,
                    ManufacturingWage_EN_NAME = entity.ManufacturingWage_EN_NAME,
                    ManufacturingWage_ID = entity.ManufacturingWage_ID,
                    ManufacturingWage_REMARKS = entity.ManufacturingWage_REMARKS,
                    AddedBy = entity.AddedBy,
                    AddedOn = entity.AddedOn,
                    Disable = entity.Disable,
                    UpdatedBy = entity.UpdatedBy,
                    UpdatedOn = entity.UpdatedOn
                };
                manufactRepo.Delete(ig, entity.ManufacturingWage_ID);
                return true;
            });
        }

        public Task<List<ManufacturingWagesVM>> GetAllAsync(int pageNum, int pageSize)
        {
            return Task.Run<List<ManufacturingWagesVM>>(() =>
            {
                int rowCount;
                var q = from p in manufactRepo.GetPaged<int>(pageNum, pageSize, p => p.ManufacturingWage_ID, false, out rowCount)
                        select new ManufacturingWagesVM
                        {
                            ManufacturingWage_AR_NAME = p.ManufacturingWage_AR_NAME,
                            ManufacturingWage_CODE = p.ManufacturingWage_CODE,
                            ManufacturingWage_EN_NAME = p.ManufacturingWage_EN_NAME,
                            ManufacturingWage_ID = p.ManufacturingWage_ID,
                            ManufacturingWage_REMARKS = p.ManufacturingWage_REMARKS,
                            AddedBy = p.AddedBy,
                            AddedOn = p.AddedOn,
                            Disable = p.Disable,
                            UpdatedBy = p.UpdatedBy,
                            UpdatedOn = p.UpdatedOn
                        };
                return q.ToList();
            });
        }

        public Task<ManufacturingWagesVM> getById(int ID)
        {
            return Task.Run<ManufacturingWagesVM>(() =>
            {
                var q = from p in manufactRepo.GetAll().Where(a=>a.ManufacturingWage_ID==ID)
                        select new ManufacturingWagesVM
                        {
                            ManufacturingWage_AR_NAME = p.ManufacturingWage_AR_NAME,
                            ManufacturingWage_CODE = p.ManufacturingWage_CODE,
                            ManufacturingWage_EN_NAME = p.ManufacturingWage_EN_NAME,
                            ManufacturingWage_ID = p.ManufacturingWage_ID,
                            ManufacturingWage_REMARKS = p.ManufacturingWage_REMARKS,
                            AddedBy = p.AddedBy,
                            AddedOn = p.AddedOn,
                            Disable = p.Disable,
                            UpdatedBy = p.UpdatedBy,
                            UpdatedOn = p.UpdatedOn
                        };
                return q.FirstOrDefault();
            });
        }

        public Task<int> CountAsync()
        {
            return Task.Run<int>(() =>
            {
                return manufactRepo.CountAsync();
            });
        }

        //public CustomItemManufacturingWagesVM GetUnitDataUsingUnitCode(string UnitCode)
        //{
        //    var q = from p in manufactRepo.GetAll().Where(x => x.ManufacturingWage_CODE == UnitCode)

        //            select new CustomItemManufacturingWagesVM
        //            {
        //                ManufacturingWage_CODE = p.ManufacturingWage_CODE,
        //                ManufacturingWage_ID = p.ManufacturingWage_ID,
        //                ManufacturingWage_AR_NAME = p.ManufacturingWage_AR_NAME,
        //                Operation = "Insert"
        //            };
        //    return q.FirstOrDefault();
        //}


        public string GetLastCode()
        {
            var lastCode = manufactRepo.GetAll().OrderByDescending(m => m.ManufacturingWage_ID).FirstOrDefault();
            return lastCode.ManufacturingWage_CODE;
        }

    }
}
