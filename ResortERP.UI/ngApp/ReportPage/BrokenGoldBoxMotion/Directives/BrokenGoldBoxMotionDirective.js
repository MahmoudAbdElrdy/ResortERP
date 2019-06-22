app.directive('brokenGoldBoxMotion', function ($rootScope, $compile) {
    return {
        restrict: 'E',
        scope: false,
        templateUrl: function (scope, element) {
            var tempurl = '../../NgApp/ReportPage/BrokenGoldBoxMotion/Views/' + $rootScope.language + '/BrokenGoldBoxMotionReportPage.html';
            $rootScope.$watch(function () { return '../../NgApp/ReportPage/BrokenGoldBoxMotion/Views/' + $rootScope.language + '/BrokenGoldBoxMotionReportPage.html'; }
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