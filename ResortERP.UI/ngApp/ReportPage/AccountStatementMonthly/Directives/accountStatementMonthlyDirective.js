app.directive('accountStatementMonthly', function ($rootScope, $compile) {
    return {
        restrict: 'E',
        scope: false,
        templateUrl: function (scope, element) {
            var tempurl = '../../NgApp/ReportPage/AccountStatementMonthly/Views/' + $rootScope.language + '/accountStatementMonthlyReportPage.html';
            $rootScope.$watch(function () { return '../../NgApp/ReportPage/AccountStatementMonthly/Views/' + $rootScope.language + '/accountStatementMonthlyReportPage.html'; }
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