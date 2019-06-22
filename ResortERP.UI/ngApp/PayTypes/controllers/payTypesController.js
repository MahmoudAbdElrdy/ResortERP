'use strict'
app.controller('payTypesController', ['$scope', '$q', '$log', '$window', 'authService', 'payTypesService', 'accountService', '$location', 'accountsService', function ($scope, $q, $log, $window, authService, payTypesService, accountService, $location, accountsService) {

    $scope.payType = {
        PAY_TYPE_ID: null, PAY_TYPE_CODE: "", PAY_TYPE_AR_NAME: "", PAY_TYPE_EN_NAME: "", PAY_TYPE_NOTES: "", AddedBy: "", AddedOn: null, UpdatedBy: ""
        , UpdatedOn: null, Disable: false, AccountID: null, BankCommissionRate: null, MaxCommission: null, CommissionTaxRate: null, AccountName: ""
    };

    $scope.payTypeList = [];
    $scope.pageNum = 1;
    $scope.pageSize = 10;
    $scope.pagesCount = 0;
    $scope.maxSize = 5;
    $scope.payTypeTotalCount = 0;
    $scope.nationList = [];
    $scope.acountList = [];

    $scope.COM_BRN_IDForFilter = 0;

    //$scope.tree = [{ name: "Node1", nodes: [] }, { name: "Node2", nodes: [] }];
    //$scope.deleteTree = function (data) {
    //    data.nodes = [];
    //};
    //$scope.addTree = function (data) {
    //    var post = data.nodes.length + 1;
    //    var newName = data.name + '-' + post;
    //    data.nodes.push({ name: newName, nodes: [] });
    //};
    //$scope.treebtn = function () {

    //}

    //$scope.model = [
    //    {
    //        name: 'Item 1 Name',
    //        children: [
    //            {
    //                name: 'Item 2 Name'
    //            }, {
    //                name: 'Item 3 Name'
    //            }
    //        ]
    //    }
    //];      







    $scope.get = function (pageNum, pageSize) {
        debugger;

        return payTypesService.get(pageNum, pageSize);
    }


    $scope.count = function () {
        return payTypesService.count();
    }

    $scope.add = function (entity) {
        return payTypesService.add(entity);
    }

    $scope.update = function (entity) {
        return payTypesService.update(entity);
    }

    $scope.delete = function (entity) {
        return payTypesService.delete(entity);
    }

    $scope.getpayTypesList = function () {
        debugger;
        $scope.get($scope.pageNum, $scope.pageSize).then(function (result) {
            // $window.alert(result.data);
            $scope.payTypeList = result.data;

        }, function (error) { });
    }

    $scope.getAllOnLoad = function () {
        $q.all([
            $scope.getpayTypesList(),
            $scope.getAccountList(),
            $scope.getPaytypeTotalCount(),
            $scope.getlastCode(),
            $scope.getCurrentDate()
        ]).then(function (allResponses) {

            var urlParams = $location.search();
            if (urlParams.foo != null && urlParams.foo != undefined && urlParams.foo != "") {

                payTypesService.getByID(parseInt(urlParams.foo)).then(function (results) {
                    $scope.payType = results.data;
                    $scope.editEntity(results.data);
                })
            }

            var urlParams = $location.search();
            if (urlParams.foo != null && urlParams.foo != undefined && urlParams.foo != "") {
                $.each($scope.payTypeList, function (index, value) {
                    if (value.PAY_TYPE_ID == parseInt(urlParams.foo)) {
                        $scope.payType = value;
                        $scope.editEntity(value);
                    }
                });

            }

        }, function (error) {
            //This will be called if $q.all finds any of the requests erroring.
            var abc = error;
            var def = 99;
        });
    }


    $scope.payTypesPageLoad = function () {
        $scope.getAllOnLoad();
    }

    $scope.clearData = function () {
        $scope.payType = {
            PAY_TYPE_ID: null, PAY_TYPE_CODE: "", PAY_TYPE_AR_NAME: "", PAY_TYPE_EN_NAME: "", PAY_TYPE_NOTES: "", Notes: "", AddedBy: "", AddedOn: null, UpdatedBy: ""
            , UpdatedOn: null, Disable: false, AccountID: null, BankCommissionRate: null, MaxCommission: null, CommissionTaxRate: null, AccountName: ""
        };
        $scope.getlastCode();
    }

    $scope.refreshData = function () {
        $scope.getAllOnLoad();
    }

    $scope.saveEntity = function () {
        if ($scope.payType !== undefined && $scope.payType !== null && $scope.payType.PAY_TYPE_CODE !== "" && $scope.payType.PAY_TYPE_AR_NAME !== "" && $scope.payType.PAY_TYPE_EN_NAME !== "") {
            if ($scope.payType.PAY_TYPE_ID === 0 || $scope.payType.PAY_TYPE_ID === null) {

                $scope.add($scope.payType).then(function (result) {
                    $scope.clearData();
                    $scope.refreshData();
                    swal({
                        title: 'تم',
                        text: 'حفظ بيانات  طريقة الدفع',
                        type: 'success',
                        timer: 1500,
                        showConfirmButton: false
                    })
                }, function (error) {
                    swal('عفواً',
                        'حدث خطأ عند حفظ بيانات طريقة الدفع',
                        'error');
                });
            }

            else {
                $scope.update($scope.payType).then(function (result) {
                    $scope.clearData();
                    $scope.refreshData();
                    swal({
                        title: 'تم',
                        text: 'تعديل بيانات  طريقة الدفع',
                        type: 'success',
                        timer: 1500,
                        showConfirmButton: false
                    })
                }, function (error) {
                    swal('عفواً',
                        'حدث خطأ عند تعديل بيانات طريقة الدفع',
                        'error');
                });
            }
        }
    }


    $scope.savePayType = function () {
        $scope.saveEntity();
    }

    $scope.editEntity = function (entity) {
        $scope.payType = entity;
        $scope.payType.AddedOn = Date(entity.AddedOn);
    }

    $scope.deleteEntity = function (entity) {
        swal({
            title: 'هل تريد حذف طريقة الدفع؟',
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
                // alert(results.data);
                if (results.data == false) {
                    swal({
                        title: "تحذير",
                        text: "طريقة الدفع تم عليها عمليات مختلفة. لا تستطيع حذفها",
                        type: "warning",
                        showConfirmButton: true

                    })
                } else {
                    $scope.clearData();
                    $scope.refreshData();
                    swal({
                        title: 'تم',
                        text: 'الحذف بنجاح',
                        type: 'success',
                        timer: 1500,
                        showConfirmButton: false
                    });
                }

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
        //swal({
        //    title: 'هل تريد حذف طريقة الدفع؟',
        //    text: "لن تستطيع عكس عملية الحذف مرة أخري",
        //    type: 'warning',
        //    showCancelButton: true,
        //    confirmButtonColor: '#3085d6',
        //    cancelButtonColor: '#d33',
        //    confirmButtonText: 'نعم',
        //    cancelButtonText: 'الغاء',
        //    confirmButtonClass: 'btn btn-success btn-lg',
        //    cancelButtonClass: 'btn btn-danger btn-lg',
        //    buttonsStyling: false
        //}).then(function () {
        //    $scope.delete(entity).then(function (results) {
        //        $scope.clearData();
        //        $scope.refreshData();
        //        swal({
        //            title: 'تم',
        //            text: 'الحذف بنجاح',
        //            type: 'success',
        //            timer: 1500,
        //            showConfirmButton: false
        //        });
        //    }, function (error) {
        //        console.log(error.data.message);
        //    });
        //}, function (dismiss) {
        //    // dismiss can be 'cancel', 'overlay',
        //    // 'close', and 'timer'
        //    if (dismiss === 'cancel') {
        //        swal({
        //            title: 'تم',
        //            text: 'الغاء عملية الحذف',
        //            type: 'error',
        //            timer: 1500,
        //            showConfirmButton: false
        //        });
        //    }
        //})
    }


    $scope.getAccountList = function () {

       // var billType = billSettigObj.BILL_TYPE_ID;
        accountsService.getAccountBoxByTypesForBill(0, "pay", $scope.COM_BRN_IDForFilter).then(function (result) {
            $scope.acountList = result.data;

        });


        //accountService.getAll().then(function (result) {
        //    $scope.acountList = result.data;

        //}, function (error) { })
    }


    $scope.getPaytypeTotalCount = function () {
        debugger;
        $scope.count().then(function (result) {
            var data = result.data;
            $scope.payTypeTotalCount = data;
            $scope.pagesCount = $scope.getPagesCount();

        }
            , function (error) {
                consol.log(error.data.message);
            });
    }


    $scope.getPagesCount = function () {
        debugger;
        var div = Math.floor($scope.payTypeTotalCount / $scope.pageSize);
        var rem = $scope.payTypeTotalCount % $scope.pageSize;
        if (rem > 0) {
            div = div + 1;
        }
        $scope.pagesCount = div;
        return div;
    }

    $scope.pageChanged = function () {
        $scope.getAllOnLoad();
        $log.log('Page changed to: ' + $scope.pageNum);
    };

    ///////////////////////////get last code
    $scope.getlastCode = function () {
        var urlParams = $location.search();
        if (urlParams.foo != null && urlParams.foo != undefined && urlParams.foo != "") { } else {
            payTypesService.getLastCode().then(function (result) {
                $scope.payType.PAY_TYPE_CODE = parseInt(result.data) + 1;
            }, function (error) { });
        }
    }

    $scope.getCurrentDate = function () {

        var date = new Date();
       
        $scope.payType.AddedOn = new Date(date);
    }


 
}]);