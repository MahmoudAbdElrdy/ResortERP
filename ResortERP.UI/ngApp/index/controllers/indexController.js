'use strict';
app.controller('indexController', ['$scope', '$location', 'authService', function ($scope, $location, authService) {

    $scope.logOut = function () {
        authService.logOut();
        $location.path('/login');
    }


    //$scope.getLastGoldWorldPrice = function () {
    //    goldWorldPriceService.GetLastGoldWorldPrice().then(function (response) {
    //        var result = response.data;
    //        $scope.GoldWorldPrice24 = (parseFloat(result)).toFixed(2);

    //        $scope.GoldWorldPrice22 = (parseFloat(result) * (22 / 24)).toFixed(2);
    //        $scope.GoldWorldPrice21 = (parseFloat(result) * (21 / 24)).toFixed(2);
    //        $scope.GoldWorldPrice18 = (parseFloat(result) * (18 / 24)).toFixed(2);

    //    })
    //}


    $scope.authentication = authService.authentication;

}]);