'use strict';
app.factory('accountsGroupService', ['$http', 'sharedService', function ($http, sharedService) {

    var serviceBase = sharedService.getBaseUrl();
    var accountsGroupServiceFactory = {};


    var _get = function (pageNum, pageSize) {
        var myUrl = serviceBase + 'ACCOUNTS_GROUP/get?pageNum=' + pageNum + '&pageSize=' + pageSize;

        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }
    var _count = function () {
        var myUrl = serviceBase + 'ACCOUNTS_GROUP/count';

        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    var _getByID = function (itemID) {
        var myUrl = serviceBase + 'ACCOUNTS_GROUP/getByID?itemID=' + itemID;

        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    var _update = function (entity) {
        var myUrl = serviceBase + 'ACCOUNTS_GROUP/update';
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
        var myUrl = serviceBase + 'ACCOUNTS_GROUP/delete';
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
        var myUrl = serviceBase + 'ACCOUNTS_GROUP/add';
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
        var myUrl = serviceBase + 'ACCOUNTS_GROUP/getMainACCOUNTS_GROUP';

        return $http.get(myUrl).then(function (results) {
            return results;
        })
    }



    var _getItemGroupByID = function (groupID) {
        var myUrl = serviceBase + 'ACCOUNTS_GROUP/getACCOUNTS_GROUPByID?groupID=' + groupID;
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }


    var _getLastCode = function () {
        var myUrl = serviceBase + 'ACCOUNTS_GROUP/getLastCode';

        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    accountsGroupServiceFactory.get = _get;
    accountsGroupServiceFactory.count = _count;
    accountsGroupServiceFactory.add = _add;
    accountsGroupServiceFactory.update = _update;
    accountsGroupServiceFactory.delete = _delete;
    accountsGroupServiceFactory.getAllMainItemGroups = _getAllMainItemGroups;
    accountsGroupServiceFactory.getItemGroupByID = _getItemGroupByID;
    accountsGroupServiceFactory.getLastCode = _getLastCode;
    accountsGroupServiceFactory.getByID = _getByID;
    return accountsGroupServiceFactory;

}]);