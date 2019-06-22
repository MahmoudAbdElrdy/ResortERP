using ResortERP.Core;
using ResortERP.Core.VM;
using ResortERP.Repository;
using ResortERP.Service.IServices;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortERP.Service.Services
{
    public class UIDViewService : IUIDViewService, IDisposable
    {
        IGenericRepository<UID_View> uidRepo;

        public UIDViewService(IGenericRepository<UID_View> uidRepo)
        {
            this.uidRepo = uidRepo;
        }

        public Task<int> CountAsync()
        {
            return Task.Run<int>(() =>
            {
                return uidRepo.CountAsync();
            });
        }

        public void Dispose()
        {
            uidRepo.Dispose();
        }

        public List<UID_View> GetAll()
        {
            var q = from entity in uidRepo.GetAll()
                    select new UID_View
                    {
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
                        AddedBy = entity.AddedBy,
                        AddedOn = entity.AddedOn,
                        COMPANY_ACTIVATION_CODE = entity.COMPANY_ACTIVATION_CODE,
                        COMPANY_EMAIL = entity.COMPANY_EMAIL,
                        CS_DataSource = entity.CS_DataSource,
                        CS_InitialCatalog = entity.CS_InitialCatalog,
                        CS_Password = entity.CS_Password,
                        CS_UserId = entity.CS_UserId,
                        ID = entity.ID,
                        IS_ACTIVATED = entity.IS_ACTIVATED,
                        MaxUsersNumber = entity.MaxUsersNumber,
                        SavePWD = entity.SavePWD,
                        SHIFT_NUMBER = entity.SHIFT_NUMBER,
                        UID = entity.UID,
                        UpdatedBy = entity.UpdatedBy,
                        updatedOn = entity.updatedOn,
                        UPWD = entity.UPWD,
                        UserGroupID = entity.UserGroupID,
                        USER_LETTER = entity.USER_LETTER,
                        USER_TYPE = entity.USER_TYPE
                    };
            return q.ToList();
        }

        public Task<UID_View> GetBy(string DatabaseName)
        {
            return Task.Run(() =>
            {
                var q = from entity in uidRepo.GetAll().Where(x => (x.CS_InitialCatalog == DatabaseName))
                        select new UID_View
                        {
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
                            AddedBy = entity.AddedBy,
                            AddedOn = entity.AddedOn,
                            COMPANY_ACTIVATION_CODE = entity.COMPANY_ACTIVATION_CODE,
                            COMPANY_EMAIL = entity.COMPANY_EMAIL,
                            CS_DataSource = entity.CS_DataSource,
                            CS_InitialCatalog = entity.CS_InitialCatalog,
                            CS_Password = entity.CS_Password,
                            CS_UserId = entity.CS_UserId,
                            ID = entity.ID,
                            IS_ACTIVATED = entity.IS_ACTIVATED,
                            MaxUsersNumber = entity.MaxUsersNumber,
                            SavePWD = entity.SavePWD,
                            SHIFT_NUMBER = entity.SHIFT_NUMBER,
                            UID = entity.UID,
                            UpdatedBy = entity.UpdatedBy,
                            updatedOn = entity.updatedOn,
                            UPWD = entity.UPWD,
                            UserGroupID = entity.UserGroupID,
                            USER_LETTER = entity.USER_LETTER,
                            USER_TYPE = entity.USER_TYPE
                        };
                return q.ToList().FirstOrDefault();
            });
        }

        public Task<UID_View> GetCompanyAsync(string DatabaseName)
        {
            return Task.Run(() =>
            {
                SqlParameter DatabaseNAmeParam = new SqlParameter("@DataBaseName", (object)DatabaseName);

                var CompanyUser = uidRepo.SQLQuery<UID_View>("exec GetCompanyUser  @DataBaseName", DatabaseNAmeParam).ToList<UID_View>().FirstOrDefault();
                return CompanyUser;
            });
        }

        public UID_View GetCompany(string DatabaseName)
        {
            SqlParameter DatabaseNAmeParam = new SqlParameter("@DataBaseName", (object)DatabaseName);

            var CompanyUser = uidRepo.SQLQuery<UID_View>("exec GetCompanyUser  @DataBaseName", DatabaseNAmeParam).ToList<UID_View>().FirstOrDefault();
            return CompanyUser;
        }


        public Task<byte[]> GetByLOGO(string DatabaseName)
        {
            return Task.Run(() =>
            {
                SqlParameter DatabaseNAmeParam = new SqlParameter("@DataBaseName", (object)DatabaseName);

                var CompanyLogo = uidRepo.SQLQuery<byte[]>("exec GetCompanyLOGO  @DataBaseName", DatabaseNAmeParam).ToList<byte[]>().FirstOrDefault();
                return CompanyLogo;
            });
        }

        public Task<List<UID_View>> GetAllAsync(int pageNum, int pageSize)
        {
            return Task.Run(() =>
            {
                int rowCount;
                var q = from entity in uidRepo.GetPaged<int>(pageNum, pageSize, p => p.ID, false, out rowCount)
                        select new UID_View
                        {
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
                            AddedBy = entity.AddedBy,
                            AddedOn = entity.AddedOn,
                            COMPANY_ACTIVATION_CODE = entity.COMPANY_ACTIVATION_CODE,
                            COMPANY_EMAIL = entity.COMPANY_EMAIL,
                            CS_DataSource = entity.CS_DataSource,
                            CS_InitialCatalog = entity.CS_InitialCatalog,
                            CS_Password = entity.CS_Password,
                            CS_UserId = entity.CS_UserId,
                            ID = entity.ID,
                            IS_ACTIVATED = entity.IS_ACTIVATED,
                            MaxUsersNumber = entity.MaxUsersNumber,
                            SavePWD = entity.SavePWD,
                            SHIFT_NUMBER = entity.SHIFT_NUMBER,
                            UID = entity.UID,
                            UpdatedBy = entity.UpdatedBy,
                            updatedOn = entity.updatedOn,
                            UPWD = entity.UPWD,
                            UserGroupID = entity.UserGroupID,
                            USER_LETTER = entity.USER_LETTER,
                            USER_TYPE = entity.USER_TYPE
                        };
                return q.ToList();
            });
        }
    }
}
