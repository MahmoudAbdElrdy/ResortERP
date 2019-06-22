app.directive('storesGuide', function ($rootScope, $compile) {
    return {
        restrict: 'E',
        scope: false,
        templateUrl: function (scope, element) {
            var tempurl = '../../NgApp/StoresGuide/views/' + $rootScope.language + '/storesGuide.html';
            $rootScope.$watch(function () { return '../../NgApp/StoresGuide/views/' + $rootScope.language + '/storesGuide.html'; }
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