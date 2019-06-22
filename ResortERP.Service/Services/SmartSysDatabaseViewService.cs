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
    public class SmartSysDatabaseViewService : ISmartSysDatabaseViewService, IDisposable
    {
        IGenericRepository<SmartSysDataBases_View> ssdRepo;

        public SmartSysDatabaseViewService(IGenericRepository<SmartSysDataBases_View> ssdRepo)
        {
            this.ssdRepo = ssdRepo;
        }

        public Task<int> CountAsync()
        {
            return Task.Run<int>(() =>
            {
                return ssdRepo.CountAsync();
            });
        }

        public void Dispose()
        {
            ssdRepo.Dispose();
        }

        public List<SmartSysDataBases_View> GetAll()
        {
            var q = from entity in ssdRepo.GetAll()
                    select new SmartSysDataBases_View
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
                        DBID = entity.DBID,
                        DbName = entity.DbName,
                        DELETEDORNOT = entity.DELETEDORNOT
                    };
            return q.ToList();
        }

        public List<SmartSysDataBases_View> GetBy(string DatabaseName)
        {
            var q = from entity in ssdRepo.GetAll().Where(x => (x.DbName == DatabaseName))
                    select new SmartSysDataBases_View
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
                        DBID = entity.DBID,
                        DbName = entity.DbName,
                        DELETEDORNOT = entity.DELETEDORNOT
                    };
            return q.ToList();
        }

        public Task<List<SmartSysDataBases_View>> GetAllAsync(int pageNum, int pageSize)
        {
            return Task.Run(() =>
            {
                int rowCount;
                var q = from entity in ssdRepo.GetPaged<int>(pageNum, pageSize, p => p.DBID, false, out rowCount)
                        select new SmartSysDataBases_View
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
                            DBID = entity.DBID,
                            DbName = entity.DbName,
                            DELETEDORNOT = entity.DELETEDORNOT
                        };
                return q.ToList();
            });
        }
    }
}
