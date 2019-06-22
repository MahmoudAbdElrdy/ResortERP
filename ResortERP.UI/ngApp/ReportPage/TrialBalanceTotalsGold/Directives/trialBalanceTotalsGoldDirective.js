app.directive('trialBalanceTotalsGold', function ($rootScope, $compile) {
    return {
        restrict: 'E',
        scope: false,
        templateUrl: function (scope, element) {
            var tempurl = '../../NgApp/ReportPage/TrialBalanceTotalsGold/Views/' + $rootScope.language + '/trialBalanceTotalsGoldReportPage.html';
            $rootScope.$watch(function () { return '../../NgApp/ReportPage/TrialBalanceTotalsGold/Views/' + $rootScope.language + '/trialBalanceTotalsGoldReportPage.html'; }
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