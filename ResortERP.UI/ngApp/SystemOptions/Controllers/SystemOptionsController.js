'use strict';
app.controller('systemOptionsController', ['$scope', '$location', '$log', '$q', 'systemOptionsService', '$document', '$uibModal', '$uibModalStack', 'uiGridConstants', 'currencyService', 'accountService', 'commonService', 'ordersService', 'authService', 'shortcutService', 'emailsService', 'salesTypeService', 'KestOptionService', 'incomeAccountTypesService', 'tBoxAccountsService', 'tStoreService', 'companyStoresService', 'tBudgetAccountsService', 'addressPartsService', 'userPrivilagesService', 'userPriviligeBranchesService', 'compBranchesService', '$window', function ($scope, $location, $log, $q, systemOptionsService, $document, $uibModal, $uibModalStack, uiGridConstants, currencyService, accountService, commonService, ordersService, authService, shortcutService, emailsService, salesTypeService, KestOptionService, incomeAccountTypesService, tBoxAccountsService, tStoreService, companyStoresService, tBudgetAccountsService, addressPartsService,userPrivilagesService ,userPriviligeBranchesService, compBranchesService,$window) {

    $scope.Custom = {
        Lost_ACC: null, Curr_Diff_Acc: null, Cust_ACC_Name: null, PBill_Acc_Name: null, Supp_Acc_NAme: null, BillProfit_Acc_Name: null, Income_Acc_ID: null, Income_Acc_Name: null, AccountTypeID: null, TBoxAccount: null, TBoxAccountID: null, TStore: null, TStoreID: null
    };

    $scope.systemOptions = {};
   
    $scope.priceType = {
        Price1: null, Price2: null, Price3: null, Price4: null, Price5: null, Price6: null, Price7: null,
        IsShowPrice1: false, IsShowPrice2: false, IsShowPrice3: false, IsShowPrice4: false, IsShowPrice5: false, IsShowPrice6: false, IsShowPrice7: false
    };


    $scope.addressParts = { AddressPartID: null, AddressPart1: "", AddressPart2: "", AddressPart3: "", AddressPart4: "", UID: "" }

    $scope.systemOptionsList = [];
    $scope.TBoxAccountsList = [];
    $scope.TStoresList = [];
    $scope.emailsList = [];
    $scope.IncomeAccountsList = [];
    $scope.KestOptList = [];
    $scope.shortcutList = [];

    $scope.systemOptionsTotalCount = 0;
    $scope.pageNum = 1;
    $scope.pageSize = 10;
    $scope.pagesCount = 0;
    $scope.maxSize = 5;

    $scope.getPagesCount = function () {
        var div = Math.floor($scope.systemOptionsTotalCount / $scope.pageSize);
        var rem = $scope.systemOptionsTotalCount % $scope.pageSize;
        if (rem > 0) {
            div = div + 1;
        }
        return div;
    }

    $scope.refreshData = function () {
        $scope.getAllOnLoad();
    }
    $scope.savesystemOptions = function () {
        $scope.saveEntityAll();
    }

    //filter Selected currencyList from BILLcurrencyList during update
    $scope.getCurrencyBasedOnCurrencyID = function (CURRENCYID) {
        for (var i = 0; i < $scope.CurrencyList.length; i++) {
            var curr = $scope.CurrencyList[i].CURRENCY_ID
            if (curr == CURRENCYID) {
                return $scope.CurrencyList[i];
            }
        }
    }

    // Initialization
    $scope.onKeyPressResult = "";

    // Utility functions

    var getKeyboardEventResult = function (keyEvent, keyEventDesc) {
        return keyEventDesc + " (keyCode: " + (window.event ? keyEvent.keyCode : keyEvent.which) + ")";
    };

    // Event handlers
    $scope.onKeyPress = function ($event) {
        $scope.onKeyPressResult = getKeyboardEventResult($event, "Key press");
    };

    $scope.emailPattern = /^([a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]){1,70}$/;
    $scope.AddToEmailList = function () {
        var userID = authService.GetUserID();
        $scope.emailsList.push({ Email: $scope.systemOptions.email, UID_ID: userID });
        $scope.systemOptions.email = "";
    };

    $scope.DeleteEmailItem = function (index) {
        $scope.emailsList.splice(index, 1);
    };

    $scope.GetEmailList = function () {
        var userID = authService.GetUserID();
        emailsService.getByUID(userID).then(function (response) {
            $scope.emailsList = response.data;
        }, function (error) {
            console.log(error.data.message);
        });
    };

    var GetAccountTypeNameByAccTypeID = function (AccTypeID) {
        for (var i = 0; i < $scope.AccountTypesList.length; i++) {
            if ($scope.AccountTypesList[i].ID == AccTypeID) {
                return $scope.AccountTypesList[i].NameAr;
            }
        }
    }

    $scope.AddToIncomeAccountsList = function () {
        var accTypeName = GetAccountTypeNameByAccTypeID($scope.Custom.AccountTypeID);
        var userName = authService.GetUserName();
        $scope.IncomeAccountsList.push({ UID: userName, ACC_ID: $scope.Custom.Income_Acc_ID, Income_Acc_Name: $scope.Custom.Income_Acc_Name, BOXACC_TYPE: $scope.Custom.AccountTypeID, Income_Account_Type_Name: accTypeName });
        $scope.Custom.Income_Acc_ID = null;
        $scope.Custom.Income_Acc_Name = null;
        $scope.Custom.AccountTypeID = null;
        $scope.Custom.AccountTypeName = null;
    };

    $scope.DeletefromIncomeAccountsList = function (index) {
        $scope.IncomeAccountsList.splice(index, 1);
    };


    $scope.AddToTStoresList = function () {
        var userName = authService.GetUserName();
        $scope.TStoresList.push({ STORE_ID: $scope.Custom.TStoreID, UID: userName, StoreName: $scope.Custom.TStore });
        $scope.Custom.TStore = null;
        $scope.Custom.TStoreID = null;
    };

    $scope.DeletefromTStoresList = function (index) {
        $scope.TStoresList.splice(index, 1);
    };

    $scope.AddToTBoxAccountsList = function () {
        var userName = authService.GetUserName();
        $scope.TBoxAccountsList.push({ ACC_ID: $scope.Custom.TBoxAccountID, UID: userName, Acc_Name: $scope.Custom.TBoxAccount });
        $scope.Custom.TBoxAccount = null;
        $scope.Custom.TBoxAccountID = null;
    };

    $scope.DeletefromTBoxAccountsList = function (index) {
        $scope.TBoxAccountsList.splice(index, 1);
    };


    $scope.saveEntityAll = function () {
        debugger;
        $scope.systemOptions.UID = authService.GetUserName();
        var userName = authService.GetUserName();
        if ($scope.systemOptions !== undefined && $scope.systemOptions !== null && $scope.systemOptions.UID !== null && $scope.systemOptions.FIRST_DATE !== null && $scope.systemOptions.END_DATE !== null && $scope.systemOptions.OPERATION_DATE !== null) {
            $scope.removeGridItem(+$scope.shortcutList.length - 1);
            $scope.addressParts.UID = userName;
            $scope.SaveAll($scope.systemOptions, $scope.shortcutList, $scope.emailsList, $scope.userData, $scope.priceType, $scope.KestOptList, $scope.IncomeAccountsList, $scope.TBoxAccountsList, $scope.TStoresList, $scope.addressParts).then(function (results) {
                //$scope.clearEnity();
                $scope.refreshData();
                swal({
                    title: 'تم',
                    text: 'حفظ الإعدادات العامة بنجاح',
                    type: 'success',
                    timer: 1500,
                    showConfirmButton: false
                });
            }, function (error) {
                console.log(error.data.message);
                swal('عفواً',
                    'حدث خطأ عند حفظ الإعدادات العامة',
                    'error');
            });
        }
    }



    $scope.GetUserSystemOption = function () {
        var userName = authService.GetUserName();
        systemOptionsService.getByUserName(userName).then(function (response) {
            var entity = response.data;
            if (entity != null) {
                $scope.systemOptions = entity;
                if ($scope.systemOptions.ReconstructionOfAssets == null ) {
                    $scope.systemOptions.ReconstructionOfAssets = 10;
                }
                $scope.systemOptions.FIRST_DATE = new Date(entity.FIRST_DATE);
                $scope.systemOptions.END_DATE = new Date(entity.END_DATE);
                $scope.systemOptions.OPERATION_DATE = new Date(entity.OPERATION_DATE);

                if (entity.LOSTACCOUNTID != null && entity.LOSTACCOUNTID != undefined && entity.LOSTACCOUNTID != "") {
                    $scope.Custom.Lost_ACC = getAccBasedOnAccID(entity.LOSTACCOUNTID);
                } else { $scope.Custom.Lost_ACC = ""; }

                if (entity.CURRENCYDIFFERENCEACCOUNTID != null && entity.CURRENCYDIFFERENCEACCOUNTID != undefined && entity.CURRENCYDIFFERENCEACCOUNTID != "") {
                    $scope.Custom.Curr_Diff_Acc = getAccBasedOnAccID(entity.CURRENCYDIFFERENCEACCOUNTID);
                } else { $scope.Custom.Curr_Diff_Acc = ""; }



                tBoxAccountsService.getByUserName(userName).then(function (response) {
                    $scope.TBoxAccountsList = response.data;
                    for (var i = 0; i < $scope.TBoxAccountsList.length; i++) {
                        $scope.TBoxAccountsList[i].Acc_Name = getAccBasedOnAccID($scope.TBoxAccountsList[i].ACC_ID);
                    }
                }, function (error) {
                    console.log(error.data.message);
                });

                tStoreService.getByUserName(userName).then(function (response) {
                    $scope.TStoresList = response.data;
                    for (var i = 0; i < $scope.TStoresList.length; i++) {
                        $scope.TStoresList[i].StoreName = getStoreBasedOnStoreID($scope.TStoresList[i].STORE_ID);
                    }
                }, function (error) {
                    console.log(error.data.message);
                });

                tBudgetAccountsService.getByUserName(userName).then(function (response) {
                    $scope.IncomeAccountsList = response.data;
                    for (var i = 0; i < $scope.IncomeAccountsList.length; i++) {
                        $scope.IncomeAccountsList[i].Income_Acc_Name = getAccBasedOnAccID($scope.IncomeAccountsList[i].ACC_ID);
                        $scope.IncomeAccountsList[i].Income_Account_Type_Name = getAccBasedOnAccID($scope.IncomeAccountsList[i].BOXACC_TYPE);
                    }
                }, function (error) {
                    console.log(error.data.message);
                });


                ///////address 

                addressPartsService.getByUserName(userName).then(function (result) {
                    //$window.alert(result);
                    //$window.alert(result.data);
                    if (result.data != null) {
                        $scope.addressParts = result.data;
                    }
                }, function (error) {
                    console.log(error.data.message);
                });
                /////



                $scope.getAllShortcutList();
                $scope.GetEmailList();
            } else {

            }
        }, function (error) {
            console.log(error.data.message);
        });





    }

    var GetKestOptionsList = function () {
        KestOptionService.getAll().then(function (response) {
            $scope.KestOptList = response.data;
            if ($scope.KestOptList != null && $scope.KestOptList != undefined) {
                $scope.Custom.Cust_ACC_Name = $scope.KestOptList[0].account_name;
                $scope.Custom.PBill_Acc_Name = $scope.KestOptList[1].account_name;
                $scope.Custom.Supp_Acc_NAme = $scope.KestOptList[2].account_name;
                $scope.Custom.BillProfit_Acc_Name = $scope.KestOptList[3].account_name;
            } else {
                $scope.Custom.Cust_ACC_Name = null;
                $scope.Custom.PBill_Acc_Name = null;
                $scope.Custom.Supp_Acc_NAme = null;
                $scope.Custom.BillProfit_Acc_Name = null;
            }
        }, function (error) {

        });
    };

    //filter Selected CompanyStoreList from CompanyStoreList during update
    var getAccBasedOnAccID = function (AccID) {
        for (var i = 0; i < $scope.accountList.length; i++) {
            var accId = $scope.accountList[i].ACC_ID
            if (accId == AccID) {
                var ArName = $scope.accountList[i].ACC_AR_NAME;
                return ArName;
            }
        } if (accId == undefined) { return null; }
    }

    var GetAllAccounts = function () {
        accountService.getAll().then(function (response) {
            $scope.accountList = response.data;
        });
    };

    var getStoreBasedOnStoreID = function (storeID) {
        for (var i = 0; i < $scope.companyStoresList.length; i++) {
            var strId = $scope.companyStoresList[i].COM_STORE_ID
            if (strId == storeID) {
                var ArName = $scope.companyStoresList[i].COM_STORE_AR_NAME;
                return ArName;
            }
        } if (strId == undefined) { return null; }
    };

    var GetAllStores = function () {
        companyStoresService.getAll().then(function (response) {
            $scope.companyStoresList = response.data;
        });
    };

    //$scope.deleteEnity = function (entity) {
    //    swal({
    //        title: 'هل تريد الحذف ؟',
    //        text: "لن تستطيع عكس عملية الحذف مرة أخري",
    //        type: 'warning',
    //        showCancelButton: true,
    //        confirmButtonColor: '#3085d6',
    //        cancelButtonColor: '#d33',
    //        confirmButtonText: 'نعم',
    //        cancelButtonText: 'الغاء',
    //        confirmButtonClass: 'btn btn-success btn-lg',
    //        cancelButtonClass: 'btn btn-danger btn-lg',
    //        buttonsStyling: false
    //    }).then(function () {
    //        $scope.delete(entity).then(function (results) {
    //            $scope.clearEnity();
    //            $scope.refreshData();
    //            swal({
    //                title: 'تم',
    //                text: 'الحذف بنجاح',
    //                type: 'success',
    //                timer: 1500,
    //                showConfirmButton: false
    //            });
    //        }, function (error) {
    //            console.log(error.data.message);
    //        });
    //    }, function (dismiss) {
    //        // dismiss can be 'cancel', 'overlay',
    //        // 'close', and 'timer'
    //        if (dismiss === 'cancel') {
    //            swal({
    //                title: 'تم',
    //                text: 'الغاء عملية الحذف',
    //                type: 'error',
    //                timer: 1500,
    //                showConfirmButton: false
    //            });
    //        }
    //    })
    //}

    var getUserData = function () {
        var userID = authService.GetUserID();
        var userName = authService.GetUserName();

        //authService.GetUserData(userID).then(function (response) {
        authService.getUserByUserName(userName).then(function (response) {
            // consol.log(response.data);
            //$window.alert(response.data);
            $scope.userData = response.data;
        }, function (error) {
            // consol.log(error.data.message);
        });
    };

    $scope.getAllOnLoad = function () {
        $q.all([
            GetSteps(),
            GetAllStores(),
            GetAllAccounts(),
            $scope.getAllsellTypeList(),
            $scope.getCurrency(),
            $scope.getPrinters(),
            $scope.getAllOrdersList(),
            $scope.getAllIncomeAccountsList(),
            $scope.GetAllComapanyBranches(),
            $scope.getAllUsers(),
            //$scope.getFilterCompBasedOnValue()
        ]).then(function (allResponses) {
            $scope.GetUserSystemOption();
            getUserData();
            GetKestOptionsList();
            $scope.shortcutItem = {
                UID: null, KEY_TYPE: null, ORDER_ID: null
            };
            $scope.addGridItem();
        }, function (error) {
        });
    }

    $scope.getAllIncomeAccountsList = function () {
        incomeAccountTypesService.getAll().then(function (response) {
            $scope.AccountTypesList = response.data;
        }, function (err) {
            console.log(err.data.message);
        });
    }

    $scope.getAllOrdersList = function () {
        ordersService.getAll().then(function (response) {
            $scope.ordersList = response.data;
        }, function (err) {
            console.log(err.data.message);
        });
    }

    $scope.getAllsellTypeList = function () {
        salesTypeService.getAll().then(function (response) {
            $scope.sellTypeList = response.data;

            $scope.priceType.Price1 = $scope.sellTypeList[0].SELL_TYPE_AR_NAME;
            $scope.priceType.Price2 = $scope.sellTypeList[1].SELL_TYPE_AR_NAME;
            $scope.priceType.Price3 = $scope.sellTypeList[2].SELL_TYPE_AR_NAME;
            $scope.priceType.Price4 = $scope.sellTypeList[3].SELL_TYPE_AR_NAME;
            $scope.priceType.Price5 = $scope.sellTypeList[4].SELL_TYPE_AR_NAME;
            $scope.priceType.Price6 = $scope.sellTypeList[5].SELL_TYPE_AR_NAME;
            $scope.priceType.Price7 = $scope.sellTypeList[6].SELL_TYPE_AR_NAME;

            $scope.priceType.IsShowPrice1 = $scope.sellTypeList[0].SHOW_OR_NOT.toLowerCase() == "true";
            $scope.priceType.IsShowPrice2 = $scope.sellTypeList[1].SHOW_OR_NOT.toLowerCase() == "true";
            $scope.priceType.IsShowPrice3 = $scope.sellTypeList[2].SHOW_OR_NOT.toLowerCase() == "true";
            $scope.priceType.IsShowPrice4 = $scope.sellTypeList[3].SHOW_OR_NOT.toLowerCase() == "true";
            $scope.priceType.IsShowPrice5 = $scope.sellTypeList[4].SHOW_OR_NOT.toLowerCase() == "true";
            $scope.priceType.IsShowPrice6 = $scope.sellTypeList[5].SHOW_OR_NOT.toLowerCase() == "true";
            $scope.priceType.IsShowPrice7 = $scope.sellTypeList[6].SHOW_OR_NOT.toLowerCase() == "true";
        }, function (err) {
            console.log(err.data.message);
        });
    };

    $scope.getAllShortcutList = function () {
        var userName = authService.GetUserName();
        var index = userName.indexOf('@');
        if (index > 0) {
            userName = userName.substring(0, index);
        }
        shortcutService.getByUID(userName).then(function (response) {
            $scope.shortcutList = response.data;
            $scope.addGridItem();
        }, function (err) {
            console.log(err.data.message);
        });
    }

    //$scope.getsystemOptionsTotalCount = function () {
    //    $scope.count().then(function (results) {
    //        var data = results.data;
    //        $scope.systemOptionsTotalCount = data;
    //        $scope.pagesCount = $scope.getPagesCount();
    //    }, function (error) {
    //        console.log(error.data.message);
    //    });
    //}

    //$scope.getsystemOptionsList = function () {
    //    $scope.get($scope.pageNum, $scope.pageSize).then(function (results) {
    //        var data = results.data;
    //        $scope.systemOptionsList = data;
    //    }, function (error) {
    //        console.log(error.data.message);
    //    });
    //}

    $scope.getPrinters = function () {
        commonService.getPrinters().then(function (response) {
            $scope.printersList = response.data;
        }, function (error) {
            console.log(error.data.message);
        });
    }
    $scope.getCurrency = function () {
        currencyService.getAll().then(function (response) {
            $scope.currencyList = response.data;
        }, function (error) {
            console.log(error.data.message);
        });
    }
    $scope.get = function (pageNum, pageSize) {
        return systemOptionsService.get(pageNum, pageSize);
    }
    $scope.SaveAll = function (SysOptEntity, shortcutList, emailsList, userDataEntity, priceTypeEntity, KestOptList, IncomeAccountsList, TBoxAccountsList, TStoresList, addressParts) {
        return systemOptionsService.SaveAll(SysOptEntity, shortcutList, emailsList, userDataEntity, priceTypeEntity, KestOptList, IncomeAccountsList, TBoxAccountsList, TStoresList, addressParts).then(function () {
            $scope.savePrivilge();
        });
    }
    $scope.count = function () {
        return systemOptionsService.count();
    }
    $scope.insert = function (entity) {
        return systemOptionsService.insert(entity);
    }
    $scope.update = function (entity) {
        return systemOptionsService.update(entity);
    }
    $scope.delete = function (entity) {
        return systemOptionsService.delete(entity);
    }
    $scope.systemOptionsPageLoad = function () {
        $scope.getAllOnLoad();
    }

    $scope.pageChanged = function () {
        $scope.getAllOnLoad();
        $log.log('Page changed to: ' + $scope.pageNum);
    };
    /*******************************************************************************************************************/
    /*Choose Account Lost*/
    /*******************************************************************************************************************/
    $scope.openAcc = function () {
        var parentElem = angular.element($document[0].querySelector('.app-content'));
        $scope.modalOpenAcc = false;
        var modalInstance = $uibModal.open({
            templateUrl: 'lostAccountModal.html',
            controller: 'modalCtrl',
            scope: $scope,
            size: 'lg',
            preserveScope: true
        });
        modalInstance.opened.then(function () {
            $scope.modalOpenAcc = true;
        });

        // we want to update state whether the modal closed or was dismissed,
        // so use finally to handle both resolved and rejected promises.
        modalInstance.result.finally(function (selectedItem) {
            $scope.modalOpenAcc = false;
        });
    };

    $scope.searchForAcc = function () {
        var search = $scope.Custom.Lost_ACC;
        $scope.getAccounts(search).then(function (response) {
            $scope.searchAccounts = response.data;
            if ($scope.searchAccounts.length == 1) {
                $scope.Custom.Lost_ACC = $scope.searchAccounts[0].ACC_AR_NAME;
                $scope.systemOptions.LOSTACCOUNTID = $scope.searchAccounts[0].ACC_ID;
            }
            else if ($scope.searchAccounts.length == 0) {
                $scope.Custom.Lost_ACC = "";
                $scope.systemOptions.LOSTACCOUNTID = "";
                return;
            }
            else {
                if ($scope.modalOpenAcc === false || $scope.modalOpenAcc == undefined) { $scope.openAcc(); }
            }

        }, function (err) {
            console.log(err.data.message);
        });
    }

    $scope.addSelectedAccount = function (acc) {
        $scope.Custom.Lost_ACC = acc.ACC_AR_NAME;
        $scope.systemOptions.LOSTACCOUNTID = acc.ACC_ID;
        $uibModalStack.dismissAll();
        ////$scope.acc.pageNumA = 1;
    };

    /*******************************************************************************************************************/
    /*Company Branches Priviliage*/
    /*******************************************************************************************************************/

    // Region var
    $scope.companyBranchesList = [];
    $scope.usersList = [];
    $scope.userList = [];
    $scope.cBChecked = false;
    $scope.userPrivBran = {
        COM_BRN_ID: [],
        ID: null,
    };
    $scope.temp = [];
    // End Region

    // Region Method
    $scope.clearEnity = function () {
        $scope.userPrivBran = {
            COM_BRN_ID: [],
            ID: null,
        };
        $scope.companyBranchesList = [];
        $scope.usersList = [];
        $scope.systemOptions.ReconstructionOfAssets = 10;
    };

    $scope.GetAllComapanyBranches = function () {
        compBranchesService.getAll().then(function (response) {
            $scope.companyBranchesList = response.data;        
        }, function (error) {
            console.log(error.data.message);
        });
    }

    $scope.getAllUsers = function () {
        userPrivilagesService.getAllUsers().then(function (results) {
            $scope.usersList = results.data;
            var userName = authService.GetUserName();
            $.each($scope.usersList, function (index, value) {
                if (value.UID == userName) {
                   // alert($scope.usersList[index])
                    $scope.userPrivBran.ID = $scope.usersList[index].ID;
                    $scope.getUserBranch();
                }
            })
        })
    }

    $scope.change = function (list) {
        $scope.temp = [];
        if ($scope.userPrivBran.ID == null) {
            swal('', 'لابد من اختيار مستخدم اولا', 'error');
            $.each($scope.companyBranchesList, function (index1, value1) {
                if (value1.COM_BRN_ID == list) {
                    $scope.companyBranchesList[index1].SELECTED = false;
                }
            })
        } else {
            //$scope.userPrivBran = [];
        } 
    };

    $scope.getUserBranch = function () {
        $scope.temp = [];
        //alert($scope.userPrivBran.ID)
        if ($scope.userPrivBran.ID != null) {
            $scope.GetUserBran($scope.userPrivBran.ID).then(function (results) {
                if (results.data.length != 0) {
                    $scope.temp = [];
                    $.each(results.data, function (index, value) {
                        $.each($scope.companyBranchesList, function (index1, value1) {
                            if (value1.COM_BRN_ID == value.COM_BRN_ID) {
                                //alert(value.COM_BRN_ID);
                                $scope.temp[$scope.temp.length++] = index1;
                            }
                        })
                    })

                    $.each($scope.companyBranchesList, function (index2, value2) {
                        $scope.companyBranchesList[index2].SELECTED = false;
                        $.each($scope.temp, function (index3, value3) {
                            if (value3 == index2) {
                                $scope.companyBranchesList[index2].SELECTED = true;
                                // $scope.userPrivBran.COM_BRN_ID[$scope.userPrivBran.COM_BRN_ID.length] = $scope.companyBranchesList[index2].COM_BRN_ID;
                            }
                        })
                    })

                } else {
                    $.each($scope.companyBranchesList, function (index1, value1) {
                        $scope.companyBranchesList[index1].SELECTED = false;
                    })
                }

            })
        }
    };

    $scope.GetUserBran = function (id) {
        return userPriviligeBranchesService.getById(id);
    }

    // Region add
    $scope.savePrivilge = function () {
        if ($scope.userPrivBran.ID == null) {
            swal('', 'لابد من اختيار مستخدم اولا', 'error');
        } else {
          //  $scope.ajaxSend();
            $scope.saveEntity();
        }
    }

    $scope.saveEntity = function () {
        $scope.userPrivBran.COM_BRN_ID = [];
        $.each($scope.companyBranchesList, function (index1, value1) {
            if ($scope.companyBranchesList[index1].SELECTED == true) {
                $scope.userPrivBran.COM_BRN_ID[$scope.userPrivBran.COM_BRN_ID.length] = $scope.companyBranchesList[index1].COM_BRN_ID;
            }
        })
        $scope.insertGetID($scope.userPrivBran).then(function (results) {
            //swal({
            //    title: 'تم',
            //    text: 'حفظ صلاحيات الموظف بنجاح',
            //    type: 'success',
            //    timer: 1500,
            //    showConfirmButton: false
            //});
        }, function (error) {
            console.log(error.data.message);
            //swal('عفواً',
            //    'حدث خطأ عند حفظ الصلاحيات للمستخدم',
            //    'error');
        });
    }

    $scope.insertGetID = function (entity) {
        return userPriviligeBranchesService.insert(entity);
    }

    // End Region


    // End Region
    /*******************************************************************************************************************/
    /*Filter with company Branch*/
    /*******************************************************************************************************************/

    // Region var

    //$scope.filterWithCompany = ;



    // End Region

    // Region Method
    //$scope.getFilterCompBasedOnValue = function () {
    //    systemOptionsService.getFilterCompValue().then(function (result) {

    //        $scope.filterWithCompany = result.data;

    //    });
    //}


    // $scope.saveFilterOption = function () {
    //     $scope.saveEntityComp();
    // }

    // $scope.saveEntityComp = function () {
    //     var userName = authService.GetUserName();
    //     systemOptionsService.insertFilterForCompany($("#FBasedonComp")[0].checked, userName).then(function (result) {
    //     });
    // }

    // $scope.ajaxSend = function () {

    //     var userNameLog = authService.GetUserName();

    //     //$.ajaxPrefilter(function (options, originalOptions, jqXHR) {

    //     //    originalOptions.data += "?userNameLog=" + userNameLog
    //     //} );

    //     //$(document).ajaxSend(function (event, jqxhr, settings) {
    //     //    jqxhr.setRequestHeader('userNameLog', 'HM')
    //     //});

    //     //$.ajaxSetup({
    //     //    data: {
    //     //        userName: userName
    //     //    }
    //     //});
    //     //userPriviligeBranchesService.logAjaxReq(userName).then(function (result) {
    //     //    alert(result);
    //     //})
    // };
    // Region add

    // End Region


    // End Region
    /*******************************************************************************************************************/
    /*Choose Account Differnce*/
    /*******************************************************************************************************************/
    $scope.openAccDiff = function () {
        var parentElem = angular.element($document[0].querySelector('.app-content'));
        $scope.modalOpenAccDiff = false;
        var modalInstance = $uibModal.open({
            templateUrl: 'DiffAccountModal.html',
            controller: 'modalCtrl',
            scope: $scope,
            size: 'lg',
            preserveScope: true
        });
        modalInstance.opened.then(function () {
            $scope.modalOpenAccDiff = true;
        });

        // we want to update state whether the modal closed or was dismissed,
        // so use finally to handle both resolved and rejected promises.
        modalInstance.result.finally(function (selectedItem) {
            $scope.modalOpenAccDiff = false;
        });
    };

    $scope.searchForAccDiff = function () {
        var search = $scope.Custom.Curr_Diff_Acc;
        $scope.getAccounts(search).then(function (response) {
            $scope.searchAccountsDiff = response.data;
            if ($scope.searchAccountsDiff.length == 1) {
                $scope.Custom.Curr_Diff_Acc = $scope.searchAccountsDiff[0].ACC_AR_NAME;
                $scope.systemOptions.CURRENCYDIFFERENCEACCOUNTID = $scope.searchAccountsDiff[0].ACC_ID;
            }
            else if ($scope.searchAccountsDiff.length == 0) {
                $scope.Custom.Curr_Diff_Acc = "";
                $scope.systemOptions.CURRENCYDIFFERENCEACCOUNTID = "";
                return;
            }
            else {
                if ($scope.modalOpenAccDiff === false || $scope.modalOpenAccDiff == undefined) { $scope.openAccDiff(); }
            }

        }, function (err) {
            console.log(err.data.message);
        });
    }

    $scope.addSelectedAccountDiff = function (acc) {
        $scope.Custom.Curr_Diff_Acc = acc.ACC_AR_NAME;
        $scope.systemOptions.CURRENCYDIFFERENCEACCOUNTID = acc.ACC_ID;
        $uibModalStack.dismissAll();
        ////$scope.acc.pageNumA = 1;
    };
    /*******************************************************************************************************************/
    /*Choose Customer Account*/
    /*******************************************************************************************************************/
    $scope.openCustAcc = function () {
        var parentElem = angular.element($document[0].querySelector('.app-content'));
        $scope.modalOpenCustAcc = false;
        var modalInstance = $uibModal.open({
            templateUrl: 'CustAccountModal.html',
            controller: 'modalCtrl',
            scope: $scope,
            size: 'lg',
            preserveScope: true
        });
        modalInstance.opened.then(function () {
            $scope.modalOpenCustAcc = true;
        });

        // we want to update state whether the modal closed or was dismissed,
        // so use finally to handle both resolved and rejected promises.
        modalInstance.result.finally(function (selectedItem) {
            $scope.modalOpenCustAcc = false;
        });
    };

    $scope.searchForCustAcc = function () {
        var search = $scope.Custom.Cust_ACC_Name;
        $scope.getAccounts(search).then(function (response) {
            $scope.searchCustAccounts = response.data;
            if ($scope.searchCustAccounts.length == 1) {
                $scope.Custom.Cust_ACC_Name = $scope.searchCustAccounts[0].ACC_AR_NAME;
                $scope.KestOptList[0].account_name = $scope.searchCustAccounts[0].ACC_AR_NAME;
                $scope.KestOptList[0].account_id = $scope.searchCustAccounts[0].ACC_ID;
                $scope.KestOptList[0].account_code = $scope.searchCustAccounts[0].ACC_CODE;
            }
            else if ($scope.searchCustAccounts.length == 0) {
                $scope.Custom.Cust_ACC_Name = null;
                $scope.KestOptList[0].account_name = null;
                $scope.KestOptList[0].account_id = null;
                $scope.KestOptList[0].account_code = null;
                return;
            }
            else {
                if ($scope.modalOpenCustAcc === false || $scope.modalOpenCustAcc == undefined) { $scope.openCustAcc(); }
            }

        }, function (err) {
            console.log(err.data.message);
        });
    }

    $scope.addSelectedCustAccount = function (acc) {
        $scope.Custom.Cust_ACC_Name = acc.ACC_AR_NAME;
        $scope.KestOptList[0].account_name = acc.ACC_AR_NAME;
        $scope.KestOptList[0].account_id = acc.ACC_ID;
        $scope.KestOptList[0].account_code = acc.ACC_CODE;
        $uibModalStack.dismissAll();
        ////$scope.acc.pageNumA = 1;
    };

    /*******************************************************************************************************************/
    /*Choose PBill Account*/
    /*******************************************************************************************************************/
    $scope.openPBillAcc = function () {
        var parentElem = angular.element($document[0].querySelector('.app-content'));
        $scope.modalOpenPBillAcc = false;
        var modalInstance = $uibModal.open({
            templateUrl: 'PBillAccountModal.html',
            controller: 'modalCtrl',
            scope: $scope,
            size: 'lg',
            preserveScope: true
        });
        modalInstance.opened.then(function () {
            $scope.modalOpenPBillAcc = true;
        });

        // we want to update state whether the modal closed or was dismissed,
        // so use finally to handle both resolved and rejected promises.
        modalInstance.result.finally(function (selectedItem) {
            $scope.modalOpenPBillAcc = false;
        });
    };

    $scope.searchForPBillAcc = function () {
        var search = $scope.Custom.PBill_Acc_Name;
        $scope.getAccounts(search).then(function (response) {
            $scope.searchPBillAccounts = response.data;
            if ($scope.searchPBillAccounts.length == 1) {
                $scope.Custom.PBill_Acc_Name = $scope.searchPBillAccounts[0].ACC_AR_NAME;
                $scope.KestOptList[1].account_name = $scope.searchPBillAccounts[0].ACC_AR_NAME;
                $scope.KestOptList[1].account_id = $scope.searchPBillAccounts[0].ACC_ID;
                $scope.KestOptList[1].account_code = $scope.searchPBillAccounts[0].ACC_CODE;
            }
            else if ($scope.searchPBillAccounts.length == 0) {
                $scope.Custom.PBill_Acc_Name = null;
                $scope.KestOptList[1].account_name = null;
                $scope.KestOptList[1].account_id = null;
                $scope.KestOptList[1].account_code = null;
                return;
            }
            else {
                if ($scope.modalOpenPBillAcc === false || $scope.modalOpenPBillAcc == undefined) { $scope.openPBillAcc(); }
            }

        }, function (err) {
            console.log(err.data.message);
        });
    }

    $scope.addSelectedPBillAccount = function (acc) {
        $scope.Custom.PBill_Acc_Name = acc.ACC_AR_NAME;
        $scope.KestOptList[1].account_name = acc.ACC_AR_NAME;
        $scope.KestOptList[1].account_id = acc.ACC_ID;
        $scope.KestOptList[1].account_code = acc.ACC_CODE;
        $uibModalStack.dismissAll();
        ////$scope.acc.pageNumA = 1;
    };

    /*******************************************************************************************************************/
    /*Choose Income Account*/
    /*******************************************************************************************************************/
    $scope.openIncomeAcc = function () {
        var parentElem = angular.element($document[0].querySelector('.app-content'));
        $scope.modalOpenIncomeAcc = false;
        var modalInstance = $uibModal.open({
            templateUrl: 'IncomeAccountModal.html',
            controller: 'modalCtrl',
            scope: $scope,
            size: 'lg',
            preserveScope: true
        });
        modalInstance.opened.then(function () {
            $scope.modalOpenIncomeAcc = true;
        });

        // we want to update state whether the modal closed or was dismissed,
        // so use finally to handle both resolved and rejected promises.
        modalInstance.result.finally(function (selectedItem) {
            $scope.modalOpenIncomeAcc = false;
        });
    };

    $scope.searchForIncomeAcc = function () {
        var search = $scope.Custom.Income_Acc_Name;
        $scope.getAccounts(search).then(function (response) {
            $scope.searchIncomeAccounts = response.data;
            if ($scope.searchIncomeAccounts.length == 1) {
                $scope.Custom.Income_Acc_Name = $scope.searchIncomeAccounts[0].ACC_AR_NAME;
                $scope.Custom.Income_Acc_ID = $scope.searchIncomeAccounts[0].ACC_ID;
                //$scope.KestOptList[1].account_name = $scope.searchIncomeAccounts[0].ACC_AR_NAME;
                //$scope.KestOptList[1].account_id = $scope.searchIncomeAccounts[0].ACC_ID;
                //$scope.KestOptList[1].account_code = $scope.searchIncomeAccounts[0].ACC_CODE;
            }
            else if ($scope.searchIncomeAccounts.length == 0) {
                $scope.Custom.Income_Acc_Name = null;
                $scope.Custom.Income_Acc_ID = null;
                //$scope.KestOptList[1].account_name = null;
                //$scope.KestOptList[1].account_id = null;
                //$scope.KestOptList[1].account_code = null;
                return;
            }
            else {
                if ($scope.modalOpenIncomeAcc === false || $scope.modalOpenIncomeAcc == undefined) { $scope.openIncomeAcc(); }
            }

        }, function (err) {
            console.log(err.data.message);
        });
    }

    $scope.addSelectedIncomeAccount = function (acc) {
        $scope.Custom.Income_Acc_Name = acc.ACC_AR_NAME;
        $scope.Custom.Income_Acc_ID = acc.ACC_ID;
        //$scope.KestOptList[1].account_name = acc.ACC_AR_NAME;
        //$scope.KestOptList[1].account_id = acc.ACC_ID;
        //$scope.KestOptList[1].account_code = acc.ACC_CODE;
        $uibModalStack.dismissAll();
        ////$scope.acc.pageNumA = 1;
    };
    /*******************************************************************************************************************/
    /*Choose Customer Account*/
    /*******************************************************************************************************************/
    $scope.openSuppAcc = function () {
        var parentElem = angular.element($document[0].querySelector('.app-content'));
        $scope.modalOpenSuppAcc = false;
        var modalInstance = $uibModal.open({
            templateUrl: 'SuppAccountModal.html',
            controller: 'modalCtrl',
            scope: $scope,
            size: 'lg',
            preserveScope: true
        });
        modalInstance.opened.then(function () {
            $scope.modalOpenSuppAcc = true;
        });

        // we want to update state whether the modal closed or was dismissed,
        // so use finally to handle both resolved and rejected promises.
        modalInstance.result.finally(function (selectedItem) {
            $scope.modalOpenSuppAcc = false;
        });
    };

    $scope.searchForSuppAcc = function () {
        var search = $scope.Custom.Supp_Acc_NAme;
        $scope.getAccounts(search).then(function (response) {
            $scope.searchSuppAccounts = response.data;
            if ($scope.searchSuppAccounts.length == 1) {
                $scope.Custom.Supp_Acc_NAme = $scope.searchSuppAccounts[0].ACC_AR_NAME;
                $scope.KestOptList[2].account_name = $scope.searchSuppAccounts[0].ACC_AR_NAME;
                $scope.KestOptList[2].account_id = $scope.searchSuppAccounts[0].ACC_ID;
                $scope.KestOptList[2].account_code = $scope.searchSuppAccounts[0].ACC_CODE;
            }
            else if ($scope.searchSuppAccounts.length == 0) {
                $scope.Custom.Supp_Acc_NAme = null;
                $scope.KestOptList[2].account_name = null;
                $scope.KestOptList[2].account_id = null;
                $scope.KestOptList[2].account_code = null;
                return;
            }
            else {
                if ($scope.modalOpenSuppAcc === false || $scope.modalOpenSuppAcc == undefined) { $scope.openSuppAcc(); }
            }

        }, function (err) {
            console.log(err.data.message);
        });
    }

    $scope.addSelectedSuppAccount = function (acc) {
        $scope.Custom.Supp_Acc_NAme = acc.ACC_AR_NAME;
        $scope.KestOptList[2].account_name = acc.ACC_AR_NAME;
        $scope.KestOptList[2].account_id = acc.ACC_ID;
        $scope.KestOptList[2].account_code = acc.ACC_CODE;
        $uibModalStack.dismissAll();
        ////$scope.acc.pageNumA = 1;
    };

    /*******************************************************************************************************************/
    /*Choose Bill Profit Account*/
    /*******************************************************************************************************************/
    $scope.openBillProfitAcc = function () {
        var parentElem = angular.element($document[0].querySelector('.app-content'));
        $scope.modalOpenBillProfitAcc = false;
        var modalInstance = $uibModal.open({
            templateUrl: 'BillProfitAccountModal.html',
            controller: 'modalCtrl',
            scope: $scope,
            size: 'lg',
            preserveScope: true
        });
        modalInstance.opened.then(function () {
            $scope.modalOpenBillProfitAcc = true;
        });

        // we want to update state whether the modal closed or was dismissed,
        // so use finally to handle both resolved and rejected promises.
        modalInstance.result.finally(function (selectedItem) {
            $scope.modalOpenBillProfitAcc = false;
        });
    };

    $scope.searchForBillProfitAcc = function () {
        var search = $scope.Custom.BillProfit_Acc_Name;
        $scope.getAccounts(search).then(function (response) {
            $scope.searchBillProfitAccounts = response.data;
            if ($scope.searchBillProfitAccounts.length == 1) {
                $scope.Custom.BillProfit_Acc_Name = $scope.searchBillProfitAccounts[0].ACC_AR_NAME;
                $scope.KestOptList[3].account_name = $scope.searchBillProfitAccounts[0].ACC_AR_NAME;
                $scope.KestOptList[3].account_id = $scope.searchBillProfitAccounts[0].ACC_ID;
                $scope.KestOptList[3].account_code = $scope.searchBillProfitAccounts[0].ACC_CODE;
            }
            else if ($scope.searchBillProfitAccounts.length == 0) {
                $scope.Custom.BillProfit_Acc_Name = null;
                $scope.KestOptList[3].account_name = null;
                $scope.KestOptList[3].account_id = null;
                $scope.KestOptList[3].account_code = null;
                return;
            }
            else {
                if ($scope.modalOpenBillProfitAcc === false || $scope.modalOpenBillProfitAcc == undefined) { $scope.openBillProfitAcc(); }
            }

        }, function (err) {
            console.log(err.data.message);
        });
    }

    $scope.addSelectedBillProfitAccount = function (acc) {
        $scope.Custom.BillProfit_Acc_Name = acc.ACC_AR_NAME;
        $scope.KestOptList[3].account_name = acc.ACC_AR_NAME;
        $scope.KestOptList[3].account_id = acc.ACC_ID;
        $scope.KestOptList[3].account_code = acc.ACC_CODE;
        $uibModalStack.dismissAll();
        ////$scope.acc.pageNumA = 1;
    };




    /*******************************************************************************************************************/
    /*Choose TBox Account*/
    /*******************************************************************************************************************/
    $scope.openTBoxAcc = function () {
        var parentElem = angular.element($document[0].querySelector('.app-content'));
        $scope.modalOpenTBoxAcc = false;
        var modalInstance = $uibModal.open({
            templateUrl: 'TBoxAccountModal.html',
            controller: 'modalCtrl',
            scope: $scope,
            size: 'lg',
            preserveScope: true
        });
        modalInstance.opened.then(function () {
            $scope.modalOpenTBoxAcc = true;
        });

        // we want to update state whether the modal closed or was dismissed,
        // so use finally to handle both resolved and rejected promises.
        modalInstance.result.finally(function (selectedItem) {
            $scope.modalOpenTBoxAcc = false;
        });
    };

    $scope.searchForTBoxAcc = function () {
        var search = $scope.Custom.TBoxAccount;
        $scope.getAccounts(search).then(function (response) {
            $scope.searchTBoxAccounts = response.data;
            if ($scope.searchTBoxAccounts.length == 1) {
                $scope.Custom.TBoxAccount = $scope.searchTBoxAccounts[0].ACC_AR_NAME;
                $scope.Custom.TBoxAccountID = $scope.searchTBoxAccounts[0].ACC_ID;
            }
            else if ($scope.searchTBoxAccounts.length == 0) {
                $scope.Custom.TBoxAccount = null;
                $scope.Custom.TBoxAccountID = null;
                return;
            }
            else {
                if ($scope.modalOpenTBoxAcc === false || $scope.modalOpenTBoxAcc == undefined) {
                    $scope.openTBoxAcc();
                }
            }

        }, function (err) {
            console.log(err.data.message);
        });
    }

    $scope.addSelectedTBoxAccount = function (acc) {
        $scope.Custom.TBoxAccount = acc.ACC_AR_NAME;
        $scope.Custom.TBoxAccountID = acc.ACC_ID;
        $uibModalStack.dismissAll();
        ////$scope.acc.pageNumA = 1;
    };


    /********************************************************************************************************************/
    $scope.getAccounts = function (search) {
        search = search == undefined || search == "" ? "" : search;
        return accountService.getSearch(search);
    }

    $scope.getStores = function (search) {
        search = search == undefined || search == "" ? "" : search;
        return companyStoresService.getSearch(search);
    }
    /********************************************************************************************************************/


    /*******************************************************************************************************************/
    /*Choose TStore Account*/
    /*******************************************************************************************************************/
    $scope.openTStore = function () {
        var parentElem = angular.element($document[0].querySelector('.app-content'));
        $scope.modalOpenTStore = false;
        var modalInstance = $uibModal.open({
            templateUrl: 'TStoreModal.html',
            controller: 'modalCtrl',
            scope: $scope,
            size: 'lg',
            preserveScope: true
        });
        modalInstance.opened.then(function () {
            $scope.modalOpenTStore = true;
        });

        // we want to update state whether the modal closed or was dismissed,
        // so use finally to handle both resolved and rejected promises.
        modalInstance.result.finally(function (selectedItem) {
            $scope.modalOpenTStore = false;
        });
    };

    $scope.searchForTStore = function () {
        var search = $scope.Custom.TStore;
        $scope.getStores(search).then(function (response) {
            $scope.searchTStore = response.data;
            if ($scope.searchTStore.length == 1) {
                $scope.Custom.TStore = $scope.searchTStore[0].COM_STORE_AR_NAME;
                $scope.Custom.TStoreID = $scope.searchTStore[0].COM_STORE_ID;
            }
            else if ($scope.searchTStore.length == 0) {
                $scope.Custom.TStore = null;
                $scope.Custom.TStoreID = null;
                return;
            }
            else {
                if ($scope.modalOpenTStore === false || $scope.modalOpenTStore == undefined) {
                    $scope.openTStore();
                }
            }

        }, function (err) {
            console.log(err.data.message);
        });
    }

    $scope.addSelectedTStore = function (store) {
        $scope.Custom.TStore = store.COM_STORE_AR_NAME;
        $scope.Custom.TStoreID = store.COM_STORE_ID;
        $uibModalStack.dismissAll();
        ////$scope.acc.pageNumA = 1;
    };

    /*******************************************************************************************************************/
    /*Adding Shortcut*/
    /*******************************************************************************************************************/


    $scope.removeGridItem = function (index) {
        $scope.shortcutList.splice(index, 1);
    };

    $scope.hideButton = function (index) {
        if ($scope.shortcutList[index].ORDER_ID == null || $scope.shortcutList[index].KEY_TYPE == "") {
            return true;
        } else return false;
    };

    $scope.showButton = function (index) {
        if ($scope.shortcutList[index].ORDER_ID == null || $scope.shortcutList[index].KEY_TYPE == "") {
            return false;
        } else return true;
    };

    $scope.insertItem = function (data, Index) {
        if (data != "" && data != null) {
            if (data != undefined && data != null) {
                if (data.KEY_TYPE != null && data.ORDER_ID != null) {
                    if ($scope.shortcutList[Index] != null && $scope.shortcutList[Index] != undefined) {
                        $scope.removeGridItem(Index);
                    }
                    $scope.shortcutItem.KEY_TYPE = data.KEY_TYPE;
                    $scope.shortcutItem.ORDER_ID = data.ORDER_ID;
                    $scope.shortcutItem.UID = authService.GetUserName();
                    $scope.shortcutList.push($scope.shortcutItem);
                    $scope.addGridItem();
                }
            }
            else {
                $scope.removeGridItem(Index);
                $scope.shortcutList.push(data);
                $scope.addGridItem();
            }
        }
    }

    $scope.shortcutItem = {};
    $scope.shortcutList = [];
    $scope.addGridItem = function () {
        var found = false;
        for (var i = 0; i < $scope.shortcutList.length; i++) {
            if ($scope.shortcutList[i] == null || $scope.shortcutList[i] == {}) {
                found = true;
            }
        }
        if (!found) {
            $scope.shortcutItem = {
                UID: null, KEY_TYPE: null, ORDER_ID: null
            };
            $scope.shortcutList.push($scope.shortcutItem);
        }
    };

    $scope.showSelectedOrder = function (order) {
        var selected = [];
        if (order != null && order != undefined) {
            if (order.ORDER_ID) {
                selected = $filter('filter')($scope.ordersList, { ORDER_ID: order.ORDER_ID });
            }
            return selected.length ? selected[0].ORDER_AR_NAME : 'Not set';
        }
    };

    /*************************************************************************************************************/


    /*******************************************************************************************************************************/
    /*******************************************************************************************************************************/


    var GetSteps = function () {
        var lang = authService.GetLanguageID();
        if (lang != null && lang != undefined && lang != "")
            if (lang == 1) {
                $scope.steps = [
                    'رسائل التحذيرات',
                    'قيم افتراضية',
                    'فواتير',
                    'اختصارات',
                    'ملحقات',
                    'البريد الالكتروني',
                    'انشاء الاكواد',
                    'بيانات المستخدم',
                    'انواع الاسعار',
                    'حسابات افتراضية',
                    'حسابات قائمة الدخل',
                    'حسابات و مخازن خاصة',
                    'اقسام العنوان',
                    'الصلاحيات على فروع الشركة',
                    'العناصر',
                    'خيارات الاظهار على حسب فرع الشركة',
                    'اعمار الارصدة'
                  
                ];
            } else if (lang == 2) {
                $scope.steps = [
                    'Warning Messages',
                    'Default Values',
                    'Invoices',
                    'Shortcuts',
                    'Attachments',
                    'Email',
                    'Creating Codes',
                    'User Data',
                    'Price Types',
                    'Default Accounts',
                    'Income Menu Accounts',
                    'Special Accounts and Stores',
                    'Address Parts',
                    'Company Branches Privilige',
                    'Items',
                    'Show Based on Company Branch',
                    'Reconstruction Of Assets'
                ];
            } else {
                $scope.steps = [
                    'رسائل التحذيرات',
                    'قيم افتراضية',
                    'فواتير',
                    'اختصارات',
                    'ملحقات',
                    'البريد الالكتروني',
                    'انشاء الاكواد',
                    'بيانات المستخدم',
                    'انواع الاسعار',
                    'حسابات افتراضية',
                    'حسابات قائمة الدخل',
                    'حسابات و مخازن خاصة',
                    'اقسام العنوان',
                    'الصلاحيات على فروع الشركة',
                    'العناصر',
                    'خيارات الاظهار على حسب فرع الشركة',
                    'اعمار الارصدة'
                ];
            }
        $scope.selection = $scope.steps[0];
    };

    $scope.getCurrentStepIndex = function () {
        // Get the index of the current step given selection
        return _.indexOf($scope.steps, $scope.selection);
    };

    // Go to a defined step index
    $scope.goToStep = function (index) {
        if (!_.isUndefined($scope.steps[index])) {
            $scope.selection = $scope.steps[index];
        }
    };

    $scope.hasNextStep = function () {
        var stepIndex = $scope.getCurrentStepIndex();
        var nextStep = stepIndex + 1;
        // Return true if there is a next step, false if not
        return !_.isUndefined($scope.steps[nextStep]);
    };

    $scope.hasPreviousStep = function () {
        var stepIndex = $scope.getCurrentStepIndex();
        var previousStep = stepIndex - 1;
        // Return true if there is a next step, false if not
        return !_.isUndefined($scope.steps[previousStep]);
    };

    $scope.incrementStep = function () {
        if ($scope.hasNextStep()) {
            var stepIndex = $scope.getCurrentStepIndex();
            var nextStep = stepIndex + 1;
            $scope.selection = $scope.steps[nextStep];
        }
    };

    $scope.decrementStep = function () {
        if ($scope.hasPreviousStep()) {
            var stepIndex = $scope.getCurrentStepIndex();
            var previousStep = stepIndex - 1;
            $scope.selection = $scope.steps[previousStep];
        }
    };






}]);