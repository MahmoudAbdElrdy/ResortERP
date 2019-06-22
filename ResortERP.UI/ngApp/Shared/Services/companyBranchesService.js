'use strict';
app.factory('companyBranchesService', ['$http', 'sharedService', function ($http, sharedService) {

    var serviceBase = sharedService.getBaseUrl();

    var companyBranchesServiceFactory = {};

    var _get = function (pageNum, pageSize) {
        var myUrl = serviceBase + 'customerBranche/get?pageNum=' + pageNum + '&pageSize=' + pageSize;
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    var _getAll = function () {
        var myUrl = serviceBase + 'customerBranche/getAll';
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }
    var _getAllByAccID = function (accID) {
        var myUrl = serviceBase + 'customerBranche/getAllByAccID?AccID=' + accID;
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    var _count = function () {
        var myUrl = serviceBase + 'customerBranche/count';
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    var _update = function (entity) {
        var myUrl = serviceBase + 'customerBranche/update';
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
        var myUrl = serviceBase + 'customerBranche/delete';
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
        var myUrl = serviceBase + 'customerBranche/add';
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

    companyBranchesServiceFactory.get = _get;
    companyBranchesServiceFactory.count = _count;
    companyBranchesServiceFactory.insert = _insert;
    companyBranchesServiceFactory.update = _update;
    companyBranchesServiceFactory.delete = _delete;
    companyBranchesServiceFactory.getAll = _getAll;
    companyBranchesServiceFactory.getAllByAccID = _getAllByAccID;

    return companyBranchesServiceFactory;

}]);