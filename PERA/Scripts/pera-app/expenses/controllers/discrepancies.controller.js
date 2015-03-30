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

        $scope.garages = [];

        Garages.all().then(garagesSuccessFn);

        function submit() {
            FormSubmit.submit(vm.garageID, vm.monthYear);
        }

        function garagesSuccessFn(data, status, headers, config) {
            $scope.garages = data.data;
        }


    }
})();