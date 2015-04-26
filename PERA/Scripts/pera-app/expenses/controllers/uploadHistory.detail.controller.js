/**
* upload history detail controller
* @namespace pera.expenses.controllers
*/
(function () {
    'use strict';

    angular
      .module('pera.expenses.controllers')
      .controller('UploadHistoryDetailController', UploadHistoryDetailController);
    UploadHistoryDetailController.$inject = ['$scope', 'Snackbar', '$stateParams', 'TeamMembers', 'InvoiceAPRs', 'QLAPRs', 'BadgeScanReports']

    function UploadHistoryDetailController($scope, Snackbar, $stateParams, TeamMembers, InvoiceAPRs, QLAPRs, BadgeScanReports) {

        console.log($stateParams.reportId);
        $scope.report;



        if ($stateParams.typeId == 1) {
            TeamMembers.invoice($stateParams.reportId).then(success, error);
            InvoiceAPRs.get($stateParams.reportId).then(reportSuccess, reportError);

        }
        else if ($stateParams.typeId == 2) {
            TeamMembers.qlreport($stateParams.reportId).then(success, error);
            QLAPRs.get($stateParams.reportId).then(reportSuccess, reportError);
        }

        else if ($stateParams.typeId == 3) {
            BadgeScanReports.scans($stateParams.reportId).then(success, error);
            BadgeScanReports.get($stateParams.reportId).then(reportSuccess, reportError);
        }

        function success(data) {
            $scope.teamMembers = data.data;
        }

        function error(data) {
            Snackbar.error("Team Members could not be retrieved");
        }

        function invoiceSuccess(data) {
            $scope.report = data.data;
        }

        function reportSuccess(data) {
            $scope.report = data.data;
        }

        function reportError(data) {
            Snackbar.error("failed to retrieve report");
        }
    }
})()