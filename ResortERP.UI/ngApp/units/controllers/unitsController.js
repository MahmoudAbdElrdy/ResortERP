'use strict';
app.controller('unitsController', ['$scope', '$location', '$log', '$q', 'authService', 'unitsService', function ($scope, $location, $log, $q, authService, unitsService) {

    $scope.units = { UNIT_ID: null, UNIT_CODE: "", UNIT_AR_NAME: "", UNIT_EN_NAME: "", UNIT_REMARKS: "", AddedBy: "", AddedOn: null, UpdatedBy: "", UpdatedOn: null, Disable: false };
    $scope.unitsList = [];


    $scope.unitsTotalCount = 0;
    $scope.pageNum = 1;
    $scope.pageSize = 10;

    $scope.pagesCount = 0;
    $scope.getPagesCount = function () {
        var div = Math.floor($scope.unitsTotalCount / $scope.pageSize);
        var rem = $scope.unitsTotalCount % $scope.pageSize;
        if (rem > 0) {
            div = div + 1;
        }
        return div;
    }
    $scope.maxSize = 5;



    $scope.clearEnity = function () {
        $scope.units = { UNIT_ID: null, UNIT_CODE: "", UNIT_AR_NAME: "", UNIT_EN_NAME: "", UNIT_REMARKS: "", AddedBy: "", AddedOn: null, UpdatedBy: "", UpdatedOn: null, Disable: false };
        $scope.getlastCode();
    }

    $scope.refreshData = function () {
        $scope.getAllOnLoad();
    }
    $scope.saveUnitsGroup = function () {

        $scope.saveEntity();
    }
    $scope.saveEntity = function () {

        if ($scope.units !== undefined && $scope.units !== null && $scope.units.UNIT_CODE !== "" && $scope.units.UNIT_AR_NAME !== "" && $scope.units.UNIT_EN_NAME !== "") {
            if ($scope.units.UNIT_ID === null || $scope.UNIT_ID === 0) {
                $scope.insert($scope.units).then(function (results) {
                    $scope.clearEnity();
                    $scope.refreshData();
                    swal({
                        title: 'تم',
                        text: 'حفظ الوحدة بنجاح',
                        type: 'success',
                        timer: 1500,
                        showConfirmButton: false
                    });
                }, function (error) {
                    console.log(error.data.message);
                    swal('عفواً',
                        'حدث خطأ عند حفظ الوحدة',
                        'error');
                });
            } else {
                $scope.update($scope.units).then(function (results) {
                    $scope.clearEnity();
                    $scope.refreshData();
                    swal({
                        title: 'تم',
                        text: 'تعديل الوحدة بنجاح',
                        type: 'success',
                        timer: 1500,
                        showConfirmButton: false
                    });
                }, function (error) {
                    console.log(error.data.message);
                    swal('عفواً',
                        'حدث خطأ عند تعديل الوحدة',
                        'error');
                });
            }
        }
    }

    $scope.dirEnity = function (entity) {
        $scope.units = entity;
    }
    $scope.deleteEnity = function (entity) {
        swal({
            title: 'هل تريد حذف الوحدة؟',
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
                        text: "هذه الوحدة تم عليه عمليات مختلفة. لا تستطيع حذفها",
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
        //swal({
        //    title: 'هل تريد حذف الوحدة؟',
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
    $scope.getAllOnLoad = function () {
        $q.all(
            [
                 $scope.getunitsList(),
                 $scope.getunitsTotalCount(),
                 $scope.getlastCode(),
            ])
            .then(function (allResponses) {

                var urlParams = $location.search();
                if (urlParams.foo != null && urlParams.foo != undefined && urlParams.foo != "") {
                    unitsService.getById(parseInt(urlParams.foo)).then(function (results) {
                        $scope.dirEnity(results.data);
                    });

                }

            }, function (error) {
                //This will be called if $q.all finds any of the requests erroring.
                var abc = error;
                var def = 99;
            });
    }
    $scope.getunitsTotalCount = function () {
        $scope.count().then(function (results) {
            var data = results.data;
            $scope.unitsTotalCount = data;
            $scope.pagesCount = $scope.getPagesCount();
        }, function (error) {

            console.log(error.data.message);
        });
    }
    $scope.getunitsList = function () {
        $scope.get($scope.pageNum, $scope.pageSize).then(function (results) {
            var data = results.data;
            $scope.unitsList = data;

        }, function (error) {

            console.log(error.data.message);
        });
    }

    $scope.get = function (pageNum, pageSize) {
        return unitsService.get(pageNum, pageSize);
    }
    $scope.count = function () {
        return unitsService.count();
    }
    $scope.insert = function (entity) {
        return unitsService.insert(entity);
    }
    $scope.update = function (entity) {
        return unitsService.update(entity);
    }
    $scope.delete = function (entity) {
        return unitsService.delete(entity);
    }
    $scope.unitsPageLoad = function () {

        $scope.getAllOnLoad();
    }




    $scope.pageChanged = function () {
        $scope.getAllOnLoad();
        $log.log('Page changed to: ' + $scope.pageNum);
    };


    $scope.getlastCode = function () {
         var urlParams = $location.search();
        if (urlParams.foo != null && urlParams.foo != undefined && urlParams.foo != "") { } else {
            unitsService.getLastCode().then(function (result) {
                $scope.units.UNIT_CODE = parseInt(result.data) + 1;
            }, function (error) { });
        }
    }

}]);