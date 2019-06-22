'use strict';
app.factory('sharedService', function ($http) {
    /*var serviceBase = 'http://localhost:55515/';*//* http://localhost:58259*/

    var serviceBase = 'http://localhost:55515/';
    return {

        getBaseUrl: function () {
            return serviceBase;
        },

        getUserType: function () {
            var myUrl = serviceBase + 'User/GetUserType';
            return $http.get(myUrl).then(function (results) {
                return results;
            });
        }



    }
})