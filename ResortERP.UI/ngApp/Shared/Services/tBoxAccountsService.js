'use strict';
app.factory('tBoxAccountsService', ['$http', 'sharedService', function ($http, sharedService) {

    var serviceBase = sharedService.getBaseUrl();

    var tBoxAccountsServiceFactory = {};

    var _get = function (pageNum, pageSize) {
        var myUrl = serviceBase + 'tBoxAccounts/get?pageNum=' + pageNum + '&pageSize=' + pageSize;
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    var _getByUID = function (userName) {
        var myUrl = serviceBase + 'tBoxAccounts/getByUID/' + userName;
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    var _getAll = function () {
        var myUrl = serviceBase + 'tBoxAccounts/getAll';
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    var _count = function () {
        var myUrl = serviceBase + 'tBoxAccounts/count';
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    var _update = function (entity) {
        var myUrl = serviceBase + 'tBoxAccounts/update';
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
        var myUrl = serviceBase + 'tBoxAccounts/delete';
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
        var myUrl = serviceBase + 'tBoxAccounts/insert';
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

    tBoxAccountsServiceFactory.get = _get;
    tBoxAccountsServiceFactory.getByUserName = _getByUID;
    tBoxAccountsServiceFactory.count = _count;
    tBoxAccountsServiceFactory.insert = _insert;
    tBoxAccountsServiceFactory.update = _update;
    tBoxAccountsServiceFactory.delete = _delete;
    tBoxAccountsServiceFactory.getAll = _getAll;

    return tBoxAccountsServiceFactory;

}]);