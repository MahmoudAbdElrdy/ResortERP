'use strict';
app.factory('CSIGService', ['$http', 'sharedService', function ($http, sharedService) {

    var serviceBase = sharedService.getBaseUrl();

    var CSIGServiceFactory = {};

    var _get = function (pageNum, pageSize, CompanyStoreID) {
        var myUrl = serviceBase + 'csig/get?pageNum=' + pageNum + '&pageSize=' + pageSize+'&CompanyStoreID=' + CompanyStoreID;
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    var _getAll = function (CompanyStoreID) {
        var myUrl = serviceBase + 'csig/getAll';
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    var _count = function (CompanyStoreID) {
        var myUrl = serviceBase + 'csig/count?CompanyStoreID=' + CompanyStoreID;
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    var _update = function (entity) {
        var myUrl = serviceBase + 'csig/update';
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
        var myUrl = serviceBase + 'csig/delete';
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
        var myUrl = serviceBase + 'csig/add';
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

    CSIGServiceFactory.get = _get;
    CSIGServiceFactory.getAll = _getAll;
    CSIGServiceFactory.count = _count;
    CSIGServiceFactory.insert = _insert;
    CSIGServiceFactory.update = _update;
    CSIGServiceFactory.delete = _delete;

    return CSIGServiceFactory;

}]);