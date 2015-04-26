(function () {
    'use strict';

    angular
      .module('pera.config')
      .config(config);

    //config.$inject = ['ui.routerProvider','ngRoute','ngResource'];
    //config.$inject = ['$routeProvider', '$urlRouterProvider', '$stateProvider', ];

    /**
    * @name config
    * @desc Enable HTML5 routing
    */
    function config($stateProvider, $routeProvider, $urlRouterProvider) {

        /*uiGmapGoogleMapApiProvider.configure({
            key: 'AIzaSyBV-BwHOcVyVW0e8yJa1sAk5GBFtm5YeHM',
            v: '3.17',
            libraries: 'weather,geometry,visualization'
        });*/

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








        // ====================== Garage ==============================


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
                    templateUrl: 'Navigation/MapTopBar',
                    controller: 'SideBarController as sidebar'
                },
                //view@state -> this targets the "sidebar" view inside the expense template: Expense/Base
                'sidebar@garage': {
                    templateUrl: 'Garage/GarageInfo',
                    controller: 'TeamMembersController as teamCtrl'
                },

            }
        }
        $stateProvider.state(mapView);

          /////////////////////////////
         //   Garage > Map > Garage //
        /////////////////////////////
        var mapGarage = {
            name: 'garage.map.garage',
            views: {
                'sidebartop@garage': {
                    templateUrl: 'Navigation/MapGarage',
                    controller: 'SideBarController as sidebar'
                }
            }
        }
        $stateProvider.state(mapGarage);

        /////////////////////////////
        //   Garage > Map > Edit   //
        ////////////////////////////
        var editGarage = {
            name: 'garage.map.edit',
            views: {
                'modalView@': {
                    templateUrl: 'Form/EditGarage',
                    controller: 'EditGarageController as controller'
                }
            }
        }
        $stateProvider.state(editGarage);


        ////////////////////////////
        //    Garage > Map > Add //
        //////////////////////////
        var garageAdd = {
            name: 'garage.map.add',
            views: {
                'modalView@': {
                    templateUrl: 'Form/EditGarage',
                    controller: 'AddGarageController as controller'
                }
            }
        }
        $stateProvider.state(garageAdd);



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






        // ====================== Upload ==============================


          //////////////////////
         //           Upload //
        //////////////////////
        var upload = {
            name: 'upload',
            abstract: true,
            url: '/upload',
            templateUrl: 'Upload/Base',
        }
        $stateProvider.state(upload);

          ///////////////////////
         // Upload > History  //
        ///////////////////////
        var uploadHistory = {
            name: 'upload.history',
            views: {
                '': {
                    templateUrl: 'Upload/History',
                    controller: 'UploadHistoryController as controller'
                },
                'sidebar-left@upload': {
                    templateUrl: 'Navigation/Upload'
                }
            }
        }
        $stateProvider.state(uploadHistory);

        /////////////////////////////////
        //  Upload > History > Detail //
        ///////////////////////////////
        var uploadHistoryDetail = {
            name: 'upload.history.detail',
            url: '/invoice/:invoiceId',
            views: {
                '@upload': {    //the blank view in the 'upload' template
                    templateUrl: 'Upload/HistoryDetail',
                    controller: 'UploadHistoryDetailController as controller'
                },
                'sidebar-left@upload': {
                    templateUrl: 'Navigation/Upload'
                }
            }
        }
        $stateProvider.state(uploadHistoryDetail);


          /////////////////////////////////
         //            Upload > Invoice //
        /////////////////////////////////
        var invoice = {
            name: 'upload.invoice',
            views: {
                '' : {
                    templateUrl: 'Form/Invoice',
                    controller: 'InvoiceController as fileCtrl'
                },
                'sidebar-left@upload': {
                    templateUrl: 'Navigation/Upload'
                }
            }
        }
        $stateProvider.state(invoice);

          /////////////////////////////////
         //      Upload > Parker Report //
        /////////////////////////////////
        var parkerReport = {
            name: 'upload.parkerReport',
            views: {
                '': {
                    templateUrl: 'Form/ParkerReport',
                    controller: 'ParkerReportController as fileCtrl'
                },
                'sidebar-left@upload': {
                    templateUrl: 'Navigation/Upload'
                }
            }
        }
        $stateProvider.state(parkerReport);

          /////////////////////////////////
         //         Upload > Badge Scan //
        /////////////////////////////////
        var badgeScan = {
            name: 'upload.badgeScan',
            views: {
                '': {
                    templateUrl: 'Form/BadgeScan',
                    controller: 'BadgeScanController as fileCtrl'
                },
                'sidebar-left@upload': {
                    templateUrl: 'Navigation/Upload'
                }
            }
        }
        $stateProvider.state(badgeScan);












        // ====================== Expense ==============================


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
         //     Expense > Discrepancies //
        /////////////////////////////////
        var discrepancies = {
            name: 'expense.discrepancies',
            views: {
                '': {
                    templateUrl: 'Form/Discrepancies',
                    controller: 'DiscrepanciesController as controller'
                },
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
        $stateProvider.state(discrepancies);

          /////////////////////////////////////
         //    Expense > Data Visualization //
        /////////////////////////////////////
        var datavisualization = {
            name: 'expense.datavisualization',
            views: {
                '': {
                    templateUrl: 'Expense/DataVisualization',
                    controller: 'DataVisualizationController as controller'
                },
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
        $stateProvider.state(datavisualization);

          ////////////////////////////////////////////
         //     Expense > Discrepancies > Overview //
        ////////////////////////////////////////////
        var discrepanciesOverview = {
            name: 'expense.discrepancies.overview',
            views: {
                'sidebar@expense': {
                    templateUrl: 'Navigation/Overview',
                    controller: 'SideBarController as sidebar'
                }
            }
        }
        $stateProvider.state(discrepanciesOverview);

          //////////////////////////////////////////
         //     Expense > Discrepancies > Garage //
        //////////////////////////////////////////
        var discrepanciesGarage = {
            name: 'expense.discrepancies.garage',
            views: {
                'sidebar@expense': {
                    templateUrl: 'Navigation/Garage',
                    controller: 'SideBarController as sidebar'
                }
            }
        }
        $stateProvider.state(discrepanciesGarage);



    }
})();