'use strict';
app.controller('itemStatusController', ['$scope', '$location', '$log', '$q', 'authService', 'itemStatusService', function ($scope, $location, $log, $q, authService, itemStatusService) {

    $scope.itemStatus = { ID: null, Code: "", ARName: "", LatName: "", Notes: "", AddedBy: "", AddedOn: null, UpdatedBy: "", UpdatedOn: null, Disable: false };
    $scope.itemStatusList = [];


    $scope.itemStatusTotalCount = 0;
    $scope.pageNum = 1;
    $scope.pageSize = 10;

    $scope.pagesCount = 0;
    $scope.getPagesCount = function () {
        var div = Math.floor($scope.itemStatusTotalCount / $scope.pageSize);
        var rem = $scope.itemStatusTotalCount % $scope.pageSize;
        if (rem > 0) {
            div = div + 1;
        }
        return div;
    }
    $scope.maxSize = 5;



    $scope.clearEnity = function () {
        $scope.itemStatus = { ID: null, Code: "", ARName: "", LatName: "", Notes: "", AddedBy: "", AddedOn: null, UpdatedBy: "", UpdatedOn: null, Disable: false };
        $scope.getlastCode();
    }
    $scope.refreshData = function () {
        $scope.getAllOnLoad();
    }
    $scope.saveitemStatusGroup = function () {

        $scope.saveEntity();
    }
    $scope.saveEntity = function () {

        if ($scope.itemStatus !== undefined && $scope.itemStatus !== null && $scope.itemStatus.Code !== "" && $scope.itemStatus.ARName !== "" && $scope.itemStatus.LatName !== "") {
            if ($scope.itemStatus.ID === null || $scope.ID === 0) {
                $scope.insert($scope.itemStatus).then(function (results) {
                    $scope.clearEnity();
                    $scope.refreshData();
                    swal({
                        title: 'تم',
                        text: 'حفظ حالة الصنف بنجاح',
                        type: 'success',
                        timer: 1500,
                        showConfirmButton: false
                    });
                }, function (error) {
                    console.log(error.data.message);
                    swal('عفواً',
                        'حدث خطأ عند حفظ حالة الصنف',
                        'error');
                });
            } else {
                $scope.update($scope.itemStatus).then(function (results) {
                    $scope.clearEnity();
                    $scope.refreshData();
                    swal({
                        title: 'تم',
                        text: 'تعديل حالة الصنف بنجاح',
                        type: 'success',
                        timer: 1500,
                        showConfirmButton: false
                    });
                }, function (error) {
                    console.log(error.data.message);
                    swal('عفواً',
                        'حدث خطأ عند تعديل حالة الصنف',
                        'error');
                });
            }
        }
    }
    $scope.dirEnity = function (entity) {
        
        debugger;
        $scope.itemStatus = entity;
    }
    $scope.deleteEnity = function (entity) {
        swal({
            title: 'هل تريد حذف حالة الصنف؟',
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
                        text: "هذا العنصر تم عليه عمليات مختلفة. لا تستطيع حذفه",
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
        //    title: 'هل تريد حذف حالة الصنف؟',
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
                 $scope.getitemStatusList(),
                 $scope.getitemStatusTotalCount(),
                 $scope.getlastCode()
            ])
            .then(function (allResponses) {

                var urlParams = $location.search();
                if (urlParams.foo != null && urlParams.foo != undefined && urlParams.foo != "") {

                    itemStatusService.getById(urlParams.foo).then(function (result) {
                        $scope.itemStatus = result.data;
                        $scope.dirEnity(result.data);
                    });

                }

            }, function (error) {
                //This will be called if $q.all finds any of the requests erroring.
                var abc = error;
                var def = 99;
            });
    }
    $scope.getitemStatusTotalCount = function () {
        $scope.count().then(function (results) {
            var data = results.data;
            $scope.itemStatusTotalCount = data;
            $scope.pagesCount = $scope.getPagesCount();
        }, function (error) {

            console.log(error.data.message);
        });
    }
    $scope.getitemStatusList = function () {
        $scope.get($scope.pageNum, $scope.pageSize).then(function (results) {
            var data = results.data;
            $scope.itemStatusList = data;

        }, function (error) {

            console.log(error.data.message);
        });
    }

    $scope.get = function (pageNum, pageSize) {
        return itemStatusService.get(pageNum, pageSize);
    }
    $scope.count = function () {
        return itemStatusService.count();
    }
    $scope.insert = function (entity) {
        return itemStatusService.insert(entity);
    }
    $scope.update = function (entity) {
        return itemStatusService.update(entity);
    }
    $scope.delete = function (entity) {
        return itemStatusService.delete(entity);
    }
    $scope.itemStatusPageLoad = function () {

        $scope.getAllOnLoad();
    }




    $scope.pageChanged = function () {
        $scope.getAllOnLoad();
        $log.log('Page changed to: ' + $scope.pageNum);
    };
    ///////////////////////get last code
    $scope.getlastCode = function () {
        var urlParams = $location.search();
        if (urlParams.foo != null && urlParams.foo != undefined && urlParams.foo != "") { } else {
            itemStatusService.getLastCode().then(function (result) {
                $scope.itemStatus.Code = parseInt(result.data) + 1;
            }, function (error) { });
        }
    }
}]);