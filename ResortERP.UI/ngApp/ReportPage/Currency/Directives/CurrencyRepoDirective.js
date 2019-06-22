app.directive('currencyRepo', function ($rootScope, $compile) {
    return {
        restrict: 'E',
        scope: false,
        templateUrl: function (scope, element) {
            var tempurl = '../../NgApp/ReportPage/Currency/Views/' + $rootScope.language + '/currencyReportPage.html';
            $rootScope.$watch(function () { return '../../NgApp/ReportPage/Currency/Views/' + $rootScope.language + '/currencyReportPage.html'; }
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