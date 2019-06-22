'use strict';
app.controller('GoldBoxMotionDailyController', ['$scope', '$rootScope', '$location', '$log', '$q', 'accountsService', 'dateFilter', '$filter', 'commonService', 'currencyService', '$base64', 'entryService', 'accountStatementGoldDailyService', 'compBranchesService', function ($scope, $rootScope, $location, $log, $q, accountsService, dateFilter, $filter, commonService, currencyService, $base64, entryService, accountStatementGoldDailyService, compBranchesService) {


    $scope.accountList = [];

    $scope.entryMasterList = [];

    $scope.cust_Acc_ID = null;

    $scope.endDate = new Date();
    $scope.endDate = "";

    $scope.show_print = false;
    $scope.var = { ACC_ID: null };
  
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

    $scope.getAccountList = function () {
        accountsService.GetAccountsFilteredByType("GoldBox").then(function (result) {
            $scope.accountList = result.data;
        }, function (error) {
            console.log(error.data.message);
        }); 
    }

    $scope.accountStatmentGoldPageLoad = function () {

        $scope.getAccountList();
        $scope.GetUserBranches();

        $scope.var.ACC_ID = 775;
        $scope.endDate = new Date();
    }

    $scope.GetUserBranches = function () {
        compBranchesService.getUserBranches().then(function (response) {
            if(response.data != undefined && response.data.length > 0) {
                angular.forEach(response.data, function (value, index) {
                    $scope.UserBranches.push({ id: value.COM_BRN_ID, label: value.COM_BRN_AR_NAME });
                });
                $scope.SelectedBranch = [$scope.UserBranches[0]];
            }
        }, function (error) { console.log(error.data.message); });
    };

    $scope.GetSearchResult = function () {

        //accountStatementGoldDailyService.getByID($scope.cust_Acc_ID).then(function (results) {

        //    console.log(results.data);

        //    $scope.entryMasterList = results.data;
        //});

        //var Accounts = '<?xml version ="1.0" encoding="windows-1256"?> <DS><ACCOUNTS><ACC_ID>775</ACC_ID></ACCOUNTS><ACCOUNTS><ACC_ID>54</ACC_ID></ACCOUNTS></DS>';

        //var Accounts = "<DS><ACCOUNTS><ACC_ID>775</ACC_ID></ACCOUNTS><ACCOUNTS><ACC_ID>775</ACC_ID></ACCOUNTS><ACCOUNTS><ACC_ID>54</ACC_ID></ACCOUNTS><ACCOUNTS><ACC_ID>3385</ACC_ID></ACCOUNTS></DS>";


        if ($scope.var.ACC_ID && $scope.endDate && $scope.SelectedBranch.length > 0) {

            var companyBranches = '';
            angular.forEach($scope.SelectedBranch, function (item, index) {
                companyBranches += item.id + ',';
            })
            companyBranches = companyBranches.substr(0, companyBranches.length - 1);

            var Accounts = "<DS><ACCOUNTS><ACC_ID>" + $scope.var.ACC_ID + "</ACC_ID></ACCOUNTS></DS>";
            
            var d1 = new Date($scope.endDate);

            var date = d1.getFullYear() + "" + ("0" + (d1.getMonth() + 1)).slice(-2) + "" + ("0" + (d1.getDate())).slice(-2);

            accountStatementGoldDailyService.getReportResult(companyBranches, Accounts, date).then(function (results) {

                console.log(results.data);

                $scope.entryMasterList = results.data;

                $scope.show_print = true;
            });

        }

        else swal("عفوا", " لابد من البحث بالحساب و التاريخ والفرع ", "error");

    }



    $scope.clearEnity = function () {

        //d.getFullYear() + "" + (d.getMonth() + 1) + "" + d.getDate();

        $scope.entryMasterList = [];

        $scope.var.ACC_ID = null;

        $scope.endDate = new Date();

        $scope.show_print = false;

    }
    
    $scope.fnExcelReport = function () {

    

        console.log($scope.createArr($scope.entryMasterList));

        //var client_name = $("#cust_Acc_ID option:selected").text();

        var client_name = $("#cust_Acc_ID").text().split("\n")[1].trim();

        var arabic = /[\u0600-\u06FF]/;

        var x1 = "حركة صندوق الذهب المشغول (يومية )".split(" ").reverse().join(" ");

        var x2 = ("اسم الصندوق : " + client_name).split(" ").reverse().join(" ");

        if (arabic.test(client_name) == false) {

            x2 = ("اسم الصندوق : ").split(" ").reverse().join(" ");

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
                        widths: ['auto', 'auto', 'auto', 'auto', 'auto', 'auto', 'auto', 'auto', 'auto', 'auto', 'auto', 'auto', '*', '*', 'auto', '*', 'auto', 'auto'],
                        //margin: [400, 400, 0, 0],
                        body: $scope.createArr($scope.entryMasterList)
                    }
                }
            ],
            images: {
                bee: "data:image/png;base64,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"
            },
            pageMargins: [5, 150, 5, 0],
            pageOrientation: "landscape",
            pageSize: 'A3',
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

    $scope.createArr_ = function (list) {


        var arr = [];

        for (var i = 0; i < list.length; i++) {
            arr[i] = [];
        }

        for (var i = 0; i < list.length; i++) {

            var obj = list[i];

            var j = 0;
            for (var key in obj) {
                if (obj.hasOwnProperty(key)) {
                    if (j >= 5) {
                        continue;
                    }
                    //if (j == 4) {
                        arr[i][j] = obj[key].split(" ").reverse().join(" ");
                    //}
                    //else
                    //    arr[i][j] = obj[key];
                    j++;
                }
            }
        }
        return arr;
    }

    $scope.createArr = function (list) {
        var arr = [];

        for (var i = 0; i <= list.length; i++) {
            arr[i] = [];
        }
    
        arr[0][0] = "الرصيد 24";
        arr[0][1] = "دائن 24";    
        arr[0][2] = "مدين 24";

        arr[0][3] = "الرصيد 22";
        arr[0][4] = "دائن 22";
        arr[0][5] = "مدين 22";

        arr[0][6] = "الرصيد 21";
        arr[0][7] = "دائن 21";
        arr[0][8] = "مدين 21";

        arr[0][9] = "الرصيد 18";
        arr[0][10] = "دائن 18";
        arr[0][11] = "مدين 18";

        arr[0][12] = "المناولة";
        arr[0][13] = "البيان";
        arr[0][14] = "رقم السند";
        arr[0][15] = "اسم السند";
        arr[0][16] = "التاريخ";
        arr[0][17] = "مسلسل";

        for (var i = 0; i < 17; i++) {
            arr[0][i] = arr[0][i].split(" ").reverse().join(" ");
        }

        var index = 1;
        for (var i = 0; i < list.length; i++) {
            var obj = list[i];
            arr[index][17] = i + 1;
            for (var key in obj) {
                if (obj.hasOwnProperty(key)) {
                    if (key == "BAL_Gold24") {
                        arr[index][0] = Number(obj[key]).toFixed(2);
                    }
                    else if (key == "CREDIT_Gold24") {
                        arr[index][1] = Number(obj[key]).toFixed(2);
                    }
                    else if (key == "DEBIT_Gold24") {
                        arr[index][2] = Number(obj[key]).toFixed(2);
                    }
                    else if (key == "BAL_Gold22") {
                        arr[index][3] = Number(obj[key]).toFixed(2);
                    }
                    else if (key == "CREDIT_Gold22") {
                        arr[index][4] = Number(obj[key]).toFixed(2);
                    }
                    else if (key == "DEBIT_Gold22") {
                        arr[index][5] = Number(obj[key]).toFixed(2);
                    }
                    else if (key == "BAL_Gold21") {
                        arr[index][6] = Number(obj[key]).toFixed(2);
                    }
                    else if (key == "CREDIT_Gold21") {
                        arr[index][7] = Number(obj[key]).toFixed(2);
                    }
                    else if (key == "DEBIT_Gold21") {
                        arr[index][8] = Number(obj[key]).toFixed(2);
                    }
                    else if (key == "BAL_Gold18") {
                        arr[index][9] = Number(obj[key]).toFixed(2);
                    }
                    else if (key == "CREDIT_Gold18") {
                        arr[index][10] = Number(obj[key]).toFixed(2);
                    }
                    else if (key == "DEBIT_Gold18") {
                        arr[index][11] = Number(obj[key]).toFixed(2);
                    }
                    else if (key == "BILL_REMARKS") {
                        if (obj[key]) {
                            arr[index][12] = obj[key];
                        }
                        else {
                            arr[index][12] = '';
                        }
                    }
                    else if (key == "NOTE") {
                        if (obj[key]) {
                            arr[index][13] = obj[key].split(" ").reverse().join(" ");
                        }
                        else {
                            arr[index][13] = '';
                        }
                    }
                    else if (key == "ENTRY_NUMBER") {
                        arr[index][14] = obj[key];
                    }
                    else if (key == "BILL_AR_NAME") {
                        if (obj[key]) {
                            arr[index][15] = obj[key].split(" ").reverse().join(" ");
                        }
                        else if (obj["CHEET"]) {
                            arr[index][15] = obj["CHEET"].split(" ").reverse().join(" ");
                        }
                        else {
                            arr[index][15] = obj["NOTE"].split(" ").reverse().join(" ");
                        }
                    }
                    else if (key == "DATE") {
                        if (obj[key]) {
                            var d1 = new Date(obj[key]);
                            var account_date = d1.getFullYear() + "-" + ("0" + (d1.getMonth() + 1)).slice(-2) + "-" + ("0" + d1.getDate()).slice(-2);
                            arr[index][16] = account_date
                        }
                        else {
                            arr[index][16] = "";
                        }
                    }
                }
            }
            index++;
        }
        return arr;
    }
    



}]);


