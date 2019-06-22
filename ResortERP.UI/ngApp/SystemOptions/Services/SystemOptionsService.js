'use strict';
app.factory('systemOptionsService', ['$http', 'sharedService', function ($http, sharedService) {
    var serviceBase = sharedService.getBaseUrl();
    var systemOptionsServiceFactory = {};

    var _get = function (pageNum, pageSize) {
        var myUrl = serviceBase + 'sysOpt/get?pageNum=' + pageNum + '&pageSize=' + pageSize;
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    var _getByUserName = function (userName) {
        var myUrl = serviceBase + 'sysOpt/getByUserName?userName=' + userName;
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    var _getAll = function () {
        var myUrl = serviceBase + 'sysOpt/getAll';
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }



    var _count = function () {
        var myUrl = serviceBase + 'sysOpt/count';
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    var _update = function (entity) {
        var myUrl = serviceBase + 'sysOpt/update';
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
        var myUrl = serviceBase + 'sysOpt/delete';
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

    var _insert = function (entity) {
        var myUrl = serviceBase + 'sysOpt/insert';
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

    var _SaveAll = function (SysOptEntity, shortcutList, emailsList, userDataEntity, priceTypeEntity, KestOptList, IncomeAccountTypesList, TBoxAccountsList, TStoresList, addPartEntity, userPrivBran) {
        var myUrl = serviceBase + 'sysOpt/SaveAll';
        var data = { SystemOptions: SysOptEntity, shortcutList: shortcutList, emailsList: emailsList, userDataEntity: userDataEntity, priceTypeEntity: priceTypeEntity, KestOptList: KestOptList, IncomeAccountTypesList: IncomeAccountTypesList, TBoxAccountsList: TBoxAccountsList, TStoresList: TStoresList, addPartEntity: addPartEntity, userPrivBran: userPrivBran};
        //var data = { ComplexObj: { emailsList: emailsList } };
        return $http({
            method: 'POST',
            url: myUrl,
            contentType: "application/json;charset=utf-8",
            data: JSON.stringify(data)
        }).then(function (results) {
            return results;
        });
    }

    var _chkExist = function (entity) {
        var myUrl = serviceBase + 'sysOpt/chkExist';
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

    var _insertFilterForCompany = function (entity,userName) {
        var myUrl = serviceBase + 'sysOpt/insertFilterForCompany';
        var data = {};
        data = [entity,  userName ];
        return $http({
            method: 'POST',
            url: myUrl,
            data: data
        }).then(function (results) {
            return results;
        });
    }
    var _getFilterCompValue = function () {
        var myUrl = serviceBase + 'sysOpt/getFilterCompValue';
        return $http({
            method: 'Get',
            url: myUrl,
        }).then(function (results) {
            return results;
        });
    }

    var _logAjaxReq = function (userName) {
        var myUrl = serviceBase + 'LogUserReq/insert';
        return $http({
            method: 'Get',
            data: userName,
            url: myUrl,
        }).then(function (results) {
            return results;
        });
    }
    systemOptionsServiceFactory.get = _get;
    systemOptionsServiceFactory.count = _count;
    systemOptionsServiceFactory.insert = _insert;
    systemOptionsServiceFactory.update = _update;
    systemOptionsServiceFactory.delete = _delete;
    systemOptionsServiceFactory.getAll = _getAll;
    systemOptionsServiceFactory.chkExist = _chkExist;
    systemOptionsServiceFactory.getByUserName = _getByUserName;
    systemOptionsServiceFactory.SaveAll = _SaveAll;
    systemOptionsServiceFactory.insertFilterForCompany = _insertFilterForCompany;
    systemOptionsServiceFactory.getFilterCompValue = _getFilterCompValue;
    systemOptionsServiceFactory.logAjaxReq = _logAjaxReq;
    return systemOptionsServiceFactory;

}]);