'use strict';
app.controller('itemCaliberController', ['$scope', '$location', '$log', '$q', 'authService', 'itemCaliberService', function ($scope, $location, $log, $q, authService, itemCaliberService) {

    $scope.itemCaliber = { ID: null, Code: "", ARName: "", LatName: "", ClearnessRate: null, CaliberNumber: null, Notes: "", Active: false };
    $scope.itemCaliberList = [];
    $scope.itemCaliberTotalCount = 0;
    $scope.pageNum = 1;
    $scope.pageSize = 10;
    $scope.pagesCount = 0;
    $scope.getPagesCount = function () {
        var div = Math.floor($scope.itemCaliberTotalCount / $scope.pageSize);
        var rem = $scope.itemCaliberTotalCount % $scope.pageSize;
        if (rem > 0) {
            div = div + 1;
        }
        $scope.pagesCount = div;
        return div;
    }
    $scope.maxSize = 5;



    $scope.changeClearnessRate = function () {
       
        var _CaliberNumber = $scope.itemCaliber.CaliberNumber;
        if ($scope.itemCaliber.CaliberNumber != undefined && $scope.itemCaliber.CaliberNumber != null && angular.isNumber($scope.itemCaliber.CaliberNumber)) {
            $scope.itemCaliber.ClearnessRate = parseFloat($scope.itemCaliber.CaliberNumber / 24).toFixed(2);
           
        }
        else
        {
            $scope.itemCaliber.ClearnessRate = 0;
        }
    }


    $scope.clearEnity = function () {
        $scope.itemCaliber = { ID: null, Code: "", ARName: "", LatName: "", ClearnessRate: null, CaliberNumber: null, Notes: "", Active: true };
        $scope.getlastCode();
    }
    $scope.refreshData = function () {
        $scope.getAllOnLoad();
    }
    $scope.saveItemsGroup = function () {
       

        $scope.saveEntity();
    }
    $scope.saveEntity = function () {
        debugger;

        if ($scope.itemCaliber !== undefined && $scope.itemCaliber !== null && $scope.itemCaliber.Code !== "" && $scope.itemCaliber.ARName !== ""  && $scope.itemCaliber.LatName !== "") {
            if ($scope.itemCaliber.ID === null ) {
                $scope.add($scope.itemCaliber).then(function (results) {
                    // var data = results.data;
                    // $scope.itemCaliberList = data;
                    $scope.clearEnity();
                    $scope.refreshData();
                    swal({
                        title: 'تم',
                        text: 'حفظ بيانات معيار الصنف بنجاح',
                        type: 'success',
                        timer: 1500,
                        showConfirmButton: false
                    });
                }, function (error) {
                    console.log(error.data.message);
                    swal('عفواً',
                        'حدث خطأ عند حفظ بيانات معيار الصنف',
                        'error');
                });
            } else {
                $scope.update($scope.itemCaliber).then(function (results) {
                    // var data = results.data;
                    // $scope.itemCaliberList = data;
                    $scope.clearEnity();
                    $scope.refreshData();
                    swal({
                        title: 'تم',
                        text: 'تعديل بيانات معيار الصنف بنجاح',
                        type: 'success',
                        timer: 1500,
                        showConfirmButton: false
                    });
                }, function (error) {
                    console.log(error.data.message);
                    swal('عفواً',
                        'حدث خطأ عند تعديل بيانات معيار الصنف',
                        'error');
                });
            }
        }
    }
    $scope.dirEnity = function (entity) {

        $scope.itemCaliber = entity;

    }
    $scope.deleteEnity = function (entity) {
        swal({
            title: 'هل تريد حذف معيار الصنف؟',
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
                        text: "معيار الصنف تمت عليه عمليات مختلفة. لا تستطيع حذفه",
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
        //    title: 'هل تريد حذف معيار الصنف؟',
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
                 $scope.getitemCaliberList(),
                 $scope.getitemCaliberTotalCount(),
                 $scope.getAllMainItemGroupsList(),
                 $scope.getlastCode()
            ])
            .then(function (allResponses) {
                //if all the requests succeeded, this will be called, and $q.all will get an
                //array of all their responses.
                //  console.log(allResponses[0].data);

                var urlParams = $location.search();
                if (urlParams.foo != null && urlParams.foo != undefined && urlParams.foo != "") {

                    itemCaliberService.getByIDLog(parseInt(urlParams.foo)).then(function (results) {
                        $scope.itemCaliber = results.data;
                        $scope.dirEnity(results.data);
                    })

                }


            }, function (error) {
                //This will be called if $q.all finds any of the requests erroring.
                var abc = error;
                var def = 99;
            });
    }
    $scope.getitemCaliberTotalCount = function () {

        $scope.count().then(function (results) {
            var data = results.data;
            $scope.itemCaliberTotalCount = data;
            $scope.pagesCount = $scope.getPagesCount();



        }, function (error) {

            console.log(error.data.message);
        });
    }
    $scope.getitemCaliberList = function () {

        $scope.get($scope.pageNum, $scope.pageSize).then(function (results) {
            var data = results.data;
            $scope.itemCaliberList = data;
        }, function (error) {

            console.log(error.data.message);
        });
    }

    $scope.getAllMainItemGroupsList = function () {
        itemCaliberService.getAllMainItemGroups().then(function (response) {
            $scope.mainitemCaliberList = response.data;
        })
    }

    $scope.get = function (pageNum, pageSize) {
        return itemCaliberService.get(pageNum, pageSize);
    }
    $scope.count = function () {
        return itemCaliberService.count();
    }
    $scope.add = function (entity) {
        return itemCaliberService.add(entity);
    }
    $scope.update = function (entity) {
        return itemCaliberService.update(entity);
    }
    $scope.delete = function (entity) {
        return itemCaliberService.delete(entity);
    }
    $scope.itemCaliberPageLoad = function () {


        $scope.getAllOnLoad();

    }

    $scope.pageChanged = function () {
        $scope.getAllOnLoad();
        $log.log('Page changed to: ' + $scope.pageNum);
    };
    /////////////////////////////////last code
    $scope.getlastCode = function () {
        var urlParams = $location.search();
        if (urlParams.foo != null && urlParams.foo != undefined && urlParams.foo != "") { } else {
            itemCaliberService.getLastCode().then(function (result) {
                $scope.itemCaliber.Code = parseInt(result.data) + 1;
            }, function (error) { });
        }
    }
}]);