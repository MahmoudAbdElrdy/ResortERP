'use strict';
app.factory('commonService', ['$http', 'sharedService', 'resourcesTranslationService', 'exportService', function ($http, sharedService, resourcesTranslationService, exportService) {

    var serviceBase = sharedService.getBaseUrl();
    var CommonFactory = {};

    var _getAllCompanyBranches = function () {
        var myUrl = serviceBase + 'common/getAllCompanyBranches';
        return $http.get(myUrl).then(function (results) {
            return results;
        })
    }

    var _getAllEmployees = function () {
        var myUrl = serviceBase + 'common/getAllEmployees';
        return $http.get(myUrl).then(function (results) {
            return results;
        })
    }

    var _getUserModule = function (Id) {
        var myUrl = serviceBase + 'common/getUserModule';
        return $http({
            url: myUrl,
            method: "GET",
            params: { Id: Id }
        }).then(function (results) {
            return results;
        })
    }
    var _getAllGovernate = function () {
        var myUrl = serviceBase + 'common/getAllGovernate';
        return $http.get(myUrl).then(function (results) {
            return results;
        })
    }

    var _getPrinters = function () {
        var myUrl = serviceBase + 'common/getPrinters';
        return $http.get(myUrl).then(function (results) {
            return results;
        })
    }

    var _getAllNations = function () {
        var myUrl = serviceBase + 'common/getAllNations';
        return $http.get(myUrl).then(function (results) {
            return results;
        })
    }

    var _getAllTowns = function () {
        var myUrl = serviceBase + 'common/getAllTowns';
        return $http.get(myUrl).then(function (results) {
            return results;
        })
    }

    var _getAllVillages = function () {
        var myUrl = serviceBase + 'common/getAllVillages';
        return $http.get(myUrl).then(function (results) {
            return results;
        })
    }

    var _GetGovernatesByNationID = function (natID) {
        debugger;
        var myUrl = serviceBase + 'common/GetGovernatesByNationID';
        return $http({
            url: myUrl,
            method: "GET",
            params: { NationID: natID }
        }).then(function (results) {
            return results;
        })
    }
    var _GetTownsByGovernateID = function (govID) {
        var myUrl = serviceBase + 'common/GetTownsByGovernateID';
        return $http({
            url: myUrl,
            method: "GET",
            params: { GovID: govID }
        }).then(function (results) {
            return results;
        })
    }

    var _GetVillageByTownID = function (townID) {
        var myUrl = serviceBase + 'common/GetVillageByTownID';
        return $http({
            url: myUrl,
            method: "GET",
            params: { TownID: townID }
        }).then(function (results) {
            return results;
        })
    }

    var _GetCompanyBranchNameByID = function (branID) {
        var myUrl = serviceBase + 'common/GetCompanyBranchNameByID';
        return $http({
            url: myUrl,
            method: "GET",
            params: { BranID: branID }
        }).then(function (results) {
            return results;
        })
    }

    var _GetEmployeeNameByID = function (empID) {
        var myUrl = serviceBase + 'common/GetEmployeeNameByID';
        return $http({
            url: myUrl,
            method: "GET",
            params: { EmpID: empID }
        }).then(function (results) {
            return results;
        })
    }

    var _GetDepartmentByCompanyBranchID = function (Com_Bran_ID) {
        var myUrl = serviceBase + 'common/GetDepartmentByCompanyBranchID';
        return $http({
            url: myUrl,
            method: "GET",
            params: { COM_BRAN_ID: Com_Bran_ID }
        }).then(function (results) {
            return results;
        })
    }

    var _getAllCustomersAccounts = function () {
        var myUrl = serviceBase + 'Acounts/GetAllCustomerAccounts';
        return $http.get(myUrl).then(function (results) {
            return results;
        })
    }


    var _getCustomerSupplier = function (Type) {
        var myUrl = serviceBase + 'accountType/getCustomerSupplier?type=' + Type;
        return $http.get(myUrl).then(function (results) {
            return results;
        })
    }

    var _setLocalStorage = function ($scope, id) {

        resourcesTranslationService.setLocalStorage($scope, id);
    }

    var _updateResource = function ($scope, element) {

        resourcesTranslationService.updateResource($scope, element);
    }

    var _getResourceByName = function (ResourceName) {
        var myUrl = serviceBase + 'resources/getByResourceName';
        var data = { "ResourceName": ResourceName };
        return $http({
            url: myUrl,
            method: "GET",
            params: data
        }).then(function (results) {
            return results;
        })
    }


    var _getModuleResources = function () {

        var moduleResources = localStorage.getItem("ModuleResources");

        if (moduleResources) {

        }
    }

    var _showDeleteAlert = function ($scope, entity) {

        swal({
            title: $scope.moduleResourse["DeleteConfirmationMessage"],
            text: $scope.moduleResourse["DeleteRestoreMessage"] ,
            type: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: $scope.moduleResourse["OkButton"],
            cancelButtonText: $scope.moduleResourse["CancelButton"],
            confirmButtonClass: 'btn btn-success btn-lg',
            cancelButtonClass: 'btn btn-danger btn-lg',
            buttonsStyling: false
        }).then(function () {
            $scope.delete(entity).then(function (results) {
                $scope.clearEnity();
                $scope.refreshData();
                swal({
                    title: $scope.moduleResourse["DoneText"],
                    text: $scope.moduleResourse["DeleteDoneMessage"],
                    type: 'success',
                    timer: 1500,
                    showConfirmButton: false
                });
            }, function (error) {
                console.log(error.data.message);
            });
        }, function (dismiss) {
            // dismiss can be 'cancel', 'overlay',
            // 'close', and 'timer'
            if (dismiss === 'cancel') {
                swal({
                    title: $scope.moduleResourse["DoneText"],
                    text: $scope.moduleResourse["DeleteCancelMessage"],
                    type: 'error',
                    timer: 1500,
                    showConfirmButton: false
                });
            }
        })
    }

    var _showUpdateAlert = function ($scope) {

        swal({
            title: $scope.moduleResourse["DoneText"],
            text: $scope.moduleResourse["UpdateDoneMessage"],
            type: 'success',
            timer: 1500,
            showConfirmButton: false
        });
    }

    var _showInsertAlert = function ($scope) {

        swal({
            title: $scope.moduleResourse["DoneText"],
            text: $scope.moduleResourse["AddDoneMessage"],
            type: 'success',
            timer: 1500,
            showConfirmButton: false
        });
    }

    var _showErrorInsertAlert = function ($scope) {

        swal($scope.moduleResourse["SorryText"],
            $scope.moduleResourse["AddErrorMessage"],
            'error');
    }

    var _showErrorUpdateAlert = function ($scope) {

        swal($scope.moduleResourse["SorryText"],
            $scope.moduleResourse["UpdateErrorMessage"],
            'error');
    }

    var _getExcelFile = function (tableId, sheetName, headerName, list) {

        exportService.getExcelFile(tableId, sheetName, headerName, list);
    }

    //calc customer credit
    var _getAccountValByaccID = function (accID) {
        var res = {};
        var myUrl = serviceBase + 'entryDetails/getAccountValByaccID?accID=' + accID;
        return $http.get(myUrl).then(function (results) {
            //return results;
            var _tempTotalMoney = results.data[8] - results.data[9];
            var tempTotalMoney = _tempTotalMoney.toFixed(3);

            var x;
            if (tempTotalMoney < 0) {
                x = "<td>رصيد اجمالى <font color='red'>:" + tempTotalMoney * -1 + "</font></td>";
            } else {
                x = "<td>رصيد اجمالى :" + tempTotalMoney + "</td>";
            }

            var _x24 = results.data[0] - results.data[4];
            var x24 = _x24.toFixed(3);
            var res24;
            if (x24 < 0) {
                res24 = "<td><font color='red'>" + x24 * -1 + "</font></td>";
            } else {
                res24 = "<td>" + x24 + "</td>";
            }

            var _x22 = results.data[1] - results.data[5];
            var x22 = _x22.toFixed(3);
            var res22;
            if (x22 < 0) {
                res22 = "<td><font color='red'>" + x22 * -1 + "</font></td>";
            } else {
                res22 = "<td>" + x22 + "</td>";
            }

            var _x21 = results.data[2] - results.data[6];
            var x21 = _x21.toFixed(3);
            var res21;
            if (x21 < 0) {
                res21 = "<td><font color='red'>" + x21 * -1 + "</font></td>";
            } else {
                res21 = "<td>" + x21 + "</td>";
            }

            var _x18 = results.data[3] - results.data[7];
            var x18 = _x18.toFixed(3);
            var res18;
            if (x18 < 0) {
                res18 = "<td><font color='red'>" + x18 * -1 + "</font></td>";
            } else {
                res18 = "<td>" + x18 + "</td>";
            }
            swal({
                // title: 'تم',
                text: ' حساب العميل من الذهب',
                showConfirmButton: true,
                html: `<h3>` + results.data[10]+`</h3>
                        <div class="panel-group accordion text-small" id="modalAccordion">
	                        <div class="panel panel-white margin-bottom-10">
		                        <div class="panel-heading">
			                        <h5 class="panel-title"><a class="accordion-toggle collapsed text-right" data-toggle="collapse" data-parent="#modalAccordion" data-target="#collapseSAlert"> <i class="icon-arrow"></i>  رصيد الذهب  </a></h5>
		                        </div>
		                        <div id="collapseSAlert" class="collapse">
			                    <div class="panel-body">
                                    <table id="table" class='table table-bordered table-hover'>
                                        <tbody>
                                             <tr> <th align='center' colspan='2'> رصيد دائن</th> </tr>
                                             <tr>
                                                 <td>رصيد دائن 24  </td><td>`+
                Number.parseFloat(results.data[0]).toFixed(3)
                + `</td></tr>
                                             <tr><td>رصيد دائن 22  </td><td> `+
                Number.parseFloat(results.data[1]).toFixed(3)
                + `</td></tr>
                                              <tr><td>رصيد دائن 21  </td><td>`+
                Number.parseFloat(results.data[2]).toFixed(3)
                + ` </td></tr>
                                             <tr><td>رصيد دائن 18   </td><td>`+
                Number.parseFloat(results.data[3]).toFixed(3)
                + `</td></tr>
                                              <tr><th align='center' colspan='2'>مدين</th> </tr>
                                             <tr><td>مدين 24  </td><td>`+
                Number.parseFloat(results.data[4]).toFixed(3)
                + `</td></tr>
                                             <tr><td>مدين 22   </td><td>`+
                Number.parseFloat(results.data[5]).toFixed(3)
                + `</td></tr>
                                              <tr><td>مدين 21  </td><td>`+
                Number.parseFloat(results.data[6]).toFixed(3)
                + ` </td></tr>
                                             <tr><td>مدين 18   </td><td>`+
                Number.parseFloat(results.data[7]).toFixed(3)
                + `</td></tr><tr><td colspan='2'>
                 <table class='table table-bordered'>
                     <tbody>
                        <tr>
                            <td> العيار </td>
                            <td> 24 </td>
                            <td> 22 </td>
                            <td> 21 </td>
                            <td> 18 </td>
                        </tr>
                        <tr>
                        <td> الرصيد </td>
                `+
                res24
                + `
                `+
                res22
                + `
                `+
                res21
                + `
                `+
                res18
                + `
                                     </tr></tbody> </table> </td> </tr> </tbody>
                                     </table>
                                 </div>
		                    </div>
	                    </div>`+
                `          
	                <div class="panel panel-white margin-bottom-10">
		                <div class="panel-heading">
			                <h5 class="panel-title"><a class="accordion-toggle collapsed text-right" data-toggle="collapse" data-parent="#modalAccordion" data-target="#collapseGAlert"> <i class="icon-arrow"></i>  رصيد المال </a></h5>
		                </div>
		                <div id="collapseGAlert" class="collapse">
			                <div class="panel-body">
                                <table id="table" class='table table-bordered'>
                                    <tbody>
                                       <tr><td>رصيد دائن : `+
                Number.parseFloat(results.data[8]).toFixed(3)
                + `</td></tr>
                <tr>
               <td>مدين : `+
                Number.parseFloat(results.data[9]).toFixed(3)
                + `</td></tr>
                <tr>`
                + x + `
                </tr>

                                    </tbody>
                                </table>
			                </div>
		                </div>
	                </div>
                </div>
                            `

            });

            res.money = tempTotalMoney;
            res.c24 = x24;
            res.c22 = x22;
            res.c21 = x21;
            res.c18 = x18;
            res.accName = results.data[10];

            return res;
        });

      //  return res;
    }



    var _getAccountBalanceByID = function (accID) {
        var res = {};
        var myUrl = serviceBase + 'entryDetails/getAccountValByaccID?accID=' + accID;
        return $http.get(myUrl).then(function (results) {
            //return results;
            var _tempTotalMoney = results.data[8] - results.data[9];
            var tempTotalMoney = _tempTotalMoney.toFixed(3);         

            var _x24 = results.data[4] - results.data[0];
            var x24 = _x24.toFixed(3);
            var res24;          

            var _x22 = results.data[5] - results.data[1];
            var x22 = _x22.toFixed(3);
            var res22;         

            var _x21 = results.data[6] - results.data[2];
            var x21 = _x21.toFixed(3);
            var res21;
           
            var _x18 = results.data[7] - results.data[3];
            var x18 = _x18.toFixed(3);
                    
            res.money = tempTotalMoney;
            res.c24 = x24;
            res.c22 = x22;
            res.c21 = x21;
            res.c18 = x18;
            res.accName = results.data[10];

            return res;
        });

        //  return res;
    }



    CommonFactory.getAllCompanyBranches = _getAllCompanyBranches;
    CommonFactory.getPrinters = _getPrinters;
    CommonFactory.getAllEmployees = _getAllEmployees;
    CommonFactory.getAllGovernate = _getAllGovernate;
    CommonFactory.getAllNations = _getAllNations;
    CommonFactory.getAllTowns = _getAllTowns;
    CommonFactory.getAllVillages = _getAllVillages;
    CommonFactory.GetGovernatesByNationID = _GetGovernatesByNationID;
    CommonFactory.GetTownsByGovernateID = _GetTownsByGovernateID;
    CommonFactory.GetVillageByTownID = _GetVillageByTownID;
    CommonFactory.GetCompanyBranchNameByID = _GetCompanyBranchNameByID;
    CommonFactory.GetEmployeeNameByID = _GetEmployeeNameByID;
    CommonFactory.getAllCustomersAccounts = _getAllCustomersAccounts;
    CommonFactory.GetDepartmentByCompanyBranchID = _GetDepartmentByCompanyBranchID;
    CommonFactory.getCustomerSupplier = _getCustomerSupplier;
    CommonFactory.setLocalStorage = _setLocalStorage;
    CommonFactory.updateResource = _updateResource;
    CommonFactory.getResourceByName = _getResourceByName
    CommonFactory.getModuleResources = _getModuleResources
    CommonFactory.showDeleteAlert = _showDeleteAlert;
    CommonFactory.showUpdateAlert = _showUpdateAlert;
    CommonFactory.showInsertAlert = _showInsertAlert;
    CommonFactory.showErrorInsertAlert = _showErrorInsertAlert;
    CommonFactory.showErrorUpdateAlert = _showErrorUpdateAlert;
    CommonFactory.getExcelFile = _getExcelFile;
    CommonFactory.getAccountValByaccID = _getAccountValByaccID;
    CommonFactory.getAccountBalanceByID = _getAccountBalanceByID;
    CommonFactory.getUserModule =_getUserModule;
    return CommonFactory;

}]);