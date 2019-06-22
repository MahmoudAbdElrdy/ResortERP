'use strict';
app.controller('generalLedgerController', ['$scope', '$rootScope', '$location', '$log', '$q', 'accountsService', 'dateFilter', '$filter', 'commonService', 'currencyService', '$base64', 'entryService', 'accountStatementService', 'costCentersService', 'generalLedgerService','compBranchesService', function ($scope, $rootScope, $location, $log, $q, accountsService, dateFilter, $filter, commonService, currencyService, $base64, entryService, accountStatementService, costCentersService, generalLedgerService, compBranchesService) {


    $scope.accountList = [];

    $scope.entryMasterList = [];

    $scope.cust_Acc_ID = null;

    $scope.startDate = "";
    $scope.endDate = "";

    $scope.show_print = false;
    $scope.var = { ACC_ID: null, CURRENCY_ID: null, COST_CENTER_ID: null };

    /******************************/

    $scope.mainCostCenterList = [];

    $scope.currenciesList = [];

    $scope.MainAcc = true;
    $scope.SubAcc = true;
    $scope.BalancedAcc = true;
    $scope.EmptyAcc = true;

    $scope.CURRENCY_RATE = null;

    $scope.type = 0;


    $scope.selectedCostCenter = null;
    $scope.selectedCurrency = null;

    $scope.sum_DEBIT = [];
    $scope.sum_CREDIT = [];
    $scope.sum_Balance = [];


    $scope.cust_Acc_ID_selected = [];

    $scope.index = 0;

    $scope.count = 0;

    $scope.currentList = null;

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

    /******************************/

    $scope.getAccountList = function () {
        accountsService.getAll().then(function (results) {
            $scope.accountList = results.data;

            //$scope.accountList = jQuery.grep($scope.accountList, function (n, i) {
            //    return (n.PARENT_ACC_ID != null);
            //});

            //$('#cust_Acc_ID').multiSelect('refresh');  

            $.each(results.data, function (index, value) {
                $('#cust_Acc_ID').multiSelect('addOption', { value: value.ACC_ID, text: value.ACC_AR_NAME, index: index });
            });

        }, function (error) {
            console.log(error.data.message);
        });
    }


    $scope.getAllCurrencies = function () {

        currencyService.getAll().then(function (response) {

            var result = response.data;
            $scope.currenciesList = result;
            console.log(result);
        })
    }

    $scope.generalLedgerPageLoad = function () {

        $scope.loadMultiSelect();

        $scope.getCostCentersList();
        $scope.getAllCurrencies();
        $scope.getAccountList();
        $scope.GetUserBranches();

        //$scope.var.ACC_ID = 775;
        $scope.startDate = new Date(2018, 8, 14);
        $scope.endDate = new Date();

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

    $scope.getCostCentersList = function () {
        costCentersService.getMainCostCenters().then(function (results) {
            $scope.mainCostCenterList = results.data;
            console.log(results.data);
        }, function (error) {
            console.log(error.data.message);
        });
    }

    $scope.changeCurrencyRate = function (selectedItem) {

        //alert($scope.var.CURRENCY_ID);

        $scope.selectedCurrency =selectedItem;

        $scope.CURRENCY_RATE = selectedItem.CURRENCY_RATE;
    };

    $scope.getCostCenter = function (selectedItem) {
        $scope.selectedCostCenter = selectedItem;
    }

    $scope.GetSearchResult = function () {
        //$scope.var.ACC_ID &&

        var cust_Acc_ID = $('#cust_Acc_ID').val();


        if (cust_Acc_ID && $scope.startDate && $scope.endDate && $scope.SelectedBranch.length > 0) {

            var companyBranches = '';
            angular.forEach($scope.SelectedBranch, function (item, index) {
                companyBranches += item.id + ',';
            })
            companyBranches = companyBranches.substr(0, companyBranches.length - 1);

            var Accounts_Xml = "";

            for (var i = 0; i < cust_Acc_ID.length; i++) {
                Accounts_Xml += "<ACCOUNTS><ACC_ID>" + cust_Acc_ID[i] + "</ACC_ID></ACCOUNTS>";
            }

            //var Accounts = "<DS><ACCOUNTS><ACC_ID>" + "883" + "</ACC_ID></ACCOUNTS><ACCOUNTS><ACC_ID>" + "773" + "</ACC_ID></ACCOUNTS></DS>";

            var Accounts = "<DS>" + Accounts_Xml + "</DS>";

            var d1 = new Date($scope.startDate);

            var startDate = d1.getFullYear() + "" + ("0" + (d1.getMonth() + 1)).slice(-2) + "" + ("0" + d1.getDate()).slice(-2);

            var d2 = new Date($scope.endDate);

            var endDate = d2.getFullYear() + "" + ("0" + (d2.getMonth() + 1)).slice(-2) + "" + ("0" + (d2.getDate())).slice(-2);

            generalLedgerService.getByAccounts(companyBranches, Accounts,"" ,startDate, endDate).then(function (results) {

                console.log(results.data);

                $scope.entryMasterList = results.data;

                $scope.show_print = true;

                $scope.count = results.data.length;

                $scope.sum_DEBIT = [];
                $scope.sum_CREDIT = [];
                $scope.sum_Balance = [];

                for (var i = 0; i < results.data.length; i++) {

                    var list = results.data[i];
                    var sum_DEBIT = 0;
                    var sum_CREDIT = 0;
                    var sum_Balance = 0;

                    $.each(list, function (index, value) {

                        sum_DEBIT += value.DEBIT;
                        sum_CREDIT += value.CREDIDT;
                        sum_Balance += value.BAL;
                    });

                    $scope.sum_DEBIT.push(sum_DEBIT);
                    $scope.sum_CREDIT.push(sum_CREDIT);
                    $scope.sum_Balance.push(sum_Balance);
                }
                console.log("--------------");
                console.log($scope.sum_DEBIT);

            });

        }

        else swal("عفوا", "لابد من البحث بالحساب و التاريخ والفرع", "error");

        //if ($scope.var.COST_CENTER_ID && $scope.startDate && $scope.endDate) { 

        //    var costCenter = $scope.var.COST_CENTER_ID;

        //    var d1 = new Date($scope.startDate);

        //    var startDate = d1.getFullYear() + "" + ("0" + (d1.getMonth() + 1)).slice(-2) + "" + ("0" + d1.getDate()).slice(-2);

        //    var d2 = new Date($scope.endDate);

        //    var endDate = d2.getFullYear() + "" + ("0" + (d2.getMonth() + 1)).slice(-2) + "" + ("0" + (d2.getDate())).slice(-2);

        //    var type = $scope.type;

        //    trialBalanceService.getByAccounts(costCenter, startDate, endDate, type).then(function (results) {

        //        console.log(results.data);

        //        //$scope.accountList = results.data;

        //        //$scope.show_print = true;

        //        //$scope.entryMasterList = $scope.accountList;

        //        //$scope.filterAccounts();

        //        //$.each($scope.accountList, function (index, value) {

        //        //    $scope.sum_BDEBIT += value.BDEBIT;
        //        //    $scope.sum_BCREDIT += value.BCREDIT;
        //        //    $scope.sum_ADEBIT += value.ADEBIT;
        //        //    $scope.sum_ACREDIT += value.ACREDIT;
        //        //})


        //    });

        //}

    }



    $scope.clearEnity = function () {

        $scope.entryMasterList = [];

        $scope.var.ACC_ID = null;

        $scope.startDate = "";
        $scope.endDate = "";

        $scope.show_print = false;

        $scope.currentList = null;

    }


    $scope.loadMultiSelect = function () {
        $(document).ready(function () {

            $('#cust_Acc_ID').multiSelect({
                selectableHeader: "<input type='text' class='search-input form-control' autocomplete='off' placeholder=''>",
                selectionHeader: "<input type='text' class='search-input form-control' autocomplete='off' placeholder=''>",
                afterInit: function (ms) {
                    var that = this,
                        $selectableSearch = that.$selectableUl.prev(),
                        $selectionSearch = that.$selectionUl.prev(),
                        selectableSearchString = '#' + that.$container.attr('id') + ' .ms-elem-selectable:not(.ms-selected)',
                        selectionSearchString = '#' + that.$container.attr('id') + ' .ms-elem-selection.ms-selected';

                    that.qs1 = $selectableSearch.quicksearch(selectableSearchString)
                        .on('keydown', function (e) {
                            if (e.which === 40) {
                                that.$selectableUl.focus();
                                return false;
                            }
                        });

                    that.qs2 = $selectionSearch.quicksearch(selectionSearchString)
                        .on('keydown', function (e) {
                            if (e.which == 40) {
                                that.$selectionUl.focus();
                                return false;
                            }
                        });
                },
                afterSelect: function () {
                    this.qs1.cache();
                    this.qs2.cache();
                },
                afterDeselect: function () {
                    this.qs1.cache();
                    this.qs2.cache();
                }
            });
        });
    }
   
    $scope.getPrevious = function () {

        if ($scope.index > 0) {
            $scope.index -= 1;
        }
    }

    $scope.getNext = function () {
        if ($scope.index + 1 < $scope.count) {
            $scope.index += 1;
        }
    }

    $scope.fnExcelReport = function () {



        console.log($scope.createArr($scope.entryMasterList[$scope.index]));

        var all_client_name = [];

        $("#cust_Acc_ID option:selected").each(function () {

            var selText = $(this).text();
            all_client_name.push(selText);

        });

        var client_name = all_client_name[$scope.index];


        var arabic = /[\u0600-\u06FF]/;

        var x1 = "دفتر الحسابات".split(" ").reverse().join(" ");

        var x2 = ("اسم العميل : " + client_name).split(" ").reverse().join(" ");

        if (arabic.test(client_name) == false) {

            x2 = ("اسم العميل : ").split(" ").reverse().join(" ");

            x2 = client_name + " " + x2;
        }



        pdfMake.fonts = {
            custom: {
                normal: 'DroidKufi-Regular.ttf'
            }
        }

        var docDefinition = {
            content: [
                {
                    //layout: 'lightHorizontalLines', // optional
                    layout: {
                        hLineWidth: function (i, node) {
                            if (i === 0 || i === node.table.body.length) {
                                return 0;
                            }
                            return (i === node.table.headerRows) ? 2 : 1;
                        },
                        vLineWidth: function (i) {
                            return 0;
                        },
                        hLineColor: function (i) {
                            return i === 1 ? 'black' : '#aaa';
                        },
                        paddingLeft: function (i) {
                            return i === 0 ? 0 : 8;
                        },
                        paddingRight: function (i, node) {
                            return (i === node.table.widths.length - 1) ? 0 : 8;
                        },
                        fillColor: function (i, node) {
                            return i % 2 == 0 ? 'white' : '#eee';
                        }
                    },
                    table: {
                        // headers are automatically repeated if the table spans over multiple pages
                        // you can declare how many rows should be treated as headers
                        //widths: [60, 60, 160, 80, 80],

                        headerRows: 1,
                        widths: [80, 80, 80, 60, 60, 100],
                        margin: [400, 400, 0, 0],
                        body: $scope.createArr($scope.entryMasterList[$scope.index])
                    }
                }
            ],
            images: {
                bee: logoFileString
            },
            pageMargins: [20, 150, 0, 0],
            //pageOrientation: "landscape",
            header: {
                text: x1 + "\n" + x2,
                alignment: "center",
                margin: [0, 80, 0, 0],
                fontSize: 16,
            },
            background: function (page) {

                return [
                    '\r\n',
                    {
                        image: 'bee',
                        width: 200
                    }
                ];

            },
            defaultStyle: {
                font: 'custom',
                alignment: 'center',
                top: 200
            },
        };

        pdfMake.createPdf(docDefinition).download();


    }

    $scope.createArr = function (list) {



        var arr = [];

        for (var i = 0; i <= list.length; i++) {
            arr[i] = [];
        }

        arr[0][0] = "الرصيد";
        arr[0][1] = "مدين";
        arr[0][2] = "دائن";
        arr[0][3] = "رقم المستند";
        arr[0][4] = "رقم القيد";
        arr[0][5] = "التاريخ";

        for (var i = 0; i < 6; i++) {

            arr[0][i] = arr[0][i].split(" ").reverse().join(" ");
        }

        var index = 1;

        for (var i = 0; i < list.length; i++) {

            var obj = list[i];

            var j = 0;
            for (var key in obj) {
                if (obj.hasOwnProperty(key)) {


                    if (key == "ENTRY_NUMBER") {
                        arr[index][3] = "";//obj[key];
                    }
                    else if (key == "CREDIDT") {

                        arr[index][2] = Number(obj[key]).toFixed(2);
                    }
                    else if (key == "DEBIT") {

                        arr[index][1] = Number(obj[key]).toFixed(2);
                    }
                    else if (key == "BAL") {

                        arr[index][0] = Number(obj[key]).toFixed(2);
                    }
                    else if (key == "BILL_NUMBER") {

                        arr[index][4] = "";//obj[key];
                    }
                    else if (key == "DATE") {

                        if (obj[key]) {

                            var d1 = new Date(obj[key]);

                            var account_date = d1.getFullYear() + "-" + ("0" + (d1.getMonth() + 1)).slice(-2) + "-" + ("0" + d1.getDate()).slice(-2);

                            arr[index][5] = account_date
                        }
                        else {
                            arr[index][5] = "";
                        }
                    }

                    j++;

                }
            }
            index++;
        }

        var _index = $scope.index;
        var header = ["صافى الرصيد", "مجموع مدين", "مجموع دائن", "", "", ""];
        var total = [$scope.sum_Balance[_index], $scope.sum_DEBIT[_index], $scope.sum_CREDIT[_index], null, null, null];

        for (var i = 0; i < header.length; i++) {

            header[i] = header[i].split(" ").reverse().join(" ");
        }

        for (var i = 0; i < total.length; i++) {

            if (total[i]) {
                total[i] = Number(total[i]).toFixed(2);
            }
        }

        arr.push(header);
        arr.push(total);

        return arr;
    }


}]);


