'use strict';
app.factory('userPriviligeBranchesService', ['$http', 'sharedService', function ($http, sharedService) {

    var serviceBase = sharedService.getBaseUrl();

    var userPriviligeBranchesServiceFactory = {};

    var _get = function (pageNum, pageSize) {
        var myUrl = serviceBase + 'UserPriviligeBranchesServices/get?pageNum=' + pageNum + '&pageSize=' + pageSize;
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    var _count = function () {
        var myUrl = serviceBase + 'UserPriviligeBranchesServices/count';
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    var _update = function (entity) {
        var myUrl = serviceBase + 'UserPriviligeBranchesServices/update';
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
        var myUrl = serviceBase + 'UserPriviligeBranchesServices/delete';
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
        var myUrl = serviceBase + 'UserPriviligeBranchesServices/insert';
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

    var _getAllUserPrivBranches = function () {
        var myUrl = serviceBase + 'UserPriviligeBranchesServices/getAll';
        return $http.get(myUrl).then(function (results) {
            return results;
        })
    }


    var _getLastCode = function () {
        var myUrl = serviceBase + 'UserPriviligeBranchesServices/getLastCode';
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    var _getById = function (id) {
        var myUrl = serviceBase + 'UserPriviligeBranchesServices/getById?id='+id;
        var data = id;
        return $http({
            method: 'Get',
            url: myUrl,
        }).then(function (results) {
            return results;
        });
    }


    var _getByCN = function (name) {
        var myUrl = serviceBase + 'UserPriviligeBranchesServices/getByCN';
        var data = {};
        data = name;
        return $http({
            method: 'POST',
            url: myUrl,
            data: JSON.stringify(data)
        }).then(function (results) {
            return results;
        });
        //return $http.get(myUrl).then(function (results) {
        //    return results;
        //});
    }

    var _logAjaxReq = function (userName) {
        var myUrl = serviceBase + 'LogUserReq/insert';
        return $http({
            method: 'Get',
            data: userName,
            url: myUrl,
        }).then(function (results) {
            return results;
        });
    }

    userPriviligeBranchesServiceFactory.get = _get;
    userPriviligeBranchesServiceFactory.count = _count;
    userPriviligeBranchesServiceFactory.insert = _insert;
    userPriviligeBranchesServiceFactory.update = _update;
    userPriviligeBranchesServiceFactory.delete = _delete;
    userPriviligeBranchesServiceFactory.getAllUserPrivBranches = _getAllUserPrivBranches;
    userPriviligeBranchesServiceFactory.getLastCode = _getLastCode;
    userPriviligeBranchesServiceFactory.getById = _getById;
    userPriviligeBranchesServiceFactory.getByCN = _getByCN;
    userPriviligeBranchesServiceFactory.logAjaxReq = _logAjaxReq;
    return userPriviligeBranchesServiceFactory;

}]);