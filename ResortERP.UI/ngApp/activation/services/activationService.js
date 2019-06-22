'use strict';
app.factory('activationService', ['$http', '$q', 'sharedService', function ($http, $q, sharedService) {

    var serviceBase = sharedService.getBaseUrl();

    var activationServiceFactory = {};

    var _activatCompany = function (activation) {
        var myUrl = serviceBase + 'User/CompanyActivate';
        var data = { activationCode: activation.activationCode, password: activation.password, username: activation.uName };
        return $http({
            method: 'POST',
            url: myUrl,
            data: JSON.stringify(data)
        }).then(function (results) {
            return results;
        }, function (error) {
            console.log(error.message);
        });
    };

    var _getByUID = function (ID) {
        var myUrl = serviceBase + 'user/getUserById/' + ID;
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    var _getInnerAdminById = function (ID) {
        var myUrl = serviceBase + 'user/getInnerAdminById/' + ID;
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    var _activateUser = function (ID) {
        var myUrl = serviceBase + 'user/activateUser/' + ID;
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    activationServiceFactory.getByUID = _getByUID;
    activationServiceFactory.getInnerAdminById = _getInnerAdminById;
    activationServiceFactory.activateUser = _activateUser;
    activationServiceFactory.activatCompany = _activatCompany;

    return activationServiceFactory;
}]);