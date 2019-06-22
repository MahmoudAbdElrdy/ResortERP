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
    public class KestOptionService : IKestOptionService, IDisposable
    {
        IGenericRepository<KEST_OPTION> kestOptRepo;
        public KestOptionService(IGenericRepository<KEST_OPTION> KEST_OPTIONRepo)
        {
            this.kestOptRepo = KEST_OPTIONRepo;
        }

        public Task<int> CountAsync()
        {
            return Task.Run<int>(() =>
            {
                return kestOptRepo.CountAsync();
            });
        }

        public bool Delete(Kest_OptionVM entity)
        {
            KEST_OPTION accType = new KEST_OPTION
            {
                option_id = entity.option_id
            };
            kestOptRepo.Delete(accType, entity.option_id);
            return true;
        }

        public Task<bool> DeleteAsync(Kest_OptionVM entity)
        {
            return Task.Run<bool>(() =>
            {
                KEST_OPTION accType = new KEST_OPTION
                {
                    option_id = entity.option_id
                };
                kestOptRepo.Delete(accType, entity.option_id);
                return true;
            });
        }

        public void Dispose()
        {
            kestOptRepo.Dispose();
        }

        public List<Kest_OptionVM> GetAll()
        {
            var q = from entity in kestOptRepo.GetAll()
                    select new Kest_OptionVM
                    {
                        option_id = entity.option_id,
                        account_code = entity.account_code,
                        account_id = entity.account_id,
                        account_name = entity.account_name,
                        Disable = entity.Disable,
                        AddedBy = entity.AddedBy,
                        AddedOn = entity.AddedOn,
                        UpdatedOn = entity.UpdatedOn,
                        UpdatedBy = entity.UpdatedBy
                    };
            return q.ToList();
        }

        

        public Task<List<Kest_OptionVM>> GetAllAsync(int pageNum, int pageSize)
        {
            return Task.Run<List<Kest_OptionVM>>(() =>
            {
                int rowCount;
                var q = from entity in kestOptRepo.GetPaged<long>(pageNum, pageSize, p => p.option_id, false, out rowCount)
                        select new Kest_OptionVM
                        {
                            option_id = entity.option_id,
                            account_code = entity.account_code,
                            account_id = entity.account_id,
                            account_name = entity.account_name,
                            Disable = entity.Disable,
                            AddedBy = entity.AddedBy,
                            AddedOn = entity.AddedOn,
                            UpdatedOn = entity.UpdatedOn,
                            UpdatedBy = entity.UpdatedBy
                        };
                return q.ToList();
            });
        }

        public bool Insert(Kest_OptionVM entity)
        {
            KEST_OPTION accType = new KEST_OPTION
            {
                option_id = entity.option_id,
                account_code = entity.account_code,
                account_id = entity.account_id,
                account_name = entity.account_name,
                Disable = entity.Disable,
                AddedBy = entity.AddedBy,
                AddedOn = entity.AddedOn,
                UpdatedOn = entity.UpdatedOn,
                UpdatedBy = entity.UpdatedBy
            };

            kestOptRepo.Add(accType);
            return true;
        }

        public Task<bool> InsertAsync(Kest_OptionVM entity)
        {
            return Task.Run<bool>(() =>
            {
                KEST_OPTION accType = new KEST_OPTION
                {
                    option_id = entity.option_id,
                    account_code = entity.account_code,
                    account_id = entity.account_id,
                    account_name = entity.account_name,
                    Disable = entity.Disable,
                    AddedBy = entity.AddedBy,
                    AddedOn = entity.AddedOn,
                    UpdatedOn = entity.UpdatedOn,
                    UpdatedBy = entity.UpdatedBy
                };
                kestOptRepo.Add(accType);
                return true;
            });
        }

        public bool Update(Kest_OptionVM entity)
        {
            KEST_OPTION accType = new KEST_OPTION
            {
                option_id = entity.option_id,
                account_code = entity.account_code,
                account_id = entity.account_id,
                account_name = entity.account_name,
                Disable = entity.Disable,
                AddedBy = entity.AddedBy,
                AddedOn = entity.AddedOn,
                UpdatedOn = entity.UpdatedOn,
                UpdatedBy = entity.UpdatedBy
            };
            kestOptRepo.Update(accType, accType.option_id);
            return true;
        }

        public Task<bool> UpdateAsync(Kest_OptionVM entity)
        {
            return Task.Run<bool>(() =>
            {
                KEST_OPTION accType = new KEST_OPTION
                {
                    option_id = entity.option_id,
                    account_code = entity.account_code,
                    account_id = entity.account_id,
                    account_name = entity.account_name,
                    Disable = entity.Disable,
                    AddedBy = entity.AddedBy,
                    AddedOn = entity.AddedOn,
                    UpdatedOn = entity.UpdatedOn,
                    UpdatedBy = entity.UpdatedBy
                };
                kestOptRepo.Update(accType, accType.option_id);
                return true;
            });
        }
    }
}
