app.directive('companyStoresRepo', function ($rootScope, $compile) {
    return {
        restrict: 'E',
        scope: false,
        templateUrl: function (scope, element) {
            var tempurl = '../../NgApp/ReportPage/CompanyStores/Views/' + $rootScope.language + '/companyStoresReportPage.html';
            $rootScope.$watch(function () { return '../../NgApp/ReportPage/CompanyStores/Views/' + $rootScope.language + '/companyStoresReportPage.html'; }
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