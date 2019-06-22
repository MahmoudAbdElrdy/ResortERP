'use strict';
app.controller('userPriviligeBranchesController', ['$scope', '$location', '$log', '$q', 'userPrivilagesService', 'userPriviligeBranchesService', 'commonService', 'compBranchesService', function ($scope, $location, $log, $q, userPrivilagesService,userPriviligeBranchesService, commonService, compBranchesService) {

    // Region var
    $scope.pageNum = 1;
    $scope.pageSize = 10;
    $scope.companyBranchesList = [];
    $scope.usersList = [];
    $scope.userList = [];
    $scope.cBChecked = false;
    $scope.userPrivBran = {
        COM_BRN_ID: [],
        ID: null,
    };
    $scope.temp = [];
    // End Region

    // Region Method
    $scope.clearEnity = function () {
        $scope.userPrivBran = {
            COM_BRN_ID: [],
            ID: null,
        };
        $scope.companyBranchesList = [];
        $scope.usersList = [];
    };

    $scope.userPrivBranPageLoad = function () {
        $scope.getAllOnLoad();
    }

    $scope.GetAllComapanyBranches = function () {
        compBranchesService.get($scope.pageNum, $scope.pageSize ).then(function (response) {
            $scope.companyBranchesList = response.data
        }, function (error) {
            console.log(error.data.message);
        });
    }

    $scope.getAllUsers = function () {
        userPrivilagesService.getAllUsers().then(function (results) {
            $scope.usersList = results.data;
        })
    }

    $scope.change = function (list) {
        $scope.temp = [];
        if ($scope.userPrivBran.ID == null) {
            swal('', 'لابد من اختيار مستخدم اولا', 'error');
            $.each($scope.companyBranchesList, function (index1, value1) {
                if (value1.COM_BRN_ID == list) {
                    $scope.companyBranchesList[index1].SELECTED = false;
                }
            })
        } else {

            //$.each($scope.userPrivBran.COM_BRN_ID, function (index, value) {
            //    if (value == list) {

            //    } else {
            //        $scope.userPrivBran.COM_BRN_ID[$scope.userPrivBran.COM_BRN_ID.length] = list;
            //    }
            //})
        }
    };

    $scope.getUserBranch = function () {
        $scope.temp = [];
        if ($scope.userPrivBran.ID != null) {
            $scope.GetUserBran($scope.userPrivBran.ID).then(function (results) {
                if (results.data.length != 0) {
                    $scope.temp = [];
                    $.each(results.data, function (index, value) {
                        $.each($scope.companyBranchesList, function (index1, value1) {
                            if (value1.COM_BRN_ID == value.COM_BRN_ID) {
                                $scope.temp[$scope.temp.length++] = index1;
                            }
                        })
                    })

                    $.each($scope.companyBranchesList, function (index2, value2) {
                        $scope.companyBranchesList[index2].SELECTED = false;
                        $.each($scope.temp, function (index3, value3) {
                            if (value3 == index2) {
                                $scope.companyBranchesList[index2].SELECTED = true;
                               // $scope.userPrivBran.COM_BRN_ID[$scope.userPrivBran.COM_BRN_ID.length] = $scope.companyBranchesList[index2].COM_BRN_ID;
                            }
                        })
                    })

                } else {
                    $.each($scope.companyBranchesList, function (index1, value1) {
                        $scope.companyBranchesList[index1].SELECTED = false;
                    })
                }

            })
        }
    };

    $scope.GetUserBran = function (id) {
        return userPriviligeBranchesService.getById(id);
    }

    // Region add
    $scope.savePrivilge = function () {
        if ($scope.userPrivBran.ID == null) {
            swal('', 'لابد من اختيار مستخدم اولا', 'error');
        } else {
            $scope.saveEntity();
        }
    }

    $scope.saveEntity = function () {
        $scope.userPrivBran.COM_BRN_ID = [];
        $.each($scope.companyBranchesList, function (index1, value1) {
            if ($scope.companyBranchesList[index1].SELECTED == true) {
                $scope.userPrivBran.COM_BRN_ID[$scope.userPrivBran.COM_BRN_ID.length] = $scope.companyBranchesList[index1].COM_BRN_ID;
            }           
        })
        $scope.insertGetID($scope.userPrivBran).then(function (results) {
            swal({
                title: 'تم',
                text: 'حفظ بيانات الموظف بنجاح',
                type: 'success',
                timer: 1500,
                showConfirmButton: false
            });
        }, function (error) {
            console.log(error.data.message);
            swal('عفواً',
                'حدث خطأ عند حفظ بيانات الموظف',
                'error');
        });
    }

    $scope.insertGetID = function (entity) {
        return userPriviligeBranchesService.insert(entity);
    }

    // End Region

    $scope.getAllOnLoad = function () {
        $q.all(
            [
                $scope.GetAllComapanyBranches(),
                $scope.getAllUsers()

            ]).then(function (allResponses) {
                $scope.clearEnity();
            }, function (error) {
                var abc = error;
                var def = 99;
            });
    }
    // End Region
}])

