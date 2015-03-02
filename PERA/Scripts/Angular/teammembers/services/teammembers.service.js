/**
* TeamMembers
* @namespace pera.teammembers.services
*/
(function () {
    'use strict';

    angular
      .module('pera.teammembers.services')
      .factory('TeamMembers', TeamMembers);

    TeamMembers.$inject = ['$http', '$rootScope', '$q', 'Snackbar'];

    /**
    * @namespace TeamMembers
    * @returns {Factory}
    */
    function TeamMembers($http, $rootScope, $q, Snackbar) {   //this is just linking up the functions to variable names and returning them

        var vm = this;
        vm.teammembers = [];
        var last_request_failed = false;
        var promise = undefined;
        

        var TeamMembers = {
            all: all,
            create: create,
            get: get,
            share: share
        };

        return TeamMembers;

        ////////////////////

        /**
        * @name all
        * @desc Get all TeamMembers
        * @returns {Promise}
        * @memberOf pera.teammembers.services.TeamMembers
        */
        function all() {
            if (!promise || last_request_failed) {
                console.log("querying database");
                promise = $http.get('/api/teammembers/');
                promise.then(teammembersSuccessFn, teammembersErrorFn);
            }
            return promise;
        }

        /**
         * @name teammembersSuccessFn
         * @desc Sets the teammembers variable to the list of teammembers
         * @param {string} badgeID The badgeID to get TeamMembers for
         * @returns {Promise}
         * @memberOf thinkster.teammembers.services.TeamMembers
         */
        function teammembersSuccessFn(data, status, headers, config, response) {
            last_request_failed = false;
            vm.teammembers = data.data;
            console.log("service success ", vm.teammembers);
            return vm.teammembers;
            //share();
        }

        function teammembersErrorFn(data, status, headers, config, response) {
            last_request_failed = true;
            //Snackbar.error(data.data.error);
            return $q.reject(response);
        }

        function share() {
            $rootScope.$broadcast('receiveTeamMembers');
        }
        /**
        * @name create
        * @desc Create a new teammember
        * @param {string} content The content of the new teammember
        * @returns {Promise}
        * @memberOf thinkster.teammembers.services.TeamMembers
        */
        function create(content) {
            return $http.post('/api/teammembers/', {
                content: content
            });
        }

        /**
         * @name get
         * @desc Get the TeamMembers of a given badge ID
         * @param {string} badgeID The badgeID to get TeamMembers for
         * @returns {Promise}
         * @memberOf thinkster.teammembers.services.TeamMembers
         */
        function get(badgeID) {
            return $http.get('/api/' + badgeID + '/teammembers/');
        }

        
    }
})();