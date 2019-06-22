'use strict'
app.controller('bankController', ['$scope', '$q', '$log', 'authService', 'bankService', 'commonService', 'currencyService', '$location', function ($scope, $q, $log, authService, bankService, commonService, currencyService, $location) {

    $scope.bank = {
        ID: null,
        Code: null,
        ArName: "",
        LatName: "",
        CurrencyID: null,
        Notes: "",
        AccountNo: "",
        AddedBy: "",
        AddedOn: null,
        UpdatedBy: "",
        UpdatedOn: null,
        Disable: false,
        NationId: null,
        GovId: null,
        TownId: null,
        VillageId: null,
        CurrencyName: "",
        AddressNotes: ""
    };

    $scope.bankList = [];
    $scope.pageNum = 1;
    $scope.pageSize = 10;
    $scope.pagesCount = 0;
    $scope.maxSize = 5;
    $scope.bankTotalCount = 0;
    $scope.nationList = [];
    $scope.currencyList = [];

    $scope.get = function (pageNum, pageSize) {
        return bankService.get(pageNum, pageSize);
    }

    $scope.count = function () {
        return bankService.count();
    }

    $scope.add = function (entity) {
        return bankService.add(entity);
    }

    $scope.update = function (entity) {
        return bankService.update(entity);
    }

    $scope.delete = function (entity) {
        return bankService.delete(entity);
    }

    $scope.cleareEntity = function () {
        $scope.bank = {
            ID: null,
            Code: null,
            ArName: "",
            LatName: "",
            CurrencyID: null,
            Notes: "",
            AccountNo: "",
            AddedBy: "",
            AddedOn: null,
            UpdatedBy: "",
            UpdatedOn: null,
            Disable: false,
            NationId: null,
            GovId: null,
            TownId: null,
            VillageId: null,
            CurrencyName: "",
            AddressNotes: ""
        }

        $scope.getlastCode();
    }

    $scope.getBankList = function () {
        $scope.get($scope.pageNum, $scope.pageSize).then(function (result) {
            $scope.bankList = result.data;

        }, function (error) {});
    }


    $scope.getAllOnLoad = function () {
        $q.all([
            $scope.getBankList(),
            $scope.getCurencyList(),
            $scope.getBankTotalCount(),
            $scope.getAllNationList(),
            $scope.getlastCode()
        ]).then(function (result) {
            var urlParams = $location.search();
            if (urlParams.foo != null && urlParams.foo != undefined && urlParams.foo != "") {

                bankService.getById(parseInt(urlParams.foo)).then(function (results) {
                    $scope.bank = results.data;
                    $scope.editEntity(results.data);
                })

            }
        }, function (error) {

        });
    }


    $scope.refreshData = function () {
        $scope.getAllOnLoad();
    }


    $scope.bankPageLoad = function () {
        $scope.getAllOnLoad();
    }

    $scope.getCurencyList = function () {
        currencyService.getAll().then(function (result) {
            $scope.currencyList = result.data;
        }, function (error) {});
    }


    $scope.saveEntity = function () {
        if ($scope.bank !== undefined && $scope.bank !== null && $scope.bank.Code !== "" && $scope.bank.ArName !== "" && $scope.bank.LatName !== "") {
            debugger;

            if ($scope.bank.ID === 0 || $scope.bank.ID === null) {
                $scope.add($scope.bank).then(function (result) {
                    $scope.cleareEntity();
                    $scope.refreshData();
                    swal({
                        title: 'تم',
                        text: 'حفظ بيانات مراكز التكلفه بنجاح',
                        type: 'success',
                        timer: 1500,
                        showConfirmButton: false
                    })
                }, function (error) {
                    swal('عفواً',
                        'حدث خطأ عند حفظ بيانات مركز التكلفه',
                        'error');
                });
            } else {
                $scope.update($scope.bank).then(function (result) {
                    $scope.cleareEntity();
                    $scope.refreshData();
                    swal({
                        title: 'تم',
                        Text: 'تعديل بيانات مراكز التكلفه',
                        type: 'success',
                        timer: 1500,
                        showConfirmButton: false
                    })
                }, function (error) {
                    swal('عفواً',
                        'حدث خطأ عند تعديل بيانات مراكز التكلفه',
                        'error');
                });
            }
        }
    }

    $scope.saveBank = function () {
        $scope.saveEntity();
    }


    $scope.editEntity = function (entity) {
        $scope.bank = entity;
        $scope.getAllGovernoratesList();
        $scope.getAllTownList();
        $scope.getAllVillageList();
    }

    $scope.deleteEntity = function (entity) {
        swal({
            title: 'هل تريد حذف البنك؟',
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
                $scope.cleareEntity();
                $scope.refreshData();
                swal({
                    title: 'تم',
                    text: 'الحذف بنجاح',
                    type: 'success',
                    timer: 1500,
                    showConfirmButton: false
                });
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


    $scope.getBankTotalCount = function () {
        debugger;
        $scope.count().then(function (result) {
            var data = result.data;
            $scope.bankTotalCount = data;
            $scope.pagesCount = $scope.getPagesCount();

        }, function (error) {
            consol.log(error.data.message);
        });
    }


    $scope.getPagesCount = function () {
        debugger;
        var div = Math.floor($scope.bankTotalCount / $scope.pageSize);
        var rem = $scope.bankTotalCount % $scope.pageSize;
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
        commonService.GetGovernatesByNationID($scope.bank.NationId).then(function (result) {
            $scope.governoratesList = result.data;
        }, function (error) {
            console.log(error.data.message);
        });
    }

    $scope.getTownByGovernoratesId = function () {
        commonService.GetTownsByGovernateID($scope.bank.GovId).then(function (result) {
            $scope.townsList = result.data;
        }, function (error) {
            console.log(error.data.message);
        });
    }


    $scope.getVillageByTownId = function () {
        commonService.GetVillageByTownID($scope.bank.TownId).then(function (result) {
            $scope.villageList = result.data;
        }, function (error) {
            console.log(error.data.message);
        });
    }
    ////////////////////////////////last code
    $scope.getlastCode = function () {
        var urlParams = $location.search();
        if (urlParams.foo != null && urlParams.foo != undefined && urlParams.foo != "") {} else {
            bankService.getLastCode().then(function (result) {
                $scope.bank.Code = parseInt(result.data) + 1;
            }, function (error) {});
        }
    }
}]);