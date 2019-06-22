app.directive('itemsInventory', function ($rootScope, $compile) {
    return {
        restrict: 'E',
        scope: false,
        templateUrl: function (scope, element) {
            var tempurl = '../../NgApp/ReportPage/ItemsInventory/Views/' + $rootScope.language + '/ItemsInventory.html';
            $rootScope.$watch(function () { return '../../NgApp/ReportPage/ItemsInventory/Views/' + $rootScope.language + '/ItemsInventory.html'; }
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
