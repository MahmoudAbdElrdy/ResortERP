'use strict';
app.controller('loginController', ['$scope', '$location', 'authService', 'blockUI', 'blockUIConfig', '$sce', '$rootScope', 'compBranchesService', 'userPriviligeBranchesService', 'localStorageService', 'commonService', 'userPrivilagesService', function ($scope, $location, authService, blockUI, blockUIConfig, $sce, $rootScope, compBranchesService, userPriviligeBranchesService, localStorageService, commonService, userPrivilagesService) {

    $scope.loginData = {
        userName: "",
        password: ""
    };
    $scope.branchSelected = {
        ID: null,
        COM_BRN_ID: []
    }

    $scope.message = "";
    $scope.pageNum = 1;
    $scope.pageSize = 10;
    $scope.userBranchesList = [];
    $scope.companyBranchesList = [];
    $scope.tempBrName = "";
    $scope.tempBrEnName = "";
    $scope.userModule = [];

    $scope.GetUserComp = function (name) {
        return userPriviligeBranchesService.getByCN(name);
    }
    
    $scope.getUserPermissions = function () {
        var UserId = authService.GetUserID();
        userPrivilagesService.getAllUserPermissions(UserId).then(function (response) {
            debugger;
            localStorageService.set('UserPermissions', response.data);
        }, function (error) {
            console.log(error);
        });
    }

    $scope.getUserModule = function (Id) {
        commonService.getUserModule(Id).then(function (response) {
            debugger;
            $scope.userModule = response.data; 
            localStorageService.set('UserModule', { UserModule: response.data });
            $scope.authentication.isAuth = true;


        }, function (error) {
            debugger;
            console.log(error.data.message);
        });
    }

    $scope.getBranchData = function () {

        var userName = authService.GetUserName();
        if (userName.split("@").length > 1) {
            $scope.GetUserComp(userName).then(function (results) {
                if (results.data !== "" && results.data !== null && results.data !== undefined && results.data.length !== 0) {
                    $scope.temp = [];
                    $scope.getUserModule(userName.split("@")[1]);
                    $scope.getUserPermissions();

                    $.each(results.data, function (index, value) {
                        $.each($scope.companyBranchesList, function (index1, value1) {
                            if (value1.COM_BRN_ID == value.COM_BRN_ID) {
                                $scope.temp[$scope.temp.length++] = index1;
                            }


                        })
                    })

                    $.each($scope.companyBranchesList, function (index2, value2) {
                        $scope.companyBranchesList[index2].SELECTED = false;
                        $.each($scope.temp, function (index3, value3) {
                            if (value3 == index2) {
                                $scope.userBranchesList[$scope.userBranchesList.length++] = value2;
                            }
                        })
                    });

                    if ($scope.userBranchesList.length > 1) {
                        $("#flagA").css({ 'display': 'block' });
                    } else {
                     //   $scope.authentication.isAuth = true;
                        $location.path('/Home');

                        $rootScope.$emit("CallGetCompany", {});
                        $rootScope.$emit("CallGetLogo", {});
                        
                        $("#tempV").remove();
                        $('.nav-user-wrapper .media').append("<div id=tempV>" + $scope.userBranchesList[0].COM_BRN_AR_NAME + "</div>");
                        localStorageService.set('tempBrName', { tempBrName: $scope.userBranchesList[0].COM_BRN_AR_NAME });
                        localStorageService.set('tempBrEnName', { tempBrEnName: $scope.userBranchesList[0].COM_BRN_EN_NAME });
                       
                        
                        localStorageService.set('branchId', { branchId: $scope.userBranchesList[0].COM_BRN_ID });

                    }


                    //  }
                } else {

                    swal("الصلاحيات", "لا تمتلك صلاحيات على فروع الشركة برجاء الرجوع الى الادمن", "error");
                    $scope.logOut();
                }
            });
        }
        else {
            //$scope.getUserModule(userName);
            $scope.getUserModule(userName);
  
            // alert($scope.companyBranchesList.length)
            $.each($scope.companyBranchesList, function (index, value) {
                //alert(value.IsDefault);
                if (value.IsDefault == true) {
                    $("#tempV").remove();
                    $('.nav-user-wrapper .media').append("<div id=tempV>" + value.COM_BRN_AR_NAME + "</div>");
                    localStorageService.set('tempBrName', { tempBrName: value.COM_BRN_AR_NAME });
                    localStorageService.set('tempBrEnName', { tempBrEnName: value.COM_BRN_EN_NAME });

                    
                    localStorageService.set('branchId', { branchId: value.COM_BRN_ID });
                    //alert(value.COM_BRN_ID)
                }
            });

            $location.path('/Home');
            $rootScope.$emit("CallGetCompany", {});
            $rootScope.$emit("CallGetLogo", {});
        }
    }

    $scope.GetAllComapanyBranches = function () {
        $scope.companyBranchesList = [];
        compBranchesService.getAll().then(function (response) {
            $scope.companyBranchesList = response.data;
            $scope.getBranchData();
        }, function (error) {
            console.log(error.data.message);
        });
    }

   
  //  $scope.getLastGoldWorldPrice();

    $scope.change = function (id) {
        if ($scope.branchSelected.ID != null) {
            $scope.authentication.isAuth = true;
            $location.path('/Home');
            $rootScope.$emit("CallGetCompany", {});
            $rootScope.$emit("CallGetLogo", {});
           // var flagM = false;
            $.each($scope.userBranchesList, function (index1, value1) {
                if (value1.COM_BRN_ID == $scope.branchSelected.ID) {
                    $("#tempV").remove();
                    $('.nav-user-wrapper .media').append("<div id=tempV>"+$scope.userBranchesList[index1].COM_BRN_AR_NAME+"</div>");
                    localStorageService.set('tempBrName', { tempBrName: $scope.userBranchesList[index1].COM_BRN_AR_NAME });
                    
                    localStorageService.set('tempBrEnName', { tempBrEnName: $scope.userBranchesList[index1].COM_BRN_EN_NAME });

                    
                    localStorageService.set('branchId', { branchId: $scope.userBranchesList[index1].COM_BRN_ID });

                }
            })

        } else {
            swal('', 'لابد من اختيار فرع اولا', 'error');
        }
    }


    $scope.login11 = function () {
      
        authService.log_in($scope.loginData).then(function (response) {
            $("#tempV").remove();
            $scope.GetAllComapanyBranches();
          


        }, function (status) {
            if (status === 400) {
                swal("خطأ", "برجاء التحقق من اسم المستخدم وكلمة المرور!", "error");
            } else if (status === 401 || status === -1) {
                swal("الصلاحيات", "لا تمتلك صلاحيات للدخول للنظام", "error");
                //$location.path('/activation').search({ logData: $scope.loginData });;
            } else if (status === 404) {
                swal("خطأ", "برجاء التحقق من اسم المستخدم وكلمة المرور!", "error");
            } else if (status === 500) {
                swal("الصلاحيات", "لا تمتلك صلاحيات للدخول للنظام", "error");
            } else {
                swal("الصلاحيات", "لا تمتلك صلاحيات للدخول للنظام", "error");
            }

            //$scope.message = err.data.message;
        });
    };

    $scope.forgetPassword = function () {
        if ($scope.loginData.userName == "") {
            swal("تحذير", "لابد من ادخال اسم المستخدم", "warning");
        } else {
            authService.ForgetPassword($scope.loginData.userName).then(function (response) {
                if (response.data == true)
                    swal({
                        title: 'تم',
                        text: 'ارسال بريد الكتروني يحتوي رابط يمكنك من تغيير كلمة المرور الخاصة بك',
                        type: 'success',
                        timer: 2000,
                        showConfirmButton: false
                    }).then(function () {
                    }, function (dismiss) {
                        // dismiss can be 'cancel', 'overlay', 'close', and 'timer'
                    });
            }, function (error) { });
        }
    };

    $scope.Register = function () {
        $location.path('/Registeration');
    };

    $scope.logOut = function () {
        authService.logOut();
        $location.path('/login');
    }




    //$scope.getLastGoldWorldPrice = function () {

    //    goldWorldPriceService.GetLastGoldWorldPrice().then(function (response) {
    //        var result = response.data;
    //        $scope.GoldWorldPrice24 = (parseFloat(result)).toFixed(2);

    //        $scope.GoldWorldPrice22 = (parseFloat(result) * (22 / 24)).toFixed(2);
    //        $scope.GoldWorldPrice21 = (parseFloat(result) * (21 / 24)).toFixed(2);
    //        $scope.GoldWorldPrice18 = (parseFloat(result) * (18 / 24)).toFixed(2);

    //    })
    //}

    $scope.authentication = authService.authentication;

}]);