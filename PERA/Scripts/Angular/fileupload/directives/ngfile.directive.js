/**
* ng-file directive
* @namespace pera.fileupload.directives
*/
(function () {
    'use strict';

    angular
      .module('pera.fileupload')
      .controller('InvoiceFormController', InvoiceFormController);

    InvoiceFormController.$inject = ['$scope', '$http']; //Here 'Garages' is the Garages Service (pera.garages.service)

    /**
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