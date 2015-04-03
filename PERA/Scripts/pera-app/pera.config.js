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
    function config($stateProvider, $routeProvider, $urlRouterProvider, uiGmapGoogleMapApiProvider) {

        uiGmapGoogleMapApiProvider.configure({
            key: 'AIzaSyBV-BwHOcVyVW0e8yJa1sAk5GBFtm5YeHM',
            v: '3.17',
            libraries: 'weather,geometry,visualization'
        });

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
                },
                'sidebar-left@expense': {
                    templateUrl: 'Navigation/Expense'
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
                },
                'sidebar-left@expense': {
                    templateUrl: 'Navigation/Expense'
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
                },
                'sidebar-left@expense': {
                    templateUrl: 'Navigation/Expense'
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
                },
                'sidebar-left@expense' : {
                    templateUrl: 'Navigation/Expense'
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
                },
                'sidebar-left@expense': {
                    templateUrl: 'Navigation/Expense'
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
                },
                'sidebar-left@expense': {
                    templateUrl: 'Navigation/Expense'
                }
        }
        }
        $stateProvider.state(discrepancies);


    }
})();