/**
* TeamMembers controller
* @namespace pera.teammembers.controllers
*/
(function () {
    'use strict';

    angular
      .module('pera.teammembers.controllers')
      .controller('TeamMembersController', TeamMembersController);

    TeamMembersController.$inject = ['$scope', 'TeamMembers', 'Snackbar']; //Here 'TeamMembers' is the TeamMembers Service (pera.teammembers.service)

    /**
    * @namespace TeamMembersController
    */
    function TeamMembersController($scope, TeamMembers, Snackbar) {
        var vm = this;
        vm.teammembers = []; //the list of teammembers to be returned



        TeamMembers.all().then(teammembersSuccessFn, teammembersErrorFn);
        //vm.teammembers = TeamMembers.all();
        //console.log(vm.teammembers);


        function teammembersSuccessFn(data, status, headers, config) {
            vm.teammembers = data.data;         //this will depend on what the API returns, it may have to change
        }

        function teammembersErrorFn(data, status, headers, config) {
            Snackbar.error("Failed to retreive Team Members");
        }


    }
})();