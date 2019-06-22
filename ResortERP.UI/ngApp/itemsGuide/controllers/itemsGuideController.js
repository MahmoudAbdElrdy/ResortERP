'use strict';
app.controller('itemsGuideController', ['$scope', '$location', '$log', '$q', 'itemsGuideService', 'itemsGroupsService', 'commonService', 'itemsService', '$window', function ($scope, $location, $log, $q, itemsGuideService, itemsGroupsService, commonService, itemsService, $window) {

    // Region var
    $scope.isCol = true;
    $scope.itemsGroupList = [];
    $scope.pattern = {};
    // End Region

    // Region Method
    $scope.clearEnity = function () {
        $scope.itemsGroupList = [];
    };

    $scope.itemsGuidePageLoad = function () {
        $scope.getAllOnLoad();
    }

    $("#addBtnNewAcc").click(function () {
        $window.location.href = "#/items";
    })

    //$scope.GetAllItemsGroup = function () {
    //    itemsGroupsService.getAllMainItemGroups($scope.pageNum, $scope.pageSize).then(function (response) {
    //        $scope.itemsGroupList = response.data
    //    }, function (error) {
    //        console.log(error.data.message);
    //    });
    //}
    //$.jstree.defaults.core.themes.variant = "large";
    $scope.GetAllItemsGroup = function () {
        itemsGuideService.get().then(function (results) {
            $.jstree.defaults.core.themes.variant = "large";
            var data = results.data;
            $scope.itemsGroupList = data;
            $('#itemsTree')
                .on('ready.jstree', function () {
                    //alert();
                    $('#itemsTree').jstree('open_all');
                    $("#col").show();
                    $('#itemsTree').slimScroll({
                        height: '800px'
                    });
                })
                .on("changed.jstree",
                function (evt, data) {
                    $("#addBtn").remove();
                    if (data.node.data == null) {
                        data.node.data = {};
                        data.node.data.data = 0;
                    }
                    if (data.node.parent != "#" && data.node.data.data != 10) {
                        $("#" + data.node.id + " >a").append("<span id='addBtn' class='btn btn-primary input-group-addon col-sm-1' style='display:inline-block'><span class='glyphicon glyphicon-plus'></span></span>");
                        $("#addBtn").click(function () {
                            //var nodeP = $('#itemsTree').jstree(true).get_node(this.parent, true);
                            $window.location.href = "#/items?new=" + data.node.id;
                        })
                    }

                    $("#itemsTree").dblclick(function () {
                        $scope.redToView(data.node.id, data.node.parent, data.node.data.data);
                    });
                   
                }
            ).jstree({

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


    //$scope.treeOptions = {
    //    nodeChildren: "children",
    //    dirSelectable: true,
    //    injectClasses: {
    //        ul: "a1",
    //        li: "a2",
    //        liSelected: "a7",
    //        iExpanded: "a3",
    //        iCollapsed: "a4",
    //        iLeaf: "a5",
    //        label: "a6",
    //        labelSelected: "a8"
    //    }
    //}

    //function Filtering($scope) {
    //    $scope.treedata = createSubTree(3, 4, "");
    //    $scope.predicate = "Node 1";
    //    $scope.comparator = false;
    //}

    //function Sorting($scope) {
    //    $scope.before = '{{predicate}}';

    //    $scope.treedata = createSubTree(3, 12, "");
    //    $scope.reverse = true;
    //    $scope.orderby = 'label';

    //    $scope.byName = function () { $scope.orderby = 'Name' };

    //}

    $scope.getAllOnLoad = function () {
        $q.all(
            [
                $scope.GetAllItemsGroup(),
                $scope.searchTree()

            ]).then(function (allResponses) {
                $scope.clearEnity();
            }, function (error) {
                var abc = error;
                var def = 99;
            });
    }

    $scope.redToView = function (id, parent,data) {

        if (parent[0] != "#"&& data!=10) {

            $window.location.href = "#/items?foo="+id;
            //$location.path('/items').search({ foo: id })
        } else {    
            $window.location.href = "#/itemsgroups?foo=" + id;
           // $window.location.replace("#/itemsgroups?foo=" + id);

        }
    }

    $scope.colapse = function () {
        alert("col");
        $('#itemsTree').jstree('close_all');
    }

    $scope.expand = function () {
        alert("ex");
        $('#itemsTree').jstree('open_all');
    }

    $scope.searchTree = function () {
        var to = false;
        $('#itemsTree_q').keyup(function () {
            if (to) { clearTimeout(to); }
            to = setTimeout(function () {
                var v = $('#itemsTree_q').val();
                $('#itemsTree').jstree(true).search(v);
            }, 250);
        });

        $("#col").click(function () {
            $("#col").hide();
            $("#exp").show();
            if ($('#itemsTree li.jstree-open').length) {
                $("#itemsTree").jstree('close_all');
            }
        })

        $("#exp").click(function () {
            $("#col").show();
            $("#exp").hide();
            if ($('#itemsTree li.jstree-closed').length) {
                $('#itemsTree').jstree('open_all');
            }
        })
    }

    // End Region
}])




