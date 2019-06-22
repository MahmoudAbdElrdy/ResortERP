using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ResortERP.Core.VM;
using ResortERP.Repository;
using ResortERP.Core;
using ResortERP.Service.IServices;
using System.Drawing.Printing;

namespace ResortERP.Service.Services
{
    public class CommonService : ICommonService, IDisposable
    {
        IGenericRepository<COMPANY_BRANCHES> companyBranchRepo;
        IGenericRepository<EMPLOYEES> employeeRepo;
        IGenericRepository<NATIONS> nationRepo;
        IGenericRepository<GOVERNORATES> governateRepo;
        IGenericRepository<TOWNS> townRepo;
        IGenericRepository<VILLAGES> villageRepo;
        IGenericRepository<Department> departmentRepo;
        IGenericRepository<UserModule> userModuleRepo;

        public CommonService(IGenericRepository<COMPANY_BRANCHES> companyBranchRepo, IGenericRepository<EMPLOYEES> employeeRepo, IGenericRepository<NATIONS> nationRepo, IGenericRepository<GOVERNORATES> governateRepo, IGenericRepository<TOWNS> townRepo, 
            IGenericRepository<VILLAGES> villageRepo, IGenericRepository<Department> departmentRepo,
             IGenericRepository<UserModule> userModuleRepo)
        {
            this.companyBranchRepo = companyBranchRepo;
            this.employeeRepo = employeeRepo;
            this.nationRepo = nationRepo;
            this.governateRepo = governateRepo;
            this.townRepo = townRepo;
            this.villageRepo = villageRepo;
            this.departmentRepo = departmentRepo;
            this.userModuleRepo = userModuleRepo;
        }


        public void Dispose()
        {
            companyBranchRepo.Dispose();
            employeeRepo.Dispose();
            nationRepo.Dispose();
            governateRepo.Dispose();
            townRepo.Dispose();
            villageRepo.Dispose();
            departmentRepo.Dispose();
        }


        public List<string> GetInstalledPrinters()
        {
            List<string> printers = new List<string>();
            foreach (String printer in PrinterSettings.InstalledPrinters)
            {
                printers.Add(printer.ToString());
            }
            return printers;
        }

        public List<COMPANY_BRANCHESVM> GetAllCompanyBranchesAsync()
        {
            var q = from p in companyBranchRepo.GetAll()
                    select new COMPANY_BRANCHESVM
                    {
                        AddedBy = p.AddedBy,
                        AddedOn = p.AddedOn,
                        COM_BRN_ID = p.COM_BRN_ID,
                        Disable = p.Disable,
                        GOV_ID = p.GOV_ID,
                        NATION_ID = p.NATION_ID,
                        TOWN_ID = p.TOWN_ID,
                        UpdatedBy = p.UpdatedBy,
                        updatedOn = p.updatedOn,
                        VILLAGE_ID = p.VILLAGE_ID,
                        COM_BRN_ADDR_REMARKS = p.COM_BRN_ADDR_REMARKS,
                        COM_BRN_AR_ABRV = p.COM_BRN_ADDR_REMARKS,
                        COM_BRN_AR_NAME = p.COM_BRN_AR_NAME,
                        COM_BRN_CODE = p.COM_BRN_CODE,
                        COM_BRN_EN_ABRV = p.COM_BRN_EN_ABRV,
                        COM_BRN_EN_NAME = p.COM_BRN_EN_NAME,
                        COM_BRN_REMARKS = p.COM_BRN_REMARKS,
                        COM_MASTER_BRN_ID = p.COM_MASTER_BRN_ID
                    };
            return q.ToList();
        }
        public List<int> GetUserModule(string Id)
        {
            var x= userModuleRepo.GetAll().Where(a => a.UID == Id).Select(a => a.ID).ToList();
            return x;

        }
        public List<EMPLOYEEVM> GetAllEmployeesAsync()
        {
            var q = from p in employeeRepo.GetAll()
                    select new EMPLOYEEVM
                    {
                        ACC_ID = p.ACC_ID,
                        AddedBy = p.AddedBy,
                        AddedOn = p.AddedOn,
                        COM_BRN_ID = p.COM_BRN_ID,
                        DEPT_ID = p.DEPT_ID,
                        Disable = p.Disable,
                        EMP_ADDR_REMARKS = p.EMP_ADDR_REMARKS,
                        EMP_AR_NAME = p.EMP_AR_NAME,
                        EMP_BARCODE = p.EMP_BARCODE,
                        EMP_BARCODE_IMAGE = p.EMP_BARCODE_IMAGE,
                        EMP_CODE = p.EMP_CODE,
                        EMP_EN_NAME = p.EMP_EN_NAME,
                        EMP_ID = p.EMP_ID,
                        EMP_PHOTO = p.EMP_PHOTO,
                        EMP_NATIONAL_ID = p.EMP_NATIONAL_ID,
                        EMP_REMARKS = p.EMP_REMARKS,
                        EMP_STATE = p.EMP_STATE,
                        EMP_TYPE_ID = p.EMP_TYPE_ID,
                        GOV_ID = p.GOV_ID,
                        NATIONALITY_ID = p.NATIONALITY_ID,
                        NATION_ID = p.NATION_ID,
                        TOWN_ID = p.TOWN_ID,
                        UpdatedBy = p.UpdatedBy,
                        updatedOn = p.updatedOn,
                        VILLAGE_ID = p.VILLAGE_ID
                    };
            return q.ToList();
        }

        public List<GOVERNORATEVM> GetAllGovernateAsync()
        {
            var q = from p in governateRepo.GetAll()
                    select new GOVERNORATEVM
                    {
                        AddedBy = p.AddedBy,
                        AddedOn = p.AddedOn,
                        Disable = p.Disable,
                        GOV_AR_NAME = p.GOV_AR_NAME,
                        GOV_EN_NAME = p.GOV_EN_NAME,
                        GOV_ID = p.GOV_ID,
                        NATION_ID = p.NATION_ID,
                        UpdatedBy = p.UpdatedBy,
                        updatedOn = p.updatedOn

                    };
            return q.ToList();
        }

        public List<NATIONVM> GetAllNationsAsync()
        {
            var q = from p in nationRepo.GetAll()
                    select new NATIONVM
                    {
                        AddedBy = p.AddedBy,
                        AddedOn = p.AddedOn,
                        Disable = p.Disable,
                        NATIONALITY_AR_NAME = p.NATIONALITY_AR_NAME,
                        NATIONALITY_EN_NAME = p.NATIONALITY_EN_NAME,
                        NATION_AR_NAME = p.NATION_AR_NAME,
                        NATION_EN_NAME = p.NATION_EN_NAME,
                        NATION_ID = p.NATION_ID,
                        UpdatedBy = p.UpdatedBy,
                        updatedOn = p.updatedOn
                    };
            return q.ToList();
        }

        public List<TOWNVM> GetAllTownsAsync()
        {
            var q = from p in townRepo.GetAll()
                    select new TOWNVM
                    {
                        AddedBy = p.AddedBy,
                        AddedOn = p.AddedOn,
                        Disable = p.Disable,
                        GOV_ID = p.GOV_ID,
                        TOWN_AR_NAME = p.TOWN_AR_NAME,
                        TOWN_EN_NAME = p.TOWN_EN_NAME,
                        TOWN_ID = p.TOWN_ID,
                        UpdatedBy = p.UpdatedBy,
                        updatedOn = p.updatedOn
                    };
            return q.ToList();
        }

        public List<VILLAGEVM> GetAllVillagesAsync()
        {
            var q = from p in villageRepo.GetAll()
                    select new VILLAGEVM
                    {
                        AddedBy = p.AddedBy,
                        AddedOn = p.AddedOn,
                        Disable = p.Disable,
                        TOWN_ID = p.TOWN_ID,
                        UpdatedBy = p.UpdatedBy,
                        updatedOn = p.updatedOn,
                        VILLAGE_AR_NAME = p.VILLAGE_AR_NAME,
                        VILLAGE_EN_NAME = p.VILLAGE_EN_NAME,
                        VILLAGE_ID = p.VILLAGE_ID
                    };
            return q.ToList();
        }

        public List<GOVERNORATEVM> GetGovernatesByNationID(int NationID)
        {
            var q = from p in governateRepo.GetAll().Where(x => x.NATION_ID == NationID)
                    select new GOVERNORATEVM
                    {
                        AddedBy = p.AddedBy,
                        AddedOn = p.AddedOn,
                        Disable = p.Disable,
                        GOV_AR_NAME = p.GOV_AR_NAME,
                        GOV_EN_NAME = p.GOV_EN_NAME,
                        GOV_ID = p.GOV_ID,
                        NATION_ID = p.NATION_ID,
                        UpdatedBy = p.UpdatedBy,
                        updatedOn = p.updatedOn

                    };
            return q.ToList();
        }

        public List<TOWNVM> GetTownsByGovernateID(int GovernateID)
        {
            var q = from p in townRepo.GetAll().Where(x => x.GOV_ID == GovernateID)
                    select new TOWNVM
                    {
                        AddedBy = p.AddedBy,
                        AddedOn = p.AddedOn,
                        Disable = p.Disable,
                        GOV_ID = p.GOV_ID,
                        TOWN_AR_NAME = p.TOWN_AR_NAME,
                        TOWN_EN_NAME = p.TOWN_EN_NAME,
                        TOWN_ID = p.TOWN_ID,
                        UpdatedBy = p.UpdatedBy,
                        updatedOn = p.updatedOn
                    };
            return q.ToList();
        }

        public List<VILLAGEVM> GetVillageByTownID(int TownID)
        {
            var q = from p in villageRepo.GetAll().Where(x => x.TOWN_ID == TownID)
                    select new VILLAGEVM
                    {
                        AddedBy = p.AddedBy,
                        AddedOn = p.AddedOn,
                        Disable = p.Disable,
                        TOWN_ID = p.TOWN_ID,
                        UpdatedBy = p.UpdatedBy,
                        updatedOn = p.updatedOn,
                        VILLAGE_AR_NAME = p.VILLAGE_AR_NAME,
                        VILLAGE_EN_NAME = p.VILLAGE_EN_NAME,
                        VILLAGE_ID = p.VILLAGE_ID
                    };
            return q.ToList(); ;
        }

        public List<DepartmentVM> GetDepartmentByCompanyBranchID(int CompanyBranchID)
        {
            var q = from entity in departmentRepo.GetAll().Where(x => x.COM_BRN_ID == CompanyBranchID)
                    select new DepartmentVM
                    {
                        DEPT_ID = entity.DEPT_ID,
                        DEPT_AR_ABRV = entity.DEPT_AR_ABRV,
                        DEPT_AR_NAME = entity.DEPT_AR_NAME,
                        DEPT_CODE = entity.DEPT_CODE,
                        DEPT_EN_ABRV = entity.DEPT_EN_ABRV,
                        DEPT_EN_NAME = entity.DEPT_EN_NAME,
                        COM_BRN_ID = entity.COM_BRN_ID,
                        AddedBy = entity.AddedBy,
                        AddedOn = entity.AddedOn,
                        Disable = entity.Disable,
                        UpdatedBy = entity.UpdatedBy,
                        updatedOn = entity.updatedOn
                    };
            return q.ToList(); ;
        }

        public string GetCompanyBranchNameByID(int BranID)
        {
            string str = companyBranchRepo.GetByID(BranID) == null ? "" : companyBranchRepo.GetByID(BranID).COM_BRN_AR_NAME.ToString();
            return str;
        }

        public string GetEmployeeNameByID(int EmpID)
        {
            string str = employeeRepo.GetByID(EmpID) == null ? "" : employeeRepo.GetByID(EmpID).EMP_AR_NAME.ToString();
            return str;
        }

        public string GetLoggedDatabaseName()
        {
            return employeeRepo.GetLoggedDatabaseName();
        }
        public Task<ConnectionString> GetLoggedDatabase()
        {
            return Task.Run(() =>
            {
                return employeeRepo.GetLoggedDatabase();
            });
        }
    }
}
