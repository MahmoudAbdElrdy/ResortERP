'use strict';
app.controller('userPrivilagesController', ['$scope', '$location', '$log', '$q', 'authService', 'userPrivilagesService', 'commonService', 'MenuService', function ($scope, $location, $log, $q, authService, userPrivilagesService, commonService, MenuService) {

    $scope.selectedUser = null;

    $scope.userPrivilage = {
        ARName: null
        , LatName: null
        , MenuID: null
        , FormID: null
        , UserOperationID: null
        , UserID: null
        , Notes: null
        , MenuOpen: true
        , OpAdd: true
        , OpUpdate: true
        , OpDelete: true
        , OpPreview: true
        , OpPrint: true
        , OpSearch: true
    }
    $scope.userPrivilagesList = [];
    $scope.userMenuList = [];
    $scope.userMenu = {};
    $scope.userMenuTotalCount = 0;
    $scope.pageNumUserMenu = 1;
    $scope.pageSizeUserMenu = 10;
    $scope.pagesCountUserMenu = 0;
    $scope.maxSizeUserMenu = 10;



    $scope.userPrivilagesTotalCount = 0;
    $scope.pageNum = 1;
    $scope.pageSize = 10;
    $scope.pagesCount = 0;

    $scope.getPagesCount = function () {

        var div = Math.floor($scope.userPrivilagesTotalCount / $scope.pageSize);
        var rem = $scope.userPrivilagesTotalCount % $scope.pageSize;
        if (rem > 0) {
            div = div + 1;
        }
        $scope.pagesCount = div;
        return div;
    }
    $scope.maxSize = 5;

    $scope.clearEnity = function () {
        $scope.refreshData();
    }
    $scope.refreshData = function () {
        $scope.getAllOnLoad();
        $scope.selectedUser = null;
        $scope.userPrivilagesList = [];
    }
    $scope.saveuserPrivilage = function () {
        $scope.saveEntity();
    }

    $scope.dirEnity = function (entity) {
        $scope.userPrivilage = entity;
    }

    $scope.getAllOnLoad = function () {
        $q.all(
            [
                GetSteps(),
                $scope.getAllUsers(),
                $scope.getMasterUserMenus(),
                $scope.getAllNationList(),
                $scope.getAllUserMenu(),
                $scope.getUserMenuTotalCount(),
                $scope.getUserMenuLastCode()

            ])
            .then(function (allResponses) {

                var urlParams = $location.search();
                if (urlParams.foo != null && urlParams.foo != undefined && urlParams.foo != "") {
                    userPrivilagesService.getByMenuId(urlParams.foo).then(function (result) {
                        $scope.userPrivilagesList = result.data;
                        $scope.selectedUser = parseInt(result.data[0].UserID);
                        //alert($scope.selectedUser);
                    })

                }


            }, function (error) {
                alert();
                var abc = error;
                var def = 99;
            });
    }
    $scope.getuserPrivilagesTotalCount = function () {
        $scope.count().then(function (results) {

            var data = results.data;
            $scope.userPrivilagesTotalCount = data;
            $scope.pagesCount = $scope.getPagesCount();

        }, function (error) {

            console.log(error.data.message);
        });
    }
    $scope.getuserPrivilagesList = function () {
        $scope.get($scope.pageNum, $scope.pageSize).then(function (results) {
            var data = results.data;
            $scope.userPrivilagesList = data;

        }, function (error) {

            console.log(error.data.message);
        });
    }

    $scope.getAllUsers = function () {
        userPrivilagesService.getAllUsers().then(function (results) {
            $scope.usersList = results.data;



            //var urlParams = $location.search();
            //if (urlParams.foo != null && urlParams.foo != undefined && urlParams.foo != "") {

           // }

        })
    }

    $scope.getMasterUserMenus = function () {
        userPrivilagesService.getMasterUserMenus(0).then(function (results) {
            $scope.masterMenus = results.data;
        })
    }
    $scope.getChildUserMenus = function (MenuID) {
        userPrivilagesService.getMasterUserMenus(MenuID).then(function (results) {
            $scope.childMenus = results.data;
        })
    }

    $scope.addChildTouserPrivilagesList = function (childMenu) {
        if ($scope.selectedUser == null || $scope.selectedUser == undefined) {
            swal('', 'لابد من اختيار مستخدم اولا', 'error');
            return;
        }
        var foundItem = null;
        for (var i = 0; i < $scope.userPrivilagesList.length; i++) {
            if ($scope.userPrivilagesList[i].MenuID === childMenu.ID) {
                foundItem = $scope.userPrivilagesList[i];
            }
        }

        if (foundItem != null) {
            swal('', 'هذة الصفحة مضافة لهذا المستخدم من قبل', 'error');
            return;
        }
        $scope.userPrivilage = {};
        $scope.userPrivilage.ARName = childMenu.ARName;
        $scope.userPrivilage.LatName = childMenu.LatName;
        $scope.userPrivilage.MenuID = childMenu.ID;
        $scope.userPrivilage.UserID = $scope.selectedUser;
        $scope.userPrivilage.MenuOpen = true;
        $scope.userPrivilage.OpAdd = true;
        $scope.userPrivilage.OpUpdate = true;
        $scope.userPrivilage.OpDelete = true;
        $scope.userPrivilage.OpPreview = true;
        $scope.userPrivilage.OpPrint = true;
        $scope.userPrivilage.OpSearch = true;
        $scope.userPrivilagesList.push($scope.userPrivilage);
    }


    //تغيير ال checkboxes
    $scope.updateuserPrivilagesListOnChange = function (cellName, data, index) {
        if (cellName == 'MenuOpen') {
            $scope.userPrivilagesList[index].MenuOpen = data;
        } else if (cellName == 'OpAdd') {
            $scope.userPrivilagesList[index].OpAdd = data;
        } else if (cellName == 'OpUpdate') {
            $scope.userPrivilagesList[index].OpUpdate = data;
        } else if (cellName == 'OpDelete') {
            $scope.userPrivilagesList[index].OpDelete = data;
        } else if (cellName == 'OpPreview') {
            $scope.userPrivilagesList[index].OpPreview = data;
        }
    }

    $scope.saveUserPrivliages = function () {
        userPrivilagesService.saveUserPrivliages($scope.userPrivilagesList).then(function (result) {
            if (result.data == true) {
                swal('تم', 'تم الحفظ بنجاح', 'success');
                return;
            } else {
                swal('خطأ', 'لم يتم الحفظ', 'error');
                return;
            }
        })

    }




    $scope.removeUserPrivilageItem = function (index) {
        $scope.userPrivilagesList.splice(index, 1);
    };

    $scope.getUserPrivilagesForCurrentUser = function (data) {
        $scope.selectedUser = data;
        userPrivilagesService.getAllUserMenusForUser($scope.selectedUser).then(function (response) {
            $scope.userPrivilagesList = response.data;
        })
    };

    $scope.get = function (pageNum, pageSize) {
        return userPrivilagesService.get(pageNum, pageSize);
    }
    $scope.count = function () {
        return userPrivilagesService.count();
    }
    $scope.add = function (entity) {
        return userPrivilagesService.add(entity);
    }
    $scope.update = function (entity) {
        return userPrivilagesService.update(entity);
    }
    $scope.delete = function (entity) {
        return userPrivilagesService.delete(entity);
    }
    $scope.userPrivilagesPageLoad = function () {

        $scope.getAllOnLoad();

    }

    $scope.pageChanged = function () {
        $scope.getAllOnLoad();
        $log.log('Page changed to: ' + $scope.pageNum);
    };
    /////////////////////////////////////////////////////////////////////////tabs
    var GetSteps = function () {
        var lang = authService.GetLanguageID();
        if (lang != null && lang != undefined && lang != "")
            if (lang == 1) {
                $scope.steps = [
                    'صلاحيات المستخدمين',
                    'قوائم',

                ];
            } else if (lang == 2) {
                $scope.steps = [
                    'User Privilage',
                    'Menues',

                ];
            } else {
                $scope.steps = [
                    'صلاحيات المستخدمين',
                    'قوائم',

                ];
            }
        $scope.selection = $scope.steps[0];
    };



    $scope.getCurrentStepIndex = function () {
        // Get the index of the current step given selection
        return _.indexOf($scope.steps, $scope.selection);
    };

    // Go to a defined step index
    $scope.goToStep = function (index) {
        if (!_.isUndefined($scope.steps[index])) {
            $scope.selection = $scope.steps[index];
        }
    };

    $scope.hasNextStep = function () {
        var stepIndex = $scope.getCurrentStepIndex();
        var nextStep = stepIndex + 1;
        // Return true if there is a next step, false if not
        return !_.isUndefined($scope.steps[nextStep]);
    };

    $scope.hasPreviousStep = function () {
        var stepIndex = $scope.getCurrentStepIndex();
        var previousStep = stepIndex - 1;
        // Return true if there is a next step, false if not
        return !_.isUndefined($scope.steps[previousStep]);
    };

    $scope.incrementStep = function () {
        if ($scope.hasNextStep()) {
            var stepIndex = $scope.getCurrentStepIndex();
            var nextStep = stepIndex + 1;
            $scope.selection = $scope.steps[nextStep];
        }
    };

    $scope.decrementStep = function () {
        if ($scope.hasPreviousStep()) {
            var stepIndex = $scope.getCurrentStepIndex();
            var previousStep = stepIndex - 1;
            $scope.selection = $scope.steps[previousStep];
        }
    };

    ///////////////////////////////////////////////////user menu//////////////////////////////////////////////////////////
    $scope.saveUserMenu = function () {

        $scope.userMenu.ARName = $scope.userMenu.ARName;
        $scope.userMenu.LatName = $scope.userMenu.LatName;
        $scope.userMenu.CountryID = $scope.userMenu.CountryID;
        $scope.userMenu.DisplayOrNot = 1;
        $scope.userMenu.UserShortcut = 0;
        $scope.userMenu.Active = $scope.userMenu.Active;
        $scope.userMenu.AddedOn = new Date();
        $scope.userMenu.Position = $scope.userMenu.Position;
        var addedBy = authService.GetUserName();
        $scope.userMenu.AddedBy = addedBy;

        if ($scope.userMenu.ID == undefined || $scope.userMenu.ID == 0) {
            MenuService.add($scope.userMenu).then(function (result) {
                $scope.refreshUserMenu();
                $scope.clearUserMenu();
                return result.data;

            });
        }

        else {
            MenuService.update($scope.userMenu).then(function (result) {
                $scope.refreshUserMenu();
                $scope.clearUserMenu();
                return result.data;

            });
        }
    }


    $scope.getAllNationList = function () {
        commonService.getAllNations().then(function (result) {
            $scope.nationList = result.data;
        });
    }


    $scope.getAllUserMenu = function () {
        MenuService.getAllMenus($scope.pageNumUserMenu, $scope.pageSizeUserMenu).then(function (result) {

            $scope.userMenuList = result.data;
        });
    }

    $scope.dirUserMenuEnity = function (entity) {
        $scope.userMenu = entity;
    }

    $scope.clearUserMenu = function () {
        $scope.userMenu = {};
    }

    $scope.refreshUserMenu = function () {
        $scope.getAllUserMenu();
    }

    $scope.deleteUserMenu = function (entity) {
        return MenuService.delete(entity);
    }

    $scope.deleteUserMenuEnity = function (entity) {

        swal({
            title: 'هل تريد حذف القائمه؟',
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
            $scope.deleteUserMenu(entity).then(function (results) {

                if (results.data == true) {

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
                else {
                    swal({
                        title: 'خطأ',
                        text: 'القائمه مستخدمه في السندات او الفواتير',
                        type: 'error',
                        timer: 1500,
                        showConfirmButton: false
                    });
                }
                //}, function (error) {
                //    console.log(error.data.message);
            });


        }


            , function (dismiss) {
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

    $scope.getPagesCountUserMenu = function () {

        var div = Math.floor($scope.userMenuTotalCount / $scope.pageSizeUserMenu);
        var rem = $scope.userMenuTotalCount % $scope.pageSizeUserMenu;
        if (rem > 0) {
            div = div + 1;
        }
        $scope.pagesCountUserMenu = div;
        return div;
    }


    $scope.pageChangedUserMenu = function () {
        $scope.getAllOnLoad();
        $log.log('Page changed to: ' + $scope.pageNum);
    };



    $scope.getUserMenuTotalCount = function () {
        debugger;
        $scope.countUserMenu().then(function (result) {
            var data = result.data;
            $scope.userMenuTotalCount = data;
            $scope.pagesCountUserMenu = $scope.getPagesCountUserMenu();

        }
            , function (error) {
                consol.log(error.data.message);
            });
    }


    $scope.countUserMenu = function () {
        return MenuService.countUserMenu();
    }


    $scope.getUserMenuLastCode = function () {
        var urlParams = $location.search();
        if (urlParams.foo != null && urlParams.foo != undefined && urlParams.foo != "") {
        }
        else {
            MenuService.getLastCode().then(function (result) {
                $scope.userMenu.Code = parseInt(result.data) + 1;
            }, function (error) { });
        }
    }

}]);