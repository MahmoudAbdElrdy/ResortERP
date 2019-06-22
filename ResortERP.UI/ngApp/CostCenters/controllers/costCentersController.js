'use strict'
app.controller('costCentersController', ['$scope', '$location', '$log', '$q', 'authService', 'costService', function ($scope, $location, $log, $q, authService, costService) {
    $scope.costCenters = { COST_CENTER_ID: null, COST_CENTER_CODE: "", COST_CENTER_AR_NAME: "", COST_CENTER_EN_NAME: "", COST_CENTER_MASTER_ID: null, COST_CENTER_REMARKS: "", AddedBy: "", AddedOn: null, UpdatedBy: "", updatedOn: null, Disable: false };
    $scope.costCentersList = [];
    $scope.costCentersTotalCount = 0;
    $scope.pageNum = 1;
    $scope.pageSize = 10;
    $scope.pagesCount = 0;
    $scope.maxSize = 5;


    $scope.get = function (pageNum, pageSize) {

        return costService.get(pageNum, pageSize);
    }

    $scope.getCostCentersList = function () {
        $scope.get($scope.pageNum, $scope.pageSize).then(function (results) {
            var data = results.data;
            $scope.costCentersList = data;
            var urlParams = $location.search();
            if (urlParams.new != null && urlParams.new != undefined && urlParams.new != "") {
                costService.getById(urlParams.new).then(function (result) {
                    $scope.costCenters.COST_CENTER_MASTER_ID = result.data.COST_CENTER_ID;
                });
            }
        }, function (error) {
            console.log(error.data.message);
        });
    }

    $scope.getAllOnLoad = function () {

        $q.all([

            $scope.getCostCentersList(),
            $scope.getMainCostCenters(),
            $scope.getCostCenterTotalCount(),
            $scope.getLastCode()
        ])
            .then(function (respons) {
                        // $scope.account.ACC_CODE = results.data.ACC_CODE;
                var urlParams = $location.search();
                if (urlParams.foo != null && urlParams.foo != undefined && urlParams.foo != "") {

                    costService.getById(parseInt(urlParams.foo)).then(function (results) {
                        $scope.costCenters = results.data;
                        $scope.loadData(results.data);
                    })

                }
      
            }, function (error) {
                var abc = error;
                var def = 99;
            });
    }

    $scope.costCentersPageLoad = function () {

        $scope.getAllOnLoad();
    }

    $scope.getMainCostCenters = function () {

        costService.getMainCostCenters().then(function (result) {
            $scope.mainCostCenterList = result.data;
        })
    }

    $scope.clearEnity = function () {

        $scope.costCenters = { COST_CENTER_ID: null, COST_CENTER_CODE: "", COST_CENTER_AR_NAME: "", COST_CENTER_EN_NAME: "", COST_CENTER_MASTER_ID: null, COST_CENTER_REMARKS: "", AddedBy: "", AddedOn: null, UpdatedBy: "", updatedOn: null, Disable: false };
        $scope.getLastCode();
    }

    $scope.refreshData = function () {
        $scope.getAllOnLoad();
    }


    $scope.saveEntity = function () {
        debugger;
        if ($scope.costCenters !== undefined && $scope.costCenters !== null && $scope.costCenters.COST_CENTER_CODE !== "" && $scope.costCenters.COST_CENTER_AR_NAME !== "" && $scope.costCenters.COST_CENTER_EN_NAME) {
            if ($scope.costCenters.COST_CENTER_ID === null || $scope.costCenters.COST_CENTER_ID === 0) {
                //add

                var userName = authService.GetUserName();
                $scope.costCenters.AddedBy = userName;

                var addedDate = new Date();
                $scope.costCenters.AddedOn = addedDate;


                $scope.add($scope.costCenters).then(function (result) {
                    $scope.clearEnity();
                    $scope.refreshData();
                    swal({
                        title: 'تم',
                        text: 'حفظ بيانات مراكز التكلفه بنجاح',
                        type: 'success',
                        timer: 1500,
                        showConfirmButton: false
                    })
                }, function (error) {
                    swal('عفواً',
                        'حدث خطأ عند حفظ بيانات مركز التكلفه',
                        'error');
                });

            }
            else { //update
                var userName = authService.GetUserName();
                $scope.costCenters.UpdatedBy = userName;

                var updatedDate = new Date();
                $scope.costCenters.updatedOn = updatedDate;

                $scope.update($scope.costCenters).then(function (result) {
                    $scope.clearEnity();
                    $scope.refreshData();
                    swal({
                        title: 'تم',
                        Text: 'تعديل بيانات مراكز التكلفه',
                        type: 'success',
                        timer: 1500,
                        showConfirmButton: false
                    })
                }, function (error) {
                    console.log(error.data.message);
                    swal('عفواً',
                        'حدث خطأ عند تعديل بيانات مراكز التكلفه',
                        'error');
                });
            }
        }
    }

    $scope.saveCostCenter = function () {

        $scope.saveEntity();
    }

    $scope.loadData = function (entity) {

        $scope.costCenters = entity;
    }

    $scope.deleteEntity = function (entity) {
        swal({
            title: 'هل تريد حذف مراكز التكلفه؟',
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
                        text: "مركز التكلفة هذا تمت عليه عمليات مختلفة. لا تستطيع حذفه",
                        type: "warning",
                        showConfirmButton: true

                    })
                } else {
                    $scope.clearEnity();
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
        //debugger;
        //swal({
        //    title: 'هل تريد حذف مراكز التكلفه؟',
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
        //        $scope.clearEnity();
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

    $scope.getPagesCount = function () {
        debugger;
        var div = Math.floor($scope.costCentersTotalCount / $scope.pageSize);
        var rem = $scope.costCentersTotalCount % $scope.pageSize;
        if (rem > 0) {
            div = div + 1;
        }
        $scope.pagesCount = div;
        return div;
    }

    $scope.getCostCenterTotalCount = function () {
        debugger;
        $scope.count().then(function (result) {
            var data = result.data;
            $scope.costCentersTotalCount = data;
            $scope.pagesCount = $scope.getPagesCount();

        }
            , function (error) {
                consol.log(error.data.message);
            });
    }


    $scope.add = function (entity) {

        return costService.add(entity);
    }

    $scope.update = function (entity) {
        debugger;
        return costService.update(entity);
    }

    $scope.delete = function (entity) {
        return costService.delete(entity);
    }

    $scope.count = function () {
        debugger;
        return costService.count();
    }

    $scope.pageChanged = function () {
        $scope.getAllOnLoad();
        $log.log('Page changed to: ' + $scope.pageNum);
    };

    $scope.getLastCode = function () {
        var urlParams = $location.search();
        if (urlParams.foo != null && urlParams.foo != undefined && urlParams.foo != "") { } else {
            costService.getLastCode().then(function (result) {
                $scope.costCenters.COST_CENTER_CODE = parseInt(result.data) + 1;
            }, function (error) { });
        }
    }

}]);