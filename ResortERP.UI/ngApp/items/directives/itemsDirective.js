app.directive('itemsDirective', function ($rootScope, $compile) {
    return {
        restrict: 'E',
        scope: false,
        templateUrl: function (scope, element) {
            var tempurl = '../../NgApp/items/Views/' + $rootScope.language + '/items.html';
            $rootScope.$watch(function () { return '../../NgApp/items/Views/' + $rootScope.language + '/items.html'; }
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