/// <reference path="../../Banks/views/ar/bank.html" />
/// <reference path="../../Banks/views/ar/bank.html" />
'use strict';
app.controller('PointOfSaleController', ['$scope', '$rootScope', '$location', '$routeParams', '$filter', '$log', '$q', 'authService', 'PointOfSaleService', '$timeout', '$document', '$uibModal', 'sharedService', 'billSettingService', 'goldWorldPriceService', 'ngDialog', 'payTypesService', 'accountsService', 'costService', 'billGridColumn', 'billPayTypesService', 'billCaliberTransactionsService', 'accountService', 'entryService', 'MenuService', 'commonService', 'localStorageService', 'itemCaliberService', 'customersService', function ($scope, $rootScope, $location, $routeParams, $filter, $log, $q, authService, PointOfSaleService, $timeout, $document, $uibModal, sharedService, billSettingService, goldWorldPriceService, ngDialog, payTypesService, accountsService, costService, billGridColumn, billPayTypesService, billCaliberTransactionsService, accountService, entryService, MenuService, commonService, localStorageService, itemCaliberService, customersService) {

    $scope.ReportData = {};
    $scope.matrixList = [];
    $scope.clearnessRate24 = 1;
    $scope.clearnessRate22 = 1;
    $scope.clearnessRate21 = 1;
    $scope.clearnessRate18 = 1;
    $scope.SearchText = {};
    $scope.bill_type_Id = 0;
    $scope.acc_wages_Id = 0;
    $scope.acc_wages_taxId = 0;
    $scope.wages_taxValue = 0;

    $scope.TempPurchasTax = 0;
    $scope.acc_purchas_Id = 0;
    $scope.acc_purchas_taxId = 0;
    $scope.purchas_taxValue = 0;

    $scope.acc_discountVal = 0;

    $scope.acc_sales_Id = 0;
    $scope.acc_vat_rateId = 0;

    $scope.wagesDiscountVal = 0;
    $scope.wagesTaxToVat = 0;

    $scope.TotalWagesValue = 0;
    $scope.TotalVatWagesValue = 0;
    $scope.BillTotalAfterTax = 0;

    $scope.convertGold = {};

    $scope.q24calc18 = 0;
    $scope.q24calc21 = 0;
    $scope.q24calc22 = 0;


    $scope.q22calc18 = 0;
    $scope.q22calc21 = 0;
    $scope.q22calc24 = 0;

    $scope.q21calc18 = 0;
    $scope.q21calc22 = 0;
    $scope.q21calc24 = 0;

    $scope.q18calc21 = 0;
    $scope.q18calc22 = 0;
    $scope.q18calc24 = 0;
    $scope.COM_BRN_IDForFilter = 0;

    $scope.gridData = {};
    $scope.entryMaster = {};
    $scope.goldBalance = {};
    $scope.custBalance = {};

    $scope.isEditReason = false;

    $scope.billSearch = {};
    $scope.isSelectedA = false;
    $scope.isDiscounted = false;

    $scope.isTotal24QuantityZero = false;
    $scope.isTotal22QuantityZero = false;
    $scope.isTotal21QuantityZero = false;
    $scope.isTotal18QuantityZero = false;
    $scope.HasGoldModule = false;

    $scope.totalName = "الاجمالي";

    $scope.userModel = [];
    $scope.checkUserModule = function () {
        if ($scope.bill_type_Id == 13 ||
            $scope.bill_type_Id == 14 ||
            $scope.bill_type_Id == 17 ||
            $scope.bill_type_Id == 18 ||
            $scope.bill_type_Id == 19 ||
            $scope.bill_type_Id == 20 ||
            $scope.bill_type_Id == 23 ||
            $scope.bill_type_Id == 24 ||
            $scope.bill_type_Id == 25 ||
            $scope.bill_type_Id == 26 ||
            $scope.bill_type_Id == 27 ||
            $scope.bill_type_Id == 28) {
            $scope.HasGoldModule = true;
        } else {
            $scope.HasGoldModule = false;
        }        
    }

    $scope.printDiv = function (printType) {

        
      
        $scope.isTotal24QuantityZero = false;
        $scope.isTotal22QuantityZero = false;
        $scope.isTotal21QuantityZero = false;
        $scope.isTotal18QuantityZero = false;

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
                frameDoc.document.write('<style>*{border: none} table td p{margin: 0 !important} #fatoraFooter * , #fatoraHeader *{border: none !important;text-align: center} table{margin: 10px 0; width: 100%} th{height:50px}table {border-collapse: collapse;}th, td {border: 1px solid black;padding: 5px 10px} p{margin: 10px 0 0 !important} .ng-hide { display: none !important; }</style>');
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
        }
        else if (printType == 'BillWithEntry') {
            $scope.PrintEntryDetailsWithBill = true;
            entryService.getEntryMasterByBill($scope.billMaster.BILL_ID).then(function (result) {
                $scope.entryMaster = result.data;
                entryService.getDetailsByEntryId($scope.entryMaster.ENTRY_ID).then(function (result) {
                    
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
                        }
                        else if ($scope.returnEntryDetails[i].ENTRY_GOLD24_DEBIT != undefined && $scope.returnEntryDetails[i].ENTRY_GOLD24_DEBIT != 0) {
                            isDepit = true;
                        }
                        else if ($scope.returnEntryDetails[i].ENTRY_GOLD22_DEBIT != undefined && $scope.returnEntryDetails[i].ENTRY_GOLD22_DEBIT != 0) {
                            isDepit = true;
                        }
                        else if ($scope.returnEntryDetails[i].ENTRY_GOLD21_DEBIT != undefined && $scope.returnEntryDetails[i].ENTRY_GOLD21_DEBIT != 0) {
                            isDepit = true;
                        }
                        else if ($scope.returnEntryDetails[i].ENTRY_GOLD18_DEBIT != undefined && $scope.returnEntryDetails[i].ENTRY_GOLD18_DEBIT != 0) {
                            isDepit = true;
                        }

                        //check credit
                        if ($scope.returnEntryDetails[i].ENTRY_CREDIT != undefined && $scope.returnEntryDetails[i].ENTRY_CREDIT != 0) {
                            isCredit = true;
                        }
                        else if ($scope.returnEntryDetails[i].ENTRY_GOLD24_CREDIT != undefined && $scope.returnEntryDetails[i].ENTRY_GOLD24_CREDIT != 0) {
                            isCredit = true;
                        }
                        else if ($scope.returnEntryDetails[i].ENTRY_GOLD22_CREDIT != undefined && $scope.returnEntryDetails[i].ENTRY_GOLD22_CREDIT != 0) {
                            isCredit = true;
                        }
                        else if ($scope.returnEntryDetails[i].ENTRY_GOLD21_CREDIT != undefined && $scope.returnEntryDetails[i].ENTRY_GOLD21_CREDIT != 0) {
                            isCredit = true;
                        }
                        else if ($scope.returnEntryDetails[i].ENTRY_GOLD18_CREDIT != undefined && $scope.returnEntryDetails[i].ENTRY_GOLD18_CREDIT != 0) {
                            isCredit = true;
                        }
                        if (isDepit) {
                            gridObj = $scope.returnEntryDetails[i];
                            $scope.DebitEntries.push(gridObj);
                        }
                        else if (isCredit) {
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
            });
        }
    }

    $scope.getPrintDivId = function () {
        //if (!$scope.HasGoldModule) {
        //    return "NormalBillPrint";
        //}
        //else {
            switch ($scope.bill_type_Id) {
                case 24:
                {
                    return "ExchangeGoldPurchases";
                }
                case 20:
                {
                    return "CashGoldPurchases";
                }
                case 23:
                {
                    return "ExchangeGold";
                }
                case 19:
                {
                    return "CashGold";
                }
                case 13:
                {
                    return "ExtchangeGoldBroke";
                }
                case 14:
                {
                    return "CashGoldBroke";
                }
                case 2:
                {
                    return "SellBillReport";
                }
                case 1:
                {
                    return "PurchBillReport";
                }
                case 26:
                {
                    return "SellBillReport";
                }
                case 25:
                {
                    return "PurchBillReport";
                }
                case 18:
                {
                    return "NoticeClosingPurchase";
                }
                case 17:
                {
                    return "NoticeClosingSale";
                }
                default:
                {
                    return "divTotals";
                }
            }
        //}
    }

    $scope.GetReportReportTotalsTafqeet = function () {
        //if ($scope.bill_type_Id == 13 || $scope.bill_type_Id == 14 || $scope.bill_type_Id == 2 || $scope.bill_type_Id == 1 || $scope.bill_type_Id == 23 || $scope.bill_type_Id == 19 || $scope.bill_type_Id == 24) {
        $scope.ReportData.Total24Tafqeet = $scope.billMaster.total24Quantity != 0 && $scope.billMaster.total24Quantity != undefined
            && $scope.billMaster.total24Quantity != "" ?
            ("فقط " + tafqeet(parseInt($scope.billMaster.total24Quantity)) + " جرام و" +
                tafqeet(parseFloat($scope.billMaster.total24Quantity.split(".")[1])) + " % من الجرام عيار 24") : "فقط صفر جرام لا غير من عيار 24";

        $scope.ReportData.Total22Tafqeet = $scope.billMaster.total22Quantity != 0 && $scope.billMaster.total22Quantity != undefined
            && $scope.billMaster.total22Quantity != "" ?
            ("فقط " + tafqeet(parseInt($scope.billMaster.total22Quantity)) + " جرام و" +
                tafqeet(parseFloat($scope.billMaster.total22Quantity.split(".")[1])) + " % من الجرام عيار 22") : "فقط صفر جرام لا غير من عيار 22";

        $scope.ReportData.Total21Tafqeet = $scope.billMaster.total21Quantity != 0 && $scope.billMaster.total21Quantity != undefined
            && $scope.billMaster.total21Quantity != "" ?
            ("فقط " + tafqeet(parseInt($scope.billMaster.total21Quantity)) + " جرام و" +
                tafqeet(parseFloat($scope.billMaster.total21Quantity.split(".")[1])) + " % من الجرام عيار 21") : "فقط صفر جرام لا غير من عيار 21";

        $scope.ReportData.Total18Tafqeet = $scope.billMaster.total18Quantity != 0 && $scope.billMaster.total18Quantity != undefined
            && $scope.billMaster.total18Quantity != "" ?
            ("فقط " + tafqeet(parseInt($scope.billMaster.total18Quantity)) + " جرام و" +
                tafqeet(parseFloat($scope.billMaster.total18Quantity.split(".")[1])) + " % من الجرام عيار 18") : "فقط صفر جرام لا غير من عيار 18";

        //}
        //if ($scope.bill_type_Id == 2 || $scope.bill_type_Id == 1 || $scope.bill_type_Id == 23 || $scope.bill_type_Id == 19 || $scope.bill_type_Id == 24) {
        $scope.ReportData.totalMustPaidTafqeet = $scope.billMaster.totalMustPaid != 0 && $scope.billMaster.totalMustPaid != undefined
            && $scope.billMaster.totalMustPaid != "" ?
            ("فقط " + tafqeet(parseInt($scope.billMaster.totalMustPaid)) + " ريال و" +
                tafqeet(parseFloat($scope.billMaster.totalMustPaid.split(".")[1])) + " هلله فقط لا غير") : "فقط صفر ريال لا غير";
        //}


        $scope.ReportData.TotalAfterTaxTafqeet = $scope.billMaster.TotalAfterTax != 0 && $scope.billMaster.TotalAfterTax != undefined
            && $scope.billMaster.TotalAfterTax != "" ?
            ("فقط " + tafqeet(parseInt($scope.billMaster.TotalAfterTax)) + " ريال و" +
                tafqeet(parseFloat($scope.billMaster.TotalAfterTax.split(".")[1])) + " هلله فقط لا غير") : "فقط صفر ريال لا غير";
    }

    $scope.goldLastPrice = function () {
        goldWorldPriceService.getLastGoldWorldPriceData().then(function (response) {
            var result = response.data.GoldPrice;
            $scope.GoldWorldPrice = parseFloat(response.data.KiloPrice).toFixed(2);

        })
    }

    $scope.calcTotalBillReport = function (x, y) {
        var result = (parseFloat(x) + parseFloat(y)).toFixed(2);
        if (!isNaN(result) && result != undefined && result != null) {
            //alert("1");
            return result;
        }
        else {
            return " ";
        }

    }

    $scope.GetReportData = function () {
        //************** Start Report Shared Data ****************//
        $scope.goldLastPrice();


        // Currency

        $.each($scope.currenciesList, function (index, value) {
            if (value.CURRENCY_ID == $scope.billMaster.CURRENCY_ID) {
                $scope.ReportData.CURRENCY_Name = value.CURRENCY_AR_NAME;
                $scope.ReportData.CURRENCY_SUB_AR_NAME = value.CURRENCY_SUB_AR_NAME;
                // alert($scope.ReportData.CURRENCY_SUB_AR_NAME);
            }
        })

        //Report date
        var DateOptions = { weekday: 'long', year: 'numeric', month: 'long', day: 'numeric' };
        $scope.ReportData.ReportEnDate = new Date().toLocaleString("ar-eg", DateOptions);
        $scope.ReportData.ReportHigriDate = new Date().toLocaleString("ar-eg-u-ca-islamic", DateOptions);

        // Get customer data
        var customerAccount = jQuery.grep($scope.CustomersAccounts, function (n, i) {
            return (n.ACC_ID === $scope.billMaster.CUST_ACC_ID);
        });

        if (customerAccount.length > 0 && customerAccount[0] != undefined) {

            $scope.ReportData.custAccountName = customerAccount[0].ACC_AR_NAME;
            $scope.ReportData.custAccountNumber = customerAccount[0].ACC_CODE;
            $scope.ReportData.TaxNumber = customerAccount[0].TaxNumber;

            accountsService.getByID($scope.billMaster.CUST_ACC_ID).then(function (Result) {

                if (Result.data.ACC_TYPE_ID == 8 || Result.data.ACC_TYPE_ID == 9 || Result.data.ACC_TYPE_ID == 10) {
                    if ($scope.billSetting.BILL_SETTING_ID == 72) {
                        $scope.ReportData.custAccountName = $scope.billMaster.DeliveryPersonName;
                    }

                    else if ($scope.billSetting.BILL_SETTING_ID == 71) {
                        $scope.ReportData.custAccountName = "نقدى";
                    }

                } else {
                    if ($scope.billSetting.BILL_SETTING_ID == 71) {
                        //  $scope.ReportData.custAccountName = "آجل";
                    }
                }
            })
        }

        // get customer address
        customersService.getById($scope.billMaster.CUST_ACC_ID).then(function (Result) {
            if (Result.data != undefined && Result.data != null && Result.data != "") {
                $scope.ReportData.customerAddressRemark = Result.data.CUST_ADDR_REMARKS != null ? Result.data.CUST_ADDR_REMARKS : '';
                $scope.ReportData.customerTaxNumber = Result.data.TaxNumber != null ? Result.data.TaxNumber : '';

                //alert($scope.billMaster.DeliveryPersonName);

                ////ACC_TYPE_ID[0]

                //accountsService.getByID($scope.billMaster.CUST_ACC_ID).then(function (Result) {
                //    alert(Result.ACC_TYPE_ID);
                //})

            }
        })


        // Get Branch data
        if (localStorageService.get('tempBrEnName') != null && localStorageService.get('tempBrEnName') != undefined) {
            $scope.ReportData.ReportBranchEnName = localStorageService.get('tempBrEnName').tempBrEnName;
        }
        else {
            $scope.ReportData.ReportBranchEnName = authService.GetUserName();
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
                //$scope.getUserType();
            }
        },
            function (err) {
                $scope.message = err.error_description;
                console.log(err.message);
            });

        if ($scope.bill_type_Id == 18 || $scope.bill_type_Id == 17) {
            goldWorldPriceService.getLastGoldWorldPriceData().then(function (response) {
                var result = response.data.GoldPrice;
                $scope.KiloPrice = parseFloat(response.data.KiloPrice).toFixed(2);

            });
        }
        //************** End Report Shared Data ****************//
    }

    //Get Printing Time for the bill
    $scope.getCurrentTime = function () {
        var d = new Date(); // for now
        var H = d.getHours(); // => 9
        var m = d.getMinutes(); // =>  30
        var s = d.getSeconds(); // => 51
        return "" + H + ":" + m + ":" + s + "";
    }



    $scope.sticky = function () {
        var y = $('.actionBar').offset().top;
        $(window).on('scroll', function () {
            if ($(this).scrollTop() > y) {
                $('.actionBar').addClass('sticky-btns-bar');
            } else {
                $('.actionBar').removeClass('sticky-btns-bar');
            }
        });
    };
    //Get Printing Employee Name 
    $scope.getCurrentEmployeeName = function () {
        if ($scope.billMaster.EMP_ID != null && $scope.billMaster.EMP_ID > 0) {
            for (var i = 0; i < $scope.EmployeesSales.length; i++) {
                if ($scope.EmployeesSales[i].EMP_ID == $scope.billMaster.EMP_ID) {
                    return $scope.EmployeesSales[i].EMP_AR_NAME;
                }
            }
        }

    }

    //Get Printing Payment type 
    $scope.getCurrentBillPayWay = function () {
        if ($scope.billMaster.BILL_PAY_WAY != null && $scope.billMaster.BILL_PAY_WAY > 0) {
            for (var i = 0; i < $scope.tpayTypes.length; i++) {
                if ($scope.tpayTypes[i].PAY_TYPE_ID == $scope.billMaster.BILL_PAY_WAY) {
                    return $scope.tpayTypes[i].PAY_TYPE_AR_NAME;
                }
            }
        }

    }

    $scope.hidePrintOnNewBill = function () {
        if ($scope.operation == "Insert") {
            return true;
        } else {
            return false;
        }

    }


    $scope.itemsGroup = { GroupID: null, GroupCode: "", GroupARName: "", GroupENName: "", GroupMasterID: null, GroupRemarks: "", AppearOnSalePoint: false };
    $scope.item = {
        ITEM_ID: null, ITEM_CODE: "", GROUP_ID: null, ITEM_AR_NAME: "", ITEM_EN_NAME: "", ITEM_REMARKS: "", PRODUCTION_DATE: false, EXPIRED_DATE: false,
        SERIAL_IN_INPUT: false, SERIAL_IN_OUTPUT: false, ITEM_PIC: null, MIN_QTY: null, ITEM_TYPE: null, ITEM_COLOR: null, ITEM_MODEL: "", ITEM_BRAND: "",
        COMPANY_ID: null, SELLEDISCOUNT: null, BURCHASEDISCOUNT: null, DOESTHEQUANTITYISAPARTOFBARCODE: false, QUANTITYLENGTHATTHEBARCODE: null, COST_CALCULATION_TYPE: null, SubjectToVAT: null, VATRate: null, CaliberID: null, ARName: null, LatName: null, ClearnessRate: null, ItemWeight: null,
        AddedBy: "", AddedOn: null, UpdatedBy: "", updatedOn: null
    };

    $scope.billMaster = {};
    $scope.billMasters = [];

    $scope.billSetting = null;


    // $scope.searchItems = [{ ITEM_CODE: 121, ITEM_AR_NAME: "Apple", ITEM_EN_NAME: "Apple", GROUP_ID: 1, ITEM_REMARKS: "لا يوجد", COMPANY_ID: 1 }, { ITEM_CODE: 121, ITEM_AR_NAME: "سامسونج", ITEM_EN_NAME: "Samsung", GROUP_ID: 1, ITEM_REMARKS: "لا يوجد", COMPANY_ID: 1 }]

    $scope.operation = "Insert";
    $scope.GoldWorldPrice = 0;


    /////////////////get last code
    $scope.getBillNumber = function () {



        var urlParams = $location.search();
        if (urlParams.foo != null && urlParams.foo != undefined && urlParams.foo != "") { } else {
            $scope.billType = ($location.search()).billType;
            var branchId = 0;
            if (localStorageService.get('branchId') != null) {
                branchId = localStorageService.get('branchId').branchId;
            }
            PointOfSaleService.GetLastBillNumber(branchId, $scope.billType).then(function (response) {
                var result = response.data;
                $scope.billMaster.BILL_NUMBER = result + 1;
            })
        }

    }



    $scope.getLastGoldWorldPrice = function () {

        goldWorldPriceService.GetLastGoldWorldPrice().then(function (response) {

            var result = response.data;
            $scope.GoldWorldPrice = result;
            //  alert(result);
        })
    }

    $scope.gridBillDetailsItem = {
        ITEM_CODE: "", ITEM_AR_NAME: "", DISC_VALUE: 0, DISC_RATE: 0, Quantity: 1, Unit: null, SubjectToVAT: null, VATRate: null, CaliberID: null, ARName: null, LatName: null, ClearnessRate: null, Price: 0, Total: 0, GridRowNumber: 0, UnitNameAr: "", CaliberNameAr: "", Caliber18: null, Caliber21: null, Caliber22: null, Caliber24: null, ItemStatus: 0,LockPrice:0
    };


    $scope.selectedItems = [];

    $scope.items = [];




    $scope.cleargridBillDetailsItem = function () {
        $scope.selectedItems = [];
        $scope.addGridBillDetailsItem();
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

            if (billType == 1 || /*billType == 27*/billType == 25) {
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

    //GetAllCustomersAccounts
    $scope.GetAllCustomersAccounts = function (billSettingObj) {

        var billType = billSettingObj.BILL_TYPE_ID;
        $scope.CustomersAccounts = [];
        accountsService.getAccountBoxByTypesForBill(billType, "cust", $scope.COM_BRN_IDForFilter).then(function (result) {
            $scope.CustomersAccounts = result.data;

        });
    }


    //GetAllBoxAccounts
    $scope.GetAllBoxAccounts = function (billSettingObj) {
        var billType = billSettingObj.BILL_TYPE_ID;
        $scope.BoxAccounts = [];
        accountsService.getAccountBoxByTypesForBill(billType, "cash", $scope.COM_BRN_IDForFilter).then(function (result) {
            $scope.BoxAccounts = result.data;
        });
    }


    //GetAllGoldBoxAccounts
    $scope.GetAllGoldBoxAccounts = function (billSettingObj) {
        var billType = billSettingObj.BILL_TYPE_ID;

        $scope.GoldBoxAccounts = [];

        if (localStorageService.get('branchId') != null) {
            $scope.billMaster.COM_BRN_ID = localStorageService.get('branchId').branchId;
            $scope.COM_BRN_IDForFilter = localStorageService.get('branchId').branchId;
        }

        accountsService.GetGoldBoxByTypesForBill("gold", $scope.COM_BRN_IDForFilter).then(function (result) {
            $scope.GoldBoxAccounts = result.data;

        });

    }


    //GetAllEmployeesSales
    $scope.GetAllEmployeesSales = function () {


        $scope.EmployeesSales = [];
        PointOfSaleService.getAllEmployeeOfTypeSales().then(function (response) {

            $scope.EmployeesSales = response.data;
        })
    }
    //Calculate the totals
    $scope.calculateTotals = function () {
        $scope.billMaster.totalQuantity = 0;
        $scope.billMaster.totalMustPaid = 0;

        $scope.billMaster.total24Quantity = 0;
        $scope.billMaster.total22Quantity = 0;
        $scope.billMaster.total21Quantity = 0;
        $scope.billMaster.total18Quantity = 0;

        //$scope.billMaster.BILL_TOTAL_EXTRA = 0;
        $scope.billMaster.totalPrice = 0;
        $scope.billMaster.TotalBeforeTax = 0;
        $scope.billMaster.TotalAfterTax = 0;
        $scope.billMaster.TotalVatRate = 0;

        $scope.billMaster.totalItemWages = 0;
        $scope.billMaster.totalManufactWages = 0;

        // $scope.billMaster.TotalBeforeTax = 0;

        $scope.footerGoldAfterDisc = 0;
        $scope.footerWagesAfterDisc = 0;



        $scope.footerGoldVat = 0;
        $scope.footerWagesVat = 0;


        $scope.billMaster.TotalDisc = 0;
        $scope.billMaster.TotalDiscPercentage = 0;

        $scope.billMaster.TotalAllDisc = 0;
        $scope.billMaster.TotalAllDiscPercentage = 0;
        $scope.billMaster.TotalVatValue = 0;
        $scope.billMaster.TotalTaxableAmount = 0;
        $scope.billMaster.TotalNotTaxableAmount = 0;

        $scope.billMaster.TotalBeforeDiscount = 0;
        $scope.billMaster.TotalAfterDiscount = 0;


        $scope.billMaster.totalOfTotalPrice = 0;

        $scope.billMaster.TotalWagesDiscValue = 0;
        $scope.billMaster.TotalWagesDiscRate = 0;

        $scope.billMaster.TotalPaid = 0;

        $scope.billMaster.TotalNotVatRate = 0;
        $scope.billMaster.TotalNotVatValue = 0;

        $scope.billMaster.totalTaxableManufactWages = 0;
        $scope.billMaster.totalNotTaxableManufactWages = 0;

        $scope.billMaster.TotalGoldManfact = 0;

        $scope.billMaster.TotalExemptOfTax = 0;
        $scope.billMaster.ExemptOfTaxValue = 0;

        $scope.billMaster.TotalMainVat = 0;
        $scope.billMaster.MainVatValue = 0;
        $scope.billMaster.manufacturingWagesMainVat = 0;

        $scope.billMaster.TotalZeroVat = 0;
        $scope.billMaster.ZeroVatValue = 0;

        // $scope.footerTotalVatRate = 0;
        $scope.footerTotalVatValue = 0;
        $scope.footerTaxTotal = 0;


        $scope.footerTotalGmPrice = 0;
        $scope.footerTotalPrice = 0;
        $scope.footerTotalClearness = 0;

        $scope.footerAfterDiscount = 0;

        $scope.footerTotalItemGmWages = 0;
        $scope.footerTotalNotTaxableAmount = 0;
        $scope.footerTotalTaxableAmount = 0;
        $scope.footerTotalGoldMan = 0;

        $scope.vat = 0;
        $scope.sumAllVat = 0;

        $scope.ReportData.reportTotalBeforeTax = 0;
        $scope.ReportData.reportTotalAfterTax = 0;


        $scope.WagesExemptTotal = 0;
        $scope.WagesExemptValue = 0;

        $scope.GoldExemptTotal = 0;
        $scope.GoldExemptValue = 0;



        $scope.WagesMainTotal = 0;
        $scope.WagesMainValue = 0;

        $scope.GoldMainTotal = 0;
        $scope.GoldMainValue = 0;


        $scope.WagesZeroTotal = 0;
        $scope.WagesZeroValue = 0;

        $scope.GoldZeroTotal = 0;
        $scope.GoldZeroValue = 0;


        $scope.WagesNotTaxable = 0;
        $scope.GoldNotTaxable = 0;

        $scope.footerGoldAfterDisc = 0;
        $scope.footerWagesAfterDisc = 0;



        $scope.footerGoldVat = 0;
        $scope.footerWagesVat = 0;


        //$scope.footerTotalActualWeight = 0;


        for (var i = 0; i < $scope.selectedItems.length; i++) {
           
            if ($scope.selectedItems[i].ITEM_CODE != null && $scope.selectedItems[i].ITEM_CODE != "" && $scope.selectedItems[i].ITEM_CODE != undefined) {
                if ($scope.selectedItems[i].DISC_VALUE == null || $scope.selectedItems[i].DISC_VALUE == undefined) {
                    $scope.selectedItems[i].DISC_VALUE = 0;
                }
                if ($scope.selectedItems[i].DISC_RATE == null) {
                    $scope.selectedItems[i].DISC_RATE = 0;
                }

              

                if ($scope.selectedItems[i].VATRate != undefined) {
                    $scope.selectedItems[i].VatValue = ((parseFloat($scope.selectedItems[i].VATRate) / 100) * (parseFloat(parseFloat($scope.selectedItems[i].Total) - parseFloat($scope.selectedItems[i].DISC_VALUE )))).toFixed(2);

                    //if ($scope.selectedItems[i].SubjectToVAT == true) {
                    //    $scope.selectedItems[i].TaxTotal = (parseFloat($scope.selectedItems[i].Total) + parseFloat($scope.selectedItems[i].VatValue)).toFixed(2);
                    //}
                    //else {
                    //    $scope.selectedItems[i].TaxTotal = (parseFloat($scope.selectedItems[i].Total)).toFixed(2);
                    //}
                }
                //else {
                //    $scope.selectedItems[i].TaxTotal = (parseFloat($scope.selectedItems[i].Total)).toFixed(2);
                //}


                $scope.billMaster.totalQuantity = Number((parseFloat($scope.selectedItems[i].Quantity) + parseFloat($scope.billMaster.totalQuantity)).toFixed(2));



                if ($scope.selectedItems[i].CaliberID == 1) {
                    $scope.billMaster.total24Quantity = (parseFloat($scope.selectedItems[i].ItemWeight) + parseFloat($scope.billMaster.total24Quantity)).toFixed(2);
                }
                if ($scope.selectedItems[i].CaliberID == 2) {
                    $scope.billMaster.total22Quantity = (parseFloat($scope.selectedItems[i].ItemWeight) + parseFloat($scope.billMaster.total22Quantity)).toFixed(2);
                }
                if ($scope.selectedItems[i].CaliberID == 3) {
                    $scope.billMaster.total21Quantity = (parseFloat($scope.selectedItems[i].ItemWeight) + parseFloat($scope.billMaster.total21Quantity)).toFixed(2);
                }
                if ($scope.selectedItems[i].CaliberID == 4) {
                    $scope.billMaster.total18Quantity = (parseFloat($scope.selectedItems[i].ItemWeight) + parseFloat($scope.billMaster.total18Quantity)).toFixed(2);
                }



                $scope.billMaster.totalOfTotalPrice = (parseFloat($scope.selectedItems[i].Total) + parseFloat($scope.billMaster.totalOfTotalPrice)).toFixed(2);

                $scope.billMaster.totalPrice = Number((parseFloat($scope.selectedItems[i].Total) - parseFloat($scope.selectedItems[i].DISC_VALUE) + parseFloat($scope.billMaster.totalPrice)).toFixed(2));


                //check if item expempt of tax or not

                //المعفي من الضريبه
                if ($scope.selectedItems[i].IsExemptOfTax == true) {
                    if ($scope.bill_type_Id == 19 || $scope.bill_type_Id == 23) {
                        $scope.billMaster.TotalExemptOfTax = (parseFloat($scope.selectedItems[i].ManufacturingWages) + parseFloat($scope.billMaster.TotalExemptOfTax)).toFixed(2);
                    }
                    else if ($scope.bill_type_Id == 24 || $scope.bill_type_Id == 20) {
                        $scope.billMaster.TotalExemptOfTax = (parseFloat($scope.selectedItems[i].TotalGoldManfact) + parseFloat($scope.billMaster.TotalExemptOfTax)).toFixed(2);



                    }
                    else {
                        $scope.billMaster.TotalExemptOfTax = (parseFloat($scope.selectedItems[i].Total - parseFloat($scope.selectedItems[i].DISC_VALUE ) + parseFloat($scope.billMaster.TotalExemptOfTax))).toFixed(2);
                    }
                    if ($scope.billMaster.ExemptOfTaxRate != undefined && $scope.billMaster.ExemptOfTaxRate != null) {
                        $scope.billMaster.ExemptOfTaxValue = ((parseFloat($scope.billMaster.ExemptOfTaxRate) * parseFloat($scope.billMaster.TotalExemptOfTax) / 100)).toFixed(2);


                        $scope.WagesExemptTotal = (parseFloat($scope.selectedItems[i].ManufacturingWages) + parseFloat($scope.WagesExemptTotal)).toFixed(2);
                        $scope.GoldExemptTotal = ((parseFloat($scope.selectedItems[i].Total) - parseFloat($scope.selectedItems[i].DISC_VALUE )) + parseFloat($scope.GoldExemptTotal)).toFixed(2);


                        $scope.WagesExemptValue = (parseFloat($scope.billMaster.ExemptOfTaxRate) * parseFloat($scope.WagesExemptTotal) / 100).toFixed(2);
                        $scope.GoldExemptValue = (parseFloat($scope.billMaster.ExemptOfTaxRate) * parseFloat($scope.GoldExemptTotal) / 100).toFixed(2)

                    }

                }
                else {
                     
                    if ($scope.selectedItems[i].SubjectToVAT == true) {

                        $scope.billMaster.TotalVatRate = (parseFloat($scope.selectedItems[i].VATRate) + parseFloat($scope.billMaster.TotalVatRate)).toFixed(2);

                        // اجمالي اجور التصنيع الخاضعه للضريبه
                        $scope.billMaster.totalTaxableManufactWages = parseFloat($scope.selectedItems[i].ManufacturingWages - parseFloat($scope.selectedItems[i].WagesDiscValue != undefined ? $scope.selectedItems[i].WagesDiscValue : 0) + parseFloat($scope.billMaster.totalTaxableManufactWages)).toFixed(2);

                        if ($scope.IsQuantityCalculated == true) {
                            $scope.Quantity = $scope.selectedItems[i].Quantity;
                        }
                        else {
                            $scope.Quantity = 1;
                        }


                        //check the main vat and zero vat
                        if ($scope.billMaster.MainVatRate != undefined && $scope.billMaster.MainVatRate != null) {
                            
                            if ($scope.selectedItems[i].VATRate == $scope.billMaster.MainVatRate) {
                                if ($scope.bill_type_Id == 19 || $scope.bill_type_Id == 23) {
                                    $scope.billMaster.manufacturingWagesMainVat = (parseFloat($scope.selectedItems[i].ManufacturingWages) - parseFloat($scope.selectedItems[i].WagesDiscValue) + parseFloat($scope.billMaster.manufacturingWagesMainVat)).toFixed(2);
                                    $scope.billMaster.TotalMainVat = $scope.billMaster.manufacturingWagesMainVat;


                                    //calc after discount
                                    //$scope.selectedItems[i].AfterDiscount = (parseFloat($scope.selectedItems[i].ManufacturingWages) - parseFloat($scope.selectedItems[i].WagesDiscValue)).toFixed(2);
                                }
                                else if ($scope.bill_type_Id == 24 || $scope.bill_type_Id == 20) {
                                    $scope.billMaster.manufacturingWagesMainVat = (parseFloat($scope.selectedItems[i].ManufacturingWages) - parseFloat($scope.selectedItems[i].WagesDiscValue) + parseFloat($scope.billMaster.manufacturingWagesMainVat)).toFixed(2);
                                    $scope.billMaster.TotalMainVat = (parseFloat($scope.billMaster.TotalMainVat) + parseFloat($scope.selectedItems[i].TotalGoldManfact)).toFixed(2);

                                    //calc after discount
                                    //$scope.selectedItems[i].AfterDiscount = (parseFloat($scope.selectedItems[i].ManufacturingWages) - parseFloat($scope.selectedItems[i].WagesDiscValue)).toFixed(2);

                                }
                                else {
                                     
                                    $scope.billMaster.TotalMainVat = ((parseFloat($scope.selectedItems[i].Total) - parseFloat($scope.selectedItems[i].DISC_VALUE )) + parseFloat($scope.billMaster.TotalMainVat)).toFixed(2);

                                    //calc after discount
                                    //$scope.selectedItems[i].AfterDiscount = (parseFloat($scope.selectedItems[i].Total) - parseFloat($scope.selectedItems[i].DISC_VALUE)).toFixed(2);

                                }
                                
                                $scope.billMaster.MainVatValue = ((parseFloat($scope.billMaster.MainVatRate) * (parseFloat($scope.billMaster.TotalMainVat))) / 100).toFixed(2);


                                $scope.WagesMainTotal = (parseFloat($scope.selectedItems[i].ManufacturingWages) + parseFloat($scope.WagesMainTotal)).toFixed(2);
                                $scope.GoldMainTotal = ((parseFloat($scope.selectedItems[i].Total) - parseFloat($scope.selectedItems[i].DISC_VALUE )) + parseFloat($scope.GoldMainTotal)).toFixed(2);


                                $scope.WagesMainValue = (parseFloat($scope.billMaster.MainVatRate) * parseFloat($scope.WagesMainTotal) / 100).toFixed(2);
                                $scope.GoldMainValue = (parseFloat($scope.billMaster.MainVatRate) * parseFloat($scope.GoldMainTotal) / 100).toFixed(2)




                                $scope.selectedItems[i].GoldVat = (parseFloat($scope.billMaster.MainVatRate) * (parseFloat($scope.selectedItems[i].Total) - parseFloat($scope.selectedItems[i].DISC_VALUE))).toFixed(2);

                                if ($scope.selectedItems[i].GoldVat != null && $scope.selectedItems[i].GoldVat != undefined && $scope.selectedItems[i].GoldVat != '') {

                                }
                                else {
                                    $scope.selectedItems[i].GoldVat = 0;
                                }


                                $scope.selectedItems[i].WagesVat = (parseFloat($scope.billMaster.MainVatRate) * (parseFloat($scope.selectedItems[i].ManufacturingWages) - parseFloat($scope.selectedItems[i].WagesDiscValue))).toFixed(2);
                                
                              

                                if ($scope.selectedItems[i].WagesVat != null && $scope.selectedItems[i].WagesVat != undefined && $scope.selectedItems[i].WagesVat != '')
                                {

                                }
                                else {
                                    $scope.selectedItems[i].WagesVat = 0;
                                }

                                $scope.selectedItems[i].GoldVat = (parseFloat($scope.billMaster.MainVatRate) * (parseFloat($scope.selectedItems[i].Total) - parseFloat($scope.selectedItems[i].DISC_VALUE))).toFixed(2);
                                $scope.selectedItems[i].WagesVat = (parseFloat($scope.billMaster.MainVatRate) * (parseFloat($scope.selectedItems[i].ManufacturingWages) - parseFloat($scope.selectedItems[i].WagesDiscValue | 0))).toFixed(2);

                            }



                        }



                        if ($scope.selectedItems[i].VATRate == undefined || $scope.selectedItems[i].VATRate == 0) {
                            if ($scope.bill_type_Id == 19 || $scope.bill_type_Id == 23) {
                                $scope.billMaster.TotalZeroVat = parseFloat($scope.selectedItems[i].ManufacturingWages) + parseFloat($scope.billMaster.TotalZeroVat);
                            }
                            else if ($scope.bill_type_Id == 24 || $scope.bill_type_Id == 20) {
                                $scope.billMaster.TotalZeroVat = (parseFloat($scope.selectedItems[i].TotalGoldManfact) + parseFloat($scope.billMaster.TotalZeroVat)).toFixed(2);
                            }
                            else {
                                $scope.billMaster.TotalZeroVat = (parseFloat($scope.selectedItems[i].NetTotal) + parseFloat($scope.billMaster.TotalZeroVat)).toFixed(2);
                            }
                            $scope.billMaster.ZeroVatValue = 0;
                            $scope.billMaster.ZeroVatRate = 0;


                            $scope.selectedItems[i].GoldVat = 0;
                            $scope.selectedItems[i].WagesVat = 0;





                            $scope.WagesZeroTotal = (parseFloat($scope.selectedItems[i].ManufacturingWages) + parseFloat($scope.WagesZeroTotal)).toFixed(2);
                            $scope.GoldZeroTotal = ((parseFloat($scope.selectedItems[i].NetTotal)) + parseFloat($scope.GoldZeroTotal)).toFixed(2);


                            //$scope.WagesZeroValue = (parseFloat($scope.billMaster.MainVatRate) * parseFloat($scope.WagesZeroTotal) / 100).toFixed(2);
                            //$scope.GoldZeroValue = (parseFloat($scope.billMaster.MainVatRate) * parseFloat($scope.GoldZeroTotal) / 100).toFixed(2)



                        }


                        //if ($scope.bill_type_Id == 19) {
                        //    $scope.billMaster.TotalVatValue = ((parseFloat($scope.wages_taxValue) * parseFloat(footerTotalItemGmWages)) + parseFloat($scope.billMaster.TotalVatValue)).toFixed(2);
                        //}
                        //else {
                        
                        if ($scope.hasGoldModule) {
                            $scope.billMaster.TotalVatValue = ((parseFloat($scope.selectedItems[i].VatValue) * parseFloat($scope.Quantity)) + parseFloat($scope.billMaster.TotalVatValue)).toFixed(2);
                        } else {
                            $scope.billMaster.TotalVatValue = (parseFloat($scope.selectedItems[i].VatValue) + parseFloat($scope.billMaster.TotalVatValue)).toFixed(2);
                        }
                        // }

                        // if type قبض  , lرجهمشغولات حساب الضريبه علي اجره الجرام
                        if ($scope.bill_type_Id == 19 || $scope.bill_type_Id == 23) {
                            $scope.billMaster.TotalTaxableAmount = Number(((parseFloat($scope.selectedItems[i].itemGmWages) * parseFloat($scope.selectedItems[i].ActualItemWeight)) - parseFloat($scope.selectedItems[i].DISC_VALUE) + parseFloat($scope.billMaster.TotalTaxableAmount)).toFixed(2));
                        }

                        else if ($scope.bill_type_Id == 20 || $scope.bill_type_Id == 24) {
                            $scope.billMaster.TotalTaxableAmount = Number(((parseFloat($scope.selectedItems[i].itemGmWages) * parseFloat($scope.selectedItems[i].ActualItemWeight)) - parseFloat($scope.selectedItems[i].DISC_VALUE) + (parseFloat($scope.selectedItems[i].Total) + parseFloat($scope.billMaster.TotalTaxableAmount)) + parseFloat($scope.billMaster.TotalTaxableAmount)).toFixed(2));
                        }
                        else {
                            $scope.billMaster.TotalTaxableAmount = Number((parseFloat($scope.selectedItems[i].Total) - parseFloat($scope.selectedItems[i].DISC_VALUE) + parseFloat($scope.billMaster.TotalTaxableAmount)).toFixed(2));
                        }




                        //////

                        if ($scope.selectedItems[i].WagesDiscValue) {
                            $scope.wagesTaxToVat = (((parseFloat($scope.selectedItems[i].ManufacturingWages) - parseFloat($scope.selectedItems[i].WagesDiscValue)) * parseFloat($scope.Quantity))).toFixed(2);
                        }
                        else {
                            $scope.wagesTaxToVat = ((parseFloat($scope.selectedItems[i].ManufacturingWages) * parseFloat($scope.Quantity))).toFixed(2);
                        }

                        ///////footer totals
                        $scope.footerTotalVatValue = (parseFloat($scope.selectedItems[i].VatValue) + parseFloat($scope.footerTotalVatValue)).toFixed(2);
                        //  $scope.footerTotalVatRate = (parseFloat($scope.selectedItems[i].VATRate) + parseFloat($scope.footerTotalVatRate)).toFixed(2);
                        // $scope.footerTaxTotal = (parseFloat($scope.selectedItems[i].TaxTotal) + parseFloat($scope.footerTaxTotal)).toFixed(2);


                    }
                    else {

                        if ($scope.BillVatRate != undefined && $scope.BillVatRate != null && $scope.BillVatRate != 0) {
                            // if type قبض مشغولات حساب الضريبه علي اجره الجرام
                            if ($scope.bill_type_Id == 19 || $scope.bill_type_Id == 23) {
                                $scope.billMaster.TotalTaxableAmount = Number(((parseFloat($scope.selectedItems[i].itemGmWages) * parseFloat($scope.selectedItems[i].ActualItemWeight)) + parseFloat($scope.billMaster.TotalTaxableAmount)).toFixed(2));

                            }
                            else if ($scope.bill_type_Id == 20 || $scope.bill_type_Id == 24) {
                                $scope.billMaster.TotalTaxableAmount = Number(((parseFloat($scope.selectedItems[i].itemGmWages) * parseFloat($scope.selectedItems[i].ActualItemWeight)) - parseFloat($scope.selectedItems[i].DISC_VALUE) + ((parseFloat($scope.selectedItems[i].Total) + parseFloat($scope.billMaster.TotalTaxableAmount))) + parseFloat($scope.billMaster.TotalTaxableAmount)).toFixed(2));
                            }
                            else {
                                $scope.billMaster.TotalTaxableAmount = Number((parseFloat($scope.selectedItems[i].Total) - parseFloat($scope.selectedItems[i].DISC_VALUE) + parseFloat($scope.billMaster.TotalTaxableAmount)).toFixed(2));
                            }
                        }
                        else {
                            $scope.billMaster.TotalNotVatRate = (parseFloat($scope.selectedItems[i].VATRate) + parseFloat($scope.billMaster.TotalNotVatRate)).toFixed(2);
                            $scope.billMaster.TotalNotVatValue = (parseFloat($scope.selectedItems[i].VatValue) + parseFloat($scope.billMaster.TotalNotVatValue)).toFixed(2);

                            //اجمالي اجور التصنيع الغير خاضعه للضريبه
                            $scope.billMaster.totalNotTaxableManufactWages = parseFloat($scope.selectedItems[i].ManufacturingWages - parseFloat($scope.selectedItems[i].WagesDiscValue != undefined ? $scope.selectedItems[i].WagesDiscValue : 0) + parseFloat($scope.billMaster.totalNotTaxableManufactWages)).toFixed(2);

                            // if type قبض مشغولات حساب الضريبه علي اجره الجرام
                            if ($scope.bill_type_Id == 19 || $scope.bill_type_Id == 23) {
                                $scope.billMaster.TotalNotTaxableAmount = Number(((parseFloat($scope.selectedItems[i].itemGmWages) * parseFloat($scope.selectedItems[i].ActualItemWeight)) - parseFloat($scope.selectedItems[i].DISC_VALUE) + parseFloat($scope.billMaster.TotalNotTaxableAmount)).toFixed(2));
                            }
                            else if ($scope.bill_type_Id == 20 || $scope.bill_type_Id == 24) {
                                $scope.billMaster.TotalNotTaxableAmount = Number(((parseFloat($scope.selectedItems[i].itemGmWages) * parseFloat($scope.selectedItems[i].ActualItemWeight)) - parseFloat($scope.selectedItems[i].DISC_VALUE) + parseFloat($scope.selectedItems[i].Total) + parseFloat($scope.billMaster.TotalNotTaxableAmount)).toFixed(2));
                            }
                            else {
                                
                                $scope.billMaster.TotalNotTaxableAmount = Number((parseFloat($scope.selectedItems[i].Total) - parseFloat($scope.selectedItems[i].DISC_VALUE) + parseFloat($scope.billMaster.TotalNotTaxableAmount)).toFixed(2));
                            }



                            $scope.WagesNotTaxable = ((parseFloat($scope.selectedItems[i].itemGmWages) * parseFloat($scope.selectedItems[i].ActualItemWeight)) + parseFloat($scope.WagesNotTaxable)).toFixed(2);
                            $scope.GoldNotTaxable = (parseFloat($scope.selectedItems[i].Total) + parseFloat($scope.GoldNotTaxable)).toFixed(2);


                        }
                    }
                }







                // calculate after discount
                if ($scope.bill_type_Id == 19 || $scope.bill_type_Id == 23) {
                    {
                        if (($scope.selectedItems[i].ManufacturingWages != null && $scope.selectedItems[i].ManufacturingWages != "" && $scope.selectedItems[i].ManufacturingWages != undefined) && ($scope.selectedItems[i].WagesDiscValue != null && $scope.selectedItems[i].WagesDiscValue != "" && $scope.selectedItems[i].WagesDiscValue != undefined)) {

                            $scope.selectedItems[i].AfterDiscount = (parseFloat($scope.selectedItems[i].ManufacturingWages) - parseFloat($scope.selectedItems[i].WagesDiscValue)).toFixed(2);
                        }
                        else
                        {
                            $scope.selectedItems[i].AfterDiscount = parseFloat($scope.selectedItems[i].ManufacturingWages);
                        }
                    }
                }
                else if ($scope.bill_type_Id == 24 || $scope.bill_type_Id == 20) {
                    if (($scope.selectedItems[i].ManufacturingWages != null && $scope.selectedItems[i].ManufacturingWages != "" && $scope.selectedItems[i].ManufacturingWages != undefined) && ($scope.selectedItems[i].WagesDiscValue != null && $scope.selectedItems[i].WagesDiscValue != "" && $scope.selectedItems[i].WagesDiscValue != undefined)) {
                        $scope.selectedItems[i].AfterDiscount = (parseFloat($scope.selectedItems[i].ManufacturingWages) - parseFloat($scope.selectedItems[i].WagesDiscValue)).toFixed(2);
                    }
                    else {
                        $scope.selectedItems[i].AfterDiscount = parseFloat($scope.selectedItems[i].ManufacturingWages);
                    }
                }
                else {
                    $scope.selectedItems[i].AfterDiscount = (parseFloat($scope.selectedItems[i].Total) - parseFloat($scope.selectedItems[i].DISC_VALUE )).toFixed(2);
                }

                $scope.footerAfterDiscount = parseFloat($scope.selectedItems[i].AfterDiscount) + $scope.footerAfterDiscount;





                ////////new updates 
                $scope.selectedItems[i].GoldAfterDisc = (parseFloat($scope.selectedItems[i].Total | 0) - parseFloat($scope.selectedItems[i].DISC_VALUE | 0)).toFixed(2);
                $scope.selectedItems[i].WagesAfterDisc = (parseFloat($scope.selectedItems[i].ManufacturingWages | 0) - parseFloat($scope.selectedItems[i].WagesDiscValue | 0)).toFixed(2);

                $scope.footerGoldAfterDisc = (parseFloat($scope.selectedItems[i].GoldAfterDisc | 0) + parseFloat($scope.footerGoldAfterDisc | 0)).toFixed(2);
                $scope.footerWagesAfterDisc = (parseFloat($scope.selectedItems[i].WagesAfterDisc | 0) + parseFloat($scope.footerWagesAfterDisc | 0)).toFixed(2);



                $scope.footerGoldVat = (parseFloat($scope.selectedItems[i].GoldVat | 0) + parseFloat($scope.footerGoldVat |0)).toFixed(2);
                $scope.footerWagesVat = (parseFloat($scope.selectedItems[i].WagesVat | 0) + parseFloat($scope.footerWagesVat | 0)).toFixed(2);
              








                if ($scope.selectedItems[i].itemGmWages != null && $scope.selectedItems[i].itemGmWages != undefined) {
                    $scope.billMaster.totalItemWages = (parseFloat($scope.selectedItems[i].itemGmWages) + parseFloat($scope.billMaster.totalItemWages)).toFixed(2);
                }


                if ($scope.selectedItems[i].WagesDiscValue) {

                    $scope.billMaster.totalManufactWages = (((parseFloat($scope.selectedItems[i].ManufacturingWages) - parseFloat($scope.selectedItems[i].WagesDiscValue)) * parseFloat($scope.Quantity)) + parseFloat($scope.billMaster.totalManufactWages)).toFixed(2);
                }
                else {
                    $scope.billMaster.totalManufactWages = ((parseFloat($scope.selectedItems[i].ManufacturingWages) * parseFloat($scope.Quantity)) + parseFloat($scope.billMaster.totalManufactWages)).toFixed(2);
                }


                $scope.billMaster.TotalDisc = (parseFloat($scope.selectedItems[i].DISC_VALUE ) + parseFloat($scope.billMaster.TotalDisc)).toFixed(2);
                $scope.billMaster.TotalDiscPercentage = (parseFloat($scope.selectedItems[i].DISC_RATE) + parseFloat($scope.billMaster.TotalDiscPercentage)).toFixed(2);


                if ($scope.selectedItems[i].WagesDiscRate) {
                    $scope.billMaster.TotalWagesDiscRate = (parseFloat($scope.selectedItems[i].WagesDiscRate) + parseFloat($scope.billMaster.TotalWagesDiscRate)).toFixed(2);
                }

                if ($scope.selectedItems[i].WagesDiscValue) {
                    $scope.billMaster.TotalWagesDiscValue = (parseFloat($scope.selectedItems[i].WagesDiscValue) + parseFloat($scope.billMaster.TotalWagesDiscValue)).toFixed(2);
                }


                ///////////get manfacturing vat rate by row after discount
                if ($scope.wages_taxValue != undefined && $scope.wages_taxValue != null) {
                    $scope.selectedItems[i].wagesVatAfterDiscForRow = (parseFloat($scope.billMaster.totalManufactWages) * parseFloat($scope.wages_taxValue)) / 100;
                }




                var netWages = 0;
                if ($scope.billMaster.MainVatRate != undefined && $scope.billMaster.MainVatRate != null && $scope.selectedItems[i].SubjectToVAT == true &&
                    ($scope.bill_type_Id == 19 || $scope.bill_type_Id == 20 || $scope.bill_type_Id == 23 || $scope.bill_type_Id == 24)) {
                    
                    netWages = ((parseFloat($scope.selectedItems[i].TotalItemGmWages) - parseFloat($scope.selectedItems[i].WagesDiscValue)) * parseFloat($scope.selectedItems[i].VATRate)) / 100;
                }
                var netVat = 0;
                if ($scope.selectedItems[i].VatValue != undefined && $scope.selectedItems[i].VatValue != null && $scope.bill_type_Id != 19 && $scope.bill_type_Id != 23) {
                    netVat = $scope.selectedItems[i].VatValue;
                }

                var totalGoldAndWages = 0;
                if ($scope.bill_type_Id == 19 || $scope.bill_type_Id == 23) {
                    
                    if ($scope.selectedItems[i].WagesDiscValue == "NaN") {
                        $scope.selectedItems[i].WagesDiscValue = 0;
                    }
                    totalGoldAndWages = parseFloat($scope.selectedItems[i].ManufacturingWages) - parseFloat($scope.selectedItems[i].WagesDiscValue);
                }
                else if ($scope.bill_type_Id == 24 || $scope.bill_type_Id == 20) {
                    totalGoldAndWages = (parseFloat($scope.selectedItems[i].NetTotal) + parseFloat($scope.selectedItems[i].ManufacturingWages) - parseFloat($scope.selectedItems[i].WagesDiscValue)).toFixed(2);
                }
                else {
                    totalGoldAndWages = (parseFloat($scope.selectedItems[i].NetTotal)).toFixed(2);
                }

                $scope.selectedItems[i].TaxTotal = (parseFloat(totalGoldAndWages) + parseFloat(netWages) + parseFloat(netVat)).toFixed(2);
              
                ////////footer totals 
                
                $scope.footerTotalClearness = (parseFloat($scope.selectedItems[i].ClearnessRate) + parseFloat($scope.footerTotalClearness)).toFixed(2);
                $scope.footerTotalGmPrice = (parseFloat($scope.selectedItems[i].GmPrice) + parseFloat($scope.footerTotalGmPrice)).toFixed(2);
                $scope.footerTotalPrice = (parseFloat($scope.selectedItems[i].Price) + parseFloat($scope.footerTotalPrice)).toFixed(2);

                $scope.footerTotalItemGmWages = (parseFloat($scope.selectedItems[i].TotalItemGmWages) + parseFloat($scope.footerTotalItemGmWages)).toFixed(2);

                $scope.footerTotalGoldMan = (parseFloat($scope.selectedItems[i].TotalGoldManfact) + parseFloat($scope.footerTotalGoldMan)).toFixed(2);

                $scope.footerTaxTotal = (parseFloat($scope.selectedItems[i].TaxTotal) + parseFloat($scope.footerTaxTotal)).toFixed(2);
               
            
            }


        }
     

        if ($scope.billMaster.total24Quantity == 0)
            $scope.isTotal24QuantityZero = true;

        if ($scope.billMaster.total22Quantity == 0)
            $scope.isTotal22QuantityZero = true;

        if ($scope.billMaster.total21Quantity == 0)
            $scope.isTotal21QuantityZero = true;

        if ($scope.billMaster.total18Quantity == 0)
            $scope.isTotal18QuantityZero = true;
         


        $scope.sumAllVat = (parseFloat($scope.billMaster.TotalNotTaxableAmount) + parseFloat($scope.billMaster.TotalMainVat) + parseFloat($scope.billMaster.TotalZeroVat) + parseFloat($scope.billMaster.TotalExemptOfTax)).toFixed(2);


        $scope.calculateTotalMustPaid();
        $scope.getDefaultBillPayTypes();


        var totalpay = 0;
        for (var i = 0; i < $scope.payTypesList.length; i++) {
            
            if ($scope.payTypesList[i].PayTypeValue != undefined) {
                totalpay = (parseFloat(totalpay) + parseFloat($scope.payTypesList[i].PayTypeValue)).toFixed(2);
            }

        }
        $scope.billMaster.TotalPaid = totalpay;
        $scope.calculateRemaining();

        $scope.TotalWagesValue = ($scope.wagesTaxToVat - $scope.wagesDiscountVal) * ($scope.wages_taxValue / 100);


        //if ($scope.IsCalcBillDiscOfTotal == undefined || $scope.IsCalcBillDiscOfTotal == null || $scope.IsCalcBillDiscOfTotal == false) {

        if ($scope.bill_type_Id == 20 || $scope.bill_type_Id == 17 || $scope.bill_type_Id == 18 || $scope.bill_type_Id == 24) {

            //$scope.vat = (((parseFloat($scope.wages_taxValue) * parseFloat($scope.billMaster.totalTaxableManufactWages)) / 100) + parseFloat($scope.billMaster.TotalVatValue)).toFixed(2);

            //$scope.ReportData.vat = ((parseFloat($scope.wages_taxValue) * parseFloat($scope.billMaster.totalTaxableManufactWages)) / 100).toFixed(2);
            //if ($scope.ReportData.vat < 0) {
            //    $scope.ReportData.vat *= -1;
            //}
            $scope.TotalVatWagesValue = parseFloat($scope.billMaster.TotalVatValue) + parseFloat($scope.TotalWagesValue);
            $scope.BillTotalAfterTax = parseFloat($scope.billMaster.TotalBeforeTax) + parseFloat($scope.TotalWagesValue);
        }
        else if ($scope.bill_type_Id == 19 || $scope.bill_type_Id == 23) {

            //   $scope.billMaster.TotalVatValue = ((parseFloat($scope.wages_taxValue) * parseFloat(footerTotalItemGmWages)) + parseFloat($scope.billMaster.TotalVatValue)).toFixed(2);
            //$scope.vat = ((parseFloat($scope.wages_taxValue) * parseFloat($scope.billMaster.totalTaxableManufactWages)) / 100).toFixed(2);

            $scope.TotalVatWagesValue = parseFloat($scope.TotalWagesValue);
            $scope.BillTotalAfterTax = parseFloat($scope.billMaster.TotalBeforeTax) + parseFloat($scope.TotalWagesValue);
        }
        else {
            //$scope.vat = $scope.billMaster.TotalVatValue;
            $scope.TotalVatWagesValue = $scope.billMaster.TotalVatValue;
            $scope.BillTotalAfterTax = $scope.billMaster.TotalAfterTax;
        }
        //}

        $scope.GetReportReportTotalsTafqeet();

        if ($scope.bill_type_Id == 18 || $scope.bill_type_Id == 17) {
            $scope.getGoldDataForPrint();
        }
        
    }


    //Calculate the remaining after discount
    $scope.calculateRemaining = function () {
        if ($scope.IsCalcBillDiscOfTotal != undefined && $scope.IsCalcBillDiscOfTotal != null && $scope.IsCalcBillDiscOfTotal == true) {
            if ($scope.billMaster.totalMustPaid == "NaN") {
                $scope.billMaster.totalMustPaid = 0;
                $scope.selectedItems[i].WagesDiscValue = 0.0;
            }
            $scope.billMaster.totalRemaining = (parseFloat($scope.billMaster.totalMustPaid) - parseFloat($scope.billMaster.TotalPaid)).toFixed(2);
        }
        else {

            if ($scope.billMaster.TotalAfterTax > 0) {
                $scope.billMaster.totalRemaining = (parseFloat($scope.billMaster.TotalAfterTax) - parseFloat($scope.billMaster.TotalPaid)).toFixed(2);
            }
            else {
                $scope.billMaster.totalRemaining = (parseFloat($scope.billMaster.totalMustPaid) - parseFloat($scope.billMaster.TotalPaid)).toFixed(2);
             
            }
        }


    }

    $scope.calculateTotalMustPaid = function () {

        /////////////////t3deel el calculations///////////////////////////////


        if ($scope.billMaster.BILL_TOTAL_EXTRA == 0 || $scope.billMaster.BILL_TOTAL_EXTRA == null || $scope.billMaster.BILL_TOTAL_EXTRA == undefined) {
            $scope.billMaster.BILL_TOTAL_EXTRA = 0;
        }

        if ($scope.billMaster.TotalAllDisc == null || $scope.billMaster.TotalAllDisc == 0 || $scope.billMaster.TotalAllDisc == undefined) {
            $scope.billMaster.TotalAllDisc = 0;
        }

        if ($scope.billMaster.TotalVatValue == null || $scope.billMaster.TotalVatValue == 0 || $scope.billMaster.TotalVatValue == undefined) {
            $scope.billMaster.TotalVatValue = 0;
        }


        if ($scope.IsCalcBillDiscOfTotal != undefined && $scope.IsCalcBillDiscOfTotal != null && $scope.IsCalcBillDiscOfTotal == true) {
            // $scope.billMaster.TotalBeforeTax = (parseFloat($scope.billMaster.TotalAfterDiscount) + parseFloat($scope.billMaster.TotalNotTaxableAmount)).toFixed(2);

            //if ($scope.BillVatRate != undefined && $scope.BillVatRate != null && $scope.BillVatRate != 0) {
            //    var vatVal = ((parseFloat($scope.BillVatRate) / 100) * parseFloat($scope.billMaster.TotalBeforeTax)).toFixed(2);
            //    $scope.billMaster.TotalAfterTax = (parseFloat($scope.billMaster.TotalBeforeTax) + parseFloat(vatVal)).toFixed(2);
            //}
        }
        else {
            $scope.billMaster.TotalBeforeTax = parseFloat($scope.billMaster.totalOfTotalPrice).toFixed(2);
            $scope.billMaster.TotalAfterTax = (parseFloat($scope.billMaster.TotalBeforeTax) + parseFloat($scope.billMaster.TotalVatValue)).toFixed(2);
        }



        if ($scope.billMaster.BILL_TOTAL_DISC == null || $scope.billMaster.BILL_TOTAL_DISC == undefined) {
            $scope.billMaster.BILL_TOTAL_DISC = 0;
        }
        $scope.billMaster.TotalAllDisc = (parseFloat($scope.billMaster.TotalDisc) + parseFloat($scope.billMaster.BILL_TOTAL_DISC)).toFixed(2);


        if ($scope.billMaster.BILL_PERCENTAGE_DISC == null || $scope.billMaster.BILL_PERCENTAGE_DISC == undefined) {
            $scope.billMaster.BILL_PERCENTAGE_DISC = 0;
        }
        $scope.billMaster.TotalAllDiscPercentage = (parseFloat($scope.billMaster.TotalDiscPercentage) + parseFloat($scope.billMaster.BILL_PERCENTAGE_DISC)).toFixed(2);


        $scope.billMaster.totalMustPaid = $scope.footerTaxTotal;
      
        ////if ($scope.bill_type_Id == 19 || $scope.bill_type_Id == 23) {
        ////    //$scope.vat = ((parseFloat($scope.wages_taxValue) * parseFloat($scope.billMaster.totalTaxableManufactWages)) / 100).toFixed(2);
        ////    $scope.billMaster.totalMustPaid = (parseFloat($scope.billMaster.totalManufactWages) + parseFloat($scope.billMaster.MainVatValue) + parseFloat($scope.billMaster.BILL_TOTAL_EXTRA) - parseFloat($scope.billMaster.TotalAllDisc)).toFixed(2);
        ////}

        ////else if ($scope.bill_type_Id == 20 || $scope.bill_type_Id == 24) {
        ////    //$scope.vat = (((parseFloat($scope.wages_taxValue) * parseFloat($scope.billMaster.totalTaxableManufactWages)) / 100) + parseFloat($scope.billMaster.TotalVatValue)).toFixed(2);
        ////    // $scope.vat = ((parseFloat($scope.wages_taxValue) * parseFloat($scope.footerTotalItemGmWages)) / 100).toFixed(2);
        ////    $scope.billMaster.totalMustPaid = (parseFloat($scope.billMaster.totalManufactWages) + parseFloat($scope.billMaster.MainVatValue) + parseFloat($scope.billMaster.BILL_TOTAL_EXTRA) + parseFloat($scope.billMaster.totalOfTotalPrice) - parseFloat($scope.billMaster.TotalAllDisc)).toFixed(2);
        ////}

        ////else {
        ////    $scope.billMaster.totalMustPaid = (parseFloat($scope.billMaster.totalOfTotalPrice) + parseFloat($scope.billMaster.TotalVatValue) + parseFloat($scope.billMaster.BILL_TOTAL_EXTRA) - parseFloat($scope.billMaster.TotalAllDisc)).toFixed(2);
        ////}

        if ($scope.IsCalcBillDiscOfTotal != undefined && $scope.IsCalcBillDiscOfTotal != null && $scope.IsCalcBillDiscOfTotal == true) {
            $scope.billMaster.TotalBeforeDiscount = (parseFloat($scope.billMaster.TotalTaxableAmount) + parseFloat($scope.billMaster.TotalNotTaxableAmount) + parseFloat($scope.billMaster.TotalExemptOfTax)).toFixed(2);
            $scope.billMaster.TotalAfterDiscount = (parseFloat($scope.billMaster.TotalBeforeDiscount) - parseFloat($scope.billMaster.TotalAllDisc)).toFixed(2);
            var vatVal;
            $scope.billMaster.TotalBeforeTax = (parseFloat($scope.billMaster.TotalAfterDiscount)).toFixed(2);

            $scope.billMaster.TotalAfterTax = (parseFloat($scope.billMaster.TotalBeforeTax) + parseFloat($scope.billMaster.TotalVatValue)).toFixed(2);

            if ($scope.BillVatRate != undefined && $scope.BillVatRate != null && $scope.BillVatRate != 0) {
                // vatVal = ((parseFloat($scope.BillVatRate) / 100) * parseFloat($scope.billMaster.TotalBeforeTax)).toFixed(2);
                vatVal = ((parseFloat($scope.BillVatRate) / 100) * (parseFloat($scope.billMaster.TotalTaxableAmount) - parseFloat($scope.billMaster.TotalAllDisc))).toFixed(2);
                $scope.TotalVatWagesValue = vatVal;
                $scope.billMaster.TotalAfterTax = (parseFloat($scope.billMaster.TotalBeforeTax) + parseFloat(vatVal)).toFixed(2);
            }

            ////hashed to recalcualte total must paid as total tax of grid

            //////if ($scope.bill_type_Id == 19 || $scope.bill_type_Id == 23) {
            //////    //$scope.vat = ((parseFloat($scope.wages_taxValue) * parseFloat($scope.billMaster.totalTaxableManufactWages)) / 100).toFixed(2);
            //////    $scope.billMaster.totalMustPaid = (parseFloat($scope.billMaster.totalManufactWages) + parseFloat($scope.billMaster.MainVatValue) + parseFloat($scope.billMaster.BILL_TOTAL_EXTRA) - parseFloat($scope.billMaster.TotalAllDisc)).toFixed(2);
            //////}
            //////else if ($scope.bill_type_Id == 20 || $scope.bill_type_Id == 24) {
            //////    //$scope.vat = ((parseFloat($scope.wages_taxValue) * parseFloat($scope.billMaster.totalTaxableManufactWages)) / 100).toFixed(2);
            //////    $scope.billMaster.totalMustPaid = (parseFloat($scope.billMaster.totalManufactWages) + parseFloat($scope.billMaster.MainVatValue) + parseFloat($scope.billMaster.totalOfTotalPrice) + parseFloat($scope.billMaster.BILL_TOTAL_EXTRA) - parseFloat($scope.billMaster.TotalAllDisc)).toFixed(2);
            //////}

            //////else {
            //////    $scope.billMaster.totalMustPaid = (parseFloat($scope.billMaster.TotalAfterTax)).toFixed(2);
            //////}
            $scope.billMaster.totalMustPaid = $scope.footerTaxTotal;
          
        }

        else {
            //if ($scope.bill_type_Id == 19 || $scope.bill_type_Id == 23) {
            //    $scope.billMaster.TotalBeforeDiscount = parseFloat($scope.footerTotalItemGmWages).toFixed(2);
            //}
            //else if ($scope.bill_type_Id == 19 || $scope.bill_type_Id == 23) {
            //    $scope.billMaster.TotalBeforeDiscount = parseFloat($scope.billMaster.totalOfTotalPrice + $scope.footerTotalItemGmWages).toFixed(2);
            //}
            //else {
            //    $scope.billMaster.TotalBeforeDiscount = parseFloat($scope.billMaster.totalOfTotalPrice).toFixed(2);
            //}
            $scope.billMaster.TotalBeforeDiscount = (parseFloat($scope.billMaster.TotalTaxableAmount) + parseFloat($scope.billMaster.TotalNotTaxableAmount) + parseFloat($scope.billMaster.TotalExemptOfTax)).toFixed(2);
            $scope.billMaster.TotalAfterDiscount = (parseFloat($scope.billMaster.TotalBeforeDiscount) - parseFloat($scope.billMaster.TotalAllDisc)).toFixed(2);
            $scope.billMaster.TotalBeforeTax = (parseFloat($scope.billMaster.TotalAfterDiscount)).toFixed(2);

        }

        if ($scope.bill_type_Id == 20) {
            $scope.ReportData.reportTotalBeforeTax = (parseFloat($scope.billMaster.TotalBeforeTax) + parseFloat($scope.footerTotalItemGmWages)).toFixed(2);
            $scope.ReportData.reportTotalAfterTax = (parseFloat($scope.ReportData.reportTotalBeforeTax) + parseFloat($scope.billMaster.MainVatValue)).toFixed(2);
        }
        $scope.calculateRemaining();
    }

    $scope.calculateTotalMustPaidBasedOnDiscount = function () {
        $scope.billMaster.BILL_PERCENTAGE_DISC = 0;

        if ($scope.IsCalcBillDiscOfTotal != undefined && $scope.IsCalcBillDiscOfTotal != null && $scope.IsCalcBillDiscOfTotal == true) {
            $scope.billMaster.BILL_PERCENTAGE_DISC = Number(((parseFloat($scope.billMaster.BILL_TOTAL_DISC) * 100) / parseFloat($scope.billMaster.TotalTaxableAmount)).toFixed(2));
        }
        else {
            $scope.billMaster.BILL_PERCENTAGE_DISC = Number(((parseFloat($scope.billMaster.BILL_TOTAL_DISC) * 100) / parseFloat($scope.billMaster.totalMustPaid)).toFixed(2));
        }


        $scope.calculateRemaining();
        $scope.calculateTotals();
        $scope.calculateTotalMustPaid();
    }

    $scope.calculateTotalMustPaidBasedOnDiscountPercentage = function () {
        $scope.billMaster.BILL_TOTAL_DISC = 0;

        if ($scope.IsCalcBillDiscOfTotal != undefined && $scope.IsCalcBillDiscOfTotal != null && $scope.IsCalcBillDiscOfTotal == true) {
            $scope.billMaster.BILL_TOTAL_DISC = Number(((parseFloat($scope.billMaster.BILL_PERCENTAGE_DISC) / 100) * parseFloat($scope.billMaster.TotalTaxableAmount)).toFixed(2));
        }

        else {
            $scope.billMaster.BILL_TOTAL_DISC = Number(((parseFloat($scope.billMaster.BILL_PERCENTAGE_DISC) / 100) * parseFloat($scope.billMaster.totalMustPaid)).toFixed(2));
        }

        $scope.calculateRemaining();
        $scope.calculateTotals();
        $scope.calculateTotalMustPaid();
    }


    ///////////////////get gold data for print اشعار اقفال بيه و شراء
    $scope.getGoldDataForPrint = function () {

        $scope.goldPrint = {};
        $scope.goldPrint.disc24 = 0;
        $scope.goldPrint.price24 = 0;

        $scope.goldPrint.disc22 = 0;
        $scope.goldPrint.price22 = 0;

        $scope.goldPrint.disc21 = 0;
        $scope.goldPrint.price21 = 0;

        $scope.goldPrint.disc18 = 0;
        $scope.goldPrint.price18 = 0;

        $scope.goldPrint.afterDisc24 = 0;
        $scope.goldPrint.afterDisc22 = 0;
        $scope.goldPrint.afterDisc21 = 0;
        $scope.goldPrint.afterDisc18 = 0;

        $scope.goldPrint.totalGoldPrice = 0;
        $scope.goldPrint.totalGoldDisc = 0;
        $scope.goldPrint.totalGoldAfterDisc = 0;
        $scope.TotalDisc = 0;

        $scope.footerGoldAfterDisc = 0;
        $scope.footerWagesAfterDisc = 0;



        $scope.footerGoldVat = 0;
        $scope.footerWagesVat = 0;


        for (var i = 0; i < $scope.selectedItems.length; i++) {
            if ($scope.selectedItems[i].ITEM_CODE != null && $scope.selectedItems[i].ITEM_CODE != "" && $scope.selectedItems[i].ITEM_CODE != undefined) {

                if ($scope.selectedItems[i].DISC_VALUE == null) {
                    $scope.selectedItems[i].DISC_VALUE = 0;
                }

                if ($scope.selectedItems[i].ARName == "عيار 24") {
                    $scope.goldPrint.disc24 = parseFloat($scope.selectedItems[i].DISC_VALUE) + parseFloat($scope.goldPrint.disc24);
                    $scope.goldPrint.price24 = parseFloat($scope.selectedItems[i].Total) + parseFloat($scope.goldPrint.price24);
                }

                else if ($scope.selectedItems[i].ARName == "عيار 22") {
                    $scope.goldPrint.disc22 = parseFloat($scope.selectedItems[i].DISC_VALUE) + parseFloat($scope.goldPrint.disc22);
                    $scope.goldPrint.price22 = parseFloat($scope.selectedItems[i].Total) + parseFloat($scope.goldPrint.price22);
                }

                else if ($scope.selectedItems[i].ARName == "عيار 21") {
                    $scope.goldPrint.disc21 = parseFloat($scope.selectedItems[i].DISC_VALUE) + parseFloat($scope.goldPrint.disc21);
                    $scope.goldPrint.price21 = parseFloat($scope.selectedItems[i].Total) + parseFloat($scope.goldPrint.price21);
                }

                else if ($scope.selectedItems[i].ARName == "عيار 18") {
                    $scope.goldPrint.disc18 = parseFloat($scope.selectedItems[i].DISC_VALUE) + parseFloat($scope.goldPrint.disc18);
                    $scope.goldPrint.price18 = parseFloat($scope.selectedItems[i].Total) + parseFloat($scope.goldPrint.price18);
                }

            }
        }

        $scope.goldPrint.afterDisc24 = parseFloat($scope.goldPrint.price24) - parseFloat($scope.goldPrint.disc24);
        $scope.goldPrint.afterDisc22 = parseFloat($scope.goldPrint.price22) - parseFloat($scope.goldPrint.disc22);
        $scope.goldPrint.afterDisc21 = parseFloat($scope.goldPrint.price21) - parseFloat($scope.goldPrint.disc21);
        $scope.goldPrint.afterDisc18 = parseFloat($scope.goldPrint.price18) - parseFloat($scope.goldPrint.disc18);

        $scope.goldPrint.totalGoldPrice = parseFloat($scope.goldPrint.price24) + parseFloat($scope.goldPrint.price22) + parseFloat($scope.goldPrint.price21) + parseFloat($scope.goldPrint.price18);
        $scope.goldPrint.totalGoldDisc = parseFloat($scope.goldPrint.disc24) + parseFloat($scope.goldPrint.disc22) + parseFloat($scope.goldPrint.disc21) + parseFloat($scope.goldPrint.disc18);
        $scope.goldPrint.totalGoldAfterDisc = parseFloat($scope.goldPrint.afterDisc24) + parseFloat($scope.goldPrint.afterDisc22) + parseFloat($scope.goldPrint.afterDisc21) + parseFloat($scope.goldPrint.afterDisc18);
    }

    ////////////////////////////////

    $scope.addGridBillDetailsItem = function () {
        var found = false;
        for (var i = 0; i < $scope.selectedItems.length; i++) {
            if ($scope.IsQuantityCalculated == true) {
                $scope.Quantity = $scope.selectedItems[i].Quantity;
            }
            else {
                $scope.Quantity = 1;
            }

            $scope.calcgridBillDetailsItemCaliber($scope.selectedItems[i], $scope.Quantity);
            if ($scope.selectedItems[i].ITEM_CODE == null || $scope.selectedItems[i].ITEM_CODE == "") {

                found = true;
            }
        }
        if (!found) {
            $scope.gridBillDetailsItem = {
                ITEM_CODE: "", ITEM_AR_NAME: "", DISC_VALUE: "", DISC_RATE: "", Quantity: "", Unit: null, SubjectToVAT: null, VATRate: null, CaliberID: null, ARName: null, LatName: null, ClearnessRate: null, Price: "", Total: "", GridRowNumber: "", UnitNameAr: "", CaliberNameAr: "", Caliber18: null, Caliber21: null, Caliber22: null, Caliber24: null, ItemStatus: 0, LockPrice: 0

            };
            $scope.selectedItems.push($scope.gridBillDetailsItem);

        }

    };

    $scope.addItemToBillDetailsItems = function (item) {
        
        //check wheather the item exist in array
        var foundItem = null;
        for (var i = 0; i < $scope.selectedItems.length; i++) {
            if (String($scope.selectedItems[i].ITEM_CODE) == item.ITEM_CODE) {
                foundItem = $scope.selectedItems[i];
            }
        }


        if ($scope.IsQuantityCalculated == true) {
            $scope.Quantity = $scope.gridBillDetailsItem.Quantity;
        }
        else {
            $scope.Quantity = 1;
        }

        // var foundItem = $filter('filter')($scope.selectedItems, { ITEM_CODE: item.ITEM_CODE }, true);
        if (foundItem != undefined && foundItem != null) {
            //   
            $scope.gridBillDetailsItem = foundItem;

            if ($scope.IsRepeatItem != true) {
                $scope.gridBillDetailsItem.Quantity = +$scope.gridBillDetailsItem.Quantity + 1;

                $scope.calcgridBillDetailsItemCaliber($scope.gridBillDetailsItem, $scope.Quantity);

                //  $scope.gridBillDetailsItem.Total = (parseFloat($scope.Quantity) * parseFloat($scope.gridBillDetailsItem.Price)).toFixed(2);

                $scope.addGridBillDetailsItem();
                $scope.calcgridBillDetailsItemCaliber($scope.selectedItems[$scope.selectedItems.length - 1], $scope.Quantity);
            }
            else {

                if ($scope.BillRowsNumber > 0) {
                    if ((parseFloat($scope.BillRowsNumber)) + 1 > $scope.selectedItems.length) {
                        $scope.addRowOfSerachItems(item);
                    }
                    else {
                        swal('عفواً',
                            'لا يمكن اضافه صفوف زياده',
                            'error');
                    }
                }
                else {
                    $scope.addRowOfSerachItems(item);
                }
            }

        } else {
            $scope.addRowOfSerachItems(item);
        }
        
        if ($scope.HasGoldModule) {
            $scope.changePriceByType();
            $scope.getTotalQuantity();
        } else {
            $scope.calculateTotals();
        }
    };


    $scope.dialogWithTimeOut = function () {
        var dialog = ngDialog.open({
            template: 'temp.html',
            className: 'ngdialog-theme-default',
            scope: $scope,
        });
        setTimeout(function () {
            dialog.close();
        }, 2000);
    };


    $scope.calcgridBillDetailsItemCaliber = function (item, Quantity) {
        // 
        item.Caliber18 = parseFloat(Quantity * item.ClearnessRate * (24 / 18)).toFixed(2);
        item.Caliber21 = parseFloat(Quantity * item.ClearnessRate * (24 / 21)).toFixed(2);
        item.Caliber22 = parseFloat(Quantity * item.ClearnessRate * (24 / 22)).toFixed(2);
        item.Caliber24 = parseFloat(Quantity * item.ClearnessRate * (24 / 24)).toFixed(2);
    };

    $scope.removeGridBillDetailsItem = function (index) {
        $scope.selectedItems.splice(index, 1);
        $scope.calculateTotals();
        $scope.getTotalQuantity();

    };

    $scope.getItemsByGroupID = function (itemGroup) {
        $scope.items = [];
        PointOfSaleService.getItemsByGroupID(itemGroup.GroupID).then(function (response) {
            $scope.items = response.data;
        })
    }


    $scope.PointOfSaleList = [];
    $scope.PointOfSaleTotalCount = 0;
    $scope.pageNum = 1;
    $scope.pageSize = 10;

    $scope.searchBy = "";


    $scope.maxSize = 5;

    $scope.pagesCount = 0;

    $scope.getPagesCount = function () {
        var div = Math.floor($scope.PointOfSaleTotalCount / $scope.pageSize);
        var rem = $scope.PointOfSaleTotalCount % $scope.pageSize;
        if (rem > 0) {
            div = div + 1;
        }
        return div;
    }
    $scope.maxSize = 5;

    $scope.getAllMainItemGroupsList = function () {

        PointOfSaleService.getAllMainItemGroups().then(function (response) {

            $scope.mainItemsGroupsList = response.data;
        })
    }

    $scope.getAllBillDetails = function (bill_ID) {
        PointOfSaleService.getAllBillDetails(bill_ID).then(function (response) {
            $scope.billDetails = response.data;
            if ($scope.billDetails != undefined || $scope.billDetails != []) {
                $scope.convertBillDetailsToSelectedItems();
            }
            $scope.addGridBillDetailsItem();
            // 
            $scope.calculateTotals();
        })
    }

    $scope.clearEnity = function () {

        $location.search('foo', null);

        if ($location.path() === $rootScope.locationPath) {
            $location.replace();
        }

        $scope.billMaster = {};
        $scope.operation = "Insert";
        $scope.cleargridBillDetailsItem();
        $scope.billMaster.BILL_DATE = new Date();
        $scope.getBillNumber();
        $scope.clearTotals();
        //  $scope.getLastGoldWorldPrice();
        $scope.billSearch = {};
        $scope.isSelectedA = false;
        $scope.getAllCurrencies();
        $scope.getCurrentDate();
        // $scope.clearSearchItems();
        // $scope.getAllTpayTypes();
        $scope.getAllSellsTypes();
        $scope.getAllOnLoad();
        $scope.searchItems = [];
        $scope.isEditReason = false;

        $scope.footerTotalVatValue = 0;
        $scope.footerTaxTotal = 0;


        $scope.footerTotalGmPrice = 0;
        $scope.footerTotalPrice = 0;
        $scope.footerTotalClearness = 0;
        $scope.footerTotalGoldMan = 0;


        $scope.footerTotalItemGmWages=0;
        
        $scope.footerTotalGoldMan = 0;
        
        $scope.footerTaxTotal = 0;



        $scope.footerGoldAfterDisc =0;
        $scope.footerWagesAfterDisc =0;
        
        
        
        $scope.footerGoldVat = 0;
        $scope.footerWagesVat = 0;
        

        


        for (var i = 1; i < $scope.matrixList.length; i++) {
            $scope.matrixList[i].Col21 = 0;
            $scope.matrixList[i].Col22 = 0;
            $scope.matrixList[i].Col24 = 0;
            $scope.matrixList[i].ColQuantity = 0;
            $scope.matrixList[i].Col18 = 0;
            $scope.matrixList[i].TransQuant = 0;
        }

        for (var i = 0; i < $scope.payTypesList.length; i++) {
            $scope.payTypesList[i].PayTypeValue = 0;
            $scope.payTypesList[i].BankCommissionValue = 0;
            $scope.payTypesList[i].CommissionTaxValue = 0;
            $scope.payTypesList[i].AccountID = null;
        }

        $scope.isTotal24QuantityZero = false;
        $scope.isTotal22QuantityZero = false;
        $scope.isTotal21QuantityZero = false;
        $scope.isTotal18QuantityZero = false;

    }
    //clear total calculations
    $scope.clearTotals = function () {
        $scope.billMaster.totalQuantity = null;
        $scope.billMaster.totalPrice = null;
        $scope.totalVATRate = null;
        $scope.TotalVatValue = null;

        $scope.billMaster.total24Quantity = null;
        $scope.billMaster.total22Quantity = null;
        $scope.billMaster.total21Quantity = null;
        $scope.billMaster.total18Quantity = null;

        $scope.billMaster.BILL_TOTAL_EXTRA = null;
        $scope.billMaster.totalMustPaid = null;
        $scope.billMaster.TotalPaid = null;
        $scope.billMaster.totalRemaining = null;
        $scope.billMaster.BILL_TOTAL_DISC = null;



    }

    $scope.refreshData = function () {
        $scope.getAllOnLoad();
    }
    $scope.savePointOfSale = function () {
        $scope.saveEntity();
    }

    $scope.convertDetailsItems = function () {
        $scope.billDetails = [];
        for (var i = 0; i < $scope.selectedItems.length; i++) {
            var x = {};
            x.BILL_ID = $scope.billMaster.BILL_ID;
            // x.ITEM_ID = $scope.selectedItems[i].ITEM_CODE;
            x.ITEM_ID = $scope.selectedItems[i].ITEM_ID;
            x.QTY = $scope.selectedItems[i].Quantity;
            
            x.UNIT_PRICE = parseFloat($scope.selectedItems[i].Price).toFixed(2);
            x.DISC_RATE = $scope.selectedItems[i].DISC_RATE;
            x.CaliberID = $scope.selectedItems[i].CaliberID;
            x.ARName = $scope.selectedItems[i].ARName;
            x.LatName = $scope.selectedItems[i].LatName;
            x.ClearnessRate = $scope.selectedItems[i].ClearnessRate;
            x.SubjectToVAT = $scope.selectedItems[i].SubjectToVAT;

            if ($scope.selectedItems[i].VATRate != undefined && $scope.selectedItems[i].VATRate != null) {
                x.VATRate = $scope.selectedItems[i].VATRate;
            }
            else { x.VATRate = 0; }
            x.DISC_VALUE = $scope.selectedItems[i].DISC_VALUE;

            x.ItemWeight = $scope.selectedItems[i].ItemWeight;
            //x.itemGmWages = $scope.selectedItems[i].itemGmWages;
            x.ManufacturingWages = $scope.selectedItems[i].ManufacturingWages;

            x.itemGmWages = 0;
            if (x.ItemStatus == 2) {
                x.itemGmWages = (parseFloat(x.ManufacturingWages) / parseFloat(x.ItemWeight)).toFixed(2);
                x.ManufacturingWages = x.ManufacturingWages;
            }
            else {
                x.itemGmWages = x.ManufacturingWages;
                x.ManufacturingWages = (parseFloat(x.itemGmWages) * parseFloat(x.ItemWeight)).toFixed(2);
            }




            x.CostCenterID = $scope.selectedItems[i].CostCenterID;

            x.ProfitMargin = $scope.selectedItems[i].ProfitMargin;
            x.ManufacturingWages = $scope.selectedItems[i].ManufacturingWages;

            x.WagesDiscValue = $scope.selectedItems[i].WagesDiscValue;
            x.WagesDiscRate = $scope.selectedItems[i].WagesDiscRate;
            x.ItemStatus = $scope.selectedItems[i].ItemStatus;

            x.COMPANY_ID = $scope.selectedItems[i].COMPANY_ID;
            x.GemsWeight = $scope.selectedItems[i].GemsWeight;
            x.GemsWages = $scope.selectedItems[i].GemsWages;

            x.ActualItemWeight = $scope.selectedItems[i].ActualItemWeight;
            x.IsExemptOfTax = $scope.selectedItems[i].IsExemptOfTax;
            x.LockPrice = $scope.selectedItems[i].LockPrice;
            x.GRID_ROW_NUMBER = i + 1;
            $scope.billDetails.push(x);
        }
    }


    $scope.validateWeight = function () {
        for (var i = 0; i < $scope.billDetails.length; i++) {
            if ($scope.billDetails[i].ActualItemWeight == 0) {
                return true;
            }

        }

        return false;
    }
    $scope.saveEntity = function () {
        debugger;
        if (localStorageService.get('branchId') != null) {
            $scope.billMaster.COM_BRN_ID = localStorageService.get('branchId').branchId;
        }

        if ($scope.bill_type_Id != undefined && $scope.bill_type_Id != null && $scope.bill_type_Id != 0) {
            $scope.billMaster.BILL_TYPE = $scope.bill_type_Id;
        }


        $scope.billMaster.BILL_TOTAL = $scope.billMaster.totalMustPaid;
        $scope.billMaster.BILL_SETTING_ID = $scope.getBillTypeQueryString();

        $scope.convertDetailsItems();
        //assign Bill_ID 
        for (var i = 0; i < $scope.billDetails.length; i++) {
            $scope.billDetails[i].BILL_ID = $scope.billMaster.BILL_ID;
            // $scope.billDetails[i].BILL_ID = $scope.billMaster.BILL_NUMBER;
        }
        $scope.billMaster.accounts = [];
        if (($scope.billMaster.BILL_TYPE == 13 || $scope.billMaster.BILL_TYPE == 14) && $scope.validateWeight()) { 
                    swal('عفواً',
                        'يجب ادخال الوزن المنصرف',
                        'error');       
        }
        else
        {
        $q.all(
            [
                $scope.callAccountObject(),
                //$scope.getEntryNumber()

            ])
            .then(function (allResponses) {
                if ($scope.billMaster !== undefined && $scope.billMaster !== null && $scope.billMaster.BILL_TOTAL != undefined) {
                    if ($scope.operation == "Insert") {


                        var countEmptyAccount = 0;
                        var countPayValue = 0;

                        if (!$scope.IsPayTypes) {

                            for (var i = 0; i < $scope.payTypesList.length; i++) {
                                //  $scope.payTypesList[i].BillMasterID = results.data;

                                if ($scope.payTypesList[i].PayTypeValue != "" && $scope.payTypesList[i].PayTypeValue != undefined && $scope.payTypesList[i].PayTypeValue != 0) {
                                    if ($scope.payTypesList[i].AccountID == undefined || $scope.payTypesList[i].AccountID == null) {
                                        countEmptyAccount++;
                                        countPayValue = parseFloat($scope.payTypesList[i].PayTypeValue) + parseFloat(countPayValue);
                                    }
                                }
                            }
                        }


                        if (countEmptyAccount > 0) {
                            swal('عفواً',
                                'يجب اختيار حساب لطرق الدفع',
                                'error');
                        }

                        else if (countPayValue > $scope.billMaster.totalMustPaid) {
                            swal('عفواً',
                                'يجب ان يكون مجموع طرق الحساب اقل من او يساوي المبلغ المستحق دفعه ',
                                'error');
                        }

                        else {
                            $scope.add($scope.billMaster).then(function (results) {
                                if (results.data) {
                                    $scope.billMasterID = results.data;
                                    if ($scope.isAutoPosted == true) {

                                        if ($scope.bill_type_Id != 9) {
                                            $scope.saveEntry();
                                        }
                                    }
                                    else {
                                        $scope.billMaster.BILL_IS_POST = false;
                                    }

                                    $scope.addDetails($scope.billDetails).then(function (res) {
                                        if (res.data) {

                                            for (var i = 0; i < $scope.payTypesList.length; i++) {
                                                $scope.payTypesList[i].BillMasterID = results.data;
                                            }
                                            $scope.insertBillPayType($scope.payTypesList, results.data);


                                            var transList = [];
                                            for (var i = 1; i < $scope.matrixList.length; i++) {

                                                $scope.matrixList[i].BillMasterID = results.data;
                                                transList.push($scope.matrixList[i]);
                                                // billCaliberTransactionsService.insert($scope.matrixList[i]);
                                            }


                                            $scope.insertMatrixList(transList, results.data)

                                            swal({
                                                title: "هل تريد طباعة الفاتورة?",
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
                                        }
                                        else {
                                            $scope.delete($scope.billMaster).then(function (res) {
                                                swal('عفواً',
                                                    'حدث خطأ عند حفظ بيانات نقطة المبيع',
                                                    'error');
                                            })

                                        }

                                    }, function (error) {
                                        $scope.delete($scope.billMaster).then(function (res) {
                                            swal('عفواً',
                                                'حدث خطأ عند حفظ بيانات نقطة المبيع',
                                                'error');
                                        })

                                    })
                                    //PointOfSaleService.addBillDetails($scope.billDetails);


                                } else {
                                    swal('عفواً',
                                        'حدث خطأ عند حفظ بيانات نقطة المبيع',
                                        'error');
                                }

                            }, function (error) {
                                swal('عفواً',
                                    'حدث خطأ عند حفظ بيانات نقطة المبيع',
                                    'error');
                            });
                            //end of check count empty account of pay types
                        }

                    }

                    else {

                        $scope.checkExistbillNumber().then(function (response) {
                            if (response.data) {

                                if (countEmptyAccount > 0) {
                                    swal('عفواً',
                                        'يجب اختيار حساب لطرق الدفع',
                                        'error');
                                }
                                else {

                                    if (($scope.billMaster.EditReason == null || $scope.billMaster.EditReason == undefined || $scope.billMaster.EditReason == "") && $scope.isEditReason == true) {
                                        swal('عفواً',
                                            'يجب ذكر سبب التعديل ',
                                            'error');
                                    }

                                    else {
                                        $scope.update($scope.billMaster).then(function (results) {
                                            if (results.data) {
                                                if ($scope.bill_type_Id != 9) {
                                                    $scope.saveEntry();
                                                }

                                                PointOfSaleService.addBillDetails($scope.billDetails).then(function (result) {
                                                    if (result.data) {

                                                        for (var i = 0; i < $scope.payTypesList.length; i++) {
                                                            $scope.payTypesList[i].BillMasterID = $scope.billMaster.BILL_ID;
                                                        }

                                                        $scope.insertBillPayType($scope.payTypesList, $scope.billMaster.BILL_ID);

                                                        var transList = [];
                                                        for (var i = 1; i < $scope.matrixList.length - 1; i++) {

                                                            $scope.matrixList[i].BillMasterID = $scope.billMaster.BILL_ID;
                                                            transList.push($scope.matrixList[i]);
                                                        }

                                                        $scope.insertMatrixList(transList, $scope.billMaster.BILL_ID);

                                                    }
                                                });
                                            }

                                            swal({
                                                title: 'تم',
                                                text: 'تعديل بيانات نقطة المبيع بنجاح',
                                                type: 'success',
                                                timer: 1500,
                                                showConfirmButton: false
                                            }

                                            ).then(function () {
                                                $scope.printDiv('Bill');
                                                $scope.clearEnity();
                                                $scope.refreshData();

                                            }, function () {
                                                $scope.clearEnity();
                                                $scope.refreshData();
                                            });

                                        }, function (error) {
                                            console.log(error.data.message);
                                            swal('عفواً',
                                                'حدث خطأ عند تعديل بيانات نقطة المبيع',
                                                'error');
                                        });
                                    }
                                }

                            }


                            else {
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
                }
                else
                {
                    swal('عفواً',
                         'لا يمكن ادخال الفاتورة بدون ادخال اصناف  ',
                        'error');  
                }

            }, function (error) {
                //This will be called if $q.all finds any of the requests erroring.
                var abc = error;
                var def = 99;
            });
    }
    }

    $scope.getItemDataUsingItemCode = function (ItemCode, sellTypeID) {

        //
        if (sellTypeID == undefined || sellTypeID == null) {
            sellTypeID = 0;
        }

        var selectedItemsBeforeChange = $scope.selectedItems;
        if (ItemCode != "" && ItemCode != null) {
            PointOfSaleService.getItemDataUsingItemCode(ItemCode, sellTypeID).then(function (response) {
                var item = response.data;
                if (item != undefined && item != null) {
                    $scope.addItemToBillDetailsItems(item);
                    //change
                    //  $scope.getTotalQuantity();
                }
                else {
                    $scope.addGridBillDetailsItem();
                }
            })
        }
    }



    $scope.getItemCurrentQuantity = function (ItemID, StoreID) {

        if (ItemID == undefined || ItemID == null) {
            return;
        }

        if (StoreID == undefined || StoreID == null) {
            StoreID = 0;
        }

        var selectedItemsBeforeChange = $scope.selectedItems;
        if (ItemID != "" && ItemID != null) {
            PointOfSaleService.getItemCurrentQuantity(ItemID, StoreID).then(function (response) {
                var ItemCurrentQuantity = response.data;
                return ItemCurrentQuantity;
            })
        }
    }



    //function eventFire(el, etype) {
    //    if (el.fireEvent) {
    //        el.fireEvent('on' + etype);
    //    } else {
    //        var evObj = document.createEvent('Events');
    //        evObj.initEvent(etype, true, false);
    //        el.dispatchEvent(evObj);
    //    }
    //}

    //funcion to search for items and get it using several parameters
    $scope.searchForItems = function () {
        if ($scope.billMaster.SELL_TYPE_ID == undefined || $scope.billMaster.SELL_TYPE_ID == null) {
            $scope.billMaster.SELL_TYPE_ID = 0;
        }

        if ($scope.search != null && $scope.search != undefined && $scope.search != "") {
            if ($scope.search.itm != null && $scope.search.itm != undefined && $scope.search.itm != "") {

                $scope.searchBy = $scope.search.itm;
            }
            //if ($scope.searchBy == null || $scope.searchBy == undefined || $scope.searchBy == "") {
            //    $scope.searchBy = $scope.search.itm;
            //}
        }



        //if ($scope.search.itm == null || $scope.search.itm == undefined || $scope.search.itm == "") {
        if ($scope.searchBy == null || $scope.searchBy == undefined || $scope.searchBy == "") {
            return;
        }
        else {
            //search for Item 
            PointOfSaleService.searchItems($scope.searchBy).then(function (response) {
                
                $scope.ITEM_ID = response.data.ITEM_ID;

                if (($scope.bill_type_Id == 19 || $scope.bill_type_Id == 20) && $scope.billMaster.CUST_ACC_ID != null && $scope.billMaster.CUST_ACC_ID != undefined) {
                    $scope.searchItems = [];
                    for (var i = 0; i < response.data.length; i++) {
                        //if (response.data[i].COMPANY_ID == $scope.billMaster.CUST_ACC_ID) {

                        if ($scope.BillRowsNumber > 0) {
                            if ((parseFloat($scope.BillRowsNumber)) + 1 > $scope.selectedItems.length) {
                                $scope.searchItems.push(response.data[i]);
                            }
                            else {
                                swal('عفواً',
                                    'لا يمكن اضافه صفوف زياده',
                                    'error');
                            }
                        }
                        else {
                            $scope.searchItems.push(response.data[i]);
                        }
                        //}
                    }
                }
                else {


                    if ($scope.BillRowsNumber > 0) {
                        if ((parseFloat($scope.BillRowsNumber)) + 1 > $scope.selectedItems.length) {
                            $scope.searchItems = response.data;
                        }
                        else {
                            swal('عفواً',
                                'لا يمكن اضافه صفوف زياده',
                                'error');
                        }
                    }
                    else {
                        $scope.searchItems = response.data;
                    }
                }

                if ($scope.searchItems.length == 1) {

                    $scope.getItemDataUsingItemCode($scope.searchItems[0].ITEM_CODE, $scope.billMaster.SELL_TYPE_ID);
                    //$scope.getTotalQuantity();
                } else if ($scope.searchItems.length == 0) {
                    $scope.searchItems = [];
                } else {
                    $scope.addSelectedItemstoItemDetails();
                    if ($scope.modalOpenItm === false || $scope.modalOpenItm == undefined) {

                        if ($scope.BillRowsNumber > 0) {
                            if ((parseFloat($scope.BillRowsNumber)) + 1 > $scope.selectedItems.length) {
                                $scope.open();
                            }

                        }
                        else {
                            $scope.open();
                        }

                        // $scope.open();
                    }
                }
            })
        }
    }

    $scope.clearWhileClick = function () {
        //if ($scope.search.itm == null || $scope.search.itm == undefined || $scope.search.itm == "") {
        if ($scope.searchBy == null || $scope.searchBy == undefined || $scope.searchBy == "") {
            $timeout(function () {
            })

        }
    }
    //function to add selected to item details
    $scope.addSelectedItemstoItemDetails = function () {
        
        if ($scope.billMaster.SELL_TYPE_ID == undefined || $scope.billMaster.SELL_TYPE_ID == null) {
            $scope.billMaster.SELL_TYPE_ID = 0;
        }
        //if ($scope.search.itm != null || $scope.search.itm != undefined) {
        if ($scope.searchBy != null || $scope.searchBy != undefined) {
            for (var i = 0; i < $scope.searchItems.length; i++) {
                if ($scope.searchItems[i].Selected == true) {
                    $scope.getItemDataUsingItemCode($scope.searchItems[i].ITEM_CODE, $scope.billMaster.SELL_TYPE_ID);
                }

            }
            //   $scope.getTotalQuantity();
        }
    }

    $scope.clearSearchItems = function () {
        $scope.search.itm = "";
        $scope.searchBy = "";
        $scope.searchItems = [];
        $scope.itemModalShowStatus = false;
    }

    //function to get itemNameAr
    $scope.getItemNameUsingItemID = function (ItemID) {
        PointOfSaleService.getItemNameUsingItemID(ItemID).then(function (response) {
            return response.data || 'Not set';
        })
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
            if ($scope.billDetails[i].WagesDiscValue == "NaN") {
                
                x.WagesDiscValue = 0;
            }
            else {
                x.WagesDiscValue = $scope.billDetails[i].WagesDiscValue;
            }
            
            if (x.WagesDiscValue == null || x.WagesDiscValue=="NaN") {
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

            if ($scope.BillRowsNumber > 0) {
                if ((parseFloat($scope.BillRowsNumber)) + 1 > $scope.selectedItems.length) {
                    $scope.selectedItems.push(x);
                }
                else {
                    swal('عفواً',
                        'لا يمكن اضافه صفوف زياده',
                        'error');
                }
            }
            else {
                $scope.selectedItems.push(x);
            }

            // $scope.selectedItems.push(x);

        }
        $scope.getTotalQuantity();
    }

    $scope.updateItemListOnChange = function (cellName, data, index) {
        if ($scope.billSetting.OUT_MINUS != null && $scope.billSetting.OUT_MINUS != undefined && $scope.billSetting.OUT_MINUS == false) {
            if (($scope.billMaster.GOLD_ACC_ID != null && $scope.billMaster.GOLD_ACC_ID != undefined && $scope.billMaster.GOLD_ACC_ID != "")) {


                if (!($scope.selectedItems[index].ITEM_ID != null && $scope.selectedItems[index].ITEM_ID != undefined)) {
                    return;
                }
                commonService.getAccountBalanceByID($scope.billMaster.GOLD_ACC_ID).then(function (results) {



                    if (results != null && results != undefined) {
                        if (!data) {
                            data = 0;
                        }
                        // 
                        var GoldBoxBalance = results;
                        var ItemCurrentQuantity24 = GoldBoxBalance.c24;
                        var ItemCurrentQuantity22 = GoldBoxBalance.c22;
                        var ItemCurrentQuantity21 = GoldBoxBalance.c21;
                        var ItemCurrentQuantity18 = GoldBoxBalance.c18;


                        if ($scope.selectedItems[index].CaliberID == 1) {

                            if (cellName == 'Quantity') {

                                var ItemWeightQuantity = 0;
                                ItemWeightQuantity = (($scope.selectedItems[index].ActualItemWeight != null && $scope.selectedItems[index].ActualItemWeight != undefined && $scope.selectedItems[index].ActualItemWeight != '') ? $scope.selectedItems[index].ActualItemWeight : 0) * data;
                                if (ItemCurrentQuantity24 < ItemWeightQuantity) {

                                    var ItemCurrentQuantityMessage = "وزن الصنف في صندوق الذهب الحالي اقل من القيمة المطلوبة \n\n" + "الوزن المتاح في صندوق الذهب من العيار 24 هو  " + ItemCurrentQuantity24 + " الوزن المتاح \n\n";
                                    swal({
                                        text: ItemCurrentQuantityMessage,
                                        type: 'warning',
                                        showCancelButton: false,
                                        confirmButtonColor: '#3085d6',
                                        confirmButtonText: 'موافق',
                                        confirmButtonClass: 'btn btn-success btn-lg',
                                        buttonsStyling: false
                                    });


                                    data = ItemCurrentQuantity24;
                                    $scope.selectedItems[index].Quantity = data / (($scope.selectedItems[index].ActualItemWeight != null && $scope.selectedItems[index].ActualItemWeight != undefined && $scope.selectedItems[index].ActualItemWeight != '') ? $scope.selectedItems[index].ActualItemWeight : 1);
                                }
                            }


                            if (cellName == 'ActualItemWeight') {

                                var ItemWeightQuantity = 0;
                                ItemWeightQuantity = (($scope.selectedItems[index].Quantity != null && $scope.selectedItems[index].Quantity != undefined && $scope.selectedItems[index].Quantity != '') ? $scope.selectedItems[index].Quantity : 0) * data;
                                if (ItemCurrentQuantity24 < ItemWeightQuantity) {

                                    var ItemCurrentQuantityMessage = "وزن الصنف في صندوق الذهب الحالي اقل من القيمة المطلوبة \n\n" + "الوزن المتاح في صندوق الذهب من العيار 24 هو  " + ItemCurrentQuantity24 + " الوزن المتاح \n\n";
                                    swal({
                                        text: ItemCurrentQuantityMessage,
                                        type: 'warning',
                                        showCancelButton: false,
                                        confirmButtonColor: '#3085d6',
                                        confirmButtonText: 'موافق',
                                        confirmButtonClass: 'btn btn-success btn-lg',
                                        buttonsStyling: false
                                    });


                                    data = ItemCurrentQuantity24;
                                    $scope.selectedItems[index].ActualItemWeight = data / (($scope.selectedItems[index].Quantity != null && $scope.selectedItems[index].Quantity != undefined && $scope.selectedItems[index].Quantity != '') ? $scope.selectedItems[index].Quantity : 1);

                                    //   $scope.selectedItems[index].ActualItemWeight = data;
                                }
                            }


                            $scope.itemEvents(cellName, data, index);
                        }



                        if ($scope.selectedItems[index].CaliberID == 2) {

                            if (cellName == 'Quantity') {

                                var ItemWeightQuantity = 0;
                                ItemWeightQuantity = (($scope.selectedItems[index].ActualItemWeight != null && $scope.selectedItems[index].ActualItemWeight != undefined && $scope.selectedItems[index].ActualItemWeight != '') ? $scope.selectedItems[index].ActualItemWeight : 0) * data;
                                if (ItemCurrentQuantity22 < ItemWeightQuantity) {

                                    var ItemCurrentQuantityMessage = "وزن الصنف في صندوق الذهب الحالي اقل من القيمة المطلوبة \n\n" + "الوزن المتاح في صندوق الذهب من العيار 22 هو  " + ItemCurrentQuantity22 + " الوزن المتاح \n\n";
                                    swal({
                                        text: ItemCurrentQuantityMessage,
                                        type: 'warning',
                                        showCancelButton: false,
                                        confirmButtonColor: '#3085d6',
                                        confirmButtonText: 'موافق',
                                        confirmButtonClass: 'btn btn-success btn-lg',
                                        buttonsStyling: false
                                    });


                                    data = ItemCurrentQuantity22;
                                    $scope.selectedItems[index].Quantity = data / (($scope.selectedItems[index].ActualItemWeight != null && $scope.selectedItems[index].ActualItemWeight != undefined && $scope.selectedItems[index].ActualItemWeight != '') ? $scope.selectedItems[index].ActualItemWeight : 1);
                                }
                            }



                            if (cellName == 'ActualItemWeight') {

                                var ItemWeightQuantity = 0;
                                ItemWeightQuantity = (($scope.selectedItems[index].Quantity != null && $scope.selectedItems[index].Quantity != undefined && $scope.selectedItems[index].Quantity != '') ? $scope.selectedItems[index].Quantity : 0) * data;
                                if (ItemCurrentQuantity22 < ItemWeightQuantity) {

                                    var ItemCurrentQuantityMessage = "وزن الصنف في صندوق الذهب الحالي اقل من القيمة المطلوبة \n\n" + "الوزن المتاح في صندوق الذهب من العيار 22 هو  " + ItemCurrentQuantity22 + " الوزن المتاح \n\n";
                                    swal({
                                        text: ItemCurrentQuantityMessage,
                                        type: 'warning',
                                        showCancelButton: false,
                                        confirmButtonColor: '#3085d6',
                                        confirmButtonText: 'موافق',
                                        confirmButtonClass: 'btn btn-success btn-lg',
                                        buttonsStyling: false
                                    });


                                    data = ItemCurrentQuantity22;

                                    $scope.selectedItems[index].ActualItemWeight = data / (($scope.selectedItems[index].Quantity != null && $scope.selectedItems[index].Quantity != undefined && $scope.selectedItems[index].Quantity != '') ? $scope.selectedItems[index].Quantity : 1);


                                }
                            }


                            $scope.itemEvents(cellName, data, index);

                        }
                        if ($scope.selectedItems[index].CaliberID == 3) {



                            if (cellName == 'Quantity') {

                                var ItemWeightQuantity = 0;
                                ItemWeightQuantity = (($scope.selectedItems[index].ActualItemWeight != null && $scope.selectedItems[index].ActualItemWeight != undefined && $scope.selectedItems[index].ActualItemWeight != '') ? $scope.selectedItems[index].ActualItemWeight : 0) * data;
                                if (ItemCurrentQuantity21 < ItemWeightQuantity) {

                                    var ItemCurrentQuantityMessage = "وزن الصنف في صندوق الذهب الحالي اقل من القيمة المطلوبة \n\n" + "الوزن المتاح في صندوق الذهب من العيار 21 هو  " + ItemCurrentQuantity21 + " الوزن المتاح \n\n";
                                    swal({
                                        text: ItemCurrentQuantityMessage,
                                        type: 'warning',
                                        showCancelButton: false,
                                        confirmButtonColor: '#3085d6',
                                        confirmButtonText: 'موافق',
                                        confirmButtonClass: 'btn btn-success btn-lg',
                                        buttonsStyling: false
                                    });


                                    data = ItemCurrentQuantity21;

                                    $scope.selectedItems[index].Quantity = data / (($scope.selectedItems[index].ActualItemWeight != null && $scope.selectedItems[index].ActualItemWeight != undefined && $scope.selectedItems[index].ActualItemWeight != '') ? $scope.selectedItems[index].ActualItemWeight : 1);


                                }
                            }



                            if (cellName == 'ActualItemWeight') {

                                var ItemWeightQuantity = 0;
                                ItemWeightQuantity = (($scope.selectedItems[index].Quantity != null && $scope.selectedItems[index].Quantity != undefined && $scope.selectedItems[index].Quantity != '') ? $scope.selectedItems[index].Quantity : 0) * data;
                                if (ItemCurrentQuantity21 < ItemWeightQuantity) {

                                    var ItemCurrentQuantityMessage = "وزن الصنف في صندوق الذهب الحالي اقل من القيمة المطلوبة \n\n" + "الوزن المتاح في صندوق الذهب من العيار 21 هو  " + ItemCurrentQuantity21 + " الوزن المتاح \n\n";
                                    swal({
                                        text: ItemCurrentQuantityMessage,
                                        type: 'warning',
                                        showCancelButton: false,
                                        confirmButtonColor: '#3085d6',
                                        confirmButtonText: 'موافق',
                                        confirmButtonClass: 'btn btn-success btn-lg',
                                        buttonsStyling: false
                                    });


                                    data = ItemCurrentQuantity21;
                                    $scope.selectedItems[index].ActualItemWeight = data / (($scope.selectedItems[index].Quantity != null && $scope.selectedItems[index].Quantity != undefined && $scope.selectedItems[index].Quantity != '') ? $scope.selectedItems[index].Quantity : 1);



                                }
                            }


                            $scope.itemEvents(cellName, data, index);


                        }
                        if ($scope.selectedItems[index].CaliberID == 4) {




                            if (cellName == 'Quantity') {

                                var ItemWeightQuantity = 0;
                                ItemWeightQuantity = (($scope.selectedItems[index].ActualItemWeight != null && $scope.selectedItems[index].ActualItemWeight != undefined && $scope.selectedItems[index].ActualItemWeight != '') ? $scope.selectedItems[index].ActualItemWeight : 0) * data;
                                if (ItemCurrentQuantity18 < ItemWeightQuantity) {

                                    var ItemCurrentQuantityMessage = "وزن الصنف في صندوق الذهب الحالي اقل من القيمة المطلوبة \n\n" + "الوزن المتاح في صندوق الذهب من العيار 18 هو  " + ItemCurrentQuantity21 + " الوزن المتاح \n\n";
                                    swal({
                                        text: ItemCurrentQuantityMessage,
                                        type: 'warning',
                                        showCancelButton: false,
                                        confirmButtonColor: '#3085d6',
                                        confirmButtonText: 'موافق',
                                        confirmButtonClass: 'btn btn-success btn-lg',
                                        buttonsStyling: false
                                    });


                                    data = ItemCurrentQuantity18;
                                    $scope.selectedItems[index].Quantity = data / (($scope.selectedItems[index].ActualItemWeight != null && $scope.selectedItems[index].ActualItemWeight != undefined && $scope.selectedItems[index].ActualItemWeight != '') ? $scope.selectedItems[index].ActualItemWeight : 1);

                                }
                            }



                            if (cellName == 'ActualItemWeight') {

                                var ItemWeightQuantity = 0;
                                ItemWeightQuantity = (($scope.selectedItems[index].Quantity != null && $scope.selectedItems[index].Quantity != undefined && $scope.selectedItems[index].Quantity != '') ? $scope.selectedItems[index].Quantity : 0) * data;
                                if (ItemCurrentQuantity18 < ItemWeightQuantity) {

                                    var ItemCurrentQuantityMessage = "وزن الصنف في صندوق الذهب الحالي اقل من القيمة المطلوبة \n\n" + "الوزن المتاح في صندوق الذهب من العيار 18 هو  " + ItemCurrentQuantity18 + " الوزن المتاح \n\n";
                                    swal({
                                        text: ItemCurrentQuantityMessage,
                                        type: 'warning',
                                        showCancelButton: false,
                                        confirmButtonColor: '#3085d6',
                                        confirmButtonText: 'موافق',
                                        confirmButtonClass: 'btn btn-success btn-lg',
                                        buttonsStyling: false
                                    });


                                    data = ItemCurrentQuantity18;

                                    $scope.selectedItems[index].ActualItemWeight = data / (($scope.selectedItems[index].Quantity != null && $scope.selectedItems[index].Quantity != undefined && $scope.selectedItems[index].Quantity != '') ? $scope.selectedItems[index].Quantity : 1);


                                }
                            }

                            $scope.itemEvents(cellName, data, index);
                        }

                    }

                }, function (error) {

                    var y = results;

                });


            }
            else {


                swal({
                    text: "برجاء اختيار صندوق الذهب حيث ان نمط الفاتورة لا يسمح بالاخراج ب السالب ",
                    type: 'warning',
                    showCancelButton: false,
                    confirmButtonColor: '#3085d6',
                    confirmButtonText: 'موافق',
                    confirmButtonClass: 'btn btn-success btn-lg',
                    buttonsStyling: false
                });
                $scope.itemEvents(cellName, data, index);

            }



            //////////////////////////////  For The Item Store Balance //////////////////////////





            //if ($scope.billSetting.BILL_EFF_ID != null && $scope.billSetting.BILL_EFF_ID != undefined && $scope.billSetting.BILL_EFF_ID == 2) {

            //    var storeID = 1;

            //    if (($scope.billMaster.COM_STORE_ID != null && $scope.billMaster.COM_STORE_ID != undefined && $scope.billMaster.COM_STORE_ID != "")) {
            //        storeID = $scope.billMaster.COM_STORE_ID;

            //    }
            //    else {
            //        storeID = 1;
            //    }


            //    if (!($scope.selectedItems[index].ITEM_ID != null && $scope.selectedItems[index].ITEM_ID != undefined)) {
            //        return;
            //    }

            //    PointOfSaleService.getItemCurrentQuantity($scope.selectedItems[index].ITEM_ID, storeID).then(function (response) {
            //        if (response != null && response != undefined && response.data != null && response.data != undefined) {
            //            if (!data) {
            //                data = 0;
            //            }
            //            
            //            var ItemCurrentQuantity = response.data;
            //            if (ItemCurrentQuantity.ItemCurrentQuantityInTheCurrentStore < data) {

            //                var ItemCurrentQuantityMessage = "وزن الصنف في المخزن الحالي اقل من القيمة المطلوبة \n\n" + "الوزن المتاح في المخزن هي " + ItemCurrentQuantity.ItemCurrentQuantityInTheCurrentStore + " الوزن المتاح \n\n" + ItemCurrentQuantity.ItemWeightQuantity;
            //                swal({
            //                    text: ItemCurrentQuantityMessage,
            //                    type: 'warning',
            //                    showCancelButton: false,
            //                    confirmButtonColor: '#3085d6',
            //                    confirmButtonText: 'موافق',
            //                    confirmButtonClass: 'btn btn-success btn-lg',
            //                    buttonsStyling: false
            //                });


            //                data = ItemCurrentQuantity.ItemCurrentQuantityInTheCurrentStore;
            //                $scope.selectedItems[index].ItemWeight = data;
            //            }

            //            $scope.itemEvents(cellName, data, index);

            //        }
            //    });

            //    //else {

            //    //    swal({
            //    //        text: "برجاء اختيار المحزن حيث ان نمط الفاتورة لا يسمح بالاخراج ب السالب ",
            //    //        type: 'warning',
            //    //        showCancelButton: false,
            //    //        confirmButtonColor: '#3085d6',
            //    //        confirmButtonText: 'موافق',
            //    //        confirmButtonClass: 'btn btn-success btn-lg',
            //    //        buttonsStyling: false
            //    //    });

            //    //    $scope.selectedItems[index].Quantity = 0;
            //    //}
            //}
            //else {
            //    $scope.itemEvents(cellName, data, index);
            //}


            //////////////////////////// End Of the Item Store Balance /////////////////////////

        }



        else {


            $scope.itemEvents(cellName, data, index);

        }

    }


    $scope.updateItemCalibersOnChange = function (cellName, data, index) {
        if (cellName == 'Caliber24') {
            //  
            if (data == null || data == undefined) {
                data = 0;
            }
            else {
                if (data < 0) {
                    data = 0;
                }
                else {
                    $scope.selectedItems[index].Caliber24 = data;
                }
            }
            if ($scope.selectedItems[index].Caliber24 < 0) {
                $scope.selectedItems[index].Caliber24 = 0;
            }
            $scope.calculateTotals();
        }

        else if (cellName == 'Caliber22') {
            if (data == null || data == undefined) {
                data = 0;
            }
            else {
                if (data < 0) {
                    data = 0;
                }
                else {
                    $scope.selectedItems[index].Caliber22 = data;
                }
            }
            if ($scope.selectedItems[index].Caliber22 < 0) {
                $scope.selectedItems[index].Caliber22 = 0;
            }
            $scope.calculateTotals();
        }


        else if (cellName == 'Caliber21') {
            if (data == null || data == undefined) {
                data = 0;
            }
            else {
                if (data < 0) {
                    data = 0;
                }
                else {
                    $scope.selectedItems[index].Caliber21 = data;
                }
            }
            if ($scope.selectedItems[index].Caliber21 < 0) {
                $scope.selectedItems[index].Caliber21 = 0;
            }
            $scope.calculateTotals();
        }

        else if (cellName == 'Caliber18') {
            if (data == null || data == undefined) {
                data = 0;
            }
            else {
                if (data < 0) {
                    data = 0;
                }
                else {
                    $scope.selectedItems[index].Caliber18 = data;
                }
            }
            if ($scope.selectedItems[index].Caliber18 < 0) {
                $scope.selectedItems[index].Caliber18 = 0;
            }
            $scope.calculateTotals();
        }

    }


    $scope.dirEnity = function (entity) {
        $scope.isTotal24QuantityZero = false;
        $scope.isTotal22QuantityZero = false;
        $scope.isTotal21QuantityZero = false;
        $scope.isTotal18QuantityZero = false;


        $scope.selectedItems = [];
        entity.BILL_DATE = new Date(entity.BILL_DATE);
        entity.BILL_NUMBER = Number(entity.BILL_NUMBER);
        entity.BILL_PAY_WAY = Number(entity.BILL_PAY_WAY);
        entity.SHIFT_NUMBER = Number(entity.SHIFT_NUMBER);
        entity.BILL_ID = Number(entity.BILL_ID);
        entity.BILL_TOTAL_DISC = Number(entity.BILL_TOTAL_DISC);
        entity.BILL_TOTAL_EXTRA = Number(entity.BILL_TOTAL_EXTRA);
        entity.ENTRY_ID = entity.ENTRY_ID;

        //angular bug
        ////$scope.billMaster.BILL_ID = entity.BILL_ID;

        $scope.getAllBillDetails(entity.BILL_ID);
        $scope.billID = entity.BILL_ID;
        $scope.billMaster = entity;
        $scope.operation = "Update";

        $scope.isEditReason = false;
        if ($scope.IsShowEditReason != undefined && $scope.IsShowEditReason != null) {
            $scope.isEditReason = $scope.IsShowEditReason;
        }

        $scope.getEntryNumber();
        // $scope.callAccountObject();

        if ($scope.isAutoPosted == true) {
            $scope.autoTransfer = true;
        }
        else if ($scope.isAutoPosted == false && entity.BILL_IS_POST == false) {
            $scope.autoTransfer = false;
        }

        else {
            $scope.autoTransfer = true;
        }

        $scope.billMaster.totalMustPaid = Number(entity.BILL_TOTAL);
        $scope.billMaster.TotalPaid = Number(entity.TotalPaid);

        $scope.getTransactionsByID(entity.BILL_ID);
        // $scope.matrixTotals();
        $scope.getGoldTotalsByBillID(entity.BILL_ID);
        $scope.billMaster.TotalVatRate = entity.TotalVatRate;
        $scope.billMaster.TotalVatValue = entity.TotalVatValue;

        $scope.billMaster.TotalWagesDiscValue = entity.TotalWagesDiscValue;
        $scope.billMaster.TotalWagesDiscRate = entity.TotalWagesDiscRate;

        $scope.ENTRY_NUMBER = entity.ENTRY_NUMBER;

        $scope.getPayTypesByID(entity.BILL_ID);

        //Get report data
        $scope.GetReportData();
    }

    $scope.deleteEnity = function (entity) {
        swal({
            title: 'هل تريد حذف نقطة المبيع؟',
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
                    $scope.deleteBillPayTypes(entity.BILL_ID);
                    $scope.deleteCaliberTrans(entity.BILL_ID);
                    $scope.deleteEntryOfBill(entity.BILL_ID);
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

        }
        else {
            $scope.class = "collapse";
        }
    }

    $scope.GetBillSetting = function () {
        
        var settingID = $scope.getBillTypeQueryString();
        var userID = authService.GetUserID();

        var userName = authService.GetUserName();
        billSettingService.getByID(settingID).then(function (result) {

            console.log($scope.billSetting)
            var billSetting = result.data;
            $scope.billSetting = billSetting;
            console.log($scope.billSetting);
            $scope.billMaster.SELL_TYPE_ID = billSetting.BILL_SELL_TYPE_ID;

            $scope.billMaster.BILL_PAY_WAY = billSetting.BILL_PAY_TYPE;

            if (billSetting.AccGoldID != undefined && billSetting.AccGoldID != null) {
                $scope.billMaster.GOLD_ACC_ID = billSetting.AccGoldID;
                $scope.changeGoldBalance($scope.billMaster.GOLD_ACC_ID);
            }
            //  
            $scope.typeOfBill = billSetting.BILL_TYPE_ID;

            $scope.billMaster.ITEM_ACC_ID = billSetting.ACC_ITEM_ID;
            $scope.billMaster.EMP_ID = userName;
            $scope.billMaster.CURRENCY_ID = billSetting.CURRENCY_ID;
            $scope.billMaster.CURRENCY_RATE = billSetting.CURRENCY_RATE;
            $scope.bill_type_Id = billSetting.BILL_TYPE_ID;
            $scope.acc_wages_Id = billSetting.AccWagesID;
            $scope.acc_wages_taxId = billSetting.AccWagesTaxID;
            $scope.wages_taxValue = billSetting.WagesTaxValue;
            $scope.acc_purchas_Id = billSetting.PurchasAccID;
            $scope.acc_purchas_taxId = billSetting.PurchasTaxAccID;
            $scope.acc_discountVal = billSetting.ACC_DISC_ID;
            $scope.acc_sales_Id = billSetting.AccSalesID;
            $scope.acc_vat_rateId = billSetting.AccVatRateID;



            if (settingID == 11 || settingID == 12) {
                $scope.billMaster.COM_STORE_ID = billSetting.TO_COM_STORE_ID;
            } else {
                $scope.billMaster.COM_STORE_ID = billSetting.COM_STORE_ID
            }


            if ($scope.bill_type_Id == 19 || $scope.bill_type_Id == 20) {
                $scope.totalName = "الاجمالي";
            }


            $scope.billSettingID = settingID;
            $scope.settingsAccounts = billSetting;
            $scope.checkBillSettingsGrid(settingID);
            $scope.checkBillSettings(billSetting);
            $scope.GetAllBoxAccounts(billSetting);
            $scope.GetAllCustomersAccounts(billSetting);
            $scope.GetAllGoldBoxAccounts(billSetting);
            $scope.getAccountList(billSetting);
            $scope.getAllTpayTypes(billSetting);

            $scope.getGridColData(settingID);
            $scope.checkUserModule();
        });
    };

    $scope.getAllOnLoad = function () {



        $q.all(
            [
                $scope.sticky(),
                $scope.getCurrentDate(),
                $scope.GetBillSetting(),
                $scope.getUserType(),
                $scope.getPointOfSaleTotalCount(),
                $scope.getAllMainItemGroupsList(),

                $scope.getAllSellsTypes(),
                $scope.getAllCompanyStores(),

                $scope.GetAllEmployeesSales(),
                $scope.addGridBillDetailsItem(),
                $scope.getBillNumber(),
                $scope.getAllCurrencies(),

                $scope.getMatrixList(),
                $scope.getBillPayTypesList(),
                $scope.getCostCentersList(),
                $scope.getMainCostCenters(),
                $scope.getPagingNums(),
                $scope.getClearnessRate(),
                $scope.getPointOfSaleList(),
                // $scope.select2EventEnter()
                //$scope.GetReportData(),
            ])
            .then(function (allResponses) {
                //if all the requests succeeded, this will be called, and $q.all will get an
                //array of all their responses.
                //  console.log(allResponses[0].data);
                var urlParams = $location.search();
                if (urlParams.foo != null && urlParams.foo != undefined && urlParams.foo != "") {
                    PointOfSaleService.getByBillID(parseInt(urlParams.foo)).then(function (results) {
                        // alert(results.data.BILL_NUMBER);
                        $scope.billMaster = results.data;
                        $scope.dirEnity(results.data);
                    })
                    //$.each($scope.billMasters, function (index, value) {
                    //    if (value.BILL_ID == parseInt(urlParams.foo)) {
                    //        // alert();
                    //        $scope.billMaster = value;
                    //        $scope.dirEnity(value);

                    //    }
                    //});

                }

            }, function (error) {
                //This will be called if $q.all finds any of the requests erroring.
                var abc = error;
                var def = 99;
            });
    }
    $scope.getPointOfSaleTotalCount = function () {

        $scope.count().then(function (results) {

            var data = results.data;
            $scope.PointOfSaleTotalCount = data;
            $scope.pagesCount = $scope.getPagesCount();
        }, function (error) {
            console.log(error.data.message);
        });
    }
    $scope.getPointOfSaleList = function () {
        var billType = $scope.getBillTypeQueryString();
        var branchId = 0;
        if (localStorageService.get('branchId') != null) {
            branchId = localStorageService.get('branchId').branchId;
        }
        $scope.get(branchId, billType, $scope.pageNum, $scope.pageSize).then(function (results) {

            var data = results.data;
            $scope.billMasters = data;



        }, function (error) {
            console.log(error.data.message);
        });
    }

    $scope.get = function (branchId, billType, pageNum, pageSize) {
        return PointOfSaleService.get(branchId, billType, pageNum, pageSize);
    }
    $scope.count = function () {
        var billType = $scope.getBillTypeQueryString();
        return PointOfSaleService.count(billType);
    }
    $scope.add = function (entity) {
        return PointOfSaleService.add(entity);
    }
    $scope.addDetails = function (Details) {
        return PointOfSaleService.addBillDetails(Details);
    }
    $scope.update = function (entity) {
        return PointOfSaleService.update(entity);
    }
    $scope.delete = function (entity) {
        return PointOfSaleService.delete(entity);
    }
    $scope.PointOfSalePageLoad = function () {

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
            templateUrl: 'SearchItemsPopUp.html',
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

    $scope.getAllCurrencies = function () {

        PointOfSaleService.getAllCurrencies().then(function (response) {

            var result = response.data;
            $scope.currenciesList = result;
        })
    }
    $scope.changeCurrencyRate = function () {
        var currencyID = $scope.billMaster.CURRENCY_ID;
        if ($scope.currenciesList != undefined || $scope.currenciesList != null) {
            for (var i = 0; i < $scope.currenciesList.length; i++) {
                if ($scope.currenciesList[i].CURRENCY_ID == currencyID) {
                    $scope.billMaster.CURRENCY_RATE = $scope.currenciesList[i].CURRENCY_RATE;
                    $scope.billMaster.currencyName = $scope.currenciesList[i].CURRENCY_AR_NAME;
                }
            }
        }
    }

    $scope.getCurrencyName = function () {
        var currencyID = $scope.billMaster.CURRENCY_ID;
        if ($scope.currenciesList != undefined || $scope.currenciesList != null) {
            for (var i = 0; i < $scope.currenciesList.length; i++) {
                if ($scope.currenciesList[i].CURRENCY_ID == currencyID) {

                    $scope.billMaster.currencyName = $scope.currenciesList[i].CURRENCY_AR_NAME;
                    return $scope.billMaster.currencyName;
                }
            }
        }
    }


    $scope.getBillTypeQueryString = function () {

        $scope.billType = ($location.search()).billType;
        return $scope.billType;
    }

    $scope.getbillTypeName = function () {
        var billType = $scope.getBillTypeQueryString();
        if (billType == 1) {
            return 'مشتريات';
        } else if (billType == 2) {
            return 'مبيعات';
        } else if (billType == 3) {
            return 'مرتجع مشتريات';
        } else if (billType == 4) {
            return 'مرتجع مبيعات';
        } else if (billType == 5) {
            return 'إذن إستلام بضاعة';
        } else if (billType == 6) {
            return 'إذن صرف بضاعة';
        } else if (billType == 7) {
            return 'طلبيات';
        } else if (billType == 8) {
            return 'عروض اسعار';
        } else if (billType == 9) {
            return 'رصيد أول المدة';
        } else if (billType == 10) {
            return 'مخزون اخر المدة';
        } else if (billType == 11) {
            return 'تحويلات مخازن';
        } else if (billType == 12) {
            return 'تحويلات مخازن بسندات';
        }
    }


    $scope.getCurrentDate = function () {
        var date = new Date();
        // var day_eid_adha = date.toHijri();
        $scope.billMaster.BILL_DATE = date;
    }


    $scope.openItemsPopup = function () {

        var parentElem = angular.element($document[0].querySelector('.app-content'));
        $scope.acccountModalOpenItm = false;

        var temp = "<div  ng-include src=\"'../../ngApp/items/views/items.html'\"></div>";

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
        });
    }


    ////////////////////////////// Open Bill Entry ////////////////////////////////
    $scope.openEntryPopup = function () {
        
        var parentElem = angular.element($document[0].querySelector('.app-content'));
        $scope.EntryModalOpenItm = false;
        $scope.FromPopUpentryType = 130;

        $scope.entrySettingId = 130;
        $scope.popUpData = { entrySettingId: $scope.entrySettingId, billID: $scope.billMaster.BILL_ID };

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
            $scope.EntryModalOpenItm = true;
        });

        // we want to update state whether the modal closed or was dismissed,
        // so use finally to handle both resolved and rejected promises.
        modalInstance.result.finally(function (selectedItem) {
            $scope.EntryModalOpenItm = false;
        });
    }

    ///////////////////////////// End Of Bill Entry //////////////////////////////
    ////////////////////////////////////////////////////////serach by code and name 
    $scope.searchCodeName = function (code) {

        if (code != null && code != undefined) {

            $scope.searchBy = code;
            $scope.searchForItems();
        }

    }

    ///////////////////matrix 

    $scope.getMatrixList = function () {
        if ($scope.matrixList.length == 0) {
            $scope.matrixObj1 = { TransQuant: "الاجمالى بعد التحويلات", Col18: "الي عيار 18", Col21: " الي عيار 21", Col22: " الي عيار 22", Col24: " الي عيار 24", ColQuantity: "الكمية", ColName: "تحويلات العيار", read18: true, read21: true, read22: true, read24: true, readQ: true };
            $scope.disable = false;
            $scope.matrixList.push($scope.matrixObj1);

            $scope.matrixObj2 = { TransQuant: 0, Col18: 0, Col21: 0, Col22: 0, Col24: 0, ColQuantity: 0, ColName: "من عيار 24", read18: false, read21: false, read22: false, read24: true, readQ: false }
            $scope.disable = true;
            $scope.matrixList.push($scope.matrixObj2);

            $scope.matrixObj3 = { TransQuant: 0, Col18: 0, Col21: 0, Col22: 0, Col24: 0, ColQuantity: 0, ColName: "من عيار 22", read18: false, read21: false, read22: true, read24: false, readQ: false }
            $scope.disable = false;
            $scope.matrixList.push($scope.matrixObj3);

            $scope.matrixObj4 = { TransQuant: 0, Col18: 0, Col21: 0, Col22: 0, Col24: 0, ColQuantity: 0, ColName: "من عيار 21", read18: false, read21: true, read22: false, read24: false, readQ: false }
            $scope.disable = true;
            $scope.matrixList.push($scope.matrixObj4);

            $scope.matrixObj5 = { TransQuant: 0, Col18: 0, Col21: 0, Col22: 0, Col24: 0, ColQuantity: 0, ColName: "من عيار 18", read18: true, read21: false, read22: false, read24: false, readQ: false }
            $scope.disable = false;
            $scope.matrixList.push($scope.matrixObj5);

        }
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
                    $scope.matrixList[1].Col24 = '';
                }

                else if ($scope.selectedItems[i].ARName == "عيار 22") {
                    $scope.total22 = ((parseFloat($scope.Quantity * $scope.selectedItems[i].ItemWeight)) + parseFloat($scope.total22)).toFixed(2);
                    $scope.matrixList[2].Col22 = '';
                }

                else if ($scope.selectedItems[i].ARName == "عيار 21") {
                    $scope.total21 = ((parseFloat($scope.Quantity * $scope.selectedItems[i].ItemWeight)) + parseFloat($scope.total21)).toFixed(2);
                    $scope.matrixList[3].Col21 = '';
                }

                else if ($scope.selectedItems[i].ARName == "عيار 18") {
                    $scope.total18 = ((parseFloat($scope.Quantity * $scope.selectedItems[i].ItemWeight)) + parseFloat($scope.total18)).toFixed(2);
                    $scope.matrixList[4].Col18 = '';
                }
            }
        }


        $scope.matrixList[1].ColQuantity = $scope.total24;
        $scope.matrixList[2].ColQuantity = $scope.total22;
        $scope.matrixList[3].ColQuantity = $scope.total21;
        $scope.matrixList[4].ColQuantity = $scope.total18;
        $scope.recalculateTransQuant();
    }


    ///////////////////////////get trans Quantity when update
    $scope.recalculateTransQuant = function () {
        //  if ($scope.operation != "Update") {
        $scope.tr24 = 0;
        $scope.tr22 = 0;
        $scope.tr21 = 0;
        $scope.tr18 = 0;

        $scope.transRow24 = 0;
        $scope.transRow22 = 0;
        $scope.transRow21 = 0;
        $scope.transRow18 = 0;

        $scope.transCol24 = 0;
        $scope.transCol22 = 0;
        $scope.transCol21 = 0;
        $scope.transCol18 = 0;

        $scope.transRow24 = (parseFloat($scope.matrixList[1].Col22) + parseFloat($scope.matrixList[1].Col21) + parseFloat($scope.matrixList[1].Col18)).toFixed(2);
        $scope.transCol24 = ((parseFloat($scope.matrixList[2].Col24) * parseFloat(22 / 24)) + (parseFloat($scope.matrixList[3].Col24) * parseFloat(21 / 24)) + (parseFloat($scope.matrixList[4].Col24) * parseFloat(18 / 24))).toFixed(2);
        if ($scope.transRow24 == 0 && $scope.transCol24 == 0) {
            $scope.matrixList[1].TransQuant = 0;
        }
        else {
            if ((parseFloat($scope.matrixList[1].ColQuantity) - parseFloat($scope.transRow24) + parseFloat($scope.transCol24)) < 0) {

                swal({
                    text: "قيمه التحويل اقل من الصفر ",
                    type: 'warning',
                    showCancelButton: false,
                    confirmButtonColor: '#3085d6',
                    confirmButtonText: 'نعم',
                    confirmButtonClass: 'btn btn-success btn-lg',
                    buttonsStyling: false
                });
                $scope.matrixList[1].TransQuant = 0;
                $scope.matrixList[1].Col22 = 0;
                $scope.matrixList[1].Col21 = 0;
                $scope.matrixList[1].Col18 = 0;
            }
            else {
                $scope.matrixList[1].TransQuant = (parseFloat($scope.matrixList[1].ColQuantity) - parseFloat($scope.transRow24) + parseFloat($scope.transCol24)).toFixed(2);

            }
        }


        $scope.transRow22 = (parseFloat($scope.matrixList[2].Col24) + parseFloat($scope.matrixList[2].Col21) + parseFloat($scope.matrixList[2].Col18)).toFixed(2);
        $scope.transCol22 = ((parseFloat($scope.matrixList[1].Col22) * parseFloat(24 / 22)) + (parseFloat($scope.matrixList[3].Col22) * parseFloat(21 / 22)) + (parseFloat($scope.matrixList[4].Col22) * parseFloat(18 / 24))).toFixed(2);
        if ($scope.transRow22 == 0 && $scope.transCol22 == 0) {
            $scope.matrixList[2].TransQuant = 0;
        }
        else {

            if ((parseFloat($scope.matrixList[2].ColQuantity) - parseFloat($scope.transRow22) + parseFloat($scope.transCol22)) < 0) {

                swal({
                    text: "قيمه التحويل اقل من الصفر ",
                    type: 'warning',
                    showCancelButton: false,
                    confirmButtonColor: '#3085d6',
                    confirmButtonText: 'نعم',
                    confirmButtonClass: 'btn btn-success btn-lg',
                    buttonsStyling: false
                });
                $scope.matrixList[2].TransQuant = 0;
                $scope.matrixList[2].Col24 = 0;
                $scope.matrixList[2].Col21 = 0;
                $scope.matrixList[2].Col18 = 0;
            }
            else {


                $scope.matrixList[2].TransQuant = (parseFloat($scope.matrixList[2].ColQuantity) - parseFloat($scope.transRow22) + parseFloat($scope.transCol22)).toFixed(2);

            }
        }


        $scope.transRow21 = (parseFloat($scope.matrixList[3].Col24) + parseFloat($scope.matrixList[3].Col22) + parseFloat($scope.matrixList[3].Col18)).toFixed(2);
        $scope.transCol21 = ((parseFloat($scope.matrixList[1].Col21) * parseFloat(24 / 21)) + (parseFloat($scope.matrixList[2].Col21) * parseFloat(22 / 21)) + (parseFloat($scope.matrixList[4].Col21) * parseFloat(18 / 21))).toFixed(2);
        if ($scope.transRow21 == 0 && $scope.transCol21 == 0) {
            $scope.matrixList[3].TransQuant = 0;
        }
        else {
            if ((parseFloat($scope.matrixList[3].ColQuantity) - parseFloat($scope.transRow21) + parseFloat($scope.transCol21)) < 0) {

                swal({
                    text: "قيمه التحويل اقل من الصفر ",
                    type: 'warning',
                    showCancelButton: false,
                    confirmButtonColor: '#3085d6',
                    confirmButtonText: 'نعم',
                    confirmButtonClass: 'btn btn-success btn-lg',
                    buttonsStyling: false
                });
                $scope.matrixList[3].TransQuant = 0;
                $scope.matrixList[3].Col24 = 0;
                $scope.matrixList[3].Col22 = 0;
                $scope.matrixList[3].Col18 = 0;
            }
            else {

                $scope.matrixList[3].TransQuant = (parseFloat($scope.matrixList[3].ColQuantity) - parseFloat($scope.transRow21) + parseFloat($scope.transCol21)).toFixed(2);

            }
        }


        $scope.transRow18 = (parseFloat($scope.matrixList[4].Col24) + parseFloat($scope.matrixList[4].Col22) + parseFloat($scope.matrixList[4].Col21)).toFixed(2);
        $scope.transCol18 = ((parseFloat($scope.matrixList[1].Col18) * parseFloat(24 / 18)) + (parseFloat($scope.matrixList[2].Col18) * parseFloat(22 / 18)) + (parseFloat($scope.matrixList[3].Col18) * parseFloat(21 / 18))).toFixed(2);
        if ($scope.transRow18 == 0 && $scope.transCol18 == 0) {
            $scope.matrixList[4].TransQuant = 0;
        }
        else {
            if ((parseFloat($scope.matrixList[4].ColQuantity) - parseFloat($scope.transRow18) + parseFloat($scope.transCol18)) < 0) {

                swal({
                    text: "قيمه التحويل اقل من الصفر ",
                    type: 'warning',
                    showCancelButton: false,
                    confirmButtonColor: '#3085d6',
                    confirmButtonText: 'نعم',
                    confirmButtonClass: 'btn btn-success btn-lg',
                    buttonsStyling: false
                });
                $scope.matrixList[4].TransQuant = 0;
                $scope.matrixList[4].Col24 = 0;
                $scope.matrixList[4].Col22 = 0;
                $scope.matrixList[4].Col21 = 0;
            }
            else {
                $scope.matrixList[4].TransQuant = (parseFloat($scope.matrixList[4].ColQuantity) - parseFloat($scope.transRow18) + parseFloat($scope.transCol18)).toFixed(2);

            }
        }



        if ($scope.transRow24 == 0 && $scope.transCol24 == 0 && $scope.matrixList[1].ColQuantity != 0) {
            $scope.matrixList[1].TransQuant = $scope.matrixList[1].ColQuantity;
        }

        if ($scope.transRow22 == 0 && $scope.transCol22 == 0 && $scope.matrixList[2].ColQuantity != 0) {
            $scope.matrixList[2].TransQuant = $scope.matrixList[2].ColQuantity;
        }

        if ($scope.transRow21 == 0 && $scope.transCol21 == 0 && $scope.matrixList[3].ColQuantity != 0) {
            $scope.matrixList[3].TransQuant = $scope.matrixList[3].ColQuantity;
        }
        if ($scope.transRow18 == 0 && $scope.transCol18 == 0 && $scope.matrixList[4].ColQuantity != 0) {
            $scope.matrixList[4].TransQuant = $scope.matrixList[4].ColQuantity;
        }

    }
    //////////////////change quantity of trans//////////////
    $scope.changeTransQunatity = function (cellName, data, index) {
        if (index == 1) {

            if (cellName == "18") {

                if (data > parseFloat($scope.matrixList[1].ColQuantity)) {

                    $("input[name='Col18']").eq(1).val(0);

                    swal({
                        text: "غير مسموح بقيمه اقل من صفر ",
                        type: 'warning',
                        showCancelButton: false,
                        confirmButtonColor: '#3085d6',
                        confirmButtonText: 'نعم',
                        confirmButtonClass: 'btn btn-success btn-lg',
                        buttonsStyling: false
                    });

                }
                else {
                    $scope.matrixList[1].Col18 = data;

                }
            }

            else if (cellName == "21") {

                if (data > parseFloat($scope.matrixList[1].ColQuantity)) {

                    $("input[name='Col21']").eq(1).val(0);

                    swal({
                        text: "غير مسموح بقيمه اقل من صفر ",
                        type: 'warning',
                        showCancelButton: false,
                        confirmButtonColor: '#3085d6',
                        confirmButtonText: 'نعم',
                        confirmButtonClass: 'btn btn-success btn-lg',
                        buttonsStyling: false
                    });
                    $scope.matrixList[1].Col21 = 0;
                }
                else {
                    $scope.matrixList[1].Col21 = data;
                }
            }


            else if (cellName == "22") {

                if (data > parseFloat($scope.matrixList[1].ColQuantity)) {

                    $("input[name='Col22']").eq(1).val(0);

                    swal({
                        text: "غير مسموح بقيمه اقل من صفر ",
                        type: 'warning',
                        showCancelButton: false,
                        confirmButtonColor: '#3085d6',
                        confirmButtonText: 'نعم',
                        confirmButtonClass: 'btn btn-success btn-lg',
                        buttonsStyling: false
                    });
                    $scope.matrixList[1].Col22 = 0;
                }
                else {
                    $scope.matrixList[1].Col22 = data;
                }
            }
        }


        else if (index == 2) {
            if (cellName == "18") {
                if (data > parseFloat($scope.matrixList[2].ColQuantity)) {

                    $("input[name='Col18']").eq(2).val(0);

                    swal({
                        text: "غير مسموح بقيمه اقل من صفر ",
                        type: 'warning',
                        showCancelButton: false,
                        confirmButtonColor: '#3085d6',
                        confirmButtonText: 'نعم',
                        confirmButtonClass: 'btn btn-success btn-lg',
                        buttonsStyling: false
                    });
                    $scope.matrixList[2].Col18 = 0;
                }
                else {
                    $scope.matrixList[2].Col18 = data;
                }
            }

            else if (cellName == "21") {
                if (data > parseFloat($scope.matrixList[2].ColQuantity)) {

                    $("input[name='Col21']").eq(2).val(0);

                    swal({
                        text: "غير مسموح بقيمه اقل من صفر ",
                        type: 'warning',
                        showCancelButton: false,
                        confirmButtonColor: '#3085d6',
                        confirmButtonText: 'نعم',
                        confirmButtonClass: 'btn btn-success btn-lg',
                        buttonsStyling: false
                    });
                    $scope.matrixList[2].Col21 = 0;
                }
                else {
                    $scope.matrixList[2].Col21 = data;
                }
            }
            else if (cellName == "24") {
                if (data > parseFloat($scope.matrixList[2].ColQuantity)) {

                    $("input[name='Col24']").eq(2).val(0);

                    swal({
                        text: "غير مسموح بقيمه اقل من صفر ",
                        type: 'warning',
                        showCancelButton: false,
                        confirmButtonColor: '#3085d6',
                        confirmButtonText: 'نعم',
                        confirmButtonClass: 'btn btn-success btn-lg',
                        buttonsStyling: false
                    });
                    $scope.matrixList[2].Col24 = 0;
                }
                else {
                    $scope.matrixList[2].Col24 = data;
                }
            }
        }



        else if (index == 3) {
            if (cellName == "18") {
                if (data > parseFloat($scope.matrixList[3].ColQuantity)) {

                    $("input[name='Col18']").eq(3).val(0);

                    swal({
                        text: "غير مسموح بقيمه اقل من صفر ",
                        type: 'warning',
                        showCancelButton: false,
                        confirmButtonColor: '#3085d6',
                        confirmButtonText: 'نعم',
                        confirmButtonClass: 'btn btn-success btn-lg',
                        buttonsStyling: false
                    });
                    $scope.matrixList[3].Col18 = 0;
                }
                else {
                    $scope.matrixList[3].Col18 = data;
                }
            }


            else if (cellName == "22") {
                if (data > parseFloat($scope.matrixList[3].ColQuantity)) {

                    $("input[name='Col22']").eq(3).val(0);

                    swal({
                        text: "غير مسموح بقيمه اقل من صفر ",
                        type: 'warning',
                        showCancelButton: false,
                        confirmButtonColor: '#3085d6',
                        confirmButtonText: 'نعم',
                        confirmButtonClass: 'btn btn-success btn-lg',
                        buttonsStyling: false
                    });
                    $scope.matrixList[3].Col22 = 0;
                }
                else {
                    $scope.matrixList[3].Col22 = data;
                }
            }

            else if (cellName == "24") {
                if (data > parseFloat($scope.matrixList[3].ColQuantity)) {

                    $("input[name='Col24']").eq(3).val(0);

                    swal({
                        text: "غير مسموح بقيمه اقل من صفر ",
                        type: 'warning',
                        showCancelButton: false,
                        confirmButtonColor: '#3085d6',
                        confirmButtonText: 'نعم',
                        confirmButtonClass: 'btn btn-success btn-lg',
                        buttonsStyling: false
                    });
                    $scope.matrixList[3].Col24 = 0;
                }
                else {
                    $scope.matrixList[3].Col24 = data;
                }
            }
        }



        else if (index == 4) {
            if (cellName == "21") {
                if (data > parseFloat($scope.matrixList[4].ColQuantity)) {

                    $("input[name='Col21']").eq(4).val(0);

                    swal({
                        text: "غير مسموح بقيمه اقل من صفر ",
                        type: 'warning',
                        showCancelButton: false,
                        confirmButtonColor: '#3085d6',
                        confirmButtonText: 'نعم',
                        confirmButtonClass: 'btn btn-success btn-lg',
                        buttonsStyling: false
                    });
                    $scope.matrixList[4].Col21 = 0;
                }
                else {
                    $scope.matrixList[4].Col21 = data;
                }

            }
            else if (cellName == "22") {
                if (data > parseFloat($scope.matrixList[4].ColQuantity)) {

                    $("input[name='Col22']").eq(4).val(0);

                    swal({
                        text: "غير مسموح بقيمه اقل من صفر ",
                        type: 'warning',
                        showCancelButton: false,
                        confirmButtonColor: '#3085d6',
                        confirmButtonText: 'نعم',
                        confirmButtonClass: 'btn btn-success btn-lg',
                        buttonsStyling: false
                    });
                    $scope.matrixList[4].Col22 = 0;
                }
                else {
                    $scope.matrixList[4].Col22 = data;
                }
            }

            else if (cellName == "24") {
                if (data > parseFloat($scope.matrixList[4].ColQuantity)) {

                    $("input[name='Col24']").eq(4).val(0);

                    swal({
                        text: "غير مسموح بقيمه اقل من صفر ",
                        type: 'warning',
                        showCancelButton: false,
                        confirmButtonColor: '#3085d6',
                        confirmButtonText: 'نعم',
                        confirmButtonClass: 'btn btn-success btn-lg',
                        buttonsStyling: false
                    });
                    $scope.matrixList[4].Col24 = 0;
                }
                else {
                    $scope.matrixList[4].Col24 = data;
                }
            }

        }
        $scope.recalculateTransQuant();

    }



    $scope.matrixTotals = function () {

        //$scope.matrixList[5].Col24 = (parseFloat($scope.matrixList[1].Col24) + parseFloat($scope.matrixList[2].Col24) + parseFloat($scope.matrixList[3].Col24) + parseFloat($scope.matrixList[4].Col24)).toFixed(2);
        //$scope.matrixList[5].Col22 = (parseFloat($scope.matrixList[1].Col22) + parseFloat($scope.matrixList[2].Col22) + parseFloat($scope.matrixList[3].Col22) + parseFloat($scope.matrixList[4].Col22)).toFixed(2);
        //$scope.matrixList[5].Col21 = (parseFloat($scope.matrixList[1].Col21) + parseFloat($scope.matrixList[2].Col21) + parseFloat($scope.matrixList[3].Col21) + parseFloat($scope.matrixList[4].Col21)).toFixed(2);
        //$scope.matrixList[5].Col18 = (parseFloat($scope.matrixList[1].Col18) + parseFloat($scope.matrixList[2].Col18) + parseFloat($scope.matrixList[3].Col18) + parseFloat($scope.matrixList[4].Col18)).toFixed(2);
        //$scope.matrixList[5].ColQuantity = (parseFloat($scope.matrixList[1].ColQuantity) + parseFloat($scope.matrixList[2].ColQuantity) + parseFloat($scope.matrixList[3].ColQuantity) + parseFloat($scope.matrixList[4].ColQuantity)).toFixed(2);
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    $scope.getAccountList = function (billSettigObj) {

        var billType = billSettigObj.BILL_TYPE_ID;
        accountsService.getAccountBoxByTypesForBill(billType, "pay", $scope.COM_BRN_IDForFilter).then(function (result) {
            $scope.accountList = result.data;

        });

    }

    /////////////////////cost centers
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
        $scope.isLockPrice = false;

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
            if ($scope.gridData.LockPrice_INDEX == null || $scope.gridData.LockPrice_INDEX == 0) { $scope.isLockPrice = true; }

        });
    }
    /////////////////////////add new 
    $scope.addNew = function () {

        if ($scope.IsQuickAccount) {


            var parentElem = angular.element($document[0].querySelector('.app-content'));
            $scope.acccountModalOpenItm = false;

            $scope.typeOfAccount = 5;

            var temp = "<div class='modal-demo'><div class='modal-body' id='modal-body'><div class='panel-body panel-body panel-white'><div class='table-responsive'> <div  ng-include src=\"'../../ngApp/QuickAccount/views/quickAccount.html'\"> </div></div></div><div class='modal-footer'> <button type='button' id='closeItemModal' ng-click='GetBillSetting();cancel()' class='btn btn- primary'>اغلاق</button> </div> </div>";
            // var temp = "<div  ng-include src=\"'../../ngApp/QuickAccount/views/quickAccount.html'\" > </div>";

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

        }
        else {
            // var type = $location.search().billType;
            //  if (type == 1 || type == 3 || type == 5 || type == 7) {

            if ($scope.BillAccountName == "مورد" || $scope.BillAccountName == "المورد") { $scope.customerType = 's'; }

            else if ($scope.BillAccountName == "ورشه" || $scope.BillAccountName == "ورشة") { $scope.customerType = 'w'; }
            else {
                $scope.customerType = 'c';
            }

            var parentElem = angular.element($document[0].querySelector('.app-content'));
            $scope.acccountModalOpenItm = false;
            var temp = "<div class='modal-demo'><div class='modal-body' id='modal-body'><div class='panel-body panel-body panel-white'><div class='table-responsive'> <div  ng-include src=\"'../../ngApp/Customers/views/customers.html'\"> </div></div></div><div class='modal-footer'> <button type='button' id='closeItemModal' ng-click='GetBillSetting();cancel()' class='btn btn- primary'>اغلاق</button> </div> </div>";
            // var temp = "<div  ng-include src=\"'../../ngApp/Customers/views/customers.html'\"> </div>";

            var modalInstance = $uibModal.open({
                //templateUrl: 'accountModal.html',
                template: temp,
                controller: 'modalCtrl',
                scope: $scope,
                preserveScope: true,
                resolve: {
                    type: function () {
                        return $scope.type;
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
        }
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
        $scope.IsLockPrice = false;


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
        if ($scope.settingChecks.IsLockPrice == true) {

            $scope.IsLockPrice = true;
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

    ///////////////get cost centers

    $scope.getMainCostCenters = function () {
        costService.getMainCostCenters().then(function (result) {
            $scope.mainCostCenterList = result.data;
        })
    }

    ///////////////////////////////////////////////////////////////////////////////pay types

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
    //////////////////////////////////////////getAccount

    $scope.getAccount = function (data, index) {
        if (data > 0) {
            $scope.payTypesList[index].AccountID = data;
        }
    }
    /////////////////////////get all bill pay ttpes by master id 
    $scope.getBillPayTypesByMasterID = function (masterId) {
        return billPayTypesService.getByMasterID(masterId);
    }

    $scope.getPayTypesByID = function (masterId) {
        $scope.getBillPayTypesByMasterID(masterId).then(function (result) {
            var payList = result.data;
            $scope.payTypesList = payList;

        });
    }

    ///////////////delete bill pay types 
    $scope.deleteBillPayTypes = function (masterID) {
        $scope.getBillPayTypesByMasterID(masterID).then(function (result) {
            if (result != null && result != undefined) {
                var returnPayTypes = result.data;
                for (var i = 0; i < returnPayTypes.length; i++) {
                    billPayTypesService.delete(returnPayTypes[i]);
                }
            }
        });
    }



    ////////////////////////////////////////////get all caliber transactions  by master id 
    $scope.getCaliberTransByMasterID = function (masterId) {
        return billCaliberTransactionsService.getByMasterID(masterId);
    }

    $scope.getTransactionsByID = function (masterId) {
        $scope.getCaliberTransByMasterID(masterId).then(function (result) {
            $scope.matrixObj1 = { TransQuant: "الاجمالى بعد التحويلات", Col18: " الي عيار 18", Col21: " الي عيار 21", Col22: " الي عيار 22", Col24: " الي عيار 24", ColQuantity: "الكمية", ColName: "تحويلات العيار", read18: true, read21: true, read22: true, read24: true, readQ: true };
            $scope.disable = false;
            $scope.matrixList[0] = $scope.matrixObj1;

            if (result.data != undefined && result.data != null) {
                var returnData = result.data;
                // $scope.matrixList = returnData;
                for (var i = 0; i < returnData.length; i++) {
                    $scope.matrixEntity = {};

                    if (returnData[i].ColName == "من عيار 24"
                    ) {
                        $scope.matrixList[1].Col18 = returnData[i].Col18;
                        $scope.matrixList[1].Col21 = returnData[i].Col21;
                        $scope.matrixList[1].Col22 = returnData[i].Col22;
                        $scope.matrixList[1].Col24 = returnData[i].Col24;
                        $scope.matrixList[1].ColQuantity = returnData[i].ColQuantity;
                        $scope.matrixList[1].TransQuant = returnData[i].TransQuant;
                        $scope.matrixList[1].ColName = returnData[i].ColName;
                    }
                    else if (returnData[i].ColName == "من عيار 22") {
                        $scope.matrixList[2].Col18 = returnData[i].Col18;
                        $scope.matrixList[2].Col21 = returnData[i].Col21;
                        $scope.matrixList[2].Col22 = returnData[i].Col22;
                        $scope.matrixList[2].Col24 = returnData[i].Col24;
                        $scope.matrixList[2].ColQuantity = returnData[i].ColQuantity;
                        $scope.matrixList[2].TransQuant = returnData[i].TransQuant;
                        $scope.matrixList[2].ColName = returnData[i].ColName;
                    }
                    else if (returnData[i].ColName == "من عيار 21") {
                        $scope.matrixList[3].Col18 = returnData[i].Col18;
                        $scope.matrixList[3].Col21 = returnData[i].Col21;
                        $scope.matrixList[3].Col22 = returnData[i].Col22;
                        $scope.matrixList[3].Col24 = returnData[i].Col24;
                        $scope.matrixList[3].ColQuantity = returnData[i].ColQuantity;
                        $scope.matrixList[3].TransQuant = returnData[i].TransQuant;
                        $scope.matrixList[3].ColName = returnData[i].ColName;
                    }
                    else if (returnData[i].ColName == "من عيار 18") {
                        $scope.matrixList[4].Col18 = returnData[i].Col18;
                        $scope.matrixList[4].Col21 = returnData[i].Col21;
                        $scope.matrixList[4].Col22 = returnData[i].Col22;
                        $scope.matrixList[4].Col24 = returnData[i].Col24;
                        $scope.matrixList[4].ColQuantity = returnData[i].ColQuantity;
                        $scope.matrixList[4].TransQuant = returnData[i].TransQuant;
                        $scope.matrixList[4].ColName = returnData[i].ColName;
                    }

                }
            }

        });
    }

    $scope.getGoldTotalsByBillID = function (billID) {
        //PointOfSaleService.getByBillID(billID).then(function (result) {
        //    var returnBill = result.data;
        //    $scope.matrixList[5].Col18 = returnBill.TransTotalGold18;
        //    $scope.matrixList[5].Col21 = returnBill.TransTotalGold21;
        //    $scope.matrixList[5].Col22 = returnBill.TransTotalGold22;
        //    $scope.matrixList[5].Col24 = returnBill.TransTotalGold24;
        //    $scope.matrixList[5].ColQuantity = returnBill.TransTotalGoldQuantity;
        //});
    }

    ///////////////////////delete caliber transaction by master id
    $scope.deleteCaliberTrans = function (masterID) {
        $scope.getCaliberTransByMasterID(masterID).then(function (result) {
            if (result != null && result != undefined) {
                var returnCaliberTrans = result.data;
                for (var i = 0; i < returnCaliberTrans.length; i++) {
                    billCaliberTransactionsService.delete(returnCaliberTrans[i]);
                }
            }

        });
    }


    $scope.deleteEntryOfBill = function (billID) {
        entryService.deleteEntryOfBill(billID).then(function (result) {
            var x = result.data;
        });
    }



    /////////////////////////////////////////////////get customer accounts object to bill master ///////////////////////////////////////////
    $scope.getCustObject = function () {
        // 
        $scope.custObject = {};
        if ($scope.billMaster.CUST_ACC_ID != undefined && $scope.billMaster.CUST_ACC_ID != null) {
            $scope.custObject.accountID = $scope.billMaster.CUST_ACC_ID;

            if ($scope.bill_type_Id == 23 || $scope.bill_type_Id == 24 || $scope.bill_type_Id == 19 || $scope.bill_type_Id == 20) {

                var TotalManufacturingWages = parseFloat($scope.billMaster.totalManufactWages);// - $scope.wagesDiscountVal);
                var ManufacturingWagesTaxes = parseFloat(parseFloat($scope.billMaster.manufacturingWagesMainVat) * parseFloat($scope.billMaster.MainVatRate) / 100);
                var TotalPurchasingValues = parseFloat($scope.billMaster.totalPrice - $scope.billMaster.TotalAllDisc);
                var PurchasingValuesTaxes = parseFloat($scope.billMaster.TotalMainVat) * parseFloat($scope.billMaster.MainVatRate) / 100;

                $scope.custObject.accountType = $scope.bill_type_Id == 19 || $scope.bill_type_Id == 20 ? "C" : "D";
              
               
                $scope.custObject.moneyValue = $scope.bill_type_Id == 19 || $scope.bill_type_Id == 23 ? (TotalManufacturingWages + ManufacturingWagesTaxes).toFixed(2) :
                    (TotalManufacturingWages + TotalPurchasingValues + parseFloat($scope.billMaster.MainVatValue)).toFixed(2);
                $scope.custObject.gold24Value = $scope.bill_type_Id == 19 || $scope.bill_type_Id == 23 ? $scope.total24 : 0;
                $scope.custObject.gold22Value = $scope.bill_type_Id == 19 || $scope.bill_type_Id == 23 ? $scope.total22 : 0;
                $scope.custObject.gold21Value = $scope.bill_type_Id == 19 || $scope.bill_type_Id == 23 ? $scope.total21 : 0;
                $scope.custObject.gold18Value = $scope.bill_type_Id == 19 || $scope.bill_type_Id == 23 ? $scope.total18 : 0;
                if ($scope.bill_type_Id == 20) {
                    
                var val   = parseFloat($scope.custObject.moneyValue) + parseFloat($scope.TempPurchasTax);
                $scope.custObject.moneyValue = val;
                }
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
                //    $scope.custObject.moneyValue = $scope.billMaster.totalPrice - $scope.billMaster.TotalAllDisc;
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
                //$scope.custObject.moneyValue = $scope.billMaster.totalManufactWages - $scope.wagesDiscountVal;
                //$scope.custObject.gold24Value = 0;
                //$scope.custObject.gold22Value = 0;
                //$scope.custObject.gold21Value = 0;
                //$scope.custObject.gold18Value = 0;
                //$scope.billMaster.accounts.push($scope.custObject);

                ////قيد قيمة ضريبة اجور التصنيع
                //$scope.custObject = {};
                //$scope.custObject.accountID = $scope.billMaster.CUST_ACC_ID;
                //$scope.custObject.accountType = $scope.bill_type_Id == 19 || $scope.bill_type_Id == 20 ? "C" : "D";
                //$scope.custObject.moneyValue = parseFloat($scope.billMaster.totalTaxableManufactWages) * $scope.wages_taxValue / 100;
                //$scope.custObject.gold24Value = 0;
                //$scope.custObject.gold22Value = 0;
                //$scope.custObject.gold21Value = 0;
                //$scope.custObject.gold18Value = 0;
                //$scope.billMaster.accounts.push($scope.custObject);
            }

            else if ($scope.bill_type_Id == 2 || /*$scope.bill_type_Id == 28*/$scope.bill_type_Id == 26 ) {
                $scope.custObject = {};
                $scope.custObject.accountID = $scope.billMaster.CUST_ACC_ID;
                $scope.custObject.accountType = 'D';
                $scope.custObject.moneyValue = $scope.billMaster.totalRemaining;
                $scope.custObject.gold24Value = 0;
                $scope.custObject.gold22Value = 0;
                $scope.custObject.gold21Value = 0;
                $scope.custObject.gold18Value = 0;
                $scope.billMaster.accounts.push($scope.custObject);
            }
            // صرف ذهب كسر
            else if ( /*$scope.bill_type_Id == 28*/$scope.bill_type_Id == 13) {
                $scope.custObject = {};
                $scope.custObject.accountID = $scope.billMaster.CUST_ACC_ID;
                $scope.custObject.accountType = 'D';
                $scope.custObject.moneyValue = 0;
                $scope.custObject.gold24Value = $scope.total24;
                $scope.custObject.gold22Value = $scope.total22;
                $scope.custObject.gold21Value = $scope.total21;
                $scope.custObject.gold18Value = $scope.total18;
                $scope.billMaster.accounts.push($scope.custObject);
            }
            // قبض ذهب كسر
            else if ( /*$scope.bill_type_Id == 28*/$scope.bill_type_Id == 14) {
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

            else if ($scope.bill_type_Id == 1 || /*$scope.bill_type_Id == 27*/$scope.bill_type_Id == 25 ) {
                $scope.custObject = {};
                $scope.custObject.accountID = $scope.billMaster.CUST_ACC_ID;
                $scope.custObject.accountType = 'C';
                $scope.custObject.moneyValue = $scope.billMaster.totalMustPaid;
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
                $scope.custObject.moneyValue = parseFloat($scope.billMaster.totalMustPaid) + parseFloat($scope.billMaster.TotalAllDisc);
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
                $scope.custObject.moneyValue = $scope.footerTaxTotal;
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
            if ($scope.bill_type_Id == 1 || /*$scope.bill_type_Id == 27*/$scope.bill_type_Id == 25  ) {
                $scope.custObject = {};

                //if ($scope.billMaster.CUST_ACC_ID != undefined && $scope.billMaster.CUST_ACC_ID != null) {
                $scope.custObject.accountID = 54;
                //}

                $scope.custObject.accountType = 'C';
                $scope.custObject.moneyValue = $scope.billMaster.totalMustPaid;
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

            if ($scope.bill_type_Id == 2 || /*$scope.bill_type_Id == 28*/ $scope.bill_type_Id == 26 || $scope.bill_type_Id == 13 ||$scope.bill_type_Id == 23 || $scope.bill_type_Id == 24) {
                $scope.checkAccountTypes($scope.bill_type_Id, 'X')
                $scope.itemObject.accountType = $scope.typeName;

                $scope.itemObject.moneyValue = 0;
                $scope.itemObject.gold24Value = $scope.total24;
                $scope.itemObject.gold22Value = $scope.total22;
                $scope.itemObject.gold21Value = $scope.total21;
                $scope.itemObject.gold18Value = $scope.total18;
            }

            else if ($scope.bill_type_Id == 1 ||/* $scope.bill_type_Id == 27*/ $scope.bill_type_Id == 25 || $scope.bill_type_Id == 14 ) {

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

            $scope.convertGold.gold24Value = $scope.matrixList[1].TransQuant;
            $scope.convertGold.gold22Value = $scope.matrixList[2].TransQuant;
            $scope.convertGold.gold21Value = $scope.matrixList[3].TransQuant;
            $scope.convertGold.gold18Value = $scope.matrixList[4].TransQuant;

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

                if ($scope.bill_type_Id == 2 || /*$scope.bill_type_Id == 28 */$scope.bill_type_Id == 26 || $scope.bill_type_Id == 13 ||$scope.bill_type_Id == 23 || $scope.bill_type_Id == 24) {
                    $scope.checkAccountTypes($scope.bill_type_Id, 'X')
                    $scope.itemObject.accountType = $scope.typeName;

                    $scope.itemObject.moneyValue = 0;
                    $scope.itemObject.gold24Value = $scope.goldAccountsList[i].gold24;
                    $scope.itemObject.gold22Value = $scope.goldAccountsList[i].gold22;
                    $scope.itemObject.gold21Value = $scope.goldAccountsList[i].gold21;
                    $scope.itemObject.gold18Value = $scope.goldAccountsList[i].gold18;
                }

                else if ($scope.bill_type_Id == 1 || /*$scope.bill_type_Id == 27*/ $scope.bill_type_Id == 25 || $scope.bill_type_Id == 14 ) {

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

                $scope.convertGold.gold24Value = $scope.matrixList[1].TransQuant;
                $scope.convertGold.gold22Value = $scope.matrixList[2].TransQuant;
                $scope.convertGold.gold21Value = $scope.matrixList[3].TransQuant;
                $scope.convertGold.gold18Value = $scope.matrixList[4].TransQuant;

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


        if ($scope.bill_type_Id == 2 || /*$scope.bill_type_Id == 28 */$scope.bill_type_Id == 26  ||$scope.bill_type_Id == 20) {
            $scope.commTaxObject.accountType = 'D';

            $scope.commTaxObject.moneyValue = value;
            $scope.commTaxObject.gold24Value = 0;
            $scope.commTaxObject.gold22Value = 0;
            $scope.commTaxObject.gold21Value = 0;
            $scope.commTaxObject.gold18Value = 0;
        }

        else if ($scope.bill_type_Id == 1 || /*$scope.bill_type_Id == 27*/$scope.bill_type_Id == 25 ) {
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

    /////





    ////////add pay types to accounts 
    $scope.billAcoounts = function (accountID, value, bankCommValue, commTaxValue) {
        //$scope.getAccountByID(accountID).then(function (result) {
        $scope.payObject = {};
        $scope.payObject.accountID = accountID;
        //    var type = result.data;

        if ($scope.bill_type_Id == 2 || /*$scope.bill_type_Id == 28*/$scope.bill_type_Id == 26  ) {
            $scope.checkAccountTypes($scope.bill_type_Id, 'S');
            $scope.payObject.accountType = $scope.typeName;
            var money = (parseFloat(value) - parseFloat(bankCommValue) - parseFloat(commTaxValue)).toFixed(2);
            $scope.payObject.moneyValue = money;
            $scope.payObject.gold24Value = 0;
            $scope.payObject.gold22Value = 0;
            $scope.payObject.gold21Value = 0;
            $scope.payObject.gold18Value = 0;

        }


        else if ($scope.bill_type_Id == 1 || /*$scope.bill_type_Id == 27*/$scope.bill_type_Id == 25 ) {
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
        if (type == 2 || /*type == 28 */type == 26 || type == 13 || type == 17 || type == 4 || type == 6 || type == 23 || type == 24) {
            if (acc == 'S') {
                $scope.typeName = 'D';
            }
            else { $scope.typeName = 'C'; }
        }
        else if (type == 1 || /*type == 27*/ type == 25 || type == 3 || type == 5 || type == 14 || type == 18 || type == 19 || type == 20) {
            if (acc == 'S') {
                $scope.typeName = 'C';
            }
            else { $scope.typeName = 'D' }
        }
    }
    ////////////////get accounts from bill settings and add them in account object


    //discount account
    $scope.getDiscAccFromSettings = function () {
        //   

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
            $scope.payObject.moneyValue = $scope.billMaster.totalPrice.toFixed(2);
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

            $scope.wagesObject.moneyValue = $scope.billMaster.totalManufactWages;// - $scope.wagesDiscountVal;
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
        //  
        if ($scope.billMaster.TotalVatRate != null && $scope.billMaster.TotalVatRate != 0 && $scope.billMaster.TotalVatRate != undefined) {

            if ($scope.settingsAccounts.AccVatRateID != null && $scope.settingsAccounts.AccVatRateID != undefined) {
                $scope.vatRateObject = {};
                $scope.vatRateObject.accountID = $scope.settingsAccounts.AccVatRateID;

                if ($scope.bill_type_Id == 2 ||/* $scope.bill_type_Id == 28*/$scope.bill_type_Id == 26  ) {

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
                    $scope.vatRateObject.moneyValue = parseFloat($scope.footerTotalVatValue);
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

            if ($scope.bill_type_Id == 1 || /*$scope.bill_type_Id == 27*/ $scope.bill_type_Id == 25 ) {
                $scope.purchasAccObject.accountType = 'D';

                $scope.purchasAccObject.moneyValue = (parseFloat($scope.billMaster.totalMustPaid) - parseFloat($scope.billMaster.TotalVatValue)).toFixed(2);
                $scope.purchasAccObject.gold24Value = 0;
                $scope.purchasAccObject.gold22Value = 0;
                $scope.purchasAccObject.gold21Value = 0;
                $scope.purchasAccObject.gold18Value = 0;
                $scope.billMaster.accounts.push($scope.purchasAccObject);
            }

            else if ($scope.bill_type_Id == 20 || $scope.bill_type_Id == 24) {
                $scope.purchasAccObject.accountType = $scope.bill_type_Id == 20 ? 'D' : "C";

                $scope.purchasAccObject.moneyValue = (parseFloat($scope.billMaster.totalPrice) - parseFloat($scope.billMaster.TotalAllDisc)).toFixed(2);
                $scope.purchasAccObject.gold24Value = 0;
                $scope.purchasAccObject.gold22Value = 0;
                $scope.purchasAccObject.gold21Value = 0;
                $scope.purchasAccObject.gold18Value = 0;
                $scope.billMaster.accounts.push($scope.purchasAccObject);
            }

            else {
                $scope.checkAccountTypes($scope.bill_type_Id, 'X');
                $scope.purchasAccObject.accountType = $scope.typeName;
                $scope.purchasAccObject.moneyValue = (parseFloat($scope.billMaster.totalPrice) - parseFloat($scope.billMaster.TotalAllDisc)).toFixed(2);
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
        $scope.TempPurchasTax= 0;
        if ($scope.settingsAccounts.PurchasTaxAccID != undefined && $scope.settingsAccounts.PurchasTaxAccID != null) {
            $scope.purchasTaxAccObject.accountID = $scope.settingsAccounts.PurchasTaxAccID;

            if ($scope.bill_type_Id == 1 || /*$scope.bill_type_Id == 27*/$scope.bill_type_Id == 25  ) {
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
                $scope.TempPurchasTax = $scope.billMaster.TotalVatValue;
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
            $scope.purchasGoldObject.gold24Value = $scope.matrixList[1].ColQuantity;
            $scope.purchasGoldObject.gold22Value = $scope.matrixList[2].ColQuantity;
            $scope.purchasGoldObject.gold21Value = $scope.matrixList[3].ColQuantity;
            $scope.purchasGoldObject.gold18Value = $scope.matrixList[4].ColQuantity;
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
            $scope.salesGoldObject.gold24Value = $scope.matrixList[1].ColQuantity;
            $scope.salesGoldObject.gold22Value = $scope.matrixList[2].ColQuantity;
            $scope.salesGoldObject.gold21Value = $scope.matrixList[3].ColQuantity;
            $scope.salesGoldObject.gold18Value = $scope.matrixList[4].ColQuantity;
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
            $scope.salesAccObject.moneyValue = parseFloat($scope.billMaster.totalPrice) - parseFloat($scope.billMaster.TotalAllDisc);
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

            $scope.AccSalesObject.moneyValue = parseFloat($scope.billMaster.totalMustPaid) - parseFloat($scope.billMaster.TotalVatValue);
            $scope.AccSalesObject.gold24Value = 0;
            $scope.AccSalesObject.gold22Value = 0;
            $scope.AccSalesObject.gold21Value = 0;
            $scope.AccSalesObject.gold18Value = 0;
            $scope.billMaster.accounts.push($scope.AccSalesObject);

        }
    }

    /////////////////

    //accSalesID of bill setting
    $scope.getAccSalesMoneyFromSettings = function () {
        
        if ($scope.settingsAccounts.AccSalesID != undefined && $scope.settingsAccounts.AccSalesID != null) {
            $scope.AccSalesObject = {};
            $scope.AccSalesObject.accountID = $scope.settingsAccounts.AccSalesID;

            $scope.AccSalesObject.accountType = 'C';

            $scope.AccSalesObject.moneyValue = $scope.billMaster.totalRemaining;
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

            var total = 0;
            for (var i = 0; i < $scope.payTypesList.length; i++) {
                total = parseFloat($scope.payTypesList[i].PayTypeValue) + parseFloat(total);
            }
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

            var total = 0;
            for (var i = 0; i < $scope.payTypesList.length; i++) {
                total = parseFloat($scope.payTypesList[i].PayTypeValue) + parseFloat(total);
            }
            $scope.checkAccountTypes($scope.bill_type_Id, 'S')
            $scope.AccSalesObject.accountType = $scope.typeName;

            $scope.purchasePayObject.moneyValue = $scope.billMaster.totalPrice.toFixed(2);
            $scope.purchasePayObject.gold24Value = 0;
            $scope.purchasePayObject.gold22Value = 0;
            $scope.purchasePayObject.gold21Value = 0;
            $scope.purchasePayObject.gold18Value = 0;
            $scope.billMaster.accounts.push($scope.purchasePayObject);

        }

    }


    //////////////////gold 

    //AccPurchaseGoldID
    $scope.getPurchasGoldAccFromSettings = function () {
        
        if ($scope.settingsAccounts.AccPurchaseGoldID != undefined && $scope.settingsAccounts.AccPurchaseGoldID != null) {
            $scope.purchasGoldAccObject = {};
            $scope.purchasGoldAccObject.accountID = $scope.settingsAccounts.AccPurchaseGoldID;

            if ($scope.bill_type_Id == 1 || /*$scope.bill_type_Id == 27*/$scope.bill_type_Id == 25 ) {
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
                $scope.purchasGoldAccObject.moneyValue = $scope.billMaster.totalPrice - $scope.billMaster.TotalAllDisc;
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

            if ($scope.bill_type_Id == 2 || /*$scope.bill_type_Id == 28 */$scope.bill_type_Id == 26  || $scope.bill_type_Id == 17) {
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




    $scope.customerForPurchase = function () {
        
        if ($scope.billMaster.CUST_ACC_ID != undefined && $scope.billMaster.CUST_ACC_ID != null) {
            $scope.wagesObject = {};
            $scope.wagesObject.accountID = $scope.billMaster.CUST_ACC_ID;
            $scope.wagesObject.accountType = 'C';
            $scope.wagesObject.moneyValue = $scope.billMaster.totalManufactWages - $scope.wagesDiscountVal;
            $scope.wagesObject.gold24Value = 0;
            $scope.wagesObject.gold22Value = 0;
            $scope.wagesObject.gold21Value = 0;
            $scope.wagesObject.gold18Value = 0;
            $scope.billMaster.accounts.push($scope.wagesObject);

            var totalManufactWagesAfterDiscount = $scope.wagesTaxToVat - $scope.wagesDiscountVal;
            $scope.entryDetObj = {};
            $scope.entryDetObj.accountID = $scope.billMaster.CUST_ACC_ID;
            $scope.entryDetObj.accountType = 'C';
            $scope.entryDetObj.moneyValue = (totalManufactWagesAfterDiscount * $scope.wages_taxValue / 100);
            $scope.entryDetObj.gold24Value = 0;
            $scope.entryDetObj.gold22Value = 0;
            $scope.entryDetObj.gold21Value = 0;
            $scope.entryDetObj.gold18Value = 0;
            $scope.billMaster.accounts.push($scope.entryDetObj);

            $scope.entryDetObj = {};
            $scope.entryDetObj.accountID = $scope.billMaster.CUST_ACC_ID;
            $scope.entryDetObj.accountType = 'C';
            $scope.entryDetObj.moneyValue = $scope.billMaster.totalPrice - $scope.billMaster.TotalAllDisc;
            $scope.entryDetObj.gold24Value = 0;
            $scope.entryDetObj.gold22Value = 0;
            $scope.entryDetObj.gold21Value = 0;
            $scope.entryDetObj.gold18Value = 0;
            $scope.billMaster.accounts.push($scope.entryDetObj);

            $scope.entryDetObj = {};
            $scope.entryDetObj.accountID = $scope.billMaster.CUST_ACC_ID;
            $scope.entryDetObj.accountType = 'C';
            $scope.entryDetObj.moneyValue = $scope.billMaster.TotalVatValue;
            $scope.entryDetObj.gold24Value = 0;
            $scope.entryDetObj.gold22Value = 0;
            $scope.entryDetObj.gold21Value = 0;
            $scope.entryDetObj.gold18Value = 0;
            $scope.billMaster.accounts.push($scope.entryDetObj);
        }

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

        else if ($scope.bill_type_Id == 2 || /*$scope.bill_type_Id == 28 */$scope.bill_type_Id == 26  ) {
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

            if ($scope.billMaster.totalRemaining != undefined && $scope.billMaster.totalRemaining) {
                $scope.getCustObject();
            }
        }
            // صرف ذهب كسر
        else if ($scope.bill_type_Id == 13) {
            
            if ($scope.IsEntryGoldItemsAccounts) {
                $scope.getGoldObjectsByItems();
            }
            else {
                $scope.getGoldObject();
            }


            if ($scope.billMaster.TotalVatValue != undefined && $scope.billMaster.TotalVatValue > 0) {
                $scope.getVatRateAccFromsettings();
            }

            if ($scope.billMaster.totalRemaining != undefined && $scope.billMaster.totalRemaining) {
                $scope.getCustObject();
            }
        }
        // قبض ذهب كسر
        else if ($scope.bill_type_Id == 14) {
            if ($scope.IsEntryGoldItemsAccounts) {
                $scope.getGoldObjectsByItems();
            }
            else {
                $scope.getGoldObject();
            }
            $scope.getCustObject();
            if ($scope.billMaster.TotalVatValue > 0) {
                $scope.getPurchasTaxAccFromSettings();
            }

        }

        else if ($scope.bill_type_Id == 1 || /*$scope.bill_type_Id == 27*/$scope.bill_type_Id == 25   ) {
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

    //////////////////////////////save point of sale entry ////////////////////////////////////////////////////
    $scope.saveEntry = function () {

        //if (localStorageService.get('branchId') != null) {
        //    $scope.billMaster.COM_BRN_ID = localStorageService.get('branchId').branchId;
        //}
        debugger;
        console.log($scope.bill_type_Id)
        if ($scope.bill_type_Id == 13 || $scope.bill_type_Id == 14) {

            //$scope.billMaster.accounts = jQuery.grep($scope.billMaster.accounts, function (n, i) {
            //    return (n.accountID === $scope.billMaster.GOLD_ACC_ID || n.accountID === $scope.billMaster.CUST_ACC_ID);
            //});
        }
        

        ////////////get total credit and depit for entry master
        for (var i = 0; i < $scope.billMaster.accounts.length; i++) {
            if ($scope.billMaster.accounts[i].accountType == 'C') {
                $scope.totalEntryCredit = parseFloat($scope.billMaster.accounts[i].moneyValue) + parseFloat($scope.totalEntryCredit);
            }
            else if ($scope.billMaster.accounts[i].accountType == 'D') {
                $scope.totalEntryDepit = parseFloat($scope.billMaster.accounts[i].moneyValue) + parseFloat($scope.totalEntryCredit);
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
        $scope.entryMaster.BILL_ID = $scope.billMasterID;
        $scope.entryMaster.EMP_ID = $scope.billMaster.EMP_ID;
        $scope.entryMaster.ENTRY_NUMBER = $scope.ENTRY_NUMBER;

        if ($scope.operation != "Insert") {
            $scope.entryMaster.ENTRY_ID = $scope.billMaster.ENTRY_ID;
            $scope.entryMaster.BILL_ID = $scope.billID;
        }

        ////////fill entry details
        $scope.entryDetails = [];

        for (var i = 0; i < $scope.billMaster.accounts.length; i++) {
            
            $scope.entryDetObj = {};
            $scope.entryDetObj.ENTRY_ROW_NUMBER = i;

            $scope.entryDetObj.ACC_ID = $scope.billMaster.accounts[i].accountID;

            if ($scope.billMaster.accounts[i].accountType == 'C') {
                $scope.entryDetObj.ENTRY_CREDIT = $scope.billMaster.accounts[i].moneyValue;

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

                    //var gold_rec = jQuery.grep($scope.billMaster.accounts, function (n, i) {
                    //    return (n.accountType === 'D' && n.accountID === $scope.billMaster.GOLD_ACC_ID);
                    //});

                    //$scope.entryDetObj.ENTRY_CREDIT = 0;
                    //$scope.entryDetObj.ENTRY_GOLD24_CREDIT = gold_rec[0].gold24Value;
                    //$scope.entryDetObj.ENTRY_GOLD22_CREDIT = gold_rec[0].gold22Value;
                    //$scope.entryDetObj.ENTRY_GOLD21_CREDIT = gold_rec[0].gold21Value;
                    //$scope.entryDetObj.ENTRY_GOLD18_CREDIT = gold_rec[0].gold18Value;

                    //if ($scope.bill_type_Id == 14) {

                    //    $scope.entryDetObj.ENTRY_GOLD24_CREDIT = $scope.convertGold.gold24Value;
                    //    $scope.entryDetObj.ENTRY_GOLD22_CREDIT = $scope.convertGold.gold22Value;
                    //    $scope.entryDetObj.ENTRY_GOLD21_CREDIT = $scope.convertGold.gold21Value;
                    //    $scope.entryDetObj.ENTRY_GOLD18_CREDIT = $scope.convertGold.gold18Value;
                    //}
                }
                //if ($scope.bill_type_Id == 19) {

                //    var wages_rec = jQuery.grep($scope.billMaster.accounts, function (n, i) {
                //        return (n.accountID === $scope.acc_wages_Id);
                //    });
                //    $scope.entryDetObj.ENTRY_CREDIT = wages_rec[0].moneyValue;

                //}
                //Purchas
                if ($scope.bill_type_Id == 18) {
                    // $scope.entryDetObj.ENTRY_CREDIT = $scope.billMaster.totalPrice - $scope.billMaster.TotalAllDisc;
                    if ($scope.billMaster.accounts[i].accountID == $scope.acc_purchas_Id) {
                        $scope.entryDetObj.ENTRY_CREDIT = 0;
                    }
                }

                //Sales
                //if ($scope.bill_type_Id == 17 && $scope.billMaster.accounts[i].accountID == $scope.acc_sales_Id) {
                //    if ($scope.entryDetObj.ENTRY_CREDIT > 0) {
                //        $scope.entryDetObj.ENTRY_CREDIT = $scope.billMaster.totalPrice - $scope.billMaster.TotalAllDisc;
                //    }
                //}

                //if ($scope.bill_type_Id == 17) {
                //    if ($scope.billMaster.accounts[i].accountID == $scope.acc_vat_rateId) {
                //        $scope.entryDetObj.ENTRY_CREDIT = $scope.billMaster.TotalVatValue;
                //    }
                //}

            }
            else if ($scope.billMaster.accounts[i].accountType == 'D') {
                $scope.entryDetObj.ENTRY_DEBIT = $scope.billMaster.accounts[i].moneyValue;
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
                    //var gold_rec = jQuery.grep($scope.billMaster.accounts, function (n, i) {
                    //    return (n.accountType === 'C');
                    //});

                    //$scope.entryDetObj.ENTRY_DEBIT = 0;
                    //$scope.entryDetObj.ENTRY_GOLD24_DEBIT = $scope.convertGold.gold24Value;
                    //$scope.entryDetObj.ENTRY_GOLD22_DEBIT = $scope.convertGold.gold22Value;
                    //$scope.entryDetObj.ENTRY_GOLD21_DEBIT = $scope.convertGold.gold21Value;
                    //$scope.entryDetObj.ENTRY_GOLD18_DEBIT = $scope.convertGold.gold18Value;

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
                $scope.entryDetObj.ACC_ID != "") {
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
        //    $scope.entryDetObj.ENTRY_DEBIT = $scope.billMaster.totalPrice - $scope.billMaster.TotalAllDisc;
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



            $scope.billMaster.BILL_ID = $scope.billMasterID;
            $scope.billMaster.BILL_IS_POST = true;
            PointOfSaleService.updateEntryID($scope.billMaster, id).then(function (response) {
                var x = response.data;
            });
        });

        //Get report data
        $scope.GetReportData();
    }


    /////////////update entry
    $scope.updateEntry = function () {
        for (var i = 0; i < $scope.billMaster.accounts.length; i++) {
            if ($scope.billMaster.accounts[i].accountType == 'C') {
                $scope.totalEntryCredit = parseFloat($scope.billMaster.accounts[i].moneyValue) + parseFloat($scope.totalEntryCredit);
            }
            else if ($scope.billMaster.accounts[i].accountType == 'D') {
                $scope.totalEntryDepit = parseFloat($scope.billMaster.accounts[i].moneyValue) + parseFloat($scope.totalEntryCredit);
            }
        }
        $scope.entryMaster = {};
        $scope.entryMaster.ENTRY_SETTING_ID = 130;
        $scope.entryMaster.ENTRY_DATE = $scope.billMaster.BILL_DATE;
        $scope.entryMaster.ENTRY_CREDIT = $scope.totalEntryCredit;
        $scope.entryMaster.ENTRY_DEBIT = $scope.totalEntryDepit;
        $scope.entryMaster.ACC_ID = $scope.billMaster.PAY_ACC_ID;
        $scope.entryMaster.CURRENCY_ID = $scope.billMaster.CURRENCY_ID;
        $scope.entryMaster.CURRENCY_RATE = $scope.billMaster.CURRENCY_RATE;
        // $scope.entryMaster.BILL_ID = $scope.billMasterID;
        $scope.entryMaster.EMP_ID = $scope.billMaster.EMP_ID;
        $scope.entryMaster.ENTRY_NUMBER = $scope.ENTRY_NUMBER;
        $scope.entryMaster.BILL_ID = $scope.billID;
        $scope.entryMaster.ENTRY_ID = $scope.billMaster.ENTRY_ID;

        ////////fill entry details
        $scope.entryDetails = [];

        for (var i = 0; i < $scope.billMaster.accounts.length; i++) {
            $scope.entryDetObj = {};
            $scope.entryDetObj.ENTRY_ROW_NUMBER = i;

            $scope.entryDetObj.ACC_ID = $scope.billMaster.accounts[i].accountID;

            if ($scope.billMaster.accounts[i].accountType == 'C') {
                $scope.entryDetObj.ENTRY_CREDIT = $scope.billMaster.accounts[i].moneyValue;

                $scope.entryDetObj.ENTRY_GOLD24_CREDIT = $scope.billMaster.accounts[i].gold24Value;
                $scope.entryDetObj.ENTRY_GOLD22_CREDIT = $scope.billMaster.accounts[i].gold22Value;
                $scope.entryDetObj.ENTRY_GOLD21_CREDIT = $scope.billMaster.accounts[i].gold21Value;
                $scope.entryDetObj.ENTRY_GOLD18_CREDIT = $scope.billMaster.accounts[i].gold18Value;
            }
            else if ($scope.billMaster.accounts[i].accountType == 'D') {
                $scope.entryDetObj.ENTRY_DEBIT = $scope.billMaster.accounts[i].moneyValue;
                $scope.entryDetObj.ENTRY_GOLD24_DEBIT = $scope.billMaster.accounts[i].gold24Value;
                $scope.entryDetObj.ENTRY_GOLD22_DEBIT = $scope.billMaster.accounts[i].gold22Value;
                $scope.entryDetObj.ENTRY_GOLD21_DEBIT = $scope.billMaster.accounts[i].gold21Value;
                $scope.entryDetObj.ENTRY_GOLD18_DEBIT = $scope.billMaster.accounts[i].gold18Value;
            }

            $scope.entryDetObj.COST_CENTER_ID = $scope.billMaster.CostCenterID;
            $scope.entryDetails.push($scope.entryDetObj);
        }


        var entryMasterDetails = { EntryMaster: $scope.entryMaster, EntryDetails: $scope.entryDetails };
        entryService.updateEntryOfBill(entryMasterDetails).then(function (result) {
            var id = result.data;

            $scope.billMaster.BILL_ID = $scope.billMasterID;

            PointOfSaleService.updateEntryID($scope.billMaster, id).then(function (response) {
                var x = response.data;
            });
        });

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

    ///////////////////////////get grid data columns///////////////////////////////////////////
    $scope.getGridColData = function (settingId) {
        billGridColumn.getBySettingID(settingId).then(function (result) {
            
            $scope.returnGridData = result.data;
            $scope.defaultData = {
                arCode: 'كود', arItem: 'الصنف', arQuantity: 'الكميه', arItemWeight: 'الوزن بالجرام', arItemGmWages: 'اجره الجرام', arManfactWages: 'اجره الصنف', arUnit: 'الوحده', arCaliberName: 'العيار',
                arPrice: 'السعر', arDiscount: 'قيمة الخصم', arDiscRate: 'نسية الخصم', arTotal: 'الاجمالي', arClearnessRate: 'النقاوه', arCaliber24: 'عيار 24', arCaliber22: 'عيار 22', arCaliber21: 'عيار 21',
                arCaliber18: 'عيار 18', arCostCenter: 'مركز التكلفه', arSubjectToVat: 'خاضع للضريبه', arVatRate: 'نسبه الضريبه', arVatValue: 'قيمة الضريبه', arWagesDiscValue: 'قيمة الخصم لاجور التصنيع'
                , arWagesDiscRate: 'نسبه الخصم لاجور التصنيع', arActualWeight: 'الوزن الفعلي', arTaxTotal: 'الاجمالي بالضريبه', arTotalItemGmWages: 'اجمالي اجره التصنيع', arTotalGoldManufact: 'اجمالي الذهب و اجور التصنيع'
                , arExemptOfTax: 'معفي من الضريبه', arItemPrice: 'سعر الصنف', arAfterDiscount: 'بعد الخصم',arLockPrice:'سعر الاقفال'
            }

            //////////////////////////////arabic names
            if ($scope.returnGridData.ARCode == "" || $scope.returnGridData.ARCode == undefined) {
                $scope.gridData.ARCode = $scope.defaultData.arCode;
            }
            else { $scope.gridData.ARCode = $scope.returnGridData.ARCode; }


            if ($scope.returnGridData.ARItem == "" || $scope.returnGridData.ARItem == undefined) {
                $scope.gridData.ARItem = $scope.defaultData.arItem;
            }
            else { $scope.gridData.ARItem = $scope.returnGridData.ARItem; }


            if ($scope.returnGridData.ARQuantity == "" || $scope.returnGridData.ARQuantity == undefined) {
                $scope.gridData.ARQuantity = $scope.defaultData.arQuantity;
            }
            else { $scope.gridData.ARQuantity = $scope.returnGridData.ARQuantity; }


            if ($scope.returnGridData.ARItemWeight == "" || $scope.returnGridData.ARItemWeight == undefined) {
                $scope.gridData.ARItemWeight = $scope.defaultData.arItemWeight;
            }
            else { $scope.gridData.ARItemWeight = $scope.returnGridData.ARItemWeight; }


            if ($scope.returnGridData.ARItemGmWages == "" || $scope.returnGridData.ARItemGmWages == undefined) {
                $scope.gridData.ARItemGmWages = $scope.defaultData.arItemGmWages;
            }
            else { $scope.gridData.ARItemGmWages = $scope.returnGridData.ARItemGmWages; }


            if ($scope.returnGridData.ARManufacturingWages == "" || $scope.returnGridData.ARManufacturingWages == undefined) {
                $scope.gridData.ARManufacturingWages = $scope.defaultData.arManfactWages;
            }
            else { $scope.gridData.ARManufacturingWages = $scope.returnGridData.ARManufacturingWages; }


            if ($scope.returnGridData.ARUnit == "" || $scope.returnGridData.ARUnit == undefined) {
                $scope.gridData.ARUnit = $scope.defaultData.arUnit;
            }
            else { $scope.gridData.ARUnit = $scope.returnGridData.ARUnit; }


            if ($scope.returnGridData.ARCaliberName == "" || $scope.returnGridData.ARCaliberName == undefined) {
                $scope.gridData.ARCaliberName = $scope.defaultData.arCaliberName;
            }
            else { $scope.gridData.ARCaliberName = $scope.returnGridData.ARCaliberName; }


            if ($scope.returnGridData.ARPrice == "" || $scope.returnGridData.ARPrice == undefined) {
                $scope.gridData.ARPrice = $scope.defaultData.arPrice;
            }
            else { $scope.gridData.ARPrice = $scope.returnGridData.ARPrice; }


            if ($scope.returnGridData.ARDiscount == "" || $scope.returnGridData.ARDiscount == undefined) {
                $scope.gridData.ARDiscount = $scope.defaultData.arDiscount;
            }
            else { $scope.gridData.ARDiscount = $scope.returnGridData.ARDiscount; }


            if ($scope.returnGridData.ARDiscRate == "" || $scope.returnGridData.ARDiscRate == undefined) {
                $scope.gridData.ARDiscRate = $scope.defaultData.arDiscRate;
            }
            else { $scope.gridData.ARDiscRate = $scope.returnGridData.ARDiscRate; }


            if ($scope.returnGridData.ARTotal == "" || $scope.returnGridData.ARTotal == undefined) {
                $scope.gridData.ARTotal = $scope.defaultData.arTotal;
            }
            else { $scope.gridData.ARTotal = $scope.returnGridData.ARTotal; }


            if ($scope.returnGridData.ARClearnessRate == "" || $scope.returnGridData.ARClearnessRate == undefined) {
                $scope.gridData.ARClearnessRate = $scope.defaultData.arClearnessRate;
            }
            else { $scope.gridData.ARClearnessRate = $scope.returnGridData.ARClearnessRate; }


            if ($scope.returnGridData.ARCaliber24 == "" || $scope.returnGridData.ARCaliber24 == undefined) {
                $scope.gridData.ARCaliber24 = $scope.defaultData.arCaliber24;
            }
            else { $scope.gridData.ARCaliber24 = $scope.returnGridData.ARCaliber24; }


            if ($scope.returnGridData.ARCaliber22 == "" || $scope.returnGridData.ARCaliber22 == undefined) {
                $scope.gridData.ARCaliber22 = $scope.defaultData.arCaliber22;
            }
            else { $scope.gridData.ARCaliber22 = $scope.returnGridData.ARCaliber22; }


            if ($scope.returnGridData.ARCaliber21 == "" || $scope.returnGridData.ARCaliber21 == undefined) {
                $scope.gridData.ARCaliber21 = $scope.defaultData.arCaliber22;
            }
            else { $scope.gridData.ARCaliber21 = $scope.returnGridData.ARCaliber21; }


            if ($scope.returnGridData.ARCaliber18 == "" || $scope.returnGridData.ARCaliber18 == undefined) {
                $scope.gridData.ARCaliber18 = $scope.defaultData.arCaliber18;
            }
            else { $scope.gridData.ARCaliber18 = $scope.returnGridData.ARCaliber18; }


            if ($scope.returnGridData.ARCostCenter == "" || $scope.returnGridData.ARCostCenter == undefined) {
                $scope.gridData.ARCostCenter = $scope.defaultData.arCostCenter;
            }
            else { $scope.gridData.ARCostCenter = $scope.returnGridData.ARCostCenter; }


            if ($scope.returnGridData.ARSubjectToVat == "" || $scope.returnGridData.ARSubjectToVat == undefined) {
                $scope.gridData.ARSubjectToVat = $scope.defaultData.arSubjectToVat;
            }
            else { $scope.gridData.ARSubjectToVat = $scope.returnGridData.ARSubjectToVat; }


            if ($scope.returnGridData.ARVatRate == "" || $scope.returnGridData.ARVatRate == undefined) {
                $scope.gridData.ARVatRate = $scope.defaultData.arVatRate;
            }
            else { $scope.gridData.ARVatRate = $scope.returnGridData.ARVatRate; }


            if ($scope.returnGridData.ARVatValue == "" || $scope.returnGridData.ARVatValue == undefined) {
                $scope.gridData.ARVatValue = $scope.defaultData.arVatValue;
            }
            else { $scope.gridData.ARVatValue = $scope.returnGridData.ARVatValue; }



            if ($scope.returnGridData.ARWagesDiscValue == "" || $scope.returnGridData.ARWagesDiscValue == undefined) {
                $scope.gridData.ARWagesDiscValue = $scope.defaultData.arWagesDiscValue;
            }
            else { $scope.gridData.ARWagesDiscValue = $scope.returnGridData.ARWagesDiscValue; }

            if ($scope.returnGridData.ARWagesDiscRate == "" || $scope.returnGridData.ARWagesDiscRate == undefined) {
                $scope.gridData.ARWagesDiscRate = $scope.defaultData.arWagesDiscRate;
            }
            else { $scope.gridData.ARWagesDiscRate = $scope.returnGridData.ARWagesDiscRate; }


            if ($scope.returnGridData.ARActualWeight == "" || $scope.returnGridData.ARActualWeight == undefined) {
                $scope.gridData.ARActualWeight = $scope.defaultData.arActualWeight;
            }
            else { $scope.gridData.ARActualWeight = $scope.returnGridData.ARActualWeight; }



            if ($scope.returnGridData.ARTaxTotal == "" || $scope.returnGridData.ARTaxTotal == undefined) {
                $scope.gridData.ARTaxTotal = $scope.defaultData.arTaxTotal;
            }
            else { $scope.gridData.ARTaxTotal = $scope.returnGridData.ARTaxTotal; }


            if ($scope.returnGridData.ARTotalItemGmWages == "" || $scope.returnGridData.ARTotalItemGmWages == undefined) {
                $scope.gridData.ARTotalItemGmWages = $scope.defaultData.arTotalItemGmWages;
            }
            else { $scope.gridData.ARTotalItemGmWages = $scope.returnGridData.ARTotalItemGmWages; }


            if ($scope.returnGridData.ARTotalGoldManufact == "" || $scope.returnGridData.ARTotalGoldManufact == undefined) {
                $scope.gridData.ARTotalGoldManufact = $scope.defaultData.arTotalGoldManufact;
            }
            else { $scope.gridData.ARTotalGoldManufact = $scope.returnGridData.ARTotalGoldManufact; }


            if ($scope.returnGridData.ARExemptOfTax == "" || $scope.returnGridData.ARExemptOfTax == undefined) {
                $scope.gridData.ARExemptOfTax = $scope.defaultData.arExemptOfTax;
            }
            else { $scope.gridData.ARExemptOfTax = $scope.returnGridData.ARExemptOfTax; }


            if ($scope.returnGridData.ARItemPrice == "" || $scope.returnGridData.ARItemPrice == undefined) {
                $scope.gridData.ARItemPrice = $scope.defaultData.arItemPrice;
            }
            else { $scope.gridData.ARItemPrice = $scope.returnGridData.ARItemPrice; }


            if ($scope.returnGridData.ARAfterDiscount == "" || $scope.returnGridData.ARAfterDiscount == undefined) {
                $scope.gridData.ARAfterDiscount = $scope.defaultData.arAfterDiscount;
            }
            else { $scope.gridData.ARAfterDiscount = $scope.returnGridData.ARAfterDiscount; }

            if ($scope.returnGridData.ARLockPrice == "" || $scope.returnGridData.ARLockPrice == undefined) {
                $scope.gridData.ARLockPrice = $scope.defaultData.arLockPrice;
            }
            else { $scope.gridData.ARLockPrice = $scope.returnGridData.ARLockPrice; }

        });
    }

    //////////////////////////////insert user menu //////////////////////////////////////////////////////////////
    $scope.addUserMenu = function () {
        $scope.userMenu.ARName = $scope.billSetting.BILL_AR_NAME;
        $scope.userMenu.LatName = $scope.billSetting.BILL_EN_NAME;
        // $scope.userMenu.MenuLink = "#/PointOfSale?billType="+
        $scope.userMenu.MenuID = 1;

    }
    ///////////////////////////////////////search bill with bill number/////////////////////////////////
    $scope.searchBill = function () {
        
        var billSetting = ($location.search()).billType;
        PointOfSaleService.getBillByBillNumber($scope.billMaster.BILL_NUMBER, billSetting).then(function (result) {
            var billMaster = result.data;


            $scope.selectedItems = [];

            if (billMaster != null) {
                billMaster.BILL_DATE = new Date(billMaster.BILL_DATE);
                billMaster.BILL_NUMBER = Number(billMaster.BILL_NUMBER);
                billMaster.BILL_PAY_WAY = Number(billMaster.BILL_PAY_WAY);
                billMaster.SHIFT_NUMBER = Number(billMaster.SHIFT_NUMBER);
                billMaster.BILL_ID = Number(billMaster.BILL_ID);
                billMaster.BILL_TOTAL_DISC = Number(billMaster.BILL_TOTAL_DISC);
                billMaster.BILL_TOTAL_EXTRA = Number(billMaster.BILL_TOTAL_EXTRA);
                billMaster.ENTRY_ID = billMaster.ENTRY_ID;

                $scope.getAllBillDetails(billMaster.BILL_ID);

                $scope.billMaster = billMaster;
                $scope.operation = "Update";

                $scope.isEditReason = false;
                if ($scope.IsShowEditReason != undefined && $scope.IsShowEditReason != null) {
                    $scope.isEditReason = $scope.IsShowEditReason;
                }

                $scope.billMaster.totalMustPaid = Number(billMaster.BILL_TOTAL);
                $scope.billMaster.TotalPaid = Number(billMaster.TotalPaid);
                $scope.getTotalQuantity();
                $scope.getPayTypesByID(billMaster.BILL_ID);
                //$scope.getCaliberTransByMasterID(entity.BILL_ID);
                $scope.getTransactionsByID(billMaster.BILL_ID);
                //  $scope.matrixTotals();

                $scope.getGoldTotalsByBillID(billMaster.BILL_ID);

                $scope.billMaster.TotalVatRate = billMaster.TotalVatRate;
                $scope.billMaster.TotalVatValue = billMaster.TotalVatValue;
            }

            document.getElementById("BILL_DATETxt").focus();
            // $scope.IsDateFocus = true;
        });
    }



    $scope.checkExistbillNumber = function () {
        
        var billSetting = ($location.search()).billType;
        return PointOfSaleService.checkBillByBillNumber($scope.billMaster.BILL_NUMBER, billSetting);

    }
    ///////////get name

    $scope.getPayName = function () {
        if ($scope.billMaster.PAY_ACC_ID != null && $scope.billMaster.PAY_ACC_ID > 0) {
            for (var i = 0; i < $scope.BoxAccounts.length; i++) {
                if ($scope.BoxAccounts[i].ACC_ID == $scope.billMaster.PAY_ACC_ID) {
                    return $scope.BoxAccounts[i].ACC_AR_NAME;
                }
            }
        }
    }


    /////////////////////get setting type from query string 
    //$scope.getBillSettingID = function () {
    //    $scope.billSettingID = $location.search().billType;
    //}

    /////////////////////////////pagniation////////////////////////////////////////////////////////////
    $scope.getPagingNums = function () {
        $scope.settingID = $location.search().billType;
        PointOfSaleService.getPaginationByType($scope.settingID).then(function (result) {
            $scope.numOfPages = result.data;
        })
    }
    /////////////////////////////////////////////////////////searching bills /////////////////////////////////////////

    $scope.openBillSearchPopUp = function () {
        var parentElem = angular.element($document[0].querySelector('.app-content'));
        $scope.modalOpenItm = false;
        var modalInstance = $uibModal.open({
            templateUrl: 'searchBillModalContent.html',
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


    $scope.getAllBillsOfSearch = function () {
        PointOfSaleService.getAllBillsBySettingID($scope.billSettingID).then(function (result) {
            $scope.searchBillList = result.data;
            $scope.openBillSearchPopUp();
        });
    }


    $scope.getbillsForSearch = function () {
        $scope.billSearch.SettingID = $scope.billSettingID;
        PointOfSaleService.getBillsForSearch($scope.billSearch).then(function (result) {
            $scope.searchBillList = result.data;
            // $scope.openBillSearchPopUp();
        })
    }


    $scope.checkSelectedBills = function () {
        var count = 0;
        var billNum;
        for (var i = 0; i < $scope.searchBillList.length; i++) {
            if ($scope.searchBillList[i].Selected == true) {
                count++;
                billNum = $scope.searchBillList[i].BILL_NUMBER;
            }
        }

        if (count > 1) {
            swal('عفواً',
                'لا يمكن اختيار اكتر من فاتوره',
                'error');
        }
        else if (count == 0) {
            swal('عفواً',
                'يجب اختيار فاتوره',
                'error');
        }
        else if (count == 1) {
            PointOfSaleService.getBillByBillNumber(billNum, $scope.billSettingID).then(function (result) {
                var billMaster = result.data;
                billMaster.BILL_DATE = new Date(billMaster.BILL_DATE);
                billMaster.BILL_NUMBER = Number(billMaster.BILL_NUMBER);
                billMaster.BILL_PAY_WAY = Number(billMaster.BILL_PAY_WAY);
                billMaster.SHIFT_NUMBER = Number(billMaster.SHIFT_NUMBER);
                billMaster.BILL_ID = Number(billMaster.BILL_ID);
                billMaster.BILL_TOTAL_DISC = Number(billMaster.BILL_TOTAL_DISC);
                billMaster.BILL_TOTAL_EXTRA = Number(billMaster.BILL_TOTAL_EXTRA);
                billMaster.ENTRY_ID = billMaster.ENTRY_ID;
                $scope.operation = "Update";
                $scope.billMaster = billMaster;

                $scope.getAllBillDetails(billMaster.BILL_NUMBER);

                $scope.billSearch = {};
            })
        }
    }

    /////////////////////////////////////bill pay types//////////////////////////////////////////////////////////////////

    $scope.getDefaultBillPayTypes = function () {
        var val = 0;
        var index = 0;
        var indexVal = 0;
        for (var i = 0; i < $scope.payTypesList.length; i++) {

            if ($scope.payTypesList[i].PayTypeValue != undefined) {
                val = (parseFloat(val) + parseFloat($scope.payTypesList[i].PayTypeValue)).toFixed(2);
            }

            if ($scope.payTypesList[i].PayTypeID == $scope.billMaster.BILL_PAY_WAY) {
                index = $scope.payTypesList.findIndex(x => x.PayTypeID === $scope.billMaster.BILL_PAY_WAY);
            }

        }

        if ($scope.payTypesList.length > 0) {
            debugger;
            if ($scope.payTypesList[index].PayTypeValue != undefined) {
                indexVal = $scope.payTypesList[index].PayTypeValue;
                val = (parseFloat(val) - parseFloat(indexVal)).toFixed(2);
            }


            $scope.payTypesList[index].PayTypeValue = (parseFloat($scope.billMaster.totalMustPaid) - parseFloat(val)).toFixed(2);
            $scope.payTypesList[index].isReadOnly = true;
            var settingID = $scope.getBillTypeQueryString();
            if (settingID == 80 && index == 0) {
                if ($scope.footerTaxTotal == 0 && $scope.selectedItems.length == 1) {
                    swal('عفواً',
                        'لا يوجد اصناف ',
                        'error');

                }
                else if (parseFloat($scope.billMaster.DiscountAmount) > parseFloat($scope.payTypesList[index].PayTypeValue)) {
                    swal('عفواً',
                        'قيمة الخصم اكبر من اجمالى الفاتورة',
                        'error');

                }

                else if ($scope.billMaster.DiscountAmount != "" && $scope.billMaster.DiscountAmount != undefined && $scope.billMaster.DiscountAmount != undefined) {

                    $scope.payTypesList[index].PayTypeValue = (parseFloat($scope.payTypesList[index].PayTypeValue) - parseFloat($scope.billMaster.DiscountAmount)).toFixed(2);
                }

            }


            $scope.valuesCalc($scope.payTypesList[index].PayTypeValue, index);
        }

        //payTypesService.getAllActive().then(function (result) {
        //    var returnPayTypes = result.data;

        //    for (var i = 0; i < returnPayTypes.length; i++) {
        //        if ($scope.billMaster.BILL_PAY_WAY == returnPayTypes[i].PAY_TYPE_ID) {
        //            $scope.payTypesList[index].AccountID = returnPayTypes[i].AccountID;
        //        }
        //    }
        //});
    }

    ///////////////insert matrix 
    $scope.insertMatrixList = function (transList, billMasterID) {
        //billCaliberTransactionsService.insert($scope.matrixList[i]);
        return billCaliberTransactionsService.insertWithMasterID(transList, billMasterID);
    }


    ///////////////////////bill pay types
    $scope.insertBillPayType = function (payTypeList, billMasterID) {
        return billPayTypesService.insertWithMasterID(payTypeList, billMasterID);
    }

    ///// values of pay types 
    $scope.valuesCalc = function (data, index) {

        $scope.totalPayTypeValue = 0;

        $scope.payTypesList[index].PayTypeValue = data
        $scope.calcCommissionOfPayTypes(index);
        var total = 0;
        var thisIndex = 0;
        for (var i = 0; i < $scope.payTypesList.length; i++) {

            if ($scope.payTypesList[i].PayTypeID == $scope.billMaster.BILL_PAY_WAY) {
                thisIndex = $scope.payTypesList.findIndex(x => x.PayTypeID === $scope.billMaster.BILL_PAY_WAY);
            }
        }

        if (data > 0) {

            if (index == thisIndex) {
                $scope.payTypesList[thisIndex].PayTypeValue = data;
                $scope.calcCommissionOfPayTypes(thisIndex);
            }

            else {
                //calculate totals of pay types values 
                for (var i = 0; i < $scope.payTypesList.length; i++) {
                    if (i != thisIndex) {
                        if ($scope.payTypesList[i].PayTypeValue != undefined && $scope.payTypesList[i].PayTypeValue != null) {
                            $scope.totalPayTypeValue = parseFloat($scope.payTypesList[i].PayTypeValue) + parseFloat($scope.totalPayTypeValue);
                        }
                    }
                }

                // check if total values more than must paid and prevent it 
                if ($scope.totalPayTypeValue > $scope.billMaster.totalMustPaid) {
                    $scope.payTypesList[index].PayTypeValue = 0;
                    $scope.payTypesList[index].BankCommissionValue = 0;
                    $scope.payTypesList[index].CommissionTaxValue = 0;
                    $scope.payTypesList[index].AccountID = null;

                    swal({
                        title: "القيمه اكبر من المبلغ المستحق دفعه",
                        type: "warning",
                        showCancelButton: true,
                        confirmButtonColor: "#DD6B55",
                        confirmButtonText: "ok",
                        //cancelButtonText: "لا",
                        closeOnConfirm: false,
                        closeOnCancel: false
                    }

                    ).then(function () {

                    });

                    ////make value to be zero if more than must paid
                    $scope.payTypesList[index].PayTypeValue = 0;
                    $scope.payTypesList[index].BankCommissionValue = 0;
                    $scope.payTypesList[index].CommissionTaxValue = 0;
                    $scope.payTypesList[index].AccountID = null;
                    $scope.totalPayTypeValueSwal = 0;


                    //calculate all totals after put zero in the more than value
                    for (var i = 0; i < $scope.payTypesList.length; i++) {
                        if (i != thisIndex) {
                            if ($scope.payTypesList[i].PayTypeValue != undefined && $scope.payTypesList[i].PayTypeValue != null) {
                                $scope.totalPayTypeValueSwal = parseFloat($scope.payTypesList[i].PayTypeValue) + parseFloat($scope.totalPayTypeValueSwal);
                            }
                        }
                    }

                    // set the default pay type to the remaining of data
                    var valOfDefault = (parseFloat($scope.billMaster.totalMustPaid) - parseFloat($scope.totalPayTypeValueSwal)).toFixed(2);
                    if (valOfDefault >= 0) {
                        $scope.payTypesList[thisIndex].PayTypeValue = valOfDefault;
                    }
                    else {
                        $scope.payTypesList[thisIndex].PayTypeValue = parseFloat(valOfDefault) * -1;
                    }
                }

                else {
                    var valOfDefault = (parseFloat($scope.billMaster.totalMustPaid) - parseFloat($scope.totalPayTypeValue)).toFixed(2);
                    if (valOfDefault >= 0) {
                        $scope.payTypesList[thisIndex].PayTypeValue = valOfDefault;
                    }
                }
            }

        }

        else {
            $scope.payTypesList[index].PayTypeValue = data;
            $scope.totalPayTypeValue = 0;
            for (var i = 0; i < $scope.payTypesList.length; i++) {
                if ($scope.payTypesList[i].PayTypeValue != undefined && $scope.payTypesList[i].PayTypeValue != null) {
                    $scope.totalPayTypeValue = parseFloat($scope.payTypesList[i].PayTypeValue) + parseFloat($scope.totalPayTypeValue);
                }
            }

            var valOfDefault = (parseFloat($scope.billMaster.totalMustPaid) - parseFloat($scope.totalPayTypeValue)).toFixed(2);

            if (valOfDefault >= 0) {
                $scope.payTypesList[thisIndex].PayTypeValue = parseFloat($scope.payTypesList[thisIndex].PayTypeValue) + parseFloat(valOfDefault);
            }
            else {
                $scope.payTypesList[thisIndex].PayTypeValue = parseFloat($scope.payTypesList[thisIndex].PayTypeValue) + (parseFloat(valOfDefault) * -1);
            }


            $scope.billMaster.totalRemaining = (parseFloat($scope.billMaster.totalMustPaid) - parseFloat($scope.totalPayTypeValue)).toFixed(2);
            $scope.payTypesList[index].PayTypeValue = 0;
            $scope.payTypesList[index].BankCommissionValue = 0;
            $scope.payTypesList[index].CommissionTaxValue = 0;
            //$scope.payTypesList[index].AccountID = null;
        }
    }


    $scope.calcCommissionOfPayTypes = function (index) {
        var commValue = (parseFloat($scope.payTypesList[index].PayTypeValue) * (parseFloat($scope.payTypesList[index].BankCommissionRate) / 100)).toFixed(2);
        if ($scope.payTypesList[index].MaxCommission != 0) {

            if (commValue > $scope.payTypesList[index].MaxCommission) {
                $scope.payTypesList[index].BankCommissionValue = $scope.payTypesList[index].MaxCommission;
            }
            else {
                $scope.payTypesList[index].BankCommissionValue = commValue;
            }

            if ($scope.payTypesList[index].BankCommissionValue != 0) {
                $scope.payTypesList[index].CommissionTaxValue = (parseFloat($scope.payTypesList[index].BankCommissionValue) * (parseFloat($scope.payTypesList[index].CommissionTaxRate / 100))).toFixed(2);
            }
        }

        else {
            $scope.payTypesList[index].BankCommissionValue = commValue;
            if ($scope.payTypesList[index].BankCommissionValue != 0) {
                $scope.payTypesList[index].CommissionTaxValue = (parseFloat($scope.payTypesList[index].BankCommissionValue) * (parseFloat($scope.payTypesList[index].CommissionTaxRate / 100))).toFixed(2);
            }
        }

    }



    $scope.changePayTypeID = function () {
        var settingID = $scope.getBillTypeQueryString();
        var billType = 0;
        billSettingService.getByID(settingID).then(function (result) {
            billType = result.data.BILL_TYPE_ID;
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
        });


        //  var val = 0;
        var index = 0;
        //  var indexVal = 0;
        for (var i = 0; i < $scope.payTypesList.length; i++) {
            if ($scope.payTypesList[i].PayTypeID == $scope.billMaster.BILL_PAY_WAY) {
                index = $scope.payTypesList.findIndex(x => x.PayTypeID === $scope.billMaster.BILL_PAY_WAY);
            }
        }


        $scope.payTypesList[index].PayTypeValue = $scope.billMaster.totalMustPaid;
        // $scope.valuesCalc($scope.payTypesList[0].PayTypeValue, 0);
        $scope.calcCommissionOfPayTypes(index);
        //  $scope.payTypesList[0].AccountID = $scope.cashAccountID;

        for (var i = 0; i < $scope.payTypesList.length; i++) {

            if ($scope.payTypesList[i].PayTypeID != $scope.billMaster.BILL_PAY_WAY) {
                $scope.payTypesList[i].PayTypeValue = 0;
                $scope.payTypesList[i].BankCommissionValue = 0;
                $scope.payTypesList[i].CommissionTaxValue = 0;
                $scope.payTypesList[i].AccountID = null;
            }
        }

    }


    //////////////////////
    $scope.changePriceByType = function () {
        goldWorldPriceService.GetLastGoldWorldPrice().then(function (result) {
            var lastPrice = result.data;

            for (var i = 0; i < $scope.selectedItems.length; i++) {
                if ($scope.selectedItems[i].ITEM_CODE != null &&
                    $scope.selectedItems[i].ITEM_CODE != "" &&
                    $scope.selectedItems[i].ITEM_CODE != undefined) {
                    var trans = 1;
                    var gemsWeight = 1;
                    var gemsWages = 1;
                    var itmWeight = 1;
                    var gemCalc = 0;

                    if ($scope.selectedItems[i].CaliberID == 2) {

                        trans = parseFloat(22 / 24);
                    } else if ($scope.selectedItems[i].CaliberID == 3) {

                        trans = parseFloat(21 / 24);
                    } else if ($scope.selectedItems[i].CaliberID == 4) {

                        trans = parseFloat(18 / 24);
                    }


                    if ($scope.selectedItems[i].GemsWeight != null &&
                        $scope.selectedItems[i].GemsWeight != 0 &&
                        $scope.selectedItems[i].GemsWeight != undefined) {
                        gemsWeight = $scope.selectedItems[i].GemsWeight;
                    }

                    if ($scope.selectedItems[i].GemsWages != null &&
                        $scope.selectedItems[i].GemsWages != 0 &&
                        $scope.selectedItems[i].GemsWages != undefined) {
                        gemsWages = $scope.selectedItems[i].GemsWages;
                    }


                    if ($scope.selectedItems[i].ItemWeight != null &&
                        $scope.selectedItems[i].ItemWeight != 0 &&
                        $scope.selectedItems[i].ItemWeight != undefined) {
                        itmWeight = $scope.selectedItems[i].ItemWeight;
                    }

                    gemCalc = parseFloat(gemsWages) * parseFloat(gemsWeight);
                    if (gemCalc == 1) {
                        gemCalc = 0;
                    }

                    if ($scope.ShowPriceTypeID == 2) {
                        var manfuctringWages = 0;
                        var profit = 0;
                        
                        var goldPrice = (parseFloat(lastPrice) * parseFloat(trans)).toFixed(2);
                        // alert($scope.selectedItems[i].ManufacturingWages);
                        if ($scope.selectedItems[i].itemGmWages != null &&
                            $scope.selectedItems[i].itemGmWages != undefined) {
                            manfuctringWages = parseFloat($scope.selectedItems[i].itemGmWages);
                        }

                        if ($scope.selectedItems[i].ProfitMargin != undefined &&
                            $scope.selectedItems[i].ProfitMargin != null) {
                            profit =
                                parseFloat($scope.selectedItems[i].ProfitMargin)
                                .toFixed(
                                    2) //* $scope.selectedItems[i].Price * parseFloat($scope.selectedItems[i].ItemWeight);
                        }

                        // alert(goldPrice);
                        //  alert(manfuctringWages);

                        // alert(profit);
                        
                        $scope.selectedItems[i].GmPrice =
                            (parseFloat(goldPrice) + parseFloat(manfuctringWages) + parseFloat(profit)).toFixed(2);
                        $scope.selectedItems[i].Price =
                            (($scope.selectedItems[i].GmPrice * parseFloat(itmWeight)) + parseFloat(gemCalc))
                            .toFixed(2);
                      

                        //$scope.selectedItems[i].TotalGoldManfact = (parseFloat($scope.selectedItems[i].Total)).toFixed(2);
                    } else {
                        
                        $scope.selectedItems[i].Price =
                        ((parseFloat(lastPrice) * parseFloat(trans) * parseFloat(itmWeight)) +
                            parseFloat(gemCalc)).toFixed(2);
                        $scope.selectedItems[i].GmPrice =
                            (parseFloat($scope.selectedItems[i].Price) / parseFloat(itmWeight)).toFixed(2);

                        // $scope.selectedItems[i].TotalGoldManfact = (parseFloat($scope.selectedItems[i].TotalItemGmWages) + parseFloat($scope.selectedItems[i].Total)).toFixed(2);
                    }
                    
                    if ($scope.IsQuantityCalculated == true) {
                        $scope.Quantity = $scope.selectedItems[i].Quantity;
                    } else {
                        $scope.Quantity = 1;
                    }

                    $scope.selectedItems[i].Total =
                        (parseFloat($scope.selectedItems[i].Price) * parseFloat($scope.Quantity)).toFixed(2);

                    $scope.selectedItems[i].NetTotal = (parseFloat($scope.selectedItems[i].Total) -
                        parseFloat($scope.selectedItems[i].DISC_VALUE)).toFixed(2);
                    
                    if ($scope.ShowPriceTypeID == 2) {
                        $scope.selectedItems[i].TotalGoldManfact =
                            (parseFloat($scope.selectedItems[i].NetTotal)).toFixed(2);

                        if ($scope.selectedItems[i].WagesDiscValue == undefined ||
                            $scope.selectedItems[i].WagesDiscValue == null ||
                            $scope.selectedItems[i].WagesDiscValue == "NaN") {
                            $scope.selectedItems[i].WagesDiscValue = 0;
                        }
                    } else {
                        

                        if ($scope.selectedItems[i].WagesDiscValue == undefined ||
                            $scope.selectedItems[i].WagesDiscValue == null || $scope.selectedItems[i].WagesDiscValue == "NaN" ) {
                            $scope.selectedItems[i].WagesDiscValue = 0;
                        }

                        $scope.selectedItems[i].TotalGoldManfact =
                        ((parseFloat($scope.selectedItems[i].TotalItemGmWages) -
                                parseFloat($scope.selectedItems[i].WagesDiscValue)) +
                            parseFloat($scope.selectedItems[i].NetTotal)).toFixed(2);
                    }


                    ////var netWages = 0;
                    ////if ($scope.wages_taxValue != undefined && $scope.wages_taxValue != null) {
                    ////    netWages = ((parseFloat($scope.selectedItems[i].TotalItemGmWages) - parseFloat($scope.selectedItems[i].WagesDiscValue)) * $scope.wages_taxValue) / 100;
                    ////}
                    ////var netVat = 0;
                    ////if ($scope.selectedItems[i].VatValue != undefined && $scope.selectedItems[i].VatValue != null) {
                    ////    netVat = $scope.selectedItems[i].VatValue;
                    ////}

                    ////$scope.selectedItems[i].TaxTotal = (parseFloat($scope.selectedItems[i].TotalGoldManfact) + parseFloat(netWages) + parseFloat(netVat)).toFixed(2);

                }
            }

            $scope.calculateTotals();
            //  $scope.getTotalQuantity();

        });
    }

    $scope.goldCredit = function (data, no) {
        if (data != null) {
            $scope.moreInfoGoldBalance(data);
        }
    }

    $scope.moreInfoGoldBalance = function (data) {
        
        commonService.getAccountValByaccID(data).then(function (results) {
            var x = results;
        }, function (error) {

            var y = results;
        });
    }


    /////////////////////////balance methods
    $scope.changeGoldBalance = function (id) {
        commonService.getAccountBalanceByID(id).then(function (results) {
            $scope.goldBalance = results;
        });
    }


    $scope.changeCustBalance = function (id) {
        commonService.getAccountBalanceByID(id).then(function (results) {
            $scope.custBalance = results;
        });
    }


    $scope.changePayBalance = function (id) {
        commonService.getAccountBalanceByID(id).then(function (results) {
            $scope.payBalance = results;
        });
    }


    //get calibers 

    $scope.getClearnessRate = function () {
        itemCaliberService.getAll().then(function (results) {
            $scope.Clearness = results.data;
            $scope.rate24 = results.data[0].ClearnessRate;
            $scope.rate22 = results.data[1].ClearnessRate;
            $scope.rate21 = results.data[2].ClearnessRate;
            $scope.rate18 = results.data[3].ClearnessRate;
        });
    }

    ////////////all item events of grids
    $scope.itemEvents = function (cellName, data, index) {
        if (!data) {
            data = 0;
        }
        debugger;
        if ($scope.IsQuantityCalculated == true) {
            $scope.Quantity = $scope.selectedItems[index].Quantity;
        }
        else {
            $scope.Quantity = 1;
        }
        
        if (cellName == 'Discount') {
            if (data == null || data == undefined) {
                data = 0;
            }
            else {

                if ($scope.selectedItems[index].DISC_VALUE == data) {
                    return false;
                }

                if (data < 0) {
                    data = 0;
                } else {

                    $scope.selectedItems[index].DISC_VALUE = data;

                    if ($scope.bill_type_Id == 23 || $scope.bill_type_Id == 24) {
                        $scope.selectedItems[index].DISC_RATE = (parseFloat($scope.selectedItems[index].DISC_VALUE * 100) / (parseFloat($scope.Quantity) * parseFloat($scope.selectedItems[index].ManufacturingWages))).toFixed(2);
                    }
                    else {
                        $scope.selectedItems[index].DISC_RATE = (parseFloat($scope.selectedItems[index].DISC_VALUE * 100) / parseFloat($scope.selectedItems[index].Total)).toFixed(2);

                    }
                }
            }



            $scope.selectedItems[index].Total = (parseFloat($scope.selectedItems[index].Price) * parseFloat($scope.Quantity)).toFixed(2);

            $scope.selectedItems[index].NetTotal = (parseFloat($scope.selectedItems[index].Total) - parseFloat($scope.selectedItems[index].DISC_VALUE)).toFixed(2);
            $scope.selectedItems[index].TotalGoldManfact = (parseFloat($scope.selectedItems[index].NetTotal) + (parseFloat($scope.selectedItems[index].TotalItemGmWages) - parseFloat($scope.selectedItems[index].WagesDiscValue))).toFixed(2);

            $scope.calculateTotals();
        }

        else if (cellName == 'DISC_RATE') {

            if (data > 0 && data <= 100) {

                if ($scope.selectedItems[index].DISC_RATE == data) {
                    return false;
                }

                $scope.selectedItems[index].DISC_VALUE = 0;
                $scope.selectedItems[index].DISC_RATE = data;
                if ($scope.bill_type_Id == 23 || $scope.bill_type_Id == 24) {
                    $scope.selectedItems[index].DISC_VALUE = ((parseFloat($scope.selectedItems[index].DISC_RATE) / 100) * (parseFloat($scope.Quantity) * parseFloat($scope.selectedItems[index].ManufacturingWages))).toFixed(2);
                }
                else {
                    $scope.selectedItems[index].DISC_VALUE = ((parseFloat($scope.selectedItems[index].DISC_RATE) / 100) * (parseFloat($scope.selectedItems[index].Total))).toFixed(2);
                }

            } else {
                if ($scope.bill_type_Id == 23 || $scope.bill_type_Id == 24) {
                    $scope.selectedItems[index].DISC_VALUE = (parseFloat($scope.selectedItems[index].DISC_RATE) * (parseFloat($scope.Quantity) * parseFloat($scope.selectedItems[index].ManufacturingWages))).toFixed(2);
                }
                else {
                    $scope.selectedItems[index].VatValue = ((parseFloat($scope.selectedItems[index].VATRate) / 100) * (parseFloat(parseFloat($scope.selectedItems[index].Total) - parseFloat($scope.selectedItems[index].DISC_VALUE)))).toFixed(2);
                }

            }

            $scope.selectedItems[index].Total = (parseFloat($scope.selectedItems[index].Price) * parseFloat($scope.Quantity)).toFixed(2);

            $scope.selectedItems[index].NetTotal = (parseFloat($scope.selectedItems[index].Total) - parseFloat($scope.selectedItems[index].DISC_VALUE)).toFixed(2);
            $scope.selectedItems[index].TotalGoldManfact = (parseFloat($scope.selectedItems[index].NetTotal) + (parseFloat($scope.selectedItems[index].TotalItemGmWages) - parseFloat($scope.selectedItems[index].WagesDiscValue))).toFixed(2);

            $scope.calculateTotals();
        }

        else if (cellName == 'Quantity') {
            //
            if (!data) {
                data = 0;
            }

            if ($scope.selectedItems[index].Quantity == data) {
                return false;
            }

            $scope.selectedItems[index].Quantity = data;

            if ($scope.IsQuantityCalculated == true) {
                $scope.Quantity = data;
            }
            else {
                $scope.Quantity = 1;
            }


            $scope.selectedItems[index].Total = (parseFloat($scope.selectedItems[index].Price) * parseFloat($scope.Quantity)).toFixed(2);
            $scope.selectedItems[index].DISC_VALUE = ((parseFloat($scope.selectedItems[index].DISC_RATE) / 100) * (parseFloat($scope.selectedItems[index].Total))).toFixed(2);

            $scope.selectedItems[index].NetTotal = (parseFloat($scope.selectedItems[index].Total) - parseFloat($scope.selectedItems[index].DISC_VALUE)).toFixed(2);

            $scope.calcgridBillDetailsItemCaliber($scope.selectedItems[index], $scope.Quantity);

            $scope.calculateTotals();

            $scope.getTotalQuantity();

        }
        else if (cellName == 'Price') {

            if (data == null || data == undefined) {
                data = 0;
            }


            if ($scope.selectedItems[index].Price == data) {
                return false;
            }
            $scope.selectedItems[index].Price = data;
            $scope.calculateTotals();
        }

        if (cellName == 'gm') {
            if ($scope.selectedItems[index].itemGmWages == data) {
                return false;
            }
            $scope.selectedItems[index].itemGmWages = data;

            $scope.selectedItems[index].TotalItemGmWages = (parseFloat($scope.selectedItems[index].ActualItemWeight) * parseFloat($scope.selectedItems[index].itemGmWages)).toFixed(2);


            //// new edit for open weight
            $scope.selectedItems[index].ManufacturingWages = (parseFloat($scope.selectedItems[index].ItemWeight) * parseFloat($scope.selectedItems[index].itemGmWages)).toFixed(2);

            if ($scope.ShowPriceTypeID == 2) {
                $scope.selectedItems[index].TotalGoldManfact = (parseFloat($scope.selectedItems[index].NetTotal)).toFixed(2);
            }
            else {
                $scope.selectedItems[index].TotalGoldManfact = ((parseFloat($scope.selectedItems[index].TotalItemGmWages) - parseFloat($scope.selectedItems[index].WagesDiscValue)) + parseFloat($scope.selectedItems[index].NetTotal)).toFixed(2);
            }

            $scope.calculateTotals();
        }
        else if (cellName == 'man') {

            if ($scope.selectedItems[index].ManufacturingWages == data) {
                return false;

            }

            $scope.selectedItems[index].ManufacturingWages = data;
            $scope.selectedItems[index].itemGmWages = (parseFloat($scope.selectedItems[index].ManufacturingWages) / parseFloat($scope.selectedItems[index].ItemWeight)).toFixed(2);
            $scope.calculateTotals();
        }

        else if (cellName == "CostCenter") {
            if ($scope.selectedItems[index].CostCenterID == data) {
                return false;
            }
            $scope.selectedItems[index].CostCenterID = data;
        }

        else if (cellName == "VatRate") {
             
            if ($scope.selectedItems[index].VATRate == data) {
                return false;
            }
            $scope.selectedItems[index].VATRate = data;

            $scope.selectedItems[index].VatValue = ((parseFloat($scope.selectedItems[index].VATRate) / 100) * parseFloat(parseFloat($scope.selectedItems[index].Total) - parseFloat($scope.selectedItems[index].DISC_RATE))).toFixed(2);
            $scope.calculateTotals();
        }

        else if (cellName == "VatValue") {
             
            if ($scope.selectedItems[index].VatValue == data) {
                return false;
            }
            $scope.selectedItems[index].VatValue = data;

            $scope.selectedItems[index].VATRate = (parseFloat($scope.selectedItems[index].VatValue * 100) / parseFloat(parseFloat($scope.selectedItems[index].Total) - parseFloat($scope.selectedItems[index].DISC_RATE))).toFixed(2);
            $scope.calculateTotals();
        }

        else if (cellName == 'ItemWeight') {
            
            if (!data) {
                data = 0;
            }

            $scope.selectedItems[index].ItemWeight = data;

            if (data > 0) {
                $scope.selectedItems[index].VatValue = ((parseFloat($scope.selectedItems[index].VATRate) / 100) * (parseFloat(parseFloat($scope.selectedItems[index].Total) - parseFloat($scope.selectedItems[index].DISC_RATE)))).toFixed(2);

            }

            if ($scope.selectedItems[index].ItemStatus == 2) {
                $scope.selectedItems[index].itemGmWages = (parseFloat($scope.selectedItems[index].ManufacturingWages) / parseFloat($scope.selectedItems[index].ItemWeight)).toFixed(2);
            }
            else {
                if ($scope.selectedItems[index].itemGmWages != null && $scope.selectedItems[index].itemGmWages != undefined && $scope.selectedItems[index].itemGmWages != 0 && $scope.selectedItems[index].ItemStatus == 3) {
                    $scope.selectedItems[index].ManufacturingWages = (parseFloat($scope.selectedItems[index].ItemWeight) * parseFloat($scope.selectedItems[index].itemGmWages)).toFixed(2);
                }
            }

            $scope.selectedItems[index].Price = (parseFloat($scope.selectedItems[index].GmPrice) * parseFloat($scope.selectedItems[index].ItemWeight)).toFixed(2);
            $scope.selectedItems[index].Total = (parseFloat($scope.selectedItems[index].Price) * parseFloat($scope.Quantity)).toFixed(2);
            $scope.selectedItems[index].DISC_VALUE = ((parseFloat($scope.selectedItems[index].DISC_RATE) / 100) * (parseFloat($scope.selectedItems[index].Total))).toFixed(2);


            $scope.selectedItems[index].NetTotal = (parseFloat($scope.selectedItems[index].Total) - parseFloat($scope.selectedItems[index].DISC_VALUE)).toFixed(2);

            $scope.getTotalQuantity();

            $scope.calculateTotals();

        }

        else if (cellName == 'GmPrice') {
            if (!data) {
                data = 0;
            }

            if ($scope.selectedItems[index].GmPrice == data) {
                return false;
            }

            
            $scope.selectedItems[index].GmPrice = data;
            debugger;
            if (data > 0)
            {
                var BillType = ($location.search()).billType;
                if (BillType != 22 && BillType != 23)
                {
                 
                    $scope.selectedItems[index].Price = (parseFloat($scope.selectedItems[index].GmPrice) * parseFloat($scope.selectedItems[index].ItemWeight)).toFixed(2);
                    $scope.selectedItems[index].Total = (parseFloat($scope.selectedItems[index].Price) * parseFloat($scope.Quantity)).toFixed(2);
                    $scope.selectedItems[index].VatValue = ((parseFloat($scope.selectedItems[index].VATRate) / 100) * (parseFloat(parseFloat($scope.selectedItems[index].Total) - parseFloat($scope.selectedItems[index].DISC_RATE)))).toFixed(2);
                    $scope.selectedItems[index].DISC_VALUE = ((parseFloat($scope.selectedItems[index].DISC_RATE) / 100) * (parseFloat($scope.selectedItems[index].Total))).toFixed(2);


                    $scope.selectedItems[index].NetTotal = (parseFloat($scope.selectedItems[index].Total) - parseFloat($scope.selectedItems[index].DISC_VALUE)).toFixed(2);
                    $scope.selectedItems[index].TotalGoldManfact = (parseFloat($scope.selectedItems[index].NetTotal) + (parseFloat($scope.selectedItems[index].TotalItemGmWages) - parseFloat($scope.selectedItems[index].WagesDiscValue))).toFixed(2);
                }
                else
                {
                    $scope.changePriceByType();
                }
            }




            $scope.calculateTotals();
        }

        else if (cellName == 'LockPrice') {
            if (!data) {
                data = 0;
            }

            if ($scope.selectedItems[index].LockPrice == data) {
                return false;
            }


            $scope.selectedItems[index].LockPrice = data;
            if (($scope.selectedItems[index].CaliberID) == 1) {
                $scope.selectedItems[index].LockPrice = parseFloat($scope.selectedItems[index].LockPrice) / 1000;
            }
            else if (($scope.selectedItems[index].CaliberID) == 2) {

                $scope.selectedItems[index].LockPrice = parseFloat($scope.selectedItems[index].LockPrice) * 22 / 24000;
            }
            else if (($scope.selectedItems[index].CaliberID) == 3) {

                $scope.selectedItems[index].LockPrice = parseFloat($scope.selectedItems[index].LockPrice) * 21 / 24000;
            }
            else if (($scope.selectedItems[index].CaliberID) == 4) {

                $scope.selectedItems[index].LockPrice = parseFloat($scope.selectedItems[index].LockPrice) * 18 / 24000;
            }
            if (data > 0) {

                $scope.selectedItems[index].Price = (parseFloat($scope.selectedItems[index].LockPrice) * parseFloat($scope.selectedItems[index].ItemWeight)).toFixed(2);
                $scope.selectedItems[index].Total = (parseFloat($scope.selectedItems[index].Price) * parseFloat($scope.Quantity)).toFixed(2);
                $scope.selectedItems[index].VatValue = ((parseFloat($scope.selectedItems[index].VATRate) / 100) * (parseFloat(parseFloat($scope.selectedItems[index].Total) - parseFloat($scope.selectedItems[index].DISC_RATE)))).toFixed(2);
                $scope.selectedItems[index].DISC_VALUE = ((parseFloat($scope.selectedItems[index].DISC_RATE) / 100) * (parseFloat($scope.selectedItems[index].Total))).toFixed(2);


                $scope.selectedItems[index].NetTotal = (parseFloat($scope.selectedItems[index].Total) - parseFloat($scope.selectedItems[index].DISC_VALUE)).toFixed(2);
                $scope.selectedItems[index].TotalGoldManfact = (parseFloat($scope.selectedItems[index].NetTotal) + (parseFloat($scope.selectedItems[index].TotalItemGmWages) - parseFloat($scope.selectedItems[index].WagesDiscValue))).toFixed(2);
            }



            $scope.calculateTotals();
        }


        else if (cellName == 'WagesDiscVal') {

            $scope.selectedItems[index].WagesDiscValue = data;

            if ($scope.selectedItems[index].ManufacturingWages != undefined && $scope.selectedItems[index].ManufacturingWages != null) {
                $scope.selectedItems[index].WagesDiscRate = (parseFloat($scope.selectedItems[index].WagesDiscValue * 100) / parseFloat($scope.selectedItems[index].ManufacturingWages)).toFixed(2);
            }


            $scope.selectedItems[index].TotalGoldManfact = (parseFloat($scope.selectedItems[index].NetTotal) + (parseFloat($scope.selectedItems[index].TotalItemGmWages) - parseFloat($scope.selectedItems[index].WagesDiscValue))).toFixed(2);
            $scope.calculateTotals();
        }

        else if (cellName == 'WagesDiscRate') {

            $scope.selectedItems[index].WagesDiscRate = data;

            if ($scope.selectedItems[index].ManufacturingWages != undefined && $scope.selectedItems[index].ManufacturingWages != null) {
                $scope.selectedItems[index].WagesDiscValue = ((parseFloat($scope.selectedItems[index].WagesDiscRate) / 100) * (parseFloat($scope.selectedItems[index].ManufacturingWages))).toFixed(2);
            }


            $scope.selectedItems[index].TotalGoldManfact = (parseFloat($scope.selectedItems[index].NetTotal) + (parseFloat($scope.selectedItems[index].TotalItemGmWages) - parseFloat($scope.selectedItems[index].WagesDiscValue))).toFixed(2);
            $scope.calculateTotals();
        }



        else if (cellName == 'ClearnessRate') {
            if (!data) {
                data = 0;
            }

            if (data > 0) {
                $scope.selectedItems[index].ClearnessRate = data;
            }


            if ($scope.IsCalcClearnessRate) {
                if ($scope.selectedItems[index].ARName == "عيار 24") {
                    $scope.selectedItems[index].ItemWeight = ((parseFloat($scope.selectedItems[index].ClearnessRate) * parseFloat($scope.selectedItems[index].ActualItemWeight)) / (parseFloat($scope.rate24) * parseFloat(1000))).toFixed(2);
                }

                else if ($scope.selectedItems[index].ARName == "عيار 22") {
                    $scope.selectedItems[index].ItemWeight = ((parseFloat($scope.selectedItems[index].ClearnessRate) * parseFloat($scope.selectedItems[index].ActualItemWeight)) / (parseFloat($scope.rate22) * parseFloat(1000))).toFixed(2);
                }

                else if ($scope.selectedItems[index].ARName == "عيار 21") {
                    $scope.selectedItems[index].ItemWeight = ((parseFloat($scope.selectedItems[index].ClearnessRate) * parseFloat($scope.selectedItems[index].ActualItemWeight)) / (parseFloat($scope.rate21) * parseFloat(1000))).toFixed(2);
                }

                else if ($scope.selectedItems[index].ARName == "عيار 18") {
                    $scope.selectedItems[index].ItemWeight = ((parseFloat($scope.selectedItems[index].ClearnessRate) * parseFloat($scope.selectedItems[index].ActualItemWeight)) / (parseFloat($scope.rate18) * parseFloat(1000))).toFixed(2);
                }

                $scope.updateItemListOnChange("ItemWeight", $scope.selectedItems[index].ItemWeight, index);
                //$scope.getTotalQuantity();
                //$scope.calculateTotals();
            }

        }

        else if (cellName == 'SubjectToVAT') {
            //if (!data) {
            //    data = 0;
            //}

            // if (data > 0) {
            if ($scope.selectedItems[index].SubjectToVAT == true) {
                $scope.selectedItems[index].IsExemptOfTax = false;
            }

            var x = $scope.selectedItems[index].SubjectToVAT;

            if ($scope.selectedItems[index].SubjectToVAT == 0) {
                $scope.selectedItems[index].VatValue = 0;
                $scope.selectedItems[index].VATRate = 0;
            }

            $scope.calculateTotals();
        }


        else if (cellName == 'ActualItemWeight') {
            if (data < 0) {
                $scope.selectedItems[index].ActualItemWeight = 0;
            }
            else {
                $scope.selectedItems[index].ActualItemWeight = data;
            }


            //if ($scope.IsCalcClearnessRate) {

            //    var cRate = 1;
            //    if ($scope.selectedItems[index].ClearnessRate != null && $scope.selectedItems[index].ClearnessRate != undefined) {
            //        cRate = $scope.selectedItems[index].ClearnessRate;
            //    }


            //    if ($scope.selectedItems[index].ARName == "عيار 24") {
            //        $scope.selectedItems[index].ItemWeight = ((parseFloat(cRate) * parseFloat($scope.selectedItems[index].ActualItemWeight)) / (parseFloat($scope.rate24) * parseFloat(1000))).toFixed(2);
            //    }

            //    else if ($scope.selectedItems[index].ARName == "عيار 22") {
            //        $scope.selectedItems[index].ItemWeight = ((parseFloat(cRate) * parseFloat($scope.selectedItems[index].ActualItemWeight)) / (parseFloat($scope.rate22) * parseFloat(1000))).toFixed(2);
            //    }

            //    else if ($scope.selectedItems[index].ARName == "عيار 21") {
            //        $scope.selectedItems[index].ItemWeight = ((parseFloat(cRate) * parseFloat($scope.selectedItems[index].ActualItemWeight)) / (parseFloat($scope.rate21) * parseFloat(1000))).toFixed(2);
            //    }

            //    else if ($scope.selectedItems[index].ARName == "عيار 18") {
            //        $scope.selectedItems[index].ItemWeight = ((parseFloat(cRate) * parseFloat($scope.selectedItems[index].ActualItemWeight)) / (parseFloat($scope.rate18) * parseFloat(1000))).toFixed(2);
            //    }

            //}
            //else {
            $scope.selectedItems[index].ItemWeight = $scope.selectedItems[index].ActualItemWeight;
            // }


            $scope.changeWeight(index);
            //edit change weight
            //$scope.updateItemListOnChange("ItemWeight", $scope.selectedItems[index].ItemWeight, index);

            //$scope.selectedItems[index].TotalItemGmWages = (parseFloat($scope.selectedItems[index].ActualItemWeight) * parseFloat($scope.selectedItems[index].itemGmWages)).toFixed(2);


            if ($scope.ShowPriceTypeID == 2) {
                $scope.selectedItems[index].TotalGoldManfact = (parseFloat($scope.selectedItems[index].NetTotal)).toFixed(2);
            }
            else {
                $scope.selectedItems[index].TotalGoldManfact = ((parseFloat($scope.selectedItems[index].TotalItemGmWages) - parseFloat($scope.selectedItems[index].WagesDiscValue)) + parseFloat($scope.selectedItems[index].NetTotal)).toFixed(2);
            }

            $scope.getTotalQuantity();
            $scope.calculateTotals();
        }



        else if (cellName == 'ExemptOfTax') {
            if ($scope.selectedItems[index].IsExemptOfTax == true) {
                $scope.selectedItems[index].SubjectToVAT = false;
                $scope.selectedItems[index].VatValue = 0;
                $scope.selectedItems[index].VATRate = 0;
            }
            $scope.calculateTotals();
        }

    }



    $scope.changeWeight = function (index) {
        var BillType = ($location.search()).billType;
        if (BillType != 22 && BillType != 23) {
            $scope.selectedItems[index].Price = (parseFloat($scope.selectedItems[index].GmPrice) * parseFloat($scope.selectedItems[index].ItemWeight)).toFixed(2);
            $scope.selectedItems[index].Total = (parseFloat($scope.selectedItems[index].Price) * parseFloat($scope.Quantity)).toFixed(2);
            $scope.selectedItems[index].VatValue = ((parseFloat($scope.selectedItems[index].VATRate) / 100) * (parseFloat(parseFloat($scope.selectedItems[index].Total) - parseFloat($scope.selectedItems[index].DISC_RATE)))).toFixed(2);
            $scope.selectedItems[index].DISC_VALUE = ((parseFloat($scope.selectedItems[index].DISC_RATE) / 100) * (parseFloat($scope.selectedItems[index].Total))).toFixed(2);
            $scope.selectedItems[index].NetTotal = (parseFloat($scope.selectedItems[index].Total) - parseFloat($scope.selectedItems[index].DISC_VALUE)).toFixed(2);
            $scope.gridBillDetailsItem.TotalItemGmWages = (parseFloat($scope.selectedItems[index].ItemWeight) * parseFloat($scope.gridBillDetailsItem.itemGmWages)).toFixed(2);
            $scope.selectedItems[index].ManufacturingWages = (parseFloat($scope.selectedItems[index].ItemWeight) * parseFloat($scope.selectedItems[index].itemGmWages)).toFixed(2);
            $scope.selectedItems[index].TotalGoldManfact = (parseFloat($scope.selectedItems[index].NetTotal) + (parseFloat($scope.selectedItems[index].TotalItemGmWages) - parseFloat($scope.selectedItems[index].WagesDiscValue))).toFixed(2);
        }
        else
        {
            $scope.selectedItems[index].Price = (parseFloat($scope.selectedItems[index].LockPrice) * parseFloat($scope.selectedItems[index].ItemWeight)).toFixed(2);
            $scope.selectedItems[index].Total = (parseFloat($scope.selectedItems[index].Price) * parseFloat($scope.Quantity)).toFixed(2);
            $scope.selectedItems[index].VatValue = ((parseFloat($scope.selectedItems[index].VATRate) / 100) * (parseFloat(parseFloat($scope.selectedItems[index].Total) - parseFloat($scope.selectedItems[index].DISC_RATE)))).toFixed(2);
            $scope.selectedItems[index].DISC_VALUE = ((parseFloat($scope.selectedItems[index].DISC_RATE) / 100) * (parseFloat($scope.selectedItems[index].Total))).toFixed(2);
            $scope.selectedItems[index].NetTotal = (parseFloat($scope.selectedItems[index].Total) - parseFloat($scope.selectedItems[index].DISC_VALUE)).toFixed(2);
            $scope.gridBillDetailsItem.TotalItemGmWages = (parseFloat($scope.selectedItems[index].ItemWeight) * parseFloat($scope.gridBillDetailsItem.itemGmWages)).toFixed(2);
            $scope.selectedItems[index].ManufacturingWages = (parseFloat($scope.selectedItems[index].ItemWeight) * parseFloat($scope.selectedItems[index].itemGmWages)).toFixed(2);
            $scope.selectedItems[index].TotalGoldManfact = (parseFloat($scope.selectedItems[index].NetTotal) + (parseFloat($scope.selectedItems[index].TotalItemGmWages) - parseFloat($scope.selectedItems[index].WagesDiscValue))).toFixed(2);

            $scope.changePriceByType();
            
        }
    }


    $scope.changeGmWages = function (index) {

    }




    $scope.addRowOfSerachItems = function (item) {
        $scope.gridBillDetailsItem = {};
        $scope.gridBillDetailsItem.GridRowNumber = item.GridRowNumber;
        $scope.gridBillDetailsItem.ITEM_CODE = item.ITEM_CODE;
        $scope.gridBillDetailsItem.ITEM_AR_NAME = item.ITEM_AR_NAME;
        // $scope.gridBillDetailsItem.Quantity = item.ItemWeight;
        $scope.gridBillDetailsItem.Quantity = 1;
        $scope.gridBillDetailsItem.CaliberID = item.CaliberID;
        $scope.gridBillDetailsItem.ARName = item.ARName;
        $scope.gridBillDetailsItem.LatName = item.LatName;
        $scope.gridBillDetailsItem.ClearnessRate = parseFloat(item.ClearnessRate) * 1000;

        $scope.gridBillDetailsItem.ItemWeight = item.ItemWeight;
        $scope.gridBillDetailsItem.ManufacturingWages = item.ManufacturingWages;
        $scope.gridBillDetailsItem.ITEM_ID = item.ITEM_ID;

        $scope.gridBillDetailsItem.LockPrice = item.LockPrice;

        
        //if (item.ManufacturingWages != undefined && item.ManufacturingWages != null) {
        //    if (item.ItemWeight != undefined && item.ItemWeight != null && item.ItemWeight != 0) {
        //        $scope.gridBillDetailsItem.itemGmWages = parseFloat(item.ManufacturingWages) / parseFloat(item.ItemWeight);
        //    }
        //    else {

        //        $scope.gridBillDetailsItem.itemGmWages = 0;
        //    }

        //}

        $scope.gridBillDetailsItem.ProfitMargin = item.ProfitMargin;

        $scope.gridBillDetailsItem.ItemStatus = item.ItemStatus;

        $scope.gridBillDetailsItem.GemsWeight = item.GemsWeight;
        $scope.gridBillDetailsItem.GemsWages = item.GemsWages;

        $scope.itemGmWages = 0;
        if (item.ItemStatus == 2) {
            $scope.gridBillDetailsItem.itemGmWages = (parseFloat(item.ManufacturingWages) / parseFloat(item.ItemWeight)).toFixed(2);
            $scope.gridBillDetailsItem.ManufacturingWages = item.ManufacturingWages;
        }
        else {
            $scope.gridBillDetailsItem.itemGmWages = item.ManufacturingWages;
            $scope.gridBillDetailsItem.ManufacturingWages = (parseFloat($scope.gridBillDetailsItem.itemGmWages) * parseFloat(item.ItemWeight)).toFixed(2);
        }

        ///////////save clearness of caliber
        if (item.ARName == "عيار 24") { $scope.clearnessRate24 = item.ClearnessRate; }
        else if (item.ARName == "عيار 22") { $scope.clearnessRate22 = item.ClearnessRate }
        else if (item.ARName == "عيار 21") { $scope.clearnessRate21 = item.ClearnessRate }
        else if (item.ARName == "عيار 18") { $scope.clearnessRate18 = item.ClearnessRate }


        $scope.gridBillDetailsItem.SubjectToVAT = item.SubjectToVAT;
        if (item.SubjectToVAT == true || item.SubjectToVAT == 1) {
            $scope.gridBillDetailsItem.VATRate = item.VATRate;
        }
        else {
            $scope.gridBillDetailsItem.VATRate = 0;
        }

        //Show the item Unit
        $scope.gridBillDetailsItem.Unit = item.Unit_Name_Ar;
        $scope.gridBillDetailsItem.DISC_VALUE = 0;
        $scope.gridBillDetailsItem.DISC_RATE = 0;

        $scope.gridBillDetailsItem.ActualItemWeight = item.ItemWeight;

        $scope.gridBillDetailsItem.TotalItemGmWages = (parseFloat(item.ItemWeight) * parseFloat($scope.gridBillDetailsItem.itemGmWages)).toFixed(2);

        // $scope.gridBillDetailsItem.TotalGoldManfact = parseFloat($scope.gridBillDetailsItem.TotalItemGmWages) + parseFloat($scope.gridBillDetailsItem.Total);

        $scope.gridBillDetailsItem.GoldAccID = item.GoldAccID;
        $scope.gridBillDetailsItem.ItemStatus = item.ItemStatus;
        
        if (!$scope.HasGoldModule) {
            $scope.gridBillDetailsItem.Price = item.ItemPrice;
            $scope.gridBillDetailsItem.Total = item.ItemPrice;
            $scope.gridBillDetailsItem.NetTotal = item.ItemPrice;
        }
        $scope.selectedItems.splice($scope.selectedItems.length - 1, 0, $scope.gridBillDetailsItem);
        $scope.addGridBillDetailsItem();
        $scope.calcgridBillDetailsItemCaliber($scope.gridBillDetailsItem, item.ItemWeight);
    }


    $scope.filter = function (by) {
        if (by) {
            $scope.filterData = {};
            $scope.filterData[by] = $scope.GoldBoxAccounts;
        } else {
            $scope.filterData = {};
        }
    };



    //////////////get item quantity
    $scope.getItemQunatitiyOnStore = function (index) {
        PointOfSaleService.getItemCurrentQuantity($scope.selectedItems[index].ITEM_ID, $scope.billMaster.COM_STORE_ID).then(function (response) {
            if (response != null && response != undefined && response.data != null && response.data != undefined) {

                var ItemCurrentQuantity = response.data;

                var ItemCurrentQuantityMessage = "وزن الصنف في المخزن الحالي  \n\n" + ItemCurrentQuantity.ItemCurrentQuantityInTheCurrentStore;
                swal({
                    text: ItemCurrentQuantityMessage,
                    type: 'warning',
                    showCancelButton: false,
                    confirmButtonColor: '#3085d6',
                    confirmButtonText: 'موافق',
                    confirmButtonClass: 'btn btn-success btn-lg',
                    buttonsStyling: false
                });

            }
        });
    }


    //$scope.custEnterEvent = function () {
    //    document.getElementById("deliveryTxt").focus();
    //}

    $scope.deliveryEnterEvent = function () {
        document.getElementById("remarksTxt").focus();
    }

    $scope.RemarksEnterEvent = function () {

        document.getElementById("empId").focus();
    }

    $('#BILL_DATETxt').keypress(function (event) {
        var keycode = (event.keyCode ? event.keyCode : event.which);
        if (keycode == '13') {
            $("#custDdl .btn").trigger("click");
        }

    });

    function tryr() {
        alert(document.getElementById("deliveryTxt"));
        $("#deliveryTxt").trigger("click");
        // document.getElementById("deliveryTxt").focus();
        //  alert(document.getElementById("deliveryTxt"))
        //document.getElementById("deliveryTxt").focus();
        // $("#deliveryTxt").trigger("click");
        // document.getElementById("deliveryTxt").select();
    }

    $scope.select2EventEnter = function () {

        $timeout(function () {

            document.getElementById("deliveryTxt").focus();

        }, 100, false);


        //  alert(document.getElementById("deliveryTxt"))
        //document.getElementById("deliveryTxt").focus();
        // $("#deliveryTxt").trigger("click");
        // document.getElementById("deliveryTxt").select();
    }
    $scope.disabledPrice = function () {
        if ($scope.IsLockPrice) {
            return true;
        } else {
            return false;
        }

    }
    $scope.disabledMatrix = function () {
        var BillType = ($location.search()).billType;
        if (BillType == 15 || BillType==14) {
            return true;
        }
         return false;
        

    }
    $scope.disabledDiscount= function () {
        var BillType = ($location.search()).billType;
        if (BillType == 80 ) {
            return true;
        }
        return false;


    }
    $scope.updateDiscount = function (data)
    {
        $scope.calculateTotals();
    }

}]);
