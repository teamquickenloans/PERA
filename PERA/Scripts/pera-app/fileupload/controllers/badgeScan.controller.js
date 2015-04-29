/**
* Badge Scan controller
* @namespace pera.fileupload.controllers
*/
(function () {
    'use strict';

    angular
      .module('pera.fileupload.controllers')
      .controller('BadgeScanController', BadgeScanController);

    BadgeScanController.$inject = ['$scope', '$upload', 'Upload', 'Snackbar', '$filter' ,'$http', 'Garages'];

    /**
    * @namespace BadgeScanController
    */
    function BadgeScanController($scope, $upload, Upload, Snackbar, $filter, $http, Garages) {

        var vm = this;
        vm.files = [];
        vm.defaultReport = [{ garageID: 0 }];
        vm.reports = angular.copy(vm.defaultReport);
        $scope.garages = [];
        $scope.usage = [];
        $scope.daysInMonth;


        vm.clearForm = clearForm;
        vm.uploadAll = uploadAll;
        vm.findUsage = findUsage;
        vm.addReport = addReport;
        vm.removeReport = removeReport;


        Garages.all().then(garagesSuccess);

        vm.defaultForm = {
            dateReceived: '',
            dateUploaded: Date.now(),
            monthYear: '',
        }

        vm.form = angular.copy(vm.defaultForm);

        function TeamMember(name, usage) {
            this.name = name;
            this.usage = usage;
        }


        function addReport() {
            vm.counter++;
            vm.reports.push({ garageID: 0 });
        }

        function removeReport() {
            vm.counter = vm.counter - 1;
            vm.reports.pop();
        }

        function uploadAll() {
            $(".ball").removeClass("hideMe");
            console.log("uploadAll");
            var form = vm.form;
            var date = Date.now();
            form.dateUploaded = $filter('date')(date, 'yyyy-MM-dd');
            if (form.dateReceived == '') {
                form.dateReceived = null;
            }

            
            console.log("Upload all");
            console.log("uploadAll monthYear:" + vm.form.monthYear);
            console.log("uploadAll dateReceived:" + vm.form.dateReceived);
            console.log("uploadAll dateUploaded:" + vm.form.dateUploaded);
            if (vm.reports && vm.reports.length) {
                for (var i = 0; i < vm.reports.length; i++) {
                    console.log(vm.reports[i].file[0].name)
                    vm.files.push(vm.reports[i].file[0])
                }
                Upload.upload(vm.files, vm.form, vm.reports, "./api/badgescanparser/upload").then(uploadSuccess, uploadFail);
            }

        }

        function uploadSuccess() {
            $(".ball").addClass("hideMe");
            Snackbar.show("Card Activity Report Uploaded Successfully");
            clearForm();
        }

        function clearForm() {
            vm.form = angular.copy(vm.defaultForm);
            //$scope.badgeScanForm.$setPristine();
            vm.files = [];
            vm.reports = angular.copy(vm.defaultReport);
            console.log("clear form");
        }

        function uploadFail() {
            $(".ball").addClass("hideMe");
            Snackbar.error("Card Activity Report upload failed.  Please recheck the formatting of the excel file.");
            clearForm();
        }

        function garagesSuccess(data) {
            $scope.garages = data.data;
        }

        function badgeScanSuccessFn(data, status, headers, config)
        {
            //remove number of days in the month from the end of string
            var temp = data.data.split("}");
            $scope.daysInMonth = temp[1];
            
            //Read data.data as a string. Parse it into an array of arrays(key/value pairs)
            temp = temp[0].replace(/"/g, '');
            temp = temp.replace('{', '');
            var array = temp.split(",");
            
            for (var i = 0; i < array.length; i++)
            {
                array[i] = array[i].split(":");

                $scope.usage.push(new TeamMember(array[i][0], array[i][1]));
            }
        }

        function badgeScanErrorFn(data, status, headers, config) {
            Snackbar.error(data.data.error);
        }

        function findUsage(id) {
            $http.get('/api/BadgeScans/GetNumberOfScans/' + id )
                .then(badgeScanSuccessFn, badgeScanErrorFn); //promise
        }

    }
})();
