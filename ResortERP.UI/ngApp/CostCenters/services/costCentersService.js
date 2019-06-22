'use strick';
app.factory('costCentersService', ['$http', 'sharedService', function ($http, sharedService) {

    var serviceBase = sharedService.getBaseUrl();
    var costCentersServiceFactory = {};

    var _get = function (pageNum, pageSize) {
        debugger;
        var myUrl = serviceBase + 'costCenters/get?pageNum=' + pageNum + '&pageSize=' + pageSize;
        return $http.get(myUrl).then(function (result) {

            return result;
        }, function (error) {
        });
    }


    var _add = function (entity) {
        var myUrl = serviceBase + 'costCenters/add';
        var data = {};
        data = entity;
        return $http({
            method: 'POST',
            url: myUrl,
            data: JSON.stringify(data)
        }).then(function (result) {
            return result;
        }, function (error) {
        });
    }

    var _getMainCostCenters = function () {
        debugger;
        var myUrl = serviceBase + 'costCenters/getMainCostCenters';
        return $http.get(myUrl).then(function (result) {
            return result;
        }, function (error) {

        });
    }


    costCentersServiceFactory.get = _get;
    costCentersServiceFactory.add = _add;
    costCentersServiceFactory.getMainCostCenters = _getMainCostCenters;
    return costCentersServiceFactory;
}]);