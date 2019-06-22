'use strict';
app.factory('ReportsService', ['$http', 'sharedService', function ($http, sharedService) {

    var serviceBase = sharedService.getBaseUrl();

    var ReportsServiceFactory = {};

    var _getBranchesList = function () {
        var myUrl = serviceBase + 'Reports/getBranchesList';
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    var _getCostCenterList = function () {
        var myUrl = serviceBase + 'Reports/getCostCenterList';
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    var _getStoresList = function () {
        var myUrl = serviceBase + 'Reports/getStoresList';
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    var _getItemGroupList = function () {
        var myUrl = serviceBase + 'Reports/getItemGroupList';
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    var _getSellTypeList = function () {
        var myUrl = serviceBase + 'Reports/getSellTypeList';
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    var _getItemsInventoryReportResult = function (searchObject) {
        var myUrl = serviceBase + 'Reports/getItemsInventoryReportResult';
        return $http.post(myUrl, searchObject).then(function (results) {
            return results;
        });
    }

    var _getItemsInventoryBalanceResult = function (searchObject) {
        var myUrl = serviceBase + 'Reports/getItemsInventoryBalanceResult';
        return $http.post(myUrl, searchObject).then(function (results) {
            return results;
        });
    }

    var _getAccountBalancesReportResult = function (searchObject) {
        var myUrl = serviceBase + 'Reports/getAccountBalancesReportResult';
        return $http.post(myUrl, searchObject).then(function (results) {
            return results;
        });
    }

    var _getAccountsByType = function (Type) {
        var myUrl = serviceBase + 'Reports/getAccountsByType?Type='+Type;
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    ReportsServiceFactory.getBranchesList = _getBranchesList;
    ReportsServiceFactory.getCostCenterList = _getCostCenterList;
    ReportsServiceFactory.getStoresList = _getStoresList;
    ReportsServiceFactory.getItemGroupList = _getItemGroupList;
    ReportsServiceFactory.getSellTypeList = _getSellTypeList; 
    ReportsServiceFactory.getItemsInventoryReportResult = _getItemsInventoryReportResult;
    ReportsServiceFactory.getItemsInventoryBalanceResult = _getItemsInventoryBalanceResult;
    ReportsServiceFactory.getAccountBalancesReportResult = _getAccountBalancesReportResult;
    ReportsServiceFactory.getAccountsByType = _getAccountsByType;

    return ReportsServiceFactory;

}]);