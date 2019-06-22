'use strict';
app.factory('AccountDetectingReportService', ['$http', 'sharedService', function ($http, sharedService) {

    var serviceBase = sharedService.getBaseUrl();

    var accountStatementServiceFactory = {};


    var _getAccountReportData = function (companyBranches, startDate, endDate) {
        var myUrl = serviceBase + 'billMaster/getAccountDetectingReportData?companyBranches=' + companyBranches + '&StartDate=' + startDate + '&EndDate=' + endDate;
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    accountStatementServiceFactory.getAccountReportData = _getAccountReportData;

    return accountStatementServiceFactory;

}]);