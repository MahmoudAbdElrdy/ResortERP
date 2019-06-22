'use strict';
app.factory('itemsGroupsService', ['$http', 'sharedService', function ($http, sharedService) {

    var serviceBase = sharedService.getBaseUrl();
    var itemsGroupsServiceFactory = {};


    var _get = function (pageNum, pageSize) {
        var myUrl = serviceBase + 'ItemsGroups/get?pageNum=' + pageNum + '&pageSize=' + pageSize;

        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }
    var _count = function () {
        var myUrl = serviceBase + 'ItemsGroups/count';

        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    var _getByID = function (itemID) {
        var myUrl = serviceBase + 'ItemsGroups/getByID?itemID=' + itemID;

        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    var _update = function (entity) {
        var myUrl = serviceBase + 'ItemsGroups/update';
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
        var myUrl = serviceBase + 'ItemsGroups/delete';
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
        var myUrl = serviceBase + 'ItemsGroups/add';
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
        var myUrl = serviceBase + 'ItemsGroups/getMainItemGroups';

        return $http.get(myUrl).then(function (results) {
            return results;
        })
    }


    var _getAllCostCalculationType = function () {
        var myUrl = serviceBase + 'costCalculationType/getCostCalculationType';

        return $http.get(myUrl).then(function (results) {
            return results;
        })
    }



    var _getItemGroupByID = function (groupID) {
        var myUrl = serviceBase + 'ItemsGroups/getItemGroupByID?groupID=' + groupID;
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }


    var _getLastCode = function () {
        var myUrl = serviceBase + 'ItemsGroups/getLastCode';

        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    itemsGroupsServiceFactory.get = _get;
    itemsGroupsServiceFactory.count = _count;
    itemsGroupsServiceFactory.add = _add;
    itemsGroupsServiceFactory.update = _update;
    itemsGroupsServiceFactory.delete = _delete;
    itemsGroupsServiceFactory.getAllMainItemGroups = _getAllMainItemGroups;
    itemsGroupsServiceFactory.getAllCostCalculationType = _getAllCostCalculationType;
    itemsGroupsServiceFactory.getItemGroupByID = _getItemGroupByID;
    itemsGroupsServiceFactory.getLastCode = _getLastCode;
    itemsGroupsServiceFactory.getByID = _getByID;
    return itemsGroupsServiceFactory;

}]);