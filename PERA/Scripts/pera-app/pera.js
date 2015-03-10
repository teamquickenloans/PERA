(function () {
    'use strict';

    angular
      .module('pera', [
        'pera.config',
        //'pera.routes',
        'pera.garages',
        'pera.utils',
        'pera.expenses',
        'pera.map',
        'pera.tabs',
        'pera.fileupload',
        'pera.teammembers',
        'pera.invoiceTeamMembers',
        'pera.sidebar',
        'ui.router',
        'ct.ui.router.extras'
      ]);
    
    //angular
    //  .module('pera.routes', ['ngRoute']);

    angular
      .module('pera.config', ['ui.router', 'ngRoute', 'ct.ui.router.extras','ct.ui.router.extras.sticky', 'ct.ui.router.extras.dsr', 'ct.ui.router.extras.examples', 'ct.ui.router.extras.statevis']]);


})();