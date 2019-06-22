'use strict';
app.factory('generalLedgerService', ['$http', 'sharedService', function ($http, sharedService) {

    var serviceBase = sharedService.getBaseUrl();

    var trialBalanceServiceFactory = {};


    var _getByAccounts = function (companyBranches, Accounts, CostCenterID, startDate, endDate) {
        var myUrl = serviceBase + 'billMaster/get_PRC_RPT_LEDGER_All?companyBranches=' + companyBranches +'&Accounts=' + Accounts + "&CostCenterId=" + CostCenterID + "&StartDate=" + startDate + "&EndDate=" + endDate;
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    trialBalanceServiceFactory.getByAccounts = _getByAccounts;

    return trialBalanceServiceFactory;

}]);