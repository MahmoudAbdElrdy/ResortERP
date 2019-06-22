'use strict';
app.factory('tStoreService', ['$http', 'sharedService', function ($http, sharedService) {

    var serviceBase = sharedService.getBaseUrl();

    var tStoreServiceFactory = {};

    var _get = function (pageNum, pageSize) {
        var myUrl = serviceBase + 'tStore/get?pageNum=' + pageNum + '&pageSize=' + pageSize;
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    var _getByUID = function (userName) {
        var myUrl = serviceBase + 'tStore/getByUID/' + userName;
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    var _getAll = function () {
        var myUrl = serviceBase + 'tStore/getAll';
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    var _count = function () {
        var myUrl = serviceBase + 'tStore/count';
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    var _update = function (entity) {
        var myUrl = serviceBase + 'tStore/update';
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
        var myUrl = serviceBase + 'tStore/delete';
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
        var myUrl = serviceBase + 'tStore/insert';
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

    tStoreServiceFactory.get = _get;
    tStoreServiceFactory.getByUserName = _getByUID;
    tStoreServiceFactory.count = _count;
    tStoreServiceFactory.insert = _insert;
    tStoreServiceFactory.update = _update;
    tStoreServiceFactory.delete = _delete;
    tStoreServiceFactory.getAll = _getAll;

    return tStoreServiceFactory;

}]);