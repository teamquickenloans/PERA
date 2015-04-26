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
        'pera.export',
        'pera.teammembers',
        'pera.invoiceTeamMembers',
        'pera.sidebar',
        'ui.router',
        'angularFileUpload',
        //'mm.foundation',
        //'ct.ui.router.extras',
        'angular-datepicker',
        'bzm-date-picker',
        'uiGmapgoogle-maps',
        'angularUtils.directives.dirPagination'
        ])
    
    //angular
    //  .module('pera.routes', ['ngRoute']);

    angular
      .module('pera.config', [
          'ui.router',
          'ngRoute',
          'ngResource',
          //'uiGmapgoogle-maps',
          //'uiGmapGoogleMapApiProvider'
          //'ngSanitize',
          //'ct.ui.router.extras'
      ])
    
    //angular.module('pera.config').$inject = ['ui.router', 'ngRoute', 'ngResource'];


})();