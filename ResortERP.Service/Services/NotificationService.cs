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
    public class NotificationService : INotificationService
    {
        IGenericRepository<Notification> _NotificationRepo;
        IGenericRepository<NotificationsView> _NotificationsViewRepo;

        public NotificationService(IGenericRepository<Notification> NotificationRepo, IGenericRepository<NotificationsView> NotificationsViewRepo)
        {
            _NotificationRepo = NotificationRepo;
            _NotificationsViewRepo = NotificationsViewRepo;
        }

        public List<NotificationVM> GetAllNotification(int lang = 1)
        {
            var q = (from entity in _NotificationRepo.GetAll()
                    .Where(x => x.Active == true)
                    .OrderBy(x => x.Position)
                     select entity).ToList().Select(x => Mapper.Map<Notification, NotificationVM>(x)).ToList();
            if (lang == 1)
            {
                foreach (var item in q)
                {
                    item.Body = item.ARBody;
                }
            }
            else
            {
                foreach (var item in q)
                {
                    item.Body = item.LatBody;
                }
            }
            return q;
        }

        public List<NotificationsViewVM> GetAllUserNotification(int userid = 1, int lang = 1, int NotifcationCategoryID = 0)
        {
            var q = (from entity in _NotificationsViewRepo.GetAll()
                    .Where(x => x.Active == true && x.ToUserID == userid && (NotifcationCategoryID == 0 || x.NotifcationCategoryID == NotifcationCategoryID))
                    .OrderBy(x => x.Position)
                     select entity).ToList().Select(x => Mapper.Map<NotificationsView, NotificationsViewVM>(x)).ToList();
            if (lang == 1)
            {
                foreach (var item in q)
                {
                    item.Body = item.ARBody;
                    item.ToUserFullName = item.ToUserARFullName;
                    item.FromUserFullName = item.FromUserARFullName;
                }
            }
            else
            {
                foreach (var item in q)
                {
                    item.Body = item.LatBody;
                    item.ToUserFullName = item.ToUserLatFullName;
                    item.FromUserFullName = item.FromUserLatFullName;
                }
            }
            return q;
        }


        public List<NotificationsViewVM> GetAllReadedUserNotification(int userid = 1, int lang = 1, int NotifcationCategoryID = 0)
        {
            var q = (from entity in _NotificationsViewRepo.GetAll()
                              .Where(x => x.Active == true && x.ToUserID == userid && x.IsRead == true && (NotifcationCategoryID == 0 || x.NotifcationCategoryID == NotifcationCategoryID))
                              .OrderBy(x => x.Position)
                     select entity).ToList().Select(x => Mapper.Map<NotificationsView, NotificationsViewVM>(x)).ToList();

            if (lang == 1)
            {
                foreach (var item in q)
                {
                    item.Body = item.ARBody;
                    item.ToUserFullName = item.ToUserARFullName;
                    item.FromUserFullName = item.FromUserARFullName;
                }
            }
            else
            {
                foreach (var item in q)
                {
                    item.Body = item.LatBody;
                    item.ToUserFullName = item.ToUserLatFullName;
                    item.FromUserFullName = item.FromUserLatFullName;
                }
            }
            return q;
        }




        public List<NotificationsViewVM> GetAllUnreadUserNotification(int userid = 1, int lang = 1, int NotifcationCategoryID = 0)
        {
            var q = (from entity in _NotificationsViewRepo.GetAll()
                 .Where(x => x.Active == true && x.ToUserID == userid && (NotifcationCategoryID == 0 || x.NotifcationCategoryID == NotifcationCategoryID) && x.IsRead == false)
                 .OrderBy(x => x.Position)
                     select entity).ToList().Select(x => Mapper.Map<NotificationsView, NotificationsViewVM>(x)).ToList();

            if (lang == 1)
            {
                foreach (var item in q)
                {
                    item.Body = item.ARBody;
                    item.ToUserFullName = item.ToUserARFullName;
                    item.FromUserFullName = item.FromUserARFullName;
                }
            }
            else
            {
                foreach (var item in q)
                {
                    item.Body = item.LatBody;
                    item.ToUserFullName = item.ToUserLatFullName;
                    item.FromUserFullName = item.FromUserLatFullName;
                }
            }
            return q;
        }



        public List<NotificationsViewVM> GetAllUnConfirmedUserNotification(int userid = 1, int lang = 1, int NotifcationCategoryID = 0)
        {
            var q = (from entity in _NotificationsViewRepo.GetAll()
                    .Where(x => x.Active == true && x.ToUserID == userid && (NotifcationCategoryID == 0 || x.NotifcationCategoryID == NotifcationCategoryID) && x.IsConfirmed == false)
                    .OrderBy(x => x.Position)
                     select entity).ToList().Select(x => Mapper.Map<NotificationsView, NotificationsViewVM>(x)).ToList();
            if (lang == 1)
            {
                foreach (var item in q)
                {
                    item.Body = item.ARBody;
                    item.ToUserFullName = item.ToUserARFullName;
                    item.FromUserFullName = item.FromUserARFullName;
                }
            }
            else
            {
                foreach (var item in q)
                {
                    item.Body = item.LatBody;
                    item.ToUserFullName = item.ToUserLatFullName;
                    item.FromUserFullName = item.FromUserLatFullName;
                }
            }
            return q;
        }


        public List<NotificationsViewVM> GetAllConfirmedUserNotification(int userid = 1, int lang = 1, int NotifcationCategoryID = 0)
        {
            var q = (from entity in _NotificationsViewRepo.GetAll()
                    .Where(x => x.Active == true && x.ToUserID == userid && (NotifcationCategoryID == 0 || x.NotifcationCategoryID == NotifcationCategoryID) && x.IsConfirmed == true)
                    .OrderBy(x => x.Position)
                     select entity).ToList().Select(x => Mapper.Map<NotificationsView, NotificationsViewVM>(x)).ToList();
            if (lang == 1)
            {
                foreach (var item in q)
                {
                    item.Body = item.ARBody;
                    item.ToUserFullName = item.ToUserARFullName;
                    item.FromUserFullName = item.FromUserARFullName;
                }
            }
            else
            {
                foreach (var item in q)
                {
                    item.Body = item.LatBody;
                    item.ToUserFullName = item.ToUserLatFullName;
                    item.FromUserFullName = item.FromUserLatFullName;
                }
            }
            return q;
        }



        public Task<int> InsertAsync(NotificationVM entity)
        {
            return Task.Run<int>(() =>
            {
                var dbEntity = Mapper.Map<NotificationVM, Notification>(entity);
                dbEntity.AddedOn = DateTime.Now;
                dbEntity.Active = true;
                dbEntity.Position = 1;
                var returnedObject = _NotificationRepo.Add(dbEntity);
                return returnedObject.ID;
            });
        }

        public Task<bool> UpdateAsync(NotificationVM entity)
        {
            return Task.Run<bool>(async () =>
            {
                var dbEntity = await getById(entity.ID);
                if (dbEntity != null)
                {
                    var mappedEntity = Mapper.Map(entity, dbEntity);
                    mappedEntity.UpdatedOn = DateTime.Now;
                    _NotificationRepo.Update(mappedEntity, mappedEntity.ID);
                    return true;
                }
                else
                    return false;
            });
        }


        public Task<bool> UpdateNotificationConfirmedStatus(int Id, int lang = 1)
        {
            return Task.Run<bool>(async () =>
            {
                var dbEntity = await getById(Id);
                if (dbEntity != null)
                {
                    dbEntity.IsConfirmed = true;
                    _NotificationRepo.Update(dbEntity, dbEntity.ID);
                    return true;
                }
                else
                    return false;
            });
        }


        public bool UpdateUserNotificationReadStatus(int Id, int lang = 1)
        {

            try
            {

                _NotificationRepo.SQLQuery<NotificationVM>("update Notification set IsRead=1 Where " + " ToUserID=" + Id).ToList();
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }

        }


        public Task<Notification> getById(int Id)
        {
            return Task.Run<Notification>(() =>
            {
                var dbEntity = (from entity in _NotificationRepo.Filter(x => x.ID == Id)
                                select entity).SingleOrDefault();
                return dbEntity;
            });
        }

        public Task<NotificationVM> getByIdForVM(int Id, int lang = 1)
        {
            return Task.Run<NotificationVM>(() =>
            {
                var dbEntity = (from entity in _NotificationRepo.Filter(x => x.ID == Id)
                                select entity).SingleOrDefault();
                var mappedEntity = Mapper.Map<Notification, NotificationVM>(dbEntity);
                mappedEntity.Body = lang == 1 ? mappedEntity.ARBody : mappedEntity.LatBody;
                return mappedEntity;
            });
        }


        public Task<bool> DeleteAsync(int id)
        {
            return Task.Run<bool>(async () =>
            {
                Notification dbEntity = await getById(id);
                if (dbEntity != null)
                {
                    _NotificationRepo.Delete(dbEntity, dbEntity.ID);
                    return true;
                }
                else
                    return false;
            });
        }

        public Task<List<NotificationVM>> GetPageData(int pageNum, int pageSize)
        {
            return Task.Run<List<NotificationVM>>(() =>
            {
                int rowCount;
                var q = (from p in _NotificationRepo.GetPaged<int>(pageNum, pageSize, p => p.Active == true, p => p.ID, false, out rowCount)
                         select p).ToList().Select(x => Mapper.Map<Notification, NotificationVM>(x));
                return q.ToList();
            });
        }

        public Task<int> GetCount()
        {
            return Task.Run<int>(async () =>
            {
                var total = await _NotificationRepo.CountAsync(x => x.Active == true);
                return total;
            });
        }
    }
}
