'use strict';
app.controller('costCentersGuideController', ['$scope', '$location', '$log', '$q', 'costCentersGuideService', 'commonService', '$window', function ($scope, $location, $log, $q, costCentersGuideService, commonService, $window) {
    // Region var
    $scope.isCol = true;
    $scope.costCentersGroupList = [];
    $scope.pattern = {};
    // End Region

    // Region Method
    $scope.clearEnity = function () {
        $scope.costCentersGroupList = [];
    };

    $scope.costCentersGuidePageLoad = function () {
        $scope.getAllOnLoad();
    }

    $("#addBtnNewAcc").click(function () {
        $window.location.href = "#/CostCenters";
    })

    $scope.GetAllcostCentersGroup = function () {
        costCentersGuideService.get().then(function (results) {
            debugger
          //  $.jstree.defaults.core.themes.variant = "large";
            var data = results.data;
            //$scope.costCentersGroupList = data;
            $('#costCentersTree')
                .on('ready.jstree', function () {
                    //alert();
                    //$('#costCentersTree').jstree('open_all');
                    $("#col").show();
                    $('#costCentersTree').slimScroll({
                        height: '800px'
                    });
                })
                .on("changed.jstree",
                function (evt, data) {
                  //  alert("change")
                  //  debugger
                    $("#addBtn").remove();
                  //  if (data.node.data == null) {
                  //      data.node.data = {};
                  //      data.node.data.data = 0;
                  //  }
                  ////  if (data.node.parent != "#") {
                       $("#" + data.node.id + " >a").append("<span id='addBtn' class='btn btn-primary input-group-addon col-sm-1' style='display:inline-block'><span class='glyphicon glyphicon-plus'></span></span>");
                       $("#addBtn").click(function () {
                           //var nodeP = $('#costCentersTree').jstree(true).get_node(this.parent, true);
                           $window.location.href = "#/CostCenters?new=" + data.node.id;
                        })
                  //  //}

                    //$("#costCentersTree").dblclick(function () {
                    //    $scope.redToView(data.node.id, data.node.parent, data.node.data.data);
                    //});
                   
                }
            )
                .jstree({

                    'core': {
                        'data': data,
                        'dblclick_toggle': false,
                    },
                    "plugins": ["search", "sort"],
                    "search": { "show_only_matches": true }
                }
            );



           // $scope.dataForTheTree = data;
        }, function (error) {
            alert(error.data.message)
            console.log(error.data.message);
            });

      
    }


    $scope.getAllOnLoad = function () {
        $q.all(
            [
                $scope.GetAllcostCentersGroup(),
                $scope.searchTree()

            ]).then(function (allResponses) {
                $scope.clearEnity();
            }, function (error) {
                var abc = error;
                var def = 99;
            });
    }

    $scope.redToView = function (id, parent,data) {
       $window.location.href = "#/CostCenters?foo="+id;
    }

    $scope.searchTree = function () {
        var to = false;
        $('#costCentersTree_q').keyup(function () {
            if (to) { clearTimeout(to); }
            to = setTimeout(function () {
                var v = $('#costCentersTree_q').val();
                $('#costCentersTree').jstree(true).search(v);
            }, 250);
        });

        $("#col").click(function () {
            $("#col").hide();
            $("#exp").show();
            if ($('#costCentersTree li.jstree-open').length) {
                $("#costCentersTree").jstree('close_all');
            }
        })

        $("#exp").click(function () {
            $("#col").show();
            $("#exp").hide();
            if ($('#costCentersTree li.jstree-closed').length) {
                $('#costCentersTree').jstree('open_all');
            }
        })
    }

    // End Region
}])




