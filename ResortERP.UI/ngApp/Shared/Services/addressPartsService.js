'use strict'
app.factory('addressPartsService', ['$http', 'sharedService', function ($http, sharedService) {


    var serviceBase = sharedService.getBaseUrl();
    var addPartsFactory = {};

    var _get = function (pageNum, pageSize) {
        var myUrl = serviceBase + 'addressPart/get';
        return $http.get(myUrl).then(function (result) {
            return result;
        }, function (error) {
        });
    }

    var _count = function () {
        var myUrl = serviceBase + 'addressPart/count';
        return $http.get(myUrl).then(function (result) {
            return result
        });
    }

    var _add = function (entity) {
        var myUrl = serviceBase + 'addressPart/insert';
        var data = entity;
        return $http({
            method: 'POST',
            url: myUrl,
            data: JSON.stringify(data)
        }).then(function (result) {
            return result;
        });
    }

    var _update = function (entity) {
        var myUrl = serviceBase + 'addressPart/update';
        var data = entity;
        return $http({
            method: 'POST',
            url: myUrl,
            data: JSON.stringify(data)
        }).then(function (result) {
            return result;
        });
    }


    var _delete = function (entity) {
        var myUrl = serviceBase + 'addressPart/delete';
        var data = entity;
        return $http({
            method: 'POST',
            url: myUrl,
            data: JSON.stringify(data)
        }).then(function (result) {
            return result;
        });
    }


    var _getByUserName = function (userName) {
        var myUrl = serviceBase + 'addressPart/getByName?UID=' + userName;
        return $http.get(myUrl).then(function (result) {
            return result;
        }, function (error) {
        });
    }
    

    addPartsFactory.get = _get;
    addPartsFactory.add = _add;
    addPartsFactory.update = _update;
    addPartsFactory.delete = _delete;
    addPartsFactory.count = _count;
    addPartsFactory.getByUserName = _getByUserName;

    return addPartsFactory;
}]);