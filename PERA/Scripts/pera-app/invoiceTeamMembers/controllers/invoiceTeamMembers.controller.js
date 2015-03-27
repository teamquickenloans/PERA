/**
* ParkerReportTeamMembers controller
* @namespace pera.invoiceTeamMembers.controllers
*/
(function () {
    'use strict';

    angular
      .module('pera.invoiceTeamMembers.controllers')
      .controller('ParkerReportTeamMembersController', ParkerReportTeamMembersController);

    ParkerReportTeamMembersController.$inject = ['$scope', 'ParkerReportTeamMembers', 'Snackbar']; //Here 'ParkerReportTeamMembers' is the ParkerReportTeamMembers Service (pera.invoiceTeamMembers.service)

    /**
    * @namespace ParkerReportTeamMembersController
    */
    function ParkerReportTeamMembersController($scope, ParkerReportTeamMembers, Snackbar) {
        var vm = this;
        vm.invoiceTeamMembers = []; //the list of invoiceTeamMembers to be returned


        ParkerReportTeamMembers.all().then(invoiceTeamMembersSuccessFn, invoiceTeamMembersErrorFn);
        //vm.invoiceTeamMembers = ParkerReportTeamMembers.all();
        //console.log(vm.invoiceTeamMembers);


        function invoiceTeamMembersSuccessFn(data, status, headers, config) {
            vm.invoiceTeamMembers = data.data;         //this will depend on what the API returns, it may have to change
        }

        function invoiceTeamMembersErrorFn(data, status, headers, config) {
            Snackbar.error(data.data.error);
        }


    }
})();