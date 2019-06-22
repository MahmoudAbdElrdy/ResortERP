using ResortERP.Core;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Configuration;
using System.Web;

namespace ResortERP.Repository
{
    public interface IDbContext : IDisposable
    {
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        int SaveChanges();
        Task<int> SaveChangesAsync();
        DbEntityEntry Entry(object entity);
        Database Database { get; }
    }

    public class SmartContext : DbContext, IDbContext
    {
        public SmartContext(ISmartConnectionStringProvider smartConnectionStringProvider) : base(smartConnectionStringProvider.getCurrentConnectionString())
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("UID");
            modelBuilder.Entity<ITEMS_GROUPS>().ToTable("ITEMS_GROUPS");
            modelBuilder.Entity<COMPANY_STORES>().ToTable("COMPANY_STORES");
            modelBuilder.Entity<ITEMS>().ToTable("ITEMS");
            modelBuilder.Entity<COMPANY_BRANCHES>().ToTable("COMPANY_BRANCHES");
            modelBuilder.Entity<GOVERNORATES>().ToTable("GOVERNORATES");
            modelBuilder.Entity<NATIONS>().ToTable("NATIONS");
            modelBuilder.Entity<TOWNS>().ToTable("TOWNS");
            modelBuilder.Entity<VILLAGES>().ToTable("VILLAGES");
            modelBuilder.Entity<ITEMS_UNITS>().ToTable("ITEMS_UNITS");
            modelBuilder.Entity<UNITS>().ToTable("UNITS");
            modelBuilder.Entity<BILL_DETAILS>().ToTable("BILL_DETAILS");
            modelBuilder.Entity<BILL_EFFECTS>().ToTable("BILL_EFFECTS");
            modelBuilder.Entity<BILL_GRID_COLUMNS>().ToTable("BILL_GRID_COLUMNS");
            modelBuilder.Entity<BILL_MASTER>().ToTable("BILL_MASTER");
            modelBuilder.Entity<BILL_SETTINGS>().ToTable("BILL_SETTINGS");
            modelBuilder.Entity<BILL_TYPES>().ToTable("BILL_TYPES");
            modelBuilder.Entity<TPAY_TYPES>().ToTable("TPAY_TYPES");
            modelBuilder.Entity<ACCOUNTS>().ToTable("ACCOUNTS");
            modelBuilder.Entity<ACCOUNTS_GROUP>().ToTable("ACCOUNTS_GROUP");
            modelBuilder.Entity<ACCOUNTS_TYPES>().ToTable("ACCOUNTS_TYPES");
            modelBuilder.Entity<SELLS_TYPES>().ToTable("SELLS_TYPES");
            modelBuilder.Entity<ITEM_PRICES>().ToTable("ITEM_PRICES");
            modelBuilder.Entity<SysDatabase>().ToTable("SysDatabase");
            modelBuilder.Entity<Currency>().ToTable("CURRENCIES");
            modelBuilder.Entity<Department>().ToTable("DEPARTMENTS");
            modelBuilder.Entity<EMPLOYEES>().ToTable("EMPLOYEES");
            modelBuilder.Entity<EmployeeTypes>().ToTable("EMPLOYEES_TYPES");
            modelBuilder.Entity<Telephones>().ToTable("TELEPHONES");
            modelBuilder.Entity<TelephonCat>().ToTable("TELEPHONE_CAT");
            modelBuilder.Entity<TelephoneTypes>().ToTable("TELEPHONES_TYPES");
            modelBuilder.Entity<CUSTOMERS>().ToTable("CUSTOMERS");
            modelBuilder.Entity<CustomerBranches>().ToTable("CUSTOMER_BRANCHES");
            modelBuilder.Entity<CustomerSon>().ToTable("CUSTOMER_SON");
            modelBuilder.Entity<SmartSysDataBases_View>().ToTable("SmartSysDataBases_View");
            modelBuilder.Entity<UID_View>().ToTable("UID_View");
            modelBuilder.Entity<COST_CENTERS>().ToTable("COST_CENTERS");
            modelBuilder.Entity<RPT_ITEM_MOTION_VIEW>().ToTable("RPT_ITEM_MOTION_VIEW");
            modelBuilder.Entity<GetCustomerSupplierData>().ToTable("GetCustomerSupplierData");
            modelBuilder.Entity<COMPANY_STORES_ITEMS_GROUPS>().ToTable("COMPANY_STORES_ITEMS_GROUPS");
            modelBuilder.Entity<BILL_SHOW_OPTIONS>().ToTable("BILL_SHOW_OPTIONS");
            modelBuilder.Entity<BILL_OPTIONS>().ToTable("BILL_OPTIONS");
            modelBuilder.Entity<ENTRY_SETTINGS>().ToTable("ENTRY_SETTINGS");
            modelBuilder.Entity<ENTRY_TYPES>().ToTable("ENTRY_TYPES");
            modelBuilder.Entity<ENTRY_DETAILS>().ToTable("ENTRY_DETAILS");
            modelBuilder.Entity<ENTRY_GRID_COLUMNS>().ToTable("ENTRY_GRID_COLUMNS");
            modelBuilder.Entity<ENTRY_MASTER>().ToTable("ENTRY_MASTER");
            modelBuilder.Entity<ENTRY_SHOW_OPTIONS>().ToTable("ENTRY_SHOW_OPTIONS");
            modelBuilder.Entity<SYSTEM_OPTIONS>().ToTable("SYSTEM_OPTIONS");
            modelBuilder.Entity<SHORTCUTS>().ToTable("SHORTCUTS");
            modelBuilder.Entity<UserForm>().ToTable("UserForm");
            modelBuilder.Entity<UserGroup>().ToTable("UserGroup");
            modelBuilder.Entity<UserLogFile>().ToTable("UserLogFile");
            modelBuilder.Entity<UserMenu>().ToTable("UserMenu");
            modelBuilder.Entity<UserModule>().ToTable("UserModule");
            modelBuilder.Entity<UserOperation>().ToTable("UserOperation");
            modelBuilder.Entity<UserPrivilige>().ToTable("UserPrivilige");
            modelBuilder.Entity<Message>().ToTable("Message");
            modelBuilder.Entity<MessagesView>().ToTable("MessagesView");
            modelBuilder.Entity<Notification>().ToTable("Notification");
            modelBuilder.Entity<NotificationsView>().ToTable("NotificationsView");
            modelBuilder.Entity<NotificationType>().ToTable("NotificationType");
            modelBuilder.Entity<ORDERS>().ToTable("ORDERS");
            modelBuilder.Entity<Emails>().ToTable("Emails");
            modelBuilder.Entity<KEST_OPTION>().ToTable("KEST_OPTION");
            modelBuilder.Entity<Income_Account_Types>().ToTable("Income_Account_Types");
            modelBuilder.Entity<TBOXACCOUNTS>().ToTable("TBOXACCOUNTS");
            modelBuilder.Entity<TSTORE>().ToTable("TSTORE");
            modelBuilder.Entity<TBUDGETACCOUNTS>().ToTable("TBUDGETACCOUNTS");
            modelBuilder.Entity<ItemCaliber>().ToTable("ItemCaliber");
            modelBuilder.Entity<ManufacturingWages>().ToTable("ManufacturingWages");
            modelBuilder.Entity<ItemStatus>().ToTable("ItemStatus");
            modelBuilder.Entity<CostCalculationType>().ToTable("CostCalculationType");
            modelBuilder.Entity<GoldWorldPrice>().ToTable("GoldWorldPrice");
            modelBuilder.Entity<Bank>().ToTable("Bank");
            modelBuilder.Entity<DashBoard>().ToTable("DashBoard");
            
            modelBuilder.Entity<CurrencyCategories>().ToTable("CurrencyCategories");
            modelBuilder.Entity<AddressPartsSettings>().ToTable("AddressPartsSettings");
            modelBuilder.Entity<BILL_PAY_TYPES>().ToTable("BILL_PAY_TYPES");
            modelBuilder.Entity<BillCaliberTransactions>().ToTable("BillCaliberTransactions");

            modelBuilder.Entity<ResourcesTranslation>().ToTable("ResourcesTranslation");
            modelBuilder.Entity<Resources>().ToTable("Resources");

            modelBuilder.Entity<UserPriviligeBranches>().ToTable("UserPriviligeBranches");
            modelBuilder.Entity<ResourcesTranslation>().ToTable("ResourcesTranslation");
            modelBuilder.Entity<Resources>().ToTable("Resources");

            modelBuilder.Entity<AssetGroup>().ToTable("AssetGroup");
            modelBuilder.Entity<AssetMaster>().ToTable("AssetMaster");
            modelBuilder.Entity<AssetDepreciationDetails>().ToTable("AssetDepreciationDetails");
            modelBuilder.Entity<AssetStatus>().ToTable("AssetStatus");
            modelBuilder.Entity<AssetType>().ToTable("AssetType");
            modelBuilder.Entity<AssetLifeSpanUnit>().ToTable("AssetLifeSpanUnit");
            modelBuilder.Entity<AssetDepreciationType>().ToTable("AssetDepreciationType");
            modelBuilder.Entity<AssetCompanyDetails>().ToTable("AssetCompanyDetails");
            modelBuilder.Entity<AssetOperationMaster>().ToTable("AssetOperationMaster");
            modelBuilder.Entity<AssetOperationDetails>().ToTable("AssetOperationDetails");

            //ITEMS_GROUPS 

            //******** HR Tables *********//
            modelBuilder.Entity<HrPslEmployee>().ToTable("HrPslEmployee");
            modelBuilder.Entity<HrPslEmployeeContacts>().ToTable("HrPslEmployeeContacts");
            modelBuilder.Entity<HrPslEmployeeExperience>().ToTable("HrPslEmployeeExperience");
            modelBuilder.Entity<HrPslEmployeeFamilyDetails>().ToTable("HrPslEmployeeFamilyDetails");
            modelBuilder.Entity<HrPslEmployeeTrainingCources>().ToTable("HrPslEmployeeTrainingCources");
            modelBuilder.Entity<HrPslEmployeeAcademicDegree>().ToTable("HrPslEmployeeAcademicDegree");
            modelBuilder.Entity<HrPslMartialStatus>().ToTable("HrPslMartialStatus");
            modelBuilder.Entity<GlBank>().ToTable("GlBank");
            modelBuilder.Entity<HrPslCity>().ToTable("HrPslCity");
            modelBuilder.Entity<HrPslReligion>().ToTable("HrPslReligion");
            modelBuilder.Entity<HrPslNationality>().ToTable("HrPslNationality");
            modelBuilder.Entity<HrPslJobTitle>().ToTable("HrPslJobTitle");
            modelBuilder.Entity<HrPslEmployeeStatus>().ToTable("HrPslEmployeeStatus");
            modelBuilder.Entity<HrPslEmployeeJobLevel>().ToTable("HrPslEmployeeJobLevel");
            modelBuilder.Entity<HrPslContactType>().ToTable("HrPslContactType");
            modelBuilder.Entity<HrPslAcademicDegree>().ToTable("HrPslAcademicDegree");
 
            //***************************//
        }
        public new DbSet<TEntity> Set<TEntity>() where TEntity : class { return base.Set<TEntity>(); }
        public new int SaveChanges() { return base.SaveChanges(); }
        public new Task<int> SaveChangesAsync() { return base.SaveChangesAsync(); }
        public new DbEntityEntry Entry(object entity) { return base.Entry(entity); }
        public new Database Database { get { return base.Database; } }

    }

    public class SmartConnectionStringProvider : ISmartConnectionStringProvider
    {
        private readonly Func<ClaimsPrincipal> _claimsPrincipalFactory;
        public SmartConnectionStringProvider(Func<ClaimsPrincipal> claimsPrincipalFactory)
        {
            _claimsPrincipalFactory = claimsPrincipalFactory;
        }
        //public string GetConnectionString()
        //{
        //    return getCurrentConnectionString();
        //}
        public string getCurrentConnectionString()
        {
            var defaultConnectionString = ConfigurationManager.ConnectionStrings["smartContext"].ConnectionString;
            // ClaimsPrincipal icp = Thread.CurrentPrincipal as ClaimsPrincipal;
            ClaimsPrincipal icp = _claimsPrincipalFactory();
            //var _icp = HttpContext.Current.User;
            if (icp == null) return defaultConnectionString;
            // Access IClaimsIdentity which contains claims
            ClaimsIdentity claimsIdentity = (ClaimsIdentity)icp.Identity;

            // Access claims
            foreach (Claim claim in claimsIdentity.Claims)
            {
                if (claim.Type == "connectionstring")
                {
                    return claim.Value;
                }

            }
            return defaultConnectionString;
        }
        //public void TestMethod()
        //{
        //    // Access the current principal
        //    var principal = _claimsPrincipalFactory();
        //}
    }

    public interface ISmartConnectionStringProvider
    {
        string getCurrentConnectionString();
    }




}
