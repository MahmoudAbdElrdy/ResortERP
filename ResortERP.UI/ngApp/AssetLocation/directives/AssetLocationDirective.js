app.directive('assetLocation', function ($rootScope, $compile) {
    return {
        restrict: 'E',
        scope: false,
        templateUrl: function (scope, element) {
            var tempurl = '../../NgApp/AssetLocation/Views/' + $rootScope.language + '/AssetLocation.html';
            $rootScope.$watch(function () { return '../../NgApp/AssetLocation/Views/' + $rootScope.language + '/AssetLocation.html'; }
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
