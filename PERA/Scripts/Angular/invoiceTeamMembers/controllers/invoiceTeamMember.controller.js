/**
* InvoiceTeamMembers controller
* @namespace pera.invoiceTeamMembers.controllers
*/
(function () {
    'use strict';

    angular
      .module('pera.invoiceTeamMembers.controllers')
      .controller('InvoiceTeamMembersController', InvoiceTeamMembersController);

    InvoiceTeamMembersController.$inject = ['$scope', 'InvoiceTeamMembers', 'Snackbar']; //Here 'InvoiceTeamMembers' is the InvoiceTeamMembers Service (pera.invoiceTeamMembers.service)

    /**
    * @namespace InvoiceTeamMembersController
    */
    function InvoiceTeamMembersController($scope, InvoiceTeamMembers, Snackbar) {
        var vm = this;
        vm.invoiceTeamMembers = []; //the list of invoiceTeamMembers to be returned



        TeamMembers.all().then(invoiceTeamMembersSuccessFn, invoiceTeamMembersErrorFn);
        //vm.invoiceTeamMembers = InvoiceTeamMembers.all();
        //console.log(vm.invoiceTeamMembers);


        function invoiceTeamMembersSuccessFn(data, status, headers, config) {
            vm.invoiceTeamMembers = data.data;         //this will depend on what the API returns, it may have to change
        }

        function invoiceTeamMembersErrorFn(data, status, headers, config) {
            Snackbar.error(data.data.error);
        }


    }
})();