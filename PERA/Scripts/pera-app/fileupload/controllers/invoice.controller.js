/**
* Invoice controller
* @namespace pera.fileupload.controllers
*/
(function () {
    'use strict';

    angular
      .module('pera.fileupload.controllers')
      .controller('InvoiceController', InvoiceController);

    InvoiceController.$inject = ['$scope', '$upload', 'Upload', 'Garages', 'Snackbar', '$filter']; //Here 'Garages' is the Garages Service (pera.garages.service)

    /**
    * @namespace InvoiceController
    */
    function InvoiceController($scope, $upload, Upload, Garages, Snackbar, $filter) {
  
        var vm = this;
        vm.files = [];
        vm.counter = 0;
        vm.reports = [{ garageID: 0 }];

        $scope.garages = [];
        $scope.data = 'none'; //the file

        vm.addReport = addReport;
        vm.uploadAll = uploadAll;

        //$scope.file = [];

        vm.invoice = {
            invoiceID: '',
            totalAmountBilled: '',
            dateReceived: '',
            dateUploaded: Date.now(),
            totalLeasedSpots: '',
            validations: '',
            monthYear: ''
        }

        Garages.all().then(garagesSuccessFn, garagesErrorFn);

        function addReport() {
            vm.counter++;
            console.log("add file");
            vm.reports.push({ garageID: 0 });
        }

        function uploadAll()
        {
            var invoice = vm.invoice;
            var date = Date.now();
            invoice.dateUploaded = $filter('date')(date, 'yyyy-MM-dd');
            if (invoice.dateReceived == '') {
                invoice.dateReceived = null;
            }
            if (invoice.totalAmountBilled == '')
            {
                invoice.totalAmountBilled = 0;
            }
            //invoice.monthYear = '01-' + invoice.monthYear 
            console.log("Upload all");
            console.log("uploadAll monthYear:" + vm.invoice.monthYear);
            console.log("uploadAll dateReceived:" + vm.invoice.dateReceived);
            console.log("uploadAll dateUploaded:" + vm.invoice.dateUploaded);
            if (vm.reports && vm.reports.length)
            {
                for (var i = 0; i < vm.reports.length; i++) {
                    console.log(vm.reports[i].file[0].name)
                    vm.files.push(vm.reports[i].file[0])
                }
                Upload.upload(vm.files, vm.invoice, vm.reports, "./api/invoiceparser/upload");
            }

        }

        function garagesSuccessFn(data, status, headers, config) {
            $scope.garages = data.data;         //this will depend on what the API returns, it may have to change
            //console.log("fileupload Contoller garages success", $scope.garages);
        }

        function garagesErrorFn(data, status, headers, config) {
            Snackbar.error(data.data.error);
        }

        /**
        * @name upload
        * @desc Try to upload a file using ng-file-upload
        *//*
        function upload(files) {
            console.log($scope.invoice.invoiceID);
            if (files && files.length) {
                for (var i = 0; i < files.length; i++) {
                    var file = files[i];
                    console.log(file.name);
                    (function (index) {
                        console.log("posting..");
                        $scope.uploads[index] = $upload.upload({
                            url: "./api/files/upload", // webapi url
                            method: "POST",
                            data: { invoice: $scope.invoice },
                            file: file
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
        
        */
    }
})();
