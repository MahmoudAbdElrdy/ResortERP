'use strict';
app.controller('billSettingController', ['$scope', '$location', '$log', '$q', 'billSettingService', 'salesTypeService', 'billTypeService', 'payTypeService', 'accountService', 'billShowOptionService', 'billOptionService', 'costCentersService', 'companyStoresService', 'currencyService', 'billGridColumn', '$timeout', 'MenuService', 'authService', 'accountsService', 'localStorageService', function ($scope, $location, $log, $q, billSettingService, salesTypeService, billTypeService, payTypeService, accountService, billShowOptionService, billOptionService, costCentersService, companyStoresService, currencyService, billGridColumn, $timeout, MenuService, authService, accountsService, localStorageService) {

    $scope.GoldSetting = false;
    $scope.gridOptions = {
        enableColumnMenus: false,
        showHeader: false,
        rowTemplate: '<div grid="grid" class="ui-grid-draggable-row" draggable="true"><div ng-repeat="(colRenderIndex, col) in colContainer.renderedColumns track by col.colDef.name" class="ui-grid-cell" ng-class="{ \'ui-grid-row-header-cell\': col.isRowHeader, \'custom\': true }" ui-grid-cell></div></div>',
        columnDefs: [{ name: "BILL_SHOW_ID", displayName: "", cellTemplate: '<div class="checkbox clip-check check-primary checkbox-inline" ><input id="{{row.entity.BILL_SHOW_AR_NAME}}" type="checkbox" ng-model="row.entity.Selected" value="{{row.entity.BILL_SHOW_ID}}"><label for="{{row.entity.BILL_SHOW_AR_NAME}}">{{row.entity.BILL_SHOW_AR_NAME}}</label></div>', enableSorting: false}],
    };

    $scope.gridOptions.onRegisterApi = function (gridApi) {
        gridApi.draggableRows.on.rowDropped($scope, function (info, dropTarget) {
            console.log("Dropped", info);
        });
      
    };

    $scope.fnOne = function (row) {
    }

    $scope.userModel = [];
    $scope.getUserModel = function () {
        if (localStorageService.get('UserModule') != null && localStorageService.get('UserModule') != undefined) {
            $scope.userModel = localStorageService.get('UserModule').UserModule;
        }

     
            
    }


    /////////////////get last code
    $scope.getSettingType = function () {



        var urlParams = $location.search();
        if (urlParams.foo != null && urlParams.foo != undefined && urlParams.foo != "") { } else {
            $scope.Type = ($location.search()).Type;
            if ($scope.Type != null && $scope.Type != undefined && $scope.Type != "")
            {
                
                $scope.GoldSetting = false;
            }
            else
            {
                $scope.GoldSetting = true;
            }
     
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

    $scope.clearEnity = function () {
        $scope.billSetting = { BillGrdCol: null, BILL_SETTING_ID: null };
        $scope.Custom = {};
        ClearBillOptionList();
        $scope.getbillShowList();
        $scope.gridData = {};
        //ClearShowOptionList();
    };

    $scope.mainMenuList = [];
    $scope.menu = {};


    //var ClearShowOptionList = function () {
    //    $scope.getbillShowList().then(function (result) {
    //        for (var i = 0; i < $scope.billShowList.length; i++) {
    //            $scope.billShowList[i].Selected = true;
    //        }
    //    });

    //};
    // $scope.gridData = {};

    $scope.gridData = {
        ARCode: 'كود', ARItem: 'الصنف', ARQuantity: 'الكميه', ARItemWeight: 'الوزن بالجرام', ARItemGmWages: 'اجره الجرام', ARManufacturingWages: 'اجره الصنف', ARUnit: 'الوحده', ARCaliberName: 'العيار',
        ARPrice: 'السعر', ARDiscount: 'قيمة الخصم', ARDiscRate: 'نسية الخصم', ARTotal: 'الاجمالي', ARClearnessRate: 'النقاوه', ARCaliber24: 'عيار 24', ARCaliber22: 'عيار 22', ARCaliber21: 'عيار 21',
        ARCaliber18: 'عيار 18', ARCostCenter: 'مركز التكلفه', ARSubjectToVat: 'خاضع للضريبه', ARVatRate: 'نسبة الضريبه', ARVatValue: 'قيمة الضريبه', ARWagesDiscValue: 'قيمة الخصم لاجور التصنيع'
        , ARWagesDiscRate: 'نسبة الخصم لاجور التصنيع', ARActualWeight: 'الوزن الفعلي', ARTotalItemGmWages: 'اجمالي اجره الجرام', ARTotalGoldManufact: 'اجمالي الذهب و اجور التصنيع'
        , ARExemptOfTax: 'معفي من الضريبه', ARItemPrice: 'سعر الصنف', ARAfterDiscount: 'بعد الخصم', ARLockPrice:'سعر الاقفال'

        , ENCode: 'Code', ENItem: 'Item', ENQuantity: 'Quantity', ENItemWeight: 'Item Weight', ENItemGmWages: 'Item Gm Wages', ENManufacturingWages: 'Manfacturing Wages', ENUnit: 'Unit'
        , ENCaliberName: 'Caliber', ENPrice: 'Price', ENDiscount: 'Discount Value', ENDiscRate: 'Discount Rate', ENTotal: 'Total', ENClearnessRate: 'Clearness', ENCaliber24: 'Caliber 24'
        , ENCaliber22: 'Caliber 22', ENCaliber21: 'Caliber 21', ENCaliber18: 'Caliber 18', ENCostCenter: 'Cost Center', ENSubjectToVat: 'Subject To Vat', ENVatRate: 'Vat Rate', ENVatValue: 'Vat Value'
        , ENWagesDiscValue: 'Wages Discount Value', ENWagesDiscRate: 'Wages Discount Rate', ENActualWeight: 'Actual Weight', ENTotalItemGmWages: 'Total ItemGmWages', ENTotalGoldManufact: 'Total Gold and manufacturing'
        , ENExemptOfTax: 'Exempt of tax', ENItemPrice: 'Item Price', ENAfterDiscount: 'After discount', ENLockPrice:'Lock Price'
    }


    $scope.defaultData = {
        arCode: 'كود', arItem: 'الصنف', arQuantity: 'الكميه', arItemWeight: 'الوزن بالجرام', arItemGmWages: 'اجره الجرام', arManfactWages: 'اجره الصنف', arUnit: 'الوحده', arCaliberName: 'العيار',
        arPrice: 'السعر', arDiscount: 'قيمة الخصم', arDiscRate: 'نسية الخصم', arTotal: 'الاجمالي', arClearnessRate: 'النقاوه', arCaliber24: 'عيار 24', arCaliber22: 'عيار 22', arCaliber21: 'عيار 21',
        arCaliber18: 'عيار 18', arCostCenter: 'مركز التكلفه', arSubjectToVat: 'خاضع للضريبه', arVatRate: 'نسبه الضريبه', arVatValue: 'قيمة الضريبه', arWagesDiscValue: 'قيمة الخصم لاجور التصنيع',
        arWagesDiscRate: 'نسبة الخصم لاجور التصنيع', arActualWeight: 'الوزن الفعلي', arTotalItemGmWages: 'اجمالي اجره الجرام', arTotalGoldManufact: 'اجمالي الذهب و اجور التصنيع'
        , arExemptOfTax: 'معفي من الضريبه', arItemPrice: 'سعر الصنف', arAfterDiscount: 'بعد الخصم', arLockPrice: 'سعر الاقفال'

        , enCode: 'Code', enItem: 'Item', enQuantity: 'Quantity', enItemWeight: 'Item Weight', enItemGmWages: 'Item Gm Wages', enManfactWages: 'Manfacturing Wages', enUnit: 'Unit'
        , enCaliberName: 'Caliber', enPrice: 'Price', enDiscount: 'Discount Value', enDiscRate: 'Discount Rate', enTotal: 'Total', enClearnessRate: 'Clearness', enCaliber24: 'Caliber 24'
        , enCaliber22: 'Caliber 22', enCaliber21: 'Caliber 21', enCaliber18: 'Caliber 18', enCostCenter: 'Cost Center', enSubjectToVat: 'Subject To Vat', enVatRate: 'Vat Rate', enVatValue: 'Vat Value'
        , enWagesDiscValue: 'Wages Discount Value', enWagesDiscRate: 'Wages Discount Rate', enActualWeight: 'Actual Weight', enTotalItemGmWages: 'Total ItemGmWages', enTotalGoldManufact: 'Total Gold and manufacturing'
        , enExemptOfTax: 'Exempt of tax', enItemPrice: 'Item Price', enAfterDiscount: 'After discount', enLockPrice: 'Lock Price'
    }


    var ClearBillOptionList = function () {
        for (var i = 0; i < $scope.billOptionList.length; i++) {
            $scope.billOptionList[i].Selected = false;
        }
    };

    $scope.Custom = {
        Currency: null, PayType: null, SellType: null, BillType: null, CompanyStore: null, ToCompanyStore: null, CostCenter: null, AccItem: null, AccDisc: null, AccExtra: null, AccPay: null, AccCost: null, AccStore: null, AccGift: null, AccID: null, AccWagesID: null, SearchType: null
    };
    $scope.billSetting = { BillGrdCol: null, BILL_SETTING_ID: null };
    $scope.billSettingTotalCount = 0;
    $scope.pageNum = 1;
    $scope.pageSize = 10;
    $scope.pagesCount = 0;
    $scope.maxSize = 5;
    $scope.maxLenght = 300;

    $scope.searchTypeList = [{ ID: 1, NAME: "البحث بكل الوحدات", Selected: false }, { ID: 2, NAME: "البحث بالوحدة الافتراضية", Selected: false }];


    $scope.getPagesCount = function () {
        var div = Math.floor($scope.billSettingTotalCount / $scope.pageSize);
        var rem = $scope.billSettingTotalCount % $scope.pageSize;
        if (rem > 0) {
            div = div + 1;
        }
        return div;
    }

    $scope.refreshData = function () {
        $scope.getAllOnLoad();
    }

    $scope.SetBillshowData = function (billSettingID, BillGridId) {
        $scope.billSetting.BillGrdCol = {};
        $scope.billSetting.BillGrdCol.BILL_SETTING_ID = billSettingID;
        $scope.billSetting.BillGrdCol.BILL_GRID_ID = BillGridId;
        var Counter = 1;
        for (var i = 0; i < $scope.billShowList.length; i++) {
            var obj = $scope.billShowList[i];
            if (obj.BILL_SHOW_ID == 1) {
                if (obj.Selected === true) {
                    $scope.billSetting.BillGrdCol.ITEM_INDEX = Counter;
                    Counter++;
                }
                else { $scope.billSetting.BillGrdCol.ITEM_INDEX = null; }
            }
            if (obj.BILL_SHOW_ID == 2) {
                if (obj.Selected === true) {
                    $scope.billSetting.BillGrdCol.QTY_INDEX = Counter;
                    Counter++;
                }
                else { $scope.billSetting.BillGrdCol.QTY_INDEX = null; }
            }
            if (obj.BILL_SHOW_ID == 3) {
                if (obj.Selected === true) {
                    $scope.billSetting.BillGrdCol.UNIT_COST_INDEX = Counter;
                    Counter++;
                }
                else { $scope.billSetting.BillGrdCol.UNIT_COST_INDEX = null; }
            }
            if (obj.BILL_SHOW_ID == 4) {
                if (obj.Selected === true) {
                    $scope.billSetting.BillGrdCol.TOTAL_INDEX = Counter;
                    Counter++;
                }
                else { $scope.billSetting.BillGrdCol.TOTAL_INDEX = null; }
            }
            if (obj.BILL_SHOW_ID == 5) {
                if (obj.Selected === true) {
                    $scope.billSetting.BillGrdCol.GIFTS_INDEX = Counter;
                    Counter++;
                }
                else { $scope.billSetting.BillGrdCol.GIFTS_INDEX = null; }
            }
            if (obj.BILL_SHOW_ID == 6) {
                if (obj.Selected === true) {
                    $scope.billSetting.BillGrdCol.DISC_RATE_INDEX = Counter;
                    Counter++;
                }
                else { $scope.billSetting.BillGrdCol.DISC_RATE_INDEX = null; }
            }
            if (obj.BILL_SHOW_ID == 7) {
                if (obj.Selected === true) {
                    $scope.billSetting.BillGrdCol.DISC_VALUE_INDEX = Counter;
                    Counter++;
                }
                else { $scope.billSetting.BillGrdCol.DISC_VALUE_INDEX = null; }
            }
            if (obj.BILL_SHOW_ID == 8) {
                if (obj.Selected === true) {
                    $scope.billSetting.BillGrdCol.EXTRA_RATE_INDEX = Counter;
                    Counter++;
                }
                else { $scope.billSetting.BillGrdCol.EXTRA_RATE_INDEX = null; }
            }
            if (obj.BILL_SHOW_ID == 9) {
                if (obj.Selected === true) {
                    $scope.billSetting.BillGrdCol.EXTRA_VALUE_INDEX = Counter;
                    Counter++;
                }
                else { $scope.billSetting.BillGrdCol.EXTRA_VALUE_INDEX = null; }
            }
            if (obj.BILL_SHOW_ID == 10) {
                if (obj.Selected === true) {
                    $scope.billSetting.BillGrdCol.PRODUCTION_DATE_INDEX = Counter;
                    Counter++;
                }
                else { $scope.billSetting.BillGrdCol.PRODUCTION_DATE_INDEX = null; }
            }
            if (obj.BILL_SHOW_ID == 11) {
                if (obj.Selected === true) {
                    $scope.billSetting.BillGrdCol.EXPIRED_DATE_INDEX = Counter;
                    Counter++;
                }
                else { $scope.billSetting.BillGrdCol.EXPIRED_DATE_INDEX = null; }
            }
            if (obj.BILL_SHOW_ID == 12) {
                if (obj.Selected === true) {
                    $scope.billSetting.BillGrdCol.BILL_REMARKS_INDEX = Counter;
                    Counter++;
                }
                else { $scope.billSetting.BillGrdCol.BILL_REMARKS_INDEX = null; }
            }
            if (obj.BILL_SHOW_ID == 13) {
                if (obj.Selected === true) {
                    $scope.billSetting.BillGrdCol.UNIT_INDEX = Counter;
                    Counter++;
                }
                else { $scope.billSetting.BillGrdCol.UNIT_INDEX = null; }
            }
            if (obj.BILL_SHOW_ID == 14) {
                if (obj.Selected === true) {
                    $scope.billSetting.BillGrdCol.ITEM_REMAIN_QTY_INDEX = Counter;
                    Counter++;
                }
                else { $scope.billSetting.BillGrdCol.ITEM_REMAIN_QTY_INDEX = null; }
            }
            if (obj.BILL_SHOW_ID == 15) {
                if (obj.Selected === true) {
                    $scope.billSetting.BillGrdCol.CTOTALCURR_INDEX = Counter;
                    Counter++;
                }
                else { $scope.billSetting.BillGrdCol.CTOTALCURR_INDEX = null; }
            }
            if (obj.BILL_SHOW_ID == 16) {
                if (obj.Selected === true) {
                    $scope.billSetting.BillGrdCol.CEXTERNALLEXPENSES_INDEX = Counter;
                    Counter++;
                }
                else { $scope.billSetting.BillGrdCol.CEXTERNALLEXPENSES_INDEX = null; }
            }
            if (obj.BILL_SHOW_ID == 17) {
                if (obj.Selected === true) {
                    $scope.billSetting.BillGrdCol.CTOTALWITHEXPENSES_INDEX = Counter;
                    Counter++;
                }
                else { $scope.billSetting.BillGrdCol.CTOTALWITHEXPENSES_INDEX = null; }
            }
            if (obj.BILL_SHOW_ID == 18) {
                if (obj.Selected === true) {
                    $scope.billSetting.BillGrdCol.CINVENTORYVALUE_INDEX = Counter;
                    Counter++;
                }
                else { $scope.billSetting.BillGrdCol.CINVENTORYVALUE_INDEX = null; }
            }
            if (obj.BILL_SHOW_ID == 19) {
                if (obj.Selected === true) {
                    $scope.billSetting.BillGrdCol.WHOLE_INDEX = Counter;
                    Counter++;
                }
                else { $scope.billSetting.BillGrdCol.WHOLE_INDEX = null; }
            }
            if (obj.BILL_SHOW_ID == 20) {
                if (obj.Selected === true) {
                    $scope.billSetting.BillGrdCol.HALF_WHOLE_INDEX = Counter;
                    Counter++;
                }
                else { $scope.billSetting.BillGrdCol.HALF_WHOLE_INDEX = null; }
            }
            if (obj.BILL_SHOW_ID == 21) {
                if (obj.Selected === true) {
                    $scope.billSetting.BillGrdCol.RETAIL_INDEX = Counter;
                    Counter++;
                }
                else { $scope.billSetting.BillGrdCol.RETAIL_INDEX = null; }
            }
            if (obj.BILL_SHOW_ID == 22) {
                if (obj.Selected === true) {
                    $scope.billSetting.BillGrdCol.END_USERS_INDEX = Counter;
                    Counter++;
                }
                else { $scope.billSetting.BillGrdCol.END_USERS_INDEX = null; }
            }
            if (obj.BILL_SHOW_ID == 23) {
                if (obj.Selected === true) {
                    $scope.billSetting.BillGrdCol.EMPLOYEE_INDEX = Counter;
                    Counter++;
                }
                else { $scope.billSetting.BillGrdCol.EMPLOYEE_INDEX = null; }
            }
            if (obj.BILL_SHOW_ID == 24) {
                if (obj.Selected === true) {
                    $scope.billSetting.BillGrdCol.EXPORT_INDEX = Counter;
                    Counter++;
                }
                else { $scope.billSetting.BillGrdCol.EXPORT_INDEX = null; }
            }
            if (obj.BILL_SHOW_ID == 25) {
                if (obj.Selected === true) {
                    $scope.billSetting.BillGrdCol.LAST_BUY_INDEX = Counter;
                    Counter++;
                }
                else { $scope.billSetting.BillGrdCol.LAST_BUY_INDEX = null; }
            }


            if (obj.BILL_SHOW_ID == 26) {
                if (obj.Selected === true) {
                    $scope.billSetting.BillGrdCol.Cost_Center_INDEX = Counter;
                    Counter++;
                }
                else { $scope.billSetting.BillGrdCol.Cost_Center_INDEX = null; }
            }


            if (obj.BILL_SHOW_ID == 27) {
                if (obj.Selected === true) {
                    $scope.billSetting.BillGrdCol.Item_Weight_INDEX = Counter;
                    Counter++;
                }
                else { $scope.billSetting.BillGrdCol.Item_Weight_INDEX = null; }
            }

            if (obj.BILL_SHOW_ID == 28) {
                if (obj.Selected === true) {
                    $scope.billSetting.BillGrdCol.Item_Wages_INDEX = Counter;
                    Counter++;
                }
                else { $scope.billSetting.BillGrdCol.Item_Wages_INDEX = null; }
            }

            if (obj.BILL_SHOW_ID == 29) {
                if (obj.Selected === true) {
                    $scope.billSetting.BillGrdCol.Manufact_Wages_INDEX = Counter;
                    Counter++;
                }
                else { $scope.billSetting.BillGrdCol.Manufact_Wages_INDEX = null; }
            }
            if (obj.BILL_SHOW_ID == 30) {
                if (obj.Selected === true) {
                    $scope.billSetting.BillGrdCol.ARName_INDEX = Counter;
                    Counter++;
                }
                else { $scope.billSetting.BillGrdCol.ARName_INDEX = null; }
            }
            if (obj.BILL_SHOW_ID == 31) {
                if (obj.Selected === true) {
                    $scope.billSetting.BillGrdCol.Clearness_Rate_INDEX = Counter;
                    Counter++;
                }
                else { $scope.billSetting.BillGrdCol.Clearness_Rate_INDEX = null; }
            }
            if (obj.BILL_SHOW_ID == 32) {
                if (obj.Selected === true) {
                    $scope.billSetting.BillGrdCol.Caliber24_INDEX = Counter;
                    Counter++;
                }
                else { $scope.billSetting.BillGrdCol.Caliber24_INDEX = null; }
            }
            if (obj.BILL_SHOW_ID == 33) {
                if (obj.Selected === true) {
                    $scope.billSetting.BillGrdCol.Caliber22_INDEX = Counter;
                    Counter++;
                }
                else { $scope.billSetting.BillGrdCol.Caliber22_INDEX = null; }
            }
            if (obj.BILL_SHOW_ID == 34) {
                if (obj.Selected === true) {
                    $scope.billSetting.BillGrdCol.Caliber21_INDEX = Counter;
                    Counter++;
                }
                else { $scope.billSetting.BillGrdCol.Caliber21_INDEX = null; }
            }
            if (obj.BILL_SHOW_ID == 35) {
                if (obj.Selected === true) {
                    $scope.billSetting.BillGrdCol.Caliber18_INDEX = Counter;
                    Counter++;
                }
                else { $scope.billSetting.BillGrdCol.Caliber18_INDEX = null; }
            }
            if (obj.BILL_SHOW_ID == 36) {
                if (obj.Selected === true) {
                    $scope.billSetting.BillGrdCol.Subject_To_VAT_INDEX = Counter;
                    Counter++;
                }
                else { $scope.billSetting.BillGrdCol.Subject_To_VAT_INDEX = null; }
            }
            if (obj.BILL_SHOW_ID == 37) {
                if (obj.Selected === true) {
                    $scope.billSetting.BillGrdCol.VAT_Rate_INDEX = Counter;
                    Counter++;
                }
                else { $scope.billSetting.BillGrdCol.VAT_Rate_INDEX = null; }
            }

            if (obj.BILL_SHOW_ID == 38) {
                if (obj.Selected === true) {
                    $scope.billSetting.BillGrdCol.Currency_INDEX = Counter;
                    Counter++;
                }
                else { $scope.billSetting.BillGrdCol.Currency_INDEX = null; }
            }

            if (obj.BILL_SHOW_ID == 39) {
                if (obj.Selected === true) {
                    $scope.billSetting.BillGrdCol.Inventory_INDEX = Counter;
                    Counter++;
                }
                else { $scope.billSetting.BillGrdCol.Inventory_INDEX = null; }
            }

            if (obj.BILL_SHOW_ID == 40) {
                if (obj.Selected === true) {
                    $scope.billSetting.BillGrdCol.GoldBox_INDEX = Counter;
                    Counter++;
                }
                else { $scope.billSetting.BillGrdCol.GoldBox_INDEX = null; }
            }

            if (obj.BILL_SHOW_ID == 41) {
                if (obj.Selected === true) {
                    $scope.billSetting.BillGrdCol.BillCostCenter_INDEX = Counter;
                    Counter++;
                }
                else { $scope.billSetting.BillGrdCol.BillCostCenter_INDEX = null; }
            }

            if (obj.BILL_SHOW_ID == 42) {
                if (obj.Selected === true) {
                    $scope.billSetting.BillGrdCol.PayTypes_INDEX = Counter;
                    Counter++;
                }
                else { $scope.billSetting.BillGrdCol.PayTypes_INDEX = null; }
            }

            if (obj.BILL_SHOW_ID == 43) {
                if (obj.Selected === true) {
                    $scope.billSetting.BillGrdCol.CaliberTrans_INDEX = Counter;
                    Counter++;
                }
                else { $scope.billSetting.BillGrdCol.CaliberTrans_INDEX = null; }
            }


            if (obj.BILL_SHOW_ID == 44) {
                if (obj.Selected === true) {
                    $scope.billSetting.BillGrdCol.VatValue_INDEX = Counter;
                    Counter++;
                }
                else { $scope.billSetting.BillGrdCol.VatValue_INDEX = null; }
            }


            if (obj.BILL_SHOW_ID == 45) {
                if (obj.Selected === true) {
                    $scope.billSetting.BillGrdCol.WagesDiscValue_INDEX = Counter;
                    Counter++;
                }
                else { $scope.billSetting.BillGrdCol.WagesDiscValue_INDEX = null; }
            }

            if (obj.BILL_SHOW_ID == 46) {
                if (obj.Selected === true) {
                    $scope.billSetting.BillGrdCol.WagesDiscRate_INDEX = Counter;
                    Counter++;
                }
                else { $scope.billSetting.BillGrdCol.WagesDiscRate_INDEX = null; }
            }


            if (obj.BILL_SHOW_ID == 47) {
                if (obj.Selected === true) {
                    $scope.billSetting.BillGrdCol.ActualWeight_INDEX = Counter;
                    Counter++;
                }
                else { $scope.billSetting.BillGrdCol.ActualWeight_INDEX = null; }
            }


            if (obj.BILL_SHOW_ID == 48) {
                if (obj.Selected === true) {
                    $scope.billSetting.BillGrdCol.TaxTotal_INDEX = Counter;
                    Counter++;
                }
                else { $scope.billSetting.BillGrdCol.TaxTotal_INDEX = null; }
            }


            if (obj.BILL_SHOW_ID == 49) {
                if (obj.Selected === true) {
                    $scope.billSetting.BillGrdCol.TotalItemGmWages_INDEX = Counter;
                    Counter++;
                }
                else { $scope.billSetting.BillGrdCol.TotalItemGmWages_INDEX = null; }
            }


            if (obj.BILL_SHOW_ID == 50) {
                if (obj.Selected === true) {
                    $scope.billSetting.BillGrdCol.TotalGoldManufact_INDEX = Counter;
                    Counter++;
                }
                else { $scope.billSetting.BillGrdCol.TotalGoldManufact_INDEX = null; }
            }


            if (obj.BILL_SHOW_ID == 51) {
                if (obj.Selected === true) {
                    $scope.billSetting.BillGrdCol.ExemptOfTax_INDEX = Counter;
                    Counter++;
                }
                else { $scope.billSetting.BillGrdCol.ExemptOfTax_INDEX = null; }
            }



            if (obj.BILL_SHOW_ID == 52) {
                if (obj.Selected === true) {
                    $scope.billSetting.BillGrdCol.ItemPrice_INDEX = Counter;
                    Counter++;
                }
                else { $scope.billSetting.BillGrdCol.ItemPrice_INDEX = null; }
            }


            if (obj.BILL_SHOW_ID == 53) {
                if (obj.Selected === true) {
                    $scope.billSetting.BillGrdCol.AfterDiscount_INDEX = Counter;
                    Counter++;
                }
             
                else { $scope.billSetting.BillGrdCol.AfterDiscount_INDEX = null; }
            }
            if (obj.BILL_SHOW_ID == 54) {
                if (obj.Selected === true) {
                    $scope.billSetting.BillGrdCol.LockPrice_INDEX = Counter;
                    Counter++;
                }
                else { $scope.billSetting.BillGrdCol.LockPrice_INDEX = null; }
            }

          
        }

        debugger;
        $scope.setGridData();
    };

    $scope.SetBillOptionData = function (option) {
        option.GENERATE_ENTRY_STATE = $scope.billOptionList[0].Selected ? 1 : 0;
        option.IS_AUTO_POSTING = $scope.billOptionList[1].Selected;
        option.IS_AUTO_POSTING = $scope.billOptionList[2].Selected;
        option.CONTINEOUS_INVENTORY = $scope.billOptionList[3].Selected;
        option.GENERATE_COST_ENTRY = $scope.billOptionList[4].Selected ? 1 : 0;
        option.PRICE_COST_EFFECT = $scope.billOptionList[5].Selected;
        option.STORE_EFFECT_STATE = $scope.billOptionList[6].Selected ? 1 : 0;
        option.STORE_EFFECT_STATE = $scope.billOptionList[7].Selected ? 2 : 3;
        option.ABRV_BILL = $scope.billOptionList[8].Selected;
        option.SHOW_COST_CENTER = $scope.billOptionList[9].Selected;
        option.SHOW_EMPLOYEE = $scope.billOptionList[10].Selected;
        option.OUT_MINUS = $scope.billOptionList[11].Selected;
        option.BILL_SHORTCUT = $scope.billOptionList[12].Selected;
        option.FIRST_EXPIRE = $scope.billOptionList[13].Selected;
        option.MIN_QTY = $scope.billOptionList[14].Selected;
        option.SERIAL_NUMBER = $scope.billOptionList[15].Selected;

        option.IsBillDate = $scope.billOptionList[16].Selected;
        option.IsShiftNumber = $scope.billOptionList[17].Selected;
        option.IsSellType = $scope.billOptionList[18].Selected;
        option.IsPayWay = $scope.billOptionList[19].Selected;
        option.IsAccount = $scope.billOptionList[20].Selected;
        option.IsCustAccID = $scope.billOptionList[21].Selected;
        option.IsItemAccID = $scope.billOptionList[22].Selected;
        option.IsCompStoreID = $scope.billOptionList[23].Selected;
        option.IsEmpID = $scope.billOptionList[24].Selected;
        option.IsBillRemarks = $scope.billOptionList[25].Selected;
        option.IsCurrency = $scope.billOptionList[26].Selected;
        option.IsPayTypes = $scope.billOptionList[27].Selected;
        option.IsCurrencyTrans = $scope.billOptionList[28].Selected;

        option.IsTotalExtra = $scope.billOptionList[29].Selected;
        option.IsTotalPaid = $scope.billOptionList[30].Selected;
        option.IsTotalMustPaid = $scope.billOptionList[31].Selected;
        option.IsTotalRemaining = $scope.billOptionList[32].Selected;
        option.IsToCompStore = $scope.billOptionList[33].Selected;

        option.IsItemsGroups = $scope.billOptionList[34].Selected;
        option.IsItems = $scope.billOptionList[35].Selected;
        option.Show_Wages_Discount = $scope.billOptionList[36].Selected;
        option.IsQuantityCalculated = $scope.billOptionList[37].Selected;

        option.IsShowEditReason = $scope.billOptionList[38].Selected;
        option.IsShowDeliveryPerson = $scope.billOptionList[39].Selected;

        option.IsShowExternalNumber = $scope.billOptionList[40].Selected;
        option.IsEnableTaxEdit = $scope.billOptionList[41].Selected;
        option.IsShowGoldBoxBalance = $scope.billOptionList[42].Selected;
        //option.IsShowCustomerBalance = $scope.billOptionList[43].Selected;

        option.IsCalcClearnessRate = $scope.billOptionList[43].Selected;

        option.IsBillDiscRate = $scope.billOptionList[44].Selected;
        option.IsBillDiscValue = $scope.billOptionList[45].Selected;
        option.IsTotalDiscValues = $scope.billOptionList[46].Selected;
        option.IsTotalDiscRates = $scope.billOptionList[47].Selected;

        option.IsShowTaxesBox = $scope.billOptionList[48].Selected;
        option.IsEnableWeight = $scope.billOptionList[49].Selected;
        option.IsEnableGmWages = $scope.billOptionList[50].Selected;
        option.IsLockPrice = $scope.billOptionList[51].Selected;

    };


    $scope.savebillSetting = function () {
        $scope.saveEntity();
    }
    $scope.saveEntity = function () {
        if ($scope.Custom.BillType != undefined || $scope.Custom.BillType != null)
            $scope.billSetting.BILL_TYPE_ID = $scope.Custom.BillType.BILL_TYPE_ID;
        else $scope.billSetting.BILL_TYPE_ID = null;

        if ($scope.Custom.SellType != undefined || $scope.Custom.SellType != null)
            $scope.billSetting.BILL_SELL_TYPE_ID = $scope.Custom.SellType.SELL_TYPE_ID;
        else $scope.billSetting.BILL_SELL_TYPE_ID = null;

        if ($scope.Custom.PayType != undefined || $scope.Custom.PayType != null)
            $scope.billSetting.BILL_PAY_TYPE = $scope.Custom.PayType.PAY_TYPE_ID;
        else $scope.billSetting.BILL_PAY_TYPE = null;

        if ($scope.Custom.Currency != undefined || $scope.Custom.Currency != null)
            $scope.billSetting.CURRENCY_ID = $scope.Custom.Currency.CURRENCY_ID;
        else $scope.billSetting.CURRENCY_ID = null;

        if ($scope.Custom.CompanyStore != undefined || $scope.Custom.CompanyStore != null)
            $scope.billSetting.COM_STORE_ID = $scope.Custom.CompanyStore.COM_STORE_ID;
        else $scope.billSetting.COM_STORE_ID = null;

        if ($scope.Custom.ToCompanyStore != undefined || $scope.Custom.ToCompanyStore != null)
            $scope.billSetting.TO_COM_STORE_ID = $scope.Custom.ToCompanyStore.COM_STORE_ID;
        else $scope.billSetting.TO_COM_STORE_ID = null;

        if ($scope.Custom.CostCenter != undefined || $scope.Custom.CostCenter != null)
            $scope.billSetting.COST_CENTER_ID = $scope.Custom.CostCenter.COST_CENTER_ID;
        else $scope.billSetting.COST_CENTER_ID = null;

        if ($scope.Custom.SearchType != undefined || $scope.Custom.SearchType != null)
            $scope.billSetting.SEARCH_ONLY_BY_DEAULT_UNIT = $scope.Custom.SearchType.ID;
        else $scope.billSetting.SEARCH_ONLY_BY_DEAULT_UNIT = null;



        if ($scope.Custom.AccItem != undefined || $scope.Custom.AccItem != null)
            $scope.billSetting.ACC_ITEM_ID = $scope.Custom.AccItem;
        else $scope.billSetting.ACC_ITEM_ID = null;

        if ($scope.Custom.AccDisc != undefined || $scope.Custom.AccDisc != null)
            $scope.billSetting.ACC_DISC_ID = $scope.Custom.AccDisc;
        else $scope.billSetting.ACC_DISC_ID = null;

        if ($scope.Custom.AccExtra != undefined || $scope.Custom.AccExtra != null)
            $scope.billSetting.ACC_EXTRA_ID = $scope.Custom.AccExtra;
        else $scope.billSetting.ACC_EXTRA_ID = null;

        if ($scope.Custom.AccPay != undefined || $scope.Custom.AccPay != null)
            $scope.billSetting.ACC_PAY_ID = $scope.Custom.AccPay;
        else $scope.billSetting.ACC_PAY_ID = null;

        if ($scope.Custom.AccCost != undefined || $scope.Custom.AccCost != null)
            $scope.billSetting.ACC_COST_ID = $scope.Custom.AccCost;
        else $scope.billSetting.ACC_COST_ID = null;

        if ($scope.Custom.AccStore != undefined || $scope.Custom.AccStore != null)
            $scope.billSetting.ACC_STORE_ID = $scope.Custom.AccStore;
        else $scope.billSetting.ACC_STORE_ID = null;

        if ($scope.Custom.AccGift != undefined || $scope.Custom.AccGift != null)
            $scope.billSetting.ACC_GIFT_ID = $scope.Custom.AccGift;
        else $scope.billSetting.ACC_GIFT_ID = null;

        if ($scope.Custom.AccWagesID != undefined || $scope.Custom.AccWagesID != null)
            $scope.billSetting.AccWagesID = $scope.Custom.AccWagesID;
        else $scope.billSetting.AccWagesID = null;

        if ($scope.Custom.AccSalesID != undefined || $scope.Custom.AccSalesID != null)
            $scope.billSetting.AccSalesID = $scope.Custom.AccSalesID;
        else $scope.billSetting.AccSalesID = null;

        if ($scope.Custom.AccVatRateID != undefined || $scope.Custom.AccVatRateID != null)
            $scope.billSetting.AccVatRateID = $scope.Custom.AccVatRateID;
        else $scope.billSetting.AccVatRateID = null;


        if ($scope.Custom.MenuID != undefined || $scope.Custom.MenuID != null)
            $scope.billSetting.MenuID = $scope.Custom.MenuID;
        else $scope.billSetting.MenuID = null;

        //////////////////////////////////////////

        if ($scope.Custom.CashAccountID != undefined || $scope.Custom.CashAccountID != null)
            $scope.billSetting.CashAccountID = $scope.Custom.CashAccountID;
        else $scope.billSetting.CashAccountID = null;



        if ($scope.Custom.VisaAccountID != undefined || $scope.Custom.VisaAccountID != null)
            $scope.billSetting.VisaAccountID = $scope.Custom.VisaAccountID;
        else $scope.billSetting.VisaAccountID = null;


        if ($scope.Custom.NoPaidAccountID != undefined || $scope.Custom.NoPaidAccountID != null)
            $scope.billSetting.NoPaidAccountID = $scope.Custom.NoPaidAccountID;
        else $scope.billSetting.NoPaidAccountID = null;


        if ($scope.Custom.PaymentAccountID != undefined || $scope.Custom.PaymentAccountID != null)
            $scope.billSetting.PaymentAccountID = $scope.Custom.PaymentAccountID;
        else $scope.billSetting.PaymentAccountID = null;



        if ($scope.Custom.AccWagesTaxID != undefined || $scope.Custom.AccWagesTaxID != null)
            $scope.billSetting.AccWagesTaxID = $scope.Custom.AccWagesTaxID;
        else $scope.billSetting.AccWagesTaxID = null;



        if ($scope.Custom.WagesTaxValue != undefined || $scope.Custom.WagesTaxValue != null)
            $scope.billSetting.WagesTaxValue = $scope.Custom.WagesTaxValue;
        else $scope.billSetting.WagesTaxValue = null;


        if ($scope.Custom.ShowPriceTypeID != undefined || $scope.Custom.ShowPriceTypeID != null)
            $scope.billSetting.ShowPriceTypeID = $scope.Custom.ShowPriceTypeID;
        else $scope.billSetting.ShowPriceTypeID = null;



        if ($scope.Custom.PurchasAccID != undefined || $scope.Custom.PurchasAccID != null)
            $scope.billSetting.PurchasAccID = $scope.Custom.PurchasAccID;
        else $scope.billSetting.PurchasAccID = null;


        if ($scope.Custom.PurchasTaxAccID != undefined || $scope.Custom.PurchasTaxAccID != null)
            $scope.billSetting.PurchasTaxAccID = $scope.Custom.PurchasTaxAccID;
        else $scope.billSetting.PurchasTaxAccID = null;




        if ($scope.Custom.AccCommissionID != undefined || $scope.Custom.AccCommissionID != null)
            $scope.billSetting.AccCommissionID = $scope.Custom.AccCommissionID;
        else $scope.billSetting.AccCommissionID = null;


        if ($scope.Custom.AccCommissionTaxID != undefined || $scope.Custom.AccCommissionTaxID != null)
            $scope.billSetting.AccCommissionTaxID = $scope.Custom.AccCommissionTaxID;
        else $scope.billSetting.AccCommissionTaxID = null;




        if ($scope.Custom.AccSalesGoldID != undefined || $scope.Custom.AccSalesGoldID != null)
            $scope.billSetting.AccSalesGoldID = $scope.Custom.AccSalesGoldID;
        else $scope.billSetting.AccSalesGoldID = null;


        if ($scope.Custom.AccPurchaseGoldID != undefined || $scope.Custom.AccPurchaseGoldID != null)
            $scope.billSetting.AccPurchaseGoldID = $scope.Custom.AccPurchaseGoldID;
        else $scope.billSetting.AccPurchaseGoldID = null;

        //if ($scope.Custom.Show_Wages_Discount != undefined || $scope.Custom.Show_Wages_Discount != null)
        //    $scope.billSetting.Show_Wages_Discount = $scope.Custom.Show_Wages_Discount;
        //else $scope.billSetting.Show_Wages_Discount = null;



        if ($scope.Custom.AccGoldID != undefined || $scope.Custom.AccGoldID != null)
            $scope.billSetting.AccGoldID = $scope.Custom.AccGoldID;
        else $scope.billSetting.AccGoldID = null;

        ///////////////////////////////////////////

        var addedBy = authService.GetUserName();
        var addeddOn = new Date();


        $scope.billSetting.AddedBy = addedBy;
        $scope.billSetting.AddedOn = addeddOn;
        //$scope.billSetting.ACC_ID = $scope.Custom.AccID.ACC_ID;

        if ($scope.billSetting !== undefined && $scope.billSetting !== null && $scope.billSetting.BILL_AR_NAME !== null && $scope.billSetting.BILL_EN_NAME !== null && $scope.billSetting.BILL_ABRV_AR !== null && $scope.billSetting.BILL_ABRV_EN !== null && $scope.billSetting.BILL_TYPE_ID !== null && $scope.billSetting.GENERATE_ENTRY_STATE !== null && $scope.billSetting.IS_AUTO_POSTING !== null && $scope.billSetting.CONTINEOUS_INVENTORY !== null && $scope.billSetting.PRICE_COST_EFFECT !== null && $scope.billSetting.LAST_PAY_PRICE !== null && $scope.billSetting.STORE_EFFECT_STATE !== null && $scope.billSetting.ABRV_BILL !== null && $scope.billSetting.SHOW_COST_CENTER !== null && $scope.billSetting.SHOW_EMPLOYEE !== null && $scope.billSetting.BILL_PAY_TYPE !== null && $scope.billSetting.BILL_SHOW_NAME !== null && $scope.billSetting.DISABLE_BILL_PAY_TYPE !== null && $scope.billSetting.SHOW_BILL_PAY_TYPE !== null && $scope.billSetting.AUTOMATIC_DISCOUNT !== null && $scope.billSetting.PRINTHALFPAGEBYLONG !== null && $scope.billSetting.MODULE_CARS !== null && $scope.billSetting.PRINT_BILL_AUTOMATIC !== null && $scope.billSetting.SHOW_THE_RETURN_PERCENTAGE !== null && $scope.billSetting.SHOW_THE_LAST_DATE_FOR_RETURN !== null && $scope.billSetting.SHOW_THE_LAST_BALANCE_ON_THE_BILL !== null && $scope.billSetting.PRINT_BILL_AS_RESET_OR_AS_BILL !== null && $scope.billSetting.NUMBEROFCOPIES !== null) {
            if ($scope.billSetting.BILL_SETTING_ID === null || $scope.BILL_SETTING_ID === 0) {
                billSettingService.chkExist($scope.billSetting).then(function (response) {
                    var check = response.data;
                    if (check != null || check != undefined) {
                        var nameArCount = check.nameAr;
                        var nameEnCount = check.nameEn;
                        var aprvArCount = check.aprvAr;
                        var aprvEnCount = check.aprvEn;
                        if (nameArCount >= 1 || nameEnCount >= 1 || aprvArCount >= 1 || aprvEnCount >= 1) {
                            var message = "<div style='text-align:right;direction:rtl'> الحقول التالية موجودة سابقاً : <br><br> <ul style='margin-right: 30px;'>";
                            message += nameArCount >= 1 ? "<li>الاسم العربي </li>" : "";
                            message += nameEnCount >= 1 ? "<li>الاسم اللاتيني </li>" : "";
                            message += aprvArCount >= 1 ? "<li>الاسم المختصر العربي </li>" : "";
                            message += aprvEnCount >= 1 ? "<li>الاسم المختصر اللاتيني </li></ul>" : "";
                            message += "<br> برجاء تغيير اسماء تلك الحقول</div>";
                            swal("", message, 'warning');
                        } else {
                            debugger
                            $scope.SetBillOptionData($scope.billSetting);
                            $scope.billSetting.BillGrdCol = null;
                            $scope.insert($scope.billSetting).then(function (results) {
                                $scope.settingID = results.data;

                                $scope.saveBillSettingAsMenu();
                                debugger;
                                $scope.SetBillshowData(results.data, 0);
                                billGridColumn.insert($scope.billSetting.BillGrdCol).then(function (response) {
                                    $scope.clearEnity();
                                    $scope.refreshData();
                                    swal({
                                        title: 'تم',
                                        text: 'حفظ اعدادات الفواتير بنجاح',
                                        type: 'success',
                                        timer: 1500,
                                        showConfirmButton: false
                                    });
                                });

                            }, function (error) {
                                console.log(error.data.message);
                                swal('عفواً',
                                    'حدث خطأ عند حفظ اعدادات الفواتير',
                                    'error');
                            });
                        }
                    }
                }, function (error) {

                });
            } else {
                $scope.SetBillOptionData($scope.billSetting);
                $scope.update($scope.billSetting).then(function (results) {
                    $scope.SetBillshowData($scope.billSetting.BILL_SETTING_ID, $scope.billSetting.BillGrdCol.BILL_GRID_ID);
                    $scope.updateSettingMenu();
                    billGridColumn.update($scope.billSetting.BillGrdCol).then(function (response) {
                        $scope.clearEnity();
                        $scope.refreshData();
                        swal({
                            title: 'تم',
                            text: 'تعديل اعدادات الفواتير بنجاح',
                            type: 'success',
                            timer: 1500,
                            showConfirmButton: false
                        });
                    });
                }, function (error) {
                    console.log(error.data.message);
                    swal('عفواً',
                        'حدث خطأ عند تعديل اعدادات الفواتير',
                        'error');
                });
            }
        }
    }


    /*****************************************************************************************************************************/
    //filter Selected BillType from BillTypeList during update
    $scope.getBillTypeBasedOnBillTypeID = function (BillTypeID) {
        for (var i = 0; i < $scope.billTypeList.length; i++) {
            if ($scope.billTypeList[i].BILL_TYPE_ID == BillTypeID) {
                return $scope.billTypeList[i];
            }
        }
    }

    //filter Selected BILLSELLTYPE from BILLSELLTYPEList during update
    $scope.getSellTypeBasedOnSellTypeID = function (BILLSELLTYPEID) {
        for (var i = 0; i < $scope.SellTypeList.length; i++) {
            if ($scope.SellTypeList[i].SELL_TYPE_ID == BILLSELLTYPEID) {
                return $scope.SellTypeList[i];
            }
        }
    }

    //filter Selected payTypeList from BILLpayTypeList during update
    $scope.getPayTypeBasedOnPayTypeID = function (BILLPAYTYPE) {
        for (var i = 0; i < $scope.payTypeList.length; i++) {
            var payType = $scope.payTypeList[i].PAY_TYPE_ID
            if (payType == BILLPAYTYPE) {
                return $scope.payTypeList[i];
            }
        }
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

    //filter Selected CompanyStoreList from CompanyStoreList during update
    $scope.getCompanyStoreBasedOnCompanyStoreID = function (CompanyStoreID) {
        for (var i = 0; i < $scope.CompanyStoresList.length; i++) {
            var compID = $scope.CompanyStoresList[i].COM_STORE_ID
            if (compID == CompanyStoreID) {
                return $scope.CompanyStoresList[i];
            }
        }
    }

    //filter Selected CompanyStoreList from CompanyStoreList during update
    $scope.getToCompanyStoreBasedOnCompanyStoreID = function (ToCompanyStoreID) {
        for (var i = 0; i < $scope.CompanyStoresList.length; i++) {
            var compID = $scope.CompanyStoresList[i].TO_COM_STORE_ID
            if (compID == ToCompanyStoreID) {
                return $scope.CompanyStoresList[i];
            }
        }
    }

    //filter Selected CompanyStoreList from CompanyStoreList during update
    $scope.getCostCenterBasedOnCostCenterID = function (CostCenterID) {
        for (var i = 0; i < $scope.CostCenterList.length; i++) {
            var coCenter = $scope.CostCenterList[i].COST_CENTER_ID
            if (coCenter == CostCenterID) {
                return $scope.CostCenterList[i];
            }
        } if (coCenter == undefined) { return null; }
    }


    //filter Selected CompanyStoreList from CompanyStoreList during update
    $scope.getAccItemBasedOnAccID = function (AccID) {
        for (var i = 0; i < $scope.accountList.length; i++) {
            var accId = $scope.accountList[i].ACC_ID
            if (accId == AccID) {
                return $scope.accountList[i];
            }
        } if (accId == undefined) { return null; }
    }

    //filter Selected CompanyStoreList from CompanyStoreList during update
    $scope.getAccDiscBasedOnAccID = function (AccID) {
        for (var i = 0; i < $scope.accountList.length; i++) {
            var accId = $scope.accountList[i].ACC_ID
            if (accId == AccID) {
                return $scope.accountList[i];
            }
        } if (accId == undefined) { return null; }
    }

    //filter Selected CompanyStoreList from CompanyStoreList during update
    $scope.getAccExtraBasedOnAccID = function (AccID) {
        for (var i = 0; i < $scope.accountList.length; i++) {
            var accId = $scope.accountList[i].ACC_ID
            if (accId == AccID) {
                return $scope.accountList[i];
            }
        } if (accId == undefined) { return null; }
    }

    //filter Selected CompanyStoreList from CompanyStoreList during update
    $scope.getAccPayBasedOnAccID = function (AccID) {
        for (var i = 0; i < $scope.accountList.length; i++) {
            var accId = $scope.accountList[i].ACC_ID
            if (accId == AccID) {
                return $scope.accountList[i];
            }
        } if (accId == undefined) { return null; }
    }

    //filter Selected CompanyStoreList from CompanyStoreList during update
    $scope.getAccCostBasedOnAccID = function (AccID) {
        for (var i = 0; i < $scope.accountList.length; i++) {
            var accId = $scope.accountList[i].ACC_ID
            if (accId == AccID) {
                return $scope.accountList[i];
            }
        } if (accId == undefined) { return null; }
    }

    //filter Selected CompanyStoreList from CompanyStoreList during update
    $scope.getAccStoreBasedOnAccID = function (AccID) {
        for (var i = 0; i < $scope.accountList.length; i++) {
            var accId = $scope.accountList[i].ACC_ID
            if (accId == AccID) {
                return $scope.accountList[i];
            }
        } if (accId == undefined) { return null; }
    }

    //filter Selected CompanyStoreList from CompanyStoreList during update
    $scope.getAccGiftBasedOnAccID = function (AccID) {
        for (var i = 0; i < $scope.accountList.length; i++) {
            var accId = $scope.accountList[i].ACC_ID
            if (accId == AccID) {
                return $scope.accountList[i];
            }
        } if (accId == undefined) { return null; }
    }

    //filter Selected CompanyStoreList from CompanyStoreList during update
    $scope.getAccIdBasedOnAccID = function (AccID) {
        for (var i = 0; i < $scope.accountList.length; i++) {
            var accId = $scope.accountList[i].ACC_ID
            if (accId == AccID) {
                return $scope.accountList[i];
            }
        } if (accId == undefined) { return null; }
    }

    //filter Selected CompanyStoreList from CompanyStoreList during update
    $scope.getSearchBasedOnSearchUnitID = function (SearchUnitID) {
        for (var i = 0; i < $scope.searchTypeList.length; i++) {
            var searId = $scope.searchTypeList[i].ID;
            if (searId == SearchUnitID) {
                return $scope.searchTypeList[i];
            }
        } if (searId == undefined) { return null; }
    }

    /*****************************************************************************************************************************/

    $scope.dirEnity = function (entity) {
        debugger;
        $scope.billSetting = entity;

        $scope.Custom.BillType = $scope.getBillTypeBasedOnBillTypeID(entity.BILL_TYPE_ID);
        $scope.Custom.SellType = $scope.getSellTypeBasedOnSellTypeID(entity.BILL_SELL_TYPE_ID);
        //$scope.SellType = 3;
        $scope.Custom.PayType = $scope.getPayTypeBasedOnPayTypeID(entity.BILL_PAY_TYPE);
        $scope.Custom.Currency = $scope.getCurrencyBasedOnCurrencyID(entity.CURRENCY_ID);
        $scope.Custom.CompanyStore = $scope.getCompanyStoreBasedOnCompanyStoreID(entity.COM_STORE_ID);
        $scope.Custom.ToCompanyStore = $scope.getToCompanyStoreBasedOnCompanyStoreID(entity.TO_COM_STORE_ID);
        $scope.Custom.CostCenter = $scope.getCostCenterBasedOnCostCenterID(entity.COST_CENTER_ID);
        $scope.Custom.SearchType = $scope.getSearchBasedOnSearchUnitID(entity.SEARCH_ONLY_BY_DEAULT_UNIT);

        $scope.Custom.AccItem = entity.ACC_ITEM_ID;
        $scope.Custom.AccDisc = entity.ACC_DISC_ID;
        $scope.Custom.AccExtra = entity.ACC_EXTRA_ID;
        $scope.Custom.AccPay = entity.ACC_PAY_ID;
        $scope.Custom.AccCost = entity.ACC_COST_ID;
        $scope.Custom.AccStore = entity.ACC_STORE_ID;
        $scope.Custom.AccGift = entity.ACC_GIFT_ID;
        $scope.Custom.AccID = entity.ACC_ID;

        $scope.Custom.AccWagesID = entity.AccWagesID;
        $scope.Custom.AccSalesID = entity.AccSalesID;
        $scope.Custom.AccVatRateID = entity.AccVatRateID;
        $scope.Custom.MenuID = entity.MenuID;

        $scope.Custom.CashAccountID = entity.CashAccountID;
        $scope.Custom.VisaAccountID = entity.VisaAccountID;
        $scope.Custom.NoPaidAccountID = entity.NoPaidAccountID;
        $scope.Custom.PaymentAccountID = entity.PaymentAccountID;

        $scope.Custom.AccWagesTaxID = entity.AccWagesTaxID;
        $scope.Custom.WagesTaxValue = entity.WagesTaxValue;

        $scope.Custom.ShowPriceTypeID = entity.ShowPriceTypeID;


        $scope.Custom.PurchasAccID = entity.PurchasAccID;
        $scope.Custom.PurchasTaxAccID = entity.PurchasTaxAccID;


        $scope.Custom.AccCommissionID = entity.AccCommissionID;
        $scope.Custom.AccCommissionTaxID = entity.AccCommissionTaxID;


        $scope.Custom.AccSalesGoldID = entity.AccSalesGoldID;
        $scope.Custom.AccPurchaseGoldID = entity.AccPurchaseGoldID;

        $scope.Custom.AccGoldID = entity.AccGoldID;

        $scope.getBillOptionData(entity);

        billGridColumn.getBySettingID(entity.BILL_SETTING_ID).then(function (result) {
            debugger;
            entity.BillGrdCol = result.data;
            $scope.getBillshowData(entity.BillGrdCol);
            $scope.getGridData(entity.BillGrdCol);
        });

        MenuService.getMenuBySettingID(entity.BILL_SETTING_ID).then(function (result) {
            if (result.data != undefined && result.data != null) {
                var menu = result.data;
                $scope.menu.menuId = menu.MenuID;
                $scope.userMenuID = menu.ID;
            }
        });
    }

    $scope.getBillOptionData = function (option) {
        $scope.billOptionList[0].Selected = option.GENERATE_ENTRY_STATE = 1;
        $scope.billOptionList[1].Selected = option.IS_AUTO_POSTING;
        $scope.billOptionList[2].Selected = option.IS_AUTO_POSTING;
        $scope.billOptionList[3].Selected = option.CONTINEOUS_INVENTORY;
        $scope.billOptionList[4].Selected = option.GENERATE_COST_ENTRY = 1;
        $scope.billOptionList[5].Selected = option.PRICE_COST_EFFECT;
        $scope.billOptionList[6].Selected = option.STORE_EFFECT_STATE = 1;
        $scope.billOptionList[7].Selected = option.STORE_EFFECT_STATE = 3;
        $scope.billOptionList[8].Selected = option.ABRV_BILL;
        $scope.billOptionList[9].Selected = option.SHOW_COST_CENTER;
        $scope.billOptionList[10].Selected = option.SHOW_EMPLOYEE;
        $scope.billOptionList[11].Selected = option.OUT_MINUS;
        $scope.billOptionList[12].Selected = option.BILL_SHORTCUT
        $scope.billOptionList[13].Selected = option.FIRST_EXPIRE;
        $scope.billOptionList[14].Selected = option.MIN_QTY;
        $scope.billOptionList[15].Selected = option.SERIAL_NUMBER;

        $scope.billOptionList[16].Selected = option.IsBillDate;
        $scope.billOptionList[17].Selected = option.IsShiftNumber;
        $scope.billOptionList[18].Selected = option.IsSellType;
        $scope.billOptionList[19].Selected = option.IsPayWay;
        $scope.billOptionList[20].Selected = option.IsAccount;
        $scope.billOptionList[21].Selected = option.IsCustAccID;
        $scope.billOptionList[22].Selected = option.IsItemAccID;
        $scope.billOptionList[23].Selected = option.IsCompStoreID;
        $scope.billOptionList[24].Selected = option.IsEmpID;
        $scope.billOptionList[25].Selected = option.IsBillRemarks;
        $scope.billOptionList[26].Selected = option.IsCurrency;
        $scope.billOptionList[27].Selected = option.IsPayTypes;
        $scope.billOptionList[28].Selected = option.IsCurrencyTrans;


        $scope.billOptionList[29].Selected = option.IsTotalExtra;
        $scope.billOptionList[30].Selected = option.IsTotalPaid;
        $scope.billOptionList[31].Selected = option.IsTotalMustPaid;
        $scope.billOptionList[32].Selected = option.IsTotalRemaining;
        $scope.billOptionList[33].Selected = option.IsToCompStore;

        $scope.billOptionList[34].Selected = option.IsItemsGroups;
        $scope.billOptionList[35].Selected = option.IsItems;
        $scope.billOptionList[36].Selected = option.Show_Wages_Discount;
        $scope.billOptionList[37].Selected = option.IsQuantityCalculated;

        $scope.billOptionList[38].Selected = option.IsShowEditReason;
        $scope.billOptionList[39].Selected = option.IsShowDeliveryPerson;


        $scope.billOptionList[40].Selected = option.IsShowExternalNumber;
        $scope.billOptionList[41].Selected = option.IsEnableTaxEdit;
        $scope.billOptionList[42].Selected = option.IsShowGoldBoxBalance;
        // $scope.billOptionList[43].Selected = option.IsShowCustomerBalance;

        $scope.billOptionList[43].Selected = option.IsCalcClearnessRate;

        $scope.billOptionList[44].Selected = option.IsBillDiscRate;
        $scope.billOptionList[45].Selected = option.IsBillDiscValue;
        $scope.billOptionList[46].Selected = option.IsTotalDiscValues;
        $scope.billOptionList[47].Selected = option.IsTotalDiscRates;

        $scope.billOptionList[48].Selected = option.IsShowTaxesBox;

        $scope.billOptionList[49].Selected = option.IsEnableWeight;
        $scope.billOptionList[50].Selected = option.IsEnableGmWages;
        $scope.billOptionList[51].Selected = option.IsLockPrice; 
    };

    $scope.getBillshowData = function (shwOpt) {
        for (var i = 0; i < $scope.billShowList.length; i++) {
            if ($scope.billShowList[i].BILL_SHOW_ID === 1) {
                $scope.billShowList[i].INDEX = shwOpt.ITEM_INDEX;
            } else if ($scope.billShowList[i].BILL_SHOW_ID === 2) {
                $scope.billShowList[i].INDEX = shwOpt.QTY_INDEX;
            } else if ($scope.billShowList[i].BILL_SHOW_ID === 3) {
                $scope.billShowList[i].INDEX = shwOpt.UNIT_COST_INDEX;
            } else if ($scope.billShowList[i].BILL_SHOW_ID === 4) {
                $scope.billShowList[i].INDEX = shwOpt.TOTAL_INDEX;
            } else if ($scope.billShowList[i].BILL_SHOW_ID === 5) {
                $scope.billShowList[i].INDEX = shwOpt.GIFTS_INDEX;
            } else if ($scope.billShowList[i].BILL_SHOW_ID === 6) {
                $scope.billShowList[i].INDEX = shwOpt.DISC_RATE_INDEX;
            } else if ($scope.billShowList[i].BILL_SHOW_ID === 7) {
                $scope.billShowList[i].INDEX = shwOpt.DISC_VALUE_INDEX;
            } else if ($scope.billShowList[i].BILL_SHOW_ID === 8) {
                $scope.billShowList[i].INDEX = shwOpt.EXTRA_RATE_INDEX;
            } else if ($scope.billShowList[i].BILL_SHOW_ID === 9) {
                $scope.billShowList[i].INDEX = shwOpt.EXTRA_VALUE_INDEX;
            } else if ($scope.billShowList[i].BILL_SHOW_ID === 10) {
                $scope.billShowList[i].INDEX = shwOpt.PRODUCTION_DATE_INDEX;
            } else if ($scope.billShowList[i].BILL_SHOW_ID === 11) {
                $scope.billShowList[i].INDEX = shwOpt.EXPIRED_DATE_INDEX;
            } else if ($scope.billShowList[i].BILL_SHOW_ID === 12) {
                $scope.billShowList[i].INDEX = shwOpt.BILL_REMARKS_INDEX;
            } else if ($scope.billShowList[i].BILL_SHOW_ID === 13) {
                $scope.billShowList[i].INDEX = shwOpt.UNIT_INDEX;
            } else if ($scope.billShowList[i].BILL_SHOW_ID === 14) {
                $scope.billShowList[i].INDEX = shwOpt.ITEM_REMAIN_QTY_INDEX;
            } else if ($scope.billShowList[i].BILL_SHOW_ID === 15) {
                $scope.billShowList[i].INDEX = shwOpt.CTOTALCURR_INDEX;
            } else if ($scope.billShowList[i].BILL_SHOW_ID === 16) {
                $scope.billShowList[i].INDEX = shwOpt.CEXTERNALLEXPENSES_INDEX;
            } else if ($scope.billShowList[i].BILL_SHOW_ID === 17) {
                $scope.billShowList[i].INDEX = shwOpt.CTOTALWITHEXPENSES_INDEX;
            } else if ($scope.billShowList[i].BILL_SHOW_ID === 18) {
                $scope.billShowList[i].INDEX = shwOpt.CINVENTORYVALUE_INDEX;
            } else if ($scope.billShowList[i].BILL_SHOW_ID === 19) {
                $scope.billShowList[i].INDEX = shwOpt.WHOLE_INDEX;
            } else if ($scope.billShowList[i].BILL_SHOW_ID === 20) {
                $scope.billShowList[i].INDEX = shwOpt.HALF_WHOLE_INDEX;
            } else if ($scope.billShowList[i].BILL_SHOW_ID === 21) {
                $scope.billShowList[i].INDEX = shwOpt.RETAIL_INDEX;
            } else if ($scope.billShowList[i].BILL_SHOW_ID === 22) {
                $scope.billShowList[i].INDEX = shwOpt.END_USERS_INDEX;
            } else if ($scope.billShowList[i].BILL_SHOW_ID === 23) {
                $scope.billShowList[i].INDEX = shwOpt.EMPLOYEE_INDEX;
            } else if ($scope.billShowList[i].BILL_SHOW_ID === 24) {
                $scope.billShowList[i].INDEX = shwOpt.EXPORT_INDEX;
            } else if ($scope.billShowList[i].BILL_SHOW_ID === 25) {
                $scope.billShowList[i].INDEX = shwOpt.LAST_BUY_INDEX;
            }

            else if ($scope.billShowList[i].BILL_SHOW_ID === 26) {
                $scope.billShowList[i].INDEX = shwOpt.Cost_Center_INDEX;
            }

            else if ($scope.billShowList[i].BILL_SHOW_ID === 27) {
                $scope.billShowList[i].INDEX = shwOpt.Item_Weight_INDEX;
            }
            else if ($scope.billShowList[i].BILL_SHOW_ID === 28) {
                $scope.billShowList[i].INDEX = shwOpt.Item_Wages_INDEX;
            }
            else if ($scope.billShowList[i].BILL_SHOW_ID === 29) {
                $scope.billShowList[i].INDEX = shwOpt.Manufact_Wages_INDEX;
            }
            else if ($scope.billShowList[i].BILL_SHOW_ID === 30) {
                $scope.billShowList[i].INDEX = shwOpt.ARName_INDEX;
            }
            else if ($scope.billShowList[i].BILL_SHOW_ID === 31) {
                $scope.billShowList[i].INDEX = shwOpt.Clearness_Rate_INDEX;
            }
            else if ($scope.billShowList[i].BILL_SHOW_ID === 32) {
                $scope.billShowList[i].INDEX = shwOpt.Caliber24_INDEX;
            }
            else if ($scope.billShowList[i].BILL_SHOW_ID === 33) {
                $scope.billShowList[i].INDEX = shwOpt.Caliber22_INDEX;
            }
            else if ($scope.billShowList[i].BILL_SHOW_ID === 34) {
                $scope.billShowList[i].INDEX = shwOpt.Caliber21_INDEX;
            }
            else if ($scope.billShowList[i].BILL_SHOW_ID === 35) {
                $scope.billShowList[i].INDEX = shwOpt.Caliber18_INDEX;
            }
            else if ($scope.billShowList[i].BILL_SHOW_ID === 36) {
                $scope.billShowList[i].INDEX = shwOpt.Subject_To_VAT_INDEX;
            }
            else if ($scope.billShowList[i].BILL_SHOW_ID === 37) {
                $scope.billShowList[i].INDEX = shwOpt.VAT_Rate_INDEX;
            }

            else if ($scope.billShowList[i].BILL_SHOW_ID === 38) {
                $scope.billShowList[i].INDEX = shwOpt.Currency_INDEX;
            }

            else if ($scope.billShowList[i].BILL_SHOW_ID === 39) {
                $scope.billShowList[i].INDEX = shwOpt.Inventory_INDEX;
            }


            else if ($scope.billShowList[i].BILL_SHOW_ID === 40) {
                $scope.billShowList[i].INDEX = shwOpt.GoldBox_INDEX;
            }

            else if ($scope.billShowList[i].BILL_SHOW_ID === 41) {
                $scope.billShowList[i].INDEX = shwOpt.BillCostCenter_INDEX;
            }

            else if ($scope.billShowList[i].BILL_SHOW_ID === 42) {
                $scope.billShowList[i].INDEX = shwOpt.PayTypes_INDEX;
            }

            else if ($scope.billShowList[i].BILL_SHOW_ID === 43) {
                $scope.billShowList[i].INDEX = shwOpt.CaliberTrans_INDEX;
            }

            else if ($scope.billShowList[i].BILL_SHOW_ID === 44) {
                $scope.billShowList[i].INDEX = shwOpt.VatValue_INDEX;
            }

            else if ($scope.billShowList[i].BILL_SHOW_ID === 45) {
                $scope.billShowList[i].INDEX = shwOpt.WagesDiscValue_INDEX;
            }

            else if ($scope.billShowList[i].BILL_SHOW_ID === 46) {
                $scope.billShowList[i].INDEX = shwOpt.WagesDiscRate_INDEX;
            }


            else if ($scope.billShowList[i].BILL_SHOW_ID === 47) {
                $scope.billShowList[i].INDEX = shwOpt.ActualWeight_INDEX;
            }


            else if ($scope.billShowList[i].BILL_SHOW_ID === 48) {
                $scope.billShowList[i].INDEX = shwOpt.TaxTotal_INDEX;
            }

            else if ($scope.billShowList[i].BILL_SHOW_ID === 49) {
                $scope.billShowList[i].INDEX = shwOpt.TotalItemGmWages_INDEX;
            }


            else if ($scope.billShowList[i].BILL_SHOW_ID === 50) {
                $scope.billShowList[i].INDEX = shwOpt.TotalGoldManufact_INDEX;
            }

            else if ($scope.billShowList[i].BILL_SHOW_ID === 51) {
                $scope.billShowList[i].INDEX = shwOpt.ExemptOfTax_INDEX;
            }


            else if ($scope.billShowList[i].BILL_SHOW_ID === 52) {
                $scope.billShowList[i].INDEX = shwOpt.ItemPrice_INDEX;
            }

            else if ($scope.billShowList[i].BILL_SHOW_ID === 53) {
                $scope.billShowList[i].INDEX = shwOpt.AfterDiscount_INDEX;
            }
            else if ($scope.billShowList[i].BILL_SHOW_ID === 54) {
                $scope.billShowList[i].INDEX = shwOpt.LockPrice_INDEX;
            }

        }

        $scope.billShowList.sort(function (a, b) {
            if (a.INDEX === null || a.INDEX === 0) {
                return 1;
            }
            else if (b.INDEX === null || b.INDEX === 0) {
                return -1;
            }
            else if (a.INDEX === b.INDEX) {
                return 0;
            }
            else {
                return parseFloat(a.INDEX) - parseFloat(b.INDEX);
            }
        })
        for (var i = 0; i < $scope.billShowList.length; i++) {
            if ($scope.billShowList[i].INDEX > 0 && $scope.billShowList[i].INDEX != null) {
                $scope.billShowList[i].Selected = true;
            } else {
                $scope.billShowList[i].Selected = false;
            }
        }
    };

    $scope.deleteEnity = function (entity) {
        swal({
            title: 'هل تريد حذف اعدادات الفواتير؟',
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
        }, function (dismiss) {
            // dismiss can be 'cancel', 'overlay',
            // 'close', and 'timer'
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


    $scope.getbillSettingTotalCount = function () {
        $scope.count().then(function (results) {
            var data = results.data;
            $scope.billSettingTotalCount = data;
            $scope.pagesCount = $scope.getPagesCount();
        }, function (error) {
            console.log(error.data.message);
        });
    }
    $scope.getbillSettingList = function () {
        $scope.get($scope.pageNum, $scope.pageSize).then(function (results) {
            $scope.billSettingList = results.data;
        }, function (error) {
            console.log(error.data.message);
        });
    }


    $scope.getSellTypeList = function () {
        salesTypeService.getAll().then(function (results) {
            $scope.SellTypeList = results.data;
        }, function (error) {
            console.log(error.data.message);
        });
    }

    $scope.getBillTypeList = function () {
        billTypeService.getAll().then(function (results) {
            $scope.billTypeList = results.data;
        }, function (error) {
            console.log(error.data.message);
        });
    }

    $scope.getPayTypeList = function () {
        payTypeService.getAll().then(function (results) {
            $scope.payTypeList = results.data;
        }, function (error) {
            console.log(error.data.message);
        });
    }



    $scope.getbillShowList = function () {
        billShowOptionService.getAll().then(function (results) {
            $scope.billShowList = {};
            $scope.gridOptions.data = {};
            var temp = [];
            var data = results.data;
            var flag = !$scope.getByUserModel(8);
            for (var i = 0; i < data.length; i++) {
                if (flag || $scope.GoldSetting ==false) {

                    if (data[i].BILL_SHOW_EN_NAME == "ItemWeight" || data[i].BILL_SHOW_EN_NAME == "ItemGmWages" ||
                        data[i].BILL_SHOW_EN_NAME == "ManufacturingWages" || data[i].BILL_SHOW_EN_NAME == "ARName"
                        || data[i].BILL_SHOW_EN_NAME == "ClearnessRate" || data[i].BILL_SHOW_EN_NAME == "Caliber24"
                        || data[i].BILL_SHOW_EN_NAME == "Caliber22" || data[i].BILL_SHOW_EN_NAME == "Caliber21"
                        || data[i].BILL_SHOW_EN_NAME == "Caliber18" || data[i].BILL_SHOW_EN_NAME == "Wages Discount Value"
                        || data[i].BILL_SHOW_EN_NAME == "Wages Discount Rate" || data[i].BILL_SHOW_EN_NAME == "Actual Weight"
                        || data[i].BILL_SHOW_EN_NAME == "Total Item Gm Wages" || data[i].BILL_SHOW_EN_NAME == "Total gold and manufacturing amounts") {
                        temp.push(data[i]);
                        temp[temp.length - 1].isShow = false;
                    } else {
                        temp.push(data[i]);
                        temp[temp.length - 1].Selected = true;
                        temp[temp.length - 1].isShow = true;
                    }

                } else {
                    temp.push(data[i]);
                    temp[temp.length - 1].Selected = true;
                    temp[temp.length - 1].isShow = true;
                }
               
            }
            $scope.gridOptions.data = temp;
            $scope.billShowList = temp;
            return $scope.billShowList;

        }, function (error) {
            console.log(error.data.message);
        });
    }

    $scope.getbillOptionList = function () {
        billOptionService.getAll().then(function (results) {
            debugger
          //  BILL_OPTION_EN_NAME: "Calculate CleanessRate"  "Enable Gm wages"  "Show Item Account" ""
            var flag = !$scope.getByUserModel(8);
           // var flag = true;
            $scope.billOptionList = [];
            var data = results.data;
            for (var i = 0; i < data.length; i++) {
                if (flag || $scope.GoldSetting == false) {
                    debugger;
                    if (data[i].BILL_OPTION_EN_NAME == "Calculate CleanessRate" || data[i].BILL_OPTION_EN_NAME == "Enable Gm wages" ||
                        data[i].BILL_OPTION_EN_NAME == "Show Item Account" || data[i].BILL_OPTION_EN_NAME == "Enable weight edit") {
                        $scope.billOptionList.push(data[i]);
                        $scope.billOptionList[i].isShow = false;
                    } else {
                        $scope.billOptionList.push(data[i]);
                        $scope.billOptionList[i].isShow = true;
                    }

                } else {
                    $scope.billOptionList.push(data[i]);
                 
                }
            }
           

        }, function (error) {
            console.log(error.data.message);
        });
    }

    $scope.getCostCenterList = function () {
        costCentersService.getAll().then(function (results) {
            $scope.CostCenterList = results.data;
        }, function (error) {
            console.log(error.data.message);
        });
    }


    $scope.getCompanyStoresList = function () {
        companyStoresService.getAll().then(function (results) {
            $scope.CompanyStoresList = results.data;
        }, function (error) {
            console.log(error.data.message);
        });
    }

    $scope.getCurrencyList = function () {
        currencyService.getAll().then(function (results) {
            $scope.CurrencyList = results.data;
        }, function (error) {
            console.log(error.data.message);
        });
    }

    $scope.$watch('Custom.Currency', function (newvalue, oldvalue) {
        $scope.getCurrencyRate();
    });
    $scope.getCurrencyRate = function () {
        if ($scope.Custom.Currency != "" && $scope.Custom.Currency != undefined) {
            var curID = $scope.Custom.Currency.CURRENCY_ID;
            if (curID != "" && curID != undefined)
                currencyService.getCurrencyRate(curID).then(
                    function (result) {
                        $scope.billSetting.CURRENCY_RATE = result.data;
                    }, function (error) {
                        console.log(error.data.message);
                    });
            else $scope.billSetting.CURRENCY_RATE = "";
        } else $scope.billSetting.CURRENCY_RATE = "";
    };
    $scope.flagHide = false;
    $scope.getAllOnLoad = function () {
        $q.all([
            
            $scope.getSettingType(),
            $scope.getbillSettingList(),
            $scope.getbillSettingTotalCount(),
            $scope.getSellTypeList(),
            $scope.getBillTypeList(),
            $scope.getPayTypeList(),
            $scope.getAccountList(),
            $scope.getbillShowList(),
            $scope.getbillOptionList(),
            $scope.getCostCenterList(),
            $scope.getCompanyStoresList(),
            $scope.getCurrencyList(),
            $scope.getMainMenus(),

            $scope.getPriceList(),

            $scope.getCustAccountList(),
            $scope.getCommTaxAccountList(),
            $scope.getCommAccountList(),
            $scope.getPurchasTaxAccountList(),
            $scope.getPurchasAccountList(),
            $scope.getVatAccountList(),
            $scope.getSalesAccountList(),
            $scope.getWagesAccountList(),
            $scope.getGiftAccountList(),
            $scope.getStoreAccountList(),
            $scope.getCostAccountList(),
            $scope.getPayAccountList(),
            $scope.getExtraAccountList(),
            $scope.getOtherAccountList(),
            $scope.getGoldAccountList(),
            $scope.getUserModel()

        ]).then(function (allResponses) {
            //$scope.clearEnity();
            debugger 
            var flag = !$scope.getByUserModel(8);
            debugger;
            if (flag || $scope.GoldSetting == false) { $scope.flagHide = true }

            var urlParams = $location.search();
            if (urlParams.foo != null && urlParams.foo != undefined && urlParams.foo != "") {
                debugger;
                billSettingService.getBillSettingByBillID(parseInt(urlParams.foo)).then(function (results) {
                    debugger;
                    $scope.billSetting = results.data;

                    $scope.dirEnity(results.data);
                })

                //$.each($scope.billSettingList, function (index, value) {
                //    //alert(value.BILL_SETTING_ID );
                //    if (value.BILL_SETTING_ID == parseInt(urlParams.foo)) {
                //        //alert();
                //        $scope.billSetting = value;
                //        $scope.dirEnity(value);
                //    }
                //});

            }

        }, function (error) {
            //This will be called if $q.all finds any of the requests erroring.
            var abc = error;
            var def = 99;
        });
    };

    $scope.formatLabel = function (model) {
        for (var i = 0; i < $scope.SellTypeList.length; i++) {
            if (model === $scope.SellTypeList[i].SELL_TYPE_ID) {
                return $scope.SellTypeList[i].SELL_TYPE_AR_NAME;
            }
        }
    }

    $scope.get = function (pageNum, pageSize) {
        return billSettingService.get(pageNum, pageSize);
    }
    $scope.count = function () {
        return billSettingService.count();
    }
    $scope.insert = function (entity) {
        return billSettingService.insert(entity);
    }
    $scope.update = function (entity) {
        return billSettingService.update(entity);
    }
    $scope.delete = function (entity) {
        return billSettingService.delete(entity);
    }
    $scope.billSettingPageLoad = function () {
        $scope.getAllOnLoad();
    }

    $scope.pageChanged = function () {
        $scope.getAllOnLoad();
        $log.log('Page changed to: ' + $scope.pageNum);
    };


    /*******************************************************************************************************************************/

    $scope.steps = [
        'بيانات الفاتوره',
        'اعدادات طباعة الفاتورة',
        'القيم الافتراضيه',
        'الحسابات الافتراضية',
        'خيارات الفاتوره',
        'خيارات الاظهار',
        'بيانات الاظهار',
    ];
    $scope.selection = $scope.steps[0];

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
    ////////////////////////////////////////////////////////////////////////////////////////
    /////set grid data 
    $scope.setGridData = function () {
        if ($scope.gridData.ARCode == "" || $scope.gridData.ARCode == undefined) {
            $scope.billSetting.BillGrdCol.ARCode = $scope.defaultData.arCode;
        }
        else
        { $scope.billSetting.BillGrdCol.ARCode = $scope.gridData.ARCode; }


        if ($scope.gridData.ARItem == "" || $scope.gridData.ARItem == undefined) {
            $scope.billSetting.BillGrdCol.ARItem = $scope.defaultData.arItem;
        }
        else { $scope.billSetting.BillGrdCol.ARItem = $scope.gridData.ARItem; }


        if ($scope.gridData.ARQuantity == "" || $scope.gridData.ARQuantity == undefined) {
            $scope.billSetting.BillGrdCol.ARQuantity = $scope.defaultData.arQuantity;
        }
        else { $scope.billSetting.BillGrdCol.ARQuantity = $scope.gridData.ARQuantity; }


        if ($scope.gridData.ARItemWeight == "" || $scope.gridData.ARItemWeight == undefined) {
            $scope.billSetting.BillGrdCol.ARItemWeight = $scope.defaultData.arItemWeight;
        }
        else { $scope.billSetting.BillGrdCol.ARItemWeight = $scope.gridData.ARItemWeight; }


        if ($scope.gridData.ARItemGmWages == "" || $scope.gridData.ARItemGmWages == undefined) {
            $scope.billSetting.BillGrdCol.ARItemGmWages = $scope.defaultData.arItemGmWages;
        }
        else { $scope.billSetting.BillGrdCol.ARItemGmWages = $scope.gridData.ARItemGmWages; }


        if ($scope.gridData.ARManufacturingWages == "" || $scope.gridData.ARManufacturingWages == undefined) {
            $scope.billSetting.BillGrdCol.ARManufacturingWages = $scope.defaultData.arManfactWages;
        }
        else { $scope.billSetting.BillGrdCol.ARManufacturingWages = $scope.gridData.ARManufacturingWages; }


        if ($scope.gridData.ARUnit == "" || $scope.gridData.ARUnit == undefined) {
            $scope.billSetting.BillGrdCol.ARUnit = $scope.defaultData.arUnit;
        }
        else { $scope.billSetting.BillGrdCol.ARUnit = $scope.gridData.ARUnit; }


        if ($scope.gridData.ARCaliberName == "" || $scope.gridData.ARCaliberName == undefined) {
            $scope.billSetting.BillGrdCol.ARCaliberName = $scope.defaultData.arCaliberName;
        }
        else { $scope.billSetting.BillGrdCol.ARCaliberName = $scope.gridData.ARCaliberName; }


        if ($scope.gridData.ARPrice == "" || $scope.gridData.ARPrice == undefined) {
            $scope.billSetting.BillGrdCol.ARPrice = $scope.defaultData.arPrice;
        }
        else { $scope.billSetting.BillGrdCol.ARPrice = $scope.gridData.ARPrice; }


        if ($scope.gridData.ARDiscount == "" || $scope.gridData.ARDiscount == undefined) {
            $scope.billSetting.BillGrdCol.ARDiscount = $scope.defaultData.arDiscount;
        }
        else { $scope.billSetting.BillGrdCol.ARDiscount = $scope.gridData.ARDiscount; }


        if ($scope.gridData.ARDiscRate == "" || $scope.gridData.ARDiscRate == undefined) {
            $scope.billSetting.BillGrdCol.ARDiscRate = $scope.defaultData.arDiscRate;
        }
        else { $scope.billSetting.BillGrdCol.ARDiscRate = $scope.gridData.ARDiscRate; }


        if ($scope.gridData.ARTotal == "" || $scope.gridData.ARTotal == undefined) {
            $scope.billSetting.BillGrdCol.ARTotal = $scope.defaultData.arTotal;
        }
        else { $scope.billSetting.BillGrdCol.ARTotal = $scope.gridData.ARTotal; }


        if ($scope.gridData.ARClearnessRate == "" || $scope.gridData.ARClearnessRate == undefined) {
            $scope.billSetting.BillGrdCol.ARClearnessRate = $scope.defaultData.arClearnessRate;
        }
        else { $scope.billSetting.BillGrdCol.ARClearnessRate = $scope.gridData.ARClearnessRate; }


        if ($scope.gridData.ARCaliber24 == "" || $scope.gridData.ARCaliber24 == undefined) {
            $scope.billSetting.BillGrdCol.ARCaliber24 = $scope.defaultData.arCaliber24;
        }
        else { $scope.billSetting.BillGrdCol.ARCaliber24 = $scope.gridData.ARCaliber24; }


        if ($scope.gridData.ARCaliber22 == "" || $scope.gridData.ARCaliber22 == undefined) {
            $scope.billSetting.BillGrdCol.ARCaliber22 = $scope.defaultData.arCaliber22;
        }
        else { $scope.billSetting.BillGrdCol.ARCaliber22 = $scope.gridData.ARCaliber22; }


        if ($scope.gridData.ARCaliber21 == "" || $scope.gridData.ARCaliber21 == undefined) {
            $scope.billSetting.BillGrdCol.ARCaliber21 = $scope.defaultData.arCaliber22;
        }
        else { $scope.billSetting.BillGrdCol.ARCaliber21 = $scope.gridData.ARCaliber21; }


        if ($scope.gridData.ARCaliber18 == "" || $scope.gridData.ARCaliber18 == undefined) {
            $scope.billSetting.BillGrdCol.ARCaliber18 = $scope.defaultData.arCaliber18;
        }
        else { $scope.billSetting.BillGrdCol.ARCaliber18 = $scope.gridData.ARCaliber18; }


        if ($scope.gridData.ARCostCenter == "" || $scope.gridData.ARCostCenter == undefined) {
            $scope.billSetting.BillGrdCol.ARCostCenter = $scope.defaultData.arCostCenter;
        }
        else { $scope.billSetting.BillGrdCol.ARCostCenter = $scope.gridData.ARCostCenter; }


        if ($scope.gridData.ARSubjectToVat == "" || $scope.gridData.ARSubjectToVat == undefined) {
            $scope.billSetting.BillGrdCol.ARSubjectToVat = $scope.defaultData.arSubjectToVat;
        }
        else { $scope.billSetting.BillGrdCol.ARSubjectToVat = $scope.gridData.ARSubjectToVat; }


        if ($scope.gridData.ARVatRate == "" || $scope.gridData.ARVatRate == undefined) {
            $scope.billSetting.BillGrdCol.ARVatRate = $scope.defaultData.arVatRate;
        }
        else { $scope.billSetting.BillGrdCol.ARVatRate = $scope.gridData.ARVatRate; }


        if ($scope.gridData.ARVatValue == "" || $scope.gridData.ARVatValue == undefined) {
            $scope.billSetting.BillGrdCol.ARVatValue = $scope.defaultData.arVatValue;
        }
        else { $scope.billSetting.BillGrdCol.ARVatValue = $scope.gridData.ARVatValue; }


        if ($scope.gridData.ARWagesDiscValue == "" || $scope.gridData.ARWagesDiscValue == undefined) {
            $scope.billSetting.BillGrdCol.ARWagesDiscValue = $scope.defaultData.arWagesDiscValue;
        }
        else { $scope.billSetting.BillGrdCol.ARWagesDiscValue = $scope.gridData.ARWagesDiscValue; }


        if ($scope.gridData.ARWagesDiscRate == "" || $scope.gridData.ARWagesDiscRate == undefined) {
            $scope.billSetting.BillGrdCol.ARWagesDiscRate = $scope.defaultData.arWagesDiscRate;
        }
        else { $scope.billSetting.BillGrdCol.ARWagesDiscRate = $scope.gridData.ARWagesDiscRate; }



        if ($scope.gridData.ARActualWeight == "" || $scope.gridData.ARActualWeight == undefined) {
            $scope.billSetting.BillGrdCol.ARActualWeight = $scope.defaultData.arActualWeight;
        }
        else { $scope.billSetting.BillGrdCol.ARActualWeight = $scope.gridData.ARActualWeight; }


        if ($scope.gridData.ARTotalItemGmWages == "" || $scope.gridData.ARTotalItemGmWages == undefined) {
            $scope.billSetting.BillGrdCol.ARTotalItemGmWages = $scope.defaultData.arTotalItemGmWages;
        }
        else { $scope.billSetting.BillGrdCol.ARTotalItemGmWages = $scope.gridData.ARTotalItemGmWages; }


        if ($scope.gridData.ARTotalGoldManufact == "" || $scope.gridData.ARTotalGoldManufact == undefined) {
            $scope.billSetting.BillGrdCol.ARTotalGoldManufact = $scope.defaultData.arTotalGoldManufact;
        }
        else { $scope.billSetting.BillGrdCol.ARTotalGoldManufact = $scope.gridData.ARTotalGoldManufact; }


        if ($scope.gridData.ARExemptOfTax == "" || $scope.gridData.ARExemptOfTax == undefined) {
            $scope.billSetting.BillGrdCol.ARExemptOfTax = $scope.defaultData.arExemptOfTax;
        }
        else { $scope.billSetting.BillGrdCol.ARExemptOfTax = $scope.gridData.ARExemptOfTax; }


        if ($scope.gridData.ARItemPrice == "" || $scope.gridData.ARItemPrice == undefined) {
            $scope.billSetting.BillGrdCol.ARItemPrice = $scope.defaultData.arItemPrice;
        }
        else { $scope.billSetting.BillGrdCol.ARItemPrice = $scope.gridData.ARItemPrice; }


        if ($scope.gridData.ARAfterDiscount == "" || $scope.gridData.ARAfterDiscount == undefined) {
            $scope.billSetting.BillGrdCol.ARAfterDiscount = $scope.defaultData.arAfterDiscount;
        }
        else { $scope.billSetting.BillGrdCol.ARAfterDiscount = $scope.gridData.ARAfterDiscount; }

        debugger;
        if ($scope.gridData.ARLockPrice == "" || $scope.gridData.ARLockPrice == undefined) {
            $scope.billSetting.BillGrdCol.ARLockPrice = $scope.defaultData.ARLockPrice;
        }
        else { $scope.billSetting.BillGrdCol.ARLockPrice = $scope.gridData.ARLockPrice; }
        /////////////////////////////////////////////////////////////////////english


        if ($scope.gridData.ENCode == "" || $scope.gridData.ENCode == undefined) {
            $scope.billSetting.BillGrdCol.ENCode = $scope.defaultData.enCode;
        }
        else { $scope.billSetting.BillGrdCol.ENCode = $scope.gridData.ENCode; }


        if ($scope.gridData.ENItem == "" || $scope.gridData.ENItem == undefined) {
            $scope.billSetting.BillGrdCol.ENItem = $scope.defaultData.enItem;
        }
        else { $scope.billSetting.BillGrdCol.ENItem = $scope.gridData.ENItem; }


        if ($scope.gridData.ENQuantity == "" || $scope.gridData.ENQuantity == undefined) {
            $scope.billSetting.BillGrdCol.ENQuantity = $scope.defaultData.enQuantity;
        }
        else { $scope.billSetting.BillGrdCol.ENQuantity = $scope.gridData.ENQuantity; }


        if ($scope.gridData.ENItemWeight == "" || $scope.gridData.ENItemWeight == undefined) {
            $scope.billSetting.BillGrdCol.ENItemWeight = $scope.defaultData.enItemWeight;
        }
        else { $scope.billSetting.BillGrdCol.ENItemWeight = $scope.gridData.ENItemWeight; }


        if ($scope.gridData.ENItemGmWages == "" || $scope.gridData.ENItemGmWages == undefined) {
            $scope.billSetting.BillGrdCol.ENItemGmWages = $scope.defaultData.enItemGmWages;
        }
        else { $scope.billSetting.BillGrdCol.ENItemGmWages = $scope.gridData.ENItemGmWages; }


        if ($scope.gridData.ENManufacturingWages == "" || $scope.gridData.ENManufacturingWages == undefined) {
            $scope.billSetting.BillGrdCol.ENManufacturingWages = $scope.defaultData.enManfactWages;
        }
        else { $scope.billSetting.BillGrdCol.ENManufacturingWages = $scope.gridData.ENManufacturingWages; }


        if ($scope.gridData.ENUnit == "" || $scope.gridData.ENUnit == undefined) {
            $scope.billSetting.BillGrdCol.ENUnit = $scope.defaultData.enUnit;
        }
        else { $scope.billSetting.BillGrdCol.ENUnit = $scope.gridData.ENUnit; }


        if ($scope.gridData.ENCaliberName == "" || $scope.gridData.ENCaliberName == undefined) {
            $scope.billSetting.BillGrdCol.ENCaliberName = $scope.defaultData.enCaliberName;
        }
        else { $scope.billSetting.BillGrdCol.ENCaliberName = $scope.gridData.ENCaliberName; }


        if ($scope.gridData.ENPrice == "" || $scope.gridData.ENPrice == undefined) {
            $scope.billSetting.BillGrdCol.ENPrice = $scope.defaultData.enPrice;
        }
        else { $scope.billSetting.BillGrdCol.ENPrice = $scope.gridData.ENPrice; }


        if ($scope.gridData.ENDiscount == "" || $scope.gridData.ENDiscount == undefined) {
            $scope.billSetting.BillGrdCol.ENDiscount = $scope.defaultData.enDiscount;
        }
        else { $scope.billSetting.BillGrdCol.ENDiscount = $scope.gridData.ENDiscount; }


        if ($scope.gridData.ENDiscRate == "" || $scope.gridData.ENDiscRate == undefined) {
            $scope.billSetting.BillGrdCol.ENDiscRate = $scope.defaultData.enDiscRate;
        }
        else { $scope.billSetting.BillGrdCol.ENDiscRate = $scope.gridData.ENDiscRate; }


        if ($scope.gridData.ENTotal == "" || $scope.gridData.ENTotal == undefined) {
            $scope.billSetting.BillGrdCol.ENTotal = $scope.defaultData.enTotal;
        }
        else { $scope.billSetting.BillGrdCol.ENTotal = $scope.gridData.ENTotal; }


        if ($scope.gridData.ENClearnessRate == "" || $scope.gridData.ENClearnessRate == undefined) {
            $scope.billSetting.BillGrdCol.ENClearnessRate = $scope.defaultData.enClearnessRate;
        }
        else { $scope.billSetting.BillGrdCol.ENClearnessRate = $scope.gridData.ENClearnessRate; }


        if ($scope.gridData.ENCaliber24 == "" || $scope.gridData.ENCaliber24 == undefined) {
            $scope.billSetting.BillGrdCol.ENCaliber24 = $scope.defaultData.enCaliber24;
        }
        else { $scope.billSetting.BillGrdCol.ENCaliber24 = $scope.gridData.ENCaliber24; }


        if ($scope.gridData.ENCaliber22 == "" || $scope.gridData.ENCaliber22 == undefined) {
            $scope.billSetting.BillGrdCol.ENCaliber22 = $scope.defaultData.enCaliber22;
        }
        else { $scope.billSetting.BillGrdCol.ENCaliber22 = $scope.gridData.ENCaliber22; }


        if ($scope.gridData.ENCaliber21 == "" || $scope.gridData.ENCaliber21 == undefined) {
            $scope.billSetting.BillGrdCol.ENCaliber21 = $scope.defaultData.enCaliber22;
        }
        else { $scope.billSetting.BillGrdCol.ENCaliber21 = $scope.gridData.ENCaliber21; }


        if ($scope.gridData.ENCaliber18 == "" || $scope.gridData.ENCaliber18 == undefined) {
            $scope.billSetting.BillGrdCol.ENCaliber18 = $scope.defaultData.enCaliber18;
        }
        else { $scope.billSetting.BillGrdCol.ENCaliber18 = $scope.gridData.ENCaliber18; }


        if ($scope.gridData.ENCostCenter == "" || $scope.gridData.ENCostCenter == undefined) {
            $scope.billSetting.BillGrdCol.ENCostCenter = $scope.defaultData.enCostCenter;
        }
        else { $scope.billSetting.BillGrdCol.ENCostCenter = $scope.gridData.ENCostCenter; }


        if ($scope.gridData.ENSubjectToVat == "" || $scope.gridData.ENSubjectToVat == undefined) {
            $scope.billSetting.BillGrdCol.ENSubjectToVat = $scope.defaultData.enSubjectToVat;
        }
        else { $scope.billSetting.BillGrdCol.ENSubjectToVat = $scope.gridData.ENSubjectToVat; }


        if ($scope.gridData.ENVatRate == "" || $scope.gridData.ENVatRate == undefined) {
            $scope.billSetting.BillGrdCol.ENVatRate = $scope.defaultData.enVatRate;
        }
        else { $scope.billSetting.BillGrdCol.ENVatRate = $scope.gridData.ENVatRate; }

        if ($scope.gridData.ENVatValue == "" || $scope.gridData.ENVatValue == undefined) {
            $scope.billSetting.BillGrdCol.ENVatValue = $scope.defaultData.enVatValue;
        }
        else { $scope.billSetting.BillGrdCol.ENVatValue = $scope.gridData.ENVatValue; }



        if ($scope.gridData.ENWagesDiscValue == "" || $scope.gridData.ENWagesDiscValue == undefined) {
            $scope.billSetting.BillGrdCol.ENWagesDiscValue = $scope.defaultData.ENWagesDiscValue;
        }
        else { $scope.billSetting.BillGrdCol.ENWagesDiscValue = $scope.gridData.ENWagesDiscValue; }


        if ($scope.gridData.ENWagesDiscRate == "" || $scope.gridData.ENWagesDiscRate == undefined) {
            $scope.billSetting.BillGrdCol.ENWagesDiscRate = $scope.defaultData.ENWagesDiscRate;
        }
        else { $scope.billSetting.BillGrdCol.ENWagesDiscRate = $scope.gridData.ENWagesDiscRate; }


        if ($scope.gridData.ENActualWeight == "" || $scope.gridData.ENActualWeight == undefined) {
            $scope.billSetting.BillGrdCol.ENActualWeight = $scope.defaultData.enActualWeight;
        }
        else { $scope.billSetting.BillGrdCol.ENActualWeight = $scope.gridData.ENActualWeight; }


        if ($scope.gridData.ENTotalItemGmWages == "" || $scope.gridData.ENTotalItemGmWages == undefined) {
            $scope.billSetting.BillGrdCol.ENTotalItemGmWages = $scope.defaultData.enTotalItemGmWages;
        }
        else { $scope.billSetting.BillGrdCol.ENTotalItemGmWages = $scope.gridData.ENTotalItemGmWages; }


        if ($scope.gridData.ENTotalGoldManufact == "" || $scope.gridData.ENTotalGoldManufact == undefined) {
            $scope.billSetting.BillGrdCol.ENTotalGoldManufact = $scope.defaultData.enTotalGoldManufact;
        }
        else { $scope.billSetting.BillGrdCol.ENTotalGoldManufact = $scope.gridData.ENTotalGoldManufact; }


        if ($scope.gridData.ENExemptOfTax == "" || $scope.gridData.ENExemptOfTax == undefined) {
            $scope.billSetting.BillGrdCol.ENExemptOfTax = $scope.defaultData.enExemptOfTax;
        }
        else { $scope.billSetting.BillGrdCol.ENExemptOfTax = $scope.gridData.ENExemptOfTax; }


        if ($scope.gridData.ENItemPrice == "" || $scope.gridData.ENItemPrice == undefined) {
            $scope.billSetting.BillGrdCol.ENItemPrice = $scope.defaultData.enItemPrice;
        }
        else { $scope.billSetting.BillGrdCol.ENItemPrice = $scope.gridData.ENItemPrice; }


        if ($scope.gridData.ENAfterDiscount == "" || $scope.gridData.ENAfterDiscount == undefined) {
            $scope.billSetting.BillGrdCol.ENAfterDiscount = $scope.defaultData.enAfterDiscount;
        }
        else { $scope.billSetting.BillGrdCol.ENAfterDiscount = $scope.gridData.ENAfterDiscount; }
      
       // ARLockPrice
       debugger;
       if ($scope.gridData.ENLockPrice == "" || $scope.gridData.ENLockPrice == undefined) {
           $scope.billSetting.BillGrdCol.ENLockPrice = $scope.defaultData.ENLockPrice;
       }
       else { $scope.billSetting.BillGrdCol.ENLockPrice = $scope.gridData.ENLockPrice; }
    }
    ///////////////get grid data
    $scope.getGridData = function (gridCol) {

        //////////////////////////////arabic names
        if (gridCol.ARCode == "" || gridCol.ARCode == undefined) {
            $scope.gridData.ARCode = $scope.defaultData.arCode;
        }
        else { $scope.gridData.ARCode = gridCol.ARCode; }


        if (gridCol.ARItem == "" || gridCol.ARItem == undefined) {
            $scope.gridData.ARItem = $scope.defaultData.arItem;
        }
        else { $scope.gridData.ARItem = gridCol.ARItem; }


        if (gridCol.ARQuantity == "" || gridCol.ARQuantity == undefined) {
            $scope.gridData.ARQuantity = $scope.defaultData.arQuantity;
        }
        else { $scope.gridData.ARQuantity = gridCol.ARQuantity; }


        if (gridCol.ARItemWeight == "" || gridCol.ARItemWeight == undefined) {
            $scope.gridData.ARItemWeight = $scope.defaultData.arItemWeight;
        }
        else { $scope.gridData.ARItemWeight = gridCol.ARItemWeight; }


        if (gridCol.ARItemGmWages == "" || gridCol.ARItemGmWages == undefined) {
            $scope.gridData.ARItemGmWages = $scope.defaultData.arItemGmWages;
        }
        else { $scope.gridData.ARItemGmWages = gridCol.ARItemGmWages; }


        if (gridCol.ARManufacturingWages == "" || gridCol.ARManufacturingWages == undefined) {
            $scope.gridData.ARManufacturingWages = $scope.defaultData.arManfactWages;
        }
        else { $scope.gridData.ARManufacturingWages = gridCol.ARManufacturingWages; }


        if (gridCol.ARUnit == "" || gridCol.ARUnit == undefined) {
            $scope.gridData.ARUnit = $scope.defaultData.arUnit;
        }
        else { $scope.gridData.ARUnit = gridCol.ARUnit; }


        if (gridCol.ARCaliberName == "" || gridCol.ARCaliberName == undefined) {
            $scope.gridData.ARCaliberName = $scope.defaultData.arCaliberName;
        }
        else { $scope.gridData.ARCaliberName = gridCol.ARCaliberName; }


        if (gridCol.ARPrice == "" || gridCol.ARPrice == undefined) {
            $scope.gridData.ARPrice = $scope.defaultData.arPrice;
        }
        else { $scope.gridData.ARPrice = gridCol.ARPrice; }


        if (gridCol.ARDiscount == "" || gridCol.ARDiscount == undefined) {
            $scope.gridData.ARDiscount = $scope.defaultData.arDiscount;
        }
        else { $scope.gridData.ARDiscount = gridCol.ARDiscount; }


        if (gridCol.ARDiscRate == "" || gridCol.ARDiscRate == undefined) {
            $scope.gridData.ARDiscRate = $scope.defaultData.arDiscRate;
        }
        else { $scope.gridData.ARDiscRate = gridCol.ARDiscRate; }


        if (gridCol.ARTotal == "" || gridCol.ARTotal == undefined) {
            $scope.gridData.ARTotal = $scope.defaultData.arTotal;
        }
        else { $scope.gridData.ARTotal = gridCol.ARTotal; }


        if (gridCol.ARClearnessRate == "" || gridCol.ARClearnessRate == undefined) {
            $scope.gridData.ARClearnessRate = $scope.defaultData.arClearnessRate;
        }
        else { $scope.gridData.ARClearnessRate = gridCol.ARClearnessRate; }


        if (gridCol.ARCaliber24 == "" || gridCol.ARCaliber24 == undefined) {
            $scope.gridData.ARCaliber24 = $scope.defaultData.arCaliber24;
        }
        else { $scope.gridData.ARCaliber24 = gridCol.ARCaliber24; }


        if (gridCol.ARCaliber22 == "" || gridCol.ARCaliber22 == undefined) {
            $scope.gridData.ARCaliber22 = $scope.defaultData.arCaliber22;
        }
        else { $scope.gridData.ARCaliber22 = gridCol.ARCaliber22; }


        if (gridCol.ARCaliber21 == "" || gridCol.ARCaliber21 == undefined) {
            $scope.gridData.ARCaliber21 = $scope.defaultData.arCaliber22;
        }
        else { $scope.gridData.ARCaliber21 = gridCol.ARCaliber21; }


        if (gridCol.ARCaliber18 == "" || gridCol.ARCaliber18 == undefined) {
            $scope.gridData.ARCaliber18 = $scope.defaultData.arCaliber18;
        }
        else { $scope.gridData.ARCaliber18 = gridCol.ARCaliber18; }


        if (gridCol.ARCostCenter == "" || gridCol.ARCostCenter == undefined) {
            $scope.gridData.ARCostCenter = $scope.defaultData.arCostCenter;
        }
        else { $scope.gridData.ARCostCenter = gridCol.ARCostCenter; }


        if (gridCol.ARSubjectToVat == "" || gridCol.ARSubjectToVat == undefined) {
            $scope.gridData.ARSubjectToVat = $scope.defaultData.arSubjectToVat;
        }
        else { $scope.gridData.ARSubjectToVat = gridCol.ARSubjectToVat; }


        if (gridCol.ARVatRate == "" || gridCol.ARVatRate == undefined) {
            $scope.gridData.ARVatRate = $scope.defaultData.arVatRate;
        }
        else { $scope.gridData.ARVatRate = gridCol.ARVatRate; }

        if (gridCol.ARVatValue == "" || gridCol.ARVatValue == undefined) {
            $scope.gridData.ARVatValue = $scope.defaultData.arVatValue;
        }
        else { $scope.gridData.ARVatValue = gridCol.ARVatValue; }


        if (gridCol.ARWagesDiscValue == "" || gridCol.ARWagesDiscValue == undefined) {
            $scope.gridData.ARWagesDiscValue = $scope.defaultData.arWagesDiscValue;
        }
        else { $scope.gridData.ARWagesDiscValue = gridCol.ARWagesDiscValue; }

        if (gridCol.ARWagesDiscRate == "" || gridCol.ARWagesDiscRate == undefined) {
            $scope.gridData.ARWagesDiscRate = $scope.defaultData.arWagesDiscRate;
        }
        else { $scope.gridData.ARWagesDiscRate = gridCol.ARWagesDiscRate; }


        if (gridCol.ARActualWeight == "" || gridCol.ARActualWeight == undefined) {
            $scope.gridData.ARActualWeight = $scope.defaultData.arActualWeight;
        }
        else { $scope.gridData.ARActualWeight = gridCol.ARActualWeight; }


        if (gridCol.ARTotalItemGmWages == "" || gridCol.ARTotalItemGmWages == undefined) {
            $scope.gridData.ARTotalItemGmWages = $scope.defaultData.arTotalItemGmWages;
        }
        else { $scope.gridData.ARTotalItemGmWages = gridCol.ARTotalItemGmWages; }


        if (gridCol.ARTotalGoldManufact == "" || gridCol.ARTotalGoldManufact == undefined) {
            $scope.gridData.ARTotalGoldManufact = $scope.defaultData.arTotalGoldManufact;
        }
        else { $scope.gridData.ARTotalGoldManufact = gridCol.ARTotalGoldManufact; }


        if (gridCol.ARExemptOfTax == "" || gridCol.ARExemptOfTax == undefined) {
            $scope.gridData.ARExemptOfTax = $scope.defaultData.arExemptOfTax;
        }
        else { $scope.gridData.ARExemptOfTax = gridCol.ARExemptOfTax; }


        if (gridCol.ARItemPrice == "" || gridCol.ARItemPrice == undefined) {
            $scope.gridData.ARItemPrice = $scope.defaultData.arItemPrice;
        }
        else { $scope.gridData.ARItemPrice = gridCol.ARItemPrice; }


        if (gridCol.ARAfterDiscount == "" || gridCol.ARAfterDiscount == undefined) {
            $scope.gridData.ARAfterDiscount = $scope.defaultData.arAfterDiscount;
        }
        else { $scope.gridData.ARAfterDiscount = gridCol.ARAfterDiscount; }
        debugger;
        if (gridCol.ARLockPrice == "" || gridCol.ARLockPrice == undefined) {
            $scope.gridData.ARLockPrice = $scope.defaultData.ARLockPrice;
        }
        else { $scope.gridData.ARLockPrice = $scope.gridData.ARLockPrice; }

        /////////english
        if (gridCol.ENLockPrice == "" || gridCol.ENLockPrice == undefined) {
            $scope.gridData.ENLockPrice = $scope.defaultData.ENLockPrice;
        }
        else { $scope.gridData.ENLockPrice = $scope.gridData.ENLockPrice; }

        if (gridCol.ENCode == "" || gridCol.ENCode == undefined) {
            $scope.gridData.ENCode = $scope.defaultData.enCode;
        }
        else { $scope.gridData.ENCode = gridCol.ENCode; }


        if (gridCol.ENItem == "" || gridCol.ENItem == undefined) {
            $scope.gridData.ENItem = $scope.defaultData.enItem;
        }
        else { $scope.gridData.ENItem = gridCol.ENItem; }


        if (gridCol.ENQuantity == "" || gridCol.ENQuantity == undefined) {
            $scope.gridData.ENQuantity = $scope.defaultData.enQuantity;
        }
        else { $scope.gridData.ENQuantity = gridCol.ENQuantity; }


        if (gridCol.ENItemWeight == "" || gridCol.ENItemWeight == undefined) {
            $scope.gridData.ENItemWeight = $scope.defaultData.enItemWeight;
        }
        else { $scope.gridData.ENItemWeight = gridCol.ENItemWeight; }


        if (gridCol.ENItemGmWages == "" || gridCol.ENItemGmWages == undefined) {
            $scope.gridData.ENItemGmWages = $scope.defaultData.enItemGmWages;
        }
        else { $scope.gridData.ENItemGmWages = gridCol.ENItemGmWages; }


        if (gridCol.ENManufacturingWages == "" || gridCol.ENManufacturingWages == undefined) {
            $scope.gridData.ENManufacturingWages = $scope.defaultData.enManfactWages;
        }
        else { $scope.gridData.ENManufacturingWages = gridCol.ENManufacturingWages; }


        if (gridCol.ENUnit == "" || gridCol.ENUnit == undefined) {
            $scope.gridData.ENUnit = $scope.defaultData.enUnit;
        }
        else { $scope.gridData.ENUnit = gridCol.ENUnit; }


        if (gridCol.ENCaliberName == "" || gridCol.ENCaliberName == undefined) {
            $scope.gridData.ENCaliberName = $scope.defaultData.enCaliberName;
        }
        else { $scope.gridData.ENCaliberName = gridCol.ENCaliberName; }


        if (gridCol.ENPrice == "" || gridCol.ENPrice == undefined) {
            $scope.gridData.ENPrice = $scope.defaultData.enPrice;
        }
        else { $scope.gridData.ENPrice = gridCol.ENPrice; }


        if (gridCol.ENDiscount == "" || gridCol.ENDiscount == undefined) {
            $scope.gridData.ENDiscount = $scope.defaultData.enDiscount;
        }
        else { $scope.gridData.ENDiscount = gridCol.ENDiscount; }


        if (gridCol.ENDiscRate == "" || gridCol.ENDiscRate == undefined) {
            $scope.gridData.ENDiscRate = $scope.defaultData.enDiscRate;
        }
        else { $scope.gridData.ENDiscRate = gridCol.ENDiscRate; }


        if (gridCol.ENTotal == "" || gridCol.ENTotal == undefined) {
            $scope.gridData.ENTotal = $scope.defaultData.enTotal;
        }
        else { $scope.gridData.ENTotal = gridCol.ENTotal; }


        if (gridCol.ENClearnessRate == "" || gridCol.ENClearnessRate == undefined) {
            $scope.gridData.ENClearnessRate = $scope.defaultData.enClearnessRate;
        }
        else { $scope.gridData.ENClearnessRate = gridCol.ENClearnessRate; }


        if (gridCol.ENCaliber24 == "" || gridCol.ENCaliber24 == undefined) {
            $scope.gridData.ENCaliber24 = $scope.defaultData.enCaliber24;
        }
        else { $scope.gridData.ENCaliber24 = gridCol.ENCaliber24; }


        if (gridCol.ENCaliber22 == "" || gridCol.ENCaliber22 == undefined) {
            $scope.gridData.ENCaliber22 = $scope.defaultData.enCaliber22;
        }
        else { $scope.gridData.ENCaliber22 = gridCol.ENCaliber22; }


        if (gridCol.ENCaliber21 == "" || gridCol.ENCaliber21 == undefined) {
            $scope.gridData.ENCaliber21 = $scope.defaultData.enCaliber22;
        }
        else { $scope.gridData.ENCaliber21 = gridCol.ENCaliber21; }


        if (gridCol.ENCaliber18 == "" || gridCol.ENCaliber18 == undefined) {
            $scope.gridData.ENCaliber18 = $scope.defaultData.enCaliber18;
        }
        else { $scope.gridData.ENCaliber18 = gridCol.ENCaliber18; }


        if (gridCol.ENCostCenter == "" || gridCol.ENCostCenter == undefined) {
            $scope.gridData.ENCostCenter = $scope.defaultData.enCostCenter;
        }
        else { $scope.gridData.ENCostCenter = gridCol.ENCostCenter; }


        if (gridCol.ENSubjectToVat == "" || gridCol.ENSubjectToVat == undefined) {
            $scope.gridData.ENSubjectToVat = $scope.defaultData.enSubjectToVat;
        }
        else { $scope.gridData.ENSubjectToVat = gridCol.ENSubjectToVat; }


        if (gridCol.ENVatRate == "" || gridCol.ENVatRate == undefined) {
            $scope.gridData.ENVatRate = $scope.defaultData.enVatRate;
        }
        else { $scope.gridData.ENVatRate = gridCol.ENVatRate; }


        if (gridCol.ENVatValue == "" || gridCol.ENVatValue == undefined) {
            $scope.gridData.ENVatValue = $scope.defaultData.enVatValue;
        }
        else { $scope.gridData.ENVatValue = gridCol.ENVatValue; }


        if (gridCol.ENWagesDiscValue == "" || gridCol.ENWagesDiscValue == undefined) {
            $scope.gridData.ENWagesDiscValue = $scope.defaultData.enWagesDiscValue;
        }
        else { $scope.gridData.ENWagesDiscValue = gridCol.ENWagesDiscValue; }


        if (gridCol.ENWagesDiscRate == "" || gridCol.ENWagesDiscRate == undefined) {
            $scope.gridData.ENWagesDiscRate = $scope.defaultData.enWagesDiscRate;
        }
        else { $scope.gridData.ENWagesDiscRate = gridCol.ENWagesDiscRate; }



        if (gridCol.ENActualWeight == "" || gridCol.ENActualWeight == undefined) {
            $scope.gridData.ENActualWeight = $scope.defaultData.enActualWeight;
        }
        else { $scope.gridData.ENActualWeight = gridCol.ENActualWeight; }


        if (gridCol.ENTotalItemGmWages == "" || gridCol.ENTotalItemGmWages == undefined) {
            $scope.gridData.ENTotalItemGmWages = $scope.defaultData.enTotalItemGmWages;
        }
        else { $scope.gridData.ENTotalItemGmWages = gridCol.ENTotalItemGmWages; }


        if (gridCol.ENTotalGoldManufact == "" || gridCol.ENTotalGoldManufact == undefined) {
            $scope.gridData.ENTotalGoldManufact = $scope.defaultData.enTotalGoldManufact;
        }
        else { $scope.gridData.ENTotalGoldManufact = gridCol.ENTotalGoldManufact; }


        if (gridCol.ENExemptOfTax == "" || gridCol.ENExemptOfTax == undefined) {
            $scope.gridData.ENExemptOfTax = $scope.defaultData.enExemptOfTax;
        }
        else { $scope.gridData.ENExemptOfTax = gridCol.ENExemptOfTax; }


        if (gridCol.ENItemPrice == "" || gridCol.ENItemPrice == undefined) {
            $scope.gridData.ENItemPrice = $scope.defaultData.enItemPrice;
        }
        else { $scope.gridData.ENItemPrice = gridCol.ENItemPrice; }


        if (gridCol.ENAfterDiscount == "" || gridCol.ENAfterDiscount == undefined) {
            $scope.gridData.ENAfterDiscount = $scope.defaultData.enAfterDiscount;
        }
        else { $scope.gridData.ENAfterDiscount = gridCol.ENAfterDiscount; }
//ARLockPrice
        //if (gridCol.ENAfterDiscount == "" || gridCol.ENAfterDiscount == undefined) {
        //    $scope.gridData.ENAfterDiscount = $scope.defaultData.enAfterDiscount;
        //}
        //else { $scope.gridData.ENAfterDiscount = gridCol.ENAfterDiscount; }
    }

    /////////////////////////////////////////////////////////user menu ///////////////////////////////////////////////////
    $scope.getMainMenus = function () {
        MenuService.getMainMenu().then(function (result) {
            debugger
            $scope.mainMenuList = result.data;
        });
    }

    $scope.saveBillSettingAsMenu = function () {
        $scope.userMenu = {};
        $scope.userMenu.ARName = $scope.billSetting.BILL_AR_NAME;
        $scope.userMenu.LatName = $scope.billSetting.BILL_EN_NAME;
        $scope.userMenu.MenuLink = "#/PointOfSale?billType=" + $scope.settingID;
        // $scope.userMenu.MenuLink = "#/PointOfSale?billType=" + $scope.billSetting.BILL_TYPE_ID;
        $scope.userMenu.MenuID = $scope.billSetting.MenuID;
        $scope.userMenu.CountryID = 1;
        $scope.userMenu.DisplayOrNot = 1;
        $scope.userMenu.UserShortcut = 0;
        $scope.userMenu.BillSetiingID = $scope.settingID;
        $scope.userMenu.Active = true;
        $scope.userMenu.AddedOn = new Date();
        var addedBy = authService.GetUserName();
        $scope.userMenu.AddedBy = addedBy;

        MenuService.add($scope.userMenu).then(function (result) {
            return result.data;
        });
    }


    $scope.updateSettingMenu = function () {
        debugger
        $scope.userMenu = {};
        $scope.userMenu.ARName = $scope.billSetting.BILL_AR_NAME;
        $scope.userMenu.LatName = $scope.billSetting.BILL_EN_NAME;
        $scope.userMenu.MenuLink = "#/PointOfSale?billType=" + $scope.billSetting.BILL_SETTING_ID;
        $scope.userMenu.MenuID = $scope.menu.menuId;
        $scope.userMenu.CountryID = 1;
        $scope.userMenu.DisplayOrNot = 1;
        $scope.userMenu.UserShortcut = 0;
        $scope.userMenu.BillSetiingID = $scope.billSetting.BILL_SETTING_ID;
        $scope.userMenu.Active = true;
        $scope.userMenu.UpdatedOn = new Date();
        $scope.userMenu.ID = $scope.userMenuID;
        MenuService.Update($scope.userMenu).then(function (result) {
            var x = result.data;
        });
    }


    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    $scope.getPriceList = function () {
        $scope.priceList = [{ priceListID: 1, priceListName: 'سعر ذهب عالمي فقط' }, { priceListID: 2, priceListName: 'سعر ذهب عالمي + اجرة التصنيع + هامش الربح' }
        ];
    }

    $scope.filterAccountsByTypes = function (param) {

        return function (item)
        { 
            if (param == "AccID")
        {
                if (item.ACC_TYPE_ID == 7 && item.PARENT_ACC_ID != null) {
                    return true;
                }
            }
            else if (param == "AccSalesGoldID") {
                if ((item.ACC_TYPE_ID == 5 || item.ACC_TYPE_ID == 6) && item.PARENT_ACC_ID != null) {
                    return true;
                }
            }
            else if (param == "") {
                if ((item.ACC_TYPE_ID != 5 && item.ACC_TYPE_ID != 6 && item.ACC_TYPE_ID != 7 && item.ACC_TYPE_ID != 21) && item.PARENT_ACC_ID != null) {
                    return true;
                }
            }
            return false;
        };
    };
    ////////////////////////////////////////////////
    $scope.getAccountList = function () {
        accountService.getAll().then(function (results) {
            $scope.accountList = results.data;
            debugger
        }, function (error) {
            console.log(error.data.message);
        });
    }
    ///////////////////////////////////default accounts 

    ///// item account
    $scope.getOtherAccountList = function () {
        accountsService.getDefaultAccountsOfBillSettings("other").then(function (result) {           
            $scope.otherAccountList = result.data;
        });
    }


    ///// extra account
    $scope.getExtraAccountList = function () {
        accountsService.getDefaultAccountsOfBillSettings("extra").then(function (result) {
            $scope.extraAccountList = result.data;
        });
    }


    ///// pay account
    $scope.getPayAccountList = function () {
        accountsService.getDefaultAccountsOfBillSettings("pay").then(function (result) {
            $scope.payAccountList = result.data;
        });
    }

    ///// cost account
    $scope.getCostAccountList = function () {
        accountsService.getDefaultAccountsOfBillSettings("cost").then(function (result) {
            $scope.costAccountList = result.data;
        });
    }

    ///// store account
    $scope.getStoreAccountList = function () {
        accountsService.getDefaultAccountsOfBillSettings("store").then(function (result) {
            $scope.storeAccountList = result.data;
        });
    }

    ///// gift account
    $scope.getGiftAccountList = function () {
        accountsService.getDefaultAccountsOfBillSettings("gift").then(function (result) {
            $scope.giftAccountList = result.data;
        });
    }

    ///// wages account
    $scope.getWagesAccountList = function () {
        accountsService.getDefaultAccountsOfBillSettings("wages").then(function (result) {
            $scope.wagesAccountList = result.data;
        });
    }

    ///// sales account
    $scope.getSalesAccountList = function () {
        accountsService.getDefaultAccountsOfBillSettings("sales").then(function (result) {
            $scope.salesAccountList = result.data;
        });
    }

    ///// vat account
    $scope.getVatAccountList = function () {
        accountsService.getDefaultAccountsOfBillSettings("vat").then(function (result) {
            $scope.vatAccountList = result.data;
        });
    }


    ///// purchas account
    $scope.getPurchasAccountList = function () {
        accountsService.getDefaultAccountsOfBillSettings("purchas").then(function (result) {
            $scope.purchasAccountList = result.data;
        });
    }

    ///// purchas tax account
    $scope.getPurchasTaxAccountList = function () {
        accountsService.getDefaultAccountsOfBillSettings("purchasTax").then(function (result) {
            $scope.purchasTaxAccountList = result.data;
        });
    }

    ///// purchas comm account
    $scope.getCommAccountList = function () {
        accountsService.getDefaultAccountsOfBillSettings("comm").then(function (result) {
            $scope.commAccountList = result.data;
        });
    }

    ///// purchas comm tax account
    $scope.getCommTaxAccountList = function () {
        accountsService.getDefaultAccountsOfBillSettings("comm").then(function (result) {
            $scope.commTaxAccountList = result.data;
        });
    }

    ///// cust tax account
    $scope.getCustAccountList = function () {
        accountsService.getDefaultAccountsOfBillSettings("cust").then(function (result) {
            $scope.custAccountList = result.data;
        });
    }


    ///////gold account
    $scope.getGoldAccountList = function () {
        accountsService.getDefaultAccountsOfBillSettings("gold").then(function (result) {
            $scope.goldAccountList = result.data;
        });
    }

}]);



