'use strict';
app.controller('trialBalanceGoldController', ['$scope', '$rootScope', '$location', '$log', '$q', 'accountsService', 'dateFilter', '$filter', 'commonService', 'currencyService', '$base64', 'entryService', 'accountStatementService', 'costCentersService', 'trialBalanceGoldService','compBranchesService', function ($scope, $rootScope, $location, $log, $q, accountsService, dateFilter, $filter, commonService, currencyService, $base64, entryService, accountStatementService, costCentersService, trialBalanceGoldService, compBranchesService) {


    $scope.accountList = [];

    $scope.entryMasterList = [];

    $scope.cust_Acc_ID = null;

    $scope.startDate = "";
    $scope.endDate = "";

    $scope.show_print = false;
    $scope.var = { ACC_ID: null, CURRENCY_ID: null, COST_CENTER_ID: null };

    $scope.startDate = new Date(Date.now());
    $scope.endDate = new Date(Date.now());
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

    $scope.sum_BDEBIT = 0;
    $scope.sum_BCREDIT = 0;
    $scope.sum_ADEBIT = 0;
    $scope.sum_ACREDIT = 0;

    $scope.sum_BDEBIT_24 = 0;
    $scope.sum_BCREDIT_24 = 0;
    $scope.sum_ADEBIT_24 = 0;
    $scope.sum_ACREDIT_24 = 0;

    $scope.sum_BDEBIT_22 = 0;
    $scope.sum_BCREDIT_22 = 0;
    $scope.sum_ADEBIT_22 = 0;
    $scope.sum_ACREDIT_22 = 0;

    $scope.sum_BDEBIT_21 = 0;
    $scope.sum_BCREDIT_21 = 0;
    $scope.sum_ADEBIT_21 = 0;
    $scope.sum_ACREDIT_21 = 0;

    $scope.sum_BDEBIT_18 = 0;
    $scope.sum_BCREDIT_18 = 0;
    $scope.sum_ADEBIT_18 = 0;
    $scope.sum_ACREDIT_18 = 0;

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

            $scope.accountList = jQuery.grep($scope.accountList, function (n, i) {
                return (n.PARENT_ACC_ID != null);
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

    $scope.trialBalanceGoldPageLoad = function () {

        $scope.getCostCentersList();
        $scope.getAllCurrencies();
        $scope.GetUserBranches();
        //$scope.getAccountList();
        //$scope.var.ACC_ID = 775;
        //$scope.startDate = new Date(2018, 8, 14);
        //$scope.endDate = new Date();
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

        if ($scope.startDate && $scope.endDate && $scope.SelectedBranch.length > 0) { 

            var companyBranches = '';
            angular.forEach($scope.SelectedBranch, function (item, index) {
                companyBranches += item.id + ',';
            })
            companyBranches = companyBranches.substr(0, companyBranches.length - 1);

           // var costCenter = $scope.var.COST_CENTER_ID;

            var d1 = new Date($scope.startDate);

            var startDate = d1.getFullYear() + "" + ("0" + (d1.getMonth() + 1)).slice(-2) + "" + ("0" + d1.getDate()).slice(-2);

            var d2 = new Date($scope.endDate);

            var endDate = d2.getFullYear() + "" + ("0" + (d2.getMonth() + 1)).slice(-2) + "" + ("0" + (d2.getDate())).slice(-2);

            //var type = $scope.type;

            trialBalanceGoldService.getByAccounts(companyBranches, startDate, endDate, 1).then(function (results) {

                console.log(results.data);

                $scope.accountList = results.data;

                $scope.show_print = true;

                $scope.entryMasterList = $scope.accountList;

                $scope.filterAccounts();

                $.each($scope.accountList, function (index, value) {

                    $scope.sum_BDEBIT += value.BDEBIT;
                    $scope.sum_BCREDIT += value.BCREDIT;
                    $scope.sum_ADEBIT += value.ADEBIT;
                    $scope.sum_ACREDIT += value.ACREDIT;

                    $scope.sum_BDEBIT_24 += value.B_DEBIT_Gold24;
                    $scope.sum_BCREDIT_24 += value.B_CREDIT_Gold24;
                    $scope.sum_ADEBIT_24 += value.IN_DEBIT_Gold24;
                    $scope.sum_ACREDIT_24 += value.IN_CREDIT_Gold24;

                    $scope.sum_BDEBIT_22 += value.B_DEBIT_Gold22;
                    $scope.sum_BCREDIT_22 += value.B_CREDIT_Gold22;
                    $scope.sum_ADEBIT_22 += value.IN_DEBIT_Gold22;
                    $scope.sum_ACREDIT_22 += value.IN_CREDIT_Gold22;

                    $scope.sum_BDEBIT_21 += value.B_DEBIT_Gold21;
                    $scope.sum_BCREDIT_21 += value.B_CREDIT_Gold21;
                    $scope.sum_ADEBIT_21 += value.IN_DEBIT_Gold21;
                    $scope.sum_ACREDIT_21 += value.IN_CREDIT_Gold21;

                    $scope.sum_BDEBIT_18 += value.B_DEBIT_Gold18;
                    $scope.sum_BCREDIT_18 += value.B_CREDIT_Gold18;
                    $scope.sum_ADEBIT_18 += value.IN_DEBIT_Gold18;
                    $scope.sum_ACREDIT_18 += value.IN_CREDIT_Gold18;
                })


            });

        }

        else swal("عفوا", "لابد من البحث بالتاريخ والفرع", "error");

    }



    $scope.clearEnity = function () {

        //d.getFullYear() + "" + (d.getMonth() + 1) + "" + d.getDate();

        $scope.entryMasterList = [];

        //$scope.var.ACC_ID = null;

        $scope.var.COST_CENTER_ID = null;


        //$scope.mainCostCenterList[0].sele
        $scope.startDate = "";
        $scope.endDate = "";

        $scope.show_print = false;

    }

    
    $scope.filterMainAccounts = function () {

        $scope.filterAccounts();

    }

    $scope.filterSubAccounts = function () {

        $scope.filterAccounts();

    }

    $scope.filterBalancedAccounts = function () {

        $scope.filterAccounts();
    }

    $scope.filterEmptyAccounts = function () {

        $scope.filterAccounts();
    }
    


    $scope.calculateSum = function () {
   

    }

    $scope.filterAccounts = function () {

        if ($scope.MainAcc == true && $scope.SubAcc == true) {

            if ($scope.BalancedAcc == true && $scope.EmptyAcc == true) {

                $scope.entryMasterList = $scope.accountList;
            }
            else if ($scope.BalancedAcc == true) {
                $scope.entryMasterList = jQuery.grep($scope.accountList, function (n, i) {
                    return (n.BDEBIT != 0 || n.BCREDIT != 0 || n.ADEBIT != 0 || n.ACREDIT != 0);
                });

            }
            else if ($scope.EmptyAcc == true) {
                $scope.entryMasterList = jQuery.grep($scope.accountList, function (n, i) {
                    return (n.BDEBIT == 0 && n.BCREDIT == 0 && n.ADEBIT == 0 && n.ACREDIT == 0);
                });
            }
            else {
                $scope.entryMasterList = $scope.accountList;
            }

        }
        else if ($scope.MainAcc == true) {

            if ($scope.BalancedAcc == true && $scope.EmptyAcc == true) {
                $scope.entryMasterList = jQuery.grep($scope.accountList, function (n, i) {
                    return (n.PARENT_ID == null);
                });
            }
            else if ($scope.BalancedAcc == true) {
                $scope.entryMasterList = jQuery.grep($scope.accountList, function (n, i) {
                    return (n.PARENT_ID == null && (n.BDEBIT != 0 || n.BCREDIT != 0 || n.ADEBIT != 0 || n.ACREDIT != 0));
                });

            }
            else if ($scope.EmptyAcc == true) {
                $scope.entryMasterList = jQuery.grep($scope.accountList, function (n, i) {
                    return (n.PARENT_ID == null && (n.BDEBIT == 0 && n.BCREDIT == 0 && n.ADEBIT == 0 && n.ACREDIT == 0));
                });
            }
            else {
                $scope.entryMasterList = jQuery.grep($scope.accountList, function (n, i) {
                    return (n.PARENT_ID == null);
                });
            }

        }
        else if ($scope.SubAcc == true) {

            if ($scope.BalancedAcc == true && $scope.EmptyAcc == true) {
                $scope.entryMasterList = jQuery.grep($scope.accountList, function (n, i) {
                    return (n.PARENT_ID != null);
                });
            }
            else if ($scope.BalancedAcc == true) {
                $scope.entryMasterList = jQuery.grep($scope.accountList, function (n, i) {
                    return (n.PARENT_ID != null && (n.BDEBIT != 0 || n.BCREDIT != 0 || n.ADEBIT != 0 || n.ACREDIT != 0));
                });

            }
            else if ($scope.EmptyAcc == true) {
                $scope.entryMasterList = jQuery.grep($scope.accountList, function (n, i) {
                    return (n.PARENT_ID != null && (n.BDEBIT == 0 && n.BCREDIT == 0 && n.ADEBIT == 0 && n.ACREDIT == 0));
                });
            }
            else {
                $scope.entryMasterList = jQuery.grep($scope.accountList, function (n, i) {
                    return (n.PARENT_ID != null);
                });
            }

        }
        else {
            $scope.entryMasterList = [];
        }

    }

    $scope.filterAccountsLevels = function () {

        if ($scope.ACC_Level) {

            $scope.entryMasterList = jQuery.grep($scope.accountList, function (n, i) {
                return (n.LEVEL == $scope.ACC_Level);
            });
        }
        else {
            $scope.entryMasterList = $scope.accountList;
        }

    }

    $scope.fnExcelReport = function () {



        console.log($scope.createArr($scope.entryMasterList));


        //var costCenter_name = $("#cost_Center_ID").text().split("\n")[1].trim();


        var arabic = /[\u0600-\u06FF]/;

        var x1 = "ميزان المراجعة بالارصدة - ذهب".split(" ").reverse().join(" ");

        //var x2 = ("مركز التكلفة : " + costCenter_name).split(" ").reverse().join(" ");

        //if (arabic.test(costCenter_name) == false) {

        //    x2 = ("مركز التكلفة : ").split(" ").reverse().join(" ");

        //    x2 = costCenter_name + " " + x2;
        //}



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
                        widths: [45, 45, 45, 45, 45, 45, 45, 45, 45, 45, 45, 45, 45, 45, 45, 45, 180],
                        margin: [400, 400, 0, 0],
                        body: $scope.createArr($scope.entryMasterList)
                    }
                }
            ],
            images: {
                bee: logoFileString
            },
            pageMargins: [20, 150, 0, 0],
            pageOrientation: "landscape",
            pageSize: 'A3',
            header: {
                text: x1 ,//+ "\n" + x2,
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


        for (var i = 0; i <= list.length + 1; i++) {
            arr[i] = [];
        }

        arr[0] = [
            {
                text: "ذهب عيار 18".split(" ").reverse().join(" "),
                colSpan: 4,
                alignment: 'center'
            },
            {},
            {},
            {},
            {
                text: "ذهب عيار 21".split(" ").reverse().join(" "),
                colSpan: 4,
                alignment: 'center'
            },
            {},
            {},
            {},
            {
                text: "ذهب عيار 22".split(" ").reverse().join(" "),
                colSpan: 4,
                alignment: 'center'
            },
            {},
            {},
            {},
            {
                text: "ذهب عيار 24".split(" ").reverse().join(" "),
                colSpan: 4,
                alignment: 'center'
            },
            {},
            {},
            {},
            {}
        ];

        arr[1][0] = "مجموع مدين 18";
        arr[1][1] = "مجموع دائن 18";
        arr[1][2] = "رصيد مدين 18";
        arr[1][3] = "رصيد دائن 18";

        arr[1][4] = "مجموع مدين 21";
        arr[1][5] = "21 مجموع دائن";
        arr[1][6] = "رصيد مدين 21";
        arr[1][7] = "رصيد دائن 21";

        arr[1][8] = "22 مجموع مدين";
        arr[1][9] = "22 مجموع دائن";
        arr[1][10] = "رصيد مدين 22";
        arr[1][11] = "رصيد دائن 22";

        arr[1][12] = "24 مجموع مدين";
        arr[1][13] = "مجموع دائن 24";
        arr[1][14] = "رصيد مدين 24";
        arr[1][15] = "رصيد دائن 24";

        arr[1][16] = "اسم الحساب";

        for (var i = 0; i < 17; i++) {

            arr[1][i] = arr[1][i].split(" ").reverse().join(" ");
        }

        var index = 2;

        for (var i = 0; i < list.length; i++) {

            var obj = list[i];

            var j = 0;
            for (var key in obj) {
                if (obj.hasOwnProperty(key)) {


                    if (key == "B_CREDIT_Gold18") {
                        arr[index][0] = Number(obj[key]).toFixed(2);
                    }
                    else if (key == "B_DEBIT_Gold18") {

                        arr[index][1] = Number(obj[key]).toFixed(2);
                    }
                    else if (key == "IN_CREDIT_Gold18") {

                        arr[index][2] = Number(obj[key]).toFixed(2);
                    }
                    else if (key == "IN_DEBIT_Gold18") {

                        arr[index][3] = Number(obj[key]).toFixed(2);
                    }
////////////////////////////
                    else if (key == "B_CREDIT_Gold21") {
                        arr[index][4] = Number(obj[key]).toFixed(2);
                    }
                    else if (key == "B_DEBIT_Gold21") {

                        arr[index][5] = Number(obj[key]).toFixed(2);
                    }
                    else if (key == "IN_CREDIT_Gold21") {

                        arr[index][6] = Number(obj[key]).toFixed(2);
                    }
                    else if (key == "IN_DEBIT_Gold21") {

                        arr[index][7] = Number(obj[key]).toFixed(2);
                    }
////////////////////////////////
                    if (key == "B_CREDIT_Gold22") {
                        arr[index][8] = Number(obj[key]).toFixed(2);
                    }
                    else if (key == "B_DEBIT_Gold22") {

                        arr[index][9] = Number(obj[key]).toFixed(2);
                    }
                    else if (key == "IN_CREDIT_Gold22") {

                        arr[index][10] = Number(obj[key]).toFixed(2);
                    }
                    else if (key == "IN_DEBIT_Gold22") {

                        arr[index][11] = Number(obj[key]).toFixed(2);
                    }
//////////////////////////////
                    if (key == "B_CREDIT_Gold24") {
                        arr[index][12] = Number(obj[key]).toFixed(2);
                    }
                    else if (key == "B_DEBIT_Gold24") {

                        arr[index][13] = Number(obj[key]).toFixed(2);
                    }
                    else if (key == "IN_CREDIT_Gold24") {

                        arr[index][14] = Number(obj[key]).toFixed(2);
                    }
                    else if (key == "IN_DEBIT_Gold24") {

                        arr[index][15] = Number(obj[key]).toFixed(2);
                    }
///////////////////////////////
                    else if (key == "AC_NAME") {

                        arr[index][16] = obj[key].split(" ").reverse().join(" ");
                    } 

                    j++;

                }
            }
            index++;
        }
        return arr;
    }



}]);


