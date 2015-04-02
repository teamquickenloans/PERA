/**
* AddGarage controller
* @namespace pera.garages.controllers
*/
(function () {
    'use strict';

    angular
      .module('pera.garages.controllers')
      .controller('AddGarageController', AddGarageController);

    AddGarageController.$inject = ['$scope', 'Garages', 'Snackbar']; //Here 'AddGarage' is the AddGarage Service (pera.garages.service)

    /**
    * @namespace EditGarageController
    */
    function AddGarageController($scope, Garages, Snackbar) {
        var vm = this;
        $scope.garages = []; //the list of garages to be returned
        $scope.new = {
            name: '',
            garageID: '',

        }
        vm.submit = submit;

        //vm.garages = EditGarage.all();
        //console.log(vm.garages);


        function garagesSuccessFn(data, status, headers, config) {
            $scope.garages = data.data;         //this will depend on what the API returns, it may have to change
        }

        function garagesErrorFn(data, status, headers, config) {
            //Snackbar.error("Failed to retrieve garages");
        }

        function submit() {
            console.log("submit");
            // Here you will post a garage to the API
            //  using the $http angular service
            Garages.create($scope.new, $scope.new.garageID);
        }



    }
})();