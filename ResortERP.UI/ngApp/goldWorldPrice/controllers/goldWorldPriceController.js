'use strict';
app.controller('goldWorldPriceController', ['$scope', '$location', '$log', '$q', 'authService', 'goldWorldPriceService', function ($scope, $location, $log, $q, authService, goldWorldPriceService) {

    $scope.goldWorldPrice = { ID: null, Code: "", ARName: "", LatName: "", PriceDate: null, GoldPrice: 0, Notes: "", Active: false };
    $scope.goldWorldPriceList = [];
    $scope.goldWorldPriceTotalCount = 0;
    $scope.pageNum = 1;
    $scope.pageSize = 10;
    $scope.pagesCount = 0;
    $scope.getPagesCount = function () {
        var div = Math.floor($scope.goldWorldPriceTotalCount / $scope.pageSize);
        var rem = $scope.goldWorldPriceTotalCount % $scope.pageSize;
        if (rem > 0) {
            div = div + 1;
        }
        $scope.pagesCount = div;
        return div;
    }
    $scope.maxSize = 5;



    $scope.clearEnity = function () {
        $scope.goldWorldPrice = { ID: null, Code: "", ARName: "", LatName: "", PriceDate: null, GoldPrice: 0, Notes: "", Active: true };
        $scope.getCurrentDate();
        $scope.getlastCode();
    }
    $scope.refreshData = function () {
        $scope.getAllOnLoad();
    }
    $scope.saveItemsGroup = function () {

        //  debugger;
        $scope.saveEntity();
    }
    $scope.saveEntity = function () {
        // debugger;

        if ($scope.goldWorldPrice !== undefined && $scope.goldWorldPrice !== null && $scope.goldWorldPrice.Code !== "" ) {
            if ($scope.goldWorldPrice.ID === null) {

                $scope.goldWorldPrice.ARName = "";
                $scope.goldWorldPrice.LatName = "";

                $scope.add($scope.goldWorldPrice).then(function (results) {
                    // var data = results.data;
                    // $scope.goldWorldPriceList = data;
                    $scope.clearEnity();
                    $scope.refreshData();
                    swal({
                        title: 'تم',
                        text: 'حفظ بيانات سعر الذهب بنجاح',
                        type: 'success',
                        timer: 1500,
                        showConfirmButton: false
                    });
                }, function (error) {
                    console.log(error.data.message);
                    swal('عفواً',
                        'حدث خطأ عند حفظ بيانات سعر الذهب',
                        'error');
                });
            } else {

                $scope.goldWorldPrice.ARName = "";
                $scope.goldWorldPrice.LatName = "";

                $scope.update($scope.goldWorldPrice).then(function (results) {
                    // var data = results.data;
                    // $scope.goldWorldPriceList = data;
                    $scope.clearEnity();
                    $scope.refreshData();
                    swal({
                        title: 'تم',
                        text: 'تعديل بيانات سعر الذهب بنجاح',
                        type: 'success',
                        timer: 1500,
                        showConfirmButton: false
                    });
                }, function (error) {
                    console.log(error.data.message);
                    swal('عفواً',
                        'حدث خطأ عند تعديل بيانات سعر الذهب',
                        'error');
                });
            }
        }
    }
    $scope.dirEnity = function (entity) {

        entity.PriceDate = new Date(entity.PriceDate);
        $scope.goldWorldPrice = entity;

    }
    $scope.deleteEnity = function (entity) {
        swal({
            title: 'هل تريد حذف سعر الذهب؟',
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
                $scope.clearEnity();
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
    $scope.getAllOnLoad = function () {

        $q.all(
            [
                $scope.getCurrentDate(),
                $scope.getgoldWorldPriceList(),
                $scope.getgoldWorldPriceTotalCount(),
                $scope.getlastCode()
                //$scope.getAllMainItemGroupsList(),

            ])
            .then(function (allResponses) {
                //if all the requests succeeded, this will be called, and $q.all will get an
                //array of all their responses.
                //  console.log(allResponses[0].data);

                var urlParams = $location.search();
                if (urlParams.foo != null && urlParams.foo != undefined && urlParams.foo != "") {

                    goldWorldPriceService.getById(parseInt(urlParams.foo)).then(function (result) {
                        $scope.goldWorldPrice = result.data;
                        $scope.dirEnity(result.data);
                    })

                }


            }, function (error) {
                //This will be called if $q.all finds any of the requests erroring.
                var abc = error;
                var def = 99;
            });
    }

    $scope.getCurrentDate = function () {
        var date = new Date();
        $scope.goldWorldPrice.PriceDate = date;
    }

    $scope.getgoldWorldPriceTotalCount = function () {

        $scope.count().then(function (results) {
            var data = results.data;
            $scope.goldWorldPriceTotalCount = data;
            $scope.pagesCount = $scope.getPagesCount();



        }, function (error) {

            console.log(error.data.message);
        });
    }
    $scope.getgoldWorldPriceList = function () {

        $scope.get($scope.pageNum, $scope.pageSize).then(function (results) {
            var data = results.data;
            $scope.goldWorldPriceList = data;

        }, function (error) {

            console.log(error.data.message);
        });
    }

    $scope.getAllMainItemGroupsList = function () {
        goldWorldPriceService.getAllMainItemGroups().then(function (response) {
            $scope.maingoldWorldPriceList = response.data;
        })
    }

    $scope.get = function (pageNum, pageSize) {
        return goldWorldPriceService.get(pageNum, pageSize);
    }
    $scope.count = function () {
        return goldWorldPriceService.count();
    }
    $scope.add = function (entity) {
        return goldWorldPriceService.add(entity);
    }
    $scope.update = function (entity) {
        return goldWorldPriceService.update(entity);
    }
    $scope.delete = function (entity) {
        return goldWorldPriceService.delete(entity);
    }
    $scope.goldWorldPricePageLoad = function () {


        $scope.getAllOnLoad();

    }

    $scope.pageChanged = function () {
        $scope.getAllOnLoad();
        $log.log('Page changed to: ' + $scope.pageNum);
    };


    $scope.getPrices = function () {
        $scope.goldWorldPrice.KiloPrice = parseFloat($scope.goldWorldPrice.GoldPrice * 1000).toFixed(2);
        $scope.goldWorldPrice.OuncePrice = parseFloat($scope.goldWorldPrice.GoldPrice * 31.10).toFixed(2);
    }

    $scope.changeKilo = function () {
        $scope.goldWorldPrice.GoldPrice = parseFloat($scope.goldWorldPrice.KiloPrice / 1000).toFixed(2);
        $scope.goldWorldPrice.OuncePrice = parseFloat($scope.goldWorldPrice.GoldPrice * 31.10).toFixed(2);
    }


    $scope.changeOunce = function () {
        $scope.goldWorldPrice.GoldPrice = parseFloat($scope.goldWorldPrice.OuncePrice / 31.10).toFixed(2);
        $scope.goldWorldPrice.KiloPrice = parseFloat($scope.goldWorldPrice.GoldPrice * 1000).toFixed(2);
    }

    //////////////////get last code

    $scope.getlastCode = function () {
        var urlParams = $location.search();
        if (urlParams.foo != null && urlParams.foo != undefined && urlParams.foo != "") { } else {
            goldWorldPriceService.getLastCode().then(function (result) {
                $scope.goldWorldPrice.Code = parseInt(result.data) + 1;
            }, function (error) { });
        }
    }
}]);