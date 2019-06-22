using ResortERP.Core;
using ResortERP.Core.VM;
using ResortERP.Repository;
using ResortERP.Service.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace ResortERP.Service.Services
{
    public class EmailsService : IEmailsService, IDisposable
    {
        IGenericRepository<Emails> emailsRepo;
        public EmailsService(IGenericRepository<Emails> EmailsRepo)
        {
            this.emailsRepo = EmailsRepo;
        }

        public Task<int> CountAsync()
        {
            return Task.Run<int>(() =>
            {
                return emailsRepo.CountAsync();
            });
        }

        public bool Delete(EmailsVM entity)
        {
            Emails email = new Emails
            {
                id = entity.id
            };
            emailsRepo.Delete(email, entity.id);
            return true;
        }

        public Task<bool> DeleteAsync(EmailsVM entity)
        {
            return Task.Run<bool>(() =>
            {
                Emails email = new Emails
                {
                    id = entity.id
                };

                emailsRepo.Delete(email, entity.id);
                return true;
            });
        }

        public void Dispose()
        {
            emailsRepo.Dispose();
        }

        public List<EmailsVM> GetAll()
        {
            var q = (from entity in emailsRepo.GetAll() select entity).Select(X => Mapper.Map<Emails, EmailsVM>(X));
            return q.ToList();
        }

        public Task<List<EmailsVM>> GetAllAsync(int pageNum, int pageSize)
        {
            return Task.Run<List<EmailsVM>>(() =>
            {
                int rowCount;
                var q = (from entity in emailsRepo.GetPaged<long>(pageNum, pageSize, p => p.id, false, out rowCount)
                         select entity).Select(X => Mapper.Map<Emails, EmailsVM>(X));
                return q.ToList();
            });
        }

        public Task<List<EmailsVM>> GetByUID(int userID)
        {
            return Task.Run<List<EmailsVM>>(() =>
            {
                var q = emailsRepo.Filter(Y => Y.UID_ID == userID).Select(X => Mapper.Map<Emails, EmailsVM>(X));
                return q.ToList();
            });
        }

        public bool Insert(EmailsVM entity)
        {
            Emails email = Mapper.Map<EmailsVM, Emails>(entity);
            Emails em = emailsRepo.Add(email);
            return em != null ? true : false;
        }

        public Task<bool> InsertAsync(EmailsVM entity)
        {
            return Task.Run<bool>(() =>
            {
                Emails email = Mapper.Map<EmailsVM, Emails>(entity);
                Emails em = emailsRepo.Add(email);
                return em != null ? true : false;
            });
        }

        public bool Update(EmailsVM entity)
        {
            Emails email = Mapper.Map<EmailsVM, Emails>(entity);
            Emails em = emailsRepo.Update(email, email.id);
            return em != null ? true : false;
        }

        public Task<bool> UpdateAsync(EmailsVM entity)
        {
            return Task.Run<bool>(() =>
            {
                Emails email = Mapper.Map<EmailsVM, Emails>(entity);
                Emails em = emailsRepo.Update(email, email.id);
                return em != null ? true : false;
            });
        }
    }
}
