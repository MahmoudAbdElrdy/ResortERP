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
    public class TelephoneTypeService : ITelephoneTypeService, IDisposable
    {
        IGenericRepository<TelephoneTypes> telephoneTypeRepo;

        public TelephoneTypeService(IGenericRepository<TelephoneTypes> telephoneTypeRepo)
        {
            this.telephoneTypeRepo = telephoneTypeRepo;
        }

        public Task<int> CountAsync()
        {
            return Task.Run<int>(() =>
            {
                return telephoneTypeRepo.CountAsync();
            });
        }

        public bool Delete(TelephoneTypesVM entity)
        {
            TelephoneTypes telTyp = new TelephoneTypes()
            {
                TELE_TYPE_ID = entity.TELE_TYPE_ID,
                DATABASE_TABLE = entity.DATABASE_TABLE,
                TELE_TYPE_AR_NAME = entity.TELE_TYPE_AR_NAME,
                TELE_TYPE_EN_NAME = entity.TELE_TYPE_EN_NAME,
                AddedBy = entity.AddedBy,
                AddedOn = entity.AddedOn,
                Disable = entity.Disable,
                UpdatedBy = entity.UpdatedBy,
                updatedOn = entity.updatedOn
            };
            telephoneTypeRepo.Delete(telTyp, entity.TELE_TYPE_ID);
            return true;
        }

        public Task<bool> DeleteAsync(TelephoneTypesVM entity)
        {
            return Task.Run<bool>(() =>
            {
                TelephoneTypes telTyp = new TelephoneTypes()
                {
                    TELE_TYPE_ID = entity.TELE_TYPE_ID,
                    DATABASE_TABLE = entity.DATABASE_TABLE,
                    TELE_TYPE_AR_NAME = entity.TELE_TYPE_AR_NAME,
                    TELE_TYPE_EN_NAME = entity.TELE_TYPE_EN_NAME,
                    AddedBy = entity.AddedBy,
                    AddedOn = entity.AddedOn,
                    Disable = entity.Disable,
                    UpdatedBy = entity.UpdatedBy,
                    updatedOn = entity.updatedOn
                };
                telephoneTypeRepo.Delete(telTyp, entity.TELE_TYPE_ID);
                return true;
            });
        }

        public void Dispose()
        {
            telephoneTypeRepo.Dispose();
        }

        public List<TelephoneTypesVM> GetAll()
        {
            var q = from entity in telephoneTypeRepo.GetAll()
                    select new TelephoneTypesVM
                    {
                        TELE_TYPE_ID = entity.TELE_TYPE_ID,
                        DATABASE_TABLE = entity.DATABASE_TABLE,
                        TELE_TYPE_AR_NAME = entity.TELE_TYPE_AR_NAME,
                        TELE_TYPE_EN_NAME = entity.TELE_TYPE_EN_NAME,
                        AddedBy = entity.AddedBy,
                        AddedOn = entity.AddedOn,
                        Disable = entity.Disable,
                        UpdatedBy = entity.UpdatedBy,
                        updatedOn = entity.updatedOn
                    };
            return q.ToList();
        }

        public Task<List<TelephoneTypesVM>> GetAllAsync(int pageNum, int pageSize)
        {
            return Task.Run(() =>
            {
                int rowCount;
                var q = from entity in telephoneTypeRepo.GetPaged<int>(pageNum, pageSize, p => p.TELE_TYPE_ID, false, out rowCount)
                        select new TelephoneTypesVM
                        {
                            TELE_TYPE_ID = entity.TELE_TYPE_ID,
                            DATABASE_TABLE = entity.DATABASE_TABLE,
                            TELE_TYPE_AR_NAME = entity.TELE_TYPE_AR_NAME,
                            TELE_TYPE_EN_NAME = entity.TELE_TYPE_EN_NAME,
                            AddedBy = entity.AddedBy,
                            AddedOn = entity.AddedOn,
                            Disable = entity.Disable,
                            UpdatedBy = entity.UpdatedBy,
                            updatedOn = entity.updatedOn
                        };
                return q.ToList();
            });
        }

        public bool Insert(TelephoneTypesVM entity)
        {
            TelephoneTypes telTyp = new TelephoneTypes()
            {
                TELE_TYPE_ID = entity.TELE_TYPE_ID,
                DATABASE_TABLE = entity.DATABASE_TABLE,
                TELE_TYPE_AR_NAME = entity.TELE_TYPE_AR_NAME,
                TELE_TYPE_EN_NAME = entity.TELE_TYPE_EN_NAME,
                AddedBy = entity.AddedBy,
                AddedOn = entity.AddedOn,
                Disable = entity.Disable,
                UpdatedBy = entity.UpdatedBy,
                updatedOn = entity.updatedOn
            };
            telephoneTypeRepo.Add(telTyp);
            return true;
        }

        public Task<int> InsertAsync(TelephoneTypesVM entity)
        {
            return Task.Run<int>(() =>
             {
                 TelephoneTypes telTyp = new TelephoneTypes()
                 {
                     TELE_TYPE_ID = entity.TELE_TYPE_ID,
                     DATABASE_TABLE = entity.DATABASE_TABLE,
                     TELE_TYPE_AR_NAME = entity.TELE_TYPE_AR_NAME,
                     TELE_TYPE_EN_NAME = entity.TELE_TYPE_EN_NAME,
                     AddedBy = entity.AddedBy,
                     AddedOn = entity.AddedOn,
                     Disable = entity.Disable,
                     UpdatedBy = entity.UpdatedBy,
                     updatedOn = entity.updatedOn
                 };
                 telephoneTypeRepo.Add(telTyp);
                 return entity.TELE_TYPE_ID;
             });
        }

        public bool Update(TelephoneTypesVM entity)
        {
            TelephoneTypes telTyp = new TelephoneTypes()
            {
                TELE_TYPE_ID = entity.TELE_TYPE_ID,
                DATABASE_TABLE = entity.DATABASE_TABLE,
                TELE_TYPE_AR_NAME = entity.TELE_TYPE_AR_NAME,
                TELE_TYPE_EN_NAME = entity.TELE_TYPE_EN_NAME,
                AddedBy = entity.AddedBy,
                AddedOn = entity.AddedOn,
                Disable = entity.Disable,
                UpdatedBy = entity.UpdatedBy,
                updatedOn = entity.updatedOn
            };
            telephoneTypeRepo.Update(telTyp, telTyp.TELE_TYPE_ID);
            return true;
        }

        public Task<bool> UpdateAsync(TelephoneTypesVM entity)
        {
            return Task.Run<bool>(() =>
            {
                TelephoneTypes telTyp = new TelephoneTypes()
                {
                    TELE_TYPE_ID = entity.TELE_TYPE_ID,
                    DATABASE_TABLE = entity.DATABASE_TABLE,
                    TELE_TYPE_AR_NAME = entity.TELE_TYPE_AR_NAME,
                    TELE_TYPE_EN_NAME = entity.TELE_TYPE_EN_NAME,
                    AddedBy = entity.AddedBy,
                    AddedOn = entity.AddedOn,
                    Disable = entity.Disable,
                    UpdatedBy = entity.UpdatedBy,
                    updatedOn = entity.updatedOn
                };
                telephoneTypeRepo.Update(telTyp, telTyp.TELE_TYPE_ID);
                return true;
            });
        }


    }
}
