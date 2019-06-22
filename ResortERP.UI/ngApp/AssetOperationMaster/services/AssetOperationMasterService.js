'use strict';
app.factory('AssetOperationMasterService', ['$http', 'sharedService', function ($http, sharedService) {

    var serviceBase = sharedService.getBaseUrl();

    var AssetOperationMasterServiceFactory = {};

    var _get = function (pageNum, pageSize, operationType) {
        var myUrl = serviceBase + 'AssetOperationMaster/get?pageNum=' + pageNum + '&pageSize=' + pageSize + '&operationType=' + operationType;
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    //var _getById = function (EMP_ID) {
    //    var myUrl = serviceBase + 'AssetOperationMaster/getById?EMP_ID=' + EMP_ID;
    //    return $http.get(myUrl).then(function (results) {
    //        return results;
    //    });
    //}

    //var _getByTypeID = function (TypeID) {
    //    var myUrl = serviceBase + 'AssetOperationMaster/getByTypeID?typeID=' + TypeID;
    //    return $http.get(myUrl).then(function (results) {
    //        return results;
    //    });
    //}

    var _count = function (operationType) {
        var myUrl = serviceBase + 'AssetOperationMaster/count?operationType=' + operationType;
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    var _update = function (entity) {
        var myUrl = serviceBase + 'AssetOperationMaster/update';
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
        var myUrl = serviceBase + 'AssetOperationMaster/delete';
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
        var myUrl = serviceBase + 'AssetOperationMaster/add';
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
    //    var myUrl = serviceBase + 'AssetOperationMaster/insertGetID';
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
        var myUrl = serviceBase + 'AssetOperationMaster/getLastCode';

        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    var _getAssetMasterList = function () {
        var myUrl = serviceBase + 'AssetOperationMaster/getAssetMasterList';
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }
    var _getCostCenterList = function () {
        var myUrl = serviceBase + 'AssetOperationMaster/getCostCenterList';
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }
    var _getDepartmentList = function () {
        var myUrl = serviceBase + 'AssetOperationMaster/getDepartmentList';
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }
    var _getCompanyList = function () {
        var myUrl = serviceBase + 'AssetOperationMaster/getCompanyList';
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    var _getCurrencyList = function () {
        var myUrl = serviceBase + 'AssetOperationMaster/getCurrencyList';
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }
    var _getAccountList = function () {
        var myUrl = serviceBase + 'AssetOperationMaster/getAccountList';
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    var _getEmployeeList = function () {
        var myUrl = serviceBase + 'AssetOperationMaster/getEmployeeList';
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    var _getEmployeeAssets = function (employeeId) {
        var myUrl = serviceBase + 'AssetOperationMaster/getEmployeeAssets?employeeId=' + employeeId;
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    AssetOperationMasterServiceFactory.get = _get;
    AssetOperationMasterServiceFactory.count = _count;
    AssetOperationMasterServiceFactory.insert = _insert;
    AssetOperationMasterServiceFactory.update = _update;
    AssetOperationMasterServiceFactory.delete = _delete;
    AssetOperationMasterServiceFactory.getLastCode = _getLastCode;

    AssetOperationMasterServiceFactory.getAssetMasterList = _getAssetMasterList;
    AssetOperationMasterServiceFactory.getCostCenterList = _getCostCenterList;
    AssetOperationMasterServiceFactory.getDepartmentList = _getDepartmentList;
    AssetOperationMasterServiceFactory.getCompanyList = _getCompanyList;
    AssetOperationMasterServiceFactory.getCurrencyList = _getCurrencyList;
    AssetOperationMasterServiceFactory.getAccountList = _getAccountList;
    AssetOperationMasterServiceFactory.getEmployeeList = _getEmployeeList;
    AssetOperationMasterServiceFactory.getEmployeeAssets = _getEmployeeAssets;
    return AssetOperationMasterServiceFactory;
}]);