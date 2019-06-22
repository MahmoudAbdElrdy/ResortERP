app.directive('registeration', function ($rootScope, $compile) {
    return {
        restrict: 'E',
        scope: false,
        templateUrl: function (scope, element) {
            var tempurl = '../../NgApp/registeration/Views/' + $rootScope.language + '/registeration.html';
            $rootScope.$watch(function () { return '../../NgApp/registeration/Views/' + $rootScope.language + '/registeration.html'; }
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