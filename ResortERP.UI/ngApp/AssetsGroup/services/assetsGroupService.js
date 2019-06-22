'use strict';
app.factory('assetsGroupService', ['$http', 'sharedService', function ($http, sharedService) {

    var serviceBase = sharedService.getBaseUrl();
    var assetGroupsServiceFactory = {};


    var _get = function (pageNum, pageSize) {
        var myUrl = serviceBase + 'assetGroup/getPaging?pageNum=' + pageNum + '&pageSize=' + pageSize;

        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    var _AssetGroupsList= function () {
        var myUrl = serviceBase + 'assetGroup/AssetGroupsList';

        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }
    
    var _count = function () {
        var myUrl = serviceBase + 'assetGroup/count';

        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    var _getByID = function (ID) {
        var myUrl = serviceBase + 'assetGroup/getById?ID=' + itemID;

        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    var _update = function (entity) {
        var myUrl = serviceBase + 'assetGroup/update';
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
        var myUrl = serviceBase + 'assetGroup/delete';
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
    var _add = function (entity) {
        debugger;
        var myUrl = serviceBase + 'assetGroup/insert';
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
        var myUrl = serviceBase + 'assetGroup/getLastCode';

        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    assetGroupsServiceFactory.get = _get;
    assetGroupsServiceFactory.count = _count;
    assetGroupsServiceFactory.add = _add;
    assetGroupsServiceFactory.update = _update;
    assetGroupsServiceFactory.delete = _delete;

    assetGroupsServiceFactory.getLastCode = _getLastCode;
    assetGroupsServiceFactory.getByID = _getByID;
    assetGroupsServiceFactory.AssetGroupsList = _AssetGroupsList;
    return assetGroupsServiceFactory;

}]);