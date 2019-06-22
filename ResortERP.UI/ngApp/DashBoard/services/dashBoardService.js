'use strict';
app.factory('dashBoardService', ['$http', 'sharedService', function ($http, sharedService) {

    var serviceBase = sharedService.getBaseUrl();

    var logFileReportServiceFactory = {};

    var _get = function (id) {
        var myUrl = serviceBase + 'DashBoard/get?id='+id;
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    var _save = function (entity) {
        var myUrl = serviceBase + 'DashBoard/save';
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

    logFileReportServiceFactory.get = _get;
    logFileReportServiceFactory.save = _save;
    //logFileReportServiceFactory.getHM = _getHM;
    return logFileReportServiceFactory;
}]);