app.directive('forgetPassword', function ($rootScope, $compile) {
    return {
        restrict: 'E',
        scope: false,
        templateUrl: function (scope, element) {
            var tempurl = '../../NgApp/ForgetPassword/Views/' + $rootScope.language + '/forgetPassword.html';
            $rootScope.$watch(function () { return '../../NgApp/ForgetPassword/Views/' + $rootScope.language + '/forgetPassword.html'; }
                , function (newVal, oldVal) {
                    if (newVal && newVal !== oldVal) {
                        element.$$element.html(newVal);
                        $compile(element.$$element)(scope);
                    }
                });
            return tempurl;
        }
    }
})