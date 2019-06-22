app.directive('riyalFundMotion', function ($rootScope, $compile) {
    return {
        restrict: 'E',
        scope: false,
        templateUrl: function (scope, element) {
            var tempurl = '../../NgApp/ReportPage/RiyalFundMotion/Views/' + $rootScope.language + '/RiyalFundMotionReportPage.html';
            $rootScope.$watch(function () { return '../../NgApp/ReportPage/RiyalFundMotion/Views/' + $rootScope.language + '/RiyalFundMotionReportPage.html'; }
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