'use strict';
app.controller('MenuController', ['$scope', '$location', 'MenuService', 'authService', '$rootScope', function ($scope, $location, MenuService, authService, $rootScope) {


    $rootScope.$on("CallGetCompany", function () {
        $scope.GetCompany();
    });

    $scope.GetCompany = function () {
        MenuService.GetCompany().then(function (response) {
            $scope.CompanyARName = response.data.COMPANY_AR_NAME;
            $scope.CompanyENName = response.data.COMPANY_EN_NAME;
            $scope.CompanyARAddress = response.data.COMPANY_AR_ADRESS;
            $scope.CompanyENAddress = response.data.COMPANY_EN_ADRESS;
            $scope.getUserType();
        },
         function (err) {
             $scope.message = err.error_description;
             console.log(err.message);
         });
    };

    //Get All items of Menu With all childs
    $scope.getMenuItems = function () {
        var langID = authService.GetLanguageID();
        MenuService.getAllUserMenu(langID).then(function (response) {
            $scope.menuItems = response.data;
        });
    }

// Function of Search menu
    $("#myInput").keyup(function (event) {
        if (event.keyCode!=8 && $("#myInput").val() != "" && $("#myInput").val() != null && $("#myInput").val() != undefined)
            $(".sub-menu").show();
        else {
            $(".sub-menu").slideUp();
            //$(".sub-menu").h();
        }
        //alert();
        // $('#search').keyup(function () {
        var matches = $('#myUL').find('li:contains(' + $("#myInput").val() + ') ');
        $('li', '#myUL').not(matches).slideUp();
        matches.slideDown();
    })



    $scope.authentication = authService.authentication;

    $scope.getUserType = function () {
        if ($scope.authentication.isAuth != false) {
            MenuService.getUserType().then(function (response) {
                var type = response.data;
                if (type == 'Cachier') {
                    $scope.hideMenuWhenCachier = true;
                } else {
                    $scope.hideMenuWhenCachier = false;
                }
            });
        }

    }

}]);