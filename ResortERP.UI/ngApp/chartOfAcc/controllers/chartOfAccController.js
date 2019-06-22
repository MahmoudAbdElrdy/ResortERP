'use strict';
app.controller('chartOfAccController', ['$scope', '$location', '$log', '$q', 'chartOfAccService', 'commonService', '$window', function ($scope, $location, $log, $q, chartOfAccService, commonService, $window) {

    // Region var
    $scope.accountsGuideList = [];
    $scope.flagDbClick = false; 
    // End Region

    // Region Method
    $scope.clearEnity = function () {
        $scope.accountsGuideList = [];
    };

    $scope.chartOfAccPageLoad = function () {
        $scope.getAllOnLoad();
    }


    $("#addBtnNewAcc").click(function () {
        $window.location.href = "#/Accounts";
    })

    //$("#accTree").dblclick(function () {
    //    $scope.flagDbClick = true;
    //});

    //$('#accTree').on("dblclick",
    //    function () {
    //        $scope.flagDbClick = true;
    //        //if ($scope.flagDbClick == true) {
    //        //    $scope.flagDbClick = false;
    //        //    $scope.redToView(data.node.id, data.node.parent);


    //    })

    $scope.GetAllAccChild = function () {
       
        chartOfAccService.getToTree().then(function (results) {
            var data = results.data;
            $scope.accountsGuideList = data;
            $.jstree.defaults.core.themes.variant = "large";


            $('#accTree')
                //.on("dblclick.jstree",
                //function () {
                //    alert("db")
                //    $scope.flagDbClick = true;
                //    //if ($scope.flagDbClick == true) {
                //    //    $scope.flagDbClick = false;
                //    //    $scope.redToView(data.node.id, data.node.parent);


                //})
                //.on('click', '.jstree-anchor', function (e) {
                //    alert("db")
                //    if ($scope.flagDbClick == false)
                //         $('#accTree').jstree(true).toggle_node(e.target);
                //})
                .on("changed.jstree",
                function (evt, data) {
                    //var idVal = $('#accTree').jstree(true).get_node(this.id).id;
                    //alert(data.node.id);
                    $("#addBtn").remove();
                    //alert(data.node.data.data);
                    if (data.node.data.data == 1) {

                        $("#" + data.node.id + " >a").append("<span id='addBtn' class='btn btn-primary input-group-addon col-sm-1' style='display:inline-block'><span class='glyphicon glyphicon-plus'></span></span>");
                        $("#addBtn").click(function () {
                            $window.location.href = "#/Accounts?new=" + data.node.id;
                            //alert(data.node.id);
                        })
                    }

                    //alert();
                    //if ($scope.flagDbClick == true) {                        
                    //    $scope.redToView(data.node.id, data.node.parent);
                    //    $scope.flagDbClick = false;
                    //}
                    $("#accTree").dblclick(function () {
                        $scope.redToView(data.node.id, data.node.parent);
                    });

                })
                .on('ready.jstree', function () {
                    $('#accTree').jstree('open_all');
                    $("#col").show();
                    $("#exp").hide();
                    $('#accTree').slimScroll({
                        height: '800px'
                    });

                    var $tree = $(this);
                    $($tree.jstree().get_json($tree, {
                        flat: true
                    })).each(function () {
                        var idVal = $('#accTree').jstree(true).get_node(this.id).id;
                        var level = $('#accTree').jstree(true).get_node(this.id).data.data;
                        if (level == 2) {
                            $("#" + idVal + " >a").css("color", "#832e32");
                            if ($("#" + idVal + " >a >i").length) {
                                $("#" + idVal + " >a >i").remove();
                                //alert();
                            }
                            if ($("#" + idVal + " >a >img").length) {
                            } else {
                                $("#" + idVal + " >a").prepend("<img id='imgAlert' src='../../../../Content/bower_components/vakata-jstree-0097fab/dist/themes/default/folder.png' width='20' style='color:#832e32;margin:0 0 0 6px' />")
                             
                            }
                           // $("#" + idVal + " >a").children()[0].remove();
                        }
                    });           
                })
                .on("open_node.jstree", function (e, data) {
                    var idVal = data.node.id;
                    if (data.node.data.data == 2) {
                        $("#" + idVal + " >a").css("color", "#832e32");
                        //$("#" + idVal + " >a").children()[0].remove();
                        if ($("#" + idVal + " >a >i").length) {
                            $("#" + idVal + " >a >i").remove();
                            //alert();
                        }
                        if ($("#" + idVal + " >a >img").length) {
                        } else {
                            $("#" + idVal + " >a").prepend("<img id='imgAlert' src='../../../../Content/bower_components/vakata-jstree-0097fab/dist/themes/default/folder.png' width='20' style='color:#832e32;margin:0 0 0 6px' />")

                        }
                    }

                    if (data.node.children_d.length > 0) {
                        $.each(data.node.children_d, function (index, value) {                   
                            var level1 = $('#accTree').jstree().get_node(value).data.data;
                            if (level1 == 2) {
                                var idVal1 = $('#accTree').jstree().get_node(value).id;
                                $("#" + idVal1 + " >a").css("color", "#832e32");
                              //  $("#" + idVal1 + " >a").children()[0].remove();
                                if ($("#" + idVal1 + " >a >i").length) {
                                    $("#" + idVal1 + " >a >i").remove();
                                    //alert();
                                }
                                if ($("#" + idVal1 + " >a >img").length) {
                                   
                                } else {
                                    $("#" + idVal1 + " >a").prepend("<img id='imgAlert' src='../../../../Content/bower_components/vakata-jstree-0097fab/dist/themes/default/folder.png' width='20' style='color:#832e32;margin:0 0 0 6px' />")

                                }                            }
                        })
                      
                    }
                })
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

            console.log(error.data.message);
        });
    }



    $scope.treeOptions = {
        nodeChildren: "children",
        dirSelectable: true,
        injectClasses: {
            ul: "a1",
            li: "a2",
            liSelected: "a7",
            iExpanded: "a3",
            iCollapsed: "a4",
            iLeaf: "a5",
            label: "a6",
            labelSelected: "a8"
        }
    }


    $scope.getAllOnLoad = function () {
        $q.all(
            [
                $scope.GetAllAccChild(),
                $scope.searchTree(),
            ]).then(function (allResponses) {
                $scope.clearEnity();
            }, function (error) {
                var abc = error;
                var def = 99;
            });
    }

    $scope.redToView = function (id, parent) {

    $window.location.href = "#/Accounts?foo=" + id;

    }

    $scope.colapse = function () {
        $('#accTree').jstree('close_all');
    }

    $scope.expand = function () {
        $('#accTree').jstree('open_all');
    }



    //$(function () {
    $scope.searchTree = function () {
        $('#accTree').on('redraw.jstree', function () {
            $('#accTree').jstree('open_all');
            $("#col").show();
            $("#exp").hide();
            $('#accTree').slimScroll({
                height: '800px'
            });

            var $tree = $(this);
            $($tree.jstree().get_json($tree, {
                flat: true
            })).each(function () {
                var idVal = $('#accTree').jstree(true).get_node(this.id).id;
                var level = $('#accTree').jstree(true).get_node(this.id).data.data;
                if (level == 2) {
                    $("#" + idVal + " >a").css("color", "#832e32");
                    //  $("#" + idVal + " >a").children()[0].remove();
                    if ($("#" + idVal + " >a >i").length) {
                        $("#" + idVal + " >a >i").remove();
                        //alert();
                    }
                    if ($("#" + idVal + " >a >img").length) {

                    } else {
                        $("#" + idVal + " >a").prepend("<img id='imgAlert' src='../../../../Content/bower_components/vakata-jstree-0097fab/dist/themes/default/folder.png' width='20' style='color:#832e32;margin:0 0 0 6px' />")

                    }
                }
            });
        })
        $('#accTree').on('clear_search.jstree', function () {
            //  alert();
            $('#accTree').jstree(true).redraw(true);

        })
        var to = false;
        $('#accTree_q').keyup(function () {
            if (to) {
                clearTimeout(to);
                // $('#accTree').jstree('open_all');
                //$('#accTree').jstree().destroy();



            }

            to = setTimeout(function () {
                var v = $('#accTree_q').val();
                $('#accTree').jstree(true).search(v);


            }, 250);
        });

        $("#col").click(function () {
            $("#col").hide();
            $("#exp").show();
            if ($('#accTree li.jstree-open').length) {
                $("#accTree").jstree('close_all');
            }

        })

        $("#exp").click(function () {
            $("#col").show();
            $("#exp").hide();
            if ($('#accTree li.jstree-closed').length) {
                $('#accTree').jstree('open_all');
            }
        })
    }



    //});
    // End Region
}])

