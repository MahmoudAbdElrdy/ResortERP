app.directive('quickAccount', function ($rootScope, $compile) {
    return {

        restrict: 'E',
        scope: false,
        templateUrl: function (scope, element) {
            var tempurl = '../../NgApp/QuickAccount/views/' + $rootScope.language + '/quickAccount.html';
            $rootScope.$watch(function () { return '../../NgApp/QuickAccount/views/' + $rootScope.language + '/quickAccount.html'; }
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