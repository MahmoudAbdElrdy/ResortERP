app.directive('accountStatementGoldDaily', function ($rootScope, $compile) {
    return {
        restrict: 'E',
        scope: false,
        templateUrl: function (scope, element) {
            var tempurl = '../../NgApp/ReportPage/AccountStatementGoldDaily/Views/' + $rootScope.language + '/accountStatementGoldDailyReportPage.html';
            $rootScope.$watch(function () { return '../../NgApp/ReportPage/AccountStatementGoldDaily/Views/' + $rootScope.language + '/accountStatementGoldDailyReportPage.html'; }
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