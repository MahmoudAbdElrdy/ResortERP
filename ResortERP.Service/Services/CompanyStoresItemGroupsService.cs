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
    public class CompanyStoresItemGroupsService : ICompanyStoresItemGroupsService, IDisposable
    {
        IGenericRepository<COMPANY_STORES_ITEMS_GROUPS> csigRepo;

        public CompanyStoresItemGroupsService(IGenericRepository<COMPANY_STORES_ITEMS_GROUPS> csigRepo)
        {
            this.csigRepo = csigRepo;
        }

        public Task<int> CountAsync(int CompnyStoreID)
        {
            return Task.Run<int>(() =>
            {
                return csigRepo.GetAll().Where(x => x.COM_STORE_ID == CompnyStoreID).ToList().Count();
            });
        }

        public bool Delete(CompStore_ItmGroupsVM entity)
        {
            COMPANY_STORES_ITEMS_GROUPS csig = new COMPANY_STORES_ITEMS_GROUPS
            {
                CSIG_ID = entity.CSIG_ID,
                COM_STORE_ID = entity.COM_STORE_ID,
                GROUP_ID = entity.GROUP_ID
            };
            csigRepo.Delete(csig, entity.CSIG_ID);
            return true;
        }

        public Task<bool> DeleteAsync(CompStore_ItmGroupsVM entity)
        {
            return Task.Run<bool>(() =>
            {
                COMPANY_STORES_ITEMS_GROUPS csig = new COMPANY_STORES_ITEMS_GROUPS
                {
                    CSIG_ID = entity.CSIG_ID,
                    COM_STORE_ID = entity.COM_STORE_ID,
                    GROUP_ID = entity.GROUP_ID
                };
                csigRepo.Delete(csig, entity.CSIG_ID);
                return true;
            });
        }

        public void Dispose()
        {
            csigRepo.Dispose();
        }

        public List<CompStore_ItmGroupsVM> GetAll(int CompnyStoreID)
        {
            var q = from entity in csigRepo.GetAll()
                    select new CompStore_ItmGroupsVM
                    {
                        CSIG_ID = entity.CSIG_ID,
                        COM_STORE_ID = entity.COM_STORE_ID,
                        GROUP_ID = entity.GROUP_ID
                    };
            return q.Where(x => x.COM_STORE_ID == CompnyStoreID).ToList();
        }

        public Task<List<CompStore_ItmGroupsVM>> GetAllAsync(int pageNum, int pageSize, int CompnyStoreID)
        {
            return Task.Run<List<CompStore_ItmGroupsVM>>(() =>
            {
                int rowCount;
                var q = from entity in csigRepo.GetPaged<long>(pageNum, pageSize, p => p.CSIG_ID, false, out rowCount)
                        select new CompStore_ItmGroupsVM
                        {
                            CSIG_ID = entity.CSIG_ID,
                            COM_STORE_ID = entity.COM_STORE_ID,
                            GROUP_ID = entity.GROUP_ID
                        };
                return q.Where(x => x.COM_STORE_ID == CompnyStoreID).ToList();
            });
        }

        public bool Insert(CompStore_ItmGroupsVM entity)
        {
            COMPANY_STORES_ITEMS_GROUPS csig = new COMPANY_STORES_ITEMS_GROUPS
            {
                CSIG_ID = entity.CSIG_ID,
                COM_STORE_ID = entity.COM_STORE_ID,
                GROUP_ID = entity.GROUP_ID
            };
            csigRepo.Add(csig);
            return true;
        }


        public Task<bool> InsertAsync(CompStore_ItmGroupsVM entity)
        {
            return Task.Run<bool>(() =>
            {
                COMPANY_STORES_ITEMS_GROUPS csig = new COMPANY_STORES_ITEMS_GROUPS
                {
                    CSIG_ID = entity.CSIG_ID,
                    COM_STORE_ID = entity.COM_STORE_ID,
                    GROUP_ID = entity.GROUP_ID
                };
                csigRepo.Add(csig);
                return true;
            });
        }

        public bool Update(CompStore_ItmGroupsVM entity)
        {
            COMPANY_STORES_ITEMS_GROUPS csig = new COMPANY_STORES_ITEMS_GROUPS
            {
                CSIG_ID = entity.CSIG_ID,
                COM_STORE_ID = entity.COM_STORE_ID,
                GROUP_ID = entity.GROUP_ID
            };
            csigRepo.Update(csig, csig.CSIG_ID);
            return true;
        }

        public Task<bool> UpdateAsync(CompStore_ItmGroupsVM entity)
        {
            return Task.Run<bool>(() =>
            {
                COMPANY_STORES_ITEMS_GROUPS csig = new COMPANY_STORES_ITEMS_GROUPS
                {
                    CSIG_ID = entity.CSIG_ID,
                    COM_STORE_ID = entity.COM_STORE_ID,
                    GROUP_ID = entity.GROUP_ID
                };
                csigRepo.Update(csig, csig.CSIG_ID);
                return true;
            });
        }
    }
}
