'use strict';
app.controller('storesGuideController', ['$scope', '$location', '$log', '$q', 'storesGuideService', 'commonService', '$window', function ($scope, $location, $log, $q, storesGuideService, commonService, $window) {
    // Region var
    $scope.isCol = true;
    $scope.storesGroupList = [];
    $scope.pattern = {};
    // End Region

    // Region Method
    $scope.clearEnity = function () {
        $scope.storesGroupList = [];
    };

    $scope.storesGuidePageLoad = function () {
        $scope.getAllOnLoad();
    }

    $("#addBtnNewAcc").click(function () {
        $window.location.href = "#/companystores";
    })

    $scope.GetAllstoresGroup = function () {
        storesGuideService.get().then(function (results) {
            debugger
          //  $.jstree.defaults.core.themes.variant = "large";
            var data = results.data;
            //$scope.storesGroupList = data;
            $('#storesTree')
                .on('ready.jstree', function () {
                    //alert();
                    //$('#storesTree').jstree('open_all');
                    $("#col").show();
                    $('#storesTree').slimScroll({
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
                           //var nodeP = $('#storesTree').jstree(true).get_node(this.parent, true);
                           $window.location.href = "#/companystores?new=" + data.node.id;
                        })
                  //  //}

                    //$("#storesTree").dblclick(function () {
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
                $scope.GetAllstoresGroup(),
                $scope.searchTree()

            ]).then(function (allResponses) {
                $scope.clearEnity();
            }, function (error) {
                var abc = error;
                var def = 99;
            });
    }

    $scope.redToView = function (id, parent,data) {
       $window.location.href = "#/companystores?foo="+id;
    }

    $scope.searchTree = function () {
        var to = false;
        $('#storesTree_q').keyup(function () {
            if (to) { clearTimeout(to); }
            to = setTimeout(function () {
                var v = $('#storesTree_q').val();
                $('#storesTree').jstree(true).search(v);
            }, 250);
        });

        $("#col").click(function () {
            $("#col").hide();
            $("#exp").show();
            if ($('#storesTree li.jstree-open').length) {
                $("#storesTree").jstree('close_all');
            }
        })

        $("#exp").click(function () {
            $("#col").show();
            $("#exp").hide();
            if ($('#storesTree li.jstree-closed').length) {
                $('#storesTree').jstree('open_all');
            }
        })
    }

    // End Region
}])




