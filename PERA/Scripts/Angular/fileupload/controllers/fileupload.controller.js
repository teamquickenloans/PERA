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
        //console.log($scope.garages);

        $scope.$watch('files', function () {
            $scope.upload($scope.files);
        });

        
        function upload(files) {
            if (files && files.length) {
                for (var i = 0; i < files.length; i++) {
                    var file = files[i];
                    $upload.upload({
                        url: 'https://angular-file-upload-cors-srv.appspot.com/upload',
                        fields: {
                            'invoice': $scope.invoice
                        },
                        file: file
                    }).progress(function (evt) {
                        var progressPercentage = parseInt(100.0 * evt.loaded / evt.total);
                        console.log('progress: ' + progressPercentage + '% ' +
                                    evt.config.file.name);
                    }).success(function (data, status, headers, config) {
                        console.log('file ' + config.file.name + 'uploaded. Response: ' +
                                    JSON.stringify(data));
                    });
                }
            }
        };

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
