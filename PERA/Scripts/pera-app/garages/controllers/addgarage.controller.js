/**
* AddGarage controller
* @namespace pera.garages.controllers
*/
(function () {
    'use strict';

    angular
      .module('pera.garages.controllers')
      .controller('AddGarageController', AddGarageController);

    AddGarageController.$inject = ['$scope', 'Garages', 'Snackbar' , 'GarageManagers']; //Here 'AddGarage' is the AddGarage Service (pera.garages.service)

    /**
    * @namespace EditGarageController
    */
    function AddGarageController($scope, Garages, Snackbar, GarageManagers) {
        var vm = this;
        $scope.garages = []; //the list of garages to be returned
        $scope.garageManagers = [];
        $scope.title = "Add a Garage";


        var defaultForm = {
            //garageID: '',
            name: '',
            address: '',
            latitude: '',
            longitude: '',
            capacity: '',
            numberOfLeasedSpaces: '',
            numberOfTeamMemberSpaces: '',
            minimumNumberOfBufferSpaces: '',
            spaceCost: '',
            transientSalePrice: '',
            owner: '',
            billingParty: '',
            //reportType: '',
            //accessToken: '',
            //accessTokenOptional: '',
            accessTokenCost: '',
            changeCost: '',
            validationCost: '',
            numberOfValidations: '',
            garageManagerID: ''
        };


        GarageManagers.all().then(managersSuccess, managersFail);
        $scope.new = angular.copy(defaultForm);


        vm.submit = submit;

        //vm.garages = EditGarage.all();
        //console.log(vm.garages);


        function garagesSuccessFn(data, status, headers, config) {
            $scope.garages = data.data;         //this will depend on what the API returns, it may have to change
        }

        function garagesErrorFn(data, status, headers, config) {
            //Snackbar.error("Failed to retrieve garages");
        }

        function managersSuccess(data, status, headers, config) {
            $scope.managers = data.data;         //this will depend on what the API returns, it may have to change
        }

        function managersFail(data, status, headers, config) {
            //Snackbar.error("Failed to retrieve garages");
        }


        function submit() {
            console.log("submit");
            // Here you will post a garage to the API
            //  using the $http angular service
            Garages.create($scope.new, $scope.new.garageID).then(success);
        }

        function success() {
            Snackbar.show($scope.new.name + " has been created");
            clearForm();
        }

       /*
         * @desc Clears the add garage form
         */
        function clearForm() {
            //clears the form
            $scope.editGarageForm.$setPristine();
            $scope.new = defaultForm;
            $scope.current = defaultForm;
        }


    }
})();