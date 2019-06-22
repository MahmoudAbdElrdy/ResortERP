app.directive('costCentersGuide', function ($rootScope, $compile) {
    return {
        restrict: 'E',
        scope: false,
        templateUrl: function (scope, element) {
            var tempurl = '../../NgApp/CostCentersGuide/views/' + $rootScope.language + '/costCentersGuide.html';
            $rootScope.$watch(function () { return '../../NgApp/CostCentersGuide/views/' + $rootScope.language + '/costCentersGuide.html'; }
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