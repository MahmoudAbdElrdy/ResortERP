'use strict';
app.directive('isAvailable', function ($timeout, $q) {
    return {
        restrict: 'AE',
        require: 'ngModel',
        link: function (scope, elm, attr, model) {
            model.$asyncValidators.userExists = function (userName) {

                var myUrl = serviceBase + 'User/GetUserObj?userName=' + userName;
                return $http.get(myUrl).then(function (results) {
                    return results;
                });

                var defer = $q.defer();
                $timeout(function () {
                    model.$setValidity('isExists', false);
                    defer.resolve;
                }, 1000);
                return defer.promise;
            };
        }
    }
}).directive('authSrc', function ($http) {
    return {
        restrict: 'A',
        scope: {
            authSrc: "="
        },
        link: function (scope, element) {
            var onSuccess = function (response) {
                var data = response.data;
                var url = window.URL || window.webkitURL;
                var src = url.createObjectURL(data);
                element.attr("src", src);
            };

            var onError = function (response) {
                console.log("failed to load image");
            };

            scope.$watch('authSrc', function (newValue) {
                if (newValue) {
                    $http({
                        method: "GET",
                        url: scope.authSrc,
                        responseType: "blob"
                    }).then(onSuccess, onError)
                }
            });


        }
    }
}).directive('login', function ($rootScope, $compile) {
    return {
        restrict: 'E',
        scope: false,
        templateUrl: function (scope, element) {
            var tempurl = '../../NgApp/authentication/Views/' + $rootScope.language + '/login.html';
            $rootScope.$watch(function () { return '../../NgApp/authentication/Views/' + $rootScope.language + '/login.html'; }
                , function (newVal, oldVal) {
                    if (newVal && newVal !== oldVal) {
                        element.$$element.html(newVal);
                        $compile(element.$$element)(scope);
                    }
                });
            return tempurl;
        }
    }
});