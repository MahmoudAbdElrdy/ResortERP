'use strict';
app.controller('itemsReportPageController', ['$scope', '$location', '$log', '$q', 'billSettingService', 'itemsService', 'companyStoresService', 'currencyService', 'costCentersService', 'employeesService', 'billProfitReportPageService', '$document', '$uibModal', '$uibModalStack', 'itemReportPageService', 'compBranchesService', function ($scope, $location, $log, $q, billSettingService, itemsService, companyStoresService, currencyService, costCentersService, employeesService, profitService, $document, $uibModal, $uibModalStack, itemRepoService, compBranchesService) {

    $scope.search = { dateFrom: null, dateTo: null };
    $scope.reportOptionList = [{ NAME: "عرض الفواتير المرحلة", ID: "1", SELECTED: false }, { NAME: "عرض الفواتير الغير مرحلة", ID: "2", SELECTED: false }, { NAME: "عرض المخزن", ID: "3", SELECTED: false }, { NAME: "عرض الخصومات و الاضافات", ID: "4", SELECTED: false }, { NAME: "عرض الهدايا", ID: "5", SELECTED: false }];


    $scope.clearEnity = function () {
        $scope.search = { dateFrom: null, dateTo: null };
        $scope.reportOptionList = [{ NAME: "عرض الفواتير المرحلة", ID: "1", SELECTED: false }, { NAME: "عرض الفواتير الغير مرحلة", ID: "2", SELECTED: false }, { NAME: "عرض المخزن", ID: "3", SELECTED: false }, { NAME: "عرض الخصومات و الاضافات", ID: "4", SELECTED: false }, { NAME: "عرض الهدايا", ID: "5", SELECTED: false }];

        for (var i = 0; i < $scope.billSettingsList.length; i++) {
            $scope.billSettingsList[i].selected = false;
        }

    };

    $scope.searchList_print = [];

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

    $scope.GetSearchResult = function () {
        debugger;
        if ($scope.search !== undefined && $scope.search !== null && $scope.search.itemName !== null && $scope.search.itemName !== undefined && $scope.SelectedBranch.length > 0) {

            var companyBranches = '';
            angular.forEach($scope.SelectedBranch, function (item, index) {
                companyBranches += item.id + ',';
            })
            companyBranches = companyBranches.substr(0, companyBranches.length - 1);

            $scope.search.companyBranches = companyBranches;

            $scope.selectedbillSettingsList = [];
            $scope.selectedReportOptionList = [];
            for (var i = 0; i < $scope.billSettingsList.length; i++) {
                if ($scope.billSettingsList[i].selected === true) {
                    $scope.selectedbillSettingsList.push($scope.billSettingsList[i]);
                }
            }
            if ($scope.selectedbillSettingsList.length > 0) {

                for (var i = 0; i < $scope.reportOptionList.length; i++) {
                    if ($scope.reportOptionList[i].SELECTED === true) {
                        $scope.selectedReportOptionList.push($scope.reportOptionList[i]);
                    }
                }
                itemsService.getSearch($scope.search, $scope.selectedbillSettingsList, $scope.selectedReportOptionList).then(
                    function (response) {
                        $scope.GettingSearchResult(response.data);
                    }, function (error) {
                        console.log(error.data.message);
                        swal('عفواً',
                        'لا يوجد نتيجة للبحث',
                        'warning');
                    });
            } else {
                swal('عفواً',
                        'يجب اختيار واحد علي الاقل من مصادر التقارير',
                        'error');
            }
        } else {
            swal('خطأ',
                        'يجب اختيار صنف وفرع',
                        'error');
        }
    };

    $scope.GettingSearchResult = function (entity) {
        if (entity !== undefined && entity !== null) {
            var quantity = 0;
            $scope.searchList = [];
            $scope.MaxPrice1 = 0;
            $scope.MaxPrice2 = 0;
            $scope.MinPrice1 = -1;
            $scope.MinPrice2 = -1;
            $scope.TotalResult1 = 0;
            $scope.TotalResult2 = 0;
            $scope.TotalResult3 = 0;
            $scope.TotalQTY1 = 0;
            $scope.TotalQTY2 = 0;
            $scope.AveragePrice1 = 0;
            $scope.AveragePrice2 = 0;
            for (var i = 0; i < entity.length; i++) {
                $scope.searchItem = {};
                if (entity[i].BILL_TYPE == 1 || entity[i].BILL_TYPE == 9 || entity[i].BILL_TYPE == 4 || entity[i].BILL_TYPE == 5 ||
                    entity[i].BILL_TYPE == 14 || entity[i].BILL_TYPE == 18 || entity[i].BILL_TYPE == 19 || entity[i].BILL_TYPE == 20) {
                    $scope.searchItem.QTY1 = entity[i].QTY;
                    $scope.searchItem.Price1 = entity[i].UNIT_PRICE;
                    $scope.searchItem.Result1 = $scope.searchItem.QTY1 * $scope.searchItem.Price1;
                    $scope.searchItem.QTY2 = "";
                    $scope.searchItem.Price2 = "";
                    $scope.searchItem.Result2 = "";
                    $scope.MinPrice1 = $scope.MinPrice1 === -1 ? $scope.searchItem.Price1 === "" ? -1 : $scope.searchItem.Price1 : $scope.MinPrice1;
                    if ($scope.MinPrice1 != -1)
                        $scope.MinPrice1 = Math.min($scope.MinPrice1, $scope.searchItem.Price1);
                }
                if (entity[i].BILL_TYPE == 2 || entity[i].BILL_TYPE == 3 || entity[i].BILL_TYPE == 6 || entity[i].BILL_TYPE == 10 ||
                    entity[i].BILL_TYPE == 13 || entity[i].BILL_TYPE == 17 || entity[i].BILL_TYPE == 23 || entity[i].BILL_TYPE == 24) {
                    $scope.searchItem.QTY2 = entity[i].QTY;
                    $scope.searchItem.Price2 = entity[i].UNIT_PRICE;
                    $scope.searchItem.Result2 = $scope.searchItem.QTY2 * $scope.searchItem.Price2;
                    $scope.searchItem.QTY1 = "";
                    $scope.searchItem.Price1 = "";
                    $scope.searchItem.Result1 = "";
                    $scope.MinPrice2 = $scope.MinPrice2 === -1 ? $scope.searchItem.Price2 === "" ? -1 : $scope.searchItem.Price2 : $scope.MinPrice2;
                    if ($scope.MinPrice2 != -1)
                        $scope.MinPrice2 = Math.min($scope.MinPrice2, $scope.searchItem.Price2);
                }
                quantity += $scope.searchItem.QTY1 - $scope.searchItem.QTY2;
                $scope.searchItem.QTY3 = quantity;
                $scope.searchItem.Price3 = "0";
                $scope.searchItem.Result3 = $scope.searchItem.QTY3 * $scope.searchItem.Price3;
                $scope.searchItem.BILL_ABRV_AR = entity[i].BILL_ABRV_AR;
                $scope.searchItem.BILL_NUMBER = entity[i].BILL_NUMBER;
                $scope.searchItem.BILL_DATE = entity[i].BILL_DATE;
                $scope.searchItem.ACC_CODE = entity[i].ACC_CODE;
                $scope.searchItem.ACC_AR_NAME = entity[i].ACC_AR_NAME;
                $scope.searchItem.UNIT_CODE = entity[i].UNIT_CODE;
                $scope.searchItem.UNIT_AR_NAME = entity[i].UNIT_AR_NAME;

                $scope.searchList.push($scope.searchItem);

                $scope.MaxPrice1 = Math.max($scope.MaxPrice1, $scope.searchItem.Price1);
                $scope.MaxPrice2 = Math.max($scope.MaxPrice2, $scope.searchItem.Price2);

                $scope.TotalResult1 += +$scope.searchItem.Result1;
                $scope.TotalResult2 += +$scope.searchItem.Result2;
                $scope.TotalResult3 += +$scope.searchItem.Result3;
                $scope.TotalQTY1 += +$scope.searchItem.QTY1;
                $scope.TotalQTY2 += +$scope.searchItem.QTY2;
                $scope.TotalQTY3 = quantity;
            }
            debugger;
            $scope.MinPrice1 = $scope.MinPrice1 == -1 ? 0 : $scope.MinPrice1;
            $scope.MinPrice2 = $scope.MinPrice2 == -1 ? 0 : $scope.MinPrice2;
            $scope.AveragePrice1 = ($scope.MaxPrice1 + $scope.MinPrice1) / 2;
            $scope.AveragePrice2 = ($scope.MaxPrice2 + $scope.MinPrice2) / 2;
            $scope.count = $scope.searchList.length;
            $scope.searchList_print = $scope.searchList;
        }
    }

    $scope.GetBillSettings = function () {
        billSettingService.getAll().then(function (response) {
            $scope.billSettingsList = response.data;
        }, function (error) { console.log(error.data.message); });
    };

    $scope.GetItems = function () {
        itemsService.getAll().then(function (response) {
            $scope.itemsList = response.data;
        }, function (error) { console.log(error.data.message); });
    };

    $scope.GetCompanyStores = function () {
        companyStoresService.getAll().then(function (response) {
            $scope.companyStoresList = response.data;
        }, function (error) { console.log(error.data.message); });
    };

    $scope.GetCurrencies = function () {
        currencyService.getAll().then(function (response) {
            $scope.currencyList = response.data;
        }, function (error) { console.log(error.data.message); });
    };

    $scope.GetCostCenters = function () {
        costCentersService.getAll().then(function (response) {
            $scope.costCentersList = response.data;
        }, function (error) { console.log(error.data.message); });
    };

    $scope.GetEmployeeSales = function () {
        employeesService.getByTypeID(1).then(function (response) {
            $scope.EmpList = response.data;
        }, function (error) { console.log(error.data.message); });
    };

    $scope.getAllOnLoad = function () {
        $q.all(
            [
                $scope.getCurrentDate(),
                $scope.GetUserBranches(),
                //$scope.GetItems(),
                //$scope.GetCompanyStores(),
                $scope.GetCurrencies(),
                //$scope.GetCostCenters(),
                //$scope.GetEmployeeSales(),
                $scope.GetBillSettings()
            ]).then(function (allResponses) {
            }, function (error) {
            });
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

    $scope.getCurrentDate = function () {
        var date = new Date();
        //$scope.search.dateFrom = new Date(date.getFullYear() + '-' + ('0' + (date.getMonth() + 1)).slice(-2) + '-' + ('0' + date.getDate()).slice(-2));
        //$scope.search.dateTo = new Date(date.getFullYear() + '-' + ('0' + (date.getMonth() + 1)).slice(-2) + '-' + ('0' + date.getDate()).slice(-2));
        $scope.search.dateFrom = date;
        $scope.search.dateTo = date;
    }

    $scope.getCurrencyRate = function () {
        if ($scope.search.currency != "" && $scope.search.currency != undefined) {
            var curID = $scope.search.currency.CURRENCY_ID;
            if (curID != "" && curID != undefined)
                currencyService.getCurrencyRate(curID).then(
                    function (result) {
                        $scope.search.currencyRate = result.data;
                    }, function (error) {
                        console.log(error.data.message);
                    });
            else $scope.search.currencyRate = "";
        } else $scope.search.currencyRate = "";
    };


    /*******************************************************************************************************************/
    /*Choose Item*/
    /*******************************************************************************************************************/
    $scope.openItm = function () {
        var parentElem = angular.element($document[0].querySelector('.app-content'));
        $scope.modalOpenItm = false;
        var modalInstance = $uibModal.open({
            templateUrl: 'itemModal.html',
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

    $scope.searchForItm = function () {
        var search = $scope.search.itemName;
        search = search == undefined || search == "" ? "" : search;
        //if (search != null && search != undefined && search != "")
        itemRepoService.getSearchForItem(search).then(function (response) {
            $scope.searchItems = response.data;
            if ($scope.searchItems.length == 1) {
                $scope.search.itemName = $scope.searchItems[0].ITEM_AR_NAME;
                $scope.search.itemID = $scope.searchItems[0].ITEM_ID;
                $scope.search.itemUnitID = $scope.searchItems[0].Unit_ID;
            }
            else if ($scope.searchItems.length == 0) {
                $scope.search.itemName = "";
                $scope.search.itemID = "";
                $scope.search.itemUnitID = "";
                return;
            }
            else {
                if ($scope.modalOpenItm === false || $scope.modalOpenItm == undefined) { $scope.openItm(); }

            }
        }, function (err) {
            console.log(err.data.message);
        });
    }

    $scope.addSelectedItm = function (itm) {
        $scope.search.itemName = itm.ITEM_AR_NAME;
        $scope.search.itemID = itm.ITEM_ID;
        $scope.search.itemUnitID = itm.Unit_ID;
        $uibModalStack.dismissAll();
    };


    /*******************************************************************************************************************/
    /*Choose Store*/
    /*******************************************************************************************************************/
    $scope.openStore = function () {
        var parentElem = angular.element($document[0].querySelector('.app-content'));
        $scope.modalOpenStor = false;
        var modalInstance = $uibModal.open({
            templateUrl: 'storeModal.html',
            controller: 'modalCtrl',
            scope: $scope,
            size: 'md',
            preserveScope: true
        });
        modalInstance.opened.then(function () {
            $scope.modalOpenStor = true;
        });

        // we want to update state whether the modal closed or was dismissed,
        // so use finally to handle both resolved and rejected promises.
        modalInstance.result.finally(function (selectedItem) {
            $scope.modalOpenStor = false;
        });
    };

    $scope.searchForStore = function () {
        var search = $scope.search.storeName;
        search = search == undefined || search == "" ? "" : search;
        //if (search != null && search != undefined && search != "")
        profitService.getSearchForStore(search).then(function (response) {
            $scope.searchStores = response.data;
            if ($scope.searchStores.length == 1) {
                $scope.search.storeName = $scope.searchStores[0].COM_STORE_AR_NAME;
                $scope.search.storeID = $scope.searchStores[0].COM_STORE_ID;
            }
            else if ($scope.searchStores.length == 0) {
                $scope.search.storeName = "";
                $scope.search.storeID = "";
                return;
            }
            else {
                if ($scope.modalOpenStor === false || $scope.modalOpenStor == undefined) { $scope.openStore(); }

            }
        }, function (err) {
            console.log(err.data.message);
        });
    }

    $scope.addSelectedStore = function (store) {
        $scope.search.storeName = store.COM_STORE_AR_NAME;
        $scope.search.storeID = store.COM_STORE_ID;
        $uibModalStack.dismissAll();
    };

    /*******************************************************************************************************************/
    /*Choose Employee*/
    /*******************************************************************************************************************/
    $scope.openEmp = function () {
        var parentElem = angular.element($document[0].querySelector('.app-content'));
        $scope.modalOpenEmp = false;
        var modalInstance = $uibModal.open({
            templateUrl: 'EmployeeModal.html',
            controller: 'modalCtrl',
            scope: $scope,
            size: 'md',
            preserveScope: true
        });
        modalInstance.opened.then(function () {
            $scope.modalOpenEmp = true;
        });

        // we want to update state whether the modal closed or was dismissed,
        // so use finally to handle both resolved and rejected promises.
        modalInstance.result.finally(function (selectedItem) {
            $scope.modalOpenEmp = false;
        });
    };

    $scope.searchForEmployee = function () {
        var search = $scope.search.EmployeeName;
        search = search == undefined || search == "" ? "" : search;
        //if (search != null && search != undefined && search != "")
        profitService.getSearchForEmployee(search, 1).then(function (response) {
            $scope.searchEmployeess = response.data;
            if ($scope.searchEmployeess.length == 1) {
                $scope.search.EmployeeName = $scope.searchEmployeess[0].EMP_AR_NAME;
                $scope.search.EmployeeID = $scope.searchEmployeess[0].EMP_ID;
            }
            else if ($scope.searchEmployeess.length == 0) {
                $scope.search.EmployeeName = "";
                $scope.search.EmployeeID = "";
                return;
            }
            else {
                if ($scope.modalOpenEmp === false || $scope.modalOpenEmp == undefined) { $scope.openEmp(); }

            }
        }, function (err) {
            console.log(err.data.message);
        });
    }

    $scope.addSelectedEmp = function (Emp) {
        $scope.search.EmployeeName = Emp.EMP_AR_NAME;
        $scope.search.EmployeeID = Emp.EMP_ID;
        $uibModalStack.dismissAll();
    };

    /*******************************************************************************************************************/
    /*Choose CostCenter*/
    /*******************************************************************************************************************/
    $scope.openCostCenter = function () {
        var parentElem = angular.element($document[0].querySelector('.app-content'));
        $scope.modalOpenCostCenter = false;
        var modalInstance = $uibModal.open({
            templateUrl: 'CostCenterModal.html',
            controller: 'modalCtrl',
            scope: $scope,
            size: 'md',
            preserveScope: true
        });
        modalInstance.opened.then(function () {
            $scope.modalOpenCostCenter = true;
        });

        // we want to update state whether the modal closed or was dismissed,
        // so use finally to handle both resolved and rejected promises.
        modalInstance.result.finally(function (selectedItem) {
            $scope.modalOpenCostCenter = false;
        });
    };

    $scope.searchForCostCenter = function () {
        var search = $scope.search.CostCenterName;
        search = search == undefined || search == "" ? "" : search;
        //if (search != null && search != undefined && search != "")
        profitService.getSearchForCostCenter(search).then(function (response) {
            $scope.searchCostCenters = response.data;
            if ($scope.searchCostCenters.length == 1) {
                $scope.search.CostCenterName = $scope.searchCostCenters[0].COST_CENTER_AR_NAME;
                $scope.search.CostCenterID = $scope.searchCostCenters[0].COST_CENTER_ID;
            }
            else if ($scope.searchCostCenters.length == 0) {
                $scope.search.CostCenterName = "";
                $scope.search.CostCenterID = "";
                return;
            }
            else {
                if ($scope.modalOpenCostCenter === false || $scope.modalOpenCostCenter == undefined) { $scope.openCostCenter(); }

            }
        }, function (err) {
            console.log(err.data.message);
        });
    }

    $scope.addSelectedCostCenter = function (CostCenter) {
        $scope.search.CostCenterName = CostCenter.COST_CENTER_AR_NAME;
        $scope.search.CostCenterID = CostCenter.COST_CENTER_ID;
        $uibModalStack.dismissAll();
    };

    $scope.addSelectedEmployee = function (emp) {
        $scope.search.EmployeeName = emp.EMP_AR_NAME;
        $scope.search.employeeID = emp.EMP_ID;
        $uibModalStack.dismissAll();
    };

    $scope.onSelect = function (item, model, label) {

        console.log(item);

        $scope.search.currency = item;

        $scope.getCurrencyRate();
    };

    $scope.fnPrintReport = function () {


        var item_name = $("#itemName").val();

        var arabic = /[\u0600-\u06FF]/;

        var x1 = "تقرير حركة صنف".split(" ").reverse().join(" ");

        var x2 = ("اسم الصنف : " + item_name).split(" ").reverse().join(" ");

        if (arabic.test(item_name) == false) {

            x2 = ("اسم الصنف : ").split(" ").reverse().join(" ");

            x2 = item_name + " " + x2;
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
                        widths: [80, 45, 45, 45, 45, 180, 45, 45, 45, 45, 45, 100, 180],
                        margin: [400, 400, 0, 0],
                        body: $scope.createArr($scope.searchList_print)
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

        arr[0][0] = "الوحدة";
        arr[0][1] = "الإجمالي";
        arr[0][2] = "السعر";
        arr[0][3] = "الكمية";

        arr[0][4] = "الإجمالي";
        arr[0][5] = "العميل";
        arr[0][6] = "السعر";
        arr[0][7] = "الكمية";

        arr[0][8] = "الإجمالي";
        arr[0][9] = "السعر";
        arr[0][10] = "الكمية";
        arr[0][11] = "التاريخ";

        arr[0][12] = "الفاتورة";

        for (var i = 0; i < 13; i++) {

            arr[0][i] = arr[0][i].split(" ").reverse().join(" ");
        }

        var index = 1;

        for (var i = 0; i < list.length; i++) {

            var obj = list[i];

            var j = 0;
            for (var key in obj) {
                if (obj.hasOwnProperty(key)) {


                    if (key == "BILL_NUMBER") {
                        arr[index][12] = (obj["BILL_ABRV_AR"] + " - " + obj[key]).split(" ").reverse().join(" ");
                    }
                    else if (key == "BILL_DATE") {

                        var d1 = new Date(obj[key]);

                        var account_date = d1.getFullYear() + "-" + ("0" + (d1.getMonth() + 1)).slice(-2) + "-" + ("0" + d1.getDate()).slice(-2);

                        arr[index][11] = account_date;
                    }
                    else if (key == "QTY1") {

                        arr[index][10] = obj[key];
                    }
                    else if (key == "Price1") {

                        arr[index][9] = Number(obj[key]).toFixed(2);
                    }
                    ////////////////////////////
                    else if (key == "Result1") {
                        arr[index][8] = Number(obj[key]).toFixed(2);
                    }
                    else if (key == "QTY2") {

                        arr[index][7] = obj[key];
                    }
                    else if (key == "Price2") {

                        arr[index][6] = Number(obj[key]).toFixed(2);
                    }
                    else if (key == "ACC_CODE") {

                        if (obj["ACC_AR_NAME"]) {
                            arr[index][5] = (obj["ACC_AR_NAME"] + " - " + obj[key]).split(" ").reverse().join(" ");
                        }
                        else {
                            arr[index][5] = obj[key];
                        }
                    }
                    ////////////////////////////////
                    else if (key == "Result2") {
                        arr[index][4] = Number(obj[key]).toFixed(2);
                    }
                    else if (key == "QTY3") {

                        arr[index][3] = obj[key];
                    }
                    else if (key == "Price3") {

                        arr[index][2] = Number(obj[key]).toFixed(2);
                    }
                    else if (key == "Result3") {

                        arr[index][1] = Number(obj[key]).toFixed(2);
                    }
                    //////////////////////////////
                    else if (key == "UNIT_CODE") {

                        arr[index][0] = (Number(obj[key]).toFixed(2) + "  " + obj["UNIT_AR_NAME"]).split(" ").reverse().join(" ");
                    }

                    j++;

                }
            }
            index++;
        }
        return arr;
    }

}]);