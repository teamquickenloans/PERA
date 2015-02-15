(function () {
    'use strict';

    angular
      .module('pera', [
        'pera.config',
        //'pera.routes',
        //'pera.teamMembers',
        'pera.garages'
      ]);
    
    angular
      .module('thinkster.routes', ['ngRoute']);

    angular
      .module('thinkster.config', []);
})();