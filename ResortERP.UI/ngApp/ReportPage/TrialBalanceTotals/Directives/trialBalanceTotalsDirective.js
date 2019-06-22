app.directive('trialBalanceTotals', function ($rootScope, $compile) {
    return {
        restrict: 'E',
        scope: false,
        templateUrl: function (scope, element) {
            var tempurl = '../../NgApp/ReportPage/TrialBalanceTotals/Views/' + $rootScope.language + '/trialBalanceTotalsReportPage.html';
            $rootScope.$watch(function () { return '../../NgApp/ReportPage/TrialBalanceTotals/Views/' + $rootScope.language + '/trialBalanceTotalsReportPage.html'; }
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