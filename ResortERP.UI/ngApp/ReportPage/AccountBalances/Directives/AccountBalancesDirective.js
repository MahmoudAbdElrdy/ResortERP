app.directive('accountBalances', function ($rootScope, $compile) {
    return {
        restrict: 'E',
        scope: false,
        templateUrl: function (scope, element) {
            var tempurl = '../../NgApp/ReportPage/AccountBalances/Views/' + $rootScope.language + '/AccountBalancesReportPage.html';
            $rootScope.$watch(function () { return '../../NgApp/ReportPage/AccountBalances/Views/' + $rootScope.language + '/AccountBalancesReportPage.html'; }
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