'use strict';
app.factory('chartOfAccService', ['$http', 'sharedService', function ($http, sharedService) {

    var serviceBase = sharedService.getBaseUrl();

    var accountsGuideServiceFactory = {};

    var _getToTree = function () {
        var myUrl = serviceBase + 'Acounts/buildTree';
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }
    accountsGuideServiceFactory.getToTree = _getToTree;
    return accountsGuideServiceFactory;

}]);