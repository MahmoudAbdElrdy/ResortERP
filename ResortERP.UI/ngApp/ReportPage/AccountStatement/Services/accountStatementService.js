'use strict';
app.factory('accountStatementService', ['$http', 'sharedService', function ($http, sharedService) {

    var serviceBase = sharedService.getBaseUrl();

    var accountStatementServiceFactory = {};


    var _getByID = function (accID) {
        var myUrl = serviceBase + 'billMaster/getAccountStatement?AccID=' + accID;
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    var _getByAccounts = function (companyBranches, Accounts, startDate, endDate) {
        var myUrl = serviceBase + 'billMaster/get_`?companyBranches=' + companyBranches +'&Accounts=' + Accounts + "&StartDate=" + startDate + "&EndDate=" + endDate;
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    var _getByAccountsGold = function (companyBranches, Accounts, startDate, endDate) {
        var myUrl = serviceBase + 'billMaster/get_PRC_RPT_LEDGER_Gold?companyBranches=' + companyBranches +'&Accounts=' + Accounts + "&StartDate=" + startDate + "&EndDate=" + endDate;
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    accountStatementServiceFactory.getByID = _getByID;

    accountStatementServiceFactory.getByAccounts = _getByAccounts;

    accountStatementServiceFactory.getByAccountsGold = _getByAccountsGold;

    return accountStatementServiceFactory;

}]);