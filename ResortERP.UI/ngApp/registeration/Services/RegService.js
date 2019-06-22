'use strict';
app.factory('RegService', ['$http', 'sharedService', '$upload', '$location', function ($http, sharedService, $upload, $location) {

    var serviceBase = sharedService.getBaseUrl();

    var UserServiceFactory = {};

    var _add = function (entity) {
        var myUrl = serviceBase + 'User/add';
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

    var _registerCompany = function (file, entity) {
        if (file) {
            return $upload.upload({
                url: serviceBase + 'api/registerCompany',
                fields: entity,
                file: file
            }).success(function (data, status, headers, config) {
                //console.log(config);
                swal({
                    title: 'تم',
                    text: 'حفظ بيانات الشركة بنجاح',
                    type: 'success',
                    timer: 1500,
                    showConfirmButton: false
                });
            }).error(function (error) {
                console.log("Error!", error);
                swal('عفواً',
                    'حدث خطأ عند حفظ بيانات الشركة',
                    'error');
            });
        }
    }

    var _ValidateReg = function (entity) {
        var myUrl = serviceBase + 'User/ValidateReg';
        var data = {};
        data = entity;
        return $http({
            method: 'POST',
            url: myUrl,
            data: JSON.stringify(data)
        }).then(function (results) {
            return results;
        });
    };

    var _checkEmail = function (email) {
        var myUrl = serviceBase + 'User/CheckEmail?email=' + email;
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }
    var _checkUserName = function (userName) {
        var myUrl = serviceBase + 'User/CheckUserName?userName=' + userName;
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }
    var _checkARCompany = function (companyArName) {
        var myUrl = serviceBase + 'User/CheckARCompany?companyArName=' + companyArName;
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }
    var _checkENCompany = function (companyEnName) {
        var myUrl = serviceBase + 'User/CheckENCompany?companyEnName=' + companyEnName;
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    UserServiceFactory.add = _add;
    UserServiceFactory.ValidateReg = _ValidateReg;
    UserServiceFactory.checkEmail = _checkEmail;
    UserServiceFactory.checkUserName = _checkUserName;
    UserServiceFactory.checkARCompany = _checkARCompany;
    UserServiceFactory.checkENCompany = _checkENCompany;
    UserServiceFactory.registerCompany = _registerCompany;

    return UserServiceFactory;
}]);