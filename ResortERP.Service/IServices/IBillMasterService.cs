using ResortERP.Core;
using ResortERP.Core.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortERP.Service.IServices
{
    public interface IBillMasterService
    {
        bool Insert(BILL_MASTERVM entity);

        Task<long> InsertAsync(BILL_MASTERVM entity);

        bool Update(BILL_MASTERVM entity);
        Task<bool> UpdateAsync(BILL_MASTERVM entity);

        bool Delete(BILL_MASTERVM entity);
        Task<bool> DeleteAsync(BILL_MASTERVM entity);
        Task<List<BILL_MASTERVM>> GetAllAsync(int branchId, int billType, int pageNum, int pageSize);
        Task<BILL_MASTERVM> getBillByBillNumber(int billNumber,int billType);        
        Task<int> CountAsync(int billType);
        Task<long> GetLastBillNumber(int branchId, int settingID);

        Task<BILL_MASTERVM> GetByBillID(int billID);
        Task<bool> UpdateBillEntryByID(BILL_MASTERVM entity, long entryID);

        Task<bool> CheckBillByBillNumber(int billNumber, int billType);
        int getPaginationByType(int billSettingID);
        List<BILL_MASTERVM> GetAllBillsBySettingID(int settingID);
        List<BILL_MASTER> getBillStores(int storeID);
        List<BILL_MASTER> getByCostCenter(int costCenterId);
        List<BILL_MASTERVM> GetBillsForSearch(BillSearch BillSearchObj);
        List<BILL_MASTER> getByCurrency(int currencyId);
        List<BILL_MASTER> getByEmp(int empId);
        List<BILL_MASTER> getByCust(int custId);
        List<BILL_MASTER> getByCompBranId(int compBranId);
        List<BILL_MASTER> getByPayTypeId(string payId);
        List<BILL_MASTER> getByItemId(int itemId);

        List<AccountStatementVM> GetAccountStatement(int AccID);

        List<AccountStatementVM> Get_PRC_RPT_LEDGER(string companyBranches, string Accounts, string StartDate, string EndDate);

        List<AccountStatementGoldVM> Get_PRC_RPT_LEDGER_Gold(string companyBranches, string Accounts, string StartDate, string EndDate);

        List<TrialBalance> Get_PRC_RPT_TRIALBALANCE(string companyBranches,  string StartDate, string EndDate, string Type);

        List<List<AccountStatementVM>> Get_PRC_RPT_LEDGER(string companyBranches, string Accounts, string CostCenterId, string StartDate, string EndDate);

        List<List<AccountStatementGoldVM>> Get_PRC_RPT_LEDGER_Gold(string companyBranches, string Accounts, string CostCenterId, string Source, string StartDate, string EndDate);

        List<TrialBalanceGoldVM> Get_PRC_RPT_TRIALBALANCE_Gold(string companyBranches, string StartDate, string EndDate, string Type);

        List<InvoiceMotionViewVM> GetInvoiceMotionReportSearch(InvoiceMotionSearchModel searchObject);

        List<DetectionReportTotalVM> getAccountDetectingReportData(string companyBranches, string startDate, string endDate);

        List<SalesTaxViewVM> getSalesTaxReportData(string companyBranches, string startDate, string endDate);

        List<PurchasingTaxViewVM> getPurchasingTaxReportData(string companyBranches, string startDate, string endDate);

        List<DetectionReportTotalVM> getAccountDetectingReportDataChart(string companyBranches, string startDate, string endDate);

        List<AccountStatementVM> getAccountStatementDailyReport(string companyBranches, string accounts, string date);

        List<AccountStatementGoldVM> getAccountStatementGoldDailyReport(string companyBranches, string Accounts, string Date);

        List<AccountStatementGoldMonthlyVM> getAccountStatementGoldMonthlyReport(string companyBranches, string accounts, string startDate, string endDate);

        List<AccountStatementMonthlyVM> getAccountStatementMonthlyReport(string companyBranches, string accounts, string startDate, string endDate);

        /*******************************************/
        List<AccountStatementVM> getExpensesTaxReport(string companyBranches, string accounts, string startDate, string endDate);
        List<AccountStatementVM> getExpensesTaxDailyReport(string companyBranches, string accounts, string date);
        List<AccountStatementMonthlyVM> getExpensesTaxMonthlyReport(string companyBranches, string accounts, string startDate, string endDate);
        List<BILL_MASTERVM> getNotPotedBills(BillPostingSearchVM searchObject);
        bool SetBillsPosted(List<int> BillIds);
    }
}
