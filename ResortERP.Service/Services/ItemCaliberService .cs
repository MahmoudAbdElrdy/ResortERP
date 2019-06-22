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
    public class ItemCaliberService : IItemCaliberService, IDisposable
    {
        IGenericRepository<ItemCaliber> itemCaliberRepo;
        public ItemCaliberService(IGenericRepository<ItemCaliber> itemCaliberRepo)
        {
            this.itemCaliberRepo = itemCaliberRepo;
        }

        public void Dispose()
        {
            itemCaliberRepo.Dispose();
        }


        public bool Insert(ItemCaliberVM entity)
        {
            ItemCaliber ig = new ItemCaliber
            {
                ID = entity.ID,
                ARName = entity.ARName,
                Code = entity.Code,
                LatName = entity.LatName,
                ClearnessRate = entity.ClearnessRate,
                CaliberNumber = entity.CaliberNumber,
                Notes = entity.Notes
            };
            itemCaliberRepo.Add(ig);
            return true;
        }

        public Task<int> InsertAsync(ItemCaliberVM entity)
        {
            return Task.Run<int>(() =>
            {
                ItemCaliber ig = new ItemCaliber
                {
                    ID = entity.ID,
                    ARName = entity.ARName,
                    Code = entity.Code,
                    LatName = entity.LatName,
                    ClearnessRate = entity.ClearnessRate,
                    CaliberNumber = entity.CaliberNumber,
                    Notes = entity.Notes
                };
                itemCaliberRepo.Add(ig);
                return ig.ID;
            }
              );

        }

        public bool Update(ItemCaliberVM entity)
        {

            ItemCaliber ig = new ItemCaliber
            {
                ID = entity.ID,
                ARName = entity.ARName,
                Code = entity.Code,
                LatName = entity.LatName,
                ClearnessRate = entity.ClearnessRate,
                CaliberNumber = entity.CaliberNumber,
                Notes = entity.Notes
            };
            itemCaliberRepo.Update(ig, ig.ID);
            return true;


        }
        public Task<bool> UpdateAsync(ItemCaliberVM entity)
        {
            return Task.Run<bool>(() =>
            {
                ItemCaliber ig = new ItemCaliber
                {
                    ID = entity.ID,
                    ARName = entity.ARName,
                    Code = entity.Code,
                    LatName = entity.LatName,
                    ClearnessRate = entity.ClearnessRate,
                    CaliberNumber = entity.CaliberNumber,
                    Notes = entity.Notes
                };
                itemCaliberRepo.Update(ig, ig.ID);
                return true;
            }
              );

        }

        public bool Delete(ItemCaliberVM entity)
        {
            ItemCaliber ig = new ItemCaliber
            {
                ID = entity.ID,
                ARName = entity.ARName,
                Code = entity.Code,
                LatName = entity.LatName,
                ClearnessRate = entity.ClearnessRate,
                CaliberNumber = entity.CaliberNumber,
                Notes = entity.Notes
            };
            itemCaliberRepo.Delete(ig, entity.ID);
            return true;


        }
        public Task<bool> DeleteAsync(ItemCaliberVM entity)
        {
            return Task.Run<bool>(() =>
            {
                ItemCaliber ig = new ItemCaliber
                {
                    ID = entity.ID,
                    ARName = entity.ARName,
                    Code = entity.Code,
                    LatName = entity.LatName,
                    ClearnessRate = entity.ClearnessRate,
                    CaliberNumber = entity.CaliberNumber,
                    Notes = entity.Notes
                };
                itemCaliberRepo.Delete(ig, entity.ID);
                return true;
            });
        }

        public Task<List<ItemCaliberVM>> GetAllAsync(int pageNum, int pageSize)
        {

            return Task.Run<List<ItemCaliberVM>>(() =>
            {

                int rowCount;
                var q = from p in itemCaliberRepo.GetPaged<int>(pageNum, pageSize, p => p.ID, false, out rowCount)
                  
                        select new ItemCaliberVM
                        {
                            ID = p.ID,
                            ARName = p.ARName,
                            Code = p.Code,
                            LatName = p.LatName,
                            ClearnessRate = p.ClearnessRate,
                            CaliberNumber = p.CaliberNumber,
                            Notes = p.Notes
                        };



                return q.ToList();




                // return new List<ItemCaliberVM>();
            });


        }

        public List<ItemCaliberVM> getAllItemCalibers()
        {
            var q = from p in itemCaliberRepo.GetAll()
                    select new ItemCaliberVM
                    {
                        ID = p.ID,
                        ARName = p.ARName,
                        Code = p.Code,
                        LatName = p.LatName,
                        ClearnessRate = p.ClearnessRate,
                        CaliberNumber = p.CaliberNumber,
                        Notes = p.Notes
                    };
            return q.ToList();
        }

        public List<ItemCaliberVM> GetAllItemGroupsPos()
        {
            var q = from p in itemCaliberRepo.GetAll().Where(x => x.Active == 1)
                    select new ItemCaliberVM
                    {
                        ID = p.ID,
                        ARName = p.ARName,
                        Code = p.Code,
                        LatName = p.LatName,
                        ClearnessRate = p.ClearnessRate,
                        CaliberNumber = p.CaliberNumber,
                        Notes = p.Notes
                    };
            return q.ToList();
        }

        public Task<int> CountAsync()
        {
            return Task.Run<int>(() =>
            {
                return itemCaliberRepo.CountAsync();
            });
        }


        public string GetLastCode()
        {
            var lastCode = itemCaliberRepo.GetAll().OrderByDescending(i => i.ID).FirstOrDefault();
            return lastCode.Code;
        }



        public ItemCaliberVM GetByID(int CaliberID)
        {
            var q = from p in itemCaliberRepo.Filter(x => x.ID == CaliberID && x.Active == 1)
                    select new ItemCaliberVM
                    {
                        ID = p.ID,
                        ARName = p.ARName,
                        Code = p.Code,
                        LatName = p.LatName,
                        ClearnessRate = p.ClearnessRate,
                        CaliberNumber = p.CaliberNumber,
                        Notes = p.Notes
                    };
            return q.FirstOrDefault();
        }


        public ItemCaliberVM GetByIDLog(int CaliberID)
        {
            var q = from p in itemCaliberRepo.GetAll().Where(x => x.ID == CaliberID)
                    select new ItemCaliberVM
                    {
                        ID = p.ID,
                        ARName = p.ARName,
                        Code = p.Code,
                        LatName = p.LatName,
                        ClearnessRate = p.ClearnessRate,
                        CaliberNumber = p.CaliberNumber,
                        Notes = p.Notes
                    };
            return q.FirstOrDefault();
        }


        public Task<List<ItemCaliberVM>> GetAll()
        {

            return Task.Run<List<ItemCaliberVM>>(() =>
            {               
                var q = from p in itemCaliberRepo.GetAll()

                        select new ItemCaliberVM
                        {
                            ID = p.ID,
                            ARName = p.ARName,
                            Code = p.Code,
                            LatName = p.LatName,
                            ClearnessRate = p.ClearnessRate,
                            CaliberNumber = p.CaliberNumber,
                            Notes = p.Notes
                        };
                return q.ToList();
            });


        }

    }
}
