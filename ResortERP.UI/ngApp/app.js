/// <reference path="D:\Projects\ResortERP\ResortERP\ResortERP.UI\Views/Menu/Menu.cshtml" />
/// <reference path="D:\Projects\ResortERP\ResortERP\ResortERP.UI\Views/Menu/Menu.cshtml" />
/// <reference path="authentication/views/authentication.html" />
/// <reference path="authentication/views/authentication.html" />
/// <reference path="home/views/home.html" />
/// <reference path="generalViews/loginStatus.html" />
var app = angular.module('app', ['ngAria', 'ngAnimate', 'ngRoute', 'LocalStorageModule', 'angular-loading-bar', 'ui.bootstrap', 'ui.bootstrap.pagination', 'xeditable', 'angularSpinner', 'angucomplete-alt', 'ngMaterial', 'blockUI', 'angularFileUpload', 'pdfjsViewer', 'ngPrint', 'base64', 'ui.grid', 'ui.grid.moveColumns', 'ui.grid.draggable-rows', 'ngDialog', 'ui.select', 'ngSanitize', 'treeControl', 'ui.tree', 'ui.tree-filter', 'angularjs-dropdown-multiselect']);
//, 'ngMaterial', 'ngAnimate'
app.run(function ($rootScope, $location, $route, authService, localStorageService, $filter) {
    var langID = authService.GetLanguageID();
    var lang = langID != null ? langID == 1 ? "ar" : "en" : "ar"
    $rootScope.title = "Home";

    $rootScope.getParameterByName = function (name) {
        results = $location.search()[name];
        return results === undefined || results === null ? "" : decodeURIComponent(results.replace(/\+/g, " "));
    };
    $rootScope.language = $rootScope.getParameterByName('lang') || lang;
    //$rootScope.language = $rootScope.getParameterByName('lang') || "ar";
    $rootScope.changeLanguage = function (language) {
        $rootScope.language = language;
    };

    $rootScope.$on('$routeChangeStart', function (scope, next, current) {
        //if ($route.current.$$route) {
            var authorizationData = authService.GetauthorizationData();
            if (authorizationData == null && $location.path() !== '/Registeration' && $location.path() !== '/activation' && $location.path() !== '/technicalSupport' && $location.path() !== '/forgetPassword') {
                $location.path('/login');
            }
            if (($location.path() == '/login' || $location.path() == '/Registeration' || $location.path() == '/activation' || $location.path() == '/technicalSupport' || $location.path() == '/forgetPassword') && authorizationData != null) {
                $location.path('/home');
            }
            if ($location.path() != '/login' && $location.path() != '/home' && next.$$route != undefined) {
                var userName = authService.GetUserName();
                if (userName.split("@").length > 1) {
                    var userPermissions = localStorageService.get("UserPermissions");
                    var userCurrentViewPermission =
                        $filter('filter')(userPermissions, { MenuUrl: $location.$$url});
                    if (userCurrentViewPermission == undefined ||
                        userCurrentViewPermission == null ||
                        userCurrentViewPermission.length == 0) {
                        swal({
                            title: 'الصلاحيات',
                            text: 'ليس لديك صلاحيات لهذه الصفحة',
                            type: 'error',
                            showConfirmButton: false
                        });
                        scope.preventDefault();
                    }
                }
            }
            //}
        $rootScope.locationPath = $location.path();
    });

    //$rootScope.$on("$routeChangeSuccess", function (currentRoute, previousRoute) {
    //    if ($route.current.$$route) {
    //        //Change page title, based on Route information
    //        //$rootScope.title = $route.current.$$route.title; //currentRoute.$$route.title;;
    //        // $rootScope.icon = $route.current.$$route.icon;
    //        //var authorizationData = localStorageService.get('authorizationData');
    //        //if (authorizationData == null && $location.path() !== '/Registeration' && $location.path() !== '/activation') {
    //        //    $location.path('/login');
    //        //}
    //        //if ($location.path() == '/login' && authorizationData != null) {
    //        //    $location.path('/home');
    //        //}
    //        var authorizationData = authService.GetauthorizationData();
    //        if (authorizationData == null && $location.path() !== '/Registeration' && $location.path() !== '/activation' && $location.path() !== '/technicalSupport' && $location.path() !== '/forgetPassword') {
    //            $location.path('/login');
    //        }
    //        if (($location.path() == '/login' || $location.path() == '/Registeration' || $location.path() == '/activation' || $location.path() == '/technicalSupport' || $location.path() == '/forgetPassword') && authorizationData != null) {
    //            $location.path('/home');
    //        }
    //    }

    //    $rootScope.locationPath = $location.path();
    //});
});

app.run(function (editableOptions) {
    editableOptions.theme = 'bs3'; // bootstrap3 theme. Can be also 'bs2', 'default'
});

app.run(['authService', 'blockUI', 'blockUIConfig', function (authService, blockUI, blockUIConfig) {
    authService.fillAuthData();
}]);


app.config(function ($httpProvider) {
    $httpProvider.interceptors.push('authInterceptorService');
});

app.config(['$compileProvider',
    function ($compileProvider) {
        $compileProvider.aHrefSanitizationWhitelist(/^\s*(http?|ftp|mailto|tel|file|blob):/);
    }]);

app.config(['$httpProvider', function ($httpProvider) {
    $httpProvider.interceptors.push(function ($q) {
        return {
            'responseError': function (rejection) {
                var defer = $q.defer();
                if (rejection.status == 401) {
                    console.dir(rejection);
                }
                if (rejection.status == 404) {
                    console.dir(rejection);
                }
                if (rejection.status == 500) {
                    console.dir(rejection);
                }
                defer.reject(rejection);
                return defer.promise;
            }
        };
    });
}]);

app.config(function (uiTreeFilterSettingsProvider) {
    uiTreeFilterSettingsProvider.addresses = ['Name'];
});
//config.headers.userLang = returnedData.data.LanguageID == 1 ? "ar" : "en";

app.config(function ($routeProvider) {

    $routeProvider.when("/home", {
        controller: "homeController",
        templateUrl: "/ngapp/home/views/home.html"
    });
    $routeProvider.when("/itemsgroups", {
        controller: "itemsGroupsController",
        templateUrl: "/ngapp/itemsGroups/views/itemsGroups.html"
    });
    $routeProvider.when("/companystores", {
        controller: "companyStoresController",
        templateUrl: "/ngapp/companystores/views/companystores.html"
    });

    $routeProvider.when("/units", {
        controller: "unitsController",
        templateUrl: "/ngapp/units/views/units.html"
    });
    $routeProvider.when("/items", {
        controller: "itemsController",
        templateUrl: "/ngapp/items/views/items.html"
    });

    $routeProvider.when("/login", {
        controller: "loginController",
        templateUrl: "/ngapp/authentication/views/login.html"
    });

    $routeProvider.when("/activation", {
        controller: "activationController",
        templateUrl: "/ngapp/activation/views/activation.html"
    });

    $routeProvider.when("/signup", {
        controller: "signupController",
        templateUrl: "/app/Auth/views/signup.html"
    });

    $routeProvider.when("/PointOfSale", {
        controller: "PointOfSaleController",
        templateUrl: "/ngapp/PointOfSale/views/PointOfSale.html"
    });

    $routeProvider.when("/entry", {
        controller: "entryController",
        templateUrl: "/ngapp/Entries/views/entry.html"
    });
    $routeProvider.when("/Registeration", {
        controller: "RegisterationController",
        templateUrl: "/ngapp/registeration/views/registeration.html"
    });

    $routeProvider.when("/innerRegister", {
        controller: "innerRegisterController",
        templateUrl: "/ngapp/innerRegister/views/innerRegister.html"
    });

    $routeProvider.when("/currency", {
        controller: "currencyController",
        templateUrl: "/ngapp/currency/views/currency.html"
    });

    $routeProvider.when("/departments", {
        controller: "departmentsController",
        templateUrl: "/ngapp/Departments/views/departments.html"
    });

    $routeProvider.when("/employees", {
        controller: "employeesController",
        templateUrl: "/ngapp/Employees/views/employees.html"
    });
    
    $routeProvider.when("/employeeTypes", {
        controller: "employeeTypesController",
        templateUrl: "/ngapp/EmployeeTypes/views/employeeTypes.html"
    });

    $routeProvider.when("/customers", {
        controller: "customerController",
        templateUrl: "/ngapp/Customers/views/customers.html"
    });

    $routeProvider.when("/currencyReport", {
        controller: "currencyReportPageController",
        templateUrl: "/ngapp/ReportPage/Currency/views/currencyReportPage.html"
    });

    $routeProvider.when("/companyStoresReport", {
        controller: "companyStoresReportPageController",
        templateUrl: "/ngapp/ReportPage/CompanyStores/views/companyStoresReportPage.html"
    }); 

    $routeProvider.when("/itemsReport", {
        controller: "itemsReportPageController",
        templateUrl: "/ngapp/ReportPage/ItemsMotion/views/ItemsReportPage.html"
    });

    $routeProvider.when("/TotalBillsReport", {
        controller: "TotalBillsMotionController",
        templateUrl: "/ngapp/ReportPage/TotalBillsMotion/views/TotalBillsMotionReport.html"
    });

    $routeProvider.when("/billProfit", {
        controller: "billProfitReportPageController",
        templateUrl: "/ngapp/ReportPage/BillProfit/views/billProfitReportPage.html"
    });

    $routeProvider.when("/billSetting", {
        controller: "billSettingController",
        templateUrl: "/ngapp/BillSettings/Views/billSetting.html"
    });

    $routeProvider.when("/entrySetting", {
        controller: "entrySettingController",
        templateUrl: "/ngapp/EntrySettings/Views/entrySetting.html"
    });

    $routeProvider.when("/SystemOptions", {
        controller: "systemOptionsController",
        templateUrl: "/ngapp/SystemOptions/Views/SystemOptions.html"
    });

    $routeProvider.when("/userPrivilages", {
        controller: "userPrivilagesController",
        templateUrl: "/ngapp/userPrivilages/views/userPrivilages.html"
    });

    $routeProvider.when("/technicalSupport", {
        controller: "technicalSupportController",
        templateUrl: "/ngapp/technicalSupport/views/technicalSupport.html"
    });

    $routeProvider.when("/changePassword", {
        controller: "changePasswordController",
        templateUrl: "/ngapp/ChangePassword/views/changePassword.html"
    });

    $routeProvider.when("/forgetPassword", {
        controller: "forgetPasswordController",
        templateUrl: "/ngapp/ForgetPassword/views/forgetPassword.html"
    });

    $routeProvider.when("/itemcaliber", {
        controller: "itemCaliberController",
        templateUrl: "/ngapp/itemCaliber/views/itemCaliber.html"
    });

    $routeProvider.when("/goldWorldPrice", {
        controller: "goldWorldPriceController",
        templateUrl: "/ngapp/goldWorldPrice/views/goldWorldPrice.html"
    });

    $routeProvider.when("/itemStatus", {
        controller: "itemStatusController",
        templateUrl: "/ngapp/itemStatus/views/itemStatus.html"
    });

    $routeProvider.when("/CostCenters", {
        controller: "costCentersController",
        templateUrl: "/ngapp/CostCenters/views/costCenters.html"
    });

    $routeProvider.when("/manufacturingWages", {
        controller: "manufacturingWagesController",
        templateUrl: "/ngapp/manufacturingWages/views/manufacturingWages.html"
    });


    $routeProvider.when("/CostCalculationType", {
        controller: "costCalculationTypeController",
        templateUrl: "/ngapp/costCalculationType/views/costCalculationType.html"
    });

    $routeProvider.when("/CompanyBranches", {
        controller: "companyBranchesController",
        templateUrl: "/ngapp/CompanyBranches/views/companyBranches.html"
    });

    $routeProvider.when("/Address", {
        controller: "addressController",
        templateUrl: "/ngapp/Address/views/address.html"
    });


    $routeProvider.when("/Banks", {
        controller: "bankController",
        templateUrl: "/ngapp/Banks/views/bank.html"
    });


    $routeProvider.when("/PayTypes", {
        controller: "payTypesController",
        templateUrl: "/ngapp/PayTypes/views/payTypes.html"
    });


    $routeProvider.when("/Accounts", {
        controller: "accountsController",
        templateUrl: "/ngapp/Accounts/views/accounts.html"
    });


    $routeProvider.when("/AccountsGroup", {
        controller: "accountsGroupController",
        templateUrl: "/ngapp/accountsGroup/views/accountsGroup.html"
    });

    $routeProvider.when("/billType", {
        controller: "billTypeController",
        templateUrl: "/ngapp/billType/views/billType.html"
    });

    $routeProvider.when("/entryType", {
        controller: "entryTypeController",
        templateUrl: "/ngapp/entryType/views/entryType.html"
    });

    $routeProvider.when("/AssetsGroup", {
        controller: "assetsGroupController",
        templateUrl: "/ngapp/AssetsGroup/views/assetsGroup.html"
    });



    $routeProvider.when("/UserPrivBrach", {
        controller: "userPriviligeBranchesController",
        templateUrl: "/ngapp/userPriviligeBranches/Views/userPriviligeBranches.html"
    });
    $routeProvider.when("/ItemsGuide", {
        controller: "itemsGuideController",
        templateUrl: "/ngapp/itemsGuide/Views/itemsGuide.html"
    });

    $routeProvider.when("/StoresGuide", {
        controller: "storesGuideController",
        templateUrl: "/ngapp/StoresGuide/Views/storesGuide.html"
    });
    $routeProvider.when("/CostCentersGuide", {
        controller: "costCentersGuideController",
        templateUrl: "/ngapp/CostCentersGuide/Views/costCentersGuide.html"
    });
    
    $routeProvider.when("/ChartOfAccount", {
        controller: "chartOfAccController",
        templateUrl: "/ngapp/chartOfAcc/Views/chartOfAcc.html"
    });

    $routeProvider.when("/AccountStatementReport", {
        controller: "accountStatementController",
        templateUrl: "/ngapp/ReportPage/AccountStatement/views/accountStatementReportPage.html"
    });

    $routeProvider.when("/AccountStatementDailyReport", {
        controller: "accountStatementDailyController",
        templateUrl: "/ngapp/ReportPage/AccountStatementDaily/views/accountStatementDaily.html"
    });

    $routeProvider.when("/AccountStatementGoldReport", {
        controller: "accountStatementGoldController",
        templateUrl: "/ngapp/ReportPage/AccountStatementGold/views/accountStatementGoldReportPage.html"
    });

    $routeProvider.when("/AccountStatementGoldDailyReport", {
        controller: "accountStatementGoldDailyController",
        templateUrl: "/ngapp/ReportPage/AccountStatementGoldDaily/views/accountStatementGoldDailyReportPage.html"
    });

    $routeProvider.when("/AccountStatementMonthlyReport", {
        controller: "accountStatementMonthlyController",
        templateUrl: "/ngapp/ReportPage/AccountStatementMonthly/views/accountStatementMonthly.html"
    });

    $routeProvider.when("/AccountStatementGoldMonthlyReport", {
        controller: "accountStatementGoldMonthlyController",
        templateUrl: "/ngapp/ReportPage/AccountStatementGoldMonthly/views/accountStatementGoldMonthlyReportPage.html"
    });

    $routeProvider.when("/TrialBalanceReport", {
        controller: "trialBalanceController",
        templateUrl: "/ngapp/ReportPage/TrialBalance/views/trialBalanceReportPage.html"
    });

    $routeProvider.when("/TrialBalanceTotalsReport", {
        controller: "trialBalanceTotalsController",
        templateUrl: "/ngapp/ReportPage/TrialBalanceTotals/views/trialBalanceTotalsReportPage.html"
    });

    $routeProvider.when("/LedgerReport", {
        controller: "generalLedgerController",
        templateUrl: "/ngapp/ReportPage/GeneralLedger/views/generalLedgerReportPage.html"
    });

    $routeProvider.when("/TrialBalanceGoldReport", {
        controller: "trialBalanceGoldController",
        templateUrl: "/ngapp/ReportPage/TrialBalanceGold/views/trialBalanceGoldReportPage.html"
    });

    $routeProvider.when("/TrialBalanceTotalsGoldReport", {
        controller: "trialBalanceTotalsGoldController",
        templateUrl: "/ngapp/ReportPage/TrialBalanceTotalsGold/views/trialBalanceTotalsGoldReportPage.html"
    });

    $routeProvider.when("/LedgerGoldReport", {
        controller: "generalLedgerGoldController",
        templateUrl: "/ngapp/ReportPage/GeneralLedgerGold/views/generalLedgerGoldReportPage.html"
    });

    $routeProvider.when("/LogFileReport", {
        controller: "logFileReportController",
        templateUrl: "/ngapp/logFileReport/views/logFileReport.html"
    });


    $routeProvider.when("/QuickAccount", {
        controller: "quickAccountController",
        templateUrl: "/ngapp/QuickAccount/views/quickAccount.html"
    }); 

    $routeProvider.when("/AccountDetectingReport", {
        controller: "AccountDetectingReportController",
        templateUrl: "/ngapp/ReportPage/AccountDetectingReport/views/AccountDetectingReport.html"
    });

    $routeProvider.when("/SalesTaxReport", {
        controller: "SalesTaxReportController",
        templateUrl: "/ngapp/ReportPage/SalesTaxReport/views/SalesTaxReport.html"
    }); 

    $routeProvider.when("/PurchasingTaxReport", {
        controller: "PurchasingTaxReportController",
        templateUrl: "/ngapp/ReportPage/PurchasingTaxReport/views/PurchasingTaxReport.html"
    });

    $routeProvider.when("/DashBoard", {
        controller: "dashBoardController",
        templateUrl: "/ngapp/DashBoard/views/dashBoard.html"
    });
    $routeProvider.when("/PurchasingTaxReport", {
        controller: "PurchasingTaxReportController",
        templateUrl: "/ngapp/ReportPage/PurchasingTaxReport/views/TestBankReportPage.html"
    });

    //*************************
    $routeProvider.when("/RiyalFundMotionReport", {
        controller: "RiyalFundMotionController",
        templateUrl: "/ngapp/ReportPage/RiyalFundMotion/views/RiyalFundMotionReportPage.html"
    });

    $routeProvider.when("/RiyalFundMotionDailyReport", {
        controller: "RiyalFundMotionDailyController",
        templateUrl: "/ngapp/ReportPage/RiyalFundMotionDaily/views/RiyalFundMotionDailyReportPage.html"
    });

    $routeProvider.when("/RiyalFundMotionMonthlyReport", {
        controller: "RiyalFundMotionMonthlyController",
        templateUrl: "/ngapp/ReportPage/RiyalFundMotionMonthly/views/RiyalFundMotionMonthlyReportPage.html"
    });

    $routeProvider.when("/GoldBoxMotionReport", {
        controller: "GoldBoxMotionController",
        templateUrl: "/ngapp/ReportPage/GoldBoxMotion/views/GoldBoxMotionReportPage.html"
    });

    $routeProvider.when("/GoldBoxMotionDailyReport", {
        controller: "GoldBoxMotionDailyController",
        templateUrl: "/ngapp/ReportPage/GoldBoxMotionDaily/views/GoldBoxMotionDailyReportPage.html"
    });

    $routeProvider.when("/GoldBoxMotionMonthlyReport", {
        controller: "GoldBoxMotionMonthlyController",
        templateUrl: "/ngapp/ReportPage/GoldBoxMotionMonthly/views/GoldBoxMotionMonthlyReportPage.html"
    });

    $routeProvider.when("/BrokenGoldBoxMotionReport", {
        controller: "BrokenGoldBoxMotionController",
        templateUrl: "/ngapp/ReportPage/BrokenGoldBoxMotion/views/BrokenGoldBoxMotionReportPage.html"
    });

    $routeProvider.when("/BrokenGoldBoxMotionDailyReport", {
        controller: "BrokenGoldBoxMotionDailyController",
        templateUrl: "/ngapp/ReportPage/BrokenGoldBoxMotionDaily/views/BrokenGoldBoxMotionDailyReportPage.html"
    });

    $routeProvider.when("/BrokenGoldBoxMotionMonthlyReport", {
        controller: "BrokenGoldBoxMotionMonthlyController",
        templateUrl: "/ngapp/ReportPage/BrokenGoldBoxMotionMonthly/views/BrokenGoldBoxMotionMonthlyReportPage.html"
    });

    //////////
    $routeProvider.when("/ExpensesTaxReport", {
        controller: "ExpensesTaxController",
        templateUrl: "/ngapp/ReportPage/ExpensesTax/views/ExpensesTaxReportPage.html"
    });

    $routeProvider.when("/ExpensesTaxDailyReport", {
        controller: "ExpensesTaxDailyController",
        templateUrl: "/ngapp/ReportPage/ExpensesTaxDaily/views/ExpensesTaxDailyReportPage.html"
    });

    $routeProvider.when("/ExpensesTaxMonthlyReport", {
        controller: "ExpensesTaxMonthlyController",
        templateUrl: "/ngapp/ReportPage/ExpensesTaxMonthly/views/ExpensesTaxMonthlyReportPage.html"
    });

    $routeProvider.when("/AssetMaster", {
        controller: "AssetMasterController",
        templateUrl: "/ngapp/AssetMaster/views/AssetMaster.html"
    });

    $routeProvider.when("/AssetOperationMaster", {
        controller: "AssetOperationMasterController",
        templateUrl: "/ngapp/AssetOperationMaster/views/AssetOperationMaster.html"
    });

    $routeProvider.when("/AssetLocation", {
        controller: "AssetLocationController",
        templateUrl: "/ngapp/AssetLocation/views/AssetLocation.html"
    });

    $routeProvider.when("/AssignAssetToEmployee", {
        controller: "AssignAssetToEmployeeController",
        templateUrl: "/ngapp/AssignAssetToEmployee/views/AssignAssetToEmployee.html"
    });
    
    $routeProvider.when("/ReceivingAssetFromEmployee", {
        controller: "ReceivingAssetFromEmployeeController",
        templateUrl: "/ngapp/ReceivingAssetFromEmployee/views/ReceivingAssetFromEmployee.html"
    });
    $routeProvider.when("/AssetCompanyDetails", {
        controller: "assetCompanyDetailsController",
        templateUrl: "/ngapp/AssetCompanyDetails/views/assetCompanyDetails.html"
    });

    $routeProvider.when("/ItemsInventoryReport", {
        controller: "ItemsInventoryController",
        templateUrl: "/ngapp/ReportPage/ItemsInventory/views/ItemsInventory.html"
    });

    $routeProvider.when("/AccountBalances", {
        controller: "AccountBalancesController",
        templateUrl: "/ngapp/ReportPage/AccountBalances/views/AccountBalancesReportPage.html"
    });

    $routeProvider.when("/BillPosting", {
        controller: "BillPostingController",
        templateUrl: "/ngapp/BillPosting/views/BillPosting.html"
    });

    $routeProvider.when("/EntryPosting", {
        controller: "EntryPostingController",
        templateUrl: "/ngapp/EntryPosting/views/EntryPosting.html"
    });

    $routeProvider.otherwise({ redirectTo: "/home" });
});

//app.config(function ($sceDelegateProvider) {
//    $sceDelegateProvider.resourceUrlWhitelist(['**']);
//});

app.directive('loginStatus', function ($rootScope, $compile) {
    return {
        restrict: 'E',
        scope: false,
        templateUrl: function (scope, element) {
            var tempurl = '../../NgApp/generalViews/Views/' + $rootScope.language + '/loginStatus.html';
            $rootScope.$watch(function () { return '../../NgApp/generalViews/Views/' + $rootScope.language + '/loginStatus.html'; }
                , function (newVal, oldVal) {
                    if (newVal && newVal !== oldVal) {
                        element.$$element.html(newVal);
                        $compile(element.$$element)(scope);
                    }
                });
            return tempurl;
        }
    }
}).directive('customMenu', function ($rootScope, $compile) {

    return {
        restrict: 'E',
        scope: false,
        templateUrl: function (scope, element) {
            var tempurl = '../../NgApp/generalViews/Views/' + $rootScope.language + '/menu.html';
            $rootScope.$watch(function () { return '../../NgApp/generalViews/Views/' + $rootScope.language + '/menu.html'; }
                , function (newVal, oldVal) {
                    if (newVal && newVal !== oldVal) {
                        element.$$element.html(newVal);
                        $compile(element.$$element)(scope);
                    }
                });
            return tempurl;
        }
    }

}).directive('customHeader', function ($rootScope, $compile) {
    return {
        restrict: 'E',
        scope: false,
        templateUrl: function (scope, element) {
            var tempurl = '../../NgApp/generalViews/Views/' + $rootScope.language + '/header.html';
            $rootScope.$watch(function () { return '../../NgApp/generalViews/Views/' + $rootScope.language + '/header.html'; }
                , function (newVal, oldVal) {
                    if (newVal && newVal !== oldVal) {
                        element.$$element.html(newVal);
                        $compile(element.$$element)(scope);
                    }
                });
            return tempurl;
        }
    }
}).directive('customHorizontal', function ($rootScope, $compile) {
    return {
        restrict: 'E',
        scope: false,
        templateUrl: function (scope, element) {
            var tempurl = '../../NgApp/generalViews/Views/' + $rootScope.language + '/horizontal.html';
            $rootScope.$watch(function () { return '../../NgApp/generalViews/Views/' + $rootScope.language + '/horizontal.html'; }
                , function (newVal, oldVal) {
                    if (newVal && newVal !== oldVal) {
                        element.$$element.html(newVal);
                        $compile(element.$$element)(scope);
                    }
                });
            return tempurl;
        }
    }
});



//app.directive('loginStatus', function () {
//    return {
//        templateUrl: '/ngapp/generalViews/loginStatus.html'
//    };
//}).directive('customMenu', function () {
//    return {
//        controller: "MenuController",
//        templateUrl: '/ngapp/generalViews/menu.html'
//    };
//})
//.directive('customHeader', function ($compile) {
//    return {
//        controller: "headerController",
//        templateUrl: '/ngapp/generalViews/header.html'
//    };

//})
//.directive('customHorizontal', function () {
//    return {
//        templateUrl: '/ngapp/generalViews/horizontal.html'
//    };
//});

//app.factory("myfact", function () {
//    var saveData = {};
//    function set(data) {
//        saveData = data;
//    }
//    function get() {
//        return saveData;
//    }
//})


