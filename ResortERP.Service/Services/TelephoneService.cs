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
    public class TelephoneService : ITelephoneService, IDisposable
    {
        IGenericRepository<Telephones> telephoneRepo;
        IGenericRepository<TelephonCat> teleCatRepo;

        public TelephoneService(IGenericRepository<Telephones> telephoneRepo, IGenericRepository<TelephonCat> teleCatRepo)
        {
            this.telephoneRepo = telephoneRepo;
            this.teleCatRepo = teleCatRepo;
        }

        public Task<int> CountAsync()
        {
            return Task.Run<int>(() =>
            {
                return telephoneRepo.CountAsync();
            });
        }

        public bool Delete(TelephoneVM entity)
        {
            Telephones tel = new Telephones()
            {
                TELE_ID = entity.TELE_ID,
                TELE_CAT_ID = entity.TELE_CAT_ID,
                TELE_NUMBER = entity.TELE_NUMBER,
                TELE_TYPE_ID = entity.TELE_TYPE_ID,
                AddedBy = entity.AddedBy,
                AddedOn = entity.AddedOn,
                UpdatedBy = entity.UpdatedBy,
                updatedOn = entity.updatedOn
            };
            object[] keys = new object[4] { entity.TELE_ID, entity.TELE_TYPE_ID, entity.TELE_NUMBER, entity.TELE_CAT_ID };
            telephoneRepo.DeleteComposite(tel, keys);
            return true;
        }

        public Task<bool> DeleteAsync(TelephoneVM entity)
        {
            return Task.Run<bool>(() =>
            {
                Telephones tel = new Telephones()
                {
                    TELE_ID = entity.TELE_ID,
                    TELE_CAT_ID = entity.TELE_CAT_ID,
                    TELE_NUMBER = entity.TELE_NUMBER,
                    TELE_TYPE_ID = entity.TELE_TYPE_ID,
                    AddedBy = entity.AddedBy,
                    AddedOn = entity.AddedOn,
                    UpdatedBy = entity.UpdatedBy,
                    updatedOn = entity.updatedOn
                };
                object[] keys = new object[4] { entity.TELE_ID, entity.TELE_TYPE_ID, entity.TELE_NUMBER, entity.TELE_CAT_ID };
                telephoneRepo.DeleteComposite(tel, keys);
                return true;
            });
        }

        public void Dispose()
        {
            telephoneRepo.Dispose();
            teleCatRepo.Dispose();
        }

        public List<TelephoneVM> GetAll()
        {
            var q = from entity in telephoneRepo.GetAll()
                    select new TelephoneVM
                    {
                        TELE_ID = entity.TELE_ID,
                        TELE_CAT_ID = entity.TELE_CAT_ID,
                        TELE_NUMBER = entity.TELE_NUMBER,
                        TELE_TYPE_ID = entity.TELE_TYPE_ID,
                        AddedBy = entity.AddedBy,
                        AddedOn = entity.AddedOn,
                        UpdatedBy = entity.UpdatedBy,
                        updatedOn = entity.updatedOn
                    };
            return q.ToList();
        }

        public List<TelephoneVM> GetBy(int ContactID, int TypeID)
        {
            var q = from entity in telephoneRepo.GetAll().Where(x => (x.TELE_ID == ContactID && x.TELE_TYPE_ID == TypeID))
                    join cat in teleCatRepo.GetAll() on entity.TELE_CAT_ID equals cat.TELE_CAT_ID into group1
                    from g1 in group1.ToList()
                    select new TelephoneVM
                    {
                        TELE_ID = entity.TELE_ID,
                        TELE_CAT_ID = entity.TELE_CAT_ID,
                        TELE_NUMBER = entity.TELE_NUMBER,
                        TELE_TYPE_ID = entity.TELE_TYPE_ID,
                        AddedBy = entity.AddedBy,
                        AddedOn = entity.AddedOn,
                        UpdatedBy = entity.UpdatedBy,
                        updatedOn = entity.updatedOn,
                        TeleCatName= g1.TELE_CAT_AR_NAME
                    };
            return q.ToList();
        }

        public Task<List<TelephoneVM>> GetAllAsync(int pageNum, int pageSize)
        {
            return Task.Run(() =>
            {
                int rowCount;
                var q = from entity in telephoneRepo.GetPaged<int>(pageNum, pageSize, p => p.TELE_ID, false, out rowCount)
                        select new TelephoneVM
                        {
                            TELE_ID = entity.TELE_ID,
                            TELE_CAT_ID = entity.TELE_CAT_ID,
                            TELE_NUMBER = entity.TELE_NUMBER,
                            TELE_TYPE_ID = entity.TELE_TYPE_ID,
                            AddedBy = entity.AddedBy,
                            AddedOn = entity.AddedOn,
                            UpdatedBy = entity.UpdatedBy,
                            updatedOn = entity.updatedOn
                        };
                return q.ToList();
            });
        }

        public bool Insert(TelephoneVM entity)
        {
            Telephones tel = new Telephones()
            {
                TELE_ID = entity.TELE_ID,
                TELE_CAT_ID = entity.TELE_CAT_ID,
                TELE_NUMBER = entity.TELE_NUMBER,
                TELE_TYPE_ID = entity.TELE_TYPE_ID,
                AddedBy = entity.AddedBy,
                AddedOn = entity.AddedOn,
                UpdatedBy = entity.UpdatedBy,
                updatedOn = entity.updatedOn
            };
            telephoneRepo.Add(tel);
            return true;
        }

        public Task<int> InsertAsync(TelephoneVM entity)
        {
            return Task.Run<int>(() =>
             {
                 Telephones tel = new Telephones()
                 {
                     TELE_ID = entity.TELE_ID,
                     TELE_CAT_ID = entity.TELE_CAT_ID,
                     TELE_NUMBER = entity.TELE_NUMBER,
                     TELE_TYPE_ID = entity.TELE_TYPE_ID,
                     AddedBy = entity.AddedBy,
                     AddedOn = entity.AddedOn,
                     UpdatedBy = entity.UpdatedBy,
                     updatedOn = entity.updatedOn
                 };
                 telephoneRepo.Add(tel);
                 return tel.TELE_ID;
             });
        }
        public bool InsertList(List<TelephoneVM> entity)
        {
            if (entity.Count > 0)
            {
                DeleteAllByTeleID(entity[0]);
                for (int i = 0; i < entity.Count; i++)
                {
                    Telephones tel = new Telephones()
                    {
                        TELE_ID = entity[i].TELE_ID,
                        TELE_CAT_ID = entity[i].TELE_CAT_ID,
                        TELE_NUMBER = entity[i].TELE_NUMBER,
                        TELE_TYPE_ID = entity[i].TELE_TYPE_ID
                    };
                    telephoneRepo.Add(tel);
                }
                return true;
            }
            return false;
        }

        public Task<List<TelephoneVM>> InsertListAsync(List<TelephoneVM> entity)
        {
            return Task.Run<List<TelephoneVM>>(() =>
            {
                if (entity.Count > 0)
                {
                    DeleteAllByTeleID(entity[0]);
                    for (int i = 0; i < entity.Count; i++)
                    {
                        Telephones tel = new Telephones()
                        {
                            TELE_ID = entity[i].TELE_ID,
                            TELE_CAT_ID = entity[i].TELE_CAT_ID,
                            TELE_NUMBER = entity[i].TELE_NUMBER,
                            TELE_TYPE_ID = entity[i].TELE_TYPE_ID
                        };
                        telephoneRepo.Add(tel);
                    }
                    return entity;
                }
                return null;
            });

        }
        public bool DeleteAllByTeleID(TelephoneVM entity)
        {
            List<TelephoneVM> tels = GetBy(entity.TELE_ID, entity.TELE_TYPE_ID);
            if (tels.Count > 0)
                foreach (TelephoneVM telv in tels)
                {
                    Telephones tel = new Telephones()
                    {
                        TELE_ID = telv.TELE_ID,
                        TELE_CAT_ID = telv.TELE_CAT_ID,
                        TELE_NUMBER = telv.TELE_NUMBER,
                        TELE_TYPE_ID = telv.TELE_TYPE_ID
                    };
                    object[] keys = new object[4] { tel.TELE_ID, tel.TELE_TYPE_ID, tel.TELE_NUMBER, tel.TELE_CAT_ID };
                    telephoneRepo.DeleteComposite(tel, keys);
                }
            return true;
        }

        public bool Update(TelephoneVM entity)
        {
            Telephones tel = new Telephones()
            {
                TELE_ID = entity.TELE_ID,
                TELE_CAT_ID = entity.TELE_CAT_ID,
                TELE_NUMBER = entity.TELE_NUMBER,
                TELE_TYPE_ID = entity.TELE_TYPE_ID,
                AddedBy = entity.AddedBy,
                AddedOn = entity.AddedOn,
                UpdatedBy = entity.UpdatedBy,
                updatedOn = entity.updatedOn
            };
            object[] keys = new object[4] { entity.TELE_ID, entity.TELE_TYPE_ID, entity.TELE_NUMBER, entity.TELE_CAT_ID };
            telephoneRepo.UpdateComposite(tel, keys);
            return true;
        }

        public Task<bool> UpdateAsync(TelephoneVM entity)
        {
            return Task.Run<bool>(() =>
            {
                Telephones tel = new Telephones()
                {
                    TELE_ID = entity.TELE_ID,
                    TELE_CAT_ID = entity.TELE_CAT_ID,
                    TELE_NUMBER = entity.TELE_NUMBER,
                    TELE_TYPE_ID = entity.TELE_TYPE_ID,
                    AddedBy = entity.AddedBy,
                    AddedOn = entity.AddedOn,
                    UpdatedBy = entity.UpdatedBy,
                    updatedOn = entity.updatedOn
                };
                object[] keys = new object[4] { entity.TELE_ID, entity.TELE_TYPE_ID, entity.TELE_NUMBER, entity.TELE_CAT_ID };
                telephoneRepo.UpdateComposite(tel, keys);
                return true;
            });
        }





        public List<TelephoneVM> GetByType(int TypeID)
        {
            var q = from entity in telephoneRepo.GetAll().Where(x => x.TELE_TYPE_ID == TypeID)
                    select new TelephoneVM
                    {
                        TELE_ID = entity.TELE_ID,
                        TELE_CAT_ID = entity.TELE_CAT_ID,
                        TELE_NUMBER = entity.TELE_NUMBER,
                        TELE_TYPE_ID = entity.TELE_TYPE_ID,
                        AddedBy = entity.AddedBy,
                        AddedOn = entity.AddedOn,
                        UpdatedBy = entity.UpdatedBy,
                        updatedOn = entity.updatedOn
                    };
            return q.ToList();
        }
    }
}
