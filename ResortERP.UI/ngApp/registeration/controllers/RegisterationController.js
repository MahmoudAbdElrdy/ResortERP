'use strict';
app.controller('RegisterationController', ['$scope', '$location', 'RegService', function ($scope, $location, RegService) {


    $scope.clearEnity = function () {
        $scope.user = { UID: "", UPWD: "", SavePWD: false, SHIFT_NUMBER: null, USER_LETTER: "", USER_TYPE: null, UserGroupID: null, AddedBy: "", AddedOn: null, UpdatedBy: "", updatedOn: null, CS_DataSource: "", CS_InitialCatalog: "", CS_UserId: "", CS_Password: "", DbDescriptionName: "", DbCreationDate: null, DbFirstPeriodDate: null, DbEndPeriodDate: null, COMPANY_AR_NAME: "", COMPANY_EN_NAME: "", COMPANY_AR_ADRESS: "", COMPANY_EN_ADRESS: "", COMPANY_TELEPHONE: "", COMPANY_FAX: "", COMPANY_LOGO: null, DELETEDORNOT: "", COMPANY_ACTIVATION_CODE: "", COMPANY_EMAIL: "", IS_ACTIVATED: false, ConfirmPassword: "", MaxUsersNumber: 1 };
    }
    $scope.clearEnity();

    $scope.saveUser = function () {

        $scope.saveEntity();
    }
    $scope.emailPattern = /^([a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]){1,70}$/;
    $scope.saveEntity = function () {

        if ($scope.user !== undefined && $scope.user !== null && $scope.user.UID !== "" && $scope.user.UPWD !== "" && $scope.user.USER_TYPE !== "" && $scope.user.COMPANY_AR_NAME !== "" && $scope.user.COMPANY_EN_NAME !== "" && $scope.user.COMPANY_EMAIL !== "" && $scope.user.MaxUsersNumber !== "") {
            debugger;
            RegService.ValidateReg($scope.user).then(
                function (response) {
                    var fieldExist = [];
                    fieldExist = response.data;
                    var count = fieldExist.length;
                    if (count > 0) {
                        var message = "<div style='text-align:right;direction:rtl'> من فضلك تأكد من التالي : <br /><br /><ol >";
                        for (var i = 0; i < count; i++) {
                            if (fieldExist[i] == "email") { message += "<li> البريد الالكتروني موجود لدينا سابقا</li>"; }
                            if (fieldExist[i] == "userName") { message += "<li> اسم المستخدم موجود لدينا سابقا</li>"; }
                            if (fieldExist[i] == "nameAr") { message += "<li>اسم الشركة العربي موجود لدينا سابقا</li> "; }
                            if (fieldExist[i] == "nameEn") { message += "<li>اسم الشركة اللاتيني موجود لدينا سابقا</li> </ol>"; }
                        }
                        message += "<br /><br /> برجاء تلافي الملاحظات السابقة لاكتمال تسجيل شركتكم</div>";
                        swal("", message, "warning");
                        return;
                    } else {
                        $scope.registerCompany().then(function (response) {
                            if (response.data === "Register Completed...") {
                                $location.path('/login');
                            } else if (response.data === "Mail not sent...") {
                                swal("تحذير", "لم يتم ارسال بريد الكتروني", "warning");
                            }
                        });
                    }
                }, function () {

                })

            //if ($scope.IsMailExist === true) { swal("Exist !", "Same Email Exists", "error"); return; }


        } else {
            swal('عفواً',
                    'حدث خطأ عند حفظ بيانات الشركة',
                    'error');
        }
    }

    $scope.add = function (entity) {
        return RegService.add(entity);
    }

    $scope.checkEmail = function () {
        RegService.checkEmail($scope.user.COMPANY_EMAIL).then(function (response) {
            $scope.IsMailExist = response.data;
        }, function (error) {
            console.log(error.message);
        });
    }
    $scope.checkUserName = function () {
        RegService.checkUserName($scope.user.UID).then(function (response) {
            $scope.IsUserNameExist = response.data;
        }, function (error) {
            console.log(error.message);
        });
    }
    $scope.checkARCompany = function () {
        RegService.checkARCompany($scope.user.COMPANY_AR_NAME).then(function (response) {
            $scope.IsArCompanyExist = response.data;
        }, function (error) {
            console.log(error.message);
        });
    }
    $scope.checkENCompany = function () {
        RegService.checkENCompany($scope.user.COMPANY_EN_NAME).then(function (response) {
            $scope.IsEnCompanyExist = response.data;
        }, function (error) {
            console.log(error.message);
        });
    }

    $scope.registerCompany = function () {
        var file = $scope.myFile;

        //console.log('file is ');
        //console.dir(file);

        return RegService.registerCompany(file, $scope.user);
    };

    $scope.imageSrc = null;

    $scope.imageUpload = function (event) {
        var files = event.target.files; //FileList object

        for (var i = 0; i < files.length; i++) {
            var file = files[i];
            var reader = new FileReader();
            reader.onload = $scope.imageIsLoaded;
            reader.readAsDataURL(file);
        }
    }

    $scope.imageIsLoaded = function (e) {
        $scope.$apply(function () {
            $scope.imageSrc = e.target.result;
        });
    }

}]);