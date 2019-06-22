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
    public class AcountsDetailsService : IAcountDetailsService, IDisposable
    {
        IGenericRepository<ACCOUNT_DETAILS> _ACCOUNT_DETAILSRepo;
        public AcountsDetailsService(IGenericRepository<ACCOUNT_DETAILS> ACCOUNT_DETAILSRepo)
        {
            this._ACCOUNT_DETAILSRepo = ACCOUNT_DETAILSRepo;
        }

        public Task<int> CountAsync()
        {
            return Task.Run<int>(() =>
            {
                return _ACCOUNT_DETAILSRepo.CountAsync();
            });
        }

        public bool Delete(AccountDetailVM entity)
        {
            ACCOUNT_DETAILS emp = new ACCOUNT_DETAILS
            {
                PARENT_ACC_ID = entity.PARENT_ACC_ID,
                AddedBy = entity.AddedBy,
                AddedOn = entity.AddedOn,
                CHILD_ACC_ID = entity.CHILD_ACC_ID,
                Disable = entity.Disable,
                PARTITIONING_FACTOR = entity.PARTITIONING_FACTOR,
                UpdatedBy = entity.UpdatedBy,
                UpdatedOn = entity.UpdatedOn

            };
            _ACCOUNT_DETAILSRepo.Delete(emp, entity.PARENT_ACC_ID);
            return true;
        }

        public Task<bool> DeleteAsync(AccountDetailVM entity)
        {
            return Task.Run<bool>(() =>
            {
                ACCOUNT_DETAILS emp = new ACCOUNT_DETAILS
                {
                    PARENT_ACC_ID = entity.PARENT_ACC_ID,
                    AddedBy = entity.AddedBy,
                    AddedOn = entity.AddedOn,
                    CHILD_ACC_ID = entity.CHILD_ACC_ID,
                    Disable = entity.Disable,
                    PARTITIONING_FACTOR = entity.PARTITIONING_FACTOR,
                    UpdatedBy = entity.UpdatedBy,
                    UpdatedOn = entity.UpdatedOn
                };
                _ACCOUNT_DETAILSRepo.Delete(emp, entity.PARENT_ACC_ID);
                return true;
            });
        }

        public void Dispose()
        {
            _ACCOUNT_DETAILSRepo.Dispose();
        }

        public List<AccountDetailVM> GetAll()
        {
            var q = from entity in _ACCOUNT_DETAILSRepo.GetAll()
                    select new AccountDetailVM
                    {
                        AddedBy = entity.AddedBy,
                        AddedOn = entity.AddedOn,
                        CHILD_ACC_ID = entity.CHILD_ACC_ID,
                        Disable = entity.Disable,
                        PARENT_ACC_ID = entity.PARENT_ACC_ID,
                        PARTITIONING_FACTOR = entity.PARTITIONING_FACTOR,
                        UpdatedBy = entity.UpdatedBy,
                        UpdatedOn = entity.UpdatedOn
                    };
            return q.ToList();
        }

        public Task<List<AccountDetailVM>> GetAllAsync(int pageNum, int pageSize)
        {
            return Task.Run<List<AccountDetailVM>>(() =>
            {
                int rowCount;
                var q = from entity in _ACCOUNT_DETAILSRepo.GetPaged<long>(pageNum, pageSize, p => p.PARENT_ACC_ID, false, out rowCount)
                        select new AccountDetailVM
                        {
                            AddedBy = entity.AddedBy,
                            AddedOn = entity.AddedOn,
                            CHILD_ACC_ID = entity.CHILD_ACC_ID,
                            Disable = entity.Disable,
                            PARENT_ACC_ID = entity.PARENT_ACC_ID,
                            PARTITIONING_FACTOR = entity.PARTITIONING_FACTOR,
                            UpdatedBy = entity.UpdatedBy,
                            UpdatedOn = entity.UpdatedOn
                        };
                return q.ToList();
            });
        }

        public bool Insert(AccountDetailVM entity)
        {
            ACCOUNT_DETAILS emp = new ACCOUNT_DETAILS
            {
                AddedBy = entity.AddedBy,
                AddedOn = entity.AddedOn,
                CHILD_ACC_ID = entity.CHILD_ACC_ID,
                Disable = entity.Disable,
                PARENT_ACC_ID = entity.PARENT_ACC_ID,
                PARTITIONING_FACTOR = entity.PARTITIONING_FACTOR,
                UpdatedBy = entity.UpdatedBy,
                UpdatedOn = entity.UpdatedOn
            };
            _ACCOUNT_DETAILSRepo.Add(emp);
            return true;
        }

        public Task<int> InsertAsync(AccountDetailVM entity)
        {
            return Task.Run<int>(() =>
            {
                ACCOUNT_DETAILS emp = new ACCOUNT_DETAILS
                {
                    AddedBy = entity.AddedBy,
                    AddedOn = entity.AddedOn,
                    CHILD_ACC_ID = entity.CHILD_ACC_ID,
                    Disable = entity.Disable,
                    PARENT_ACC_ID = entity.PARENT_ACC_ID,
                    PARTITIONING_FACTOR = entity.PARTITIONING_FACTOR,
                    UpdatedBy = entity.UpdatedBy,
                    UpdatedOn = entity.UpdatedOn
                };
                _ACCOUNT_DETAILSRepo.Add(emp);
                return emp.PARENT_ACC_ID;
            });
        }

        public bool Update(AccountDetailVM entity)
        {
            ACCOUNT_DETAILS emp = new ACCOUNT_DETAILS
            {
                AddedBy = entity.AddedBy,
                AddedOn = entity.AddedOn,
                CHILD_ACC_ID = entity.CHILD_ACC_ID,
                Disable = entity.Disable,
                PARENT_ACC_ID = entity.PARENT_ACC_ID,
                PARTITIONING_FACTOR = entity.PARTITIONING_FACTOR,
                UpdatedBy = entity.UpdatedBy,
                UpdatedOn = entity.UpdatedOn
            };
            _ACCOUNT_DETAILSRepo.Update(emp, emp.PARENT_ACC_ID);
            return true;
        }

        public Task<bool> UpdateAsync(AccountDetailVM entity)
        {
            return Task.Run<bool>(() =>
            {
                ACCOUNT_DETAILS emp = new ACCOUNT_DETAILS
                {
                    AddedBy = entity.AddedBy,
                    AddedOn = entity.AddedOn,
                    CHILD_ACC_ID = entity.CHILD_ACC_ID,
                    Disable = entity.Disable,
                    PARENT_ACC_ID = entity.PARENT_ACC_ID,
                    PARTITIONING_FACTOR = entity.PARTITIONING_FACTOR,
                    UpdatedBy = entity.UpdatedBy,
                    UpdatedOn = entity.UpdatedOn
                };
                _ACCOUNT_DETAILSRepo.Update(emp, emp.PARENT_ACC_ID);
                return true;
            });
        }
    }
}
