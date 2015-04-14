/**
* ParkerReportTeamMembers
* @namespace pera.invoiceTeamMembers.services
*/
(function () {
    'use strict';

    angular
      .module('pera.invoiceTeamMembers.services')
      .factory('ParkerReportTeamMembers', ParkerReportTeamMembers);

    ParkerReportTeamMembers.$inject = ['$http', '$rootScope', '$q', 'Snackbar'];

    /**
    * @namespace ParkerReportTeamMembers
    * @returns {Factory}
    */
    function ParkerReportTeamMembers($http, $rootScope, $q, Snackbar) {   //this is just linking up the functions to variable names and returning them

        var vm = this;
        vm.invoiceTeamMembers = [];
        var last_request_failed = false;
        var promise = undefined;


        var ParkerReportTeamMembers = {
            all: all,
            create: create,
            get: get,
            share: share
        };

        return ParkerReportTeamMembers;

        ////////////////////

        /**
        * @name all
        * @desc Get all ParkerReportTeamMembers
        * @returns {Promise}
        * @memberOf pera.invoiceTeamMembers.services.ParkerReportTeamMembers
        */
        function all() {
            if (!promise || last_request_failed) {
                console.log("querying database");
                promise = $http.get('/api/parkerReportTeamMembers/getParkerReportTeamMembers/');
                promise.then(invoiceTeamMembersSuccessFn, invoiceTeamMembersErrorFn);
            }
            return promise;
        }

        /**
         * @name invoiceTeamMembersSuccessFn
         * @desc Sets the invoiceTeamMembers variable to the list of invoiceTeamMembers
         * @param {string} badgeID The badgeID to get ParkerReportTeamMembers for
         * @returns {Promise}
         * @memberOf thinkster.invoiceTeamMembers.services.ParkerReportTeamMembers
         */
        function invoiceTeamMembersSuccessFn(data, status, headers, config, response) {
            last_request_failed = false;
            vm.invoiceTeamMembers = data.data;
            console.log("service success ", vm.invoiceTeamMembers);
            return vm.invoiceTeamMembers;
            //share();
        }

        function invoiceTeamMembersErrorFn(data, status, headers, config, response) {
            last_request_failed = true;
            Snackbar.error(data.data.error);
            return $q.reject(response);
        }

        function share() {
            $rootScope.$broadcast('receiveParkerReportTeamMembers');
        }
        /**
        * @name create
        * @desc Create a new invoiceTeamMembers
        * @param {string} content The content of the new invoiceTeamMembers
        * @returns {Promise}
        * @memberOf thinkster.invoiceTeamMembers.services.ParkerReportTeamMembers
        */
        function create(content) {
            return $http.post('/api/invoiceTeamMembers/', {
                content: content
            });
        }

        /**
         * @name get
         * @desc Get the ParkerReportTeamMembers of a given badge ID
         * @param {string} badgeID The badgeID to get ParkerReportTeamMembers for
         * @returns {Promise}
         * @memberOf thinkster.invoiceTeamMembers.services.ParkerReportTeamMembers
         */
        function get(badgeID) {
            return $http.get('/api/' + badgeID + '/invoiceTeamMembers/');
        }


    }
})();