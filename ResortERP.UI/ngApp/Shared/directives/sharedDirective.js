'use strict'
app.directive('pressEnter', function () {
    return function (scope, element, attrs) {
        element.bind("keydown keypress", function (event) {
            if (event.which === 13) {
                scope.$apply(function () {
                    scope.$eval(attrs.pressEnter);
                });

                event.preventDefault();
            }
        });
    };
}).directive('getKeyboard', function () {
    return {
        restrict: 'A',
        require: 'ngModel',
        scope: {
            ngModel: '='
        },
        link: function (scope, element, attrs, ngModelCtrl) {
            element.bind("keydown keypress", function (event) {
                if (event.which != 8) {
                    var val = this.value;
                    var str = val.split("+");
                    var count = str.length;
                    for (var j = 0; j < count; j++) {
                        var itm = String(str[j]).trim();
                        if (itm == event.key) {
                            event.preventDefault();
                            ngModelCtrl.$modelValue = this.value;
                            scope.ngModel = this.value;
                            return this.value;
                        }
                    }
                    if (count > 2) {
                        val = "";
                    }
                    event.preventDefault();
                    var modelVal = this.value = (val != null && val != "" && val != undefined) ? val + ' + ' + event.key : (event.key != undefined && event.key != null && event.key != "") ? event.key : val;
                    ngModelCtrl.$modelValue = modelVal;
                    scope.ngModel = modelVal;
                    return this.value = modelVal;
                } else {
                    return this.value = "";
                }
                //return event.key;
            });
        }
    };
}).directive('preventText', function () {
    return function (scope, element, attrs) {
        element.bind("keydown keypress", function (event) {
            event.preventDefault();
        });
    };
}).directive('onlyNumber', function () {
    return {
        require: 'ngModel',
        link: function (scope, element, attr, ngModelCtrl) {
            function fromUser(text) {
                var transformedInput = text.replace(/[^0-9]/g, '');
                console.log(transformedInput);
                if (transformedInput !== text) {
                    ngModelCtrl.$setViewValue(transformedInput);
                    ngModelCtrl.$render();
                }
                return transformedInput;  // or return Number(transformedInput)
            }
            ngModelCtrl.$parsers.push(fromUser);
        }
    };
}).directive('ngConfirmClick', [
    function () {
        return {
            link: function (scope, element, attr) {
                var msg = attr.ngConfirmClick || "Are you sure?";
                var clickAction = attr.confirmedClick;
                element.bind('click', function (event) {
                    if (window.confirm(msg)) {
                        scope.$apply(clickAction)
                    }
                });
            }
        };
    }
]).directive('dateInput',
    function (dateFilter) {
        return {
            require: 'ngModel',
            template: '<input type="date"></input>',
            replace: true,
            link: function (scope, elm, attrs, ngModelCtrl) {
                ngModelCtrl.$formatters.unshift(function (modelValue) {
                    return dateFilter(modelValue, 'dd/MM/yyyy');
                });

                ngModelCtrl.$parsers.unshift(function (viewValue) {
                    return new Date(viewValue);
                });
            },
        };
    }).directive('moDateInput', function ($window) {
        return {
            require: '^ngModel',
            restrict: 'A',
            link: function (scope, elm, attrs, ctrl) {
                var moment = $window.moment;
                var dateFormat = attrs.moDateInput;
                attrs.$observe('moDateInput', function (newValue) {
                    if (dateFormat == newValue || !ctrl.$modelValue) return;
                    dateFormat = newValue;
                    ctrl.$modelValue = new Date(ctrl.$setViewValue);
                });

                ctrl.$formatters.unshift(function (modelValue) {
                    if (!dateFormat || !modelValue) return "";
                    var retVal = moment(modelValue).format(dateFormat);
                    return retVal;
                });

                ctrl.$parsers.unshift(function (viewValue) {
                    var date = moment(viewValue, dateFormat);
                    return (date && date.isValid() && date.year() > 1950) ? date.toDate() : "";
                });
            }
        };
    }).directive('opendialog',
   function () {
       var openDialog = {
           link: function (scope, element, attrs) {
               function openDialog() {
                   var element = angular.element('#myModal');
                   var ctrl = element.controller();
                   ctrl.setModel(scope.blub);
                   element.modal('show');
               }
               element.bind('click', openDialog);
           }
       }
       return openDialog;
   }).directive("ngFileSelect", function () {
       return {
           link: function ($scope, el) {
               el.bind("change", function (e) {
                   $scope.file = (e.srcElement || e.target).files[0];
                   $scope.getFile();
               })
           }
       };
   }).directive("limitTo", [function () {
       return {
           restrict: "A",
           link: function (scope, elem, attrs) {
               var limit = parseInt(attrs.limitTo);
               angular.element(elem).on("keypress", function (e) {
                   if (this.value.length == limit) e.preventDefault();
               });
           }
       }
   }]).directive("maxNum", [function () {
       return {
           restrict: "A",
           link: function (scope, elem, attrs) {
               var limit = parseInt(attrs.maxNum);
               angular.element(elem).on("keyup", function (e) {
                   var val = parseInt(this.value);
                   if (val > limit) {
                       e.preventDefault();
                       return this.value = this.value.substr(0, 2);
                   } else if (val < 0) {
                       e.preventDefault();
                       return this.value = 0;
                   }

               });
           }
       }
   }]).directive("biDirection", function () {
       return {
           restrict: 'C',//target all elements with class bi-direction
           link: function (scope, element, attrs) {
               element.bind('click', function () {
                   $(".sp-right").each(function () {
                       $(this).removeClass("sp-left").addClass("sp-right");
                   });
                   $(".sp-left").each(function () {
                       $(this).removeClass("sp-right").addClass("sp-left");
                   });
               });
           }
       };
   }).directive('typeaheadShowOnFocus', function () {
       return {
           require: 'ngModel',
           link: function ($scope, element, attrs, ngModel) {
               element.bind('focus', function () {
                   ngModel.$setViewValue();
                   $(element).trigger('input');
                   $(element).trigger('change');
               });
           }
       };
    }).directive('resLang', ['commonService', '$location', function (commonService, $location) {
        return {
            scope: false,
            link: function (scope, element, attr) {

                //var pageName = $location.path().replace('/', '');
                //alert(pageName);

                var pageName = attr.resLang;

                commonService.setLocalStorage(scope, pageName);

                var queryResult = document.querySelectorAll('[data-rt],[data-rte]');

                var wrappedQueryResult = angular.element(queryResult);

                wrappedQueryResult.bind('click', function (event) {

                    var changeChecked = angular.element(document.querySelector('#updateResourse'));

                    if (changeChecked[0].checked == false) {
                        return;
                    }
                    var curr_elemet = angular.element(event.target)[0];
                    commonService.updateResource(scope, curr_elemet);

                });

            }
        };
    }]).directive('showHiddenElements', function () {
        return {
            restrict: 'A',
            link: function ($scope, element, attrs, ngModel) {
                element.bind('click', function () {

                    var queryResult = document.querySelectorAll("[data-rte]");
                    angular.forEach(queryResult, function (val, key) {

                        var div_element = angular.element(val);

                        if (element[0].checked) {

                            $(div_element).closest(".form-group").removeClass("hidden");
                            $(div_element).closest(".input-group").removeClass("hidden");
                        }
                        else {
                            var displsyVal = angular.element(div_element).attr("data-hidden");
                            if (displsyVal == "0") {
                                $(div_element).closest(".form-group").addClass("hidden");
                                $(div_element).closest(".input-group").addClass("hidden");
                            }
                        }

                    });
                });
            }
        };
    }).directive('requiredResourceDirective', function () {

        function determineIfRequired(groupName) {

            if (!groupName)
                return true;

            if (groupName.length == 0 || groupName == null || groupName == undefined || groupName === '') {
                return true;
            } else {
                return false;
            }
        }

        function getFieldName(element) {
            var div_element = angular.element(element);
            var ngModel_attr = div_element.attr("ng-model");
            var FieldName = ngModel_attr.split('.')[1];
            return FieldName;
        }

        return {
            require: '?ngModel',
            link: function (scope, element, attr, modelCtrl) {

                if (!modelCtrl) return;

                var group = {};

                //scope.$on('$destroy', function () {

                //});

                function updateValidity() {

                    var div_element = angular.element(element);
                    var ngModel_attr = div_element.attr("ng-model");
                    var FieldName = ngModel_attr.split('.')[1];
                    ////console.log(FieldName);

                    var isReqValue = scope.inputReuired[FieldName];

                    var element_value = angular.element(element).val();


                    if (isReqValue == true) {

                        if (element_value.length == 0 || element_value == null || element_value == undefined || element_value === '') {
                            modelCtrl.$setValidity('required', false);
                        } else {
                            modelCtrl.$setValidity('required', true);
                        }
                    } else {
                        modelCtrl.$setValidity('required', true);
                    }

                }

                function validate(value) {

                    group.isRequired = determineIfRequired(value);

                    var element_attr = getFieldName(element);
                    scope.requiredValidation[element_attr] = group.isRequired;

                    ///console.log("group.isRequired -- " + group.isRequired + "----" + scope.requiredValidation[element_attr]);

                    updateValidity();
                    return value;
                }
 
                modelCtrl.$formatters.push(validate);
                modelCtrl.$parsers.unshift(validate);
                //scope.$watch('checkVal', updateValidity);

                scope.$watch('requiredValidation', function (newVal, oldVal) {

                    updateValidity();
                }, true);


            }
        };
    }).directive('selectToExport', function () {
        return {
            restrict: 'A',
            link: function ($scope, element, attrs, ngModel) {
                element.bind('click', function () {

                    event.stopPropagation();
                });
            }
        };
    });