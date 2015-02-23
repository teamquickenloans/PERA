/**
* Garages controller
* @namespace pera.garages.controllers
*/
(function () {
    'use strict';

    angular
      .module('pera.garages.controllers')
      .controller('GaragesController', GaragesController);

    GaragesController.$inject = ['$scope', 'Garages', 'Snackbar']; //Here 'Garages' is the Garages Service (pera.garages.service)

    /**
    * @namespace GaragesController
    */
    function GaragesController($scope, Garages, Snackbar) {
        var vm = this;
        vm.garages = []; //the list of garages to be returned
        vm.costs = []; //the total cost of leased spaces for each garage
        vm.totalCost = 0; //the total cost for all garages
        vm.totalCapacity = 0;
        vm.totalLeasedSpaces = 0;
        vm.totalTeamMemberSpaces = 0;
        vm.totalRequiredBufferSize = 0;
        vm.averageCostPerSpace = 0;
        vm.averageTransientSalePrice = 0;

        
        activate();

        function activate() {
            Garages.all().then(garagesSuccessFn, garagesErrorFn);
        }

        function garagesSuccessFn(data, status, headers, config) {
            vm.garages = data.data;         //this will depend on what the API returns, it may have to change
            calculateData();
        }

        function garagesErrorFn(data, status, headers, config) {
            Snackbar.error(data.data.error);
        }


        //TODO: do this in the backend and possibly append it to the passed json file.
        function calculateData () //TODO: Move this to a service so it is only called once
        {
            for (var i = 0; i < vm.garages.length; i++)
            {
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
