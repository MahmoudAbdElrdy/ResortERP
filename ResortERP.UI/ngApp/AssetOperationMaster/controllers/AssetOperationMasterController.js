'use strict';
app.controller('AssetOperationMasterController', ['$scope', '$location', '$filter', '$log', '$q', '$document', '$uibModal', 'AssetOperationMasterService', 'commonService', 'authService', 'entryService', 'localStorageService', function ($scope, $location, $filter, $log, $q, $document, $uibModal, AssetOperationMasterService, commonService, authService, entryService, localStorageService ) {
    var UserId = authService.GetUserID();
    $scope.OperationType = $location.search().OperationType
    $scope.AssetOperationMaster = {
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
        $scope.AssetOperationMaster.OperationDetails.push({
            RowNumber: $scope.AssetOperationMaster.OperationDetails.length + 1,
            Value: 0,
            AddedBy: UserId
        });
    }

    $scope.removeoperationDetail = function (index) {
        $scope.AssetOperationMaster.OperationDetails.splice(index, 1);
    }

    $scope.clearEnity = function () {
        $scope.AssetOperationMaster = {
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

    $scope.AssetOperationMasterList = [];
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

    $scope.changeMasterCostCenter = function () {
        if ($scope.AssetOperationMaster.CostCenterID != undefined && $scope.AssetOperationMaster.CostCenterID != "") {
            angular.forEach($scope.AssetOperationMaster.OperationDetails, function (item, index) {
                //if (item.CostCenterID == undefined || item.CostCenterID == "") {
                item.CostCenterID = $scope.AssetOperationMaster.CostCenterID;
                //}
            })
        }
    }

    $scope.changeMasterSeller = function () {
        if ($scope.AssetOperationMaster.SellerAccountID != undefined && $scope.AssetOperationMaster.SellerAccountID != "") {
            angular.forEach($scope.AssetOperationMaster.OperationDetails, function (item, index) {
                //if (item.SellerAccountID == undefined || item.SellerAccountID == "") {
                item.SellerAccountID = $scope.AssetOperationMaster.SellerAccountID;
                //}
            })
        }
    }

    $scope.changeMasterDistributer = function () {
        if ($scope.AssetOperationMaster.DistributorID != undefined && $scope.AssetOperationMaster.DistributorID != "") {
            angular.forEach($scope.AssetOperationMaster.OperationDetails, function (item, index) {
                //if (item.DistributorID == undefined || item.DistributorID == "") {
                item.DistributorID = $scope.AssetOperationMaster.DistributorID;
                //}
            })
        }
    }

    $scope.saveAssetOperationMaster = function () {
        $scope.saveEntity();
    }
    $scope.saveEntity = function () {
        if ($scope.AssetOperationMaster !== undefined && $scope.AssetOperationMaster !== null && $scope.AssetOperationMaster.Code !== undefined &&
            $scope.AssetOperationMaster.Code !== "" && $scope.AssetOperationMaster.Number !== undefined &&
            $scope.AssetOperationMaster.Number !== "" && $scope.AssetOperationMaster.FromAccountID !== undefined &&
            $scope.AssetOperationMaster.FromAccountID !== "" && $scope.AssetOperationMaster.AssetOperationTypeID !== undefined &&
            $scope.AssetOperationMaster.AssetOperationTypeID !== "") {
            var filteredArray = $filter('filter')($scope.AssetOperationMaster.OperationDetails, function (item) {
                return item.AssetMasterID != undefined &&
                    item.AssetMasterID != "" &&
                    item.Value != undefined && item.Value != "";
            });
            if (filteredArray.length > 0) {
                $scope.AssetOperationMaster.OperationDetails = filteredArray;
                if ($scope.AssetOperationMaster.ID === null || $scope.AssetOperationMaster.ID === 0 || $scope.AssetOperationMaster.ID === undefined) {
                    var UserId = authService.GetUserID();
                    if (UserId != undefined && UserId != "") {
                        $scope.AssetOperationMaster.AddedBy = UserId;
                    }
                    $scope.insert($scope.AssetOperationMaster).then(function (results) {
                        if (results.data != false) {
                            $scope.SaveEntry(results.data);
                            $scope.clearEnity();
                            $scope.getAssetOperations();
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
                        $scope.AssetOperationMaster.UpdatedBy = UserId;
                    }
                    $scope.update($scope.AssetOperationMaster).then(function (results) {
                        if (results.data != false) {
                            $scope.SaveEntry($scope.AssetOperationMaster.ID);
                            $scope.clearEnity();
                            $scope.getAssetOperations();
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
                    text: 'يجب اختيار اصل واحد على الاقل!',
                    type: 'error',
                    timer: 1500,
                    showConfirmButton: false
                });
            }
        }
        else {
            swal({
                title: 'خطأ',
                text: 'يجب اختيار الحساب وادخال الكود ورقم العملية!',
                type: 'error',
                timer: 1500,
                showConfirmButton: false
            });
        }
    }

    $scope.SaveEntry = function (AssetOperationMasterID) {
        var totalValue = 0;
        //console.log(AssetOperationMasterID);
        var EntryMaster = {
            ENTRY_SETTING_ID: 30,
            ENTRY_DATE: new Date(),
            ENTRY_NUMBER: AssetOperationMasterID,
            CURRENCY_ID: $scope.AssetOperationMaster.CurrencyID == undefined || $scope.AssetOperationMaster.CurrencyID == "" ? 1 : $scope.AssetOperationMaster.CurrencyID,
            CURRENCY_RATE: $scope.AssetOperationMaster.CurrencyRate == undefined || $scope.AssetOperationMaster.CurrencyRate == "" ? 1 : $scope.AssetOperationMaster.CurrencyRate,
            AssetOperationMasterID: AssetOperationMasterID,
        }
        var entryDetails = []

        if (localStorageService.get('branchId') != null) {
            EntryMaster.COM_BRN_ID = localStorageService.get('branchId').branchId;
        }
        angular.forEach($scope.AssetOperationMaster.OperationDetails, function (item, index) {
            var assetAccount = $filter('filter')($scope.AssetMasterList, function (account) {
                return account.OptionValue == item.AssetMasterID
            })[0].AccountId;
            if (assetAccount != undefined && assetAccount != null && assetAccount != 0) {
                totalValue += parseFloat(item.Value);

                var entryDetailObject = {
                    ENTRY_ROW_NUMBER: index,
                    ENTRY_CREDIT: item.Value,
                    COST_CENTER_ID: item.CostCenterID,
                    ACC_ID: assetAccount
                };
                entryDetails.push(entryDetailObject);
            }
        })
        EntryMaster.ENTRY_CREDIT = totalValue;
        EntryMaster.ENTRY_DEBIT = totalValue;
        entryDetails.push({
            ENTRY_ROW_NUMBER: $scope.AssetOperationMaster.OperationDetails.length,
            ENTRY_DEBIT: totalValue,
            COST_CENTER_ID: $scope.AssetOperationMaster.CostCenterID,
            ACC_ID: $scope.AssetOperationMaster.FromAccountID
        });

        var entryMasterDetails = { EntryMaster: EntryMaster, EntryDetails: entryDetails };
        //console.log(entryMasterDetails);
        //console.log($scope.AssetMasterList)
        entryService.InsertEntryAsset(entryMasterDetails).then(function (result) {
            console.log("entry saved");
        })
    }


    ////////////////////////////// Open Bill Entry ////////////////////////////////
    $scope.openEntryPopup = function () {

        var parentElem = angular.element($document[0].querySelector('.app-content'));
        $scope.EntryModalOpenItm = false;
        $scope.FromPopUpentryType = 130;

        $scope.entrySettingId = 130;
        $scope.popUpData = { entrySettingId: $scope.entrySettingId, AssetOperationId: $scope.AssetOperationMaster.ID };

        var temp = "<div  ng-include src=\"'../../ngApp/Entries/views/entry.html'\"></div>";

        var modalInstance = $uibModal.open({
            //templateUrl: 'accountModal.html',
            template: temp,
            controller: 'modalCtrl',
            scope: $scope,
            preserveScope: true,
            resolve: {
                popUpData: function () {
                    return $scope.popUpData;
                }
            }
        });
        modalInstance.opened.then(function () {
            $scope.EntryModalOpenItm = true;
        });

        // we want to update state whether the modal closed or was dismissed,
        // so use finally to handle both resolved and rejected promises.
        modalInstance.result.finally(function (selectedItem) {
            $scope.EntryModalOpenItm = false;
        });
    }

    $scope.dirEnity = function (entity) {
        $scope.AssetOperationMaster = entity;
        $scope.hideEntry = false;
        //$scope.getAssetDepreciationDetails($scope.AssetOperationMaster.ID);       
    }

    //$scope.getAssetDepreciationDetails = function (AssetOperationMasterId) {
    //    AssetOperationMasterService.getAssetDepreciationDetails(AssetOperationMasterId).then(function (responce) {
    //        $scope.AssetOperationMaster.Depreciation = responce.data;
    //    });
    //}

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
                    $scope.getAssetOperations();
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
                $scope.getCostCenterList(),
                $scope.getDepartmentList(),
                $scope.getCompanyList(),
                $scope.getCurrencyList(),
                $scope.getAccountList(),

                $scope.getAssetOperations(),

            ]).then(function (allResponses) {
                $scope.clearEnity();

                //var urlParams = $location.search();
                //if (urlParams.foo != null && urlParams.foo != undefined && urlParams.foo != "") {

                //    AssetOperationMasterService.getById(parseInt(urlParams.foo)).then(function (results) {

                //        $scope.AssetOperationMaster = results.data;
                //        $scope.dirEnity(results.data);
                //    })


                //}

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

    $scope.getCostCenterList = function () {
        AssetOperationMasterService.getCostCenterList().then(function (response) {
            $scope.CostCenterList = response.data;
        })
    }
    $scope.getDepartmentList = function () {
        AssetOperationMasterService.getDepartmentList().then(function (response) {
            $scope.DepartmentList = response.data;
        })
    }

    $scope.getCompanyList = function () {
        AssetOperationMasterService.getCompanyList().then(function (response) {
            $scope.CompanyList = response.data;
        })
    }

    $scope.getCurrencyList = function () {
        AssetOperationMasterService.getCurrencyList().then(function (response) {
            $scope.CurrencyList = response.data;
        })
    }
    $scope.getAccountList = function () {
        AssetOperationMasterService.getAccountList().then(function (response) {
            $scope.AccountList = response.data;
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
    $scope.AssetOperationMasterPageLoad = function () {
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
                $scope.AssetOperationMaster.Code = parseInt(result.data) + 1;
            }, function (error) { });
        }
    }




    //////////////////////////load image
    $scope.imageSrc = null;

    $scope.imageUpload = function (event) {
        var files = event.target.files; //FileList object

        for (var i = 0; i < files.length; i++) {
            var file = files[i];
            var reader = new FileReader();
            reader.onload = $scope.imageIsLoaded;
            reader.readAsDataURL(file);
        }
    }

    $scope.imageIsLoaded = function (e) {
        $scope.$apply(function () {
            $scope.imageSrc = e.target.result;
        });
    }



    $scope.setLocalStorage = function () {

        return false;

        var a = $("input[name=barcode]").attr("ng-model");
        console.log(a);

        //var queryResult = document.querySelectorAll('[ng-model]');
        ///console.log(queryResult);

        var FieldName = a.split('.')[1];
        console.log(FieldName);

        var queryResult = document.querySelectorAll("[ng-model]");
        angular.forEach(queryResult, function (val, key) {

            var div_element = angular.element(val);

            var ngModel_attr = div_element.attr("ng-model");

            var FieldName = ngModel_attr.split('.')[1];

            div_element.attr("data-test", FieldName);

            //var label = $(div_element).prev().prev('label');

            var label = $(div_element).prevAll('label');

            ////console.log(label);

            $(label).attr("data-aaaa", "123456789");


        });


    }



}]);
