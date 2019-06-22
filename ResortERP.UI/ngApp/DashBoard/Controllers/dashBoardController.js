'use strict';
app.controller('dashBoardController', ['$scope', '$rootScope', '$location', '$log', '$q', 'accountsService', 'dateFilter', '$filter', 'commonService', 'currencyService', '$base64', 'entryService', 'accountStatementService', 'compBranchesService', 'dashBoardService', 'localStorageService', function ($scope, $rootScope, $location, $log, $q, accountsService, dateFilter, $filter, commonService, currencyService, $base64, entryService, accountStatementService, compBranchesService, dashBoardService, localStorageService) {


    $scope.accountList = [];

    $scope.cust_Acc_ID = null;

    $scope.var = { ACC_ID: null };

    $scope.UserBranches = [];


    //$scope.dropdownSetting = {
    //    scrollable: true,
    //    scrollableHeight: '300px',
    //    checkBoxes: true,
    //    enableSearch: true,
    //    buttonClasses: 'btn btn-default btn-block',
    //    styleActive: true,
    //    smartButtonTextProvider: function (selectedItems) {
    //        if (selectedItems.length > 5) {
    //            return "تم اختيار " + selectedItems.length + ' حسابات';
    //        }
    //        else if (selectedItems.length == 5) {
    //            return selectedItems[0].label + ', ' + selectedItems[1].label + ', ' + selectedItems[2].label + selectedItems[3].label + selectedItems[4].label;
    //        }
    //        else if (selectedItems.length == 4) {
    //            return selectedItems[0].label + ', ' + selectedItems[1].label + ', ' + selectedItems[2].label + selectedItems[3].label;
    //        }
    //        else if (selectedItems.length == 3) {
    //            return selectedItems[0].label + ', ' + selectedItems[1].label + ', ' + selectedItems[2].label;
    //        }
    //        else if (selectedItems.length == 2) {
    //            return selectedItems[0].label + ', ' + selectedItems[1].label;
    //        }
    //        else if (selectedItems.length == 1) {
    //            return selectedItems[0].label;
    //        }
    //        else {
    //            return 'اختار حساب';
    //        }
    //    }
    //}

    $scope.loadMultiSelect = function () {
        $(document).ready(function () {

            $('#cust_Acc_ID').multiSelect({
                max:3,
                selectableHeader: "<input type='text' class='search-input form-control' autocomplete='off' placeholder=''>",
                selectionHeader: "<input type='text' class='search-input form-control' autocomplete='off' placeholder=''>",
                afterInit: function (ms) {
                    var that = this,
                        $selectableSearch = that.$selectableUl.prev(),
                        $selectionSearch = that.$selectionUl.prev(),
                        selectableSearchString = '#' + that.$container.attr('id') + ' .ms-elem-selectable:not(.ms-selected)',
                        selectionSearchString = '#' + that.$container.attr('id') + ' .ms-elem-selection.ms-selected';

                    that.qs1 = $selectableSearch.quicksearch(selectableSearchString)
                        .on('keydown', function (e) {
                            if (e.which === 40) {
                                that.$selectableUl.focus();
                                return false;
                            }
                        });

                    that.qs2 = $selectionSearch.quicksearch(selectionSearchString)
                        .on('keydown', function (e) {
                            if (e.which == 40) {
                                that.$selectionUl.focus();
                                return false;
                            }
                        });
                },
                afterSelect: function () {
                    this.qs1.cache();
                    this.qs2.cache();
                },
                afterDeselect: function () {
                    this.qs1.cache();
                    this.qs2.cache();
                }
            });

            $("select").change(function () {
                if ($("select option:selected").length > 3) {
                    $(this).removeAttr("selected");             
                    alert('You can select upto 3 options only');
                }
            });

           // $('#cust_Acc_ID').multiSelect({ 'select': ['elem_0', 'elem_1', 'elem_2', 'elem_3', 'elem_4'] });
        });
    }


    $scope.getAccountList = function () {
        accountsService.getAll().then(function (results) {
            $.each(results.data, function (index, value) {
                $('#cust_Acc_ID').multiSelect('addOption', { value: value.ACC_ID, text: value.ACC_AR_NAME, index: index });
            });

            $scope.getDashBoardDa();

        }, function (error) {
            console.log(error.data.message);
        });
    }

    $scope.userId = 0;
    $scope.cust_Acc_ID_selected = [];

    $scope.dashBoardLoad = function () {
        $scope.loadMultiSelect();
        $scope.getAccountList();
     
    }

    $scope.getDashBoardDa = function () {
        if (localStorageService.get('userID') != null) {
            $scope.userId = localStorageService.get('userID').UserID;
        }

        dashBoardService.get($scope.userId).then(function (results) {
            if (results.data != null && results.data != undefined && results.data != "")
            {
                var dataVal = [];
                var x = null;
                if (results.data.accountShow != "" && results.data.accountShow != null) {
                    x = results.data.accountShow.split(",");
                    $.each(x, function (index, value) {
                        if (!isNaN(value) && value != null && value != "") {
                            $("#" + value + "-selectable").trigger("click")
                            dataVal.push(parseInt(value));
                        }

                    })
                    $('#cust_Acc_ID').val(dataVal);
                    $('#cust_Acc_ID').multiSelect("refresh");
                }

               
            }
        })
    }

    $scope.save = function () {
        var data = {};
        var temp = "";
     
        if ($('#cust_Acc_ID').val() != "" && $('#cust_Acc_ID').val() != null) {
            $.each($('#cust_Acc_ID').val(), function (index, value) {
                temp += value + ",";
            })
        }
        
      
        data.userId = $scope.userId;
        data.accountShow = temp;

        dashBoardService.save(data).then(function (results) {
            if (results.data != null && results.data != "" && results.data != undefined) {
                swal({
                    title: 'تم',
                    text: 'الحذف بنجاح',
                    type: 'success',
                    timer: 1500,
                    showConfirmButton: false
                });
            }
        })
    }

    $scope.clearEnity = function () {

        //d.getFullYear() + "" + (d.getMonth() + 1) + "" + d.getDate();

        $scope.entryMasterList = [];

        $scope.var.ACC_ID = null;

        $scope.startDate = "";
        $scope.endDate = "";

        $scope.show_print = false;

    }





}]);


