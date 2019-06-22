'use strict';
app.controller('billProfitReportPageController', ['$scope', '$location', '$log', '$q', 'companyStoresService', 'currencyService', 'costCentersService', 'employeesService', 'customersService', 'accountService', '$document', '$uibModal', 'billProfitReportPageService', '$uibModalStack','compBranchesService', function ($scope, $location, $log, $q, companyStoresService, currencyService, costCentersService, employeesService, customersService, accountService, $document, $uibModal, profitService, $uibModalStack, compBranchesService) {

    $scope.billProfit = { dateFrom: null, dateTo: null };
    $scope.reportOptionList = [{ NAME: "عرض المخزن", ID: "1", SELECTED: false }, { NAME: "عرض البائع", ID: "2", SELECTED: false }, { NAME: "عرض الخصومات و الاضافات", ID: "3", SELECTED: false }];
    $scope.billTypesList = [{ BILL_TYPE_AR_NAME: "مبيعات ", BILL_SETTING_ID: "", BILL_TYPE_ID: "2", SELECTED: true }, { BILL_TYPE_AR_NAME: "مبيعات ضريبية", BILL_SETTING_ID: "13", BILL_TYPE_ID: "2", SELECTED: true }];

    $scope.clearEnity = function () {
        $scope.billProfit = { dateFrom: null, dateTo: null };
        $scope.reportOptionList = [{ NAME: "عرض المخزن", ID: "1", SELECTED: false }, { NAME: "عرض البائع", ID: "2", SELECTED: false }, { NAME: "عرض الخصومات و الاضافات", ID: "3", SELECTED: false }];
        $scope.billTypesList = [{ BILL_TYPE_AR_NAME: "مبيعات ", BILL_SETTING_ID: "2", BILL_TYPE_ID: "2", SELECTED: true }, { BILL_TYPE_AR_NAME: "مبيعات ضريبية", BILL_SETTING_ID: "13", BILL_TYPE_ID: "2", SELECTED: true }];
    };

    $scope.UserBranches = [];
    $scope.dropdownSetting = {
        scrollable: true,
        scrollableHeight: '300px',
        checkBoxes: true,
        enableSearch: true,
        buttonClasses: 'btn btn-default btn-block',
        styleActive: true,
        smartButtonTextProvider: function (selectedItems) {
            if (selectedItems.length > 3) {
                return "تم اختيار " + selectedItems.length + ' فروع';
            }
            else if (selectedItems.length == 3) {
                return selectedItems[0].label + ', ' + selectedItems[1].label + ', ' + selectedItems[2].label;
            }
            else if (selectedItems.length == 2) {
                return selectedItems[0].label + ', ' + selectedItems[1].label;
            }
            else if (selectedItems.length == 1) {
                return selectedItems[0].label;
            }
            else {
                return 'اختار فرع';
            }
        }
    }

    $scope.GetCustomers = function () {
        customersService.getAll().then(function (response) {
            $scope.searchCustomers = response.data;
        }, function (error) { console.log(error.data.message); });
    };

    $scope.GetAccounts = function () {
        accountService.GetAllCustomerAccounts().then(function (response) {
            $scope.accountsList = response.data;
        }, function (error) { console.log(error.data.message); });
    };

    $scope.GetCompanyStores = function () {
        companyStoresService.getAll().then(function (response) {
            $scope.companyStoresList = response.data;
        }, function (error) { console.log(error.data.message); });
    };

    $scope.GetCurrencies = function () {
        currencyService.getAll().then(function (response) {
            $scope.currencyList = response.data;
        }, function (error) { console.log(error.data.message); });
    };

    $scope.GetCostCenters = function () {
        costCentersService.getAll().then(function (response) {
            $scope.costCentersList = response.data;
        }, function (error) { console.log(error.data.message); });
    };

    $scope.GetEmployeeSales = function () {
        employeesService.getByTypeID(1).then(function (response) {
            $scope.EmpList = response.data;
        }, function (error) { console.log(error.data.message); });
    };

    $scope.getAllOnLoad = function () {
        $q.all(
            [
                $scope.getCurrentDate(),
                $scope.GetCurrencies(),
                $scope.GetUserBranches(),
                //$scope.GetCompanyStores(),
                //$scope.GetCostCenters(),
                //$scope.GetEmployeeSales(),
                //$scope.GetCustomers(),
                //$scope.GetAccounts()
            ]).then(function (allResponses) {
            }, function (error) {
            });
    }

    $scope.GetUserBranches = function () {
        compBranchesService.getUserBranches().then(function (response) {
            if (response.data != undefined && response.data.length > 0) {
                angular.forEach(response.data, function (value, index) {
                    $scope.UserBranches.push({ id: value.COM_BRN_ID, label: value.COM_BRN_AR_NAME });
                });
                $scope.SelectedBranch = [$scope.UserBranches[0]];
            }
        }, function (error) { console.log(error.data.message); });
    };

    $scope.getCurrentDate = function () {
        var date = new Date();
        $scope.billProfit.dateFrom = date;
        $scope.billProfit.dateTo = date;
    }

    $scope.getCurrencyRate = function () {
        if ($scope.billProfit.currency != "" && $scope.billProfit.currency != undefined) {
            var curID = $scope.billProfit.currency.CURRENCY_ID;
            if (curID != "" && curID != undefined)
                currencyService.getCurrencyRate(curID).then(
                    function (result) {
                        $scope.billProfit.currencyRate = result.data;
                    }, function (error) {
                        console.log(error.data.message);
                    });
            else $scope.billProfit.currencyRate = "";
        } else $scope.billProfit.currencyRate = "";
    };

    $scope.GetSearchResult = function () {
        var search = $scope.billProfit;
    };

    /*******************************************************************************************************************/
    /*Choose Customer*/
    /*******************************************************************************************************************/
    $scope.openCust = function () {
        var parentElem = angular.element($document[0].querySelector('.app-content'));
        $scope.modalOpenCust = false;

        var modalInstance = $uibModal.open({
            templateUrl: 'customerModal.html',
            controller: 'modalCtrl',
            scope: $scope,
            size: 'lg',
            preserveScope: true
        });

        modalInstance.opened.then(function () {
            $scope.modalOpenCust = true;
        });

        // we want to update state whether the modal closed or was dismissed,
        // so use finally to handle both resolved and rejected promises.
        modalInstance.result.finally(function (selectedItem) {
            $scope.modalOpenCust = false;
        });

    };

    $scope.searchForCustomer = function () {
        var search = $scope.billProfit.customerName;
        search = search == undefined || search == "" ? "" : search;
        //if (search != null && search != undefined && search != "")
        profitService.getSearchForCustomer(search).then(function (response) {
            $scope.searchCustomers = response.data;
            if ($scope.searchCustomers.length == 1) {
                $scope.billProfit.customerName = $scope.searchCustomers[0].CUST_AR_NAME;
                $scope.billProfit.accountName = $scope.searchCustomers[0].Account.ACC_AR_NAME;
                $scope.billProfit.customerID = $scope.searchCustomers[0].ACC_ID;
                $scope.billProfit.accountID = $scope.searchCustomers[0].Account.ACC_ID;
            }
            else if ($scope.searchCustomers.length == 0) {
                $scope.billProfit.customerName = "";
                $scope.billProfit.accountName = "";
                $scope.billProfit.customerID = "";
                $scope.billProfit.accountID = "";
                return;
            }
            else {
                if ($scope.modalOpenCust === false || $scope.modalOpenCust == undefined) { $scope.openCust(); }
            }
        }, function (err) {
            console.log(err.data.message);
        });
        //else {
        //    $scope.GetCustomers();
        //    if ($scope.modalOpenCust === false || $scope.modalOpenCust == undefined) { $scope.openCust(); }
        //    //$scope.billProfit.customerName = "";
        //    //$scope.billProfit.accountName = "";
        //    return;
        //}
    }

    $scope.addSelectedCustomer = function (customer) {
        $scope.billProfit.customerName = customer.CUST_AR_NAME;
        $scope.billProfit.accountName = customer.Account.ACC_AR_NAME;
        $scope.billProfit.customerID = customer.ACC_ID;
        $scope.billProfit.accountID = customer.Account.ACC_ID;
        $uibModalStack.dismissAll();
    };


    /*******************************************************************************************************************/
    /*Choose Store*/
    /*******************************************************************************************************************/
    $scope.openStore = function () {
        var parentElem = angular.element($document[0].querySelector('.app-content'));
        $scope.modalOpenStor = false;
        var modalInstance = $uibModal.open({
            templateUrl: 'storeModal.html',
            controller: 'modalCtrl',
            scope: $scope,
            size: 'md',
            preserveScope: true
        });
        modalInstance.opened.then(function () {
            $scope.modalOpenStor = true;
        });

        // we want to update state whether the modal closed or was dismissed,
        // so use finally to handle both resolved and rejected promises.
        modalInstance.result.finally(function (selectedItem) {
            $scope.modalOpenStor = false;
        });
    };

    $scope.searchForStore = function () {
        var search = $scope.billProfit.storeName;
        search = search == undefined || search == "" ? "" : search;
        //if (search != null && search != undefined && search != "")
        profitService.getSearchForStore(search).then(function (response) {
            $scope.searchStores = response.data;
            if ($scope.searchStores.length == 1) {
                $scope.billProfit.storeName = $scope.searchStores[0].COM_STORE_AR_NAME;
                $scope.billProfit.storeID = $scope.searchStores[0].COM_STORE_ID;
            }
            else if ($scope.searchStores.length == 0) {
                $scope.billProfit.storeName = "";
                $scope.billProfit.storeID = "";
                return;
            }
            else {
                if ($scope.modalOpenStor === false || $scope.modalOpenStor == undefined) { $scope.openStore(); }

            }
        }, function (err) {
            console.log(err.data.message);
        });
    }

    $scope.addSelectedStore = function (store) {
        $scope.billProfit.storeName = store.COM_STORE_AR_NAME;
        $scope.billProfit.storeID = store.COM_STORE_ID;
        $uibModalStack.dismissAll();
    };

    /*******************************************************************************************************************/
    /*Choose Employee*/
    /*******************************************************************************************************************/
    $scope.openEmp = function () {
        var parentElem = angular.element($document[0].querySelector('.app-content'));
        $scope.modalOpenEmp = false;
        var modalInstance = $uibModal.open({
            templateUrl: 'EmployeeModal.html',
            controller: 'modalCtrl',
            scope: $scope,
            size: 'md',
            preserveScope: true
        });
        modalInstance.opened.then(function () {
            $scope.modalOpenEmp = true;
        });

        // we want to update state whether the modal closed or was dismissed,
        // so use finally to handle both resolved and rejected promises.
        modalInstance.result.finally(function (selectedItem) {
            $scope.modalOpenEmp = false;
        });
    };

    $scope.searchForEmployee = function () {
        var search = $scope.billProfit.EmployeeName;
        search = search == undefined || search == "" ? "" : search;
        //if (search != null && search != undefined && search != "")
        profitService.getSearchForEmployee(search, 1).then(function (response) {
            $scope.searchEmployeess = response.data;
            if ($scope.searchEmployeess.length == 1) {
                $scope.billProfit.EmployeeName = $scope.searchEmployeess[0].EMP_AR_NAME;
                $scope.billProfit.EmployeeID = $scope.searchEmployeess[0].EMP_ID;
            }
            else if ($scope.searchEmployeess.length == 0) {
                $scope.billProfit.EmployeeName = "";
                $scope.billProfit.EmployeeID = "";
                return;
            }
            else {
                if ($scope.modalOpenEmp === false || $scope.modalOpenEmp == undefined) { $scope.openEmp(); }

            }
        }, function (err) {
            console.log(err.data.message);
        });
    }

    $scope.addSelectedEmp = function (Emp) {
        $scope.billProfit.EmployeeName = Emp.EMP_AR_NAME;
        $scope.billProfit.EmployeeID = Emp.EMP_ID;
        $uibModalStack.dismissAll();
    };

    /*******************************************************************************************************************/
    /*Choose CostCenter*/
    /*******************************************************************************************************************/
    $scope.openCostCenter = function () {
        var parentElem = angular.element($document[0].querySelector('.app-content'));
        $scope.modalOpenCostCenter = false;
        var modalInstance = $uibModal.open({
            templateUrl: 'CostCenterModal.html',
            controller: 'modalCtrl',
            scope: $scope,
            size: 'md',
            preserveScope: true
        });
        modalInstance.opened.then(function () {
            $scope.modalOpenCostCenter = true;
        });

        // we want to update state whether the modal closed or was dismissed,
        // so use finally to handle both resolved and rejected promises.
        modalInstance.result.finally(function (selectedItem) {
            $scope.modalOpenCostCenter = false;
        });
    };

    $scope.searchForCostCenter = function () {
        var search = $scope.billProfit.CostCenterName;
        search = search == undefined || search == "" ? "" : search;
        //if (search != null && search != undefined && search != "")
        profitService.getSearchForCostCenter(search).then(function (response) {
            $scope.searchCostCenters = response.data;
            if ($scope.searchCostCenters.length == 1) {
                $scope.billProfit.CostCenterName = $scope.searchCostCenters[0].COST_CENTER_AR_NAME;
                $scope.billProfit.CostCenterID = $scope.searchCostCenters[0].COST_CENTER_ID;
            }
            else if ($scope.searchCostCenters.length == 0) {
                $scope.billProfit.CostCenterName = "";
                $scope.billProfit.CostCenterID = "";
                return;
            }
            else {
                if ($scope.modalOpenCostCenter === false || $scope.modalOpenCostCenter == undefined) { $scope.openCostCenter(); }

            }
        }, function (err) {
            console.log(err.data.message);
        });
    }

    $scope.addSelectedCostCenter = function (CostCenter) {
        $scope.billProfit.CostCenterName = CostCenter.COST_CENTER_AR_NAME;
        $scope.billProfit.CostCenterID = CostCenter.COST_CENTER_ID;
        $uibModalStack.dismissAll();
    };

    
    $scope.addSelectedEmployee = function (emp) {
        $scope.billProfit.EmployeeName = emp.EMP_AR_NAME;
        $scope.billProfit.employeeID = emp.EMP_ID;
        $uibModalStack.dismissAll();
    };

    $scope.onSelect = function (item, model, label) {

        console.log(item);

        $scope.billProfit.currency = item;

        $scope.getCurrencyRate();
    };

}]);