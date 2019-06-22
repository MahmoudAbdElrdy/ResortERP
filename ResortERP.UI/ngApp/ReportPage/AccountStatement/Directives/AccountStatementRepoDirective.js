app.directive('accountStatementRepo', function ($rootScope, $compile) {
    return {
        restrict: 'E',
        scope: false,
        templateUrl: function (scope, element) {
            var tempurl = '../../NgApp/ReportPage/AccountStatement/Views/' + $rootScope.language + '/accountStatementReportPage.html';
            $rootScope.$watch(function () { return '../../NgApp/ReportPage/AccountStatement/Views/' + $rootScope.language + '/accountStatementReportPage.html'; }
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