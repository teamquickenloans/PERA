(function () {
    'use strict';

    angular
      .module('pera', [
        'pera.config',
        //'pera.routes',
        //'pera.teamMembers',
        'pera.garages',
        'pera.utils',
        'pera.dashboard',
        'pera.tabs',
        'pera.fileupload',
        'ngRoute',
        'pera.teammembers'
      ]);
    
    //angular
    //  .module('pera.routes', ['ngRoute']);

    angular
      .module('pera.config', ['ngRoute']);


})();