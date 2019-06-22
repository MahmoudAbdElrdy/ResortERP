using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ResortERP.Core.VM;
using ResortERP.Core;

namespace ResortERP.Service.IServices
{
    public interface ICompanyBranchesService
    {
        Task<List<COMPANY_BRANCHESVM>> GetAllAsync(int pageNum, int pageSize);
        Task<int> InsertAsync(COMPANY_BRANCHESVM entity);
        Task<bool> UpdateAsync(COMPANY_BRANCHESVM entity);
        Task<bool> DeleteAsync(COMPANY_BRANCHESVM entity);
        Task<bool> getById(int id);
        Task<List<COMPANY_BRANCHESVM>> getMainComapnyBranches();
        Task<int> countAsync();
        string GetLastCode();
        List<COMPANY_BRANCHES> getByNationID(int nationId);
        List<COMPANY_BRANCHES> getByGOVID(int govId);
        List<COMPANY_BRANCHES> getByTownID(int townId);
        List<COMPANY_BRANCHES> getByVilID(long villageId);
        Task<List<COMPANY_BRANCHESVM>> GetAll();
        Task<List<COMPANY_BRANCHESVM>> GetAllReport(string id);
        Task<COMPANY_BRANCHESVM> getByIdLog(int COM_BRN_ID);
    }
}
