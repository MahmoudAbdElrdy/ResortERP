'use strict';
app.factory('trialBalanceGoldService', ['$http', 'sharedService', function ($http, sharedService) {

    var serviceBase = sharedService.getBaseUrl();

    var trialBalanceGoldServiceFactory = {};


    var _getByAccounts = function (companyBranches, startDate, endDate, type) {
        var myUrl = serviceBase + 'billMaster/Get_PRC_RPT_TrialBalance_Gold?companyBranches=' + companyBranches  + "&StartDate=" + startDate + "&EndDate=" + endDate + "&Type=" + type;
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    trialBalanceGoldServiceFactory.getByAccounts = _getByAccounts;

    return trialBalanceGoldServiceFactory;

}]);