using ResortERP.Core.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortERP.Service.IServices
{
    public interface ICommonService
    {
        List<NATIONVM> GetAllNationsAsync();
        List<GOVERNORATEVM> GetAllGovernateAsync();
        List<TOWNVM> GetAllTownsAsync();
        List<VILLAGEVM> GetAllVillagesAsync();
        List<EMPLOYEEVM> GetAllEmployeesAsync();
        List<COMPANY_BRANCHESVM> GetAllCompanyBranchesAsync();

        List<GOVERNORATEVM> GetGovernatesByNationID(int NationID);
        List<TOWNVM> GetTownsByGovernateID(int GovernateID);
        List<VILLAGEVM> GetVillageByTownID(int TownID);
        List<DepartmentVM> GetDepartmentByCompanyBranchID(int COM_BRAN_ID);

        string GetCompanyBranchNameByID(int BranID);
        string GetEmployeeNameByID(int EmpID);
        string GetLoggedDatabaseName();
        Task<Repository.ConnectionString> GetLoggedDatabase();
        List<string> GetInstalledPrinters();
        List<int> GetUserModule(string Id);
    }
}
