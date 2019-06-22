app.directive('chartOfAcc', function ($rootScope, $compile) {
    return {
        restrict: 'E',
        scope: false,
        templateUrl: function (scope, element) {
            var tempurl = '../../NgApp/chartOfAcc/views/' + $rootScope.language + '/chartOfAcc.html';
            $rootScope.$watch(function () { return '../../NgApp/chartOfAcc/views/' + $rootScope.language + '/chartOfAcc.html'; }
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