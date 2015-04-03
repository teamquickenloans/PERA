/**
* ParkerReport controller
* @namespace pera.parkerreport.controllers
*/
(function () {
    'use strict';

    angular
      .module('pera.fileupload.controllers')
      .controller('ParkerReportController', ParkerReportController);

    ParkerReportController.$inject = ['$scope', '$upload', 'Upload', 'Garages', 'Snackbar', '$filter']; //Here 'Garages' is the Garages Service (pera.garages.service)

    /**
    * @namespace ParkerReportController
    */
    function ParkerReportController($scope, $upload, Upload, Garages, Snackbar, $filter) {

        var vm = this;
        vm.files = [];
        vm.defaultReport = [{ garageID: 0 }];
        vm.reports = angular.copy(vm.defaultReport);
        $scope.garages = [];

        vm.uploadAll = uploadAll;
        vm.clearForm = clearForm;
        vm.addReport = addReport;

        vm.defaultForm = {
            dateReceived: '',
            dateUploaded: Date.now(),
            monthYear: '',
        };
        vm.form = angular.copy(vm.defaultForm);

        Garages.all().then(garagesSuccessFn, garagesErrorFn);

        function addReport() {
            vm.counter++;
            vm.reports.push({ garageID: 0 });
        }

        function uploadAll() {
            console.log("uploadAll");
            var form = vm.form;
            var date = Date.now();
            form.dateUploaded = $filter('date')(date, 'yyyy-MM-dd');
            if (form.dateReceived == '') {
                form.dateReceived = null;
            }

            //invoice.monthYear = '01-' + invoice.monthYear 
            console.log("Upload all");
            console.log("uploadAll monthYear:" + vm.form.monthYear);
            console.log("uploadAll dateReceived:" + vm.form.dateReceived);
            console.log("uploadAll dateUploaded:" + vm.form.dateUploaded);
            if (vm.reports && vm.reports.length) {
                for (var i = 0; i < vm.reports.length; i++) {
                    console.log(vm.reports[i].file[0].name)
                    vm.files.push(vm.reports[i].file[0])
                }
                Upload.upload(vm.files, vm.form, vm.reports, "./api/parkerreportparser/upload")
                    .then(uploadSuccess,uploadFail);
            }

        }
        function uploadSuccess() {
            Snackbar.show("Invoice Uploaded Successfully");
            clearForm();
        }

        function clearForm() {
            vm.form = angular.copy(vm.defaultForm);
            $scope.parkerReportForm.$setPristine();
            vm.files = [];
            vm.reports = angular.copy(vm.defaultReport);
            console.log("clear form");
        }
        function uploadFail() {
            Snackbar.error("Invoice upload failed.  Please recheck the formatting of the excel file.");
            clearForm();
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