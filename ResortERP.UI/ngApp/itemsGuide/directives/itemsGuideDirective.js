app.directive('itemsGuide', function ($rootScope, $compile) {
    return {
        restrict: 'E',
        scope: false,
        templateUrl: function (scope, element) {
            var tempurl = '../../NgApp/itemsGuide/views/' + $rootScope.language + '/itemsGuide.html';
            $rootScope.$watch(function () { return '../../NgApp/itemsGuide/views/' + $rootScope.language + '/itemsGuide.html'; }
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