app.directive('itemsGroups', function ($rootScope, $compile) {
    return {
        restrict: 'E',
        scope: false,
        templateUrl: function (scope, element) {
            var tempurl = '../../NgApp/itemsGroups/Views/' + $rootScope.language + '/itemsGroups.html';
            $rootScope.$watch(function () { return '../../NgApp/itemsGroups/Views/' + $rootScope.language + '/itemsGroups.html'; }
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