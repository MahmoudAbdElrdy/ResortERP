app.directive('itemCaliber', function ($rootScope, $compile) {
    return {
        restrict: 'E',
        scope: false,
        templateUrl: function (scope, element) {
            var tempurl = '../../NgApp/itemCaliber/Views/' + $rootScope.language + '/itemCaliber.html';
            $rootScope.$watch(function () { return '../../NgApp/itemCaliber/Views/' + $rootScope.language + '/itemCaliber.html'; }
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