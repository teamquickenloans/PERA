/**
* ng-file directive
* @namespace pera.fileupload
*/
(function () {
    'use strict';

    /**
    * @desc file directive that can be used anywhere across the pera app
    * @example <div ng-file></div>
    */
    angular
      .module('pera.fileupload')
      .directive('ngFile', ngFile);

    ngFile.$inject = ['$scope', '$http']; //Here 'Garages' is the Garages Service (pera.garages.service)

    /**aaaaaaaaaaaaaaa
    * @namespace InvoiceFormController
    */
    function ngFile(){

        var scope = { 
            fileread: "="
        }

        return {
            scope: scope,
            link: link
        }
        /**
         * @ name link
         */
        function link(scope, element, attributes) {
            element.bind("change", change);

            function change(changeEvent) {
                var reader = new FileReader();
                reader.onload = onload;

                function onload(loadEvent) {
                    scope.$apply(function () {
                        scope.fileread = loadEvent.target.result;
                    });
                }
                reader.readAsDataURL(changeEvent.target.files[0]);
            };
        }
    }
})();