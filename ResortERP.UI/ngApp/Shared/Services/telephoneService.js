'use strict';
app.factory('telephoneService', ['$http', 'sharedService', function ($http, sharedService) {

    var serviceBase = sharedService.getBaseUrl();

    var telephoneServiceFactory = {};

    var _get = function (pageNum, pageSize) {
        var myUrl = serviceBase + 'telephone/get?pageNum=' + pageNum + '&pageSize=' + pageSize;
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    var _getTeleCat = function () {
        var myUrl = serviceBase + 'teleCat/getAll';
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    var _getBy = function (ContactID, typeID) {
        var myUrl = serviceBase + 'telephone/getby?ContactID=' + ContactID + '&typeID=' + typeID;
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    var _count = function () {
        var myUrl = serviceBase + 'telephone/count';
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    var _update = function (entity) {
        var myUrl = serviceBase + 'telephone/update';
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
        var myUrl = serviceBase + 'telephone/delete';
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

    var _deleteByTeleID = function (entity) {
        var myUrl = serviceBase + 'telephone/deleteByTeleID';
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
        var myUrl = serviceBase + 'telephone/insert';
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

    var _insertList = function (entity) {
        var myUrl = serviceBase + 'telephone/insertList';
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


    var _getBytype = function (typeID) {
        var myUrl = serviceBase + 'telephone/getByType?typeID=' + typeID;
        return $http.get(myUrl).then(function (result) {
            return result;
        }, function (error) { });
    }



    var _getTypeNameByID = function (typeID) {
        var myUrl = serviceBase + 'telephoneCat/getByID?typeID=' + typeID;
        return $http.get(myUrl).then(function (result) {
            alert(result);
            alert(result.data);
            return result;
        }, function (error) { });
    }

    

    telephoneServiceFactory.get = _get;
    telephoneServiceFactory.getBy = _getBy;
    telephoneServiceFactory.count = _count;
    telephoneServiceFactory.insert = _insert;
    telephoneServiceFactory.update = _update;
    telephoneServiceFactory.delete = _delete;
    telephoneServiceFactory.insertList = _insertList;
    telephoneServiceFactory.deleteByTeleID = _deleteByTeleID;
    telephoneServiceFactory.getTeleCat = _getTeleCat;
    telephoneServiceFactory.getByType = _getBytype;
    telephoneServiceFactory.getTypeNameByID = _getTypeNameByID;

    return telephoneServiceFactory;

}]);