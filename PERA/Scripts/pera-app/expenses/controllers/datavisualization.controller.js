
(function () {
    'use strict';

    angular
      .module('pera.expenses.controllers')
      .controller('DataVisualizationController', UploadHistoryController);
    DataVisualizationController.$inject = ['$scope', 'Snackbar', 'Invoices', 'InvoiceAPRs']

    function DataVisualizationController($scope, Snackbar, Invoices, InvoiceAPRs) {
        var vm = this;
        $scope.invoices = [];
        $scope.invoiceAPRs = [];

        vm.issues = issues;
        vm.uploads = uploads;

        Invoices.all().then(invoicesSuccess, invoicesError);
        InvoiceAPRs.all().then(invoiceAPRsSuccess, invoiceAPRsError);


        function invoicesSuccess(data, status, headers, config) {
            $scope.invoices = data.data;
        }

        function invoicesError(data, status, headers, config) {
            Snackbar.error("Failed to retrieve invoices");
        }

        function invoiceAPRsSuccess(data, status, headers, config) {
            console.log("retrieved aprs")
            $scope.invoiceAPRs = data.data;
        }

        function invoiceAPRsError(data, status, headers, config) {
        }
    };