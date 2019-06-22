'use strict';
app.controller('manufacturingWagesController', ['$scope', '$location', '$log', '$q', 'authService', 'manufacturingWagesService', function ($scope, $location, $log, $q, authService, manufacturingWagesService) {

    $scope.manufacturingWages = { ManufacturingWage_ID: null, ManufacturingWage_CODE: "", ManufacturingWages_AR_NAME: "", ManufacturingWage_EN_NAME: "", ManufacturingWage_REMARKS: "", AddedBy: "", AddedOn: null, UpdatedBy: "", UpdatedOn: null, Disable: false };
    $scope.manufacturingWagesList = [];


    $scope.manufacturingWagesTotalCount = 0;
    $scope.pageNum = 1;
    $scope.pageSize = 10;

    $scope.pagesCount = 0;
    $scope.getPagesCount = function () {
        var div = Math.floor($scope.manufacturingWagesTotalCount / $scope.pageSize);
        var rem = $scope.manufacturingWagesTotalCount % $scope.pageSize;
        if (rem > 0) {
            div = div + 1;
        }
        return div;
    }
    $scope.maxSize = 5;



    $scope.clearEnity = function () {
        $scope.manufacturingWages = { ManufacturingWage_ID: null, ManufacturingWage_CODE: "", ManufacturingWage_AR_NAME: "", ManufacturingWage_EN_NAME: "", ManufacturingWage_REMARKS: "", AddedBy: "", AddedOn: null, UpdatedBy: "", UpdatedOn: null, Disable: false };
        $scope.getlastCode();
    }
    $scope.refreshData = function () {
        $scope.getAllOnLoad();
    }
    $scope.savemanufacturingWagesGroup = function () {

        $scope.saveEntity();
    }
    $scope.saveEntity = function () {

        if ($scope.manufacturingWages !== undefined && $scope.manufacturingWages !== null && $scope.manufacturingWages.ManufacturingWage_CODE !== "" && $scope.manufacturingWages.ManufacturingWage_AR_NAME !== "" && $scope.manufacturingWages.ManufacturingWage_EN_NAME !== "") {
            if ($scope.manufacturingWages.ManufacturingWage_ID === null || $scope.ManufacturingWage_ID === 0) {
                $scope.insert($scope.manufacturingWages).then(function (results) {
                    $scope.clearEnity();
                    $scope.refreshData();
                    swal({
                        title: 'تم',
                        text: 'حفظ اجور التصنيع بنجاح',
                        type: 'success',
                        timer: 1500,
                        showConfirmButton: false
                    });
                }, function (error) {
                    console.log(error.data.message);
                    swal('عفواً',
                        'حدث خطأ عند حفظ اجور التصنيع',
                        'error');
                });
            } else {
                $scope.update($scope.manufacturingWages).then(function (results) {
                    $scope.clearEnity();
                    $scope.refreshData();
                    swal({
                        title: 'تم',
                        text: 'تعديل اجور التصنيع بنجاح',
                        type: 'success',
                        timer: 1500,
                        showConfirmButton: false
                    });
                }, function (error) {
                    console.log(error.data.message);
                    swal('عفواً',
                        'حدث خطأ عند تعديل اجور التصنيع',
                        'error');
                });
            }
        }
    }
    $scope.dirEnity = function (entity) {
        $scope.manufacturingWages = entity;
    }
    $scope.deleteEnity = function (entity) {
        swal({
            title: 'هل تريد حذف اجور التصنيع؟',
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
       debugger;
        $q.all(
            [
                 $scope.getmanufacturingWagesList(),
                 $scope.getmanufacturingWagesTotalCount(),
                 $scope.getlastCode()
            ])
            .then(function (allResponses) {

                var urlParams = $location.search();
                if (urlParams.foo != null && urlParams.foo != undefined && urlParams.foo != "") {

                    manufacturingWagesService.getById(parseInt(urlParams.foo)).then(function (results) {
                        $scope.dirEnity(results.data);
                    })
                }

            }, function (error) {
                //This will be called if $q.all finds any of the requests erroring.
                var abc = error;
                var def = 99;
            });
      
    }
    $scope.getmanufacturingWagesTotalCount = function () {
        $scope.count().then(function (results) {
            var data = results.data;
            $scope.manufacturingWagesTotalCount = data;
            $scope.pagesCount = $scope.getPagesCount();
        }, function (error) {

            console.log(error.data.message);
        });
    }
    $scope.getmanufacturingWagesList = function () {
        $scope.get($scope.pageNum, $scope.pageSize).then(function (results) {
            var data = results.data;
            debugger;
            $scope.manufacturingWagesList = data;

        }, function (error) {

            console.log(error.data.message);
        });
    }

    $scope.get = function (pageNum, pageSize) {
        return manufacturingWagesService.get(pageNum, pageSize);
    }
    $scope.count = function () {
        return manufacturingWagesService.count();
    }
    $scope.insert = function (entity) {
        return manufacturingWagesService.insert(entity);
    }
    $scope.update = function (entity) {
        return manufacturingWagesService.update(entity);
    }
    $scope.delete = function (entity) {
        return manufacturingWagesService.delete(entity);
    }
    $scope.manufacturingWagesPageLoad = function () {

        $scope.getAllOnLoad();
    }




    $scope.pageChanged = function () {
        $scope.getAllOnLoad();
        $log.log('Page changed to: ' + $scope.pageNum);
    };



///////////////////////////get last code
    $scope.getlastCode = function () {
        var urlParams = $location.search();
        if (urlParams.foo != null && urlParams.foo != undefined && urlParams.foo != "") { } else {
            manufacturingWagesService.getLastCode().then(function (result) {
                $scope.manufacturingWages.ManufacturingWage_CODE = parseInt(result.data) + 1;
            }, function (error) { });
        }
    }
}]);