'use strict';
app.controller('employeeTypesController', ['$scope', '$location', '$log', '$q', 'employeeTypesService', function ($scope, $location, $log, $q, employeeTypesService) {
    $scope.employeeTypes = { EMP_TYPE_ID: null, EMP_TYPE_CODE: null, EMP_TYPE_AR_NAME: null, EMP_TYPE_EN_NAME: null };

    $scope.clearEnity = function () {
        $scope.employeeTypes = { EMP_TYPE_ID: null, EMP_TYPE_CODE: null, EMP_TYPE_AR_NAME: null, EMP_TYPE_EN_NAME: null };
        $scope.getlastCode();
    }

    $scope.employeeTypesList = [];
    $scope.employeeTypesTotalCount = 0;
    $scope.pageNum = 1;
    $scope.pageSize = 10;
    $scope.pagesCount = 0;
    $scope.maxSize = 5;

    $scope.getPagesCount = function () {
        var div = Math.floor($scope.employeeTypesTotalCount / $scope.pageSize);
        var rem = $scope.employeeTypesTotalCount % $scope.pageSize;
        if (rem > 0) {
            div = div + 1;
        }
        return div;
    }

    $scope.refreshData = function () {
        $scope.getAllOnLoad();
    }
    $scope.saveEmployeeTypes = function () {
        $scope.saveEntity();
    }
    $scope.saveEntity = function () {
        if ($scope.employeeTypes !== undefined && $scope.employeeTypes !== null && $scope.employeeTypes.EMP_TYPE_CODE !== null && $scope.employeeTypes.EMP_TYPE_AR_NAME !== null && $scope.employeeTypes.EMP_TYPE_EN_NAME !== null) {
            if ($scope.employeeTypes.EMP_TYPE_ID === null || $scope.EMP_TYPE_ID === 0) {
                $scope.insert($scope.employeeTypes).then(function (results) {
                    $scope.clearEnity();
                    $scope.refreshData();
                    swal({
                        title: 'تم',
                        text: 'حفظ بيانات الوظيفة بنجاح',
                        type: 'success',
                        timer: 1500,
                        showConfirmButton: false
                    });
                }, function (error) {
                    console.log(error.data.message);
                    swal('عفواً',
                        'حدث خطأ عند حفظ بيانات الوظيفة',
                        'error');
                });
            } else {
                $scope.update($scope.employeeTypes).then(function (results) {
                    $scope.clearEnity();
                    $scope.refreshData();
                    swal({
                        title: 'تم',
                        text: 'تعديل بيانات الوظيفة بنجاح',
                        type: 'success',
                        timer: 1500,
                        showConfirmButton: false
                    });
                }, function (error) {
                    console.log(error.data.message);
                    swal('عفواً',
                        'حدث خطأ عند تعديل بيانات الوظيفة',
                        'error');
                });
            }
        }
    }
    $scope.dirEnity = function (entity) {
        $scope.employeeTypes = entity;
    }
    $scope.deleteEnity = function (entity) {
        swal({
            title: 'هل تريد حذف الوظيفة؟',
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
                        text: "هذه الوظيفة تمت عليها عمليات مختلفة. لا تستطيع حذفها",
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
        //    title: 'هل تريد حذف الوظيفة؟',
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
                $scope.getemployeeTypesList(),
                $scope.getemployeeTypesTotalCount(),
                $scope.getlastCode()
            ]).then(function (allResponses) {

                var urlParams = $location.search();
                if (urlParams.foo != null && urlParams.foo != undefined && urlParams.foo != "") {

                    employeeTypesService.getById(parseInt(urlParams.foo)).then(function (results) {
                        $scope.employeeTypes = results.data;
                        $scope.dirEnity(results.data);
                    })
                }

            }, function (error) {
                //This will be called if $q.all finds any of the requests erroring.
                var abc = error;
                var def = 99;
            });
    }
    $scope.getemployeeTypesTotalCount = function () {
        $scope.count().then(function (results) {
            var data = results.data;
            $scope.employeeTypesTotalCount = data;
            $scope.pagesCount = $scope.getPagesCount();
        }, function (error) {
            console.log(error.data.message);
        });
    }

    //$scope.myData = [{ "fieldName1": "row1-col1", "fieldName2": "row1-col2", "fieldName3": "row1-col3" },
    //    { "fieldName1": "row2-col1", "fieldName2": "row2-col2", "fieldName3": "row2-col3" }];

    //$scope.myGridOptions = {
    //    colNames: ['columnName1', 'columnName2', 'columnName3'],
    //    colModel: [{ name: 'fieldName1' }, { name: 'fieldName2', align: 'center' }, { name: 'fieldName3', align: 'center' }]
    //};

    $scope.getemployeeTypesList = function () {
        $scope.get($scope.pageNum, $scope.pageSize).then(function (results) {
            var data = results.data;
            $scope.employeeTypesList = data;

        }, function (error) {
            console.log(error.data.message);
        });
    }


    $scope.get = function (pageNum, pageSize) {
        return employeeTypesService.get(pageNum, pageSize);
    }
    $scope.count = function () {
        return employeeTypesService.count();
    }
    $scope.insert = function (entity) {
        return employeeTypesService.insert(entity);
    }
    $scope.update = function (entity) {
        return employeeTypesService.update(entity);
    }
    $scope.delete = function (entity) {
        return employeeTypesService.delete(entity);
    }
    $scope.employeeTypesPageLoad = function () {
        $scope.getAllOnLoad();
    }

    $scope.pageChanged = function () {
        $scope.getAllOnLoad();
        $log.log('Page changed to: ' + $scope.pageNum);
    };

//////////////////////////////////////last code
    $scope.getlastCode = function () {
        var urlParams = $location.search();
        if (urlParams.foo != null && urlParams.foo != undefined && urlParams.foo != "") { } else {
            employeeTypesService.getLastCode().then(function (result) {
                $scope.employeeTypes.EMP_TYPE_CODE = parseInt(result.data) + 1;
            }, function (error) { });
        }
    }
}]);