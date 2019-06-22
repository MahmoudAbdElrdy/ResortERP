'use strict';
app.controller('headerController', ['$scope', '$q', '$location', '$route', '$window', 'headerService', 'authService', '$timeout', 'NotificationsService', 'MessageService', '$rootScope', 'goldWorldPriceService', 'localStorageService', function ($scope, $q, $location, $route, $window, headerService, authService, $timeout, NotificationsService, MessageService, $rootScope, goldWorldPriceService, localStorageService) {

    $rootScope.$on("CallGetLogo", function () {
        $scope.GetLogo();
    });

    $scope.hasGoldModule = true;

    $scope.userModel = [];
    $scope.getUserModel = function () {
        if (localStorageService.get('UserModule') != null && localStorageService.get('UserModule') != undefined) {
            $scope.userModel = localStorageService.get('UserModule').UserModule;
            debugger
        }
    }

    $scope.getByUserModel = function (Id) {
        debugger
        for (var i = 0; i < $scope.userModel.length; i++) {
            if (parseInt(Id) == parseInt($scope.userModel[i])) {
                return true;
            }
        }
        return false;
    }

    $scope.GetLogo = function () {

        headerService.GetLogo().then(function (response) {
            $scope.logo = response.data;
        },
            function (err) {
                $scope.message = err.error_description;
                console.log(err.message);
            });
    };

    $scope.authentication = authService.authentication;

    $scope.getAllOnLoad = function () {
        $q.all([
            //$scope.getAllCountries(),
            $scope.GetAllNotifcations(),
            $scope.GetAllMessages(),
            $scope.getLastGoldWorldPrice(),
            $scope.getUserModel(),
            //$scope.getAllUnreadUserNotification(),
            //$scope.getAllreadedUserNotification()

        ]).then(function (allResponses) {
            if (false/*!$scope.getByUserModel(8)*/) {
                //$("#HideTable").remove();
                $scope.hasGoldModule = false;
                $scope.GoldWorldPrice24 = 0;
                $scope.GoldWorldPrice22 = 0;
                $scope.GoldWorldPrice21 = 0;
                $scope.GoldWorldPrice18 = 0;
                $scope.KiloPrice = 0;
            }
            //if all the requests succeeded, this will be called, and $q.all will get an
            //array of all their responses.
            //  console.log(allResponses[0].data);

        }, function (error) {
            //This will be called if $q.all finds any of the requests erroring.
            var abc = error;
            var def = 99;
        });
    }

    $scope.HeaderPageLoad = function () {
        $scope.getAllOnLoad();
    }


    $scope.NotifcationsCount = 0;
    $scope.unReadListCount = 0;



    $scope.ChangeLanguage = function (lang) {
        var UID = authService.GetUserID();
        var langID = lang == 'ar' ? 1 : 2;
        var userName = authService.GetUserName();
        headerService.changeLanguage(userName, UID, langID).then(function (result) {
            var res = result.data;
            authService.SetLanguageID(langID);
            $window.location.reload();
        });
    }

    $scope.GetAllNotifcations = function () {
        $scope.NotificationsList = [];
        var UserID = authService.GetUserID();
        var langID = authService.GetLanguageID();
        NotificationsService.GetAllUnreadUserNotification(UserID, langID).then(function (result) {
            var follow = false;
            if (!follow) {
                $scope.unReadList = result.data.Notifications;
                var unReadLength = result.data.Notifications.length;
                $scope.unReadListCount = unReadLength;
                for (var i = 0; i < unReadLength; i++) {
                    $scope.unReadList[i].isReaded = false;
                    $scope.NotificationsList.push($scope.unReadList[i]);
                }
                follow = true
            }
            if (follow)
                NotificationsService.GetAllReadedUserNotification(UserID, langID).then(function (result) {
                    $scope.readedList = result.data.Notifications;
                    var readedLength = result.data.Notifications.length;
                    for (var i = 0; i < readedLength; i++) {
                        $scope.readedList[i].isReaded = true;
                        $scope.NotificationsList.push($scope.readedList[i]);
                    }
                    $scope.NotifcationsCount = $scope.NotificationsList.length;
                });
        });
    };

    $scope.MessagesCount = 0;
    $scope.unReadMsgListCount = 0;

    $scope.GetAllMessages = function () {
        $scope.MessagesList = [];
        var UserID = authService.GetUserID();
        var langID = authService.GetLanguageID();
        MessageService.GetAllUnreadUserMessage(UserID, langID).then(function (result) {
            var follow = false;
            if (!follow) {
                $scope.unReadMsgList = result.data.Messages;
                var unReadMsgLength = result.data.Messages.length;
                $scope.unReadMsgListCount = unReadMsgLength;
                for (var i = 0; i < unReadMsgLength; i++) {
                    $scope.unReadMsgList[i].isMsgReaded = false;
                    $scope.MessagesList.push($scope.unReadMsgList[i]);
                }
                follow = true
            }
            if (follow)
                MessageService.GetAllReadedUserMessage(UserID, langID).then(function (result) {
                    $scope.readedMsgList = result.data.Messages;
                    var readedMsgLength = result.data.Messages.length;
                    for (var i = 0; i < readedMsgLength; i++) {
                        $scope.readedMsgList[i].isMsgReaded = true;
                        $scope.MessagesList.push($scope.readedMsgList[i]);
                    }
                    $scope.MessagesCount = $scope.MessagesList.length;
                });
        });
    };

    $scope.UpdateReadStatus = function (messageID) {
        var langID = authService.GetLanguageID();
        MessageService.UpdateMessageReadStatus(messageID, langID).then(function (result) {
            var res = result.data;
            if (res)
                $scope.GetAllMessages();
        });
    };

    $scope.updateReadedNotifcation = function () {
        if ($scope.unReadListCount > 0 && $scope.unReadListCount != null && $scope.unReadListCount != undefined) {
            var UserID = authService.GetUserID();
            var langID = authService.GetLanguageID();
            NotificationsService.UpdateUserNotificationReadStatus(UserID, langID).then(function (result) {
                var res = result.data;
                if (res)
                    $scope.GetAllNotifcations();
            });
        }
    };



    $scope.getLastGoldWorldPrice = function () {

        goldWorldPriceService.getLastGoldWorldPriceData().then(function (response) {
            var result = response.data.GoldPrice;
            $scope.GoldWorldPrice24 = (parseFloat(result)).toFixed(2);
            $scope.GoldWorldPrice22 = (parseFloat(result) * (22 / 24)).toFixed(2);
            $scope.GoldWorldPrice21 = (parseFloat(result) * (21 / 24)).toFixed(2);
            $scope.GoldWorldPrice18 = (parseFloat(result) * (18 / 24)).toFixed(2);

            $scope.KiloPrice = parseFloat(response.data.KiloPrice).toFixed(2);

        })
    }


}]);