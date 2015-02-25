/**
* FileUpload controller
* @namespace pera.fileupload.controllers
*/
(function () {
    'use strict';

    angular
      .module('pera.fileupload.controllers')
      .controller('FileUploadController', FileUploadController);

    FileUploadController.$inject = ['$scope','InvoiceForm', 'Garages']; //Here 'Garages' is the Garages Service (pera.garages.service)

    /**
    * @namespace FileUploadController
    */
    function FileUploadController($scope, InvoiceForm, Garages) {
  
        var vm = this;
        $scope.garages = [];

        $scope.data = 'none'; //the file
        $scope.upload = upload;
        $scope.file = [];
        $scope.month = "";
        $scope.invoice = {
            invoiceID: '',
            garageID: '',
            totalAmountBilled: '',
            dateReceived: '',
            billingStartDate: '',
            billingEndDate: '',
            numberOfLeasedSpots: '',
            numberOfValidations: ''
        }

        Garages.all().then(garagesSuccessFn, garagesErrorFn);
        console.log($scope.garages);

        function upload() {
            $scope.file = document.getElementById('file').files[0]
            var reader = new FileReader();
            reader.onloadend = setData;

            function setData(e) {
                $scope.data = e.target.result;
            }
            //console.log($scope.data);
            var fileData = reader.readAsBinaryString($scope.file);
            //console.log($scope.data);
            //submitFile(fileData)
            InvoiceForm.submit($scope.invoice, $scope.file, "IngestInvoice/AddFile");
        }

        
        function activate() {

        }

        function garagesSuccessFn(data, status, headers, config) {
            $scope.garages = data.data;         //this will depend on what the API returns, it may have to change
            //console.log("fileupload Contoller garages success", $scope.garages);
        }

        function garagesErrorFn(data, status, headers, config) {
            Snackbar.error(data.data.error);
        }


        /*function activate() {
            Garages.all().then(garagesSuccessFn, garagesErrorFn);
        }

        function garagesSuccessFn(data, status, headers, config) {
            vm.garages = data.data;         //this will depend on what the API returns, it may have to change
        }

        function garagesErrorFn(data, status, headers, config) {
            Snackbar.error(data.data.error);
        }*/
    }
})();
