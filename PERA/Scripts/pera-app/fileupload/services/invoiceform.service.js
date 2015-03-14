/**
* Invoice Form
* @namespace pera.fileupload.services
*/
(function () {
    'use strict';

    angular
      .module('pera.fileupload.services')
      .factory('InvoiceForm', InvoiceForm);

    InvoiceForm.$inject = ['$http','$q', 'Snackbar'];

    /**
    * @namespace InvoiceForm
    * @returns {Factory}
    */
    function InvoiceForm($http, $q, Snackbar) {   //this is just linking up the functions to variable names and returning them

        var InvoiceForm = {
            submit: submit
        };

        return InvoiceForm;

        ////////////////////

        /**
        * @name submit
        * @desc Post the form
        * @returns {Promise}
        * @memberOf pera.fileupload.services.InvoiceForm
        */
        function submit(invoice, file, uploadUrl) {
            var fd = new FormData();
            fd.append('file', file);
            fd.append('invoice', angular.toJson(invoice));
            return $http.post(uploadUrl, fd, {
                transformRequest: angular.identity,
                headers: { 'Content-Type': undefined }
            }).then(submitSuccess, submitError);
        }
        /**
        * @name submitSuccess
        * @desc Indicate success
        * @returns {Promise}
        * @memberOf pera.fileupload.services.InvoiceForm
        */
        function submitSuccess(data) {
            console.log("form submitted");
        }

        /**
        * @name submitError
        * @desc Pop up error message
        * @returns {Promise}
        * @memberOf pera.fileupload.services.InvoiceForm
        */
        function submitError(data) {
            Snackbar.error(data.data.error);
        }
    }
})();


