app.directive('companyStores', function ($rootScope, $compile) {
    return {
        restrict: 'E',
        scope: false,
        templateUrl: function (scope, element) {
            var tempurl = '../../NgApp/companystores/Views/' + $rootScope.language + '/companystores.html';
            $rootScope.$watch(function () { return '../../NgApp/companystores/Views/' + $rootScope.language + '/companystores.html'; }
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