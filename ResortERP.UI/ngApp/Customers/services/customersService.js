'use strict';
app.factory('customersService', ['$http', 'sharedService', function ($http, sharedService) {

    var serviceBase = sharedService.getBaseUrl();

    var customerServiceFactory = {};

    var _get = function (pageNum, pageSize, type) {
        var myUrl = serviceBase + 'customer/get?pageNum=' + pageNum + '&pageSize=' + pageSize + '&type=' + type;
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }


    var _getByIdLog = function (custId,type) {
        var myUrl = serviceBase + 'CustomerSupplier/getByIdLog?custId=' + custId + '&type=' + type;
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    var _getById = function (custId) {
        var myUrl = serviceBase + 'customer/getById?id=' + custId ;
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }


    var _getCustomerSupplier = function (pageNum, pageSize, type) {
        var myUrl = serviceBase + 'CustomerSupplier/get?pageNum=' + pageNum + '&pageSize=' + pageSize + '&type=' + type;
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    var _getAllCustomerSupplier = function (type) {
        var myUrl = serviceBase + 'CustomerSupplier/getAll?type=' + type;
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    var _getAll = function () {
        var myUrl = serviceBase + 'customer/getAll';
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    var _count = function (type) {
        var myUrl = serviceBase + 'customer/count?type=' + type;
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    var _update = function (entity) {
        var myUrl = serviceBase + 'customer/update';
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
        var myUrl = serviceBase + 'customer/delete';
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
        var myUrl = serviceBase + 'customer/insert';
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

    var _InsertUpdateCustomerDependence = function (entity, teleList, CustBranList, trans) {
       
        var myUrl = serviceBase + 'customer/InsertUpdateCustomerDependence';
        var data = { obj: { customer: entity, telephones: teleList, customerBran: CustBranList, transaction: trans } };
        return $http({
            method: 'POST',
            url: myUrl,
            data: JSON.stringify(data),
            contentType: 'application/json; charset=utf-8',
            dataType: 'json'
        }).then(function (results) {
            return results;
        }, function (error) {
            console.log(error.data.message)
        });
    }


    var _getLastCode = function () {
        var myUrl = serviceBase + 'customer/getLastCode';
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    customerServiceFactory.get = _get;
    customerServiceFactory.getById = _getById;
    customerServiceFactory.getAll = _getAll;
    customerServiceFactory.count = _count;
    customerServiceFactory.insert = _insert;
    customerServiceFactory.InsertUpdateCustomerDependence = _InsertUpdateCustomerDependence;
    customerServiceFactory.update = _update;
    customerServiceFactory.delete = _delete;
    customerServiceFactory.getCustomerSupplier = _getCustomerSupplier;
    customerServiceFactory.getAllCustomerSupplier = _getAllCustomerSupplier;
    customerServiceFactory.getLastCode = _getLastCode;
    customerServiceFactory.getByIdLog = _getByIdLog;
    return customerServiceFactory;

}]);