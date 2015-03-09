/**
* SideBar controller
* @namespace pera.sidebar.controllers
*/
(function () {
	'use strict';

	angular
      .module('pera.sidebar.controllers')
      .controller('SideBarController', SideBarController);

	SideBarController.$inject = ['$scope', 'SideBar', 'Garages', 'Snackbar'];

	/**
    * @namespace SideBarController
    */
	function SideBarController($scope, SideBar, Garages, Snackbar) {
		var vm = this;
		vm.garages = [];
		vm.currentGarage = null;
		vm.getCurrent = getCurrent;
		vm.setCurrent = setCurrent;

		vm.costs = [];
		vm.totalCost = 0; //the total cost for all garages
		vm.totalCapacity = 0;
		vm.totalLeasedSpaces = 0;
		vm.totalTeamMemberSpaces = 0;
		vm.totalRequiredBufferSize = 0;
		vm.averageCostPerSpace = 0;
		vm.averageTransientSalePrice = 0;

		Garages.all().then(garagesSuccessFn);

		function all() {
		    return Garages.all();
		}
		function garagesSuccessFn(data, status, headers, config) {
			vm.garages = data.data;
			vm.costs = Garages.costs();
			vm.totalCost = Garages.totalCost(); //the total cost for all garages
			vm.totalCapacity = Garages.totalCapacity();
			vm.totalLeasedSpaces = Garages.totalLeasedSpaces();
			vm.totalTeamMemberSpaces = Garages.totalTeamMemberSpaces();
			vm.totalRequiredBufferSize = Garages.totalRequiredBufferSize();
			vm.averageCostPerSpace = Garages.averageCostPerSpace();
			vm.averageTransientSalePrice = Garages.averageTransientSalePrice();
		}
        
		function getCurrent() {
		    vm.currentGarage = SideBar.getCurrent();
		    return vm.currentGarage;
		}_

		function setCurrent(current) {
		    SideBar.setCurrent(current);
		}
	}
})();