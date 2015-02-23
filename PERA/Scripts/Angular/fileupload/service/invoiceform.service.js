/**
* Garages
* @namespace pera.garages.services
*/
(function () {
    'use strict';

    angular
      .module('pera.fileupload.services')
      .factory('InvoiceSubmission', InvoiceSubmission);

    SubmitInvoice.$inject = ['$http','$q'];

    /**
    * @namespace InvoiceSubmission
    * @returns {Factory}
    */
    function InvoiceSubmission($http, $q) {   //this is just linking up the functions to variable names and returning them
        var InvoiceSubmission = {
            submit: submit
        };

        return InvoiceSubmission;

        ////////////////////

        /**
        * @name submit
        * @desc Post the form
        * @returns {Promise}
        * @memberOf pera.fileupload.services.InvoiceSubmission
        */
        function submit(file, uploadUrl) {
            var fd = new FormData();
            fd.append('file', file);
            return $http.post(uploadUrl, fd, {
                transformRequest: angular.identity,
                headers: { 'Content-Type': undefined }
            })
            .success(function () {

            })
            .error(function () {

            });
        }

    }
})();





var LoginFactory = function ($http, $q) {
    return function (emailAddress, password, rememberMe) {

        var deferredObject = $q.defer();

        $http.post(
            '/Account/Login', {
                Email: emailAddress,
                Password: password,
                RememberMe: rememberMe
            }
        ).
        success(function (data) {
            if (data == "True") {
                deferredObject.resolve({ success: true });
            } else {
                deferredObject.resolve({ success: false });
            }
        }).
        error(function () {
            deferredObject.resolve({ success: false });
        });

        return deferredObject.promise;
    }
}

LoginFactory.$inject = ['$http', '$q'];