/**
* Date Picker
* @namespace pera.utils.directives
*/
(function () {
    'use strict';

    angular
      .module('pera.utils.directives')
      .directive('ngDatepicker', datePicker);

    datePicker.$inject = ['$scope', '$state']
    /**
    * @namespace Snackbar
    */
    function datePicker() {
        console.log('inside date picker!');
        
        return {
            restrict: 'A',
            link: link 
        }

        function link() {
            scope.$on('$stateChangeSuccess', stateChangeSuccessFn);
        }
            
       function stateChangeSuccessFn (event, toState, toParams, fromState) {
                event.targetScope.$watch('$viewContentLoaded', init);
       }
      
        function init (scope, element, attrs) {
            $(element).fdatepicker()
        }

    }
})();