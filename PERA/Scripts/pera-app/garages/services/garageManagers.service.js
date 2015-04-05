/**
* GarageManagers
* @namespace pera.managers.services
*/
(function () {
    'use strict';

    angular
      .module('pera.garages.services')
      .service('GarageManagers', GarageManagers);

    GarageManagers.$inject = ['$http', '$rootScope', '$q', 'Snackbar'];

    /**
    * @namespace GarageManagers
    * @returns {Factory}
    */
    function GarageManagers($http, $rootScope, $q, Snackbar) {   //this is just linking up the functions to variable names and returning them
        
        var vm = this;
        vm.managers = [];
        vm.manager;
        var last_request_failed = false;
        var last_manager_failed = false;
        var allPromise = undefined;
        var managerPromise = undefined;
        
        var GarageManagers = {
            all: all,
            create: create,
            update: update,
            get: get,
            share: share,
           
        };

        return GarageManagers;

        ////////////////////

        /**
        * @name all
        * @desc Get all GarageManagers
        * @returns {Promise}
        * @memberOf pera.managers.services.GarageManagers
        */
        function all() {
            if (!allPromise || last_request_failed) {
                console.log("querying database");
                allPromise = $http.get('/api/garageManagers/');
                allPromise.then(managersSuccess, managersError);
            }
            return allPromise;
        }

        /**
        * @name get
        * @desc Get one Garage
        * @returns {Promise}
        * @memberOf pera.managers.services.GarageManagers
        */
        function get(managerID) 
        {
            if (!managerPromise[managerID] || last_manager_failed[managerID]) {
                managerPromise[managerID] = $http.get('/api/garageManagers/' + managerID);
                console.log("getting one garageManager");
                managerPromise.then(managerSuccess, managerError);
            }
            return managerPromise;
        }

        function managerSuccess(data, status, headers, config, response) {
            last_manager_failed = false;
            vm.manager = data.data;
            console.log("service success ", vm.manager);
            return vm.manager;
        }

        function managerError(data, status, headers, config, response) {
            last_manager_failed = true;
            Snackbar.error("Error retrieving managers");
            return $q.reject(response);
        }


        function managersSuccess(data, status, headers, config, response) {
            last_request_failed = false;
            vm.managers = data.data;
            console.log("service success ", vm.managers);
            calculateTotals();
            return vm.managers;
            //share();
        }

        function managersError(data, status, headers, config, response) {
            last_request_failed = true;
            Snackbar.error("Error retrieving managers");
            return $q.reject(response);
        }
        function calculateTotals() {
            for (var i = 0; i < vm.managers.length; i++) {
                var temp = vm.managers[i].numberOfLeasedSpaces * vm.managers[i].spaceCost;
                vm.managers[i].cost = temp;
                vm.totalCost += temp;
                vm.totalCapacity += vm.managers[i].capacity;
                vm.totalLeasedSpaces += vm.managers[i].numberOfLeasedSpaces;
                vm.totalRequiredBufferSize += vm.managers[i].minimumNumberOfBufferSpaces;
                vm.totalTeamMemberSpaces += vm.managers[i].numberOfTeamMemberSpaces;

                vm.averageCostPerSpace += vm.managers[i].spaceCost;
                vm.averageTransientSalePrice += vm.managers[i].transientSalePrice;
            }
            vm.averageCostPerSpace = vm.averageCostPerSpace / vm.managers.length;
            vm.averageTransientSalePrice = vm.averageTransientSalePrice / vm.managers.length;

        };

        function share() {
            $rootScope.$broadcast('receiveGarageManagers');
        }
        /**
        * @name create
        * @desc Create a new manager
        * @param {string} content The content of the new manager
        * @returns {Promise}
        * @memberOf thinkster.managers.services.GarageManagers
        */
        function create(content) {
            return $http.post('/api/managers/', content);
        }
        /**
         * @name update
         * @desc Update a manager
         * @param {string} content The content of the new manager
         * @param {int} managerID The managerID
         * @returns {Promise}
         * @memberOf thinkster.managers.services.GarageManagers
         */
        function update(Garage, managerID) {
            console.log(managerID + " " + Garage.managerID);
            return $http.put('/api/managers/' + managerID, Garage);
        }

        /**
         * @name get
         * @desc Get the GarageManagers of a given badge ID
         * @param {string} badgeID The badgeID to get GarageManagers for
         * @returns {Promise}
         * @memberOf thinkster.managers.services.GarageManagers
         */
        function getBadge(badgeID) {
            return $http.get('/api/' + badgeID + '/managers/');
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