﻿/**
* Garages
* @namespace thinkster.garages.services
*/
(function () {
    'use strict';

    angular
      .module('pera.garages.services')
      .factory('Garages',Garages);

    Garages.$inject = ['$http'];

    /**
    * @namespace Garages
    * @returns {Factory}
    */
    function Garages($http) {   //this is just linking up the functions to variable names and returning them
        var Garages = {
            all: all,
            create: create,
            get: get
        };

        return Garages;

        ////////////////////

        /**
        * @name all
        * @desc Get all Garages
        * @returns {Promise}
        * @memberOf thinkster.garages.services.Garages
        */
        function all() {
            return $http.get('/api/garages/');
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