using ResortERP.Core;
using ResortERP.Core.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortERP.Service.IServices
{
    public interface ICustomersService
    {
        bool Insert(CustomersVM entity);
        Task<int> InsertAsync(CustomersVM entity);
        bool Update(CustomersVM entity);
        Task<bool> UpdateAsync(CustomersVM entity);

        bool Delete(CustomersVM entity);
        Task<bool> DeleteAsync(CustomersVM customer);
        Task<List<CustomersVM>> GetAllAsync(int pageNum, int pageSize, char type);
        Task<int> CountAsync(char type);
        List<CustomersVM> GetAll();

        int? InsertUpdateCustomerDependence(CustomersVM customer, List<TelephoneVM> telephones, List<CustomerBranchesVM> customerBran, char transaction);
        Task<List<CustomersVM>> GetSearchForCustomer(string Search);
        string GetLastCode();
        List<CUSTOMERS> getByNationID(int nationId);
        List<CUSTOMERS> getByGOVID(int govId);
        List<CUSTOMERS> getByTownID(int townId);
        List<CUSTOMERS> getByVilID(long villageId);
        List<CUSTOMERS> getByCompBranID(int compBran);
        List<CUSTOMERS> getByDept(int deptId);
        Task<List<CustomersVM>> getSearchForCustomer(string search);
        CustomersVM GetById(int id);

        Task<List<CustomersVM>> getChartsData();
      
    }
}
