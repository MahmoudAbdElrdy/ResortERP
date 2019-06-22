'use strict';
app.controller('entrySettingController', ['$scope', '$location', '$log', '$q', 'entrySettingService', 'accountService', '$document', '$uibModal', '$uibModalStack', 'currencyService', 'entryTypeService', 'uiGridConstants', 'entryShowOptionService', 'entryGridColumnService', 'accountsService', 'MenuService', 'authService', 'localStorageService', function ($scope, $location, $log, $q, entrySettingService, accountService, $document, $uibModal, $uibModalStack, currencyService, entryTypeService, uiGridConstants, entryShowOptionService, entryGridColumnService, accountsService, MenuService, authService, localStorageService) {

    $scope.GoldSetting = false;
    $scope.uiGridOptions = {
        enableColumnMenus: false,
        showHeader: false,
        rowTemplate: '<div grid="grid" class="ui-grid-draggable-row" draggable="true"><div ng-repeat="(colRenderIndex, col) in colContainer.renderedColumns track by col.colDef.name" class="ui-grid-cell" ng-class="{ \'ui-grid-row-header-cell\': col.isRowHeader, \'custom\': true }" ui-grid-cell></div></div>',
        columnDefs: [
            { name: "Color", displayName: "", cellTemplate: '<input type="color" ng-model="row.entity.Color" class="form-control" value="{{row.entity.Color}}" style="width:90%;margin:auto" />', enableSorting: false },
            { name: "WIDTH", displayName: "", cellTemplate: '<input type="number" ng-model="row.entity.WIDTH" class="form-control" placeholder="عرض الحقل" style="width:90%;margin:auto" />', enableSorting: false },
            { name: "ENTRY_SHOW_ID", displayName: "", cellTemplate: '<div class="checkbox clip-check check-primary checkbox-inline" ><input id="{{row.entity.ENTRY_SHOW_ID}}" type="checkbox" ng-model="row.entity.Selected" value="{{row.entity.ENTRY_SHOW_ID}}"><label for="{{row.entity.ENTRY_SHOW_ID}}">{{row.entity.ENTRY_SHOW_AR_NAME}}</label></div>', enableSorting: false }
        ],
    };

    $scope.uiGridOptions.onRegisterApi = function (gridApi) {
        gridApi.draggableRows.on.rowDropped($scope, function (info, dropTarget) {
            console.log("Dropped", info);
        });
    };

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
            if ($scope.Type != null && $scope.Type != undefined && $scope.Type != "") {

                $scope.GoldSetting = false;
            }
            else {
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
    $scope.gridData = {
        ARCode: 'كود', ENCode: 'Code', ARAccountName: 'الحساب', ENAccountName: 'Account', ARCredit: 'دائن', ENCredit: 'Credit', ARDepit: 'مدين', ENDepit: 'Depit', ARCredit24: 'دائن 24', ENCredit24: 'Credit 24'
        , ARDepit24: 'مدين 24', ENDepit24: 'Depit 24', ARCredit22: 'دائن 22', ENCredit22: 'Credit 22', ARDepit22: 'مدين 22', ENDepit22: 'Depit 22', ARCredit21: 'دائن 21', ENCredit21: 'Credit 21'
        , ARDepit21: 'مدين 21', ENDepit21: 'Depit 21', ARCredit18: 'دائن 18', ENCredit18: 'Credit 18', ARDepit18: 'مدين 18', ENDepit18: 'Depit 18', ARCostCenter: ' مركز التكلفه', ENCostCenter: 'Cost Center'
        , ARRemarks: 'شرح', ENRemarks: 'Remarks', ARTaxable: 'خاضع للضريبه', EnTaxable: 'Taxable', ARTaxRate: 'نسبه الضريبه', ENTaxRate: 'Tax Rate', ARTaxValue: 'قيمة الضريبه', ENTaxValue: 'Tax Value'
        , ARCheckNumber: 'رقم الشيك', ENCheckNumber: 'Check Number', ARCheckDate: 'تاريخ الشيك', ENCheckDate: 'Check Date', ARCheckIssueDate: 'تاريخ الاستحقاق', ENCheckIssueDate: 'Check Issue Date'
        , ARExemptOfTax: 'معفي من الضريبه', ENExemptOfTax: 'Exempt of tax'

    }


    $scope.clearEnity = function () {
        $scope.entrySetting = { ENTRY_SETTING_ID: null, ACCEPT_DIST_ACC: false, AUTO_POSTING: false, COSTCENTER_BELONG: false };
        $scope.Custom = {};
        $scope.getEntryTypesList();
        $scope.getentryShowList();

        $scope.gridData = {
            ARCode: 'كود', ENCode: 'Code', ARAccountName: 'الحساب', ENAccountName: 'Account', ARCredit: 'دائن', ENCredit: 'Credit', ARDepit: 'مدين', ENDepit: 'Depit', ARCredit24: 'دائن 24', ENCredit24: 'Credit 24'
            , ARDepit24: 'مدين 24', ENDepit24: 'Depit 24', ARCredit22: 'دائن 22', ENCredit22: 'Credit 22', ARDepit22: 'مدين 22', ENDepit22: 'Depit 22', ARCredit21: 'دائن 21', ENCredit21: 'Credit 21'
            , ARDepit21: 'مدين 21', ENDepit21: 'Depit 21', ARCredit18: 'دائن 18', ENCredit18: 'Credit 18', ARDepit18: 'مدين 18', ENDepit18: 'Depit 18', ARCostCenter: ' مركز التكلفه', ENCostCenter: 'Cost Center'
            , ARRemarks: 'شرح', ENRemarks: 'Remarks', ARTaxable: 'خاضع للضريبه', EnTaxable: 'Taxable', ARTaxRate: 'نسبه الضريبه', ENTaxRate: 'Tax Rate', ARTaxValue: 'قيمة الضريبه', ENTaxValue: 'Tax Value'
            , ARCheckNumber: 'رقم الشيك', ENCheckNumber: 'Check Number', ARCheckDate: 'تاريخ الشيك', ENCheckDate: 'Check Date', ARCheckIssueDate: 'تاريخ الاستحقاق', ENCheckIssueDate: 'Check Issue Date'
            , ARExemptOfTax: 'معفي من الضريبه', ENExemptOfTax: 'Exempt of tax'
        }
    }

    $scope.Custom = {
        Currency: null, ACC_Name: "", CURRENCY_RATE: "", role: null
    };


    $scope.entrySetting = { ENTRY_SETTING_ID: null, ACCEPT_DIST_ACC: false, AUTO_POSTING: false, COSTCENTER_BELONG: false };
    $scope.entrySettingList = [];
    $scope.entrySettingTotalCount = 0;
    $scope.entryTypeList = [];
    $scope.pageNum = 1;
    $scope.pageSize = 10;
    $scope.pagesCount = 0;
    $scope.maxSize = 5;
    $scope.accountList = [];
    $scope.taxAccountList = [];

    $scope.mainMenuList = [];
    $scope.menu = {};

    $scope.entryModeList = [{ modeId: 1, modeName: 'عادي' }, { modeId: 2, modeName: 'مركب' }]

    $scope.getPagesCount = function () {
        var div = Math.floor($scope.entrySettingTotalCount / $scope.pageSize);
        var rem = $scope.entrySettingTotalCount % $scope.pageSize;
        if (rem > 0) {
            div = div + 1;
        }
        return div;
    }

    $scope.refreshData = function () {
        $scope.getAllOnLoad();
    }
    $scope.saveentrySetting = function () {
        $scope.saveEntity();
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

    //filter Selected currencyList from BILLcurrencyList during update
    $scope.getAccountNameBasedOnAccountID = function (AccountID) {
        return accountService.getByID(AccountID);
    }

    $scope.SetEntryshowData = function (entrySettingID) {
        $scope.EntryGrdCol = {};
        $scope.IsShow = true;
        if (entrySettingID !== 0) {
            $scope.EntryGrdCol.ENTRY_SETTING_ID = entrySettingID;
        }
        var Counter = 1;
        $scope.valid = true;
        for (var i = 0; i < $scope.entryShowList.length; i++) {
            var obj = $scope.entryShowList[i];
            if (obj.ENTRY_SHOW_ID == 1) {
                if (obj.Selected === true) {
                    $scope.EntryGrdCol.CREDIT_INDEX = Counter;
                    $scope.EntryGrdCol.CREDIT_WIDTH = obj.WIDTH;
                    $scope.EntryGrdCol.CREDIT_COLOR_HEXA = obj.Color;
                    $scope.EntryGrdCol.IS_CREDIT = true;
                    Counter++;
                }
                else {
                    $scope.EntryGrdCol.CREDIT_INDEX = null;
                    $scope.EntryGrdCol.CREDIT_WIDTH = null;
                    $scope.EntryGrdCol.CREDIT_COLOR_HEXA = null;
                    $scope.EntryGrdCol.IS_CREDIT = false;
                }
            }
            if (obj.ENTRY_SHOW_ID == 2) {
                if (obj.Selected === true) {
                    $scope.EntryGrdCol.DEBIT_INDEX = Counter;
                    $scope.EntryGrdCol.DEBIT_WIDTH = obj.WIDTH;
                    $scope.EntryGrdCol.DEBIT_COLOR_HEXA = obj.Color;
                    $scope.EntryGrdCol.IS_DEBIT = true;
                    Counter++;
                }
                else {
                    $scope.EntryGrdCol.DEBIT_INDEX = null;
                    $scope.EntryGrdCol.DEBIT_WIDTH = null;
                    $scope.EntryGrdCol.DEBIT_COLOR_HEXA = null;
                    $scope.EntryGrdCol.IS_DEBIT = false;
                }
            }
            if (obj.ENTRY_SHOW_ID == 3) {
                if (obj.Selected === true) {
                    $scope.EntryGrdCol.ACC_INDEX = Counter;
                    $scope.EntryGrdCol.ACC_WIDTH = obj.WIDTH;
                    $scope.EntryGrdCol.ACC_COLOR_HEXA = obj.Color;
                    $scope.EntryGrdCol.IS_ACC = true;
                    Counter++;
                }
                else {
                    $scope.EntryGrdCol.ACC_INDEX = null;
                    $scope.EntryGrdCol.ACC_WIDTH = null;
                    $scope.EntryGrdCol.ACC_COLOR_HEXA = null;
                    $scope.EntryGrdCol.IS_ACC = false;
                }
            }
            if (obj.ENTRY_SHOW_ID == 4) {
                if (obj.Selected === true) {
                    $scope.EntryGrdCol.COST_CENTER_INDEX = Counter;
                    $scope.EntryGrdCol.COST_CENTER_WIDTH = obj.WIDTH;
                    $scope.EntryGrdCol.COST_CENTER_COLOR_HEXA = obj.Color;
                    $scope.EntryGrdCol.IS_COST_CENTER = true;
                    Counter++;
                }
                else {
                    $scope.EntryGrdCol.COST_CENTER_INDEX = null;
                    $scope.EntryGrdCol.COST_CENTER_WIDTH = null;
                    $scope.EntryGrdCol.COST_CENTER_COLOR_HEXA = null;
                    $scope.EntryGrdCol.IS_COST_CENTER = false;
                }
            }
            if (obj.ENTRY_SHOW_ID == 5) {
                if (obj.Selected === true) {
                    $scope.EntryGrdCol.REMARKS_INDEX = Counter;
                    $scope.EntryGrdCol.REMARKS_WIDTH = obj.WIDTH;
                    $scope.EntryGrdCol.REMARKS_COLOR_HEXA = obj.Color;
                    $scope.EntryGrdCol.IS_REMARKS = true;
                    Counter++;
                }
                else {
                    $scope.EntryGrdCol.REMARKS_INDEX = null;
                    $scope.EntryGrdCol.REMARKS_WIDTH = null;
                    $scope.EntryGrdCol.REMARKS_COLOR_HEXA = null;
                    $scope.EntryGrdCol.IS_REMARKS = false;
                }
            }

            if (obj.ENTRY_SHOW_ID == 6) {
                if (obj.Selected === true) {
                    $scope.EntryGrdCol.TaxRate_INDEX = Counter;
                    $scope.EntryGrdCol.TaxRate_WIDTH = obj.WIDTH;
                    $scope.EntryGrdCol.TaxRate_COLOR_HEXA = obj.Color;
                    $scope.EntryGrdCol.IS_TaxRate = true;
                    Counter++;
                }
                else {
                    $scope.EntryGrdCol.TaxRate_INDEX = null;
                    $scope.EntryGrdCol.TaxRate_WIDTH = null;
                    $scope.EntryGrdCol.TaxRate_COLOR_HEXA = null;
                    $scope.EntryGrdCol.IS_TaxRate = false;
                }
            }

            if (obj.ENTRY_SHOW_ID == 7) {
                if (obj.Selected === true) {
                    $scope.EntryGrdCol.Taxable_INDEX = Counter;
                    $scope.EntryGrdCol.Taxable_WIDTH = obj.WIDTH;
                    $scope.EntryGrdCol.Taxable_COLOR_HEXA = obj.Color;
                    $scope.EntryGrdCol.IS_Taxable = true;
                    Counter++;
                }
                else {
                    $scope.EntryGrdCol.Taxable_INDEX = null;
                    $scope.EntryGrdCol.Taxable_WIDTH = null;
                    $scope.EntryGrdCol.Taxable_COLOR_HEXA = null;
                    $scope.EntryGrdCol.IS_Taxable = false;
                }
            }



            if (obj.ENTRY_SHOW_ID == 8) {
                if (obj.Selected === true) {
                    $scope.EntryGrdCol.GoldCredit24_INDEX = Counter;
                    $scope.EntryGrdCol.GoldCredit24_WIDTH = obj.WIDTH;
                    $scope.EntryGrdCol.GoldCredit24_COLOR_HEXA = obj.Color;
                    $scope.EntryGrdCol.IS_GoldCredit24 = true;
                    Counter++;
                }
                else {
                    $scope.EntryGrdCol.GoldCredit24_INDEX = null;
                    $scope.EntryGrdCol.GoldCredit24_WIDTH = null;
                    $scope.EntryGrdCol.GoldCredit24_COLOR_HEXA = null;
                    $scope.EntryGrdCol.IS_GoldCredit24 = false;
                }
            }


            if (obj.ENTRY_SHOW_ID == 9) {
                if (obj.Selected === true) {
                    $scope.EntryGrdCol.GoldDepit24_INDEX = Counter;
                    $scope.EntryGrdCol.GoldDepit24_WIDTH = obj.WIDTH;
                    $scope.EntryGrdCol.GoldDepit24_COLOR_HEXA = obj.Color;
                    $scope.EntryGrdCol.IS_GoldDepit24 = true;
                    Counter++;
                }
                else {
                    $scope.EntryGrdCol.GoldDepit24_INDEX = null;
                    $scope.EntryGrdCol.GoldDepit24_WIDTH = null;
                    $scope.EntryGrdCol.GoldDepit24_COLOR_HEXA = null;
                    $scope.EntryGrdCol.IS_GoldDepit24 = false;
                }

            }


            //////////22
            if (obj.ENTRY_SHOW_ID == 10) {
                if (obj.Selected === true) {
                    $scope.EntryGrdCol.GoldCredit22_INDEX = Counter;
                    $scope.EntryGrdCol.GoldCredit22_WIDTH = obj.WIDTH;
                    $scope.EntryGrdCol.GoldCredit22_COLOR_HEXA = obj.Color;
                    $scope.EntryGrdCol.IS_GoldCredit22 = true;
                    Counter++;
                }
                else {
                    $scope.EntryGrdCol.GoldCredit22_INDEX = null;
                    $scope.EntryGrdCol.GoldCredit22_WIDTH = null;
                    $scope.EntryGrdCol.GoldCredit22_COLOR_HEXA = null;
                    $scope.EntryGrdCol.IS_GoldCredit22 = false;
                }

            }


            if (obj.ENTRY_SHOW_ID == 11) {
                if (obj.Selected === true) {
                    $scope.EntryGrdCol.GoldDepit22_INDEX = Counter;
                    $scope.EntryGrdCol.GoldDepit22_WIDTH = obj.WIDTH;
                    $scope.EntryGrdCol.GoldDepit22_COLOR_HEXA = obj.Color;
                    $scope.EntryGrdCol.IS_GoldDepit22 = true;
                    Counter++;
                }
                else {
                    $scope.EntryGrdCol.GoldDepit22_INDEX = null;
                    $scope.EntryGrdCol.GoldDepit22_WIDTH = null;
                    $scope.EntryGrdCol.GoldDepit22_COLOR_HEXA = null;
                    $scope.EntryGrdCol.IS_GoldDepit22 = false;
                }

            }

            ////////////21
            if (obj.ENTRY_SHOW_ID == 12) {
                if (obj.Selected === true) {
                    $scope.EntryGrdCol.GoldCredit21_INDEX = Counter;
                    $scope.EntryGrdCol.GoldCredit21_WIDTH = obj.WIDTH;
                    $scope.EntryGrdCol.GoldCredit21_COLOR_HEXA = obj.Color;
                    $scope.EntryGrdCol.IS_GoldCredit21 = true;
                    Counter++;
                }
                else {
                    $scope.EntryGrdCol.GoldCredit21_INDEX = null;
                    $scope.EntryGrdCol.GoldCredit21_WIDTH = null;
                    $scope.EntryGrdCol.GoldCredit21_COLOR_HEXA = null;
                    $scope.EntryGrdCol.IS_GoldCredit21 = false;
                }

            }


            if (obj.ENTRY_SHOW_ID == 13) {
                if (obj.Selected === true) {
                    $scope.EntryGrdCol.GoldDepit21_INDEX = Counter;
                    $scope.EntryGrdCol.GoldDepit21_WIDTH = obj.WIDTH;
                    $scope.EntryGrdCol.GoldDepit21_COLOR_HEXA = obj.Color;
                    $scope.EntryGrdCol.IS_GoldDepit21 = true;
                    Counter++;
                }
                else {
                    $scope.EntryGrdCol.GoldDepit21_INDEX = null;
                    $scope.EntryGrdCol.GoldDepit21_WIDTH = null;
                    $scope.EntryGrdCol.GoldDepit21_COLOR_HEXA = null;
                    $scope.EntryGrdCol.IS_GoldDepit21 = false;
                }

            }

            //////////////18

            if (obj.ENTRY_SHOW_ID == 14) {
                if (obj.Selected === true) {
                    $scope.EntryGrdCol.GoldCredit18_INDEX = Counter;
                    $scope.EntryGrdCol.GoldCredit18_WIDTH = obj.WIDTH;
                    $scope.EntryGrdCol.GoldCredit18_COLOR_HEXA = obj.Color;
                    $scope.EntryGrdCol.IS_GoldCredit18 = true;
                    Counter++;
                }
                else {
                    $scope.EntryGrdCol.GoldCredit18_INDEX = null;
                    $scope.EntryGrdCol.GoldCredit18_WIDTH = null;
                    $scope.EntryGrdCol.GoldCredit18_COLOR_HEXA = null;
                    $scope.EntryGrdCol.IS_GoldCredit18 = false;
                }

            }


            if (obj.ENTRY_SHOW_ID == 15) {
                if (obj.Selected === true) {
                    $scope.EntryGrdCol.GoldDepit18_INDEX = Counter;
                    $scope.EntryGrdCol.GoldDepit18_WIDTH = obj.WIDTH;
                    $scope.EntryGrdCol.GoldDepit18_COLOR_HEXA = obj.Color;
                    $scope.EntryGrdCol.IS_GoldDepit18 = true;
                    Counter++;
                }
                else {
                    $scope.EntryGrdCol.GoldDepit18_INDEX = null;
                    $scope.EntryGrdCol.GoldDepit18_WIDTH = null;
                    $scope.EntryGrdCol.GoldDepit18_COLOR_HEXA = null;
                    $scope.EntryGrdCol.IS_GoldDepit18 = false;
                }

            }



            if (obj.ENTRY_SHOW_ID == 16) {
                if (obj.Selected === true) {
                    $scope.EntryGrdCol.TaxValue_INDEX = Counter;
                    $scope.EntryGrdCol.TaxValue_WIDTH = obj.WIDTH;
                    $scope.EntryGrdCol.TaxValue_COLOR_HEXA = obj.Color;
                    $scope.EntryGrdCol.IS_TaxValue = true;
                    Counter++;
                }
                else {
                    $scope.EntryGrdCol.TaxValue_INDEX = null;
                    $scope.EntryGrdCol.TaxValue_WIDTH = null;
                    $scope.EntryGrdCol.TaxValue_COLOR_HEXA = null;
                    $scope.EntryGrdCol.IS_TaxValue = false;
                }
            }




            if (obj.ENTRY_SHOW_ID == 17) {
                if (obj.Selected === true) {
                    $scope.EntryGrdCol.CheckNumber_INDEX = Counter;
                    $scope.EntryGrdCol.CheckNumber_WIDTH = obj.WIDTH;
                    $scope.EntryGrdCol.CheckNumber_COLOR_HEXA = obj.Color;
                    $scope.EntryGrdCol.IsCheckNumber = true;
                    Counter++;
                }
                else {
                    $scope.EntryGrdCol.CheckNumber_INDEX = null;
                    $scope.EntryGrdCol.CheckNumber_WIDTH = null;
                    $scope.EntryGrdCol.CheckNumber_COLOR_HEXA = null;
                    $scope.EntryGrdCol.IsCheckNumber = false;
                }
            }


            if (obj.ENTRY_SHOW_ID == 18) {
                if (obj.Selected === true) {
                    $scope.EntryGrdCol.CheckDate_INDEX = Counter;
                    $scope.EntryGrdCol.CheckDate_WIDTH = obj.WIDTH;
                    $scope.EntryGrdCol.CheckDate_COLOR_HEXA = obj.Color;
                    $scope.EntryGrdCol.IsCheckDate = true;
                    Counter++;
                }
                else {
                    $scope.EntryGrdCol.CheckDate_INDEX = null;
                    $scope.EntryGrdCol.CheckDate_WIDTH = null;
                    $scope.EntryGrdCol.CheckDate_COLOR_HEXA = null;
                    $scope.EntryGrdCol.IsCheckDate = false;
                }
            }



            if (obj.ENTRY_SHOW_ID == 19) {
                if (obj.Selected === true) {
                    $scope.EntryGrdCol.CheckIssueDate_INDEX = Counter;
                    $scope.EntryGrdCol.CheckIssueDate_WIDTH = obj.WIDTH;
                    $scope.EntryGrdCol.CheckIssueDate_COLOR_HEXA = obj.Color;
                    $scope.EntryGrdCol.IsCheckIssueDate = true;
                    Counter++;
                }
                else {
                    $scope.EntryGrdCol.CheckIssueDate_INDEX = null;
                    $scope.EntryGrdCol.CheckIssueDate_WIDTH = null;
                    $scope.EntryGrdCol.CheckIssueDate_COLOR_HEXA = null;
                    $scope.EntryGrdCol.IsCheckIssueDate = false;
                }
            }



            if (obj.ENTRY_SHOW_ID == 20) {
                if (obj.Selected === true) {
                    $scope.EntryGrdCol.ExemptOfTax_INDEX = Counter;
                    $scope.EntryGrdCol.ExemptOfTax_WIDTH = obj.WIDTH;
                    $scope.EntryGrdCol.ExemptOfTax_COLOR_HEXA = obj.Color;
                    $scope.EntryGrdCol.IsExemptOfTax = true;
                    Counter++;
                }
                else {
                    $scope.EntryGrdCol.ExemptOfTax_INDEX = null;
                    $scope.EntryGrdCol.ExemptOfTax_WIDTH = null;
                    $scope.EntryGrdCol.ExemptOfTax_COLOR_HEXA = null;
                    $scope.EntryGrdCol.IsExemptOfTax = false;
                }
            }


        }


        if ($scope.EntryGrdCol.IS_CREDIT === false && $scope.EntryGrdCol.IS_DEBIT === false) {

            swal({
                //title: 'تم',
                text: 'يجب اختيار دائن او مدين',
                type: 'success',
                timer: 1500,
                showConfirmButton: false

            });
            $scope.valid = false;
        }


        if ($scope.EntryGrdCol.IS_ACC === false) {

            swal({
                //title: 'تم',
                text: 'يجب اختيار الحساب',
                type: 'success',
                timer: 1500,
                showConfirmButton: false
            });
            $scope.valid = false;
        }

        $scope.setGridData();

    };

    $scope.saveEntity = function () {
        if ($scope.Custom.Currency != undefined || $scope.Custom.Currency != null)
            $scope.entrySetting.CURRENCY_ID = $scope.Custom.Currency.CURRENCY_ID;
        else $scope.entrySetting.CURRENCY_ID = null;

        $scope.entrySetting.ENTRY_TYPE_ID = $scope.Custom.role.ENTRY_TYPE_ID;

        var addedBy = authService.GetUserName();
        var addeddOn = new Date();


        $scope.entrySetting.AddedBy = addedBy;
        $scope.entrySetting.AddedOn = addeddOn;


        if ($scope.entrySetting !== undefined && $scope.entrySetting !== null && $scope.entrySetting.ENTRY_SETTING_AR_NAME !== null && $scope.entrySetting.ENTRY_SETTING_EN_NAME !== null && $scope.entrySetting.ENTRY_SETTING_AR_ABRV !== null && $scope.entrySetting.ENTRY_SETTING_EN_ABRV !== null && $scope.entrySetting.CURRENCY_ID !== null) {
            if ($scope.entrySetting.ENTRY_SETTING_ID === null || $scope.ENTRY_SETTING_ID === 0) {
                var response = $scope.checkCreditDebit();//.then(function (response) {
                //alert(response);
                // alert(x);
                if (response === true) {
                    $scope.insert($scope.entrySetting).then(function (results) {
                        $scope.entrySetting.ENTRY_SETTING_ID = results.data;


                        $scope.settingID = results.data;

                        if ($scope.entrySetting.MenuID != undefined && $scope.entrySetting.MenuID != null && $scope.entrySetting.MenuID != 0) {
                            $scope.saveEntrySettingAsMenu();
                        }

                        $scope.SetEntryshowData(results.data);
                        if ($scope.valid == true) {
                            if ($scope.IsShow === true) {
                                $scope.IsShow = false;
                                entryGridColumnService.insert($scope.EntryGrdCol).then(function (response) {
                                    $scope.clearEnity();
                                    $scope.refreshData();
                                    swal({
                                        title: 'تم',
                                        text: 'حفظ بيانات إعدادات السندات بنجاح',
                                        type: 'success',
                                        timer: 1500,
                                        showConfirmButton: false
                                    });
                                });
                            }
                        }

                    }, function (error) {
                        console.log(error.data.message);
                        swal('عفواً',
                            'حدث خطأ عند حفظ إعدادات السندات',
                            'error');
                    });

                }
                //  }, function (error) { });


            } else {
                $scope.update($scope.entrySetting).then(function (results) {
                    $scope.SetEntryshowData($scope.entrySetting.ENTRY_SETTING_ID);

                    if ($scope.entrySetting.MenuID != undefined && $scope.entrySetting.MenuID != null && $scope.entrySetting.MenuID != 0) {
                        $scope.updateSettingMenu();
                    }
                    if ($scope.valid == true) {
                        if ($scope.IsShow === true) {
                            $scope.IsShow = false;
                            entryGridColumnService.update($scope.EntryGrdCol).then(function (response) {
                                $scope.clearEnity();
                                $scope.refreshData();
                                swal({
                                    title: 'تم',
                                    text: 'تعديل إعدادات السندات بنجاح',
                                    type: 'success',
                                    timer: 1500,
                                    showConfirmButton: false
                                });
                            });
                        }
                    }
                }, function (error) {
                    console.log(error.data.message);
                    swal('عفواً',
                        'حدث خطأ عند تعديل إعدادات السندات',
                        'error');
                });
            }
        }
    }

    $scope.getEntryshowData = function (shwOpt) {
        for (var i = 0; i < $scope.entryShowList.length; i++) {
            if ($scope.entryShowList[i].ENTRY_SHOW_ID === 1) {
                $scope.entryShowList[i].INDEX = shwOpt.CREDIT_INDEX;
                $scope.entryShowList[i].WIDTH = shwOpt.CREDIT_WIDTH;
                $scope.entryShowList[i].Color = shwOpt.CREDIT_COLOR_HEXA;
            }

            else if ($scope.entryShowList[i].ENTRY_SHOW_ID === 2) {
                $scope.entryShowList[i].INDEX = shwOpt.DEBIT_INDEX;
                $scope.entryShowList[i].WIDTH = shwOpt.DEBIT_WIDTH;
                $scope.entryShowList[i].Color = shwOpt.DEBIT_COLOR_HEXA;
            }

            else if ($scope.entryShowList[i].ENTRY_SHOW_ID === 3) {
                $scope.entryShowList[i].INDEX = shwOpt.ACC_INDEX;
                $scope.entryShowList[i].WIDTH = shwOpt.ACC_WIDTH;
                $scope.entryShowList[i].Color = shwOpt.ACC_COLOR_HEXA;
            }

            else if ($scope.entryShowList[i].ENTRY_SHOW_ID === 4) {
                $scope.entryShowList[i].INDEX = shwOpt.COST_CENTER_INDEX;
                $scope.entryShowList[i].WIDTH = shwOpt.COST_CENTER_WIDTH;
                $scope.entryShowList[i].Color = shwOpt.COST_CENTER_COLOR_HEXA;
            }

            else if ($scope.entryShowList[i].ENTRY_SHOW_ID === 5) {
                $scope.entryShowList[i].INDEX = shwOpt.REMARKS_INDEX;
                $scope.entryShowList[i].WIDTH = shwOpt.REMARKS_WIDTH;
                $scope.entryShowList[i].Color = shwOpt.REMARKS_COLOR_HEXA;
            }

            else if ($scope.entryShowList[i].ENTRY_SHOW_ID === 6) {
                $scope.entryShowList[i].INDEX = shwOpt.TaxRate_INDEX;
                $scope.entryShowList[i].WIDTH = shwOpt.TaxRate_WIDTH;
                $scope.entryShowList[i].Color = shwOpt.TaxRate_COLOR_HEXA;
            }

            else if ($scope.entryShowList[i].ENTRY_SHOW_ID === 7) {
                $scope.entryShowList[i].INDEX = shwOpt.Taxable_INDEX;
                $scope.entryShowList[i].WIDTH = shwOpt.Taxable_WIDTH;
                $scope.entryShowList[i].Color = shwOpt.Taxable_COLOR_HEXA;
            }

            else if ($scope.entryShowList[i].ENTRY_SHOW_ID === 8) {
                $scope.entryShowList[i].INDEX = shwOpt.GoldCredit24_INDEX;
                $scope.entryShowList[i].WIDTH = shwOpt.GoldCredit24_WIDTH;
                $scope.entryShowList[i].Color = shwOpt.GoldCredit24_COLOR_HEXA;
            }

            else if ($scope.entryShowList[i].ENTRY_SHOW_ID === 9) {
                $scope.entryShowList[i].INDEX = shwOpt.GoldDepit24_INDEX;
                $scope.entryShowList[i].WIDTH = shwOpt.GoldDepit24_WIDTH;
                $scope.entryShowList[i].Color = shwOpt.GoldDepit24_COLOR_HEXA;
            }

            else if ($scope.entryShowList[i].ENTRY_SHOW_ID === 10) {
                $scope.entryShowList[i].INDEX = shwOpt.GoldCredit22_INDEX;
                $scope.entryShowList[i].WIDTH = shwOpt.GoldCredit22_WIDTH;
                $scope.entryShowList[i].Color = shwOpt.GoldCredit22_COLOR_HEXA;
            }

            else if ($scope.entryShowList[i].ENTRY_SHOW_ID === 11) {
                $scope.entryShowList[i].INDEX = shwOpt.GoldDepit22_INDEX;
                $scope.entryShowList[i].WIDTH = shwOpt.GoldDepit22_WIDTH;
                $scope.entryShowList[i].Color = shwOpt.GoldDepit22_COLOR_HEXA;
            }

            else if ($scope.entryShowList[i].ENTRY_SHOW_ID === 12) {
                $scope.entryShowList[i].INDEX = shwOpt.GoldCredit21_INDEX;
                $scope.entryShowList[i].WIDTH = shwOpt.GoldCredit21_WIDTH;
                $scope.entryShowList[i].Color = shwOpt.GoldCredit21_COLOR_HEXA;
            }

            else if ($scope.entryShowList[i].ENTRY_SHOW_ID === 13) {
                $scope.entryShowList[i].INDEX = shwOpt.GoldDepit21_INDEX;
                $scope.entryShowList[i].WIDTH = shwOpt.GoldDepit21_WIDTH;
                $scope.entryShowList[i].Color = shwOpt.GoldDepit21_COLOR_HEXA;
            }


            else if ($scope.entryShowList[i].ENTRY_SHOW_ID === 14) {
                $scope.entryShowList[i].INDEX = shwOpt.GoldCredit21_INDEX;
                $scope.entryShowList[i].WIDTH = shwOpt.GoldCredit21_WIDTH;
                $scope.entryShowList[i].Color = shwOpt.GoldCredit21_COLOR_HEXA;
            }

            else if ($scope.entryShowList[i].ENTRY_SHOW_ID === 15) {
                $scope.entryShowList[i].INDEX = shwOpt.GoldDepit18_INDEX;
                $scope.entryShowList[i].WIDTH = shwOpt.GoldDepit18_WIDTH;
                $scope.entryShowList[i].Color = shwOpt.GoldDepit18_COLOR_HEXA;
            }


            else if ($scope.entryShowList[i].ENTRY_SHOW_ID === 16) {
                $scope.entryShowList[i].INDEX = shwOpt.TaxValue_INDEX;
                $scope.entryShowList[i].WIDTH = shwOpt.TaxValue_WIDTH;
                $scope.entryShowList[i].Color = shwOpt.TaxValue_COLOR_HEXA;
            }



            else if ($scope.entryShowList[i].ENTRY_SHOW_ID === 17) {
                $scope.entryShowList[i].INDEX = shwOpt.CheckNumber_INDEX;
                $scope.entryShowList[i].WIDTH = shwOpt.CheckNumber_WIDTH;
                $scope.entryShowList[i].Color = shwOpt.CheckNumber_COLOR_HEXA;
            }

            else if ($scope.entryShowList[i].ENTRY_SHOW_ID === 18) {
                $scope.entryShowList[i].INDEX = shwOpt.CheckDate_INDEX;
                $scope.entryShowList[i].WIDTH = shwOpt.CheckDate_WIDTH;
                $scope.entryShowList[i].Color = shwOpt.CheckDate_COLOR_HEXA;
            }

            else if ($scope.entryShowList[i].ENTRY_SHOW_ID === 19) {
                $scope.entryShowList[i].INDEX = shwOpt.CheckIssueDate_INDEX;
                $scope.entryShowList[i].WIDTH = shwOpt.CheckIssueDate_WIDTH;
                $scope.entryShowList[i].Color = shwOpt.CheckIssueDate_COLOR_HEXA;
            }


            else if ($scope.entryShowList[i].ENTRY_SHOW_ID === 20) {
                $scope.entryShowList[i].INDEX = shwOpt.ExemptOfTax_INDEX;
                $scope.entryShowList[i].WIDTH = shwOpt.ExemptOfTax_WIDTH;
                $scope.entryShowList[i].Color = shwOpt.ExemptOfTax_COLOR_HEXA;
            }


        }

        $scope.entryShowList.sort(function (a, b) {
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
        for (var i = 0; i < $scope.entryShowList.length; i++) {
            if ($scope.entryShowList[i].INDEX > 0 && $scope.entryShowList[i].INDEX != null) {
                $scope.entryShowList[i].Selected = true;
            } else {
                $scope.entryShowList[i].Selected = false;
            }
        }
    }

    $scope.dirEnity = function (entity) {
        if (entity.ENTRY_ACC_ID != null && entity.ENTRY_ACC_ID != undefined && entity.ENTRY_ACC_ID != "") {
            $scope.getAccountNameBasedOnAccountID(entity.ENTRY_ACC_ID).then(function (result) {
                $scope.Custom.ACC_Name = result.data.ACC_AR_NAME;
            });
        } else { $scope.Custom.ACC_Name = ""; }

        $scope.Custom.role = $scope.entryTypeList[+entity.ENTRY_TYPE_ID - 1];
        $scope.Custom.Currency = $scope.getCurrencyBasedOnCurrencyID(entity.CURRENCY_ID);
        $scope.entrySetting = entity;


        entryGridColumnService.getByID(entity.ENTRY_SETTING_ID).then(function (result) {
            debugger
            $scope.getEntryshowData(result.data);
            $scope.getGridData(result.data);
        });


        MenuService.getMenuByEntrySettingID(entity.ENTRY_SETTING_ID).then(function (result) {
            if (result.data != null) {
                var menu = result.data;
                $scope.menu.menuId = menu.MenuID;
                $scope.userMenuID = menu.ID;
            }
        });
    }

    $scope.deleteEnity = function (entity) {
        swal({
            title: 'هل تريد حذف إعدادات السندات؟',
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


    $scope.$watch('Custom.Currency', function (newvalue, oldvalue) {
        $scope.getCurrencyRate();
    });
    $scope.getCurrencyRate = function () {
        if ($scope.Custom.Currency != "" && $scope.Custom.Currency != undefined) {
            var curID = $scope.Custom.Currency.CURRENCY_ID;
            if (curID != "" && curID != undefined)
                currencyService.getCurrencyRate(curID).then(
                    function (result) {
                        $scope.Custom.CURRENCY_RATE = result.data;
                    }, function (error) {
                        console.log(error.data.message);
                    });
            else $scope.Custom.CURRENCY_RATE = "";
        } else $scope.Custom.CURRENCY_RATE = "";
    };

    $scope.getentryShowList = function () {
        entryShowOptionService.getAll().then(function (results) {
            $scope.entryShowList = {};
            $scope.uiGridOptions.data = {};

            var flag = !$scope.getByUserModel(8);
            var temp = [];
            var data = results.data;
            for (var i = 0; i < data.length; i++) {

                if (flag || $scope.GoldSetting == false) {
                  
                    if (data[i].ENTRY_SHOW_AR_NAME == "دائن عيار 24" || data[i].ENTRY_SHOW_AR_NAME == "مدين عيار 24" ||
                        data[i].ENTRY_SHOW_AR_NAME == "دائن عيار 22" || data[i].ENTRY_SHOW_AR_NAME == "مدين عيار 22"||
                        data[i].ENTRY_SHOW_AR_NAME == "دائن عيار 21" || data[i].ENTRY_SHOW_AR_NAME == "مدين عيار 21" ||
                        data[i].ENTRY_SHOW_AR_NAME == "دائن عيار 18" || data[i].ENTRY_SHOW_AR_NAME == "مدين عيار 18")
                    {
                        //temp.push(data[i]);
                        //temp[temp.length - 1].isShow = false;
                    } else {
                        temp.push(data[i]);
                        temp[temp.length - 1].Selected = true;
                        temp[temp.length - 1].isShow = true;
                        temp[temp.length - 1].WIDTH = "";
                        temp[temp.length - 1].Color = "#000000";
                        if (temp[temp.length - 1].ENTRY_SHOW_ID == 3) {
                            temp[temp.length - 1].disapled = true;
                            
                        }
                    }

                } else {
                    temp.push(data[i]);
                    temp[temp.length - 1].Selected = true;
                    temp[temp.length - 1].isShow = true;
                    temp[temp.length - 1].WIDTH = "";
                    temp[temp.length - 1].Color = "#000000";
                    if (temp[temp.length - 1].ENTRY_SHOW_ID == 3) {
                        temp[temp.length - 1].disapled = true;
                    }
                }
            }

            $scope.uiGridOptions.data = temp;
            return $scope.entryShowList = temp;
        }, function (error) {
            console.log(error.data.message);
        });
    }
    $scope.flagHide = false;
    $scope.getAllOnLoad = function () {
        debugger
        $q.all(
            [
                $scope.getentrySettingList(),
                $scope.getentrySettingTotalCount(),
                $scope.getCurrencyList(),
                $scope.getEntryTypesList(),
                $scope.getentryShowList(),
                $scope.getAccountsList(),
                $scope.getMainMenus(),
                $scope.getUserModel()
            ]).then(function (allResponses) {

                var urlParams = $location.search();
                if (urlParams.foo != null && urlParams.foo != undefined && urlParams.foo != "") {

                    entrySettingService.getBySettingID(parseInt(urlParams.foo)).then(function (results) {
                        $scope.entrySetting = results.data;
                        $scope.dirEnity(results.data);
                    })


                }
                var flag = !$scope.getByUserModel(8);
                if (flag || $scope.GoldSetting == false) { $scope.flagHide = true }
            }, function (error) {
                //This will be called if $q.all finds any of the requests erroring.
                var abc = error;
                var def = 99;
            });
    }
    $scope.getentrySettingTotalCount = function () {
        $scope.count().then(function (results) {
            var data = results.data;
            $scope.entrySettingTotalCount = data;
            debugger
            $scope.pagesCount = $scope.getPagesCount();
        }, function (error) {
            console.log(error.data.message);
        });
    }
    $scope.getentrySettingList = function () {

        $scope.get($scope.pageNum, $scope.pageSize).then(function (results) {
            debugger
            var data = results.data;
            $scope.entrySettingList = data;

        }, function (error) {
            console.log(error.data.message);
        });
    }

    $scope.getCurrencyList = function () {
        currencyService.getAll().then(function (results) {
            debugger
            $scope.CurrencyList = results.data;
        }, function (error) {
            console.log(error.data.message);
        });
    }

    $scope.getEntryTypesList = function () {
        entryTypeService.getAll().then(function (results) {
            $scope.entryTypeList = results.data;
            $scope.Custom.role = $scope.entryTypeList[0];
        }, function (error) {
            console.log(error.data.message);
        });
    }

    $scope.get = function (pageNum, pageSize) {
        return entrySettingService.get(pageNum, pageSize);
    }
    $scope.count = function () {
        return entrySettingService.count();
    }
    $scope.insert = function (entity) {
        return entrySettingService.insert(entity);
    }
    $scope.update = function (entity) {
        return entrySettingService.update(entity);
    }
    $scope.delete = function (entity) {
        return entrySettingService.delete(entity);
    }
    $scope.entrySettingPageLoad = function () {
        $scope.getAllOnLoad();
    }

    $scope.pageChanged = function () {
        $scope.getAllOnLoad();
        $log.log('Page changed to: ' + $scope.pageNum);
    };
    /*******************************************************************************************************************/
    /*Choose Account*/
    /*******************************************************************************************************************/
    $scope.openItm = function () {
        var parentElem = angular.element($document[0].querySelector('.app-content'));
        $scope.modalOpenItm = false;
        var modalInstance = $uibModal.open({
            templateUrl: 'accountModal.html',
            controller: 'modalCtrl',
            scope: $scope,
            size: 'lg',
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

    $scope.acc = {};
    $scope.acc.pageNumA = 1;
    $scope.acc.pageSizeA = 10;
    $scope.acc.pagesCountA = 0;
    $scope.maxSizeA = 5;
    $scope.accountTotalCount = 0;

    $scope.getPagesCountA = function () {
        var div1 = Math.floor($scope.accountTotalCount / $scope.acc.pageSizeA);
        var rem11 = $scope.accountTotalCount % $scope.acc.pageSizeA;
        if (rem11 > 0) {
            div1 = div1 + 1;
        }
        return div1;
    }

    $scope.searchForItm = function () {
        var search = $scope.Custom.ACC_Name;
        $scope.getAccountTotalCount(search);
        if ($scope.acc.pageNumA != 1 && $scope.acc.pagesCountA <= $scope.acc.pageNumA) {
            $scope.acc.pageNumA = $scope.acc.pagesCountA;
        }
        $scope.getAccounts(search, $scope.acc.pageNumA, $scope.acc.pageSizeA).then(function (response) {
            $scope.searchItems = response.data;
            if ($scope.searchItems.length == 1) {
                $scope.Custom.ACC_Name = $scope.searchItems[0].ACC_AR_NAME;
                $scope.entrySetting.ENTRY_ACC_ID = $scope.searchItems[0].ACC_ID;
            }
            else if ($scope.searchItems.length == 0) {
                $scope.Custom.ACC_Name = "";
                $scope.entrySetting.ENTRY_ACC_ID = "";
                return;
            }
            else {
                if ($scope.modalOpenItm === false || $scope.modalOpenItm == undefined) { $scope.openItm(); }
            }

        }, function (err) {
            console.log(err.data.message);
        });
    }

    $scope.getAccounts = function (search, pageNumA, pageSizeA) {
        search = search == undefined || search == "" ? "" : search;
        return accountService.getSearchForAccount(search, pageNumA, pageSizeA);
    }

    $scope.getAccountTotalCount = function (search) {
        search = search == undefined || search == "" ? "" : search;

        accountService.countSearch(search).then(function (results) {
            var data = results.data;
            $scope.accountTotalCount = data;
            $scope.acc.pagesCountA = $scope.getPagesCountA();
        }, function (error) {
            console.log(error.data.message);
        });
    }

    $scope.addSelectedItm = function (itm) {
        $scope.Custom.ACC_Name = itm.ACC_AR_NAME;
        $scope.entrySetting.ENTRY_ACC_ID = itm.ACC_ID;
        $uibModalStack.dismissAll();
        ////$scope.acc.pageNumA = 1;
    };

    $scope.pageChangedA = function () {
        $scope.searchForItm();
        //$scope.getAccounts($scope.Custom.ACC_Name, $scope.acc.pageNumA, $scope.pageSizeA);
        $log.log('Page changed to: ' + $scope.acc.pageNumA);
    };

    /*******************************************************************************************************************************/

    $scope.steps = [
        'بيانات السند',
        'نوع السند',
        'حقول السند',
        'بيانات الاظهار',
        'اعدادات الطباعه'
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



    $scope.checkCreditDebit = function () {
        var Counter = 1;
        var isCredit = false;
        var isDepit = false;
        var result = true;
        for (var i = 0; i < $scope.entryShowList.length; i++) {
            var obj = $scope.entryShowList[i];
            if (obj.ENTRY_SHOW_ID == 1) {
                if (obj.Selected === true) {

                    isCredit = true;
                    Counter++;
                }
                else {

                    isCredit = false;
                }
            }
            if (obj.ENTRY_SHOW_ID == 2) {
                if (obj.Selected === true) {

                    isDepit = true;
                    Counter++;
                }
                else {

                    isDepit = false;
                }
            }
        }

        if (isCredit === false && isDepit === false) {
            swal({
                //title: 'تم',
                text: 'يجب اختيار دائن او مدين',
                type: 'success',
                timer: 1500,
                showConfirmButton: false
            });
            result = false;
        }
        return result;

    };

    /////////////////////////////////////////////////////accounts
    $scope.getAccountsList = function () {
        accountsService.getAccountBoxByTypesForEntry(0).then(function (result) {
            //accountsService.getAllAccounts().then(function (result) {
            debugger
            $scope.accountList = result.data;
            $scope.taxAccountList = result.data;
        });
    }

    /////////////////////////////////////grid dataa of columns////////////////////////////////////////////////////////////
    $scope.setGridData = function () {
        $scope.EntryGrdCol.ARCode = $scope.gridData.ARCode;
        $scope.EntryGrdCol.ENCode = $scope.gridData.ENCode;
        $scope.EntryGrdCol.ARAccountName = $scope.gridData.ARAccountName;
        $scope.EntryGrdCol.ENAccountName = $scope.gridData.ENAccountName;
        $scope.EntryGrdCol.ARCredit = $scope.gridData.ARCredit;
        $scope.EntryGrdCol.ENCredit = $scope.gridData.ENCredit;
        $scope.EntryGrdCol.ARDepit = $scope.gridData.ARDepit;
        $scope.EntryGrdCol.ENDepit = $scope.gridData.ENDepit;
        $scope.EntryGrdCol.ARCredit24 = $scope.gridData.ARCredit24;
        $scope.EntryGrdCol.ENCredit24 = $scope.gridData.ENCredit24;
        $scope.EntryGrdCol.ARDepit24 = $scope.gridData.ARDepit24;
        $scope.EntryGrdCol.ENDepit24 = $scope.gridData.ENDepit24;
        $scope.EntryGrdCol.ARCredit22 = $scope.gridData.ARCredit22;
        $scope.EntryGrdCol.ENCredit22 = $scope.gridData.ENCredit22;
        $scope.EntryGrdCol.ARDepit22 = $scope.gridData.ARDepit22;
        $scope.EntryGrdCol.ENDepit22 = $scope.gridData.ENDepit22;
        $scope.EntryGrdCol.ARCredit21 = $scope.gridData.ARCredit21;
        $scope.EntryGrdCol.ENCredit21 = $scope.gridData.ENCredit21;
        $scope.EntryGrdCol.ARDepit21 = $scope.gridData.ARDepit21;
        $scope.EntryGrdCol.ENDepit21 = $scope.gridData.ENDepit21;
        $scope.EntryGrdCol.ARCredit18 = $scope.gridData.ARCredit18;
        $scope.EntryGrdCol.ENCredit18 = $scope.gridData.ENCredit18;
        $scope.EntryGrdCol.ARDepit18 = $scope.gridData.ARDepit18;
        $scope.EntryGrdCol.ENDepit18 = $scope.gridData.ENDepit18;
        $scope.EntryGrdCol.ARCostCenter = $scope.gridData.ARCostCenter;
        $scope.EntryGrdCol.ENCostCenter = $scope.gridData.ENCostCenter;
        $scope.EntryGrdCol.ARRemarks = $scope.gridData.ARRemarks;
        $scope.EntryGrdCol.ENRemarks = $scope.gridData.ENRemarks;
        $scope.EntryGrdCol.ARTaxable = $scope.gridData.ARTaxable;
        $scope.EntryGrdCol.EnTaxable = $scope.gridData.EnTaxable;


        $scope.EntryGrdCol.ARTaxRate = $scope.gridData.ARTaxRate;
        $scope.EntryGrdCol.ENTaxRate = $scope.gridData.ENTaxRate;

        $scope.EntryGrdCol.ARTaxValue = $scope.gridData.ARTaxValue;
        $scope.EntryGrdCol.ENTaxValue = $scope.gridData.ENTaxValue;



        $scope.EntryGrdCol.ARCheckNumber = $scope.gridData.ARCheckNumber;
        $scope.EntryGrdCol.ENCheckNumber = $scope.gridData.ENCheckNumber;

        $scope.EntryGrdCol.ARCheckDate = $scope.gridData.ARCheckDate;
        $scope.EntryGrdCol.ENCheckDate = $scope.gridData.ENCheckDate;

        $scope.EntryGrdCol.ARCheckIssueDate = $scope.gridData.ARCheckIssueDate;
        $scope.EntryGrdCol.ENCheckIssueDate = $scope.gridData.ENCheckIssueDate;

        $scope.EntryGrdCol.ARExemptOfTax = $scope.gridData.ARExemptOfTax;
        $scope.EntryGrdCol.ENExemptOfTax = $scope.gridData.ENExemptOfTax;
    }


    $scope.getGridData = function (gridCol) {


        $scope.defaultGridData = {
            ARCode: 'كود', ENCode: 'Code', ARAccountName: 'الحساب', ENAccountName: 'Account', ARCredit: 'دائن', ENCredit: 'Credit', ARDepit: 'مدين', ENDepit: 'Depit', ARCredit24: 'دائن 24', ENCredit24: 'Credit 24'
            , ARDepit24: 'مدين 24', ENDepit24: 'Depit 24', ARCredit22: 'دائن 22', ENCredit22: 'Credit 22', ARDepit22: 'مدين 22', ENDepit22: 'Depit 22', ARCredit21: 'دائن 21', ENCredit21: 'Credit 21'
            , ARDepit21: 'مدين 21', ENDepit21: 'Depit 21', ARCredit18: 'دائن 18', ENCredit18: 'Credit 18', ARDepit18: 'مدين 18', ENDepit18: 'Depit 18', ARCostCenter: ' مركز التكلفه', ENCostCenter: 'Cost Center'
            , ARRemarks: 'شرح', ENRemarks: 'Remarks', ARTaxable: 'خاضع للضريبه', EnTaxable: 'Taxable', ARTaxRate: 'نسبة الضريبه', ENTaxRate: 'Tax Rate', ARTaxValue: 'قيمة الضريبه', ENTaxValue: 'Tax Value'
            , ARCheckNumber: 'رقم الشيك', ENCheckNumber: 'Check Number', ARCheckDate: 'تاريخ الشيك', ENCheckDate: 'Check Date', ARCheckIssueDate: 'تاريخ الاستحقاق', ENCheckIssueDate: 'Check Issue Date'
            , ARExemptOfTax: 'معفي من الضريبه', ENExemptOfTax: 'Exempt of tax'
        }


        if (gridCol.ARCode == "" || gridCol.ARCode == undefined) {
            $scope.gridData.ARCode = $scope.defaultGridData.ARCode;
        }
        else {
            $scope.gridData.ARCode = gridCol.ARCode;
        }



        if (gridCol.ARAccountName == "" || gridCol.ARAccountName == undefined) {
            $scope.gridData.ARAccountName = $scope.defaultGridData.ARAccountName;
        }
        else {
            $scope.gridData.ARAccountName = gridCol.ARAccountName;
        }


        if (gridCol.ARCredit == "" || gridCol.ARCredit == undefined) {
            $scope.gridData.ARCredit = $scope.defaultGridData.ARCredit;
        }
        else {
            $scope.gridData.ARCredit = gridCol.ARCredit;
        }

        if (gridCol.ARDepit == "" || gridCol.ARDepit == undefined) {
            $scope.gridData.ARDepit = $scope.defaultGridData.ARDepit;
        }
        else {
            $scope.gridData.ARDepit = gridCol.ARDepit;
        }

        if (gridCol.ARCredit24 == "" || gridCol.ARCredit24 == undefined) {
            $scope.gridData.ARCredit24 = $scope.defaultGridData.ARCredit24;
        }
        else {
            $scope.gridData.ARCredit24 = gridCol.ARCredit24;
        }

        if (gridCol.ARDepit24 == "" || gridCol.ARDepit24 == undefined) {
            $scope.gridData.ARDepit24 = $scope.defaultGridData.ARDepit24;
        }
        else {
            $scope.gridData.ARDepit24 = gridCol.ARDepit24;
        }


        if (gridCol.ARCredit22 == "" || gridCol.ARCredit22 == undefined) {
            $scope.gridData.ARCredit22 = $scope.defaultGridData.ARCredit22;
        }
        else {
            $scope.gridData.ARCredit22 = gridCol.ARCredit22;
        }


        if (gridCol.ARDepit22 == "" || gridCol.ARDepit22 == undefined) {
            $scope.gridData.ARDepit22 = $scope.defaultGridData.ARDepit22;
        }
        else {
            $scope.gridData.ARDepit22 = gridCol.ARDepit22;
        }


        if (gridCol.ARCredit21 == "" || gridCol.ARCredit21 == undefined) {
            $scope.gridData.ARCredit21 = $scope.defaultGridData.ARCredit21;
        }
        else {
            $scope.gridData.ARCredit21 = gridCol.ARCredit21;
        }


        if (gridCol.ARDepit21 == "" || gridCol.ARDepit21 == undefined) {
            $scope.gridData.ARDepit21 = $scope.defaultGridData.ARDepit21;
        }
        else {
            $scope.gridData.ARDepit21 = gridCol.ARDepit21;
        }


        if (gridCol.ARCredit18 == "" || gridCol.ARCredit18 == undefined) {
            $scope.gridData.ARCredit18 = $scope.defaultGridData.ARCredit18;
        }
        else {
            $scope.gridData.ARCredit18 = gridCol.ARCredit18;
        }


        if (gridCol.ARDepit18 == "" || gridCol.ARDepit18 == undefined) {
            $scope.gridData.ARDepit18 = $scope.defaultGridData.ARDepit18;
        }
        else {
            $scope.gridData.ARDepit18 = gridCol.ARDepit18;
        }



        if (gridCol.ARCostCenter == "" || gridCol.ARCostCenter == undefined) {
            $scope.gridData.ARCostCenter = $scope.defaultGridData.ARCostCenter;
        }
        else {
            $scope.gridData.ARCostCenter = gridCol.ARCostCenter;
        }


        if (gridCol.ARRemarks == "" || gridCol.ARRemarks == undefined) {
            $scope.gridData.ARRemarks = $scope.defaultGridData.ARRemarks;
        }
        else {
            $scope.gridData.ARRemarks = gridCol.ARRemarks;
        }


        if (gridCol.ARTaxable == "" || gridCol.ARTaxable == undefined) {
            $scope.gridData.ARTaxable = $scope.defaultGridData.ARTaxable;
        }
        else {
            $scope.gridData.ARTaxable = gridCol.ARTaxable;
        }


        if (gridCol.ARTaxRate == "" || gridCol.ARTaxRate == undefined) {
            $scope.gridData.ARTaxRate = $scope.defaultGridData.ARTaxRate;
        }
        else {
            $scope.gridData.ARTaxRate = gridCol.ARTaxRate;
        }

        if (gridCol.ARTaxValue == "" || gridCol.ARTaxValue == undefined) {
            $scope.gridData.ARTaxValue = $scope.defaultGridData.ARTaxValue;
        }
        else {
            $scope.gridData.ARTaxValue = gridCol.ARTaxValue;
        }
        ////////////english/////////////

        if (gridCol.ENCode == "" || gridCol.ENCode == undefined) {
            $scope.gridData.ENCode = $scope.defaultGridData.ENCode;
        }
        else {
            $scope.gridData.ENCode = gridCol.ENCode;
        }



        if (gridCol.ENAccountName == "" || gridCol.ENAccountName == undefined) {
            $scope.gridData.ENAccountName = $scope.defaultGridData.ENAccountName;
        }
        else {
            $scope.gridData.ENAccountName = gridCol.ENAccountName;
        }


        if (gridCol.ENCredit == "" || gridCol.ENCredit == undefined) {
            $scope.gridData.ENCredit = $scope.defaultGridData.ENCredit;
        }
        else {
            $scope.gridData.ENCredit = gridCol.ENCredit;
        }

        if (gridCol.ENDepit == "" || gridCol.ENDepit == undefined) {
            $scope.gridData.ENDepit = $scope.defaultGridData.ENDepit;
        }
        else {
            $scope.gridData.ENDepit = gridCol.ENDepit;
        }

        if (gridCol.ENCredit24 == "" || gridCol.ENCredit24 == undefined) {
            $scope.gridData.ENCredit24 = $scope.defaultGridData.ENCredit24;
        }
        else {
            $scope.gridData.ENCredit24 = gridCol.ENCredit24;
        }

        if (gridCol.ENDepit24 == "" || gridCol.ENDepit24 == undefined) {
            $scope.gridData.ENDepit24 = $scope.defaultGridData.ENDepit24;
        }
        else {
            $scope.gridData.ENDepit24 = gridCol.ENDepit24;
        }


        if (gridCol.ENCredit22 == "" || gridCol.ENCredit22 == undefined) {
            $scope.gridData.ENCredit22 = $scope.defaultGridData.ENCredit22;
        }
        else {
            $scope.gridData.ENCredit22 = gridCol.ENCredit22;
        }


        if (gridCol.ENDepit22 == "" || gridCol.ENDepit22 == undefined) {
            $scope.gridData.ENDepit22 = $scope.defaultGridData.ENDepit22;
        }
        else {
            $scope.gridData.ENDepit22 = gridCol.ENDepit22;
        }


        if (gridCol.ENCredit21 == "" || gridCol.ENCredit21 == undefined) {
            $scope.gridData.ENCredit21 = $scope.defaultGridData.ENCredit21;
        }
        else {
            $scope.gridData.ENCredit21 = gridCol.ENCredit21;
        }


        if (gridCol.ENDepit21 == "" || gridCol.ENDepit21 == undefined) {
            $scope.gridData.ENDepit21 = $scope.defaultGridData.ENDepit21;
        }
        else {
            $scope.gridData.ENDepit21 = gridCol.ENDepit21;
        }


        if (gridCol.ENCredit18 == "" || gridCol.ENCredit18 == undefined) {
            $scope.gridData.ENCredit18 = $scope.defaultGridData.ENCredit18;
        }
        else {
            $scope.gridData.ENCredit18 = gridCol.ENCredit18;
        }


        if (gridCol.ENDepit18 == "" || gridCol.ENDepit18 == undefined) {
            $scope.gridData.ENDepit18 = $scope.defaultGridData.ENDepit18;
        }
        else {
            $scope.gridData.ENDepit18 = gridCol.ENDepit18;
        }



        if (gridCol.ENCostCenter == "" || gridCol.ENCostCenter == undefined) {
            $scope.gridData.ENCostCenter = $scope.defaultGridData.ENCostCenter;
        }
        else {
            $scope.gridData.ENCostCenter = gridCol.ENCostCenter;
        }


        if (gridCol.ENRemarks == "" || gridCol.ENRemarks == undefined) {
            $scope.gridData.ENRemarks = $scope.defaultGridData.ENRemarks;
        }
        else {
            $scope.gridData.ENRemarks = gridCol.ENRemarks;
        }


        if (gridCol.ENTaxable == "" || gridCol.ENTaxable == undefined) {
            $scope.gridData.ENTaxable = $scope.defaultGridData.ENTaxable;
        }
        else {
            $scope.gridData.ENTaxable = gridCol.ENTaxable;
        }


        if (gridCol.ENTaxRate == "" || gridCol.ENTaxRate == undefined) {
            $scope.gridData.ENTaxRate = $scope.defaultGridData.ENTaxRate;
        }
        else {
            $scope.gridData.ENTaxRate = gridCol.ENTaxRate;
        }


        if (gridCol.ENTaxValue == "" || gridCol.ENTaxValue == undefined) {
            $scope.gridData.ENTaxValue = $scope.defaultGridData.ENTaxValue;
        }
        else {
            $scope.gridData.ENTaxValue = gridCol.ENTaxValue;
        }

        ////////////

        if (gridCol.ARCheckNumber == "" || gridCol.ARCheckNumber == undefined) {
            $scope.gridData.ARCheckNumber = $scope.defaultGridData.ARCheckNumber;
        }
        else {
            $scope.gridData.ARCheckNumber = gridCol.ARCheckNumber;
        }


        if (gridCol.ENCheckNumber == "" || gridCol.ENCheckNumber == undefined) {
            $scope.gridData.ENCheckNumber = $scope.defaultGridData.ENCheckNumber;
        }
        else {
            $scope.gridData.ENCheckNumber = gridCol.ENCheckNumber;
        }


        if (gridCol.ARCheckDate == "" || gridCol.ARCheckDate == undefined) {
            $scope.gridData.ARCheckDate = $scope.defaultGridData.ARCheckDate;
        }
        else {
            $scope.gridData.ARCheckDate = gridCol.ARCheckDate;
        }



        if (gridCol.ENCheckDate == "" || gridCol.ENCheckDate == undefined) {
            $scope.gridData.ENCheckDate = $scope.defaultGridData.ENCheckDate;
        }
        else {
            $scope.gridData.ENCheckDate = gridCol.ENCheckDate;
        }


        if (gridCol.ARCheckIssueDate == "" || gridCol.ARCheckIssueDate == undefined) {
            $scope.gridData.ARCheckIssueDate = $scope.defaultGridData.ARCheckIssueDate;
        }
        else {
            $scope.gridData.ARCheckIssueDate = gridCol.ARCheckIssueDate;
        }


        if (gridCol.ENCheckIssueDate == "" || gridCol.ENCheckIssueDate == undefined) {
            $scope.gridData.ENCheckIssueDate = $scope.defaultGridData.ENCheckIssueDate;
        }
        else {
            $scope.gridData.ENCheckIssueDate = gridCol.ENCheckIssueDate;
        }

        ////////////

        if (gridCol.ARExemptOfTax == "" || gridCol.ARExemptOfTax == undefined) {
            $scope.gridData.ARExemptOfTax = $scope.defaultGridData.ARExemptOfTax;
        }
        else {
            $scope.gridData.ARExemptOfTax = gridCol.ARExemptOfTax;
        }


        if (gridCol.ENExemptOfTax == "" || gridCol.ENExemptOfTax == undefined) {
            $scope.gridData.ENExemptOfTax = $scope.defaultGridData.ENExemptOfTax;
        }
        else {
            $scope.gridData.ENExemptOfTax = gridCol.ENExemptOfTax;
        }
    }


    ///////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////user menu ///////////////////////////////////////////////////
    $scope.getMainMenus = function () {
        MenuService.getMainMenu().then(function (result) {
            $scope.mainMenuList = result.data;
        });
    }

    $scope.saveEntrySettingAsMenu = function () {
        $scope.userMenu = {};
        $scope.userMenu.ARName = $scope.entrySetting.ENTRY_SETTING_AR_NAME;
        $scope.userMenu.LatName = $scope.entrySetting.ENTRY_SETTING_EN_NAME;
        $scope.userMenu.MenuLink = "#/entry?entryType=" + $scope.settingID;
        $scope.userMenu.MenuID = $scope.entrySetting.MenuID;
        $scope.userMenu.CountryID = 1;
        $scope.userMenu.DisplayOrNot = 1;
        $scope.userMenu.UserShortcut = 0;
        $scope.userMenu.EntrySettingID = $scope.settingID;
        $scope.userMenu.Active = true;
        $scope.userMenu.AddedOn = new Date();
        var addedBy = authService.GetUserName();
        $scope.userMenu.AddedBy = addedBy;

        MenuService.add($scope.userMenu).then(function (result) {
            return result.data;
        });
    }


    $scope.updateSettingMenu = function () {
        $scope.userMenu = {};
        $scope.userMenu.ARName = $scope.entrySetting.ENTRY_SETTING_AR_NAME;
        $scope.userMenu.LatName = $scope.entrySetting.ENTRY_SETTING_EN_NAME;
        $scope.userMenu.MenuLink = "#/entry?entryType=" + $scope.entrySetting.ENTRY_SETTING_ID;
        $scope.userMenu.MenuID = $scope.menu.menuId;
        $scope.userMenu.CountryID = 1;
        $scope.userMenu.DisplayOrNot = 1;
        $scope.userMenu.UserShortcut = 0;
        $scope.userMenu.EntrySettingID = $scope.entrySetting.ENTRY_SETTING_ID;
        $scope.userMenu.Active = true;
        $scope.userMenu.UpdatedOn = new Date();
        $scope.userMenu.ID = $scope.userMenuID;
        MenuService.Update($scope.userMenu).then(function (result) {
            var x = result.data;
        });
    }
}]);