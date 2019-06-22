app.directive('assetGroup', function ($rootScope, $compile) {
    return {
        restrict: 'E',
        scope: false,
        templateUrl: function (scope, element) {
            var tempurl = '../../NgApp/AssetsGroup/views/' + $rootScope.language + '/assetsGroup.html';
            $rootScope.$watch(function () { return '../../NgApp/AssetsGroup/views/' + $rootScope.language + '/assetsGroup.html'; }
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