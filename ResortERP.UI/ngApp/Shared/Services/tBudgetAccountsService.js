'use strict';
app.factory('tBudgetAccountsService', ['$http', 'sharedService', function ($http, sharedService) {

    var serviceBase = sharedService.getBaseUrl();

    var tBudAccServiceFactory = {};

    var _get = function (pageNum, pageSize) {
        var myUrl = serviceBase + 'tBudAcc/get?pageNum=' + pageNum + '&pageSize=' + pageSize;
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    var _getByUID = function (userName) {
        var myUrl = serviceBase + 'tBudAcc/getByUID/' + userName;
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    var _getAll = function () {
        var myUrl = serviceBase + 'tBudAcc/getAll';
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    var _count = function () {
        var myUrl = serviceBase + 'tBudAcc/count';
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    var _update = function (entity) {
        var myUrl = serviceBase + 'tBudAcc/update';
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
        var myUrl = serviceBase + 'tBudAcc/delete';
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
        var myUrl = serviceBase + 'tBudAcc/insert';
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

    tBudAccServiceFactory.get = _get;
    tBudAccServiceFactory.getByUserName = _getByUID;
    tBudAccServiceFactory.count = _count;
    tBudAccServiceFactory.insert = _insert;
    tBudAccServiceFactory.update = _update;
    tBudAccServiceFactory.delete = _delete;
    tBudAccServiceFactory.getAll = _getAll;

    return tBudAccServiceFactory;

}]);