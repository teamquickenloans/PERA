﻿/**
* Garages controller
* @namespace pera.garages.controllers
*/
(function () {
    'use strict';

    angular
      .module('pera.garages.controllers')
      .controller('GaragesController', GaragesController);

    GaragesController.$inject = ['$scope', 'Garages']; //Here 'Garages' is the Garages Service (pera.garages.service)

    /**
    * @namespace GaragesController
    */
    function GaragesController($scope, Garages) {
        var vm = this;
        vm.garages = []; //the list of garages to be returned
        
        activate();

        function activate() {
            Garages.all().then(garagesSuccessFn, garagesErrorFn);
        }

        function garagesSuccessFn(data, status, headers, config) {
            vm.garages = data.data;         //this will depend on what the API returns, it may have to change
        }

        function garagesErrorFn(data, status, headers, config) {
            Snackbar.error(data.data.error);
        }
    }
})();