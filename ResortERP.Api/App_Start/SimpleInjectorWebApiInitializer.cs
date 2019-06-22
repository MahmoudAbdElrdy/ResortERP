[assembly: WebActivator.PostApplicationStartMethod(typeof(ResortERP.Api.App_Start.SimpleInjectorWebApiInitializer), "Initialize")]

namespace ResortERP.Api.App_Start
{
    using System.Web.Http;
    using SimpleInjector;
    using SimpleInjector.Integration.WebApi;
    using Service;
    using System.Security.Claims;
    using System.Threading;
    using System.Net.Http;
    using System;
    using System.Configuration;
    using Service.IServices;
    using Service.Services;

    public static class SimpleInjectorWebApiInitializer
    {
        /// <summary>Initialize the container and register it as Web API Dependency Resolver.</summary>
        public static void Initialize()
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new WebApiRequestLifestyle();

            InitializeContainer(container);

            container.RegisterWebApiControllers(GlobalConfiguration.Configuration);
            container.EnableHttpRequestMessageTracking(GlobalConfiguration.Configuration);
            container.Verify();

            GlobalConfiguration.Configuration.DependencyResolver =
                new SimpleInjectorWebApiDependencyResolver(container);
        }

        private static void InitializeContainer(Container container)
        {
            container.RegisterSingleton<Func<ClaimsPrincipal>>(() => { return (ClaimsPrincipal)Thread.CurrentPrincipal; });
            container.RegisterSingleton<IRequestMessageProvider>(new RequestMessageProvider(container));
            // string connectionString =getCurrentConnectionString();
            //Register your services here (remove this line).

            // For instance:
            container.Register<IUserService, UserService>(container.Options.DefaultScopedLifestyle);
            container.Register<IUnitsService, UnitsService>(container.Options.DefaultScopedLifestyle);
            container.Register<IItemsService, ItemsService>(container.Options.DefaultScopedLifestyle);
            container.Register<ICommonService, CommonService>(container.Options.DefaultScopedLifestyle);
            container.Register<IMessageService, MessageService>(container.Options.DefaultScopedLifestyle);
            container.Register<IAcountsService, AcountsService>(container.Options.DefaultScopedLifestyle);
            container.Register<IUIDViewService, UIDViewService>(container.Options.DefaultScopedLifestyle);
            container.Register<IShortcutService, ShortcutService>(container.Options.DefaultScopedLifestyle);
            container.Register<IEmployeeService, EmployeeService>(container.Options.DefaultScopedLifestyle);
            container.Register<ICurrencyService, CurrencyService>(container.Options.DefaultScopedLifestyle);
            container.Register<IBillTypesService, BillTypesService>(container.Options.DefaultScopedLifestyle);
            container.Register<ICustomersService, CustomersService>(container.Options.DefaultScopedLifestyle);
            container.Register<ITelephoneService, TelephoneService>(container.Options.DefaultScopedLifestyle);
            container.Register<IUserGroupService, UserGroupService>(container.Options.DefaultScopedLifestyle);
            container.Register<IEntryTypesService, EntryTypesService>(container.Options.DefaultScopedLifestyle);
            container.Register<IBillMasterService, BillMasterService>(container.Options.DefaultScopedLifestyle);
            container.Register<ITPAY_TYPESService, TPAY_TYPESService>(container.Options.DefaultScopedLifestyle);
            container.Register<IDepartmentService, DepartmentService>(container.Options.DefaultScopedLifestyle);
            container.Register<IBillOptionService, BillOptionService>(container.Options.DefaultScopedLifestyle);
            container.Register<IItemsGroupsService, ItemsGroupsService>(container.Options.DefaultScopedLifestyle);
            container.Register<IBillDetailsService, BillDetailsService>(container.Options.DefaultScopedLifestyle);
            container.Register<IBillEffectsService, BillEffectsService>(container.Options.DefaultScopedLifestyle);
            container.Register<ISELLS_TYPESService, SELLS_TYPESService>(container.Options.DefaultScopedLifestyle);
            container.Register<ICustomerSonService, CustomerSonService>(container.Options.DefaultScopedLifestyle);
            container.Register<ICostCentersService, CostCentersService>(container.Options.DefaultScopedLifestyle);
            container.Register<IEntryMasterService, EntryMasterService>(container.Options.DefaultScopedLifestyle);
            container.Register<INotificationService, NotificationService>(container.Options.DefaultScopedLifestyle);
            container.Register<IAcountsTypesService, AcountsTypesService>(container.Options.DefaultScopedLifestyle);
            container.Register<IBillSettingsService, BillSettingsService>(container.Options.DefaultScopedLifestyle);
            container.Register<ITelephoneCatService, TelephoneCatService>(container.Options.DefaultScopedLifestyle);
            container.Register<IEntryDetailsService, EntryDetailsService>(container.Options.DefaultScopedLifestyle);
            container.Register<IEntrySettingService, EntrySettingService>(container.Options.DefaultScopedLifestyle);
            container.Register<ITelephoneTypeService, TelephoneTypeService>(container.Options.DefaultScopedLifestyle);
            container.Register<ICompanyStoresService, CompanyStoresService>(container.Options.DefaultScopedLifestyle);
            container.Register<IEmployeeTypesService, EmployeeTypesService>(container.Options.DefaultScopedLifestyle);
            container.Register<ISystemOptionsService, SystemOptionsService>(container.Options.DefaultScopedLifestyle);
            container.Register<IAcountDetailsService, AcountsDetailsService>(container.Options.DefaultScopedLifestyle);
            container.Register<IBillShowOptionService, BillShowOptionService>(container.Options.DefaultScopedLifestyle);
            container.Register<IEntryShowOptionService, EntryShowOptionService>(container.Options.DefaultScopedLifestyle);
            container.Register<IBillGridColumnsService, BillGridColumnsService>(container.Options.DefaultScopedLifestyle);
            container.Register<ICustomerBranchesService, CustomerBranchesService>(container.Options.DefaultScopedLifestyle);
            container.Register<IEntryGridColumnsService, EntryGridColumnsService>(container.Options.DefaultScopedLifestyle);
            container.Register<INotificationTypeService, NotificationTypeService>(container.Options.DefaultScopedLifestyle);
            container.Register<IGetCustomerSupplierService, GetCustomerSupplierService>(container.Options.DefaultScopedLifestyle);
            container.Register<IRPT_ITEM_MOTION_VIEWService, RPT_ITEM_MOTION_VIEWService>(container.Options.DefaultScopedLifestyle);
            container.Register<ISmartSysDatabaseViewService, SmartSysDatabaseViewService>(container.Options.DefaultScopedLifestyle);
            container.Register<ICompanyStoresItemGroupsService, CompanyStoresItemGroupsService>(container.Options.DefaultScopedLifestyle);
            container.Register<IUserMenuService, UserMenuService>(container.Options.DefaultScopedLifestyle);
            container.Register<IUserPriviligeService, UserPriviligeService>(container.Options.DefaultScopedLifestyle);
            container.Register<IOrdersService, OrdersService>(container.Options.DefaultScopedLifestyle);
            container.Register<IEmailsService, EmailsService>(container.Options.DefaultScopedLifestyle);
            container.Register<IKestOptionService, KestOptionService>(container.Options.DefaultScopedLifestyle);
            container.Register<IIncomeAccountTypesService, IncomeAccountTypesService>(container.Options.DefaultScopedLifestyle);
            container.Register<ITBoxAccountsService, TBoxAccountsService>(container.Options.DefaultScopedLifestyle);
            container.Register<ITStoreService, TStoreService>(container.Options.DefaultScopedLifestyle);
            container.Register<ITBudgetAccountsService, TBudgetAccountsService>(container.Options.DefaultScopedLifestyle);
            container.Register<IItemCaliberService, ItemCaliberService>(container.Options.DefaultScopedLifestyle);
            container.Register<IItemStatusService, ItemStatusService>(container.Options.DefaultScopedLifestyle);
            container.Register<IManufacturingWagesService, ManufacturingWagesService>(container.Options.DefaultScopedLifestyle);

            container.Register<ICompanyBranchesService, CompanyBranchesService>(container.Options.DefaultScopedLifestyle);
            container.Register<IAddressService, AddressService>(container.Options.DefaultScopedLifestyle);
            container.Register<IGoldWorldPriceService, GoldWorldPriceService>(container.Options.DefaultScopedLifestyle);
            container.Register<ICostCalculationTypeService, CostCalculationTypeService>(container.Options.DefaultScopedLifestyle);
            container.Register<IBankService, BankService>(container.Options.DefaultScopedLifestyle);
            container.Register<ICurrencyCategoriesService, CurrencyCategoriesService>(container.Options.DefaultScopedLifestyle);
            container.Register<IAddressPartsSettingsService, AddressPartsSettingsService>(container.Options.DefaultScopedLifestyle);

            container.Register<IBillPayTypesService, BillPayTypesService>(container.Options.DefaultScopedLifestyle);
            container.Register<IBillCaliberTransactionsService, BillCaliberTransactionsService>(container.Options.DefaultScopedLifestyle);
            container.Register<IUserPriviligeBranchesService, UserPriviligeBranchesService>(container.Options.DefaultScopedLifestyle);
            container.Register<IUnit_ItemsService, Unit_ItemsService>(container.Options.DefaultScopedLifestyle);
            container.Register<IAssetCompanyDetailsService, AssetCompanyDetailsService>(container.Options.DefaultScopedLifestyle);
            container.Register<IResourcesTranslationService, ResourcesTranslationService>(container.Options.DefaultScopedLifestyle);
            container.Register<IResourcesService, ResourcesService>(container.Options.DefaultScopedLifestyle);
            container.Register<IUserLogFileService, UserLogFileService>(container.Options.DefaultScopedLifestyle);
            container.Register<IUserFormService, UserFormService>(container.Options.DefaultScopedLifestyle);
            container.Register<IDashBoardService, DashBoardService>(container.Options.DefaultScopedLifestyle);
            container.Register<IAssetGroupService, AssetGroupService>(container.Options.DefaultScopedLifestyle);
            container.Register<IAssetMasterService, AssetMasterService>(container.Options.DefaultScopedLifestyle);
            container.Register<IAssetOperationMasterService, AssetOperationMasterService>(container.Options.DefaultScopedLifestyle);
            container.Register<IReportsService, ReportsService>(container.Options.DefaultScopedLifestyle);
            container.Register<IACCOUNTS_GROUPService, ACCOUNTS_GROUPService>(container.Options.DefaultScopedLifestyle);
            Bootstrapper.Bootstrap(container);

        }

        //public static string getCurrentConnectionString()
        //{
        //    var defaultConnectionString = ConfigurationManager.ConnectionStrings["smartContext"].ToString();
        //    ClaimsPrincipal icp = Thread.CurrentPrincipal as ClaimsPrincipal;
        //    if (icp == null) return defaultConnectionString;
        //    // Access IClaimsIdentity which contains claims
        //    ClaimsIdentity claimsIdentity = (ClaimsIdentity)icp.Identity;
        //    // Access claims
        //    foreach (Claim claim in claimsIdentity.Claims)
        //    {
        //        if (claim.Type == "connectionstring")
        //        {
        //            return claim.Value;
        //        }
        //    }
        //    return defaultConnectionString;
        //}
    }

    public interface IRequestMessageProvider
    {
        HttpRequestMessage CurrentMessage { get; }
    }
    public sealed class RequestMessageProvider : IRequestMessageProvider
    {
        private readonly Container container;

        public RequestMessageProvider(Container container)
        {
            this.container = container;
            // Bootstrapper.Bootstrap(container, connectionString); 
        }

        public HttpRequestMessage CurrentMessage
        {
            get
            {
                return this.container.GetCurrentHttpRequestMessage();
            }
        }
    }

}
