'use strict';
app.controller('billTypeController', ['$scope', '$location', '$log', '$q', 'authService', 'billTypeService', 'accountsService', 'localStorageService', function ($scope, $location, $log, $q, authService, billTypeService,  accountsService, localStorageService) {

    $scope.billType = { BILL_TYPE_ID: null, BILL_TYPE_CODE: "", BILL_TYPE_AR_NAME: "", BILL_TYPE_EN_NAME: "", BILL_TYPE_MASTER_ID: null, BILL_TYPE_REMARKS: ""};
    $scope.billTypeList = [];
    $scope.billTypeTotalCount = 0;
    $scope.pageNum = 1;
    $scope.pageSize = 10;
    $scope.pagesCount = 0;
    $scope.getPagesCount = function () {
        var div = Math.floor($scope.billTypeTotalCount / $scope.pageSize);
        var rem = $scope.billTypeTotalCount % $scope.pageSize;
        if (rem > 0) {
            div = div + 1;
        }
        $scope.pagesCount = div;
        return div;
    }
    $scope.maxSize = 5;



    $scope.clearEnity = function () {
       // $scope.getlastCode();
        $scope.billType = { BILL_TYPE_ID: null, BILL_TYPE_CODE: "", BILL_TYPE_AR_NAME: "", BILL_TYPE_EN_NAME: "", BILL_TYPE_MASTER_ID: null, BILL_TYPE_REMARKS: ""};
    }
    $scope.refreshData = function () {
        $scope.getAllOnLoad();
    }
    $scope.savebillType = function () {


        $scope.saveEntity();
    }
    $scope.saveEntity = function () {
         debugger;
        if ($scope.billType !== undefined && $scope.billType !== null
            && $scope.billType.BILL_EFF_ID !== "" && $scope.billType.BILL_TYPE_AR_NAME !== "" && $scope.billType.BILL_TYPE_EN_NAME !== "") {
            if ($scope.billType.BILL_TYPE_ID === null) {
                $scope.add($scope.billType).then(function (results) {
                    // var data = results.data;
                    // $scope.billTypeList = data;
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
          
                $scope.update($scope.billType).then(function (results) {
                    // var data = results.data;
                    // $scope.billTypeList = data;
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

        $scope.billType = entity;
       

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
    //    billTypeService.getAllMainItemGroups().then(function (response) {
    //        $scope.mainbillTypesList = response.data;
    //    })
    //}
    $scope.TypeEnum=["","مدخلات","مخرجات","تحولات","لا تؤثر"]
    $scope.getAllOnLoad = function () {

        $q.all(
            [
                $scope.getbillTypeList(),
                $scope.getbillTypeTotalCount(), 
                //$scope.getbillTypeList(),
                //$scope.getlastCode(),
              
            ])
            .then(function (allResponses) {
                var urlParams = $location.search();
                if (urlParams.foo != null && urlParams.foo != undefined && urlParams.foo != "") {

                    billTypeService.getByID(urlParams.foo).then(function (result) {
                        $scope.billType = result.data;
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
    $scope.getbillTypeTotalCount = function () {
        $scope.count().then(function (results) {
            var data = results.data;
            $scope.billTypeTotalCount = data;
            $scope.pagesCount = $scope.getPagesCount();

        }, function (error) {

            console.log(error.data.message);
        });
    }
    $scope.getbillTypeList = function () {
        $scope.get($scope.pageNum, $scope.pageSize).then(function (results) {
            debugger
            var data = results.data;
            $scope.billTypeList = data;
        }, function (error) {

            console.log(error.data.message);
        });
    }

  $scope.get = function (pageNum, pageSize) {
        return billTypeService.get(pageNum, pageSize);
    }
    $scope.count = function () {
        return billTypeService.count();
    }
    $scope.add = function (entity) {
        return billTypeService.insert(entity);
    }
    $scope.update = function (entity) {
        return billTypeService.update(entity);
    }
    $scope.delete = function (entity) {
        return billTypeService.delete(entity);
    }
    $scope.billTypePageLoad = function () {

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
    //        billTypeService.getLastCode().then(function (result) {
    //            $scope.billType.BILL_TYPE_CODE = parseInt(result.data) + 1;
    //        }, function (error) { });
    //    }

    //}

}]);