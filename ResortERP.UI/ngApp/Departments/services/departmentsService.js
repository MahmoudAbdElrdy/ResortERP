'use strict';
app.factory('departmentsService', ['$http', 'sharedService', function ($http, sharedService) {

    var serviceBase = sharedService.getBaseUrl();

    var departmentServiceFactory = {};

    var _get = function (pageNum, pageSize) {
        var myUrl = serviceBase + 'department/get?pageNum=' + pageNum + '&pageSize=' + pageSize;
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    var _getById = function (DEPT_ID) {
        var myUrl = serviceBase + 'department/getById?DEPT_ID=' + DEPT_ID;
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    var _count = function () {
        var myUrl = serviceBase + 'department/count';
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    var _update = function (entity) {
        var myUrl = serviceBase + 'department/update';
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
        var myUrl = serviceBase + 'department/delete';
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
        var myUrl = serviceBase + 'department/insert';
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

    var _getAllDepartments = function () {
        var myUrl = serviceBase + 'department/getAll';
        return $http.get(myUrl).then(function (results) {
            return results;
        })
    }


    var _getLastCode = function () {
        var myUrl = serviceBase + 'department/getLastCode';
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }


    departmentServiceFactory.get = _get;
    departmentServiceFactory.count = _count;
    departmentServiceFactory.insert = _insert;
    departmentServiceFactory.update = _update;
    departmentServiceFactory.delete = _delete;
    departmentServiceFactory.getAllDepartments = _getAllDepartments;
    departmentServiceFactory.getLastCode = _getLastCode;
    departmentServiceFactory.getById = _getById;
    return departmentServiceFactory;

}]);