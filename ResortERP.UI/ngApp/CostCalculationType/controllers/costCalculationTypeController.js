'use strict';
app.controller('costCalculationTypeController', ['$scope', '$location', '$log', '$q', 'authService', 'costCalculationTypeService', function ($scope, $location, $log, $q, authService, costCalculationTypeService) {

    $scope.costCalculationType = { ID: null, Code: "", ARName: "", LatName: "", Notes: "", AddedBy: "", AddedOn: null, UpdatedBy: "", UpdatedOn: null, Disable: false };
    $scope.costCalculationTypeList = [];


    $scope.costCalculationTypeTotalCount = 0;
    $scope.pageNum = 1;
    $scope.pageSize = 10;

    $scope.pagesCount = 0;
    $scope.getPagesCount = function () {
        var div = Math.floor($scope.costCalculationTypeTotalCount / $scope.pageSize);
        var rem = $scope.costCalculationTypeTotalCount % $scope.pageSize;
        if (rem > 0) {
            div = div + 1;
        }
        return div;
    }
    $scope.maxSize = 5;



    $scope.clearEnity = function () {
        $scope.costCalculationType = { ID: null, Code: "", ARName: "", LatName: "", Notes: "", AddedBy: "", AddedOn: null, UpdatedBy: "", UpdatedOn: null, Disable: false };
    }
    $scope.refreshData = function () {
        $scope.getAllOnLoad();
    }
    $scope.savecostCalculationTypeGroup = function () {

        $scope.saveEntity();
    }
    $scope.saveEntity = function () {

        if ($scope.costCalculationType !== undefined && $scope.costCalculationType !== null && $scope.costCalculationType.Code !== "" && $scope.costCalculationType.ARName !== "" && $scope.costCalculationType.LatName !== "") {
            if ($scope.costCalculationType.ID === null || $scope.ID === 0) {
                $scope.insert($scope.costCalculationType).then(function (results) {
                    $scope.clearEnity();
                    $scope.refreshData();
                    swal({
                        title: 'تم',
                        text: 'حفظ طريقة حساب التكلفة بنجاح',
                        type: 'success',
                        timer: 1500,
                        showConfirmButton: false
                    });
                }, function (error) {
                    console.log(error.data.message);
                    swal('عفواً',
                        'حدث خطأ عند حفظ طريقة حساب التكلفة',
                        'error');
                });
            } else {
                $scope.update($scope.costCalculationType).then(function (results) {
                    $scope.clearEnity();
                    $scope.refreshData();
                    swal({
                        title: 'تم',
                        text: 'تعديل طريقة حساب التكلفة بنجاح',
                        type: 'success',
                        timer: 1500,
                        showConfirmButton: false
                    });
                }, function (error) {
                    console.log(error.data.message);
                    swal('عفواً',
                        'حدث خطأ عند تعديل طريقة حساب التكلفة',
                        'error');
                });
            }
        }
    }
    $scope.dirEnity = function (entity) {
        $scope.costCalculationType = entity;
    }
    $scope.deleteEnity = function (entity) {
        swal({
            title: 'هل تريد حذف طريقة حساب التكلفة؟',
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
    $scope.getAllOnLoad = function () {
       
        $q.all(
            [
                 $scope.getcostCalculationTypeList(),
                 $scope.getcostCalculationTypeTotalCount()
            ])
            .then(function (allResponses) {
            }, function (error) {
                //This will be called if $q.all finds any of the requests erroring.
                var abc = error;
                var def = 99;
            });
    }
    $scope.getcostCalculationTypeTotalCount = function () {
        $scope.count().then(function (results) {
            var data = results.data;
            $scope.costCalculationTypeTotalCount = data;
            $scope.pagesCount = $scope.getPagesCount();
        }, function (error) {

            console.log(error.data.message);
        });
    }
    $scope.getcostCalculationTypeList = function () {
        $scope.get($scope.pageNum, $scope.pageSize).then(function (results) {
            var data = results.data;
            debugger;
            $scope.costCalculationTypeList = data;
        }, function (error) {

            console.log(error.data.message);
        });
    }

    $scope.get = function (pageNum, pageSize) {
        return costCalculationTypeService.get(pageNum, pageSize);
    }
    $scope.count = function () {
        return costCalculationTypeService.count();
    }
    $scope.insert = function (entity) {
        return costCalculationTypeService.insert(entity);
    }
    $scope.update = function (entity) {
        return costCalculationTypeService.update(entity);
    }
    $scope.delete = function (entity) {
        return costCalculationTypeService.delete(entity);
    }
    $scope.costCalculationTypePageLoad = function () {

        $scope.getAllOnLoad();
    }




    $scope.pageChanged = function () {
        $scope.getAllOnLoad();
        $log.log('Page changed to: ' + $scope.pageNum);
    };

}]);