app.directive('changePassword', function ($rootScope, $compile) {
    return {
        restrict: 'E',
        scope: false,
        templateUrl: function (scope, element) {
            var tempurl = '../../NgApp/changePassword/Views/' + $rootScope.language + '/changePassword.html';
            $rootScope.$watch(function () { return '../../NgApp/changePassword/Views/' + $rootScope.language + '/changePassword.html'; }
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