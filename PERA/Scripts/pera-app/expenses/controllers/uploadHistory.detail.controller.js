/**
* upload history detail controller
* @namespace pera.expenses.controllers
*/
(function () {
    'use strict';

    angular
      .module('pera.expenses.controllers')
      .controller('UploadHistoryDetailController', UploadHistoryDetailController);
    UploadHistoryDetailController.$inject = ['$scope', 'Snackbar', '$stateParams', 'TeamMembers']

    function UploadHistoryDetailController($scope, Snackbar, $stateParams, TeamMembers) {

        console.log($stateParams.reportId);

        if ($stateParams.typeId == 1)
            getTeamMembers();

        else if ($stateParams.typeId == 2)
            TeamMembers.qlreport($stateParams.reportId).then(success, error);

        function getTeamMembers() {
            TeamMembers.invoice($stateParams.reportId).then(success, error);
        }

        function success(data) {
            $scope.teamMembers = data.data;
        }

        function error(data) {
            Snackbar.error("Team Members could not be retrieved");
        }
    }
})()