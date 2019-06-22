app.directive('entryPosting', function ($rootScope, $compile) {
    return {
        restrict: 'E',
        scope: false,
        templateUrl: function (scope, element) {
            var tempurl = '../../NgApp/EntryPosting/Views/' + $rootScope.language + '/EntryPosting.html';
            $rootScope.$watch(function () { return '../../NgApp/EntryPosting/Views/' + $rootScope.language + '/EntryPosting.html'; }
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