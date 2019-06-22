'use strict';
app.factory('headerService', ['$http', 'sharedService', function ($http, sharedService) {

    var serviceBase = sharedService.getBaseUrl();

    var headerFactory = {};

    var _getLogo = function () {
        var myUrl = serviceBase + 'uidView/getLogo';
        return $http.get(myUrl).then(
            function (results) {
                return results;
            });
    }

    var _changeLanguage = function (userName, UserID, LanguageID) {
        var myUrl = serviceBase + 'User/changeLanguage';
        var data = { userName: userName, userID: UserID, languageID: LanguageID };
        return $http({
            method: 'POST',
            url: myUrl,
            params: data
        }).then(function (results) {
            return results;
        });
    }

    headerFactory.GetLogo = _getLogo;
    headerFactory.changeLanguage = _changeLanguage;

    return headerFactory;
}]);