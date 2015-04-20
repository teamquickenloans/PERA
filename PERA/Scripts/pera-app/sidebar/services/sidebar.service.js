/**
* SideBar
* @namespace pera.sidebar.services
*/
(function () {
    'use strict';

    angular
      .module('pera.sidebar.services')
      .factory('SideBar', SideBar);

    //SideBar.$inject = ['$http', '$rootScope', '$q', 'Snackbar'];

    /**
    * @namespace SideBar
    * @returns {Factory}
    */
    function SideBar() {   //this is just linking up the functions to variable names and returning them

        var vm = this;
        vm.currentGarage = null;
        vm.marker = null;

        var SideBar = {
            getCurrent: getCurrent,
            setCurrent: setCurrent
        };

        return SideBar;

        function getCurrent() {
            return vm.currentGarage;
        }
        function setCurrent(garage) {
            vm.currentGarage = garage;
            console.log(garage.name);
        }
        function setMarker(marker) {
            vm.marker = marker;
            console.log("SideBar.setMarker");
        }
        function getMarker() {
            return vm.marker;
        }
    }
})();