'use strict';
app.factory('companyStoresService', ['$http', 'sharedService', function ($http, sharedService) {

    var serviceBase = sharedService.getBaseUrl();

    var companyStoresServiceFactory = {};

    var _get = function (pageNum, pageSize) {
        var myUrl = serviceBase + 'companyStores/get?pageNum=' + pageNum + '&pageSize=' + pageSize;
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    var _getSearch = function (search) {
        var myUrl = serviceBase + 'companyStores/searchForStores?search=' + search;
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    var _getAll = function () {
        var myUrl = serviceBase + 'companyStores/getAll';
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    var _count = function () {
        var myUrl = serviceBase + 'companyStores/count';
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    var _update = function (entity) {
        var myUrl = serviceBase + 'companyStores/update';
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
        var myUrl = serviceBase + 'companyStores/delete';
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
        var myUrl = serviceBase + 'companyStores/insert';
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

    var _getLastCode = function () {
        var myUrl = serviceBase + 'companyStores/getLastCode';
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    var _getById = function (COM_STORE_ID) {
        var myUrl = serviceBase + 'companyStores/getById?COM_STORE_ID=' + COM_STORE_ID;
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    companyStoresServiceFactory.get = _get;
    companyStoresServiceFactory.getAll = _getAll;
    companyStoresServiceFactory.getSearch = _getSearch;
    companyStoresServiceFactory.count = _count;
    companyStoresServiceFactory.insert = _insert;
    companyStoresServiceFactory.update = _update;
    companyStoresServiceFactory.delete = _delete;
    companyStoresServiceFactory.getLastCode = _getLastCode;
    companyStoresServiceFactory.getById =_getById
    return companyStoresServiceFactory;

}]);