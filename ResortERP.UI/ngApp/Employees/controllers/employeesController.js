'use strict';
app.controller('employeesController', ['$scope', '$location', '$log', '$q', 'employeesService', 'commonService', 'departmentsService', 'employeeTypesService', 'telephoneService', function ($scope, $location, $log, $q, employeesService, commonService, departmentsService, employeeTypesService, telephoneService) {
    $scope.employees = {
        HrPslEmployeeID: null, EmployeeCode: null, EmployeeFirstName: "", EmployeeFirstNameEn: "", EmployeeLastName: "",
        EmployeeLastNameEn: "", DateOfBirth: null, GenderID: null, Address: "", HrPslCityID: null, HrPslCountyID: null, HrPslNationalityID: null,
        HrPslReligionID: null, HrPslMaritalstatusID: null, NationalID: null, NationalIdExpData: null, PassportNo: null, PassportNoExpDate: null,
        Image: "", HrPslJobTitleID: null, DateOfJoin: null, HrPslContractTypeID: null, HrPslDeptartmentID: null, HrPslBranchID: null, IsManager: null,
        DirectManagerID: null, WorkMobileNum: null, HrPslPaymentID: null, PersonalMobileNum: null, HomeNum: null, Email: "", HrPslEmployeeStatusID: null,
        HrPslBenefitsID: null, HrPslEmployeeJobLevelID: null, DrivingLicenseNumber: "", DrivingLicenseIssueDate: null, DrivingLicenseExpireDate: null,
        DrivingLicenseIssuer: "", DrivingLicenseType: null, ResidenceType: null, ResidencyNumber: null, ResidenceExpireDate: null, BloodGroup: "",
        HasChronicDisease: null, ChronicDiseaseDescreption: "", HasInjuries: null, InjuriesDescreption: "", HrPslManagementId: null, HasCar: null,
        GlBankID: null, BankAccountNumber: "",
        AcademicDegrees: [],
        Experiences: [],
        FamilyDetails: [],
        TrainingCourses: [],
        Contacts: []
    };

    $scope.EmployeeAcademicDegree = {};
    $scope.EmployeeExperience = {};
    $scope.EmployeeFamilyDetail = {};
    $scope.EmployeeTrainingCourses = {};
    $scope.EmployeeContact = {};

    $scope.RelationshipTypeList = [
        {
            OptionValue: 1,
            OptionText:'زوج / زوجة'
        },
        {
            OptionValue: 2,
            OptionText: 'ابن'
        },
        {
            OptionValue: 3,
            OptionText: 'ابنة'
        },
        {
            OptionValue: 4,
            OptionText: 'اب'
        },
        {
            OptionValue: 5,
            OptionText: 'ام'
        },
        {
            OptionValue: 6,
            OptionText: 'اخ'
        },
        {
            OptionValue: 7,
            OptionText: 'اخت'
        },
        {
            OptionValue: 8,
            OptionText: 'عم / خال'
        }
    ]

    $scope.clearEnity = function () {
        $scope.employees = {
            HrPslEmployeeID: null, EmployeeCode: null, EmployeeFirstName: "", EmployeeFirstNameEn: "", EmployeeLastName: "",
            EmployeeLastNameEn: "", DateOfBirth: null, GenderID: null, Address: "", HrPslCityID: null, HrPslCountyID: null, HrPslNationalityID: null,
            HrPslReligionID: null, HrPslMaritalstatusID: null, NationalID: null, NationalIdExpData: null, PassportNo: null, PassportNoExpDate: null,
            Image: "", HrPslJobTitleID: null, DateOfJoin: null, HrPslContractTypeID: null, HrPslDeptartmentID: null, HrPslBranchID: null, IsManager: null,
            DirectManagerID: null, WorkMobileNum: null, HrPslPaymentID: null, PersonalMobileNum: null, HomeNum: null, Email: "", HrPslEmployeeStatusID: null,
            HrPslBenefitsID: null, HrPslEmployeeJobLevelID: null, DrivingLicenseNumber: "", DrivingLicenseIssueDate: null, DrivingLicenseExpireDate: null,
            DrivingLicenseIssuer: "", DrivingLicenseType: null, ResidenceType: null, ResidencyNumber: null, ResidenceExpireDate: null, BloodGroup: "",
            HasChronicDisease: null, ChronicDiseaseDescreption: "", HasInjuries: null, InjuriesDescreption: "", HrPslManagementId: null, HasCar: null,
            GlBankID: null, BankAccountNumber: "",
            AcademicDegrees: [],
            Experiences: [],
            FamilyDetails: [],
            TrainingCourses: [],
            Contacts: []
        };
        $scope.TelephoneList = [];
        $scope.telephoneItem = {
            TELE_ID: null, TELE_NUMBER: null, TELE_TYPE_ID: 3, TELE_CAT_ID: null
        };
        $scope.imageSrc = null;
        //  $scope.addGridItem();
        $scope.getlastCode();
    }

    $("#tabs").tabs({
        active: 0,
        //collapsible: true,
        //active: false,
        //   event: "mouseover",
        //disabled: true,
        classes: {
            "ui-tabs": "highlight"
        },
        heightStyle: "content",
        //hide: true
        // hide: { effect: "explode", duration: 1000 },
        //   show: { effect: "blind", duration: 800 }
    });

    $scope.imageSrc = null;
  
    $scope.employeesList = [];
    $scope.employeesTotalCount = 0;
    $scope.pageNum = 1;
    $scope.pageSize = 10;
    $scope.pagesCount = 0;
    $scope.maxSize = 5;
    $scope.moduleResourse = {};
    $scope.inputReuired = {};
    $scope.displayORNot = {};
    $scope.inputDataType = {};

    $scope.radioVal = 2;

    $scope.wariningList = [{ warningId: 1, warningName: "option 1" }, { warningId: 2, warningName: "option 2" }, { warningId: 3, warningName: "option 3" } ];

    //Employee Contacts
    $scope.AddContact = function () {
        $scope.employees.Contacts.push($scope.EmployeeContact);
        $scope.EmployeeContact = {};
    }
    $scope.EditContact = function (item) {
        $scope.EmployeeContact = item;
    }
    $scope.DeleteContact = function (index) {
        $scope.employees.Contacts.splice(index, 1);
    }
    //******************

    //Employee academic degrees
    $scope.AddAcademicDegree = function () {
        $scope.employees.AcademicDegrees.push($scope.EmployeeAcademicDegree);
        $scope.EmployeeAcademicDegree = {};
    }
    $scope.EditAcademicDegree = function (item) {
        $scope.EmployeeAcademicDegree = item;
    }
    $scope.DeleteAcademicDegree = function (index) {
        $scope.employees.AcademicDegrees.splice(index, 1);
    }
    //******************

    //Employee Experiences
    $scope.AddExperience = function () {
        $scope.employees.Experiences.push($scope.EmployeeExperience);
        $scope.EmployeeExperience = {};
    }
    $scope.EditExperience = function (item) {
        $scope.EmployeeExperience = item;
    }
    $scope.DeleteExperience = function (index) {
        $scope.employees.Experiences.splice(index, 1);
    }
    //******************

    //Employee training Courses
    $scope.AddCourse = function () {
        $scope.employees.TrainingCourses.push($scope.EmployeeTrainingCourses);
        $scope.EmployeeTrainingCourses = {};
    }
    $scope.EditCourse = function (item) {
        $scope.EmployeeTrainingCourses = item;
    }
    $scope.DeleteCourse = function (index) {
        $scope.employees.TrainingCourses.splice(index, 1);
    }
    //******************

    //Employee Family details
    $scope.AddFamily = function () {
        $scope.employees.FamilyDetails.push($scope.EmployeeFamilyDetail);
        $scope.EmployeeFamilyDetail = {};
    }
    $scope.EditFamily = function (item) {
        $scope.EmployeeFamilyDetail = item;
    }
    $scope.DeleteFamily = function (index) {
        $scope.employees.FamilyDetails.splice(index, 1);
    }
    //******************

    $scope.getPagesCount = function () {
        var div = Math.floor($scope.employeesTotalCount / $scope.pageSize);
        var rem = $scope.employeesTotalCount % $scope.pageSize;
        if (rem > 0) {
            div = div + 1;
        }
        return div;
    }

    $scope.refreshData = function () {
        $scope.getAllOnLoad();
    }
    $scope.saveemployees = function () {
        $scope.saveEntity();
    }
    $scope.saveEntity = function () {
        if ($scope.employees !== undefined && $scope.employees !== null && $scope.employees.EMP_CODE !== null) {
            if ($scope.employees.HrPslEmployeeID === null || $scope.HrPslEmployeeID === 0) {

                if ($scope.imageSrc != null && $scope.imageSrc != undefined)
                    $scope.employees.Item_Base64_Photo = $scope.imageSrc;

                $scope.insert($scope.employees).then(function (results) {
                    $scope.employees.HrPslEmployeeID = results.data;
                    swal({
                        title: 'اضافة موظف',
                        text: 'تم الحفظ بنجاح',
                        type: 'success',
                        timer: 1500,
                        showConfirmButton: false
                    });
                    //for (var i = 0; i < $scope.TelephoneList.length; i++) {
                    //    $scope.TelephoneList[i].TELE_ID = $scope.employees.EMP_ID;
                    //}
                    //var index = $scope.TelephoneList.length - 1;
                    //if ($scope.TelephoneList[index] != null && $scope.TelephoneList[index] != undefined && $scope.TelephoneList[index].TELE_NUMBER == null && $scope.TelephoneList[index].TELE_CAT_ID == null)
                    //    $scope.removeGridItem(index);
                    //$scope.insertEmpContacts($scope.TelephoneList).then(function () {
                    //    $scope.clearEnity(); $scope.refreshData();
                    //});
                    //commonService.showInsertAlert($scope);
                }, function (error) {
                    console.log(error.data.message);
                    swal({
                        title: 'اضافة موظف',
                        text: 'حدث خطأ ما!',
                        type: 'error',
                        timer: 1500,
                        showConfirmButton: false
                    });
                    //commonService.showErrorInsertAlert($scope);
                });
            } else {

                //if ($scope.imageSrc != null && $scope.imageSrc != undefined)
                //    $scope.employees.Item_Base64_Photo = $scope.imageSrc;


                $scope.update($scope.employees).then(function (results) {
                    swal({
                        title: 'تعديل الموظف',
                        text: 'تم التعديل بنجاح',
                        type: 'success',
                        timer: 1500,
                        showConfirmButton: false
                    });
                    //var index = $scope.TelephoneList.length - 1;
                    //if ($scope.TelephoneList[index] != null && $scope.TelephoneList[index] != undefined && $scope.TelephoneList[index].TELE_NUMBER == null && $scope.TelephoneList[index].TELE_CAT_ID == null)
                    //    $scope.removeGridItem(index);
                    //$scope.insertEmpContacts($scope.TelephoneList).then(function () {
                    //    $scope.clearEnity(); $scope.refreshData();
                    //});
                    //commonService.showUpdateAlert($scope);
                }, function (error) {
                    console.log(error.data.message);
                    swal({
                        title: 'تعديل الموظف',
                        text: 'حدث خطأ ما!',
                        type: 'error',
                        timer: 1500,
                        showConfirmButton: false
                    });
                    //commonService.showErrorUpdateAlert($scope);
                });
            }
        }
    }
    $scope.dirEnity = function (entity) {
        $scope.imageSrc = "data:image/png;base64," + entity.EMP_PHOTO;
        $scope.employees = entity;

        $scope.getEmployeeAcademicDegrees($scope.employees.HrPslEmployeeID);
        $scope.getEmployeeExperiences($scope.employees.HrPslEmployeeID);
        $scope.getEmployeeFamilyDetails($scope.employees.HrPslEmployeeID);
        $scope.getEmployeeTrainingCourses($scope.employees.HrPslEmployeeID);
        $scope.getEmployeeContacts($scope.employees.HrPslEmployeeID);

        //if ($scope.employees.EMP_NATIONAL_ID == null) {
        //    $scope.EmpNationStatus = 2;
        //    $scope.empNatStaShow2 = true;
        //    $scope.empNatStaShow = false;
        //}
        ////$scope.getAllEmpTelephone();
        //$scope.GetGovernatesByNationID();
    }

    $scope.getEmployeeAcademicDegrees = function (employeeId) {
        employeesService.getEmployeeAcademicDegrees(employeeId).then(function (responce) {
            $scope.employees.AcademicDegrees = responce.data;
        });
    }
    $scope.getEmployeeExperiences = function (employeeId) {
        employeesService.getEmployeeExperiences(employeeId).then(function (responce) {
            $scope.employees.Experiences = responce.data;
        });
    }
    $scope.getEmployeeFamilyDetails = function (employeeId) {
        employeesService.getEmployeeFamilyDetails(employeeId).then(function (responce) {
            $scope.employees.FamilyDetails = responce.data;
        });
    }
    $scope.getEmployeeTrainingCourses = function (employeeId) {
        employeesService.getEmployeeTrainingCourses(employeeId).then(function (responce) {
            $scope.employees.TrainingCourses = responce.data;
        });
    }
    $scope.getEmployeeContacts = function (employeeId) {
        employeesService.getEmployeeContacts(employeeId).then(function (responce) {
            $scope.employees.Contacts = responce.data;
        });
    }

    $scope.deleteEnity = function (entity) {
        swal({
            title: 'هل تريد حذف الموظف؟',
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
                        text: "هذا الموظف تمت عليه عمليات مختلفة. لا تستطيع حذفه",
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
        //commonService.showDeleteAlert($scope, entity);

    }

    // status of nationality

    $scope.employeeNatList = [{ typeID: 1, typeName: "مواطن" }, { typeID: 2, typeName: "مقيم (مغترب) " }];
    $scope.EmpNationStatus = 1;
    $scope.empNatStaShow = true;
    $scope.empNatStaShow2 = false;


    $scope.getUserStatusOfNat = function () {
        if ($scope.EmpNationStatus == 1) {
            $scope.empNatStaShow = true;
            $scope.empNatStaShow2 = false;
        } else if ($scope.EmpNationStatus == 2) {
            $scope.empNatStaShow = false;
            $scope.empNatStaShow2 = true;
            }

    };


    $scope.getAllOnLoad = function () {
        $q.all(
            [
                $scope.getemployeesList(),
                $scope.getemployeesTotalCount(),
                $scope.getAllNationList(),
                $scope.getAllCompanyBranchesList(),
                $scope.GetAllEmployeeTypes(),
                $scope.getAllCustomersAccounts(),
                //$scope.getTeleCatTypes(),
                $scope.getlastCode(),
                $scope.setLocalStorage(),

                $scope.getMartialStatusList(),
                $scope.getRelegionList(),
                $scope.getCityList(),
                $scope.getNationalityList(),
                $scope.getJobTitleList(),
                $scope.getJobLevelList(),
                $scope.getDeptartmentList(),
                $scope.getEmployeeStatusList(),
                $scope.getDirectManagerList(),
                $scope.getContactTypeList(),
                $scope.getAcademicDegreeList(),
                $scope.getBankList()

            ]).then(function (allResponses) {
                $scope.clearEnity();

                var urlParams = $location.search();
                if (urlParams.foo != null && urlParams.foo != undefined && urlParams.foo != "") {

                    employeesService.getById(parseInt(urlParams.foo)).then(function (results) {
           
                        $scope.employees = results.data;
                        $scope.dirEnity(results.data);
                    })


                }

            }, function (error) {
                //This will be called if $q.all finds any of the requests erroring.
                var abc = error;
                var def = 99;
            });
    }

    $scope.getMartialStatusList = function () {
        employeesService.getMartialStatusList().then(function (response) {
            $scope.MartialStatusList = response.data;
        })
    }
    $scope.getRelegionList = function () {
        employeesService.getRelegionList().then(function (response) {
            $scope.RelegionList = response.data;
        })
    }
    $scope.getCityList = function () {
        employeesService.getCityList().then(function (response) {
            $scope.CityList = response.data;
        })
    }
    $scope.getNationalityList = function () {
        employeesService.getNationalityList().then(function (response) {
            $scope.NationalityList = response.data;
        })
    }
    $scope.getJobTitleList = function () {
        employeesService.getJobTitleList().then(function (response) {
            $scope.JobTitleList = response.data;
        })
    }
    $scope.getJobLevelList = function () {
        employeesService.getJobLevelList().then(function (response) {
            $scope.JobLevelList = response.data;
        })
    }
    $scope.getDeptartmentList = function () {
        employeesService.getDeptartmentList().then(function (response) {
            $scope.DeptartmentList = response.data;
        })
    }
    $scope.getEmployeeStatusList = function () {
        employeesService.getEmployeeStatusList().then(function (response) {
            $scope.EmployeeStatusList = response.data;
        })
    }
    $scope.getDirectManagerList = function () {
        employeesService.getDirectManagerList().then(function (response) {
            $scope.DirectManagerList = response.data;
        })
    }
    $scope.getContactTypeList = function () {
        employeesService.getContactTypeList().then(function (response) {
            $scope.ContactTypeList = response.data;
        })
    }
    $scope.getAcademicDegreeList = function () {
        employeesService.getAcademicDegreeList().then(function (response) {
            $scope.AcademicDegreeList = response.data;
        })
    }
    $scope.getBankList = function () {
        employeesService.getBankList().then(function (response) {
            $scope.BankList = response.data;
        })
    }

    $scope.getemployeesTotalCount = function () {
        $scope.count().then(function (results) {
            var data = results.data;
            $scope.employeesTotalCount = data;
            $scope.pagesCount = $scope.getPagesCount();
        }, function (error) {
            console.log(error.data.message);
        });
    }
    $scope.getemployeesList = function () {
        $scope.get($scope.pageNum, $scope.pageSize).then(function (results) {
            var data = results.data;
            $scope.employeesList = data;

        }, function (error) {
            console.log(error.data.message);
        });
    }


    $scope.get = function (pageNum, pageSize) {
        return employeesService.get(pageNum, pageSize);
    }
    $scope.count = function () {
        return employeesService.count();
    }
    $scope.insert = function (entity) {
        return employeesService.insert(entity);
    }
    $scope.insertGetID = function (entity) {
        return employeesService.insertGetID(entity);
    }
    $scope.insertEmpContacts = function (entity) {
        return telephoneService.insertList(entity);
    }
    $scope.getTeleCatTypes = function () {
        telephoneService.getTeleCat().then(function (results) {
            return $scope.catList = results.data;
        });
    }
    $scope.showSelectedTeleCatTypes = function (user) {
        var selected = [];
        if (user != null && user != undefined) {
            if (user.TELE_CAT_ID) {
                selected = $filter('filter')($scope.catList, { TELE_CAT_ID: user.TELE_CAT_ID });
            }
            return selected.length ? selected[0].TELE_CAT_AR_NAME : 'Not set';
        }
    };
    $scope.update = function (entity) {
        return employeesService.update(entity);
    }
    $scope.delete = function (entity) {
        return employeesService.delete(entity);
    }
    $scope.employeesPageLoad = function () {
        $scope.getAllOnLoad();
    }


    $scope.getAllNationList = function () {
        commonService.getAllNations().then(function (response) {
            $scope.NationList = response.data;
        })
    }


    $scope.removeGridItem = function (index) {
        $scope.TelephoneList.splice(index, 1);
    };

    $scope.hideButton = function (index) {
        if ($scope.TelephoneList[index].TELE_NUMBER == null || $scope.TelephoneList[index].TELE_NUMBER == "" || $scope.TelephoneList[index].TELE_CAT_ID == null) {
            return true;
        } else return false;
    };

    $scope.showButton = function (index) {
        if ($scope.TelephoneList[index].TELE_NUMBER == null || $scope.TelephoneList[index].TELE_NUMBER == "" || $scope.TelephoneList[index].TELE_CAT_ID == null) {
            return false;
        } else return true;
    };

    //$scope.insertItem = function (data, Index) {
    //    if (data != "" && data != null) {
    //        if (data != undefined && data != null) {
    //            if (data.TELE_NUMBER != null && data.TELE_NUMBER != "" && data.TELE_CAT_ID != null) {
    //                if ($scope.TelephoneList[Index] != null && $scope.TelephoneList[Index] != undefined) {
    //                    $scope.removeGridItem(Index);
    //                }
    //                if ($scope.employees.EMP_ID === null || $scope.EMP_ID === 0)
    //                { $scope.telephoneItem.TELE_ID = null; }
    //                else { $scope.telephoneItem.TELE_ID = $scope.employees.EMP_ID; }
    //                $scope.telephoneItem.TELE_NUMBER = data.TELE_NUMBER;
    //                $scope.telephoneItem.TELE_CAT_ID = data.TELE_CAT_ID;
    //                $scope.TelephoneList.push($scope.telephoneItem);
    //                $scope.addGridItem();
    //            }
    //        }
    //        else {
    //            $scope.removeGridItem(Index);
    //            $scope.TelephoneList.push(data);
    //            $scope.addGridItem();
    //        }
    //    }
    //}


    //$scope.getAllEmpTelephone = function () {
    //    telephoneService.getBy($scope.employees.EMP_ID, 3).then(function (response) {
    //        $scope.TelephoneList = response.data;
    //        $scope.addGridItem();
    //    })
    //}

    //$scope.telephoneItem = {};
    //$scope.TelephoneList = [];
    //$scope.addGridItem = function () {
    //    var found = false;
    //    for (var i = 0; i < $scope.TelephoneList.length; i++) {
    //        if ($scope.TelephoneList[i] == null || $scope.TelephoneList[i] == {}) {
    //            found = true;
    //        }
    //    }
    //    if (!found) {
    //        $scope.telephoneItem = {
    //            TELE_ID: null, TELE_NUMBER: null, TELE_TYPE_ID: 3, TELE_CAT_ID: null
    //        };
    //        $scope.TelephoneList.push($scope.telephoneItem);
    //    }
    //};


    $scope.getAllCompanyBranchesList = function () {

        commonService.getAllCompanyBranches().then(function (response) {
            $scope.CompanyBranchesList = response.data;
        })
    }

    $scope.getAllGovernateTown = function () {
        $scope.GetTownsByGovernateID();
    }
    $scope.getAllTownVillage = function () {
        $scope.GetVillageByTownID();
    }

    $scope.getAllNastionGovernate = function () {
        $scope.GetGovernatesByNationID();
    }
    $scope.GetGovernatesByNationID = function () {
        commonService.GetGovernatesByNationID($scope.employees.NATION_ID).then(function (response) {
            $scope.GovernateListByNationID = response.data
        }, function (error) {
            console.log(error.data.message);
        });
    }
    $scope.GetTownsByGovernateID = function () {
        commonService.GetTownsByGovernateID($scope.employees.GOV_ID).then(function (response) {
            $scope.TownsListByGovernateID = response.data
        }, function (error) {
            console.log(error.data.message);
        });
    }
    $scope.GetVillageByTownID = function () {
        commonService.GetVillageByTownID($scope.employees.TOWN_ID).then(function (response) {
            $scope.VillagesListByTownID = response.data
        }, function (error) {
            console.log(error.data.message);
        });
    }
    $scope.GetDepartmentByCompanyBranchID = function () {
        commonService.GetDepartmentByCompanyBranchID($scope.employees.COM_BRN_ID).then(function (response) {
            $scope.GetDepartmentByCompanyBranchIDList = response.data
        }, function (error) {
            console.log(error.data.message);
        });
    }
    $scope.GetAllDepartments = function () {
        departmentsService.getAllDepartments().then(function (response) {
            $scope.DepartmentList = response.data;


        }, function (error) {
            console.log(error.data.message);
        });
    }
    $scope.GetAllEmployeeTypes = function () {
        employeeTypesService.getAll().then(function (response) {
            $scope.EmployeeTypesList = response.data
        }, function (error) {
            console.log(error.data.message);
        });
    }
    $scope.getAllCustomersAccounts = function () {
        commonService.getAllCustomersAccounts().then(function (response) {
            $scope.CustomersAccountsList = response.data
        }, function (error) {
            console.log(error.data.message);
        });
    }

    $scope.pageChanged = function () {
        $scope.getAllOnLoad();
        $log.log('Page changed to: ' + $scope.pageNum);
    };


    ///////////////////////////telephones

    //$scope.getTeleCatTypes = function () {
    //    telephoneService.getTeleCat().then(function (results) {
    //        return $scope.catList = results.data;
    //    });
    //}


    $scope.addTeleToList = function () {
        $scope.typeName = {};
        telephoneService.getTypeNameByID($scope.teleCatId).then(function (result) {
            $scope.typeName = result.data;
            $scope.TelephoneList.push({ TELE_NUMBER: $scope.teleNumber, TELE_CAT_ID: $scope.teleCatId, TeleCatName: $scope.typeName.TELE_CAT_AR_NAME, TELE_TYPE_ID: 3 });
            $scope.teleNumber = "";
            $scope.teleTypeId = "";
        });
    }



    $scope.removeTeleItem = function (index) {
        $scope.TelephoneList.splice(index, 1);
    };


    $scope.getlastCode = function () {
        var urlParams = $location.search();
        if (urlParams.foo != null && urlParams.foo != undefined && urlParams.foo != "") { } else {
            employeesService.getLastCode().then(function (result) {
                $scope.employees.EmployeeCode = parseInt(result.data) + 1;
            }, function (error) { });
        }
    }




    //////////////////////////load image
    $scope.imageSrc = null;

    $scope.imageUpload = function (event) {
        var files = event.target.files; //FileList object

        for (var i = 0; i < files.length; i++) {
            var file = files[i];
            var reader = new FileReader();
            reader.onload = $scope.imageIsLoaded;
            reader.readAsDataURL(file);
        }
    }

    $scope.imageIsLoaded = function (e) {
        $scope.$apply(function () {
            $scope.imageSrc = e.target.result;
        });
    }



    $scope.setLocalStorage = function () {

        //commonService.getAllResources().then(function (result) {

        //    var all_resourese = result.data;

        //    var json_res = JSON.parse(all_resourese);

        //    console.log(json_res["rt"]);

        //    localStorage.setItem("resoursesModule", JSON.stringify(json_res["r"]));

        //    commonService.loadModuleResources($scope, $location);
        //});

        //var pageName = $location.path().replace('/', '');

        //commonService.setLocalStorage(pageName);

        //pageName = "CURRENCIES";
        
        /*commonService.getResourceByName(pageName).then(function (result) {
            console.log(result.data);
            //commonService.setLocalStorage(result.data.ID);

            var module_resourse = result.data;
     
        }, function (error) { });

        /*var queryResult = document.querySelector('.panel-body');
        console.log(queryResult);
        var wrappedQueryResult = angular.element(queryResult);
        console.log(wrappedQueryResult);

        queryResult = document.querySelectorAll('[data-rt="EMP_CODE"]');
        console.log(queryResult);

        wrappedQueryResult = angular.element(queryResult);
        console.log(wrappedQueryResult[0].dataset.id);


        wrappedQueryResult.text("hhhhhhhhhhhhh");*/

        //var panes = [12, 55, 44, 77, 588];
        //angular.forEach(panes, function (pane,i) {
        //    console.log(i);
        //});

        return false;

        var a = $("input[name=barcode]").attr("ng-model");
        console.log(a);

        //var queryResult = document.querySelectorAll('[ng-model]');
        ///console.log(queryResult);

        var FieldName = a.split('.')[1];
        console.log(FieldName);

        var queryResult = document.querySelectorAll("[ng-model]");
        angular.forEach(queryResult, function (val, key) {

            var div_element = angular.element(val);

            var ngModel_attr = div_element.attr("ng-model");

            var FieldName = ngModel_attr.split('.')[1];

            div_element.attr("data-test", FieldName);

            //var label = $(div_element).prev().prev('label');

            var label = $(div_element).prevAll('label');

            ////console.log(label);

            $(label).attr("data-aaaa", "123456789");


        });


    }



}]);
