'use strict';
app.factory('itemsService', ['$http', 'sharedService', function ($http, sharedService) {

    var serviceBase = sharedService.getBaseUrl();
    var itemsServiceFactory = {};


    var _get = function (pageNum, pageSize) {
        var myUrl = serviceBase + 'Items/get?pageNum=' + pageNum + '&pageSize=' + pageSize;

        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }
    var _getByID = function (itemID) {
        var myUrl = serviceBase + 'Items/getByID?itemID=' + itemID;

        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }
    var _getAll = function () {
        var myUrl = serviceBase + 'Items/getAll';
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }
    var _count = function () {
        var myUrl = serviceBase + 'Items/count';

        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }



    var _update = function (entity) {
        var myUrl = serviceBase + 'Items/update';
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

    var _chkValidat = function (code, arName, enName) {
        var myUrl = serviceBase + 'Items/validat?Code=' + code + '&ArName=' + arName + '&EnName=' + enName;

        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    var _delete = function (entity) {
        var myUrl = serviceBase + 'Items/delete';
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
    var _add = function (entity) {
        // debugger;
        var myUrl = serviceBase + 'Items/add';
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

    var _SaveItemUnits = function (itemUnitsList, deletedItemUnits) {
        debugger;
        
        var data = { ItemUnitsList: itemUnitsList, DeltedItemUnits: deletedItemUnits };
        var myUrl = serviceBase + 'Items/SaveItemUnits';
        //var data = { ItemUnitsList: itemUnitsList, DeltedItemUnits: deletedItemUnits };
        return $http({

            url: myUrl,
            method: 'POST',
            data: data
        }).then(function (results) {
            return results;
        }, function (error) {
            alert(error.data);
        });
    }

    var _getSearch = function (entity, selectedBillSettingsList, selectedReportOptionList) {
        var data = { searchItm: entity, bilSettings: selectedBillSettingsList, rptOptions: selectedReportOptionList };
        var myUrl = serviceBase + 'RPT_ITEM_MOTION_VIEW/getSearch';
        return $http({
            url: myUrl,
            method: 'POST',
            data: data
        }).then(function (results) {
            return results;
        }, function (error) {
            alert(error.data);
        });
    }

    var _getAllMainItemGroups = function () {
        var myUrl = serviceBase + 'ItemsGroups/getMainItemGroups';

        return $http.get(myUrl).then(function (results) {
            return results;
        })
    }


    var _getAllItemCalibers = function () {

        var myUrl = serviceBase + 'ItemCaliber/getAllItemCalibers';

        return $http.get(myUrl).then(function (results) {
            return results;
        })
    }

    var _getAllCostCalculationType = function () {
        var myUrl = serviceBase + 'costCalculationType/getCostCalculationType';

        return $http.get(myUrl).then(function (results) {
            return results;
        })
    }


    var _getAllItemStatus = function () {
        var myUrl = serviceBase + 'itemStatus/getItemStatus';

        return $http.get(myUrl).then(function (results) {
            return results;
        })
    }



    var _getAllItemUnits = function (itemId) {
        var myUrl = serviceBase + 'Items/GetItemsUnits?ItemID=' + itemId;

        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }


    var _getLastItemCodeByGroup = function (groupID) {
        var myUrl = serviceBase + 'Items/getLastItemCodeByGroup?groupID=' + groupID;

        return $http.get(myUrl).then(function (results) {
            return results;
        })
    }

    //GetAllCustomersAccounts
    var _getAllCustomersAccounts = function () {
        //  debugger;
        var myUrl = serviceBase + 'Acounts/GetAllCustomerAccounts?Type=' + 1;

        return $http.get(myUrl).then(function (results) {
            return results;
        })
    }

    var _getUnitDataUsingUnitCode = function (UnitCode) {
        var myUrl = serviceBase + 'units/GetUnitDataUsingUnitCode?UnitCode=' + UnitCode;
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }




    var _getItemGroupByID = function (groupID) {
        var myUrl = serviceBase + 'ItemsGroups/getItemGroupByID?groupID=' + groupID;
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }



    var _getSystemOptionsByUserName = function (userName) {
        var myUrl = serviceBase + 'sysOpt/getByUserName?userName=' + userName;
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }



    var _getLastItemCode = function () {
        var myUrl = serviceBase + 'Items/getLastItemCode';
        return $http.get(myUrl).then(function (results) {
            return results;
        })
    }

    itemsServiceFactory.getByID = _getByID;
    itemsServiceFactory.get = _get;
    itemsServiceFactory.getSearch = _getSearch;
    itemsServiceFactory.getAll = _getAll;
    itemsServiceFactory.count = _count;
    itemsServiceFactory.add = _add;
    itemsServiceFactory.update = _update;
    itemsServiceFactory.delete = _delete;
    itemsServiceFactory.getAllItemUnits = _getAllItemUnits;
    itemsServiceFactory.SaveItemUnits = _SaveItemUnits;
    itemsServiceFactory.getAllMainItemGroups = _getAllMainItemGroups;
    itemsServiceFactory.getLastItemCodeByGroup = _getLastItemCodeByGroup;
    itemsServiceFactory.getAllCustomersAccounts = _getAllCustomersAccounts;
    itemsServiceFactory.getUnitDataUsingUnitCode = _getUnitDataUsingUnitCode;
    itemsServiceFactory.chkValidat = _chkValidat;   
    itemsServiceFactory.getAllItemCalibers = _getAllItemCalibers;
    itemsServiceFactory.getAllItemStatus = _getAllItemStatus;
    itemsServiceFactory.getAllCostCalculationType = _getAllCostCalculationType;
    itemsServiceFactory.getItemGroupByID = _getItemGroupByID;
    itemsServiceFactory.getSystemOptionsByUserName = _getSystemOptionsByUserName;
    itemsServiceFactory.getLastItemCode = _getLastItemCode;

    return itemsServiceFactory;

}]);