'use strict';
app.controller('TestBankReportPageController', ['$scope', '$location', '$log', '$q', 'bankService', function ($scope, $location, $log, $q, bankService) {

    $scope.bank = {};
    $scope.bankCount = 0;
    $scope.pageNum = 1;
    $scope.pageSize = 10;
    $scope.pagesCount = 0;
    $scope.maxSize = 5;
    $scope.count = 0;

    $scope.getPagesCount = function () {
        var div = Math.floor($scope.bankCount / $scope.pageSize);
        var rem = $scope.bankCount % $scope.pageSize;
        if (rem > 0) {
            div = div + 1;
        }
        return div;
    }
    $scope.getBankTotalCount = function () {
        debugger;
        $scope.count().then(function (result) {
            var data = result.data;
            $scope.bankTotalCount = data;
            $scope.pagesCount = $scope.getPagesCount();

        }, function (error) {
            consol.log(error.data.message);
        });
    }


    $scope.getPagesCount = function () {
        debugger;
        var div = Math.floor($scope.bankTotalCount / $scope.pageSize);
        var rem = $scope.bankTotalCount % $scope.pageSize;
        if (rem > 0) {
            div = div + 1;
        }
        $scope.pagesCount = div;
        return div;
    }

   

    $scope.clearEnity = function () {
        $scope.bank = {
            ID: null,
            Code: null,
            ArName: "",
            LatName: "",
            CurrencyID: null,
            Notes: "",
            AccountNo: "",
            AddedBy: "",
            AddedOn: null,
            UpdatedBy: "",
            UpdatedOn: null,
            Disable: false,
            NationId: null,
            GovId: null,
            TownId: null,
            VillageId: null,
            CurrencyName: "",
            AddressNotes: ""
        };
        $scope.bankList = {};
        $scope.bankCount = 0;
    }
    $scope.count = function () {
        return bankService.count();
    }
    $scope.GetSearchResult = function () {
        debugger;
        bankService.getAll().then(function (result) {
            $scope.bankList = result.data;
            }, function (error) { });
    }

}]);