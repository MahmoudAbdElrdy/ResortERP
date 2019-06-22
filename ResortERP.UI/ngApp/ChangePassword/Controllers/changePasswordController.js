'use strict';
app.controller('changePasswordController', ['$scope', '$location', '$log', '$q', 'changePasswordService', 'authService', function ($scope, $location, $log, $q, changePasswordService, authService) {

    $scope.changePass = {};

    $scope.getAllOnLoad = function () {
        $q.all([
        ]).then(function (allResponses) {
        }, function (error) {
        });
    }

    $scope.changePswrd = function () {
        var userID = authService.GetUserID();
        var userName = authService.GetUserName();
        var password = $scope.changePass.NewPass;
        changePasswordService.changePassword(userID, userName, password).then(
            function (result) {
                $scope.changePass = {};
                swal({
                    title: 'تم',
                    text: 'تغيير كلمة السر بنجاح',
                    type: 'success',
                    timer: 1500,
                    showConfirmButton: false
                }).then(function () {
                }, function (dismiss) {
                    // dismiss can be 'cancel', 'overlay', 'close', and 'timer'
                    if (dismiss === 'timer') {
                        $location.path('/home');
                        $scope.$apply();
                    }
                });
            }, function (error) {
            });
    }

    $scope.ValidatePassword = function () {
        var userID = authService.GetUserID();
        var userName = authService.GetUserName();
        var password = $scope.changePass.CurrentPass;
        return changePasswordService.ValidateCurrentPassword(userID, userName, password).then(
            function (result) {
                if (result.data == true) {
                    $scope.IsExist = true;
                } else {
                    $scope.IsExist = false;
                }
            }, function (error) {
                $scope.IsExist = false;
            });
    }

    $scope.pageChanged = function () {
        $scope.getAllOnLoad();
    };
}]);