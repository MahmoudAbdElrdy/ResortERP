'use strict';
app.controller('companyStoresController', ['$scope', '$location', '$log', '$q', 'authService', 'companyStoresService', 'commonService', 'employeesService', 'telephoneService', '$window', function ($scope, $location, $log, $q, authService, companyStoresService, commonService, employeesService, telephoneService, $window) {

    $scope.companyStore = { COM_STORE_ID: null, COM_STORE_CODE: "", COM_STORE_AR_NAME: "", COM_STORE_EN_NAME: "", COM_STORE_AR_ABRV: "", COM_STORE_EN_ABRV: "", NATION_ID: null, GOV_ID: null, TOWN_ID: null, VILLAGE_ID: null, COM_STORE_ADDR_REMARKS: "", COM_MASTER_STORE_ID: "", COM_STORE_REMARKS: "", COM_BRN_ID: null, EMP_ID: null, COM_PRINTER_NAME: "", AddedBy: "", AddedOn: null, UpdatedBy: "", updatedOn: null, Disable: false };

    $scope.clearEnity = function () {
        $scope.companyStore = { COM_STORE_ID: null, COM_STORE_CODE: "", COM_STORE_AR_NAME: "", COM_STORE_EN_NAME: "", COM_STORE_AR_ABRV: "", COM_STORE_EN_ABRV: "", NATION_ID: null, GOV_ID: null, TOWN_ID: null, VILLAGE_ID: null, COM_STORE_ADDR_REMARKS: "", COM_MASTER_STORE_ID: "", COM_STORE_REMARKS: "", COM_BRN_ID: null, EMP_ID: null, COM_PRINTER_NAME: "", AddedBy: "", AddedOn: null, UpdatedBy: "", updatedOn: null, Disable: false };
        //$scope.TelephoneList = [
        //    { TELE_NUMBER: "", TELE_CAT_ID: null }
        //];
        //$scope.telephoneItem = {
        //    TELE_ID: null, TELE_NUMBER: null, TELE_TYPE_ID: 3, TELE_CAT_ID: null
        //};
        $scope.TelephoneList = [];
        $scope.telephoneItem = {}
        //  $scope.addGridItem();
        $scope.telephone = {};

        $scope.getLastCode();
    }

    $scope.companyStoresList = [];
    $scope.companyStoresTotalCount = 0;
    $scope.pageNum = 1;
    $scope.pageSize = 10;
    $scope.pagesCount = 0;
    $scope.maxSize = 5;

    $scope.getPagesCount = function () {
        var div = Math.floor($scope.companyStoresTotalCount / $scope.pageSize);
        var rem = $scope.companyStoresTotalCount % $scope.pageSize;
        if (rem > 0) {
            div = div + 1;
        }
        return div;
    }

    $scope.refreshData = function () {
        $scope.getAllOnLoad();
    }
    $scope.saveCompanyStores = function () {

        $scope.saveEntity();
    }
    $scope.saveEntity = function () {
        if ($scope.companyStore !== undefined && $scope.companyStore !== null && $scope.companyStore.COM_STORE_CODE !== "" && $scope.companyStore.COM_STORE_AR_NAME !== "" && $scope.companyStore.COM_STORE_EN_NAME !== "") {
            if ($scope.companyStore.COM_STORE_ID === null || $scope.COM_STORE_ID === 0) {
                $scope.insert($scope.companyStore).then(function (results) {
                    $scope.companyStore.COM_STORE_ID = results.data;
                    for (var i = 0; i < $scope.TelephoneList.length; i++) {
                        $scope.TelephoneList[i].TELE_ID = $scope.companyStore.COM_STORE_ID;
                    }

                    //if ($scope.TelephoneList.length !== 0) {
                    //    var index = $scope.TelephoneList.length - 1;

                       // if ($scope.TelephoneList[index] != null && $scope.TelephoneList[index] != undefined)
                            //$scope.removeGridItem(index);
                   // }

                    $scope.insertComStoreContacts($scope.TelephoneList).then(function () {
                        $scope.clearEnity(); $scope.refreshData();
                        swal({
                            title: 'تم',
                            text: 'حفظ بيانات المخزن بنجاح',
                            type: 'success',
                            timer: 1500,
                            showConfirmButton: false
                        });
                    });
                }, function (error) {
                    console.log(error.data.message);
                    swal('عفواً',
                        'حدث خطأ عند حفظ بيانات المخزن',
                        'error');
                });
            } else {
                $scope.update($scope.companyStore).then(function (results) {
                    var index = $scope.TelephoneList.length - 1;
                    if ($scope.TelephoneList[index] != null && $scope.TelephoneList[index] != undefined && $scope.TelephoneList[index].TELE_NUMBER == null && $scope.TelephoneList[index].TELE_CAT_ID == null)
                        $scope.removeGridItem(index);

                    for (var i = 0; i < $scope.TelephoneList.length; i++) {
                        $scope.TelephoneList[i].TELE_ID = $scope.companyStore.COM_STORE_ID;
                    }
                    $scope.insertComStoreContacts($scope.TelephoneList).then(function () {
                        $scope.clearEnity(); $scope.refreshData();
                        swal({
                            title: 'تم',
                            text: 'تعديل بيانات المخزن بنجاح',
                            type: 'success',
                            timer: 1500,
                            showConfirmButton: false
                        });
                    });
                }, function (error) {
                    console.log(error.data.message);
                    swal('عفواً',
                        'حدث خطأ عند تعديل بيانات المخزن',
                        'error');
                });
            }
        }
    }
    $scope.dirEnity = function (entity) {
        $scope.companyStore = entity;
        $scope.getAllComStoresTelephone();
        $scope.GetGovernatesByNationID();
    }
    $scope.deleteEnity = function (entity) {
        swal({
            title: 'هل تريد حذف المخزن؟',
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
                        text: "هذا المخزن تم عليه عمليات مختلفة. لا تستطيع حذفه",
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
        //    title: 'هل تريد حذف المخزن؟',
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
                $scope.getCompanyStoresList(),
                $scope.getCompanyStoresTotalCount(),
                $scope.getAllNationList(),
                $scope.getAllCompanyBranchesList(),
                $scope.getAllEmployeesList(),
                //  $scope.getAllComStoresTelephone(),
                $scope.getTeleCatTypes(),
                $scope.getLastCode()


            ]).then(function (allResponses) {
                $scope.clearEnity();


                var urlParams = $location.search();
                if (urlParams.foo != null && urlParams.foo != undefined && urlParams.foo != "") {

                    companyStoresService.getById(parseInt(urlParams.foo)).then(function (results) {
                        $scope.companyStore = results.data;
                        $scope.dirEnity(results.data);
                    })
                    //$.each($scope.companyStoresList, function (index, value) {
                    //    if (value.COM_STORE_ID == parseInt(urlParams.foo)) {
                    //        $scope.companyStore = value;
                    //        $scope.dirEnity(value);
                    //    }
                    //});

                }

                

            }, function (error) {
                //This will be called if $q.all finds any of the requests erroring.
                var abc = error;
                var def = 99;
            });
    }
    $scope.getCompanyStoresTotalCount = function () {
        $scope.count().then(function (results) {
            var data = results.data;
            $scope.companyStoresTotalCount = data;
            $scope.pagesCount = $scope.getPagesCount();
        }, function (error) {

            console.log(error.data.message);
        });
    }
    $scope.getCompanyStoresList = function () {

        $scope.get($scope.pageNum, $scope.pageSize).then(function (results) {
            var data = results.data;
            $scope.companyStoresList = data;
            var urlParams = $location.search();
            if (urlParams.new != null && urlParams.new != undefined && urlParams.new != "") {
                // $scope.account.PARENT_ACC_ID = parseInt(urlParams.new);

                companyStoresService.getById(urlParams.new).then(function (result) {
                    $scope.companyStore.COM_MASTER_STORE_ID = result.data.COM_STORE_ID;
                });
            }
        }, function (error) {

            console.log(error.data.message);
        });
    }




    $scope.getAllNastionGovernate = function () {
        $scope.GetGovernatesByNationID();
    }
    $scope.getAllGovernateTown = function () {
        $scope.GetTownsByGovernateID();
    }
    $scope.getAllTownVillage = function () {
        $scope.GetVillageByTownID();
    }



    $scope.GetGovernatesByNationID = function () {
        commonService.GetGovernatesByNationID($scope.companyStore.NATION_ID).then(function (response) {
            $scope.GovernateListByNationID = response.data
        }, function (error) {
            console.log(error.data.message);
        });
    }
    $scope.GetTownsByGovernateID = function () {
        commonService.GetTownsByGovernateID($scope.companyStore.GOV_ID).then(function (response) {
            $scope.TownsListByGovernateID = response.data
        }, function (error) {
            console.log(error.data.message);
        });
    }
    $scope.GetVillageByTownID = function () {
        commonService.GetVillageByTownID($scope.companyStore.TOWN_ID).then(function (response) {
            $scope.VillagesListByTownID = response.data
        }, function (error) {
            console.log(error.data.message);
        });
    }


    $scope.getAllNationList = function () {

        commonService.getAllNations().then(function (response) {
            $scope.NationList = response.data;
        })
    }
    $scope.getAllCompanyBranchesList = function () {

        commonService.getAllCompanyBranches().then(function (response) {
            $scope.CompanyBranchesList = response.data;
        })
    }
    $scope.getAllEmployeesList = function () {

        employeesService.getByTypeID(5).then(function (response) {
            $scope.EmployeesList = response.data;
        })
    }
    $scope.getAllGovernateList = function () {

        commonService.getAllGovernate().then(function (response) {
            $scope.GovernateList = response.data;
        })
    }
    $scope.getAllTownsList = function () {
        commonService.getAllTowns().then(function (response) {
            $scope.TownsList = response.data;
        })
    }
    $scope.getAllVillagesList = function () {

        commonService.getAllVillages().then(function (response) {
            $scope.VillagesList = response.data;
        })
    }


    $scope.GetCompanyBranchNameByID = function (branID) {
        commonService.GetCompanyBranchNameByID(branID).then(function (response) {
            return response.data;
        }, function (error) {
            console.log(error.data.message);
        });
    }
    $scope.GetEmployeeNameByID = function (empID) {
        commonService.GetEmployeeNameByID(empID).then(function (response) {
            return response.data;
        }, function (error) {
            console.log(error.data.message);
        });
    }

    $scope.getTeleCatTypes = function () {
        telephoneService.getTeleCat().then(function (results) {
            return $scope.catList = results.data;
        });
    }

    $scope.get = function (pageNum, pageSize) {
        return companyStoresService.get(pageNum, pageSize);
    }
    $scope.count = function () {
        return companyStoresService.count();
    }
    $scope.insert = function (entity) {
        return companyStoresService.insert(entity);
    }
    $scope.update = function (entity) {
        return companyStoresService.update(entity);
    }
    $scope.delete = function (entity) {
        return companyStoresService.delete(entity);
    }
    $scope.companyStoresPageLoad = function () {
        $scope.getAllOnLoad();
    }
    $scope.insertComStoreContacts = function (entity) {
        return telephoneService.insertList(entity);
    }
    $scope.pageChanged = function () {
        $scope.getAllOnLoad();
        $log.log('Page changed to: ' + $scope.pageNum);
    };

    /********************************************************************************************************/
    /*Telephone List*/
    /********************************************************************************************************/


    //$scope.hideButton = function (index) {
    //    if ($scope.TelephoneList[index].TELE_NUMBER == null || $scope.TelephoneList[index].TELE_NUMBER == "" || $scope.TelephoneList[index].TELE_CAT_ID == null) {
    //        return true;
    //    } else return false;
    //};

    ////$scope.showButton = function (index) {
    ////    if ($scope.TelephoneList[index].TELE_NUMBER == null || $scope.TelephoneList[index].TELE_NUMBER == "" || $scope.TelephoneList[index].TELE_CAT_ID == null) {
    ////        return false;
    ////    } else return true;
    ////};

    //$scope.insertItem = function (data, Index) {
    //    if (data != "" && data != null) {
    //        if (data != undefined && data != null) {
    //            // if (data.TELE_NUMBER != null && data.TELE_NUMBER != "" && data.TELE_CAT_ID != null)
    //            if (data.TELE_NUMBER != null && data.TELE_NUMBER != "") {
    //                if ($scope.TelephoneList[Index] != null && $scope.TelephoneList[Index] != undefined) {
    //                    $scope.removeGridItem(Index);
    //                }
    //                if ($scope.companyStore.COM_STORE_ID === null || $scope.COM_STORE_ID === 0)
    //                { $scope.telephoneItem.TELE_ID = null; }
    //                else { $scope.telephoneItem.TELE_ID = $scope.companyStore.COM_STORE_ID; }
    //                $scope.telephoneItem.TELE_NUMBER = data.TELE_NUMBER;
    //                $scope.telephoneItem.TELE_CAT_ID = data.TELE_CAT_ID;
    //                $scope.TelephoneList.push($scope.telephoneItem);
    //                $scope.addGridItem();

    //                //$scope.hideButton(Index);
    //            }
    //        }
    //        else {
    //            $scope.removeGridItem(Index);
    //            $scope.TelephoneList.push(data);
    //            $scope.addGridItem();
    //        }
    //    }
    //}


    //$scope.loadTele = function () {

    //}


    //$scope.hideButton = function (index) {
    //    if ($scope.TelephoneList[index].TELE_CAT_ID == null) {
    //        return true;
    //    } else return false;
    //};


    //$scope.editGridItem = function (data, index) {
    //    if (data != "" && data != null) {
    //        if (data != undefined && data != null) {
    //            // if (data.TELE_NUMBER != null && data.TELE_NUMBER != "" && data.TELE_CAT_ID != null)
    //            if (data.TELE_NUMBER != null && data.TELE_NUMBER != "") {
    //                //if ($scope.TelephoneList[Index] != null && $scope.TelephoneList[Index] != undefined) {
    //                //    $scope.removeGridItem(Index);
    //                //}
    //                //if ($scope.companyStore.COM_STORE_ID === null || $scope.COM_STORE_ID === 0)
    //                //{ $scope.telephoneItem.TELE_ID = null; }
    //                //else { $scope.telephoneItem.TELE_ID = $scope.companyStore.COM_STORE_ID; }

    //                $scope.telephoneItem.TELE_NUMBER = data.TELE_NUMBER;
    //                $scope.telephoneItem.TELE_CAT_ID = data.TELE_CAT_ID;

    //                //$scope.TelephoneList[index].TELE_NUMBER = data.TELE_NUMBER;
    //                //$scope.TelephoneList[index].TELE_CAT_ID = data.TELE_CAT_ID;
    //                //$scope.TelephoneList.push($scope.telephoneItem);
    //                // arr.splice(2, 0, "Lene");
    //                $scope.TelephoneList.splice(index, 0, $scope.telephoneItem);


    //                //$scope.TelephoneList.push($scope.telephoneItem);
    //               // $scope.addGridItem();
    //            }
    //        }
    //        else {
    //           // $scope.removeGridItem(Index);
    //            //$scope.TelephoneList.push(data);
    //            $scope.TelephoneList.splice(index, 0, $scope.telephoneItem);
    //            //$scope.addGridItem();
    //        }
    //    }
    //}








    //$scope.editingData = {};

    //for (var i = 0, length = $scope.TelephoneList.length; i < length; i++) {
    //    $scope.editingData[$scope.TelephoneList[i].id] = false;
    //}

    //$scope.modify = function (tableData) {
    //    $scope.editingData[TelephoneList.id] = true;
    //};


    //$scope.update = function (tableData) {
    //    $scope.editingData[TelephoneList.id] = false;
    //};








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
    //            TELE_ID: null, TELE_NUMBER: null, TELE_TYPE_ID: 2, TELE_CAT_ID: null
    //        };
    //        $scope.TelephoneList.push($scope.telephoneItem);
    //    }
    //};



    $scope.getLastCode = function () {
        var urlParams = $location.search();
        if (urlParams.foo != null && urlParams.foo != undefined && urlParams.foo != "") { } else {
            companyStoresService.getLastCode().then(function (result) {
                $scope.companyStore.COM_STORE_CODE = parseInt(result.data) + 1;
            });
        }
    }



    ////////////////////////////////////////telephone
    $scope.addTeleToList = function () {
        $scope.typeName = {};
        // $scope.getTypeName().then(function (result) {
        telephoneService.getTypeNameByID($scope.teleCatId).then(function (result) {
            
            $scope.typeName = result.data;
            $scope.TelephoneList.push({ TELE_NUMBER: $scope.teleNumber, TELE_CAT_ID: $scope.teleCatId, TeleCatName: $scope.typeName.TELE_CAT_AR_NAME, TELE_TYPE_ID:2});
            $scope.teleNumber = "";
            $scope.teleTypeId = "";
        });

        //$scope.TelephoneList.push({ teleNumber: $scope.teleNumber, teleTypeId: $scope.teleTypeId, teleTypeName: $scope.typeName.TELE_TYPE_AR_NAME });
        //$scope.telephone = {};
        // });
    }



    $scope.removeGridItem = function (index) {
        $scope.TelephoneList.splice(index, 1);
    };



    //$scope.getTypeName = function () {
    //    telephoneService.getTypeNameByID($scope.teleTypeId).then(function (result) {
    //        //alert(result.data);
    //        $scope.typeName = result.data;
    //        return result;
    //    });
    //}



    $scope.getAllComStoresTelephone = function () {
        //$window.alert($scope.companyStore.COM_STORE_ID);
        if ($scope.companyStore.COM_STORE_ID != null) {
            telephoneService.getBy($scope.companyStore.COM_STORE_ID, 2).then(function (response) {
                //$window.alert(response);
                //$window.alert(response.data);
                $scope.TelephoneList = response.data;
                // $scope.addGridItem();
            })
        }
    }
}]);