'use strict';
app.factory('billCaliberTransactionsService', ['$http', 'sharedService', function ($http, sharedService) {

    var serviceBase = sharedService.getBaseUrl();

    var billCaliberTransFactory = {};

    var _get = function (pageNum, pageSize) {
        var myUrl = serviceBase + 'billCaliberTrans/get?pageNum=' + pageNum + '&pageSize=' + pageSize;
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }


    var _count = function () {
        var myUrl = serviceBase + 'billCaliberTrans/count';
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    var _update = function (entity) {
        var myUrl = serviceBase + 'billCaliberTrans/update';
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
        var myUrl = serviceBase + 'billCaliberTrans/delete';
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
        var myUrl = serviceBase + 'billCaliberTrans/insert';
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


    var _getByMasterID = function (masterID) {
        var myUrl = serviceBase + 'billCaliberTrans/getByMasterID?masterID=' + masterID;
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }



    var insertWithMasterID = function (transList) {
        var data = {};
        data = transList;
        var myUrl = serviceBase + 'billCaliberTrans/insertWithMasterID';
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


    billCaliberTransFactory.get = _get;
    billCaliberTransFactory.count = _count;
    billCaliberTransFactory.insert = _insert;
    billCaliberTransFactory.update = _update;
    billCaliberTransFactory.delete = _delete;
    billCaliberTransFactory.getByMasterID = _getByMasterID;
    billCaliberTransFactory.insertWithMasterID = insertWithMasterID;
    


    return billCaliberTransFactory;

}]);