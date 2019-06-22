'use strict';
app.factory('itemCaliberService', ['$http', 'sharedService', function ($http, sharedService) {

    var serviceBase = sharedService.getBaseUrl();
    var itemCaliberServiceFactory = {};


    var _get = function (pageNum, pageSize) {
        var myUrl = serviceBase + 'itemCaliber/get?pageNum=' + pageNum + '&pageSize=' + pageSize;

        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }
    var _count = function () {
        var myUrl = serviceBase + 'itemCaliber/count';

        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }



    var _update = function (entity) {
        var myUrl = serviceBase + 'itemCaliber/update';
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
        var myUrl = serviceBase + 'itemCaliber/delete';
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
        if (entity.Active == 'true')
            entity.Active = 1;
        else
            entity.Active = 0;

        var myUrl = serviceBase + 'itemCaliber/add';
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


    var _getAllMainItemGroups = function () {
        var myUrl = serviceBase + 'itemCaliber/getMainItemGroups';

        return $http.get(myUrl).then(function (results) {
            return results;
        })
    }


    var _getLastCode = function () {
        var myUrl = serviceBase + 'itemCaliber/getLastCode';
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }


    var _getByID = function (caliberID) {
        var myUrl = serviceBase + 'itemCaliber/getByID?caliberId=' + caliberID;
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    var _getByIDLog = function (caliberID) {
        var myUrl = serviceBase + 'itemCaliber/getByIDLog?caliberId=' + caliberID;
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }
    var _getAll = function () {
        var myUrl = serviceBase + 'itemCaliber/getAll';
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    itemCaliberServiceFactory.get = _get;
    itemCaliberServiceFactory.count = _count;
    itemCaliberServiceFactory.add = _add;
    itemCaliberServiceFactory.update = _update;
    itemCaliberServiceFactory.delete = _delete;
    itemCaliberServiceFactory.getAllMainItemGroups = _getAllMainItemGroups;
    itemCaliberServiceFactory.getLastCode = _getLastCode;
    itemCaliberServiceFactory.getByID = _getByID;
    itemCaliberServiceFactory.getByIDLog = _getByIDLog;
    itemCaliberServiceFactory.getAll = _getAll;
    return itemCaliberServiceFactory;

}]);