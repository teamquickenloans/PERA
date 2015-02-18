(function () {
    'use strict';

    angular
      .module('pera', [
        'pera.config',
        //'pera.routes',
        //'pera.teamMembers',
        'pera.garages',
        'pera.utils'
      ]);
    
    angular
      .module('thinkster.routes', ['ngRoute']);

    angular
      .module('thinkster.config', []);
})();