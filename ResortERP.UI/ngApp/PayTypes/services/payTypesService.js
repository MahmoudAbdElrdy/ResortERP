'use strict'
app.factory('payTypesService', ['$http', 'sharedService', function ($http, sharedService) {


    var payTypesFactory = {};
    var serviceBase = sharedService.getBaseUrl();


    var _get = function (pageNum, pageSize) {
        // debugger;
        var myUrl = serviceBase + 'tPayTypes/get?pageNum=' + pageNum + '&pageSize=' + pageSize;
        return $http.get(myUrl).then(function (result) {
            return result;
        }, function (error) { });
    }

    var _count = function () {
        var myUrl = serviceBase + 'tPayTypes/count';
        return $http.get(myUrl).then(function (result) {
            return result;
        }, function (error) { });
    }


    var _add = function (entity) {
        var myUrl = serviceBase + 'tPayTypes/add';
        var data = entity;
        return $http({
            method: 'POST',
            url: myUrl,
            data: JSON.stringify(data),
        }).then(function (result) {
            return result;
        }, function (error) { });
    }


    var _update = function (entity) {
        var myUrl = serviceBase + 'tPayTypes/update';
        var data = {};
        var data = entity;
        return $http({
            method: 'POST',
            url: myUrl,
            data: JSON.stringify(data),
        }).then(function (result) {
            return result;
        }, function (error) { });
    }


    var _delete = function (entity) {
        var myUrl = serviceBase + 'tPayTypes/delete';
        var data = entity;
        return $http({
            method: 'POST',
            url: myUrl,
            data: JSON.stringify(data),
        }).then(function (result) {
            return result;
        }, function (error) { });
    }


    var _getAll = function () {
        // debugger;
        var myUrl = serviceBase + 'tPayTypes/getAll';
        return $http.get(myUrl).then(function (result) {
            return result;
        }, function (error) { });
    }

    var _getLastCode = function () {
        var myUrl = serviceBase + 'tPayTypes/getLastCode';
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }



    var _getByID = function (payTypeID) {
        // debugger;
        var myUrl = serviceBase + 'tPayTypes/getByID?payTypeID=' + payTypeID;
        return $http.get(myUrl).then(function (result) {
            return result;
        }, function (error) { });
    }


    var _getAllActive = function () {
        // debugger;
        var myUrl = serviceBase + 'tPayTypes/getAllActive';
        return $http.get(myUrl).then(function (result) {
            return result;
        }, function (error) { });
    }


    payTypesFactory.get = _get;
    payTypesFactory.count = _count;
    payTypesFactory.add = _add;
    payTypesFactory.update = _update;
    payTypesFactory.delete = _delete;
    payTypesFactory.getAll = _getAll;
    payTypesFactory.getLastCode = _getLastCode;
    payTypesFactory.getByID = _getByID;
    payTypesFactory.getAllActive = _getAllActive;

    return payTypesFactory;

}]);