'use strict';
app.factory('entryGridColumnService', ['$http', 'sharedService', function ($http, sharedService) {

    var serviceBase = sharedService.getBaseUrl();

    var entryGridColumnServiceFactory = {};

    var _get = function (pageNum, pageSize) {
        var myUrl = serviceBase + 'entryGrdCol/get?pageNum=' + pageNum + '&pageSize=' + pageSize;
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    var _getByID = function (entrySettingID) {
        var myUrl = serviceBase + 'entryGrdCol/getByID?EntrySettingID=' + entrySettingID;
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    var _getAll = function () {
        var myUrl = serviceBase + 'entryGrdCol/getAll';
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    var _count = function () {
        var myUrl = serviceBase + 'entryGrdCol/count';
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    var _update = function (entity) {
        var myUrl = serviceBase + 'entryGrdCol/update';
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
        var myUrl = serviceBase + 'entryGrdCol/delete';
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
        var myUrl = serviceBase + 'entryGrdCol/insert';
        var data = {};
        data = JSON.stringify(entity);
        return $http({
            method: 'POST',
            url: myUrl,
            data: data
        }).then(function (results) {
            return results;
        });
    }

    entryGridColumnServiceFactory.get = _get;
    entryGridColumnServiceFactory.getByID = _getByID;
    entryGridColumnServiceFactory.count = _count;
    entryGridColumnServiceFactory.insert = _insert;
    entryGridColumnServiceFactory.update = _update;
    entryGridColumnServiceFactory.delete = _delete;
    entryGridColumnServiceFactory.getAll = _getAll;

    return entryGridColumnServiceFactory;

}]);