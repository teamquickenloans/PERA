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
        $scope.new = {
            name: '',
            garageID: '',

        }

        $scope.edit;
        $scope.add;

        
        $scope.mode = "false";
        vm.submit = submit;

        $scope.watch('mode', switchMode);
        $scope.$watch('current', update)

        Garages.all().then(garagesSuccessFn, garagesErrorFn);
        //vm.garages = EditGarage.all();
        //console.log(vm.garages);

        // switches modes and saves result in variable
        function switchMode()
        {
            if( mode === "true")
            {
                // we are switching from add to edit
                $scope.current = $scope.edit;
                $scope.add = $scope.current;
            }

        }

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
            Garages.update($scope.new,$scope.new.garageID);
        }



    }
})();
