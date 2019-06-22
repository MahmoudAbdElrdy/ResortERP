'use strict';
app.factory('emailsService', ['$http', 'sharedService', function ($http, sharedService) {

    var serviceBase = sharedService.getBaseUrl();

    var emailServiceFactory = {};

    var _get = function (pageNum, pageSize) {
        var myUrl = serviceBase + 'email/get/' + pageNum + '/' + pageSize;
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    var _getByUID = function (userID) {
        var myUrl = serviceBase + 'email/getByUID/' + userID;
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    var _getAll = function () {
        var myUrl = serviceBase + 'email/getAll';
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    var _count = function () {
        var myUrl = serviceBase + 'email/count';
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    var _update = function (entity) {
        var myUrl = serviceBase + 'email/update';
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
        var myUrl = serviceBase + 'email/delete';
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
        var myUrl = serviceBase + 'email/insert';
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

    emailServiceFactory.get = _get;
    emailServiceFactory.count = _count;
    emailServiceFactory.insert = _insert;
    emailServiceFactory.update = _update;
    emailServiceFactory.delete = _delete;
    emailServiceFactory.getAll = _getAll;
    emailServiceFactory.getByUID = _getByUID;

    return emailServiceFactory;

}]);