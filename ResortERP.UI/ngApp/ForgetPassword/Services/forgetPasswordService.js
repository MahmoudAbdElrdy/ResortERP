'use strict';
app.factory('forgetPasswordService', ['$http', 'sharedService', function ($http, sharedService) {

    var serviceBase = sharedService.getBaseUrl();

    var changePasswordServiceFactory = {};

    var _chkPass = function (userID, userName, password) {
        var myUrl = serviceBase + 'User/chkPass?userID=' + userID + '&userName=' + userName + '&password=' + password;
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

    changePasswordServiceFactory.changeingPassword = _changePassword;
    changePasswordServiceFactory.chkPass = _chkPass;

    return changePasswordServiceFactory;

}]);