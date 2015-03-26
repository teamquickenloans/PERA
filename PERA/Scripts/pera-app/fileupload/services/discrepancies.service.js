/**
* Submit
* @namespace pera.fileupload.services
*/
(function () {
    'use strict';

    angular
      .module('pera.fileupload.services')
      .factory('FormSubmit', FormSubmit);

    Submit.$inject = ['$http'];


    function FormSubmit($http) {

        var vm = this;

        var FormSubmit = {
            submit: submit
        };

        return FormSubmit;

        /**
        * @name submit
        * @desc Try to submit a file using ng-file-submit
        */
        function submit(garageID, monthYear) {
            console.log(garageID);
            console.log('posting form..');
            return $http.post('/Discrepancies/Post', { 
                    garageID: garageID,
                    monthYear: monthYear
            });
        }
    }

})();