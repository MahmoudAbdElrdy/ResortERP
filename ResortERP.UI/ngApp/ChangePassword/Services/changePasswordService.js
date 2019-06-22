'use strict';
app.factory('changePasswordService', ['$http', 'sharedService', function ($http, sharedService) {

    var serviceBase = sharedService.getBaseUrl();

    var changePasswordServiceFactory = {};

    var _ValidateCurrentPassword = function (userID, userName, password) {
        var myUrl = serviceBase + 'User/ValidateCurrentPassword/' + userID + '/' + userName + '/' + password;
        return $http.get(myUrl).then(
            function (results) {
                return results;
            });
    }

    var _changePassword = function (userID, userName, password) {
        var myUrl = serviceBase + 'User/changePassword';
        var data = { UserID: userID, UserName: userName, Password: password };
        return $http({
            method: 'POST',
            url: myUrl,
            params: data
        }).then(function (results) {
            return results;
        });
    }

    changePasswordServiceFactory.changePassword = _changePassword;
    changePasswordServiceFactory.ValidateCurrentPassword = _ValidateCurrentPassword;

    return changePasswordServiceFactory;

}]);