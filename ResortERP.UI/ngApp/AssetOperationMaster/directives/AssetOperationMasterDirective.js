app.directive('assetOperationMaster', function ($rootScope, $compile) {
    return {
        restrict: 'E',
        scope: false,
        templateUrl: function (scope, element) {
            var tempurl = '../../NgApp/AssetOperationMaster/Views/' + $rootScope.language + '/AssetOperationMaster.html';
            $rootScope.$watch(function () { return '../../NgApp/AssetOperationMaster/Views/' + $rootScope.language + '/AssetOperationMaster.html'; }
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
