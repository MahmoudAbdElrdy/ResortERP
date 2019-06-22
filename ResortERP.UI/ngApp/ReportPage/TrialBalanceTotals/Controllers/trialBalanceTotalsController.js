'use strict';
app.controller('trialBalanceTotalsController', ['$scope', '$rootScope', '$location', '$log', '$q', 'accountsService', 'dateFilter', '$filter', 'commonService', 'currencyService', '$base64', 'entryService', 'accountStatementService', 'costCentersService', 'trialBalanceService', 'compBranchesService', function ($scope, $rootScope, $location, $log, $q, accountsService, dateFilter, $filter, commonService, currencyService, $base64, entryService, accountStatementService, costCentersService, trialBalanceService, compBranchesService) {


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

    $scope.sum_BBallance = 0;
    $scope.sum_ADEBIT = 0;
    $scope.sum_ACREDIT = 0;
    $scope.sum_ABalance = 0;

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

    $scope.trialBalancePageLoad = function () {

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

        if ( $scope.startDate && $scope.endDate && $scope.SelectedBranch.length > 0) { 

            var companyBranches = '';
            angular.forEach($scope.SelectedBranch, function (item, index) {
                companyBranches += item.id + ',';
            })
            companyBranches = companyBranches.substr(0, companyBranches.length - 1);

            var costCenter = $scope.var.COST_CENTER_ID;

            var d1 = new Date($scope.startDate);

            var startDate = d1.getFullYear() + "" + ("0" + (d1.getMonth() + 1)).slice(-2) + "" + ("0" + d1.getDate()).slice(-2);

            var d2 = new Date($scope.endDate);

            var endDate = d2.getFullYear() + "" + ("0" + (d2.getMonth() + 1)).slice(-2) + "" + ("0" + (d2.getDate())).slice(-2);

            //var type = $scope.type;

            trialBalanceService.getByAccounts(companyBranches, startDate, endDate, 0).then(function (results) {

                console.log(results.data);

                $scope.accountList = results.data;

                $scope.show_print = true;

                $scope.entryMasterList = $scope.accountList;

                $scope.filterAccounts();

                //$.each($scope.accountList, function (index, value) {
                //    $scope.sum_BBallance += value.BDEBIT ? parseFloat(value.BDEBIT) : 0;
                //    $scope.sum_INDEBIT += value.INDEBIT ? parseFloat(value.INDEBIT) : 0;
                //    $scope.sum_INCREDIT += value.INCREDIT ? parseFloat(value.INCREDIT) : 0;
                //    $scope.sum_ABallance += value.ACREDIT ? parseFloat(value.ACREDIT) : 0;
                //})


            });

        }

        else swal("عفوا", "لابد من البحث بالتاريخ", "error");

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

        var all_client_name = [];

        $("#cust_Acc_ID option:selected").each(function () {

            var selText = $(this).text();
            all_client_name.push(selText);

        });

        var client_name = all_client_name[$scope.index];


        var arabic = /[\u0600-\u06FF]/;

        var x1 = "ميزان المراجعة".split(" ").reverse().join(" ");

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
                        widths: ['*', '*', '*', '*', 'auto', 'auto', '*'],
                        //margin: [400, 400, 0, 0],
                        body: $scope.createArr($scope.entryMasterList)
                    }
                }
            ],
            images: {
                bee: logoFileString
            },
            pageMargins: [20, 150, 0, 0],
            pageOrientation: "landscape",
            header: {
                text: x1, //+ "\n" + x2,
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

        arr[0][0] = "الرصيد الحالى";
        arr[0][1] = "حركة دائن";
        arr[0][2] = "حركة مدين";
        arr[0][3] = "الرصيد السابق";
        arr[0][4] = "اسم الحساب";
        arr[0][5] = "رقم الحساب";
        arr[0][6] = "مسلسل";


        for (var i = 0; i < 6; i++) {

            arr[0][i] = arr[0][i].split(" ").reverse().join(" ");
        }

        var index = 1;

        for (var i = 0; i < list.length; i++) {
            arr[index][6] = i + 1;
            var obj = list[i];
            for (var key in obj) {
                if (obj.hasOwnProperty(key)) {
                    if (key == "ACREDIT") {
                        arr[index][0] = Number(parseFloat(obj['ADEBIT']) - parseFloat(obj[key])).toFixed(2);
                    }
                    else if (key == "INCREDIT") {
                        arr[index][1] = Number(obj[key]).toFixed(2);
                    }
                    else if (key == "INDEBIT") {
                        arr[index][2] = Number(obj[key]).toFixed(2);
                    }
                    else if (key == "BCREDIT") {
                        arr[index][3] = Number(parseFloat(obj['BDEBIT']) - parseFloat(obj[key])).toFixed(2);
                    }                                     
                    else if (key == "AC_NAME") {
                        arr[index][4] = obj[key] ? obj[key].split(" ").reverse().join(" ") : '';
                    }
                    else if (key == "AC_CODE") {
                        arr[index][5] = obj[key] ? obj[key].split(" ").reverse().join(" ") : '';
                    }
                }
            }
            index++;
        }
        return arr;
    }



}]);


