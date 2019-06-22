app.directive('costCalculationType', function ($rootScope, $compile) {
    return {
        restrict: 'E',
        scope: false,
        templateUrl: function (scope, element) {
            var tempurl = '../../NgApp/costCalculationType/Views/' + $rootScope.language + '/costCalculationType.html';
            $rootScope.$watch(function () { return '../../NgApp/costCalculationType/Views/' + $rootScope.language + '/costCalculationType.html'; }
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