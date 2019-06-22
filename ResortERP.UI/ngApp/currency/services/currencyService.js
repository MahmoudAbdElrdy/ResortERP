'use strict';
app.factory('currencyService', ['$http', 'sharedService', function ($http, sharedService) {

    var serviceBase = sharedService.getBaseUrl();

    var currencyServiceFactory = {};

    var _get = function (pageNum, pageSize) {
        var myUrl = serviceBase + 'currency/get?pageNum=' + pageNum + '&pageSize=' + pageSize;
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }
    var _getAll = function () {
        var myUrl = serviceBase + 'currency/getAll';
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }
    var _ExportReport = function () {
        var myUrl = serviceBase + 'currency/exportReport';
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }
    var _getBy = function (CurrencyID) {
        var myUrl = serviceBase + 'currency/getByID?currencyID=' + CurrencyID;
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    var _getCurrencyRate = function (CurrencyID) {
        var myUrl = serviceBase + 'currency/getCurrencyRate?CurrencyID=' + CurrencyID;
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    var _count = function () {
        var myUrl = serviceBase + 'currency/count';
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    var _update = function (entity) {
        var myUrl = serviceBase + 'currency/update';
        var data = {};
        data = entity;
        return $http({
            method: 'POST',
            url: myUrl,
            data: JSON.stringify(data)
        }).then(function (results) {
            return results;
        });
    }

    var _delete = function (entity) {
        var myUrl = serviceBase + 'currency/delete';
        var data = {};
        data = entity;
        return $http({
            method: 'POST',
            url: myUrl,
            data: JSON.stringify(data)
        }).then(function (results) {
            return results;
        });
    }

    var _insert = function (entity) {
        var myUrl = serviceBase + 'currency/insert';
        var data = {};
        data = entity;
        return $http({
            method: 'POST',
            url: myUrl,
            data: JSON.stringify(data)
        }).then(function (results) {
            return results;
        });
    }
    var _getSearchResult = function (entity, pageNum, pageSize) {
        var myUrl = serviceBase + 'currency/getSearchResult';
        var data = { currency: entity, pageNum: pageNum, pageSize: pageSize };
        return $http({
            method: 'POST',
            url: myUrl,
            data: JSON.stringify(data)
        }).then(function (results) {
            return results;
        }, function (error) { console.log(error.data.message); });
    }


    var _getLastCode = function () {
        var myUrl = serviceBase + 'currency/getLastCode';
        return $http.get(myUrl).then(function (result) {
            return result;
        }, function (error) { });
    }



    var _getByCurrId = function (currId) {

        var myUrl = serviceBase + 'currency/getByCurrId?CurrId=' + currId;
        return $http.get(myUrl).then(function (result) {
            return result;
        }, function (error) { });
    }




    currencyServiceFactory.get = _get;
    currencyServiceFactory.getBy = _getBy;
    currencyServiceFactory.getCurrencyRate = _getCurrencyRate;
    currencyServiceFactory.getAll = _getAll;
    currencyServiceFactory.count = _count;
    currencyServiceFactory.insert = _insert;
    currencyServiceFactory.update = _update;
    currencyServiceFactory.delete = _delete;
    currencyServiceFactory.ExportReport = _ExportReport;
    currencyServiceFactory.getSearchResult = _getSearchResult;
    currencyServiceFactory.getLastCode = _getLastCode;
    currencyServiceFactory.getByCurrId = _getByCurrId;

    return currencyServiceFactory;

}]);