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
    public class EmployeeService : IEmployeeService, IDisposable
    {
        IGenericRepository<EMPLOYEES> employeeRepo;
        IGenericRepository<HrPslEmployee> EmployeeRep;
        IGenericRepository<HrPslMartialStatus> MartialStatusRep;
        IGenericRepository<Department> DepartmentRep;
        IGenericRepository<HrPslCity> CityRep;
        IGenericRepository<GlBank> BankRep;
        IGenericRepository<HrPslReligion> ReligionRep;
        IGenericRepository<HrPslNationality> NationalityRep;
        IGenericRepository<HrPslJobTitle> JobTitleRep;
        IGenericRepository<HrPslEmployeeStatus> EmployeeStatusRep;
        IGenericRepository<HrPslEmployeeJobLevel> EmployeeJobLevelRep;
        IGenericRepository<HrPslContactType> ContactTypeRep;
        IGenericRepository<HrPslAcademicDegree> AcademicDegreeRep;
        IGenericRepository<HrPslEmployeeAcademicDegree> EmployeeAcademicDegreeRep;
        IGenericRepository<HrPslEmployeeExperience> EmployeeExperienceRep;
        IGenericRepository<HrPslEmployeeFamilyDetails> EmployeeFamilyDetailsRep;
        IGenericRepository<HrPslEmployeeContacts> EmployeeContactsRep;
        IGenericRepository<HrPslEmployeeTrainingCources> EmployeeTrainingCourcesRep;

        public EmployeeService(IGenericRepository<EMPLOYEES> employeeRepo, IGenericRepository<HrPslEmployee> EmployeeRep,
            IGenericRepository<HrPslMartialStatus> MartialStatusRep, IGenericRepository<Department> DepartmentRep,
            IGenericRepository<HrPslCity> CityRep,
            IGenericRepository<GlBank> BankRep, IGenericRepository<HrPslReligion> ReligionRep,
            IGenericRepository<HrPslNationality> NationalityRep, IGenericRepository<HrPslJobTitle> JobTitleRep,
            IGenericRepository<HrPslEmployeeStatus> EmployeeStatusRep, IGenericRepository<HrPslEmployeeJobLevel> EmployeeJobLevelRep,
            IGenericRepository<HrPslContactType> ContactTypeRep, IGenericRepository<HrPslAcademicDegree> AcademicDegreeRep,
            IGenericRepository<HrPslEmployeeAcademicDegree> EmployeeAcademicDegreeRep, IGenericRepository<HrPslEmployeeExperience> EmployeeExperienceRep,
            IGenericRepository<HrPslEmployeeFamilyDetails> EmployeeFamilyDetailsRep, IGenericRepository<HrPslEmployeeContacts> EmployeeContactsRep,
            IGenericRepository<HrPslEmployeeTrainingCources> EmployeeTrainingCourcesRep)
        {
            this.employeeRepo = employeeRepo;
            this.EmployeeRep = EmployeeRep;
            this.MartialStatusRep = MartialStatusRep;
            this.DepartmentRep = DepartmentRep;
            this.CityRep = CityRep;
            this.BankRep = BankRep;
            this.ReligionRep = ReligionRep;
            this.NationalityRep = NationalityRep;
            this.JobTitleRep = JobTitleRep;
            this.EmployeeStatusRep = EmployeeStatusRep;
            this.EmployeeJobLevelRep = EmployeeJobLevelRep;
            this.ContactTypeRep = ContactTypeRep;
            this.AcademicDegreeRep = AcademicDegreeRep;

            this.EmployeeAcademicDegreeRep = EmployeeAcademicDegreeRep;
            this.EmployeeExperienceRep = EmployeeExperienceRep;
            this.EmployeeFamilyDetailsRep = EmployeeFamilyDetailsRep;
            this.EmployeeContactsRep = EmployeeContactsRep;
            this.EmployeeTrainingCourcesRep = EmployeeTrainingCourcesRep;
        }

        public Task<int> CountAsync()
        {
            return Task.Run<int>(() =>
            {
                return EmployeeRep.CountAsync();
            });
        }

        public bool Delete(EMPLOYEEVM entity)
        {
            EMPLOYEES emp = new EMPLOYEES
            {
                ACC_ID = entity.ACC_ID,
                AddedBy = entity.AddedBy,
                AddedOn = entity.AddedOn,
                COM_BRN_ID = entity.COM_BRN_ID,
                DEPT_ID = entity.DEPT_ID,
                Disable = entity.Disable,
                EMP_ADDR_REMARKS = entity.EMP_ADDR_REMARKS,
                EMP_AR_NAME = entity.EMP_AR_NAME,
                EMP_BARCODE = entity.EMP_BARCODE,
                EMP_BARCODE_IMAGE = entity.EMP_BARCODE_IMAGE,
                EMP_CODE = entity.EMP_CODE,
                EMP_EN_NAME = entity.EMP_EN_NAME,
                EMP_ID = entity.EMP_ID,
                EMP_NATIONAL_ID = entity.EMP_NATIONAL_ID,
                EMP_PHOTO = entity.EMP_PHOTO,
                EMP_REMARKS = entity.EMP_REMARKS,
                EMP_STATE = entity.EMP_STATE,
                EMP_TYPE_ID = entity.EMP_TYPE_ID,
                GOV_ID = entity.GOV_ID,
                NATIONALITY_ID = entity.NATIONALITY_ID,
                NATION_ID = entity.NATION_ID,
                TOWN_ID = entity.TOWN_ID,
                UpdatedBy = entity.UpdatedBy,
                updatedOn = entity.updatedOn,
                VILLAGE_ID = entity.VILLAGE_ID,
                Visa_Number = entity.Visa_Number,
                Pass_Number = entity.Pass_Number,
                Visa_ExpDateM = entity.Visa_ExpDateM,
                Visa_ExpDateH = entity.Visa_ExpDateH,
                Pass_ExpDate = entity.Pass_ExpDate
            };
            employeeRepo.Delete(emp, entity.EMP_ID);

            //todo:Remove this after full integration with HR project
            DeleteEmployeeOnHRProjectEmployeeTable(emp.EMP_ID);
            return true;
        }

        public Task<bool> DeleteAsync(HrPslEmployeeVM entity)
        {
            return Task.Run<bool>(() =>
            {
                HrPslEmployee Employee = new HrPslEmployee
                {
                    HrPslEmployeeID = entity.HrPslEmployeeID,
                    EmployeeCode = entity.EmployeeCode,
                    EmployeeFirstName = entity.EmployeeFirstName,
                    EmployeeFirstNameEn = entity.EmployeeFirstNameEn,
                    EmployeeLastName = entity.EmployeeLastName,
                    EmployeeLastNameEn = entity.EmployeeLastNameEn,
                    DateOfJoin = entity.DateOfJoin,
                    HrPslJobTitleID = entity.HrPslJobTitleID,
                    HrPslBranchID = entity.HrPslBranchID,
                    HrPslDeptartmentID = entity.HrPslDeptartmentID,
                    HrPslContractTypeID = entity.HrPslContractTypeID,
                    HrPslEmployeeStatusID = entity.HrPslEmployeeStatusID,
                    WorkMobileNum = entity.WorkMobileNum,
                    DirectManagerID = entity.DirectManagerID,
                    HrPslEmployeeJobLevelID = entity.HrPslEmployeeJobLevelID,
                    HrPslManagementId = entity.HrPslManagementId,
                    GenderID = entity.GenderID,
                    HrPslMaritalstatusID = entity.HrPslMaritalstatusID,
                    HrPslReligionID = entity.HrPslReligionID,
                    HrPslCountyID = entity.HrPslCountyID,
                    HrPslCityID = entity.HrPslCityID,
                    Address = entity.Address,
                    HrPslNationalityID = entity.HrPslNationalityID,
                    NationalID = entity.NationalID,
                    NationalIdExpData = entity.NationalIdExpData,
                    PassportNo = entity.PassportNo,
                    PassportNoExpDate = entity.PassportNoExpDate,
                    DateOfBirth = entity.DateOfBirth,
                    ResidenceType = entity.ResidenceType,
                    ResidencyNumber = entity.ResidencyNumber,
                    ResidenceExpireDate = entity.ResidenceExpireDate,
                    DrivingLicenseNumber = entity.DrivingLicenseNumber,
                    DrivingLicenseIssueDate = entity.DrivingLicenseIssueDate,
                    DrivingLicenseExpireDate = entity.DrivingLicenseExpireDate,
                    DrivingLicenseIssuer = entity.DrivingLicenseIssuer,
                    DrivingLicenseType = entity.DrivingLicenseType,
                    HasCar = entity.HasCar,
                    GlBankID = entity.GlBankID,
                    BankAccountNumber = entity.BankAccountNumber,
                    HasChronicDisease = entity.HasChronicDisease,
                    ChronicDiseaseDescreption = entity.ChronicDiseaseDescreption,
                    HasInjuries = entity.HasInjuries,
                    InjuriesDescreption = entity.InjuriesDescreption,
                    BloodGroup = entity.BloodGroup
                };

                //Remove all related data to employee
                EmployeeAcademicDegreeRep.DeleteRange(EmployeeAcademicDegreeRep.FilterAsync(p => p.HrPslEmployeeID == entity.HrPslEmployeeID).Result);
                EmployeeContactsRep.DeleteRange(EmployeeContactsRep.FilterAsync(p => p.HrPslEmployeeID == entity.HrPslEmployeeID).Result);
                EmployeeExperienceRep.DeleteRange(EmployeeExperienceRep.FilterAsync(p => p.HrPslEmployeeID == entity.HrPslEmployeeID).Result);
                EmployeeFamilyDetailsRep.DeleteRange(EmployeeFamilyDetailsRep.FilterAsync(p => p.HrPslEmployeeID == entity.HrPslEmployeeID).Result);
                EmployeeTrainingCourcesRep.DeleteRange(EmployeeTrainingCourcesRep.FilterAsync(p => p.HrPslEmployeeID == entity.HrPslEmployeeID).Result);

                EmployeeRep.Delete(Employee, entity.HrPslEmployeeID);

                //todo:Remove this after full integration with HR project
                //DeleteEmployeeOnHRProjectEmployeeTable(emp.EMP_ID);
                return true;
            });
        }

        public void Dispose()
        {
            employeeRepo.Dispose();
        }

        public List<EMPLOYEEVM> GetAll()
        {
            var q = from entity in employeeRepo.GetAll()
                    select new EMPLOYEEVM
                    {
                        ACC_ID = entity.ACC_ID,
                        AddedBy = entity.AddedBy,
                        AddedOn = entity.AddedOn,
                        COM_BRN_ID = entity.COM_BRN_ID,
                        DEPT_ID = entity.DEPT_ID,
                        Disable = entity.Disable,
                        EMP_ADDR_REMARKS = entity.EMP_ADDR_REMARKS,
                        EMP_AR_NAME = entity.EMP_AR_NAME,
                        EMP_BARCODE = entity.EMP_BARCODE,
                        EMP_BARCODE_IMAGE = entity.EMP_BARCODE_IMAGE,
                        EMP_CODE = entity.EMP_CODE,
                        EMP_EN_NAME = entity.EMP_EN_NAME,
                        EMP_ID = entity.EMP_ID,
                        EMP_NATIONAL_ID = entity.EMP_NATIONAL_ID,
                        EMP_PHOTO = entity.EMP_PHOTO,
                        EMP_REMARKS = entity.EMP_REMARKS,
                        EMP_STATE = entity.EMP_STATE,
                        EMP_TYPE_ID = entity.EMP_TYPE_ID,
                        GOV_ID = entity.GOV_ID,
                        NATIONALITY_ID = entity.NATIONALITY_ID,
                        NATION_ID = entity.NATION_ID,
                        TOWN_ID = entity.TOWN_ID,
                        UpdatedBy = entity.UpdatedBy,
                        updatedOn = entity.updatedOn,
                        VILLAGE_ID = entity.VILLAGE_ID,
                        Visa_Number = entity.Visa_Number,
                        Pass_Number = entity.Pass_Number,
                        Visa_ExpDateM = entity.Visa_ExpDateM,
                        Visa_ExpDateH = entity.Visa_ExpDateH,
                        Pass_ExpDate = entity.Pass_ExpDate
                    };
            return q.ToList();
        }
        public EMPLOYEEVM getById(int EMP_ID)
        {
            var q = from entity in employeeRepo.GetAll().Where(a=>a.EMP_ID== EMP_ID)
                    select new EMPLOYEEVM
                    {
                        ACC_ID = entity.ACC_ID,
                        AddedBy = entity.AddedBy,
                        AddedOn = entity.AddedOn,
                        COM_BRN_ID = entity.COM_BRN_ID,
                        DEPT_ID = entity.DEPT_ID,
                        Disable = entity.Disable,
                        EMP_ADDR_REMARKS = entity.EMP_ADDR_REMARKS,
                        EMP_AR_NAME = entity.EMP_AR_NAME,
                        EMP_BARCODE = entity.EMP_BARCODE,
                        EMP_BARCODE_IMAGE = entity.EMP_BARCODE_IMAGE,
                        EMP_CODE = entity.EMP_CODE,
                        EMP_EN_NAME = entity.EMP_EN_NAME,
                        EMP_ID = entity.EMP_ID,
                        EMP_NATIONAL_ID = entity.EMP_NATIONAL_ID,
                        EMP_PHOTO = entity.EMP_PHOTO,
                        EMP_REMARKS = entity.EMP_REMARKS,
                        EMP_STATE = entity.EMP_STATE,
                        EMP_TYPE_ID = entity.EMP_TYPE_ID,
                        GOV_ID = entity.GOV_ID,
                        NATIONALITY_ID = entity.NATIONALITY_ID,
                        NATION_ID = entity.NATION_ID,
                        TOWN_ID = entity.TOWN_ID,
                        UpdatedBy = entity.UpdatedBy,
                        updatedOn = entity.updatedOn,
                        VILLAGE_ID = entity.VILLAGE_ID,
                        Visa_Number = entity.Visa_Number,
                        Pass_Number = entity.Pass_Number,
                        Visa_ExpDateM = entity.Visa_ExpDateM,
                        Visa_ExpDateH = entity.Visa_ExpDateH,
                        Pass_ExpDate = entity.Pass_ExpDate
                    };
            return q.FirstOrDefault();
        }
        public List<EMPLOYEEVM> GetByTypeID(int typeID)
        {
            var q = from entity in employeeRepo.GetAll().Where(X => X.EMP_TYPE_ID == typeID)
                    select new EMPLOYEEVM
                    {
                        ACC_ID = entity.ACC_ID,
                        AddedBy = entity.AddedBy,
                        AddedOn = entity.AddedOn,
                        COM_BRN_ID = entity.COM_BRN_ID,
                        DEPT_ID = entity.DEPT_ID,
                        Disable = entity.Disable,
                        EMP_ADDR_REMARKS = entity.EMP_ADDR_REMARKS,
                        EMP_AR_NAME = entity.EMP_AR_NAME,
                        EMP_BARCODE = entity.EMP_BARCODE,
                        EMP_BARCODE_IMAGE = entity.EMP_BARCODE_IMAGE,
                        EMP_CODE = entity.EMP_CODE,
                        EMP_EN_NAME = entity.EMP_EN_NAME,
                        EMP_ID = entity.EMP_ID,
                        EMP_NATIONAL_ID = entity.EMP_NATIONAL_ID,
                        EMP_PHOTO = entity.EMP_PHOTO,
                        EMP_REMARKS = entity.EMP_REMARKS,
                        EMP_STATE = entity.EMP_STATE,
                        EMP_TYPE_ID = entity.EMP_TYPE_ID,
                        GOV_ID = entity.GOV_ID,
                        NATIONALITY_ID = entity.NATIONALITY_ID,
                        NATION_ID = entity.NATION_ID,
                        TOWN_ID = entity.TOWN_ID,
                        UpdatedBy = entity.UpdatedBy,
                        updatedOn = entity.updatedOn,
                        VILLAGE_ID = entity.VILLAGE_ID,
                        Visa_Number = entity.Visa_Number,
                        Pass_Number = entity.Pass_Number,
                        Visa_ExpDateM = entity.Visa_ExpDateM,
                        Visa_ExpDateH = entity.Visa_ExpDateH,
                        Pass_ExpDate = entity.Pass_ExpDate
                    };
            return q.ToList();
        }

        public Task<List<EMPLOYEEVM>> GetSearchForStore(string Search, int typeID)
        {
            return Task.Run<List<EMPLOYEEVM>>(() =>
            {
                var q = from entity in employeeRepo.GetAll()
                        select new EMPLOYEEVM
                        {
                            ACC_ID = entity.ACC_ID,
                            AddedBy = entity.AddedBy,
                            AddedOn = entity.AddedOn,
                            COM_BRN_ID = entity.COM_BRN_ID,
                            DEPT_ID = entity.DEPT_ID,
                            Disable = entity.Disable,
                            EMP_ADDR_REMARKS = entity.EMP_ADDR_REMARKS,
                            EMP_AR_NAME = entity.EMP_AR_NAME,
                            EMP_BARCODE = entity.EMP_BARCODE,
                            EMP_BARCODE_IMAGE = entity.EMP_BARCODE_IMAGE,
                            EMP_CODE = entity.EMP_CODE,
                            EMP_EN_NAME = entity.EMP_EN_NAME,
                            EMP_ID = entity.EMP_ID,
                            EMP_NATIONAL_ID = entity.EMP_NATIONAL_ID,
                            EMP_PHOTO = entity.EMP_PHOTO,
                            EMP_REMARKS = entity.EMP_REMARKS,
                            EMP_STATE = entity.EMP_STATE,
                            EMP_TYPE_ID = entity.EMP_TYPE_ID,
                            GOV_ID = entity.GOV_ID,
                            NATIONALITY_ID = entity.NATIONALITY_ID,
                            NATION_ID = entity.NATION_ID,
                            TOWN_ID = entity.TOWN_ID,
                            UpdatedBy = entity.UpdatedBy,
                            updatedOn = entity.updatedOn,
                            VILLAGE_ID = entity.VILLAGE_ID,
                            Visa_Number = entity.Visa_Number,
                            Pass_Number = entity.Pass_Number,
                            Visa_ExpDateM = entity.Visa_ExpDateM,
                            Visa_ExpDateH = entity.Visa_ExpDateH,
                            Pass_ExpDate = entity.Pass_ExpDate
                        };
                if (!string.IsNullOrEmpty(Search))
                {
                    return q.Where(x => (x.EMP_TYPE_ID == typeID) && (x.EMP_CODE.ToLower().Contains(Search.ToLower()) || x.EMP_AR_NAME.ToLower().Contains(Search.ToLower()) || x.EMP_EN_NAME.ToLower().Contains(Search.ToLower()))).ToList();
                }
                else
                {
                    return q.Where(x => (x.EMP_TYPE_ID == typeID)).ToList();
                }
            });
        }

        public Task<List<HrPslEmployeeVM>> GetAllAsync(int pageNum, int pageSize)
        {
            return Task.Run<List<HrPslEmployeeVM>>(() =>
            {
                int rowCount;
                var q = from entity in EmployeeRep.GetPaged<long>(pageNum, pageSize, p => p.HrPslEmployeeID, false, out rowCount)
                        select new HrPslEmployeeVM
                        {
                            HrPslEmployeeID = entity.HrPslEmployeeID,
                            EmployeeCode = entity.EmployeeCode,
                            EmployeeFirstName = entity.EmployeeFirstName,
                            EmployeeFirstNameEn = entity.EmployeeFirstNameEn,
                            EmployeeLastName = entity.EmployeeLastName,
                            EmployeeLastNameEn = entity.EmployeeLastNameEn,
                            DateOfJoin = entity.DateOfJoin,
                            HrPslJobTitleID = entity.HrPslJobTitleID,
                            HrPslBranchID = entity.HrPslBranchID,
                            HrPslDeptartmentID = entity.HrPslDeptartmentID,
                            HrPslContractTypeID = entity.HrPslContractTypeID,
                            HrPslEmployeeStatusID = entity.HrPslEmployeeStatusID,
                            WorkMobileNum = entity.WorkMobileNum,
                            DirectManagerID = entity.DirectManagerID,
                            HrPslEmployeeJobLevelID = entity.HrPslEmployeeJobLevelID,
                            HrPslManagementId = entity.HrPslManagementId,
                            GenderID = entity.GenderID,
                            HrPslMaritalstatusID = entity.HrPslMaritalstatusID,
                            HrPslReligionID = entity.HrPslReligionID,
                            HrPslCountyID = entity.HrPslCountyID,
                            HrPslCityID = entity.HrPslCityID,
                            Address = entity.Address,
                            HrPslNationalityID = entity.HrPslNationalityID,
                            NationalID = entity.NationalID,
                            NationalIdExpData = entity.NationalIdExpData,
                            PassportNo = entity.PassportNo,
                            PassportNoExpDate = entity.PassportNoExpDate,
                            DateOfBirth = entity.DateOfBirth,
                            ResidenceType = entity.ResidenceType,
                            ResidencyNumber = entity.ResidencyNumber,
                            ResidenceExpireDate = entity.ResidenceExpireDate,
                            DrivingLicenseNumber = entity.DrivingLicenseNumber,
                            DrivingLicenseIssueDate = entity.DrivingLicenseIssueDate,
                            DrivingLicenseExpireDate = entity.DrivingLicenseExpireDate,
                            DrivingLicenseIssuer = entity.DrivingLicenseIssuer,
                            DrivingLicenseType = entity.DrivingLicenseType,
                            HasCar = entity.HasCar,
                            GlBankID = entity.GlBankID,
                            BankAccountNumber = entity.BankAccountNumber,
                            HasChronicDisease = entity.HasChronicDisease,
                            ChronicDiseaseDescreption = entity.ChronicDiseaseDescreption,
                            HasInjuries = entity.HasInjuries,
                            InjuriesDescreption = entity.InjuriesDescreption,
                            BloodGroup = entity.BloodGroup,
                        };
                return q.ToList();
            });
        }

        public bool Insert(EMPLOYEEVM entity)
        {
            EMPLOYEES emp = new EMPLOYEES
            {
                ACC_ID = entity.ACC_ID,
                AddedBy = entity.AddedBy,
                AddedOn = entity.AddedOn,
                COM_BRN_ID = entity.COM_BRN_ID,
                DEPT_ID = entity.DEPT_ID,
                Disable = entity.Disable,
                EMP_ADDR_REMARKS = entity.EMP_ADDR_REMARKS,
                EMP_AR_NAME = entity.EMP_AR_NAME,
                EMP_BARCODE = entity.EMP_BARCODE,
                EMP_BARCODE_IMAGE = entity.EMP_BARCODE_IMAGE,
                EMP_CODE = entity.EMP_CODE,
                EMP_EN_NAME = entity.EMP_EN_NAME,
                EMP_ID = entity.EMP_ID,
                EMP_NATIONAL_ID = entity.EMP_NATIONAL_ID,
                EMP_PHOTO = entity.EMP_PHOTO,
                EMP_REMARKS = entity.EMP_REMARKS,
                EMP_STATE = entity.EMP_STATE,
                EMP_TYPE_ID = entity.EMP_TYPE_ID,
                GOV_ID = entity.GOV_ID,
                NATIONALITY_ID = entity.NATIONALITY_ID,
                NATION_ID = entity.NATION_ID,
                TOWN_ID = entity.TOWN_ID,
                UpdatedBy = entity.UpdatedBy,
                updatedOn = entity.updatedOn,
                VILLAGE_ID = entity.VILLAGE_ID,
                Visa_Number = entity.Visa_Number,
                Pass_Number = entity.Pass_Number,
                Visa_ExpDateM = entity.Visa_ExpDateM,
                Visa_ExpDateH = entity.Visa_ExpDateH,
                Pass_ExpDate = entity.Pass_ExpDate
            };
            employeeRepo.Add(emp);

            //todo:Remove this after full integration with HR project
            AddEmployeeOnHRProjectEmployeeTable(emp);
            return true;
        }
        public int InsertGetID(EMPLOYEEVM entity)
        {
            if (entity.Item_Base64_Photo != null)
            {
                string base64 = entity.Item_Base64_Photo;
                entity.Item_Base64_Photo = String.Format(base64);
                base64 = base64.Remove(0, base64.IndexOf("base64,") + 7);
                entity.EMP_PHOTO = Convert.FromBase64String(base64);
            }


            EMPLOYEES emp = new EMPLOYEES
            {
                ACC_ID = entity.ACC_ID,
                AddedBy = entity.AddedBy,
                AddedOn = entity.AddedOn,
                COM_BRN_ID = entity.COM_BRN_ID,
                DEPT_ID = entity.DEPT_ID,
                Disable = entity.Disable,
                EMP_ADDR_REMARKS = entity.EMP_ADDR_REMARKS,
                EMP_AR_NAME = entity.EMP_AR_NAME,
                EMP_BARCODE = entity.EMP_BARCODE,
                EMP_BARCODE_IMAGE = entity.EMP_BARCODE_IMAGE,
                EMP_CODE = entity.EMP_CODE,
                EMP_EN_NAME = entity.EMP_EN_NAME,
                EMP_ID = entity.EMP_ID,
                EMP_NATIONAL_ID = entity.EMP_NATIONAL_ID,
                EMP_PHOTO = entity.EMP_PHOTO,
                EMP_REMARKS = entity.EMP_REMARKS,
                EMP_STATE = entity.EMP_STATE,
                EMP_TYPE_ID = entity.EMP_TYPE_ID,
                GOV_ID = entity.GOV_ID,
                NATIONALITY_ID = entity.NATIONALITY_ID,
                NATION_ID = entity.NATION_ID,
                TOWN_ID = entity.TOWN_ID,
                UpdatedBy = entity.UpdatedBy,
                updatedOn = entity.updatedOn,
                VILLAGE_ID = entity.VILLAGE_ID,
                Visa_Number = entity.Visa_Number,
                Pass_Number = entity.Pass_Number,
                Visa_ExpDateM = entity.Visa_ExpDateM,
                Visa_ExpDateH = entity.Visa_ExpDateH,
                Pass_ExpDate = entity.Pass_ExpDate

            };
            employeeRepo.Add(emp);

            //todo:Remove this after full integration with HR project
            AddEmployeeOnHRProjectEmployeeTable(emp);
            return emp.EMP_ID;
        }
        public Task<int> InsertAsync(HrPslEmployeeVM entity)
        {

            return Task.Run<int>(() =>
            {

                //string base64 = entity.Item_Base64_Photo;
                //entity.Item_Base64_Photo = String.Format(base64);
                //base64 = base64.Remove(0, base64.IndexOf("base64,") + 7);
                //entity.EMP_PHOTO = Convert.FromBase64String(base64);


                HrPslEmployee Employee = new HrPslEmployee
                {
                    EmployeeCode = entity.EmployeeCode,
                    EmployeeFirstName = entity.EmployeeFirstName,
                    EmployeeFirstNameEn = entity.EmployeeFirstNameEn,
                    EmployeeLastName = entity.EmployeeLastName,
                    EmployeeLastNameEn = entity.EmployeeLastNameEn,
                    DateOfJoin = entity.DateOfJoin,
                    HrPslJobTitleID = entity.HrPslJobTitleID,
                    HrPslBranchID = entity.HrPslBranchID,
                    HrPslDeptartmentID = entity.HrPslDeptartmentID,
                    HrPslContractTypeID = entity.HrPslContractTypeID,
                    HrPslEmployeeStatusID = entity.HrPslEmployeeStatusID,
                    WorkMobileNum = entity.WorkMobileNum,
                    DirectManagerID = entity.DirectManagerID,
                    HrPslEmployeeJobLevelID = entity.HrPslEmployeeJobLevelID,
                    HrPslManagementId = entity.HrPslManagementId,
                    GenderID = entity.GenderID,
                    HrPslMaritalstatusID = entity.HrPslMaritalstatusID,
                    HrPslReligionID = entity.HrPslReligionID,
                    HrPslCountyID = entity.HrPslCountyID,
                    HrPslCityID = entity.HrPslCityID,
                    Address = entity.Address,
                    HrPslNationalityID = entity.HrPslNationalityID,
                    NationalID = entity.NationalID,
                    NationalIdExpData = entity.NationalIdExpData,
                    PassportNo = entity.PassportNo,
                    PassportNoExpDate = entity.PassportNoExpDate,
                    DateOfBirth = entity.DateOfBirth,
                    ResidenceType = entity.ResidenceType,
                    ResidencyNumber = entity.ResidencyNumber,
                    ResidenceExpireDate = entity.ResidenceExpireDate,
                    DrivingLicenseNumber = entity.DrivingLicenseNumber,
                    DrivingLicenseIssueDate = entity.DrivingLicenseIssueDate,
                    DrivingLicenseExpireDate = entity.DrivingLicenseExpireDate,
                    DrivingLicenseIssuer = entity.DrivingLicenseIssuer,
                    DrivingLicenseType = entity.DrivingLicenseType,
                    HasCar = entity.HasCar,
                    GlBankID = entity.GlBankID,
                    BankAccountNumber = entity.BankAccountNumber,
                    HasChronicDisease = entity.HasChronicDisease,
                    ChronicDiseaseDescreption = entity.ChronicDiseaseDescreption,
                    HasInjuries = entity.HasInjuries,
                    InjuriesDescreption = entity.InjuriesDescreption,
                    BloodGroup = entity.BloodGroup
                };
                EmployeeRep.Add(Employee);

                EmployeeAcademicDegreeRep.AddRange(entity.AcademicDegrees);
                EmployeeContactsRep.AddRange(entity.Contacts);
                EmployeeExperienceRep.AddRange(entity.Experiences);
                EmployeeFamilyDetailsRep.AddRange(entity.FamilyDetails);
                EmployeeTrainingCourcesRep.AddRange(entity.TrainingCourses);

                //todo:Remove this after full integration with HR project
                //AddEmployeeOnHRProjectEmployeeTable(emp);
                return entity.HrPslEmployeeID;
            });
        }
        public Task<int> InsertGetIDAsync(EMPLOYEEVM entity)
        {
            return Task.Run<int>(() =>
            {
                EMPLOYEES emp = new EMPLOYEES
                {
                    ACC_ID = entity.ACC_ID,
                    AddedBy = entity.AddedBy,
                    AddedOn = entity.AddedOn,
                    COM_BRN_ID = entity.COM_BRN_ID,
                    DEPT_ID = entity.DEPT_ID,
                    Disable = entity.Disable,
                    EMP_ADDR_REMARKS = entity.EMP_ADDR_REMARKS,
                    EMP_AR_NAME = entity.EMP_AR_NAME,
                    EMP_BARCODE = entity.EMP_BARCODE,
                    EMP_BARCODE_IMAGE = entity.EMP_BARCODE_IMAGE,
                    EMP_CODE = entity.EMP_CODE,
                    EMP_EN_NAME = entity.EMP_EN_NAME,
                    EMP_ID = entity.EMP_ID,
                    EMP_NATIONAL_ID = entity.EMP_NATIONAL_ID,
                    EMP_PHOTO = entity.EMP_PHOTO,
                    EMP_REMARKS = entity.EMP_REMARKS,
                    EMP_STATE = entity.EMP_STATE,
                    EMP_TYPE_ID = entity.EMP_TYPE_ID,
                    GOV_ID = entity.GOV_ID,
                    NATIONALITY_ID = entity.NATIONALITY_ID,
                    NATION_ID = entity.NATION_ID,
                    TOWN_ID = entity.TOWN_ID,
                    UpdatedBy = entity.UpdatedBy,
                    updatedOn = entity.updatedOn,
                    VILLAGE_ID = entity.VILLAGE_ID,
                    Visa_Number = entity.Visa_Number,
                    Pass_Number = entity.Pass_Number,
                    Visa_ExpDateM = entity.Visa_ExpDateM,
                    Visa_ExpDateH = entity.Visa_ExpDateH,
                    Pass_ExpDate = entity.Pass_ExpDate
                };
                employeeRepo.Add(emp);

                //todo:Remove this after full integration with HR project
                AddEmployeeOnHRProjectEmployeeTable(emp);
                return emp.EMP_ID;
            });
        }
        public bool Update(EMPLOYEEVM entity)
        {
            EMPLOYEES emp = new EMPLOYEES
            {
                ACC_ID = entity.ACC_ID,
                AddedBy = entity.AddedBy,
                AddedOn = entity.AddedOn,
                COM_BRN_ID = entity.COM_BRN_ID,
                DEPT_ID = entity.DEPT_ID,
                Disable = entity.Disable,
                EMP_ADDR_REMARKS = entity.EMP_ADDR_REMARKS,
                EMP_AR_NAME = entity.EMP_AR_NAME,
                EMP_BARCODE = entity.EMP_BARCODE,
                EMP_BARCODE_IMAGE = entity.EMP_BARCODE_IMAGE,
                EMP_CODE = entity.EMP_CODE,
                EMP_EN_NAME = entity.EMP_EN_NAME,
                EMP_ID = entity.EMP_ID,
                EMP_NATIONAL_ID = entity.EMP_NATIONAL_ID,
                EMP_PHOTO = entity.EMP_PHOTO,
                EMP_REMARKS = entity.EMP_REMARKS,
                EMP_STATE = entity.EMP_STATE,
                EMP_TYPE_ID = entity.EMP_TYPE_ID,
                GOV_ID = entity.GOV_ID,
                NATIONALITY_ID = entity.NATIONALITY_ID,
                NATION_ID = entity.NATION_ID,
                TOWN_ID = entity.TOWN_ID,
                UpdatedBy = entity.UpdatedBy,
                updatedOn = entity.updatedOn,
                VILLAGE_ID = entity.VILLAGE_ID,
                Visa_Number = entity.Visa_Number,
                Pass_Number = entity.Pass_Number,
                Visa_ExpDateM = entity.Visa_ExpDateM,
                Visa_ExpDateH = entity.Visa_ExpDateH,
                Pass_ExpDate = entity.Pass_ExpDate
            };
            employeeRepo.Update(emp, emp.EMP_ID);

            //todo:Remove this after full integration with HR project
            EditEmployeeOnHRProjectEmployeeTable(emp);
            return true;
        }

        public Task<bool> UpdateAsync(HrPslEmployeeVM entity)
        {
            return Task.Run<bool>(() =>
            {
                //if (entity.Item_Base64_Photo != null)
                //{
                //    string base64 = entity.Item_Base64_Photo;
                //    entity.Item_Base64_Photo = String.Format(base64);
                //    base64 = base64.Remove(0, base64.IndexOf("base64,") + 7);
                //    entity.EMP_PHOTO = Convert.FromBase64String(base64);
                //}

                HrPslEmployee Employee = new HrPslEmployee
                {
                    HrPslEmployeeID = entity.HrPslEmployeeID,
                    EmployeeCode = entity.EmployeeCode,
                    EmployeeFirstName = entity.EmployeeFirstName,
                    EmployeeFirstNameEn = entity.EmployeeFirstNameEn,
                    EmployeeLastName = entity.EmployeeLastName,
                    EmployeeLastNameEn = entity.EmployeeLastNameEn,
                    DateOfJoin = entity.DateOfJoin,
                    HrPslJobTitleID = entity.HrPslJobTitleID,
                    HrPslBranchID = entity.HrPslBranchID,
                    HrPslDeptartmentID = entity.HrPslDeptartmentID,
                    HrPslContractTypeID = entity.HrPslContractTypeID,
                    HrPslEmployeeStatusID = entity.HrPslEmployeeStatusID,
                    WorkMobileNum = entity.WorkMobileNum,
                    DirectManagerID = entity.DirectManagerID,
                    HrPslEmployeeJobLevelID = entity.HrPslEmployeeJobLevelID,
                    HrPslManagementId = entity.HrPslManagementId,
                    GenderID = entity.GenderID,
                    HrPslMaritalstatusID = entity.HrPslMaritalstatusID,
                    HrPslReligionID = entity.HrPslReligionID,
                    HrPslCountyID = entity.HrPslCountyID,
                    HrPslCityID = entity.HrPslCityID,
                    Address = entity.Address,
                    HrPslNationalityID = entity.HrPslNationalityID,
                    NationalID = entity.NationalID,
                    NationalIdExpData = entity.NationalIdExpData,
                    PassportNo = entity.PassportNo,
                    PassportNoExpDate = entity.PassportNoExpDate,
                    DateOfBirth = entity.DateOfBirth,
                    ResidenceType = entity.ResidenceType,
                    ResidencyNumber = entity.ResidencyNumber,
                    ResidenceExpireDate = entity.ResidenceExpireDate,
                    DrivingLicenseNumber = entity.DrivingLicenseNumber,
                    DrivingLicenseIssueDate = entity.DrivingLicenseIssueDate,
                    DrivingLicenseExpireDate = entity.DrivingLicenseExpireDate,
                    DrivingLicenseIssuer = entity.DrivingLicenseIssuer,
                    DrivingLicenseType = entity.DrivingLicenseType,
                    HasCar = entity.HasCar,
                    GlBankID = entity.GlBankID,
                    BankAccountNumber = entity.BankAccountNumber,
                    HasChronicDisease = entity.HasChronicDisease,
                    ChronicDiseaseDescreption = entity.ChronicDiseaseDescreption,
                    HasInjuries = entity.HasInjuries,
                    InjuriesDescreption = entity.InjuriesDescreption,
                    BloodGroup = entity.BloodGroup                    
                };
                EmployeeRep.Update(Employee, Employee.HrPslEmployeeID);

                //Remove all related data to employee
                EmployeeAcademicDegreeRep.DeleteRange(EmployeeAcademicDegreeRep.FilterAsync(p=>p.HrPslEmployeeID == entity.HrPslEmployeeID).Result);
                EmployeeContactsRep.DeleteRange(EmployeeContactsRep.FilterAsync(p => p.HrPslEmployeeID == entity.HrPslEmployeeID).Result);
                EmployeeExperienceRep.DeleteRange(EmployeeExperienceRep.FilterAsync(p => p.HrPslEmployeeID == entity.HrPslEmployeeID).Result);
                EmployeeFamilyDetailsRep.DeleteRange(EmployeeFamilyDetailsRep.FilterAsync(p => p.HrPslEmployeeID == entity.HrPslEmployeeID).Result);
                EmployeeTrainingCourcesRep.DeleteRange(EmployeeTrainingCourcesRep.FilterAsync(p => p.HrPslEmployeeID == entity.HrPslEmployeeID).Result);

                //Add all related data to employee
                EmployeeAcademicDegreeRep.AddRange(entity.AcademicDegrees);
                EmployeeContactsRep.AddRange(entity.Contacts);
                EmployeeExperienceRep.AddRange(entity.Experiences);
                EmployeeFamilyDetailsRep.AddRange(entity.FamilyDetails);
                EmployeeTrainingCourcesRep.AddRange(entity.TrainingCourses);

                //todo:Remove this after full integration with HR project
                //EditEmployeeOnHRProjectEmployeeTable(emp);
                return true;
            });
        }

        public List<EMPLOYEEVM> GetAllSalesEmployees()
        {
            var q = from entity in employeeRepo.GetAll()
                    select new EMPLOYEEVM
                    {
                        EMP_AR_NAME = entity.EMP_AR_NAME,
                        EMP_CODE = entity.EMP_CODE,
                        EMP_EN_NAME = entity.EMP_EN_NAME,
                        EMP_ID = entity.EMP_ID
                    };
            return q.ToList();
        }



        public string GetLastCode()
        {
            var lastCode = employeeRepo.GetAll().OrderByDescending(e => e.EMP_ID).FirstOrDefault();
            return lastCode.EMP_CODE;           
        }

        public List<EMPLOYEES> getByEmpType(int empTypeId)
        {
            var q = employeeRepo.GetAll().Where(a => a.EMP_TYPE_ID == empTypeId).ToList();
            return q;
        }
        public List<EMPLOYEES> getByNationID(int nationId)
        {
            var q = employeeRepo.GetAll().Where(a => a.NATION_ID == nationId).ToList();
            return q;
        }
        public List<EMPLOYEES> getByGOVID(int govId)
        {
            var q = employeeRepo.GetAll().Where(a => a.GOV_ID == govId).ToList();
            return q;
        }
        public List<EMPLOYEES> getByTownID(int townId)
        {
            var q = employeeRepo.GetAll().Where(a => a.TOWN_ID == townId).ToList();
            return q;
        }
        public List<EMPLOYEES> getByVilID(long villageId)
        {
            var q = employeeRepo.GetAll().Where(a => a.VILLAGE_ID == villageId).ToList();
            return q;
        }
        public List<EMPLOYEES> getByDeptID(long deptId)
        {
            var q = employeeRepo.GetAll().Where(a => a.DEPT_ID == deptId).ToList();
            return q;
        }

        public List<DropDownMenuOptionsVM> getMartialStatusList()
        {
            return MartialStatusRep.GetAll().Select(p => new DropDownMenuOptionsVM
            {
                OptionValue = p.HrPslMartialStatusID,
                OptionText = p.MartialStatusName,
            }).ToList();
        }

        public List<DropDownMenuOptionsVM> getRelegionList()
        {
            return ReligionRep.GetAll().Select(p => new DropDownMenuOptionsVM
            {
                OptionValue = p.HrPslReligionID,
                OptionText = p.ReligionName,
            }).ToList();
        }

        public List<DropDownMenuOptionsVM> getCityList()
        {
            return CityRep.GetAll().Select(p => new DropDownMenuOptionsVM
            {
                OptionValue = p.HrPslCityID,
                OptionText = p.CityName,
            }).ToList();
        }

        public List<DropDownMenuOptionsVM> getNationalityList()
        {
            return NationalityRep.GetAll().Select(p => new DropDownMenuOptionsVM
            {
                OptionValue = p.HrPslCountryID,
                OptionText = p.NationalityName,
            }).ToList();
        }

        public List<DropDownMenuOptionsVM> getJobTitleList()
        {
            return JobTitleRep.GetAll().Select(p => new DropDownMenuOptionsVM
            {
                OptionValue = p.HrPslJobTitleID,
                OptionText = p.JobTitleName,
            }).ToList();
        }

        public List<DropDownMenuOptionsVM> getJobLevelList()
        {
            return EmployeeJobLevelRep.GetAll().Select(p => new DropDownMenuOptionsVM
            {
                OptionValue = p.HrPslEmployeeJobLevelID,
                OptionText = p.EmployeeJobLevelName,
            }).ToList();
        }

        public List<DropDownMenuOptionsVM> getDeptartmentList()
        {
            return DepartmentRep.GetAll().Select(p => new DropDownMenuOptionsVM
            {
                OptionValue = p.DEPT_ID,
                OptionText = p.DEPT_AR_NAME,
            }).ToList();
        }

        public List<DropDownMenuOptionsVM> getEmployeeStatusList()
        {
            return MartialStatusRep.GetAll().Select(p => new DropDownMenuOptionsVM
            {
                OptionValue = p.HrPslMartialStatusID,
                OptionText = p.MartialStatusName,
            }).ToList();
        }

        public List<DropDownMenuOptionsVM> getDirectManagerList()
        {
            return EmployeeRep.GetAll().Select(p => new DropDownMenuOptionsVM
            {
                OptionValue = p.HrPslEmployeeID,
                OptionText = p.EmployeeFirstName + " " + p.EmployeeLastName,
            }).ToList();
        }

        public List<DropDownMenuOptionsVM> getContactTypeList()
        {
            return ContactTypeRep.GetAll().Select(p => new DropDownMenuOptionsVM
            {
                OptionValue = p.HrPslContactTypeID,
                OptionText = p.ContactTypeName,
            }).ToList();
        }

        public List<DropDownMenuOptionsVM> getAcademicDegreeList()
        {
            return AcademicDegreeRep.GetAll().Select(p => new DropDownMenuOptionsVM
            {
                OptionValue = p.HrPslAcademicDegreeID,
                OptionText = p.AcademicDegreeName,
            }).ToList();
        }

        public List<DropDownMenuOptionsVM> getBankList()
        {
            return BankRep.GetAll().Select(p => new DropDownMenuOptionsVM
            {
                OptionValue = p.GlBankID,
                OptionText = p.NameAr,
            }).ToList();
        }

        public List<HrPslEmployeeAcademicDegree> getEmployeeAcademicDegrees(int employeeId)
        {
            return EmployeeAcademicDegreeRep.Filter(p => p.HrPslEmployeeID == employeeId).ToList();
        }

        public List<HrPslEmployeeExperience> getEmployeeExperiences(int employeeId)
        {
            return EmployeeExperienceRep.Filter(p => p.HrPslEmployeeID == employeeId).ToList();
        }

        public List<HrPslEmployeeFamilyDetails> getEmployeeFamilyDetails(int employeeId)
        {
            return EmployeeFamilyDetailsRep.Filter(p => p.HrPslEmployeeID == employeeId).ToList();
        }

        public List<HrPslEmployeeTrainingCources> getEmployeeTrainingCourses(int employeeId)
        {
            return EmployeeTrainingCourcesRep.Filter(p => p.HrPslEmployeeID == employeeId).ToList();
        }

        public List<HrPslEmployeeContacts> getEmployeeContacts(int employeeId)
        {
            return EmployeeContactsRep.Filter(p => p.HrPslEmployeeID == employeeId).ToList();
        }

        #region Employee table in HR project
        //todo:Remove this functions after full integration with HR project
        //**************************************************************//
        private void AddEmployeeOnHRProjectEmployeeTable(EMPLOYEES entity)
        {
            try
            {
                var Query = "CREATE TABLE #temp (id [int]) " +
                    "Insert [dbo].[HrPslEmployee] ([GoldEmployeeId],[EmployeeCode],[EmployeeFirstName],[EmployeeFirstNameEN],[EmployeeLastName],[EmployeeLastNameEN]) " +
                    "OUTPUT INSERTED.HrPslEmployeeID into #temp " +
                    "values (" + entity.EMP_ID + "," + entity.EMP_CODE + ",N'" + entity.EMP_AR_NAME + "'," +
                    "N'" + entity.EMP_EN_NAME + "','','') "+
                    "update EMPLOYEES set HrPslEmployeeId = (select top 1 id from #temp) where EMP_ID = " + entity.EMP_ID + " " +
                    "drop table #temp";
                employeeRepo.ExecuteSqlCommand(Query);
            }
            catch (Exception)
            {

            }
        }

        private void EditEmployeeOnHRProjectEmployeeTable(EMPLOYEES entity)
        {
            try
            {
                var Query = "Update [dbo].[HrPslEmployee] " +
                    "set [EmployeeCode] = " + entity.EMP_CODE + " , [EmployeeFirstName] = N'" + entity.EMP_AR_NAME + "'" +
                    " , [EmployeeFirstNameEN] = N'" + entity.EMP_EN_NAME + "' Where [GoldEmployeeId] = " + entity.EMP_ID;
                employeeRepo.ExecuteSqlCommand(Query);
            }
            catch (Exception)
            {

            }
        }

        private void DeleteEmployeeOnHRProjectEmployeeTable(int id)
        {
            try
            {
                var Query = "Delete [dbo].[HrPslEmployee] Where [GoldEmployeeId] = " + id;
                employeeRepo.ExecuteSqlCommand(Query);
            }
            catch (Exception)
            {

            }
        }
        //**************************************************************//
        #endregion
    }



}
