/**
* EditGarage controller
* @namespace pera.garages.controllers
*/
(function () {
    'use strict';

    angular
      .module('pera.garages.controllers')
      .controller('EditGarageController', EditGarageController);

    EditGarageController.$inject = ['$scope', 'Garages', 'Snackbar', 'GarageManagers', 'SideBar']; //Here 'EditGarage' is the EditGarage Service (pera.garages.service)

    /**
    * @namespace EditGarageController
    */
    function EditGarageController($scope, Garages, Snackbar, GarageManagers, SideBar) {
        var vm = this;
        $scope.garages = []; //the list of garages to be returned
        $scope.managers = [];
        $scope.new = SideBar.getCurrent(); 
        $scope.mode = true;
        $scope.title = "Edit a Garage";
        $scope.clear = clearForm;

        var defaultForm = {
            garageID: '',
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

        $scope.$watch('SideBar.currentGarage', update);

        $scope.edit = angular.copy(defaultForm);
        $scope.add = angular.copy(defaultForm);

        

        vm.submit = submit;

        //$scope.$watch('mode', switchMode);


        Garages.all().then(garagesSuccessFn, garagesErrorFn);
        GarageManagers.all().then(managersSuccess, managersFail);
        //vm.garages = EditGarage.all();
        //console.log(vm.garages);

        // switches modes and saves result in variable
        function switchMode()
        {
            if ($scope.mode === true)
            {
                console.log("add to edit");
                // we are switching from add to edit
                $scope.add = $scope.new; //store the current values
                $scope.new = $scope.edit;//grab the old values
                $scope.title = "Edit a Garage";

            }
            else if ($scope.mode === false)
            {
                console.log("edit to add");
                //we are switching from edit to add
                $scope.edit = $scope.new; //store the current values
                $scope.new = $scope.add;  //grab the old values
                $scope.title = "Add a Garage";

            }

        }

        function update() {
            console.log("watch current garage: " + SideBar.getCurrent().name);
            $scope.new = angular.copy(SideBar.getCurrent());
        }

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


        function submit()
        {
            console.log("submit");
            // Here you will post a garage to the API
            //  using the $http angular service
            //if ($scope.mode === "true")
                Garages.update($scope.new, $scope.new.garageID).then(clearForm);
            //else if ($scope.mode === "false")
            //    Garages.create($scope.new).then(clearForm);
        }

        /**
        * clears the edit garage form
        */
        function clearForm() {
            //clears the form
            $scope.editGarageForm.$setPristine();
            $scope.new = defaultForm;
            $scope.current = defaultForm;
        }

    }
})();
