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
        vm.costs = [];
        vm.totalCost = 0; //the total cost for all garages
        vm.totalCapacity = 0;
        vm.totalLeasedSpaces = 0;
        vm.totalTeamMemberSpaces = 0;
        vm.totalRequiredBufferSize = 0;
        vm.averageCostPerSpace = 0;
        vm.averageTransientSalePrice = 0;


        
        Garages.all().then(garagesSuccessFn, garagesErrorFn);
        //vm.garages = Garages.all();
        //console.log(vm.garages);


        function garagesSuccessFn(data, status, headers, config) {
            vm.garages = data.data;         //this will depend on what the API returns, it may have to change
            //Garages.calculateTotals();
            vm.costs = Garages.costs();
            vm.totalCost = Garages.totalCost(); //the total cost for all garages
            vm.totalCapacity = Garages.totalCapacity();
            vm.totalLeasedSpaces = Garages.totalLeasedSpaces();
            vm.totalTeamMemberSpaces = Garages.totalTeamMemberSpaces();
            vm.totalRequiredBufferSize = Garages.totalRequiredBufferSize();
            vm.averageCostPerSpace = Garages.averageCostPerSpace();
            vm.averageTransientSalePrice = Garages.averageTransientSalePrice();
        }

        function garagesErrorFn(data, status, headers, config) {
            Snackbar.error(data.data.error);
        }

        
    }
})();
