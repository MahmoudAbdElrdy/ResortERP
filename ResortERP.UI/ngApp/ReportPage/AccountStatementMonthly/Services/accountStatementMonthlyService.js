'use strict';
app.factory('accountStatementMonthlyService', ['$http', 'sharedService', function ($http, sharedService) {

    var serviceBase = sharedService.getBaseUrl();

    var accountStatementServiceFactory = {};

    var _getReportResult = function (companyBranches, Accounts, startDate, endDate) {
        var myUrl = serviceBase + 'billMaster/getAccountStatementMonthlyReport?companyBranches=' + companyBranches + '&Accounts=' + Accounts + "&StartDate=" + startDate + "&EndDate=" + endDate;
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    accountStatementServiceFactory.getReportResult = _getReportResult;
    
    return accountStatementServiceFactory;

}]);