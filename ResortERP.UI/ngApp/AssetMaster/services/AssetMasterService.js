'use strict';
app.factory('AssetMasterService', ['$http', 'sharedService', function ($http, sharedService) {

    var serviceBase = sharedService.getBaseUrl();

    var employeeServiceFactory = {};

    var _get = function (pageNum, pageSize) {
        var myUrl = serviceBase + 'assetMaster/get?pageNum=' + pageNum + '&pageSize=' + pageSize;
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    //var _getById = function (EMP_ID) {
    //    var myUrl = serviceBase + 'assetMaster/getById?EMP_ID=' + EMP_ID;
    //    return $http.get(myUrl).then(function (results) {
    //        return results;
    //    });
    //}

    //var _getByTypeID = function (TypeID) {
    //    var myUrl = serviceBase + 'assetMaster/getByTypeID?typeID=' + TypeID;
    //    return $http.get(myUrl).then(function (results) {
    //        return results;
    //    });
    //}

    var _count = function () {
        var myUrl = serviceBase + 'assetMaster/count';
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    var _update = function (entity) {
        var myUrl = serviceBase + 'assetMaster/update';
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
        var myUrl = serviceBase + 'assetMaster/delete';
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
        var myUrl = serviceBase + 'assetMaster/add';
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

    //var _insertGetID = function (entity) {
    //    var myUrl = serviceBase + 'assetMaster/insertGetID';
    //    var data = {};
    //    data = entity;
    //    return $http({
    //        method: 'POST',
    //        url: myUrl,
    //        data: JSON.stringify(data)
    //    }).then(function (results) {
    //        return results;
    //    });
    //}

    var _getLastCode = function () {
        var myUrl = serviceBase + 'assetMaster/getLastCode';

        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    var _getAssetTypeList = function () {
        var myUrl = serviceBase + 'assetMaster/getAssetTypeList';
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }
    var _getAssetStatusList = function () {
        var myUrl = serviceBase + 'assetMaster/getAssetStatusList';
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }
    var _getOriginNationList = function () {
        var myUrl = serviceBase + 'assetMaster/getOriginNationList';
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }
    var _getCompanyList = function () {
        var myUrl = serviceBase + 'assetMaster/getCompanyList';
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }
    var _getAssetDepreciationTypeList = function () {
        var myUrl = serviceBase + 'assetMaster/getAssetDepreciationTypeList';
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }
    var _getAssetLifeSpanUnitList = function () {
        var myUrl = serviceBase + 'assetMaster/getAssetLifeSpanUnitList';
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }
    var _getCurrencyList = function () {
        var myUrl = serviceBase + 'assetMaster/getCurrencyList';
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }
    var _getAccountList = function () {
        var myUrl = serviceBase + 'assetMaster/getAccountList';
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }
    var _getDepartmentList = function () {
        var myUrl = serviceBase + 'assetMaster/getDepartmentList';
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }
    var _getAssetGroupList = function () {
        var myUrl = serviceBase + 'assetMaster/getAssetGroupList';
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    var _getAssetDepreciationDetails = function (AssetMasterId) {
        var myUrl = serviceBase + 'assetMaster/getAssetDepreciationDetails?AssetMasterId=' + AssetMasterId;
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    employeeServiceFactory.get = _get;
    employeeServiceFactory.count = _count;
    employeeServiceFactory.insert = _insert;
    //employeeServiceFactory.insertGetID = _insertGetID;
    employeeServiceFactory.update = _update;
    employeeServiceFactory.delete = _delete;
    //employeeServiceFactory.getByTypeID = _getByTypeID;
    employeeServiceFactory.getLastCode = _getLastCode;
    //employeeServiceFactory.getById = _getById;

    employeeServiceFactory.getAssetTypeList = _getAssetTypeList;
    employeeServiceFactory.getAssetStatusList = _getAssetStatusList;
    employeeServiceFactory.getOriginNationList = _getOriginNationList;
    employeeServiceFactory.getCompanyList = _getCompanyList;
    employeeServiceFactory.getAssetDepreciationTypeList = _getAssetDepreciationTypeList;
    employeeServiceFactory.getAssetLifeSpanUnitList = _getAssetLifeSpanUnitList;
    employeeServiceFactory.getCurrencyList = _getCurrencyList;
    employeeServiceFactory.getAccountList = _getAccountList;
    employeeServiceFactory.getDepartmentList = _getDepartmentList;
    employeeServiceFactory.getAssetGroupList = _getAssetGroupList;
    employeeServiceFactory.getAssetDepreciationDetails = _getAssetDepreciationDetails;

    //employeeServiceFactory.getAcademicDegreeList = _getAcademicDegreeList;
    //employeeServiceFactory.getBankList = _getBankList;

    //employeeServiceFactory.getEmployeeAcademicDegrees = _getEmployeeAcademicDegrees;
    //employeeServiceFactory.getEmployeeExperiences = _getEmployeeExperiences;
    //employeeServiceFactory.getEmployeeFamilyDetails = _getEmployeeFamilyDetails;
    //employeeServiceFactory.getEmployeeTrainingCourses = _getEmployeeTrainingCourses;
    //employeeServiceFactory.getEmployeeContacts = _getEmployeeContacts;

    return employeeServiceFactory;
}]);