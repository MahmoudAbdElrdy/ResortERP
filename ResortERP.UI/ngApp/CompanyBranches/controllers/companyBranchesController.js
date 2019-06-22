'use strict'
app.controller('companyBranchesController', ['$scope', '$location', '$log', '$q', 'authService', 'compBranchesService', 'commonService', 'currencyService', function ($scope, $location, $log, $q, authService, compBranchesService, commonService, currencyService) {
    $scope.companyBranches = {
        COM_BRN_ID: null, COM_BRN_CODE: "", COM_BRN_AR_NAME: "", COM_BRN_EN_NAME: "", COM_BRN_AR_ABRV: "", COM_BRN_EN_ABRV: "", NATION_ID: 0, GOV_ID: 0, TOWN_ID: null, VILLAGE_ID: null,
        COM_BRN_ADDR_REMARKS: "", COM_MASTER_BRN_ID: null, COM_BRN_REMARKS: "", AddedBy: "", AddedOn: null, UpdatedBy: "", updatedOn: null, Disable: false
    }


    $scope.companyBranchesList = [];
    $scope.pageNum = 1;
    $scope.pageSize = 10;
    $scope.pagesCount = 0;
    $scope.maxSize = 5;
    $scope.companyBranchesTotalCount = 0;
    $scope.nationList = [];


    $scope.get = function (pageNum, pageSize) {
        return compBranchesService.get(pageNum, pageSize)
    }


    $scope.add = function (entity) {

        return compBranchesService.add(entity)
    }

    $scope.update = function (entity) {

        return compBranchesService.update(entity);
    }

    $scope.delete = function (entity) {
        return compBranchesService.delete(entity);
    }

    $scope.count = function () {
        return compBranchesService.count();
    }

    $scope.getMainCompanyBranches = function () {
        return compBranchesService.getMainCompanyBranches();
    }

    $scope.getCompanyBranchesList = function () {
        $scope.get($scope.pageNum, $scope.pageSize).then(function (result) {
            var data = result.data;
            $scope.companyBranchesList = data;

        }, function (error) {
        });
    }

    $scope.getAllOnLoad = function () {
        $q.all([
            $scope.getCompanyBranchesList(),
            $scope.getMainCompanyBranches(),
            $scope.getCompanyBranchesTotalCount(),
            $scope.getAllNationList(),
            $scope.getAllCurrencies(),
            $scope.getlastCode()
        ]).then(function (result) {
            var urlParams = $location.search();
            if (urlParams.foo != null && urlParams.foo != undefined && urlParams.foo != "") {
                compBranchesService.getById(parseInt(urlParams.foo)).then(function (results) {
                    $scope.companyBranches = results.data;
                    $scope.loadEditData(results.data);
                })

            }
        }, function (error) {

        });
    }

    $scope.companyBranchesPageLoad = function () {
        $scope.getAllOnLoad();
    }
    $scope.clearEnity = function () {
        $scope.companyBranches = {
            COM_BRN_ID: null, COM_BRN_CODE: "", COM_BRN_AR_NAME: "", COM_BRN_EN_NAME: "", COM_BRN_AR_ABRV: "", COM_BRN_EN_ABRV: "", NATION_ID: 0, GOV_ID: 0, TOWN_ID: null, VILLAGE_ID: null,
            COM_BRN_ADDR_REMARKS: "", COM_MASTER_BRN_ID: null, COM_BRN_REMARKS: "", AddedBy: "", AddedOn: null, UpdatedBy: "", updatedOn: null, Disable: false
        }
        $scope.getlastCode();
    }

    $scope.refreshData = function () {
        $scope.getAllOnLoad();
    }


    $scope.saveEnity = function () {
        if ($scope.companyBranches !== undefined && $scope.companyBranches !== null && $scope.companyBranches.COM_BRN_CODE !== "" && $scope.companyBranches.COM_BRN_AR_NAME !== "" && $scope.companyBranches.COM_BRN_EN_NAME !== "") {

            var userName = authService.GetUserName();
            var transDate = new Date();

            if ($scope.companyBranches.COM_BRN_ID === null || $scope.companyBranches.COM_BRN_ID === 0 || $scope.companyBranches.COM_BRN_ID === undefined) {
                //add
                $scope.companyBranches.AddedBy = userName;
                $scope.companyBranches.AddedOn = transDate;

                $scope.add($scope.companyBranches).then(function (result) {
                    $scope.clearEnity();
                    $scope.refreshData();
                    swal({
                        title: 'تم',
                        text: 'حفظ بيانات فرع الشركه',
                        type: 'success',
                        timer: 1500,
                        showConfirmationButton: false
                    })

                }
                    , function (error) {
                        swal('عفواً',
                            'حدث خطأ عند حفظ بيانات فرع الشركه',
                            'error');
                    });

            }
            else {

                //update
                $scope.companyBranches.UpdatedBy = userName;
                $scope.companyBranches.updatedOn = transDate;

                $scope.update($scope.companyBranches).then(function (result) {
                    $scope.clearEnity();
                    $scope.refreshData();
                    swal({
                        title: 'تم',
                        text: 'تعديل بيانات فرع الشركه',
                        type: 'success',
                        timer: 1500,
                        showConfirmationButton: false
                    })

                }
                    , function (error) {
                        swal('عفواً',
                            'حدث خطأ عند تعديل بيانات فرع الشركه',
                            'error');
                    });
            }
        }
    }

    $scope.saveCompanyBranch = function () {
        $scope.saveEnity();
    }


    $scope.loadEditData = function (entity) {

        $scope.companyBranches = entity;
        $scope.getAllGovernoratesList();
        $scope.getAllTownList();
        $scope.getAllVillageList();

    }

    //$scope.deleteEntity = function (entity) {

    //    swal({
    //        title: 'هل تريد حذف مراكز التكلفه؟',
    //        text: "لن تستطيع عكس عملية الحذف مرة أخري",
    //        type: 'warning',
    //        showCancelButton: true,
    //        confirmButtonColor: '#3085d6',
    //        cancelButtonColor: '#d33',
    //        confirmButtonText: 'نعم',
    //        cancelButtonText: 'الغاء',
    //        confirmButtonClass: 'btn btn-success btn-lg',
    //        cancelButtonClass: 'btn btn-danger btn-lg',
    //        buttonsStyling: false
    //    }).then(function () {
    //        $scope.delete(entity).then(function (results) {
    //            $scope.clearEnity();
    //            $scope.refreshData();
    //            swal({
    //                title: 'تم',
    //                text: 'الحذف بنجاح',
    //                type: 'success',
    //                timer: 1500,
    //                showConfirmButton: false
    //            });
    //        }, function (error) {
    //            console.log(error.data.message);
    //        });
    //    }, function (dismiss) {
    //        // dismiss can be 'cancel', 'overlay',
    //        // 'close', and 'timer'
    //        if (dismiss === 'cancel') {
    //            swal({
    //                title: 'تم',
    //                text: 'الغاء عملية الحذف',
    //                type: 'error',
    //                timer: 1500,
    //                showConfirmButton: false
    //            });
    //        }
    //    })
    //}

    $scope.deleteEntity = function (entity) {
        swal({
            title: 'هل تريد حذف فرع الشركة؟',
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
                        text: "هذا الفرع تمت عليه عمليات مختلفة. لا تستطيع حذفه",
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
        //    title: 'هل تريد حذف مراكز التكلفه؟',
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
        //        // alert(results.data);
        //        if (results.data == false) {
        //            swal({
        //                title: "تحذير",
        //                text: "هذا الفرع له مستخدمين",
        //                type: "warning",
        //                showCancelButton: true,
        //                confirmButtonColor: '#3085d6',
        //                cancelButtonColor: '#d33',
        //                confirmButtonText: 'حذف',
        //                cancelButtonText: 'الغاء الحذف',
        //                confirmButtonClass: 'btn btn-danger btn-lg',
        //                cancelButtonClass: 'btn btn-success btn-lg',
        //                buttonsStyling: false

        //            }).then((willDelete) => {

        //                if (willDelete) {

        //                    compBranchesService.deleteBranUsers(entity).then(function (result) {
        //                        $scope.clearEnity();
        //                        $scope.refreshData();
        //                        swal({
        //                            title: 'تم',
        //                            text: ' الحذف',
        //                            type: 'success',
        //                            timer: 1500,
        //                            showConfirmButton: false
        //                        });
        //                    });

        //                }
        //                //else {
        //                //    alert(willDelete);
        //                //    swal("تم الغاء الحذف ");
        //                //}

        //            },
        //                function (dismiss) {
        //                    // dismiss can be 'cancel', 'overlay',
        //                    // 'close', and 'timer'
        //                    if (dismiss === 'cancel') {
        //                        swal({
        //                            title: 'تم',
        //                            text: 'الغاء عملية الحذف',
        //                            type: 'error',
        //                            timer: 1500,
        //                            showConfirmButton: false
        //                        });
        //                    }
        //                }

        //                );
        //        } else {
        //            $scope.clearEnity();
        //            $scope.refreshData();
        //            swal({
        //                title: 'تم',
        //                text: 'الحذف بنجاح',
        //                type: 'success',
        //                timer: 1500,
        //                showConfirmButton: false
        //            });
        //        }

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

    $scope.getCompanyBranchesTotalCount = function () {

        $scope.count().then(function (result) {
            var data = result.data;
            $scope.companyBranchesTotalCount = data;
            $scope.pagesCount = $scope.getPagesCount();

        }
            , function (error) {
                consol.log(error.data.message);
            });

    }


    $scope.getPagesCount = function () {

        var div = Math.floor($scope.companyBranchesTotalCount / $scope.pageSize);
        var rem = $scope.companyBranchesTotalCount % $scope.pageSize;
        if (rem > 0) {
            div = div + 1;
        }
        $scope.pagesCount = div;
        return div;
    }


    $scope.pageChanged = function () {
        $scope.getAllOnLoad();
        $log.log('Page changed to: ' + $scope.pageNum);
    };

    //Address
    $scope.getAllNationList = function () {
        commonService.getAllNations().then(function (result) {
            $scope.nationList = result.data;
        });
    }

    $scope.getAllGovernoratesList = function () {
        $scope.getGovernoratesByNationId();
    }


    $scope.getAllTownList = function () {
        $scope.getTownByGovernoratesId();
    }


    $scope.getAllVillageList = function () {
        $scope.getVillageByTownId();
    }


    $scope.getGovernoratesByNationId = function () {
        commonService.GetGovernatesByNationID($scope.companyBranches.NATION_ID).then(function (result) {
            $scope.governoratesList = result.data;
        }
            , function (error) {
                console.log(error.data.message);
            });
    }

    $scope.getTownByGovernoratesId = function () {
        commonService.GetTownsByGovernateID($scope.companyBranches.GOV_ID).then(function (result) {
            $scope.townsList = result.data;
        }
            , function (error) {
                console.log(error.data.message);
            });
    }


    $scope.getVillageByTownId = function () {
        commonService.GetVillageByTownID($scope.companyBranches.TOWN_ID).then(function (result) {
            $scope.villageList = result.data;
        }
            , function (error) {
                console.log(error.data.message);
            });
    }



    $scope.getAllCurrencies = function () {

        currencyService.getAll().then(function (response) {

            var result = response.data;
            $scope.currenciesList = result;
        })
    }


    $scope.changeCurrencyRate = function () {
        var currencyID = $scope.companyBranches.CURRENCY_ID;
        currencyService.getBy(currencyID).then(function (result) {
            $scope.CURRENCY_RATE = result.data.CURRENCY_RATE;
        });

    }
    ///////////////////////////last code
    $scope.getlastCode = function () {
        var urlParams = $location.search();
        if (urlParams.foo != null && urlParams.foo != undefined && urlParams.foo != "") { } else {
            compBranchesService.getLastCode().then(function (result) {
                $scope.companyBranches.COM_BRN_CODE = parseInt(result.data) + 1;
            }, function (error) { });
        }
    }


}]);