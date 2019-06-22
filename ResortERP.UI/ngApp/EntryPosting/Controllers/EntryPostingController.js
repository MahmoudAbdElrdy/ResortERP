'use strict';
app.controller('EntryPostingController', ['$scope', '$rootScope', '$location', '$log', '$q', 'accountsService', 'dateFilter', '$filter', 'commonService', 'entryService', '$base64', 'entrySettingService', 'costCentersService', 'ReportsService', 'compBranchesService', function ($scope, $rootScope, $location, $log, $q, accountsService, dateFilter, $filter, commonService, entryService, $base64, entrySettingService, costCentersService, ReportsService, compBranchesService) {

    $scope.accountList = [];   

    $scope.EntryPostingPageLoad = function () {
        $scope.GetAllEmployeesList();
        $scope.getCostCentersList();
        $scope.getAllboxAccountList();
        //$scope.getPayTypeList();
        $scope.getEntryTypes();
        $scope.EntryPosting = {};
        $scope.startDate = new Date();
        $scope.endDate = new Date();
    }

    $scope.getAllboxAccountList = function () {
        $scope.storesList = [];
        entryService.getAllBoxAccounts().then(function (response) {

            $scope.boxAccountList = response.data;
        })
    }

    $scope.GetAllEmployeesList = function () {


        $scope.employeeList = [];
        entryService.getAllEmployeeOfTypeSales().then(function (response) {

            $scope.employeeList = response.data;
        })
    }

    $scope.getCostCentersList = function () {
        costCentersService.getMainCostCenters().then(function (results) {
            $scope.mainCostCenterList = results.data;
            //console.log(results.data);
        }, function (error) {
            console.log(error.data.message);
        });
    }

    //$scope.getPayTypeList=function() {
    //    entryService.getAllTPayTypes().then(function (results) {
    //        $scope.payTypeList = results.data;
    //    }, function (error) {
    //        console.log(error.data.message);
    //    });
    //}

    $scope.getEntryTypes = function() {
        entrySettingService.getAll().then(function(results) {
            $scope.entryTypesList = results.data;
        },
        function(error) {
            console.log(error.data.message);
        });
    }

    $scope.GetSearchResult = function () {        
        if ($scope.startDate && $scope.endDate) {
            $scope.EntryPosting.StartDate = new Date(Date.UTC($scope.startDate.getFullYear(),
                $scope.startDate.getMonth(),
                $scope.startDate.getDate(),
                $scope.startDate.getHours(),
                $scope.startDate.getMinutes()));
            $scope.EntryPosting.EndDate = new Date(Date.UTC($scope.endDate.getFullYear(),
                $scope.endDate.getMonth(),
                $scope.endDate.getDate(),
                $scope.endDate.getHours(),
                $scope.endDate.getMinutes()));
            entryService.getNotPostedEntries($scope.EntryPosting).then(function (results) {
                $scope.searchResultBills = results.data;
                $scope.disableSave = false;
            });
        }
        else swal("عفوا", "يجب اختيار تاريخ البداية و تاريخ النهاية ", "error");
    }

    $scope.clearEnity = function () {
        $scope.EntryPosting = {};
        $scope.startDate = new Date();
        $scope.endDate = new Date();
        $scope.searchResultBills = [];
        $scope.disableSave = true;
    }

    $scope.SelectAllBills = function() {
        angular.forEach($scope.searchResultBills,
            function(item, index) {
                item.Selected = $scope.SelectAll;
            });
    }

    $scope.PostEntries=function() {
        var entryIds = [];
        angular.forEach($scope.searchResultBills,function(item, index) {
            if (item.Selected === true) {
                entryIds.push(item.ENTRY_ID);
            }
        })
        if (entryIds.length > 0) {
            entryService.SetEntriesPosted(entryIds).then(function (result) {
                if (result) {
                    swal("", "تم حفظ البيانات بنجاح ", "success");
                    $scope.clearEnity();
                }
            });
        } else {
            swal("عفوا", "يجب اختيار السندات المراد ترحيلها اولا ", "error");
        }
    }

}]);


