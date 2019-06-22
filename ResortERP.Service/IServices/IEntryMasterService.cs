using ResortERP.Core;
using ResortERP.Core.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortERP.Service.IServices
{
    public interface IEntryMasterService
    {
        long Insert(Entry_MasterVM entity);
        Task<long> InsertAsync(Entry_MasterVM entity);
        bool Update(Entry_MasterVM entity);
        Task<bool> UpdateAsync(Entry_MasterVM entity);

        bool Delete(Entry_MasterVM entity);
        Task<bool> DeleteAsync(Entry_MasterVM entity);
        //Task<List<Entry_MasterVM>> GetAllAsync(int pageNum, int pageSize);
        Task<int> CountAsync(int entryType);
        List<Entry_MasterVM> GetAll();
        long InsertEntryMasterDetails(EntryMasterDetails Obj);
        long GetLastEntryNumber(int settingID);
        Task<List<Entry_MasterVM>> GetAllAsync(int entryType, int pageNum, int pageSize);
        List<ENTRY_DETAILS> GetAllDetails(long EntryMasterID);
        List<Entry_DetailsVM> GetDetailsByEntryId(long EntryMasterID);

        Task<long> InsertEntryBill(EntryMasterDetails Obj);
        Task<long> InsertEntryAsset(EntryMasterDetails Obj);
        
        EntryMasterDetails GetEntryByBillID(long billID);
        Entry_MasterVM GetEntryMasterByBill(long billID);
        Entry_MasterVM getEntryMasterByAssetOperation(long assetOperationId);
        Task<EntryMasterDetails> getEntryByEntryNumber(int entryNumber, int entryType);
        Task<bool> CheckEntryByEntryNumber(int billNumber, int billType);

        Task<EntryMasterDetails> getDailyEntryByMsterID(int masterID);
        Task<EntryMasterDetails> getEntryByEntryID(long entryID);
        Entry_MasterVM getEntryByaccountID(int accountID);

        Entry_MasterVM getEntryByCustID(int customerID);

        Task<bool> updateEntryOfBill(EntryMasterDetails masterDetails);
        Task<bool> deleteEntryOfBill(int billID);
        List<ENTRY_MASTER> getByCurrency(int currencyId);
        List<ENTRY_MASTER> getByEmp(int empId);
        List<ENTRY_MASTER> getByCust(int custId);
        bool SetEntriesPosted(List<int> entryIds);
        List<Entry_MasterVM> getNotPostedEntries(EntryPostingSearchVM searchObject);
    }
}
