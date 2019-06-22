'use strict';
app.controller('logFileReportController', ['$scope', '$rootScope', '$location', '$log', '$q', 'dateFilter', '$filter', 'commonService', 'currencyService', '$base64', 'entryService', 'logFileReportService', '$window', function ($scope, $rootScope, $location, $log, $q, dateFilter, $filter, commonService, currencyService, $base64, entryService, logFileReportService, $window) {


    $scope.formList = [];
    $scope.userList = [];
    $scope.userSel = { UID: null };
    $scope.selFormData = [];
    $scope.branchList = [];
    $scope.branchSel = { COM_BRN_ID: null };
    $scope.startDate = null;
    $scope.endDate = null;

    $scope.show_print = false;
  
    $scope.var;
    $scope.varData = [];
    $scope.flagEx = false;

    $scope.pageNum = 1;
    $scope.pageSize = 10;

    $scope.pagesCount = 0;
    $scope.maxSize = 5;
    $scope.logTotalCount = 0;

    $scope.getPagesCount = function () {
        debugger;
        var div = Math.floor($scope.logTotalCount / $scope.pageSize);
        var rem = $scope.logTotalCount % $scope.pageSize;
        if (rem > 0) {
            div = div + 1;
        }
        $scope.pagesCount = div;
        return div;
    }

    $scope.getLogTotalCount = function (varData1, startDate, endDate, UID, COM_BRN_ID) {
        debugger;
        $scope.varData = [];
        $.each($(".select2-selection__choice"), function (index, value) {
            $scope.varData.push(value.title);
            $scope.flagEx = true;
        })
        $scope.count($scope.varData, startDate, endDate, UID, COM_BRN_ID).then(function (result) {
            var data = result.data;
            $scope.logTotalCount = data;
            $scope.pagesCount = $scope.getPagesCount();

        }
            , function (error) {
                //consol.log(error.data.message);
            });
    }


    $scope.redToView = function (id, url) {
        if (url != null) {
            //  alert(id + "gg" + url);
            if (!url.includes("foo")) {
                if (url.includes("?")) {
                    $window.location.href = "#" + url + "&foo=" + id;
                } else {
                    $window.location.href = "#" + url + "?foo=" + id;
                }
            }              
            else {
                $window.location.href = "#" + url;
            }
        } else {    
           // $window.location.href = "#/" + url+"?foo=" + id;
        }
    }

    $scope.getFormsList = function () {
        logFileReportService.get().then(function (results) {
            $scope.formList = results.data;
            $(".select2Custom").select2({
                placeholder: "اختر القائمة",
                allowClear: true
            });
        });
    }

    $scope.getAllUsers = function () {
        logFileReportService.getAllUsers().then(function (results) {
            $scope.userList = results.data;
            $(".select2Custom").select2({
                placeholder: "اختر المستخدم",
                allowClear: true
            });
        });
    }


    $scope.getAllCompBranches = function () {
        logFileReportService.getAllCompBranches().then(function (results) {
            $scope.branchList = results.data;
            $(".select2Custom").select2({
                placeholder: "اختر فرع الشركة",
                allowClear: true
            });

        });
    }

    $scope.LogFileReportLoad = function () {
        $scope.getFormsList();
        $scope.getAllUsers();
        $scope.getAllCompBranches();
        $scope.startDate = new Date(Date.now());
        //$scope.getLogTotalCount();
        // $scope.endDate = new Date();
    }

    $scope.pageChanged = function () {
        $scope.GetSearchResult();
        $log.log('Page changed to: ' + $scope.pageNum);
    };

    $scope.count = function (varData, startDate, endDate, UID, COM_BRN_ID) {
        return logFileReportService.count(varData, startDate, endDate, UID, COM_BRN_ID);
    }

    $scope.GetSearchResult = function () {
      //  $(".select2-selection__choice")[0].innerText
        $scope.varData = [];
        $.each($(".select2-selection__choice"), function (index, value) {
            $scope.varData.push(value.title);
            $scope.flagEx = true;
        })
       // if ($scope.flagEx) {

            $scope.flagEx = false;
            //var Forms = "<DS><Forms><ARName>" + $scope.var.ARName + "</ARName></Forms></DS>";
            var startDate = null;
            var endDate = null;
            if ($scope.startDate != null) {
                var d1 = new Date($scope.startDate);
                startDate = d1.getFullYear() + "/" + ("0" + (d1.getMonth() + 1)).slice(-2) + "/" + ("0" + d1.getDate()).slice(-2);
            }

            if ($scope.endDate != null) {
                var d2 = new Date($scope.endDate);
                endDate = d2.getFullYear() + "/" + ("0" + (d2.getMonth() + 1)).slice(-2) + "/" + ("0" + (d2.getDate() + 1)).slice(-2);
            }
            logFileReportService.getByRangDate($scope.varData, startDate, endDate, $scope.userSel.UID, $scope.branchSel.COM_BRN_ID , $scope.pageNum, $scope.pageSize).then(function (results) {

                //console.log(results.data);

                $scope.selFormData = results.data;
                $scope.getLogTotalCount($scope.varData, startDate, endDate, $scope.userSel.UID, $scope.branchSel.COM_BRN_ID);
                $scope.pagesCount = $scope.getPagesCount();
                $scope.show_print = true;
            });

    //    }

     //   else {
          //swal("عفوا", "لابد من البحث بالقائمة ", "error");
     //   }

    }



    $scope.clearEnity = function () {

        d.getFullYear() + "" + (d.getMonth() + 1) + "" + d.getDate();

        $scope.selFormData = [];

        $scope.var.ARName = null;

        $scope.startDate = "";
        $scope.endDate = "";

        $scope.show_print = false;

    }
    
 
    

}]);


