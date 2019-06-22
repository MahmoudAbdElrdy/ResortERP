app.directive('brokenGoldBoxMotionMonthly', function ($rootScope, $compile) {
    return {
        restrict: 'E',
        scope: false,
        templateUrl: function (scope, element) {
            var tempurl = '../../NgApp/ReportPage/BrokenGoldBoxMotionMonthly/Views/' + $rootScope.language + '/BrokenGoldBoxMotionMonthlyReportPage.html';
            $rootScope.$watch(function () { return '../../NgApp/ReportPage/BrokenGoldBoxMotionMonthly/Views/' + $rootScope.language + '/BrokenGoldBoxMotionMonthlyReportPage.html'; }
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