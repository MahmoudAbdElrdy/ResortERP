'use strict';
app.factory('billSettingService', ['$http', 'sharedService', function ($http, sharedService) {

    var serviceBase = sharedService.getBaseUrl();

    var billSettingServiceFactory = {};

    var _get = function (pageNum, pageSize) {
        var myUrl = serviceBase + 'billSettings/get?pageNum=' + pageNum + '&pageSize=' + pageSize;
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    var _getAll = function () {
        var myUrl = serviceBase + 'billSettings/getAll';
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    var _getByID = function (typeID) {
        var myUrl = serviceBase + 'billSettings/getBillSettingByID?typeID=' + typeID;
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    var _count = function () {
        var myUrl = serviceBase + 'billSettings/count';
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    var _update = function (entity) {
        var myUrl = serviceBase + 'billSettings/update';
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
        var myUrl = serviceBase + 'billSettings/delete';
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
        var myUrl = serviceBase + 'billSettings/insert';
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

    var _chkExist = function (entity) {
        var myUrl = serviceBase + 'billSettings/chkExist';
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

    var _getBillSettingByBillID = function (BILL_SETTING_ID) {
        var myUrl = serviceBase + 'billSettings/getBillSettingByBillID?BILL_SETTING_ID=' + BILL_SETTING_ID;
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    billSettingServiceFactory.get = _get;
    billSettingServiceFactory.count = _count;
    billSettingServiceFactory.insert = _insert;
    billSettingServiceFactory.update = _update;
    billSettingServiceFactory.delete = _delete;
    billSettingServiceFactory.getAll = _getAll;
    billSettingServiceFactory.getByID = _getByID;
    billSettingServiceFactory.chkExist = _chkExist;
    billSettingServiceFactory.getBillSettingByBillID = _getBillSettingByBillID;
    return billSettingServiceFactory;

}]);