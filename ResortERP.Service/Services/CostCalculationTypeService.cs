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
    public class CostCalculationTypeService : ICostCalculationTypeService, IDisposable
    {
        IGenericRepository<CostCalculationType> costCalculationTypeRepo;
        public CostCalculationTypeService(IGenericRepository<CostCalculationType> costCalculationTypeRepo)
        {
            this.costCalculationTypeRepo = costCalculationTypeRepo;
        }

        public void Dispose()
        {
            costCalculationTypeRepo.Dispose();
        }


        public bool Insert(CostCalculationTypeVM entity)
        {
            CostCalculationType ig = new CostCalculationType
            {
                ARName = entity.ARName,
                Code = entity.Code,
                LatName = entity.LatName,
                ID = entity.ID,
                Notes = entity.Notes,
                AddedBy = entity.AddedBy,
                AddedOn = entity.AddedOn,
                Disable = entity.Disable,
                UpdatedBy = entity.UpdatedBy,
                UpdatedOn = entity.UpdatedOn
            };
            costCalculationTypeRepo.Add(ig);
            return true;
        }

        public Task<bool> InsertAsync(CostCalculationTypeVM entity)
        {
            return Task.Run<bool>(() =>
            {
                CostCalculationType ig = new CostCalculationType
                {
                    ARName = entity.ARName,
                    Code = entity.Code,
                    LatName = entity.LatName,
                    ID = entity.ID,
                    Notes = entity.Notes,
                    AddedBy = entity.AddedBy,
                    AddedOn = entity.AddedOn,
                    Disable = entity.Disable,
                    UpdatedBy = entity.UpdatedBy,
                    UpdatedOn = entity.UpdatedOn
                };
                costCalculationTypeRepo.Add(ig);
                return true;
            });
        }

        public bool Update(CostCalculationTypeVM entity)
        {
            CostCalculationType ig = new CostCalculationType
            {
                ARName = entity.ARName,
                Code = entity.Code,
                LatName = entity.LatName,
                ID = entity.ID,
                Notes = entity.Notes,
                AddedBy = entity.AddedBy,
                AddedOn = entity.AddedOn,
                Disable = entity.Disable,
                UpdatedBy = entity.UpdatedBy,
                UpdatedOn = entity.UpdatedOn
            };
            costCalculationTypeRepo.Update(ig, ig.ID);
            return true;
        }
        public Task<bool> UpdateAsync(CostCalculationTypeVM entity)
        {
            return Task.Run<bool>(() =>
            {
                CostCalculationType ig = new CostCalculationType
                {
                    ARName = entity.ARName,
                    Code = entity.Code,
                    LatName = entity.LatName,
                    ID = entity.ID,
                    Notes = entity.Notes,
                    AddedBy = entity.AddedBy,
                    AddedOn = entity.AddedOn,
                    Disable = entity.Disable,
                    UpdatedBy = entity.UpdatedBy,
                    UpdatedOn = entity.UpdatedOn
                };
                costCalculationTypeRepo.Update(ig, ig.ID);
                return true;
            });
        }

        public bool Delete(CostCalculationTypeVM entity)
        {
            CostCalculationType ig = new CostCalculationType
            {
                ARName = entity.ARName,
                Code = entity.Code,
                LatName = entity.LatName,
                ID = entity.ID,
                Notes = entity.Notes,
                AddedBy = entity.AddedBy,
                AddedOn = entity.AddedOn,
                Disable = entity.Disable,
                UpdatedBy = entity.UpdatedBy,
                UpdatedOn = entity.UpdatedOn
            };
            costCalculationTypeRepo.Delete(ig, entity.ID);
            return true;
        }

        public Task<bool> DeleteAsync(CostCalculationTypeVM entity)
        {
            return Task.Run<bool>(() =>
            {
                CostCalculationType ig = new CostCalculationType
                {
                    ARName = entity.ARName,
                    Code = entity.Code,
                    LatName = entity.LatName,
                    ID = entity.ID,
                    Notes = entity.Notes,
                    AddedBy = entity.AddedBy,
                    AddedOn = entity.AddedOn,
                    Disable = entity.Disable,
                    UpdatedBy = entity.UpdatedBy,
                    UpdatedOn = entity.UpdatedOn
                };
                costCalculationTypeRepo.Delete(ig, entity.ID);
                return true;
            });
        }

        public Task<List<CostCalculationTypeVM>> GetAllAsync(int pageNum, int pageSize)
        {
            return Task.Run<List<CostCalculationTypeVM>>(() =>
            {
                int rowCount;
                var q = from p in costCalculationTypeRepo.GetPaged<int>(pageNum, pageSize, p => p.ID, false, out rowCount)
                        select new CostCalculationTypeVM
                        {
                            ARName = p.ARName,
                            Code = p.Code,
                            LatName = p.LatName,
                            ID = p.ID,
                            Notes = p.Notes,
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
                return costCalculationTypeRepo.CountAsync();
            });
        }

        public CustomItemUnitsVM GetUnitDataUsingUnitCode(string UnitCode)
        {
            var q = from p in costCalculationTypeRepo.GetAll().Where(x => x.Code == UnitCode)

                    select new CustomItemUnitsVM
                    {
                        UNIT_CODE = p.Code,
                        UNIT_ID = p.ID,
                        UNIT_AR_NAME = p.ARName,
                        Operation = "Insert"
                    };
            return q.FirstOrDefault();
        }

        public Task<List<CostCalculationTypeVM>> GetAllgetCostCalculationType()
        {
            return Task.Run<List<CostCalculationTypeVM>>(() =>
            {
                int rowCount;
                var q = from p in costCalculationTypeRepo.GetAll().ToList()
                        select new CostCalculationTypeVM
                        {
                            ARName = p.ARName,
                            Code = p.Code,
                            LatName = p.LatName,
                            ID = p.ID,
                            Notes = p.Notes,
                            AddedBy = p.AddedBy,
                            AddedOn = p.AddedOn,
                            Disable = p.Disable,
                            UpdatedBy = p.UpdatedBy,
                            UpdatedOn = p.UpdatedOn
                        };
                return q.ToList();
            });
        }
    }
}
