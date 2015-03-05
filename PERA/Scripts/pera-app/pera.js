(function () {
    'use strict';

    angular
      .module('pera', [
        'pera.config',
        //'pera.routes',
        'pera.garages',
        'pera.utils',
        'pera.dashboard',
        'pera.tabs',
        'pera.fileupload',
        'pera.teammembers',
        'pera.invoiceTeamMembers',
        'ngRoute',
        'ui.router'
      ]);
    
    //angular
    //  .module('pera.routes', ['ngRoute']);

    angular
      .module('pera.config', ['ngRoute']);


})();