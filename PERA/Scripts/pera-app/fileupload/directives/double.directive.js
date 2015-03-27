/**
* double directive
* @namespace pera.fileupload.directives
*/
(function () {
    'use strict';

    /**
    * @desc file directive that can be used anywhere across the pera app
    * @example <div ng-file></div>
    */
    angular
      .module('pera.fileupload')
      .directive('double', double);

    double.$inject = ['$scope', '$http']; //Here 'Garages' is the Garages Service (pera.garages.service)

    /**aaaaaaaaaaaaaaa
    * @namespace InvoiceFormController
    */
    function double(){
    }
}
