/**
* BadgeScanReports
* @namespace pera.expenses.services
*/
(function () {
    'use strict';

    angular
      .module('pera.expenses.services')
      .service('BadgeScanReports', BadgeScanReports);

    BadgeScanReports.$inject = ['$http', '$q'];


    function BadgeScanReports($http, $q) {

        var BadgeScanReports = {
            all: all
        };

        return BadgeScanReports;

        var vm = this;
        $scope.reports = {};
        var allPromise = undefined;
        var last_request_failed = false;

        /**
        * @name all
        * @desc Get all invoice active parker reports from the database
        */
        function all() {
            if (!allPromise || last_request_failed) {
                //console.log("querying database");
                allPromise = $http.get('/api/BadgeScanReports/getBadgeScanReports/');
                allPromise.then(allSuccess, allError);
            }
            return allPromise;
        }

        /**
     * @desc Function to call if all invoiceAPRs are gotten from the DB
     */
        function allSuccess(data, status, headers, config, response) {
            last_request_failed = false;
            return data.data;
            //share();
        }

        /**
        * @desc Function to call if there is a problem retrieving the invoiceAPRs
        */
        function allError(data, status, headers, config, response) {
            last_request_failed = true;
            return $q.reject(response);
        }

    }
})();