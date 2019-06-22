'use strict';
app.factory('AccountsTreeService', ['$http', 'sharedService', function ($http, sharedService) {

    var serviceBase = sharedService.getBaseUrl();

    var accountsServiceFactory = {};

    var _get = function (pageNum, pageSize) {
        var myUrl = serviceBase + 'AccountsTree/get?pageNum=' + pageNum + '&pageSize=' + pageSize;
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }



    var _count = function (pageNum, pageSize) {
        var myUrl = serviceBase + 'AccountsTreeService/count';
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    //var _getCustomerSupplier = function (pageNum, pageSize) {
    //    var myUrl = serviceBase + 'CustomerSupplier/get?pageNum=' + pageNum + '&pageSize=' + pageSize ;
    //    return $http.get(myUrl).then(function (results) {
    //        return results;
    //    });
    //}

    //var _getAllCustomerSupplier = function (type) {
    //    var myUrl = serviceBase + 'CustomerSupplier/getAll?type=' + type;
    //    return $http.get(myUrl).then(function (results) {
    //        return results;
    //    });
    //}

    //var _getAll = function () {
    //    var myUrl = serviceBase + 'customer/getAll';
    //    return $http.get(myUrl).then(function (results) {
    //        return results;
    //    });
    //}

    //var _count = function (type) {
    //    var myUrl = serviceBase + 'customer/count?type=' + type;
    //    return $http.get(myUrl).then(function (results) {
    //        return results;
    //    });
    //}

    var _update = function (entity) {
        var myUrl = serviceBase + 'Acounts/update';
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
        var myUrl = serviceBase + 'Acounts/delete';
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
        var myUrl = serviceBase + 'Acounts/add';
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
    
    var _getAllAccounts = function () {
        var myUrl = serviceBase + 'Acounts/getAll';
        return $http.get(myUrl).then(function (result) {
            return result;
        });
    }

    var _getAccountTypes = function () {
        var myUrl = serviceBase + 'accountType/getAll';
        return $http.get(myUrl).then(function (result) {
            return result;
        });
    }

    var _getLastCode = function () {
        var myUrl = serviceBase + 'Accounts/getLastCode';
        return $http.get(myUrl).then(function (result) {
            return result;
        }, function (error) { });
    }


    var _getCodeWithoutParent = function () {
        var myUrl = serviceBase + 'Accounts/getCodeWithoutParent';
        return $http.get(myUrl).then(function (result) {
            return result;
        }, function (error) { });
    }


    var _getAccountByCode = function (accountCode) {
        var myUrl = serviceBase + 'Accounts/getAccountByCode?AccountCode=' + accountCode;
        return $http.get(myUrl).then(function (result) {
            return result;
        }, function (error) { });
    }



    var _getParentDataByID = function (parentID) {
        var myUrl = serviceBase + 'Accounts/getParentDataByID?parentID=' + parentID;
        return $http.get(myUrl).then(function (result) {
            return result;
        }, function (error) { });
    }


    var _getTypesByQuerystring = function (type) {
        var myUrl = serviceBase + 'accountType/getByQueryString?type=' + type;
        return $http.get(myUrl).then(function (result) {
            return result;
        }, function (error) { });
    }


    var checkAccountIfUsed = function (accountID) {
        var myUrl = serviceBase + 'Acounts/checkAccountIfUsed?accountID=' + accountID;
        return $http.get(myUrl).then(function (result) {
            return result;
        });
    }


    //var _InsertUpdateCustomerDependence = function (entity, teleList, CustBranList, trans) {
    //    debugger;
    //    var myUrl = serviceBase + 'customer/InsertUpdateCustomerDependence';
    //    var data = { obj: { customer: entity, telephones: teleList, customerBran: CustBranList, transaction: trans } };
    //    return $http({
    //        method: 'POST',
    //        url: myUrl,
    //        data: JSON.stringify(data),
    //        contentType: 'application/json; charset=utf-8',
    //        dataType: 'json'
    //    }).then(function (results) {
    //        return results;
    //    }, function (error) {
    //        console.log(error.data.message)
    //    });
    //}

    accountsServiceFactory.get = _get;
    // customerServiceFactory.getAll = _getAll;
    // customerServiceFactory.count = _count;
    accountsServiceFactory.insert = _insert;
    //customerServiceFactory.InsertUpdateCustomerDependence = _InsertUpdateCustomerDependence;
    accountsServiceFactory.update = _update;
    accountsServiceFactory.delete = _delete;
    accountsServiceFactory.count = _count;
    accountsServiceFactory.getAllAccounts = _getAllAccounts;
    accountsServiceFactory.getAccountTypes = _getAccountTypes;
    accountsServiceFactory.getLastCode = _getLastCode;
    accountsServiceFactory.getCodeWithoutParent = _getCodeWithoutParent;
    accountsServiceFactory.getAccountByCode = _getAccountByCode;
    accountsServiceFactory.getParentDataByID = _getParentDataByID;
    accountsServiceFactory.getTypesByQuerystring = _getTypesByQuerystring;
    accountsServiceFactory.checkAccountIfUsed = checkAccountIfUsed;
    // customerServiceFactory.getCustomerSupplier = _getCustomerSupplier;
    // customerServiceFactory.getAllCustomerSupplier = _getAllCustomerSupplier;

    return accountsServiceFactory;

}]);