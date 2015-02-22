/**
* FileUpload controller
* @namespace pera.fileupload.controllers
*/
(function () {
    'use strict';

    angular
      .module('pera.fileupload.controllers')
      .controller('FileUploadController', FileUploadController);

    FileUploadController.$inject = ['$scope','$http']; //Here 'Garages' is the Garages Service (pera.garages.service)

    /**
    * @namespace FileUploadController
    */
    function FileUploadController($scope,$http) {
  
        $scope.data = 'none'; //the list of garages to be returned
        $scope.upload = upload;

        function upload() {
            var file = document.getElementById('file').files[0]
            var reader = new FileReader();
            reader.onloadend = setData;

            function setData(e) {
                $scope.data = e.target.result;
            }

            reader.readAsBinaryString(file);
            submitfile(file);
        }
        function submitfile(file) {
            $http.post("IngestInvoice/AddFile", file).success(function (data) {
                alert("ok");});
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
