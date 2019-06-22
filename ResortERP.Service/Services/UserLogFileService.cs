using ResortERP.Core;
using ResortERP.Repository;
using ResortERP.Service.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ResortERP.Core.VM;

namespace ResortERP.Service.Services
{
    public class UserLogFileService : IUserLogFileService, IDisposable
    {
        IGenericRepository<UserLogFile> UserLogFileRepo;
        IGenericRepository<UserForm> UserFormRepo;
        IGenericRepository<UserOperation> UserOperationRepo;
        IGenericRepository<BILL_SETTINGS> BILL_SETTINGSRepo;
        IGenericRepository<ENTRY_SETTINGS> ENTRY_SETTINGSRepo;
        IGenericRepository<ACCOUNTS_TYPES> ACCOUNTS_TYPESRepo;
        IGenericRepository<ACCOUNTS> ACCOUNTSRepo;
        //public UserLogFileService()
        //{
        //}
        public UserLogFileService(
            IGenericRepository<UserLogFile> UserLogFileRepo, 
            IGenericRepository<UserForm> UserFormRepo, 
            IGenericRepository<UserOperation> UserOperationRepo, 
            IGenericRepository<BILL_SETTINGS> BILL_SETTINGSRepo,
            IGenericRepository<ENTRY_SETTINGS> ENTRY_SETTINGSRepo,
            IGenericRepository<ACCOUNTS_TYPES> ACCOUNTS_TYPESRepo,
            IGenericRepository<ACCOUNTS> ACCOUNTSRepo
            )
        {
            this.UserLogFileRepo = UserLogFileRepo;
            this.UserFormRepo = UserFormRepo;
            this.UserOperationRepo = UserOperationRepo;
            this.BILL_SETTINGSRepo = BILL_SETTINGSRepo;
            this.ENTRY_SETTINGSRepo = ENTRY_SETTINGSRepo;
            this.ACCOUNTS_TYPESRepo = ACCOUNTS_TYPESRepo;
            this.ACCOUNTSRepo = ACCOUNTSRepo;
        }


        //public Task<UserLogFile> InsertAsync(UserLogFile entity)
        //{
        //    return Task.Run<UserLogFile>(() =>
        //    {

        //        UserLogFileRepo.Add(entity);
        //        return entity;
        //    }
        //      );

        //}

        public Task<UserLogFile> Insert(UserLogFile entity)
        {

            return Task.Run<UserLogFile>(() =>
            {
                if (entity != null)
                {
                    var q = UserOperationRepo.GetAll().Where(a => a.LatName == entity.OpName).FirstOrDefault();
                    if (q != null)
                    {
                        entity.OpName = q.ARName;
                    }

                    if (entity.FormName.Split('_').Count() > 1 || entity.FormName.Split('?').Count() > 1 || entity.FormName.Split('&').Count() > 1)
                    {
                        foreach (var item in UserFormRepo.GetAll())
                        {

                            if (item.Code.Split('/').Count() > 1)
                            {
                                var q2 = UserFormRepo.GetAll().Where(a => (a.Code.Split('/')[0] == entity.FormName.Split('_')[0]) || (a.Code.Split('/')[0] == entity.FormName.Split('?')[0]) || (a.Code.Split('/')[0] == entity.FormName.Split('&')[0]));
                                foreach (var childItem in q2)
                                {
                                    if (entity.FormName.Split('_').Count() > 1)
                                    {
                                        var q3 = BILL_SETTINGSRepo.GetAll().Where(a => a.BILL_SETTING_ID == int.Parse(entity.FormName.Split('_')[1])).SingleOrDefault();
                                        if (q3 != null)
                                        {
                                            //entity.FormName = q3.BILL_AR_NAME;

                                            //entity.Code = entity.FormName.Split('_')[2];
                                            entity.TypeName = q3.BILL_AR_NAME;
                                            entity.FormName = UserFormRepo.GetAll().Where(a => (a.Code == entity.FormName.Split('_')[0]) || (a.Code == entity.FormName.Split('?')[0]) || (a.Code == entity.FormName.Split('&')[0])).SingleOrDefault().ARName;

                                            //if (entity.Notes != null)
                                            //{
                                            //     var temp = entity.Notes;
                                            //    entity.Notes = null;
                                            //    entity.Notes = entity.TypeName + " رقم " + temp;
                                            //}
                                        }
                                        else
                                        {
                                            entity.FormName = childItem.ARName;
                                        }
                                    }

                                    if (entity.FormName.Split('?').Count() > 1)
                                    {
                                        var q4 = ENTRY_SETTINGSRepo.GetAll().Where(a => a.ENTRY_SETTING_ID == int.Parse(entity.FormName.Split('?')[1])).SingleOrDefault();
                                        if (q4 != null)
                                        {
                                            //entity.FormName = q4.ENTRY_SETTING_AR_NAME;

                                            //entity.Code = entity.FormName.Split('?')[2];
                                            entity.TypeName = q4.ENTRY_SETTING_AR_NAME;

                                            //if (entity.Notes != null)
                                            //{
                                            //    var temp = entity.Notes;
                                            //    entity.Notes = null;
                                            //    entity.Notes = entity.TypeName + " رقم " + temp;
                                            //}
                                            var x =  UserFormRepo.GetAll().Where(a => (a.Code == entity.FormName.Split('_')[0]) || (a.Code == entity.FormName.Split('?')[0]) || (a.Code == entity.FormName.Split('&')[0])).SingleOrDefault();
                                            if (x != null)
                                            {
                                                entity.FormName = x.ARName;
                                            }
                                            //entity.FormName = childItem.ARName;
                                            //entity.FormName = UserFormRepo.GetAll().Where(a => (a.Code == entity.FormName.Split('_')[0]) || (a.Code == entity.FormName.Split('?')[0]) || (a.Code == entity.FormName.Split('&')[0])).SingleOrDefault().ARName;
                                        }
                                        else
                                        {
                                            entity.FormName = childItem.ARName;
                                        }
                                    }

                                    if (entity.FormName.Split('&').Count() > 1)
                                    {
                                        var q5 = ACCOUNTSRepo.GetAll().Where(a => a.ACC_ID == int.Parse(entity.FormName.Split('&')[1])).SingleOrDefault();
                                        if (q5 != null)
                                        {
                                            var q4 = ACCOUNTS_TYPESRepo.GetAll().Where(a => a.ACC_TYPE_ID == q5.ACC_TYPE_ID).SingleOrDefault();
                                            if (q4 != null)
                                            {
                                                entity.Code = entity.FormName.Split('&')[2];
                                                entity.TypeName = q4.ACC_TYPE_AR_NAME;
                                                entity.FormName = childItem.ARName;
                                            }
                                            else
                                            {
                                                entity.FormName = childItem.ARName;
                                            }
                                        }

                                    }

                                }

                            }

                        }
                    }
                    else
                    {
                        if (entity.FormName.Split('+').Count() > 1)
                        {
                            var q2 = UserFormRepo.GetAll().Where(a => a.Code == entity.FormName.Split('+')[0]).FirstOrDefault();
                            if (q2 != null)
                            {
                                entity.Code = entity.FormName.Split('+')[1];
                                entity.FormName = q2.ARName;
                            }
                        }
                        else
                        {
                            var q2 = UserFormRepo.GetAll().Where(a => a.Code == entity.FormName).FirstOrDefault();
                            if (q2 != null)
                            {
                                entity.FormName = q2.ARName;

                            }
                        }

                        //if (entity.Notes != null&& entity.TypeName==null)
                        //{
                        //    var temp = entity.Notes;
                        //    entity.Notes = null;
                        //    entity.Notes = entity.FormName + " رقم " + temp;
                        //}

                    }

                    //var q2 = UserFormRepo.GetAll().Where(a => a.Code == entity.FormName).FirstOrDefault();
                    //if (q2 != null)
                    //{
                    //    entity.FormName = q2.ARName;
                    //}
                    if (entity.Notes != null)
                    {
                        var temp = entity.Notes;
                        entity.Notes = null;
                        if(entity.TypeName!=null)
                        entity.Notes = entity.TypeName + " رقم " + temp;
                        else
                        {
                            entity.Notes = entity.FormName + " رقم " + temp;
                        }
                    }
                    if(entity.IDCRUD!="1")
                        UserLogFileRepo.Add(entity);
                }
            
                return entity;


            }
                      );

        }
        public void Dispose()
        {
            UserLogFileRepo.Dispose();
            UserFormRepo.Dispose();
            UserOperationRepo.Dispose();
            BILL_SETTINGSRepo.Dispose();
            ENTRY_SETTINGSRepo.Dispose();
            ACCOUNTS_TYPESRepo.Dispose();
            ACCOUNTSRepo.Dispose();
        }

        public List<UserLogFile> GetAll(int pageNum, int pageSize)
        {
           return UserLogFileRepo.GetAll().ToList();
        }

        public Task<int> CountAsync()
        {
            return Task.Run(
                () => {
                    return UserLogFileRepo.CountAsync();
                });
        }
    }
}
