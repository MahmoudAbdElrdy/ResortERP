'use strict';
app.controller('AssetMasterController', ['$scope', '$location', '$log', '$q', 'AssetMasterService', 'commonService', 'authService', function ($scope, $location, $log, $q, AssetMasterService, commonService, authService) {
    $scope.AssetMaster = {
        Depreciation: {}
    };
    
    $scope.clearEnity = function () {
        $scope.AssetMaster = {
            Depreciation: {}
        };
        $scope.imageSrc = null;
        //  $scope.addGridItem();
        $scope.getlastCode();
    }

    $("#tabs").tabs({
        active: 0,
        //collapsible: true,
        //active: false,
        //   event: "mouseover",
        //disabled: true,
        classes: {
            "ui-tabs": "highlight"
        },
        heightStyle: "content",
        //hide: true
        // hide: { effect: "explode", duration: 1000 },
        //   show: { effect: "blind", duration: 800 }
    });

    $scope.imageSrc = null;
  
    $scope.assetmasterList = [];
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
    $scope.saveAssetMaster = function () {
        $scope.saveEntity();
    }
    $scope.saveEntity = function () {
        if ($scope.AssetMaster !== undefined && $scope.AssetMaster !== null && $scope.AssetMaster.Code !== undefined &&
            $scope.AssetMaster.Code !== "" && $scope.AssetMaster.ARName !== undefined &&
            $scope.AssetMaster.ARName !== "" && $scope.AssetMaster.LatName !== undefined && $scope.AssetMaster.LatName !== "") {           
            if ($scope.AssetMaster.ID === null || $scope.AssetMaster.ID === 0 || $scope.AssetMaster.ID === undefined) {
                if ($scope.imageSrc != null && $scope.imageSrc != undefined)
                    $scope.AssetMaster.asset_Base64_Photo = $scope.imageSrc;
                var UserId = authService.GetUserID();
                if (UserId != undefined && UserId != "") {
                    $scope.AssetMaster.AddedBy = UserId;
                }
                $scope.insert($scope.AssetMaster).then(function (results) {
                    if (results.data) {
                        $scope.clearEnity();
                        $scope.getAssetsList();
                        swal({
                            title: 'اضافة أصل',
                            text: 'تم الحفظ بنجاح',
                            type: 'success',
                            timer: 1500,
                            showConfirmButton: false
                        });
                    }
                    else {
                        swal({
                            title: 'اضافة أصل',
                            text: 'الاسم او الكود موجود من قبل',
                            type: 'error',
                            timer: 1500,
                            showConfirmButton: false
                        });
                    }
                    //for (var i = 0; i < $scope.TelephoneList.length; i++) {
                    //    $scope.TelephoneList[i].TELE_ID = $scope.AssetMaster.EMP_ID;
                    //}
                    //var index = $scope.TelephoneList.length - 1;
                    //if ($scope.TelephoneList[index] != null && $scope.TelephoneList[index] != undefined && $scope.TelephoneList[index].TELE_NUMBER == null && $scope.TelephoneList[index].TELE_CAT_ID == null)
                    //    $scope.removeGridItem(index);
                    //$scope.insertEmpContacts($scope.TelephoneList).then(function () {
                    //    $scope.clearEnity(); $scope.refreshData();
                    //});
                    //commonService.showInsertAlert($scope);
                }, function (error) {
                    console.log(error.data.message);
                    swal({
                        title: 'اضافة أصل',
                        text: 'حدث خطأ ما!',
                        type: 'error',
                        timer: 1500,
                        showConfirmButton: false
                    });
                    //commonService.showErrorInsertAlert($scope);
                });
            } else {
                if ($scope.imageSrc != null && $scope.imageSrc != undefined)
                    $scope.AssetMaster.asset_Base64_Photo = $scope.imageSrc;

                var UserId = authService.GetUserID();
                if (UserId != undefined && UserId != "") {
                    $scope.AssetMaster.UpdatedBy = UserId;
                }
                $scope.update($scope.AssetMaster).then(function (results) {
                    if (results.data) {
                        $scope.clearEnity();
                        $scope.getAssetsList();
                        swal({
                            title: 'تعديل أصل',
                            text: 'تم التعديل بنجاح',
                            type: 'success',
                            timer: 1500,
                            showConfirmButton: false
                        });
                    }
                    else {
                        swal({
                            title: 'اضافة أصل',
                            text: 'الاسم او الكود موجود من قبل',
                            type: 'error',
                            timer: 1500,
                            showConfirmButton: false
                        });
                    }
                }, function (error) {
                    console.log(error.data.message);
                    swal({
                        title: 'تعديل أصل',
                        text: 'حدث خطأ ما!',
                        type: 'error',
                        timer: 1500,
                        showConfirmButton: false
                    });
                    //commonService.showErrorUpdateAlert($scope);
                });
            }
        }
    }
    $scope.dirEnity = function (entity) {
        debugger
        $scope.imageSrc = "data:image/png;base64," + entity.AssetPhoto;
        $scope.AssetMaster = entity;
        $scope.AssetMaster.ManufactureDate = new Date(entity.ManufactureDate);
        $scope.AssetMaster.WarrantyStartDate = new Date(entity.WarrantyStartDate);

        $scope.AssetMaster.WarrantyEndDate = new Date(entity.WarrantyEndDate);
        $scope.AssetMaster.ReceivingDate = new Date(entity.ReceivingDate);

        $scope.AssetMaster.ContractDate = new Date(entity.ContractDate);
        $scope.AssetMaster.PurchasingDate = new Date(entity.PurchasingDate);


        $scope.AssetMaster.CustomsStatmentDate = new Date(entity.CustomsStatmentDate);
        $scope.AssetMaster.ShippingDate = new Date(entity.ShippingDate);

        $scope.AssetMaster.ShippingArrivalDate = new Date(entity.ShippingArrivalDate);
        
        
        $scope.getAssetDepreciationDetails($scope.AssetMaster.ID);       
    }

    $scope.getAssetDepreciationDetails = function (AssetMasterId) {
        AssetMasterService.getAssetDepreciationDetails(AssetMasterId).then(function (responce) {
            $scope.AssetMaster.Depreciation = responce.data;
        });
    }

    $scope.deleteEnity = function (entity) {
        swal({
            title: 'هل تريد حذف هذا الاصل؟',
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
                    $scope.getAssetsList();
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

    // status of nationality

    //$scope.employeeNatList = [{ typeID: 1, typeName: "مواطن" }, { typeID: 2, typeName: "مقيم (مغترب) " }];
    //$scope.EmpNationStatus = 1;
    //$scope.empNatStaShow = true;
    //$scope.empNatStaShow2 = false;


    //$scope.getUserStatusOfNat = function () {
    //    if ($scope.EmpNationStatus == 1) {
    //        $scope.empNatStaShow = true;
    //        $scope.empNatStaShow2 = false;
    //    } else if ($scope.EmpNationStatus == 2) {
    //        $scope.empNatStaShow = false;
    //        $scope.empNatStaShow2 = true;
    //        }

    //};


    $scope.getAllOnLoad = function () {
        $q.all(
            [
                $scope.getAssetsTotalCount(),
                $scope.getAssetGroupList(),
                $scope.getAssetTypeList(),
                $scope.getAssetStatusList(),
                $scope.getOriginNationList(),
                $scope.getCompanyList(),
                $scope.getAssetDepreciationTypeList(),
                $scope.getAssetLifeSpanUnitList(),
                $scope.getCurrencyList(),
                $scope.getAccountList(),
                $scope.getDepartmentList(),

                $scope.getAssetsList(),
                //$scope.getCityList(),
                //$scope.getNationalityList(),
                //$scope.getJobTitleList(),
                //$scope.getJobLevelList(),
                //$scope.getDeptartmentList(),
                //$scope.getEmployeeStatusList(),
                //$scope.getDirectManagerList(),
                //$scope.getContactTypeList(),
                //$scope.getAcademicDegreeList(),
                //$scope.getBankList()

            ]).then(function (allResponses) {
                $scope.clearEnity();

                var urlParams = $location.search();
                if (urlParams.foo != null && urlParams.foo != undefined && urlParams.foo != "") {

                    AssetMasterService.getById(parseInt(urlParams.foo)).then(function (results) {
           
                        $scope.AssetMaster = results.data;
                        $scope.dirEnity(results.data);
                    })


                }

            }, function (error) {
                //This will be called if $q.all finds any of the requests erroring.
                var abc = error;
                var def = 99;
            });
    }

    $scope.getAssetGroupList = function () {
        AssetMasterService.getAssetGroupList().then(function (response) {
            $scope.AssetGroupList = response.data;
        })
    }

    $scope.getAssetTypeList = function () {
        AssetMasterService.getAssetTypeList().then(function (response) {
            $scope.AssetTypeList = response.data;
        })
    }
    $scope.getAssetStatusList = function () {
        AssetMasterService.getAssetStatusList().then(function (response) {
            $scope.AssetStatusList = response.data;
        })
    }
    $scope.getOriginNationList = function () {
        AssetMasterService.getOriginNationList().then(function (response) {
            $scope.OriginNationList = response.data;
        })
    }
    $scope.getNationalityList = function () {
        AssetMasterService.getNationalityList().then(function (response) {
            $scope.NationalityList = response.data;
        })
    }
    $scope.getCompanyList = function () {
        AssetMasterService.getCompanyList().then(function (response) {
            $scope.CompanyList = response.data;
        })
    }
    $scope.getAssetDepreciationTypeList = function () {
        AssetMasterService.getAssetDepreciationTypeList().then(function (response) {
            $scope.AssetDepreciationTypeList = response.data;
        })
    }
    $scope.getAssetLifeSpanUnitList = function () {
        AssetMasterService.getAssetLifeSpanUnitList().then(function (response) {
            $scope.AssetLifeSpanUnitList = response.data;
        })
    }
    $scope.getCurrencyList = function () {
        AssetMasterService.getCurrencyList().then(function (response) {
            $scope.CurrencyList = response.data;
        })
    }
    $scope.getAccountList = function () {
        AssetMasterService.getAccountList().then(function (response) {
            $scope.AccountList = response.data;
        })
    }

    $scope.getDepartmentList = function () {
        AssetMasterService.getDepartmentList().then(function (response) {
            $scope.DepartmentList = response.data;
        })
    }
    //$scope.getContactTypeList = function () {
    //    AssetMasterService.getContactTypeList().then(function (response) {
    //        $scope.ContactTypeList = response.data;
    //    })
    //}
    //$scope.getAcademicDegreeList = function () {
    //    AssetMasterService.getAcademicDegreeList().then(function (response) {
    //        $scope.AcademicDegreeList = response.data;
    //    })
    //}
    //$scope.getBankList = function () {
    //    AssetMasterService.getBankList().then(function (response) {
    //        $scope.BankList = response.data;
    //    })
    //}

    $scope.getAssetsTotalCount = function () {
        $scope.count().then(function (results) {
            var data = results.data;
            $scope.assetsTotalCount = data;
            $scope.pagesCount = $scope.getPagesCount();
        }, function (error) {
            console.log(error.data.message);
        });
    }
    $scope.getAssetsList = function () {
        $scope.get($scope.pageNum, $scope.pageSize).then(function (results) {
            var data = results.data;
            $scope.AssetsList = data;

        }, function (error) {
            console.log(error.data.message);
        });
    }


    $scope.get = function (pageNum, pageSize) {
        return AssetMasterService.get(pageNum, pageSize);
    }
    $scope.count = function () {
        return AssetMasterService.count();
    }
    $scope.insert = function (entity) {
        return AssetMasterService.insert(entity);
    }
    $scope.insertGetID = function (entity) {
        return AssetMasterService.insertGetID(entity);
    }
    //$scope.insertEmpContacts = function (entity) {
    //    return telephoneService.insertList(entity);
    //}
    //$scope.getTeleCatTypes = function () {
    //    telephoneService.getTeleCat().then(function (results) {
    //        return $scope.catList = results.data;
    //    });
    //}
    //$scope.showSelectedTeleCatTypes = function (user) {
    //    var selected = [];
    //    if (user != null && user != undefined) {
    //        if (user.TELE_CAT_ID) {
    //            selected = $filter('filter')($scope.catList, { TELE_CAT_ID: user.TELE_CAT_ID });
    //        }
    //        return selected.length ? selected[0].TELE_CAT_AR_NAME : 'Not set';
    //    }
    //};
    $scope.update = function (entity) {
        return AssetMasterService.update(entity);
    }
    $scope.delete = function (entity) {
        return AssetMasterService.delete(entity);
    }
    $scope.AssetMasterPageLoad = function () {
        $scope.getAllOnLoad();
    }


    //$scope.getAllNationList = function () {
    //    commonService.getAllNations().then(function (response) {
    //        $scope.NationList = response.data;
    //    })
    //}


    //$scope.removeGridItem = function (index) {
    //    $scope.TelephoneList.splice(index, 1);
    //};

    //$scope.hideButton = function (index) {
    //    if ($scope.TelephoneList[index].TELE_NUMBER == null || $scope.TelephoneList[index].TELE_NUMBER == "" || $scope.TelephoneList[index].TELE_CAT_ID == null) {
    //        return true;
    //    } else return false;
    //};

    //$scope.showButton = function (index) {
    //    if ($scope.TelephoneList[index].TELE_NUMBER == null || $scope.TelephoneList[index].TELE_NUMBER == "" || $scope.TelephoneList[index].TELE_CAT_ID == null) {
    //        return false;
    //    } else return true;
    //};

    //$scope.insertItem = function (data, Index) {
    //    if (data != "" && data != null) {
    //        if (data != undefined && data != null) {
    //            if (data.TELE_NUMBER != null && data.TELE_NUMBER != "" && data.TELE_CAT_ID != null) {
    //                if ($scope.TelephoneList[Index] != null && $scope.TelephoneList[Index] != undefined) {
    //                    $scope.removeGridItem(Index);
    //                }
    //                if ($scope.AssetMaster.EMP_ID === null || $scope.EMP_ID === 0)
    //                { $scope.telephoneItem.TELE_ID = null; }
    //                else { $scope.telephoneItem.TELE_ID = $scope.AssetMaster.EMP_ID; }
    //                $scope.telephoneItem.TELE_NUMBER = data.TELE_NUMBER;
    //                $scope.telephoneItem.TELE_CAT_ID = data.TELE_CAT_ID;
    //                $scope.TelephoneList.push($scope.telephoneItem);
    //                $scope.addGridItem();
    //            }
    //        }
    //        else {
    //            $scope.removeGridItem(Index);
    //            $scope.TelephoneList.push(data);
    //            $scope.addGridItem();
    //        }
    //    }
    //}


    //$scope.getAllEmpTelephone = function () {
    //    telephoneService.getBy($scope.AssetMaster.EMP_ID, 3).then(function (response) {
    //        $scope.TelephoneList = response.data;
    //        $scope.addGridItem();
    //    })
    //}

    //$scope.telephoneItem = {};
    //$scope.TelephoneList = [];
    //$scope.addGridItem = function () {
    //    var found = false;
    //    for (var i = 0; i < $scope.TelephoneList.length; i++) {
    //        if ($scope.TelephoneList[i] == null || $scope.TelephoneList[i] == {}) {
    //            found = true;
    //        }
    //    }
    //    if (!found) {
    //        $scope.telephoneItem = {
    //            TELE_ID: null, TELE_NUMBER: null, TELE_TYPE_ID: 3, TELE_CAT_ID: null
    //        };
    //        $scope.TelephoneList.push($scope.telephoneItem);
    //    }
    //};


    //$scope.getAllCompanyBranchesList = function () {
    //    commonService.getAllCompanyBranches().then(function (response) {
    //        $scope.CompanyBranchesList = response.data;
    //    })
    //}

    //$scope.getAllGovernateTown = function () {
    //    $scope.GetTownsByGovernateID();
    //}
    //$scope.getAllTownVillage = function () {
    //    $scope.GetVillageByTownID();
    //}

    //$scope.getAllNastionGovernate = function () {
    //    $scope.GetGovernatesByNationID();
    //}
    //$scope.GetGovernatesByNationID = function () {
    //    commonService.GetGovernatesByNationID($scope.AssetMaster.NATION_ID).then(function (response) {
    //        $scope.GovernateListByNationID = response.data
    //    }, function (error) {
    //        console.log(error.data.message);
    //    });
    //}
    //$scope.GetTownsByGovernateID = function () {
    //    commonService.GetTownsByGovernateID($scope.AssetMaster.GOV_ID).then(function (response) {
    //        $scope.TownsListByGovernateID = response.data
    //    }, function (error) {
    //        console.log(error.data.message);
    //    });
    //}
    //$scope.GetVillageByTownID = function () {
    //    commonService.GetVillageByTownID($scope.AssetMaster.TOWN_ID).then(function (response) {
    //        $scope.VillagesListByTownID = response.data
    //    }, function (error) {
    //        console.log(error.data.message);
    //    });
    //}
    //$scope.GetDepartmentByCompanyBranchID = function () {
    //    commonService.GetDepartmentByCompanyBranchID($scope.AssetMaster.COM_BRN_ID).then(function (response) {
    //        $scope.GetDepartmentByCompanyBranchIDList = response.data
    //    }, function (error) {
    //        console.log(error.data.message);
    //    });
    //}
    //$scope.GetAllDepartments = function () {
    //    departmentsService.getAllDepartments().then(function (response) {
    //        $scope.DepartmentList = response.data;


    //    }, function (error) {
    //        console.log(error.data.message);
    //    });
    //}
    //$scope.GetAllEmployeeTypes = function () {
    //    employeeTypesService.getAll().then(function (response) {
    //        $scope.EmployeeTypesList = response.data
    //    }, function (error) {
    //        console.log(error.data.message);
    //    });
    //}
    //$scope.getAllCustomersAccounts = function () {
    //    commonService.getAllCustomersAccounts().then(function (response) {
    //        $scope.CustomersAccountsList = response.data
    //    }, function (error) {
    //        console.log(error.data.message);
    //    });
    //}

    $scope.pageChanged = function () {
        $scope.getAllOnLoad();
        $log.log('Page changed to: ' + $scope.pageNum);
    };


    ///////////////////////////telephones

    //$scope.getTeleCatTypes = function () {
    //    telephoneService.getTeleCat().then(function (results) {
    //        return $scope.catList = results.data;
    //    });
    //}


    //$scope.addTeleToList = function () {
    //    $scope.typeName = {};
    //    telephoneService.getTypeNameByID($scope.teleCatId).then(function (result) {
    //        $scope.typeName = result.data;
    //        $scope.TelephoneList.push({ TELE_NUMBER: $scope.teleNumber, TELE_CAT_ID: $scope.teleCatId, TeleCatName: $scope.typeName.TELE_CAT_AR_NAME, TELE_TYPE_ID: 3 });
    //        $scope.teleNumber = "";
    //        $scope.teleTypeId = "";
    //    });
    //}



    //$scope.removeTeleItem = function (index) {
    //    $scope.TelephoneList.splice(index, 1);
    //};


    $scope.getlastCode = function () {
        var urlParams = $location.search();
        if (urlParams.foo != null && urlParams.foo != undefined && urlParams.foo != "") { } else {
            AssetMasterService.getLastCode().then(function (result) {
                $scope.AssetMaster.Code = parseInt(result.data) + 1;
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

        //commonService.getAllResources().then(function (result) {

        //    var all_resourese = result.data;

        //    var json_res = JSON.parse(all_resourese);

        //    console.log(json_res["rt"]);

        //    localStorage.setItem("resoursesModule", JSON.stringify(json_res["r"]));

        //    commonService.loadModuleResources($scope, $location);
        //});

        //var pageName = $location.path().replace('/', '');

        //commonService.setLocalStorage(pageName);

        //pageName = "CURRENCIES";
        
        /*commonService.getResourceByName(pageName).then(function (result) {
            console.log(result.data);
            //commonService.setLocalStorage(result.data.ID);

            var module_resourse = result.data;
     
        }, function (error) { });

        /*var queryResult = document.querySelector('.panel-body');
        console.log(queryResult);
        var wrappedQueryResult = angular.element(queryResult);
        console.log(wrappedQueryResult);

        queryResult = document.querySelectorAll('[data-rt="Code"]');
        console.log(queryResult);

        wrappedQueryResult = angular.element(queryResult);
        console.log(wrappedQueryResult[0].dataset.id);


        wrappedQueryResult.text("hhhhhhhhhhhhh");*/

        //var panes = [12, 55, 44, 77, 588];
        //angular.forEach(panes, function (pane,i) {
        //    console.log(i);
        //});

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
