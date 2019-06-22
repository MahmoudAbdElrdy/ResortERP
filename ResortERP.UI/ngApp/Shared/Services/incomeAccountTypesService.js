'use strict';
app.factory('incomeAccountTypesService', ['$http', 'sharedService', function ($http, sharedService) {

    var serviceBase = sharedService.getBaseUrl();

    var incomeAccountTypeServiceFactory = {};

    var _get = function (pageNum, pageSize) {
        var myUrl = serviceBase + 'incomeAccountType/get?pageNum=' + pageNum + '&pageSize=' + pageSize;
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    var _getAll = function () {
        var myUrl = serviceBase + 'incomeAccountType/getAll';
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    var _count = function () {
        var myUrl = serviceBase + 'incomeAccountType/count';
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    var _update = function (entity) {
        var myUrl = serviceBase + 'incomeAccountType/update';
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
        var myUrl = serviceBase + 'incomeAccountType/delete';
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
        var myUrl = serviceBase + 'incomeAccountType/add';
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

    incomeAccountTypeServiceFactory.get = _get;
    incomeAccountTypeServiceFactory.count = _count;
    incomeAccountTypeServiceFactory.insert = _insert;
    incomeAccountTypeServiceFactory.update = _update;
    incomeAccountTypeServiceFactory.delete = _delete;
    incomeAccountTypeServiceFactory.getAll = _getAll;

    return incomeAccountTypeServiceFactory;

}]);