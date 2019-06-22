'use strict'
app.factory('currencyCategoriesService', ['$http', 'sharedService', function ($http, sharedService) {

    var serviceBase = sharedService.getBaseUrl();
    var categoryFactory = {};

    var _get = function (pageNum, pageSize) {
        var myUrl = serviceBase + 'category/gatCat?pageNum=' + pageNum + '&pageSize=' + pageSize;
        return $http.get(myUrl).then(function (result) {
            return result;
        }, function (error) {
        });
    }


    var _getByCurrID = function (currId) {
        var myUrl = serviceBase + 'category/gatByCurrId?currId=' + currId;
        return $http.get(myUrl).then(function (result) {
            return result;
        }, function (error) {
        });
    }

    var _getByCurrIdCatId = function (currId, catId) {
        var myUrl = serviceBase + 'category/gatByCurrIdCatId?currId=' + currId + '&catId=' + catId;
        return $http.get(myUrl).then(function (result) {
            return result;
        }, function (error) {
        });
    }



    var _count = function () {
        var myUrl = serviceBase + 'category/countCat';
        return $http.get(myUrl).then(function (result) {
            return result
        });
    }

    var _add = function (entity) {
        var myUrl = serviceBase + 'category/insertCat';
        var data = entity;
        return $http({
            method: 'POST',
            url: myUrl,
            data: JSON.stringify(data)
        }).then(function (result) {
            return result;
        });
    }

    var _update = function (entity) {
        var myUrl = serviceBase + 'category/updateCat';
        var data = entity;
        return $http({
            method: 'POST',
            url: myUrl,
            data: JSON.stringify(data)
        }).then(function (result) {
            return result;
        });
    }


    var _delete = function (entity) {
        var myUrl = serviceBase + 'category/deleteCat';
        var data = entity;
        return $http({
            method: 'POST',
            url: myUrl,
            data: JSON.stringify(data)
        }).then(function (result) {
            return result;
        });
    }


    var _getLastCode = function (pageNum, pageSize) {
        var myUrl = serviceBase + 'category/gatLastCode';
        return $http.get(myUrl).then(function (result) {
            return result;
        }, function (error) {
        });
    }

    categoryFactory.add = _add;
    categoryFactory.count = _count;
    categoryFactory.update = _update;
    categoryFactory.get = _get;
    categoryFactory.delete = _delete;
    categoryFactory.getByCurrID = _getByCurrID;
    categoryFactory.getByCurrIdCatId = _getByCurrIdCatId;
    categoryFactory.getLastCode = _getLastCode;

    return categoryFactory;

}]);