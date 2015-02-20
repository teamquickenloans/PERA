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
        'pera.tabs'
      ]);
    
    //angular
    //  .module('pera.routes', ['ngRoute']);

    angular
      .module('pera.config',[]);
})();