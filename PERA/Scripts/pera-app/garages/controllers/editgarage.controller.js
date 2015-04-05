/**
* EditGarage controller
* @namespace pera.garages.controllers
*/
(function () {
    'use strict';

    angular
      .module('pera.garages.controllers')
      .controller('EditGarageController', EditGarageController);

    EditGarageController.$inject = ['$scope', 'Garages', 'Snackbar']; //Here 'EditGarage' is the EditGarage Service (pera.garages.service)

    /**
    * @namespace EditGarageController
    */
    function EditGarageController($scope, Garages, Snackbar) {
        var vm = this;
        $scope.garages = []; //the list of garages to be returned
        $scope.current;

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
            reportType: '',
            accessToken: '',
            accessTokenOptional: '',
            accessTokenCost: '',
            changeCost: '',
            validationCost: '',
            numberOfValidations: '',
            garageManager: ''
        };
        $scope.new = defaultForm;

        vm.submit = submit;

        $scope.$watch('current', update)

        Garages.all().then(garagesSuccessFn, garagesErrorFn);
        //vm.garages = EditGarage.all();
        //console.log(vm.garages);

        function update()
        {
            $scope.new = $scope.current;
            console.log($scope.new.name);
        }
        

        function garagesSuccessFn(data, status, headers, config) {
            $scope.garages = data.data;         //this will depend on what the API returns, it may have to change
        }

        function garagesErrorFn(data, status, headers, config) {
            //Snackbar.error("Failed to retrieve garages");
        }

        function submit()
        {
            console.log("submit");
            // Here you will post a garage to the API
            //  using the $http angular service
            if ($scope.editGarageForm.garagesubmit == "add") {
                console.log("add garage");
                Garages.create($scope.new, $scope.new.garageID).then(clearForm);    
            }
            else if ($scope.editGarageForm.garagesubmit == "edit") {
                console.log("edit garage");
                Garages.update($scope.new, $scope.new.garageID).then(clearForm);
            }
            
        }

       /** function next()
        {
            if ($scope.addOrEditGarageForm.garagevalue == "add") {
                //Garages.create($scope.new, $scope.new.garageID).then(clearForm);
                console.log("add garage");
            }
            else if ($scope.addOrEditGarageForm.garagevalue == "edit"){
                //submit();
                console.log("edit garage");
            }
            
        }*/

        /**
        * clears the edit garage form
        */
        function clearForm() {
            Snackbar.show("Garage updated successfully");
            //clears the form
            $scope.editGarageForm.$setPristine();
            $scope.new = defaultForm;
            $scope.current = defaultForm;
        }

    }
})();
