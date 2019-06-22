app.directive('entryType', function ($rootScope, $compile) {
    return {
        restrict: 'E',
        scope: false,
        templateUrl: function (scope, element) {
            var tempurl = '../../NgApp/entryType/Views/' + $rootScope.language + '/entryType.html';
            $rootScope.$watch(function () { return '../../NgApp/entryType/Views/' + $rootScope.language + '/entryType.html'; }
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