app.directive('accountStatementGoldMonthly', function ($rootScope, $compile) {
    return {
        restrict: 'E',
        scope: false,
        templateUrl: function (scope, element) {
            var tempurl = '../../NgApp/ReportPage/AccountStatementGoldMonthly/Views/' + $rootScope.language + '/accountStatementGoldMonthlyReportPage.html';
            $rootScope.$watch(function () { return '../../NgApp/ReportPage/AccountStatementGoldMonthly/Views/' + $rootScope.language + '/accountStatementGoldMonthlyReportPage.html'; }
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