'use strict';
app.factory('TotalBillsMotionService', ['$http', 'sharedService', function ($http, sharedService) {

    var serviceBase = sharedService.getBaseUrl();

    var totalBillsMotionFactory = {};

    var _getSearchForCustomer = function (search) {
        var myUrl = serviceBase + 'customer/getSearchForCustomer?Search=' + search;
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    var _getSearch = function (search) {
        var myUrl = serviceBase + 'billMaster/GetInvoiceMotionReportSearch';
        return $http({
            url: myUrl,
            method: 'POST',
            data: JSON.stringify(search)
        }).then(function (results) {
            return results;
        }, function (error) {
            alert(error.data);
        });
    }


    totalBillsMotionFactory.getSearchForCustomer = _getSearchForCustomer;
    totalBillsMotionFactory.getSearch = _getSearch;

    return totalBillsMotionFactory;

}]);