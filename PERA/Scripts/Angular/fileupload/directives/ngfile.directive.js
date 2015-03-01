/**
* ng-file directive
* @namespace pera.fileupload.directives
*/
(function () {
    'use strict';

    /**
    * @desc file directive that can be used anywhere across the pera app
    * @example <div acme-sales-customer-info></div>
    */
    angular
      .module('pera.fileupload')
      .controller('InvoiceFormController', InvoiceFormController);

    InvoiceFormController.$inject = ['$scope', '$http']; //Here 'Garages' is the Garages Service (pera.garages.service)

    /**aaaaaaaaaaaaaaa
    * @namespace InvoiceFormController
    */

.directive("fileread", [function () {
    return {
        scope: {
            fileread: "="
        },
        link: function (scope, element, attributes) {
            element.bind("change", function (changeEvent) {
                var reader = new FileReader();
                reader.onload = function (loadEvent) {
                    scope.$apply(function () {
                        scope.fileread = loadEvent.target.result;
                    });
                }
                reader.readAsDataURL(changeEvent.target.files[0]);
            });
        }
    }
}]);