app.directive('assignAssetToEmployee', function ($rootScope, $compile) {
    return {
        restrict: 'E',
        scope: false,
        templateUrl: function (scope, element) {
            var tempurl = '../../NgApp/AssignAssetToEmployee/Views/' + $rootScope.language + '/AssignAssetToEmployee.html';
            $rootScope.$watch(function () { return '../../NgApp/AssignAssetToEmployee/Views/' + $rootScope.language + '/AssignAssetToEmployee.html'; }
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
