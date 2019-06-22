app.directive('totalBillsMotion', function ($rootScope, $compile) {
    return {
        restrict: 'E',
        scope: false,
        templateUrl: function (scope, element) {
            var tempurl = '../../NgApp/ReportPage/TotalBillsMotion/Views/' + $rootScope.language + '/TotalBillsMotionReport.html';
            $rootScope.$watch(function () { return '../../NgApp/ReportPage/TotalBillsMotion/Views/' + $rootScope.language + '/TotalBillsMotionReport.html'; }
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