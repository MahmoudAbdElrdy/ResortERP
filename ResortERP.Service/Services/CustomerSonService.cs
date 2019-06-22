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
    public class CustomerSonService : ICustomerSonService, IDisposable
    {
        IGenericRepository<CustomerSon> customerSonRepo;
        public CustomerSonService(IGenericRepository<CustomerSon> customerSonRepo)
        {
            this.customerSonRepo = customerSonRepo;
        }

        public Task<int> CountAsync()
        {
            return Task.Run<int>(() =>
            {
                return customerSonRepo.CountAsync();
            });
        }

        public bool Delete(CustomerSonVM entity)
        {
            CustomerSon cus = new CustomerSon
            {
                CUSTOMER_ID = entity.CUSTOMER_ID,
                CUSTOMER_SON_ID = entity.CUSTOMER_SON_ID,
                CUSTOMER_BIRTHDATE = entity.CUSTOMER_BIRTHDATE,
                CUSTOMER_SON_NAME = entity.CUSTOMER_SON_NAME,
                CUSTOMER_SON_ORDER = entity.CUSTOMER_SON_ORDER,
                CUSTOMER_SON_SEX = entity.CUSTOMER_SON_SEX,
                AddedBy = entity.AddedBy,
                AddedOn = entity.AddedOn,
                UpdatedBy = entity.UpdatedBy,
                updatedOn = entity.updatedOn
            };
            customerSonRepo.Delete(cus, entity.CUSTOMER_SON_ID);
            return true;
        }

        public Task<bool> DeleteAsync(CustomerSonVM entity)
        {
            return Task.Run<bool>(() =>
            {
                CustomerSon cus = new CustomerSon
                {
                    CUSTOMER_ID = entity.CUSTOMER_ID,
                    CUSTOMER_SON_ID = entity.CUSTOMER_SON_ID,
                    CUSTOMER_BIRTHDATE = entity.CUSTOMER_BIRTHDATE,
                    CUSTOMER_SON_NAME = entity.CUSTOMER_SON_NAME,
                    CUSTOMER_SON_ORDER = entity.CUSTOMER_SON_ORDER,
                    CUSTOMER_SON_SEX = entity.CUSTOMER_SON_SEX,
                    AddedBy = entity.AddedBy,
                    AddedOn = entity.AddedOn,
                    UpdatedBy = entity.UpdatedBy,
                    updatedOn = entity.updatedOn
                };
                customerSonRepo.Delete(cus, entity.CUSTOMER_SON_ID);
                return true;
            });
        }

        public void Dispose()
        {
            customerSonRepo.Dispose();
        }

        public List<CustomerSonVM> GetAll()
        {
            var q = from entity in customerSonRepo.GetAll()
                    select new CustomerSonVM
                    {
                        CUSTOMER_ID = entity.CUSTOMER_ID,
                        CUSTOMER_SON_ID = entity.CUSTOMER_SON_ID,
                        CUSTOMER_BIRTHDATE = entity.CUSTOMER_BIRTHDATE,
                        CUSTOMER_SON_NAME = entity.CUSTOMER_SON_NAME,
                        CUSTOMER_SON_ORDER = entity.CUSTOMER_SON_ORDER,
                        CUSTOMER_SON_SEX = entity.CUSTOMER_SON_SEX,
                        AddedBy = entity.AddedBy,
                        AddedOn = entity.AddedOn,
                        UpdatedBy = entity.UpdatedBy,
                        updatedOn = entity.updatedOn
                    };
            return q.ToList();
        }

        public Task<List<CustomerSonVM>> GetAllAsync(int pageNum, int pageSize)
        {
            return Task.Run<List<CustomerSonVM>>(() =>
            {
                int rowCount;
                var q = from entity in customerSonRepo.GetPaged<long>(pageNum, pageSize, p => p.CUSTOMER_SON_ID, false, out rowCount)
                        select new CustomerSonVM
                        {
                            CUSTOMER_ID = entity.CUSTOMER_ID,
                            CUSTOMER_SON_ID = entity.CUSTOMER_SON_ID,
                            CUSTOMER_BIRTHDATE = entity.CUSTOMER_BIRTHDATE,
                            CUSTOMER_SON_NAME = entity.CUSTOMER_SON_NAME,
                            CUSTOMER_SON_ORDER = entity.CUSTOMER_SON_ORDER,
                            CUSTOMER_SON_SEX = entity.CUSTOMER_SON_SEX,
                            AddedBy = entity.AddedBy,
                            AddedOn = entity.AddedOn,
                            UpdatedBy = entity.UpdatedBy,
                            updatedOn = entity.updatedOn
                        };
                return q.ToList();
            });
        }

        public bool Insert(CustomerSonVM entity)
        {
            CustomerSon cus = new CustomerSon
            {
                CUSTOMER_ID = entity.CUSTOMER_ID,
                CUSTOMER_SON_ID = entity.CUSTOMER_SON_ID,
                CUSTOMER_BIRTHDATE = entity.CUSTOMER_BIRTHDATE,
                CUSTOMER_SON_NAME = entity.CUSTOMER_SON_NAME,
                CUSTOMER_SON_ORDER = entity.CUSTOMER_SON_ORDER,
                CUSTOMER_SON_SEX = entity.CUSTOMER_SON_SEX,
                AddedBy = entity.AddedBy,
                AddedOn = entity.AddedOn,
                UpdatedBy = entity.UpdatedBy,
                updatedOn = entity.updatedOn
            };
            customerSonRepo.Add(cus);
            return true;
        }

        public Task<bool> InsertAsync(CustomerSonVM entity)
        {
            return Task.Run<bool>(() =>
            {
                CustomerSon cus = new CustomerSon
                {
                    CUSTOMER_ID = entity.CUSTOMER_ID,
                    CUSTOMER_SON_ID = entity.CUSTOMER_SON_ID,
                    CUSTOMER_BIRTHDATE = entity.CUSTOMER_BIRTHDATE,
                    CUSTOMER_SON_NAME = entity.CUSTOMER_SON_NAME,
                    CUSTOMER_SON_ORDER = entity.CUSTOMER_SON_ORDER,
                    CUSTOMER_SON_SEX = entity.CUSTOMER_SON_SEX,
                    AddedBy = entity.AddedBy,
                    AddedOn = entity.AddedOn,
                    UpdatedBy = entity.UpdatedBy,
                    updatedOn = entity.updatedOn
                };
                customerSonRepo.Add(cus);
                return true;
            });
        }

        public bool Update(CustomerSonVM entity)
        {
            CustomerSon cus = new CustomerSon
            {
                CUSTOMER_ID = entity.CUSTOMER_ID,
                CUSTOMER_SON_ID = entity.CUSTOMER_SON_ID,
                CUSTOMER_BIRTHDATE = entity.CUSTOMER_BIRTHDATE,
                CUSTOMER_SON_NAME = entity.CUSTOMER_SON_NAME,
                CUSTOMER_SON_ORDER = entity.CUSTOMER_SON_ORDER,
                CUSTOMER_SON_SEX = entity.CUSTOMER_SON_SEX,
                AddedBy = entity.AddedBy,
                AddedOn = entity.AddedOn,
                UpdatedBy = entity.UpdatedBy,
                updatedOn = entity.updatedOn
            };
            customerSonRepo.Update(cus, cus.CUSTOMER_SON_ID);
            return true;
        }

        public Task<bool> UpdateAsync(CustomerSonVM entity)
        {
            return Task.Run<bool>(() =>
            {
                CustomerSon cus = new CustomerSon
                {
                    CUSTOMER_ID = entity.CUSTOMER_ID,
                    CUSTOMER_SON_ID = entity.CUSTOMER_SON_ID,
                    CUSTOMER_BIRTHDATE = entity.CUSTOMER_BIRTHDATE,
                    CUSTOMER_SON_NAME = entity.CUSTOMER_SON_NAME,
                    CUSTOMER_SON_ORDER = entity.CUSTOMER_SON_ORDER,
                    CUSTOMER_SON_SEX = entity.CUSTOMER_SON_SEX,
                    AddedBy = entity.AddedBy,
                    AddedOn = entity.AddedOn,
                    UpdatedBy = entity.UpdatedBy,
                    updatedOn = entity.updatedOn
                };
                customerSonRepo.Update(cus, cus.CUSTOMER_SON_ID);
                return true;
            });
        }
    }
}
