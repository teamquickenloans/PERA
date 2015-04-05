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
        vm.garageID = 0;
        vm.submit = submit;
        vm.clearForm = clearForm;
        vm.promise;
        $scope.garages = [];
        $scope.discrepancies = [];

        Garages.all().then(garagesSuccessFn);

        function submit() {
            vm.promise = FormSubmit.submit(vm.garageID, vm.monthYear)
            vm.promise.then(submitSuccess, submitFail);
        }

        function submitSuccess(response) {
            Snackbar.show('Succesfully identified discrepancies');
            clearForm();
            $scope.discrepancies = response.data;
            console.log("Submit success: " + response.data);
            var i = 0;
            angular.forEach(response.data, function (value, key) {
                console.log(value);
            });
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