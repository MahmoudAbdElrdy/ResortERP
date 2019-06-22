'use strict';
app.factory('PurchasingTaxReportService', ['$http', 'sharedService', function ($http, sharedService) {

    var serviceBase = sharedService.getBaseUrl();

    var accountStatementServiceFactory = {};


    var _getReportData = function (companyBranches, startDate, endDate) {
        var myUrl = serviceBase + 'billMaster/getPurchasingTaxReportData?companyBranches=' + companyBranches + '&StartDate=' + startDate + '&EndDate=' + endDate;
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    accountStatementServiceFactory.getReportData = _getReportData;

    return accountStatementServiceFactory;

}]);