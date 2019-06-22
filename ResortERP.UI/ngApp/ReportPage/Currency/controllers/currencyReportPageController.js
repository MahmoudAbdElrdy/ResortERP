'use strict';
app.controller('currencyReportPageController', ['$scope', '$location', '$log', '$q', 'currencyService', function ($scope, $location, $log, $q, currencyService) {

    $scope.currency = {};
    $scope.currencyCount = 0;
    $scope.pageNum = 1;
    $scope.pageSize = 10;
    $scope.pagesCount = 0;
    $scope.maxSize = 5;
    $scope.count = 0;

    $scope.getPagesCount = function () {
        var div = Math.floor($scope.currencyCount / $scope.pageSize);
        var rem = $scope.currencyCount % $scope.pageSize;
        if (rem > 0) {
            div = div + 1;
        }
        return div;
    }
    $scope.getcurrencyTotalCount = function (count) {
        $scope.currencyCount = count;
        $scope.pagesCount = $scope.getPagesCount();
    }

    $scope.clearEnity = function () {
        $scope.currency = { CURRENCY_ID: null, CURRENCY_CODE: null, CURRENCY_AR_NAME: null, CURRENCY_EN_NAME: null, CURRENCY_SUB_AR_NAME: null, CURRENCY_SUB_EN_NAME: null, CURRENCY_SYMBOL_AR_NAME: null, CURRENCY_SYMBOL_EN_NAME: null, CURRENCY_RATE: null, SUB_TO_CURRENCY_TRANS: null, CURRENCY_FIXED_RATE: null };
        $scope.currencyList = {};
        $scope.currencyCount = 0;
    }

    $scope.GetSearchResult = function () {
        if ($scope.currency !== undefined && $scope.currency !== null && ($scope.currency.CURRENCY_CODE !== null && $scope.currency.CURRENCY_CODE !== undefined) || ($scope.currency.CURRENCY_AR_NAME !== null && $scope.currency.CURRENCY_AR_NAME !== undefined) || ($scope.currency.CURRENCY_EN_NAME !== null && $scope.currency.CURRENCY_EN_NAME !== undefined) || ($scope.currency.CURRENCY_SUB_AR_NAME !== null && $scope.currency.CURRENCY_SUB_AR_NAME !== undefined) || ($scope.currency.CURRENCY_SUB_EN_NAME !== null && $scope.currency.CURRENCY_SUB_EN_NAME !== undefined) || ($scope.currency.CURRENCY_RATE !== null && $scope.currency.CURRENCY_RATE !== undefined))
            currencyService.getSearchResult($scope.currency, $scope.pageNum, $scope.pageSize).then(function (result) {
                $scope.count = $scope.getcurrencyTotalCount(result.data.length);
                return $scope.currencyList = result.data;
            }, function (error) { console.log(error.data.message); });
        else swal("عفوا", "لابد من البحث باحد الحقول علي الاقل", "error");
    }

}]);