'use strict';
app.factory('entryShowOptionService', ['$http', 'sharedService', function ($http, sharedService) {

    var serviceBase = sharedService.getBaseUrl();

    var entryShowOptServiceFactory = {};

    var _get = function (pageNum, pageSize) {
        var myUrl = serviceBase + 'entryShowOption/get?pageNum=' + pageNum + '&pageSize=' + pageSize;
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    var _getAll = function () {
        var myUrl = serviceBase + 'entryShowOption/getAll';
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    var _count = function () {
        var myUrl = serviceBase + 'entryShowOption/count';
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    var _update = function (entity) {
        var myUrl = serviceBase + 'entryShowOption/update';
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
        var myUrl = serviceBase + 'entryShowOption/delete';
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
        var myUrl = serviceBase + 'entryShowOption/insert';
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

    entryShowOptServiceFactory.get = _get;
    entryShowOptServiceFactory.count = _count;
    entryShowOptServiceFactory.insert = _insert;
    entryShowOptServiceFactory.update = _update;
    entryShowOptServiceFactory.delete = _delete;
    entryShowOptServiceFactory.getAll = _getAll;

    return entryShowOptServiceFactory;

}]);