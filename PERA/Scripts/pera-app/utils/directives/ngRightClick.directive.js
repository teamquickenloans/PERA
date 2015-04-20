/**
* Date Picker
* @namespace pera.utils.directives
*/
(function () {
    'use strict';

    angular
      .module('pera.utils.directives')
      .directive('ngRightClick', ngRightClick);

    ngRightClick.$inject = ['$parse']
    /**
    * @namespace Snackbar
    */

    function ngRightClick($parse) {
        return function (scope, element, attrs) {
            var fn = $parse(attrs.ngRightClick);
            element.bind('contextmenu', contextmenu);

            function contextmenu(event) {
                scope.$apply(function () {
                    event.preventDefault();
                    fn(scope, { $event: event });
                });
            };
        };
    }
}
)();

