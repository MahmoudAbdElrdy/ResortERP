app.directive('receivingAssetFromEmployee', function ($rootScope, $compile) {
    return {
        restrict: 'E',
        scope: false,
        templateUrl: function (scope, element) {
            var tempurl = '../../NgApp/ReceivingAssetFromEmployee/Views/' + $rootScope.language + '/ReceivingAssetFromEmployee.html';
            $rootScope.$watch(function () { return '../../NgApp/ReceivingAssetFromEmployee/Views/' + $rootScope.language + '/ReceivingAssetFromEmployee.html'; }
                , function (newVal, oldVal) {
                    if (newVal && newVal !== oldVal) {
                        element.$$element.html(newVal);
                        $compile(element.$$element)(scope);
                    }
                });
            return tempurl;
        }
    }
});
