'use strict'
app.factory('compBranchesService', ['$http', 'sharedService', function ($http, sharedService) {

    var serviceBase = sharedService.getBaseUrl();
    var companyBranchesFactory = {};

    var _get = function (pageNum, pageSize) {
        var myUrl = serviceBase + 'companyBranches/get?pageNum=' + pageNum + '&pageSize=' + pageSize;
        return $http.get(myUrl).then(function (result) {
            return result;
        }, function (error) {
        });
    }

   
    var _getAll = function () {
        var myUrl = serviceBase + 'companyBranches/getAll';
        return $http.get(myUrl).then(function (result) {
            return result;
        }, function (error) {
        });
    }

    var _getById = function (COM_BRN_ID) {
        var myUrl = serviceBase + 'companyBranches/getByIdLog?COM_BRN_ID=' + COM_BRN_ID;
        return $http.get(myUrl).then(function (result) {
            return result;
        }, function (error) {
        });
    }

    var _getUserBranches = function () {
        var myUrl = serviceBase + 'companyBranches/getUserBranches';
        return $http.get(myUrl).then(function (result) {
            return result;
        }, function (error) {
        });
    }

    var _count = function () {
       
        var myUrl = serviceBase + 'companyBranches/count';
        return $http.get(myUrl).then(function (result) {
            return result;
        }, function (error) {
        });
    }


    var _getMainCompanyBranches = function () {
        var myUrl = serviceBase + 'companyBranches/getMainCompanyBranches';
        return $http.get(myUrl).then(function (result) {
            return result;
        }, function (error) {
        });
    }


    var _add = function (entity) {
       
        var myUrl = serviceBase + 'companyBranches/add';
        var data = {};
        data = entity;
        return $http({
            method: 'POST',
            url: myUrl,
            data: JSON.stringify(data)
        }).then(function (result) {
            return result;
        }, function (error) {
        });
    }


    var _update = function (entity) {
        
        var myUrl = serviceBase + 'companyBranches/update';
        var data = {};
        data = entity;
        return $http({
            method: 'POST',
            url: myUrl,
            data: JSON.stringify(data)
        }).then(function (result) {
            return result;
        }, function (error) {
        });
    }

    var _delete = function (entity) {
        var myUrl = serviceBase + 'companyBranches/delete';
        var data = {};
        data = entity;
        return $http({
            method: 'POST',
            url: myUrl,
            data: JSON.stringify(data)
        }).then(function (result) {
            return result;
        }, function (error) {
        });
    }

    // delete branch and user privilige on this branch
    var _deleteBranUsers = function (entity) {
        var myUrl = serviceBase + 'companyBranches/deleteBranUsers';
        var data = {};
        data = entity;
        //alert(data);
        return $http({
            method: 'POST',
            url: myUrl,
            data: JSON.stringify(data)
        }).then(function (results) {
            return results;
        });
    }

    var _getLastCode = function () {
        var myUrl = serviceBase + 'companyBranches/getLastCode';
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    companyBranchesFactory.add = _add;
    companyBranchesFactory.update = _update;
    companyBranchesFactory.delete = _delete;
    companyBranchesFactory.deleteBranUsers = _deleteBranUsers;
    companyBranchesFactory.count = _count;
    companyBranchesFactory.get = _get;
    companyBranchesFactory.getMainCompanyBranches = _getMainCompanyBranches;
    companyBranchesFactory.getLastCode = _getLastCode;
    companyBranchesFactory.getAll = _getAll;
    companyBranchesFactory.getById = _getById;
    companyBranchesFactory.getUserBranches = _getUserBranches;
    return companyBranchesFactory;
}]);