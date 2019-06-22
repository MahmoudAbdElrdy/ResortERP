app.directive('trialBalanceGoldRepo', function ($rootScope, $compile) {
    return {
        restrict: 'E',
        scope: false,
        templateUrl: function (scope, element) {
            var tempurl = '../../NgApp/ReportPage/TrialBalanceGold/Views/' + $rootScope.language + '/trialBalanceGoldReportPage.html';
            $rootScope.$watch(function () { return '../../NgApp/ReportPage/TrialBalanceGold/Views/' + $rootScope.language + '/trialBalanceGoldReportPage.html'; }
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