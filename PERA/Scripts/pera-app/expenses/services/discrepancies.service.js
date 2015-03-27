/**
* FormSubmit
* @namespace pera.expenses.services
*/
(function () {
    'use strict';

    angular
      .module('pera.expenses.services')
      .factory('FormSubmit', FormSubmit);

    FormSubmit.$inject = ['$http'];


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