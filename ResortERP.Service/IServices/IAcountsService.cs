using ResortERP.Core;
using ResortERP.Core.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortERP.Service.IServices
{
    public interface IAcountsService
    {
        bool Insert(AccountVM entity);
        int InsertGetID(AccountVM entity);
        Task<int> InsertAsync(AccountVM entity);
        bool Update(AccountVM entity);
        Task<bool> UpdateAsync(AccountVM entity);
        bool Delete(AccountVM entity);
        Task<bool> DeleteAsync(AccountVM entity);
        Task<List<AccountVM>> GetAllAsync(int pageNum, int pageSize);
        Task<int> CountAsync();
        List<AccountVM> GetAll();
        List<AccountVM> GetAllCustomerAccounts(int Type);
        List<AccountVM> GetAllBoxAccounts();
        List<AccountVM> getAllBankAccounts();
        List<AccountVM> GetAllGoldBoxAccounts();
        Task<List<AccountVM>> getSearchForAccount(string accountName, int pageNumA, int pageSizeA);
        Task<int> CountofSearchAsync(string accountName);
        Task<ACCOUNTS> GetByAccountID(int AccountID);
        Task<List<AccountVM>> GetSearch(string search);
        List<AccountVM> SearchItems(string SearchCriteria);
        AccountVM GetAccountDataByCode(string AccountCode, int Type);
        string GetLastCode();
        string GetCodeWithoutParent();
        Task<bool> GetByAccountcode(string AccountCode);
        AccountVM GetParentDataByID(int ParentID);

        string checkAccountIfUsed(int accountID);
        Task<AccountVM> GetByAccountIDTree(int AccountID);
        List<AccountsGuidVM> buildAccTree();
        List<AccountVM> GetAccountBoxByTypesForEntry(int EntryType);
        Task<List<ACCOUNTS>> GetAccountBoxByTypesForBill(int BillType, string AccType, int CompBran);
        List<ACCOUNTS> GetDefaultAccountsOfBillSettings(string AccType);
        Task<List<AccountVM>> GetAllMainAccounts();

        List<AccountVM> GetCustomerAccountsForComplexEntry();

        List<AccountVM> SearchAccountsForDepitGrid(string SearchCriteria, int EntryTypeID);
        List<AccountVM> SearchAccountsForCreditGrid(string SearchCriteria, int EntryTypeID);


        Task<List<ACCOUNTS>> GetGoldBoxByTypesForBill(string AccType, int CompBran);

        List<ACCOUNTS> GetAccountsFilteredByType(string AccType);

    }
}
