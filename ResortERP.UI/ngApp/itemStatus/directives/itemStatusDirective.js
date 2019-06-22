app.directive('itemStatus', function ($rootScope, $compile) {
    return {
        restrict: 'E',
        scope: false,
        templateUrl: function (scope, element) {
            var tempurl = '../../NgApp/itemStatus/Views/' + $rootScope.language + '/itemStatus.html';
            $rootScope.$watch(function () { return '../../NgApp/itemStatus/Views/' + $rootScope.language + '/itemStatus.html'; }
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