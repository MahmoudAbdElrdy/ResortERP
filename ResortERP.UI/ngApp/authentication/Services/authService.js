'use strict';
app.factory('authService', ['$http', '$q', 'localStorageService', 'sharedService', '$location', '$rootScope', function ($http, $q, localStorageService, sharedService, $location, $rootScope) {

    //var serviceBase = 'http://localhost:55515/';
    var serviceBase = sharedService.getBaseUrl();

    var authServiceFactory = {
    };

    var _authentication = {
        isAuth: false,
        userName: "",
        //password: ""
    };

    var _saveRegistration = function (registration) {
        _logOut();
        return $http.post(serviceBase + 'register', registration).then(function (response) {
            return response;
        });
    };

    var _ForgetPassword = function (userName) {
        var myUrl = serviceBase + 'User/ForgetPassword/' + userName;
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    };

    //var _log_in = function (loginData) {
    //    return $http.get(serviceBase + "api/GetUser?username=" + loginData.userName + "&password=" + loginData.password, {
    //        headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
    //    }).then(function (response) {
    //        localStorageService.set('authorizationData', { token: response.data, userName: loginData.userName });
    //        _authentication.isAuth = true;
    //        _authentication.userName = loginData.userName;
    //    }, function (error) {
    //        console.log(error.message);
    //        _logOut();
    //    });
    //};
    var getUserData = function (userName, password) {
        
        var myUrl = serviceBase + 'api/GetUser?userName=' + userName + '&password=' + password;

        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    var _getUserData = function (userID) {
        var myUrl = serviceBase + 'User/GetUserData/' + userID;
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    var _SetLanguageID = function (langID) {
        return localStorageService.set('userLang', { LanguageID: langID });
    };

    var _GetLanguageID = function () {
        var userdata = localStorageService.get('userLang');
        return userdata != null ? userdata.LanguageID : 1;
    };

    var _GetauthorizationData = function () {
        var userdata = localStorageService.get('authorizationData');
        return userdata;
    };

    var _GetUserID = function () {
        
        var userdata = localStorageService.get('userID');
        return userdata.UserID;
    };

    var _GetUserName = function () {
        var userdata = localStorageService.get('authorizationData');
        return userdata != null ? userdata.userName : "";
    };

    var _log_in = function (loginData) {
       // debugger;
        var data = "grant_type=password&username=" + loginData.userName + "&password=" + loginData.password;
        var deferred = $q.defer();
        $http.post(serviceBase + 'token', data, {
            headers: {
                'Content-Type': 'application/x-www-form-urlencoded'
            }
        }).success(function (response, status, data) {

           
            var userData = getUserData(loginData.userName, loginData.password);
            userData.then(function (returnedData) {
                localStorageService.set('userType', { UserTypeID: returnedData.data.USER_TYPE });
                localStorageService.set('userID', { UserID: returnedData.data.ID });
                localStorageService.set('userLang', { LanguageID: returnedData.data.LanguageID });

                localStorageService.set('authorizationData', { token: response.access_token, userName: loginData.userName });
                //localStorageService.set('authorizationData', { userName: loginData.userName });
                //_getAuthenticated(loginData);
                _authentication.isAuth = false;
                _authentication.userName = loginData.userName;
                deferred.resolve(response);
                //deferred.resolve(returnedData);
                var s = data;
            }, function (response, status, data) {
                deferred.reject(status);
                _logOut();
            });
        }).error(function (response, status, data) {
            deferred.reject(status);
            _logOut();
        });
        return deferred.promise;
    };

    var _logOut = function () {
        localStorageService.remove('authorizationData');
        localStorageService.remove('userType');
        localStorageService.remove('userID');
        localStorageService.remove('userLang');
        localStorageService.remove('tempBrName');
        localStorageService.remove('tempBrEnName');
        localStorageService.remove('UserModule');
        localStorageService.remove('UserPermissions');
        localStorageService.remove('branchId');
        _authentication.isAuth = false;
        _authentication.userName = "";

    };

    var _fillAuthData = function () {
        var authData = localStorageService.get('authorizationData');
        if (authData) {
            _authentication.isAuth = true;
            _authentication.userName = authData.userName;

        }
    };

    var _getAuthenticated = function (loginData) {
        alert(loginData.userName + '  ' + loginData.password);
        var myUrl = serviceBase + 'User/GetUserObj';
        var data = {
            username: loginData.userName, password: loginData.password
        };
        return $http({
            method: 'POST',
            url: myUrl,
            data: JSON.stringify(data)
        }).then(function (results) {
            //sharedService.COMPANY_AR_NAME = results.COMPANY_AR_NAME;
            return results;
        }, function (error) {
            alert(error.message);
        });
    };



    var _getUserByUserName = function (userName) {
        var myUrl = serviceBase + 'User/GetUserByUserName?userName=' + userName;
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }


    authServiceFactory.saveRegistration = _saveRegistration;
    authServiceFactory.log_in = _log_in;
    authServiceFactory.logOut = _logOut;
    authServiceFactory.fillAuthData = _fillAuthData;
    authServiceFactory.authentication = _authentication;
    authServiceFactory.getAuthenticated = _getAuthenticated;
    authServiceFactory.GetUserID = _GetUserID;
    authServiceFactory.GetLanguageID = _GetLanguageID;
    authServiceFactory.SetLanguageID = _SetLanguageID;
    authServiceFactory.GetUserName = _GetUserName;
    authServiceFactory.GetauthorizationData = _GetauthorizationData;
    authServiceFactory.ForgetPassword = _ForgetPassword;
    authServiceFactory.GetUserData = _getUserData;
    authServiceFactory.getUserByUserName = _getUserByUserName;

    return authServiceFactory;
}]);