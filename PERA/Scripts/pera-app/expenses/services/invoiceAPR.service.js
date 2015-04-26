/**
* InvoiceAPRs
* @namespace pera.expenses.services
*/
(function () {
    'use strict';

    angular
      .module('pera.expenses.services')
      .factory('InvoiceAPRs', InvoiceAPRs);

    InvoiceAPRs.$inject = ['$http', '$q'];


    function InvoiceAPRs($http, $q) {
        var allPromise = undefined;
        var last_request_failed = false;
        var vm = this;
        vm.invoiceAPRs = [];
        vm.invoiceAPR;


        var InvoiceAPRs = {
            all: all,
            reports: reports,
            get: get,
        };

        return InvoiceAPRs;

        /**
        * @name all
        * @desc Get all invoice active parker reports from the database
        */
        function all() {
            if (!allPromise || last_request_failed) {
                //console.log("querying database");
                allPromise = $http.get('/api/InvoiceActiveParkerReports/getInvoiceActiveParkerReports/');
                allPromise.then(allSuccess, allError);
            }
            return allPromise;
        }
        
        /**
        * @desc Function to call if all invoiceAPRs are gotten from the DB
        */
        function allSuccess(data, status, headers, config, response) {
            last_request_failed = false;
            vm.invoiceAPRs = data.data;
            //console.log("service success ", vm.invoiceAPRs);
            return vm.invoiceAPRs;
            //share();
        }

        /**
        * @desc Function to call if there is a problem retrieving the invoiceAPRs
        */
        function allError(data, status, headers, config, response) {
            last_request_failed = true;
            return $q.reject(response);
        }

        /**
          * @desc  
          */
        function reports(invoiceID) {
            var promise = $http.get('/api/invoiceActiveParkerReports/invoice/' + invoiceID);
            promise.then(reportsSuccess);
            return promise;
        }

        function reportsSuccess(data) {
            return data.data;
        }

        function oneSuccess(data, status, headers, config, response) {
            //last_request_failed = false;
            vm.invoiceAPR = data.data;
            console.log("service success ", vm.invoiceAPR);
            return vm.invoiceAPR;
            //share();
        }

        function oneError(data, status, headers, config, response) {
            //last_request_failed = true;
            //Snackbar.error("Error retrieving invoiceAPRs");
            return $q.reject(response);
        }


        /**
        * @name get
        * @desc Get one InvoiceAPR
        * @returns {Promise}
        * @memberOf pera.expenses.services.InvoiceAPRs
        */
        function get(invoiceAPRID) {
            var promise = $http.get('/api/invoiceActiveParkerReports/getInvoiceActiveParkerReports/' + invoiceAPRID);
            //console.log("getting one IAPR");
            promise.then(oneSuccess, oneError);
            return promise;
        }

       

        function oneSuccess(data, status, headers, config, response) {
            //last_request_failed = false;
            vm.invoiceAPR = data.data;
            console.log("service success ", vm.invoiceAPR);
            return vm.invoiceAPR;
            //share();
        }

        function oneError(data, status, headers, config, response) {
            //last_request_failed = true;
            //Snackbar.error("Error retrieving invoiceAPRs");
            return $q.reject(response);
        }

    }

})();