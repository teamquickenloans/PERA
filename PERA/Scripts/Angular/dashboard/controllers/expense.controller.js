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
        this.uploads = uploads;
    };
    

    var issues = [
        { garage: "COBO Congress", name: "Jessica Filcher", commonID: 12546743, issue: "Duplicate Parker", date: 1288323623006 },
        { garage: "N/A", name: "Bill Roberts", commonID: 13746382, issue: "Invalid Parker", date: 1288323624102 },
        { garage: "Detroit Opera House", name: "Howard Jones", commonID: 83750298, issue: "Duplicate Parker", date: 1288383623009 },
        { garage: "Book Cadillac | AT&T Lot", name: "Steve Wilko", commonID: 23876409, issue: "Invalid Parker", date: 1288322433050 },
        { garage: "1001 Woodward", name: "Jennifer Hartle", commonID: 78653492, issue: "Invalid Parker", date: 1288323738006 },
        { garage: "Howard Lot A", name: "Eric Miles", commonID: 90783462, issue: "Invalid Parker", date: 1288323620000 },
        { garage: "Premier", name: "Guy Anthony", commonID: 78659342, issue: "Duplicate Parker", date: 1288323476006 },
        { garage: "COBO Rooftop", name: "Ryan Filarski", commonID: 87943212, issue: "Duplicate Parker", date: 1288323622206 },
        { garage: "2 Detroit", name: "Mona Deliwala", commonID: 23478635, issue: "No Parking Spot", date: 1288323543006 },
        { garage: "Howard Lot C", name: "Erin Werner", commonID: 54827946, issue: "Invalid Parker", date: 1288323223111 },
        { garage: "COBO Congress", name: "Jessica Filcher", commonID: 12546743, issue: "Duplicate Parker", date: 1288323623006 },
        { garage: "N/A", name: "Bill Roberts", commonID: 13746382, issue: "Invalid Parker", date: 1288323624102 },
        { garage: "Detroit Opera House", name: "Howard Jones", commonID: 83750298, issue: "Duplicate Parker", date: 1288383623009 },
        { garage: "Book Cadillac | AT&T Lot", name: "Steve Wilko", commonID: 23876409, issue: "Invalid Parker", date: 1288322433050 },
        { garage: "1001 Woodward", name: "Jennifer Hartle", commonID: 78653492, issue: "Invalid Parker", date: 1288323738006 },
        { garage: "Howard Lot A", name: "Eric Miles", commonID: 90783462, issue: "Invalid Parker", date: 1288323620000 },
        { garage: "Premier", name: "Guy Anthony", commonID: 78659342, issue: "Duplicate Parker", date: 1288323476006 },
        { garage: "COBO Rooftop", name: "Ryan Filarski", commonID: 87943212, issue: "Duplicate Parker", date: 1288323622206 },
        { garage: "2 Detroit", name: "Mona Deliwala", commonID: 23478635, issue: "No Parking Spot", date: 1288323543006 },
        { garage: "Howard Lot C", name: "Erin Werner", commonID: 54827946, issue: "Invalid Parker", date: 1288323223111 },
        { garage: "COBO Congress", name: "Jessica Filcher", commonID: 12546743, issue: "Duplicate Parker", date: 1288323623006 },
        { garage: "N/A", name: "Bill Roberts", commonID: 13746382, issue: "Invalid Parker", date: 1288323624102 },
        { garage: "Detroit Opera House", name: "Howard Jones", commonID: 83750298, issue: "Duplicate Parker", date: 1288383623009 },
        { garage: "Book Cadillac | AT&T Lot", name: "Steve Wilko", commonID: 23876409, issue: "Invalid Parker", date: 1288322433050 },
        { garage: "1001 Woodward", name: "Jennifer Hartle", commonID: 78653492, issue: "Invalid Parker", date: 1288323738006 },
        { garage: "Howard Lot A", name: "Eric Miles", commonID: 90783462, issue: "Invalid Parker", date: 1288323620000 },
        { garage: "Premier", name: "Guy Anthony", commonID: 78659342, issue: "Duplicate Parker", date: 1288323476006 },
        { garage: "COBO Rooftop", name: "Ryan Filarski", commonID: 87943212, issue: "Duplicate Parker", date: 1288323622206 },
        { garage: "2 Detroit", name: "Mona Deliwala", commonID: 23478635, issue: "No Parking Spot", date: 1288323543006 },
        { garage: "Howard Lot C", name: "Erin Werner", commonID: 54827946, issue: "Invalid Parker", date: 1288323223111 },
        { garage: "COBO Congress", name: "Jessica Filcher", commonID: 12546743, issue: "Duplicate Parker", date: 1288323623006 },
        { garage: "N/A", name: "Bill Roberts", commonID: 13746382, issue: "Invalid Parker", date: 1288323624102 },
        { garage: "Detroit Opera House", name: "Howard Jones", commonID: 83750298, issue: "Duplicate Parker", date: 1288383623009 },
        { garage: "Book Cadillac | AT&T Lot", name: "Steve Wilko", commonID: 23876409, issue: "Invalid Parker", date: 1288322433050 },
        { garage: "1001 Woodward", name: "Jennifer Hartle", commonID: 78653492, issue: "Invalid Parker", date: 1288323738006 },
        { garage: "Howard Lot A", name: "Eric Miles", commonID: 90783462, issue: "Invalid Parker", date: 1288323620000 },
        { garage: "Premier", name: "Guy Anthony", commonID: 78659342, issue: "Duplicate Parker", date: 1288323476006 },
        { garage: "COBO Rooftop", name: "Ryan Filarski", commonID: 87943212, issue: "Duplicate Parker", date: 1288323622206 },
        { garage: "2 Detroit", name: "Mona Deliwala", commonID: 23478635, issue: "No Parking Spot", date: 1288323543006 },
        { garage: "Howard Lot C", name: "Erin Werner", commonID: 54827946, issue: "Invalid Parker", date: 1288323223111 },
        { garage: "COBO Congress", name: "Jessica Filcher", commonID: 12546743, issue: "Duplicate Parker", date: 1288323623006 },
        { garage: "N/A", name: "Bill Roberts", commonID: 13746382, issue: "Invalid Parker", date: 1288323624102 },
        { garage: "Detroit Opera House", name: "Howard Jones", commonID: 83750298, issue: "Duplicate Parker", date: 1288383623009 },
        { garage: "Book Cadillac | AT&T Lot", name: "Steve Wilko", commonID: 23876409, issue: "Invalid Parker", date: 1288322433050 },
        { garage: "1001 Woodward", name: "Jennifer Hartle", commonID: 78653492, issue: "Invalid Parker", date: 1288323738006 },
        { garage: "Howard Lot A", name: "Eric Miles", commonID: 90783462, issue: "Invalid Parker", date: 1288323620000 },
        { garage: "Premier", name: "Guy Anthony", commonID: 78659342, issue: "Duplicate Parker", date: 1288323476006 },
        { garage: "COBO Rooftop", name: "Ryan Filarski", commonID: 87943212, issue: "Duplicate Parker", date: 1288323622206 },
        { garage: "2 Detroit", name: "Mona Deliwala", commonID: 23478635, issue: "No Parking Spot", date: 1288323543006 },
        { garage: "Howard Lot C", name: "Erin Werner", commonID: 54827946, issue: "Invalid Parker", date: 1288323223111 }
    ];
    

    var uploads = [
        { garage: "Howard Lot C", type: "Invoice", date: 1288323223111 },
        { garage: "COBO Congress", type: "Invoice", date: 1288323738006 },
        { garage: "Howard Lot A", type: "Invoice", date: 1288323476006 },
        { garage: "1001 Woodward", type: "Invoice", date: 1288323624102 },
        { garage: "Premier", type: "Invoice", date: 1288323543006 },
        { garage: "Book Cadillac", type: "Invoice", date: 1288323622206 },
        { garage: "2 Detroit", type: "Invoice", date: 1288383623009 },
        { garage: "AT&T Lot", type: "Invoice", date: 1288322433050 },
        { garage: "Howard Lot C", type: "Invoice", date: 1288323223111 },
        { garage: "COBO Congress", type: "Invoice", date: 1288323738006 },
        { garage: "Howard Lot A", type: "Invoice", date: 1288323476006 },
        { garage: "1001 Woodward", type: "Invoice", date: 1288323624102 },
        { garage: "Premier", type: "Invoice", date: 1288323543006 },
        { garage: "Book Cadillac", type: "Invoice", date: 1288323622206 },
        { garage: "2 Detroit", type: "Invoice", date: 1288383623009 },
        { garage: "AT&T Lot", type: "Invoice", date: 1288322433050 },
        { garage: "Howard Lot C", type: "Invoice", date: 1288323223111 },
        { garage: "COBO Congress", type: "Invoice", date: 1288323738006 },
        { garage: "Howard Lot A", type: "Invoice", date: 1288323476006 },
        { garage: "1001 Woodward", type: "Invoice", date: 1288323624102 },
        { garage: "Premier", type: "Invoice", date: 1288323543006 },
        { garage: "Book Cadillac", type: "Invoice", date: 1288323622206 },
        { garage: "2 Detroit", type: "Invoice", date: 1288383623009 },
        { garage: "AT&T Lot", type: "Invoice", date: 1288322433050 },
        { garage: "Howard Lot C", type: "Invoice", date: 1288323223111 },
        { garage: "COBO Congress", type: "Invoice", date: 1288323738006 },
        { garage: "Howard Lot A", type: "Invoice", date: 1288323476006 },
        { garage: "1001 Woodward", type: "Invoice", date: 1288323624102 },
        { garage: "Premier", type: "Invoice", date: 1288323543006 },
        { garage: "Book Cadillac", type: "Invoice", date: 1288323622206 },
        { garage: "2 Detroit", type: "Invoice", date: 1288383623009 },
        { garage: "AT&T Lot", type: "Invoice", date: 1288322433050 },
        { garage: "Howard Lot C", type: "Invoice", date: 1288323223111 },
        { garage: "COBO Congress", type: "Invoice", date: 1288323738006 },
        { garage: "Howard Lot A", type: "Invoice", date: 1288323476006 },
        { garage: "1001 Woodward", type: "Invoice", date: 1288323624102 },
        { garage: "Premier", type: "Invoice", date: 1288323543006 },
        { garage: "Book Cadillac", type: "Invoice", date: 1288323622206 },
        { garage: "2 Detroit", type: "Invoice", date: 1288383623009 },
        { garage: "AT&T Lot", type: "Invoice", date: 1288322433050 },
        { garage: "Howard Lot C", type: "Invoice", date: 1288323223111 },
        { garage: "COBO Congress", type: "Invoice", date: 1288323738006 },
        { garage: "Howard Lot A", type: "Invoice", date: 1288323476006 },
        { garage: "1001 Woodward", type: "Invoice", date: 1288323624102 },
        { garage: "Premier", type: "Invoice", date: 1288323543006 },
        { garage: "Book Cadillac", type: "Invoice", date: 1288323622206 },
        { garage: "2 Detroit", type: "Invoice", date: 1288383623009 },
        { garage: "AT&T Lot", type: "Invoice", date: 1288322433050 },
        { garage: "Howard Lot C", type: "Invoice", date: 1288323223111 },
        { garage: "COBO Congress", type: "Invoice", date: 1288323738006 },
        { garage: "Howard Lot A", type: "Invoice", date: 1288323476006 },
        { garage: "1001 Woodward", type: "Invoice", date: 1288323624102 },
        { garage: "Premier", type: "Invoice", date: 1288323543006 },
        { garage: "Book Cadillac", type: "Invoice", date: 1288323622206 },
        { garage: "2 Detroit", type: "Invoice", date: 1288383623009 },
        { garage: "AT&T Lot", type: "Invoice", date: 1288322433050 }
    ];

    
})();