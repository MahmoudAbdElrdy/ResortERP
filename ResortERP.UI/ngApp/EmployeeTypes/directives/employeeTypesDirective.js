app.directive('employeeTypes', function ($rootScope, $compile) {
    return {
        restrict: 'E',
        scope: false,
        templateUrl: function (scope, element) {
            var tempurl = '../../NgApp/EmployeeTypes/Views/' + $rootScope.language + '/employeeTypes.html';
            $rootScope.$watch(function () { return '../../NgApp/EmployeeTypes/Views/' + $rootScope.language + '/employeeTypes.html'; }
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