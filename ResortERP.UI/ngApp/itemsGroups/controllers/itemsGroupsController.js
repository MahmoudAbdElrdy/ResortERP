'use strict';
app.controller('itemsGroupsController', ['$scope', '$location', '$log', '$q', 'authService', 'itemsGroupsService', 'itemsService', 'accountsService', 'localStorageService', function ($scope, $location, $log, $q, authService, itemsGroupsService, itemsService, accountsService, localStorageService) {


    $scope.GoldSetting = false;

    $scope.getSettingType = function () {



        var urlParams = $location.search();
        if (urlParams.foo != null && urlParams.foo != undefined && urlParams.foo != "") {} else {
            $scope.Type = ($location.search()).Type;
            if ($scope.Type != null && $scope.Type != undefined && $scope.Type != "") {

                $scope.GoldSetting = false;
            } else {
                $scope.GoldSetting = true;
            }

        }

    };

    $scope.itemsGroup = {
        GroupID: null,
        GroupCode: "",
        GroupARName: "",
        GroupENName: "",
        GroupMasterID: null,
        GroupRemarks: "",
        AppearOnSalePoint: false,
        CaliberID: null,
        DOESTHEQUANTITYISAPARTOFBARCODE: false,
        QUANTITYLENGTHATTHEBARCODE: null,
        QUANTITYSTARTATTHEBARCODE: null,
        QUANTITYPARTSTARTATTHEBARCODE: null,
        QUANTITYPARTSTARTATTHEBARCODE: null
    };
    $scope.itemsGroupsList = [];
    $scope.itemsGroupsTotalCount = 0;
    $scope.pageNum = 1;
    $scope.pageSize = 10;
    $scope.pagesCount = 0;
    $scope.getPagesCount = function () {
        var div = Math.floor($scope.itemsGroupsTotalCount / $scope.pageSize);
        var rem = $scope.itemsGroupsTotalCount % $scope.pageSize;
        if (rem > 0) {
            div = div + 1;
        }
        $scope.pagesCount = div;
        return div;
    }
    $scope.maxSize = 5;

    $scope.userModel = [];
    $scope.getUserModel = function () {
        if (localStorageService.get('UserModule') != null && localStorageService.get('UserModule') != undefined) {
            $scope.userModel = localStorageService.get('UserModule').UserModule;
        }
    }

    $scope.getByUserModel = function (Id) {
        for (var i = 0; i < $scope.userModel.length; i++) {
            if (parseInt(Id) == parseInt($scope.userModel[i])) {
                return true;
            }
        }

        return false;
    }

    $scope.clearEnity = function () {
        // $scope.itemsGroup = { GroupID: null, GroupCode: "", GroupARName: "", GroupENName: "", GroupMasterID: null, GroupRemarks: "", AppearOnSalePoint: false, CaliberID: null, DOESTHEQUANTITYISAPARTOFBARCODE: false, QUANTITYLENGTHATTHEBARCODE: null, QUANTITYSTARTATTHEBARCODE: null, QUANTITYPARTSTARTATTHEBARCODE: null, QUANTITYPARTSTARTATTHEBARCODE: null };
        $scope.getlastCode();
        $scope.itemsGroup = {
            GroupID: null,
            GroupCode: "",
            GroupARName: "",
            GroupENName: "",
            GroupMasterID: null,
            GroupRemarks: "",
            AppearOnSalePoint: false,
            CaliberID: null,
            DOESTHEQUANTITYISAPARTOFBARCODE: false,
            QUANTITYLENGTHATTHEBARCODE: null,
            QUANTITYSTARTATTHEBARCODE: null,
            QUANTITYPARTSTARTATTHEBARCODE: null,
            QUANTITYPARTSTARTATTHEBARCODE: null
        };

        // $scope.itemsGroup = {};
    }
    $scope.refreshData = function () {
        $scope.getAllOnLoad();
    }
    $scope.saveItemsGroup = function () {


        $scope.saveEntity();
    }
    $scope.saveEntity = function () {
        // debugger;

        if ($scope.itemsGroup !== undefined && $scope.itemsGroup !== null && $scope.itemsGroup.GroupCode !== "" && $scope.itemsGroup.GroupARName !== "" && $scope.itemsGroup.GroupENName !== "") {
            if ($scope.itemsGroup.GroupID === null  || $scope.itemsGroup.GroupID === undefined) {
                $scope.add($scope.itemsGroup).then(function (results) {
                    // var data = results.data;
                    // $scope.itemsGroupsList = data;
                    $scope.clearEnity();
                    $scope.refreshData();
                    swal({
                        title: 'تم',
                        text: 'حفظ بيانات مجموعة الاصناف بنجاح',
                        type: 'success',
                        timer: 1500,
                        showConfirmButton: false
                    });
                }, function (error) {
                    console.log(error.data.message);
                    swal('عفواً',
                        'حدث خطأ عند حفظ بيانات مجموعة الاصناف',
                        'error');
                });
            } else {
                debugger;
                $scope.update($scope.itemsGroup).then(function (results) {
                    // var data = results.data;
                    // $scope.itemsGroupsList = data;

                    $scope.clearEnity();
                    $scope.refreshData();
                    swal({
                        title: 'تم',
                        text: 'تعديل بيانات مجموعة الاصناف بنجاح',
                        type: 'success',
                        timer: 1500,
                        showConfirmButton: false
                    });
                }, function (error) {
                    console.log(error.data.message);
                    swal('عفواً',
                        'حدث خطأ عند تعديل بيانات مجموعة الاصناف',
                        'error');
                });
            }
        }
    }
    $scope.dirEnity = function (entity) {

        $scope.itemsGroup = entity;


    }
    $scope.deleteEnity = function (entity) {
        swal({
            title: 'هل تريد حذف مجموعة الاصناف؟',
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
                        text: "هذا الصنف تم عليه عمليات مختلفة. لا تستطيع حذفه",
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
        //    title: 'هل تريد حذف مجموعة الاصناف؟',
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

    $scope.flagHide = false;

    $scope.getAllOnLoad = function () {

        $q.all(
                [
                    $scope.getSettingType(),
                    $scope.getItemsGroupsList(),
                    $scope.getItemsGroupsTotalCount(),
                    $scope.getAllMainItemGroupsList(),
                    $scope.getAllItemCalibers(),
                    $scope.getAllCostCalculationTypeList(),
                    $scope.getlastCode(),
                    $scope.getAllItemStatusList(),
                    $scope.GetAllGoldBoxAccounts(),
                    $scope.getUserModel()
                ])
            .then(function (allResponses) {
                var flag = !$scope.getByUserModel(8);
                if (flag || $scope.GoldSetting == false) {
                    $scope.flagHide = true
                }

                var urlParams = $location.search();
                if (urlParams.foo != null && urlParams.foo != undefined && urlParams.foo != "") {

                    itemsGroupsService.getByID(urlParams.foo).then(function (result) {
                        $scope.itemsGroup = result.data;
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
    $scope.getItemsGroupsTotalCount = function () {
        $scope.count().then(function (results) {
            var data = results.data;
            $scope.itemsGroupsTotalCount = data;
            $scope.pagesCount = $scope.getPagesCount();



        }, function (error) {

            console.log(error.data.message);
        });
    }
    $scope.getItemsGroupsList = function () {
        $scope.get($scope.pageNum, $scope.pageSize).then(function (results) {
            debugger;
            var data = results.data;
            $scope.itemsGroupsList = data;
        }, function (error) {

            console.log(error.data.message);
        });
    }
    $scope.getID = function(id){
        for(var i=0;i<$scope.itemsGroupsList.length;i++){
          if(id == $scope.itemsGroupsList[i].GroupID)
            return $scope.itemsGroupsList[i].GroupARName;
          }
        }

    $scope.getAllCostCalculationTypeList = function () {

        itemsService.getAllCostCalculationType().then(function (response) {
            // debugger;
            $scope.costCalculationTypes = response.data;
        })
    }



    $scope.getAllItemCalibers = function () {

        itemsService.getAllItemCalibers().then(function (response) {
            // debugger;
            $scope.itemCalibersList = response.data;
        })
    }

    $scope.getAllMainItemGroupsList = function () {
        itemsGroupsService.getAllMainItemGroups().then(function (response) {
            $scope.mainItemsGroupsList = response.data;
        })
    }

    $scope.get = function (pageNum, pageSize) {
        return itemsGroupsService.get(pageNum, pageSize);
    }
    $scope.count = function () {
        return itemsGroupsService.count();
    }
    $scope.add = function (entity) {
        return itemsGroupsService.add(entity);
    }
    $scope.update = function (entity) {
        return itemsGroupsService.update(entity);
    }
    $scope.delete = function (entity) {
        return itemsGroupsService.delete(entity);
    }
    $scope.itemsGroupsPageLoad = function () {

        $scope.getAllOnLoad();

    }

    $scope.pageChanged = function () {
        $scope.getAllOnLoad();
        $log.log('Page changed to: ' + $scope.pageNum);
    };
    ///////////////////////get last code

    $scope.getlastCode = function () {
        var urlParams = $location.search();
        if (urlParams.foo != null && urlParams.foo != undefined && urlParams.foo != "") {} else {
            itemsGroupsService.getLastCode().then(function (result) {
                $scope.itemsGroup.GroupCode = parseInt(result.data) + 1;
            }, function (error) {});
        }

    }


    ////////item status
    $scope.getAllItemStatusList = function () {

        itemsService.getAllItemStatus().then(function (response) {
            $scope.itemStatusList = response.data;
        })
    }


    //GetAllGoldBoxAccounts
    $scope.GetAllGoldBoxAccounts = function () {

        $scope.GoldBoxAccounts = [];
        if (localStorageService.get('branchId') != null) {
            $scope.COM_BRN_ID = localStorageService.get('branchId').branchId;
        } else {
            $scope.COM_BRN_ID = 0;
        }
        accountsService.getAccountBoxByTypesForBill(0, "gold", $scope.COM_BRN_ID).then(function (result) {
            $scope.GoldBoxAccounts = result.data;
        });

    }
}]);