using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ResortERP.Service.IServices;
using ResortERP.Repository;
using ResortERP.Core;
using ResortERP.Core.VM;
using System.Drawing;
using System.IO;
using SolversTeamERP.Core.VM;
using System.Data.SqlClient;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Core.Objects;
using ResortERP.Service;

namespace ResortERP.Service.Services
{
    public class ReportsService : IReportsService, IDisposable
    {
        IGenericRepository<BILL_MASTER> billMasterRepo;
        IGenericRepository<ITEMS_GROUPS> itemGroupRepo;
        IGenericRepository<COST_CENTERS> costCenterRepo;
        IGenericRepository<COMPANY_STORES> storeRepo;
        IGenericRepository<COMPANY_BRANCHES> branchRepo;
        IGenericRepository<SELLS_TYPES> sellTypeRepo;
        IGenericRepository<ACCOUNTS> accountRepo;

        private IDbContext context;

        public void Dispose()
        {
            billMasterRepo.Dispose();
            itemGroupRepo.Dispose();
            costCenterRepo.Dispose();
            storeRepo.Dispose();
            branchRepo.Dispose();
            sellTypeRepo.Dispose();
            context.Dispose();
        }


        public ReportsService(IGenericRepository<BILL_MASTER> _billMasterRepo, IGenericRepository<ITEMS_GROUPS> _itemGroupRepo,
        IGenericRepository<COST_CENTERS> _costCenterRepo, IGenericRepository<COMPANY_STORES> _storeRepo, IGenericRepository<COMPANY_BRANCHES> _branchRepo,
        IGenericRepository<SELLS_TYPES> _sellTypeRepo, IGenericRepository<ACCOUNTS> _accountRepo, IDbContext _context)
        {
            billMasterRepo = _billMasterRepo;
            costCenterRepo = _costCenterRepo;
            storeRepo = _storeRepo;
            branchRepo = _branchRepo;
            itemGroupRepo = _itemGroupRepo;
            sellTypeRepo = _sellTypeRepo;
            context = _context;
            accountRepo = _accountRepo;
        }

        public Task<List<DropDownMenuOptionsVM>> getBranchesList()
        {
            return Task.Run(() =>
            {
                return branchRepo.GetAll().Select(p => new DropDownMenuOptionsVM
                {
                    OptionValue = p.COM_BRN_ID,
                    OptionText = p.COM_BRN_AR_NAME,
                    OptionTextEn = p.COM_BRN_EN_NAME
                }).ToList();
            });
        }

        public Task<List<DropDownMenuOptionsVM>> getCostCenterList()
        {
            return Task.Run(() =>
            {
                return costCenterRepo.GetAll().Select(p => new DropDownMenuOptionsVM
                {
                    OptionValue = p.COST_CENTER_ID,
                    OptionText = p.COST_CENTER_AR_NAME,
                    OptionTextEn = p.COST_CENTER_EN_NAME
                }).ToList();
            });
        }

        public Task<List<DropDownMenuOptionsVM>> getStoresList()
        {
            return Task.Run(() =>
            {
                return storeRepo.GetAll().Select(p => new DropDownMenuOptionsVM
                {
                    OptionValue = p.COM_STORE_ID,
                    OptionText = p.COM_STORE_AR_NAME,
                    OptionTextEn = p.COM_STORE_EN_NAME
                }).ToList();
            });
        }

        public Task<List<DropDownMenuOptionsVM>> getItemGroupList()
        {
            return Task.Run(() =>
            {
                return itemGroupRepo.GetAll().Select(p => new DropDownMenuOptionsVM
                {
                    OptionValue = p.GROUP_ID,
                    OptionText = p.GROUP_AR_NAME,
                    OptionTextEn = p.GROUP_EN_NAME
                }).ToList();
            });
        }
        
        public Task<List<DropDownMenuOptionsVM>> getSellTypeList()
        {
            return Task.Run(() =>
            {
                return sellTypeRepo.GetAll().Select(p => new DropDownMenuOptionsVM
                {
                    OptionValue = p.SELL_TYPE_ID,
                    OptionText = p.SELL_TYPE_AR_NAME,
                    OptionTextEn = p.SELL_TYPE_EN_NAME
                }).ToList();
            });
        }

        public Task<ItemsInventoryReportResultVM> getItemsInventoryReportResult(ItemsInventoryReportSearchVM searchObject)
        {
            return Task.Run(() =>
            {
                try
                {
                    var itemsInventory = new ItemsInventoryReportResultVM();

                    string Options = "", Select = "", ToSelect = "", SortBy= " order by ";
                    if (searchObject.StoreId != 0)
                    {
                        Options += " AND (BILL_MASTER.COM_STORE_ID = " + searchObject.StoreId + " OR  BILL_MASTER.TO_COM_STORE_ID = " + searchObject.StoreId + ") ";
                    }
                    if (searchObject.ShowZeroBalancesOnly)
                    {
                        Options += " AND QTY = 0 ";
                    }
                    else if (searchObject.ShowZeroBalances && searchObject.ShowNegativeBalancesOnly)
                    {
                        Options += " AND QTY <= 0 ";
                    }
                    else if (searchObject.ShowNegativeBalancesOnly)
                    {
                        Options += " AND QTY < 0 ";
                    }
                    else if (!searchObject.ShowZeroBalances)
                    {
                        Options += " AND QTY <> 0 ";
                    }
                    if (searchObject.GroupId != null)
                    {
                        Options += " AND ITEMS.GROUP_ID = " + searchObject.GroupId;
                    }
                    if (searchObject.CostCenterId != 0)
                    {
                        Options += " AND BILL_MASTER.COST_CENTER_ID = " + searchObject.CostCenterId;
                    }
                    if (searchObject.ShowExpired)
                    {
                        Select += " , #BILL_DETAILS.EXPIRED_DATE ";
                        ToSelect += " , #BILL_DETAILS.EXPIRED_DATE ";
                    }
                    if (searchObject.ShowStoreDetails)
                    {
                        Select += ",BILL_MASTER.COM_STORE_ID ";
                        ToSelect += ",BILL_MASTER.COM_STORE_ID,BILL_MASTER.TO_COM_STORE_ID";
                    }
                    if (searchObject.ExpireDate != null)
                    {
                        Options += " AND #BILL_DETAILS.EXPIRED_DATE < " + searchObject.ExpireDate.Value;
                    }
                    if(searchObject.SortBy == 1)
                    {
                        SortBy += "ITEM_CODE";
                    }
                    else
                    {
                        SortBy += "ITEM_AR_NAME";
                    }

                    //SqlParameter companyBranches_Param = new SqlParameter("@CompanyBranchID", companyBranches);

                    SqlParameter Options_Param = new SqlParameter("@OPTIONS", Options);
                    SqlParameter Select_Param = new SqlParameter("@SELECT", Select);
                    SqlParameter ToSelect_Param = new SqlParameter("@TO_SELECT", ToSelect);
                    SqlParameter StoresDetails_Param = new SqlParameter("@STORES_DETAILS", searchObject.ShowStoreDetails);
                    SqlParameter ExpiredView_Param = new SqlParameter("@EXPIRED_VIEW", searchObject.ShowExpired);
                    SqlParameter Sort_Param = new SqlParameter("@SORT", SortBy);
                    SqlParameter Store_Param = new SqlParameter("@COM_STORE_ID", searchObject.StoreId);
                    SqlParameter STARTDATE_Param = new SqlParameter("@START_DATE", searchObject.dateFrom);
                    SqlParameter ENDDATE_Param = new SqlParameter("@END_DATE", searchObject.dateTo);
                    //SqlParameter COSTCENTER_ID_Param = new SqlParameter("@COSTCENTER_ID", DBNull.Value);
                    SqlParameter Group_Param;
                    if(searchObject.GroupId == null)
                    {
                        Group_Param = new SqlParameter("@GROUP_ID", DBNull.Value);
                    }
                    else
                    {
                        Group_Param = new SqlParameter("@GROUP_ID", searchObject.GroupId);
                    }

                    using (SqlConnection conn = new SqlConnection(context.Database.Connection.ConnectionString))
                    {
                        using (var cmd = new SqlCommand())
                        {
                            cmd.CommandText = "[RPT_ITEMS_INVENTORY]";
                            cmd.Connection = conn;
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add(Options_Param);
                            cmd.Parameters.Add(Select_Param);
                            cmd.Parameters.Add(ToSelect_Param);
                            cmd.Parameters.Add(StoresDetails_Param);
                            cmd.Parameters.Add(ExpiredView_Param);
                            cmd.Parameters.Add(Sort_Param);
                            cmd.Parameters.Add(STARTDATE_Param);
                            cmd.Parameters.Add(ENDDATE_Param);
                            cmd.Parameters.Add(Group_Param);

                            conn.Open();
                            
                            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                            DataSet ds = new DataSet();
                            adapter.Fill(ds);
                            itemsInventory.InputsOnGroups = ds.Tables[0].ConvertDataTable<ItemsInventoryReportGroupsResultVM>();
                            itemsInventory.OutputOnGroups = ds.Tables[1].ConvertDataTable<ItemsInventoryReportGroupsResultVM>();
                            itemsInventory.TransferOnGroups = ds.Tables[2].ConvertDataTable<ItemsInventoryReportGroupsResultVM>();
                            itemsInventory.Inputs = ds.Tables[3].ConvertDataTable<ItemsInventoryReportGroupsResultVM>();
                            itemsInventory.Outputs = ds.Tables[4].ConvertDataTable<ItemsInventoryReportGroupsResultVM>();
                            itemsInventory.Transfer = ds.Tables[5].ConvertDataTable<ItemsInventoryReportGroupsResultVM>();
                            itemsInventory.EmptyItems = ds.Tables[6].ConvertDataTable<ItemsInventoryReportGroupsResultVM>();
                            itemsInventory.EmptyItemsOnGroups = ds.Tables[7].ConvertDataTable<ItemsInventoryReportGroupsResultVM>();
                            itemsInventory.ItemsBalanceOnGroup = ds.Tables[8].ConvertDataTable<ItemsInventoryReportGroupsResultVM>();

                            conn.Close();
                        }
                    }

                    return itemsInventory;
                }
                catch (Exception e)
                {
                    return null;
                }
            });
        }

        public Task<ItemsInventoryReportResultVM> getItemsInventoryBalanceResult(ItemsInventoryReportSearchVM searchObject)
        {
            return Task.Run(() =>
            {
                try
                {
                    var itemsInventory = new ItemsInventoryReportResultVM();

                    string Options = "", Select = "", ToSelect = "";
                    if (searchObject.StoreId != 0)
                    {
                        Options += " AND COM_STORE_ID = " + searchObject.StoreId;
                    }
                    if (searchObject.ShowZeroBalancesOnly)
                    {
                        Options += " AND QTY = 0 ";
                    }
                    else if (searchObject.ShowZeroBalances && searchObject.ShowNegativeBalancesOnly)
                    {
                        Options += " AND QTY <= 0 ";
                    }
                    else if (searchObject.ShowNegativeBalancesOnly)
                    {
                        Options += " AND QTY < 0 ";
                    }
                    else if (!searchObject.ShowZeroBalances)
                    {
                        Options += " AND QTY <> 0 ";
                    }
                    if (searchObject.GroupId != null)
                    {
                        Options += " AND GROUP_ID = " + searchObject.GroupId;
                    }
                    if (searchObject.CostCenterId != 0)
                    {
                        Options += " AND COST_CENTER_ID = " + searchObject.CostCenterId;
                    }
                    if (searchObject.ShowExpired)
                    {
                        Select += " , EXPIRED_DATE ";
                        ToSelect += " , EXPIRED_DATE ";
                    }
                    if (searchObject.ExpireDate != null)
                    {
                        Options += " AND EXPIRED_DATE < " + searchObject.ExpireDate.Value;
                    }
                    if (searchObject.ShowStoreDetails)
                    {
                        Select += ",COM_STORE_ID ";
                        ToSelect += ",COM_STORE_ID";
                    }

                    //SqlParameter companyBranches_Param = new SqlParameter("@CompanyBranchID", companyBranches);

                    SqlParameter Options_Param = new SqlParameter("@OPTIONS", Options);
                    SqlParameter Select_Param = new SqlParameter("@SELECT", Select);
                    SqlParameter Store_Param = new SqlParameter("@COM_STORE_ID", searchObject.StoreId);
                    SqlParameter Sort_Param = new SqlParameter("@SORT", " order by ITEM_CODE ");
                    SqlParameter STARTDATE_Param = new SqlParameter("@START_DATE", searchObject.dateFrom);
                    SqlParameter ENDDATE_Param = new SqlParameter("@END_DATE", searchObject.dateTo);

                    using (SqlConnection conn = new SqlConnection(context.Database.Connection.ConnectionString))
                    {
                        using (var cmd = new SqlCommand())
                        {
                            cmd.CommandText = "[GET_ITEM_BALANCE]";
                            cmd.Connection = conn;
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add(Options_Param);
                            cmd.Parameters.Add(Select_Param);
                            cmd.Parameters.Add(Sort_Param);
                            cmd.Parameters.Add(STARTDATE_Param);
                            cmd.Parameters.Add(ENDDATE_Param);
                            cmd.Parameters.Add(Store_Param);

                            conn.Open();

                            SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                            DataSet ds = new DataSet();
                            adapter.Fill(ds);

                            var prices = ds.Tables[1].ConvertDataTable<ItemsInventoryReportPricesVM>();

                            itemsInventory.ItemsBalance =
                                (from b in ds.Tables[0].ConvertDataTable<ItemsInventoryReportBalanceResultVM>()
                                    join p in prices on b.ITEM_ID equals p.ITEM_ID
                                    select new ItemsInventoryReportBalanceResultVM
                                    {
                                        ITEM_CODE = b.ITEM_CODE,
                                        ITEM_AR_NAME = b.ITEM_AR_NAME,
                                        ITEM_EN_NAME = b.ITEM_EN_NAME,
                                        GROUP_ID = b.GROUP_ID,
                                        QTY = b.QTY,
                                        WHOLE_PRICEQTY = p.WHOLE_PRICEQTY,
                                        HALF_WHOLE_PRICEQTY = p.HALF_WHOLE_PRICEQTY,
                                        EMP_PRICEQTY = p.EMP_PRICEQTY,
                                        EXPORT_PRICEQTY = p.EXPORT_PRICEQTY,
                                        RETAIL_PRICEQTY = p.RETAIL_PRICEQTY,
                                        CONSUMER_PRICEQTY = p.CONSUMER_PRICEQTY,
                                        LAST_BUY_PRICEQTY = p.LAST_BUY_PRICEQTY
                                    }).ToList();
                            var totalPrices = new ItemsInventoryReportTotalPricesVM();
                            totalPrices.WHOLE_PRICE = (double) prices.Where(p => p.WHOLE_PRICEQTY != null).Sum(p => p.WHOLE_PRICEQTY);
                            totalPrices.HALF_WHOLE_PRICE = (double)prices.Where(p => p.HALF_WHOLE_PRICEQTY != null).Sum(p => p.HALF_WHOLE_PRICEQTY);
                            totalPrices.EMP_PRICE = (double)prices.Where(p => p.EMP_PRICEQTY != null).Sum(p => p.EMP_PRICEQTY);
                            totalPrices.CONSUMER_PRICE = (double)prices.Where(p => p.CONSUMER_PRICEQTY != null).Sum(p => p.CONSUMER_PRICEQTY);
                            totalPrices.EXPORT_PRICE = (double)prices.Where(p => p.EXPORT_PRICEQTY != null).Sum(p => p.EXPORT_PRICEQTY);
                            totalPrices.LAST_BUY_PRICE = (double)prices.Where(p => p.LAST_BUY_PRICEQTY != null).Sum(p => p.LAST_BUY_PRICEQTY);
                            totalPrices.RETAIL_PRICE = (double)prices.Where(p => p.RETAIL_PRICEQTY != null).Sum(p => p.RETAIL_PRICEQTY);
                            itemsInventory.TotalPrices = totalPrices;
                            conn.Close();
                        }
                    }

                    return itemsInventory;
                }
                catch (Exception e)
                {
                    return null;
                }
            });
        }

        public Task<AccountBalancesReportResultVM> getAccountBalancesReportResult(AccountBalancesReportSearchVM searchObject)
        {
            return Task.Run(() =>
            {
                var accountBalances = new AccountBalancesReportResultVM();

                try
                {
                    string Options = "", resources = "";
                    if (searchObject.CostCenterId != 0)
                    {
                        Options += " AND COST_CENTER_ID = " + searchObject.CostCenterId;
                    }
                    //if (searchObject.CurrencyId != 0)
                    //{
                    //    Options += " AND COST_CENTER_ID = " + searchObject.CostCenterId;
                    //}

                    //SqlParameter companyBranches_Param = new SqlParameter("@CompanyBranchID", companyBranches);
                    if (searchObject.ReportSources.Length > 0)
                    {
                        resources += " (";
                        foreach (int item in searchObject.ReportSources)
                        {
                            resources += " ENTRY_SETTING_ID =" + item + " OR ";
                        }

                        resources = resources.Remove(resources.Length - 4, 3) + ")";
                    }
                    else
                    {
                        resources += " 1=1";
                    }

                    SqlParameter Options_Param = new SqlParameter("@OPTIONS", Options);
                    SqlParameter Resources_Param = new SqlParameter("@RESOURCES", resources);
                    SqlParameter Accounts_Param = new SqlParameter("@ACCOUNTS", searchObject.Accounts);
                    SqlParameter ReportType_Param = new SqlParameter("@TYPE", searchObject.ReportType);
                    SqlParameter STARTDATE_Param = new SqlParameter("@STARTDATE", searchObject.startDate);
                    SqlParameter ENDDATE_Param = new SqlParameter("@ENDDATE", searchObject.endDate);
                    SqlParameter Number_Param = new SqlParameter("@NUMBER", searchObject.Number);

                    using (SqlConnection conn = new SqlConnection(context.Database.Connection.ConnectionString))
                    {
                        using (var cmd = new SqlCommand())
                        {
                            cmd.CommandText = "[RPT_NOT_MOVING_CUSTOMERS]";
                            cmd.Connection = conn;
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add(Options_Param);
                            cmd.Parameters.Add(Accounts_Param);
                            cmd.Parameters.Add(Resources_Param);
                            cmd.Parameters.Add(STARTDATE_Param);
                            cmd.Parameters.Add(ENDDATE_Param);
                            cmd.Parameters.Add(ReportType_Param);
                            cmd.Parameters.Add(Number_Param);

                            conn.Open();

                            SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                            DataSet ds = new DataSet();
                            adapter.Fill(ds);

                            double sum = 0, prevBalanceSum = 0;
                            accountBalances.Transactions =
                                (from trans in ds.Tables[0].ConvertDataTable<AccountBalancesTransactionsVM>()
                                    join accData in ds.Tables[1]
                                            .ConvertDataTable<AccountBalancesPreviousBalanceVM>()
                                        on trans.ACC_ID equals accData.ACC_ID
                                    join lastTrans in ds.Tables[2].ConvertDataTable<AccountBalancesLastTransactionsVM>()
                                        on trans.ACC_ID equals lastTrans.ACC_ID
                                    join pBalance in ds.Tables[3]
                                            .ConvertDataTable<AccountBalancesTransactionsVM>()
                                        on trans.ACC_ID equals pBalance.ACC_ID into x
                                    from prevBalance in x.DefaultIfEmpty()
                                    select new AccountBalancesTransactionsVM
                                    {
                                        ACC_ID = trans.ACC_ID,
                                        ACC_CODE = accData.ACC_CODE,
                                        ACC_AR_NAME = accData.ACC_AR_NAME,
                                        ACC_EN_NAME = accData.ACC_EN_NAME,
                                        NUMBER = trans.NUMBER,
                                        DEBIT = trans.DEBIT,
                                        CREDIT = trans.CREDIT,
                                        PreviousDebit = prevBalance == null ? 0 : prevBalance.DEBIT,
                                        PreviousCredit = prevBalance == null ? 0 : prevBalance.CREDIT,
                                        LAST_CREDIT = lastTrans.LAST_CREDIT,
                                        LAST_DEBIT = lastTrans.LAST_DEBIT,
                                    }
                                ).Select(p =>
                                {
                                    sum += (p.DEBIT == null ? 0 : (double) p.DEBIT) -
                                           (p.CREDIT == null ? 0 : (double) p.CREDIT);
                                    prevBalanceSum += (p.PreviousDebit == null ? 0 : (double) p.PreviousDebit) -
                                                      (p.PreviousCredit == null ? 0 : (double) p.PreviousCredit);
                                    p.Balance = sum;
                                    p.PreviousBalance = prevBalanceSum;
                                    return p;
                                }).ToList();

                            accountBalances.TotalCredit = (double)accountBalances.Transactions.Sum(p => p.CREDIT);
                            accountBalances.TotalDebit = (double)accountBalances.Transactions.Sum(p => p.DEBIT);
                            accountBalances.TotalBalance = accountBalances.Transactions.Any()
                                ? (double) accountBalances.Transactions.Last().Balance
                                : 0;
                            accountBalances.TotalPrevCredit = (double)accountBalances.Transactions.Sum(p => p.PreviousCredit);
                            accountBalances.TotalPrevDebit = (double)accountBalances.Transactions.Sum(p => p.PreviousDebit);
                            accountBalances.TotalPrevBalance = accountBalances.Transactions.Any()
                                ? (double)accountBalances.Transactions.Last().PreviousBalance
                                : 0;

                            conn.Close();
                        }
                    }

                    return accountBalances;
                }
                catch (Exception e)
                {
                    return accountBalances;
                }
            });
        }

        public Task<List<DropDownMenuOptionsVM>> getAccountsByType(string type)
        {
            return Task.Run(() =>
            {
                try
                {
                    switch (type)
                    {
                        case "A":
                            return accountRepo.GetAll().Select(p => new DropDownMenuOptionsVM()
                            {
                                OptionValue = p.ACC_ID,
                                OptionText = p.ACC_AR_NAME,
                                OptionTextEn = p.ACC_EN_NAME
                            }).ToList();
                        case "C":
                            return accountRepo
                                .FilterAsync(p =>
                                    p.ACC_TYPE_ID == (byte) AccountEnum.Acc5 ||
                                    p.ACC_TYPE_ID == (byte) AccountEnum.Acc7).Result.Select(p =>
                                    new DropDownMenuOptionsVM()
                                    {
                                        OptionValue = p.ACC_ID,
                                        OptionText = p.ACC_AR_NAME,
                                        OptionTextEn = p.ACC_EN_NAME
                                    }).ToList();
                        case "S":
                            return accountRepo
                                .FilterAsync(p =>
                                    p.ACC_TYPE_ID == (byte) AccountEnum.Acc6 ||
                                    p.ACC_TYPE_ID == (byte) AccountEnum.Acc7).Result.Select(p =>
                                    new DropDownMenuOptionsVM()
                                    {
                                        OptionValue = p.ACC_ID,
                                        OptionText = p.ACC_AR_NAME,
                                        OptionTextEn = p.ACC_EN_NAME
                                    }).ToList();
                        default:
                            return null;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            });
        }
    }
}
