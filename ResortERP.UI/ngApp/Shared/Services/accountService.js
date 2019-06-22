'use strict';
app.factory('accountService', ['$http', 'sharedService', function ($http, sharedService) {

    var serviceBase = sharedService.getBaseUrl();

    var accountServiceFactory = {};

    var _get = function (pageNum, pageSize) {
        var myUrl = serviceBase + 'Acounts/get?pageNum=' + pageNum + '&pageSize=' + pageSize;
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    var _getByID = function (AccountID) {
        var myUrl = serviceBase + 'Acounts/getByID?AccountID=' + AccountID;
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    var _getSearchForAccount = function (Name, pageNumA, pageSizeA) {
        var myUrl = serviceBase + 'Acounts/getSearchForAccount?accountName=' + Name + '&pageNumA=' + pageNumA + '&pageSizeA=' + pageSizeA;
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    var _getSearch = function (search) {
        var myUrl = serviceBase + 'Acounts/getSearch?search=' + search;
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    var _getAll = function () {
        var myUrl = serviceBase + 'Acounts/getAll';
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    var _GetAllCustomerAccounts = function () {
        var myUrl = serviceBase + 'Acounts/GetAllCustomerAccounts';
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    var _count = function () {
        var myUrl = serviceBase + 'Acounts/count';
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    var _countSearch = function (search) {
        var myUrl = serviceBase + 'Acounts/getSearchCount?accountName=' + search;
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    var _update = function (entity) {
        var myUrl = serviceBase + 'Acounts/update';
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
        var myUrl = serviceBase + 'Acounts/delete';
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
        var myUrl = serviceBase + 'Acounts/add';
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

    accountServiceFactory.get = _get;
    accountServiceFactory.getByID = _getByID;
    accountServiceFactory.getAll = _getAll;
    accountServiceFactory.count = _count;
    accountServiceFactory.countSearch = _countSearch;
    accountServiceFactory.insert = _insert;
    accountServiceFactory.update = _update;
    accountServiceFactory.delete = _delete;
    accountServiceFactory.GetAllCustomerAccounts = _GetAllCustomerAccounts;
    accountServiceFactory.getSearchForAccount = _getSearchForAccount;
    accountServiceFactory.getSearch = _getSearch;

    return accountServiceFactory;

}]);