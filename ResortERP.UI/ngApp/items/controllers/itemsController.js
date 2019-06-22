'use strict';
app.controller('itemsController', ['$scope', '$location', '$log', '$q', 'authService', 'itemsService', 'goldWorldPriceService', '$filter', '$mdDialog', 'itemsGroupsService', 'systemOptionsService', 'commonService', 'accountsService', 'itemCaliberService', 'localStorageService', function ($scope, $location, $log, $q, authService, itemsService, goldWorldPriceService, $filter, $mdDialog, itemsGroupsService, systemOptionsService, commonService, accountsService, itemCaliberService, localStorageService) {

    $scope.GoldSetting = false;

    $scope.getSettingType = function () {



        var urlParams = $location.search();
        if (urlParams.foo != null && urlParams.foo != undefined && urlParams.foo != "") { } else {
            $scope.Type = ($location.search()).Type;
            if ($scope.Type != null && $scope.Type != undefined && $scope.Type != "") {

                $scope.GoldSetting = false;
            }
            else {
                $scope.GoldSetting = true;
            }

        }

    };

    $scope.item = {};
    $scope.itemUnit = {};
    $scope.deletedItemUnits = [];
    $scope.IsManufacturingWages = false;
    $scope.IsGmWages = true;

    $scope.moduleResourse = {};
    $scope.inputReuired = {};
    $scope.displayORNot = {};
    $scope.inputDataType = {};
    $scope.requiredValidation = { LoadResource: false };

    $scope.checked = function () {
        $scope.disableVATRate = !$scope.disableVATRate;
    }

    $scope.addItemUnit = function (itemUnit) {
        $scope.itemUnit.Operation = "Insert";
        $scope.itemUnitsList.push(itemUnit);
        $scope.clearItemUnit();
    }

    $scope.userModel = [];
    $scope.getUserModel = function () {
        if (localStorageService.get('UserModule') != null && localStorageService.get('UserModule') != undefined) {
            $scope.userModel = localStorageService.get('UserModule').UserModule;
        }
    }

    $scope.getByUserModel = function (Id) {
        debugger
        for (var i = 0; i < $scope.userModel.length; i++) {
            if (parseInt(Id) == parseInt($scope.userModel[i])) {
                return true;
            }
        }

        return false;
    }

    //Get Last Item Code from Database and add 1 to it
    $scope.getLastGoldWorldPrice = function () {
        goldWorldPriceService.GetLastGoldWorldPrice().then(function (response) {
            $scope.item.GlobalGoldPrice = response.data;
            $scope.item.ManufacturingWages = 0;
            $scope.item.ProfitMargin = 0;
            $scope.item.ItemPrice = $scope.item.GlobalGoldPrice;
            $scope.item.LowestSellingPrice = $scope.item.ItemPrice;
            $scope.item.ItemWeight = 0;
            $scope.getPrices();
        })
    }



    $scope.calculateItemPrice = function () {
        $scope.item.ItemPrice = 0;

        if (parseInt($scope.item.GlobalGoldPrice) != undefined && $scope.item.GlobalGoldPrice != null) {

            $scope.item.ItemPrice = $scope.item.GlobalGoldPrice;
        }
        if (parseInt($scope.item.ManufacturingWages) != undefined && $scope.item.ManufacturingWages != null) {

            $scope.item.ItemPrice = $scope.item.ItemPrice + $scope.item.ManufacturingWages;
        }

        if (parseInt($scope.item.ProfitMargin) != undefined && $scope.item.ProfitMargin != null) {

            $scope.item.ItemPrice = $scope.item.ItemPrice + $scope.item.ProfitMargin;
        }

        if ($scope.item.ManufacturingWages && $scope.item.ItemWeight) {
            if (parseInt($scope.item.ManufacturingWages) && parseInt($scope.item.ItemWeight)) {
                $scope.item.itemGmWages = $scope.item.ManufacturingWages / $scope.item.ItemWeight;
            }
        }

    }



    $scope.editItemUnit = function (itemUnit, index) {
        $scope.itemUnit.Operation = "Edit";
        $scope.itemUnit = itemUnit;
    }

    $scope.deleteItemUnit = function (entity) {
        var result = confirm("هل تريد الحذف؟");
        if (result) {
            $scope.deletedItemUnits.push(entity.ITEM_UNIT_ID);
            var i = $scope.itemUnitsList.indexOf(entity);
            if (i != -1) {
                $scope.itemUnitsList.splice(i, 1);
            }

        }

    }

    $scope.getAllItemCalibers = function () {
        itemsService.getAllItemCalibers().then(function (response) {
            // debugger;
            $scope.itemCalibersList = response.data;
        })
    }
   
    //$scope.itemUnitsList = function (){
    //    debugger;
      
    //        itemsService.getUnitDataUsingUnitCode(1).then(function (response) {
    //            debugger;
    //            var itemUnit = response.data;
    //                $scope.itemUnitsList.push(itemUnit);
                  
    //                $scope.addEmptyItemUnit();
               
    //        })
        
    //}


    $scope.getAllItemGroups = function () {

        itemsService.getAllMainItemGroups().then(function (response) {
            $scope.itemGroupsList = response.data;

            var urlParams = $location.search();
            if (urlParams.new != null && urlParams.new != undefined && urlParams.new != "") {
                // $scope.account.PARENT_ACC_ID = parseInt(urlParams.new);

                itemsService.getByID(urlParams.new).then(function (result) {
                    $scope.item.GROUP_ID = result.data.GROUP_ID;
                });
            }

        })
    }

    $scope.clearItemUnit = function () {
        $scope.itemUnitsList = [];
    }

    $scope.items = [];
    //$scope.itemUnitsList = [];

    $scope.itemsTotalCount = 0;

    $scope.pageNum = 1;
    $scope.pageSize = 10;

    $scope.pagesCount = 0;
    $scope.getPagesCount = function () {
        var div = Math.floor($scope.itemsTotalCount / $scope.pageSize);
        var rem = $scope.itemsTotalCount % $scope.pageSize;
        if (rem > 0) {
            div = div + 1;
        }
        return div;
    }
    $scope.maxSize = 5;



    $scope.clearEnity = function () {
        $scope.item = {};
        $scope.clearItemUnit();
        //  $scope.getLastItemCodeByGroup();
        $scope.deletedItemUnits = [];
        $scope.GetAllCustomersAccounts();
        $scope.getUnitDataUsingUnitCodeLoad(9, 1);
        $scope.customCompanyId = null;
        $scope.customGroupId = null;
      //  $scope.addEmptyItemUnit();
        $scope.imageSrc = null;
        $scope.getLastGoldWorldPrice();
        //   $scope.refreshData();
    }

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

    $scope.refreshData = function () {
        $scope.getAllOnLoad();

    }
    //you need this function to convert the dataURI
    function dataURItoBlob(dataURI) {
        var binary = atob(dataURI.split(',')[1]);
        var mimeString = dataURI.split(',')[0].split(':')[1].split(';')[0];
        var array = [];
        for (var i = 0; i < binary.length; i++) {
            array.push(binary.charCodeAt(i));
        }
        return new Blob([new Uint8Array(array)], {
            type: mimeString
        });
    }




    $scope.saveItems = function () {
        $scope.saveEntity();
    }
    function blobToUint8Array(b) {
        var uri = URL.createObjectURL(b),
            xhr = new XMLHttpRequest(),
            i,
            ui8;

        xhr.open('GET', uri, false);
        xhr.send();

        URL.revokeObjectURL(uri);

        ui8 = new Uint8Array(xhr.response.length);

        for (i = 0; i < xhr.response.length; ++i) {
            ui8[i] = xhr.response.charCodeAt(i);
        }

        return ui8;
    }

    //GetAllCustomersAccounts
    $scope.GetAllCustomersAccounts = function () {
         debugger;
        $scope.CustomersAccounts = [];
        itemsService.getAllCustomersAccounts().then(function (response) {
            $scope.CustomersAccounts = response.data;
        })
    }




    $scope.saveEntity = function () {
        //var fd = new FormData();
        //var imgBlob = dataURItoBlob($scope.uploadme);
        //$scope.items.ITEM_PIC = blobToUint8Array(imgBlob);
        if (!($scope.item.COMPANY_ID != null && $scope.item.COMPANY_ID != undefined)) {
            if ($scope.customCompanyId != null && $scope.customCompanyId != undefined && $scope.customCompanyId.ACC_ID != null && $scope.customCompanyId.ACC_ID != undefined) {
                $scope.item.COMPANY_ID = $scope.customCompanyId.ACC_ID;
            }
            else {
                $scope.item.COMPANY_ID = null;
            }
        }



        if (!($scope.item.GROUP_ID != null && $scope.item.GROUP_ID != undefined)) {
            if ($scope.customGroupId != null && $scope.customGroupId != undefined && $scope.customGroupId.GroupID != null && $scope.customGroupId.GroupID != undefined) {
                $scope.item.GROUP_ID = $scope.customGroupId.GroupID;
            }
            else {
                $scope.item.GROUP_ID = null;
            }
        }

        //Remove last element from itemUnitsList
        if ($scope.item !== undefined && $scope.item !== null && $scope.item.ITEM_CODE !== null && $scope.item.GROUP_ID !== null && $scope.item.ITEM_AR_NAME !== "" && $scope.item.ITEM_EN_NAME !== "") {

            //calc item gm wages
            if ($scope.item.ManufacturingWages != undefined && $scope.item.ManufacturingWages != null ) {
                if ($scope.item.ItemWeight != undefined && $scope.item.ItemWeight != null && $scope.item.ItemWeight != 0) {
                    $scope.item.itemGmWages = (parseFloat($scope.item.ManufacturingWages) / parseFloat($scope.item.ItemWeight)).toFixed(2);
                }
                else {
                    $scope.item.itemGmWages = (parseFloat($scope.item.ManufacturingWages) / 1).toFixed(2);
                }
            }
            else {
                if ($scope.item.ManufacturingWages == 0) {
                    $scope.item.itemGmWages = 0;
                }
            }


            if ($scope.item.ITEM_ID == null) {
                if ($scope.imageSrc != null && $scope.imageSrc != undefined)
                    $scope.item.Item_Base64_Photo = $scope.imageSrc;
                debugger
                if ($scope.itemUnitsList.length > 0 && $scope.itemUnitsList[0].UNIT_CODE != undefined) {
                    itemsService.chkValidat($scope.item.ITEM_CODE, $scope.item.ITEM_AR_NAME, $scope.item.ITEM_EN_NAME).then(function (response) {
                        //var chkArName = response.data.itemArName;
                        var chkEnName = response.data.itemEnName;
                        var chkCode = response.data.itemCode;


                        if (chkEnName == false && chkCode == false) {
                            if ($scope.item.SubjectToVAT == false) {
                                $scope.item.VATRate = 0;
                            }

                            $scope.add($scope.item).then(function (results) {
                                debugger;
                                //insert itemUnits into database
                                if (results.data > 0) {

                                    var chkDefault = false;

                                    for (var i = 0; i < $scope.itemUnitsList.length; i++) {
                                        $scope.itemUnitsList[i].Item_ID = results.data;

                                        if ($scope.itemUnitsList[i].IS_DEFAULT == true) {
                                            chkDefault = true;
                                            $scope.itemUnitsList[i].UNIT_TRANS_RATE = 1;
                                        }
                                    }

                                    if (chkDefault == false && $scope.itemUnitsList.length > 0) {
                                        $scope.itemUnitsList[0].IS_DEFAULT = true;
                                        $scope.itemUnitsList[0].UNIT_TRANS_RATE = 1;
                                    }
                                    debugger;
                                    itemsService.SaveItemUnits($scope.itemUnitsList, []).then(function () {
                                        //$scope.clearEnity();
                                        $scope.refreshData();
                                    });
                                }
                                swal({
                                    title: 'تم',
                                    text: 'حفظ بيانات الصنف بنجاح',
                                    type: 'success',
                                    timer: 1500,
                                    showConfirmButton: false
                                });
                            }, function (error) {
                                debugger;
                                console.log(error.data.message);
                                swal('عفواً',
                                    'حدث خطأ عند حفظ بيانات الصنف',
                                    'error');
                            });
                        }
                        else {
                            var msg = "<div style='text-align:right;direction:rtl'> الحقول التالية موجودة سابقاً : <br><br> <ul style='margin-right: 30px;'>";
                         //   msg += chkArName == true ? "<li>اسم الصنف العربي </li>" : "";
                            msg += chkEnName == true ? "<li>اسم الصنف اللاتيني </li>" : "";
                            msg += chkCode == true ? "<li>كود الصنف </li>" : "";
                            msg += "<br> برجاء تغيير الحقول المكررة</div>";
                            swal("", msg, 'warning');
                        }
                    }, function (error) {
                        debugger;
                        console.log(error.data.message);
                        swal('عفواً',
                            'حدث خطأ عند حفظ بيانات الصنف',
                            'error');
                    });


                }
                /////

                else {
                    swal({
                        title: 'عفوا',
                        text: 'يجب اضافة وحده للعنصر',
                        type: 'error',
                        timer: 1500,
                        showConfirmButton: false
                    });
                }

            }

            else {
                if ($scope.imageSrc != null && $scope.imageSrc != undefined)
                    $scope.item.Item_Base64_Photo = $scope.imageSrc;

                if ($scope.itemUnitsList.length > 0 && $scope.itemUnitsList[0].UNIT_CODE != undefined) {
                    if ($scope.item.SubjectToVAT == false) {
                        $scope.item.VATRate = 0;
                    }
                    $scope.update($scope.item).then(function (results) {
                        if (results.data) {
                            debugger;
                            var chkDefault = false;

                            for (var i = 0; i < $scope.itemUnitsList.length; i++) {
                                $scope.itemUnitsList[i].Item_ID = $scope.item.ITEM_ID;

                                if ($scope.itemUnitsList[i].IS_DEFAULT == true) {
                                    chkDefault = true;
                                    $scope.itemUnitsList[i].UNIT_TRANS_RATE = 1;
                                }
                            }

                            if (chkDefault == false && $scope.itemUnitsList.length > 0) {
                                $scope.itemUnitsList[0].IS_DEFAULT = true;
                                $scope.itemUnitsList[0].UNIT_TRANS_RATE = 1;
                            }


                            itemsService.SaveItemUnits($scope.itemUnitsList, $scope.deletedItemUnits).then(function () {
                                //$scope.clearEnity();
                                $scope.refreshData();
                            });

                        }
                        swal({
                            title: 'تم',
                            text: 'تعديل بيانات الصنف بنجاح',
                            type: 'success',
                            timer: 1500,
                            showConfirmButton: false
                        });
                    }, function (error) {
                        console.log(error.data.message);
                        swal('عفواً',
                            'حدث خطأ عند تعديل بيانات الصنف',
                            'error');
                    });


                }


                else {
                    swal({
                        title: 'عفوا',
                        text: 'يجب اضافة وحده للعنصر',
                        type: 'error',
                        timer: 1500,
                        showConfirmButton: false
                    });
                }
            }
        }
    }
    //filter Selected Account from AccountList during update
    $scope.getAccountBasedOnCompanyId = function (COMPANY_ID) {
        for (var i = 0; i < $scope.CustomersAccounts.length; i++) {
            if ($scope.CustomersAccounts[i].ACC_ID == Number(COMPANY_ID)) {
                return $scope.CustomersAccounts[i];
            }
        }
    }

    //filter Selected ItemGroup from ItemGroupsList during update
    $scope.getItemGroupBasedonGroupId = function (groupId) {
        for (var i = 0; i < $scope.itemGroupsList.length; i++) {
            if ($scope.itemGroupsList[i].GroupID == Number(groupId)) {
                return $scope.itemGroupsList[i];
            }
        }
    }


    //filter Selected ItemCaliber from ItemCalibersList during update
    $scope.getItemCaliberBasedonCaliberId = function (caliberId) {
        //for (var i = 0; i < $scope.CalibersList.length; i++) {
        //    if ($scope.CalibersList[i].GroupID == Number(caliberId)) {
        //        return $scope.CalibersList[i];
        //    }
        //}
    }

    $scope.dirEnity = function (entity) {
        itemsService.getByID(entity.ITEM_ID).then(function (result) {
            $scope.item = result.data;

            if ($scope.item.ItemStatus == 2) {
                $scope.IsManufacturingWages = false;
                $scope.IsGmWages = true;
            }
            else {
                $scope.IsManufacturingWages = true;
                $scope.IsGmWages = false;
            }

            $scope.imageSrc = "data:image/png;base64," + $scope.item.ITEM_PIC;
            $scope.getItemUnitsList($scope.item.ITEM_ID);
            $scope.customCompanyId = $scope.getAccountBasedOnCompanyId(entity.COMPANY_ID);
            $scope.customGroupId = $scope.getItemGroupBasedonGroupId(entity.GROUP_ID);
            $scope.customCaliberId = $scope.getItemCaliberBasedonCaliberId(entity.CaliberID);

            // $scope.getItemGroupdata();

            if ($scope.item.GROUP_ID !== 0 && $scope.item.GROUP_ID !== undefined && $scope.item.GROUP_ID !== null) {
                itemsService.getItemGroupByID($scope.item.GROUP_ID).then(function (result) {

                    $scope.item.DOESTHEQUANTITYISAPARTOFBARCODE = result.data.DOESTHEQUANTITYISAPARTOFBARCODE;
                    $scope.item.QUANTITYLENGTHATTHEBARCODE = result.data.QUANTITYLENGTHATTHEBARCODE;
                    $scope.item.QUANTITYPARTSTARTATTHEBARCODE = result.data.QUANTITYPARTSTARTATTHEBARCODE;
                    $scope.item.QUANTITYPARTLENGTHATTHEBARCODE = result.data.QUANTITYPARTLENGTHATTHEBARCODE;
                    $scope.item.COST_CALCULATION_TYPE = result.data.COST_CALCULATION_TYPE;
                    if (result.data.CaliberID != null && result.data.CaliberID != 0 && result.data.CaliberID != undefined) {
                        $scope.item.CaliberID = result.data.CaliberID;
                    }
                    $scope.groupCode = result.data.GroupCode;
                });
            }

            $scope.item.ItemStatus = entity.ItemStatus;
            $scope.item.GoldAccID = entity.GoldAccID;

        });

    }
    $scope.deleteEnity = function (entity) {
        swal({
            title: 'هل تريد حذف الصنف؟',
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
        //    title: 'هل تريد حذف الصنف؟',
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
        //        //$scope.clearEnity();
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
                $scope.clearEnity(),
                $scope.getItemsList(),
                $scope.getItemsTotalCount(),
                $scope.getAllItemGroups(),
                $scope.getAllItemCalibers(),
                //getLastItemCode(),
                $scope.GetAllCustomersAccounts(),
              //  $scope.addEmptyItemUnit(),
                $scope.getAllItemStatusList(),
                $scope.getAllCostCalculationTypeList(),
                $scope.getSystemOption(),
                $scope.getLastItemCode(),
                $scope.GetAllGoldBoxAccounts(),
                $scope.getUserModel(),
              //  $scope.getUnitDataUsingUnitCodeLoad(1,1) 

            ]).then(function (allResponses) {
            debugger; 
                var flag = !$scope.getByUserModel(8);
                if (flag || $scope.GoldSetting == false) { $scope.flagHide = true; }
                //if all the requests succeeded, this will be called, and $q.all will get an
                //array of all their responses.
                //  console.log(allResponses[0].data);
                var urlParams = $location.search();

                if (urlParams.foo != null && urlParams.foo != undefined && urlParams.foo != "") {
                    //$scope.dirEnity(urlParams.foo);
                    itemsService.getByID(urlParams.foo).then(function (result) {
                        $scope.item = result.data;
                        $scope.item = result.data;
                        $scope.imageSrc = "data:image/png;base64," + $scope.item.ITEM_PIC;
                        $scope.getItemUnitsList($scope.item.ITEM_ID);
                        $scope.customCompanyId = $scope.getAccountBasedOnCompanyId(entity.COMPANY_ID);
                        $scope.customGroupId = $scope.getItemGroupBasedonGroupId(entity.GROUP_ID);
                        $scope.customCaliberId = $scope.getItemCaliberBasedonCaliberId(entity.CaliberID);
                    });
                }
            }, function (error) {
                //This will be called if $q.all finds any of the requests erroring.
                var abc = error;
                var def = 99;
            });
    }
    $scope.getItemsTotalCount = function () {
        $scope.count().then(function (results) {
            var data = results.data;
            $scope.itemsTotalCount = data;
            $scope.pagesCount = $scope.getPagesCount();



        }, function (error) {

            console.log(error.data.message);
        });
    }
    $scope.getItemsList = function () {
        $scope.get($scope.pageNum, $scope.pageSize).then(function (results) {
            var data = results.data;
            $scope.items = data;
        }, function (error) {

            console.log(error.data.message);
        });
    }

    $scope.getItemUnitsList = function (itemID) {
        itemsService.getAllItemUnits(itemID).then(function (results) {
            if (results.data.length > 0) {
                $scope.itemUnitsList = results.data;
            } else {
                $scope.itemUnitsList = [];
                $scope.addEmptyItemUnit();
            }

        }, function (error) {
            console.log(error.data.message);
        });
    }


    $scope.get = function (pageNum, pageSize) {
        return itemsService.get(pageNum, pageSize);
    }
    $scope.count = function () {
        return itemsService.count();
    }
    $scope.add = function (entity) {
        return itemsService.add(entity);
    }
    $scope.update = function (entity) {
        return itemsService.update(entity);
    }
    $scope.delete = function (entity) {
        return itemsService.delete(entity);
    }
    $scope.itemsPageLoad = function () {
        $scope.getAllOnLoad();
    }
    $scope.pageChanged = function () {
        $scope.getAllOnLoad();
        $log.log('Page changed to: ' + $scope.pageNum);
    };


    ////xeditable events
    $scope.updateItemUnitsListOnChange = function (cellName, data, index) {
        if (cellName == 'UNIT_TRANS_RATE') {
            $scope.itemUnitsList[index].UNIT_TRANS_RATE = data;
        } else if (cellName == 'IS_DEFAULT') {
            for (var i = 0; i < $scope.itemUnitsList.length; i++) {
                $scope.itemUnitsList[i].IS_DEFAULT = false;
            }
            $scope.itemUnitsList[index].IS_DEFAULT = data;

        } else if (cellName == 'UNIT_BARCODE') {
            $scope.itemUnitsList[index].UNIT_BARCODE = data;
        } else if (cellName == 'WHOLE_PRICE') {
            $scope.itemUnitsList[index].WHOLE_PRICE = data;
        } else if (cellName == 'HALF_WHOLE_PRICE') {
            $scope.itemUnitsList[index].HALF_WHOLE_PRICE = data;
        } else if (cellName == 'RETAIL_PRICE') {
            $scope.itemUnitsList[index].RETAIL_PRICE = data;
        } else if (cellName == 'CONSUMER_PRICE') {
            $scope.itemUnitsList[index].CONSUMER_PRICE = data;
        } else if (cellName == 'EMP_PRICE') {
            $scope.itemUnitsList[index].EMP_PRICE = data;
        } else if (cellName == 'EXPORT_PRICE') {
            $scope.itemUnitsList[index].EXPORT_PRICE = data;
        } else if (cellName == 'LAST_BUY_PRICE') {
            $scope.itemUnitsList[index].LAST_BUY_PRICE = data;
        }

    }


    //function to get all details of item using itemcode to insert to grid view using item code

    $scope.getUnitDataUsingUnitCode = function (UnitCode, Index) {
        debugger;
        var selectedItemsUnitsBeforeChange = $scope.itemUnitsList;
        if (UnitCode != "" && UnitCode != null) {
            itemsService.getUnitDataUsingUnitCode(UnitCode).then(function (response) {
               
                var itemUnit = response.data;
                if (itemUnit != undefined && itemUnit != null) {
                    if ($scope.itemUnitsList[Index] != null && $scope.itemUnitsList[Index] != undefined) {
                        $scope.removeItemUnit(Index);
                    }
                    $scope.itemUnitsList.push(itemUnit);
                    debugger;
                    $scope.addEmptyItemUnit();
                }
                else {
                    $scope.removeItemUnit(Index);
                    $scope.addEmptyItemUnit();
                }
            })
        }
    }
    $scope.getUnitDataUsingUnitCodeLoad = function (UnitCode, Index) {
        debugger;
        var selectedItemsUnitsBeforeChange = $scope.itemUnitsList;
        if (UnitCode != "" && UnitCode != null) {
            itemsService.getUnitDataUsingUnitCode(UnitCode).then(function (response) {

                var itemUnit = response.data;
                if (itemUnit != undefined && itemUnit != null) {
                    if ($scope.itemUnitsList[Index] != null && $scope.itemUnitsList[Index] != undefined) {
                        $scope.removeItemUnit(Index);
                    }
                    if (itemUnit != null) {
                        debugger;
                        $scope.itemUnitsList.push(itemUnit);
                    }
                   
                   
                  //  $scope.addEmptyItemUnit();
                }
                else {
                   // $scope.removeItemUnit(Index);
                   // $scope.addEmptyItemUnit();
                }
            })
        }
    }
    $scope.removeItemUnit = function (index) {

        $scope.deletedItemUnits.push($scope.itemUnitsList[index].ItemUnitID)
        $scope.itemUnitsList.splice(index, 1);
    };

    $scope.addEmptyItemUnit = function () {
        debugger;
        var found = false;
        for (var i = 0; i < $scope.itemUnitsList.length; i++) {
            if ($scope.itemUnitsList[i].UNIT_ID == null || $scope.itemUnitsList[i].UNIT_ID == undefined) {
                found = true;
            }
        }
        if (!found) {

            var empty = {
                Item_ID: null, ItemUnitID: null, UNIT_CODE: null, UNIT_AR_NAME: null, UNIT_TRANS_RATE: null, IS_DEFAULT: false, UNIT_BARCODE: null, UNIT_ID: null
                , WHOLE_PRICE: 0, HALF_WHOLE_PRICE: 0, EMP_PRICE: 0, EXPORT_PRICE: 0, RETAIL_PRICE: 0, CONSUMER_PRICE: 0, LAST_BUY_PRICE: 0, Operation: 'Insert'
            };
            $scope.itemUnitsList.push(empty);
        }
    };

    $scope.getAllItemStatusList = function () {

        itemsService.getAllItemStatus().then(function (response) {
            //  debugger;
            $scope.itemStatusList = response.data;
        })
    }
    $scope.getAllCostCalculationTypeList = function () {

        itemsService.getAllCostCalculationType().then(function (response) {
            //  debugger;
            $scope.costCalculationTypes = response.data;
        })
    }

    $scope.itemTypes = [{ typeID: 1, typeName: "مخزني" }, { typeID: 2, typeName: "خدمي" }];
    $scope.itemMainUnits = [];
    //$scope.costCalculationTypes = [{ costTypeID: 1, costTypeName: "المتوسط AVG" }, { costTypeID: 2, costTypeName: "وارد اولا يصرف اولا FIFO" }, { costTypeID: 3, costTypeName: "وارد اخرا يصرف اولا LIFO" }];


    $scope.setItemColor = function (ITEM_COLOR) {
        $scope.item.ITEM_COLOR = ITEM_COLOR;// document.getElementById("item.ITEM_COLOR").value;
        $scope.item.ITEM_COLOR = ITEM_COLOR;
        console.log(ITEM_COLOR);
    }

    $scope.ITEM_COLOR = '#ff0000';

    $scope.getItemGroupdata = function () {
        if ($scope.item.GROUP_ID !== 0 && $scope.item.GROUP_ID !== undefined && $scope.item.GROUP_ID !== null) {
            itemsService.getItemGroupByID($scope.item.GROUP_ID).then(function (result) {

                $scope.item.DOESTHEQUANTITYISAPARTOFBARCODE = result.data.DOESTHEQUANTITYISAPARTOFBARCODE;
                $scope.item.QUANTITYLENGTHATTHEBARCODE = result.data.QUANTITYLENGTHATTHEBARCODE;
                $scope.item.QUANTITYPARTSTARTATTHEBARCODE = result.data.QUANTITYPARTSTARTATTHEBARCODE;
                $scope.item.QUANTITYPARTLENGTHATTHEBARCODE = result.data.QUANTITYPARTLENGTHATTHEBARCODE;
                $scope.item.COST_CALCULATION_TYPE = result.data.COST_CALCULATION_TYPE;
                if (result.data.CaliberID != null && result.data.CaliberID != 0 && result.data.CaliberID != undefined) {
                    $scope.item.CaliberID = result.data.CaliberID;
                }
                $scope.groupCode = result.data.GroupCode;
                $scope.item.ItemStatus = result.data.ItemStatusID;
                $scope.item.GoldAccID = result.data.GoldAccID;
                $scope.getClearnessRateByCaliber();

            });
        }
    }



    $scope.getPrices = function () {
        $scope.item.KiloPrice = $scope.item.ItemPrice * 1000;
        $scope.item.OuncePrice = $scope.item.ItemPrice;
    }


    //////////////////////////////////////////////////////////////////////////////////////////
    $scope.getSystemOption = function () {
        var userName = authService.GetUserName();
        itemsService.getSystemOptionsByUserName(userName).then(function (response) {
            $scope.setting = response.data;
            $scope.isShowPrice = $scope.setting.IsShowPrices;
            //if ($scope.setting.IsItemCodeRelatedByGroup == true) {
            //    var separtor = '';
            //    $scope.item.IsItemCodeRelatedByGroup = true;
            //    if ($scope.setting.CodeSeparator != undefined && $scope.setting.CodeSeparator != null) {
            //        separtor = $scope.setting.CodeSeparator;

            //    }
            //    $scope.getLastItemCodeByGroup(separtor);
            //}
        });
    }


    //Get Last Item Code by group id  from Database and add 1 to it
    $scope.getLastItemCodeByGroup = function (separtor) {

        var urlParams = $location.search();
        if (urlParams.foo != null && urlParams.foo != undefined && urlParams.foo != "") {
        } else {
            itemsService.getLastItemCodeByGroup($scope.item.GROUP_ID).then(function (response) {
                $scope.item.ITEM_CODE = $scope.groupCode + separtor + (parseInt(response.data) + 1);
            })
        }
    }


    $scope.getLastItemCode = function () {


        var urlParams = $location.search();
        if (urlParams.foo != null && urlParams.foo != undefined && urlParams.foo != "") {
        } else {
            itemsService.getLastItemCode().then(function (response) {
                $scope.item.ITEM_CODE = parseInt(response.data) + 1;
            })
        }
    }

    $scope.SetWagesType = function (itemStatusId) {
        if (itemStatusId == 2) {
            $scope.IsManufacturingWages = false;
            $scope.IsGmWages = true;
        }
        else {
            $scope.IsManufacturingWages = true;
            $scope.IsGmWages = false;
        }
    }

    $scope.calculateItemGmWages = function () {

        if ($scope.item.itemGmWages && $scope.item.ItemWeight) {
            if (parseInt($scope.item.itemGmWages) && parseInt($scope.item.ItemWeight)) {
                $scope.item.ManufacturingWages = $scope.item.itemGmWages * $scope.item.ItemWeight;
            }
        }
    }


    $scope.fnExcelReport = function () {

        itemsService.getAll().then(function (response) {
            commonService.getExcelFile("sample-table-2", "Sheet 1", "Items", response.data);
        })

    }




    //GetAllGoldBoxAccounts
    $scope.GetAllGoldBoxAccounts = function () {

        $scope.GoldBoxAccounts = [];
        $scope.compBranch = 0;
        if (localStorageService.get('branchId') != null) {

            $scope.compBranch = localStorageService.get('branchId').branchId;
            $scope.COM_BRN_IDForFilter = localStorageService.get('branchId').branchId;

        }

        accountsService.getAccountBoxByTypesForBill(0, "gold", $scope.compBranch).then(function (result) {
            $scope.GoldBoxAccounts = result.data;
        });
    }
    $scope.select2EventEnter = function () {

        $timeout(function () {

            document.getElementById("deliveryTxt").focus();

        }, 100, false);


        //  alert(document.getElementById("deliveryTxt"))
        //document.getElementById("deliveryTxt").focus();
        // $("#deliveryTxt").trigger("click");
        // document.getElementById("deliveryTxt").select();
    }

    $scope.getClearnessRateByCaliber = function () {

        var rate = 0;
        itemCaliberService.getByID($scope.item.CaliberID).then(function (result) {
            if (result.data != undefined && result.data != null) {
                if ($scope.item.CaliberID == 1) {
                    rate = (parseFloat(24 / 24) * parseFloat(1000)).toFixed(2);
                }
                else if ($scope.item.CaliberID == 2) {
                    rate = (parseFloat(22 / 24) * parseFloat(1000)).toFixed(2);
                }

                else if ($scope.item.CaliberID == 3) {
                    rate = (parseFloat(21 / 24) * parseFloat(1000)).toFixed(2);
                }

                else if ($scope.item.CaliberID == 4) {
                    rate = (parseFloat(18 / 24) * parseFloat(1000)).toFixed(2);
                }
            }
            $scope.item.ClearnessRate = rate;
        });
    }

}]);
