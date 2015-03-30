/**
* Expenses Module
* @namespace pera.expenses
*/

(function () {
    'use strict';

    angular
      .module('pera.expenses', [
        'pera.expenses.controllers',
        'pera.expenses.services'
      ]);

    angular
      .module('pera.expenses.controllers', []);

    angular
        .module('pera.expenses.services', []);
})();