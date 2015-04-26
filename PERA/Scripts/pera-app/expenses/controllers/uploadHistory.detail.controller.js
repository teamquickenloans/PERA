/**
* upload history detail controller
* @namespace pera.expenses.controllers
*/
(function () {
    'use strict';

    angular
      .module('pera.expenses.controllers')
      .controller('UploadHistoryDetailController', UploadHistoryDetailController);
    UploadHistoryDetailController.$inject = ['$scope', 'Snackbar', '$stateParams', 'TeamMembers', 'InvoiceAPRs', 'QLAPRs', 'BadgeScanReports', 'Garages', '$filter']

    function UploadHistoryDetailController($scope, Snackbar, $stateParams, TeamMembers, InvoiceAPRs, QLAPRs, BadgeScanReports, Garages, $filter) {

        $scope.report;
        $scope.title;
        $scope.garageName;


        if ($stateParams.typeId == 1) {
            TeamMembers.invoice($stateParams.reportId).then(success, error);
            InvoiceAPRs.get($stateParams.reportId).then(invoiceSuccess, reportError);
        }
        else if ($stateParams.typeId == 2) {
            TeamMembers.qlreport($stateParams.reportId).then(success, error);
            QLAPRs.get($stateParams.reportId).then(reportSuccess, reportError);
        }

        else if ($stateParams.typeId == 3) {
            BadgeScanReports.scans($stateParams.reportId).then(success, error);
            BadgeScanReports.get($stateParams.reportId).then(badgeSuccess, reportError);
        }

        function success(data) {
            $scope.teamMembers = data.data;
        }

        function error(data) {
            Snackbar.error("Team Members could not be retrieved");
        }

        function invoiceSuccess(data) {
            $scope.report = data.data;
            console.log('$scope.report: ' + $scope.report);
            Garages.get($scope.report.garageID).then(invoiceGarage);
        }

        function reportSuccess(data) {
            $scope.report = data.data;
            Garages.get($scope.report.garageID).then(garage);
        }

        function badgeSuccess(data) {
            $scope.report = data.data;
            Garages.get($scope.report.garageID).then(badgeGarage);
        }

       function badgeGarage(data) {
            $scope.garage = data.data;
            $scope.garageName = $scope.garage.name;
            $scope.title = " Badge Activity Parker Report ";
            $scope.title += "for " + $filter('date')($scope.report.monthYear, 'MMMM yyyy');
        }

        function reportError(data) {
            Snackbar.error("failed to retrieve report");
        }

        function invoiceGarage(data) {
            $scope.garage = data.data;
            $scope.garageName = $scope.garage.name;
            $scope.title = " Invoice Active Parker Report ";
            $scope.title += "for " + $filter('date')($scope.report.monthYear, 'MMMM yyyy');
        }

        function garage(data) {
            $scope.garage = data.data;
            $scope.garageName = $scope.garage.name;
            $scope.title = " Quicken Loans Active Parker Report ";
            $scope.title += "for " + $filter('date')($scope.report.monthYear, 'MMMM yyyy');

        }
    }
})()