app.directive('salesTaxReport', function ($rootScope, $compile) {
    return {
        restrict: 'E',
        scope: false,
        templateUrl: function (scope, element) {
            var tempurl = '../../NgApp/ReportPage/SalesTaxReport/Views/' + $rootScope.language + '/SalesTaxReport.html';
            $rootScope.$watch(function () { return '../../NgApp/ReportPage/SalesTaxReport/Views/' + $rootScope.language + '/SalesTaxReport.html'; }
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