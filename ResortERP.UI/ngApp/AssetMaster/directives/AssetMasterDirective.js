app.directive('assetMaster', function ($rootScope, $compile) {
    return {
        restrict: 'E',
        scope: false,
        templateUrl: function (scope, element) {
            var tempurl = '../../NgApp/AssetMaster/Views/' + $rootScope.language + '/AssetMaster.html';
            $rootScope.$watch(function () { return '../../NgApp/AssetMaster/Views/' + $rootScope.language + '/AssetMaster.html'; }
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
