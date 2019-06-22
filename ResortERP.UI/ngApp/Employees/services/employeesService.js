'use strict';
app.factory('employeesService', ['$http', 'sharedService', function ($http, sharedService) {

    var serviceBase = sharedService.getBaseUrl();

    var employeeServiceFactory = {};

    var _get = function (pageNum, pageSize) {
        var myUrl = serviceBase + 'employee/get?pageNum=' + pageNum + '&pageSize=' + pageSize;
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    var _getById = function (EMP_ID) {
        var myUrl = serviceBase + 'employee/getById?EMP_ID=' + EMP_ID;
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    var _getByTypeID = function (TypeID) {
        var myUrl = serviceBase + 'employee/getByTypeID?typeID=' + TypeID;
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    var _count = function () {
        var myUrl = serviceBase + 'employee/count';
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    var _update = function (entity) {
        var myUrl = serviceBase + 'employee/update';
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
        var myUrl = serviceBase + 'employee/delete';
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
        var myUrl = serviceBase + 'employee/insert';
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

    var _insertGetID = function (entity) {
        var myUrl = serviceBase + 'employee/insertGetID';
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



    var _getLastCode = function () {
        var myUrl = serviceBase + 'employee/getLastCode';

        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    var _getMartialStatusList = function () {
        var myUrl = serviceBase + 'employee/getMartialStatusList';
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    var _getRelegionList = function () {
        var myUrl = serviceBase + 'employee/getRelegionList';
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }
    var _getCityList = function () {
        var myUrl = serviceBase + 'employee/getCityList';
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }
    var _getNationalityList = function () {
        var myUrl = serviceBase + 'employee/getNationalityList';
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }
    var _getJobTitleList = function () {
        var myUrl = serviceBase + 'employee/getJobTitleList';
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }
    var _getJobLevelList = function () {
        var myUrl = serviceBase + 'employee/getJobLevelList';
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }
    var _getDeptartmentList = function () {
        var myUrl = serviceBase + 'employee/getDeptartmentList';
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }
    var _getEmployeeStatusList = function () {
        var myUrl = serviceBase + 'employee/getEmployeeStatusList';
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }
    var _getDirectManagerList = function () {
        var myUrl = serviceBase + 'employee/getDirectManagerList';
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }
    var _getContactTypeList = function () {
        var myUrl = serviceBase + 'employee/getContactTypeList';
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }
    var _getAcademicDegreeList = function () {
        var myUrl = serviceBase + 'employee/getAcademicDegreeList';
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }
    var _getBankList = function () {
        var myUrl = serviceBase + 'employee/getBankList';
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    var _getEmployeeAcademicDegrees = function (employeeId) {
        var myUrl = serviceBase + 'employee/getEmployeeAcademicDegrees?employeeId=' + employeeId;
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }
    var _getEmployeeExperiences = function (employeeId) {
        var myUrl = serviceBase + 'employee/getEmployeeExperiences?employeeId=' + employeeId;
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }
    var _getEmployeeFamilyDetails = function (employeeId) {
        var myUrl = serviceBase + 'employee/getEmployeeFamilyDetails?employeeId=' + employeeId;
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }
    var _getEmployeeTrainingCourses = function (employeeId) {
        var myUrl = serviceBase + 'employee/getEmployeeTrainingCourses?employeeId=' + employeeId;
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }
    var _getEmployeeContacts = function (employeeId) {
        var myUrl = serviceBase + 'employee/getEmployeeContacts?employeeId=' + employeeId;
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    employeeServiceFactory.get = _get;
    employeeServiceFactory.count = _count;
    employeeServiceFactory.insert = _insert;
    employeeServiceFactory.insertGetID = _insertGetID;
    employeeServiceFactory.update = _update;
    employeeServiceFactory.delete = _delete;
    employeeServiceFactory.getByTypeID = _getByTypeID;
    employeeServiceFactory.getLastCode = _getLastCode;
    employeeServiceFactory.getById = _getById;

    employeeServiceFactory.getMartialStatusList = _getMartialStatusList;
    employeeServiceFactory.getRelegionList = _getRelegionList;
    employeeServiceFactory.getCityList = _getCityList;
    employeeServiceFactory.getNationalityList = _getNationalityList;
    employeeServiceFactory.getJobTitleList = _getJobTitleList;
    employeeServiceFactory.getJobLevelList = _getJobLevelList;
    employeeServiceFactory.getDeptartmentList = _getDeptartmentList;
    employeeServiceFactory.getEmployeeStatusList = _getEmployeeStatusList;
    employeeServiceFactory.getDirectManagerList = _getDirectManagerList;
    employeeServiceFactory.getContactTypeList = _getContactTypeList;
    employeeServiceFactory.getAcademicDegreeList = _getAcademicDegreeList;
    employeeServiceFactory.getBankList = _getBankList;

    employeeServiceFactory.getEmployeeAcademicDegrees = _getEmployeeAcademicDegrees;
    employeeServiceFactory.getEmployeeExperiences = _getEmployeeExperiences;
    employeeServiceFactory.getEmployeeFamilyDetails = _getEmployeeFamilyDetails;
    employeeServiceFactory.getEmployeeTrainingCourses = _getEmployeeTrainingCourses;
    employeeServiceFactory.getEmployeeContacts = _getEmployeeContacts;

    return employeeServiceFactory;
}]);