'use strict';
app.factory('MenuService', ['$http', 'sharedService', function ($http, sharedService) {

    var serviceBase = sharedService.getBaseUrl();

    var UserFactory = {};

    var _getCompany = function () {
        var myUrl = serviceBase + 'uidView/getCompany';
        return $http.get(myUrl).then(
            function (results) {
                return results;
            });
    }


    var _getUserType = function () {
        var myUrl = serviceBase + 'User/GetUserType';
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    var _getAllUserMenu = function (lang) {
        var myUrl = serviceBase + 'UserMenu/GetAllUserMenuForWeb/0/' + lang;
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }


    var _add = function (entity) {
        var myUrl = serviceBase + 'UserMenu/insert';
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

    var _Update = function (entity) {
        var myUrl = serviceBase + 'UserMenu/update';
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
        var myUrl = serviceBase + 'UserMenu/delete';
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


    var _getMainMenu = function () {
        var myUrl = serviceBase + 'UserMenu/getMainMenu';
        return $http.get(myUrl).then(function (result) {
            return result;
        }, function (error) {
        });
    }


    var _getMenuBySettingID = function (settingID) {
        var myUrl = serviceBase + 'UserMenu/getMenuBySettingId?settingID=' + settingID;
        return $http.get(myUrl).then(function (result) {
            return result;
        }, function (error) {
        });
    }

    var _getMenuByEntrySettingID = function (settingID) {
        var myUrl = serviceBase + 'UserMenu/getMenuByEntrySettingId?settingID=' + settingID;
        return $http.get(myUrl).then(function (result) {
            return result;
        }, function (error) {
        });
    }


    var _getAllMenus = function (pageNum, pageSize) {
        var myUrl = serviceBase + 'UserMenu/getAllMenuAsync?pageNum=' + pageNum + '&pageSize=' + pageSize;
        return $http.get(myUrl).then(function (result) {
            return result;
        }, function (error) {
        });
    }



    var _countUserMenu = function () {
        var myUrl = serviceBase + 'UserMenu/count';
        return $http.get(myUrl).then(function (result) {
            return result;
        }, function (error) {
        });
    }

    var _getLastCode = function () {
        var myUrl = serviceBase + 'UserMenu/getLastCode';
        return $http.get(myUrl).then(function (result) {
            return result;
        }, function (error) {
        });
    }


    UserFactory.GetCompany = _getCompany;
    UserFactory.getUserType = _getUserType;
    UserFactory.getAllUserMenu = _getAllUserMenu;
    UserFactory.add = _add;
    UserFactory.Update = _Update;
    UserFactory.delete = _delete;
    UserFactory.getMainMenu = _getMainMenu;
    UserFactory.getMenuBySettingID = _getMenuBySettingID;
    UserFactory.getMenuByEntrySettingID = _getMenuByEntrySettingID;
    UserFactory.getAllMenus = _getAllMenus;
    UserFactory.countUserMenu = _countUserMenu;
    UserFactory.getLastCode = _getLastCode;

    return UserFactory;
}]);