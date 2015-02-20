/**
* Tabs controller
* @namespace pera.tabs.controllers
*/
(function () {
    'use strict';

    angular
      .module('pera.tabs.controllers')
      .controller('TabController', TabController);


    function TabController() {
        this.tab = 1;

        this.setTab = function (val) {
            this.tab = val;
        };

        this.isSet = function (val) {
            return this.tab === val;
        };



        //for nav bar side tabs ----------------------
        this.sideTab = 0;

        this.setSideTab = function (val) {
            this.sideTab = val;
        };

        this.sideIsSet = function (val) {
            return this.sideTab === val;
        };
    }
})();