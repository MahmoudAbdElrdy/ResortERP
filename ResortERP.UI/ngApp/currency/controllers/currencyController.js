'use strict';
app.controller('currencyController', ['$scope', '$location', '$log', '$q', 'currencyService', 'currencyCategoriesService', '$window', '$rootScope', function ($scope, $location, $log, $q, currencyService, currencyCategoriesService, $window, $rootScope) {
    $scope.currency = { CURRENCY_ID: null, CURRENCY_CODE: null, CURRENCY_AR_NAME: null, CURRENCY_EN_NAME: null, CURRENCY_SUB_AR_NAME: null, CURRENCY_SUB_EN_NAME: null, CURRENCY_SYMBOL_AR_NAME: null, CURRENCY_SYMBOL_EN_NAME: null, CURRENCY_RATE: null, SUB_TO_CURRENCY_TRANS: null, CURRENCY_FIXED_RATE: null };

    $scope.clearEnity = function () {

        $location.search('foo', null);

        if ($location.path() === $rootScope.locationPath) {
            $location.replace();
        }

        $scope.currency = { CURRENCY_ID: null, CURRENCY_CODE: null, CURRENCY_AR_NAME: null, CURRENCY_EN_NAME: null, CURRENCY_SUB_AR_NAME: null, CURRENCY_SUB_EN_NAME: null, CURRENCY_SYMBOL_AR_NAME: null, CURRENCY_SYMBOL_EN_NAME: null, CURRENCY_RATE: null, SUB_TO_CURRENCY_TRANS: null, CURRENCY_FIXED_RATE: null };
        $scope.getLastCode();
    }

    $scope.currencyList = [];
    $scope.currencyTransList = [{ ID: "10", name: "10" }, { ID: "100", name: "100" }, { ID: "1000", name: "1000" }];
    $scope.currencyTotalCount = 0;
    $scope.pageNum = 1;
    $scope.pageSize = 10;
    $scope.pagesCount = 0;
    $scope.maxSize = 5;
    $scope.catList = [];
    $scope.currId = null;

    //$scope.getSelected = function (cur) {
    //    var ID = $scope.currency.SUB_TO_CURRENCY_TRANS;
    //    var name = $.grep($scope.currencyTransList, function (cur) {
    //        return cur.Id == ID;
    //    })[0].name;
    //    alert("Selected Value: " + ID + "\nSelected Text: " + name);
    //}

    $scope.getPagesCount = function () {
        var div = Math.floor($scope.currencyTotalCount / $scope.pageSize);
        var rem = $scope.currencyTotalCount % $scope.pageSize;
        if (rem > 0) {
            div = div + 1;
        }
        return div;
    }

    $scope.refreshData = function () {
        $scope.getAllOnLoad();
    }
    $scope.savecurrency = function () {
        $scope.saveEntity();
    }
    $scope.saveEntity = function () {
        if ($scope.currency !== undefined && $scope.currency !== null && $scope.currency.CURRENCY_CODE !== null && $scope.currency.CURRENCY_AR_NAME !== null && $scope.currency.CURRENCY_EN_NAME !== null && $scope.currency.CURRENCY_SUB_AR_NAME !== null && $scope.currency.CURRENCY_SUB_EN_NAME !== null && $scope.currency.SUB_TO_CURRENCY_TRANS !== null) {
            var x = $scope.catList.length;
            if ($scope.currency.CURRENCY_ID === null || $scope.CURRENCY_ID === 0) {

                $scope.insert($scope.currency).then(function (results) {
                    $scope.currId = results.data;
                    if ($scope.catList.length !== 0) {
                        $scope.saveEntityCat();
                    }
                    $scope.clearEnity();
                    $scope.refreshData();
                    //$scope.cleareEntitycat();
                    //$scope.refreshDataCat();
                    swal({
                        title: 'تم',
                        text: 'حفظ العملة بنجاح',
                        type: 'success',
                        timer: 1500,
                        showConfirmButton: false
                    });
                }, function (error) {
                    console.log(error.data.message);
                    swal('عفواً',
                        'حدث خطأ عند حفظ العملة',
                        'error');
                });
            } else {
                $scope.update($scope.currency).then(function (results) {
                    $scope.clearEnity();
                    $scope.refreshData();
                    //$scope.cleareEntitycat();
                    //$scope.refreshDataCat();
                    if ($scope.catList.length !== 0) {
                        $scope.saveEntityCat();
                    }
                    swal({
                        title: 'تم',
                        text: 'تعديل العملة بنجاح',
                        type: 'success',
                        timer: 1500,
                        showConfirmButton: false
                    });
                }, function (error) {
                    console.log(error.data.message);
                    swal('عفواً',
                        'حدث خطأ عند تعديل العملة',
                        'error');
                });
            }
        }
    }
    $scope.dirEnity = function (entity) {
        $scope.currency = entity;
        $scope.getCatList($scope.currency.CURRENCY_ID);
    }
    $scope.deleteEnity = function (entity) {
        swal({
            title: 'هل تريد حذف العمله؟',
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
                        text: "هذه العمله تمت عليها عمليات مختلفة. لا تستطيع حذفها",
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
        //    title: 'هل تريد حذف العمله؟',
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
                $scope.getcurrencyList(),
                $scope.getcurrencyTotalCount(),
                $scope.getLastCode(),
                $scope.getAllOnLoadCat()

            ]).then(function (allResponses) {
            }, function (error) {
                //This will be called if $q.all finds any of the requests erroring.
                var abc = error;
                var def = 99;
            });
    }
    $scope.getcurrencyTotalCount = function () {
        $scope.count().then(function (results) {
            var data = results.data;
            $scope.currencyTotalCount = data;
            $scope.pagesCount = $scope.getPagesCount();
        }, function (error) {
            console.log(error.data.message);
        });
    }
    $scope.getcurrencyList = function () {
        $scope.get($scope.pageNum, $scope.pageSize).then(function (results) {
            var data = results.data;
            $scope.currencyList = data;
        }, function (error) {
            console.log(error.data.message);
        });
    }


    $scope.get = function (pageNum, pageSize) {
        return currencyService.get(pageNum, pageSize);
    }
    $scope.count = function () {
        return currencyService.count();
    }
    $scope.insert = function (entity) {
        return currencyService.insert(entity);
    }
    $scope.update = function (entity) {
        return currencyService.update(entity);
    }
    $scope.delete = function (entity) {
        return currencyService.delete(entity);
    }
    $scope.currencyPageLoad = function () {
        $scope.getAllOnLoad();
    }

    $scope.pageChanged = function () {
        $scope.getAllOnLoad();
        $log.log('Page changed to: ' + $scope.pageNum);
    };


    $scope.getLastCode = function () {
        var urlParams = $location.search();
        if (urlParams.foo != null && urlParams.foo != undefined && urlParams.foo != "") { } else {
            currencyService.getLastCode().then(function (result) {
                $scope.currency.CURRENCY_CODE = parseInt(result.data) + 1;
            });
        }
    }




    //update
    //$scope.addSkill = function (someParam) {
    //    $scope.skills.push({
    //        editable: true
    //        //some other staff you're implementing
    //    });


    ///////categories

    $scope.category = {
        CategoryID: null, CategoryCode: "", CategoryArName: "", CategoryEnName: "", CategoryTrans: null, CurrencyID: null, AddedBy: "", AddedOn: null, UpdatedBy: ""
        , UpdatedOn: null, Disable: false
    };


    $scope.pageNumCat = 1;
    $scope.pageSizeCat = 10;
    $scope.pagesCountCat = 0;
    $scope.maxSizeCat = 5;
    $scope.catTotalCount = 0;
    //$scope.catList = [];
    $scope.existCatList = [];



    $scope.getCatList = function (Id) {
        if (Id != null && Id != 0) {
            currencyCategoriesService.getByCurrID(Id).then(function (result) {
                $scope.catList = result.data;

            }, function (error) { });
        }
        else {
            $scope.catList = [];
        }
    }

    $scope.getAllOnLoadCat = function () {

        $q.all([
            $scope.getCatList(),
            $scope.getLastCodeCat(0)
            // $scope.getBankTotalCount(),

        ]).then(function (respons) {
            var urlParams = $location.search();
            if (urlParams.foo != null && urlParams.foo != undefined && urlParams.foo != "") {

                currencyService.getByCurrId(parseInt(urlParams.foo)).then(function (results) {
                    $scope.currency = results.data;
                    $scope.dirEnity(results.data);
                })
            }

        }, function (error) {
            var abc = error;
            var def = 99;
        });
    }


    $scope.saveCat = function () {
        if ($scope.category !== undefined && $scope.category !== null && $scope.category.CategoryCode !== "" && $scope.category.CategoryArName !== "" && $scope.category.CategoryEnName !== "") {
            if ($scope.category.CategoryID === 0 || $scope.category.CategoryID === null) {
                //add
                $scope.lastCat = $scope.category.CategoryCode;
                $scope.getLastCodeCat($scope.lastCat);
                $scope.catList.push($scope.category);
                $scope.cleareEntitycat();
                
                $scope.lastCat = 0;
            }
        }
    }


    $scope.addCat = function (entity) {
        return currencyCategoriesService.add(entity);
    }


    $scope.cleareEntitycat = function () {
        $scope.category = {
            CategoryID: null, CategoryCode: "", CategoryArName: "", CategoryEnName: "", CategoryTrans: null, CurrencyID: null, AddedBy: "", AddedOn: null, UpdatedBy: ""
            , UpdatedOn: null, Disable: false
        }
        
    }


    $scope.refreshDataCat = function () {
        $scope.getAllOnLoadCat();
    }

    $scope.getByCurrIdCatId = function (currId, catId) {
        return currencyCategoriesService.getByCurrIdCatId(currId, catId);

    }


    $scope.updateCat = function (entity) {
        return currencyCategoriesService.update(entity);
    }


    $scope.saveEntityCat = function () {
        //  if ($scope.category !== undefined && $scope.category !== null && $scope.category.CategoryCode !== "" && $scope.category.CategoryArName !== "" && $scope.category.CategoryEnName !== "") {
        // if ($scope.category.CategoryID === 0 || $scope.category.CategoryID === null) {
        if ($scope.catList.length !== 0) {

            for (var i = 0; i <= $scope.catList.length - 1; i++) {
                //$window.alert($scope.catList[i]);
                //$scope.category.CategoryID = parseInt($scope.catList[i].CategoryCode);
                $scope.category.CategoryArName = $scope.catList[i].CategoryArName;
                $scope.category.CategoryEnName = $scope.catList[i].CategoryEnName;
                $scope.category.CategoryCode = $scope.catList[i].CategoryCode;
                $scope.category.Disable = $scope.catList[i].Disable;
                $scope.category.CurrencyID = $scope.currId;
                $scope.category.CategoryTrans = $scope.catList[i].CategoryTrans;
                $scope.getByCurrIdCatId($scope.currId, $scope.catList[i].CategoryCode).then(function (result) {
                    // $window.alert(result.data);

                    var res = result.data;
                    // $window.alert(res);
                    // $window.alert($scope.catList[i]);
                    if (res === "t") {
                        //$window.alert('true');
                        $scope.updateCat($scope.category);
                        $scope.cleareEntitycat();
                        $scope.refreshDataCat();
                    }
                    else {

                        //$window.alert('false');
                        $scope.addCat($scope.category);
                        $scope.cleareEntitycat();
                        $scope.refreshDataCat();
                        // $scope.updateCat($scope.catList[i]);
                    }
                }, function (error) {
                });
            }
        }
        //  }
        //  }
    }



    $scope.deleteCat = function (index) {
        $scope.catList.splice(index, 1);
    }



    $scope.getLastCodeCat = function (lastCode) {
        var urlParams = $location.search();
        if (urlParams.foo != null && urlParams.foo != undefined && urlParams.foo != "") { } else {
            currencyCategoriesService.getLastCode().then(function (result) {
                if (lastCode === 0) {
                    $scope.category.CategoryCode = parseInt(result.data) + 1;
                }
                else {
                    $scope.category.CategoryCode = lastCode + 1;
                }
            }, function (error) { });
        }
    }

}]);