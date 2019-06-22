'use strict';
app.factory('NotificationsService', ['$http', 'sharedService', function ($http, sharedService) {

    var serviceBase = sharedService.getBaseUrl();

    var NotificationsServiceFactory = {};

    var _GetAllNotification = function (lang) {
        var myUrl = serviceBase + 'Notification/GetAllNotification/' + lang;
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    var _GetAllUserNotification = function (userid, lang) {
        var myUrl = serviceBase + 'Notification/GetAllUserNotification/' + userid + '/' + lang;
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    var _GetAllUnreadUserNotification = function (userid, lang) {
        var myUrl = serviceBase + 'Notification/GetAllUnreadUserNotification/' + userid + '/' + lang;
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    var _GetAllReadedUserNotification = function (userid, lang) {
        var myUrl = serviceBase + 'Notification/GetAllReadedUserNotification/' + userid + '/' + lang;
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    var _GetAllConfirmedUserNotification = function (userid, lang) {
        var myUrl = serviceBase + 'Notification/GetAllConfirmedUserNotification/' + userid + '/' + lang;
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    var _GetAllUnConfirmedUserNotification = function (userid, lang) {
        var myUrl = serviceBase + 'Notification/GetAllUnConfirmedUserNotification/' + userid + '/' + lang;
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    var _UpdateNotificationConfirmedStatus = function (userid, lang) {
        var myUrl = serviceBase + 'Notification/UpdateNotificationConfirmedStatus/' + userid + '/' + lang;
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    var _UpdateUserNotificationReadStatus = function (userid, lang) {
        var myUrl = serviceBase + 'Notification/UpdateUserNotificationReadStatus/' + userid + '/' + lang;
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    var _add = function (entity) {
        var myUrl = serviceBase + 'Notification/insert';
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

    var _update = function (entity) {
        var myUrl = serviceBase + 'Notification/update';
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

    var _delete = function (Id) {
        var myUrl = serviceBase + 'Notification/delete/' + Id;
        return $http({
            method: 'delete',
            url: myUrl
        }).then(function (results) {
            return results;
        });
    }

    var _getByID = function (Id) {
        var myUrl = serviceBase + 'Notification/' + Id;
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    var _count = function () {
        var myUrl = serviceBase + 'Notification/count';
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    var _getPagedData = function (pageNum, pageSize) {
        var myUrl = serviceBase + 'Notification/getPagedData/' + pageNum + '/' + pageSize;
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    NotificationsServiceFactory.GetAllNotification = _GetAllNotification;
    NotificationsServiceFactory.GetAllUserNotification = _GetAllUserNotification;
    NotificationsServiceFactory.GetAllUnreadUserNotification = _GetAllUnreadUserNotification;
    NotificationsServiceFactory.GetAllReadedUserNotification = _GetAllReadedUserNotification;
    NotificationsServiceFactory.GetAllConfirmedUserNotification = _GetAllConfirmedUserNotification;
    NotificationsServiceFactory.GetAllUnConfirmedUserNotification = _GetAllUnConfirmedUserNotification;
    NotificationsServiceFactory.UpdateNotificationConfirmedStatus = _UpdateNotificationConfirmedStatus;
    NotificationsServiceFactory.UpdateUserNotificationReadStatus = _UpdateUserNotificationReadStatus;
    NotificationsServiceFactory.add = _add;
    NotificationsServiceFactory.update = _update;
    NotificationsServiceFactory.delete = _delete;
    NotificationsServiceFactory.getByID = _getByID;
    NotificationsServiceFactory.count = _count;
    NotificationsServiceFactory.getPagedData = _getPagedData;

    return NotificationsServiceFactory;

}]);