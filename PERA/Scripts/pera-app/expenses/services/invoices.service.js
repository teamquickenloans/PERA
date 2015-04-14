/**
* Invoices
* @namespace pera.expenses.services
*/
(function () {
    'use strict';

    angular
      .module('pera.expenses.services')
      .factory('Invoices', Invoices);

    Invoices.$inject = ['$http'];


    function Invoices($http) {
        var allPromise = undefined;
        var last_request_failed = false;
        var vm = this;
        vm.invoices = [];

        var Invoices = {
            all: all
        };

        return Invoices;

        /**
        * @name submit
        * @desc Try to submit a file using ng-file-submit
        */
        function all() {
            if (!allPromise || last_request_failed) {
                console.log("querying database");
                allPromise = $http.get('/api/invoices/getInvoices/');
                allPromise.then(invoiceSuccess, invoiceError);
            }
            return allPromise;
        }
        
        /**
        * @name getParkerReports
        * @desc 
        */
        function invoiceSuccess(data, status, headers, config, response) {
            last_request_failed = false;
            vm.invoices = data.data;
            console.log("service success ", vm.invoices);
            return vm.invoices;
            //share();
        }

        function invoiceError(data, status, headers, config, response) {
            last_request_failed = true;
            Snackbar.error("Error retrieving invoices");
            return $q.reject(response);
        }
    }

})();