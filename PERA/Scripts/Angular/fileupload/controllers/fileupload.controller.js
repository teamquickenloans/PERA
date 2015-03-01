/**
* FileUpload controller
* @namespace pera.fileupload.controllers
*/
(function () {
    'use strict';

    angular
      .module('pera.fileupload.controllers')
      .controller('FileUploadController', FileUploadController);

    FileUploadController.$inject = ['$scope', '$upload', 'InvoiceForm', 'Garages']; //Here 'Garages' is the Garages Service (pera.garages.service)

    /**
    * @namespace FileUploadController
    */
    function FileUploadController($scope, $upload, InvoiceForm, Garages) {
  
        var vm = this;
        $scope.garages = [];
        $scope.uploads = [];
        $scope.upload = upload;

        $scope.data = 'none'; //the file
        //$scope.file = [];

        vm.month = "";
        vm.invoice = {
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




        /*$scope.$watch('files', function () {
            $scope.upload($scope.files);
        });*/

        /**
        * @name upload
        * @desc Try to upload a file using angular-file-upload
        */
        function upload($files) {
            console.log("in upload fn");
            if ($files && $files.length) {
                console.log("in if");
                for (var i = 0; i < $files.length; i++) {
                    var $file = $files[i];
                    (function (index) {
                        console.log("posting..");
                        $scope.uploads[index] = $upload.upload({
                            url: "./api/files/upload", // webapi url
                            method: "POST",
                            data: { invoice: vm.invoice },
                            file: $file
                        }, uploadProgressFn, uploadSuccessFn)
                    })(i);
                }
            }
        };

        function uploadProgressFn(evt) {
            var progressPercentage = parseInt(100.0 * evt.loaded / evt.total);
            console.log('progress: ' + progressPercentage + '% ' + evt.config.file.name);
        };
        function uploadSuccessFn(data, status, headers, config) {
            console.log("posted!");
            console.log('file ' + config.file.name + 'uploaded. Response: ' + JSON.stringify(data));
        };

        function abortUpload(index) {
            $scope.uploads[index].abort();
        }

        /*
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
        }*/

        
        function activate() {

        }

        function garagesSuccessFn(data, status, headers, config) {
            $scope.garages = data.data;         //this will depend on what the API returns, it may have to change
            //console.log("fileupload Contoller garages success", $scope.garages);
        }

        function garagesErrorFn(data, status, headers, config) {
            Snackbar.error(data.data.error);
        }
    }
})();
