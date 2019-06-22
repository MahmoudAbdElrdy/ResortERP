'use strict';
app.factory('MessageService', ['$http', 'sharedService', function ($http, sharedService) {

    var serviceBase = sharedService.getBaseUrl();

    var MessagesServiceFactory = {};

    var _GetAllMessage = function (lang) {
        var myUrl = serviceBase + 'Message/GetAllMessage/' + lang;
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    var _GetAllUserMessage = function (userid, lang) {
        var myUrl = serviceBase + 'Message/GetAllUserMessage/' + userid + '/' + lang;
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    var _GetAllUnreadUserMessage = function (userid, lang) {
        var myUrl = serviceBase + 'Message/GetAllUnreadUserMessage/' + userid + '/' + lang;
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    var _GetAllReadedUserMessage = function (userid, lang) {
        var myUrl = serviceBase + 'Message/GetAllReadedUserMessage/' + userid + '/' + lang;
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    var _GetAllConfirmedUserMessage = function (userid, lang) {
        var myUrl = serviceBase + 'Message/GetAllConfirmedUserMessage/' + userid + '/' + lang;
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    var _GetAllUnConfirmedUserMessage = function (userid, lang) {
        var myUrl = serviceBase + 'Message/GetAllUnConfirmedUserMessage/' + userid + '/' + lang;
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    var _UpdateMessageReadStatus = function (messageid, lang) {
        var myUrl = serviceBase + 'Message/UpdateMessageReadStatus/' + messageid + '/' + lang;
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    var _UpdateMessageReceivedStatus = function (messageid, lang) {
        var myUrl = serviceBase + 'Message/UpdateMessageReceivedStatus/' + messageid + '/' + lang;
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    var _UpdateUserMessageReadStatus = function (userid, lang) {
        var myUrl = serviceBase + 'Message/UpdateUserMessageReadStatus/' + userid + '/' + lang;
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    var _add = function (entity) {
        var myUrl = serviceBase + 'Message/insert';
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
        var myUrl = serviceBase + 'Message/update';
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
        var myUrl = serviceBase + 'Message/delete/' + Id;
        return $http({
            method: 'delete',
            url: myUrl
        }).then(function (results) {
            return results;
        });
    }

    var _getByID = function (Id) {
        var myUrl = serviceBase + 'Message/' + Id;
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    var _count = function () {
        var myUrl = serviceBase + 'Message/count';
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    var _getPagedData = function (pageNum, pageSize) {
        var myUrl = serviceBase + 'Message/getPagedData/' + pageNum + '/' + pageSize;
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    MessagesServiceFactory.GetAllMessage = _GetAllMessage;
    MessagesServiceFactory.GetAllUserMessage = _GetAllUserMessage;
    MessagesServiceFactory.GetAllUnreadUserMessage = _GetAllUnreadUserMessage;
    MessagesServiceFactory.GetAllReadedUserMessage = _GetAllReadedUserMessage;
    MessagesServiceFactory.GetAllConfirmedUserMessage = _GetAllConfirmedUserMessage;
    MessagesServiceFactory.GetAllUnConfirmedUserMessage = _GetAllUnConfirmedUserMessage;
    MessagesServiceFactory.UpdateMessageReadStatus = _UpdateMessageReadStatus;
    MessagesServiceFactory.UpdateMessageReceivedStatus = _UpdateMessageReceivedStatus;
    MessagesServiceFactory.UpdateUserMessageReadStatus = _UpdateUserMessageReadStatus;
    MessagesServiceFactory.add = _add;
    MessagesServiceFactory.update = _update;
    MessagesServiceFactory.delete = _delete;
    MessagesServiceFactory.getByID = _getByID;
    MessagesServiceFactory.count = _count;
    MessagesServiceFactory.getPagedData = _getPagedData;

    return MessagesServiceFactory;

}]);