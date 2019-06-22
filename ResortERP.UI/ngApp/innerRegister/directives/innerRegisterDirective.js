app.directive('innerRegisterDir', function ($rootScope, $compile) {
    return {
        restrict: 'E',
        scope: false,
        templateUrl: function (scope, element) {
            var tempurl = '../../NgApp/innerRegister/views/' + $rootScope.language + '/innerRegister.html';
            $rootScope.$watch(function () { return '../../NgApp/innerRegister/Views/' + $rootScope.language + '/innerRegister.html'; }
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