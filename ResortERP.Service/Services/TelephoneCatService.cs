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
    public class TelephoneCatService : ITelephoneCatService, IDisposable
    {
        IGenericRepository<TelephonCat> telephoneCatRepo;

        public TelephoneCatService(IGenericRepository<TelephonCat> telephoneCatRepo)
        {
            this.telephoneCatRepo = telephoneCatRepo;
        }

        public Task<int> CountAsync()
        {
            return Task.Run<int>(() =>
            {
                return telephoneCatRepo.CountAsync();
            });
        }

        public bool Delete(TelephoneCatVM entity)
        {
            TelephonCat telCat = new TelephonCat()
            {
                TELE_CAT_ID = entity.TELE_CAT_ID,
                TELE_CAT_AR_NAME = entity.TELE_CAT_AR_NAME,
                TELE_CAT_EN_NAME = entity.TELE_CAT_EN_NAME,
                AddedBy = entity.AddedBy,
                AddedOn = entity.AddedOn,
                UpdatedBy = entity.UpdatedBy,
                updatedOn = entity.updatedOn
            };
            telephoneCatRepo.Delete(telCat, entity.TELE_CAT_ID);
            return true;
        }

        public Task<bool> DeleteAsync(TelephoneCatVM entity)
        {
            return Task.Run<bool>(() =>
            {
                TelephonCat telCat = new TelephonCat()
                {
                    TELE_CAT_ID = entity.TELE_CAT_ID,
                    TELE_CAT_AR_NAME = entity.TELE_CAT_AR_NAME,
                    TELE_CAT_EN_NAME = entity.TELE_CAT_EN_NAME,
                    AddedBy = entity.AddedBy,
                    AddedOn = entity.AddedOn,
                    UpdatedBy = entity.UpdatedBy,
                    updatedOn = entity.updatedOn
                };
                telephoneCatRepo.Delete(telCat, entity.TELE_CAT_ID);
                return true;
            });
        }

        public void Dispose()
        {
            telephoneCatRepo.Dispose();
        }

        public List<TelephoneCatVM> GetAll()
        {
            var q = from entity in telephoneCatRepo.GetAll()
                    select new TelephoneCatVM
                    {
                        TELE_CAT_ID = entity.TELE_CAT_ID,
                        TELE_CAT_AR_NAME = entity.TELE_CAT_AR_NAME,
                        TELE_CAT_EN_NAME = entity.TELE_CAT_EN_NAME,
                        AddedBy = entity.AddedBy,
                        AddedOn = entity.AddedOn,
                        UpdatedBy = entity.UpdatedBy,
                        updatedOn = entity.updatedOn
                    };
            return q.ToList();
        }

        public Task<List<TelephoneCatVM>> GetAllAsync(int pageNum, int pageSize)
        {
            return Task.Run(() =>
            {
                int rowCount;
                var q = from entity in telephoneCatRepo.GetPaged<int>(pageNum, pageSize, p => p.TELE_CAT_ID, false, out rowCount)
                        select new TelephoneCatVM
                        {
                            TELE_CAT_ID = entity.TELE_CAT_ID,
                            TELE_CAT_AR_NAME = entity.TELE_CAT_AR_NAME,
                            TELE_CAT_EN_NAME = entity.TELE_CAT_EN_NAME,
                            AddedBy = entity.AddedBy,
                            AddedOn = entity.AddedOn,
                            UpdatedBy = entity.UpdatedBy,
                            updatedOn = entity.updatedOn
                        };
                return q.ToList();
            });
        }

        public bool Insert(TelephoneCatVM entity)
        {
            TelephonCat telCat = new TelephonCat()
            {
                TELE_CAT_ID = entity.TELE_CAT_ID,
                TELE_CAT_AR_NAME = entity.TELE_CAT_AR_NAME,
                TELE_CAT_EN_NAME = entity.TELE_CAT_EN_NAME,
                AddedBy = entity.AddedBy,
                AddedOn = entity.AddedOn,
                UpdatedBy = entity.UpdatedBy,
                updatedOn = entity.updatedOn
            };
            telephoneCatRepo.Add(telCat);
            return true;
        }

        public Task<int> InsertAsync(TelephoneCatVM entity)
        {
            return Task.Run<int>(() =>
             {
                 TelephonCat telCat = new TelephonCat()
                 {
                     TELE_CAT_ID = entity.TELE_CAT_ID,
                     TELE_CAT_AR_NAME = entity.TELE_CAT_AR_NAME,
                     TELE_CAT_EN_NAME = entity.TELE_CAT_EN_NAME,
                     AddedBy = entity.AddedBy,
                     AddedOn = entity.AddedOn,
                     UpdatedBy = entity.UpdatedBy,
                     updatedOn = entity.updatedOn
                 };
                 telephoneCatRepo.Add(telCat);
                 return telCat.TELE_CAT_ID;
             });
        }

        public bool Update(TelephoneCatVM entity)
        {
            TelephonCat telCat = new TelephonCat()
            {
                TELE_CAT_ID = entity.TELE_CAT_ID,
                TELE_CAT_AR_NAME = entity.TELE_CAT_AR_NAME,
                TELE_CAT_EN_NAME = entity.TELE_CAT_EN_NAME,
                AddedBy = entity.AddedBy,
                AddedOn = entity.AddedOn,
                UpdatedBy = entity.UpdatedBy,
                updatedOn = entity.updatedOn
            };
            telephoneCatRepo.Update(telCat, telCat.TELE_CAT_ID);
            return true;
        }

        public Task<bool> UpdateAsync(TelephoneCatVM entity)
        {
            return Task.Run<bool>(() =>
            {
                TelephonCat telCat = new TelephonCat()
                {
                    TELE_CAT_ID = entity.TELE_CAT_ID,
                    TELE_CAT_AR_NAME = entity.TELE_CAT_AR_NAME,
                    TELE_CAT_EN_NAME = entity.TELE_CAT_EN_NAME,
                    AddedBy = entity.AddedBy,
                    AddedOn = entity.AddedOn,
                    UpdatedBy = entity.UpdatedBy,
                    updatedOn = entity.updatedOn
                };
                telephoneCatRepo.Update(telCat, telCat.TELE_CAT_ID);
                return true;
            });
        }




        public TelephoneCatVM GetByID(int TypeID)
        {
            var q = from entity in telephoneCatRepo.GetAll().Where(e => e.TELE_CAT_ID == TypeID)
                    select new TelephoneCatVM
                    {
                        TELE_CAT_ID = entity.TELE_CAT_ID,
                        TELE_CAT_AR_NAME = entity.TELE_CAT_AR_NAME,
                        TELE_CAT_EN_NAME = entity.TELE_CAT_EN_NAME,
                        AddedBy = entity.AddedBy,
                        AddedOn = entity.AddedOn,
                        UpdatedBy = entity.UpdatedBy,
                        updatedOn = entity.updatedOn
                    };
            return q.FirstOrDefault();
        }
    }
}
