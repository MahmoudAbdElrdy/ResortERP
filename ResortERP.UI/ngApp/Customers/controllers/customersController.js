'use strict';
app.controller('customerController', ['$scope', '$location', '$log', '$q', 'customersService', 'dateFilter', '$filter', 'commonService', 'currencyService', 'telephoneService', 'salesTypeService', 'companyBranchesService', '$base64', 'accountsService', 'addressService', 'entryService', function ($scope, $location, $log, $q, customersService, dateFilter, $filter, commonService, currencyService, telephoneService, salesTypeService, companyBranchesService, $base64, accountsService, addressService, entryService) {


    $scope.cancel = function () {
        $uibModalInstance.dismiss();
    };

    $scope.customer = { ACC_ID: 0, Account: { ACC_CODE: null } };
    $scope.customer.Account = { ACC_CODE: null };
    $scope.ACC_CODE = 0;
    $scope.clearEnity = function () {
        $scope.customer = { ACC_ID: 0 };
        $scope.imageSrc = null;
        $scope.customer.Cust_Photo_Base64 = null;
        $scope.telephoneList = [];
        $scope.telephoneItem = {
            TELE_ID: null, TELE_NUMBER: null, TELE_TYPE_ID: 1, TELE_CAT_ID: null
        };
        //$scope.addGridItem();imageSrc

        $scope.customerBranchesList = [];
        $scope.cBranItem = {
            ACC_ID: 0, ACC_BRN_AR_NAME: null, ACC_BRN_EN_NAME: null, NATION_ID: null, GOV_ID: null, BRN_ADDR_REMARKS: null, BRN_REMARKS: null
        };
        //$scope.addcBranGridItem();
        $scope.TelephoneList = [];
        $scope.branchList = [];
        $scope.customer.Account = {};
        //$scope.customer.Cust_Photo_Base64 = null;
        //$scope.refreshData();

        $scope.getlastCode();
    }

    $scope.customerList = [];
    $scope.customerTotalCount = 0;
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
    $scope.customer.Account.ACC_CODE = null;


    $scope.wariningList = [{ warningId: 1, warningName: "دائن" },
    { warningId: 2, warningName: "مدين" },
    { warningId: 3, warningName: "بدون" },
    { warningId: 4, warningName: "كلاهما" }
    ];



    $scope.stateList = [{ stateId: 1, stateName: "رئيسي" },
    { stateId: 2, stateName: "فرعي" }];

    // $scope.imageSrc = {};

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

    //$scope.getFile = function () {
    //    $scope.progress = 0;
    //    fileReader.readAsDataUrl($scope.file, $scope)
    //                  .then(function (result) {
    //                      $scope.imageSrc = result;
    //                  });
    //};

    //$scope.$on("fileProgress", function (e, progress) {
    //    $scope.progress = progress.loaded / progress.total;
    //});

    //$scope.$watch('Date', function (newValue) {
    //    $scope.customer.ACC_DATE = $filter('date')(newValue, 'dd/MM/yyyy');
    //});

    //$scope.$watch('customer.ACC_DATE', function (newValue) {
    //    $scope.Date = $filter('date')(newValue, 'dd/MM/yyyy');
    //});
    //$scope.Date = $filter('date')($scope.customer.Account.ACC_DATE, 'dd/MM/yyyy');
    $scope.getPrice = function () {
        currencyService.getBy($scope.customer.Account.CURRENCY_ID).then(function (response) {
            $scope.CURRENCY_RATE = response.data;
        });
    }

    $scope.getPagesCount = function () {
        var div = Math.floor($scope.customerTotalCount / $scope.pageSize);
        var rem = $scope.customerTotalCount % $scope.pageSize;
        if (rem > 0) {
            div = div + 1;
        }
        return div;
    }

    $scope.refreshData = function () {
        $scope.getAllOnLoad();
    }
    $scope.savecustomer = function () {
       
        $scope.saveEntity();
    }

    $scope.saveEntity = function () {
        if ($scope.customer !== undefined && $scope.customer !== null && $scope.customer.CUST_CODE !== null && $scope.customer.CUST_AR_NAME !== null && $scope.customer.CUST_EN_NAME !== null && $scope.customer.SELL_TYPE_ID !== null) {
            if ($scope.customer.ACC_ID === null || $scope.customer.ACC_ID === 0 || $scope.ACC_ID === 0) {
                //$scope.removeGridItem($scope.telephoneList.length - 1);
                // $scope.removecBranGridItem($scope.customerBranchesList.length - 1);
                if ($scope.imageSrc != null && $scope.imageSrc != undefined && $scope.imageSrc != {})
                    $scope.customer.Cust_Photo_Base64 = $scope.imageSrc;
                //debugger;
                $scope.customer.Account.ACC_CODE = $scope.ACC_CODE;
                if ($scope.customer.Account.ACC_CODE != null && $scope.customer.Account.ACC_CODE != undefined) {
                    accountsService.getAccountByCode($scope.customer.Account.ACC_CODE).then(function (response) {
                        if (response.data == true) {
                            swal({
                                // title: 'تم',
                                text: 'كود ' + $scope.CustSup + ' مسجل سابقا',
                                type: 'success',
                                timer: 1500,
                                showConfirmButton: false
                            });
                        }
                        else {

                            $scope.InsertUpdateCustomerDependence($scope.customer, $scope.TelephoneList, $scope.branchList, 'I').then(function (results) {
                                $scope.customerAccID = results.data;
                                if ($scope.customer.CreditOpeningAccount != undefined || $scope.customer.DepitOpeningAccount != undefined) {
                                    $scope.saveEntry();
                                }
                                $scope.clearEnity();
                                $scope.refreshData();
                                swal({
                                    title: 'تم',
                                    text: 'حفظ بيانات ' + $scope.CustSup + ' بنجاح',
                                    type: 'success',
                                    timer: 1500,
                                    showConfirmButton: false
                                });
                            }, function (error) {
                                console.log(error.data.message);
                                swal('عفواً',
                                    'حدث خطأ عند حفظ بيانات ' + $scope.CustSup + '',
                                    'error');

                            });
                        }

                    });
                }

            } else {
                //$scope.removeGridItem($scope.telephoneList.length - 1);
                //$scope.removecBranGridItem($scope.customerBranchesList.length - 1);
                if ($scope.imageSrc != null && $scope.imageSrc != undefined)
                    $scope.customer.Cust_Photo_Base64 = $scope.imageSrc;
                $scope.customer.Account.ACC_CODE = $scope.ACC_CODE;
                $scope.InsertUpdateCustomerDependence($scope.customer, $scope.telephoneList, $scope.customerBranchesList, 'U').then(function (results) {
                    if ($scope.customer.CreditOpeningAccount != undefined || $scope.customer.DepitOpeningAccount != undefined) {
                        $scope.saveEntry();
                    }
                    $scope.clearEnity();
                    $scope.refreshData();
                    swal({
                        title: 'تم',
                        text: 'تعديل بيانات ' + $scope.CustSup + ' بنجاح',
                        type: 'success',
                        timer: 1500,
                        showConfirmButton: false
                    });
                }, function (error) {
                    console.log(error.data.message);
                    swal('عفواً',
                        'حدث خطأ عند تعديل بيانات ' + $scope.CustSup + '',
                        'error');
                });
            }
        }
    }
    $scope.dirEnity = function (entity) {

        entity.Account.ACC_DATE = new Date(entity.Account.ACC_DATE);

        $scope.customer = entity;
        $scope.imageSrc = "data:image/png;base64," + entity.CUST_PHOTO;
        $scope.getAllCustomerTelephone();
        $scope.getAllCustomerBranches();

        entryService.getEntryByCustID(entity.ACC_ID).then(function (result) {
            if (result.data != undefined && result.data != null) {
                $scope.custEntryMaster = result.data.ENTRY_ID;
            }
        });

    }
    $scope.deleteEnity = function (entity) {
        swal({
            title: 'هل تريد حذف ' + $scope.CustSup + '؟',
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
                        text: "هذا العميل تمت عليه عمليات مختلفة. لا تستطيع حذفه",
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
        //    title: 'هل تريد حذف ' + $scope.CustSup + '؟',
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
        if ($location.search().type != undefined) {
            $scope.type = $location.search().type;
        }
        else {
            $scope.type = $scope.customerType;
        }
        
        $q.all(
            [
                $scope.checkCustSup($scope.type),
                $scope.getAllSalesTypes(),
                $scope.getcustomerList(),
                $scope.getcustomerTotalCount(),
                $scope.getAccountDefaultCode(),
                $scope.getAccountTypes(),
                $scope.getCurrency(),
                $scope.getAllNationList(),
                $scope.getTeleCatTypes(),
                $scope.getAllCompanyBranchesList(),
                $scope.getAccountsType(),
                $scope.getParentFinalList(),
                $scope.getTeleCatTypes(),
                $scope.getlastCode(),
                $scope.getEntryNumber()


            ]).then(function (allResponses) {
                $scope.clearEnity();

                var urlParams = $location.search();
                if (urlParams.foo != null && urlParams.foo != undefined && urlParams.foo != "") {
                    if ($location.search().type != undefined) {
                        $scope.type = $location.search().type;
                    }
                    else {
                        $scope.type = $scope.customerType;
                    }
                    customersService.getByIdLog(parseInt(urlParams.foo), $scope.type).then(function (results) {
                        var data = results.data.Result;
                        $scope.customerList = data;
                        $scope.dirEnity(data);
                    })

                }

            }, function (error) {
                //This will be called if $q.all finds any of the requests erroring.
                var abc = error;
                var def = 99;
            });
    }
    $scope.checkCustSup = function (type) {
        if (type == 'c') {
            $scope.CustSup = "العميل";
            $scope.CustSupM = "العملاء";
        } else if (type == 's') {
            $scope.CustSup = "المورد";
            $scope.CustSupM = "الموردين";
        }
        else if (type == 'w') {
            $scope.CustSup = "الورشة";
            $scope.CustSupM = "الورش";
        }
    }

    $scope.getcustomerTotalCount = function () {

        if ($location.search().type != undefined) {
            $scope.type = $location.search().type;
        }
        else {
            $scope.type= $scope.customerType; 
        }


        $scope.count($scope.type).then(function (results) {
            var data = results.data;
            $scope.customerTotalCount = data;
            $scope.pagesCount = $scope.getPagesCount();
        }, function (error) {
            console.log(error.data.message);
        });
    }
    $scope.getcustomerList = function () {

        if ($location.search().type != undefined) {
            $scope.type = $location.search().type;
        }
        else {
            $scope.type = $scope.customerType;
        }

        $scope.get($scope.pageNum, $scope.pageSize, $scope.type).then(function (results) {
            var data = results.data;
            $scope.customerList = data;
        }, function (error) {
            console.log(error.data.message);
        });
    }

    $scope.getAccountTypes = function () {

        if ($location.search().type != undefined) {
            $scope.type = $location.search().type;
        }
        else {
            $scope.type = $scope.customerType;
        }

        $scope.getCustomerSupplier($scope.type).then(function (response) {
            $scope.customerSuplierList = response.data;
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
    /*************************************************************************************************************/
    //$scope.removeGridItem = function (index) {
    //    $scope.telephoneList.splice(index, 1);
    //};

    //$scope.hideButton = function (index) {
    //    if ($scope.telephoneList[index].TELE_NUMBER == null || $scope.telephoneList[index].TELE_NUMBER == "" || $scope.telephoneList[index].TELE_CAT_ID == null) {
    //        return true;
    //    } else return false;
    //};

    //$scope.showButton = function (index) {
    //    if ($scope.telephoneList[index].TELE_NUMBER == null || $scope.telephoneList[index].TELE_NUMBER == "" || $scope.telephoneList[index].TELE_CAT_ID == null) {
    //        return false;
    //    } else return true;
    //};

    //$scope.insertItem = function (data, Index) {
    //    if (data != "" && data != null) {
    //        if (data != undefined && data != null) {
    //            if (data.TELE_NUMBER != null && data.TELE_NUMBER != "" && data.TELE_CAT_ID != null) {
    //                if ($scope.telephoneList[Index] != null && $scope.telephoneList[Index] != undefined) {
    //                    $scope.removeGridItem(Index);
    //                }
    //                if ($scope.customer.ACC_ID === null || $scope.ACC_ID === 0)
    //                { $scope.telephoneItem.TELE_ID = null; }
    //                else { $scope.telephoneItem.TELE_ID = $scope.customer.ACC_ID; }
    //                $scope.telephoneItem.TELE_NUMBER = data.TELE_NUMBER;
    //                $scope.telephoneItem.TELE_CAT_ID = data.TELE_CAT_ID;
    //                $scope.telephoneList.push($scope.telephoneItem);
    //                $scope.addGridItem();
    //            }
    //        }
    //        else {
    //            $scope.removeGridItem(Index);
    //            $scope.telephoneList.push(data);
    //            $scope.addGridItem();
    //        }
    //    }
    //}
    $scope.getAllCustomerTelephone = function () {
        telephoneService.getBy($scope.customer.ACC_ID, 1).then(function (response) {
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



    /*************************************************************************************************************/
    //$scope.removecBranGridItem = function (index) {
    //    $scope.customerBranchesList.splice(index, 1);
    //};

    //$scope.hidecBranButton = function (index) {
    //    if ($scope.customerBranchesList[index].ACC_BRN_AR_NAME == null || $scope.customerBranchesList[index].ACC_BRN_AR_NAME == "" || $scope.customerBranchesList[index].ACC_BRN_EN_NAME == null || $scope.customerBranchesList[index].ACC_BRN_EN_NAME == "" || $scope.customerBranchesList[index].NATION_ID == null || $scope.customerBranchesList[index].NATION_ID == "" || $scope.customerBranchesList[index].GOV_ID == null || $scope.customerBranchesList[index].GOV_ID == "") {
    //        return true;
    //    } else return false;
    //};

    //$scope.showcBranButton = function (index) {
    //    if ($scope.customerBranchesList[index].ACC_BRN_AR_NAME == null || $scope.customerBranchesList[index].ACC_BRN_AR_NAME == "" || $scope.customerBranchesList[index].ACC_BRN_EN_NAME == null || $scope.customerBranchesList[index].ACC_BRN_EN_NAME == "" || $scope.customerBranchesList[index].NATION_ID == null || $scope.customerBranchesList[index].NATION_ID == "" || $scope.customerBranchesList[index].GOV_ID == null || $scope.customerBranchesList[index].GOV_ID == "") {
    //        return false;
    //    } else return true;
    //};

    //$scope.insertcBranItem = function (data, Index) {
    //    if (data != "" && data != null) {
    //        if (data != undefined && data != null) {
    //            if (data.ACC_BRN_AR_NAME != null && data.ACC_BRN_AR_NAME != "" && data.ACC_BRN_EN_NAME != null && data.ACC_BRN_EN_NAME != "" && data.NATION_ID != null && data.NATION_ID != "" && data.GOV_ID != null && data.GOV_ID != "") {
    //                if ($scope.customerBranchesList[Index] != null && $scope.customerBranchesList[Index] != undefined) {
    //                    $scope.removecBranGridItem(Index);
    //                }
    //                if ($scope.customer.ACC_ID === null || $scope.customer.ACC_ID === 0 || $scope.ACC_ID === 0)
    //                { $scope.cBranItem.ACC_ID = 0; }
    //                else { $scope.cBranItem.ACC_ID = $scope.customer.ACC_ID; }
    //                $scope.cBranItem.ACC_BRN_AR_NAME = data.ACC_BRN_AR_NAME;
    //                $scope.cBranItem.ACC_BRN_EN_NAME = data.ACC_BRN_EN_NAME;
    //                $scope.cBranItem.NATION_ID = data.NATION_ID;
    //                $scope.cBranItem.GOV_ID = data.GOV_ID;
    //                $scope.cBranItem.BRN_ADDR_REMARKS = data.BRN_ADDR_REMARKS;
    //                $scope.cBranItem.BRN_REMARKS = data.BRN_REMARKS;
    //                $scope.customerBranchesList.push($scope.cBranItem);
    //                $scope.addcBranGridItem();
    //            }
    //        }
    //        else {
    //            $scope.removecBranGridItem(Index);
    //            $scope.customerBranchesList.push(data);
    //            $scope.addcBranGridItem();
    //        }
    //    }
    //}
    $scope.getAllCustomerBranches = function () {
        companyBranchesService.getAllByAccID($scope.customer.ACC_ID).then(function (response) {
            // $scope.customerBranchesList = response.data;
            // $scope.addcBranGridItem();
            $scope.branchList = response.data;
        })
    }

    $scope.cBranItem = {};
    $scope.customerBranchesList = [];
    $scope.addcBranGridItem = function () {
        var found_ = false;
        for (var i = 0; i < $scope.customerBranchesList.length; i++) {
            if ($scope.customerBranchesList[i] == null || $scope.customerBranchesList[i] == {}) {
                found_ = true;
            }
        }
        if (!found_) {
            $scope.cBranItem = {
                ACC_ID: null, ACC_BRN_AR_NAME: null, ACC_BRN_EN_NAME: null, NATION_ID: null, GOV_ID: null, BRN_ADDR_REMARKS: null, BRN_REMARKS: null
            };
            $scope.customerBranchesList.push($scope.cBranItem);
        }
    };
    $scope.showSelectedNation = function (user) {
        var selected = [];
        if (user != null && user != undefined) {
            if (user.NATION_ID) {
                selected = $filter('filter')($scope.NationList, { NATION_ID: user.NATION_ID });
            }
            //$scope.GetGovernatesByNationIDcBran(user.NATION_ID);
            return selected.length ? selected[0].NATION_AR_NAME : 'Not set';
        }
    };
    $scope.showSelectedcBranGov = function (user) {
        var selected = [];
        if (user != null && user != undefined) {
            if (user.GOV_ID) {
                selected = $filter('filter')($scope.GovernateListByNationIDcBran, { GOV_ID: user.GOV_ID });
            }
            return selected.length ? selected[0].GOV_AR_NAME : 'Not set';
        }
    };

    /*************************************************************************************************************/






    $scope.getAllCompanyBranchesList = function () {
        commonService.getAllCompanyBranches().then(function (response) {
            $scope.CompanyBranchesList = response.data;
        })
    }
    $scope.GetDepartmentByCompanyBranchID = function () {
        commonService.GetDepartmentByCompanyBranchID($scope.customer.Comp_Bran_ID).then(function (response) {
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
        commonService.GetGovernatesByNationID($scope.customer.NATION_ID).then(function (response) {
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
        commonService.GetTownsByGovernateID($scope.customer.GOV_ID).then(function (response) {
            $scope.TownsListByGovernateID = response.data
        }, function (error) {
            console.log(error.data.message);
        });
    }
    $scope.GetVillageByTownID = function () {
        commonService.GetVillageByTownID($scope.customer.TOWN_ID).then(function (response) {
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
    $scope.getCurrency = function () {
        currencyService.getAll().then(function (response) {
            $scope.currencyList = response.data;
        }, function (error) {
            console.log(error.data.message);
        });
    }
    $scope.getCustomerSupplier = function (type) {
        return commonService.getCustomerSupplier(type);
    }
    $scope.get = function (pageNum, pageSize, type) {
        return customersService.getCustomerSupplier(pageNum, pageSize, type);
        //return customersService.getAllCustomerSupplier(type);
    }
    $scope.count = function (type) {
        return customersService.count(type);
    }
    $scope.InsertUpdateCustomerDependence = function (entity, teleList, customBranList, trans) {
      
        return customersService.InsertUpdateCustomerDependence(entity, teleList, customBranList, trans);
    }
    $scope.update = function (entity) {
        return customersService.update(entity);
    }
    $scope.delete = function (entity) {
        return customersService.delete(entity);
    }
    $scope.customerPageLoad = function () {
        $scope.getAllOnLoad();
    }

    $scope.pageChanged = function () {
        $scope.getAllOnLoad();
        $log.log('Page changed to: ' + $scope.pageNum);
    };


    //////////////////////////////////////////////////////////////////////////////////////
    $scope.getAccountsType = function () {
        // accountsService.getAccountTypes().then(function (result) {

        if ($location.search().type != undefined) {
            $scope.type = $location.search().type;
        }
        else {
            $scope.type = $scope.customerType;
        }

        accountsService.getTypesByQuerystring($scope.type).then(function (result) {
            $scope.accountTypesList = result.data;
        });
    }


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
        commonService.GetGovernatesByNationID($scope.branch.NATION_ID).then(function (response) {
            $scope.GovBranList = response.data
        }, function (error) {
            console.log(error.data.message);
        });
    }


    $scope.getAllGovernateTownBran = function () {
        $scope.GetTownsByGovernateIDBran();
    }


    $scope.GetTownsByGovernateIDBran = function () {
        commonService.GetTownsByGovernateID($scope.branch.GOV_ID).then(function (response) {
            $scope.TownBranList = response.data
        }, function (error) {
            console.log(error.data.message);
        });
    }


    $scope.getAllTownVillageBran = function () {
        $scope.GetVillageByTownIDBran();
    }

    $scope.GetVillageByTownIDBran = function () {
        commonService.GetVillageByTownID($scope.branch.TOWN_ID).then(function (response) {
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
        addressService.getNationByID($scope.branch.NATION_ID).then(function (result) {
            if (result !== null && result !== undefined) {
                $scope.nationName = result.data.NATION_AR_NAME;
            }
            addressService.getGovernByID($scope.branch.GOV_ID).then(function (response) {
                if (response.data !== null && response.data !== undefined) {
                    $scope.govName = response.data.GOV_AR_NAME;
                }


                $scope.branchList.push({
                    ACC_BRN_AR_NAME: $scope.branch.ACC_BRN_AR_NAME, ACC_BRN_EN_NAME: $scope.branch.ACC_BRN_EN_NAME, NATION_ID: $scope.branch.NATION_ID, GOV_ID: $scope.branch.GOV_ID, TOWN_ID: $scope.branch.TOWN_ID,
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
        accountsService.getParentDataByID($scope.customer.Account.PARENT_ACC_ID).then(function (result) {
            var parent = result.data;
            //var code = result.data.ACC_CODE;
            //var sons = result.data.ACC_NSONS;
            //$scope.ACC_CODE = code + sons + '1';
            $scope.ACC_CODE = parent.SonCode;
        });
    }
    //////////////////////////last code
    $scope.getlastCode = function () {
        var urlParams = $location.search();
        if (urlParams.foo != null && urlParams.foo != undefined && urlParams.foo != "") { } else {
            customersService.getLastCode().then(function (result) {
                $scope.customer.CUST_CODE = parseInt(result.data) + 1;
            }, function (error) { });
        }
    }

    /////////////////////////////////////////////////////save entry for customer 
    $scope.saveEntry = function () {

        ////////////get total credit and depit for entry master


        $scope.entryMaster = {};
        $scope.entryMaster.ENTRY_SETTING_ID = 79;
        $scope.entryMaster.ENTRY_DATE = $scope.customer.Account.ACC_DATE;
        $scope.entryMaster.ENTRY_CREDIT = 0;
        $scope.entryMaster.ENTRY_DEBIT = 0;
        $scope.entryMaster.ACC_ID = 52;
        $scope.entryMaster.CURRENCY_ID = $scope.customer.Account.CURRENCY_ID;
        $scope.entryMaster.CURRENCY_RATE = $scope.CURRENCY_RATE;
        $scope.entryMaster.ENTRY_NUMBER = $scope.ENTRY_NUMBER;
        $scope.entryMaster.CustID = $scope.customerAccID;
        ////////fill entry details
        $scope.entryDetails = [];


        if ($scope.custEntryMaster != undefined && $scope.custEntryMaster != null) {
            $scope.entryMaster.ENTRY_ID = $scope.custEntryMaster;
        }

        // for (var i = 0; i < $scope.billMaster.accounts.length; i++) {
        $scope.entryDetObj = {};
        $scope.entryDetObj.ENTRY_ROW_NUMBER = 0;

        $scope.entryDetObj.ACC_ID = $scope.customerAccID;
        $scope.entryDetObj.ENTRY_CREDIT = $scope.customer.CreditOpeningAccount;
        $scope.entryDetObj.ENTRY_DEBIT = $scope.customer.DepitOpeningAccount;

        //  $scope.entryDetObj.COST_CENTER_ID = $scope.billMaster.CostCenterID;
        $scope.entryDetails.push($scope.entryDetObj);


        var entryMasterDetails = { EntryMaster: $scope.entryMaster, EntryDetails: $scope.entryDetails };
        entryService.addEntryMasterDetails(entryMasterDetails).then(function (result) {
            var id = result.data;

            //$scope.billMaster.BILL_ID = $scope.billMasterID;
            //PointOfSaleService.updateEntryID($scope.billMaster, id).then(function (response) {
            //    var x = response.data;
            //});
        });

    }


    $scope.getEntryNumber = function () {
        entryService.GetLastEntryNumber(79).then(function (response) {
            var result = response.data;
            $scope.ENTRY_NUMBER = result + 1;
            //return $scope.ENTRY_NUMBER;
        })
    }
}]);