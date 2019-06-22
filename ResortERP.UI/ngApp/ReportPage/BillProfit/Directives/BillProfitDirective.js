app.directive('billProfit', function ($rootScope, $compile) {
    return {
        restrict: 'E',
        scope: false,
        templateUrl: function (scope, element) {
            var tempurl = '../../NgApp/ReportPage/BillProfit/Views/' + $rootScope.language + '/billProfitReportPage.html';
            $rootScope.$watch(function () { return '../../NgApp/ReportPage/BillProfit/Views/' + $rootScope.language + '/billProfitReportPage.html'; }
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
