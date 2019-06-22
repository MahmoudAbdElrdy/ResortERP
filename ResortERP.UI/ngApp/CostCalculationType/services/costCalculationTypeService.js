'use strict';
app.factory('costCalculationTypeService', ['$http', 'sharedService', function ($http, sharedService) {

    var serviceBase = sharedService.getBaseUrl();

    var costCalculationTypeServiceFactory = {};


    var _get = function (pageNum, pageSize) {
        var myUrl = serviceBase + 'costCalculationType/get?pageNum=' + pageNum + '&pageSize=' + pageSize;

        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }
    var _count = function () {
        var myUrl = serviceBase + 'costCalculationType/count';

        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }
    var _update = function (entity) {
        var myUrl = serviceBase + 'costCalculationType/update';
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
        var myUrl = serviceBase + 'costCalculationType/delete';
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
        var myUrl = serviceBase + 'costCalculationType/insert';
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

    costCalculationTypeServiceFactory.get = _get;
    costCalculationTypeServiceFactory.count = _count;
    costCalculationTypeServiceFactory.insert = _insert;
    costCalculationTypeServiceFactory.update = _update;
    costCalculationTypeServiceFactory.delete = _delete;

    return costCalculationTypeServiceFactory;
}]);