app.directive('technicalSupport', function ($rootScope, $compile) {
    return {
        restrict: 'E',
        scope: false,
        templateUrl: function (scope, element) {
            var tempurl = '../../NgApp/technicalSupport/Views/' + $rootScope.language + '/technicalSupport.html';
            $rootScope.$watch(function () { return '../../NgApp/technicalSupport/Views/' + $rootScope.language + '/technicalSupport.html'; }
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