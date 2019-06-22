'use strict'
app.factory('addressService', ['$http', 'sharedService', function ($http, sharedService) {

    var serviceBase = sharedService.getBaseUrl();
    var addressFactory = {};

    var _getNations = function (pageNum, pageSize) {
        var myUrl = serviceBase + 'address/getNations?pageNum=' + pageNum + '&pagesize=' + pageSize;
        return $http.get(myUrl).then(function (result) {
            return result;
        }
            , function (error) {
            });
    }


    var _countNation = function () {
        var myUrl = serviceBase + 'address/countNation';
        return $http.get(myUrl).then(function (result) {
            return result;
        }, function (error) { });
    }


    var _addNation = function (entity) {
        var myUrl = serviceBase + 'address/addNation';
        var data = {};
        data = entity;
        return $http({
            method: 'POST',
            url: myUrl,
            data: JSON.stringify(data)
        }).then(function (result) {
            return result;
        }, function (error) { });
    }


    var _updateNation = function (entity) {
        var myUrl = serviceBase + 'address/updateNation';
        var data = {};
        data = entity;
        return $http({
            method: 'POST',
            url: myUrl,
            data: JSON.stringify(data)
        }).then(function (result) {
            return result;
        }, function (error) { });
    }


    var _deleteNation = function (entity) {
        var myUrl = serviceBase + 'address/deleteNation';
        var data = {};
        data = entity;
        return $http({
            method: 'POST',
            url: myUrl,
            data: JSON.stringify(data)
        }).then(function (result) {
            return result;
        }, function (error) { });
    }


    var _getNationByID = function (nationID) {
        var myUrl = serviceBase + 'address/getNationByID?nationID=' + nationID;
        return $http.get(myUrl).then(function (result) {
            return result;
        }
            , function (error) {
            });
    }












    var _getGovern = function (pageNum, pageSize) {
        debugger;
        var myUrl = serviceBase + 'address/getGoverns?pageNum=' + pageNum + '&pagesize=' + pageSize;
        return $http.get(myUrl).then(function (result) {
            return result;
        }, function (error) { })
    }


    var _countGovern = function () {
        var myUrl = serviceBase + 'address/countGovern';
        return $http.get(myUrl).then(function (result) {
            return result;
        }, function (error) { });
    }


    var _addGovern = function (entity) {
        var myUrl = serviceBase + 'address/addGovern';
        var data = {};
        data = entity;
        return $http({
            method: 'POST',
            url: myUrl,
            data: JSON.stringify(data)
        }).then(function (result) {
            return result;
        }, function (error) { });
    }


    var _updateGovern = function (entity) {
        var myUrl = serviceBase + 'address/updateGovern';
        var data = {};
        data = entity;
        return $http({
            method: 'POST',
            url: myUrl,
            data: JSON.stringify(data)
        }).then(function (result) {
            return result;
        }, function (error) { });
    }


    var _deleteGovern = function (entity) {
        var myUrl = serviceBase + 'address/deleteGovern';
        var data = {};
        data = entity;
        return $http({
            method: 'POST',
            url: myUrl,
            data: JSON.stringify(data)
        }).then(function (result) {
            return result;
        }, function (error) { });
    }



    var _getGovernByID = function (govID) {
        var myUrl = serviceBase + 'address/getGovByID?govID=' + govID;
        return $http.get(myUrl).then(function (result) {
            return result;
        }
            , function (error) {
            });
    }








    var _getTown = function (pageNum, pageSize) {
        debugger;
        var myUrl = serviceBase + 'address/getTowns?pageNum=' + pageNum + '&pagesize=' + pageSize;
        return $http.get(myUrl).then(function (result) {
            return result;
        }, function (error) { })
    }


    var _countTown = function () {
        var myUrl = serviceBase + 'address/countTown';
        return $http.get(myUrl).then(function (result) {
            return result;
        }, function (error) { });
    }


    var _addTown = function (entity) {
        var myUrl = serviceBase + 'address/addTown';
        var data = {};
        data = entity;
        return $http({
            method: 'POST',
            url: myUrl,
            data: JSON.stringify(data)
        }).then(function (result) {
            return result;
        }, function (error) { });
    }


    var _updateTown = function (entity) {
        var myUrl = serviceBase + 'address/updateTown';
        var data = {};
        data = entity;
        return $http({
            method: 'POST',
            url: myUrl,
            data: JSON.stringify(data)
        }).then(function (result) {
            return result;
        }, function (error) { });
    }


    var _deleteTown = function (entity) {
        var myUrl = serviceBase + 'address/deleteTown';
        var data = {};
        data = entity;
        return $http({
            method: 'POST',
            url: myUrl,
            data: JSON.stringify(data)
        }).then(function (result) {
            return result;
        }, function (error) { });
    }


    var _getTownByID = function (townID) {
        var myUrl = serviceBase + 'address/getTownByID?townID=' + townID;
        return $http.get(myUrl).then(function (result) {
            return result;
        }
            , function (error) {
            });
    }









    var _getVillage = function (pageNum, pageSize) {
        var myUrl = serviceBase + 'address/getVillages?pageNum=' + pageNum + '&pagesize=' + pageSize;
        return $http.get(myUrl).then(function (result) {
            return result;
        }, function (error) { })
    }


    var _countVillage = function () {
        var myUrl = serviceBase + 'address/countVillage';
        return $http.get(myUrl).then(function (result) {
            return result;
        }, function (error) { });
    }


    var _addVillage = function (entity) {
        var myUrl = serviceBase + 'address/addVillage';
        var data = {};
        data = entity;
        return $http({
            method: 'POST',
            url: myUrl,
            data: JSON.stringify(data)
        }).then(function (result) {
            return result;
        }, function (error) { });
    }


    var _updateVillage = function (entity) {
        debugger;
        var myUrl = serviceBase + 'address/updateVillage';
        var data = {};
        data = entity;
        return $http({
            method: 'POST',
            url: myUrl,
            data: JSON.stringify(data)
        }).then(function (result) {
            return result;
        }, function (error) { });
    }


    var _deleteVillage = function (entity) {
        debugger;
        var myUrl = serviceBase + 'address/deleteVillage';
        var data = {};
        data = entity;
        return $http({
            method: 'POST',
            url: myUrl,
            data: JSON.stringify(data)
        }).then(function (result) {
            return result;
        }, function (error) { });
    }



    var _getVillageByID = function (villID) {
        var myUrl = serviceBase + 'address/getVillByID?villID=' + villID;
        return $http.get(myUrl).then(function (result) {
            return result;
        }
            , function (error) {
            });
    }




    addressFactory.getNations = _getNations;
    addressFactory.countNation = _countNation;
    addressFactory.addNation = _addNation;
    addressFactory.updateNation = _updateNation;
    addressFactory.deleteNation = _deleteNation;
    addressFactory.getNationByID = _getNationByID;

    addressFactory.getGovern = _getGovern;
    addressFactory.countGovern = _countGovern;
    addressFactory.addGovern = _addGovern;
    addressFactory.updateGovern = _updateGovern;
    addressFactory.deleteGovern = _deleteGovern;
    addressFactory.getGovernByID = _getGovernByID;

    addressFactory.getTown = _getTown;
    addressFactory.countTown = _countTown;
    addressFactory.addTown = _addTown;
    addressFactory.updateTown = _updateTown;
    addressFactory.deleteTown = _deleteTown;
    addressFactory.getTownByID = _getTownByID;

    addressFactory.getVillage = _getVillage;
    addressFactory.countVillage = _countVillage;
    addressFactory.addVillage = _addVillage;
    addressFactory.updateVillage = _updateVillage;
    addressFactory.deleteVillage = _deleteVillage;
    addressFactory.getVillageByID = _getVillageByID;

    return addressFactory;
}]);