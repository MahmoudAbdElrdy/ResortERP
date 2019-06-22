using System;
using System.Collections.Generic;

namespace ResortERP.Core
{
    public partial class HrPslEmployee
    {
        public HrPslEmployee()
        {
   //         HRCustody = new HashSet<HRCustody>();
   //         HREmpVacOpeningBalance = new HashSet<HREmpVacOpeningBalance>();
   //         HRPermission = new HashSet<HRPermission>();
   //         HRVacationDelegationToNavigation = new HashSet<HRVacation>();
   //         HRVacationHrPslEmployee = new HashSet<HRVacation>();
   //         HrPrEmployeeDeduction = new HashSet<HrPrEmployeeDeduction>();
   //         HrPrEmployeeEarnings = new HashSet<HrPrEmployeeEarnings>();
   //         HrPrEmployeeInstallment = new HashSet<HrPrEmployeeInstallment>();
   //         HrPrEmployeeSalary = new HashSet<HrPrEmployeeSalary>();
   //         HrPslEmployeeAcademicDegree = new HashSet<HrPslEmployeeAcademicDegree>();
   //         HrPslEmployeeAnnualVacationBalance = new HashSet<HrPslEmployeeAnnualVacationBalance>();
   //         HrPslEmployeeExperience = new HashSet<HrPslEmployeeExperience>();
   //         HrPslEmployeeFamilyDetails = new HashSet<HrPslEmployeeFamilyDetails>();
   //         HrPslEmployeePunishmentHrPslEmployee = new HashSet<HrPslEmployeePunishment>();
   //         HrPslEmployeePunishmentInvestigatorEmployee = new HashSet<HrPslEmployeePunishment>();
   //         HrTaEmpAttendence = new HashSet<HrTaEmpAttendence>();
   //         HrTaEmpTAMachine = new HashSet<HrTaEmpTAMachine>();
   //         HrTaEmployeeAttendanceCalculations = new HashSet<HrTaEmployeeAttendanceCalculations>();
   //         HrTaEmployeeWorkPeriod = new HashSet<HrTaEmployeeWorkPeriod>();
			//HrPslEmployeeTrainingCources = new HashSet<HrPslEmployeeTrainingCources>();
        }

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

  //      public CompanyBranches HrPslBranch { get; set; }
  //      public HrPslCity HrPslCity { get; set; }
  //      public HrPslCountry HrPslCounty { get; set; }
  //      public Departments Deptartments { get; set; }
  //      //public HrPslEducationLevel HrPslEducationLevel { get; set; }
  //      public HrPslEmployeeJobLevel HrPslEmployeeJobLevel { get; set; }
  //      public HrPslEmployee HrPslEmployeeNavigation { get; set; }
  //      public HrPslEmployeeStatus HrPslEmployeeStatus { get; set; }
  //      public HrPslJobTitle HrPslJobTitle { get; set; }
  //      public HrPslMartialStatus HrPslMaritalstatus { get; set; }
  //      public HrPslNationality HrPslNationality { get; set; }
  //      public HrPslReligion HrPslReligion { get; set; }
  //      public HrPslEmployee InverseHrPslEmployeeNavigation { get; set; }
  //      public GlBank GlBank { get; set; }
  //      public ICollection<HRCustody> HRCustody { get; set; }
  //      public ICollection<HREmpVacOpeningBalance> HREmpVacOpeningBalance { get; set; }
  //      public ICollection<HRPermission> HRPermission { get; set; }
  //      public ICollection<HRVacation> HRVacationDelegationToNavigation { get; set; }
  //      public ICollection<HRVacation> HRVacationHrPslEmployee { get; set; }
  //      public ICollection<HrPrEmployeeDeduction> HrPrEmployeeDeduction { get; set; }
  //      public ICollection<HrPrEmployeeEarnings> HrPrEmployeeEarnings { get; set; }
  //      public ICollection<HrPrEmployeeInstallment> HrPrEmployeeInstallment { get; set; }
  //      public ICollection<HrPrEmployeeSalary> HrPrEmployeeSalary { get; set; }
  //      public ICollection<HrPslEmployeeAcademicDegree> HrPslEmployeeAcademicDegree { get; set; }
  //      public ICollection<HrPslEmployeeAnnualVacationBalance> HrPslEmployeeAnnualVacationBalance { get; set; }
  //      public ICollection<HrPslEmployeeExperience> HrPslEmployeeExperience { get; set; }
  //      public ICollection<HrPslEmployeeFamilyDetails> HrPslEmployeeFamilyDetails { get; set; }
  //      public ICollection<HrPslEmployeePunishment> HrPslEmployeePunishmentHrPslEmployee { get; set; }
  //      public ICollection<HrPslEmployeePunishment> HrPslEmployeePunishmentInvestigatorEmployee { get; set; }
  //      public ICollection<HrTaEmpAttendence> HrTaEmpAttendence { get; set; }
  //      public ICollection<HrTaEmpTAMachine> HrTaEmpTAMachine { get; set; }
  //      public ICollection<HrTaEmployeeAttendanceCalculations> HrTaEmployeeAttendanceCalculations { get; set; }
  //      public ICollection<HrTaEmployeeWorkPeriod> HrTaEmployeeWorkPeriod { get; set; }
		//public ICollection<HrPslEmployeeTrainingCources> HrPslEmployeeTrainingCources { get; set; }
    }
}
