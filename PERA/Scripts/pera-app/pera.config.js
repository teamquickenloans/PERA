(function () {
    'use strict';

    angular
      .module('pera.config')
      .config(config);

    //config.$inject = ['ui.router'];

    /**
    * @name config
    * @desc Enable HTML5 routing
    */
    function config($stateProvider, $routeProvider) {
        /*
        
        $routeProvider
                  .when('/garageMap', {
                      templateUrl: "GarageMap/garageMap",
                      controller: 'MapController as map'
                  });*/
                      /*
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
            });*/
       
        $stateProvider
          .state('detectedIssues', {
              url: "/detectedIssues",
              views: {
                  'content': {
                      templateUrl: "ReconcileExpenses/DetectedIssues",
                      controller: "ExpensesController as expenseCtrl"
                  },
                  'sidebar': {
                      templateUrl: "GarageMap/Overview",
                      controller: "SideBarController as garageCtrl"
                  }
              }
              
          })
          .state('uploadHistory', {
              views: {
                  'content': {
                      templateUrl: 'ReconcileExpenses/UploadHistory',
                      controller: 'ExpensesController as expenseCtrl'
                  }
              }
          })


          .state('overview', {
              views: {
                  'sidebar': {
                      templateUrl: "GarageMap/Overview",
                      controller: "SideBarController as garageCtrl"
                  }
              }
          })
          .state('singleGarage', {
              views: {
                  'sidebar': {
                      templateUrl: "GarageMap/SingleGarage"
                  }
              }

          })
          .state('garageMap', {
              templateUrl: 'GarageMap/GarageMap',
              views: {
                  'garageMap.sidebartop@garageMap': {
                      templateUrl: 'GarageMap/Overview',
                      controller: 'MapController as map'
                  },
                  'garageMap.sidebarbottom@garageMap': {
                      templateUrl: 'GarageMap/SingleGarage',
                      controller: 'MapController as map'
                  }
        }
          });
    }
})();