/**
* Discrepancies controller
* @namespace pera.fileupload.controllers
*/
(function () {
    'use strict';

    angular
      .module('pera.expenses.controllers')
      .controller('DiscrepanciesController', DiscrepanciesController);

    DiscrepanciesController.$inject = ['$scope', 'FormSubmit', 'Garages', 'Snackbar', '$filter']; //Here 'Garages' is the Garages Service (pera.garages.service)

    /**
    * @namespace DiscrepanciesController
    */
    function DiscrepanciesController($scope, FormSubmit, Garages, Snackbar, $filter) {
        var vm = this;
        vm.monthYear = '';
        $scope.garage = { garageID: 0 };
        vm.garageID = 0;
        vm.submit = submit;
        vm.clearForm = clearForm;
        vm.promise;
        $scope.garages = [];
        $scope.duplicate = [];
        $scope.missing = [];
        $scope.extra = [];
        $scope.spinner = false;
        $scope.loaded = false;

        Garages.all().then(garagesSuccessFn);

        function submit() {
            $scope.spinner = true;
            vm.promise = FormSubmit.submit(vm.garageID, vm.monthYear)
            vm.promise.then(submitSuccess, submitFail);
        }

        $scope.$watch('garage', updateGarageID)

        function updateGarageID() {
            vm.garageID = $scope.garage.garageID;
        }

        function submitSuccess(response) {
            $scope.spinner = false;
            Snackbar.show('Succesfully identified discrepancies');
            clearForm();
            $scope.duplicate = response.data[0];
            $scope.missing = response.data[2];
            $scope.extra = response.data[1];
            $scope.loaded = true;

            console.log("Submit success: " + response.data);
            var i = 0;
            console.log("Duplicates:" + $scope.duplicate);
            console.log("Missing:" + $scope.missing);
            
        }

        function submitFail()
        {
            Snackbar.show('Error identifying discrepancies')
            //clearForm();
        }

        function clearForm() {
            $scope.discrepanciesForm.$setPristine();
            vm.monthYear = '';
            vm.garageID = 0;
        }

        function garagesSuccessFn(data, status, headers, config) {
            $scope.garages = data.data;
        }


    }
})();