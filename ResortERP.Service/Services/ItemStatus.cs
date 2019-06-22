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
    public class ItemStatusService : IItemStatusService, IDisposable
    {
        IGenericRepository<ItemStatus> itemStatusRepo;
        public ItemStatusService(IGenericRepository<ItemStatus> itemStatusRepo)
        {
            this.itemStatusRepo = itemStatusRepo;
        }

        public void Dispose()
        {
            itemStatusRepo.Dispose();
        }


        public bool Insert(ItemStatusVM entity)
        {
            ItemStatus ig = new ItemStatus
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
            itemStatusRepo.Add(ig);
            return true;
        }

        public Task<int> InsertAsync(ItemStatusVM entity)
        {
            return Task.Run<int>(() =>
            {
                ItemStatus ig = new ItemStatus
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
                itemStatusRepo.Add(ig);
                return ig.ID;
            });
        }

        public bool Update(ItemStatusVM entity)
        {
            ItemStatus ig = new ItemStatus
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
            itemStatusRepo.Update(ig, ig.ID);
            return true;
        }
        public Task<bool> UpdateAsync(ItemStatusVM entity)
        {
            return Task.Run<bool>(() =>
            {
                ItemStatus ig = new ItemStatus
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
                itemStatusRepo.Update(ig, ig.ID);
                return true;
            });
        }

        public bool Delete(ItemStatusVM entity)
        {
            ItemStatus ig = new ItemStatus
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
            itemStatusRepo.Delete(ig, entity.ID);
            return true;
        }

        public Task<bool> DeleteAsync(ItemStatusVM entity)
        {
            return Task.Run<bool>(() =>
            {
                ItemStatus ig = new ItemStatus
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
                itemStatusRepo.Delete(ig, entity.ID);
                return true;
            });
        }

        public Task<List<ItemStatusVM>> GetAllAsync(int pageNum, int pageSize)
        {
            return Task.Run<List<ItemStatusVM>>(() =>
            {
                int rowCount;
                var q = from p in itemStatusRepo.GetPaged<int>(pageNum, pageSize, p => p.ID, false, out rowCount)
                        select new ItemStatusVM
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
        public Task<ItemStatusVM> getById(int ID)
        {
            return Task.Run<ItemStatusVM>(() =>
            {
                var q = from p in itemStatusRepo.GetAll().Where(a=>a.ID==ID)
                        select new ItemStatusVM
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
                return q.FirstOrDefault();
            });
        }
        public Task<int> CountAsync()
        {
            return Task.Run<int>(() =>
            {
                return itemStatusRepo.CountAsync();
            });
        }

        public CustomItemUnitsVM GetUnitDataUsingUnitCode(string UnitCode)
        {
            var q = from p in itemStatusRepo.GetAll().Where(x => x.Code == UnitCode)

                    select new CustomItemUnitsVM
                    {
                        UNIT_CODE = p.Code,
                        UNIT_ID = p.ID,
                        UNIT_AR_NAME = p.ARName,
                        Operation = "Insert"
                    };
            return q.FirstOrDefault();
        }

        public Task<List<ItemStatusVM>> GetAllgetItemStatus()
        {
            return Task.Run<List<ItemStatusVM>>(() =>
            {
                int rowCount;
                var q = from p in itemStatusRepo.GetAll().ToList()
                        select new ItemStatusVM
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


        public string GetLastCode()
        {
            var lastCode = itemStatusRepo.GetAll().OrderByDescending(i => i.ID).FirstOrDefault();
            return lastCode.Code;
        }
    }
}
