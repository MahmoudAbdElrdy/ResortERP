app.directive('accountsGroup', function ($rootScope, $compile) {
    return {
        restrict: 'E',
        scope: false,
        templateUrl: function (scope, element) {
            var tempurl = '../../NgApp/accountsGroup/Views/' + $rootScope.language + '/accountsGroup.html';
            $rootScope.$watch(function () { return '../../NgApp/accountsGroup/Views/' + $rootScope.language + '/accountsGroup.html'; }
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