'use strict';
app.controller('entryController', ['$scope', '$rootScope', '$location', '$routeParams', '$filter', '$log', '$q', 'authService', 'entryService', 'entrySettingService', 'PointOfSaleService', '$timeout', '$document', '$uibModal', 'sharedService', 'costService', 'currencyService', 'entryGridColumnService', 'accountsService', 'commonService', 'localStorageService', 'MenuService', 'customersService', function ($scope, $rootScope, $location, $routeParams, $filter, $log, $q, authService, entryService, entrySettingService, PointOfSaleService, $timeout, $document, $uibModal, sharedService, costService, currencyService, entryGridColumnService, accountsService, commonService, localStorageService, MenuService, customersService) {

    $scope.ReportData = {};

    $scope.entryMaster = {};
    $scope.entryMasters = [];

    $scope.entryDetails = [];
    $scope.entryDetails2 = [];

    $scope.costCentersList = [];
    $scope.isShow = false;

    $scope.gridData = {};

    $scope.isComplixDepit = false;
    $scope.isComplixCredit = false;


    $scope.parentList = [];
    $scope.finalList = [];
    $scope.currencyList = [];
    $scope.accountTypesList = [];
    $scope.account = {};
    $scope.searchBy = "";
    $scope.notification = {};
    $scope.isEditReasonshow = false;
    $scope.IsTaxesIncluded = false;
    $scope.IsRepeatItem = false;
    $scope.IsQuickAccount = false;
    $scope.IsShowTaxesBox = false;
    $scope.IsTaxAccForAccount = false;

    $scope.wariningList = [{
            warningId: 1,
            warningName: "Credit"
        },
        {
            warningId: 2,
            warningName: "Depit"
        },
        {
            warningId: 3,
            warningName: "Without"
        },
        {
            warningId: 4,
            warningName: "Both"
        }
    ];

    $scope.stateList = [{
            stateId: 1,
            stateName: "رئيسي"
        },
        {
            stateId: 2,
            stateName: "فرعي"
        }
    ];

    $scope.userModel = [];
    $scope.getUserModel = function () {
        if (localStorageService.get('UserModule') != null && localStorageService.get('UserModule') != undefined) {
            $scope.userModel = localStorageService.get('UserModule').UserModule;
        }
    }

    $scope.getByUserModel = function (Id) {
        for (var i = 0; i < $scope.userModel.length; i++) {
            if (parseInt(Id) == parseInt($scope.userModel[i])) {
                return true;
            }
        }

        return false;
    }



    $scope.IsEnableTaxEdit = true;

    $scope.entryDetails = [{
        ACC_ID: null,
        ACC_CODE: "",
        ACC_AR_NAME: "",
        ENTRY_CREDIT: 0,
        ENTRY_GOLD_DEBIT: 0,
        ENTRY_GOLD_CREDIT: 0,
        ENTRY_DEBIT: 0,
        ENTRY_DETAILS_REMARKS: "",
        COST_CENTER_ID: 0,
        isComplixDepit: false
    }];
    $scope.entryDetails2 = [{
        ACC_ID: null,
        ACC_CODE: "",
        ACC_AR_NAME: "",
        ENTRY_CREDIT: 0,
        ENTRY_GOLD_DEBIT: 0,
        ENTRY_GOLD_CREDIT: 0,
        ENTRY_DEBIT: 0,
        ENTRY_DETAILS_REMARKS: "",
        COST_CENTER_ID: 0,
        isComplixCredit: false
    }];
    //$scope.entrySetting = { ENTRY_SETTING_ID:null, ENTRY_SETTING_AR_NAME:""}

    //GetAllBoxAccounts
    $scope.GetAllBoxAccounts = function () {

        var entryType = $scope.getBillTypeQueryString();
        $scope.CustomersAccounts = [];
        entryService.GetAllBoxAccounts().then(function (response) {
            $scope.CustomersAccounts = response.data;
        })
    }


    $scope.removeGridBillDetailsItem = function (index) {
        $scope.entryDetails.splice(index, 1);
    };


    $scope.entryList = [];
    $scope.entryTotalCount = 0;
    $scope.pageNum = 1;
    $scope.pageSize = 10;

    //search
    $scope.gridBillDetailsItem = {
        ACC_CODE: "",
        ACC_AR_NAME: "",
        ACC_EN_NAME: "",
        ACC_TYPE_ID: null,
        ACC_DATE: null,
        ACC_CHECK_DATE: null,
        ACC_STATE: null,
        ACC_CREDIT: null,
        ACC_DEBIT: null,
        ACC_MAX_DEBIT: null,
        ACC_MAX_CREDIT: null,
        CURRENCY_ID: null,
        PARENT_ACC_ID: null,
        FINAL_ACC_ID: null,
        ACC_LEVEL: null,
        ACC_REMARKS: "",
        ACC_NSONS: null,
        ACC_TURNNING_YES_OR_NO: "",
        ACC_DEBIT_OR_CREDIT_OR_WITHOUT: null,
        ACC_CUSTOMER_CODE: "",
        Deactivate: false,
        SubjectToVAT: false,
        VATRate: null,
        AddedBy: "",
        AddedOn: null,
        UpdatedBy: "",
        UpdatedOn: null
    };

    $scope.selectedItems = [];

    $scope.items = [];


    $scope.pagesCount = 0;
    $scope.getPagesCount = function () {
        var div = Math.floor($scope.entryTotalCount / $scope.pageSize);
        var rem = $scope.entryTotalCount % $scope.pageSize;
        if (rem > 0) {
            div = div + 1;
        }
        return div;
    }
    $scope.maxSize = 10;


    $scope.clearEnity = function () {

        $location.search('foo', null);

        if ($location.path() === $rootScope.locationPath) {
            $location.replace();
        }

        $scope.entryMaster.ACC_ID = "";
        $scope.entryMaster = {};
        $scope.entryDetails = [];
        $scope.totalCredit = "";
        $scope.totalDepit = "";
        $scope.totalCurrencyCredit = "";
        $scope.totalCurrenctDepit = "";
        $scope.totalTaxRateCredit = "";
        $scope.totalTaxRateDebit = "";
        $scope.totalNotTaxCredit = "";
        $scope.totalNotTaxDebit = "";

        $scope.totalGoldCredit18 = "";
        $scope.totalGoldCredit21 = "";
        $scope.totalGoldCredit22 = "";
        $scope.totalGoldCredit24 = "";
        $scope.totalGoldDepit18 = "";
        $scope.totalGoldDepit21 = "";
        $scope.totalGoldDepit22 = "";
        $scope.totalGoldDepit24 = "";

        $scope.totalCurGoldCredit18 = "";
        $scope.totalCurGoldCredit21 = "";
        $scope.totalCurGoldCredit22 = "";
        $scope.totalCurGoldCredit24 = "";
        $scope.totalCurGoldDepit18 = "";
        $scope.totalCurGoldDepit21 = "";
        $scope.totalCurGoldDepit22 = "";
        $scope.totalCurGoldDepit24 = "";

        //second grid
        $scope.entryDetails2 = [];
        $scope.totalCreditGrd2 = 0;
        $scope.totalTaxRateCreditGrd2 = 0;
        $scope.totalTaxValueCreditGrd2 = 0;
        $scope.totalNotTaxCreditGrd2 = 0;
        $scope.totalGoldCredit24Grd2 = 0;
        $scope.totalGoldCredit22Grd2 = 0;
        $scope.totalGoldCredit21Grd2 = 0;
        $scope.totalGoldCredit18Grd2 = 0;
        $scope.totalCreditWithTaxGrd2 = 0;

        $scope.EntryVal = "";
        $scope.entryMaster.EntryVal = 0;
        $scope.entryDetails2 = [{
            ACC_ID: null,
            ACC_CODE: "",
            ACC_AR_NAME: "",
            ENTRY_CREDIT: 0,
            ENTRY_GOLD_DEBIT: 0,
            ENTRY_GOLD_CREDIT: 0,
            ENTRY_DEBIT: 0,
            ENTRY_DETAILS_REMARKS: "",
            COST_CENTER_ID: 0,
            isComplixCredit: false
        }];
        if ($scope.search2 !== null && $scope.search2 !== undefined) {
            $scope.search2 = "";
        }


        /////

        $scope.dailyEntryMaster = {};
        $scope.entryDetails = [];
        $scope.dailyEntryID = undefined;
        $scope.isEditReasonshow = false;
        $scope.notification = {};

        if ($scope.search !== null && $scope.search !== undefined) {
            $scope.search = "";
        }

        $scope.entryDetails = [{
            ACC_ID: null,
            ACC_CODE: "",
            ACC_AR_NAME: "",
            ENTRY_CREDIT: 0,
            ENTRY_GOLD_DEBIT: 0,
            ENTRY_GOLD_CREDIT: 0,
            ENTRY_DEBIT: 0,
            ENTRY_DETAILS_REMARKS: "",
            COST_CENTER_ID: 0,
            isComplixDepit: false
        }];
        //  $scope.entryDetails = [{ ACC_ID: null, ACC_AR_NAME: "", ENTRY_CREDIT: 0, ENTRY_GOLD_DEBIT: 0, ENTRY_GOLD_CREDIT: 0, ENTRY_DEBIT: 0, ENTRY_DETAILS_REMARKS: "", COST_CENTER_ID: 0 }];
        $scope.getEntryNumber();
        $scope.getSettingById();
        $scope.getCurrentDate();

        //

        //var urlParams = $location.search();
        //if (urlParams.foo != null && urlParams.foo != undefined && urlParams.foo != "") { } else {

        //}

    }


    $scope.refreshData = function () {
        $scope.getAllOnLoad();
    }
    $scope.saveEntry = function () {
        $scope.saveEntity();
    }




    $scope.saveEntity = function () {
        //Get report data
        $scope.GetReportData();
        debugger; 
        $scope.entryMaster.ENTRY_SETTING_ID = $scope.settingId;
        if (localStorageService.get('branchId') != null) {
            $scope.entryMaster.COM_BRN_ID = localStorageService.get('branchId').branchId;
        }


        $scope.savedEntryDetails = $scope.entryDetails;
        var entryMasterDetails = {
            EntryMaster: $scope.entryMaster,
            EntryDetails: $scope.savedEntryDetails
        };

        $scope.checkEntryByEntryNumber().then(function (response) {
            if (response.data || $scope.entryMaster.ENTRY_ID == 0 || $scope.entryMaster.ENTRY_ID == undefined) {
                 
                // if ($scope.typeOfEntry == 10 || $scope.typeOfEntry == 11) {
                if ($scope.typeOfEntry == 3) {
                    //var length = $scope.entryDetails.length;
                    //$scope.entryDetails.splice(parseInt(length) - 1, 1);

                    var calcDmoney = 0;
                    var calcCmoney = 0;
                    var calcD24 = 0;
                    var calcD22 = 0;
                    var calcD21 = 0;
                    var calcD18 = 0;
                    var calcC24 = 0;
                    var calcC22 = 0;
                    var calcC21 = 0;
                    var calcC18 = 0;

                    var rate22 = parseFloat(22 / 24);
                    var rate21 = parseFloat(21 / 24);
                    var rate18 = parseFloat(18 / 24);
                    for (var i = 0; i < $scope.entryDetails.length; i++) {

                        if ($scope.entryDetails[i].ENTRY_DEBIT != undefined) {
                            var dMoney = (parseFloat($scope.entryDetails[i].ENTRY_DEBIT)).toFixed(2);
                            calcDmoney = (parseFloat(dMoney) + parseFloat(calcDmoney)).toFixed(2);
                        }

                        if ($scope.entryDetails[i].ENTRY_GOLD24_DEBIT != undefined) {
                            var d24 = (parseFloat($scope.entryDetails[i].ENTRY_GOLD24_DEBIT)).toFixed(2);
                            calcD24 = (parseFloat(d24) + parseFloat(calcD24)).toFixed(2);
                        }

                        if ($scope.entryDetails[i].ENTRY_GOLD22_DEBIT != undefined) {
                            var d22 = (parseFloat($scope.entryDetails[i].ENTRY_GOLD22_DEBIT)).toFixed(2);
                            calcD22 = ((parseFloat(d22) * parseFloat(rate22)) + parseFloat(calcD22)).toFixed(2);
                        }

                        if ($scope.entryDetails[i].ENTRY_GOLD21_DEBIT != undefined) {
                            var d21 = parseFloat($scope.entryDetails[i].ENTRY_GOLD21_DEBIT).toFixed(2);
                            calcD21 = ((parseFloat(d21) * parseFloat(rate21)) + parseFloat(calcD21)).toFixed(2);
                        }

                        if ($scope.entryDetails[i].ENTRY_GOLD18_DEBIT != undefined) {
                            var d18 = parseFloat($scope.entryDetails[i].ENTRY_GOLD18_DEBIT).toFixed(2);
                            calcD18 = ((parseFloat(d18) * parseFloat(rate18)) + parseFloat(calcD18)).toFixed(2);
                        }
                    }




                    for (var i = 0; i < $scope.entryDetails2.length; i++) {
                        if ($scope.entryDetails2[i].ENTRY_CREDIT != undefined) {
                            var cMoney = parseFloat($scope.entryDetails2[i].ENTRY_CREDIT).toFixed(2);
                            calcCmoney = (parseFloat(cMoney) + parseFloat(calcCmoney)).toFixed(2);
                        }

                        if ($scope.entryDetails2[i].ENTRY_GOLD24_CREDIT != undefined) {
                            var c24 = parseFloat($scope.entryDetails2[i].ENTRY_GOLD24_CREDIT).toFixed(2);
                            calcC24 = (parseFloat(c24) + parseFloat(calcC24)).toFixed(2);
                        }

                        if ($scope.entryDetails2[i].ENTRY_GOLD22_CREDIT != undefined) {
                            var c22 = parseFloat($scope.entryDetails2[i].ENTRY_GOLD22_CREDIT).toFixed(2);
                            calcC22 = ((parseFloat(c22) * parseFloat(rate22)) + parseFloat(calcC22)).toFixed(2);
                        }

                        if ($scope.entryDetails2[i].ENTRY_GOLD21_CREDIT != undefined) {
                            var c21 = parseFloat($scope.entryDetails2[i].ENTRY_GOLD21_CREDIT).toFixed(2);
                            calcC21 = ((parseFloat(c21) * parseFloat(rate21)) + parseFloat(calcC21)).toFixed(2);
                        }

                        if ($scope.entryDetails2[i].ENTRY_GOLD18_CREDIT != undefined) {
                            var c18 = parseFloat($scope.entryDetails2[i].ENTRY_GOLD18_CREDIT).toFixed(2);
                            calcC18 = ((parseFloat(c18) * parseFloat(rate18)) + parseFloat(calcC18)).toFixed(2);
                        }
                    }

                    //for (var i = 0; i < $scope.entryDetails.length; i++) {
                    //    if ($scope.entryDetails[i].ENTRY_CREDIT != undefined) {
                    //        calcCmoney = parseFloat($scope.entryDetails[i].ENTRY_CREDIT) + parseFloat(calcCmoney);
                    //    }

                    //    if ($scope.entryDetails[i].ENTRY_DEBIT != undefined) {
                    //        calcDmoney = parseFloat($scope.entryDetails[i].ENTRY_DEBIT) + parseFloat(calcDmoney);
                    //    }

                    //    if ($scope.entryDetails[i].ENTRY_GOLD24_CREDIT != undefined) {
                    //        calcC24 = parseFloat($scope.entryDetails[i].ENTRY_GOLD24_CREDIT) + parseFloat(calcC24);
                    //    }

                    //    if ($scope.entryDetails[i].ENTRY_GOLD24_DEBIT != undefined) {
                    //        calcD24 = parseFloat($scope.entryDetails[i].ENTRY_GOLD24_DEBIT) + parseFloat(calcD24);
                    //    }

                    //    if ($scope.entryDetails[i].ENTRY_GOLD22_CREDIT != undefined) {
                    //        calcC22 = parseFloat($scope.entryDetails[i].ENTRY_GOLD22_CREDIT) + parseFloat(calcC22);
                    //    }

                    //    if ($scope.entryDetails[i].ENTRY_GOLD22_DEBIT != undefined) {
                    //        calcD22 = parseFloat($scope.entryDetails[i].ENTRY_GOLD22_DEBIT) + parseFloat(calcD22);
                    //    }

                    //    if ($scope.entryDetails[i].ENTRY_GOLD21_CREDIT != undefined) {
                    //        calcC21 = parseFloat($scope.entryDetails[i].ENTRY_GOLD21_CREDIT) + parseFloat(calcC21);
                    //    }

                    //    if ($scope.entryDetails[i].ENTRY_GOLD21_DEBIT != undefined) {
                    //        calcD21 = parseFloat($scope.entryDetails[i].ENTRY_GOLD21_DEBIT) + parseFloat(calcD21);
                    //    }

                    //    if ($scope.entryDetails[i].ENTRY_GOLD18_CREDIT != undefined) {
                    //        calcC18 = parseFloat($scope.entryDetails[i].ENTRY_GOLD18_CREDIT) + parseFloat(calcC18);
                    //    }

                    //    if ($scope.entryDetails[i].ENTRY_GOLD18_DEBIT != undefined) {
                    //        calcD18 = parseFloat($scope.entryDetails[i].ENTRY_GOLD18_DEBIT) + parseFloat(calcD18);
                    //    }
                    //}


                    if (parseFloat((calcCmoney != null && calcCmoney != undefined) ? calcCmoney : 0) - parseFloat((calcDmoney != null && calcDmoney != undefined) ? calcDmoney : 0) != 0 || parseFloat((calcC24 != null && calcC24 != undefined) ? calcC24 : 0) - parseFloat((calcD24 != null && calcD24 != undefined) ? calcD24 : 0) != 0 || parseFloat((calcC22 != null && calcC22 != undefined) ? calcC22 : 0) - parseFloat((calcD22 != null && calcD22 != undefined) ? calcD22 : 0) != 0 || parseFloat((calcC21 != null && calcC21 != undefined) ? calcC21 : 0) - parseFloat((calcD21 != null && calcD21 != undefined) ? calcD21 : 0) != 0 || parseFloat((calcC18 != null && calcC18 != undefined) ? calcC18 : 0) - parseFloat((calcD18 != null && calcD18 != undefined) ? calcD18 : 0) != 0) {
                        $scope.entryObj = {
                            ACC_ID: null,
                            ACC_CODE: "",
                            ACC_AR_NAME: "",
                            ENTRY_CREDIT: 0,
                            ENTRY_GOLD_DEBIT: 0,
                            ENTRY_GOLD_CREDIT: 0,
                            ENTRY_DEBIT: 0,
                            ENTRY_DETAILS_REMARKS: "",
                            COST_CENTER_ID: 0
                        }
                        $scope.entryDetails.push($scope.entryObj);

                        swal({
                            // title: 'تم',
                            text: 'القيد غير متوازن',
                            type: 'error',
                            timer: 1500,
                            showConfirmButton: false
                        });
                        return false;
                    }
                }


                if ($scope.EntryModeID == 2) {
                    $scope.checkBalanceOfComplexentry();
                    if ($scope.diffMoney != 0 || $scope.diffGold != 0) {
                        if ($scope.diffMoney != 0 && $scope.diffGold == 0) {
                            swal({
                                title: 'تحذير',
                                text: ' القيد غير متوازن بقيمه ' + $scope.diffMoney,
                                type: 'warning',
                                showCancelButton: true,
                                confirmButtonColor: '#3085d6',
                                cancelButtonColor: '#d33',
                                confirmButtonText: 'نعم',
                                cancelButtonText: 'الغاء',
                                confirmButtonClass: 'btn btn-success btn-lg',
                                cancelButtonClass: 'btn btn-danger btn-lg',
                                buttonsStyling: false
                            });
                            return false;
                        } else if ($scope.diffMoney == 0 && $scope.diffGold != 0) {
                            swal({
                                title: 'تحذير',
                                text: ' القيد غير متوازن بقيمه ' + $scope.diffGold +
                                    ' من عيار 24 ',
                                type: 'warning',
                                showCancelButton: true,
                                confirmButtonColor: '#3085d6',
                                cancelButtonColor: '#d33',
                                confirmButtonText: 'نعم',
                                cancelButtonText: 'الغاء',
                                confirmButtonClass: 'btn btn-success btn-lg',
                                cancelButtonClass: 'btn btn-danger btn-lg',
                                buttonsStyling: false

                            });
                            return false;
                        } else if ($scope.diffMoney != 0 && $scope.diffGold != 0) {
                            swal({
                                // title: 'تم',
                                title: 'تحذير',
                                text: ' القيد غير متوازن بقيمه ' + $scope.diffGold +
                                    '  من عيار 24 و نقدي بقيمه' + $scope.diffMoney,
                                type: 'warning',
                                showCancelButton: true,
                                confirmButtonColor: '#3085d6',
                                cancelButtonColor: '#d33',
                                confirmButtonText: 'نعم',
                                cancelButtonText: 'الغاء',
                                confirmButtonClass: 'btn btn-success btn-lg',
                                cancelButtonClass: 'btn btn-danger btn-lg',
                                buttonsStyling: false
                            });
                            return false;
                        }
                    } else {
                        //save second grid
                        
                        if ($scope.operation != "Update" || $scope.operation == undefined) {
                            var length = $scope.savedEntryDetails.length;
                            $scope.savedEntryDetails.splice(parseInt(length) - 1, 1);

                            var length2 = $scope.entryDetails2.length;
                            $scope.entryDetails2.splice(parseInt(length2) - 1, 1);

                            for (var i = 0; i < $scope.entryDetails2.length; i++) {
                                var gridEntry = {}
                                gridEntry = $scope.entryDetails2[i];
                                $scope.savedEntryDetails.push(gridEntry);
                            }
                        }
                    }
                } else {
                    var length = $scope.savedEntryDetails.length;
                    $scope.savedEntryDetails.splice(parseInt(length) - 1, 1);
                }

                if ($scope.operation == "Update" && ($scope.entryMaster.EditReason == null || $scope.entryMaster.EditReason == undefined || $scope.entryMaster.EditReason == "")) {
                    swal('عفواً',
                        'يجب ذكر سبب التعديل ',
                        'error');
                } else {

                    if ($scope.operation == "Update") {
                        var length = $scope.savedEntryDetails.length;
                        $scope.savedEntryDetails.splice(parseInt(length) - 1, 1);

                        var length2 = $scope.entryDetails2.length;
                        $scope.entryDetails2.splice(parseInt(length2) - 1, 1);

                        for (var i = 0; i < $scope.entryDetails2.length; i++) {
                            var gridEntry = {}
                            gridEntry = $scope.entryDetails2[i];
                            $scope.savedEntryDetails.push(gridEntry);
                        }
                    }


                    entryService.addEntryMasterDetails(entryMasterDetails).then(function (result) {
                        if (result.data != undefined) {
                             
                            $scope.masterID = result.data;
                            if ($scope.settingId != 3) {
                                $scope.saveDailyEntry();
                            }
                            swal({
                                    title: "هل تريد طباعة السند?",
                                    type: "warning",
                                    showCancelButton: true,
                                    confirmButtonColor: "#DD6B55",
                                    confirmButtonText: "نعم",
                                    cancelButtonText: "لا",
                                    closeOnConfirm: false,
                                    closeOnCancel: false
                                }

                            ).then(function () {
                                
                                $scope.printDiv('Bill');
                                setTimeout(function () {
                                    $scope.clearEnity();
                                    $scope.refreshData();
                                }, 5000);
                            }, function () {
                                $scope.clearEnity();
                                $scope.refreshData();
                            });
                            //   if ($scope.savedDailyEntry != null && $scope.savedDailyEntry != undefined) {
                            //$scope.clearEnity();
                            //$scope.refreshData();
                            //swal({
                            //    title: 'تم',
                            //    text: 'الحفظ  بنجاح',
                            //    type: 'success',
                            //    timer: 1500,
                            //    showConfirmButton: false
                            //});
                            //  }
                        } else {
                            swal({
                                title: 'خطأ',
                                text: 'لم يتم الحفظ',
                                type: 'error',
                                timer: 1500,
                                showConfirmButton: false
                            });
                        }

                    })
                }
                //end

            } else {

                swal({
                    // title: 'تم',
                    text: 'رقم الفاتوره غير موجود',
                    type: 'error',
                    timer: 1500,
                    showConfirmButton: false
                });
            }

        });

    }



    $scope.clearSearchAccounts = function () {
        $scope.search = "";
        $scope.searchAccounts = [];
        $scope.itemModalShowStatus = false;

    }

    //function to get itemNameAr
    $scope.getItemNameUsingItemID = function (ItemID) {
        entryService.getItemNameUsingItemID(ItemID).then(function (response) {
            return response.data || 'Not set';
        })
    }
    $scope.convertBillDetailsToSelectedItems = function () {
        $scope.selectedItems = [];

        for (var i = 0; i < $scope.billDetails.length; i++) {
            var x = {};

            x.ITEM_CODE = $scope.billDetails[i].ITEM_ID;
            x.ITEM_AR_NAME = $scope.billDetails[i].ITEM_AR_NAME;
            x.Quantity = $scope.billDetails[i].QTY;
            x.Price = $scope.billDetails[i].UNIT_PRICE;
            x.Unit = $scope.billDetails[i].UnitNameAr;
            x.Discount = $scope.billDetails[i].DISC_VALUE;
            x.DISC_RATE = $scope.billDetails[i].DISC_RATE;
            x.Total = x.Price * x.Quantity;
            $scope.selectedItems.push(x);
        }
    }


    $scope.updateCostCenter = function (data, index) {
        if (data == null || data == undefined) {
            data = 0;
        } else {
            // $scope.entryDetails[index].ENTRY_DEBIT = 0;
            $scope.entryDetails[index].COST_CENTER_ID = data;
        }
        // $scope.calculateTotals();
    }



    $scope.updateItemListOnChange = function (cellName, data, index) {
        
        if (cellName == 'ENTRY_CREDIT') {
             
            if (data == null || data == undefined) {
                data = 0;
            }
            if (parseInt(data) > 0) {
                $scope.entryDetails[index].ENTRY_DEBIT = 0;
                $scope.entryDetails[index].ENTRY_CREDIT = data;

                if ($scope.typeOfEntry != 88 && $scope.typeOfEntry != 89 && $scope.IsTaxesIncluded == true) {
                    if ($scope.entryDetails[index].Taxable && $scope.entryDetails[index].TaxRate != undefined && $scope.entryDetails[index].TaxRate != null && $scope.entryDetails[index].TaxRate != 0) {
                        $scope.entryDetails[index].CreditValueWithoutTax = parseFloat(data * (1 / (1 + parseFloat($scope.entryDetails[index].TaxRate) / 100))).toFixed(2);
                        $scope.entryDetails[index].TaxValue = parseFloat(data - $scope.entryDetails[index].CreditValueWithoutTax).toFixed(2);
                    }
                } else {
                    if ($scope.entryDetails[index].TaxRate != undefined && $scope.entryDetails[index].TaxRate != null && $scope.entryDetails[index].TaxRate != 0 && ($scope.entryDetails[index].TaxValue == undefined || $scope.entryDetails[index].TaxValue == 0 || $scope.entryDetails[index].TaxValue == null)) {
                        $scope.entryDetails[index].TaxValue = ((parseFloat($scope.entryDetails[index].TaxRate) / 100) * parseFloat($scope.entryDetails[index].ENTRY_CREDIT)).toFixed(2);
                    } else if ($scope.entryDetails[index].TaxValue != undefined && $scope.entryDetails[index].TaxValue != null && $scope.entryDetails[index].TaxValue != 0 && ($scope.entryDetails[index].TaxRate == undefined || $scope.entryDetails[index].TaxRate == 0 || $scope.entryDetails[index].TaxRate == null)) {
                        $scope.entryDetails[index].TaxRate = (parseFloat($scope.entryDetails[index].TaxValue * 100) / parseFloat($scope.entryDetails[index].ENTRY_CREDIT)).toFixed(2);
                    } else if (($scope.entryDetails[index].TaxRate != undefined && $scope.entryDetails[index].TaxRate != null && $scope.entryDetails[index].TaxRate != 0) && ($scope.entryDetails[index].TaxValue != undefined && $scope.entryDetails[index].TaxValue != null && $scope.entryDetails[index].TaxValue != 0)) {
                        $scope.entryDetails[index].TaxValue = ((parseFloat($scope.entryDetails[index].TaxRate) / 100) * parseFloat($scope.entryDetails[index].ENTRY_CREDIT)).toFixed(2);
                    }
                }
            }
            $scope.calculateTotals();
        } else if (cellName == 'ENTRY_DEBIT') {
            
            if (data == null || data == undefined) {
                data = 0;
            }
            if (parseInt(data) > 0) {
                $scope.entryDetails[index].ENTRY_CREDIT = 0;
                $scope.entryDetails[index].ENTRY_DEBIT = data;

                if (($scope.typeOfEntry != 88 && $scope.typeOfEntry != 89) && $scope.IsTaxesIncluded == true) {

                    if ($scope.entryDetails[index].Taxable && $scope.entryDetails[index].TaxRate != undefined && $scope.entryDetails[index].TaxRate != null && $scope.entryDetails[index].TaxRate != 0) {
                        $scope.entryDetails[index].DebitValueWithoutTax = parseFloat(data * (1 / (1 + parseFloat($scope.entryDetails[index].TaxRate) / 100))).toFixed(2);
                        $scope.entryDetails[index].TaxValue = (data - $scope.entryDetails[index].DebitValueWithoutTax).toFixed(2);
                    }
                } else {
                    if ($scope.entryDetails[index].TaxRate != undefined && $scope.entryDetails[index].TaxRate != null && $scope.entryDetails[index].TaxRate != 0 && ($scope.entryDetails[index].TaxValue == undefined || $scope.entryDetails[index].TaxValue == 0 || $scope.entryDetails[index].TaxValue == null)) {
                        $scope.entryDetails[index].TaxValue = ((parseFloat($scope.entryDetails[index].TaxRate) / 100) * parseFloat($scope.entryDetails[index].ENTRY_DEBIT)).toFixed(2);
                    } else if ($scope.entryDetails[index].TaxValue != undefined && $scope.entryDetails[index].TaxValue != null && $scope.entryDetails[index].TaxValue != 0 && ($scope.entryDetails[index].TaxRate == undefined || $scope.entryDetails[index].TaxRate == 0 || $scope.entryDetails[index].TaxRate == null)) {
                        $scope.entryDetails[index].TaxRate = (parseFloat($scope.entryDetails[index].TaxValue * 100) / parseFloat($scope.entryDetails[index].ENTRY_DEBIT)).toFixed(2);
                    } else if (($scope.entryDetails[index].TaxRate != undefined && $scope.entryDetails[index].TaxRate != null && $scope.entryDetails[index].TaxRate != 0) && ($scope.entryDetails[index].TaxValue != undefined && $scope.entryDetails[index].TaxValue != null && $scope.entryDetails[index].TaxValue != 0)) {
                        $scope.entryDetails[index].TaxValue = ((parseFloat($scope.entryDetails[index].TaxRate) / 100) * parseFloat($scope.entryDetails[index].ENTRY_DEBIT)).toFixed(2);
                    }
                }
            }

            $scope.addTaxAfterCheckTaxable(index);
            $scope.calculateTotals();
        } else if (cellName == 'TaxRate') {
            if (data == null || data == undefined) {
                data = 0;
            }
             
            $scope.entryDetails[index].TaxRate = data;

            if ($scope.entryDetails[index].ENTRY_CREDIT != undefined && $scope.entryDetails[index].ENTRY_CREDIT != 0) {
                if ($scope.IsTaxesIncluded) {
                    var creditBeforeTax = (parseFloat($scope.entryDetails[index].ENTRY_CREDIT) * (100 / (100 + parseFloat($scope.entryDetails[index].TaxRate)))).toFixed(2);
                    $scope.entryDetails[index].CreditValueWithoutTax = creditBeforeTax;
                    $scope.entryDetails[index].TaxValue = (parseFloat($scope.entryDetails[index].ENTRY_CREDIT) - parseFloat(creditBeforeTax)).toFixed(2);
                } else {
                    $scope.entryDetails[index].TaxValue = ((parseFloat($scope.entryDetails[index].TaxRate) / 100) * parseFloat(creditBeforeTax)).toFixed(2);
                }
            } else if ($scope.entryDetails[index].ENTRY_DEBIT != undefined && $scope.entryDetails[index].ENTRY_DEBIT != 0) {

                if ($scope.IsTaxesIncluded) {
                    var depitBeforTax = (parseFloat($scope.entryDetails[index].ENTRY_DEBIT) * (100 / (100 + parseFloat($scope.entryDetails[index].TaxRate)))).toFixed(2);
                    $scope.entryDetails[index].TaxValue = (parseFloat($scope.entryDetails[index].ENTRY_DEBIT) - parseFloat(depitBeforTax)).toFixed(2);
                } else {
                    $scope.entryDetails[index].TaxValue = ((parseFloat($scope.entryDetails[index].TaxRate) / 100) * parseFloat($scope.entryDetails[index].ENTRY_DEBIT)).toFixed(2);
                }
            }



            if ($scope.entryDetails[index].Taxable == true) {
                if ($scope.entryDetails[index].TaxRate == $scope.entryMaster.MainVatRate) {
                    $scope.entryDetails[index].IsMainVatValue = true;
                    $scope.entryDetails[index].MainVatValue = ($scope.entryDetails[index].ENTRY_DEBIT != null ? $scope.entryDetails[index].ENTRY_DEBIT : $scope.entryDetails[index].ENTRY_CREDIT);
                    $scope.entryDetails[index].MainVat = $scope.entryDetails[index].TaxValue;

                    $scope.entryDetails[index].IsZeroVatValue = false;
                    $scope.entryDetails[index].ZeroVatValue = 0;
                    $scope.entryDetails[index].IsExemptOfTax = false;
                    $scope.entryDetails[index].ExemptOfTaxValue = 0;
                } else if ($scope.entryDetails[index].TaxRate == 0) {
                    $scope.entryDetails[index].IsMainVatValue = false;
                    $scope.entryDetails[index].MainVatValue = 0;
                    $scope.entryDetails[index].MainVat = 0;

                    $scope.entryDetails[index].IsZeroVatValue = true;
                    $scope.entryDetails[index].ZeroVatValue = ($scope.entryDetails[index].ENTRY_DEBIT != null ? $scope.entryDetails[index].ENTRY_DEBIT : $scope.entryDetails[index].ENTRY_CREDIT);

                    $scope.entryDetails[index].IsExemptOfTax = false;
                    $scope.entryDetails[index].ExemptOfTaxValue = 0;
                } else {
                    $scope.entryDetails[index].IsMainVatValue = false;
                    $scope.entryDetails[index].MainVatValue = 0;
                    $scope.entryDetails[index].MainVat = 0;

                    $scope.entryDetails[index].IsZeroVatValue = false;
                    $scope.entryDetails[index].ZeroVatValue = 0;

                    $scope.entryDetails[index].IsExemptOfTax = false;
                    $scope.entryDetails[index].ExemptOfTaxValue = 0;
                }
            } else {
                $scope.entryDetails[index].IsMainVatValue = false;
                $scope.entryDetails[index].MainVatValue = 0;
                $scope.entryDetails[index].MainVat = 0;

                $scope.entryDetails[index].IsZeroVatValue = false;
                $scope.entryDetails[index].ZeroVatValue = 0;

                $scope.entryDetails[index].IsExemptOfTax = false;
                $scope.entryDetails[index].ExemptOfTaxValue = 0;

            }






            $scope.addTaxAfterCheckTaxable(index);

            $scope.calculateTotals();
        } else if (cellName == 'TaxValue') {
            if (data == null || data == undefined) {
                data = 0;
            }
            $scope.entryDetails[index].TaxValue = data;
            if ($scope.entryDetails[index].ENTRY_CREDIT != undefined && $scope.entryDetails[index].ENTRY_CREDIT != 0) {

                if ($scope.IsTaxesIncluded) {
                    var creditBeforTax = parseFloat($scope.entryDetails[index].ENTRY_CREDIT) - parseFloat($scope.entryDetails[index].TaxValue);
                    $scope.entryDetails[index].CreditValueWithoutTax = creditBeforeTax;
                    $scope.entryDetails[index].TaxRate = (parseFloat($scope.entryDetails[index].TaxValue * 100) / parseFloat(creditBeforTax)).toFixed(2);
                } else {

                    $scope.entryDetails[index].TaxRate = (parseFloat($scope.entryDetails[index].TaxValue * 100) / parseFloat($scope.entryDetails[index].ENTRY_CREDIT)).toFixed(2);
                }
            } else if ($scope.entryDetails[index].ENTRY_DEBIT != undefined && $scope.entryDetails[index].ENTRY_DEBIT != 0) {

                if ($scope.IsTaxesIncluded) {
                    var depitBeforTax = parseFloat($scope.entryDetails[index].ENTRY_DEBIT) - parseFloat($scope.entryDetails[index].TaxValue);
                    $scope.entryDetails[index].DebitValueWithoutTax = depitBeforTax;
                    $scope.entryDetails[index].TaxRate = (parseFloat($scope.entryDetails[index].TaxValue * 100) / parseFloat(depitBeforTax)).toFixed(2);
                } else {
                    $scope.entryDetails[index].TaxRate = (parseFloat($scope.entryDetails[index].TaxValue * 100) / parseFloat($scope.entryDetails[index].ENTRY_DEBIT)).toFixed(2);
                }
            }

            /////////separte taxes
            if ($scope.entryDetails[index].Taxable == true) {
                if ($scope.entryDetails[index].TaxRate == $scope.entryMaster.MainVatRate) {
                    $scope.entryDetails[index].IsMainVatValue = true;
                    $scope.entryDetails[index].MainVatValue = ($scope.entryDetails[index].ENTRY_DEBIT != null ? $scope.entryDetails[index].ENTRY_DEBIT : $scope.entryDetails[index].ENTRY_CREDIT);
                    $scope.entryDetails[index].MainVat = $scope.entryDetails[index].TaxValue;

                    $scope.entryDetails[index].IsZeroVatValue = false;
                    $scope.entryDetails[index].ZeroVatValue = 0;
                    $scope.entryDetails[index].IsExemptOfTax = false;
                    $scope.entryDetails[index].ExemptOfTaxValue = 0;
                } else if ($scope.entryDetails[index].TaxRate == 0) {
                    $scope.entryDetails[index].IsMainVatValue = false;
                    $scope.entryDetails[index].MainVatValue = 0;
                    $scope.entryDetails[index].MainVat = 0;

                    $scope.entryDetails[index].IsZeroVatValue = true;
                    $scope.entryDetails[index].ZeroVatValue = ($scope.entryDetails[index].ENTRY_DEBIT != null ? $scope.entryDetails[index].ENTRY_DEBIT : $scope.entryDetails[index].ENTRY_CREDIT);

                    $scope.entryDetails[index].IsExemptOfTax = false;
                    $scope.entryDetails[index].ExemptOfTaxValue = 0;
                } else {
                    $scope.entryDetails[index].IsMainVatValue = false;
                    $scope.entryDetails[index].MainVatValue = 0;
                    $scope.entryDetails[index].MainVat = 0;

                    $scope.entryDetails[index].IsZeroVatValue = false;
                    $scope.entryDetails[index].ZeroVatValue = 0;

                    $scope.entryDetails[index].IsExemptOfTax = false;
                    $scope.entryDetails[index].ExemptOfTaxValue = 0;
                }
            } else {
                $scope.entryDetails[index].IsMainVatValue = false;
                $scope.entryDetails[index].MainVatValue = 0;
                $scope.entryDetails[index].MainVat = 0;

                $scope.entryDetails[index].IsZeroVatValue = false;
                $scope.entryDetails[index].ZeroVatValue = 0;

                $scope.entryDetails[index].IsExemptOfTax = false;
                $scope.entryDetails[index].ExemptOfTaxValue = 0;

            }




            $scope.addTaxAfterCheckTaxable(index);
            $scope.calculateTotals();
        } else if (cellName == 'Taxable') {

            //to check if adding tax account of complix entry or not
            if ($scope.entryDetails[index].ENTRY_DEBIT != undefined && $scope.entryDetails[index].ENTRY_DEBIT != 0 && $scope.entryDetails[index].ENTRY_DEBIT != null) {
                $scope.depitTaxAccountComplix = true;
            } else if ($scope.entryDetails[index].ENTRY_CREDIT != undefined && $scope.entryDetails[index].ENTRY_CREDIT != 0 && $scope.entryDetails[index].ENTRY_CREDIT != null) {
                $scope.creditTaxAccountComplix = true;
            }

            var x = $scope.entryDetails[index].Taxable;

            if ($scope.entryDetails[index].Taxable == false) {

                $scope.RemoveTaxAfterUnCheckTaxable(index, $scope.entryDetails[index].TaxValue);

                $scope.entryDetails[index].TaxValue = 0;
                $scope.entryDetails[index].TaxRate = 0;
            } else {
                $scope.entryDetails[index].IsExemptOfTax = false;
            }





            if ($scope.entryDetails[index].Taxable == true) {
                if ($scope.entryDetails[index].TaxRate == $scope.entryMaster.MainVatRate) {
                    $scope.entryDetails[index].IsMainVatValue = true;
                    $scope.entryDetails[index].MainVatValue = ($scope.entryDetails[index].ENTRY_DEBIT != null ? $scope.entryDetails[index].ENTRY_DEBIT : $scope.entryDetails[index].ENTRY_CREDIT);
                    $scope.entryDetails[index].MainVat = $scope.entryDetails[index].TaxValue;

                    $scope.entryDetails[index].IsZeroVatValue = false;
                    $scope.entryDetails[index].ZeroVatValue = 0;
                    $scope.entryDetails[index].IsExemptOfTax = false;
                    $scope.entryDetails[index].ExemptOfTaxValue = 0;
                } else if ($scope.entryDetails[index].TaxRate == 0) {
                    $scope.entryDetails[index].IsMainVatValue = false;
                    $scope.entryDetails[index].MainVatValue = 0;
                    $scope.entryDetails[index].MainVat = 0;

                    $scope.entryDetails[index].IsZeroVatValue = true;
                    $scope.entryDetails[index].ZeroVatValue = ($scope.entryDetails[index].ENTRY_DEBIT != null ? $scope.entryDetails[index].ENTRY_DEBIT : $scope.entryDetails[index].ENTRY_CREDIT);

                    $scope.entryDetails[index].IsExemptOfTax = false;
                    $scope.entryDetails[index].ExemptOfTaxValue = 0;
                } else {
                    $scope.entryDetails[index].IsMainVatValue = false;
                    $scope.entryDetails[index].MainVatValue = 0;
                    $scope.entryDetails[index].MainVat = 0;

                    $scope.entryDetails[index].IsZeroVatValue = false;
                    $scope.entryDetails[index].ZeroVatValue = 0;

                    $scope.entryDetails[index].IsExemptOfTax = false;
                    $scope.entryDetails[index].ExemptOfTaxValue = 0;
                }
            } else {
                $scope.entryDetails[index].IsMainVatValue = false;
                $scope.entryDetails[index].MainVatValue = 0;
                $scope.entryDetails[index].MainVat = 0;

                $scope.entryDetails[index].IsZeroVatValue = false;
                $scope.entryDetails[index].ZeroVatValue = 0;

                $scope.entryDetails[index].IsExemptOfTax = false;
                $scope.entryDetails[index].ExemptOfTaxValue = 0;

            }





            $scope.calculateTotals();

            if ($scope.entryDetails[index].Taxable == true) {
                $scope.addTaxAccountForEachAccount(index);
            }
            //if (index == 0) {
            //    $scope.addTaxAccount();
            //}

        } else if (cellName == 'GOLD_CREDIT24') {
            if (data == null || data == undefined) {
                data = 0;
            }

            if (parseInt(data) > 0) {
                $scope.entryDetails[index].ENTRY_GOLD24_DEBIT = 0;
                $scope.entryDetails[index].ENTRY_GOLD24_CREDIT = data;
            }

            $scope.calculateTotals();
        } else if (cellName == 'GOLD_DEBIT24') {
            if (data == null || data == undefined) {
                data = 0;
            }

            if (parseInt(data) > 0) {
                $scope.entryDetails[index].ENTRY_GOLD24_CREDIT = 0;
                $scope.entryDetails[index].ENTRY_GOLD24_DEBIT = data;
            }
            $scope.calculateTotals();
        } else if (cellName == 'GOLD_CREDIT22') {
            if (data == null || data == undefined) {
                data = 0;
            }

            if (parseInt(data) > 0) {
                $scope.entryDetails[index].ENTRY_GOLD22_DEBIT = 0;
                $scope.entryDetails[index].ENTRY_GOLD22_CREDIT = data;
            }
            $scope.calculateTotals();
        } else if (cellName == 'GOLD_DEBIT22') {
            if (data == null || data == undefined) {
                data = 0;
            }

            if (parseInt(data) > 0) {
                $scope.entryDetails[index].ENTRY_GOLD22_CREDIT = 0;
                $scope.entryDetails[index].ENTRY_GOLD22_DEBIT = data;
            }
            $scope.calculateTotals();
        } else if (cellName == 'GOLD_CREDIT21') {
            if (data == null || data == undefined) {
                data = 0;
            }

            if (parseInt(data) > 0) {
                $scope.entryDetails[index].ENTRY_GOLD21_DEBIT = 0;
                $scope.entryDetails[index].ENTRY_GOLD21_CREDIT = data;
            }
            $scope.calculateTotals();
        } else if (cellName == 'GOLD_DEBIT21') {
            if (data == null || data == undefined) {
                data = 0;
            }

            if (parseInt(data) > 0) {
                $scope.entryDetails[index].ENTRY_GOLD21_CREDIT = 0;
                $scope.entryDetails[index].ENTRY_GOLD21_DEBIT = data;
            }
            $scope.calculateTotals();
        } else if (cellName == 'GOLD_CREDIT18') {
            if (data == null || data == undefined) {
                data = 0;
            }

            if (parseInt(data) > 0) {
                $scope.entryDetails[index].ENTRY_GOLD18_DEBIT = 0;
                $scope.entryDetails[index].ENTRY_GOLD18_CREDIT = data;
            }

            $scope.calculateTotals();
        } else if (cellName == 'GOLD_DEBIT18') {
            if (data == null || data == undefined) {
                data = 0;
            }

            if (parseInt(data) > 0) {
                $scope.entryDetails[index].ENTRY_GOLD18_CREDIT = 0;
                $scope.entryDetails[index].ENTRY_GOLD18_DEBIT = data;
            }

            $scope.calculateTotals();
        } else if (cellName == 'CheckNumber') {
            if (data == null || data == undefined) {
                data = 0;
            }
            if (parseInt(data) > 0) {
                $scope.entryDetails[index].CheckNumber = data;
            }

        } else if (cellName == 'IsExemptOfTax') {

            if ($scope.entryDetails[index].IsExemptOfTax == true) {
                $scope.entryDetails[index].Taxable = false;
                $scope.RemoveTaxAfterUnCheckTaxable(index, $scope.entryDetails[index].TaxValue);
                $scope.entryDetails[index].TaxValue = 0;
                $scope.entryDetails[index].TaxRate = 0;




                $scope.entryDetails[index].IsMainVatValue = false;
                $scope.entryDetails[index].MainVatValue = 0;
                $scope.entryDetails[index].MainVat = 0;

                $scope.entryDetails[index].IsZeroVatValue = false;
                $scope.entryDetails[index].ZeroVatValue = 0;

                $scope.entryDetails[index].IsExemptOfTax = true;
                $scope.entryDetails[index].ExemptOfTaxValue = ($scope.entryDetails[index].ENTRY_DEBIT != null ? $scope.entryDetails[index].ENTRY_DEBIT : $scope.entryDetails[index].ENTRY_CREDIT);

            }

            $scope.calculateTotals();
        }


    }


    $scope.getDetailsByEntryId = function (entryId) {
        entryService.getDetailsByEntryId(entryId).then(function (result) {
            
            if ($scope.EntryModeID == 2) {
                $scope.returnEntryDetails = result.data;


                $scope.entryDetails.splice(0, 1);
                $scope.entryDetails2.splice(0, 1);

                for (var i = 0; i < $scope.returnEntryDetails.length; i++) {
                    var gridObj = {};
                    var isCredit = false;
                    var isDepit = false;
                    // check depit
                    if ($scope.returnEntryDetails[i].ENTRY_DEBIT != undefined && $scope.returnEntryDetails[i].ENTRY_DEBIT != 0) {
                        isDepit = true;
                    } else if ($scope.returnEntryDetails[i].ENTRY_GOLD24_DEBIT != undefined && $scope.returnEntryDetails[i].ENTRY_GOLD24_DEBIT != 0) {
                        isDepit = true;
                    } else if ($scope.returnEntryDetails[i].ENTRY_GOLD22_DEBIT != undefined && $scope.returnEntryDetails[i].ENTRY_GOLD22_DEBIT != 0) {
                        isDepit = true;
                    } else if ($scope.returnEntryDetails[i].ENTRY_GOLD21_DEBIT != undefined && $scope.returnEntryDetails[i].ENTRY_GOLD21_DEBIT != 0) {
                        isDepit = true;
                    } else if ($scope.returnEntryDetails[i].ENTRY_GOLD18_DEBIT != undefined && $scope.returnEntryDetails[i].ENTRY_GOLD18_DEBIT != 0) {
                        isDepit = true;
                    }

                    //check credit
                    if ($scope.returnEntryDetails[i].ENTRY_CREDIT != undefined && $scope.returnEntryDetails[i].ENTRY_CREDIT != 0) {
                        isCredit = true;
                    } else if ($scope.returnEntryDetails[i].ENTRY_GOLD24_CREDIT != undefined && $scope.returnEntryDetails[i].ENTRY_GOLD24_CREDIT != 0) {
                        isCredit = true;
                    } else if ($scope.returnEntryDetails[i].ENTRY_GOLD22_CREDIT != undefined && $scope.returnEntryDetails[i].ENTRY_GOLD22_CREDIT != 0) {
                        isCredit = true;
                    } else if ($scope.returnEntryDetails[i].ENTRY_GOLD21_CREDIT != undefined && $scope.returnEntryDetails[i].ENTRY_GOLD21_CREDIT != 0) {
                        isCredit = true;
                    } else if ($scope.returnEntryDetails[i].ENTRY_GOLD18_CREDIT != undefined && $scope.returnEntryDetails[i].ENTRY_GOLD18_CREDIT != 0) {
                        isCredit = true;
                    }




                    if (isDepit) {

                        gridObj = $scope.returnEntryDetails[i];

                        gridObj.CheckDate = new Date($scope.returnEntryDetails[i].CheckDate);
                        gridObj.CheckIssueDate = new Date($scope.returnEntryDetails[i].CheckIssueDate);

                        if ($scope.typeOfEntry == 1 || $scope.typeOfEntry == 5 || $scope.typeOfEntry == 11) {
                            gridObj.isComplixDepit = true;
                        }
                        //if ($scope.typeOfEntry == 10) {
                        //    $scope.notification.moneyValue = $scope.entryDetails[0].ENTRY_CREDIT;
                        //    $scope.notification.gold24 = $scope.entryDetails[0].ENTRY_GOLD24_CREDIT;
                        //    $scope.notification.gold22 = $scope.entryDetails[0].ENTRY_GOLD22_CREDIT;
                        //    $scope.notification.gold21 = $scope.entryDetails[0].ENTRY_GOLD21_CREDIT;
                        //    $scope.notification.gold18 = $scope.entryDetails[0].ENTRY_GOLD18_CREDIT;
                        //}

                        //else if ($scope.typeOfEntry == 11) {
                        //    $scope.notification.moneyValue = $scope.entryDetails[0].ENTRY_DEBIT;
                        //    $scope.notification.gold24 = $scope.entryDetails[0].ENTRY_GOLD24_DEBIT;
                        //    $scope.notification.gold22 = $scope.entryDetails[0].ENTRY_GOLD22_DEBIT;
                        //    $scope.notification.gold21 = $scope.entryDetails[0].ENTRY_GOLD21_DEBIT;
                        //    $scope.notification.gold18 = $scope.entryDetails[0].ENTRY_GOLD18_DEBIT;
                        //}


                        $scope.entryDetails.push(gridObj);

                    } else if (isCredit) {


                        gridObj = $scope.returnEntryDetails[i];
                        gridObj.CheckDate = new Date($scope.returnEntryDetails[i].CheckDate);
                        gridObj.CheckIssueDate = new Date($scope.returnEntryDetails[i].CheckIssueDate);
                        gridObj.ENTRY_CREDIT = $scope.returnEntryDetails[i].ENTRY_CREDIT;

                        if ($scope.typeOfEntry == 2 || $scope.typeOfEntry == 4 || $scope.typeOfEntry == 10) {
                            gridObj.isComplixCredit = true;
                        }


                        //if ($scope.typeOfEntry == 10) {
                        //    $scope.notification.moneyValue = $scope.entryDetails2[0].ENTRY_CREDIT;
                        //    $scope.notification.gold24 = $scope.entryDetails2[0].ENTRY_GOLD24_CREDIT;
                        //    $scope.notification.gold22 = $scope.entryDetails2[0].ENTRY_GOLD22_CREDIT;
                        //    $scope.notification.gold21 = $scope.entryDetails2[0].ENTRY_GOLD21_CREDIT;
                        //    $scope.notification.gold18 = $scope.entryDetails2[0].ENTRY_GOLD18_CREDIT;
                        //}

                        //else if ($scope.typeOfEntry == 11) {
                        //    $scope.notification.moneyValue = $scope.entryDetails2[0].ENTRY_DEBIT;
                        //    $scope.notification.gold24 = $scope.entryDetails2[0].ENTRY_GOLD24_DEBIT;
                        //    $scope.notification.gold22 = $scope.entryDetails2[0].ENTRY_GOLD22_DEBIT;
                        //    $scope.notification.gold21 = $scope.entryDetails2[0].ENTRY_GOLD21_DEBIT;
                        //    $scope.notification.gold18 = $scope.entryDetails2[0].ENTRY_GOLD18_DEBIT;
                        //}


                        $scope.entryDetails2.push(gridObj);
                    }
                }
            } else {
                $scope.entryDetails = result.data;

                if ($scope.typeOfEntry == 1 || $scope.typeOfEntry == 2 || $scope.typeOfEntry == 4 || $scope.typeOfEntry == 5) {
                    
                    $scope.EntryContent = result.data;
                    var Entries = $.grep(result.data, function (element, index) {
                        return element.ACC_ID != $scope.entryMaster.ACC_ID && element.ACC_ID != $scope.TaxAccountId;
                    })
                    $scope.entryDetails = Entries;
                } else if ($scope.typeOfEntry == 10) {
                    $scope.notification.moneyValue = $scope.entryDetails[0].ENTRY_CREDIT;
                    $scope.notification.gold24 = $scope.entryDetails[0].ENTRY_GOLD24_CREDIT;
                    $scope.notification.gold22 = $scope.entryDetails[0].ENTRY_GOLD22_CREDIT;
                    $scope.notification.gold21 = $scope.entryDetails[0].ENTRY_GOLD21_CREDIT;
                    $scope.notification.gold18 = $scope.entryDetails[0].ENTRY_GOLD18_CREDIT;
                } else if ($scope.typeOfEntry == 11) {
                    $scope.notification.moneyValue = $scope.entryDetails[0].ENTRY_DEBIT;
                    $scope.notification.gold24 = $scope.entryDetails[0].ENTRY_GOLD24_DEBIT;
                    $scope.notification.gold22 = $scope.entryDetails[0].ENTRY_GOLD22_DEBIT;
                    $scope.notification.gold21 = $scope.entryDetails[0].ENTRY_GOLD21_DEBIT;
                    $scope.notification.gold18 = $scope.entryDetails[0].ENTRY_GOLD18_DEBIT;
                }

            }
            // alert(result.data);
            $scope.calculateTotals();

            $scope.entryObj = {
                ACC_ID: null,
                ACC_CODE: "",
                ACC_AR_NAME: "",
                ENTRY_CREDIT: 0,
                ENTRY_GOLD_DEBIT: 0,
                ENTRY_GOLD_CREDIT: 0,
                ENTRY_DEBIT: 0,
                ENTRY_DETAILS_REMARKS: "",
                COST_CENTER_ID: 0
            }
            $scope.entryDetails.push($scope.entryObj);
            $scope.entryDetails2.push($scope.entryObj);

        });
    }


    $scope.dirEnity = function (entity) {

       
        $scope.selectedItems = [];

        $scope.entryDetails = [];
        $scope.entryDetails2 = [];

        $scope.entryObj = {
            ACC_ID: null,
            ACC_CODE: "",
            ACC_AR_NAME: "",
            ENTRY_CREDIT: 0,
            ENTRY_GOLD_DEBIT: 0,
            ENTRY_GOLD_CREDIT: 0,
            ENTRY_DEBIT: 0,
            ENTRY_DETAILS_REMARKS: "",
            COST_CENTER_ID: 0,
            CustAccID: null,
            EntryVal: 0
        }
        $scope.entryDetails.push($scope.entryObj);
        $scope.entryDetails2.push($scope.entryObj);

        debugger;
        entity.ENTRY_DATE = new Date(entity.ENTRY_DATE);
        entity.BILL_NUMBER = Number(entity.BILL_NUMBER);
        entity.BILL_PAY_WAY = Number(entity.BILL_PAY_WAY);
        entity.CHECK_DATE = new Date(entity.CHECK_DATE);
        entity.COLLECT_ENTRY_CODE = Number(entity.COLLECT_ENTRY_CODE);
        entity.ENTRY_NUMBER = Number(entity.ENTRY_NUMBER);
        entity.TaxNumber = Number(entity.TaxNumber);
        $scope.entryMaster = entity;


        $scope.operation = "Update";

        $scope.isEditReasonshow = true;

        $scope.getDetailsByEntryId(entity.ENTRY_ID); //.then(function (result) {

        var entryType = $scope.getBillTypeQueryString();

        if (entryType != 3) {
            $scope.getDailyEntryByMasterID(entity.ENTRY_ID);
        }
        //var length = $scope.entryDetails.length;
        //$scope.entryDetails.splice(parseInt(length) + 1, 1);
        //   $scope.calculateTotals();
        //});
        // alert(x);

        //Get report data
        $scope.GetReportData();
        $scope.GetReportReportTotalsTafqeet();



    }
    $scope.deleteEnity = function (entity) {
        swal({
            title: 'هل تريد حذف السند؟',
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
            if (entity != null && entity != undefined) {
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

    $scope.getUserType = function () {
        $scope.allBillsHideStatus = false;

        sharedService.getUserType().then(function (response) {
            var type = response.data;
            if (type == 'Cachier') {
                $scope.allBillsHideStatus = true;
            }
        });
    }

    $scope.changeClass = function (c) {
        if (c == 'collapse') {
            $scope.class = "";
        } else {
            $scope.class = "collapse";
        }
    }

    $scope.GetBillSetting = function () {
        var entryType = $scope.getBillTypeQueryString();
        var userID = authService.GetUserID();
        entrySettingService.getByID(entryType).then(function (result) {

            if (result.data != undefined && result.data != null) {
                var billSetting = result.data;
                $scope.entryMaster.SELL_TYPE_ID = billSetting.BILL_SELL_TYPE_ID;
                $scope.entryMaster.BILL_PAY_WAY = billSetting.BILL_PAY_TYPE;
                //$scope.entryMaster.CUST_ACC_ID = billSetting.
                $scope.entryMaster.EMP_ID = userID;
                $scope.entryMaster.CURRENCY_ID = billSetting.CURRENCY_ID;
                $scope.entryMaster.CURRENCY_RATE = billSetting.CURRENCY_RATE;

                if (entryType == 11 || entryType == 12) {
                    $scope.entryMaster.COM_STORE_ID = billSetting.TO_COM_STORE_ID;
                } else {
                    $scope.entryMaster.COM_STORE_ID = billSetting.COM_STORE_ID
                }
            }

        });
    };
    $scope.flagHide = false;
    $scope.getAllOnLoad = function () {
        //alert($scope.popUpData);
        //alert($scope.popUpData.entrySettingId);
        //alert($scope.popUpData.billID);

        $scope.settingId = $location.search().entryType;

        if ($scope.popUpData != undefined && $scope.popUpData != null) {
            $scope.settingId = 130;
            // if ($scope.popUpData.billID != undefined && $scope.popUpData.billID != null) {
            $scope.isPopUp = true;
            // }
        } else {
            $scope.isPopUp = false;
        }

        $q.all(
                [
                    $scope.$broadcast("sticky", {}),
                    $scope.getCurrentDate(),
                    $scope.GetBillSetting(),
                    $scope.getUserType(),
                    $scope.getEntryType(),
                    $scope.getentryList(),
                    $scope.getentryTotalCount(),
                    $scope.getAllMainItemGroupsList(),
                    $scope.getAllTpayTypes(),
                    $scope.getAllSellsTypes(),
                    $scope.getAllCompanyStores(),
                    $scope.GetAllEmployeesSales(),
                    //  $scope.addGridBillDetailsItem(),
                    $scope.getEntryNumber(),
                    $scope.getAllCurrencies(),
                    $scope.getCostCentersList(),

                    $scope.getSettingById(),

                    $scope.getGridColumnsCheck(),
                    $scope.getGridData(),
                    $scope.getCustomerAccountsForComplexEntry(),
                    $scope.getUserModel()
                    //           $scope.itemModalShowStatus = false,
                    //$timeout(function () {
                    //    eventFire(document.getElementById('closeItemModal'), 'click');
                    //})

                ])
            .then(function (allResponses) {

                //var flag = !$scope.getByUserModel(8);

               // if (flag) {
                    $scope.flagHide = false
              //  }
                //if all the requests succeeded, this will be called, and $q.all will get an
                //array of all their responses.
                //  console.log(allResponses[0].data);
                if ($scope.popUpData != undefined && $scope.popUpData != null) {

                    //  $scope.isPopUp = true;
                    if ($scope.popUpData.billID != undefined && $scope.popUpData.billID != null) {
                        $scope.getEntryByBill($scope.popUpData.billID);
                    } else if ($scope.popUpData.AssetOperationId != undefined && $scope.popUpData.AssetOperationId != null) {
                        $scope.getEntryByAssetOperation($scope.popUpData.AssetOperationId);
                    } else if ($scope.popUpData.dailyEntryID != undefined && $scope.popUpData.dailyEntryID != null) {
                        $scope.loadDailyEntry($scope.popUpData.dailyEntryID);
                    }


                }

                var urlParams = $location.search();
                if (urlParams.foo != null && urlParams.foo != undefined && urlParams.foo != "") {

                    entryService.getEntryByEntryID(parseInt(urlParams.foo)).then(function (results) {

                        $scope.entryMaster = results.data.EntryMaster;ب
                        $scope.dirEnity(results.data.EntryMaster);

                    })
                }

                $scope.GetReportData();
            }, function (error) {

                //This will be called if $q.all finds any of the requests erroring.
                var abc = error;
                var def = 99;
            });
    }


    $scope.getAllMainItemGroupsList = function () {
        PointOfSaleService.getAllMainItemGroups().then(function (response) {
            $scope.mainItemsGroupsList = response.data;
        })
    }


    //GetAllTpayTypes
    $scope.getAllTpayTypes = function () {
        $scope.tpayTypes = [];
        PointOfSaleService.getAllTPayTypes().then(function (response) {
            $scope.tpayTypes = response.data;
        })
    }

    //GetAllSellsTypes
    $scope.getAllSellsTypes = function () {
        $scope.SellsTypes = [];
        PointOfSaleService.getAllSELLS_TYPES().then(function (response) {
            $scope.SellsTypes = response.data;
        })
    }

    //GetAllCompanyStores
    $scope.getAllCompanyStores = function () {
        $scope.CompanyStores = [];
        PointOfSaleService.getAllCompanyStores().then(function (response) {

            $scope.CompanyStores = response.data;
        })
    }


    //GetAllBoxAccounts
    $scope.GetAllBoxAccounts = function () {

        //typeOfEntry
        $scope.BoxAccounts = [];

        if ($scope.typeOfEntry == 1 || $scope.typeOfEntry == 2 || $scope.typeOfEntry == 4 || $scope.typeOfEntry == 5) {
            accountsService.getAccountBoxByTypesForEntry($scope.typeOfEntry).then(function (response) {
                $scope.BoxAccounts = response.data;
                //$scope.entryMaster.ACC_ID = "";

            });
        } else {
            accountsService.getAccountBoxByTypesForEntry(0).then(function (response) {
                $scope.BoxAccounts = response.data;
                //$scope.entryMaster.ACC_ID = "";

            });
        }

    }

    //GetAllGoldBoxAccounts
    $scope.GetAllGoldBoxAccounts = function () {

        var entrytype = $scope.getBillTypeQueryString();
        $scope.GoldBoxAccounts = [];
        PointOfSaleService.getAllGoldBoxAccounts().then(function (response) {
            
            var returnedCustomerAccounts = response.data;
            if (entrytype == 1 || entrytype == 3 || entrytype == 5 || entrytype == 7) {
                $scope.CustomersAccounts = [];
                for (var i = 0; i < returnedCustomerAccounts.length; i++) {
                    if (returnedCustomerAccounts[i].ACC_TYPE_ID == 13) {
                        $scope.GoldBoxAccounts.push(returnedCustomerAccounts[i]);
                    }
                }

            } else if (entrytype == 2 || entrytype == 4 || entrytype == 6 || entrytype == 8) {
                $scope.GoldBoxAccounts = [];
                for (var i = 0; i < returnedCustomerAccounts.length; i++) {
                    if (returnedCustomerAccounts[i].ACC_TYPE_ID == 13) {
                        $scope.GoldBoxAccounts.push(returnedCustomerAccounts[i]);
                    }
                }
            }
        })
    }


    //GetAllEmployeesSales
    $scope.GetAllEmployeesSales = function () {
        $scope.EmployeesSales = [];
        PointOfSaleService.getAllEmployeeOfTypeSales().then(function (response) {

            $scope.EmployeesSales = response.data;
        })
    }

    $scope.addGridBillDetailsItem = function () {
        var found = false;
        for (var i = 0; i < $scope.selectedItems.length; i++) {
            if ($scope.selectedItems[i].ITEM_CODE == null || $scope.selectedItems[i].ITEM_CODE == "") {

                found = true;
            }
        }
        if (!found) {
            $scope.gridBillDetailsItem = {
                ITEM_CODE: "",
                ITEM_AR_NAME: "",
                Discount: "",
                DISC_RATE: "",
                Quantity: "",
                Unit: null,
                SubjectToVAT: null,
                VATRate: null,
                CaliberID: null,
                ARName: null,
                LatName: null,
                ClearnessRate: null,
                Price: "",
                Total: "",
                GridRowNumber: "",
                UnitNameAr: "",
                CaliberNameAr: "",
                Caliber18: null,
                Caliber21: null,
                Caliber24: null
            };
            //$scope.selectedItems.splice($scope.selectedItems.length - 1, 0, $scope.gridBillDetailsItem);
            $scope.selectedItems.push($scope.gridBillDetailsItem);
            //$scope.calculateTotals();
        }
    };

    ////////////////get last code
    $scope.getEntryNumber = function () {
        var urlParams = $location.search();
        if (urlParams.foo != null && urlParams.foo != undefined && urlParams.foo != "") {} else {
            entryService.GetLastEntryNumber($scope.settingId).then(function (response) {
                var result = response.data;
                $scope.entryMaster.ENTRY_NUMBER = result + 1;
            })
        }
    }

    $scope.getentryTotalCount = function () {

        $scope.count().then(function (results) {
            var data = results.data;
            $scope.entryTotalCount = data;
            $scope.pagesCount = $scope.getPagesCount();
        }, function (error) {
            console.log(error.data.message);
        });
    }
    $scope.getentryList = function () {

        var entryType = $scope.getBillTypeQueryString();
        $scope.get(entryType, $scope.pageNum, $scope.pageSize).then(function (results) {
            var data = results.data;
            $scope.entryMasters = data;


        }, function (error) {
            console.log(error.data.message);
        });
    }

    $scope.get = function (entryType, pageNum, pageSize) {
        return entryService.get(entryType, pageNum, pageSize);
    }
    $scope.count = function () {
        var entryType = $scope.getBillTypeQueryString();
        return entryService.count(entryType);
    }
    $scope.add = function (entity) {
        return entryService.add(entity);
    }
    $scope.addDetails = function (Details) {
        return entryService.addBillDetails(Details);
    }
    $scope.update = function (entity) {
        return entryService.update(entity);
    }
    $scope.delete = function (entity) {
        return entryService.delete(entity);
    }
    $scope.entryPageLoad = function () {

        $scope.getAllOnLoad();
    }


    $scope.pageChanged = function () {
        $scope.getAllOnLoad();
        $log.log('Page changed to: ' + $scope.pageNum);
    };

    //Modal
    $scope.animationsEnabled = true;

    $scope.open = function () {
        var parentElem = angular.element($document[0].querySelector('.app-content'));
        $scope.modalOpenItm = false;
        var modalInstance = $uibModal.open({
            templateUrl: 'myModalContent.html',
            controller: 'modalCtrl',
            scope: $scope,
            preserveScope: true
        });
        modalInstance.opened.then(function () {
            $scope.modalOpenItm = true;
        });

        // we want to update state whether the modal closed or was dismissed,
        // so use finally to handle both resolved and rejected promises.
        modalInstance.result.finally(function (selectedItem) {
            $scope.modalOpenItm = false;
        });
    };

    //$scope.billDiscountTypes = [{ billDiscountTypeID: 1, billDiscountTypeName: "خصم بالقيمة" }, { billDiscountTypeID: 2, billDiscountTypeID: "خصم بالنسبة" }];

    $scope.getAllCurrencies = function () {

        entryService.getAllCurrencies().then(function (response) {

            var result = response.data;
            $scope.currenciesList = result;
        })
    }
    $scope.changeCurrencyRate = function () {
        var currencyID = $scope.entryMaster.CURRENCY_ID;
        if ($scope.currenciesList != undefined || $scope.currenciesList != null) {
            for (var i = 0; i < $scope.currenciesList.length; i++) {
                if ($scope.currenciesList[i].CURRENCY_ID == currencyID) {
                    $scope.entryMaster.CURRENCY_RATE = $scope.currenciesList[i].CURRENCY_RATE;
                    $scope.entryMaster.currencyName = $scope.currenciesList[i].CURRENCY_AR_NAME;
                }
            }
        }
        $scope.calculateTotals();
    }

    $scope.getCurrencyName = function () {
        var currencyID = $scope.entryMaster.CURRENCY_ID;
        if ($scope.currenciesList != undefined || $scope.currenciesList != null) {
            for (var i = 0; i < $scope.currenciesList.length; i++) {
                if ($scope.currenciesList[i].CURRENCY_ID == currencyID) {

                    $scope.entryMaster.currencyName = $scope.currenciesList[i].CURRENCY_AR_NAME;
                    return $scope.entryMaster.currencyName;
                }
            }
        }
    }


    $scope.getBillTypeQueryString = function () {

        if ($location.search().entryType != undefined) {
            $scope.entryType = $location.search().entryType;
        } else {
            $scope.entryType = $scope.popUpData.entrySettingId;
        }
        return $scope.entryType;
    }

    $scope.getentryTypeName = function () {
        var entryType = $scope.getBillTypeQueryString();
        if (entryType == 1) {
            return 'سند قبض نقدي';
        } else if (entryType == 2) {
            return 'سند صرف نقدي';
        } else if (entryType == 3) {
            return 'قــيد اليـومية';
        } else if (entryType == 4) {
            return 'سند صرف شيك';
        } else if (entryType == 5) {
            return 'سند قبض شيك';
        } else if (entryType == 6) {
            return 'يومية المبيعات';
        } else if (entryType == 7) {
            return 'يومية المشتريات';
        } else if (entryType == 8) {
            return 'يومية مرتجع مبيعات';
        } else if (entryType == 9) {
            return 'يومية مرتجع مشتريات';
        } else if (entryType == 10) {
            return 'مصروفات';
        } else if (entryType == 11) {
            return ' دفعات موردين';
        } else if (entryType == 12) {
            return 'دفعات عملاء';
        }
    }


    $scope.getCurrentDate = function () {
        // alert('Entry');
        var date = new Date();
        $scope.entryMaster.ENTRY_DATE = date;
        $scope.entryMaster.CHECK_DATE = date;
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
            entryService.searchAccountsForDepitGrid($scope.searchBy, $scope.typeOfEntry).then(function (response) {
                $scope.searchItems = response.data;
                if ($scope.searchItems.length == 1) {

                    $scope.checkExist($scope.searchItems[0].ACC_CODE, $scope.searchItems[0]);

                } else if ($scope.searchItems.length == 0) {
                    $scope.searchItems = [];
                } else {
                    $scope.checkSelected();
                    if ($scope.modalOpenItm === false || $scope.modalOpenItm == undefined) {
                        $scope.open();
                    }
                }
            })
        }
    }


    $scope.selectedList = [];
    //check selected accounts
    $scope.checkSelected = function () {
        if ($scope.searchBy != null || $scope.searchBy != undefined) {
            for (var i = 0; i < $scope.searchItems.length; i++) {
                if ($scope.searchItems[i].Selected == true) {
                    $scope.checkExist($scope.searchItems[i].ACC_CODE, $scope.searchItems[i]);
                    $scope.selectedList.push($scope.searchItems[i]);
                }

            }
        }
    }


    //check if account already exist on grid if not add it
    $scope.checkExist = function (accountcode, item) {
        var found = false;
        var emptyIndex = 0;
        for (var i = 0; i < $scope.entryDetails.length; i++) {
            if (String($scope.entryDetails[i].ACC_CODE) == accountcode) {
                found = true;
            }

            if ($scope.entryDetails[i].ACC_CODE == "") {
                // $scope.entryDetails.splice(i, 1);
                emptyIndex = i;
            }
        }
        if (!found) {
            $scope.addAccountToGrid(item, emptyIndex);
        } else {
            if ($scope.IsRepeatItem) {
                $scope.addAccountToGrid(item, emptyIndex);

            }
        }
    }

    /////add account to grid 
    $scope.addAccountToGrid = function (item, emptyIndex) {
        
        $scope.entryDetails.splice(emptyIndex, 1);
        $scope.entryDetailsEntity = {
            ACC_ID: null,
            ACC_CODE: "",
            ACC_AR_NAME: "",
            ENTRY_CREDIT: 0,
            ENTRY_GOLD_DEBIT: 0,
            ENTRY_GOLD_CREDIT: 0,
            ENTRY_DEBIT: 0,
            ENTRY_DETAILS_REMARKS: "",
            COST_CENTER_ID: 0,
            TaxValue: 0,
            Taxable: false
        };
        $scope.entryDetailsEntity.ACC_ID = item.ACC_ID;
        $scope.entryDetailsEntity.ACC_CODE = item.ACC_CODE;
        $scope.entryDetailsEntity.ACC_AR_NAME = item.ACC_AR_NAME;

        $scope.entryDetailsEntity.TaxAccountID = item.TaxAccountID;

        $scope.entryDetailsEntity.Taxable = item.SubjectToVAT;
        if (item.SubjectToVAT == true) {
            $scope.entryDetailsEntity.TaxRate = item.VATRate;
        } else {
            $scope.entryDetailsEntity.TaxRate = 0;
        }


        $scope.entryDetailsEntity.ENTRY_CREDIT = item.ENTRY_CREDIT;
        $scope.entryDetailsEntity.ENTRY_GOLD24_CREDIT = item.ENTRY_GOLD24_CREDIT;
        $scope.entryDetailsEntity.ENTRY_GOLD22_CREDIT = item.ENTRY_GOLD22_CREDIT;
        $scope.entryDetailsEntity.ENTRY_GOLD21_CREDIT = item.ENTRY_GOLD21_CREDIT;
        $scope.entryDetailsEntity.ENTRY_GOLD18_CREDIT = item.ENTRY_GOLD18_CREDIT;

        $scope.entryDetailsEntity.ENTRY_DEBIT = item.ENTRY_DEBIT;
        $scope.entryDetailsEntity.ENTRY_GOLD24_DEBIT = item.ENTRY_GOLD24_DEBIT;
        $scope.entryDetailsEntity.ENTRY_GOLD22_DEBIT = item.ENTRY_GOLD22_DEBIT;
        $scope.entryDetailsEntity.ENTRY_GOLD21_DEBIT = item.ENTRY_GOLD21_DEBIT;
        $scope.entryDetailsEntity.ENTRY_GOLD18_DEBIT = item.ENTRY_GOLD18_DEBIT;

        $scope.entryDetails.push($scope.entryDetailsEntity);
        $scope.entryObj = {
            ACC_ID: null,
            ACC_CODE: "",
            ACC_AR_NAME: "",
            ENTRY_CREDIT: 0,
            ENTRY_GOLD_DEBIT: 0,
            ENTRY_GOLD_CREDIT: 0,
            ENTRY_DEBIT: 0,
            ENTRY_DETAILS_REMARKS: "",
            COST_CENTER_ID: 0,
            TaxValue: 0,
            Taxable: false
        }
        $scope.entryDetails.push($scope.entryObj);
        $scope.calculateTotals();
    }

    //////////serach for accounts by code and name
    $scope.searchCodeName = function (code) {
        if (code != null && code != undefined) {

            $scope.searchBy = code;
            $scope.searchForAccounts();
        }
    }

    $scope.getCostCenter = function (pageNum, pageSize) {
        return costService.getMainCostCenters(pageNum, pageSize);
    }

    //get cost center List
    $scope.getCostCentersList = function () {
        $scope.getCostCenter().then(function (results) {
            var data = results.data;
            $scope.costCentersList = data;
        }, function (error) {
            console.log(error.data.message);
        });
    }


    //calculate totals
    $scope.calculateTotals = function () {
        $scope.totalCredit = 0;
        $scope.totalDepit = 0;
        $scope.totalTaxRateCredit = 0;
        $scope.totalTaxRateDebit = 0;

        $scope.totalTaxValueCredit = 0;
        $scope.totalTaxValueDebit = 0;

        $scope.totalNotTaxCredit = 0;
        $scope.totalNotTaxDebit = 0;

        $scope.totalGoldCredit24 = 0;
        $scope.totalGoldDepit24 = 0;

        $scope.totalGoldCredit22 = 0;
        $scope.totalGoldDepit22 = 0;

        $scope.totalGoldCredit21 = 0;
        $scope.totalGoldDepit21 = 0;

        $scope.totalGoldCredit18 = 0;
        $scope.totalGoldDepit18 = 0;

        $scope.totalCurrencyCredit = 0;
        $scope.totalCurrenctDepit = 0;

        $scope.totalCurGoldCredit24 = 0;
        $scope.totalCurGoldDepit24 = 0;


        $scope.totalCurGoldCredit22 = 0;
        $scope.totalCurGoldDepit22 = 0;


        $scope.totalCurGoldCredit21 = 0;
        $scope.totalCurGoldDepit21 = 0;


        $scope.totalCurGoldCredit18 = 0;
        $scope.totalCurGoldDepit18 = 0;


        $scope.totalCreditWithTax = 0;
        $scope.totalDepitWithTax = 0;


        $scope.creditTaxIncludedBefore = 0;
        $scope.totalCreditTaxIncludedBefore = 0;
        $scope.depitTaxIncludedBefore = 0;
        $scope.totalDepitTaxIncludedBefore = 0;


        $scope.cridetTotalExemptOfTax = 0;
        $scope.cridetExemptOfTaxValue = 0;

        $scope.depitTotalExemptOfTax = 0;
        $scope.depitExemptOfTaxValue = 0;


        $scope.cridetTotalMainTax = 0;
        $scope.cridetMainTaxValue = 0;

        $scope.depitTotalMainTax = 0;
        $scope.depitMainTaxValue = 0;


        $scope.cridetTotalZeroTax = 0;
        $scope.cridetZeroTaxValue = 0;

        $scope.depitTotalZeroTax = 0;
        $scope.depitZeroTaxValue = 0;


        for (var i = 0; i < $scope.entryDetails.length; i++) {
            if ($scope.entryDetails[i].ACC_ID != null && $scope.entryDetails[i].ACC_ID != "" && $scope.entryDetails[i].ACC_ID != undefined) {

                if ($scope.entryDetails[i].ENTRY_CREDIT != null && $scope.entryDetails[i].ENTRY_CREDIT != undefined && $scope.entryDetails[i].ENTRY_CREDIT != 0) {

                    //exculde tax amount of complix entry
                    //if (($scope.EntryModeID == 2 && ($scope.typeOfEntry == 2 || $scope.typeOfEntry == 4) && $scope.entryDetails[i].ACC_ID != $scope.TaxAccountId)) {
                    //   $scope.totalCredit = (parseFloat($scope.entryDetails[i].ENTRY_CREDIT) + parseFloat($scope.totalCredit));
                    //}
                    //else if ($scope.EntryModeID != 2) {
                    $scope.totalCredit = (parseFloat($scope.entryDetails[i].ENTRY_CREDIT) + parseFloat($scope.totalCredit));
                    //  }
                }

                if ($scope.entryDetails[i].ENTRY_DEBIT != null && $scope.entryDetails[i].ENTRY_DEBIT != undefined && $scope.entryDetails[i].ENTRY_DEBIT != 0) {

                    if ($scope.settingId == 130) {
                        $scope.totalDepit = (parseFloat($scope.entryDetails[i].ENTRY_DEBIT) + parseFloat($scope.totalDepit));
                    } else {
                        //exculde tax amount of complix entry
                        if (($scope.EntryModeID == 2 && ($scope.typeOfEntry == 1 || $scope.typeOfEntry == 5 || $scope.typeOfEntry == 11) && $scope.IsTaxesIncluded == false && $scope.entryDetails[i].ACC_ID != $scope.TaxAccountId)) {
                            $scope.totalDepit = (parseFloat($scope.entryDetails[i].ENTRY_DEBIT) + parseFloat($scope.totalDepit));
                        }

                        //else if (($scope.EntryModeID == 2 && ($scope.typeOfEntry != 1 && $scope.typeOfEntry != 5 || $scope.typeOfEntry == 11) && $scope.IsTaxesIncluded == true)) {
                        else if (($scope.EntryModeID == 2 && ($scope.typeOfEntry != 1 && $scope.typeOfEntry != 5 || $scope.typeOfEntry == 11))) {
                            $scope.totalDepit = (parseFloat($scope.entryDetails[i].ENTRY_DEBIT) + parseFloat($scope.totalDepit));
                        } else if ($scope.EntryModeID != 2) {
                            $scope.totalDepit = (parseFloat($scope.entryDetails[i].ENTRY_DEBIT) + parseFloat($scope.totalDepit));
                        } else {
                            $scope.totalDepit = (parseFloat($scope.entryDetails[i].ENTRY_DEBIT) + parseFloat($scope.totalDepit));
                        }
                    }
                }

                ////////////////////////////////////////////gold
                if ($scope.entryDetails[i].ENTRY_GOLD24_CREDIT != null && $scope.entryDetails[i].ENTRY_GOLD24_CREDIT != undefined && $scope.entryDetails[i].ENTRY_GOLD24_CREDIT != 0) {
                    $scope.totalGoldCredit24 = (parseFloat($scope.entryDetails[i].ENTRY_GOLD24_CREDIT) + parseFloat($scope.totalGoldCredit24));
                }

                if ($scope.entryDetails[i].ENTRY_GOLD24_DEBIT != null && $scope.entryDetails[i].ENTRY_GOLD24_DEBIT != undefined && $scope.entryDetails[i].ENTRY_GOLD24_DEBIT != 0) {
                    $scope.totalGoldDepit24 = (parseFloat($scope.entryDetails[i].ENTRY_GOLD24_DEBIT) + parseFloat($scope.totalGoldDepit24));
                }


                if ($scope.entryDetails[i].ENTRY_GOLD22_CREDIT != null && $scope.entryDetails[i].ENTRY_GOLD22_CREDIT != undefined && $scope.entryDetails[i].ENTRY_GOLD22_CREDIT != 0) {
                    $scope.totalGoldCredit22 = (parseFloat($scope.entryDetails[i].ENTRY_GOLD22_CREDIT) + parseFloat($scope.totalGoldCredit22));
                }

                if ($scope.entryDetails[i].ENTRY_GOLD22_DEBIT != null && $scope.entryDetails[i].ENTRY_GOLD22_DEBIT != undefined && $scope.entryDetails[i].ENTRY_GOLD22_DEBIT != 0) {
                    $scope.totalGoldDepit22 = (parseFloat($scope.entryDetails[i].ENTRY_GOLD22_DEBIT) + parseFloat($scope.totalGoldDepit22));
                }


                if ($scope.entryDetails[i].ENTRY_GOLD21_CREDIT != null && $scope.entryDetails[i].ENTRY_GOLD21_CREDIT != undefined && $scope.entryDetails[i].ENTRY_GOLD21_CREDIT != 0) {
                    $scope.totalGoldCredit21 = (parseFloat($scope.entryDetails[i].ENTRY_GOLD21_CREDIT) + parseFloat($scope.totalGoldCredit21));
                }

                if ($scope.entryDetails[i].ENTRY_GOLD21_DEBIT != null && $scope.entryDetails[i].ENTRY_GOLD21_DEBIT != undefined && $scope.entryDetails[i].ENTRY_GOLD21_DEBIT != 0) {
                    $scope.totalGoldDepit21 = (parseFloat($scope.entryDetails[i].ENTRY_GOLD21_DEBIT) + parseFloat($scope.totalGoldDepit21));
                }



                if ($scope.entryDetails[i].ENTRY_GOLD18_CREDIT != null && $scope.entryDetails[i].ENTRY_GOLD18_CREDIT != undefined && $scope.entryDetails[i].ENTRY_GOLD18_CREDIT != 0) {
                    $scope.totalGoldCredit18 = (parseFloat($scope.entryDetails[i].ENTRY_GOLD18_CREDIT) + parseFloat($scope.totalGoldCredit18));
                }

                if ($scope.entryDetails[i].ENTRY_GOLD18_DEBIT != null && $scope.entryDetails[i].ENTRY_GOLD18_DEBIT != undefined && $scope.entryDetails[i].ENTRY_GOLD18_DEBIT != 0) {
                    $scope.totalGoldDepit18 = (parseFloat($scope.entryDetails[i].ENTRY_GOLD18_DEBIT) + parseFloat($scope.totalGoldDepit18));
                }


                ////////////////////////////////////////////////taxable of details 

                if ($scope.entryMaster.Taxable == false || $scope.entryMaster.Taxable == null || $scope.entryMaster.Taxable == undefined) {

                    if ($scope.entryDetails[i].IsExemptOfTax == true) {

                        //للدائن في السند العادي
                        if ($scope.entryDetails[i].ENTRY_CREDIT != null && $scope.entryDetails[i].ENTRY_CREDIT != undefined && $scope.entryDetails[i].ENTRY_CREDIT != 0) {
                            $scope.cridetTotalExemptOfTax = parseFloat($scope.entryDetails[i].ENTRY_CREDIT) + parseFloat($scope.cridetTotalExemptOfTax);
                            //يوجد ضريبه للسند لحساب القيمه
                            if ($scope.entryMaster.ExemptVatRate != undefined && $scope.entryMaster.ExemptVatRate != null) {
                                $scope.cridetExemptOfTaxValue = ((parseFloat($scope.entryMaster.ExemptVatRate) * parseFloat($scope.cridetTotalExemptOfTax)) / 100).toFixed(2);
                            }
                        } else if ($scope.entryDetails[i].ENTRY_DEBIT != null && $scope.entryDetails[i].ENTRY_DEBIT != undefined && $scope.entryDetails[i].ENTRY_DEBIT != 0) {

                            $scope.depitTotalExemptOfTax = parseFloat($scope.entryDetails[i].ENTRY_DEBIT) + parseFloat($scope.depitTotalExemptOfTax);
                            //يوجد ضريبه للسند لحساب القيمه
                            if ($scope.entryMaster.ExemptVatRate != undefined && $scope.entryMaster.ExemptVatRate != null) {
                                $scope.depitExemptOfTaxValue = ((parseFloat($scope.entryMaster.ExemptVatRate) * parseFloat($scope.depitTotalExemptOfTax)) / 100).toFixed(2);
                            }
                        }


                    } else {
                        // الصنف خاضع للضريبه 
                        if ($scope.entryDetails[i].Taxable != null && $scope.entryDetails[i].Taxable != undefined && $scope.entryDetails[i].Taxable == true) {
                            if ($scope.entryDetails[i].ENTRY_CREDIT != null && $scope.entryDetails[i].ENTRY_CREDIT != undefined && $scope.entryDetails[i].ENTRY_CREDIT != 0) {

                                if ($scope.entryDetails[i].TaxRate != null && $scope.entryDetails[i].TaxRate != undefined) {
                                    $scope.totalTaxRateCredit = (parseFloat($scope.entryDetails[i].TaxRate) + parseFloat($scope.totalTaxRateCredit)).toFixed(2);
                                }

                                if ($scope.entryDetails[i].TaxValue != null && $scope.entryDetails[i].TaxValue != undefined) {
                                    $scope.totalTaxValueCredit = (parseFloat($scope.entryDetails[i].TaxValue) + parseFloat($scope.totalTaxValueCredit)).toFixed(2);
                                }



                                // الضريبه الاساسيه و ضريبه الزيرو في حاله ان السند نوعه عادي 
                                if ($scope.entryDetails[i].TaxRate == $scope.entryMaster.MainVatRate) {

                                    var beforetax = (parseFloat($scope.entryDetails[i].ENTRY_CREDIT) * (100 / (100 + parseFloat($scope.entryMaster.MainVatRate)))).toFixed(2);

                                    $scope.cridetTotalMainTax = parseFloat(beforetax) + parseFloat($scope.cridetTotalMainTax);
                                    //يوجد ضريبه للسند لحساب القيمه
                                    if ($scope.entryMaster.MainVatRate != undefined && $scope.entryMaster.MainVatRate != null) {
                                        $scope.cridetMainTaxValue = ((parseFloat($scope.entryMaster.MainVatRate) * parseFloat($scope.cridetTotalMainTax)) / 100).toFixed(2);
                                    }

                                    // $scope.cridetTotalMainTax = $scope.cridetTotalMainTax - $scope.cridetMainTaxValue;
                                }


                                // الضريبه الاساسيه و ضريبه الزيرو في حاله ان السند نوعه عادي 
                                if ($scope.entryDetails[i].TaxRate == 0 || $scope.entryDetails[i].TaxRate == undefined) {
                                    $scope.cridetTotalZeroTax = parseFloat($scope.entryDetails[i].ENTRY_CREDIT) + parseFloat($scope.cridetTotalZeroTax);
                                    $scope.cridetZeroTaxValue = 0;
                                    $scope.EntryMaster.ZeroVatRate = 0;
                                }



                            } else if ($scope.entryDetails[i].ENTRY_DEBIT != null && $scope.entryDetails[i].ENTRY_DEBIT != undefined && $scope.entryDetails[i].ENTRY_DEBIT != 0) {
                                if ($scope.entryDetails[i].TaxRate != null && $scope.entryDetails[i].TaxRate != undefined) {
                                    $scope.totalTaxRateDebit = (parseFloat($scope.entryDetails[i].TaxRate) + parseFloat($scope.totalTaxRateDebit)).toFixed(2);
                                }


                                if ($scope.entryDetails[i].TaxValue != null && $scope.entryDetails[i].TaxValue != undefined) {
                                    $scope.totalTaxValueDebit = (parseFloat($scope.entryDetails[i].TaxValue) + parseFloat($scope.totalTaxValueDebit)).toFixed(2);
                                }


                                // الضريبه الاساسيه و ضريبه الزيرو في حاله ان السند نوعه عادي 
                                if ($scope.entryDetails[i].TaxRate == $scope.entryMaster.MainVatRate) {

                                    var beforetax = (parseFloat($scope.entryDetails[i].ENTRY_DEBIT) * (100 / (100 + parseFloat($scope.entryMaster.MainVatRate)))).toFixed(2);


                                    $scope.depitTotalMainTax = parseFloat(beforetax) + parseFloat($scope.depitTotalMainTax);
                                    //يوجد ضريبه للسند لحساب القيمه
                                    if ($scope.entryMaster.MainVatRate != undefined && $scope.entryMaster.MainVatRate != null) {
                                        $scope.depitMainTaxValue = ((parseFloat($scope.entryMaster.MainVatRate) * parseFloat($scope.depitTotalMainTax)) / 100).toFixed(2);
                                    }

                                    //   $scope.depitTotalMainTax = $scope.depitTotalMainTax - $scope.depitMainTaxValue;
                                }

                                if ($scope.entryDetails[i].TaxRate == 0 || $scope.entryDetails[i].TaxRate == undefined) {
                                    $scope.depitTotalZeroTax = parseFloat($scope.entryDetails[i].ENTRY_DEBIT) + parseFloat($scope.depitTotalZeroTax);
                                    $scope.depitZeroTaxValue = 0;
                                }


                            }
                        }
                        //الغير خاضع للضريبه
                        else {
                            if ($scope.entryDetails[i].ENTRY_CREDIT != null && $scope.entryDetails[i].ENTRY_CREDIT != undefined && $scope.entryDetails[i].ENTRY_CREDIT != 0) {
                                {
                                    $scope.totalNotTaxCredit = (parseFloat($scope.entryDetails[i].ENTRY_CREDIT) + parseFloat($scope.totalNotTaxCredit));
                                }
                            } else if ($scope.entryDetails[i].ENTRY_DEBIT != null && $scope.entryDetails[i].ENTRY_DEBIT != undefined && $scope.entryDetails[i].ENTRY_DEBIT != 0) {
                                {
                                    //exculde tax amount of complix entry
                                    //if (($scope.EntryModeID == 2 && ($scope.typeOfEntry == 1 || $scope.typeOfEntry == 5) && $scope.entryDetails[i].ACC_ID != $scope.TaxAccountId)) {
                                    //    $scope.totalNotTaxDebit = (parseFloat($scope.entryDetails[i].ENTRY_DEBIT) + parseFloat($scope.totalNotTaxDebit));
                                    //}
                                    //else
                                    //{
                                    $scope.totalNotTaxDebit = (parseFloat($scope.entryDetails[i].ENTRY_DEBIT) + parseFloat($scope.totalNotTaxDebit));
                                    // }
                                }
                            }
                        }
                    }




                }


                if ($scope.entryMaster.Taxable == true) {
                    if ($scope.entryTaxRate != null && $scope.entryTaxRate != undefined) {
                        //if amount includes tax
                        if ($scope.IsTaxesIncluded) {

                            if ($scope.entryDetails[i].ENTRY_CREDIT != null && $scope.entryDetails[i].ENTRY_CREDIT != undefined && $scope.entryDetails[i].ENTRY_CREDIT != 0) {

                                //exculde tax amount of complix entry
                                // if (($scope.EntryModeID == 2 && ($scope.typeOfEntry == 2 || $scope.typeOfEntry == 4) && $scope.entryDetails[i].ACC_ID != $scope.TaxAccountId)) {
                                $scope.creditTaxIncludedBefore = (parseFloat($scope.entryDetails[i].ENTRY_CREDIT) * (100 / (100 + parseFloat($scope.entryTaxRate)))).toFixed(2);
                                // }
                                $scope.totalCreditTaxIncludedBefore = (parseFloat($scope.creditTaxIncludedBefore) + parseFloat($scope.totalCreditTaxIncludedBefore));
                            }

                            if ($scope.entryDetails[i].ENTRY_DEBIT != null && $scope.entryDetails[i].ENTRY_DEBIT != undefined && $scope.entryDetails[i].ENTRY_DEBIT != 0) {

                                //exculde tax amount of complix entry
                                //if (($scope.EntryModeID == 2 && ($scope.typeOfEntry == 1 || $scope.typeOfEntry == 5) && $scope.entryDetails[i].ACC_ID != $scope.TaxAccountId)) {
                                //    $scope.depitTaxIncludedBefore = (parseFloat($scope.entryDetails[i].ENTRY_DEBIT) * (100 / (100 + parseFloat($scope.entryTaxRate)))).toFixed(2);
                                //}

                                //else {
                                $scope.depitTaxIncludedBefore = (parseFloat($scope.entryDetails[i].ENTRY_DEBIT) * (100 / (100 + parseFloat($scope.entryTaxRate)))).toFixed(2);
                                // }

                                $scope.totalDepitTaxIncludedBefore = (parseFloat($scope.depitTaxIncludedBefore) + parseFloat($scope.totalDepitTaxIncludedBefore));
                            }

                            $scope.totalCreditWithTax = parseFloat($scope.totalCreditTaxIncludedBefore).toFixed(2);
                            $scope.totalDepitWithTax = parseFloat($scope.totalDepitTaxIncludedBefore).toFixed(2);
                            $scope.totalNotTaxCredit = 0;
                            $scope.totalNotTaxDebit = 0;
                        } else {

                            $scope.totalCreditWithTax = (parseFloat($scope.totalCredit)).toFixed(2);
                            $scope.totalDepitWithTax = (parseFloat($scope.totalDepit)).toFixed(2);

                            $scope.totalNotTaxCredit = 0;
                            $scope.totalNotTaxDebit = 0;
                        }
                    }
                } else {
                    $scope.totalCreditWithTax = 0;
                    $scope.totalDepitWithTax = 0;
                    //$scope.totalNotTaxCredit = 0;
                    //$scope.totalNotTaxDebit = 0;
                }
            }
        }


        if ($scope.EntryModeID == 2) {
            $scope.calculateTotalsGrid2();

            $scope.totalCredit = parseFloat($scope.totalCredit) + parseFloat($scope.totalCreditGrd2);

            $scope.totalTaxRateCredit = parseFloat($scope.totalTaxRateCredit) + parseFloat($scope.totalTaxRateCreditGrd2);

            $scope.totalTaxValueCredit = parseFloat($scope.totalTaxValueCredit) + parseFloat($scope.totalTaxValueCreditGrd2);

            $scope.totalNotTaxCredit = parseFloat($scope.totalNotTaxCredit) + parseFloat($scope.totalNotTaxCreditGrd2);

            $scope.totalGoldCredit24 = parseFloat($scope.totalGoldCredit24) + parseFloat($scope.totalGoldCredit24Grd2);

            $scope.totalGoldCredit22 = parseFloat($scope.totalGoldCredit22) + parseFloat($scope.totalGoldCredit22Grd2);

            $scope.totalGoldCredit21 = parseFloat($scope.totalGoldCredit21) + parseFloat($scope.totalGoldCredit21Grd2);

            $scope.totalGoldCredit18 = parseFloat($scope.totalGoldCredit18) + parseFloat($scope.totalGoldCredit18Grd2);

            $scope.totalCreditWithTax = parseFloat($scope.totalCreditWithTax) + parseFloat($scope.totalCreditWithTaxGrd2);


            ////////////ضرائب
            $scope.cridetTotalExemptOfTax = parseFloat($scope.cridetTotalExemptOfTax) + parseFloat($scope.cridetTotalExemptOfTax2);
            $scope.cridetExemptOfTaxValue = parseFloat($scope.cridetExemptOfTaxValue) + parseFloat($scope.cridetExemptOfTaxValue2);

            $scope.cridetTotalMainTax = parseFloat($scope.cridetTotalMainTax) + parseFloat($scope.cridetTotalMainTax2);
            $scope.cridetMainTaxValue = parseFloat($scope.cridetMainTaxValue) + parseFloat($scope.cridetMainTaxValue2);

            $scope.cridetTotalZeroTax = parseFloat($scope.cridetTotalZeroTax) + parseFloat($scope.cridetTotalZeroTax2);
            $scope.cridetZeroTaxValue = parseFloat($scope.cridetZeroTaxValue) + parseFloat($scope.cridetZeroTaxValue2);


        }

        $scope.entryMaster.TotalCredit = Number(parseFloat($scope.totalCredit).toFixed(2));
        $scope.entryMaster.TotalDepit = Number(parseFloat($scope.totalDepit).toFixed(2));
        $scope.entryMaster.TotalGoldCredit24 = Number(parseFloat($scope.totalGoldCredit24).toFixed(2));
        $scope.entryMaster.TotalGoldDepit24 = Number(parseFloat($scope.totalGoldDepit24).toFixed(2));
        $scope.entryMaster.TotalGoldCredit22 = Number(parseFloat($scope.totalGoldCredit22).toFixed(2));
        $scope.entryMaster.TotalGoldDepit22 = Number(parseFloat($scope.totalGoldDepit22).toFixed(2));
        $scope.entryMaster.TotalGoldCredit21 = Number(parseFloat($scope.totalGoldCredit21).toFixed(2));
        $scope.entryMaster.TotalGoldDepit21 = Number(parseFloat($scope.totalGoldDepit21).toFixed(2));
        $scope.entryMaster.TotalGoldCredit18 = Number(parseFloat($scope.totalGoldCredit18).toFixed(2));
        $scope.entryMaster.TotalGoldDepit18 = Number(parseFloat($scope.totalGoldDepit18).toFixed(2));

        $scope.entryMaster.TotalTaxRateCredit = Number(parseFloat($scope.totalTaxRateCredit).toFixed(2));
        $scope.entryMaster.TotalTaxValueCredit = Number($scope.totalTaxValueCredit);
        $scope.entryMaster.TotalTaxRateDebit = Number($scope.totalTaxRateDebit);
        $scope.entryMaster.TotalTaxValueDebit = Number($scope.totalTaxValueDebit);

        $scope.entryMaster.TotalNotTaxCredit = Number(parseFloat($scope.totalNotTaxCredit).toFixed(2));
        $scope.entryMaster.TotalNotTaxDebit = Number(parseFloat($scope.totalNotTaxDebit).toFixed(2));

        $scope.entryMaster.TotalCreditWithTax = Number(parseFloat($scope.totalCreditWithTax).toFixed(2));
        $scope.entryMaster.TotalDepitWithTax = Number(parseFloat($scope.totalDepitWithTax).toFixed(2));




        $scope.entryMaster.TotalCreditExemptOfTax = (parseFloat($scope.cridetTotalExemptOfTax)).toFixed(2);
        $scope.entryMaster.ExemptCreditOfTaxValue = (parseFloat($scope.cridetExemptOfTaxValue)).toFixed(2);

        $scope.entryMaster.TotalDepitExemptOfTax = (parseFloat($scope.depitTotalExemptOfTax)).toFixed(2);
        $scope.entryMaster.ExemptDepitOfTaxValue = (parseFloat($scope.depitExemptOfTaxValue)).toFixed(2);


        $scope.entryMaster.TotalCreditTotalMainVat = (parseFloat($scope.cridetTotalMainTax)).toFixed(2);
        $scope.entryMaster.MainCreditVatValue = (parseFloat($scope.cridetMainTaxValue)).toFixed(2);


        $scope.entryMaster.TotalDepitTotalMainVat = (parseFloat($scope.depitTotalMainTax)).toFixed(2);
        $scope.entryMaster.MainDepitVatValue = (parseFloat($scope.depitMainTaxValue)).toFixed(2);


        $scope.entryMaster.TotalCreditTotalZeroVat = (parseFloat($scope.cridetTotalZeroTax)).toFixed(2);
        $scope.entryMaster.ZeroCreditVatValue = (parseFloat($scope.cridetZeroTaxValue)).toFixed(2);


        $scope.entryMaster.TotalDepitTotalZeroVat = (parseFloat($scope.depitTotalZeroTax)).toFixed(2);
        $scope.entryMaster.ZeroDepitVatValue = (parseFloat($scope.depitZeroTaxValue)).toFixed(2);


        $scope.calculateCurrencyTotals();
        // $scope.addTaxAccount();

        // Report Data
        $scope.GetReportReportTotalsTafqeet();
    }




    //check if currency is default
    $scope.calculateCurrencyTotals = function () {
        currencyService.getByCurrId($scope.entryMaster.CURRENCY_ID).then(function (result) {
            $scope.curr = result.data;
            // alert($scope.curr);
            var rate = 1;
            if ($scope.curr.DefaultCurrency != 'true' || $scope.curr.DefaultCurrency == null) {
                // alert($scope.curr.DefaultCurrency);
                if ($scope.entryMaster.CURRENCY_RATE != undefined && $scope.entryMaster.CURRENCY_RATE != null) {
                    rate = $scope.entryMaster.CURRENCY_RATE;
                }
            }
            if (rate == 0) {
                rate = 1;
            }
            $scope.totalCurrencyCredit = (parseFloat($scope.totalCredit)) * parseFloat(rate);
            $scope.entryMaster.TotalCurrencyCredit = Number(parseFloat($scope.totalCurrencyCredit).toFixed(2));


            $scope.totalCurrenctDepit = parseFloat($scope.totalDepit) * parseFloat(rate);
            $scope.entryMaster.TotalCurrenctDepit = Number(parseFloat($scope.totalCurrenctDepit).toFixed(2));


            $scope.totalCurGoldCredit24 = (parseFloat($scope.totalGoldCredit24)) * parseFloat(rate);
            $scope.entryMaster.TotalCurGoldCredit24 = Number(parseFloat($scope.totalCurGoldCredit24).toFixed(2));

            $scope.totalCurGoldDepit24 = (parseFloat($scope.totalGoldDepit24)) * parseFloat(rate);
            $scope.entryMaster.TotalCurGoldDepit24 = Number(parseFloat($scope.totalCurGoldDepit24).toFixed(2));

            $scope.totalCurGoldCredit22 = (parseFloat($scope.totalGoldCredit22)) * parseFloat(rate);
            $scope.entryMaster.TotalCurGoldCredit22 = Number(parseFloat($scope.totalCurGoldCredit22).toFixed(2));

            $scope.totalCurGoldDepit22 = (parseFloat($scope.totalGoldDepit22)) * parseFloat(rate);
            $scope.entryMaster.TotalCurGoldDepit22 = Number(parseFloat($scope.totalCurGoldDepit22).toFixed(2));

            $scope.totalCurGoldCredit21 = (parseFloat($scope.totalGoldCredit21)) * parseFloat(rate);
            $scope.entryMaster.TotalCurGoldCredit21 = Number(parseFloat($scope.totalCurGoldCredit21).toFixed(2));

            $scope.totalCurGoldDepit21 = (parseFloat($scope.totalGoldDepit21)) * parseFloat(rate);
            $scope.entryMaster.TotalCurGoldDepit21 = Number(parseFloat($scope.totalCurGoldDepit21).toFixed(2));

            $scope.totalCurGoldCredit18 = (parseFloat($scope.totalGoldCredit18)) * parseFloat(rate);
            $scope.entryMaster.TotalCurGoldCredit18 = Number(parseFloat($scope.totalCurGoldCredit18).toFixed(2));

            $scope.totalCurGoldDepit18 = (parseFloat($scope.totalGoldDepit18)) * parseFloat(rate);
            $scope.entryMaster.TotalCurGoldDepit18 = Number(parseFloat($scope.totalCurGoldDepit18).toFixed(2));

            //Report Exchange Reyal
            $scope.GetReportReportTotalsTafqeet();

        });
    }


    $scope.getSettingById = function () {

        entryService.getSettingByID($scope.settingId).then(function (result) {

            $scope.entrySetting = result.data;
            var urlParams = $location.search();
            if (urlParams.foo != null && urlParams.foo != undefined && urlParams.foo != "") {} else {
                $scope.entryMaster.ACC_ID = $scope.entrySetting.ENTRY_ACC_ID;
            }
            $scope.flagNull = false;

            $.each($scope.BoxAccounts, function (index, value) {
                if (value.ACC_ID == $scope.entryMaster.ACC_ID) {
                    $scope.flagNull = true;
                }
            })

            //if ($scope.flagNull == false) {
            //$scope.entryMaster.ACC_ID = null;
            //}

            $scope.entryMaster.CURRENCY_ID = $scope.entrySetting.CURRENCY_ID;
            $scope.TaxAccountId = $scope.entrySetting.TaxAccountID;

            $scope.EntryModeID = 1;

            $scope.changeCurrencyRate();

            $scope.isEntryTaxable = false;
            if ($scope.entrySetting.IsTaxable != undefined && $scope.entrySetting.IsTaxable != null) {
                $scope.isEntryTaxable = $scope.entrySetting.IsTaxable;
            }

            $scope.entryMaster.Taxable = $scope.entrySetting.IsTaxable;

            $scope.entryMasterTaxRate = $scope.entrySetting.TaxRate;



            $scope.entryMaster.ExemptVatRate = $scope.entrySetting.ExemptVatRate;
            $scope.entryMaster.MainVatRate = $scope.entrySetting.MainVatRate;


            $scope.entryTaxRate;
            if ($scope.entrySetting.TaxRate != undefined && $scope.entrySetting.TaxRate != null) {
                $scope.entryTaxRate = $scope.entrySetting.TaxRate;
            }


            if ($scope.entrySetting.IsEnableTaxEdit == true) {
                $scope.IsEnableTaxEdit = false;
            }

            if ($scope.entrySetting.EntryModeID != undefined && $scope.entrySetting.EntryModeID != null && $scope.entrySetting.EntryModeID != 0) {
                $scope.EntryModeID = $scope.entrySetting.EntryModeID;
                $scope.IsEntryMode = true;
            }


            if ($scope.entrySetting.IsTaxesIncluded == true) {
                $scope.IsTaxesIncluded = true;
            }


            if ($scope.TaxAccountId != undefined && $scope.TaxAccountId != null && $scope.TaxAccountId != 0) {
                accountsService.getByID($scope.TaxAccountId).then(function (result) {
                    $scope.taxAccount = result.data;
                })
            }


            if ($scope.entrySetting.IsRepeatItem == true) {
                $scope.IsRepeatItem = true;
            }

            if ($scope.entrySetting.IsQuickAccount == true) {
                $scope.IsQuickAccount = true;
            }

            if ($scope.entrySetting.IsShowTaxesBox == true) {
                $scope.IsShowTaxesBox = true;
            }

            if ($scope.entrySetting.IsTaxAccForAccount == true) {
                $scope.IsTaxAccForAccount = true;
            }

            $scope.ShowEntryTotalsSammaryAsTable = $scope.entrySetting.ShowEntryTotalsSammaryAsTable == true ? true : false;
        });

    }


    $scope.getEntryType = function () {
        entryService.getTypeBySettingID($scope.settingId).then(function (result) {
            $scope.entryType = result.data;
            $scope.typeOfEntry = result.data;
            $scope.GetAllBoxAccounts();
            $scope.getSettingById();
            // alert($scope.entryType);
        });
        // return $scope.entryType;
    }

    $scope.getTypeOfShow = function () {
        return $scope.entryType;
    }


    $scope.getNotShow = function () {
        return $scope.typeOfEntry;
    }

    $scope.getGridColumnsCheck = function () {
        $scope.isCredit = false;
        $scope.isDepit = false;
        $scope.isAcc = false;
        $scope.isCostCenter = false;
        $scope.isRemarks = false;
        $scope.isTaxValue = false;
        $scope.isTaxable = false;
        $scope.isTaxRate = false;

        $scope.isGoldCredit24 = false;
        $scope.isGoldDepit24 = false;

        $scope.isGoldCredit22 = false;
        $scope.isGoldDepit22 = false;

        $scope.isGoldCredit21 = false;
        $scope.isGoldDepit21 = false;

        $scope.isGoldCredit18 = false;
        $scope.isGoldDepit18 = false;


        $scope.IsCheckNumber = false;
        $scope.IsCheckDate = false;
        $scope.IsCheckIssueDate = false;

        $scope.IsExemptOfTax = false;


        entryGridColumnService.getByID($scope.settingId).then(function (result) {
            $scope.gridData = result.data;

            if ($scope.EntryModeID == 1) {

                $scope.isGridCredit = $scope.gridData.IS_GoldCredit24;
                $scope.isGridCredit24 = $scope.gridData.IS_GoldCredit24;
                $scope.isGridCredit22 = $scope.gridData.IS_GoldCredit22;
                $scope.isGridCredit21 = $scope.gridData.IS_GoldCredit21;
                $scope.isGridCredit18 = $scope.gridData.IS_GoldCredit18;
            }

            $scope.isCredit = $scope.gridData.IS_CREDIT;
            $scope.isDepit = $scope.gridData.IS_DEBIT;
            $scope.isAcc = $scope.gridData.IS_ACC;
            $scope.isCostCenter = $scope.gridData.IS_COST_CENTER;
            $scope.isRemarks = $scope.gridData.IS_REMARKS;
            $scope.isTaxValue = $scope.gridData.IS_TaxValue;
            $scope.isTaxRate = $scope.gridData.IS_TaxRate;
            $scope.isTaxable = $scope.gridData.IS_Taxable;

            $scope.isGoldCredit24 = $scope.gridData.IS_GoldCredit24;
            $scope.isGoldDepit24 = $scope.gridData.IS_GoldDepit24;

            $scope.isGoldCredit22 = $scope.gridData.IS_GoldCredit22;
            $scope.isGoldDepit22 = $scope.gridData.IS_GoldDepit22;

            $scope.isGoldCredit21 = $scope.gridData.IS_GoldCredit21;
            $scope.isGoldDepit21 = $scope.gridData.IS_GoldDepit21;

            $scope.isGoldCredit18 = $scope.gridData.IS_GoldCredit18;
            $scope.isGoldDepit18 = $scope.gridData.IS_GoldDepit18;


            $scope.IsCheckNumber = $scope.gridData.IsCheckNumber;
            $scope.IsCheckDate = $scope.gridData.IsCheckDate;
            $scope.IsCheckIssueDate = $scope.gridData.IsCheckIssueDate;

            $scope.IsExemptOfTax = $scope.gridData.IsExemptOfTax;

        });

    }
    /////////////////////////////////////////////////////new account popup//////////////////////////////////////////
    $scope.openAccountPopup = function () {


        if ($scope.IsQuickAccount) {


            var parentElem = angular.element($document[0].querySelector('.app-content'));
            $scope.acccountModalOpenItm = false;

            $scope.typeOfAccount = 10;

            var temp = "<div class='modal-demo'><div class='modal-body' id='modal-body'><div class='panel-body panel-body panel-white'><div class='table-responsive'> <div  ng-include src=\"'../../ngApp/QuickAccount/views/quickAccount.html'\"> </div></div></div><div class='modal-footer'> <button type='button' id='closeItemModal' ng-click='GetAllBoxAccounts();cancel()' class='btn btn- primary'>اغلاق</button> </div> </div>";

            // var temp = " <div  ng-include src=\"'../../ngApp/QuickAccount/views/quickAccount.html'\"> <div class='modal-footer'> <button type='button' id='closeItemModal' class='btn btn- primary'>'اضافة واغلاق'</button> </div> </div>";

            var modalInstance = $uibModal.open({
                //templateUrl: 'accountModal.html',
                template: temp,
                controller: 'modalCtrl',
                scope: $scope,
                preserveScope: true,
                resolve: {
                    typeOfAccount: function () {
                        return $scope.typeOfAccount;
                    }
                }
            });
            modalInstance.opened.then(function () {
                $scope.acccountModalOpenItm = true;
            });

            // we want to update state whether the modal closed or was dismissed,
            // so use finally to handle both resolved and rejected promises.
            modalInstance.result.finally(function (selectedItem) {
                $scope.acccountModalOpenItm = false;
            });
        } else {
            var parentElem = angular.element($document[0].querySelector('.app-content'));
            $scope.acccountModalOpenItm = false;

            var modalInstance = $uibModal.open({
                //templateUrl: 'accountModal.html',
                template: temp,
                controller: 'modalCtrl',
                scope: $scope,
                preserveScope: true,
                resolve: {
                    items: function () {
                        return $scope.items;
                    }
                }
            });
            modalInstance.opened.then(function () {
                $scope.acccountModalOpenItm = true;
            });

            // we want to update state whether the modal closed or was dismissed,
            // so use finally to handle both resolved and rejected promises.
            modalInstance.result.finally(function (selectedItem) {
                $scope.acccountModalOpenItm = false;
                $scope.GetBoxes();

            });
        }
    }
    //////////////////////////////////////////////////////

    $scope.GetBoxes = function () {
        //var entrytype = $scope.getBillTypeQueryString();
        $scope.BoxAccounts = [];
        PointOfSaleService.getAllBoxAccounts().then(function (response) {
            var returnedCustomerAccounts = response.data;
            if ($scope.entryType == 1 || $scope.entryType == 3 || $scope.entryType == 5 || $scope.entryType == 7) {
                $scope.CustomersAccounts = [];
                for (var i = 0; i < returnedCustomerAccounts.length; i++) {
                    if (returnedCustomerAccounts[i].ACC_TYPE_ID == 8 || returnedCustomerAccounts[i].ACC_TYPE_ID == 10) {
                        $scope.BoxAccounts.push(returnedCustomerAccounts[i]);
                    }
                }

            } else if ($scope.entryType == 2 || $scope.entryType == 4 || $scope.entryType == 6 || $scope.entryType == 8) {

                $scope.BoxAccounts = [];
                for (var i = 0; i < returnedCustomerAccounts.length; i++) {
                    if (returnedCustomerAccounts[i].ACC_TYPE_ID == 8 || returnedCustomerAccounts[i].ACC_TYPE_ID == 10) {
                        $scope.BoxAccounts.push(returnedCustomerAccounts[i]);
                    }

                }

            }
        })
        //var entryType = $scope.getBillTypeQueryString();
        //$scope.CustomersAccounts = [];
        //entryService.GetAllBoxAccounts().then(function (response) {
        //    $scope.CustomersAccounts = response.data;
        //})
    }



    //////////////////////////////////////////////////new currency pop up 

    $scope.openCurrencyPopup = function () {

        var parentElem = angular.element($document[0].querySelector('.app-content'));
        $scope.acccountModalOpenItm = false;

        var temp = "<div  ng-include src=\"'../../ngApp/currency/views/currency.html'\"> </div>";

        var modalInstance = $uibModal.open({
            //templateUrl: 'accountModal.html',
            template: temp,
            controller: 'modalCtrl',
            scope: $scope,
            preserveScope: true,
            resolve: {
                items: function () {
                    return $scope.items;
                }
            }
        });
        modalInstance.opened.then(function () {
            $scope.acccountModalOpenItm = true;
        });

        // we want to update state whether the modal closed or was dismissed,
        // so use finally to handle both resolved and rejected promises.
        modalInstance.result.finally(function (selectedItem) {
            $scope.acccountModalOpenItm = false;
            $scope.getAllCurrencies();

        });
    }
    ////////////////////////////////////////////////////////new employee pop up 
    $scope.openEmployeePopup = function () {

        var parentElem = angular.element($document[0].querySelector('.app-content'));
        $scope.acccountModalOpenItm = false;

        var temp = "<div  ng-include src=\"'../../ngApp/Employees/views/employees.html'\"> </div>";

        var modalInstance = $uibModal.open({
            //templateUrl: 'accountModal.html',
            template: temp,
            controller: 'modalCtrl',
            scope: $scope,
            preserveScope: true,
            resolve: {
                items: function () {
                    return $scope.items;
                }
            }
        });
        modalInstance.opened.then(function () {
            $scope.acccountModalOpenItm = true;
        });

        // we want to update state whether the modal closed or was dismissed,
        // so use finally to handle both resolved and rejected promises.
        modalInstance.result.finally(function (selectedItem) {
            $scope.acccountModalOpenItm = false;
            $scope.GetAllEmployeesSales();

        });
    }


    /////////////////////////////get entry master by bill id 
    $scope.getEntryByBill = function (billID) {

        entryService.getEntryMasterByBill(billID).then(function (result) {
            $scope.entryMaster = result.data;
            $scope.getDetailsByEntryId($scope.entryMaster.ENTRY_ID);


        });
    }

    $scope.getEntryByAssetOperation = function (assetOperationId) {

        entryService.getEntryMasterByAssetOperation(assetOperationId).then(function (result) {
            $scope.entryMaster = result.data;
            $scope.getDetailsByEntryId($scope.entryMaster.ENTRY_ID);


        });
    }
    ////////////////////////////////////////////get grid data of columns //////////////////////////////////////
    $scope.getGridData = function () {



        $scope.defaultGridData = {
            ARCode: 'كود',
            ENCode: 'Code',
            ARAccountName: 'الحساب',
            ENAccountName: 'Account',
            ARCredit: 'دائن',
            ENCredit: 'Credit',
            ARDepit: 'مدين',
            ENDepit: 'Depit',
            ARCredit24: 'دائن 24',
            ENCredit24: 'Credit 24',
            ARDepit24: 'مدين 24',
            ENDepit24: 'Depit 24',
            ARCredit22: 'دائن 22',
            ENCredit22: 'Credit 22',
            ARDepit22: 'مدين 22',
            ENDepit22: 'Depit 22',
            ARCredit21: 'دائن 21',
            ENCredit21: 'Credit 21',
            ARDepit21: 'مدين 21',
            ENDepit21: 'Depit 21',
            ARCredit18: 'دائن 18',
            ENCredit18: 'Credit 18',
            ARDepit18: 'مدين 18',
            ENDepit18: 'Depit 18',
            ARCostCenter: ' مركز التكلفه',
            ENCostCenter: 'Cost Center',
            ARRemarks: 'شرح',
            ENRemarks: 'Remarks',
            ARTaxable: 'خاضع للضريبه',
            EnTaxable: 'Taxable',
            ARTaxRate: 'نسبة الضريبه',
            ENTaxRate: 'Tax Rate',
            ARTaxValue: 'قيمة الضريبه',
            ENTaxValue: 'Tax Value',
            ARCheckNumber: 'رقم الشيك',
            ENCheckNumber: 'Check Number',
            ARCheckDate: 'تاريخ الشيك',
            ENCheckDate: 'Check Date',
            ARCheckIssueDate: 'تاريخ الاستحقاق',
            ENCheckIssueDate: 'Check Issue Date',
            ARExemptOfTax: 'معفي من الضريبه',
            ENExemptOfTax: 'Exempt of tax'
        }


        //if ($location.search().entryType != undefined) {
        //    $scope.settingId = $location.search().entryType;
        //}
        //else {
        //    $scope.settingId = $scope.popUpData.entrySettingId;
        //}

        entryGridColumnService.getByID($scope.settingId).then(function (result) {
            $scope.returnGridData = result.data;

            if ($scope.returnGridData.ARCode == "" || $scope.returnGridData.ARCode == undefined) {
                $scope.gridData.ARCode = $scope.defaultGridData.ARCode;
            } else {
                $scope.gridData.ARCode = $scope.returnGridData.ARCode;
            }



            if ($scope.returnGridData.ARAccountName == "" || $scope.returnGridData.ARAccountName == undefined) {
                $scope.gridData.ARAccountName = $scope.defaultGridData.ARAccountName;
            } else {
                $scope.gridData.ARAccountName = $scope.returnGridData.ARAccountName;
            }


            if ($scope.returnGridData.ARCredit == "" || $scope.returnGridData.ARCredit == undefined) {
                $scope.gridData.ARCredit = $scope.defaultGridData.ARCredit;
            } else {
                $scope.gridData.ARCredit = $scope.returnGridData.ARCredit;
            }

            if ($scope.returnGridData.ARDepit == "" || $scope.returnGridData.ARDepit == undefined) {
                $scope.gridData.ARDepit = $scope.defaultGridData.ARDepit;
            } else {
                $scope.gridData.ARDepit = $scope.returnGridData.ARDepit;
            }

            if ($scope.returnGridData.ARCredit24 == "" || $scope.returnGridData.ARCredit24 == undefined) {
                $scope.gridData.ARCredit24 = $scope.defaultGridData.ARCredit24;
            } else {
                $scope.gridData.ARCredit24 = $scope.returnGridData.ARCredit24;
            }

            if ($scope.returnGridData.ARDepit24 == "" || $scope.returnGridData.ARDepit24 == undefined) {
                $scope.gridData.ARDepit24 = $scope.defaultGridData.ARDepit24;
            } else {
                $scope.gridData.ARDepit24 = $scope.returnGridData.ARDepit24;
            }


            if ($scope.returnGridData.ARCredit22 == "" || $scope.returnGridData.ARCredit22 == undefined) {
                $scope.gridData.ARCredit22 = $scope.defaultGridData.ARCredit22;
            } else {
                $scope.gridData.ARCredit22 = $scope.returnGridData.ARCredit22;
            }


            if ($scope.returnGridData.ARDepit22 == "" || $scope.returnGridData.ARDepit22 == undefined) {
                $scope.gridData.ARDepit22 = $scope.defaultGridData.ARDepit22;
            } else {
                $scope.gridData.ARDepit22 = $scope.returnGridData.ARDepit22;
            }


            if ($scope.returnGridData.ARCredit21 == "" || $scope.returnGridData.ARCredit21 == undefined) {
                $scope.gridData.ARCredit21 = $scope.defaultGridData.ARCredit21;
            } else {
                $scope.gridData.ARCredit21 = $scope.returnGridData.ARCredit21;
            }


            if ($scope.returnGridData.ARDepit21 == "" || $scope.returnGridData.ARDepit21 == undefined) {
                $scope.gridData.ARDepit21 = $scope.defaultGridData.ARDepit21;
            } else {
                $scope.gridData.ARDepit21 = $scope.returnGridData.ARDepit21;
            }


            if ($scope.returnGridData.ARCredit18 == "" || $scope.returnGridData.ARCredit18 == undefined) {
                $scope.gridData.ARCredit18 = $scope.defaultGridData.ARCredit18;
            } else {
                $scope.gridData.ARCredit18 = $scope.returnGridData.ARCredit18;
            }


            if ($scope.returnGridData.ARDepit18 == "" || $scope.returnGridData.ARDepit18 == undefined) {
                $scope.gridData.ARDepit18 = $scope.defaultGridData.ARDepit18;
            } else {
                $scope.gridData.ARDepit18 = $scope.returnGridData.ARDepit18;
            }



            if ($scope.returnGridData.ARCostCenter == "" || $scope.returnGridData.ARCostCenter == undefined) {
                $scope.gridData.ARCostCenter = $scope.defaultGridData.ARCostCenter;
            } else {
                $scope.gridData.ARCostCenter = $scope.returnGridData.ARCostCenter;
            }


            if ($scope.returnGridData.ARRemarks == "" || $scope.returnGridData.ARRemarks == undefined) {
                $scope.gridData.ARRemarks = $scope.defaultGridData.ARRemarks;
            } else {
                $scope.gridData.ARRemarks = $scope.returnGridData.ARRemarks;
            }


            if ($scope.returnGridData.ARTaxable == "" || $scope.returnGridData.ARTaxable == undefined) {
                $scope.gridData.ARTaxable = $scope.defaultGridData.ARTaxable;
            } else {
                $scope.gridData.ARTaxable = $scope.returnGridData.ARTaxable;
            }


            if ($scope.returnGridData.ARTaxRate == "" || $scope.returnGridData.ARTaxRate == undefined) {
                $scope.gridData.ARTaxRate = $scope.defaultGridData.ARTaxRate;
            } else {
                $scope.gridData.ARTaxRate = $scope.returnGridData.ARTaxRate;
            }

            if ($scope.returnGridData.ARTaxValue == "" || $scope.returnGridData.ARTaxValue == undefined) {
                $scope.gridData.ARTaxValue = $scope.defaultGridData.ARTaxValue;
            } else {
                $scope.gridData.ARTaxValue = $scope.returnGridData.ARTaxValue;
            }


            if ($scope.returnGridData.ARCheckNumber == "" || $scope.returnGridData.ARCheckNumber == undefined) {
                $scope.gridData.ARCheckNumber = $scope.defaultGridData.ARCheckNumber;
            } else {
                $scope.gridData.ARCheckNumber = $scope.returnGridData.ARCheckNumber;
            }

            if ($scope.returnGridData.ARCheckDate == "" || $scope.returnGridData.ARCheckDate == undefined) {
                $scope.gridData.ARCheckDate = $scope.defaultGridData.ARCheckDate;
            } else {
                $scope.gridData.ARCheckDate = $scope.returnGridData.ARCheckDate;
            }


            if ($scope.returnGridData.ARCheckIssueDate == "" || $scope.returnGridData.ARCheckIssueDate == undefined) {
                $scope.gridData.ARCheckIssueDate = $scope.defaultGridData.ARCheckIssueDate;
            } else {
                $scope.gridData.ARCheckIssueDate = $scope.returnGridData.ARCheckIssueDate;
            }



            if ($scope.returnGridData.ARExemptOfTax == "" || $scope.returnGridData.ARExemptOfTax == undefined) {
                $scope.gridData.ARExemptOfTax = $scope.defaultGridData.ARExemptOfTax;
            } else {
                $scope.gridData.ARExemptOfTax = $scope.returnGridData.ARExemptOfTax;
            }

            $scope.gridData.ENCode = $scope.returnGridData.ENCode;
            $scope.gridData.ENAccountName = $scope.returnGridData.ENAccountName;
            $scope.gridData.ENCredit = $scope.returnGridData.ENCredit;
            $scope.gridData.ENDepit = $scope.returnGridData.ENDepit;
            $scope.gridData.ENCredit24 = $scope.returnGridData.ENCredit24;
            $scope.gridData.ENDepit24 = $scope.returnGridData.ENDepit24;
            $scope.gridData.ENCredit22 = $scope.returnGridData.ENCredit22;
            $scope.gridData.ENDepit22 = $scope.returnGridData.ENDepit22;
            $scope.gridData.ENCredit21 = $scope.returnGridData.ENCredit21;
            $scope.gridData.ENDepit21 = $scope.returnGridData.ENDepit21;
            $scope.gridData.ENCredit18 = $scope.returnGridData.ENCredit18;
            $scope.gridData.ENDepit18 = $scope.returnGridData.ENDepit18;
            $scope.gridData.ENCostCenter = $scope.returnGridData.ENCostCenter;
            $scope.gridData.ENRemarks = $scope.returnGridData.ENRemarks;
            $scope.gridData.EnTaxable = $scope.returnGridData.EnTaxable;
            $scope.gridData.ENTaxRate = $scope.returnGridData.ENTaxRate;

            $scope.gridData.ENCheckNumber = $scope.returnGridData.ENCheckNumber;
            $scope.gridData.ENCheckDate = $scope.returnGridData.ENCheckDate;
            $scope.gridData.ENCheckIssueDate = $scope.returnGridData.ENCheckIssueDate;

            $scope.gridData.ENExemptOfTax = $scope.returnGridData.ENExemptOfTax;

        });


    }

    ///////////////////////////////////////search of code for entry ////////////////////////////////////////

    $scope.searchEntry = function () {
        var entrySetting = ($location.search()).entryType;
        entryService.getEntryByEntryNumber($scope.entryMaster.ENTRY_NUMBER, entrySetting).then(function (result) {


            var entryMaster = result.data.EntryMaster;

            $scope.entryMaster = entryMaster;
            $scope.selectedItems = [];
            $scope.entryMaster.ENTRY_DATE = new Date(entryMaster.ENTRY_DATE);
            $scope.entryMaster.BILL_NUMBER = Number(entryMaster.BILL_NUMBER);
            $scope.entryMaster.BILL_PAY_WAY = Number(entryMaster.BILL_PAY_WAY);
            $scope.entryMaster.CHECK_DATE = new Date(entryMaster.CHECK_DATE);
            $scope.entryMaster.COLLECT_ENTRY_CODE = Number(entryMaster.COLLECT_ENTRY_CODE);
            $scope.entryMaster.ENTRY_NUMBER = Number(entryMaster.ENTRY_NUMBER);

            $scope.getDetailsByEntryId(entryMaster.ENTRY_ID)
        });
    }




    $scope.checkEntryByEntryNumber = function () {
        var entrySetting = ($location.search()).entryType;
        // if ($scope.entryMaster.ENTRY_NUMBER != undefined) {
        return entryService.checkEntryByEntryNumber($scope.entryMaster.ENTRY_NUMBER, entrySetting);
        //}
        //else {
        //    return true;
        //}

    }


    ///////////////////////////////////////////printing////////////////////

    //// hiding or showing print btn
    $scope.hidePrintOnNewBill = function () {
        if ($scope.operation == "Insert") {
            return true;
        } else {
            return false;
        }

    }

    $scope.operation = "Insert";

    $scope.printDiv = function (printType) {
        
        var divId;
        var contents;
        divId = $scope.getPrintDivId();
        if (printType == 'Bill') {
            
            $scope.PrintEntryDetailsWithBill = false;
            setTimeout(function () {
                contents = document.getElementById(divId).innerHTML;
                var frame1 = document.createElement('iframe');
                frame1.name = "frame1";
                frame1.style.position = "absolute";
                frame1.style.top = "-1000000px";
                document.body.appendChild(frame1);
                var frameDoc = frame1.contentWindow ? frame1.contentWindow : frame1.contentDocument.document ? frame1.contentDocument.document : frame1.contentDocument;
                frameDoc.document.open();
                frameDoc.document.write('<html id="printTable" dir="rtl"><head>');
                frameDoc.document.write('<style>*{border: none} #fatoraFooter * , #fatoraHeader *{border: none !important;text-align: center} table{margin:10px 0; width: 100%} th{height:50px}table {border-collapse: collapse;}th, td {border: 1px solid black;padding: 5px 10px}</style>');
                frameDoc.document.write('</head><body>');
                frameDoc.document.write(contents);
                frameDoc.document.write('</body></html>');
                frameDoc.document.close();
                setTimeout(function () {
                    window.frames["frame1"].focus();
                    window.frames["frame1"].print();
                    document.body.removeChild(frame1);
                }, 500);
                return false;
            }, 500)
        } else if (printType == 'BillWithEntry') {
            $scope.PrintEntryDetailsWithBill = true;
            entryService.getDetailsByEntryId($scope.dailyEntryID).then(function (result) {
                $scope.returnEntryDetails = result.data;

                $scope.DebitEntries = [];
                $scope.CreditEntries = [];

                for (var i = 0; i < $scope.returnEntryDetails.length; i++) {
                    var gridObj = {};
                    var isCredit = false;
                    var isDepit = false;
                    // check depit
                    if ($scope.returnEntryDetails[i].ENTRY_DEBIT != undefined && $scope.returnEntryDetails[i].ENTRY_DEBIT != 0) {
                        isDepit = true;
                    } else if ($scope.returnEntryDetails[i].ENTRY_GOLD24_DEBIT != undefined && $scope.returnEntryDetails[i].ENTRY_GOLD24_DEBIT != 0) {
                        isDepit = true;
                    } else if ($scope.returnEntryDetails[i].ENTRY_GOLD22_DEBIT != undefined && $scope.returnEntryDetails[i].ENTRY_GOLD22_DEBIT != 0) {
                        isDepit = true;
                    } else if ($scope.returnEntryDetails[i].ENTRY_GOLD21_DEBIT != undefined && $scope.returnEntryDetails[i].ENTRY_GOLD21_DEBIT != 0) {
                        isDepit = true;
                    } else if ($scope.returnEntryDetails[i].ENTRY_GOLD18_DEBIT != undefined && $scope.returnEntryDetails[i].ENTRY_GOLD18_DEBIT != 0) {
                        isDepit = true;
                    }

                    //check credit
                    if ($scope.returnEntryDetails[i].ENTRY_CREDIT != undefined && $scope.returnEntryDetails[i].ENTRY_CREDIT != 0) {
                        isCredit = true;
                    } else if ($scope.returnEntryDetails[i].ENTRY_GOLD24_CREDIT != undefined && $scope.returnEntryDetails[i].ENTRY_GOLD24_CREDIT != 0) {
                        isCredit = true;
                    } else if ($scope.returnEntryDetails[i].ENTRY_GOLD22_CREDIT != undefined && $scope.returnEntryDetails[i].ENTRY_GOLD22_CREDIT != 0) {
                        isCredit = true;
                    } else if ($scope.returnEntryDetails[i].ENTRY_GOLD21_CREDIT != undefined && $scope.returnEntryDetails[i].ENTRY_GOLD21_CREDIT != 0) {
                        isCredit = true;
                    } else if ($scope.returnEntryDetails[i].ENTRY_GOLD18_CREDIT != undefined && $scope.returnEntryDetails[i].ENTRY_GOLD18_CREDIT != 0) {
                        isCredit = true;
                    }
                    if (isDepit) {
                        gridObj = $scope.returnEntryDetails[i];
                        $scope.DebitEntries.push(gridObj);
                    } else if (isCredit) {
                        gridObj = $scope.returnEntryDetails[i];
                        $scope.CreditEntries.push(gridObj);
                    }
                }
                setTimeout(function () {
                    contents = document.getElementById(divId).innerHTML;
                    var frame1 = document.createElement('iframe');
                    frame1.name = "frame1";
                    frame1.style.position = "absolute";
                    frame1.style.top = "-1000000px";
                    document.body.appendChild(frame1);
                    var frameDoc = frame1.contentWindow ? frame1.contentWindow : frame1.contentDocument.document ? frame1.contentDocument.document : frame1.contentDocument;
                    frameDoc.document.open();
                    frameDoc.document.write('<html id="printTable" dir="rtl"><head>');
                    frameDoc.document.write('<style>*{border: none} #fatoraFooter * , #fatoraHeader *{border: none !important;text-align: center} table{margin:10px 0; width: 100%} th{height:50px}table {border-collapse: collapse;}th, td {border: 1px solid black;padding: 5px 10px}</style>');
                    frameDoc.document.write('</head><body>');
                    frameDoc.document.write(contents);
                    frameDoc.document.write('</body></html>');
                    frameDoc.document.close();
                    setTimeout(function () {
                        window.frames["frame1"].focus();
                        window.frames["frame1"].print();
                        document.body.removeChild(frame1);
                    }, 500);
                    return false;
                }, 500)

            });
        }
    }

    $scope.getPrintDivId = function () {
        //$scope.GetCustomerDataForReport();
        //$scope.GetCompanyData();
        
        switch ($scope.typeOfEntry) {
       
            case 1: {
                return "divPrintGetReyal"
            }
            case 2: {
                return "divPrintExchangeReyal"
            }
            case 4: {
                return "divPrintExchangeCheck"
            }
            case 5: {
                return "divPrintGetCheck"
            }
            case 10: {
                return "entryCredit"
            }
            case 11: {
                return "entryDebit"
            }
            default: {
                return ""
            }
        }
    }

    /////print
    //$scope.printDiv = function (divName) {
    //    var contents = document.getElementById(divName).innerHTML;
    //    var frame1 = document.createElement('iframe');
    //    frame1.name = "frame1";
    //    frame1.style.position = "absolute";
    //    frame1.style.top = "-1000000px";
    //    document.body.appendChild(frame1);
    //    var frameDoc = frame1.contentWindow ? frame1.contentWindow : frame1.contentDocument.document ? frame1.contentDocument.document : frame1.contentDocument;
    //    frameDoc.document.open();
    //    frameDoc.document.write('<html  dir="rtl"><head><title></title>');
    //    frameDoc.document.write('</head><body>');
    //    frameDoc.document.write(contents);
    //    frameDoc.document.write('</body></html>');
    //    frameDoc.document.close();
    //    setTimeout(function () {
    //        window.frames["frame1"].focus();
    //        window.frames["frame1"].print();
    //        document.body.removeChild(frame1);
    //    }, 500);
    //    return false;
    //}



    //Get Printing Time for the bill
    $scope.getCurrentTime = function () {
        var d = new Date(); // for now
        var H = d.getHours(); // => 9
        var m = d.getMinutes(); // =>  30
        var s = d.getSeconds(); // => 51
        return "" + H + ":" + m + ":" + s + "";
    }


    //get employee for printing 
    $scope.getCurrentEmployeeName = function () {
        if ($scope.entryMaster.EMP_ID != null && $scope.entryMaster.EMP_ID > 0) {
            for (var i = 0; i < $scope.EmployeesSales.length; i++) {
                if ($scope.EmployeesSales[i].EMP_ID == $scope.entryMaster.EMP_ID) {
                    return $scope.EmployeesSales[i].EMP_AR_NAME;
                }
            }
        }

    }

    //getAccount
    $scope.getAccountName = function () {
        if ($scope.entryMaster.ACC_ID != null && $scope.entryMaster.ACC_ID > 0) {
            for (var i = 0; i < $scope.BoxAccounts.length; i++) {
                if ($scope.BoxAccounts[i].ACC_ID == $scope.entryMaster.ACC_ID) {
                    return $scope.BoxAccounts[i].ACC_AR_NAME;
                }
            }
        }

    }

    //////currency

    $scope.getCurrencyName = function () {
        var currencyID = $scope.entryMaster.CURRENCY_ID;
        if ($scope.currenciesList != undefined || $scope.currenciesList != null) {
            for (var i = 0; i < $scope.currenciesList.length; i++) {
                if ($scope.currenciesList[i].CURRENCY_ID == currencyID) {

                    $scope.entryMaster.currencyName = $scope.currenciesList[i].CURRENCY_AR_NAME;
                    return $scope.entryMaster.currencyName;
                }
            }
        }
    }


    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //////////save daily entry of every entry///////////////

    $scope.saveDailyEntry = function () {
        ////////////get total credit and depit for entry master
        
        $scope.dailyEntryMaster = $scope.entryMaster;

        $scope.dailyEntryMaster.ENTRY_SETTING_ID = 130;
        $scope.dailyEntryMaster.RelatedEntryID = $scope.masterID;
        $scope.dailyEntryMaster.ENTRY_ID = 0;
        //var length = $scope.entryDetails.length;
        //$scope.entryDetails.splice(parseInt(length) - 1, 1);

        /**************************************************************/
        //سندات القبض النقدى والشيكات 
        if ($scope.typeOfEntry == 1 || $scope.typeOfEntry == 5 || $scope.typeOfEntry == 11) {
            var TotalTaxes = 0;

            //in case of compelix entry
            if ($scope.EntryModeID == 2) {
                
                for (var i = 0; i < $scope.entryDetails2.length; i++) {
                    if ($scope.entryMaster.Taxable) {
                        if ($scope.entryTaxRate == null && $scope.entryTaxRate == undefined) {
                            $scope.entryTaxRate = 0;
                        }

                        var ValueBeforeTax = parseFloat($scope.entryDetails2[i].ENTRY_CREDIT / (1 + ($scope.entryTaxRate / 100))).toFixed(2);
                        TotalTaxes += $scope.entryDetails2[i].ENTRY_CREDIT - ValueBeforeTax;
                        $scope.entryDetails2[i].ENTRY_CREDIT = ValueBeforeTax;
                    } else {

                        if ($scope.entryDetails2[i].Taxable && $scope.entryDetails2[i].TaxRate != 0 && $scope.entryDetails2[i].TaxRate != undefined) {

                            if ($scope.entryDetails2[i].TaxValue != undefined && $scope.entryDetails2[i].TaxValue != null && $scope.entryDetails2[i].TaxValue != 0) {
                                TotalTaxes += $scope.entryDetails2[i].TaxValue;
                            } else
                            if ($scope.entryDetails2[i].ENTRY_CREDIT != null && $scope.entryDetails2[i].ENTRY_CREDIT != undefined) {
                                TotalTaxes += $scope.entryDetails2[i].ENTRY_CREDIT;
                            }


                            $scope.entryDetails2[i].ENTRY_CREDIT = $scope.entryDetails2[i].CreditValueWithoutTax;

                        }
                    }
                }
            } else {

                for (var i = 0; i < $scope.entryDetails.length; i++) {
                    
                    if ($scope.entryMaster.Taxable) {
                        if ($scope.entryTaxRate == null && $scope.entryTaxRate == undefined) {
                            $scope.entryTaxRate = 0;
                        }
                        var ValueBeforeTax = parseFloat($scope.entryDetails[i].ENTRY_CREDIT / (1 + ($scope.entryTaxRate / 100))).toFixed(2);
                        TotalTaxes += $scope.entryDetails[i].ENTRY_CREDIT - ValueBeforeTax;
                        $scope.entryDetails[i].ENTRY_CREDIT = ValueBeforeTax;
                    } else {

                        if ($scope.entryDetails[i].Taxable && $scope.entryDetails[i].TaxRate != 0 && $scope.entryDetails[i].TaxRate != undefined) {
                            TotalTaxes += $scope.entryDetails[i].TaxValue;
                            $scope.entryDetails[i].ENTRY_CREDIT = (parseFloat($scope.entryDetails[i].ENTRY_CREDIT) - parseFloat($scope.entryDetails[i].TaxValue)).toFixed(2);
                        }
                    }
                }

            }




            //قيد الضرائب

            if ($scope.IsTaxAccForAccount) {
                
                $scope.getTaxAccountsOfDetailsForCredit();
                if ($scope.taxAccountsList.length > 0) {
                    for (var i = 0; i < $scope.taxAccountsList.length; i++) {
                        var accObj = {};
                        accObj.ACC_ID = $scope.taxAccountsList[i].AccId;
                        accObj.ENTRY_CREDIT = $scope.taxAccountsList[i].tax;

                        $scope.entryDetails.push(accObj);
                    }
                }

            } else {
                var TaxEntry = {
                    ACC_ID: $scope.TaxAccountId,
                    ENTRY_CREDIT: parseFloat(TotalTaxes).toFixed(2)
                }
                $scope.entryDetails.push(TaxEntry);
            }

            //قيد الاجمالى
            var BasicAccount = {
                ACC_ID: $scope.entryMaster.ACC_ID,
                //القيم المدينة
                ENTRY_DEBIT: $scope.entryMaster.TotalCredit,
                //ENTRY_GOLD18_DEBIT: $scope.entryMaster.TotalGoldCredit18,
                //ENTRY_GOLD21_DEBIT: $scope.entryMaster.TotalGoldCredit21,
                //ENTRY_GOLD22_DEBIT: $scope.entryMaster.TotalGoldCredit22,
                //ENTRY_GOLD24_DEBIT: $scope.entryMaster.TotalGoldCredit24,

                //القيم الدائنة
                ENTRY_CREDIT: 0,
                ENTRY_GOLD18_CREDIT: 0,
                ENTRY_GOLD21_CREDIT: 0,
                ENTRY_GOLD22_CREDIT: 0,
                ENTRY_GOLD24_CREDIT: 0,
            }
            if ($scope.EntryModeID != 2) {
                $scope.entryDetails.push(BasicAccount);
            }
        }

        //سندات الصرف النقدى والشيكات
        else if ($scope.typeOfEntry == 2 || $scope.typeOfEntry == 4 || $scope.typeOfEntry == 10) {
            
            var TotalTaxes = 0;
            for (var i = 0; i < $scope.entryDetails.length; i++) {
                if ($scope.entryMaster.Taxable) {
                    if ($scope.entryTaxRate == null && $scope.entryTaxRate == undefined) {
                        $scope.entryTaxRate = 0;
                    }

                    if ($scope.entryDetails[i].ENTRY_DEBIT != undefined && $scope.entryDetails[i].ENTRY_DEBIT != null && $scope.entryDetails[i].ENTRY_DEBIT != 0) {
                        var ValueBeforeTax = parseFloat($scope.entryDetails[i].ENTRY_DEBIT / (1 + ($scope.entryTaxRate / 100))).toFixed(2);
                        TotalTaxes += $scope.entryDetails[i].ENTRY_DEBIT - ValueBeforeTax;
                        $scope.entryDetails[i].ENTRY_DEBIT = ValueBeforeTax;
                    }
                } else {
                    if ($scope.entryDetails[i].Taxable && $scope.entryDetails[i].TaxRate != 0 && $scope.entryDetails[i].TaxRate != undefined) {
                        TotalTaxes += $scope.entryDetails[i].TaxValue;
                        $scope.entryDetails[i].ENTRY_DEBIT = (parseFloat($scope.entryDetails[i].ENTRY_DEBIT) - parseFloat($scope.entryDetails[i].TaxValue)).toFixed(2);
                    }
                }
            }
            //قيد الضرائب


            if ($scope.IsTaxAccForAccount) {
                
                $scope.getTaxAccountsOfDetailsForDepit();
                if ($scope.taxAccountsList.length > 0) {
                    for (var i = 0; i < $scope.taxAccountsList.length; i++) {
                        var accObj = {};
                        accObj.ACC_ID = $scope.taxAccountsList[i].AccId;
                        accObj.ENTRY_DEBIT = $scope.taxAccountsList[i].tax;

                        $scope.entryDetails.push(accObj);
                    }
                }

            }
            else {
                var TaxEntry = {
                    
                    ACC_ID: $scope.TaxAccountId,
                    ENTRY_DEBIT: parseFloat(TotalTaxes).toFixed(2)
                }
                $scope.entryDetails.push(TaxEntry);

            }
            //قيد الاجمالى
            var BasicAccount = {
                ACC_ID: $scope.entryMaster.CustAccID,
                //القيم المدينة
                // ENTRY_DEBIT: 0,
                ENTRY_GOLD18_DEBIT: 0,
                ENTRY_GOLD21_DEBIT: 0,
                ENTRY_GOLD22_DEBIT: 0,
                ENTRY_GOLD24_DEBIT: 0,

                //القيم الدائنة
                ENTRY_CREDIT: $scope.entryMaster.TotalDepit,
                //ENTRY_GOLD18_CREDIT: $scope.entryMaster.TotalGoldDepit18,
                //ENTRY_GOLD21_CREDIT: $scope.entryMaster.TotalGoldDepit21,
                //ENTRY_GOLD22_CREDIT: $scope.entryMaster.TotalGoldDepit22,
                //ENTRY_GOLD24_CREDIT: $scope.entryMaster.TotalGoldDepit24,
            }
            if ($scope.EntryModeID != 2) {
                $scope.entryDetails.push(BasicAccount);
            } else {
                var customerAccount = jQuery.grep($scope.entryDetails, function (n, i) {
                    return (n.ACC_ID === $scope.entryMaster.CustAccID);
                });

                //customerAccount[0].ENTRY_DEBIT = parseFloat(customerAccount[0].ENTRY_DEBIT) - parseFloat(TotalTaxes);
            }
        }

        /**************************************************************/

        var entryMasterDetails = {
            EntryMaster: $scope.dailyEntryMaster,
            EntryDetails: $scope.entryDetails
        };


        // if (response.data || $scope.entryMaster.ENTRY_ID == 0 || $scope.entryMaster.ENTRY_ID == undefined) {

        entryService.addEntryMasterDetails(entryMasterDetails).then(function (result) {
            $scope.savedDailyEntry = result.data;
        });


        // }
    }


    //$scope.getEntryNumber = function () {
    //    entryService.GetLastEntryNumber(3).then(function (response) {
    //        var result = response.data;
    //        $scope.entryMaster.ENTRY_NUMBER = result + 1;
    //        //return $scope.ENTRY_NUMBER;
    //    })
    //}


    $scope.getDailyEntryByMasterID = function (masterID) {
        entryService.getDailyEntryByMsterID(masterID).then(function (result) {
            $scope.dailyEntryID = result.data.EntryMaster.ENTRY_ID;
        });
    }



    ////////////////////////////// Open daily Entry ////////////////////////////////
    $scope.openEntryPopup = function () {

        var parentElem = angular.element($document[0].querySelector('.app-content'));
        $scope.acccountModalOpenItm = false;
        $scope.FromPopUpentryType = 130;

        $scope.entrySettingId = 130;
        $scope.popUpData = {
            entrySettingId: $scope.entrySettingId,
            dailyEntryID: $scope.dailyEntryID
        };

        //if ($scope.typeOfEntry == 1 || $scope.typeOfEntry == 2 || $scope.typeOfEntry == 4 || $scope.typeOfEntry == 5) {
        //    var modalInstance = $uibModal.open({
        //        templateUrl: 'EntryDataModalContent.html',
        //        controller: 'modalCtrl',
        //        scope: $scope,
        //        preserveScope: true
        //    });
        //    modalInstance.opened.then(function () {
        //        $scope.modalOpenItm = true;
        //    });

        //    // we want to update state whether the modal closed or was dismissed,
        //    // so use finally to handle both resolved and rejected promises.
        //    modalInstance.result.finally(function (selectedItem) {
        //        $scope.modalOpenItm = false;
        //    });
        //}
        //else {
        var temp = "<div  ng-include src=\"'../../ngApp/Entries/views/entry.html'\"></div>";

        var modalInstance = $uibModal.open({
            //templateUrl: 'accountModal.html',
            template: temp,
            controller: 'modalCtrl',
            scope: $scope,
            preserveScope: true,
            resolve: {
                popUpData: function () {
                    return $scope.popUpData;
                }
            }
        });
        modalInstance.opened.then(function () {
            $scope.acccountModalOpenItm = true;

        });

        // we want to update state whether the modal closed or was dismissed,
        // so use finally to handle both resolved and rejected promises.
        modalInstance.result.finally(function (selectedItem) {
            $scope.acccountModalOpenItm = false;
            $scope.settingId = $location.search().entryType;
            $scope.popUpData = undefined;
        });
        //}
    }



    $scope.loadDailyEntry = function (dailyEntryID) {

        entryService.getEntryByEntryID(dailyEntryID).then(function (result) {

            //entryService.GetLastEntryNumber(130).then(function (response) {
            //var resultNum = response.data;
            //$scope.entryMaster.ENTRY_NUMBER = resultNum + 1;

            $scope.entryMaster = result.data.EntryMaster;
            $scope.getDetailsByEntryId($scope.entryMaster.ENTRY_ID);

            //})

        });
    }


    $scope.changeTax = function () {
        if ($scope.entryMaster.Taxable == true) {
            // if ($scope.entryMaster.TaxValue != null && $scope.entryMaster.TaxValue != undefined && $scope.entryMaster.TaxValue != 0) {
            $scope.calculateTotals();
            //  }
        } else {
            $scope.calculateTotals();
        }

    }



    ///////////////////////////////////////////////////////////
    $scope.addNotification = function () {

        var accCode;
        var accName;
        for (var i = 0; i < $scope.BoxAccounts.length; i++) {
            if ($scope.entryMaster.ACC_ID == $scope.BoxAccounts[i].ACC_ID) {
                accCode = $scope.BoxAccounts[i].ACC_CODE;
                accName = $scope.BoxAccounts[i].ACC_AR_NAME;
            }
        }

        if ($scope.typeOfEntry == 10) {

            $scope.creditObject = {};
            $scope.creditObject.ACC_ID = $scope.entryMaster.ACC_ID;
            $scope.creditObject.ACC_CODE = accCode;
            $scope.creditObject.ACC_AR_NAME = accName;
            $scope.creditObject.ENTRY_CREDIT = $scope.notification.moneyValue;
            $scope.creditObject.ENTRY_GOLD24_CREDIT = $scope.notification.gold24;
            $scope.creditObject.ENTRY_GOLD22_CREDIT = $scope.notification.gold22;
            $scope.creditObject.ENTRY_GOLD21_CREDIT = $scope.notification.gold21;
            $scope.creditObject.ENTRY_GOLD18_CREDIT = $scope.notification.gold18;

            $scope.checkExist(accCode, $scope.creditObject);
        } else if ($scope.typeOfEntry == 11) {
            $scope.depitObject = {};
            $scope.depitObject.ACC_ID = $scope.entryMaster.ACC_ID;
            $scope.depitObject.ACC_CODE = accCode;
            $scope.depitObject.ACC_AR_NAME = accName;
            $scope.depitObject.ENTRY_DEBIT = $scope.notification.moneyValue;
            $scope.depitObject.ENTRY_GOLD24_DEBIT = $scope.notification.gold24;
            $scope.depitObject.ENTRY_GOLD22_DEBIT = $scope.notification.gold22;
            $scope.depitObject.ENTRY_GOLD21_DEBIT = $scope.notification.gold21;
            $scope.depitObject.ENTRY_GOLD18_DEBIT = $scope.notification.gold18;

            $scope.checkExist(accCode, $scope.depitObject);
        }
    }
    $scope.goldCredit = function (data, no) {
        if (data != null) {
            $scope.moreInfoGoldBalance(data);
        }
    }
    $scope.moreInfoGoldBalance = function (data) {
        commonService.getAccountValByaccID(data).then(function (results) {});
    }



    $scope.changeAccountBalance = function (id) {
        commonService.getAccountBalanceByID(id).then(function (results) {
            $scope.accountBalance = results;
        });
    }



    ///////////////////////////////////////////////////////complex entry second grid data//////////////////////////////////////////////

    $scope.searchForAccountsGrid2 = function () {

        if ($scope.search2 != null && $scope.search2 != undefined && $scope.search2 != "") {
            $scope.searchBy2 = $scope.search2
        }

        if ($scope.searchBy2 == null || $scope.searchBy2 == undefined || $scope.searchBy2 == "") {
            // if ($scope.searchCode != null && $scope.searchCode != undefined)
            return;
        } else {
            //search for Item 
            // entryService.searchAccounts($scope.searchBy2).then(function (response) {
            entryService.searchAccountsForCreditGrid($scope.searchBy2, $scope.typeOfEntry).then(function (response) {
                $scope.searchItems2 = response.data;
                if ($scope.searchItems2.length == 1) {

                    $scope.checkExist2($scope.searchItems2[0].ACC_CODE, $scope.searchItems2[0]);

                } else if ($scope.searchItems2.length == 0) {
                    $scope.searchItems2 = [];
                } else {
                    $scope.checkSelected2();
                    if ($scope.modalOpenItm === false || $scope.modalOpenItm == undefined) {
                        $scope.open2();
                    }
                }
            })
        }
    }



    $scope.selectedList2 = [];
    //check selected accounts
    $scope.checkSelected2 = function () {
        if ($scope.searchBy2 != null || $scope.searchBy2 != undefined) {
            for (var i = 0; i < $scope.searchItems2.length; i++) {
                if ($scope.searchItems2[i].Selected == true) {
                    $scope.checkExist2($scope.searchItems2[i].ACC_CODE, $scope.searchItems2[i]);
                    $scope.selectedList2.push($scope.searchItems2[i]);
                }

            }
        }
    }


    //check if account already exist on grid if not add it
    $scope.checkExist2 = function (accountcode, item) {
        var found = false;
        var emptyIndex = 0;
        for (var i = 0; i < $scope.entryDetails2.length; i++) {
            if (String($scope.entryDetails2[i].ACC_CODE) == accountcode) {
                found = true;
            }

            if ($scope.entryDetails2[i].ACC_CODE == "") {
                // $scope.entryDetails.splice(i, 1);
                emptyIndex = i;
            }
        }
        if (!found) {
            $scope.addAccountToGrid2(item, emptyIndex);

        } else {
            if ($scope.IsRepeatItem) {
                $scope.addAccountToGrid2(item, emptyIndex);
            }
        }
    }


    // add account to grid 2
    $scope.addAccountToGrid2 = function (item, emptyIndex) {
        $scope.entryDetails2.splice(emptyIndex, 1);
        $scope.entryDetailsEntity2 = {
            ACC_ID: null,
            ACC_CODE: "",
            ACC_AR_NAME: "",
            ENTRY_CREDIT: 0,
            ENTRY_GOLD_DEBIT: 0,
            ENTRY_GOLD_CREDIT: 0,
            ENTRY_DEBIT: 0,
            ENTRY_DETAILS_REMARKS: "",
            COST_CENTER_ID: 0,
            TaxValue: 0,
            Taxable: false
        };
        $scope.entryDetailsEntity2.ACC_ID = item.ACC_ID;
        $scope.entryDetailsEntity2.ACC_CODE = item.ACC_CODE;
        $scope.entryDetailsEntity2.ACC_AR_NAME = item.ACC_AR_NAME;
        $scope.entryDetailsEntity2.TaxAccountID = item.TaxAccountID;


        $scope.entryDetailsEntity2.Taxable = item.SubjectToVAT;

        if (item.SubjectToVAT == true) {
            $scope.entryDetailsEntity2.TaxRate = item.VATRate;
        } else {
            $scope.entryDetailsEntity2.TaxRate = 0;
        }

        $scope.entryDetailsEntity2.ENTRY_CREDIT = item.ENTRY_CREDIT;
        $scope.entryDetailsEntity2.ENTRY_GOLD24_CREDIT = item.ENTRY_GOLD24_CREDIT;
        $scope.entryDetailsEntity2.ENTRY_GOLD22_CREDIT = item.ENTRY_GOLD22_CREDIT;
        $scope.entryDetailsEntity2.ENTRY_GOLD21_CREDIT = item.ENTRY_GOLD21_CREDIT;
        $scope.entryDetailsEntity2.ENTRY_GOLD18_CREDIT = item.ENTRY_GOLD18_CREDIT;

        $scope.entryDetails2.push($scope.entryDetailsEntity2);

        $scope.entryObj2 = {
            ACC_ID: null,
            ACC_CODE: "",
            ACC_AR_NAME: "",
            ENTRY_CREDIT: 0,
            ENTRY_GOLD_DEBIT: 0,
            ENTRY_GOLD_CREDIT: 0,
            ENTRY_DEBIT: 0,
            ENTRY_DETAILS_REMARKS: "",
            COST_CENTER_ID: 0,
            TaxValue: 0,
            Taxable: false
        }
        $scope.entryDetails2.push($scope.entryObj2);
        $scope.calculateTotals();
    }


    // open search items 2 for second grid
    $scope.open2 = function () {
        var parentElem = angular.element($document[0].querySelector('.app-content'));
        $scope.modalOpenItm = false;
        var modalInstance = $uibModal.open({
            templateUrl: 'myModalContent2.html',
            controller: 'modalCtrl',
            scope: $scope,
            preserveScope: true
        });
        modalInstance.opened.then(function () {
            $scope.modalOpenItm = true;
        });

        // we want to update state whether the modal closed or was dismissed,
        // so use finally to handle both resolved and rejected promises.
        modalInstance.result.finally(function (selectedItem) {
            $scope.modalOpenItm = false;
        });
    };


    // grid 2 events 
    $scope.updateItemListOnChange2 = function (cellName, data, index) {

        if (cellName == 'ENTRY_CREDIT') {
            if (data == null || data == undefined) {
                data = 0;
            }
            if (parseInt(data) > 0) {
                $scope.entryDetails2[index].ENTRY_DEBIT = 0;
                $scope.entryDetails2[index].ENTRY_CREDIT = data;

                if ($scope.typeOfEntry != 88 && $scope.typeOfEntry != 89 && $scope.IsTaxesIncluded == true) {
                    if ($scope.entryDetails2[index].Taxable && $scope.entryDetails2[index].TaxRate != undefined && $scope.entryDetails2[index].TaxRate != null && $scope.entryDetails2[index].TaxRate != 0) {
                        $scope.entryDetails2[index].CreditValueWithoutTax = parseFloat(data * (1 / (1 + parseFloat($scope.entryDetails2[index].TaxRate) / 100))).toFixed(2);
                        $scope.entryDetails2[index].TaxValue = parseFloat(data - $scope.entryDetails2[index].CreditValueWithoutTax).toFixed(2);
                    }
                } else {
                    if ($scope.entryDetails2[index].TaxRate != undefined && $scope.entryDetails2[index].TaxRate != null && $scope.entryDetails2[index].TaxRate != 0 && ($scope.entryDetails2[index].TaxValue == undefined || $scope.entryDetails2[index].TaxValue == 0 || $scope.entryDetails2[index].TaxValue == null)) {
                        $scope.entryDetails2[index].TaxValue = ((parseFloat($scope.entryDetails2[index].TaxRate) / 100) * parseFloat($scope.entryDetails2[index].ENTRY_CREDIT)).toFixed(2);
                    } else if ($scope.entryDetails2[index].TaxValue != undefined && $scope.entryDetails2[index].TaxValue != null && $scope.entryDetails2[index].TaxValue != 0 && ($scope.entryDetails2[index].TaxRate == undefined || $scope.entryDetails2[index].TaxRate == 0 || $scope.entryDetails2[index].TaxRate == null)) {
                        $scope.entryDetails2[index].TaxRate = (parseFloat($scope.entryDetails2[index].TaxValue * 100) / parseFloat($scope.entryDetails2[index].ENTRY_CREDIT)).toFixed(2);
                    } else if (($scope.entryDetails2[index].TaxRate != undefined && $scope.entryDetails2[index].TaxRate != null && $scope.entryDetails2[index].TaxRate != 0) && ($scope.entryDetails2[index].TaxValue != undefined && $scope.entryDetails2[index].TaxValue != null && $scope.entryDetails2[index].TaxValue != 0)) {
                        $scope.entryDetails2[index].TaxValue = ((parseFloat($scope.entryDetails2[index].TaxRate) / 100) * parseFloat($scope.entryDetails2[index].ENTRY_CREDIT)).toFixed(2);
                    }
                }
            }
            $scope.addTaxAfterCheckTaxableCredit(index);
            $scope.calculateTotals();
        } else if (cellName == 'TaxRate') {
            if (data == null || data == undefined) {
                data = 0;
            }

            $scope.entryDetails2[index].TaxRate = data;

            if ($scope.entryDetails2[index].ENTRY_CREDIT != undefined && $scope.entryDetails2[index].ENTRY_CREDIT != 0) {

                if ($scope.IsTaxesIncluded) {
                    var creditBeforeTax = (parseFloat($scope.entryDetails2[index].ENTRY_CREDIT) * (100 / (100 + parseFloat($scope.entryDetails2[index].TaxRate)))).toFixed(2);
                    $scope.entryDetails2[index].CreditValueWithoutTax = creditBeforeTax;
                    $scope.entryDetails2[index].TaxValue = (parseFloat($scope.entryDetails2[index].ENTRY_CREDIT) - parseFloat(creditBeforeTax)).toFixed(2);
                } else {

                    $scope.entryDetails2[index].TaxValue = ((parseFloat($scope.entryDetails2[index].TaxRate) / 100) * parseFloat($scope.entryDetails2[index].ENTRY_CREDIT)).toFixed(2);
                }
            } else if ($scope.entryDetails2[index].ENTRY_DEBIT != undefined && $scope.entryDetails2[index].ENTRY_DEBIT != 0) {
                $scope.entryDetails2[index].TaxValue = ((parseFloat($scope.entryDetails2[index].TaxRate) / 100) * parseFloat($scope.entryDetails2[index].ENTRY_DEBIT)).toFixed(2);
            }




            /////////separte taxes
            if ($scope.entryDetails2[index].Taxable == true) {
                if ($scope.entryDetails2[index].TaxRate == $scope.entryMaster.MainVatRate) {
                    $scope.entryDetails2[index].IsMainVatValue = true;
                    $scope.entryDetails2[index].MainVatValue = ($scope.entryDetails2[index].ENTRY_CREDIT);
                    $scope.entryDetails2[index].MainVat = $scope.entryDetails2[index].TaxValue;

                    $scope.entryDetails2[index].IsZeroVatValue = false;
                    $scope.entryDetails2[index].ZeroVatValue = 0;
                    $scope.entryDetails2[index].IsExemptOfTax = false;
                    $scope.entryDetails2[index].ExemptOfTaxValue = 0;
                } else if ($scope.entryDetails2[index].TaxRate == 0) {
                    $scope.entryDetails2[index].IsMainVatValue = false;
                    $scope.entryDetails2[index].MainVatValue = 0;
                    $scope.entryDetails2[index].MainVat = 0;

                    $scope.entryDetails2[index].IsZeroVatValue = true;
                    $scope.entryDetails2[index].ZeroVatValue = ($scope.entryDetails2[index].ENTRY_CREDIT);

                    $scope.entryDetails[index].IsExemptOfTax = false;
                    $scope.entryDetails[index].ExemptOfTaxValue = 0;
                } else {
                    $scope.entryDetails2[index].IsMainVatValue = false;
                    $scope.entryDetails2[index].MainVatValue = 0;
                    $scope.entryDetails2[index].MainVat = 0;

                    $scope.entryDetails2[index].IsZeroVatValue = false;
                    $scope.entryDetails2[index].ZeroVatValue = 0;

                    $scope.entryDetails2[index].IsExemptOfTax = false;
                    $scope.entryDetails2[index].ExemptOfTaxValue = 0;

                }

            } else {
                $scope.entryDetails2[index].IsMainVatValue = false;
                $scope.entryDetails2[index].MainVatValue = 0;
                $scope.entryDetails2[index].MainVat = 0;

                $scope.entryDetails2[index].IsZeroVatValue = false;
                $scope.entryDetails2[index].ZeroVatValue = 0;

                $scope.entryDetails2[index].IsExemptOfTax = false;
                $scope.entryDetails2[index].ExemptOfTaxValue = 0;

            }




            $scope.addTaxAfterCheckTaxableCredit(index);
            $scope.calculateTotals();
        } else if (cellName == 'TaxValue') {
            if (data == null || data == undefined) {
                data = 0;
            }
            $scope.entryDetails2[index].TaxValue = data;


            if ($scope.entryDetails2[index].ENTRY_CREDIT != undefined && $scope.entryDetails2[index].ENTRY_CREDIT != 0) {

                if ($scope.IsTaxesIncluded) {
                    var creditBeforTax = parseFloat($scope.entryDetails2[index].ENTRY_CREDIT) - parseFloat($scope.entryDetails2[index].TaxValue);
                    $scope.entryDetails2[index].CreditValueWithoutTax = creditBeforeTax;
                    $scope.entryDetails2[index].TaxRate = (parseFloat($scope.entryDetails2[index].TaxValue * 100) / parseFloat(creditBeforTax)).toFixed(2);
                } else {
                    $scope.entryDetails2[index].TaxRate = (parseFloat($scope.entryDetails2[index].TaxValue * 100) / parseFloat($scope.entryDetails2[index].ENTRY_CREDIT)).toFixed(2);
                }
            } else if ($scope.entryDetails2[index].ENTRY_DEBIT != undefined && $scope.entryDetails2[index].ENTRY_DEBIT != 0) {
                $scope.entryDetails2[index].TaxRate = (parseFloat($scope.entryDetails2[index].TaxValue * 100) / parseFloat($scope.entryDetails2[index].ENTRY_DEBIT)).toFixed(2);
            }



            /////////separte taxes
            if ($scope.entryDetails2[index].Taxable == true) {
                if ($scope.entryDetails2[index].TaxRate == $scope.entryMaster.MainVatRate) {
                    $scope.entryDetails2[index].IsMainVatValue = true;
                    $scope.entryDetails2[index].MainVatValue = ($scope.entryDetails2[index].ENTRY_CREDIT);
                    $scope.entryDetails2[index].MainVat = $scope.entryDetails2[index].TaxValue;

                    $scope.entryDetails2[index].IsZeroVatValue = false;
                    $scope.entryDetails2[index].ZeroVatValue = 0;
                    $scope.entryDetails2[index].IsExemptOfTax = false;
                    $scope.entryDetails2[index].ExemptOfTaxValue = 0;
                } else if ($scope.entryDetails2[index].TaxRate == 0) {
                    $scope.entryDetails2[index].IsMainVatValue = false;
                    $scope.entryDetails2[index].MainVatValue = 0;
                    $scope.entryDetails2[index].MainVat = 0;

                    $scope.entryDetails2[index].IsZeroVatValue = true;
                    $scope.entryDetails2[index].ZeroVatValue = ($scope.entryDetails2[index].ENTRY_CREDIT);

                    $scope.entryDetails[index].IsExemptOfTax = false;
                    $scope.entryDetails[index].ExemptOfTaxValue = 0;
                } else {
                    $scope.entryDetails2[index].IsMainVatValue = false;
                    $scope.entryDetails2[index].MainVatValue = 0;
                    $scope.entryDetails2[index].MainVat = 0;

                    $scope.entryDetails2[index].IsZeroVatValue = false;
                    $scope.entryDetails2[index].ZeroVatValue = 0;

                    $scope.entryDetails2[index].IsExemptOfTax = false;
                    $scope.entryDetails2[index].ExemptOfTaxValue = 0;

                }

            } else {
                $scope.entryDetails2[index].IsMainVatValue = false;
                $scope.entryDetails2[index].MainVatValue = 0;
                $scope.entryDetails2[index].MainVat = 0;

                $scope.entryDetails2[index].IsZeroVatValue = false;
                $scope.entryDetails2[index].ZeroVatValue = 0;

                $scope.entryDetails2[index].IsExemptOfTax = false;
                $scope.entryDetails2[index].ExemptOfTaxValue = 0;

            }






            $scope.addTaxAfterCheckTaxableCredit(index);
            $scope.calculateTotals();
        } else if (cellName == 'Taxable') {
             
            $scope.creditTaxAccountComplix = true;
            var x = $scope.entryDetails2[index].Taxable;

            if ($scope.entryDetails2[index].Taxable == false) {

                $scope.RemoveTaxAfterUnCheckTaxableCredit(index, $scope.entryDetails2[index].TaxValue);

                $scope.entryDetails2[index].TaxValue = 0;
                $scope.entryDetails2[index].TaxRate = 0;
            } else {
                $scope.entryDetails2[index].IsExemptOfTax = false;
            }

            if ($scope.entryDetails2[index].Taxable == true) {
                $scope.addTaxAccountForEachAccount(index);
            }






            if ($scope.entryDetails2[index].Taxable == true) {
                if ($scope.entryDetails2[index].TaxRate == $scope.entryMaster.MainVatRate) {
                    $scope.entryDetails2[index].IsMainVatValue = true;
                    $scope.entryDetails2[index].MainVatValue = ($scope.entryDetails2[index].ENTRY_CREDIT);
                    $scope.entryDetails2[index].MainVat = $scope.entryDetails2[index].TaxValue;

                    $scope.entryDetails2[index].IsZeroVatValue = false;
                    $scope.entryDetails2[index].ZeroVatValue = 0;
                    $scope.entryDetails2[index].IsExemptOfTax = false;
                    $scope.entryDetails2[index].ExemptOfTaxValue = 0;
                } else if ($scope.entryDetails2[index].TaxRate == 0) {
                    $scope.entryDetails2[index].IsMainVatValue = false;
                    $scope.entryDetails2[index].MainVatValue = 0;
                    $scope.entryDetails2[index].MainVat = 0;

                    $scope.entryDetails2[index].IsZeroVatValue = true;
                    $scope.entryDetails2[index].ZeroVatValue = ($scope.entryDetails2[index].ENTRY_CREDIT);

                    $scope.entryDetails2[index].IsExemptOfTax = false;
                    $scope.entryDetails2[index].ExemptOfTaxValue = 0;
                } else {
                    $scope.entryDetails2[index].IsMainVatValue = false;
                    $scope.entryDetails2[index].MainVatValue = 0;
                    $scope.entryDetails2[index].MainVat = 0;

                    $scope.entryDetails2[index].IsZeroVatValue = false;
                    $scope.entryDetails2[index].ZeroVatValue = 0;

                    $scope.entryDetails2[index].IsExemptOfTax = false;
                    $scope.entryDetails2[index].ExemptOfTaxValue = 0;
                }
            } else {
                $scope.entryDetails2[index].IsMainVatValue = false;
                $scope.entryDetails2[index].MainVatValue = 0;
                $scope.entryDetails2[index].MainVat = 0;

                $scope.entryDetails2[index].IsZeroVatValue = false;
                $scope.entryDetails2[index].ZeroVatValue = 0;

                $scope.entryDetails2[index].IsExemptOfTax = false;
                $scope.entryDetails2[index].ExemptOfTaxValue = 0;

            }




            $scope.calculateTotals();

            //if ($scope.entryDetails2[index].Taxable == true) {
            //    if (index == 0) {
            //        $scope.addTaxAccount();
            //    }
            //}
        } else if (cellName == 'GOLD_CREDIT24') {
            if (data == null || data == undefined) {
                data = 0;
            }

            if (parseInt(data) > 0) {
                // $scope.entryDetails2[index].ENTRY_GOLD24_DEBIT = 0;
                $scope.entryDetails2[index].ENTRY_GOLD24_CREDIT = data;
            }

            $scope.calculateTotals();
        } else if (cellName == 'GOLD_DEBIT24') {
            if (data == null || data == undefined) {
                data = 0;
            }

            if (parseInt(data) > 0) {
                $scope.entryDetails2[index].ENTRY_GOLD24_CREDIT = 0;
                // $scope.entryDetails2[index].ENTRY_GOLD24_DEBIT = data;
            }

            $scope.calculateTotals();
        } else if (cellName == 'GOLD_CREDIT22') {
            if (data == null || data == undefined) {
                data = 0;
            }

            if (parseInt(data) > 0) {
                // $scope.entryDetails2[index].ENTRY_GOLD22_DEBIT = 0;
                $scope.entryDetails2[index].ENTRY_GOLD22_CREDIT = data;
            }

            $scope.calculateTotals();
        } else if (cellName == 'GOLD_DEBIT22') {
            if (data == null || data == undefined) {
                data = 0;
            }

            if (parseInt(data) > 0) {
                $scope.entryDetails2[index].ENTRY_GOLD22_CREDIT = 0;
                //  $scope.entryDetails2[index].ENTRY_GOLD22_DEBIT = data;
            }

            $scope.calculateTotals();
        } else if (cellName == 'GOLD_CREDIT21') {
            if (data == null || data == undefined) {
                data = 0;
            }

            if (parseInt(data) > 0) {
                // $scope.entryDetails2[index].ENTRY_GOLD21_DEBIT = 0;
                $scope.entryDetails2[index].ENTRY_GOLD21_CREDIT = data;
            }

            $scope.calculateTotals();
        } else if (cellName == 'GOLD_DEBIT21') {
            if (data == null || data == undefined) {
                data = 0;
            }

            if (parseInt(data) > 0) {
                $scope.entryDetails2[index].ENTRY_GOLD21_CREDIT = 0;
                // $scope.entryDetails2[index].ENTRY_GOLD21_DEBIT = data;
            }

            $scope.calculateTotals();
        } else if (cellName == 'GOLD_CREDIT18') {
            if (data == null || data == undefined) {
                data = 0;
            }

            if (parseInt(data) > 0) {
                //  $scope.entryDetails2[index].ENTRY_GOLD18_DEBIT = 0;
                $scope.entryDetails2[index].ENTRY_GOLD18_CREDIT = data;
            }

            $scope.calculateTotals();
        } else if (cellName == 'GOLD_DEBIT18') {
            if (data == null || data == undefined) {
                data = 0;
            }

            if (parseInt(data) > 0) {
                $scope.entryDetails2[index].ENTRY_GOLD18_CREDIT = 0;
                //$scope.entryDetails2[index].ENTRY_GOLD18_DEBIT = data;
            }

            $scope.calculateTotals();
        } else if (cellName == 'CheckNumber') {
            if (data == null || data == undefined) {
                data = 0;
            }
            if (parseInt(data) > 0) {
                $scope.entryDetails2[index].CheckNumber = data;
            }

        } else if (cellName == 'IsExemptOfTax') {

            if ($scope.entryDetails2[index].IsExemptOfTax == true) {


                $scope.RemoveTaxAfterUnCheckTaxableCredit(index, $scope.entryDetails2[index].TaxValue);
                $scope.entryDetails2[index].Taxable = false;
                $scope.entryDetails2[index].TaxValue = 0;
                $scope.entryDetails2[index].TaxRate = 0;
            }

            $scope.calculateTotals();
        }


        if ($scope.entryDetails2[index].IsExemptOfTax == true) {
            $scope.entryDetails2[index].Taxable = false;
            $scope.RemoveTaxAfterUnCheckTaxableCredit(index, $scope.entryDetails2[index].TaxValue);
            $scope.entryDetails2[index].TaxValue = 0;
            $scope.entryDetails2[index].TaxRate = 0;




            $scope.entryDetails2[index].IsMainVatValue = false;
            $scope.entryDetails2[index].MainVatValue = 0;
            $scope.entryDetails2[index].MainVat = 0;

            $scope.entryDetails2[index].IsZeroVatValue = false;
            $scope.entryDetails2[index].ZeroVatValue = 0;

            $scope.entryDetails2[index].IsExemptOfTax = true;
            $scope.entryDetails2[index].ExemptOfTaxValue = ($scope.entryDetails2[index].ENTRY_CREDIT);

        }


    }


    //calculateTotals of second grid

    $scope.calculateTotalsGrid2 = function () {

        $scope.totalCreditGrd2 = 0;

        $scope.totalTaxRateCreditGrd2 = 0;

        $scope.totalTaxValueCreditGrd2 = 0;

        $scope.totalNotTaxCreditGrd2 = 0;

        $scope.totalGoldCredit24Grd2 = 0;

        $scope.totalGoldCredit22Grd2 = 0;

        $scope.totalGoldCredit21Grd2 = 0;

        $scope.totalGoldCredit18Grd2 = 0;

        $scope.totalCreditWithTaxGrd2 = 0;

        $scope.creditTaxIncludedBefore2 = 0;
        $scope.totalCreditTaxIncludedBefore2 = 0;


        $scope.cridetTotalExemptOfTax2 = 0;
        $scope.cridetExemptOfTaxValue2 = 0;

        $scope.cridetTotalMainTax2 = 0;
        $scope.cridetMainTaxValue2 = 0;


        $scope.cridetTotalZeroTax2 = 0;
        $scope.cridetZeroTaxValue2 = 0;





        for (var i = 0; i < $scope.entryDetails2.length; i++) {
            if ($scope.entryDetails2[i].ACC_ID != null && $scope.entryDetails2[i].ACC_ID != "" && $scope.entryDetails2[i].ACC_ID != undefined) {

                if ($scope.entryDetails2[i].ENTRY_CREDIT != null && $scope.entryDetails2[i].ENTRY_CREDIT != undefined && $scope.entryDetails2[i].ENTRY_CREDIT != 0) {

                    if ($scope.settingId == 130) {
                        $scope.totalCreditGrd2 = (parseFloat($scope.entryDetails2[i].ENTRY_CREDIT) + parseFloat($scope.totalCreditGrd2));
                    } else {
                        $scope.totalCreditGrd2 = (parseFloat($scope.entryDetails2[i].ENTRY_CREDIT) + parseFloat($scope.totalCreditGrd2));
                        //todo:check commented conditions
                        //if (($scope.EntryModeID == 2 && ($scope.typeOfEntry == 2 || $scope.typeOfEntry == 4) && $scope.IsTaxesIncluded == false && $scope.entryDetails2[i].ACC_ID != $scope.TaxAccountId)) {
                        //    $scope.totalCreditGrd2 = (parseFloat($scope.entryDetails2[i].ENTRY_CREDIT) + parseFloat($scope.totalCreditGrd2));
                        //}


                        //if (($scope.EntryModeID == 2 && ($scope.typeOfEntry == 2 || $scope.typeOfEntry == 4) && $scope.IsTaxesIncluded == false)) {
                        //    $scope.totalCreditGrd2 = (parseFloat($scope.entryDetails2[i].ENTRY_CREDIT) + parseFloat($scope.totalCreditGrd2));
                        //}

                        ////else if (($scope.EntryModeID == 2 && ($scope.typeOfEntry != 2 && $scope.typeOfEntry != 4) && $scope.IsTaxesIncluded == true)) {
                        //else if (($scope.EntryModeID == 2 && ($scope.typeOfEntry != 2 && $scope.typeOfEntry != 4))) {
                        //    $scope.totalCreditGrd2 = (parseFloat($scope.entryDetails2[i].ENTRY_CREDIT) + parseFloat($scope.totalCreditGrd2));
                        //}

                        ////else if ($scope.EntryModeID != 2) {
                        ////    $scope.totalCreditGrd2 = (parseFloat($scope.entryDetails2[i].ENTRY_CREDIT) + parseFloat($scope.totalCreditGrd2));
                        ////}


                        //if (($scope.EntryModeID == 2 && ($scope.typeOfEntry == 2 || $scope.typeOfEntry == 4) && $scope.entryDetails2[i].ACC_ID != $scope.TaxAccountId)) {
                        //    $scope.totalCreditGrd2 = (parseFloat($scope.entryDetails2[i].ENTRY_CREDIT) + parseFloat($scope.totalCreditGrd2));
                        //}
                        //else {
                        //    $scope.totalCreditGrd2 = (parseFloat($scope.entryDetails2[i].ENTRY_CREDIT) + parseFloat($scope.totalCreditGrd2));
                        //}
                    }
                }


                ////////////////////////////////////////////gold
                if ($scope.entryDetails2[i].ENTRY_GOLD24_CREDIT != null && $scope.entryDetails2[i].ENTRY_GOLD24_CREDIT != undefined && $scope.entryDetails2[i].ENTRY_GOLD24_CREDIT != 0) {
                    $scope.totalGoldCredit24Grd2 = (parseFloat($scope.entryDetails2[i].ENTRY_GOLD24_CREDIT) + parseFloat($scope.totalGoldCredit24Grd2));
                }


                if ($scope.entryDetails2[i].ENTRY_GOLD22_CREDIT != null && $scope.entryDetails2[i].ENTRY_GOLD22_CREDIT != undefined && $scope.entryDetails2[i].ENTRY_GOLD22_CREDIT != 0) {
                    $scope.totalGoldCredit22Grd2 = (parseFloat($scope.entryDetails2[i].ENTRY_GOLD22_CREDIT) + parseFloat($scope.totalGoldCredit22Grd2));
                }


                if ($scope.entryDetails2[i].ENTRY_GOLD21_CREDIT != null && $scope.entryDetails2[i].ENTRY_GOLD21_CREDIT != undefined && $scope.entryDetails2[i].ENTRY_GOLD21_CREDIT != 0) {
                    $scope.totalGoldCredit21Grd2 = (parseFloat($scope.entryDetails2[i].ENTRY_GOLD21_CREDIT) + parseFloat($scope.totalGoldCredit21Grd2));
                }


                if ($scope.entryDetails2[i].ENTRY_GOLD18_CREDIT != null && $scope.entryDetails2[i].ENTRY_GOLD18_CREDIT != undefined && $scope.entryDetails2[i].ENTRY_GOLD18_CREDIT != 0) {
                    $scope.totalGoldCredit18Grd2 = (parseFloat($scope.entryDetails2[i].ENTRY_GOLD18_CREDIT) + parseFloat($scope.totalGoldCredit18Grd2));
                }


                ////////////////////////////////////////////////taxable of details 

                if ($scope.entryMaster.Taxable == false || $scope.entryMaster.Taxable == null || $scope.entryMaster.Taxable == undefined) {





                    if ($scope.entryDetails2[i].IsExemptOfTax == true) {

                        //للدائن في السند العادي
                        if ($scope.entryDetails2[i].ENTRY_CREDIT != null && $scope.entryDetails2[i].ENTRY_CREDIT != undefined && $scope.entryDetails2[i].ENTRY_CREDIT != 0) {
                            $scope.cridetTotalExemptOfTax2 = parseFloat($scope.entryDetails2[i].ENTRY_CREDIT) + parseFloat($scope.cridetTotalExemptOfTax2);
                            //يوجد ضريبه للسند لحساب القيمه
                            if ($scope.entryMaster.ExemptVatRate != undefined && $scope.entryMaster.ExemptVatRate != null) {
                                $scope.cridetExemptOfTaxValue2 = ((parseFloat($scope.entryMaster.ExemptVatRate) * parseFloat($scope.cridetTotalExemptOfTax2)) / 100).toFixed(2);
                            }
                        }

                    } else {
                        if ($scope.entryDetails2[i].Taxable != null && $scope.entryDetails2[i].Taxable != undefined && $scope.entryDetails2[i].Taxable == true) {
                            if ($scope.entryDetails2[i].ENTRY_CREDIT != null && $scope.entryDetails2[i].ENTRY_CREDIT != undefined && $scope.entryDetails2[i].ENTRY_CREDIT != 0) {

                                if ($scope.entryDetails2[i].TaxRate != null && $scope.entryDetails2[i].TaxRate != undefined) {
                                    $scope.totalTaxRateCreditGrd2 = (parseFloat($scope.entryDetails2[i].TaxRate) + parseFloat($scope.totalTaxRateCreditGrd2)).toFixed(2);
                                }

                                if ($scope.entryDetails2[i].TaxValue != null && $scope.entryDetails2[i].TaxValue != undefined) {
                                    $scope.totalTaxValueCreditGrd2 = (parseFloat($scope.entryDetails2[i].TaxValue) + parseFloat($scope.totalTaxValueCreditGrd2)).toFixed(2);
                                }



                                // الضريبه الاساسيه و ضريبه الزيرو في حاله ان السند نوعه عادي 
                                if ($scope.entryDetails2[i].TaxRate == $scope.entryMaster.MainVatRate) {






                                    var beforetax = (parseFloat($scope.entryDetails2[i].ENTRY_CREDIT) * (100 / (100 + parseFloat($scope.entryMaster.MainVatRate)))).toFixed(2);

                                    $scope.cridetTotalMainTax2 = parseFloat(beforetax) + parseFloat($scope.cridetTotalMainTax2);
                                    //يوجد ضريبه للسند لحساب القيمه
                                    if ($scope.entryMaster.MainVatRate != undefined && $scope.entryMaster.MainVatRate != null) {
                                        $scope.cridetMainTaxValue2 = ((parseFloat($scope.entryMaster.MainVatRate) * parseFloat($scope.cridetTotalMainTax2)) / 100).toFixed(2);
                                    }


                                    //   $scope.cridetTotalMainTax2 = $scope.cridetTotalMainTax2 - $scope.cridetMainTaxValue2;

                                }


                                // الضريبه الاساسيه و ضريبه الزيرو في حاله ان السند نوعه عادي 
                                if ($scope.entryDetails2[i].TaxRate == 0 || $scope.entryDetails2[i].TaxRate == undefined) {
                                    $scope.cridetTotalZeroTax2 = parseFloat($scope.entryDetails2[i].ENTRY_CREDIT) + parseFloat($scope.cridetTotalZeroTax2);
                                    $scope.cridetZeroTaxValue2 = 0;
                                }



                            }
                        } else {
                            if ($scope.entryDetails2[i].ENTRY_CREDIT != null && $scope.entryDetails2[i].ENTRY_CREDIT != undefined && $scope.entryDetails2[i].ENTRY_CREDIT != 0) {
                                {
                                    $scope.totalNotTaxCreditGrd2 = (parseFloat($scope.entryDetails2[i].ENTRY_CREDIT) + parseFloat($scope.totalNotTaxCreditGrd2));
                                }
                            }
                        }

                    }

                }

                if ($scope.entryMaster.Taxable == true) {
                    
                    if ($scope.entryTaxRate != null && $scope.entryTaxRate != undefined) {
                        if ($scope.IsTaxesIncluded) {
                            if ($scope.entryDetails2[i].ENTRY_CREDIT != null && $scope.entryDetails2[i].ENTRY_CREDIT != undefined && $scope.entryDetails2[i].ENTRY_CREDIT != 0) {
                                $scope.creditTaxIncludedBefore2 = (parseFloat($scope.entryDetails2[i].ENTRY_CREDIT) * (100 / (100 + parseFloat($scope.entryTaxRate)))).toFixed(2);
                                $scope.totalCreditTaxIncludedBefore2 = (parseFloat($scope.creditTaxIncludedBefore2) + parseFloat($scope.totalCreditTaxIncludedBefore2));
                            }

                            $scope.totalCreditWithTaxGrd2 = parseFloat($scope.totalCreditTaxIncludedBefore2).toFixed(2);
                            $scope.totalNotTaxCreditGrd2 = 0;
                        } else {
                            //$scope.totalCreditWithTaxGrd2 = (parseFloat($scope.totalCreditGrd2) * (parseFloat($scope.entryTaxRate) / 100)).toFixed(2);
                            $scope.totalCreditWithTaxGrd2 = (parseFloat($scope.totalCreditGrd2)).toFixed(2);
                            $scope.totalNotTaxCreditGrd2 = 0;
                        }
                    }
                } else {

                    $scope.totalCreditWithTaxGrd2 = (parseFloat($scope.totalCreditGrd2)).toFixed(2);
                    //  $scope.totalNotTaxCreditGrd2 = (parseFloat($scope.totalDepit)).toFixed(2);


                    //$scope.totalCreditWithTaxGrd2 = 0;
                    //$scope.totalNotTaxCreditGrd2 = 0;
                }
            }
        }
    }


    $scope.removeGridBillDetailsItem2 = function (index) {
        $scope.entryDetails2.splice(index, 1);
    };


    //GetAllCustomersAccounts
    $scope.getCustomerAccountsForComplexEntry = function () {
        $scope.complexEntryCustomersAccounts = [];
        accountsService.getCustomerAccountsForComplexEntry().then(function (result) {
            

            $scope.complexEntryCustomersAccounts = result.data;
        });

    }



    $scope.checkBalanceOfComplexentry = function () {

        var calcDmoney = 0;
        var calcCmoney = 0;
        var calcD24 = 0;
        var calcD22 = 0;
        var calcD21 = 0;
        var calcD18 = 0;
        var calcC24 = 0;
        var calcC22 = 0;
        var calcC21 = 0;
        var calcC18 = 0;

        $scope.diffMoney = 0;
        $scope.diff24 = 0;
        $scope.diff22 = 0;
        $scope.diff21 = 0;
        $scope.diff18 = 0;
        $scope.diffName = "";
        $scope.diffGold = 0;
        
        var rate22 = parseFloat(22 / 24);
        var rate21 = parseFloat(21 / 24);
        var rate18 = parseFloat(18 / 24);

        for (var i = 0; i < $scope.entryDetails.length; i++) {

            if ($scope.entryDetails[i].ENTRY_DEBIT != undefined) {
                var dMoney = (parseFloat($scope.entryDetails[i].ENTRY_DEBIT)).toFixed(2);
                calcDmoney = (parseFloat(dMoney) + parseFloat(calcDmoney)).toFixed(2);
            }

            if ($scope.entryDetails[i].ENTRY_GOLD24_DEBIT != undefined) {
                var d24 = (parseFloat($scope.entryDetails[i].ENTRY_GOLD24_DEBIT)).toFixed(2);
                calcD24 = (parseFloat(d24) + parseFloat(calcD24)).toFixed(2);
            }

            if ($scope.entryDetails[i].ENTRY_GOLD22_DEBIT != undefined) {
                var d22 = (parseFloat($scope.entryDetails[i].ENTRY_GOLD22_DEBIT)).toFixed(2);
                calcD22 = ((parseFloat(d22) * parseFloat(rate22)) + parseFloat(calcD22)).toFixed(2);
            }

            if ($scope.entryDetails[i].ENTRY_GOLD21_DEBIT != undefined) {
                var d21 = parseFloat($scope.entryDetails[i].ENTRY_GOLD21_DEBIT).toFixed(2);
                calcD21 = ((parseFloat(d21) * parseFloat(rate21)) + parseFloat(calcD21)).toFixed(2);
            }

            if ($scope.entryDetails[i].ENTRY_GOLD18_DEBIT != undefined) {
                var d18 = parseFloat($scope.entryDetails[i].ENTRY_GOLD18_DEBIT).toFixed(2);
                calcD18 = ((parseFloat(d18) * parseFloat(rate18)) + parseFloat(calcD18)).toFixed(2);
            }
        }




        for (var i = 0; i < $scope.entryDetails2.length; i++) {
            if ($scope.entryDetails2[i].ENTRY_CREDIT != undefined) {
                var cMoney = parseFloat($scope.entryDetails2[i].ENTRY_CREDIT).toFixed(2);
                calcCmoney = (parseFloat(cMoney) + parseFloat(calcCmoney)).toFixed(2);
            }

            if ($scope.entryDetails2[i].ENTRY_GOLD24_CREDIT != undefined) {
                var c24 = parseFloat($scope.entryDetails2[i].ENTRY_GOLD24_CREDIT).toFixed(2);
                calcC24 = (parseFloat(c24) + parseFloat(calcC24)).toFixed(2);
            }

            if ($scope.entryDetails2[i].ENTRY_GOLD22_CREDIT != undefined) {
                var c22 = parseFloat($scope.entryDetails2[i].ENTRY_GOLD22_CREDIT).toFixed(2);
                calcC22 = ((parseFloat(c22) * parseFloat(rate22)) + parseFloat(calcC22)).toFixed(2);
            }

            if ($scope.entryDetails2[i].ENTRY_GOLD21_CREDIT != undefined) {
                var c21 = parseFloat($scope.entryDetails2[i].ENTRY_GOLD21_CREDIT).toFixed(2);
                calcC21 = ((parseFloat(c21) * parseFloat(rate21)) + parseFloat(calcC21)).toFixed(2);
            }

            if ($scope.entryDetails2[i].ENTRY_GOLD18_CREDIT != undefined) {
                var c18 = parseFloat($scope.entryDetails2[i].ENTRY_GOLD18_CREDIT).toFixed(2);
                calcC18 = ((parseFloat(c18) * parseFloat(rate18)) + parseFloat(calcC18)).toFixed(2);
            }
        }

        $scope.diffMoney = (parseFloat(calcDmoney) - parseFloat(calcCmoney)).toFixed(2);
        $scope.diffGold = ((parseFloat(calcD24) + parseFloat(calcD22) + parseFloat(calcD21) + parseFloat(calcD18)) - (parseFloat(calcC24) + parseFloat(calcC22) + parseFloat(calcC21) + parseFloat(calcC18))).toFixed(2);

        if ($scope.diffMoney != 0) {
            if ($scope.diffMoney) {
                $scope.diffName = "مدين"
            } else {
                $scope.diffName = "دائن"
            }
        }

        if ($scope.diffGold != 0) {
            if ($scope.diffGold) {
                $scope.diffName = "مدين"
            } else {
                $scope.diffName = "دائن"
            }
        }


    }

    $scope.addComplexEntryRow = function () {
        
        var accBoxCode;
        var accBoxName;
        var accBoxID;

        var accCustCode;
        var accCustName;
        var accCustID;
        var accTaxAccId;

        var obj = {};

        for (var i = 0; i < $scope.BoxAccounts.length; i++) {
            if ($scope.BoxAccounts[i].ACC_ID == $scope.entryMaster.ACC_ID) {
                accBoxCode = $scope.BoxAccounts[i].ACC_CODE;
                accBoxName = $scope.BoxAccounts[i].ACC_AR_NAME;
                accBoxID = $scope.BoxAccounts[i].ACC_ID;
                accTaxAccId = $scope.BoxAccounts[i].TaxAccountID;
                break;
            }
        }
                   var obj2 = {};

            for (var i = 0; i < $scope.complexEntryCustomersAccounts.length; i++) {
                if ($scope.complexEntryCustomersAccounts[i].ACC_ID == $scope.entryMaster.CustAccID) {
                    accCustCode = $scope.complexEntryCustomersAccounts[i].ACC_CODE;
                    accCustName = $scope.complexEntryCustomersAccounts[i].ACC_AR_NAME;
                    accCustID = $scope.complexEntryCustomersAccounts[i].ACC_ID;
                    //  accTaxAccId = $scope.complexEntryCustomersAccounts[i].TaxAccountID;
                    break;
                }
            }

           



        $scope.emptyObj = {
            ACC_ID: null,
            ACC_CODE: "",
            ACC_AR_NAME: "",
            ENTRY_CREDIT: 0,
            ENTRY_GOLD_DEBIT: 0,
            ENTRY_GOLD_CREDIT: 0,
            ENTRY_DEBIT: 0,
            ENTRY_DETAILS_REMARKS: "",
            COST_CENTER_ID: 0,
            TaxValue: 0,
            Taxable: false
        }

        obj2.ACC_CODE = accCustCode;
        obj2.ACC_AR_NAME = accCustName;
        obj2.ACC_ID = accCustID;
        //  obj.TaxAccountID = accTaxAccId;


        $scope.emptyObj2 = {
            ACC_ID: null,
            ACC_CODE: "",
            ACC_AR_NAME: "",
            ENTRY_CREDIT: 0,
            ENTRY_GOLD_DEBIT: 0,
            ENTRY_GOLD_CREDIT: 0,
            ENTRY_DEBIT: 0,
            ENTRY_DETAILS_REMARKS: "",
            COST_CENTER_ID: 0,
            TaxValue: 0,
            Taxable: false
        }

        obj.ACC_CODE = accBoxCode;
        obj.ACC_AR_NAME = accBoxName;
        obj.ACC_ID = accBoxID;
        obj.TaxAccountID = accTaxAccId;

        if ($scope.typeOfEntry == 1 || $scope.typeOfEntry == 5 || $scope.typeOfEntry == 11) {
            //edit value
            if ($scope.entryDetails[0].ACC_ID != null && $scope.entryDetails[0].ACC_ID != "" && $scope.entryDetails[0].ACC_ID != undefined) {
                $scope.entryDetails[0].ENTRY_DEBIT = $scope.entryMaster.EntryVal;
                 $scope.entryDetails2[0].ENTRY_CREDIT = $scope.entryMaster.EntryVal;
                if ($scope.typeOfEntry == 11) {
                    $scope.entryDetails[0].ENTRY_GOLD24_DEBIT = $scope.notification.gold24;
                    $scope.entryDetails[0].ENTRY_GOLD22_DEBIT = $scope.notification.gold22;
                    $scope.entryDetails[0].ENTRY_GOLD21_DEBIT = $scope.notification.gold21;
                    $scope.entryDetails[0].ENTRY_GOLD18_DEBIT = $scope.notification.gold18;

                }

            }

            // add row
            else {
                $scope.entryDetails.splice(0, 1);
                $scope.entryDetails2.splice(0, 1);
                obj.ENTRY_DEBIT = $scope.entryMaster.EntryVal;
                obj2.ENTRY_CREDIT = $scope.entryMaster.EntryVal;
                if ($scope.typeOfEntry == 11) {
                    obj.ENTRY_GOLD24_DEBIT = $scope.notification.gold24;
                    obj.ENTRY_GOLD22_DEBIT = $scope.notification.gold22;
                    obj.ENTRY_GOLD21_DEBIT = $scope.notification.gold21;
                    obj.ENTRY_GOLD18_DEBIT = $scope.notification.gold18;


                    for (var i = 0; i < $scope.complexEntryCustomersAccounts.length; i++) {
                        if ($scope.complexEntryCustomersAccounts[i].ACC_ID == $scope.entryMaster.CustAccID) {
                            accBoxCode = $scope.complexEntryCustomersAccounts[i].ACC_CODE;
                            accBoxName = $scope.complexEntryCustomersAccounts[i].ACC_AR_NAME;
                            accBoxID = $scope.complexEntryCustomersAccounts[i].ACC_ID;
                            accTaxAccId = $scope.complexEntryCustomersAccounts[i].TaxAccountId;
                            break;
                        }
                    }
                    obj.ACC_CODE = accBoxCode;
                    obj.ACC_AR_NAME = accBoxName;
                    obj.ACC_ID = accBoxID;
                    obj.TaxAccountID = accTaxAccId;
                    //obj.ACC_ID = $scope.entryMaster.CustAccID;
                }
                
                obj.isComplixDepit = true;
                $scope.entryDetails.push(obj);
                $scope.entryDetails.push($scope.emptyObj);
                $scope.entryDetails2.push(obj2);
                $scope.entryDetails2.push($scope.emptyObj2);
            }
        } else if ($scope.typeOfEntry == 2 || $scope.typeOfEntry == 4 || $scope.typeOfEntry == 10) {

            if ($scope.entryDetails2[0].ACC_ID != null && $scope.entryDetails2[0].ACC_ID != "" && $scope.entryDetails2[0].ACC_ID != undefined) {
                $scope.entryDetails2[0].ENTRY_CREDIT = $scope.entryMaster.EntryVal;
                $scope.entryDetails[0].ENTRY_DEBIT = $scope.entryMaster.EntryVal;
           
                if ($scope.typeOfEntry == 10) {
                    $scope.entryDetails2[0].ENTRY_GOLD24_CREDIT = $scope.notification.gold24;
                    $scope.entryDetails2[0].ENTRY_GOLD22_CREDIT = $scope.notification.gold22;
                    $scope.entryDetails2[0].ENTRY_GOLD21_CREDIT = $scope.notification.gold21;
                    $scope.entryDetails2[0].ENTRY_GOLD18_CREDIT = $scope.notification.gold18;
                }
            } else {
                debugger
                $scope.entryDetails2.splice(0, 1);
                $scope.entryDetails.splice(0, 1);
                obj.ENTRY_CREDIT = $scope.entryMaster.EntryVal;
               // obj.ENTRY_DEBIT = $scope.entryMaster.EntryVal;
                obj2.ENTRY_DEBIT = $scope.entryMaster.EntryVal;
                if ($scope.typeOfEntry == 10) {
                    obj.ENTRY_GOLD24_CREDIT = $scope.notification.gold24;
                    obj.ENTRY_GOLD22_CREDIT = $scope.notification.gold22;
                    obj.ENTRY_GOLD21_CREDIT = $scope.notification.gold21;
                    obj.ENTRY_GOLD18_CREDIT = $scope.notification.gold18;



                    for (var i = 0; i < $scope.complexEntryCustomersAccounts.length; i++) {
                        if ($scope.complexEntryCustomersAccounts[i].ACC_ID == $scope.entryMaster.CustAccID) {
                            accBoxCode = $scope.complexEntryCustomersAccounts[i].ACC_CODE;
                            accBoxName = $scope.complexEntryCustomersAccounts[i].ACC_AR_NAME;
                            accBoxID = $scope.complexEntryCustomersAccounts[i].ACC_ID;
                            accTaxAccId = $scope.complexEntryCustomersAccounts[i].TaxAccountId;
                            break;
                        }
                    }
                    obj.ACC_CODE = accBoxCode;
                    obj.ACC_AR_NAME = accBoxName;
                    obj.ACC_ID = accBoxID;
                    obj.TaxAccountID = accTaxAccId;

                    //  obj.ACC_ID = $scope.entryMaster.CustAccID;
                }

                
                obj.isComplixCredit = true;
                $scope.entryDetails.push(obj2);
                $scope.entryDetails.push($scope.emptyObj2);
                $scope.entryDetails2.push(obj);
                $scope.entryDetails2.push($scope.emptyObj);
                   
            }
        }

        $scope.calculateTotals();
        //Report Exchange Reyal
        $scope.GetReportReportTotalsTafqeet();
    }


    //$scope.addComplexEntryRowInverse = function () {
    //    
    //    if ($scope.typeOfEntry != 11 && $scope.typeOfEntry != 10) {
    //        var accBoxCode;
    //        var accBoxName;
    //        var accBoxID;

    //        var accCustCode;
    //        var accCustName;
    //        var accCustID;
    //        var accTaxAccId;
    //        //  var TaxAccountID;

    //        var obj = {};

    //        for (var i = 0; i < $scope.complexEntryCustomersAccounts.length; i++) {
    //            if ($scope.complexEntryCustomersAccounts[i].ACC_ID == $scope.entryMaster.CustAccID) {
    //                accCustCode = $scope.complexEntryCustomersAccounts[i].ACC_CODE;
    //                accCustName = $scope.complexEntryCustomersAccounts[i].ACC_AR_NAME;
    //                accCustID = $scope.complexEntryCustomersAccounts[i].ACC_ID;
    //                //  accTaxAccId = $scope.complexEntryCustomersAccounts[i].TaxAccountID;
    //                break;
    //            }
    //        }

    //        obj.ACC_CODE = accCustCode;
    //        obj.ACC_AR_NAME = accCustName;
    //        obj.ACC_ID = accCustID;
    //        //  obj.TaxAccountID = accTaxAccId;

    //        $scope.emptyObj = {
    //            ACC_ID: null,
    //            ACC_CODE: "",
    //            ACC_AR_NAME: "",
    //            ENTRY_CREDIT: 0,
    //            ENTRY_GOLD_DEBIT: 0,
    //            ENTRY_GOLD_CREDIT: 0,
    //            ENTRY_DEBIT: 0,
    //            ENTRY_DETAILS_REMARKS: "",
    //            COST_CENTER_ID: 0,
    //            TaxValue: 0,
    //            Taxable: false
    //        }

    //        if ($scope.typeOfEntry == 1 || $scope.typeOfEntry == 5 || $scope.typeOfEntry == 11) {

    //            var gridTaxAccountDepit = 0;
    //            for (var i = 0; i < $scope.entryDetails.length; i++) {
    //                if ($scope.entryDetails[i].ACC_ID == $scope.TaxAccountId) {
    //                    gridTaxAccountDepit = $scope.entryDetails[i].ENTRY_DEBIT;
    //                }
    //            }


    //            //edit value
    //            if ($scope.entryDetails2[0].ACC_ID != null && $scope.entryDetails2[0].ACC_ID != "" && $scope.entryDetails2[0].ACC_ID != undefined) {

    //                //to calc tax with value to credit
    //                if ($scope.entryMaster.Taxable == true && $scope.IsTaxesIncluded == false) {

    //                    $scope.entryDetails2[0].ENTRY_CREDIT = (parseFloat($scope.entryMaster.EntryVal) + parseFloat(gridTaxAccountDepit)).toFixed(2);
    //                } else {
    //                    $scope.entryDetails2[0].ENTRY_CREDIT = $scope.entryMaster.EntryVal;
    //                }
    //            }
    //            // add row
    //            else {
    //                $scope.entryDetails2.splice(0, 1);

    //                //to calc tax with value to credit
    //                if ($scope.entryMaster.Taxable == true && $scope.IsTaxesIncluded == false) {
    //                    obj.ENTRY_CREDIT = (parseFloat($scope.entryMaster.EntryVal) + parseFloat(gridTaxAccountDepit)).toFixed(2);
    //                } else if ($scope.entryMaster.Taxable == false && $scope.IsTaxesIncluded == false) {
    //                    obj.ENTRY_CREDIT = (parseFloat($scope.entryMaster.EntryVal) + parseFloat(gridTaxAccountDepit)).toFixed(2);
    //                } else {
    //                    obj.ENTRY_CREDIT = $scope.entryMaster.EntryVal;
    //                }

    //                $scope.entryDetails2.splice(0, 1);
    //                $scope.entryDetails2.push(obj);
    //                $scope.entryDetails2.push($scope.emptyObj);
    //            }
    //        } else if ($scope.typeOfEntry == 2 || $scope.typeOfEntry == 4) {


    //            var gridTaxAccountCredit = 0;

    //            for (var i = 0; i < $scope.entryDetails2.length; i++) {
    //                if ($scope.entryDetails2[i].ACC_ID == $scope.TaxAccountId) {
    //                    gridTaxAccountCredit = $scope.entryDetails2[i].ENTRY_CREDIT;
    //                }
    //            }


    //            //edit value
    //            if ($scope.entryDetails[0].ACC_ID != null && $scope.entryDetails[0].ACC_ID != "" && $scope.entryDetails[0].ACC_ID != undefined) {
    //                //to calc tax with value to credit
    //                if ($scope.entryMaster.Taxable == true && $scope.IsTaxesIncluded == false) {
    //                    $scope.entryDetails[0].ENTRY_DEBIT = (parseFloat($scope.entryMaster.EntryVal) + parseFloat(gridTaxAccountCredit)).toFixed(2);;
    //                } else {
    //                    $scope.entryDetails[0].ENTRY_DEBIT = $scope.entryMaster.EntryVal;
    //                }
    //            }
    //            // add row
    //            else {

    //                //to calc tax with value to credit
    //                if ($scope.entryMaster.Taxable == true && $scope.IsTaxesIncluded == false) {
    //                    obj.ENTRY_DEBIT = (parseFloat($scope.entryMaster.EntryVal) + parseFloat(gridTaxAccountCredit)).toFixed(2);
    //                } else if ($scope.entryMaster.Taxable == false && $scope.IsTaxesIncluded == false) {
    //                    obj.ENTRY_DEBIT = (parseFloat($scope.entryMaster.EntryVal) + parseFloat(gridTaxAccountCredit)).toFixed(2);
    //                } else {
    //                    obj.ENTRY_DEBIT = $scope.entryMaster.EntryVal;
    //                }


    //                $scope.entryDetails.splice(0, 1);
    //                // obj.ENTRY_DEBIT = $scope.entryMaster.EntryVal;
    //                $scope.entryDetails.push(obj);
    //                $scope.entryDetails.push($scope.emptyObj);
    //            }
    //        }
    //        $scope.calculateTotals();
    //    }
    //}


    //////////serach for accounts by code and name
    $scope.searchCodeName2 = function (code) {
        if (code != null && code != undefined) {
            $scope.searchBy2 = code;
            $scope.searchForAccountsGrid2();
        }
    }

    //////////////add tax account to grids

    $scope.addTaxAccount = function () {
        ///  1 w 5 hdyf fel credit w 2 w 4 hdyf fel depit 
        if ($scope.EntryModeID == 2) {
            if ($scope.IsTaxesIncluded == false) {
                if ($scope.typeOfEntry == 1 || $scope.typeOfEntry == 5 || $scope.typeOfEntry == 11) {
                    var found = false;
                    $scope.taxAddedAccount = {};
                    var taxValueOfEntry;
                    $scope.emptyObj = {
                        ACC_ID: null,
                        ACC_CODE: "",
                        ACC_AR_NAME: "",
                        ENTRY_CREDIT: 0,
                        ENTRY_GOLD_DEBIT: 0,
                        ENTRY_GOLD_CREDIT: 0,
                        ENTRY_DEBIT: 0,
                        ENTRY_DETAILS_REMARKS: "",
                        COST_CENTER_ID: 0,
                        TaxValue: 0,
                        Taxable: false
                    }
                    
                    if ($scope.entryMaster.Taxable == true) {

                        taxValueOfEntry = (parseFloat($scope.totalDepitWithTax) * (parseFloat($scope.entryTaxRate) / 100)).toFixed(2);
                    } else {
                        taxValueOfEntry = $scope.totalTaxValueDebit;
                    }

                    for (var i = 0; i < $scope.entryDetails.length; i++) {
                        if ($scope.TaxAccountId == $scope.entryDetails[i].ACC_ID) {
                            found = true;
                            $scope.entryDetails[i].ENTRY_DEBIT = taxValueOfEntry;
                            break;
                        }
                    }
                    if (!found) {
                        if ($scope.taxAccount != undefined && $scope.taxAccount != null && $scope.taxAccount != 0) {
                            $scope.taxAddedAccount.ACC_ID = $scope.taxAccount.ACC_ID;
                            $scope.taxAddedAccount.ACC_CODE = $scope.taxAccount.ACC_CODE;
                            $scope.taxAddedAccount.ACC_AR_NAME = $scope.taxAccount.ACC_AR_NAME;
                            $scope.taxAddedAccount.ENTRY_DEBIT = taxValueOfEntry;

                            if ($scope.entryMaster.Taxable) {
                                $scope.entryDetails.splice($scope.entryDetails.length - 1, 1);
                                $scope.entryDetails.push($scope.taxAddedAccount);
                                $scope.entryDetails.push($scope.emptyObj);
                            } else {
                                if ($scope.depitTaxAccountComplix) {
                                    $scope.entryDetails.splice($scope.entryDetails.length - 1, 1);
                                    $scope.entryDetails.push($scope.taxAddedAccount);
                                    $scope.entryDetails.push($scope.emptyObj);
                                }
                            }

                        }
                    }
                    $scope.entryMaster.TotalDepit = parseFloat($scope.entryMaster.TotalDepit) + parseFloat(taxValueOfEntry);
                } else if ($scope.typeOfEntry == 2 || $scope.typeOfEntry == 4 || $scope.typeOfEntry == 10) {
                    
                    var found = false;
                    $scope.taxAddedAccount = {};
                    var taxValueOfEntry
                    $scope.emptyObj = {
                        ACC_ID: null,
                        ACC_CODE: "",
                        ACC_AR_NAME: "",
                        ENTRY_CREDIT: 0,
                        ENTRY_GOLD_DEBIT: 0,
                        ENTRY_GOLD_CREDIT: 0,
                        ENTRY_DEBIT: 0,
                        ENTRY_DETAILS_REMARKS: "",
                        COST_CENTER_ID: 0,
                        TaxValue: 0,
                        Taxable: false
                    }
                    if ($scope.entryMaster.Taxable == true) {

                        taxValueOfEntry = (parseFloat($scope.totalCreditWithTax) * (parseFloat($scope.entryTaxRate) / 100)).toFixed(2);
                    } else {
                        taxValueOfEntry = $scope.totalTaxValueCredit;
                        // taxValueOfEntry = $scope.totalCreditWithTax;
                    }

                    for (var i = 0; i < $scope.entryDetails2.length; i++) {
                        if ($scope.TaxAccountId == $scope.entryDetails2[i].ACC_ID) {
                            found = true;
                            $scope.entryDetails2[i].ENTRY_CREDIT = taxValueOfEntry;
                            break;
                        }
                    }
                    if (!found) {
                        if ($scope.taxAccount != undefined && $scope.taxAccount != null && $scope.taxAccount != 0) {
                            $scope.taxAddedAccount.ACC_ID = $scope.taxAccount.ACC_ID;
                            $scope.taxAddedAccount.ACC_CODE = $scope.taxAccount.ACC_CODE;
                            $scope.taxAddedAccount.ACC_AR_NAME = $scope.taxAccount.ACC_AR_NAME;
                            $scope.taxAddedAccount.ENTRY_CREDIT = taxValueOfEntry;

                            if ($scope.entryMaster.Taxable) {
                                $scope.entryDetails2.splice($scope.entryDetails2.length - 1, 1);
                                $scope.entryDetails2.push($scope.taxAddedAccount);
                                $scope.entryDetails2.push($scope.emptyObj);
                            } else {
                                if ($scope.creditTaxAccountComplix) {
                                    $scope.entryDetails2.splice($scope.entryDetails2.length - 1, 1);
                                    $scope.entryDetails2.push($scope.taxAddedAccount);
                                    $scope.entryDetails2.push($scope.emptyObj);
                                }
                            }

                        }
                    }
                    
                    $scope.entryMaster.TotalCredit = parseFloat($scope.entryMaster.TotalCredit) + parseFloat(taxValueOfEntry);
                }
            }
        }
    }


    $scope.getReport1D = function () {
        $.each($scope.currenciesList, function (index, value) {
            if (value.CURRENCY_ID == $scope.entryMaster.CURRENCY_ID) {
                $scope.ReportData.CURRENCY_Name = value.CURRENCY_AR_NAME;
                $scope.ReportData.CURRENCY_SUB_AR_NAME = value.CURRENCY_SUB_AR_NAME;
                // alert($scope.ReportData.CURRENCY_SUB_AR_NAME);
            }
        })
        $.each($scope.BoxAccounts, function (index, value) {
            if (value.ACC_ID == $scope.entryMaster.ACC_ID) {
                $scope.ReportData.ReportBankName = value.ACC_AR_NAME;
                // alert($scope.ReportData.CURRENCY_SUB_AR_NAME);
            }
        })

        var date = $scope.entryMaster.CHECK_DATE;
        var DateOptions = {
            weekday: 'long',
            year: 'numeric',
            month: 'long',
            day: 'numeric'
        };
        $scope.ReportData.ReportCheckDate = new Date(date).toLocaleString("ar-EG", DateOptions);
    }

    $scope.GetReportReportTotalsTafqeet = function () {


        $scope.ReportData.TotalNoTafqeet = $scope.entryMaster.TotalDepit != 0 && $scope.entryMaster.TotalDepit != undefined &&
            $scope.entryMaster.TotalDepit != "" ?
            (tafqeet((parseFloat($scope.entryMaster.TotalDepit) % 1).toFixed(2))) != 0 ? ("فقط " + tafqeet(parseInt($scope.entryMaster.TotalDepit)) + " " + $scope.ReportData.CURRENCY_Name + " فقط لا غير  ") : ("فقط " + tafqeet(parseInt($scope.entryMaster.TotalDepit)) + " و " + $scope.ReportData.CURRENCY_Name + " فقط لا غير  ") :
            " ";
        // alert($scope.entryMaster.TotalCredit)
        
        if ($scope.ReportData.CURRENCY_Name == undefined) {
            $scope.ReportData.CURRENCY_Name = "ريال سعودي";

        }
        $scope.ReportData.TotalCreditTafqeet = $scope.entryMaster.TotalCredit != 0 && $scope.entryMaster.TotalCredit != undefined &&
            $scope.entryMaster.TotalCredit != "" ?
            (tafqeet((parseFloat($scope.entryMaster.TotalCredit) % 1).toFixed(2))) != 0 ? ("فقط " + tafqeet(parseInt($scope.entryMaster.TotalCredit)) + " " + $scope.ReportData.CURRENCY_Name + " فقط لا غير  ") : ("فقط " + tafqeet(parseInt($scope.entryMaster.TotalCredit)) + " و " + $scope.ReportData.CURRENCY_Name + " فقط لا غير  ") :
            " ";


        $scope.ReportData.TotalDebitCheckTafqeet = $scope.entryMaster.TotalCurrenctDepit != 0 && $scope.entryMaster.TotalCurrenctDepit != undefined &&
            $scope.entryMaster.TotalCurrenctDepit != "" ?
            (tafqeet((parseFloat($scope.entryMaster.TotalCurrenctDepit) % 1).toFixed(2))) != 0 ? ("فقط " + tafqeet(parseInt($scope.entryMaster.TotalCurrenctDepit)) + " " + $scope.ReportData.CURRENCY_Name + " فقط لا غير  ") : ("فقط " + tafqeet(parseInt($scope.entryMaster.TotalCurrenctDepit)) + " و " + $scope.ReportData.CURRENCY_Name + " فقط لا غير  ") :
            "فقط صفر " + $scope.ReportData.CURRENCY_Name + " لا غير";;

        $scope.ReportData.TotalCreditCheckTafqeet = $scope.entryMaster.TotalCurrencyCredit != 0 && $scope.entryMaster.TotalCurrencyCredit != undefined &&
            $scope.entryMaster.TotalCurrencyCredit != "" ?
            (tafqeet((parseFloat($scope.entryMaster.TotalCurrencyCredit) % 1).toFixed(2))) != 0 ? ("فقط " + tafqeet(parseInt($scope.entryMaster.TotalCurrencyCredit)) + " " + $scope.ReportData.CURRENCY_Name + " فقط لا غير  ") : ("فقط " + tafqeet(parseInt($scope.entryMaster.TotalCurrencyCredit)) + " و " + $scope.ReportData.CURRENCY_Name + " فقط لا غير  ") :
            "فقط صفر " + $scope.ReportData.CURRENCY_Name + " لا غير";

        // alert($scope.entryMaster.TotalGoldDepit24);

        $scope.ReportData.Total24Tafqeet = $scope.entryMaster.TotalGoldDepit24 != 0 && $scope.entryMaster.TotalGoldDepit24 != undefined &&
            $scope.entryMaster.TotalGoldDepit24 != "" ?
            ("فقط " + tafqeet(parseInt($scope.entryMaster.TotalGoldDepit24)) + " جرام و" +
                tafqeet(parseFloat($scope.entryMaster.TotalGoldDepit24).toFixed(2).split(".")[1]) + " % من الجرام عيار 24") : "فقط صفر جرام لا غير من عيار 24";

        //alert($scope.ReportData.Total24Tafqeet);

        $scope.ReportData.Total22Tafqeet = $scope.entryMaster.TotalGoldDepit22 != 0 && $scope.entryMaster.TotalGoldDepit22 != undefined &&
            $scope.entryMaster.TotalGoldDepit22 != "" ?
            ("فقط " + tafqeet(parseInt($scope.entryMaster.TotalGoldDepit22)) + " جرام و" +
                tafqeet(parseFloat($scope.entryMaster.TotalGoldDepit22).toFixed(2).split(".")[1]) + " % من الجرام عيار 22") : "فقط صفر جرام لا غير من عيار 22";

        $scope.ReportData.Total21Tafqeet = $scope.entryMaster.TotalGoldDepit21 != 0 && $scope.entryMaster.TotalGoldDepit21 != undefined &&
            $scope.entryMaster.TotalGoldDepit21 != "" ?
            ("فقط " + tafqeet(parseInt($scope.entryMaster.TotalGoldDepit21)) + " جرام و" +
                tafqeet(parseFloat($scope.entryMaster.TotalGoldDepit21).toFixed(2).split(".")[1]) + " % من الجرام عيار 21") : "فقط صفر جرام لا غير من عيار 21";

        $scope.ReportData.Total18Tafqeet = $scope.entryMaster.TotalGoldDepit18 != 0 && $scope.entryMaster.TotalGoldDepit18 != undefined &&
            $scope.entryMaster.TotalGoldDepit18 != "" ?
            ("فقط " + tafqeet(parseInt($scope.entryMaster.TotalGoldDepit18)) + " جرام و" +
                tafqeet(parseFloat($scope.entryMaster.TotalGoldDepit18).toFixed(2).split(".")[1]) + " % من الجرام عيار 18") : "فقط صفر جرام لا غير من عيار 18";

        // credit

        $scope.ReportData.Total24TafqeetCredit = $scope.entryMaster.TotalGoldCredit24 != 0 && $scope.entryMaster.TotalGoldCredit24 != undefined &&
            $scope.entryMaster.TotalGoldCredit24 != "" ?
            ("فقط " + tafqeet(parseInt($scope.entryMaster.TotalGoldCredit24)) + " جرام و" +
                tafqeet(parseFloat($scope.entryMaster.TotalGoldCredit24).toFixed(2).split(".")[1]) + " % من الجرام عيار 24") : "فقط صفر جرام لا غير من عيار 24";

        $scope.ReportData.Total22TafqeetCredit = $scope.entryMaster.TotalGoldCredit22 != 0 && $scope.entryMaster.TotalGoldCredit22 != undefined &&
            $scope.entryMaster.TotalGoldCredit22 != "" ?
            ("فقط " + tafqeet(parseInt($scope.entryMaster.TotalGoldCredit22)) + " جرام و" +
                tafqeet(parseFloat($scope.entryMaster.TotalGoldCredit22).toFixed(2).split(".")[1]) + " % من الجرام عيار 22") : "فقط صفر جرام لا غير من عيار 22";

        $scope.ReportData.Total21TafqeetCredit = $scope.entryMaster.TotalGoldCredit21 != 0 && $scope.entryMaster.TotalGoldCredit21 != undefined &&
            $scope.entryMaster.TotalGoldCredit21 != "" ?
            ("فقط " + tafqeet(parseInt($scope.entryMaster.TotalGoldCredit21)) + " جرام و" +
                tafqeet(parseFloat($scope.entryMaster.TotalGoldCredit21).toFixed(2).split(".")[1]) + " % من الجرام عيار 21") : "فقط صفر جرام لا غير من عيار 21";

        $scope.ReportData.Total18TafqeetCredit = $scope.entryMaster.TotalGoldCredit18 != 0 && $scope.entryMaster.TotalGoldCredit18 != undefined &&
            $scope.entryMaster.TotalGoldCredit18 != "" ?
            ("فقط " + tafqeet(parseInt($scope.entryMaster.TotalGoldCredit18)) + " جرام و" +
                tafqeet(parseFloat($scope.entryMaster.TotalGoldCredit18).toFixed(2).split(".")[1]) + " % من الجرام عيار 18") : "فقط صفر جرام لا غير من عيار 18";
    }

    $scope.GetReportData = function () {

        $scope.getReport1D();

        //************** Start Report Shared Data ****************//

        //Report date
        var DateOptions = {
            weekday: 'long',
            year: 'numeric',
            month: 'long',
            day: 'numeric'
        };
        $scope.ReportData.ReportEnDate = new Date().toLocaleString("en-us", DateOptions);
        $scope.ReportData.ReportArDate = new Date().toLocaleString("ar-EG", DateOptions);
        $scope.ReportData.ReportHigriDate = new Date().toLocaleString("ar-eg-u-ca-islamic", DateOptions);

        // Get customer data
        var customerAccount = jQuery.grep($scope.complexEntryCustomersAccounts, function (n, i) {
            return (n.ACC_ID === $scope.entryMaster.CustAccID);
        });
        if (customerAccount.length > 0 && customerAccount[0] != undefined) {
            // alert();
            $scope.ReportData.custAccountName = customerAccount[0].ACC_AR_NAME;
            $scope.ReportData.custAccountNumber = customerAccount[0].ACC_CODE;
        }

        // get customer address
        customersService.getById($scope.entryMaster.CustAccID).then(function (Result) {
            if (Result.data != undefined && Result.data != null && Result.data != "") {
                $scope.ReportData.customerAddressRemark = Result.data.CUST_ADDR_REMARKS;
                $scope.ReportData.customerTaxNumber = Result.data.TaxNumber != null ? Result.data.TaxNumber : '';
            }
        })

        // Get Branch data
        if (localStorageService.get('tempBrName') != null && localStorageService.get('tempBrName') != undefined) {
            $scope.ReportData.ReportBranchName = localStorageService.get('tempBrName').tempBrName;
        } else {
            $scope.ReportData.ReportBranchName = authService.GetUserName();
        }

        //Get company data
        MenuService.GetCompany().then(function (response) {
                if (response.data != undefined && response.data != null && response.data != "") {
                    $scope.ReportData.CompanyARName = response.data.COMPANY_AR_NAME;
                    $scope.ReportData.CompanyENName = response.data.COMPANY_EN_NAME;
                    $scope.ReportData.CompanyARAddress = response.data.COMPANY_AR_ADRESS;
                    $scope.ReportData.CompanyENAddress = response.data.COMPANY_EN_ADRESS;
                    $scope.ReportData.COMPANY_TELEPHONE = response.data.COMPANY_TELEPHONE;
                    $scope.ReportData.CommercialRegister = response.data.CommercialRegister;
                    $scope.ReportData.CompanyTaxNumber = response.data.TaxNumber;
                }
                //$scope.getUserType();
            },
            function (err) {
                $scope.message = err.error_description;
                console.log(err.message);
            });
        //************** End Report Shared Data ****************//
    }
    //table footer

    $scope.printEntryData = function () {
        var Content = document.getElementById('PopupEntriesPrintDiv').innerHTML;
        var frame1 = document.createElement('iframe');
        frame1.name = "frame1";
        frame1.style.position = "absolute";
        frame1.style.top = "-1000000px";
        document.body.appendChild(frame1);
        var frameDoc = frame1.contentWindow ? frame1.contentWindow : frame1.contentDocument.document ? frame1.contentDocument.document : frame1.contentDocument;
        frameDoc.document.open();
        frameDoc.document.write('<html id="printTable" dir="rtl"><head>');
        frameDoc.document.write('<style>*{border: none} #fatoraFooter * , #fatoraHeader *{border: none !important;text-align: center} table{margin:10px 0; width: 100%} th{height:50px}table {border-collapse: collapse;}th, td {border: 1px solid black;padding: 5px 10px}</style>');
        frameDoc.document.write('</head><body>');
        frameDoc.document.write(Content);
        frameDoc.document.write('</body></html>');
        frameDoc.document.close();
        setTimeout(function () {
            window.frames["frame1"].focus();
            window.frames["frame1"].print();
            document.body.removeChild(frame1);
        }, 500);
        return false;
    }








    ////////////////////////////////
    //$scope.getGoldObjectsByItems = function () {
    //    $scope.getGoldAccountOfDetails();

    //    for (var i = 0; i < $scope.goldAccountsList.length; i++) {
    //        $scope.itemObject = {};
    //        if ($scope.goldAccountsList[i].AccId != undefined && $scope.goldAccountsList[i].AccId != null) {
    //            $scope.itemObject.accountID = $scope.goldAccountsList[i].AccId;


    //            $scope.checkAccountTypes($scope.bill_type_Id, 'X')
    //            $scope.itemObject.accountType = $scope.typeName;

    //            if ($scope.bill_type_Id == 2 || $scope.bill_type_Id == 23 || $scope.bill_type_Id == 24) {
    //                $scope.checkAccountTypes($scope.bill_type_Id, 'X')
    //                $scope.itemObject.accountType = $scope.typeName;

    //                $scope.itemObject.moneyValue = 0;
    //                $scope.itemObject.gold24Value = $scope.goldAccountsList[i].gold24;
    //                $scope.itemObject.gold22Value = $scope.goldAccountsList[i].gold22;
    //                $scope.itemObject.gold21Value = $scope.goldAccountsList[i].gold21;
    //                $scope.itemObject.gold18Value = $scope.goldAccountsList[i].gold18;
    //            }

    //            else if ($scope.bill_type_Id == 1) {

    //                $scope.checkAccountTypes($scope.bill_type_Id, 'X')
    //                $scope.itemObject.accountType = $scope.typeName;
    //                $scope.itemObject.moneyValue = 0;
    //                $scope.itemObject.gold24Value = $scope.goldAccountsList[i].gold24;
    //                $scope.itemObject.gold22Value = $scope.goldAccountsList[i].gold22;
    //                $scope.itemObject.gold21Value = $scope.goldAccountsList[i].gold21;
    //                $scope.itemObject.gold18Value = $scope.goldAccountsList[i].gold18;
    //            }

    //            else if ($scope.bill_type_Id == 19 || $scope.bill_type_Id == 20) {
    //                $scope.itemObject.moneyValue = 0;
    //                $scope.itemObject.gold24Value = $scope.goldAccountsList[i].gold24;
    //                $scope.itemObject.gold22Value = $scope.goldAccountsList[i].gold22;
    //                $scope.itemObject.gold21Value = $scope.goldAccountsList[i].gold21;
    //                $scope.itemObject.gold18Value = $scope.goldAccountsList[i].gold18;
    //            }
    //            else {
    //                $scope.itemObject.moneyValue = 0;
    //                $scope.itemObject.gold24Value = $scope.goldAccountsList[i].gold24;
    //                $scope.itemObject.gold22Value = $scope.goldAccountsList[i].gold22;
    //                $scope.itemObject.gold21Value = $scope.goldAccountsList[i].gold21;
    //                $scope.itemObject.gold18Value = $scope.goldAccountsList[i].gold18;

    //            }
    //            $scope.billMaster.accounts.push($scope.itemObject);

    //            $scope.convertGold.gold24Value = $scope.matrixList[1].TransQuant;
    //            $scope.convertGold.gold22Value = $scope.matrixList[2].TransQuant;
    //            $scope.convertGold.gold21Value = $scope.matrixList[3].TransQuant;
    //            $scope.convertGold.gold18Value = $scope.matrixList[4].TransQuant;

    //        }
    //    }

    //}



    // قيود 
    $scope.getTaxAccountsOfDetailsForCredit = function () {
         
        $scope.taxAccountsList = [];
        //var goldAcoountObject = { AccId: 0, gold24: 0, gold22: 0, gold21: 0, gold18: 0 };
        for (var i = 0; i < $scope.entryDetails.length; i++) {
            if ($scope.entryDetails[i].ACC_CODE != null && $scope.entryDetails[i].ACC_CODE != "" && $scope.entryDetails[i].ACC_CODE != undefined && $scope.entryDetails[i].ENTRY_CREDIT != null &&
                $scope.entryDetails[i].ENTRY_CREDIT != undefined && $scope.entryDetails[i].ENTRY_CREDIT != 0) {

                if ($scope.entryDetails[i].TaxAccountID != null && $scope.entryDetails[i].TaxAccountID != undefined && $scope.entryDetails[i].TaxAccountID != 0) {

                    var exist = $filter("filter")($scope.taxAccountsList, {
                        'AccId': $scope.entryDetails[i].TaxAccountID
                    });

                    //////// if account exist add to its value
                    if (exist.length > 0) {
                        var index = $scope.taxAccountsList.findIndex(x => x.AccId === $scope.entryDetails[i].TaxAccountID);
                        $scope.taxAccountsList[index].tax = parseFloat($scope.entryDetails[i].TaxValue) + parseFloat($scope.taxAccountsList[index].tax);
                    }

                    /////////tax account not exist so push it
                    else {
                        var taxAcoountObject = {
                            AccId: 0,
                            tax: 0
                        };
                        taxAcoountObject.AccId = $scope.entryDetails[i].TaxAccountID;
                        taxAcoountObject.tax = parseFloat($scope.entryDetails[i].TaxValue);
                        $scope.taxAccountsList.push(taxAcoountObject);
                    }
                }

                //tax account is empty and get master tax account from setting
                else {
                    var exist = $filter("filter")($scope.taxAccountsList, {
                        'AccId': $scope.TaxAccountId
                    });
                    ///// if exist add tax to its value
                    if (exist.length > 0) {
                        var index = $scope.taxAccountsList.findIndex(x => x.AccId === $scope.TaxAccountId);
                        $scope.taxAccountsList[index].tax = parseFloat($scope.entryDetails[i].TaxValue) + parseFloat($scope.taxAccountsList[index].tax);
                    }

                    //////if not exist add tax account 
                    else {
                        var taxAcoountObject = {
                            AccId: 0,
                            tax: 0
                        };
                        taxAcoountObject.AccId = $scope.TaxAccountId;
                        taxAcoountObject.tax = parseFloat($scope.entryDetails[i].TaxValue);
                        $scope.taxAccountsList.push(taxAcoountObject);
                    }
                }
            }
        }
    }

    $scope.getTaxAccountsOfDetailsForDepit = function () {
        
        $scope.taxAccountsList = [];
        //var goldAcoountObject = { AccId: 0, gold24: 0, gold22: 0, gold21: 0, gold18: 0 };
        for (var i = 0; i < $scope.entryDetails.length; i++) {
            if ($scope.entryDetails[i].ACC_CODE != null && $scope.entryDetails[i].ACC_CODE != "" && $scope.entryDetails[i].ACC_CODE != undefined && $scope.entryDetails[i].ENTRY_DEBIT != null &&
                $scope.entryDetails[i].ENTRY_DEBIT != undefined && $scope.entryDetails[i].ENTRY_DEBIT != 0) {

                if ($scope.entryDetails[i].TaxAccountID != null && $scope.entryDetails[i].TaxAccountID != undefined && $scope.entryDetails[i].TaxAccountID != 0) {

                    var exist = $filter("filter")($scope.taxAccountsList, {
                        'AccId': $scope.entryDetails[i].TaxAccountID
                    });

                    //////// if account exist add to its value
                    if (exist.length > 0) {
                        var index = $scope.taxAccountsList.findIndex(x => x.AccId === $scope.entryDetails[i].TaxAccountID);
                        $scope.taxAccountsList[index].tax = parseFloat($scope.entryDetails[i].TaxValue) + parseFloat($scope.taxAccountsList[index].tax);
                    }

                    /////////tax account not exist so push it
                    else {
                        var taxAcoountObject = {
                            AccId: 0,
                            tax: 0
                        };
                        taxAcoountObject.AccId = $scope.entryDetails[i].TaxAccountID;
                        taxAcoountObject.tax = parseFloat($scope.entryDetails[i].TaxValue);
                        $scope.taxAccountsList.push(taxAcoountObject);
                    }
                }

                //tax account is empty and get master tax account from setting
                else {
                    var exist = $filter("filter")($scope.taxAccountsList, {
                        'AccId': $scope.TaxAccountId
                    });
                    ///// if exist add tax to its value
                    if (exist.length > 0) {
                        var index = $scope.taxAccountsList.findIndex(x => x.AccId === $scope.TaxAccountId);
                        $scope.taxAccountsList[index].tax = parseFloat($scope.entryDetails[i].TaxValue) + parseFloat($scope.taxAccountsList[index].tax);
                    }

                    //////if not exist add tax account 
                    else {
                        var taxAcoountObject = {
                            AccId: 0,
                            tax: 0
                        };
                        taxAcoountObject.AccId = $scope.TaxAccountId;
                        taxAcoountObject.tax = parseFloat($scope.entryDetails[i].TaxValue);
                        $scope.taxAccountsList.push(taxAcoountObject);
                    }
                }
            }
        }
    }
    //




    ////////////////// add tax account of account if taxable
    $scope.addTaxAccountForEachAccount = function (index) {
        ///  1 w 5 hdyf fel credit w 2 w 4 hdyf fel depit 
        if ($scope.EntryModeID == 2) {
            if ($scope.IsTaxesIncluded == false) {
                if ($scope.typeOfEntry == 1 || $scope.typeOfEntry == 5 || $scope.typeOfEntry == 11) {
                    $scope.taxAddedAccount = {};
                    //  var taxValueOfEntry;
                    $scope.emptyObj = {
                        ACC_ID: null,
                        ACC_CODE: "",
                        ACC_AR_NAME: "",
                        ENTRY_CREDIT: 0,
                        ENTRY_GOLD_DEBIT: 0,
                        ENTRY_GOLD_CREDIT: 0,
                        ENTRY_DEBIT: 0,
                        ENTRY_DETAILS_REMARKS: "",
                        COST_CENTER_ID: 0,
                        TaxValue: 0,
                        Taxable: false
                    }

                    if ($scope.entryDetails[index].TaxAccountID != null && $scope.entryDetails[index].TaxAccountID != undefined && $scope.entryDetails[index].TaxAccountID != 0) {

                        $scope.taxAddedAccount.ACC_ID = $scope.entryDetails[index].TaxAccountID;
                        var taxAccountData = $filter("filter")($scope.BoxAccounts, {
                            'ACC_ID': $scope.entryDetails[index].TaxAccountID
                        }, true);

                        $scope.taxAddedAccount.ACC_CODE = taxAccountData[0].ACC_CODE;
                        $scope.taxAddedAccount.ACC_AR_NAME = taxAccountData[0].ACC_AR_NAME;

                        //////check if account id exist

                        var existAccount = $filter("filter")($scope.entryDetails, {
                            'ACC_ID': $scope.entryDetails[index].TaxAccountID
                        }, true);
                        if (existAccount.length > 0) {
                            var taxIndex = $scope.entryDetails.findIndex(x => x.ACC_ID === $scope.entryDetails[index].TaxAccountID);
                            $scope.taxAddedAccount.ENTRY_DEBIT = parseFloat($scope.entryDetails[index].TaxValue) + parseFloat($scope.entryDetails[taxIndex].ENTRY_DEBIT);
                        } else {
                            $scope.taxAddedAccount.ENTRY_DEBIT = $scope.entryDetails[index].TaxValue;
                            if ($scope.depitTaxAccountComplix) {
                                $scope.entryDetails.splice($scope.entryDetails.length - 1, 1);
                                $scope.entryDetails.push($scope.taxAddedAccount);
                                $scope.entryDetails.push($scope.emptyObj);
                            }
                        }

                    } else {
                        if ($scope.taxAccount != undefined && $scope.taxAccount != null && $scope.taxAccount != 0) {
                            $scope.taxAddedAccount.ACC_ID = $scope.taxAccount.ACC_ID;
                            $scope.taxAddedAccount.ACC_CODE = $scope.taxAccount.ACC_CODE;
                            $scope.taxAddedAccount.ACC_AR_NAME = $scope.taxAccount.ACC_AR_NAME;


                            //////check if account id exist

                            var existAccount = $filter("filter")($scope.entryDetails, {
                                'ACC_ID': $scope.taxAccount.ACC_ID
                            }, true);
                            if (existAccount.length > 0) {
                                var taxIndex = $scope.entryDetails.findIndex(x => x.ACC_ID === $scope.taxAccount);
                                $scope.taxAddedAccount.ENTRY_DEBIT = parseFloat($scope.entryDetails[index].TaxValue) + parseFloat($scope.entryDetails[taxIndex].ENTRY_DEBIT);
                            } else {

                                $scope.taxAddedAccount.ENTRY_DEBIT = $scope.entryDetails[index].TaxValue;

                                if ($scope.depitTaxAccountComplix) {
                                    $scope.entryDetails.splice($scope.entryDetails.length - 1, 1);
                                    $scope.entryDetails.push($scope.taxAddedAccount);
                                    $scope.entryDetails.push($scope.emptyObj);
                                }
                            }

                        }
                    }
                    $scope.entryMaster.TotalDepit = parseFloat($scope.entryMaster.TotalDepit) + parseFloat(($scope.entryDetails[index].TaxValue != null && $scope.entryDetails[index].TaxValue != undefined) ? $scope.entryDetails[index].TaxValue : 0);
                } else if ($scope.typeOfEntry == 2 || $scope.typeOfEntry == 4 || $scope.typeOfEntry == 10) {
                    

                    $scope.taxAddedAccount = {};
                    //  var taxValueOfEntry;
                    $scope.emptyObj = {
                        ACC_ID: null,
                        ACC_CODE: "",
                        ACC_AR_NAME: "",
                        ENTRY_CREDIT: 0,
                        ENTRY_GOLD_DEBIT: 0,
                        ENTRY_GOLD_CREDIT: 0,
                        ENTRY_DEBIT: 0,
                        ENTRY_DETAILS_REMARKS: "",
                        COST_CENTER_ID: 0,
                        TaxValue: 0,
                        Taxable: false
                    }

                    if ($scope.entryDetails2[index].TaxAccountID != null && $scope.entryDetails2[index].TaxAccountID != undefined && $scope.entryDetails2[index].TaxAccountID != 0) {

                        $scope.taxAddedAccount.ACC_ID = $scope.entryDetails2[index].TaxAccountID;
                        var taxAccountData = $filter("filter")($scope.BoxAccounts, {
                            'ACC_ID': $scope.entryDetails2[index].TaxAccountID
                        }, true);

                        $scope.taxAddedAccount.ACC_CODE = taxAccountData[0].ACC_CODE;
                        $scope.taxAddedAccount.ACC_AR_NAME = taxAccountData[0].ACC_AR_NAME;

                        //////check if account id exist

                        var existAccount = $filter("filter")($scope.entryDetails2, {
                            'ACC_ID': $scope.entryDetails2[index].TaxAccountID
                        }, true);
                        if (existAccount.length > 0) {
                            var taxIndex = $scope.entryDetails2.findIndex(x => x.ACC_ID === $scope.entryDetails2[index].TaxAccountID);
                            $scope.taxAddedAccount.ENTRY_CREDIT = parseFloat($scope.entryDetails2[index].TaxValue) + parseFloat($scope.entryDetails2[taxIndex].ENTRY_CREDIT);
                        } else {
                            $scope.taxAddedAccount.ENTRY_CREDIT = $scope.entryDetails2[index].TaxValue;
                            if ($scope.creditTaxAccountComplix) {
                                $scope.entryDetails2.splice($scope.entryDetails2.length - 1, 1);
                                $scope.entryDetails2.push($scope.taxAddedAccount);
                                $scope.entryDetails2.push($scope.emptyObj);
                            }
                        }

                    } else {
                        if ($scope.taxAccount != undefined && $scope.taxAccount != null && $scope.taxAccount != 0) {
                            $scope.taxAddedAccount.ACC_ID = $scope.taxAccount.ACC_ID;
                            $scope.taxAddedAccount.ACC_CODE = $scope.taxAccount.ACC_CODE;
                            $scope.taxAddedAccount.ACC_AR_NAME = $scope.taxAccount.ACC_AR_NAME;


                            //////check if account id exist

                            var existAccount = $filter("filter")($scope.entryDetails2, {
                                'ACC_ID': $scope.taxAccount.ACC_ID
                            }, true);
                            if (existAccount.length > 0) {
                                var taxIndex = $scope.entryDetails2.findIndex(x => x.ACC_ID === $scope.taxAccount);
                                $scope.taxAddedAccount.ENTRY_CREDIT = parseFloat($scope.entryDetails2[index].TaxValue) + parseFloat($scope.entryDetails2[taxIndex].ENTRY_CREDIT);
                            } else {

                                $scope.taxAddedAccount.ENTRY_CREDIT = $scope.entryDetails2[index].TaxValue;

                                if ($scope.creditTaxAccountComplix) {
                                    $scope.entryDetails2.splice($scope.entryDetails2.length - 1, 1);
                                    $scope.entryDetails2.push($scope.taxAddedAccount);
                                    $scope.entryDetails2.push($scope.emptyObj);
                                }
                            }

                        }
                    }
                    $scope.entryMaster.TotalCredit = parseFloat($scope.entryMaster.TotalCredit) + parseFloat(($scope.entryDetails2[index].TaxValue != null && $scope.entryDetails2[index].TaxValue != undefined) ? $scope.entryDetails2[index].TaxValue : 0);

                }
            }
        }
    }


    /////
    $scope.addTaxAfterCheckTaxable = function (index) {

        if ($scope.EntryModeID == 2) {
            if ($scope.IsTaxesIncluded == false) {

                if ($scope.entryDetails[index].TaxAccountID != null && $scope.entryDetails[index].TaxAccountID != undefined) {

                    var exist = $filter("filter")($scope.entryDetails, {
                        'ACC_ID': $scope.entryDetails[index].TaxAccountID
                    }, true);
                    //////// if account exist add to its value
                    if (exist.length > 0) {
                        var taxIndex = $scope.entryDetails.findIndex(x => x.ACC_ID === $scope.entryDetails[index].TaxAccountID);
                        if ($scope.typeOfEntry == 1 || $scope.typeOfEntry == 5 || $scope.typeOfEntry == 11) {
                            $scope.entryDetails[taxIndex].ENTRY_DEBIT = 0;
                            for (var i = 0; i < $scope.entryDetails.length; i++) {
                                if ($scope.entryDetails[i].TaxAccountID == $scope.entryDetails[taxIndex].ACC_ID) {
                                    $scope.entryDetails[taxIndex].ENTRY_DEBIT = (parseFloat($scope.entryDetails[i].TaxValue) +
                                        parseFloat(($scope.entryDetails[taxIndex].ENTRY_DEBIT != null && $scope.entryDetails[taxIndex].ENTRY_DEBIT != undefined) ? $scope.entryDetails[taxIndex].ENTRY_DEBIT : 0)).toFixed(2);
                                }
                            }
                        }
                    }
                } else {
                    var taxIndex = $scope.entryDetails.findIndex(x => x.ACC_ID === $scope.taxAccount.ACC_ID);
                    if (taxIndex != null && taxIndex >= 0) {
                        if ($scope.typeOfEntry == 1 || $scope.typeOfEntry == 5 || $scope.typeOfEntry == 11) {

                            $scope.entryDetails[taxIndex].ENTRY_DEBIT = 0;
                            for (var i = 0; i < $scope.entryDetails.length; i++) {

                                if (($scope.entryDetails[i].TaxAccountID == undefined || $scope.entryDetails[i].TaxAccountID == null || $scope.entryDetails[i].TaxAccountID == $scope.taxAccount.ACC_ID) && $scope.entryDetails[i].TaxValue != undefined) {

                                    $scope.entryDetails[taxIndex].ENTRY_DEBIT = (parseFloat($scope.entryDetails[i].TaxValue) +
                                        parseFloat(($scope.entryDetails[taxIndex].ENTRY_DEBIT != null && $scope.entryDetails[taxIndex].ENTRY_DEBIT != undefined) ? $scope.entryDetails[taxIndex].ENTRY_DEBIT : 0)).toFixed(2);
                                }
                            }
                        }
                    }
                }
            }
        }
    }


    $scope.addTaxAfterCheckTaxableCredit = function (index) {

        if ($scope.EntryModeID == 2) {
            if ($scope.IsTaxesIncluded == false) {

                if ($scope.entryDetails2[index].TaxAccountID != null && $scope.entryDetails2[index].TaxAccountID != undefined) {

                    var exist = $filter("filter")($scope.entryDetails2, {
                        'ACC_ID': $scope.entryDetails2[index].TaxAccountID
                    }, true);
                    //////// if account exist add to its value
                    if (exist.length > 0) {
                        var taxIndex = $scope.entryDetails2.findIndex(x => x.ACC_ID === $scope.entryDetails2[index].TaxAccountID);


                        if ($scope.typeOfEntry == 2 || $scope.typeOfEntry == 4 || $scope.typeOfEntry == 10) {


                            $scope.entryDetails2[taxIndex].ENTRY_CREDIT = 0;

                            for (var i = 0; i < $scope.entryDetails2.length; i++) {
                                if ($scope.entryDetails2[i].TaxAccountID == $scope.entryDetails2[taxIndex].ACC_ID) {
                                    $scope.entryDetails2[taxIndex].ENTRY_CREDIT = (parseFloat($scope.entryDetails2[i].TaxValue) +
                                        parseFloat(($scope.entryDetails2[taxIndex].ENTRY_CREDIT != null && $scope.entryDetails2[taxIndex].ENTRY_CREDIT != undefined) ? $scope.entryDetails2[taxIndex].ENTRY_CREDIT : 0)).toFixed(2);
                                }
                            }

                            //$scope.entryDetails2[taxIndex].ENTRY_CREDIT = parseFloat($scope.entryDetails2[index].TaxValue)
                            //    + parseFloat(($scope.entryDetails2[taxIndex].ENTRY_CREDIT != null && $scope.entryDetails2[taxIndex].ENTRY_CREDIT != undefined) ? $scope.entryDetails2[taxIndex].ENTRY_CREDIT : 0);
                        }

                    }


                } else {
                    var taxIndex = $scope.entryDetails2.findIndex(x => x.ACC_ID === $scope.taxAccount.ACC_ID);
                    if (taxIndex != null && taxIndex >= 0) {

                        if ($scope.typeOfEntry == 2 || $scope.typeOfEntry == 4 || $scope.typeOfEntry == 10) {


                            $scope.entryDetails2[taxIndex].ENTRY_CREDIT = 0;
                            for (var i = 0; i < $scope.entryDetails2.length; i++) {

                                if (($scope.entryDetails2[i].TaxAccountID == undefined || $scope.entryDetails2[i].TaxAccountID == null || $scope.entryDetails2[i].TaxAccountID == $scope.taxAccount.ACC_ID) && $scope.entryDetails2[i].TaxValue != undefined) {

                                    $scope.entryDetails2[taxIndex].ENTRY_CREDIT = parseFloat($scope.entryDetails2[i].TaxValue) +
                                        parseFloat(($scope.entryDetails2[taxIndex].ENTRY_CREDIT != null && $scope.entryDetails2[taxIndex].ENTRY_CREDIT != undefined) ? $scope.entryDetails2[taxIndex].ENTRY_CREDIT : 0);
                                }
                            }



                            //$scope.entryDetails2[taxIndex].ENTRY_CREDIT = parseFloat($scope.entryDetails2[index].TaxValue)
                            //    + parseFloat(($scope.entryDetails2[taxIndex].ENTRY_CREDIT != null && $scope.entryDetails2[taxIndex].ENTRY_CREDIT != undefined) ? $scope.entryDetails2[taxIndex].ENTRY_CREDIT : 0);
                        }

                    }
                }
            }
        }
    }




    $scope.RemoveTaxAfterUnCheckTaxable = function (index, oldTax) {

        if ($scope.EntryModeID == 2) {
            if ($scope.IsTaxesIncluded == false) {

                if ($scope.entryDetails[index].TaxAccountID != null && $scope.entryDetails[index].TaxAccountID != undefined) {

                    var exist = $filter("filter")($scope.entryDetails, {
                        'ACC_ID': $scope.entryDetails[index].TaxAccountID
                    }, true);
                    //////// if account exist add to its value
                    if (exist.length > 0) {
                        var taxIndex = $scope.entryDetails.findIndex(x => x.ACC_ID === $scope.entryDetails[index].TaxAccountID);
                        if ($scope.typeOfEntry == 1 || $scope.typeOfEntry == 5 || $scope.typeOfEntry == 11) {
                            $scope.entryDetails[taxIndex].ENTRY_DEBIT = parseFloat(($scope.entryDetails[taxIndex].ENTRY_DEBIT != null && $scope.entryDetails[taxIndex].ENTRY_DEBIT != undefined) ? $scope.entryDetails[taxIndex].ENTRY_DEBIT : 0) -
                                parseFloat(oldTax);
                        }

                    }


                } else {
                    var taxIndex = $scope.entryDetails.findIndex(x => x.ACC_ID === $scope.taxAccount.ACC_ID);
                    if (taxIndex != null) {
                        if ($scope.typeOfEntry == 1 || $scope.typeOfEntry == 5 || $scope.typeOfEntry == 11) {
                            $scope.entryDetails[taxIndex].ENTRY_DEBIT = parseFloat(($scope.entryDetails[taxIndex].ENTRY_DEBIT != null && $scope.entryDetails[taxIndex].ENTRY_DEBIT != undefined) ? $scope.entryDetails[taxIndex].ENTRY_DEBIT : 0) -
                                parseFloat(oldTax);
                        }

                    }
                }
            }
        }
    }



    $scope.RemoveTaxAfterUnCheckTaxableCredit = function (index, oldTax) {

        if ($scope.EntryModeID == 2) {
            if ($scope.IsTaxesIncluded == false) {

                if ($scope.entryDetails2[index].TaxAccountID != null && $scope.entryDetails2[index].TaxAccountID != undefined) {

                    var exist = $filter("filter")($scope.entryDetails2, {
                        'ACC_ID': $scope.entryDetails2[index].TaxAccountID
                    }, true);
                    //////// if account exist add to its value
                    if (exist.length > 0) {
                        var taxIndex = $scope.entryDetails2.findIndex(x => x.ACC_ID === $scope.entryDetails2[index].TaxAccountID);

                        if ($scope.typeOfEntry == 2 || $scope.typeOfEntry == 4 || $scope.typeOfEntry == 10) {
                            $scope.entryDetails2[taxIndex].ENTRY_CREDIT = parseFloat(($scope.entryDetails2[taxIndex].ENTRY_CREDIT != null && $scope.entryDetails2[taxIndex].ENTRY_CREDIT != undefined) ? $scope.entryDetails2[taxIndex].ENTRY_CREDIT : 0) -
                                parseFloat(oldTax);
                        }
                    }
                } else {
                    var taxIndex = $scope.entryDetails2.findIndex(x => x.ACC_ID === $scope.taxAccount.ACC_ID);
                    if (taxIndex != null && taxIndex > 0) {
                        if ($scope.typeOfEntry == 2 || $scope.typeOfEntry == 4 || $scope.typeOfEntry == 10) {
                            $scope.entryDetails2[taxIndex].ENTRY_CREDIT = parseFloat(($scope.entryDetails2[taxIndex].ENTRY_CREDIT != null && $scope.entryDetails2[taxIndex].ENTRY_CREDIT != undefined) ? $scope.entryDetails2[taxIndex].ENTRY_CREDIT : 0) -
                                parseFloat(oldTax);
                        }
                    }
                }
            }
        }
    }



}]);