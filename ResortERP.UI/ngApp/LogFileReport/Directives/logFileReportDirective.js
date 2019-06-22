app.directive('logFileReport', function ($rootScope, $compile) {
    return {
        restrict: 'E',
        scope: false,
        templateUrl: function (scope, element) {
            var tempurl = '../../NgApp/LogFileReport/views/' + $rootScope.language + '/logFileReport.html';
            $rootScope.$watch(function () { return '../../NgApp/LogFileReport/views/' + $rootScope.language + '/logFileReport.html'; }
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