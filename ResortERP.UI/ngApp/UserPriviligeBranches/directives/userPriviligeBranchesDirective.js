app.directive('userPriviligeBranches', function ($rootScope, $compile) {
    return {
        restrict: 'E',
        scope: false,
        templateUrl: function (scope, element) {
            var tempurl = '../../NgApp/UserPriviligeBranches/views/' + $rootScope.language + '/userPriviligeBranches.html';
            $rootScope.$watch(function () { return '../../NgApp/UserPriviligeBranches/views/' + $rootScope.language + '/userPriviligeBranches.html'; }
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