(function () {
    'use strict';

    angular
      .module('pera.config')
      .config(config);

    config.$inject = ['ui.router'];

    /**
    * @name config
    * @desc Enable HTML5 routing
    */
    function config($stateProvider) {

        /*
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
        */
        $stateProvider
          .state('detectedIssues', {
              url: "detectedIssues",
              templateUrl: "ReconcileExpenses/detectedIssues.html"
          })
          .state('uploadHistory', {
              url: "uploadHistory",
              templateUrl: "ReconcileExpenses/uploadHistory.html"
          })
          .state('garageMap', {
              url: "garageMap",
              template: "GarageMap/garageMap.html"
          });
    }
})();