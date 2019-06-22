'use strict'
app.controller('addressController', ['$scope', '$location', '$log', '$q', 'authService', 'addressService', 'commonService', 'addressPartsService', '$window', function ($scope, $location, $log, $q, authService, addressService, commonService, addressPartsService, $window) {

    //Nations
    $scope.nations = { NATION_ID: null, NATION_AR_NAME: "", NATION_EN_NAME: "", NATIONALITY_AR_NAME: "", NATIONALITY_EN_NAME: "", AddedBy: "", AddedOn: null, UpdatedBy: "", updatedOn: null, Disable: false };
    $scope.nationsList = [];
    $scope.nationsPageNum = 1;
    $scope.nationPageSize = 10;
    $scope.nationPagesCount = 0;
    $scope.nationMaxSize = 5;
    $scope.nationTotalCount = 0;

    $scope.addressParts = { AddressPartID: null, AddressPart1: "", AddressPart2: "", AddressPart3: "", AddressPart4: "", UID: "" }

    $scope.addressPageLoad = function () {
        $scope.getAddressParts();
        $scope.getAllNationOnLoad();
        $scope.getAllGovernOnLoad();
        $scope.getAllTownsOnLoad();
        $scope.getAllVillageOnLoad();
    }

    $scope.getAddressParts = function () {
        var userName = authService.GetUserName();
        addressPartsService.getByUserName(userName).then(function (result) {
            //$window.alert(result);
            //$window.alert(result.data);
            if (result.data != null) {
                $scope.addressParts = result.data;
            }
        }, function (error) {
            console.log(error.data.message);
        });

    }


    $scope.getNations = function (nationsPageNum, nationPageSize) {
        return addressService.getNations(nationsPageNum, nationPageSize);
    }


    $scope.addNation = function (entity) {
        return addressService.addNation(entity);
    }


    $scope.updateNation = function (entity) {
        return addressService.updateNation(entity);
    }

    $scope.deleteNation = function (entity) {
        return addressService.deleteNation(entity);
    }

    $scope.countNation = function () {
        return addressService.countNation();
    }


    $scope.getNationList = function () {
        $scope.getNations($scope.nationsPageNum, $scope.nationPageSize).then(function (result) {
            $scope.nationsList = result.data;

            var urlParams = $location.search();
            if (urlParams.foo != null && urlParams.foo != undefined && urlParams.foo != "") {
                $.each($scope.nationsList, function (index, value) {
                    if (value.NATION_ID == parseInt(urlParams.foo)) {
                        $scope.nations = value;
                        $scope.EditNation(value);
                    }
                });

            }

        }, function (error) { });
    }

    $scope.getAllNationOnLoad = function () {
        $q.all([
            $scope.getNationList(),
            $scope.getNationTotalCount(),
        ]).then(function (result) { }, function (error) { });
    }

    $scope.refreshNation = function () {
        $scope.getAllNationOnLoad();
    }

    $scope.clearNation = function () {
        $scope.nations = { NATION_ID: null, NATION_AR_NAME: "", NATION_EN_NAME: "", NATIONALITY_AR_NAME: "", NATIONALITY_EN_NAME: "", AddedBy: "", AddedOn: null, UpdatedBy: "", updatedOn: null, Disable: false };
    }

    $scope.saveNation = function () {
        if ($scope.nations !== undefined && $scope.nations !== null && $scope.nations.NATION_AR_NAME !== "" && $scope.nations.NATION_EN_NAME) {
            if ($scope.nations.NATION_ID === undefined || $scope.nations.NATION_ID === null || $scope.nations.NATION_ID === 0) {
                //add
                var userName = authService.GetUserName();
                $scope.nations.AddedBy = userName;

                var addedDate = new Date();
                $scope.nations.AddedOn = addedDate;

                $scope.addNation($scope.nations).then(function (result) {
                    $scope.refreshNation();
                    $scope.clearNation();
                    swal({
                        title: 'تم',
                        text: 'حفظ بيانات الدوله بنجاح',
                        type: 'success',
                        timer: 1500,
                        showConfirmButton: false
                    })
                }, function (error) {
                    swal('عفواً',
                        'حدث خطأ عند حفظ بيانات الدوله',
                        'error');
                });

            }
            else {
                //update
                var userName = authService.GetUserName();
                $scope.nations.UpdatedBy = userName;

                var addedDate = new Date();
                $scope.nations.updatedOn = addedDate;

                $scope.updateNation($scope.nations).then(function (result) {
                    $scope.clearNation();
                    $scope.refreshNation();
                    swal({
                        title: 'تم',
                        Text: 'تعديل بيانات الدوله',
                        type: 'success',
                        timer: 1500,
                        showConfirmButton: false
                    })
                }, function (error) {
                    swal('عفواً',
                        'حدث خطأ عند تعديل بيانات الدوله',
                        'error');
                });
            }
        }
    }

    $scope.saveNationEntity = function () {
        $scope.saveNation();
    }


    $scope.EditNation = function (entity) {
        $scope.nations = entity;
    }

    $scope.deleteNationEntity = function (entity) {
        swal({
            title: 'هل تريد حذف الدوله؟',
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
            $scope.deleteNation(entity).then(function (results) {
                // alert(results.data);
                if (results.data == false) {
                    swal({
                        title: "تحذير",
                        text: "هذه الدولة تم عليها عمليات مختلفة. لا تستطيع حذفها",
                        type: "warning",
                        showConfirmButton: true

                    })
                } else {
                    $scope.clearNation();
                    $scope.refreshNation();
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
        //    title: 'هل تريد حذف الدوله؟',
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
        //    $scope.deleteNation(entity).then(function (results) {
        //        $scope.clearNation();
        //        $scope.refreshNation();
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

    $scope.getNationPagesCount = function () {
      
        var div = Math.floor($scope.nationTotalCount / $scope.nationPageSize);
        var rem = $scope.nationTotalCount % $scope.nationPageSize;
        if (rem > 0) {
            div = div + 1;
        }
        $scope.nationPagesCount = div;
        return div;
    }

    $scope.getNationTotalCount = function () {

        $scope.countNation().then(function (result) {
            var data = result.data;
            $scope.nationTotalCount = data;
            $scope.nationPagesCount = $scope.getNationPagesCount();

        }
            , function (error) {
                consol.log(error.data.message);
            });
    }

    $scope.nationPageChanged = function () {
        $scope.getAllNationOnLoad();
        $log.log('Page changed to: ' + $scope.nationsPageNum);
    };
    //End Nations




    //Governrates
    $scope.governs = { GOV_ID: null, GOV_AR_NAME: "", GOV_EN_NAME: "", NATION_ID: null, NationName: "", AddedBy: "", AddedOn: null, UpdatedBy: "", updatedOn: null, Disable: false };
    $scope.governList = [];
    $scope.governPageNum = 1;
    $scope.governPageSize = 10;
    $scope.governPagesCount = 0;
    $scope.governMaxSize = 5;
    $scope.governTotalCount = 0;



    $scope.getGoverns = function (governPageNum, governPageSize) {
        return addressService.getGovern(governPageNum, governPageSize);
    }


    $scope.addGovern = function (entity) {
        return addressService.addGovern(entity);
    }


    $scope.updateGovern = function (entity) {
        return addressService.updateGovern(entity);
    }

    $scope.deleteGovern = function (entity) {
        return addressService.deleteGovern(entity);
    }

    $scope.countGovern = function () {
        return addressService.countGovern();
    }

    $scope.getAllGovernOnLoad = function () {
        $q.all([
            $scope.getGovernList(),
            $scope.getGovernTotalCount(),
        ]).then(function (result) { }, function (error) { });
    }

    $scope.getGovernList = function () {
        $scope.getGoverns($scope.governPageNum, $scope.governPageSize).then(function (result) {

            $scope.governList = result.data;
            var urlParams = $location.search();
            if (urlParams.foo != null && urlParams.foo != undefined && urlParams.foo != "") {
                $.each($scope.governList, function (index, value) {
                    if (value.GOV_ID == parseInt(urlParams.foo)) {
                        $('ul.nav.nav-tabs a[href="#tab2"]').trigger('click');
                        $scope.governs = value;
                        $scope.EditGoverns(value);
                    }
                });

            }
        }, function (error) { });
    }


    $scope.refreshGovern = function () {
        $scope.getAllGovernOnLoad();
    }

    $scope.clearGovern = function () {
        $scope.governs = { GOV_ID: null, GOV_AR_NAME: "", GOV_EN_NAME: "", NATION_ID: null, NationName: "", AddedBy: "", AddedOn: null, UpdatedBy: "", updatedOn: null, Disable: false };
    }

    $scope.saveGovern = function () {
        if ($scope.governs !== undefined && $scope.governs !== null && $scope.governs.GOV_EN_NAME !== "" && $scope.governs.GOV_AR_NAME) {
            if ($scope.governs.GOV_ID === undefined || $scope.governs.GOV_ID === null || $scope.governs.GOV_ID === 0) {
                //add
                var userName = authService.GetUserName();
                $scope.governs.AddedBy = userName;

                var addedDate = new Date();
                $scope.governs.AddedOn = addedDate;

                $scope.addGovern($scope.governs).then(function (result) {
                    $scope.refreshGovern();
                    $scope.clearGovern();
                    swal({
                        title: 'تم',
                        text: 'حفظ بيانات المحافظه بنجاح',
                        type: 'success',
                        timer: 1500,
                        showConfirmButton: false
                    })
                }, function (error) {
                    swal('عفواً',
                        'حدث خطأ عند حفظ بيانات المحافظه',
                        'error');
                });

            }
            else {
                //update
                var userName = authService.GetUserName();
                $scope.governs.UpdatedBy = userName;

                var addedDate = new Date();
                $scope.governs.updatedOn = addedDate;

                $scope.updateGovern($scope.governs).then(function (result) {
                    $scope.clearGovern();
                    $scope.refreshGovern();
                    swal({
                        title: 'تم',
                        Text: 'تعديل بيانات المحافظه',
                        type: 'success',
                        timer: 1500,
                        showConfirmButton: false
                    })
                }, function (error) {
                    swal('عفواً',
                        'حدث خطأ عند تعديل بيانات المحافظه',
                        'error');
                });
            }
        }
    }

    $scope.saveGovernEntity = function () {
        $scope.saveGovern();
    }


    $scope.EditGoverns = function (entity) {
        $scope.governs = entity;
    }

    $scope.deletegovernEntity = function (entity) {
        swal({
            title: 'هل تريد حذف المحافظه؟',
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
            $scope.deleteGovern(entity).then(function (results) {
                // alert(results.data);
                if (results.data == false) {
                    swal({
                        title: "تحذير",
                        text: "هذه المحافظه تم عليها عمليات مختلفة. لا تستطيع حذفها",
                        type: "warning",
                        showConfirmButton: true

                    })
                } else {
                    $scope.clearGovern();
                    $scope.refreshGovern();
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
        //    title: 'هل تريد حذف المحافظه؟',
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
        //    $scope.deleteGovern(entity).then(function (results) {
        //        $scope.clearGovern();
        //        $scope.refreshGovern();
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

    $scope.getGovernPagesCount = function () {
   
        var div = Math.floor($scope.governTotalCount / $scope.governPageSize);
        var rem = $scope.governTotalCount % $scope.governPageSize;
        if (rem > 0) {
            div = div + 1;
        }
        $scope.governPagesCount = div;
        return div;
    }

    $scope.getGovernTotalCount = function () {

        $scope.countGovern().then(function (result) {
            var data = result.data;
            $scope.governTotalCount = data;
            $scope.governPagesCount = $scope.getGovernPagesCount();

        }
            , function (error) {
                consol.log(error.data.message);
            });
    }

    $scope.governPageChanged = function () {
        $scope.getAllGovernOnLoad();
        $log.log('Page changed to: ' + $scope.governPageNum);
    };
    ///end govern





    //towns

    $scope.towns = { TOWN_ID: null, TOWN_AR_NAME: "", TOWN_EN_NAME: "", GOV_ID: null, GovName: "", NationId: null, NationName:"" ,AddedBy: "", AddedOn: null, UpdatedBy: "", updatedOn: null, Disable: false };
    $scope.townsList = [];
    $scope.townPageNum = 1;
    $scope.townPageSize = 10;
    $scope.townPagesCount = 0;
    $scope.townMaxSize = 5;
    $scope.townTotalCount = 0;
    $scope.ddlTownGovList = [];


    $scope.getTowns = function (townPageNum, townPageSize) {
        return addressService.getTown(townPageNum, townPageSize);
    }

    $scope.addTown = function (entity) {
        return addressService.addTown(entity);
    }

    $scope.updateTown = function (entity) {
        return addressService.updateTown(entity);
    }

    $scope.deleteTown = function (entity) {
        return addressService.deleteTown(entity);
    }

    $scope.countTown = function () {
        return addressService.countTown();
    }

    $scope.getAllTownsOnLoad = function () {
        $q.all([
            $scope.getTownsList(),
            $scope.getTownTotalCount(),
        ]).then(function (result) { }, function (error) { });
    }

    $scope.getTownsList = function () {
        $scope.getTowns($scope.townPageNum, $scope.townPageSize).then(function (result) {
            $scope.townsList = result.data;

            var urlParams = $location.search();
            if (urlParams.foo != null && urlParams.foo != undefined && urlParams.foo != "") {
                $.each($scope.townsList, function (index, value) {
                    if (value.TOWN_ID == parseInt(urlParams.foo)) {
                        $('ul.nav.nav-tabs a[href="#tab3"]').trigger('click');
                        $scope.towns = value;
                        $scope.EditTown(value);
                    }
                });

            }
        }, function (error) { });
    }

    $scope.refreshTown = function () {
        $scope.getAllTownsOnLoad();
    }

    $scope.clearTowns = function () {
        $scope.towns = { TOWN_ID: null, TOWN_AR_NAME: "", TOWN_EN_NAME: "", GOV_ID: null, GovName: "", NationId: null, NationName: "", AddedBy: "", AddedOn: null, UpdatedBy: "", updatedOn: null, Disable: false };
    }

    $scope.saveTown = function () {
        if ($scope.towns !== undefined && $scope.towns !== null && $scope.towns.TOWN_AR_NAME !== "" && $scope.towns.TOWN_EN_NAME) {
            if ($scope.towns.TOWN_ID === undefined || $scope.towns.TOWN_ID === null || $scope.towns.TOWN_ID === 0) {
                //add
                var userName = authService.GetUserName();
                $scope.towns.AddedBy = userName;

                var addedDate = new Date();
                $scope.towns.AddedOn = addedDate;

                $scope.addTown($scope.towns).then(function (result) {
                    $scope.refreshTown();
                    $scope.clearTowns();
                    swal({
                        title: 'تم',
                        text: 'حفظ بيانات المدينه بنجاح',
                        type: 'success',
                        timer: 1500,
                        showConfirmButton: false
                    })
                }, function (error) {
                    swal('عفواً',
                        'حدث خطأ عند حفظ بيانات المدينه',
                        'error');
                });

            }
            else {
                //update
                var userName = authService.GetUserName();
                $scope.towns.UpdatedBy = userName;

                var addedDate = new Date();
                $scope.towns.updatedOn = addedDate;

                $scope.updateTown($scope.towns).then(function (result) {
                    $scope.clearTowns();
                    $scope.refreshTown();
                    swal({
                        title: 'تم',
                        Text: 'تعديل بيانات المدينه',
                        type: 'success',
                        timer: 1500,
                        showConfirmButton: false
                    })
                }, function (error) {
                    swal('عفواً',
                        'حدث خطأ عند تعديل بيانات المدينه',
                        'error');
                });
            }
        }
    }

    $scope.saveTownEntity = function () {
        $scope.saveTown();
    }

    $scope.EditTown = function (entity) {
        $scope.getNationList();
        $scope.towns = entity;
    }

    $scope.deleteTownEntity = function (entity) {
        swal({
            title: 'هل تريد حذف المدينه؟',
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
            $scope.deleteTown(entity).then(function (results) {
                // alert(results.data);
                if (results.data == false) {
                    swal({
                        title: "تحذير",
                        text: "هذه المدينة تم عليها عمليات مختلفة. لا تستطيع حذفها",
                        type: "warning",
                        showConfirmButton: true

                    })
                } else {
                    $scope.clearTowns();
                    $scope.refreshTown();
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
        //    title: 'هل تريد حذف المدينه؟',
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
        //    $scope.deleteTown(entity).then(function (results) {
        //        $scope.clearTowns();
        //        $scope.refreshTown();
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

    $scope.getTownPagesCount = function () {

        var div = Math.floor($scope.townTotalCount / $scope.townPageSize);
        var rem = $scope.townTotalCount % $scope.townPageSize;
        if (rem > 0) {
            div = div + 1;
        }
        $scope.townPagesCount = div;
        return div;
    }

    $scope.getTownTotalCount = function () {

        $scope.countTown().then(function (result) {
            var data = result.data;
            $scope.townTotalCount = data;
            $scope.townPagesCount = $scope.getTownPagesCount();

        }
            , function (error) {
                consol.log(error.data.message);
            });
    }

    $scope.townPageChanged = function () {
        $scope.getAllTownsOnLoad();
        $log.log('Page changed to: ' + $scope.townPageNum);
    };

    $scope.getGovernoratesByNationId = function () {
        commonService.GetGovernatesByNationID($scope.towns.NationId).then(function (result) {
            $scope.ddlTownGovList = result.data;
        }
            , function (error) {
                console.log(error.data.message);
            });
    }

    //end town 






    //village

    $scope.village = { VILLAGE_ID: null, VILLAGE_AR_NAME: "", VILLAGE_EN_NAME: "", TOWN_ID: null, TownName: "", GovId: null, GovName: "", NationId: null, NationName:"", AddedBy: "", AddedOn: null, UpdatedBy: "", updatedOn: null, Disable: false };
    $scope.villageList = [];
    $scope.villagePageNum = 1;
    $scope.villagePageSize = 10;
    $scope.villagePagesCount = 0;
    $scope.villageMaxSize = 5;
    $scope.villageTotalCount = 0;
    $scope.ddlVillGovList = [];
    $scope.ddlVillTownList = [];

    $scope.getVillage = function (villagePageNum, villagePageSize) {
        return addressService.getVillage(villagePageNum, villagePageSize);
    }

    $scope.addVillage = function (entity) {
        return addressService.addVillage(entity);
    }

    $scope.updateVillage = function (entity) {
        return addressService.updateVillage(entity);
    }

    $scope.deleteVillage = function (entity) {
        return addressService.deleteVillage(entity);
    }

    $scope.countVillage = function () {
        return addressService.countVillage();
    }

    $scope.getAllVillageOnLoad = function () {
        $q.all([
            $scope.getVillageList(),
            $scope.getVillageTotalCount(),
        ]).then(function (result) { }, function (error) { });
    }

    $scope.getVillageList = function () {
        debugger;
        $scope.getVillage($scope.villagePageNum, $scope.villagePageSize).then(function (result) {
            $scope.villageList = result.data;

            var urlParams = $location.search();
            if (urlParams.foo != null && urlParams.foo != undefined && urlParams.foo != "") {
                $.each($scope.villageList, function (index, value) {
                    if (value.VILLAGE_ID == parseInt(urlParams.foo)) {
                        $('ul.nav.nav-tabs a[href="#tab4"]').trigger('click');
                        $scope.village = value;
                        $scope.EditVillage(value);
                    }
                });

            }
        }, function (error) { });
    }

    $scope.refreshVillage = function () {
        $scope.getAllVillageOnLoad();
    }

    $scope.clearVillage = function () {
        $scope.village = { VILLAGE_ID: null, VILLAGE_AR_NAME: "", VILLAGE_EN_NAME: "", TOWN_ID: null, TownName: "", GovId: null, GovName: "", NationId: null, NationName: "", AddedBy: "", AddedOn: null, UpdatedBy: "", updatedOn: null, Disable: false };
    }

    $scope.saveVillage = function () {
        debugger;
        if ($scope.village !== undefined && $scope.village !== null && $scope.village.VILLAGE_AR_NAME !== "" && $scope.village.VILLAGE_EN_NAME !=="") {
            if ($scope.village.VILLAGE_ID === undefined || $scope.village.VILLAGE_ID === null || $scope.village.VILLAGE_ID === 0) {
                //add
                var userName = authService.GetUserName();
                $scope.village.AddedBy = userName;

                var addedDate = new Date();
                $scope.village.AddedOn = addedDate;

                $scope.addVillage($scope.village).then(function (result) {
                    $scope.refreshVillage();
                    $scope.clearVillage();
                    swal({
                        title: 'تم',
                        text: 'حفظ بيانات الحي بنجاح',
                        type: 'success',
                        timer: 1500,
                        showConfirmButton: false
                    })
                }, function (error) {
                    swal('عفواً',
                        'حدث خطأ عند حفظ بيانات الحي',
                        'error');
                });

            }
            else {
                //update
                var userName = authService.GetUserName();
                $scope.village.UpdatedBy = userName;

                var addedDate = new Date();
                $scope.village.updatedOn = addedDate;

                $scope.updateVillage($scope.village).then(function (result) {
                    $scope.clearVillage();
                    $scope.refreshVillage();
                    swal({
                        title: 'تم',
                        Text: 'تعديل بيانات الحي',
                        type: 'success',
                        timer: 1500,
                        showConfirmButton: false
                    })
                }, function (error) {
                    swal('عفواً',
                        'حدث خطأ عند تعديل بيانات الحي',
                        'error');
                });
            }
        }
    }

    $scope.saveVillageEntity = function () {
        $scope.saveVillage();
    }

    $scope.EditVillage = function (entity) {
        $scope.village = entity;
        $scope.getNationList();
        $scope.getVillGovernByNationId();
        $scope.getVillTownByGovern();
    }

    $scope.deleteVillageEntity = function (entity) {
        swal({
            title: 'هل تريد حذف الحي؟',
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
            $scope.deleteVillage(entity).then(function (results) {
                // alert(results.data);
                if (results.data == false) {
                    swal({
                        title: "تحذير",
                        text: "هذه المدينة تم عليها عمليات مختلفة. لا تستطيع حذفها",
                        type: "warning",
                        showConfirmButton: true

                    })
                } else {
                    $scope.clearVillage();
                    $scope.refreshVillage();
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
        //debugger;
        //swal({
        //    title: 'هل تريد حذف الحي؟',
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
        //    $scope.deleteVillage(entity).then(function (results) {
        //        $scope.clearVillage();
        //        $scope.refreshVillage();
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

    $scope.getVillagePagesCount = function () {

        var div = Math.floor($scope.villageTotalCount / $scope.villagePageSize);
        var rem = $scope.villageTotalCount % $scope.villagePageSize;
        if (rem > 0) {
            div = div + 1;
        }
        $scope.villagePagesCount = div;
        return div;
    }

    $scope.getVillageTotalCount = function () {

        $scope.countVillage().then(function (result) {
            var data = result.data;
            $scope.villageTotalCount = data;
            $scope.villagePagesCount = $scope.getVillagePagesCount();

        }
            , function (error) {
                consol.log(error.data.message);
            });
    }

    $scope.villagePageChanged = function () {
        $scope.getAllVillageOnLoad();
        $log.log('Page changed to: ' + $scope.villagePageNum);
    };

    $scope.getVillGovernByNationId = function () {
        commonService.GetGovernatesByNationID($scope.village.NationId).then(function (result) {
            $scope.ddlVillGovList = result.data;
        }
            , function (error) {
                console.log(error.data.message);
            });
    }

    $scope.getVillTownByGovern = function () {
        commonService.GetTownsByGovernateID($scope.village.GovId).then(function (result) {
            $scope.ddlVillTownList= result.data;
        }
            , function (error) {
                console.log(error.data.message);
            });
    }







}]);