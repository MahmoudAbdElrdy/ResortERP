'use strict';
app.controller('activationController', ['$scope', '$location', 'authService', 'activationService', function ($scope, $location, authService, activationService) {


    $scope.getActivation = function () {
        debugger;
        $scope.UID = $location.search().UID;
        $scope.activationCod = $location.search().AC.trim();
        $scope.GetUserByID($scope.UID).then(function (result) {
            $scope.userObj = result.data;
            if ($scope.userObj.IS_ACTIVATED === true) {
                $scope.showMsg = true;
                $scope.helloMsgAr = "تم تفعيل هذا الحساب سابقاً";
                $scope.msgAr = "برجاء التواصل مع الدعم الفني للنظام";
                $scope.helloMsgEn = "This account has already been activated";
                $scope.msgEn = "Please contact the technical support of the system";
            } else {
                if ($scope.userObj.COMPANY_ACTIVATION_CODE == $scope.activationCod) {
                    var endDate = new Date($scope.userObj.ActivationRequestedDateTime).addDays(2);
                    if (endDate >= new Date()) {
                        $scope.ActivateUser($scope.UID).then(function (result) {
                            if (result.data == true) {
                                $scope.showMsg = true;
                                $scope.helloMsgAr = " مرحباً بك " + $scope.userObj.COMPANY_AR_NAME + " في نظام ResortERP";
                                $scope.msgAr = "لقد تم تفعيل حسابك بنجاح";
                                $scope.helloMsgEn = " Welcome " + $scope.userObj.COMPANY_AR_NAME + " in ResortERP system";
                                $scope.msgEn = "Your account has been successfully activated";
                            } else {
                                $scope.showMsg = false;
                                $scope.helloMsgAr = "حدث خطأ";
                                $scope.msgAr = "برجاء التواصل مع الدعم الفني للنظام";
                                $scope.helloMsgEn = "An error occurred";
                                $scope.msgEn = "Please contact the technical support of the system";
                            }
                        }, function (error) {
                            $scope.showMsg = false;
                            $scope.helloMsgAr = "حدث خطأ";
                            $scope.msgAr = "برجاء التواصل مع الدعم الفني للنظام";
                            $scope.helloMsgEn = "An error occurred";
                            $scope.msgEn = "Please contact the technical support of the system";
                            console.log(error.message);
                        });
                    } else {
                        $scope.showMsg = false;
                        $scope.helloMsgAr = "عفواً لقد تم انتهاء صلاحية هذا الرابط";
                        $scope.msgAr = "برجاء التواصل مع الدعم الفني للنظام";
                        $scope.helloMsgEn = "Sorry, this link has expired";
                        $scope.msgEn = "Please contact the technical support of the system";
                    }
                }
                else {
                    $scope.showMsg = false;
                    $scope.helloMsgAr = "عفواً تأكد من صلاحية هذا الرابط";
                    $scope.msgAr = "برجاء التواصل مع الدعم الفني للنظام";
                    $scope.helloMsgEn = "Oops, make sure this link is valid";
                    $scope.msgEn = "Please contact the technical support of the system";
                }
            }
        }, function (error) {
            $scope.showMsg = false;
            $scope.helloMsgAr = "حدث خطأ";
            $scope.msgAr = "برجاء التواصل مع الدعم الفني للنظام";
            $scope.helloMsgEn = "An error occurred";
            $scope.msgEn = "Please contact the technical support of the system";
            console.log(error.message);
        });

    };

    Date.prototype.addDays = function (days) {
        var dat = new Date(this.valueOf());
        dat.setDate(dat.getDate() + days);
        return dat;
    };
    $scope.GetUserByID = function (id) {
        return activationService.getInnerAdminById(id);
    };
    $scope.ActivateUser = function (id) {
        return activationService.activateUser(id);
    };

    /*******************************************************************************************************************/
    $scope.act = { uName: null, password: null, activationCode: null };

    $scope.activateCompany = function () {
        if ($scope.act != undefined && $scope.act != null && $scope.act.activationCode != null) {
            $scope.getdata();
            activationService.activatCompany($scope.act).then(
                function (response) {
                    if (response.status === 200 && response.data === true) {
                        swal({
                            title: '! Congratulation',
                            text: 'Your user activated successfully',
                            timer: 2000,
                            type: "success",
                            showConfirmButton: false
                        }).then(function () {
                        }, function (dismiss) {
                            if (dismiss === 'timer') {
                                console.log('user activated successfully');
                            }
                        });
                        $location.path('/login');
                    } else { swal("Warning", "Activation code is wrong", "warning"); }
                }, function (error, data, status) {

                });
        }
        else swal("Warning", "Activation code mustn't be null", "warning");
    };

    $scope.getdata = function () {
        $scope.act.uName = $location.search().logData.userName;
        $scope.act.password = $location.search().logData.password;
    };

    $scope.Register = function () {
        $location.path('/Registeration');
    };

}]);