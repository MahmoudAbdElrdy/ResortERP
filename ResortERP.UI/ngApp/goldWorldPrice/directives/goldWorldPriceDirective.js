app.directive('goldWorldPrice', function ($rootScope, $compile) {
    return {
        restrict: 'E',
        scope: false,
        templateUrl: function (scope, element) {
            var tempurl = '../../NgApp/goldWorldPrice/Views/' + $rootScope.language + '/goldWorldPrice.html';
            $rootScope.$watch(function () { return '../../NgApp/goldWorldPrice/Views/' + $rootScope.language + '/goldWorldPrice.html'; }
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