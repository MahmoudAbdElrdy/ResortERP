'use strict';
app.factory('exportService', ['$http', 'sharedService', 'localStorageService', '$filter', function ($http, sharedService, localStorageService, $filter) {



    var serviceBase = sharedService.getBaseUrl();
    var exportServiceFactory = {};


    var _tableToExcel = (function () {
        var uri = 'data:application/vnd.ms-excel;base64,'
            , template = '<html xmlns:o="urn:schemas-microsoft-com:office:office" xmlns:x="urn:schemas-microsoft-com:office:excel" xmlns="http://www.w3.org/TR/REC-html40">' +
                '<head><!--[if gte mso 9]><xml><x:ExcelWorkbook><x:ExcelWorksheets><x:ExcelWorksheet><x:Name>{worksheet}</x:Name><x:WorksheetOptions>' +
                '<x:DisplayGridlines/></x:WorksheetOptions></x:ExcelWorksheet></x:ExcelWorksheets></x:ExcelWorkbook></xml><![endif]-->' +
                '<meta http-equiv="content-type" content="text/plain; charset=UTF-8"/></head><body><table>{table}</table></body></html>'
            , base64 = function (s) { return window.btoa(unescape(encodeURIComponent(s))) }
            , format = function (s, c) { return s.replace(/{(\w+)}/g, function (m, p) { return c[p]; }) }
        return function (table, name, header, data) {
            if (!table.nodeType) table = document.getElementById(table)
            var ctx = { worksheet: name || 'Worksheet', table: _getTableHtml(table, header, data) }
            window.location.href = uri + base64(format(template, ctx))
        }
    })()


    var _getTableHtml = function (table, headerName, list) {


        var selectColumns = {};
        var columnsHeader = "<tr>";

        var tableId = angular.element(table).attr('id');
        var row = table.rows[0];
        var header_row = table.rows[1];
        var cellLength = row.cells.length - 1;

        for (var y = 0; y < cellLength; y += 1) {

            var first_row = $('#' + tableId + ' tr').eq(0).find('td').eq(y).find("input");

            var checkbox = first_row.prop("checked");

            var colName = first_row.attr("data-colName");

            selectColumns[colName] = checkbox;

            if (checkbox === true) {
                columnsHeader += "<td>" + header_row.cells[y].innerText + "</td>";
            }
        }
        columnsHeader += "</tr>";

        var table_text = "<table>";

        table_text += "<tr><td colspan='#colspan#' align='center'>" + headerName + "</td></tr>";
        table_text += columnsHeader;

        var index = 0;

        for (var j = 0; j < list.length; j++) {

            table_text = table_text + "<tr>";

            var obj = list[j];

            for (var key in obj) {
                if (obj.hasOwnProperty(key)) {

                    if (selectColumns[key] && selectColumns[key] == true) {
                        table_text = table_text + "<td>" + obj[key] + "</td>";
                        if (j == 0) {
                            index++;
                        }
                    }
                }
            }


            table_text = table_text + "</tr>";

        }

        table_text += "</table>";
        table_text = table_text.replace("#colspan#", index);

        return table_text;
    }


    var _getExcelFile = function (tableId, sheetName, headerName, list) {

        _tableToExcel(tableId, sheetName, headerName, list);
    }


    exportServiceFactory.tableToExcel = _tableToExcel;
    exportServiceFactory.getTableHtml = _getTableHtml;
    exportServiceFactory.getExcelFile = _getExcelFile;

    return exportServiceFactory;

}]);