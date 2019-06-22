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
    public class MessageService : IMessageService
    {
        IGenericRepository<Message> _MessageRepo;
        IGenericRepository<MessagesView> _MessagesViewRepo;
        Translator t = new Translator();

        public MessageService(IGenericRepository<Message> MessageRepo, IGenericRepository<MessagesView> MessagesViewRepo)
        {
            _MessageRepo = MessageRepo;
            _MessagesViewRepo = MessagesViewRepo;
            t = new Translator();
        }

        public List<MessageVM> GetAllMessage(int lang = 1)
        {
            var q = (from entity in _MessageRepo.GetAll()
                    .Where(x => x.Active == true)
                    .OrderBy(x => x.Position)
                     select entity).ToList().Select(x => Mapper.Map<Message, MessageVM>(x)).ToList();
            if (lang == 1)
            {
                foreach (var item in q)
                {

                    item.Body = item.ARBody;
                    item.TranslatedBody = t.Translate(item.Body, "Arabic", "English");

                }
            }
            else
            {
                foreach (var item in q)
                {
                    item.Body = item.LatBody;
                    item.TranslatedBody = t.Translate(item.Body, "Arabic", "English");
                }
            }
            return q;
        }

        public List<MessagesViewVM> GetAllUserMessage(int userid = 1, int lang = 1, int MessageCategoryID = 0)
        {

            // Initialize the translator


            var q = (from entity in _MessagesViewRepo.GetAll()
                    .Where(x => x.Active == true && x.ToUserID == userid && (MessageCategoryID == 0 || x.MessageCategoryID == MessageCategoryID))
                    .OrderBy(x => x.Position)
                     select entity).ToList().Select(x => Mapper.Map<MessagesView, MessagesViewVM>(x)).ToList();
            if (lang == 1)
            {
                foreach (var item in q)
                {

                    item.MessageBody = item.MessageARBody;
                    item.TranslatedBody = t.Translate(item.MessageBody, "Arabic", "English");
                    item.ToUserFullName = item.ToUserARFullName;
                    item.FromUserFullName = item.FromUserARFullName;
                }
            }
            else
            {
                foreach (var item in q)
                {


                    item.MessageBody = item.MessageLatBody;
                    item.TranslatedBody = t.Translate(item.MessageBody, "Arabic", "English");
                    item.ToUserFullName = item.ToUserLatFullName;
                    item.FromUserFullName = item.FromUserLatFullName;
                }
            }
            return q;
        }


        public List<MessagesViewVM> GetAllReadedUserMessage(int userid = 1, int lang = 1, int MessageCategoryID = 0)
        {
            //var q = new List<MessagesViewVM>();
            //if(MessageCategoryID==0)
            var q = (from entity in _MessagesViewRepo.GetAll()
                    .Where(x => x.Active == true && x.ToUserID == userid && x.IsRead == true && (MessageCategoryID == 0 || x.MessageCategoryID == MessageCategoryID))
                    .OrderBy(x => x.Position)
                     select entity).ToList().Select(x => Mapper.Map<MessagesView, MessagesViewVM>(x)).ToList();
            if (lang == 1)
            {
                foreach (var item in q)
                {

                    item.MessageBody = item.MessageARBody;
                    item.TranslatedBody = t.Translate(item.MessageBody, "Arabic", "English");
                    item.ToUserFullName = item.ToUserARFullName;
                    item.FromUserFullName = item.FromUserARFullName;
                }
            }
            else
            {
                foreach (var item in q)
                {
                    item.MessageARSubject = t.Translate(item.MessageARSubject, "Arabic", "English");

                    item.MessageBody = item.MessageLatBody;
                    item.TranslatedBody = t.Translate(item.MessageBody, "Arabic", "English");
                    item.ToUserFullName = item.ToUserLatFullName;
                    item.FromUserFullName = item.FromUserLatFullName;
                }
            }
            return q;
        }




        public List<MessagesViewVM> GetAllUnreadUserMessage(int userid = 1, int lang = 1, int MessageCategoryID = 0)
        {
            var q = (from entity in _MessagesViewRepo.GetAll()
                    .Where(x => x.Active == true && x.ToUserID == userid && x.IsRead == false && (MessageCategoryID == 0 || x.MessageCategoryID == MessageCategoryID))
                    .OrderBy(x => x.Position)
                     select entity).ToList().Select(x => Mapper.Map<MessagesView, MessagesViewVM>(x)).ToList();
            if (lang == 1)
            {
                foreach (var item in q)
                {

                    item.MessageBody = item.MessageARBody;
                    item.TranslatedBody = t.Translate(item.MessageBody, "Arabic", "English");
                    item.ToUserFullName = item.ToUserARFullName;
                    item.FromUserFullName = item.FromUserARFullName;
                }
            }
            else
            {
                foreach (var item in q)
                {
                    item.MessageARSubject = t.Translate(item.MessageARSubject, "Arabic", "English");

                    item.MessageBody = item.MessageLatBody;
                    item.TranslatedBody = t.Translate(item.MessageBody, "Arabic", "English");
                    item.ToUserFullName = item.ToUserLatFullName;
                    item.FromUserFullName = item.FromUserLatFullName;
                }
            }
            return q;
        }



        public List<MessagesViewVM> GetAllUnConfirmedUserMessage(int userid = 1, int lang = 1, int MessageCategoryID = 0)
        {
            var q = (from entity in _MessagesViewRepo.GetAll()
                    .Where(x => x.Active == true && x.ToUserID == userid && (MessageCategoryID == 0 || x.MessageCategoryID == MessageCategoryID))
                    .OrderBy(x => x.Position)
                     select entity).ToList().Select(x => Mapper.Map<MessagesView, MessagesViewVM>(x)).ToList();
            if (lang == 1)
            {
                foreach (var item in q)
                {

                    item.MessageBody = item.MessageARBody;
                    item.TranslatedBody = t.Translate(item.MessageBody, "Arabic", "English");
                    item.ToUserFullName = item.ToUserARFullName;
                    item.FromUserFullName = item.FromUserARFullName;
                }
            }
            else
            {
                foreach (var item in q)
                {
                    item.MessageARSubject = t.Translate(item.MessageARSubject, "Arabic", "English");

                    item.MessageBody = item.MessageLatBody;
                    item.TranslatedBody = t.Translate(item.MessageBody, "Arabic", "English");
                    item.ToUserFullName = item.ToUserLatFullName;
                    item.FromUserFullName = item.FromUserLatFullName;
                }
            }
            return q;
        }


        public List<MessagesViewVM> GetAllConfirmedUserMessage(int userid = 1, int lang = 1, int MessageCategoryID = 0)
        {
            var q = (from entity in _MessagesViewRepo.GetAll()
                    .Where(x => x.Active == true && x.ToUserID == userid && (MessageCategoryID == 0 || x.MessageCategoryID == MessageCategoryID))
                    .OrderBy(x => x.Position)
                     select entity).ToList().Select(x => Mapper.Map<MessagesView, MessagesViewVM>(x)).ToList();
            if (lang == 1)
            {
                foreach (var item in q)
                {

                    item.MessageBody = item.MessageARBody;
                    item.TranslatedBody = t.Translate(item.MessageBody, "Arabic", "English");
                    item.ToUserFullName = item.ToUserARFullName;
                    item.FromUserFullName = item.FromUserARFullName;
                }
            }
            else
            {
                foreach (var item in q)
                {
                    item.MessageARSubject = t.Translate(item.MessageARSubject, "Arabic", "English");

                    item.MessageBody = item.MessageLatBody;
                    item.TranslatedBody = t.Translate(item.MessageBody, "Arabic", "English");
                    item.ToUserFullName = item.ToUserLatFullName;
                    item.FromUserFullName = item.FromUserLatFullName;
                }
            }
            return q;
        }



        public Task<int> InsertAsync(MessageVM entity)
        {
            return Task.Run<int>(() =>
            {
                var dbEntity = Mapper.Map<MessageVM, Message>(entity);
                dbEntity.AddedOn = DateTime.Now;
                dbEntity.Active = true;
                dbEntity.Position = 1;
                var returnedObject = _MessageRepo.Add(dbEntity);

                return returnedObject.ID;
            });
        }

        public Task<bool> UpdateAsync(MessageVM entity)
        {
            return Task.Run<bool>(async () =>
            {
                var dbEntity = await getById(entity.ID);
                if (dbEntity != null)
                {
                    var mappedEntity = Mapper.Map(entity, dbEntity);
                    mappedEntity.UpdatedOn = DateTime.Now;
                    _MessageRepo.Update(mappedEntity, mappedEntity.ID);
                    return true;
                }
                else
                    return false;
            });
        }


        public Task<bool> UpdateMessageReadStatus(int Id, int lang = 1)
        {
            return Task.Run<bool>(async () =>
            {
                var dbEntity = await getById(Id);
                if (dbEntity != null)
                {
                    dbEntity.IsRead = true;
                    _MessageRepo.Update(dbEntity, dbEntity.ID);
                    return true;
                }
                else
                    return false;
            });
        }

        public Task<bool> UpdateMessageReceivedStatus(int Id, int lang = 1)
        {
            return Task.Run<bool>(async () =>
            {
                var dbEntity = await getById(Id);
                if (dbEntity != null)
                {
                    dbEntity.IsReceived = true;
                    _MessageRepo.Update(dbEntity, dbEntity.ID);
                    return true;
                }
                else
                    return false;
            });
        }

        public bool UpdateUserMessageReadStatus(int Id, int lang = 1)
        {

            try
            {

                _MessageRepo.SQLQuery<MessageVM>("update Message set IsRead=1 Where " + " ToUserID=" + Id).ToList();
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }

        }


        public Task<Message> getById(int Id)
        {
            return Task.Run<Message>(() =>
            {
                var dbEntity = (from entity in _MessageRepo.Filter(x => x.ID == Id)
                                select entity).SingleOrDefault();
                return dbEntity;
            });
        }

        public Task<MessageVM> getByIdForVM(int Id, int lang = 1)
        {
            return Task.Run<MessageVM>(() =>
            {
                var dbEntity = (from entity in _MessageRepo.Filter(x => x.ID == Id)
                                select entity).SingleOrDefault();
                var mappedEntity = Mapper.Map<Message, MessageVM>(dbEntity);
                mappedEntity.Body = lang == 1 ? mappedEntity.ARBody : mappedEntity.LatBody;
                return mappedEntity;
            });
        }


        public Task<bool> DeleteAsync(int id)
        {
            return Task.Run<bool>(async () =>
            {
                Message dbEntity = await getById(id);
                if (dbEntity != null)
                {
                    _MessageRepo.Delete(dbEntity, dbEntity.ID);
                    return true;
                }
                else
                    return false;
            });
        }

        public Task<List<MessageVM>> GetPageData(int pageNum, int pageSize)
        {
            return Task.Run<List<MessageVM>>(() =>
            {
                int rowCount;
                var q = (from p in _MessageRepo.GetPaged<int>(pageNum, pageSize, p => p.Active == true, p => p.ID, false, out rowCount)
                         select p).ToList().Select(x => Mapper.Map<Message, MessageVM>(x));
                return q.ToList();
            });
        }

        public Task<int> GetCount()
        {
            return Task.Run<int>(async () =>
            {
                var total = await _MessageRepo.CountAsync(x => x.Active == true);
                return total;
            });
        }
    }
}
