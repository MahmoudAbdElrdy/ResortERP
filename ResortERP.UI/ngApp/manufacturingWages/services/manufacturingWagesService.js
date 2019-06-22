'use strict';
app.factory('manufacturingWagesService', ['$http', 'sharedService', function ($http, sharedService) {

    var serviceBase = sharedService.getBaseUrl();

    var manufacturingWagesServiceFactory = {};


    var _get = function (pageNum, pageSize) {
        var myUrl = serviceBase + 'manufacturingWages/get?pageNum=' + pageNum + '&pageSize=' + pageSize;

        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    var _getById = function (ID) {
        var myUrl = serviceBase + 'manufacturingWages/getById?ID=' + ID;

        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    var _count = function () {
        var myUrl = serviceBase + 'manufacturingWages/count';

        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }
    var _update = function (entity) {
        var myUrl = serviceBase + 'manufacturingWages/update';
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
        var myUrl = serviceBase + 'manufacturingWages/delete';
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
        var myUrl = serviceBase + 'manufacturingWages/insert';
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
        var myUrl = serviceBase + 'manufacturingWages/getLastCode';
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }


    manufacturingWagesServiceFactory.get = _get;
    manufacturingWagesServiceFactory.count = _count;
    manufacturingWagesServiceFactory.insert = _insert;
    manufacturingWagesServiceFactory.update = _update;
    manufacturingWagesServiceFactory.delete = _delete;
    manufacturingWagesServiceFactory.getLastCode = _getLastCode;
    manufacturingWagesServiceFactory.getById = _getById;
    return manufacturingWagesServiceFactory;
}]);