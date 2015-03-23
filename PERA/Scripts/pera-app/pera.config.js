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
        }

        ////////////////////////////////////////////////////
        //          SideBar                               //
        ////////////////////////////////////////////////////
        var overview = {
            name: 'overview',
            sticky: true,
            views: {
                'sidebar': {
                    templateUrl: "SideBar/Overview",
                    controller: "SideBarController as garageCtrl"
                }
            }
        }

        var singleGarage = {
            name: 'singleGarage',
            sticky: true,
            views:
                {
                    'sidebar': {
                        templateUrl: "SideBar/SingleGarage",
                        controller: "SideBarController as sidebar"
                    }
                }
        }

        ////////////////////////////////////////////////////
        //          Main                                  //
        ////////////////////////////////////////////////////

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
        /*
        //this is a nested view.  it will populate the ui-view inside of Upload/InvoiceForm.cshtml
        var uploadHistory_invoice = {
            name: 'invoice',
            views: {
                'modal':{
                    templateUrl: 'Upload/InvoiceForm',
                    controller: 'FileUploadController as fileCtrl'
                }
            }*/

        var invoice = {
            name: 'invoice',
            sticky: true,
            views: {
                'main' : {
                    templateUrl: 'Upload/InvoiceForm',
                    controller: 'FileUploadController as fileCtrl'
                }
            }

        }

        var parkerReport = {
            name: 'parkerReport',
            sticky: true,
            views: {
                'main': {
                    templateUrl: 'Upload/ParkerReport',
                    controller: 'ParkerReportController as fileCtrl'
                }
            }
        }


        $stateProvider.state(home);
        $stateProvider.state(overview);
        $stateProvider.state(singleGarage);
        $stateProvider.state(detectedIssues);
        $stateProvider.state(uploadHistory);
        //$stateProvider.state(uploadHistory_invoice);
        $stateProvider.state(garageMap);
        $stateProvider.state(invoice);
        $stateProvider.state(parkerReport);

    }
})();