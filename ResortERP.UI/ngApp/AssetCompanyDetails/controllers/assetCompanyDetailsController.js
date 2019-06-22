'use strict';
app.controller('assetCompanyDetailsController', ['$scope', '$location', '$log', '$q', 'assetCompanyDetailsService', 'dateFilter', '$filter', 'commonService', 'currencyService', 'telephoneService', 'salesTypeService', 'companyBranchesService', '$base64', 'accountsService', 'addressService', 'entryService', 'authService', function ($scope, $location, $log, $q, assetCompanyDetailsService, dateFilter, $filter, commonService, currencyService, telephoneService, salesTypeService, companyBranchesService, $base64, accountsService, addressService, entryService, authService) {

    $scope.cancel = function () {
        $uibModalInstance.dismiss();
    };

    $scope.assetCompanyDetails = { ID: 0, Code: null, Active: true };
    $scope.Code = 0;
    $scope.clearEnity = function () {
        $scope.assetCompanyDetails = { ID: 0 };
        //$scope.imageSrc = null;
        //$scope.assetCompanyDetails.Cust_Photo_Base64 = null;
        $scope.telephoneList = [];
        $scope.telephoneItem = {
            TELE_ID: null, TELE_NUMBER: null, TELE_TYPE_ID: 1, TELE_CAT_ID: null
        };
        //$scope.addGridItem();imageSrc

        //$scope.assetCompanyDetailsBranchesList = [];
        //$scope.cBranItem = {
        //    ID: 0, ACC_BRN_AR_NAME: null, ACC_BRN_EN_NAME: null, CompanyNationID: null, CompanyGovernerateID: null, BRN_ADDR_REMARKS: null, BRN_REMARKS: null
        //};
        //$scope.addcBranGridItem();
        $scope.TelephoneList = [];
        $scope.branchList = [];
        $scope.assetCompanyDetails.Account = {};
        //$scope.assetCompanyDetails.Cust_Photo_Base64 = null;
        //$scope.refreshData();

        $scope.getlastCode();
    }

    $scope.assetCompanyDetailsList = [];
    $scope.assetCompanyDetailsTotalCount = 0;
    $scope.pageNum = 1;
    $scope.pageSize = 10;
    $scope.pagesCount = 0;
    $scope.maxSize = 5;
    $scope.accountTypesList = [];
    $scope.catList = [];
    $scope.TelephoneList = [];

    $scope.GovBranList = [];
    $scope.TownBranList = [];
    $scope.VillBranList = [];
    $scope.branchList = [];
    $scope.assetCompanyDetails.Code = null;


    $scope.Type = [{ typeId: 1, typeName: "مصنع" },
    { typeId: 2, typeName: "مورد" }];

    
    $scope.getPagesCount = function () {
        var div = Math.floor($scope.assetCompanyDetailsTotalCount / $scope.pageSize);
        var rem = $scope.assetCompanyDetailsTotalCount % $scope.pageSize;
        if (rem > 0) {
            div = div + 1;
        }
        return div;
    }

    $scope.refreshData = function () {
        $scope.getAllOnLoad();
    }
    $scope.saveassetCompanyDetails = function () {
        $scope.saveEntity();

      
       
    }

    $scope.saveEntity = function () {
        if ($scope.assetCompanyDetails !== undefined && $scope.assetCompanyDetails !== null && $scope.assetCompanyDetails.Code !== null && $scope.assetCompanyDetails.ARName !== null && $scope.assetCompanyDetails.ENName !== null ) {
            //if ($scope.assetCompanyDetails.ID === null || $scope.assetCompanyDetails.ID === 0 || $scope.ID === 0) {

            //    $scope.assetCompanyDetails.Code = $scope.Code;
               

            //} else {

              //  $scope.assetCompanyDetails.Code = $scope.Code;
                //$scope.InsertUpdateassetCompanyDetailsDependence($scope.assetCompanyDetails, $scope.telephoneList, $scope.assetCompanyDetailsBranchesList, 'U').then(function (results) {
                //    if ($scope.assetCompanyDetails.CreditOpeningAccount != undefined || $scope.assetCompanyDetails.DepitOpeningAccount != undefined) {
                //        $scope.saveEntry();
                //    }
                 
            if ($scope.assetCompanyDetails.ID == 0) {
                $scope.saveEntry();
               
            } else {
                var UserId = authService.GetUserID();
                if (UserId != undefined && UserId != "") {
                    $scope.assetCompanyDetails.UpdatedBy = UserId;
                }
                $scope.update($scope.assetCompanyDetails);
                $scope.clearEnity();
            }
                //}, function (error) {
                //    console.log(error.data.message);
                //    swal('عفواً',
                //        'حدث خطأ عند تعديل بيانات ' + $scope.CustSup + '',
                //        'error');
                //});
           // }
        }
    }
    $scope.dirEnity = function (entity) {
        debugger
        $scope.assetCompanyDetails = entity;
        
        //$scope.getAllassetCompanyDetailsTelephone();
        //$scope.getAllassetCompanyDetailsBranches();
    }
    $scope.deleteEnity = function (entity) {
        swal({
            title: ' هل تريد حذف هذه الشركة ؟ ',
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
                        text: "هذه الشركة تمت عليه عمليات مختلفة. لا تستطيع حذفه",
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
     
    }
    $scope.getAllOnLoad = function () {
      
        $q.all(
            [
                //$scope.checkCustSup($scope.type),
                //$scope.getAllSalesTypes(),
                $scope.getassetCompanyDetailsList(),
                $scope.getassetCompanyDetailsTotalCount(),
                //$scope.getAccountDefaultCode(),
                //$scope.getAccountTypes(),
                //$scope.getCurrency(),
                $scope.getAllNationList(),
                $scope.getTeleCatTypes(),
                //$scope.getAllCompanyBranchesList(),
               // $scope.getAccountsType(),
                //$scope.getParentFinalList(),
                $scope.getTeleCatTypes(),
                $scope.getlastCode(),
                //$scope.getEntryNumber()


            ]).then(function (allResponses) {
                $scope.clearEnity();

                var urlParams = $location.search();
                if (urlParams.foo != null && urlParams.foo != undefined && urlParams.foo != "") {

                    assetCompanyDetailsService.getById(parseInt(urlParams.foo)).then(function (results) {
                        var data = results.data;
                        $scope.assetCompanyDetailsList = data;
                        $scope.dirEnity(data);
                    })

                }

            }, function (error) {
                //This will be called if $q.all finds any of the requests erroring.
                var abc = error;
                var def = 99;
            });
    }
    

    $scope.getassetCompanyDetailsTotalCount = function () {

        $scope.count().then(function (results) {
            var data = results.data;
            $scope.assetCompanyDetailsTotalCount = data;
            $scope.pagesCount = $scope.getPagesCount();
        }, function (error) {
            console.log(error.data.message);
        });
    }
    $scope.getassetCompanyDetailsList = function () {

        $scope.get($scope.pageNum, $scope.pageSize).then(function (results) {
            var data = results.data;
            $scope.assetCompanyDetailsList = data;
        }, function (error) {
            console.log(error.data.message);
        });
    }

    $scope.getAllNastionGovernate = function () {
        $scope.GetGovernatesByNationID();
    }
    $scope.getAllNastionGovernatecBran = function (data) {
        $scope.GetGovernatesByNationIDcBran(data);
    }
    $scope.getAllGovernateTown = function () {
        $scope.GetTownsByGovernateID();
    }
    $scope.getAllTownVillage = function () {
        $scope.GetVillageByTownID();
    }
    
    $scope.getAllassetCompanyDetailsTelephone = function () {
        telephoneService.getBy($scope.assetCompanyDetails.ID, 1).then(function (response) {
            //$scope.telephoneList = response.data;
            $scope.TelephoneList = response.data;
            // $scope.addGridItem();
        })
    }

    $scope.telephoneItem = {};
    //$scope.telephoneList = [];
    //$scope.addGridItem = function () {
    //    var found = false;
    //    for (var i = 0; i < $scope.telephoneList.length; i++) {
    //        if ($scope.telephoneList[i] == null || $scope.telephoneList[i] == {}) {
    //            found = true;
    //        }
    //    }
    //    if (!found) {
    //        $scope.telephoneItem = {
    //            TELE_ID: null, TELE_NUMBER: null, TELE_TYPE_ID: 1, TELE_CAT_ID: null
    //        };
    //        $scope.telephoneList.push($scope.telephoneItem);
    //    }
    //};
    $scope.showSelectedTeleCatTypes = function (user) {
        var selected = [];
        if (user != null && user != undefined) {
            if (user.TELE_CAT_ID) {
                selected = $filter('filter')($scope.catList, { TELE_CAT_ID: user.TELE_CAT_ID });
            }
            return selected.length ? selected[0].TELE_CAT_AR_NAME : 'Not set';
        }
    };

    /*************************************************************************************************************/



   
    $scope.getAllassetCompanyDetailsBranches = function () {
        companyBranchesService.getAllByAccID($scope.assetCompanyDetails.ID).then(function (response) {
            // $scope.assetCompanyDetailsBranchesList = response.data;
            // $scope.addcBranGridItem();
            $scope.branchList = response.data;
        })
    }

    $scope.cBranItem = {};
    $scope.assetCompanyDetailsBranchesList = [];
    $scope.addcBranGridItem = function () {
        var found_ = false;
        for (var i = 0; i < $scope.assetCompanyDetailsBranchesList.length; i++) {
            if ($scope.assetCompanyDetailsBranchesList[i] == null || $scope.assetCompanyDetailsBranchesList[i] == {}) {
                found_ = true;
            }
        }
        if (!found_) {
            $scope.cBranItem = {
                ID: null, ACC_BRN_AR_NAME: null, ACC_BRN_EN_NAME: null, CompanyNationID: null, CompanyGovernerateID: null, BRN_ADDR_REMARKS: null, BRN_REMARKS: null
            };
            $scope.assetCompanyDetailsBranchesList.push($scope.cBranItem);
        }
    };
    $scope.showSelectedNation = function (user) {
        var selected = [];
        if (user != null && user != undefined) {
            if (user.CompanyNationID) {
                selected = $filter('filter')($scope.NationList, { CompanyNationID: user.CompanyNationID });
            }
            //$scope.GetGovernatesByNationIDcBran(user.CompanyNationID);
            return selected.length ? selected[0].NATION_AR_NAME : 'Not set';
        }
    };
    $scope.showSelectedcBranGov = function (user) {
        var selected = [];
        if (user != null && user != undefined) {
            if (user.CompanyGovernerateID) {
                selected = $filter('filter')($scope.GovernateListByNationIDcBran, { CompanyGovernerateID: user.CompanyGovernerateID });
            }
            return selected.length ? selected[0].GOV_AR_NAME : 'Not set';
        }
    };

    /*************************************************************************************************************/






    //$scope.getAllCompanyBranchesList = function () {
    //    commonService.getAllCompanyBranches().then(function (response) {
    //        $scope.CompanyBranchesList = response.data;
    //    })
    //}
    $scope.GetDepartmentByCompanyBranchID = function () {
        commonService.GetDepartmentByCompanyBranchID($scope.assetCompanyDetails.Comp_Bran_ID).then(function (response) {
            $scope.GetDepartmentByCompanyBranchIDList = response.data
        }, function (error) {
            console.log(error.data.message);
        });
    }
    $scope.getAllSalesTypes = function () {
        salesTypeService.getAll().then(function (results) {
            return $scope.salesTypeList = results.data;
        });
    }
    $scope.getTeleCatTypes = function () {
        telephoneService.getTeleCat().then(function (results) {
            return $scope.catList = results.data;
        });
    }
    $scope.GetGovernatesByNationID = function () {
        commonService.GetGovernatesByNationID($scope.assetCompanyDetails.CompanyNationID).then(function (response) {
            $scope.GovernateListByNationID = response.data
        }, function (error) {
            console.log(error.data.message);
        });
    }
    $scope.GetGovernatesByNationIDcBran = function (data) {
        commonService.GetGovernatesByNationID(data).then(function (response) {
            $scope.GovernateListByNationIDcBran = response.data
        }, function (error) {
            console.log(error.data.message);
        });
    }
    $scope.GetTownsByGovernateID = function () {
        commonService.GetTownsByGovernateID($scope.assetCompanyDetails.CompanyGovernerateID).then(function (response) {
            $scope.TownsListByGovernateID = response.data
        }, function (error) {
            console.log(error.data.message);
        });
    }
    $scope.GetVillageByTownID = function () {
        commonService.GetVillageByTownID($scope.assetCompanyDetails.CompanyTownID).then(function (response) {
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


    $scope.get = function (pageNum, pageSize) {
        return assetCompanyDetailsService.get(pageNum, pageSize);
        //return assetCompanyDetailsService.getAllassetCompanyDetailsupplier(type);
    }
    $scope.count = function (type) {
        return assetCompanyDetailsService.count(type);
    }
    $scope.InsertUpdateassetCompanyDetailsDependence = function (entity, teleList, customBranList, trans) {
      
        return assetCompanyDetailsService.InsertUpdateassetCompanyDetailsDependence(entity, teleList, customBranList, trans);
    }
    $scope.update = function (entity) {
       
        assetCompanyDetailsService.update(entity).then(function (result) {
            if (result.data == true) {
                var id = result.data;
                $scope.refreshData();
                swal({
                    title: 'تم',
                    text: 'تعديل البيانات بنجاح ',
                    type: 'success',
                    timer: 1500,
                    showConfirmButton: false
                });
            }
        })
    }
    $scope.delete = function (entity) {
        return assetCompanyDetailsService.delete(entity);
    }
    $scope.assetCompanyDetailsPageLoad = function () {
        $scope.getAllOnLoad();
    }

    $scope.pageChanged = function () {
        $scope.getAllOnLoad();
        $log.log('Page changed to: ' + $scope.pageNum);
    };


    //////////////////////////////////////////////////////////////////////////////////////



    $scope.getParentFinalList = function () {
        accountsService.getAllAccounts().then(function (result) {
            $scope.parentList = result.data;
            $scope.finalList = result.data;
        });
    }


  ///////////////////telephones
    $scope.getTeleCatTypes = function () {
        telephoneService.getTeleCat().then(function (results) {
            return $scope.catList = results.data;
        });
    }


    $scope.addTeleToList = function () {
        $scope.typeName = {};
        telephoneService.getTypeNameByID($scope.teleCatId).then(function (result) {
           // alert(result.data);
            $scope.typeName = result.data;
            $scope.TelephoneList.push({ TELE_NUMBER: $scope.teleNumber, TELE_CAT_ID: $scope.teleCatId, TeleCatName: $scope.typeName.TELE_CAT_AR_NAME, TELE_TYPE_ID: 1 });
            $scope.teleNumber = "";
            $scope.teleTypeId = "";
        });
    }



    $scope.removeTeleItem = function (index) {
        $scope.TelephoneList.splice(index, 1);
    };


    //////////////branches 
    $scope.getAllNationGovernateBran = function () {
        $scope.GetGovernatesByNationIDBran();
    }

    $scope.GetGovernatesByNationIDBran = function () {
        commonService.GetGovernatesByNationID($scope.branch.CompanyNationID).then(function (response) {
            $scope.GovBranList = response.data
        }, function (error) {
            console.log(error.data.message);
        });
    }


    $scope.getAllGovernateTownBran = function () {
        $scope.GetTownsByGovernateIDBran();
    }


    $scope.GetTownsByGovernateIDBran = function () {
        commonService.GetTownsByGovernateID($scope.branch.CompanyGovernerateID).then(function (response) {
            $scope.TownBranList = response.data
        }, function (error) {
            console.log(error.data.message);
        });
    }


    $scope.getAllTownVillageBran = function () {
        $scope.GetVillageByTownIDBran();
    }

    $scope.GetVillageByTownIDBran = function () {
        commonService.GetVillageByTownID($scope.branch.CompanyTownID).then(function (response) {
            $scope.VillBranList = response.data
        }, function (error) {
            console.log(error.data.message);
        });
    }


    $scope.addBranToList = function () {

        $scope.nationName = "";
        $scope.govName = "";
        $scope.townName = "";
        $scope.villName = "";
        addressService.getNationByID($scope.branch.CompanyNationID).then(function (result) {
            if (result !== null && result !== undefined) {
                $scope.nationName = result.data.NATION_AR_NAME;
            }
            addressService.getGovernByID($scope.branch.CompanyGovernerateID).then(function (response) {
                if (response.data !== null && response.data !== undefined) {
                    $scope.govName = response.data.GOV_AR_NAME;
                }


                $scope.branchList.push({
                    ACC_BRN_AR_NAME: $scope.branch.ACC_BRN_AR_NAME, ACC_BRN_EN_NAME: $scope.branch.ACC_BRN_EN_NAME, CompanyNationID: $scope.branch.CompanyNationID, CompanyGovernerateID: $scope.branch.CompanyGovernerateID, CompanyTownID: $scope.branch.CompanyTownID,
                    VILLAGE_ID: $scope.branch.VILLAGE_ID, BRN_ADDR_REMARKS: $scope.branch.BRN_ADDR_REMARKS, BRN_REMARKS: $scope.branch.BRN_REMARKS, NationName: $scope.nationName, GovName: $scope.govName
                });
                $scope.branch = {};
            });


        });
    }


    $scope.removeBranItem = function (index) {
        $scope.branchList.splice(index, 1);
    };

    ///////////////////////////////////////////////////////////////////////////////////
    //getDefaultcode



    $scope.getAccountDefaultCode = function () {
        // var count
        accountsService.getCodeWithoutParent().then(function (result) {          
            $scope.ACC_CODE = parseInt(result.data) + 1;
        });
    }


    $scope.getCodeByParentID = function () {
        accountsService.getParentDataByID($scope.assetCompanyDetails.Account.PARENT_ACC_ID).then(function (result) {
            var parent = result.data;
            //var Code = result.data.ACC_CODE;
            //var sons = result.data.ACC_NSONS;
            //$scope.ACC_CODE = Code + sons + '1';
            $scope.ACC_CODE = parent.SonCode;
        });
    }
    //////////////////////////last Code
    $scope.getlastCode = function () {
        var urlParams = $location.search();
        if (urlParams.foo != null && urlParams.foo != undefined && urlParams.foo != "") { } else {
            assetCompanyDetailsService.getLastCode().then(function (result) {
                $scope.assetCompanyDetails.Code = parseInt(result.data) + 1;
            }, function (error) { });
        }
    }

    /////////////////////////////////////////////////////save entry for assetCompanyDetails 
    $scope.saveEntry = function () {
        var UserId = authService.GetUserID();
        if (UserId != undefined && UserId != "") {
            $scope.assetCompanyDetails.AddedBy = UserId;
        }
        assetCompanyDetailsService.insert($scope.assetCompanyDetails).then(function (result) {
            if (result.data == true) {
                var id = result.data;
                $scope.refreshData();
                swal({
                    title: 'تم',
                    text: 'اضافة البيانات بنجاح ',
                    type: 'success',
                    timer: 1500,
                    showConfirmButton: false
                });
            }
        });

    }



}]);