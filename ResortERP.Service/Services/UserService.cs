using AutoMapper;
using ResortERP.Core;
using ResortERP.Core.VM;
using ResortERP.Repository;
using ResortERP.Service.IServices;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using System.Web.UI.WebControls;


namespace ResortERP.Service.Services
{
    public class UserService : IUserService, IDisposable
    {
        IGenericRepository<User> userRepo;
        ICommonRepository<User> commonRepo;


        public UserService(IGenericRepository<User> userRepo, ICommonRepository<User> commonRepo)
        {
            this.userRepo = userRepo;
            this.commonRepo = commonRepo;
        }

        public void Dispose()
        {
            userRepo.Dispose();
            commonRepo.Dispose();
        }

        public Task<UserVM> GetUser(string userName)
        {
            return Task.Run<UserVM>(() =>
            {
                return getUser(userName);
            });
        }
        public Task<UserVM> GetAdminUserByCompanyAsync(string companyEnName)
        {
            return Task.Run<UserVM>(() =>
            {
                return GetAdminUserByCompany(companyEnName);
            });
        }
        public Task<UserVM> GetUserAsync(string userName, string password)
        {
            return Task.Run<UserVM>(() =>
            {
                return getUser(userName, password);
            });
        }
        public UserVM GetUser(string userName, string password)
        {

            return getUser(userName, password);

        }

        UserVM getUser(string userName)
        {
            //if (userName.Contains("@"))
            //{
            //string[] names = userName.Split('@');
            //string CompanyName = names[1].ToString();
            //string userNameSplitted = names[0].ToString();
            //SqlParameter companyNameParam = new SqlParameter("@companyName", (object)CompanyName);
            //SqlParameter userNameParam = new SqlParameter("@userName", (object)userNameSplitted);
            SqlParameter userNameParam = new SqlParameter("@userName", (object)userName);
            List<UserVM> User = userRepo.SQLQuery<UserVM>("exec GetInsideUserForgetPassword @userName", userNameParam).ToList<UserVM>();
            return User.FirstOrDefault();
            //}
            //else
            //{
            //    //var q = from p in userRepo.FilterAsync(p => p.UID == userName && p.UPWD == password).Result
            //    var q = (from p in userRepo.Filter(p => p.UID == userName)
            //             select p).ToList().Select(x => Mapper.Map<User, UserVM>(x)).FirstOrDefault();
            //    return q;
            //}
            //var q = from p in userRepo.FilterAsync(p => p.UID == userName).Result
            //        select new UserVM { ID = p.ID, UID = p.UID, UPWD = p.UPWD };
            //return q.SingleOrDefault();
        }

        public Task<List<string>> ValidateReg(UserVM entity)
        {
            return Task.Run<List<string>>(() =>
            {
                SqlParameter COMPANY_EMAILParam = new SqlParameter("@email", (object)entity.COMPANY_EMAIL);
                SqlParameter UIDParam = new SqlParameter("@userName", (object)entity.UID);
                SqlParameter COMPANY_AR_NAMEParam = new SqlParameter("@nameAr", (object)entity.COMPANY_AR_NAME);
                SqlParameter COMPANY_EN_NAMEParam = new SqlParameter("@nameEn", (object)entity.COMPANY_EN_NAME);
                var ValidateObj = userRepo.SQLQuery<ValidationRegistery>("exec ValidateRegisteration  @email,@userName,@nameAr,@nameEn", COMPANY_EMAILParam, UIDParam, COMPANY_AR_NAMEParam, COMPANY_EN_NAMEParam).ToList<ValidationRegistery>().FirstOrDefault();

                List<string> strLst = new List<string>();
                if (ValidateObj.Email == "true") { strLst.Add("email"); }
                if (ValidateObj.UserName == "true") { strLst.Add("userName"); }
                if (ValidateObj.CompanyArName == "true") { strLst.Add("nameAr"); }
                if (ValidateObj.CompanyEnName == "true") { strLst.Add("nameEn"); }

                return strLst;

                //List<string> strLst = new List<string>();
                //if (CheckEmailIsExist(entity.COMPANY_EMAIL) > 0) { strLst.Add("email"); }
                //if (CheckUserNameIsExist(entity.UID) > 0) { strLst.Add("userName"); }
                //if (CheckARCompanyNameIsExist(entity.COMPANY_AR_NAME) > 0) { strLst.Add("nameAr"); }
                //if (CheckENCompanyNameIsExist(entity.COMPANY_EN_NAME) > 0) { strLst.Add("nameEn"); }

                //return strLst;
            });
        }

        public Task<bool> ActivateUser(int Id)
        {
            return Task.Run<bool>(async () =>
            {
                var dbEntity = await getById(Id);
                try
                {
                    #region SysConfigAdmin_Activation
                    if (dbEntity != null)
                    {
                        var mappedEntity = Mapper.Map<User>(dbEntity);
                        mappedEntity.IS_ACTIVATED = true;
                        User user = userRepo.Update(mappedEntity, mappedEntity.ID);
                        if (user != null)
                        {
                            int res = await ActivateInnerAdmin(Id);
                            return res > 0;
                        }
                        else return false;
                    }
                    else return false;
                    #endregion

                }
                catch { return false; }
            });
        }
        public Task<User> getInnerAdminById(int Id)
        {
            return Task.Run<User>(() =>
            {
                SqlParameter UserIDParam = new SqlParameter("@ID", (object)Id);
                var dbEntity = userRepo.SQLQuery<User>("exec GetInnerAdmin  @ID", UserIDParam).FirstOrDefault();
                return dbEntity;
            });
        }
        public Task<int> ActivateInnerAdmin(int Id)
        {
            return Task.Run<int>(() =>
            {
                SqlParameter UserIDParam = new SqlParameter("@ID", (object)Id);
                var dbEntity = userRepo.ExecuteSqlCommand("exec ActivateInnerAdmin  @ID", UserIDParam);
                return dbEntity;
            });
        }

        public Task<User> getById(int Id)
        {
            return Task.Run<User>(() =>
            {
                var dbEntity = userRepo.Filter(x => x.ID == Id).SingleOrDefault();
                return dbEntity;
            });
        }


        int CheckEmailIsExist(string email)
        {
            return userRepo.Filter(y => y.COMPANY_EMAIL != null).Select(x => new User()
            {
                COMPANY_EMAIL = x.COMPANY_EMAIL
            }).Where(x => x.COMPANY_EMAIL.ToLower() == email.ToLower()).ToList().Count();
        }

        int CheckUserNameIsExist(string userName)
        {
            return userRepo.Filter(y => y.UID != null).Select(x => new User()
            {
                UID = x.UID
            }).Where(x => x.UID == userName).ToList().Count();
        }

        int CheckARCompanyNameIsExist(string companyARName)
        {
            return userRepo.Filter(y => y.COMPANY_AR_NAME != null).Select(x => new User()
            {
                COMPANY_AR_NAME = x.COMPANY_AR_NAME
            }).Where(x => x.COMPANY_AR_NAME == companyARName).ToList().Count();
        }

        int CheckENCompanyNameIsExist(string companyENName)
        {
            return userRepo.Filter(y => y.COMPANY_EN_NAME != null).Select(x => new User()
            {
                COMPANY_EN_NAME = x.COMPANY_EN_NAME
            }).Where(x => x.COMPANY_EN_NAME == companyENName).ToList().Count();
        }


        public Task<bool> CheckEmailExist(string email)
        {
            return Task.Run<bool>(() =>
             {
                 return userRepo.GetAll().Any(x => x.COMPANY_EMAIL.ToLower() == email.ToLower());
             });
        }

        public Task<bool> CheckUserNameExist(string userName)
        {
            return Task.Run<bool>(() =>
            {
                return userRepo.GetAll().Any(x => x.UID.ToLower() == userName.ToLower());
            });
        }

        public Task<bool> CheckARCompanyNameExist(string companyARName)
        {
            return Task.Run<bool>(() =>
            {
                return userRepo.GetAll().Any(x => x.COMPANY_AR_NAME.ToLower() == companyARName.ToLower());
            });
        }

        public Task<bool> CheckENCompanyNameExist(string companyENName)
        {
            return Task.Run<bool>(() =>
            {
                return userRepo.GetAll().Any(x => x.COMPANY_EN_NAME.ToLower() == companyENName.ToLower());
            });
        }


        UserVM getUser(string userName, string password)
        {
            //if (userName.Contains("@"))
            //{
            //string[] names = userName.Split('@');
            //string CompanyName = names[1].ToString();
            //string userNameSplitted = names[0].ToString();
            //SqlParameter companyNameParam = new SqlParameter("@companyName", (object)CompanyName);
            SqlParameter userNameParam = new SqlParameter("@userName", (object)userName);
            SqlParameter passwordParam = new SqlParameter("@password", (object)password);
            List<UserVM> User = userRepo.SQLQuery<UserVM>("exec GetInsideUserAuthenticated  @userName,@password", userNameParam, passwordParam).ToList<UserVM>();
            return User.FirstOrDefault();
            //}
            //else
            //{
            //    //var q = from p in userRepo.FilterAsync(p => p.UID == userName && p.UPWD == password).Result
            //    var q = (from p in userRepo.Filter(p => p.UID == userName && p.UPWD == password)
            //             select p).ToList().Select(x => Mapper.Map<User, UserVM>(x)).FirstOrDefault();
            //    return q;
            //}
        }



        public UserVM GetAdminUserByCompany(string companyEnName)
        {
            var q = from p in userRepo.FilterAsync(p => p.COMPANY_EN_NAME == companyEnName).Result
                    select new UserVM { ID = p.ID, UID = p.UID, UPWD = p.UPWD, CS_DataSource = p.CS_DataSource, CS_InitialCatalog = p.CS_InitialCatalog, CS_UserId = p.CS_UserId, CS_Password = p.CS_Password, COMPANY_EN_NAME = p.COMPANY_EN_NAME, COMPANY_AR_NAME = p.COMPANY_AR_NAME };
            return q.SingleOrDefault();
        }
        public UserVM getUserObj(string userName, string password)
        {
            var q = from p in userRepo.FilterAsync(p => p.UID == userName && p.UPWD == password).Result
                    select new UserVM { ID = p.ID, UID = p.UID, UPWD = p.UPWD, CS_DataSource = p.CS_DataSource, CS_InitialCatalog = p.CS_InitialCatalog, CS_UserId = p.CS_UserId, CS_Password = p.CS_Password };
            return q.SingleOrDefault();
        }

        public Task<User> InsertAsync(UserVM entity)
        {
            return Task.Run<User>(() =>
            {
                User uid = new User
                {
                    ID = entity.ID,
                    UID = entity.UID,
                    UPWD = entity.UPWD,
                    CS_DataSource = entity.CS_DataSource,
                    CS_InitialCatalog = entity.CS_InitialCatalog,
                    CS_UserId = entity.CS_UserId,
                    CS_Password = entity.CS_Password,
                    AddedBy = entity.AddedBy,
                    AddedOn = entity.AddedOn,
                    COMPANY_AR_ADRESS = entity.COMPANY_AR_ADRESS,
                    COMPANY_AR_NAME = entity.COMPANY_AR_NAME,
                    COMPANY_EN_ADRESS = entity.COMPANY_EN_ADRESS,
                    COMPANY_EN_NAME = entity.COMPANY_EN_NAME,
                    COMPANY_FAX = entity.COMPANY_FAX,
                    COMPANY_LOGO = entity.COMPANY_LOGO,
                    COMPANY_TELEPHONE = entity.COMPANY_TELEPHONE,
                    DbCreationDate = entity.DbCreationDate,
                    DbDescriptionName = entity.DbDescriptionName,
                    DbEndPeriodDate = entity.DbEndPeriodDate,
                    DbFirstPeriodDate = entity.DbFirstPeriodDate,
                    DELETEDORNOT = entity.DELETEDORNOT,
                    SavePWD = entity.SavePWD,
                    SHIFT_NUMBER = entity.SHIFT_NUMBER,
                    UpdatedBy = entity.UpdatedBy,
                    updatedOn = entity.updatedOn,
                    UserGroupID = entity.UserGroupID,
                    USER_LETTER = entity.USER_LETTER,
                    USER_TYPE = entity.USER_TYPE,
                    COMPANY_ACTIVATION_CODE = entity.COMPANY_ACTIVATION_CODE,
                    COMPANY_EMAIL = entity.COMPANY_EMAIL,
                    IS_ACTIVATED = entity.IS_ACTIVATED,
                    MaxUsersNumber = entity.MaxUsersNumber,
                    DateTypeID = entity.DateTypeID,
                    LanguageID = entity.LanguageID,
                    ActivationRequestedDateTime = entity.ActivationRequestedDateTime,
                    IsAdmin = entity.IsAdmin
                };
                userRepo.Add(uid);
                return uid;
            }
              );

        }
        public Task<bool> InsertInnerUserAsync(UserVM entity)
        {
            return Task.Run<bool>(() =>
            {
                SqlParameter ActivationRequestedDateTimeParam = new SqlParameter("@ActivationRequestedDateTime", (object)entity.ActivationRequestedDateTime ?? DBNull.Value);
                SqlParameter AddedByParam = new SqlParameter("@AddedBy", (object)entity.AddedBy ?? DBNull.Value);
                SqlParameter AddedOnParam = new SqlParameter("@AddedOn", (object)entity.AddedOn ?? DBNull.Value);
                SqlParameter COMPANY_ACTIVATION_CODEParam = new SqlParameter("@COMPANY_ACTIVATION_CODE", (object)entity.COMPANY_ACTIVATION_CODE ?? DBNull.Value);
                SqlParameter COMPANY_AR_ADRESSParam = new SqlParameter("@COMPANY_AR_ADRESS", (object)entity.COMPANY_AR_ADRESS ?? DBNull.Value);
                SqlParameter COMPANY_AR_NAMEParam = new SqlParameter("@COMPANY_AR_NAME", (object)entity.COMPANY_AR_NAME ?? DBNull.Value);
                SqlParameter COMPANY_EMAILParam = new SqlParameter("@COMPANY_EMAIL", (object)entity.COMPANY_EMAIL ?? DBNull.Value);
                SqlParameter COMPANY_EN_ADRESSParam = new SqlParameter("@COMPANY_EN_ADRESS", (object)entity.COMPANY_EN_ADRESS ?? DBNull.Value);
                SqlParameter COMPANY_EN_NAMEParam = new SqlParameter("@COMPANY_EN_NAME", (object)entity.COMPANY_EN_NAME ?? DBNull.Value);
                SqlParameter COMPANY_FAXParam = new SqlParameter("@COMPANY_FAX", (object)entity.COMPANY_FAX ?? DBNull.Value);
                //SqlParameter COMPANY_LOGOParam = new SqlParameter("@COMPANY_LOGO", (object)entity.COMPANY_LOGO ?? DBNull.Value);
                SqlParameter COMPANY_TELEPHONEParam = new SqlParameter("@COMPANY_TELEPHONE", (object)entity.COMPANY_TELEPHONE ?? DBNull.Value);
                SqlParameter CS_DataSourceParam = new SqlParameter("@CS_DataSource", (object)entity.CS_DataSource ?? DBNull.Value);
                SqlParameter CS_InitialCatalogParam = new SqlParameter("@CS_InitialCatalog", (object)entity.CS_InitialCatalog ?? DBNull.Value);
                SqlParameter CS_PasswordParam = new SqlParameter("@CS_Password", (object)entity.CS_Password ?? DBNull.Value);
                SqlParameter CS_UserIdParam = new SqlParameter("@CS_UserId", (object)entity.CS_UserId ?? DBNull.Value);
                SqlParameter DateTypeIDParam = new SqlParameter("@DateTypeID", (object)entity.DateTypeID ?? DBNull.Value);
                SqlParameter DbCreationDateParam = new SqlParameter("@DbCreationDate", (object)entity.DbCreationDate ?? DBNull.Value);
                SqlParameter DbDescriptionNameParam = new SqlParameter("@DbDescriptionName", (object)entity.DbDescriptionName ?? DBNull.Value);
                SqlParameter DbEndPeriodDateParam = new SqlParameter("@DbEndPeriodDate", (object)entity.DbEndPeriodDate ?? DBNull.Value);
                SqlParameter DbFirstPeriodDateParam = new SqlParameter("@DbFirstPeriodDate", (object)entity.DbFirstPeriodDate ?? DBNull.Value);
                SqlParameter DELETEDORNOTParam = new SqlParameter("@DELETEDORNOT", (object)entity.DELETEDORNOT ?? DBNull.Value);
                SqlParameter IS_ACTIVATEDParam = new SqlParameter("@IS_ACTIVATED", (object)entity.IS_ACTIVATED ?? DBNull.Value);
                SqlParameter LanguageIDParam = new SqlParameter("@LanguageID", (object)entity.LanguageID ?? DBNull.Value);
                SqlParameter MaxUsersNumberParam = new SqlParameter("@MaxUsersNumber", (object)entity.MaxUsersNumber ?? DBNull.Value);
                SqlParameter SavePWDParam = new SqlParameter("@SavePWD", (object)entity.SavePWD ?? DBNull.Value);
                SqlParameter SHIFT_NUMBERParam = new SqlParameter("@SHIFT_NUMBER", (object)entity.SHIFT_NUMBER ?? DBNull.Value);
                SqlParameter UIDParam = new SqlParameter("@UID", (object)entity.UID ?? DBNull.Value);
                SqlParameter UpdatedByParam = new SqlParameter("@UpdatedBy", (object)entity.UpdatedBy ?? DBNull.Value);
                SqlParameter updatedOnParam = new SqlParameter("@updatedOn", (object)entity.updatedOn ?? DBNull.Value);
                SqlParameter UPWDParam = new SqlParameter("@UPWD", (object)entity.UPWD ?? DBNull.Value);
                SqlParameter UserGroupIDParam = new SqlParameter("@UserGroupID", (object)entity.UserGroupID ?? DBNull.Value);
                SqlParameter USER_LETTERParam = new SqlParameter("@USER_LETTER", (object)entity.USER_LETTER ?? DBNull.Value);
                SqlParameter USER_TYPEParam = new SqlParameter("@USER_TYPE", (object)entity.USER_TYPE ?? DBNull.Value);
                SqlParameter IsAdminParam = new SqlParameter("@IsAdmin", (object)entity.IsAdmin ?? DBNull.Value);
                var res = userRepo.ExecuteSqlCommand("exec Insert_Inner_AdminUser  @UID, @UPWD, @SavePWD, @SHIFT_NUMBER, @USER_LETTER, @USER_TYPE, @UserGroupID, @AddedBy, @AddedOn, @UpdatedBy, @updatedOn, @CS_DataSource, @CS_InitialCatalog, @CS_UserId, @CS_Password, @DbDescriptionName, @DbCreationDate, @DbFirstPeriodDate, @DbEndPeriodDate, @COMPANY_AR_NAME, @COMPANY_EN_NAME, @COMPANY_AR_ADRESS, @COMPANY_EN_ADRESS, @COMPANY_TELEPHONE, @COMPANY_FAX, @DELETEDORNOT, @COMPANY_EMAIL, @COMPANY_ACTIVATION_CODE, @IS_ACTIVATED, @MaxUsersNumber, @LanguageID, @DateTypeID, @ActivationRequestedDateTime, @IsAdmin", UIDParam, UPWDParam, SavePWDParam, SHIFT_NUMBERParam, USER_LETTERParam, USER_TYPEParam, UserGroupIDParam, AddedByParam, AddedOnParam, UpdatedByParam, updatedOnParam, CS_DataSourceParam, CS_InitialCatalogParam, CS_UserIdParam, CS_PasswordParam, DbDescriptionNameParam, DbCreationDateParam, DbFirstPeriodDateParam, DbEndPeriodDateParam, COMPANY_AR_NAMEParam, COMPANY_EN_NAMEParam, COMPANY_AR_ADRESSParam, COMPANY_EN_ADRESSParam, COMPANY_TELEPHONEParam, COMPANY_FAXParam, DELETEDORNOTParam, COMPANY_EMAILParam, COMPANY_ACTIVATION_CODEParam, IS_ACTIVATEDParam, MaxUsersNumberParam, LanguageIDParam, DateTypeIDParam, ActivationRequestedDateTimeParam, IsAdminParam);
                return res > 0;
            });
        }

        public bool Insert(UserVM entity)
        {
            User uid = new User
            {
                ID = entity.ID,
                UID = entity.UID,
                UPWD = entity.UPWD,
                CS_DataSource = entity.CS_DataSource,
                CS_InitialCatalog = entity.CS_InitialCatalog,
                CS_UserId = entity.CS_UserId,
                CS_Password = entity.CS_Password,
                AddedBy = entity.AddedBy,
                AddedOn = entity.AddedOn,
                COMPANY_AR_ADRESS = entity.COMPANY_AR_ADRESS,
                COMPANY_AR_NAME = entity.COMPANY_AR_NAME,
                COMPANY_EN_ADRESS = entity.COMPANY_EN_ADRESS,
                COMPANY_EN_NAME = entity.COMPANY_EN_NAME,
                COMPANY_FAX = entity.COMPANY_FAX,
                COMPANY_LOGO = entity.COMPANY_LOGO,
                COMPANY_TELEPHONE = entity.COMPANY_TELEPHONE,
                DbCreationDate = entity.DbCreationDate,
                DbDescriptionName = entity.DbDescriptionName,
                DbEndPeriodDate = entity.DbEndPeriodDate,
                DbFirstPeriodDate = entity.DbFirstPeriodDate,
                DELETEDORNOT = entity.DELETEDORNOT,
                SavePWD = entity.SavePWD,
                SHIFT_NUMBER = entity.SHIFT_NUMBER,
                UpdatedBy = entity.UpdatedBy,
                updatedOn = entity.updatedOn,
                UserGroupID = entity.UserGroupID,
                USER_LETTER = entity.USER_LETTER,
                USER_TYPE = entity.USER_TYPE,
                COMPANY_ACTIVATION_CODE = entity.COMPANY_ACTIVATION_CODE,
                COMPANY_EMAIL = entity.COMPANY_EMAIL,
                IS_ACTIVATED = entity.IS_ACTIVATED,
                MaxUsersNumber = entity.MaxUsersNumber,
                DateTypeID = entity.DateTypeID,
                LanguageID = entity.LanguageID,
                ActivationRequestedDateTime = entity.ActivationRequestedDateTime,
                IsAdmin = entity.IsAdmin
            };
            userRepo.Add(uid);
            return true;
        }
        public Task<bool> changeLanguage(string userName, int userID, int languageID)
        {
            return Task.Run<bool>(() =>
            {
                SqlParameter userIDParam = new SqlParameter("@ID", (object)userID);
                SqlParameter languageIDParam = new SqlParameter("@LangID", (object)languageID);
                SqlParameter userNameParam = new SqlParameter("@userName", (object)userName);
                var res = userRepo.ExecuteSqlCommand("exec Update_UID_ChangeLanguageID  @ID, @LangID, @userName", userIDParam, languageIDParam, userNameParam);
                return res > 0;
            });
        }
        public bool Update(UserVM entity)
        {
            User uid = new User
            {
                ID = entity.ID,
                UID = entity.UID,
                UPWD = entity.UPWD,
                CS_DataSource = entity.CS_DataSource,
                CS_InitialCatalog = entity.CS_InitialCatalog,
                CS_UserId = entity.CS_UserId,
                CS_Password = entity.CS_Password,
                AddedBy = entity.AddedBy,
                AddedOn = entity.AddedOn,
                COMPANY_AR_ADRESS = entity.COMPANY_AR_ADRESS,
                COMPANY_AR_NAME = entity.COMPANY_AR_NAME,
                COMPANY_EN_ADRESS = entity.COMPANY_EN_ADRESS,
                COMPANY_EN_NAME = entity.COMPANY_EN_NAME,
                COMPANY_FAX = entity.COMPANY_FAX,
                COMPANY_LOGO = entity.COMPANY_LOGO,
                COMPANY_TELEPHONE = entity.COMPANY_TELEPHONE,
                DbCreationDate = entity.DbCreationDate,
                DbDescriptionName = entity.DbDescriptionName,
                DbEndPeriodDate = entity.DbEndPeriodDate,
                DbFirstPeriodDate = entity.DbFirstPeriodDate,
                DELETEDORNOT = entity.DELETEDORNOT,
                SavePWD = entity.SavePWD,
                SHIFT_NUMBER = entity.SHIFT_NUMBER,
                UpdatedBy = entity.UpdatedBy,
                updatedOn = entity.updatedOn,
                UserGroupID = entity.UserGroupID,
                USER_LETTER = entity.USER_LETTER,
                USER_TYPE = entity.USER_TYPE,
                COMPANY_ACTIVATION_CODE = entity.COMPANY_ACTIVATION_CODE,
                COMPANY_EMAIL = entity.COMPANY_EMAIL,
                IS_ACTIVATED = entity.IS_ACTIVATED,
                MaxUsersNumber = entity.MaxUsersNumber,
                DateTypeID = entity.DateTypeID,
                LanguageID = entity.LanguageID,
                ActivationRequestedDateTime = entity.ActivationRequestedDateTime,
                IsAdmin = entity.IsAdmin
            };
            userRepo.Update(uid, uid.ID);
            return true;
        }

        public Task<bool> UpdateAsync(UserVM entity)
        {
            return Task.Run<bool>(() =>
            {
                User uid = new User
                {
                    ID = entity.ID,
                    UID = entity.UID,
                    UPWD = entity.UPWD,
                    CS_DataSource = entity.CS_DataSource,
                    CS_InitialCatalog = entity.CS_InitialCatalog,
                    CS_UserId = entity.CS_UserId,
                    CS_Password = entity.CS_Password,
                    AddedBy = entity.AddedBy,
                    AddedOn = entity.AddedOn,
                    COMPANY_AR_ADRESS = entity.COMPANY_AR_ADRESS,
                    COMPANY_AR_NAME = entity.COMPANY_AR_NAME,
                    COMPANY_EN_ADRESS = entity.COMPANY_EN_ADRESS,
                    COMPANY_EN_NAME = entity.COMPANY_EN_NAME,
                    COMPANY_FAX = entity.COMPANY_FAX,
                    COMPANY_LOGO = entity.COMPANY_LOGO,
                    COMPANY_TELEPHONE = entity.COMPANY_TELEPHONE,
                    DbCreationDate = entity.DbCreationDate,
                    DbDescriptionName = entity.DbDescriptionName,
                    DbEndPeriodDate = entity.DbEndPeriodDate,
                    DbFirstPeriodDate = entity.DbFirstPeriodDate,
                    DELETEDORNOT = entity.DELETEDORNOT,
                    SavePWD = entity.SavePWD,
                    SHIFT_NUMBER = entity.SHIFT_NUMBER,
                    UpdatedBy = entity.UpdatedBy,
                    updatedOn = entity.updatedOn,
                    UserGroupID = entity.UserGroupID,
                    USER_LETTER = entity.USER_LETTER,
                    USER_TYPE = entity.USER_TYPE,
                    COMPANY_ACTIVATION_CODE = entity.COMPANY_ACTIVATION_CODE,
                    COMPANY_EMAIL = entity.COMPANY_EMAIL,
                    IS_ACTIVATED = entity.IS_ACTIVATED,
                    MaxUsersNumber = entity.MaxUsersNumber,
                    DateTypeID = entity.DateTypeID,
                    LanguageID = entity.LanguageID,
                    ActivationRequestedDateTime = entity.ActivationRequestedDateTime,
                    IsAdmin = entity.IsAdmin
                };
                userRepo.Update(uid, uid.ID);
                return true;
            });
        }

        public bool Delete(UserVM entity)
        {
            User uid = new User
            {
                ID = entity.ID
            };
            userRepo.Delete(uid, entity.ID);
            return true;
        }

        public Task<bool> DeleteAsync(UserVM entity)
        {
            return Task.Run<bool>(() =>
            {
                User uid = new User
                {
                    ID = entity.ID
                };
                userRepo.Delete(uid, entity.ID);
                return true;
            });
        }

        public Task<List<UserVM>> GetAllAsync(int pageNum, int pageSize)
        {
            return Task.Run<List<UserVM>>(() =>
            {
                int rowCount;
                var q = from entity in userRepo.GetPaged<string>(pageNum, pageSize, p => p.UID, false, out rowCount)
                        select new UserVM
                        {
                            ID = entity.ID,
                            UID = entity.UID,
                            UPWD = entity.UPWD,
                            CS_DataSource = entity.CS_DataSource,
                            CS_InitialCatalog = entity.CS_InitialCatalog,
                            CS_UserId = entity.CS_UserId,
                            CS_Password = entity.CS_Password,
                            AddedBy = entity.AddedBy,
                            AddedOn = entity.AddedOn,
                            COMPANY_AR_ADRESS = entity.COMPANY_AR_ADRESS,
                            COMPANY_AR_NAME = entity.COMPANY_AR_NAME,
                            COMPANY_EN_ADRESS = entity.COMPANY_EN_ADRESS,
                            COMPANY_EN_NAME = entity.COMPANY_EN_NAME,
                            COMPANY_FAX = entity.COMPANY_FAX,
                            COMPANY_LOGO = entity.COMPANY_LOGO,
                            COMPANY_TELEPHONE = entity.COMPANY_TELEPHONE,
                            DbCreationDate = entity.DbCreationDate,
                            DbDescriptionName = entity.DbDescriptionName,
                            DbEndPeriodDate = entity.DbEndPeriodDate,
                            DbFirstPeriodDate = entity.DbFirstPeriodDate,
                            DELETEDORNOT = entity.DELETEDORNOT,
                            SavePWD = entity.SavePWD,
                            SHIFT_NUMBER = entity.SHIFT_NUMBER,
                            UpdatedBy = entity.UpdatedBy,
                            updatedOn = entity.updatedOn,
                            UserGroupID = entity.UserGroupID,
                            USER_LETTER = entity.USER_LETTER,
                            USER_TYPE = entity.USER_TYPE,
                            COMPANY_ACTIVATION_CODE = entity.COMPANY_ACTIVATION_CODE,
                            COMPANY_EMAIL = entity.COMPANY_EMAIL,
                            IS_ACTIVATED = entity.IS_ACTIVATED,
                            MaxUsersNumber = entity.MaxUsersNumber,
                            DateTypeID = entity.DateTypeID,
                            LanguageID = entity.LanguageID,
                            ActivationRequestedDateTime = entity.ActivationRequestedDateTime,
                            IsAdmin = entity.IsAdmin
                        };
                return q.ToList();
            });
        }

        public Task<int> CountAsync()
        {
            return Task.Run<int>(() =>
            {
                return userRepo.CountAsync();
            });
        }

        public List<UserVM> GetAll()
        {
            var q = from entity in userRepo.GetAll()
                    select new UserVM
                    {
                        ID = entity.ID,
                        UID = entity.UID,
                        UPWD = entity.UPWD,
                        CS_DataSource = entity.CS_DataSource,
                        CS_InitialCatalog = entity.CS_InitialCatalog,
                        CS_UserId = entity.CS_UserId,
                        CS_Password = entity.CS_Password,
                        AddedBy = entity.AddedBy,
                        AddedOn = entity.AddedOn,
                        COMPANY_AR_ADRESS = entity.COMPANY_AR_ADRESS,
                        COMPANY_AR_NAME = entity.COMPANY_AR_NAME,
                        COMPANY_EN_ADRESS = entity.COMPANY_EN_ADRESS,
                        COMPANY_EN_NAME = entity.COMPANY_EN_NAME,
                        COMPANY_FAX = entity.COMPANY_FAX,
                        COMPANY_LOGO = entity.COMPANY_LOGO,
                        COMPANY_TELEPHONE = entity.COMPANY_TELEPHONE,
                        DbCreationDate = entity.DbCreationDate,
                        DbDescriptionName = entity.DbDescriptionName,
                        DbEndPeriodDate = entity.DbEndPeriodDate,
                        DbFirstPeriodDate = entity.DbFirstPeriodDate,
                        DELETEDORNOT = entity.DELETEDORNOT,
                        SavePWD = entity.SavePWD,
                        SHIFT_NUMBER = entity.SHIFT_NUMBER,
                        UpdatedBy = entity.UpdatedBy,
                        updatedOn = entity.updatedOn,
                        UserGroupID = entity.UserGroupID,
                        USER_LETTER = entity.USER_LETTER,
                        USER_TYPE = entity.USER_TYPE,
                        COMPANY_ACTIVATION_CODE = entity.COMPANY_ACTIVATION_CODE,
                        COMPANY_EMAIL = entity.COMPANY_EMAIL,
                        IS_ACTIVATED = entity.IS_ACTIVATED,
                        MaxUsersNumber = entity.MaxUsersNumber,
                        DateTypeID = entity.DateTypeID,
                        LanguageID = entity.LanguageID,
                        ActivationRequestedDateTime = entity.ActivationRequestedDateTime,
                        IsAdmin = entity.IsAdmin
                    };
            return q.ToList();
        }
        public string CopyDataBase(string BackUpFile, string DatabaseFilePath, string _databaseName)
        {
            return commonRepo.CopyDataBase(BackUpFile, DatabaseFilePath, _databaseName);
        }

        public string GetNextDatabaseName()
        {
            return commonRepo.GetNextDatabaseName();
        }

        public string GetCompanyNameByUserID(int ID)
        {
            User user = userRepo.GetByID(ID);
            return user.COMPANY_AR_NAME;
        }
        //public void uploadFile(byte[] file)
        //{
        //    byte[] ff = file;

        //}

        public Task<string> GetUserType()
        {
            return Task.Run<string>(() =>
            {
                //Get the current claims principal
                var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;
                // Get the claims values
                var type = identity.Claims.Where(c => c.Type == "USER_TYPE").Select(c => c.Value).SingleOrDefault();

                if (type == null)
                {
                    return "Admin";
                }

                else if (Convert.ToInt32(type) == 3 || Convert.ToInt32(type) == 4)
                {
                    return "Cachier";
                }

                else
                {
                    return "Admin";
                }
            });
        }


        public Task<bool> SendMail(string sendFrom, string s_password, string sendTo, string emailSubject, string emailBody, FileUpload Attachment, string Host, int Port, LinkedResource inlineLogo)
        {
            return Task.Run(() =>
            {
                //string sendFrom = WebConfigurationManager.AppSettings["email"].ToString();
                MailAddress ma_from = new MailAddress(sendFrom, "ResortERP");
                MailAddress ma_to = new MailAddress(sendTo, null);

                //string s_password = WebConfigurationManager.AppSettings["ePass"].ToString(); ;
                string s_subject = emailSubject;
                string s_body = emailBody;

                SmtpClient smtp = new SmtpClient
                {
                    Host = Host,
                    //Host = "smtpout.secureserver.net",
                    //change the port to prt 587. This seems to be the standard for Google smtp transmissions.
                    Port = Port,
                    //Port = 80,
                    //enable SSL to be true, otherwise it will get kicked back by the Google server.
                    EnableSsl = false,
                    //The following properties need set as well
                    DeliveryMethod = SmtpDeliveryMethod.Network,

                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(ma_from.Address, s_password)
                };



                using (MailMessage mail = new MailMessage(ma_from, ma_to) { Subject = s_subject, Body = s_body })
                {
                    try
                    {
                        mail.IsBodyHtml = true;
                        mail.CC.Add(ma_from);

                        var view = AlternateView.CreateAlternateViewFromString(s_body, null, "text/html");
                        view.LinkedResources.Add(inlineLogo);
                        mail.AlternateViews.Add(view);
                        smtp.UseDefaultCredentials = false;
                       // smtp.EnableSsl = true;
                        smtp.Send(mail);
                        return true;
                    }
                    catch (Exception ex)
                    {
                        string str = ex.InnerException.Data.ToString();
                        return false;
                    }
                }
            });
        }

        public Task<bool> ReceiveMail(string sendFrom, string sendTo, string s_password, string emailR, string nameR, string emailSubject, string emailBody, string Host, int Port)
        {
            return Task.Run(() =>
            {
                //string sendFrom = WebConfigurationManager.AppSettings["email"].ToString();
                MailAddress ma_from = new MailAddress(sendFrom, nameR + " [" + emailR + "] ");
                MailAddress ma_to = new MailAddress(sendTo, "Info");

                //string s_password = WebConfigurationManager.AppSettings["ePass"].ToString(); ;
                string s_subject = emailSubject;
                string s_body = emailBody;

                SmtpClient smtp = new SmtpClient
                {
                    Host = Host,
                    //change the port to prt 587. This seems to be the standard for Google smtp transmissions.
                    Port = Port,
                    //enable SSL to be true, otherwise it will get kicked back by the Google server.
                    EnableSsl = false,
                    //The following properties need set as well
                    DeliveryMethod = SmtpDeliveryMethod.Network,

                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(ma_from.Address, s_password)
                };
                using (MailMessage mail = new MailMessage(ma_from, ma_to) { Subject = s_subject, Body = s_body })
                {
                    try
                    {
                        mail.CC.Add(ma_from);
                        smtp.Send(mail);
                        return true;
                    }
                    catch (Exception ex)
                    {
                        string str = ex.InnerException.Data.ToString();
                        return false;
                    }
                }
            });
        }

        //Get All Users For Menus
        public List<UserVM> GetAllUsers()
        {
            var q = (from p in userRepo.GetAll()
                     select p).Select(x => Mapper.Map<User, UserVM>(x)).ToList();
            return q;
        }

        public Task<bool> ValidateCurrentPassword(int userID, string UserName, string password)
        {
            return Task.Run<bool>(() =>
            {
                var user = getUser(UserName);
                if (user.UPWD == password) { return true; }
                else { return false; }
            });
        }

        public Task<bool> changePassword(int userID, string userName, string password)
        {
            return Task.Run<bool>(() =>
            {
                SqlParameter userIDParam = new SqlParameter("@userID", (object)userID ?? DBNull.Value);
                SqlParameter userNameParam = new SqlParameter("@userName", (object)userName ?? DBNull.Value);
                SqlParameter passwordParam = new SqlParameter("@password", (object)password ?? DBNull.Value);
                var res = userRepo.ExecuteSqlCommand("exec ChangePassword @userID, @userName, @password", userIDParam, userNameParam, passwordParam);
                return res > 0;
            });
        }

        public Task<UserVM> GetUserData(int userID)
        {
            return Task.Run<UserVM>(() =>
            {
                var user = userRepo.Filter(X => X.ID == userID).Select(x => Mapper.Map<User, UserVM>(x)).FirstOrDefault();
                return user;
            });
        }



        public UserVM getUserByUserName (string userName)
        {
            var user = from u in userRepo.GetAll().Where(u => u.UID == userName)
                       select new UserVM
                       {
                           ID = u.ID,
                           UID= u.UID
                       };
            return user.FirstOrDefault();
           
        }
    }
}
