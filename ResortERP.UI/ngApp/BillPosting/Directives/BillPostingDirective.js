app.directive('billPosting', function ($rootScope, $compile) {
    return {
        restrict: 'E',
        scope: false,
        templateUrl: function (scope, element) {
            var tempurl = '../../NgApp/BillPosting/Views/' + $rootScope.language + '/BillPosting.html';
            $rootScope.$watch(function () { return '../../NgApp/BillPosting/Views/' + $rootScope.language + '/BillPosting.html'; }
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