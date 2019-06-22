'use strict';
app.factory('entrySettingService', ['$http', 'sharedService', function ($http, sharedService) {

    var serviceBase = sharedService.getBaseUrl();

    var entrySettingServiceFactory = {};

    var _get = function (pageNum, pageSize) {
        var myUrl = serviceBase + 'entrySetting/get?pageNum=' + pageNum + '&pageSize=' + pageSize;
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    var _count = function () {
        var myUrl = serviceBase + 'entrySetting/count';
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    var _update = function (entity) {
        var myUrl = serviceBase + 'entrySetting/update';
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
        var myUrl = serviceBase + 'entrySetting/delete';
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
        var myUrl = serviceBase + 'entrySetting/insert';
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

    var _getAll = function () {
        var myUrl = serviceBase + 'entrySetting/getAll';
        return $http.get(myUrl).then(function (results) {
            return results;
        })
    }


    var _getByID = function (typeID) {
        var myUrl = serviceBase + 'entrySetting/getEntrySettingByID/' + typeID;
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    var _getBySettingID = function (SettingID) {
        var myUrl = serviceBase + 'entrySetting/getSettingBySettingID?SettingID=' + SettingID;
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }


    entrySettingServiceFactory.get = _get;
    entrySettingServiceFactory.count = _count;
    entrySettingServiceFactory.insert = _insert;
    entrySettingServiceFactory.update = _update;
    entrySettingServiceFactory.delete = _delete;
    entrySettingServiceFactory.getAll = _getAll;
    entrySettingServiceFactory.getByID = _getByID;
    entrySettingServiceFactory.getBySettingID = _getBySettingID;

    return entrySettingServiceFactory;

}]);