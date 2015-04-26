/**
* QLAPRs
* @namespace pera.expenses.services
*/
(function () {
    'use strict';

    angular
      .module('pera.expenses.services')
      .factory('QLAPRs', QLAPRs);

    QLAPRs.$inject = ['$http'];


    function QLAPRs($http) {
        var allPromise = undefined;
        var last_request_failed = false;
        var vm = this;
        vm.reports = [];

        var QLAPRs = {
            all: all,
            get: get
        };

        return QLAPRs;

        /**
        * @name submit
        * @desc Try to submit a file using ng-file-submit
        */
        function all() {
            if (!allPromise || last_request_failed) {
               // console.log("querying database");
                allPromise = $http.get('api/QLActiveParkerReports/getQLActiveParkerReports/');
                allPromise.then(reportSuccess, reportError);
            }
            return allPromise;
        }
        
        /**
        * @name get
        * @desc Get one InvoiceAPR
        * @returns {Promise}
        * @memberOf pera.expenses.services.InvoiceAPRs
        */
        function get(reportID) {
            var promise = $http.get('/api/qlActiveParkerReports/getQLactiveParkerReport/' + reportID);
            return promise;
        }

        /**
        * @name getParkerReports
        * @desc 
        */
        function reportSuccess(data, status, headers, config, response) {
            last_request_failed = false;
            vm.reports = data.data;
            //console.log("service success ", vm.reports);
            return vm.reports;
            //share();
        }

        function reportError(data, status, headers, config, response) {
            last_request_failed = true;
            //Snackbar.error("Error retrieving reports");
            return $q.reject(response);
        }
    }

})();