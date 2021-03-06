﻿(function () {
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
        'angularFileUpload',
        //'mm.foundation',
        'ct.ui.router.extras',
        'angular-datepicker',
        'bzm-date-picker'
        ])
    
    //angular
    //  .module('pera.routes', ['ngRoute']);

    angular
      .module('pera.config', [
          'ui.router',
          'ngRoute',
          'ngResource',
          'ngSanitize',
          'ct.ui.router.extras'
      ])


})();