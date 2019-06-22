'use strict';
app.factory('userPrivilagesService', ['$http', 'sharedService', 'localStorageService', function ($http, sharedService, localStorageService) {
    
   

    var serviceBase = sharedService.getBaseUrl();
    var userPrivilagesServiceFactory = {};


    var _get = function (pageNum, pageSize) {
        var myUrl = serviceBase + 'property/getPagedData?pageNum=' + pageNum + '&pageSize=' + pageSize;

        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }
    var _count = function () {
        var myUrl = serviceBase + 'property/count';

        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    var _getAllUsers = function () {
        var myUrl = serviceBase + 'user/GetAllUsers';
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    var _getAllUserMenusForUser = function (userId) {
        var myUrl = serviceBase + 'UserPrivilige/GetAllUserMenusForUser/' + userId;

        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }


    var _getUserMenu = function (userId) {
        var myUrl = serviceBase + 'UserPrivilige/GetUserMenu/0/1';
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }
    

    var _getByMenuId = function (id) {

        var myUrl = serviceBase + 'UserPrivilige/getByMenuId?id='+id;
        return $http({
            method: 'Get',
            url: myUrl,
        }).then(function (results) {
            return results;
        });
    }

    var _getMasterUserMenus = function (MenuID) {
        var myUrl = serviceBase + 'UserPrivilige/GetMasterUserMenus/0/1/' + MenuID;
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    var _saveUserPrivliages = function (entity) {

        var myUrl = serviceBase + 'UserPrivilige/InsertUserPrivilages';
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

    var _getAllUserPermissions = function (userId) {
        var myUrl = serviceBase + 'UserPrivilige/getAllUserPermissions/' + userId;

        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    userPrivilagesServiceFactory.get = _get;
    userPrivilagesServiceFactory.count = _count;
    userPrivilagesServiceFactory.getAllUsers = _getAllUsers;
    userPrivilagesServiceFactory.getAllUserMenusForUser = _getAllUserMenusForUser;
    userPrivilagesServiceFactory.getUserMenu = _getUserMenu;
    userPrivilagesServiceFactory.getMasterUserMenus = _getMasterUserMenus;
    userPrivilagesServiceFactory.saveUserPrivliages = _saveUserPrivliages;
    userPrivilagesServiceFactory.getByMenuId = _getByMenuId;
    userPrivilagesServiceFactory.getAllUserPermissions = _getAllUserPermissions;
    return userPrivilagesServiceFactory;

}]);