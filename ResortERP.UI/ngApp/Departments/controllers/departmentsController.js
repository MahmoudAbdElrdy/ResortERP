'use strict';
app.controller('departmentsController', ['$scope', '$location', '$log', '$q', 'departmentsService', 'commonService', function ($scope, $location, $log, $q, departmentsService, commonService) {
    $scope.departments = { DEPT_ID: null, DEPT_CODE: null, DEPT_AR_NAME: null, DEPT_EN_NAME: null, DEPT_AR_ABRV: null, DEPT_EN_ABRV: null, COM_BRN_ID: null };

    $scope.clearEnity = function () {
        $scope.departments = { DEPT_ID: null, DEPT_CODE: null, DEPT_AR_NAME: null, DEPT_EN_NAME: null, DEPT_AR_ABRV: null, DEPT_EN_ABRV: null, COM_BRN_ID: null };
        $scope.getlastCode();
    }

    $scope.departmentsList = [];
    $scope.departmentsTotalCount = 0;
    $scope.pageNum = 1;
    $scope.pageSize = 10;
    $scope.pagesCount = 0;
    $scope.maxSize = 5;

    $scope.moduleResourse = {};
    $scope.inputReuired = {};
    $scope.displayORNot = {};
    $scope.inputDataType = {};
    $scope.requiredValidation = { LoadResource: false };

    $scope.getPagesCount = function () {
        var div = Math.floor($scope.departmentsTotalCount / $scope.pageSize);
        var rem = $scope.departmentsTotalCount % $scope.pageSize;
        if (rem > 0) {
            div = div + 1;
        }
        return div;
    }

    $scope.refreshData = function () {
        $scope.getAllOnLoad();
    }
    $scope.savedepartments = function () {
        $scope.saveEntity();
    }
    $scope.saveEntity = function () {
        if ($scope.departments !== undefined && $scope.departments !== null && $scope.departments.DEPT_CODE !== null && $scope.departments.DEPT_AR_NAME !== null && $scope.departments.DEPT_EN_NAME !== null && $scope.departments.DEPT_AR_ABRV !== null && $scope.departments.DEPT_EN_ABRV !== null && $scope.departments.COM_BRN_ID !== null) {
            if ($scope.departments.DEPT_ID === null || $scope.DEPT_ID === 0) {
                $scope.insert($scope.departments).then(function (results) {
                    $scope.clearEnity();
                    $scope.refreshData();
                    swal({
                        title: 'تم',
                        text: 'حفظ بيانات القسم بنجاح',
                        type: 'success',
                        timer: 1500,
                        showConfirmButton: false
                    });
                }, function (error) {
                    console.log(error.data.message);
                    swal('عفواً',
                        'حدث خطأ عند حفظ بيانات القسم',
                        'error');
                });
            } else {
                $scope.update($scope.departments).then(function (results) {
                    $scope.clearEnity();
                    $scope.refreshData();
                    swal({
                        title: 'تم',
                        text: 'تعديل بيانات القسم بنجاح',
                        type: 'success',
                        timer: 1500,
                        showConfirmButton: false
                    });
                }, function (error) {
                    console.log(error.data.message);
                    swal('عفواً',
                        'حدث خطأ عند تعديل بيانات القسم',
                        'error');
                });
            }
        }
    }
    $scope.dirEnity = function (entity) {
        $scope.departments = entity;
    }
    $scope.deleteEnity = function (entity) {
        swal({
            title: 'هل تريد حذف القسم؟',
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
                        text: "هذا القسم تم عليه عمليات مختلفة. لا تستطيع حذفه",
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
        //swal({
        //    title: 'هل تريد حذف القسم؟',
        //    text: "لن تستطيع عكس عملية الحذف مرة أخري",
        //    type: 'warning',
        //    showCancelButton: true,
        //    confirmButtonColor: '#3085d6',
        //    cancelButtonColor: '#d33',
        //    confirmButtonText: 'نعم',
        //    cancelButtonText: 'الغاء',
        //    confirmButtonClass: 'btn btn-success btn-lg',
        //    cancelButtonClass: 'btn btn-danger btn-lg',
        //    buttonsStyling: false
        //}).then(function () {
        //    $scope.delete(entity).then(function (results) {
        //        $scope.clearEnity();
        //        $scope.refreshData();
        //        swal({
        //            title: 'تم',
        //            text: 'الحذف بنجاح',
        //            type: 'success',
        //            timer: 1500,
        //            showConfirmButton: false
        //        });
        //    }, function (error) {
        //        console.log(error.data.message);
        //    });
        //}, function (dismiss) {
        //    // dismiss can be 'cancel', 'overlay',
        //    // 'close', and 'timer'
        //    if (dismiss === 'cancel') {
        //        swal({
        //            title: 'تم',
        //            text: 'الغاء عملية الحذف',
        //            type: 'error',
        //            timer: 1500,
        //            showConfirmButton: false
        //        });
        //    }
        //})
    }
    $scope.getAllOnLoad = function () {
        $q.all(
            [
                $scope.getdepartmentsList(),
                $scope.getdepartmentsTotalCount(),
                $scope.getAllCompanyBranchesList(),
                $scope.getlastCode()
            ]).then(function (allResponses) {

                var urlParams = $location.search();
                if (urlParams.foo != null && urlParams.foo != undefined && urlParams.foo != "") {
                    departmentsService.getById(parseInt(urlParams.foo)).then(function (results) {
                        $scope.departments = results.data;
                        $scope.dirEnity(results.data);
                    })

                }

            }, function (error) {
                //This will be called if $q.all finds any of the requests erroring.
                var abc = error;
                var def = 99;
            });
    }
    $scope.getdepartmentsTotalCount = function () {
        $scope.count().then(function (results) {
            var data = results.data;
            $scope.departmentsTotalCount = data;
            $scope.pagesCount = $scope.getPagesCount();
        }, function (error) {
            console.log(error.data.message);
        });
    }
    $scope.getdepartmentsList = function () {
        $scope.get($scope.pageNum, $scope.pageSize).then(function (results) {
            var data = results.data;
            $scope.departmentsList = data;

        }, function (error) {
            console.log(error.data.message);
        });
    }


    $scope.get = function (pageNum, pageSize) {
        return departmentsService.get(pageNum, pageSize);
    }
    $scope.count = function () {
        return departmentsService.count();
    }
    $scope.insert = function (entity) {
        return departmentsService.insert(entity);
    }
    $scope.update = function (entity) {
        return departmentsService.update(entity);
    }
    $scope.delete = function (entity) {
        return departmentsService.delete(entity);
    }
    $scope.departmentsPageLoad = function () {
        $scope.getAllOnLoad();
    }

    $scope.getAllCompanyBranchesList = function () {

        commonService.getAllCompanyBranches().then(function (response) {
            $scope.CompanyBranchesList = response.data;
        })
    }

    $scope.pageChanged = function () {
        $scope.getAllOnLoad();
        $log.log('Page changed to: ' + $scope.pageNum);
    };
    /////////////////////////////last code
    $scope.getlastCode = function () {
        var urlParams = $location.search();
        if (urlParams.foo != null && urlParams.foo != undefined && urlParams.foo != "") { } else {
            departmentsService.getLastCode().then(function (result) {
                $scope.departments.DEPT_CODE = parseInt(result.data) + 1;
            }, function (error) { });
        }
    }


    $scope.fnExcelReport44 = function () {

        //alert('test');

        var tab_text = "<table border='2px'><tr bgcolor='#87AFC6'>";
        var textRange; var j = 0;
        var tab = document.getElementById('sample-table-2'); // id of table

        for (j = 0; j < tab.rows.length; j++) {
            //tab_text = tab_text + tab.rows[j].innerHTML + "</tr>";
            //tab_text=tab_text+"</tr>";

            tab_text = tab_text + "<tr>";

            var row = tab.rows[j];

            //your code goes here, looping over every row.
            //cells are accessed as easy

            var cellLength = row.cells.length - 1;

            for (var y = 0; y < cellLength; y += 1) {
                var cell = row.cells[y];

                tab_text = tab_text + "<td>";
                tab_text = tab_text + cell.innerText;
                tab_text = tab_text + "</td>";
            }
     
            tab_text = tab_text + "</tr>";
        }

        tab_text = tab_text + "</table>";
        tab_text = tab_text.replace(/<A[^>]*>|<\/A>/g, "");//remove if u want links in your table
        tab_text = tab_text.replace(/<img[^>]*>/gi, ""); // remove if u want images in your table
        tab_text = tab_text.replace(/<input[^>]*>|<\/input>/gi, ""); // reomves input params

        var ua = window.navigator.userAgent;
        var msie = ua.indexOf("MSIE ");

        var sa = null;

        if (msie > 0 || !!navigator.userAgent.match(/Trident.*rv\:11\./))      // If Internet Explorer
        {
            var txtArea1 = $("#txtArea1");
            txtArea1.document.open("txt/html", "replace");
            txtArea1.document.write(tab_text);
            txtArea1.document.close();
            txtArea1.focus();
            sa = txtArea1.document.execCommand("SaveAs", true, "excel_document.xls");
        }
        else                 //other browser not tested on IE 11
            sa = window.open('data:application/vnd.ms-excel,' + encodeURIComponent(tab_text));

        return (sa);

    }

    $scope.fnExcelReport23 = function () {
        //var VALUE = "hello,gfhfghf,gjhgjhgj";

        var table_text = '';
        var tab = document.getElementById('sample-table-2'); // id of table

        table_text += 'Departments' + '\r\n';

        for (var j = 0; j < tab.rows.length; j++) {

            var row = tab.rows[j];

            var cellLength = row.cells.length - 1;

            for (var y = 0; y < cellLength; y += 1) {
                if (y == 0)
                    continue;
                var cell = row.cells[y];

                table_text = table_text + '"' + cell.innerText + '"';

                if (y < cellLength - 1) {
                    table_text += ',';
                }   
            }

            var len = tab.rows.length - 1;
            if (j < len) {
                table_text += '\r\n';
            }

        }

        var sa = window.open('data:text/csv;charset=utf-8,' + encodeURIComponent('\ufeff' + table_text));
        return (sa);

        ///var download = document.getElementById('download');
        ///download.setAttribute('href', 'data:text/csv;charset=utf-8,' + encodeURIComponent(table_text));
        ///download.setAttribute('download', 'filename.csv');
    }

    $scope.getTableData = function () {

        var table_text = '';
        var tab = document.getElementById('sample-table-2'); // id of table

        for (var j = 0; j < tab.rows.length; j++) {

            var row = tab.rows[j];

            var cellLength = row.cells.length - 1;

            for (var y = 0; y < cellLength; y += 1) {
                var cell = row.cells[y];

                table_text = table_text + '"' + cell.innerText + '"';

                if (y < cellLength - 1) {
                    table_text += ',';
                }
            }

            var len = tab.rows.length - 1;
            if (j < len) {
                table_text += '\r\n';
            }

        }

        return table_text;
    }

    $scope.fnExcelReport45 = function () {

        var text = $scope.getTableData();

        var pom = document.getElementById('download');
        var csvContent = "\ufeff" + text; //here we load our csv data 
        var blob = new Blob([csvContent], { type: 'text/csv;charset=utf-8;' });
        var url = URL.createObjectURL(blob);
        pom.href = url;
        pom.setAttribute('download', 'foo.csv');
        pom.click();

    }

    $scope.fnExcelReport55 = function () {

        //commonService.getExcelFile("sample-table-2", "Sheet 1", "Deparments", $scope.departmentsList);

        
        var columns = [
            { title: "ID", dataKey: "id" },
            { title: "Name", dataKey: "name" },
            { title: "Country", dataKey: "country" },

        ];
        var rows = [
            { "id": 1, "name": "شششش", "country": "Tanzania" },
            { "id": 2, "name": "Nelson", "country": "Kazakhstan" },
            { "id": 3, "name": "Garcia", "country": "Madagascar" },

        ];

        // Only pt supported (not mm or in)
        var doc = new jsPDF('p', 'pt');

        doc.addFileToVFS("~/Content/fonts/DroidKufi-Regular.ttf", "");
        doc.addFont('~/Content/fonts/DroidKufi-Regular.ttf', 'DroidKufi-Regular', 'normal');
        doc.setFont('DroidKufi-Regular');

        //doc.autoTable(columns, rows, {
        //    styles: { fillColor: [200, 200, 200] },
        //    columnStyles: {
        //        //id: { fillColor: [200, 200, 200] }
        //    },
        //    margin: { top: 60 },
        //    addPageContent: function (data) {
        //        doc.text("Header", 40, 30);
        //    }
        //});
        doc.text("<h3>ملف</h3>",100, 150);


        doc.save('table.pdf');

    }

    $scope.getTableHtml = function () {

        var table_text = "<table>";//

        table_text += "<tr><td colspan='6' align='center'>Deparments</td></tr>";

        var tab = document.getElementById('sample-table-2');

        for (var j = 0; j < tab.rows.length; j++) {

            table_text = table_text + "<tr>";

            var row = tab.rows[j];

            var cellLength = row.cells.length - 1;

            for (var y = 0; y < cellLength; y += 1) {
                var cell = row.cells[y];
                if (y == 0)
                    continue;

                var checkbox = $('#sample-table-2 tr').eq(0).find('td').eq(y).find("input").prop("checked");

                if (checkbox == false) {
                    continue;
                }

                table_text = table_text +"<td>" + cell.innerText + "</td>";

            }

            table_text = table_text + "</tr>";

        }

        table_text += "</table>";

        return table_text;
    }


    $scope.fnExcelReport = function () {

        console.log($scope.createArr($scope.departmentsList));

        pdfMake.fonts = {
            custom: {
                normal: 'DroidKufi-Regular.ttf'
            }
        }

        //var docDefinition = { content: 'This is an sample PDF printed with pdfMake' };

        //pdfMake.createPdf(docDefinition).download();

        var docDefinition = {
            content: 'This is an sample PDF printed with pdfMake الشمس <h1>test</h1>',
            defaultStyle: {
                font: 'custom'
            }
        };

        var docDefinition = {
            content: [
                {
                    //layout: 'lightHorizontalLines', // optional
                    layout: {
                        hLineWidth: function (i, node) {
                            if (i === 0 || i === node.table.body.length) {
                                return 0;
                            }
                            return (i === node.table.headerRows) ? 2 : 1;
                        },
                        vLineWidth: function (i) {
                            return 0;
                        },
                        hLineColor: function (i) {
                            return i === 1 ? 'black' : '#aaa';
                        },
                        paddingLeft: function (i) {
                            return i === 0 ? 0 : 8;
                        },
                        paddingRight: function (i, node) {
                            return (i === node.table.widths.length - 1) ? 0 : 8;
                        },
                        fillColor: function (i, node) {
                            return i % 2 == 0 ? 'white' : '#eee';
                        }
                    },
                    table: {
                        // headers are automatically repeated if the table spans over multiple pages
                        // you can declare how many rows should be treated as headers
                        headerRows: 1,
                        //widths: ['*', 'auto', 100, '*'],

                        body: $scope.createArr($scope.departmentsList)
                    }
                }
            ],
            defaultStyle: {
                font: 'custom'
            }
        };


        pdfMake.createPdf(docDefinition).download();


    }

    $scope.createArr = function (list){

        //var selectColumns = {};
        //var columnsHeader = "<tr>";

        //var tableId = "sample-table-2";
        //var table = document.getElementById("sample-table-2");
        //var row = table.rows[0];
        //var header_row = table.rows[1];
        //var cellLength = row.cells.length - 1;

        //for (var y = 0; y < cellLength; y += 1) {

        //    var first_row = $('#' + tableId + ' tr').eq(0).find('td').eq(y).find("input");

        //    var checkbox = first_row.prop("checked");

        //    var colName = first_row.attr("data-colName");

        //    selectColumns[colName] = checkbox;

        //    if (checkbox === true) {
        //        columnsHeader += "<td>" + header_row.cells[y].innerText + "</td>";
        //    }
        //}

        var arr = [];

        for (var i = 0; i < list.length; i++) {
            arr[i] = [];
        }

        for (var i = 0; i < list.length; i++) {

            var obj = list[i];

            //for (var j = 0; j < 4; j++) {

            //    arr[i][j] = i * j;
            //}

            var j = 0;
            for (var key in obj) {
                if (obj.hasOwnProperty(key)) {
                    if (j >= 7) {
                        continue;
                    }

                    arr[i][j] = obj[key];
                    j++;                 
                }
            }
        }
        return arr;
    }

}]);