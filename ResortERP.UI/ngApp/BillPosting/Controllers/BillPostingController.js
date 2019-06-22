'use strict';
app.controller('BillPostingController', ['$scope', '$rootScope', '$location', '$log', '$q', 'accountsService', 'dateFilter', '$filter', 'commonService', 'PointOfSaleService', '$base64', 'entryService', 'billSettingService', 'costCentersService', 'ReportsService', 'compBranchesService', 'entrySettingService', 'authService', 'payTypesService', 'billGridColumn', 'localStorageService', function ($scope, $rootScope, $location, $log, $q, accountsService, dateFilter, $filter, commonService, PointOfSaleService, $base64, entryService, billSettingService, costCentersService, ReportsService, compBranchesService, entrySettingService, authService, payTypesService, billGridColumn, localStorageService) {

    $scope.accountList = [];   

    $scope.BillPostingPageLoad = function () {
        $scope.GetAllEmployeesList();
        $scope.getCostCentersList();
        $scope.getAllStoresList();
        $scope.getPayTypeList();
        $scope.getBillTypes();
        $scope.getBillPayTypesList();
        $scope.BillPosting = {};
        $scope.startDate = new Date();
        $scope.endDate = new Date();
    }

    $scope.getAllStoresList = function () {
        $scope.storesList = [];
        PointOfSaleService.getAllCompanyStores().then(function (response) {

            $scope.storesList = response.data;
        })
    }

    $scope.GetAllEmployeesList = function () {


        $scope.employeeList = [];
        PointOfSaleService.getAllEmployeeOfTypeSales().then(function (response) {

            $scope.employeeList = response.data;
        })
    }

    $scope.getCostCentersList = function () {
        costCentersService.getMainCostCenters().then(function (results) {
            $scope.mainCostCenterList = results.data;
            //console.log(results.data);
        }, function (error) {
            console.log(error.data.message);
        });
    }

    $scope.getPayTypeList=function() {
        PointOfSaleService.getAllTPayTypes().then(function (results) {
            $scope.payTypeList = results.data;
        }, function (error) {
            console.log(error.data.message);
        });
    }

    $scope.getBillTypes=function() {
        billSettingService.getAll().then(function(results) {
            $scope.billTypesList = results.data;
        },
        function(error) {
            console.log(error.data.message);
        });
    }

    $scope.getBillPayTypesList = function () {
        payTypesService.getAllActive().then(function (result) {
            var returnPayTypes = result.data;
            $scope.payTypesList = [];

            for (var i = 0; i < returnPayTypes.length; i++) {
                $scope.payTypeEntity = {};
                $scope.payTypeEntity.PayTypeID = returnPayTypes[i].PAY_TYPE_ID;
                $scope.payTypeEntity.PAY_TYPE_AR_NAME = returnPayTypes[i].PAY_TYPE_AR_NAME;
                $scope.payTypeEntity.BankCommissionRate = returnPayTypes[i].BankCommissionRate;
                $scope.payTypeEntity.MaxCommission = returnPayTypes[i].MaxCommission;
                $scope.payTypeEntity.CommissionTaxRate = returnPayTypes[i].CommissionTaxRate;
                $scope.payTypeEntity.AccountID = returnPayTypes[i].AccountID;
                $scope.payTypesList.push($scope.payTypeEntity);
            }

        });
    }

    $scope.GetSearchResult = function () {        
        if ($scope.startDate && $scope.endDate) {
            $scope.BillPosting.startDate = new Date(Date.UTC($scope.startDate.getFullYear(),
                $scope.startDate.getMonth(),
                $scope.startDate.getDate(),
                $scope.startDate.getHours(),
                $scope.startDate.getMinutes()));
            $scope.BillPosting.endDate = new Date(Date.UTC($scope.endDate.getFullYear(),
                $scope.endDate.getMonth(),
                $scope.endDate.getDate(),
                $scope.endDate.getHours(),
                $scope.endDate.getMinutes()));
            PointOfSaleService.getNotPotedBills($scope.BillPosting).then(function (results) {
                $scope.searchResultBills = results.data;
                $scope.disableSave = false;
            });
        }
        else swal("عفوا", "يجب اختيار تاريخ البداية و تاريخ النهاية ", "error");
    }

    $scope.clearEnity = function () {
        $scope.BillPosting = {};
        $scope.startDate = new Date();
        $scope.endDate = new Date();
        $scope.searchResultBills = [];
        $scope.disableSave = true;
    }

    $scope.SelectAllBills = function() {
        angular.forEach($scope.searchResultBills,
            function(item, index) {
                item.Selected = $scope.SelectAll;
            });
    }

    $scope.PostBills = function() {
        var selectedBills = $filter("filter")($scope.searchResultBills, { 'Selected': true }, true);
        //angular.forEach($scope.searchResultBills,function(item, index) {
        //    if (item.Selected === true) {
        //        billIds.push(item.BILL_ID);
        //    }
        //})
        if (selectedBills.length > 0) {
            for (var i = 0; i < selectedBills.length; i++) {

                (function () {
            //angular.forEach(selectedBills, function (item) {
            var index = i;
            $scope.billMaster = selectedBills[index];// item;
                 $q.all([
                    $scope.GetBillSetting(selectedBills[index].BILL_SETTING_ID),
                    $scope.getAllBillDetails(selectedBills[index].BILL_ID),
                    //$scope.getTotalQuantity(),
                    $scope.getEntryNumber()
                ])
                .then(function (allResponses) {
                    $scope.billMaster.accounts = [];
                    $q.all([
                        $scope.callAccountObject()
                        ]).then(function (response) {
                        $scope.saveEntry();
                    });

                    //PointOfSaleService.SetBillsPosted(billIds).then(function (result) {
                    //    if (result) {
                    //        swal("", "تم حفظ البيانات بنجاح ", "success");
                    //    }
                    //});
                });
            //})
                })();
            }
            swal("", "تم حفظ البيانات بنجاح ", "success");
            $scope.clearEnity();
        } else {
            swal("عفوا", "يجب اختيار الفواتير المراد ترحيلها اولا ", "error");
        }
    }

    $scope.getAllBillDetails = function (bill_ID) {

        PointOfSaleService.getAllBillDetails(bill_ID).then(function (response) {
            $scope.billDetails = response.data;
            if ($scope.billDetails != undefined || $scope.billDetails != []) {
                $scope.convertBillDetailsToSelectedItems();
            }
            //$scope.addGridBillDetailsItem();
            // debugger;
            //$scope.calculateTotals();
        })
    }

    ////////////////////////get  qunatity when choose items from selected list
    $scope.getTotalQuantity = function () {

        $scope.total24 = 0;
        $scope.total22 = 0;
        $scope.total21 = 0;
        $scope.total18 = 0;

        for (var i = 0; i < $scope.selectedItems.length; i++) {
            if ($scope.selectedItems[i].ITEM_CODE != null && $scope.selectedItems[i].ITEM_CODE != "" && $scope.selectedItems[i].ITEM_CODE != undefined) {

                if ($scope.IsQuantityCalculated == true) {
                    $scope.Quantity = $scope.selectedItems[i].Quantity;
                }
                else {
                    $scope.Quantity = 1;
                }

                if ($scope.selectedItems[i].ARName == "عيار 24") {
                    $scope.total24 = ((parseFloat($scope.Quantity * $scope.selectedItems[i].ItemWeight)) + parseFloat($scope.total24)).toFixed(2);
                    //$scope.matrixList[1].Col24 = '';
                }

                else if ($scope.selectedItems[i].ARName == "عيار 22") {
                    $scope.total22 = ((parseFloat($scope.Quantity * $scope.selectedItems[i].ItemWeight)) + parseFloat($scope.total22)).toFixed(2);
                    //$scope.matrixList[2].Col22 = '';
                }

                else if ($scope.selectedItems[i].ARName == "عيار 21") {
                    $scope.total21 = ((parseFloat($scope.Quantity * $scope.selectedItems[i].ItemWeight)) + parseFloat($scope.total21)).toFixed(2);
                    //$scope.matrixList[3].Col21 = '';
                }

                else if ($scope.selectedItems[i].ARName == "عيار 18") {
                    $scope.total18 = ((parseFloat($scope.Quantity * $scope.selectedItems[i].ItemWeight)) + parseFloat($scope.total18)).toFixed(2);
                    //$scope.matrixList[4].Col18 = '';
                }
            }
        }


        //$scope.matrixList[1].ColQuantity = $scope.total24;
        //$scope.matrixList[2].ColQuantity = $scope.total22;
        //$scope.matrixList[3].ColQuantity = $scope.total21;
        //$scope.matrixList[4].ColQuantity = $scope.total18;
        //$scope.recalculateTransQuant();
    }

    $scope.convertBillDetailsToSelectedItems = function () {
        $scope.selectedItems = [];

        for (var i = 0; i < $scope.billDetails.length; i++) {
            var x = {};
            x.ITEM_ID = $scope.billDetails[i].ITEM_ID;
            x.ITEM_CODE = $scope.billDetails[i].ITEM_CODE;
            //x.ITEM_CODE = $scope.billDetails[i].ITEM_ID;
            x.ITEM_AR_NAME = $scope.billDetails[i].ITEM_AR_NAME;
            x.Quantity = $scope.billDetails[i].QTY;

            x.GmPrice = (parseFloat($scope.billDetails[i].UNIT_PRICE) / parseFloat($scope.billDetails[i].ItemWeight)).toFixed(2);

            x.Price = parseFloat($scope.billDetails[i].UNIT_PRICE).toFixed(2);
            x.Unit = $scope.billDetails[i].UnitNameAr;
            x.CaliberID = $scope.billDetails[i].CaliberID;
            x.ARName = $scope.billDetails[i].ARName;
            x.LatName = $scope.billDetails[i].LatName;
            x.ClearnessRate = parseFloat($scope.billDetails[i].ClearnessRate);
            x.SubjectToVAT = $scope.billDetails[i].SubjectToVAT;

            x.VATRate = $scope.billDetails[i].VATRate;

            x.DISC_VALUE = $scope.billDetails[i].DISC_VALUE;
            x.DISC_RATE = $scope.billDetails[i].DISC_RATE;

            x.COMPANY_ID = $scope.billDetails[i].COMPANY_ID;

            if ($scope.IsQuantityCalculated == true) {
                $scope.Quantity = x.Quantity;
            }
            else {
                $scope.Quantity = 1;
            }
            x.Total = (parseFloat(x.Price) * parseFloat($scope.Quantity)).toFixed(2);

            x.NetTotal = (parseFloat(x.Total) - parseFloat(x.DISC_VALUE)).toFixed(2);

            x.ItemWeight = $scope.billDetails[i].ItemWeight;
            //x.itemGmWages = $scope.billDetails[i].itemGmWages;
            x.ManufacturingWages = $scope.billDetails[i].ManufacturingWages;

            x.itemGmWages = 0;
            if ($scope.billDetails[i].ItemStatus == 2) {
                x.itemGmWages = (parseFloat($scope.billDetails[i].ManufacturingWages) / parseFloat($scope.billDetails[i].ItemWeight)).toFixed(2);
                // x.ManufacturingWages = item.ManufacturingWages;
            }
            else {

                x.itemGmWages = (parseFloat($scope.billDetails[i].ManufacturingWages) / parseFloat($scope.billDetails[i].ItemWeight)).toFixed(2);
                //x.ManufacturingWages = (parseFloat(x.itemGmWages) * parseFloat($scope.billDetails[i].ItemWeight)).toFixed(2);
                //     $scope.gridBillDetailsItem.ManufacturingWages = item.itemGmWages * parseFloat(item.ItemWeight);
            }

            // x.TotalItemGmWages = (parseFloat($scope.billDetails[i].ActualItemWeight) * parseFloat($scope.billDetails[i].itemGmWages)).toFixed(2);

            x.TotalItemGmWages = (parseFloat(x.itemGmWages) * parseFloat($scope.billDetails[i].ActualItemWeight)).toFixed(2);

            x.WagesDiscValue = $scope.billDetails[i].WagesDiscValue;
            if (x.WagesDiscValue == null) {
                x.WagesDiscValue = 0;
            }


            if ($scope.ShowPriceTypeID == 2) {
                x.TotalGoldManfact = parseFloat(x.NetTotal);
            }
            else {
                x.TotalGoldManfact = (parseFloat(x.TotalItemGmWages) - parseFloat(x.WagesDiscValue)) + parseFloat(x.NetTotal);
            }

            x.CostCenterID = $scope.billDetails[i].CostCenterID;
            x.Discount = $scope.billDetails[i].DISC_VALUE;
            x.SubjectToVAT = $scope.billDetails[i].SubjectToVAT;

            x.ProfitMargin = $scope.billDetails[i].ProfitMargin;
            // x.ManufacturingWages = $scope.billDetails[i].ManufacturingWages;


            x.WagesDiscRate = $scope.billDetails[i].WagesDiscRate;
            x.ItemStatus = $scope.billDetails[i].ItemStatus;

            x.GemsWages = $scope.billDetails[i].GemsWages;
            x.GemsWeight = $scope.billDetails[i].GemsWeight;

            x.ActualItemWeight = $scope.billDetails[i].ActualItemWeight;

            x.IsExemptOfTax = $scope.billDetails[i].IsExemptOfTax;

            if ($scope.billDetails[i].VATRate != null && $scope.billDetails[i].VATRate != undefined && $scope.billDetails[i].VATRate != 0) {
                var vatval = ((parseFloat($scope.billDetails[i].VATRate) / 100) * parseFloat(x.Total)).toFixed(2);
                x.TaxTotal = (parseFloat(x.Total) + parseFloat(vatval)).toFixed(2);

            }

            //if ($scope.BillRowsNumber > 0) {
            //    if ((parseFloat($scope.BillRowsNumber)) + 1 > $scope.selectedItems.length) {
            //        $scope.selectedItems.push(x);
            //    }
            //    else {
            //        swal('عفواً',
            //            'لا يمكن اضافه صفوف زياده',
            //            'error');
            //    }
            //}
            //else {
                $scope.selectedItems.push(x);
            //}

            // $scope.selectedItems.push(x);

        }
        $scope.getTotalQuantity();
    }


    $scope.GetBillSetting = function (billSettingId) {
        //var settingID = $scope.getBillTypeQueryString();
        //var userID = authService.GetUserID();

        //var userName = authService.GetUserName();

        let promise = billSettingService.getByID(billSettingId).then(function (result) {

            var billSetting = result.data;
            $scope.billSetting = billSetting;
            //$scope.billMaster.SELL_TYPE_ID = billSetting.BILL_SELL_TYPE_ID;

            //$scope.billMaster.BILL_PAY_WAY = billSetting.BILL_PAY_TYPE;

            //if (billSetting.AccGoldID != undefined && billSetting.AccGoldID != null) {
            //    $scope.billMaster.GOLD_ACC_ID = billSetting.AccGoldID;
            //    $scope.changeGoldBalance($scope.billMaster.GOLD_ACC_ID);
            //}
            ////  debugger;
            //$scope.typeOfBill = billSetting.BILL_TYPE_ID;

            //$scope.billMaster.ITEM_ACC_ID = billSetting.ACC_ITEM_ID;
            //$scope.billMaster.EMP_ID = userName;
            //$scope.billMaster.CURRENCY_ID = billSetting.CURRENCY_ID;
            //$scope.billMaster.CURRENCY_RATE = billSetting.CURRENCY_RATE;
            $scope.bill_type_Id = billSetting.BILL_TYPE_ID;
            $scope.acc_wages_Id = billSetting.AccWagesID;
            $scope.acc_wages_taxId = billSetting.AccWagesTaxID;
            $scope.wages_taxValue = billSetting.WagesTaxValue;
            $scope.acc_purchas_Id = billSetting.PurchasAccID;
            $scope.acc_purchas_taxId = billSetting.PurchasTaxAccID;
            $scope.acc_discountVal = billSetting.ACC_DISC_ID;
            $scope.acc_sales_Id = billSetting.AccSalesID;
            $scope.acc_vat_rateId = billSetting.AccVatRateID;



            //if (settingID == 11 || settingID == 12) {
            //    $scope.billMaster.COM_STORE_ID = billSetting.TO_COM_STORE_ID;
            //} else {
            //    $scope.billMaster.COM_STORE_ID = billSetting.COM_STORE_ID
            //}


            if ($scope.bill_type_Id == 19 || $scope.bill_type_Id == 20) {
                $scope.totalName = "الاجمالي";
            }

            //$scope.billSettingID = settingID;
            $scope.settingsAccounts = billSetting;
            $scope.checkBillSettingsGrid(billSettingId);
            $scope.checkBillSettings(billSetting);
            //$scope.GetAllBoxAccounts(billSetting);
            //$scope.GetAllCustomersAccounts(billSetting);
            //$scope.GetAllGoldBoxAccounts(billSetting);
            //$scope.getAccountList(billSetting);
            $scope.getAllTpayTypes(billSetting);

            //$scope.getGridColData(settingID);
            //$scope.callAccountObject();

        });
        return promise;
    };

    //////////////////////////////check bill settings of grid columns
    $scope.checkBillSettingsGrid = function (settingID) {
        $scope.isItem = false;
        $scope.isQty = false;
        $scope.isItemWeight = false;
        $scope.isItemGmWages = false;
        $scope.isManufactWages = false;
        $scope.isUnit = false;
        $scope.isARName = false;
        $scope.isUnitCost = false;
        $scope.isDiscRate = false;
        $scope.isDiscValue = false;
        $scope.isTotal = false;
        $scope.isClearnessRate = false;
        $scope.isCaliber24 = false;
        $scope.isCaliber22 = false;
        $scope.isCaliber21 = false;
        $scope.isCaliber18 = false;
        $scope.isCostCenter = false;
        $scope.isSubjectToVAT = false;
        $scope.isVATRate = false;
        $scope.isVatValue = false;

        $scope.isWagesDiscValue = false;
        $scope.isWagesDiscRate = false;
        $scope.isActualWeight = false;

        $scope.isTaxTotal = false;

        $scope.isTotalItemGmWages = false;
        $scope.isTotalGoldManfact = false;

        $scope.isExemptOfTax = false;
        $scope.isItemPrice = false;

        $scope.isAfterDiscount = false;

        // $scope.settingId = ($location.search()).billType;

        billGridColumn.getBySettingID(settingID).then(function (result) {
            $scope.gridData = result.data;
            if ($scope.gridData.ITEM_INDEX == null || $scope.gridData.ITEM_INDEX == 0) { $scope.isItem = true; }

            if ($scope.gridData.QTY_INDEX == null || $scope.gridData.QTY_INDEX == 0) { $scope.isQty = true; }

            if ($scope.gridData.Item_Weight_INDEX == null || $scope.gridData.Item_Weight_INDEX == 0) { $scope.isItemWeight = true; }

            if ($scope.gridData.Item_Wages_INDEX == null || $scope.gridData.Item_Wages_INDEX == 0) { $scope.isItemGmWages = true; }

            if ($scope.gridData.Manufact_Wages_INDEX == null || $scope.gridData.Manufact_Wages_INDEX == 0) { $scope.isManufactWages = true; }

            if ($scope.gridData.UNIT_INDEX == null || $scope.gridData.UNIT_INDEX == 0) { $scope.isUnit = true; }

            if ($scope.gridData.ARName_INDEX == null || $scope.gridData.ARName_INDEX == 0) { $scope.isARName = true; }

            if ($scope.gridData.UNIT_COST_INDEX == null || $scope.gridData.UNIT_COST_INDEX == 0) { $scope.isUnitCost = true; }

            if ($scope.gridData.DISC_RATE_INDEX == null || $scope.gridData.DISC_RATE_INDEX == 0) { $scope.isDiscRate = true; }

            if ($scope.gridData.DISC_VALUE_INDEX == null || $scope.gridData.DISC_VALUE_INDEX == 0) { $scope.isDiscValue = true; }

            if ($scope.gridData.TOTAL_INDEX == null || $scope.gridData.TOTAL_INDEX == 0) { $scope.isTotal = true; }

            if ($scope.gridData.Clearness_Rate_INDEX == null || $scope.gridData.Clearness_Rate_INDEX == 0) { $scope.isClearnessRate = true; }

            if ($scope.gridData.Caliber24_INDEX == null || $scope.gridData.Caliber24_INDEX == 0) { $scope.isCaliber24 = true; }

            if ($scope.gridData.Caliber22_INDEX == null || $scope.gridData.Caliber22_INDEX == 0) { $scope.isCaliber22 = true; }

            if ($scope.gridData.Caliber21_INDEX == null || $scope.gridData.Caliber21_INDEX == 0) { $scope.isCaliber21 = true; }

            if ($scope.gridData.Caliber18_INDEX == null || $scope.gridData.Caliber18_INDEX == 0) { $scope.isCaliber18 = true; }

            if ($scope.gridData.Subject_To_VAT_INDEX == null || $scope.gridData.Subject_To_VAT_INDEX == 0) { $scope.isSubjectToVAT = true; }

            if ($scope.gridData.VAT_Rate_INDEX == null || $scope.gridData.VAT_Rate_INDEX == 0) { $scope.isVATRate = true; }

            if ($scope.gridData.Cost_Center_INDEX == null || $scope.gridData.Cost_Center_INDEX == 0) { $scope.isCostCenter = true; }


            if ($scope.gridData.VatValue_INDEX == null || $scope.gridData.VatValue_INDEX == 0) { $scope.isVatValue = true; }



            if ($scope.gridData.WagesDiscValue_INDEX == null || $scope.gridData.WagesDiscValue_INDEX == 0) { $scope.isWagesDiscValue = true; }

            if ($scope.gridData.WagesDiscRate_INDEX == null || $scope.gridData.WagesDiscRate_INDEX == 0) { $scope.isWagesDiscRate = true; }

            if ($scope.gridData.ActualWeight_INDEX == null || $scope.gridData.ActualWeight_INDEX == 0) { $scope.isActualWeight = true; }

            if ($scope.gridData.TaxTotal_INDEX == null || $scope.gridData.TaxTotal_INDEX == 0) { $scope.isTaxTotal = true; }

            if ($scope.gridData.TotalItemGmWages_INDEX == null || $scope.gridData.TotalItemGmWages_INDEX == 0) { $scope.isTotalItemGmWages = true; }


            if ($scope.gridData.TotalGoldManufact_INDEX == null || $scope.gridData.TotalGoldManufact_INDEX == 0) { $scope.isTotalGoldManfact = true; }

            if ($scope.gridData.ExemptOfTax_INDEX == null || $scope.gridData.ExemptOfTax_INDEX == 0) { $scope.isExemptOfTax = true; }

            if ($scope.gridData.ItemPrice_INDEX == null || $scope.gridData.ItemPrice_INDEX == 0) { $scope.isItemPrice = true; }

            if ($scope.gridData.AfterDiscount_INDEX == null || $scope.gridData.AfterDiscount_INDEX == 0) { $scope.isAfterDiscount = true; }

        });
    }

    ///////get show and hide of master setting
    $scope.checkBillSettings = function (billSettingObj) {
        // var settingID = ($location.search()).billType;
        $scope.IsBillDate = true;
        $scope.IsShiftNumber = true;
        $scope.IsSellType = true;
        $scope.IsPayWay = true;
        $scope.IsAccount = true;
        $scope.IsCustAccID = true;
        $scope.IsItemAccID = true;
        $scope.IsCompStoreID = true;
        $scope.IsEmpID = true;
        $scope.IsBillRemarks = true;
        $scope.IsCurrency = true;
        $scope.IsPayTypes = true;
        $scope.IsCurrencyTrans = true;
        $scope.IsCostCenter = true;

        $scope.IsToCompStore = true;
        $scope.IsTotalExtra = true;
        $scope.IsTotalMustPaid = true;
        $scope.IsTotalRemaining = true;
        $scope.IsTotalPaid = true;
        $scope.IsItemsGroups = true;
        $scope.IsItems = true;
        $scope.BILL_SHORTCUT = true;

        $scope.IsQuantityCalculated = false;

        $scope.IsShowEditReason = false;
        $scope.IsShowDeliveryPerson = false;


        $scope.IsShowExternalNumber = false;
        $scope.IsEnableTaxEdit = true;
        $scope.IsShowGoldBoxBalance = false;
        $scope.IsShowCustomerBalance = false;

        $scope.IsCalcClearnessRate = false;

        $scope.IsBillDiscValue = true;
        $scope.IsBillDiscRate = true;
        $scope.IsTotalDiscValues = true;
        $scope.IsTotalDiscRates = true;

        $scope.IsCalcBillDiscOfTotal = false;
        $scope.IsRepeatItem = false;
        $scope.IsQuickAccount = false;
        $scope.BillRowsNumber = 0;

        $scope.IsEntryGoldItemsAccounts = false;

        $scope.IsTotalsSummary = false;
        $scope.IsShowTaxesBox = false;

        $scope.IsEnableWeight = false;
        $scope.IsEnableGmWages = false;


        $scope.ShowWagesDiscount = true;
        $scope.BillVatRate = 0;
        $scope.settingChecks = billSettingObj;

        $scope.billMaster.ExemptOfTaxRate = 0;
        $scope.billMaster.MainVatRate = 0;

        if ($scope.settingChecks.IsBillDate == true) {
            $scope.IsBillDate = false;
        }

        if ($scope.settingChecks.IsShiftNumber == true) {
            $scope.IsShiftNumber = false;
        }

        if ($scope.settingChecks.IsSellType == true) {
            $scope.IsSellType = false;
        }

        if ($scope.settingChecks.IsPayWay == true) {
            $scope.IsPayWay = false;
        }

        if ($scope.settingChecks.IsAccount == true) {
            $scope.IsAccount = false;
        }

        if ($scope.settingChecks.IsCustAccID == true) {
            $scope.IsCustAccID = false;
        }

        if ($scope.settingChecks.IsItemAccID == true) {
            $scope.IsItemAccID = false;
        }

        if ($scope.settingChecks.IsCompStoreID == true) {
            $scope.IsCompStoreID = false;
        }

        if ($scope.settingChecks.IsEmpID == true) {
            $scope.IsEmpID = false;
        }

        if ($scope.settingChecks.IsBillRemarks == true) {
            $scope.IsBillRemarks = false;
        }

        if ($scope.settingChecks.IsCurrency == true) {
            $scope.IsCurrency = false;
        }

        if ($scope.settingChecks.IsPayTypes == true) {
            $scope.IsPayTypes = false;
        }

        if ($scope.settingChecks.IsCurrencyTrans == true) {
            $scope.IsCurrencyTrans = false;
        }

        if ($scope.settingChecks.SHOW_COST_CENTER == true) {
            $scope.IsCostCenter = false;
        }

        //////////
        if ($scope.settingChecks.IsTotalExtra == true) {
            $scope.IsTotalExtra = false;
        }

        if ($scope.settingChecks.IsTotalPaid == true) {
            $scope.IsTotalPaid = false;
        }

        if ($scope.settingChecks.IsTotalMustPaid == true) {
            $scope.IsTotalMustPaid = false;
        }

        if ($scope.settingChecks.IsTotalRemaining == true) {
            $scope.IsTotalRemaining = false;
        }

        if ($scope.settingChecks.IsToCompStore == true) {
            $scope.IsToCompStore = false;
        }

        if ($scope.settingChecks.IsItemsGroups == true) {
            $scope.IsItemsGroups = false;
        }

        if ($scope.settingChecks.IsItems == true) {
            $scope.IsItems = false;
        }
        if ($scope.settingChecks.Show_Wages_Discount == true) {
            $scope.ShowWagesDiscount = false;
        }
        if ($scope.settingChecks.BILL_SHORTCUT == true) {
            $scope.BILL_SHORTCUT = false;
        }

        if ($scope.settingChecks.IsQuantityCalculated == true) {
            $scope.IsQuantityCalculated = true;
        }


        if ($scope.settingChecks.IsShowEditReason == true) {
            $scope.IsShowEditReason = true;
            var urlParams = $location.search();
            if (urlParams.foo != null && urlParams.foo != undefined && urlParams.foo != "") {
                $scope.isEditReason = $scope.IsShowEditReason;
            }
        }

        if ($scope.settingChecks.IsShowDeliveryPerson == true) {
            $scope.IsShowDeliveryPerson = true;
        }


        if ($scope.settingChecks.IsShowExternalNumber == true) {
            $scope.IsShowExternalNumber = true;
        }

        if ($scope.settingChecks.IsEnableTaxEdit == true) {
            $scope.IsEnableTaxEdit = false;
        }

        if ($scope.settingChecks.IsShowGoldBoxBalance == true) {
            $scope.IsShowGoldBoxBalance = true;
        }

        if ($scope.settingChecks.IsShowCustomerBalance == true) {
            $scope.IsShowCustomerBalance = true;
        }


        if ($scope.settingChecks.IsCalcClearnessRate == true) {
            $scope.IsCalcClearnessRate = true;
        }



        if ($scope.settingChecks.IsBillDiscValue == true) {
            $scope.IsBillDiscValue = false;
        }

        if ($scope.settingChecks.IsBillDiscRate == true) {
            $scope.IsBillDiscRate = false;
        }

        if ($scope.settingChecks.IsTotalDiscValues == true) {
            $scope.IsTotalDiscValues = false;
        }

        if ($scope.settingChecks.IsTotalDiscRates == true) {
            $scope.IsTotalDiscRates = false;
        }

        if ($scope.settingChecks.IsCalcBillDiscOfTotal == true) {
            $scope.IsCalcBillDiscOfTotal = true;
        }


        if ($scope.settingChecks.IsRepeatItem == true) {
            $scope.IsRepeatItem = true;
        }

        if ($scope.settingChecks.IsQuickAccount == true) {
            $scope.IsQuickAccount = true;
        }

        if ($scope.settingChecks.IsEntryGoldItemsAccounts == true) {
            $scope.IsEntryGoldItemsAccounts = true;
        }

        if ($scope.settingChecks.IsTotalsSummary == true) {
            $scope.IsTotalsSummary = true;
        }


        if ($scope.settingChecks.IsShowTaxesBox == true) {
            $scope.IsShowTaxesBox = true;
        }


        if ($scope.settingChecks.IsEnableWeight == true) {
            $scope.IsEnableWeight = true;
        }

        if ($scope.settingChecks.IsEnableGmWages == true) {
            $scope.IsEnableGmWages = true;
        }


        ///////
        if ($scope.settingChecks.BillEmpName != null && $scope.settingChecks.BillEmpName != undefined) {
            $scope.BillEmpName = $scope.settingChecks.BillEmpName;
        }

        if ($scope.settingChecks.BillAccountName != null && $scope.settingChecks.BillAccountName != undefined) {
            $scope.BillAccountName = $scope.settingChecks.BillAccountName;
        }

        if ($scope.settingChecks.BILL_SHOW_NAME != null && $scope.settingChecks.BILL_SHOW_NAME != undefined) {
            $scope.BILL_SHOW_NAME = $scope.settingChecks.BILL_SHOW_NAME;
        }


        if ($scope.settingChecks.IS_AUTO_POSTING != null && $scope.settingChecks.IS_AUTO_POSTING != undefined) {
            $scope.isAutoPosted = $scope.settingChecks.IS_AUTO_POSTING;
            $scope.autoTransfer = true;
        }

        if ($scope.settingChecks.CashAccountID != null && $scope.settingChecks.CashAccountID != undefined) {
            $scope.cashAccountID = $scope.settingChecks.CashAccountID;
        }

        if ($scope.settingChecks.VisaAccountID != null && $scope.settingChecks.VisaAccountID != undefined) {
            $scope.visaAccountID = $scope.settingChecks.VisaAccountID;
        }

        if ($scope.settingChecks.NoPaidAccountID != null && $scope.settingChecks.NoPaidAccountID != undefined) {
            $scope.noPaidAccountID = $scope.settingChecks.NoPaidAccountID;
        }

        if ($scope.settingChecks.PaymentAccountID != null && $scope.settingChecks.PaymentAccountID != undefined) {
            $scope.paymentAccountID = $scope.settingChecks.PaymentAccountID;
        }

        if ($scope.settingChecks.ShowPriceTypeID != null && $scope.settingChecks.ShowPriceTypeID != undefined) {
            $scope.ShowPriceTypeID = $scope.settingChecks.ShowPriceTypeID;
        }

        if ($scope.settingChecks.BillRowsNumber != null && $scope.settingChecks.BillRowsNumber != undefined) {
            $scope.BillRowsNumber = $scope.settingChecks.BillRowsNumber;
        }


        if ($scope.settingChecks.BillVatRate != null && $scope.settingChecks.BillVatRate != undefined) {
            $scope.BillVatRate = $scope.settingChecks.BillVatRate;
        }


        if ($scope.settingChecks.ExemptVatRate != null && $scope.settingChecks.ExemptVatRate != undefined) {
            $scope.billMaster.ExemptOfTaxRate = $scope.settingChecks.ExemptVatRate;
        }

        if ($scope.settingChecks.MainVatRate != null && $scope.settingChecks.MainVatRate != undefined) {
            $scope.billMaster.MainVatRate = $scope.settingChecks.MainVatRate;
        }
    }

    //GetAllTpayTypes
    $scope.getAllTpayTypes = function (billSettingObj) {
        $scope.tpayTypes = [];

        payTypesService.getAllActive().then(function (response) {
            //PointOfSaleService.getAllTPayTypes().then(function (response) {
            $scope.tpayTypes = response.data;

            //var settingID = $scope.getBillTypeQueryString();
            //var billType = 0;
            //billSettingService.getByID(settingID).then(function (result) {
            var billType = billSettingObj.BILL_TYPE_ID;

            if (billType == 1) {
                for (var i = 0; i < $scope.tpayTypes.length; i++) {
                    if ($scope.tpayTypes[i].PAY_TYPE_ID == $scope.billMaster.BILL_PAY_WAY) {
                        var urlParams = $location.search();
                        if (urlParams.foo != null && urlParams.foo != undefined && urlParams.foo != "") { } else {
                            $scope.billMaster.CUST_ACC_ID = $scope.tpayTypes[i].AccountID;
                        }
                    }
                }
            }
            // });

        })
    }

    /////////////////////////////call all accounts of object
    $scope.callAccountObject = function () {

        if ($scope.bill_type_Id == 19) {
            $scope.getWagesAccFromSettings();
            $scope.getWagesTaxAccFromSettings();
            $scope.getCustObject();

            if ($scope.IsEntryGoldItemsAccounts) {
                $scope.getGoldObjectsByItems();
            }
            else {
                $scope.getGoldObject();
            }
        }
        else if ($scope.bill_type_Id == 20) {

            $scope.getWagesAccFromSettings();
            $scope.getWagesTaxAccFromSettings();

            $scope.getPurchasAccFromSettings();
            $scope.getPurchasTaxAccFromSettings();

            if ($scope.IsEntryGoldItemsAccounts) {
                $scope.getGoldObjectsByItems();
            }
            else {
                $scope.getGoldObject();
            }

            $scope.getPurchasGoldAccFromSettings();
            //$scope.customerForPurchase();
            $scope.getCustObject();
            if (!$scope.IsPayTypes) {
                $scope.getPayTypesAccountObjects();
            }
        }

        else if ($scope.bill_type_Id == 2) {
            if ($scope.IsEntryGoldItemsAccounts) {
                $scope.getGoldObjectsByItems();
            }
            else {
                $scope.getGoldObject();
            }

            $scope.getSalesGoldAccFromSettings();
            $scope.getPayTypesAccountObjects();
            $scope.getAccSalesFromSettings();

            if ($scope.billMaster.TotalVatValue != undefined && $scope.billMaster.TotalVatValue > 0) {
                $scope.getVatRateAccFromsettings();
            }

            if ($scope.billMaster.TotalRemaining != undefined && $scope.billMaster.TotalRemaining) {
                $scope.getCustObject();
            }
        }

        else if ($scope.bill_type_Id == 1) {
            if ($scope.IsEntryGoldItemsAccounts) {
                $scope.getGoldObjectsByItems();
            }
            else {
                $scope.getGoldObject();
            }
            $scope.getPurchasGoldAccFromSettings();
            $scope.getCustObject();
            $scope.getPurchasAccFromSettings();
            if ($scope.billMaster.TotalVatValue > 0) {
                $scope.getPurchasTaxAccFromSettings();
            }

        }

        else if ($scope.bill_type_Id == 23) {
            $scope.getCustObject();

            if ($scope.IsEntryGoldItemsAccounts) {
                $scope.getGoldObjectsByItems();
            }
            else {
                $scope.getGoldObject();
            }

            //$scope.getPurchasAccFromSettings();
            $scope.getWagesAccFromSettings();
            $scope.getWagesTaxAccFromSettings();
        }
        else if ($scope.bill_type_Id == 24) {

            $scope.getCustObject();

            if ($scope.IsEntryGoldItemsAccounts) {
                $scope.getGoldObjectsByItems();
            }
            else {
                $scope.getGoldObject();
            }

            $scope.getWagesAccFromSettings();
            $scope.getWagesTaxAccFromSettings();
            $scope.getPurchasAccFromSettings();
            $scope.getPurchasTaxAccFromSettings();
            $scope.getPurchasGoldAccFromSettings();

        }
        else if ($scope.bill_type_Id == 18) {

            $scope.getCustObject();
            if ($scope.IsEntryGoldItemsAccounts) {
                $scope.getGoldObjectsByItems();
            }
            else {
                $scope.getGoldObject();
            }

            $scope.getPurchasGoldObject('C');
            $scope.getPurchasGoldAccFromSettings();
            $scope.getDiscountAccFromSettings();
            $scope.getVatRateAccFromsettings();
        }
        else if ($scope.bill_type_Id == 17) {

            $scope.getCustObject();
            //if ($scope.IsEntryGoldItemsAccounts) {
            //    $scope.getGoldObjectsByItems();
            //}
            //else {
            //    $scope.getGoldObject();
            //}
            $scope.getAccSalesFromSettings();
            $scope.getSalesGoldAccFromSettings();
            $scope.getDiscountAccFromSettings();
            $scope.getVatRateAccFromsettings();
        }
        else {
            $scope.getCustObject();

            if ($scope.IsEntryGoldItemsAccounts) {
                $scope.getGoldObjectsByItems();
            }
            else {
                $scope.getGoldObject();
            }
            $scope.getPayBoxObject();
            $scope.getPayTypesAccountObjects();
            $scope.getDiscAccFromSettings();
            $scope.getStoreAccFromSettings();
            $scope.getWagesAccFromSettings();
            $scope.getGiftAccFromSettings();
            $scope.getCostAccFromSettings();
            $scope.getExtraAccFromSettings();
            $scope.getVatRateAccFromsettings();
        }

    }

    /////////////////////////////////////////////////get customer accounts object to bill master ///////////////////////////////////////////
    $scope.getCustObject = function () {
        // debugger;

        $scope.custObject = {};
        if ($scope.billMaster.CUST_ACC_ID != undefined && $scope.billMaster.CUST_ACC_ID != null) {
            $scope.custObject.accountID = $scope.billMaster.CUST_ACC_ID;

            if ($scope.bill_type_Id == 23 || $scope.bill_type_Id == 24 || $scope.bill_type_Id == 19 || $scope.bill_type_Id == 20) {

                var TotalManufacturingWages = parseFloat($scope.billMaster.TotalManufactWages);// - $scope.wagesDiscountVal);
                var ManufacturingWagesTaxes = parseFloat(parseFloat($scope.billMaster.manufacturingWagesMainVat) * parseFloat($scope.billMaster.MainVatRate) / 100);
                var TotalPurchasingValues = parseFloat($scope.billMaster.TotalPrice - $scope.billMaster.TotalAllDisc);
                var PurchasingValuesTaxes = parseFloat($scope.billMaster.TotalMainVat) * parseFloat($scope.billMaster.MainVatRate) / 100;

                $scope.custObject.accountType = $scope.bill_type_Id == 19 || $scope.bill_type_Id == 20 ? "C" : "D";
                $scope.custObject.moneyValue = $scope.bill_type_Id == 19 || $scope.bill_type_Id == 23 ? (TotalManufacturingWages + ManufacturingWagesTaxes).toFixed(2) :
                    (TotalManufacturingWages + TotalPurchasingValues + parseFloat($scope.billMaster.MainVatValue)).toFixed(2);
                $scope.custObject.gold24Value = $scope.bill_type_Id == 19 || $scope.bill_type_Id == 23 ? $scope.total24 : 0;
                $scope.custObject.gold22Value = $scope.bill_type_Id == 19 || $scope.bill_type_Id == 23 ? $scope.total22 : 0;
                $scope.custObject.gold21Value = $scope.bill_type_Id == 19 || $scope.bill_type_Id == 23 ? $scope.total21 : 0;
                $scope.custObject.gold18Value = $scope.bill_type_Id == 19 || $scope.bill_type_Id == 23 ? $scope.total18 : 0;
                $scope.billMaster.accounts.push($scope.custObject);

                //if ($scope.bill_type_Id == 23 || $scope.bill_type_Id == 19) {
                //    //قيد كميات الذهب
                //    //$scope.checkAccountTypes($scope.bill_type_Id, 'S')
                //    $scope.custObject.accountType = $scope.bill_type_Id == 19 ? "C" : "D";
                //    $scope.custObject.moneyValue = 0;
                //    $scope.custObject.gold24Value = $scope.total24;
                //    $scope.custObject.gold22Value = $scope.total22;
                //    $scope.custObject.gold21Value = $scope.total21;
                //    $scope.custObject.gold18Value = $scope.total18;
                //    $scope.billMaster.accounts.push($scope.custObject);
                //}
                //if ($scope.bill_type_Id == 20 || $scope.bill_type_Id == 24) {
                //    //قيد قيمة المشتريات
                //    //$scope.checkAccountTypes($scope.bill_type_Id, 'S')
                //    $scope.custObject = {};
                //    $scope.custObject.accountID = $scope.billMaster.CUST_ACC_ID;
                //    $scope.custObject.accountType = $scope.bill_type_Id == 20 ? "C" : "D";
                //    $scope.custObject.moneyValue = $scope.billMaster.TotalPrice - $scope.billMaster.TotalAllDisc;
                //    $scope.custObject.gold24Value = 0;
                //    $scope.custObject.gold22Value = 0;
                //    $scope.custObject.gold21Value = 0;
                //    $scope.custObject.gold18Value = 0;
                //    $scope.billMaster.accounts.push($scope.custObject);

                //    //قيد قيمة ضريبة المشتريات
                //    $scope.custObject = {};
                //    $scope.custObject.accountID = $scope.billMaster.CUST_ACC_ID;
                //    $scope.custObject.accountType = $scope.bill_type_Id == 20 ? "C" : "D";
                //    $scope.custObject.moneyValue = $scope.footerTotalVatValue;
                //    $scope.custObject.gold24Value = 0;
                //    $scope.custObject.gold22Value = 0;
                //    $scope.custObject.gold21Value = 0;
                //    $scope.custObject.gold18Value = 0;
                //    $scope.billMaster.accounts.push($scope.custObject);
                //}
                ////قيد قيمة اجور التصنيع
                //$scope.custObject = {};
                //$scope.custObject.accountID = $scope.billMaster.CUST_ACC_ID;
                //$scope.custObject.accountType = $scope.bill_type_Id == 19 || $scope.bill_type_Id == 20 ? "C" : "D";
                //$scope.custObject.moneyValue = $scope.billMaster.TotalManufactWages - $scope.wagesDiscountVal;
                //$scope.custObject.gold24Value = 0;
                //$scope.custObject.gold22Value = 0;
                //$scope.custObject.gold21Value = 0;
                //$scope.custObject.gold18Value = 0;
                //$scope.billMaster.accounts.push($scope.custObject);

                ////قيد قيمة ضريبة اجور التصنيع
                //$scope.custObject = {};
                //$scope.custObject.accountID = $scope.billMaster.CUST_ACC_ID;
                //$scope.custObject.accountType = $scope.bill_type_Id == 19 || $scope.bill_type_Id == 20 ? "C" : "D";
                //$scope.custObject.moneyValue = parseFloat($scope.billMaster.TotalTaxableManufactWages) * $scope.wages_taxValue / 100;
                //$scope.custObject.gold24Value = 0;
                //$scope.custObject.gold22Value = 0;
                //$scope.custObject.gold21Value = 0;
                //$scope.custObject.gold18Value = 0;
                //$scope.billMaster.accounts.push($scope.custObject);
            }

            else if ($scope.bill_type_Id == 2) {
                $scope.custObject = {};
                $scope.custObject.accountID = $scope.billMaster.CUST_ACC_ID;
                $scope.custObject.accountType = 'D';
                $scope.custObject.moneyValue = $scope.billMaster.TotalRemaining;
                $scope.custObject.gold24Value = 0;
                $scope.custObject.gold22Value = 0;
                $scope.custObject.gold21Value = 0;
                $scope.custObject.gold18Value = 0;
                $scope.billMaster.accounts.push($scope.custObject);
            }

            else if ($scope.bill_type_Id == 1) {
                $scope.custObject = {};
                $scope.custObject.accountID = $scope.billMaster.CUST_ACC_ID;
                $scope.custObject.accountType = 'C';
                $scope.custObject.moneyValue = $scope.billMaster.TotalMustPaid;
                $scope.custObject.gold24Value = 0;
                $scope.custObject.gold22Value = 0;
                $scope.custObject.gold21Value = 0;
                $scope.custObject.gold18Value = 0;
                $scope.billMaster.accounts.push($scope.custObject);
            }
            else if ($scope.bill_type_Id == 17) {
                // قيد ريال مدين
                $scope.custObject = {};
                $scope.custObject.accountID = $scope.billMaster.CUST_ACC_ID;
                $scope.custObject.accountType = 'D';
                $scope.custObject.moneyValue = parseFloat($scope.billMaster.TotalMustPaid) + parseFloat($scope.billMaster.TotalAllDisc);
                $scope.custObject.gold24Value = 0;
                $scope.custObject.gold22Value = 0;
                $scope.custObject.gold21Value = 0;
                $scope.custObject.gold18Value = 0;
                $scope.billMaster.accounts.push($scope.custObject);

                // قيد ذهب دائن
                $scope.custObject = {};
                $scope.custObject.accountID = $scope.billMaster.CUST_ACC_ID;
                $scope.custObject.accountType = 'C';
                $scope.custObject.moneyValue = 0;
                $scope.custObject.gold24Value = $scope.total24;
                $scope.custObject.gold22Value = $scope.total22;
                $scope.custObject.gold21Value = $scope.total21;
                $scope.custObject.gold18Value = $scope.total18;
                $scope.billMaster.accounts.push($scope.custObject);
            }

            else if ($scope.bill_type_Id == 18) {
                $scope.checkAccountTypes($scope.bill_type_Id, 'S')
                $scope.custObject.accountType = $scope.typeName;
                $scope.custObject.moneyValue = $scope.billMaster.TotalMustPaid;
                $scope.custObject.gold24Value = 0;
                $scope.custObject.gold22Value = 0;
                $scope.custObject.gold21Value = 0;
                $scope.custObject.gold18Value = 0;

                $scope.billMaster.accounts.push($scope.custObject);
            }
            else {
                $scope.checkAccountTypes($scope.bill_type_Id, 'S')
                $scope.custObject.accountType = $scope.typeName;
                $scope.custObject.moneyValue = $scope.billMaster.TotalAfterTax;
                $scope.custObject.gold24Value = 0;
                $scope.custObject.gold22Value = 0;
                $scope.custObject.gold21Value = 0;
                $scope.custObject.gold18Value = 0;

                $scope.billMaster.accounts.push($scope.custObject);
            }

        }

        else {
            if ($scope.bill_type_Id == 1) {
                $scope.custObject = {};

                //if ($scope.billMaster.CUST_ACC_ID != undefined && $scope.billMaster.CUST_ACC_ID != null) {
                $scope.custObject.accountID = 54;
                //}

                $scope.custObject.accountType = 'C';
                $scope.custObject.moneyValue = $scope.billMaster.TotalMustPaid;
                $scope.custObject.gold24Value = 0;
                $scope.custObject.gold22Value = 0;
                $scope.custObject.gold21Value = 0;
                $scope.custObject.gold18Value = 0;
                $scope.billMaster.accounts.push($scope.custObject);
            }
        }
    }

    /////////////////////////get gold accounts object to bill master
    $scope.getGoldObject = function () {

        $scope.itemObject = {};
        if ($scope.billMaster.GOLD_ACC_ID != undefined && $scope.billMaster.GOLD_ACC_ID != null) {
            $scope.itemObject.accountID = $scope.billMaster.GOLD_ACC_ID;


            $scope.checkAccountTypes($scope.bill_type_Id, 'X')
            $scope.itemObject.accountType = $scope.typeName;

            if ($scope.bill_type_Id == 2 || $scope.bill_type_Id == 23 || $scope.bill_type_Id == 24) {
                $scope.checkAccountTypes($scope.bill_type_Id, 'X')
                $scope.itemObject.accountType = $scope.typeName;

                $scope.itemObject.moneyValue = 0;
                $scope.itemObject.gold24Value = $scope.total24;
                $scope.itemObject.gold22Value = $scope.total22;
                $scope.itemObject.gold21Value = $scope.total21;
                $scope.itemObject.gold18Value = $scope.total18;
            }

            else if ($scope.bill_type_Id == 1) {

                $scope.checkAccountTypes($scope.bill_type_Id, 'X')
                $scope.itemObject.accountType = $scope.typeName;
                $scope.itemObject.moneyValue = 0;
                $scope.itemObject.gold24Value = $scope.total24;
                $scope.itemObject.gold22Value = $scope.total22;
                $scope.itemObject.gold21Value = $scope.total21;
                $scope.itemObject.gold18Value = $scope.total18;
            }

            else if ($scope.bill_type_Id == 19 || $scope.bill_type_Id == 20) {
                $scope.itemObject.moneyValue = 0;
                $scope.itemObject.gold24Value = $scope.total24;
                $scope.itemObject.gold22Value = $scope.total22;
                $scope.itemObject.gold21Value = $scope.total21;
                $scope.itemObject.gold18Value = $scope.total18;
            }
            else {
                $scope.itemObject.moneyValue = 0;
                $scope.itemObject.gold24Value = $scope.total24;
                $scope.itemObject.gold22Value = $scope.total22;
                $scope.itemObject.gold21Value = $scope.total21;
                $scope.itemObject.gold18Value = $scope.total18;

            }
            $scope.billMaster.accounts.push($scope.itemObject);

            //$scope.convertGold.gold24Value = $scope.matrixList[1].TransQuant;
            //$scope.convertGold.gold22Value = $scope.matrixList[2].TransQuant;
            //$scope.convertGold.gold21Value = $scope.matrixList[3].TransQuant;
            //$scope.convertGold.gold18Value = $scope.matrixList[4].TransQuant;

        }
    }

    ///////
    $scope.getGoldObjectsByItems = function () {
        $scope.getGoldAccountOfDetails();

        for (var i = 0; i < $scope.goldAccountsList.length; i++) {
            $scope.itemObject = {};
            if ($scope.goldAccountsList[i].AccId != undefined && $scope.goldAccountsList[i].AccId != null) {
                $scope.itemObject.accountID = $scope.goldAccountsList[i].AccId;


                $scope.checkAccountTypes($scope.bill_type_Id, 'X')
                $scope.itemObject.accountType = $scope.typeName;

                if ($scope.bill_type_Id == 2 || $scope.bill_type_Id == 23 || $scope.bill_type_Id == 24) {
                    $scope.checkAccountTypes($scope.bill_type_Id, 'X')
                    $scope.itemObject.accountType = $scope.typeName;

                    $scope.itemObject.moneyValue = 0;
                    $scope.itemObject.gold24Value = $scope.goldAccountsList[i].gold24;
                    $scope.itemObject.gold22Value = $scope.goldAccountsList[i].gold22;
                    $scope.itemObject.gold21Value = $scope.goldAccountsList[i].gold21;
                    $scope.itemObject.gold18Value = $scope.goldAccountsList[i].gold18;
                }

                else if ($scope.bill_type_Id == 1) {

                    $scope.checkAccountTypes($scope.bill_type_Id, 'X')
                    $scope.itemObject.accountType = $scope.typeName;
                    $scope.itemObject.moneyValue = 0;
                    $scope.itemObject.gold24Value = $scope.goldAccountsList[i].gold24;
                    $scope.itemObject.gold22Value = $scope.goldAccountsList[i].gold22;
                    $scope.itemObject.gold21Value = $scope.goldAccountsList[i].gold21;
                    $scope.itemObject.gold18Value = $scope.goldAccountsList[i].gold18;
                }

                else if ($scope.bill_type_Id == 19 || $scope.bill_type_Id == 20) {
                    $scope.itemObject.moneyValue = 0;
                    $scope.itemObject.gold24Value = $scope.goldAccountsList[i].gold24;
                    $scope.itemObject.gold22Value = $scope.goldAccountsList[i].gold22;
                    $scope.itemObject.gold21Value = $scope.goldAccountsList[i].gold21;
                    $scope.itemObject.gold18Value = $scope.goldAccountsList[i].gold18;
                }
                else {
                    $scope.itemObject.moneyValue = 0;
                    $scope.itemObject.gold24Value = $scope.goldAccountsList[i].gold24;
                    $scope.itemObject.gold22Value = $scope.goldAccountsList[i].gold22;
                    $scope.itemObject.gold21Value = $scope.goldAccountsList[i].gold21;
                    $scope.itemObject.gold18Value = $scope.goldAccountsList[i].gold18;

                }
                $scope.billMaster.accounts.push($scope.itemObject);

                //$scope.convertGold.gold24Value = $scope.matrixList[1].TransQuant;
                //$scope.convertGold.gold22Value = $scope.matrixList[2].TransQuant;
                //$scope.convertGold.gold21Value = $scope.matrixList[3].TransQuant;
                //$scope.convertGold.gold18Value = $scope.matrixList[4].TransQuant;

            }
        }

    }

    //////////gold object of every details
    $scope.getGoldAccountOfDetails = function () {

        $scope.goldAccountsList = [];
        //var goldAcoountObject = { AccId: 0, gold24: 0, gold22: 0, gold21: 0, gold18: 0 };
        for (var i = 0; i < $scope.selectedItems.length; i++) {
            if ($scope.selectedItems[i].ITEM_CODE != null && $scope.selectedItems[i].ITEM_CODE != "" && $scope.selectedItems[i].ITEM_CODE != undefined) {

                if ($scope.selectedItems[i].GoldAccID != null && $scope.selectedItems[i].GoldAccID != undefined && $scope.selectedItems[i].GoldAccID != 0) {
                    var exist = $filter("filter")($scope.goldAccountsList, { 'AccId': $scope.selectedItems[i].GoldAccID });
                    if (exist.length > 0) {
                        var index = $scope.goldAccountsList.findIndex(x => x.AccId === $scope.selectedItems[i].GoldAccID);
                        if ($scope.selectedItems[i].ARName == "عيار 24") {
                            $scope.goldAccountsList[index].gold24 = parseFloat($scope.selectedItems[i].ItemWeight) + parseFloat($scope.goldAccountsList[index].gold24);
                        }

                        else if ($scope.selectedItems[i].ARName == "عيار 22") {
                            $scope.goldAccountsList[index].gold22 = parseFloat($scope.selectedItems[i].ItemWeight) + parseFloat($scope.goldAccountsList[index].gold22);
                        }

                        else if ($scope.selectedItems[i].ARName == "عيار 21") {
                            $scope.goldAccountsList[index].gold21 = parseFloat($scope.selectedItems[i].ItemWeight) + parseFloat($scope.goldAccountsList[index].gold21);
                        }

                        else if ($scope.selectedItems[i].ARName == "عيار 18") {
                            $scope.goldAccountsList[index].gold18 = parseFloat($scope.selectedItems[i].ItemWeight) + parseFloat($scope.goldAccountsList[index].gold18);
                        }

                    }

                    else {

                        var goldAcoountObject = { AccId: 0, gold24: 0, gold22: 0, gold21: 0, gold18: 0 };
                        goldAcoountObject.AccId = $scope.selectedItems[i].GoldAccID;

                        if ($scope.selectedItems[i].ARName == "عيار 24") {
                            goldAcoountObject.gold24 = parseFloat($scope.selectedItems[i].ItemWeight);
                        }

                        else if ($scope.selectedItems[i].ARName == "عيار 22") {
                            goldAcoountObject.gold22 = parseFloat($scope.selectedItems[i].ItemWeight);
                        }

                        else if ($scope.selectedItems[i].ARName == "عيار 21") {
                            goldAcoountObject.gold21 = parseFloat($scope.selectedItems[i].ItemWeight);
                        }

                        else if ($scope.selectedItems[i].ARName == "عيار 18") {
                            goldAcoountObject.gold18 = parseFloat($scope.selectedItems[i].ItemWeight);
                        }
                        $scope.goldAccountsList.push(goldAcoountObject);
                    }

                }

                //gold account of item is empty and get master gold account
                else {
                    var exist = $filter("filter")($scope.goldAccountsList, { 'AccId': $scope.billMaster.GOLD_ACC_ID });
                    if (exist.length > 0) {
                        var index = $scope.goldAccountsList.findIndex(x => x.AccId === $scope.billMaster.GOLD_ACC_ID);
                        if ($scope.selectedItems[i].ARName == "عيار 24") {
                            $scope.goldAccountsList[index].gold24 = parseFloat($scope.selectedItems[i].ItemWeight) + parseFloat($scope.goldAccountsList[index].gold24);
                        }

                        else if ($scope.selectedItems[i].ARName == "عيار 22") {
                            $scope.goldAccountsList[index].gold22 = parseFloat($scope.selectedItems[i].ItemWeight) + parseFloat($scope.goldAccountsList[index].gold22);
                        }

                        else if ($scope.selectedItems[i].ARName == "عيار 21") {
                            $scope.goldAccountsList[index].gold21 = parseFloat($scope.selectedItems[i].ItemWeight) + parseFloat($scope.goldAccountsList[index].gold21);
                        }

                        else if ($scope.selectedItems[i].ARName == "عيار 18") {
                            $scope.goldAccountsList[index].gold18 = parseFloat($scope.selectedItems[i].ItemWeight) + parseFloat($scope.goldAccountsList[index].gold18);
                        }

                    }

                    else {

                        var goldAcoountObject = { AccId: 0, gold24: 0, gold22: 0, gold21: 0, gold18: 0 };
                        goldAcoountObject.AccId = $scope.billMaster.GOLD_ACC_ID;

                        if ($scope.selectedItems[i].ARName == "عيار 24") {
                            goldAcoountObject.gold24 = parseFloat($scope.selectedItems[i].ItemWeight);
                        }

                        else if ($scope.selectedItems[i].ARName == "عيار 22") {
                            goldAcoountObject.gold22 = parseFloat($scope.selectedItems[i].ItemWeight);
                        }

                        else if ($scope.selectedItems[i].ARName == "عيار 21") {
                            goldAcoountObject.gold21 = parseFloat($scope.selectedItems[i].ItemWeight);
                        }

                        else if ($scope.selectedItems[i].ARName == "عيار 18") {
                            goldAcoountObject.gold18 = parseFloat($scope.selectedItems[i].ItemWeight);
                        }
                        $scope.goldAccountsList.push(goldAcoountObject);
                    }
                }
            }
        }
    }

    ////////////////////////////get pay box account object to bill master
    $scope.getPayBoxObject = function () {

        $scope.payBoxObject = {};
        if ($scope.billMaster.PAY_ACC_ID != undefined && $scope.billMaster.PAY_ACC_ID != null) {
            $scope.payBoxObject.accountID = $scope.billMaster.PAY_ACC_ID;

            $scope.checkAccountTypes($scope.bill_type_Id, 'X')
            $scope.payBoxObject.accountType = $scope.typeName;
            $scope.payBoxObject.moneyValue = $scope.billMaster.TotalAfterTax;
            $scope.payBoxObject.gold24Value = 0;
            $scope.payBoxObject.gold22Value = 0;
            $scope.payBoxObject.gold21Value = 0;
            $scope.payBoxObject.gold18Value = 0;
            $scope.billMaster.accounts.push($scope.payBoxObject);

        }
    }

    ///////////////////////////////////get all accounts of paytypes
    $scope.getPayTypesAccountObjects = function () {

        var totalBankcomm = 0;
        var totalCommTax = 0;
        var bankCommAccountId;
        var commTaxAccountId;

        bankCommAccountId = $scope.settingsAccounts.AccCommissionID;
        commTaxAccountId = $scope.settingsAccounts.AccCommissionTaxID;

        for (var i = 0; i < $scope.payTypesList.length; i++) {
            if ($scope.payTypesList[i].AccountID != undefined && $scope.payTypesList[i].AccountID != null && $scope.payTypesList[i].PayTypeValue != null && $scope.payTypesList[i].PayTypeValue != undefined && $scope.payTypesList[i].PayTypeValue != 0) {
                var bankComm = 0;
                var commTax = 0;
                if ($scope.payTypesList[i].BankCommissionValue != undefined && $scope.payTypesList[i].BankCommissionValue != 0) {
                    bankComm = $scope.payTypesList[i].BankCommissionValue;
                    totalBankcomm = (parseFloat(bankComm) + parseFloat(totalBankcomm)).toFixed(2);
                }

                if ($scope.payTypesList[i].CommissionTaxValue != undefined && $scope.payTypesList[i].CommissionTaxValue != 0) {
                    commTax = $scope.payTypesList[i].CommissionTaxValue;
                    totalCommTax = (parseFloat(commTax) + parseFloat(totalCommTax)).toFixed(2);
                }
                $scope.billAcoounts($scope.payTypesList[i].AccountID, $scope.payTypesList[i].PayTypeValue, bankComm, commTax);
            }
        }
        $scope.commAndTaxAcoounts(bankCommAccountId, totalBankcomm);
        $scope.commAndTaxAcoounts(commTaxAccountId, totalCommTax);

    }

    //add commission and tax acc
    $scope.commAndTaxAcoounts = function (accountID, value) {

        $scope.commTaxObject = {};
        $scope.commTaxObject.accountID = accountID;


        if ($scope.bill_type_Id == 2 || $scope.bill_type_Id == 20) {
            $scope.commTaxObject.accountType = 'D';

            $scope.commTaxObject.moneyValue = value;
            $scope.commTaxObject.gold24Value = 0;
            $scope.commTaxObject.gold22Value = 0;
            $scope.commTaxObject.gold21Value = 0;
            $scope.commTaxObject.gold18Value = 0;
        }

        else if ($scope.bill_type_Id == 1) {
            $scope.checkAccountTypes($scope.bill_type_Id, 'S');
            $scope.payObject.accountType = $scope.typeName;
            $scope.commTaxObject.moneyValue = value;
            $scope.commTaxObject.gold24Value = 0;
            $scope.commTaxObject.gold22Value = 0;
            $scope.commTaxObject.gold21Value = 0;
            $scope.commTaxObject.gold18Value = 0;
        }

        $scope.billMaster.accounts.push($scope.commTaxObject);

    }

    ////////add pay types to accounts 
    $scope.billAcoounts = function (accountID, value, bankCommValue, commTaxValue) {
        //$scope.getAccountByID(accountID).then(function (result) {
        $scope.payObject = {};
        $scope.payObject.accountID = accountID;
        //    var type = result.data;

        if ($scope.bill_type_Id == 2) {
            $scope.checkAccountTypes($scope.bill_type_Id, 'S');
            $scope.payObject.accountType = $scope.typeName;
            var money = (parseFloat(value) - parseFloat(bankCommValue) - parseFloat(commTaxValue)).toFixed(2);
            $scope.payObject.moneyValue = money;
            $scope.payObject.gold24Value = 0;
            $scope.payObject.gold22Value = 0;
            $scope.payObject.gold21Value = 0;
            $scope.payObject.gold18Value = 0;

        }


        else if ($scope.bill_type_Id == 1) {
            $scope.checkAccountTypes($scope.bill_type_Id, 'S');
            $scope.payObject.accountType = $scope.typeName;
            var money = parseFloat(value) - parseFloat(bankCommValue) - parseFloat(commTaxValue);
            $scope.payObject.moneyValue = money;
            $scope.payObject.gold24Value = 0;
            $scope.payObject.gold22Value = 0;
            $scope.payObject.gold21Value = 0;
            $scope.payObject.gold18Value = 0;

        }

        //edit 
        else if ($scope.bill_type_Id == 20) {
            $scope.checkAccountTypes($scope.bill_type_Id, 'D');
            $scope.payObject.accountType = $scope.typeName;
            var money = (parseFloat(value) - parseFloat(bankCommValue) - parseFloat(commTaxValue)).toFixed(2);
            $scope.payObject.moneyValue = money;
            $scope.payObject.gold24Value = 0;
            $scope.payObject.gold22Value = 0;
            $scope.payObject.gold21Value = 0;
            $scope.payObject.gold18Value = 0;


            $scope.custObject = {};
            $scope.custObject.accountID = $scope.billMaster.CUST_ACC_ID;
            $scope.custObject.accountType = 'C';
            $scope.custObject.moneyValue = parseFloat(value).toFixed(2);
            $scope.custObject.gold24Value = 0;
            $scope.custObject.gold22Value = 0;
            $scope.custObject.gold21Value = 0;
            $scope.custObject.gold18Value = 0;
            $scope.billMaster.accounts.push($scope.custObject);
        }


        else {
            $scope.checkAccountTypes($scope.bill_type_Id, 'X');
            $scope.payObject.accountType = $scope.typeName;
            $scope.payObject.moneyValue = value;
            $scope.payObject.gold24Value = 0;
            $scope.payObject.gold22Value = 0;
            $scope.payObject.gold21Value = 0;
            $scope.payObject.gold18Value = 0;
        }
        $scope.billMaster.accounts.push($scope.payObject);
        // });
    }

    $scope.getAccountByID = function (accId) {

        var type = accountService.getByID(accId);
        return type;
    }

    $scope.checkAccountTypes = function (type, acc) {
        var typeName = "";
        if (type == 2 || type == 13 || type == 17 || type == 4 || type == 6 || type == 23 || type == 24) {
            if (acc == 'S') {
                $scope.typeName = 'D';
            }
            else { $scope.typeName = 'C'; }
        }
        else if (type == 1 || type == 3 || type == 5 || type == 14 || type == 18 || type == 19 || type == 20) {
            if (acc == 'S') {
                $scope.typeName = 'C';
            }
            else { $scope.typeName = 'D' }
        }
    }

    ////////////////get accounts from bill settings and add them in account object
    //discount account
    $scope.getDiscAccFromSettings = function () {
        //   debugger;

        if ($scope.settingsAccounts.ACC_DISC_ID != undefined && $scope.settingsAccounts.ACC_DISC_ID != null) {
            $scope.discObject = {};
            $scope.discObject.accountID = $scope.settingsAccounts.ACC_DISC_ID;

            $scope.checkAccountTypes($scope.bill_type_Id, 'X')
            $scope.discObject.accountType = $scope.typeName;
            $scope.discObject.moneyValue = $scope.billMaster.TotalAllDisc;
            $scope.discObject.gold24Value = 0;
            $scope.discObject.gold22Value = 0;
            $scope.discObject.gold21Value = 0;
            $scope.discObject.gold18Value = 0;
            $scope.billMaster.accounts.push($scope.discObject);
        }
    }

    //store account
    $scope.getStoreAccFromSettings = function () {

        if ($scope.settingsAccounts.ACC_STORE_ID != undefined && $scope.settingsAccounts.ACC_STORE_ID != null) {
            $scope.storeObject = {};
            $scope.storeObject.accountID = $scope.settingsAccounts.ACC_STORE_ID;

            $scope.checkAccountTypes($scope.bill_type_Id, 'X')
            $scope.storeObject.accountType = $scope.typeName;
            $scope.storeObject.moneyValue = 0;
            $scope.storeObject.gold24Value = 0;
            $scope.storeObject.gold22Value = 0;
            $scope.storeObject.gold21Value = 0;
            $scope.storeObject.gold18Value = 0;
            $scope.billMaster.accounts.push($scope.storeObject);
        }
    }

    //pay account 
    $scope.getPayAccFromSettings = function () {

        if ($scope.settingsAccounts.ACC_PAY_ID != undefined && $scope.settingsAccounts.ACC_PAY_ID != null) {
            $scope.payObject = {};
            $scope.payObject.accountID = $scope.settingsAccounts.ACC_PAY_ID;

            $scope.checkAccountTypes($scope.bill_type_Id, 'X')
            $scope.payObject.accountType = $scope.typeName;
            $scope.payObject.moneyValue = $scope.billMaster.TotalPrice.toFixed(2);
            $scope.payObject.gold24Value = 0;
            $scope.payObject.gold22Value = 0;
            $scope.payObject.gold21Value = 0;
            $scope.payObject.gold18Value = 0;
            $scope.billMaster.accounts.push($scope.payObject);
        }
    }

    //wages acc
    $scope.getWagesAccFromSettings = function () {

        if ($scope.settingsAccounts.AccWagesID != undefined && $scope.settingsAccounts.AccWagesID != null) {
            $scope.wagesObject = {};
            $scope.wagesObject.accountID = $scope.settingsAccounts.AccWagesID;

            if ($scope.bill_type_Id == 23 || $scope.bill_type_Id == 24) {
                $scope.wagesObject.accountType = 'C';
            }
            else if ($scope.bill_type_Id == 19 || $scope.bill_type_Id == 20) {
                $scope.wagesObject.accountType = 'D';
            }
            else {
                $scope.checkAccountTypes($scope.bill_type_Id, 'X')
                $scope.wagesObject.accountType = $scope.typeName;
            }

            $scope.wagesObject.moneyValue = $scope.billMaster.TotalManufactWages;// - $scope.wagesDiscountVal;
            $scope.wagesObject.gold24Value = 0;
            $scope.wagesObject.gold22Value = 0;
            $scope.wagesObject.gold21Value = 0;
            $scope.wagesObject.gold18Value = 0;
            $scope.billMaster.accounts.push($scope.wagesObject);

        }

    }

    ///gift acc
    $scope.getGiftAccFromSettings = function () {

        if ($scope.settingsAccounts.ACC_GIFT_ID != undefined && $scope.settingsAccounts.ACC_GIFT_ID != null) {
            $scope.giftObject = {};
            $scope.giftObject.accountID = $scope.settingsAccounts.ACC_GIFT_ID;

            $scope.checkAccountTypes($scope.bill_type_Id, 'X')
            $scope.giftObject.accountType = $scope.typeName;
            $scope.giftObject.moneyValue = 0;
            $scope.giftObject.gold24Value = 0;
            $scope.giftObject.gold22Value = 0;
            $scope.giftObject.gold21Value = 0;
            $scope.giftObject.gold18Value = 0;
            $scope.billMaster.accounts.push($scope.giftObject);

        }

    }

    ///////cost acc
    $scope.getCostAccFromSettings = function () {

        if ($scope.settingsAccounts.ACC_COST_ID != undefined && $scope.settingsAccounts.ACC_COST_ID != null) {
            $scope.costObject = {};
            $scope.costObject.accountID = $scope.settingsAccounts.ACC_COST_ID;

            $scope.checkAccountTypes($scope.bill_type_Id, 'X')
            $scope.costObject.accountType = $scope.typeName;
            $scope.costObject.moneyValue = 0;
            $scope.costObject.gold24Value = 0;
            $scope.costObject.gold22Value = 0;
            $scope.costObject.gold21Value = 0;
            $scope.costObject.gold18Value = 0;
            $scope.billMaster.accounts.push($scope.costObject);

        }

    }

    ///////extra acc
    $scope.getExtraAccFromSettings = function () {

        if ($scope.settingsAccounts.ACC_EXTRA_ID != null && $scope.settingsAccounts.ACC_EXTRA_ID != undefined) {
            $scope.extraObject = {};
            $scope.extraObject.accountID = $scope.settingsAccounts.ACC_EXTRA_ID;

            $scope.checkAccountTypes($scope.bill_type_Id, 'X')
            $scope.extraObject.accountType = $scope.typeName;
            $scope.extraObject.moneyValue = 0;
            $scope.extraObject.gold24Value = 0;
            $scope.extraObject.gold22Value = 0;
            $scope.extraObject.gold21Value = 0;
            $scope.extraObject.gold18Value = 0;
            $scope.billMaster.accounts.push($scope.extraObject);

        }

    }

    //////subject to vat acc
    $scope.getVatRateAccFromsettings = function () {
        //  debugger;
        if ($scope.billMaster.TotalVatRate != null && $scope.billMaster.TotalVatRate != 0 && $scope.billMaster.TotalVatRate != undefined) {

            if ($scope.settingsAccounts.AccVatRateID != null && $scope.settingsAccounts.AccVatRateID != undefined) {
                $scope.vatRateObject = {};
                $scope.vatRateObject.accountID = $scope.settingsAccounts.AccVatRateID;

                if ($scope.bill_type_Id == 2) {

                    $scope.vatRateObject.accountType = 'C';
                    $scope.vatRateObject.moneyValue = $scope.billMaster.TotalVatValue;
                    $scope.vatRateObject.gold24Value = 0;
                    $scope.vatRateObject.gold22Value = 0;
                    $scope.vatRateObject.gold21Value = 0;
                    $scope.vatRateObject.gold18Value = 0;
                    $scope.billMaster.accounts.push($scope.vatRateObject);
                }
                else if ($scope.bill_type_Id == 17) {

                    $scope.vatRateObject.accountType = 'C';
                    $scope.vatRateObject.moneyValue = parseFloat($scope.billMaster.TotalVatValue);
                    $scope.vatRateObject.gold24Value = 0;
                    $scope.vatRateObject.gold22Value = 0;
                    $scope.vatRateObject.gold21Value = 0;
                    $scope.vatRateObject.gold18Value = 0;
                    $scope.billMaster.accounts.push($scope.vatRateObject);
                }
                else {
                    $scope.checkAccountTypes($scope.bill_type_Id, 'X')
                    $scope.vatRateObject.accountType = $scope.typeName;
                    $scope.vatRateObject.moneyValue = 0;
                    $scope.vatRateObject.gold24Value = 0;
                    $scope.vatRateObject.gold22Value = 0;
                    $scope.vatRateObject.gold21Value = 0;
                    $scope.vatRateObject.gold18Value = 0;
                    $scope.billMaster.accounts.push($scope.vatRateObject);
                }

            }

        }
    }

    //wages tax acc
    $scope.getWagesTaxAccFromSettings = function () {

        if ($scope.settingsAccounts.AccWagesTaxID != undefined && $scope.settingsAccounts.AccWagesTaxID != null) {
            $scope.wagesTaxObject = {};
            $scope.wagesTaxObject.accountID = $scope.settingsAccounts.AccWagesTaxID;

            if ($scope.bill_type_Id == 23 || $scope.bill_type_Id == 24) {
                $scope.wagesTaxObject.accountType = 'C';
            }
            else if ($scope.bill_type_Id == 19 || $scope.bill_type_Id == 20) {
                $scope.wagesTaxObject.accountType = 'D';
            }
            else {
                $scope.checkAccountTypes($scope.bill_type_Id, 'X')
                $scope.wagesTaxObject.accountType = $scope.typeName;
            }

            $scope.wagesTaxObject.moneyValue = (parseFloat($scope.billMaster.manufacturingWagesMainVat) * parseFloat($scope.billMaster.MainVatRate) / 100).toFixed(2);
            $scope.wagesTaxObject.gold24Value = 0;
            $scope.wagesTaxObject.gold22Value = 0;
            $scope.wagesTaxObject.gold21Value = 0;
            $scope.wagesTaxObject.gold18Value = 0;
            $scope.billMaster.accounts.push($scope.wagesTaxObject);

        }

    }

    //PurchasAccID
    $scope.getPurchasAccFromSettings = function () {

        if ($scope.settingsAccounts.PurchasAccID != undefined && $scope.settingsAccounts.PurchasAccID != null) {
            $scope.purchasAccObject = {};
            $scope.purchasAccObject.accountID = $scope.settingsAccounts.PurchasAccID;

            if ($scope.bill_type_Id == 1) {
                $scope.purchasAccObject.accountType = 'D';

                $scope.purchasAccObject.moneyValue = (parseFloat($scope.billMaster.TotalMustPaid) - parseFloat($scope.billMaster.TotalVatValue)).toFixed(2);
                $scope.purchasAccObject.gold24Value = 0;
                $scope.purchasAccObject.gold22Value = 0;
                $scope.purchasAccObject.gold21Value = 0;
                $scope.purchasAccObject.gold18Value = 0;
                $scope.billMaster.accounts.push($scope.purchasAccObject);
            }

            else if ($scope.bill_type_Id == 20 || $scope.bill_type_Id == 24) {
                $scope.purchasAccObject.accountType = $scope.bill_type_Id == 20 ? 'D' : "C";

                $scope.purchasAccObject.moneyValue = (parseFloat($scope.billMaster.TotalPrice) - parseFloat($scope.billMaster.TotalAllDisc)).toFixed(2);
                $scope.purchasAccObject.gold24Value = 0;
                $scope.purchasAccObject.gold22Value = 0;
                $scope.purchasAccObject.gold21Value = 0;
                $scope.purchasAccObject.gold18Value = 0;
                $scope.billMaster.accounts.push($scope.purchasAccObject);
            }

            else {
                $scope.checkAccountTypes($scope.bill_type_Id, 'X');
                $scope.purchasAccObject.accountType = $scope.typeName;
                $scope.purchasAccObject.moneyValue = (parseFloat($scope.billMaster.TotalPrice) - parseFloat($scope.billMaster.TotalAllDisc)).toFixed(2);
                $scope.purchasAccObject.gold24Value = 0;
                $scope.purchasAccObject.gold22Value = 0;
                $scope.purchasAccObject.gold21Value = 0;
                $scope.purchasAccObject.gold18Value = 0;
                $scope.billMaster.accounts.push($scope.purchasAccObject);
            }
        }

    }

    //PurchasTaxAccID
    $scope.getPurchasTaxAccFromSettings = function () {
        $scope.purchasTaxAccObject = {};

        if ($scope.settingsAccounts.PurchasTaxAccID != undefined && $scope.settingsAccounts.PurchasTaxAccID != null) {
            $scope.purchasTaxAccObject.accountID = $scope.settingsAccounts.PurchasTaxAccID;

            if ($scope.bill_type_Id == 1) {
                $scope.purchasTaxAccObject.accountType = 'D';
                $scope.purchasTaxAccObject.moneyValue = $scope.billMaster.TotalVatValue;
                $scope.purchasTaxAccObject.gold24Value = 0;
                $scope.purchasTaxAccObject.gold22Value = 0;
                $scope.purchasTaxAccObject.gold21Value = 0;
                $scope.purchasTaxAccObject.gold18Value = 0;
                $scope.billMaster.accounts.push($scope.purchasTaxAccObject);
            }

            else if ($scope.bill_type_Id == 20 || $scope.bill_type_Id == 24) {
                $scope.purchasTaxAccObject.accountType = $scope.bill_type_Id == 20 ? 'D' : "C";
                $scope.purchasTaxAccObject.moneyValue = $scope.billMaster.TotalVatValue;
                $scope.purchasTaxAccObject.gold24Value = 0;
                $scope.purchasTaxAccObject.gold22Value = 0;
                $scope.purchasTaxAccObject.gold21Value = 0;
                $scope.purchasTaxAccObject.gold18Value = 0;
                $scope.billMaster.accounts.push($scope.purchasTaxAccObject);
            }

            else {
                $scope.checkAccountTypes($scope.bill_type_Id, 'X')
                $scope.purchasTaxAccObject.accountType = $scope.typeName;
                $scope.purchasTaxAccObject.moneyValue = $scope.billMaster.TotalVatValue;
                $scope.purchasTaxAccObject.gold24Value = 0;
                $scope.purchasTaxAccObject.gold22Value = 0;
                $scope.purchasTaxAccObject.gold21Value = 0;
                $scope.purchasTaxAccObject.gold18Value = 0;
                $scope.billMaster.accounts.push($scope.purchasTaxAccObject);
            }
        }

    }

    // Purchas Gold
    $scope.getPurchasGoldObject = function (typeVal) {

        if ($scope.settingsAccounts.PurchasAccID != undefined && $scope.settingsAccounts.PurchasAccID != null) {
            $scope.purchasGoldObject = {};
            $scope.purchasGoldObject.accountID = $scope.settingsAccounts.PurchasAccID;

            $scope.purchasGoldObject.accountType = typeVal;
            $scope.purchasGoldObject.moneyValue = 0;
            $scope.purchasGoldObject.gold24Value = $scope.total24;
            $scope.purchasGoldObject.gold22Value = $scope.total22;
            $scope.purchasGoldObject.gold21Value = $scope.total21;
            $scope.purchasGoldObject.gold18Value = $scope.total18;
            $scope.billMaster.accounts.push($scope.purchasGoldObject);

        }
    }

    //ACC_DISC_ID
    $scope.getDiscountAccFromSettings = function () {

        if ($scope.settingsAccounts.ACC_DISC_ID != undefined && $scope.settingsAccounts.ACC_DISC_ID != null) {
            $scope.discountAccObject = {};
            $scope.discountAccObject.accountID = $scope.settingsAccounts.ACC_DISC_ID;

            $scope.checkAccountTypes($scope.bill_type_Id, 'X');
            $scope.discountAccObject.accountType = $scope.typeName;
            $scope.discountAccObject.moneyValue = $scope.billMaster.TotalAllDisc;
            $scope.discountAccObject.gold24Value = 0;
            $scope.discountAccObject.gold22Value = 0;
            $scope.discountAccObject.gold21Value = 0;
            $scope.discountAccObject.gold18Value = 0;
            $scope.billMaster.accounts.push($scope.discountAccObject);

        }

    }

    // Sales Gold
    $scope.getSalesGoldObject = function () {

        if ($scope.settingsAccounts.AccSalesID != undefined && $scope.settingsAccounts.AccSalesID != null) {
            $scope.salesGoldObject = {};
            $scope.salesGoldObject.accountID = $scope.settingsAccounts.AccSalesID;
            $scope.salesGoldObject.accountType = 'D';
            $scope.salesGoldObject.moneyValue = 0;
            $scope.salesGoldObject.gold24Value = $scope.total24;
            $scope.salesGoldObject.gold22Value = $scope.total22;
            $scope.salesGoldObject.gold21Value = $scope.total21;
            $scope.salesGoldObject.gold18Value = $scope.total18;
            $scope.billMaster.accounts.push($scope.salesGoldObject);

        }
    }

    //SalesAccID
    $scope.getSalesAccFromSettings = function () {

        if ($scope.settingsAccounts.AccSalesID != undefined && $scope.settingsAccounts.AccSalesID != null) {
            $scope.salesAccObject = {};
            $scope.salesAccObject.accountID = $scope.settingsAccounts.AccSalesID;

            $scope.checkAccountTypes($scope.bill_type_Id, 'X')
            $scope.salesAccObject.accountType = $scope.typeName;
            $scope.salesAccObject.moneyValue = parseFloat($scope.billMaster.TotalPrice) - parseFloat($scope.billMaster.TotalAllDisc);
            $scope.salesAccObject.gold24Value = 0;
            $scope.salesAccObject.gold22Value = 0;
            $scope.salesAccObject.gold21Value = 0;
            $scope.salesAccObject.gold18Value = 0;
            $scope.billMaster.accounts.push($scope.salesAccObject);
        }

    }

    //accSalesID of bill setting
    $scope.getAccSalesFromSettings = function () {

        if ($scope.settingsAccounts.AccSalesID != undefined && $scope.settingsAccounts.AccSalesID != null) {
            $scope.AccSalesObject = {};
            $scope.AccSalesObject.accountID = $scope.settingsAccounts.AccSalesID;
            $scope.AccSalesObject.accountType = 'C';

            $scope.AccSalesObject.moneyValue = parseFloat($scope.billMaster.TotalMustPaid) - parseFloat($scope.billMaster.TotalVatValue);
            $scope.AccSalesObject.gold24Value = 0;
            $scope.AccSalesObject.gold22Value = 0;
            $scope.AccSalesObject.gold21Value = 0;
            $scope.AccSalesObject.gold18Value = 0;
            $scope.billMaster.accounts.push($scope.AccSalesObject);

        }
    }

    //accSalesID of bill setting
    $scope.getAccSalesMoneyFromSettings = function () {

        if ($scope.settingsAccounts.AccSalesID != undefined && $scope.settingsAccounts.AccSalesID != null) {
            $scope.AccSalesObject = {};
            $scope.AccSalesObject.accountID = $scope.settingsAccounts.AccSalesID;

            $scope.AccSalesObject.accountType = 'C';

            $scope.AccSalesObject.moneyValue = $scope.billMaster.TotalRemaining;
            $scope.AccSalesObject.gold24Value = 0;
            $scope.AccSalesObject.gold22Value = 0;
            $scope.AccSalesObject.gold21Value = 0;
            $scope.AccSalesObject.gold18Value = 0;
            $scope.billMaster.accounts.push($scope.AccSalesObject);

        }
    }

    $scope.getSalesAccFromSettingsForPayTypes = function () {
        if ($scope.settingsAccounts.AccSalesID != undefined && $scope.settingsAccounts.AccSalesID != null) {
            $scope.payAccObject = {};
            $scope.payAccObject.accountID = $scope.settingsAccounts.AccSalesID;

            //var total = 0;
            //for (var i = 0; i < $scope.payTypesList.length; i++) {
            //    total = parseFloat($scope.payTypesList[i].PayTypeValue) + parseFloat(total);
            //}
            $scope.payAccObject.accountType = 'C';
            $scope.payAccObject.moneyValue = $scope.billMaster.TotalBeforeTax;
            $scope.payAccObject.gold24Value = 0;
            $scope.payAccObject.gold22Value = 0;
            $scope.payAccObject.gold21Value = 0;
            $scope.payAccObject.gold18Value = 0;
            $scope.billMaster.accounts.push($scope.payAccObject);

        }
    }

    $scope.getPurchaseAccFromSettingsForPayTypes = function () {
        if ($scope.settingsAccounts.AccSalesID != undefined && $scope.settingsAccounts.AccSalesID != null) {
            $scope.purchasePayObject = {};
            $scope.purchasePayObject.accountID = $scope.settingsAccounts.AccSalesID;

            //var total = 0;
            //for (var i = 0; i < $scope.payTypesList.length; i++) {
            //    total = parseFloat($scope.payTypesList[i].PayTypeValue) + parseFloat(total);
            //}
            $scope.checkAccountTypes($scope.bill_type_Id, 'S')
            $scope.AccSalesObject.accountType = $scope.typeName;

            $scope.purchasePayObject.moneyValue = $scope.billMaster.TotalPrice.toFixed(2);
            $scope.purchasePayObject.gold24Value = 0;
            $scope.purchasePayObject.gold22Value = 0;
            $scope.purchasePayObject.gold21Value = 0;
            $scope.purchasePayObject.gold18Value = 0;
            $scope.billMaster.accounts.push($scope.purchasePayObject);

        }

    }

    //AccPurchaseGoldID
    $scope.getPurchasGoldAccFromSettings = function () {

        if ($scope.settingsAccounts.AccPurchaseGoldID != undefined && $scope.settingsAccounts.AccPurchaseGoldID != null) {
            $scope.purchasGoldAccObject = {};
            $scope.purchasGoldAccObject.accountID = $scope.settingsAccounts.AccPurchaseGoldID;

            if ($scope.bill_type_Id == 1) {
                $scope.purchasGoldAccObject.accountType = 'C';

                $scope.purchasGoldAccObject.moneyValue = 0;
                $scope.purchasGoldAccObject.gold24Value = $scope.total24;
                $scope.purchasGoldAccObject.gold22Value = $scope.total22;
                $scope.purchasGoldAccObject.gold21Value = $scope.total21;
                $scope.purchasGoldAccObject.gold18Value = $scope.total18;
                $scope.billMaster.accounts.push($scope.purchasGoldAccObject);
            }
            else if ($scope.bill_type_Id == 18) {

                $scope.checkAccountTypes($scope.bill_type_Id, 'X');
                $scope.purchasGoldAccObject.accountType = $scope.typeName;
                $scope.purchasGoldAccObject.moneyValue = $scope.billMaster.TotalPrice - $scope.billMaster.TotalAllDisc;
                $scope.purchasGoldAccObject.gold24Value = 0;
                $scope.purchasGoldAccObject.gold22Value = 0;
                $scope.purchasGoldAccObject.gold21Value = 0;
                $scope.purchasGoldAccObject.gold18Value = 0;
                $scope.billMaster.accounts.push($scope.purchasGoldAccObject);
            }
            else if ($scope.bill_type_Id == 24) {

                $scope.purchasGoldAccObject.accountType = 'D';

                $scope.purchasGoldAccObject.moneyValue = 0;
                $scope.purchasGoldAccObject.gold24Value = $scope.total24;
                $scope.purchasGoldAccObject.gold22Value = $scope.total22;
                $scope.purchasGoldAccObject.gold21Value = $scope.total21;
                $scope.purchasGoldAccObject.gold18Value = $scope.total18;
                $scope.billMaster.accounts.push($scope.purchasGoldAccObject);
            }

            else if ($scope.bill_type_Id == 20) {

                $scope.purchasGoldAccObject.accountType = 'C';
                $scope.purchasGoldAccObject.moneyValue = 0;
                $scope.purchasGoldAccObject.gold24Value = $scope.total24;
                $scope.purchasGoldAccObject.gold22Value = $scope.total22;
                $scope.purchasGoldAccObject.gold21Value = $scope.total21;
                $scope.purchasGoldAccObject.gold18Value = $scope.total18;
                $scope.billMaster.accounts.push($scope.purchasGoldAccObject);
            }

        }

    }

    //AccSalesGoldID
    $scope.getSalesGoldAccFromSettings = function () {

        if ($scope.settingsAccounts.AccSalesGoldID != undefined && $scope.settingsAccounts.AccSalesGoldID != null) {
            $scope.purchasGoldAccObject = {};
            $scope.purchasGoldAccObject.accountID = $scope.settingsAccounts.AccSalesGoldID;

            if ($scope.bill_type_Id == 2 || $scope.bill_type_Id == 17) {
                $scope.purchasGoldAccObject.accountType = 'D';

                $scope.purchasGoldAccObject.moneyValue = 0;
                $scope.purchasGoldAccObject.gold24Value = $scope.total24;
                $scope.purchasGoldAccObject.gold22Value = $scope.total22;
                $scope.purchasGoldAccObject.gold21Value = $scope.total21;
                $scope.purchasGoldAccObject.gold18Value = $scope.total18;
                $scope.billMaster.accounts.push($scope.purchasGoldAccObject);
            }
            //else if ($scope.bill_type_Id == 17) {

            //    $scope.checkAccountTypes($scope.bill_type_Id, 'X');
            //    $scope.purchasGoldAccObject.accountType = 'C';
            //    $scope.purchasGoldAccObject.moneyValue = 0;
            //    $scope.purchasGoldAccObject.gold24Value = $scope.total24;
            //    $scope.purchasGoldAccObject.gold22Value = $scope.total22;
            //    $scope.purchasGoldAccObject.gold21Value = $scope.total21;
            //    $scope.purchasGoldAccObject.gold18Value = $scope.total18;
            //    $scope.billMaster.accounts.push($scope.purchasGoldAccObject);
            //}

        }

    }

    //////////////////////////////save point of sale entry ////////////////////////////////////////////////////
    $scope.saveEntry = function () {

        //if (localStorageService.get('branchId') != null) {
        //    $scope.billMaster.COM_BRN_ID = localStorageService.get('branchId').branchId;
        //}
        
        if ($scope.bill_type_Id == 13 || $scope.bill_type_Id == 14) {

            $scope.billMaster.accounts = jQuery.grep($scope.billMaster.accounts, function (n, i) {
                return (n.accountID === $scope.billMaster.GOLD_ACC_ID || n.accountID === $scope.billMaster.CUST_ACC_ID);
            });
        }

        console.log($scope.billMaster.accounts);

        ////////////get total credit and depit for entry master
        for (var i = 0; i < $scope.billMaster.accounts.length; i++) {
            if ($scope.billMaster.accounts[i].accountType == 'C') {
                $scope.totalEntryCredit = parseFloat($scope.billMaster.accounts[i].moneyValue) + parseFloat($scope.totalEntryCredit);
            }
            else if ($scope.billMaster.accounts[i].accountType == 'D') {
                $scope.totalEntryDepit = parseFloat($scope.billMaster.accounts[i].moneyValue) + parseFloat($scope.totalEntryDepit);
            }
        }
        $scope.entryMaster = {};
        if (localStorageService.get('branchId') != null) {
            $scope.entryMaster.COM_BRN_ID = localStorageService.get('branchId').branchId;
        }
        $scope.entryMaster.ENTRY_SETTING_ID = 130;
        $scope.entryMaster.ENTRY_DATE = $scope.billMaster.BILL_DATE;
        $scope.entryMaster.ENTRY_CREDIT = $scope.totalEntryCredit;
        $scope.entryMaster.ENTRY_DEBIT = $scope.totalEntryDepit;
        $scope.entryMaster.ACC_ID = $scope.billMaster.PAY_ACC_ID;
        if ($scope.billMaster.CURRENCY_ID != undefined && $scope.billMaster.CURRENCY_ID != null) {
            $scope.entryMaster.CURRENCY_ID = $scope.billMaster.CURRENCY_ID;
        }
        else {
            $scope.entryMaster.CURRENCY_ID = 1;
        }

        if ($scope.billMaster.CURRENCY_RATE != undefined && $scope.billMaster.CURRENCY_RATE != null) {
            $scope.entryMaster.CURRENCY_RATE = $scope.billMaster.CURRENCY_RATE;
        }
        else {
            $scope.billMaster.CURRENCY_RATE = 1;
        }
        $scope.entryMaster.BILL_ID = $scope.billMaster.BILL_ID;
        $scope.entryMaster.EMP_ID = $scope.billMaster.EMP_ID;
        $scope.entryMaster.ENTRY_NUMBER = $scope.ENTRY_NUMBER;

        //if ($scope.operation != "Insert") {
        //    $scope.entryMaster.ENTRY_ID = $scope.billMaster.ENTRY_ID;
        //    $scope.entryMaster.BILL_ID = $scope.billID;
        //}

        ////////fill entry details
        $scope.entryDetails = [];

        for (var i = 0; i < $scope.billMaster.accounts.length; i++) {
            $scope.entryDetObj = {};
            $scope.entryDetObj.ENTRY_ROW_NUMBER = i;

            $scope.entryDetObj.ACC_ID = $scope.billMaster.accounts[i].accountID;

            if ($scope.billMaster.accounts[i].accountType == 'C') {
                $scope.entryDetObj.ENTRY_CREDIT = $scope.billMaster.accounts[i].moneyValue == undefined ||
                    $scope.billMaster.accounts[i].moneyValue == null
                    ? 0
                    : $scope.billMaster.accounts[i].moneyValue;

                $scope.entryDetObj.ENTRY_GOLD24_CREDIT = $scope.billMaster.accounts[i].gold24Value;
                $scope.entryDetObj.ENTRY_GOLD22_CREDIT = $scope.billMaster.accounts[i].gold22Value;
                $scope.entryDetObj.ENTRY_GOLD21_CREDIT = $scope.billMaster.accounts[i].gold21Value;
                $scope.entryDetObj.ENTRY_GOLD18_CREDIT = $scope.billMaster.accounts[i].gold18Value;

                //if ($scope.bill_type_Id == 18) {
                //    $scope.entryDetObj.ENTRY_CREDIT = 0;
                //}

                //Debit
                $scope.entryDetObj.ENTRY_GOLD24_DEBIT = 0;
                $scope.entryDetObj.ENTRY_GOLD22_DEBIT = 0;
                $scope.entryDetObj.ENTRY_GOLD21_DEBIT = 0;
                $scope.entryDetObj.ENTRY_GOLD18_DEBIT = 0;

                if ($scope.bill_type_Id == 14) {

                    var gold_rec = jQuery.grep($scope.billMaster.accounts, function (n, i) {
                        return (n.accountType === 'D' && n.accountID === $scope.billMaster.GOLD_ACC_ID);
                    });

                    $scope.entryDetObj.ENTRY_CREDIT = 0;
                    $scope.entryDetObj.ENTRY_GOLD24_CREDIT = gold_rec[0].gold24Value;
                    $scope.entryDetObj.ENTRY_GOLD22_CREDIT = gold_rec[0].gold22Value;
                    $scope.entryDetObj.ENTRY_GOLD21_CREDIT = gold_rec[0].gold21Value;
                    $scope.entryDetObj.ENTRY_GOLD18_CREDIT = gold_rec[0].gold18Value;

                    if ($scope.bill_type_Id == 14) {

                        $scope.entryDetObj.ENTRY_GOLD24_CREDIT = $scope.total24;// $scope.convertGold.gold24Value;
                        $scope.entryDetObj.ENTRY_GOLD22_CREDIT = $scope.total22;// $scope.convertGold.gold22Value;
                        $scope.entryDetObj.ENTRY_GOLD21_CREDIT = $scope.total21;// $scope.convertGold.gold21Value;
                        $scope.entryDetObj.ENTRY_GOLD18_CREDIT = $scope.total18;// $scope.convertGold.gold18Value;
                    }
                }
                //if ($scope.bill_type_Id == 19) {

                //    var wages_rec = jQuery.grep($scope.billMaster.accounts, function (n, i) {
                //        return (n.accountID === $scope.acc_wages_Id);
                //    });
                //    $scope.entryDetObj.ENTRY_CREDIT = wages_rec[0].moneyValue;

                //}
                //Purchas
                if ($scope.bill_type_Id == 18) {
                    // $scope.entryDetObj.ENTRY_CREDIT = $scope.billMaster.TotalPrice - $scope.billMaster.TotalAllDisc;
                    if ($scope.billMaster.accounts[i].accountID == $scope.acc_purchas_Id) {
                        $scope.entryDetObj.ENTRY_CREDIT = 0;
                    }
                }

                //Sales
                //if ($scope.bill_type_Id == 17 && $scope.billMaster.accounts[i].accountID == $scope.acc_sales_Id) {
                //    if ($scope.entryDetObj.ENTRY_CREDIT > 0) {
                //        $scope.entryDetObj.ENTRY_CREDIT = $scope.billMaster.TotalPrice - $scope.billMaster.TotalAllDisc;
                //    }
                //}

                //if ($scope.bill_type_Id == 17) {
                //    if ($scope.billMaster.accounts[i].accountID == $scope.acc_vat_rateId) {
                //        $scope.entryDetObj.ENTRY_CREDIT = $scope.billMaster.TotalVatValue;
                //    }
                //}

            }
            else if ($scope.billMaster.accounts[i].accountType == 'D') {
                $scope.entryDetObj.ENTRY_DEBIT = $scope.billMaster.accounts[i].moneyValue == undefined ||
                    $scope.billMaster.accounts[i].moneyValue == null
                    ? 0
                    : $scope.billMaster.accounts[i].moneyValue;
                $scope.entryDetObj.ENTRY_GOLD24_DEBIT = $scope.billMaster.accounts[i].gold24Value;
                $scope.entryDetObj.ENTRY_GOLD22_DEBIT = $scope.billMaster.accounts[i].gold22Value;
                $scope.entryDetObj.ENTRY_GOLD21_DEBIT = $scope.billMaster.accounts[i].gold21Value;
                $scope.entryDetObj.ENTRY_GOLD18_DEBIT = $scope.billMaster.accounts[i].gold18Value;

                //Credit
                $scope.entryDetObj.ENTRY_GOLD24_CREDIT = 0;
                $scope.entryDetObj.ENTRY_GOLD22_CREDIT = 0;
                $scope.entryDetObj.ENTRY_GOLD21_CREDIT = 0;
                $scope.entryDetObj.ENTRY_GOLD18_CREDIT = 0;

                if ($scope.bill_type_Id == 13) {
                    var gold_rec = jQuery.grep($scope.billMaster.accounts, function (n, i) {
                        return (n.accountType === 'C');
                    });

                    $scope.entryDetObj.ENTRY_DEBIT = 0;
                    $scope.entryDetObj.ENTRY_GOLD24_DEBIT = $scope.total24;//$scope.convertGold.gold24Value;
                    $scope.entryDetObj.ENTRY_GOLD22_DEBIT = $scope.total22;//$scope.convertGold.gold22Value;
                    $scope.entryDetObj.ENTRY_GOLD21_DEBIT = $scope.total21;//$scope.convertGold.gold21Value;
                    $scope.entryDetObj.ENTRY_GOLD18_DEBIT = $scope.total18;//$scope.convertGold.gold18Value;

                }
                if ($scope.billMaster.accounts[i].accountID == $scope.acc_wages_Id) {

                    //$scope.entryDetObj.ENTRY_CREDIT = $scope.entryDetObj.ENTRY_DEBIT;
                    //$scope.entryDetObj.ENTRY_DEBIT = 0;
                }
                else if ($scope.billMaster.accounts[i].accountID == $scope.acc_wages_taxId) {

                    //$scope.entryDetObj.ENTRY_CREDIT = $scope.entryDetObj.ENTRY_DEBIT - ($scope.entryDetObj.ENTRY_DEBIT * $scope.wages_taxValue / 100);
                    //$scope.entryDetObj.ENTRY_DEBIT = 0;
                }

                if ($scope.billMaster.accounts[i].accountID == $scope.acc_discountVal) {
                    $scope.entryDetObj.ENTRY_CREDIT = $scope.entryDetObj.ENTRY_DEBIT;
                    $scope.entryDetObj.ENTRY_DEBIT = 0;
                }

                if ($scope.bill_type_Id == 18) {
                    if ($scope.billMaster.accounts[i].accountID == $scope.acc_vat_rateId) {
                        $scope.entryDetObj.ENTRY_DEBIT = $scope.billMaster.TotalVatValue;
                    }
                }
            }

            $scope.entryDetObj.COST_CENTER_ID = $scope.billMaster.CostCenterID;
            if ($scope.entryDetObj.ACC_ID != null &&
                $scope.entryDetObj.ACC_ID != undefined &&
                $scope.entryDetObj.ACC_ID != "" &&
                $scope.entryDetObj.ACC_ID != 0) {
                $scope.entryDetails.push($scope.entryDetObj);
            }
        }

        //if ($scope.bill_type_Id == 19) {

        //    var cust_rec = jQuery.grep($scope.billMaster.accounts, function (n, i) {
        //        return (n.accountID === $scope.billMaster.CUST_ACC_ID);
        //    });

        //    var totalManufactWagesAfterDiscount = $scope.wagesTaxToVat - $scope.wagesDiscountVal;
        //    if ($scope.bill_type_Id == 19) {
        //        $scope.entryDetObj = {};
        //        $scope.entryDetObj.ACC_ID = $scope.billMaster.CUST_ACC_ID;
        //        $scope.entryDetObj.ENTRY_CREDIT = (totalManufactWagesAfterDiscount * $scope.wages_taxValue / 100);
        //        $scope.entryDetails.push($scope.entryDetObj);
        //    }

        //}

        if ($scope.bill_type_Id == 18) {

            var gold_rec = jQuery.grep($scope.billMaster.accounts, function (n, i) {
                return (n.accountID === $scope.acc_purchas_Id);
            });

            $scope.entryDetObj = {};
            $scope.entryDetObj.ACC_ID = $scope.billMaster.CUST_ACC_ID;
            $scope.entryDetObj.ENTRY_DEBIT = 0;
            $scope.entryDetObj.ENTRY_GOLD24_DEBIT = gold_rec[0].gold24Value;
            $scope.entryDetObj.ENTRY_GOLD22_DEBIT = gold_rec[0].gold22Value;
            $scope.entryDetObj.ENTRY_GOLD21_DEBIT = gold_rec[0].gold21Value;
            $scope.entryDetObj.ENTRY_GOLD18_DEBIT = gold_rec[0].gold18Value;
            $scope.entryDetails.push($scope.entryDetObj);

            $scope.entryDetObj = {};
            $scope.entryDetObj.ACC_ID = $scope.billMaster.CUST_ACC_ID;
            $scope.entryDetObj.ENTRY_CREDIT = $scope.billMaster.TotalVatValue + $scope.billMaster.TotalAllDisc;
            $scope.entryDetObj.ENTRY_GOLD24_DEBIT = 0;
            $scope.entryDetObj.ENTRY_GOLD22_DEBIT = 0;
            $scope.entryDetObj.ENTRY_GOLD21_DEBIT = 0;
            $scope.entryDetObj.ENTRY_GOLD18_DEBIT = 0;
            $scope.entryDetails.push($scope.entryDetObj);

            //$scope.entryDetObj = {};
            //$scope.entryDetObj.ACC_ID = $scope.acc_purchas_Id;
            //$scope.entryDetObj.ENTRY_DEBIT = $scope.billMaster.TotalAllDisc;
            //$scope.entryDetObj.ENTRY_GOLD24_DEBIT = 0;
            //$scope.entryDetObj.ENTRY_GOLD22_DEBIT = 0;
            //$scope.entryDetObj.ENTRY_GOLD21_DEBIT = 0;
            //$scope.entryDetObj.ENTRY_GOLD18_DEBIT = 0;
            //$scope.entryDetails.push($scope.entryDetObj);
        }

        //if ($scope.bill_type_Id == 17) {

        //    var gold_rec = jQuery.grep($scope.billMaster.accounts, function (n, i) {
        //        return (n.accountID === $scope.acc_sales_Id);
        //    });

        //    $scope.entryDetObj = {};
        //    $scope.entryDetObj.ACC_ID = $scope.billMaster.CUST_ACC_ID;
        //    $scope.entryDetObj.ENTRY_CREDIT = 0;
        //    $scope.entryDetObj.ENTRY_GOLD24_CREDIT = gold_rec[0].gold24Value;
        //    $scope.entryDetObj.ENTRY_GOLD22_CREDIT = gold_rec[0].gold22Value;
        //    $scope.entryDetObj.ENTRY_GOLD21_CREDIT = gold_rec[0].gold21Value;
        //    $scope.entryDetObj.ENTRY_GOLD18_CREDIT = gold_rec[0].gold18Value;
        //    $scope.entryDetails.push($scope.entryDetObj);

        //    $scope.entryDetObj = {};
        //    $scope.entryDetObj.ACC_ID = $scope.billMaster.CUST_ACC_ID;
        //    $scope.entryDetObj.ENTRY_DEBIT = $scope.billMaster.TotalVatValue + $scope.billMaster.TotalAllDisc;
        //    $scope.entryDetObj.ENTRY_GOLD24_DEBIT = 0;
        //    $scope.entryDetObj.ENTRY_GOLD22_DEBIT = 0;
        //    $scope.entryDetObj.ENTRY_GOLD21_DEBIT = 0;
        //    $scope.entryDetObj.ENTRY_GOLD18_DEBIT = 0;
        //    $scope.entryDetails.push($scope.entryDetObj);

        //    //$scope.entryDetObj = {};
        //    //$scope.entryDetObj.ACC_ID = $scope.billMaster.CUST_ACC_ID;
        //    //$scope.entryDetObj.ENTRY_DEBIT = $scope.billMaster.TotalAllDisc;
        //    //$scope.entryDetObj.ENTRY_GOLD24_DEBIT = 0;
        //    //$scope.entryDetObj.ENTRY_GOLD22_DEBIT = 0;
        //    //$scope.entryDetObj.ENTRY_GOLD21_DEBIT = 0;
        //    //$scope.entryDetObj.ENTRY_GOLD18_DEBIT = 0;
        //    //$scope.entryDetails.push($scope.entryDetObj);
        //}

        //if ($scope.bill_type_Id == 24) {

        //    $scope.entryDetObj = {};
        //    $scope.entryDetObj.ACC_ID = $scope.billMaster.CUST_ACC_ID;
        //    $scope.entryDetObj.ENTRY_DEBIT = $scope.billMaster.TotalPrice - $scope.billMaster.TotalAllDisc;
        //    $scope.entryDetObj.ENTRY_GOLD24_DEBIT = 0;
        //    $scope.entryDetObj.ENTRY_GOLD22_DEBIT = 0;
        //    $scope.entryDetObj.ENTRY_GOLD21_DEBIT = 0;
        //    $scope.entryDetObj.ENTRY_GOLD18_DEBIT = 0;
        //    $scope.entryDetails.push($scope.entryDetObj);

        //    $scope.entryDetObj = {};
        //    $scope.entryDetObj.ACC_ID = $scope.billMaster.CUST_ACC_ID;
        //    $scope.entryDetObj.ENTRY_DEBIT = $scope.billMaster.TotalVatValue;
        //    $scope.entryDetObj.ENTRY_GOLD24_DEBIT = 0;
        //    $scope.entryDetObj.ENTRY_GOLD22_DEBIT = 0;
        //    $scope.entryDetObj.ENTRY_GOLD21_DEBIT = 0;
        //    $scope.entryDetObj.ENTRY_GOLD18_DEBIT = 0;
        //    $scope.entryDetails.push($scope.entryDetObj);
        //}


        var entryMasterDetails = { EntryMaster: $scope.entryMaster, EntryDetails: $scope.entryDetails };
        entryService.addEntryBill(entryMasterDetails).then(function (result) {
            var id = result.data;



            //$scope.billMaster.BILL_ID = $scope.billMasterID;
            $scope.billMaster.BILL_IS_POST = true;
            let promise = PointOfSaleService.updateEntryID($scope.billMaster, id).then(function (response) {
                var x = response.data;
            });
            return promise;
        });

        //Get report data
        //$scope.GetReportData();
    }

    $scope.getEntryNumber = function () {

        var urlParams = $location.search();
        if (urlParams.foo != null && urlParams.foo != undefined && urlParams.foo != "") { } else {
            entryService.GetLastEntryNumber(3).then(function (response) {
                var result = response.data;
                $scope.ENTRY_NUMBER = result + 1;
                //return $scope.ENTRY_NUMBER;
            })
        }

    }
}]);


