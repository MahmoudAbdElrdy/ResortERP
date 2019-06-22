'use strict';
app.controller('accountsGroupController', ['$scope', '$location', '$log', '$q', 'authService', 'accountsGroupService', 'accountsService', 'localStorageService', function ($scope, $location, $log, $q, authService, accountsGroupService,  accountsService, localStorageService) {

    $scope.accountsGroup = { GROUP_ID: null, GROUP_CODE: "", GROUP_AR_NAME: "", GROUP_EN_NAME: "", GROUP_MASTER_ID: null, GROUP_REMARKS: ""};
    $scope.accountsGroupList = [];
    $scope.accountsGroupTotalCount = 0;
    $scope.pageNum = 1;
    $scope.pageSize = 10;
    $scope.pagesCount = 0;
    $scope.masterName =null;
    $scope.getPagesCount = function () {
        var div = Math.floor($scope.accountsGroupTotalCount / $scope.pageSize);
        var rem = $scope.accountsGroupTotalCount % $scope.pageSize;
        if (rem > 0) {
            div = div + 1;
        }
        $scope.pagesCount = div;
        return div;
    }
    $scope.maxSize = 5;



    $scope.clearEnity = function () {
        $scope.getlastCode();
        $scope.accountsGroup = { GROUP_ID: null, GROUP_CODE: "", GROUP_AR_NAME: "", GROUP_EN_NAME: "", GROUP_MASTER_ID: null, GROUP_REMARKS: ""};
    }
    $scope.refreshData = function () {
        $scope.getAllOnLoad();
    }
    $scope.saveaccountsGroup = function () {


        $scope.saveEntity();
    }
    $scope.saveEntity = function () {
         debugger;
        if ($scope.accountsGroup !== undefined && $scope.accountsGroup !== null && $scope.accountsGroup.GROUP_CODE !== "" && $scope.accountsGroup.GROUP_AR_NAME !== "" && $scope.accountsGroup.GROUP_EN_NAME !== "") {
            if ($scope.accountsGroup.GROUP_ID === null) {
                $scope.add($scope.accountsGroup).then(function (results) {
                    // var data = results.data;
                    // $scope.accountsGroupList = data;
                    $scope.clearEnity();
                    $scope.refreshData();
                    swal({
                        title: 'تم',
                        text: 'حفظ بيانات مجموعة الحسابات بنجاح',
                        type: 'success',
                        timer: 1500,
                        showConfirmButton: false
                    });
                }, function (error) {
                    console.log(error.data.message);
                    swal('عفواً',
                        'حدث خطأ عند حفظ بيانات مجموعة الحسابات',
                        'error');
                });
            } else {
          
                $scope.update($scope.accountsGroup).then(function (results) {
                    // var data = results.data;
                    // $scope.accountsGroupList = data;
                    $scope.clearEnity();
                    $scope.refreshData();
                    swal({
                        title: 'تم',
                        text: 'تعديل بيانات مجموعة الحسابات بنجاح',
                        type: 'success',
                        timer: 1500,
                        showConfirmButton: false
                    });
                }, function (error) {
                    console.log(error.data.message);
                    swal('عفواً',
                        'حدث خطأ عند تعديل بيانات مجموعة الحسابات',
                        'error');
                });
            }
        }
    }
    $scope.dirEnity = function (entity) {

        $scope.accountsGroup = entity;
       

    }
    $scope.deleteEnity = function (entity) {
        swal({
            title: 'هل تريد حذف مجموعة الحسابات؟',
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
                        text: "هذا الحساب تم عليه عمليات مختلفة. لا تستطيع حذفه",
                        type: "warning",
                        showConfirmButton: true

                    })
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

    $scope.getAllMainItemGroupsList = function () {
        accountsGroupService.getAllMainItemGroups().then(function (response) {
            $scope.mainaccountsGroupsList = response.data;
        })
    }

    $scope.getAllOnLoad = function () {

        $q.all(
            [
                $scope.getaccountsGroupTotalCount(), 
                $scope.getAllMainItemGroupsList(),
                $scope.getaccountsGroupList(),
                $scope.getlastCode(),
              
            ])
            .then(function (allResponses) {
                var urlParams = $location.search();
                if (urlParams.foo != null && urlParams.foo != undefined && urlParams.foo != "") {

                    accountsGroupService.getByID(urlParams.foo).then(function (result) {
                        $scope.accountsGroup = result.data;
                        $scope.dirEnity(result.data);
                    

                    });
                }
                //if all the requests succeeded, this will be called, and $q.all will get an
                //array of all their responses.
                //  console.log(allResponses[0].data);

            }, function (error) {
                //This will be called if $q.all finds any of the requests erroring.
                var abc = error;
                var def = 99;
            });
    }
    $scope.getaccountsGroupTotalCount = function () {
        $scope.count().then(function (results) {
            var data = results.data;
            $scope.accountsGroupTotalCount = data;
            $scope.pagesCount = $scope.getPagesCount();

        }, function (error) {

            console.log(error.data.message);
        });
    }
    $scope.getaccountsGroupList = function () {
        $scope.get($scope.pageNum, $scope.pageSize).then(function (results) {
            var data = results.data;
            $scope.accountsGroupList = data;
        }, function (error) {

            console.log(error.data.message);
        });
    }

    $scope.getID = function(id){
        for(var i=0;i<$scope.accountsGroupList.length;i++){
          if(id == $scope.accountsGroupList[i].GROUP_CODE)
            return $scope.accountsGroupList[i].GROUP_AR_NAME;
          }
        }
    

  $scope.get = function (pageNum, pageSize) {
        return accountsGroupService.get(pageNum, pageSize);
    }
    $scope.count = function () {
        return accountsGroupService.count();
    }
    $scope.add = function (entity) {
        return accountsGroupService.add(entity);
    }
    $scope.update = function (entity) {
        return accountsGroupService.update(entity);
    }
    $scope.delete = function (entity) {
        return accountsGroupService.delete(entity);
    }
    $scope.accountsGroupPageLoad = function () {

        $scope.getAllOnLoad();

    }

    $scope.pageChanged = function () {
        $scope.getAllOnLoad();
        $log.log('Page changed to: ' + $scope.pageNum);
    };
    ///////////////////////get last code

    $scope.getlastCode = function () {
        var urlParams = $location.search();
        if (urlParams.foo != null && urlParams.foo != undefined && urlParams.foo != "") {
        } else {
            accountsGroupService.getLastCode().then(function (result) {
                $scope.accountsGroup.GROUP_CODE = parseInt(result.data) + 1;
            }, function (error) { });
        }

    }

}]);