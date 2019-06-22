using AutoMapper;
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
    public class NotificationTypeService : INotificationTypeService
    {
        IGenericRepository<NotificationType> _NotificationTypeRepo;

        public NotificationTypeService(IGenericRepository<NotificationType> NotificationTypeRepo)
        {
            _NotificationTypeRepo = NotificationTypeRepo;
        }

        public List<NotificationTypeVM> GetAllNotificationType(int country = 1, int lang = 1)
        {
            var q = (from entity in _NotificationTypeRepo.GetAll()
                    .Where(x => x.Active == true && x.CountryID == country)
                    .OrderBy(x => x.Position)
                     select entity).ToList().Select(x => Mapper.Map<NotificationType, NotificationTypeVM>(x)).ToList();
            if (lang == 1)
            {
                foreach (var item in q)
                {
                    item.NAME = item.ARName;
                }
            }
            else
            {
                foreach (var item in q)
                {
                    item.NAME = item.LatName;
                }
            }
            return q;
        }

        public Task<int> InsertAsync(NotificationTypeVM entity)
        {
            return Task.Run<int>(() =>
            {
                var dbEntity = Mapper.Map<NotificationTypeVM, NotificationType>(entity);
                dbEntity.AddedOn = DateTime.Now;
                dbEntity.Active = true;
                dbEntity.Position = 1;
                var returnedObject = _NotificationTypeRepo.Add(dbEntity);
                return returnedObject.ID;
            });
        }

        public Task<bool> UpdateAsync(NotificationTypeVM entity)
        {
            return Task.Run<bool>(async () =>
            {
                var dbEntity = await getById(entity.ID);
                if (dbEntity != null)
                {
                    var mappedEntity = Mapper.Map(entity, dbEntity);
                    mappedEntity.UpdatedOn = DateTime.Now;
                    _NotificationTypeRepo.Update(mappedEntity, mappedEntity.ID);
                    return true;
                }
                else
                    return false;
            });
        }

        public Task<NotificationType> getById(int Id)
        {
            return Task.Run<NotificationType>(() =>
            {
                var dbEntity = (from entity in _NotificationTypeRepo.Filter(x => x.ID == Id)
                                select entity).SingleOrDefault();
                return dbEntity;
            });
        }

        public Task<NotificationTypeVM> getByIdForVM(int Id, int lang = 1)
        {
            return Task.Run<NotificationTypeVM>(() =>
            {
                var dbEntity = (from entity in _NotificationTypeRepo.Filter(x => x.ID == Id)
                                select entity).SingleOrDefault();
                var mappedEntity = Mapper.Map<NotificationType, NotificationTypeVM>(dbEntity);
                mappedEntity.NAME = lang == 1 ? mappedEntity.ARName : mappedEntity.LatName;
                return mappedEntity;
            });
        }


        public Task<bool> DeleteAsync(int id)
        {
            return Task.Run<bool>(async () =>
            {
                NotificationType dbEntity = await getById(id);
                if (dbEntity != null)
                {
                    _NotificationTypeRepo.Delete(dbEntity, dbEntity.ID);
                    return true;
                }
                else
                    return false;
            });
        }

        public Task<List<NotificationTypeVM>> GetPageData(int pageNum, int pageSize, int countryId)
        {
            return Task.Run<List<NotificationTypeVM>>(() =>
            {
                int rowCount;
                var q = (from p in _NotificationTypeRepo.GetPaged<int>(pageNum, pageSize, p => p.Active == true && p.CountryID == countryId, p => p.ID, false, out rowCount)
                         select p).ToList().Select(x => Mapper.Map<NotificationType, NotificationTypeVM>(x));
                return q.ToList();
            });
        }

        public Task<int> GetCount(int countryId)
        {
            return Task.Run<int>(async () =>
            {
                var total = await _NotificationTypeRepo.CountAsync(x => x.Active == true && x.CountryID == countryId);
                return total;
            });
        }
    }
}
