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
        var last_request_failed = false;
        var promise = undefined;
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
            if (!promise || last_request_failed) {
                console.log("querying database");
                promise = $http.get('/api/garages/');
                promise.then(garagesSuccessFn, garagesErrorFn);
            }
            return promise
        }

        /**
         * @name garagesSuccessFn
         * @desc Sets the garages variable to the list of garages
         * @param {string} badgeID The badgeID to get Garages for
         * @returns {Promise}
         * @memberOf thinkster.garages.services.Garages
         */
        function garagesSuccessFn(data, status, headers, config, response) {
            last_request_failed = false;
            vm.garages = data.data;
            console.log("service success ", vm.garages);
            calculateTotals();
            return vm.garages;
            //share();
        }

        function garagesErrorFn(data, status, headers, config, response) {
            last_request_failed = true;
            Snackbar.error(data.data.error);
            return $q.reject(response);
        }

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
            return $http.post('/api/garages/', {
                content: content
            });
        }

        /**
         * @name get
         * @desc Get the Garages of a given badge ID
         * @param {string} badgeID The badgeID to get Garages for
         * @returns {Promise}
         * @memberOf thinkster.garages.services.Garages
         */
        function get(badgeID) {
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
            return vm.getTotalTeamMemberSpaces;
        }

        function getAverageCostPerSpace() {
            return vm.averageCostPerSpace;
        }

        function getAverageTransientSalePrice() {
            return vm.averageTransientSalePrice;
        }

        function calculateTotals() //TODO: Move this to a service so it is only called once
        {
            for (var i = 0; i < vm.garages.length; i++) {
                var temp = vm.garages[i].numberOfLeasedSpaces * vm.garages[i].spaceCost;
                vm.costs[i] = temp;
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
    }
})();