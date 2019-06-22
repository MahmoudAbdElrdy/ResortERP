'use strict';
app.factory('entryService', ['$http', 'sharedService', function ($http, sharedService) {

    var serviceBase = sharedService.getBaseUrl();

    var entryServiceFactory = {};


    var _get = function (entryType, pageNum, pageSize) {
        var myUrl = serviceBase + 'entryMaster/get?entryType=' + entryType + '&pageNum=' + pageNum + '&pageSize=' + pageSize;

        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }
    var _count = function (entryType) {
        var myUrl = serviceBase + 'entryMaster/count?entryType=' + entryType;

        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }



    var _update = function (entity) {
        var myUrl = serviceBase + 'entryMaster/update';
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
        var myUrl = serviceBase + 'entryMaster/delete';
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
    //Insert into entryMaster 
    var _add = function (entity) {
        var myUrl = serviceBase + 'entryMaster/add';
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


    //Insert into entryDetails

    var _addEntryDetails = function (entryDetails) {
        var myUrl = serviceBase + 'entryDetails/insert';
        var data = {};
        data = entryDetails;
        return $http({
            method: 'POST',
            url: myUrl,
            data: data
        }).then(function (results) {
            return results;
        });
    }


    var _getAllMainItemGroups = function () {
        var myUrl = serviceBase + 'ItemsGroups/getMainItemGroupsPos';

        return $http.get(myUrl).then(function (results) {
            return results;
        })
    }

    var _getItemsByGroupID = function (groupID) {
        var myUrl = serviceBase + 'Items/GetItemsByItemGroup?GroupID=' + groupID;
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    var _GetLastEntryNumber = function (entrySetting) {
        var myUrl = serviceBase + 'entryMaster/GetLastEntryNumber?settingID=' + entrySetting;
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    //Get All t-pay types

    var _getAllTPayTypes = function () {
        var myUrl = serviceBase + 'tPayTypes/getAll';

        return $http.get(myUrl).then(function (results) {
            return results;
        })
    }

    //Get All SellsTypes

    var _getAllSELLS_TYPES = function () {
        var myUrl = serviceBase + 'SELLS_TYPES/getAll';

        return $http.get(myUrl).then(function (results) {
            return results;
        })
    }

    //GetAllCompanyStores
    var _getAllCompanyStores = function () {
        var myUrl = serviceBase + 'companyStores/getAll';

        return $http.get(myUrl).then(function (results) {
            return results;
        })
    }

    //getAllBoxAccounts
    var _getAllBoxAccounts = function () {
        var myUrl = serviceBase + 'Acounts/GetAllBoxAccounts';

        return $http.get(myUrl).then(function (results) {
            return results;
        })
    }

    //GetAllEmployeeOfTypeSales
    var _getAllEmployeeOfTypeSales = function () {
        var myUrl = serviceBase + 'employee/GetAllSalesEmployees';
        return $http.get(myUrl).then(function (results) {
            return results;
        })
    }


    var _getAllentryDetails = function (billID) {
        var myUrl = serviceBase + 'entryDetails/GetAllentryDetailsByBill_ID?Bill_ID=' + billID;
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }
    var _getItemNameUsingItemID = function (ItemID) {
        var myUrl = serviceBase + 'Items/GetItemNameUsingItemID?ItemID=' + ItemID;

        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }


    var _getItemDataUsingItemCode = function (ItemCode, sellTypeID) {
        var myUrl = serviceBase + 'Items/GetItemDataUsingItemCode?ItemCode=' + ItemCode + '&sellTypeID=' + sellTypeID;
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    //Search for items with search criteria
    var _searchAccounts = function (searchCriteria) {
        var myUrl = serviceBase + 'Accounts/searchAccounts?SearchCriteria=' + searchCriteria;
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }


    var _getAllCurrencies = function () {
        var myUrl = serviceBase + 'currency/getAll';
        return $http.get(myUrl).then(function (results) {
            return results;
        })
    }

    var _addEntryMasterDetails = function (entryMasterDetails) {
        var myUrl = serviceBase + 'entryMaster/InsertMasterDetails';
        var data = {};
        data = entryMasterDetails;
        return $http({
            method: 'POST',
            url: myUrl,
            data: JSON.stringify(data)
        }).then(function (results) {
            return results;
        });
    }


    var _getAccountDataByCode = function (accountCode, type) {
        var myUrl = serviceBase + 'Accounts/GetAccountDataByCode?AccountCode=' + accountCode + '&Type=' + 1;
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }



    var _getDetailsByEntryId = function (entryId) {
        var myUrl = serviceBase + 'entryMaster/GetDetailsByEntryId?EntryId=' + entryId;
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }


    var _getSettingByID = function (settingID) {
        var myUrl = serviceBase + 'entrySetting/getSettingBySettingID?SettingID=' + settingID;
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }



    var _getTypeBySettingID = function (settingID) {
        var myUrl = serviceBase + 'entrySetting/getEntryTypeBySettingID?SettingID=' + settingID;
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }



    var _addEntryMaster = function (entryMaster) {
        var myUrl = serviceBase + 'entryMaster/insert';
        var data = {};
        data = entryMaster;
        return $http({
            method: 'POST',
            url: myUrl,
            data: JSON.stringify(data)
        }).then(function (results) {
            return results;
        });
    }


    var _addEntryBill = function (entryMasterDetails) {
        var myUrl = serviceBase + 'entryMaster/InsertEntryBill';
        var data = {};
        data = entryMasterDetails;
        return $http({
            method: 'POST',
            url: myUrl,
            data: JSON.stringify(data)
        }).then(function (results) {
            return results;
        });
    }
    

    var _InsertEntryAsset = function (entryMasterDetails) {
        var myUrl = serviceBase + 'entryMaster/InsertEntryAsset';
        var data = {};
        data = entryMasterDetails;
        return $http({
            method: 'POST',
            url: myUrl,
            data: JSON.stringify(data)
        }).then(function (results) {
            return results;
        });
    }

    var _getEntryByBillID = function (billID) {
        var myUrl = serviceBase + 'entryMaster/getEntryByBillID?billID=' + billID;
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }


    var _getEntryMasterByBill = function (billID) {
        var myUrl = serviceBase + 'entryMaster/getEntryMasterByBill?billID=' + billID;
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    var _getEntryMasterByAssetOperation = function (assetOperationId) {
        var myUrl = serviceBase + 'entryMaster/getEntryMasterByAssetOperation?assetOperationId=' + assetOperationId;
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }


    var _getEntryByEntryNumber = function (entryNumber, entryType) {
        var myUrl = serviceBase + 'entryMaster/getEntryByEntryNumber?entryNumber=' + entryNumber + '&entryType=' + entryType;
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }


    var checkEntryByEntryNumber = function (entryNumber, entryType) {
        var myUrl = serviceBase + 'entryMaster/checkEntryByEntryNumber?entryNumber=' + entryNumber + '&entryType=' + entryType;
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }


    var getDailyEntryByMsterID = function (masterID) {
        var myUrl = serviceBase + 'entryMaster/getDailyEntryByMsterID?masterID=' + masterID;
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }



    var getEntryByEntryID = function (masterID) {
        var myUrl = serviceBase + 'entryMaster/getEntryByEntryID?masterID=' + masterID;
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }



    var getEntryByaccountID = function (accountID) {
        var myUrl = serviceBase + 'entryMaster/getEntryByAccountID?accountID=' + accountID;
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }


    var getEntryByCustID = function (customerID) {
        var myUrl = serviceBase + 'entryMaster/getEntryByCustID?customerID=' + customerID;
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }




    var updateEntryOfBill = function (entity) {
        var myUrl = serviceBase + 'entryMaster/updateEntryOfBill';
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



    //var deleteEntryOfBill = function (billID) {
    //    var myUrl = serviceBase + 'entryMaster/deleteEntryOfBill';
    //    var data = {};
    //    data = billID;
    //    return $http({
    //        method: 'POST',
    //        url: myUrl,
    //        data: JSON.stringify(data)
    //    }).then(function (results) {
    //        return results;
    //    });
    //}


    var deleteEntryOfBill = function (billID) {
        var myUrl = serviceBase + 'entryMaster/deleteEntryOfBill?billID=' + billID;
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    var _getAllBankAccounts = function (billID) {
        var myUrl = serviceBase + 'Acounts/getAllBankAccounts';
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }


    ////////////////////////////////////serach accounts with filteration
    var _searchAccountsForDepitGrid = function (searchCriteria, entryTypeId) {
        var myUrl = serviceBase + 'Accounts/SearchAccountsForDepitGrid?SearchCriteria=' + searchCriteria + '&EntryTypeID=' + entryTypeId;
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }


    var _searchAccountsForCreditGrid = function (searchCriteria, entryTypeId) {
        var myUrl = serviceBase + 'Accounts/SearchAccountsForCreditGrid?SearchCriteria=' + searchCriteria + '&EntryTypeID=' + entryTypeId;
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    var _getNotPostedEntries = function (EntrySearchObj) {
        var myUrl = serviceBase + 'entryMaster/getNotPostedEntries';
        var data = {};
        data = EntrySearchObj;
        return $http({
            method: 'POST',
            url: myUrl,
            data: JSON.stringify(data)
        }).then(function (results) {
            return results;
        });
    }

    var _SetEntriesPosted = function (entryIds) {
        var myUrl = serviceBase + 'entryMaster/SetEntriesPosted';
        var data = {};
        data = entryIds;
        return $http({
            method: 'POST',
            url: myUrl,
            data: JSON.stringify(data)
        }).then(function (results) {
            return results;
        });
    }

    entryServiceFactory.get = _get;
    entryServiceFactory.count = _count;
    entryServiceFactory.add = _add;
    entryServiceFactory.update = _update;
    entryServiceFactory.delete = _delete;
    entryServiceFactory.getAllMainItemGroups = _getAllMainItemGroups;
    entryServiceFactory.getItemsByGroupID = _getItemsByGroupID;
    entryServiceFactory.GetLastEntryNumber = _GetLastEntryNumber;
    entryServiceFactory.getAllTPayTypes = _getAllTPayTypes;
    entryServiceFactory.getAllSELLS_TYPES = _getAllSELLS_TYPES;
    entryServiceFactory.getAllCompanyStores = _getAllCompanyStores;
    entryServiceFactory.getAllBoxAccounts = _getAllBoxAccounts;
    entryServiceFactory.getAllEmployeeOfTypeSales = _getAllEmployeeOfTypeSales;
    entryServiceFactory.addEntryDetails = _addEntryDetails;
    entryServiceFactory.getAllentryDetails = _getAllentryDetails;
    entryServiceFactory.getItemNameUsingItemID = _getItemNameUsingItemID;
    entryServiceFactory.getItemDataUsingItemCode = _getItemDataUsingItemCode;
    entryServiceFactory.searchAccounts = _searchAccounts;
    entryServiceFactory.getAllCurrencies = _getAllCurrencies;
    entryServiceFactory.addEntryMasterDetails = _addEntryMasterDetails;
    entryServiceFactory.getAccountDataByCode = _getAccountDataByCode;
    entryServiceFactory.getDetailsByEntryId = _getDetailsByEntryId;
    entryServiceFactory.getSettingByID = _getSettingByID;
    entryServiceFactory.getTypeBySettingID = _getTypeBySettingID;
    entryServiceFactory.addEntryMaster = _addEntryMaster;
    entryServiceFactory.addEntryBill = _addEntryBill; 
    entryServiceFactory.InsertEntryAsset= _InsertEntryAsset;

    entryServiceFactory.getEntryByBillID = _getEntryByBillID;
    entryServiceFactory.getEntryMasterByBill = _getEntryMasterByBill;
    entryServiceFactory.getEntryMasterByAssetOperation = _getEntryMasterByAssetOperation;
    entryServiceFactory.getEntryByEntryNumber = _getEntryByEntryNumber;
    entryServiceFactory.checkEntryByEntryNumber = checkEntryByEntryNumber;
    entryServiceFactory.getDailyEntryByMsterID = getDailyEntryByMsterID;
    entryServiceFactory.getEntryByEntryID = getEntryByEntryID;
    entryServiceFactory.getEntryByaccountID = getEntryByaccountID;
    entryServiceFactory.getEntryByCustID = getEntryByCustID;
    entryServiceFactory.updateEntryOfBill = updateEntryOfBill;
    entryServiceFactory.deleteEntryOfBill = deleteEntryOfBill;
    entryServiceFactory.getAllBankAccounts = _getAllBankAccounts;

    entryServiceFactory.searchAccountsForDepitGrid = _searchAccountsForDepitGrid;
    entryServiceFactory.searchAccountsForCreditGrid = _searchAccountsForCreditGrid;

    entryServiceFactory.getNotPostedEntries = _getNotPostedEntries;
    entryServiceFactory.SetEntriesPosted = _SetEntriesPosted;

    return entryServiceFactory;
}]); 