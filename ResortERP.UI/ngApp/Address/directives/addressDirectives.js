app.directive('address', function ($rootScope, $compile) {
    return {

        restrict: 'E',
        scope: false,
        templateUrl: function (scope, element) {
            var tempurl = '../../NgApp/Address/Views/' + $rootScope.language + '/address.html';
            $rootScope.$watch(function () { return '../../NgApp/Address/Views/' + $rootScope.language + '/address.html'; }
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