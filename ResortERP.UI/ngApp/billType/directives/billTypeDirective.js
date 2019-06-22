app.directive('billType', function ($rootScope, $compile) {
    return {
        restrict: 'E',
        scope: false,
        templateUrl: function (scope, element) {
            var tempurl = '../../NgApp/billType/Views/' + $rootScope.language + '/billType.html';
            $rootScope.$watch(function () { return '../../NgApp/billType/Views/' + $rootScope.language + '/billType.html'; }
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