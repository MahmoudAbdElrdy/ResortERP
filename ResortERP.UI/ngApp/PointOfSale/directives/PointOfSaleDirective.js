app.directive('pointOfSale', function ($rootScope, $compile) {
    return {
        restrict: 'E',
        scope: false,
        templateUrl: function (scope, element) {
            var tempurl = '../../NgApp/PointOfSale/Views/' + $rootScope.language + '/PointOfSale.html';
            $rootScope.$watch(function () { return '../../NgApp/PointOfSale/Views/' + $rootScope.language + '/PointOfSale.html'; }
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