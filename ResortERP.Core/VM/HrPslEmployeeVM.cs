using System;
using System.Collections.Generic;

namespace ResortERP.Core.VM
{
    public class HrPslEmployeeVM
    {
        public int HrPslEmployeeID { get; set; }
        public string EmployeeCode { get; set; }
        public string EmployeeFirstName { get; set; }
        public string EmployeeFirstNameEn { get; set; }
        public string EmployeeLastName { get; set; }
        public string EmployeeLastNameEn { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public int? GenderID { get; set; }
        public string Address { get; set; }
        public int? HrPslCityID { get; set; }
        public int? HrPslCountyID { get; set; }
        public int? HrPslNationalityID { get; set; }
        public int? HrPslReligionID { get; set; }
        public int? HrPslMaritalstatusID { get; set; }
        public int? NationalID { get; set; }
        public DateTime? NationalIdExpData { get; set; }
        public int? PassportNo { get; set; }
        public DateTime? PassportNoExpDate { get; set; }
        public string Image { get; set; }
        public int? HrPslJobTitleID { get; set; }
        public DateTime? DateOfJoin { get; set; }
        public int? HrPslContractTypeID { get; set; }
        public int? HrPslDeptartmentID { get; set; }
        public int? HrPslBranchID { get; set; }
        public short? IsManager { get; set; }
        public int? DirectManagerID { get; set; }
        public int? WorkMobileNum { get; set; }
        public int? HrPslPaymentID { get; set; }
        public int? PersonalMobileNum { get; set; }
        public int? HomeNum { get; set; }
        public string Email { get; set; }
        public int? HrPslEmployeeStatusID { get; set; }
        public int? HrPslBenefitsID { get; set; }
        public int? HrPslEmployeeJobLevelID { get; set; }
        public string DrivingLicenseNumber { get; set; }
        public DateTime? DrivingLicenseIssueDate { get; set; }
        public DateTime? DrivingLicenseExpireDate { get; set; }
        public string DrivingLicenseIssuer { get; set; }
        public int? DrivingLicenseType { get; set; }
        public int? ResidenceType { get; set; }
        public string ResidencyNumber { get; set; }
        public DateTime? ResidenceExpireDate { get; set; }
        public string BloodGroup { get; set; }
        public bool? HasChronicDisease { get; set; }
        public string ChronicDiseaseDescreption { get; set; }
        public bool? HasInjuries { get; set; }
        public string InjuriesDescreption { get; set; }
        public int? HrPslManagementId { get; set; }
        public bool? HasCar { get; set; }
        public int? GlBankID { get; set; }
        public string BankAccountNumber { get; set; }

        public List<HrPslEmployeeAcademicDegree> AcademicDegrees { get; set; }
        public List<HrPslEmployeeExperience> Experiences { get; set; }
        public List<HrPslEmployeeFamilyDetails> FamilyDetails { get; set; }
        public List<HrPslEmployeeTrainingCources> TrainingCourses { get; set; }
        public List<HrPslEmployeeContacts> Contacts { get; set; }
    }
}
