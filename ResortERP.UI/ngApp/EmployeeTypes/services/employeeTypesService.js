'use strict';
app.factory('employeeTypesService', ['$http', 'sharedService', function ($http, sharedService) {

    var serviceBase = sharedService.getBaseUrl();

    var employeeTypesServiceFactory = {};

    var _get = function (pageNum, pageSize) {
        var myUrl = serviceBase + 'employeeTypes/get?pageNum=' + pageNum + '&pageSize=' + pageSize;
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    var _count = function () {
        var myUrl = serviceBase + 'employeeTypes/count';
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    var _update = function (entity) {
        var myUrl = serviceBase + 'employeeTypes/update';
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
        var myUrl = serviceBase + 'employeeTypes/delete';
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
        var myUrl = serviceBase + 'employeeTypes/insert';
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

    var _getAll = function () {
        var myUrl = serviceBase + 'employeeTypes/getAll';
        return $http.get(myUrl).then(function (results) {
            return results;
        })
    }

    var _getById = function (EMP_TYPE_ID) {
        var myUrl = serviceBase + 'employeeTypes/getById?EMP_TYPE_ID=' + EMP_TYPE_ID;
        return $http.get(myUrl).then(function (results) {
            return results;
        })
    }

    var _getLastCode = function () {
        var myUrl = serviceBase + 'employeeTypes/getLastCode';
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }


    employeeTypesServiceFactory.get = _get;
    employeeTypesServiceFactory.count = _count;
    employeeTypesServiceFactory.insert = _insert;
    employeeTypesServiceFactory.update = _update;
    employeeTypesServiceFactory.delete = _delete;
    employeeTypesServiceFactory.getAll = _getAll;
    employeeTypesServiceFactory.getLastCode = _getLastCode;
    employeeTypesServiceFactory.getById = _getById;
    return employeeTypesServiceFactory;

}]);