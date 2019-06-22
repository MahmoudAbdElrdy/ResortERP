'use strict';
app.factory('itemStatusService', ['$http', 'sharedService', function ($http, sharedService) {

    var serviceBase = sharedService.getBaseUrl();

    var itemStatusServiceFactory = {};


    var _get = function (pageNum, pageSize) {
        var myUrl = serviceBase + 'itemStatus/get?pageNum=' + pageNum + '&pageSize=' + pageSize;

        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    var _getById = function (ID) {
        var myUrl = serviceBase + 'itemStatus/getById?ID=' + ID;

        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    var _count = function () {
        var myUrl = serviceBase + 'itemStatus/count';

        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }
    var _update = function (entity) {
        var myUrl = serviceBase + 'itemStatus/update';
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
        var myUrl = serviceBase + 'itemStatus/delete';
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
        var myUrl = serviceBase + 'itemStatus/insert';
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
        var myUrl = serviceBase + 'itemStatus/getLastCode';
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    itemStatusServiceFactory.get = _get;
    itemStatusServiceFactory.count = _count;
    itemStatusServiceFactory.insert = _insert;
    itemStatusServiceFactory.update = _update;
    itemStatusServiceFactory.delete = _delete;
    itemStatusServiceFactory.getLastCode = _getLastCode;
    itemStatusServiceFactory.getById = _getById;
    return itemStatusServiceFactory;
}]);