using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ResortERP.Service.IServices;
using ResortERP.Repository;
using ResortERP.Core;
using ResortERP.Core.VM;

namespace ResortERP.Service.Services
{
    public class CompanyBranchesService : ICompanyBranchesService, IDisposable
    {
        IGenericRepository<COMPANY_BRANCHES> companyBranchesRepo;
        IGenericRepository<UserPriviligeBranches> userPriviligeBranchesRepo;
        IGenericRepository<User> uidRepo;
        public void Dispose()
        {
            companyBranchesRepo.Dispose();
        }

        public CompanyBranchesService(IGenericRepository<COMPANY_BRANCHES> companyBranchesRepo, IGenericRepository<UserPriviligeBranches> userPriviligeBranchesRepo, IGenericRepository<User> uidRepo)
        {
            this.companyBranchesRepo = companyBranchesRepo;
            this.userPriviligeBranchesRepo = userPriviligeBranchesRepo;
            this.uidRepo = uidRepo;
        }

        public Task<List<COMPANY_BRANCHESVM>> GetAllReport(string id)
        {
            return Task.Run(() =>
            {
                var q = uidRepo.GetAll().Where(a => a.UID == id);
                if (q != null)
                {

                    var userId = q.FirstOrDefault().ID;
                    var companyBranchesList = from entity in companyBranchesRepo.GetAll() join cb in userPriviligeBranchesRepo.GetAll().Where(a => a.ID == userId)
                                              on entity.COM_BRN_ID equals cb.COM_BRN_ID
                                              select new COMPANY_BRANCHESVM
                                          {
                                              COM_BRN_ID = entity.COM_BRN_ID,
                                              COM_BRN_CODE = entity.COM_BRN_CODE,
                                              COM_BRN_AR_NAME = entity.COM_BRN_AR_NAME,
                                              COM_BRN_EN_NAME = entity.COM_BRN_EN_NAME,
                                              COM_BRN_AR_ABRV = entity.COM_BRN_AR_ABRV,
                                              COM_BRN_EN_ABRV = entity.COM_BRN_EN_ABRV,
                                              NATION_ID = entity.NATION_ID,
                                              GOV_ID = entity.GOV_ID,
                                              TOWN_ID = entity.TOWN_ID,
                                              VILLAGE_ID = entity.VILLAGE_ID,
                                              COM_BRN_ADDR_REMARKS = entity.COM_BRN_ADDR_REMARKS,
                                              COM_MASTER_BRN_ID = entity.COM_MASTER_BRN_ID,
                                              COM_BRN_REMARKS = entity.COM_BRN_REMARKS,
                                              AddedBy = entity.AddedBy,
                                              AddedOn = entity.AddedOn,
                                              UpdatedBy = entity.UpdatedBy,
                                              updatedOn = entity.updatedOn,
                                              Disable = entity.Disable,
                                              CurrencyID = entity.CurrencyID,
                                              ManagerName = entity.ManagerName,

                                              IsDefault = entity.IsDefault
                                          };
                    return companyBranchesList.ToList();
                }
                return null;
            });
        }
        public Task<List<COMPANY_BRANCHESVM>> GetAll()
        {
            return Task.Run(() =>
            {
                var companyBranchesList = from entity in companyBranchesRepo.GetAll()
                                          select new COMPANY_BRANCHESVM
                                          {
                                              COM_BRN_ID = entity.COM_BRN_ID,
                                              COM_BRN_CODE = entity.COM_BRN_CODE,
                                              COM_BRN_AR_NAME = entity.COM_BRN_AR_NAME,
                                              COM_BRN_EN_NAME = entity.COM_BRN_EN_NAME,
                                              COM_BRN_AR_ABRV = entity.COM_BRN_AR_ABRV,
                                              COM_BRN_EN_ABRV = entity.COM_BRN_EN_ABRV,
                                              NATION_ID = entity.NATION_ID,
                                              GOV_ID = entity.GOV_ID,
                                              TOWN_ID = entity.TOWN_ID,
                                              VILLAGE_ID = entity.VILLAGE_ID,
                                              COM_BRN_ADDR_REMARKS = entity.COM_BRN_ADDR_REMARKS,
                                              COM_MASTER_BRN_ID = entity.COM_MASTER_BRN_ID,
                                              COM_BRN_REMARKS = entity.COM_BRN_REMARKS,
                                              AddedBy = entity.AddedBy,
                                              AddedOn = entity.AddedOn,
                                              UpdatedBy = entity.UpdatedBy,
                                              updatedOn = entity.updatedOn,
                                              Disable = entity.Disable,
                                              CurrencyID = entity.CurrencyID,
                                              ManagerName = entity.ManagerName,

                                              IsDefault = entity.IsDefault
                                          };
                return companyBranchesList.ToList();

            });
        }

        public Task<COMPANY_BRANCHESVM> getByIdLog(int COM_BRN_ID)
        {
            return Task.Run(() =>
            {
                var companyBranchesList = from entity in companyBranchesRepo.GetAll().Where(a=>a.COM_BRN_ID== COM_BRN_ID)
                                          select new COMPANY_BRANCHESVM
                                          {
                                              COM_BRN_ID = entity.COM_BRN_ID,
                                              COM_BRN_CODE = entity.COM_BRN_CODE,
                                              COM_BRN_AR_NAME = entity.COM_BRN_AR_NAME,
                                              COM_BRN_EN_NAME = entity.COM_BRN_EN_NAME,
                                              COM_BRN_AR_ABRV = entity.COM_BRN_AR_ABRV,
                                              COM_BRN_EN_ABRV = entity.COM_BRN_EN_ABRV,
                                              NATION_ID = entity.NATION_ID,
                                              GOV_ID = entity.GOV_ID,
                                              TOWN_ID = entity.TOWN_ID,
                                              VILLAGE_ID = entity.VILLAGE_ID,
                                              COM_BRN_ADDR_REMARKS = entity.COM_BRN_ADDR_REMARKS,
                                              COM_MASTER_BRN_ID = entity.COM_MASTER_BRN_ID,
                                              COM_BRN_REMARKS = entity.COM_BRN_REMARKS,
                                              AddedBy = entity.AddedBy,
                                              AddedOn = entity.AddedOn,
                                              UpdatedBy = entity.UpdatedBy,
                                              updatedOn = entity.updatedOn,
                                              Disable = entity.Disable,
                                              CurrencyID = entity.CurrencyID,
                                              ManagerName = entity.ManagerName,

                                              IsDefault = entity.IsDefault
                                          };
                return companyBranchesList.FirstOrDefault();

            });
        }
        public Task<List<COMPANY_BRANCHESVM>> GetAllAsync(int pageNum, int pageSize)
        {
            return Task.Run(() =>
            {
                int rowCount;
                var companyBranchesList = from entity in companyBranchesRepo.GetPaged(pageNum, pageSize, c => c.COM_BRN_ID, false, out rowCount)
                                          join cb in companyBranchesRepo.GetPaged<int>(pageNum, pageSize, c => c.COM_BRN_ID, false, out rowCount) on entity.COM_MASTER_BRN_ID equals cb.COM_BRN_ID into g
                                          from y in g.DefaultIfEmpty()
                                          select new COMPANY_BRANCHESVM
                                          {
                                              COM_BRN_ID = entity.COM_BRN_ID,
                                              COM_BRN_CODE = entity.COM_BRN_CODE,
                                              COM_BRN_AR_NAME = entity.COM_BRN_AR_NAME,
                                              COM_BRN_EN_NAME = entity.COM_BRN_EN_NAME,
                                              COM_BRN_AR_ABRV = entity.COM_BRN_AR_ABRV,
                                              COM_BRN_EN_ABRV = entity.COM_BRN_EN_ABRV,
                                              NATION_ID = entity.NATION_ID,
                                              GOV_ID = entity.GOV_ID,
                                              TOWN_ID = entity.TOWN_ID,
                                              VILLAGE_ID = entity.VILLAGE_ID,
                                              COM_BRN_ADDR_REMARKS = entity.COM_BRN_ADDR_REMARKS,
                                              COM_MASTER_BRN_ID = entity.COM_MASTER_BRN_ID,
                                              COM_BRN_REMARKS = entity.COM_BRN_REMARKS,
                                              AddedBy = entity.AddedBy,
                                              AddedOn = entity.AddedOn,
                                              UpdatedBy = entity.UpdatedBy,
                                              updatedOn = entity.updatedOn,
                                              Disable = entity.Disable,
                                              
                                              MasterCompanyBranch = y.COM_BRN_AR_NAME,
                                              CurrencyID = entity.CurrencyID,
                                              ManagerName=entity.ManagerName,

                                              IsDefault=entity.IsDefault
                                          };
                return companyBranchesList.ToList();

            });
        }

        public Task<int> InsertAsync(COMPANY_BRANCHESVM entity)
        {
            return Task.Run(() =>
            {
                if (entity.IsDefault == true)
                {
                    var q = companyBranchesRepo.GetAll().Where(a => a.IsDefault == true);
                    if (q != null)
                    {
                        var y = q.FirstOrDefault();
                        y.IsDefault = false;
                        companyBranchesRepo.Update(y,y.COM_BRN_ID);
                    }
                   
                }
                COMPANY_BRANCHES cb = new COMPANY_BRANCHES();
                {
                    cb.COM_BRN_CODE = entity.COM_BRN_CODE;
                    cb.COM_BRN_AR_NAME = entity.COM_BRN_AR_NAME;
                    cb.COM_BRN_EN_NAME = entity.COM_BRN_EN_NAME;
                    cb.COM_BRN_AR_ABRV = entity.COM_BRN_AR_ABRV;
                    cb.COM_BRN_EN_ABRV = entity.COM_BRN_EN_ABRV;
                    cb.NATION_ID = entity.NATION_ID;
                    cb.GOV_ID = entity.GOV_ID;
                    cb.TOWN_ID = entity.TOWN_ID;
                    cb.COM_BRN_ID = entity.COM_BRN_ID;
                    cb.VILLAGE_ID = entity.VILLAGE_ID;
                    cb.COM_BRN_ADDR_REMARKS = entity.COM_BRN_ADDR_REMARKS;
                    cb.COM_MASTER_BRN_ID = entity.COM_MASTER_BRN_ID;
                    cb.COM_BRN_REMARKS = entity.COM_BRN_REMARKS;
                    cb.AddedBy = entity.AddedBy;
                    cb.AddedOn = entity.AddedOn;
                    cb.UpdatedBy = entity.UpdatedBy;
                    cb.updatedOn = entity.updatedOn;
                    cb.Disable = entity.Disable;
                    cb.CurrencyID = entity.CurrencyID;
                    cb.ManagerName = entity.ManagerName;
                    cb.IsDefault = entity.IsDefault;
                };
                companyBranchesRepo.Add(cb);
                return cb.COM_BRN_ID;
            });
        }

        public Task<bool> UpdateAsync(COMPANY_BRANCHESVM entity)
        {
            return Task.Run(() =>
            {
                if (entity.IsDefault == true)
                {
                    var q = companyBranchesRepo.GetAll().Where(a => a.IsDefault == true);
                    if (q != null)
                    {
                        var y = q.FirstOrDefault();
                        y.IsDefault = false;
                        companyBranchesRepo.Update(y, y.COM_BRN_ID);
                    }

                }
                COMPANY_BRANCHES cb = new COMPANY_BRANCHES();
                {
                    cb.COM_BRN_CODE = entity.COM_BRN_CODE;
                    cb.COM_BRN_AR_NAME = entity.COM_BRN_AR_NAME;
                    cb.COM_BRN_EN_NAME = entity.COM_BRN_EN_NAME;
                    cb.COM_BRN_AR_ABRV = entity.COM_BRN_AR_ABRV;
                    cb.COM_BRN_EN_ABRV = entity.COM_BRN_EN_ABRV;
                    cb.NATION_ID = entity.NATION_ID;
                    cb.GOV_ID = entity.GOV_ID;
                    cb.TOWN_ID = entity.TOWN_ID;
                    cb.COM_BRN_ID = entity.COM_BRN_ID;
                    cb.VILLAGE_ID = entity.VILLAGE_ID;
                    cb.COM_BRN_ADDR_REMARKS = entity.COM_BRN_ADDR_REMARKS;
                    cb.COM_MASTER_BRN_ID = entity.COM_MASTER_BRN_ID;
                    cb.COM_BRN_REMARKS = entity.COM_BRN_REMARKS;
                    cb.AddedBy = entity.AddedBy;
                    cb.AddedOn = entity.AddedOn;
                    cb.UpdatedBy = entity.UpdatedBy;
                    cb.updatedOn = entity.updatedOn;
                    cb.Disable = entity.Disable;
                    cb.CurrencyID = entity.CurrencyID;
                    cb.ManagerName = entity.ManagerName;
                    cb.IsDefault = entity.IsDefault;
                };
                companyBranchesRepo.Update(cb, cb.COM_BRN_ID);
                return true;
            });
        }
        public Task<bool> DeleteAsync(COMPANY_BRANCHESVM entity)
        {
            return Task.Run(() =>
            {
                COMPANY_BRANCHES cb = new COMPANY_BRANCHES();
                {
                    cb.COM_BRN_CODE = entity.COM_BRN_CODE;
                    cb.COM_BRN_AR_NAME = entity.COM_BRN_AR_NAME;
                    cb.COM_BRN_EN_NAME = entity.COM_BRN_EN_NAME;
                    cb.COM_BRN_AR_ABRV = entity.COM_BRN_AR_ABRV;
                    cb.COM_BRN_EN_ABRV = entity.COM_BRN_EN_ABRV;
                    cb.NATION_ID = entity.NATION_ID;
                    cb.GOV_ID = entity.GOV_ID;
                    cb.TOWN_ID = entity.TOWN_ID;
                    cb.COM_BRN_ID = entity.COM_BRN_ID;
                    cb.VILLAGE_ID = entity.VILLAGE_ID;
                    cb.COM_BRN_ADDR_REMARKS = entity.COM_BRN_ADDR_REMARKS;
                    cb.COM_MASTER_BRN_ID = entity.COM_MASTER_BRN_ID;
                    cb.COM_BRN_REMARKS = entity.COM_BRN_REMARKS;
                    cb.AddedBy = entity.AddedBy;
                    cb.AddedOn = entity.AddedOn;
                    cb.UpdatedBy = entity.UpdatedBy;
                    cb.updatedOn = entity.updatedOn;
                    cb.Disable = entity.Disable;
                    cb.CurrencyID = entity.CurrencyID;
                    cb.ManagerName = entity.ManagerName;
                    cb.IsDefault = entity.IsDefault;
                }
                companyBranchesRepo.Delete(cb, cb.COM_BRN_ID);
                return true;
            });
        }
        public Task<bool> getById(int id)
        {
            return Task.Run(() =>
            {
                var q = companyBranchesRepo.GetAll().Where(a => a.COM_BRN_ID == id);
                if (q != null)
                    return true;
                else
                    return false;
            });
        }


        public Task<List<COMPANY_BRANCHESVM>> getMainComapnyBranches()
        {
            return Task.Run(() =>
            {
                var companyBrancesList = from c in companyBranchesRepo.GetAll()
                                         select new COMPANY_BRANCHESVM
                                         {
                                             COM_BRN_ID = c.COM_BRN_ID,
                                             COM_BRN_CODE = c.COM_BRN_CODE,
                                             COM_BRN_AR_NAME = c.COM_BRN_AR_NAME,
                                             COM_BRN_EN_NAME = c.COM_BRN_EN_NAME,
                                             COM_BRN_AR_ABRV = c.COM_BRN_AR_ABRV,
                                             COM_BRN_EN_ABRV = c.COM_BRN_EN_ABRV,
                                             NATION_ID = c.NATION_ID,
                                             GOV_ID = c.GOV_ID,
                                             TOWN_ID = c.TOWN_ID,
                                             VILLAGE_ID = c.VILLAGE_ID,
                                             COM_BRN_ADDR_REMARKS = c.COM_BRN_ADDR_REMARKS,
                                             COM_MASTER_BRN_ID = c.COM_MASTER_BRN_ID,
                                             COM_BRN_REMARKS = c.COM_BRN_REMARKS,
                                             AddedBy = c.AddedBy,
                                             AddedOn = c.AddedOn,
                                             UpdatedBy = c.UpdatedBy,
                                             updatedOn = c.updatedOn,
                                             Disable = c.Disable,
                                             CurrencyID = c.CurrencyID,
                                             ManagerName = c.ManagerName,
                                              IsDefault = c.IsDefault
                                         };
                return companyBrancesList.ToList();
            });

        }

        public Task<int> countAsync()
        {
            return Task.Run(() =>
             {
                 return companyBranchesRepo.CountAsync();
             });
        }


        public string GetLastCode()
        {
            var lastCode = companyBranchesRepo.GetAll().OrderByDescending(c => c.COM_BRN_ID).FirstOrDefault();
            return lastCode.COM_BRN_CODE;
        }

        public List<COMPANY_BRANCHES> getByNationID(int nationId)
        {
            var q = companyBranchesRepo.GetAll().Where(a => a.NATION_ID == nationId).ToList();
            return q;
        }
        public List<COMPANY_BRANCHES> getByGOVID(int govId)
        {
            var q = companyBranchesRepo.GetAll().Where(a => a.GOV_ID == govId).ToList();
            return q;
        }
        public List<COMPANY_BRANCHES> getByTownID(int townId)
        {
            var q = companyBranchesRepo.GetAll().Where(a => a.TOWN_ID == townId).ToList();
            return q;
        }
        public List<COMPANY_BRANCHES> getByVilID(long villageId)
        {
            var q = companyBranchesRepo.GetAll().Where(a => a.VILLAGE_ID == villageId).ToList();
            return q;
        }

    }
}
