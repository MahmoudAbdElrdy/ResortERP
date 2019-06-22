'use strict';
app.factory('generalLedgerGoldService', ['$http', 'sharedService', function ($http, sharedService) {

    var serviceBase = sharedService.getBaseUrl();

    var generalLedgerGoldServiceFactory = {};


    var _getByAccounts = function (companyBranches, Accounts, CostCenterID, Source ,startDate, endDate) {
        var myUrl = serviceBase + 'billMaster/get_PRC_RPT_LEDGER_All_Gold?companyBranches=' + companyBranches +'&Accounts=' + Accounts + "&CostCenterId=" + CostCenterID + "&Source=" + Source + "&StartDate=" + startDate + "&EndDate=" + endDate;
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    generalLedgerGoldServiceFactory.getByAccounts = _getByAccounts;

    return generalLedgerGoldServiceFactory;

}]);