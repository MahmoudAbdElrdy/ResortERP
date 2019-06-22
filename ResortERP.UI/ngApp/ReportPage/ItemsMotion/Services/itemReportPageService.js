'use strict';
app.factory('itemReportPageService', ['$http', 'sharedService', function ($http, sharedService) {

    var serviceBase = sharedService.getBaseUrl();

    var billProfitServiceFactory = {};

    var _getSearchForItem = function (search) {
        var myUrl = serviceBase + 'itemMotion/getSearchForItem?Search=' + search;
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }


    billProfitServiceFactory.getSearchForItem = _getSearchForItem;

    return billProfitServiceFactory;

}]);