using ResortERP.Core;
using ResortERP.Core.VM;
using ResortERP.Repository;
using ResortERP.Service.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResortERP.Service.Services
{
    public class IncomeAccountTypesService : IIncomeAccountTypesService, IDisposable
    {
        IGenericRepository<Income_Account_Types> incomeAccTypRepo;
        public IncomeAccountTypesService(IGenericRepository<Income_Account_Types> Income_Account_TypesRepo)
        {
            this.incomeAccTypRepo = Income_Account_TypesRepo;
        }

        public Task<int> CountAsync()
        {
            return Task.Run<int>(() =>
            {
                return incomeAccTypRepo.CountAsync();
            });
        }

        public bool Delete(Income_Account_TypesVM entity)
        {
            Income_Account_Types accType = new Income_Account_Types
            {
                ID = entity.ID
            };
            incomeAccTypRepo.Delete(accType, entity.ID);
            return true;
        }

        public Task<bool> DeleteAsync(Income_Account_TypesVM entity)
        {
            return Task.Run<bool>(() =>
            {
                Income_Account_Types accType = new Income_Account_Types
                {
                    ID = entity.ID
                };
                incomeAccTypRepo.Delete(accType, entity.ID);
                return true;
            });
        }

        public void Dispose()
        {
            incomeAccTypRepo.Dispose();
        }

        public List<Income_Account_TypesVM> GetAll()
        {
            var q = from entity in incomeAccTypRepo.GetAll()
                    select new Income_Account_TypesVM
                    {
                        ID = entity.ID,
                        NameAr = entity.NameAr,
                        NameEn = entity.NameEn,
                        AddedBy = entity.AddedBy,
                        AddedOn = entity.AddedOn,
                        disable = entity.disable,
                        UpdatedOn = entity.UpdatedOn,
                        UpdatedBy = entity.UpdatedBy
                    };
            return q.ToList();
        }



        public Task<List<Income_Account_TypesVM>> GetAllAsync(int pageNum, int pageSize)
        {
            return Task.Run<List<Income_Account_TypesVM>>(() =>
            {
                int rowCount;
                var q = from entity in incomeAccTypRepo.GetPaged<long>(pageNum, pageSize, p => p.ID, false, out rowCount)
                        select new Income_Account_TypesVM
                        {
                            ID = entity.ID,
                            NameAr = entity.NameAr,
                            NameEn = entity.NameEn,
                            AddedBy = entity.AddedBy,
                            AddedOn = entity.AddedOn,
                            disable = entity.disable,
                            UpdatedOn = entity.UpdatedOn,
                            UpdatedBy = entity.UpdatedBy
                        };
                return q.ToList();
            });
        }

        public bool Insert(Income_Account_TypesVM entity)
        {
            Income_Account_Types accType = new Income_Account_Types
            {
                ID = entity.ID,
                NameAr = entity.NameAr,
                NameEn = entity.NameEn,
                AddedBy = entity.AddedBy,
                AddedOn = entity.AddedOn,
                disable = entity.disable,
                UpdatedOn = entity.UpdatedOn,
                UpdatedBy = entity.UpdatedBy
            };

            incomeAccTypRepo.Add(accType);
            return true;
        }

        public Task<bool> InsertAsync(Income_Account_TypesVM entity)
        {
            return Task.Run<bool>(() =>
            {
                Income_Account_Types accType = new Income_Account_Types
                {
                    ID = entity.ID,
                    NameAr = entity.NameAr,
                    NameEn = entity.NameEn,
                    AddedBy = entity.AddedBy,
                    AddedOn = entity.AddedOn,
                    disable = entity.disable,
                    UpdatedOn = entity.UpdatedOn,
                    UpdatedBy = entity.UpdatedBy
                };
                incomeAccTypRepo.Add(accType);
                return true;
            });
        }

        public bool Update(Income_Account_TypesVM entity)
        {
            Income_Account_Types accType = new Income_Account_Types
            {
                ID = entity.ID,
                NameAr = entity.NameAr,
                NameEn = entity.NameEn,
                AddedBy = entity.AddedBy,
                AddedOn = entity.AddedOn,
                disable = entity.disable,
                UpdatedOn = entity.UpdatedOn,
                UpdatedBy = entity.UpdatedBy
            };
            incomeAccTypRepo.Update(accType, accType.ID);
            return true;
        }

        public Task<bool> UpdateAsync(Income_Account_TypesVM entity)
        {
            return Task.Run<bool>(() =>
            {
                Income_Account_Types accType = new Income_Account_Types
                {
                    ID = entity.ID,
                    NameAr = entity.NameAr,
                    NameEn = entity.NameEn,
                    AddedBy = entity.AddedBy,
                    AddedOn = entity.AddedOn,
                    disable = entity.disable,
                    UpdatedOn = entity.UpdatedOn,
                    UpdatedBy = entity.UpdatedBy
                };
                incomeAccTypRepo.Update(accType, accType.ID);
                return true;
            });
        }
    }
}
