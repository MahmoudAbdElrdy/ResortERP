'use strict';
app.factory('trialBalanceService', ['$http', 'sharedService', function ($http, sharedService) {

    var serviceBase = sharedService.getBaseUrl();

    var trialBalanceServiceFactory = {};


    var _getByAccounts = function (companyBranches, startDate, endDate, type) {
        var myUrl = serviceBase + 'billMaster/Get_PRC_RPT_TrialBalance?companyBranches=' + companyBranches  + "&StartDate=" + startDate + "&EndDate=" + endDate + "&Type=" + type;
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    trialBalanceServiceFactory.getByAccounts = _getByAccounts;

    return trialBalanceServiceFactory;

}]);