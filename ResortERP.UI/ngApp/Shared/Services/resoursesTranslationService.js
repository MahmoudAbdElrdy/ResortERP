'use strict';
app.factory('resourcesTranslationService', ['$http', 'sharedService', 'localStorageService','$filter', function ($http, sharedService, localStorageService, $filter) {



    var serviceBase = sharedService.getBaseUrl();
    var resourcesTranslationServiceFactory = {};



    var _getAll = function () {
        var myUrl = serviceBase + 'resourcesTranslation/getAll';
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    var _update = function (entity) {
        var myUrl = serviceBase + 'resourcesTranslation/update';
        var data = {};
        data = entity;
        return $http({
            method: 'POST',
            url: myUrl,
            data: JSON.stringify(data)
        }).then(function (results) {
            return results;
        });
    }

    //{ cache: true }


    var _updateById = function (entity) {
        var myUrl = serviceBase + 'resourcesTranslation/updateById';
        var data = {};
        data = entity;
        return $http({
            method: 'POST',
            url: myUrl,
            data: JSON.stringify(data)
        }).then(function (results) {
            return results;
        });
    }

    var _getAllResources = function () {
        var myUrl = serviceBase + 'resources/getAll';
        return $http.get(myUrl).then(function (results) {
            return results;
        });
    }

    var _setElementText = function (element, textValue) {

        if (element.prop("tagName") == "LABEL" || element.prop("tagName") == "TH" || element.prop("tagName") == "DIV") {

            element.text(textValue);
        }
        else if (element.prop("tagName") == "INPUT" || element.prop("tagName") == "TEXTAREA") {

            if (element.prop("type") == "checkbox")
                element.next("label").text(textValue);
            else
                element.attr("placeholder", textValue);
        }
        else if (element.prop("tagName") == "SELECT") {

            $(element).find("option:first-child").text(textValue);
        }
        else if (element.prop("tagName") == "IMG") {

            $(element).attr("alt", textValue);
        }
    }


    var _getElementText = function (element) {

        var text;

        if (element.prop("tagName") == "LABEL" || element.prop("tagName") == "TH" || element.prop("tagName") == "DIV") {

            text = angular.element(element).text().trim();
        }
        else if (element.prop("tagName") == "INPUT" || element.prop("tagName") == "TEXTAREA") {

            if (element.prop("type") == "checkbox")
                text = angular.element(element).next('label').text();
            else
                text = angular.element(element).attr("placeholder");
        }
        else if (element.prop("tagName") == "SELECT") {

            text = $(element).find("option:first-child").text();
        }
        else if (element.prop("tagName") == "IMG") {

            text = (element).attr("alt");
        }

        return text;
    }

    var _setLocalStorage = function ($scope, ResourcesID) {

        _setFieldNameAttributes($scope);

        var resoursesValue = localStorage.getItem("resourses");
        var resoursesModuleValue = localStorage.getItem("resoursesModule");

        if (resoursesValue && resoursesModuleValue) {

            _loadResources($scope, ResourcesID);
            _loadModuleResources($scope, ResourcesID);
        }
        else {
            _getAllResources().then(function (result) {

                console.log(result);

                var all_resourese = result.data;

                var json_res = JSON.parse(all_resourese);

                var resourses = json_res["rt"];
                var resoursesModule = json_res["r"];

                localStorage.setItem('resourses', JSON.stringify(resourses));

                localStorage.setItem("resoursesModule", JSON.stringify(resoursesModule));

                _loadResources($scope, ResourcesID);
                _loadModuleResources($scope, ResourcesID);

                $scope.requiredValidation['LoadResource'] = true;
                
            }, function (error) { });
        }
    }

    var _loadResources = function ($scope, ResourcesID) {

        var resoursesValue = localStorage.getItem("resourses");

        var resourses = JSON.parse(resoursesValue);

        var res = jQuery.grep(resourses, function (a) {
            //return a.ResourcesID == ResourcesID;
            return a.ResourceName.toUpperCase() == ResourcesID.toUpperCase()
        });

        console.log(res);

        //return;

        var langId = _getLanguageId();

        angular.forEach(res, function (val, key) {

            var queryResult = document.querySelectorAll('[data-rt="' + val.FieldName + '"]');
            var element = angular.element(queryResult);

            _setElementText(element, val.ARGridColumnText);
            if (langId == "2") {
                _setElementText(element, val.LatGridColumnText);
            }
   
            queryResult = document.querySelectorAll('[data-rte="' + val.FieldName + '"]');
            element = angular.element(queryResult);

            _setElementText(element, val.ARPlaceholderText);

            if (langId == "2") {
                _setElementText(element, val.LatPlaceholderText);
            }
            
            //$('[data-rt="' + val.FieldName + '"]').attr("data-id", val.ID);

            //$('[data-rte="' + val.FieldName + '"]').attr("data-id", val.ID);

            queryResult = document.querySelectorAll('[data-rt="' + val.FieldName + '"]');
            angular.element(queryResult).attr("data-id", val.ID);

            queryResult = document.querySelectorAll('[data-rte="' + val.FieldName + '"]');
            angular.element(queryResult).attr("data-id", val.ID);

            angular.element(queryResult).attr("data-hidden", val.DisplayORNot);

            $scope.inputReuired[val.FieldName] = val.IsRequired;
            $scope.displayORNot[val.FieldName] = val.DisplayORNot;
            $scope.inputDataType[val.FieldName] = val.InputDataType;

            if ($scope.inputReuired[val.FieldName] == true) {
                //element.prop("required", true);
            }
            else {
                //element.prop("required", false);
            }

            if (val.InputDataType == "1") {
                $(element).prop('type', 'text');
            }
            else if (val.InputDataType== "2") {
                $(element).prop('type', 'number');
            }

            if (val.DisplayORNot == "0") {
                $(element).closest(".form-group").addClass("hidden");
                $(element).closest(".form-group").addClass("hidden-elements");
                $(element).closest(".input-group").addClass("hidden-elements");
            }

        });
    }

    var _updateResource = function ($scope, element) {

        var id = angular.element(element).attr("data-id");

        var langId = _getLanguageId();

        var attr_name = angular.element(element).attr("data-rt");
        if (!attr_name) {
            attr_name = angular.element(element).attr("data-rte");
        }

        var alert_content = '';

        alert_content = _getAlertHtmlContent($scope, element, attr_name);

        swal({
            //input: 'text',
            //inputValue: _getElementText(angular.element(element)),
            showCancelButton: true,
            confirmButtonText: $scope.moduleResourse["OkButton"],
            cancelButtonText: $scope.moduleResourse["CancelButton"],
            allowOutsideClick: false,
            html: alert_content
        }).then(value => {


            value = angular.element(document.querySelector('#swal-input2')).val();

            var fieldRequired = angular.element(document.querySelector('input[name=IsRequired]'));
            var reqValue = true;
            (fieldRequired[0]) ? reqValue = fieldRequired[0].checked : reqValue = true;

            var fieldDisplay = angular.element(document.querySelector('input[name=displayOrNot]'));
            var displayValue = true;
            (fieldDisplay[0]) ? displayValue = fieldDisplay[0].checked : displayValue = true;

            var displayVal = (displayValue == true) ? "1" : "0";

            var dataType = angular.element(document.querySelector('#ddlDataType'));
            var dataTypeVal = dataType.val();

            (displayValue == false) ? reqValue = false : null;

            var element_attr = angular.element(element).attr('data-rt');
            var queryResult = document.querySelectorAll('[data-rt="' + element_attr + '"]');
            var element_selector = angular.element(queryResult);

            console.log($scope.inputReuired);

            _setElementText(element_selector, value);

            var entity = { "ID": id, "ARGridColumnText": value, "DisplayORNot": displayVal, "IsRequired": reqValue, "InputDataType": dataTypeVal };
            if (langId == "2") {
                entity = { "ID": id, "LatGridColumnText": value, "DisplayORNot": displayVal, "IsRequired": reqValue, "InputDataType": dataTypeVal };
            }

            var attr = angular.element(element).attr('data-rte');
            if (typeof attr !== typeof undefined && attr !== false) {

                entity = { "ID": id, "ARPlaceholderText": value, "DisplayORNot": displayVal, "IsRequired": reqValue, "InputDataType": dataTypeVal };
                if (langId == "2") {
                    entity = { "ID": id, "LatPlaceholderText": value, "DisplayORNot": displayVal, "IsRequired": reqValue, "InputDataType": dataTypeVal };
                }

                var element_attr = angular.element(element).attr('data-rte');
                var queryResult = document.querySelectorAll('[data-rte="' + element_attr + '"]');
                var element_selector = angular.element(queryResult);

                _setElementText(element_selector, value);
            }

            $scope.inputReuired[element_attr] = reqValue;
            $scope.displayORNot[element_attr] = displayValue;
            $scope.inputDataType[element_attr] = dataTypeVal;

            if (dataTypeVal == "1") {
                $(element).prop('type', 'text');
            }
            else if (dataTypeVal == "2") {
                $(element).prop('type', 'number');
            }

            if (displayValue == false) {
                $(element_selector).closest(".form-group").addClass("hidden");
                angular.element(element_selector).attr("data-hidden", "0");

                $(element_selector).closest(".input-group").addClass("hidden");
                angular.element(element_selector).attr("data-hidden", "0");
            }
            else {
                angular.element(element_selector).attr("data-hidden", "1");
                $(element_selector).closest(".form-group").removeClass("hidden-elements");

                angular.element(element_selector).attr("data-hidden", "1");
                $(element_selector).closest(".input-group").removeClass("hidden-elements");
            }

            $scope.requiredValidation[element_attr] = !$scope.requiredValidation[element_attr];

            _updateById(entity).then(function (results) {

                localStorage.removeItem('resourses');

            }, function (error) {

            });

        });
    }

    var _loadModuleResources = function ($scope, ResourceName) {

        //pageName = "CURRENCIES";

        var resoursesValue = localStorage.getItem("resoursesModule");

        var resourses = JSON.parse(resoursesValue);

        var res = jQuery.grep(resourses, function (a) {

            return a.ResourceName.toUpperCase() == ResourceName.toUpperCase()
        });

        console.log(res[0]);

        var module_resourse = res[0];

        var langId = _getLanguageId();

        if (langId == 1) {

            $scope.moduleResourse["Header"] = module_resourse["ARHeader"];
            $scope.moduleResourse["GridHeaderText"] = module_resourse["ARGridHeaderText"];
            $scope.moduleResourse["RefreshButton"] = module_resourse["ARRefreshButton"];
            $scope.moduleResourse["NewButton"] = module_resourse["ARNewButton"];
            $scope.moduleResourse["BrowseButton"] = module_resourse["ARBrowseButton"];
            $scope.moduleResourse["AddOtherButton"] = module_resourse["ARAddOtherButton"];

            $scope.moduleResourse["GroupOne"] = module_resourse["ARGroupOne"];
            $scope.moduleResourse["GroupTwo"] = module_resourse["ARGroupTwo"];
            $scope.moduleResourse["GroupThree"] = module_resourse["ARGroupThree"];
            $scope.moduleResourse["GroupFour"] = module_resourse["ARGroupFour"];

            $scope.moduleResourse["ResourceTitle"] = module_resourse["ARResourceTitle"];

            $scope.moduleResourse["DeleteButton"] = module_resourse["ARDeleteButton"];
            $scope.moduleResourse["DeleteConfirmationMessage"] = module_resourse["ARDeleteConfirmationMessage"];
            $scope.moduleResourse["DeleteDoneMessage"] = module_resourse["ARDeleteDoneMessage"];
            $scope.moduleResourse["DeleteCancelMessage"] = module_resourse["ARDeleteCancelMessage"];
            $scope.moduleResourse["DeleteRestoreMessage"] = module_resourse["ARDeleteRestoreMessage"];

            $scope.moduleResourse["UpdateButton"] = module_resourse["ARUpdateButton"];
            $scope.moduleResourse["UpdateMessage"] = module_resourse["ARUpdateMessage"];
            $scope.moduleResourse["UpdateDoneMessage"] = module_resourse["ARUpdateDoneMessage"];

            $scope.moduleResourse["AddButton"] = module_resourse["ARAddButton"];
            $scope.moduleResourse["AddDoneMessage"] = module_resourse["ARAddDoneMessage"];

            $scope.moduleResourse["OkButton"] = module_resourse["AROkButton"];
            $scope.moduleResourse["CancelButton"] = module_resourse["ARCancelButton"];
            $scope.moduleResourse["DoneText"] = module_resourse["ARDoneText"];
            $scope.moduleResourse["SorryText"] = module_resourse["ARSorryText"];

            $scope.moduleResourse["AddErrorMessage"] = module_resourse["ARAddErrorMessage"];
            $scope.moduleResourse["UpdateErrorMessage"] = module_resourse["ARUpdateErrorMessage"];

            $scope.moduleResourse["TotalRecordsText"] = module_resourse["ARTotalRecordsText"];

        }
        else if (langId == 2) {

            $scope.moduleResourse["Header"] = module_resourse["LatHeader"];
            $scope.moduleResourse["GridHeaderText"] = module_resourse["LatGridHeaderText"];
            $scope.moduleResourse["RefreshButton"] = module_resourse["LatRefreshButton"];
            $scope.moduleResourse["NewButton"] = module_resourse["LatNewButton"];
            $scope.moduleResourse["BrowseButton"] = module_resourse["LatBrowseButton"];
            $scope.moduleResourse["AddOtherButton"] = module_resourse["LatAddOtherButton"];

            $scope.moduleResourse["GroupOne"] = module_resourse["LatGroupOne"];
            $scope.moduleResourse["GroupTwo"] = module_resourse["LatGroupTwo"];
            $scope.moduleResourse["GroupThree"] = module_resourse["LatGroupThree"];
            $scope.moduleResourse["GroupFour"] = module_resourse["LatGroupfour"];

            $scope.moduleResourse["ResourceTitle"] = module_resourse["LatResourceTitle"];

            $scope.moduleResourse["DeleteButton"] = module_resourse["LatDeleteButton"];
            $scope.moduleResourse["DeleteConfirmationMessage"] = module_resourse["LatDeleteConfirmationMessage"];
            $scope.moduleResourse["DeleteDoneMessage"] = module_resourse["LatDeleteDoneMessage"];
            $scope.moduleResourse["DeleteCancelMessage"] = module_resourse["LatDeleteCancelMessage"];
            $scope.moduleResourse["DeleteRestoreMessage"] = module_resourse["LatDeleteRestoreMessage"];

            $scope.moduleResourse["UpdateButton"] = module_resourse["LatUpdateButton"];
            $scope.moduleResourse["UpdateMessage"] = module_resourse["LatUpdateMessage"];
            $scope.moduleResourse["UpdateDoneMessage"] = module_resourse["LatUpdateDoneMessage"];

            $scope.moduleResourse["AddButton"] = module_resourse["LatAddButton"];
            $scope.moduleResourse["AddDoneMessage"] = module_resourse["LatAddDoneMessage"];

            $scope.moduleResourse["OkButton"] = module_resourse["LatOkButton"];
            $scope.moduleResourse["CancelButton"] = module_resourse["LatCancelButton"];
            $scope.moduleResourse["DoneText"] = module_resourse["LatDoneText"];
            $scope.moduleResourse["SorryText"] = module_resourse["LatSorryText"];

            $scope.moduleResourse["AddErrorMessage"] = module_resourse["LatAddErrorMessage"];
            $scope.moduleResourse["UpdateErrorMessage"] = module_resourse["LatUpdateErrorMessage"];

            $scope.moduleResourse["TotalRecordsText"] = module_resourse["LatTotalRecordsText"];
        }

    }

    var _getLanguageId = function () {

        var langId = 1;
        var langItem = localStorage.getItem("ls.userLang");
        if (langItem) {
            langId = JSON.parse(langItem).LanguageID;
        }
        return langId;
    }

    var _getAlertHtmlContent = function ($scope, element, attr_name) {

        var alert_content = '';

        var elementType = _getElementTagName(angular.element(element));

        alert_content += '<input id="swal-input2" class="swal2-input" value="' + _getElementText(angular.element(element)) + '">' + '<br>';

        if (elementType == "label")
            return alert_content;

        alert_content += '<div class="row" style="margin-bottom: 18px;">' + '<div class="col-md-6 text-right">';

        if ($scope.inputReuired[attr_name] == false) {

            alert_content += '<label><input type="checkbox" name="IsRequired"> مطلوب</label>';
        }
        else {
            alert_content += '<label><input type="checkbox" name="IsRequired" checked> مطلوب</label>';
        }
        alert_content += '<br>' + '</div>' +
            '<div class="col-md-6 text-right">';

        if ($scope.displayORNot[attr_name] == "1") {

            alert_content += '<label><input type="checkbox" name="displayOrNot" checked> اخفاء</label>';
        }
        else {
            alert_content += '<label><input type="checkbox" name="displayOrNot"> اظهار</label>';
        }

        alert_content += '<br>' + '</div>' + '</div>';
        

        if (elementType == "select" || elementType == "label" || elementType =="checkbox")
            return alert_content;

        alert_content += '<select id="ddlDataType" class="form-control">';

        if (!$scope.inputDataType[attr_name]) {
            alert_content += '<option value="" selected>نوع البيانات</option>';
        }
        else {
            alert_content += '<option value="">نوع البيانات</option>';
        }

        if ($scope.inputDataType[attr_name] == "1") {
            alert_content += '<option value="1" selected>نص</option>';
        }
        else {
            alert_content += '<option value="1">نص</option>';
        }

        if ($scope.inputDataType[attr_name] == "2") {
            alert_content += '<option value="2" selected>ارقام</option>';
        }
        else {
            alert_content += '<option value="2">ارقام</option>';
        }

        alert_content += '</select>';

        return alert_content;
    }

    var _setFieldNameAttributes = function ($scope) {

        var queryResult = document.querySelectorAll("[ng-model]");
        angular.forEach(queryResult, function (val, key) {

            var div_element = angular.element(val);

            var ngModel_attr = div_element.attr("ng-model");

            var FieldName = ngModel_attr.split('.')[1];

            div_element.attr("data-rte", FieldName);

            var label = $(div_element).prevAll('label');

            $(label).attr("data-rt", FieldName);

            if (label.length == 0) {
                var prev_div = $(div_element).prev('div.input-group-addon');
                $(prev_div).attr("data-rt", FieldName);
            }

        });
    }

    var _getElementTagName = function (element) {

        if (element.prop("tagName") == "LABEL") {

            return "label";
        }
        else if (element.prop("tagName") == "INPUT" && element.prop("type") == "checkbox") {

            return "checkbox";
        }
        else if (element.prop("tagName") == "INPUT") {

            return "input";
        }
        else if (element.prop("tagName") == "SELECT") {

            return "select";
        }   
        else {

            return "";
        }
    }


    //resourcesTranslationServiceFactory.get = _get;
    resourcesTranslationServiceFactory.getAll = _getAll;
    //resourcesTranslationServiceFactory.count = _count;
    //resourcesTranslationServiceFactory.insert = _insert;
    resourcesTranslationServiceFactory.update = _update;
    //resourcesTranslationServiceFactory.delete = _delete;
    resourcesTranslationServiceFactory.updateById = _updateById;

    resourcesTranslationServiceFactory.setElementText = _setElementText;
    resourcesTranslationServiceFactory.getElementText = _getElementText;
    resourcesTranslationServiceFactory.loadResources = _loadResources;
    resourcesTranslationServiceFactory.setLocalStorage = _setLocalStorage;
    resourcesTranslationServiceFactory.updateResource = _updateResource;
    resourcesTranslationServiceFactory.loadModuleResources = _loadModuleResources;

    return resourcesTranslationServiceFactory;

}]);