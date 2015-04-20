/**
* TeamMembers controller
* @namespace pera.teammembers.controllers
*/
(function () {
    'use strict';

    angular
      .module('pera.teammembers.controllers')
      .controller('TeamMembersController', TeamMembersController);

    TeamMembersController.$inject = ['$scope', 'TeamMembers', 'Snackbar', 'SideBar']; //Here 'TeamMembers' is the TeamMembers Service (pera.teammembers.service)

    /**
    * @namespace TeamMembersController
    */
    function TeamMembersController($scope, TeamMembers, Snackbar, SideBar) {
        var vm = this;
        vm.teamMembers = []; //the list of teammembers to be returned

        $scope.currentGarage = null;
        $scope.getCurrent = getCurrent;
        
        $scope.$watch('currentGarage', update);

        //vm.teammembers = TeamMembers.all();
        //console.log(vm.teammembers);

        function getCurrent() {
            $scope.currentGarage = SideBar.getCurrent();
            return SideBar.getCurrent();
        }

        function update() {
            console.log('update');
            if($scope.currentGarage)
                TeamMembers.garage($scope.currentGarage.garageID).then(teammembersSuccessFn, teammembersErrorFn);
        }

        function teammembersSuccessFn(data, status, headers, config) {
            console.log(data.data);
            vm.teamMembers[$scope.currentGarage.garageID] = data.data;         //this will depend on what the API returns, it may have to change
        }

        function teammembersErrorFn(data, status, headers, config) {
            Snackbar.error("Failed to retreive Team Members");

        }


    }
})();