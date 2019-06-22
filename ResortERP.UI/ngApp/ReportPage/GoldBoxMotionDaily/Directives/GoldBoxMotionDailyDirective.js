app.directive('goldBoxMotionDaily', function ($rootScope, $compile) {
    return {
        restrict: 'E',
        scope: false,
        templateUrl: function (scope, element) {
            var tempurl = '../../NgApp/ReportPage/GoldBoxMotionDaily/Views/' + $rootScope.language + '/GoldBoxMotionDailyReportPage.html';
            $rootScope.$watch(function () { return '../../NgApp/ReportPage/GoldBoxMotionDaily/Views/' + $rootScope.language + '/GoldBoxMotionDailyReportPage.html'; }
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