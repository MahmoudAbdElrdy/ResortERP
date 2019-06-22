'use strict';
app.controller('entryTypeController', ['$scope', '$location', '$log', '$q', 'authService', 'entryTypeService', 'accountsService', 'localStorageService', function ($scope, $location, $log, $q, authService, entryTypeService,  accountsService, localStorageService) {

    $scope.entryType = { ENTRY_TYPE_ID: null, ENTRY_TYPE_CODE: "", ENTRY_TYPE_AR_NAME: "", ENTRY_TYPE_EN_NAME: "", ENTRY_TYPE_MASTER_ID: null, ENTRY_TYPE_REMARKS: ""};
    $scope.entryTypeList = [];
    $scope.entryTypeTotalCount = 0;
    $scope.pageNum = 1;
    $scope.pageSize = 10;
    $scope.pagesCount = 0;
    $scope.getPagesCount = function () {
        var div = Math.floor($scope.entryTypeTotalCount / $scope.pageSize);
        var rem = $scope.entryTypeTotalCount % $scope.pageSize;
        if (rem > 0) {
            div = div + 1;
        }
        $scope.pagesCount = div;
        return div;
    }
    $scope.maxSize = 5;



    $scope.clearEnity = function () {
       // $scope.getlastCode();
        $scope.entryType = { ENTRY_TYPE_ID: null, ENTRY_AR_NAME: "", ENTRY_EN_NAME: ""};
    }
    $scope.refreshData = function () {
        $scope.getAllOnLoad();
    }
    $scope.saveentryType = function () {


        $scope.saveEntity();
    }
    $scope.saveEntity = function () {
         debugger;
        if ($scope.entryType !== undefined && $scope.entryType !== null
            && $scope.entryType.ENTRY_AR_NAME !== "" && $scope.entryType.ENTRY_EN_NAME !== "") {
            if ($scope.entryType.ENTRY_TYPE_ID === null) {
                $scope.add($scope.entryType).then(function (results) {
                    // var data = results.data;
                    // $scope.entryTypeList = data;
                    $scope.clearEnity();
                    $scope.refreshData();
                    swal({
                        title: 'تم',
                        text: 'حفظ بيانات نوع الفاتورة بنجاح',
                        type: 'success',
                        timer: 1500,
                        showConfirmButton: false
                    });
                }, function (error) {
                    console.log(error.data.message);
                    swal('عفواً',
                        'حدث خطأ عند حفظ بيانات نوع الفاتورة',
                        'error');
                });
            } else {
          
                $scope.update($scope.entryType).then(function (results) {
                    // var data = results.data;
                    // $scope.entryTypeList = data;
                    $scope.clearEnity();
                    $scope.refreshData();
                    swal({
                        title: 'تم',
                        text: 'تعديل بيانات نوع الفاتورة بنجاح',
                        type: 'success',
                        timer: 1500,
                        showConfirmButton: false
                    });
                }, function (error) {
                    console.log(error.data.message);
                    swal('عفواً',
                        'حدث خطأ عند تعديل بيانات نوع الفاتورة',
                        'error');
                });
            }
        }
    }
    $scope.dirEnity = function (entity) {

        $scope.entryType = entity;
       

    }
    $scope.deleteEnity = function (entity) {
        swal({
            title: 'هل تريد حذف نوع الفاتورة؟',
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
                        text: "هذا الحساب تم عليه عمليات مختلفة. لا تستطيع حذفه",
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

    }

    //$scope.getAllMainItemGroupsList = function () {
    //    entryTypeService.getAllMainItemGroups().then(function (response) {
    //        $scope.mainentryTypesList = response.data;
    //    })
    //}
    $scope.TypeEnum=["","مدخلات","مخرجات","تحولات","لا تؤثر"]
    $scope.getAllOnLoad = function () {

        $q.all(
            [
                $scope.getentryTypeList(),
                $scope.getentryTypeTotalCount(), 
                //$scope.getentryTypeList(),
                //$scope.getlastCode(),
              
            ])
            .then(function (allResponses) {
                var urlParams = $location.search();
                if (urlParams.foo != null && urlParams.foo != undefined && urlParams.foo != "") {

                    entryTypeService.getByID(urlParams.foo).then(function (result) {
                        $scope.entryType = result.data;
                        $scope.dirEnity(result.data);
                    

                    });
                }
                //if all the requests succeeded, this will be called, and $q.all will get an
                //array of all their responses.
                //  console.log(allResponses[0].data);

            }, function (error) {
                //This will be called if $q.all finds any of the requests erroring.
                var abc = error;
                var def = 99;
            });
    }
    $scope.getentryTypeTotalCount = function () {
        $scope.count().then(function (results) {
            var data = results.data;
            $scope.entryTypeTotalCount = data;
            $scope.pagesCount = $scope.getPagesCount();

        }, function (error) {

            console.log(error.data.message);
        });
    }
    $scope.getentryTypeList = function () {
        $scope.get($scope.pageNum, $scope.pageSize).then(function (results) {
            debugger
            var data = results.data;
            $scope.entryTypeList = data;
        }, function (error) {

            console.log(error.data.message);
        });
    }

  $scope.get = function (pageNum, pageSize) {
        return entryTypeService.get(pageNum, pageSize);
    }
    $scope.count = function () {
        return entryTypeService.count();
    }
    $scope.add = function (entity) {
        return entryTypeService.insert(entity);
    }
    $scope.update = function (entity) {
        return entryTypeService.update(entity);
    }
    $scope.delete = function (entity) {
        return entryTypeService.delete(entity);
    }
    $scope.entryTypePageLoad = function () {

        $scope.getAllOnLoad();

    }

    $scope.pageChanged = function () {
        $scope.getAllOnLoad();
        $log.log('Page changed to: ' + $scope.pageNum);
    };
    ///////////////////////get last code

    //$scope.getlastCode = function () {
    //    var urlParams = $location.search();
    //    if (urlParams.foo != null && urlParams.foo != undefined && urlParams.foo != "") {
    //    } else {
    //        entryTypeService.getLastCode().then(function (result) {
    //            $scope.entryType.ENTRY_TYPE_CODE = parseInt(result.data) + 1;
    //        }, function (error) { });
    //    }

    //}

}]);