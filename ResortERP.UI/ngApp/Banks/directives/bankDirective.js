app.directive('bank', function ($rootScope, $compile) {
    return {

        restrict: 'E',
        scope: false,
        templateUrl: function (scope, element) {
            var tempurl = '../../NgApp/Banks/views/' + $rootScope.language + '/bank.html';
            $rootScope.$watch(function () { return '../../NgApp/Banks/views/' + $rootScope.language + '/bank.html'; }
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