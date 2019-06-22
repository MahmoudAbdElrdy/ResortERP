'use strict';
app.controller('accountsController', ['$scope', '$rootScope', '$location', '$log', '$q', 'accountsService', 'dateFilter', '$filter', 'commonService', 'currencyService', '$base64', 'entryService', 'compBranchesService', 'itemsService', 'authService', 'localStorageService', 'accountsGroupService', function ($scope, $rootScope, $location, $log, $q, accountsService, dateFilter, $filter, commonService, currencyService, $base64, entryService, compBranchesService, itemsService, authService, localStorageService, accountsGroupService)
{


    $scope.accountsList = [];
    $scope.account = {};
    $scope.currencyList = [];
    $scope.accountTypesList = [];

    $scope.accountsTotalCount = 0;
    $scope.pageNum = 1;
    $scope.pageSize = 10;
    $scope.pagesCount = 0;
    $scope.maxSize = 5;
    $scope.searchBy = "";

    $scope.parentList = [];
    $scope.finalList = [];
    $scope.branchesList = [];
    $scope.taxAccountList = [];
    $scope.selectedID = 0;
    $scope.allAccount = [];

    $scope.wariningList = [{ warningId: 1, warningName: "Credit" },
    { warningId: 2, warningName: "Depit" },
    { warningId: 3, warningName: "Without" },
    { warningId: 4, warningName: "Both" }
    ];

    $scope.stateList = [{ stateId: 1, stateName: "رئيسي" },
    { stateId: 2, stateName: "فرعي" }];

    $scope.moduleResourse = {};
    $scope.inputReuired = {};
    $scope.displayORNot = {};
    $scope.inputDataType = {};
    $scope.requiredValidation = { LoadResource: false };

    $scope.names = ["john", "bill", "charlie", "robert", "alban", "oscar", "marie", "celine", "brad", "drew", "rebecca", "michel", "francis", "jean", "paul", "pierre", "nicolas", "alfred", "gerard", "louis", "albert", "edouard", "benoit", "guillaume", "nicolas", "joseph"];
    ///////////////////////////////////////////////////////////////////////////accounts

    $scope.get = function (pageNum, pageSize) {
        return accountsService.get(pageNum, pageSize);
    }

    $scope.count = function () {
        return accountsService.count();
    }

    $scope.update = function (entity) {
        return accountsService.update(entity);
    }
    $scope.delete = function (entity) {
        return accountsService.delete(entity);
    }

    $scope.insert = function (entity) {
        return accountsService.insert(entity);
    }

    $scope.getAccountsList = function () {

        $scope.get($scope.pageNum, $scope.pageSize).then(function (results) {
            var data = results.data;
            $scope.accountsList = data;
        }, function (error) {
            console.log(error.data.message);
        });
    }



    $scope.getAllMainItemGroupsList = function () {
        accountsGroupService.getAllMainItemGroups().then(function (response) {
            $scope.mainaccountsGroupsList = response.data;
        })
    }
    $scope.getAllOnLoadA = function () {

        $q.all(
            [
                $scope.getAccountsList(),
                $scope.getaccountsTotalCount(),
                $scope.getParentList(),
                $scope.getFinalList(),
                $scope.getcurrencyList(),
                $scope.getAccountsType(),
                //  $scope.getLastCode(),
                $scope.getAllMainItemGroupsList(),
                $scope.getCodeWithoutParent(),
                $scope.getCurrentDate(),
                $scope.getEntryNumber(),
                $scope.getCompanyBranches(),
                $scope.getSystemOption(),
                $scope.getTaxAccountsList(),
                $scope.getAllAccounts()

            ]).then(function (allResponses) {
                $scope.clearEnity();
                var urlParams = $location.search();

                if (urlParams.foo != null && urlParams.foo != undefined && urlParams.foo != "") {
                    accountsService.getByIDTree(urlParams.foo).then(function (results) {
                        $scope.account = results.data;
                        //alert(results.data.ACC_CODE)
                        $scope.dirEnity(results.data);
                        // $scope.account.ACC_CODE = results.data.ACC_CODE;
                    });
                }
            }, function (error) {
                //alert("in");
                var abc = error;
                var def = 99;

            });

    }


    $scope.clearEnity = function () {
        $scope.account = {};
        //$scope.accountsList = [];
        $scope.currencyList = [];
        $scope.accountTypesList = [];
        $scope.parentList = [];
        $scope.finalList = [];
        $scope.wariningList = [{ warningId: 1, warningName: "Credit" },
        { warningId: 2, warningName: "Depit" },
        { warningId: 3, warningName: "Without" },
        { warningId: 4, warningName: "Both" }
        ];

        $scope.stateList = [{ stateId: 1, stateName: "رئيسي" },
        { stateId: 2, stateName: "فرعي" }];

        // $scope.getLastCode();

        var urlParams = $location.search();

        $scope.getCodeWithoutParent();
        $scope.getParentList();
        $scope.getFinalList();
        $scope.getCurrentDate();
        $scope.getcurrencyList();

    }


    $scope.refreshData = function () {
        $scope.getAllOnLoadA();
    }


    $scope.accountsPageLoad = function () {
        $scope.getAllOnLoadA();
    }


    $scope.pageChanged = function () {
        $scope.getAllOnLoadA();
        $log.log('Page changed to: ' + $scope.pageNum);
    };


    $scope.getPagesCount = function () {
        var div = Math.floor($scope.accountsTotalCount / $scope.pageSize);
        var rem = $scope.accountsTotalCount % $scope.pageSize;
        if (rem > 0) {
            div = div + 1;
        }
        return div;
    }



    $scope.getaccountsTotalCount = function () {
        $scope.type = $location.search().type;
        $scope.count($scope.type).then(function (results) {
            var data = results.data;
            $scope.accountsTotalCount = data;
            $scope.pagesCount = $scope.getPagesCount();
        }, function (error) {
            console.log(error.data.message);
        });
    }

    $scope.getParentFinalList = function () {
        accountsService.getAllAccounts().then(function (result) {
            $scope.parentList = result.data;
            $scope.finalList = result.data;
        });
    }


    $scope.getParentList = function () {
        // accountsService.getAllAccounts().then(function (result) {
        accountsService.getMainAccounts().then(function (result) {
            $scope.parentList = result.data;
            var urlParams = $location.search();
            if (urlParams.new != null && urlParams.new != undefined && urlParams.new != "") {
                $scope.account.PARENT_ACC_ID = parseInt(urlParams.new);
                $scope.getFinalAccByParentID();

                $.each($scope.parentList, function (index, value) {
                    if ($scope.account.PARENT_ACC_ID == value.ACC_ID) {
                        $.each($scope.wariningList, function (index1, value1) {
                            if (value1.warningId == value.ACC_DEBIT_OR_CREDIT_OR_WITHOUT) {

                                $scope.account.ACC_DEBIT_OR_CREDIT_OR_WITHOUT = value1.warningId;
                                //alert(value1.warningId);
                            }
                        })

                    }

                })
            }
        });
    }

    $scope.getFinalList = function () {
        accountsService.getAllAccounts().then(function (result) {
            $scope.finalList = result.data;
        });
    }


    $scope.getcurrencyList = function () {
        currencyService.getAll().then(function (result) {
            $scope.currencyList = result.data;
        });
    }


    $scope.getAccountsType = function () {
        accountsService.getAccountTypes().then(function (result) {
            $scope.accountTypesList = result.data;
        });
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



                if (localStorageService.get('branchId') != null) {
                    $scope.account.Company_Branch_ID = localStorageService.get('branchId').branchId;
                    // $scope.COM_BRN_IDForFilter = localStorageService.get('branchId').branchId;
                }



                $scope.insert($scope.account).then(function (results) {
                    $scope.account.ACC_ID = results.data;
                    if ($scope.account.CreditOpeningAccount != undefined || $scope.account.DepitOpeningAccount != undefined) {
                        $scope.saveEntry();
                    }
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
            else {

                if (localStorageService.get('branchId') != null) {
                    $scope.account.Company_Branch_ID = localStorageService.get('branchId').branchId;
                    // $scope.COM_BRN_IDForFilter = localStorageService.get('branchId').branchId;
                }

                $scope.update($scope.account).then(function (results) {
                    $scope.saveEntry();
                    $scope.clearEnity();
                    $scope.refreshData();
                    swal({
                        title: 'تم',
                        text: '  تعديل بيانات الحساب',
                        type: 'success',
                        timer: 1500,
                        showConfirmButton: false
                    });
                }, function (error) {
                    console.log(error.data.message);
                    swal('عفواً',
                        'حدث خطأ عند تعديل بيانات الحساب',
                        'error');
                });
            }
        }
    }

    $scope.getCurrentDate = function () {

        var date = new Date();
        //$scope.account.ACC_CHECK_DATE = date;
        //$scope.account.ACC_DATE = date;

        $scope.account.ACC_CHECK_DATE = new Date(date);
        $scope.account.ACC_DATE = new Date(date);
    }
    /////////////////////////////////////////////last code ////////////////////////////////

    $scope.getLastCode = function () {
        accountsService.getLastCode().then(function (result) {
            // alert(result.data);
            $scope.account.ACC_CODE = parseInt(result.data) + 1;
        });
    }



    $scope.getCodeWithoutParent = function () {
        var urlParams = $location.search();
        if (urlParams.foo != null && urlParams.foo != undefined && urlParams.foo != "") {
        } else {
            accountsService.getCodeWithoutParent().then(function (result) {
                // alert(result.data);
                $scope.account.ACC_CODE = parseInt(result.data) + 1;
            });
        }

    }

    //////////////////////////////////////////////////////////////////////////////////
    $scope.dirEnity = function (entity) {

        entity.ACC_DATE = new Date(entity.ACC_DATE);
        entity.ACC_CHECK_DATE = new Date(entity.ACC_CHECK_DATE);
        $scope.account = entity;
        entryService.getEntryByaccountID(entity.ACC_ID).then(function (result) {
            $scope.accountEntryMaster = result.data;
        });
    }

    $scope.deleteEnity = function (entity) {
        swal({
            title: 'هل تريد حذف الحساب ؟ ',
            text: "لن تستطيع عكس عملية الحذف مرة أخري",
            type: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'نعم',
            cancelButtonText: 'الغاء',
            confirmButtonClass: 'btn btn-success btn-lg',
            cancelButtonClass: 'btn btn-danger btn-lg',
            buttonsStyling: false
        }).then(function () {

            $scope.checkAccountIfUsed(entity.ACC_ID).then(function (result) {

                if (result != undefined && result != null) {
                    var message = result.data;
                    if (message != "" && message != undefined) {
                        swal({
                            title: 'خطأ',
                            text: message,
                            type: 'warning',
                            timer: 1500,
                            //showCancelButton: true,
                            //cancelButtonText: 'موافق',
                            //showConfirmButton: true
                        });
                    }
                    else {


                        $scope.delete(entity).then(function (results) {
                            $scope.clearEnity();
                            $scope.refreshData();
                            swal({
                                title: 'تم',
                                text: 'الحذف بنجاح',
                                type: 'success',
                                timer: 1500,
                                showConfirmButton: false
                            });
                        }, function (error) {
                            console.log(error.data.message);
                        });
                    }
                }
            })

        }, function (dismiss) {

            if (dismiss === 'cancel') {
                swal({
                    title: 'تم',
                    text: 'الغاء عملية الحذف',
                    type: 'error',
                    timer: 1500,
                    showConfirmButton: false
                });
            }
        })
    }


    $scope.$on("myEvent", function () {
        alert("nada");
    });


    $scope.getcall = function ($scope) {
        $rootScope.$on("CallParentMethod", function () {
            $scope.parentmethod();
        });

        $scope.parentmethod = function () {
            alert("x");
        }
    }



    $scope.checkAccountIfUsed = function (accountID) {
        return accountsService.checkAccountIfUsed(accountID);
    }




    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //////////////////////////////save account entry ////////////////////////////////////////////////////
    $scope.saveEntry = function () {

        ////////////get total credit and depit for entry master


        $scope.entryMaster = {};
        $scope.entryMaster.ENTRY_SETTING_ID = 79;
        $scope.entryMaster.ENTRY_DATE = $scope.account.ACC_DATE;
        $scope.entryMaster.ENTRY_CREDIT = 0;
        $scope.entryMaster.ENTRY_DEBIT = 0;
        $scope.entryMaster.ACC_ID = 52;
        $scope.entryMaster.CURRENCY_ID = $scope.account.CURRENCY_ID;
        $scope.entryMaster.CURRENCY_RATE = $scope.CURRENCY_RATE;
        $scope.entryMaster.ENTRY_NUMBER = $scope.ENTRY_NUMBER;
        $scope.entryMaster.OpeningAccID = $scope.account.ACC_ID;
        $scope.entryMaster.IsLog = false;


        ////////fill entry details
        $scope.entryDetails = [];


        if ($scope.accountEntryMaster != undefined && $scope.accountEntryMaster != null) {
            $scope.entryMaster.ENTRY_ID = $scope.accountEntryMaster.ENTRY_ID;
        }

        // for (var i = 0; i < $scope.billMaster.accounts.length; i++) {
        $scope.entryDetObj = {};
        $scope.entryDetObj.ENTRY_ROW_NUMBER = 0;

        $scope.entryDetObj.ACC_ID = $scope.account.ACC_ID;
        $scope.entryDetObj.ENTRY_CREDIT = $scope.account.CreditOpeningAccount;
        $scope.entryDetObj.ENTRY_DEBIT = $scope.account.DepitOpeningAccount;

        //  $scope.entryDetObj.COST_CENTER_ID = $scope.billMaster.CostCenterID;
        $scope.entryDetails.push($scope.entryDetObj);


        var entryMasterDetails = { EntryMaster: $scope.entryMaster, EntryDetails: $scope.entryDetails };
        entryService.addEntryMasterDetails(entryMasterDetails).then(function (result) {
            var id = result.data;

            //$scope.billMaster.BILL_ID = $scope.billMasterID;
            //PointOfSaleService.updateEntryID($scope.billMaster, id).then(function (response) {
            //    var x = response.data;
            //});
        });

    }


    $scope.getEntryNumber = function () {
        entryService.GetLastEntryNumber(79).then(function (response) {
            var result = response.data;
            $scope.ENTRY_NUMBER = result + 1;
            //return $scope.ENTRY_NUMBER;
        })
    }






    $scope.getCurrencyRate = function () {
        var currencyID = $scope.account.CURRENCY_ID;
        if ($scope.currencyList != undefined || $scope.currencyList != null) {
            for (var i = 0; i < $scope.currencyList.length; i++) {
                if ($scope.currencyList[i].CURRENCY_ID == currencyID) {
                    $scope.CURRENCY_RATE = $scope.currencyList[i].CURRENCY_RATE;
                }
            }
        }

    }



    $scope.getFinalAccByParentID = function () {
        accountsService.getParentDataByID($scope.account.PARENT_ACC_ID).then(function (result) {
            var parent = result.data;
            if (parent.GROUP_ID != null) {
                $scope.account.GROUP_ID = parent.GROUP_ID;
            } else {
                $scope.account.GROUP_ID = null;
            }
            if (parent.FINAL_ACC_ID != null) {
                $scope.account.FINAL_ACC_ID = parent.FINAL_ACC_ID;
            }
            else {
                $scope.account.FINAL_ACC_ID = null;
            }
            $scope.account.ACC_CODE = parent.SonCode;

        })
    }

    $scope.getCompanyBranches = function () {

        compBranchesService.getMainCompanyBranches().then(function (result) {
            var branches = result.data;
            $scope.branchesList = branches;
        });
    }

    $scope.fnExcelReport = function () {

        accountsService.getAllAccounts().then(function (response) {
            commonService.getExcelFile("sample-table-2", "Sheet 1", "Accounts", response.data);
        })

    }


    $scope.getSystemOption = function () {
        var userName = authService.GetUserName();
        itemsService.getSystemOptionsByUserName(userName).then(function (response) {
            $scope.setting = response.data;
            $scope.defaultCurrency = $scope.setting.DEFAULT_CURRENCY;
        });
    }


    $scope.getTaxAccountsList = function () {
        accountsService.getAccountBoxByTypesForEntry(0).then(function (result) {
            $scope.taxAccountList = result.data;
        });
    }



    //function to search of account
    $scope.searchForAccounts = function () {

        if ($scope.search != null && $scope.search != undefined && $scope.search != "") {
            $scope.searchBy = $scope.search
        }

        if ($scope.searchBy == null || $scope.searchBy == undefined || $scope.searchBy == "") {
            // if ($scope.searchCode != null && $scope.searchCode != undefined)
            return;
        } else {
            //search for Item 
            // entryService.searchAccounts($scope.searchBy).then(function (response) {
            searchAccounts($scope.searchBy).then(function (response) {
                $scope.searchItems = response.data;
                if ($scope.searchItems.length == 1) {

                    $scope.checkExist($scope.searchItems[0].ACC_CODE, $scope.searchItems[0]);

                } else if ($scope.searchItems.length == 0) {
                    $scope.searchItems = [];
                } else {
                    $scope.checkSelected();
                    if ($scope.modalOpenItm === false || $scope.modalOpenItm == undefined) { $scope.open(); }
                }
            })
        }
    }

    $scope.getAllAccounts = function () {
        accountsService.getAll($scope.selectedID).then(function (result) {
            $scope.allAccount = result.data;
        })
    }


    $scope.$watch('selectedAcc', function (newx, old) {
         $scope.selectedID = newx.originalObject.ACC_ID;
         if ($scope.selectedID != null && $scope.selectedID != undefined) {
             accountsService.getByID($scope.selectedID).then(function (result) {
                $scope.account = result.data;
            })
        }
    });


    $scope.deleteAccount = function () {
        if ($scope.selectedID != null && $scope.selectedID != undefined && $scope.selectedID != 0)
        {        
            $scope.deleteEnity($scope.account);
        }
    }


}]);