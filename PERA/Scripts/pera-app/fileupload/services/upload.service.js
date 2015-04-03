/**
* Upload
* @namespace pera.fileupload.services
*/
(function () {
    'use strict';

    angular
      .module('pera.fileupload.services')
      .factory('Upload', Upload);

    Upload.$inject = ['$upload', 'Snackbar'];


    function Upload($upload, Snackbar) {

        var vm = this;
        vm.uploads = [];

        var Upload = {
            upload: upload
        };

        return Upload;

        /**
        * @name upload
        * @desc Try to upload a file using ng-file-upload
        */
        function upload(files, form, reports, url) {
            if (files && files.length) {
                for (var i = 0; i < files.length; i++) {
                    var file = files[i];
                    console.log(file.name);
                    (function (index) {
                        console.log("posting..");
                        vm.uploads[index] = $upload.upload({
                            url: url, // webapi url
                            method: "POST",
                            data: form,
                            fields: { garageID: reports[i].garageID },
                            file: file
                        }, uploadProgressFn, uploadSuccessFn)
                    })(i);
                    return vm.uploads[0];
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