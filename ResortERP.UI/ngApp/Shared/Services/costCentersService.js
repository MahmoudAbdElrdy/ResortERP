'use strict';
app.factory('costCentersService', ['$http', 'sharedService', function ($http, sharedService) {

    var serviceBase = sharedService.getBaseUrl();

    var costCentersServiceFactory = {};

    var _get = function (pageNum, pageSize) {
       // debugger;
        var myUrl = serviceBase + 'costCenters/get?pageNum=' + pageNum + '&pageSize=' + pageSize;
        return $http.get(myUrl).then(function (results) {            
            return results;
        });
    }
    var _getAll = function () {
        var myUrl = serviceBase + 'costCenters/getAll';
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    var _count = function () {
        var myUrl = serviceBase + 'costCenters/count';
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    var _update = function (entity) {
        var myUrl = serviceBase + 'costCenters/update';
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
        var myUrl = serviceBase + 'costCenters/delete';
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
        var myUrl = serviceBase + 'costCenters/insert';
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


    var _getMainCostCenters = function () {
        debugger;
        var myUrl = serviceBase + 'costCenters/getMainCostCenters';
        return $http.get(myUrl).then(function (result) {
            return result;
        }, function (error) {

        });
    }


    costCentersServiceFactory.get = _get;
    costCentersServiceFactory.getAll = _getAll;
    costCentersServiceFactory.count = _count;
    costCentersServiceFactory.insert = _insert;
    costCentersServiceFactory.update = _update;
    costCentersServiceFactory.delete = _delete;
    costCentersServiceFactory.getMainCostCenters = _getMainCostCenters;
    return costCentersServiceFactory;

}]);