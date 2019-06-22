'use strict';
app.factory('billPayTypesService', ['$http', 'sharedService', function ($http, sharedService) {

    var serviceBase = sharedService.getBaseUrl();

    var billPayTypesFactory = {};

    var _get = function (pageNum, pageSize) {
        var myUrl = serviceBase + 'billPaytypes/get?pageNum=' + pageNum + '&pageSize=' + pageSize;
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }


    var _count = function () {
        var myUrl = serviceBase + 'billPaytypes/count';
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    var _update = function (entity) {
        var myUrl = serviceBase + 'billPaytypes/update';
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
        var myUrl = serviceBase + 'billPaytypes/delete';
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
        var myUrl = serviceBase + 'billPaytypes/insert';
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



    //var insertWithMasterID = function (payTypesList, masterID) {
    //    var myUrl = serviceBase + 'billPaytypes/insertWithMasterID';
    //    var data = { PayList: payTypesList, BillMasterID: masterID };
    //    // data = entity;
    //    return $http({
    //        method: 'POST',
    //        url: myUrl,
    //        data: data
    //    }).then(function (results) {
    //        return results;
    //    });
    //}



    var insertWithMasterID = function (payTypesList) {
        var data = {};
        data = payTypesList;
        var myUrl = serviceBase + 'billPaytypes/insertWithMasterID';
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





    var _getByMasterID = function (masterID) {
        var myUrl = serviceBase + 'billPaytypes/getByMasterID?masterID=' + masterID;
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }



    billPayTypesFactory.get = _get;
    billPayTypesFactory.count = _count;
    billPayTypesFactory.insert = _insert;
    billPayTypesFactory.update = _update;
    billPayTypesFactory.delete = _delete;
    billPayTypesFactory.getByMasterID = _getByMasterID;
    billPayTypesFactory.insertWithMasterID = insertWithMasterID;

    return billPayTypesFactory;

}]);