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
    function config($stateProvider, $routeProvider, $urlRouterProvider) {
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

        var home = {
            name: 'home',
            url: '/',
            template: 'welcome to the Parking Dashboard'
        };

        var overview = {
            name: 'overview',
            url: '/overview',
            sticky: true,
            views: {
                'sidebar': {
                    templateUrl: "GarageMap/Overview",
                    controller: "SideBarController as garageCtrl"
                }
            }
        };

        //////////////////////
        //  GarageMap       //
        //////////////////////
        var garageMap = {
            name: 'garageMap',
            sticky: true,
            views:
                {
                    'main' : {
                        templateUrl: "GarageMap/GarageMap",
                        controller: "MapController as map"
                    }
                }

        };

        var singleGarage = {
            name: 'singleGarage',
            sticky: true,
            views:
                {
                    'sidebar': {
                        templateUrl: "GarageMap/SingleGarage",
                        controller: "SideBarController as sidebar"
                    }
                }
        }

        var detectedIssues = {
            name: 'detectedIssues',
            sticky: true,
            views: {
                'main': {
                    templateUrl: "ReconcileExpenses/DetectedIssues",
                    controller: "ExpensesController as expenseCtrl"
                }
            }
        }
        var uploadHistory = {
            name: 'uploadHistory',
            sticky: true,
            views: {
                'main': {
                    templateUrl: 'ReconcileExpenses/UploadHistory',
                    controller: 'ExpensesController as expenseCtrl'
                }
            }
        }

        var uploadHistory_invoice = {
            name: 'uploadHistory.invoice',
            templateUrl: 'Upload/InvoiceForm'
        }

        /*
        var uploadHistory_singleGarage = {
            name: 'uploadHistory.singleGarage',
            views: {
                'sidebar': {
                    templateUrl: "GarageMap/SingleGarage",
                    controller: "SideBarController as sidebar"
                },
                'content': {
                    templateUrl: 'ReconcileExpenses/UploadHistory',
                    controller: 'ExpensesController as expenseCtrl'
                }
            }
        };

        


        var garageMapSG = {
        name: 'garageMap.singleGarge',
        templateUrl: "GarageMap/SingleGarage",
        controller: "SideBarController as sidebar"
        };*/

        $stateProvider.state(home);
        $stateProvider.state(overview);
        $stateProvider.state(singleGarage);
        $stateProvider.state(detectedIssues);
        $stateProvider.state(uploadHistory);
        //$stateProvider.state(uploadHistory_singleGarage);
        $stateProvider.state(garageMap);
        //$stateProvider.state(garageMapSG);
        $stateProvider.state(uploadHistory_invoice);


    }
})();