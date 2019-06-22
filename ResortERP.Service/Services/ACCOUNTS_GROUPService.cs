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
    public class ACCOUNTS_GROUPService : IACCOUNTS_GROUPService, IDisposable
    {
        IGenericRepository<ACCOUNTS_GROUP> ACCOUNTS_GROUPRepo;
        IGenericRepository<ACCOUNTS> ACCOUNTSRepo;
        public ACCOUNTS_GROUPService(IGenericRepository<ACCOUNTS_GROUP> ACCOUNTS_GROUPRepo, IGenericRepository<ACCOUNTS> ACCOUNTSRepo)
        {
            this.ACCOUNTS_GROUPRepo = ACCOUNTS_GROUPRepo;
            this.ACCOUNTSRepo = ACCOUNTSRepo;
        }

        public void Dispose()
        {
            ACCOUNTS_GROUPRepo.Dispose();
        }


        public bool Insert(ACCOUNTS_GROUPVM entity)
        {
            ACCOUNTS_GROUP ig = new ACCOUNTS_GROUP
            {
                GROUP_AR_NAME = entity.GROUP_AR_NAME,
                GROUP_CODE = entity.GROUP_CODE,
                GROUP_EN_NAME = entity.GROUP_EN_NAME,
                GROUP_ID = entity.GROUP_ID,
                GROUP_MASTER_ID = entity.GROUP_MASTER_ID,
                AddedBy = entity.AddedBy,
                AddedOn=DateTime.Now,
                GROUP_REMARKS=entity.GROUP_REMARKS
            };
            ACCOUNTS_GROUPRepo.Add(ig);
            return true;
        }

        public Task<int> InsertAsync(ACCOUNTS_GROUPVM entity)
        {
            return Task.Run<int>(() =>
            {
                ACCOUNTS_GROUP ig = new ACCOUNTS_GROUP
                {
                    GROUP_AR_NAME = entity.GROUP_AR_NAME,
                    GROUP_CODE = entity.GROUP_CODE,
                    GROUP_EN_NAME = entity.GROUP_EN_NAME,
                    GROUP_ID = entity.GROUP_ID,
                    GROUP_MASTER_ID = entity.GROUP_MASTER_ID,
                    AddedBy = entity.AddedBy,
                    AddedOn = DateTime.Now,
                    GROUP_REMARKS = entity.GROUP_REMARKS
                };
                ACCOUNTS_GROUPRepo.Add(ig);
                return ig.GROUP_ID;
            }
              );

        }

        public bool Update(ACCOUNTS_GROUPVM entity)
        {

            ACCOUNTS_GROUP ig = new ACCOUNTS_GROUP
            {
                GROUP_AR_NAME = entity.GROUP_AR_NAME,
                GROUP_CODE = entity.GROUP_CODE,
                GROUP_EN_NAME = entity.GROUP_EN_NAME,
                GROUP_ID = entity.GROUP_ID,
                GROUP_MASTER_ID = entity.GROUP_MASTER_ID,
                UpdatedBy = entity.UpdatedBy,
                updatedOn = DateTime.Now,
                GROUP_REMARKS = entity.GROUP_REMARKS
            };
            ACCOUNTS_GROUPRepo.Update(ig, ig.GROUP_ID);
            return true;


        }
        public Task<bool> UpdateAsync(ACCOUNTS_GROUPVM entity)
        {
            return Task.Run<bool>(() =>
            {
                ACCOUNTS_GROUP ig = new ACCOUNTS_GROUP
                {
                    GROUP_AR_NAME = entity.GROUP_AR_NAME,
                    GROUP_CODE = entity.GROUP_CODE,
                    GROUP_EN_NAME = entity.GROUP_EN_NAME,
                    GROUP_ID = entity.GROUP_ID,
                    GROUP_MASTER_ID = entity.GROUP_MASTER_ID,
                    UpdatedBy = entity.UpdatedBy,
                    updatedOn = DateTime.Now,
                    GROUP_REMARKS = entity.GROUP_REMARKS
                };
                ACCOUNTS_GROUPRepo.Update(ig, ig.GROUP_ID);
                return true;
            }
              );

        }

        public bool Delete(ACCOUNTS_GROUPVM entity)
        {
            ACCOUNTS_GROUP ig = new ACCOUNTS_GROUP
            {
                GROUP_AR_NAME = entity.GROUP_AR_NAME,
                GROUP_CODE = entity.GROUP_CODE,
                GROUP_EN_NAME = entity.GROUP_EN_NAME,
                GROUP_ID = entity.GROUP_ID,
                GROUP_MASTER_ID = entity.GROUP_MASTER_ID,
                UpdatedBy = entity.UpdatedBy,
                updatedOn = DateTime.Now,
                GROUP_REMARKS = entity.GROUP_REMARKS
            };
            ACCOUNTS_GROUPRepo.Delete(ig, entity.GROUP_ID);
            return true;


        }
        public Task<bool> DeleteAsync(ACCOUNTS_GROUPVM entity)
        {
            return Task.Run<bool>(() =>
            {
                ACCOUNTS_GROUP ig = new ACCOUNTS_GROUP
                {
                    GROUP_AR_NAME = entity.GROUP_AR_NAME,
                    GROUP_CODE = entity.GROUP_CODE,
                    GROUP_EN_NAME = entity.GROUP_EN_NAME,
                    GROUP_ID = entity.GROUP_ID,
                    GROUP_MASTER_ID = entity.GROUP_MASTER_ID,
                    UpdatedBy = entity.UpdatedBy,
                    updatedOn = DateTime.Now,
                    GROUP_REMARKS = entity.GROUP_REMARKS
                };
                ACCOUNTS_GROUPRepo.Delete(ig, entity.GROUP_ID);
                return true;
            });
        }
         
        public Task<List<ACCOUNTS_GROUPVM>> GetAllAsync(int pageNum, int pageSize)
        {

            return Task.Run<List<ACCOUNTS_GROUPVM>>(() =>
            {

                int rowCount;
                var q = from p in ACCOUNTS_GROUPRepo.GetPaged<int>(pageNum, pageSize, p => p.GROUP_ID, false, out rowCount)
                        join e1 in ACCOUNTS_GROUPRepo.GetPaged<int>(pageNum, pageSize, p => p.GROUP_ID, false, out rowCount) on p.GROUP_MASTER_ID equals e1.GROUP_ID into g
                        from y in g.DefaultIfEmpty()
                        select new ACCOUNTS_GROUPVM
                        {
                            GROUP_AR_NAME = p.GROUP_AR_NAME,
                            GROUP_CODE = p.GROUP_CODE,
                            GROUP_EN_NAME = p.GROUP_EN_NAME,
                            GROUP_ID = p.GROUP_ID,
                            GROUP_MASTER_ID = p.GROUP_MASTER_ID,
                            UpdatedBy = p.UpdatedBy,
                            updatedOn = DateTime.Now,
                            AddedOn=p.AddedOn,
                            AddedBy=p.AddedBy,
                            GROUP_REMARKS = p.GROUP_REMARKS
                        };



                return q.ToList();

            });


        }

        public List<ACCOUNTS_GROUPVM> GetAllACCOUNTS_GROUP()
        {
            var q = from p in ACCOUNTS_GROUPRepo.GetAll()
                    select new ACCOUNTS_GROUPVM
                    {
                        GROUP_AR_NAME = p.GROUP_AR_NAME,
                        GROUP_CODE = p.GROUP_CODE,
                        GROUP_EN_NAME = p.GROUP_EN_NAME,
                        GROUP_ID = p.GROUP_ID,
                        GROUP_MASTER_ID = p.GROUP_MASTER_ID,
                        UpdatedBy = p.UpdatedBy,
                        updatedOn = DateTime.Now,
                        AddedOn = p.AddedOn,
                        AddedBy = p.AddedBy,
                        GROUP_REMARKS = p.GROUP_REMARKS
                    };
            return q.ToList();
        }

        public Task<int> CountAsync()
        {
            return Task.Run<int>(() =>
            {
                return ACCOUNTS_GROUPRepo.CountAsync();
            });
        }

        public Task<ACCOUNTS_GROUPVM> getACCOUNTS_GROUPByID(int GroupID)
        {
            return Task.Run<ACCOUNTS_GROUPVM>(() =>
            {
                var q = from g in ACCOUNTS_GROUPRepo.Filter(g => g.GROUP_ID == GroupID)
                        select new ACCOUNTS_GROUPVM
                        {
                            GROUP_AR_NAME = g.GROUP_AR_NAME,
                            GROUP_CODE = g.GROUP_CODE,
                            GROUP_EN_NAME = g.GROUP_EN_NAME,
                            GROUP_ID = g.GROUP_ID,
                            GROUP_MASTER_ID = g.GROUP_MASTER_ID,
                            UpdatedBy = g.UpdatedBy,
                            updatedOn = DateTime.Now,
                            AddedOn = g.AddedOn,
                            AddedBy = g.AddedBy,
                            GROUP_REMARKS = g.GROUP_REMARKS
                        };
                return q.FirstOrDefault();
            });
        }


        public string GetLastCode()
        {
            var lastCode = ACCOUNTS_GROUPRepo.GetAll().OrderByDescending(i => i.GROUP_ID).FirstOrDefault();
            if (lastCode != null)
                return lastCode.GROUP_CODE;
            else return "0";
        }
        public ACCOUNTS_GROUPVM GetByID(int ACCOUNTS_GROUPID)
        {
                var q = from p in ACCOUNTS_GROUPRepo.GetAll()
                        select new ACCOUNTS_GROUPVM
                        {
                            GROUP_AR_NAME = p.GROUP_AR_NAME,
                            GROUP_CODE = p.GROUP_CODE,
                            GROUP_EN_NAME = p.GROUP_EN_NAME,
                            GROUP_ID = p.GROUP_ID,
                            GROUP_MASTER_ID = p.GROUP_MASTER_ID,
                            UpdatedBy = p.UpdatedBy,
                            updatedOn = DateTime.Now,
                            AddedOn = p.AddedOn,
                            AddedBy = p.AddedBy,
                            GROUP_REMARKS = p.GROUP_REMARKS
                        };
            var res= q.Where(x => x.GROUP_ID == ACCOUNTS_GROUPID).FirstOrDefault();
            return res;

        }

    
    }
}
