'use strict';
app.factory('billProfitReportPageService', ['$http', 'sharedService', function ($http, sharedService) {

    var serviceBase = sharedService.getBaseUrl();

    var billProfitServiceFactory = {};

    var _getSearchForCustomer = function (search) {
        var myUrl = serviceBase + 'billProfit/getSearchForCustomer?Search=' + search;
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    var _getSearchForStore = function (search) {
        var myUrl = serviceBase + 'billProfit/getSearchForStore?Search=' + search;
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    var _getSearchForEmployee = function (search, type) {
        var myUrl = serviceBase + 'billProfit/getSearchForEmployee?Search=' + search + '&TypeID=' + type;
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    var _getSearchForCostCenter = function (search) {
        var myUrl = serviceBase + 'billProfit/getSearchForCostCenter?Search=' + search;
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    billProfitServiceFactory.getSearchForCustomer = _getSearchForCustomer;
    billProfitServiceFactory.getSearchForStore = _getSearchForStore;
    billProfitServiceFactory.getSearchForEmployee = _getSearchForEmployee;
    billProfitServiceFactory.getSearchForCostCenter = _getSearchForCostCenter;

    return billProfitServiceFactory;

}]);