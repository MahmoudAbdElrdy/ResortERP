'use strict';
app.controller('forgetPasswordController', ['$scope', '$location', '$log', '$q', 'forgetPasswordService', 'authService', function ($scope, $location, $log, $q, forgetPasswordService, authService) {

    $scope.changePass = {};

    $scope.getAllOnLoad = function () {
        $q.all([
            $scope.chkOldPass()
        ]).then(function (allResponses) {
        }, function (error) {
        });
    }

    $scope.chkOldPass = function () {
        var oldpass = $location.search().OPW;
        var userID = Number($location.search().UID);
        var userName = $location.search().UN;
        forgetPasswordService.chkPass(userID, userName, oldpass).then(function (response) {
            var res = response.data;
            if (res) {
                $scope.IsExist = true;
            }
            else {
                $scope.IsExist = false;
            }
        }, function (error) {
            $scope.IsExist = false;
        });
    };

    $scope.ChangePassword = function () {
        var userID = $location.search().UID;
        var userName = $location.search().UN;
        var password = $scope.changePass.NewPass;
        forgetPasswordService.changeingPassword(userID, userName, password).then(function (response) {
            var res = response.data;
            if (res) {
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
                        $location.path('/login');
                        $scope.$apply();
                    }
                });
            }
            else {
                swal("خطأ", "لم يتم تغيير كلمة السر", "error");
            }
        }, function (error) {

        })
    };


    $scope.pageChanged = function () {
        $scope.getAllOnLoad();
    };
}]);