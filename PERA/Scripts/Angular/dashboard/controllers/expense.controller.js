/**
* Expense controller
* @namespace pera.dashboard.controllers
*/
(function () {
    'use strict';

    angular
      .module('pera.dashboard.controllers')
      .controller('ExpenseController', ExpenseController);

    function ExpenseController() {
        this.issues = issues;
    };


    var issues = [
        { garage: "COBO Congress", issue: "Failure to De-allocate", date: 1288323623006 },
        { garage: "N/A", issue: "Un-allocated Team Member", date: 1288323624102 },
        { garage: "Detroit Opera House", issue: "Out-Dated Badge ID", date: 1288383623009 },
        { garage: "Book Cadillac | AT&T Lot", issue: "Duplicate Allocation", date: 1288322433050 },
        { garage: "1001 Woodward", issue: "Failure to De-allocate", date: 1288323738006 },
        { garage: "Howard Lot A", issue: "Invalid Allocation", date: 1288323620000 },
        { garage: "Premier", issue: "Invalid Allocation", date: 1288323476006 },
        { garage: "COBO Rooftop", issue: "Invalid Allocation", date: 1288323622206 },
        { garage: "2 Detroit", issue: "Failure to De-allocate", date: 1288323543006 },
        { garage: "Howard Lot C", issue: "Invalid Allocation", date: 1288323223111 },
        { garage: "COBO Congress", issue: "Failure to De-allocate", date: 1288323623006 },
        { garage: "N/A", issue: "Un-allocated Team Member", date: 1288323624102 },
        { garage: "Detroit Opera House", issue: "Out-Dated Badge ID", date: 1288383623009 },
        { garage: "Book Cadillac | AT&T Lot", issue: "Duplicate Allocation", date: 1288322433050 },
        { garage: "1001 Woodward", issue: "Failure to De-allocate", date: 1288323738006 },
        { garage: "Howard Lot A", issue: "Invalid Allocation", date: 1288323620000 },
        { garage: "Premier", issue: "Invalid Allocation", date: 1288323476006 },
        { garage: "COBO Rooftop", issue: "Invalid Allocation", date: 1288323622206 },
        { garage: "2 Detroit", issue: "Failure to De-allocate", date: 1288323543006 },
        { garage: "Howard Lot C", issue: "Invalid Allocation", date: 1288323223111 },
        { garage: "COBO Congress", issue: "Failure to De-allocate", date: 1288323623006 },
        { garage: "N/A", issue: "Un-allocated Team Member", date: 1288323624102 },
        { garage: "Detroit Opera House", issue: "Out-Dated Badge ID", date: 1288383623009 },
        { garage: "Book Cadillac | AT&T Lot", issue: "Duplicate Allocation", date: 1288322433050 },
        { garage: "1001 Woodward", issue: "Failure to De-allocate", date: 1288323738006 },
        { garage: "Howard Lot A", issue: "Invalid Allocation", date: 1288323620000 },
        { garage: "Premier", issue: "Invalid Allocation", date: 1288323476006 },
        { garage: "COBO Rooftop", issue: "Invalid Allocation", date: 1288323622206 },
        { garage: "2 Detroit", issue: "Failure to De-allocate", date: 1288323543006 },
        { garage: "Howard Lot C", issue: "Invalid Allocation", date: 1288323223111 },
        { garage: "COBO Congress", issue: "Failure to De-allocate", date: 1288323623006 },
        { garage: "N/A", issue: "Un-allocated Team Member", date: 1288323624102 },
        { garage: "Detroit Opera House", issue: "Out-Dated Badge ID", date: 1288383623009 },
        { garage: "Book Cadillac | AT&T Lot", issue: "Duplicate Allocation", date: 1288322433050 },
        { garage: "1001 Woodward", issue: "Failure to De-allocate", date: 1288323738006 },
        { garage: "Howard Lot A", issue: "Invalid Allocation", date: 1288323620000 },
        { garage: "Premier", issue: "Invalid Allocation", date: 1288323476006 },
        { garage: "COBO Rooftop", issue: "Invalid Allocation", date: 1288323622206 },
        { garage: "2 Detroit", issue: "Failure to De-allocate", date: 1288323543006 },
        { garage: "Howard Lot C", issue: "Invalid Allocation", date: 1288323223111 },
        { garage: "COBO Congress", issue: "Failure to De-allocate", date: 1288323623006 },
        { garage: "N/A", issue: "Un-allocated Team Member", date: 1288323624102 },
        { garage: "Detroit Opera House", issue: "Out-Dated Badge ID", date: 1288383623009 },
        { garage: "Book Cadillac | AT&T Lot", issue: "Duplicate Allocation", date: 1288322433050 },
        { garage: "1001 Woodward", issue: "Failure to De-allocate", date: 1288323738006 },
        { garage: "Howard Lot A", issue: "Invalid Allocation", date: 1288323620000 },
        { garage: "Premier", issue: "Invalid Allocation", date: 1288323476006 },
        { garage: "COBO Rooftop", issue: "Invalid Allocation", date: 1288323622206 },
        { garage: "2 Detroit", issue: "Failure to De-allocate", date: 1288323543006 },
        { garage: "Howard Lot C", issue: "Invalid Allocation", date: 1288323223111 },
        { garage: "COBO Congress", issue: "Failure to De-allocate", date: 1288323623006 },
        { garage: "N/A", issue: "Un-allocated Team Member", date: 1288323624102 },
        { garage: "Detroit Opera House", issue: "Out-Dated Badge ID", date: 1288383623009 },
        { garage: "Book Cadillac | AT&T Lot", issue: "Duplicate Allocation", date: 1288322433050 },
        { garage: "1001 Woodward", issue: "Failure to De-allocate", date: 1288323738006 },
        { garage: "Howard Lot A", issue: "Invalid Allocation", date: 1288323620000 },
        { garage: "Premier", issue: "Invalid Allocation", date: 1288323476006 },
        { garage: "COBO Rooftop", issue: "Invalid Allocation", date: 1288323622206 },
        { garage: "2 Detroit", issue: "Failure to De-allocate", date: 1288323543006 },
        { garage: "Howard Lot C", issue: "Invalid Allocation", date: 1288323223111 }
    ];

})();