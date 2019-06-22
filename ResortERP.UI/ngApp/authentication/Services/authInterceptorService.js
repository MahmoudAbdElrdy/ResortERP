'use strict';
app.factory('authInterceptorService', ['$q', '$location', 'localStorageService', '$filter', function ($q, $location, localStorageService, $filter) {

    var authInterceptorServiceFactory = {};

    var _request = function (config) {
        var canceler = $q.defer();
        config.timeout = canceler.promise;

        config.headers = config.headers || {};
        /*
        config.headers.common =  {};

        config.headers.post = {};
        config.headers.put =  {};
        config.headers.patch = {};
        */
        /*
        config.headers.common = config.headers.common|| {};
        
        config.headers.post =  config.headers.post || {};
        config.headers.put =  config.headers.put ||{};
        config.headers.patch = config.headers.patch || {};
        */
        var authData = localStorageService.get('authorizationData');
        if (authData) {
            //debugger;
            var lang = authData.LanguageID == null || authData.LanguageID == undefined ? "ar" : authData.LanguageID == 1 ? "ar" : "en";
            config.headers.Authorization = 'Bearer ' + authData.token;
            //config.headers['x-csrf-token'] = 'HM';
            config.headers.userNameLog = authData.userName;
            if (localStorageService.get('branchId') != null) {
                config.headers.COMBRNID = localStorageService.get('branchId').branchId;
            } else {
                config.headers.COMBRNID = "";
            }
            config.headers.Language = lang;
            if (config.method == "POST")
                config.headers.myCustomUrl = $location.url();

            var userdata = localStorageService.get('authorizationData');
            var userName = userdata != null ? userdata.userName : "";
            if (userName.split("@").length > 1) {
                var userPermissions = localStorageService.get("UserPermissions");
                var userCurrentViewPermission =
                    $filter('filter')(userPermissions, { MenuUrl: $location.$$url });
                if (userCurrentViewPermission != undefined &&
                    userCurrentViewPermission != null &&
                    userCurrentViewPermission.length > 0) {
                    if (config.method == "POST" &&
                        config.url != undefined &&
                        config.url != null &&
                        config.url != "" &&
                        (config.url.indexOf("add") !== -1 ||
                        config.url.indexOf("insert") !== -1||
                        config.url.indexOf("update") !== -1 ||
                        config.url.indexOf("edit") !== -1 ||
                        config.url.indexOf("delete") !== -1 ||
                        config.url.indexOf("remove") !== -1)) {
                        if ((config.url.indexOf("add") !== -1 || config.url.indexOf("insert") !== -1) && userCurrentViewPermission[0].OpAdd == false) {
                            swal({
                                title: 'الصلاحيات',
                                text: 'ليس لديك صلاحيات الاضافة لهذه الصفحة',
                                type: 'error',
                                showConfirmButton: false
                            });
                            canceler.resolve();
                        }
                        else if ((config.url.indexOf("update") !== -1 || config.url.indexOf("edit") !== -1) && userCurrentViewPermission[0].OpUpdate == false) {
                            swal({
                                title: 'الصلاحيات',
                                text: 'ليس لديك صلاحيات التعديل لهذه الصفحة',
                                type: 'error',
                                showConfirmButton: false
                            });
                            canceler.resolve();
                        }
                        else if ((config.url.indexOf("delete") !== -1 || config.url.indexOf("remove") !== -1) && userCurrentViewPermission[0].OpDelete == false) {
                            swal({
                                title: 'الصلاحيات',
                                text: 'ليس لديك صلاحيات الحذف لهذه الصفحة',
                                type: 'error',
                                showConfirmButton: false
                            });
                            canceler.resolve();
                        }
                    }
                }
            }
        }

        return config;
    }

    var _responseError = function (rejection) {
        if (rejection.status === 401) {
            $location.path('/login');
        }
        return $q.reject(rejection);
    }

    authInterceptorServiceFactory.request = _request;
    authInterceptorServiceFactory.responseError = _responseError;

    return authInterceptorServiceFactory;
}]);