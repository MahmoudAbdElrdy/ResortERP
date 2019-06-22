'use strict';
app.controller('AssetLocationController', ['$scope', '$location', '$filter', '$log', '$q', '$document', '$uibModal', 'AssetOperationMasterService', 'commonService', 'authService', 'entryService', 'localStorageService', function ($scope, $location, $filter, $log, $q, $document, $uibModal, AssetOperationMasterService, commonService, authService, entryService, localStorageService){
    var UserId = authService.GetUserID();
    $scope.OperationType = 7;
    $scope.AssetLocation = {
        AssetOperationTypeID: $scope.OperationType,
        OperationDetails: [{
            RowNumber: 1,
            Value: 0,
            AddedBy: UserId
        }]
    };

    $scope.OerationDetails = [{

    }];

    $scope.addOperationDetail = function () {
        $scope.AssetLocation.OperationDetails.push({
            RowNumber: $scope.AssetLocation.OperationDetails.length + 1,
            Value: 0,
            AddedBy: UserId
        });
    }

    $scope.removeoperationDetail = function (index) {
        $scope.AssetLocation.OperationDetails.splice(index, 1);
    }

    $scope.selectAssetMaster = function (item) {
        if (item.AssetMasterID != undefined && item.AssetMasterID != "") {
            var assetMaster = $filter('filter')($scope.AssetMasterList, function (asset) {
                return asset.OptionValue == item.AssetMasterID
            });
            if (assetMaster != undefined && assetMaster.length > 0) {
                item.DepartmentName = assetMaster[0].DepartmentName;
                item.FromDepartmentID = assetMaster[0].DepartmentId;
            }
        }
    }

    $scope.clearEnity = function () {
        $scope.AssetLocation = {
            AssetOperationTypeID: $scope.OperationType,
            OperationDetails: [{
                RowNumber: 1,
                Value: 0,
                AddedBy: UserId
            }]
        };
        $scope.getlastCode();
        $scope.hideEntry = true;
    }

    $scope.AssetLocationList = [];
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

    $scope.saveAssetLocation = function () {
        $scope.saveEntity();
    }
    $scope.saveEntity = function () {
        if ($scope.AssetLocation !== undefined && $scope.AssetLocation !== null && $scope.AssetLocation.Code !== undefined &&
            $scope.AssetLocation.Code !== "" && $scope.AssetLocation.Number !== undefined &&
            $scope.AssetLocation.Number !== "" && $scope.AssetLocation.AssetOperationTypeID !== undefined &&
            $scope.AssetLocation.AssetOperationTypeID !== "") {
            var filteredArray = $filter('filter')($scope.AssetLocation.OperationDetails, function (item) {
                return item.AssetMasterID != undefined &&
                    item.AssetMasterID != "" &&
                    item.ToDepartmentID != undefined && item.ToDepartmentID != "";
            });
            if (filteredArray.length > 0) {
                $scope.AssetLocation.OperationDetails = filteredArray;
                if ($scope.AssetLocation.ID === null || $scope.AssetLocation.ID === 0 || $scope.AssetLocation.ID === undefined) {
                    var UserId = authService.GetUserID();
                    if (UserId != undefined && UserId != "") {
                        $scope.AssetLocation.AddedBy = UserId;
                    }
                    $scope.insert($scope.AssetLocation).then(function (results) {
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
                        $scope.AssetLocation.UpdatedBy = UserId;
                    }
                    $scope.update($scope.AssetLocation).then(function (results) {
                        if (results.data != false) {
                            //$scope.SaveEntry($scope.AssetLocation.ID);
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
                    text: 'يجب اختيار اصل واحد على الاقل ويجب اختيار القسم !',
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
        $scope.AssetLocation = entity;
        $scope.hideEntry = false;
        //$scope.getAssetDepreciationDetails($scope.AssetLocation.ID);       
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
                $scope.getDepartmentList(),

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

    $scope.getDepartmentList = function () {
        AssetOperationMasterService.getDepartmentList().then(function (response) {
            $scope.DepartmentList = response.data;
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
    $scope.AssetLocationPageLoad = function () {
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
                $scope.AssetLocation.Code = parseInt(result.data) + 1;
            }, function (error) { });
        }
    }


}]);
