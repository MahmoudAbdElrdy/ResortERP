app.directive('customers', function ($rootScope, $compile) {
    return {
        restrict: 'E',
        scope: false,
        templateUrl: function (scope, element) {
            var tempurl = '../../NgApp/Customers/Views/' + $rootScope.language + '/customers.html';
            $rootScope.$watch(function () { return '../../NgApp/Customers/Views/' + $rootScope.language + '/customers.html'; }
                , function (newVal, oldVal) {
                    if (newVal && newVal !== oldVal) {
                        element.$$element.html(newVal);
                        $compile(element.$$element)(scope);
                    }
                });
            return tempurl;
        }
    }
});