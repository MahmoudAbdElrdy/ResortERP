'use strict';
app.factory('innerRegisterService', ['$http', 'sharedService', function ($http, sharedService) {

    var serviceBase = sharedService.getBaseUrl();

    var UserServiceFactory = {};

    var _get = function (pageNum, pageSize) {
        var myUrl = serviceBase + 'User/get?pageNum=' + pageNum + '&pageSize=' + pageSize;
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    var _count = function () {
        var myUrl = serviceBase + 'User/count';
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    var _update = function (entity) {
        var myUrl = serviceBase + 'User/update';
        var data = {};
        data = entity;
        return $http({
            method: 'POST',
            url: myUrl,
            data: JSON.stringify(data)
        }).then(function (results) {
            return results;
        });
    }

    var _delete = function (entity) {
        var myUrl = serviceBase + 'User/delete';
        var data = {};
        data = entity;
        return $http({
            method: 'POST',
            url: myUrl,
            data: JSON.stringify(data)
        }).then(function (results) {
            return results;
        });
    }

    // delete user and his privilige on branches
    var _deleteBran = function (entity) {
        var myUrl = serviceBase + 'User/deleteBran';
        var data = {};
        data = entity;
        return $http({
            method: 'POST',
            url: myUrl,
            data: JSON.stringify(data)
        }).then(function (results) {
            return results;
        });
    }

    var _add = function (entity) {
        var myUrl = serviceBase + 'User/addRegularUser';
        var data = {};
        data = entity;
        return $http({
            method: 'POST',
            url: myUrl,
            data: JSON.stringify(data)
        }).then(function (results) {
            return results;
        });
    }

    var _checkEmail = function (email) {
        var myUrl = serviceBase + 'User/CheckEmail?email=' + email;
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    var _checkUserName = function (UID) {
        var myUrl = serviceBase + 'User/CheckUserName?userName=' + UID;
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    UserServiceFactory.add = _add;
    UserServiceFactory.delete = _delete;
    UserServiceFactory.deleteBran =_deleteBran
    UserServiceFactory.update = _update;
    UserServiceFactory.count = _count;
    UserServiceFactory.get = _get;
    UserServiceFactory.checkEmail = _checkEmail;
    UserServiceFactory.checkUserName = _checkUserName;
    return UserServiceFactory;
}]);