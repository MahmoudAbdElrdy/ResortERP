using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ResortERP.Core.VM;
using ResortERP.Core;

namespace ResortERP.Service.IServices
{
    public interface ICompanyStoresService
    {
        bool Insert(CompanyStoresVM entity);
        Task<short> InsertAsync(CompanyStoresVM entity);
        bool Update(CompanyStoresVM entity);
        Task<short> UpdateAsync(CompanyStoresVM entity);

        bool Delete(CompanyStoresVM entity);
        Task<bool> DeleteAsync(CompanyStoresVM entity);
        Task<List<CompanyStoresVM>> GetAllAsync(int pageNum, int pageSize);
        Task<int> CountAsync();
        List<CompanyStoresVM> GetAll();
        Task<List<CompanyStoresVM>> GetSearchForStore(string search);
        Task<List<CompanyStoresVM>> GetSearch(string search);
        string getLastCode();
        List<COMPANY_STORES> getCompBranByStoresID(int compBranId);
        List<COMPANY_STORES> getByNationID(int nationId);
        List<COMPANY_STORES> getByGOVID(int govId);
        List<COMPANY_STORES> getByTownID(int townId);
        List<COMPANY_STORES> getByVilID(long villageId);
        CompanyStoresVM GetById(int COM_STORE_ID);
        List<StoresGuidVM> getForTree();
    }
}
