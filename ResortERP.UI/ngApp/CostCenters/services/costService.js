'use strict'
app.factory('costService', ['$http', 'sharedService', function ($http, sharedService) {
    var serviceBase = sharedService.getBaseUrl();
    var costServiceFactory = {};

    var _get = function (pageNum, pageSize) {
        var myUrl = serviceBase + 'costCenters/get?pageNum=' + pageNum + '&pageSize=' + pageSize;
        return $http.get(myUrl).then(function (result) {

            return result;
        }, function (error) {
        });
    }

    var _getById = function (COST_CENTER_ID) {
        var myUrl = serviceBase + 'costCenters/getById?COST_CENTER_ID=' + COST_CENTER_ID;
        return $http.get(myUrl).then(function (result) {

            return result;
        }, function (error) {
        });
    }

    var _getMainCostCenters = function () {

        var myUrl = serviceBase + 'costCenters/getMainCostCenters';
        return $http.get(myUrl).then(function (result) {
            return result;
        }, function (error) {

        });
    }


    var _add = function (entity) {
        debugger;
        var myUrl = serviceBase + 'costCenters/insert';
        var data = {};
        data = entity;
        return $http({
            method: 'POST',
            url: myUrl,
            data: JSON.stringify(data)
        }).then(function (result) {
            return result;
        }, function (error) {
        });
    }

    var _update = function (entity) {
        debugger;
        var myUrl = serviceBase + 'costCenters/update';
        var data = {};
        data = entity;
        return $http({
            method: 'POST',
            url: myUrl,
            data: JSON.stringify(data)
        }).then(function (result) {
            return result;

        }, function (error) {
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
        }).then(function (result) {
            return result;
        }, function (error) {
        });
    }


    var _count = function () {
        var myUrl = serviceBase + 'costCenters/count';
        return $http.get(myUrl).then(function (result) {
            return result;
        }, function (error) {
        });
    }


    var _getLastCode = function () {
        var myUrl = serviceBase + 'costCenters/getLastCode';
        return $http.get(myUrl).then(function (result) {
            return result;
        }, function (error) { });
    }

    costServiceFactory.getMainCostCenters = _getMainCostCenters;
    costServiceFactory.get = _get;
    costServiceFactory.add = _add;
    costServiceFactory.update = _update;
    costServiceFactory.delete = _delete;
    costServiceFactory.count = _count;
    costServiceFactory.getLastCode = _getLastCode;
    costServiceFactory.getById =_getById;
    return costServiceFactory;
}]);