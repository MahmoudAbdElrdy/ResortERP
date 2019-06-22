'use strict';
app.factory('assetCompanyDetailsService', ['$http', 'sharedService', function ($http, sharedService) {

    var serviceBase = sharedService.getBaseUrl();

    var assetCompanyDetailserviceFactory = {};

    var _get = function (pageNum, pageSize) {
        var myUrl = serviceBase + 'AssetCompanyDetails/get?pageNum=' + pageNum + '&pageSize=' + pageSize ;
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    var _getById = function (Id) {
        var myUrl = serviceBase + 'AssetCompanyDetails/getById?Id=' + Id ;
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }
    

  
    var _getAll = function () {
        var myUrl = serviceBase + 'AssetCompanyDetails/getAll';
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    var _count = function () {
        var myUrl = serviceBase + 'AssetCompanyDetails/count';
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    var _update = function (entity) {
        var myUrl = serviceBase + 'AssetCompanyDetails/update';
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
        var myUrl = serviceBase + 'AssetCompanyDetails/delete';
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
        var myUrl = serviceBase + 'AssetCompanyDetails/insert';
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

    var _getLastCode = function () {
        var myUrl = serviceBase + 'AssetCompanyDetails/getLastCode';
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    assetCompanyDetailserviceFactory.get = _get;
    assetCompanyDetailserviceFactory.getById = _getById;
    assetCompanyDetailserviceFactory.getAll = _getAll;
    assetCompanyDetailserviceFactory.count = _count;
    assetCompanyDetailserviceFactory.insert = _insert;
  
    assetCompanyDetailserviceFactory.update = _update;
    assetCompanyDetailserviceFactory.delete = _delete;

    assetCompanyDetailserviceFactory.getLastCode = _getLastCode;

    return assetCompanyDetailserviceFactory;

}]);