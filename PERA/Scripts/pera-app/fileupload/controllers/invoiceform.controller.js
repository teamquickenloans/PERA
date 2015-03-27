/**
* InvoiceForm controller
* @namespace pera.fileupload.controllers
*/
(function () {
    'use strict';

    angular
      .module('pera.fileupload.controllers')
      .controller('InvoiceFormController', InvoiceFormController);

    InvoiceFormController.$inject = ['$scope', '$http']; //Here 'Garages' is the Garages Service (pera.garages.service)

    /**
    * @namespace InvoiceFormController
    */
    function InvoiceFormController($scope, $http) {
        $scope.invoiceForm = {
            invoiceID: '',
            totalAmountBilled: '',
            dateReceived: '',
            billingStartDate: '',
            billingEndDate: '',
            numberOfLeasedSpots: '',
            numberOfValidations: ''
        }

        $scope.submit = submit;

        function submit() {
            //TODO:
        }
    }
})();
