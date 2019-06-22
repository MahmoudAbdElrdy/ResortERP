'use strict';
app.controller('companyStoresReportPageController', ['$scope', '$location', '$log', '$q', 'salesTypeService', 'compBranchesService', 'companyStoresService', 'costCentersService', function ($scope, $location, $log, $q, salesTypeService, compBranchesService, companyStoresService, costCentersService) {


    $scope.companyName = null;

    $scope.storeName = null;

    $scope.costCenter = null;

    $scope.fromDate = null;

    $scope.toDate = null;

    $scope.sort = null;

    $scope.inventoryPrice = null;

    $scope.userBranchList = [];

    $scope.compStoresList = [];

    $scope.costCentersList = [];


    $scope.GetSalesTypes = function () {
        salesTypeService.getAll().then(function (response) {
            $scope.salesTypesList = response.data;
        }, function (error) {
            console.log(error.data.message);
        });
    };

    $scope.getAllOnLoad = function () {
        $q.all(
            [
                $scope.GetSalesTypes(),
                $scope.GetCompBranches(),
                $scope.GetCompStores(),
                $scope.GetcostCenters(),
            ]).then(function (allResponses) {
            }, function (error) {
            });
    }

    $scope.GetCompBranches = function () {
        //getMainCompanyBranches
        compBranchesService.getUserBranches().then(function (response) {
            if (response.data != undefined && response.data.length > 0) {
                $scope.userBranchList = response.data;
                $scope.COM_BRN_ID = [response.data[0].COM_BRN_ID];
            }
        }, function (error) {
            console.log(error.data.message);
        });
    };


    $scope.GetCompStores = function () {
        companyStoresService.getAll().then(function (response) {
            $scope.compStoresList = response.data;
        }, function (error) {
            console.log(error.data.message);
        });
    };



    $scope.GetcostCenters = function () {
        costCentersService.getAll().then(function (response) {
            $scope.costCentersList = response.data;
        }, function (error) {
            console.log(error.data.message);
        });
    };

}]);