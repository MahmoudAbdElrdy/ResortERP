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
    public class CustomerBranchesService : ICustomerBranchesService, IDisposable
    {
        IGenericRepository<CustomerBranches> customerBrancheRepo;
        public CustomerBranchesService(IGenericRepository<CustomerBranches> customerBrancheRepo)
        {
            this.customerBrancheRepo = customerBrancheRepo;
        }

        public Task<int> CountAsync()
        {
            return Task.Run<int>(() =>
            {
                return customerBrancheRepo.CountAsync();
            });
        }

        public bool Delete(CustomerBranchesVM entity)
        {
            CustomerBranches cus = new CustomerBranches
            {
                ACC_BRN_AR_NAME = entity.ACC_BRN_AR_NAME,
                ACC_BRN_EN_NAME = entity.ACC_BRN_EN_NAME,
                BRN_ADDR_REMARKS = entity.BRN_ADDR_REMARKS,
                BRN_REMARKS = entity.BRN_REMARKS,
                CUST_BRN_ID = entity.CUST_BRN_ID,
                ACC_ID = entity.ACC_ID,
                GOV_ID = entity.GOV_ID,
                NATION_ID = entity.NATION_ID,
                TOWN_ID = entity.TOWN_ID,
                VILLAGE_ID = entity.VILLAGE_ID,
                AddedBy = entity.AddedBy,
                AddedOn = entity.AddedOn,
                UpdatedBy = entity.UpdatedBy,
                updatedOn = entity.updatedOn
            };
            customerBrancheRepo.Delete(cus, entity.CUST_BRN_ID);
            return true;
        }

        public Task<bool> DeleteAsync(CustomerBranchesVM entity)
        {
            return Task.Run<bool>(() =>
            {
                CustomerBranches cus = new CustomerBranches
                {
                    ACC_BRN_AR_NAME = entity.ACC_BRN_AR_NAME,
                    ACC_BRN_EN_NAME = entity.ACC_BRN_EN_NAME,
                    BRN_ADDR_REMARKS = entity.BRN_ADDR_REMARKS,
                    BRN_REMARKS = entity.BRN_REMARKS,
                    CUST_BRN_ID = entity.CUST_BRN_ID,
                    ACC_ID = entity.ACC_ID,
                    GOV_ID = entity.GOV_ID,
                    NATION_ID = entity.NATION_ID,
                    TOWN_ID = entity.TOWN_ID,
                    VILLAGE_ID = entity.VILLAGE_ID,
                    AddedBy = entity.AddedBy,
                    AddedOn = entity.AddedOn,
                    UpdatedBy = entity.UpdatedBy,
                    updatedOn = entity.updatedOn
                };
                customerBrancheRepo.Delete(cus, entity.CUST_BRN_ID);
                return true;
            });
        }

        public void Dispose()
        {
            customerBrancheRepo.Dispose();
        }

        public List<CustomerBranchesVM> GetAll()
        {
            var q = from entity in customerBrancheRepo.GetAll()
                    select new CustomerBranchesVM
                    {
                        ACC_BRN_AR_NAME = entity.ACC_BRN_AR_NAME,
                        ACC_BRN_EN_NAME = entity.ACC_BRN_EN_NAME,
                        BRN_ADDR_REMARKS = entity.BRN_ADDR_REMARKS,
                        BRN_REMARKS = entity.BRN_REMARKS,
                        CUST_BRN_ID = entity.CUST_BRN_ID,
                        ACC_ID = entity.ACC_ID,
                        GOV_ID = entity.GOV_ID,
                        NATION_ID = entity.NATION_ID,
                        TOWN_ID = entity.TOWN_ID,
                        VILLAGE_ID = entity.VILLAGE_ID,
                        AddedBy = entity.AddedBy,
                        AddedOn = entity.AddedOn,
                        UpdatedBy = entity.UpdatedBy,
                        updatedOn = entity.updatedOn
                    };
            return q.ToList();
        }
        public List<CustomerBranchesVM> GetAllByAccID(int AccID)
        {
            var q = from entity in customerBrancheRepo.GetAll().Where(x => x.ACC_ID == AccID)
                    select new CustomerBranchesVM
                    {
                        ACC_BRN_AR_NAME = entity.ACC_BRN_AR_NAME,
                        ACC_BRN_EN_NAME = entity.ACC_BRN_EN_NAME,
                        BRN_ADDR_REMARKS = entity.BRN_ADDR_REMARKS,
                        BRN_REMARKS = entity.BRN_REMARKS,
                        CUST_BRN_ID = entity.CUST_BRN_ID,
                        ACC_ID = entity.ACC_ID,
                        GOV_ID = entity.GOV_ID,
                        NATION_ID = entity.NATION_ID,
                        TOWN_ID = entity.TOWN_ID,
                        VILLAGE_ID = entity.VILLAGE_ID,
                        AddedBy = entity.AddedBy,
                        AddedOn = entity.AddedOn,
                        UpdatedBy = entity.UpdatedBy,
                        updatedOn = entity.updatedOn
                    };
            return q.ToList();
        }

        public Task<List<CustomerBranchesVM>> GetAllAsync(int pageNum, int pageSize)
        {
            return Task.Run<List<CustomerBranchesVM>>(() =>
            {
                int rowCount;
                var q = from entity in customerBrancheRepo.GetPaged<long>(pageNum, pageSize, p => p.CUST_BRN_ID, false, out rowCount)
                        select new CustomerBranchesVM
                        {
                            ACC_BRN_AR_NAME = entity.ACC_BRN_AR_NAME,
                            ACC_BRN_EN_NAME = entity.ACC_BRN_EN_NAME,
                            BRN_ADDR_REMARKS = entity.BRN_ADDR_REMARKS,
                            BRN_REMARKS = entity.BRN_REMARKS,
                            CUST_BRN_ID = entity.CUST_BRN_ID,
                            ACC_ID = entity.ACC_ID,
                            GOV_ID = entity.GOV_ID,
                            NATION_ID = entity.NATION_ID,
                            TOWN_ID = entity.TOWN_ID,
                            VILLAGE_ID = entity.VILLAGE_ID,
                            AddedBy = entity.AddedBy,
                            AddedOn = entity.AddedOn,
                            UpdatedBy = entity.UpdatedBy,
                            updatedOn = entity.updatedOn
                        };
                return q.ToList();
            });
        }

        public bool Insert(CustomerBranchesVM entity)
        {
            CustomerBranches cus = new CustomerBranches
            {
                ACC_BRN_AR_NAME = entity.ACC_BRN_AR_NAME,
                ACC_BRN_EN_NAME = entity.ACC_BRN_EN_NAME,
                BRN_ADDR_REMARKS = entity.BRN_ADDR_REMARKS,
                BRN_REMARKS = entity.BRN_REMARKS,
                CUST_BRN_ID = entity.CUST_BRN_ID,
                ACC_ID = entity.ACC_ID,
                GOV_ID = entity.GOV_ID,
                NATION_ID = entity.NATION_ID,
                TOWN_ID = entity.TOWN_ID,
                VILLAGE_ID = entity.VILLAGE_ID,
                AddedBy = entity.AddedBy,
                AddedOn = entity.AddedOn,
                UpdatedBy = entity.UpdatedBy,
                updatedOn = entity.updatedOn
            };
            customerBrancheRepo.Add(cus);
            return true;
        }

        public Task<bool> InsertAsync(CustomerBranchesVM entity)
        {
            return Task.Run<bool>(() =>
            {
                CustomerBranches cus = new CustomerBranches
                {
                    ACC_BRN_AR_NAME = entity.ACC_BRN_AR_NAME,
                    ACC_BRN_EN_NAME = entity.ACC_BRN_EN_NAME,
                    BRN_ADDR_REMARKS = entity.BRN_ADDR_REMARKS,
                    BRN_REMARKS = entity.BRN_REMARKS,
                    CUST_BRN_ID = entity.CUST_BRN_ID,
                    ACC_ID = entity.ACC_ID,
                    GOV_ID = entity.GOV_ID,
                    NATION_ID = entity.NATION_ID,
                    TOWN_ID = entity.TOWN_ID,
                    VILLAGE_ID = entity.VILLAGE_ID,
                    AddedBy = entity.AddedBy,
                    AddedOn = entity.AddedOn,
                    UpdatedBy = entity.UpdatedBy,
                    updatedOn = entity.updatedOn
                };
                customerBrancheRepo.Add(cus);
                return true;
            });
        }

        public bool Update(CustomerBranchesVM entity)
        {
            CustomerBranches cus = new CustomerBranches
            {
                ACC_BRN_AR_NAME = entity.ACC_BRN_AR_NAME,
                ACC_BRN_EN_NAME = entity.ACC_BRN_EN_NAME,
                BRN_ADDR_REMARKS = entity.BRN_ADDR_REMARKS,
                BRN_REMARKS = entity.BRN_REMARKS,
                CUST_BRN_ID = entity.CUST_BRN_ID,
                ACC_ID = entity.ACC_ID,
                GOV_ID = entity.GOV_ID,
                NATION_ID = entity.NATION_ID,
                TOWN_ID = entity.TOWN_ID,
                VILLAGE_ID = entity.VILLAGE_ID,
                AddedBy = entity.AddedBy,
                AddedOn = entity.AddedOn,
                UpdatedBy = entity.UpdatedBy,
                updatedOn = entity.updatedOn
            };
            customerBrancheRepo.Update(cus, cus.CUST_BRN_ID);
            return true;
        }

        public Task<bool> UpdateAsync(CustomerBranchesVM entity)
        {
            return Task.Run<bool>(() =>
            {
                CustomerBranches cus = new CustomerBranches
                {
                    ACC_BRN_AR_NAME = entity.ACC_BRN_AR_NAME,
                    ACC_BRN_EN_NAME = entity.ACC_BRN_EN_NAME,
                    BRN_ADDR_REMARKS = entity.BRN_ADDR_REMARKS,
                    BRN_REMARKS = entity.BRN_REMARKS,
                    CUST_BRN_ID = entity.CUST_BRN_ID,
                    ACC_ID = entity.ACC_ID,
                    GOV_ID = entity.GOV_ID,
                    NATION_ID = entity.NATION_ID,
                    TOWN_ID = entity.TOWN_ID,
                    VILLAGE_ID = entity.VILLAGE_ID,
                    AddedBy = entity.AddedBy,
                    AddedOn = entity.AddedOn,
                    UpdatedBy = entity.UpdatedBy,
                    updatedOn = entity.updatedOn
                };
                customerBrancheRepo.Update(cus, cus.CUST_BRN_ID);
                return true;
            });
        }
    }
}
