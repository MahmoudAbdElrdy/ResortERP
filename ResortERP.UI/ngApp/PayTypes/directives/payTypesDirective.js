app.directive('payTypes', function ($rootScope, $compile) {
    return {

        restrict: 'E',
        scope: false,
        templateUrl: function (scope, element) {
            var tempurl = '../../NgApp/PayTypes/views/' + $rootScope.language + '/payTypes.html';
            $rootScope.$watch(function () { return '../../NgApp/PayTypes/views/' + $rootScope.language + '/payTypes.html'; }
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