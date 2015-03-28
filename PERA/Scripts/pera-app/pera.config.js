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
        $stateProvider.state(home);

          /////////////////
         //    Overview //
        /////////////////
        var overview = {
            name: 'overview',
            views: {
                'sidebar': {
                    templateUrl: 'Navigation/Overview',
                    controller: 'SideBarController as garageCtrl'
                }
            }
        }
        $stateProvider.state(overview);

          /////////////////////
         //   Single Garage //
        /////////////////////
        var singleGarage = {
            name: 'singleGarage',
            views:
                {
                    'sidebar': {
                        templateUrl: 'Navigation/SingleGarage',
                        controller: 'SideBarController as sidebar'
                    }
                }
        }
        $stateProvider.state(singleGarage);


          ///////////////////
         //        Garage //
        ///////////////////
        var garage = {
            name: 'garage',
            abstract: true,
            url: '/garage',
            templateUrl: 'Garage/Base'
        }
        $stateProvider.state(garage);

          //////////////////////
         //     Garage > Map //
        //////////////////////
        var mapView = {
            name: 'garage.map',
            url: '',
            views: {
                '': {
                    templateUrl: 'Garage/Map',
                    controller: 'MapController as map'
                },
                'right-nav@': {
                    templateUrl: 'Navigation/TopBar',
                    controller: 'SideBarController as sidebar'
                },
                //view@state -> this targets the "sidebar" view inside the expense template: Expense/Base
                'sidebar@garage': {
                    templateUrl: 'Garage/GarageInfo',
                    controller: ''
                }
            }
        }
        $stateProvider.state(mapView);

          //////////////////////
         //    Garage > Edit //
        //////////////////////
        var garageEdit = {
            name: 'garage.edit',
            url: '',
            views: {
                '': {
                    templateUrl: 'Form/EditGarage',
                    controller: 'EditGarageController as controller'
                },
                'right-nav@': {
                    templateUrl: 'Navigation/TopBar',
                    controller: 'SideBarController as sidebar'
                },
                //view@state -> this targets the "sidebar" view inside the expense template: Expense/Base
                'sidebar@expense': {
                    templateUrl: 'Garage/GarageInfo',
                    controller: ''
                }
            }
        }
        $stateProvider.state(garageEdit);


          //////////////////////
         //          Expense //
        //////////////////////
        var expense = {
            name: 'expense',
            abstract: true,
            url: '/expense',
            templateUrl: 'Expense/Base',
        }
        $stateProvider.state(expense);

          /////////////////////////////////
         //   Expense > Detected Issues //
        /////////////////////////////////
        var detectedIssues = {
            name: 'expense.detectedIssues',
            url: '',
            views: {
                // targets the unnamed ui-view in parent template
                '': {
                    templateUrl: 'Expense/DetectedIssues',
                    controller: 'ExpensesController as expenseCtrl'
                },
                // targets the ui-view='right-nav' in index.html
                'right-nav@': {
                    templateUrl: 'Navigation/TopBar',
                    controller: 'SideBarController as sidebar'
                },
                'sidebar@expense': {
                    templateUrl: 'Navigation/Overview',
                    controller: 'SideBarController as sidebar'
                }
            }
        }
        $stateProvider.state(detectedIssues);
        
          ////////////////////////////////////////////
         //   Expense > Detected Issues > Overview //
        ////////////////////////////////////////////
        var detectedIssuesOverview = {
            name: 'expense.detectedIssues.overview',
            views: {
                'sidebar@expense': {
                    templateUrl: 'Navigation/Overview',
                    controller: 'SideBarController as sidebar'
                }
            }
        }
        $stateProvider.state(detectedIssuesOverview);

        ///////////////////////////////////////////
        //   Expense > Detected Issues > Garage //
        //////////////////////////////////////////
        var detectedIssuesGarage = {
            name: 'expense.detectedIssues.garage',
            views: {
                'sidebar@expense': {
                    templateUrl: 'Navigation/Garage',
                    controller: 'SideBarController as sidebar'
                }
            }
        }
        $stateProvider.state(detectedIssuesGarage);

          /////////////////////////////////
         //    Expense > Upload History //
        /////////////////////////////////
        var uploadHistory = {
            name: 'expense.uploadHistory',
            views: {
                '': {
                    templateUrl: 'Expense/UploadHistory',
                    controller: 'ExpensesController as expenseCtrl'
                }
            }
        }
        $stateProvider.state(uploadHistory);

          /////////////////////////////////
         //           Expense > Invoice //
        /////////////////////////////////
        var invoice = {
            name: 'expense.invoice',
            views: {
                '' : {
                    templateUrl: 'Form/Invoice',
                    controller: 'InvoiceController as fileCtrl'
                }
            }
        }
        $stateProvider.state(invoice);

          /////////////////////////////////
         //     Expense > Parker Report //
        /////////////////////////////////
        var parkerReport = {
            name: 'expense.parkerReport',
            views: {
                '': {
                    templateUrl: 'Form/ParkerReport',
                    controller: 'ParkerReportController as fileCtrl'
                }
            }
        }
        $stateProvider.state(parkerReport);

        var discrepancies = {
            name: 'expense.discrepancies',
            views: {
                '': {
                    templateUrl: 'Form/Discrepancies',
                    controller: 'DiscrepanciesController as controller'
                }
        }
        }
        $stateProvider.state(discrepancies);

        /////////////////////////////////
        //        Expense > Badge Scan //
        /////////////////////////////////
        var badgeScan = {
            name: 'expense.badgeScan',
            views: {
                '': {
                    templateUrl: 'Form/BadgeScan',
                    controller: 'BadgeScanController as fileCtrl'
                }
            }
        }
        $stateProvider.state(badgeScan);


    }
})();