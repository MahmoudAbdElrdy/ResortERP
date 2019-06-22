'use strict';
app.factory('goldWorldPriceService', ['$http', 'sharedService', function ($http, sharedService) {

    var serviceBase = sharedService.getBaseUrl();
    var goldWorldPriceServiceFactory = {};



    var _GetLastGoldWorldPrice = function () {
        var myUrl = serviceBase + 'GoldWorldPrice/GetLastGoldWorldPrice';
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    var _get = function (pageNum, pageSize) {
        var myUrl = serviceBase + 'goldWorldPrice/get?pageNum=' + pageNum + '&pageSize=' + pageSize;

        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    var _getById = function (ID) {
        var myUrl = serviceBase + 'goldWorldPrice/getById?ID=' + ID;

        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    var _count = function () {
        var myUrl = serviceBase + 'goldWorldPrice/count';

        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }



    var _update = function (entity) {
        var myUrl = serviceBase + 'goldWorldPrice/update';
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
        var myUrl = serviceBase + 'goldWorldPrice/delete';
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
    var _add = function (entity) {
        debugger;
        if (entity.Active == 'true')
            entity.Active = 1;
        else
            entity.Active = 0;

        var myUrl = serviceBase + 'goldWorldPrice/add';
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


    var _getAllMainItemGroups = function () {
        var myUrl = serviceBase + 'goldWorldPrice/getMainItemGroups';

        return $http.get(myUrl).then(function (results) {
            return results;
        })
    }

    var _getLastCode = function () {
        var myUrl = serviceBase + 'GoldWorldPrice/getLastCode';
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }


    var _getLastGoldWorldPriceData = function () {
        var myUrl = serviceBase + 'GoldWorldPrice/GetLastGoldWorldPriceData';
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }


    goldWorldPriceServiceFactory.get = _get;
    goldWorldPriceServiceFactory.count = _count;
    goldWorldPriceServiceFactory.add = _add;
    goldWorldPriceServiceFactory.update = _update;
    goldWorldPriceServiceFactory.delete = _delete;
    goldWorldPriceServiceFactory.getAllMainItemGroups = _getAllMainItemGroups;
    goldWorldPriceServiceFactory.GetLastGoldWorldPrice = _GetLastGoldWorldPrice;
    goldWorldPriceServiceFactory.getLastCode = _getLastCode;
    goldWorldPriceServiceFactory.getLastGoldWorldPriceData = _getLastGoldWorldPriceData;
    goldWorldPriceServiceFactory.getById = _getById;

    return goldWorldPriceServiceFactory;

}]);