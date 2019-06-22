'use strict';
app.factory('billGridColumn', ['$http', 'sharedService', function ($http, sharedService) {

    var serviceBase = sharedService.getBaseUrl();

    var bilGrdServiceFactory = {};

    var _get = function (pageNum, pageSize) {
        var myUrl = serviceBase + 'billGridColumns/get?pageNum=' + pageNum + '&pageSize=' + pageSize;
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    var _getAll = function () {
        var myUrl = serviceBase + 'billGridColumns/getAll';
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    var _count = function () {
        var myUrl = serviceBase + 'billGridColumns/count';
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    var _update = function (entity) {
        var myUrl = serviceBase + 'billGridColumns/update';
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
        var myUrl = serviceBase + 'billGridColumns/delete';
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
        var myUrl = serviceBase + 'billGridColumns/insert';
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


    var _getBySettingID = function (settingId) {
        var myUrl = serviceBase + 'billGridColumns/getBysettingId?settingId=' + settingId ;
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }


    bilGrdServiceFactory.get = _get;
    bilGrdServiceFactory.count = _count;
    bilGrdServiceFactory.insert = _insert;
    bilGrdServiceFactory.update = _update;
    bilGrdServiceFactory.delete = _delete;
    bilGrdServiceFactory.getAll = _getAll;
    bilGrdServiceFactory.getBySettingID = _getBySettingID;

    return bilGrdServiceFactory;

}]);