/**
* Garages controller
* @namespace thinkster.authentication.controllers
*/
(function () {
    'use strict';

    angular
      .module('thinkster.authentication.controllers')
      .controller('GaragesController', GaragesController);

    GaragesController.$inject = ['$location', '$scope', 'Garages']; //Here 'Garages' is the Garages Service (pera.garages.service)

    /**
    * @namespace GaragesController
    */
    function GaragesController($location, $scope, Garages) {
        var vm = this;
        $scope.garages = []; //the list of garages to be returned
        
        activate();

        /**
        * @name register
        * @desc Garages a new user
        * @memberOf thinkster.authentication.controllers.GaragesController
        */
        function register() {
            Authentication.register(vm.email, vm.password, vm.username);
        }

        function activate() {
            Garages.all().then(garagesSuccessFn, garagesErrorFn);
        }

        function garagesSuccessFn(data, status, headers, config) {
            $scope.garages = data.data;         //this will depend on what the API returns, it may have to change
        }

        function garagesErrorFn(data, status, headers, config) {
            Snackbar.error(data.data.error);
        }

    }
})();