app.directive('manufacturingWages', function ($rootScope, $compile) {
    return {
        restrict: 'E',
        scope: false,
        templateUrl: function (scope, element) {
            var tempurl = '../../NgApp/manufacturingWages/Views/' + $rootScope.language + '/manufacturingWages.html';
            $rootScope.$watch(function () { return '../../NgApp/manufacturingWages/Views/' + $rootScope.language + '/manufacturingWages.html'; }
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