﻿/**
* Initialiazer
* @namespace pera.map.services
*/

// Google async initializer needs global function, so we use $window
(function () {
    'use strict';

    angular
        .module('pera.map.services')
        .factory('Initializer', Initializer);

    Initializer.$inject = ['$window', '$q'];

    function Initializer($window, $q) {

        //Google's url for async maps initialization accepting callback function
        var asyncUrl = 'https://maps.googleapis.com/maps/api/js?callback=',
            mapsDefer = $q.defer();

        //Callback function - resolving promise after maps successfully loaded
        $window.googleMapsInitialized = mapsDefer.resolve; // removed ()

        //Async loader
        var asyncLoad = function (asyncUrl, callbackName) {
            console.log("async loader");
            var script = document.createElement('script');
            //script.type = 'text/javascript';
            script.src = asyncUrl + callbackName;
            document.body.appendChild(script);
        };
        //Start loading google maps
        asyncLoad(asyncUrl, 'googleMapsInitialized');

        //Usage: Initializer.mapsInitialized.then(callback)
        return {
            mapsInitialized: mapsDefer.promise
        };
    }
})();