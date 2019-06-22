'use strict';
app.factory('unitsService', ['$http', 'sharedService', function ($http, sharedService) {

    var serviceBase = sharedService.getBaseUrl();

    var unitsServiceFactory = {};


    var _get = function (pageNum, pageSize) {
        var myUrl = serviceBase + 'units/get?pageNum=' + pageNum + '&pageSize=' + pageSize;

        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }
    var _count = function () {
        var myUrl = serviceBase + 'units/count';

        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }
    var _update = function (entity) {
        var myUrl = serviceBase + 'units/update';
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
        var myUrl = serviceBase + 'units/delete';
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
        var myUrl = serviceBase + 'units/insert';
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
        var myUrl = serviceBase + 'units/getLastCode';

        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    var _getById = function (UNIT_ID) {
        var myUrl = serviceBase + 'units/getById?UNIT_ID=' + UNIT_ID;

        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    unitsServiceFactory.get = _get;
    unitsServiceFactory.count = _count;
    unitsServiceFactory.insert = _insert;
    unitsServiceFactory.update = _update;
    unitsServiceFactory.delete = _delete;
    unitsServiceFactory.getLastCode = _getLastCode;
    unitsServiceFactory.getById =_getById;
    return unitsServiceFactory;
}]);