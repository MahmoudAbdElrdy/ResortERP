'use strict'
app.factory('bankService', ['$http', 'sharedService', function ($http, sharedService) {

    
    var serviceBase = sharedService.getBaseUrl();
    var bankFactory = {};

    var _get = function (pageNum, pageSize) {
        var myUrl = serviceBase + 'bank/gatBanks?pageNum=' + pageNum + '&pageSize=' + pageSize;
        return $http.get(myUrl).then(function (result) {
            return result;
        }, function (error) {
        });
    }
    var _getAll = function () {
        var myUrl = serviceBase + 'bank/getAll';
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }
    var _getSearchResult = function (entity, pageNum, pageSize) {
        var myUrl = serviceBase + 'bank/getSearchResult';
        var data = { bank: entity, pageNum: pageNum, pageSize: pageSize };
        return $http({
            method: 'POST',
            url: myUrl,
            data: JSON.stringify(data)
        }).then(function (results) {
            return results;
        }, function (error) { console.log(error.data.message); });
    }

    var _getById = function (ID) {
        var myUrl = serviceBase + 'bank/getById?ID=' + ID;
        return $http.get(myUrl).then(function (result) {
            return result;
        }, function (error) {
        });
    }

    var _count = function () {
        var myUrl = serviceBase + 'bank/countBanks';
        return $http.get(myUrl).then(function (result) {
            return result
        });
    }

    var _add = function (entity) {
        var myUrl = serviceBase + 'bank/insertBank';
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
        var myUrl = serviceBase + 'bank/updateBank';
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
        var myUrl = serviceBase + 'bank/deleteBank';
        var data = entity;
        return $http({
            method: 'POST',
            url: myUrl,
            data: JSON.stringify(data)
        }).then(function (result) {
            return result;
        });
    }


    var _getLastCode = function () {
        var myUrl = serviceBase + 'bank/getLastCode';
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }


    bankFactory.get = _get;
    bankFactory.getById = _getById;
    bankFactory.add = _add;
    bankFactory.update = _update;
    bankFactory.delete = _delete;
    bankFactory.count = _count;
    bankFactory.getLastCode = _getLastCode;

    return bankFactory;
}]);