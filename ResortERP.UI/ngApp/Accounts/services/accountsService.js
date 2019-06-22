'use strict';
app.factory('accountsService', ['$http', 'sharedService', function ($http, sharedService) {

    var serviceBase = sharedService.getBaseUrl();

    var accountsServiceFactory = {};

    var _get = function (pageNum, pageSize) {
        var myUrl = serviceBase + 'Acounts/get?pageNum=' + pageNum + '&pageSize=' + pageSize;
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }



    var _count = function (pageNum, pageSize) {
        var myUrl = serviceBase + 'Accounts/count';
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

    var _getAll = function () {
        var myUrl = serviceBase + 'Acounts/getAll';
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

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


    var _getAllAccounts = function (type = 0, level = 0) {
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

    var _getByIDTree = function (AccId) {
        var myUrl = serviceBase + 'Acounts/getByIDTree?AccountID=' + AccId;

        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }


    var _getByID = function (AccId) {
        var myUrl = serviceBase + 'Acounts/getByID?AccountID=' + AccId;

        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }


    var _getAccountBoxByTypesForEntry = function (entryType) {
        var myUrl = serviceBase + 'Acounts/GetAccountBoxByTypesForEntry?EntryType=' + entryType;
        return $http.get(myUrl).then(function (result) {
            return result;
        }, function (error) { });
    }


    var _getAccountBoxByTypesForBill = function (billType, accType, CompBran) {
        var myUrl = serviceBase + 'Acounts/GetAccountBoxByTypesForBill?BillType=' + billType + '&AccType=' + accType + '&CompBran=' + CompBran;
        return $http.get(myUrl).then(function (result) {
            return result;
        }, function (error) { });
    }




    var _GetGoldBoxByTypesForBill = function (accType, CompBran) {
        var myUrl = serviceBase + 'Acounts/GetGoldBoxByTypesForBill?AccType=' + accType + '&CompBran=' + CompBran;
        return $http.get(myUrl).then(function (result) {
            return result;
        }, function (error) { });
    }











    var _getDefaultAccountsOfBillSettings = function (accType) {
        var myUrl = serviceBase + 'Acounts/GetDefaultAccountsOfBillSettings?AccType=' + accType;
        return $http.get(myUrl).then(function (result) {
            return result;
        }, function (error) { });
    }




    var _getMainAccounts = function () {
        var myUrl = serviceBase + 'Accounts/GetAllMainAccounts';
        return $http.get(myUrl).then(function (result) {
            return result;
        });
    }




    var _getCustomerAccountsForComplexEntry = function (accType) {
        var myUrl = serviceBase + 'Accounts/GetCustomerAccountsForComplexEntry';
        return $http.get(myUrl).then(function (result) {
            return result;
        }, function (error) { });
    }    

    var _GetAccountsFilteredByType = function (accType) {
        var myUrl = serviceBase + 'Accounts/GetAccountsFilteredByType?AccountType=' + accType;
        return $http.get(myUrl).then(function (result) {
            return result;
        }, function (error) { });
    }


    var _searchAccounts = function (searchCriteria) {
        var myUrl = serviceBase + 'Accounts/searchAccounts?SearchCriteria=' + searchCriteria;
        return $http.get(myUrl).then(function (results) {
            return results;
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
    accountsServiceFactory.getAll = _getAll;
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
    accountsServiceFactory.getByID = _getByID;
    accountsServiceFactory.getByIDTree = _getByIDTree;
    accountsServiceFactory.getAccountBoxByTypesForEntry = _getAccountBoxByTypesForEntry;
    accountsServiceFactory.getAccountBoxByTypesForBill = _getAccountBoxByTypesForBill;
    accountsServiceFactory.getDefaultAccountsOfBillSettings = _getDefaultAccountsOfBillSettings;
    accountsServiceFactory.getMainAccounts = _getMainAccounts;
    accountsServiceFactory.getCustomerAccountsForComplexEntry = _getCustomerAccountsForComplexEntry;
    accountsServiceFactory.GetAccountsFilteredByType = _GetAccountsFilteredByType;


    accountsServiceFactory.GetGoldBoxByTypesForBill = _GetGoldBoxByTypesForBill;

    accountsServiceFactory.searchAccounts = _searchAccounts;
    // customerServiceFactory.getCustomerSupplier = _getCustomerSupplier;
    // customerServiceFactory.getAllCustomerSupplier = _getAllCustomerSupplier;

    return accountsServiceFactory;

}]);