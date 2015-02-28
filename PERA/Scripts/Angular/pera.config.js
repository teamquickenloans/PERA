(function () {
    'use strict';

    angular
      .module('pera.config')
      .config(config);

    config.$inject = ['$routeProvider'];

    /**
    * @name config
    * @desc Enable HTML5 routing
    */
    function config($routeProvider) {


        $routeProvider.
            when('/routeOne', {
                templateUrl: 'ingestInvoice/one'
            })
            .when('/routeTwo', {
                templateUrl: 'ingestInvoice/two'
            })
            .when('/routeThree', {
                templateUrl: 'ingestInvoice/three'
            })
            .when('/invoiceForm', {
                templateUrl: 'Upload/InvoiceForm'
            })
            .when('/invoiceFile', {
                templateUrl: 'Upload/InvoiceFile'
            });
    }
})();