'use strict';
app.controller('assetsGroupController', ['$scope', '$location', '$log', '$q', 'authService', 'assetsGroupService', 'accountsService', 'localStorageService', function ($scope, $location, $log, $q, authService, assetsGroupService, accountsService, localStorageService) {

    $scope.assetGroup = { };
    $scope.assetGroupsList = [];
    //$scope.assetGroupsTotalCount = 0;
    $scope.pageNum = 1;
    $scope.pageSize = 10;
    //$scope.pagesCount = 0;
    //$scope.getPagesCount = function () {
    //    var div = Math.floor($scope.assetGroupsTotalCount / $scope.pageSize);
    //    var rem = $scope.assetGroupsTotalCount % $scope.pageSize;
    //    if (rem > 0) {
    //        div = div + 1;
    //    }
    //    $scope.pagesCount = div;
    //    return div;
    //}
    //$scope.maxSize = 5;



    $scope.clearEnity = function () {
       // $scope.assetGroup = { ID: null, GroupCode: "", GroupARName: "", GroupENName: "", GroupMasterID: null, GroupRemarks: "", AppearOnSalePoint: false, CaliberID: null, DOESTHEQUANTITYISAPARTOFBARCODE: false, QUANTITYLENGTHATTHEBARCODE: null, QUANTITYSTARTATTHEBARCODE: null, QUANTITYPARTSTARTATTHEBARCODE: null, QUANTITYPARTSTARTATTHEBARCODE: null };
        $scope.getlastCode();
        $scope.assetGroup = {
            ID: null, Code: "", ARName: "", LatName: "", AssetGroupID: null, Notes: "", AssetGroupAccountID: null,
            DepreciationAccountID: null, TotalDepreciationAccountID: null, ExpensesAccountID: null,
            CapitalProfitAccountID: null, CapitalLossAccountID: null, AppraisalExcessAccountID: null,
            ApprasialDeficitAccountID: null, Active: false, Position:""
        };

      // $scope.assetGroup = {};
    }
    $scope.refreshData = function () {
        $scope.getAllOnLoad();
    }
    $scope.saveAssetGroup= function () {
        $scope.saveEntity();
    }
    $scope.saveEntity = function () {
        // debugger;

        if ($scope.assetGroup !== undefined && $scope.assetGroup !== null && $scope.assetGroup.Code !== ""
            && $scope.assetGroup.ARName !== "" && $scope.assetGroup.LatName !== ""
            && $scope.assetGroup.Position !== "") {

            if ($scope.assetGroup.ID === null) {
                $scope.add($scope.assetGroup).then(function (results) {
                    // var data = results.data;
                    // $scope.assetGroupsList = data;
                    $scope.clearEnity();
                    $scope.refreshData();
                    swal({
                        title: 'تم',
                        text: 'حفظ بيانات مجموعة الاصول بنجاح',
                        type: 'success',
                        timer: 1500,
                        showConfirmButton: false
                    });
                }, function (error) {
                    console.log(error.data.message);
                    swal('عفواً',
                        'حدث خطأ عند حفظ بيانات مجموعة الاصول',
                        'error');
                });
            } else {
                $scope.update($scope.assetGroup).then(function (results) {
                    // var data = results.data;
                    // $scope.assetGroupsList = data;
                    $scope.clearEnity();
                    $scope.refreshData();
                    swal({
                        title: 'تم',
                        text: 'تعديل بيانات مجموعة الاصول بنجاح',
                        type: 'success',
                        timer: 1500,
                        showConfirmButton: false
                    });
                }, function (error) {
                    console.log(error.data.message);
                    swal('عفواً',
                        'حدث خطأ عند تعديل بيانات مجموعة الاصول',
                        'error');
                });
            }
        }
    }
    $scope.dirEnity = function (entity) {
        debugger
        $scope.assetGroup = entity;
       

    }
    $scope.deleteEnity = function (entity) {
        swal({
            title: 'هل تريد حذف مجموعة الاصول؟',
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
                        text: "هذا الاصل تم عليه عمليات مختلفة. لا تستطيع حذفه",
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
        //    title: 'هل تريد حذف مجموعة الاصول؟',
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
                $scope.getAssetsGroupsList(),
                //$scope.getassetGroupsTotalCount(),
              
                //$scope.getAllItemCalibers(),
                //$scope.getAllCostCalculationTypeList(),
                $scope.getlastCode(),
                //$scope.getAllItemStatusList(),
                //$scope.GetAllGoldBoxAccounts()
            ])
            .then(function (allResponses) {
                var urlParams = $location.search();
                if (urlParams.foo != null && urlParams.foo != undefined && urlParams.foo != "") {

                    assetsGroupService.getByID(urlParams.foo).then(function (result) {
                        $scope.assetGroup = result.data;
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
    //$scope.getassetGroupsTotalCount = function () {
    //    $scope.count().then(function (results) {
    //        var data = results.data;
    //        $scope.assetGroupsTotalCount = data;
    //        $scope.pagesCount = $scope.getPagesCount();



    //    }, function (error) {

    //        console.log(error.data.message);
    //    });
    //}
    $scope.getAssetsGroupsList = function () {
        $scope.get($scope.pageNum, $scope.pageSize).then(function (results) {
            var data = results.data;
            $scope.assetsGroupsList = data;
        }, function (error) {

            console.log(error.data.message);
        });
    }


    //$scope.getAllCostCalculationTypeList = function () {

    //    itemsService.getAllCostCalculationType().then(function (response) {
    //        // debugger;
    //        $scope.costCalculationTypes = response.data;
    //    })
    //}



    //$scope.getAllItemCalibers = function () {

    //    itemsService.getAllItemCalibers().then(function (response) {
    //        // debugger;
    //        $scope.itemCalibersList = response.data;
    //    })
    //}

    $scope.getAllMainItemGroupsList = function () {
        assetsGroupService.AssetGroupsList().then(function (results) {
            debugger
            var data = results.data;
            $scope.mainAssetGroupsList = data;
        }, function (error) {

            console.log(error.data.message);
        });
    }

    $scope.get = function (pageNum, pageSize) {
        debugger;
        return assetsGroupService.get(pageNum, pageSize);
    }
    //$scope.count = function () {
    //    return assetsGroupService.count();
    //}
    $scope.add = function (entity) {
        return assetsGroupService.add(entity);
    }
    $scope.update = function (entity) { 
        return assetsGroupService.update(entity);
    }
    $scope.delete = function (entity) {
        return assetsGroupService.delete(entity);
    }
    $scope.assetGroupsPageLoad = function () {
        $scope.getAllMainItemGroupsList();
        $scope.GetAllAccounts();
        $scope.getAllOnLoad();


    }

    //$scope.pageChanged = function () {
    //    $scope.getAllOnLoad();
    //    $log.log('Page changed to: ' + $scope.pageNum);
    //};
    /////////////////////////get last code

    $scope.getlastCode = function () {
        var urlParams = $location.search();
        if (urlParams.foo != null && urlParams.foo != undefined && urlParams.foo != "") {
        } else {
            assetsGroupService.getLastCode().then(function (result) {
                $scope.assetGroup.Code = parseInt(result.data) + 1;
            }, function (error) { });
        }

    }


    //////////item status
    //$scope.getAllItemStatusList = function () {

    //    itemsService.getAllItemStatus().then(function (response) {
    //        $scope.itemStatusList = response.data;
    //    })
    //}


    ////GetAllGoldBoxAccounts
    $scope.GetAllAccounts = function () {

        $scope.AllAccounts = [];
        if (localStorageService.get('branchId') != null) {
            $scope.COM_BRN_ID = localStorageService.get('branchId').branchId;
        } else {
            $scope.COM_BRN_ID = 0;
        }
        accountsService.getAll().then(function (result) {
            $scope.AllAccounts = result.data;
        });

    }
}]);