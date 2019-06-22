app.directive('brokenGoldBoxMotionDaily', function ($rootScope, $compile) {
    return {
        restrict: 'E',
        scope: false,
        templateUrl: function (scope, element) {
            var tempurl = '../../NgApp/ReportPage/BrokenGoldBoxMotionDaily/Views/' + $rootScope.language + '/BrokenGoldBoxMotionDailyReportPage.html';
            $rootScope.$watch(function () { return '../../NgApp/ReportPage/BrokenGoldBoxMotionDaily/Views/' + $rootScope.language + '/BrokenGoldBoxMotionDailyReportPage.html'; }
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