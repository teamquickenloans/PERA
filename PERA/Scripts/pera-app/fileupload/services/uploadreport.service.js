/**
* UploadReport
* @namespace pera.fileupload.services
*/
(function () {
    'use strict';

    angular
      .module('pera.fileupload.services')
      .factory('UploadReport', UploadReport);

    UploadReport.$inject = ['$upload', 'Snackbar'];


    function UploadReport($upload, Snackbar) {

        var vm = this;
        vm.uploads = [];

        var UploadReport = {
            upload: upload
        };

        return UploadReport;

        /**
        * @name upload
        * @desc Try to upload a file using ng-file-upload
        */
        function upload(files, invoice, reports) {
            console.log(invoice.monthYear);
            if (files && files.length) {
                for (var i = 0; i < files.length; i++) {
                    var file = files[i];
                    console.log(file.name);
                    (function (index) {
                        console.log("posting..");
                        vm.uploads[index] = $upload.upload({
                            url: "./api/files/upload", // webapi url
                            method: "POST",
                            data: form,
                            fields: { garageID: reports[i].garageID },
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
    }

})();