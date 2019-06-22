'use strict';
app.factory('logFileReportService', ['$http', 'sharedService', function ($http, sharedService) {

    var serviceBase = sharedService.getBaseUrl();

    var logFileReportServiceFactory = {};

    var _get = function (pageNum, pageSize) {
        var myUrl = serviceBase + 'LogFileReport/GetAll';
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    var _getAllUsers = function () {
        var myUrl = serviceBase + 'LogFileReport/getAllUsers';
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    //var _getHM = function (id) {
    //    var myUrl = serviceBase + 'LogFileReport/Edit?ID='+id;
    //    return $http.get(myUrl).then(function (results) {
    //        return results;
    //    });
    //}
   
    var _getAllCompBranches = function () {
        var myUrl = serviceBase + 'LogFileReport/getAllBranshes';
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    var _count = function (Forms1, startDate, endDate, UID, COM_BRN_ID) {
        var Forms = JSON.stringify(Forms1);
        var myUrl = serviceBase + 'LogFileReportServices/count?StartDate=' + startDate + "&EndDate=" + endDate + "&UID=" + UID + "&branchId=" + COM_BRN_ID;
            serviceBase + 'LogFileReportServices/count';
            return $http({
                method: 'Post',
                url: myUrl,
                data: Forms
            }).then(function (results) {
                return results;
            });
    }

    var _getBySelForms = function (ARName) {
        var myUrl = serviceBase + 'LogFileReport/GetBySelForms?ARName=' + ARName;
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    var _getByRangDate = function (Forms1, startDate, endDate, UID, COM_BRN_ID, pageNum, pageSize) {
        var Forms = JSON.stringify(Forms1);
        var myUrl = serviceBase + 'LogFileReport/GetByRangDate?StartDate=' + startDate + "&EndDate=" + endDate + "&UID=" + UID + "&branchId=" + COM_BRN_ID + '&pageNum=' + pageNum + '&pageSize=' + pageSize;
        return $http({
            method: 'Post',
            url: myUrl,
            data: Forms
        }).then(function (results) {
            return results;
        });

        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    logFileReportServiceFactory.count = _count;
    logFileReportServiceFactory.get = _get;
    logFileReportServiceFactory.getBySelForms = _getBySelForms;
    logFileReportServiceFactory.getByRangDate = _getByRangDate;
    logFileReportServiceFactory.getAllUsers = _getAllUsers;
    logFileReportServiceFactory.getAllCompBranches = _getAllCompBranches;
    //logFileReportServiceFactory.getHM = _getHM;
    return logFileReportServiceFactory;
}]);