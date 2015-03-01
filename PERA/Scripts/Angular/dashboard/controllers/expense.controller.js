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
        { garage: "COBO Congress", issue: "Duplicate Parker", date: 1288323623006 },
        { garage: "N/A", issue: "Invalid Parker", date: 1288323624102 },
        { garage: "Detroit Opera House", issue: "Duplicate Parker", date: 1288383623009 },
        { garage: "Book Cadillac | AT&T Lot", issue: "Invalid Parker", date: 1288322433050 },
        { garage: "1001 Woodward", issue: "Invalid Parker", date: 1288323738006 },
        { garage: "Howard Lot A", issue: "Invalid Parker", date: 1288323620000 },
        { garage: "Premier", issue: "Duplicate Parker", date: 1288323476006 },
        { garage: "COBO Rooftop", issue: "Duplicate Parker", date: 1288323622206 },
        { garage: "2 Detroit", issue: "No Parking Spot", date: 1288323543006 },
        { garage: "Howard Lot C", issue: "Invalid Parker", date: 1288323223111 },
        { garage: "COBO Congress", issue: "Duplicate Parker", date: 1288323623006 },
        { garage: "N/A", issue: "No Parking Spot", date: 1288323624102 },
        { garage: "Detroit Opera House", issue: "No Parking Spot", date: 1288383623009 },
        { garage: "Book Cadillac | AT&T Lot", issue: "No Parking Spot", date: 1288322433050 },
        { garage: "1001 Woodward", issue: "Duplicate Parker", date: 1288323738006 },
        { garage: "Howard Lot A", issue: "Invalid Parker", date: 1288323620000 },
        { garage: "Premier", issue: "No Parking Spot", date: 1288323476006 },
        { garage: "COBO Rooftop", issue: "Duplicate Parker", date: 1288323622206 },
        { garage: "2 Detroit", issue: "Invalid Parker", date: 1288323543006 },
        { garage: "Howard Lot C", issue: "Duplicate Parker", date: 1288323223111 },
        { garage: "COBO Congress", issue: "Duplicate Parker", date: 1288323623006 },
        { garage: "N/A", issue: "Invalid Parker", date: 1288323624102 },
        { garage: "Detroit Opera House", issue: "Duplicate Parker", date: 1288383623009 },
        { garage: "Book Cadillac | AT&T Lot", issue: "Invalid Parker", date: 1288322433050 },
        { garage: "1001 Woodward", issue: "Invalid Parker", date: 1288323738006 },
        { garage: "Howard Lot A", issue: "Invalid Parker", date: 1288323620000 },
        { garage: "Premier", issue: "Duplicate Parker", date: 1288323476006 },
        { garage: "COBO Rooftop", issue: "Duplicate Parker", date: 1288323622206 },
        { garage: "2 Detroit", issue: "No Parking Spot", date: 1288323543006 },
        { garage: "Howard Lot C", issue: "Invalid Parker", date: 1288323223111 },
        { garage: "COBO Congress", issue: "Duplicate Parker", date: 1288323623006 },
        { garage: "N/A", issue: "No Parking Spot", date: 1288323624102 },
        { garage: "Detroit Opera House", issue: "No Parking Spot", date: 1288383623009 },
        { garage: "Book Cadillac | AT&T Lot", issue: "No Parking Spot", date: 1288322433050 },
        { garage: "1001 Woodward", issue: "Duplicate Parker", date: 1288323738006 },
        { garage: "Howard Lot A", issue: "Invalid Parker", date: 1288323620000 },
        { garage: "Premier", issue: "No Parking Spot", date: 1288323476006 },
        { garage: "COBO Rooftop", issue: "Duplicate Parker", date: 1288323622206 },
        { garage: "2 Detroit", issue: "Invalid Parker", date: 1288323543006 },
        { garage: "Howard Lot C", issue: "Duplicate Parker", date: 1288323223111 },
        { garage: "COBO Congress", issue: "Duplicate Parker", date: 1288323623006 },
        { garage: "N/A", issue: "Invalid Parker", date: 1288323624102 },
        { garage: "Detroit Opera House", issue: "Duplicate Parker", date: 1288383623009 },
        { garage: "Book Cadillac | AT&T Lot", issue: "Invalid Parker", date: 1288322433050 },
        { garage: "1001 Woodward", issue: "Invalid Parker", date: 1288323738006 },
        { garage: "Howard Lot A", issue: "Invalid Parker", date: 1288323620000 },
        { garage: "Premier", issue: "Duplicate Parker", date: 1288323476006 },
        { garage: "COBO Rooftop", issue: "Duplicate Parker", date: 1288323622206 },
        { garage: "2 Detroit", issue: "No Parking Spot", date: 1288323543006 },
        { garage: "Howard Lot C", issue: "Invalid Parker", date: 1288323223111 },
        { garage: "COBO Congress", issue: "Duplicate Parker", date: 1288323623006 },
        { garage: "N/A", issue: "No Parking Spot", date: 1288323624102 },
        { garage: "Detroit Opera House", issue: "No Parking Spot", date: 1288383623009 },
        { garage: "Book Cadillac | AT&T Lot", issue: "No Parking Spot", date: 1288322433050 },
        { garage: "1001 Woodward", issue: "Duplicate Parker", date: 1288323738006 },
        { garage: "Howard Lot A", issue: "Invalid Parker", date: 1288323620000 },
        { garage: "Premier", issue: "No Parking Spot", date: 1288323476006 },
        { garage: "COBO Rooftop", issue: "Duplicate Parker", date: 1288323622206 },
        { garage: "2 Detroit", issue: "Invalid Parker", date: 1288323543006 },
        { garage: "Howard Lot C", issue: "Duplicate Parker", date: 1288323223111 }
    ];

})();