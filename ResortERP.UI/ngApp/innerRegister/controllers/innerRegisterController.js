'use strict';
app.controller('innerRegisterController', ['$scope', '$location', '$log', '$q', 'innerRegisterService', 'userGroupService', 'MenuService', function ($scope, $location, $log, $q, innerRegisterService, userGroupService, MenuService) {

    $scope.showAddNewUser = function () {
        if ($scope.MaxUsers === -1 || $scope.MaxUsers <= $scope.usersTotalCount) {
            $scope.IsShow = false;
        } else { $scope.IsShow = true; }
    };

    $scope.clearEnity = function () {
        $scope.user = { ID: null, ConfirmPassword: "", COMPANY_AR_NAME: "", COMPANY_EN_NAME: "", UID: "", COMPANY_EMAIL: "", UPWD: null, COMPANY_AR_ADRESS: "", COMPANY_EN_ADRESS: "", COMPANY_TELEPHONE: "", COMPANY_FAX: "", USER_TYPE: "", USER_LETTER:""};
        $scope.showAddNewUser();
    }
    $scope.clearEnity();
    $scope.usersList = [];
    $scope.usersTotalCount = 0;
    $scope.pageNum = 1;
    $scope.pageSize = 10;
    $scope.pagesCount = 0;
    $scope.maxSize = 5;
    $scope.MaxUsers = -1;




    $scope.getPagesCount = function () {
        var div = Math.floor($scope.usersTotalCount / $scope.pageSize);
        var rem = $scope.usersTotalCount % $scope.pageSize;
        if (rem > 0) {
            div = div + 1;
        }
        return div;
    }

    $scope.refreshData = function () {
        $scope.getAllOnLoad();
    }

    $scope.saveUser = function () {

        $scope.saveEntity();
    }
    $scope.emailPattern = /^([a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]){1,70}$/;
    $scope.saveEntity = function () {

        if ($scope.user !== undefined && $scope.user !== null && $scope.user.UID !== "" && $scope.user.UPWD !== "" && $scope.user.USER_TYPE !== "" && $scope.user.COMPANY_AR_NAME !== "" && $scope.user.COMPANY_EN_NAME !== "" && $scope.user.COMPANY_EMAIL !== "") {
            if ($scope.user.ID === null || $scope.ID === 0) {
                $scope.add($scope.user).then(function (results) {
                    $scope.clearEnity();
                    $scope.getAllOnLoad();
                    swal({
                        title: 'تم',
                        text: 'حفظ بيانات المستخدم بنجاح',
                        type: 'success',
                        timer: 1500,
                        showConfirmButton: false
                    });
                }, function (error) {
                    console.log(error.data.message);
                    swal('عفواً',
                        'حدث خطأ عند حفظ بيانات المستخدم',
                        'error');
                });
            } else {
                $scope.update($scope.user).then(function (results) {
                    $scope.clearEnity();
                    $scope.refreshData();
                    swal({
                        title: 'تم',
                        text: 'تعديل بيانات المستخدم بنجاح',
                        type: 'success',
                        timer: 1500,
                        showConfirmButton: false
                    });
                }, function (error) {
                    console.log(error.data.message);
                    swal('عفواً',
                        'حدث خطأ عند تعديل بيانات المستخدم',
                        'error');
                });
            }
            $scope.showAddNewUser();
        }
    }

    $scope.dirEnity = function (entity) {
        $scope.IsShow = true;
        $scope.user = entity;
    }
    $scope.deleteEnity = function (entity) {
        swal({
            title: 'هل تريد حذف المستخدم؟',
            text: "لن تستطيع عكس عملية الحذف مرة أخري",
            type: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'نعم',
            cancelButtonText: 'الغاء',
            confirmButtonClass: 'btn btn-success btn-lg',
            cancelButtonClass: 'btn btn-danger btn-lg',
            buttonsStyling: false
        }).then(function () {
            $scope.delete(entity).then(function (results) {
               // alert(results.data);
                if (results.data == false) {
                    swal({
                        title: "تحذير",
                        text: "هذا المستخدم له صلاحيات على افرع الشركة",
                        type: "warning",
                        showCancelButton: true,
                        confirmButtonColor: '#3085d6',
                        cancelButtonColor: '#d33',
                        confirmButtonText: 'حذف',
                        cancelButtonText: 'الغاء الحذف',
                        confirmButtonClass: 'btn btn-danger btn-lg',
                        cancelButtonClass: 'btn btn-success btn-lg',
                        buttonsStyling: false

                    }).then((willDelete) => {

                        if (willDelete) {

                            innerRegisterService.deleteBran(entity).then(function (result) {
                                $scope.clearEnity();
                                $scope.refreshData();
                                swal({
                                    title: 'تم',
                                    text: ' الحذف',
                                    type: 'success',
                                    timer: 1500,
                                    showConfirmButton: false
                                });
                            });

                        }
                        //else {
                        //    alert(willDelete);
                        //    swal("تم الغاء الحذف ");
                        //}

                        },
                        function (dismiss) {
                            // dismiss can be 'cancel', 'overlay',
                            // 'close', and 'timer'
                            if (dismiss === 'cancel') {
                                swal({
                                    title: 'تم',
                                    text: 'الغاء عملية الحذف',
                                    type: 'error',
                                    timer: 1500,
                                    showConfirmButton: false
                                });
                            }
                        }

                    );
                } else {
                    $scope.clearEnity();
                    $scope.refreshData();
                    swal({
                        title: 'تم',
                        text: 'الحذف بنجاح',
                        type: 'success',
                        timer: 1500,
                        showConfirmButton: false
                    });
                }

            }, function (error) {
                console.log(error.data.message);
            });
        }, function (dismiss) {
            // dismiss can be 'cancel', 'overlay',
            // 'close', and 'timer'
            if (dismiss === 'cancel') {
                swal({
                    title: 'تم',
                    text: 'الغاء عملية الحذف',
                    type: 'error',
                    timer: 1500,
                    showConfirmButton: false
                });
            }
        })
    }

    $scope.checkEmail = function () {
        innerRegisterService.checkEmail($scope.user.COMPANY_EMAIL).then(function (response) {

            $scope.IsMailExist = response.data;
            //if (response.data == true) {
            //    document.getElementsByName("email")[0].validity().valid = false;
            //} else {
            //    document.getElementsByName("email")[0].validity().valid = true;
            //}
        }, function (error) {
            console.log(error.message);
        });
    }
    $scope.checkUserName = function () {

        innerRegisterService.checkUserName($scope.user.UID).then(function (response) {
            $scope.IsUserNameExist = response.data;
            //if (response.data == true) {
            //    document.getElementsByName("email")[0].validity().valid = false;
            //} else {
            //    document.getElementsByName("email")[0].validity().valid = true;
            //}
        }, function (error) {
            console.log(error.message);
        });
    }
    //var uniqueEmail = function () {
    //    document.getElementsByName("email")[0].addEventListener("blur", function () {
    //        alert();
    //    })
    //}

    $scope.getAllOnLoad = function () {
        $q.all([
                 $scope.getUsersList(),
                 $scope.getUsersTotalCount(),
                 $scope.getUserTypes(),
                 $scope.GetCompany(),
                 $scope.showAddNewUser()
        ]).then(function (allResponses) {

            $scope.showAddNewUser();
        }, function (error) {
            //This will be called if $q.all finds any of the requests erroring.
            var abc = error;
            var def = 99;
        });
    }


    $scope.getUsersTotalCount = function () {
        $scope.count().then(function (results) {
            var data = results.data;
            $scope.usersTotalCount = data;
            $scope.pagesCount = $scope.getPagesCount();
        }, function (error) {

            console.log(error.data.message);
        });
    }
    $scope.getUsersList = function () {

        $scope.get($scope.pageNum, $scope.pageSize).then(function (results) {
            var data = results.data;
            $scope.usersList = data;
        }, function (error) {

            console.log(error.data.message);
        });
    }

    $scope.getUserTypes = function (pageNum, pageSize) {
        userGroupService.getAll().then(function (result) {
            $scope.userGroupList = result.data;
        }, function (error) {
            console.log(error.data.message);
        });
    }

    $scope.GetCompany = function () {
        MenuService.GetCompany().then(function (response) {
            $scope.CompanyENName = response.data.COMPANY_EN_NAME;
            $scope.MaxUsers = response.data.MaxUsersNumber;
            $scope.showAddNewUser();
        },
         function (err) {
             $scope.message = err.error_description;
             console.log(err.message);
         });
    };

    $scope.get = function (pageNum, pageSize) {
        return innerRegisterService.get(pageNum, pageSize);
    }
    $scope.count = function () {
        return innerRegisterService.count();
    }

    $scope.add = function (entity) {
        return innerRegisterService.add(entity);
    }
    $scope.update = function (entity) {
        return innerRegisterService.update(entity);
    }
    $scope.delete = function (entity) {
        return innerRegisterService.delete(entity);
    }
    $scope.innerRegisterPageLoad = function () {
        $scope.getAllOnLoad();
    }

    $scope.pageChanged = function () {
        $scope.getAllOnLoad();
        $log.log('Page changed to: ' + $scope.pageNum);
    };


}]);