'use strict';
app.controller('technicalSupportController', ['$scope', '$location', '$log', '$q', function ($scope, $location, $log, $q) {



    $scope.getAllOnLoad = function () {
        $q.all([
        ]).then(function (allResponses) {
        }, function (error) {
        });
    }

    $scope.pageChanged = function () {
        $scope.getAllOnLoad();
        $log.log('Page changed to: ' + $scope.pageNum);
    };
}]);