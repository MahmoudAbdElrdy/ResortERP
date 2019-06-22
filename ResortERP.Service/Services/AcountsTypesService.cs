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
    public class AcountsTypesService : IAcountsTypesService, IDisposable
    {
        IGenericRepository<ACCOUNTS_TYPES> _ACCOUNTS_TYPESRepo;
        public AcountsTypesService(IGenericRepository<ACCOUNTS_TYPES> ACCOUNTS_TYPESRepo)
        {
            this._ACCOUNTS_TYPESRepo = ACCOUNTS_TYPESRepo;
        }

        public Task<int> CountAsync()
        {
            return Task.Run<int>(() =>
            {
                return _ACCOUNTS_TYPESRepo.CountAsync();
            });
        }

        public bool Delete(AccountTypeVM entity)
        {
            ACCOUNTS_TYPES accType = new ACCOUNTS_TYPES
            {
                ACC_TYPE_ID = entity.ACC_TYPE_ID
            };
            _ACCOUNTS_TYPESRepo.Delete(accType, entity.ACC_TYPE_ID);
            return true;
        }

        public Task<bool> DeleteAsync(AccountTypeVM entity)
        {
            return Task.Run<bool>(() =>
            {
                ACCOUNTS_TYPES accType = new ACCOUNTS_TYPES
                {
                    ACC_TYPE_ID = entity.ACC_TYPE_ID
                };
                _ACCOUNTS_TYPESRepo.Delete(accType, entity.ACC_TYPE_ID);
                return true;
            });
        }

        public void Dispose()
        {
            _ACCOUNTS_TYPESRepo.Dispose();
        }

        public List<AccountTypeVM> GetAll()
        {
            var q = from entity in _ACCOUNTS_TYPESRepo.GetAll()
                    select new AccountTypeVM
                    {
                        ACC_TYPE_ID = entity.ACC_TYPE_ID,
                        AddedBy = entity.AddedBy,
                        AddedOn = entity.AddedOn,
                        ACC_TYPE_AR_NAME = entity.ACC_TYPE_AR_NAME,
                        ACC_TYPE_EN_NAME = entity.ACC_TYPE_EN_NAME,
                        disable = entity.disable,
                        UpdatedOn = entity.UpdatedOn,
                        UpdatedBy = entity.UpdatedBy
                    };
            return q.ToList();
        }

        public List<AccountTypeVM> GetCustomerSupplier(char type)
        {
            if (type == 'c')
            {
                var q = from entity in _ACCOUNTS_TYPESRepo.GetAll().Where(x => (x.ACC_TYPE_ID == 5 || x.ACC_TYPE_ID == 7))
                        select new AccountTypeVM
                        {
                            ACC_TYPE_ID = entity.ACC_TYPE_ID,
                            AddedBy = entity.AddedBy,
                            AddedOn = entity.AddedOn,
                            ACC_TYPE_AR_NAME = entity.ACC_TYPE_AR_NAME,
                            ACC_TYPE_EN_NAME = entity.ACC_TYPE_EN_NAME,
                            disable = entity.disable,
                            UpdatedOn = entity.UpdatedOn,
                            UpdatedBy = entity.UpdatedBy
                        };
                return q.ToList();
            }
            else if (type == 's')
            {
                var q = from entity in _ACCOUNTS_TYPESRepo.GetAll().Where(x => (x.ACC_TYPE_ID == 6 || x.ACC_TYPE_ID == 7))
                        select new AccountTypeVM
                        {
                            ACC_TYPE_ID = entity.ACC_TYPE_ID,
                            AddedBy = entity.AddedBy,
                            AddedOn = entity.AddedOn,
                            ACC_TYPE_AR_NAME = entity.ACC_TYPE_AR_NAME,
                            ACC_TYPE_EN_NAME = entity.ACC_TYPE_EN_NAME,
                            disable = entity.disable,
                            UpdatedOn = entity.UpdatedOn,
                            UpdatedBy = entity.UpdatedBy
                        };
                return q.ToList();
            }
            else { return null; }

        }

        public Task<List<AccountTypeVM>> GetAllAsync(int pageNum, int pageSize)
        {
            return Task.Run<List<AccountTypeVM>>(() =>
            {
                int rowCount;
                var q = from entity in _ACCOUNTS_TYPESRepo.GetPaged<long>(pageNum, pageSize, p => p.ACC_TYPE_ID, false, out rowCount)
                        select new AccountTypeVM
                        {
                            ACC_TYPE_ID = entity.ACC_TYPE_ID,
                            AddedBy = entity.AddedBy,
                            AddedOn = entity.AddedOn,
                            ACC_TYPE_AR_NAME = entity.ACC_TYPE_AR_NAME,
                            ACC_TYPE_EN_NAME = entity.ACC_TYPE_EN_NAME,
                            disable = entity.disable,
                            UpdatedOn = entity.UpdatedOn,
                            UpdatedBy = entity.UpdatedBy
                        };
                return q.ToList();
            });
        }

        public bool Insert(AccountTypeVM entity)
        {
            ACCOUNTS_TYPES accType = new ACCOUNTS_TYPES
            {
                ACC_TYPE_ID = entity.ACC_TYPE_ID,
                AddedBy = entity.AddedBy,
                AddedOn = entity.AddedOn,
                ACC_TYPE_AR_NAME = entity.ACC_TYPE_AR_NAME,
                ACC_TYPE_EN_NAME = entity.ACC_TYPE_EN_NAME,
                disable = entity.disable,
                UpdatedOn = entity.UpdatedOn,
                UpdatedBy = entity.UpdatedBy
            };

            _ACCOUNTS_TYPESRepo.Add(accType);
            return true;
        }

        public Task<int> InsertAsync(AccountTypeVM entity)
        {
            return Task.Run<int>(() =>
            {
                ACCOUNTS_TYPES accType = new ACCOUNTS_TYPES
                {
                    ACC_TYPE_ID = entity.ACC_TYPE_ID,
                    AddedBy = entity.AddedBy,
                    AddedOn = entity.AddedOn,
                    ACC_TYPE_AR_NAME = entity.ACC_TYPE_AR_NAME,
                    ACC_TYPE_EN_NAME = entity.ACC_TYPE_EN_NAME,
                    disable = entity.disable,
                    UpdatedOn = entity.UpdatedOn,
                    UpdatedBy = entity.UpdatedBy
                };
                _ACCOUNTS_TYPESRepo.Add(accType);
                return accType.ACC_TYPE_ID;
            });
        }

        public bool Update(AccountTypeVM entity)
        {
            ACCOUNTS_TYPES accType = new ACCOUNTS_TYPES
            {
                ACC_TYPE_ID = entity.ACC_TYPE_ID,
                AddedBy = entity.AddedBy,
                AddedOn = entity.AddedOn,
                ACC_TYPE_AR_NAME = entity.ACC_TYPE_AR_NAME,
                ACC_TYPE_EN_NAME = entity.ACC_TYPE_EN_NAME,
                disable = entity.disable,
                UpdatedOn = entity.UpdatedOn,
                UpdatedBy = entity.UpdatedBy
            };
            _ACCOUNTS_TYPESRepo.Update(accType, accType.ACC_TYPE_ID);
            return true;
        }

        public Task<bool> UpdateAsync(AccountTypeVM entity)
        {
            return Task.Run<bool>(() =>
            {
                ACCOUNTS_TYPES accType = new ACCOUNTS_TYPES
                {
                    ACC_TYPE_ID = entity.ACC_TYPE_ID,
                    AddedBy = entity.AddedBy,
                    AddedOn = entity.AddedOn,
                    ACC_TYPE_AR_NAME = entity.ACC_TYPE_AR_NAME,
                    ACC_TYPE_EN_NAME = entity.ACC_TYPE_EN_NAME,
                    disable = entity.disable,
                    UpdatedOn = entity.UpdatedOn,
                    UpdatedBy = entity.UpdatedBy
                };
                _ACCOUNTS_TYPESRepo.Update(accType, accType.ACC_TYPE_ID);
                return true;
            });
        }


        public List<AccountTypeVM> GetByTypeQueryString(char type)
        {
            if (type == 'c' )
            {
                var q = from entity in _ACCOUNTS_TYPESRepo.GetAll().Where(a => a.ACC_TYPE_ID == 5 || a.ACC_TYPE_ID == 7)
                        select new AccountTypeVM
                        {
                            ACC_TYPE_ID = entity.ACC_TYPE_ID,
                            AddedBy = entity.AddedBy,
                            AddedOn = entity.AddedOn,
                            ACC_TYPE_AR_NAME = entity.ACC_TYPE_AR_NAME,
                            ACC_TYPE_EN_NAME = entity.ACC_TYPE_EN_NAME,
                            disable = entity.disable,
                            UpdatedOn = entity.UpdatedOn,
                            UpdatedBy = entity.UpdatedBy
                        };

                return q.ToList();
            }
            else if (type == 's')
            {
                var q = from entity in _ACCOUNTS_TYPESRepo.GetAll().Where(a => a.ACC_TYPE_ID == 6 || a.ACC_TYPE_ID == 7)
                        select new AccountTypeVM
                        {
                            ACC_TYPE_ID = entity.ACC_TYPE_ID,
                            AddedBy = entity.AddedBy,
                            AddedOn = entity.AddedOn,
                            ACC_TYPE_AR_NAME = entity.ACC_TYPE_AR_NAME,
                            ACC_TYPE_EN_NAME = entity.ACC_TYPE_EN_NAME,
                            disable = entity.disable,
                            UpdatedOn = entity.UpdatedOn,
                            UpdatedBy = entity.UpdatedBy
                        };

                return q.ToList();
            }
            else if (type == 'w')
            {
                var q = from entity in _ACCOUNTS_TYPESRepo.GetAll().Where(a => a.ACC_TYPE_ID == 21)
                        select new AccountTypeVM
                        {
                            ACC_TYPE_ID = entity.ACC_TYPE_ID,
                            AddedBy = entity.AddedBy,
                            AddedOn = entity.AddedOn,
                            ACC_TYPE_AR_NAME = entity.ACC_TYPE_AR_NAME,
                            ACC_TYPE_EN_NAME = entity.ACC_TYPE_EN_NAME,
                            disable = entity.disable,
                            UpdatedOn = entity.UpdatedOn,
                            UpdatedBy = entity.UpdatedBy
                        };

                return q.ToList();

            }
            else
            {
                var q = from entity in _ACCOUNTS_TYPESRepo.GetAll()
                        select new AccountTypeVM
                        {
                            ACC_TYPE_ID = entity.ACC_TYPE_ID,
                            AddedBy = entity.AddedBy,
                            AddedOn = entity.AddedOn,
                            ACC_TYPE_AR_NAME = entity.ACC_TYPE_AR_NAME,
                            ACC_TYPE_EN_NAME = entity.ACC_TYPE_EN_NAME,
                            disable = entity.disable,
                            UpdatedOn = entity.UpdatedOn,
                            UpdatedBy = entity.UpdatedBy
                        };

                return q.ToList();
            }
        }
    }
}
