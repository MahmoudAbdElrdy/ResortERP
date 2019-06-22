app.directive('systemOptions', function ($rootScope, $compile) {
    return {
        restrict: 'E',
        scope: false,
        templateUrl: function (scope, element) {
            var tempurl = '../../NgApp/SystemOptions/Views/' + $rootScope.language + '/SystemOptions.html';
            $rootScope.$watch(function () { return '../../NgApp/SystemOptions/Views/' + $rootScope.language + '/SystemOptions.html'; }
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