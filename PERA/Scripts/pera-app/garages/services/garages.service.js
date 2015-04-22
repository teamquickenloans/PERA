/**
* Garages
* @namespace pera.garages.services
*/
(function () {
    'use strict';

    angular
      .module('pera.garages.services')
      .factory('Garages', Garages);

    Garages.$inject = ['$http', '$rootScope', '$q', 'Snackbar'];

    /**
    * @namespace Garages
    * @returns {Factory}
    */
    function Garages($http, $rootScope, $q, Snackbar) {   //this is just linking up the functions to variable names and returning them
        
        var vm = this;
        vm.garages = [];
        vm.garage;
        var last_request_failed = false;
        var last_garage_failed = false;
        var allPromise = undefined;
        var garagePromise = undefined;
        vm.costs = []; //the total cost of leased spaces for each garage
        vm.totalCost = 0; //the total cost for all garages
        vm.totalCapacity = 0;
        vm.totalLeasedSpaces = 0;
        vm.totalTeamMemberSpaces = 0;
        vm.totalRequiredBufferSize = 0;
        vm.averageCostPerSpace = 0;
        vm.averageTransientSalePrice = 0;
        
        var Garages = {
            all: all,
            create: create,
            update: update,
            get: get,
            share: share,
            costs: getCosts,
            totalCost: getTotalCost,
            totalCapacity: getTotalCapacity,
            totalLeasedSpaces: getTotalLeasedSpaces,
            totalTeamMemberSpaces: getTotalTeamMemberSpaces,
            totalRequiredBufferSize: getTotalRequiredBufferSize,
            averageCostPerSpace: getAverageCostPerSpace,
            averageTransientSalePrice: getAverageTransientSalePrice
        };

        return Garages;

        ////////////////////

        /**
        * @name all
        * @desc Get all Garages
        * @returns {Promise}
        * @memberOf pera.garages.services.Garages
        */
        function all() {
            if (!allPromise || last_request_failed) {
                //console.log("querying database");
                allPromise = $http.get('/api/garages/getgarages/');
                allPromise.then(garagesSuccess, garagesError);
            }
            return allPromise;
        }

        /**
        * @name get
        * @desc Get one Garage
        * @returns {Promise}
        * @memberOf pera.garages.services.Garages
        */
        function get(garageID) 
        {
            if (!garagePromise[garageID] || last_garage_failed[garageID]) {
                garagePromise[garageID] = $http.get('/api/garages/get/' + garageID);
                //console.log("getting one garage");
                garagePromise[garageID].then(garageSuccess, garageError);
            }
            return garagePromise;
        }

        function garageSuccess(data, status, headers, config, response) {
            last_garage_failed = false;
            vm.garage = data.data;
            //console.log("service success ", vm.garage);
            return vm.garage;
        }

        function garageError(data, status, headers, config, response) {
            last_garage_failed = true;
            //Snackbar.error("Error retrieving garages");
            return $q.reject(response);
        }


        function garagesSuccess(data, status, headers, config, response) {
            last_request_failed = false;
            vm.garages = data.data;
            //console.log("service success ", vm.garages);
            calculateTotals();
            return vm.garages;
            //share();
        }

        function garagesError(data, status, headers, config, response) {
            last_request_failed = true;
            //Snackbar.error("Error retrieving garages");
            return $q.reject(response);
        }
        function calculateTotals() {
            for (var i = 0; i < vm.garages.length; i++) {
                var temp = vm.garages[i].numberOfLeasedSpaces * vm.garages[i].spaceCost;
                vm.garages[i].cost = temp;
                vm.totalCost += temp;
                vm.totalCapacity += vm.garages[i].capacity;
                vm.totalLeasedSpaces += vm.garages[i].numberOfLeasedSpaces;
                vm.totalRequiredBufferSize += vm.garages[i].minimumNumberOfBufferSpaces;
                vm.totalTeamMemberSpaces += vm.garages[i].numberOfTeamMemberSpaces;

                vm.averageCostPerSpace += vm.garages[i].spaceCost;
                vm.averageTransientSalePrice += vm.garages[i].transientSalePrice;
            }
            vm.averageCostPerSpace = vm.averageCostPerSpace / vm.garages.length;
            vm.averageTransientSalePrice = vm.averageTransientSalePrice / vm.garages.length;

        };

        function share() {
            $rootScope.$broadcast('receiveGarages');
        }
        /**
        * @name create
        * @desc Create a new garage
        * @param {string} content The content of the new garage
        * @returns {Promise}
        * @memberOf thinkster.garages.services.Garages
        */
        function create(content) {
            return $http.post('/api/garages/postGarage', content);
        }

        /**
         * @name update
         * @desc Update a garage
         * @param {string} content The content of the new garage
         * @param {int} garageID The garageID
         * @returns {Promise}
         * @memberOf thinkster.garages.services.Garages
         */
        function update(Garage, garageID) {
            console.log(garageID + " " + Garage.garageID);
            return $http.put('/api/garages/putGarage/' + garageID, Garage);
        }

        /**
         * @name get
         * @desc Get the Garages of a given badge ID
         * @param {string} badgeID The badgeID to get Garages for
         * @returns {Promise}
         * @memberOf thinkster.garages.services.Garages
         */
        function getBadge(badgeID) {
            return $http.get('/api/' + badgeID + '/garages/');
        }

        function getCosts() {
            return vm.costs;
        }

        function getTotalCost() {
            return vm.totalCost;
        }

        function getTotalCapacity() {
            return vm.totalCapacity;
        }

        function getTotalLeasedSpaces() {
            return vm.totalLeasedSpaces;
        }

        function getTotalRequiredBufferSize() {
            return vm.totalRequiredBufferSize;
        }

        function getTotalTeamMemberSpaces() {
            return vm.totalTeamMemberSpaces;
        }

        function getAverageCostPerSpace() {
            return vm.averageCostPerSpace;
        }

        function getAverageTransientSalePrice() {
            return vm.averageTransientSalePrice;
        }


    }
})();