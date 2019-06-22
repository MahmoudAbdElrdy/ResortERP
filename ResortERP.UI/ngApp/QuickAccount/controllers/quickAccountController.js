'use strict'
app.controller('quickAccountController', ['$scope', '$q', '$log', '$window', 'authService', 'accountsService', 'itemsService', '$location', function ($scope, $q, $log, $window, authService, accountsService, itemsService, $location) {

    $scope.account = {};
    $scope.parentList = [];
    $scope.finalList = [];
    $scope.accountTypesList = [];


    $scope.stateList = [{ stateId: 1, stateName: "رئيسي" },
    { stateId: 2, stateName: "فرعي" }];



    $scope.quickAccountPageLoad = function () {
        $scope.getAllOnLoad();
    }


    $scope.getAllOnLoad = function () {



        $q.all(
            [
                $scope.getParentList(),
                $scope.getParentFinalList(),
                $scope.getLastCode(),
                $scope.getAccountsType(),
                $scope.getSystemOption(),
                $scope.defaultState()

            ]).then(function (allResponses) {
                if ($scope.typeOfAccount != undefined && $scope.typeOfAccount != null) {
                    $scope.account.ACC_TYPE_ID = $scope.typeOfAccount;
                }

                var urlParams = $location.search();

                if (urlParams.foo != null && urlParams.foo != undefined && urlParams.foo != "") {
                    accountsService.getByIDTree(urlParams.foo).then(function (results) {
                        $scope.account = results.data;
                        //alert(results.data.ACC_CODE)
                        //$scope.dirEnity(results.data);
                        // $scope.account.ACC_CODE = results.data.ACC_CODE;
                    });
                }

                // $scope.clearEnity();

            }, function (error) {
                //alert("in");
                var abc = error;
                var def = 99;

            });
    }


    $scope.defaultState = function () {
        $scope.account.ACC_STATE = 2;
    }


    $scope.getParentList = function () {
        // accountsService.getAllAccounts().then(function (result) {
        accountsService.getMainAccounts().then(function (result) {
            $scope.parentList = result.data;

        });
    }

    $scope.getFinalAccByParentID = function () {
        accountsService.getParentDataByID($scope.account.PARENT_ACC_ID).then(function (result) {
            var parent = result.data;
            if (parent.FINAL_ACC_ID != null) {
                $scope.account.FINAL_ACC_ID = parent.FINAL_ACC_ID;
            }
            else {
                $scope.account.FINAL_ACC_ID = null;
            }
            $scope.account.ACC_CODE = parent.SonCode;

        })
    }

    $scope.getParentFinalList = function () {
        accountsService.getAllAccounts().then(function (result) {
            $scope.parentList = result.data;
            $scope.finalList = result.data;
        });
    }


    $scope.getLastCode = function () {
        accountsService.getLastCode().then(function (result) {
            // alert(result.data);
            $scope.account.ACC_CODE = parseInt(result.data) + 1;
        });
    }

    $scope.getAccountsType = function () {
        accountsService.getAccountTypes().then(function (result) {
            $scope.accountTypesList = result.data;
        });
    }


    $scope.getSystemOption = function () {
        var userName = authService.GetUserName();
        itemsService.getSystemOptionsByUserName(userName).then(function (response) {
            $scope.setting = response.data;
            $scope.defaultCurrency = $scope.setting.DEFAULT_CURRENCY;
        });
    }

    $scope.clearEnity = function () {
        //$scope.parentList = [];
        //$scope.finalList = [];
        //$scope.accountTypesList = [];
        $scope.account = {};
    }

    $scope.insert = function (entity) {
        return accountsService.insert(entity);
    }

    $scope.refreshData = function () {
        $scope.getAllOnLoad();
    }


    $scope.saveAccount = function () {
        // debugger;
        $scope.saveEntity();
    }

    $scope.saveEntity = function () {
        if ($scope.account !== undefined && $scope.account !== null && $scope.account.ACC_CODE !== null && $scope.account.ACC_AR_NAME !== null && $scope.account.ACC_EN_NAME !== null) {
            if ($scope.account.ACC_ID === null || $scope.account.ACC_ID === undefined || $scope.account.ACC_ID === 0) {

                if ($scope.account.ACC_TYPE_ID == 0 || $scope.account.ACC_TYPE_ID == null || $scope.account.ACC_TYPE_ID == undefined) {
                    $scope.account.ACC_TYPE_ID = 1;
                }

                if ($scope.account.ACC_CHECK_DATE == 0 || $scope.account.ACC_CHECK_DATE == null || $scope.account.ACC_CHECK_DATE == undefined) {
                    $scope.account.ACC_CHECK_DATE = new Date();
                }

                if ($scope.account.ACC_STATE == 0 || $scope.account.ACC_STATE == null || $scope.account.ACC_STATE == undefined) {
                    $scope.account.ACC_STATE = 2;
                }

                if ($scope.account.ACC_CREDIT == 0 || $scope.account.ACC_CREDIT == null || $scope.account.ACC_CREDIT == undefined) {
                    $scope.account.ACC_CREDIT = 0;
                }


                if ($scope.account.ACC_DEBIT == 0 || $scope.account.ACC_DEBIT == null || $scope.account.ACC_DEBIT == undefined) {
                    $scope.account.ACC_DEBIT = 0;
                }


                if ($scope.account.ACC_DEBIT_OR_CREDIT_OR_WITHOUT == 0 || $scope.account.ACC_DEBIT_OR_CREDIT_OR_WITHOUT == null || $scope.account.ACC_DEBIT_OR_CREDIT_OR_WITHOUT == undefined) {
                    $scope.account.ACC_DEBIT_OR_CREDIT_OR_WITHOUT = 4;
                }

                if ($scope.account.CURRENCY_ID == 0 || $scope.account.CURRENCY_ID == null || $scope.account.CURRENCY_ID == undefined) {
                    if ($scope.defaultCurrency != undefined && $scope.defaultCurrency != null) {
                        $scope.account.CURRENCY_ID = $scope.defaultCurrency;
                    }
                    else {
                        $scope.account.CURRENCY_ID = 1;
                    }
                }


                var date = new Date();

                $scope.account.ACC_CHECK_DATE = new Date(date);
                $scope.account.ACC_DATE = new Date(date);

                $scope.account.Deactivate = false;
                $scope.account.VATRate = 0;
                $scope.account.ACC_MAX_CREDIT = 0;
                $scope.account.ACC_MAX_DEBIT = 0;
                $scope.account.CreditOpeningAccount = 0;
                $scope.account.ACC_REMARKS = "";


                $scope.insert($scope.account).then(function (results) {
                    $scope.account.ACC_ID = results.data;
                    //if ($scope.account.CreditOpeningAccount != undefined || $scope.account.DepitOpeningAccount != undefined) {
                    //    $scope.saveEntry();
                    //}
                    $scope.clearEnity();
                    $scope.refreshData();
                    swal({
                        title: 'تم',
                        text: ' حفظ بيانات الحساب ',
                        type: 'success',
                        timer: 1500,
                        showConfirmButton: false
                    });
                }, function (error) {
                    console.log(error.data.message);
                    swal('عفواً',
                        'حدث خطأ عند حفظ بيانات الحساب ',
                        'error');
                });

            }
            //else {

            //    $scope.update($scope.account).then(function (results) {
            //        $scope.saveEntry();
            //        $scope.clearEnity();
            //        $scope.refreshData();
            //        swal({
            //            title: 'تم',
            //            text: '  تعديل بيانات الحساب',
            //            type: 'success',
            //            timer: 1500,
            //            showConfirmButton: false
            //        });
            //    }, function (error) {
            //        console.log(error.data.message);
            //        swal('عفواً',
            //            'حدث خطأ عند تعديل بيانات الحساب',
            //            'error');
            //    });
            //}
        }
    }


}]);