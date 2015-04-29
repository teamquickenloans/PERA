/**
* upload history controller
* @namespace pera.expenses.controllers
*/
(function () {
    'use strict';

    angular
      .module('pera.expenses.controllers')
      .controller('UploadHistoryController', UploadHistoryController);
    UploadHistoryController.$inject = ['$scope', 'Snackbar', 'Invoices', 'InvoiceAPRs', 'QLAPRs', 'Garages', 'BadgeScanReports']

    function UploadHistoryController($scope, Snackbar, Invoices, InvoiceAPRs, QLAPRs, Garages, BadgeScanReports) {
        var vm = this;
        $scope.invoices = [];
        $scope.invoiceAPRs = {};
        $scope.garages = {};
        $scope.badgeReports = {};

        $scope.getReports = getReports;

        Invoices.all().then(invoicesSuccess, invoicesError);
        QLAPRs.all().then(qlSuccess);
        Garages.all().then(garagesSuccess);
        BadgeScanReports.all().then(badgeSuccess);

        function invoicesSuccess(data, status, headers, config) {
            $scope.invoices = data.data;
            for (var i = 0; i < $scope.invoices.length; i++) {
                var invoiceID = $scope.invoices[i].invoiceID;
                getReports(invoiceID).then(invoiceAPRsSuccess);
            }
        }

        function invoicesError(data, status, headers, config) {
            Snackbar.error("Failed to retrieve invoices");
        }

        function getReports(invoiceID) {
            return InvoiceAPRs.reports(invoiceID);
        }

        function qlSuccess(data, status, headers, config) {
            $scope.qlAPRs = data.data;
        }

        function garagesSuccess(data, status, headers, config) {
            for (var i = 0; i < data.data.length; i++) {
                $scope.garages[data.data[i].garageID] = data.data[i];
            }
        }


        function invoiceAPRsSuccess(data, status, headers, config) {
            $scope.invoiceAPRs[data.data[0].invoiceID] = data.data;
        }

        function invoiceAPRsError(data, status, headers, config) {
            ;
        }

        function badgeSuccess(data) {
            console.log(data.data);
            $scope.badgeReports = data.data;
        }

        function deleteInvoice(invoiceID) {

        }
    };
})();