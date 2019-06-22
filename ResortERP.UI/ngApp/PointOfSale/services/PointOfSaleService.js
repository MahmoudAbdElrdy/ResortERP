'use strict';
app.factory('PointOfSaleService', ['$http', 'sharedService', function ($http, sharedService) {

    var serviceBase = sharedService.getBaseUrl();

    var PointOfSaleServiceFactory = {};


    var _get = function (branchId, billType,pageNum, pageSize) {
        var myUrl = serviceBase + 'billMaster/get?pageNum=' + pageNum + '&pageSize=' + pageSize + '&billType=' + billType + '&branchId=' + branchId;

        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }
    var _count = function (billType) {
        var myUrl = serviceBase + 'billMaster/count?billType=' + billType;

        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }



    var _update = function (entity) {
        var myUrl = serviceBase + 'billMaster/update';
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
        var myUrl = serviceBase + 'billMaster/delete';
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
    //Insert into BillMaster 
    var _add = function (entity) {
        var myUrl = serviceBase + 'billMaster/add';
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


    //Insert into BillDetails
    
    var _addBillDetails = function (billDetails) {
        var myUrl = serviceBase + 'billDetails/addBillDetails';
        var data = {};
        data = billDetails;
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

    var _GetLastBillNumber = function (branchId, settingID) {
        var myUrl = serviceBase + 'billMaster/GetLastBillNumber?settingID=' + settingID + '&branchId=' + branchId;
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
        var myUrl = serviceBase +'SELLS_TYPES/getAll';

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

    //GetAllCustomersAccounts
    var _getAllCustomersAccounts = function (billType) {
        var myUrl = serviceBase + 'Acounts/GetAllCustomerAccounts?Type=' + billType;

        return $http.get(myUrl).then(function (results) {
            return results;
        })
    }


    //GetAllBoxAccounts
    var _getAllBoxAccounts = function () {
        var myUrl = serviceBase + 'Acounts/GetAllBoxAccounts';

        return $http.get(myUrl).then(function (results) {
            return results;
        })
    }


    //GetAllGoldBoxAccounts
    var _getAllGoldBoxAccounts = function () {
        var myUrl = serviceBase + 'Acounts/GetAllGoldBoxAccounts';

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


    var _getAllBillDetails = function (billID) {
        var myUrl = serviceBase + 'billDetails/GetAllBillDetailsByBill_ID?Bill_ID=' + billID;

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

    
    var _getItemDataUsingItemCode = function (ItemCode,sellTypeID) {
        var myUrl = serviceBase + 'Items/GetItemDataUsingItemCode?ItemCode=' + ItemCode + '&sellTypeID=' + sellTypeID;

        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }


    var _getItemCurrentQuantity = function (ItemID, StoreID) {
        var myUrl = serviceBase + 'Items/getItemCurrentQuantity?ItemID=' + ItemID + '&StoreID=' + StoreID;

        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }



    //Search for items with search criteria
    var _searchItems = function (searchCriteria) {
        var myUrl = serviceBase + 'Items/SearchItems?SearchCriteria=' + searchCriteria;
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



    var _getByBillID = function (billID) {
        var myUrl = serviceBase + 'billMaster/getByID?billID=' + billID ;

        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }



    var _updateEntryID = function (entity, entryID) {
        var myUrl = serviceBase + 'billMaster/updateEntryID'; 

        var data = { BillMaster: entity, EntryID: entryID };
       // var data = {};
        //data = entity, entryID;
        return $http({
            method: 'POST',
            url: myUrl,
            data: JSON.stringify(data)
        }).then(function (results) {
            return results;
        });
    }



    var getBillByBillNumber = function (billNo, billType) {
        var myUrl = serviceBase + 'billMaster/getBillByBillNumber?billNo=' + billNo + '&billType=' + billType;

        return $http.get(myUrl).then(function (results) {
            return results;
        })
    }

    var checkBillByBillNumber = function (billNo, billType) {
        var myUrl = serviceBase + 'billMaster/checkBillByBillNumber?billNo=' + billNo + '&billType=' + billType;

        return $http.get(myUrl).then(function (results) {
            return results;
        })
    }



    var getPaginationByType = function (billsettingID) {
        var myUrl = serviceBase + 'billMaster/getPaginationByType?billSettingID=' + billsettingID;

        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }



    var getAllBillsBySettingID = function (billsettingID) {
        var myUrl = serviceBase + 'billMaster/GetAllBillsBySettingID?settingID=' + billsettingID;
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }



    var getBillsForSearch = function (BillSearchObj) {
        var myUrl = serviceBase + 'billMaster/getBillsForSearch';
        var data = {};
        data = BillSearchObj;
        return $http({
            method: 'POST',
            url: myUrl,
            data: JSON.stringify(data)
        }).then(function (results) {
            return results;
        });
    }

    var _getNotPotedBills = function (BillSearchObj) {
        var myUrl = serviceBase + 'billMaster/getNotPotedBills';
        var data = {};
        data = BillSearchObj;
        return $http({
            method: 'POST',
            url: myUrl,
            data: JSON.stringify(data)
        }).then(function (results) {
            return results;
        });
    }

    var _SetBillsPosted = function (BillIds) {
        var myUrl = serviceBase + 'billMaster/SetBillsPosted';
        var data = {};
        data = BillIds;
        return $http({
            method: 'POST',
            url: myUrl,
            data: JSON.stringify(data)
        }).then(function (results) {
            return results;
        });
    }

    PointOfSaleServiceFactory.get = _get;
    PointOfSaleServiceFactory.count = _count;
    PointOfSaleServiceFactory.add = _add;
    PointOfSaleServiceFactory.update = _update;
    PointOfSaleServiceFactory.delete = _delete;
    PointOfSaleServiceFactory.getAllMainItemGroups = _getAllMainItemGroups;
    PointOfSaleServiceFactory.getItemsByGroupID = _getItemsByGroupID;
    PointOfSaleServiceFactory.GetLastBillNumber = _GetLastBillNumber;
    PointOfSaleServiceFactory.getAllTPayTypes = _getAllTPayTypes;
    PointOfSaleServiceFactory.getAllSELLS_TYPES = _getAllSELLS_TYPES;
    PointOfSaleServiceFactory.getAllCompanyStores = _getAllCompanyStores;
    PointOfSaleServiceFactory.getAllCustomersAccounts = _getAllCustomersAccounts;
    PointOfSaleServiceFactory.getAllBoxAccounts = _getAllBoxAccounts;
    PointOfSaleServiceFactory.getAllGoldBoxAccounts = _getAllGoldBoxAccounts;
    PointOfSaleServiceFactory.getAllEmployeeOfTypeSales = _getAllEmployeeOfTypeSales;
    PointOfSaleServiceFactory.addBillDetails = _addBillDetails;
    PointOfSaleServiceFactory.getAllBillDetails = _getAllBillDetails;
    PointOfSaleServiceFactory.getItemNameUsingItemID = _getItemNameUsingItemID;
    PointOfSaleServiceFactory.getItemDataUsingItemCode = _getItemDataUsingItemCode;
    PointOfSaleServiceFactory.searchItems = _searchItems;
    PointOfSaleServiceFactory.getAllCurrencies = _getAllCurrencies;
    PointOfSaleServiceFactory.getByBillID = _getByBillID;
    PointOfSaleServiceFactory.updateEntryID = _updateEntryID;
    PointOfSaleServiceFactory.getBillByBillNumber = getBillByBillNumber;
    PointOfSaleServiceFactory.checkBillByBillNumber = checkBillByBillNumber;
    PointOfSaleServiceFactory.getPaginationByType = getPaginationByType;
    PointOfSaleServiceFactory.getAllBillsBySettingID = getAllBillsBySettingID;
    PointOfSaleServiceFactory.getBillsForSearch = getBillsForSearch;
    PointOfSaleServiceFactory.getItemCurrentQuantity = _getItemCurrentQuantity;
    PointOfSaleServiceFactory.getNotPotedBills = _getNotPotedBills;
    PointOfSaleServiceFactory.SetBillsPosted = _SetBillsPosted;

    return PointOfSaleServiceFactory;
}]);