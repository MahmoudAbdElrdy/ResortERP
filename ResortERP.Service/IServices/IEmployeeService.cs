using ResortERP.Core;
using ResortERP.Core.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortERP.Service.IServices
{
    public interface IEmployeeService
    {
        bool Insert(EMPLOYEEVM entity);
        Task<int> InsertAsync(HrPslEmployeeVM entity);
        bool Update(EMPLOYEEVM entity);
        Task<bool> UpdateAsync(HrPslEmployeeVM entity);

        bool Delete(EMPLOYEEVM entity);
        Task<bool> DeleteAsync(HrPslEmployeeVM entity);
        Task<List<HrPslEmployeeVM>> GetAllAsync(int pageNum, int pageSize);
        Task<int> CountAsync();
        List<EMPLOYEEVM> GetAll();
        List<EMPLOYEEVM> GetAllSalesEmployees();


        int InsertGetID(EMPLOYEEVM entity);
        Task<int> InsertGetIDAsync(EMPLOYEEVM entity);
        List<EMPLOYEEVM> GetByTypeID(int typeID);
        Task<List<EMPLOYEEVM>> GetSearchForStore(string Search, int typeID);
        string GetLastCode();
        List<EMPLOYEES> getByEmpType(int empTypeId);
        List<EMPLOYEES> getByNationID(int nationId);
        List<EMPLOYEES> getByGOVID(int govId);
        List<EMPLOYEES> getByTownID(int townId);
        List<EMPLOYEES> getByVilID(long villageId);
        List<EMPLOYEES> getByDeptID(long deptId);

        EMPLOYEEVM getById(int EMP_ID);

        List<DropDownMenuOptionsVM> getMartialStatusList();
        List<DropDownMenuOptionsVM> getRelegionList();
        List<DropDownMenuOptionsVM> getCityList();
        List<DropDownMenuOptionsVM> getNationalityList();
        List<DropDownMenuOptionsVM> getJobTitleList();
        List<DropDownMenuOptionsVM> getJobLevelList();
        List<DropDownMenuOptionsVM> getDeptartmentList();
        List<DropDownMenuOptionsVM> getEmployeeStatusList();
        List<DropDownMenuOptionsVM> getDirectManagerList();
        List<DropDownMenuOptionsVM> getContactTypeList();
        List<DropDownMenuOptionsVM> getAcademicDegreeList();
        List<DropDownMenuOptionsVM> getBankList();

        List<HrPslEmployeeAcademicDegree> getEmployeeAcademicDegrees(int employeeId);
        List<HrPslEmployeeExperience> getEmployeeExperiences(int employeeId);
        List<HrPslEmployeeFamilyDetails> getEmployeeFamilyDetails(int employeeId);
        List<HrPslEmployeeTrainingCources> getEmployeeTrainingCourses(int employeeId);
        List<HrPslEmployeeContacts> getEmployeeContacts(int employeeId);
    }
}
