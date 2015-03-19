(function () {
    'use strict';

    angular
      .module('pera.fileupload', [
        'pera.fileupload.controllers',
        'pera.fileupload.services'
      ]);

    angular
      .module('pera.fileupload.controllers', ['angularFileUpload']);

    angular
      .module('pera.fileupload.services', []);

    angular
      .module('pera.fileupload.directives', []);


})();