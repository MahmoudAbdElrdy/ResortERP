'use strict';
app.controller('homeController', ['$scope', '$location', 'authService', 'localStorageService', function ($scope, $location, authService, localStorageService) {


    $scope.checkAuth = function () {
        if (!authService.authentication.isAuth) {
            $location.path('/login');
        }

        $("#tempV").remove();
        //if (localStorageService.get('tempBrName') === null || localStorageService.get('tempBrName') === "" || localStorageService.get('tempBrName') === undefined) { }

        if (localStorageService.get('tempBrName')!=null)
            $('.nav-user-wrapper .media .media-body').append("<div id='tempV'>" + localStorageService.get('tempBrName').tempBrName + "</div>");

    }
    $scope.homePageLoad = function () {
        $scope.checkAuth();
    }

}]);