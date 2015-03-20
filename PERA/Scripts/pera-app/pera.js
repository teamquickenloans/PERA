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
        'ngRoute',
        'pera.teammembers',
        'pera.invoiceTeamMembers'
      ]);
    
    //angular
    //  .module('pera.routes', ['ngRoute']);

    angular
      .module('pera.config', ['ngRoute']);


})();