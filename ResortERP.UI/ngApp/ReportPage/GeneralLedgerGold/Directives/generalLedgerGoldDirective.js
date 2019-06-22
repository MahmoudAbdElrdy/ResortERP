app.directive('generalLedgerGoldRepo', function ($rootScope, $compile) {
    return {
        restrict: 'E',
        scope: false,
        templateUrl: function (scope, element) {
            var tempurl = '../../NgApp/ReportPage/GeneralLedgerGold/Views/' + $rootScope.language + '/generalLedgerGoldReportPage.html';
            $rootScope.$watch(function () { return '../../NgApp/ReportPage/GeneralLedgerGold/Views/' + $rootScope.language + '/generalLedgerGoldReportPage.html'; }
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