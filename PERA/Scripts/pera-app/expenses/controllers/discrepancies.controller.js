/**
* Discrepancies controller
* @namespace pera.fileupload.controllers
*/
(function () {
    'use strict';

    angular
      .module('pera.expenses.controllers')
      .controller('DiscrepanciesController', DiscrepanciesController);

    DiscrepanciesController.$inject = ['$scope', 'FormSubmit', 'Garages', 'Snackbar', '$filter','$http']; //Here 'Garages' is the Garages Service (pera.garages.service)

    /**
    * @namespace DiscrepanciesController
    */
    function DiscrepanciesController($scope, FormSubmit, Garages, Snackbar, $filter, $http) {
        var vm = this;
        vm.monthYear = '';
        $scope.garage = { garageID: 0 };
        vm.garageID = 0;
        vm.submit = submit;
        vm.clearForm = clearForm;
        vm.promise;
        $scope.garages = [];
        $scope.duplicate = [];
        $scope.missing = [];
        $scope.extra = [];
        $scope.loaded = false;

        Garages.all().then(garagesSuccessFn);

        function submit() {
            $(".ball").removeClass("hideMe");
            vm.promise = FormSubmit.submit(vm.garageID, vm.monthYear)
            vm.promise.then(submitSuccess, submitFail);
        }

        $scope.$watch('garage', updateGarageID)

        function updateGarageID() {
            vm.garageID = $scope.garage.garageID;
        }

        function submitSuccess(response) {
            $(".ball").addClass("hideMe");
            Snackbar.show('Succesfully identified discrepancies');
            clearForm();
            $scope.duplicate = response.data[0];
            $scope.missing = response.data[2];
            $scope.extra = response.data[1];
            $scope.loaded = true;

            console.log("Submit success: " + response.data);
            var i = 0;
            console.log("Duplicates:" + $scope.duplicate);
            console.log("Missing:" + $scope.missing);
            

            $scope.downloadfile = function(downloadPath)
            {
                window.open("output.xlsx", '_blank', '');
            }
            
        }

        function submitFail()
        {
            $(".ball").addClass("hideMe");
            Snackbar.show('Error identifying discrepancies')
            //clearForm();
        }

        function clearForm() {
            $scope.discrepanciesForm.$setPristine();
            vm.monthYear = '';
            vm.garageID = 0;
        }

        function garagesSuccessFn(data, status, headers, config) {
            $scope.garages = data.data;
        }

        $scope.getthefile = function () {
            $http({
                method: 'GET',
                cache: false,
                url: 'api/Values/GetFile',
                headers: {
                    'Content-Type': 'application/json; charset=utf-8'
                }
            }).success(function (data, status) {
                window.open(data, '_blank', '');
                console.log(data) // displays text data if the file is a text file, binary if it's an image            
                // now what should I write here to download the file I receive from the WebAPI method.
            }).error(function (data, status) {

            });
            window.open('api/Values/GetFile', '_blank', '');
        }
        
    }
})();