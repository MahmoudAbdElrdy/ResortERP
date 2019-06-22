app.directive('accountStatementDaily', function ($rootScope, $compile) {
    return {
        restrict: 'E',
        scope: false,
        templateUrl: function (scope, element) {
            var tempurl = '../../NgApp/ReportPage/AccountStatementDaily/Views/' + $rootScope.language + '/accountStatementDailyReportPage.html';
            $rootScope.$watch(function () { return '../../NgApp/ReportPage/AccountStatementDaily/Views/' + $rootScope.language + '/accountStatementDailyReportPage.html'; }
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