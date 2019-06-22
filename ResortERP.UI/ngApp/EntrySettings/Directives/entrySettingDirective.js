app.directive('entrySetting', function ($rootScope, $compile) {
    return {
        restrict: 'E',
        scope: false,
        templateUrl: function (scope, element) {
            var tempurl = '../../NgApp/EntrySettings/Views/' + $rootScope.language + '/entrySetting.html';
            $rootScope.$watch(function () { return '../../NgApp/EntrySettings/Views/' + $rootScope.language + '/entrySetting.html'; }
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