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

        //this is a nested view.  it will populate the ui-view inside of Upload/InvoiceForm.cshtml
        var uploadHistory_invoice = {
            name: 'uploadHistory.invoice',
            templateUrl: 'Upload/InvoiceForm'
        }


        $stateProvider.state(home);
        $stateProvider.state(overview);
        $stateProvider.state(singleGarage);
        $stateProvider.state(detectedIssues);
        $stateProvider.state(uploadHistory);
        $stateProvider.state(uploadHistory_invoice);
        $stateProvider.state(garageMap);


    }
})();