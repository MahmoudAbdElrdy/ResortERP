'use strict';
app.factory('shortcutService', ['$http', 'sharedService', function ($http, sharedService) {

    var serviceBase = sharedService.getBaseUrl();

    var shortcutServiceFactory = {};

    var _get = function (pageNum, pageSize) {
        var myUrl = serviceBase + 'shortcut/get?pageNum=' + pageNum + '&pageSize=' + pageSize;
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    var _getAll = function () {
        var myUrl = serviceBase + 'shortcut/getAll';
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    var _getByUID = function (userName) {
        var myUrl = serviceBase + 'shortcut/getByUID/' + userName;
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    var _count = function () {
        var myUrl = serviceBase + 'shortcut/count';
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    var _update = function (entity) {
        var myUrl = serviceBase + 'shortcut/update';
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
        var myUrl = serviceBase + 'shortcut/delete';
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
        var myUrl = serviceBase + 'shortcut/insert';
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

    shortcutServiceFactory.get = _get;
    shortcutServiceFactory.count = _count;
    shortcutServiceFactory.insert = _insert;
    shortcutServiceFactory.update = _update;
    shortcutServiceFactory.delete = _delete;
    shortcutServiceFactory.getAll = _getAll;
    shortcutServiceFactory.getByUID = _getByUID;

    return shortcutServiceFactory;

}]);