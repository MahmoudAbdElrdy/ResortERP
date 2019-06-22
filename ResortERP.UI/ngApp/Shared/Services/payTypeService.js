'use strict';
app.factory('payTypeService', ['$http', 'sharedService', function ($http, sharedService) {

    var serviceBase = sharedService.getBaseUrl();

    var payTypeServiceFactory = {};

    var _get = function (pageNum, pageSize) {
        var myUrl = serviceBase + 'tPayTypes/get?pageNum=' + pageNum + '&pageSize=' + pageSize;
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    var _getAll = function () {
        var myUrl = serviceBase + 'tPayTypes/getAll';
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    var _count = function () {
        var myUrl = serviceBase + 'tPayTypes/count';
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    var _update = function (entity) {
        var myUrl = serviceBase + 'tPayTypes/update';
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
        var myUrl = serviceBase + 'tPayTypes/delete';
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
        var myUrl = serviceBase + 'tPayTypes/add';
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


    var _getAllActive = function () {
        // debugger;
        var myUrl = serviceBase + 'tPayTypes/getAllActive';
        return $http.get(myUrl).then(function (result) {
            return result;
        }, function (error) { });
    }



    payTypeServiceFactory.get = _get;
    payTypeServiceFactory.count = _count;
    payTypeServiceFactory.insert = _insert;
    payTypeServiceFactory.update = _update;
    payTypeServiceFactory.delete = _delete;
    payTypeServiceFactory.getAll = _getAll;
    payTypeServiceFactory.getAllActive = _getAllActive;


    return payTypeServiceFactory;

}]);