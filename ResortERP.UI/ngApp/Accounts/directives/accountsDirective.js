app.directive('accounts', function ($rootScope, $compile) {
    return {
        restrict: 'E',
        scope: false,
        templateUrl: function (scope, element) {
            var tempurl = '../../NgApp/Accounts/Views/' + $rootScope.language + '/accounts.html';
            $rootScope.$watch(function () { return '../../NgApp/Accounts/Views/' + $rootScope.language + '/accounts.html'; }
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




//app.directive('accounts', []).directive('autoComplete', function ($timeout) {
//    return function (scope, element, iAttrs) {
//        element.autocomplete({
//            source: scope[iAttrs.uiItems],
//            select: function () {
//                $timeout(function () {
//                    element.trigger('input');
//                }, 0);
//            }
//        });
//    };
//});

//app.directive('pressEnter', function () {
//    return function (scope, element, attrs) {
//        element.bind("keydown keypress", function (event) {
//            if (event.which === 13) {
//                scope.$apply(function () {
//                    scope.$eval(attrs.pressEnter);
//                });

//                event.preventDefault();
//            }
//        });
//    };




app.directive('autoComplete', function ($timeout) {
    return function (scope, element, attrs) {
        element.autocomplete({
            source: scope[attrs.uiItems],
            select: function () {
                $timeout(function () {
                    element.trigger('input');
                }, 0);
            }
        });
    };
})