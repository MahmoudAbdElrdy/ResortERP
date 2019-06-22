app.directive('assetCompanyDetails', function ($rootScope, $compile) {
    return {
        restrict: 'E',
        scope: false,
        templateUrl: function (scope, element) {
            var tempurl = '../../NgApp/AssetCompanyDetails/views/' + $rootScope.language + '/assetCompanyDetails.html';
            $rootScope.$watch(function () { return '../../NgApp/AssetCompanyDetails/views/' + $rootScope.language + '/assetCompanyDetails.html'; }
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