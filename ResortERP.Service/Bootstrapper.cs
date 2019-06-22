using SimpleInjector;
using ResortERP.Core;
using ResortERP.Repository;

namespace ResortERP.Service
{
    public static class Bootstrapper
    {
        public static void Bootstrap(Container container)
        {
            container.Options.AllowOverridingRegistrations = true;


            container.Register<IGenericRepository<User>, GenericRepository<User>>(container.Options.DefaultScopedLifestyle);
            container.Register<ICommonRepository<User>, CommonRepository<User>>(container.Options.DefaultScopedLifestyle);
            container.Register<ICommonRepository<SysDatabase>, CommonRepository<SysDatabase>>(container.Options.DefaultScopedLifestyle);
            container.Register<IGenericRepository<ITEMS_GROUPS>, GenericRepository<ITEMS_GROUPS>>(container.Options.DefaultScopedLifestyle);
            container.Register<IGenericRepository<ACCOUNTS_GROUP>, GenericRepository<ACCOUNTS_GROUP>>(container.Options.DefaultScopedLifestyle);
            container.Register<IGenericRepository<ITEMS>, GenericRepository<ITEMS>>(container.Options.DefaultScopedLifestyle);
            container.Register<IGenericRepository<COMPANY_STORES>, GenericRepository<COMPANY_STORES>>(container.Options.DefaultScopedLifestyle);
            container.Register<IGenericRepository<COMPANY_BRANCHES>, GenericRepository<COMPANY_BRANCHES>>(container.Options.DefaultScopedLifestyle);
            container.Register<IGenericRepository<EMPLOYEES>, GenericRepository<EMPLOYEES>>(container.Options.DefaultScopedLifestyle);
            container.Register<IGenericRepository<GOVERNORATES>, GenericRepository<GOVERNORATES>>(container.Options.DefaultScopedLifestyle);
            container.Register<IGenericRepository<NATIONS>, GenericRepository<NATIONS>>(container.Options.DefaultScopedLifestyle);
            container.Register<IGenericRepository<TOWNS>, GenericRepository<TOWNS>>(container.Options.DefaultScopedLifestyle);
            container.Register<IGenericRepository<VILLAGES>, GenericRepository<VILLAGES>>(container.Options.DefaultScopedLifestyle);
            container.Register<IGenericRepository<ITEMS_UNITS>, GenericRepository<ITEMS_UNITS>>(container.Options.DefaultScopedLifestyle);
            container.Register<IGenericRepository<UNITS>, GenericRepository<UNITS>>(container.Options.DefaultScopedLifestyle);
            container.Register<IGenericRepository<BILL_DETAILS>, GenericRepository<BILL_DETAILS>>(container.Options.DefaultScopedLifestyle);
            container.Register<IGenericRepository<BILL_EFFECTS>, GenericRepository<BILL_EFFECTS>>(container.Options.DefaultScopedLifestyle);
            container.Register<IGenericRepository<BILL_GRID_COLUMNS>, GenericRepository<BILL_GRID_COLUMNS>>(container.Options.DefaultScopedLifestyle);
            container.Register<IGenericRepository<BILL_MASTER>, GenericRepository<BILL_MASTER>>(container.Options.DefaultScopedLifestyle);
            container.Register<IGenericRepository<BILL_SETTINGS>, GenericRepository<BILL_SETTINGS>>(container.Options.DefaultScopedLifestyle);
            container.Register<IGenericRepository<BILL_TYPES>, GenericRepository<BILL_TYPES>>(container.Options.DefaultScopedLifestyle);
            container.Register<IGenericRepository<SELLS_TYPES>, GenericRepository<SELLS_TYPES>>(container.Options.DefaultScopedLifestyle);
            container.Register<IGenericRepository<ACCOUNT_DETAILS>, GenericRepository<ACCOUNT_DETAILS>>(container.Options.DefaultScopedLifestyle);
            container.Register<IGenericRepository<ACCOUNTS>, GenericRepository<ACCOUNTS>>(container.Options.DefaultScopedLifestyle);
            container.Register<IGenericRepository<ACCOUNTS_TYPES>, GenericRepository<ACCOUNTS_TYPES>>(container.Options.DefaultScopedLifestyle);
            container.Register<IGenericRepository<CUSTOMERS>, GenericRepository<CUSTOMERS>>(container.Options.DefaultScopedLifestyle);
            container.Register<IGenericRepository<CustomerSon>, GenericRepository<CustomerSon>>(container.Options.DefaultScopedLifestyle);
            container.Register<IGenericRepository<CustomerBranches>, GenericRepository<CustomerBranches>>(container.Options.DefaultScopedLifestyle);
            container.Register<IGenericRepository<TPAY_TYPES>, GenericRepository<TPAY_TYPES>>(container.Options.DefaultScopedLifestyle);
            container.Register<IGenericRepository<ITEM_PRICES>, GenericRepository<ITEM_PRICES>>(container.Options.DefaultScopedLifestyle);
            container.Register<IGenericRepository<Currency>, GenericRepository<Currency>>(container.Options.DefaultScopedLifestyle);
            container.Register<IGenericRepository<Department>, GenericRepository<Department>>(container.Options.DefaultScopedLifestyle);
            container.Register<IGenericRepository<EmployeeTypes>, GenericRepository<EmployeeTypes>>(container.Options.DefaultScopedLifestyle);
            container.Register<IGenericRepository<Telephones>, GenericRepository<Telephones>>(container.Options.DefaultScopedLifestyle);
            container.Register<IGenericRepository<TelephonCat>, GenericRepository<TelephonCat>>(container.Options.DefaultScopedLifestyle);
            container.Register<IGenericRepository<TelephoneTypes>, GenericRepository<TelephoneTypes>>(container.Options.DefaultScopedLifestyle);
            container.Register<IGenericRepository<SmartSysDataBases_View>, GenericRepository<SmartSysDataBases_View>>(container.Options.DefaultScopedLifestyle);
            container.Register<IGenericRepository<UID_View>, GenericRepository<UID_View>>(container.Options.DefaultScopedLifestyle);
            container.Register<IGenericRepository<UserGroup>, GenericRepository<UserGroup>>(container.Options.DefaultScopedLifestyle);
            container.Register<IGenericRepository<COST_CENTERS>, GenericRepository<COST_CENTERS>>(container.Options.DefaultScopedLifestyle);
            container.Register<IGenericRepository<RPT_ITEM_MOTION_VIEW>, GenericRepository<RPT_ITEM_MOTION_VIEW>>(container.Options.DefaultScopedLifestyle);
            container.Register<IGenericRepository<GetCustomerSupplierData>, GenericRepository<GetCustomerSupplierData>>(container.Options.DefaultScopedLifestyle);
            container.Register<IGenericRepository<COMPANY_STORES_ITEMS_GROUPS>, GenericRepository<COMPANY_STORES_ITEMS_GROUPS>>(container.Options.DefaultScopedLifestyle);
            container.Register<IGenericRepository<BILL_SHOW_OPTIONS>, GenericRepository<BILL_SHOW_OPTIONS>>(container.Options.DefaultScopedLifestyle);
            container.Register<IGenericRepository<BILL_OPTIONS>, GenericRepository<BILL_OPTIONS>>(container.Options.DefaultScopedLifestyle);
            container.Register<IGenericRepository<ENTRY_DETAILS>, GenericRepository<ENTRY_DETAILS>>(container.Options.DefaultScopedLifestyle);
            container.Register<IGenericRepository<ENTRY_GRID_COLUMNS>, GenericRepository<ENTRY_GRID_COLUMNS>>(container.Options.DefaultScopedLifestyle);
            container.Register<IGenericRepository<ENTRY_MASTER>, GenericRepository<ENTRY_MASTER>>(container.Options.DefaultScopedLifestyle);
            container.Register<IGenericRepository<ENTRY_SETTINGS>, GenericRepository<ENTRY_SETTINGS>>(container.Options.DefaultScopedLifestyle);
            container.Register<IGenericRepository<ENTRY_TYPES>, GenericRepository<ENTRY_TYPES>>(container.Options.DefaultScopedLifestyle);
            container.Register<IGenericRepository<ENTRY_SHOW_OPTIONS>, GenericRepository<ENTRY_SHOW_OPTIONS>>(container.Options.DefaultScopedLifestyle);
            container.Register<IGenericRepository<SYSTEM_OPTIONS>, GenericRepository<SYSTEM_OPTIONS>>(container.Options.DefaultScopedLifestyle);
            container.Register<IGenericRepository<SHORTCUTS>, GenericRepository<SHORTCUTS>>(container.Options.DefaultScopedLifestyle);
            container.Register<IGenericRepository<NotificationType>, GenericRepository<NotificationType>>(container.Options.DefaultScopedLifestyle);
            container.Register<IGenericRepository<Notification>, GenericRepository<Notification>>(container.Options.DefaultScopedLifestyle);
            container.Register<IGenericRepository<NotificationsView>, GenericRepository<NotificationsView>>(container.Options.DefaultScopedLifestyle);
            container.Register<IGenericRepository<Message>, GenericRepository<Message>>(container.Options.DefaultScopedLifestyle);
            container.Register<IGenericRepository<MessagesView>, GenericRepository<MessagesView>>(container.Options.DefaultScopedLifestyle);
            container.Register<IGenericRepository<UserMenu>, GenericRepository<UserMenu>>(container.Options.DefaultScopedLifestyle);
            container.Register<IGenericRepository<UserPrivilige>, GenericRepository<UserPrivilige>>(container.Options.DefaultScopedLifestyle);
            container.Register<IGenericRepository<ORDERS>, GenericRepository<ORDERS>>(container.Options.DefaultScopedLifestyle);
            container.Register<IGenericRepository<Emails>, GenericRepository<Emails>>(container.Options.DefaultScopedLifestyle);
            container.Register<IGenericRepository<KEST_OPTION>, GenericRepository<KEST_OPTION>>(container.Options.DefaultScopedLifestyle);
            container.Register<IGenericRepository<Income_Account_Types>, GenericRepository<Income_Account_Types>>(container.Options.DefaultScopedLifestyle);
            container.Register<IGenericRepository<TBOXACCOUNTS>, GenericRepository<TBOXACCOUNTS>>(container.Options.DefaultScopedLifestyle);
            container.Register<IGenericRepository<TSTORE>, GenericRepository<TSTORE>>(container.Options.DefaultScopedLifestyle);
            container.Register<IGenericRepository<TBUDGETACCOUNTS>, GenericRepository<TBUDGETACCOUNTS>>(container.Options.DefaultScopedLifestyle);
            container.Register<IGenericRepository<ItemCaliber>, GenericRepository<ItemCaliber>>(container.Options.DefaultScopedLifestyle);
            container.Register<IGenericRepository<ItemStatus>, GenericRepository<ItemStatus>>(container.Options.DefaultScopedLifestyle);
            container.Register<IGenericRepository<ManufacturingWages>, GenericRepository<ManufacturingWages>>(container.Options.DefaultScopedLifestyle);
            container.Register<IGenericRepository<CostCalculationType>, GenericRepository<CostCalculationType>>(container.Options.DefaultScopedLifestyle);
            container.Register<IGenericRepository<GoldWorldPrice>, GenericRepository<GoldWorldPrice>>(container.Options.DefaultScopedLifestyle);
            container.Register<IGenericRepository<Bank>, GenericRepository<Bank>>(container.Options.DefaultScopedLifestyle);

            container.Register<IGenericRepository<CurrencyCategories>, GenericRepository<CurrencyCategories>>(container.Options.DefaultScopedLifestyle);
            container.Register<IGenericRepository<AddressPartsSettings>, GenericRepository<AddressPartsSettings>>(container.Options.DefaultScopedLifestyle);

            container.Register<IGenericRepository<BILL_PAY_TYPES>, GenericRepository<BILL_PAY_TYPES>>(container.Options.DefaultScopedLifestyle);
            container.Register<IGenericRepository<BillCaliberTransactions>, GenericRepository<BillCaliberTransactions>>(container.Options.DefaultScopedLifestyle);

            container.Register<IGenericRepository<ResourcesTranslation>, GenericRepository<ResourcesTranslation>>(container.Options.DefaultScopedLifestyle);
            container.Register<IGenericRepository<Resources>, GenericRepository<Resources>>(container.Options.DefaultScopedLifestyle);

            container.Register<IGenericRepository<UserPriviligeBranches>, GenericRepository<UserPriviligeBranches>>(container.Options.DefaultScopedLifestyle);
            container.Register<IGenericRepository<UserForm>, GenericRepository<UserForm>>(container.Options.DefaultScopedLifestyle);
            container.Register<IGenericRepository<AssetGroup>, GenericRepository<AssetGroup>>(container.Options.DefaultScopedLifestyle);
            container.Register<IGenericRepository<AssetMaster>, GenericRepository<AssetMaster>>(container.Options.DefaultScopedLifestyle);
            container.Register<IGenericRepository<AssetDepreciationDetails>, GenericRepository<AssetDepreciationDetails>>(container.Options.DefaultScopedLifestyle);
            container.Register<IGenericRepository<AssetStatus>, GenericRepository<AssetStatus>>(container.Options.DefaultScopedLifestyle);
            container.Register<IGenericRepository<AssetType>, GenericRepository<AssetType>>(container.Options.DefaultScopedLifestyle);
            container.Register<IGenericRepository<AssetDepreciationType>, GenericRepository<AssetDepreciationType>>(container.Options.DefaultScopedLifestyle);
            container.Register<IGenericRepository<AssetLifeSpanUnit>, GenericRepository<AssetLifeSpanUnit>>(container.Options.DefaultScopedLifestyle);
            container.Register<IGenericRepository<AssetCompanyDetails>, GenericRepository<AssetCompanyDetails>>(container.Options.DefaultScopedLifestyle);
            container.Register<IGenericRepository<AssetOperationMaster>, GenericRepository<AssetOperationMaster>>(container.Options.DefaultScopedLifestyle);
            container.Register<IGenericRepository<AssetOperationDetails>, GenericRepository<AssetOperationDetails>>(container.Options.DefaultScopedLifestyle);
            container.Register<IGenericRepository<UserModule>, GenericRepository<UserModule>>(container.Options.DefaultScopedLifestyle);

            container.Register<IGenericRepository<ResourcesTranslation>, GenericRepository<ResourcesTranslation>>(container.Options.DefaultScopedLifestyle);
            container.Register<IGenericRepository<Resources>, GenericRepository<Resources>>(container.Options.DefaultScopedLifestyle);
            container.Register<IGenericRepository<UserLogFile>, GenericRepository<UserLogFile>>(container.Options.DefaultScopedLifestyle);
            container.Register<IGenericRepository<UserForm>, GenericRepository<UserForm>>(container.Options.DefaultScopedLifestyle);
            container.Register<IGenericRepository<UserOperation>, GenericRepository<UserOperation>>(container.Options.DefaultScopedLifestyle);
            container.Register<IGenericRepository<DashBoard>, GenericRepository<DashBoard>>(container.Options.DefaultScopedLifestyle);

            container.Register<IGenericRepository<HrPslEmployee>, GenericRepository<HrPslEmployee>>(container.Options.DefaultScopedLifestyle);
            container.Register<IGenericRepository<HrPslMartialStatus>, GenericRepository<HrPslMartialStatus>>(container.Options.DefaultScopedLifestyle);

            container.Register<IGenericRepository<GlBank>, GenericRepository<GlBank>>(container.Options.DefaultScopedLifestyle);
            container.Register<IGenericRepository<HrPslCity>, GenericRepository<HrPslCity>>(container.Options.DefaultScopedLifestyle);
            container.Register<IGenericRepository<HrPslReligion>, GenericRepository<HrPslReligion>>(container.Options.DefaultScopedLifestyle);
            container.Register<IGenericRepository<HrPslNationality>, GenericRepository<HrPslNationality>>(container.Options.DefaultScopedLifestyle);
            container.Register<IGenericRepository<HrPslJobTitle>, GenericRepository<HrPslJobTitle>>(container.Options.DefaultScopedLifestyle);
            container.Register<IGenericRepository<HrPslEmployeeStatus>, GenericRepository<HrPslEmployeeStatus>>(container.Options.DefaultScopedLifestyle);
            container.Register<IGenericRepository<HrPslEmployeeJobLevel>, GenericRepository<HrPslEmployeeJobLevel>>(container.Options.DefaultScopedLifestyle);
            container.Register<IGenericRepository<HrPslContactType>, GenericRepository<HrPslContactType>>(container.Options.DefaultScopedLifestyle);
            container.Register<IGenericRepository<HrPslAcademicDegree>, GenericRepository<HrPslAcademicDegree>>(container.Options.DefaultScopedLifestyle);
            container.Register<IGenericRepository<HrPslEmployeeAcademicDegree>, GenericRepository<HrPslEmployeeAcademicDegree>>(container.Options.DefaultScopedLifestyle);
            container.Register<IGenericRepository<HrPslEmployeeExperience>, GenericRepository<HrPslEmployeeExperience>>(container.Options.DefaultScopedLifestyle);
            container.Register<IGenericRepository<HrPslEmployeeFamilyDetails>, GenericRepository<HrPslEmployeeFamilyDetails>>(container.Options.DefaultScopedLifestyle);
            container.Register<IGenericRepository<HrPslEmployeeContacts>, GenericRepository<HrPslEmployeeContacts>>(container.Options.DefaultScopedLifestyle);
            container.Register<IGenericRepository<HrPslEmployeeTrainingCources>, GenericRepository<HrPslEmployeeTrainingCources>>(container.Options.DefaultScopedLifestyle);

            container.Register<IGenericRepository<AssetCompanyDetails>, GenericRepository<AssetCompanyDetails>>(container.Options.DefaultScopedLifestyle);



            ResortERP.Repository.Bootstrapper.Bootstrap(container);
            // etc
        }
    }
}
