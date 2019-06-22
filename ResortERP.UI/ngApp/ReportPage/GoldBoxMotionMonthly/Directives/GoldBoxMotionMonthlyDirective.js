app.directive('goldBoxMotionMonthly', function ($rootScope, $compile) {
    return {
        restrict: 'E',
        scope: false,
        templateUrl: function (scope, element) {
            var tempurl = '../../NgApp/ReportPage/GoldBoxMotionMonthly/Views/' + $rootScope.language + '/GoldBoxMotionMonthlyReportPage.html';
            $rootScope.$watch(function () { return '../../NgApp/ReportPage/GoldBoxMotionMonthly/Views/' + $rootScope.language + '/GoldBoxMotionMonthlyReportPage.html'; }
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