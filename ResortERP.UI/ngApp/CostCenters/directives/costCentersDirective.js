app.directive('costCenters', function ($rootScope, $compile) {
    return {

        restrict: 'E',
        scope: false,
        templateUrl: function (scope, element) {
            var tempurl = '../../NgApp/CostCenters/Views/' + $rootScope.language + '/costCenters.html';
            $rootScope.$watch(function () { return '../../NgApp/CostCenters/Views/' + $rootScope.language + '/costCenters.html'; }
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