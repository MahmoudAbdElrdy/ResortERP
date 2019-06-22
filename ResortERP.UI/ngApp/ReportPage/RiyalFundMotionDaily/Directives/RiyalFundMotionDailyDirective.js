app.directive('riyalFundMotionDaily', function ($rootScope, $compile) {
    return {
        restrict: 'E',
        scope: false,
        templateUrl: function (scope, element) {
            var tempurl = '../../NgApp/ReportPage/RiyalFundMotionDaily/Views/' + $rootScope.language + '/RiyalFundMotionDailyReportPage.html';
            $rootScope.$watch(function () { return '../../NgApp/ReportPage/RiyalFundMotionDaily/Views/' + $rootScope.language + '/RiyalFundMotionDailyReportPage.html'; }
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