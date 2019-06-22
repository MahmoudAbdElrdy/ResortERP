app.directive('goldBoxMotion', function ($rootScope, $compile) {
    return {
        restrict: 'E',
        scope: false,
        templateUrl: function (scope, element) {
            var tempurl = '../../NgApp/ReportPage/GoldBoxMotion/Views/' + $rootScope.language + '/GoldBoxMotionReportPage.html';
            $rootScope.$watch(function () { return '../../NgApp/ReportPage/GoldBoxMotion/Views/' + $rootScope.language + '/GoldBoxMotionReportPage.html'; }
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