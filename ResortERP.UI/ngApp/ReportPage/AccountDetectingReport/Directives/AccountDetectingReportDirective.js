app.directive('accountDetectingReport', function ($rootScope, $compile) {
    return {
        restrict: 'E',
        scope: false,
        templateUrl: function (scope, element) {
            var tempurl = '../../NgApp/ReportPage/AccountDetectingReport/Views/' + $rootScope.language + '/AccountDetectingReportPage.html';
            $rootScope.$watch(function () { return '../../NgApp/ReportPage/AccountDetectingReport/Views/' + $rootScope.language + '/AccountDetectingReportPage.html'; }
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