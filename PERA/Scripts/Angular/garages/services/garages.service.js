/**
* Garages
* @namespace pera.garages.services
*/
(function () {
    'use strict';

    angular
      .module('pera.garages.services')
      .factory('Garages', Garages);

    Garages.$inject = ['$http', '$rootScope', '$q', 'Snackbar'];

    /**
    * @namespace Garages
    * @returns {Factory}
    */
    function Garages($http, $rootScope, $q, Snackbar) {   //this is just linking up the functions to variable names and returning them
        
        var vm = this;
        vm.garages = [];
        var last_request_failed = false;
        var promise = undefined;


        var Garages = {
            all: all,
            create: create,
            get: get,
            share: share
        };

        return Garages;

        ////////////////////

        /**
        * @name all
        * @desc Get all Garages
        * @returns {Promise}
        * @memberOf pera.garages.services.Garages
        */
        function all() {
            if (!promise || last_request_failed) {
                console.log("querying database");
                promise = $http.get('/api/garages/');
                promise.then(garagesSuccessFn, garagesErrorFn);
            }
            return promise
        }

        /**
         * @name garagesSuccessFn
         * @desc Sets the garages variable to the list of garages
         * @param {string} badgeID The badgeID to get Garages for
         * @returns {Promise}
         * @memberOf thinkster.garages.services.Garages
         */
        function garagesSuccessFn(data, status, headers, config, response) {
            last_request_failed = false;
            vm.garages = data.data;
            console.log("service success ", vm.garages);
            return vm.garages;
            //share();
        }

        function garagesErrorFn(data, status, headers, config, response) {
            last_request_failed = true;
            Snackbar.error(data.data.error);
            return $q.reject(response);
        }

        function share() {
            $rootScope.$broadcast('receiveGarages');
        }
        /**
        * @name create
        * @desc Create a new garage
        * @param {string} content The content of the new garage
        * @returns {Promise}
        * @memberOf thinkster.garages.services.Garages
        */
        function create(content) {
            return $http.post('/api/garages/', {
                content: content
            });
        }

        /**
         * @name get
         * @desc Get the Garages of a given badge ID
         * @param {string} badgeID The badgeID to get Garages for
         * @returns {Promise}
         * @memberOf thinkster.garages.services.Garages
         */
        function get(badgeID) {
            return $http.get('/api/' + badgeID + '/garages/');
        }


    }
})();