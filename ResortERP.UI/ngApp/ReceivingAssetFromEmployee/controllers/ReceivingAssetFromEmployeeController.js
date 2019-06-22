'use strict';
app.controller('ReceivingAssetFromEmployeeController', ['$scope', '$location', '$filter', '$log', '$q', '$document', '$uibModal', 'AssetOperationMasterService', 'commonService', 'authService', 'entryService', 'localStorageService', function ($scope, $location, $filter, $log, $q, $document, $uibModal, AssetOperationMasterService, commonService, authService, entryService, localStorageService){
    var UserId = authService.GetUserID();
    $scope.OperationType = 10;
    $scope.ReceivingAssetFromEmployee = {
        AssetOperationTypeID: $scope.OperationType,
        //OperationDetails: [{
        //    RowNumber: 1,
        //    Value: 0,
        //    AddedBy: UserId
        //}]
    };

    $scope.OerationDetails = [];

    //$scope.addOperationDetail = function () {
    //    $scope.ReceivingAssetFromEmployee.OperationDetails.push({
    //        RowNumber: $scope.ReceivingAssetFromEmployee.OperationDetails.length + 1,
    //        Value: 0,
    //        AddedBy: UserId
    //    });
    //}

    $scope.SelectAllAssets = function () {
        angular.forEach($scope.OerationDetails, function (item, index) {
            if ($scope.SelectAll == true) {
                item.Selected = true;
            }
            else {
                item.Selected = false;
            }
        })
    }

    $scope.SelectAsset = function (item) {
        !item.Selected;
    }

    $scope.selectEmployee = function () {
        if ($scope.ReceivingAssetFromEmployee.FromEmployeeID != undefined && $scope.ReceivingAssetFromEmployee.FromEmployeeID != "") {
            AssetOperationMasterService.getEmployeeAssets($scope.ReceivingAssetFromEmployee.FromEmployeeID).then(function (data) {
                if (data != undefined && data.data != undefined && data.data.length > 0) {
                    $scope.OerationDetails = data.data;
                }
                else {
                    $scope.OerationDetails = [];
                }
            })
        }
    }

    $scope.clearEnity = function () {
        $scope.ReceivingAssetFromEmployee = {
            AssetOperationTypeID: $scope.OperationType,
            //OperationDetails: [{
            //    RowNumber: 1,
            //    Value: 0,
            //    AddedBy: UserId
            //}]
        };
        $scope.getlastCode();
        $scope.hideEntry = true;
        $scope.OerationDetails = [];
    }

    $scope.ReceivingAssetFromEmployeeList = [];
    $scope.assetsTotalCount = 0;
    $scope.pageNum = 1;
    $scope.pageSize = 10;
    $scope.pagesCount = 0;
    $scope.maxSize = 5;
    $scope.moduleResourse = {};
    $scope.inputReuired = {};
    $scope.displayORNot = {};
    $scope.inputDataType = {};

    $scope.radioVal = 2;

    $scope.getPagesCount = function () {
        var div = Math.floor($scope.assetsTotalCount / $scope.pageSize);
        var rem = $scope.assetsTotalCount % $scope.pageSize;
        if (rem > 0) {
            div = div + 1;
        }
        return div;
    }

    $scope.refreshData = function () {
        $scope.getAllOnLoad();
    }

    $scope.saveReceivingAssetFromEmployee = function () {
        $scope.saveEntity();
    }
    $scope.saveEntity = function () {
        if ($scope.ReceivingAssetFromEmployee !== undefined && $scope.ReceivingAssetFromEmployee !== null && $scope.ReceivingAssetFromEmployee.Code !== undefined &&
            $scope.ReceivingAssetFromEmployee.Code !== "" && $scope.ReceivingAssetFromEmployee.Number !== undefined &&
            $scope.ReceivingAssetFromEmployee.Number !== "" && $scope.ReceivingAssetFromEmployee.AssetOperationTypeID !== undefined &&
            $scope.ReceivingAssetFromEmployee.AssetOperationTypeID !== "") {
            var filteredArray = $filter('filter')($scope.OerationDetails, function (item) {
                return item.AssetMasterID != undefined &&
                    item.AssetMasterID != "" &&
                    item.FromEmployeeID != undefined && item.FromEmployeeID != ""
                    && item.Selected != undefined && item.Selected != "" && item.Selected != false;
            });
            if (filteredArray.length > 0) {
                $scope.ReceivingAssetFromEmployee.OperationDetails = filteredArray;
                if ($scope.ReceivingAssetFromEmployee.ID === null || $scope.ReceivingAssetFromEmployee.ID === 0 || $scope.ReceivingAssetFromEmployee.ID === undefined) {
                    var UserId = authService.GetUserID();
                    if (UserId != undefined && UserId != "") {
                        $scope.ReceivingAssetFromEmployee.AddedBy = UserId;
                    }
                    $scope.insert($scope.ReceivingAssetFromEmployee).then(function (results) {
                        if (results.data != false) {
                            //$scope.SaveEntry(results.data);
                            $scope.clearEnity();
                            $scope.refreshData();
                            swal({
                                title: 'اضافة',
                                text: 'تم الحفظ بنجاح',
                                type: 'success',
                                timer: 1500,
                                showConfirmButton: false
                            });
                        }
                        else {
                            swal({
                                title: 'اضافة',
                                text: 'الرقم او الكود موجود من قبل',
                                type: 'error',
                                timer: 1500,
                                showConfirmButton: false
                            });
                        }
                    }, function (error) {
                        console.log(error.data.message);
                        swal({
                            title: 'اضافة',
                            text: 'حدث خطأ ما!',
                            type: 'error',
                            timer: 1500,
                            showConfirmButton: false
                        });
                    });
                }
                else {
                    var UserId = authService.GetUserID();
                    if (UserId != undefined && UserId != "") {
                        $scope.ReceivingAssetFromEmployee.UpdatedBy = UserId;
                    }
                    $scope.update($scope.ReceivingAssetFromEmployee).then(function (results) {
                        if (results.data != false) {
                            //$scope.SaveEntry($scope.ReceivingAssetFromEmployee.ID);
                            $scope.clearEnity();
                            $scope.refreshData();
                            swal({
                                title: 'تعديل',
                                text: 'تم التعديل بنجاح',
                                type: 'success',
                                timer: 1500,
                                showConfirmButton: false
                            });
                        }
                        else {
                            swal({
                                title: 'تعديل',
                                text: 'الرقم او الكود موجود من قبل',
                                type: 'error',
                                timer: 1500,
                                showConfirmButton: false
                            });
                        }
                    }, function (error) {
                        console.log(error.data.message);
                        swal({
                            title: 'تعديل',
                            text: 'حدث خطأ ما!',
                            type: 'error',
                            timer: 1500,
                            showConfirmButton: false
                        });
                        //commonService.showErrorUpdateAlert($scope);
                    });
                }
            }
            else {
                swal({
                    title: 'خطأ',
                    text: 'يجب اختيار اصل واحد على الاقل ويجب اختيار الموظف !',
                    type: 'error',
                    timer: 1500,
                    showConfirmButton: false
                });
            }
        }
        else {
            swal({
                title: 'خطأ',
                text: 'يجب ادخال الكود ورقم العملية!',
                type: 'error',
                timer: 1500,
                showConfirmButton: false
            });
        }
    }

    $scope.dirEnity = function (entity) {
        $scope.ReceivingAssetFromEmployee = entity;
        $scope.OerationDetails = entity.OperationDetails;
        $scope.hideEntry = false;
        //$scope.getAssetDepreciationDetails($scope.ReceivingAssetFromEmployee.ID);       
    }

    $scope.deleteEnity = function (entity) {
        swal({
            title: 'هل تريد حذف هذه العملية؟',
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
                        text: "هذا الاصل تمت عليه عمليات مختلفة. لا تستطيع حذفه",
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
        //commonService.showDeleteAlert($scope, entity);

    }


    $scope.getAllOnLoad = function () {
        $q.all(
            [
                $scope.getAssetOperationsTotalCount(),
                $scope.getAssetMasterList(),
                $scope.getEmployeeList(),

                $scope.getAssetOperations(),

            ]).then(function (allResponses) {
                $scope.clearEnity();

            }, function (error) {
                //This will be called if $q.all finds any of the requests erroring.
                var abc = error;
                var def = 99;
            });
    }

    $scope.getAssetMasterList = function () {
        AssetOperationMasterService.getAssetMasterList().then(function (response) {
            $scope.AssetMasterList = response.data;
        })
    }

    $scope.getEmployeeList = function () {
        AssetOperationMasterService.getEmployeeList().then(function (response) {
            $scope.EmployeeList = response.data;
        })
    }


    $scope.getAssetOperationsTotalCount = function () {
        $scope.count($scope.OperationType).then(function (results) {
            var data = results.data;
            $scope.assetsTotalCount = data;
            $scope.pagesCount = $scope.getPagesCount();
        }, function (error) {
            console.log(error.data.message);
        });
    }
    $scope.getAssetOperations = function () {
        $scope.get($scope.pageNum, $scope.pageSize, $scope.OperationType).then(function (results) {
            var data = results.data;
            $scope.AssetsList = data;

        }, function (error) {
            console.log(error.data.message);
        });
    }


    $scope.get = function (pageNum, pageSize, OperationType) {
        return AssetOperationMasterService.get(pageNum, pageSize, OperationType);
    }
    $scope.count = function (OperationType) {
        return AssetOperationMasterService.count(OperationType);
    }
    $scope.insert = function (entity) {
        return AssetOperationMasterService.insert(entity);
    }
    $scope.insertGetID = function (entity) {
        return AssetOperationMasterService.insertGetID(entity);
    }

    $scope.update = function (entity) {
        return AssetOperationMasterService.update(entity);
    }
    $scope.delete = function (entity) {
        return AssetOperationMasterService.delete(entity);
    }
    $scope.ReceivingAssetFromEmployeePageLoad = function () {
        $scope.getAllOnLoad();
    }

    $scope.pageChanged = function () {
        $scope.getAllOnLoad();
        $log.log('Page changed to: ' + $scope.pageNum);
    };

    $scope.getlastCode = function () {
        var urlParams = $location.search();
        if (urlParams.foo != null && urlParams.foo != undefined && urlParams.foo != "") { } else {
            AssetOperationMasterService.getLastCode().then(function (result) {
                $scope.ReceivingAssetFromEmployee.Code = parseInt(result.data) + 1;
            }, function (error) { });
        }
    }


}]);
