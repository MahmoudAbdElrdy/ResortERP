app.directive('riyalFundMotionMonthly', function ($rootScope, $compile) {
    return {
        restrict: 'E',
        scope: false,
        templateUrl: function (scope, element) {
            var tempurl = '../../NgApp/ReportPage/RiyalFundMotionMonthly/Views/' + $rootScope.language + '/RiyalFundMotionMonthlyReportPage.html';
            $rootScope.$watch(function () { return '../../NgApp/ReportPage/RiyalFundMotionMonthly/Views/' + $rootScope.language + '/RiyalFundMotionMonthlyReportPage.html'; }
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